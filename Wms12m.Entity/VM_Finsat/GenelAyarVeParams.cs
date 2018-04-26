namespace Wms12m.Entity
{
    public class GenelAyarVeParams
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
}