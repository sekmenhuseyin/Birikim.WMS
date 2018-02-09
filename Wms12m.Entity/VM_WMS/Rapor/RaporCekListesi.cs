using System;

namespace Wms12m.Entity
{
    public class RaporCekListesi
    {
        public string EvrakNo { get; set; }
        public DateTime? Tarih { get; set; }
        public string Chk { get; set; }
        public string Unvan1 { get; set; }
        public decimal Tutar { get; set; }
        public DateTime? VadeTarih { get; set; }
        public DateTime? KayitTarih { get; set; }
        public string Pozisyon { get; set; }
        public string BorcluUnvan1 { get; set; }
        public string SonPozisyon { get; set; }
        public string VerildigiYer { get; set; }
        public string VerildigiYerUnvan { get; set; }
    }
}
