using System;

namespace Wms12m.Entity
{
    public class RaporCariEkstre
    {
        public int ID { get; set; }
        public string EvrakNo { get; set; }
        public DateTime? Tarih { get; set; }
        public string IslemTip { get; set; }
        public DateTime? VadeTarih { get; set; }
        public decimal? Borc { get; set; }
        public decimal? Alacak { get; set; }
        public decimal? BorcBakiye { get; set; }
        public decimal? AlacakBakiye { get; set; }
    }
}
