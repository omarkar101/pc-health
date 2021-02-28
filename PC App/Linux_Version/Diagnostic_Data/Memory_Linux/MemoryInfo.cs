using System;
using System.ComponentModel;

namespace Memory_Linux
{
    /// <summary>
    /// A class that is responsible for memmory informaiton on PCs using Linux OS
    /// </summary>
    public static class MemoryInfo
    {
        public static float MemoryUsagePercentage
        {
            get => UpdateMemoryUsage();
        }

        private static float UpdateMemoryUsage()
        {
            var memoryLines = System.IO.File.ReadAllLines("/proc/meminfo");
            var totalMemory = 0.0f;
            var freeMemory = 0.0f;
            foreach (var line in memoryLines)
            {
                var lineArray = System.Text.RegularExpressions.Regex.Split(line, @"\s+");
                
                if (lineArray[0].Equals("MemTotal:"))
                {
                    totalMemory = float.Parse(lineArray[1]);
                }
                if (lineArray[0].Equals("MemFree:") || lineArray[0].Equals("MemAvailable:"))
                {
                    freeMemory += float.Parse(lineArray[1]);
                }
            }

            var memoryUsagePercentage = (freeMemory / totalMemory) * 100;

            return memoryUsagePercentage; 
        }


    }
}
