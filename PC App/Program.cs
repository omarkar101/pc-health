using System.Threading.Tasks;
using Cpu_Linux;
using PC_App.General_Info;
using PC_App.Linux_Version.Diagnostic_Data.Disk_Linux;
using PC_App.Linux_Version.Diagnostic_Data.Memory_Linux;

namespace PC_App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var msg = new DiagnosticInfo
            {
                _cpuUsage = CpuInfo.CpuPercentage,
                _diskUsage = DiskInfo.DiskUsagePercentage,
                _memoryUsage = MemoryInfo.MemoryUsagePercentage,
                _networkUsage = 212,
                _totalFreeDiskSpace = DiskInfo.DiskFreeSpacePercentage
            };

            await General_Info_Send.Start(msg, 9000);
        }
        
    }
}
