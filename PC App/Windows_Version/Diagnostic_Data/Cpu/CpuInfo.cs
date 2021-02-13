using System;
using System.Diagnostics;
namespace Cpu_Windows{
    public  class CpuInfo
{

    int totalHits = 0;

    public static void printCpuUsage() //prints a percentage indicating how much CPU processing power is used
    {

        PerformanceCounter cpuCounter = new PerformanceCounter("Processor","% Processor Time","_Total"); //creates a Performance Counter object that indicates how much processing power is used
        // will always start at 0
        dynamic firstValue = cpuCounter.NextValue();
        System.Threading.Thread.Sleep(1000);
        // now matches task manager reading
        dynamic secondValue = cpuCounter.NextValue();
        Console.WriteLine("CPU performance is: " + secondValue + " %");

    }
}
}