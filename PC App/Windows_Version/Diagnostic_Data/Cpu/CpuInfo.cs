using System;
using System.Diagnostics;
namespace Cpu_Windows
{
    /// <summary>
    /// Class responsible for the CPU info of a PC using Windows OS
    /// </summary>
    public class CpuInfo
    {
        /// <summary>
        /// Creates a Performance Counter object that indicates how much processing power is used
        /// </summary>
        PerformanceCounter cpuCounter;
        /// <summary>
        /// Stores a float that shows the percantage of the used processing power
        /// </summary>
        float cpuPercentage;

        /// <summary>
        /// Default Constructor that will tell the class to store Processor information
        /// </summary>
        public CpuInfo()
        {
            cpuCounter = new PerformanceCounter();
            CpuCounter.CategoryName = "Processor";
            CpuCounter.InstanceName = "_Total";
            CpuCounter.CounterName = "% Processor Time";
            CpuPercentage = updateCpuUsage();
        }

        /// <summary>
        /// Getter and setter for the CpuCounter variable
        /// </summary>
        /// <value>The value of the used processing power</value>
        public PerformanceCounter CpuCounter { get => cpuCounter; set => cpuCounter = value; }
        /// <summary>
        /// Getter and setter for the CpuPercemtage variable
        /// </summary>
        /// <value>The percentage of the used processing power</value>
        public float CpuPercentage { get => cpuPercentage; set => cpuPercentage = value; }
        /// <summary>
        /// Method that computes the percemtage of processing used.
        /// </summary>
        /// <returns>Returns CpuPercentage</returns>
        public float updateCpuUsage()//returns CpuPercentage
        {
            float firstValue = CpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            // now matches task manager reading
            CpuPercentage = CpuCounter.NextValue();
            return CpuPercentage;
        }
    }
}