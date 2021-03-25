using System;
using System.Collections.Generic;
using System.IO;

namespace PC_App.Linux_Version.Diagnostic_Data.Services_Linux
{
    public class ServicesInfo
    {
        public static List<Tuple<string, string>> ServicesNamesAndStatus => GetServicesInfo();
        public static List<Tuple<string, string>> GetServicesInfo()
        {
            var info = Helper.Bash(string.Join(" ", "service  --status-all"));
            var servicesAndStatus = new List<Tuple<string, string>>();
            using (StringReader reader = new StringReader(info))
            {
                string line;
                string serviceName;
                string serviceStatus;
                while ((line = reader.ReadLine()) != null)
                {
                    serviceStatus = (line[3] == '+' ? "Running" : "Stopped");
                    serviceName = line.Substring(6);
                    servicesAndStatus.Add(new Tuple<string, string>(serviceName, serviceStatus));
                }
            }
            return servicesAndStatus;
        }
    }
}
