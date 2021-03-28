using System;
using System.Linq;
using System.Management;

namespace Memory_Windows
{
    public static class MemoryInfo
    {
        public static dynamic RamUsagePercentage {get => UpdateMemoryUsage();}

        private static dynamic UpdateMemoryUsage()
        { //prints a percentage showing memory usage(Ram usage)
            ManagementObjectSearcher wmiObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem"); //creates a WMI object that contains multiple management objects
            dynamic memoryValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new
            {
                FreePhysicalMemory = Double.Parse(mo["FreePhysicalMemory"].ToString()),
                TotalVisibleMemorySize = Double.Parse(mo["TotalVisibleMemorySize"].ToString())
            }).FirstOrDefault(); //assigns memoryvalues to an object containing doubles indicating The free memory and the total usable memory in the system
            dynamic ramUsagePercentage = 0;
            if (memoryValues != null)
            {
                ramUsagePercentage = ((memoryValues.TotalVisibleMemorySize - memoryValues.FreePhysicalMemory) / memoryValues.TotalVisibleMemorySize) * 100; //calculates the percentage of memory used in the system.
            }
            return ramUsagePercentage;
        }
    }
}