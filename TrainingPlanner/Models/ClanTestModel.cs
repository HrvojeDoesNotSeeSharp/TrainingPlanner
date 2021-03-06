﻿using System.Collections.Generic;

namespace TrainingPlanner.Models
{
    public class ClanTestModel
    {
        public Clan Clan { get; set; }
        public List<Test> ListaTest { get; set; }
        public List<Antropometrija> ListAntropometrija { get; set; }
        public List<Amneza> ListaAmneza { get; set; }
    }
}