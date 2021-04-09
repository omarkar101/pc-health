using System;
using System.Collections.Generic;
using CommonModels;

namespace Services
{
    public static class StaticStorageServices
    {
                                //admin, 
        public static Dictionary<string, Dictionary<string, DiagnosticData>> PcMapper =
            new Dictionary<string, Dictionary<string, DiagnosticData>>();

        public static Dictionary<string, string> AdminMapper = new Dictionary<string, string>();
    }
}