//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrainingPlanner
{
    using System;
    using System.Collections.Generic;
    
    public partial class AmnezaSlike
    {
        public int SlikaId { get; set; }
        public string SlikaIme { get; set; }
        public int AmnezaAmnezaId { get; set; }
    
        public virtual Amneza Amneza { get; set; }
    }
}
