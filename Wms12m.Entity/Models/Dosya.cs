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
    
    public partial class Dosya
    {
        public int ID { get; set; }
        public System.Guid Guid { get; set; }
        public Nullable<int> GorevID { get; set; }
        public Nullable<int> ProjeID { get; set; }
        public string DosyaAdi { get; set; }
        public string Boyut { get; set; }
        public int BoyutByte { get; set; }
        public string Kaydeden { get; set; }
        public System.DateTime Tarih { get; set; }
    
        public virtual Gorevler Gorevler { get; set; }
        public virtual ProjeForm ProjeForm { get; set; }
    }
}
