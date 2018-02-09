namespace Wms12m.Entity
{
    /// <summary>
    /// sipariş toplama sırası
    /// </summary>
    public class frmSiparisToplama
    {
        public string Birim { get; set; }
        public string Bolum { get; set; }
        public string EvrakNo { get; set; }
        public string Kat { get; set; }
        public int KatID { get; set; }
        public string Koridor { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public string Raf { get; set; }
        public decimal Stok { get; set; }
        public int YerID { get; set; }
    }
}