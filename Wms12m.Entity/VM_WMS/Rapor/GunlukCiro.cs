using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
    public class GunlukCiro
    {
        public string EVRAKNO { get; set; }
        public decimal? Tutar { get; set; }
        public string Tarih { get; set; }
        public string CHK { get; set; }
        public string Unvan { get; set; }
        public string TipKod { get; set; }
        public string GrupKod { get; set; }
        public static string Sorgu = @"
                                    SELECT 
                                    STI.EVRAKNO,
                                    (SUM(STI.Tutar)-SUM(STI.IskontoTutar1)) AS Tutar,
                                    CONVERT(NVARCHAR(10),CONVERT(DATETIME,STI.Tarih-2),104) AS Tarih,
                                    STI.CHK,
                                    CONCAT(CHK.Unvan1,SPACE(1),CHK.Unvan1) AS Unvan,
                                    CHK.TipKod,
                                    CHK.GrupKod
                                    FROM FINSAT6{0}.FINSAT6{0}.STI AS STI WITH (NOLOCK)
                                    INNER JOIN FINSAT6{0}.FINSAT6{0}.STK AS STK WITH (NOLOCK) ON STK.MALKODU = STI.MALKODU 
                                    INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK WITH (NOLOCK) ON CHK.HesapKodu = STI.CHK
                                    WHERE STI.KynkEvrakTip IN (1,2,163)
                                    AND (STI.Tarih BETWEEN {1} AND {2})
                                    GROUP BY STI.EvrakNo,STI.Tarih,STI.Chk,CONCAT(CHK.Unvan1,SPACE(1),CHK.Unvan1),CHK.TipKod,CHK.GrupKod";
    }
}
