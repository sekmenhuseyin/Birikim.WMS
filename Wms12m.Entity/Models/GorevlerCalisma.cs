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
    
    public partial class GorevlerCalisma
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GorevlerCalisma()
        {
            this.GorevlerCalismaToDoLists = new HashSet<GorevlerCalismaToDoList>();
        }
    
        public int ID { get; set; }
        public int GorevID { get; set; }
        public System.DateTime Tarih { get; set; }
        public int Sure { get; set; }
        public string Calisma { get; set; }
        public string Kaydeden { get; set; }
        public System.DateTime KayitTarih { get; set; }
        public string Degistiren { get; set; }
        public System.DateTime DegisTarih { get; set; }
    
        public virtual Gorevler Gorevler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GorevlerCalismaToDoList> GorevlerCalismaToDoLists { get; set; }
    }
}