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
    
    public partial class CRM_GorusmeNotlari_Result
    {
        public int ID { get; set; }
        public string GorusenKisi { get; set; }
        public string GorusulenKurum { get; set; }
        public string GorusulenKisi { get; set; }
        public Nullable<System.DateTime> GorusmeTarihi { get; set; }
        public Nullable<System.DateTime> YenidenGorusmeTarihi { get; set; }
        public string GorusmeBicimi { get; set; }
        public string GorusmeNedeni { get; set; }
        public string GorusmeNotuDurumu { get; set; }
        public string Kaydeden { get; set; }
        public System.DateTime KayitTarih { get; set; }
    }
}
