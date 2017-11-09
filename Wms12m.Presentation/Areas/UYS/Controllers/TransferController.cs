using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.UYS.Controllers
{
    public class TransferController : RootController
    {
        #region Planlama
        /// <summary>
        /// transfer planlama
        /// </summary>
        public ActionResult Index()
        {
            var sirket = db.GetSirketDBs().FirstOrDefault();
            var GirisDepo = db.Database.SqlQuery<frmDepoList>(string.Format("SELECT Depo, DepoAdi + ' [' + Depo + ']' as DepoAdi FROM FINSAT6{0}.FINSAT6{0}.DEP WHERE Depo <> 'TD' ORDER BY DepoAdi", sirket)).ToList();
            var CikisDepo = db.Database.SqlQuery<frmDepoList>(string.Format(@"SELECT        UYSPLN6{0}.UYSPLN6{0}.SIN.SEntry as Depo, FINSAT6{0}.FINSAT6{0}.DEP.DepoAdi + ' [' + Depo + ']' as DepoAdi
																			FROM            UYSPLN6{0}.UYSPLN6{0}.SIN INNER JOIN FINSAT6{0}.FINSAT6{0}.DEP ON UYSPLN6{0}.UYSPLN6{0}.SIN.SEntry = FINSAT6{0}.FINSAT6{0}.DEP.Depo
																			WHERE(UYSPLN6{0}.UYSPLN6{0}.SIN.SSection = 'DepoUsers') AND(UYSPLN6{0}.UYSPLN6{0}.SIN.SValue LIKE '%{1}%') AND(FINSAT6{0}.FINSAT6{0}.DEP.Depo <> 'TD')
																			 ORDER BY DepoAdi", sirket, vUser.UserName)).ToList();
            ViewBag.GirisDepo = new SelectList(GirisDepo, "Depo", "DepoAdi");
            ViewBag.CikisDepo = new SelectList(CikisDepo, "Depo", "DepoAdi");
            return View("Index");
        }
        /// <summary>
        /// malzeme koduna göre stok ve serinoyu getirir
        /// </summary>
        [HttpPost]
        public JsonResult GetSeri(string MalKodu, int Tarih, string Depo)
        {
            string sql = String.Format(@"SELECT SeriNo as id, convert(varchar(50), isnull(sum(case when IslemTur = 0 then Miktar else -Miktar end), 0)) as value
											FROM FINSAT6{0}.FINSAT6{0}.STI WITH(nolock)
											where STI.MalKodu = '{1}' and Tarih <= {2} and Depo = '{3}' and IrsFat <> 2 and KynkEvrakTip <> 95 and KynkEvrakTip not in (141, 142, 143, 144) and not(KynkEvrakTip in (68, 69) and ErekIIFKEvrakTip in (5, 2) and IrsFat = 3)
											GROUP by STI.MalKodu, Depo, SeriNo", db.GetSirketDBs().FirstOrDefault(), MalKodu, Tarih, Depo);
            try
            {
                var list = db.Database.SqlQuery<frmJson>(sql).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "/UYS/Transfer/GetSeri");
                return Json(new List<frmJson>(), JsonRequestBehavior.AllowGet);
            }

        }
        /// <summary>
        /// ürün stoğu bul
        /// </summary>
        public JsonResult GetStock(string MalKodu, int Tarih, string SeriNo, string Depo)
        {
            var sql = string.Format(@"SELECT '{1}' as MalKodu, isnull(sum(case when IslemTur = 0 then Miktar else -Miktar end), 0) as Miktar, Birim1 as Birim
									FROM FINSAT6{0}.FINSAT6{0}.STI WITH (nolock) left join FINSAT6{0}.FINSAT6{0}.STK WITH (nolock) ON STK.MalKodu = STI.MalKodu 
									where STI.MalKodu = '{1}' and Tarih <= {2} and SeriNo = '{4}' and DATALENGTH(SeriNo) = {5} and Depo ='{3}' and IrsFat <> 2 and KynkEvrakTip <> 95 and KynkEvrakTip not in (141,142,143,144) and not (KynkEvrakTip in (68,69) and ErekIIFKEvrakTip in (5,2) and IrsFat = 3)
									GROUP by STI.MalKodu, Depo, SeriNo, Birim1", db.GetSirketDBs().FirstOrDefault(), MalKodu, Tarih, Depo, SeriNo, SeriNo.Length);
            return Json(db.Database.SqlQuery<frmMalKoduMiktar>(sql).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// transfer kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Save(frmUysTransfer tbl)
        {
            var uysf = new UYSF(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, db.GetSirketDBs().FirstOrDefault());
            int tarih = fn.ToOADate();
            int saat = fn.ToOATime();
            //get son emir no
            var EmirVeEvrak = db.Database.SqlQuery<frmUysEmirEvrak>(string.Format(@"SELECT
										ISNULL((SELECT TOP (1) EmirNo FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE (EmirNo LIKE 'DD%') ORDER BY Row_ID DESC), 'DD000000') as EmirNo, 
										ISNULL((SELECT TOP (1) EvrakNo FROM (
													SELECT EvrakNo FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo LIKE 'DD%')
													UNION
													SELECT StiNo as EvrakNo FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE (EmirNo LIKE 'DD%')
												)t ORDER BY EvrakNo DESC), 'DD000000') as EvrakNo", db.GetSirketDBs().FirstOrDefault())).FirstOrDefault();
            if (EmirVeEvrak.EvrakNo == "") EmirVeEvrak.EvrakNo = "DD000000";
            EmirVeEvrak.EmirNo = uysf.EvrakNoArttir(EmirVeEvrak.EmirNo, "DD");
            EmirVeEvrak.EvrakNo = uysf.EvrakNoArttir(EmirVeEvrak.EvrakNo, "DD");
            //create list
            var liste = new List<frmUysWaitingTransfer>();
            for (int i = 0; i < tbl.MalKodu.Length; i++)
            {
                liste.Add(new frmUysWaitingTransfer()
                {
                    EvrakNo = EmirVeEvrak.EvrakNo,
                    CikisDepo = tbl.CikisDepo,
                    AraDepo = "TD",
                    GirisDepo = tbl.GirisDepo,
                    Kaydeden = vUser.UserName,
                    Kaydeden2 = vUser.FirstName,
                    MalKodu = tbl.MalKodu[i],
                    SeriNo = tbl.SeriNo[i],
                    Birim = tbl.Birim[i],
                    Miktar = tbl.Miktar[i],
                    Tarih = tbl.Tarih
                });
            }
            var emir = new EMG();
            emir.DefaultValueSet();
            emir.EmirNo = EmirVeEvrak.EmirNo;
            emir.BasTarih = tarih;
            emir.Talimat2 = vUser.FirstName;
            emir.Kod2 = tbl.CikisDepo;
            emir.Kod3 = tbl.GirisDepo;
            emir.StiNo = EmirVeEvrak.EvrakNo;
            emir.KayitTarih = tarih;
            emir.KayitSaat = saat;
            //send to db
            var sonuc = uysf.DepoTransfer(liste, emir, false);
            return Json(sonuc, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Onaylama
        /// <summary>
        /// onay bekleyen transfer sayfası
        /// </summary>
        public ActionResult Waiting()
        {
            return View("Waiting");
        }
        /// <summary>
        /// onay bekleyen transfer listesi
        /// </summary>
        public PartialViewResult WaitingList(bool Id)
        {
            var sirket = db.GetSirketDBs().FirstOrDefault();
            var sql = "";
            if (Id)//onaylanmış transfer
                sql = @"SELECT        UYSPLN6{0}.UYSPLN6{0}.EMG.EmirNo, FINSAT6{0}.FINSAT6{0}.STI.EvrakNo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod2 AS CikisDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod3 AS GirisDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Talimat2 AS Kaydeden, UYSPLN6{0}.UYSPLN6{0}.EMG.Talimat3 AS Kaydeden2, FINSAT6{0}.FINSAT6{0}.STI.Tarih
						FROM            UYSPLN6{0}.UYSPLN6{0}.EMG INNER JOIN FINSAT6{0}.FINSAT6{0}.STI ON UYSPLN6{0}.UYSPLN6{0}.EMG.TrsfrNo = FINSAT6{0}.FINSAT6{0}.STI.EvrakNo
						WHERE        (FINSAT6{0}.FINSAT6{0}.STI.KynkEvrakTip = 53) AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTip = 6) AND (UYSPLN6{0}.UYSPLN6{0}.EMG.TrsfrNo <> '')
						GROUP BY UYSPLN6{0}.UYSPLN6{0}.EMG.EmirNo, FINSAT6{0}.FINSAT6{0}.STI.EvrakNo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod2, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod3, UYSPLN6{0}.UYSPLN6{0}.EMG.Talimat2, UYSPLN6{0}.UYSPLN6{0}.EMG.Talimat3, FINSAT6{0}.FINSAT6{0}.STI.Tarih
						ORDER BY FINSAT6{0}.FINSAT6{0}.STI.Tarih DESC, CikisDepo";
            else
                sql = @"SELECT        UYSPLN6{0}.UYSPLN6{0}.EMG.EmirNo, FINSAT6{0}.FINSAT6{0}.STI.EvrakNo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod2 AS CikisDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod3 AS GirisDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Talimat2 AS Kaydeden, UYSPLN6{0}.UYSPLN6{0}.EMG.Talimat3 AS Kaydeden2, FINSAT6{0}.FINSAT6{0}.STI.Tarih
						FROM            UYSPLN6{0}.UYSPLN6{0}.EMG INNER JOIN FINSAT6{0}.FINSAT6{0}.STI ON UYSPLN6{0}.UYSPLN6{0}.EMG.StiNo = FINSAT6{0}.FINSAT6{0}.STI.EvrakNo
						WHERE        (FINSAT6{0}.FINSAT6{0}.STI.KynkEvrakTip = 53) AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTip = 6) AND (UYSPLN6{0}.UYSPLN6{0}.EMG.TrsfrNo = '')
						GROUP BY UYSPLN6{0}.UYSPLN6{0}.EMG.EmirNo, FINSAT6{0}.FINSAT6{0}.STI.EvrakNo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod2, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod3, UYSPLN6{0}.UYSPLN6{0}.EMG.Talimat2, UYSPLN6{0}.UYSPLN6{0}.EMG.Talimat3, FINSAT6{0}.FINSAT6{0}.STI.Tarih
						ORDER BY FINSAT6{0}.FINSAT6{0}.STI.Tarih DESC, CikisDepo";
            //execute sql
            var liste = db.Database.SqlQuery<frmUysWaitingTransfer>(string.Format(sql, sirket)).ToList();
            return PartialView("WaitingList", liste);
        }
        /// <summary>
        /// onay bekleyen transfer listesi
        /// </summary>
        public PartialViewResult Details(string EvrakNo, bool Tip)
        {
            var sirket = db.GetSirketDBs().FirstOrDefault();
            var sql = "";
            if (Tip)//onaylanmış transfer
                sql = @"SELECT        UYSPLN6{0}.UYSPLN6{0}.EMG.EmirNo, FINSAT6{0}.FINSAT6{0}.STI.EvrakNo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod2 AS CikisDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod3 AS GirisDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Talimat2 AS Kaydeden, UYSPLN6{0}.UYSPLN6{0}.EMG.Talimat3 AS Kaydeden2, FINSAT6{0}.FINSAT6{0}.STI.Tarih
                                            FINSAT6{0}.FINSAT6{0}.STI.MalKodu, FINSAT6{0}.FINSAT6{0}.STI.SeriNo, FINSAT6{0}.FINSAT6{0}.STI.Miktar, FINSAT6{0}.FINSAT6{0}.STI.Birim
                        FROM            UYSPLN6{0}.UYSPLN6{0}.EMG INNER JOIN FINSAT6{0}.FINSAT6{0}.STI ON UYSPLN6{0}.UYSPLN6{0}.EMG.TrsfrNo = FINSAT6{0}.FINSAT6{0}.STI.EvrakNo
                        WHERE        (FINSAT6{0}.FINSAT6{0}.STI.KynkEvrakTip = 53) AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTip = 6) AND (UYSPLN6{0}.UYSPLN6{0}.EMG.EmirNo = '{1}')";
            else
                sql = @"SELECT        UYSPLN6{0}.UYSPLN6{0}.EMG.EmirNo, FINSAT6{0}.FINSAT6{0}.STI.EvrakNo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod2 AS CikisDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod3 AS GirisDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Talimat2 AS Kaydeden, UYSPLN6{0}.UYSPLN6{0}.EMG.Talimat3 AS Kaydeden2, FINSAT6{0}.FINSAT6{0}.STI.Tarih
                                            FINSAT6{0}.FINSAT6{0}.STI.MalKodu, FINSAT6{0}.FINSAT6{0}.STI.SeriNo, FINSAT6{0}.FINSAT6{0}.STI.Miktar, FINSAT6{0}.FINSAT6{0}.STI.Birim
                        FROM            UYSPLN6{0}.UYSPLN6{0}.EMG INNER JOIN FINSAT6{0}.FINSAT6{0}.STI ON UYSPLN6{0}.UYSPLN6{0}.EMG.StiNo = FINSAT6{0}.FINSAT6{0}.STI.EvrakNo
                        WHERE        (FINSAT6{0}.FINSAT6{0}.STI.KynkEvrakTip = 53) AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTip = 6) AND (UYSPLN6{0}.UYSPLN6{0}.EMG.EmirNo = '{1}')";
            //execute sql
            var liste = db.Database.SqlQuery<frmUysWaitingTransfer>(string.Format(sql, sirket, EvrakNo)).ToList();
            //yetki
            ViewBag.Yetki = db.Database.SqlQuery<string>(string.Format("SELECT SEntry FROM UYSPLN6{0}.UYSPLN6{0}.SIN WHERE (SSection = 'DepoUsers') AND (SValue LIKE '%{1}%')", sirket, vUser.UserName)).ToList();
            return PartialView("WaitingList", liste);
        }
        /// <summary>
        /// bekleyen transferi onayla
        /// </summary>
        [HttpPost]
        public JsonResult Approve(string ID)
        {
            var uysf = new UYSF(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, db.GetSirketDBs().FirstOrDefault());
            int tarih = fn.ToOADate();
            int saat = fn.ToOATime();
            //get son emir no
            var EmirVeEvrak = db.Database.SqlQuery<frmUysEmirEvrak>(string.Format(@"SELECT
										ISNULL((SELECT TOP (1) EmirNo FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE (EmirNo LIKE 'DD%') ORDER BY Row_ID DESC), 'DD000000') as EmirNo, 
										ISNULL((SELECT TOP (1) EvrakNo FROM (
													SELECT EvrakNo FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo LIKE 'DG%')
													UNION
													SELECT TrsfrNo as EvrakNo FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE (EmirNo LIKE 'DD%')
												)t ORDER BY EvrakNo DESC), 'DG000000') as EvrakNo", db.GetSirketDBs().FirstOrDefault())).FirstOrDefault();
            if (EmirVeEvrak.EvrakNo == "") EmirVeEvrak.EvrakNo = "DG000000";
            EmirVeEvrak.EmirNo = uysf.EvrakNoArttir(EmirVeEvrak.EmirNo, "DD");
            EmirVeEvrak.EvrakNo = uysf.EvrakNoArttir(EmirVeEvrak.EvrakNo, "DG");
            //create list
            var liste = db.Database.SqlQuery<frmUysWaitingTransfer>(string.Format(@"
																			SELECT       '{2}' as EvrakNo, 'TD' as AraDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod2 as CikisDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod3 as GirisDepo, '{3}' as Kaydeden, '{4}' AS Kaydeden2, FINSAT6{0}.FINSAT6{0}.STI.MalKodu, FINSAT6{0}.FINSAT6{0}.STI.SeriNo, 
																									 FINSAT6{0}.FINSAT6{0}.STI.Birim, FINSAT6{0}.FINSAT6{0}.STI.Miktar, {5} as Tarih
																			FROM            UYSPLN6{0}.UYSPLN6{0}.EMG INNER JOIN
																									 FINSAT6{0}.FINSAT6{0}.STI ON UYSPLN6{0}.UYSPLN6{0}.EMG.StiNo = FINSAT6{0}.FINSAT6{0}.STI.EvrakNo
																			WHERE        (UYSPLN6{0}.UYSPLN6{0}.EMG.StiNo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTur = 1) AND (FINSAT6{0}.FINSAT6{0}.STI.KynkEvrakTip = 53) AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTip = 6)
																	", db.GetSirketDBs().FirstOrDefault(), ID, EmirVeEvrak.EvrakNo, vUser.UserName, vUser.FirstName, tarih)).ToList();
            var emir = db.Database.SqlQuery<EMG>(string.Format(@"SELECT * FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE StiNo = '{1}'", db.GetSirketDBs().FirstOrDefault(), ID)).FirstOrDefault();
            emir.EmirNo = EmirVeEvrak.EmirNo;
            emir.BitTarih = tarih;
            emir.Talimat3 = vUser.FirstName;
            emir.TrsfrNo = EmirVeEvrak.EvrakNo;
            emir.KayitTarih = tarih;
            emir.KayitSaat = saat;
            emir.RecID = -1;
            emir.Birim = -1;
            emir.CurDurum = 1;
            emir.CurDurSb = -1;
            emir.SonDurSb = -1;
            emir.PlOnay = -1;
            emir.YMUret = -1;
            emir.YMMly = -1;
            emir.YMEndMly = -1;
            emir.YMDepo = -1;
            emir.YMHmdCik = -1;
            emir.Teklif = -1;
            emir.KayitTuru = -1;
            //send to db
            var sonuc = uysf.DepoTransfer(liste, emir, true);
            return Json(sonuc, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// transfer sil
        /// </summary>
        public JsonResult Delete(string ID)
        {
            var sirket = db.GetSirketDBs().FirstOrDefault();
            int tarih = fn.ToOADate();
            int saat = fn.ToOATime();
            var sql = string.Format("DELETE FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo = '{1}') AND (KynkEvrakTip = 53) AND (IslemTip = 6);DELETE FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE (StiNo = '{1}' AND TrsfrNo = '') OR (TrsfrNo = '{1}');", sirket, ID);
            //ürün listesi
            var liste = db.Database.SqlQuery<frmUysWaitingTransfer>(string.Format(@"SELECT MalKodu, Birim, Miktar, case when IslemTur=0 then '' else Depo end as CikisDepo, case when IslemTur=1 then '' else Depo end as GirisDepo FROM FINSAT6{0}.FINSAT6{0}.STI
																					WHERE (KynkEvrakTip = 53) AND (IslemTip = 6) AND (EvrakNo = '{1}')", sirket, ID)).ToList();
            foreach (var item in liste)
            {
                //ara depodan düş
                if (item.CikisDepo == "")
                {
                    sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.DST " +
                        "SET GirMiktar = GirMiktar - {3}, SonGirTarih = {5}, Degistiren = '{4}', DegisTarih = {5}, DegisSaat = {6} " +
                        "WHERE (MalKodu = '{1}') AND (Depo = '{2}');", sirket, item.MalKodu, item.GirisDepo, item.Miktar.ToDot(), vUser.UserName, tarih, saat);
                    sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.STK " +
                        "SET TahminiStok = TahminiStok - {2}, GirMiktar = GirMiktar - {2},  Degistiren = '{3}', DegisTarih = {4}, DegisSaat = {5} " +
                        "WHERE (MalKodu = '{1}');", sirket, item.MalKodu, item.Miktar.ToDot(), vUser.UserName, tarih, saat);
                }
                //çıkış depodan düş
                sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.DST " +
                    "SET CikMiktar = CikMiktar - {3}, SonCikTarih = {5}, Degistiren = '{4}', DegisTarih = {5}, DegisSaat = {6} " +
                    "WHERE (MalKodu = '{1}') AND (Depo = '{2}');", sirket, item.MalKodu, item.CikisDepo, item.Miktar.ToDot(), vUser.UserName, tarih, saat);
                sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.STK " +
                    "SET TahminiStok = TahminiStok + {2}, CikMiktar = CikMiktar - {2}, Degistiren = '{3}', DegisTarih = {4}, DegisSaat = {5} " +
                    "WHERE (MalKodu = '{1}');", sirket, item.MalKodu, item.Miktar.ToDot(), vUser.UserName, tarih, saat);
            }
            //execute
            try
            {
                db.Database.ExecuteSqlCommand(sql);
                return Json(new Result(true, 1), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "UYS/Transfer/Delete");
                return Json(new Result(false), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}