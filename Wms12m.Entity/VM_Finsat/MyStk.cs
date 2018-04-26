namespace Wms12m.Entity
{
    public class MyStk
    {
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }

        ///Birim1, Birim2, Birim3
        public string Birim1 { get; set; }
        public string Birim2 { get; set; }
        public string Birim3 { get; set; }

        public decimal BirimFiyat { get; set; }

        public override string ToString() => MalKodu + "   " + MalAdi;
    }
}