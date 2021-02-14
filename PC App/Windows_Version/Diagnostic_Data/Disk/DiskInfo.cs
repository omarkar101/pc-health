using System;
using System.Diagnostics;
using System.IO;

namespace Disk_Windows
{
    public class DiskInfo
    {
        private PerformanceCounter diskCounter;//creates a performanceCounter object indicating how much of the disk's read/write capability is used

        public PerformanceCounter DiskCounter { get => diskCounter; set => diskCounter = value; }
        public float DiskCounterPercentage { get => diskCounterPercentage; set => diskCounterPercentage = value; }
        public long FreeSpaceInGB { get => freespaceinGB; set => freespaceinGB = value; }

        private float diskCounterPercentage;
        private long freespaceinGB;
        public DiskInfo()
        {
            diskCounter = new PerformanceCounter();
            DiskCounter.CategoryName = "PhysicalDisk";
            DiskCounter.CounterName = "% Disk Time";
            DiskCounter.InstanceName = "_Total";
            DiskCounterPercentage = updateDiskUsage();
        }
        
        public long updateFreeSpaceInGB()//updates and returns FreeSpaceInGB the available free space in all the disks
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    FreeSpaceInGB = drive.AvailableFreeSpace / 1000000000;
                }
            }
            return FreeSpaceInGB;
        }

        public float updateDiskUsage()//updates and returns DiskCounterPercentage to get the latest
        {
            DiskCounterPercentage = DiskCounter.NextValue(); //always starts at 0
            System.Threading.Thread.Sleep(1000);
            DiskCounterPercentage = DiskCounter.NextValue();// now matches task manager value
            return DiskCounterPercentage;
        }

    }
}