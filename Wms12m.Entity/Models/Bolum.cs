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
    
    public partial class Bolum
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bolum()
        {
            this.Kats = new HashSet<Kat>();
        }
    
        public int ID { get; set; }
        public int RafID { get; set; }
        public string BolumAd { get; set; }
        public int SiraNo { get; set; }
        public bool Aktif { get; set; }
        public int Kaydeden { get; set; }
        public int KayitTarih { get; set; }
        public int Degistiren { get; set; }
        public int DegisTarih { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual Raf Raf { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kat> Kats { get; set; }
    }
}
