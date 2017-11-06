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
    /// <summary>
    /// depo listesi
    /// </summary>
    public class frmDepoList
    {
        public string Depo { get; set; }
        public string DepoAdi { get; set; }
    }
    /// <summary>
    /// bekleyen transfer listesi
    /// </summary>
    public class frmWaitingList
    {
        public string StiNo { get; set; }
        public string Depo { get; set; }
    }
    /// <summary>
    /// uys kayıt
    /// </summary>
    public class frmUysTransfer
    {
        public string CikisDepo { get; set; }
        public string AraDepo { get; set; }
        public string GirisDepo { get; set; }
        public int Tarih { get; set; }
        public string[] MalKodu { get; set; }
        public string[] Birim { get; set; }
        public string[] SeriNo { get; set; }
        public decimal[] Miktar { get; set; }
    }
    /// <summary>
    /// uys kayıt
    /// </summary>
    public class frmUysWaitingTransfer
    {
        public string CikisDepo { get; set; }
        public string AraDepo { get; set; }
        public string GirisDepo { get; set; }
        public string EmirNo { get; set; }
        public string EvrakNo { get; set; }
        public string Kaydeden { get; set; }
        public string Kaydeden2 { get; set; }
        public int Tarih { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string SeriNo { get; set; }
        public string Birim { get; set; }
        public decimal Miktar { get; set; }
    }
    /// <summary>
    /// bekleyen transfer listesi
    /// </summary>
    public class frmUysEmirEvrak
    {
        public string EmirNo { get; set; }
        public string EvrakNo { get; set; }
    }
}
