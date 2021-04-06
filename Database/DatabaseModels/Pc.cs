using System;
using System.Collections.Generic;

#nullable disable

namespace Database.DatabaseModels
{
    public partial class Pc
    {
        public Pc()
        {
            AdminHasPcs = new HashSet<AdminHasPc>();
            Services = new HashSet<Service>();
        }

        public string PcId { get; set; }
        public string PcOs { get; set; }
        public float PcCpuUsage { get; set; }
        public float PcDiskTotalFreeSpace { get; set; }
        public float PcDiskTotalSpace { get; set; }
        public string PcFirewallStatus { get; set; }
        public double PcMemoryUsage { get; set; }
        public double? PcNetworkAverageBytesSend { get; set; }
        public double? PcNetworkAverageBytesReceived { get; set; }
        public string PcUsername { get; set; }

        public virtual ICollection<AdminHasPc> AdminHasPcs { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
