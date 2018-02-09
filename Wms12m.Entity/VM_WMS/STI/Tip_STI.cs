namespace Wms12m.Entity
{
    public class Tip_STI
    {
        public bool AktarimDurumu { get; set; }
        public string Barkod { get; set; }
        public string Birim { get; set; }
        public int ID { get; set; }
        public int irsID { get; set; }
        public string Kaydeden { get; set; }
        public string MakaraNo { get; set; }
        public string MalAdi { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public decimal OkutulanMiktar { get; set; }
        public string Raf { get; set; }
        public int SiraNo { get; set; }
        public decimal YerlestirmeMiktari { get; set; }
        public decimal? YerMiktar { get; set; }
    }
}