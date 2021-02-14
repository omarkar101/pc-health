using System;
using System.Diagnostics;
using System.IO;

namespace Disk_Windows
{
    /// <summary>
    /// Class responsible for disk info of PCs' runnung on Windows OS
    /// </summary>
    public class DiskInfo
    {
        /// <summary>
        /// Creates a performanceCounter object indicating how much of the disk's read/write capability is used
        /// </summary>
        private PerformanceCounter diskCounter;

        public PerformanceCounter DiskCounter { get => diskCounter; set => diskCounter = value; }
        public float DiskCounterPercentage { get => diskCounterPercentage; set => diskCounterPercentage = value; }
        public long FreeSpaceInGB { get => freespaceinGB; set => freespaceinGB = value; }
        /// <summary>
        /// Stores the percentage of the used disk storage
        /// </summary>
        private float diskCounterPercentage;
        /// <summary>
        /// Stores how much disk storage is free in GB
        /// </summary>
        private long freespaceinGB;
        /// <summary>
        /// Default constructor that tells the class to get the disk information
        /// </summary>
        public DiskInfo()
        {
            diskCounter = new PerformanceCounter();
            DiskCounter.CategoryName = "PhysicalDisk";
            DiskCounter.CounterName = "% Disk Time";
            DiskCounter.InstanceName = "_Total";
            DiskCounterPercentage = updateDiskUsage();
        }
        /// <summary>
        /// Updates FreeSpaceInGB the available space in all the disks
        /// </summary>
        /// <returns>Returns the available space in all disks</returns>
        public long updateFreeSpaceInGB()
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
        /// <summary>
        /// Updates DiskounterPercentage to the current used percentage. It starts from zero then the system rests for 1 sec then updates the percentage to its true value.
        /// </summary>
        /// <returns>Returns the current percentage of the used disk storage</returns>
        public float updateDiskUsage()//updates and returns DiskCounterPercentage to get the latest
        {
            DiskCounterPercentage = DiskCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            DiskCounterPercentage = DiskCounter.NextValue();// now matches task manager value
            return DiskCounterPercentage;
        }

    }
}