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
        public long FreespaceinGB { get => freespaceinGB; set => freespaceinGB = value; }

        private float diskCounterPercentage;
        private long freespaceinGB;
        public DiskInfo()
        {
            diskCounter = new PerformanceCounter();
            DiskCounter.CategoryName = "PhysicalDisk";
            DiskCounter.CounterName = "% Disk Time";
            DiskCounter.InstanceName = "_Total";
            DiskCounterPercentage = 0;
        }


        public void printAvailableFreeSpace() //prints the available free space in all the disks
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    FreespaceinGB = drive.AvailableFreeSpace / 1000000000;
                    Console.WriteLine("available space is : " + FreespaceinGB + " GB");
                }
            }


        }

        public void printDiskUsage()
        { //prints the percentage of disk read/write capability used

            DiskCounterPercentage = DiskCounter.NextValue(); //always starts at 0
            System.Threading.Thread.Sleep(1000);
            DiskCounterPercentage = DiskCounter.NextValue();// now matches task manager value
            Console.WriteLine("Disk usage is : " + DiskCounterPercentage + " %");
        }

    }
}