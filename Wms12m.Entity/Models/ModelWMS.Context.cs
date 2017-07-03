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
    
        public virtual DbSet<Simge> Simges { get; set; }
        public virtual DbSet<WebMenu> WebMenus { get; set; }
        public virtual DbSet<Perm> Perms { get; set; }
        public virtual DbSet<RolePerm> RolePerms { get; set; }
        public virtual DbSet<UserPerm> UserPerms { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Bolum> Bolums { get; set; }
        public virtual DbSet<Depo> Depoes { get; set; }
        public virtual DbSet<Gorev> Gorevs { get; set; }
        public virtual DbSet<GorevYer> GorevYers { get; set; }
        public virtual DbSet<Koridor> Koridors { get; set; }
        public virtual DbSet<Olcu> Olcus { get; set; }
        public virtual DbSet<Raf> Rafs { get; set; }
        public virtual DbSet<Transfer> Transfers { get; set; }
        public virtual DbSet<Transfer_Detay> Transfer_Detay { get; set; }
        public virtual DbSet<Yer> Yers { get; set; }
        public virtual DbSet<Yer_Log> Yer_Log { get; set; }
        public virtual DbSet<Kat> Kats { get; set; }
        public virtual DbSet<IR> IRS { get; set; }
        public virtual DbSet<GorevNo> GorevNoes { get; set; }
        public virtual DbSet<Combo_Name> Combo_Name { get; set; }
        public virtual DbSet<ComboItem_Name> ComboItem_Name { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<GorevUser> GorevUsers { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<GorevCalisma> GorevCalismas { get; set; }
        public virtual DbSet<UserDevice> UserDevices { get; set; }
        public virtual DbSet<Gorevler> Gorevlers { get; set; }
        public virtual DbSet<Musteri> Musteris { get; set; }
        public virtual DbSet<ProjeForm> ProjeForms { get; set; }
        public virtual DbSet<IRS_Detay> IRS_Detay { get; set; }
        public virtual DbSet<GorevPaketNo> GorevPaketNoes { get; set; }
        public virtual DbSet<GorevPaketler> GorevPaketlers { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
    
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
    
        public virtual ObjectResult<Nullable<bool>> GetPermissionFor(Nullable<int> userID, string roleName, string permName, string group, string perm)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
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
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<bool>>("WMSEntities.GetPermissionFor", userIDParameter, roleNameParameter, permNameParameter, groupParameter, permParameter);
        }
    
        public virtual ObjectResult<InsertIrsaliye_Result> InsertIrsaliye(string sirketKod, Nullable<int> depoID, string gorevNo, string irsEvrakNo, Nullable<int> irsTarih, string gorevBilgi, Nullable<bool> irsIslemTur, Nullable<int> gorevTipiID, string olusturan, Nullable<int> olusturmaTarihi, Nullable<int> olusturmaSaati, string hesapKodu, string teslimCHK, Nullable<short> valorGun, string linkEvrakNo)
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
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InsertIrsaliye_Result>("WMSEntities.InsertIrsaliye", sirketKodParameter, depoIDParameter, gorevNoParameter, irsEvrakNoParameter, irsTarihParameter, gorevBilgiParameter, irsIslemTurParameter, gorevTipiIDParameter, olusturanParameter, olusturmaTarihiParameter, olusturmaSaatiParameter, hesapKoduParameter, teslimCHKParameter, valorGunParameter, linkEvrakNoParameter);
        }
    
        public virtual ObjectResult<GetGorevlis_Result> GetGorevlis(Nullable<int> depoID)
        {
            var depoIDParameter = depoID.HasValue ?
                new ObjectParameter("DepoID", depoID) :
                new ObjectParameter("DepoID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetGorevlis_Result>("WMSEntities.GetGorevlis", depoIDParameter);
        }
    
        public virtual int UpdateIrsaliye(Nullable<int> irsID, string evrakNo, string linkEvrakNo)
        {
            var irsIDParameter = irsID.HasValue ?
                new ObjectParameter("IrsID", irsID) :
                new ObjectParameter("IrsID", typeof(int));
    
            var evrakNoParameter = evrakNo != null ?
                new ObjectParameter("EvrakNo", evrakNo) :
                new ObjectParameter("EvrakNo", typeof(string));
    
            var linkEvrakNoParameter = linkEvrakNo != null ?
                new ObjectParameter("LinkEvrakNo", linkEvrakNo) :
                new ObjectParameter("LinkEvrakNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WMSEntities.UpdateIrsaliye", irsIDParameter, evrakNoParameter, linkEvrakNoParameter);
        }
    
        public virtual ObjectResult<string> SettingsGorevNo(Nullable<int> tarih, Nullable<int> depoID)
        {
            var tarihParameter = tarih.HasValue ?
                new ObjectParameter("tarih", tarih) :
                new ObjectParameter("tarih", typeof(int));
    
            var depoIDParameter = depoID.HasValue ?
                new ObjectParameter("DepoID", depoID) :
                new ObjectParameter("DepoID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("WMSEntities.SettingsGorevNo", tarihParameter, depoIDParameter);
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
    
        public virtual ObjectResult<MenuRolGetir_Result> MenuRolGetir(Nullable<int> menuID)
        {
            var menuIDParameter = menuID.HasValue ?
                new ObjectParameter("MenuID", menuID) :
                new ObjectParameter("MenuID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MenuRolGetir_Result>("WMSEntities.MenuRolGetir", menuIDParameter);
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
    
        public virtual ObjectResult<GetUserPermsFor_Result> GetUserPermsFor(Nullable<int> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetUserPermsFor_Result>("WMSEntities.GetUserPermsFor", userIDParameter);
        }
    
        public virtual ObjectResult<GetHomeSummary_Result> GetHomeSummary(string userName, Nullable<int> userID)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetHomeSummary_Result>("WMSEntities.GetHomeSummary", userNameParameter, userIDParameter);
        }
    
        public virtual ObjectResult<GetMenuRoles_Result> GetMenuRoles()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetMenuRoles_Result>("WMSEntities.GetMenuRoles");
        }
    
        public virtual ObjectResult<GetRolePermsFor_Result> GetRolePermsFor(string roleName, string group)
        {
            var roleNameParameter = roleName != null ?
                new ObjectParameter("RoleName", roleName) :
                new ObjectParameter("RoleName", typeof(string));
    
            var groupParameter = group != null ?
                new ObjectParameter("Group", group) :
                new ObjectParameter("Group", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRolePermsFor_Result>("WMSEntities.GetRolePermsFor", roleNameParameter, groupParameter);
        }
    
        public virtual ObjectResult<MenuGetirici_Result> MenuGetirici(Nullable<int> webSiteTipiID, Nullable<int> menuYeriID, string roleName, Nullable<short> ustMenuID, string url)
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
    
            var urlParameter = url != null ?
                new ObjectParameter("url", url) :
                new ObjectParameter("url", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MenuGetirici_Result>("WMSEntities.MenuGetirici", webSiteTipiIDParameter, menuYeriIDParameter, roleNameParameter, ustMenuIDParameter, urlParameter);
        }
    
        public virtual int LogLogins(string userName, string ipAddress, Nullable<bool> loggedIn, string comment)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var ipAddressParameter = ipAddress != null ?
                new ObjectParameter("IpAddress", ipAddress) :
                new ObjectParameter("IpAddress", typeof(string));
    
            var loggedInParameter = loggedIn.HasValue ?
                new ObjectParameter("LoggedIn", loggedIn) :
                new ObjectParameter("LoggedIn", typeof(bool));
    
            var commentParameter = comment != null ?
                new ObjectParameter("Comment", comment) :
                new ObjectParameter("Comment", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WMSEntities.LogLogins", userNameParameter, ipAddressParameter, loggedInParameter, commentParameter);
        }
    
        public virtual int UpdateUserDevice(Nullable<int> userID, string device)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var deviceParameter = device != null ?
                new ObjectParameter("Device", device) :
                new ObjectParameter("Device", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WMSEntities.UpdateUserDevice", userIDParameter, deviceParameter);
        }
    
        public virtual int LogActions(string site, string area, string controller, string action, Nullable<int> type, Nullable<int> selectedID, string request, string details, string username, string ipAddress)
        {
            var siteParameter = site != null ?
                new ObjectParameter("Site", site) :
                new ObjectParameter("Site", typeof(string));
    
            var areaParameter = area != null ?
                new ObjectParameter("Area", area) :
                new ObjectParameter("Area", typeof(string));
    
            var controllerParameter = controller != null ?
                new ObjectParameter("Controller", controller) :
                new ObjectParameter("Controller", typeof(string));
    
            var actionParameter = action != null ?
                new ObjectParameter("Action", action) :
                new ObjectParameter("Action", typeof(string));
    
            var typeParameter = type.HasValue ?
                new ObjectParameter("Type", type) :
                new ObjectParameter("Type", typeof(int));
    
            var selectedIDParameter = selectedID.HasValue ?
                new ObjectParameter("SelectedID", selectedID) :
                new ObjectParameter("SelectedID", typeof(int));
    
            var requestParameter = request != null ?
                new ObjectParameter("Request", request) :
                new ObjectParameter("Request", typeof(string));
    
            var detailsParameter = details != null ?
                new ObjectParameter("Details", details) :
                new ObjectParameter("Details", typeof(string));
    
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var ipAddressParameter = ipAddress != null ?
                new ObjectParameter("IpAddress", ipAddress) :
                new ObjectParameter("IpAddress", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WMSEntities.LogActions", siteParameter, areaParameter, controllerParameter, actionParameter, typeParameter, selectedIDParameter, requestParameter, detailsParameter, usernameParameter, ipAddressParameter);
        }
    
        public virtual ObjectResult<string> GetKynkEvrakNosForGorev(Nullable<int> gorevID)
        {
            var gorevIDParameter = gorevID.HasValue ?
                new ObjectParameter("GorevID", gorevID) :
                new ObjectParameter("GorevID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("WMSEntities.GetKynkEvrakNosForGorev", gorevIDParameter);
        }
    
        public virtual ObjectResult<string> GetKynkTarihsForGorev(Nullable<int> gorevID)
        {
            var gorevIDParameter = gorevID.HasValue ?
                new ObjectParameter("GorevID", gorevID) :
                new ObjectParameter("GorevID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("WMSEntities.GetKynkTarihsForGorev", gorevIDParameter);
        }
    
        public virtual int UpdateGorevDurum(Nullable<int> tarih, Nullable<int> saat, Nullable<int> irsID)
        {
            var tarihParameter = tarih.HasValue ?
                new ObjectParameter("Tarih", tarih) :
                new ObjectParameter("Tarih", typeof(int));
    
            var saatParameter = saat.HasValue ?
                new ObjectParameter("Saat", saat) :
                new ObjectParameter("Saat", typeof(int));
    
            var irsIDParameter = irsID.HasValue ?
                new ObjectParameter("IrsID", irsID) :
                new ObjectParameter("IrsID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WMSEntities.UpdateGorevDurum", tarihParameter, saatParameter, irsIDParameter);
        }
    
        public virtual int SettingsPaketNo(Nullable<int> depoID, Nullable<int> gorevID, string username, Nullable<int> tarih)
        {
            var depoIDParameter = depoID.HasValue ?
                new ObjectParameter("DepoID", depoID) :
                new ObjectParameter("DepoID", typeof(int));
    
            var gorevIDParameter = gorevID.HasValue ?
                new ObjectParameter("GorevID", gorevID) :
                new ObjectParameter("GorevID", typeof(int));
    
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var tarihParameter = tarih.HasValue ?
                new ObjectParameter("Tarih", tarih) :
                new ObjectParameter("Tarih", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WMSEntities.SettingsPaketNo", depoIDParameter, gorevIDParameter, usernameParameter, tarihParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> TerminalFinishGorev(Nullable<int> gorevID, Nullable<int> irsaliyeID, string yeniGorevNo, Nullable<int> bitisTarihi, Nullable<int> bitisSaati, string kullanici, string linkEvrakNo, Nullable<int> görevTipiID, Nullable<int> yeniGorevTipiID)
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
    
            var kullaniciParameter = kullanici != null ?
                new ObjectParameter("Kullanici", kullanici) :
                new ObjectParameter("Kullanici", typeof(string));
    
            var linkEvrakNoParameter = linkEvrakNo != null ?
                new ObjectParameter("LinkEvrakNo", linkEvrakNo) :
                new ObjectParameter("LinkEvrakNo", typeof(string));
    
            var görevTipiIDParameter = görevTipiID.HasValue ?
                new ObjectParameter("GörevTipiID", görevTipiID) :
                new ObjectParameter("GörevTipiID", typeof(int));
    
            var yeniGorevTipiIDParameter = yeniGorevTipiID.HasValue ?
                new ObjectParameter("YeniGorevTipiID", yeniGorevTipiID) :
                new ObjectParameter("YeniGorevTipiID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("WMSEntities.TerminalFinishGorev", gorevIDParameter, irsaliyeIDParameter, yeniGorevNoParameter, bitisTarihiParameter, bitisSaatiParameter, kullaniciParameter, linkEvrakNoParameter, görevTipiIDParameter, yeniGorevTipiIDParameter);
        }
    }
}
