namespace Wms12m.Entity
{
    /// <summary>
    /// sipariş listesi
    /// </summary>
    public class frmTransferMalzemeler
    {
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string Birim { get; set; }
        public decimal Depo1StokMiktar { get; set; }
        public decimal Depo1KritikMiktar { get; set; }
        public decimal Depo1GerekenMiktar { get; set; }
        public decimal AlSiparis { get; set; }
        public decimal SatSiparis { get; set; }
        public decimal Depo2GunesStok { get; set; }
        public decimal Depo2WmsStok { get; set; }
        public decimal Depo2KritikMiktar { get; set; }
        public decimal Depo2GerekenMiktar { get; set; }
        public decimal Depo2Miktar { get; set; }
    }
    /// <summary>
    /// transfer sayfa 1 onay list
    /// </summary>
    public class frmTransferMalzemeApprove
    {
        public string checkboxes { get; set; }
        public string SirketID { get; set; }
        public string GirisDepo { get; set; }
        public string CikisDepo { get; set; }
        public string AraDepo { get; set; }
    }
    /// <summary>
    /// genel bir şey
    /// </summary>
    public class frmMalKoduMiktar
    {
        public string MalKodu { get; set; }
        public string Birim { get; set; }
        public decimal Miktar { get; set; }
    }
}
