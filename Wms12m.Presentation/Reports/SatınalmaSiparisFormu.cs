using KurumsalKaynakPlanlaması12M;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Presentation.Reports;

namespace Wms12m
{
    public static class SatınalmaSiparisFormu
    {
        public static void SatinalmaSiparisFormu(string sipEvrakNo, string hesapKodu, int siparisTarih, bool pdfKaydet, string sirketKodu)
        {
            if (string.IsNullOrEmpty(sipEvrakNo) || string.IsNullOrEmpty(hesapKodu)) return;

            WMSEntities db = new WMSEntities();
            var dset = new DsSatinalma();
            var dvzCinsi = "TL";
            short islemTip = 1;
            var chk = new KKP_CHK();
            var talep = new SatTalep();
            var hata = false;
            var say = 1;
            DateTime? sipTarih = null;
            var fDD = new SatinalmaRaporData()
            {
                FTDD = new List<RaporFTD>(),
                CHKK = new List<KKP_CHK>(),
                SPII = new List<RaporSiparis>(),
                TLP = new List<SatTalep>()
            };
            decimal ftdMalBedeli = 0, ftdKDV = 0, ftdToplam = 0;
            var rPR = db.MultipleResults(string.Format("[FINSAT6{0}].[wms].[Satinalma_RaporDetay] @EvrakNo='{1}',@HesapKodu='{2}',@Siptarih={3}", sirketKodu, sipEvrakNo, hesapKodu, siparisTarih)).With<RaporFTD>().With<RaporSiparis>().With<KKP_CHK>().With<SatTalep>().Execute();
            foreach (RaporFTD item in rPR[0])
            {
                fDD.FTDD.Add(item);
                if (item.DovizCinsi.ToString().Trim() != "")
                    dvzCinsi = item.DovizCinsi.ToString().Trim();

                var satirTip = (short)item.SatirTip;
                var tutar = (decimal)item.Tutar;

                if (satirTip == 0) ftdMalBedeli = tutar;
                else if (satirTip == 9) ftdKDV = tutar;
                else if (satirTip == 12) ftdToplam = tutar;
            }

            foreach (RaporSiparis item in rPR[1])
            {
                fDD.SPII.Add(item);
                sipTarih = new DateTime(1900, 1, 1).AddDays((int)item.Tarih - 2);
                islemTip = (short)item.IslemTip;

                var row = dset.Siparis.NewSiparisRow();
                row.SatirNo = say;
                row.Malzeme = item.MalKodu.ToString();
                row.DvzCinsi = item.DovizCinsi.ToString() == "" ? "TL" : item.DovizCinsi.ToString();
                row.MalzemeAdi = item.MalAdi.ToString().Trim();
                if (item.EkDosya != null && item.EkDosya.ToString().Trim() != "")
                    row.MalzemeAdi += string.Format(" ({0}: {1})"
                        , islemTip == (short)KKPIslemTipSPI.İçPiyasa ? "Ek Dosya" : "File"
                        , item.EkDosya);

                row.Aciklama = item.Aciklama.ToString();

                row.OnaylananTarih = new DateTime(1900, 1, 1).AddDays((int)item.Tarih - 2).ToString("dd.MM.yyyy");
                row.Miktar = (decimal)item.BirimMiktar;
                row.Fiyat = (decimal)item.BirimFiyat;
                row.Tutar = (decimal)item.Tutar;
                if ((short)item.DvzTL == 1)
                    row.Tutar = (decimal)item.DovizTutar;
                row.Birim = item.Birim.ToString();

                row.Vade = item.Vade;
                row.TeslimYeri = item.TeslimYeri != null ? item.TeslimYeri.ToString() : "";

                row.FTDMalBedeli = ftdMalBedeli;
                row.FTDKDV = ftdKDV;
                row.FTDTutar = ftdToplam;

                row.DovizCinsi = dvzCinsi;

                dset.Siparis.AddSiparisRow(row);

                say++;
            }

            foreach (KKP_CHK item in rPR[2])
            {
                fDD.CHKK.Add(item);
                chk = (item);
            }

            foreach (SatTalep item in rPR[3])
            {
                fDD.TLP.Add(item);
                talep = item;
            }

            try
            {
                foreach (var item in dset.Siparis)
                {
                    if (talep.IstenenTarih != null)
                        item.IstenenTarih = talep.IstenenTarih.ToString("dd.MM.yyyy");
                }
            }
            catch (Exception)
            {
                hata = true;
            }

            if (hata) return;

            try
            {
                db = new WMSEntities();
                var satisUzmani = db.Database.SqlQuery<GenelAyarVeParam>(string.Format("SELECT * FROM [Kaynak].[sta].[GenelAyarVeParams] WHERE Tip=1 AND Tip2=0 AND SiparisSorumlu = '{0}'", talep.Kaydeden)).FirstOrDefault();
                var gmy = db.Database.SqlQuery<GenelAyarVeParam>(string.Format("SELECT * FROM [Kaynak].[sta].[GenelAyarVeParams] WHERE Tip=1 AND Tip2=1 AND SiparisSorumlu = '{0}'", talep.GMYMaliOnaylayan)).FirstOrDefault();

                var rep = new SatSipForm();
                if (islemTip == (short)KKPIslemTipSPI.İçPiyasa)
                {
                    if (satisUzmani != null)
                    {
                        var user = db.Users.Where(m => m.Kod == satisUzmani.SiparisSorumlu).FirstOrDefault();
                        if (user != null)
                        {
                            rep.lblBirinciKisiAd.Text = user.AdSoyad;
                            rep.picBirinciKisiImza.Image = ByteArrayToImage.ToImage(satisUzmani.SiparisSorumluImza);
                        }
                    }

                    if (gmy != null)
                    {
                        var user = db.Users.Where(m => m.Kod == gmy.SiparisSorumlu).FirstOrDefault();
                        if (user != null)
                        {
                            rep.PicikinciKisiImza.Image = ByteArrayToImage.ToImage(gmy.SiparisSorumluImza);
                        }
                    }

                    if (talep.GMOnaylayan != null)
                    {
                        var gm = db.Database.SqlQuery<GenelAyarVeParam>(string.Format("SELECT * FROM [Kaynak].[sta].[GenelAyarVeParams] WHERE Tip=1 AND Tip2=2 AND SiparisSorumlu = '{0}'", (talep.GMOnaylayan ?? "-1"))).FirstOrDefault();
                        if (gm != null)
                        {
                            var user = db.Users.Where(m => m.Kod == gm.SiparisSorumlu).FirstOrDefault();
                            if (user != null)
                            {
                                rep.PicUcuncuKisiImza.Image = ByteArrayToImage.ToImage(gm.SiparisSorumluImza);
                            }
                        }
                    }
                    else
                        rep.lblUcuncuKisiUnvn.Visible = false;

                    rep.lblUnvan.Text = chk.Unvan1;
                    rep.lblAdrs.Text = string.Format("{0} {1} {2}", chk.FaturaAdres1, chk.FaturaAdres2, chk.FaturaAdres3);
                    rep.lblTel.Text = chk.Telefon1;
                    rep.lblFax.Text = chk.Fax1;
                    rep.lblEmail.Text = chk.SatAlmaIslemEMail;
                    rep.lblOrderDate.Text = dset.Siparis[0].OnaylananTarih;
                    rep.lblOrderNo.Text = sipEvrakNo;
                    rep.lblOrderDate.Text = sipTarih == null ? "" : ((DateTime)sipTarih).ToString("dd.MM.yyyy");

                    rep.DataSource = dset;
                }
                else if (islemTip == (short)KKPIslemTipSPI.DışPiyasa)
                {
                    if (satisUzmani != null)
                    {
                        var user = db.Users.Where(m => m.Kod == satisUzmani.SiparisSorumlu).FirstOrDefault();
                        if (user != null)
                        {
                            rep.lblBirinciKisiAd.Text = user.AdSoyad;
                            rep.picBirinciKisiImza.Image = ByteArrayToImage.ToImage(satisUzmani.SiparisSorumluImza);
                        }
                    }

                    if (gmy != null)
                    {
                        var user = db.Users.Where(m => m.Kod == gmy.SiparisSorumlu).FirstOrDefault();
                        if (user != null)
                        {
                            rep.PicikinciKisiImza.Image = ByteArrayToImage.ToImage(gmy.SiparisSorumluImza);
                        }
                    }

                    if (talep.GMOnaylayan != null)
                    {
                        var gm = db.Database.SqlQuery<GenelAyarVeParam>(string.Format("SELECT * FROM [Kaynak].[sta].[GenelAyarVeParams] WHERE Tip=1 AND Tip2=2 AND SiparisSorumlu = '{0}'", talep.GMOnaylayan ?? "-1")).FirstOrDefault();
                        if (gm != null)
                        {
                            var user = db.Users.Where(m => m.Kod == gm.SiparisSorumlu).FirstOrDefault();
                            if (user != null)
                            {
                                rep.PicUcuncuKisiImza.Image = ByteArrayToImage.ToImage(gm.SiparisSorumluImza);
                            }
                        }
                    }
                    else
                        rep.lblUcuncuKisiUnvn.Visible = false;

                    rep.lblUnvan.Text = chk.Unvan1;
                    rep.lblAdrs.Text = string.Format("{0} {1} {2}", chk.FaturaAdres1, chk.FaturaAdres2, chk.FaturaAdres3);
                    rep.lblTel.Text = chk.Telefon1;
                    rep.lblFax.Text = chk.Fax1;
                    rep.lblEmail.Text = chk.SatAlmaIslemEMail;
                    rep.lblOrderDate.Text = dset.Siparis[0].OnaylananTarih;
                    rep.lblOrderNo.Text = sipEvrakNo;
                    rep.lblOrderDate.Text = sipTarih == null ? "" : ((DateTime)sipTarih).ToString("dd.MM.yyyy");

                    rep.DataSource = dset;
                }

                if (pdfKaydet)
                {
                    var temp = string.Format("{0}{1}.pdf", Path.GetTempPath(), sipEvrakNo);
                    if (File.Exists(temp)) File.Delete(temp);
                    rep.ExportToPdf(temp);
                }
            }
            catch (Exception ex)
            {
                var inner = "";
                if (ex.InnerException != null)
                {
                    inner = ex.InnerException == null ? "" : ex.InnerException.Message;
                    if (ex.InnerException.InnerException != null) inner += ": " + ex.InnerException.InnerException.Message;
                }

                db.Logger("", "", "", ex.Message, inner, "Reports/SatınalmaSiparisFormu");

                return;
            }
        }
    }
}