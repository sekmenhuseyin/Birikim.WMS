namespace Wms12m.Entity
{
    /// <summary>
    /// siparişten getir sayfası
    /// </summary>
    public class frmIrsaliyeMalzeme
    {
        public string Birim { get; set; }
        public decimal BirimMiktar { get; set; }
        public int DegisSaat { get; set; }
        public string EvrakNo { get; set; }
        public string Kod1 { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public int ROW_ID { get; set; }
        public short SiraNo { get; set; }
        public int Tarih { get; set; }
    }
}