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
        /// <summary>
        /// transfer planlama
        /// </summary>
        public ActionResult Index()
        {
            var liste = db.Database.SqlQuery<frmDepoList>(string.Format("SELECT Depo, DepoAdi + ' [' + Depo + ']' as DepoAdi FROM FINSAT6{0}.FINSAT6{0}.DEP ORDER BY DepoAdi", db.GetSirketDBs().FirstOrDefault())).ToList();
            ViewBag.GirisDepo = new SelectList(liste, "Depo", "DepoAdi");
            ViewBag.CikisDepo = ViewBag.GirisDepo;
            return View("Index");
        }
        /// <summary>
        /// onay bekleyen transfer sayfası
        /// </summary>
        public ActionResult Waiting()
        {
            var liste = db.Database.SqlQuery<frmWaitingList>(string.Format(@"SELECT StiNo, Kod2 + ' => ' + Kod3 + ' (' + BIRIKIM.wms.fnFormatDateFromInt(BasTarih) + ')' as Depo FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE(StiNo NOT IN
                                                                                    (SELECT StiNo FROM UYSPLN6{0}.UYSPLN6{0}.EMG AS EMG_1 WHERE (TrsfrNo <> '') GROUP BY StiNo))
                                                                            ORDER BY BasTarih, Kod2", db.GetSirketDBs().FirstOrDefault())).ToList();
            ViewBag.DurumID = new SelectList(liste, "StiNo", "Depo");
            return View("Waiting");
        }
        /// <summary>
        /// onay bekleyen transfer listesi
        /// </summary>
        public PartialViewResult WaitingList(string Id)
        {
            var liste = db.Database.SqlQuery<frmUysWaitingTransfer>(string.Format(@"SELECT FINSAT6{0}.FINSAT6{0}.STI.MalKodu, FINSAT6{0}.FINSAT6{0}.STI.Birim, FINSAT6{0}.FINSAT6{0}.STI.Miktar, FINSAT6{0}.FINSAT6{0}.STI.EvrakNo, FINSAT6{0}.FINSAT6{0}.STI.Kaydeden, FINSAT6{0}.FINSAT6{0}.STI.Tarih, FINSAT6{0}.FINSAT6{0}.STK.MalAdi
                                                                                    FROM FINSAT6{0}.FINSAT6{0}.STI INNER JOIN FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.STI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu
                                                                                    WHERE (FINSAT6{0}.FINSAT6{0}.STI.KynkEvrakTip = 53) AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTip = 6) AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTur = 1) AND (FINSAT6{0}.FINSAT6{0}.STI.EvrakNo = '{1}')", db.GetSirketDBs().FirstOrDefault(), Id)).ToList();
            return PartialView("WaitingList", liste);
        }
        /// <summary>
        /// ürün stoğu bul
        /// </summary>
        public JsonResult GetStock(string MalKodu, int Tarih, string SeriNo, string Depo)
        {
            if (SeriNo != "") SeriNo = ("000000" + SeriNo).Right(6); else SeriNo = "      ";
            var sql = string.Format(@"SELECT '{1}' as MalKodu, isnull(sum(case when IslemTur = 0 then Miktar else -Miktar end), 0) as Miktar, (SELECT Birim1 FROM FINSAT6{0}.FINSAT6{0}.STK WHERE (MalKodu = '{1}')) as Birim
                                    FROM FINSAT6{0}.FINSAT6{0}.STI WITH (nolock) left join FINSAT6{0}.FINSAT6{0}.STK WITH (nolock) ON STK.MalKodu = STI.MalKodu 
                                    where STI.MalKodu = '{1}' and Tarih <= {2} and SeriNo = '  {3}' and Depo ='{4}'  and IrsFat <> 2 and KynkEvrakTip <> 95 and KynkEvrakTip not in (141,142,143,144) and not (KynkEvrakTip in (68,69) and ErekIIFKEvrakTip in (5,2) and IrsFat = 3)
                                    GROUP by STI.MalKodu, Depo,SeriNo", db.GetSirketDBs().FirstOrDefault(), MalKodu, Tarih, SeriNo, Depo);
            return Json(db.Database.SqlQuery<frmMalKoduMiktar>(sql).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// transfer sil
        /// </summary>
        public JsonResult Delete(string ID)
        {
            var sirket = db.GetSirketDBs().FirstOrDefault();
            int tarih = fn.ToOADate();
            int saat = fn.ToOATime();
            var sql = string.Format("DELETE FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo = '{1}') AND (KynkEvrakTip = 53) AND (IslemTip = 6);DELETE FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE (StiNo = '{1}');", sirket, ID);
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
                else
                {
                    sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.DST " +
                        "SET CikMiktar = CikMiktar - {3}, SonCikTarih = {5}, Degistiren = '{4}', DegisTarih = {5}, DegisSaat = {6} " +
                        "WHERE (MalKodu = '{1}') AND (Depo = '{2}');", sirket, item.MalKodu, item.GirisDepo, item.Miktar.ToDot(), vUser.UserName, tarih, saat);
                    sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.STK " +
                        "SET TahminiStok = TahminiStok + {2}, CikMiktar = CikMiktar - {2}, Degistiren = '{3}', DegisTarih = {4}, DegisSaat = {5} " +
                        "WHERE (MalKodu = '{1}');", sirket, item.MalKodu, item.Miktar.ToDot(), vUser.UserName, tarih, saat);
                }
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
        /// <summary>
        /// transfer kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Save(frmUysTransfer tbl)
        {
            var uysf = new UYSF(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, db.GetSirketDBs().FirstOrDefault());
            //get son emir no
            var EmirVeEvrak = db.Database.SqlQuery<frmUysEmirEvrak>(string.Format(@"SELECT
                                        (SELECT TOP (1) EmirNo FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE (EmirNo LIKE 'DD%') ORDER BY Row_ID DESC) as EmirNo, 
                                        (SELECT TOP (1) EvrakNo FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo LIKE 'DD%') ORDER BY Row_ID DESC) as EvrakNo", db.GetSirketDBs().FirstOrDefault())).FirstOrDefault();
            EmirVeEvrak.EvrakNo = uysf.EvrakNoArttir(EmirVeEvrak.EvrakNo, "DD");
            EmirVeEvrak.EmirNo = uysf.EvrakNoArttir(EmirVeEvrak.EmirNo, "DD");
            //create list
            var liste = new List<frmUysWaitingTransfer>();
            for (int i = 0; i < tbl.MalKodu.Length; i++)
            {
                liste.Add(new frmUysWaitingTransfer()
                {
                    EvrakNo = EmirVeEvrak.EvrakNo,
                    EmirNo = EmirVeEvrak.EmirNo,
                    CikisDepo = tbl.CikisDepo,
                    AraDepo = "TD",
                    GirisDepo = tbl.GirisDepo,
                    Kaydeden = vUser.UserName,
                    Kaydeden2 = vUser.FirstName,
                    MalKodu = tbl.MalKodu[i],
                    SeriNo = tbl.SeriNo[i],
                    Birim = tbl.Birim[i],
                    Miktar = tbl.Miktar[i]
                });
            }
            //send to db
            var sonuc = uysf.DepoTransfer(liste, false);
            return Json(sonuc, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// bekleyen transferi onayla
        /// </summary>
        [HttpPost]
        public JsonResult Approve(string ID)
        {
            var uysf = new UYSF(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, db.GetSirketDBs().FirstOrDefault());
            //get son emir no
            var EmirVeEvrak = db.Database.SqlQuery<frmUysEmirEvrak>(string.Format(@"SELECT
                                        (SELECT TOP (1) EmirNo FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE (EmirNo LIKE 'DD%') ORDER BY Row_ID DESC) as EmirNo, 
                                        (SELECT TOP (1) EvrakNo FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo LIKE 'DG%') ORDER BY Row_ID DESC) as EvrakNo", db.GetSirketDBs().FirstOrDefault())).FirstOrDefault();
            //create list
            var liste = db.Database.SqlQuery<frmUysWaitingTransfer>(string.Format(@"
                                                                            SELECT        '{2}' as EvrakNo, '{3}' as EmirNo, 'TD' as AraDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod2 as CikisDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod3 as GirisDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.EmirNo, FINSAT6{0}.FINSAT6{0}.STI.EvrakNo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kaydeden, UYSPLN6{0}.UYSPLN6{0}.EMG.Talimat2 AS Kaydeden2, FINSAT6{0}.FINSAT6{0}.STI.MalKodu, FINSAT6{0}.FINSAT6{0}.STI.SeriNo, 
                                                                                                     FINSAT6{0}.FINSAT6{0}.STI.Birim, FINSAT6{0}.FINSAT6{0}.STI.Miktar
                                                                            FROM            UYSPLN6{0}.UYSPLN6{0}.EMG INNER JOIN
                                                                                                     FINSAT6{0}.FINSAT6{0}.STI ON UYSPLN6{0}.UYSPLN6{0}.EMG.StiNo = FINSAT6{0}.FINSAT6{0}.STI.EvrakNo
                                                                            WHERE        (UYSPLN6{0}.UYSPLN6{0}.EMG.EmirNo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTur = 1) AND (FINSAT6{0}.FINSAT6{0}.STI.KynkEvrakTip = 53) AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTip = 6)
                                                                    ", db.GetSirketDBs().FirstOrDefault(), ID, EmirVeEvrak.EvrakNo, EmirVeEvrak.EmirNo)).ToList();
            //send to db
            var sonuc = uysf.DepoTransfer(liste, false);
            return Json(sonuc, JsonRequestBehavior.AllowGet);
        }
    }
}