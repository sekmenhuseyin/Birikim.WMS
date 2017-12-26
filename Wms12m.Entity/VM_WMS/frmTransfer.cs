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
        public string SirketID { get; set; }
        public string GirisDepo { get; set; }
        public string CikisDepo { get; set; }
        public string AraDepo { get; set; }
        public string[] MalKodus { get; set; }
        public string[] Birims { get; set; }
        public decimal[] Miktars { get; set; }
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
    /// Depo Transfer
    /// </summary>
    public class frmDepoTransferDetay
    {
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string Birim { get; set; }
        public decimal Miktar { get; set; }

        public decimal GirisDepoDoluluk { get; set; }
        public decimal GirisDepoStok { get; set; }
        public decimal GirisDepoKritikStok { get; set; }
        public decimal CikisDepoStok { get; set; }
        public decimal CikisDepoKritikStok { get; set; }
        public decimal GenelStok { get; set; }
        public decimal GenelKritikStok { get; set; }
        public decimal GirisDepoAlimSiparis { get; set; }
        public decimal GirisDepoSatisSiparis { get; set; }
        public decimal CikisDepoAlimSiparis { get; set; }
        public decimal CikisDepoSatisSiparis { get; set; }
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
    public class frmUysTransferDetay
    {
        public string MalKodu { get; set; }
        public string Birim { get; set; }
        public string SeriNo { get; set; }
        public decimal Miktar { get; set; }
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
    /// <summary>
    /// yetki kontrol
    /// </summary>
    public class frmUysDepoYetki
    {
        public int? GirisYetki { get; set; }
        public int? CikisYetki { get; set; }
    }
    /// <summary>
    /// stok kontrol
    /// </summary>
    public class frmUysStokKontrol
    {
        public decimal Miktar1 { get; set; }
        public decimal Miktar2 { get; set; }
    }
}
