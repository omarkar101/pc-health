using System;
using System.Collections.Generic;

#nullable disable

namespace Database.DatabaseModels
{
    public partial class Admin
    {
        public Admin()
        {
            AdminHasPcs = new HashSet<AdminHasPc>();
        }

        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public string AdminCredentialsUsername { get; set; }

        public virtual Credential AdminCredentialsUsernameNavigation { get; set; }
        public virtual ICollection<AdminHasPc> AdminHasPcs { get; set; }
    }
}
