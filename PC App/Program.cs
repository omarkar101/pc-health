using System;
using System.Threading.Tasks;
using PC_App.Services;
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
