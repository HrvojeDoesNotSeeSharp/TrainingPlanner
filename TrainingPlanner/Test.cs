//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using TrainingPlanner.Models;

namespace TrainingPlanner
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(TestMetadata))]
    public partial class Test
    {
        public Test()
        {
            this.Slika = new HashSet<Slika>();
        }
    
        public int TestId { get; set; }
        public System.DateTime DatumTesta { get; set; }
        public short Ergometar { get; set; }
        public short Zgibovi { get; set; }
        public short Sklekovi { get; set; }
        public short Trbusnjaci { get; set; }
        public short Cucnjevi { get; set; }
        public string Name { get; set; }
        public int ClanId { get; set; }
    
        public virtual Clan Clan { get; set; }
        public virtual ICollection<Slika> Slika { get; set; }
    }
}
