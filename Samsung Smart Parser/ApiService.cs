using System;
using System.Configuration;
using System.Net.Http;
using System.Text;

namespace Samsung_Smart_Parser
{
    /// <summary>
    /// Small, synchronous wrapper for calls to MES endpoints.
    /// Kept synchronous to preserve original behavior; can be converted to async easily.
    /// </summary>
    public class ApiService
    {
        private readonly string _endpointSendTestMES = ConfigurationManager.AppSettings["EPSENDTESTMES"];
        private readonly string _endpointFullPerform = ConfigurationManager.AppSettings["EPFULLPERFORM"];
        private readonly string _endpointAddDefect = ConfigurationManager.AppSettings["EPADDDEFECT"];

        // Simple event if UI wants to listen to outgoing messages (kept for parity)
        public event Action<string> OnSendToJems;

        public ApiService() { }

        /// <summary>
        /// Send functional test (FVT) result to MES.
        /// Returns "YES" on success (response content contains 'Success'), "NO" otherwise.
        /// </summary>
        public string SendSerialNumberFVTSync(string serialNumber, string equipment, string result, string failure, string step)
        {
            var isFail = string.Equals(result, "FAIL", StringComparison.OrdinalIgnoreCase);
            var testResultChar = isFail ? "F" : "P";

            var json = isFail
                ? $@"{{""Serial"": ""{serialNumber}"","" +
                  $@""Customer"": ""Samsung"","" +
                  $@""Division"": ""Samsung"","" +
                  $@""Equipment"": ""{equipment}"","" +
                  $@""Step"": ""{step}"","" +
                  $@""TestResult"": ""{testResultChar}"","" +
                  $@""FailureLabel"": ""{failure}""}}"
                : $@"{{""Serial"": ""{serialNumber}"","" +
                  $@""Customer"": ""Samsung"","" +
                  $@""Division"": ""Samsung"","" +
                  $@""Equipment"": ""{equipment}"","" +
                  $@""Step"": ""{step}"","" +
                  $@""TestResult"": ""{testResultChar}""}}";

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                try
                {
                    var response = client.PostAsync(_endpointSendTestMES, content).GetAwaiter().GetResult();
                    var body = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    var ok = body.IndexOf("Success", StringComparison.OrdinalIgnoreCase) >= 0;
                    OnSendToJems?.Invoke(ok ? "YES" : "NO");
                    return ok ? "YES" : "NO";
                }
                catch (Exception ex)
                {
                    OnSendToJems?.Invoke("NO: " + ex.Message);
                    return "NO";
                }
            }
        }

        /// <summary>
        /// Sends a QC PASS using the FullPerform endpoint.
        /// Kept synchronous to keep parity with original code.
        /// </summary>
        public string SendSerialNumberQCPASSSync(string serialNumber, string result, string step, string equipment)
        {
            var json = $@"{{"" +
                       $@""siteName"": ""Manaus"","" +
                       $@""customerName"": ""Samsung"","" +
                       $@""serialNumber"": ""{serialNumber}"","" +
                       $@""resourceName"": ""{equipment}"","" +
                       $@""isSingleWipMode"": true" +
                       $@"}}";

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                try
                {
                    var response = client.PostAsync(_endpointFullPerform, content).GetAwaiter().GetResult();
                    var body = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return body.IndexOf("Success", StringComparison.OrdinalIgnoreCase) >= 0 ? "YES" : "NO";
                }
                catch
                {
                    return "NO";
                }
            }
        }

        /// <summary>
        /// Sends a QC FAIL as a defect (AddDefect endpoint).
        /// Payload here is a template — you should adapt defect fields to your real data.
        /// </summary>
        public string SendSerialNumberQCFAILSync(string serialNumber, string defectCRD, string defectName, string equipment)
        {
            var json = $@"{{"" +
                       $@""resourceName"": ""{equipment}"","" +
                       $@""serialNumber"": ""{serialNumber}"","" +
                       $@""defects"": [{{"" +
                       $@""defectCRD"": ""{defectCRD}"","" +
                       $@""defectQuantity"": 1,"" +
                       $@""defectName"": ""{defectName}""}}],"" +
                       $@""hasValidNumericField"": true" +
                       $@"}}";

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                try
                {
                    var response = client.PostAsync(_endpointAddDefect, content).GetAwaiter().GetResult();
                    var body = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return body.IndexOf("Success", StringComparison.OrdinalIgnoreCase) >= 0 ? "YES" : "NO";
                }
                catch
                {
                    return "NO";
                }
            }
        }
    }
}
