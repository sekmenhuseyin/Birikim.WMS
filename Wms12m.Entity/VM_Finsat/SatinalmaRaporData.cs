using System.Collections.Generic;
using KurumsalKaynakPlanlaması12M;

namespace Wms12m.Entity
{
    public class SatinalmaRaporData
    {
        public List<RaporFTD> FTDD { get; set; }
        public List<KKP_CHK> CHKK { get; set; }
        public List<RaporSiparis> SPII { get; set; }
        public List<SatTalep> TLP { get; set; }
    }
}