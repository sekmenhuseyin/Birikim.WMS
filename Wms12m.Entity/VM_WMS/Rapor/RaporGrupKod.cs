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
}
