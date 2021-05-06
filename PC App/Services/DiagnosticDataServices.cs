using System;
using System.Collections.Generic;
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
        private static int CpuHighCounter { get; set; } = 0;
        private static int MemoryHighCounter { get; set; } = 0;
        private static bool []_cpuHighBitArray = new bool[60];
        private static bool[] _memoryHighBitArray = new bool[60];
        private static int Counter { get; set; } = -1;
        private static DateTime StallTime { get; set; } = DateTime.MinValue;
        public static async Task<string> GetDiagnosticData()
        {
            var pcConfigurationJsonString = "{\"PcUsername\" : \"\", \"Admins\" : []}";

            try
            {
                pcConfigurationJsonString = await File.ReadAllTextAsync(@"Configurations.json", Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            } 

            var pcConfigurations = JsonSerializer.Deserialize<PcConfiguration>(pcConfigurationJsonString);

            if (pcConfigurations == null) return "false";
            //making to lower
            MakePcConfigurationToLower(pcConfigurations);

            var diagnosticData = CreateDiagnosticData(pcConfigurations);

            CountHigh(diagnosticData);

            if (DateTime.UtcNow.CompareTo(StallTime) >= 0 && StallTime.CompareTo(DateTime.MinValue) != 0)
            {
                StallTime = DateTime.MinValue;
            }

            await SendWarningIfDangerousAndNoStall(diagnosticData);

            return JsonSerializer.Serialize(diagnosticData);

        }

        private static void MakePcConfigurationToLower(PcConfiguration? pcConfigurations)
        {
            if (pcConfigurations != null)
            {
                pcConfigurations.PcEmail = pcConfigurations.PcEmail.ToLower();
                for (var i = 0; i < pcConfigurations.Admins.Count; i++)
                {
                    pcConfigurations.Admins[i] = new Tuple<string, string>(pcConfigurations.Admins[i].Item1.ToLower(),
                        pcConfigurations.Admins[i].Item2);
                }
            }
        }

        private static DiagnosticData CreateDiagnosticData(PcConfiguration? pcConfigurations)
        {
            var linuxFalseWindowsTrue = !RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

            var diagnosticData = new DiagnosticData()
            {
                CpuUsage =
                    (linuxFalseWindowsTrue ? Cpu_Windows.CpuInfo.CpuPercentage : Cpu_Linux.CpuInfo.CpuPercentage),
                AvgNetworkBytesReceived = NetworkInfo.AvgNetworkBytesReceived,
                AvgNetworkBytesSent = NetworkInfo.AvgNetworkBytesSent,
                DiskTotalSpace = DiskInfo.DiskCounterPercentage,
                MemoryUsage = (linuxFalseWindowsTrue
                    ? Memory_Windows.MemoryInfo.RamUsagePercentage
                    : Memory_Linux.MemoryInfo.MemoryUsagePercentage),
                TotalFreeDiskSpace = DiskInfo.FreeSpaceInGb,
                PcId = new DeviceIdBuilder()
                    .AddMachineName()
                    .ToString(),
                Os = (linuxFalseWindowsTrue ? "Windows" : "Linux"),
                Services = linuxFalseWindowsTrue
                    ? PC_App.Windows_Version.Diagnostic_Data.Services_Windows.ServicesInfo.ServicesNamesAndStatus
                    : PC_App.Linux_Version.Diagnostic_Data.Services_Linux.ServicesInfo.ServicesNamesAndStatus,
                FirewallStatus = linuxFalseWindowsTrue
                    ? (PC_App.Windows_Version.Diagnostic_Data.Firewall_Windows.FirewallInfo.FirewallStatus
                        ? "Active"
                        : "Inactive")
                    : PC_App.Linux_Version.Diagnostic_Data.Firewall_Linux.FirewallInfo.FirewallStatus
                        ? "Active"
                        : "Inactive",
                CurrentSecond = (Counter = (Counter + 1) % 60),
                PcConfiguration = pcConfigurations,
                HealthStatus = "Healthy"
            };
            return diagnosticData;
        }

        private static async Task SendWarningIfDangerousAndNoStall(DiagnosticData diagnosticData)
        {
            if (CpuHighCounter >= 40 || MemoryHighCounter >= 40)
            {
                diagnosticData.HealthStatus = "Unhealthy";
                if (StallTime.CompareTo(DateTime.MinValue) == 0)
                {
                    await SendWarning(diagnosticData.PcConfiguration);
                }
            }
        }

        private static async Task SendWarning(PcConfiguration pcConfiguration)
        {
            var pcHealthData = new PcHealthData()
            {
                CpuHighCounter = CpuHighCounter,
                MemoryHighCounter = MemoryHighCounter,
                PcConfiguration = pcConfiguration ?? new PcConfiguration()
            };
            StallTime = DateTime.UtcNow.AddMinutes(10);
            await PostServices.PostPcHealthData("https://pc-health.azurewebsites.net/Pc/PostPcHealthDataFromPc",
                pcHealthData);
        }
        private static void CountHigh(DiagnosticData diagnosticData)
        {
            if (_cpuHighBitArray[Counter])
            {
                CpuHighCounter--;
                _cpuHighBitArray[Counter] = false;
            }

            if (_memoryHighBitArray[Counter])
            {
                MemoryHighCounter--;
                _memoryHighBitArray[Counter] = false;
            }

            if (diagnosticData.CpuUsage > 80)
            {
                CpuHighCounter++;
                _cpuHighBitArray[Counter] = true;
            }

            if (diagnosticData.MemoryUsage > 80)
            {
                MemoryHighCounter++;
                _memoryHighBitArray[Counter] = true;
            }
        }
    }
}