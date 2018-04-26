namespace Wms12m.Entity
{
    public class SatTeklif
    {
        public int ID { get; set; }

        public int TeklifNo { get; set; }

        public System.DateTime Tarih { get; set; }

        public string HesapKodu { get; set; }

        public string MalKodu { get; set; }

        public string Birim { get; set; }

        public decimal BirimFiyat { get; set; }

        public decimal TeklifMiktar { get; set; }

        public short Durum { get; set; }

        public short DvzTL { get; set; }

        public string DvzCinsi { get; set; }

        public int TerminSure { get; set; }

        public decimal MinSipMiktar { get; set; }

        public System.Nullable<System.DateTime> TeklifBasTarih { get; set; }

        public System.Nullable<System.DateTime> TeklifBitTarih { get; set; }

        public System.Nullable<short> Vade { get; set; }

        public string TeslimYeri { get; set; }

        public string TeklifDosya { get; set; }

        public bool OneriDurum { get; set; }

        public string Aciklama { get; set; }

        public string Aciklama2 { get; set; }

        public string Aciklama3 { get; set; }

        public string Satinalmaci { get; set; }

        public string Kademe2Onaylayan { get; set; }

        public System.Nullable<System.DateTime> Kademe2OnayTarih { get; set; }

        public string Kademe1Onaylayan { get; set; }

        public System.Nullable<System.DateTime> Kademe1OnayTarih { get; set; }

        public short Durum2 { get; set; }

        public string KynkTalepNo { get; set; }

        public string KynkTalepEkDosya { get; set; }

        public string Kaydeden { get; set; }

        public System.DateTime KayitTarih { get; set; }

        public string KayitSirKodu { get; set; }

        public string Degistiren { get; set; }

        public System.DateTime DegisTarih { get; set; }

        public string DegisSirKodu { get; set; }

        public string TeklifAciklamasi { get; set; }

        public string Unvan { get; set; }

        public string MalAdi { get; set; }

        public string DvzTLStr
        {
            get
            {
                if (DvzTL == 0) return "Yerel Para";
                else if (DvzTL == 1) return "Döviz";
                return "";
            }
            set { }
        }
        public string DurumStr
        {
            get
            {
                if (Durum == (short)TeklifDurum.Giris)
                    return "Teklif Giriş";
                else if (Durum == (short)TeklifDurum.Fiyat)
                    return "Teklif Fiyat";
                else if (Durum == (short)TeklifDurum.TedarikciSecimiYapildi)
                    return "Tedarikçi Seçimi Yapıldı";
                else if (Durum == (short)TeklifDurum.GMYOrmanElenen)
                    return "GMY Tedarikçi Elenen";
                else if (Durum == (short)TeklifDurum.GMYOrmanOnayli)
                    return "GMY Tedarikçi Onay";
                else if (Durum == (short)TeklifDurum.GMYMaliElenen)
                    return "GMY Mali Elenen";
                else if (Durum == (short)TeklifDurum.GMYMaliOnayli)
                    return "Onaylı";
                return "";
            }
            set { }
        }
    }
}