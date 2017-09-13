using System;
using System.Collections.Generic;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m
{
    public class YeniNesil
    {
        private string ConStr { get; set; }
        private string SirketKodu { get; set; }
        List<STIEvrakBilgi> EvrakNoList;
        private List<HesapItem> HesapList;
        /// <summary>
        /// yeni finsat
        /// </summary>
        public YeniNesil(string conStr, string sirketKodu)
        {
            ConStr = conStr;
            SirketKodu = sirketKodu;
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
            var evrakBilgiList = new List<STIEvrakBilgi>();
            SqlExper Exper = new SqlExper(ConStr, SirketKodu);
            evrakBilgiList = Exper.SelectList<STIEvrakBilgi>(string.Format(@"
            SELECT 0 as Tip, * FROM (
            SELECT TOP 1 STK005_EvrakSeriNo as EvrakNo, STK005_SEQNo as SiraNo  
            FROM YNS{0}.YNS{0}.STK005(NOLOCK)
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
            ORDER BY ID DESC) Teklif", SirketKodu));
            if (evrakBilgiList.IsNull() || evrakBilgiList.Count != 3)
            {
                throw new Exception(@"Sipariş ve/veya fatura için son evrak numarası bulunamadı. Lütfen STK002 ve STK005 tablolarını inceleyin");
            }
            EvrakNoList = new List<STIEvrakBilgi>();
            for (int i = 0; i < 3; i++)
            {
                string evrakNo = "";
                string seri = "";
                int siraNo = 0;
                evrakNo = evrakBilgiList[i].EvrakNo;
                siraNo = evrakBilgiList[i].SiraNo;
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
        /// <summary>
        /// fatura kaydetme işllemleri
        /// </summary>
        public Result FaturaKaydet(List<SepetUrun> pSepetUrunleri)
        {
            var FaturaSatirlari = new List<STK005>();

            EvrakNolari_YukleAyarla();
            HesapListesini_YukleAyarla(pSepetUrunleri[0].HesapKodu);

            int faturaSiraNo = EvrakNoList[0].SiraNo;
            int siparisSiraNo = EvrakNoList[1].SiraNo;
            string saat = DateTime.Now.ToString("HHmmss"); ///O anın saatini döner 154010 gibi
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

                FaturaSatirlari.Add(fatura);
            }
            return new Result();
        }
    }
}
