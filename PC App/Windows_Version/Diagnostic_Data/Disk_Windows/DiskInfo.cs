using System;
using System.Diagnostics;
using System.IO;
namespace Disk_Windows
{
    public static class DiskInfo
    {
       public static float DiskCounterPercentage { get => updateDiskUsage() ; }
        public static long FreeSpaceInGB { get => updateFreeSpaceInGB() ; }
        
        private static long updateFreeSpaceInGB()//updates and returns FreeSpaceInGB the available free space in all the disks
        {
            long freeSpaceInGB = 0;
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    freeSpaceInGB = drive.AvailableFreeSpace / 1000000000;
                }
            }
            return freeSpaceInGB;
        }
        private static float updateDiskUsage()//updates and returns DiskCounterPercentage to get the latest
        {
            PerformanceCounter diskCounter = new PerformanceCounter();//creates a performanceCounter object indicating how much of the disk's read/write capability is used
            diskCounter.CategoryName = "PhysicalDisk";
            diskCounter.CounterName = "% Disk Time";
            diskCounter.InstanceName = "_Total";
            float firstvalue = diskCounter.NextValue(); //always starts at 0
            System.Threading.Thread.Sleep(1000);
            float CurrentValue = diskCounter.NextValue();// now matches task manager value
            return CurrentValue;
        }
        
         
       

    }
}