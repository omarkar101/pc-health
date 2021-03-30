using System;
using System.Collections.Generic;

#nullable disable

namespace Database.DatabaseModels
{
    public partial class Admin
    {
        public Admin()
        {
            Pcs = new HashSet<Pc>();
        }

        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public string AdminCredentialsUsername { get; set; }

        public virtual Credential AdminCredentialsUsernameNavigation { get; set; }
        public virtual ICollection<Pc> Pcs { get; set; }
    }
}
