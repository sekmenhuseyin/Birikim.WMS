namespace Wms12m.Entity
{
    public class Tip_STI
    {
        public int ID { get; set; }
        public int irsID { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public string Barkod { get; set; }
        public int SiraNo { get; set; }
        public string Kaydeden { get; set; }
        public bool AktarimDurumu { get; set; }
        public decimal OkutulanMiktar { get; set; }
        public decimal YerlestirmeMiktari { get; set; }
        public decimal? YerMiktar { get; set; }
        public string Raf { get; set; }
    }

    public class STIMax
    {
        public string SirketKod { get; set; }
        public int IrsaliyeID { get; set; }
        public string Chk { get; set; }
        public string EvrakNo { get; set; }
        public string HesapKodu { get; set; }
        public string TeslimChk { get; set; }
        public string DepoKodu { get; set; }
        public int Tarih { get; set; }
        public short ValorGun { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public decimal OkutulanMiktar { get; set; }
        public string SiparisNo { get; set; }
        public short KynkSiparisSiraNo { get; set; }
        public int KynkSiparisTarih { get; set; }
        public decimal KynkSiparisMiktar { get; set; }
        public decimal TeslimMiktar { get; set; }
        public decimal KapatilanMiktar { get; set; }
        public decimal KDV { get; set; }
        public string FytListeNo { get; set; }
        public decimal ToplamIskonto { get; set; }
        public float? IskontoOran5 { get; set; }
        public float? IskontoOran4 { get; set; }
        public float? IskontoOran3 { get; set; }
        public float? IskontoOran2 { get; set; }
        public float? IskontoOran1 { get; set; }
        public float? KdvOran { get; set; }
        public decimal? Fiyat { get; set; }
    }


    public class Tip_IRS
    {
        public int ID { get; set; }
        public string EvrakNo { get; set; }
        public string HesapKodu { get; set; }
        public string TeslimCHK { get; set; }
        public string DepoID { get; set; }
        public string Kaydeden { get; set; }
        public string Tarih { get; set; }
        public string SirketKod { get; set; }
        public string Unvan { get; set; }
    }

    public class Tip_GOREV
    {
        public int ID { get; set; }
        public int IrsaliyeID { get; set; }
        public string EvrakNo { get; set; }
        public string Bilgi { get; set; }
        public string Atayan { get; set; }
        public string DepoID { get; set; }
        public string OlusturmaTarihi { get; set; }
        public string Gorevli { get; set; }
        public string Aciklama { get; set; }
        public string GorevNo { get; set; }
        public string Durum { get; set; }
    }
    public class Tip_Malzeme
    {
        public string MalAdi { get; set; }
        public string Birim { get; set; }
    }


}
