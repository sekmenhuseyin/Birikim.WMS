using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
    public class BekleyenMalzDepo
    {
        public decimal ADN { get; set; }
        public decimal ADR { get; set; }
        public decimal ANK { get; set; }
        public decimal ANR { get; set; }
        public decimal BOS { get; set; }
        public decimal BOŞ { get; set; }
        public decimal BRZ { get; set; }
        public decimal BUR { get; set; }
        public decimal CRL { get; set; }
        public decimal CRZ { get; set; }
        public decimal DDL { get; set; }
        public decimal DDR { get; set; }
        public decimal GBR { get; set; }
        public decimal GBZ { get; set; }
        public decimal GOP { get; set; }
        public decimal HDK { get; set; }
        public decimal HDK1 { get; set; }
        public decimal HDR { get; set; }
        public decimal HIR { get; set; }
        public decimal IGP { get; set; }
        public decimal IGR { get; set; }
        public decimal IKR { get; set; }
        public decimal INT { get; set; }
        public decimal IPR { get; set; }
        public decimal ITR { get; set; }
        public decimal IZG { get; set; }
        public decimal IZR { get; set; }
        public decimal PHD { get; set; }
        public decimal PHLP { get; set; }
        public decimal PRP { get; set; }
        public decimal TMP { get; set; }
        public decimal YOL { get; set; }
        public decimal ZMR { get; set; }
        public decimal ZMR2 { get; set; }
        public static string Sorgu = @"EXEC FINSAT6{0}.wms.RP_MalzemeDepo @MalKodu,@SırketKodu";
    }
}
