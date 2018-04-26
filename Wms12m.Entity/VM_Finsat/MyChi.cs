using System;
using System.Collections.Generic;

namespace Wms12m.Entity
{
    public class MyChi
    {
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public string EvrakNo { get; set; }
        public DateTime Tarih { get; set; }
        public string EvrakNo2 { get; set; }
        public decimal Kod13 { get; set; }
        public decimal Kod14 { get; set; }
        public decimal topSterIbre { get; set; }
        public decimal topSterYaprak { get; set; }
        public decimal topM3Ibre { get; set; }
        public decimal topM3Yaprak { get; set; }

        public override string ToString() => Unvan;

        public List<MySti> FaturaDetay { get; set; }
    }
}