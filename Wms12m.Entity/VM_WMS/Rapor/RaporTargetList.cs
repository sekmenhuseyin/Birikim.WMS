namespace Wms12m.Entity
{
    public class RaporTargetList
    {
        /// <summary> VarChar(20) (Allow Null) </summary>
        public string GrupKod { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal Borc { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal Alacak { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal Bakiye { get; set; }

        public static string Sorgu = @"
        SELECT 
        B.GrupKod,
        ISNULL(SUM(CASE WHEN A.BA = 0 THEN A.Tutar ELSE 0 END),0) AS Borc,
        ISNULL(SUM(CASE WHEN A.BA = 1 THEN A.Tutar ELSE 0 END),0) AS Alacak,
        ISNULL(SUM(CASE WHEN A.BA = 0 THEN A.Tutar ELSE -A.Tutar END),0) AS Bakiye FROM 
        (
        SELECT 
        KarsiHesapKodu AS HesapKodu,
        (CASE IslemTip WHEN 5 THEN -Tutar WHEN 9 THEN -Tutar ELSE Tutar END) AS Tutar,
        (CASE WHEN BA = 0 THEN 1 ELSE 0 END) AS BA FROM [FINSAT6{0}].[FINSAT6{0}].CHI (NOLOCK) WHERE KarsiHesapKodu IS NOT NULL AND KarsiHesapKodu <> '' AND IslemTip NOT IN (16,21,27,32,36,37,41,42) AND (Tarih BETWEEN 18264 AND 55000 ) 
        UNION ALL 
        SELECT 
        HesapKodu,
        (CASE IslemTip WHEN 5 THEN -Tutar WHEN 9 THEN -Tutar ELSE Tutar END) AS Tutar,
        BA FROM [FINSAT6{0}].[FINSAT6{0}].CHI (NOLOCK) WHERE IslemTip NOT IN (16,21,27,32,36,37,41,42) AND (Tarih BETWEEN 18264 AND 55000 )
        ) AS A 
        LEFT JOIN [FINSAT6{0}].[FINSAT6{0}].CHK B (NOLOCK) ON B.HesapKodu = A.HesapKodu 
        WHERE 
        (B.karttip in (0,4) AND (B.hesapkodu BETWEEN '1' AND '8') AND (B.GrupKod BETWEEN '' AND 'ZZZZZ') AND 
        (B.TipKod BETWEEN '' AND 'ZZZZZ') AND (B.BolgeKod BETWEEN '' AND '9999') AND (B.OzelKod BETWEEN '' AND 'ZZZZZ') AND 
        (B.DovizCinsi BETWEEN '' AND 'ZZZ') AND (B.KartTip BETWEEN 0 AND 4) AND (B.Kod1 BETWEEN '' AND 'ZZZZZ') AND 
        (B.Kod2 BETWEEN '' AND 'ZZZZZ') AND (B.Kod3 BETWEEN '' AND 'ZZZZZ') AND (B.Kod4 BETWEEN '' AND 'ZZZZZ')
        )
        GROUP BY B.Grupkod ORDER BY B.GrupKod";
    }
}
