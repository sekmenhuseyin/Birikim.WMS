﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wms12m.Entity.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class WMSEntities : DbContext
    {
        public WMSEntities()
            : base("name=WMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ComboItemName> ComboItemNames { get; set; }
        public virtual DbSet<ComboName> ComboNames { get; set; }
        public virtual DbSet<TK_BOL> TK_BOL { get; set; }
        public virtual DbSet<TK_KAT> TK_KAT { get; set; }
        public virtual DbSet<TK_KOR> TK_KOR { get; set; }
        public virtual DbSet<TK_RAF> TK_RAF { get; set; }
        public virtual DbSet<WMS_IRS> WMS_IRS { get; set; }
        public virtual DbSet<WMS_STI> WMS_STI { get; set; }
        public virtual DbSet<TK_DEP> TK_DEP { get; set; }
        public virtual DbSet<Olcu> Olcus { get; set; }
        public virtual DbSet<USR01> USR01 { get; set; }
        public virtual DbSet<GorevListesi> GorevListesis { get; set; }
    
        public virtual ObjectResult<GetSirkets_Result> GetSirkets()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetSirkets_Result>("WMSEntities.GetSirkets");
        }
    }
}
