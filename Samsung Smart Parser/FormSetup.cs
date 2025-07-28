using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Samsung_Smart_Parser
{
    public partial class FormSetup : Form
    {
        
        string qtyPCs = ConfigurationManager.AppSettings["QTYPCS"];

        private bool hasIndividualPC = false;
        public string LinhaSelecionada => comboBoxLinhas.SelectedItem?.ToString() ?? "";
        public string PCSelecionado => comboBoxPC.SelectedItem?.ToString() ?? "";

        public string StepSelecionado => comboBoxStep.SelectedItem?.ToString() ?? "";

        public string ProdutoSelecionado => comboBoxProduto.SelectedItem?.ToString() ?? "";



        private Form1 _formPrincipal;

        public Form1 MainForm { get; set; }

        public FormSetup()
        {
            InitializeComponent();
        }

        private void FormSetup_Load(object sender, EventArgs e)
        {
            comboBoxProduto.Items.Add("NOTEBOOK");
            comboBoxProduto.Items.Add("SMARTPHONE");
            comboBoxProduto.SelectedIndexChanged += ComboBoxProduto_SelectedIndexChanged;

            // Define por padrão
            comboBoxProduto.SelectedIndex = 0;

            // PC
            if (int.TryParse(qtyPCs, out int totalPCs))
            {
                for (int j = 1; j <= totalPCs; j++)
                {
                    comboBoxPC.Items.Add($"{j:00}");
                }
                comboBoxPC.SelectedIndex = 0;
            }

            // Chama manualmente a função para preencher as linhas e steps conforme a seleção padrão
            ComboBoxProduto_SelectedIndexChanged(null, null);
        }

        public FormSetup(Form1 formPrincipal)
        {
            InitializeComponent();
            _formPrincipal = formPrincipal;
        }

        private void SalvarConfiguracaoIni()
        {
            string linha = LinhaSelecionada; // Ex: "SAMSUNG SMARTPHONE 01"
            string PC = PCSelecionado; // Ex: "01"
            string step = StepSelecionado; // ex dl

            if (step == "FINAL INSPECTION")
            {
                step = "FNI";
            }

            if (string.IsNullOrWhiteSpace(step) || string.IsNullOrWhiteSpace(linha) || (string.IsNullOrWhiteSpace(PC) && (step != "FT" && step != "BT" && step != "FNI")))
            {
                MessageBox.Show("Preencha todos os campos antes de salvar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Extrai o número da linha (ex: "01" de "SAMSUNG NOTEBOOK 01")
            string linhaNumero = new string(linha.Where(char.IsDigit).ToArray()).PadLeft(2, '0');

            hasIndividualPC = (step == "FT" || step == "BT" || step == "DL" || step == "FCT");

            string prefixo;

            if (linha.ToUpper().Contains("NOTE"))
            {
                // Se for NOTE, o prefixo é só linha-step
                prefixo = $"NOTE{linhaNumero}-{step}-";
            }
            else if (linha.ToUpper().Contains("RIO"))
            {
                // Se for DIAGNOSTIC, o prefixo é só "DIAG-"
                prefixo = $"INLINE_{step}_{PC}-";
            }
            else if (linha.ToUpper().Contains("DIAG"))
            {
                // Se for DIAGNOSTIC, o prefixo é só "DIAG-"
                prefixo = $"DIAG-{step}-";
            }
            else
            {
                // Se for SMART, segue lógica existente
                if (!hasIndividualPC)
                {
                    // Se for FT ou BT, ignora PC no prefixo
                    prefixo = $"SMART{linhaNumero}-{step}-";
                }
                else
                {
                    // Caso padrão, inclui PC
                    if(step == "DL")
                        prefixo = $"SMART{linhaNumero}-{step}-{PC}-";
                    else
                        prefixo = $"SMART{linhaNumero}-{step}-{PC}";
                }
            }

            // Cria conteúdo do INI
            string path = Path.Combine(Application.StartupPath, "config.ini");
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("[Station Prefix]");
            sb.AppendLine($"Prefix={prefixo}");
            sb.AppendLine($"Line={linha}");
            sb.AppendLine();

            File.WriteAllText(path, sb.ToString());

            MessageBox.Show("Configuração de estação salva com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBoxLinhas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxBancadas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonSalvar1_Click(object sender, EventArgs e)
        {
            SalvarConfiguracaoIni();

            // Verifique se a referência foi atribuída
            

            this.Close();

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            string caminhoIni = Path.Combine(Application.StartupPath, "config.ini");

            if (File.Exists(caminhoIni))
            {
                try
                {
                    File.Delete(caminhoIni);
                    MessageBox.Show("Arquivo de configuração excluído com sucesso!", "Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao apagar o arquivo de configuração: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Arquivo de configuração não encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonReset_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBoxStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStep = comboBoxStep.SelectedItem?.ToString();

            if (selectedStep == "FT" || selectedStep == "BT")
            {
                comboBoxPC.BackColor = Color.Gray;
                comboBoxPC.Enabled = false;   // Desabilita o combobox PC
                comboBoxPC.SelectedIndex = -1; // Deseleciona o PC
                
            }
            else
            {
                comboBoxPC.BackColor = Color.White;
                comboBoxPC.Enabled = true;    // Habilita o combobox PC
                if (comboBoxPC.SelectedIndex == -1 && comboBoxPC.Items.Count > 0)
                {
                    comboBoxPC.SelectedIndex = 0; // Seleciona o primeiro item automaticamente, se nada estiver selecionado
                }
            }
        }

        private void AtualizarLinhas()
        {
            comboBoxLinhas.Items.Clear();

            if (comboBoxProduto.SelectedItem.ToString() == "SMARTPHONE")
            {
                for (int i = 1; i <= 1; i++)
                {
                    comboBoxLinhas.Items.Add($"SAMSUNG SMARTPHONE {i:00}");
                }
            }
            else if (comboBoxProduto.SelectedItem.ToString() == "NOTEBOOK")
            {
                for (int i = 1; i <= 1; i++)
                {
                    comboBoxLinhas.Items.Add($"SAMSUNG NOTEBOOK {i:00}");
                }
            }

            comboBoxLinhas.Items.Add("DIAGNOSTIC");

            if (comboBoxLinhas.Items.Count > 0)
                comboBoxLinhas.SelectedIndex = 0;
        }

        private void ComboBoxProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxLinhas.Items.Clear();
            comboBoxStep.Items.Clear();

            string produtoSelecionado = comboBoxProduto.SelectedItem.ToString();

            if (produtoSelecionado == "SMARTPHONE")
            {
                for (int i = 1; i <= 1; i++)
                {
                    comboBoxLinhas.Items.Add($"SAMSUNG SMARTPHONE {i:00}");
                }

                comboBoxStep.Items.Add("DL");
                comboBoxStep.Items.Add("FT");
                
                comboBoxStep.Items.Add("BT");
                comboBoxStep.Items.Add("FINAL INSPECTION");
            }
            else if (produtoSelecionado == "NOTEBOOK")
            {
                for (int i = 1; i <= 1; i++)
                {
                    comboBoxLinhas.Items.Add($"SAMSUNG NOTEBOOK {i:00}");
                }
                comboBoxStep.Items.Add("FCT");

                comboBoxLinhas.Items.Add("RIO JUTAI");
            }

            // Adiciona sempre a opção DIAGNOSTIC
            

            

            comboBoxLinhas.SelectedIndex = 0;
            comboBoxStep.SelectedIndex = 0;
        }


    }
}
