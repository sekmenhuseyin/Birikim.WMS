using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
    public class RaporHdfList
    {
        /// <summary> Int (Not Null) </summary>
        public int ID { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short TIP { get; set; }
        /// <summary> VarChar(20) (Allow Null) </summary>
        public string BOLGE { get; set; }
        /// <summary> VarChar(20) (Allow Null) </summary>
        public string TEMSILCI { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string URUNGRUP { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal HEDEF { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int TARIH { get; set; }


        public static string Sorgu = @"
        SELECT H1.ID,H1.TIP,BLG.Grupkod AS BOLGE,TEM.TipKod AS TEMSILCI,URN.UrunGrup AS URUNGRUP,H1.HEDEF,H1.TARIH from FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK) 
        LEFT JOIN (
        SELECT DISTINCT(CHK.Grupkod) AS Grupkod,MIN(CHK.ROW_ID) AS RowID FROM FINSAT6{0}.FINSAT6{0}.CHK(NOLOCK) AS CHK 
        WHERE CHK.KartTip IN (0, 4) AND (CHK.HesapKodu BETWEEN '1' AND '8') AND CHK.Grupkod <> ' '
        GROUP BY CHK.GrupKod
        ) AS BLG ON H1.BOLGE=BLG.RowID
        LEFT JOIN (
        SELECT DISTINCT(CHK.Grupkod) AS Grupkod,CHK.Tipkod AS TipKod,MIN(CHK.ROW_ID) AS RowID FROM FINSAT6{0}.FINSAT6{0}.CHK AS CHK (NOLOCK) 
        WHERE CHK.KartTip IN (0,4) AND (CHK.HesapKodu BETWEEN '1' and '8') AND CHK.Grupkod <>' ' AND CHK.Tipkod <>' ' 
        GROUP BY CHK.GrupKod,CHK.TipKod
        ) AS TEM ON H1.TEMSILCI=TEM.RowID
        LEFT JOIN (
        SELECT  DISTINCT(A.MalAdi4) AS UrunGrup,MIN(A.ROW_ID) AS RowID 
        FROM FINSAT6{0}.FINSAT6{0}.STK AS A (NOLOCK) WHERE A.MalAdi4 <>' '
        GROUP BY A.MalAdi4
        ) AS URN ON H1.URUNGRUP=URN.RowID
        WHERE H1.TIP=1
        ";

    }
    public class RaporBolgeHdfList
    {
        /// <summary> Int (Not Null) </summary>
        public int ID { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short TIP { get; set; }
        /// <summary> VarChar(20) (Allow Null) </summary>
        public string BOLGE { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal HEDEF { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int TARIH { get; set; }

        public static string Sorgu = @"
        SELECT H1.ID,H1.TIP,BLG.Grupkod AS BOLGE,H1.HEDEF,H1.TARIH
        FROM FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK)
        LEFT JOIN (
        SELECT DISTINCT(CHK.Grupkod) AS Grupkod,MIN(CHK.ROW_ID) AS RowID FROM FINSAT6{0}.FINSAT6{0}.CHK(NOLOCK) AS CHK 
        WHERE CHK.KartTip IN (0, 4) AND (CHK.HesapKodu BETWEEN '1' AND '8') AND CHK.Grupkod <> ' '
        GROUP BY CHK.GrupKod
        ) AS BLG ON H1.BOLGE=BLG.RowID
        WHERE H1.TIP=0 ";
    }
}
