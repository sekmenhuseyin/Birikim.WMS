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
    
    public partial class IR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IR()
        {
            this.STIs = new HashSet<STI>();
        }
    
        public int ID { get; set; }
        public int DepoID { get; set; }
        public short IslemTur { get; set; }
        public string EvrakNo { get; set; }
        public string HesapKodu { get; set; }
        public string TeslimCHK { get; set; }
        public int Tarih { get; set; }
        public string Kaydeden { get; set; }
        public int KayitTarih { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STI> STIs { get; set; }
        public virtual TK_DEP TK_DEP { get; set; }
    }
}
