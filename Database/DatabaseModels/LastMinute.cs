using System;
using System.Collections.Generic;

#nullable disable

namespace Database.DatabaseModels
{
    public partial class LastMinute
    {
        public string PcId { get; set; }
        public int Second { get; set; }
        public float? PcCpuUsage { get; set; }
        public double? PcMemoryUsage { get; set; }
        public double? PcNetworkAverageBytesSend { get; set; }
        public double? PcNetworkAverageBytesReceived { get; set; }
    }
}
