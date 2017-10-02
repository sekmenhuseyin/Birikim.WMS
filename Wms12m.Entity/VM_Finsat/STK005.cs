using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using DevHelper;

namespace Wms12m.Entity
{
    #region STK005E Enum 
    public enum STK005E 
    {
        STK005_Row_ID,
        STK005_MalKodu,
        STK005_IslemTarihi,
        STK005_GC,
        STK005_CariHesapKodu,
        STK005_EvrakSeriNo,
        STK005_Miktari,
        STK005_BirimFiyati,
        STK005_Tutari,
        STK005_Iskonto,
        STK005_KDVTutari,
        STK005_IslemTipi,
        STK005_Kod1,
        STK005_Kod2,
        STK005_IrsaliyeNo,
        STK005_FaturaDurumu,
        STK005_MuhasebelesmeDurumu,
        STK005_KDVDurumu,
        STK005_ParaBirimi,
        STK005_SEQNo,
        STK005_GirenKaynak,
        STK005_GirenTarih,
        STK005_GirenSaat,
        STK005_GirenKodu,
        STK005_GirenSurum,
        STK005_DegistirenKaynak,
        STK005_DegistirenTarih,
        STK005_DegistirenSaat,
        STK005_DegistirenKodu,
        STK005_DegistirenSurum,
        STK005_IptalDurumu,
        STK005_AsilEvrakTarihi,
        STK005_SevkTarihi,
        STK005_OTVDahilHaric,
        STK005_OTV,
        STK005_Miktar2,
        STK005_Tutar2,
        STK005_KalemIskontoOrani1,
        STK005_KalemIskontoOrani2,
        STK005_KalemIskontoOrani3,
        STK005_KalemIskontoOrani4,
        STK005_KalemIskontoOrani5,
        STK005_KalemIskontoTutari1,
        STK005_KalemIskontoTutari2,
        STK005_KalemIskontoTutari3,
        STK005_KalemIskontoTutari4,
        STK005_KalemIskontoTutari5,
        STK005_DovizCinsi,
        STK005_DovizTutari,
        STK005_DovizKuru,
        STK005_Aciklama1,
        STK005_Aciklama2,
        STK005_Depo,
        STK005_Kod3,
        STK005_Kod4,
        STK005_Kod5,
        STK005_Kod6,
        STK005_Kod7,
        STK005_Kod8,
        STK005_Kod9,
        STK005_Kod10,
        STK005_Kod11,
        STK005_Kod12,
        STK005_Vasita,
        STK005_MalSeriNo,
        STK005_EvrakSeriNo2,
        STK005_SiparisNo,
        STK005_VadeTarihi,
        STK005_IrsaliyeFaturaTarihi,
        STK005_Tarih2,
        STK005_SiparisTarihi,
        STK005_IadeFaturaNo,
        STK005_IadeFaturaTarihi,
        STK005_Masraf,
        STK005_MaliyetSekli,
        STK005_MaliyetTutari,
        STK005_MaliyetTarihi,
        STK005_MaliyetMuhasebelesmeSekli,
        STK005_MaliyetMuhasebelesmeDurumu,
        STK005_MasrafMerkezi,
        STK005_MuhasebeFisTarihi,
        STK005_MuhasebeFisTipi,
        STK005_MuhasebeFisNo,
        STK005_MuhasebeFisKodu,
        STK005_MuhasebeSiraNo,
        STK005_MuhasebeHesapNo,
        STK005_MuhasebeKarsiHesapNo,
        STK005_MuhasebeYevmiyeSekli,
        STK005_Kod9Flag,
        STK005_Kod10Flag,
        STK005_StokTrFinansmanGider,
        STK005_StokTrVadeFarki,
        STK005_StokTrKrediVadeTarihi,
        STK005_StokTrSozFaizOrani,
        STK005_StokTrStokDuzHesapKodu,
        STK005_StokTrSmmDuzHesapKodu,
        STK005_StokTrNonReelFinansGidSpk,
        STK005_StokTrNonReelFinansGidMly,
        STK005_StokTrDuzKatsayiSpk,
        STK005_StokTrDuzKatsayiMly,
        STK005_StokTrDuzTutarSpk,
        STK005_StokTrDuzTutarMly,
        STK005_StokTrDonem,
        STK005_StokTrYil,
        STK005_StokTrDuzSatSpk,
        STK005_StokTrDuzSatMly,
        STK005_StokTrSatMalYontemi,
        STK005_StokTrSatMaliyeti,
        STK005_StokTrKrediAlimTarihi,
        STK005_StokTrDuzMhsFlag,
        STK005_StokTrSatMhsFlag,
        STK005_StokTrKrediTutari,
        STK005_StokTrKrediArindirmaSekli,
        STK005_StokTrGiderTipi,
        STK005_StokTrIlgiliEvrak,
        STK005_StokTrIlgiliTarih,
        STK005_KDVOranFlag,
        STK005_EvrakTipi,
        STK005_TeslimCHKodu,
        STK005_KarsiMuhasebeKodu,
        STK005_ExtFldTutar1,
        STK005_FaturalasanMiktar,
        STK005_KDVTevkIslemTuru,
        STK005_KDVTevkOrani,
        STK005_KDVTevkTutar,
        STK005_IthalatNo,
        STK005_IthalatAktarmaFlag,
        STK005_BarkodNo,
        STK005_KarekodTarih,
        STK005_PartiNo,
        STK005_EFaturaTipi,
        STK005_EFaturaDurumu,
        STK005_EFaturaOTVListeNo,
        STK005_ToplamKalemIskontosu,
        STK005_KDVMuafAciklama,
        STK005_KarsiMalKodu,
        STK005_UreticiMalKodu,
        STK005_StopajOrani,
        STK005_StopajTutari,
        STK005_IthalatBedeli,
        STK005_IskontoNedeni,
        STK005_Birim,
        STK005_FaturaMiktari,
        STK005_OTVTevkYokVarFlag,
        STK005_OTVTevkTutar,
        STK005_DovizBirimFiyati,
        STK005_KDVMuafTutari,
        STK005_IhracatNo,
        STK005_EArsivFaturaTipi,
        STK005_EArsivFaturaTeslimSekli,
        STK005_EArsivFaturaDurumu,
        STK005_Not1,
        STK005_Not2,
        STK005_Not3,
        STK005_Not4,
        STK005_Not5,
        STK005_AgirlikBirimi,
        STK005_AgirlikBrut,
        STK005_AgirlikNet,
        STK005_AgirlikDara,
        STK005_HacimBirimi,
        STK005_HacimBrut,
        STK005_HacimNet,
        STK005_KapTipi,
        STK005_KapAdedi,
        STK005_FiiliIhracatTarihi,
        STK005_RafKodu

    } 
    #endregion /// STK005E Enum                
    public class STK005 :  INotifyPropertyChanged
    {
        #region Properties
        #region Fields  
        private int _STK005_Row_ID; 
        private string _STK005_MalKodu; 
        private int? _STK005_IslemTarihi; 
        private byte? _STK005_GC; 
        private string _STK005_CariHesapKodu; 
        private string _STK005_EvrakSeriNo; 
        private decimal? _STK005_Miktari; 
        private decimal? _STK005_BirimFiyati; 
        private decimal? _STK005_Tutari; 
        private decimal? _STK005_Iskonto; 
        private decimal? _STK005_KDVTutari; 
        private short? _STK005_IslemTipi; 
        private string _STK005_Kod1; 
        private string _STK005_Kod2; 
        private string _STK005_IrsaliyeNo; 
        private byte? _STK005_FaturaDurumu; 
        private byte? _STK005_MuhasebelesmeDurumu; 
        private byte? _STK005_KDVDurumu; 
        private byte? _STK005_ParaBirimi; 
        private int? _STK005_SEQNo; 
        private string _STK005_GirenKaynak; 
        private int? _STK005_GirenTarih; 
        private string _STK005_GirenSaat; 
        private string _STK005_GirenKodu; 
        private string _STK005_GirenSurum; 
        private string _STK005_DegistirenKaynak; 
        private int? _STK005_DegistirenTarih; 
        private string _STK005_DegistirenSaat; 
        private string _STK005_DegistirenKodu; 
        private string _STK005_DegistirenSurum; 
        private byte? _STK005_IptalDurumu; 
        private int? _STK005_AsilEvrakTarihi; 
        private int? _STK005_SevkTarihi; 
        private byte? _STK005_OTVDahilHaric; 
        private decimal? _STK005_OTV; 
        private decimal? _STK005_Miktar2; 
        private decimal? _STK005_Tutar2; 
        private decimal? _STK005_KalemIskontoOrani1; 
        private decimal? _STK005_KalemIskontoOrani2; 
        private decimal? _STK005_KalemIskontoOrani3; 
        private decimal? _STK005_KalemIskontoOrani4; 
        private decimal? _STK005_KalemIskontoOrani5; 
        private decimal? _STK005_KalemIskontoTutari1; 
        private decimal? _STK005_KalemIskontoTutari2; 
        private decimal? _STK005_KalemIskontoTutari3; 
        private decimal? _STK005_KalemIskontoTutari4; 
        private decimal? _STK005_KalemIskontoTutari5; 
        private string _STK005_DovizCinsi; 
        private decimal? _STK005_DovizTutari; 
        private decimal? _STK005_DovizKuru; 
        private string _STK005_Aciklama1; 
        private string _STK005_Aciklama2; 
        private string _STK005_Depo; 
        private string _STK005_Kod3; 
        private string _STK005_Kod4; 
        private string _STK005_Kod5; 
        private string _STK005_Kod6; 
        private string _STK005_Kod7; 
        private string _STK005_Kod8; 
        private string _STK005_Kod9; 
        private string _STK005_Kod10; 
        private decimal? _STK005_Kod11; 
        private decimal? _STK005_Kod12; 
        private string _STK005_Vasita; 
        private string _STK005_MalSeriNo; 
        private string _STK005_EvrakSeriNo2; 
        private string _STK005_SiparisNo; 
        private int? _STK005_VadeTarihi; 
        private int? _STK005_IrsaliyeFaturaTarihi; 
        private int? _STK005_Tarih2; 
        private int? _STK005_SiparisTarihi; 
        private string _STK005_IadeFaturaNo; 
        private int? _STK005_IadeFaturaTarihi; 
        private decimal? _STK005_Masraf; 
        private short? _STK005_MaliyetSekli; 
        private decimal? _STK005_MaliyetTutari; 
        private int? _STK005_MaliyetTarihi; 
        private short? _STK005_MaliyetMuhasebelesmeSekli; 
        private byte? _STK005_MaliyetMuhasebelesmeDurumu; 
        private string _STK005_MasrafMerkezi; 
        private int? _STK005_MuhasebeFisTarihi; 
        private byte? _STK005_MuhasebeFisTipi; 
        private int? _STK005_MuhasebeFisNo; 
        private string _STK005_MuhasebeFisKodu; 
        private short? _STK005_MuhasebeSiraNo; 
        private string _STK005_MuhasebeHesapNo; 
        private string _STK005_MuhasebeKarsiHesapNo; 
        private byte? _STK005_MuhasebeYevmiyeSekli; 
        private byte? _STK005_Kod9Flag; 
        private byte? _STK005_Kod10Flag; 
        private decimal? _STK005_StokTrFinansmanGider; 
        private decimal? _STK005_StokTrVadeFarki; 
        private int? _STK005_StokTrKrediVadeTarihi; 
        private decimal? _STK005_StokTrSozFaizOrani; 
        private string _STK005_StokTrStokDuzHesapKodu; 
        private string _STK005_StokTrSmmDuzHesapKodu; 
        private decimal? _STK005_StokTrNonReelFinansGidSpk; 
        private decimal? _STK005_StokTrNonReelFinansGidMly; 
        private decimal? _STK005_StokTrDuzKatsayiSpk; 
        private decimal? _STK005_StokTrDuzKatsayiMly; 
        private decimal? _STK005_StokTrDuzTutarSpk; 
        private decimal? _STK005_StokTrDuzTutarMly; 
        private byte? _STK005_StokTrDonem; 
        private int? _STK005_StokTrYil; 
        private decimal? _STK005_StokTrDuzSatSpk; 
        private decimal? _STK005_StokTrDuzSatMly; 
        private short? _STK005_StokTrSatMalYontemi; 
        private decimal? _STK005_StokTrSatMaliyeti; 
        private int? _STK005_StokTrKrediAlimTarihi; 
        private byte? _STK005_StokTrDuzMhsFlag; 
        private byte? _STK005_StokTrSatMhsFlag; 
        private decimal? _STK005_StokTrKrediTutari; 
        private byte? _STK005_StokTrKrediArindirmaSekli; 
        private byte? _STK005_StokTrGiderTipi; 
        private string _STK005_StokTrIlgiliEvrak; 
        private int? _STK005_StokTrIlgiliTarih; 
        private byte? _STK005_KDVOranFlag; 
        private short? _STK005_EvrakTipi; 
        private string _STK005_TeslimCHKodu; 
        private string _STK005_KarsiMuhasebeKodu; 
        private decimal? _STK005_ExtFldTutar1; 
        private decimal? _STK005_FaturalasanMiktar; 
        private int? _STK005_KDVTevkIslemTuru; 
        private string _STK005_KDVTevkOrani; 
        private decimal? _STK005_KDVTevkTutar; 
        private string _STK005_IthalatNo; 
        private byte? _STK005_IthalatAktarmaFlag; 
        private string _STK005_BarkodNo; 
        private int? _STK005_KarekodTarih; 
        private string _STK005_PartiNo; 
        private byte? _STK005_EFaturaTipi; 
        private short? _STK005_EFaturaDurumu; 
        private string _STK005_EFaturaOTVListeNo; 
        private decimal? _STK005_ToplamKalemIskontosu; 
        private string _STK005_KDVMuafAciklama; 
        private string _STK005_KarsiMalKodu; 
        private string _STK005_UreticiMalKodu; 
        private decimal? _STK005_StopajOrani; 
        private decimal? _STK005_StopajTutari; 
        private decimal? _STK005_IthalatBedeli; 
        private string _STK005_IskontoNedeni; 
        private string _STK005_Birim; 
        private decimal? _STK005_FaturaMiktari; 
        private byte? _STK005_OTVTevkYokVarFlag; 
        private decimal? _STK005_OTVTevkTutar; 
        private decimal? _STK005_DovizBirimFiyati; 
        private decimal? _STK005_KDVMuafTutari; 
        private string _STK005_IhracatNo; 
        private byte? _STK005_EArsivFaturaTipi; 
        private byte? _STK005_EArsivFaturaTeslimSekli; 
        private short? _STK005_EArsivFaturaDurumu; 
        private string _STK005_Not1; 
        private string _STK005_Not2; 
        private string _STK005_Not3; 
        private string _STK005_Not4; 
        private string _STK005_Not5; 
        private string _STK005_AgirlikBirimi; 
        private decimal? _STK005_AgirlikBrut; 
        private decimal? _STK005_AgirlikNet; 
        private decimal? _STK005_AgirlikDara; 
        private string _STK005_HacimBirimi; 
        private decimal? _STK005_HacimBrut; 
        private decimal? _STK005_HacimNet; 
        private string _STK005_KapTipi; 
        private decimal? _STK005_KapAdedi; 
        private int? _STK005_FiiliIhracatTarihi; 
        private string _STK005_RafKodu; 
        private int _pk_STK005_Row_ID;
        #endregion /// Fields
     
       
        /// <summary> INT (4) PrimaryKey IdentityKey * </summary>
        public int STK005_Row_ID 
        {
            get {   return this._STK005_Row_ID;   }                          
        }
       
        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK005_MalKodu 
        {
            get {   return this._STK005_MalKodu;   } 
            set 
            {
                this._STK005_MalKodu = value;
                OnPropertyChanged("STK005_MalKodu"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_IslemTarihi 
        {
            get {   return this._STK005_IslemTarihi;   } 
            set 
            {
                this._STK005_IslemTarihi = value;
                OnPropertyChanged("STK005_IslemTarihi"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_GC 
        {
            get {   return this._STK005_GC;   } 
            set 
            {
                this._STK005_GC = value;
                OnPropertyChanged("STK005_GC"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK005_CariHesapKodu 
        {
            get {   return this._STK005_CariHesapKodu;   } 
            set 
            {
                this._STK005_CariHesapKodu = value;
                OnPropertyChanged("STK005_CariHesapKodu"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK005_EvrakSeriNo 
        {
            get {   return this._STK005_EvrakSeriNo;   } 
            set 
            {
                this._STK005_EvrakSeriNo = value;
                OnPropertyChanged("STK005_EvrakSeriNo"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_Miktari 
        {
            get {   return this._STK005_Miktari;   } 
            set 
            {
                this._STK005_Miktari = value;
                OnPropertyChanged("STK005_Miktari"); 
            }                         
        }
       
        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK005_BirimFiyati 
        {
            get {   return this._STK005_BirimFiyati;   } 
            set 
            {
                this._STK005_BirimFiyati = value;
                OnPropertyChanged("STK005_BirimFiyati"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_Tutari 
        {
            get {   return this._STK005_Tutari;   } 
            set 
            {
                this._STK005_Tutari = value;
                OnPropertyChanged("STK005_Tutari"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_Iskonto 
        {
            get {   return this._STK005_Iskonto;   } 
            set 
            {
                this._STK005_Iskonto = value;
                OnPropertyChanged("STK005_Iskonto"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_KDVTutari 
        {
            get {   return this._STK005_KDVTutari;   } 
            set 
            {
                this._STK005_KDVTutari = value;
                OnPropertyChanged("STK005_KDVTutari"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK005_IslemTipi 
        {
            get {   return this._STK005_IslemTipi;   } 
            set 
            {
                this._STK005_IslemTipi = value;
                OnPropertyChanged("STK005_IslemTipi"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod1 
        {
            get {   return this._STK005_Kod1;   } 
            set 
            {
                this._STK005_Kod1 = value;
                OnPropertyChanged("STK005_Kod1"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod2 
        {
            get {   return this._STK005_Kod2;   } 
            set 
            {
                this._STK005_Kod2 = value;
                OnPropertyChanged("STK005_Kod2"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK005_IrsaliyeNo 
        {
            get {   return this._STK005_IrsaliyeNo;   } 
            set 
            {
                this._STK005_IrsaliyeNo = value;
                OnPropertyChanged("STK005_IrsaliyeNo"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_FaturaDurumu 
        {
            get {   return this._STK005_FaturaDurumu;   } 
            set 
            {
                this._STK005_FaturaDurumu = value;
                OnPropertyChanged("STK005_FaturaDurumu"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_MuhasebelesmeDurumu 
        {
            get {   return this._STK005_MuhasebelesmeDurumu;   } 
            set 
            {
                this._STK005_MuhasebelesmeDurumu = value;
                OnPropertyChanged("STK005_MuhasebelesmeDurumu"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_KDVDurumu 
        {
            get {   return this._STK005_KDVDurumu;   } 
            set 
            {
                this._STK005_KDVDurumu = value;
                OnPropertyChanged("STK005_KDVDurumu"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_ParaBirimi 
        {
            get {   return this._STK005_ParaBirimi;   } 
            set 
            {
                this._STK005_ParaBirimi = value;
                OnPropertyChanged("STK005_ParaBirimi"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_SEQNo 
        {
            get {   return this._STK005_SEQNo;   } 
            set 
            {
                this._STK005_SEQNo = value;
                OnPropertyChanged("STK005_SEQNo"); 
            }                         
        }
       
        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK005_GirenKaynak 
        {
            get {   return this._STK005_GirenKaynak;   } 
            set 
            {
                this._STK005_GirenKaynak = value;
                OnPropertyChanged("STK005_GirenKaynak"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_GirenTarih 
        {
            get {   return this._STK005_GirenTarih;   } 
            set 
            {
                this._STK005_GirenTarih = value;
                OnPropertyChanged("STK005_GirenTarih"); 
            }                         
        }
       
        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string STK005_GirenSaat 
        {
            get {   return this._STK005_GirenSaat;   } 
            set 
            {
                this._STK005_GirenSaat = value;
                OnPropertyChanged("STK005_GirenSaat"); 
            }                         
        }
       
        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK005_GirenKodu 
        {
            get {   return this._STK005_GirenKodu;   } 
            set 
            {
                this._STK005_GirenKodu = value;
                OnPropertyChanged("STK005_GirenKodu"); 
            }                         
        }
       
        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string STK005_GirenSurum 
        {
            get {   return this._STK005_GirenSurum;   } 
            set 
            {
                this._STK005_GirenSurum = value;
                OnPropertyChanged("STK005_GirenSurum"); 
            }                         
        }
       
        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK005_DegistirenKaynak 
        {
            get {   return this._STK005_DegistirenKaynak;   } 
            set 
            {
                this._STK005_DegistirenKaynak = value;
                OnPropertyChanged("STK005_DegistirenKaynak"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_DegistirenTarih 
        {
            get {   return this._STK005_DegistirenTarih;   } 
            set 
            {
                this._STK005_DegistirenTarih = value;
                OnPropertyChanged("STK005_DegistirenTarih"); 
            }                         
        }
       
        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string STK005_DegistirenSaat 
        {
            get {   return this._STK005_DegistirenSaat;   } 
            set 
            {
                this._STK005_DegistirenSaat = value;
                OnPropertyChanged("STK005_DegistirenSaat"); 
            }                         
        }
       
        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK005_DegistirenKodu 
        {
            get {   return this._STK005_DegistirenKodu;   } 
            set 
            {
                this._STK005_DegistirenKodu = value;
                OnPropertyChanged("STK005_DegistirenKodu"); 
            }                         
        }
       
        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string STK005_DegistirenSurum 
        {
            get {   return this._STK005_DegistirenSurum;   } 
            set 
            {
                this._STK005_DegistirenSurum = value;
                OnPropertyChanged("STK005_DegistirenSurum"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_IptalDurumu 
        {
            get {   return this._STK005_IptalDurumu;   } 
            set 
            {
                this._STK005_IptalDurumu = value;
                OnPropertyChanged("STK005_IptalDurumu"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_AsilEvrakTarihi 
        {
            get {   return this._STK005_AsilEvrakTarihi;   } 
            set 
            {
                this._STK005_AsilEvrakTarihi = value;
                OnPropertyChanged("STK005_AsilEvrakTarihi"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_SevkTarihi 
        {
            get {   return this._STK005_SevkTarihi;   } 
            set 
            {
                this._STK005_SevkTarihi = value;
                OnPropertyChanged("STK005_SevkTarihi"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_OTVDahilHaric 
        {
            get {   return this._STK005_OTVDahilHaric;   } 
            set 
            {
                this._STK005_OTVDahilHaric = value;
                OnPropertyChanged("STK005_OTVDahilHaric"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_OTV 
        {
            get {   return this._STK005_OTV;   } 
            set 
            {
                this._STK005_OTV = value;
                OnPropertyChanged("STK005_OTV"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_Miktar2 
        {
            get {   return this._STK005_Miktar2;   } 
            set 
            {
                this._STK005_Miktar2 = value;
                OnPropertyChanged("STK005_Miktar2"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_Tutar2 
        {
            get {   return this._STK005_Tutar2;   } 
            set 
            {
                this._STK005_Tutar2 = value;
                OnPropertyChanged("STK005_Tutar2"); 
            }                         
        }
       
        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK005_KalemIskontoOrani1 
        {
            get {   return this._STK005_KalemIskontoOrani1;   } 
            set 
            {
                this._STK005_KalemIskontoOrani1 = value;
                OnPropertyChanged("STK005_KalemIskontoOrani1"); 
            }                         
        }
       
        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK005_KalemIskontoOrani2 
        {
            get {   return this._STK005_KalemIskontoOrani2;   } 
            set 
            {
                this._STK005_KalemIskontoOrani2 = value;
                OnPropertyChanged("STK005_KalemIskontoOrani2"); 
            }                         
        }
       
        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK005_KalemIskontoOrani3 
        {
            get {   return this._STK005_KalemIskontoOrani3;   } 
            set 
            {
                this._STK005_KalemIskontoOrani3 = value;
                OnPropertyChanged("STK005_KalemIskontoOrani3"); 
            }                         
        }
       
        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK005_KalemIskontoOrani4 
        {
            get {   return this._STK005_KalemIskontoOrani4;   } 
            set 
            {
                this._STK005_KalemIskontoOrani4 = value;
                OnPropertyChanged("STK005_KalemIskontoOrani4"); 
            }                         
        }
       
        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK005_KalemIskontoOrani5 
        {
            get {   return this._STK005_KalemIskontoOrani5;   } 
            set 
            {
                this._STK005_KalemIskontoOrani5 = value;
                OnPropertyChanged("STK005_KalemIskontoOrani5"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_KalemIskontoTutari1 
        {
            get {   return this._STK005_KalemIskontoTutari1;   } 
            set 
            {
                this._STK005_KalemIskontoTutari1 = value;
                OnPropertyChanged("STK005_KalemIskontoTutari1"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_KalemIskontoTutari2 
        {
            get {   return this._STK005_KalemIskontoTutari2;   } 
            set 
            {
                this._STK005_KalemIskontoTutari2 = value;
                OnPropertyChanged("STK005_KalemIskontoTutari2"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_KalemIskontoTutari3 
        {
            get {   return this._STK005_KalemIskontoTutari3;   } 
            set 
            {
                this._STK005_KalemIskontoTutari3 = value;
                OnPropertyChanged("STK005_KalemIskontoTutari3"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_KalemIskontoTutari4 
        {
            get {   return this._STK005_KalemIskontoTutari4;   } 
            set 
            {
                this._STK005_KalemIskontoTutari4 = value;
                OnPropertyChanged("STK005_KalemIskontoTutari4"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_KalemIskontoTutari5 
        {
            get {   return this._STK005_KalemIskontoTutari5;   } 
            set 
            {
                this._STK005_KalemIskontoTutari5 = value;
                OnPropertyChanged("STK005_KalemIskontoTutari5"); 
            }                         
        }
       
        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string STK005_DovizCinsi 
        {
            get {   return this._STK005_DovizCinsi;   } 
            set 
            {
                this._STK005_DovizCinsi = value;
                OnPropertyChanged("STK005_DovizCinsi"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_DovizTutari 
        {
            get {   return this._STK005_DovizTutari;   } 
            set 
            {
                this._STK005_DovizTutari = value;
                OnPropertyChanged("STK005_DovizTutari"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_DovizKuru 
        {
            get {   return this._STK005_DovizKuru;   } 
            set 
            {
                this._STK005_DovizKuru = value;
                OnPropertyChanged("STK005_DovizKuru"); 
            }                         
        }
       
        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string STK005_Aciklama1 
        {
            get {   return this._STK005_Aciklama1;   } 
            set 
            {
                this._STK005_Aciklama1 = value;
                OnPropertyChanged("STK005_Aciklama1"); 
            }                         
        }
       
        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string STK005_Aciklama2 
        {
            get {   return this._STK005_Aciklama2;   } 
            set 
            {
                this._STK005_Aciklama2 = value;
                OnPropertyChanged("STK005_Aciklama2"); 
            }                         
        }
       
        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK005_Depo 
        {
            get {   return this._STK005_Depo;   } 
            set 
            {
                this._STK005_Depo = value;
                OnPropertyChanged("STK005_Depo"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod3 
        {
            get {   return this._STK005_Kod3;   } 
            set 
            {
                this._STK005_Kod3 = value;
                OnPropertyChanged("STK005_Kod3"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod4 
        {
            get {   return this._STK005_Kod4;   } 
            set 
            {
                this._STK005_Kod4 = value;
                OnPropertyChanged("STK005_Kod4"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod5 
        {
            get {   return this._STK005_Kod5;   } 
            set 
            {
                this._STK005_Kod5 = value;
                OnPropertyChanged("STK005_Kod5"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod6 
        {
            get {   return this._STK005_Kod6;   } 
            set 
            {
                this._STK005_Kod6 = value;
                OnPropertyChanged("STK005_Kod6"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod7 
        {
            get {   return this._STK005_Kod7;   } 
            set 
            {
                this._STK005_Kod7 = value;
                OnPropertyChanged("STK005_Kod7"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod8 
        {
            get {   return this._STK005_Kod8;   } 
            set 
            {
                this._STK005_Kod8 = value;
                OnPropertyChanged("STK005_Kod8"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod9 
        {
            get {   return this._STK005_Kod9;   } 
            set 
            {
                this._STK005_Kod9 = value;
                OnPropertyChanged("STK005_Kod9"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod10 
        {
            get {   return this._STK005_Kod10;   } 
            set 
            {
                this._STK005_Kod10 = value;
                OnPropertyChanged("STK005_Kod10"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_Kod11 
        {
            get {   return this._STK005_Kod11;   } 
            set 
            {
                this._STK005_Kod11 = value;
                OnPropertyChanged("STK005_Kod11"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_Kod12 
        {
            get {   return this._STK005_Kod12;   } 
            set 
            {
                this._STK005_Kod12 = value;
                OnPropertyChanged("STK005_Kod12"); 
            }                         
        }
       
        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string STK005_Vasita 
        {
            get {   return this._STK005_Vasita;   } 
            set 
            {
                this._STK005_Vasita = value;
                OnPropertyChanged("STK005_Vasita"); 
            }                         
        }
       
        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK005_MalSeriNo 
        {
            get {   return this._STK005_MalSeriNo;   } 
            set 
            {
                this._STK005_MalSeriNo = value;
                OnPropertyChanged("STK005_MalSeriNo"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK005_EvrakSeriNo2 
        {
            get {   return this._STK005_EvrakSeriNo2;   } 
            set 
            {
                this._STK005_EvrakSeriNo2 = value;
                OnPropertyChanged("STK005_EvrakSeriNo2"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK005_SiparisNo 
        {
            get {   return this._STK005_SiparisNo;   } 
            set 
            {
                this._STK005_SiparisNo = value;
                OnPropertyChanged("STK005_SiparisNo"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_VadeTarihi 
        {
            get {   return this._STK005_VadeTarihi;   } 
            set 
            {
                this._STK005_VadeTarihi = value;
                OnPropertyChanged("STK005_VadeTarihi"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_IrsaliyeFaturaTarihi 
        {
            get {   return this._STK005_IrsaliyeFaturaTarihi;   } 
            set 
            {
                this._STK005_IrsaliyeFaturaTarihi = value;
                OnPropertyChanged("STK005_IrsaliyeFaturaTarihi"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_Tarih2 
        {
            get {   return this._STK005_Tarih2;   } 
            set 
            {
                this._STK005_Tarih2 = value;
                OnPropertyChanged("STK005_Tarih2"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_SiparisTarihi 
        {
            get {   return this._STK005_SiparisTarihi;   } 
            set 
            {
                this._STK005_SiparisTarihi = value;
                OnPropertyChanged("STK005_SiparisTarihi"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK005_IadeFaturaNo 
        {
            get {   return this._STK005_IadeFaturaNo;   } 
            set 
            {
                this._STK005_IadeFaturaNo = value;
                OnPropertyChanged("STK005_IadeFaturaNo"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_IadeFaturaTarihi 
        {
            get {   return this._STK005_IadeFaturaTarihi;   } 
            set 
            {
                this._STK005_IadeFaturaTarihi = value;
                OnPropertyChanged("STK005_IadeFaturaTarihi"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_Masraf 
        {
            get {   return this._STK005_Masraf;   } 
            set 
            {
                this._STK005_Masraf = value;
                OnPropertyChanged("STK005_Masraf"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK005_MaliyetSekli 
        {
            get {   return this._STK005_MaliyetSekli;   } 
            set 
            {
                this._STK005_MaliyetSekli = value;
                OnPropertyChanged("STK005_MaliyetSekli"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_MaliyetTutari 
        {
            get {   return this._STK005_MaliyetTutari;   } 
            set 
            {
                this._STK005_MaliyetTutari = value;
                OnPropertyChanged("STK005_MaliyetTutari"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_MaliyetTarihi 
        {
            get {   return this._STK005_MaliyetTarihi;   } 
            set 
            {
                this._STK005_MaliyetTarihi = value;
                OnPropertyChanged("STK005_MaliyetTarihi"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK005_MaliyetMuhasebelesmeSekli 
        {
            get {   return this._STK005_MaliyetMuhasebelesmeSekli;   } 
            set 
            {
                this._STK005_MaliyetMuhasebelesmeSekli = value;
                OnPropertyChanged("STK005_MaliyetMuhasebelesmeSekli"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_MaliyetMuhasebelesmeDurumu 
        {
            get {   return this._STK005_MaliyetMuhasebelesmeDurumu;   } 
            set 
            {
                this._STK005_MaliyetMuhasebelesmeDurumu = value;
                OnPropertyChanged("STK005_MaliyetMuhasebelesmeDurumu"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_MasrafMerkezi 
        {
            get {   return this._STK005_MasrafMerkezi;   } 
            set 
            {
                this._STK005_MasrafMerkezi = value;
                OnPropertyChanged("STK005_MasrafMerkezi"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_MuhasebeFisTarihi 
        {
            get {   return this._STK005_MuhasebeFisTarihi;   } 
            set 
            {
                this._STK005_MuhasebeFisTarihi = value;
                OnPropertyChanged("STK005_MuhasebeFisTarihi"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_MuhasebeFisTipi 
        {
            get {   return this._STK005_MuhasebeFisTipi;   } 
            set 
            {
                this._STK005_MuhasebeFisTipi = value;
                OnPropertyChanged("STK005_MuhasebeFisTipi"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_MuhasebeFisNo 
        {
            get {   return this._STK005_MuhasebeFisNo;   } 
            set 
            {
                this._STK005_MuhasebeFisNo = value;
                OnPropertyChanged("STK005_MuhasebeFisNo"); 
            }                         
        }
       
        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string STK005_MuhasebeFisKodu 
        {
            get {   return this._STK005_MuhasebeFisKodu;   } 
            set 
            {
                this._STK005_MuhasebeFisKodu = value;
                OnPropertyChanged("STK005_MuhasebeFisKodu"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK005_MuhasebeSiraNo 
        {
            get {   return this._STK005_MuhasebeSiraNo;   } 
            set 
            {
                this._STK005_MuhasebeSiraNo = value;
                OnPropertyChanged("STK005_MuhasebeSiraNo"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_MuhasebeHesapNo 
        {
            get {   return this._STK005_MuhasebeHesapNo;   } 
            set 
            {
                this._STK005_MuhasebeHesapNo = value;
                OnPropertyChanged("STK005_MuhasebeHesapNo"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_MuhasebeKarsiHesapNo 
        {
            get {   return this._STK005_MuhasebeKarsiHesapNo;   } 
            set 
            {
                this._STK005_MuhasebeKarsiHesapNo = value;
                OnPropertyChanged("STK005_MuhasebeKarsiHesapNo"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_MuhasebeYevmiyeSekli 
        {
            get {   return this._STK005_MuhasebeYevmiyeSekli;   } 
            set 
            {
                this._STK005_MuhasebeYevmiyeSekli = value;
                OnPropertyChanged("STK005_MuhasebeYevmiyeSekli"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_Kod9Flag 
        {
            get {   return this._STK005_Kod9Flag;   } 
            set 
            {
                this._STK005_Kod9Flag = value;
                OnPropertyChanged("STK005_Kod9Flag"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_Kod10Flag 
        {
            get {   return this._STK005_Kod10Flag;   } 
            set 
            {
                this._STK005_Kod10Flag = value;
                OnPropertyChanged("STK005_Kod10Flag"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrFinansmanGider 
        {
            get {   return this._STK005_StokTrFinansmanGider;   } 
            set 
            {
                this._STK005_StokTrFinansmanGider = value;
                OnPropertyChanged("STK005_StokTrFinansmanGider"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrVadeFarki 
        {
            get {   return this._STK005_StokTrVadeFarki;   } 
            set 
            {
                this._STK005_StokTrVadeFarki = value;
                OnPropertyChanged("STK005_StokTrVadeFarki"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_StokTrKrediVadeTarihi 
        {
            get {   return this._STK005_StokTrKrediVadeTarihi;   } 
            set 
            {
                this._STK005_StokTrKrediVadeTarihi = value;
                OnPropertyChanged("STK005_StokTrKrediVadeTarihi"); 
            }                         
        }
       
        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK005_StokTrSozFaizOrani 
        {
            get {   return this._STK005_StokTrSozFaizOrani;   } 
            set 
            {
                this._STK005_StokTrSozFaizOrani = value;
                OnPropertyChanged("STK005_StokTrSozFaizOrani"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_StokTrStokDuzHesapKodu 
        {
            get {   return this._STK005_StokTrStokDuzHesapKodu;   } 
            set 
            {
                this._STK005_StokTrStokDuzHesapKodu = value;
                OnPropertyChanged("STK005_StokTrStokDuzHesapKodu"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_StokTrSmmDuzHesapKodu 
        {
            get {   return this._STK005_StokTrSmmDuzHesapKodu;   } 
            set 
            {
                this._STK005_StokTrSmmDuzHesapKodu = value;
                OnPropertyChanged("STK005_StokTrSmmDuzHesapKodu"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrNonReelFinansGidSpk 
        {
            get {   return this._STK005_StokTrNonReelFinansGidSpk;   } 
            set 
            {
                this._STK005_StokTrNonReelFinansGidSpk = value;
                OnPropertyChanged("STK005_StokTrNonReelFinansGidSpk"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrNonReelFinansGidMly 
        {
            get {   return this._STK005_StokTrNonReelFinansGidMly;   } 
            set 
            {
                this._STK005_StokTrNonReelFinansGidMly = value;
                OnPropertyChanged("STK005_StokTrNonReelFinansGidMly"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrDuzKatsayiSpk 
        {
            get {   return this._STK005_StokTrDuzKatsayiSpk;   } 
            set 
            {
                this._STK005_StokTrDuzKatsayiSpk = value;
                OnPropertyChanged("STK005_StokTrDuzKatsayiSpk"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrDuzKatsayiMly 
        {
            get {   return this._STK005_StokTrDuzKatsayiMly;   } 
            set 
            {
                this._STK005_StokTrDuzKatsayiMly = value;
                OnPropertyChanged("STK005_StokTrDuzKatsayiMly"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrDuzTutarSpk 
        {
            get {   return this._STK005_StokTrDuzTutarSpk;   } 
            set 
            {
                this._STK005_StokTrDuzTutarSpk = value;
                OnPropertyChanged("STK005_StokTrDuzTutarSpk"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrDuzTutarMly 
        {
            get {   return this._STK005_StokTrDuzTutarMly;   } 
            set 
            {
                this._STK005_StokTrDuzTutarMly = value;
                OnPropertyChanged("STK005_StokTrDuzTutarMly"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_StokTrDonem 
        {
            get {   return this._STK005_StokTrDonem;   } 
            set 
            {
                this._STK005_StokTrDonem = value;
                OnPropertyChanged("STK005_StokTrDonem"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_StokTrYil 
        {
            get {   return this._STK005_StokTrYil;   } 
            set 
            {
                this._STK005_StokTrYil = value;
                OnPropertyChanged("STK005_StokTrYil"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrDuzSatSpk 
        {
            get {   return this._STK005_StokTrDuzSatSpk;   } 
            set 
            {
                this._STK005_StokTrDuzSatSpk = value;
                OnPropertyChanged("STK005_StokTrDuzSatSpk"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrDuzSatMly 
        {
            get {   return this._STK005_StokTrDuzSatMly;   } 
            set 
            {
                this._STK005_StokTrDuzSatMly = value;
                OnPropertyChanged("STK005_StokTrDuzSatMly"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK005_StokTrSatMalYontemi 
        {
            get {   return this._STK005_StokTrSatMalYontemi;   } 
            set 
            {
                this._STK005_StokTrSatMalYontemi = value;
                OnPropertyChanged("STK005_StokTrSatMalYontemi"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrSatMaliyeti 
        {
            get {   return this._STK005_StokTrSatMaliyeti;   } 
            set 
            {
                this._STK005_StokTrSatMaliyeti = value;
                OnPropertyChanged("STK005_StokTrSatMaliyeti"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_StokTrKrediAlimTarihi 
        {
            get {   return this._STK005_StokTrKrediAlimTarihi;   } 
            set 
            {
                this._STK005_StokTrKrediAlimTarihi = value;
                OnPropertyChanged("STK005_StokTrKrediAlimTarihi"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_StokTrDuzMhsFlag 
        {
            get {   return this._STK005_StokTrDuzMhsFlag;   } 
            set 
            {
                this._STK005_StokTrDuzMhsFlag = value;
                OnPropertyChanged("STK005_StokTrDuzMhsFlag"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_StokTrSatMhsFlag 
        {
            get {   return this._STK005_StokTrSatMhsFlag;   } 
            set 
            {
                this._STK005_StokTrSatMhsFlag = value;
                OnPropertyChanged("STK005_StokTrSatMhsFlag"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrKrediTutari 
        {
            get {   return this._STK005_StokTrKrediTutari;   } 
            set 
            {
                this._STK005_StokTrKrediTutari = value;
                OnPropertyChanged("STK005_StokTrKrediTutari"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_StokTrKrediArindirmaSekli 
        {
            get {   return this._STK005_StokTrKrediArindirmaSekli;   } 
            set 
            {
                this._STK005_StokTrKrediArindirmaSekli = value;
                OnPropertyChanged("STK005_StokTrKrediArindirmaSekli"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_StokTrGiderTipi 
        {
            get {   return this._STK005_StokTrGiderTipi;   } 
            set 
            {
                this._STK005_StokTrGiderTipi = value;
                OnPropertyChanged("STK005_StokTrGiderTipi"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK005_StokTrIlgiliEvrak 
        {
            get {   return this._STK005_StokTrIlgiliEvrak;   } 
            set 
            {
                this._STK005_StokTrIlgiliEvrak = value;
                OnPropertyChanged("STK005_StokTrIlgiliEvrak"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_StokTrIlgiliTarih 
        {
            get {   return this._STK005_StokTrIlgiliTarih;   } 
            set 
            {
                this._STK005_StokTrIlgiliTarih = value;
                OnPropertyChanged("STK005_StokTrIlgiliTarih"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_KDVOranFlag 
        {
            get {   return this._STK005_KDVOranFlag;   } 
            set 
            {
                this._STK005_KDVOranFlag = value;
                OnPropertyChanged("STK005_KDVOranFlag"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK005_EvrakTipi 
        {
            get {   return this._STK005_EvrakTipi;   } 
            set 
            {
                this._STK005_EvrakTipi = value;
                OnPropertyChanged("STK005_EvrakTipi"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK005_TeslimCHKodu 
        {
            get {   return this._STK005_TeslimCHKodu;   } 
            set 
            {
                this._STK005_TeslimCHKodu = value;
                OnPropertyChanged("STK005_TeslimCHKodu"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_KarsiMuhasebeKodu 
        {
            get {   return this._STK005_KarsiMuhasebeKodu;   } 
            set 
            {
                this._STK005_KarsiMuhasebeKodu = value;
                OnPropertyChanged("STK005_KarsiMuhasebeKodu"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_ExtFldTutar1 
        {
            get {   return this._STK005_ExtFldTutar1;   } 
            set 
            {
                this._STK005_ExtFldTutar1 = value;
                OnPropertyChanged("STK005_ExtFldTutar1"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_FaturalasanMiktar 
        {
            get {   return this._STK005_FaturalasanMiktar;   } 
            set 
            {
                this._STK005_FaturalasanMiktar = value;
                OnPropertyChanged("STK005_FaturalasanMiktar"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_KDVTevkIslemTuru 
        {
            get {   return this._STK005_KDVTevkIslemTuru;   } 
            set 
            {
                this._STK005_KDVTevkIslemTuru = value;
                OnPropertyChanged("STK005_KDVTevkIslemTuru"); 
            }                         
        }
       
        /// <summary> NVARCHAR (14) Allow Null </summary>
        public string STK005_KDVTevkOrani 
        {
            get {   return this._STK005_KDVTevkOrani;   } 
            set 
            {
                this._STK005_KDVTevkOrani = value;
                OnPropertyChanged("STK005_KDVTevkOrani"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_KDVTevkTutar 
        {
            get {   return this._STK005_KDVTevkTutar;   } 
            set 
            {
                this._STK005_KDVTevkTutar = value;
                OnPropertyChanged("STK005_KDVTevkTutar"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_IthalatNo 
        {
            get {   return this._STK005_IthalatNo;   } 
            set 
            {
                this._STK005_IthalatNo = value;
                OnPropertyChanged("STK005_IthalatNo"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_IthalatAktarmaFlag 
        {
            get {   return this._STK005_IthalatAktarmaFlag;   } 
            set 
            {
                this._STK005_IthalatAktarmaFlag = value;
                OnPropertyChanged("STK005_IthalatAktarmaFlag"); 
            }                         
        }
       
        /// <summary> NVARCHAR (28) Allow Null </summary>
        public string STK005_BarkodNo 
        {
            get {   return this._STK005_BarkodNo;   } 
            set 
            {
                this._STK005_BarkodNo = value;
                OnPropertyChanged("STK005_BarkodNo"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_KarekodTarih 
        {
            get {   return this._STK005_KarekodTarih;   } 
            set 
            {
                this._STK005_KarekodTarih = value;
                OnPropertyChanged("STK005_KarekodTarih"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_PartiNo 
        {
            get {   return this._STK005_PartiNo;   } 
            set 
            {
                this._STK005_PartiNo = value;
                OnPropertyChanged("STK005_PartiNo"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_EFaturaTipi 
        {
            get {   return this._STK005_EFaturaTipi;   } 
            set 
            {
                this._STK005_EFaturaTipi = value;
                OnPropertyChanged("STK005_EFaturaTipi"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK005_EFaturaDurumu 
        {
            get {   return this._STK005_EFaturaDurumu;   } 
            set 
            {
                this._STK005_EFaturaDurumu = value;
                OnPropertyChanged("STK005_EFaturaDurumu"); 
            }                         
        }
       
        /// <summary> NVARCHAR (4) Allow Null </summary>
        public string STK005_EFaturaOTVListeNo 
        {
            get {   return this._STK005_EFaturaOTVListeNo;   } 
            set 
            {
                this._STK005_EFaturaOTVListeNo = value;
                OnPropertyChanged("STK005_EFaturaOTVListeNo"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_ToplamKalemIskontosu 
        {
            get {   return this._STK005_ToplamKalemIskontosu;   } 
            set 
            {
                this._STK005_ToplamKalemIskontosu = value;
                OnPropertyChanged("STK005_ToplamKalemIskontosu"); 
            }                         
        }
       
        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK005_KDVMuafAciklama 
        {
            get {   return this._STK005_KDVMuafAciklama;   } 
            set 
            {
                this._STK005_KDVMuafAciklama = value;
                OnPropertyChanged("STK005_KDVMuafAciklama"); 
            }                         
        }
       
        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string STK005_KarsiMalKodu 
        {
            get {   return this._STK005_KarsiMalKodu;   } 
            set 
            {
                this._STK005_KarsiMalKodu = value;
                OnPropertyChanged("STK005_KarsiMalKodu"); 
            }                         
        }
       
        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string STK005_UreticiMalKodu 
        {
            get {   return this._STK005_UreticiMalKodu;   } 
            set 
            {
                this._STK005_UreticiMalKodu = value;
                OnPropertyChanged("STK005_UreticiMalKodu"); 
            }                         
        }
       
        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK005_StopajOrani 
        {
            get {   return this._STK005_StopajOrani;   } 
            set 
            {
                this._STK005_StopajOrani = value;
                OnPropertyChanged("STK005_StopajOrani"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StopajTutari 
        {
            get {   return this._STK005_StopajTutari;   } 
            set 
            {
                this._STK005_StopajTutari = value;
                OnPropertyChanged("STK005_StopajTutari"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_IthalatBedeli 
        {
            get {   return this._STK005_IthalatBedeli;   } 
            set 
            {
                this._STK005_IthalatBedeli = value;
                OnPropertyChanged("STK005_IthalatBedeli"); 
            }                         
        }
       
        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string STK005_IskontoNedeni 
        {
            get {   return this._STK005_IskontoNedeni;   } 
            set 
            {
                this._STK005_IskontoNedeni = value;
                OnPropertyChanged("STK005_IskontoNedeni"); 
            }                         
        }
       
        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK005_Birim 
        {
            get {   return this._STK005_Birim;   } 
            set 
            {
                this._STK005_Birim = value;
                OnPropertyChanged("STK005_Birim"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_FaturaMiktari 
        {
            get {   return this._STK005_FaturaMiktari;   } 
            set 
            {
                this._STK005_FaturaMiktari = value;
                OnPropertyChanged("STK005_FaturaMiktari"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_OTVTevkYokVarFlag 
        {
            get {   return this._STK005_OTVTevkYokVarFlag;   } 
            set 
            {
                this._STK005_OTVTevkYokVarFlag = value;
                OnPropertyChanged("STK005_OTVTevkYokVarFlag"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_OTVTevkTutar 
        {
            get {   return this._STK005_OTVTevkTutar;   } 
            set 
            {
                this._STK005_OTVTevkTutar = value;
                OnPropertyChanged("STK005_OTVTevkTutar"); 
            }                         
        }
       
        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK005_DovizBirimFiyati 
        {
            get {   return this._STK005_DovizBirimFiyati;   } 
            set 
            {
                this._STK005_DovizBirimFiyati = value;
                OnPropertyChanged("STK005_DovizBirimFiyati"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_KDVMuafTutari 
        {
            get {   return this._STK005_KDVMuafTutari;   } 
            set 
            {
                this._STK005_KDVMuafTutari = value;
                OnPropertyChanged("STK005_KDVMuafTutari"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_IhracatNo 
        {
            get {   return this._STK005_IhracatNo;   } 
            set 
            {
                this._STK005_IhracatNo = value;
                OnPropertyChanged("STK005_IhracatNo"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_EArsivFaturaTipi 
        {
            get {   return this._STK005_EArsivFaturaTipi;   } 
            set 
            {
                this._STK005_EArsivFaturaTipi = value;
                OnPropertyChanged("STK005_EArsivFaturaTipi"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_EArsivFaturaTeslimSekli 
        {
            get {   return this._STK005_EArsivFaturaTeslimSekli;   } 
            set 
            {
                this._STK005_EArsivFaturaTeslimSekli = value;
                OnPropertyChanged("STK005_EArsivFaturaTeslimSekli"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK005_EArsivFaturaDurumu 
        {
            get {   return this._STK005_EArsivFaturaDurumu;   } 
            set 
            {
                this._STK005_EArsivFaturaDurumu = value;
                OnPropertyChanged("STK005_EArsivFaturaDurumu"); 
            }                         
        }
       
        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string STK005_Not1 
        {
            get {   return this._STK005_Not1;   } 
            set 
            {
                this._STK005_Not1 = value;
                OnPropertyChanged("STK005_Not1"); 
            }                         
        }
       
        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string STK005_Not2 
        {
            get {   return this._STK005_Not2;   } 
            set 
            {
                this._STK005_Not2 = value;
                OnPropertyChanged("STK005_Not2"); 
            }                         
        }
       
        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string STK005_Not3 
        {
            get {   return this._STK005_Not3;   } 
            set 
            {
                this._STK005_Not3 = value;
                OnPropertyChanged("STK005_Not3"); 
            }                         
        }
       
        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string STK005_Not4 
        {
            get {   return this._STK005_Not4;   } 
            set 
            {
                this._STK005_Not4 = value;
                OnPropertyChanged("STK005_Not4"); 
            }                         
        }
       
        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string STK005_Not5 
        {
            get {   return this._STK005_Not5;   } 
            set 
            {
                this._STK005_Not5 = value;
                OnPropertyChanged("STK005_Not5"); 
            }                         
        }
       
        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK005_AgirlikBirimi 
        {
            get {   return this._STK005_AgirlikBirimi;   } 
            set 
            {
                this._STK005_AgirlikBirimi = value;
                OnPropertyChanged("STK005_AgirlikBirimi"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_AgirlikBrut 
        {
            get {   return this._STK005_AgirlikBrut;   } 
            set 
            {
                this._STK005_AgirlikBrut = value;
                OnPropertyChanged("STK005_AgirlikBrut"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_AgirlikNet 
        {
            get {   return this._STK005_AgirlikNet;   } 
            set 
            {
                this._STK005_AgirlikNet = value;
                OnPropertyChanged("STK005_AgirlikNet"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_AgirlikDara 
        {
            get {   return this._STK005_AgirlikDara;   } 
            set 
            {
                this._STK005_AgirlikDara = value;
                OnPropertyChanged("STK005_AgirlikDara"); 
            }                         
        }
       
        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK005_HacimBirimi 
        {
            get {   return this._STK005_HacimBirimi;   } 
            set 
            {
                this._STK005_HacimBirimi = value;
                OnPropertyChanged("STK005_HacimBirimi"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_HacimBrut 
        {
            get {   return this._STK005_HacimBrut;   } 
            set 
            {
                this._STK005_HacimBrut = value;
                OnPropertyChanged("STK005_HacimBrut"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_HacimNet 
        {
            get {   return this._STK005_HacimNet;   } 
            set 
            {
                this._STK005_HacimNet = value;
                OnPropertyChanged("STK005_HacimNet"); 
            }                         
        }
       
        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK005_KapTipi 
        {
            get {   return this._STK005_KapTipi;   } 
            set 
            {
                this._STK005_KapTipi = value;
                OnPropertyChanged("STK005_KapTipi"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_KapAdedi 
        {
            get {   return this._STK005_KapAdedi;   } 
            set 
            {
                this._STK005_KapAdedi = value;
                OnPropertyChanged("STK005_KapAdedi"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_FiiliIhracatTarihi 
        {
            get {   return this._STK005_FiiliIhracatTarihi;   } 
            set 
            {
                this._STK005_FiiliIhracatTarihi = value;
                OnPropertyChanged("STK005_FiiliIhracatTarihi"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_RafKodu 
        {
            get {   return this._STK005_RafKodu;   } 
            set 
            {
                this._STK005_RafKodu = value;
                OnPropertyChanged("STK005_RafKodu"); 
            }                         
        }

        /// <summary> INT (4) PRIMARY KEY * </summary>
        public int pk_STK005_Row_ID 
        {
            private get {   return this._pk_STK005_Row_ID;   } 
            set 
            {
                this._pk_STK005_Row_ID = value;
                OnPropertyChanged("pk_STK005_Row_ID"); 
            }                         
        }
        #endregion /// Properties             
        #region Tablo Bilgileri & Metodlar

        public void WhereAdd(STK005E Property, object Deger, Operand And_Or = Operand.AND)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(STK005E), Property), Deger, And_Or));
        }

        public void WhereAdd(STK005E Property, Islem islem, object Deger, Operand And_Or = Operand.AND)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(STK005E), Property), islem, Deger, And_Or));
        }

        public void WhereAdd(STK005E Property, Operand In_NotIn, params object[] Degerler_Parantez)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(STK005E), Property), In_NotIn, Degerler_Parantez));
        }

        public void WhereAdd(params object[] Degerler)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Degerler));
        }

        /// <summary> Set işleminde Property " = " eşit ile otomatik başlar. </summary>
        public void SetAdd(STK005E Property, params object[] Degerler)
        {
            SetList.Add(SqlExperOperatorIslem.SetAdd(Enum.GetName(typeof(STK005E), Property), Degerler));
        }

        private List<string> WhereList = new List<string>();
        private List<string> SetList = new List<string>(); 
        private string info_FullTableName = "YNS{0}.YNS{0}.STK005";            
        private string[] info_PrimaryKeys = { "pk_STK005_Row_ID" };
        private string[] info_IdentityKeys = { "STK005_Row_ID" };

        private List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public STK005()
        {
            ChangedProperties = new List<string>();
            this.PropertyChanged += STK005_PropertyChanged;
        }

        public void AcceptChanges()
        {            
            ChangedProperties.Clear();
        }

        void STK005_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!ChangedProperties.Contains(e.PropertyName))
            {
                ChangedProperties.Add(e.PropertyName);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion  /// Tablo Bilgileri & Metodlar
    } 
}
