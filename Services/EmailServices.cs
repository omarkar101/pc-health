using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
    }

    internal class EmailVerifier
    {
        public bool smtp_check { get; set; }
    }
}
