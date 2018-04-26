namespace Wms12m.Entity
{
    public class BekleyenFiyatListesi
    {
        public int ID { get; set; }

        public string FiyatListNum { get; set; }

        public string MalKodu { get; set; }

        public string MalAdi { get; set; }

        public string HesapKodu { get; set; }

        public string Unvan { get; set; }

        public decimal SatisFiyat1 { get; set; }

        public string SatisFiyat1Birim { get; set; }

        public int SatisFiyat1BirimInt { get; set; }

        public decimal DovizSatisFiyat1 { get; set; }

        public string DovizSF1Birim { get; set; }

        public int DovizSF1BirimInt { get; set; }

        public string DovizCinsi { get; set; }

        public int ROW_ID { get; set; }

        public bool Onay { get; set; }

        public string Durum { get; set; }

        public string HangiOnayda { get; set; }
    }
}