using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using PC_App.Services;
using System.Windows;

namespace PC_App
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                await PostServices.PostDiagnosticData();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Press Enter to Exit");
                Console.ReadLine();
            }
        }
    }
}