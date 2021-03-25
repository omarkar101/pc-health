using System;
using System.Threading.Tasks;
using Services;

namespace PC_App
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            GetPostServices.PostDiagnosticGetTime();
        }
    }
}
