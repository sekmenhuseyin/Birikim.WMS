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
    
    public partial class ComboItem_Name
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ComboItem_Name()
        {
            this.Gorevs = new HashSet<Gorev>();
            this.Gorevs1 = new HashSet<Gorev>();
            this.Kats = new HashSet<Kat>();
            this.WebMenus = new HashSet<WebMenu>();
            this.WebMenus1 = new HashSet<WebMenu>();
            this.Gorevlers = new HashSet<Gorevler>();
            this.Gorevlers1 = new HashSet<Gorevler>();
            this.Gorevlers2 = new HashSet<Gorevler>();
            this.Gorevlers3 = new HashSet<Gorevler>();
        }
    
        public int ID { get; set; }
        public int ComboID { get; set; }
        public string Name { get; set; }
        public bool Visible { get; set; }
    
        public virtual Combo_Name Combo_Name { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gorev> Gorevs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gorev> Gorevs1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kat> Kats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WebMenu> WebMenus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WebMenu> WebMenus1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gorevler> Gorevlers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gorevler> Gorevlers1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gorevler> Gorevlers2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gorevler> Gorevlers3 { get; set; }
        public virtual GorevPaketler GorevPaketler { get; set; }
    }
}
