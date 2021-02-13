using System;
namespace Cpu
{
    public class CpuInfo
    {
        string cpuInfoLoc;
        string[] cpuInfoLines;
        public CpuInfo()
        {
            cpuInfoLoc = "/proc/cpuinfo";
            cpuInfoLines = System.IO.File.ReadAllLines(cpuInfoLoc);
        }

        public CpuInfo(string cpuInfoLoc, string[] cpuInfoLines)
        {
            this.cpuInfoLoc = cpuInfoLoc;
            this.cpuInfoLines = cpuInfoLines;
        }

        public void printCpuInfo()
        {
            foreach(var line in cpuInfoLines)
            {
                Console.WriteLine(line);
            }
        }
        public string CpuInfoLoc { get => cpuInfoLoc; set => cpuInfoLoc = value; }
        public string[] CpuInfoLines { get => cpuInfoLines; set => cpuInfoLines = value; }
    }
}
