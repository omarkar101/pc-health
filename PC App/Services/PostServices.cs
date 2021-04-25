using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CommonModels;

namespace PC_App.Services
{
    public static class PostServices
    {
        public static async Task PostDiagnosticData()
        {
            while (true)
            {
                try
                {
                    await PostDiagnosticData("https://pchealth.azurewebsites.net/Pc/PostDiagnosticDataFromPc");
                    Thread.Sleep(500);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        public static async Task PostDiagnosticData(string url)
        {
            var diagnosticData = await DiagnosticDataServices.GetDiagnosticData();

            await Post(url, diagnosticData);
        }

        public static async Task PostPcHealthData(string url, PcHealthData pcHealthData)
        {
            var pcHealthDataJsonString = JsonSerializer.Serialize(pcHealthData);

            await Post(url, pcHealthDataJsonString);
        }

        private static async Task Post(string url, string msg)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            await using (var streamWriter = new StreamWriter(await httpWebRequest.GetRequestStreamAsync()))
            {
                await streamWriter.WriteAsync(msg);
            }
            var httpResponse = (HttpWebResponse)(httpWebRequest.GetResponse());
        }
    }
}