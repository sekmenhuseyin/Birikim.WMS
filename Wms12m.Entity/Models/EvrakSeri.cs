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
    
    public partial class EvrakSeri
    {
        public int ID { get; set; }
        public string SirketKodu { get; set; }
        public int Tip { get; set; }
        public string SeriNo { get; set; }
    
        public virtual ComboItem_Name ComboItem_Name { get; set; }
    }
}