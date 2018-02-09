namespace Wms12m.Entity
{
    /// <summary>
    /// sipariş listesi formları
    /// </summary>
    public class frmSiparisSteps
    {
        public string DepoID { get; set; }
        public string[] EvrakNos { get; set; }
        public int[] IDs { get; set; }
        public string[] MalKodus { get; set; }
        public decimal[] Miktars { get; set; }
    }
}