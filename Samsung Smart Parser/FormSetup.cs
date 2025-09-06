using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Samsung_Smart_Parser
{
    /// <summary>
    /// Simplified FormSetup: builds station prefix and writes config.ini
    /// The UI elements (comboBox*, buttons) are assumed to exist in the designer.
    /// </summary>
    public partial class FormSetup : Form
    {
        private string qtyPCs = ConfigurationManager.AppSettings["QTYPCS"];
        public string LinhaSelecionada => comboBoxLinhas.SelectedItem?.ToString() ?? "";
        public string PCSelecionado => comboBoxPC.SelectedItem?.ToString() ?? "";
        public string StepSelecionado => comboBoxStep.SelectedItem?.ToString() ?? "";
        public string ProdutoSelecionado => comboBoxProduto.SelectedItem?.ToString() ?? "";

        public FormSetup()
        {
            InitializeComponent();
        }

        private void FormSetup_Load(object sender, EventArgs e)
        {
            comboBoxProduto.Items.Add("NOTEBOOK");
            comboBoxProduto.Items.Add("SMARTPHONE");
            comboBoxProduto.SelectedIndexChanged += ComboBoxProduto_SelectedIndexChanged;
            comboBoxProduto.SelectedIndex = 0;

            if (int.TryParse(qtyPCs, out int totalPCs))
            {
                for (int j = 1; j <= totalPCs; j++)
                    comboBoxPC.Items.Add($"{j:00}");
                comboBoxPC.SelectedIndex = 0;
            }

            ComboBoxProduto_SelectedIndexChanged(null, null);
        }

        private void buttonSalvar1_Click(object sender, EventArgs e)
        {
            SalvarConfiguracaoIni();
            this.Close();
        }

        private void SalvarConfiguracaoIni()
        {
            var linha = LinhaSelecionada;
            var pc = PCSelecionado;
            var step = StepSelecionado;
            if (step == "FINAL INSPECTION") step = "FNI";

            if (string.IsNullOrWhiteSpace(step) || string.IsNullOrWhiteSpace(linha) || (string.IsNullOrWhiteSpace(pc) && (step != "FT" && step != "BT" && step != "FNI")))
            {
                MessageBox.Show("Preencha todos os campos antes de salvar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Extract digits from line (e.g. "SAMSUNG SMARTPHONE 01" -> "01")
            var linhaNumero = new string(linha.Where(char.IsDigit).ToArray()).PadLeft(2, '0');
            bool hasIndividualPC = (step == "FT" || step == "BT" || step == "DL" || step == "FCT");

            string prefixo;
            if (linha.ToUpper().Contains("NOTE")) prefixo = $"NOTE{linhaNumero}-{step}-";
            else if (linha.ToUpper().Contains("DIAG")) prefixo = $"DIAG-{step}-";
            else
            {
                if (!hasIndividualPC) prefixo = $"SMART{linhaNumero}-{step}-";
                else prefixo = step == "DL" ? $"SMART{linhaNumero}-{step}-{pc}-" : $"SMART{linhaNumero}-{step}-{pc}";
            }

            var iniPath = Path.Combine(Application.StartupPath, "config.ini");
            var sb = new StringBuilder();
            sb.AppendLine("[Station Prefix]");
            sb.AppendLine($"Prefix={prefixo}");
            sb.AppendLine($"Line={linha}");
            File.WriteAllText(iniPath, sb.ToString());
            MessageBox.Show("Configuração de estação salva com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBoxProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxLinhas.Items.Clear();
            comboBoxStep.Items.Clear();
            var produto = comboBoxProduto.SelectedItem?.ToString();
            if (produto == "SMARTPHONE")
            {
                comboBoxLinhas.Items.Add("SAMSUNG SMARTPHONE 01");
                comboBoxStep.Items.Add("DL"); comboBoxStep.Items.Add("FT"); comboBoxStep.Items.Add("BT"); comboBoxStep.Items.Add("FINAL INSPECTION");
            }
            else if (produto == "NOTEBOOK")
            {
                comboBoxLinhas.Items.Add("SAMSUNG NOTEBOOK 01");
                comboBoxStep.Items.Add("FCT");
                comboBoxLinhas.Items.Add("RIO JUTAI");
            }
            comboBoxLinhas.Items.Add("DIAGNOSTIC");
            comboBoxLinhas.SelectedIndex = 0;
            comboBoxStep.SelectedIndex = 0;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            var ini = Path.Combine(Application.StartupPath, "config.ini");
            if (File.Exists(ini)) File.Delete(ini);
            MessageBox.Show("Arquivo de configuração excluído com sucesso!", "Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
