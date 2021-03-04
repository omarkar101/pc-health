using System.Runtime.InteropServices;
using System.Text.Json;
using DeviceId;
using Models;

namespace Services
{
    public static class DiagnosticDataServices
    {
        public static string GetDiagnosticData() {
            
            bool linux_false_Windows_true;

            if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) linux_false_Windows_true = false;
            else linux_false_Windows_true = true;
            
            Diagnostic_Data diagnostic_Data = new Diagnostic_Data()
            {
                CpuUsage = (linux_false_Windows_true ? Cpu_Windows.CpuInfo.CpuPercentage : Cpu_Linux.CpuInfo.CpuPercentage),
                AvgNetworkBytesReceived = (linux_false_Windows_true ? Network_Windows.NetworkInfo.DataReceived : Network_Linux.NetworkInfo.AvgNetworkBytesReceived),
                AvgNetworkBytesSent = (linux_false_Windows_true ? Network_Windows.NetworkInfo.DataSent : Network_Linux.NetworkInfo.AvgNetworkBytesSent),
                DiskTotalSpace = (linux_false_Windows_true ? Disk_Windows.DiskInfo.DiskCounterPercentage : Disk_Linux.DiskInfo.DiskSize),
                MemoryUsage = (linux_false_Windows_true ? Memory_Windows.MemoryInfo.RamUsagePercentage : Memory_Linux.MemoryInfo.MemoryUsagePercentage),
                TotalFreeDiskSpace = (linux_false_Windows_true ? Disk_Windows.DiskInfo.FreeSpaceInGB : Disk_Linux.DiskInfo.DiskFreeSpaceGB),
                PC_ID = new DeviceIdBuilder()
                    .AddMachineName()
                    .ToString(),
                OS = (linux_false_Windows_true ? "Windows" : "Linux")
            };
            string DiagnosticDataInJsonFormat = JsonSerializer.Serialize<Diagnostic_Data>(diagnostic_Data);
            return DiagnosticDataInJsonFormat;
        }
    }
}