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
    
    public partial class Zagrijavanje
    {
        public int ZagrijavanjeId { get; set; }
        public string Naziv { get; set; }
        public string Info { get; set; }
        public int TreningId { get; set; }
        public string Tempo { get; set; }
        public string Puls { get; set; }
        public string ZagrijavanjeNapomena { get; set; }
        public int ZagrijavanjePopisZagrijavanjeId { get; set; }
        public string Trajanje { get; set; }
        public byte[] Slika { get; set; }
    
        public virtual Trening Trening { get; set; }
        public virtual ZagrijavanjePopis ZagrijavanjePopis { get; set; }
    }
}
