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
    
    public partial class UserDetail
    {
        public int UserID { get; set; }
        public int DepoID { get; set; }
        public Nullable<int> SatisIrsaliyeSeri { get; set; }
        public Nullable<int> SatisFaturaSeri { get; set; }
        public Nullable<int> TransferInSeri { get; set; }
        public Nullable<int> TransferOutSeri { get; set; }
        public Nullable<int> SayimSeri { get; set; }
        public string GosterilecekSirket { get; set; }
        public string GosterilecekDepo { get; set; }
        public bool PasifKartlariGoster { get; set; }
        public string GostCHKKodAlani { get; set; }
        public string GostCHKDeger { get; set; }
        public string GostSTKKodAlani { get; set; }
        public string GostSTKDeger { get; set; }
    
        public virtual User User { get; set; }
        public virtual Depo Depo { get; set; }
    }
}
