using System.IO;

namespace PC_App.Common_Version.Diagnostic_Data.Disk
{
    public static class DiskInfo
    {
        public static float DiskCounterPercentage { get => UpdateDiskInfo().TotalFreeSpaceInGB ; }
        public static float FreeSpaceInGb { get => UpdateDiskInfo().freeSpaceInGB ; }
        
        private static (float freeSpaceInGB, float TotalFreeSpaceInGB) UpdateDiskInfo()//updates and returns FreeSpaceInGB the available free space in all the disks
        {
            float freeSpaceInGb = 0;
            float totalFreeSpaceInGb = 0;
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    freeSpaceInGb += ((float)drive.AvailableFreeSpace) / 1000000000;
                    totalFreeSpaceInGb += ((float) drive.TotalSize )/1000000000;
                }
            }
            return (freeSpaceInGb, totalFreeSpaceInGb);
        }
    }
}