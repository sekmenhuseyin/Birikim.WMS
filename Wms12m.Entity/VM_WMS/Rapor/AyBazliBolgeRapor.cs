namespace Wms12m.Entity
{
    public class AyBazliBolgeRapor
    {
        public string Bolge { get; set; }
        public decimal Ocak { get; set; }
        public decimal Subat { get; set; }
        public decimal Mart { get; set; }
        public decimal Nisan { get; set; }
        public decimal Mayis { get; set; }
        public decimal Haziran { get; set; }
        public decimal Temmuz { get; set; }
        public decimal Agustos { get; set; }
        public decimal Eylul { get; set; }
        public decimal Ekim { get; set; }
        public decimal Kasim { get; set; }
        public decimal Aralik { get; set; }
        public decimal ToplamHedef { get; set; }
        public static string Sorgu = @"

IF (OBJECT_ID('tempdb..#TargetAyBazliBolgeRapor') IS NOT NULL) BEGIN DROP TABLE #TargetAyBazliBolgeRapor END
CREATE TABLE #TargetAyBazliBolgeRapor(Bolge NVARCHAR(20),Hedef NUMERIC(25,2),Ay INT)
INSERT INTO #TargetAyBazliBolgeRapor
SELECT H1.BOLGE,H1.HEDEF,CONVERT(INT,LEFT(H1.AYYIL,2)) AS Ay
FROM FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK)
WHERE H1.TIP=0 AND RIGHT(H1.AYYIL,4)='{1}'
SELECT D.Bolge,D.Ocak, D.Subat, D.Mart, D.Nisan, D.Mayis, D.Haziran, D.Temmuz, D.Agustos,
D.Eylul, D.Ekim, D.Kasim, D.Aralik,(D.Ocak+ D.Subat+ D.Mart+ D.Nisan+ D.Mayis+ D.Haziran+ D.Temmuz+ D.Agustos+
D.Eylul+ D.Ekim+ D.Kasim+ D.Aralik) as ToplamHedef  FROM (
SELECT C.Bolge,
ISNULL(MAX(CASE WHEN C.Ay=1 THEN C.Hedef ELSE 0 END),0) AS Ocak,
ISNULL(MAX(CASE WHEN C.Ay=2 THEN C.Hedef ELSE 0 END),0) AS Subat,
ISNULL(MAX(CASE WHEN C.Ay=3 THEN C.Hedef ELSE 0 END),0) AS Mart,
ISNULL(MAX(CASE WHEN C.Ay=4 THEN C.Hedef ELSE 0 END),0) AS Nisan, 
ISNULL(MAX(CASE WHEN C.Ay=5 THEN C.Hedef ELSE 0 END),0) AS Mayis,
ISNULL(MAX(CASE WHEN C.Ay=6 THEN C.Hedef ELSE 0 END),0) AS Haziran,
ISNULL(MAX(CASE WHEN C.Ay=7 THEN C.Hedef ELSE 0 END),0) AS Temmuz,
ISNULL(MAX(CASE WHEN C.Ay=8 THEN C.Hedef ELSE 0 END),0) AS Agustos,
ISNULL(MAX(CASE WHEN C.Ay=9 THEN C.Hedef ELSE 0 END),0) AS Eylul,
ISNULL(MAX(CASE WHEN C.Ay=10 THEN C.Hedef ELSE 0 END),0) AS Ekim,
ISNULL(MAX(CASE WHEN C.Ay=11 THEN C.Hedef ELSE 0 END),0) AS Kasim,
ISNULL(MAX(CASE WHEN C.Ay=12 THEN C.Hedef ELSE 0 END),0) AS Aralik
FROM #TargetAyBazliBolgeRapor AS C WITH (NOLOCK)
GROUP BY C.Bolge) AS D



IF (OBJECT_ID('tempdb..#TargetAyBazliBolgeRapor') IS NOT NULL) BEGIN DROP TABLE #TargetAyBazliBolgeRapor END


                                      ";
    }
}