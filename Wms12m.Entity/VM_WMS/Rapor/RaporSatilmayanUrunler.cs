namespace Wms12m.Entity
{
    public class RaporSatilmayanUrunler
    {
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string Birim { get; set; }
        public decimal? StokMiktar { get; set; }
        public decimal KritikStok { get; set; }
        public string SonFaturaNo { get; set; }
        public string SonFaturaTarihi { get; set; }
        public string SonUretimTarihi { get; set; }
        public decimal SonSatisMiktari { get; set; }
        public string SonSatisMiktariBirim { get; set; }
    }
}
