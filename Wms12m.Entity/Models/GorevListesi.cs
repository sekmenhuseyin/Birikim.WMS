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
        public int GorevTipiID { get; set; }
        public int DurumID { get; set; }
        public int OlusturanID { get; set; }
        public int OlusturmaTarihi { get; set; }
        public int OlusturmaSaati { get; set; }
        public string Bilgi { get; set; }
        public string Aciklama { get; set; }
        public Nullable<int> IrsaliyeID { get; set; }
        public Nullable<int> GorevliID { get; set; }
        public Nullable<int> AtayanID { get; set; }
        public Nullable<int> AtamaTarihi { get; set; }
        public Nullable<int> BitisTarihi { get; set; }
        public Nullable<int> BitisSaati { get; set; }
    
        public virtual USR01 USR01 { get; set; }
        public virtual USR01 USR011 { get; set; }
        public virtual WMS_IRS WMS_IRS { get; set; }
        public virtual USR01 USR012 { get; set; }
        public virtual TK_DEP TK_DEP { get; set; }
        public virtual ComboItemName ComboItemName { get; set; }
        public virtual ComboItemName ComboItemName1 { get; set; }
    }
}
