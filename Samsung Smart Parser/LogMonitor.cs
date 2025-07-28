using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace Samsung_Smart_Parser
{


    public class LogMonitor
    {
        //public event Action<string> OnLogDetected; // ← AQUI


        static string folderPath = ConfigurationManager.AppSettings["LogPath"];
        static string datalakePath = ConfigurationManager.AppSettings["DatalakePath"];
        static FileSystemWatcher watcher;
        static readonly Dictionary<string, FileState> fileStates = new Dictionary<string, FileState>();
        static readonly object fileStatesLock = new object();

        private static bool running = false;

        // Evento público para avisar o Form de um novo log
        public static event Action<string> OnLogDetected;

        public static event Action<string> OnApiResponse;

        private static Func<bool> _isDatalakeOn;

        public LogMonitor(Func<bool> isDatalakeOnFunc)
        {
            _isDatalakeOn = isDatalakeOnFunc;
        }


        class FileState
        {
            public long LastPosition;
            public List<string> Buffer = new List<string>();
            public bool IsReadingResult = false;
            public DateTime LastUpdate = DateTime.Now;
            public string FileName;
        }

        public static void Start()
        {
            if (running) return;
            running = true;

            watcher = new FileSystemWatcher(folderPath, "*.csv");
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size;
            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.EnableRaisingEvents = true;

            Task.Run(() =>
            {
                while (running)
                {
                    try
                    {
                        List<string> filesToRead;

                        lock (fileStatesLock)
                        {
                            filesToRead = new List<string>(fileStates.Keys);
                        }

                        foreach (var file in filesToRead)
                        {
                            if (File.Exists(file))
                            {
                                ReadNewLines(file);
                            }
                        }

                        ProcessStalledBuffers();
                    }
                    catch (Exception ex)
                    {
                        OnLogDetected?.Invoke("Erro no monitor: " + ex.Message);
                    }

                    Thread.Sleep(1000);
                }
            });
        }

        public static void Stop()
        {
            running = false;
            if (watcher != null)
            {
                watcher.Dispose();
                watcher = null;
            }
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            string file = e.FullPath;

            lock (fileStatesLock)
            {
                if (!fileStates.ContainsKey(file))
                {
                    fileStates[file] = new FileState
                    {
                        FileName = Path.GetFileName(file)
                    };

                    OnLogDetected?.Invoke("Novo arquivo monitorado: " + fileStates[file].FileName);
                }
            }
        }

        private static void ReadNewLines(string file)
        {
            FileState state;

            lock (fileStatesLock)
            {
                state = fileStates[file];
            }

            long lastPos = state.LastPosition;

            try
            {
                using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    if (fs.Length < lastPos)
                        lastPos = 0;

                    fs.Seek(lastPos, SeekOrigin.Begin);

                    using (var sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            ProcessLine(file, line);
                        }

                        lastPos = fs.Position;
                    }
                }

                lock (fileStatesLock)
                {
                    state.LastPosition = lastPos;
                    state.LastUpdate = DateTime.Now;
                }
            }
            catch
            {
                // Pode ignorar erros aqui, ou logar se quiser
            }
        }

        private static void ProcessLine(string file, string line)
        {
            FileState state;

            lock (fileStatesLock)
            {
                state = fileStates[file];
            }

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
            List<FileState> statesToProcess = new List<FileState>();

            lock (fileStatesLock)
            {
                var now = DateTime.Now;
                foreach (var kvp in fileStates)
                {
                    var state = kvp.Value;

                    lock (state)
                    {
                        if (state.Buffer.Count > 0 && (now - state.LastUpdate).TotalSeconds > 5)
                        {
                            statesToProcess.Add(state);
                        }
                    }
                }
            }

            foreach (var state in statesToProcess)
            {
                lock (state)
                {
                    ExtractInfo(state.Buffer, state.FileName);
                    state.Buffer.Clear();
                }
            }
        }

        private static string GetValue(string line)
        {
            int index = line.IndexOf(':');
            if (index >= 0)
                return line.Substring(index + 1).Trim();
            return "N/A";
        }

        public static void ExtractInfo(List<string> lines, string fileName)
        {
            string jig = "N/A", result = "N/A", failure = "N/A", serialNumber = "N/A";
            string equipPrefix = GetPrefixFromIni();
            string step = "XX";
            int firstDashIndex = equipPrefix.IndexOf('-');

            int firstSeparatorIndex = equipPrefix.IndexOfAny(new char[] { '-', '_' });

            if (firstSeparatorIndex != -1)
            {
                // Procurar o segundo separador a partir do próximo caractere
                int secondSeparatorIndex = equipPrefix.IndexOfAny(new char[] { '-', '_' }, firstSeparatorIndex + 1);

                if (secondSeparatorIndex != -1 && secondSeparatorIndex > firstSeparatorIndex + 1)
                {
                    step = equipPrefix.Substring(firstSeparatorIndex + 1, secondSeparatorIndex - firstSeparatorIndex - 1);
                }
            }
            string model = "N/A"; // Variável para guardar o model
            string testTime = "N/A"; // Variável para guardar o model

            foreach (var line in lines)
            {
                string trimmed = line.TrimStart();
                if (trimmed.StartsWith("JIG"))
                    jig = GetValue(trimmed);
                else if (trimmed.StartsWith("RESULT"))
                    result = GetValue(trimmed);
                else if (trimmed.StartsWith("FAILITEM"))
                    failure = GetValue(trimmed);
                else if (trimmed.StartsWith("P/N"))
                    serialNumber = GetValue(trimmed);
                else if (trimmed.StartsWith("TEST-TIME"))
                    testTime = GetValue(trimmed);
                else if (trimmed.StartsWith("MODEL"))
                    model = GetValue(trimmed);  // Captura o valor do MODEL
            }

            //MessageBox.Show($"Tempo do teste: {testTime}", "TEST-TIME", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var (initTime, endTime) = GetTestTimes(testTime);
            //MessageBox.Show($"Tempo do teste: {initTime}", "TEST-TIME", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //MessageBox.Show($"Tempo do teste: {endTime}", "TEST-TIME", MessageBoxButtons.OK, MessageBoxIcon.Information);

            string nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);

            string lastTwo = "";
            string equipamento = "";

            if (step == "DL" || step == "FCT")
            {
                lastTwo = nameWithoutExt.Length >= 2 ? nameWithoutExt.Substring(nameWithoutExt.Length - 2, 2) : "XX";
                equipamento = equipPrefix + lastTwo;
            }
            else
            {
                equipamento = equipPrefix;
            }
                string dataHora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var api = new ApiService();
            string sendToJems = api.SendSerialNumberFVTSync(serialNumber, equipamento, result, failure, step);

            string logMsg = result == "FAIL"
                ? $"{dataHora} | {serialNumber} | {result} | {failure} | {equipamento} | {sendToJems} | {model}"
                : $"{dataHora} | {serialNumber} | {result} | N/A | {equipamento} | {sendToJems} | {model}";

            if (_isDatalakeOn())
            {
                
                SendToTestPerformance(lines, serialNumber, equipamento, equipPrefix, model, result, initTime, endTime);
            }
            
            OnLogDetected?.Invoke(logMsg);
        }





        private static string GetPrefixFromIni()
        {
            string iniPath = Path.Combine(Application.StartupPath, "config.ini");

            if (!File.Exists(iniPath))
                return "XX"; // valor padrão se o arquivo não existir

            string[] lines = File.ReadAllLines(iniPath);
            bool inSection = false;

            foreach (string line in lines)
            {
                string trimmed = line.Trim();

                if (trimmed.StartsWith("[") && trimmed.EndsWith("]"))
                {
                    inSection = trimmed.Equals("[Station Prefix]", StringComparison.OrdinalIgnoreCase);
                }
                else if (inSection && trimmed.StartsWith("Prefix=", StringComparison.OrdinalIgnoreCase))
                {
                    return trimmed.Substring("Prefix=".Length).Trim();
                }
            }

            return "XX"; // valor padrão se não encontrar
        }

        private static void SendToTestPerformance(
            List<string> lines,
            string serialNumber,
            string equipamento,
            string equipPrefix,
            string model,
            string result,
            string initTime,
            string endTime)
        {
            int testStart = lines.FindIndex(l => l.Trim().StartsWith("#TEST"));
            int testEnd = lines.FindIndex(testStart + 1, l => l.Trim().StartsWith("#END"));

            if (testStart >= 0 && testEnd > testStart)
            {
                List<string> outputLines = new List<string>();

                // Cabeçalho conforme solicitado
                outputLines.Add($"S{serialNumber}");
                outputLines.Add("CSAMSUNG");

                outputLines.Add($"N{equipamento}");

                // Pega 2 dígitos após o primeiro '-' em equipPrefix
                string pValue = "XX";
                int firstDash = equipPrefix.IndexOf('-');
                if (firstDash >= 0 && equipPrefix.Length >= firstDash + 3)
                    pValue = equipPrefix.Substring(firstDash + 1, 2);
                outputLines.Add($"P{pValue}");

                outputLines.Add($"n{model}");
                outputLines.Add("O2566932");

                // T {result} transformado em P ou F
                string tValue = result.ToUpper() == "PASS" ? "P" : result.ToUpper() == "FAIL" ? "F" : result;
                outputLines.Add($"T{tValue}");
                outputLines.Add($"[{endTime}");
                outputLines.Add($"]{initTime}");

                outputLines.Add(""); // Linha em branco antes dos dados

                for (int i = testStart + 1; i < testEnd; i++)
                {
                    string line = lines[i].Trim();

                    if (line.StartsWith("/") || string.IsNullOrWhiteSpace(line)) continue;
                    if (line.Contains("Test Conditions") && line.Contains("Measured Value")) continue;

                    string[] parts = line.Split(',');
                    if (parts.Length < 5) continue;

                    string testCondition = parts[0].Trim();
                    string measuredValue = parts[1].Trim();
                    string lowerLimit = parts[2].Trim();
                    string upperLimit = parts[3].Trim();
                    string pf = parts[4].Trim();

                    outputLines.Add($"M{testCondition}");
                    outputLines.Add($"d{measuredValue}");
                    outputLines.Add($"k{upperLimit}");
                    outputLines.Add($"l{lowerLimit}");
                    outputLines.Add("UdB");
                    outputLines.Add($"q{pf}");
                    outputLines.Add("");
                }

                try
                {
                    //Directory.CreateDirectory(@"C:\test");
                    string filename = $@"{datalakePath}\{serialNumber}_{DateTime.Now:yyyyMMdd_HHmmssfff}.txt";
                    File.WriteAllLines(filename, outputLines, Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao salvar arquivo TEST: " + ex.Message);
                }
            }


        }

        private static (string initTime, string endTime) GetTestTimes(string testTimeSecondsStr)
        {
            int testTimeSeconds = 0;
            if (!int.TryParse(testTimeSecondsStr, out testTimeSeconds))
            {
                testTimeSeconds = 0; // padrão caso não seja possível converter
            }

            DateTime initTime = DateTime.Now;
            string initTimeFormatted = initTime.ToString("yyyy/MM/dd'T'HH:mm:ss");

            DateTime endTime = initTime.AddSeconds(-testTimeSeconds);
            string endTimeFormatted = endTime.ToString("yyyy/MM/dd'T'HH:mm:ss");

            return (initTimeFormatted, endTimeFormatted);
        }



    }
}