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
    using System.ComponentModel.DataAnnotations;
    using TrainingPlanner.Models;

    [MetadataType(typeof(AmnezaMetadata))]
    public partial class Amneza
    {
        public Amneza()
        {
            this.AmnezaSlike = new HashSet<AmnezaSlike>();
        }
    
        public int AmnezaId { get; set; }
        public string Ime { get; set; }
        public string Opis { get; set; }
        public int ClanClanId { get; set; }
    
        public virtual ICollection<AmnezaSlike> AmnezaSlike { get; set; }
        public virtual Clan Clan { get; set; }
    }
}
