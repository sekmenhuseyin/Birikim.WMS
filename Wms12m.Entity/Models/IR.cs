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
            this.Gorevs = new HashSet<Gorev>();
            this.Yer_Log = new HashSet<Yer_Log>();
            this.Gorevs1 = new HashSet<Gorev>();
            this.IRS_Detay = new HashSet<IRS_Detay>();
        }
    
        public int ID { get; set; }
        public string SirketKod { get; set; }
        public int DepoID { get; set; }
        public bool IslemTur { get; set; }
        public string EvrakNo { get; set; }
        public string HesapKodu { get; set; }
        public string TeslimCHK { get; set; }
        public int Tarih { get; set; }
        public int Kaydeden { get; set; }
        public int KayitTarih { get; set; }
        public bool Onay { get; set; }
        public string Unvan { get; set; }
        public string LinkEvrakNo { get; set; }
    
        public virtual Depo Depo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gorev> Gorevs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yer_Log> Yer_Log { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gorev> Gorevs1 { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IRS_Detay> IRS_Detay { get; set; }
    }
}
