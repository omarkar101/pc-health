using System;
using System.Diagnostics;
namespace Cpu_Windows
{
    public static class CpuInfo
    {
        /// <summary>
        /// Getter for the updated Cpu Percentage
        /// </summary>
        public static float CpuPercentage { get => UpdateCpuUsage(); }

        private static float UpdateCpuUsage()//returns CpuPercentage
        {
            PerformanceCounter cpuCounter = new PerformanceCounter();//creates a Performance Counter object that indicates how much processing power is used
            cpuCounter.CategoryName = "Processor Information";
            cpuCounter.CounterName = "% Processor Utility";
            cpuCounter.InstanceName = "_Total";
            var firstValue = cpuCounter.NextSample();
            System.Threading.Thread.Sleep(100);
            // now matches task manager reading
            var currentValue = cpuCounter.NextSample();
            var cpuValue = CounterSample.Calculate(firstValue, currentValue);
            return cpuValue;
        }
    }
}