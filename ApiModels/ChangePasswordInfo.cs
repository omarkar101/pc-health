using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiModels
{
    public class ChangePasswordInfo
    {
        public string CredentialUsername { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
