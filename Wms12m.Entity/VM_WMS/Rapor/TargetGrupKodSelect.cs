namespace Wms12m.Entity
{
    public class TargetGrupKodSelect
    {



        /// <summary> VarChar(20) (Allow Null) </summary>
        public string GrupKod { get; set; }


        public static string Sorgu = @"
                               
                                DECLARE @TAR1 INT,@TAR2 INT
                                SET @TAR1=ISNULL(FINSAT6{0}.dbo.AyIlkSonGun({1},{2},1),0)
                                SET @TAR2=ISNULL(FINSAT6{0}.dbo.AyIlkSonGun({1},{2},0),0)

                                SELECT GrupKod FROM
                                (
                                SELECT 
                                ROW_NUMBER() OVER(ORDER BY B.GrupKod) AS SiraNo,
                                B.GrupKod
                                FROM ( 
                                SELECT IC1.KarsiHesapKodu AS HesapKodu,
                                (CASE WHEN IC1.IslemTip = 5 THEN -IC1.Tutar WHEN IC1.IslemTip = 9 THEN -IC1.Tutar ELSE IC1.Tutar END) AS Tutar,
                                (CASE WHEN IC1.BA = 0 THEN 1 ELSE 0 END) AS BA 
                                FROM FINSAT671.FINSAT671.CHI AS IC1 WITH (NOLOCK) 
                                WHERE (IC1.KarsiHesapKodu IS NOT NULL) 
                                AND IC1.KarsiHesapKodu <> '' 
                                AND IC1.IslemTip NOT IN (16,21,27,32,36,37,41,42) 
                                AND (IC1.Tarih BETWEEN @TAR1 AND @TAR2) 
                                UNION ALL 
                                SELECT IC2.HesapKodu,
                                (CASE WHEN IC2.IslemTip=5 THEN -IC2.Tutar WHEN IC2.IslemTip=9 THEN -IC2.Tutar ELSE IC2.Tutar END) AS Tutar,
                                IC2.BA 
                                FROM FINSAT671.FINSAT671.CHI AS IC2 WITH (NOLOCK) 
                                WHERE IC2.IslemTip NOT IN (16,21,27,32,36,37,41,42) 
                                AND (IC2.Tarih BETWEEN @TAR1 AND @TAR2 )
                                ) AS A 
                                LEFT JOIN FINSAT671.FINSAT671.CHK AS B WITH (NOLOCK) ON B.HesapKodu = A.HesapKodu
                                LEFT JOIN (
                                SELECT CHK.Grupkod,
                                SUM(CASE WHEN STI.Kynkevraktip IN (1,163) THEN (STI.Tutar-STI.ToplamIskonto) 
                                    ELSE (STI.Tutar-STI.ToplamIskonto)*-1 END) AS NetCiro,
                                SUM(CASE WHEN STI.Kynkevraktip = 2 THEN (STI.Tutar-STI.ToplamIskonto) ELSE 0 END) AS ToplamIade 
                                FROM FINSAT671.FINSAT671.STI AS STI WITH (NOLOCK) 
                                LEFT JOIN FINSAT671.FINSAT671.CHK AS CHK WITH (NOLOCK) ON CHK.Hesapkodu=STI.CHK 
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
                                FROM FINSAT671.FINSAT671.SPI AS SPI WITH (NOLOCK) 
                                INNER JOIN FINSAT671.FINSAT671.CHK AS CHK WITH (NOLOCK) ON CHK.HesapKodu=SPI.CHK 
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
                                FROM FINSAT671.FINSAT671.CHI AS IC1 WITH (NOLOCK) 
                                WHERE (IC1.KarsiHesapKodu IS NOT NULL) 
                                AND IC1.KarsiHesapKodu <> '' 
                                AND IC1.IslemTip NOT IN (16,21,27,32,36,37,41,42) 
                                AND (IC1.Tarih BETWEEN @TAR1 AND @TAR2) 
                                UNION ALL 
                                SELECT IC2.HesapKodu,
                                (CASE WHEN IC2.IslemTip=5 THEN -IC2.Tutar WHEN IC2.IslemTip=9 THEN -IC2.Tutar ELSE IC2.Tutar END) AS Tutar,
                                IC2.BA 
                                FROM FINSAT671.FINSAT671.CHI AS IC2 WITH (NOLOCK) 
                                WHERE IC2.IslemTip NOT IN (16,21,27,32,36,37,41,42) 
                                AND (IC2.Tarih BETWEEN @TAR1 AND @TAR2 )
                                ) AS A 
                                LEFT JOIN FINSAT671.FINSAT671.CHK AS B WITH (NOLOCK) ON B.HesapKodu = A.HesapKodu 
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
                                SELECT H1.BOLGE AS GrupKod,H1.HEDEF FROM FINSAT671.FINSAT671.HDF AS H1 WITH (NOLOCK)
                                WHERE H1.TIP=0 AND H1.AYYIL = '{3}'
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
                                AND (B.Kod4 BETWEEN '' AND 'ZZZZZ')
	                            AND (B.GrupKod <>'FİK'))
                                GROUP BY B.Grupkod,TT1.NetCiro,TT1.ToplamIade,TT2.BekleyenSiparis,TT3.Bakiye,TT4.HEDEF
                                ) AS D WHERE D.SiraNo = '{4}'
                                  ORDER BY D.GrupKod";


    }
}
