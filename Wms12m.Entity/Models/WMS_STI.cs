//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wms12m.Entity.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class WMS_STI
    {
        public int ID { get; set; }
        public int IrsaliyeID { get; set; }
        public string MalKodu { get; set; }
        public string Birim { get; set; }
        public decimal Miktar { get; set; }
        public Nullable<decimal> MiktarReal { get; set; }
        public string MalAdi { get; set; }
    
        public virtual WMS_IRS WMS_IRS { get; set; }
    }
}
