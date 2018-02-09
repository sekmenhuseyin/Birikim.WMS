namespace Wms12m.Entity
{
    public class HesapItem
    {
        public static string Sorgu = @"
        SELECT CAR002_HesapKodu as HesapKodu, CAR002_Unvan1+' '+CAR002_Unvan2 as Unvan,
               CAR002_BankaHesapKodu as BankaHesapKodu, CAR002_ParaBirimi as ParaBirimi
        FROM  YNS{{0}}.YNS{{0}}.CAR002(NOLOCK)
        WHERE CAR002_AktifFlag=1 AND CAR002_BankaHesapKodu='{0}'
        ";

        public string BankaHesapKodu { get; set; }
        public string HesapKodu { get; set; }
        public string ParaBirimi { get; set; }
        public string Unvan { get; set; }
    }
}