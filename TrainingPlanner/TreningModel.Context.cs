﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TreningModelContainer : DbContext
    {
        public TreningModelContainer()
            : base("name=TreningModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Clan> Clan { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<Trening> Trening { get; set; }
        public DbSet<IstezanjePopis> IstezanjePopis { get; set; }
        public DbSet<VjezbePopis> VjezbePopis { get; set; }
        public DbSet<ZagrijavanjePopis> ZagrijavanjePopis { get; set; }
        public DbSet<Zagrijavanje> Zagrijavanje { get; set; }
        public DbSet<Vjezba> Vjezba { get; set; }
        public DbSet<Istezanje> Istezanje { get; set; }
    }
}
