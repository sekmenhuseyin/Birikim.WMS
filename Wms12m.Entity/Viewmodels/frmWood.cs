using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
    public class TahsisOnayOdun { 
    /// <summary> Int (Not Null) </summary>
    public int ID { get; set; }
    /// <summary> SmallInt (Not Null) </summary>
    public short Tip { get; set; }
    /// <summary> Int (Not Null) </summary>
    public int Yil { get; set; }
    /// <summary> Int (Not Null) </summary>
    public int Hafta { get; set; }
    /// <summary> Int (Not Null) </summary>
    public int Tarih { get; set; }
    /// <summary> SmallInt (Not Null) </summary>
    public short SiraNo { get; set; }
    /// <summary> VarChar(20) (Not Null) </summary>
    public string OrmIslt { get; set; }
    /// <summary> VarChar(18) (Allow Null) </summary>
    public string TipAck { get; set; }
    /// <summary> VarChar(30) (Allow Null) </summary>
    public string EmvalCinsi { get; set; }
    /// <summary> Decimal(18,2) (Not Null) </summary>
    public decimal Miktar { get; set; }
    /// <summary> VarChar(4) (Allow Null) </summary>
    public string Birim { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? MuhBdl1 { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? MuhBdl2 { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? IbreliMiktarSter { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? IbreliMiktarM3 { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? YaprakliMiktarSter { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? YaprakliMiktarM3 { get; set; }
    /// <summary> VarChar(100) (Allow Null) </summary>
    public string NrdKull { get; set; }
    /// <summary> VarChar(100) (Allow Null) </summary>
    public string StkDrm { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? NakFiyati { get; set; }
    /// <summary> VarChar(250) (Allow Null) </summary>
    public string Aciklama { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TavanMiktar { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TavanFiyat { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? Ortalama { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? VezirOrt { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? Vezir { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? KstEnt { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? Camsan { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? Terme { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? SFC { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? Piyasa { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? Pazarlik { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahBirimFiyat { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahMalBedeli { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahAgFn { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahVadeFarki { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahKdv18 { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahToplam { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahTeminat { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahMektupFaizi { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahTopMektupTutar { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahPesinat { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahGenelTop { get; set; }
    /// <summary> VarChar(100) (Allow Null) </summary>
    public string TahOncelik { get; set; }
    /// <summary> Bit (Not Null) </summary>
    public bool Onay { get; set; }
    /// <summary> VarChar(5) (Allow Null) </summary>
    public string Onaylayan { get; set; }
    /// <summary> SmallDatetime (Allow Null) </summary>
    public DateTime? OnayTarih { get; set; }
    /// <summary> Decimal(4,4) (Not Null) </summary>
    public decimal PlanIbreliSter { get; set; }
    /// <summary> Decimal(4,4) (Not Null) </summary>
    public decimal PlanIbreliM3 { get; set; }
    /// <summary> Decimal(4,4) (Not Null) </summary>
    public decimal PlanYaprakliSter { get; set; }
    /// <summary> Decimal(4,4) (Not Null) </summary>
    public decimal PlanYaprakliM3 { get; set; }
    /// <summary> Decimal(4,4) (Not Null) </summary>
    public decimal KulIbreliSter { get; set; }
    /// <summary> Decimal(4,4) (Not Null) </summary>
    public decimal KulYaprakliSter { get; set; }
    /// <summary> Decimal(4,4) (Not Null) </summary>
    public decimal KulIbreliM3 { get; set; }
    /// <summary> Decimal(4,4) (Not Null) </summary>
    public decimal KulYaprakliM3 { get; set; }
    /// <summary> VarChar(81) (Allow Null) </summary>
    public string OrmIsltUnvan { get; set; }
    /// <summary> VarChar(50) (Allow Null) </summary>
    public string EmvalCinsiAdi { get; set; }
    /// <summary> SmallDatetime (Allow Null) </summary>
    public DateTime? Tarih2 { get; set; }
    /// <summary> Bit (Allow Null) </summary>
    public bool? Secim { get; set; }
    }
}
