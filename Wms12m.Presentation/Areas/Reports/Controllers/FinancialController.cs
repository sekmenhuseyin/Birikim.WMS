using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        #region Tao
        public ActionResult Target()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
            List<RaporGrupKod> _raporGrupKod;
            List<RaporTargetUrunGrup> _raporTargetUrunGrup;
            try
            {
                _raporGrupKod = db.Database.SqlQuery<RaporGrupKod>(string.Format(RaporGrupKod.Sorgu, vUser.SirketKodu)).ToList();
                _raporTargetUrunGrup = db.Database.SqlQuery<RaporTargetUrunGrup>(string.Format(RaporTargetUrunGrup.Sorgu, vUser.SirketKodu)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/Target");
                _raporGrupKod = new List<RaporGrupKod>();
                _raporTargetUrunGrup = new List<RaporTargetUrunGrup>();
            }
            ViewBag.BOLGE = new SelectList(_raporGrupKod, "GrupKod", "GrupKod");
            ViewBag.URUNGRUP = new SelectList(_raporTargetUrunGrup, "UrunGrup", "UrunGrup");
            return View("Target", new HDF());
        }
        [HttpPost]
        public JsonResult TargetTemsilciList(string GrupKod)
        {
            var json = new JavaScriptSerializer();
            List<SelectListItem> listRapTemsilci = new List<SelectListItem>();
            try
            {
                foreach (RaporTemsilci item in db.Database.SqlQuery<RaporTemsilci>(string.Format(RaporTemsilci.Sorgu, vUser.SirketKodu, "'" + GrupKod + "'")).ToList())
                {
                    listRapTemsilci.Add(new SelectListItem
                    {
                        Selected = false,
                        Text = item.TipKod,
                        Value = item.GrupKod,
                    });
                }
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TargetTemsilciList");
            }
            return Json(listRapTemsilci.Select(x => new { Value = x.Value, Text = x.Text, Selected = x.Selected }), JsonRequestBehavior.AllowGet);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult TargetSave(HDF hdf)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Writing) == false) { return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet); }
            if (ModelState.IsValid)
            {
                var _Result = new Result(true);
                string sorgu = "";
                if (hdf.ID == 0)
                {
                    sorgu = String.Format(@"INSERT INTO FINSAT6{0}.FINSAT6{0}.HDF (TIP,BOLGE,TEMSILCI,URUNGRUP,HEDEF,TARIH) VALUES ({1},'{2}','{3}','{4}','{5}',{6})"
                    , vUser.SirketKodu, 1, hdf.BOLGE, hdf.TEMSILCI, hdf.URUNGRUP, hdf.HEDEF, Convert.ToInt32(DateTime.Today.ToOADate()));
                }
                else
                {
                    sorgu = String.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.HDF SET TIP=1
                    ,BOLGE='{1}',TEMSILCI='{2}',URUNGRUP='{3}',HEDEF={4}
                    WHERE ID={5}", vUser.SirketKodu, hdf.BOLGE, hdf.TEMSILCI, hdf.URUNGRUP, Convert.ToInt32(hdf.HEDEF), hdf.ID);
                }

                try
                {
                    db.Database.ExecuteSqlCommand(sorgu);
                    _Result.Status = true;
                    _Result.Message = "İşlem başarılı";
                }
                catch (Exception ex)
                {
                    Logger(ex, "/Reports/Financial/TargetSave");
                    _Result.Status = false;
                    _Result.Message = "İşlem başarısız";
                }
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
            return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
        }
        #endregion Tao
        public PartialViewResult TargetList()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return null; }
            List<HDF> hdfList;
            try
            {
                hdfList = db.Database.SqlQuery<HDF>(String.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].HDF WITH (NOLOCK)", vUser.SirketKodu)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TargetList");
                hdfList = new List<HDF>();
            }
            return PartialView(hdfList);
        }
        public PartialViewResult TargetEdit(int? id)
        {
            var json = new JavaScriptSerializer();
            HDF h;
            //List<SelectListItem> listRapTemsilci = new List<SelectListItem>();
            List<RaporGrupKod> _raporGrupKod;
            List<RaporTargetUrunGrup> _raporTargetUrunGrup;
            List<RaporTemsilci> _raporTemsilci;
            if (CheckPerm(Perms.Raporlar, PermTypes.Updating) == false) { return null; }
            try
            {
                string rT = "";
                _raporGrupKod = db.Database.SqlQuery<RaporGrupKod>(String.Format(RaporGrupKod.Sorgu, vUser.SirketKodu)).ToList();
                _raporTargetUrunGrup = db.Database.SqlQuery<RaporTargetUrunGrup>(String.Format(RaporTargetUrunGrup.Sorgu, vUser.SirketKodu)).ToList();
                h = db.Database.SqlQuery<HDF>(String.Format("SELECT K.* FROM [FINSAT6{0}].[FINSAT6{0}].HDF AS K WITH (NOLOCK) WHERE K.ID={1}", vUser.SirketKodu, id)).FirstOrDefault();
                rT = String.Format(@"(SELECT K.BOLGE FROM [FINSAT6{0}].[FINSAT6{0}].HDF AS K WITH (NOLOCK) WHERE K.ID={1})", vUser.SirketKodu, id);
                _raporTemsilci = db.Database.SqlQuery<RaporTemsilci>(String.Format(RaporTemsilci.Sorgu, vUser.SirketKodu, rT)).ToList();

                //foreach (RaporTemsilci item in db.Database.SqlQuery<RaporTemsilci>(String.Format(RaporTemsilci.Sorgu, vUser.SirketKodu, rT)).ToList())
                //{
                //    listRapTemsilci.Add(new SelectListItem()
                //    {
                //        Selected = false,
                //        Text = item.TipKod,
                //        Value = item.GrupKod
                //    });
                //}
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TargetEdit");
                h = new HDF();
                _raporGrupKod = new List<RaporGrupKod>();
                _raporTargetUrunGrup = new List<RaporTargetUrunGrup>();
                _raporTemsilci = new List<RaporTemsilci>();
            }
            ViewBag.BOLGE = new SelectList(_raporGrupKod, "GrupKod", "GrupKod", h.BOLGE);
            ViewBag.URUNGRUP = new SelectList(_raporTargetUrunGrup, "UrunGrup", "UrunGrup", h.URUNGRUP);
            //ViewBag.TEMSILCI = new SelectList(listRapTemsilci, "TipKod", "GrupKod");
            //var t = new SelectList(listRapTemsilci, "TipKod", "GrupKod");
            ViewBag.Temp = h.BOLGE;
            return PartialView(h);
        }
        public JsonResult TargetDelete(string Id)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Deleting) == false) { return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet); }
            string sorgu = "";
            sorgu = String.Format(@"DELETE FROM [FINSAT6{0}].[FINSAT6{0}].HDF WHERE ID={1}", vUser.SirketKodu, Id);
            try
            {
                db.Database.ExecuteSqlCommand(sorgu);
                LogActions("Reports", "Financial", "Delete", ComboItems.alSil, Id.ToInt32());
                return Json(new Result(true, Id.ToInt32()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new Result(false, "Projeye ait form bulunduğu için silme işlemi gerçekleştirilememiştir."), JsonRequestBehavior.AllowGet);
            }
        }
    }
}