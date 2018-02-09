namespace Wms12m.Entity
{
    /// <summary>
    /// sipariş listesi onay
    /// </summary>
    public class frmSiparisIadeSteps
    {
        public string[] Birims { get; set; }
        public string DepoID { get; set; }
        public string EvrakNo { get; set; }
        public string[] EvrakNos { get; set; }
        public string HesapKodu { get; set; }
        public int[] IDs { get; set; }
        public string[] MalKodus { get; set; }
        public decimal[] Miktars { get; set; }
        public string Tarih { get; set; }
    }
}