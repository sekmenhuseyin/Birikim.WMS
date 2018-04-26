using System;
using KurumsalKaynakPlanlaması12M;

namespace Wms12m.Entity
{
    public class SatTalep
    {
        public string TalepNo { get; set; }
        public string MalKodu { get; set; }
        public DateTime Tarih { get; set; }
        public short Tip { get; set; }
        public string Birim { get; set; }
        public decimal BirimMiktar { get; set; }
        public double? Miktar { get; set; }
        public int? Operator { get; set; }
        public double? Katsayi { get; set; }
        public DateTime IstenenTarih { get; set; }
        public string Aciklama { get; set; }
        public string Aciklama2 { get; set; }
        public string Aciklama3 { get; set; }
        public short Durum { get; set; }
        public string EkDosya { get; set; }
        public string Kademe1Onaylayan { get; set; }
        public DateTime? Kademe1OnayTarih { get; set; }
        public string Kademe2Onaylayan { get; set; }
        public DateTime? Kademe2OnayTarih { get; set; }
        public string Satinalmaci { get; set; }
        public int? TeklifNo { get; set; }
        public string HesapKodu { get; set; }
        public decimal? BirimFiyat { get; set; }
        public short? DvzTL { get; set; }
        public string DvzCinsi { get; set; }
        public decimal? DvzKuru { get; set; }
        public float KDVOran { get; set; }
        public int? SipTalepNo { get; set; }
        public short? SipIslemTip { get; set; }
        public string GMYMaliOnaylayan { get; set; }
        public DateTime? GMYMaliOnayTarih { get; set; }
        public string Kaydeden { get; set; }
        public string TesisKodu { get; set; }
        public string MalAdi { get; set; }
        public short? TeklifVade { get; set; }
        public string TeklifAciklamasi { get; set; }
        public short? FTDDovizTL { get; set; }
        public string FTDDovizCinsi { get; set; }
        public decimal? FTDDovizKuru { get; set; }

        public int ID { get; set; }
        public Nullable<decimal> TeslimMiktar { get; set; }
        public Nullable<decimal> KapatilanMiktar { get; set; }
        public Nullable<decimal> KapatilanMiktar_Onceki { get; set; }

        public string GMOnaylayan { get; set; }
        public Nullable<DateTime> GMOnayTarih { get; set; }
        public string SipEvrakNo { get; set; }
        public Nullable<DateTime> SipTerminTarih { get; set; }
        public string SirketKodu { get; set; }

        public string Unvan { get; set; }

        public DateTime KayitTarih { get; set; }
        public string Degistiren { get; set; }
        public DateTime DegisTarih { get; set; }
        public bool Gizle { get; set; }

        public string TesisAdi { get; set; }
        public string KynkTalepNo { get; set; }

        public string TipStr
        {
            get
            {
                if (Tip == 0) return "MTO";
                else if (Tip == 1) return "MTS";
                return "";
            }
            private set { }
        }
        public string DvzTLStr
        {
            get
            {
                if (DvzTL == 0) return "Yerel Para";
                else if (DvzTL == 1) return "Döviz";
                return "";
            }
            private set { }
        }
        public string SipIslemTipStr
        {
            get
            {
                if (SipIslemTip == (short)KKPIslemTipSPI.İçPiyasa)
                    return "İç Piyasa";
                else if (SipIslemTip == (short)KKPIslemTipSPI.DışPiyasa)
                    return "Dış Piyasa";
                return "";
            }
            private set { }
        }

        public Nullable<decimal> DvzTutar
        {
            get
            {
                if (BirimFiyat == null) return null;

                if (DvzTL == 1)
                    return BirimMiktar * (decimal)BirimFiyat;
                return null;
            }
            private set { }
        }

        public Nullable<decimal> Tutar
        {
            get
            {
                if (BirimFiyat == null) return null;

                if (DvzTL == 0)
                    return BirimMiktar * (decimal)BirimFiyat;
                else if (DvzTL == 1 && DvzKuru != null && DvzKuru > 0 && DvzTutar != null)
                    return (decimal)DvzTutar * (decimal)DvzKuru;
                return null;
            }
            private set { }
        }

        public string TalepEden { get; set; }
        public bool OneriDurum { get; set; }
        public decimal AcikTalepMiktar { get; set; }
        public decimal TeklifMiktar { get; set; }
        public int TerminSure { get; set; }
        public decimal MinSipMiktar { get; set; }
        public Nullable<DateTime> TeklifBasTarih { get; set; }
        public Nullable<DateTime> TeklifBitTarih { get; set; }
        public Nullable<short> Vade { get; set; }
        public string TeslimYeri { get; set; }
        public string TLPKaydeden { get; set; }

        public string DurumStr
        {
            get
            {
                if (Durum == (short)TeklifDurum.Giris)
                    return "Teklif Giriş";
                else if (Durum == (short)TeklifDurum.Fiyat)
                    return "Teklif Fiyat";
                else if (Durum == (short)TeklifDurum.TedarikciSecimiYapildi)
                    return "Tedarikçi Seçimi Yapıldı";
                else if (Durum == (short)TeklifDurum.GMYOrmanElenen)
                    return "GMY Tedarikçi Elenen";
                else if (Durum == (short)TeklifDurum.GMYOrmanOnayli)
                    return "GMY Tedarikçi Onay";
                else if (Durum == (short)TeklifDurum.GMYMaliElenen)
                    return "GMY Mali Elenen";
                else if (Durum == (short)TeklifDurum.GMYMaliOnayli)
                    return "Onaylı";
                return "";
            }
            private set { }
        }
    }
}