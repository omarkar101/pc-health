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
            List<string> lines = new List<string>();
            using (StringReader reader = new StringReader(info))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            foreach (var line in lines)
            {
                Console.WriteLine(line[2] + " " + line[6]);   
            }

            return null;
        }
    }
}
