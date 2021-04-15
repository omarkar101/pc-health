using System;
using System.Threading;
using System.Threading.Tasks;
using PC_App.Services;

namespace PC_App
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            await PostServices.PostDiagnosticData();
        }
    }
}