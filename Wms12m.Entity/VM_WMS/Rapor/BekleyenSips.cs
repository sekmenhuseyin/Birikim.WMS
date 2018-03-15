namespace Wms12m.Entity
{
    public class BekleyenSips
    {
        /// <summary> VarChar(20) (Not Null) </summary>
        public string HesapKodu { get; set; }
        /// <summary> VarChar(81) (Not Null) </summary>
        public string Unvan { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string GrupKod { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string TipKod { get; set; }
        /// <summary> Decimal(26,6) (Allow Null) </summary>
        public decimal NetTutar { get; set; }
        public static string Sorgu = @"
                                    SELECT {1}
                                    SPI.Chk AS HesapKodu,
                                    CONCAT(CHK.Unvan1,SPACE(1),CHK.Unvan2) AS Unvan,
                                    CHK.GrupKod,
                                    CHK.TipKod,
                                    SUM(((SPI.Tutar - SPI.ToplamIskonto)/SPI.BirimMiktar)*(SPI.BirimMiktar-SPI.TeslimMiktar-SPI.KapatilanMiktar)) AS NetTutar
                                    FROM FINSAT6{0}.FINSAT6{0}.SPI AS SPI WITH (NOLOCK)
                                    INNER JOIN FINSAT6{0}.FINSAT6{0}.STK AS STK WITH (NOLOCK) ON STK.MALKODU = SPI.MALKODU 
                                    INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK WITH (NOLOCK) ON CHK.HesapKodu = SPI.Chk 
                                    WHERE SPI.KynkEvrakTip=62 AND SPI.SiparisDurumu=0
                                    GROUP BY SPI.chk,CHK.Unvan1,CHK.Unvan2,CHK.GrupKod,CHK.TipKod
                                    ";
    }
}