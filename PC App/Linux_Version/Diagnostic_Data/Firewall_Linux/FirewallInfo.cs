using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_App.Linux_Version.Diagnostic_Data.Firewall_Linux
{
    public class FirewallInfo
    {
        public static Boolean FirewallStatus => GetFirewallInfo();

        public static Boolean GetFirewallInfo()
        {
            var info = Helper.Bash(string.Join(" ", "sudo ufw status"));
            var infoList = info.Split(' ');
            return (infoList[1].Equals("inactive"));
        }
    }
}
