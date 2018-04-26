namespace Wms12m.Entity
{
    public class FiyatListSelect
    {
        public System.Nullable<bool> Onay { get; set; }

        public int ROW_ID { get; set; }

        public string MalKodu { get; set; }

        public string MalAdi { get; set; }

        public string GrupKod { get; set; }

        public string MusteriKodu { get; set; }

        public string Unvan { get; set; }

        public decimal SatisFiyat1 { get; set; }

        public string SF1Birim { get; set; }

        public string DovizSF1Birim { get; set; }

        public decimal DvzSatisFiyat1 { get; set; }

        public string SF1DovizCinsi { get; set; }

        public int KayitTarih { get; set; }

        public string Birim1 { get; set; }

        public string Birim2 { get; set; }

        public string Birim3 { get; set; }
    }
}