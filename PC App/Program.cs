using System.Threading;
using System.Threading.Tasks;
using Services;

namespace PC_App
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            while (true)
            {
                PostServices.PostDiagnosticData("https://pchealth.azurewebsites.net/api/Base/GetDiagnosticDataFromPc");
                Thread.Sleep(10000);
            }

        }
    }
}
