using System;
using System.Collections.Generic;
using System.ComponentModel;
using Birikim.Helpers;

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
    public class STK005 : INotifyPropertyChanged
    {
        #region Properties
        #region Fields  
        int sTK005_Row_ID;
        string sTK005_MalKodu;
        int? sTK005_IslemTarihi;
        byte? sTK005_GC;
        string sTK005_CariHesapKodu;
        string sTK005_EvrakSeriNo;
        decimal? sTK005_Miktari;
        decimal? sTK005_BirimFiyati;
        decimal? sTK005_Tutari;
        decimal? sTK005_Iskonto;
        decimal? sTK005_KDVTutari;
        short? sTK005_IslemTipi;
        string sTK005_Kod1;
        string sTK005_Kod2;
        string sTK005_IrsaliyeNo;
        byte? sTK005_FaturaDurumu;
        byte? sTK005_MuhasebelesmeDurumu;
        byte? sTK005_KDVDurumu;
        byte? sTK005_ParaBirimi;
        int? sTK005_SEQNo;
        string sTK005_GirenKaynak;
        int? sTK005_GirenTarih;
        string sTK005_GirenSaat;
        string sTK005_GirenKodu;
        string sTK005_GirenSurum;
        string sTK005_DegistirenKaynak;
        int? sTK005_DegistirenTarih;
        string sTK005_DegistirenSaat;
        string sTK005_DegistirenKodu;
        string sTK005_DegistirenSurum;
        byte? sTK005_IptalDurumu;
        int? sTK005_AsilEvrakTarihi;
        int? sTK005_SevkTarihi;
        byte? sTK005_OTVDahilHaric;
        decimal? sTK005_OTV;
        decimal? sTK005_Miktar2;
        decimal? sTK005_Tutar2;
        decimal? sTK005_KalemIskontoOrani1;
        decimal? sTK005_KalemIskontoOrani2;
        decimal? sTK005_KalemIskontoOrani3;
        decimal? sTK005_KalemIskontoOrani4;
        decimal? sTK005_KalemIskontoOrani5;
        decimal? sTK005_KalemIskontoTutari1;
        decimal? sTK005_KalemIskontoTutari2;
        decimal? sTK005_KalemIskontoTutari3;
        decimal? sTK005_KalemIskontoTutari4;
        decimal? sTK005_KalemIskontoTutari5;
        string sTK005_DovizCinsi;
        decimal? sTK005_DovizTutari;
        decimal? sTK005_DovizKuru;
        string sTK005_Aciklama1;
        string sTK005_Aciklama2;
        string sTK005_Depo;
        string sTK005_Kod3;
        string sTK005_Kod4;
        string sTK005_Kod5;
        string sTK005_Kod6;
        string sTK005_Kod7;
        string sTK005_Kod8;
        string sTK005_Kod9;
        string sTK005_Kod10;
        decimal? sTK005_Kod11;
        decimal? sTK005_Kod12;
        string sTK005_Vasita;
        string sTK005_MalSeriNo;
        string sTK005_EvrakSeriNo2;
        string sTK005_SiparisNo;
        int? sTK005_VadeTarihi;
        int? sTK005_IrsaliyeFaturaTarihi;
        int? sTK005_Tarih2;
        int? sTK005_SiparisTarihi;
        string sTK005_IadeFaturaNo;
        int? sTK005_IadeFaturaTarihi;
        decimal? sTK005_Masraf;
        short? sTK005_MaliyetSekli;
        decimal? sTK005_MaliyetTutari;
        int? sTK005_MaliyetTarihi;
        short? sTK005_MaliyetMuhasebelesmeSekli;
        byte? sTK005_MaliyetMuhasebelesmeDurumu;
        string sTK005_MasrafMerkezi;
        int? sTK005_MuhasebeFisTarihi;
        byte? sTK005_MuhasebeFisTipi;
        int? sTK005_MuhasebeFisNo;
        string sTK005_MuhasebeFisKodu;
        short? sTK005_MuhasebeSiraNo;
        string sTK005_MuhasebeHesapNo;
        string sTK005_MuhasebeKarsiHesapNo;
        byte? sTK005_MuhasebeYevmiyeSekli;
        byte? sTK005_Kod9Flag;
        byte? sTK005_Kod10Flag;
        decimal? sTK005_StokTrFinansmanGider;
        decimal? sTK005_StokTrVadeFarki;
        int? sTK005_StokTrKrediVadeTarihi;
        decimal? sTK005_StokTrSozFaizOrani;
        string sTK005_StokTrStokDuzHesapKodu;
        string sTK005_StokTrSmmDuzHesapKodu;
        decimal? sTK005_StokTrNonReelFinansGidSpk;
        decimal? sTK005_StokTrNonReelFinansGidMly;
        decimal? sTK005_StokTrDuzKatsayiSpk;
        decimal? sTK005_StokTrDuzKatsayiMly;
        decimal? sTK005_StokTrDuzTutarSpk;
        decimal? sTK005_StokTrDuzTutarMly;
        byte? sTK005_StokTrDonem;
        int? sTK005_StokTrYil;
        decimal? sTK005_StokTrDuzSatSpk;
        decimal? sTK005_StokTrDuzSatMly;
        short? sTK005_StokTrSatMalYontemi;
        decimal? sTK005_StokTrSatMaliyeti;
        int? sTK005_StokTrKrediAlimTarihi;
        byte? sTK005_StokTrDuzMhsFlag;
        byte? sTK005_StokTrSatMhsFlag;
        decimal? sTK005_StokTrKrediTutari;
        byte? sTK005_StokTrKrediArindirmaSekli;
        byte? sTK005_StokTrGiderTipi;
        string sTK005_StokTrIlgiliEvrak;
        int? sTK005_StokTrIlgiliTarih;
        byte? sTK005_KDVOranFlag;
        short? sTK005_EvrakTipi;
        string sTK005_TeslimCHKodu;
        string sTK005_KarsiMuhasebeKodu;
        decimal? sTK005_ExtFldTutar1;
        decimal? sTK005_FaturalasanMiktar;
        int? sTK005_KDVTevkIslemTuru;
        string sTK005_KDVTevkOrani;
        decimal? sTK005_KDVTevkTutar;
        string sTK005_IthalatNo;
        byte? sTK005_IthalatAktarmaFlag;
        string sTK005_BarkodNo;
        int? sTK005_KarekodTarih;
        string sTK005_PartiNo;
        byte? sTK005_EFaturaTipi;
        short? sTK005_EFaturaDurumu;
        string sTK005_EFaturaOTVListeNo;
        decimal? sTK005_ToplamKalemIskontosu;
        string sTK005_KDVMuafAciklama;
        string sTK005_KarsiMalKodu;
        string sTK005_UreticiMalKodu;
        decimal? sTK005_StopajOrani;
        decimal? sTK005_StopajTutari;
        decimal? sTK005_IthalatBedeli;
        string sTK005_IskontoNedeni;
        string sTK005_Birim;
        decimal? sTK005_FaturaMiktari;
        byte? sTK005_OTVTevkYokVarFlag;
        decimal? sTK005_OTVTevkTutar;
        decimal? sTK005_DovizBirimFiyati;
        decimal? sTK005_KDVMuafTutari;
        string sTK005_IhracatNo;
        byte? sTK005_EArsivFaturaTipi;
        byte? sTK005_EArsivFaturaTeslimSekli;
        short? sTK005_EArsivFaturaDurumu;
        string sTK005_Not1;
        string sTK005_Not2;
        string sTK005_Not3;
        string sTK005_Not4;
        string sTK005_Not5;
        string sTK005_AgirlikBirimi;
        decimal? sTK005_AgirlikBrut;
        decimal? sTK005_AgirlikNet;
        decimal? sTK005_AgirlikDara;
        string sTK005_HacimBirimi;
        decimal? sTK005_HacimBrut;
        decimal? sTK005_HacimNet;
        string sTK005_KapTipi;
        decimal? sTK005_KapAdedi;
        int? sTK005_FiiliIhracatTarihi;
        string sTK005_RafKodu;
        int _pk_STK005_Row_ID;

        ///EK ALANLAR Sorgulara dahil olmayacak
        public string Adres1;
        public string Adres2;
        public string Adres3;
        public string Aciklama1;
        public string Aciklama2;
        public string Aciklama3;
        #endregion /// Fields

        /// <summary> INT (4) PrimaryKey IdentityKey * </summary>
        public int STK005_Row_ID => sTK005_Row_ID;

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK005_MalKodu
        {
            get { return sTK005_MalKodu; }
            set
            {
                sTK005_MalKodu = value;
                OnPropertyChanged("STK005_MalKodu");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_IslemTarihi
        {
            get { return sTK005_IslemTarihi; }
            set
            {
                sTK005_IslemTarihi = value;
                OnPropertyChanged("STK005_IslemTarihi");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_GC
        {
            get { return sTK005_GC; }
            set
            {
                sTK005_GC = value;
                OnPropertyChanged("STK005_GC");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK005_CariHesapKodu
        {
            get { return sTK005_CariHesapKodu; }
            set
            {
                sTK005_CariHesapKodu = value;
                OnPropertyChanged("STK005_CariHesapKodu");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK005_EvrakSeriNo
        {
            get { return sTK005_EvrakSeriNo; }
            set
            {
                sTK005_EvrakSeriNo = value;
                OnPropertyChanged("STK005_EvrakSeriNo");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_Miktari
        {
            get { return sTK005_Miktari; }
            set
            {
                sTK005_Miktari = value;
                OnPropertyChanged("STK005_Miktari");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK005_BirimFiyati
        {
            get { return sTK005_BirimFiyati; }
            set
            {
                sTK005_BirimFiyati = value;
                OnPropertyChanged("STK005_BirimFiyati");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_Tutari
        {
            get { return sTK005_Tutari; }
            set
            {
                sTK005_Tutari = value;
                OnPropertyChanged("STK005_Tutari");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_Iskonto
        {
            get { return sTK005_Iskonto; }
            set
            {
                sTK005_Iskonto = value;
                OnPropertyChanged("STK005_Iskonto");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_KDVTutari
        {
            get { return sTK005_KDVTutari; }
            set
            {
                sTK005_KDVTutari = value;
                OnPropertyChanged("STK005_KDVTutari");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK005_IslemTipi
        {
            get { return sTK005_IslemTipi; }
            set
            {
                sTK005_IslemTipi = value;
                OnPropertyChanged("STK005_IslemTipi");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod1
        {
            get { return sTK005_Kod1; }
            set
            {
                sTK005_Kod1 = value;
                OnPropertyChanged("STK005_Kod1");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod2
        {
            get { return sTK005_Kod2; }
            set
            {
                sTK005_Kod2 = value;
                OnPropertyChanged("STK005_Kod2");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK005_IrsaliyeNo
        {
            get { return sTK005_IrsaliyeNo; }
            set
            {
                sTK005_IrsaliyeNo = value;
                OnPropertyChanged("STK005_IrsaliyeNo");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_FaturaDurumu
        {
            get { return sTK005_FaturaDurumu; }
            set
            {
                sTK005_FaturaDurumu = value;
                OnPropertyChanged("STK005_FaturaDurumu");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_MuhasebelesmeDurumu
        {
            get { return sTK005_MuhasebelesmeDurumu; }
            set
            {
                sTK005_MuhasebelesmeDurumu = value;
                OnPropertyChanged("STK005_MuhasebelesmeDurumu");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_KDVDurumu
        {
            get { return sTK005_KDVDurumu; }
            set
            {
                sTK005_KDVDurumu = value;
                OnPropertyChanged("STK005_KDVDurumu");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_ParaBirimi
        {
            get { return sTK005_ParaBirimi; }
            set
            {
                sTK005_ParaBirimi = value;
                OnPropertyChanged("STK005_ParaBirimi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_SEQNo
        {
            get { return sTK005_SEQNo; }
            set
            {
                sTK005_SEQNo = value;
                OnPropertyChanged("STK005_SEQNo");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK005_GirenKaynak
        {
            get { return sTK005_GirenKaynak; }
            set
            {
                sTK005_GirenKaynak = value;
                OnPropertyChanged("STK005_GirenKaynak");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_GirenTarih
        {
            get { return sTK005_GirenTarih; }
            set
            {
                sTK005_GirenTarih = value;
                OnPropertyChanged("STK005_GirenTarih");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string STK005_GirenSaat
        {
            get { return sTK005_GirenSaat; }
            set
            {
                sTK005_GirenSaat = value;
                OnPropertyChanged("STK005_GirenSaat");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK005_GirenKodu
        {
            get { return sTK005_GirenKodu; }
            set
            {
                sTK005_GirenKodu = value;
                OnPropertyChanged("STK005_GirenKodu");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string STK005_GirenSurum
        {
            get { return sTK005_GirenSurum; }
            set
            {
                sTK005_GirenSurum = value;
                OnPropertyChanged("STK005_GirenSurum");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK005_DegistirenKaynak
        {
            get { return sTK005_DegistirenKaynak; }
            set
            {
                sTK005_DegistirenKaynak = value;
                OnPropertyChanged("STK005_DegistirenKaynak");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_DegistirenTarih
        {
            get { return sTK005_DegistirenTarih; }
            set
            {
                sTK005_DegistirenTarih = value;
                OnPropertyChanged("STK005_DegistirenTarih");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string STK005_DegistirenSaat
        {
            get { return sTK005_DegistirenSaat; }
            set
            {
                sTK005_DegistirenSaat = value;
                OnPropertyChanged("STK005_DegistirenSaat");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK005_DegistirenKodu
        {
            get { return sTK005_DegistirenKodu; }
            set
            {
                sTK005_DegistirenKodu = value;
                OnPropertyChanged("STK005_DegistirenKodu");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string STK005_DegistirenSurum
        {
            get { return sTK005_DegistirenSurum; }
            set
            {
                sTK005_DegistirenSurum = value;
                OnPropertyChanged("STK005_DegistirenSurum");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_IptalDurumu
        {
            get { return sTK005_IptalDurumu; }
            set
            {
                sTK005_IptalDurumu = value;
                OnPropertyChanged("STK005_IptalDurumu");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_AsilEvrakTarihi
        {
            get { return sTK005_AsilEvrakTarihi; }
            set
            {
                sTK005_AsilEvrakTarihi = value;
                OnPropertyChanged("STK005_AsilEvrakTarihi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_SevkTarihi
        {
            get { return sTK005_SevkTarihi; }
            set
            {
                sTK005_SevkTarihi = value;
                OnPropertyChanged("STK005_SevkTarihi");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_OTVDahilHaric
        {
            get { return sTK005_OTVDahilHaric; }
            set
            {
                sTK005_OTVDahilHaric = value;
                OnPropertyChanged("STK005_OTVDahilHaric");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_OTV
        {
            get { return sTK005_OTV; }
            set
            {
                sTK005_OTV = value;
                OnPropertyChanged("STK005_OTV");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_Miktar2
        {
            get { return sTK005_Miktar2; }
            set
            {
                sTK005_Miktar2 = value;
                OnPropertyChanged("STK005_Miktar2");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_Tutar2
        {
            get { return sTK005_Tutar2; }
            set
            {
                sTK005_Tutar2 = value;
                OnPropertyChanged("STK005_Tutar2");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK005_KalemIskontoOrani1
        {
            get { return sTK005_KalemIskontoOrani1; }
            set
            {
                sTK005_KalemIskontoOrani1 = value;
                OnPropertyChanged("STK005_KalemIskontoOrani1");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK005_KalemIskontoOrani2
        {
            get { return sTK005_KalemIskontoOrani2; }
            set
            {
                sTK005_KalemIskontoOrani2 = value;
                OnPropertyChanged("STK005_KalemIskontoOrani2");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK005_KalemIskontoOrani3
        {
            get { return sTK005_KalemIskontoOrani3; }
            set
            {
                sTK005_KalemIskontoOrani3 = value;
                OnPropertyChanged("STK005_KalemIskontoOrani3");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK005_KalemIskontoOrani4
        {
            get { return sTK005_KalemIskontoOrani4; }
            set
            {
                sTK005_KalemIskontoOrani4 = value;
                OnPropertyChanged("STK005_KalemIskontoOrani4");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK005_KalemIskontoOrani5
        {
            get { return sTK005_KalemIskontoOrani5; }
            set
            {
                sTK005_KalemIskontoOrani5 = value;
                OnPropertyChanged("STK005_KalemIskontoOrani5");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_KalemIskontoTutari1
        {
            get { return sTK005_KalemIskontoTutari1; }
            set
            {
                sTK005_KalemIskontoTutari1 = value;
                OnPropertyChanged("STK005_KalemIskontoTutari1");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_KalemIskontoTutari2
        {
            get { return sTK005_KalemIskontoTutari2; }
            set
            {
                sTK005_KalemIskontoTutari2 = value;
                OnPropertyChanged("STK005_KalemIskontoTutari2");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_KalemIskontoTutari3
        {
            get { return sTK005_KalemIskontoTutari3; }
            set
            {
                sTK005_KalemIskontoTutari3 = value;
                OnPropertyChanged("STK005_KalemIskontoTutari3");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_KalemIskontoTutari4
        {
            get { return sTK005_KalemIskontoTutari4; }
            set
            {
                sTK005_KalemIskontoTutari4 = value;
                OnPropertyChanged("STK005_KalemIskontoTutari4");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_KalemIskontoTutari5
        {
            get { return sTK005_KalemIskontoTutari5; }
            set
            {
                sTK005_KalemIskontoTutari5 = value;
                OnPropertyChanged("STK005_KalemIskontoTutari5");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string STK005_DovizCinsi
        {
            get { return sTK005_DovizCinsi; }
            set
            {
                sTK005_DovizCinsi = value;
                OnPropertyChanged("STK005_DovizCinsi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_DovizTutari
        {
            get { return sTK005_DovizTutari; }
            set
            {
                sTK005_DovizTutari = value;
                OnPropertyChanged("STK005_DovizTutari");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_DovizKuru
        {
            get { return sTK005_DovizKuru; }
            set
            {
                sTK005_DovizKuru = value;
                OnPropertyChanged("STK005_DovizKuru");
            }
        }

        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string STK005_Aciklama1
        {
            get { return sTK005_Aciklama1; }
            set
            {
                sTK005_Aciklama1 = value;
                OnPropertyChanged("STK005_Aciklama1");
            }
        }

        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string STK005_Aciklama2
        {
            get { return sTK005_Aciklama2; }
            set
            {
                sTK005_Aciklama2 = value;
                OnPropertyChanged("STK005_Aciklama2");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK005_Depo
        {
            get { return sTK005_Depo; }
            set
            {
                sTK005_Depo = value;
                OnPropertyChanged("STK005_Depo");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod3
        {
            get { return sTK005_Kod3; }
            set
            {
                sTK005_Kod3 = value;
                OnPropertyChanged("STK005_Kod3");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod4
        {
            get { return sTK005_Kod4; }
            set
            {
                sTK005_Kod4 = value;
                OnPropertyChanged("STK005_Kod4");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod5
        {
            get { return sTK005_Kod5; }
            set
            {
                sTK005_Kod5 = value;
                OnPropertyChanged("STK005_Kod5");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod6
        {
            get { return sTK005_Kod6; }
            set
            {
                sTK005_Kod6 = value;
                OnPropertyChanged("STK005_Kod6");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod7
        {
            get { return sTK005_Kod7; }
            set
            {
                sTK005_Kod7 = value;
                OnPropertyChanged("STK005_Kod7");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod8
        {
            get { return sTK005_Kod8; }
            set
            {
                sTK005_Kod8 = value;
                OnPropertyChanged("STK005_Kod8");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod9
        {
            get { return sTK005_Kod9; }
            set
            {
                sTK005_Kod9 = value;
                OnPropertyChanged("STK005_Kod9");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_Kod10
        {
            get { return sTK005_Kod10; }
            set
            {
                sTK005_Kod10 = value;
                OnPropertyChanged("STK005_Kod10");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_Kod11
        {
            get { return sTK005_Kod11; }
            set
            {
                sTK005_Kod11 = value;
                OnPropertyChanged("STK005_Kod11");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_Kod12
        {
            get { return sTK005_Kod12; }
            set
            {
                sTK005_Kod12 = value;
                OnPropertyChanged("STK005_Kod12");
            }
        }

        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string STK005_Vasita
        {
            get { return sTK005_Vasita; }
            set
            {
                sTK005_Vasita = value;
                OnPropertyChanged("STK005_Vasita");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK005_MalSeriNo
        {
            get { return sTK005_MalSeriNo; }
            set
            {
                sTK005_MalSeriNo = value;
                OnPropertyChanged("STK005_MalSeriNo");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK005_EvrakSeriNo2
        {
            get { return sTK005_EvrakSeriNo2; }
            set
            {
                sTK005_EvrakSeriNo2 = value;
                OnPropertyChanged("STK005_EvrakSeriNo2");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK005_SiparisNo
        {
            get { return sTK005_SiparisNo; }
            set
            {
                sTK005_SiparisNo = value;
                OnPropertyChanged("STK005_SiparisNo");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_VadeTarihi
        {
            get { return sTK005_VadeTarihi; }
            set
            {
                sTK005_VadeTarihi = value;
                OnPropertyChanged("STK005_VadeTarihi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_IrsaliyeFaturaTarihi
        {
            get { return sTK005_IrsaliyeFaturaTarihi; }
            set
            {
                sTK005_IrsaliyeFaturaTarihi = value;
                OnPropertyChanged("STK005_IrsaliyeFaturaTarihi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_Tarih2
        {
            get { return sTK005_Tarih2; }
            set
            {
                sTK005_Tarih2 = value;
                OnPropertyChanged("STK005_Tarih2");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_SiparisTarihi
        {
            get { return sTK005_SiparisTarihi; }
            set
            {
                sTK005_SiparisTarihi = value;
                OnPropertyChanged("STK005_SiparisTarihi");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK005_IadeFaturaNo
        {
            get { return sTK005_IadeFaturaNo; }
            set
            {
                sTK005_IadeFaturaNo = value;
                OnPropertyChanged("STK005_IadeFaturaNo");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_IadeFaturaTarihi
        {
            get { return sTK005_IadeFaturaTarihi; }
            set
            {
                sTK005_IadeFaturaTarihi = value;
                OnPropertyChanged("STK005_IadeFaturaTarihi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_Masraf
        {
            get { return sTK005_Masraf; }
            set
            {
                sTK005_Masraf = value;
                OnPropertyChanged("STK005_Masraf");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK005_MaliyetSekli
        {
            get { return sTK005_MaliyetSekli; }
            set
            {
                sTK005_MaliyetSekli = value;
                OnPropertyChanged("STK005_MaliyetSekli");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_MaliyetTutari
        {
            get { return sTK005_MaliyetTutari; }
            set
            {
                sTK005_MaliyetTutari = value;
                OnPropertyChanged("STK005_MaliyetTutari");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_MaliyetTarihi
        {
            get { return sTK005_MaliyetTarihi; }
            set
            {
                sTK005_MaliyetTarihi = value;
                OnPropertyChanged("STK005_MaliyetTarihi");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK005_MaliyetMuhasebelesmeSekli
        {
            get { return sTK005_MaliyetMuhasebelesmeSekli; }
            set
            {
                sTK005_MaliyetMuhasebelesmeSekli = value;
                OnPropertyChanged("STK005_MaliyetMuhasebelesmeSekli");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_MaliyetMuhasebelesmeDurumu
        {
            get { return sTK005_MaliyetMuhasebelesmeDurumu; }
            set
            {
                sTK005_MaliyetMuhasebelesmeDurumu = value;
                OnPropertyChanged("STK005_MaliyetMuhasebelesmeDurumu");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_MasrafMerkezi
        {
            get { return sTK005_MasrafMerkezi; }
            set
            {
                sTK005_MasrafMerkezi = value;
                OnPropertyChanged("STK005_MasrafMerkezi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_MuhasebeFisTarihi
        {
            get { return sTK005_MuhasebeFisTarihi; }
            set
            {
                sTK005_MuhasebeFisTarihi = value;
                OnPropertyChanged("STK005_MuhasebeFisTarihi");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_MuhasebeFisTipi
        {
            get { return sTK005_MuhasebeFisTipi; }
            set
            {
                sTK005_MuhasebeFisTipi = value;
                OnPropertyChanged("STK005_MuhasebeFisTipi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_MuhasebeFisNo
        {
            get { return sTK005_MuhasebeFisNo; }
            set
            {
                sTK005_MuhasebeFisNo = value;
                OnPropertyChanged("STK005_MuhasebeFisNo");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string STK005_MuhasebeFisKodu
        {
            get { return sTK005_MuhasebeFisKodu; }
            set
            {
                sTK005_MuhasebeFisKodu = value;
                OnPropertyChanged("STK005_MuhasebeFisKodu");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK005_MuhasebeSiraNo
        {
            get { return sTK005_MuhasebeSiraNo; }
            set
            {
                sTK005_MuhasebeSiraNo = value;
                OnPropertyChanged("STK005_MuhasebeSiraNo");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_MuhasebeHesapNo
        {
            get { return sTK005_MuhasebeHesapNo; }
            set
            {
                sTK005_MuhasebeHesapNo = value;
                OnPropertyChanged("STK005_MuhasebeHesapNo");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_MuhasebeKarsiHesapNo
        {
            get { return sTK005_MuhasebeKarsiHesapNo; }
            set
            {
                sTK005_MuhasebeKarsiHesapNo = value;
                OnPropertyChanged("STK005_MuhasebeKarsiHesapNo");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_MuhasebeYevmiyeSekli
        {
            get { return sTK005_MuhasebeYevmiyeSekli; }
            set
            {
                sTK005_MuhasebeYevmiyeSekli = value;
                OnPropertyChanged("STK005_MuhasebeYevmiyeSekli");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_Kod9Flag
        {
            get { return sTK005_Kod9Flag; }
            set
            {
                sTK005_Kod9Flag = value;
                OnPropertyChanged("STK005_Kod9Flag");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_Kod10Flag
        {
            get { return sTK005_Kod10Flag; }
            set
            {
                sTK005_Kod10Flag = value;
                OnPropertyChanged("STK005_Kod10Flag");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrFinansmanGider
        {
            get { return sTK005_StokTrFinansmanGider; }
            set
            {
                sTK005_StokTrFinansmanGider = value;
                OnPropertyChanged("STK005_StokTrFinansmanGider");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrVadeFarki
        {
            get { return sTK005_StokTrVadeFarki; }
            set
            {
                sTK005_StokTrVadeFarki = value;
                OnPropertyChanged("STK005_StokTrVadeFarki");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_StokTrKrediVadeTarihi
        {
            get { return sTK005_StokTrKrediVadeTarihi; }
            set
            {
                sTK005_StokTrKrediVadeTarihi = value;
                OnPropertyChanged("STK005_StokTrKrediVadeTarihi");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK005_StokTrSozFaizOrani
        {
            get { return sTK005_StokTrSozFaizOrani; }
            set
            {
                sTK005_StokTrSozFaizOrani = value;
                OnPropertyChanged("STK005_StokTrSozFaizOrani");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_StokTrStokDuzHesapKodu
        {
            get { return sTK005_StokTrStokDuzHesapKodu; }
            set
            {
                sTK005_StokTrStokDuzHesapKodu = value;
                OnPropertyChanged("STK005_StokTrStokDuzHesapKodu");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_StokTrSmmDuzHesapKodu
        {
            get { return sTK005_StokTrSmmDuzHesapKodu; }
            set
            {
                sTK005_StokTrSmmDuzHesapKodu = value;
                OnPropertyChanged("STK005_StokTrSmmDuzHesapKodu");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrNonReelFinansGidSpk
        {
            get { return sTK005_StokTrNonReelFinansGidSpk; }
            set
            {
                sTK005_StokTrNonReelFinansGidSpk = value;
                OnPropertyChanged("STK005_StokTrNonReelFinansGidSpk");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrNonReelFinansGidMly
        {
            get { return sTK005_StokTrNonReelFinansGidMly; }
            set
            {
                sTK005_StokTrNonReelFinansGidMly = value;
                OnPropertyChanged("STK005_StokTrNonReelFinansGidMly");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrDuzKatsayiSpk
        {
            get { return sTK005_StokTrDuzKatsayiSpk; }
            set
            {
                sTK005_StokTrDuzKatsayiSpk = value;
                OnPropertyChanged("STK005_StokTrDuzKatsayiSpk");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrDuzKatsayiMly
        {
            get { return sTK005_StokTrDuzKatsayiMly; }
            set
            {
                sTK005_StokTrDuzKatsayiMly = value;
                OnPropertyChanged("STK005_StokTrDuzKatsayiMly");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrDuzTutarSpk
        {
            get { return sTK005_StokTrDuzTutarSpk; }
            set
            {
                sTK005_StokTrDuzTutarSpk = value;
                OnPropertyChanged("STK005_StokTrDuzTutarSpk");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrDuzTutarMly
        {
            get { return sTK005_StokTrDuzTutarMly; }
            set
            {
                sTK005_StokTrDuzTutarMly = value;
                OnPropertyChanged("STK005_StokTrDuzTutarMly");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_StokTrDonem
        {
            get { return sTK005_StokTrDonem; }
            set
            {
                sTK005_StokTrDonem = value;
                OnPropertyChanged("STK005_StokTrDonem");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_StokTrYil
        {
            get { return sTK005_StokTrYil; }
            set
            {
                sTK005_StokTrYil = value;
                OnPropertyChanged("STK005_StokTrYil");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrDuzSatSpk
        {
            get { return sTK005_StokTrDuzSatSpk; }
            set
            {
                sTK005_StokTrDuzSatSpk = value;
                OnPropertyChanged("STK005_StokTrDuzSatSpk");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrDuzSatMly
        {
            get { return sTK005_StokTrDuzSatMly; }
            set
            {
                sTK005_StokTrDuzSatMly = value;
                OnPropertyChanged("STK005_StokTrDuzSatMly");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK005_StokTrSatMalYontemi
        {
            get { return sTK005_StokTrSatMalYontemi; }
            set
            {
                sTK005_StokTrSatMalYontemi = value;
                OnPropertyChanged("STK005_StokTrSatMalYontemi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrSatMaliyeti
        {
            get { return sTK005_StokTrSatMaliyeti; }
            set
            {
                sTK005_StokTrSatMaliyeti = value;
                OnPropertyChanged("STK005_StokTrSatMaliyeti");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_StokTrKrediAlimTarihi
        {
            get { return sTK005_StokTrKrediAlimTarihi; }
            set
            {
                sTK005_StokTrKrediAlimTarihi = value;
                OnPropertyChanged("STK005_StokTrKrediAlimTarihi");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_StokTrDuzMhsFlag
        {
            get { return sTK005_StokTrDuzMhsFlag; }
            set
            {
                sTK005_StokTrDuzMhsFlag = value;
                OnPropertyChanged("STK005_StokTrDuzMhsFlag");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_StokTrSatMhsFlag
        {
            get { return sTK005_StokTrSatMhsFlag; }
            set
            {
                sTK005_StokTrSatMhsFlag = value;
                OnPropertyChanged("STK005_StokTrSatMhsFlag");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StokTrKrediTutari
        {
            get { return sTK005_StokTrKrediTutari; }
            set
            {
                sTK005_StokTrKrediTutari = value;
                OnPropertyChanged("STK005_StokTrKrediTutari");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_StokTrKrediArindirmaSekli
        {
            get { return sTK005_StokTrKrediArindirmaSekli; }
            set
            {
                sTK005_StokTrKrediArindirmaSekli = value;
                OnPropertyChanged("STK005_StokTrKrediArindirmaSekli");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_StokTrGiderTipi
        {
            get { return sTK005_StokTrGiderTipi; }
            set
            {
                sTK005_StokTrGiderTipi = value;
                OnPropertyChanged("STK005_StokTrGiderTipi");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK005_StokTrIlgiliEvrak
        {
            get { return sTK005_StokTrIlgiliEvrak; }
            set
            {
                sTK005_StokTrIlgiliEvrak = value;
                OnPropertyChanged("STK005_StokTrIlgiliEvrak");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_StokTrIlgiliTarih
        {
            get { return sTK005_StokTrIlgiliTarih; }
            set
            {
                sTK005_StokTrIlgiliTarih = value;
                OnPropertyChanged("STK005_StokTrIlgiliTarih");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_KDVOranFlag
        {
            get { return sTK005_KDVOranFlag; }
            set
            {
                sTK005_KDVOranFlag = value;
                OnPropertyChanged("STK005_KDVOranFlag");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK005_EvrakTipi
        {
            get { return sTK005_EvrakTipi; }
            set
            {
                sTK005_EvrakTipi = value;
                OnPropertyChanged("STK005_EvrakTipi");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK005_TeslimCHKodu
        {
            get { return sTK005_TeslimCHKodu; }
            set
            {
                sTK005_TeslimCHKodu = value;
                OnPropertyChanged("STK005_TeslimCHKodu");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_KarsiMuhasebeKodu
        {
            get { return sTK005_KarsiMuhasebeKodu; }
            set
            {
                sTK005_KarsiMuhasebeKodu = value;
                OnPropertyChanged("STK005_KarsiMuhasebeKodu");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_ExtFldTutar1
        {
            get { return sTK005_ExtFldTutar1; }
            set
            {
                sTK005_ExtFldTutar1 = value;
                OnPropertyChanged("STK005_ExtFldTutar1");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_FaturalasanMiktar
        {
            get { return sTK005_FaturalasanMiktar; }
            set
            {
                sTK005_FaturalasanMiktar = value;
                OnPropertyChanged("STK005_FaturalasanMiktar");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_KDVTevkIslemTuru
        {
            get { return sTK005_KDVTevkIslemTuru; }
            set
            {
                sTK005_KDVTevkIslemTuru = value;
                OnPropertyChanged("STK005_KDVTevkIslemTuru");
            }
        }

        /// <summary> NVARCHAR (14) Allow Null </summary>
        public string STK005_KDVTevkOrani
        {
            get { return sTK005_KDVTevkOrani; }
            set
            {
                sTK005_KDVTevkOrani = value;
                OnPropertyChanged("STK005_KDVTevkOrani");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_KDVTevkTutar
        {
            get { return sTK005_KDVTevkTutar; }
            set
            {
                sTK005_KDVTevkTutar = value;
                OnPropertyChanged("STK005_KDVTevkTutar");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_IthalatNo
        {
            get { return sTK005_IthalatNo; }
            set
            {
                sTK005_IthalatNo = value;
                OnPropertyChanged("STK005_IthalatNo");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_IthalatAktarmaFlag
        {
            get { return sTK005_IthalatAktarmaFlag; }
            set
            {
                sTK005_IthalatAktarmaFlag = value;
                OnPropertyChanged("STK005_IthalatAktarmaFlag");
            }
        }

        /// <summary> NVARCHAR (28) Allow Null </summary>
        public string STK005_BarkodNo
        {
            get { return sTK005_BarkodNo; }
            set
            {
                sTK005_BarkodNo = value;
                OnPropertyChanged("STK005_BarkodNo");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_KarekodTarih
        {
            get { return sTK005_KarekodTarih; }
            set
            {
                sTK005_KarekodTarih = value;
                OnPropertyChanged("STK005_KarekodTarih");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_PartiNo
        {
            get { return sTK005_PartiNo; }
            set
            {
                sTK005_PartiNo = value;
                OnPropertyChanged("STK005_PartiNo");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_EFaturaTipi
        {
            get { return sTK005_EFaturaTipi; }
            set
            {
                sTK005_EFaturaTipi = value;
                OnPropertyChanged("STK005_EFaturaTipi");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK005_EFaturaDurumu
        {
            get { return sTK005_EFaturaDurumu; }
            set
            {
                sTK005_EFaturaDurumu = value;
                OnPropertyChanged("STK005_EFaturaDurumu");
            }
        }

        /// <summary> NVARCHAR (4) Allow Null </summary>
        public string STK005_EFaturaOTVListeNo
        {
            get { return sTK005_EFaturaOTVListeNo; }
            set
            {
                sTK005_EFaturaOTVListeNo = value;
                OnPropertyChanged("STK005_EFaturaOTVListeNo");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_ToplamKalemIskontosu
        {
            get { return sTK005_ToplamKalemIskontosu; }
            set
            {
                sTK005_ToplamKalemIskontosu = value;
                OnPropertyChanged("STK005_ToplamKalemIskontosu");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK005_KDVMuafAciklama
        {
            get { return sTK005_KDVMuafAciklama; }
            set
            {
                sTK005_KDVMuafAciklama = value;
                OnPropertyChanged("STK005_KDVMuafAciklama");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string STK005_KarsiMalKodu
        {
            get { return sTK005_KarsiMalKodu; }
            set
            {
                sTK005_KarsiMalKodu = value;
                OnPropertyChanged("STK005_KarsiMalKodu");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string STK005_UreticiMalKodu
        {
            get { return sTK005_UreticiMalKodu; }
            set
            {
                sTK005_UreticiMalKodu = value;
                OnPropertyChanged("STK005_UreticiMalKodu");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK005_StopajOrani
        {
            get { return sTK005_StopajOrani; }
            set
            {
                sTK005_StopajOrani = value;
                OnPropertyChanged("STK005_StopajOrani");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_StopajTutari
        {
            get { return sTK005_StopajTutari; }
            set
            {
                sTK005_StopajTutari = value;
                OnPropertyChanged("STK005_StopajTutari");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_IthalatBedeli
        {
            get { return sTK005_IthalatBedeli; }
            set
            {
                sTK005_IthalatBedeli = value;
                OnPropertyChanged("STK005_IthalatBedeli");
            }
        }

        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string STK005_IskontoNedeni
        {
            get { return sTK005_IskontoNedeni; }
            set
            {
                sTK005_IskontoNedeni = value;
                OnPropertyChanged("STK005_IskontoNedeni");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK005_Birim
        {
            get { return sTK005_Birim; }
            set
            {
                sTK005_Birim = value;
                OnPropertyChanged("STK005_Birim");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_FaturaMiktari
        {
            get { return sTK005_FaturaMiktari; }
            set
            {
                sTK005_FaturaMiktari = value;
                OnPropertyChanged("STK005_FaturaMiktari");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_OTVTevkYokVarFlag
        {
            get { return sTK005_OTVTevkYokVarFlag; }
            set
            {
                sTK005_OTVTevkYokVarFlag = value;
                OnPropertyChanged("STK005_OTVTevkYokVarFlag");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_OTVTevkTutar
        {
            get { return sTK005_OTVTevkTutar; }
            set
            {
                sTK005_OTVTevkTutar = value;
                OnPropertyChanged("STK005_OTVTevkTutar");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK005_DovizBirimFiyati
        {
            get { return sTK005_DovizBirimFiyati; }
            set
            {
                sTK005_DovizBirimFiyati = value;
                OnPropertyChanged("STK005_DovizBirimFiyati");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_KDVMuafTutari
        {
            get { return sTK005_KDVMuafTutari; }
            set
            {
                sTK005_KDVMuafTutari = value;
                OnPropertyChanged("STK005_KDVMuafTutari");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_IhracatNo
        {
            get { return sTK005_IhracatNo; }
            set
            {
                sTK005_IhracatNo = value;
                OnPropertyChanged("STK005_IhracatNo");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_EArsivFaturaTipi
        {
            get { return sTK005_EArsivFaturaTipi; }
            set
            {
                sTK005_EArsivFaturaTipi = value;
                OnPropertyChanged("STK005_EArsivFaturaTipi");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK005_EArsivFaturaTeslimSekli
        {
            get { return sTK005_EArsivFaturaTeslimSekli; }
            set
            {
                sTK005_EArsivFaturaTeslimSekli = value;
                OnPropertyChanged("STK005_EArsivFaturaTeslimSekli");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK005_EArsivFaturaDurumu
        {
            get { return sTK005_EArsivFaturaDurumu; }
            set
            {
                sTK005_EArsivFaturaDurumu = value;
                OnPropertyChanged("STK005_EArsivFaturaDurumu");
            }
        }

        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string STK005_Not1
        {
            get { return sTK005_Not1; }
            set
            {
                sTK005_Not1 = value;
                OnPropertyChanged("STK005_Not1");
            }
        }

        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string STK005_Not2
        {
            get { return sTK005_Not2; }
            set
            {
                sTK005_Not2 = value;
                OnPropertyChanged("STK005_Not2");
            }
        }

        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string STK005_Not3
        {
            get { return sTK005_Not3; }
            set
            {
                sTK005_Not3 = value;
                OnPropertyChanged("STK005_Not3");
            }
        }

        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string STK005_Not4
        {
            get { return sTK005_Not4; }
            set
            {
                sTK005_Not4 = value;
                OnPropertyChanged("STK005_Not4");
            }
        }

        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string STK005_Not5
        {
            get { return sTK005_Not5; }
            set
            {
                sTK005_Not5 = value;
                OnPropertyChanged("STK005_Not5");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK005_AgirlikBirimi
        {
            get { return sTK005_AgirlikBirimi; }
            set
            {
                sTK005_AgirlikBirimi = value;
                OnPropertyChanged("STK005_AgirlikBirimi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_AgirlikBrut
        {
            get { return sTK005_AgirlikBrut; }
            set
            {
                sTK005_AgirlikBrut = value;
                OnPropertyChanged("STK005_AgirlikBrut");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_AgirlikNet
        {
            get { return sTK005_AgirlikNet; }
            set
            {
                sTK005_AgirlikNet = value;
                OnPropertyChanged("STK005_AgirlikNet");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_AgirlikDara
        {
            get { return sTK005_AgirlikDara; }
            set
            {
                sTK005_AgirlikDara = value;
                OnPropertyChanged("STK005_AgirlikDara");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK005_HacimBirimi
        {
            get { return sTK005_HacimBirimi; }
            set
            {
                sTK005_HacimBirimi = value;
                OnPropertyChanged("STK005_HacimBirimi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_HacimBrut
        {
            get { return sTK005_HacimBrut; }
            set
            {
                sTK005_HacimBrut = value;
                OnPropertyChanged("STK005_HacimBrut");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_HacimNet
        {
            get { return sTK005_HacimNet; }
            set
            {
                sTK005_HacimNet = value;
                OnPropertyChanged("STK005_HacimNet");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK005_KapTipi
        {
            get { return sTK005_KapTipi; }
            set
            {
                sTK005_KapTipi = value;
                OnPropertyChanged("STK005_KapTipi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK005_KapAdedi
        {
            get { return sTK005_KapAdedi; }
            set
            {
                sTK005_KapAdedi = value;
                OnPropertyChanged("STK005_KapAdedi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK005_FiiliIhracatTarihi
        {
            get { return sTK005_FiiliIhracatTarihi; }
            set
            {
                sTK005_FiiliIhracatTarihi = value;
                OnPropertyChanged("STK005_FiiliIhracatTarihi");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK005_RafKodu
        {
            get { return sTK005_RafKodu; }
            set
            {
                sTK005_RafKodu = value;
                OnPropertyChanged("STK005_RafKodu");
            }
        }

        /// <summary> INT (4) PRIMARY KEY * </summary>
        public int pk_STK005_Row_ID
        {
            private get { return _pk_STK005_Row_ID; }
            set
            {
                _pk_STK005_Row_ID = value;
                OnPropertyChanged("pk_STK005_Row_ID");
            }
        }
        #endregion /// Properties             
        #region Tablo Bilgileri & Metodlar

        public void WhereAdd(STK005E Property, object deger, Operand and_Or = Operand.AND)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(STK005E), Property), deger, and_Or));
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

        List<string> WhereList = new List<string>();
        List<string> SetList = new List<string>();
        string info_FullTableName = "YNS{0}.YNS{0}.STK005";
        string[] info_PrimaryKeys = { "pk_STK005_Row_ID" };
        string[] info_IdentityKeys = { "STK005_Row_ID" };

        List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public STK005()
        {
            ChangedProperties = new List<string>();
            PropertyChanged += STK005_PropertyChanged;
        }

        public void AcceptChanges() => ChangedProperties.Clear();

        void STK005_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!ChangedProperties.Contains(e.PropertyName))
            {
                ChangedProperties.Add(e.PropertyName);
            }
        }

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion  /// Tablo Bilgileri & Metodlar
    }
}