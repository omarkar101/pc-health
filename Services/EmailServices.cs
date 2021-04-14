using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class EmailServices
    {
        public static bool VerifyEmail(string email)
        {
            const string accessKey = "72363a2f50669fc6d48692f969b87c54";
            var url = "http://apilayer.net/api/check?access_key=" + accessKey + "&email=" + email;

            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = request.GetResponse();
            using var stream = response.GetResponseStream();
            var reader = new StreamReader(stream!);
            var jsonString = reader.ReadToEnd();
            var emailVerifier = JsonSerializer.Deserialize<EmailVerifier>(jsonString);
            return emailVerifier != null && emailVerifier.smtp_check;
        }
        public static async Task SendEmail(string email, string body)
        {
            try
            {
                var client = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                var request = new RestRequest(Method.POST); 
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("api-key", "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");
                request.AddParameter("application/json", "{\"sender\":{\"name\":\"PC-Health\",\"email\":\"team.mirai101@gmail.com\"},\"to\":[{\"email\":\""+ email +"\",\"name\":\"Rony\"}],\"subject\":\"PC-Health\",\"textContent\":\""+ body +"\"}", ParameterType.RequestBody);
                IRestResponse response = await client.ExecuteAsync(request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    internal class EmailVerifier
    {
        public bool smtp_check { get; set; }
    }
}
