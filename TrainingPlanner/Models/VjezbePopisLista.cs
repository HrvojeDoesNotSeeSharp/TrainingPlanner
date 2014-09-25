using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingPlanner.Models
{
    public class VjezbePopisLista
    {
        public List<VjezbePopis> vjezbePopis { get; set; }
        public int TreningId { get; set; }
        public int Izmijeni { get; set; }
    }
}