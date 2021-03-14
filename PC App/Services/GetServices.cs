using System;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Services
{
    public static class GetServices
    {
        public static DateTime GetTime(string url)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            var request = WebRequest.Create(url);
            request.Method = "GET";
            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);
            var data = reader.ReadToEnd();
            return JsonSerializer.Deserialize<DateTime>(data);
        }
    }
}