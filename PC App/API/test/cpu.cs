/*using System;
using System.Diagnostics;
namespace test
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
            CpuPercentage = updateCpuUsage();
        }

        public PerformanceCounter CpuCounter { get => cpuCounter; set => cpuCounter = value; }
        public float CpuPercentage { get => cpuPercentage; set => cpuPercentage = value; }

        public float updateCpuUsage()//returns CpuPercentage
        {
            float firstValue = CpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            // now matches task manager reading
            CpuPercentage = CpuCounter.NextValue();
            return CpuPercentage;
        }
    }
}*/