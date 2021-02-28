using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System;
using System.Diagnostics;
using WebApi.Models;

namespace WebApi.Services
{
    public class DiagnosticDataServices
    {
        public string GetDiagnosticData(){
        //     // CpuInfo c = new CpuInfo();
        //     // DiskInfo d = new DiskInfo();
        //     // MemoryInfo m = new MemoryInfo();
        //     // stats stat = new stats();
        //     // stat._cpuUsage = c.updateCpuUsage();
        //     // stat._diskUsage = d.updateDiskUsage();
        //     // stat._memoryUsage = m.updateMemoryUsage();
        //     // stat._totalFreeDiskSpace = d.updateFreeSpaceInGB();
        //     string DiagnosticDataInJsonFormat = JsonSerializer.Serialize(stat);
        //     return DiagnosticDataInJsonFormat;
        }

        // public async Task<Diagnostic_Data> RetrieveDiagnosticData(){
        //     var DiagnosticDataTask = client.GetStreamAsync("");
        //     var retrievedDiagnosticData = await JsonSerializer.DeserializeAsync<Diagnostic_Data>(await DiagnosticDataTask);
        //     return retrievedDiagnosticData;
        // }
    }
}