namespace Wms12m.Entity
{
    public class RaporStokKodCase
    {
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string GrupKod { get; set; }
        public string TipKod { get; set; }
        public string OzelKod { get; set; }
        public string Kod1 { get; set; }
        public string Kod2 { get; set; }
        public string Kod3 { get; set; }
        public decimal GirMiktar { get; set; }
        public decimal CikMiktar { get; set; }
        public decimal StokMiktar { get; set; }
        public string Birim1 { get; set; }
        public decimal? StokMiktarBirim2 { get; set; }
        public string Birim2 { get; set; }
        public decimal? StokMiktarBirim3 { get; set; }
        public string Birim3 { get; set; }
    }
}
