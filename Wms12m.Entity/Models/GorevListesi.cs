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
    
    public partial class GorevListesi
    {
        public int ID { get; set; }
        public int DepoID { get; set; }
        public string GorevNo { get; set; }
        public byte GorevTipi { get; set; }
        public byte Durum { get; set; }
        public int AtayanID { get; set; }
        public int OlusturmaTarihi { get; set; }
        public Nullable<int> IrsaliyeID { get; set; }
        public Nullable<int> GorevliID { get; set; }
        public Nullable<int> AtamaTarihi { get; set; }
        public Nullable<int> BitirmeTarihi { get; set; }
        public string Bilgi { get; set; }
        public string Aciklama { get; set; }
    
        public virtual IR IR { get; set; }
        public virtual TK_DEP TK_DEP { get; set; }
    }
}
