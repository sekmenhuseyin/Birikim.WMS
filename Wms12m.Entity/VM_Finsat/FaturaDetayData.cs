using System.Collections.Generic;

namespace Wms12m.Entity
{
    public class FaturaDetayData
    {
        public List<FaturaDetayFTD> FTD { get; set; }
        public List<FaturaDetayGenel> GENEL { get; set; }
        public List<FaturaDetaySTI> STI { get; set; }
    }
}