using DevHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Wms12m.Entity
{
    #region CAR003E Enum 
    public enum CAR003E
    {
        CAR003_Row_ID,
        CAR003_HesapKodu,
        CAR003_Tarih,
        CAR003_IslemTipi,
        CAR003_EvrakSeriNo,
        CAR003_Aciklama,
        CAR003_BA,
        CAR003_Tutar,
        CAR003_VadeTarihi,
        CAR003_KarsiEvrakSeriNo,
        CAR003_Kod1,
        CAR003_Kod2,
        CAR003_KDVOrani,
        CAR003_KDVDahilHaric,
        CAR003_MuhasebelesmeDurumu,
        CAR003_ParaBirimi,
        CAR003_SEQNo,
        CAR003_GirenKaynak,
        CAR003_GirenTarih,
        CAR003_GirenSaat,
        CAR003_GirenKodu,
        CAR003_GirenSurum,
        CAR003_DegistirenKaynak,
        CAR003_DegistirenTarih,
        CAR003_DegistirenSaat,
        CAR003_DegistirenKodu,
        CAR003_DegistirenSurum,
        CAR003_IptalDurumu,
        CAR003_AsilEvrakTarihi,
        CAR003_otvdahilharic,
        CAR003_otvtutari,
        CAR003_KarsiHesapKodu,
        CAR003_Kod3,
        CAR003_Kod4,
        CAR003_Kod5,
        CAR003_Kod6,
        CAR003_Kod7,
        CAR003_Kod8,
        CAR003_Kod9,
        CAR003_Kod10,
        CAR003_Kod11,
        CAR003_Kod12,
        CAR003_Tutar2,
        CAR003_Tarih2,
        CAR003_Aciklama2,
        CAR003_EvrakSeriNo2,
        CAR003_DovizCinsi,
        CAR003_DovizTutari,
        CAR003_DovizKuru,
        CAR003_SenetCekBordroNo,
        CAR003_SenetCekPozisyonTipi,
        CAR003_MuhasebelesmeSekli,
        CAR003_MuhasebeFisTarihi,
        CAR003_MuhasebeTipi,
        CAR003_MuhasebeFisNumarasi,
        CAR003_MuhasebeFisKodu,
        CAR003_MuhasebeSiraNo,
        CAR003_MuhasebeHesapNo,
        CAR003_MuhasebeKarsiHeaspNo,
        CAR003_MuhasebeYevmiyeSekli,
        CAR003_IskontoTuru,
        CAR003_EvrakSeriNo3,
        CAR003_MasrafMerkezi,
        CAR003_VadeFarkiTarihi,
        CAR003_VadeFarkiTutari,
        CAR003_FaizOrani,
        CAR003_Ulke,
        CAR003_VergiHesapNo,
        CAR003_Unvani,
        CAR003_EvrakSayisi,
        CAR003_EvrakTipi,
        CAR003_MHSMaddeNo,
        CAR003_IBAN,
        CAR003_KKPOSTableRowID,
        CAR003_KKTaksitSayisi,
        CAR003_KKTaksitNo,
        CAR003_KKKomisyonID,
        CAR003_KDVTevkIslemTuru,
        CAR003_KDVTevkOrani,
        CAR003_IthalatNo,
        CAR003_IthalatAktarmaFlag,
        CAR003_KDVTevkIslemBedeli,
        CAR003_OdemeTipi,
        CAR003_YevmiyeEvrakTip,
        CAR003_EvrakTipAciklama,
        CAR003_EFaturaTipi,
        CAR003_EFaturaDurumu,
        CAR003_EFaturaOTVListeNo,
        CAR003_EFaturaDonemBas,
        CAR003_EFaturaDonemBit,
        CAR003_EFaturaSure,
        CAR003_EFaturaSureBirimi,
        CAR003_EFaturaDonemAciklama,
        CAR003_EFaturaNot,
        CAR003_EFaturaReferansNo,
        CAR003_YevEvrakNo,
        CAR003_YevEvrakTarihi,
        CAR003_StopajOrani,
        CAR003_OdemeTuru,
        CAR003_TalimatNo,
        CAR003_IhracatNo,
        CAR003_GrpSiraNo,
        CAR003_EArsivFaturaTipi,
        CAR003_EArsivFaturaTeslimSekli,
        CAR003_EArsivFaturaDurumu,
        CAR003_EArsivAdres,
        CAR003_EArsivSemt,
        CAR003_EArsivIL,
        CAR003_Unvani2,
        CAR003_YOKCSeriNo,
        CAR003_YOKCZRaporuNo,
        CAR003_YOKCBelgeTipi,
        CAR003_YOKCBilgiFisiTipi,
        CAR003_YOKCFisNo,
        CAR003_YOKCFisTarihi,
        CAR003_OdemeTurKodu,
        CAR003_VergiDairesiKodu,
        CAR003_FiiliIhracatTarihi,
        CAR003_YOKCFisSaat,
        CAR003_YOKCDuzenlemeTip,
        CAR003_YOKCBankaOnayKod,
        CAR003_YOKCUniqueID

    }
    #endregion /// CAR003E Enum                
    public class CAR003 : INotifyPropertyChanged
    {
        #region Properties
        #region Fields  
        int cAR003_Row_ID;
        string cAR003_HesapKodu;
        int? cAR003_Tarih;
        short? cAR003_IslemTipi;
        string cAR003_EvrakSeriNo;
        string cAR003_Aciklama;
        byte? cAR003_BA;
        decimal? cAR003_Tutar;
        int? cAR003_VadeTarihi;
        string cAR003_KarsiEvrakSeriNo;
        string cAR003_Kod1;
        string cAR003_Kod2;
        byte? cAR003_KDVOrani;
        byte? cAR003_KDVDahilHaric;
        byte? cAR003_MuhasebelesmeDurumu;
        byte? cAR003_ParaBirimi;
        int? cAR003_SEQNo;
        string cAR003_GirenKaynak;
        int? cAR003_GirenTarih;
        string cAR003_GirenSaat;
        string cAR003_GirenKodu;
        string cAR003_GirenSurum;
        string cAR003_DegistirenKaynak;
        int? cAR003_DegistirenTarih;
        string cAR003_DegistirenSaat;
        string cAR003_DegistirenKodu;
        string cAR003_DegistirenSurum;
        byte? cAR003_IptalDurumu;
        int? cAR003_AsilEvrakTarihi;
        byte? cAR003_otvdahilharic;
        decimal? cAR003_otvtutari;
        string cAR003_KarsiHesapKodu;
        string cAR003_Kod3;
        string cAR003_Kod4;
        string cAR003_Kod5;
        string cAR003_Kod6;
        string cAR003_Kod7;
        string cAR003_Kod8;
        string cAR003_Kod9;
        string cAR003_Kod10;
        decimal? cAR003_Kod11;
        decimal? cAR003_Kod12;
        decimal? cAR003_Tutar2;
        int? cAR003_Tarih2;
        string cAR003_Aciklama2;
        string cAR003_EvrakSeriNo2;
        string cAR003_DovizCinsi;
        decimal? cAR003_DovizTutari;
        decimal? cAR003_DovizKuru;
        string cAR003_SenetCekBordroNo;
        short? cAR003_SenetCekPozisyonTipi;
        short? cAR003_MuhasebelesmeSekli;
        int? cAR003_MuhasebeFisTarihi;
        byte? cAR003_MuhasebeTipi;
        int? cAR003_MuhasebeFisNumarasi;
        string cAR003_MuhasebeFisKodu;
        short? cAR003_MuhasebeSiraNo;
        string cAR003_MuhasebeHesapNo;
        string cAR003_MuhasebeKarsiHeaspNo;
        byte? cAR003_MuhasebeYevmiyeSekli;
        string cAR003_IskontoTuru;
        string cAR003_EvrakSeriNo3;
        string cAR003_MasrafMerkezi;
        int? cAR003_VadeFarkiTarihi;
        decimal? cAR003_VadeFarkiTutari;
        decimal? cAR003_FaizOrani;
        int? cAR003_Ulke;
        string cAR003_VergiHesapNo;
        string cAR003_Unvani;
        short? cAR003_EvrakSayisi;
        short? cAR003_EvrakTipi;
        int? cAR003_MHSMaddeNo;
        string cAR003_IBAN;
        int? cAR003_KKPOSTableRowID;
        short? cAR003_KKTaksitSayisi;
        short? cAR003_KKTaksitNo;
        int? cAR003_KKKomisyonID;
        int? cAR003_KDVTevkIslemTuru;
        string cAR003_KDVTevkOrani;
        string cAR003_IthalatNo;
        byte? cAR003_IthalatAktarmaFlag;
        decimal? cAR003_KDVTevkIslemBedeli;
        string cAR003_OdemeTipi;
        byte? cAR003_YevmiyeEvrakTip;
        string cAR003_EvrakTipAciklama;
        byte? cAR003_EFaturaTipi;
        short? cAR003_EFaturaDurumu;
        string cAR003_EFaturaOTVListeNo;
        int? cAR003_EFaturaDonemBas;
        int? cAR003_EFaturaDonemBit;
        int? cAR003_EFaturaSure;
        byte? cAR003_EFaturaSureBirimi;
        string cAR003_EFaturaDonemAciklama;
        string cAR003_EFaturaNot;
        string cAR003_EFaturaReferansNo;
        string cAR003_YevEvrakNo;
        int? cAR003_YevEvrakTarihi;
        decimal? cAR003_StopajOrani;
        short? cAR003_OdemeTuru;
        string cAR003_TalimatNo;
        string cAR003_IhracatNo;
        int? cAR003_GrpSiraNo;
        byte? cAR003_EArsivFaturaTipi;
        byte? cAR003_EArsivFaturaTeslimSekli;
        short? cAR003_EArsivFaturaDurumu;
        string cAR003_EArsivAdres;
        string cAR003_EArsivSemt;
        string cAR003_EArsivIL;
        string cAR003_Unvani2;
        string cAR003_YOKCSeriNo;
        int? cAR003_YOKCZRaporuNo;
        byte? cAR003_YOKCBelgeTipi;
        byte? cAR003_YOKCBilgiFisiTipi;
        int? cAR003_YOKCFisNo;
        int? cAR003_YOKCFisTarihi;
        string cAR003_OdemeTurKodu;
        int? cAR003_VergiDairesiKodu;
        int? cAR003_FiiliIhracatTarihi;
        string cAR003_YOKCFisSaat;
        byte? cAR003_YOKCDuzenlemeTip;
        string cAR003_YOKCBankaOnayKod;
        string cAR003_YOKCUniqueID;
        int _pk_CAR003_Row_ID;
        #endregion /// Fields

        /// <summary> INT (4) PrimaryKey IdentityKey * </summary>
        public int CAR003_Row_ID => cAR003_Row_ID;

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR003_HesapKodu
        {
            get { return cAR003_HesapKodu; }
            set
            {
                cAR003_HesapKodu = value;
                OnPropertyChanged("CAR003_HesapKodu");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_Tarih
        {
            get { return cAR003_Tarih; }
            set
            {
                cAR003_Tarih = value;
                OnPropertyChanged("CAR003_Tarih");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_IslemTipi
        {
            get { return cAR003_IslemTipi; }
            set
            {
                cAR003_IslemTipi = value;
                OnPropertyChanged("CAR003_IslemTipi");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR003_EvrakSeriNo
        {
            get { return cAR003_EvrakSeriNo; }
            set
            {
                cAR003_EvrakSeriNo = value;
                OnPropertyChanged("CAR003_EvrakSeriNo");
            }
        }

        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR003_Aciklama
        {
            get { return cAR003_Aciklama; }
            set
            {
                cAR003_Aciklama = value;
                OnPropertyChanged("CAR003_Aciklama");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_BA
        {
            get { return cAR003_BA; }
            set
            {
                cAR003_BA = value;
                OnPropertyChanged("CAR003_BA");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR003_Tutar
        {
            get { return cAR003_Tutar; }
            set
            {
                cAR003_Tutar = value;
                OnPropertyChanged("CAR003_Tutar");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_VadeTarihi
        {
            get { return cAR003_VadeTarihi; }
            set
            {
                cAR003_VadeTarihi = value;
                OnPropertyChanged("CAR003_VadeTarihi");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR003_KarsiEvrakSeriNo
        {
            get { return cAR003_KarsiEvrakSeriNo; }
            set
            {
                cAR003_KarsiEvrakSeriNo = value;
                OnPropertyChanged("CAR003_KarsiEvrakSeriNo");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_Kod1
        {
            get { return cAR003_Kod1; }
            set
            {
                cAR003_Kod1 = value;
                OnPropertyChanged("CAR003_Kod1");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_Kod2
        {
            get { return cAR003_Kod2; }
            set
            {
                cAR003_Kod2 = value;
                OnPropertyChanged("CAR003_Kod2");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_KDVOrani
        {
            get { return cAR003_KDVOrani; }
            set
            {
                cAR003_KDVOrani = value;
                OnPropertyChanged("CAR003_KDVOrani");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_KDVDahilHaric
        {
            get { return cAR003_KDVDahilHaric; }
            set
            {
                cAR003_KDVDahilHaric = value;
                OnPropertyChanged("CAR003_KDVDahilHaric");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_MuhasebelesmeDurumu
        {
            get { return cAR003_MuhasebelesmeDurumu; }
            set
            {
                cAR003_MuhasebelesmeDurumu = value;
                OnPropertyChanged("CAR003_MuhasebelesmeDurumu");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_ParaBirimi
        {
            get { return cAR003_ParaBirimi; }
            set
            {
                cAR003_ParaBirimi = value;
                OnPropertyChanged("CAR003_ParaBirimi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_SEQNo
        {
            get { return cAR003_SEQNo; }
            set
            {
                cAR003_SEQNo = value;
                OnPropertyChanged("CAR003_SEQNo");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR003_GirenKaynak
        {
            get { return cAR003_GirenKaynak; }
            set
            {
                cAR003_GirenKaynak = value;
                OnPropertyChanged("CAR003_GirenKaynak");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_GirenTarih
        {
            get { return cAR003_GirenTarih; }
            set
            {
                cAR003_GirenTarih = value;
                OnPropertyChanged("CAR003_GirenTarih");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR003_GirenSaat
        {
            get { return cAR003_GirenSaat; }
            set
            {
                cAR003_GirenSaat = value;
                OnPropertyChanged("CAR003_GirenSaat");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_GirenKodu
        {
            get { return cAR003_GirenKodu; }
            set
            {
                cAR003_GirenKodu = value;
                OnPropertyChanged("CAR003_GirenKodu");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR003_GirenSurum
        {
            get { return cAR003_GirenSurum; }
            set
            {
                cAR003_GirenSurum = value;
                OnPropertyChanged("CAR003_GirenSurum");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR003_DegistirenKaynak
        {
            get { return cAR003_DegistirenKaynak; }
            set
            {
                cAR003_DegistirenKaynak = value;
                OnPropertyChanged("CAR003_DegistirenKaynak");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_DegistirenTarih
        {
            get { return cAR003_DegistirenTarih; }
            set
            {
                cAR003_DegistirenTarih = value;
                OnPropertyChanged("CAR003_DegistirenTarih");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR003_DegistirenSaat
        {
            get { return cAR003_DegistirenSaat; }
            set
            {
                cAR003_DegistirenSaat = value;
                OnPropertyChanged("CAR003_DegistirenSaat");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_DegistirenKodu
        {
            get { return cAR003_DegistirenKodu; }
            set
            {
                cAR003_DegistirenKodu = value;
                OnPropertyChanged("CAR003_DegistirenKodu");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR003_DegistirenSurum
        {
            get { return cAR003_DegistirenSurum; }
            set
            {
                cAR003_DegistirenSurum = value;
                OnPropertyChanged("CAR003_DegistirenSurum");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_IptalDurumu
        {
            get { return cAR003_IptalDurumu; }
            set
            {
                cAR003_IptalDurumu = value;
                OnPropertyChanged("CAR003_IptalDurumu");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_AsilEvrakTarihi
        {
            get { return cAR003_AsilEvrakTarihi; }
            set
            {
                cAR003_AsilEvrakTarihi = value;
                OnPropertyChanged("CAR003_AsilEvrakTarihi");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_otvdahilharic
        {
            get { return cAR003_otvdahilharic; }
            set
            {
                cAR003_otvdahilharic = value;
                OnPropertyChanged("CAR003_otvdahilharic");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR003_otvtutari
        {
            get { return cAR003_otvtutari; }
            set
            {
                cAR003_otvtutari = value;
                OnPropertyChanged("CAR003_otvtutari");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR003_KarsiHesapKodu
        {
            get { return cAR003_KarsiHesapKodu; }
            set
            {
                cAR003_KarsiHesapKodu = value;
                OnPropertyChanged("CAR003_KarsiHesapKodu");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_Kod3
        {
            get { return cAR003_Kod3; }
            set
            {
                cAR003_Kod3 = value;
                OnPropertyChanged("CAR003_Kod3");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_Kod4
        {
            get { return cAR003_Kod4; }
            set
            {
                cAR003_Kod4 = value;
                OnPropertyChanged("CAR003_Kod4");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_Kod5
        {
            get { return cAR003_Kod5; }
            set
            {
                cAR003_Kod5 = value;
                OnPropertyChanged("CAR003_Kod5");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_Kod6
        {
            get { return cAR003_Kod6; }
            set
            {
                cAR003_Kod6 = value;
                OnPropertyChanged("CAR003_Kod6");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_Kod7
        {
            get { return cAR003_Kod7; }
            set
            {
                cAR003_Kod7 = value;
                OnPropertyChanged("CAR003_Kod7");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_Kod8
        {
            get { return cAR003_Kod8; }
            set
            {
                cAR003_Kod8 = value;
                OnPropertyChanged("CAR003_Kod8");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR003_Kod9
        {
            get { return cAR003_Kod9; }
            set
            {
                cAR003_Kod9 = value;
                OnPropertyChanged("CAR003_Kod9");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR003_Kod10
        {
            get { return cAR003_Kod10; }
            set
            {
                cAR003_Kod10 = value;
                OnPropertyChanged("CAR003_Kod10");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR003_Kod11
        {
            get { return cAR003_Kod11; }
            set
            {
                cAR003_Kod11 = value;
                OnPropertyChanged("CAR003_Kod11");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR003_Kod12
        {
            get { return cAR003_Kod12; }
            set
            {
                cAR003_Kod12 = value;
                OnPropertyChanged("CAR003_Kod12");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR003_Tutar2
        {
            get { return cAR003_Tutar2; }
            set
            {
                cAR003_Tutar2 = value;
                OnPropertyChanged("CAR003_Tutar2");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_Tarih2
        {
            get { return cAR003_Tarih2; }
            set
            {
                cAR003_Tarih2 = value;
                OnPropertyChanged("CAR003_Tarih2");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string CAR003_Aciklama2
        {
            get { return cAR003_Aciklama2; }
            set
            {
                cAR003_Aciklama2 = value;
                OnPropertyChanged("CAR003_Aciklama2");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR003_EvrakSeriNo2
        {
            get { return cAR003_EvrakSeriNo2; }
            set
            {
                cAR003_EvrakSeriNo2 = value;
                OnPropertyChanged("CAR003_EvrakSeriNo2");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string CAR003_DovizCinsi
        {
            get { return cAR003_DovizCinsi; }
            set
            {
                cAR003_DovizCinsi = value;
                OnPropertyChanged("CAR003_DovizCinsi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR003_DovizTutari
        {
            get { return cAR003_DovizTutari; }
            set
            {
                cAR003_DovizTutari = value;
                OnPropertyChanged("CAR003_DovizTutari");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR003_DovizKuru
        {
            get { return cAR003_DovizKuru; }
            set
            {
                cAR003_DovizKuru = value;
                OnPropertyChanged("CAR003_DovizKuru");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR003_SenetCekBordroNo
        {
            get { return cAR003_SenetCekBordroNo; }
            set
            {
                cAR003_SenetCekBordroNo = value;
                OnPropertyChanged("CAR003_SenetCekBordroNo");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_SenetCekPozisyonTipi
        {
            get { return cAR003_SenetCekPozisyonTipi; }
            set
            {
                cAR003_SenetCekPozisyonTipi = value;
                OnPropertyChanged("CAR003_SenetCekPozisyonTipi");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_MuhasebelesmeSekli
        {
            get { return cAR003_MuhasebelesmeSekli; }
            set
            {
                cAR003_MuhasebelesmeSekli = value;
                OnPropertyChanged("CAR003_MuhasebelesmeSekli");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_MuhasebeFisTarihi
        {
            get { return cAR003_MuhasebeFisTarihi; }
            set
            {
                cAR003_MuhasebeFisTarihi = value;
                OnPropertyChanged("CAR003_MuhasebeFisTarihi");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_MuhasebeTipi
        {
            get { return cAR003_MuhasebeTipi; }
            set
            {
                cAR003_MuhasebeTipi = value;
                OnPropertyChanged("CAR003_MuhasebeTipi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_MuhasebeFisNumarasi
        {
            get { return cAR003_MuhasebeFisNumarasi; }
            set
            {
                cAR003_MuhasebeFisNumarasi = value;
                OnPropertyChanged("CAR003_MuhasebeFisNumarasi");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string CAR003_MuhasebeFisKodu
        {
            get { return cAR003_MuhasebeFisKodu; }
            set
            {
                cAR003_MuhasebeFisKodu = value;
                OnPropertyChanged("CAR003_MuhasebeFisKodu");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_MuhasebeSiraNo
        {
            get { return cAR003_MuhasebeSiraNo; }
            set
            {
                cAR003_MuhasebeSiraNo = value;
                OnPropertyChanged("CAR003_MuhasebeSiraNo");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR003_MuhasebeHesapNo
        {
            get { return cAR003_MuhasebeHesapNo; }
            set
            {
                cAR003_MuhasebeHesapNo = value;
                OnPropertyChanged("CAR003_MuhasebeHesapNo");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR003_MuhasebeKarsiHeaspNo
        {
            get { return cAR003_MuhasebeKarsiHeaspNo; }
            set
            {
                cAR003_MuhasebeKarsiHeaspNo = value;
                OnPropertyChanged("CAR003_MuhasebeKarsiHeaspNo");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_MuhasebeYevmiyeSekli
        {
            get { return cAR003_MuhasebeYevmiyeSekli; }
            set
            {
                cAR003_MuhasebeYevmiyeSekli = value;
                OnPropertyChanged("CAR003_MuhasebeYevmiyeSekli");
            }
        }

        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string CAR003_IskontoTuru
        {
            get { return cAR003_IskontoTuru; }
            set
            {
                cAR003_IskontoTuru = value;
                OnPropertyChanged("CAR003_IskontoTuru");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR003_EvrakSeriNo3
        {
            get { return cAR003_EvrakSeriNo3; }
            set
            {
                cAR003_EvrakSeriNo3 = value;
                OnPropertyChanged("CAR003_EvrakSeriNo3");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR003_MasrafMerkezi
        {
            get { return cAR003_MasrafMerkezi; }
            set
            {
                cAR003_MasrafMerkezi = value;
                OnPropertyChanged("CAR003_MasrafMerkezi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_VadeFarkiTarihi
        {
            get { return cAR003_VadeFarkiTarihi; }
            set
            {
                cAR003_VadeFarkiTarihi = value;
                OnPropertyChanged("CAR003_VadeFarkiTarihi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR003_VadeFarkiTutari
        {
            get { return cAR003_VadeFarkiTutari; }
            set
            {
                cAR003_VadeFarkiTutari = value;
                OnPropertyChanged("CAR003_VadeFarkiTutari");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? CAR003_FaizOrani
        {
            get { return cAR003_FaizOrani; }
            set
            {
                cAR003_FaizOrani = value;
                OnPropertyChanged("CAR003_FaizOrani");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_Ulke
        {
            get { return cAR003_Ulke; }
            set
            {
                cAR003_Ulke = value;
                OnPropertyChanged("CAR003_Ulke");
            }
        }

        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string CAR003_VergiHesapNo
        {
            get { return cAR003_VergiHesapNo; }
            set
            {
                cAR003_VergiHesapNo = value;
                OnPropertyChanged("CAR003_VergiHesapNo");
            }
        }

        /// <summary> NVARCHAR (120) Allow Null </summary>
        public string CAR003_Unvani
        {
            get { return cAR003_Unvani; }
            set
            {
                cAR003_Unvani = value;
                OnPropertyChanged("CAR003_Unvani");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_EvrakSayisi
        {
            get { return cAR003_EvrakSayisi; }
            set
            {
                cAR003_EvrakSayisi = value;
                OnPropertyChanged("CAR003_EvrakSayisi");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_EvrakTipi
        {
            get { return cAR003_EvrakTipi; }
            set
            {
                cAR003_EvrakTipi = value;
                OnPropertyChanged("CAR003_EvrakTipi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_MHSMaddeNo
        {
            get { return cAR003_MHSMaddeNo; }
            set
            {
                cAR003_MHSMaddeNo = value;
                OnPropertyChanged("CAR003_MHSMaddeNo");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR003_IBAN
        {
            get { return cAR003_IBAN; }
            set
            {
                cAR003_IBAN = value;
                OnPropertyChanged("CAR003_IBAN");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_KKPOSTableRowID
        {
            get { return cAR003_KKPOSTableRowID; }
            set
            {
                cAR003_KKPOSTableRowID = value;
                OnPropertyChanged("CAR003_KKPOSTableRowID");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_KKTaksitSayisi
        {
            get { return cAR003_KKTaksitSayisi; }
            set
            {
                cAR003_KKTaksitSayisi = value;
                OnPropertyChanged("CAR003_KKTaksitSayisi");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_KKTaksitNo
        {
            get { return cAR003_KKTaksitNo; }
            set
            {
                cAR003_KKTaksitNo = value;
                OnPropertyChanged("CAR003_KKTaksitNo");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_KKKomisyonID
        {
            get { return cAR003_KKKomisyonID; }
            set
            {
                cAR003_KKKomisyonID = value;
                OnPropertyChanged("CAR003_KKKomisyonID");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_KDVTevkIslemTuru
        {
            get { return cAR003_KDVTevkIslemTuru; }
            set
            {
                cAR003_KDVTevkIslemTuru = value;
                OnPropertyChanged("CAR003_KDVTevkIslemTuru");
            }
        }

        /// <summary> NVARCHAR (14) Allow Null </summary>
        public string CAR003_KDVTevkOrani
        {
            get { return cAR003_KDVTevkOrani; }
            set
            {
                cAR003_KDVTevkOrani = value;
                OnPropertyChanged("CAR003_KDVTevkOrani");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR003_IthalatNo
        {
            get { return cAR003_IthalatNo; }
            set
            {
                cAR003_IthalatNo = value;
                OnPropertyChanged("CAR003_IthalatNo");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_IthalatAktarmaFlag
        {
            get { return cAR003_IthalatAktarmaFlag; }
            set
            {
                cAR003_IthalatAktarmaFlag = value;
                OnPropertyChanged("CAR003_IthalatAktarmaFlag");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR003_KDVTevkIslemBedeli
        {
            get { return cAR003_KDVTevkIslemBedeli; }
            set
            {
                cAR003_KDVTevkIslemBedeli = value;
                OnPropertyChanged("CAR003_KDVTevkIslemBedeli");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR003_OdemeTipi
        {
            get { return cAR003_OdemeTipi; }
            set
            {
                cAR003_OdemeTipi = value;
                OnPropertyChanged("CAR003_OdemeTipi");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_YevmiyeEvrakTip
        {
            get { return cAR003_YevmiyeEvrakTip; }
            set
            {
                cAR003_YevmiyeEvrakTip = value;
                OnPropertyChanged("CAR003_YevmiyeEvrakTip");
            }
        }

        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR003_EvrakTipAciklama
        {
            get { return cAR003_EvrakTipAciklama; }
            set
            {
                cAR003_EvrakTipAciklama = value;
                OnPropertyChanged("CAR003_EvrakTipAciklama");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_EFaturaTipi
        {
            get { return cAR003_EFaturaTipi; }
            set
            {
                cAR003_EFaturaTipi = value;
                OnPropertyChanged("CAR003_EFaturaTipi");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_EFaturaDurumu
        {
            get { return cAR003_EFaturaDurumu; }
            set
            {
                cAR003_EFaturaDurumu = value;
                OnPropertyChanged("CAR003_EFaturaDurumu");
            }
        }

        /// <summary> NVARCHAR (4) Allow Null </summary>
        public string CAR003_EFaturaOTVListeNo
        {
            get { return cAR003_EFaturaOTVListeNo; }
            set
            {
                cAR003_EFaturaOTVListeNo = value;
                OnPropertyChanged("CAR003_EFaturaOTVListeNo");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_EFaturaDonemBas
        {
            get { return cAR003_EFaturaDonemBas; }
            set
            {
                cAR003_EFaturaDonemBas = value;
                OnPropertyChanged("CAR003_EFaturaDonemBas");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_EFaturaDonemBit
        {
            get { return cAR003_EFaturaDonemBit; }
            set
            {
                cAR003_EFaturaDonemBit = value;
                OnPropertyChanged("CAR003_EFaturaDonemBit");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_EFaturaSure
        {
            get { return cAR003_EFaturaSure; }
            set
            {
                cAR003_EFaturaSure = value;
                OnPropertyChanged("CAR003_EFaturaSure");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_EFaturaSureBirimi
        {
            get { return cAR003_EFaturaSureBirimi; }
            set
            {
                cAR003_EFaturaSureBirimi = value;
                OnPropertyChanged("CAR003_EFaturaSureBirimi");
            }
        }

        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR003_EFaturaDonemAciklama
        {
            get { return cAR003_EFaturaDonemAciklama; }
            set
            {
                cAR003_EFaturaDonemAciklama = value;
                OnPropertyChanged("CAR003_EFaturaDonemAciklama");
            }
        }

        /// <summary> NVARCHAR (512) Allow Null </summary>
        public string CAR003_EFaturaNot
        {
            get { return cAR003_EFaturaNot; }
            set
            {
                cAR003_EFaturaNot = value;
                OnPropertyChanged("CAR003_EFaturaNot");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR003_EFaturaReferansNo
        {
            get { return cAR003_EFaturaReferansNo; }
            set
            {
                cAR003_EFaturaReferansNo = value;
                OnPropertyChanged("CAR003_EFaturaReferansNo");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR003_YevEvrakNo
        {
            get { return cAR003_YevEvrakNo; }
            set
            {
                cAR003_YevEvrakNo = value;
                OnPropertyChanged("CAR003_YevEvrakNo");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_YevEvrakTarihi
        {
            get { return cAR003_YevEvrakTarihi; }
            set
            {
                cAR003_YevEvrakTarihi = value;
                OnPropertyChanged("CAR003_YevEvrakTarihi");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? CAR003_StopajOrani
        {
            get { return cAR003_StopajOrani; }
            set
            {
                cAR003_StopajOrani = value;
                OnPropertyChanged("CAR003_StopajOrani");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_OdemeTuru
        {
            get { return cAR003_OdemeTuru; }
            set
            {
                cAR003_OdemeTuru = value;
                OnPropertyChanged("CAR003_OdemeTuru");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR003_TalimatNo
        {
            get { return cAR003_TalimatNo; }
            set
            {
                cAR003_TalimatNo = value;
                OnPropertyChanged("CAR003_TalimatNo");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR003_IhracatNo
        {
            get { return cAR003_IhracatNo; }
            set
            {
                cAR003_IhracatNo = value;
                OnPropertyChanged("CAR003_IhracatNo");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_GrpSiraNo
        {
            get { return cAR003_GrpSiraNo; }
            set
            {
                cAR003_GrpSiraNo = value;
                OnPropertyChanged("CAR003_GrpSiraNo");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_EArsivFaturaTipi
        {
            get { return cAR003_EArsivFaturaTipi; }
            set
            {
                cAR003_EArsivFaturaTipi = value;
                OnPropertyChanged("CAR003_EArsivFaturaTipi");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_EArsivFaturaTeslimSekli
        {
            get { return cAR003_EArsivFaturaTeslimSekli; }
            set
            {
                cAR003_EArsivFaturaTeslimSekli = value;
                OnPropertyChanged("CAR003_EArsivFaturaTeslimSekli");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_EArsivFaturaDurumu
        {
            get { return cAR003_EArsivFaturaDurumu; }
            set
            {
                cAR003_EArsivFaturaDurumu = value;
                OnPropertyChanged("CAR003_EArsivFaturaDurumu");
            }
        }

        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string CAR003_EArsivAdres
        {
            get { return cAR003_EArsivAdres; }
            set
            {
                cAR003_EArsivAdres = value;
                OnPropertyChanged("CAR003_EArsivAdres");
            }
        }

        /// <summary> NVARCHAR (54) Allow Null </summary>
        public string CAR003_EArsivSemt
        {
            get { return cAR003_EArsivSemt; }
            set
            {
                cAR003_EArsivSemt = value;
                OnPropertyChanged("CAR003_EArsivSemt");
            }
        }

        /// <summary> NVARCHAR (54) Allow Null </summary>
        public string CAR003_EArsivIL
        {
            get { return cAR003_EArsivIL; }
            set
            {
                cAR003_EArsivIL = value;
                OnPropertyChanged("CAR003_EArsivIL");
            }
        }

        /// <summary> NVARCHAR (120) Allow Null </summary>
        public string CAR003_Unvani2
        {
            get { return cAR003_Unvani2; }
            set
            {
                cAR003_Unvani2 = value;
                OnPropertyChanged("CAR003_Unvani2");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR003_YOKCSeriNo
        {
            get { return cAR003_YOKCSeriNo; }
            set
            {
                cAR003_YOKCSeriNo = value;
                OnPropertyChanged("CAR003_YOKCSeriNo");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_YOKCZRaporuNo
        {
            get { return cAR003_YOKCZRaporuNo; }
            set
            {
                cAR003_YOKCZRaporuNo = value;
                OnPropertyChanged("CAR003_YOKCZRaporuNo");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_YOKCBelgeTipi
        {
            get { return cAR003_YOKCBelgeTipi; }
            set
            {
                cAR003_YOKCBelgeTipi = value;
                OnPropertyChanged("CAR003_YOKCBelgeTipi");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_YOKCBilgiFisiTipi
        {
            get { return cAR003_YOKCBilgiFisiTipi; }
            set
            {
                cAR003_YOKCBilgiFisiTipi = value;
                OnPropertyChanged("CAR003_YOKCBilgiFisiTipi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_YOKCFisNo
        {
            get { return cAR003_YOKCFisNo; }
            set
            {
                cAR003_YOKCFisNo = value;
                OnPropertyChanged("CAR003_YOKCFisNo");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_YOKCFisTarihi
        {
            get { return cAR003_YOKCFisTarihi; }
            set
            {
                cAR003_YOKCFisTarihi = value;
                OnPropertyChanged("CAR003_YOKCFisTarihi");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string CAR003_OdemeTurKodu
        {
            get { return cAR003_OdemeTurKodu; }
            set
            {
                cAR003_OdemeTurKodu = value;
                OnPropertyChanged("CAR003_OdemeTurKodu");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_VergiDairesiKodu
        {
            get { return cAR003_VergiDairesiKodu; }
            set
            {
                cAR003_VergiDairesiKodu = value;
                OnPropertyChanged("CAR003_VergiDairesiKodu");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_FiiliIhracatTarihi
        {
            get { return cAR003_FiiliIhracatTarihi; }
            set
            {
                cAR003_FiiliIhracatTarihi = value;
                OnPropertyChanged("CAR003_FiiliIhracatTarihi");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR003_YOKCFisSaat
        {
            get { return cAR003_YOKCFisSaat; }
            set
            {
                cAR003_YOKCFisSaat = value;
                OnPropertyChanged("CAR003_YOKCFisSaat");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_YOKCDuzenlemeTip
        {
            get { return cAR003_YOKCDuzenlemeTip; }
            set
            {
                cAR003_YOKCDuzenlemeTip = value;
                OnPropertyChanged("CAR003_YOKCDuzenlemeTip");
            }
        }

        /// <summary> NVARCHAR (12) Allow Null </summary>
        public string CAR003_YOKCBankaOnayKod
        {
            get { return cAR003_YOKCBankaOnayKod; }
            set
            {
                cAR003_YOKCBankaOnayKod = value;
                OnPropertyChanged("CAR003_YOKCBankaOnayKod");
            }
        }

        /// <summary> NVARCHAR (48) Allow Null </summary>
        public string CAR003_YOKCUniqueID
        {
            get { return cAR003_YOKCUniqueID; }
            set
            {
                cAR003_YOKCUniqueID = value;
                OnPropertyChanged("CAR003_YOKCUniqueID");
            }
        }

        /// <summary> INT (4) PRIMARY KEY * </summary>
        public int pk_CAR003_Row_ID
        {
            private get { return _pk_CAR003_Row_ID; }
            set
            {
                _pk_CAR003_Row_ID = value;
                OnPropertyChanged("pk_CAR003_Row_ID");
            }
        }
        #endregion /// Properties             
        #region Tablo Bilgileri & Metodlar

        public void WhereAdd(CAR003E Property, object deger, Operand and_Or = Operand.AND)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(CAR003E), Property), deger, and_Or));
        }

        public void WhereAdd(CAR003E Property, Islem islem, object Deger, Operand And_Or = Operand.AND)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(CAR003E), Property), islem, Deger, And_Or));
        }

        public void WhereAdd(CAR003E Property, Operand In_NotIn, params object[] Degerler_Parantez)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(CAR003E), Property), In_NotIn, Degerler_Parantez));
        }

        public void WhereAdd(params object[] Degerler)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Degerler));
        }

        /// <summary> Set işleminde Property " = " eşit ile otomatik başlar. </summary>
        public void SetAdd(CAR003E Property, params object[] Degerler)
        {
            SetList.Add(SqlExperOperatorIslem.SetAdd(Enum.GetName(typeof(CAR003E), Property), Degerler));
        }

        List<string> WhereList = new List<string>();
        List<string> SetList = new List<string>();
        string info_FullTableName = "YNS{0}.YNS{0}.CAR003";
        string[] info_PrimaryKeys = { "pk_CAR003_Row_ID" };
        string[] info_IdentityKeys = { "CAR003_Row_ID" };

        List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public CAR003()
        {
            ChangedProperties = new List<string>();
            PropertyChanged += CAR003_PropertyChanged;
        }

        public void AcceptChanges() => ChangedProperties.Clear();

        void CAR003_PropertyChanged(object sender, PropertyChangedEventArgs e)
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