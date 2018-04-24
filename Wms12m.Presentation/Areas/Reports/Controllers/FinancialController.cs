using Birikim.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Reports.Controllers
{
    public class FinancialController : RootController
    {
        /// <summary>
        /// cari ciro
        /// </summary>
        public ActionResult CariCiro()
        {

            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            string sql = string.Format("[FINSAT6{0}].[wms].[CariCiroRaporu]", vUser.SirketKodu);
            if (ViewBag.settings.BolgeKoduParametre == true)
                sql += string.Format(" @UserName='{0}'", vUser.UserName);
            var CHK = db.Database.SqlQuery<CariCiroRaporuResult>(sql).ToList();
            return View(CHK);
        }

        /// <summary>
        /// cari ekstre
        /// </summary>
        public ActionResult CariEkstre()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            string sql = string.Format("[FINSAT6{0}].[wms].[CHKSelectKartTip]", vUser.SirketKodu);
            if (ViewBag.settings.BolgeKoduParametre == true)
                sql += string.Format(" @UserName='{0}'", vUser.UserName);
            var CHK = db.Database.SqlQuery<RaporCHKSelect>(sql).ToList();
            return View(CHK);
        }

        public PartialViewResult CariEkstreList(string chk)
        {
            var CE = db.Database.SqlQuery<RaporCariEkstre>(string.Format("[FINSAT6{0}].[wms].[DB_CariEkstre] @HesapKodu = '{1}'", vUser.SirketKodu, chk)).ToList();
            return PartialView("CariEkstreList", CE);
        }

        /// <summary>
        /// Vadesi Gelmemis Cekler
        /// </summary>
        public ActionResult VadesiGelmemisCek()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult VadesiGelmemisCekList(int bastarih, int bittarih)
        {
            string sql = string.Format("[FINSAT6{0}].[wms].[VadesiGelmemisCekler] @BasTarih = {1}, @BitTarih = {2}", vUser.SirketKodu, bastarih, bittarih);
            if (ViewBag.settings.BolgeKoduParametre == true)
                sql += string.Format(",@UserName='{0}'", vUser.UserName);
            var VGC = db.Database.SqlQuery<RaporVadesiGelmemisCekler>(sql).ToList();
            return PartialView("VadesiGelmemisCekList", VGC);
        }

        /// <summary>
        /// çek listesi
        /// </summary>
        public ActionResult Cek()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            var PZ = db.Database.SqlQuery<RaporPozisyonCekSenet>(string.Format("[FINSAT6{0}].[wms].[PozisyonCekSenet]", vUser.SirketKodu)).ToList();
            return View(PZ);
        }

        public PartialViewResult CekList(int pozisyon, int ay, int yil)
        {

            string sql = string.Format("[FINSAT6{0}].[wms].[CekListesiRaporu] @Ay = {1}, @IslemTip = {2}, @Yil = {3}", vUser.SirketKodu, ay, pozisyon, yil);
            if (ViewBag.settings.BolgeKoduParametre == true)
                sql += string.Format(",@UserName='{0}'", vUser.UserName);
            var CLR = db.Database.SqlQuery<RaporCekListesi>(sql).ToList();
            return PartialView("CekList", CLR);
        }

        /// <summary>
        /// toplam risk bakiyesi
        /// </summary>
        public ActionResult RiskBakiye()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult RiskBakiyeList(int bastarih, int bittarih, int basvadetarih, int bitvadetarih, string chk_bas, string chk_bit)
        {
            string sql = string.Format("[FINSAT6{0}].[wms].[ToplamRiskBakiyesi] @BasTarih = {1}, @BitTarih = {2},@VadeBaslangic = {3}, @VadeBitis = {4},@BasHesapKodu= '{5}', @BitHesapKodu = '{6}'", vUser.SirketKodu, bastarih, bittarih, basvadetarih, bitvadetarih, chk_bas, chk_bit);
            if (ViewBag.settings.BolgeKoduParametre == true)
                sql += string.Format(",@UserName='{0}'", vUser.UserName);
            var TRB = db.Database.SqlQuery<RaporToplamRiskBakiyesi>(sql).ToList();
            return PartialView("RiskBakiyeList", TRB);
        }

        /// <summary>
        /// çek portfoy listesi
        /// </summary>
        public ActionResult CekPortfoy()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult CekPortfoyList(int bastarih, int bittarih, int basvadetarih, int bitvadetarih)
        {
            string sql = string.Format("[FINSAT6{0}].[wms].[CekPortfoyListesi] @BasTarih = {1}, @BitTarih = {2},@BasVadeTarih = {3}, @BitVadeTarih = {4}", vUser.SirketKodu, bastarih, bittarih, basvadetarih, bitvadetarih);
            if (ViewBag.settings.BolgeKoduParametre == true)
                sql += string.Format(",@UserName='{0}'", vUser.UserName);
            var GS = db.Database.SqlQuery<RaporCekPortfoyListesi>(sql).ToList();
            return PartialView("CekPortfoyList", GS);

        }

        /// <summary>
        /// ödeme yapmayan müşteriler
        /// </summary>
        /// <returns></returns>
        public ActionResult OdemeYapmayanMusteriler()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult OdemeYapmayanMusterilerList(int gunsayisi)
        {

            string sql = string.Format("[FINSAT6{0}].[wms].[OdemeYapmayanMusteriler] @Number = {1}", vUser.SirketKodu, gunsayisi);
            if (ViewBag.settings.BolgeKoduParametre == true)
                sql += string.Format(",@UserName='{0}'", vUser.UserName);
            var OYM = db.Database.SqlQuery<RaporOdemeYapmayanMusteriler>(sql).ToList();
            return PartialView("OdemeYapmayanMusterilerList", OYM);


        }

        /// <summary>
        /// bakiye
        /// </summary>
        public ActionResult Bakiye()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            string sql = string.Format("[FINSAT6{0}].[wms].[CHKSelectKartTip]", vUser.SirketKodu);
            if (ViewBag.settings.BolgeKoduParametre == true)
                sql += string.Format(" @UserName='{0}'", vUser.UserName);
            var CHK = db.Database.SqlQuery<RaporCHKSelect>(sql).ToList();
            return View(CHK);
        }

        public PartialViewResult BakiyeList(int bastarih, int bittarih, int basvadetarih, int bitvadetarih, string chk_bas, string chk_bit, decimal bakiye)
        {
            string sql = string.Format("[FINSAT6{0}].[wms].[BakiyeRaporu] @BasTarih = {1}, @BitTarih = {2},@VadeBaslangic = {3}, @VadeBitis = {4},@BasHesapKodu = '{5}', @BitHesapKodu = '{6}', @Bakiye = {7}", vUser.SirketKodu, bastarih, bittarih, basvadetarih, bitvadetarih, chk_bas, chk_bit, bakiye);
            if (ViewBag.settings.BolgeKoduParametre == true)
                sql += string.Format(",@UserName='{0}'", vUser.UserName);
            var BR = db.Database.SqlQuery<RaporBakiye>(sql).ToList();
            return PartialView("BakiyeList", BR);

        }

        public string BakiyeDetay(string CHK)
        {
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var CE = db.Database.SqlQuery<RaporCariEkstre>(string.Format("[FINSAT6{0}].[wms].[DB_CariEkstre] @HesapKodu = '{1}'", vUser.SirketKodu, CHK)).ToList();
            return json.Serialize(CE);
        }

        /// <summary>
        /// satış bağlantı
        /// </summary>
        public ActionResult SatisBaglanti()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult SatisBaglantiList(int tip)
        {

            string sql = string.Format("[FINSAT6{0}].[wms].[SatisBaglantiRaporu] @Tip={1}", vUser.SirketKodu, tip);
            if (ViewBag.settings.BolgeKoduParametre == true)
                sql += string.Format(",@UserName='{0}'", vUser.UserName);
            var SBR = db.Database.SqlQuery<SatisBaglatiRapru>(sql).ToList();
            return PartialView("SatisBaglantiList", SBR);


        }

        public string SatBagSozlesmeDetayListesiSelect(string listeNo)
        {
            var STL = db.Database.SqlQuery<BaglantiDetaySelect>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect] @ListeNo='{1}'", vUser.SirketKodu, listeNo)).ToList();
            var json = new JavaScriptSerializer().Serialize(STL);
            return json;
        }

        public string SatBagHareketListesiSelect(string listeNo)
        {
            var STL = db.Database.SqlQuery<SatisBaglantiHareketleri>(string.Format("[FINSAT6{0}].[wms].[SatisBaglantiHareketleri] @SozlesmeNo='{1}'", vUser.SirketKodu, listeNo)).ToList();
            var json = new JavaScriptSerializer().Serialize(STL);
            return json;
        }

        /// <summary>
        /// toplam risk analiz raporu
        /// </summary>
        public ActionResult RiskAnaliz()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult RiskAnalizList(string baschk, string bitchk)
        {
            return PartialView("RiskAnalizList");
        }

        public string RiskAnalizListData(string baschk, string bitchk)
        {
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            string sql = string.Format("[FINSAT6{0}].[wms].[ToplamRiskAnaliziRaporu] @BasHesapKodu='{1}', @BitHesapKodu='{2}'", vUser.SirketKodu, baschk, bitchk);
            if (ViewBag.settings.BolgeKoduParametre == true)
                sql += string.Format(",@UserName='{0}'", vUser.UserName);
            var TRAR = db.Database.SqlQuery<ToplamRiskAnaliziRaporu>(sql).ToList();
            return json.Serialize(TRAR);
        }

        /// <summary>
        /// cari detay
        /// </summary>
        public string CariDetayCek(string CHK, string EvrakNo)
        {
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var CE = db.Database.SqlQuery<RaporCariEkstreCek>(string.Format("[FINSAT6{0}].[wms].[BTB_RP_CariEkstreDetay_Cek] @Tip='', @HesapKodu = '{1}', @EvrakNo='{2}'", vUser.SirketKodu, CHK, EvrakNo)).ToList();
            return json.Serialize(CE);
        }

        public string CariDetayFatura(string CHK, string EvrakNo)
        {
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var CE = db.Database.SqlQuery<RaporCariEkstreFatura>(string.Format("[FINSAT6{0}].[wms].[BTB_RP_CariEkstreDetay_Fatura]  @Tip='', @HesapKodu = '{1}', @EvrakNo='{2}'", vUser.SirketKodu, CHK, EvrakNo)).ToList();
            return json.Serialize(CE);
        }

        public string CariDetayDiger(string CHK, string EvrakNo)
        {
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var CE = db.Database.SqlQuery<RaporCariEkstreDiger>(string.Format("[FINSAT6{0}].[wms].[BTB_RP_CariEkstreDetay_Diger]  @HesapKodu = '{1}', @EvrakNo='{2}'", vUser.SirketKodu, CHK, EvrakNo)).ToList();
            return json.Serialize(CE);
        }

        /// <summary>
        /// sipariş kampanya
        /// </summary>
        public string SiparisKampanyaDetay(string CHK, string EvrakNo, int bastarih, int bittarih)
        {
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var list = db.Database.SqlQuery<KampanyaSiparisDetay>(string.Format("[FINSAT6{0}].[wms].[SiparisKampanyaDetay] @CHK='{1}', @EvrakNo='{2}', @BasTarih={3}, @BitTarih={4}", vUser.SirketKodu, CHK, EvrakNo, bastarih, bittarih)).ToList();
            return json.Serialize(list);
        }

        public JsonResult GetChKCode(string term)
        {
            string sql = string.Format("FINSAT6{0}.[wms].[CHKSearch] @HesapKodu = N'{1}', @Unvan = N'', @top = 200", vUser.SirketKodu, term);
            if (ViewBag.settings.BolgeKoduParametre == true)
                sql += string.Format(",@UserName='{0}'", vUser.UserName);
            var list = db.Database.SqlQuery<frmJson>(sql).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TahsilatKontrol()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public JsonResult TahsilatKontrolList()
        {
            string sql = String.Format("[FINSAT6{0}].[wms].[RP_TahsilatKontrol]", vUser.SirketKodu);
            if (ViewBag.settings.BolgeKoduParametre == true)
                sql += string.Format("@UserName='{0}'", vUser.UserName);
            var list = db.Database.SqlQuery<RP_TahsilatKontrol>(sql).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        #region AksiyonSatis
        public ActionResult AksiyonSatis()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult AksiyonSatisList(int Kod13)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return null; }
            List<AksiyonSatisSelect> OYM;
            try
            {
                string sorgu = "";
                sorgu = String.Format(AksiyonSatisSelect.Sorgu, vUser.SirketKodu, Kod13);
                OYM = db.Database.SqlQuery<AksiyonSatisSelect>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                OYM = new List<AksiyonSatisSelect>();
                Logger(ex, "/Reports/Financial/AksiyonSatisList");
            }
            return PartialView("AksiyonSatisList", OYM);
        }
        #endregion
        #region HedefGrupTanimlariKarti
        public ActionResult HdfGrpTanimKarti()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
            List<RaporGrupKod> _raporGrupKod;
            try
            {
                _raporGrupKod = db.Database.SqlQuery<RaporGrupKod>(String.Format(RaporGrupKod.Sorgu, vUser.SirketKodu)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/HdfGrpTanimKarti");
                _raporGrupKod = new List<RaporGrupKod>();
            }
            ViewBag.YILLAR = HdfGrupProperties(1);
            ViewBag.AYLAR = HdfGrupProperties(2);
            ViewBag.BOLGE = new SelectList(_raporGrupKod, "GrupKod", "GrupKod");
            return View("HdfGrpTanimKarti", new HDF());
        }
        public PartialViewResult HdfGrpTanimKartiList()
        {
            List<HDF> hdfGrpTanimList;
            try
            {
                hdfGrpTanimList = db.Database.SqlQuery<HDF>(String.Format(HDF.SelectSorgu, vUser.SirketKodu, 0)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/HdfGrpTanimKartiList");
                hdfGrpTanimList = new List<HDF>();
            }
            return PartialView("HdfGrpTanimKartiList", hdfGrpTanimList);
        }
        public JsonResult HdfGrpTanimKartiSave(string ID, string AYYIL, string BOLGE, string HEDEF, string yilSelect, string aySelect)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Writing) == false) { return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet); }
            var _Result = new Result(true);
            int snc = 0;
            string sorgu = "";
            switch (Convert.ToInt32(ID))
            {
                case 0:
                    sorgu = String.Format(HDF.HdfGrupTanimInsert, vUser.SirketKodu, BOLGE, HEDEF, Convert.ToInt32(DateTime.Today.ToOADate()), YilAyConvert(yilSelect, aySelect));
                    break;
                default:
                    sorgu = String.Format(HDF.HdfGrupTanimUpdate, vUser.SirketKodu, BOLGE, HEDEF, Convert.ToInt32(ID), AYYIL);
                    break;
            }
            try
            {
                snc = db.Database.SqlQuery<int>(sorgu).FirstOrDefault();
            }
            catch (Exception ex)
            {
                snc = 0;
                Logger(ex, "/Reports/Financial/HdfGrpTanimKartiSave");
                _Result.Status = false;
                _Result.Message = "İşlem başarısız";
            }
            if (snc > 0)
            {
                switch (snc)
                {
                    case 10:
                        _Result.Status = false;
                        _Result.Message = "Girilen grup koduna bu ay içinde kayıt girilmiştir.Kontrol ediniz!";
                        break;
                    case 7:
                        _Result.Status = false;
                        _Result.Message = "Temsilci değerlerinden daha azı girilemez!";
                        break;
                    case 5:
                        _Result.Status = false;
                        _Result.Message = "Değiştirilen grup kodunun temsilcileri bulunduğu için işlem iptal edildi!";
                        break;
                    default:
                        _Result.Status = true;
                        _Result.Message = "İşlem başarılı";
                        break;
                }
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult HdfGrpTanimKartiEdit(int id)
        {
            HDF h;
            List<RaporGrupKod> _raporGrupKod;
            if (CheckPerm(Perms.Raporlar, PermTypes.Updating) == false) { return null; }
            try
            {
                _raporGrupKod = db.Database.SqlQuery<RaporGrupKod>(String.Format(RaporGrupKod.Sorgu, vUser.SirketKodu)).ToList();
                h = db.Database.SqlQuery<HDF>(String.Format(@"
                SELECT H1.ID,H1.TIP,H1.BOLGE,H1.TEMSILCI,H1.URUNGRUP,CONVERT(NVARCHAR,H1.HEDEF) AS HEDEF,H1.TARIH,H1.AYYIL 
                FROM [FINSAT6{0}].[FINSAT6{0}].HDF AS H1 WITH (NOLOCK) WHERE H1.ID={1} AND H1.TIP=0
                ", vUser.SirketKodu, id)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/HdfGrpTanimKartiEdit");
                h = new HDF();
                _raporGrupKod = new List<RaporGrupKod>();
            }
            ViewBag.BOLGE = new SelectList(_raporGrupKod, "GrupKod", "GrupKod", h.BOLGE);
            return PartialView(h);
        }
        public JsonResult HdfGrpTanimKartiDelete(string Id)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Deleting) == false) { return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet); }
            var _Result = new Result(true);
            string sorgu = "";
            int snc = 0;
            sorgu = String.Format(HDF.HdfGrupTanimDelete, vUser.SirketKodu, Id);
            try
            {
                snc = db.Database.SqlQuery<int>(sorgu).FirstOrDefault();
            }
            catch (Exception ex)
            {
                snc = 0;
                Logger(ex, "/Reports/Financial/HdfGrpTanimKartiDelete");
                _Result.Status = false;
                _Result.Message = ex.Message;
            }
            if (snc > 0)
            {
                switch (snc)
                {
                    case 5:
                        _Result.Status = false;
                        _Result.Message = "İlgili bölgenin temsilcileri bulunduğundan kayıt silinemez!";
                        _Result.Id = 0;
                        break;
                    default:
                        _Result.Status = true;
                        _Result.Id = Id.ToInt32();
                        break;
                }
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region TemsilciGrubuTanimlariKarti
        public ActionResult TemsilciGrupTanimlariKarti()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
            List<RaporGrupKod> _raporGrupKod;
            try
            {
                _raporGrupKod = db.Database.SqlQuery<RaporGrupKod>(String.Format(RaporGrupKod.Sorgu, vUser.SirketKodu)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TemsilciGrupTanimlariKarti");
                _raporGrupKod = new List<RaporGrupKod>();
            }
            ViewBag.BOLGE = new SelectList(_raporGrupKod, "GrupKod", "GrupKod");
            ViewBag.TEMSILCI = new SelectList(new List<RaporTemsilci>(), "TipKod", "TipKod");
            ViewBag.YILLAR = HdfGrupProperties(1);
            ViewBag.AYLAR = HdfGrupProperties(2);
            return View("TemsilciGrupTanimlariKarti", new HDF());
        }
        public PartialViewResult TemsilciGrupTanimlariKartiList()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return null; }
            List<HDF> hdfList;
            try
            {
                hdfList = db.Database.SqlQuery<HDF>(String.Format(HDF.SelectSorgu, vUser.SirketKodu, 1)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TemsilciGrupTanimlariKartiList");
                hdfList = new List<HDF>();
            }
            return PartialView(hdfList);
        }
        public JsonResult TemsilciGrupTanimlariKartiSave(string ID, string AYYIL, string BOLGE, string TEMSILCI, string yilSelect, string aySelect, string HEDEF)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Writing) == false) { return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet); }
            var _Result = new Result(true);
            string sorgu = "";
            int snc = 0;
            switch (Convert.ToInt32(ID))
            {
                case 0:
                    sorgu = String.Format(HDF.TemsilciGrupTanimInsert, vUser.SirketKodu, BOLGE, TEMSILCI, HEDEF, Convert.ToInt32(DateTime.Today.ToOADate()), YilAyConvert(yilSelect, aySelect));
                    break;
                default:
                    sorgu = String.Format(HDF.TemsilciGrupTanimUpdate, vUser.SirketKodu, BOLGE, TEMSILCI, HEDEF, Convert.ToInt32(ID), AYYIL);
                    break;
            }
            try
            {
                snc = db.Database.SqlQuery<int>(sorgu).FirstOrDefault();
            }
            catch (Exception ex)
            {
                snc = 0;
                Logger(ex, "/Reports/Financial/TemsilciGrupTanimlariKartiSave");
                _Result.Status = false;
                _Result.Message = "İşlem başarısız";
            }
            if (snc > 0)
            {
                switch (snc)
                {
                    case 10:
                        _Result.Status = false;
                        _Result.Message = "Hedef limiti aşılmıştır.";
                        break;
                    case 5:
                        _Result.Status = false;
                        _Result.Message = "Girilen Bölgeye ait miktar bulunmamaktadır.";
                        break;
                    case 15:
                        _Result.Status = false;
                        _Result.Message = "Girilen tarih aralığı ile ilgili kayıt girilmiştir.";
                        break;
                    default:
                        _Result.Status = true;
                        _Result.Message = "İşlem başarılı";
                        break;
                }
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult TemsilciGrupTanimlariKartiEdit(int id)
        {
            HDF h;
            List<RaporGrupKod> _raporGrupKod;
            if (CheckPerm(Perms.Raporlar, PermTypes.Updating) == false) { return null; }
            try
            {
                _raporGrupKod = db.Database.SqlQuery<RaporGrupKod>(String.Format(RaporGrupKod.Sorgu, vUser.SirketKodu)).ToList();
                h = db.Database.SqlQuery<HDF>(String.Format(@"
                  SELECT H1.ID,H1.TIP,H1.BOLGE,H1.TEMSILCI,H1.URUNGRUP,CONVERT(NVARCHAR,H1.HEDEF) AS HEDEF,H1.TARIH,H1.AYYIL 
                  FROM [FINSAT6{0}].[FINSAT6{0}].HDF AS H1 WITH (NOLOCK) WHERE H1.ID={1} AND H1.TIP=1", vUser.SirketKodu, id)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TemsilciGrupTanimlariKartiEdit");
                h = new HDF();
                _raporGrupKod = new List<RaporGrupKod>();
            }
            ViewBag.BOLGE = new SelectList(_raporGrupKod, "GrupKod", "GrupKod", h.BOLGE);
            ViewBag.Temp = h.TEMSILCI;
            return PartialView(h);
        }
        public JsonResult TemsilciGrupTanimlariKartiDelete(string Id)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Deleting) == false) { return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet); }
            string sorgu = "";
            sorgu = String.Format(HDF.TemsilciGrupTanimDelete, vUser.SirketKodu, Id);
            try
            {
                db.Database.ExecuteSqlCommand(sorgu);
                LogActions("Reports", "Financial", "TemsilciGrupTanimlariKartiDelete", ComboItems.alSil, Id.ToInt32());
                return Json(new Result(true, Id.ToInt32()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TemsilciGrupTanimlariKartiDelete");
                return Json(new Result(false, "Projeye ait form bulunduğu için silme işlemi gerçekleştirilememiştir."), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region UrunGrubuTanimlarKarti
        public ActionResult UrunGrupTanimlariKarti()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
            List<RaporGrupKod> _raporGrupKod;
            List<RaporTargetUrunGrup> _raporTargetUrunGrup;
            try
            {
                _raporGrupKod = db.Database.SqlQuery<RaporGrupKod>(String.Format(RaporGrupKod.Sorgu, vUser.SirketKodu)).ToList();
                _raporTargetUrunGrup = db.Database.SqlQuery<RaporTargetUrunGrup>(String.Format(RaporTargetUrunGrup.Sorgu, vUser.SirketKodu))
                .ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/UrunGrupTanimlariKarti");
                _raporGrupKod = new List<RaporGrupKod>();
                _raporTargetUrunGrup = new List<RaporTargetUrunGrup>();
            }
            ViewBag.BOLGE = new SelectList(_raporGrupKod, "GrupKod", "GrupKod");
            ViewBag.URUNGRUP = new SelectList(_raporTargetUrunGrup, "UrunGrup", "UrunGrup");
            ViewBag.TEMSILCI = new SelectList(new List<RaporTemsilci>(), "TipKod", "TipKod");
            ViewBag.YILLAR = HdfGrupProperties(1);
            ViewBag.AYLAR = HdfGrupProperties(2);
            return View("UrunGrupTanimlariKarti", new HDF());
        }
        public PartialViewResult UrunGrupTanimlariKartiList()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return null; }
            List<HDF> hdfList;
            try
            {
                hdfList = db.Database.SqlQuery<HDF>(String.Format(HDF.SelectSorgu, vUser.SirketKodu, 2)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/UrunGrupTanimlariKartiList");
                hdfList = new List<HDF>();
            }
            return PartialView(hdfList);
        }
        public JsonResult UrunGrupTanimlariKartiSave(string ID, string AYYIL, string BOLGE, string TEMSILCI, string URUNGRUP, string yilSelect, string aySelect, string HEDEF)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Writing) == false) { return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet); }
            var _Result = new Result(true);
            string sorgu = "";
            switch (Convert.ToInt32(ID))
            {
                case 0:
                    sorgu = String.Format(HDF.UrunGrupTanimInsert, vUser.SirketKodu, BOLGE, TEMSILCI, URUNGRUP, HEDEF, Convert.ToInt32(DateTime.Today.ToOADate()), YilAyConvert(yilSelect, aySelect));
                    break;
                default:
                    sorgu = String.Format(HDF.UrunGrupTanimUpdate, vUser.SirketKodu, HEDEF, Convert.ToInt32(ID));
                    break;
            }
            try
            {
                db.Database.ExecuteSqlCommand(sorgu);
                _Result.Status = true;
                _Result.Message = "İşlem başarılı";
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/UrunGrupTanimlariKartiSave");
                _Result.Status = false;
                _Result.Message = "İşlem başarısız";
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult UrunGrupTanimlariKartiEdit(int id)
        {
            HDF h;
            List<RaporGrupKod> _raporGrupKod;
            List<RaporTargetUrunGrup> _raporTargetUrunGrup;
            if (CheckPerm(Perms.Raporlar, PermTypes.Updating) == false) { return null; }
            try
            {
                _raporGrupKod = db.Database.SqlQuery<RaporGrupKod>(String.Format(RaporGrupKod.Sorgu, vUser.SirketKodu)).ToList();
                _raporTargetUrunGrup = db.Database.SqlQuery<RaporTargetUrunGrup>(String.Format(RaporTargetUrunGrup.Sorgu, vUser.SirketKodu)).ToList();
                h = db.Database.SqlQuery<HDF>(String.Format(@"
                  SELECT H1.ID,H1.TIP,H1.BOLGE,H1.TEMSILCI,H1.URUNGRUP,CONVERT(NVARCHAR,H1.HEDEF) AS HEDEF,H1.TARIH,H1.AYYIL 
                  FROM [FINSAT6{0}].[FINSAT6{0}].HDF AS H1 WITH (NOLOCK) WHERE H1.ID={1} AND H1.TIP = 2", vUser.SirketKodu, id)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/UrunGrupTanimlariKartiEdit");
                h = new HDF();
                _raporGrupKod = new List<RaporGrupKod>();
                _raporTargetUrunGrup = new List<RaporTargetUrunGrup>();
            }
            ViewBag.BOLGE = new SelectList(_raporGrupKod, "GrupKod", "GrupKod", h.BOLGE);
            ViewBag.URUNGRUP = new SelectList(_raporTargetUrunGrup, "UrunGrup", "UrunGrup", h.URUNGRUP);
            ViewBag.Temp = h.TEMSILCI;
            return PartialView(h);
        }
        public JsonResult UrunGrupTanimlariKartiDelete(string Id)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Deleting) == false) { return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet); }
            string sorgu = "";
            sorgu = String.Format(HDF.UrunGrupTanimDelete, vUser.SirketKodu, Id);
            try
            {
                db.Database.ExecuteSqlCommand(sorgu);
                LogActions("Reports", "Financial", "UrunGrupTanimlariKartiDelete", ComboItems.alSil, Id.ToInt32());
                return Json(new Result(true, Id.ToInt32()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/UrunGrupTanimlariKartiDelete");
                return Json(new Result(false, "Projeye ait form bulunduğu için silme işlemi gerçekleştirilememiştir."), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region TargetRapor
        public ActionResult TargetRapor()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
            ViewBag.Yillar = HdfGrupProperties(1);
            ViewBag.Aylar = HdfGrupProperties(2);
            return View();
        }
        public PartialViewResult TargetRaporList(string Ay, string Yil)
        {
            ViewBag.Ay = Ay;
            ViewBag.Yil = Yil;
            return PartialView("TargetRaporList");
        }
        public string TargetRaporSelect(string Ay, string Yil)
        {
            ViewBag.Ay = Ay;
            ViewBag.Yil = Yil;
            List<CTargetRapor> tl;
            var json = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            try
            {
                string sorgu = "", r = "";
                decimal toplamCiro = 0;
                r = YilAyConvert(Yil, Ay);
                sorgu = String.Format(CTargetRapor.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil), (Convert.ToInt32(Ay) + 1), r);
                tl = db.Database.SqlQuery<CTargetRapor>(sorgu).ToList();
                if ((tl == null ? 0 : tl.Count()) > 0)
                {
                    toplamCiro = tl.Sum(x => x.NetCiro);
                    foreach (CTargetRapor item in tl)
                    {
                        item.CiroOran = Math.Round((item.NetCiro * 100 / toplamCiro), 2);
                    }
                }
            }
            catch (Exception ex)
            {
                tl = new List<CTargetRapor>();
                Logger(ex, "/Reports/Financial/TargetRaporSelect");
            }
            return json.Serialize(tl);
        }
        public PartialViewResult TargetRaporBolgeList(string GrupKod, string Ay, string Yil)
        {
            ViewBag.GRUPKOD = GrupKod;
            ViewBag.AY = Ay;
            ViewBag.YIL = Yil;
            return PartialView("TargetRaporBolgeList");
        }
        public string TargetRaporBolgeSelect(string GrupKod, string Ay, string Yil)
        {
            List<CTargetRaporTemsilci> ctrt;
            var json = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            try
            {
                string sorgu = "", r = "", sorgu2 = "";
                decimal toplamCiro = 0;
                r = YilAyConvert(Yil, Ay);
                // sorgu2 = String.Format(TargetGrupKodSelect.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil), (Convert.ToInt32(Ay) + 1), r, GrupKod.ToString());
                //  var Berk = db.Database.SqlQuery<TargetGrupKodSelect>(string.Format(TargetGrupKodSelect.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil), (Convert.ToInt32(Ay) + 1), r, GrupKod.ToString())).FirstOrDefault();
                // sorgu = String.Format(CTargetRaporTemsilci.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil), (Convert.ToInt32(Ay) + 1), Berk.GrupKod.ToString(), r);
                sorgu = String.Format(CTargetRaporTemsilci.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil), (Convert.ToInt32(Ay) + 1), GrupKod.ToString(), r);
                ctrt = db.Database.SqlQuery<CTargetRaporTemsilci>(sorgu).ToList();
                if ((ctrt == null ? 0 : ctrt.Count()) > 0)
                {
                    toplamCiro = ctrt.Sum(x => x.NetCiro);
                    foreach (CTargetRaporTemsilci item in ctrt)
                    {
                        item.CiroOran = Math.Round((item.NetCiro * 100 / toplamCiro), 2);
                    }
                }
            }
            catch (Exception ex)
            {
                ctrt = new List<CTargetRaporTemsilci>();
                Logger(ex, "/Reports/Financial/TargetRaporBolgeSelect");
            }
            return json.Serialize(ctrt);
        }
        public PartialViewResult TargetRaporUrunGrupList(string GrupKod, string Ay, string Yil)
        {
            ViewBag.GRUPKOD = GrupKod;
            ViewBag.AY = Ay;
            ViewBag.YIL = Yil;
            return PartialView("TargetRaporUrunGrupList");
        }
        public string TargetRaporUrunGrupSelect(string GrupKod, string Ay, string Yil)
        {
            List<UrunGrupRapor> ugr;
            var json = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            try
            {
                string sorgu = "", r = "";
                r = YilAyConvert(Yil, Ay);
                sorgu = String.Format(UrunGrupRapor.Sorgu, vUser.SirketKodu, GrupKod.ToString(), r, Convert.ToInt32(Yil), (Convert.ToInt32(Ay) + 1));
                ugr = db.Database.SqlQuery<UrunGrupRapor>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                ugr = new List<UrunGrupRapor>();
                Logger(ex, "/Reports/Financial/TargetRaporUrunGrupSelect");
            }
            return json.Serialize(ugr);
        }
        public PartialViewResult TargetRaporPRTList(string GrupKod, string Ay, string Yil)
        {
            ViewBag.GRUPKOD = GrupKod;
            ViewBag.AY = Ay;
            ViewBag.YIL = Yil;
            return PartialView("TargetRaporPRTList");
        }
        public string TargetRaporPRTSelect(string GrupKod, string Ay, string Yil)
        {
            List<PRTRapor> prt;
            var json = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            try
            {
                string sorgu = "";
                string r = YilAyConvert(Yil, Ay);
                sorgu = String.Format(PRTRapor.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil), Convert.ToInt32(Ay) + 1, GrupKod.ToString());
                prt = db.Database.SqlQuery<PRTRapor>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                prt = new List<PRTRapor>();
                Logger(ex, "/Reports/Financial/TargetRaporPRTSelect");
            }
            return json.Serialize(prt);
        }
        #endregion
        #region TargetAyBazlıRaporBolge-YAPILDI
        public ActionResult TargetAyBazliRapor()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
            ViewBag.Yillar = HdfGrupProperties(1);
            return View();
        }
        public PartialViewResult TargetAyBazliRaporList(string Yil)
        {
            ViewBag.Yil = Yil;
            return PartialView("TargetAyBazliRaporList");
        }
        public string TargetAyBazliRaporSelect(string Yil)
        {
            List<AyBazliBolgeRapor> tabbr;
            var json = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            try
            {
                string sorgu = "";
                sorgu = String.Format(AyBazliBolgeRapor.Sorgu, vUser.SirketKodu, Yil);
                tabbr = db.Database.SqlQuery<AyBazliBolgeRapor>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                tabbr = new List<AyBazliBolgeRapor>();
                Logger(ex, "/Reports/Financial/TargetAyBazliRaporSelect");
            }
            return json.Serialize(tabbr);
        }
        #endregion
        #region TargetAyBazlıRaporTemsilci-YAPILDI
        public ActionResult TargetAyBazliTemsilciRapor()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
            ViewBag.Yillar = HdfGrupProperties(1);
            return View();
        }
        public PartialViewResult TargetAyBazliTemsilciList(string Yil)
        {
            ViewBag.Yil = JsonConvert.SerializeObject(Yil);
            return PartialView("TargetAyBazliTemsilciList");
        }
        public string TargetAyBazliTemsilciSelect(string Yil)
        {
            List<AyBazliTemsilciRapor> tsbtr;
            var json = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            try
            {
                string sorgu = "";
                sorgu = String.Format(AyBazliTemsilciRapor.Sorgu, vUser.SirketKodu, Yil);
                tsbtr = db.Database.SqlQuery<AyBazliTemsilciRapor>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                tsbtr = new List<AyBazliTemsilciRapor>();
                Logger(ex, "/Reports/Financial/TargetAyBazliTemsilciSelect");
            }
            return json.Serialize(tsbtr);
        }
        #endregion
        #region GunlukSiparis-YAPILDI
        public ActionResult RaporGunlukSiparis()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
            ViewBag.Yillar = HdfGrupProperties(1);
            ViewBag.Aylar = HdfGrupProperties(2);
            return View();
        }
        public PartialViewResult RaporGunlukSiparisList(int BasTarih, int BitTarih)
        {
            ViewBag.BasTarih = JsonConvert.SerializeObject(BasTarih);
            ViewBag.BitTarih = JsonConvert.SerializeObject(BitTarih);
            return PartialView("RaporGunlukSiparisList");
        }
        public string RaporGunlukSiparisSelect(int BasTarih, int BitTarih)
        {
            List<GunlukSiparis> gs;
            var json = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            try
            {
                string sorgu = "";
                sorgu = String.Format(GunlukSiparis.Sorgu, vUser.SirketKodu, BasTarih, BitTarih);
                gs = db.Database.SqlQuery<GunlukSiparis>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                gs = new List<GunlukSiparis>();
                Logger(ex, "/Reports/Financial/RaporGunlukSiparisSelect");
            }
            return json.Serialize(gs);
        }
        #endregion
        #region BekleyenSiparisler-YAPILDI
        public ActionResult RaporBekleyenSiparisler()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
            return View();
        }
        public PartialViewResult RaporBekleyenSiparislerList()
        {
            return PartialView("RaporBekleyenSiparislerList");
        }
        public string RaporBekleyenSiparislerSelect()
        {
            List<BekleyenSips> bs;
            var json = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            try
            {
                string sorgu = "";
                sorgu = String.Format(BekleyenSips.Sorgu, vUser.SirketKodu);
                bs = db.Database.SqlQuery<BekleyenSips>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                bs = new List<BekleyenSips>();
                Logger(ex, "/Reports/Financial/RaporBekleyenSiparislerSelect");
            }
            return json.Serialize(bs);
        }
        public PartialViewResult BekleyenMalzemeList(string HesapKodu)
        {
            ViewBag.HesapKodu = HesapKodu;
            return PartialView("BekleyenMalzemeList");
        }
        public string BekleyenMalzemeSelect(string HesapKodu)
        {
            List<BekleyenMalz> bm;
            var json = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            try
            {
                string sorgu = "";
                sorgu = String.Format(BekleyenMalz.Sorgu, vUser.SirketKodu, HesapKodu);
                bm = db.Database.SqlQuery<BekleyenMalz>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                bm = new List<BekleyenMalz>();
                Logger(ex, "/Reports/Financial/BekleyenMalzemeSelect");
            }
            return json.Serialize(bm);
        }
        public PartialViewResult BekleyenMalzemeDetay(string HesapKodu)
        {
            List<BekleyenMalz> list;
            string sorgu = "";
            sorgu = String.Format(BekleyenMalz.Sorgu, vUser.SirketKodu, HesapKodu);
            list = db.Database.SqlQuery<BekleyenMalz>(sorgu).ToList();
            ViewBag.HesapKodu = HesapKodu;
            return PartialView("BekleyenMalzemeDetay", list);
        }
        public PartialViewResult MalzemeDepoList(string MalKodu)
        {
            ViewBag.MlKodu = MalKodu;
            return PartialView("MalzemeDepoList");
        }
        public string MalzemeDepoSelect(string MalKodu)
        {
            var json = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            List<BekleyenMalzDepo> list;
            try
            {
                string sorgu = "";
                sorgu = String.Format(BekleyenMalzDepo.Sorgu, vUser.SirketKodu, MalKodu);
                list = db.Database.SqlQuery<BekleyenMalzDepo>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                list = new List<BekleyenMalzDepo>();
                Logger(ex, "/Reports/Financial/MalzemeDepoSelect");
            }
            return json.Serialize(list);
        }
        #endregion
        #region UrunSatisAnalizi-YAPILDI
        public ActionResult RaporUrunSatisAnalizi()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
            List<RaporGrupKod> _raporGrupKod;
            try
            {
                _raporGrupKod = db.Database.SqlQuery<RaporGrupKod>(String.Format(RaporGrupKod.Sorgu, vUser.SirketKodu)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/RaporUrunSatisAnalizi");
                _raporGrupKod = new List<RaporGrupKod>();
            }
            ViewBag.BOLGE = _raporGrupKod;
            ViewBag.Yillar = HdfGrupProperties(1);
            return View();
        }
        public PartialViewResult RaporUrunSatisAnaliziList(string Yil, string GrupKod, string TipKod)
        {
            ViewBag.Yil = Yil;
            ViewBag.GrupKod = GrupKod;
            ViewBag.TipKod = (TipKod == null ? "0" : TipKod);
            ViewBag.Eltit = (GrupKod == "0" ? "Tümü" : String.Concat(GrupKod, " - ", (TipKod == null || TipKod == "0" ? "Tümü" : TipKod)));
            return PartialView("RaporUrunSatisAnaliziList");
        }
        public string RaporUrunSatisAnaliziSelect(string Yil, string GrupKod, string TipKod)
        {
            List<SatisAnaliziTemsilci> sat;
            var json = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            try
            {
                string sorgu = "";
                sorgu = String.Format(SatisAnaliziTemsilci.Sorgu, vUser.SirketKodu, GrupKod, Convert.ToInt32(Yil), TipKod);
                sat = db.Database.SqlQuery<SatisAnaliziTemsilci>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                sat = new List<SatisAnaliziTemsilci>();
                Logger(ex, "/Reports/Financial/RaporUrunSatisAnaliziSelect");
            }
            return json.Serialize(sat);
        }
        #endregion
        #region WebSiparis-YAPILDI
        public ActionResult RaporWebSiparis()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
            ViewBag.Yillar = HdfGrupProperties(1);
            ViewBag.Aylar = HdfGrupProperties(2);
            ViewBag.SipTipi = HdfGrupProperties(3);
            return View();
        }
        public PartialViewResult RaporWebSiparisList(string Ay, string Yil, string SipTipi)
        {
            ViewBag.Ay = Ay;
            ViewBag.Yil = Yil;
            ViewBag.Sip = SipTipi;
            return PartialView("RaporWebSiparisList");
        }
        public string RaporWebSiparisSelect(string Yil, string Ay, string Sip)
        {
            List<WebSiparis> ws;
            var json = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            try
            {
                string sorgu = "", siparisTipi = "";
                switch (Sip)
                {
                    case "0": siparisTipi = ""; break;
                    case "1": siparisTipi = @"AND (SPI.EvrakNo LIKE 'W%' OR SPI.EVRAKNO LIKE 'KW%')"; break;
                    default: siparisTipi = @"AND (SPI.EvrakNo LIKE 'TW%')"; break;
                }
                sorgu = String.Format(WebSiparis.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil), Convert.ToInt32(Ay) + 1, siparisTipi, "");
                ws = db.Database.SqlQuery<WebSiparis>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                ws = new List<WebSiparis>();
                Logger(ex, "/Reports/Financial/RaporWebSiparisSelect");
            }
            return json.Serialize(ws);
        }
        #endregion
        #region TumMusteriCiro -YAPILDI
        public ActionResult TumMusteriCiro()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
            return View("TumMusteriCiro");
        }
        public string TumMusteriCiroSelect()
        {
            List<MusteriCiro> mc;
            var json = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            try
            {
                mc = db.Database.SqlQuery<MusteriCiro>(String.Format(MusteriCiro.Sorgu, vUser.SirketKodu)).ToList();
            }
            catch (Exception ex)
            {
                mc = new List<MusteriCiro>();
                Logger(ex, "/Reports/Financial/TumMusteriCiroSelect");
            }
            return json.Serialize(mc);
        }
        #endregion
        #region GünlükCiroRaporu
        public ActionResult GunlukCiroRaporu()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
            ViewBag.Yillar = HdfGrupProperties(1);
            ViewBag.Aylar = HdfGrupProperties(2);
            return View();
        }
        public PartialViewResult GunlukCiroRaporuList(int BasTarih, int BitTarih)
        {
            ViewBag.BasTarih = JsonConvert.SerializeObject(BasTarih);
            ViewBag.BitTarih = JsonConvert.SerializeObject(BitTarih);
            return PartialView("GunlukCiroRaporuList");
        }
        public string GunlukCiroRaporuSelect(int BasTarih, int BitTarih)
        {
            List<GunlukCiro> gs;
            JavaScriptSerializer json = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            try
            {
                string sorgu = "";
                sorgu = String.Format(GunlukCiro.Sorgu, vUser.SirketKodu, BasTarih, BitTarih);
                gs = db.Database.SqlQuery<GunlukCiro>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                gs = new List<GunlukCiro>();
                Logger(ex, "/Reports/Financial/GunlukCiroRaporuSelect");
            }
            return json.Serialize(gs);
        }
        #endregion
        public PartialViewResult RaporGunlukSiparisDetay(string EVRAKNO, string TIP)
        {
            //GunlukCiroRaporu ve RaporGunlukSiparis kullanılmakta
            List<GunlukSiparisDetay> gs;
            string sorgu = "", tblAdi = "", tWhere = "";
            switch (TIP)
            {
                case "0": tblAdi = "SPI"; tWhere = "62"; break;
                default: tblAdi = "STI"; tWhere = "1,2,163"; break;
            }
            sorgu = String.Format(GunlukSiparisDetay.Sorgu, vUser.SirketKodu, tblAdi, tWhere, EVRAKNO);
            try { gs = db.Database.SqlQuery<GunlukSiparisDetay>(sorgu).ToList(); }
            catch (Exception ex)
            {
                gs = new List<GunlukSiparisDetay>();
                Logger(ex, "/Reports/Financial/RaporGunlukSiparisDetay");
            }
            return PartialView("RaporGunlukSiparisDetay", gs);
        }
        public JsonResult TemsilciGetir(string GrupKod, string TipKod)
        {
            List<SelectListItem> grpTanimKartList = new List<SelectListItem>();
            try
            {
                var tet = db.Database.SqlQuery<RaporTemsilci>(String.Format(RaporTemsilci.Sorgu, vUser.SirketKodu, GrupKod)).ToList();
                foreach (RaporTemsilci item in tet)
                {
                    grpTanimKartList.Add(new SelectListItem
                    {
                        Selected = (TipKod == item.TipKod ? true : false),
                        Text = item.TipKod,
                        Value = item.TipKod,
                    });
                }
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TemsilciGetir");
            }
            return Json(grpTanimKartList.Select(x => new { x.Value, x.Text, x.Selected }), JsonRequestBehavior.AllowGet);
        }
        private List<string> HdfGrupProperties(int val)
        {
            List<string> ret = new List<string>();
            switch (val)
            {
                case 1:
                    for (int i = 2016; i <= DateTime.Now.Year; i++)
                    {
                        ret.Add(i.ToString());
                    }
                    break;
                case 2:
                    ret.Add("Ocak"); ret.Add("Şubat"); ret.Add("Mart"); ret.Add("Nisan"); ret.Add("Mayıs"); ret.Add("Haziran");
                    ret.Add("Temmuz"); ret.Add("Ağustos"); ret.Add("Eylül"); ret.Add("Ekim"); ret.Add("Kasım"); ret.Add("Aralık");
                    break;
                default:
                    ret.Add("Tümü"); ret.Add("Tablet"); ret.Add("B2B");
                    break;
            }
            return ret;
        }
        private string YilAyConvert(string yil, string ay)
        {
            string MMyyyy = "";
            MMyyyy = ((ay.ToInt32() + 1).ToString2().Length == 1 ? "0" + (ay.ToInt32() + 1).ToString2() : (ay.ToInt32() + 1).ToString2()) + yil;
            return MMyyyy;
        }
    }
}