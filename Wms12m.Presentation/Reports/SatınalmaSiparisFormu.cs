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
        public static void SatinalmaSiparisFormu(string sipEvrakNo, string hesapKodu, int siparisTarih, bool pdfKaydet, string SirketKodu)
        {
            WMSEntities db = new WMSEntities();
            if (string.IsNullOrEmpty(sipEvrakNo) || string.IsNullOrEmpty(hesapKodu))
                return;

            DsSatinalma dset = new DsSatinalma();
            string dvzCinsi = "TL";
            short islemTip = 1;
            KKP_CHK chk = new KKP_CHK();
            SatTalep talep = new SatTalep();
            bool hata = false;
            int say = 1;
            DateTime? sipTarih = null;
            SatinalmaRaporData _FDD = new SatinalmaRaporData()
            {
                FTDD = new List<RaporFTD>(),
                CHKK = new List<KKP_CHK>(),
                SPII = new List<RaporSiparis>(),
                TLP = new List<SatTalep>()
            };
            decimal ftdMalBedeli = 0, ftdKDV = 0, ftdToplam = 0;
            var RPR = db.MultipleResults(string.Format("[FINSAT6{0}].[wms].[Satinalma_RaporDetay] @EvrakNo='{1}',@HesapKodu='{2}',@Siptarih={3}", SirketKodu, sipEvrakNo, hesapKodu, siparisTarih)).With<RaporFTD>().With<RaporSiparis>().With<KKP_CHK>().With<SatTalep>().Execute();
            foreach (RaporFTD item in RPR[0])
            {
                _FDD.FTDD.Add(item);
                if (item.DovizCinsi.ToString().Trim() != "")
                    dvzCinsi = item.DovizCinsi.ToString().Trim();

                short satirTip = (short)item.SatirTip;
                decimal tutar = (decimal)item.Tutar;

                if (satirTip == 0)
                    ftdMalBedeli = tutar;
                else if (satirTip == 9)
                    ftdKDV = tutar;
                else if (satirTip == 12)
                    ftdToplam = tutar;
            }
            foreach (RaporSiparis item in RPR[1])
            {
                _FDD.SPII.Add(item);
                sipTarih = new DateTime(1900, 1, 1).AddDays((int)item.Tarih - 2);
                islemTip = (short)item.IslemTip;

                DsSatinalma.SiparisRow row = dset.Siparis.NewSiparisRow();
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
            foreach (KKP_CHK item in RPR[2])
            {
                _FDD.CHKK.Add(item);
                chk = (item);
            }
            foreach (SatTalep item in RPR[3])
            {
                _FDD.TLP.Add(item);
                talep = item;
            }
            try
            {
                foreach (var item in dset.Siparis)
                {
                    if (talep.IstenenTarih != null)
                        item.IstenenTarih = ((DateTime)talep.IstenenTarih).ToString("dd.MM.yyyy");
                }

            }
            catch (Exception)
            {
                hata = true;

            }
            if (hata)
                return;

            try
            {

                GenelAyarVeParam satisUzmani = db.Database.SqlQuery<GenelAyarVeParam>(string.Format("SELECT * FROM [Kaynak].[sta].[GenelAyarVeParams] WHERE Tip=1 AND Tip2=0 AND SiparisSorumlu = '{0}'", talep.Kaydeden)).FirstOrDefault();
                GenelAyarVeParam gmy = db.Database.SqlQuery<GenelAyarVeParam>(string.Format("SELECT * FROM [Kaynak].[sta].[GenelAyarVeParams] WHERE Tip=1 AND Tip2=1 AND SiparisSorumlu = '{0}'", talep.GMYMaliOnaylayan)).FirstOrDefault();

                if (islemTip == (short)KKPIslemTipSPI.İçPiyasa)
                {
                    SatSipForm rep = new SatSipForm();
                    if (satisUzmani != null)
                    {
                        User user = db.Users.Where(m => m.Kod == satisUzmani.SiparisSorumlu).FirstOrDefault();
                        if (user != null)
                        {
                            rep.lblBirinciKisiAd.Text = user.AdSoyad;
                            rep.picBirinciKisiImza.Image = ByteArrayToImage.byteArrayToImage(satisUzmani.SiparisSorumluImza);
                        }
                    }

                    if (gmy != null)
                    {
                        User user = db.Users.Where(m => m.Kod == gmy.SiparisSorumlu).FirstOrDefault();
                        if (user != null)
                        {
                            rep.PicikinciKisiImza.Image = ByteArrayToImage.byteArrayToImage(gmy.SiparisSorumluImza);
                        }
                    }

                    if (talep.GMOnaylayan != null)
                    {
                        GenelAyarVeParam gm = db.Database.SqlQuery<GenelAyarVeParam>(string.Format("SELECT * FROM [Kaynak].[sta].[GenelAyarVeParams] WHERE Tip=1 AND Tip2=2 AND SiparisSorumlu = '{0}'", (talep.GMOnaylayan ?? "-1"))).FirstOrDefault();
                        if (gm != null)
                        {
                            User user = db.Users.Where(m => m.Kod == gm.SiparisSorumlu).FirstOrDefault();
                            if (user != null)
                            {
                                rep.PicUcuncuKisiImza.Image = ByteArrayToImage.byteArrayToImage(gm.SiparisSorumluImza);
                            }
                        }
                    }
                    else
                        rep.lblUcuncuKisiUnvn.Visible = false;



                    rep.lblUnvan.Text = chk.Unvan1;
                    rep.lblAdrs.Text = String.Format("{0} {1} {2}", chk.FaturaAdres1, chk.FaturaAdres2, chk.FaturaAdres3);
                    rep.lblTel.Text = chk.Telefon1;
                    rep.lblFax.Text = chk.Fax1;
                    rep.lblEmail.Text = chk.SatAlmaIslemEMail;
                    rep.lblOrderDate.Text = dset.Siparis[0].OnaylananTarih;
                    rep.lblOrderNo.Text = sipEvrakNo;
                    rep.lblOrderDate.Text = sipTarih == null ? "" : ((DateTime)sipTarih).ToString("dd.MM.yyyy");

                    rep.DataSource = dset;

                    if (pdfKaydet)
                    {
                        string temp = String.Format("{0}{1}.pdf", Path.GetTempPath(), sipEvrakNo);
                        if (File.Exists(temp))
                        {
                            File.Delete(temp);
                        }
                        rep.ExportToPdf(temp);
                    }
                }
                else if (islemTip == (short)KKPIslemTipSPI.DışPiyasa)
                {
                    SatSipForm rep = new SatSipForm();

                    if (satisUzmani != null)
                    {
                        User user = db.Users.Where(m => m.Kod == satisUzmani.SiparisSorumlu).FirstOrDefault();
                        if (user != null)
                        {
                            rep.lblBirinciKisiAd.Text = user.AdSoyad;
                            rep.picBirinciKisiImza.Image = ByteArrayToImage.byteArrayToImage(satisUzmani.SiparisSorumluImza);
                        }
                    }

                    if (gmy != null)
                    {
                        User user = db.Users.Where(m => m.Kod == gmy.SiparisSorumlu).FirstOrDefault();
                        if (user != null)
                        {
                            rep.PicikinciKisiImza.Image = ByteArrayToImage.byteArrayToImage(gmy.SiparisSorumluImza);
                        }
                    }

                    if (talep.GMOnaylayan != null)
                    {
                        GenelAyarVeParam gm = db.Database.SqlQuery<GenelAyarVeParam>(string.Format("SELECT * FROM [Kaynak].[sta].[GenelAyarVeParams] WHERE Tip=1 AND Tip2=2 AND SiparisSorumlu = '{1}'", SirketKodu, (talep.GMOnaylayan ?? "-1"))).FirstOrDefault();
                        if (gm != null)
                        {
                            User user = db.Users.Where(m => m.Kod == gm.SiparisSorumlu).FirstOrDefault();
                            if (user != null)
                            {
                                rep.PicUcuncuKisiImza.Image = ByteArrayToImage.byteArrayToImage(gm.SiparisSorumluImza);
                            }
                        }
                    }
                    else
                        rep.lblUcuncuKisiUnvn.Visible = false;



                    rep.lblUnvan.Text = chk.Unvan1;
                    rep.lblAdrs.Text = String.Format("{0} {1} {2}", chk.FaturaAdres1, chk.FaturaAdres2, chk.FaturaAdres3);
                    rep.lblTel.Text = chk.Telefon1;
                    rep.lblFax.Text = chk.Fax1;
                    rep.lblEmail.Text = chk.SatAlmaIslemEMail;
                    rep.lblOrderDate.Text = dset.Siparis[0].OnaylananTarih;
                    rep.lblOrderNo.Text = sipEvrakNo;
                    rep.lblOrderDate.Text = sipTarih == null ? "" : ((DateTime)sipTarih).ToString("dd.MM.yyyy");

                    rep.DataSource = dset;

                    if (pdfKaydet)
                    {
                        string temp = String.Format("{0}{1}.pdf", Path.GetTempPath(), sipEvrakNo);
                        if (File.Exists(temp))
                        {
                            File.Delete(temp);
                        }
                        rep.ExportToPdf(temp);

                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
