using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace Samsung_Smart_Parser
{
    internal class LogMonitorFNI
    {
        private FileSystemWatcher watcher;
        static string logFNIPath = ConfigurationManager.AppSettings["LogPathFNI"];
        private ApiService apiService;
        string equipPrefix = GetPrefixFromIni();
        public static event Action<string> OnLogDetectedFNI;

        public LogMonitorFNI()
        {
            apiService = new ApiService();
        }

        public void Start()
        {
            string pasta = logFNIPath;

            if (!Directory.Exists(pasta))
            {
                MessageBox.Show($"A pasta {logFNIPath} não existe.");
                return;
            }

            

            watcher = new FileSystemWatcher(pasta);
            watcher.Filter = "*.*"; // Pode ajustar para "*.txt", "*.log", etc.
            watcher.Created += ArquivoCriado;
            watcher.EnableRaisingEvents = true;
        }

        private void ArquivoCriado(object sender, FileSystemEventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            ProcessarArquivo(e.FullPath);
        }

        public void ProcessarArquivo(string caminhoArquivo)
        {
            try
            {
                string[] linhas = File.ReadAllLines(caminhoArquivo);

                OnLogDetectedFNI?.Invoke("Novo arquivo monitorado: ");

                if (linhas.Length < 3)
                {
                    MessageBox.Show("Arquivo inválido: menos de 3 linhas");
                    return;
                }

                string serialNumber = linhas[0].Trim().Split('_')[0];

                string linhaStatus = linhas[2].Trim();

                string result = InterpretarStatus(linhaStatus);

                //MessageBox.Show($"Serial: {serialNumber}\nResultado: {result}");


                string dataHora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                int firstDashIndex = equipPrefix.IndexOf('-');
                int secondDashIndex = equipPrefix.IndexOf('-', firstDashIndex + 1);

                string step = equipPrefix.Substring(firstDashIndex + 1, secondDashIndex - firstDashIndex - 1);


                string equipamento = equipPrefix.Substring(0, equipPrefix.Length - 1);


                string sendToJems;

                if (result == "FAIL")
                {
                    MessageBox.Show("NO DEFECT ENDPOINT");
                    sendToJems = "NO";
                }
                else
                {
                    sendToJems = apiService.SendSerialNumberQCPASSSync(serialNumber, result, step, equipamento);
                }

                string logMsg = $"{dataHora} | {serialNumber} | {result} | N/A | {equipamento} | {sendToJems} | A065";


                string backupPath = @"C:\SMARTBACKUP";

                if (!Directory.Exists(backupPath))
                {
                    MessageBox.Show("A pasta C:\\SMARTBACKUP não existe.");
                }
                else
                {
                    // Monta o novo nome do arquivo: SERIAL_YYYY-MM-DD_HH-MM-SS.txt
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                    string novoNome = $"{serialNumber}_{timestamp}_{result}.txt";
                    string destinoCompleto = Path.Combine(backupPath, novoNome);

                    try
                    {
                        File.Move(caminhoArquivo, destinoCompleto);
                    }
                    catch (Exception moveEx)
                    {
                        MessageBox.Show("Erro ao mover o arquivo para o backup: " + moveEx.Message);
                    }
                }

                OnLogDetectedFNI?.Invoke(logMsg);


            }


            catch (Exception ex)
            {
                MessageBox.Show("Erro ao processar arquivo: " + ex.Message);
            }


        }

        private string InterpretarStatus(string linhaStatus)
        {
            bool topOk = linhaStatus.Contains("TOP: 1");
            bool botOk = linhaStatus.Contains("BOT: 1");

            if (topOk && botOk)
                return "PASS";

            return "FAIL";
        }

        public void Stop()
        {
            if (watcher != null)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
                watcher = null;
            }
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

    }
}
