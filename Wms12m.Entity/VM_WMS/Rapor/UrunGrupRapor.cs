namespace Wms12m.Entity
{
    public class UrunGrupRapor
    {
        public string UrunGrup { get; set; }
        public decimal Hedef { get; set; }
        public string TipKod { get; set; }
        public decimal NetCiro { get; set; }
        public decimal Oran { get; set; }

        public static string Sorgu = @"
                                        DECLARE @TAR1 INT,@TAR2 INT
                                        SET @TAR1=FINSAT6{0}.DBO.AyIlkSonGun({3},{4},1)
                                        SET @TAR2=FINSAT6{0}.DBO.AyIlkSonGun({3},{4},0)
                                        IF(OBJECT_ID('tempdb..#UrunGrupRapor') IS NOT NULL) BEGIN DROP TABLE #UrunGrupRapor END
                                        CREATE TABLE #UrunGrupRapor (Kod9 NVARCHAR(20),Hedef NUMERIC(25,6),TipKod NVARCHAR(50),NetCiro NUMERIC(25,6))
                                        INSERT INTO #UrunGrupRapor
                                        SELECT H1.URUNGRUP
                                        ,H1.HEDEF
                                        ,D.TipKod
                                        ,D.NetCiro
                                        FROM FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK)
                                        RIGHT JOIN (
                                       SELECT STK.MalAdi4, SUM(CASE WHEN STI.KynkEvrakTip IN (1,163) THEN (STI.Tutar-STI.ToplamIskonto) 
                                                                ELSE (STI.Tutar-STI.ToplamIskonto)*-1 END) AS NetCiro,CHK.TipKod  
                                        FROM FINSAT6{0}.FINSAT6{0}.STI AS STI WITH (NOLOCK)
                                        LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK WITH (NOLOCK) ON CHK.Hesapkodu=STI.CHK 
                                        LEFT JOIN FINSAT6{0}.FINSAT6{0}.STK AS STK WITH (NOLOCK) ON STK.MALKODU=STI.MALKODU
                                        WHERE (CHK.KartTip IN (0,4)) AND (CHK.HesapKodu BETWEEN '1' AND '8') AND (STI.KynkEvrakTip IN (1,2,163)) 
                                        AND (STI.Tarih BETWEEN @TAR1 and @TAR2) AND CHK.GrupKod='{1}' AND STK.MalAdi4<>' '
                                          GROUP BY STK.MalAdi4,CHK.TipKod
                                        ) AS D ON H1.URUNGRUP=D.MalAdi4
                                        WHERE H1.BOLGE='{1}'  
                                        AND H1.AYYIL='{2}' 
                                        AND H1.TIP = 2 

                                        SELECT C.Kod9 AS UrunGrup,C.Hedef,C.TipKod,C.NetCiro,((C.NetCiro*100)/C.Hedef) AS Oran FROM #UrunGrupRapor AS C WITH (NOLOCK)
                                        IF(OBJECT_ID('tempdb..#UrunGrupRapor') IS NOT NULL) BEGIN DROP TABLE #UrunGrupRapor END
                                     ";
    }
}