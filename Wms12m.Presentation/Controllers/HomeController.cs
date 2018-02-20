using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class HomeController : RootController
    {
        /// <summary>
        /// Anasayfa
        /// </summary>
        public ActionResult Index()
        {
            var theCharts = new Charts(db, vUser.SirketKodu);
            Setting setts = ViewBag.settings;
            var bo = new BekleyenOnaylar();
            var tarih = fn.ToOADate();
            var islemtip = 0;
            var ay = 0;
            var doviz = "TL";
            var kod = "Tümü";
            //kutucuklar
            var tbl = new GetHomeSummary_Result { depo = 0, gorev = 0, kull = 0, MalKabul = 0, Paketle = 0, RafaKaldir = 0, Sayim = 0, Sevkiyat = 0, SiparisTopla = 0, yetki = "" };
            try
            {
                tbl = db.GetHomeSummary(vUser.UserName, vUser.Id, vUser.DepoId).FirstOrDefault();
            }
            catch (Exception)
            {
                // ignored
            }
            // Bekleyen Onaylar
            if (setts.OnayCek || setts.OnayFiyat || setts.OnayRisk || setts.OnaySiparis || setts.OnaySozlesme || setts.OnayStok || setts.OnayTekno || setts.OnayTeminat)
                if (tbl != null)
                    try
                    {
                        bo = theCharts.BekleyenOnaylar(tbl.yetki.Contains("'DashboardKasa'"));
                    }
                    catch (Exception ex)
                    {
                        Logger(ex, "Home/Index");
                    }
            ViewBag.BekleyenOnaylar = bo;
            // etkinlikler
            var tblEtki = db.Etkinliks.Where(m => m.Tekrarlayan == false && (m.TatilTipi == 76 || m.TatilTipi == 78) && m.Tarih == DateTime.Today).ToList();
            var tekrarlayan = db.Etkinliks.Where(m => m.Tekrarlayan && (m.TatilTipi == 76 || m.TatilTipi == 78) && m.Tarih.Day == DateTime.Today.Day && m.Tarih.Month == DateTime.Today.Month).ToList();
            foreach (var item in tekrarlayan)
            {
                var fark = DateTime.Today.Year - item.Tarih.Year;
                var item2 = new Etkinlik()
                {
                    Aciklama = item.Username + " " + item.Aciklama + " (" + fark + ". Yıldönümü)",
                    ComboItem_Name = item.ComboItem_Name
                };
                tblEtki.Add(item2);
            }
            ViewBag.Tatil = tblEtki;
            //charts
            ViewBag.Ay = ay;
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            ViewBag.Doviz = doviz;
            if (tbl != null)
            {
                if (tbl.yetki.Contains("'ChartGunlukSatis'"))
                {
                    ViewBag.ChartGunlukSatis = theCharts.ChartGunlukSatisAnalizi(tarih);
                    ViewBag.ChartGunlukZaman = theCharts.ChartGunlukZaman();
                    ViewBag.ChartYear2Day = theCharts.ChartYear2Day();
                    ViewBag.ChartGunlukSatisKriter = theCharts.ChartGunlukSatisAnalizi(kod, islemtip, tarih);
                }
                if (tbl.yetki.Contains("'ChartAylikSatisAnaliziBar'"))
                {
                    ViewBag.ChartAylikSatis = dbl.DB_Aylik_SatisAnalizi.Where(m => m.DB == vUser.SirketKodu).ToList();
                    ViewBag.ChartAylikSatisKodTipDoviz = dbl.DB_Aylik_SatisAnalizi_Tip_Kod_Doviz.Where(m => m.DB == vUser.SirketKodu && m.Grup == kod && m.Kriter == doviz && m.IslemTip == islemtip).ToList();
                }
                if (tbl.yetki.Contains("'ChartUrunGrubuSatis'"))
                {
                    ViewBag.Kriter = doviz;
                    ViewBag.ChartUrunGrubuSatis = dbl.DB_UrunGrubu_SatisAnalizi.Where(m => m.DB == vUser.SirketKodu && m.Ay == 0).ToList();
                    ViewBag.ChartUrunGrubuSatisKriter = dbl.DB_UrunGrubu_SatisAnalizi_Kriter.Where(m => m.DB == vUser.SirketKodu && m.Ay == tarih && m.GrupKod == doviz).ToList();
                }
                if (tbl.yetki.Contains("'ChartLokasyonSatis'"))
                {
                    ViewBag.Kriter = doviz;
                    ViewBag.ChartLokasyonSatis = dbl.DB_LokasyonBazli_SatisAnalizi.Where(m => m.DB == vUser.SirketKodu && m.Ay == tarih).ToList();
                    ViewBag.ChartLokasyonSatisKriter = dbl.DB_LokasyonBazli_SatisAnalizi_Kriter.Where(m => m.DB == vUser.SirketKodu && m.Ay == tarih && m.GrupKod == doviz).ToList();
                }
                if (tbl.yetki.Contains("'ChartBakiyeRiskAnalizi'"))
                {
                    ViewBag.ChartBakiyeRisk = dbl.DB_BakiyeRiskAnalizi.Where(m => m.DB == vUser.SirketKodu).ToList();
                }
                if (tbl.yetki.Contains("'ChartBaglantiUrunGrubu'"))
                {
                    ViewBag.ChartBaglantiUrunGrubu = dbl.DB_SatisBaglanti_UrunGrubu.Where(m => m.DB == vUser.SirketKodu).ToList();
                }
                if (tbl.yetki.Contains("'ChartGunlukMDFUretimi'"))
                {
                    ViewBag.ChartGunlukMDFUretimi = theCharts.ChartGunlukMdfUretimi(tarih);
                }
                if (tbl.yetki.Contains("'ChartBaglantiZamanCizelgesi'"))
                {
                    ViewBag.ChartBaglantiZamanCizelgesi = theCharts.ChartBaglantiZaman();
                }
                if (tbl.yetki.Contains("'ChartBolgeBazliSatisAnalizi'"))
                {
                    ViewBag.ChartBolgeBazliSatis = theCharts.ChartBolgeBazliSatisAnalizi(ay, kod);
                }
                if (tbl.yetki.Contains("'ChartBekleyenSiparisUrunGrubu'"))
                {
                    ViewBag.ChartBekleyenSiparisUrunGrubu = dbl.DB_SatisBaglanti_UrunGrubu.Where(m => m.DB == vUser.SirketKodu).ToList();
                    ViewBag.ChartBekleyenSiparisUrunGrubuMiktar = dbl.DB_SatisBaglanti_UrunGrubu.Where(m => m.DB == vUser.SirketKodu).ToList();
                    ViewBag.ChartBekleyenSiparisUrunGrubuMiktarKriter = dbl.DB_SatisBaglanti_UrunGrubu.Where(m => m.DB == vUser.SirketKodu).ToList();
                    ViewBag.ChartBekleyenSiparisMusteri = dbl.DB_SatisBaglanti_UrunGrubu.Where(m => m.DB == vUser.SirketKodu).ToList();
                }
            }
            // return
            return View("Index", tbl);
        }

        /// <summary>
        /// redirect from notification
        /// </summary>
        public ActionResult GoTo(string Id)
        {
            var ID = Id.ToInt32();
            var satir = db.Messages.FirstOrDefault(m => m.ID == ID);
            if (satir.Okundu == false)
            {
                satir.Okundu = true;
                db.SaveChanges();
            }
            //url varsa oraya git
            if (satir.URL != null) return Redirect(satir.URL);
            else return RedirectToAction("Index");
        }

        /// <summary>
        /// child view for menu
        /// </summary>
        /// <param name="mYeri">menu yeri no</param>
        /// <param name="mUstNo">üst menu no</param>
        /// <param name="url">current url</param>
        [ChildActionOnly]
        public PartialViewResult Menu(byte mYeri, short mUstNo, string url)
        {
            for (int i = 0; i < 3; i++)
            {
                var ind = url.IndexOf("/");
                url = url.Right(url.Length - ind - 1);
            }

            url = "/" + url;
            ViewBag.ustMenu = mUstNo;
            var tablo = db.MenuGetirici(ComboItems.WMS.ToInt32(), mYeri, vUser.RoleName, mUstNo, url).ToList();
            return PartialView("../Shared/Menu", tablo);
        }

        /// <summary>
        /// Notifications
        /// </summary>
        public PartialViewResult Notifications(bool Onay = false)
        {
            // sadece onaylama ise kaydet yeter
            if (Onay)
            {
                var tablo = db.Messages.Where(m => m.MesajTipi == 85 && m.Kime == vUser.UserName && m.Okundu == false).ToList();
                foreach (var item in tablo.Where(m => m.Okundu == false))
                    item.Okundu = true;

                db.SaveChanges();
                return null;
            }
            else//yeni mesaj geldiğinde signalR burayı çağırıyor
            {
                var trh = DateTime.Now.AddDays(-30);
                var tablo = db.Messages.Where(m => m.MesajTipi == 85 && m.Kime == vUser.UserName && (m.Okundu == false || m.Tarih > trh)).OrderByDescending(m => m.Tarih).ToList();
                return PartialView("../Shared/Notifications", tablo);
            }
        }

        /// <summary>
        /// UsersChat
        /// </summary>
        public PartialViewResult UsersChat(string ID)
        {
            if (ID == "")
                ViewBag.guid = "0";
            else
                ViewBag.guid = db.Users.Where(m => m.Kod == ID).Select(m => m.Guid.ToString()).FirstOrDefault();
            var sql = string.Format("BIRIKIM.dbo.GetChats @MesajTipi = {0}, @Kimden = N'{1}', @Kime = N'{2}'", ID == "" ? ComboItems.GrupMesajı.ToInt32() : ComboItems.KişiselMesaj.ToInt32(), ID, vUser.UserName);
            var list = db.Database.SqlQuery<frmMessages>(sql).ToList();
            return PartialView("../Shared/UsersChat", list);
        }

        public PartialViewResult GorevAylikCalisma(string user, int tarih, string tip)
        {
            ViewBag.user = user;
            ViewBag.tarih = tarih;
            ViewBag.userID = new SelectList(Persons.GetList(), "Kod", "AdSoyad", user);
            ViewBag.gorevcalismatarih7 = EnumHelperExtension.SelectListFor((Aylar)tarih);
            var yil = DateTime.Today.Year;
            if (tarih > DateTime.Today.Month) yil--;
            var sql = "";
            if (user != "") sql += "AND ong.GorevlerCalisma.Kaydeden = '" + user + "'";
            var liste = db.Database.SqlQuery<chartGorevCalisma>(string.Format(@"SELECT ong.ProjeForm.Proje, SUM(ong.GorevlerCalisma.Sure) AS Sure
                    FROM ong.GorevlerCalisma INNER JOIN ong.Gorevler ON ong.GorevlerCalisma.GorevID = ong.Gorevler.ID INNER JOIN ong.ProjeForm ON ong.Gorevler.ProjeFormID = ong.ProjeForm.ID
                    WHERE (ong.GorevlerCalisma.Tarih >= '{0}') AND (ong.GorevlerCalisma.Tarih < '{1}') {2}
                    GROUP BY ong.ProjeForm.Proje", new DateTime(yil, tarih, 1).ToString("yyyy-MM-dd"), new DateTime(yil, tarih, 1).AddMonths(1).ToString("yyyy-MM-dd"), sql)).ToList();
            if (tip == "Pie")
                return PartialView("GorevProjesi/AylikCalismaPie", liste);
            else
                return PartialView("GorevProjesi/AylikCalisma", liste);
        }

        public PartialViewResult GorevCalismaListesi(int tarihStart, int tarihEnd)
        {
            ViewBag.tarihStart = tarihStart;
            ViewBag.tarihStart2 = tarihStart.FromOADateInt();
            ViewBag.tarihEnd = tarihEnd;
            ViewBag.tarihEnd2 = tarihEnd.FromOADateInt();
            var liste = db.Database.SqlQuery<chartGorevCalismaAnaliz>(string.Format(@"SELECT ong.Musteri.Unvan, ong.ProjeForm.Proje, ong.Gorevler.Gorev, ong.GorevlerCalisma.Calisma AS Aciklama, ong.GorevlerCalisma.Kaydeden, SUM(ong.GorevlerCalisma.Sure) AS Sure, ong.GorevlerCalisma.Tarih, ProjeForm_1.GitGuid
                    FROM ong.GorevlerCalisma INNER JOIN
                                             ong.Gorevler ON ong.GorevlerCalisma.GorevID = ong.Gorevler.ID INNER JOIN
                                             ong.ProjeForm ON ong.Gorevler.ProjeFormID = ong.ProjeForm.ID INNER JOIN
                                             ong.Musteri ON ong.ProjeForm.MusteriID = ong.Musteri.ID LEFT OUTER JOIN
                                             ong.ProjeForm AS ProjeForm_1 ON ong.ProjeForm.PID = ProjeForm_1.ID
                    WHERE (ong.GorevlerCalisma.Tarih >= '{0}') AND (ong.GorevlerCalisma.Tarih <= '{1}')
                    GROUP BY ong.GorevlerCalisma.Kaydeden, ong.Musteri.Unvan, ong.ProjeForm.Proje, ong.Gorevler.Gorev, ong.GorevlerCalisma.Calisma, ong.GorevlerCalisma.Tarih, ProjeForm_1.GitGuid", tarihStart.FromOaDate().ToString("yyyy-MM-dd"), tarihEnd.FromOaDate().ToString("yyyy-MM-dd"))).ToList();
            return PartialView("GorevProjesi/CalismaListesi", liste);
        }

        public PartialViewResult GorevGit(int tarih)
        {
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            var liste = new List<ForJson>();
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(ViewBag.settings.GitServerAddress + "Repository/CommitsList/" + tarih);
                liste = JsonConvert.DeserializeObject<List<ForJson>>(json);
            }

            return PartialView("GorevProjesi/GorevGit", liste);
        }

        public PartialViewResult GorevGunlukCalisma(string user, int tarih, string tip)
        {
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.user = user;
            ViewBag.userID = new SelectList(Persons.GetList(), "Kod", "AdSoyad", user);
            var liste = db.Database.SqlQuery<chartGorevCalisma>(string.Format(@"SELECT ong.ProjeForm.Proje, SUM(ong.GorevlerCalisma.Sure) AS Sure
                    FROM ong.GorevlerCalisma INNER JOIN ong.Gorevler ON ong.GorevlerCalisma.GorevID = ong.Gorevler.ID INNER JOIN ong.ProjeForm ON ong.Gorevler.ProjeFormID = ong.ProjeForm.ID
                    WHERE (ong.GorevlerCalisma.Tarih = '{0}') AND (case when '{1}' <> '' then case when (ong.GorevlerCalisma.Kaydeden = '{1}') then 1 else 0 end else 1 end = 1)
                    GROUP BY ong.ProjeForm.Proje", tarih.FromOaDate().ToString("yyyy-MM-dd"), user)).ToList();
            if (tip == "Pie")
                return PartialView("GorevProjesi/GunlukCalismaPie", liste);
            else
                return PartialView("GorevProjesi/GunlukCalisma", liste);
        }

        public PartialViewResult GorevMusteriAnalizi(int musteri, int proje, int tarihStart, int tarihEnd, string tip)
        {
            ViewBag.tarihStart = tarihStart;
            ViewBag.tarihStart2 = tarihStart.FromOADateInt();
            ViewBag.tarihEnd = tarihEnd;
            ViewBag.tarihEnd2 = tarihEnd.FromOADateInt();
            ViewBag.musteri = musteri;
            ViewBag.proje = proje;
            ViewBag.MusteriID = new SelectList(db.Musteris.OrderBy(m => m.Unvan).ToList(), "ID", "Unvan", musteri);
            if (musteri == 0)
                ViewBag.ProjeID = new SelectList(new List<ProjeForm>(), "ID", "Proje");
            else
                ViewBag.ProjeID = new SelectList(db.ProjeForms.Where(m => m.MusteriID == musteri && m.PID == null).OrderBy(m => m.Proje).ToList(), "ID", "Proje", proje);
            var sql = "";
            if (musteri > 0) sql += " AND ong.ProjeForm.MusteriID = " + musteri;
            if (proje > 0) sql += " AND ong.ProjeForm.PID = " + proje;
            var liste = db.Database.SqlQuery<chartGorevCalisma>(string.Format(@"SELECT ong.GorevlerCalisma.Kaydeden AS Proje, SUM(ong.GorevlerCalisma.Sure) AS Sure
                    FROM ong.GorevlerCalisma INNER JOIN ong.Gorevler ON ong.GorevlerCalisma.GorevID = ong.Gorevler.ID INNER JOIN ong.ProjeForm ON ong.Gorevler.ProjeFormID = ong.ProjeForm.ID
                    WHERE (ong.GorevlerCalisma.Tarih >= '{0}') AND (ong.GorevlerCalisma.Tarih <= '{1}'){2}
                    GROUP BY ong.GorevlerCalisma.Kaydeden", tarihStart.FromOaDate().ToString("yyyy-MM-dd"), tarihEnd.FromOaDate().ToString("yyyy-MM-dd"), sql)).ToList();
            if (tip == "Pie")
                return PartialView("GorevProjesi/MusteriAnaliziPie", liste);
            else
                return PartialView("GorevProjesi/MusteriAnalizi", liste);
        }

        public PartialViewResult GorevProjeAnalizi(int musteri, int proje)
        {
            ViewBag.musteri = musteri;
            ViewBag.proje = proje;
            ViewBag.MusteriID = new SelectList(db.Musteris.OrderBy(m => m.Unvan).ToList(), "ID", "Unvan", musteri);
            if (musteri == 0)
                ViewBag.ProjeID = new SelectList(new List<ProjeForm>(), "ID", "Proje");
            else
                ViewBag.ProjeID = new SelectList(db.ProjeForms.Where(m => m.MusteriID == musteri && m.PID == null).OrderBy(m => m.Proje).ToList(), "ID", "Proje", proje);
            var sql = "";
            if (musteri > 0) sql += "AND ong.ProjeForm.MusteriID = " + musteri;
            if (proje > 0) sql += "AND ong.ProjeForm.PID = " + proje;
            // listeyi getir
            var liste = db.Database.SqlQuery<chartGorevProje1>(string.Format(@"SELECT YEAR(ong.GorevlerCalisma.Tarih) AS Yil, MONTH(ong.GorevlerCalisma.Tarih) AS Ay, SUM(ong.GorevlerCalisma.Sure) AS Sure
                        FROM ong.GorevlerCalisma INNER JOIN ong.Gorevler ON ong.GorevlerCalisma.GorevID = ong.Gorevler.ID INNER JOIN ong.ProjeForm ON ong.Gorevler.ProjeFormID = ong.ProjeForm.ID
                        WHERE        (ong.GorevlerCalisma.ID > 0) {1}
                        GROUP BY YEAR(ong.GorevlerCalisma.Tarih), MONTH(ong.GorevlerCalisma.Tarih)
                        HAVING        (YEAR(ong.GorevlerCalisma.Tarih) > {0})
                        ORDER BY Yil, Ay", DateTime.Today.Year - 2, sql)).ToList();
            // yeni liste oluştur
            var sonliste = new List<chartGorevProje>();
            for (int i = 0; i < 12; i++)
            {
                sonliste.Add(new chartGorevProje() { Ay = ((Aylar)i + 1).ToString(), GecenYil = 0, BuYil = 0 });
            }

            // ilk listeyi yeni listeye yaz
            foreach (var item in liste)
            {
                if (item.Yil == DateTime.Today.Year - 1)
                    sonliste[item.Ay - 1].GecenYil = item.Sure;
                else
                    sonliste[item.Ay - 1].BuYil = item.Sure;
            }

            return PartialView("GorevProjesi/ProjeAnalizi", sonliste);
        }

        public PartialViewResult AylikSatisAnaliziBar()
        {
            if (CheckPerm(Perms.ChartAylikSatisAnaliziBar, PermTypes.Reading) == false) return PartialView("Satis/AylikSatisAnaliziBar", new List<DB_Aylik_SatisAnalizi>());
            var liste = dbl.DB_Aylik_SatisAnalizi.Where(m => m.DB == vUser.SirketKodu).ToList();
            if (liste.Count == 0)
                liste = new Charts(db, vUser.SirketKodu).ChartMonthly();
            return PartialView("Satis/AylikSatisAnaliziBar", liste);
        }

        public PartialViewResult AylikSatisAnaliziKodTipDovizBar(string kod, int islemtip, string doviz)
        {
            ViewBag.Doviz = doviz;
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            if (CheckPerm(Perms.ChartAylikSatisAnaliziBar, PermTypes.Reading) == false) return PartialView("Satis/AylikSatisAnaliziKodTipDovizBar", new List<DB_Aylik_SatisAnalizi_Tip_Kod_Doviz>());
            var liste = dbl.DB_Aylik_SatisAnalizi_Tip_Kod_Doviz.Where(m => m.DB == vUser.SirketKodu && m.Grup == kod && m.Kriter == doviz && m.IslemTip == islemtip).ToList();
            if (liste.Count == 0)
                liste = new Charts(db, vUser.SirketKodu).ChartMonthlyByKriter(kod, islemtip, doviz);

            return PartialView("Satis/AylikSatisAnaliziKodTipDovizBar", liste);
        }

        public PartialViewResult AylikSatisCHKAnaliziBar(string chk)
        {
            ViewBag.CHK = chk;
            if (CheckPerm(Perms.ChartAylikSatisAnaliziBar, PermTypes.Reading) == false) return PartialView("Satis/AylikSatisCHKAnaliziBar", new List<ChartAylikSatisAnalizi>());
            List<ChartAylikSatisAnalizi> liste = new List<ChartAylikSatisAnalizi>
                {
                    new ChartAylikSatisAnalizi() { Ay = "0", Yil2015 = 0, Yil2016 = 0, Yil2017 = 0 }
                };
            if (chk != "")
                liste = new Charts(db, vUser.SirketKodu).ChartAylikSatisAnalizi(chk);

            return PartialView("Satis/AylikSatisCHKAnaliziBar", liste);
        }

        public PartialViewResult BaglantiUrunGrubu()
        {
            if (CheckPerm(Perms.ChartBaglantiUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BaglantiUrunGrubu", new List<DB_SatisBaglanti_UrunGrubu>());
            var liste = dbl.DB_SatisBaglanti_UrunGrubu.Where(m => m.DB == vUser.SirketKodu).ToList();
            if (liste.Count == 0)
                liste = new Charts(db, vUser.SirketKodu).BaglantiUrunGrubu();

            return PartialView("Satis/BaglantiUrunGrubu", liste);
        }

        public PartialViewResult BaglantiUrunGrubuPie()
        {
            if (CheckPerm(Perms.ChartBaglantiUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BaglantiUrunGrubuPie", new List<DB_SatisBaglanti_UrunGrubu>());
            var liste = dbl.DB_SatisBaglanti_UrunGrubu.Where(m => m.DB == vUser.SirketKodu).ToList();
            if (liste.Count == 0)
                liste = new Charts(db, vUser.SirketKodu).BaglantiUrunGrubu();

            return PartialView("Satis/BaglantiUrunGrubuPie", liste);
        }

        public PartialViewResult BaglantiZamanCizelgesi()
        {
            if (CheckPerm(Perms.ChartBaglantiZamanCizelgesi, PermTypes.Reading) == false) return PartialView("Satis/BaglantiZamanCizelgesi", new List<ChartBaglantiZaman>());
            List<ChartBaglantiZaman> liste;
            liste = new Charts(db, vUser.SirketKodu).ChartBaglantiZaman();

            return PartialView("Satis/BaglantiZamanCizelgesi", liste);
        }

        public PartialViewResult BakiyeRiskAnalizi()
        {
            if (CheckPerm(Perms.ChartBakiyeRiskAnalizi, PermTypes.Reading) == false) return PartialView("Satis/BakiyeRiskAnalizi", new List<DB_BakiyeRiskAnalizi>());
            var liste = dbl.DB_BakiyeRiskAnalizi.Where(m => m.DB == vUser.SirketKodu).ToList();
            if (liste.Count == 0)
                liste = new Charts(db, vUser.SirketKodu).BakiyeRiskAnalizi();
            return PartialView("Satis/BakiyeRiskAnalizi", liste);
        }

        public PartialViewResult BekleyenSiparisMusteriAnalizi(string kod, string doviz)
        {
            ViewBag.Doviz = doviz;
            ViewBag.Kriter = kod;
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BekleyenSiparisMusteriAnalizi", new List<ChartBekleyenSiparisUrunGrubu>());
            var liste = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_Musteri_Analizi] @Kod = '{1}', @Kriter = '{2}'", vUser.SirketKodu, kod, doviz)).ToList();
            return PartialView("Satis/BekleyenSiparisMusteriAnalizi", liste);
        }

        public PartialViewResult BekleyenSiparisUrunGrubu(int bastarih, int bittarih)
        {
            ViewBag.BasTarih = bastarih;
            ViewBag.BasTarih2 = bastarih.FromOADateInt();
            ViewBag.BitTarih = bittarih;
            ViewBag.BitTarih2 = bittarih.FromOADateInt();
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BekleyenSiparisUrunGrubu", new List<ChartBekleyenSiparisUrunGrubu>());
            var liste = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu] @BasTarih = {1}, @BitTarih = {2}", vUser.SirketKodu, bastarih, bittarih)).ToList();

            return PartialView("Satis/BekleyenSiparisUrunGrubu", liste);
        }

        public PartialViewResult BekleyenSiparisUrunGrubuMiktar(bool miktarTutar)
        {
            ViewBag.MiktarTutar = "Miktar";
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BekleyenSiparisUrunGrubuMiktar", new List<CachedChartBekleyenUrunMiktarFiyat_Result>());
            List<CachedChartBekleyenUrunMiktarFiyat_Result> liste;
            if (miktarTutar)
            {
                liste = db.CachedChartBekleyenUrunMiktarFiyat(vUser.SirketKodu, true).ToList();
                ViewBag.MiktarTutar = "Miktar";
                if (liste.Count == 0)
                    liste = db.Database.SqlQuery<CachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Miktar]", vUser.SirketKodu)).ToList();
            }
            else
            {
                liste = db.CachedChartBekleyenUrunMiktarFiyat(vUser.SirketKodu, false).ToList();
                ViewBag.MiktarTutar = "Tutar";
                if (liste.Count == 0)
                    liste = db.Database.SqlQuery<CachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat]", vUser.SirketKodu)).ToList();
            }

            return PartialView("Satis/BekleyenSiparisUrunGrubuMiktar", liste);
        }

        public PartialViewResult BekleyenSiparisUrunGrubuMiktarKriter(string kriter)
        {
            ViewBag.MiktarTutar = "Tutar";
            ViewBag.Kriter = kriter;
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BekleyenSiparisUrunGrubuMiktarKriter", new List<CachedChartBekleyenUrunMiktarFiyat_Result>());
            var liste = db.Database.SqlQuery<CachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat_Kriter] @Kriter='{1}'", vUser.SirketKodu, kriter)).ToList();

            return PartialView("Satis/BekleyenSiparisUrunGrubuMiktarKriter", liste);
        }

        public PartialViewResult BekleyenSiparisUrunGrubuMiktarKriterPie(string kriter)
        {
            ViewBag.MiktarTutar = "Tutar";
            ViewBag.Kriter = kriter;
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BekleyenSiparisUrunGrubuMiktarKriterPie", new List<CachedChartBekleyenUrunMiktarFiyat_Result>());
            var liste = db.Database.SqlQuery<CachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat_Kriter] @Kriter='{1}'", vUser.SirketKodu, kriter)).ToList();

            return PartialView("Satis/BekleyenSiparisUrunGrubuMiktarKriterPie", liste);
        }

        public PartialViewResult BekleyenSiparisUrunGrubuMiktarPie(bool miktarTutar)
        {
            ViewBag.MiktarTutar = "Miktar";
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BekleyenSiparisUrunGrubuMiktarPie", new List<CachedChartBekleyenUrunMiktarFiyat_Result>());
            List<CachedChartBekleyenUrunMiktarFiyat_Result> liste;
            if (miktarTutar)
            {
                liste = db.CachedChartBekleyenUrunMiktarFiyat(vUser.SirketKodu, true).ToList();
                if (liste.Count == 0)
                    liste = db.Database.SqlQuery<CachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Miktar]", vUser.SirketKodu)).ToList();

                ViewBag.MiktarTutar = "Miktar";
            }
            else
            {
                liste = db.CachedChartBekleyenUrunMiktarFiyat(vUser.SirketKodu, false).ToList();
                if (liste.Count == 0)
                    liste = db.Database.SqlQuery<CachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat]", vUser.SirketKodu)).ToList();

                ViewBag.MiktarTutar = "Tutar";
            }

            return PartialView("Satis/BekleyenSiparisUrunGrubuMiktarPie", liste);
        }

        public PartialViewResult BolgeBazliSatisAnalizi(int ay, string kriter)
        {
            ViewBag.Ay = ay;
            ViewBag.Kriter = kriter;
            if (CheckPerm(Perms.ChartBolgeBazliSatisAnalizi, PermTypes.Reading) == false) return PartialView("Satis/BolgeBazliSatisAnalizi", new List<ChartBolgeBazliSatisAnalizi>());
            var liste = new Charts(db, vUser.SirketKodu).ChartBolgeBazliSatisAnalizi(ay, kriter);

            return PartialView("Satis/BolgeBazliSatisAnalizi", liste);
        }

        public PartialViewResult BolgeBazliSatisAnaliziPie(int ay, string kriter)
        {
            ViewBag.Ay = ay;
            ViewBag.Kriter = kriter;
            if (CheckPerm(Perms.ChartBolgeBazliSatisAnalizi, PermTypes.Reading) == false) return PartialView("Satis/BolgeBazliSatisAnaliziPie", new List<ChartBolgeBazliSatisAnalizi>());
            var liste = new Charts(db, vUser.SirketKodu).ChartBolgeBazliSatisAnalizi(ay, kriter);
            return PartialView("Satis/BolgeBazliSatisAnaliziPie", liste);
        }

        public PartialViewResult GunlukMDFUretimi(int tarih)
        {
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            if (CheckPerm(Perms.ChartGunlukMDFUretimi, PermTypes.Reading) == false) return PartialView("Satis/GunlukMDFUretim", new List<ChartGunlukMDFUretimi>());
            var liste = new Charts(db, vUser.SirketKodu).ChartGunlukMdfUretimi(tarih);

            return PartialView("Satis/GunlukMDFUretim", liste);
        }

        public PartialViewResult GunlukMDFUretimiPie(int tarih)
        {
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            if (CheckPerm(Perms.ChartGunlukMDFUretimi, PermTypes.Reading) == false) return PartialView("Satis/GunlukMDFUretimPie", new List<ChartGunlukMDFUretimi>());
            var liste = new Charts(db, vUser.SirketKodu).ChartGunlukMdfUretimi(tarih);

            return PartialView("Satis/GunlukMDFUretimPie", liste);
        }

        public PartialViewResult GunlukSatis(int tarih)
        {
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("Satis/GunlukSatis", new List<ChartGunlukSatisAnalizi>());
            var liste = new Charts(db, vUser.SirketKodu).ChartGunlukSatisAnalizi(tarih);
            return PartialView("Satis/GunlukSatis", liste);
        }

        public PartialViewResult GunlukSatisDoubleKriter(string kod, int islemtip, int tarih)
        {
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("Satis/GunlukSatisAnaliziDoubleKriter", new List<ChartGunlukSatisAnalizi>());
            var liste = new Charts(db, vUser.SirketKodu).ChartGunlukSatisAnalizi(kod, islemtip, tarih);

            return PartialView("Satis/GunlukSatisAnaliziDoubleKriter", liste);
        }

        public PartialViewResult GunlukSatisDoubleKriterPie(string kod, int islemtip, int tarih)
        {
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("Satis/GunlukSatisAnaliziDoubleKriterPie", new List<ChartGunlukSatisAnalizi>());
            var liste = new Charts(db, vUser.SirketKodu).ChartGunlukSatisAnalizi(kod, islemtip, tarih);
            return PartialView("Satis/GunlukSatisAnaliziDoubleKriterPie", liste);
        }

        public PartialViewResult GunlukSatisPie(int tarih)
        {
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("Satis/GunlukSatisPie", new List<ChartGunlukSatisAnalizi>());
            var liste = new Charts(db, vUser.SirketKodu).ChartGunlukSatisAnalizi(tarih);
            return PartialView("Satis/GunlukSatisPie", liste);
        }

        public PartialViewResult GunlukSatisYearToDay()
        {
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("Satis/GunlukSatısAnaliziYearToDay", new List<DB_GunlukSatisAnaliziYearToDay>());
            var liste = dbl.DB_GunlukSatisAnaliziYearToDay.Where(m => m.DB == vUser.SirketKodu).ToList();
            if (liste.Count == 0)
                liste = new Charts(db, vUser.SirketKodu).ChartYear2Day();
            return PartialView("Satis/GunlukSatısAnaliziYearToDay", liste);
        }

        public PartialViewResult GunlukSatisYearToDayPie()
        {
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("PartialGunlukSatisYearToDayPie", new List<DB_GunlukSatisAnaliziYearToDay>());
            var liste = dbl.DB_GunlukSatisAnaliziYearToDay.Where(m => m.DB == vUser.SirketKodu).ToList();
            if (liste.Count == 0)
                liste = new Charts(db, vUser.SirketKodu).ChartYear2Day();
            return PartialView("Satis/GunlukSatısAnaliziYearToDayPie", liste);
        }

        public PartialViewResult GunlukSatisZamanCizelgesi()
        {
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("Satis/GunlukSatisZamanCizelgesi", new List<ChartBaglantiZaman>());
            var liste = new Charts(db, vUser.SirketKodu).ChartGunlukZaman();
            return PartialView("Satis/GunlukSatisZamanCizelgesi", liste);
        }

        public PartialViewResult LokasyonSatis(int tarih)
        {
            ViewBag.Tarih = tarih;
            if (CheckPerm(Perms.ChartLokasyonSatis, PermTypes.Reading) == false) return PartialView("Satis/LokasyonSatis", new List<DB_LokasyonBazli_SatisAnalizi>());
            var liste = dbl.DB_LokasyonBazli_SatisAnalizi.Where(m => m.DB == vUser.SirketKodu && m.Ay == tarih).ToList();
            if (liste.Count == 0)
                liste = new Charts(db, vUser.SirketKodu).ChartLocation(tarih);

            return PartialView("Satis/LokasyonSatis", liste);
        }

        public PartialViewResult LokasyonSatisKriter(int tarih, string kriter)
        {
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kriter;
            if (CheckPerm(Perms.ChartLokasyonSatis, PermTypes.Reading) == false) return PartialView("Satis/LokasyonSatisKriter", new List<DB_LokasyonBazli_SatisAnalizi_Kriter>());
            var liste = dbl.DB_LokasyonBazli_SatisAnalizi_Kriter.Where(m => m.DB == vUser.SirketKodu && m.Ay == tarih && m.GrupKod == kriter).ToList();
            if (liste.Count == 0)
                liste = new Charts(db, vUser.SirketKodu).ChartLocationKriter(tarih, kriter);

            return PartialView("Satis/LokasyonSatisKriter", liste);
        }

        public PartialViewResult SatisTemsilcisiAylikSatisAnalizi(string kod, int tarih)
        {
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kod;
            if (CheckPerm(Perms.ChartSatisTemsilcisiAylikSatisAnalizi, PermTypes.Reading) == false) return PartialView("Satis/SatisTemsilcisi_AylikSatisAnalizi", new List<ChartSatisTemsilcisiAylikSatisAnalizi>());
            var liste = new Charts(db, vUser.SirketKodu).SatisTemsilcisiAylikSatisAnalizi(tarih, kod);
            return PartialView("Satis/SatisTemsilcisi_AylikSatisAnalizi", liste);
        }

        public PartialViewResult UrunGrubuSatis(int tarih)
        {
            ViewBag.Tarih = tarih;
            if (CheckPerm(Perms.ChartUrunGrubuSatis, PermTypes.Reading) == false) return PartialView("Satis/UrunGrubuSatis", new List<DB_UrunGrubu_SatisAnalizi>());
            var liste = dbl.DB_UrunGrubu_SatisAnalizi.Where(m => m.DB == vUser.SirketKodu && m.Ay == tarih).ToList();
            if (liste.Count == 0)
                liste = new Charts(db, vUser.SirketKodu).ChartUrunGrubu(tarih);
            return PartialView("Satis/UrunGrubuSatis", liste);
        }

        public PartialViewResult UrunGrubuSatisKriter(int tarih, string kriter)
        {
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kriter;
            if (CheckPerm(Perms.ChartUrunGrubuSatis, PermTypes.Reading) == false) return PartialView("Satis/UrunGrubuSatisKriter", new List<DB_UrunGrubu_SatisAnalizi_Kriter>());
            var liste = dbl.DB_UrunGrubu_SatisAnalizi_Kriter.Where(m => m.DB == vUser.SirketKodu && m.Ay == tarih && m.GrupKod == kriter).ToList();
            if (liste.Count == 0)
                liste = new Charts(db, vUser.SirketKodu).ChartUrunGrubuKriter(tarih, kriter);
            return PartialView("Satis/UrunGrubuSatisKriter", liste);
        }

        public string BolgeBazliSatisAnaliziKriter()
        {
            var liste = new Charts(db, vUser.SirketKodu).BolgeBazliSatisAnaliziKriter();
            return new JavaScriptSerializer().Serialize(liste);
        }

        public string CHKSelect()
        {
            var liste = new Charts(db, vUser.SirketKodu).ChkSelect();
            return new JavaScriptSerializer().Serialize(liste);
        }

        public string Connection()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString;
        }

        public string GunlukSatisAnaliziKriterSelect()
        {
            var liste = new Charts(db, vUser.SirketKodu).GunlukSatisAnaliziKriterSelect();
            return new JavaScriptSerializer().Serialize(liste);
        }
    }
}