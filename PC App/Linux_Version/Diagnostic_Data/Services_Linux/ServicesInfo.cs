using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_App.Linux_Version.Diagnostic_Data.Services_Linux
{
    public class ServicesInfo
    {
        public static List<Tuple<string, string>> ServicesNamesAndStatus => GetServicesInfo();
        public static List<Tuple<string, string>> GetServicesInfo()
        {
            var info = Helper.Bash(string.Join(" ", "service  --status-all"));
            Console.WriteLine(info);
            return null;
        }
    }
}
