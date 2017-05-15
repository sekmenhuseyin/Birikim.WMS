using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
    public class RiskTanimToplu
    {
        public System.Nullable<bool> Onay { get; set; }

        public string HesapKodu { get; set; }

        public string Unvan { get; set; }

        public decimal SahsiCekLimiti { get; set; }

        public decimal MusteriCekLimiti { get; set; }

        public System.Nullable<decimal> YeniSahsiCekLimiti { get; set; }

        public System.Nullable<decimal> YeniMusteriCekLimiti { get; set; }
    }
}
