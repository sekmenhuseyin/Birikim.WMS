using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
    public class FaturaDetayFTD
    {
        /// <summary> VarChar(64) (Not Null) </summary>
        public string Tip { get; set; }
        /// <summary> VarChar(64) (Not Null) </summary>
        public string Aciklama { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string IskontoOran { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string Iskonto { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string DvzTL { get; set; }
        /// <summary> VarChar(3) (Not Null) </summary>
        public string DovizCinsi { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string DovizKuru { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string DovizTutar { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string KdvMuafiyetNedeni { get; set; }

    }

    public class FaturaDetayGenel
    {
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Chk { get; set; }
        /// <summary> VarChar(81) (Not Null) </summary>
        public string Unvan { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string TeslimYeriKodu { get; set; }
        /// <summary> VarChar(5) (Not Null) </summary>
        public string Kaydeden { get; set; }
        /// <summary> VarChar(5) (Not Null) </summary>
        public string Degistiren { get; set; }
        /// <summary> VarChar(120) (Not Null) </summary>
        public string Aciklama { get; set; }
        /// <summary> VarChar(120) (Not Null) </summary>
        public string Aciklama2 { get; set; }
        /// <summary> VarChar(120) (Not Null) </summary>
        public string Aciklama3 { get; set; }
        /// <summary> VarChar(120) (Not Null) </summary>
        public string Aciklama4 { get; set; }
        /// <summary> VarChar(120) (Not Null) </summary>
        public string Aciklama5 { get; set; }
        /// <summary> VarChar(120) (Not Null) </summary>
        public string Aciklama6 { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string EvrakNo { get; set; }
        /// <summary> VarChar(10) (Allow Null) </summary>
        public string Tarih { get; set; }
        /// <summary> VarChar(100) (Allow Null) </summary>
        public string IslemTip { get; set; }
        /// <summary> VarChar(10) (Allow Null) </summary>
        public string VadeTarih { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string DvzTL { get; set; }
        /// <summary> VarChar(3) (Not Null) </summary>
        public string DovizCinsi { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string DovizKuru { get; set; }
        /// <summary> VarChar(100) (Not Null) </summary>
        public string EFatTip { get; set; }

    }

    public class FaturaDetaySTI
    {
        /// <summary> VarChar(30) (Not Null) </summary>
        public string MalKodu { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string MalAdi { get; set; }
        /// <summary> VarChar(4) (Not Null) </summary>
        public string Birim { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string BirimMiktar { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string BirimFiyat { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string Tutar { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string Depo { get; set; }
        /// <summary> Real (Not Null) </summary>
        public Single KdvOran { get; set; }
        /// <summary> VarChar(100) (Allow Null) </summary>
        public string KdvMuafiyetNedeni { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string DvzTL { get; set; }
        /// <summary> VarChar(3) (Not Null) </summary>
        public string DovizCinsi { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string DovizKuru { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string DovizTutar { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string IskontoOran1 { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string IskontoOran2 { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string IskontoOran3 { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string IskontoOran4 { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string IskontoOran5 { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string NetTutar { get; set; }

    }

    public class FaturaOnay
    {
        /// <summary> VarChar(14) (Allow Null) </summary>
        public string Tarih { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string EvrakNo { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Chk { get; set; }
        /// <summary> VarChar(128) (Not Null) </summary>
        public string Not1 { get; set; }
        /// <summary> VarChar(82) (Allow Null) </summary>
        public string Unvan { get; set; }

    }
}
