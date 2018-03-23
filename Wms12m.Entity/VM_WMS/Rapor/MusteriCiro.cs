namespace Wms12m.Entity
{
    public class MusteriCiro
    {
        /// <summary> VarChar(20) (Not Null) </summary>
        public string HesapKodu { get; set; }
        /// <summary> VarChar(81) (Not Null) </summary>
        public string Unvan { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string TipKod { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string GrupKod { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal KrediLimiti { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal NetCiro { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal NetIade { get; set; }
        public static string Sorgu = @"
                                    SELECT 
                                    STI.Chk AS HesapKodu,
                                    CONCAT(CHK.Unvan1,SPACE(1),CHK.Unvan2) AS Unvan,
                                    CHK.TipKod,
                                    CHK.GrupKod,
                                    CHK.KrediLimiti AS KrediLimiti,
                                    SUM(CASE WHEN STI.KynkEvrakTip IN (1,163) THEN (STI.Tutar-STI.ToplamIskonto)
                                    ELSE (STI.Tutar-STI.ToplamIskonto)*-1 END) AS NetCiro,
                                    SUM(CASE WHEN STI.KynkEvrakTip=2 THEN (STI.Tutar-STI.ToplamIskonto) ELSE 0 END) AS NetIade  
                                    FROM FINSAT6{0}.FINSAT6{0}.STI AS STI WITH (NOLOCK)
                                    INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK WITH (NOLOCK) ON CHK.HesapKodu=STI.Chk
                                    WHERE (CHK.Kod3='MÜŞ') AND (STI.KynkEvrakTip IN (1,2,163))
                                    GROUP BY STI.Chk,CHK.Unvan1,CHK.Unvan2,CHK.TipKod,CHK.GrupKod,CHK.KrediLimiti
                                    ";
    }
}

