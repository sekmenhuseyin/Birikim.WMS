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
    
    public partial class Gorev
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gorev()
        {
            this.GorevYers = new HashSet<GorevYer>();
            this.Transfers = new HashSet<Transfer>();
            this.IRS = new HashSet<IR>();
        }
    
        public int ID { get; set; }
        public int DepoID { get; set; }
        public string GorevNo { get; set; }
        public int GorevTipiID { get; set; }
        public int DurumID { get; set; }
        public string Olusturan { get; set; }
        public int OlusturmaTarihi { get; set; }
        public int OlusturmaSaati { get; set; }
        public string Bilgi { get; set; }
        public string Aciklama { get; set; }
        public Nullable<int> IrsaliyeID { get; set; }
        public string Gorevli { get; set; }
        public string Atayan { get; set; }
        public Nullable<int> AtamaTarihi { get; set; }
        public Nullable<int> BitisTarihi { get; set; }
        public Nullable<int> BitisSaati { get; set; }
    
        public virtual Depo Depo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GorevYer> GorevYers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transfer> Transfers { get; set; }
        public virtual IR IR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IR> IRS { get; set; }
        public virtual ComboItem_Name ComboItem_Name { get; set; }
        public virtual ComboItem_Name ComboItem_Name1 { get; set; }
    }
}
