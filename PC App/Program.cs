using System;
using System.Threading;
using System.Threading.Tasks;
using Services;

namespace PC_App
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            DateTime timeGet;
            DateTime time;

            DateTime ping;
            double pingDiff;
            
            while (true)
            {
                ping = DateTime.UtcNow;
                timeGet = GetServices.GetTime("https://pchealth.azurewebsites.net/api/Base/GetTime");
                pingDiff = (DateTime.UtcNow - ping).TotalMilliseconds;
                // timeGet = GetServices.GetTime("https://localhost:5001/api/Base/GetTime");
                
                time = DateTime.UtcNow;
                
                // Console.WriteLine((timeGet - time).TotalMilliseconds);
                
                if ((timeGet - time).TotalMilliseconds <= pingDiff)
                {
                    // Console.WriteLine("OK");
                    // Thread.Sleep(500);
                    // PostServices.PostDiagnosticData("https://localhost:5001/api/Base/GetDiagnosticDataFromPc");
                    PostServices.PostDiagnosticData("https://pchealth.azurewebsites.net/api/Base/GetDiagnosticDataFromPc");
                }
            }
        }
    }
}
