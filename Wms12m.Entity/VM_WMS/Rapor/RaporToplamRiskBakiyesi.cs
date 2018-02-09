namespace Wms12m.Entity
{
    public class RaporToplamRiskBakiyesi
    {
        public string HesapKodu { get; set; }
        public string Ünvan { get; set; }
        public decimal? Borç { get; set; }
        public decimal? Alacak { get; set; }
        public decimal? Bakiye { get; set; }
        public decimal? ToplamBakiye { get; set; }
    }
}
