namespace Wms12m.Entity
{
    public class MMK
    {
        public string HesapKod { get; set; }
        public string HesapAd { get; set; }

        public override string ToString() => string.Format("{0}-{1}", HesapKod, HesapAd);
    }
}