namespace Wms12m.Entity
{
    public class RaporStok
    {
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string DepoKodu { get; set; }
        public string DepoAdi { get; set; }
        public decimal? Birim1StokMiktar { get; set; }
        public string Birim1 { get; set; }
        public double? Birim2StokMiktar { get; set; }
        public string Birim2 { get; set; }
        public string SatMalKodu { get; set; }
        public string StokAdresi { get; set; }
        public string OnayliTedarikci { get; set; }
        public string OnayliTedarikciUnvan { get; set; }
    }
}
