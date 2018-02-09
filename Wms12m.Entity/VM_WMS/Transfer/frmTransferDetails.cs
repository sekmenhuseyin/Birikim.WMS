namespace Wms12m.Entity
{
    /// <summary>
    /// sipariş listesi
    /// </summary>
    public class frmTransferDetails
    {
        public string AraDepo { get; set; }
        public string Birim { get; set; }
        public string CikisDepo { get; set; }
        public string EvrakNo { get; set; }
        public string GirisDepo { get; set; }
        public int GorevID { get; set; }
        public int ID { get; set; }
        public string MalAdi { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public string Olusturan { get; set; }
        public string Onaylayan { get; set; }
        public string Tarih { get; set; }
    }
}