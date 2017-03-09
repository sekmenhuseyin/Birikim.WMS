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
    
        public virtual DbSet<Combo_Name> Combo_Name { get; set; }
        public virtual DbSet<ComboItem_Name> ComboItem_Name { get; set; }
        public virtual DbSet<IR> IRS { get; set; }
        public virtual DbSet<IRS_Detay> IRS_Detay { get; set; }
        public virtual DbSet<Olcu> Olcus { get; set; }
        public virtual DbSet<Yer> Yers { get; set; }
        public virtual DbSet<Yer_Log> Yer_Log { get; set; }
        public virtual DbSet<Bolum> Bolums { get; set; }
        public virtual DbSet<Depo> Depoes { get; set; }
        public virtual DbSet<Koridor> Koridors { get; set; }
        public virtual DbSet<Raf> Rafs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Kat> Kats { get; set; }
        public virtual DbSet<Gorev> Gorevs { get; set; }
    
        public virtual ObjectResult<GetSirkets_Result> GetSirkets()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetSirkets_Result>("WMSEntities.GetSirkets");
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
    
        public virtual ObjectResult<GetHucreAd_Result> GetHucreAd(Nullable<int> depoID)
        {
            var depoIDParameter = depoID.HasValue ?
                new ObjectParameter("DepoID", depoID) :
                new ObjectParameter("DepoID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetHucreAd_Result>("WMSEntities.GetHucreAd", depoIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> GetHucreKatID(Nullable<int> depoID, string kod)
        {
            var depoIDParameter = depoID.HasValue ?
                new ObjectParameter("DepoID", depoID) :
                new ObjectParameter("DepoID", typeof(int));
    
            var kodParameter = kod != null ?
                new ObjectParameter("Kod", kod) :
                new ObjectParameter("Kod", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("WMSEntities.GetHucreKatID", depoIDParameter, kodParameter);
        }
    
        public virtual ObjectResult<string> SettingsGorevNo(Nullable<int> tarih)
        {
            var tarihParameter = tarih.HasValue ?
                new ObjectParameter("tarih", tarih) :
                new ObjectParameter("tarih", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("WMSEntities.SettingsGorevNo", tarihParameter);
        }
    
        public virtual ObjectResult<string> SettingsIrsaliyeNo(Nullable<int> tarih)
        {
            var tarihParameter = tarih.HasValue ?
                new ObjectParameter("tarih", tarih) :
                new ObjectParameter("tarih", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("WMSEntities.SettingsIrsaliyeNo", tarihParameter);
        }
    }
}
