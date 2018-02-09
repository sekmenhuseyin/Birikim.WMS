namespace Wms12m.Entity
{
    /// <summary>
    /// onay bekleyen transfer listesi
    /// </summary>
    public class frmOnayTransferList
    {
        public int ID { get; set; }
        public string TransferNo { get; set; }
        public int TransferTarihi { get; set; }
        public string Tarih { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public string CikisDepo { get; set; }
        public string GirisDepo { get; set; }
        public string Kaydeden { get; set; }
        public int KayitTarih { get; set; }
    }
}