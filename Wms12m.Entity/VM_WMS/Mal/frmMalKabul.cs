namespace Wms12m.Entity
{
    /// <summary>
    /// mal kabul kayıt
    /// </summary>
    public class frmMalKabul
    {
        public int ID { get; set; }
        public decimal OkutulanMiktar { get; set; }
        public string MakaraNo { get; set; }
        public bool? YeniSatir { get; set; }
    }
}