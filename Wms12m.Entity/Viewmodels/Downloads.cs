namespace Wms12m.Entity
{
    public class downStock
    {
        public string HucreAd { get; set; }
        public string MalKodu { get; set; }
        public string Birim { get; set; }
        public string Miktar { get; set; }
    }
    public class downStockHistory
    {
        public string islem { get; set; }
        public string Tarih { get; set; }
        public string Saat { get; set; }
        public string EvrakNo { get; set; }
        public string HucreAd { get; set; }
        public string MalKodu { get; set; }
        public string Birim { get; set; }
        public string Miktar { get; set; }
        public string Stok { get; set; }
    }
}
