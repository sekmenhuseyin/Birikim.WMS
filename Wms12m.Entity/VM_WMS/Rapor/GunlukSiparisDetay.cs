namespace Wms12m.Entity
{
    public class GunlukSiparisDetay
    {
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal BirimMiktar { get; set; }
        public decimal? IskontoOran1 { get; set; }
        public decimal SatisFiyat3 { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal? NetTutar { get; set; }
        public short Valorgun { get; set; }
        public decimal AlisFiyat1 { get; set; }
        public string TipKod { get; set; }
        public string GrupKod { get; set; }
        public string EvrakNo { get; set; }
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public string MalAdi4 { get; set; }
        public static string Sorgu = @"
          SELECT
          SS.MalKodu,
          STK.MalAdi,
          SS.BirimMiktar,
          CONVERT(DECIMAL(10,2),SS.IskontoOran1) AS IskontoOran1,
          STK.SatisFiyat3,
          SS.BirimFiyat,
          (SS.Tutar - SS.ToplamIskonto) AS NetTutar,
          SS.Valorgun,
          STK.AlisFiyat1,
          CHK.TipKod,
          CHK.GrupKod,
          SS.EvrakNo,
          SS.Chk AS HesapKodu,
          CONCAT(CHK.Unvan1,SPACE(1),CHK.Unvan2) AS Unvan,
          STK.MalAdi4
          FROM FINSAT6{0}.FINSAT6{0}.{1} AS SS WITH (NOLOCK)
          INNER JOIN FINSAT6{0}.FINSAT6{0}.STK AS STK WITH (NOLOCK) ON STK.MALKODU = SS.MALKODU 
          INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK WITH (NOLOCK) ON CHK.HesapKodu = SS.CHK
          WHERE SS.KynkEvrakTip IN ({2}) 
          AND LTRIM(SS.EvrakNo) = LTRIM('{3}')
          ";
    }
}
