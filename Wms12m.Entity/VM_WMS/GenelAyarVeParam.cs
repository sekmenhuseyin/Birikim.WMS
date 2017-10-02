using KurumsalKaynakPlanlaması12M;
using System.Collections.Generic;
using Wms12m.Entity;

namespace Wms12m
{
    public class GenelAyarVeParam
    {
        public int ID { get; set; }

        public short Tip { get; set; }

        public short Tip2 { get; set; }

        public string TalebiGiren { get; set; }

        public string Talep2KademeOnaylayan { get; set; }

        public string Talep1KademeOnaylayan { get; set; }

        public string SiparisSorumlu { get; set; }

        public byte[] SiparisSorumluImza { get; set; }

        public System.Nullable<decimal> SiparisGMYOnayLimit { get; set; }

        public string Satinalmaci { get; set; }

        public System.Nullable<bool> SatinalmaciAdmin { get; set; }

        public string MailExp { get; set; }

        public string AccountName { get; set; }

        public string AccountPass { get; set; }

        public string MailServer { get; set; }

        public System.Nullable<bool> EnableSSL { get; set; }

        public System.Nullable<int> SmtpPort { get; set; }

        public string MailTo { get; set; }

        public string MailCc { get; set; }

        public System.Nullable<int> OngoruStokAsgariFaktor { get; set; }

        public System.Nullable<int> OngoruStokAzamiFaktor { get; set; }

        public string OzelParca { get; set; }

        public string DosyaTipi { get; set; }

        public string DosyaYolu { get; set; }

        public string Kaydeden { get; set; }

        public System.DateTime KayitTarih { get; set; }

        public string Degistiren { get; set; }

        public System.DateTime DegisTarih { get; set; }

        public string TipAck { get; set; }

        public string Tip2Ack { get; set; }
    }


    public class RaporFTD
    {
        public string DovizCinsi { get; set; }

        public short SatirTip { get; set; }

        public decimal Tutar { get; set; }
    }

    public class RaporCHK
    {
        public string HesapKodu { get; set; }

        public string Unvan1 { get; set; }

        public string FaturaAdres1 { get; set; }

        public string FaturaAdres2 { get; set; }

        public string FaturaAdres3 { get; set; }

        public string Telefon1 { get; set; }

        public string Fax1 { get; set; }

        public string SirketEMail { get; set; }

        public string SatAlmaIslemEMail { get; set; }
    }

    public class RaporSiparis
    {
        public int Tarih { get; set; }

        public string MalKodu { get; set; }

        public string MalAdi { get; set; }

        public string DovizCinsi { get; set; }

        public string EkDosya { get; set; }

        public string Aciklama { get; set; }

        public decimal BirimMiktar { get; set; }

        public decimal BirimFiyat { get; set; }

        public decimal Tutar { get; set; }

        public short DvzTL { get; set; }

        public decimal DovizTutar { get; set; }

        public string Birim { get; set; }

        public short Vade { get; set; }

        public string TeslimYeri { get; set; }

        public short IslemTip { get; set; }
    }


    public class SatinalmaRaporData
    {
        public List<RaporFTD> FTDD { get; set; }
        public List<KKP_CHK> CHKK { get; set; }
        public List<RaporSiparis> SPII { get; set; }
        public List<SatTalep> TLP { get; set; }
    }
}
