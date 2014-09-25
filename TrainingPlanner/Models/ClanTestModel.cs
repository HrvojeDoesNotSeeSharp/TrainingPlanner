using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingPlanner.Models
{
    public class ClanTestModel
    {
        public Clan Clan { get; set; }
        public List<Test> listaTest { get; set; }
    }
}