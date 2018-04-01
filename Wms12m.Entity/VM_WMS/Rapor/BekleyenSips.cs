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
        public string Tarih { get; set; }
        public string SipNo { get; set; }

        public static string Sorgu = @"
                                     SELECT 
                                  SPI.Chk AS HesapKodu,
                                    CONCAT(CHK.Unvan1,SPACE(1),CHK.Unvan2) AS Unvan,
                                    CHK.GrupKod,
                                    CHK.TipKod,
                                    (CASE CHK.Grupkod when 'İHRACAT' THEN

									SUM(((SPI.Tutar - SPI.ToplamIskonto)/SPI.BirimMiktar)*(SPI.BirimMiktar-SPI.TeslimMiktar-SPI.KapatilanMiktar)*
									
                                ISNULL(dvz.kur00,1))

									ELSE
									SUM(((SPI.Tutar - SPI.ToplamIskonto)/SPI.BirimMiktar)*(SPI.BirimMiktar-SPI.TeslimMiktar-SPI.KapatilanMiktar)) END) AS NetTutar,
		                            REPLACE(CONVERT(NVARCHAR(10),DATEADD(DD,SPI.Tarih,'1899-12-30'),104),'.','-') AS Tarih,
									SPI.EvrakNo as SipNo
                                    FROM FINSAT6{0}.FINSAT6{0}.SPI AS SPI WITH (NOLOCK)
                                    INNER JOIN FINSAT6{0}.FINSAT6{0}.STK AS STK WITH (NOLOCK) ON STK.MALKODU = SPI.MALKODU 
                                    INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK WITH (NOLOCK) ON CHK.HesapKodu = SPI.Chk
									 LEFT JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON DVZ.DovizCinsi=SPI.DovizCinsi and SPI.Tarih=DVZ.Tarih 
                                    WHERE SPI.KynkEvrakTip=62 AND SPI.SiparisDurumu=0
                                    GROUP BY SPI.chk,CHK.Unvan1,CHK.Unvan2,CHK.GrupKod,CHK.TipKod, SPI.Tarih, SPI.EvrakNo
                                    ";
    }
}