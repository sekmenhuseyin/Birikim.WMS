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
    public partial class GorevCalisma
    {
        public int ID { get; set; }
        public int GorevID { get; set; }
        public System.DateTime Tarih { get; set; }
        public int CalismaSure { get; set; }
        public string Calisma { get; set; }
        public string ToDoListID { get; set; }
        public string Kaydeden { get; set; }
        public System.DateTime KayitTarih { get; set; }
        public string Degistiren { get; set; }
        public System.DateTime DegisTarih { get; set; }
        public string[] work { get; set; }
        public string[] checkitem { get; set; }
        public virtual Gorevler Gorevler { get; set; }
    }
}
