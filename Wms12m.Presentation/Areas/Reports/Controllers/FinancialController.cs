﻿using Birikim.Models;
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

        public ActionResult AksiyonSatis()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult AksiyonSatisList(int Kod13)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var OYM = db.Database.SqlQuery<AksiyonSatis>(string.Format(@"SELECT STk.MalAdi4 As StkMalAdi4,CHK.GrupKod as CHKGrupKod, CHK.TipKod as CHKTipKod,
SUM(BirimMiktar) as BirimMiktar, SUM(Tutar-ToplamIskonto) as NetTutar   FROM FINSAT6{0}.FINSAT6{0}.SPI(NOLOCK) SPI
INNER JOIN FINSAT6{0}.FINSAT6{0}.STK(NOLOCK)STK ON STK.Malkodu = SPI.Malkodu
INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK(NOLOCK) CHK ON CHK.HesapKodu = SPI.Chk
WHERE SPI.Kynkevraktip = 62 and stk.Kod13 = {1}
GROUP BY  STk.MalAdi4, CHK.GrupKod, CHK.TipKod", vUser.SirketKodu, Kod13)).ToList();
            return PartialView("AksiyonSatisList", OYM);
        }
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
                        _Result.Message = "Hedef limiti aşılmıştır.Kontrol ediniz!";
                        break;
                    case 5:
                        _Result.Status = false;
                        _Result.Message = "Girilen Bölgeye ait miktar bulunmamaktadır.Kontrol ediniz!";
                        break;
                    case 15:
                        _Result.Status = false;
                        _Result.Message = "Girilen tarih aralığı ile ilgili kayıt girilmiştir.Kontrol ediniz";
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
            List<CTargetRapor> tl;
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
            return new JavaScriptSerializer().Serialize(tl);
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
            try
            {
                string sorgu = "", r = "";
                decimal toplamCiro = 0;
                r = YilAyConvert(Yil, Ay);
                sorgu = String.Format(CTargetRaporTemsilci.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil), (Convert.ToInt32(Ay) + 1), GrupKod, r);
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
            return new JavaScriptSerializer().Serialize(ctrt);
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
            try
            {
                string sorgu = "";
                sorgu = String.Format(UrunGrupRapor.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil), Convert.ToInt32(Ay) + 1, GrupKod);
                ugr = db.Database.SqlQuery<UrunGrupRapor>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                ugr = new List<UrunGrupRapor>();
                Logger(ex, "/Reports/Financial/TargetRaporUrunGrupSelect");
            }
            return new JavaScriptSerializer().Serialize(ugr);
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
            try
            {
                string sorgu = "";
                sorgu = String.Format(PRTRapor.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil), Convert.ToInt32(Ay) + 1, GrupKod);
                prt = db.Database.SqlQuery<PRTRapor>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                prt = new List<PRTRapor>();
                Logger(ex, "/Reports/Financial/TargetRaporPRTSelect");
            }
            return new JavaScriptSerializer().Serialize(prt);
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
        #region GunlukSiparis-YAPILDI
        public ActionResult RaporGunlukSiparis()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) { return Redirect("/"); }
            ViewBag.Yillar = HdfGrupProperties(1);
            ViewBag.Aylar = HdfGrupProperties(2);
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
                //TODO : TOP 100
                string sorgu = "";
                sorgu = String.Format(GunlukSiparis.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil), Convert.ToInt32(Ay) + 1, "TOP 100");
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
            try
            {
                //TODO : TOP 100
                string sorgu = "";
                sorgu = String.Format(BekleyenSips.Sorgu, vUser.SirketKodu, "TOP 100");
                bs = db.Database.SqlQuery<BekleyenSips>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                bs = new List<BekleyenSips>();
                Logger(ex, "/Reports/Financial/RaporBekleyenSiparislerSelect");
            }
            return new JavaScriptSerializer().Serialize(bs);
        }
        public PartialViewResult BekleyenMalzemeList(string HesapKodu)
        {
            ViewBag.HesapKodu = HesapKodu;
            return PartialView("BekleyenMalzemeList");
        }
        public string BekleyenMalzemeSelect(string HesapKodu)
        {
            List<BekleyenMalz> bm;
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
            return new JavaScriptSerializer().Serialize(bm);
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
            ViewBag.Yillar = HdfGrupProperties(1);
            return View(_raporGrupKod);
        }
        public PartialViewResult UrunSatisTemsilciAnalizList(string GrupKod, string Yil)
        {
            ViewBag.GrupKod = GrupKod;
            ViewBag.Yil = Yil;
            return PartialView("UrunSatisTemsilciAnalizList");
        }
        public string UrunSatisTemsilciAnalizSelect(string GrupKod, string Yil)
        {
            List<SatisAnaliziTemsilci> sat;
            try
            {
                string sorgu = "";
                sorgu = String.Format(SatisAnaliziTemsilci.Sorgu, vUser.SirketKodu, GrupKod, Convert.ToInt32(Yil));
                sat = db.Database.SqlQuery<SatisAnaliziTemsilci>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                sat = new List<SatisAnaliziTemsilci>();
                Logger(ex, "/Reports/Financial/UrunSatisTemsilciAnalizSelect");
            }
            return new JavaScriptSerializer().Serialize(sat);
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
            try
            {
                //TODO : TOP 100
                string sorgu = "", siparisTipi = "";
                switch (Sip)
                {
                    case "0": siparisTipi = ""; break;
                    case "1": siparisTipi = @"AND (SPI.EvrakNo LIKE 'W%' OR SPI.EVRAKNO LIKE 'KW%')"; break;
                    default: siparisTipi = @"AND (SPI.EvrakNo LIKE 'TW%')"; break;
                }
                sorgu = String.Format(WebSiparis.Sorgu, vUser.SirketKodu, Convert.ToInt32(Yil), Convert.ToInt32(Ay) + 1, siparisTipi, "TOP 100");
                ws = db.Database.SqlQuery<WebSiparis>(sorgu).ToList();
            }
            catch (Exception ex)
            {
                ws = new List<WebSiparis>();
                Logger(ex, "/Reports/Financial/RaporWebSiparisSelect");
            }
            return new JavaScriptSerializer().Serialize(ws);
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
            try
            {
                mc = db.Database.SqlQuery<MusteriCiro>(String.Format(MusteriCiro.Sorgu, vUser.SirketKodu)).ToList();
            }
            catch (Exception ex)
            {
                mc = new List<MusteriCiro>();
                Logger(ex, "/Reports/Financial/TumMusteriCiroSelect");
            }
            return new JavaScriptSerializer().Serialize(mc);
        }
        #endregion
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
            MMyyyy = ((Convert.ToInt32(ay) + 1).ToString().Length == 1 ? "0" + (Convert.ToInt32(ay) + 1).ToString() : (Convert.ToInt32(ay) + 1).ToString()) + yil;
            return MMyyyy;
        }
    }
}