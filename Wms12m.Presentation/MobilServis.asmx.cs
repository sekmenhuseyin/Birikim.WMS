using OnikimCore.GunesCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Web.Services;
using TumFaturaKayit;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://www.12mbilgisayar.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class MobilServis : WebService, IDisposable
    {
        //declerations
        private WMSEntities db = new WMSEntities();
        /// <summary>
        /// login işlemleri
        /// </summary>
        [WebMethod]
        public Login LoginKontrol(string userID, string sifre)
        {
            //new user
            var user = new User() { Kod = userID, Sifre = sifre };
            //log in actions
            var person = new Persons();
            var result = person.Login(user);
            //check result
            if (result.Id > 0)
                try
                {
                    return db.Users.Where(m => m.ID == result.Id).Select(m => new Login { ID = m.ID, Kod = m.Kod, AdSoyad = m.AdSoyad, DepoKodu = m.Depo.DepoKodu, DepoID = m.Depo.ID }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    db.Logger(userID, "", "", ex.Message, ex.InnerException != null ? ex.InnerException.Message : "", "WebService/Login");
                    return null;
                }
            return null;
        }
        /// <summary>
        /// depoya ait görev özetini getirir
        /// </summary>
        [WebMethod]
        public List<GorevOzet> GetGorevOzet(int ID)
        {
            return db.Gorevs.Where(m => m.DepoID == ID && m.DurumID == 9).GroupBy(m => new { m.ComboItem_Name1.Name, m.ComboItem_Name1.ID }).Select(m => new GorevOzet { ID = m.Key.ID, Ad = m.Key.Name, Sayi = m.Count(n => n.ID > 0) }).ToList();
        }
        /// <summary>
        /// irsaliyeleri getir
        /// </summary>
        [WebMethod]
        public List<Tip_IRS> GetIrsaliyeList()
        {
            return db.IRS.Select(m => new Tip_IRS { ID = m.ID, DepoID = m.Depo.DepoKodu, EvrakNo = m.EvrakNo, HesapKodu = m.HesapKodu, Kaydeden = m.User.AdSoyad, SirketKod = m.SirketKod, Tarih = m.Tarih.ToString(), TeslimCHK = m.TeslimCHK, Unvan = "" }).ToList();
        }
        /// <summary>
        /// seçili irsaliyenin bilgileri
        /// </summary>
        [WebMethod]
        public Tip_IRS GetIrsaliye(int GorevID)
        {
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            if (mGorev.IsNull())
                return null;
            string sql = string.Format(@"SELECT IRS.ID, IRS.EvrakNo, Depo.DepoKodu AS DepoID, IRS.HesapKodu, CONVERT(VARCHAR(10), CONVERT(Datetime, IRS.Tarih - 2), 104) AS Tarih, IRS.TeslimCHK,
                                        (SELECT Kod FROM usr.Users WITH (nolock) WHERE (ID = IRS.Kaydeden)) AS Kaydeden,
                                        (SELECT Unvan1 + ' ' + Unvan2 AS Expr1 FROM FINSAT6{0}.FINSAT6{0}.CHK WITH (NOLOCK) WHERE (HesapKodu = IRS.HesapKodu)) AS Unvan
                                        FROM wms.IRS AS IRS WITH (nolock) INNER JOIN wms.Depo WITH (nolock) ON IRS.DepoID = Depo.ID
                                        WHERE (IRS.ID = {1})", mGorev.IR.SirketKod, mGorev.IR.ID);
            return db.Database.SqlQuery<Tip_IRS>(sql).FirstOrDefault();
        }
        /// <summary>
        /// görev listelerini filtreye göre getirir
        /// </summary>
        [WebMethod]
        public List<Tip_GOREV> GetGorevList(int gorevli, int durum, int gorevtipi, int DepoID)
        {
            string sql = string.Format("SELECT GRV.ID, ISNULL(GRV.IrsaliyeID, 0), CONVERT(VARCHAR(10), CONVERT(Datetime, GRV.OlusturmaTarihi - 2), 104) AS OlusturmaTarihi, GRV.Bilgi, GRV.Aciklama, GRV.GorevNo, ISNULL(wms.IRS.EvrakNo, ''), wms.Depo.DepoKodu, Users_1.Kod AS Atayan, usr.Users.Kod AS Gorevli, ComboItem_Name.[Name] AS Durum " +
                                    "FROM wms.Gorev AS GRV WITH (nolock) INNER JOIN wms.Depo WITH (nolock) ON GRV.DepoID = wms.Depo.ID INNER JOIN ComboItem_Name WITH (nolock) ON GRV.DurumID = ComboItem_Name.ID LEFT OUTER JOIN usr.Users WITH (nolock) ON GRV.GorevliID = usr.Users.ID LEFT OUTER JOIN usr.Users AS Users_1 WITH (nolock) ON GRV.AtayanID = Users_1.ID LEFT OUTER JOIN wms.IRS WITH (nolock) ON GRV.IrsaliyeID = wms.IRS.ID " +
                                    "WHERE (wms.Depo.ID = {3}) and case when ({0}>0) then case when (GRV.GorevTipiID = {0}) then 1 else 0 end else 0 end =1 AND case when ({1}>0) then case when (GRV.GorevliID = {1}) then 1 else 0 end else 1 end = 1 AND  case when ({2}>0) then case when (GRV.DurumID = {2}) then 1 else 0 end else 0 end =1", gorevtipi, gorevli, durum, DepoID);
            return db.Database.SqlQuery<Tip_GOREV>(sql).ToList();
        }
        /// <summary>
        /// bir şirkete ait kullanıcıları getirir
        /// </summary>
        [WebMethod]
        public List<Kullanicilar> GetUsers()
        {
            return db.Users.Select(m => new Kullanicilar { ID = m.ID, Kod = m.Kod }).ToList();
        }
        /// <summary>
        /// durumları listeler
        /// </summary>
        [WebMethod]
        public List<Durum> GetDurums()
        {
            return db.ComboItem_Name.Where(m => m.Visible == true).Where(m => m.ComboID == 2).Select(m => new Durum { ID = m.ID, Name = m.Name }).ToList();
        }
        /// <summary>
        /// malzemeleri getir
        /// </summary>
        [WebMethod]
        public List<Tip_STI> GetMalzemes(int GorevID, int irsaliyeID, bool devamMi)
        {
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            if (mGorev.IsNull())
                return null;
            string sql;
            if (mGorev.GorevTipiID == ComboItems.SiparişTopla.ToInt32() || mGorev.GorevTipiID == ComboItems.Transfer.ToInt32())
            {
                string sqltmp = ""; if (devamMi == true) sqltmp += "AND wms.GorevYer.Miktar>ISNULL(wms.GorevYer.YerlestirmeMiktari,0) ";
                sql = string.Format("SELECT wms.GorevYer.ID, wms.GorevYer.MalKodu, wms.GorevYer.Miktar, wms.GorevYer.Birim, wms.Yer.HucreAd AS Raf, ISNULL(wms.GorevYer.YerlestirmeMiktari,0) as YerlestirmeMiktari, " +
                                    "ISNULL((SELECT MalAdi FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE (MalKodu = wms.GorevYer.MalKodu)),'') AS MalAdi " +
                                    "FROM wms.GorevYer WITH(nolock) INNER JOIN wms.Yer WITH(nolock) ON wms.GorevYer.YerID = wms.Yer.ID " +
                                    "WHERE (wms.GorevYer.GorevID = {1}) {2}" +
                                    "ORDER BY wms.GorevYer.Sira", mGorev.IR.SirketKod, GorevID, sqltmp);
            }
            else
            {
                sql = string.Format("SELECT ID, {1} as irsID, MalKodu, Miktar, Birim, ISNULL(OkutulanMiktar, 0) AS OkutulanMiktar, ISNULL(YerlestirmeMiktari, 0) AS YerlestirmeMiktari, " +
                                    "ISNULL((SELECT TOP(1) HucreAd FROM wms.Yer_Log WITH(NOLOCK) WHERE(MalKodu = wms.IRS_Detay.MalKodu) AND(IrsaliyeID = {1})),'') AS Raf, " +
                                    "ISNULL((SELECT MalAdi FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE (MalKodu = wms.IRS_Detay.MalKodu)),'') AS MalAdi " +
                                    "FROM wms.IRS_Detay WITH (nolock) WHERE (IrsaliyeID = {1})", mGorev.IR.SirketKod, mGorev.IR.ID);
                if (devamMi == true)
                    if (mGorev.GorevTipiID == ComboItems.MalKabul.ToInt32() || mGorev.GorevTipiID == ComboItems.Paketle.ToInt32())
                        sql += " AND (Miktar > ISNULL(OkutulanMiktar,0))";
                    else //2 and 3
                        sql += " AND (Miktar > ISNULL(YerlestirmeMiktari,0))";
            }
            return db.Database.SqlQuery<Tip_STI>(sql).ToList();
        }
        /// <summary>
        /// mal kabul kayıt işlemleri
        /// </summary>
        [WebMethod]
        public Result Mal_Kabul(List<frmMalKabul> StiList)
        {
            var stok = new Stok();
            foreach (var item in StiList)
            {
                var tmp = stok.Detail(item.ID);
                if (tmp.OkutulanMiktar == null) tmp.OkutulanMiktar = item.OkutulanMiktar;
                else tmp.OkutulanMiktar += item.OkutulanMiktar;
                stok.Operation(tmp);
            }
            try
            {
                return new Result(true);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        /// <summary>
        /// mal kabul onay
        /// </summary>
        [WebMethod]
        public Result MalKabul_GoreviTamamla(int GorevID, int kulID)
        {
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "İrsaliye bulunamadı !");
            var sonuc = MalKabulToLink(mGorev.IR.SirketKod, mGorev.IR.ID, kulID);
            if (sonuc.Status == true)
            {
                string gorevNo = db.SettingsGorevNo(DateTime.Today.ToOADateInt()).FirstOrDefault();
                db.TerminalFinishGorev(GorevID, mGorev.IrsaliyeID, gorevNo, DateTime.Today.ToOADateInt(), DateTime.Now.SaatiAl(), kulID, "", ComboItems.MalKabul.ToInt32(), ComboItems.RafaKaldır.ToInt32());
                return new Result(true);
            }
            else
                return new Result(false, sonuc.Message);

        }
        private Result MalKabulToLink(string sirketKodu, int irsID, int kulID)
        {
            DevHelper.Ayarlar.SetConStr(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString);
            DevHelper.Ayarlar.SirketKodu = sirketKodu;
            string kaydeden = db.Users.Where(m => m.ID == kulID).Select(m => m.Kod).FirstOrDefault();
            List<STIBase> STIBaseList = new List<STIBase>();
            string sql = String.Format("SELECT IRS.EvrakNo, IRSD.IrsaliyeID, IRSD.MalKodu, (SELECT MalAdi FROM FINSAT6{0}.FINSAT6{0}.STK(NOLOCK) WHERE MalKodu=IRSD.MalKodu) as MalAdi, IRSD.Miktar, IRSD.Birim, ISNULL(IRSD.OkutulanMiktar, 0) AS OkutulanMiktar, D.DepoKodu, IRS.HesapKodu, IRS.Tarih, ISNULL(IRSD.KynkSiparisNo, '') AS SiparisNo,  ISNULL(IRSD.KynkSiparisSiraNo,0) as KynkSiparisSiraNo, ISNULL(IRSD.KynkSiparisTarih,0) as KynkSiparisTarih, ISNULL(IRSD.KynkSiparisMiktar,0) as KynkSiparisMiktar " +
                                        "FROM wms.IRS_Detay AS IRSD WITH (NOLOCK) LEFT OUTER JOIN wms.IRS AS IRS WITH (NOLOCK) ON IRSD.IrsaliyeID = IRS.ID LEFT OUTER JOIN wms.Depo AS D WITH (NOLOCK) ON IRS.DepoID = D.ID " +
                                        "WHERE IrsaliyeID={1}", sirketKodu, irsID);
            var STList = db.Database.SqlQuery<STIMax>(sql).ToList();
            foreach (STIMax stItem in STList)
            {
                STIBase sti = new STIBase()
                {
                    EvrakNo = stItem.EvrakNo,
                    HesapKodu = stItem.HesapKodu,
                    Tarih = stItem.Tarih.IntToDate(),
                    MalKodu = stItem.MalKodu,
                    Miktar = stItem.OkutulanMiktar,
                    Birim = stItem.Birim,
                    Depo = stItem.DepoKodu,
                    EvrakTipi = STIEvrakTipi.AlimIrsaliyesi,
                    Kaydeden = kaydeden,
                    KayitSurum = "9.01.028",
                    KayitKaynak = 74
                };
                if (stItem.SiparisNo.IsNotNullEmpty())
                {
                    sti.KayitTipi = STIKayitTipi.Siparisten_Irsaliye;
                    sti.KaynakSiparisNo = stItem.SiparisNo;
                    sti.KaynakSiparisTarih = stItem.KynkSiparisTarih;
                    sti.SiparisSiraNo = stItem.KynkSiparisSiraNo;
                    sti.SiparisMiktar = stItem.KynkSiparisMiktar;
                }
                else
                {
                    sti.KayitTipi = STIKayitTipi.Irsaliye;
                    sti.KaynakSiparisNo = "";
                    sti.KaynakSiparisTarih = 0;
                    sti.SiparisSiraNo = 0;
                    sti.SiparisMiktar = 0;
                }
                STIBaseList.Add(sti);
            }
            Irsaliye_Islemleri IrsIslem = new Irsaliye_Islemleri(sirketKodu);
            var Sonuc = IrsIslem.Irsaliye_Kayit(-1, STIBaseList);
            //sonuç döner
            if (Sonuc.Hata.IsNull())
            {
                return new Result(true, Sonuc.Veri.ToString2());
            }
            else
                return new Result(false, Sonuc.Hata.Message);
        }
        /// <summary>
        /// rafa yerleştir
        /// </summary>
        [WebMethod]
        public Result Rafa_Kaldir(List<frmYerlesme> YerlestirmeList, int kulID)
        {
            foreach (var item in YerlestirmeList)
            {
                //hücre adından kat id bulunur
                var kat = db.GetHucreKatID(item.DepoID, item.RafNo).FirstOrDefault();
                if (kat != null)
                {
                    //irs detay tablosu güncellenir
                    var irsdetay = new Stok();
                    var tmp = irsdetay.Detail(item.IrsDetayID);
                    if (tmp.Miktar >= ((tmp.YerlestirmeMiktari != null ? tmp.YerlestirmeMiktari : 0) + item.Miktar))
                    {
                        if (tmp.YerlestirmeMiktari == null) tmp.YerlestirmeMiktari = item.Miktar;
                        else tmp.YerlestirmeMiktari += item.Miktar;
                        irsdetay.Operation(tmp);
                        //yerleştirme kaydı yapılır
                        var stok = new Yerlestirme();
                        var tmp2 = stok.Detail(kat.Value, item.MalKodu, item.Birim);
                        if (tmp2 == null)
                        {
                            tmp2 = new Yer()
                            {
                                KatID = kat.Value,
                                MalKodu = item.MalKodu,
                                Birim = item.Birim,
                                Miktar = item.Miktar
                            };
                            stok.Insert(tmp2, item.IrsID, kulID);
                        }
                        else
                        {
                            tmp2.Miktar += item.Miktar;
                            stok.Update(tmp2, item.IrsID, kulID, false);
                        }
                    }
                }
            }
            return new Result(true);
        }
        /// <summary>
        /// rafa kaldır görevi tamamlanınca
        /// </summary>
        [WebMethod]
        public Result RafaKaldir_GoreviTamamla(int GorevID, int kulID)
        {
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "İrsaliye bulunamadı !");
            var list = mGorev.IR.IRS_Detay.Where(m => m.YerlestirmeMiktari != m.Miktar).FirstOrDefault();
            if (list.IsNotNull())
                return new Result(false, "İşlem bitmemiş !");
            db.TerminalFinishGorev(GorevID, mGorev.IrsaliyeID, "", DateTime.Today.ToOADateInt(), DateTime.Now.SaatiAl(), kulID, "", ComboItems.RafaKaldır.ToInt32(), 0);
            return new Result(true);
        }
        /// <summary>
        /// raftan indir
        /// </summary>
        [WebMethod]
        public Result Siparis_Topla(List<frmYerlesme> YerlestirmeList, int kulID)
        {
            foreach (var item in YerlestirmeList)
            {
                //hücre adından kat id bulunur
                var kat = db.GetHucreKatID(item.DepoID, item.RafNo).FirstOrDefault();
                if (kat != null)
                {
                    //yerleştirme kaydı yapılır
                    var stok = new Yerlestirme();
                    var tmp2 = stok.Detail(kat.Value, item.MalKodu, item.Birim);
                    if (tmp2 != null)
                    {
                        var x = db.GorevYers.Where(m => m.YerID == tmp2.ID && m.GorevID == item.GorevID && m.MalKodu == item.MalKodu && m.Birim == item.Birim).FirstOrDefault();
                        if (tmp2.Miktar >= item.Miktar && item.Miktar <= (x.Miktar - (x.YerlestirmeMiktari != null ? x.YerlestirmeMiktari : 0)))
                        {
                            tmp2.Miktar -= item.Miktar;
                            stok.Update(tmp2, item.IrsID, kulID, true);
                            //raftan indirdiğini kaydet
                            db.TerminalRaftanIndir(item.IrsID, kat, item.Miktar);
                        }
                    }
                }
            }
            return new Result(true);
        }
        /// <summary>
        /// sipariş toplama görevi tamamlma
        /// </summary>
        [WebMethod]
        public Result SiparisTopla_GoreviTamamla(int GorevID, int kulID)
        {
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "İrsaliye bulunamadı !");
            var tmpYer = mGorev.GorevYers.Where(m => m.YerlestirmeMiktari < m.Miktar).FirstOrDefault();
            if (tmpYer.IsNotNull())
                return new Result(false, "İşlem bitmemiş !");
            //kaydeden bulunur
            string kaydeden = db.Users.Where(m => m.ID == kulID).Select(m => m.Kod).FirstOrDefault();
            int seritipi = ComboItems.EArşiv.ToInt32();
            //liste getirilir
            string sql = string.Format("SELECT wms.IRS.SirketKod, wms.GorevIRS.IrsaliyeID, wms.IRS.Tarih, wms.IRS.HesapKodu, wms.IRS.TeslimCHK, ISNULL(wms.IRS.ValorGun,0) as ValorGun, wms.IRS.EvrakNo " +
                                        "FROM wms.GorevIRS INNER JOIN wms.IRS ON wms.GorevIRS.IrsaliyeID = wms.IRS.ID " +
                                        "WHERE (wms.GorevIRS.GorevID = {0}) " +
                                        "GROUP BY wms.IRS.SirketKod, wms.GorevIRS.IrsaliyeID, wms.IRS.Tarih, wms.IRS.HesapKodu, wms.IRS.TeslimCHK, wms.IRS.ValorGun, wms.IRS.EvrakNo", mGorev.ID);
            var list = db.Database.SqlQuery<STIMax>(sql).ToList();
            int tarih = DateTime.Today.ToOADateInt(), saat = DateTime.Now.SaatiAl();
            foreach (var item in list)
            {
                string evrakserino = db.EvrakSeris.Where(m => m.SirketKodu == item.SirketKod && m.Tip == seritipi).Select(m => m.SeriNo).FirstOrDefault();
                //listedeki her eleman için döngü yapılır
                var sonuc = SiparisToplamaToLink(item.SirketKod, item.IrsaliyeID, mGorev.Depo.DepoKodu, evrakserino, item.Tarih, item.HesapKodu, kaydeden);
                if (sonuc.Status == true)
                {
                    string gorevNo = db.SettingsGorevNo(tarih).FirstOrDefault();
                    var x = db.InsertIrsaliye(item.SirketKod, mGorev.DepoID, gorevNo, item.EvrakNo, item.Tarih, "Irs: " + item.EvrakNo + " Alıcı: " + item.HesapKodu.GetUnvan(item.SirketKod), true, ComboItems.Paketle.ToInt32(), kulID, kaydeden, tarih, saat, item.HesapKodu, item.TeslimChk, item.ValorGun, "").FirstOrDefault();
                }
            }
            db.TerminalFinishGorev(GorevID, mGorev.IrsaliyeID, "", tarih, saat, kulID, "", ComboItems.SiparişTopla.ToInt32(), 0);
            return new Result(true);
        }
        private Result SiparisToplamaToLink(string sirketKodu, int irsID, string DepoKodu, string EvrakSeriNo, int Tarih, string CHK, string kaydeden)
        {
            var STIBaseList = new List<ParamSti>();
            //evrak no getir
            var ftrKayit = new FaturaKayit(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, sirketKodu);
            string evrkno = ftrKayit.EvrakNo_Getir(EvrakSeriNo);
            int saat = DateTime.Now.SaatiAl();
            //listeyi dön
            string sql = String.Format("SELECT MalKodu, Miktar, Birim, KynkSiparisNo as EvrakNo,KynkSiparisTarih, KynkSiparisSiraNo  FROM wms.IRS_Detay WITH (NOLOCK) WHERE IrsaliyeID={0}", irsID);
            var list = db.Database.SqlQuery<STIMax>(sql).ToList();
            foreach (STIMax item in list)
            {
                sql = string.Format("SELECT Chk, Miktar, MalKodu, Fiyat, Birim, Depo, ToplamIskonto, KDV, KDVOran, IskontoOran1, IskontoOran2, IskontoOran3, IskontoOran4, IskontoOran5, EvrakNo as KaynakSiparisNo, Tarih as KaynakSiparisTarih, SiraNo as SiparisSiraNo, Miktar as SiparisMiktar, TeslimMiktar, KapatilanMiktar, FytListeNo, ValorGun, Kod1, Kod2, Kod3, Kod10, Kod13, Kod14, KayitKaynak, KayitSurum, DegisKaynak, DegisSurum " +
                                    "FROM FINSAT6{0}.FINSAT6{0}.SPI WHERE (EvrakNo = '{1}') AND (Chk = '{2}') AND (Depo = '{3}') AND (Tarih = {4}) AND (SiraNo = {5}) AND (KynkEvrakTip = 62) AND (SiparisDurumu = 0) AND (Kod10 IN ('Terminal', 'Onaylandı'))", sirketKodu, item.EvrakNo, CHK, DepoKodu, item.KynkSiparisTarih, item.KynkSiparisSiraNo);
                var finsat = db.Database.SqlQuery<ParamSti>(sql).FirstOrDefault();
                if (finsat != null)
                {
                    finsat.Miktar = item.Miktar;
                    finsat.EvrakNo = evrkno;
                    finsat.Tarih = Tarih;
                    finsat.Kaydeden = kaydeden;
                    finsat.KayitSurum = "9.01.028";
                    finsat.KayitKaynak = 74;
                    finsat.KayitTarih = Tarih;
                    finsat.KayitSaat = saat;
                    finsat.Degistiren = kaydeden;
                    finsat.DegisSurum = "9.01.028";
                    finsat.DegisKaynak = 74;
                    finsat.DegisTarih = Tarih;
                    finsat.DegisSaat = saat;
                    STIBaseList.Add(finsat);
                }
            }
            //finsat işlemleri
            try
            {
                ftrKayit.FaturaKaydet(STIBaseList, EvrakSeriNo);
                return new Result(true);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        /// <summary>
        /// mal kabul kayıt işlemleri
        /// </summary>
        [WebMethod]
        public Result Paketle(List<frmMalKabul> StiList)
        {
            var stok = new Stok();
            foreach (var item in StiList)
            {
                var tmp = stok.Detail(item.ID);
                if (tmp.Miktar >= ((tmp.OkutulanMiktar != null ? tmp.OkutulanMiktar : 0) + item.OkutulanMiktar))
                {
                    if (tmp.OkutulanMiktar == null) tmp.OkutulanMiktar = item.OkutulanMiktar;
                    else tmp.OkutulanMiktar += item.OkutulanMiktar;
                    stok.Operation(tmp);
                }
            }
            try
            {
                return new Result(true);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        /// <summary>
        /// paketle görevini tamamla
        /// </summary>
        [WebMethod]
        public Result Paketle_GoreviTamamla(int GorevID, int IrsaliyeID, int kulID)
        {
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "İrsaliye bulunamadı !");
            var mIrsaliye = db.IRS.Where(m => m.ID == IrsaliyeID).FirstOrDefault();
            var list = mIrsaliye.IRS_Detay.Where(m => m.OkutulanMiktar != m.Miktar).FirstOrDefault();
            if (list.IsNotNull())
                return new Result(false, "İşlem bitmemiş !");
            int tarih = DateTime.Today.ToOADateInt();
            string gorevNo = db.SettingsGorevNo(tarih).FirstOrDefault();
            db.TerminalFinishGorev(GorevID, IrsaliyeID, gorevNo, tarih, DateTime.Now.SaatiAl(), kulID, "", ComboItems.Paketle.ToInt32(), ComboItems.Sevkiyat.ToInt32());
            //change gorev details
            mIrsaliye.Onay = true;
            db.SaveChanges();
            //check for all irsaliye
            string sql = "SELECT wms.Gorev.ID FROM wms.Gorev INNER JOIN wms.GorevIRS ON wms.Gorev.ID = wms.GorevIRS.GorevID INNER JOIN wms.IRS ON wms.GorevIRS.IrsaliyeID = wms.IRS.ID WHERE (wms.IRS.Onay = 0) AND wms.Gorev.ID = " + GorevID;
            var tmp = db.Database.SqlQuery<int>(sql).ToList();
            if (tmp.Count != 0)
            {
                mGorev.DurumID = ComboItems.Açık.ToInt32();
                mGorev.BitisSaati = null;
                mGorev.BitisTarihi = null;
                db.SaveChanges();
            }
            return new Result(true);

        }
        /// <summary>
        /// transfer kaydet
        /// </summary>
        [WebMethod]
        public Result Transfer(List<frmYerlesme> YerlestirmeList, int kulID)
        {
            //foreach (var item in YerlestirmeList)
            //{
            //    //hücre adından kat id bulunur
            //    var kat = db.GetHucreKatID(item.DepoID, item.RafNo).FirstOrDefault();
            //    if (kat != null)
            //    {
            //        //yerleştirme kaydı yapılır
            //        var stok = new Yerlestirme();
            //        var tmp2 = stok.Detail(kat.Value, item.MalKodu, item.Birim);
            //        if (tmp2 != null)
            //        {
            //            var x = db.GorevYers.Where(m => m.YerID == tmp2.ID && m.GorevID == item.GorevID && m.MalKodu == item.MalKodu && m.Birim == item.Birim).FirstOrDefault();
            //            if (tmp2.Miktar >= item.Miktar && item.Miktar <= (x.Miktar - (x.YerlestirmeMiktari != null ? x.YerlestirmeMiktari : 0)))
            //            {
            //                tmp2.Miktar -= item.Miktar;
            //                stok.Update(tmp2, item.IrsID, kulID, true);
            //                //raftan indirdiğini kaydet
            //                db.TerminalRaftanIndir(item.IrsID, kat, item.Miktar);
            //            }
            //        }
            //    }
            //}
            return new Result(true);
        }
        /// <summary>
        /// sipariş toplama görevi tamamlma
        /// </summary>
        [WebMethod]
        public Result Transfer_GoreviTamamla(int GorevID, int kulID)
        {
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "İrsaliye bulunamadı !");
            //var tmpYer = mGorev.GorevYers.Where(m => m.YerlestirmeMiktari < m.Miktar).FirstOrDefault();
            //if (tmpYer.IsNotNull())
            //    return new Result(false, "İşlem bitmemiş !");
            ////kaydeden bulunur
            //string kaydeden = db.Users.Where(m => m.ID == kulID).Select(m => m.Kod).FirstOrDefault();
            //int seritipi = ComboItems.EArşiv.ToInt32();
            ////liste getirilir
            //string sql = string.Format("SELECT wms.IRS.SirketKod, wms.GorevIRS.IrsaliyeID, wms.IRS.Tarih, wms.IRS.HesapKodu, wms.IRS.TeslimCHK, ISNULL(wms.IRS.ValorGun,0) as ValorGun, wms.IRS.EvrakNo " +
            //                            "FROM wms.GorevIRS INNER JOIN wms.IRS ON wms.GorevIRS.IrsaliyeID = wms.IRS.ID " +
            //                            "WHERE (wms.GorevIRS.GorevID = {0}) " +
            //                            "GROUP BY wms.IRS.SirketKod, wms.GorevIRS.IrsaliyeID, wms.IRS.Tarih, wms.IRS.HesapKodu, wms.IRS.TeslimCHK, wms.IRS.ValorGun, wms.IRS.EvrakNo", mGorev.ID);
            //var list = db.Database.SqlQuery<STIMax>(sql).ToList();
            //int tarih = DateTime.Today.ToOADateInt(), saat = DateTime.Now.SaatiAl();
            //foreach (var item in list)
            //{
            //    string evrakserino = db.EvrakSeris.Where(m => m.SirketKodu == item.SirketKod && m.Tip == seritipi).Select(m => m.SeriNo).FirstOrDefault();
            //    //listedeki her eleman için döngü yapılır
            //    var sonuc = SiparisToplamaToLink(item.SirketKod, item.IrsaliyeID, mGorev.Depo.DepoKodu, evrakserino, item.Tarih, item.HesapKodu, kaydeden);
            //    if (sonuc.Status == true)
            //    {
            //        string gorevNo = db.SettingsGorevNo(tarih).FirstOrDefault();
            //        var x = db.InsertIrsaliye(item.SirketKod, mGorev.DepoID, gorevNo, item.EvrakNo, item.Tarih, "Irs: " + item.EvrakNo + " Alıcı: " + item.HesapKodu.GetUnvan(item.SirketKod), true, ComboItems.Paketle.ToInt32(), kulID, kaydeden, tarih, saat, item.HesapKodu, item.TeslimChk, item.ValorGun, "").FirstOrDefault();
            //    }
            //}
            //db.TerminalFinishGorev(GorevID, mGorev.IrsaliyeID, "", tarih, saat, kulID, "", ComboItems.SiparişTopla.ToInt32(), 0);
            return new Result(true);
        }
        /// <summary>
        /// dispose override
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
