using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WMSMobil
{
    class PanelEx : Panel
    {
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public string MakaraNo { get; set; }
        public string Barkod { get; set; }
        public int SiraNo { get; set; }
        public decimal OkutulanMiktar { get; set; }
        public string Raf { get; set; }
        public decimal YerlestirmeMiktari { get; set; }
        public decimal IslemMiktar { get; set; }
        public decimal SetMamulMiktar { get; set; }
    }
    class PanelIrs : Panel
    {
        public string EvrakNo { get; set; }
        public string HesapKodu { get; set; }
        public string TeslimYeri { get; set; }
        public int Depo { get; set; }
        public int ID { get; set; }
        public string Kaydeden { get; set; }
        public int IrsaliyeTarihi { get; set; }
        public string SirketKodu { get; set; }
        public string Unvan { get; set; }
    }

    class PanelGrv : Panel
    {
        public int ID { get; set; }
        public int IrsaliyeID { get; set; }
        public string DepoID { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public string Atayan { get; set; }
        public string Gorevli { get; set; }
        public string Bilgi { get; set; }
    }
    class IRS_Detay
    {
        public int ID { get; set; }
        public int IrsaliyeID { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public string KynkSiparisNo { get; set; }
        public Nullable<short> KynkSiparisSiraNo { get; set; }
        public Nullable<int> KynkSiparisTarih { get; set; }
        public Nullable<decimal> KynkSiparisMiktar { get; set; }
        public Nullable<decimal> OkutulanMiktar { get; set; }
        public Nullable<decimal> YerlestirmeMiktari { get; set; }
    }    
}
