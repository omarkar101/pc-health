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
        
        
        /// <summary>
        /// Stores the percentage of the used disk storage
        /// </summary>
        private double diskUsagePercentage;

        /// <summary>
        /// Getter for Disk Usage Percentage
        /// </summary>
        /// <returns>Gets updated Disk Usage Percentage</returns>
        public double DiskUsagePercentage { get => updateDiskUsage(); }
        
        
        
        /// <summary>
        /// Stores how much disk storage is free in GB
        /// </summary>
        private double diskFreeSpaceInGB;
        
        public double DiskFreeSpaceInGB { get => diskFreeSpaceInGB; set => diskFreeSpaceInGB = value; }
        



        /// <summary>
        /// Default constructor that tells the class to get the disk information
        /// </summary>
        public DiskInfo()
        {
            diskCounter = new PerformanceCounter();
            DiskCounter.CategoryName = "PhysicalDisk";
            DiskCounter.CounterName = "% Disk Time";
            DiskCounter.InstanceName = "_Total";
        }
        
        
        
        /// <summary>
        /// Updates DiskFreeSpaceInGB the available space in all the disks
        /// </summary>
        /// <returns>Returns the available space in all disks</returns>
        public double updateFreeSpaceInGB()
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    DiskFreeSpaceInGB = drive.AvailableFreeSpace / 1000000000.0;
                }
            }
            return DiskFreeSpaceInGB;
        }
        
        
        
        
        /// <summary>
        /// Updates DiskounterPercentage to the current used percentage. It starts from zero then the system rests for 1 sec then updates the percentage to its true value.
        /// </summary>
        /// <returns>Returns the current percentage of the used disk storage</returns>
        public double updateDiskUsage()//updates and returns DiskUsagePercentage to get the latest
        {
            diskUsagePercentage = DiskCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            diskUsagePercentage = DiskCounter.NextValue();// now matches task manager value
            return diskUsagePercentage;
        }

    }
}