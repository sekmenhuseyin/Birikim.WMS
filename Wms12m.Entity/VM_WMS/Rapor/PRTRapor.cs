namespace Wms12m.Entity
{
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
                                          AND (IC1.Tarih BETWEEN 0 AND @TAR2) 
                                          UNION ALL 
                                          SELECT IC2.HesapKodu,
                                          (CASE WHEN IC2.IslemTip=5 THEN -IC2.Tutar WHEN IC2.IslemTip=9 THEN -IC2.Tutar ELSE IC2.Tutar END) AS Tutar,
                                          IC2.BA 
                                          FROM FINSAT6{0}.FINSAT6{0}.CHI AS IC2 WITH (NOLOCK) 
                                          WHERE IC2.IslemTip NOT IN (16,21,27,32,36,37,41,42) 
                                          AND (IC2.Tarih BETWEEN 0 AND @TAR2)) AS A 
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
}