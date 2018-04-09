namespace Wms12m.Entity
{
    public class GunlukSiparis
    {
        public string EVRAKNO { get; set; }
        public decimal Tutar { get; set; }
        public string Tarih { get; set; }
        public string CHK { get; set; }
        public string Unvan { get; set; }
        public string TipKod { get; set; }
        public string GrupKod { get; set; }
        public static string Sorgu = @"
        SELECT  SPI.EVRAKNO,
        sum(SPI.Tutar)-sum(SPI.IskontoTutar1)as Tutar,
        CONVERT(VARCHAR(10),CONVERT(datetime,SPI.Tarih-2),104)as Tarih,
        SPI.CHK,(CHK.Unvan1+' '+CHK.Unvan2) as Unvan,CHK.TipKod,CHK.GrupKod
                  FROM FINSAT6{0}.FINSAT6{0}.SPI AS SPI WITH (NOLOCK)
                  INNER JOIN FINSAT6{0}.FINSAT6{0}.STK AS STK WITH (NOLOCK) ON STK.MALKODU = SPI.MALKODU 
                  INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK WITH (NOLOCK) ON CHK.HesapKodu = SPI.CHK
                  WHERE SPI.KynkEvrakTip=62
                  AND SPI.Tarih BETWEEN {1} AND {2}
                  Group By SPI.EvrakNo,SPI.Tarih, SPI.Chk, (CHK.Unvan1+' '+CHK.Unvan2), CHK.TipKod,CHK.GrupKod";
    }
}