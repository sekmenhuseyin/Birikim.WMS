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
    using System.ComponentModel.DataAnnotations;

    public partial class GorevCalisma
    {
        public int ID { get; set; }
        public int GorevID { get; set; }
        public System.DateTime Tarih { get; set; }
        [Range(0, 540)]
        [Required(ErrorMessage = "�al��ma S�resi 0 ve 540 aras� olmal�d�r.")]
        public int CalismaSure { get; set; }
        public string Calisma { get; set; }
        public string Kaydeden { get; set; }
        public System.DateTime KayitTarih { get; set; }
        public string Degistiren { get; set; }
        public System.DateTime DegisTarih { get; set; }
    
        public virtual Gorevler Gorevler { get; set; }
    }
}
