namespace Wms12m.Entity
{
    public class RaporSiparis
    {
        public int Tarih { get; set; }

        public string MalKodu { get; set; }

        public string MalAdi { get; set; }

        public string DovizCinsi { get; set; }

        public string EkDosya { get; set; }

        public string Aciklama { get; set; }

        public decimal BirimMiktar { get; set; }

        public decimal BirimFiyat { get; set; }

        public decimal Tutar { get; set; }

        public short DvzTL { get; set; }

        public decimal DovizTutar { get; set; }

        public string Birim { get; set; }

        public short Vade { get; set; }

        public string TeslimYeri { get; set; }

        public short IslemTip { get; set; }
    }
}