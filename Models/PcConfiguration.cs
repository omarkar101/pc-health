using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonModels
{
    public class PcConfiguration
    {
        public string PcUsername { get; set; }
        public List<Tuple<string, string>> Admins { get; set; }
    }
}
