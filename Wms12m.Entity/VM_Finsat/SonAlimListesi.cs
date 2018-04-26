using System;

namespace Wms12m.Entity
{
    public class SonAlimListesi
    {
        public string EvrakNo { get; set; }

        public int Tarih { get; set; }

        public string Chk { get; set; }

        public string MalKodu { get; set; }

        public string Birim { get; set; }

        public decimal BirimMiktar { get; set; }

        public decimal BirimFiyat { get; set; }

        public decimal Tutar { get; set; }

        public decimal DvzBirimFiyat { get; set; }

        public decimal DovizKuru { get; set; }

        public decimal DovizTutar { get; set; }

        public string Unvan { get; set; }

        public string MalAdi { get; set; }

        public DateTime Tarih_D
        {
            get { return new DateTime(1900, 1, 1).AddDays(Tarih - 2); }
            set { }
        }
    }
}