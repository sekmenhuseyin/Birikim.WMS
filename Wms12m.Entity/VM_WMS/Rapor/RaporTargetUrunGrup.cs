namespace Wms12m.Entity
{
    public class RaporTargetUrunGrup
    {
        /// <summary> VarChar(50) (Not Null) </summary>
        public string UrunGrup { get; set; }
        public static string Sorgu = @"
        SELECT DISTINCT(A.MalAdi4) AS UrunGrup FROM [FINSAT6{0}].[FINSAT6{0}].STK AS A (NOLOCK) WHERE A.MalAdi4 <>' ' ORDER BY A.MalAdi4
        ";
    }
}
