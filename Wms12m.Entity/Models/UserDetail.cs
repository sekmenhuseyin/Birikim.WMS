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
        public int AlimIrsaliyeSeri { get; set; }
        public int SatisIrsaliyeSeri { get; set; }
        public int SatisFaturaSeri { get; set; }
        public int TransferInSeri { get; set; }
        public int TransferOutSeri { get; set; }
    
        public virtual User User { get; set; }
        public virtual Depo Depo { get; set; }
    }
}
