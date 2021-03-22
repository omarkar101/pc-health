using System;
using System.Collections.Generic;

namespace Models
{
    public class DiagnosticData
    {
        public float CpuUsage { get; set; }

        public float TotalFreeDiskSpace { get; set; }

        public float DiskTotalSpace { get; set; }

        public double MemoryUsage { get; set; }

        public double AvgNetworkBytesSent { get; set; }

        public double AvgNetworkBytesReceived { get; set; }

        public string PC_ID { get; set; }

        public string OS { get; set; }

        public List<Tuple<string, string>> Services { get; set; }

        public string FirewallStatus { get; set; }
    }
}
