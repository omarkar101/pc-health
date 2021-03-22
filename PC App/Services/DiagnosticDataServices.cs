using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json;
using DeviceId;
using Models;
using PC_App.Common_Version.Diagnostic_Data.Disk;
using PC_App.Common_Version.Diagnostic_Data.Network;
using PC_App.Windows_Version.Diagnostic_Data.Firewall_Windows;
using PC_App.Windows_Version.Diagnostic_Data.Services_Windows;

namespace Services
{
    /// <summary>
    /// A service that gets the Diagnostic Data according to the Operating System
    /// </summary>
    public static class DiagnosticDataServices
    {
        public static string GetDiagnosticData() {
            
            bool linux_false_Windows_true;

            linux_false_Windows_true = !RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            
            var diagnostic_Data = new DiagnosticData()
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
                OS = (linux_false_Windows_true ? "Windows" : "Linux"),
                Services = linux_false_Windows_true ?  ServicesInfo.ServicesNamesAndStatus : new List<Tuple<string, string>>()
                ,
                FirewallStatus = linux_false_Windows_true ? 
                    (PC_App.Windows_Version.Diagnostic_Data.Firewall_Windows.FirewallInfo.FirewallStatus ? 
                    "Active" : "Inactive") : ""
            };
            return JsonSerializer.Serialize<DiagnosticData>(diagnostic_Data);
        }
    }
}