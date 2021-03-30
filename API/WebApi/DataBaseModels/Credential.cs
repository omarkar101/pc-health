using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Models
{
    public partial class Credential
    {
        public string CredentialsUsername { get; set; }
        public string CredentialsPassword { get; set; }
        public string CredentialsSalt { get; set; }

        public virtual Admin Admin { get; set; }
    }
}
