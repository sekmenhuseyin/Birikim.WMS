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
    
    public partial class Gorevler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gorevler()
        {
            this.GorevCalismas = new HashSet<GorevCalisma>();
            this.GorevToDoLists = new HashSet<GorevToDoList>();
        }
    
        public int ID { get; set; }
        public int ProjeFormID { get; set; }
        public string Sorumlu { get; set; }
        public string Sorumlu2 { get; set; }
        public string Sorumlu3 { get; set; }
        public string KaliteKontrol { get; set; }
        public string Gorev { get; set; }
        public string Aciklama { get; set; }
        public int OncelikID { get; set; }
        public int DurumID { get; set; }
        public Nullable<int> GorevTipiID { get; set; }
        public Nullable<int> DepartmanID { get; set; }
        public Nullable<System.DateTime> TahminiBitis { get; set; }
        public Nullable<System.DateTime> BitisTarih { get; set; }
        public Nullable<short> IslemTip { get; set; }
        public Nullable<int> IslemSira { get; set; }
        public string Kaydeden { get; set; }
        public System.DateTime KayitTarih { get; set; }
        public string Degistiren { get; set; }
        public System.DateTime DegisTarih { get; set; }
        public bool Kontrol { get; set; }
        public bool AktifPasif { get; set; }
    
        public virtual ComboItem_Name ComboItem_Name { get; set; }
        public virtual ComboItem_Name ComboItem_Name1 { get; set; }
        public virtual ComboItem_Name ComboItem_Name2 { get; set; }
        public virtual ComboItem_Name ComboItem_Name3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GorevCalisma> GorevCalismas { get; set; }
        public virtual ProjeForm ProjeForm { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GorevToDoList> GorevToDoLists { get; set; }
    }
}
