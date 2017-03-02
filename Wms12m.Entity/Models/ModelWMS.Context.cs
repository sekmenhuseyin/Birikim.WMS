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
    
        public virtual ObjectResult<Nullable<int>> GetGorevNo(Nullable<int> tarih)
        {
            var tarihParameter = tarih.HasValue ?
                new ObjectParameter("tarih", tarih) :
                new ObjectParameter("tarih", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("WMSEntities.GetGorevNo", tarihParameter);
        }
    
        public virtual int Logger(string userName, string machine, string ipAddress, string description, string method, string url)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var machineParameter = machine != null ?
                new ObjectParameter("Machine", machine) :
                new ObjectParameter("Machine", typeof(string));
    
            var ipAddressParameter = ipAddress != null ?
                new ObjectParameter("IpAddress", ipAddress) :
                new ObjectParameter("IpAddress", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var methodParameter = method != null ?
                new ObjectParameter("Method", method) :
                new ObjectParameter("Method", typeof(string));
    
            var urlParameter = url != null ?
                new ObjectParameter("Url", url) :
                new ObjectParameter("Url", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WMSEntities.Logger", userNameParameter, machineParameter, ipAddressParameter, descriptionParameter, methodParameter, urlParameter);
        }
    }
}
