namespace Wms12m.Entity
{
    /// <summary>
    /// onay bekleyen sipariş listesi
    /// </summary>
    public class frmOnaySiparisList
    {
        public decimal? BirimFiyat { get; set; }
        public int Cesit { get; set; }
        public string Depo { get; set; }
        public string DovizCinsi { get; set; }
        public decimal? EsikFiyat { get; set; }
        public string EvrakSeriNo { get; set; }
        public string HesapKodu { get; set; }
        public int ID { get; set; }
        public string Kaydeden { get; set; }
        public string MalAdi { get; set; }
        public string MalKodu { get; set; }
        public decimal? Miktar { get; set; }
        public string Notlar { get; set; }
        public string Tarih { get; set; }
        public decimal? Tutar { get; set; }
        public string Unvan { get; set; }
    }
}