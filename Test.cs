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
    
    public partial class Test
    {
        public int TestId { get; set; }
        public byte[] Dijagnostika { get; set; }
        public System.DateTime DatumTesta { get; set; }
        public short Ergometar { get; set; }
        public short Zgibovi { get; set; }
        public short Sklekovi { get; set; }
        public short Trbusnjaci { get; set; }
        public short Cucnjevi { get; set; }
        public string Name { get; set; }
        public string DijagnostikaType { get; set; }
        public int ClanId { get; set; }
    
        public virtual Clan Clan { get; set; }
    }
}
