using System;
using System.Collections.Generic;
using Models;

namespace Services
{
    public static class StaticStorageServices
    {
        public static Dictionary<string, DiagnosticData> PcMapper = new Dictionary<string, DiagnosticData>();
        //public static ConfigurationFromWebsiteData PCsConfiguration = new ConfigurationFromWebsiteData();
        //public static DateTime TimeToGetPcConfiguration = DateTime.UtcNow.AddSeconds(PCsConfiguration.Time);
    }
}