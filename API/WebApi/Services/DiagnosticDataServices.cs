using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System;
using System.Diagnostics;
using WebApi.Models;
using Cpu_Linux;
using Disk_Linux;
using System.Runtime.InteropServices;
using Memory_Linux;
using Network_Linux;
using Cpu_Windows;
using Disk_Windows;
using Memory_Windows;
using Network_Windows;

namespace WebApi.Services
{
    public class DiagnosticDataServices
    {
        public string GetDiagnosticData() {
            
            bool linux_false_Windows_true;

            if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) linux_false_Windows_true = false;
            else linux_false_Windows_true = true;
            
            Diagnostic_Data diagnostic_Data = new Diagnostic_Data()
            {
                CpuUsage = (linux_false_Windows_true ? Cpu_Windows.CpuInfo.CpuPercentage : Cpu_Linux.CpuInfo.CpuPercentage),
                AvgNetworkBytesReceived = (linux_false_Windows_true ? Network_Windows.NetworkInfo.DataReceived : Network_Linux.NetworkInfo.AvgNetworkBytesReceived),
                AvgNetworkBytesSent = (linux_false_Windows_true ? Network_Windows.NetworkInfo.DataSent : Network_Linux.NetworkInfo.AvgNetworkBytesSent),
                DiskUsage = (linux_false_Windows_true ? Disk_Windows.DiskInfo.DiskCounterPercentage : Disk_Linux.DiskInfo.DiskUsagePercentage),
                MemoryUsage = (linux_false_Windows_true ? Memory_Windows.MemoryInfo.RamUsagePercentage : Memory_Linux.MemoryInfo.MemoryUsagePercentage),
                TotalFreeDiskSpace = (linux_false_Windows_true ? Disk_Windows.DiskInfo.FreeSpaceInGB : Disk_Linux.DiskInfo.DiskFreeSpaceGB)
            };
            string DiagnosticDataInJsonFormat = JsonSerializer.Serialize(diagnostic_Data);
            return DiagnosticDataInJsonFormat;
        }
    }
}