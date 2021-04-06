using System;
using System.Collections.Generic;

#nullable disable

namespace Database.DatabaseModels
{
    public partial class AdminHasPc
    {
        public string AdminCredentialsUsername { get; set; }
        public string PcId { get; set; }

        public virtual Admin AdminCredentialsUsernameNavigation { get; set; }
        public virtual Pc Pc { get; set; }
    }
}
