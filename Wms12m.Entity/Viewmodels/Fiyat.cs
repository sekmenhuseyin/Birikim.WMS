using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
   public class Fiyat
    {
        /// <summary> Int (Not Null) </summary>
        public int ID { get; set; }
        /// <summary> VarChar(8) (Not Null) </summary>
        public string FiyatListNum { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string MalKodu { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string HesapKodu { get; set; }
        /// <summary> Decimal(24,6) (Not Null) </summary>
        public decimal SatisFiyat1 { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string SatisFiyat1Birim { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int SatisFiyat1BirimInt { get; set; }
        /// <summary> Decimal(24,6) (Not Null) </summary>
        public decimal DovizSatisFiyat1 { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string DovizSF1Birim { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int DovizSF1BirimInt { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string DovizCinsi { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int ROW_ID { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short Durum { get; set; }
        /// <summary> Bit (Not Null) </summary>
        public bool Onay { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string Onaylayan { get; set; }
        /// <summary> SmallDatetime (Allow Null) </summary>
        public DateTime? SMOnayTarih { get; set; }
        /// <summary> Bit (Not Null) </summary>
        public bool SPGMYOnay { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string SPGMYOnaylayan { get; set; }
        /// <summary> SmallDatetime (Allow Null) </summary>
        public DateTime? SPGMYOnayTarih { get; set; }
        /// <summary> Bit (Not Null) </summary>
        public bool GMOnay { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string GMOnaylayan { get; set; }
        /// <summary> SmallDatetime (Allow Null) </summary>
        public DateTime? GMOnayTarih { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int BasTarih { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int BitTarih { get; set; }
    }
}
