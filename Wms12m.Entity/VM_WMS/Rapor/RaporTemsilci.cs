namespace Wms12m.Entity
{
    public class RaporTemsilci
    {
        public string GrupKod { get; set; }
        public string TipKod { get; set; }
        public static string Sorgu = @"
        SELECT DISTINCT(CHK.Grupkod) AS Grupkod,CHK.Tipkod AS TipKod FROM [FINSAT6{0}].[FINSAT6{0}].CHK AS CHK (NOLOCK) 
        WHERE CHK.KartTip IN (0,4) AND (CHK.HesapKodu BETWEEN '1' and '8') AND CHK.Grupkod <>' ' AND CHK.Tipkod <>' '  AND CHK.GrupKod='{1}'
        ORDER BY CHK.Grupkod,CHK.TipKod
        ";
    }
}
