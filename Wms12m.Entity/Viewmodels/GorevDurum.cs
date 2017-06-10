using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
    public class GorevDurum
    {
        /// <summary> Int (Not Null) </summary>
        public int ID { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int ProjeFormID { get; set; }
        /// <summary> VarChar(5) (Allow Null) </summary>
        public string Sorumlu { get; set; }
        /// <summary> VarChar(5) (Allow Null) </summary>
        public string Sorumlu2 { get; set; }
        /// <summary> VarChar(5) (Allow Null) </summary>
        public string Sorumlu3 { get; set; }
        /// <summary> VarChar(100) (Not Null) </summary>
        public string Gorev { get; set; }
        /// <summary> NVarChar(Max) (Not Null) </summary>
        public string Aciklama { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int OncelikID { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int DurumID { get; set; }
        /// <summary> Int (Allow Null) </summary>
        public int? GorevTipiID { get; set; }
        /// <summary> Int (Allow Null) </summary>
        public int? DepartmanID { get; set; }
        /// <summary> SmallDatetime (Allow Null) </summary>
        public DateTime? TahminiBitis { get; set; }
        /// <summary> SmallDatetime (Allow Null) </summary>
        public DateTime? BitisTarih { get; set; }
        /// <summary> SmallInt (Allow Null) </summary>
        public short? IslemTip { get; set; }
        /// <summary> Int (Allow Null) </summary>
        public int? IslemSira { get; set; }
        /// <summary> VarChar(5) (Not Null) </summary>
        public string Kaydeden { get; set; }
        /// <summary> SmallDatetime (Not Null) </summary>
        public DateTime KayitTarih { get; set; }
        /// <summary> VarChar(5) (Not Null) </summary>
        public string Degistiren { get; set; }
        /// <summary> SmallDatetime (Not Null) </summary>
        public DateTime DegisTarih { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string Durum { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string Oncelik { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string GorevTipi { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string Departman { get; set; }
    }
}
