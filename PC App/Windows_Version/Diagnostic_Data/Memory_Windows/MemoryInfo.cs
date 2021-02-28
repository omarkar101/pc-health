using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Diagnostics;
using System.IO;
namespace Memory_Windows
{
    public static class MemoryInfo
    {
        public static dynamic RamUsagePercentage {get => updateMemoryUsage();}

        private static dynamic updateMemoryUsage()
        { //prints a percentage showing memory usage(Ram usage)
            ManagementObjectSearcher WmiObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem"); //creates a WMI object that contains multiple management objects
            dynamic MemoryValues = WmiObject.Get().Cast<ManagementObject>().Select(mo => new
            {
                FreePhysicalMemory = Double.Parse(mo["FreePhysicalMemory"].ToString()),
                TotalVisibleMemorySize = Double.Parse(mo["TotalVisibleMemorySize"].ToString())
            }).FirstOrDefault(); //assigns memoryvalues to an object containing doubles indicating The free memory and the total usable memory in the system
            dynamic ramUsagePercentage = 0;
            if (MemoryValues != null)
            {
                ramUsagePercentage = ((MemoryValues.TotalVisibleMemorySize - MemoryValues.FreePhysicalMemory) / MemoryValues.TotalVisibleMemorySize) * 100; //calculates the percentage of memory used in the system.
            }
            return ramUsagePercentage;
        }
    }
}