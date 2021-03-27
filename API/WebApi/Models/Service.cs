using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Models
{
    public partial class Service
    {
        public string ServiceName { get; set; }
        public byte ServiceStatus { get; set; }
        public int PcId { get; set; }

        public virtual Pc Pc { get; set; }
    }
}
