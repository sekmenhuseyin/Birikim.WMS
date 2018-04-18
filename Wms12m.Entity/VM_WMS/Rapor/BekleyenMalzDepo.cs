using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
    public class BekleyenMalzDepo
    {
        public string Depo { get; set; }
        public decimal Stok { get; set; }
        public static string Sorgu = @"SELECT D.Depo,D.Stok FROM (
                                        SELECT DST.Depo,(DST.DvrMiktar+DST.GirMiktar-DST.CikMiktar) AS Stok 
                                        FROM FINSAT6{0}.FINSAT6{0}.DST AS DST WITH (NOLOCK)
                                        WHERE DST.MalKodu='{1}'
                                        ) AS D WHERE D.Stok>0";
    }
}
