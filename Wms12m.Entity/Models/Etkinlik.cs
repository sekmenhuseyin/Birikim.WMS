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
    
    public partial class Etkinlik
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public int TatilTipi { get; set; }
        public System.DateTime Tarih { get; set; }
        public string Aciklama { get; set; }
        public bool Tekrarlayan { get; set; }
        public Nullable<int> Sure { get; set; }
        public bool Onay { get; set; }
    
        public virtual ComboItem_Name ComboItem_Name { get; set; }
    }
}
