using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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
        private static int Counter { get; set; } = -1;
        public static string GetDiagnosticData()
        {
            var pcConfigurationJsonString = "{\"PcUsername\" : \"\", \"Admins\" : []}";

            try
            {
                pcConfigurationJsonString = File.ReadAllText(@"~\..\Configurations.json", Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var pcConfigurations = JsonSerializer.Deserialize<PcConfiguration>(pcConfigurationJsonString);


            var linuxFalseWindowsTrue = !RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            
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
                CurrentSecond = (Counter = (Counter +1)%60),
                PcConfiguration = pcConfigurations
            };
            //Console.WriteLine(JsonSerializer.Serialize(diagnosticData));
            return JsonSerializer.Serialize(diagnosticData);
        }
    }
}