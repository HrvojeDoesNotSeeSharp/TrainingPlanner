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
    
    public partial class VjezbaZagrijavanje
    {
        public int VjezbaId { get; set; }
        public string ImeVjezbe { get; set; }
        public string Info { get; set; }
        public string BrojPonavljanja { get; set; }
        public string BrojSerija { get; set; }
        public string Kilogrami { get; set; }
        public string Odmor { get; set; }
        public int VjezbePopisVjezbeId { get; set; }
        public int ZagrijavanjeSkupinaZagrijavanjeSkupinaId { get; set; }
        public string Napomena { get; set; }
    
        public virtual VjezbePopis VjezbePopis { get; set; }
        public virtual ZagrijavanjeSkupina ZagrijavanjeSkupina { get; set; }
    }
}
