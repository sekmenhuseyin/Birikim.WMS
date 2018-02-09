namespace Wms12m.Entity
{
    /// <summary>
    /// transfer sayfa 1 onay list
    /// </summary>
    public class frmTransferMalzemeApprove
    {
        public string AraDepo { get; set; }
        public string[] Birims { get; set; }
        public string CikisDepo { get; set; }
        public string GirisDepo { get; set; }
        public string[] MalKodus { get; set; }
        public string[] Miktar { get; set; }
        public decimal[] Miktars { get; set; }
    }
}