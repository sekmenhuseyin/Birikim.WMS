namespace Wms12m.Entity
{
    public class MyDst
    {
        public string Depo { get; set; }
        public string DepoAdi { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string Birim { get; set; }

        public decimal DvrMiktar { get; set; }
        public decimal GirMiktar { get; set; }
        public decimal CikMiktar { get; set; }

        public decimal StokMiktar { get { return DvrMiktar + GirMiktar - CikMiktar; } set { } }

        public decimal STIDvrMiktar { get; set; }
        public decimal STIGirMiktar { get; set; }
        public decimal STICikMiktar { get; set; }

        public decimal STIKalanMiktar { get { return STIDvrMiktar + STIGirMiktar - STICikMiktar; } set { } }

        public override string ToString()
        {
            return string.Format("{0} - {1}  ({2}-{3})", Depo, DepoAdi, MalKodu, MalAdi);
        }
    }
}