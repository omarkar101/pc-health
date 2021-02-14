using System.Diagnostics;
namespace Disk_Linux
{
    /// <summary>
    /// A class responsible for disk information of PCs using Linux OS.
    /// </summary>
    public static class DiskInfo
    {
        /// <summary>
        /// This function gets the disk's space information on Linux OS using the Bash function.
        /// </summary>
        /// <returns>Returns a string containing all disk space information</returns>
        public static string GetDiskSpace()
        {
            return string.Join(" ", "df / -h").Bash();
        }
        /// <summary>
        /// Private function that takes as an input  a command passed to the terminal.
        ///</summary>
        /// <param name="cmd"> The command passed to the terminal</param>
        /// <returns>Returns a string containing the output of the passed command.</returns>
        private static string Bash(this string cmd)
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
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
    }
}