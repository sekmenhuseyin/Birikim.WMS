using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
    public class HDF
    {
        public int ID { get; set; }
        public short TIP { get; set; }
        public int BOLGE { get; set; }
        public int? TEMSILCI { get; set; }
        public int? URUNGRUP { get; set; }
        public decimal HEDEF { get; set; }
        public int TARIH { get; set; }
    }
}
