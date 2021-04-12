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
                var message = new MailMessage();
                var smtp = new SmtpClient();
                message.From = new MailAddress("team.mirai101@gmail.com");
                message.To.Add(new MailAddress(email));
                message.Subject = "Pc Health";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("team.mirai101@gmail.com", "TeaMirai101");
                await smtp.SendMailAsync(message);
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
