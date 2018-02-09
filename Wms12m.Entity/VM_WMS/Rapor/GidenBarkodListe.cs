using System;

namespace Wms12m.Entity
{
    public class GidenBarkodListe
    {
        public int ID { get; set; }
        /// <summary> VarChar(2) (Not Null) </summary>
        public string SirketKodu { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string SevkEvrakNo { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string IrsaliyeNo { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string SiparisNo { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Chk { get; set; }
        /// <summary> VarChar(80) (Not Null) </summary>
        public string Unvan { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SipSiraNo { get; set; }
        /// <summary> VarChar(30) (Not Null) </summary>
        public string MalKodu { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string MalAdi { get; set; }
        /// <summary> VarChar(100) (Not Null) </summary>
        public string BarkodNo { get; set; }
        /// <summary> Decimal(24,6) (Not Null) </summary>
        public decimal BarkodMiktar { get; set; }
        /// <summary> VarChar(4) (Not Null) </summary>
        public string Birim { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string Depo { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short IslemTip { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short AktarimDurum { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string Kaydeden { get; set; }
        /// <summary> Datetime (Not Null) </summary>
        public DateTime KayitTarih { get; set; }

    }
}
