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
    
    public partial class Yer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Yer()
        {
            this.GorevYers = new HashSet<GorevYer>();
        }
    
        public int ID { get; set; }
        public int KatID { get; set; }
        public string HucreAd { get; set; }
        public string MalKodu { get; set; }
        public string Birim { get; set; }
        public decimal Miktar { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GorevYer> GorevYers { get; set; }
        public virtual Kat Kat { get; set; }
    }
}
