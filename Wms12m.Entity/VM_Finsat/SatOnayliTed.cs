namespace Wms12m.Entity
{
    public class SatOnayliTed
    {
        public int ID { get; set; }

        public int TeklifNo { get; set; }

        public string HesapKodu { get; set; }

        public string MalKodu { get; set; }

        public short SiraNo { get; set; }

        public string Kaydeden { get; set; }

        public System.DateTime KayitTarih { get; set; }

        public string KayitSirKodu { get; set; }

        public string Degistiren { get; set; }

        public System.DateTime DegisTarih { get; set; }

        public string DegisSirKodu { get; set; }

        // public SatTeklif  TeklifInfo { get; set; }

        public string Unvan1 { get; set; }
        public string Birim { get; set; }
        public decimal BirimFiyat { get; set; }
        public short DvzTL { get; set; }
        public string DvzCinsi { get; set; }
        public int TerminSure { get; set; }
        public decimal MinSipMiktar { get; set; }
        public System.Nullable<System.DateTime> TeklifBasTarih { get; set; }
        public System.Nullable<System.DateTime> TeklifBitTarih { get; set; }
        public string MalAdi { get; set; }
    }
}