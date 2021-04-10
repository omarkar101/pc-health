using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PC_App.Services
{
    public static class PostServices
    {
        public static async Task PostDiagnosticData(string url)
        {
            var diagnosticData = DiagnosticDataServices.GetDiagnosticData();
            
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; }; 
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            await using (var streamWriter = new StreamWriter(await httpWebRequest.GetRequestStreamAsync()))
            {
                await streamWriter.WriteAsync(diagnosticData);
            }
            var httpResponse = (HttpWebResponse)(httpWebRequest.GetResponse());
        }
    }
}