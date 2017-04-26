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
        public virtual DbSet<Bolum> Bolums { get; set; }
        public virtual DbSet<Depo> Depoes { get; set; }
        public virtual DbSet<Gorev> Gorevs { get; set; }
        public virtual DbSet<Kat> Kats { get; set; }
        public virtual DbSet<Koridor> Koridors { get; set; }
        public virtual DbSet<Raf> Rafs { get; set; }
        public virtual DbSet<Yer> Yers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<GorevYer> GorevYers { get; set; }
        public virtual DbSet<Simge> Simges { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<IRS_Detay> IRS_Detay { get; set; }
        public virtual DbSet<IR> IRS { get; set; }
        public virtual DbSet<Yer_Log> Yer_Log { get; set; }
        public virtual DbSet<EvrakSeri> EvrakSeris { get; set; }
        public virtual DbSet<Transfer_Detay> Transfer_Detay { get; set; }
        public virtual DbSet<Transfer> Transfers { get; set; }
        public virtual DbSet<Ayarlar> Ayarlars { get; set; }
        public virtual DbSet<Olcu> Olcus { get; set; }
        public virtual DbSet<WebMenu> WebMenus { get; set; }
        public virtual DbSet<Perm> Perms { get; set; }
        public virtual DbSet<RolePerm> RolePerms { get; set; }
    
        public virtual ObjectResult<string> GetSirketDBs()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("WMSEntities.GetSirketDBs");
        }
    
        public virtual ObjectResult<GetSirkets_Result> GetSirkets()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetSirkets_Result>("WMSEntities.GetSirkets");
        }
    
        public virtual int Logger(string userName, string machine, string ipAddress, string description, string message, string source)
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
    
            var messageParameter = message != null ?
                new ObjectParameter("Message", message) :
                new ObjectParameter("Message", typeof(string));
    
            var sourceParameter = source != null ?
                new ObjectParameter("Source", source) :
                new ObjectParameter("Source", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WMSEntities.Logger", userNameParameter, machineParameter, ipAddressParameter, descriptionParameter, messageParameter, sourceParameter);
        }
    
        public virtual int MenuRolEkle(Nullable<short> menuID, string rolNo)
        {
            var menuIDParameter = menuID.HasValue ?
                new ObjectParameter("MenuID", menuID) :
                new ObjectParameter("MenuID", typeof(short));
    
            var rolNoParameter = rolNo != null ?
                new ObjectParameter("RolNo", rolNo) :
                new ObjectParameter("RolNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WMSEntities.MenuRolEkle", menuIDParameter, rolNoParameter);
        }
    
        public virtual int DeleteFromGorev(Nullable<int> gorevID)
        {
            var gorevIDParameter = gorevID.HasValue ?
                new ObjectParameter("GorevID", gorevID) :
                new ObjectParameter("GorevID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WMSEntities.DeleteFromGorev", gorevIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> GetBolumSiralamaFromGorevId(Nullable<int> gorevID, Nullable<int> koridorID, Nullable<bool> desc)
        {
            var gorevIDParameter = gorevID.HasValue ?
                new ObjectParameter("GorevID", gorevID) :
                new ObjectParameter("GorevID", typeof(int));
    
            var koridorIDParameter = koridorID.HasValue ?
                new ObjectParameter("KoridorID", koridorID) :
                new ObjectParameter("KoridorID", typeof(int));
    
            var descParameter = desc.HasValue ?
                new ObjectParameter("desc", desc) :
                new ObjectParameter("desc", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("WMSEntities.GetBolumSiralamaFromGorevId", gorevIDParameter, koridorIDParameter, descParameter);
        }
    
        public virtual ObjectResult<GetHomeSummary_Result> GetHomeSummary()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetHomeSummary_Result>("WMSEntities.GetHomeSummary");
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
    
        public virtual ObjectResult<GetIrsDetayfromGorev_Result> GetIrsDetayfromGorev(Nullable<int> gorevID)
        {
            var gorevIDParameter = gorevID.HasValue ?
                new ObjectParameter("GorevID", gorevID) :
                new ObjectParameter("GorevID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetIrsDetayfromGorev_Result>("WMSEntities.GetIrsDetayfromGorev", gorevIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> GetKoridorIdFromGorevId(Nullable<int> gorevID)
        {
            var gorevIDParameter = gorevID.HasValue ?
                new ObjectParameter("GorevID", gorevID) :
                new ObjectParameter("GorevID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("WMSEntities.GetKoridorIdFromGorevId", gorevIDParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> GetStock(Nullable<int> depoID, string malKodu, string birim, Nullable<bool> includeRezerv)
        {
            var depoIDParameter = depoID.HasValue ?
                new ObjectParameter("DepoID", depoID) :
                new ObjectParameter("DepoID", typeof(int));
    
            var malKoduParameter = malKodu != null ?
                new ObjectParameter("MalKodu", malKodu) :
                new ObjectParameter("MalKodu", typeof(string));
    
            var birimParameter = birim != null ?
                new ObjectParameter("Birim", birim) :
                new ObjectParameter("Birim", typeof(string));
    
            var includeRezervParameter = includeRezerv.HasValue ?
                new ObjectParameter("includeRezerv", includeRezerv) :
                new ObjectParameter("includeRezerv", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("WMSEntities.GetStock", depoIDParameter, malKoduParameter, birimParameter, includeRezervParameter);
        }
    
        public virtual ObjectResult<InsertIrsaliye_Result> InsertIrsaliye(string sirketKod, Nullable<int> depoID, string gorevNo, string irsEvrakNo, Nullable<int> irsTarih, string gorevBilgi, Nullable<bool> irsIslemTur, Nullable<int> gorevTipiID, Nullable<int> olusturanID, string olusturan, Nullable<int> olusturmaTarihi, Nullable<int> olusturmaSaati, string hesapKodu, string teslimCHK, Nullable<short> valorGun, string linkEvrakNo)
        {
            var sirketKodParameter = sirketKod != null ?
                new ObjectParameter("SirketKod", sirketKod) :
                new ObjectParameter("SirketKod", typeof(string));
    
            var depoIDParameter = depoID.HasValue ?
                new ObjectParameter("DepoID", depoID) :
                new ObjectParameter("DepoID", typeof(int));
    
            var gorevNoParameter = gorevNo != null ?
                new ObjectParameter("GorevNo", gorevNo) :
                new ObjectParameter("GorevNo", typeof(string));
    
            var irsEvrakNoParameter = irsEvrakNo != null ?
                new ObjectParameter("IrsEvrakNo", irsEvrakNo) :
                new ObjectParameter("IrsEvrakNo", typeof(string));
    
            var irsTarihParameter = irsTarih.HasValue ?
                new ObjectParameter("IrsTarih", irsTarih) :
                new ObjectParameter("IrsTarih", typeof(int));
    
            var gorevBilgiParameter = gorevBilgi != null ?
                new ObjectParameter("GorevBilgi", gorevBilgi) :
                new ObjectParameter("GorevBilgi", typeof(string));
    
            var irsIslemTurParameter = irsIslemTur.HasValue ?
                new ObjectParameter("IrsIslemTur", irsIslemTur) :
                new ObjectParameter("IrsIslemTur", typeof(bool));
    
            var gorevTipiIDParameter = gorevTipiID.HasValue ?
                new ObjectParameter("GorevTipiID", gorevTipiID) :
                new ObjectParameter("GorevTipiID", typeof(int));
    
            var olusturanIDParameter = olusturanID.HasValue ?
                new ObjectParameter("OlusturanID", olusturanID) :
                new ObjectParameter("OlusturanID", typeof(int));
    
            var olusturanParameter = olusturan != null ?
                new ObjectParameter("Olusturan", olusturan) :
                new ObjectParameter("Olusturan", typeof(string));
    
            var olusturmaTarihiParameter = olusturmaTarihi.HasValue ?
                new ObjectParameter("OlusturmaTarihi", olusturmaTarihi) :
                new ObjectParameter("OlusturmaTarihi", typeof(int));
    
            var olusturmaSaatiParameter = olusturmaSaati.HasValue ?
                new ObjectParameter("OlusturmaSaati", olusturmaSaati) :
                new ObjectParameter("OlusturmaSaati", typeof(int));
    
            var hesapKoduParameter = hesapKodu != null ?
                new ObjectParameter("HesapKodu", hesapKodu) :
                new ObjectParameter("HesapKodu", typeof(string));
    
            var teslimCHKParameter = teslimCHK != null ?
                new ObjectParameter("TeslimCHK", teslimCHK) :
                new ObjectParameter("TeslimCHK", typeof(string));
    
            var valorGunParameter = valorGun.HasValue ?
                new ObjectParameter("ValorGun", valorGun) :
                new ObjectParameter("ValorGun", typeof(short));
    
            var linkEvrakNoParameter = linkEvrakNo != null ?
                new ObjectParameter("LinkEvrakNo", linkEvrakNo) :
                new ObjectParameter("LinkEvrakNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InsertIrsaliye_Result>("WMSEntities.InsertIrsaliye", sirketKodParameter, depoIDParameter, gorevNoParameter, irsEvrakNoParameter, irsTarihParameter, gorevBilgiParameter, irsIslemTurParameter, gorevTipiIDParameter, olusturanIDParameter, olusturanParameter, olusturmaTarihiParameter, olusturmaSaatiParameter, hesapKoduParameter, teslimCHKParameter, valorGunParameter, linkEvrakNoParameter);
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
    
        public virtual int TerminalFinishGorev(Nullable<int> gorevID, Nullable<int> irsaliyeID, string yeniGorevNo, Nullable<int> bitisTarihi, Nullable<int> bitisSaati, Nullable<int> kullaniciID, string linkEvrakNo, Nullable<int> görevTipiID, Nullable<int> yeniGorevTipiID)
        {
            var gorevIDParameter = gorevID.HasValue ?
                new ObjectParameter("GorevID", gorevID) :
                new ObjectParameter("GorevID", typeof(int));
    
            var irsaliyeIDParameter = irsaliyeID.HasValue ?
                new ObjectParameter("IrsaliyeID", irsaliyeID) :
                new ObjectParameter("IrsaliyeID", typeof(int));
    
            var yeniGorevNoParameter = yeniGorevNo != null ?
                new ObjectParameter("YeniGorevNo", yeniGorevNo) :
                new ObjectParameter("YeniGorevNo", typeof(string));
    
            var bitisTarihiParameter = bitisTarihi.HasValue ?
                new ObjectParameter("BitisTarihi", bitisTarihi) :
                new ObjectParameter("BitisTarihi", typeof(int));
    
            var bitisSaatiParameter = bitisSaati.HasValue ?
                new ObjectParameter("BitisSaati", bitisSaati) :
                new ObjectParameter("BitisSaati", typeof(int));
    
            var kullaniciIDParameter = kullaniciID.HasValue ?
                new ObjectParameter("KullaniciID", kullaniciID) :
                new ObjectParameter("KullaniciID", typeof(int));
    
            var linkEvrakNoParameter = linkEvrakNo != null ?
                new ObjectParameter("LinkEvrakNo", linkEvrakNo) :
                new ObjectParameter("LinkEvrakNo", typeof(string));
    
            var görevTipiIDParameter = görevTipiID.HasValue ?
                new ObjectParameter("GörevTipiID", görevTipiID) :
                new ObjectParameter("GörevTipiID", typeof(int));
    
            var yeniGorevTipiIDParameter = yeniGorevTipiID.HasValue ?
                new ObjectParameter("YeniGorevTipiID", yeniGorevTipiID) :
                new ObjectParameter("YeniGorevTipiID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WMSEntities.TerminalFinishGorev", gorevIDParameter, irsaliyeIDParameter, yeniGorevNoParameter, bitisTarihiParameter, bitisSaatiParameter, kullaniciIDParameter, linkEvrakNoParameter, görevTipiIDParameter, yeniGorevTipiIDParameter);
        }
    
        public virtual ObjectResult<YetkiDepo_Result> YetkiDepo(Nullable<int> iD)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<YetkiDepo_Result>("WMSEntities.YetkiDepo", iDParameter);
        }
    
        public virtual int YetkiDepoSet(Nullable<int> depoID, Nullable<int> userID, Nullable<bool> ekle)
        {
            var depoIDParameter = depoID.HasValue ?
                new ObjectParameter("DepoID", depoID) :
                new ObjectParameter("DepoID", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var ekleParameter = ekle.HasValue ?
                new ObjectParameter("Ekle", ekle) :
                new ObjectParameter("Ekle", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WMSEntities.YetkiDepoSet", depoIDParameter, userIDParameter, ekleParameter);
        }
    
        public virtual int MenuSiralayici(Nullable<int> webSiteTipiNo, Nullable<int> menuYeriNo, Nullable<short> ustMenuNo)
        {
            var webSiteTipiNoParameter = webSiteTipiNo.HasValue ?
                new ObjectParameter("WebSiteTipiNo", webSiteTipiNo) :
                new ObjectParameter("WebSiteTipiNo", typeof(int));
    
            var menuYeriNoParameter = menuYeriNo.HasValue ?
                new ObjectParameter("MenuYeriNo", menuYeriNo) :
                new ObjectParameter("MenuYeriNo", typeof(int));
    
            var ustMenuNoParameter = ustMenuNo.HasValue ?
                new ObjectParameter("UstMenuNo", ustMenuNo) :
                new ObjectParameter("UstMenuNo", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WMSEntities.MenuSiralayici", webSiteTipiNoParameter, menuYeriNoParameter, ustMenuNoParameter);
        }
    
        public virtual ObjectResult<MenuRolGetir_Result> MenuRolGetir(Nullable<short> menuNo)
        {
            var menuNoParameter = menuNo.HasValue ?
                new ObjectParameter("MenuNo", menuNo) :
                new ObjectParameter("MenuNo", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MenuRolGetir_Result>("WMSEntities.MenuRolGetir", menuNoParameter);
        }
    
        public virtual ObjectResult<MenuGetirici_Result> MenuGetirici(Nullable<int> webSiteTipiID, Nullable<int> menuYeriID, string roleName, Nullable<short> ustMenuID)
        {
            var webSiteTipiIDParameter = webSiteTipiID.HasValue ?
                new ObjectParameter("WebSiteTipiID", webSiteTipiID) :
                new ObjectParameter("WebSiteTipiID", typeof(int));
    
            var menuYeriIDParameter = menuYeriID.HasValue ?
                new ObjectParameter("MenuYeriID", menuYeriID) :
                new ObjectParameter("MenuYeriID", typeof(int));
    
            var roleNameParameter = roleName != null ?
                new ObjectParameter("RoleName", roleName) :
                new ObjectParameter("RoleName", typeof(string));
    
            var ustMenuIDParameter = ustMenuID.HasValue ?
                new ObjectParameter("UstMenuID", ustMenuID) :
                new ObjectParameter("UstMenuID", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MenuGetirici_Result>("WMSEntities.MenuGetirici", webSiteTipiIDParameter, menuYeriIDParameter, roleNameParameter, ustMenuIDParameter);
        }
    
        public virtual ObjectResult<Nullable<short>> MenuFindAktif(Nullable<int> webSiteTipiNo, Nullable<int> menuYeriNo, string roleName, Nullable<short> ustMenuID, string url)
        {
            var webSiteTipiNoParameter = webSiteTipiNo.HasValue ?
                new ObjectParameter("WebSiteTipiNo", webSiteTipiNo) :
                new ObjectParameter("WebSiteTipiNo", typeof(int));
    
            var menuYeriNoParameter = menuYeriNo.HasValue ?
                new ObjectParameter("MenuYeriNo", menuYeriNo) :
                new ObjectParameter("MenuYeriNo", typeof(int));
    
            var roleNameParameter = roleName != null ?
                new ObjectParameter("RoleName", roleName) :
                new ObjectParameter("RoleName", typeof(string));
    
            var ustMenuIDParameter = ustMenuID.HasValue ?
                new ObjectParameter("UstMenuID", ustMenuID) :
                new ObjectParameter("UstMenuID", typeof(short));
    
            var urlParameter = url != null ?
                new ObjectParameter("url", url) :
                new ObjectParameter("url", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<short>>("WMSEntities.MenuFindAktif", webSiteTipiNoParameter, menuYeriNoParameter, roleNameParameter, ustMenuIDParameter, urlParameter);
        }
    
        public virtual ObjectResult<Nullable<bool>> GetPermissionFor(string roleName, string permName, string group, string perm)
        {
            var roleNameParameter = roleName != null ?
                new ObjectParameter("RoleName", roleName) :
                new ObjectParameter("RoleName", typeof(string));
    
            var permNameParameter = permName != null ?
                new ObjectParameter("PermName", permName) :
                new ObjectParameter("PermName", typeof(string));
    
            var groupParameter = group != null ?
                new ObjectParameter("Group", group) :
                new ObjectParameter("Group", typeof(string));
    
            var permParameter = perm != null ?
                new ObjectParameter("Perm", perm) :
                new ObjectParameter("Perm", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<bool>>("WMSEntities.GetPermissionFor", roleNameParameter, permNameParameter, groupParameter, permParameter);
        }
    }
}
