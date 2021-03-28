using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_App.Windows_Version.Diagnostic_Data.Firewall_Windows
{
    public class FirewallInfo
    {
        public static Boolean FirewallStatus => GetFirewallInfo();
        public static Boolean GetFirewallInfo()
        {
            const int netFwProfile2Domain = 1;
            const int netFwProfile2Private = 2;
            const int netFwProfile2Public = 4;

            // Create the firewall type.
            var fwManagerType = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");

            // Use the firewall type to create a firewall manager object.
            dynamic fwManager = Activator.CreateInstance(fwManagerType);

            // Get the firewall settings.
            bool checkDomain =
                fwManager.FirewallEnabled(netFwProfile2Domain);
            bool checkPrivate =
                fwManager.FirewallEnabled(netFwProfile2Private);
            bool checkPublic =
                fwManager.FirewallEnabled(netFwProfile2Public);

            return checkPublic && checkPrivate && checkDomain;
        }
    }
}
