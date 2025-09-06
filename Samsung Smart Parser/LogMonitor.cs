using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Samsung_Smart_Parser
{
    /// <summary>
    /// LogMonitor watches CSV logs from test equipment, reconstructs test blocks
    /// and forwards result to ApiService and optionally writes to the Datalake.
    /// This version keeps the original behavior but simplifies/clarifies responsibilities.
    /// </summary>
    public class LogMonitor
    {
        private static readonly string folderPath = ConfigurationManager.AppSettings["LogPath"];
        private static readonly string datalakePath = ConfigurationManager.AppSettings["DatalakePath"];
        private static FileSystemWatcher watcher;
        private static readonly Dictionary<string, FileState> fileStates = new Dictionary<string, FileState>();
        private static readonly object fileStatesLock = new object();
        private static bool running = false;
        public static event Action<string> OnLogDetected;
        private static Func<bool> _isDatalakeOn;

        private class FileState
        {
            public long LastPosition;
            public List<string> Buffer = new List<string>();
            public DateTime LastUpdate = DateTime.Now;
            public string FileName;
        }

        public LogMonitor(Func<bool> isDatalakeOnFunc)
        {
            _isDatalakeOn = isDatalakeOnFunc;
        }

        public static void Start()
        {
            if (running) return;
            running = true;

            watcher = new FileSystemWatcher(folderPath, "*.csv")
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size
            };
            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.EnableRaisingEvents = true;

            // Background loop to read new lines and process stalled buffers
            ThreadPool.QueueUserWorkItem(_ =>
            {
                while (running)
                {
                    try
                    {
                        List<string> keys;
                        lock (fileStatesLock) keys = fileStates.Keys.ToList();
                        foreach (var file in keys)
                        {
                            if (File.Exists(file))
                                ReadNewLines(file);
                        }
                        ProcessStalledBuffers();
                    }
                    catch (Exception ex)
                    {
                        OnLogDetected?.Invoke("Monitor error: " + ex.Message);
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        public static void Stop()
        {
            running = false;
            watcher?.Dispose();
            watcher = null;
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            lock (fileStatesLock)
            {
                if (!fileStates.ContainsKey(e.FullPath))
                {
                    fileStates[e.FullPath] = new FileState { FileName = Path.GetFileName(e.FullPath) };
                    OnLogDetected?.Invoke("New file monitored: " + fileStates[e.FullPath].FileName);
                }
            }
        }

        private static void ReadNewLines(string file)
        {
            FileState state;
            lock (fileStatesLock) state = fileStates[file];

            try
            {
                using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    if (fs.Length < state.LastPosition) state.LastPosition = 0;
                    fs.Seek(state.LastPosition, SeekOrigin.Begin);
                    using (var sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            ProcessLine(file, line);
                        }
                        state.LastPosition = fs.Position;
                    }
                }
                state.LastUpdate = DateTime.Now;
            }
            catch
            {
                // Ignored - monitor will try again
            }
        }

        private static void ProcessLine(string file, string line)
        {
            FileState state;
            lock (fileStatesLock) state = fileStates[file];

            lock (state)
            {
                if (line.StartsWith("#INIT"))
                {
                    state.Buffer.Clear();
                    state.Buffer.Add(line);
                }
                else if (state.Buffer.Count > 0)
                {
                    state.Buffer.Add(line);
                    if (line.StartsWith("TEST-TIME"))
                    {
                        ExtractInfo(state.Buffer, state.FileName);
                        state.Buffer.Clear();
                    }
                }
            }
        }

        private static void ProcessStalledBuffers()
        {
            List<FileState> toProcess = new List<FileState>();
            lock (fileStatesLock)
            {
                var now = DateTime.Now;
                foreach (var kvp in fileStates)
                {
                    var s = kvp.Value;
                    lock (s)
                    {
                        if (s.Buffer.Count > 0 && (now - s.LastUpdate).TotalSeconds > 5)
                            toProcess.Add(s);
                    }
                }
            }

            foreach (var s in toProcess)
            {
                lock (s)
                {
                    ExtractInfo(s.Buffer, s.FileName);
                    s.Buffer.Clear();
                }
            }
        }

        private static string GetValue(string line)
        {
            var idx = line.IndexOf(':');
            if (idx >= 0) return line.Substring(idx + 1).Trim();
            return "N/A";
        }

        public static void ExtractInfo(List<string> lines, string fileName)
        {
            // Default values
            string jig = "N/A", result = "N/A", failure = "N/A", serialNumber = "N/A", model = "N/A", testTime = "0";

            foreach (var raw in lines)
            {
                var line = raw.TrimStart();
                if (line.StartsWith("JIG")) jig = GetValue(line);
                else if (line.StartsWith("RESULT")) result = GetValue(line);
                else if (line.StartsWith("FAILITEM")) failure = GetValue(line);
                else if (line.StartsWith("P/N")) serialNumber = GetValue(line);
                else if (line.StartsWith("TEST-TIME")) testTime = GetValue(line);
                else if (line.StartsWith("MODEL")) model = GetValue(line);
            }

            // Compute times (end = now, init = now - seconds)
            var (initTime, endTime) = GetTestTimes(testTime);

            var equipPrefix = GetPrefixFromIni();
            string nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);

            string equipamento;
            string step = ExtractStepFromPrefix(equipPrefix);

            if (step == "DL" || step == "FCT")
            {
                var lastTwo = nameWithoutExt.Length >= 2 ? nameWithoutExt.Substring(nameWithoutExt.Length - 2) : "XX";
                equipamento = equipPrefix + lastTwo;
            }
            else
            {
                equipamento = equipPrefix;
            }

            // Send to MES
            var api = new ApiService();
            var sendToJems = api.SendSerialNumberFVTSync(serialNumber, equipamento, result, failure, step);

            // Build log for UI
            var dataHora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var logMsg = result.Equals("FAIL", StringComparison.OrdinalIgnoreCase)
                ? $"{dataHora} | {serialNumber} | {result} | {failure} | {equipamento} | {sendToJems} | {model}"
                : $"{dataHora} | {serialNumber} | {result} | N/A | {equipamento} | {sendToJems} | {model}";

            // Optionally write to datalake
            if (_isDatalakeOn())
                SendToTestPerformance(lines, serialNumber, equipamento, equipPrefix, model, result, initTime, endTime);

            OnLogDetected?.Invoke(logMsg);
        }

        private static void SendToTestPerformance(List<string> lines, string serialNumber, string equipamento, string equipPrefix, string model, string result, string initTime, string endTime)
        {
            int testStart = lines.FindIndex(l => l.Trim().StartsWith("#TEST"));
            int testEnd = lines.FindIndex(testStart + 1, l => l.Trim().StartsWith("#END"));

            if (testStart < 0 || testEnd <= testStart) return;

            var output = new List<string>();
            output.Add($"S{serialNumber}");           // Serial
            output.Add("CSAMSUNG");                  // Customer code
            output.Add($"N{equipamento}");           // Equipment
            output.Add($"P{ExtractStepTwoDigits(equipPrefix)}"); // P value (simple extraction)
            output.Add($"n{model}");                 // Model
            output.Add("O2566932");                  // Operator (hardcoded as original)
            var tValue = result.Equals("PASS", StringComparison.OrdinalIgnoreCase) ? "P" : result.Equals("FAIL", StringComparison.OrdinalIgnoreCase) ? "F" : result;
            output.Add($"T{tValue}");                // Test result
            output.Add($"[{endTime}");               // End time (format preserved)
            output.Add($"]{initTime}");              // Start time (format preserved)
            output.Add("");                          // blank line before measurements

            for (int i = testStart + 1; i < testEnd; i++)
            {
                var line = lines[i].Trim();
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("/")) continue;
                if (!line.Contains(",")) continue;
                var parts = line.Split(',');
                if (parts.Length < 5) continue;

                var testCondition = parts[0].Trim();
                var measuredValue = parts[1].Trim();
                var lowerLimit = parts[2].Trim();
                var upperLimit = parts[3].Trim();
                var pf = parts[4].Trim();

                output.Add($"M{testCondition}");
                output.Add($"d{measuredValue}");
                output.Add($"k{upperLimit}");
                output.Add($"l{lowerLimit}");
                output.Add("UdB");
                output.Add($"q{pf}");
                output.Add(""); // blank line between records
            }

            try
            {
                Directory.CreateDirectory(datalakePath);
                var filename = Path.Combine(datalakePath, $"{serialNumber}_{DateTime.Now:yyyyMMdd_HHmmssfff}.txt");
                File.WriteAllLines(filename, output, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                OnLogDetected?.Invoke("Datalake write error: " + ex.Message);
            }
        }

        private static (string initTime, string endTime) GetTestTimes(string testTimeSecondsStr)
        {
            if (!int.TryParse(testTimeSecondsStr, out int seconds)) seconds = 0;
            var end = DateTime.Now;
            var init = end.AddSeconds(-seconds);
            return (init.ToString("yyyy/MM/dd'T'HH:mm:ss"), end.ToString("yyyy/MM/dd'T'HH:mm:ss"));
        }

        private static string ExtractStepFromPrefix(string prefix)
        {
            if (string.IsNullOrEmpty(prefix)) return "XX";
            int first = prefix.IndexOfAny(new char[] { '-', '_' });
            if (first < 0) return prefix;
            int second = prefix.IndexOfAny(new char[] { '-', '_' }, first + 1);
            if (second < 0) return prefix.Substring(first + 1);
            return prefix.Substring(first + 1, second - first - 1);
        }

        private static string ExtractStepTwoDigits(string prefix)
        {
            var first = prefix.IndexOfAny(new char[] { '-', '_' });
            if (first < 0) return "XX";
            if (prefix.Length >= first + 3) return prefix.Substring(first + 1, 2);
            return "XX";
        }

        private static string GetPrefixFromIni()
        {
            var iniPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");
            if (!File.Exists(iniPath)) return "XX";
            foreach (var line in File.ReadAllLines(iniPath))
            {
                var trimmed = line.Trim();
                if (trimmed.StartsWith("Prefix=", StringComparison.OrdinalIgnoreCase))
                    return trimmed.Substring("Prefix=".Length).Trim();
            }
            return "XX";
        }
    }
}
