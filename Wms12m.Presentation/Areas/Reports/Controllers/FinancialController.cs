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
                _raporTargetUrunGrup = db.Database.SqlQuery<RaporTargetUrunGrup>(String.Format(RaporTargetUrunGrup.Sorgu, vUser.SirketKodu)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/Target");
                _raporGrupKod = new List<RaporGrupKod>();
                _raporTargetUrunGrup = new List<RaporTargetUrunGrup>();
            }
            ViewBag.BOLGE = new SelectList(_raporGrupKod, "RowID", "GrupKod");
            ViewBag.URUNGRUP = new SelectList(_raporTargetUrunGrup, "RowID", "UrunGrup");
            ViewBag.TEMSILCI = new SelectList(new List<RaporTemsilci>(), "RowID", "TipKod");
            return View("Target", new HDF());
        }
        public JsonResult TargetTemsilciList(int GrupKod, int TipKod)
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
                        Selected = (TipKod == item.RowID ? true : false),
                        Text = item.TipKod,
                        Value = item.RowID.ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TargetTemsilciList");
            }
            return Json(listRapTemsilci.Select(x => new { x.Value, x.Text, x.Selected }), JsonRequestBehavior.AllowGet);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult TargetSave(HDF hdf)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Writing) == false) { return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet); }

            //TODO : Uğur hedefin eksi değer girilmesi engellenecek.
            var _Result = new Result(true);
            string sorgu = "";
            int snc = 0;
            switch (hdf.ID)
            {
                case 0:
                    sorgu = String.Format(@"
                    DECLARE @MAXDURUM INT,@MAXHEDEF NUMERIC(25,6) SET @MAXDURUM=(
                    SELECT COUNT(*) FROM FINSAT6{0}.FINSAT6{0}.HDF AS H WITH (NOLOCK) WHERE H.TIP=0 AND H.BOLGE={2})
                    IF(@MAXDURUM>0)
                    BEGIN
                    SET @MAXHEDEF=(
                    SELECT HH.HEDEF-ISNULL(D.HEDEF,0) FROM FINSAT6{0}.FINSAT6{0}.HDF AS HH WITH (NOLOCK)
                    LEFT JOIN 
                    (SELECT IC1.BOLGE,SUM(IC1.HEDEF) AS HEDEF FROM FINSAT6{0}.FINSAT6{0}.HDF AS IC1 WITH (NOLOCK) 
                    WHERE IC1.TIP=1 GROUP BY IC1.BOLGE
                    ) AS D ON HH.BOLGE=D.BOLGE
                    WHERE HH.TIP=0 AND HH.BOLGE={2})
                    IF(@MAXHEDEF>={5})
                    BEGIN
                    INSERT INTO FINSAT6{0}.FINSAT6{0}.HDF (TIP,BOLGE,TEMSILCI,URUNGRUP,HEDEF,TARIH) VALUES ({1},{2},{3},{4},'{5}',{6})
                    SELECT 1
                    END
                    ELSE BEGIN SELECT 10 END
                    END
                    ELSE BEGIN SELECT 5 END
                    ", vUser.SirketKodu, 1, hdf.BOLGE, hdf.TEMSILCI, hdf.URUNGRUP, hdf.HEDEF, Convert.ToInt32(DateTime.Today.ToOADate()));
                    break;
                default:
                    sorgu = String.Format(@"
                        DECLARE @MAXDURUM INT,@MAXHEDEF NUMERIC(25,6),@GNCHEDEF NUMERIC(25,6)
                        SET @MAXDURUM=(
                        SELECT COUNT(*) FROM FINSAT6{0}.FINSAT6{0}.HDF AS H WITH (NOLOCK) WHERE H.TIP=0 AND H.BOLGE={1})
                        IF(@MAXDURUM>0)
                        BEGIN
                        SET @GNCHEDEF =(SELECT HEDEF FROM FINSAT6{0}.FINSAT6{0}.HDF WITH (NOLOCK) WHERE ID={5} AND TIP=1)
                        SET @MAXHEDEF=(SELECT HH.HEDEF-ISNULL(D.HEDEF,0) FROM 
                        FINSAT6{0}.FINSAT6{0}.HDF AS HH WITH (NOLOCK)
                        LEFT JOIN 
                        (SELECT IC1.BOLGE,SUM(IC1.HEDEF) AS HEDEF FROM 
                        FINSAT6{0}.FINSAT6{0}.HDF AS IC1 WITH (NOLOCK) 
                        WHERE IC1.TIP=1 GROUP BY IC1.BOLGE
                        ) AS D ON HH.BOLGE=D.BOLGE
                        WHERE HH.TIP=0 AND HH.BOLGE={1})
                        IF((@MAXHEDEF+@GNCHEDEF)>={4})
                        BEGIN
                        UPDATE FINSAT6{0}.FINSAT6{0}.HDF SET BOLGE={1},TEMSILCI={2},URUNGRUP={3},HEDEF={4} WHERE ID={5} AND TIP=1
                        SELECT 1
                        END
                        ELSE BEGIN SELECT 10 END
                        END
                        ELSE BEGIN SELECT 5 END
                        ", vUser.SirketKodu, hdf.BOLGE, hdf.TEMSILCI, hdf.URUNGRUP, Convert.ToInt32(hdf.HEDEF), hdf.ID);
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
            List<RaporHdfList> hdfList;
            try
            {
                hdfList = db.Database.SqlQuery<RaporHdfList>(String.Format(RaporHdfList.Sorgu, vUser.SirketKodu)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TargetList");
                hdfList = new List<RaporHdfList>();
            }
            return PartialView(hdfList);
        }
        public PartialViewResult TargetEdit(int? id)
        {
            HDF h;
            List<RaporGrupKod> _raporGrupKod;
            List<RaporTargetUrunGrup> _raporTargetUrunGrup;
            if (CheckPerm(Perms.Raporlar, PermTypes.Updating) == false) { return null; }
            try
            {
                _raporGrupKod = db.Database.SqlQuery<RaporGrupKod>(String.Format(RaporGrupKod.Sorgu, vUser.SirketKodu)).ToList();
                _raporTargetUrunGrup = db.Database.SqlQuery<RaporTargetUrunGrup>(String.Format(RaporTargetUrunGrup.Sorgu, vUser.SirketKodu)).ToList();
                h = db.Database.SqlQuery<HDF>(String.Format(@"SELECT K.* FROM [FINSAT6{0}].[FINSAT6{0}].HDF AS K WITH (NOLOCK) WHERE K.ID={1} AND K.TIP=1", vUser.SirketKodu, id)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TargetEdit");
                h = new HDF();
                _raporGrupKod = new List<RaporGrupKod>();
                _raporTargetUrunGrup = new List<RaporTargetUrunGrup>();
            }
            ViewBag.BOLGE = new SelectList(_raporGrupKod, "RowID", "GrupKod", h.BOLGE);
            ViewBag.URUNGRUP = new SelectList(_raporTargetUrunGrup, "RowID", "UrunGrup", h.URUNGRUP);
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
            ViewBag.BOLGE = new SelectList(_raporGrupKod, "RowID", "GrupKod");
            return View("TargetBolge", new HDF());
        }
        public PartialViewResult TargetBolgeList()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return null; }
            List<RaporBolgeHdfList> hdfList;
            try
            {
                hdfList = db.Database.SqlQuery<RaporBolgeHdfList>(String.Format(RaporBolgeHdfList.Sorgu, vUser.SirketKodu)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TargetBolgeList");
                hdfList = new List<RaporBolgeHdfList>();
            }
            return PartialView(hdfList);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult TargetBolgeSave(HDF hdf)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Writing) == false) { return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet); }
            var _Result = new Result(true);
            string sorgu = "";
            switch (hdf.ID)
            {
                case 0:
                    sorgu = String.Format(@"INSERT INTO FINSAT6{0}.FINSAT6{0}.HDF (TIP,BOLGE,HEDEF,TARIH) VALUES (0,{1},'{2}',{3})"
                    , vUser.SirketKodu, hdf.BOLGE, hdf.HEDEF, Convert.ToInt32(DateTime.Today.ToOADate()));
                    break;
                default:
                    sorgu = String.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.HDF SET BOLGE={1},HEDEF='{2}' WHERE ID={3} AND TIP=0", vUser.SirketKodu, hdf.BOLGE, Convert.ToInt32(hdf.HEDEF), hdf.ID);
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
                Logger(ex, "/Reports/Financial/TargetBolgeSave");
                _Result.Status = false;
                _Result.Message = "İşlem başarısız";
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult TargetBolgeEdit(int? id)
        {
            HDF h;
            List<RaporGrupKod> _raporGrupKod;
            if (CheckPerm(Perms.Raporlar, PermTypes.Updating) == false) { return null; }
            try
            {
                _raporGrupKod = db.Database.SqlQuery<RaporGrupKod>(String.Format(RaporGrupKod.Sorgu, vUser.SirketKodu)).ToList();
                h = db.Database.SqlQuery<HDF>(String.Format(@"SELECT K.* FROM [FINSAT6{0}].[FINSAT6{0}].HDF AS K WITH (NOLOCK) WHERE K.ID={1} AND K.TIP=0", vUser.SirketKodu, id)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TargetBolgeEdit");
                h = new HDF();
                _raporGrupKod = new List<RaporGrupKod>();
            }
            ViewBag.BOLGE = new SelectList(_raporGrupKod, "RowID", "GrupKod", h.BOLGE);
            return PartialView(h);
        }
        public JsonResult TargetBolgeDelete(string Id)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Deleting) == false) { return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet); }
            string sorgu = "";
            sorgu = String.Format(@"DELETE FROM [FINSAT6{0}].[FINSAT6{0}].HDF WHERE ID={1} AND TIP=0", vUser.SirketKodu, Id);
            try
            {
                db.Database.ExecuteSqlCommand(sorgu);
                LogActions("Reports", "Financial", "Delete", ComboItems.alSil, Id.ToInt32());
                return Json(new Result(true, Id.ToInt32()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Financial/TargetBolgeDelete");
                return Json(new Result(false, "Projeye ait form bulunduğu için silme işlemi gerçekleştirilememiştir."), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}