using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace Samsung_Smart_Parser
{
    /// <summary>
    /// Lightweight watcher for FNI (Final Inspection) files.
    /// It reads the file, determines PASS/FAIL and optionally calls API.
    /// </summary>
    internal class LogMonitorFNI
    {
        private FileSystemWatcher watcher;
        private static readonly string logFNIPath = ConfigurationManager.AppSettings["LogPathFNI"];
        private readonly ApiService apiService = new ApiService();
        private readonly string equipPrefix = GetPrefixFromIni();

        public static event Action<string> OnLogDetectedFNI;

        public void Start()
        {
            if (!Directory.Exists(logFNIPath))
            {
                MessageBox.Show($"FNI path does not exist: {logFNIPath}");
                return;
            }

            watcher = new FileSystemWatcher(logFNIPath);
            watcher.Filter = "*.*";
            watcher.Created += ArquivoCriado;
            watcher.EnableRaisingEvents = true;
        }

        private void ArquivoCriado(object sender, FileSystemEventArgs e)
        {
            // Small delay to let file be completely written
            System.Threading.Thread.Sleep(200);
            ProcessarArquivo(e.FullPath);
        }

        public void ProcessarArquivo(string caminhoArquivo)
        {
            try
            {
                var linhas = File.ReadAllLines(caminhoArquivo);
                if (linhas.Length < 3)
                {
                    OnLogDetectedFNI?.Invoke("Invalid FNI file: too few lines");
                    return;
                }

                var serialNumber = linhas[0].Trim().Split('_')[0];
                var statusLine = linhas[2].Trim();
                var result = InterpretarStatus(statusLine);

                // Extract short step and equipment (basic parsing from prefix)
                var step = ExtractStepFromPrefix(equipPrefix);
                var equipment = equipPrefix; // simplified; keep as prefix

                string sendToJems;
                if (result == "FAIL")
                {
                    // In original code FAIL did not call AddDefect endpoint automatically.
                    // Here we follow the same behavior but it can be extended to call SendSerialNumberQCFAILSync.
                    sendToJems = "NO";
                }
                else
                {
                    sendToJems = apiService.SendSerialNumberQCPASSSync(serialNumber, result, step, equipment);
                }

                // Move processed file to backup folder (if exists)
                var backupPath = @"C:\SMARTBACKUP";
                if (!Directory.Exists(backupPath))
                    Directory.CreateDirectory(backupPath);

                var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                var novoNome = Path.Combine(backupPath, $"{serialNumber}_{timestamp}_{result}.txt");
                File.Move(caminhoArquivo, novoNome);

                var logMsg = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {serialNumber} | {result} | N/A | {equipment} | {sendToJems} | FNI";
                OnLogDetectedFNI?.Invoke(logMsg);
            }
            catch (Exception ex)
            {
                OnLogDetectedFNI?.Invoke("Error processing FNI file: " + ex.Message);
            }
        }

        private static string InterpretarStatus(string linhaStatus)
        {
            bool topOk = linhaStatus.Contains("TOP: 1");
            bool botOk = linhaStatus.Contains("BOT: 1");
            return topOk && botOk ? "PASS" : "FAIL";
        }

        private static string ExtractStepFromPrefix(string prefix)
        {
            // Very light parsing to extract the step (between first and second separator)
            if (string.IsNullOrEmpty(prefix)) return "XX";
            int first = prefix.IndexOfAny(new char[] { '-', '_' });
            if (first < 0) return prefix;
            int second = prefix.IndexOfAny(new char[] { '-', '_' }, first + 1);
            if (second < 0) return prefix.Substring(first + 1);
            return prefix.Substring(first + 1, second - first - 1);
        }

        private static string GetPrefixFromIni()
        {
            var iniPath = Path.Combine(Application.StartupPath, "config.ini");
            if (!File.Exists(iniPath)) return "XX";
            foreach (var line in File.ReadAllLines(iniPath))
            {
                var trimmed = line.Trim();
                if (trimmed.StartsWith("Prefix=", StringComparison.OrdinalIgnoreCase))
                    return trimmed.Substring("Prefix=".Length).Trim();
            }
            return "XX";
        }

        public void Stop()
        {
            watcher?.Dispose();
            watcher = null;
        }
    }
}
