namespace Wms12m.Entity
{
    /// <summary>
    /// raporlar
    /// </summary>
    public class RaporVadesiGelmemisCekler
    {
        public string EvrakNo { get; set; }
        public string CHK { get; set; }
        public string Unvan { get; set; }
        public string CekNo { get; set; }
        public decimal? Tutar { get; set; }
        public string Tarih { get; set; }
        public string VadeTarih { get; set; }
        public string VerildigiYer { get; set; }
        public string VerildigiYerUnvan { get; set; }
        public string CekiDuzenleyen { get; set; }
    }
}
