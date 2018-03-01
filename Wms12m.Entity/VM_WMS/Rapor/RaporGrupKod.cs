using System;

namespace Wms12m.Entity
{
    public class RaporGrupKod
    {
        public string GrupKod { get; set; }
        public static string Sorgu = @"
        SELECT DISTINCT(CHK.Grupkod) AS Grupkod FROM [FINSAT6{0}].[FINSAT6{0}].CHK(NOLOCK) AS CHK 
        WHERE CHK.KartTip IN (0, 4) AND (CHK.HesapKodu BETWEEN '1' AND '8') AND CHK.Grupkod <> ' '
        ORDER BY CHK.Grupkod
        ";
    }
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
                                 SUM(((SPI.Tutar-SPI.ToplamIskonto)/SPI.BirimMiktar)*(SPI.Birimmiktar-SPI.TeslimMiktar-SPI.KapatilanMiktar)) AS BekleyenSiparis 
                                 FROM FINSAT6{0}.FINSAT6{0}.SPI AS SPI WITH (NOLOCK) 
                                 INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK  WITH (NOLOCK) ON CHK.HesapKodu=SPI.CHK 
                                 WHERE SPI.SiparisDurumu=0 
                                 AND SPI.Kod10 NOT IN ('Reddedildi') 
                                AND (SPI.Tarih BETWEEN @TAR1 AND @TAR2) 
                                 AND CHK.GrupKod='{3}'
                                 GROUP BY CHK.TipKod
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
                                AND (IC1.Tarih BETWEEN @TAR1 AND @TAR2) 
                                UNION ALL 
                                SELECT IC2.HesapKodu,
                                (CASE WHEN IC2.IslemTip=5 THEN -IC2.Tutar WHEN IC2.IslemTip=9 THEN -IC2.Tutar ELSE IC2.Tutar END) AS Tutar,
                                IC2.BA 
                                FROM FINSAT6{0}.FINSAT6{0}.CHI AS IC2 WITH (NOLOCK) 
                                WHERE IC2.IslemTip NOT IN (16,21,27,32,36,37,41,42) 
                                AND (IC2.Tarih BETWEEN @TAR1 AND @TAR2 )) AS A 
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
                                AND (IC1.Tarih BETWEEN @TAR1 AND @TAR2) 
                                UNION ALL 
                                SELECT IC2.HesapKodu,
                                (CASE WHEN IC2.IslemTip=5 THEN -IC2.Tutar WHEN IC2.IslemTip=9 THEN -IC2.Tutar ELSE IC2.Tutar END) AS Tutar,
                                IC2.BA 
                                FROM FINSAT6{0}.FINSAT6{0}.CHI AS IC2 WITH (NOLOCK) 
                                WHERE IC2.IslemTip NOT IN (16,21,27,32,36,37,41,42) 
                                AND (IC2.Tarih BETWEEN @TAR1 AND @TAR2 )) AS A 
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
                                WHERE H1.TIP=1 AND H1.BOLGE='{3}' AND (H1.TARIH BETWEEN @TAR1 AND @TAR2)
                                ";
    }
    public class CTargetRapor
    {
        /// <summary> VarChar(20) (Allow Null) </summary>
        public string Grupkod { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal Hedef { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal HedefOran { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal ToplamIade { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal NetCiro { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
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

                                SELECT 
                                B.Grupkod,
                                ISNULL(TT4.HEDEF,0) AS Hedef,
                                (CASE WHEN ISNULL(TT4.HEDEF,0) = '0' THEN 0 ELSE (ISNULL(TT1.NetCiro,0) * 100 / ISNULL(TT4.HEDEF,0)) END) AS HedefOran,
                                ISNULL(TT1.ToplamIade,0) AS ToplamIade,
                                ISNULL(TT1.NetCiro,0) AS NetCiro,
                                0.0 AS CiroOran,
                                ISNULL(SUM(CASE WHEN A.BA = 0 THEN A.Tutar ELSE -A.Tutar END),0) AS CariBorc,
                                ISNULL(TT3.Bakiye,0) AS PRT,
                                ISNULL(TT2.BekleyenSiparis,0) AS BekleyenSiparis
                                FROM ( 
                                SELECT IC1.KarsiHesapKodu AS HesapKodu,
                                (CASE WHEN IC1.IslemTip = 5 THEN -IC1.Tutar WHEN IC1.IslemTip = 9 THEN -IC1.Tutar ELSE IC1.Tutar END) AS Tutar,
                                (CASE WHEN IC1.BA = 0 THEN 1 ELSE 0 END) AS BA 
                                FROM FINSAT6{0}.FINSAT6{0}.CHI AS IC1 WITH (NOLOCK) 
                                WHERE (IC1.KarsiHesapKodu IS NOT NULL) 
                                AND IC1.KarsiHesapKodu <> '' 
                                AND IC1.IslemTip NOT IN (16,21,27,32,36,37,41,42) 
                                AND (IC1.Tarih BETWEEN @TAR1 AND @TAR2) 
                                UNION ALL 
                                SELECT IC2.HesapKodu,
                                (CASE WHEN IC2.IslemTip=5 THEN -IC2.Tutar WHEN IC2.IslemTip=9 THEN -IC2.Tutar ELSE IC2.Tutar END) AS Tutar,
                                IC2.BA 
                                FROM FINSAT6{0}.FINSAT6{0}.CHI AS IC2 WITH (NOLOCK) 
                                WHERE IC2.IslemTip NOT IN (16,21,27,32,36,37,41,42) 
                                AND (IC2.Tarih BETWEEN @TAR1 AND @TAR2 )) AS A 
                                LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK AS B WITH (NOLOCK) ON B.HesapKodu = A.HesapKodu
                                LEFT JOIN (
                                SELECT CHK.Grupkod,
                                SUM(CASE WHEN STI.Kynkevraktip IN (1,163) THEN (STI.Tutar-STI.ToplamIskonto) 
                                    ELSE (STI.Tutar-STI.ToplamIskonto)*-1 END) AS NetCiro,
                                SUM(CASE WHEN STI.Kynkevraktip = 2 THEN (STI.Tutar-STI.ToplamIskonto) ELSE 0 END) AS ToplamIade 
                                FROM FINSAT6{0}.FINSAT6{0}.STI AS STI WITH (NOLOCK) 
                                LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK WITH (NOLOCK) ON CHK.Hesapkodu=STI.CHK 
                                WHERE CHK.Karttip IN (0,4) 
                                AND (CHK.Hesapkodu BETWEEN '1' AND '8') 
                                AND STI.Kynkevraktip IN (1,2,163) 
                                AND (STI.Tarih BETWEEN @TAR1 AND @TAR2)
                                GROUP BY CHK.GrupKod
                                ) AS TT1 ON B.GrupKod=TT1.GrupKod
                                LEFT JOIN (
                                SELECT CHK.GrupKod,
                                SUM(((SPI.Tutar-SPI.ToplamIskonto)/SPI.BirimMiktar)*
                                (Birimmiktar-TeslimMiktar-KapatilanMiktar)) AS BekleyenSiparis
                                FROM FINSAT6{0}.FINSAT6{0}.SPI AS SPI WITH (NOLOCK) 
                                INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK WITH (NOLOCK) ON CHK.HesapKodu=SPI.CHK 
                                WHERE SPI.SiparisDurumu=0 
                                AND SPI.Kod10 != ('Reddedildi') 
                                AND (SPI.Tarih BETWEEN @TAR1 AND @TAR2) 
                                GROUP BY CHK.Grupkod
                                ) AS TT2 ON B.GrupKod=TT2.GrupKod
                                LEFT JOIN (
                                SELECT 
                                B.Grupkod,
                                ISNULL(SUM(CASE WHEN A.BA = 0 THEN A.Tutar ELSE -A.Tutar END),0) AS Bakiye 
                                FROM ( 
                                SELECT IC1.KarsiHesapKodu AS HesapKodu,
                                (CASE WHEN IC1.IslemTip=5 THEN -IC1.Tutar WHEN IC1.IslemTip=9 THEN -IC1.Tutar ELSE IC1.Tutar END) AS Tutar,
                                (CASE WHEN IC1.BA = 0 THEN 1 ELSE 0 END) AS BA 
                                FROM FINSAT6{0}.FINSAT6{0}.CHI AS IC1 WITH (NOLOCK) 
                                WHERE (IC1.KarsiHesapKodu IS NOT NULL) 
                                AND IC1.KarsiHesapKodu <> '' 
                                AND IC1.IslemTip NOT IN (16,21,27,32,36,37,41,42) 
                                AND (IC1.Tarih BETWEEN @TAR1 AND @TAR2) 
                                UNION ALL 
                                SELECT IC2.HesapKodu,
                                (CASE WHEN IC2.IslemTip=5 THEN -IC2.Tutar WHEN IC2.IslemTip=9 THEN -IC2.Tutar ELSE IC2.Tutar END) AS Tutar,
                                IC2.BA 
                                FROM FINSAT6{0}.FINSAT6{0}.CHI AS IC2 WITH (NOLOCK) 
                                WHERE IC2.IslemTip NOT IN (16,21,27,32,36,37,41,42) 
                                AND (IC2.Tarih BETWEEN @TAR1 AND @TAR2 )) AS A 
                                LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK AS B WITH (NOLOCK) ON B.HesapKodu = A.HesapKodu 
                                WHERE A.HesapKodu LIKE '%PR'
                                AND (B.KartTip IN (0,4) 
                                AND (B.HesapKodu BETWEEN '1' AND '8')
                                AND (B.GrupKod BETWEEN '' AND 'ZZZZZ') 
                                AND (B.TipKod BETWEEN '' AND 'ZZZZZ') 
                                AND (B.BolgeKod BETWEEN '' AND '9999') 
                                AND (B.OzelKod BETWEEN '' AND 'ZZZZZ') 
                                AND (B.DovizCinsi BETWEEN '' AND 'ZZZ') 
                                AND (B.KartTip BETWEEN 0 AND 4)
                                AND (B.Kod1 BETWEEN '' AND 'ZZZZZ') 
                                AND (B.Kod2 BETWEEN '' AND 'ZZZZZ') 
                                AND (B.Kod3 BETWEEN '' AND 'ZZZZZ') 
                                AND (B.Kod4 BETWEEN '' AND 'ZZZZZ'))
                                GROUP BY B.Grupkod
                                ) AS TT3 ON B.GrupKod=TT3.GrupKod
                                LEFT JOIN (
                                SELECT H1.BOLGE AS GrupKod,H1.HEDEF FROM FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK)
                                WHERE H1.TIP=0 AND (H1.TARIH BETWEEN @TAR1 AND @TAR2)
                                ) AS TT4 ON B.GrupKod=TT4.GrupKod
                                WHERE (B.KartTip IN (0,4) 
                                AND (B.HesapKodu BETWEEN '1' AND '8')
                                AND (B.GrupKod BETWEEN '' AND 'ZZZZZ')
                                AND (B.TipKod BETWEEN '' AND 'ZZZZZ') 
                                AND (B.BolgeKod BETWEEN '' AND '9999') 
                                AND (B.OzelKod BETWEEN '' AND 'ZZZZZ') 
                                AND (B.DovizCinsi BETWEEN '' AND 'ZZZ') 
                                AND (B.KartTip BETWEEN 0 AND 4)
                                AND (B.Kod1 BETWEEN '' AND 'ZZZZZ') 
                                AND (B.Kod2 BETWEEN '' AND 'ZZZZZ') 
                                AND (B.Kod3 BETWEEN '' AND 'ZZZZZ') 
                                AND (B.Kod4 BETWEEN '' AND 'ZZZZZ'))
								GROUP BY B.Grupkod,TT1.NetCiro,TT1.ToplamIade,TT2.BekleyenSiparis,TT3.Bakiye,TT4.HEDEF
                                ORDER BY B.GrupKod
                                ";
    }
    public class UrunGrupRapor
    {
        /// <summary> VarChar(50) (Not Null) </summary>
        public string UrunGrup { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal NetCiro { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal ToplamIade { get; set; }

        public static string Sorgu = @"
                                        DECLARE @TAR1 INT,@TAR2 INT
                                        SET @TAR1=ISNULL(FINSAT6{0}.dbo.AyIlkSonGun({1},{2},1),0)
                                        SET @TAR2=ISNULL(FINSAT6{0}.dbo.AyIlkSonGun({1},{2},0),0)
                                        SELECT STK.MalAdi4 AS UrunGrup,
                                        SUM(CASE WHEN STI.KynkEvrakTip IN (1,163) THEN (STI.Tutar-STI.ToplamIskonto) 
                                            ELSE (STI.Tutar-STI.ToplamIskonto)*-1 END) AS NetCiro,
                                        SUM(CASE WHEN STI.KynkEvrakTip IN (2) THEN (STI.Tutar-STI.ToplamIskonto) 
                                            ELSE 0 END) AS ToplamIade 
                                        FROM FINSAT6{0}.FINSAT6{0}.STI AS STI WITH (NOLOCK) 
                                        INNER JOIN FINSAT6{0}.FINSAT6{0}.STK AS STK  WITH (NOLOCK) ON STK.Malkodu=STI.Malkodu
                                        LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK WITH (NOLOCK) ON CHK.Hesapkodu=STI.CHK 
                                        WHERE CHK.KartTip IN (0,4) 
                                        AND (CHK.HesapKodu BETWEEN '1' AND '8') 
                                        AND STI.KynkEvrakTip IN (1,2,163) 
                                        AND (STI.Tarih BETWEEN @TAR1 AND @TAR2)
                                        AND CHK.GrupKod='{3}'
                                        GROUP BY STK.MalAdi4
                                        ";
    }
    public class PRTRapor
    {
        /// <summary> VarChar(20) (Allow Null) </summary>
        public string HesapKodu { get; set; }
        /// <summary> VarChar(81) (Not Null) </summary>
        public string Unvan { get; set; }
        /// <summary> VarChar(20) (Allow Null) </summary>
        public string GrupKod { get; set; }
        /// <summary> VarChar(20) (Allow Null) </summary>
        public string TipKod { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal Bakiye { get; set; }

        public static string Sorgu = @"
                                   DECLARE @TAR1 INT,@TAR2 INT
                                   SET @TAR1=ISNULL(FINSAT6{0}.dbo.AyIlkSonGun({1},{2},1),0)
                                   SET @TAR2=ISNULL(FINSAT6{0}.dbo.AyIlkSonGun({1},{2},0),0)
                                   SELECT ISNULL(B.HesapKodu,'') AS HesapKodu,
                                   CONCAT(B.Unvan1,SPACE(1),B.Unvan2) AS Unvan,
                                   ISNULL(B.GrupKod,'') AS GrupKod,
                                   ISNULL(B.TipKod,'') AS TipKod,
                                   ISNULL(SUM(CASE WHEN A.BA = 0 THEN A.Tutar ELSE -A.Tutar END),0) AS Bakiye 
                                   FROM (
                                   SELECT IC1.KarsiHesapKodu AS HesapKodu,
                                          (CASE WHEN IC1.IslemTip=5 THEN -IC1.Tutar WHEN IC1.IslemTip=9 THEN -IC1.Tutar ELSE IC1.Tutar END) AS Tutar,
                                          (CASE WHEN IC1.BA = 0 THEN 1 ELSE 0 END) AS BA 
                                          FROM FINSAT6{0}.FINSAT6{0}.CHI AS IC1 WITH (NOLOCK) 
                                          WHERE (IC1.KarsiHesapKodu IS NOT NULL) 
                                          AND IC1.KarsiHesapKodu <> '' 
                                          AND IC1.IslemTip NOT IN (16,21,27,32,36,37,41,42) 
                                          AND (IC1.Tarih BETWEEN @TAR1 AND @TAR2) 
                                          UNION ALL 
                                          SELECT IC2.HesapKodu,
                                          (CASE WHEN IC2.IslemTip=5 THEN -IC2.Tutar WHEN IC2.IslemTip=9 THEN -IC2.Tutar ELSE IC2.Tutar END) AS Tutar,
                                          IC2.BA 
                                          FROM FINSAT6{0}.FINSAT6{0}.CHI AS IC2 WITH (NOLOCK) 
                                          WHERE IC2.IslemTip NOT IN (16,21,27,32,36,37,41,42) 
                                          AND (IC2.Tarih BETWEEN @TAR1 AND @TAR2)) AS A 
                                   LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK AS B WITH (NOLOCK) ON B.HesapKodu = A.HesapKodu 
                                   WHERE B.KartTip IN (0,4) 
                                   AND (B.HesapKodu BETWEEN '1' AND '8') 
                                   AND B.GrupKod='{3}'
                                   AND (B.TipKod BETWEEN '' AND 'ZZZZZ')
                                   AND (B.BolgeKod BETWEEN '' AND '9999') 
                                   AND (B.OzelKod BETWEEN '' AND 'ZZZZZ') 
                                   AND (B.DovizCinsi BETWEEN '' AND 'ZZZ') 
                                   AND (B.KartTip BETWEEN 0 AND 4)
                                   AND (B.Kod1 BETWEEN '' AND 'ZZZZZ') 
                                   AND (B.Kod2 BETWEEN '' AND 'ZZZZZ')
                                   AND (B.Kod3 BETWEEN '' AND 'ZZZZZ') 
                                   AND (B.Kod4 BETWEEN '' AND 'ZZZZZ')
                                   AND (B.Hesapkodu like '%PR')
                                   GROUP BY B.HesapKodu,B.Unvan1,B.Unvan2,B.GrupKod,B.TipKod ORDER BY B.HesapKodu
                                   ";
    }
    public class AyBazliBolgeRapor
    {
        public string Bolge { get; set; }
        public decimal Ocak { get; set; }
        public decimal Subat { get; set; }
        public decimal Mart { get; set; }
        public decimal Nisan { get; set; }
        public decimal Mayis { get; set; }
        public decimal Haziran { get; set; }
        public decimal Temmuz { get; set; }
        public decimal Agustos { get; set; }
        public decimal Eylul { get; set; }
        public decimal Ekim { get; set; }
        public decimal Kasim { get; set; }
        public decimal Aralik { get; set; }
        public static string Sorgu = @"
                                    IF (OBJECT_ID('tempdb..#TargetAyBazliBolgeRapor') IS NOT NULL) BEGIN DROP TABLE #TargetAyBazliBolgeRapor END
                                    CREATE TABLE #TargetAyBazliBolgeRapor(Bolge NVARCHAR(20),Hedef NUMERIC(25,2),Ay INT)
                                    INSERT INTO #TargetAyBazliBolgeRapor
                                    SELECT H1.BOLGE,H1.HEDEF,MONTH(DATEADD(DD,H1.TARIH,'1899-12-30')) AS Ay
                                    FROM FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK)
                                    WHERE H1.TIP=0 AND YEAR(DATEADD(DD,H1.TARIH,'1899-12-30'))={1}
                                    SELECT C.Bolge,
                                    ISNULL(MAX(CASE WHEN C.Ay=1 THEN C.Hedef ELSE 0 END),0) AS Ocak,
                                    ISNULL(MAX(CASE WHEN C.Ay=2 THEN C.Hedef ELSE 0 END),0) AS Subat,
                                    ISNULL(MAX(CASE WHEN C.Ay=3 THEN C.Hedef ELSE 0 END),0) AS Mart,
                                    ISNULL(MAX(CASE WHEN C.Ay=4 THEN C.Hedef ELSE 0 END),0) AS Nisan, 
                                    ISNULL(MAX(CASE WHEN C.Ay=5 THEN C.Hedef ELSE 0 END),0) AS Mayis,
                                    ISNULL(MAX(CASE WHEN C.Ay=6 THEN C.Hedef ELSE 0 END),0) AS Haziran,
                                    ISNULL(MAX(CASE WHEN C.Ay=7 THEN C.Hedef ELSE 0 END),0) AS Temmuz,
                                    ISNULL(MAX(CASE WHEN C.Ay=8 THEN C.Hedef ELSE 0 END),0) AS Agustos,
                                    ISNULL(MAX(CASE WHEN C.Ay=9 THEN C.Hedef ELSE 0 END),0) AS Eylul,
                                    ISNULL(MAX(CASE WHEN C.Ay=10 THEN C.Hedef ELSE 0 END),0) AS Ekim,
                                    ISNULL(MAX(CASE WHEN C.Ay=11 THEN C.Hedef ELSE 0 END),0) AS Kasim,
                                    ISNULL(MAX(CASE WHEN C.Ay=12 THEN C.Hedef ELSE 0 END),0) AS Aralik
                                    FROM #TargetAyBazliBolgeRapor AS C WITH (NOLOCK)
                                    GROUP BY C.Bolge
                                    IF (OBJECT_ID('tempdb..#TargetAyBazliBolgeRapor') IS NOT NULL) BEGIN DROP TABLE #TargetAyBazliBolgeRapor END
                                      ";
    }
    public class AyBazliTemsilciRapor
    {
        public string Temsilci { get; set; }
        public string Bolge { get; set; }
        public decimal OcakHedef { get; set; }
        public decimal OcakNetCiro { get; set; }
        public decimal SubatHedef { get; set; }
        public decimal SubatNetCiro { get; set; }
        public decimal MartHedef { get; set; }
        public decimal MartNetCiro { get; set; }
        public decimal NisanHedef { get; set; }
        public decimal NisanNetCiro { get; set; }
        public decimal MayisHedef { get; set; }
        public decimal MayisNetCiro { get; set; }
        public decimal HaziranHedef { get; set; }
        public decimal HaziranNetCiro { get; set; }
        public decimal TemmuzHedef { get; set; }
        public decimal TemmuzNetCiro { get; set; }
        public decimal AgustosHedef { get; set; }
        public decimal AgustosNetCiro { get; set; }
        public decimal EylulHedef { get; set; }
        public decimal EylulNetCiro { get; set; }
        public decimal EkimHedef { get; set; }
        public decimal EkimNetCiro { get; set; }
        public decimal KasimHedef { get; set; }
        public decimal KasimNetCiro { get; set; }
        public decimal AralikHedef { get; set; }
        public decimal AralikNetCiro { get; set; }
        public static string Sorgu = @"
                                        IF (OBJECT_ID('tempdb..#TargetAyBazliTemsilciRapor') IS NOT NULL) BEGIN DROP TABLE #TargetAyBazliTemsilciRapor END
                                        CREATE TABLE #TargetAyBazliTemsilciRapor(Temsilci NVARCHAR(20),Bolge NVARCHAR(20),Hedef NUMERIC(25,2),NetCiro NUMERIC(25,2),Ay INT)
                                        INSERT INTO #TargetAyBazliTemsilciRapor
                                        SELECT H1.TEMSILCI
                                        ,H1.BOLGE
                                        ,H1.HEDEF
                                        ,ISNULL(D1.NetCiro,0) AS NetCiro
                                        ,MONTH(DATEADD(DD,H1.TARIH,'1899-12-30')) AS Ay
                                        FROM FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK)
                                        LEFT JOIN (
                                        SELECT CHK.TipKod,
                                                                        SUM(CASE WHEN STI.KynkEvrakTip IN (1,163) THEN (STI.Tutar-STI.ToplamIskonto) 
                                                                            ELSE (STI.Tutar-STI.ToplamIskonto)*-1 END) AS NetCiro,
                                                                        MONTH(DATEADD(DD,STI.Tarih,'1899-12-30')) AS Ay
                                                                        FROM FINSAT6{0}.FINSAT6{0}.STI AS STI WITH (NOLOCK) 
                                                                        LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK WITH (NOLOCK) ON CHK.HesapKodu=STI.CHK 
                                                                        WHERE CHK.KartTip IN (0,4) 
                                                                        AND (CHK.HesapKodu BETWEEN '1' AND '8') 
                                                                        AND STI.KynkEvrakTip IN (1,2,163) 
								                                        AND YEAR(DATEADD(DD,STI.Tarih,'1899-12-30'))={1}
                                                                        GROUP BY CHK.TipKod,MONTH(DATEADD(DD,STI.Tarih,'1899-12-30'))
                                        ) AS D1 ON H1.TEMSILCI = D1.TipKod AND MONTH(DATEADD(DD,H1.TARIH,'1899-12-30')) = D1.Ay
                                        WHERE H1.TIP = 1 AND YEAR(DATEADD(DD,H1.TARIH,'1899-12-30'))={1}

                                        SELECT CC.Temsilci,CC.Bolge,
                                        ISNULL(MAX(CASE WHEN CC.Ay=1 THEN CC.Hedef ELSE 0 END),0) AS OcakHedef,
                                        ISNULL(MAX(CASE WHEN CC.Ay=1 THEN CC.NetCiro ELSE 0 END),0) AS OcakNetCiro,
                                        ISNULL(MAX(CASE WHEN CC.Ay=2 THEN CC.Hedef ELSE 0 END),0) AS SubatHedef,
                                        ISNULL(MAX(CASE WHEN CC.Ay=2 THEN CC.NetCiro ELSE 0 END),0) AS SubatNetCiro,
                                        ISNULL(MAX(CASE WHEN CC.Ay=3 THEN CC.Hedef ELSE 0 END),0) AS MartHedef,
                                        ISNULL(MAX(CASE WHEN CC.Ay=3 THEN CC.NetCiro ELSE 0 END),0) AS MartNetCiro,
                                        ISNULL(MAX(CASE WHEN CC.Ay=4 THEN CC.Hedef ELSE 0 END),0) AS NisanHedef, 
                                        ISNULL(MAX(CASE WHEN CC.Ay=4 THEN CC.NetCiro ELSE 0 END),0) AS NisanNetCiro,
                                        ISNULL(MAX(CASE WHEN CC.Ay=5 THEN CC.Hedef ELSE 0 END),0) AS MayisHedef,
                                        ISNULL(MAX(CASE WHEN CC.Ay=5 THEN CC.NetCiro ELSE 0 END),0) AS MayisNetCiro,
                                        ISNULL(MAX(CASE WHEN CC.Ay=6 THEN CC.Hedef ELSE 0 END),0) AS HaziranHedef,
                                        ISNULL(MAX(CASE WHEN CC.Ay=6 THEN CC.NetCiro ELSE 0 END),0) AS HaziranNetCiro,
                                        ISNULL(MAX(CASE WHEN CC.Ay=7 THEN CC.Hedef ELSE 0 END),0) AS TemmuzHedef,
                                        ISNULL(MAX(CASE WHEN CC.Ay=7 THEN CC.NetCiro ELSE 0 END),0) AS TemmuzNetCiro,
                                        ISNULL(MAX(CASE WHEN CC.Ay=8 THEN CC.Hedef ELSE 0 END),0) AS AgustosHedef,
                                        ISNULL(MAX(CASE WHEN CC.Ay=8 THEN CC.NetCiro ELSE 0 END),0) AS AgustosNetCiro,
                                        ISNULL(MAX(CASE WHEN CC.Ay=9 THEN CC.Hedef ELSE 0 END),0) AS EylulHedef,
                                        ISNULL(MAX(CASE WHEN CC.Ay=9 THEN CC.NetCiro ELSE 0 END),0) AS EylulNetCiro,
                                        ISNULL(MAX(CASE WHEN CC.Ay=10 THEN CC.Hedef ELSE 0 END),0) AS EkimHedef,
                                        ISNULL(MAX(CASE WHEN CC.Ay=10 THEN CC.NetCiro ELSE 0 END),0) AS EkimNetCiro,
                                        ISNULL(MAX(CASE WHEN CC.Ay=11 THEN CC.Hedef ELSE 0 END),0) AS KasimHedef,
                                        ISNULL(MAX(CASE WHEN CC.Ay=11 THEN CC.NetCiro ELSE 0 END),0) AS KasimNetCiro,
                                        ISNULL(MAX(CASE WHEN CC.Ay=12 THEN CC.Hedef ELSE 0 END),0) AS AralikHedef,
                                        ISNULL(MAX(CASE WHEN CC.Ay=12 THEN CC.NetCiro ELSE 0 END),0) AS AralikNetCiro
                                        FROM #TargetAyBazliTemsilciRapor AS CC WITH (NOLOCK)
                                        GROUP BY CC.Temsilci,CC.Bolge
                                        IF (OBJECT_ID('tempdb..#TargetAyBazliTemsilciRapor') IS NOT NULL) BEGIN DROP TABLE #TargetAyBazliTemsilciRapor END
                                        ";
    }
    public class GunlukSiparis
    {
        /// <summary> VarChar(30) (Not Null) </summary>
        public string MalKodu { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string MalAdi { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal BirimMiktar { get; set; }
        /// <summary> Decimal(10,2) (Not Null) </summary>
        public decimal IskontoOran1 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal SatisFiyat3 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal BirimFiyat { get; set; }
        /// <summary> Decimal(26,6) (Allow Null) </summary>
        public decimal NetTutar { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short Valorgun { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal AlisFiyat1 { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string TipKod { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string GrupKod { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string EvrakNo { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string HesapKodu { get; set; }
        /// <summary> VarChar(81) (Not Null) </summary>
        public string Unvan { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string MalAdi4 { get; set; }

        public static string Sorgu = @"
                                   DECLARE @TAR1 INT,@TAR2 INT
                                   SET @TAR1 = FINSAT6{0}.dbo.AyIlkSonGun({1},{2},1)
                                   SET @TAR2 = FINSAT6{0}.dbo.AyIlkSonGun({1},{2},0)
                                   SELECT {3}
                                   SPI.MalKodu,
                                   STK.MalAdi,
                                   SPI.BirimMiktar,
                                   CONVERT(DECIMAL(10,2),SPI.IskontoOran1) AS IskontoOran1,
                                   STK.SatisFiyat3,
                                   SPI.BirimFiyat,
                                   (SPI.Tutar - SPI.ToplamIskonto) AS NetTutar,
                                   SPI.Valorgun,
                                   STK.AlisFiyat1,
                                   CHK.TipKod,
                                   CHK.GrupKod,
                                   SPI.EvrakNo,
                                   SPI.Chk AS HesapKodu,
                                   CONCAT(CHK.Unvan1,SPACE(1),CHK.Unvan2) AS Unvan,
                                   STK.MalAdi4
                                   FROM FINSAT6{0}.FINSAT6{0}.SPI AS SPI WITH (NOLOCK)
                                   INNER JOIN FINSAT6{0}.FINSAT6{0}.STK AS STK WITH (NOLOCK) ON STK.MALKODU = SPI.MALKODU 
                                   INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK WITH (NOLOCK) ON CHK.HesapKodu = SPI.CHK
                                   WHERE SPI.KynkEvrakTip=62 AND (SPI.Tarih BETWEEN @TAR1 AND @TAR2)
                                   ";
    }
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
                                    DECLARE @TAR1 INT,@TAR2 INT
                                    SET @TAR1 = FINSAT6{0}.dbo.AyIlkSonGun({1},{2},1)
                                    SET @TAR2 = FINSAT6{0}.dbo.AyIlkSonGun({1},{2},0)
                                    SELECT {3}
                                    SPI.Chk AS HesapKodu,
                                    CONCAT(CHK.Unvan1,SPACE(1),CHK.Unvan2) AS Unvan,
                                    CHK.GrupKod,
                                    CHK.TipKod,
                                    (SPI.Tutar - SPI.ToplamIskonto) AS NetTutar
                                    FROM FINSAT6{0}.FINSAT6{0}.SPI AS SPI WITH (NOLOCK)
                                    INNER JOIN FINSAT6{0}.FINSAT6{0}.STK AS STK WITH (NOLOCK) ON STK.MALKODU = SPI.MALKODU 
                                    INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK WITH (NOLOCK) ON CHK.HesapKodu = SPI.Chk 
                                    WHERE SPI.KynkEvrakTip=62 AND (SPI.Tarih BETWEEN @TAR1 AND @TAR2)
                                    ";
    }
}