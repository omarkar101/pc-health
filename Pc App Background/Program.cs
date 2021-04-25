using System;
using System.Diagnostics;

namespace Pc_App_Background
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using Process myProcess = new();
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = @"PC App.exe";
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
