namespace Wms12m.Entity
{
    /// <summary>
    /// uys kayıt
    /// </summary>
    public class frmUysTransfer
    {
        public string AraDepo { get; set; }
        public string[] Birim { get; set; }
        public string CikisDepo { get; set; }
        public string GirisDepo { get; set; }
        public string[] MalKodu { get; set; }
        public decimal[] Miktar { get; set; }
        public string[] SeriNo { get; set; }
        public int Tarih { get; set; }
    }
}