using System.Diagnostics;

namespace PC_App.Linux_Version.Diagnostic_Data.Disk_Linux
{
    
    /// <summary>
    /// A class responsible for disk information of PCs using Linux OS.
    /// </summary>
    public static class DiskInfo
    {
        /// <summary>
        /// Getter for the Disk Usage Percentage
        /// </summary>
        /// <value>Gets Disk Usage Percentage</value>
        public static float DiskUsagePercentage { get => updateDisk().UsedSizePercentage; }
        

        /// <summary>
        /// Getter for the main disk size
        /// </summary>
        /// <value>Gets main Disk Size</value>
        public static float DiskSize { get => updateDisk().FullSize; }

        /// <summary>
        /// Getter for the free disk space
        /// </summary>
        /// <value>Gets Free</value>
        public static float DiskFreeSpacePercentage { get => updateDisk().FreeSizePercentage; }

        /// <summary>
        /// Updates the Disk Usage Percentage to the latest
        /// </summary>
        /// <returns>the updated disk usage percentage</returns>
        private static (float FullSize, float UsedSizePercentage, float FreeSizePercentage) updateDisk()
        {
            string[] diskInfo = System.Text.RegularExpressions.Regex.Split(GetDiskInfo(), @"\s+");

            var fullSize = float.Parse(diskInfo[1].Substring(0, diskInfo[1].Length-1));
            var UsedSize = float.Parse(diskInfo[2].Substring(0, diskInfo[2].Length-1));
            return (fullSize, (UsedSize/fullSize)*100, ((fullSize - UsedSize)/fullSize)*100);
        }

        /// <summary>
        /// This function gets the disk's space information on Linux OS using the Bash function.
        /// </summary>
        /// <returns>Returns a string containing all disk space information</returns>
        private static string GetDiskInfo()
        {
            return Bash(string.Join(" ", "df / -h"));
        }
        /// <summary>
        /// Private function that takes as an input  a command passed to the terminal.
        ///</summary>
        /// <param name="cmd"> The command passed to the terminal</param>
        /// <returns>Returns a string containing the output of the passed command.</returns>
        private static string Bash(string cmd)
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
            process.StandardOutput.ReadLine();
            string result = process.StandardOutput.ReadLine();
            process.WaitForExit();
            return result; 
        }
    }
}
