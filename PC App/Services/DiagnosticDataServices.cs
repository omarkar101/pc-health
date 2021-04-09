using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json;
using CommonModels;
using DeviceId;
using PC_App.Common_Version.Diagnostic_Data.Disk;
using PC_App.Common_Version.Diagnostic_Data.Network;

namespace PC_App.Services
{
    /// <summary>
    /// A service that gets the Diagnostic Data according to the Operating System
    /// </summary>
    public static class DiagnosticDataServices
    {
        private static int Counter { get; set; } = 0;
        public static string GetDiagnosticData() {
            
            bool linuxFalseWindowsTrue;

            linuxFalseWindowsTrue = !RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            
            var diagnosticData = new DiagnosticData()
            {
                CpuUsage = (linuxFalseWindowsTrue ? Cpu_Windows.CpuInfo.CpuPercentage : Cpu_Linux.CpuInfo.CpuPercentage),
                AvgNetworkBytesReceived = NetworkInfo.AvgNetworkBytesReceived,
                AvgNetworkBytesSent = NetworkInfo.AvgNetworkBytesSent,
                DiskTotalSpace = DiskInfo.DiskCounterPercentage,
                MemoryUsage = (linuxFalseWindowsTrue ? Memory_Windows.MemoryInfo.RamUsagePercentage : Memory_Linux.MemoryInfo.MemoryUsagePercentage),
                TotalFreeDiskSpace = DiskInfo.FreeSpaceInGb,
                PcId = new DeviceIdBuilder()
                    .AddMachineName()
                    .ToString(),
                Os = (linuxFalseWindowsTrue ? "Windows" : "Linux"),
                Services = linuxFalseWindowsTrue ? PC_App.Windows_Version.Diagnostic_Data.Services_Windows.ServicesInfo.ServicesNamesAndStatus : PC_App.Linux_Version.Diagnostic_Data.Services_Linux.ServicesInfo.ServicesNamesAndStatus,
                FirewallStatus = linuxFalseWindowsTrue ? 
                    (PC_App.Windows_Version.Diagnostic_Data.Firewall_Windows.FirewallInfo.FirewallStatus ? 
                    "Active" : "Inactive") : 
                    PC_App.Linux_Version.Diagnostic_Data.Firewall_Linux.FirewallInfo.FirewallStatus ? "Active" : "Inactive",
                AdminUsernames = new List<string>(){ "rony123", "omk13","mmm130" },
                PcUsername = "",
                CurrentSecond = (Counter = (Counter + 1)%60)
            };
            return JsonSerializer.Serialize<DiagnosticData>(diagnosticData);
        }
    }
}