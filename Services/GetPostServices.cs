using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class GetPostServices
    {
        public static async Task<string> Post(string url, string msg, string tokenHeader = null)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            if (tokenHeader != null)
            {
                httpWebRequest.Headers.Add("Authorization", $"Bearer {tokenHeader}");
            }
            await using (var streamWriter = new StreamWriter(await httpWebRequest.GetRequestStreamAsync()))
            {
                await streamWriter.WriteAsync(msg);
            }
            var httpResponse = ((HttpWebResponse)(httpWebRequest.GetResponse())).GetResponseStream();
            var readStream = new StreamReader (httpResponse, Encoding.UTF8);
            return await readStream.ReadToEndAsync();
        }

        public static async Task<string> Get(string url, string tokenHeader = null)
        {
            var request = WebRequest.Create(url);
            if (tokenHeader != null)
            {
                request.Headers.Add("Authorization", $"Bearer {tokenHeader}");
            }
            request.Method = "GET";
            var webResponse = await request.GetResponseAsync();
            var webStream = webResponse.GetResponseStream();
            var reader = new StreamReader(webStream);
            return await reader.ReadToEndAsync();
        }
        
    }
}