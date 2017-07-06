using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Web.Services;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Entity.Mysql;

namespace Wms12m
{
    /// <summary>
    /// Summary description for Terminal
    /// </summary>
    [WebService(Namespace = "http://www.12mconsulting.com.tr/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class Terminal : BaseService
    {
        /// <summary>
        /// login işlemleri
        /// </summary>
        [WebMethod]
        public Login LoginKontrol(string userID, string sifre, string AuthGiven)
        {
            if (AuthGiven.Cozumle() != AuthPass) return new Login() { ID = 0, AdSoyad = "Yetkisiz giriş!" };
            //new user
            var user = new User() { Kod = userID.Left(5), Sifre = sifre };
            //log in actions
            var person = new Persons();
            var result = person.Login(user, "Terminal");
            //check result
            if (result.Id > 0)
            {
                if (db.Users.Where(m => m.ID == result.Id).FirstOrDefault().UserDetail == null)
                {
                    return new Login() { ID = 0, AdSoyad = "Depoya ait bir yetkiniz yok!" };
                }
                else
                    try
                    {
                        db.LogLogins(userID, "Terminal", true, "");
                        var tbl = db.Users.Where(m => m.ID == result.Id).Select(m => new Login { ID = m.ID, Kod = m.Kod, Guid = m.Guid.ToString(), AdSoyad = m.AdSoyad, DepoKodu = m.UserDetail.Depo.DepoKodu, DepoID = m.UserDetail.Depo.ID }).FirstOrDefault();
                        tbl.Guid = tbl.Guid.Sifrele();
                        return tbl;
                    }
                    catch (Exception ex)
                    {
                        Logger(userID, "Terminal", ex, "Service/Terminal/Login");
                        db.LogLogins(userID, "Terminal", false, result.Message);
                    }
            }
            else
                db.LogLogins(userID, "Terminal", false, result.Message);
            return new Login() { ID = 0, AdSoyad = "Hatalı Kullanıcı adı ve şifre!" };
        }
        /// <summary>
        /// login işlemleri
        /// </summary>
        [WebMethod]
        public Login LoginKontrol2(string barkod, string AuthGiven)
        {
            if (AuthGiven.Cozumle() != AuthPass) return new Login() { ID = 0, AdSoyad = "Yetkisiz giriş!" };
            string guid = barkod.Left(8).ToLower();
            int userID = barkod.Remove(0, 8).ToInt32();
            var tbl = db.Users.Where(m => m.ID == userID).FirstOrDefault();
            if (tbl == null)
            {
                db.LogLogins(barkod, "Terminal", false, "Hatalı barkod");
                return new Login() { ID = 0, AdSoyad = "Hatalı barkod" };
            }
            if (tbl.Guid.ToString().ToLower().Right(8) != guid)
            {
                db.LogLogins(barkod, "Terminal", false, "Hatalı barkod");
                return new Login() { ID = 0, AdSoyad = "Hatalı barkod" };
            }
            if (tbl.UserDetail == null)
            {
                db.LogLogins(barkod, "Terminal", false, "Depoya ait bir yetkiniz yok");
                return new Login() { ID = 0, AdSoyad = "Depoya ait bir yetkiniz yok" };
            }
            else
            {
                db.LogLogins(tbl.Kod, "Terminal", true, "");
                db.UpdateUserDevice(tbl.ID, "Terminal");
                return new Login { ID = tbl.ID, Kod = tbl.Kod, Guid = tbl.Guid.Sifrele(), AdSoyad = tbl.AdSoyad, DepoKodu = tbl.UserDetail.Depo.DepoKodu, DepoID = tbl.UserDetail.Depo.ID };
            }
        }
        /// <summary>
        /// depoya ait görev özetini getirir
        /// </summary>
        [WebMethod]
        public List<GorevOzet> GetGorevOzet(int ID, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new List<GorevOzet>();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new List<GorevOzet>();
            //return
            string sql = string.Format("SELECT ComboItem_Name.ID, ComboItem_Name.Name AS Ad, COUNT(wms.Gorev.ID) AS Sayi " +
                "FROM wms.Gorev INNER JOIN ComboItem_Name ON wms.Gorev.GorevTipiID = ComboItem_Name.ID LEFT OUTER JOIN wms.GorevUsers ON wms.Gorev.ID = wms.GorevUsers.GorevID " +
                "WHERE (wms.Gorev.DepoID = {0}) AND (wms.GorevUsers.UserID = {1} OR wms.GorevUsers.UserID IS NULL) AND (wms.GorevUsers.BitisTarihi IS NULL) AND (wms.Gorev.DurumID = 9) " +
                "GROUP BY ComboItem_Name.Name, ComboItem_Name.ID", ID, KullID);
            return db.Database.SqlQuery<GorevOzet>(sql).ToList();
        }
        /// <summary>
        /// irsaliyeleri getir
        /// </summary>
        [WebMethod]
        public List<Tip_IRS> GetIrsaliyeList(int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new List<Tip_IRS>();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new List<Tip_IRS>();
            //return
            return db.IRS.Select(m => new Tip_IRS { ID = m.ID, DepoID = m.Depo.DepoKodu, EvrakNo = m.EvrakNo, HesapKodu = m.HesapKodu, SirketKod = m.SirketKod, Tarih = m.Tarih.ToString(), TeslimCHK = m.TeslimCHK, Unvan = "" }).ToList();
        }
        /// <summary>
        /// seçili irsaliyenin bilgileri
        /// </summary>
        [WebMethod]
        public Tip_IRS GetIrsaliye(int GorevID, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Tip_IRS();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new Tip_IRS();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            if (mGorev.IsNull() || mGorev.IR == null)
                return new Tip_IRS();
            //return
            string sql = string.Format("SELECT MIN(IRS.ID) as ID, wms.Depo.DepoKodu AS DepoID, IRS.HesapKodu, wms.fnFormatDateFromInt(IRS.Tarih) AS Tarih, " +
                "(SELECT Unvan1 + ' ' + Unvan2 AS Expr1 FROM FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) WHERE (HesapKodu = IRS.HesapKodu)) AS Unvan, " +
                "(SELECT wms.IRS.EvrakNo + '  ' FROM wms.IRS WITH(nolock) INNER JOIN wms.GorevIRS WITH(nolock) ON wms.IRS.ID = wms.GorevIRS.IrsaliyeID WHERE (wms.GorevIRS.GorevID = {1}) FOR XML PATH('')) as EvrakNo " +
                "FROM wms.IRS AS IRS WITH(nolock) INNER JOIN wms.Depo WITH(nolock) ON IRS.DepoID = wms.Depo.ID INNER JOIN wms.GorevIRS WITH(nolock) ON IRS.ID = wms.GorevIRS.IrsaliyeID " +
                "WHERE (wms.GorevIRS.GorevID = {1}) " +
                "GROUP BY wms.Depo.DepoKodu, IRS.HesapKodu, wms.fnFormatDateFromInt(IRS.Tarih)", mGorev.IR.SirketKod, mGorev.ID);
            return db.Database.SqlQuery<Tip_IRS>(sql).FirstOrDefault();
        }
        /// <summary>
        /// görev listelerini filtreye göre getirir
        /// </summary>
        [WebMethod]
        public List<Tip_GOREV> GetGorevList(int gorevli, int durum, int gorevtipi, int DepoID, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new List<Tip_GOREV>();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new List<Tip_GOREV>();
            //return
            string gtipi = db.ComboItem_Name.Where(m => m.ID == gorevtipi).Select(m => m.Name).FirstOrDefault();
            gtipi = "Terminal" + gtipi.Replace(" ", "");
            var kullanici = db.Users.Where(m => m.ID == KullID).FirstOrDefault();
            var yetkikontrol = db.GetPermissionFor(KullID, kullanici.RoleName, gtipi, "WMS", "Reading").FirstOrDefault().Value;
            if (yetkikontrol == true)
            {
                string sql = string.Format("SELECT Gorev.ID, ISNULL(Gorev.IrsaliyeID, 0) as IrsaliyeID, wms.fnFormatDateFromInt(Gorev.OlusturmaTarihi) AS OlusturmaTarihi, Gorev.Bilgi, Gorev.Aciklama, Gorev.GorevNo, ISNULL(wms.IRS.EvrakNo, '') as EvrakNo, wms.Depo.DepoKodu, Gorev.Atayan, Gorev.Gorevli, ComboItem_Name.[Name] AS Durum " +
                                        "FROM wms.Gorev WITH (nolock) INNER JOIN wms.Depo WITH (nolock) ON Gorev.DepoID = wms.Depo.ID INNER JOIN ComboItem_Name WITH (nolock) ON Gorev.DurumID = ComboItem_Name.ID LEFT OUTER JOIN wms.GorevUsers WITH (nolock) ON wms.Gorev.ID = wms.GorevUsers.GorevID LEFT OUTER JOIN usr.Users WITH (nolock) ON Gorev.Gorevli = usr.Users.Kod LEFT OUTER JOIN wms.IRS WITH (nolock) ON Gorev.IrsaliyeID = wms.IRS.ID " +
                                        "WHERE (wms.GorevUsers.BitisTarihi IS NULL) AND (wms.Depo.ID = {3}) and case when ({0}>0) then case when (Gorev.GorevTipiID = {0}) then 1 else 0 end else 0 end = 1 AND (CASE WHEN ({1} > 0) THEN CASE WHEN (wms.GorevUsers.UserID = {1} OR wms.GorevUsers.UserID IS NULL) THEN 1 ELSE 0 END ELSE 1 END = 1) AND case when ({1}>0) then case when (usr.Users.ID = {1}) then 1 else 0 end else 1 end = 1 AND  case when ({2}>0) then case when (Gorev.DurumID = {2}) then 1 else 0 end else 1 end = 1", gorevtipi, gorevli, durum, DepoID);
                return db.Database.SqlQuery<Tip_GOREV>(sql).ToList();
            }
            return new List<Tip_GOREV>();
        }
        /// <summary>
        /// bir şirkete ait kullanıcıları getirir
        /// </summary>
        [WebMethod]
        public List<GetGorevlis_Result> GetUsers(int DepoID, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new List<GetGorevlis_Result>();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new List<GetGorevlis_Result>();
            //return
            return db.GetGorevlis(DepoID).ToList();
        }
        /// <summary>
        /// durumları listeler
        /// </summary>
        [WebMethod]
        public List<Durum> GetDurums(int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new List<Durum>();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new List<Durum>();
            //return
            return db.ComboItem_Name.Where(m => m.Visible == true).Where(m => m.ComboID == 2).Select(m => new Durum { ID = m.ID, Name = m.Name }).ToList();
        }
        /// <summary>
        /// paket tiplerini listeler
        /// </summary>
        [WebMethod]
        public List<Durum> GetPaketTip(int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new List<Durum>();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new List<Durum>();
            //return
            return db.ComboItem_Name.Where(m => m.Visible == true).Where(m => m.ComboID == 12).Select(m => new Durum { ID = m.ID, Name = m.Name }).ToList();
        }
        /// <summary>
        /// malzemeleri getir
        /// </summary>
        [WebMethod]
        public List<Tip_STI> GetMalzemes(int GorevID, int KullID, bool devamMi, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new List<Tip_STI>();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new List<Tip_STI>();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            if (mGorev.IsNull())
                return new List<Tip_STI>();
            var tu = mGorev.GorevUsers.Where(m => m.UserName == tbl.Kod).FirstOrDefault();
            if (tu != null)
                if (tu.BitisTarihi != null)
                    return new List<Tip_STI>();
            //return
            string sql = "", sql2 = "";
            if (mGorev.GorevTipiID == ComboItems.SiparişTopla.ToInt32() || mGorev.GorevTipiID == ComboItems.TransferÇıkış.ToInt32() || mGorev.GorevTipiID == ComboItems.KontrolSayım.ToInt32())
            {
                var dbs = db.GetSirketDBs();
                foreach (var item in dbs)
                {
                    if (sql != "")
                    {
                        sql += " UNION "; sql2 += " UNION ";
                    }
                    sql += string.Format("SELECT MalAdi FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE (MalKodu = wms.GorevYer.MalKodu)", item);
                    sql2 += string.Format("SELECT case when Barkod1='' then Barkod2 else Barkod1 end Barkod FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE (MalKodu = wms.GorevYer.MalKodu)", item);
                }
                sql = "(Select TOP(1) MalAdi from (" + sql + ") t WHERE MalAdi <> '')";
                sql2 = "(Select TOP(1) Barkod from (" + sql2 + ") t WHERE Barkod <> '')";
                string sqltmp = ""; if (devamMi == true && mGorev.GorevTipiID != ComboItems.KontrolSayım.ToInt32()) sqltmp += "AND wms.GorevYer.Miktar>ISNULL(wms.GorevYer.YerlestirmeMiktari,0) ";
                sql = string.Format("SELECT wms.GorevYer.ID, wms.GorevYer.MalKodu, wms.GorevYer.Miktar, wms.GorevYer.Birim, wms.Yer.HucreAd AS Raf, ISNULL(wms.GorevYer.YerlestirmeMiktari,0) as YerlestirmeMiktari, " +
                                    "ISNULL(" + sql + ",'') AS MalAdi, ISNULL(" + sql2 + ",'') AS Barkod " +
                                    "FROM wms.GorevYer WITH(nolock) INNER JOIN wms.Yer WITH(nolock) ON wms.GorevYer.YerID = wms.Yer.ID " +
                                    "WHERE (wms.GorevYer.GorevID = {0}) {1}" +
                                    "ORDER BY wms.GorevYer.Sira", GorevID, sqltmp);
            }
            else if (mGorev.GorevTipiID == ComboItems.TransferGiriş.ToInt32() && mGorev.Transfers.Count == 0)
            {
                sql = string.Format("SELECT wms.IRS_Detay.ID, wms.IRS.ID as irsID, wms.IRS_Detay.MalKodu, wms.IRS_Detay.Miktar, wms.IRS_Detay.Birim, ISNULL(wms.IRS_Detay.OkutulanMiktar, 0) AS OkutulanMiktar, wms.Yer_Log.HucreAd AS Raf, SUM(wms.Yer_Log.Miktar) as Raf, SUM(wms.Yer_Log.Miktar) AS YerMiktar, " +
                                    "ISNULL((SELECT MalAdi FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE (MalKodu = wms.IRS_Detay.MalKodu)),'') AS MalAdi, " +
                                    "ISNULL((SELECT case when Barkod1='' then Barkod2 else Barkod1 end Barkod FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE (MalKodu = wms.IRS_Detay.MalKodu)),'') AS Barkod " +
                                    "FROM wms.IRS_Detay WITH (nolock) INNER JOIN wms.IRS WITH (nolock) ON wms.IRS_Detay.IrsaliyeID = wms.IRS.ID INNER JOIN wms.GorevIRS WITH (nolock) ON wms.IRS.ID = wms.GorevIRS.IrsaliyeID LEFT OUTER JOIN wms.Yer_Log WITH (nolock) ON wms.IRS_Detay.KynkSiparisID = wms.Yer_Log.KatID AND wms.IRS_Detay.MalKodu = wms.Yer_Log.MalKodu " +
                                    "WHERE (wms.GorevIRS.GorevID = {1}) AND (wms.Yer_Log.GC = 0 OR wms.Yer_Log.GC IS NULL) " +
                                    "GROUP BY wms.IRS_Detay.ID, wms.IRS.ID, wms.IRS_Detay.MalKodu, wms.IRS_Detay.Miktar, wms.IRS_Detay.Birim, ISNULL(wms.IRS_Detay.OkutulanMiktar, 0), ISNULL(wms.IRS_Detay.YerlestirmeMiktari, 0), wms.Yer_Log.HucreAd ", mGorev.IR.SirketKod, mGorev.ID, mGorev.DepoID);
                if (devamMi == true)
                    sql += "HAVING (wms.IRS_Detay.Miktar > ISNULL(YerlestirmeMiktari,0))";
            }
            else
            {
                sql = (mGorev.GorevTipiID == ComboItems.Paketle.ToInt32() || mGorev.GorevTipiID == ComboItems.BarkodHazırla.ToInt32() || mGorev.GorevTipiID == ComboItems.Sevket.ToInt32()) ? "(wms.Yer_Log.GC = 1) " : "(wms.Yer_Log.GC = 0 OR wms.Yer_Log.GC IS NULL) ";
                sql = string.Format("SELECT wms.IRS_Detay.ID, wms.IRS.ID as irsID, wms.IRS_Detay.MalKodu, wms.IRS_Detay.Miktar, wms.IRS_Detay.Birim, ISNULL(wms.IRS_Detay.OkutulanMiktar, 0) AS OkutulanMiktar, wms.Yer_Log.HucreAd AS Raf, SUM(wms.Yer_Log.Miktar) as Raf, SUM(wms.Yer_Log.Miktar) AS YerMiktar, " +
                                    "ISNULL((SELECT MalAdi FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE (MalKodu = wms.IRS_Detay.MalKodu)),'') AS MalAdi, " +
                                    "ISNULL((SELECT case when Barkod1='' then Barkod2 else Barkod1 end Barkod FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE (MalKodu = wms.IRS_Detay.MalKodu)),'') AS Barkod " +
                                    "FROM wms.IRS_Detay WITH (nolock) INNER JOIN wms.IRS WITH (nolock) ON wms.IRS_Detay.IrsaliyeID = wms.IRS.ID INNER JOIN wms.GorevIRS WITH (nolock) ON wms.IRS.ID = wms.GorevIRS.IrsaliyeID LEFT OUTER JOIN wms.Yer_Log WITH (nolock) ON wms.IRS_Detay.IrsaliyeID = wms.Yer_Log.IrsaliyeID AND wms.IRS_Detay.MalKodu = wms.Yer_Log.MalKodu " +
                                    "WHERE (wms.GorevIRS.GorevID = {1}) AND " + sql +
                                    "GROUP BY wms.IRS_Detay.ID, wms.IRS.ID, wms.IRS_Detay.MalKodu, wms.IRS_Detay.Miktar, wms.IRS_Detay.Birim, ISNULL(wms.IRS_Detay.OkutulanMiktar, 0), ISNULL(wms.IRS_Detay.YerlestirmeMiktari, 0), wms.Yer_Log.HucreAd ", mGorev.IR.SirketKod, mGorev.ID, mGorev.DepoID);
                if (devamMi == true)
                    if (mGorev.GorevTipiID == ComboItems.MalKabul.ToInt32() || mGorev.GorevTipiID == ComboItems.Paketle.ToInt32() || mGorev.GorevTipiID == ComboItems.Sevket.ToInt32())
                        sql += "HAVING (wms.IRS_Detay.Miktar > ISNULL(OkutulanMiktar,0))";
                    else
                        sql += "HAVING (wms.IRS_Detay.Miktar > ISNULL(YerlestirmeMiktari,0))";
            }
            return db.Database.SqlQuery<Tip_STI>(sql).ToList();
        }
        /// <summary>
        /// malzemeyi barkoda göre bulur
        /// </summary>
        [WebMethod]
        public Tip_Malzeme GetMalzemeFromBarcode(string malkodu, string barkod, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Tip_Malzeme();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new Tip_Malzeme();
            //return
            string sql = "";
            var dbs = db.GetSirketDBs();
            foreach (var item in dbs)
            {
                if (sql != "") sql += " UNION ";
                sql += string.Format("SELECT MalKodu, MalAdi, Birim1, case when Barkod1='' then Barkod2 else Barkod1 end Barkod FROM FINSAT6{0}.FINSAT6{0}.STK WHERE ", item);
                if (malkodu != "") sql += string.Format("(MalKodu = '{0}')", malkodu);
                else sql += string.Format("(BarKod1 = '{0}') OR (BarKod2 = '{0}')", barkod);
            }
            sql = "SELECT MalKodu, MalAdi, Birim1 as Birim, Barkod from (" + sql + ") as t where MalAdi<>''";
            return db.Database.SqlQuery<Tip_Malzeme>(sql).FirstOrDefault();
        }
        /// <summary>
        /// mal kabul kayıt işlemleri
        /// </summary>
        [WebMethod]
        public Result Mal_Kabul(List<frmMalKabul> StiList, int GorevID, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            int durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "İrsaliye bulunamadı!");
            //return
            //add to gorev user table
            var tbl = db.GorevUsers.Where(m => m.GorevID == GorevID && m.UserName == tblx.Kod).FirstOrDefault();
            if (tbl == null)
            {
                tbl = new GorevUser()
                {
                    UserName = tblx.Kod,
                    GorevID = GorevID,
                    BaslamaTarihi = DateTime.Today.ToOADateInt()
                };
                db.GorevUsers.Add(tbl);
                db.SaveChanges();
            }
            using (var stok = new IrsaliyeDetay())
            {
                foreach (var item in StiList)
                {
                    var tmp = stok.Detail(item.ID);
                    tmp.OkutulanMiktar = item.OkutulanMiktar;
                    try
                    {
                        stok.Operation(tmp);
                    }
                    catch (Exception ex)
                    {
                        Logger(KullID.ToString(), "Terminal", ex, "Service/Terminal/Mal_Kabul");
                    }
                }
            }
            return new Result(true);
        }
        /// <summary>
        /// mal kabul için miktar kontrol
        /// </summary>
        [WebMethod]
        public Result MalKabul_GorevKontrol(int GorevID, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            int durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "İrsaliye bulunamadı !");
            //return
            string sql = string.Format("SELECT COUNT(wms.IRS_Detay.OkutulanMiktar) as Bitmeyen " +
                "FROM wms.GorevIRS INNER JOIN wms.IRS_Detay ON wms.GorevIRS.IrsaliyeID = wms.IRS_Detay.IrsaliyeID " +
                "WHERE(wms.GorevIRS.GorevID = {0}) AND(wms.IRS_Detay.OkutulanMiktar IS NULL OR wms.IRS_Detay.OkutulanMiktar <> wms.IRS_Detay.Miktar)", mGorev.ID);
            var tbl = db.Database.SqlQuery<int>(sql).FirstOrDefault();
            if (tbl != 0)
                return new Result(false, -1, "İşlem bitmemiş !");
            return new Result(true);
        }
        /// <summary>
        /// mal kabul onay
        /// </summary>
        [WebMethod]
        public Result MalKabul_GoreviTamamla(int GorevID, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            int durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "İrsaliye bulunamadı !");
            //variables
            string gorevNo = db.SettingsGorevNo(DateTime.Today.ToOADateInt(), mGorev.DepoID).FirstOrDefault();
            var kull = db.Users.Where(m => m.ID == KullID).Select(m => m.Kod).FirstOrDefault();
            Finsat finsat = new Finsat(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, mGorev.IR.SirketKod);
            //loop iraliyes
            foreach (var item in mGorev.IRS.Where(m => m.Onay == false))
            {
                string sql = string.Format("SELECT EvrakNo FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo = '{1}') AND (KynkEvrakTip = 3) AND (Chk = {2})", item.SirketKod, item.EvrakNo, item.HesapKodu);
                var sti = db.Database.SqlQuery<string>(sql).FirstOrDefault();
                if (sti != null)
                    return new Result(false, item.EvrakNo + " nolu evrak daha önce kullanılmış");
                //send to finsat
                var sonuc = finsat.MalKabul(item, KullID);
                if (sonuc.Status == true)
                {
                    //finish
                    db.TerminalFinishGorev(GorevID, item.ID, gorevNo, DateTime.Today.ToOADateInt(), DateTime.Now.ToOaTime(), kull, "", ComboItems.MalKabul.ToInt32(), ComboItems.RafaKaldır.ToInt32()).FirstOrDefault();
                    LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "MalKabul_GoreviTamamla", ComboItems.alDüzenle, GorevID, "MalKabul => RafaKaldır");
                    //add to stok
                    var KatID = db.GetHucreKatID(item.DepoID, "R-ZR-V").FirstOrDefault();
                    if (KatID != null)
                    {
                        var list = db.GetIrsDetayfromGorev(GorevID).ToList();
                        foreach (var item2 in list)
                        {
                            //yerleştirme kaydı yapılır
                            var stok = new Yerlestirme();
                            var tmp2 = stok.Detail(KatID.Value, item2.MalKodu, item2.Birim);
                            if (tmp2 == null)
                            {
                                tmp2 = new Yer()
                                {
                                    KatID = KatID.Value,
                                    MalKodu = item2.MalKodu,
                                    Birim = item2.Birim,
                                    Miktar = item2.Miktar.Value
                                };
                                stok.Insert(tmp2, 0, KullID);
                            }
                            else
                            {
                                tmp2.Miktar += item2.Miktar.Value;
                                stok.Update(tmp2, 0, KullID, false, item2.Miktar.Value);
                            }
                        }
                    }
                    //add to mysql
                    if (db.Settings.FirstOrDefault().KabloSiparisMySql == true)
                    {
                        var listedb = db.GetSirketDBs().ToList();
                        sql = "";
                        foreach (var itemd in listedb)
                        {
                            if (sql != "") sql += " UNION ";
                            sql = String.Format("SELECT FINSAT6{0}.FINSAT6{0}.STK.MalAdi4, FINSAT6{0}.FINSAT6{0}.STK.Nesne2, FINSAT6{0}.FINSAT6{0}.STK.Kod15, wms.IRS_Detay.Miktar, wms.IRS_Detay.MakaraNo " +
                                                        "FROM wms.IRS_Detay INNER JOIN FINSAT6{0}.FINSAT6{0}.STK ON wms.IRS_Detay.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                                                        "WHERE (FINSAT6{0}.FINSAT6{0}.STK.Kod1 = 'KKABLO') AND (wms.IRS_Detay.IrsaliyeID = {1}) AND (wms.IRS_Detay.Birim = FINSAT6{0}.FINSAT6{0}.STK.Birim1 OR wms.IRS_Detay.Birim = FINSAT6{0}.FINSAT6{0}.STK.Birim2)", itemd, mGorev.IrsaliyeID);
                        }
                        sql = "SELECT * from (" + sql + ") t";
                        var stks = db.Database.SqlQuery<frmCableStk>(sql).ToList();
                        if (stks.Count > 0)
                        {
                            try
                            {
                                using (KabloEntities dbx = new KabloEntities())
                                {
                                    string depo = dbx.depoes.Where(m => m.id == mGorev.Depo.KabloDepoID).Select(m => m.depo1).FirstOrDefault();
                                    foreach (var itemx in stks)
                                    {
                                        //sid bul
                                        int sid = dbx.indices.Where(m => m.cins == itemx.Nesne2 && m.kesit == itemx.Kod15).Select(m => m.id).FirstOrDefault();
                                        //stoğa kaydet
                                        stok tbls = new stok()
                                        {
                                            marka = itemx.MalAdi4,
                                            cins = itemx.Nesne2,
                                            kesit = itemx.Kod15,
                                            sid = sid,
                                            depo = depo,
                                            renk = "",
                                            makara = "KAPALI",
                                            rezerve = "0",
                                            sure = new TimeSpan(),
                                            tarih = DateTime.Now,
                                            tip = "",
                                            rmiktar = 0,
                                            miktar = itemx.Miktar,
                                            makarano = itemx.MakaraNo
                                        };
                                        dbx.stoks.Add(tbls);
                                        dbx.SaveChanges();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger(kull, "Terminal", ex, "Service/Terminal/MalKabul_GoreviTamamla");
                                return new Result(false, "Kablo kaydı hariç her şey tamamlandı!");
                            }
                        }
                    }
                }
                else
                    return new Result(false, sonuc.Message);
            }
            //return if all true: tüm israliyeler biterse görevi kapat
            //görev user ve görev tablosu
            var tbl2 = db.GorevUsers.Where(m => m.GorevID == GorevID && m.UserName == tblx.Kod).FirstOrDefault();
            tbl2.BitisTarihi = DateTime.Today.ToOADateInt();
            mGorev.DurumID = ComboItems.Tamamlanan.ToInt32();
            db.SaveChanges();
            return new Result(true);
        }
        /// <summary>
        /// rafa yerleştir
        /// </summary>
        [WebMethod]
        public Result Rafa_Kaldir(List<frmYerlesme> YerlestirmeList, int KullID, int GorevID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            int durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "İrsaliye bulunamadı !");
            //add to gorev user table
            var tbl = db.GorevUsers.Where(m => m.GorevID == GorevID && m.UserName == tblx.Kod).FirstOrDefault();
            if (tbl == null)
            {
                tbl = new GorevUser()
                {
                    UserName = tblx.Kod,
                    GorevID = GorevID,
                    BaslamaTarihi = DateTime.Today.ToOADateInt()
                };
                db.GorevUsers.Add(tbl);
                db.SaveChanges();
            }
            //loop
            Result _result = new Result(true);
            var Rkat = db.GetHucreKatID(mGorev.IR.DepoID, "R-ZR-V").FirstOrDefault();
            foreach (var item in YerlestirmeList)
            {
                //hücre adından kat id bulunur
                var kat = db.GetHucreKatID(item.DepoID, item.RafNo).FirstOrDefault();
                if (kat != null)
                {
                    //irs detay tablosu güncellenir
                    var irsdetay = new IrsaliyeDetay();
                    var tmp = irsdetay.Detail(item.IrsDetayID);
                    if (tmp.Miktar >= ((tmp.YerlestirmeMiktari ?? 0) + item.Miktar))
                    {
                        if (tmp.YerlestirmeMiktari == null) tmp.YerlestirmeMiktari = item.Miktar;
                        else tmp.YerlestirmeMiktari += item.Miktar;
                        //irs detay kayıt
                        irsdetay.Operation(tmp);
                        var stok = new Yerlestirme();
                        //rezervden düşürülür
                        var tmp2 = stok.Detail(Rkat.Value, item.MalKodu, item.Birim);
                        tmp2.Miktar -= item.Miktar;
                        stok.Update(tmp2, item.IrsID, KullID, true, item.Miktar);
                        //yerleştirme kaydı yapılır
                        tmp2 = stok.Detail(kat.Value, item.MalKodu, item.Birim);
                        if (tmp2 == null)
                        {
                            tmp2 = new Yer()
                            {
                                KatID = kat.Value,
                                MalKodu = item.MalKodu,
                                Birim = item.Birim,
                                Miktar = item.Miktar
                            };
                            stok.Insert(tmp2, item.IrsID, KullID);
                        }
                        else
                        {
                            tmp2.Miktar += item.Miktar;
                            stok.Update(tmp2, item.IrsID, KullID, false, item.Miktar);
                        }
                    }
                    else
                        _result = new Result(false, item.MalKodu + " için fazla mal yazılmış");
                }
                else
                    _result = new Result(false, item.RafNo + " adlı yer bulunamadı");
            }
            return _result;
        }
        /// <summary>
        /// rafa kaldır görevi tamamlanınca
        /// </summary>
        [WebMethod]
        public Result RafaKaldir_GoreviTamamla(int GorevID, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            int durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "İrsaliye bulunamadı !");
            var list = mGorev.IR.IRS_Detay.Where(m => m.IrsaliyeID == mGorev.IrsaliyeID && (m.YerlestirmeMiktari != m.Miktar || m.YerlestirmeMiktari == null)).FirstOrDefault();
            if (list.IsNotNull())
                return new Result(false, "İşlem bitmemiş !");
            //görevi tamamla
            var kull = db.Users.Where(m => m.ID == KullID).Select(m => m.Kod).FirstOrDefault();
            db.TerminalFinishGorev(GorevID, mGorev.IrsaliyeID, "", DateTime.Today.ToOADateInt(), DateTime.Now.ToOaTime(), kull, "", ComboItems.RafaKaldır.ToInt32(), 0).FirstOrDefault();
            LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "RafaKaldir_GoreviTamamla", ComboItems.alDüzenle, GorevID, "RafaKaldır => -");
            //görev user tablosu
            var tbl = db.GorevUsers.Where(m => m.GorevID == GorevID && m.UserName == tblx.Kod).FirstOrDefault();
            tbl.BitisTarihi = DateTime.Today.ToOADateInt();
            db.SaveChanges();
            return new Result(true);
        }
        /// <summary>
        /// raftan indir
        /// </summary>
        [WebMethod]
        public Result Siparis_Topla(List<frmYerlesme> YerlestirmeList, int KullID, int GorevID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            int durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "İrsaliye bulunamadı !");
            //add to gorev user table
            var tbl = db.GorevUsers.Where(m => m.GorevID == GorevID && m.UserName == tblx.Kod).FirstOrDefault();
            if (tbl == null)
            {
                tbl = new GorevUser()
                {
                    UserName = tblx.Kod,
                    GorevID = GorevID,
                    BaslamaTarihi = DateTime.Today.ToOADateInt()
                };
                db.GorevUsers.Add(tbl);
                db.SaveChanges();
            }
            //loop
            Result _result = new Result(true);
            foreach (var item in YerlestirmeList)
            {
                //hücre adından kat id bulunur
                var kat = db.GetHucreKatID(item.DepoID, item.RafNo).FirstOrDefault();
                if (kat != null)
                {
                    //yerleştirme kaydı yapılır
                    var yerlestirme = new Yerlestirme();
                    var tmp2 = yerlestirme.Detail(kat.Value, item.MalKodu, item.Birim);
                    if (tmp2 != null)
                    {
                        var grvYer = db.GorevYers.Where(m => m.YerID == tmp2.ID && m.GorevID == item.GorevID && m.MalKodu == item.MalKodu && m.Birim == item.Birim).FirstOrDefault();
                        if (grvYer == null)
                        {

                        }
                        else
                            if (tmp2.Miktar >= item.Miktar && item.Miktar <= (grvYer.Miktar - (grvYer.YerlestirmeMiktari ?? 0)))
                        {
                            //raftan indirdiğini kaydet
                            grvYer.YerlestirmeMiktari = (grvYer.YerlestirmeMiktari ?? 0) + item.Miktar;
                            db.SaveChanges();
                            //yerlestirme tablosuna kaydet
                            tmp2.Miktar -= item.Miktar;
                            yerlestirme.Update(tmp2, item.IrsID, KullID, true, item.Miktar);
                        }
                        else
                            _result = new Result(false, item.MalKodu + " için fazla mal yazılmış");
                    }
                    else
                        _result = new Result(false, item.MalKodu + " için fazla mal yazılmış");
                }
                else
                    _result = new Result(false, "İrsaliye bulunamadı !");
            }
            return _result;
        }
        /// <summary>
        /// sipariş toplama görevi tamamlma
        /// </summary>
        [WebMethod]
        public Result SiparisTopla_GoreviTamamla(int GorevID, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            int durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "İrsaliye bulunamadı !");
            var tmpYer = mGorev.GorevYers.Where(m => m.GorevID == mGorev.ID && (m.YerlestirmeMiktari < m.Miktar || m.YerlestirmeMiktari == null)).FirstOrDefault();
            if (tmpYer.IsNotNull())
                return new Result(false, "İşlem bitmemiş !");
            //kaydeden bulunur
            var kull = db.Users.Where(m => m.ID == KullID).FirstOrDefault();
            if (kull.UserDetail.SatisFaturaSeri == null || kull.UserDetail.SatisIrsaliyeSeri == null)
                return new Result(false, "Bu kullanıcıya ait seri nolar hatalı !");
            if (kull.UserDetail.SatisFaturaSeri.Value < 1 || kull.UserDetail.SatisFaturaSeri.Value > 199 || kull.UserDetail.SatisIrsaliyeSeri.Value < 1 || kull.UserDetail.SatisIrsaliyeSeri.Value > 199)
                return new Result(false, "Bu kullanıcıya ait seri nolar hatalı !");
            //değiş saat kontrol
            //string sql = string.Format("SELECT wms.GorevIRS.GorevID " +
            //            "FROM wms.IRS_Detay INNER JOIN FINSAT6{0}.FINSAT6{0}.SPI ON wms.IRS_Detay.KynkSiparisID = FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID AND wms.IRS_Detay.KynkSiparisNo = FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo AND wms.IRS_Detay.KynkSiparisSiraNo = FINSAT6{0}.FINSAT6{0}.SPI.SiraNo AND wms.IRS_Detay.KynkSiparisTarih = FINSAT6{0}.FINSAT6{0}.SPI.Tarih AND " +
            //            "wms.IRS_Detay.KynkDegisSaat<> FINSAT6{0}.FINSAT6{0}.SPI.DegisSaat INNER JOIN wms.GorevIRS ON wms.IRS_Detay.IrsaliyeID = wms.GorevIRS.IrsaliyeID " +
            //            "WHERE(wms.GorevIRS.GorevID = {1})", mGorev.IR.SirketKod, GorevID);
            //var kontrol= db.Database.SqlQuery<int>(sql).ToList();
            //if (kontrol.Count != 0)
            //    return new Result(false, "Bu göreve ait siparişler değişmiş, Lütfen bir daha oluşturun");
            //liste getirilir
            string sql = string.Format("SELECT wms.IRS.SirketKod, wms.GorevIRS.IrsaliyeID, wms.IRS.Tarih, wms.IRS.HesapKodu, wms.IRS.TeslimCHK, ISNULL(wms.IRS.ValorGun,0) as ValorGun, wms.IRS.EvrakNo " +
                        "FROM wms.GorevIRS INNER JOIN wms.IRS ON wms.GorevIRS.IrsaliyeID = wms.IRS.ID " +
                        "WHERE (wms.GorevIRS.GorevID = {0}) AND (wms.IRS.Onay = 0) " +
                        "GROUP BY wms.IRS.SirketKod, wms.GorevIRS.IrsaliyeID, wms.IRS.Tarih, wms.IRS.HesapKodu, wms.IRS.TeslimCHK, wms.IRS.ValorGun, wms.IRS.EvrakNo", mGorev.ID);
            var list = db.Database.SqlQuery<STIMax>(sql).ToList();
            int tarih = DateTime.Today.ToOADateInt(), saat = DateTime.Now.ToOaTime();
            foreach (var item in list)
            {
                //muhsebe yılı bulunur
                sql = string.Format("SELECT ISNULL(" +
                                        "(SELECT TOP 1 YEAR(CAST(SDK.Tarih-2 AS DATETIME)) " +
                                        "FROM SOLAR6.DBO.SIR(NOLOCK) INNER JOIN SOLAR6.DBO.SDK(NOLOCK) ON SIR.Kod = SDK.SirketKod AND SDK.Tip = 1 " +
                                        "WHERE SDK.Kod = '{0}' ORDER BY Tarih DESC)" +
                                    ",Year(GETDATE())) as Yil", item.SirketKod);
                int yil = db.Database.SqlQuery<int>(sql).FirstOrDefault();
                //efatura kullanıcısı mı bul
                sql = string.Format("SELECT EFatKullanici FROM FINSAT6{0}.FINSAT6{0}.CHK WHERE (HesapKodu = '{1}')", item.SirketKod, item.HesapKodu);
                var tmp = db.Database.SqlQuery<short>(sql).FirstOrDefault();
                bool efatKullanici = false;
                if (tmp == 1) efatKullanici = true;
                //listedeki her eleman için döngü yapılır
                Finsat finsat = new Finsat(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, item.SirketKod);
                var sonuc = finsat.FaturaKayıt(item.IrsaliyeID, mGorev.Depo.DepoKodu, efatKullanici, item.Tarih, item.HesapKodu, kull.Kod, kull.UserDetail.SatisIrsaliyeSeri.Value, kull.UserDetail.SatisFaturaSeri.Value, yil);
                if (sonuc.Status == true)
                {
                    //update irsaliye
                    string fatNo = sonuc.Message.Left(sonuc.Message.IndexOf(","));
                    string irsNo = sonuc.Message.Substring(sonuc.Message.IndexOf(",") + 1);
                    db.UpdateIrsaliye(item.IrsaliyeID, irsNo, fatNo);
                    //yeni görev
                    string gorevNo = db.SettingsGorevNo(tarih, mGorev.DepoID).FirstOrDefault();
                    string alıcı = item.HesapKodu.GetUnvan(item.SirketKod);
                    var x = db.InsertIrsaliye(item.SirketKod, mGorev.DepoID, gorevNo, irsNo, item.Tarih, "Irs: " + irsNo + " Alıcı: " + alıcı, true, ComboItems.Paketle.ToInt32(), kull.Kod, tarih, saat, item.HesapKodu, item.TeslimChk, item.ValorGun, "").FirstOrDefault();
                    LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "SiparisTopla_GoreviTamamla", ComboItems.alEkle, x.GorevID.Value, "Irs: " + irsNo + " Alıcı: " + alıcı);
                }
                else
                    return new Result(false, sonuc.Message);
            }
            db.TerminalFinishGorev(GorevID, mGorev.IrsaliyeID, "", tarih, saat, kull.Kod, "", ComboItems.SiparişTopla.ToInt32(), 0).FirstOrDefault();
            //görev user tablosu
            var tbl = db.GorevUsers.Where(m => m.GorevID == GorevID && m.UserName == tblx.Kod).FirstOrDefault();
            tbl.BitisTarihi = DateTime.Today.ToOADateInt();
            db.SaveChanges();
            LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "SiparisTopla_GoreviTamamla", ComboItems.alDüzenle, GorevID, "SiparişTopla => -");
            //kablo hareketlere kaydet
            if (db.Settings.FirstOrDefault().KabloSiparisMySql == true)
                try
                {
                    using (KabloEntities dbx = new KabloEntities())
                    {
                        //önce depo adını bul
                        string depo = dbx.depoes.Where(m => m.id == mGorev.Depo.KabloDepoID).Select(m => m.depo1).FirstOrDefault();
                        bool varmi = false;
                        foreach (var item in mGorev.IRS)
                        {
                            foreach (var item2 in item.IRS_Detay)
                            {
                                //istenen stk bilgilerini bul
                                sql = String.Format("SELECT FINSAT6{0}.FINSAT6{0}.STK.MalAdi4, FINSAT6{0}.FINSAT6{0}.STK.Nesne2, FINSAT6{0}.FINSAT6{0}.STK.Kod15 " +
                                                      "FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) " +
                                                      "WHERE (FINSAT6{0}.FINSAT6{0}.STK.MalKodu = '{1}') AND (FINSAT6{0}.FINSAT6{0}.STK.Kod1 = 'KKABLO')", item.SirketKod, item2.MalKodu);
                                var stk = db.Database.SqlQuery<frmCableStk>(sql).FirstOrDefault();
                                if (stk != null)
                                {
                                    //makarayı bul
                                    var kablo = dbx.stoks.Where(m => m.depo == depo && m.marka == stk.MalAdi4 && m.cins == stk.Nesne2 && m.kesit == stk.Kod15 && m.id == item2.KynkSiparisID).FirstOrDefault();
                                    //kabloya açık yap
                                    if (kablo.miktar != item2.Miktar)
                                        kablo.makara = "AÇIK";
                                    //yeni hareket ekle
                                    hareket tblh = new hareket()
                                    {
                                        id = kablo.id,
                                        miktar = item2.Miktar,
                                        musteri = item.HesapKodu.GetUnvan(item.SirketKod).Left(40),
                                        tarih = DateTime.Now,
                                        kaydigiren = tblx.AdSoyad
                                    };
                                    dbx.harekets.Add(tblh);
                                    varmi = true;
                                }
                            }
                        }
                        if (varmi == true) dbx.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Logger(kull.AdSoyad, "Terminal", ex, "Service/Terminal/SiparisTopla_GoreviTamamla");
                    return new Result(false, "Kablo kaydı hariç her şey tamamlandı!");
                }
            return new Result(true);
        }
        /// <summary>
        /// mal kabul kayıt işlemleri
        /// </summary>
        [WebMethod]
        public Result Paketle(List<frmMalKabul> StiList, int GorevID, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            int durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "İrsaliye bulunamadı !");
            //add to gorev user table
            var tbl = db.GorevUsers.Where(m => m.GorevID == GorevID && m.UserName == tblx.Kod).FirstOrDefault();
            if (tbl == null)
            {
                tbl = new GorevUser()
                {
                    UserName = tblx.Kod,
                    GorevID = GorevID,
                    BaslamaTarihi = DateTime.Today.ToOADateInt()
                };
                db.GorevUsers.Add(tbl);
                db.SaveChanges();
            }
            //loop
            Result _result = new Result(true);
            var stok = new IrsaliyeDetay();
            try
            {
                foreach (var item in StiList)
                {
                    var tmp = stok.Detail(item.ID);
                    if (tmp.Miktar >= item.OkutulanMiktar)
                    {
                        tmp.OkutulanMiktar = item.OkutulanMiktar;
                        stok.Operation(tmp);
                    }
                    else
                        _result = new Result(false, tmp.MalKodu + " için fazla mal yazılmış");
                }
            }
            catch (Exception ex)
            {
                Logger(KullID.ToString(), "Terminal", ex, "Service/Terminal/Paketle");
                return new Result(false, "Bir hata oldu");
            }
            return _result;
        }
        /// <summary>
        /// paketle görevini tamamla
        /// </summary>
        [WebMethod]
        public Result Paketle_GoreviTamamla(int GorevID, int IrsaliyeID, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            int durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görev bulunamadı !");
            var mIrsaliye = db.IRS.Where(m => m.ID == IrsaliyeID).FirstOrDefault();
            var list = mIrsaliye.IRS_Detay.Where(m => m.IrsaliyeID == mGorev.IrsaliyeID && (m.OkutulanMiktar != m.Miktar || m.OkutulanMiktar == null)).FirstOrDefault();
            if (list.IsNotNull())
                return new Result(false, "İşlem bitmemiş !");
            //paket bilgilerini hazırla
            int tarih = DateTime.Today.ToOADateInt();
            db.SettingsPaketNo(mGorev.DepoID, GorevID, tblx.Kod, tarih);
            LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "Paketle_GoreviTamamla", ComboItems.alEkle, GorevID, "Paket Barkodu");
            //görev user tablosu
            var tbl = db.GorevUsers.Where(m => m.GorevID == GorevID && m.UserName == tblx.Kod).FirstOrDefault();
            tbl.BitisTarihi = DateTime.Today.ToOADateInt();
            db.SaveChanges();
            //return
            return new Result(true);

        }
        /// <summary>
        /// seçili irsaliyenin bilgileri
        /// </summary>
        [WebMethod]
        public frmGorevPaket GetPackageBarcodeDetails(int GorevID, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new frmGorevPaket();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new frmGorevPaket();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            if (mGorev.IsNull() || mGorev.IR == null)
                return new frmGorevPaket();
            //gets
            var list = db.GorevPaketlers.Where(m => m.GorevID == GorevID).Select(m => new frmGorevPaket { SevkiyatNo = m.SevkiyatNo, PaketNo = m.PaketNo, Adet = m.Adet, PaketTipiID = m.PaketTipiID, Agirlik = m.Agirlik, HepsiVar = true }).FirstOrDefault();
            //get ağırlık

            //return
            return list;
        }
        /// <summary>
        /// barkoddan irsaliyenin bilgileri
        /// </summary>
        [WebMethod]
        public Tip_IRS GetIrsaliyeFromBarcode(string barkod, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Tip_IRS();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new Tip_IRS();
            if (barkod == "" || barkod == null)
                return new Tip_IRS();
            //return
            string sql = string.Format("SELECT wms.IRS.SirketKod FROM wms.GorevPaketler INNER JOIN wms.GorevIRS ON wms.GorevPaketler.GorevID = wms.GorevIRS.GorevID INNER JOIN wms.IRS ON wms.GorevIRS.IrsaliyeID = wms.IRS.ID WHERE (wms.GorevPaketler.PaketNo = '{0}')", barkod);
            var sirketkodu = db.Database.SqlQuery<string>(sql).FirstOrDefault();
            sql = string.Format("SELECT MIN(GorevPaketler.GorevID) AS ID, wms.Depo.DepoKodu AS DepoID, IRS.HesapKodu, CONVERT(varchar(15), wms.fnRoundUp(wms.GorevPaketler.Agirlik,2)) as TeslimCHK,  wms.fnFormatDateFromInt(wms.GorevPaketler.KayitTarih) AS Tarih, " +
                "(SELECT Unvan1 + ' ' + Unvan2 AS Expr1 FROM FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) WHERE(HesapKodu = IRS.HesapKodu)) AS Unvan, " +
                "(SELECT wms.IRS.EvrakNo + '  ' FROM wms.IRS WITH(nolock) INNER JOIN wms.GorevIRS WITH(nolock) ON wms.IRS.ID = wms.GorevIRS.IrsaliyeID INNER JOIN wms.GorevPaketler ON wms.GorevIRS.GorevID = wms.GorevPaketler.GorevID WHERE(wms.GorevPaketler.PaketNo = '{1}') FOR XML PATH('')) as EvrakNo " +
                "FROM wms.IRS AS IRS WITH(nolock) INNER JOIN wms.Depo WITH(nolock) ON IRS.DepoID = wms.Depo.ID INNER JOIN wms.GorevIRS WITH(nolock) ON IRS.ID = wms.GorevIRS.IrsaliyeID INNER JOIN wms.GorevPaketler ON wms.GorevIRS.GorevID = wms.GorevPaketler.GorevID " +
                "WHERE (wms.GorevPaketler.PaketNo = '{1}') " +
                "GROUP BY wms.Depo.DepoKodu, IRS.HesapKodu, CONVERT(varchar(15), wms.fnRoundUp(wms.GorevPaketler.Agirlik,2)), wms.fnFormatDateFromInt(wms.GorevPaketler.KayitTarih)", sirketkodu, barkod);
            return db.Database.SqlQuery<Tip_IRS>(sql).FirstOrDefault();
        }
        /// <summary>
        /// paketle görevini tamamla
        /// </summary>
        [WebMethod]
        public Result UpdatePackageBarcode(frmGorevPaket pkt, int GorevID, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            int durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görev bulunamadı !");
            //görevi bitir
            int tarih = DateTime.Today.ToOADateInt();
            string gorevNo = db.SettingsGorevNo(tarih, mGorev.DepoID).FirstOrDefault();
            var sevkiyat = db.Settings.Select(m => m.SevkiyatVarmi).FirstOrDefault();
            var idx = db.TerminalFinishGorev(GorevID, mGorev.IrsaliyeID, gorevNo, tarih, DateTime.Now.ToOaTime(), tblx.Kod, "", ComboItems.Paketle.ToInt32(), sevkiyat ? ComboItems.Sevket.ToInt32() : 0).FirstOrDefault();
            LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "UpdatePackageBarcode", ComboItems.alDüzenle, idx.ToInt32(), "Paketle => Sevkiyat");
            //update
            var tbl = db.GorevPaketlers.Where(m => m.GorevID == GorevID).FirstOrDefault();
            tbl.SevkiyatNo = pkt.SevkiyatNo;
            tbl.PaketNo = pkt.PaketNo;
            tbl.PaketTipiID = pkt.PaketTipiID;
            tbl.Adet = pkt.Adet;
            tbl.Agirlik = pkt.Agirlik;
            tbl.Degistiren = tblx.Kod;
            tbl.DegisTarih = tarih;
            db.SaveChanges();
            //return
            return new Result(true);

        }
        /// <summary>
        /// paketle görevini tamamla
        /// </summary>
        [WebMethod]
        public Result Sevkiyat_GoreviTamamla(int GorevID, int IrsaliyeID, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            int durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görev bulunamadı !");
            //return
            int tarih = DateTime.Today.ToOADateInt();
            var kull = db.Users.Where(m => m.ID == KullID).Select(m => m.Kod).FirstOrDefault();
            db.TerminalFinishGorev(GorevID, IrsaliyeID, "", tarih, DateTime.Now.ToOaTime(), kull, "", ComboItems.Sevket.ToInt32(), 0).FirstOrDefault();
            LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "Sevkiyat_GoreviTamamla", ComboItems.alDüzenle, GorevID, "Sevkiyat => -");
            //görev user tablosu
            var tbl = db.GorevUsers.Where(m => m.GorevID == GorevID && m.UserName == tblx.Kod).FirstOrDefault();
            tbl.BitisTarihi = DateTime.Today.ToOADateInt();
            db.SaveChanges();
            return new Result(true);

        }
        /// <summary>
        /// transfer çıkış görevleri tamamlma
        /// </summary>
        [WebMethod]
        public Result TransferCikis_GoreviTamamla(int GorevID, int KullID, string AuthGiven, string Guid)
        {
            //kontrols
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            int durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Transfer bulunamadı !");
            var tmpYer = mGorev.GorevYers.Where(m => m.GorevID == mGorev.ID && (m.YerlestirmeMiktari < m.Miktar || m.YerlestirmeMiktari == null)).FirstOrDefault();
            if (tmpYer.IsNotNull())
                return new Result(false, "İşlem bitmemiş !");
            //kullanıcı kontrol
            var kull = db.Users.Where(m => m.ID == KullID).FirstOrDefault();
            //aktar
            Result sonuc;
            int tarih = DateTime.Today.ToOADateInt();
            int saat = DateTime.Now.ToOaTime();
            var transfer = mGorev.Transfers.FirstOrDefault();
            if (transfer == null)//iç transfer
            {
                db.TerminalFinishGorev(GorevID, mGorev.IrsaliyeID, "", tarih, DateTime.Now.ToOaTime(), kull.Kod, "", ComboItems.TransferÇıkış.ToInt32(), 0).FirstOrDefault();
                LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "TransferGiris_GoreviTamamla", ComboItems.alDüzenle, GorevID, "TransferÇıkış => -");
                //update gorev table
                var id = mGorev.IR.LinkEvrakNo.ToInt32();
                var tmp = db.Gorevs.Where(m => m.ID == id).FirstOrDefault();
                tmp.DurumID = ComboItems.Açık.ToInt32();
                db.SaveChanges();
                sonuc = new Result(true);
            }
            else//dış transfer
            {
                string gorevNo = db.SettingsGorevNo(tarih, mGorev.DepoID).FirstOrDefault();
                if (kull.UserDetail.TransferOutSeri == null)
                    return new Result(false, "Bu kullanıcıya ait seri nolar hatalı !");
                if (kull.UserDetail.TransferOutSeri.Value < 1 || kull.UserDetail.TransferOutSeri.Value > 199)
                    return new Result(false, "Bu kullanıcıya ait seri nolar hatalı !");
                Finsat finsat = new Finsat(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, mGorev.IR.SirketKod);
                sonuc = finsat.DepoTransfer(transfer, false, kull.Kod, kull.UserDetail.TransferOutSeri.Value);
                if (sonuc.Status == true)
                {
                    //finish
                    db.TerminalFinishGorev(GorevID, mGorev.IrsaliyeID, "", tarih, DateTime.Now.ToOaTime(), kull.Kod, "", ComboItems.TransferÇıkış.ToInt32(), 0).FirstOrDefault();
                    LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "TransferGiris_GoreviTamamla", ComboItems.alDüzenle, GorevID, "TransferÇıkış => -");
                    //get depo details
                    var araDepo = db.Depoes.Where(m => m.ID == transfer.AraDepoID).Select(m => m.DepoKodu).FirstOrDefault();
                    var girisDepo = db.Depoes.Where(m => m.ID == transfer.GirisDepoID).Select(m => m.DepoKodu).FirstOrDefault();
                    //add new irsaliye for giriş
                    var cevap = db.InsertIrsaliye(transfer.SirketKod, transfer.GirisDepoID, gorevNo, gorevNo, tarih, "Giriş: " + girisDepo + ", Çıkış: " + araDepo, false, ComboItems.TransferGiriş.ToInt32(), kull.Kod, tarih, saat, mGorev.IR.HesapKodu, "", 0, "").FirstOrDefault();
                    //insert irs_detay
                    foreach (var item in mGorev.IR.IRS_Detay)
                    {
                        var tbli = new IRS_Detay() { IrsaliyeID = cevap.IrsaliyeID.Value, MalKodu = item.MalKodu, Miktar = item.Miktar, Birim = item.Birim };
                        db.IRS_Detay.Add(tbli);
                    }
                    //yeni görev id'yi yaz
                    transfer.GorevID = cevap.GorevID.Value;
                    mGorev.IR.DepoID = transfer.GirisDepoID;
                    mGorev.IR.EvrakNo = sonuc.Data.ToString();
                    //görev user tablosu
                    var tbl = db.GorevUsers.Where(m => m.GorevID == GorevID && m.UserName == tblx.Kod).FirstOrDefault();
                    tbl.BitisTarihi = DateTime.Today.ToOADateInt();
                    db.SaveChanges();
                }
            }
            return sonuc;
        }
        /// <summary>
        /// transfer giriş görevleri tamamlma
        /// </summary>
        [WebMethod]
        public Result TransferGiris_GoreviTamamla(int GorevID, int KullID, string AuthGiven, string Guid)
        {
            //kontrols
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            int durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Transfer bulunamadı !");
            var tmpYer = mGorev.IR.IRS_Detay.Where(m => m.IrsaliyeID == mGorev.IrsaliyeID && (m.YerlestirmeMiktari < m.Miktar || m.YerlestirmeMiktari == null)).FirstOrDefault();
            if (tmpYer.IsNotNull())
                return new Result(false, "İşlem bitmemiş !");
            //kullanıcı kontrol
            var kull = db.Users.Where(m => m.ID == KullID).FirstOrDefault();
            if (kull.UserDetail.TransferInSeri == null)
                return new Result(false, "Bu kullanıcıya ait seri nolar hatalı !");
            if (kull.UserDetail.TransferInSeri < 1 || kull.UserDetail.TransferInSeri > 199)
                return new Result(false, "Bu kullanıcıya ait seri nolar hatalı !");
            //aktar
            //görev bitir
            Result sonuc;
            int tarih = DateTime.Today.ToOADateInt();
            var transfer = mGorev.Transfers.FirstOrDefault();
            if (transfer == null)//iç transfer
            {
                db.TerminalFinishGorev(GorevID, mGorev.IrsaliyeID, "", tarih, DateTime.Now.ToOaTime(), kull.Kod, "", ComboItems.TransferÇıkış.ToInt32(), 0).FirstOrDefault();
                LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "TransferGiris_GoreviTamamla", ComboItems.alDüzenle, GorevID, "TransferÇıkış => -");
                sonuc = new Result(true);
            }
            else//dış transfer
            {
                Finsat finsat = new Finsat(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, mGorev.IR.SirketKod);
                sonuc = finsat.DepoTransfer(mGorev.Transfers.FirstOrDefault(), true, kull.Kod, kull.UserDetail.TransferInSeri.Value);
                if (sonuc.Status == true)
                {
                    //update irsaliye
                    db.UpdateIrsaliye(mGorev.IrsaliyeID, sonuc.Data.ToString(), "");
                    //finish
                    db.TerminalFinishGorev(GorevID, mGorev.IrsaliyeID, "", tarih, DateTime.Now.ToOaTime(), kull.Kod, "", ComboItems.TransferGiriş.ToInt32(), 0).FirstOrDefault();
                    LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "TransferGiris_GoreviTamamla", ComboItems.alDüzenle, GorevID, "TransferGiriş => -");
                    //görev user tablosu
                    var tbl = db.GorevUsers.Where(m => m.GorevID == GorevID && m.UserName == tblx.Kod).FirstOrDefault();
                    tbl.BitisTarihi = DateTime.Today.ToOADateInt();
                    db.SaveChanges();
                }
            }
            return sonuc;
        }
        /// <summary>
        /// kontrollü sayımda satırları kaydet
        /// </summary>
        [WebMethod]
        public Result Kontrollu_Say(List<frmYerlesme> StiList, int GorevID, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            int durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görev bulunamadı !");
            //add to gorev user table
            var tbl = db.GorevUsers.Where(m => m.GorevID == GorevID && m.UserName == tblx.Kod).FirstOrDefault();
            if (tbl == null)
            {
                tbl = new GorevUser()
                {
                    UserName = tblx.Kod,
                    GorevID = GorevID,
                    BaslamaTarihi = DateTime.Today.ToOADateInt()
                };
                db.GorevUsers.Add(tbl);
            }
            //loop the list
            foreach (var item in StiList)
            {
                //görev yer tablosu
                GorevYer tbl2;
                if (item.IrsDetayID == 0)
                {
                    var katID = db.GetHucreKatID(mGorev.DepoID, item.RafNo).FirstOrDefault();
                    if (katID != null)
                    {
                        var yert = db.Yers.Where(m => m.KatID == katID && m.MalKodu == item.MalKodu).FirstOrDefault();
                        if (yert == null)
                        {
                            yert = new Yer() { KatID = katID.Value, MalKodu = item.MalKodu, Birim = item.Birim, Miktar = 0 };
                            db.Yers.Add(yert);
                            db.SaveChanges();
                        }
                        tbl2 = new GorevYer() { GorevID = GorevID, MalKodu = item.MalKodu, Birim = item.Birim, Miktar = item.Miktar, YerlestirmeMiktari = item.Miktar, GC = false, YerID = yert.ID };
                        db.GorevYers.Add(tbl2);
                    }
                }
                else
                {
                    tbl2 = db.GorevYers.Where(m => m.ID == item.IrsDetayID).FirstOrDefault();
                    tbl2.Miktar = item.Miktar;
                    tbl2.YerlestirmeMiktari = item.Miktar;
                }
                //kaydetme işlemleri
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger(KullID.ToString(), "Terminal", ex, "Service/Terminal/Kontrollu_Say");
                }
            }
            return new Result(true);
        }
        /// <summary>
        /// kontrollü sayımda satırları kaydet
        /// </summary>
        [WebMethod]
        public Result KontrolluSay_GoreviTamamla(int GorevID, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            int durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görev bulunamadı !");
            //update gorev user table
            var tbl = db.GorevUsers.Where(m => m.GorevID == GorevID && m.UserName == tblx.Kod).FirstOrDefault();
            if (tbl != null)
            {
                tbl.BitisTarihi = DateTime.Today.ToOADateInt();
                db.SaveChanges();
            }
            LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "KontrolluSay_GoreviTamamla", ComboItems.alDüzenle, GorevID, "KontrolluSay => -");
            return new Result(true);
        }
    }
}
