namespace Wms12m.Entity
{
    public class RaporTemsilci
    {
        public string GrupKod { get; set; }
        public string TipKod { get; set; }
        public int RowID { get; set; }
        public int GrupRowID { get; set; }
        public static string Sorgu = @"
        SELECT 
        DISTINCT(CHK.Grupkod) AS Grupkod,
        D.RowID AS GrupRowID,
        CHK.Tipkod AS TipKod,
        MIN(CHK.ROW_ID) AS RowID
        FROM [FINSAT6{0}].[FINSAT6{0}].CHK AS CHK WITH (NOLOCK) 
        LEFT JOIN (
        SELECT DISTINCT(IC1.Grupkod) AS Grupkod,MIN(IC1.ROW_ID) AS RowID FROM [FINSAT6{0}].[FINSAT6{0}].CHK AS IC1 WITH (NOLOCK)
        WHERE IC1.KartTip IN (0, 4) AND (IC1.HesapKodu BETWEEN '1' AND '8') AND IC1.Grupkod <> ' '
        GROUP BY IC1.GrupKod
        ) AS D ON CHK.GrupKod=D.Grupkod
        WHERE CHK.KartTip IN (0,4) AND (CHK.HesapKodu BETWEEN '1' and '8') AND CHK.Grupkod <>' ' AND CHK.Tipkod <>' ' AND D.RowID={1}
        GROUP BY CHK.Grupkod,D.RowID,CHK.TipKod
        ORDER BY CHK.Grupkod,CHK.TipKod
        ";
    }
}
