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
    
        public virtual DbSet<Olcu> Olcus { get; set; }
        public virtual DbSet<TK_BOL> TK_BOL { get; set; }
        public virtual DbSet<TK_DEP> TK_DEP { get; set; }
        public virtual DbSet<TK_KAT> TK_KAT { get; set; }
        public virtual DbSet<TK_KOR> TK_KOR { get; set; }
        public virtual DbSet<TK_RAF> TK_RAF { get; set; }
        public virtual DbSet<IR> IRS { get; set; }
        public virtual DbSet<STI> STIs { get; set; }
        public virtual DbSet<ComboItemName> ComboItemNames { get; set; }
        public virtual DbSet<ComboName> ComboNames { get; set; }
        public virtual DbSet<USR01> USR01 { get; set; }
        public virtual DbSet<GorevListesi> GorevListesis { get; set; }
    
        public virtual ObjectResult<GetHesapCodes_Result> GetHesapCodes()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetHesapCodes_Result>("GetHesapCodes");
        }
    
        public virtual ObjectResult<GetMalzemeCodes_Result> GetMalzemeCodes()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetMalzemeCodes_Result>("GetMalzemeCodes");
        }
    
        public virtual ObjectResult<string> GetHesapUnvan(string kod)
        {
            var kodParameter = kod != null ?
                new ObjectParameter("kod", kod) :
                new ObjectParameter("kod", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("GetHesapUnvan", kodParameter);
        }
    
        public virtual ObjectResult<string> GetMalzemeAd(string kod)
        {
            var kodParameter = kod != null ?
                new ObjectParameter("kod", kod) :
                new ObjectParameter("kod", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("GetMalzemeAd", kodParameter);
        }
    
        public virtual ObjectResult<GetIrsaliyeSTI_Result> GetIrsaliyeSTI(Nullable<int> irsaliyeID)
        {
            var irsaliyeIDParameter = irsaliyeID.HasValue ?
                new ObjectParameter("IrsaliyeID", irsaliyeID) :
                new ObjectParameter("IrsaliyeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetIrsaliyeSTI_Result>("GetIrsaliyeSTI", irsaliyeIDParameter);
        }
    
        public virtual ObjectResult<string> GetMalBirim(string kod)
        {
            var kodParameter = kod != null ?
                new ObjectParameter("kod", kod) :
                new ObjectParameter("kod", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("GetMalBirim", kodParameter);
        }
    
        public virtual ObjectResult<GetMalzeme_Result> GetMalzeme(string kod, string ad)
        {
            var kodParameter = kod != null ?
                new ObjectParameter("kod", kod) :
                new ObjectParameter("kod", typeof(string));
    
            var adParameter = ad != null ?
                new ObjectParameter("ad", ad) :
                new ObjectParameter("ad", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetMalzeme_Result>("GetMalzeme", kodParameter, adParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> UpdateIRS(Nullable<int> iD, Nullable<int> depoID, Nullable<bool> islemTur, string evrakNo, string hesapKodu, string teslimCHK, Nullable<int> tarih, string kaydeden, Nullable<int> kayitTarih, Nullable<int> loggedUserID)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            var depoIDParameter = depoID.HasValue ?
                new ObjectParameter("DepoID", depoID) :
                new ObjectParameter("DepoID", typeof(int));
    
            var islemTurParameter = islemTur.HasValue ?
                new ObjectParameter("IslemTur", islemTur) :
                new ObjectParameter("IslemTur", typeof(bool));
    
            var evrakNoParameter = evrakNo != null ?
                new ObjectParameter("EvrakNo", evrakNo) :
                new ObjectParameter("EvrakNo", typeof(string));
    
            var hesapKoduParameter = hesapKodu != null ?
                new ObjectParameter("HesapKodu", hesapKodu) :
                new ObjectParameter("HesapKodu", typeof(string));
    
            var teslimCHKParameter = teslimCHK != null ?
                new ObjectParameter("TeslimCHK", teslimCHK) :
                new ObjectParameter("TeslimCHK", typeof(string));
    
            var tarihParameter = tarih.HasValue ?
                new ObjectParameter("Tarih", tarih) :
                new ObjectParameter("Tarih", typeof(int));
    
            var kaydedenParameter = kaydeden != null ?
                new ObjectParameter("Kaydeden", kaydeden) :
                new ObjectParameter("Kaydeden", typeof(string));
    
            var kayitTarihParameter = kayitTarih.HasValue ?
                new ObjectParameter("KayitTarih", kayitTarih) :
                new ObjectParameter("KayitTarih", typeof(int));
    
            var loggedUserIDParameter = loggedUserID.HasValue ?
                new ObjectParameter("LoggedUserID", loggedUserID) :
                new ObjectParameter("LoggedUserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("UpdateIRS", iDParameter, depoIDParameter, islemTurParameter, evrakNoParameter, hesapKoduParameter, teslimCHKParameter, tarihParameter, kaydedenParameter, kayitTarihParameter, loggedUserIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> UpdateSTI(Nullable<int> iD, Nullable<int> irsaliyeID, string malKodu, Nullable<decimal> miktar, string birim)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            var irsaliyeIDParameter = irsaliyeID.HasValue ?
                new ObjectParameter("IrsaliyeID", irsaliyeID) :
                new ObjectParameter("IrsaliyeID", typeof(int));
    
            var malKoduParameter = malKodu != null ?
                new ObjectParameter("MalKodu", malKodu) :
                new ObjectParameter("MalKodu", typeof(string));
    
            var miktarParameter = miktar.HasValue ?
                new ObjectParameter("Miktar", miktar) :
                new ObjectParameter("Miktar", typeof(decimal));
    
            var birimParameter = birim != null ?
                new ObjectParameter("Birim", birim) :
                new ObjectParameter("Birim", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("UpdateSTI", iDParameter, irsaliyeIDParameter, malKoduParameter, miktarParameter, birimParameter);
        }
    
        public virtual ObjectResult<GetSirkets_Result> GetSirkets()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetSirkets_Result>("GetSirkets");
        }
    }
}
