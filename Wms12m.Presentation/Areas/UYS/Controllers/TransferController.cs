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
            var liste = db.Database.SqlQuery<frmWaitingList>(string.Format(@"SELECT StiNo, StiNo + ': ' + Kod2 + ' => ' + Kod3 + ' (' + BIRIKIM.wms.fnFormatDateFromInt(BasTarih) + ')' as Depo FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE (StiNo NOT IN
                                                                                    (SELECT StiNo FROM UYSPLN6{0}.UYSPLN6{0}.EMG AS EMG_1 WHERE (TrsfrNo <> '') GROUP BY StiNo))
                                                                            ORDER BY BasTarih DESC, Kod2", db.GetSirketDBs().FirstOrDefault())).ToList();
            ViewBag.DurumID = new SelectList(liste, "StiNo", "Depo");
            return View("Waiting");
        }
        /// <summary>
        /// onay bekleyen transfer listesi
        /// </summary>
        public PartialViewResult WaitingList(string Id, bool Onay)
        {
            var liste = db.Database.SqlQuery<frmUysWaitingTransfer>(string.Format(@"SELECT FINSAT6{0}.FINSAT6{0}.STI.MalKodu, FINSAT6{0}.FINSAT6{0}.STI.Birim, FINSAT6{0}.FINSAT6{0}.STI.Miktar, FINSAT6{0}.FINSAT6{0}.STI.EvrakNo, FINSAT6{0}.FINSAT6{0}.STI.Kaydeden, FINSAT6{0}.FINSAT6{0}.STI.Tarih, FINSAT6{0}.FINSAT6{0}.STK.MalAdi, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod2 as CikisDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod3 as GirisDepo
                                                                                    FROM FINSAT6{0}.FINSAT6{0}.STI INNER JOIN FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.STI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu INNER JOIN UYSPLN6{0}.UYSPLN6{0}.EMG ON FINSAT6{0}.FINSAT6{0}.STI.EvrakNo = UYSPLN6{0}.UYSPLN6{0}.EMG.{2}
                                                                                    WHERE (FINSAT6{0}.FINSAT6{0}.STI.KynkEvrakTip = 53) AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTip = 6) AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTur = 1) AND (FINSAT6{0}.FINSAT6{0}.STI.EvrakNo = '{1}')", db.GetSirketDBs().FirstOrDefault(), Id, Onay == false ? "TrsfrNo" : "StiNo")).ToList();
            var Row_ID = db.Database.SqlQuery<int?>(string.Format("SELECT Row_ID FROM UYSPLN6{0}.UYSPLN6{0}.SIN WHERE (SSection = 'DepoUsers') AND (SValue LIKE '%{1}%') AND (SEntry = '{2}')", db.GetSirketDBs().FirstOrDefault(), vUser.UserName, liste[0].GirisDepo)).FirstOrDefault();
            ViewBag.Yetki = Onay == false ? false : Row_ID == null ? false : true;
            return PartialView("WaitingList", liste);
        }
        /// <summary>
        /// transfer sil
        /// </summary>
        public JsonResult GetComboList(bool ID)
        {
            var liste = new List<SelectListItem>();
            if (ID == true)//onay bekleyen listesi
            {
                liste = db.Database.SqlQuery<SelectListItem>(string.Format(@"SELECT StiNo as Value, StiNo + ': ' + Kod2 + ' => ' + Kod3 + ' (' + BIRIKIM.wms.fnFormatDateFromInt(BasTarih) + ')' as Text FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE (StiNo NOT IN
                                                                                    (SELECT StiNo FROM UYSPLN6{0}.UYSPLN6{0}.EMG AS EMG_1 WHERE (TrsfrNo <> '') GROUP BY StiNo))
                                                                            ORDER BY BasTarih DESC, Kod2", db.GetSirketDBs().FirstOrDefault())).ToList();
            }
            else//önceden onaylanmışların listesi
            {
                liste = db.Database.SqlQuery<SelectListItem>(string.Format(@"SELECT TrsfrNo as Value, TrsfrNo + ': ' + Kod2 + ' => ' + Kod3 + ' (' + BIRIKIM.wms.fnFormatDateFromInt(BasTarih) + ')' as Text FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE (TrsfrNo <> '') ORDER BY BasTarih DESC, Kod2", db.GetSirketDBs().FirstOrDefault())).ToList();
            }
            return Json(liste, JsonRequestBehavior.AllowGet);
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
            int tarih = fn.ToOADate();
            int saat = fn.ToOATime();
            //get son emir no
            var EmirVeEvrak = db.Database.SqlQuery<frmUysEmirEvrak>(string.Format(@"SELECT
                                        ISNULL((SELECT TOP (1) EmirNo FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE (EmirNo LIKE 'DD%') ORDER BY Row_ID DESC), 'DD000000') as EmirNo, 
                                        ISNULL((SELECT TOP (1) EvrakNo FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo LIKE 'DD%') ORDER BY Row_ID DESC), 'DD000000') as EvrakNo", db.GetSirketDBs().FirstOrDefault())).FirstOrDefault();
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
                    Miktar = tbl.Miktar[i]
                });
            }
            var emir = new EMG();
            emir.DefaultValueSet();
            emir.EmirNo = EmirVeEvrak.EmirNo;
            emir.BasTarih = tarih;
            emir.BasSaat = saat;
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
                                        ISNULL((SELECT TOP (1) EvrakNo FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo LIKE 'DG%') ORDER BY Row_ID DESC), 'DG000000') as EvrakNo", db.GetSirketDBs().FirstOrDefault())).FirstOrDefault();
            EmirVeEvrak.EmirNo = uysf.EvrakNoArttir(EmirVeEvrak.EmirNo, "DD");
            EmirVeEvrak.EvrakNo = uysf.EvrakNoArttir(EmirVeEvrak.EvrakNo, "DG");
            //create list
            var liste = db.Database.SqlQuery<frmUysWaitingTransfer>(string.Format(@"
                                                                            SELECT       '{2}' as EvrakNo, 'TD' as AraDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod2 as CikisDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod3 as GirisDepo, '{3}' as Kaydeden, '{4}' AS Kaydeden2, FINSAT6{0}.FINSAT6{0}.STI.MalKodu, FINSAT6{0}.FINSAT6{0}.STI.SeriNo, 
                                                                                                     FINSAT6{0}.FINSAT6{0}.STI.Birim, FINSAT6{0}.FINSAT6{0}.STI.Miktar
                                                                            FROM            UYSPLN6{0}.UYSPLN6{0}.EMG INNER JOIN
                                                                                                     FINSAT6{0}.FINSAT6{0}.STI ON UYSPLN6{0}.UYSPLN6{0}.EMG.StiNo = FINSAT6{0}.FINSAT6{0}.STI.EvrakNo
                                                                            WHERE        (UYSPLN6{0}.UYSPLN6{0}.EMG.EmirNo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTur = 1) AND (FINSAT6{0}.FINSAT6{0}.STI.KynkEvrakTip = 53) AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTip = 6)
                                                                    ", db.GetSirketDBs().FirstOrDefault(), ID, EmirVeEvrak.EvrakNo, vUser.UserName, vUser.FirstName)).ToList();
            var emir = db.Database.SqlQuery<EMG>(string.Format(@"SELECT * FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE EmirNo = '{1}'", db.GetSirketDBs().FirstOrDefault(), ID)).FirstOrDefault();
            emir.EmirNo = EmirVeEvrak.EmirNo;
            emir.BitTarih = tarih;
            emir.BitSaat = saat;
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
    }
}