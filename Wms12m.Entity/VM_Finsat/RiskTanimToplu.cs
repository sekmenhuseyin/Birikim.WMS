namespace Wms12m.Entity
{
    public class RiskTanimToplu
    {
        public bool? Onay { get; set; }

        public string HesapKodu { get; set; }

        public string Unvan { get; set; }

        public decimal SahsiCekLimiti { get; set; }

        public decimal MusteriCekLimiti { get; set; }

        public decimal? YeniSahsiCekLimiti { get; set; }

        public decimal? YeniMusteriCekLimiti { get; set; }
    }
}