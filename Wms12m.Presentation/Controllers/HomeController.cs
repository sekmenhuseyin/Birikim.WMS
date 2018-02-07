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
            }
            // Bekleyen Onaylar
            if (setts.OnayCek == true || setts.OnayFiyat == true || setts.OnayRisk == true || setts.OnaySiparis == true || setts.OnaySozlesme == true || setts.OnayStok == true || setts.OnayTekno == true || setts.OnayTeminat == true)
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
            var tekrarlayan = db.Etkinliks.Where(m => m.Tekrarlayan == true && (m.TatilTipi == 76 || m.TatilTipi == 78) && m.Tarih.Day == DateTime.Today.Day && m.Tarih.Month == DateTime.Today.Month).ToList();
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
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            ViewBag.Doviz = doviz;
            if (tbl.yetki.Contains("'ChartGunlukSatis'") == true)
            {
                ViewBag.ChartGunlukSatis = theCharts.ChartGunlukSatisAnalizi(tarih);
                ViewBag.ChartGunlukZaman = theCharts.ChartBaglantiZaman();
                ViewBag.ChartYear2Day = theCharts.ChartYear2Day();
                ViewBag.ChartGunlukSatisKriter = theCharts.ChartGunlukSatisAnalizi(kod, islemtip, tarih);
            }
            if (tbl.yetki.Contains("'ChartAylikSatisAnaliziBar'") == true)
            {
                ViewBag.ChartAylikSatis = dbl.DB_Aylik_SatisAnalizi.Where(m => m.DB == vUser.SirketKodu).ToList();
                ViewBag.ChartAylikSatisKodTipDoviz = dbl.DB_Aylik_SatisAnalizi_Tip_Kod_Doviz.Where(m => m.DB == vUser.SirketKodu && m.Grup == kod && m.Kriter == doviz && m.IslemTip == islemtip).ToList();
            }
            if (tbl.yetki.Contains("'ChartUrunGrubuSatis'") == true)
            {
                ViewBag.Kriter = doviz;
                ViewBag.ChartUrunGrubuSatis = dbl.DB_UrunGrubu_SatisAnalizi.Where(m => m.DB == vUser.SirketKodu && m.Ay == 0).ToList();
                ViewBag.ChartUrunGrubuSatisKriter = dbl.DB_UrunGrubu_SatisAnalizi_Kriter.Where(m => m.DB == vUser.SirketKodu && m.Ay == tarih && m.GrupKod == doviz).ToList();
            }
            if (tbl.yetki.Contains("'ChartLokasyonSatis'") == true)
            {
                ViewBag.Kriter = doviz;
                ViewBag.ChartLokasyonSatis = dbl.DB_LokasyonBazli_SatisAnalizi.Where(m => m.DB == vUser.SirketKodu && m.Ay == tarih).ToList();
                ViewBag.ChartLokasyonSatisKriter = dbl.DB_LokasyonBazli_SatisAnalizi_Kriter.Where(m => m.DB == vUser.SirketKodu && m.Ay == tarih && m.GrupKod == doviz).ToList();
            }
            if (tbl.yetki.Contains("'ChartBakiyeRiskAnalizi'") == true)
            {
                ViewBag.ChartBakiyeRisk = dbl.DB_BakiyeRiskAnalizi.Where(m => m.DB == vUser.SirketKodu).ToList();
            }
            if (tbl.yetki.Contains("'ChartBaglantiUrunGrubu'") == true)
            {
                ViewBag.ChartBaglantiUrunGrubu = dbl.DB_SatisBaglanti_UrunGrubu.Where(m => m.DB == vUser.SirketKodu).ToList();
            }
            if (tbl.yetki.Contains("'ChartGunlukMDFUretimi'") == true)
            {
                ViewBag.ChartGunlukMDFUretimi = new List<ChartGunlukMDFUretimi>();
            }
            if (tbl.yetki.Contains("'ChartBaglantiZamanCizelgesi'") == true)
            {
                ViewBag.ChartBaglantiZamanCizelgesi = new List<ChartBaglantiZaman>();
            }
            if (tbl.yetki.Contains("'ChartBolgeBazliSatisAnalizi'") == true)
            {
                ViewBag.ChartBolgeBazliSatis = new List<ChartBolgeBazliSatisAnalizi>();
            }
            if (tbl.yetki.Contains("'ChartBekleyenSiparisUrunGrubu'") == true)
            {
                ViewBag.ChartBekleyenSiparisUrunGrubu = dbl.DB_SatisBaglanti_UrunGrubu.Where(m => m.DB == vUser.SirketKodu).ToList();
                ViewBag.ChartBekleyenSiparisUrunGrubuMiktar = dbl.DB_SatisBaglanti_UrunGrubu.Where(m => m.DB == vUser.SirketKodu).ToList();
                ViewBag.ChartBekleyenSiparisUrunGrubuMiktarKriter = dbl.DB_SatisBaglanti_UrunGrubu.Where(m => m.DB == vUser.SirketKodu).ToList();
                ViewBag.ChartBekleyenSiparisMusteri = dbl.DB_SatisBaglanti_UrunGrubu.Where(m => m.DB == vUser.SirketKodu).ToList();
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
            var satir = db.Messages.Where(m => m.ID == ID).FirstOrDefault();
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
            if (Onay == true)
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

        public PartialViewResult PartialAylikSatisAnaliziBar()
        {
            if (CheckPerm(Perms.ChartAylikSatisAnaliziBar, PermTypes.Reading) == false) return PartialView("Satis/AylikSatisAnaliziBar", new List<DB_Aylik_SatisAnalizi>());
            var liste = dbl.DB_Aylik_SatisAnalizi.Where(m => m.DB == vUser.SirketKodu).ToList();
            if (liste.Count == 0)
                try
                {
                    var theCharts = new Charts(db, vUser.SirketKodu);
                    liste = theCharts.ChartMonthly();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/PartialAylikSatisAnaliziBar");
                    liste = new List<DB_Aylik_SatisAnalizi>();
                }

            return PartialView("Satis/AylikSatisAnaliziBar", liste);
        }

        public PartialViewResult PartialAylikSatisAnaliziKodTipDovizBar(string kod, int islemtip, string doviz)
        {
            ViewBag.Doviz = doviz;
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            if (CheckPerm(Perms.ChartAylikSatisAnaliziBar, PermTypes.Reading) == false) return PartialView("Satis/AylikSatisAnaliziKodTipDovizBar", new List<DB_Aylik_SatisAnalizi_Tip_Kod_Doviz>());
            var liste = dbl.DB_Aylik_SatisAnalizi_Tip_Kod_Doviz.Where(m => m.DB == vUser.SirketKodu && m.Grup == kod && m.Kriter == doviz && m.IslemTip == islemtip).ToList();
            try
            {
                var theCharts = new Charts(db, vUser.SirketKodu);
                liste = theCharts.ChartMonthlyByKriter(kod, islemtip, doviz);
            }
            catch (Exception ex)
            {
                Logger(ex, "Home/PartialAylikSatisAnaliziKodTipDovizBar");
            }

            return PartialView("Satis/AylikSatisAnaliziKodTipDovizBar", liste);
        }

        public PartialViewResult PartialAylikSatisCHKAnaliziBar(string chk)
        {
            ViewBag.CHK = chk;
            if (CheckPerm(Perms.ChartAylikSatisAnaliziBar, PermTypes.Reading) == false) return PartialView("Satis/AylikSatisCHKAnaliziBar", new List<ChartAylikSatisAnalizi>());
            List<ChartAylikSatisAnalizi> liste;
            if (chk == "")
            {
                liste = new List<ChartAylikSatisAnalizi>
                {
                    new ChartAylikSatisAnalizi() { Ay = "0", Yil2015 = 0, Yil2016 = 0, Yil2017 = 0 }
                };
            }
            else
                try
                {
                    var theCharts = new Charts(db, vUser.SirketKodu);
                    liste = theCharts.ChartAylikSatisAnalizi(chk);
                }
                catch (Exception)
                {
                    liste = new List<ChartAylikSatisAnalizi>
                    {
                        new ChartAylikSatisAnalizi() { Ay = "0", Yil2015 = 0, Yil2016 = 0, Yil2017 = 0 }
                    };
                }

            return PartialView("Satis/AylikSatisCHKAnaliziBar", liste);
        }

        public PartialViewResult PartialBaglantiUrunGrubu()
        {
            if (CheckPerm(Perms.ChartBaglantiUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BaglantiUrunGrubu", new List<DB_SatisBaglanti_UrunGrubu>());
            var liste = dbl.DB_SatisBaglanti_UrunGrubu.Where(m => m.DB == vUser.SirketKodu).ToList();
            if (liste.Count == 0)
                try
                {
                    liste = db.Database.SqlQuery<DB_SatisBaglanti_UrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_SatisBaglanti_UrunGrubu]", vUser.SirketKodu)).ToList();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/PartialBaglantiUrunGrubu");
                    liste = new List<DB_SatisBaglanti_UrunGrubu>();
                }

            return PartialView("Satis/BaglantiUrunGrubu", liste);
        }

        public PartialViewResult PartialBaglantiUrunGrubuPie()
        {
            if (CheckPerm(Perms.ChartBaglantiUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BaglantiUrunGrubuPie", new List<DB_SatisBaglanti_UrunGrubu>());
            var liste = dbl.DB_SatisBaglanti_UrunGrubu.Where(m => m.DB == vUser.SirketKodu).ToList();
            if (liste.Count == 0)
                try
                {
                    liste = db.Database.SqlQuery<DB_SatisBaglanti_UrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_SatisBaglanti_UrunGrubu]", vUser.SirketKodu)).ToList();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/PartialBaglantiUrunGrubuPie");
                    liste = new List<DB_SatisBaglanti_UrunGrubu>();
                }

            return PartialView("Satis/BaglantiUrunGrubuPie", liste);
        }

        public PartialViewResult PartialBaglantiZamanCizelgesi()
        {
            if (CheckPerm(Perms.ChartBaglantiZamanCizelgesi, PermTypes.Reading) == false) return PartialView("Satis/BaglantiZamanCizelgesi", new List<ChartBaglantiZaman>());
            List<ChartBaglantiZaman> liste;
            try
            {
                liste = db.Database.SqlQuery<ChartBaglantiZaman>(string.Format("[FINSAT6{0}].[wms].[DB_BaglantiLogGetir]", vUser.SirketKodu)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "Home/PartialBaglantiZamanCizelgesi");
                liste = new List<ChartBaglantiZaman>();
            }

            return PartialView("Satis/BaglantiZamanCizelgesi", liste);
        }

        public PartialViewResult PartialBakiyeRiskAnalizi()
        {
            if (CheckPerm(Perms.ChartBakiyeRiskAnalizi, PermTypes.Reading) == false) return PartialView("Satis/BakiyeRiskAnalizi", new List<DB_BakiyeRiskAnalizi>());
            var liste = dbl.DB_BakiyeRiskAnalizi.Where(m => m.DB == vUser.SirketKodu).ToList();
            if (liste.Count == 0)
                try
                {
                    liste = db.Database.SqlQuery<DB_BakiyeRiskAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_BakiyeRiskAnalizi]", vUser.SirketKodu)).ToList();
                }
                catch (Exception)
                {
                }

            return PartialView("Satis/BakiyeRiskAnalizi", liste);
        }

        public PartialViewResult PartialBekleyenSiparisMusteriAnalizi(string kod, string doviz)
        {
            ViewBag.Doviz = doviz;
            ViewBag.Kriter = kod;
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BekleyenSiparisMusteriAnalizi", new List<ChartBekleyenSiparisUrunGrubu>());
            List<ChartBekleyenSiparisUrunGrubu> liste;
            try
            {
                liste = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_Musteri_Analizi] @Kod = '{1}', @Kriter = '{2}'", vUser.SirketKodu, kod, doviz)).ToList();
            }
            catch (Exception)
            {
                liste = new List<ChartBekleyenSiparisUrunGrubu>();
            }

            return PartialView("Satis/BekleyenSiparisMusteriAnalizi", liste);
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubu(int bastarih, int bittarih)
        {
            ViewBag.BasTarih = bastarih;
            ViewBag.BasTarih2 = bastarih.FromOADateInt();
            ViewBag.BitTarih = bittarih;
            ViewBag.BitTarih2 = bittarih.FromOADateInt();
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BekleyenSiparisUrunGrubu", new List<ChartBekleyenSiparisUrunGrubu>());
            List<ChartBekleyenSiparisUrunGrubu> BSUG;
            try
            {
                BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu] @BasTarih = {1}, @BitTarih = {2}", vUser.SirketKodu, bastarih, bittarih)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "Home/PartialBekleyenSiparisUrunGrubu");
                BSUG = new List<ChartBekleyenSiparisUrunGrubu>();
            }

            return PartialView("Satis/BekleyenSiparisUrunGrubu", BSUG);
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktar(bool miktarTutar)
        {
            ViewBag.MiktarTutar = "Miktar";
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BekleyenSiparisUrunGrubuMiktar", new List<CachedChartBekleyenUrunMiktarFiyat_Result>());
            List<CachedChartBekleyenUrunMiktarFiyat_Result> BSUG;
            if (miktarTutar == true)
            {
                BSUG = db.CachedChartBekleyenUrunMiktarFiyat(vUser.SirketKodu, true).ToList();
                ViewBag.MiktarTutar = "Miktar";
                if (BSUG.Count == 0)
                    try
                    {
                        BSUG = db.Database.SqlQuery<CachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Miktar]", vUser.SirketKodu)).ToList();
                    }
                    catch (Exception)
                    {
                        BSUG = new List<CachedChartBekleyenUrunMiktarFiyat_Result>();
                    }
            }
            else
            {
                BSUG = db.CachedChartBekleyenUrunMiktarFiyat(vUser.SirketKodu, false).ToList();
                ViewBag.MiktarTutar = "Tutar";
                if (BSUG.Count == 0)
                    try
                    {
                        BSUG = db.Database.SqlQuery<CachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat]", vUser.SirketKodu)).ToList();
                    }
                    catch (Exception)
                    {
                        BSUG = new List<CachedChartBekleyenUrunMiktarFiyat_Result>();
                    }
            }

            return PartialView("Satis/BekleyenSiparisUrunGrubuMiktar", BSUG);
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktarKriter(string kriter)
        {
            ViewBag.MiktarTutar = "Tutar";
            ViewBag.Kriter = kriter;
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BekleyenSiparisUrunGrubuMiktarKriter", new List<CachedChartBekleyenUrunMiktarFiyat_Result>());
            List<CachedChartBekleyenUrunMiktarFiyat_Result> BSUG;
            try
            {
                BSUG = db.Database.SqlQuery<CachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat_Kriter] @Kriter='{1}'", vUser.SirketKodu, kriter)).ToList();
            }
            catch (Exception)
            {
                BSUG = new List<CachedChartBekleyenUrunMiktarFiyat_Result>();
            }

            return PartialView("Satis/BekleyenSiparisUrunGrubuMiktarKriter", BSUG);
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktarKriterPie(string kriter)
        {
            ViewBag.MiktarTutar = "Tutar";
            ViewBag.Kriter = kriter;
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BekleyenSiparisUrunGrubuMiktarKriterPie", new List<CachedChartBekleyenUrunMiktarFiyat_Result>());
            List<CachedChartBekleyenUrunMiktarFiyat_Result> BSUG;
            try
            {
                BSUG = db.Database.SqlQuery<CachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat_Kriter] @Kriter='{1}'", vUser.SirketKodu, kriter)).ToList();
            }
            catch (Exception)
            {
                BSUG = new List<CachedChartBekleyenUrunMiktarFiyat_Result>();
            }

            return PartialView("Satis/BekleyenSiparisUrunGrubuMiktarKriterPie", BSUG);
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktarPie(bool miktarTutar)
        {
            ViewBag.MiktarTutar = "Miktar";
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BekleyenSiparisUrunGrubuMiktarPie", new List<CachedChartBekleyenUrunMiktarFiyat_Result>());
            List<CachedChartBekleyenUrunMiktarFiyat_Result> BSUG;
            if (miktarTutar == true)
            {
                BSUG = db.CachedChartBekleyenUrunMiktarFiyat(vUser.SirketKodu, true).ToList();
                if (BSUG.Count == 0)
                    try
                    {
                        BSUG = db.Database.SqlQuery<CachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Miktar]", vUser.SirketKodu)).ToList();
                    }
                    catch (Exception)
                    {
                        BSUG = new List<CachedChartBekleyenUrunMiktarFiyat_Result>();
                    }

                ViewBag.MiktarTutar = "Miktar";
            }
            else
            {
                BSUG = db.CachedChartBekleyenUrunMiktarFiyat(vUser.SirketKodu, false).ToList();
                if (BSUG.Count == 0)
                    try
                    {
                        BSUG = db.Database.SqlQuery<CachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat]", vUser.SirketKodu)).ToList();
                    }
                    catch (Exception)
                    {
                        BSUG = new List<CachedChartBekleyenUrunMiktarFiyat_Result>();
                    }

                ViewBag.MiktarTutar = "Tutar";
            }

            return PartialView("Satis/BekleyenSiparisUrunGrubuMiktarPie", BSUG);
        }

        public PartialViewResult PartialBolgeBazliSatisAnalizi(int ay, string kriter)
        {
            ViewBag.Ay = ay;
            ViewBag.Kriter = kriter;
            if (CheckPerm(Perms.ChartBolgeBazliSatisAnalizi, PermTypes.Reading) == false) return PartialView("Satis/BolgeBazliSatisAnalizi", new List<ChartBolgeBazliSatisAnalizi>());
            List<ChartBolgeBazliSatisAnalizi> liste;
            try
            {
                liste = db.Database.SqlQuery<ChartBolgeBazliSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Bolge_Bazinda_SatisAnalizi] @Ay = {1}, @Kriter = '{2}'", vUser.SirketKodu, ay, kriter)).ToList();
            }
            catch (Exception)
            {
                liste = new List<ChartBolgeBazliSatisAnalizi>();
            }

            return PartialView("Satis/BolgeBazliSatisAnalizi", liste);
        }

        public PartialViewResult PartialBolgeBazliSatisAnaliziPie(int ay, string kriter)
        {
            ViewBag.Ay = ay;
            ViewBag.Kriter = kriter;
            if (CheckPerm(Perms.ChartBolgeBazliSatisAnalizi, PermTypes.Reading) == false) return PartialView("Satis/BolgeBazliSatisAnaliziPie", new List<ChartBolgeBazliSatisAnalizi>());
            List<ChartBolgeBazliSatisAnalizi> liste;
            try
            {
                liste = db.Database.SqlQuery<ChartBolgeBazliSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Bolge_Bazinda_SatisAnalizi] @Ay = {1}, @Kriter = '{2}'", vUser.SirketKodu, ay, kriter)).ToList();
            }
            catch (Exception)
            {
                liste = new List<ChartBolgeBazliSatisAnalizi>();
            }

            return PartialView("Satis/BolgeBazliSatisAnaliziPie", liste);
        }

        public PartialViewResult PartialGunlukMDFUretimi(int tarih)
        {
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            if (CheckPerm(Perms.ChartGunlukMDFUretimi, PermTypes.Reading) == false) return PartialView("Satis/GunlukMDFUretim", new List<ChartGunlukMDFUretimi>());
            List<ChartGunlukMDFUretimi> liste;
            try
            {
                liste = db.Database.SqlQuery<ChartGunlukMDFUretimi>(string.Format("[FINSAT6{0}].[wms].[MDF_UretimRapor_Chart] @BasTarih = {1}, @BitTarih = {2}, @Tip={3}", vUser.SirketKodu, tarih, tarih, 1)).ToList();
            }
            catch (Exception)
            {
                liste = new List<ChartGunlukMDFUretimi>();
            }

            return PartialView("Satis/GunlukMDFUretim", liste);
        }

        public PartialViewResult PartialGunlukMDFUretimiPie(int tarih)
        {
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            if (CheckPerm(Perms.ChartGunlukMDFUretimi, PermTypes.Reading) == false) return PartialView("Satis/GunlukMDFUretimPie", new List<ChartGunlukMDFUretimi>());
            List<ChartGunlukMDFUretimi> GSA;
            try
            {
                GSA = db.Database.SqlQuery<ChartGunlukMDFUretimi>(string.Format("[FINSAT6{0}].[wms].[MDF_UretimRapor_Chart] @BasTarih = {1}, @BitTarih = {2}, @Tip={3}", vUser.SirketKodu, tarih, tarih, 1)).ToList();
            }
            catch (Exception)
            {
                GSA = new List<ChartGunlukMDFUretimi>();
            }

            return PartialView("Satis/GunlukMDFUretimPie", GSA);
        }

        public PartialViewResult PartialGunlukSatis(int tarih)
        {
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("Satis/GunlukSatis", new List<ChartGunlukSatisAnalizi>());
            List<ChartGunlukSatisAnalizi> liste;
            try
            {
                var theCharts = new Charts(db, vUser.SirketKodu);
                liste = theCharts.ChartGunlukSatisAnalizi(tarih);
            }
            catch (Exception)
            {
                liste = new List<ChartGunlukSatisAnalizi>();
            }

            return PartialView("Satis/GunlukSatis", liste);
        }

        public PartialViewResult PartialGunlukSatisDoubleKriter(string kod, int islemtip, int tarih)
        {
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("Satis/GunlukSatisAnaliziDoubleKriter", new List<ChartGunlukSatisAnalizi>());
            List<ChartGunlukSatisAnalizi> liste;
            try
            {
                var theCharts = new Charts(db, vUser.SirketKodu);
                liste = theCharts.ChartGunlukSatisAnalizi(kod, islemtip, tarih);
            }
            catch (Exception)
            {
                liste = new List<ChartGunlukSatisAnalizi>();
            }

            return PartialView("Satis/GunlukSatisAnaliziDoubleKriter", liste);
        }

        public PartialViewResult PartialGunlukSatisDoubleKriterPie(string kod, int islemtip, int tarih)
        {
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("Satis/GunlukSatisAnaliziDoubleKriterPie", new List<ChartGunlukSatisAnalizi>());
            List<ChartGunlukSatisAnalizi> liste;
            try
            {
                var theCharts = new Charts(db, vUser.SirketKodu);
                liste = theCharts.ChartGunlukSatisAnalizi(kod, islemtip, tarih);
            }
            catch (Exception)
            {
                liste = new List<ChartGunlukSatisAnalizi>();
            }

            return PartialView("Satis/GunlukSatisAnaliziDoubleKriterPie", liste);
        }

        public PartialViewResult PartialGunlukSatisPie(int tarih)
        {
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("Satis/GunlukSatisPie", new List<ChartGunlukSatisAnalizi>());
            List<ChartGunlukSatisAnalizi> liste;
            try
            {
                var theCharts = new Charts(db, vUser.SirketKodu);
                liste = theCharts.ChartGunlukSatisAnalizi(tarih);
            }
            catch (Exception)
            {
                liste = new List<ChartGunlukSatisAnalizi>();
            }

            return PartialView("Satis/GunlukSatisPie", liste);
        }

        public PartialViewResult PartialGunlukSatisYearToDay()
        {
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("Satis/GunlukSatısAnaliziYearToDay", new List<DB_GunlukSatisAnaliziYearToDay>());
            var liste = dbl.DB_GunlukSatisAnaliziYearToDay.Where(m => m.DB == vUser.SirketKodu).ToList();
            if (liste.Count == 0)
                try
                {
                    var theCharts = new Charts(db, vUser.SirketKodu);
                    liste = theCharts.ChartYear2Day();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/ChartGunlukSatisYearToDay");
                    liste = new List<DB_GunlukSatisAnaliziYearToDay>();
                }

            return PartialView("Satis/GunlukSatısAnaliziYearToDay", liste);
        }

        public PartialViewResult PartialGunlukSatisYearToDayPie()
        {
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("PartialGunlukSatisYearToDayPie", new List<DB_GunlukSatisAnaliziYearToDay>());
            var liste = dbl.DB_GunlukSatisAnaliziYearToDay.Where(m => m.DB == vUser.SirketKodu).ToList();
            if (liste.Count == 0)
                try
                {
                    var theCharts = new Charts(db, vUser.SirketKodu);
                    liste = theCharts.ChartYear2Day();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/PartialGunlukSatisYearToDayPie");
                    liste = new List<DB_GunlukSatisAnaliziYearToDay>();
                }

            return PartialView("Satis/GunlukSatısAnaliziYearToDayPie", liste);
        }

        public PartialViewResult PartialGunlukSatisZamanCizelgesi()
        {
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("Satis/GunlukSatisZamanCizelgesi", new List<ChartBaglantiZaman>());
            List<ChartBaglantiZaman> liste;
            try
            {
                var theCharts = new Charts(db, vUser.SirketKodu);
                liste = theCharts.ChartBaglantiZaman();
            }
            catch (Exception ex)
            {
                Logger(ex, "Home/PartialGunlukSatisZamanCizelgesi");
                liste = new List<ChartBaglantiZaman>();
            }

            return PartialView("Satis/GunlukSatisZamanCizelgesi", liste);
        }

        public PartialViewResult PartialLokasyonSatis(int tarih)
        {
            ViewBag.Tarih = tarih;
            if (CheckPerm(Perms.ChartLokasyonSatis, PermTypes.Reading) == false) return PartialView("Satis/LokasyonSatis", new List<DB_LokasyonBazli_SatisAnalizi>());
            var liste = dbl.DB_LokasyonBazli_SatisAnalizi.Where(m => m.DB == vUser.SirketKodu && m.Ay == tarih).ToList();
            if (liste.Count == 0)
                try
                {
                    var theCharts = new Charts(db, vUser.SirketKodu);
                    liste = theCharts.ChartLocation(tarih);
                }
                catch (Exception)
                {
                    liste = new List<DB_LokasyonBazli_SatisAnalizi>();
                }

            return PartialView("Satis/LokasyonSatis", liste);
        }

        public PartialViewResult PartialLokasyonSatisKriter(int tarih, string kriter)
        {
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kriter;
            if (CheckPerm(Perms.ChartLokasyonSatis, PermTypes.Reading) == false) return PartialView("Satis/LokasyonSatisKriter", new List<DB_LokasyonBazli_SatisAnalizi_Kriter>());
            var liste = dbl.DB_LokasyonBazli_SatisAnalizi_Kriter.Where(m => m.DB == vUser.SirketKodu && m.Ay == tarih && m.GrupKod == kriter).ToList();
            if (liste.Count == 0)
                try
                {
                    var theCharts = new Charts(db, vUser.SirketKodu);
                    liste = theCharts.ChartLocationKriter(tarih, kriter);
                }
                catch (Exception)
                {
                    liste = new List<DB_LokasyonBazli_SatisAnalizi_Kriter>();
                }

            return PartialView("Satis/LokasyonSatisKriter", liste);
        }

        public PartialViewResult PartialSatisTemsilcisiAylikSatisAnalizi(string kod, short tarih)
        {
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kod;
            if (CheckPerm(Perms.ChartSatisTemsilcisiAylikSatisAnalizi, PermTypes.Reading) == false) return PartialView("Satis/SatisTemsilcisi_AylikSatisAnalizi", new List<ChartSatisTemsilcisiAylikSatisAnalizi>());
            List<ChartSatisTemsilcisiAylikSatisAnalizi> list;
            try
            {
                list = db.Database.SqlQuery<ChartSatisTemsilcisiAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[SatisTemsilcisi_AylikSatisAnalizi] @Ay = '{1}', @Kriter = '{2}'", vUser.SirketKodu, tarih, kod)).ToList();
            }
            catch (Exception)
            {
                list = new List<ChartSatisTemsilcisiAylikSatisAnalizi>();
            }

            return PartialView("Satis/SatisTemsilcisi_AylikSatisAnalizi", list);
        }

        public PartialViewResult PartialUrunGrubuSatis(int tarih)
        {
            ViewBag.Tarih = tarih;
            if (CheckPerm(Perms.ChartUrunGrubuSatis, PermTypes.Reading) == false) return PartialView("Satis/UrunGrubuSatis", new List<DB_UrunGrubu_SatisAnalizi>());
            var liste = dbl.DB_UrunGrubu_SatisAnalizi.Where(m => m.DB == vUser.SirketKodu && m.Ay == tarih).ToList();
            if (liste.Count == 0)
                try
                {
                    var theCharts = new Charts(db, vUser.SirketKodu);
                    liste = theCharts.ChartUrunGrubu(tarih);
                }
                catch (Exception)
                {
                    liste = new List<DB_UrunGrubu_SatisAnalizi>();
                }

            return PartialView("Satis/UrunGrubuSatis", liste);
        }

        public PartialViewResult PartialUrunGrubuSatisKriter(int tarih, string kriter)
        {
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kriter;
            if (CheckPerm(Perms.ChartUrunGrubuSatis, PermTypes.Reading) == false) return PartialView("Satis/UrunGrubuSatisKriter", new List<DB_UrunGrubu_SatisAnalizi_Kriter>());
            var liste = dbl.DB_UrunGrubu_SatisAnalizi_Kriter.Where(m => m.DB == vUser.SirketKodu && m.Ay == tarih && m.GrupKod == kriter).ToList();
            if (liste.Count == 0)
                try
                {
                    var theCharts = new Charts(db, vUser.SirketKodu);
                    liste = theCharts.ChartUrunGrubuKriter(tarih, kriter);
                }
                catch (Exception)
                {
                    liste = new List<DB_UrunGrubu_SatisAnalizi_Kriter>();
                }

            return PartialView("Satis/UrunGrubuSatisKriter", liste);
        }

        public string BolgeBazliSatisAnaliziKriter()
        {
            var Kriter = db.Database.SqlQuery<ChartBolgeBazliSatisAnaliziKriter>(string.Format("[FINSAT6{0}].[wms].[BolgeBazliSatisAnaliziKriterSelect]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(Kriter);
            return json;
        }

        public string CHKSelect()
        {
            var CHK = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[wms].[CHKSelectKartTip]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(CHK);
            return json;
        }

        public string Connection()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString;
        }

        public string GunlukSatisAnaliziKriterSelect()
        {
            var Kriter = db.Database.SqlQuery<ChartBolgeBazliSatisAnaliziKriter>(string.Format("[FINSAT6{0}].[wms].[GunlukSatisAnaliziKriterSelect]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(Kriter);
            return json;
        }
    }
}