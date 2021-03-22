using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;

namespace PC_App.Windows_Version.Diagnostic_Data.Services_Windows
{
    class ServicesInfo
    {
        public static List<Tuple<string, string>> ServicesNamesAndStatus => GetServicesInfo();
        public static List<Tuple<string, string>> GetServicesInfo()
        {
            var services = ServiceController.GetServices();
            // try to find service name
            return services.Select(service => new Tuple<string, string>(service.ServiceName, service.Status.ToString())).ToList();
        }
    }
}
