using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_App.Linux_Version
{
    public static class Helper
    {
        public static string Bash(string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            string result = "";
            process.Start();
            //ReadLine();
            while (!process.StandardOutput.EndOfStream)
            {
                result += process.StandardOutput.ReadLine() + "\n";
            }
            process.WaitForExit();
            return result;
        }
    }
}
