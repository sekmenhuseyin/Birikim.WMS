using Birikim.Helpers;
using Birikim.Models;
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
using Wms12m.Entity.Mysql;

namespace Wms12m.Presentation
{
    /// <summary>
    /// el Terminalleri için web servisi
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
            // log in actions
            Result result;
            using (var _Person = new Persons())
                result = _Person.Login(new User() { Kod = userID.Left(5), Sifre = sifre }, "Terminal");
            // check result
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
            var guid = barkod.Left(8).ToLower();
            var userID = barkod.Remove(0, 8).ToInt32();
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
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new List<GorevOzet>();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new List<GorevOzet>();
            // return
            var sql = string.Format("EXEC BIRIKIM.wms.TerminalGorevOzet @DepoID = {0}, @Username = '{1}'", ID, tbl.Kod);
            return db.Database.SqlQuery<GorevOzet>(sql).ToList();
        }

        /// <summary>
        /// irsaliyeleri getir
        /// </summary>
        [WebMethod]
        public List<Tip_IRS> GetIrsaliyeList(int KullID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new List<Tip_IRS>();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new List<Tip_IRS>();
            // return
            return db.IRS.Select(m => new Tip_IRS { ID = m.ID, DepoID = m.Depo.DepoKodu, EvrakNo = m.EvrakNo, HesapKodu = m.HesapKodu, SirketKod = m.SirketKod, Tarih = m.Tarih.ToString(), TeslimCHK = m.TeslimCHK, Unvan = "" }).ToList();
        }

        /// <summary>
        /// seçili irsaliyenin bilgileri
        /// </summary>
        [WebMethod]
        public Tip_IRS GetIrsaliye(int GorevID, int KullID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Tip_IRS();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new Tip_IRS();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            if (mGorev.IsNull() || mGorev.IR == null)
                return new Tip_IRS();
            // return
            var sql = string.Format("EXEC FINSAT6{0}.wms.TerminalGetIrsaliye @GorevID = {1}", mGorev.IR.SirketKod, mGorev.ID);
            return db.Database.SqlQuery<Tip_IRS>(sql).FirstOrDefault();
        }

        /// <summary>
        /// seçili malkoduna malzeme ait bilgileri
        /// </summary>
        [WebMethod]
        public List<Tip_STI2> GetMalKoduMalzemes(string MalKodu, int GorevID, int KullID, bool devamMi, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new List<Tip_STI2>();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new List<Tip_STI2>();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            if (mGorev.IsNull()) return new List<Tip_STI2>();
            var sql = "";
            if (mGorev.GorevTipiID == ComboItems.SiparişTopla.ToInt32() || mGorev.GorevTipiID == ComboItems.TransferÇıkış.ToInt32() || mGorev.GorevTipiID == ComboItems.KontrolSayım.ToInt32())
                sql = string.Format("EXEC FINSAT6{0}.wms.TerminalGetMalzemeSiparis {1}, {2}", mGorev.IR.SirketKod, GorevID, devamMi == true && mGorev.GorevTipiID != ComboItems.KontrolSayım.ToInt32() ? 1 : 0);
            else if (mGorev.GorevTipiID == ComboItems.TransferGiriş.ToInt32() && mGorev.Transfers.Count == 0)
                sql = string.Format("EXEC FINSAT6{0}.wms.TerminalGetMalzemeTransfer {1}, {2}", mGorev.IR.SirketKod, GorevID, devamMi == true ? 1 : 0);
            else
                sql = string.Format("EXEC FINSAT6{0}.wms.TerminalGetMalzemeGenel {1}, {2}, {3}, {4}", mGorev.IR.SirketKod, GorevID, devamMi == true ? 1 : 0, mGorev.GorevTipiID == ComboItems.MalKabul.ToInt32() || mGorev.GorevTipiID == ComboItems.Paketle.ToInt32() || mGorev.GorevTipiID == ComboItems.Sevket.ToInt32() ? 1 : 0, mGorev.GorevTipiID == ComboItems.Paketle.ToInt32() || mGorev.GorevTipiID == ComboItems.BarkodHazırla.ToInt32() || mGorev.GorevTipiID == ComboItems.Sevket.ToInt32() ? 1 : 0);
            try
            {
                return db.Database.SqlQuery<Tip_STI2>(sql).Where(m => m.MalKodu == MalKodu).ToList();
            }
            catch (Exception ex)
            {
                Logger(KullID.ToString(), "Terminal", ex, "Service/Terminal/GetMalzemes");
                return new List<Tip_STI2>();
            }
        }

        /// <summary>
        /// görev listelerini filtreye göre getirir
        /// </summary>
        [WebMethod]
        public List<Tip_GOREV> GetGorevList(string gorevli, int durum, int gorevtipi, int DepoID, int KullID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new List<Tip_GOREV>();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new List<Tip_GOREV>();
            // return
            var gtipi = db.ComboItem_Name.Where(m => m.ID == gorevtipi).Select(m => m.Name).FirstOrDefault();
            gtipi = "Terminal" + gtipi.Replace(" ", "");
            var yetkikontrol = db.GetPermissionFor(KullID, tbl.RoleName, gtipi, "WMS", "Reading").FirstOrDefault().Value;
            if (yetkikontrol == true)
            {
                var sql = string.Format("EXEC BIRIKIM.wms.TerminalGorevList @DurumID = {0}, @DepoID = {1}, @GorevTipiID = {2}, @Gorevli = '{3}', @Username = '{4}'", durum, DepoID, gorevtipi, gorevli, tbl.Kod);
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
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new List<GetGorevlis_Result>();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new List<GetGorevlis_Result>();
            // return
            return db.GetGorevlis(DepoID).ToList();
        }

        /// <summary>
        /// durumları listeler
        /// </summary>
        [WebMethod]
        public List<Durum> GetDurums(int KullID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new List<Durum>();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new List<Durum>();
            // return
            return db.ComboItem_Name.Where(m => m.Visible == true && m.ComboID == (int)Combos.GorevDurum).Select(m => new Durum { ID = m.ID, Name = m.Name }).ToList();
        }

        /// <summary>
        /// paket tiplerini listeler
        /// </summary>
        [WebMethod]
        public List<Durum> GetPaketTip(int KullID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new List<Durum>();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new List<Durum>();
            // return
            return db.ComboItem_Name.Where(m => m.Visible == true && m.ComboID == (int)Combos.PaketTipi).Select(m => new Durum { ID = m.ID, Name = m.Name }).ToList();
        }

        /// <summary>
        /// malzemeleri getir
        /// </summary>
        [WebMethod]
        public List<Tip_STI> GetMalzemes(int GorevID, int KullID, bool devamMi, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new List<Tip_STI>();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new List<Tip_STI>();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            if (mGorev.IsNull()) return new List<Tip_STI>();
            var sql = "";
            if (mGorev.GorevTipiID == ComboItems.SiparişTopla.ToInt32() || mGorev.GorevTipiID == ComboItems.TransferÇıkış.ToInt32() || mGorev.GorevTipiID == ComboItems.KontrolSayım.ToInt32())
                sql = string.Format("EXEC FINSAT6{0}.wms.TerminalGetMalzemeSiparis {1}, {2}", mGorev.IR.SirketKod, GorevID, devamMi == true && mGorev.GorevTipiID != ComboItems.KontrolSayım.ToInt32() ? 1 : 0);
            else if (mGorev.GorevTipiID == ComboItems.TransferGiriş.ToInt32() && mGorev.Transfers.Count == 0)
                sql = string.Format("EXEC FINSAT6{0}.wms.TerminalGetMalzemeTransfer {1}, {2}", mGorev.IR.SirketKod, GorevID, devamMi == true ? 1 : 0);
            else
                sql = string.Format("EXEC FINSAT6{0}.wms.TerminalGetMalzemeGenel {1}, {2}, {3}, {4}", mGorev.IR.SirketKod, GorevID, devamMi == true ? 1 : 0, mGorev.GorevTipiID == ComboItems.MalKabul.ToInt32() || mGorev.GorevTipiID == ComboItems.Paketle.ToInt32() || mGorev.GorevTipiID == ComboItems.Sevket.ToInt32() ? 1 : 0, mGorev.GorevTipiID == ComboItems.Paketle.ToInt32() || mGorev.GorevTipiID == ComboItems.BarkodHazırla.ToInt32() || mGorev.GorevTipiID == ComboItems.Sevket.ToInt32() ? 1 : 0);
            try
            {
                return db.Database.SqlQuery<Tip_STI>(sql).ToList();
            }
            catch (Exception ex)
            {
                Logger(KullID.ToString(), "Terminal", ex, "Service/Terminal/GetMalzemes");
                return new List<Tip_STI>();
            }
        }

        /// <summary>
        /// malzemeyi barkoda göre bulur
        /// </summary>
        [WebMethod]
        public Tip_Malzeme GetMalzemeFromBarcode(string malkodu, string barkod, int GorevID, int KullID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Tip_Malzeme();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new Tip_Malzeme();
            try
            {
                // sql
                var mGorev = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
                var sql = string.Format("EXEC FINSAT6{0}.wms.getStkOzet @Malkodu = '{1}', @Barkod = '{2}'", mGorev.IR.SirketKod, malkodu, barkod);
                //return
                var satir = db.Database.SqlQuery<Tip_Malzeme>(sql).ToList();
                //eğer satır yoksa boş döndür
                if (satir.Count == 0)
                    return new Tip_Malzeme();
                //eğer kontrollü sayımsa ve çok satır varsa haber ver
                else if (satir.Count > 1 && mGorev.GorevTipiID == ComboItems.KabloSayım.ToInt32())
                    return new Tip_Malzeme() { Barkod = "Sistemde bu barkoddan " + satir.Count + " adet bulundu" };
                //tek satırsa onu döndür
                else
                    return satir.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger(KullID.ToString(), "Terminal", ex, "Service/Terminal/GetMalzemeFromBarcode");
                return new Tip_Malzeme();
            }
        }

        /// <summary>
        /// seçili depoda böyle bir raf var mı
        /// </summary>
        [WebMethod]
        public bool IfExistsRaf(int DepoID, string Raf, int KullID, string AuthGiven, string Guid)
        {
            var kat = db.GetHucreKatID(DepoID, Raf).FirstOrDefault();
            return kat != null;
        }

        /// <summary>
        /// Rafraki Stok Kontrolü
        /// </summary>
        [WebMethod]
        public decimal RafStokKontrol(int DepoID, string Raf, string MalKodu)
        {
            var miktar = db.Yers.Where(a => a.HucreAd == Raf && a.MalKodu == MalKodu && a.DepoID == DepoID).Select(a => a.Miktar).FirstOrDefault();
            miktar = miktar.IsNull() ? 0 : miktar;
            return miktar;
        }

        /// <summary>
        /// mal kabul kayıt işlemleri
        /// </summary>
        [WebMethod]
        public Result Mal_Kabul(List<frmMalKabul> StiList, int GorevID, int KullID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görevi kontrol ediniz!");
            // add to gorev user table
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

            foreach (var item in StiList)
            {
                var tmp = IrsaliyeDetay.Detail(item.ID);
                if (tmp.OkutulanMiktar == null) tmp.OkutulanMiktar = 0;
                tmp.OkutulanMiktar += item.OkutulanMiktar;
                try
                {
                    IrsaliyeDetay.Operation(tmp);
                }
                catch (Exception ex)
                {
                    Logger(KullID.ToString(), "Terminal", ex, "Service/Terminal/Mal_Kabul");
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
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görevi kontrol ediniz!");
            // return
            var sql = string.Format("EXEC BIRIKIM.wms.TerminalMalKabul_GorevKontrol {0}", mGorev.ID);
            var tbl = db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
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
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görevi kontrol ediniz !");
            // variables
            var gorevNo = db.SettingsGorevNo(DateTime.Today.ToOADateInt(), mGorev.DepoID).FirstOrDefault();
            var kull = db.Users.Where(m => m.ID == KullID).Select(m => m.Kod).FirstOrDefault();
            SqlExper sqlExper = new SqlExper(mGorev.IR.SirketKod, ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString);
            var finsat = new Finsat(db, mGorev.IR.SirketKod, sqlExper, new FaturaKayit(sqlExper, mGorev.IR.SirketKod, sqlExper, mGorev.IR.SirketKod));
            // loop iraliyes
            foreach (var item in mGorev.IRS.Where(m => m.Onay == true && m.LinkEvrakNo == null))
            {
                var sql = string.Format("SELECT EvrakNo FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo = '{1}') AND (KynkEvrakTip = 3) AND (Chk = '{2}')", item.SirketKod, item.EvrakNo, item.HesapKodu);
                var sti = db.Database.SqlQuery<string>(sql).FirstOrDefault();
                if (sti != null)
                    return new Result(false, item.EvrakNo + " nolu evrak daha önce kullanılmış");
                var KatID = db.GetHucreKatID(item.DepoID, "R-ZR-V").FirstOrDefault();
                if (KatID == null)
                    return new Result(false, "Deponun rezerv katı bulunamadı");
                // send to finsat
                var sonuc = finsat.MalKabul(item, KullID);
                if (sonuc.Status == true)
                {
                    // finish
                    db.TerminalFinishGorev(GorevID, item.ID, gorevNo, DateTime.Today.ToOADateInt(), DateTime.Now.ToOaTime(), kull, item.EvrakNo, ComboItems.MalKabul.ToInt32(), ComboItems.RafaKaldır.ToInt32()).FirstOrDefault();
                    LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "MalKabul_GoreviTamamla", ComboItems.alDüzenle, GorevID, "MalKabul => RafaKaldır");
                    // add to stok
                    sql = string.Format("EXEC FINSAT6{0}.wms.GetIrsDetayfromGorev {1}", mGorev.IR.SirketKod, GorevID);
                    var list = db.Database.SqlQuery<frmIrsDetayfromGorev>(sql).ToList();
                    foreach (var item2 in list)
                    {
                        // yerleştirme kaydı yapılır
                        var tmp2 = Yerlestirme.Detail(KatID.Value, item2.MalKodu, "", item2.MakaraNo);
                        if (tmp2 == null)//yeni kayıt ekle
                        {
                            tmp2 = new Yer()
                            {
                                KatID = KatID.Value,
                                MalKodu = item2.MalKodu,
                                Birim = item2.Birim,
                                Miktar = item2.Miktar.Value
                            };
                            if (item2.MakaraNo != "" && item2.MakaraNo != null) tmp2.MakaraNo = item2.MakaraNo;
                            // kaydet
                            var cevap = Yerlestirme.Insert(tmp2, KullID, "Mal Kabul");
                            if (cevap.Status == false)//tek ihtimal: makara no var ve çok önceki kayıtlarla çakıştı
                            {
                                //tmp2.MakaraNo = "Boş-" + db.SettingsMakaraNo(item.DepoID).FirstOrDefault();
                                //Yerlestirme.Insert(tmp2, KullID, "Mal Kabul");
                                return new Result(false, "Makara no girilmelidir.");
                            }
                        }
                        else//güncelle
                        {
                            tmp2.Miktar += item2.Miktar.Value;
                            Yerlestirme.Update(tmp2, KullID, "Mal Kabul", item2.Miktar.Value, false);
                        }
                    }
                    // add to mysql
                    if (db.Settings.FirstOrDefault().KabloSiparisMySql == true)
                    {
                        sql = string.Format("SELECT FINSAT6{0}.FINSAT6{0}.STK.MalAdi4 as Marka, FINSAT6{0}.FINSAT6{0}.STK.Nesne2 as Cins, FINSAT6{0}.FINSAT6{0}.STK.Kod15 as Kesit, wms.IRS_Detay.Miktar, wms.IRS_Detay.MakaraNo " +
                                                "FROM wms.IRS_Detay INNER JOIN FINSAT6{0}.FINSAT6{0}.STK ON wms.IRS_Detay.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                                                "WHERE (FINSAT6{0}.FINSAT6{0}.STK.Kod1 = 'KKABLO') AND (wms.IRS_Detay.IrsaliyeID = {1}) AND (wms.IRS_Detay.Birim = FINSAT6{0}.FINSAT6{0}.STK.Birim1 OR wms.IRS_Detay.Birim = FINSAT6{0}.FINSAT6{0}.STK.Birim2)", mGorev.IR.SirketKod, mGorev.IrsaliyeID);
                        var stks = db.Database.SqlQuery<frmCableStk>(sql).ToList();
                        if (stks.Count > 0)
                        {
                            try
                            {
                                using (KabloEntities dbx = new KabloEntities())
                                {
                                    var depo = dbx.depoes.Where(m => m.id == mGorev.Depo.KabloDepoID).Select(m => m.depo1).FirstOrDefault();
                                    foreach (var itemx in stks)
                                    {
                                        // sid bul
                                        var sid = dbx.indices.Where(m => m.cins == itemx.Cins && m.kesit == itemx.Kesit).FirstOrDefault();
                                        if (sid == null)
                                        {
                                            sid = new index() { cins = itemx.Cins, kesit = itemx.Kesit, agirlik = 0 };
                                            dbx.indices.Add(sid);
                                            dbx.SaveChanges();
                                        }
                                        // stoğa kaydet
                                        var tbls = new stok()
                                        {
                                            marka = itemx.Marka,
                                            cins = itemx.Cins,
                                            kesit = itemx.Kesit,
                                            sid = sid.id,
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
                                // return new Result(false, "Kablo kaydı hariç her şey tamamlandı!");
                            }
                        }
                    }
                }
                else
                    return new Result(false, sonuc.Message);
            }
            // return if all true: tüm israliyeler biterse görevi kapat
            // görev user ve görev tablosu
            var tbl2 = db.GorevUsers.Where(m => m.GorevID == GorevID).ToList();
            foreach (var item in tbl2)
            {
                item.BitisTarihi = DateTime.Today.ToOADateInt();
            }
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
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görevi kontrol ediniz!");
            // add to gorev user table
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
            // loop
            var _result = new Result(true);
            var Rkat = db.GetHucreKatID(mGorev.IR.DepoID, "R-ZR-V").FirstOrDefault();
            foreach (var item in YerlestirmeList)
            {
                // hücre adından kat id bulunur
                var kat = db.GetHucreKatID(item.DepoID, item.RafNo).FirstOrDefault();
                if (kat == null)
                {
                    return new Result(false, item.RafNo + " adlı yer bulunamadı");
                }
                else
                {
                    // irs detay tablosu güncellenir
                    var tmp = IrsaliyeDetay.Detail(item.IrsDetayID);
                    if (tmp.Miktar < ((tmp.YerlestirmeMiktari ?? 0) + item.Miktar))
                    {
                        return new Result(false, item.MalKodu + " için fazla mal yazılmış");
                    }
                    else
                    {
                        if (tmp.YerlestirmeMiktari == null) tmp.YerlestirmeMiktari = item.Miktar;
                        else tmp.YerlestirmeMiktari += item.Miktar;
                        // rezervden düşürülür
                        var tmp2 = Yerlestirme.Detail(Rkat.Value, item.MalKodu, "", item.MakaraNo);
                        tmp2.Miktar -= item.Miktar;
                        var sonuc = Yerlestirme.Update(tmp2, KullID, "Rafa Kaldır", item.Miktar, true, item.IrsID, item.IrsDetayID);
                        if (sonuc.Status == false)
                            return new Result(false, "Hata: " + sonuc.Message);
                        // yerleştirme kaydı yapılır
                        tmp2 = Yerlestirme.Detail(kat.Value, item.MalKodu, "", item.MakaraNo);
                        if (tmp2 == null || tmp2.MakaraNo.ToString2() != item.MakaraNo.ToString2())
                        {
                            tmp2 = new Yer()
                            {
                                KatID = kat.Value,
                                MalKodu = item.MalKodu,
                                Birim = item.Birim,
                                Miktar = item.Miktar
                            };
                            if (item.MakaraNo != "" || item.MakaraNo != null) tmp2.MakaraNo = item.MakaraNo;
                            sonuc = Yerlestirme.Insert(tmp2, KullID, "Rafa Kaldır", item.IrsID, item.IrsDetayID);
                            if (sonuc.Status == false)
                                return new Result(false, "Hata: " + sonuc.Message);
                        }
                        else
                        {
                            tmp2.Miktar += item.Miktar;
                            sonuc = Yerlestirme.Update(tmp2, KullID, "Rafa Kaldır", item.Miktar, false, item.IrsID, item.IrsDetayID);
                            if (sonuc.Status == false)
                                return new Result(false, "Hata: " + sonuc.Message);
                        }
                        // irs detay kayıt
                        IrsaliyeDetay.Operation(tmp);
                    }
                }
            }
            return _result;
        }

        /// <summary>
        /// rafa kaldır görevi tamamlanınca
        /// </summary>
        [WebMethod]
        public Result RafaKaldir_GoreviTamamla(int GorevID, int KullID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görevi kontrol ediniz !");
            var list = mGorev.IR.IRS_Detay.Where(m => m.IrsaliyeID == mGorev.IrsaliyeID && (m.YerlestirmeMiktari != m.Miktar || m.YerlestirmeMiktari == null)).FirstOrDefault();
            if (list.IsNotNull())
                return new Result(false, "İşlem bitmemiş !");
            // görevi tamamla
            var kull = db.Users.Where(m => m.ID == KullID).Select(m => m.Kod).FirstOrDefault();
            db.TerminalFinishGorev(GorevID, mGorev.IrsaliyeID, "", DateTime.Today.ToOADateInt(), DateTime.Now.ToOaTime(), kull, "", ComboItems.RafaKaldır.ToInt32(), 0).FirstOrDefault();
            LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "RafaKaldir_GoreviTamamla", ComboItems.alDüzenle, GorevID, "RafaKaldır => -");
            // görev user tablosu
            var tbl2 = db.GorevUsers.Where(m => m.GorevID == GorevID).ToList();
            foreach (var item in tbl2)
            {
                item.BitisTarihi = DateTime.Today.ToOADateInt();
            }
            db.SaveChanges();
            return new Result(true);
        }

        /// <summary>
        /// raftan indir
        /// </summary>
        [WebMethod]
        public Result Siparis_Topla(List<frmYerlesme> YerlestirmeList, int KullID, int GorevID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görevi kontrol ediniz !");
            // add to gorev user table
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

            // loop
            var _result = new Result(true);
            foreach (var item in YerlestirmeList)
            {
                List<GorevYer> grv = db.GorevYers.Where(m => m.GorevID == GorevID && m.MalKodu == item.MalKodu).ToList();
                decimal YerlestirmeMiktar = 0;
                decimal Miktar = 0;
                if (grv.IsNotNull())
                {
                    YerlestirmeMiktar = grv.Sum(x => x.YerlestirmeMiktari).ToDecimal();
                    Miktar = grv.Sum(x => x.Miktar).ToDecimal();

                    if (Miktar < (YerlestirmeMiktar + item.Miktar))
                        return new Result(false, "İşlem bitmemiş(Miktar ile Yerleştirme miktarı uyumsuz.)");
                }

                // irsdetay id aslında bizim GorevYerid
                var GorevYerID = item.IrsDetayID;
                if (GorevYerID == 0)
                {
                    var kat = db.GetHucreKatID(item.DepoID, item.RafNo).FirstOrDefault();
                    if (kat != null)
                    {
                        var tmptable = db.Yers.Where(m => m.KatID == kat.Value && m.MalKodu == item.MalKodu && m.Birim == item.Birim && m.Miktar > 0).FirstOrDefault();

                        if (tmptable.IsNull())
                        {
                            _result = new Result(false, item.RafNo + " rafında " + item.MalKodu + " malkoduna ait stok bulunmamaktadır!");
                            return _result;
                        }
                        else
                        {
                            var mik = db.GorevYers.Where(a => a.YerID == kat && a.GorevID == GorevID && a.MalKodu == item.MalKodu).Sum(c => c.YerlestirmeMiktari);
                            if ((mik.IsNull() ? 0 : mik) + item.Miktar > tmptable.Miktar)
                            {
                                _result = new Result(false, item.RafNo + " rafında " + item.MalKodu + " malkoduna ait yeterli stok bulunmamaktadır! Raf Stok Miktar: " + tmptable.Miktar);
                                return _result;
                            }
                        }

                        var grvYer = new GorevYer() { GorevID = GorevID, YerID = tmptable.ID, MalKodu = item.MalKodu, Miktar = 0, YerlestirmeMiktari = item.Miktar, Birim = item.Birim, GC = true };
                        db.GorevYers.Add(grvYer);
                        db.SaveChanges();
                    }
                }
                else
                {
                    // hücre adından kat id bulunur
                    var kat = db.GetHucreKatID(item.DepoID, item.RafNo).FirstOrDefault();
                    if (kat != null)
                    {
                        var grvYer = db.GorevYers.Where(m => m.ID == GorevYerID).FirstOrDefault();
                        if (grvYer == null)//new gorev yer satırı
                        {
                            var tmptable = db.Yers.Where(m => m.KatID == kat.Value && m.MalKodu == item.MalKodu && m.Birim == item.Birim).FirstOrDefault();
                            grvYer = new GorevYer() { GorevID = GorevID, YerID = tmptable.ID, MalKodu = item.MalKodu, Miktar = 0, YerlestirmeMiktari = item.Miktar, Birim = item.Birim, GC = true };
                            db.GorevYers.Add(grvYer);
                        }
                        else//update gorevyer satırı
                        {
                            grvYer.YerlestirmeMiktari = (grvYer.YerlestirmeMiktari ?? 0) + item.Miktar;
                        }

                        db.SaveChanges();
                    }
                    else
                        _result = new Result(false, "İrsaliye bulunamadı !");
                }
            }

            return _result;
        }

        /// <summary>
        /// sipariş toplama görevi tamamlma
        /// </summary>
        [WebMethod]
        public Result SiparisTopla_GoreviTamamla(int GorevID, int KullID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görevi kontrol ediniz!");

            // yeterince okutulmuş mu kontrol edilir
            var kontrol1 = db.Database.SqlQuery<frmSiparisToplaKontrol>(@"SELECT        SUM(wms.GorevYer.Miktar) AS Miktar, ISNULL(SUM(wms.GorevYer.YerlestirmeMiktari),0) AS YerlestirmeMiktari, wms.GorevYer.MalKodu
                                                                        FROM            wms.GorevYer WITH (nolock) INNER JOIN
                                                                                                 wms.Yer WITH (nolock) ON wms.GorevYer.YerID = wms.Yer.ID
                                                                        WHERE        wms.GorevYer.GorevID = " + GorevID + " GROUP BY wms.GorevYer.MalKodu").ToList();
            foreach (var item in kontrol1)
            {
                if (item.Miktar != item.YerlestirmeMiktari)
                    return new Result(false, "İşlem bitmemiş !");
            }

            // kaydeden bulunur
            var kull = db.Users.Where(m => m.ID == KullID).FirstOrDefault();
            if (kull.UserDetail.SatisFaturaSeri == null || kull.UserDetail.SatisIrsaliyeSeri == null)
                return new Result(false, "Bu kullanıcıya ait seri nolar hatalı ! Lütfen terminal yetkilerinden seriyi değiştirin yada Güneşten seçili seri için bir değer verin.");
            if (kull.UserDetail.SatisFaturaSeri.Value < 1 || kull.UserDetail.SatisFaturaSeri.Value > 199 || kull.UserDetail.SatisIrsaliyeSeri.Value < 1 || kull.UserDetail.SatisIrsaliyeSeri.Value > 199)
                return new Result(false, "Bu kullanıcıya ait seri nolar hatalı ! Lütfen terminal yetkilerinden seriyi değiştirin yada Güneşten seçili seri için bir değer verin.");
            // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // TODO: burada satış siparişinin bilgileri değişmiş mi kontrol etmesi lazım
            // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // değiş saat kontrol
            // string sql = string.Format("SELECT wms.GorevIRS.GorevID " +
            //            "FROM wms.IRS_Detay INNER JOIN FINSAT6{0}.FINSAT6{0}.SPI ON wms.IRS_Detay.KynkSiparisID = FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID AND wms.IRS_Detay.KynkSiparisNo = FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo AND wms.IRS_Detay.KynkSiparisSiraNo = FINSAT6{0}.FINSAT6{0}.SPI.SiraNo AND wms.IRS_Detay.KynkSiparisTarih = FINSAT6{0}.FINSAT6{0}.SPI.Tarih AND " +
            //            "wms.IRS_Detay.KynkDegisSaat<> FINSAT6{0}.FINSAT6{0}.SPI.DegisSaat INNER JOIN wms.GorevIRS ON wms.IRS_Detay.IrsaliyeID = wms.GorevIRS.IrsaliyeID " +
            //            "WHERE(wms.GorevIRS.GorevID = {1})", mGorev.IR.SirketKod, GorevID);
            // var kontrol= db.Database.SqlQuery<int>(sql).ToList();
            // if (kontrol.Count != 0)
            //    return new Result(false, "Bu göreve ait siparişler değişmiş, Lütfen bir daha oluşturun");
            // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // liste getirilir
            var sql = string.Format("SELECT wms.IRS.SirketKod, wms.GorevIRS.IrsaliyeID, wms.IRS.Tarih, wms.IRS.HesapKodu, wms.IRS.TeslimCHK, ISNULL(wms.IRS.ValorGun,0) as ValorGun, wms.IRS.EvrakNo " +
                        "FROM wms.GorevIRS INNER JOIN wms.IRS ON wms.GorevIRS.IrsaliyeID = wms.IRS.ID " +
                        "WHERE (wms.GorevIRS.GorevID = {0}) AND (wms.IRS.Onay = 0) " +
                        "GROUP BY wms.IRS.SirketKod, wms.GorevIRS.IrsaliyeID, wms.IRS.Tarih, wms.IRS.HesapKodu, wms.IRS.TeslimCHK, wms.IRS.ValorGun, wms.IRS.EvrakNo, wms.IRS.TeslimCHK", mGorev.ID);
            var list = db.Database.SqlQuery<STIMax>(sql).ToList();
            int tarih = DateTime.Today.ToOADateInt(), saat = DateTime.Now.ToOaTime();
            var hatali = 0;
            foreach (var item in list)
            {
                // muhasebe yılı bulunur
                sql = string.Format(@"EXEC BIRIKIM.dbo.GetSirketMuhasebeYear @SirketKodu = '{0}', @Tarih = {1}", item.SirketKod, item.Tarih);
                var yil = db.Database.SqlQuery<int>(sql).FirstOrDefault();
                sql = string.Format(@"EXEC BIRIKIM.dbo.GetSirketMuhasebeKod @SirketKodu = '{0}', @Tarih = {1}", item.SirketKod, item.Tarih);
                var kod = db.Database.SqlQuery<string>(sql).FirstOrDefault();
                // efatura kullanıcısı mı bul
                sql = string.Format("SELECT EFatKullanici FROM FINSAT6{0}.FINSAT6{0}.CHK WHERE (HesapKodu = '{1}')", item.SirketKod, item.HesapKodu);
                var tmp = db.Database.SqlQuery<short>(sql).FirstOrDefault();
                var efatKullanici = false;
                if (tmp == 1) efatKullanici = true;
                // listedeki her eleman için döngü yapılır
                SqlExper sqlExper = new SqlExper(mGorev.IR.SirketKod, ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString);
                SqlExper sqlExper2 = new SqlExper(kod, ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString);
                var finsat = new Finsat(db, mGorev.IR.SirketKod, sqlExper, new FaturaKayit(sqlExper, mGorev.IR.SirketKod, sqlExper2, kod));
                var sonuc = finsat.FaturaKayıt(item.IrsaliyeID, mGorev.Depo.DepoKodu, efatKullanici, item.Tarih, item.HesapKodu, kull.Kod, kull.UserDetail.SatisIrsaliyeSeri.Value, kull.UserDetail.SatisFaturaSeri.Value, yil);
                if (sonuc.Status == true)
                {
                    // update irsaliye
                    var fatNo = sonuc.Message.Left(sonuc.Message.IndexOf(","));
                    var irsNo = sonuc.Message.Substring(sonuc.Message.IndexOf(",") + 1);
                    db.UpdateIrsaliye(item.IrsaliyeID, fatNo, irsNo);
                    // yeni görev
                    var gorevNo = db.SettingsGorevNo(tarih, mGorev.DepoID).FirstOrDefault();
                    var alıcı = mGorev.IR.HesapKodu;
                    var x = db.InsertIrsaliye(item.SirketKod, mGorev.DepoID, gorevNo, fatNo, item.Tarih, "Fat: " + fatNo + " Alıcı: " + alıcı, true, ComboItems.Paketle.ToInt32(), kull.Kod, tarih, saat, item.HesapKodu, item.TeslimChk, item.ValorGun, "", "").FirstOrDefault();
                    LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "SiparisTopla_GoreviTamamla", ComboItems.alEkle, x.GorevID.Value, "Fat: " + fatNo + " Alıcı: " + alıcı);
                }
                else
                {
                    hatali++;
                    if (sonuc.Message.StartsWith("Bu sipariş kapanmış") == false)
                        return new Result(false, sonuc.Message);
                }
            }
            if (hatali > 0 && hatali != list.Count)
                return new Result(false, "Sipariş kapanmış");
            // yerleştirmeden düşülür
            // gorev yer miktar update
            db.Database.ExecuteSqlCommand("UPDATE wms.GorevYer SET Miktar = YerlestirmeMiktari WHERE GorevID = " + GorevID);
            // for loop 1: irs detay
            var list1 = db.Database.SqlQuery<IRS_Detay>(@"SELECT wms.IRS_Detay.* FROM wms.GorevIRS INNER JOIN  wms.IRS_Detay ON wms.GorevIRS.IrsaliyeID = wms.IRS_Detay.IrsaliyeID WHERE wms.GorevIRS.GorevID = " + GorevID).ToList();
            var list2 = db.Database.SqlQuery<GorevYer>("SELECT ID, GorevID, YerID, MalKodu, Miktar, YerlestirmeMiktari, MakaraNo, Birim, GC, Sira FROM wms.GorevYer WHERE (Miktar > 0) AND GorevID = " + GorevID).ToList();
            List<frmTempGorevYer> tempGorevYer = new List<frmTempGorevYer>();
            foreach (var item3 in list2)
            {
                var tmpTbl = new frmTempGorevYer
                {
                    ID = item3.ID,
                    Aktif = true,
                    Birim = item3.Birim,
                    GC = item3.GC,
                    GorevID = item3.GorevID,
                    MakaraNo = item3.MakaraNo,
                    MalKodu = item3.MalKodu,
                    Miktar = item3.Miktar,
                    YerID = item3.YerID,
                    YerlestirmeMiktari = item3.YerlestirmeMiktari
                };
                tempGorevYer.Add(tmpTbl);
            }

            foreach (var item1 in list1)
            {
                var gerekenMiktar = item1.Miktar;
                foreach (var item2 in tempGorevYer)
                {
                    if (gerekenMiktar > 0)
                    {
                        if (item2.Aktif == true && item2.MalKodu == item2.MalKodu && gerekenMiktar >= item2.Miktar)
                        {
                            item2.Aktif = false;
                            item2.MakaraNo = item1.MakaraNo;
                            item2.IrsaliyeID = item1.IrsaliyeID;
                            item2.YerlestirmeMiktari = item2.Miktar;
                            gerekenMiktar -= item2.Miktar;
                        }
                        else if (item2.Aktif == true && item2.MalKodu == item2.MalKodu && gerekenMiktar < item2.Miktar)
                        {
                            item2.Aktif = true;
                            item2.Miktar = item2.Miktar - gerekenMiktar;
                            item2.YerlestirmeMiktari = item2.Miktar - gerekenMiktar;
                            item2.MakaraNo = item1.MakaraNo;
                            item2.IrsaliyeID = item1.IrsaliyeID;
                            tempGorevYer.Add(new frmTempGorevYer
                            {
                                ID = item2.ID,
                                GorevID = item2.GorevID,
                                YerID = item2.YerID,
                                IrsaliyeID = item2.IrsaliyeID,
                                MalKodu = item2.MalKodu,
                                Birim = item2.Birim,
                                GC = item2.GC,
                                MakaraNo = item2.MakaraNo,
                                PID = item2.PID,
                                IU = item2.IU,
                                Miktar = gerekenMiktar,
                                YerlestirmeMiktari = gerekenMiktar,
                                Aktif = false
                            });
                            gerekenMiktar = 0;
                            break;
                        }
                    }
                }
            }

            foreach (var item2 in tempGorevYer)
            {
                var dusulecek = Yerlestirme.Detail(item2.YerID);
                dusulecek.Miktar -= item2.YerlestirmeMiktari.Value;
                dusulecek.MakaraDurum = false;
                Yerlestirme.Update(dusulecek, KullID, "Sipariş Topla", item2.YerlestirmeMiktari.Value, true, item2.IrsaliyeID);
            }

            // finish gorev
            db.TerminalFinishGorev(GorevID, mGorev.IrsaliyeID, "", tarih, saat, kull.Kod, "", ComboItems.SiparişTopla.ToInt32(), 0).FirstOrDefault();
            // görev user tablosu
            var tbl2 = db.GorevUsers.Where(m => m.GorevID == GorevID).ToList();
            foreach (var item in tbl2)
            {
                item.BitisTarihi = DateTime.Today.ToOADateInt();
            }
            db.SaveChanges();
            LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "SiparisTopla_GoreviTamamla", ComboItems.alDüzenle, GorevID, "SiparişTopla => -");
            // kablo hareketlere kaydet
            if (db.Settings.FirstOrDefault().KabloSiparisMySql == true)
                try
                {
                    using (KabloEntities dbx = new KabloEntities())
                    {
                        // önce depo adını bul
                        var depo = dbx.depoes.Where(m => m.id == mGorev.Depo.KabloDepoID).Select(m => m.depo1).FirstOrDefault();
                        var varmi = false;
                        foreach (var item in mGorev.IRS)
                        {
                            foreach (var item2 in item.IRS_Detay)
                            {
                                // istenen stk bilgilerini bul
                                sql = string.Format("SELECT FINSAT6{0}.FINSAT6{0}.STK.MalAdi4 as Marka, FINSAT6{0}.FINSAT6{0}.STK.Nesne2 as Cins, FINSAT6{0}.FINSAT6{0}.STK.Kod15 as Kesit " +
                                                      "FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) " +
                                                      "WHERE (FINSAT6{0}.FINSAT6{0}.STK.MalKodu = '{1}') AND (FINSAT6{0}.FINSAT6{0}.STK.Kod1 = 'KKABLO')", item.SirketKod, item2.MalKodu);
                                var stk = db.Database.SqlQuery<frmCableStk>(sql).FirstOrDefault();
                                if (stk != null)
                                {
                                    // makarayı bul
                                    var kablo = dbx.stoks.Where(m => m.depo == depo && m.makarano == item2.MakaraNo).FirstOrDefault();
                                    if (kablo != null)
                                    {
                                        // kabloya açık yap
                                        if (kablo.miktar != item2.Miktar)
                                            kablo.makara = "AÇIK";
                                        // yeni hareket ekle
                                        var tblh = new hareket()
                                        {
                                            id = kablo.id,
                                            miktar = item2.Miktar,
                                            musteri = mGorev.IR.HesapKodu,
                                            tarih = DateTime.Now,
                                            kaydigiren = tblx.AdSoyad
                                        };
                                        dbx.harekets.Add(tblh);
                                        varmi = true;
                                    }
                                }
                            }
                        }

                        if (varmi == true) dbx.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Logger(kull.AdSoyad, "Terminal", ex, "Service/Terminal/SiparisTopla_GoreviTamamla");
                }

            return new Result(true);
        }

        /// <summary>
        /// mal kabul kayıt işlemleri
        /// </summary>
        [WebMethod]
        public Result Paketle(List<frmMalKabul> StiList, int GorevID, int KullID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görevi kontrol ediniz!");
            // add to gorev user table
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

            // loop
            var _result = new Result(true);
            try
            {
                foreach (var item in StiList)
                {
                    var tmp = IrsaliyeDetay.Detail(item.ID);
                    if (tmp.OkutulanMiktar == null) tmp.OkutulanMiktar = 0;
                    if (tmp.Miktar >= (tmp.OkutulanMiktar + item.OkutulanMiktar))
                    {
                        tmp.OkutulanMiktar += item.OkutulanMiktar;
                        IrsaliyeDetay.Operation(tmp);
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
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görev bulunamadı !");
            var mIrsaliye = db.IRS.Where(m => m.ID == IrsaliyeID).FirstOrDefault();
            var list = mIrsaliye.IRS_Detay.Where(m => m.IrsaliyeID == mGorev.IrsaliyeID && (m.OkutulanMiktar != m.Miktar || m.OkutulanMiktar == null)).FirstOrDefault();
            if (list.IsNotNull())
                return new Result(false, "İşlem bitmemiş !");
            // paket bilgilerini hazırla
            var tarih = DateTime.Today.ToOADateInt();
            db.SettingsPaketNo(mGorev.DepoID, GorevID, tblx.Kod, tarih);
            LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "Paketle_GoreviTamamla", ComboItems.alEkle, GorevID, "Paket Barkodu");
            // finish gorev
            var gorevNo = db.SettingsGorevNo(tarih, mGorev.DepoID).FirstOrDefault();
            var sevkiyat = db.Settings.Select(m => m.SevkiyatVarmi).FirstOrDefault();
            var id = db.TerminalFinishGorev(GorevID, mGorev.IrsaliyeID, gorevNo, tarih, DateTime.Now.ToOaTime(), tblx.Kod, "", ComboItems.Paketle.ToInt32(), sevkiyat ? ComboItems.Sevket.ToInt32() : 0).FirstOrDefault();
            LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "Paketle_GoreviTamamla", ComboItems.alDüzenle, id.ToInt32(), "Paketle => Sevkiyat");
            // görev user tablosu
            var tbl2 = db.GorevUsers.Where(m => m.GorevID == GorevID).ToList();
            foreach (var item in tbl2)
            {
                item.BitisTarihi = DateTime.Today.ToOADateInt();
            }
            db.SaveChanges();
            // return
            return new Result(true);
        }

        /// <summary>
        /// seçili irsaliyenin bilgileri
        /// </summary>
        [WebMethod]
        public frmGorevPaket GetPackageBarcodeDetails(int GorevID, int KullID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new frmGorevPaket();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new frmGorevPaket();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            if (mGorev.IsNull() || mGorev.IR == null)
                return new frmGorevPaket();
            // return
            return db.Database.SqlQuery<frmGorevPaket>(string.Format("[BIRIKIM].[wms].[TerminalPackageBarcodeDetails] {0}", GorevID)).FirstOrDefault();
        }

        /// <summary>
        /// paketle görevini tamamla
        /// </summary>
        [WebMethod]
        public Result UpdatePackageBarcode(frmGorevPaket pkt, int GorevID, int KullID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görev bulunamadı !");
            // update
            var tbl = db.GorevPaketlers.Where(m => m.GorevID == GorevID).FirstOrDefault();
            tbl.SevkiyatNo = pkt.SevkiyatNo;
            tbl.PaketNo = pkt.PaketNo;
            tbl.PaketTipiID = pkt.PaketTipiID;
            tbl.Adet = pkt.Adet;
            tbl.Agirlik = pkt.Agirlik;
            tbl.Degistiren = tblx.Kod;
            tbl.DegisTarih = DateTime.Today.ToOADateInt();
            tbl.Printed = false;
            db.SaveChanges();
            LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "UpdatePackageBarcode", ComboItems.alDüzenle, GorevID, "Paket Barkodu");
            // return
            return new Result(true);
        }

        /// <summary>
        /// barkoddan irsaliyenin bilgileri
        /// </summary>
        [WebMethod]
        public Tip_IRS GetIrsaliyeFromBarcode(string barkod, int KullID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Tip_IRS();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new Tip_IRS();
            if (barkod == "" || barkod == null)
                return new Tip_IRS();
            // return
            var sql = string.Format("SELECT MIN(GorevPaketler.GorevID) AS ID, wms.Depo.DepoKodu AS DepoID, IRS.HesapKodu, CONVERT(varchar(15), BIRIKIM.dbo.fnRoundUp(wms.GorevPaketler.Agirlik,2)) as TeslimCHK,  BIRIKIM.dbo.fnFormatDateFromInt(wms.GorevPaketler.KayitTarih) AS Tarih, " +
                "(SELECT Unvan1 + ' ' + Unvan2 AS Expr1 FROM FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) WHERE(HesapKodu = IRS.HesapKodu)) AS Unvan, " +
                "(SELECT wms.IRS.EvrakNo + '  ' FROM wms.IRS WITH(nolock) INNER JOIN wms.GorevIRS WITH(nolock) ON wms.IRS.ID = wms.GorevIRS.IrsaliyeID INNER JOIN wms.GorevPaketler ON wms.GorevIRS.GorevID = wms.GorevPaketler.GorevID WHERE(wms.GorevPaketler.PaketNo = '{1}') FOR XML PATH('')) as EvrakNo " +
                "FROM wms.IRS AS IRS WITH(nolock) INNER JOIN wms.Depo WITH(nolock) ON IRS.DepoID = wms.Depo.ID INNER JOIN wms.GorevIRS WITH(nolock) ON IRS.ID = wms.GorevIRS.IrsaliyeID INNER JOIN wms.GorevPaketler ON wms.GorevIRS.GorevID = wms.GorevPaketler.GorevID " +
                "WHERE (wms.GorevPaketler.PaketNo = '{1}') " +
                "GROUP BY wms.Depo.DepoKodu, IRS.HesapKodu, CONVERT(varchar(15), BIRIKIM.dbo.fnRoundUp(wms.GorevPaketler.Agirlik,2)), BIRIKIM.dbo.fnFormatDateFromInt(wms.GorevPaketler.KayitTarih)", db.GetSirkets().FirstOrDefault().Kod, barkod);
            return db.Database.SqlQuery<Tip_IRS>(sql).FirstOrDefault();
        }

        /// <summary>
        /// paketle görevini tamamla
        /// </summary>
        [WebMethod]
        public Result Sevkiyat_GoreviTamamla(int GorevID, int IrsaliyeID, int KullID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görev bulunamadı !");
            // return
            var tarih = DateTime.Today.ToOADateInt();
            var kull = db.Users.Where(m => m.ID == KullID).Select(m => m.Kod).FirstOrDefault();
            db.TerminalFinishGorev(GorevID, IrsaliyeID, "", tarih, DateTime.Now.ToOaTime(), kull, "", ComboItems.Sevket.ToInt32(), 0).FirstOrDefault();
            LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "Sevkiyat_GoreviTamamla", ComboItems.alDüzenle, GorevID, "Sevkiyat => -");
            // görev user tablosu
            var tbl2 = db.GorevUsers.Where(m => m.GorevID == GorevID).ToList();
            foreach (var item in tbl2)
            {
                item.BitisTarihi = DateTime.Today.ToOADateInt();
            }
            db.SaveChanges();
            return new Result(true);
        }

        /// <summary>
        /// transfer çıkış görevleri tamamlma
        /// </summary>
        [WebMethod]
        public Result TransferCikis_GoreviTamamla(int GorevID, int KullID, string AuthGiven, string Guid)
        {
            // kontrols
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Transfer bulunamadı !");
            var tmpYer = mGorev.GorevYers.Where(m => m.GorevID == mGorev.ID && (m.YerlestirmeMiktari < m.Miktar || m.YerlestirmeMiktari == null)).FirstOrDefault();
            if (tmpYer.IsNotNull())
                return new Result(false, "İşlem bitmemiş !");
            // kullanıcı kontrol
            var kull = db.Users.Where(m => m.ID == KullID).FirstOrDefault();
            // aktar
            Result sonuc;
            var tarih = DateTime.Today.ToOADateInt();
            var saat = DateTime.Now.ToOaTime();
            var transfer = mGorev.Transfers.FirstOrDefault();
            if (transfer == null)//iç transfer
            {
                // burada görevyer/irsdetay tablosundaki ürünleri rzrv hücresine atacak

                var KatID = db.GetHucreKatID(mGorev.DepoID, "R-ZR-V").FirstOrDefault();
                foreach (var item2 in mGorev.GorevYers)
                {
                    var dusulecek = Yerlestirme.Detail(item2.YerID);
                    dusulecek.Miktar -= item2.YerlestirmeMiktari.Value;
                    dusulecek.MakaraDurum = false;
                    Yerlestirme.Update(dusulecek, KullID, "Transfer Çıkıs", item2.YerlestirmeMiktari.Value, true, mGorev.IrsaliyeID.Value);
                    // yerleştirme kaydı yapılır
                    var tmp2 = Yerlestirme.Detail(KatID.Value, item2.MalKodu, item2.Birim);
                    if (tmp2 == null)
                    {
                        tmp2 = new Yer()
                        {
                            KatID = KatID.Value,
                            MalKodu = item2.MalKodu,
                            Birim = item2.Birim,
                            Miktar = item2.YerlestirmeMiktari.Value
                        };
                        if (item2.MakaraNo != "" || item2.MakaraNo != null) tmp2.MakaraNo = item2.MakaraNo;
                        Yerlestirme.Insert(tmp2, KullID, "Transfer Çıkıs");
                    }
                    else if (item2.MakaraNo != "" || item2.MakaraNo != null)
                        if (tmp2.MakaraNo != item2.MakaraNo)
                        {
                            tmp2 = new Yer()
                            {
                                KatID = KatID.Value,
                                MalKodu = item2.MalKodu,
                                Birim = item2.Birim,
                                Miktar = item2.YerlestirmeMiktari.Value
                            };
                            tmp2.MakaraNo = item2.MakaraNo;
                            Yerlestirme.Insert(tmp2, KullID, "Transfer Çıkıs");
                        }
                        else
                        {
                            tmp2.Miktar += item2.YerlestirmeMiktari.Value;
                            Yerlestirme.Update(tmp2, KullID, "Transfer Çıkıs", item2.YerlestirmeMiktari.Value, false);
                        }
                }
                // burada görevyer/irsdetay tablosundaki ürünleri rzrv hücresine atacak
                db.TerminalFinishGorev(GorevID, mGorev.IrsaliyeID, "", tarih, DateTime.Now.ToOaTime(), kull.Kod, "", ComboItems.TransferÇıkış.ToInt32(), 0).FirstOrDefault();
                LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "TransferGiris_GoreviTamamla", ComboItems.alDüzenle, GorevID, "TransferÇıkış => -");
                // update gorev table
                var id = mGorev.IR.LinkEvrakNo.ToInt32();
                var tmp = db.Gorevs.Where(m => m.ID == id).FirstOrDefault();
                tmp.DurumID = ComboItems.Açık.ToInt32();
                db.SaveChanges();
                sonuc = new Result(true);
            }
            else//dış transfer
            {
                if (kull.UserDetail.TransferOutSeri == null)
                    return new Result(false, "Bu kullanıcıya ait seri nolar hatalı ! Lütfen terminal yetkilerinden seriyi değiştirin yada Güneşten seçili seri için bir değer verin.");
                if (kull.UserDetail.TransferOutSeri.Value < 1 || kull.UserDetail.TransferOutSeri.Value > 199)
                    return new Result(false, "Bu kullanıcıya ait seri nolar hatalı ! Lütfen terminal yetkilerinden seriyi değiştirin yada Güneşten seçili seri için bir değer verin.");
                SqlExper sqlExper = new SqlExper(mGorev.IR.SirketKod, ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString);
                var finsat = new Finsat(db, mGorev.IR.SirketKod, sqlExper, new FaturaKayit(sqlExper, mGorev.IR.SirketKod, sqlExper, mGorev.IR.SirketKod));
                sonuc = finsat.DepoTransfer(transfer, false, kull.Kod, kull.UserDetail.TransferOutSeri.Value);
                if (sonuc.Status == true)
                {
                    // get depo details
                    var cikisDepo = db.Depoes.Where(m => m.ID == transfer.CikisDepoID).Select(m => m.DepoKodu).FirstOrDefault();
                    var araDepo = db.Depoes.Where(m => m.ID == transfer.AraDepoID).FirstOrDefault();
                    var girisDepo = db.Depoes.Where(m => m.ID == transfer.GirisDepoID).Select(m => m.DepoKodu).FirstOrDefault();
                    var KatID = db.GetHucreKatID(araDepo.ID, "R-ZR-V").FirstOrDefault();
                    // yerleştirmeden düşülür
                    var yerleştirilen = db.Database.SqlQuery<frmSiparisToplayerlestirilen>(@"SELECT        YerID, SUM(YerlestirmeMiktari) AS YerlestirmeMiktari, MalKodu, Birim, MakaraNo
                                                                                            FROM            wms.GorevYer WITH (nolock)
                                                                                            WHERE        GorevID =  " + GorevID + " GROUP BY YerID, MalKodu, Birim, MakaraNo").ToList();
                    foreach (var item2 in yerleştirilen)
                    {
                        var dusulecek = Yerlestirme.Detail(item2.YerID);
                        dusulecek.Miktar -= item2.YerlestirmeMiktari;
                        dusulecek.MakaraDurum = false;
                        Yerlestirme.Update(dusulecek, KullID, "Transfer Çıkıs", item2.YerlestirmeMiktari, true, mGorev.IrsaliyeID.Value);
                        // yerleştirme kaydı yapılır
                        var tmp2 = Yerlestirme.Detail(KatID.Value, item2.MalKodu, item2.Birim);
                        if (tmp2 == null)
                        {
                            tmp2 = new Yer()
                            {
                                KatID = KatID.Value,
                                MalKodu = item2.MalKodu,
                                Birim = item2.Birim,
                                Miktar = item2.YerlestirmeMiktari
                            };
                            if (item2.MakaraNo != "" || item2.MakaraNo != null) tmp2.MakaraNo = item2.MakaraNo;
                            Yerlestirme.Insert(tmp2, KullID, "Transfer Çıkıs");
                        }
                        else if (item2.MakaraNo != "" || item2.MakaraNo != null)
                            if (tmp2.MakaraNo != item2.MakaraNo)
                            {
                                tmp2 = new Yer()
                                {
                                    KatID = KatID.Value,
                                    MalKodu = item2.MalKodu,
                                    Birim = item2.Birim,
                                    Miktar = item2.YerlestirmeMiktari
                                };
                                tmp2.MakaraNo = item2.MakaraNo;
                                Yerlestirme.Insert(tmp2, KullID, "Transfer Çıkıs");
                            }
                            else
                            {
                                tmp2.Miktar += item2.YerlestirmeMiktari;
                                Yerlestirme.Update(tmp2, KullID, "Transfer Çıkıs", item2.YerlestirmeMiktari, false);
                            }
                    }

                    // finish
                    db.TerminalFinishGorev(GorevID, mGorev.IrsaliyeID, "", tarih, DateTime.Now.ToOaTime(), kull.Kod, "", ComboItems.TransferÇıkış.ToInt32(), 0).FirstOrDefault();
                    LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "TransferCikis_GoreviTamamla", ComboItems.alDüzenle, GorevID, "TransferÇıkış: " + cikisDepo + " => " + girisDepo);
                    // görev user tablosu
                    var tbl2 = db.GorevUsers.Where(m => m.GorevID == GorevID).ToList();
                    foreach (var item in tbl2)
                    {
                        item.BitisTarihi = DateTime.Today.ToOADateInt();
                    }
                    // add new irsaliye and görev for giriş
                    var gorevNo = db.SettingsGorevNo(tarih, transfer.GirisDepoID).FirstOrDefault();
                    var cevap = db.InsertIrsaliye(transfer.SirketKod, transfer.GirisDepoID, gorevNo, gorevNo, tarih, "Giriş: " + girisDepo + ", Çıkış: " + cikisDepo, false, ComboItems.TransferGiriş.ToInt32(), kull.Kod, tarih, saat, mGorev.IR.HesapKodu, "", 0, "", "").FirstOrDefault();
                    var grvtbl = db.Gorevs.Where(m => m.ID == cevap.GorevID).FirstOrDefault();
                    grvtbl.DurumID = ComboItems.Açık.ToInt32();
                    // insert irs_detay
                    foreach (var item in mGorev.GorevYers)
                    {
                        var tbli = new IRS_Detay() { IrsaliyeID = cevap.IrsaliyeID.Value, MalKodu = item.MalKodu, Miktar = item.Miktar, Birim = item.Birim };
                        db.IRS_Detay.Add(tbli);
                    }

                    // yeni görev id'yi yaz
                    transfer.GorevID = cevap.GorevID.Value;
                    mGorev.IR.DepoID = transfer.GirisDepoID;
                    mGorev.IR.EvrakNo = sonuc.Data.ToString();
                    db.SaveChanges();
                }
            }

            return sonuc;
        }

        /// <summary>
        /// rafa yerleştir
        /// </summary>
        [WebMethod]
        public Result Transfer_Giris(List<frmYerlesme> YerlestirmeList, int KullID, int GorevID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görevi kontrol ediniz!");
            // add to gorev user table
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

            // loop
            var _result = new Result(true);
            int? Rkat;
            var transfer = mGorev.Transfers.FirstOrDefault();
            if (transfer == null)//iç transfer
            {
                Rkat = db.GetHucreKatID(mGorev.DepoID, "R-ZR-V").FirstOrDefault();
                foreach (var item in YerlestirmeList)
                {
                    // hücre adından kat id bulunur
                    var kat = db.GetHucreKatID(item.DepoID, item.RafNo).FirstOrDefault();
                    if (kat != null)
                    {
                        // irs detay tablosu güncellenir
                        var tmp = IrsaliyeDetay.Detail(item.IrsDetayID);
                        if (tmp.Miktar >= ((tmp.YerlestirmeMiktari ?? 0) + item.Miktar))
                        {
                            if (tmp.YerlestirmeMiktari == null) tmp.YerlestirmeMiktari = item.Miktar;
                            else tmp.YerlestirmeMiktari += item.Miktar;
                            // irs detay kayıt
                            IrsaliyeDetay.Operation(tmp);
                        }
                        else
                            _result = new Result(false, item.MalKodu + " için fazla mal yazılmış");
                    }
                    else
                        _result = new Result(false, item.RafNo + " adlı yer bulunamadı");
                }
            }
            else//dış transfer
            {
                Rkat = db.GetHucreKatID(mGorev.Transfers.FirstOrDefault().AraDepoID, "R-ZR-V").FirstOrDefault();
                foreach (var item in YerlestirmeList)
                {
                    // hücre adından kat id bulunur
                    var kat = db.GetHucreKatID(item.DepoID, item.RafNo).FirstOrDefault();
                    if (kat != null)
                    {
                        // irs detay tablosu güncellenir
                        var tmp = IrsaliyeDetay.Detail(item.IrsDetayID);
                        if (tmp.Miktar >= ((tmp.YerlestirmeMiktari ?? 0) + item.Miktar))
                        {
                            if (tmp.YerlestirmeMiktari == null) tmp.YerlestirmeMiktari = item.Miktar;
                            else tmp.YerlestirmeMiktari += item.Miktar;
                            // irs detay kayıt
                            IrsaliyeDetay.Operation(tmp);
                            // rezervden düşürülür
                            var tmp2 = Yerlestirme.Detail(Rkat.Value, item.MalKodu, item.Birim);
                            tmp2.Miktar -= item.Miktar;
                            Yerlestirme.Update(tmp2, KullID, "Transfer Giriş", item.Miktar, true, item.IrsID);
                            var makarano = tmp2.MakaraNo;
                            // yerleştirme kaydı yapılır
                            tmp2 = Yerlestirme.Detail(kat.Value, item.MalKodu, item.Birim);
                            if (tmp2 == null)
                            {
                                tmp2 = new Yer()
                                {
                                    KatID = kat.Value,
                                    MalKodu = item.MalKodu,
                                    Birim = item.Birim,
                                    Miktar = item.Miktar
                                };
                                if (makarano != "" || makarano != null) tmp2.MakaraNo = makarano;
                                Yerlestirme.Insert(tmp2, KullID, "Transfer Giriş", item.IrsID);
                            }
                            else if (tmp2.MakaraNo != makarano)
                            {
                                tmp2 = new Yer()
                                {
                                    KatID = kat.Value,
                                    MalKodu = item.MalKodu,
                                    Birim = item.Birim,
                                    Miktar = item.Miktar
                                };
                                if (makarano != "" || makarano != null) tmp2.MakaraNo = makarano;
                                Yerlestirme.Insert(tmp2, KullID, "Transfer Giriş", item.IrsID);
                            }
                            else
                            {
                                tmp2.Miktar += item.Miktar;
                                Yerlestirme.Update(tmp2, KullID, "Transfer Giriş", item.Miktar, false, item.IrsID);
                            }
                        }
                        else
                            _result = new Result(false, item.MalKodu + " için fazla mal yazılmış");
                    }
                    else
                        _result = new Result(false, item.RafNo + " adlı yer bulunamadı");
                }
            }

            return _result;
        }

        /// <summary>
        /// transfer giriş görevleri tamamlma
        /// </summary>
        [WebMethod]
        public Result TransferGiris_GoreviTamamla(int GorevID, int KullID, string AuthGiven, string Guid)
        {
            // kontrols
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Transfer bulunamadı !");
            var tmpYer = mGorev.IR.IRS_Detay.Where(m => m.IrsaliyeID == mGorev.IrsaliyeID && (m.YerlestirmeMiktari < m.Miktar || m.YerlestirmeMiktari == null)).FirstOrDefault();
            if (tmpYer.IsNotNull())
                return new Result(false, "İşlem bitmemiş !");
            // kullanıcı kontrol
            var kull = db.Users.Where(m => m.ID == KullID).FirstOrDefault();
            if (kull.UserDetail.TransferInSeri == null)
                return new Result(false, "Bu kullanıcıya ait seri nolar hatalı ! Lütfen terminal yetkilerinden seriyi değiştirin yada Güneşten seçili seri için bir değer verin.");
            if (kull.UserDetail.TransferInSeri < 1 || kull.UserDetail.TransferInSeri > 199)
                return new Result(false, "Bu kullanıcıya ait seri nolar hatalı ! Lütfen terminal yetkilerinden seriyi değiştirin yada Güneşten seçili seri için bir değer verin.");
            // aktar
            // görev bitir
            Result sonuc;
            var tarih = DateTime.Today.ToOADateInt();
            var transfer = mGorev.Transfers.FirstOrDefault();
            if (transfer == null)//iç transfer
            {
                var Rkat = db.GetHucreKatID(mGorev.DepoID, "R-ZR-V").FirstOrDefault();
                List<Tip_STI> list = new List<Tip_STI>();
                list = db.Database.SqlQuery<Tip_STI>(string.Format("[BIRIKIM].[wms].[GetSTIList] {0},'{1}',{2},{3}", false, mGorev.ComboItem_Name1.Name, GorevID, mGorev.Transfers.Count)).ToList();
                foreach (var item in list)
                {
                    // hücre adından kat id bulunur
                    var kat = db.GetHucreKatID(mGorev.DepoID, item.Raf).FirstOrDefault();
                    if (kat != null)
                    {
                        // irs detay tablosu güncellenir
                        var tmp = IrsaliyeDetay.Detail(item.ID);
                        if (tmp.Miktar >= (tmp.YerlestirmeMiktari ?? 0))
                        {
                            // rezervden düşürülür
                            var tmp2 = Yerlestirme.Detail(Rkat.Value, item.MalKodu, item.Birim);
                            tmp2.Miktar -= item.Miktar;
                            Yerlestirme.Update(tmp2, KullID, "Transfer Giriş", item.Miktar, true, item.irsID);
                            var makarano = tmp2.MakaraNo;
                            // yerleştirme kaydı yapılır
                            tmp2 = Yerlestirme.Detail(kat.Value, item.MalKodu, item.Birim);
                            if (tmp2 == null)
                            {
                                tmp2 = new Yer()
                                {
                                    KatID = kat.Value,
                                    MalKodu = item.MalKodu,
                                    Birim = item.Birim,
                                    Miktar = item.Miktar
                                };
                                if (makarano != "" || makarano != null) tmp2.MakaraNo = makarano;
                                Yerlestirme.Insert(tmp2, KullID, "Transfer Giriş", item.irsID);
                            }
                            else if (tmp2.MakaraNo != makarano)
                            {
                                tmp2 = new Yer()
                                {
                                    KatID = kat.Value,
                                    MalKodu = item.MalKodu,
                                    Birim = item.Birim,
                                    Miktar = item.Miktar
                                };
                                if (makarano != "" || makarano != null) tmp2.MakaraNo = makarano;
                                Yerlestirme.Insert(tmp2, KullID, "Transfer Giriş", item.irsID);
                            }
                            else
                            {
                                tmp2.Miktar += item.Miktar;
                                Yerlestirme.Update(tmp2, KullID, "Transfer Giriş", item.Miktar, false, item.irsID);
                            }
                        }
                        else
                            sonuc = new Result(false, item.MalKodu + " için fazla mal yazılmış");
                    }
                    else
                        sonuc = new Result(false, item.Raf + " adlı yer bulunamadı");
                }

                db.TerminalFinishGorev(GorevID, mGorev.IrsaliyeID, "", tarih, DateTime.Now.ToOaTime(), kull.Kod, "", ComboItems.TransferÇıkış.ToInt32(), 0).FirstOrDefault();
                LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "TransferGiris_GoreviTamamla", ComboItems.alDüzenle, GorevID, "TransferÇıkış => -");
                sonuc = new Result(true);
            }
            else//dış transfer
            {
                SqlExper sqlExper = new SqlExper(mGorev.IR.SirketKod, ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString);
                var finsat = new Finsat(db, mGorev.IR.SirketKod, sqlExper, new FaturaKayit(sqlExper, mGorev.IR.SirketKod, sqlExper, mGorev.IR.SirketKod));
                sonuc = finsat.DepoTransfer(mGorev.Transfers.FirstOrDefault(), true, kull.Kod, kull.UserDetail.TransferInSeri.Value);
                if (sonuc.Status == true)
                {
                    // update irsaliye
                    db.UpdateIrsaliye(mGorev.IrsaliyeID, sonuc.Data.ToString(), "");
                    // finish
                    db.TerminalFinishGorev(GorevID, mGorev.IrsaliyeID, "", tarih, DateTime.Now.ToOaTime(), kull.Kod, "", ComboItems.TransferGiriş.ToInt32(), 0).FirstOrDefault();
                    LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "TransferGiris_GoreviTamamla", ComboItems.alDüzenle, GorevID, "TransferGiriş => -");
                    // görev user tablosu
                    var tbl2 = db.GorevUsers.Where(m => m.GorevID == GorevID).ToList();
                    foreach (var item in tbl2)
                    {
                        item.BitisTarihi = DateTime.Today.ToOADateInt();
                    }
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
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görev bulunamadı !");
            // add to gorev user table
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

            // loop the list
            foreach (var item in StiList)
            {
                // görev yer tablosunda var mı dye kontrol ediyoruz
                GorevYer tbl2;
                if (item.IrsDetayID == 0)
                {
                    var kontroltemp = db.GorevYers.Where(m => m.GorevID == GorevID && m.MalKodu == item.MalKodu && m.Yer.HucreAd == item.RafNo);
                    if (item.MakaraNo != "" && item.MakaraNo != null)
                        kontroltemp = kontroltemp.Where(m => m.MakaraNo == item.MakaraNo);
                    var kontrol = kontroltemp.FirstOrDefault();
                    if (kontrol != null) item.IrsDetayID = kontrol.ID;
                }
                //eğer görev yer tablosunda kayıt yoksa yeni ekle
                if (item.IrsDetayID == 0)
                {
                    //önce yer tablosu için kayda bak, oksa ekle
                    var katID = db.GetHucreKatID(mGorev.DepoID, item.RafNo).FirstOrDefault();
                    if (katID != null)
                    {
                        var yertemp = db.Yers.Where(m => m.KatID == katID && m.MalKodu == item.MalKodu);
                        if (item.MakaraNo != "" && item.MakaraNo != null)
                            yertemp = yertemp.Where(m => m.MakaraNo == item.MakaraNo);
                        var yert = yertemp.FirstOrDefault();
                        if (yert == null)
                        {
                            yert = new Yer() { KatID = katID.Value, MalKodu = item.MalKodu, Birim = item.Birim, Miktar = 0 };
                            if (item.MakaraNo != "" && item.MakaraNo != null)
                                yert.MakaraNo = item.MakaraNo;
                            db.Yers.Add(yert);
                            db.SaveChanges();
                        }
                        //sonra görevyer tablosuna ekle
                        tbl2 = new GorevYer() { GorevID = GorevID, MalKodu = item.MalKodu, Birim = item.Birim, Miktar = item.Miktar, YerlestirmeMiktari = item.Miktar, GC = false, YerID = yert.ID, MakaraNo = item.MakaraNo };
                        db.GorevYers.Add(tbl2);
                    }
                }
                //görevyer tablosunda kayı varsa onu güncelle
                else
                {
                    tbl2 = db.GorevYers.Where(m => m.ID == item.IrsDetayID).FirstOrDefault();
                    if ((tbl2.Miktar + item.Miktar) < 0)
                    {
                        tbl2.Miktar = 0;
                    }
                    else
                    {
                        tbl2.Miktar += item.Miktar;
                    }
                    //toplam miktar 0dan küçükse 0 yap
                    if ((tbl2.YerlestirmeMiktari + item.Miktar) < 0)
                    {
                        tbl2.YerlestirmeMiktari = 0;
                    }
                    else
                    {
                        tbl2.YerlestirmeMiktari += item.Miktar;
                    }
                }

                // kaydetme işlemleri
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
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görev bulunamadı !");
            // update gorev user table
            var tbl = db.GorevUsers.Where(m => m.GorevID == GorevID && m.UserName == tblx.Kod).FirstOrDefault();
            if (tbl != null)
            {
                tbl.BitisTarihi = DateTime.Today.ToOADateInt();
            }
            else
            {
                tbl = new GorevUser()
                {
                    UserName = tblx.Kod,
                    GorevID = GorevID,
                    BaslamaTarihi = DateTime.Today.ToOADateInt(),
                    BitisTarihi = DateTime.Today.ToOADateInt()
                };
                db.GorevUsers.Add(tbl);
            }
            try
            {
                db.SaveChanges();
                LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "KontrolluSay_GoreviTamamla", ComboItems.alDüzenle, GorevID, "KontrolluSay => -");
                return new Result(true);
            }
            catch (Exception ex)
            {
                Logger(KullID.ToString(), "Terminal", ex, "Service/Terminal/KontrolluSay_GoreviTamamla");
                return new Result(false, "Hata oldu");
            }
        }

        /// <summary>
        /// Alimdan İade İşlem Yap
        /// </summary>
        [WebMethod]
        public Result AlimdanIade(List<frmYerlesme> YerlestirmeList, int KullID, int GorevID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görevi kontrol ediniz !");
            // add to gorev user table
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

            // loop
            var _result = new Result(true);
            foreach (var item in YerlestirmeList)
            {
                // irsdetay id aslında bizim GorevYerid
                var GorevYerID = item.IrsDetayID;
                if (GorevYerID == 0)
                {
                    var kat = db.GetHucreKatID(item.DepoID, item.RafNo).FirstOrDefault();
                    if (kat != null)
                    {
                        var tmptable = db.Yers.Where(m => m.KatID == kat.Value && m.MalKodu == item.MalKodu && m.Birim == item.Birim).FirstOrDefault();
                        var grvYer = new GorevYer() { GorevID = GorevID, YerID = tmptable.ID, MalKodu = item.MalKodu, Miktar = 0, YerlestirmeMiktari = item.Miktar, Birim = item.Birim, GC = true };
                        db.GorevYers.Add(grvYer);
                        db.SaveChanges();
                    }
                }
                else
                {
                    // hücre adından kat id bulunur
                    var kat = db.GetHucreKatID(item.DepoID, item.RafNo).FirstOrDefault();
                    if (kat != null)
                    {
                        var grvYer = db.GorevYers.Where(m => m.ID == GorevYerID).FirstOrDefault();
                        if (grvYer == null)//new gorev yer satırı
                        {
                            var tmptable = db.Yers.Where(m => m.KatID == kat.Value && m.MalKodu == item.MalKodu && m.Birim == item.Birim).FirstOrDefault();
                            grvYer = new GorevYer() { GorevID = GorevID, YerID = tmptable.ID, MalKodu = item.MalKodu, Miktar = 0, YerlestirmeMiktari = item.Miktar, Birim = item.Birim, GC = true };
                            db.GorevYers.Add(grvYer);
                        }
                        else//update gorevyer satırı
                        {
                            grvYer.YerlestirmeMiktari = (grvYer.YerlestirmeMiktari ?? 0) + item.Miktar;
                        }

                        db.SaveChanges();
                    }
                    else
                        _result = new Result(false, "İrsaliye bulunamadı !");
                }
            }

            return _result;
        }

        /// <summary>
        /// Alimdan İade Görevi Tamamla
        /// </summary>
        [WebMethod]
        public Result AlimdanIade_GoreviTamamla(int GorevID, int KullID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görevi kontrol ediniz!");

            // yeterince okutulmuş mu kontrol edilir
            var kontrol1 = db.Database.SqlQuery<frmSiparisToplaKontrol>(@"SELECT        SUM(wms.GorevYer.Miktar) AS Miktar, ISNULL(SUM(wms.GorevYer.YerlestirmeMiktari),0) AS YerlestirmeMiktari, wms.GorevYer.MalKodu
                                                                        FROM            wms.GorevYer WITH (nolock) INNER JOIN
                                                                                                 wms.Yer WITH (nolock) ON wms.GorevYer.YerID = wms.Yer.ID
                                                                        WHERE        wms.GorevYer.GorevID = " + GorevID + " GROUP BY wms.GorevYer.MalKodu").ToList();
            foreach (var item in kontrol1)
            {
                if (item.Miktar != item.YerlestirmeMiktari)
                    return new Result(false, "İşlem bitmemiş !");
            }

            // kaydeden bulunur
            var kull = db.Users.Where(m => m.ID == KullID).FirstOrDefault();
            if (kull.UserDetail.SatisFaturaSeri == null || kull.UserDetail.SatisIrsaliyeSeri == null)
                return new Result(false, "Bu kullanıcıya ait seri nolar hatalı ! Lütfen terminal yetkilerinden seriyi değiştirin yada Güneşten seçili seri için bir değer verin.");
            if (kull.UserDetail.SatisFaturaSeri.Value < 1 || kull.UserDetail.SatisFaturaSeri.Value > 199 || kull.UserDetail.SatisIrsaliyeSeri.Value < 1 || kull.UserDetail.SatisIrsaliyeSeri.Value > 199)
                return new Result(false, "Bu kullanıcıya ait seri nolar hatalı ! Lütfen terminal yetkilerinden seriyi değiştirin yada Güneşten seçili seri için bir değer verin.");
            // liste getirilir
            var sql = string.Format("SELECT wms.IRS.SirketKod, wms.GorevIRS.IrsaliyeID, wms.IRS.Tarih, wms.IRS.HesapKodu, wms.IRS.TeslimCHK, ISNULL(wms.IRS.ValorGun,0) as ValorGun, wms.IRS.EvrakNo " +
                        "FROM wms.GorevIRS INNER JOIN wms.IRS ON wms.GorevIRS.IrsaliyeID = wms.IRS.ID " +
                        "WHERE (wms.GorevIRS.GorevID = {0}) AND (wms.IRS.Onay = 0) " +
                        "GROUP BY wms.IRS.SirketKod, wms.GorevIRS.IrsaliyeID, wms.IRS.Tarih, wms.IRS.HesapKodu, wms.IRS.TeslimCHK, wms.IRS.ValorGun, wms.IRS.EvrakNo", mGorev.ID);
            var list = db.Database.SqlQuery<STIMax>(sql).ToList();
            int tarih = DateTime.Today.ToOADateInt(), saat = DateTime.Now.ToOaTime();
            foreach (var item in list)
            {
                // muhasebe yılı bulunur
                sql = string.Format(@"EXEC BIRIKIM.dbo.GetSirketMuhasebeYear @SirketKodu = '{0}', @Tarih = {1}", item.SirketKod, item.Tarih);
                var yil = db.Database.SqlQuery<int>(sql).FirstOrDefault();
                // muhasebe yılı bulunur
                sql = string.Format(@"EXEC BIRIKIM.dbo.GetSirketMuhasebeKod @SirketKodu = '{0}', @Tarih = {1}", item.SirketKod, item.Tarih);
                var kod = db.Database.SqlQuery<string>(sql).FirstOrDefault();
                // efatura kullanıcısı mı bul
                sql = string.Format("SELECT EFatKullanici FROM FINSAT6{0}.FINSAT6{0}.CHK WHERE (HesapKodu = '{1}')", item.SirketKod, item.HesapKodu);
                var tmp = db.Database.SqlQuery<short>(sql).FirstOrDefault();
                var efatKullanici = false;
                if (tmp == 1) efatKullanici = true;
                // listedeki her eleman için döngü yapılır
                SqlExper sqlExper = new SqlExper(mGorev.IR.SirketKod, ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString);
                SqlExper sqlExper2 = new SqlExper(kod, ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString);
                var finsat = new Finsat(db, mGorev.IR.SirketKod, sqlExper, new FaturaKayit(sqlExper, mGorev.IR.SirketKod, sqlExper2, kod));
                var sonuc = finsat.AlımdanIadeFaturaKayıt(item.IrsaliyeID, mGorev.Depo.DepoKodu, efatKullanici, item.Tarih, item.HesapKodu, kull.Kod, kull.UserDetail.AlimdanIadeFaturaSeri.Value, yil);
                if (sonuc.Status == true)
                {
                    // update irsaliye
                    var fatNo = sonuc.Message.Left(sonuc.Message.IndexOf(","));
                    var irsNo = sonuc.Message.Substring(sonuc.Message.IndexOf(",") + 1);
                    db.UpdateIrsaliye(item.IrsaliyeID, fatNo, irsNo);
                    // yeni görev
                    var gorevNo = db.SettingsGorevNo(tarih, mGorev.DepoID).FirstOrDefault();
                    var alıcı = mGorev.IR.HesapKodu;
                    var x = db.InsertIrsaliye(item.SirketKod, mGorev.DepoID, gorevNo, fatNo, item.Tarih, "Fat: " + fatNo + " Alıcı: " + alıcı, true, ComboItems.Paketle.ToInt32(), kull.Kod, tarih, saat, item.HesapKodu, item.TeslimChk, item.ValorGun, "", "").FirstOrDefault();
                    LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "SiparisTopla_GoreviTamamla", ComboItems.alEkle, x.GorevID.Value, "Fat: " + fatNo + " Alıcı: " + alıcı);
                }
                else
                    return new Result(false, sonuc.Message);
            }
            // yerleştirmeden düşülür
            // gorev yer miktar update
            db.Database.ExecuteSqlCommand("UPDATE wms.GorevYer SET Miktar = YerlestirmeMiktari WHERE GorevID = " + GorevID);
            // for loop 1: irs detay
            var list1 = db.Database.SqlQuery<IRS_Detay>(@"SELECT wms.IRS_Detay.* FROM wms.GorevIRS INNER JOIN  wms.IRS_Detay ON wms.GorevIRS.IrsaliyeID = wms.IRS_Detay.IrsaliyeID WHERE wms.GorevIRS.GorevID = " + GorevID).ToList();
            var list2 = db.Database.SqlQuery<GorevYer>("SELECT ID, GorevID, YerID, MalKodu, Miktar, YerlestirmeMiktari, MakaraNo, Birim, GC, Sira FROM wms.GorevYer WHERE (Miktar > 0) AND GorevID = " + GorevID).ToList();
            List<frmTempGorevYer> tempGorevYer = new List<frmTempGorevYer>();
            foreach (var item3 in list2)
            {
                var tmpTbl = new frmTempGorevYer
                {
                    ID = item3.ID,
                    Aktif = true,
                    Birim = item3.Birim,
                    GC = item3.GC,
                    GorevID = item3.GorevID,
                    MakaraNo = item3.MakaraNo,
                    MalKodu = item3.MalKodu,
                    Miktar = item3.Miktar,
                    YerID = item3.YerID,
                    YerlestirmeMiktari = item3.YerlestirmeMiktari
                };
                tempGorevYer.Add(tmpTbl);
            }

            foreach (var item1 in list1)
            {
                var gerekenMiktar = item1.Miktar;
                foreach (var item2 in tempGorevYer)
                {
                    if (gerekenMiktar > 0)
                    {
                        if (item2.Aktif == true && item2.MalKodu == item2.MalKodu && gerekenMiktar >= item2.Miktar)
                        {
                            item2.Aktif = false;
                            item2.MakaraNo = item1.MakaraNo;
                            item2.IrsaliyeID = item1.IrsaliyeID;
                            item2.YerlestirmeMiktari = item2.Miktar;
                            gerekenMiktar -= item2.Miktar;
                        }
                        else if (item2.Aktif == true && item2.MalKodu == item2.MalKodu && gerekenMiktar < item2.Miktar)
                        {
                            item2.Aktif = true;
                            item2.Miktar = item2.Miktar - gerekenMiktar;
                            item2.YerlestirmeMiktari = item2.Miktar - gerekenMiktar;
                            item2.MakaraNo = item1.MakaraNo;
                            item2.IrsaliyeID = item1.IrsaliyeID;
                            tempGorevYer.Add(new frmTempGorevYer
                            {
                                ID = item2.ID,
                                GorevID = item2.GorevID,
                                YerID = item2.YerID,
                                IrsaliyeID = item2.IrsaliyeID,
                                MalKodu = item2.MalKodu,
                                Birim = item2.Birim,
                                GC = item2.GC,
                                MakaraNo = item2.MakaraNo,
                                PID = item2.PID,
                                IU = item2.IU,
                                Miktar = gerekenMiktar,
                                YerlestirmeMiktari = gerekenMiktar,
                                Aktif = false
                            });
                            gerekenMiktar = 0;
                            break;
                        }
                    }
                }

                foreach (var item2 in tempGorevYer)
                {
                    var dusulecek = Yerlestirme.Detail(item2.YerID);
                    dusulecek.Miktar -= item2.YerlestirmeMiktari.Value;
                    dusulecek.MakaraDurum = false;
                    Yerlestirme.Update(dusulecek, KullID, "Alımdan İade", item2.YerlestirmeMiktari.Value, true, item2.IrsaliyeID);
                }
            }

            // finish gorev
            db.TerminalFinishGorev(GorevID, mGorev.IrsaliyeID, "", tarih, saat, kull.Kod, "", ComboItems.SiparişTopla.ToInt32(), 0).FirstOrDefault();
            // görev user tablosu
            var tbl2 = db.GorevUsers.Where(m => m.GorevID == GorevID).ToList();
            foreach (var item in tbl2)
            {
                item.BitisTarihi = DateTime.Today.ToOADateInt();
            }
            db.SaveChanges();
            LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "SiparisTopla_GoreviTamamla", ComboItems.alDüzenle, GorevID, "SiparişTopla => -");
            // kablo hareketlere kaydet
            if (db.Settings.FirstOrDefault().KabloSiparisMySql == true)
                try
                {
                    using (KabloEntities dbx = new KabloEntities())
                    {
                        // önce depo adını bul
                        var depo = dbx.depoes.Where(m => m.id == mGorev.Depo.KabloDepoID).Select(m => m.depo1).FirstOrDefault();
                        var varmi = false;
                        foreach (var item in mGorev.IRS)
                        {
                            foreach (var item2 in item.IRS_Detay)
                            {
                                // istenen stk bilgilerini bul
                                sql = string.Format("SELECT FINSAT6{0}.FINSAT6{0}.STK.MalAdi4 as Marka, FINSAT6{0}.FINSAT6{0}.STK.Nesne2 as Cins, FINSAT6{0}.FINSAT6{0}.STK.Kod15 as Kesit " +
                                                      "FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) " +
                                                      "WHERE (FINSAT6{0}.FINSAT6{0}.STK.MalKodu = '{1}') AND (FINSAT6{0}.FINSAT6{0}.STK.Kod1 = 'KKABLO')", item.SirketKod, item2.MalKodu);
                                var stk = db.Database.SqlQuery<frmCableStk>(sql).FirstOrDefault();
                                if (stk != null)
                                {
                                    // makarayı bul
                                    var kablo = dbx.stoks.Where(m => m.depo == depo && m.makarano == item2.MakaraNo).FirstOrDefault();
                                    // kabloya açık yap
                                    if (kablo.miktar != item2.Miktar)
                                        kablo.makara = "AÇIK";
                                    // yeni hareket ekle
                                    var tblh = new hareket()
                                    {
                                        id = kablo.id,
                                        miktar = item2.Miktar,
                                        musteri = mGorev.IR.HesapKodu,
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
                }

            return new Result(true);
        }

        /// <summary>
        /// satıştan iade kayıt
        /// </summary>
        [WebMethod]
        public Result SatistanIade(List<frmMalKabul> StiList, int GorevID, int KullID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görevi kontrol ediniz!");
            // return
            // add to gorev user table
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

            foreach (var item in StiList)
            {
                var tmp = IrsaliyeDetay.Detail(item.ID);
                if (tmp.OkutulanMiktar == null) tmp.OkutulanMiktar = 0;
                tmp.OkutulanMiktar += item.OkutulanMiktar;
                try
                {
                    IrsaliyeDetay.Operation(tmp);
                }
                catch (Exception ex)
                {
                    Logger(KullID.ToString(), "Terminal", ex, "Service/Terminal/Mal_Kabul");
                }
            }

            return new Result(true);
        }

        /// <summary>
        /// satıştan iade görev tamamla
        /// </summary>
        [WebMethod]
        public Result SatistanIade_GorevKontrol(int GorevID, int KullID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görevi kontrol ediniz!");
            // return
            var sql = string.Format("EXEC BIRIKIM.wms.TerminalMalKabul_GorevKontrol {0}", mGorev.ID);
            var tbl = db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
            if (tbl != 0)
                return new Result(false, -1, "İşlem bitmemiş !");
            return new Result(true);
        }

        /// <summary>
        /// mal kabul onay
        /// </summary>
        [WebMethod]
        public Result SatistanIade_GoreviTamamla(int GorevID, int KullID, string AuthGiven, string Guid)
        {
            // kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new Result(false, "Yetkisiz giriş!");
            Guid = Guid.Cozumle();
            var tblx = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tblx == null) return new Result(false, "Yetkisiz giriş!");
            var durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return new Result(false, "Görevi kontrol ediniz !");
            // variables
            var gorevNo = db.SettingsGorevNo(DateTime.Today.ToOADateInt(), mGorev.DepoID).FirstOrDefault();
            var kull = db.Users.Where(m => m.ID == KullID).FirstOrDefault();
            if (kull.UserDetail.SatisFaturaSeri == null || kull.UserDetail.SatisIrsaliyeSeri == null)
                return new Result(false, "Bu kullanıcıya ait seri nolar hatalı ! Lütfen terminal yetkilerinden seriyi değiştirin yada Güneşten seçili seri için bir değer verin.");
            SqlExper sqlExper = new SqlExper(mGorev.IR.SirketKod, ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString);
            var finsat = new Finsat(db, mGorev.IR.SirketKod, sqlExper, new FaturaKayit(sqlExper, mGorev.IR.SirketKod, sqlExper, mGorev.IR.SirketKod));
            // loop iraliyes
            foreach (var item in mGorev.IRS.Where(m => m.Onay == true))
            {
                var sql = "";
                var KatID = db.GetHucreKatID(item.DepoID, "R-ZR-V").FirstOrDefault();
                if (KatID == null)
                    return new Result(false, "Deponun rezerv katı bulunamadı");
                // muhasebe yılı bulunur
                sql = string.Format(@"EXEC BIRIKIM.dbo.GetSirketMuhasebeYear @SirketKodu = '{0}', @Tarih = {1}", item.SirketKod, item.Tarih);
                var yil = db.Database.SqlQuery<int>(sql).FirstOrDefault();
                // efatura kullanıcısı mı bul
                sql = string.Format("SELECT EFatKullanici FROM FINSAT6{0}.FINSAT6{0}.CHK WHERE (HesapKodu = '{1}')", item.SirketKod, item.HesapKodu);
                var tmp = db.Database.SqlQuery<short>(sql).FirstOrDefault();
                var efatKullanici = false;
                if (tmp == 1) efatKullanici = true;
                // send to finsat
                var sonuc = finsat.SatisIade(item, KullID, kull.UserDetail.SatistanIadeIrsaliyeSeri.Value, yil, efatKullanici, kull.UserDetail.Depo.DepoKodu);
                if (sonuc.Status == true)
                {
                    // finish
                    db.TerminalFinishGorev(GorevID, item.ID, gorevNo, DateTime.Today.ToOADateInt(), DateTime.Now.ToOaTime(), kull.Kod, item.EvrakNo, ComboItems.Satıştanİade.ToInt32(), ComboItems.RafaKaldır.ToInt32()).FirstOrDefault();
                    LogActions(KullID.ToString(), "Terminal", "Service", "Terminal", "SatistanIade_GorevKontrol", ComboItems.alDüzenle, GorevID, "Satış İade => RafaKaldır");
                    // add to stok
                    sql = string.Format("EXEC FINSAT6{0}.wms.GetIrsDetayfromGorev {1}", mGorev.IR.SirketKod, GorevID);
                    var list = db.Database.SqlQuery<frmIrsDetayfromGorev>(sql).ToList();
                    foreach (var item2 in list)
                    {
                        // yerleştirme kaydı yapılır
                        var tmp2 = Yerlestirme.Detail(KatID.Value, item2.MalKodu, item2.Birim);
                        if (tmp2 == null)
                        {
                            tmp2 = new Yer()
                            {
                                KatID = KatID.Value,
                                MalKodu = item2.MalKodu,
                                Birim = item2.Birim,
                                Miktar = item2.Miktar.Value
                            };
                            if (item2.MakaraNo != "" || item2.MakaraNo != null) tmp2.MakaraNo = item2.MakaraNo;
                            Yerlestirme.Insert(tmp2, KullID, "Satıştan İade");
                        }
                        else if (item2.MakaraNo != "" || item2.MakaraNo != null)
                            if (tmp2.MakaraNo != item2.MakaraNo)
                            {
                                tmp2 = new Yer()
                                {
                                    KatID = KatID.Value,
                                    MalKodu = item2.MalKodu,
                                    Birim = item2.Birim,
                                    Miktar = item2.Miktar.Value
                                };
                                tmp2.MakaraNo = item2.MakaraNo;
                                Yerlestirme.Insert(tmp2, KullID, "Satıştan İade");
                            }
                            else
                            {
                                tmp2.Miktar += item2.Miktar.Value;
                                Yerlestirme.Update(tmp2, KullID, "Satıştan İade", item2.Miktar.Value, false);
                            }
                    }

                    // add to mysql
                    if (db.Settings.FirstOrDefault().KabloSiparisMySql == true)
                    {
                        sql = string.Format("SELECT FINSAT6{0}.FINSAT6{0}.STK.MalAdi4 as Marka, FINSAT6{0}.FINSAT6{0}.STK.Nesne2 as Cins, FINSAT6{0}.FINSAT6{0}.STK.Kod15 as Kesit, wms.IRS_Detay.Miktar, wms.IRS_Detay.MakaraNo " +
                                            "FROM wms.IRS_Detay INNER JOIN FINSAT6{0}.FINSAT6{0}.STK ON wms.IRS_Detay.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                                            "WHERE (FINSAT6{0}.FINSAT6{0}.STK.Kod1 = 'KKABLO') AND (wms.IRS_Detay.IrsaliyeID = {1}) AND (wms.IRS_Detay.Birim = FINSAT6{0}.FINSAT6{0}.STK.Birim1 OR wms.IRS_Detay.Birim = FINSAT6{0}.FINSAT6{0}.STK.Birim2)", mGorev.IR.SirketKod, mGorev.IrsaliyeID);
                        var stks = db.Database.SqlQuery<frmCableStk>(sql).ToList();
                        if (stks.Count > 0)
                        {
                            try
                            {
                                using (KabloEntities dbx = new KabloEntities())
                                {
                                    var depo = dbx.depoes.Where(m => m.id == mGorev.Depo.KabloDepoID).Select(m => m.depo1).FirstOrDefault();
                                    foreach (var itemx in stks)
                                    {
                                        // sid bul
                                        var sid = dbx.indices.Where(m => m.cins == itemx.Cins && m.kesit == itemx.Kesit).Select(m => m.id).FirstOrDefault();
                                        // stoğa kaydet
                                        var tbls = new stok()
                                        {
                                            marka = itemx.Marka,
                                            cins = itemx.Cins,
                                            kesit = itemx.Kesit,
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
                                Logger(kull.Kod, "Terminal", ex, "Service/Terminal/SatistanIade_GoreviTamamla");
                                // return new Result(false, "Kablo kaydı hariç her şey tamamlandı!");
                            }
                        }
                    }
                }
                else
                    return new Result(false, sonuc.Message);
            }
            // return if all true: tüm israliyeler biterse görevi kapat
            // görev user ve görev tablosu
            var tbl2 = db.GorevUsers.Where(m => m.GorevID == GorevID).ToList();
            foreach (var item in tbl2)
            {
                item.BitisTarihi = DateTime.Today.ToOADateInt();
            }
            mGorev.DurumID = ComboItems.Tamamlanan.ToInt32();
            db.SaveChanges();
            return new Result(true);
        }
    }
}