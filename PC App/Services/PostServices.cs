using System;
using System.IO;
using System.Net;

namespace Services
{
    public static class PostServices
    {
        public static void PostDiagnosticData(string url)
        {
            string diagnosticData = DiagnosticDataServices.GetDiagnosticData();
            
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; }; 
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(diagnosticData);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        }
    }
}