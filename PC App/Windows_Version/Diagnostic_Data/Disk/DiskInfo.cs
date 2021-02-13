using System;
using System.Diagnostics;
using System.IO;

namespace Disk_Windows{
public class DiskInfo{
public static PerformanceCounter diskCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total"); //creates a performanceCounter object indicating how much of the disk's read/write capability is used
 public static float j = 0; //sets j to 0
public static void printAvailableFreeSpace() //prints the available free space in all the disks
{
    foreach (DriveInfo drive in DriveInfo.GetDrives())
    {
        if (drive.IsReady)
        {
            Console.WriteLine("available space is : " + drive.AvailableFreeSpace/1000000000 +" GB");
        }
    }
    
    
}

public static void printDiskUsage(){ //prints the percentage of disk read/write capability used
j = diskCounter.NextValue(); //always starts at 0
System.Threading.Thread.Sleep(1000);
j = diskCounter.NextValue();// now matches task manager value
Console.WriteLine("Disk usage is : " + j +" %");
}

}
}