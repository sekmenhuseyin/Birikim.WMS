namespace Wms12m.Entity
{
    public class RaporCariEkstreFatura
    {
        public string EvrakNo { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string Miktar { get; set; }
        public string Birim { get; set; }
        public string BirimFiyat { get; set; }
        public string Tutar { get; set; }
        public string IskontoOran1 { get; set; }
        public string IskontoOran2 { get; set; }
        public string IskontoOran3 { get; set; }
        public string IskontoOran4 { get; set; }
        public string IskontoOran5 { get; set; }
        public string ToplamIskonto { get; set; }
        public string NetTutar { get; set; }
    }
}
