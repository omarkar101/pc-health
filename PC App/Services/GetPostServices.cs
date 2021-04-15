using System;
using System.Threading;
using System.Threading.Tasks;
using Services;

namespace PC_App.Services
{
    public static class GetPostServices
    {
        public static async Task PostDiagnosticGetTime()
        {
            while (true)
            {
                try
                {
                    await PostServices.PostDiagnosticData("http://pc-health.somee.com/Pc/PostDiagnosticDataFromPc");
                    Thread.Sleep(500);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}