using System;
using System.Diagnostics;
using System.IO;
namespace Disk_Windows
{
    public static class DiskInfo
    {
       public static float DiskCounterPercentage { get => UpdateDiskInfo().TotalFreeSpaceInGB ; }
        public static float FreeSpaceInGB { get => UpdateDiskInfo().freeSpaceInGB ; }
        
        private static (float freeSpaceInGB, float TotalFreeSpaceInGB) UpdateDiskInfo()//updates and returns FreeSpaceInGB the available free space in all the disks
        {
            float freeSpaceInGB = 0;
            float TotalFreeSpaceInGB = 0;
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    freeSpaceInGB += ((float)drive.AvailableFreeSpace) / 1000000000;
                    TotalFreeSpaceInGB += ((float) drive.TotalSize )/1000000000;
                }
            }
            return (freeSpaceInGB, TotalFreeSpaceInGB);
        }
        
         
       

    }
}