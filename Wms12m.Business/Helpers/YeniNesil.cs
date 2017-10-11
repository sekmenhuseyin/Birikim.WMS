using DevHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m
{
    public class YeniNesil
    {
        private string ConStr { get; set; }
        private string SirketKodu { get; set; }
        private List<STIEvrakBilgi> EvrakNoList;
        private List<HesapItem> HesapList;
        private List<STK005> FaturaSatirlari;
        /// <summary>
        /// yeni finsat
        /// </summary>
        public YeniNesil(string conStr, string sirketKodu)
        {
            ConStr = conStr;
            SirketKodu = sirketKodu;
        }
        /// <summary>
        /// fatura kaydetme işllemleri
        /// </summary>
        public Result FaturaKaydet(List<SepetUrun> pSepetUrunleri)
        {
            FaturaSatirlari = new List<STK005>();

            EvrakNolari_YukleAyarla();
            HesapListesini_YukleAyarla(pSepetUrunleri[0].HesapKodu);

            int faturaSiraNo = EvrakNoList[0].SiraNo;
            int siparisSiraNo = EvrakNoList[1].SiraNo;
            string saat = DateTime.Now.ToString("HHmmss"); ///O anın saatini döner 154010 gibi
            SqlExper Sqlexper = new SqlExper(ConStr, SirketKodu);
            foreach (SepetUrun item in pSepetUrunleri)
            {
                string hesapKodu = "";
                HesapItem hesap = HesapList.Find(x => x.ParaBirimi.ToUpper() == item.ParaCinsi.ToUpper());
                if (hesap.IsNullEmpty())
                    throw new Exception("Hesapla ilgili stoğun döviz cinsleri uyuşmamaktadır !");
                hesapKodu = hesap.HesapKodu;
                faturaSiraNo++;
                STK005 fatura = new STK005();
                fatura.DefaultValueSet();
                fatura.STK005_MalKodu = item.UrunKodu;
                fatura.STK005_IslemTarihi = DateTime.Today.ToOADate().ToInt32();
                fatura.STK005_GC = 1;
                fatura.STK005_CariHesapKodu = hesapKodu;
                fatura.STK005_TeslimCHKodu = hesapKodu;
                fatura.STK005_EvrakSeriNo = EvrakNoList[0].EvrakNo;
                fatura.STK005_Miktari = item.Miktar.ToDecimal();
                fatura.STK005_BirimFiyati = item.Fiyat.ToDecimal();
                fatura.STK005_Tutari = item.Miktar.ToDecimal() * item.Fiyat.ToDecimal();
                fatura.STK005_IslemTipi = 2;
                fatura.STK005_EvrakTipi = 11;
                fatura.STK005_KDVOranFlag = 4;
                fatura.STK005_FaturaDurumu = 1;
                fatura.STK005_ParaBirimi = 1;
                fatura.STK005_SEQNo = faturaSiraNo;
                fatura.STK005_IptalDurumu = 1;
                fatura.STK005_AsilEvrakTarihi = fatura.STK005_IslemTarihi;
                fatura.STK005_SevkTarihi = fatura.STK005_IslemTarihi;
                fatura.STK005_VadeTarihi = fatura.STK005_IslemTarihi + 60;  ///Default 60 gün vade 
                fatura.STK005_DovizCinsi = item.ParaCinsi;
                fatura.STK005_Depo = item.Depo;
                fatura.STK005_RafKodu = item.Depo + "-A0";
                fatura.STK005_MaliyetMuhasebelesmeSekli = 1;
                fatura.STK005_Not5 = "AndMobil - " + item.KullaniciKodu;   ///Log amaçlı bilgi

                fatura.STK005_GirenKaynak = "Y6018";
                fatura.STK005_GirenSurum = "7.1.00";
                fatura.STK005_GirenKodu = item.KullaniciKodu;
                fatura.STK005_GirenTarih = DateTime.Today.Date.ToOADate().ToInt32();
                fatura.STK005_GirenSaat = saat;
                fatura.STK005_DegistirenKaynak = "Y6018";
                fatura.STK005_DegistirenSurum = "7.1.00";
                fatura.STK005_DegistirenKodu = item.KullaniciKodu;
                fatura.STK005_DegistirenTarih = DateTime.Today.Date.ToOADate().ToInt32();
                fatura.STK005_DegistirenSaat = saat;

                fatura.Adres1 = item.Adres1;
                fatura.Adres2 = item.Adres2;
                fatura.Adres3 = item.Adres3;
                fatura.Aciklama1 = item.Aciklama1;
                fatura.Aciklama2 = item.Aciklama2;
                fatura.Aciklama3 = item.Aciklama3;

                FaturaSatirlari.Add(fatura);
            }
            foreach (var item in FaturaSatirlari)
            {
                Sqlexper.Insert(item);
                STK004 stk = new STK004();
                stk.pk_STK004_MalKodu = item.STK005_MalKodu;
                stk.STK004_DegistirenKodu = item.STK005_DegistirenKodu;
                stk.STK004_DegistirenSaat = item.STK005_DegistirenSaat;
                stk.STK004_DegistirenTarih = item.STK005_DegistirenTarih;

                stk.SetAdd(STK004E.STK004_CikisMiktari, STK004E.STK004_CikisMiktari, SetIslem.Arti, item.STK005_Miktari.ToDot());
                stk.SetAdd(STK004E.STK004_CikisTutari, STK004E.STK004_CikisTutari, SetIslem.Arti, item.STK005_Tutari.ToDot());
                stk.STK004_SonCikisTarihi = item.STK005_DegistirenTarih;
                Sqlexper.Update(stk);
            }

            foreach (var grupItem in FaturaSatirlari.GroupBy(x => x.STK005_CariHesapKodu))
            {
                var firstItem = grupItem.First();

                #region  FTD INSERT

                CAR005 fMal = new CAR005();
                fMal.DefaultValueSet();
                fMal.CAR005_FaturaNo = firstItem.STK005_EvrakSeriNo;
                fMal.CAR005_FaturaTarihi = firstItem.STK005_IslemTarihi;
                fMal.CAR005_CHKodu = firstItem.STK005_CariHesapKodu;
                fMal.CAR005_TeslimCHKodu = firstItem.STK005_TeslimCHKodu;
                fMal.CAR005_ParaBirimi = firstItem.STK005_ParaBirimi;
                fMal.CAR005_TeslimCHKodu = firstItem.STK005_TeslimCHKodu;
                fMal.CAR005_IptalDurumu = firstItem.STK005_IptalDurumu;

                fMal.CAR005_SatirTipi = "G";
                fMal.CAR005_SatirNo = 2;

                fMal.CAR005_Tutar = grupItem.Sum(x => x.STK005_Tutari);
                fMal.CAR005_DovizCinsi = firstItem.STK005_DovizCinsi;

                fMal.CAR005_CariIslemTipi = 4;
                fMal.CAR005_Secenek = 1;
                fMal.CAR005_BA = "B";

                fMal.CAR005_TevkIslemTuru = null;
                fMal.CAR005_EFaturaDurumu = null;
                fMal.CAR005_EFaturaDonemBas = null;
                fMal.CAR005_EFaturaDonemBit = null;
                fMal.CAR005_EFaturaSure = null;
                fMal.CAR005_EFaturaSureBirimi = null;
                fMal.CAR005_TutarAlanDegeri = null;
                fMal.CAR005_AliciSiparisTarihi = null;
                fMal.CAR005_EArsivFaturaTipi = null;
                fMal.CAR005_EArsivFaturaDurumu = null;
                fMal.CAR005_EArsivFaturaTeslimSekli = null;

                Sqlexper.Insert(fMal);

                short satirNo = 3;
                CAR005 fSevkAciklama = new CAR005();
                fSevkAciklama.Set(fMal);
                fSevkAciklama.CAR005_SatirTipi = "A";
                fSevkAciklama.CAR005_Tutar = 0;
                fSevkAciklama.CAR005_Oran = 0;
                fSevkAciklama.CAR005_ParaBirimi = 0;
                fSevkAciklama.CAR005_DovizCinsi = "";
                fSevkAciklama.CAR005_Secenek = 5;

                if (firstItem.Adres1.IsNotNullEmpty())
                {
                    fSevkAciklama.CAR005_SatirNo = satirNo;
                    fSevkAciklama.CAR005_Filler = firstItem.Adres1.Substring(0, 1);
                    fSevkAciklama.CAR005_SatirAciklama = firstItem.Adres1.Substring(1, firstItem.Adres1.Length - 1);
                    Sqlexper.Insert(fSevkAciklama);
                    satirNo++;
                }
                if (firstItem.Adres2.IsNotNullEmpty())
                {
                    fSevkAciklama.CAR005_SatirNo = satirNo;
                    fSevkAciklama.CAR005_Filler = firstItem.Adres2.Substring(0, 1);
                    fSevkAciklama.CAR005_SatirAciklama = firstItem.Adres2.Substring(1, firstItem.Adres2.Length - 1);
                    Sqlexper.Insert(fSevkAciklama);
                    satirNo++;
                }
                if (firstItem.Adres3.IsNotNullEmpty())
                {
                    fSevkAciklama.CAR005_SatirNo = satirNo;
                    fSevkAciklama.CAR005_Filler = firstItem.Adres3.Substring(0, 1);
                    fSevkAciklama.CAR005_SatirAciklama = firstItem.Adres3.Substring(1, firstItem.Adres3.Length - 1);
                    Sqlexper.Insert(fSevkAciklama);
                    satirNo++;
                }

                if (firstItem.Aciklama1.IsNotNullEmpty())
                {
                    fSevkAciklama.CAR005_SatirNo = satirNo;
                    fSevkAciklama.CAR005_Filler = firstItem.Aciklama1.Substring(0, 1);
                    fSevkAciklama.CAR005_SatirAciklama = firstItem.Aciklama1.Substring(1, firstItem.Aciklama1.Length - 1);
                    Sqlexper.Insert(fSevkAciklama);
                    satirNo++;
                }
                if (firstItem.Aciklama2.IsNotNullEmpty())
                {
                    fSevkAciklama.CAR005_SatirNo = satirNo;
                    fSevkAciklama.CAR005_Filler = firstItem.Aciklama2.Substring(0, 1);
                    fSevkAciklama.CAR005_SatirAciklama = firstItem.Aciklama2.Substring(1, firstItem.Aciklama2.Length - 1);
                    Sqlexper.Insert(fSevkAciklama);
                    satirNo++;
                }
                if (firstItem.Aciklama3.IsNotNullEmpty())
                {
                    fSevkAciklama.CAR005_SatirNo = satirNo;
                    fSevkAciklama.CAR005_Filler = firstItem.Aciklama3.Substring(0, 1);
                    fSevkAciklama.CAR005_SatirAciklama = firstItem.Aciklama3.Substring(1, firstItem.Aciklama3.Length - 1);
                    Sqlexper.Insert(fSevkAciklama);
                    satirNo++;
                }

                #endregion   FTD INSERT END


                #region  CHI INSERT

                int siraNo = FaturaSatirlari[0].STK005_SEQNo.ToInt32();

                CAR003 chiMal = new CAR003();
                chiMal.DefaultValueSet();
                chiMal.CAR003_HesapKodu = firstItem.STK005_CariHesapKodu;
                chiMal.CAR003_Tarih = firstItem.STK005_IslemTarihi;
                chiMal.CAR003_EvrakSeriNo = firstItem.STK005_EvrakSeriNo;

                chiMal.CAR003_BA = (byte)0;
                chiMal.CAR003_IslemTipi = 4;
                chiMal.CAR003_EvrakTipi = 11;

                chiMal.CAR003_Tutar = grupItem.Sum(x => x.STK005_Tutari);
                chiMal.CAR003_KarsiEvrakSeriNo = firstItem.STK005_IadeFaturaNo;

                chiMal.CAR003_SEQNo = siraNo;
                chiMal.CAR003_VadeTarihi = firstItem.STK005_VadeTarihi;
                chiMal.CAR003_KDVOrani = 4;
                chiMal.CAR003_ParaBirimi = firstItem.STK005_ParaBirimi;
                chiMal.CAR003_IptalDurumu = 1;
                chiMal.CAR003_AsilEvrakTarihi = firstItem.STK005_AsilEvrakTarihi;
                chiMal.CAR003_MuhasebelesmeSekli = 1;
                chiMal.CAR003_Aciklama = "Faturamız";
                chiMal.CAR003_EvrakSayisi = 1;
                chiMal.CAR003_DovizCinsi = firstItem.STK005_DovizCinsi;

                chiMal.CAR003_YevmiyeEvrakTip = null;
                chiMal.CAR003_EFaturaDurumu = null;
                chiMal.CAR003_EArsivFaturaTipi = null;
                chiMal.CAR003_EArsivFaturaTeslimSekli = null;
                chiMal.CAR003_EArsivFaturaDurumu = null;
                chiMal.CAR003_YOKCZRaporuNo = null;
                chiMal.CAR003_YOKCBelgeTipi = null;
                chiMal.CAR003_YOKCBilgiFisiTipi = null;
                chiMal.CAR003_YOKCFisNo = null;
                chiMal.CAR003_YOKCFisSaat = null;
                chiMal.CAR003_YOKCDuzenlemeTip = null;

                chiMal.CAR003_GirenKaynak = "AN001";
                chiMal.CAR003_GirenSurum = "7.1.00";
                chiMal.CAR003_GirenKodu = firstItem.STK005_DegistirenKodu;
                chiMal.CAR003_GirenTarih = DateTime.Today.Date.ToOADate().ToInt32();
                chiMal.CAR003_GirenSaat = saat;
                chiMal.CAR003_DegistirenKaynak = "AN001";
                chiMal.CAR003_DegistirenSurum = "7.1.00";
                chiMal.CAR003_DegistirenKodu = firstItem.STK005_DegistirenKodu;
                chiMal.CAR003_DegistirenTarih = DateTime.Today.Date.ToOADate().ToInt32();
                chiMal.CAR003_DegistirenSaat = saat;

                Sqlexper.Insert(chiMal);

                #endregion  CHI INSERT END


                #region  CHK UPDATE

                CAR002 chk = new CAR002();
                chk.pk_CAR002_HesapKodu = firstItem.STK005_CariHesapKodu;
                chk.SetAdd(CAR002E.CAR002_CiroBorc, CAR002E.CAR002_CiroBorc, SetIslem.Arti, chiMal.CAR003_Tutar.ToDot());
                chk.CAR002_SonBorcTarihi = firstItem.STK005_IslemTarihi;
                Sqlexper.Update(chk);

                #endregion   CHK UPDATE END              
            }
            Sqlexper.AcceptChanges();
            return new Result();
        }
        /// <summary>
        /// evrak no oluştur
        /// </summary>
        private string EvrakNoOlustur(int evrakKarakterSayisi, string seri, int no)
        {
            if (no.ToString().Length > evrakKarakterSayisi - seri.Length)
                throw new Exception("Evrak numarasında taşma olmuştur !");
            string format = "";
            for (int i = 0; i < evrakKarakterSayisi - seri.Length; i++)
                format += "0";
            format = string.Format("1:{0}", format);
            format = "{0}{" + format + "}";
            format = string.Format(format, seri, no + 1);
            return format;
        }

        private void EvrakNolari_YukleAyarla()
        {
            List<STIEvrakBilgi> evrakBilgiList = new List<STIEvrakBilgi>();
            SqlExper Exper = new SqlExper(ConStr, SirketKodu);

            evrakBilgiList = Exper.SelectList<STIEvrakBilgi>(string.Format(@"
            SELECT 0 as Tip, * FROM (
            SELECT TOP 1 STK005_EvrakSeriNo as EvrakNo, STK005_SEQNo as SiraNo  
            FROM YNS{0}.YNS{0}.STK005(NOLOCK)
            WHERE STK005_EvrakTipi=11
            ORDER BY STK005_Row_ID DESC) Fatura
            UNION ALL
            SELECT 1 as Tip, * FROM (
            SELECT TOP 1 STK002_EvrakSeriNo as EvrakNo, STK002_SEQNo as SiraNo  
            FROM YNS{0}.YNS{0}.STK002(NOLOCK)
            WHERE STK002_GC=1
            ORDER BY STK002_Row_ID DESC) Siparis
            UNION ALL
            SELECT 2 as Tip, * FROM (
            SELECT TOP 1 TeklifNo as EvrakNo, 0 as SiraNo  
            FROM YNS{0}.YNS{0}.Teklif(NOLOCK)
            WHERE IslemTur=1
            ORDER BY ID DESC) Teklif
            UNION ALL
            SELECT 3 as Tip, * FROM (
            SELECT TOP 1 EvrakNo, 0 as SiraNo  
            FROM YNS{0}.YNS{0}.TempFatura(NOLOCK)
            ORDER BY ID DESC) TempFatura", SirketKodu));

           
            if (evrakBilgiList.IsNull() || evrakBilgiList.Count != 4)
            {
                throw new Exception(@"Sipariş ve/veya fatura için son evrak numarası bulunamadı. Lütfen STK002, STK005, Teklif ve TempFatura tablolarını inceleyin");
            }

            List<string> tabloList = new List<string> { "STK005", "STK002", "Teklif", "Fatura" };

            EvrakNoList = new List<STIEvrakBilgi>();
            for (int i = 0; i < 4; i++)
            {
                string evrakNo = "";
                string seri = "";
                int siraNo = 0;
                evrakNo = evrakBilgiList[i].EvrakNo;
                siraNo = evrakBilgiList[i].SiraNo;

                if (evrakNo.Length < 4)
                    throw new Exception(string.Format(@"Son evrak numarası geçersiz lütfen {0} tablosundaki son evrak numarasını kontrol edin !", tabloList[i]));

                seri = evrakNo.Substring(0, 2);
                evrakNo = evrakNo.Remove(0, 2);
                int no = evrakNo.ToInt32();

                evrakNo = EvrakNoOlustur(8, seri, no);

                EvrakNoList.Add(new STIEvrakBilgi { EvrakNo = evrakNo, SiraNo = siraNo, Tip = evrakBilgiList[i].Tip });
            }

        }

        /// <summary>
        /// hesap listesi
        /// </summary>
        private void HesapListesini_YukleAyarla(string HesapKodu)
        {
            HesapList = new List<HesapItem>();
            SqlExper Sqlexper = new SqlExper(ConStr, SirketKodu);
            HesapList = Sqlexper.SelectList<HesapItem>(string.Format(HesapItem.Sorgu, HesapKodu));
        }
    }
}
