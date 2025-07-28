using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Samsung_Smart_Parser
{
    public partial class History : Form
    {
        private string _serial;

        public History(string serial)
        {
            InitializeComponent();
            _serial = serial;
            labelSNHist.Text = $"NÚMERO DE SÉRIE: {_serial}";
            _ = CarregarHistoricoAsync(serial);
        }

        private async Task CarregarHistoricoAsync(string serial)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"http://10.56.17.58/Mes4Api/Test/GetOperationsHistories?siteName=MANAUS&serialNumber={serial}";
                    var response = await client.GetStringAsync(url);

                    if (response.IndexOf("An error has occurred", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        MessageBox.Show("Serial não existe no JEMS");
                        return;
                    }

                    var dados = JsonConvert.DeserializeObject<RootObject>(response);
                    listViewHist.Items.Clear();

                    var historico = dados?.Wips?.FirstOrDefault()?.OperationHistories;

                    if (historico != null)
                    {
                        foreach (var item in historico)
                        {
                            var hora = DateTime.TryParse(item.EndDateTime, out var dt)
                                ? dt.ToString("HH:mm:ss")
                                : "N/A";

                            var listItem = new ListViewItem(hora);
                            listItem.SubItems.Add(item.RouteStepName ?? "N/A");
                            listItem.SubItems.Add(item.OperationStatus ?? "N/A");

                            // Cor baseada no resultado
                            if (string.Equals(item.OperationStatus, "PASS", StringComparison.OrdinalIgnoreCase))
                            {
                                listItem.ForeColor = Color.Green;
                            }
                            else if (string.Equals(item.OperationStatus, "FAIL", StringComparison.OrdinalIgnoreCase))
                            {
                                listItem.ForeColor = Color.Red;
                            }
                            else
                            {
                                listItem.ForeColor = Color.Black;
                            }

                            listViewHist.Items.Add(listItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Serial não existe no MES!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // JSON Models
        public class OperationHistory
        {
            public string RouteStepName { get; set; }
            public string EndDateTime { get; set; }
            public string OperationStatus { get; set; }
        }

        public class Wip
        {
            public List<OperationHistory> OperationHistories { get; set; }
        }

        public class RootObject
        {
            public List<Wip> Wips { get; set; }
        }

        private void History_Load(object sender, EventArgs e)
        {

        }

        private void labelSNHist_Click(object sender, EventArgs e)
        {

        }

        private void buttonExitSN_Click(object sender, EventArgs e)
        {
            this.Close(); // Fecha o form atual (History)
        }
    }
}
