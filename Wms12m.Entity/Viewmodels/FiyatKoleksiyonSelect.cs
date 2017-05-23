using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
   public class FiyatKoleksiyonSelect
    {
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kod4 { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string TipKod { get; set; }
        /// <summary> VarChar(8) (Not Null) </summary>
        public string FiyatListNum { get; set; }
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
        /// <summary> Bit (Allow Null) </summary>
        public bool? Onay { get; set; }
        /// <summary> VarChar(19) (Not Null) </summary>
        public string Durum { get; set; }
    }
}
