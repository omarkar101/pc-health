using System;
namespace Cpu_Linux
{
    /// <summary>
    ///A class responsible for CPU info of PCs using Linux OS.
    /// </summary>
    public static class CpuInfo
    {
        /// <summary>
        /// Getter for the Cpu Utilization Percentage.
        /// </summary>
        /// <value>Gets the Cpu Utilization Percentage</value>
        public static float CpuPercentage { get => UpdateCpuPercentage(); }

        private static float UpdateCpuPercentage()
        {
            string []cpuInfoTempArray = System.IO.File.ReadAllLines("/proc/stat")[0].Split(new string[] {"  ", " "}, StringSplitOptions.None);
            float totalTimeOfCpuTmp = 0;
            float cpuPercentage;
            for(int i = 1; i < cpuInfoTempArray.Length; i++)
        /// A double that stores the Cpu Utilization Percentage
        /// </summary>
        private double _cpuPercentage;
        /// <summary>
        /// A string that stores the name of the file that stores the CPU information.
        /// </summary>
        private string _cpuInfoLoc;
        /// <summary>
        /// This string array will store the CPU information.
        /// </summary>
        private string[] _cpuInfoLines;
        /// <summary>
        /// This constructor will extract the information from the default CPU information file and store them in variables.
        /// </summary>
        public CpuInfo()
        {
            _cpuInfoLoc = "/proc/cpuinfo";
            _cpuInfoLines = System.IO.File.ReadAllLines(_cpuInfoLoc);

            //Applying some code to get the Cpu Percentage
            var cpuInfoTempArray = System.IO.File.ReadAllLines("/proc/stat")[0].Split(new string[] {"  ", " "}, StringSplitOptions.None);
            var totalTimeOfCpuTmp = 0.0;
            for(var i = 1; i < cpuInfoTempArray.Length; i++)
            {
                totalTimeOfCpuTmp += float.Parse(cpuInfoTempArray[i]); 
            }
            float idleTime = float.Parse(cpuInfoTempArray[4]); //this is the idle Cpu time
            float fracIdleTime = idleTime/totalTimeOfCpuTmp;
            cpuPercentage = (float)(1.0 - fracIdleTime) * 100;
            return cpuPercentage;
        }
            var idleTime = double.Parse(cpuInfoTempArray[4]); //this is the idle Cpu time
            var fracIdleTime = idleTime/totalTimeOfCpuTmp;
            _cpuPercentage = (1.0 - fracIdleTime) * 100;
        }

        /// <summary>
        /// This consructor will take 2 parameters, and stores information depending on these parameters.
        /// </summary>
        /// <param name="cpuInfoLoc">This parameter decides the file that we want to extract information from.</param>
        /// <param name="cpuInfoLines">This parameter will store all the information found in that file.</param>
        public CpuInfo(string cpuInfoLoc, string[] cpuInfoLines)
        {
            this._cpuInfoLoc = cpuInfoLoc;
            this._cpuInfoLines = cpuInfoLines;
        }
        /// <summary>
        /// Getter for the string array CpuInfoLines.
        /// </summary>
        /// <value>Gets the elements of the array.</value>
        public string[] CpuInfoLines { get => _cpuInfoLines; }
        /// <summary>
        /// Getter for the Cpu Utilization Percentage.
        /// </summary>
        /// <value>Gets the Cpu Utilization Percentage</value>
        public double CpuPercentage => _cpuPercentage;
    }
}
