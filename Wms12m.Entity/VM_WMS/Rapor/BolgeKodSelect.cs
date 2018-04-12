namespace Wms12m.Entity
{
    public class BolgeKodSelect
    {
        /// <summary> VarChar(4) (Not Null) </summary>
        public string BolgeKod { get; set; }
        /// <summary> VarChar(100) (Not Null) </summary>
        public string Aciklama { get; set; }


        public static string Sorgu = @"
        SELECT DISTINCT CHK.BolgeKod, KTK.Aciklama FROM FINSAT6{0}.FINSAT6{0}.CHK (NOLOCK) as CHK 
INNER JOIN SOLAR6.DBO.KTK (NOLOCK) as KTK on CHK.BolgeKod = KTK.kod
WHERE CHK.BolgeKod <> '' and KTK.tip=0";


    }
}
