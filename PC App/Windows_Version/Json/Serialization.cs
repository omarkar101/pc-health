using Cpu_Windows;
using Info_Windows;
using Disk_Windows;
using Network_Windows;
using Memory_Windows;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Serialization{
    public class Serializer{
        Diagnostic_Info d = new Diagnostic_Info();
        CpuInfo cpu = new CpuInfo();
        DiskInfo disk = new DiskInfo();
        MemoryInfo memory = new MemoryInfo();
        NetworkInfo network =new  NetworkInfo();
        public string jsonString;
        public Serializer(){
            d.CPUUsage = cpu.updateCpuUsage();
            d.DiskUsage = disk.updateDiskUsage();
            d.TotalFreeDiskSpace = disk.updateFreeSpaceInGB();
            d.MemoryUsage = memory.updateMemoryUsage();
            d.NetworkUsage = network.updateNetworkUsage();
            jsonString = JsonSerializer.Serialize<Diagnostic_Info>(d);
            


            
        }


    }
}