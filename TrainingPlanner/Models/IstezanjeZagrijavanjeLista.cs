using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingPlanner.Models
{
    public class IstezanjeZagrijavanjeLista
    {
        public List<IstezanjePopis> IstezanjePopis { get; set; }
        public List<ZagrijavanjePopis> ZagrijavanjePopis { get; set; }
        public int TreningId { get; set; }
        public int Izmijeni { get; set; }
    }
}