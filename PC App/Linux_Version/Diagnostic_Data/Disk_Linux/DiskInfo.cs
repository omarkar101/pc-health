using System.Diagnostics;
namespace Disk_Linux
{
    
    /// <summary>
    /// A class responsible for disk information of PCs using Linux OS.
    /// </summary>
    public class DiskInfo
    {
        /// <summary>
        /// Stores the disk Usage Percentage
        /// </summary>
        double diskUsagePercentage;
        
        /// <summary>
        /// Getter for the Disk Usage Percentage
        /// </summary>
        /// <value>Gets Disk Usage Percentage</value>
        public double DiskUsagePercentage { get => updateDisk().UsedSizePercentage; }


        /// <summary>
        /// Stores main Disk Size
        /// </summary>
        double diskSize;

        /// <summary>
        /// Getter for the main disk size
        /// </summary>
        /// <value>Gets main Disk Size</value>
        public double DiskSize { get => updateDisk().FullSize; }


        /// <summary>
        /// Stores the remaining free Disk Space
        /// </summary>
        double diskFreeSpacePercentage;

        /// <summary>
        /// Getter for the free disk space
        /// </summary>
        /// <value>Gets Free</value>
        public double DiskFreeSpacePercentage { get => updateDisk().FreeSizePercentage; }

        

        /// <summary>
        /// Updates the Disk Usage Percentage to the latest
        /// </summary>
        /// <returns>the updated disk usage percentage</returns>
        public (double FullSize, double UsedSizePercentage, double FreeSizePercentage) updateDisk()
        {
            string[] diskInfo = System.Text.RegularExpressions.Regex.Split(GetDiskInfo(), @"\s+");

            double fullSize = double.Parse(diskInfo[1].Substring(0, diskInfo[1].Length-1));
            double UsedSize = double.Parse(diskInfo[2].Substring(0, diskInfo[2].Length-1));
            return (fullSize, (UsedSize/fullSize)*100, ((fullSize - UsedSize)/fullSize)*100);
        }

        /// <summary>
        /// This function gets the disk's space information on Linux OS using the Bash function.
        /// </summary>
        /// <returns>Returns a string containing all disk space information</returns>
        public string GetDiskInfo()
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