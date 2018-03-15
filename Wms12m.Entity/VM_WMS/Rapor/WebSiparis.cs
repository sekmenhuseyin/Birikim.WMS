namespace Wms12m.Entity
{
    public class WebSiparis
    {
        /// <summary> NVarChar(10) (Allow Null) </summary>
        public string Tarih { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string EvrakNo { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string TipKod { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string GrupKod { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string HesapKodu { get; set; }
        /// <summary> VarChar(81) (Not Null) </summary>
        public string Unvan { get; set; }
        /// <summary> Decimal(26,6) (Not Null) </summary>
        public decimal Toplam { get; set; }
        /// <summary> VarChar(6) (Not Null) </summary>
        public string SiparisDurumu { get; set; }

        public static string Sorgu = @"
                                    DECLARE @TAR1 INT,@TAR2 INT
                                    SET @TAR1 = FINSAT6{0}.dbo.AyIlkSonGun({1},{2},1)
                                    SET @TAR2 = FINSAT6{0}.dbo.AyIlkSonGun({1},{2},0)
                                    SELECT {4}
                                    CONVERT(NVARCHAR(10),DATEADD(DD,SPI.Tarih,'1899-12-30'),104) AS Tarih,
                                    SPI.EvrakNo,
                                    CHK.TipKod,
                                    CHK.GrupKod,
                                    SPI.Chk AS HesapKodu,
                                    CONCAT(CHK.Unvan1,SPACE(1),CHK.Unvan2) AS Unvan,
                                    SUM(SPI.Tutar - SPI.ToplamIskonto) AS Toplam,
                                    (CASE WHEN SPI.SiparisDurumu = 1 THEN 'Kapalı' ELSE 'Açık' END) AS SiparisDurumu
                                    FROM FINSAT6{0}.FINSAT6{0}.SPI AS SPI WITH (NOLOCK)
                                    INNER JOIN FINSAT6{0}.FINSAT6{0}.STK AS STK WITH (NOLOCK) ON STK.MALKODU=SPI.MALKODU 
                                    INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK WITH (NOLOCK) ON CHK.HesapKodu=SPI.Chk 
                                    WHERE SPI.KynkEvrakTip = 62 AND (SPI.Tarih BETWEEN @TAR1 AND @TAR2) 
                                    {3}
                                    GROUP BY SPI.Tarih,SPI.EvrakNo,CHK.TipKod,CHK.GrupKod,SPI.Chk,CHK.Unvan1,CHK.Unvan2,SPI.SiparisDurumu
                                    ";
    }
}