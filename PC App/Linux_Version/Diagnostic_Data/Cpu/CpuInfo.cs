using System;
namespace Cpu_Linux
{
    /// <summary>
    ///A class responsible for CPU info of PCs using Linux OS.
    /// </summary>
    public class CpuInfo
    {
        /// <summary>
        /// A string that stores the name of the file that stores the CPU information.
        /// </summary>
        string cpuInfoLoc;
        /// <summary>
        /// This string array will store the CPU information.
        /// </summary>
        string[] cpuInfoLines;
        /// <summary>
        /// This constructor will extract the information from the default CPU information file and store them in variables.
        /// </summary>
        public CpuInfo()
        {
            cpuInfoLoc = "/proc/cpuinfo";
            cpuInfoLines = System.IO.File.ReadAllLines(cpuInfoLoc);
        }

        /// <summary>
        /// This consructor will take 2 parameters, and stores information depending on these parameters.
        /// </summary>
        /// <param name="cpuInfoLoc">his parameter decides the file that we want to extract information from.</param>
        /// <param name="cpuInfoLines">This parameter will store all the information found in that file.</param>
        public CpuInfo(string cpuInfoLoc, string[] cpuInfoLines)
        {
            this.cpuInfoLoc = cpuInfoLoc;
            this.cpuInfoLines = cpuInfoLines;
        }
        /// <summary>
        /// This method will print to the screen all the store information.
        /// </summary>
        public void printCpuInfo()
        {
            foreach (var line in cpuInfoLines)
            {
                Console.WriteLine(line);
            }
        }
        /// <summary>
        /// Getter and Setter for CpuInfoLoc variable.
        /// </summary>
        /// <value>Will set/get the value of CpuInfoLoc</value>
        public string CpuInfoLoc { get => cpuInfoLoc; set => cpuInfoLoc = value; }
        /// <summary>
        /// Getter and Setter for the string array CpuInfoLines.
        /// </summary>
        /// <value>Sets/Gets the elements of the array.</value>
        public string[] CpuInfoLines { get => cpuInfoLines; set => cpuInfoLines = value; }
    }
}
