using System;

namespace Wms12m.Entity.VM_Finsat
{
    public class Teklif
    {
        public string Aciklama { get; set; }
        public string Aciklama2 { get; set; }
        public string Aciklama3 { get; set; }
        public decimal AcikTalepMiktar { get; set; }
        public string Birim { get; set; }
        public decimal BirimFiyat { get; set; }
        public DateTime DegisTarih { get; set; }
        public string Degistiren { get; set; }
        public short Durum { get; set; }
        public short Durum2 { get; set; }

        public string DurumStr
        {
            get
            {
                switch (Durum)
                {
                    case (short)TeklifDurum.Giris:
                        return "Teklif Giriş";
                    case (short)TeklifDurum.Fiyat:
                        return "Teklif Fiyat";
                    case (short)TeklifDurum.TedarikciSecimiYapildi:
                        return "Tedarikçi Seçimi Yapıldı";
                    case (short)TeklifDurum.GMYOrmanElenen:
                        return "GMY Tedarikçi Elenen";
                    case (short)TeklifDurum.GMYOrmanOnayli:
                        return "GMY Tedarikçi Onay";
                    case (short)TeklifDurum.GMYMaliElenen:
                        return "GMY Mali Elenen";
                    case (short)TeklifDurum.GMYMaliOnayli:
                        return "Onaylı";
                }

                return "";
            }
            private set { }
        }

        public string DvzCinsi { get; set; }
        public decimal? DvzKuru { get; set; }

        /// <summary>
        /// 0 Yerel Para; 1 Döviz
        /// </summary>
        public short DvzTL { get; set; }

        public string DvzTLStr
        {
            get
            {
                if (DvzTL == 0) return "Yerel Para";
                else if (DvzTL == 1) return "Döviz";
                return "";
            }
            private set { }
        }

        public decimal? DvzTutar
        {
            get
            {
                if (DvzTL == 1)
                    return TeklifMiktar * (decimal)BirimFiyat;
                return null;
            }
            private set { }
        }

        public decimal GenelTutar2
        {
            get
            {
                return Tutar2 + KDVTutar2;
            }
            set { }
        }

        public string HesapKodu { get; set; }
        public decimal HesapTutar { get; set; }
        public int ID { get; set; }
        public string Kademe1Onaylayan { get; set; }
        public DateTime? Kademe1OnayTarih { get; set; }
        public string Kademe2Onaylayan { get; set; }
        public DateTime? Kademe2OnayTarih { get; set; }
        public string Kaydeden { get; set; }
        public DateTime KayitTarih { get; set; }
        public float KDVOrani { get; set; }

        public decimal KDVTutar
        {
            get
            {
                return MalBedeli * (decimal)KDVOrani;
            }
            private set { }
        }

        public decimal KDVTutar2
        {
            get
            {
                return (Tutar2 * (decimal)KDVOrani) / 100;
            }
            set { }
        }

        public string KynkTalepEkDosya { get; set; }
        public string KynkTalepEkDosya2 { get; set; }
        public string KynkTalepEkDosya3 { get; set; }
        public decimal? KynkTalepMiktar { get; set; }
        public string KynkTalepNo { get; set; }
        public string MalAdi { get; set; }
        public decimal MalBedeli
        {
            get
            {
                if (DvzTL == 0)
                    return TeklifMiktar * (decimal)BirimFiyat;
                else if (DvzTL == 1 && DvzKuru != null && DvzKuru > 0 && DvzTutar != null)
                    return (decimal)DvzTutar * (decimal)DvzKuru;
                return 0;
            }
            private set { }
        }
        public string MalKodu { get; set; }
        public decimal MinSipMiktar { get; set; }
        public bool OneriDurum { get; set; }
        public string Satinalmaci { get; set; }
        public bool Sec { get; set; }
        public bool Secim { get; set; }
        public string TalepEden { get; set; }
        public DateTime Tarih { get; set; }
        public string TeklifAciklamasi { get; set; }
        public DateTime? TeklifBasTarih { get; set; }
        public DateTime? TeklifBitTarih { get; set; }
        public string TeklifDosya { get; set; }
        public string TeklifDosyaYolu { get; set; }
        public decimal TeklifMiktar { get; set; }
        public int TeklifNo { get; set; }
        public int TerminSure { get; set; }
        public string TesisKodu { get; set; }
        public string TeslimYeri { get; set; }
        public string TLPKaydeden { get; set; }
        public string TalepEdenAdSoyad { get; set; }
        public string TesisAdi { get; set; }
        public decimal Tutar
        {
            get
            {
                return MalBedeli + KDVTutar;
            }
            set { }
        }
        public decimal Tutar2
        {
            get
            {
                return TeklifMiktar * (decimal)BirimFiyat;
            }
            set { }
        }
        public string Unvan { get; set; }
        public short? Vade { get; set; }

        public bool SozlesmeliTeklif { get; set; }
        public string TeklifAd { get; set; }
        public decimal MaxSipMiktar { get; set; }
        public int OdemeBicimId { get; set; }

    }
}
