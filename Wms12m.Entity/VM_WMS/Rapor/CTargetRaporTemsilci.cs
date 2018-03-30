namespace Wms12m.Entity
{
    public class CTargetRaporTemsilci
    {
        /// <summary> NVarChar(20) (Allow Null) </summary>
        public string Temsilci { get; set; }
        /// <summary> NVarChar(20) (Not Null) </summary>
        public string GrupKod { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal Hedef { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal HedefOran { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal ToplamIade { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal NetSatisTutari { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal NetCiro { get; set; }
        /// <summary> Decimal(1,1) (Not Null) </summary>
        public decimal CiroOran { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal CariBorc { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal PRT { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal BekleyenSiparis { get; set; }

        public static string Sorgu = @"
       DECLARE @TAR1 INT,@TAR2 INT
        SET @TAR1=ISNULL(FINSAT6{0}.dbo.AyIlkSonGun({1},{2},1),0)
        SET @TAR2=ISNULL(FINSAT6{0}.dbo.AyIlkSonGun({1},{2},0),0)
        SELECT H1.TEMSILCI AS Temsilci,
        H1.BOLGE AS GrupKod,
        H1.HEDEF AS Hedef,
        (CASE WHEN H1.HEDEF>0 THEN (ISNULL(T1.NetCiro,0)* 100 / H1.HEDEF) ELSE 0 END) AS HedefOran,
        ISNULL(T1.ToplamIade,0) AS ToplamIade,
        (ISNULL(T1.ToplamIade,0)+ISNULL(T1.NetCiro,0)) AS NetSatisTutari,
        ISNULL(T1.NetCiro,0) AS NetCiro,
        0.0 AS CiroOran,
        ISNULL(T4.CariBorc,0) AS CariBorc,
        ISNULL(T3.Bakiye,0) AS PRT,
        ISNULL(T2.BekleyenSiparis,0) AS BekleyenSiparis
        FROM FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK)
        LEFT JOIN (
        SELECT CHK.TipKod AS Bolge,
            SUM(CASE WHEN STI.Kynkevraktip in (1,163) THEN (STI.Tutar-STI.ToplamIskonto) 
            ELSE (STI.Tutar-STI.ToplamIskonto)*-1 END) AS NetCiro,
        SUM(CASE WHEN STI.Kynkevraktip in (2) THEN (STI.Tutar-STI.ToplamIskonto) ELSE 0 END) AS ToplamIade
        FROM FINSAT6{0}.FINSAT6{0}.STI AS STI WITH (NOLOCK) 
        LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK WITH (NOLOCK) ON CHK.Hesapkodu=STI.CHK 
        WHERE CHK.Karttip IN (0,4) 
        AND (CHK.Hesapkodu BETWEEN '1' AND '8') 
        AND STI.Kynkevraktip IN (1,2,163) 
        AND (STI.Tarih BETWEEN @TAR1 AND @TAR2) 
        AND CHK.Grupkod='{3}'
        GROUP BY CHK.TipKod
        ) AS T1 ON H1.TEMSILCI=T1.Bolge
        LEFT JOIN (
            SELECT CHK.TipKod,
            case	CHK.Grupkod when 'İHRACAT' THEN 
        SUM((((SPI.Tutar-SPI.ToplamIskonto)/SPI.BirimMiktar)*(Birimmiktar-TeslimMiktar-KapatilanMiktar)) *
        ISNULL(dvz.kur00,1))
        ELSE SUM(((SPI.Tutar-SPI.ToplamIskonto)/SPI.BirimMiktar)*(Birimmiktar-TeslimMiktar-KapatilanMiktar)) END AS BekleyenSiparis
            FROM FINSAT6{0}.FINSAT6{0}.SPI AS SPI WITH (NOLOCK) 
            INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK  WITH (NOLOCK) ON CHK.HesapKodu=SPI.CHK 
	        LEFT JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON DVZ.DovizCinsi=SPI.DovizCinsi and SPI.Tarih=DVZ.Tarih
            WHERE SPI.SiparisDurumu=0 and SPI.KynkEvrakTip=62
            --AND SPI.Kod10 NOT IN ('Reddedildi') 
        --AND (SPI.Tarih BETWEEN @TAR1 AND @TAR2) 
            AND CHK.GrupKod='{3}'
            GROUP BY CHK.TipKod,CHK.Grupkod
        ) AS T2 ON H1.TEMSILCI=T2.TipKod
        LEFT JOIN (
        SELECT 
        B.TipKod,
        ISNULL(SUM(CASE WHEN A.BA = 0 THEN A.Tutar ELSE -A.Tutar END),0) AS Bakiye 
        FROM ( 
        SELECT IC1.KarsiHesapKodu AS HesapKodu,
        (CASE WHEN IC1.IslemTip=5 THEN -IC1.Tutar WHEN IC1.IslemTip=9 THEN -IC1.Tutar ELSE IC1.Tutar END) AS Tutar,
        (CASE WHEN IC1.BA = 0 THEN 1 ELSE 0 END) AS BA 
        FROM FINSAT6{0}.FINSAT6{0}.CHI AS IC1 WITH (NOLOCK) 
        WHERE (IC1.KarsiHesapKodu IS NOT NULL) 
        AND IC1.KarsiHesapKodu <> '' 
        AND IC1.IslemTip NOT IN (16,21,27,32,36,37,41,42) 
        AND (IC1.Tarih BETWEEN 0 AND @TAR2) 
        UNION ALL 
        SELECT IC2.HesapKodu,
        (CASE WHEN IC2.IslemTip=5 THEN -IC2.Tutar WHEN IC2.IslemTip=9 THEN -IC2.Tutar ELSE IC2.Tutar END) AS Tutar,
        IC2.BA 
        FROM FINSAT6{0}.FINSAT6{0}.CHI AS IC2 WITH (NOLOCK) 
        WHERE IC2.IslemTip NOT IN (16,21,27,32,36,37,41,42) 
        AND (IC2.Tarih BETWEEN 0 AND @TAR2 )) AS A 
        LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK AS B WITH (NOLOCK) ON B.HesapKodu = A.HesapKodu 
        WHERE A.HesapKodu LIKE '%PR'
        AND (B.KartTip IN (0,4) 
        AND (B.HesapKodu BETWEEN '1' AND '8')
        AND (B.GrupKod='{3}') 
        AND (B.TipKod BETWEEN '' AND 'ZZZZZ') 
        AND (B.BolgeKod BETWEEN '' AND '9999') 
        AND (B.OzelKod BETWEEN '' AND 'ZZZZZ') 
        AND (B.DovizCinsi BETWEEN '' AND 'ZZZ') 
        AND (B.KartTip BETWEEN 0 AND 4)
        AND (B.Kod1 BETWEEN '' AND 'ZZZZZ') 
        AND (B.Kod2 BETWEEN '' AND 'ZZZZZ') 
        AND (B.Kod3 BETWEEN '' AND 'ZZZZZ') 
        AND (B.Kod4 BETWEEN '' AND 'ZZZZZ'))
        GROUP BY B.TipKod

        ) AS T3 ON H1.TEMSILCI=T3.TipKod
        LEFT JOIN (
        SELECT 
        B.TipKod,
        ISNULL(SUM(CASE WHEN A.BA = 0 THEN A.Tutar ELSE -A.Tutar END),0) AS CariBorc
        FROM ( 
        SELECT IC1.KarsiHesapKodu AS HesapKodu,
        (CASE WHEN IC1.IslemTip = 5 THEN -IC1.Tutar WHEN IC1.IslemTip = 9 THEN -IC1.Tutar ELSE IC1.Tutar END) AS Tutar,
        (CASE WHEN IC1.BA = 0 THEN 1 ELSE 0 END) AS BA 
        FROM FINSAT6{0}.FINSAT6{0}.CHI AS IC1 WITH (NOLOCK) 
        WHERE (IC1.KarsiHesapKodu IS NOT NULL) 
        AND IC1.KarsiHesapKodu <> '' 
        AND IC1.IslemTip NOT IN (16,21,27,32,36,37,41,42) 
        AND (IC1.Tarih BETWEEN 0 AND @TAR2) 
        UNION ALL 
        SELECT IC2.HesapKodu,
        (CASE WHEN IC2.IslemTip=5 THEN -IC2.Tutar WHEN IC2.IslemTip=9 THEN -IC2.Tutar ELSE IC2.Tutar END) AS Tutar,
        IC2.BA 
        FROM FINSAT6{0}.FINSAT6{0}.CHI AS IC2 WITH (NOLOCK) 
        WHERE IC2.IslemTip NOT IN (16,21,27,32,36,37,41,42) 
        AND (IC2.Tarih BETWEEN 0 AND @TAR2 )) AS A 
        LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK AS B WITH (NOLOCK) ON B.HesapKodu = A.HesapKodu
        WHERE (B.KartTip IN (0,4) 
        AND (B.HesapKodu BETWEEN '1' AND '8')
        AND (B.GrupKod='{3}')
        AND (B.TipKod BETWEEN '' AND 'ZZZZZ') 
        AND (B.BolgeKod BETWEEN '' AND '9999') 
        AND (B.OzelKod BETWEEN '' AND 'ZZZZZ') 
        AND (B.DovizCinsi BETWEEN '' AND 'ZZZ') 
        AND (B.KartTip BETWEEN 0 AND 4)
        AND (B.Kod1 BETWEEN '' AND 'ZZZZZ') 
        AND (B.Kod2 BETWEEN '' AND 'ZZZZZ') 
        AND (B.Kod3 BETWEEN '' AND 'ZZZZZ') 
        AND (B.Kod4 BETWEEN '' AND 'ZZZZZ'))
        GROUP BY B.TipKod

        ) AS T4 ON H1.TEMSILCI=T4.TipKod
          WHERE H1.TIP=1 AND H1.BOLGE='{3}' AND H1.AYYIL='{4}'  ";

    }
}