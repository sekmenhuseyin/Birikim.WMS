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
    SELECT A.UrunGrup, SUM(A.Hedef) AS Hedef, A.TipKod, A.NetCiro, CASE WHEN SUM(A.Hedef)=0 THEN 0 ELSE ((A.NetCiro*100)/SUM(A.Hedef)) END AS Oran FROM(
	SELECT H1.URUNGRUP AS UrunGrup
	,H1.HEDEF AS Hedef
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
	AND (STI.Tarih BETWEEN FINSAT6{0}.DBO.AyIlkSonGun({3},{4},1) and FINSAT6{0}.DBO.AyIlkSonGun({3},{4},0)) AND CHK.GrupKod='{1}' AND STK.MalAdi4<>' '
	GROUP BY STK.MalAdi4,CHK.TipKod
	) AS D ON H1.URUNGRUP=D.MalAdi4 AND H1.TEMSILCI=D.TipKod
	WHERE H1.BOLGE='{1}'  
	AND H1.AYYIL='{2}' 
	AND H1.TIP = 2 
    ) AS A
    GROUP BY  A.UrunGrup, A.TipKod, A.NetCiro 
    ";
    }
}