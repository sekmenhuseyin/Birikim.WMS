namespace Wms12m.Entity
{
    public class SevkiyatKalanRapor
    {

        /// <summary> VarChar(16) (Not Null) </summary>
        public string EvrakNo { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int Tarih { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int TahTeslimTarih { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Chk { get; set; }
        /// <summary> VarChar(40) (Not Null) </summary>
        public string Unvan { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SiraNo { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string TeslimChk { get; set; }
        /// <summary> VarChar(40) (Not Null) </summary>
        public string TeslimYeriAdi { get; set; }
        /// <summary> VarChar(30) (Not Null) </summary>
        public string MalKodu { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string MalAdi { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal Miktar { get; set; }
        /// <summary> VarChar(4) (Not Null) </summary>
        public string Birim { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal BirimMiktar { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal TeslimMiktar { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal KapatilanMiktar { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string Depo { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? SevkEvrakMiktar { get; set; }
        /// <summary> VarChar(4) (Not Null) </summary>
        public string Birim1 { get; set; }
        /// <summary> VarChar(4) (Not Null) </summary>
        public string Birim2 { get; set; }
        /// <summary> VarChar(4) (Not Null) </summary>
        public string Birim3 { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short Operator2 { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short Operator3 { get; set; }
        /// <summary> Float (Not Null) </summary>
        public double KatSayi2 { get; set; }
        /// <summary> Float (Not Null) </summary>
        public double KatSayi3 { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string GrupKod { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kod16 { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? Birim1StokMiktar { get; set; }
        /// <summary> Decimal(24,6) (Allow Null) </summary>
        public decimal? BTBKatSayi { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int KayitTarih { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int KayitSaat { get; set; }
        /// <summary> VarChar(122) (Not Null) </summary>
        public string SevkEdilecekIlIlce { get; set; }


    }
}
