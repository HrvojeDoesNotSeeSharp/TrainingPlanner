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

    public partial class IstezanjeTreningTemplate
    {
        public IstezanjeTreningTemplate()
        {
            this.IstezanjeT = new HashSet<IstezanjeT>();
        }
    
        public int IstezanjeTreningTemplateId { get; set; }
        public string NazivPredloska { get; set; }
    
        public virtual ICollection<IstezanjeT> IstezanjeT { get; set; }
    }
}