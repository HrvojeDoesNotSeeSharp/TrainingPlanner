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
    
    public partial class AerobneVjezbe
    {
        public int AerobnaVjezbaId { get; set; }
        public string Naziv { get; set; }
        public string Puls { get; set; }
        public string Tempo { get; set; }
        public string Trajanje { get; set; }
        public string Napomena { get; set; }
        public int SekcijaVjezbiSekcijaId { get; set; }
        public int AerobneVjezbePopisAerobnaVjezbaId { get; set; }
        public string Ime { get; set; }
        public string Info { get; set; }
        public byte[] Slika { get; set; }
    
        public virtual SekcijaVjezbi SekcijaVjezbi { get; set; }
        public virtual AerobneVjezbePopis AerobneVjezbePopis { get; set; }
    }
}
