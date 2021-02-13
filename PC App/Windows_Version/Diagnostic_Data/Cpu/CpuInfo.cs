using System;
using System.Diagnostics;
namespace Cpu_Windows
{
    public class CpuInfo
    {
        PerformanceCounter cpuCounter;//creates a Performance Counter object that indicates how much processing power is used
        float cpuPercentage;
        public CpuInfo()
        {
            cpuCounter = new PerformanceCounter();
            CpuCounter.CategoryName = "Processor";
            CpuCounter.InstanceName = "_Total";
            CpuCounter.CounterName = "% Processor Time";
            CpuPercentage = 0;
        }

        public PerformanceCounter CpuCounter { get => cpuCounter; set => cpuCounter = value; }
        public float CpuPercentage { get => cpuPercentage; set => cpuPercentage = value; }

        public void printCpuUsage() //prints a percentage indicating how much CPU processing power is used
        {
            // will always start at 0
            float firstValue = CpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            // now matches task manager reading
            float secondValue = CpuCounter.NextValue();
            Console.WriteLine("CPU performance is: " + secondValue + " %");

        }
    }
}