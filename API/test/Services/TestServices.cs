using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System;
using System.Diagnostics;
using Cpu_Windows;
using Disk_Windows;
using Memory_Windows;
namespace test{
    public class TestServices
    {
        private readonly HttpClient client = new HttpClient();
        
        public string  GetTestData(){
            CpuInfo c = new CpuInfo();
            DiskInfo d = new DiskInfo();
            MemoryInfo m = new MemoryInfo();
            stats stat = new stats();
            stat._cpuUsage = c.updateCpuUsage();
            stat._diskUsage = d.updateDiskUsage();
            stat._memoryUsage = m.updateMemoryUsage();
            stat._totalFreeDiskSpace = d.updateFreeSpaceInGB();
            string s = JsonSerializer.Serialize(stat);
            return s;
        }
        public async Task<stats> RetrieveTestData(){
            var statTask = client.GetStreamAsync("https://localhost:5001/api/Test/Hi");
            var retrievedstats = await JsonSerializer.DeserializeAsync<stats>(await statTask);
            return retrievedstats;
        }
    }
}