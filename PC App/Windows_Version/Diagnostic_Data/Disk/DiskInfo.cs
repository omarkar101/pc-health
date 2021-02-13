using System;
using System.Diagnostics;
using System.IO;

namespace Disk_Windows
{
    public class DiskInfo
    {
        private PerformanceCounter diskCounter;//creates a performanceCounter object indicating how much of the disk's read/write capability is used

        public PerformanceCounter DiskCounter { get => diskCounter; set => diskCounter = value; }
        public DiskInfo()
        {
            diskCounter = new PerformanceCounter();
            DiskCounter.CategoryName = "PhysicalDisk";
            DiskCounter.CounterName = "% Disk Time";
            DiskCounter.InstanceName = "_Total";
        }


        public void printAvailableFreeSpace() //prints the available free space in all the disks
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    Console.WriteLine("available space is : " + drive.AvailableFreeSpace / 1000000000 + " GB");
                }
            }


        }

        public void printDiskUsage()
        { //prints the percentage of disk read/write capability used
            int diskCounterTmp;
            diskCounterTmp = DiskCounter.NextValue(); //always starts at 0
            System.Threading.Thread.Sleep(1000);
            diskCounterTmp = DiskCounter.NextValue();// now matches task manager value
            Console.WriteLine("Disk usage is : " + diskCounterTmp + " %");
        }

    }
}