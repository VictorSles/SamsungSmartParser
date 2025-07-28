using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Samsung_Smart_Parser
{
    public partial class Form1 : Form
    {
        private LogMonitorFNI monitorFNI;
        private string apiUrl = "http://10.56.17.58/Mes4Api/swagger/ui/index";
        private bool tentarNovamente = true;
        private bool datalakeOn = true;
        private bool AplicacaoRodando = false;
        private System.Windows.Forms.Timer apiHealthTimer;
        private HttpClient httpClient = new HttpClient();
        private APIConnError apiErrorForm;
        private FileSystemWatcher preStartWatcher;
        private static string senhaCorreta = ConfigurationManager.AppSettings["AdminPassword"];
        private System.Windows.Forms.Timer pastaCheckTimer;
        static string datalakePath = ConfigurationManager.AppSettings["DatalakePath"];
        private string equipmentPrefixGlobal = string.Empty;
        private bool FNI = false;


        public Form1()
        {
            InitializeComponent();
            ConfigurarLayoutEInterface();
        }

        private LogMonitor monitor;

        //private LogMonitorFNI monitorFNI;

        private void Form1_Load(object sender, EventArgs e)
        {

            pictureBoxPlay_Click(null, null);

            foreach (DataGridViewColumn col in dataGridViewHistory.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            monitor = new LogMonitor(() => this.datalakeOn);

            LogMonitor.OnLogDetected += AdicionarLogNaDataGridView;

            LogMonitorFNI.OnLogDetectedFNI += AdicionarLogNaDataGridView;

            pastaCheckTimer = new System.Windows.Forms.Timer();
            pastaCheckTimer.Interval = 15_000; // 3 minutos em milissegundos
            pastaCheckTimer.Tick += PastaCheckTimer_Tick;
            pastaCheckTimer.Start();

            string logPath = ConfigurationManager.AppSettings["LogPath"];

            dataGridViewHistory.Rows.Clear();

            // Adiciona 20 linhas em branco
            for (int i = 0; i < 14; i++)
            {
                dataGridViewHistory.Rows.Add();
            }

            if (Directory.Exists(logPath))
            {
                preStartWatcher = new FileSystemWatcher(logPath);
                preStartWatcher.Filter = "*.csv"; // Ou "*.*" se preferir monitorar tudo
                preStartWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
                preStartWatcher.Changed += PreStartWatcher_Changed;
                preStartWatcher.Created += PreStartWatcher_Changed;
                preStartWatcher.EnableRaisingEvents = true;
            }


            

            apiHealthTimer = new System.Windows.Forms.Timer();
            apiHealthTimer.Interval = 15_000; // checa a cada 30 segundos (30.000 ms)
            apiHealthTimer.Tick += ApiHealthTimer_Tick;

            PastaCheckTimer_Tick(null, null); // força uma verificação imediata

        }

        private void ConfigurarLayoutEInterface()
        {

            listViewPVError.ItemActivate += listViewPVError_ItemActivate;

            pictureBoxStop.SendToBack();
            pictureBoxPlay.BringToFront();
            pictureBoxDLON.BringToFront();
            listViewPVError.Columns[0].TextAlign = HorizontalAlignment.Center;
            pictureBoxPlay.Cursor = Cursors.Hand;
            pictureBoxStop.Cursor = Cursors.Hand;
            pictureBoxConfig.Cursor = Cursors.Hand;
            pictureBoxConfig.Cursor = Cursors.Hand;
            pictureBoxClose.Cursor = Cursors.Hand;
            pictureBoxDLON.Cursor = Cursors.Hand;
            pictureBoxDLOFF.Cursor = Cursors.Hand;
            //this.FormBorderStyle = FormBorderStyle.Sizable;
            //this.WindowState = FormWindowState.Normal;

        }

        private async void pictureBoxPlay_Click(object sender, EventArgs e)
        {
            if (!CarregarConfiguracaoEquipamentos())
                return;

            AtualizarStatus("Conectando-se ao JEMSmm...", Color.Yellow);

            bool conectado = false;
            tentarNovamente = true;
            int tentativas = 2;

            using (HttpClient client = new HttpClient())
            {
                while (!conectado && tentarNovamente && tentativas > 0)
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(apiUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            conectado = true;
                            AplicacaoRodando = true;
                            pictureBoxStop.BringToFront();
                            AtualizarStatus("Conectado ao JEMSmm", Color.Green);

                            if (preStartWatcher != null)
                            {
                                preStartWatcher.EnableRaisingEvents = false;
                                preStartWatcher.Dispose();
                                preStartWatcher = null;
                            }

                            if (equipmentPrefixGlobal.Contains("FNI"))
                            {
                                FNI = true;
                                pictureBoxDLOFF.BringToFront();
                                pictureBoxDLON.SendToBack();
                                datalakeOn = false;
                                dataGridViewHistory.Columns[3].HeaderText = "CRD";

                                if (monitorFNI == null)
                                    monitorFNI = new LogMonitorFNI();
                                monitorFNI.Start();
                            }
                            else
                            {
                                dataGridViewHistory.Columns[3].HeaderText = "FAILURE";
                                LogMonitor.Start();
                            }

                            apiHealthTimer.Start();
                        }
                        else
                        {
                            tentativas--;
                            AtualizarStatus($"Falha ao conectar-se ao JEMSmm!\nTentativas restantes: {tentativas}", Color.Orange);
                            await Task.Delay(2000);
                        }
                    }
                    catch
                    {
                        tentativas--;
                        AtualizarStatus($"Falha ao conectar-se ao JEMSmm!\nTentativas restantes: {tentativas}", Color.Orange);
                        await Task.Delay(2000);
                    }
                }

                if (!conectado)
                {
                    AtualizarStatus("Sem conexão com o JEMSmm!", Color.Red);
                    await Task.Delay(3000);
                    AtualizarStatus("Aguardando conexão com o JEMSmm...", Color.Yellow);
                }
            }
        }

        private void AtualizarStatus(string texto, Color cor)
        {
            labelAPIStatus.Text = texto;
            labelAPIStatus.ForeColor = cor;
            labelAPIStatus.Font = new Font(labelAPIStatus.Font.FontFamily, 10, FontStyle.Bold);
            labelAPIStatus.Refresh();
        }



        private void pictureBoxStop_Click(object sender, EventArgs e)
        {
            using (var senhaForm = new Form())
            {
                senhaForm.Text = "Autenticação necessária";
                senhaForm.Width = 300;
                senhaForm.Height = 150;
                senhaForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                senhaForm.StartPosition = FormStartPosition.CenterParent;
                senhaForm.MinimizeBox = false;
                senhaForm.MaximizeBox = false;

                var label = new Label() { Left = 10, Top = 20, Text = "Digite a senha:", AutoSize = true };
                var textBox = new TextBox() { Left = 10, Top = 50, Width = 260, UseSystemPasswordChar = true };
                var buttonOk = new Button() { Text = "OK", Left = 115, Width = 75, Top = 80, DialogResult = DialogResult.OK };

                senhaForm.Controls.Add(label);
                senhaForm.Controls.Add(textBox);
                senhaForm.Controls.Add(buttonOk);

                senhaForm.AcceptButton = buttonOk;

                if (senhaForm.ShowDialog() == DialogResult.OK)
                {
                    if (textBox.Text == senhaCorreta)
                    {
                        labelAPIStatus.Text = "Aguardando conexão com o JEMSmm...";
                        labelAPIStatus.ForeColor = Color.Yellow;
                        labelAPIStatus.Refresh();
                        pictureBoxPlay.BringToFront();
                        pictureBoxStop.SendToBack();
                        LogMonitor.Stop();
                        apiHealthTimer.Stop();
                        AplicacaoRodando = false;
                    }
                    else
                    {
                        MessageBox.Show("Senha incorreta. Operação cancelada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void pictureBoxConfig_Click(object sender, EventArgs e)
        {
            if (!AplicacaoRodando)
            {
                using (FormSetup setup = new FormSetup())
                {
                    setup.StartPosition = FormStartPosition.CenterParent;
                    setup.MainForm = this; // <-- Aqui você passa a referência do Form1



                    if (setup.ShowDialog(this) == DialogResult.OK)
                    {
                        // Código para pegar os dados do setup
                    }
                }
            }
            else
            {
                MessageBox.Show("Pare a aplicação para acessar as configurações!");
            }
        }


        // Função para exibir erro e iniciar o timer
        private void MostrarErroAPI()
        {
            labelAPIStatus.Text = "Erro ao conectar-se ao JEMSmm!";
            labelAPIStatus.ForeColor = Color.Red;

            timerAPIError.Interval = 5000; // 3 segundos
            timerAPIError.Start(); // Inicia o timer aqui
        }

        // Evento do timer que limpa o status
        private void timerAPIError_Tick(object sender, EventArgs e)
        {
            timerAPIError.Stop(); // Para o timer após os 3 segundos
            labelAPIStatus.Text = "Aguardando conexão com o JEMSmm...";
            labelAPIStatus.ForeColor = Color.Yellow;
            // Opcional: limpa o status
            //labelAPIStatus.Text = string.Empty;
            pictureBoxStop.SendToBack();
            // Cancela futuras tentativas até clicar no botão novamente
            tentarNovamente = false;
        }




        private bool CarregarConfiguracaoEquipamentos()
        {
            string caminhoIni = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");

            if (!File.Exists(caminhoIni))
            {
                MessageBox.Show("Arquivo de configuração dos testadores não encontrado. Configure os testadores primeiro.");
                pictureBoxStop.SendToBack();
                pictureBoxPlay.BringToFront();
                return false;
            }

            IniFile ini = new IniFile(caminhoIni);

            string equipmentPrefix = ini.Read("Prefix", "Station Prefix");
            string line = ini.Read("Line", "Station Prefix");

            if (string.IsNullOrWhiteSpace(equipmentPrefix))
            {
                MessageBox.Show("Prefixo da estação não encontrado no arquivo de configuração.");
                return false;
            }

            // Salva o prefixo numa variável de classe
            equipmentPrefixGlobal = equipmentPrefix;

            if (line == "DIAGNOSTIC")
            {
                datalakeOn = false;
                pictureBoxDLOFF.BringToFront();
                pictureBoxDLON.SendToBack();
            }
            else
            {
                datalakeOn = true;
                pictureBoxDLOFF.SendToBack();
                pictureBoxDLON.BringToFront();
            }

            string[] partes = Regex.Split(equipmentPrefix, "[-_]");



            string PC = partes.Length >= 3 ? partes[2] : "";

            if (partes.Length < 2)
            {
                MessageBox.Show("Formato do prefixo inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // labelLine recebe a primeira parte, ex: SMART01
            string linha = partes[0];
            
            labelLine.Text = line;

            // labelStation recebe a segunda parte, ex: DL, FT ou BT
            string etapa = partes[1];
            switch (etapa)
            {
                case "DL":
                    labelStation.Text = "DOWNLOAD";
                    break;
                case "FT":
                    labelStation.Text = "FUNCTIONAL TEST";
                    break;
                case "BT":
                    labelStation.Text = "BT";
                    break;
                default:

                    labelStation.Text = etapa; // mantém como está caso não esteja listado
                    break;
            }

            labelStation.Text += $" - {PC}";

            return true;

        }

        private void labelLine_Click(object sender, EventArgs e)
        {

        }

        public void AdicionarLogNaDataGridView(string log)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AdicionarLogNaDataGridView), log);
                return;
            }

            var partes = log.Split('|').Select(p => p.Trim()).ToArray();
            if (partes.Length < 7)
                return;

            string timestamp = partes[0];
            string serial = partes[1];
            string result = partes[2];
            string failure = partes[3];
            string equipment = partes[4];
            string sentToJems = partes[5];
            string model = partes[6];

            Form1 form1 = (Form1)Application.OpenForms["Form1"];
            if (form1 != null)
            {
                form1.labelModel.Text = model;
            }

            // Inserir no topo
            dataGridViewHistory.Rows.Insert(0, timestamp, serial, result, failure, equipment, model, sentToJems);
            DataGridViewRow row = dataGridViewHistory.Rows[0];

            // PASS = linha verde (exceto ColumnSentToJems)
            if (result.ToUpper() == "PASS")
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.OwningColumn.Name != "ColumnSentToJems")
                        cell.Style.ForeColor = Color.Green;
                }
            }
            else if (result.ToUpper() == "FAIL")
            {
                row.DefaultCellStyle.ForeColor = Color.Red;
            }

            // SentToJems colorir individual
            var cellSentToJems = row.Cells["ColumnSentToJems"];
            if (sentToJems.Equals("NO", StringComparison.OrdinalIgnoreCase))
            {
                cellSentToJems.Style.ForeColor = Color.Red;
            }
            else if (sentToJems.Equals("YES", StringComparison.OrdinalIgnoreCase))
            {
                cellSentToJems.Style.ForeColor = Color.Green;
            }

            // Erro PV
            if (partes.Any(p => string.Equals(p, "NO", StringComparison.OrdinalIgnoreCase)))
            {
                var pvErrorForm = new PVError(serial);
                pvErrorForm.TopMost = true;
                pvErrorForm.Show();
                pvErrorForm.BringToFront();
                pvErrorForm.Focus();

                form1?.AdicionarNaListViewPVError(serial);

                row.DefaultCellStyle.ForeColor = Color.Red;

                var timer = new System.Windows.Forms.Timer();
                timer.Interval = 5000;
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    pvErrorForm.Close();
                    timer.Dispose();
                };
                timer.Start();
            }

            // Scroll para o topo (opcional, já que insere no topo)
            dataGridViewHistory.FirstDisplayedScrollingRowIndex = 0;
        }


        private async void ApiHealthTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                var response = await httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    labelAPIStatus.Text = "Conectado ao JEMSmm";
                    labelAPIStatus.ForeColor = Color.Green;
                    labelDLStatus.Text = "CONNECTION: OK";
                    labelDLStatus.ForeColor = Color.Green;
                    // Se o form de erro estiver aberto, fecha
                    if (apiErrorForm != null && !apiErrorForm.IsDisposed)
                    {
                        apiErrorForm.Invoke(new Action(() => apiErrorForm.Close()));
                        apiErrorForm = null;
                    }
                }
                else
                {
                    HandleApiConnectionError();
                }
            }
            catch
            {
                HandleApiConnectionError();
            }
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close(); // Isso vai acionar o MainForm_FormClosing

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string canCloseConfig = ConfigurationManager.AppSettings["CanClose"];

            if (string.Equals(canCloseConfig, "true", StringComparison.OrdinalIgnoreCase))
            {
                // Pode fechar normalmente
                return;
            }

            // Solicita senha para fechar
            using (var senhaForm = new Form())
            {
                senhaForm.Text = "Autenticação necessária";
                senhaForm.Width = 300;
                senhaForm.Height = 150;
                senhaForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                senhaForm.StartPosition = FormStartPosition.CenterParent;
                senhaForm.MinimizeBox = false;
                senhaForm.MaximizeBox = false;
                senhaForm.TopMost = true;

                var label = new Label() { Left = 10, Top = 20, Text = "Digite a senha:", AutoSize = true };
                var textBox = new TextBox() { Left = 10, Top = 50, Width = 260, UseSystemPasswordChar = true };
                var buttonOk = new Button() { Text = "OK", Left = 115, Width = 75, Top = 80, DialogResult = DialogResult.OK };

                senhaForm.Controls.Add(label);
                senhaForm.Controls.Add(textBox);
                senhaForm.Controls.Add(buttonOk);
                senhaForm.AcceptButton = buttonOk;

                if (senhaForm.ShowDialog() == DialogResult.OK)
                {
                    if (textBox.Text == senhaCorreta)
                    {
                        // Libera fechamento
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Senha incorreta. Encerramento cancelado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }
                else
                {
                    // Cancelado sem digitar senha
                    e.Cancel = true;
                }
            }
        }


        private void HandleApiConnectionError()
        {
            labelAPIStatus.Text = "Conexão perdida com o JEMSmm";
            labelAPIStatus.ForeColor = Color.Red;
            labelDLStatus.Text = "CONNECTION: NOK";
            labelDLStatus.ForeColor = Color.Red;

            if (apiErrorForm == null || apiErrorForm.IsDisposed)
            {
                apiErrorForm = new APIConnError();
                apiErrorForm.StartPosition = FormStartPosition.CenterScreen;
                apiErrorForm.TopMost = true;
                apiErrorForm.Show();
            }
        }

        public void AdicionarNaListViewPVError(string serialNumber)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AdicionarNaListViewPVError), serialNumber);
                return;
            }

            var item = new ListViewItem(serialNumber)
            {
                ForeColor = Color.Red
            };

            listViewPVError.Items.Insert(0, item);
        }

        private void listViewPVError_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PreStartWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (!AplicacaoRodando)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    NotRunningError form = new NotRunningError();
                    form.Show();
                });
            }
        }

        private void listViewPVError_ItemActivate(object sender, EventArgs e)
        {
            if (listViewPVError.SelectedItems.Count > 0)
            {
                string serialNumber = listViewPVError.SelectedItems[0].Text;

                History historyForm = new History(serialNumber);
                historyForm.Show();
            }
        }

        private void pictureBoxDLOFF_Click(object sender, EventArgs e)
        {
            if (VerificarConexaoComPasta() && !FNI)
            {
                pictureBoxDLON.BringToFront();
                pictureBoxDLOFF.SendToBack();
                // Se quiser fazer algo mais, coloca aqui
                datalakeOn = true;
            }
            else
            {
                if (FNI == true)
                {
                    MessageBox.Show("Tester performance não se aplica para estações QC!", "Posto não habilitado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Não foi possível ligar o Tester Performance. Verifique a conexão com a pasta DATALAKE!", "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }


        }

        private void pictureBoxDLON_Click(object sender, EventArgs e)
        {
            

            using (var senhaForm = new Form())
            {
                senhaForm.Text = "Autenticação necessária";
                senhaForm.Width = 300;
                senhaForm.Height = 150;
                senhaForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                senhaForm.StartPosition = FormStartPosition.CenterParent;
                senhaForm.MinimizeBox = false;
                senhaForm.MaximizeBox = false;

                var label = new Label() { Left = 10, Top = 20, Text = "Digite a senha:", AutoSize = true };
                var textBox = new TextBox() { Left = 10, Top = 50, Width = 260, UseSystemPasswordChar = true };
                var buttonOk = new Button() { Text = "OK", Left = 115, Width = 75, Top = 80, DialogResult = DialogResult.OK };

                senhaForm.Controls.Add(label);
                senhaForm.Controls.Add(textBox);
                senhaForm.Controls.Add(buttonOk);

                senhaForm.AcceptButton = buttonOk;

                if (senhaForm.ShowDialog() == DialogResult.OK)
                {
                    if (textBox.Text == senhaCorreta)
                    {
                        pictureBoxDLOFF.BringToFront();
                        pictureBoxDLON.SendToBack();
                        //labelAPIStatus.Text = "Aguardando conexão com o JEMSmm...";
                        //labelAPIStatus.ForeColor = Color.Yellow;
                        //labelAPIStatus.Refresh();
                        //pictureBoxPlay.BringToFront();
                        //pictureBoxStop.SendToBack();
                        //LogMonitor.Stop();
                        //apiHealthTimer.Stop();
                        datalakeOn = false;
                    }
                    else
                    {
                        MessageBox.Show("Senha incorreta. Operação cancelada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private bool VerificarConexaoComPasta()
        {
            try
            {
                string path = datalakePath;
                return Directory.Exists(path);
            }
            catch
            {
                return false;
            }
        }

        private void PastaCheckTimer_Tick(object sender, EventArgs e)
        {
            if (VerificarConexaoComPasta())
            {
                labelDLStatus.Text = "CONNECTION: OK";
                labelDLStatus.ForeColor = Color.Green;

            }
            else
            {
                labelDLStatus.Text = "CONNECTION: NOK";
                labelDLStatus.ForeColor = Color.Red;
                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
