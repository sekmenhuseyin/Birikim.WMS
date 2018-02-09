namespace Wms12m.Entity
{
    public class SatisIadeOnay
    {
        public string IadeNo { get; set; }
        public string IadeTarih { get; set; }
        public string Kaydeden { get; set; }
        public bool Onay { get; set; }
        public SatisIadeOnayList tbl { get; set; }
    }
}