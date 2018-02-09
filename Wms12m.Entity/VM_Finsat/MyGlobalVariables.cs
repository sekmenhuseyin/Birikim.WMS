﻿using KurumsalKaynakPlanlaması12M;
using System.Collections.Generic;

namespace Wms12m.Entity
{
    public static class MyGlobalVariables
    {
        public static KKPEvrakSiparis SipEvrak { get; set; }
        public static List<MMK> TesisList { get; set; }
        public static List<SatTalep> SipTalepList { get; set; }
        public static List<SatTalep> TalepSource { get; set; }
        public static List<SatTalep> GMYTedarikciOnayList { get; set; }
        public static List<SatTalep> GMYTedarikciOnayDetayList { get; set; }
        public static List<KKP_SPI> GridSource { get; set; }
        public static List<SatTalep> GridTedarikciSource { get; set; }
        public static List<KKP_FTD> GridFTD { get; set; }
        public static bool DovizDurum { get; set; }
        public static string Birim { get; set; }
        public static string Depo { get; set; }
    }
}