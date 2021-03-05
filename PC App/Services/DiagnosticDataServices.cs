using System.Runtime.InteropServices;
using System.Text.Json;
using DeviceId;
using Models;
using PC_App.Common_Version.Diagnostic_Data.Disk;
using PC_App.Common_Version.Diagnostic_Data.Network;

namespace Services
{
    /// <summary>
    /// A service that gets the Diagnostic Data according to the Operating System
    /// </summary>
    public static class DiagnosticDataServices
    {
        public static string GetDiagnosticData() {
            
            bool linux_false_Windows_true;

            if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) linux_false_Windows_true = false;
            else linux_false_Windows_true = true;
            
            Diagnostic_Data diagnostic_Data = new Diagnostic_Data()
            {
                CpuUsage = (linux_false_Windows_true ? Cpu_Windows.CpuInfo.CpuPercentage : Cpu_Linux.CpuInfo.CpuPercentage),
                AvgNetworkBytesReceived = NetworkInfo.AvgNetworkBytesReceived,
                AvgNetworkBytesSent = NetworkInfo.AvgNetworkBytesSent,
                DiskTotalSpace = DiskInfo.DiskCounterPercentage,
                MemoryUsage = (linux_false_Windows_true ? Memory_Windows.MemoryInfo.RamUsagePercentage : Memory_Linux.MemoryInfo.MemoryUsagePercentage),
                TotalFreeDiskSpace = DiskInfo.FreeSpaceInGB,
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