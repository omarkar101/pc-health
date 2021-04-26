using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonModels
{
    public class PcHealthData
    {
        public PcConfiguration PcConfiguration { get; set; }
        public int CpuHighCounter { get; set; }
        public int MemoryHighCounter { get; set; }
    }
}
