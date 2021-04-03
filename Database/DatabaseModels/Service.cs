using System;
using System.Collections.Generic;

#nullable disable

namespace Database.DatabaseModels
{
    public partial class Service
    {
        public string ServiceName { get; set; }
        public byte ServiceStatus { get; set; }
        public string PcId { get; set; }

        public virtual Pc Pc { get; set; }
    }
}
