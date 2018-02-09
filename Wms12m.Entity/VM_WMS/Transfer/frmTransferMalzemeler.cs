namespace Wms12m.Entity
{
    /// <summary>
    /// sipariş listesi
    /// </summary>
    public class frmTransferMalzemeler
    {
        public decimal AlSiparis { get; set; }
        public string Birim { get; set; }
        public decimal Depo1GerekenMiktar { get; set; }
        public decimal Depo1KritikMiktar { get; set; }
        public decimal Depo1StokMiktar { get; set; }
        public decimal Depo2GerekenMiktar { get; set; }
        public decimal Depo2GunesStok { get; set; }
        public decimal Depo2KritikMiktar { get; set; }
        public decimal Depo2Miktar { get; set; }
        public decimal Depo2WmsStok { get; set; }
        public string MalAdi { get; set; }
        public string MalKodu { get; set; }
        public decimal SatSiparis { get; set; }
    }
}