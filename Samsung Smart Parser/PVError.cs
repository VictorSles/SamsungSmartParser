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
using Newtonsoft.Json;

namespace Samsung_Smart_Parser
{
    public partial class PVError : Form
    {
        private readonly string _serialNumber;

        public PVError(string serialNumber)
        {
            InitializeComponent();
            _serialNumber = serialNumber;
            labelSerialNumberPV.Text = serialNumber;
        }

        private async void PVError_Load(object sender, EventArgs e)
        {
            await BuscarUltimoStepAsync(_serialNumber);
        }

        private async Task BuscarUltimoStepAsync(string serial)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"http://10.56.17.58/Mes4Api/Test/GetOperationsHistories?siteName=MANAUS&serialNumber={serial}";
                    var response = await client.GetStringAsync(url);

                    var dados = JsonConvert.DeserializeObject<RootObject>(response);

                    var ultimoStep = dados?.Wips?.FirstOrDefault()?
                                         .OperationHistories?.FirstOrDefault()?
                                         .RouteStepName;

                    if (!string.IsNullOrEmpty(ultimoStep))
                    {
                        labelLastStep.Text = $"ÚLTIMO STEP: {ultimoStep}";
                    }
                    else
                    {
                        labelLastStep.Text = "ÚLTIMO STEP: N/A";
                    }
                }
            }
            catch (Exception ex)
            {
                labelLastStep.Text = $"ERRO: SERIAL NÃO EXISTE NO JEMS!";
            }
        }
        public class OperationHistory
        {
            public string RouteStepName { get; set; }
        }

        public class Wip
        {
            public List<OperationHistory> OperationHistories { get; set; }
        }

        public class RootObject
        {
            public List<Wip> Wips { get; set; }
        }

    }
}
