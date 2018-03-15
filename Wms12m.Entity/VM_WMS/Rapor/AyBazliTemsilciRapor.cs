namespace Wms12m.Entity
{
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
                                        ,CONVERT(INT,LEFT(H1.AYYIL,2)) AS Ay
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
                                        ) AS D1 ON H1.TEMSILCI = D1.TipKod AND CONVERT(INT,LEFT(H1.AYYIL,2)) = D1.Ay
                                        WHERE H1.TIP = 1 AND RIGHT(H1.AYYIL,4)='{1}'

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
}