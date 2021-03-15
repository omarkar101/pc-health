using System;
using System.Threading;

namespace Services
{
    public static class GetPostServices
    {
        public static void PostDiagnosticGetTime()
        {
            DateTime timeGet;
            DateTime time;

            DateTime ping;
            double pingDiff;
            
            while (true)
            {
                try
                {
                    Thread.Sleep(100);
                    ping = DateTime.UtcNow;
                    timeGet = GetServices.GetTime("https://pchealth.azurewebsites.net/api/Base/GetTime");
                    pingDiff = (DateTime.UtcNow - ping).TotalMilliseconds;
                
                    time = DateTime.UtcNow;
                
                    if ((timeGet - time).TotalMilliseconds <= pingDiff)
                    {
                        PostServices.PostDiagnosticData("https://pchealth.azurewebsites.net/api/Base/PostDiagnosticDataFromPc");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}