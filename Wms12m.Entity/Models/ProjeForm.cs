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
    
    public partial class ProjeForm
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProjeForm()
        {
            this.Gorevlers = new HashSet<Gorevler>();
            this.ProjeForm1 = new HashSet<ProjeForm>();
        }
    
        public int ID { get; set; }
        public int MusteriID { get; set; }
        public string Proje { get; set; }
        public string Form { get; set; }
        public string Sorumlu { get; set; }
        public string KarsiSorumlu { get; set; }
        public string Aciklama { get; set; }
        public bool MesaiKontrol { get; set; }
        public Nullable<int> MesaiKota { get; set; }
        public Nullable<int> PID { get; set; }
        public string Kaydeden { get; set; }
        public System.DateTime KayitTarih { get; set; }
        public string Degistiren { get; set; }
        public System.DateTime DegisTarih { get; set; }
        public string GitAddress { get; set; }
        public string GitGuid { get; set; }
        public bool Aktif { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gorevler> Gorevlers { get; set; }
        public virtual Musteri Musteri { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjeForm> ProjeForm1 { get; set; }
        public virtual ProjeForm ProjeForm2 { get; set; }
    }
}
