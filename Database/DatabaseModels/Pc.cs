using System;
using System.Collections.Generic;

#nullable disable

namespace Database.DatabaseModels
{
    public partial class Pc
    {
        public Pc()
        {
            Services = new HashSet<Service>();
        }

        public int PcId { get; set; }
        public string PcOs { get; set; }
        public float PcCpuUsage { get; set; }
        public float PcDiskTotalFreeSpace { get; set; }
        public float PcDiskTotalSpace { get; set; }
        public byte PcFirewallStatus { get; set; }
        public double? PcMemoryUsage { get; set; }
        public double? PcNetworkAverageBytesSend { get; set; }
        public double? PcNetworkAverageBytesReceived { get; set; }
        public string AdminCredentialsUsername { get; set; }
        public string PcUsername { get; set; }

        public virtual Admin AdminCredentialsUsernameNavigation { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
