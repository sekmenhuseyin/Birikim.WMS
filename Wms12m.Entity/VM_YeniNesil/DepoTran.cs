namespace Wms12m.Entity
{
    #endregion /// SatisIadeDetay Class

    public class DepoTran
    {
        public string KullaniciKodu { get; set; }
        public string MalKodu { get; set; }
        public string GirisDepo { get; set; }
        public string CikisDepo { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public int MiatTarih { get; set; }
    }
}