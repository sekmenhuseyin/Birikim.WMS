﻿using Birikim.Models;
using System;
using System.Collections.Generic;
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
            var girisDepo = db.Database.SqlQuery<frmDepoList>(string.Format("SELECT Depo, DepoAdi + ' [' + Depo + ']' as DepoAdi FROM FINSAT6{0}.FINSAT6{0}.DEP WHERE Depo <> 'TD' ORDER BY DepoAdi", vUser.SirketKodu)).ToList();
            var cikisDepo = db.Database.SqlQuery<frmDepoList>(string.Format(@"SELECT        UYSPLN6{0}.UYSPLN6{0}.SIN.SEntry as Depo, FINSAT6{0}.FINSAT6{0}.DEP.DepoAdi + ' [' + Depo + ']' as DepoAdi
																			FROM            UYSPLN6{0}.UYSPLN6{0}.SIN INNER JOIN FINSAT6{0}.FINSAT6{0}.DEP ON UYSPLN6{0}.UYSPLN6{0}.SIN.SEntry = FINSAT6{0}.FINSAT6{0}.DEP.Depo
																			WHERE(UYSPLN6{0}.UYSPLN6{0}.SIN.SSection = 'DepoUsers') AND(UYSPLN6{0}.UYSPLN6{0}.SIN.SValue LIKE '%{1}%') AND(FINSAT6{0}.FINSAT6{0}.DEP.Depo <> 'TD')
																			 ORDER BY DepoAdi", vUser.SirketKodu, vUser.UserName)).ToList();
            ViewBag.GirisDepo = new SelectList(girisDepo, "Depo", "DepoAdi");
            ViewBag.CikisDepo = new SelectList(cikisDepo, "Depo", "DepoAdi");
            return View("Index");
        }

        /// <summary>
        /// malzeme koduna göre stok ve serinoyu getirir
        /// </summary>
        [HttpPost]
        public JsonResult GetSeri(string MalKodu, int Tarih, string Depo)
        {
            var sql = string.Format(@"SELECT SeriNo as id, convert(varchar(50), isnull(sum(case when IslemTur = 0 then Miktar else -Miktar end), 0)) as value
											FROM FINSAT6{0}.FINSAT6{0}.STI WITH(nolock)
											where STI.MalKodu = '{1}' and Tarih <= {2} and Depo = '{3}' and IrsFat <> 2 and KynkEvrakTip <> 95 and KynkEvrakTip not in (141, 142, 143, 144) and not(KynkEvrakTip in (68, 69) and ErekIIFKEvrakTip in (5, 2) and IrsFat = 3)
											GROUP by STI.MalKodu, Depo, SeriNo", vUser.SirketKodu, MalKodu, Tarih, Depo);
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
        public JsonResult GetStock(string MalKodu, int Tarih, string SeriNo, string Depo, decimal Miktar)
        {
            // miktar kontrol
            if (fn.isNumeric(Miktar) == false)
                return Json(new frmMalKoduMiktar() { MalKodu = "Hata", Birim = "Miktar hatalı" }, JsonRequestBehavior.AllowGet);
            else if (Miktar < 0)
                return Json(new frmMalKoduMiktar() { MalKodu = "Hata", Birim = "Miktar hatalı" }, JsonRequestBehavior.AllowGet);
            // kontrol
            var sql = string.Format(@"SELECT
                                    ISNULL((SELECT SUM(CASE WHEN IslemTur = 0 THEN Miktar ELSE - Miktar END) AS Miktar
                                        FROM FINSAT6{0}.FINSAT6{0}.STI WITH (nolock) LEFT OUTER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH (nolock) ON FINSAT6{0}.FINSAT6{0}.STK.MalKodu = FINSAT6{0}.FINSAT6{0}.STI.MalKodu
                                        WHERE (FINSAT6{0}.FINSAT6{0}.STI.MalKodu = '{1}') AND (FINSAT6{0}.FINSAT6{0}.STI.Tarih <= {2}) AND (FINSAT6{0}.FINSAT6{0}.STI.SeriNo = '{4}') AND (DATALENGTH(FINSAT6{0}.FINSAT6{0}.STI.SeriNo) = {5}) AND (FINSAT6{0}.FINSAT6{0}.STI.Depo = '{3}') AND (FINSAT6{0}.FINSAT6{0}.STI.IrsFat <> 2) AND (FINSAT6{0}.FINSAT6{0}.STI.KynkEvrakTip <> 95)and KynkEvrakTip not in (141,142,143,144) and not (KynkEvrakTip in (68,69) and ErekIIFKEvrakTip in (5,2) and IrsFat = 3)
                                        GROUP BY FINSAT6{0}.FINSAT6{0}.STI.MalKodu, FINSAT6{0}.FINSAT6{0}.STI.Depo, FINSAT6{0}.FINSAT6{0}.STI.SeriNo), 0) as Miktar1,
                                    ISNULL((SELECT SUM(CASE WHEN IslemTur = 0 THEN Miktar ELSE - Miktar END) AS Miktar
                                        FROM FINSAT6{0}.FINSAT6{0}.STI WITH (nolock) LEFT OUTER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH (nolock) ON FINSAT6{0}.FINSAT6{0}.STK.MalKodu = FINSAT6{0}.FINSAT6{0}.STI.MalKodu
                                        WHERE (FINSAT6{0}.FINSAT6{0}.STI.MalKodu = '{1}') AND (FINSAT6{0}.FINSAT6{0}.STI.SeriNo = '{4}') AND (DATALENGTH(FINSAT6{0}.FINSAT6{0}.STI.SeriNo) = {5}) AND (FINSAT6{0}.FINSAT6{0}.STI.Depo = '{3}') AND (FINSAT6{0}.FINSAT6{0}.STI.IrsFat <> 2) AND(FINSAT6{0}.FINSAT6{0}.STI.KynkEvrakTip <> 95) and KynkEvrakTip not in (141,142,143,144) and not (KynkEvrakTip in (68,69) and ErekIIFKEvrakTip in (5,2) and IrsFat = 3)
                                        GROUP BY FINSAT6{0}.FINSAT6{0}.STI.MalKodu, FINSAT6{0}.FINSAT6{0}.STI.Depo, FINSAT6{0}.FINSAT6{0}.STI.SeriNo), 0) as Miktar2",
                                    vUser.SirketKodu, MalKodu, Tarih, Depo, SeriNo, SeriNo.Length);
            var stokKontrol = db.Database.SqlQuery<frmUysStokKontrol>(sql).FirstOrDefault();
            if (stokKontrol.Miktar1 < Miktar || stokKontrol.Miktar2 < Miktar)
                return Json(new frmMalKoduMiktar() { MalKodu = "Hata", Birim = "Stok yetersiz" }, JsonRequestBehavior.AllowGet);
            // return
            sql = string.Format(@"SELECT '{1}' as MalKodu, isnull(sum(case when IslemTur = 0 then Miktar else -Miktar end), 0) as Miktar, Birim1 as Birim
									FROM FINSAT6{0}.FINSAT6{0}.STI WITH (nolock) left join FINSAT6{0}.FINSAT6{0}.STK WITH (nolock) ON STK.MalKodu = STI.MalKodu
									where STI.MalKodu = '{1}' and Tarih <= {2} and SeriNo = '{4}' and DATALENGTH(SeriNo) = {5} and Depo ='{3}' and IrsFat <> 2 and KynkEvrakTip <> 95 and KynkEvrakTip not in (141,142,143,144) and not (KynkEvrakTip in (68,69) and ErekIIFKEvrakTip in (5,2) and IrsFat = 3)
									GROUP by STI.MalKodu, Depo, SeriNo, Birim1", vUser.SirketKodu, MalKodu, Tarih, Depo, SeriNo, SeriNo.Length);
            return Json(db.Database.SqlQuery<frmMalKoduMiktar>(sql).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// transfer kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Save(frmUysTransfer tbl)
        {
            // kontrol
            if (tbl.GirisDepo == tbl.CikisDepo)
            {
                return Json(new Result(false, "Çıkış depo ile giriş depo aynı olamaz"), JsonRequestBehavior.AllowGet);
            }

            var listKontrol = new List<frmUysTransferDetay>();
            for (int i = 0; i < tbl.MalKodu.Length; i++)
            {
                var varmi = false;
                if (tbl.Miktar[i] <= 0)
                {
                    return Json(new Result(false, "'" + tbl.SeriNo[i] + "' serili '" + tbl.MalKodu[i] + "' ürünü için yanlış miktar yazılmış"), JsonRequestBehavior.AllowGet);
                }

                foreach (var item in listKontrol)
                {
                    if (item.MalKodu == tbl.MalKodu[i] && item.SeriNo == tbl.SeriNo[i])
                    {
                        item.Miktar += tbl.Miktar[i];
                        varmi = true;
                        continue;
                    }
                }

                if (varmi == false)
                {
                    listKontrol.Add(new frmUysTransferDetay() { MalKodu = tbl.MalKodu[i], Birim = tbl.Birim[i], SeriNo = tbl.SeriNo[i], Miktar = tbl.Miktar[i] });
                }
            }

            // stok kontrol
            foreach (var item in listKontrol)
            {
                var sql = string.Format(@"SELECT
                                    ISNULL((SELECT SUM(CASE WHEN IslemTur = 0 THEN Miktar ELSE - Miktar END) AS Miktar
                                        FROM FINSAT6{0}.FINSAT6{0}.STI WITH (nolock) LEFT OUTER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH (nolock) ON FINSAT6{0}.FINSAT6{0}.STK.MalKodu = FINSAT6{0}.FINSAT6{0}.STI.MalKodu
                                        WHERE (FINSAT6{0}.FINSAT6{0}.STI.MalKodu = '{1}') AND (FINSAT6{0}.FINSAT6{0}.STI.Tarih <= {2}) AND (FINSAT6{0}.FINSAT6{0}.STI.SeriNo = '{4}') AND (DATALENGTH(FINSAT6{0}.FINSAT6{0}.STI.SeriNo) = {5}) AND (FINSAT6{0}.FINSAT6{0}.STI.Depo = '{3}') AND (FINSAT6{0}.FINSAT6{0}.STI.IrsFat <> 2) AND (FINSAT6{0}.FINSAT6{0}.STI.KynkEvrakTip <> 95)and KynkEvrakTip not in (141,142,143,144) and not (KynkEvrakTip in (68,69) and ErekIIFKEvrakTip in (5,2) and IrsFat = 3)
                                        GROUP BY FINSAT6{0}.FINSAT6{0}.STI.MalKodu, FINSAT6{0}.FINSAT6{0}.STI.Depo, FINSAT6{0}.FINSAT6{0}.STI.SeriNo), 0) as Miktar1,
                                    ISNULL((SELECT SUM(CASE WHEN IslemTur = 0 THEN Miktar ELSE - Miktar END) AS Miktar
                                        FROM FINSAT6{0}.FINSAT6{0}.STI WITH (nolock) LEFT OUTER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH (nolock) ON FINSAT6{0}.FINSAT6{0}.STK.MalKodu = FINSAT6{0}.FINSAT6{0}.STI.MalKodu
                                        WHERE (FINSAT6{0}.FINSAT6{0}.STI.MalKodu = '{1}') AND (FINSAT6{0}.FINSAT6{0}.STI.SeriNo = '{4}') AND (DATALENGTH(FINSAT6{0}.FINSAT6{0}.STI.SeriNo) = {5}) AND (FINSAT6{0}.FINSAT6{0}.STI.Depo = '{3}') AND (FINSAT6{0}.FINSAT6{0}.STI.IrsFat <> 2) AND(FINSAT6{0}.FINSAT6{0}.STI.KynkEvrakTip <> 95) and KynkEvrakTip not in (141,142,143,144) and not (KynkEvrakTip in (68,69) and ErekIIFKEvrakTip in (5,2) and IrsFat = 3)
                                        GROUP BY FINSAT6{0}.FINSAT6{0}.STI.MalKodu, FINSAT6{0}.FINSAT6{0}.STI.Depo, FINSAT6{0}.FINSAT6{0}.STI.SeriNo), 0) as Miktar2",
                                        vUser.SirketKodu, item.MalKodu, tbl.Tarih, tbl.CikisDepo, item.SeriNo, item.SeriNo.Length);
                var stokKontrol = db.Database.SqlQuery<frmUysStokKontrol>(sql).FirstOrDefault();
                if (stokKontrol.Miktar1 < item.Miktar || stokKontrol.Miktar2 < item.Miktar)
                    return Json(new Result(false, "'" + item.SeriNo + "' serili '" + item.MalKodu + "' ürünü için stok yetersiz"), JsonRequestBehavior.AllowGet);
            }

            // variables
            var uysf = new UYSF(SqlExper, vUser.SirketKodu);
            var tarih = fn.ToOADate();
            var saat = fn.ToOATime();
            // get son emir no
            var EmirVeEvrak = db.Database.SqlQuery<frmUysEmirEvrak>(string.Format(@"SELECT
										ISNULL((SELECT TOP (1) EmirNo FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE (EmirNo LIKE 'DD%') ORDER BY Row_ID DESC), 'DD000000') as EmirNo,
										ISNULL((SELECT TOP (1) EvrakNo FROM (
													SELECT EvrakNo FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo LIKE 'DD%')
													UNION
													SELECT StiNo as EvrakNo FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE (EmirNo LIKE 'DD%')
												)t ORDER BY EvrakNo DESC), 'DD000000') as EvrakNo", vUser.SirketKodu)).FirstOrDefault();
            if (EmirVeEvrak.EvrakNo == "") EmirVeEvrak.EvrakNo = "DD000000";
            EmirVeEvrak.EmirNo = uysf.EvrakNoArttir(EmirVeEvrak.EmirNo, "DD");
            EmirVeEvrak.EvrakNo = uysf.EvrakNoArttir(EmirVeEvrak.EvrakNo, "DD");
            // create list
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
                    Kaydeden2 = vUser.FullName,
                    MalKodu = tbl.MalKodu[i],
                    SeriNo = tbl.SeriNo[i],
                    Birim = tbl.Birim[i],
                    Miktar = tbl.Miktar[i],
                    Tarih = tbl.Tarih
                });
            }

            var emir = new EMG2();
            emir.DefaultValueSet();
            emir.EmirNo = EmirVeEvrak.EmirNo;
            emir.BasTarih = tarih;
            emir.BasSaat = saat;
            emir.Talimat2 = vUser.FullName;
            emir.Kod2 = tbl.CikisDepo;
            emir.Kod3 = tbl.GirisDepo;
            emir.StiNo = EmirVeEvrak.EvrakNo;
            emir.KayitTarih = tarih;
            emir.KayitSaat = saat;
            // send to db
            var sonuc = uysf.DepoTransfer(liste, emir, false);
            return Json(sonuc, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// onay bekleyen transfer sayfası
        /// </summary>
        public ActionResult Waiting() => View("Waiting");

        /// <summary>
        /// onay bekleyen transfer listesi
        /// </summary>
        public PartialViewResult WaitingList(bool Id)
        {
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
            // execute sql
            var liste = db.Database.SqlQuery<frmUysWaitingTransfer>(string.Format(sql, vUser.SirketKodu)).ToList();
            return PartialView("WaitingList", liste);
        }

        /// <summary>
        /// onay bekleyen transfer listesi
        /// </summary>
        public PartialViewResult Details(string EvrakNo, bool Tip)
        {
            var sql = "";
            if (Tip)//onaylanmış transfer
                sql = @"SELECT        UYSPLN6{0}.UYSPLN6{0}.EMG.EmirNo, FINSAT6{0}.FINSAT6{0}.STI.EvrakNo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod2 AS CikisDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod3 AS GirisDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Talimat2 AS Kaydeden, UYSPLN6{0}.UYSPLN6{0}.EMG.Talimat3 AS Kaydeden2, FINSAT6{0}.FINSAT6{0}.STI.Tarih,
                                            FINSAT6{0}.FINSAT6{0}.STI.MalKodu, FINSAT6{0}.FINSAT6{0}.STI.SeriNo, FINSAT6{0}.FINSAT6{0}.STI.Miktar, FINSAT6{0}.FINSAT6{0}.STI.Birim, FINSAT6{0}.FINSAT6{0}.STK.MalAdi
                        FROM            UYSPLN6{0}.UYSPLN6{0}.EMG INNER JOIN FINSAT6{0}.FINSAT6{0}.STI ON UYSPLN6{0}.UYSPLN6{0}.EMG.TrsfrNo = FINSAT6{0}.FINSAT6{0}.STI.EvrakNo INNER JOIN FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.STI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu
                        WHERE        (FINSAT6{0}.FINSAT6{0}.STI.KynkEvrakTip = 53) AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTip = 6) AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTur = 0) AND (UYSPLN6{0}.UYSPLN6{0}.EMG.EmirNo = '{1}')";
            else
                sql = @"SELECT        UYSPLN6{0}.UYSPLN6{0}.EMG.EmirNo, FINSAT6{0}.FINSAT6{0}.STI.EvrakNo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod2 AS CikisDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod3 AS GirisDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Talimat2 AS Kaydeden, UYSPLN6{0}.UYSPLN6{0}.EMG.Talimat3 AS Kaydeden2, FINSAT6{0}.FINSAT6{0}.STI.Tarih,
                                            FINSAT6{0}.FINSAT6{0}.STI.MalKodu, FINSAT6{0}.FINSAT6{0}.STI.SeriNo, FINSAT6{0}.FINSAT6{0}.STI.Miktar, FINSAT6{0}.FINSAT6{0}.STI.Birim, FINSAT6{0}.FINSAT6{0}.STK.MalAdi
                        FROM            UYSPLN6{0}.UYSPLN6{0}.EMG INNER JOIN FINSAT6{0}.FINSAT6{0}.STI ON UYSPLN6{0}.UYSPLN6{0}.EMG.StiNo = FINSAT6{0}.FINSAT6{0}.STI.EvrakNo INNER JOIN FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.STI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu
                        WHERE        (FINSAT6{0}.FINSAT6{0}.STI.KynkEvrakTip = 53) AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTip = 6) AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTur = 0) AND (UYSPLN6{0}.UYSPLN6{0}.EMG.EmirNo = '{1}')";
            // execute sql
            var liste = db.Database.SqlQuery<frmUysWaitingTransfer>(string.Format(sql, vUser.SirketKodu, EvrakNo)).ToList();
            var Row_ID = db.Database.SqlQuery<frmUysDepoYetki>(string.Format(@"SELECT
                                    (SELECT Row_ID FROM UYSPLN6{0}.UYSPLN6{0}.SIN WHERE(SSection = 'DepoUsers') AND(SValue LIKE '%{1}%') AND(SEntry = '{2}')) as GirisYetki,
                                    (SELECT Row_ID FROM UYSPLN6{0}.UYSPLN6{0}.SIN WHERE(SSection = 'DepoUsers') AND(SValue LIKE '%{1}%') AND(SEntry = '{3}')) as CikisYetki",
                                    vUser.SirketKodu, vUser.UserName, liste[0].GirisDepo, liste[0].CikisDepo)).FirstOrDefault();
            // yetki
            ViewBag.Yetki = Tip == true ? false : (Row_ID.GirisYetki == null ? false : true);//onaylanmışsa tekrar onay olmayacak, onaylanmamışsa yetkiye bakacak
            ViewBag.Yetki2 = Tip == true ? (Row_ID.GirisYetki == null ? false : true) : (Row_ID.CikisYetki == null ? false : true);//onaylanmışsa giriş yetkiye sahip olan silebilir, onaylanmamışsa çıkış yetkiye sahip olan silebilir
            ViewBag.Tip = Tip;
            return PartialView("Details", liste);
        }

        /// <summary>
        /// bekleyen transferi onayla
        /// </summary>
        /// <param name="ID">EmirNo</param>
        [HttpPost]
        public JsonResult Approve(string ID)
        {
            var uysf = new UYSF(SqlExper, vUser.SirketKodu);
            var tarih = fn.ToOADate();
            var saat = fn.ToOATime();
            // get son emir no
            var EmirVeEvrak = db.Database.SqlQuery<frmUysEmirEvrak>(string.Format(@"SELECT '' as EmirNo,
										ISNULL((SELECT TOP (1) EvrakNo FROM (
													SELECT EvrakNo FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo LIKE 'DG%')
													UNION
													SELECT TrsfrNo as EvrakNo FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE (EmirNo LIKE 'DD%')
												)t ORDER BY EvrakNo DESC), 'DG000000') as EvrakNo", vUser.SirketKodu)).FirstOrDefault();
            if (EmirVeEvrak.EvrakNo == "") EmirVeEvrak.EvrakNo = "DG000000";
            EmirVeEvrak.EvrakNo = uysf.EvrakNoArttir(EmirVeEvrak.EvrakNo, "DG");
            // create list
            var liste = db.Database.SqlQuery<frmUysWaitingTransfer>(string.Format(@"
									SELECT       '{2}' as EvrakNo, 'TD' as AraDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod2 as CikisDepo, UYSPLN6{0}.UYSPLN6{0}.EMG.Kod3 as GirisDepo, '{3}' as Kaydeden, '{4}' AS Kaydeden2, FINSAT6{0}.FINSAT6{0}.STI.MalKodu, FINSAT6{0}.FINSAT6{0}.STI.SeriNo,
																FINSAT6{0}.FINSAT6{0}.STI.Birim, FINSAT6{0}.FINSAT6{0}.STI.Miktar, {5} as Tarih
									FROM            UYSPLN6{0}.UYSPLN6{0}.EMG INNER JOIN
																FINSAT6{0}.FINSAT6{0}.STI ON UYSPLN6{0}.UYSPLN6{0}.EMG.StiNo = FINSAT6{0}.FINSAT6{0}.STI.EvrakNo
									WHERE        (UYSPLN6{0}.UYSPLN6{0}.EMG.EmirNo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTur = 1) AND (FINSAT6{0}.FINSAT6{0}.STI.KynkEvrakTip = 53) AND (FINSAT6{0}.FINSAT6{0}.STI.IslemTip = 6)
							", vUser.SirketKodu, ID, EmirVeEvrak.EvrakNo, vUser.UserName, vUser.FullName, tarih)).ToList();
            // send to db
            var sonuc = uysf.DepoTransfer(liste, null, true);
            if (sonuc.Status == true)
            {
                db.Database.ExecuteSqlCommand(string.Format(@"UPDATE UYSPLN6{0}.UYSPLN6{0}.EMG
                        SET BitTarih = {2}, BitSaat = {3}, Talimat3 = '{4}', TrsfrNo = '{5}', Degistiren= '{6}', DegisTarih = {2}, DegisSaat = {3}, CurDurum = 1, RecID = -1, Birim = -1, CurDurSb = -1, SonDurSb = -1, PlOnay = -1, YMUret = -1, YMMly = -1, YMEndMly = -1, YMDepo = -1, YMHmdCik = -1, Teklif = -1, KayitTuru = -1
                        WHERE EmirNo = '{1}'", vUser.SirketKodu, ID, tarih, saat, vUser.FullName, EmirVeEvrak.EvrakNo, vUser.UserName));
            }

            return Json(sonuc, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// transfer sil
        /// </summary>
        /// <param name="ID">EvrakNo</param>
        public JsonResult Delete(string ID, bool Tip)
        {
            var tarih = fn.ToOADate();
            var saat = fn.ToOATime();
            var sql = string.Format("DELETE FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo = '{1}') AND (KynkEvrakTip = 53) AND (IslemTip = 6);", vUser.SirketKodu, ID);
            if (Tip == false)//onaylanmamışsa emg sil
            {
                sql += string.Format("DELETE FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE (StiNo = '{1}');", vUser.SirketKodu, ID);
            }
            else//onaylanmışsa sadece güncelle
            {
                sql += string.Format(@"UPDATE UYSPLN6{0}.UYSPLN6{0}.EMG
                        SET BitTarih = 0, BitSaat = 0, Talimat3 = '', TrsfrNo = '', Degistiren= '{4}', DegisTarih = {2}, DegisSaat = {3}, RecID = 0, Birim = 0, CurDurum = 0, CurDurSb = 0, SonDurSb = 0, PlOnay = 0, YMUret = 0, YMMly = 0, YMEndMly = 0, YMDepo = 0, YMHmdCik = 0, Teklif = 0, KayitTuru = 0
                        WHERE TrsfrNo = '{1}'", vUser.SirketKodu, ID, tarih, saat, vUser.UserName);
            }

            // ürün listesi
            var liste = db.Database.SqlQuery<frmUysWaitingTransfer>(string.Format(@"SELECT MalKodu, Birim, Miktar, case when IslemTur=0 then '' else Depo end as CikisDepo, case when IslemTur=1 then '' else Depo end as GirisDepo FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (KynkEvrakTip = 53) AND (IslemTip = 6) AND (EvrakNo = '{1}')", vUser.SirketKodu, ID)).ToList();
            foreach (var item in liste)
            {
                // ara depodan düş
                if (item.CikisDepo == "")
                {
                    sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.DST " +
                        "SET GirMiktar = GirMiktar - {3}, SonGirTarih = {5}, Degistiren = '{4}', DegisTarih = {5}, DegisSaat = {6} " +
                        "WHERE (MalKodu = '{1}') AND (Depo = '{2}');", vUser.SirketKodu, item.MalKodu, item.GirisDepo, item.Miktar.ToDot(), vUser.UserName, tarih, saat);
                    sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.STK " +
                        "SET TahminiStok = TahminiStok - {2}, GirMiktar = GirMiktar - {2},  Degistiren = '{3}', DegisTarih = {4}, DegisSaat = {5} " +
                        "WHERE (MalKodu = '{1}');", vUser.SirketKodu, item.MalKodu, item.Miktar.ToDot(), vUser.UserName, tarih, saat);
                }

                // çıkış depodan düş
                sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.DST " +
                    "SET CikMiktar = CikMiktar - {3}, SonCikTarih = {5}, Degistiren = '{4}', DegisTarih = {5}, DegisSaat = {6} " +
                    "WHERE (MalKodu = '{1}') AND (Depo = '{2}');", vUser.SirketKodu, item.MalKodu, item.CikisDepo, item.Miktar.ToDot(), vUser.UserName, tarih, saat);
                sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.STK " +
                    "SET TahminiStok = TahminiStok + {2}, CikMiktar = CikMiktar - {2}, Degistiren = '{3}', DegisTarih = {4}, DegisSaat = {5} " +
                    "WHERE (MalKodu = '{1}');", vUser.SirketKodu, item.MalKodu, item.Miktar.ToDot(), vUser.UserName, tarih, saat);
            }

            // execute
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
    }
}