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
            string []cpuInfoTempArray = System.Text.RegularExpressions.Regex.Split(System.IO.File.ReadAllLines("/proc/stat")[0], @"\s+");
            float totalTimeOfCpuTmp = 0;
            float cpuPercentage;
            for(int i = 1; i < cpuInfoTempArray.Length; i++)
            {
                totalTimeOfCpuTmp += float.Parse(cpuInfoTempArray[i]); 
            }
            float idleTime = float.Parse(cpuInfoTempArray[4]); //this is the idle Cpu time
            float fracIdleTime = idleTime/totalTimeOfCpuTmp;
            cpuPercentage = (float)(1.0 - fracIdleTime) * 100;
            return cpuPercentage;
        }
    }
}
