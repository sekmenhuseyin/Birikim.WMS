namespace Wms12m.Entity
{
    public class SatisAnaliziTemsilci
    {
        public string Kod9 { get; set; }
        public string MalAdi4 { get; set; }
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
        public decimal NetCiro { get; set; }
        public static string Sorgu = @"
                         IF(OBJECT_ID('tempdb..#UrunBolgeSatisAnalizi') IS NOT NULL)
                         BEGIN DROP TABLE #UrunBolgeSatisAnalizi END
                         CREATE TABLE #UrunBolgeSatisAnalizi(MalAdi4 VARCHAR(100),Kod9 NVARCHAR(50),Ay INT,NetCiro NUMERIC(25,6))
                         INSERT INTO #UrunBolgeSatisAnalizi
                         SELECT STK.MalAdi4,CHK.TipKod,MONTH(DATEADD(DD,STI.Tarih,'1899-12-30')) AS Ay,
                         SUM(CASE WHEN STI.KynkEvrakTip IN (1,163) THEN (STI.Tutar-STI.ToplamIskonto) 
                         ELSE (STI.Tutar-STI.ToplamIskonto)*-1 END) AS NetCiro
                         FROM FINSAT6{0}.FINSAT6{0}.STI AS STI WITH (NOLOCK) 
				         INNER JOIN FINSAT6{0}.FINSAT6{0}.STK AS STK WITH (NOLOCK) ON STI.MalKodu = STK.MalKodu 
                         LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK WITH (NOLOCK) ON CHK.Hesapkodu = STI.CHK
                         WHERE CHK.KartTip IN (0,4) 
                          AND (CHK.HesapKodu BETWEEN '1' AND '8') 
                         AND STI.KynkEvrakTip IN (1,2,163) 
				         AND (CHK.GrupKod='{1}' OR '{1}'='0') 
                         AND YEAR(DATEADD(DD,STI.Tarih,'1899-12-30'))={2}
                         AND (CHK.TipKod = '{3}' OR '{3}'='0')
				         AND ISNULL(STK.MalAdi4,'')<>''
                         GROUP BY STK.MalAdi4,CHK.TipKod,MONTH(DATEADD(DD,STI.Tarih,'1899-12-30'))

						 SELECT B.MalAdi4, 

					SUM(b.Ocak) as Ocak,
				      SUM(b.Subat) as Subat,
					    SUM(b.Mart) as Mart,
						  SUM(b.Nisan) as Nisan,
						    SUM(b.Mayis) as Mayis,
							  SUM(b.Haziran) as Haziran,
							   SUM(b.Temmuz) as Temmuz,
					    	SUM(b.Agustos) as Agustos,
						  SUM(b.Eylul) as Eylul,
						SUM(b.Ekim) as Ekim,
					  SUM(b.Kasim) as Kasim,
				    SUM(b.Aralik) as Aralik,	   
				  (SUM(b.Ocak)+SUM(b.Subat)+SUM(b.Mart)+SUM(b.Nisan)+SUM(b.Mayis)+SUM(b.Haziran)+ 
					SUM(b.Temmuz)+SUM(b.Agustos)+SUM(b.Eylul)+SUM(b.Ekim)+SUM(b.Kasim)+SUM(b.Aralik)) AS NetCiro	 
						  FROM(
                         SELECT US.MalAdi4,
                         ISNULL(MAX(CASE WHEN US.Ay = 1 THEN US.NetCiro ELSE 0 END),0) AS Ocak,
                         ISNULL(MAX(CASE WHEN US.Ay = 2 THEN US.NetCiro ELSE 0 END),0) AS Subat, 
                         ISNULL(MAX(CASE WHEN US.Ay = 3 THEN US.NetCiro ELSE 0 END),0) AS Mart,
                         ISNULL(MAX(CASE WHEN US.Ay = 4 THEN US.NetCiro ELSE 0 END),0) AS Nisan,
                         ISNULL(MAX(CASE WHEN US.Ay = 5 THEN US.NetCiro ELSE 0 END),0) AS Mayis,
                         ISNULL(MAX(CASE WHEN US.Ay = 6 THEN US.NetCiro ELSE 0 END),0) AS Haziran,
                         ISNULL(MAX(CASE WHEN US.Ay = 7 THEN US.NetCiro ELSE 0 END),0) AS Temmuz,
                         ISNULL(MAX(CASE WHEN US.Ay = 8 THEN US.NetCiro ELSE 0 END),0) AS Agustos,
                         ISNULL(MAX(CASE WHEN US.Ay = 9 THEN US.NetCiro ELSE 0 END),0) AS Eylul,
                         ISNULL(MAX(CASE WHEN US.Ay = 10 THEN US.NetCiro ELSE 0 END),0) AS Ekim,
                         ISNULL(MAX(CASE WHEN US.Ay = 11 THEN US.NetCiro ELSE 0 END),0) AS Kasim,
                         ISNULL(MAX(CASE WHEN US.Ay = 12 THEN US.NetCiro ELSE 0 END),0) AS Aralik
                         FROM #UrunBolgeSatisAnalizi AS US WITH (NOLOCK)
                         GROUP BY US.MalAdi4,US.Kod9 
						 ) AS B
						 Group BY B.MalAdi4
						 IF(OBJECT_ID('tempdb..#UrunBolgeSatisAnalizi') IS NOT NULL) BEGIN DROP TABLE #UrunBolgeSatisAnalizi END   ";
    }
}