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
    
    public partial class Message
    {
        public int ID { get; set; }
        public int MesajTipi { get; set; }
        public System.DateTime Tarih { get; set; }
        public string Kimden { get; set; }
        public string Kime { get; set; }
        public string Mesaj { get; set; }
        public string URL { get; set; }
        public bool Goruldu { get; set; }
        public bool Okundu { get; set; }
        public bool GonderenSildi { get; set; }
        public bool AliciSildi { get; set; }
    
        public virtual ComboItem_Name ComboItem_Name { get; set; }
    }
}
