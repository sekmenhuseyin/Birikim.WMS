using KurumsalKaynakPlanlaması12M;
using System.Collections.Generic;

namespace Wms12m.Entity
{
    public static class MyGlobalVariables
    {
        public static KKPEvrakSiparis SipEvrak { get; set; }
        public static List<MMK> TesisList { get; set; }
        public static List<SatTalep> SipTalepList { get; set; }
        public static List<SatTalep> TalepSource { get; set; }
        public static List<KKP_SPI> GridSource { get; set; }
        public static List<KKP_FTD> GridFTD { get; set; }
        public static bool DovizDurum { get; set; }
        public static string Birim { get; set; }
        public static string Depo { get; set; }

        public static KKPEvrakSiparis SipEvrakGMY { get; set; }
        public static List<SatTalep> GMYOnayList { get; set; }
        public static List<SatTalep> GMYOnayDetayList { get; set; }
        public static List<SatTalep> SatSipGMYMaliOnayList { get; set; }
        public static List<SatTalep> SatSipGMYMaliOnayDetayList { get; set; }
        public static List<KKP_FTD> GridGMYFTD { get; set; }
        public static List<SatTalep> GMYSource { get; set; }

        public enum GMYOnayTip
        {
            GMYTedarikciOnay,
            GMYMaliOnay
        }
    }
}