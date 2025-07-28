using Samsung_Smart_Parser;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


public class ApiService
{
    private readonly HttpClient _httpClient;

    //ENDPOINTS

    private readonly string _endpointSendTestMES = ConfigurationManager.AppSettings["EPSENDTESTMES"];
    private readonly string _endpointFullPerform = ConfigurationManager.AppSettings["EPFULLPERFORM"];
    private readonly string _endpointAddDefect = ConfigurationManager.AppSettings["EPADDDEFECT"];

    public event Action<string> OnSendToJems;



    public ApiService()
    {
        _httpClient = new HttpClient();

    }

    public string SendSerialNumberFVTSync(string serialNumber, string equipamento, string result, string failure, string step)
    {
        string json;

        if (result == "FAIL")
        {
            json = $@"
        {{
            ""Serial"": ""{serialNumber}"",
            ""Customer"": ""Samsung"",
            ""Division"": ""Samsung"",
            ""Equipment"": ""{equipamento}"",
            ""Step"": ""{step}"",
            ""TestResult"": ""F"",
            ""FailureLabel"": ""{failure}""
        }}";
        }
        else
        {
            json = $@"
        {{
            ""Serial"": ""{serialNumber}"",
            ""Customer"": ""Samsung"",
            ""Division"": ""Samsung"",
            ""Equipment"": ""{equipamento}"",
            ""Step"": ""{step}"",
            ""TestResult"": ""P""
        }}";
        }

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using (var client = new HttpClient())
        {
            var response = client.PostAsync(_endpointSendTestMES, content).GetAwaiter().GetResult(); // síncrono
            string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (responseContent.IndexOf("Success", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "YES";
            }
            else
            {
                return "NO";
            }
        }
    }


    public string SendSerialNumberQCPASSSync(string serialNumber, string result, string step, string equipamento)
    {


        string json;

        json = $@"
        {{
            ""siteName"": ""Manaus"",
            ""customerName"": ""Samsung"",
            ""serialNumber"": ""{serialNumber}"",
            ""resourceName"": ""{equipamento}"",
            ""isSingleWipMode"": true
        }}";

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using (var client = new HttpClient())
        {
            var response = client.PostAsync(_endpointFullPerform, content).GetAwaiter().GetResult(); // síncrono
            string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (responseContent.IndexOf("Success", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "YES";
            }
            else
            {
                return "NO";
            }
        }
    }

    public string SendSerialNumberQCFAILSync(string serialNumber, string result, string step, string equipamento)
    {
       
        string json = $@"
        {{
            ""resourceName"": ""{equipamento}"",
            ""serialNumber"": ""{serialNumber}"",
            ""defects"": [
                {{
                    ""defectCRD"": ""CRD"",
                    ""defectQuantity"": 1,
                    ""defectName"": ""Display quebrado""
                }}
            ],
            ""hasValidNumericField"": true
        }}";


        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using (var client = new HttpClient())
        {
            var response = client.PostAsync(_endpointAddDefect, content).GetAwaiter().GetResult(); // síncrono
            string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (responseContent.IndexOf("Success", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "YES";
            }
            else
            {
                return "NO";
            }
        }
    }


}




