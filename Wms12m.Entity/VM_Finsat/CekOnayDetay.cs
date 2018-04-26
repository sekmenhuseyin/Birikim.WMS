namespace Wms12m.Entity
{
    public class CekOnayDetay
    {
        public string EvrakNo { get; set; }

        public System.Nullable<System.DateTime> Tarih { get; set; }

        public string Veren { get; set; }

        public string VerenUnvan { get; set; }

        public string Borclu { get; set; }

        public string BorcluUnvan { get; set; }

        public decimal Tutar { get; set; }

        public System.Nullable<System.DateTime> VadeTarih { get; set; }

        public string SonPozisyon { get; set; }
    }
}