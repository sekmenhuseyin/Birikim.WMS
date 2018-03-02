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
            var cHK = db.Database.SqlQuery<CariCiroRaporuResult>(string.Format("[FINSAT6{0}].[wms].[CariCiroRaporu]", vUser.SirketKodu)).ToList();
            return View(cHK);
        }

        /// <summary>
        /// cari ekstre
        /// </summary>
        public ActionResult CariEkstre()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            var CHK = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[wms].[CHKSelectKartTip]", vUser.SirketKodu)).ToList();
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
            var VGC = db.Database.SqlQuery<RaporVadesiGelmemisCekler>(string.Format("[FINSAT6{0}].[wms].[VadesiGelmemisCekler] @BasTarih = {1}, @BitTarih = {2}", vUser.SirketKodu, bastarih, bittarih)).ToList();
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
            var CLR = db.Database.SqlQuery<RaporCekListesi>(string.Format("[FINSAT6{0}].[wms].[CekListesiRaporu] @Ay = {1}, @IslemTip = {2}, @Yil = {3}", vUser.SirketKodu, ay, pozisyon, yil)).ToList();
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
            var TRB = db.Database.SqlQuery<RaporToplamRiskBakiyesi>(string.Format("[FINSAT6{0}].[wms].[ToplamRiskBakiyesi] @BasTarih = {1}, @BitTarih = {2},@VadeBaslangic = {3}, @VadeBitis = {4},@BasHesapKodu= '{5}', @BitHesapKodu = '{6}'", vUser.SirketKodu, bastarih, bittarih, basvadetarih, bitvadetarih, chk_bas, chk_bit)).ToList();
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
            var GS = db.Database.SqlQuery<RaporCekPortfoyListesi>(string.Format("[FINSAT6{0}].[wms].[CekPortfoyListesi] @BasTarih = {1}, @BitTarih = {2},@BasVadeTarih = {3}, @BitVadeTarih = {4}", vUser.SirketKodu, bastarih, bittarih, basvadetarih, bitvadetarih)).ToList();
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
            var OYM = db.Database.SqlQuery<RaporOdemeYapmayanMusteriler>(string.Format("[FINSAT6{0}].[wms].[OdemeYapmayanMusteriler] @Number = {1}", vUser.SirketKodu, gunsayisi)).ToList();
            return PartialView("OdemeYapmayanMusterilerList", OYM);
        }

        /// <summary>
        /// bakiye
        /// </summary>
        public ActionResult Bakiye()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            var CHK = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[wms].[CHKSelectKartTip]", vUser.SirketKodu)).ToList();
            return View(CHK);
        }

        public PartialViewResult BakiyeList(int bastarih, int bittarih, int basvadetarih, int bitvadetarih, string chk_bas, string chk_bit, decimal bakiye)
        {
            var BR = db.Database.SqlQuery<RaporBakiye>(string.Format("[FINSAT6{0}].[wms].[BakiyeRaporu] @BasTarih = {1}, @BitTarih = {2},@VadeBaslangic = {3}, @VadeBitis = {4},@BasHesapKodu = '{5}', @BitHesapKodu = '{6}', @Bakiye = {7}", vUser.SirketKodu, bastarih, bittarih, basvadetarih, bitvadetarih, chk_bas, chk_bit, bakiye)).ToList();
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
            var SBR = db.Database.SqlQuery<SatisBaglatiRapru>(string.Format("[FINSAT6{0}].[wms].[SatisBaglantiRaporu] @Tip={1}", vUser.SirketKodu, tip)).ToList();
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
            var TRAR = db.Database.SqlQuery<ToplamRiskAnaliziRaporu>(string.Format("[FINSAT6{0}].[wms].[ToplamRiskAnaliziRaporu] @BasHesapKodu='{1}', @BitHesapKodu='{2}'", vUser.SirketKodu, baschk, bitchk)).ToList();
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
            var sql = string.Format("FINSAT6{0}.[wms].[CHKSearch] @HesapKodu = N'{1}', @Unvan = N'', @top = 200", vUser.SirketKodu, term);
            // return
            try
            {
                var list = db.Database.SqlQuery<frmJson>(sql).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "Reports/Financial/GetChKCode");
                return Json(new List<frmJson>(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult TahsilatKontrol()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public JsonResult TahsilatKontrolList()
        {
            var list = db.Database.SqlQuery<RP_TahsilatKontrol>(String.Format("[FINSAT6{0}].[wms].[RP_TahsilatKontrol]", vUser.SirketKodu)).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #region Target
        public ActionResult Target()
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
                Logger(ex, "/Reports/Financial/Target");
                _raporGrupKod = new List<RaporGrupKod>();
                _raporTargetUrunGrup = new List<RaporTargetUrunGrup>();
            }
            ViewBag.BOLGE = new SelectList(_raporGrupKod, "GrupKod", "GrupKod");
            ViewBag.URUNGRUP = new SelectList(_raporTargetUrunGrup, "UrunGrup", "UrunGrup");
            ViewBag.TEMSILCI = new SelectList(new List<RaporTemsilci>(), "TipKod", "TipKod");
            return View("Target", new HDF());
        }
        public JsonResult TargetTemsilciList(string GrupKod, string TipKod)
        {
            var json = new JavaScriptSerializer();
            List<SelectListItem> listRapTemsilci = new List<SelectListItem>();
            try
            {
                var tet = db.Database.SqlQuery<RaporTemsilci>(String.Format(RaporTemsilci.Sorgu, vUser.SirketKodu, GrupKod)).ToList();
                foreach (RaporTemsilci item in tet)
                {
                    listRapTemsilci.Add(new SelectListItem
                    {
                        Selected = (TipKod == item.TipKod ? true : false),
                        Text = item.TipKod,
                        Value = item.TipKod,
                    });
                }
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TargetTemsilciList");
            }
            return Json(listRapTemsilci.Select(x => new { x.Value, x.Text, x.Selected }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult TargetSave(HDF hdf)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Writing) == false) { return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet); }
            var _Result = new Result(true);
            string sorgu = "";
            int snc = 0;
            switch (hdf.ID)
            {
                case 0:
                    sorgu = String.Format(HDF.TInsertSorgu, vUser.SirketKodu, hdf.BOLGE, hdf.TEMSILCI, hdf.URUNGRUP, hdf.HEDEF,
                    Convert.ToInt32(DateTime.Today.ToOADate()), DateTime.Now.ToString("MMyyyy"));
                    break;
                default:
                    sorgu = String.Format(HDF.TUpdateSorgu, vUser.SirketKodu, hdf.BOLGE, hdf.TEMSILCI, hdf.URUNGRUP, hdf.HEDEF, hdf.ID, hdf.AYYIL);
                    break;
            }
            try
            {
                snc = db.Database.SqlQuery<int>(sorgu).FirstOrDefault();
            }
            catch (Exception ex)
            {
                snc = 0;
                Logger(ex, "/Reports/Financial/TargetSave");
                _Result.Status = false;
                _Result.Message = "İşlem başarısız";
            }
            if (snc > 0)
            {
                switch (snc)
                {
                    case 10:
                        _Result.Status = false;
                        _Result.Message = "Hedef limiti aşılmıştır.Kontrol ediniz!";
                        break;
                    case 5:
                        _Result.Status = false;
                        _Result.Message = "Girilen Bölgeye ait miktar bulunmamaktadır.Kontrol ediniz!";
                        break;
                    default:
                        _Result.Status = true;
                        _Result.Message = "İşlem başarılı";
                        break;
                }
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult TargetList()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return null; }
            List<HDF> hdfList;
            try
            {
                hdfList = db.Database.SqlQuery<HDF>(String.Format(@"
                SELECT H1.ID,H1.TIP,H1.BOLGE,H1.TEMSILCI,H1.URUNGRUP,CONVERT(NVARCHAR,H1.HEDEF) AS HEDEF,H1.TARIH,H1.AYYIL 
                FROM FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK) WHERE H1.TIP=1", vUser.SirketKodu)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TargetList");
                hdfList = new List<HDF>();
            }
            return PartialView(hdfList);
        }
        public PartialViewResult TargetEdit(int id)
        {
            HDF h;
            List<RaporGrupKod> _raporGrupKod;
            List<RaporTargetUrunGrup> _raporTargetUrunGrup;
            if (CheckPerm(Perms.Raporlar, PermTypes.Updating) == false) { return null; }
            try
            {
                _raporGrupKod = db.Database.SqlQuery<RaporGrupKod>(String.Format(RaporGrupKod.Sorgu, vUser.SirketKodu)).ToList();
                _raporTargetUrunGrup = db.Database.SqlQuery<RaporTargetUrunGrup>(String.Format(RaporTargetUrunGrup.Sorgu, vUser.SirketKodu))
                .ToList();
                h = db.Database.SqlQuery<HDF>(String.Format(@"
                  SELECT H1.ID,H1.TIP,H1.BOLGE,H1.TEMSILCI,H1.URUNGRUP,CONVERT(NVARCHAR,H1.HEDEF) AS HEDEF,H1.TARIH,H1.AYYIL 
                  FROM [FINSAT6{0}].[FINSAT6{0}].HDF AS H1 WITH (NOLOCK) WHERE H1.ID={1} AND H1.TIP=1", vUser.SirketKodu, id)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TargetEdit");
                h = new HDF();
                _raporGrupKod = new List<RaporGrupKod>();
                _raporTargetUrunGrup = new List<RaporTargetUrunGrup>();
            }
            ViewBag.BOLGE = new SelectList(_raporGrupKod, "GrupKod", "GrupKod", h.BOLGE);
            ViewBag.URUNGRUP = new SelectList(_raporTargetUrunGrup, "UrunGrup", "UrunGrup", h.URUNGRUP);
            ViewBag.Temp = h.TEMSILCI;
            return PartialView(h);
        }
        public JsonResult TargetDelete(string Id)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Deleting) == false) { return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet); }
            string sorgu = "";
            sorgu = String.Format(@"DELETE FROM [FINSAT6{0}].[FINSAT6{0}].HDF WHERE ID={1} AND TIP=1", vUser.SirketKodu, Id);
            try
            {
                db.Database.ExecuteSqlCommand(sorgu);
                LogActions("Reports", "Financial", "Delete", ComboItems.alSil, Id.ToInt32());
                return Json(new Result(true, Id.ToInt32()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TargetDelete");
                return Json(new Result(false, "Projeye ait form bulunduğu için silme işlemi gerçekleştirilememiştir."), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region TargetBolge
        public ActionResult TargetBolge()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
            List<RaporGrupKod> _raporGrupKod;
            try
            {
                _raporGrupKod = db.Database.SqlQuery<RaporGrupKod>(String.Format(RaporGrupKod.Sorgu, vUser.SirketKodu)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TargetBolge");
                _raporGrupKod = new List<RaporGrupKod>();
            }
            ViewBag.BOLGE = new SelectList(_raporGrupKod, "GrupKod", "GrupKod");
            return View("TargetBolge", new HDF());
        }
        public PartialViewResult TargetBolgeList()
        {
            List<HDF> hdfList;
            try
            {
                hdfList = db.Database.SqlQuery<HDF>(String.Format(@"
                SELECT H1.ID,H1.TIP,H1.BOLGE,H1.TEMSILCI,H1.URUNGRUP,CONVERT(NVARCHAR,H1.HEDEF) AS HEDEF,H1.TARIH,H1.AYYIL 
                FROM [FINSAT6{0}].[FINSAT6{0}].HDF AS H1 WITH (NOLOCK) WHERE H1.TIP=0
                ", vUser.SirketKodu)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TargetBolgeList");
                hdfList = new List<HDF>();
            }
            return PartialView("TargetBolgeList",hdfList);
        }
        public JsonResult TargetBolgeSave(HDF hdf)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Writing) == false) { return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet); }
            var _Result = new Result(true);
            int snc = 0;
            string sorgu = "";
            switch (hdf.ID)
            {
                case 0:
                    sorgu = String.Format(HDF.BInsertSorgu, vUser.SirketKodu, hdf.BOLGE, hdf.HEDEF, Convert.ToInt32(DateTime.Today.ToOADate()), DateTime.Now.ToString("MMyyyy"));
                    break;
                default:
                    sorgu = String.Format(HDF.BUpdateSorgu, vUser.SirketKodu, hdf.BOLGE, hdf.HEDEF, hdf.ID, hdf.AYYIL);
                    break;
            }
            try
            {
                snc = db.Database.SqlQuery<int>(sorgu).FirstOrDefault();
            }
            catch (Exception ex)
            {
                snc = 0;
                Logger(ex, "/Reports/Financial/TargetBolgeSave");
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
        public PartialViewResult TargetBolgeEdit(int id)
        {
            HDF h;
            List<RaporGrupKod> _raporGrupKod;
            if (CheckPerm(Perms.Raporlar, PermTypes.Updating) == false) { return null; }
            try
            {
                _raporGrupKod = db.Database.SqlQuery<RaporGrupKod>(String.Format(RaporGrupKod.Sorgu, vUser.SirketKodu)).ToList();
                h = db.Database.SqlQuery<HDF>(String.Format(@"
                SELECT H1.ID,H1.TIP,H1.BOLGE,H1.TEMSILCI,H1.URUNGRUP,CONVERT(NVARCHAR,H1.HEDEF) AS HEDEF,H1.TARIH,H1.AYYIL 
                FROM [FINSAT6{0}].[FINSAT6{0}].HDF AS H1 WITH (NOLOCK) WHERE H1.ID={1} AND H1.TIP=0", vUser.SirketKodu, id)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TargetBolgeEdit");
                h = new HDF();
                _raporGrupKod = new List<RaporGrupKod>();
            }
            ViewBag.BOLGE = new SelectList(_raporGrupKod, "GrupKod", "GrupKod", h.BOLGE);
            return PartialView(h);
        }
        public JsonResult TargetBolgeDelete(string Id)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Deleting) == false) { return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet); }
            var _Result = new Result(true);
            string sorgu = "";
            int snc = 0;
            sorgu = String.Format(HDF.BDeleteSorgu, vUser.SirketKodu, Id);
            try
            {
                snc = db.Database.SqlQuery<int>(sorgu).FirstOrDefault();
            }
            catch (Exception ex)
            {
                snc = 0;
                Logger(ex, "/Reports/Financial/TargetBolgeDelete");
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
        #region TargetRapor
        public ActionResult TargetRapor()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
            return View();
        }
        public PartialViewResult TargetRaporList(string Ay, string Yil)
        {
            List<CTargetRapor> tl;
            ViewBag.Ay = Ay;
            ViewBag.Yil = Yil;
            try
            {
                string sorgu = "";
                decimal toplamCiro = 0;
                sorgu = String.Format(CTargetRapor.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil), Convert.ToInt32(Ay));
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
                Logger(ex, "/Reports/Financial/TargetRaporList");
            }
            return PartialView("TargetRaporList", tl);
        }
        public PartialViewResult TargetRaporBolgeList(string GrupKod, string Ay, string Yil)
        {
            List<CTargetRaporTemsilci> ctrt;
            try
            {
                string sorgu = "";
                decimal toplamCiro = 0;
                sorgu = String.Format(CTargetRaporTemsilci.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil), Convert.ToInt32(Ay), GrupKod);
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
                Logger(ex, "/Reports/Financial/TargetRaporBolgeList");
            }
            return PartialView("TargetRaporBolgeList", ctrt);
        }
        public PartialViewResult TargetRaporUrunGrupList(string GrupKod, string Ay, string Yil)
        {
            List<UrunGrupRapor> ugr;
            try
            {
                string sorgu = "";
                sorgu = String.Format(UrunGrupRapor.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil), Convert.ToInt32(Ay), GrupKod);
                ugr = db.Database.SqlQuery<UrunGrupRapor>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                ugr = new List<UrunGrupRapor>();
                Logger(ex, "/Reports/Financial/TargetRaporUrunGrupList");
            }
            return PartialView("TargetRaporUrunGrupList", ugr);
        }
        public PartialViewResult TargetRaporPRTList(string GrupKod, string Ay, string Yil)
        {
            List<PRTRapor> prt;
            try
            {
                string sorgu = "";
                sorgu = String.Format(PRTRapor.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil), Convert.ToInt32(Ay), GrupKod);
                prt = db.Database.SqlQuery<PRTRapor>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                prt = new List<PRTRapor>();
                Logger(ex, "/Reports/Financial/TargetRaporPRTList");
            }
            return PartialView("TargetRaporPRTList", prt);
        }
        #endregion
        #region TargetAyBazlıRaporBolge
        public ActionResult TargetAyBazliRapor()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
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
            try
            {
                string sorgu = "";
                sorgu = String.Format(AyBazliBolgeRapor.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil));
                tabbr = db.Database.SqlQuery<AyBazliBolgeRapor>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                tabbr = new List<AyBazliBolgeRapor>();
                Logger(ex, "/Reports/Financial/TargetAyBazliRaporSelect");
            }
            return new JavaScriptSerializer().Serialize(tabbr);
        }
        #endregion
        #region TargetAyBazlıRaporTemsilci
        public ActionResult TargetAyBazliTemsilciRapor()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
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
            try
            {
                string sorgu = "";
                sorgu = String.Format(AyBazliTemsilciRapor.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil));
                tsbtr = db.Database.SqlQuery<AyBazliTemsilciRapor>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                tsbtr = new List<AyBazliTemsilciRapor>();
                Logger(ex, "/Reports/Financial/TargetAyBazliTemsilciSelect");
            }
            return new JavaScriptSerializer().Serialize(tsbtr);
        }
        #endregion
        #region GunlukSiparis
        public ActionResult RaporGunlukSiparis()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
            return View();
        }
        public PartialViewResult RaporGunlukSiparisList(string Yil, string Ay)
        {
            ViewBag.Yil = JsonConvert.SerializeObject(Yil);
            ViewBag.Ay = JsonConvert.SerializeObject(Ay);
            return PartialView("RaporGunlukSiparisList");
        }
        public string RaporGunlukSiparisSelect(string Yil, string Ay)
        {
            List<GunlukSiparis> gs;
            try
            {
                string sorgu = "";
                sorgu = String.Format(GunlukSiparis.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil), Convert.ToInt32(Ay), "TOP 100");
                gs = db.Database.SqlQuery<GunlukSiparis>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                gs = new List<GunlukSiparis>();
                Logger(ex, "/Reports/Financial/RaporGunlukSiparisSelect");
            }
            return new JavaScriptSerializer().Serialize(gs);
        }
        #endregion
        #region BekleyenSiparisler
        public ActionResult RaporBekleyenSiparisler()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
            return View();
        }
        public PartialViewResult RaporBekleyenSiparislerList(string Yil,string Ay)
        {
            ViewBag.Yil = Yil;
            ViewBag.Ay = Ay;
            return PartialView("RaporBekleyenSiparislerList");
        }
        public string RaporBekleyenSiparislerSelect(string Yil, string Ay)
        {
            List<BekleyenSips> bs;
            try
            {
                string sorgu = "";
                sorgu = String.Format(BekleyenSips.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil), Convert.ToInt32(Ay), "TOP 100");
                bs = db.Database.SqlQuery<BekleyenSips>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                bs = new List<BekleyenSips>();
                Logger(ex, "/Reports/Financial/RaporBekleyenSiparislerSelect");
            }
            return new JavaScriptSerializer().Serialize(bs);
        }
        #endregion
        #region UrunSatisAnalizi
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
            return View(_raporGrupKod);
        }
        public PartialViewResult UrunSatisTemsilciAnalizList(string GrupKod,string Yil)
        {
            List<SatisAnaliziTemsilci> sat;
            if (String.IsNullOrEmpty(Yil))
            {
                return PartialView("UrunSatisTemsilciAnalizList", null);
            }
            
            try
            {
                string sorgu = "";
                sorgu = String.Format(SatisAnaliziTemsilci.Sorgu, vUser.SirketKodu, GrupKod, Convert.ToInt32(Yil));
                sat = db.Database.SqlQuery<SatisAnaliziTemsilci>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                sat = new List<SatisAnaliziTemsilci>();
                Logger(ex, "/Reports/Financial/UrunSatisTemsilciAnalizList");
            }
            ViewBag.Yil = Yil;
            return PartialView("UrunSatisTemsilciAnalizList", sat);
        }
        #endregion
    }
}