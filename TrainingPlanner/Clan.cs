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
    
    public partial class Clan
    {
        public Clan()
        {
            this.Trening = new HashSet<Trening>();
            this.Test = new HashSet<Test>();
        }
    
        public int ClanId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public System.DateTime GodinaRodenja { get; set; }
        public string Visina { get; set; }
        public string Tezina { get; set; }
        public short GodineStarosti { get; set; }
        public string Sport { get; set; }
        public string Amneza { get; set; }
        public string Napomena { get; set; }
    
        public virtual ICollection<Trening> Trening { get; set; }
        public virtual ICollection<Test> Test { get; set; }
    }
}