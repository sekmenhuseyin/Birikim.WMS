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
    
    public partial class GorevlerToDoList
    {
        public int ID { get; set; }
        public int GorevID { get; set; }
        public string Aciklama { get; set; }
        public bool Onay { get; set; }
        public bool KontrolOnay { get; set; }
        public string Kaydeden { get; set; }
        public System.DateTime KayitTarih { get; set; }
        public string Degistiren { get; set; }
        public System.DateTime DegisTarih { get; set; }
    
        public virtual Gorevler Gorevler { get; set; }
    }
}