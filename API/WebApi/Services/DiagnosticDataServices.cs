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
            
            bool linux_0_Windows_1;

            if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) linux_0_Windows_1 = false;
            else linux_0_Windows_1 = true;
            
            Diagnostic_Data diagnostic_Data = new Diagnostic_Data()
            {
                CpuUsage = Cpu_Linux.CpuInfo.CpuPercentage,
                AvgNetworkBytesReceived = Network_Linux.NetworkInfo.AvgNetworkBytesReceived,
                AvgNetworkBytesSent = Network_Linux.NetworkInfo.AvgNetworkBytesSent,
                DiskUsage = Disk_Linux.DiskInfo.DiskUsagePercentage,
                MemoryUsage = Memory_Linux.MemoryInfo.MemoryUsagePercentage,
                TotalFreeDiskSpace = Disk_Linux.DiskInfo.DiskSize
            };
            string DiagnosticDataInJsonFormat = JsonSerializer.Serialize(diagnostic_Data);
            return DiagnosticDataInJsonFormat;
        }
    }
}