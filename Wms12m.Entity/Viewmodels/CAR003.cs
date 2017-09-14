using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using DevHelper;

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
    public class CAR003 :  INotifyPropertyChanged
    {
        #region Properties
        #region Fields  
        private int _CAR003_Row_ID; 
        private string _CAR003_HesapKodu; 
        private int? _CAR003_Tarih; 
        private short? _CAR003_IslemTipi; 
        private string _CAR003_EvrakSeriNo; 
        private string _CAR003_Aciklama; 
        private byte? _CAR003_BA; 
        private decimal? _CAR003_Tutar; 
        private int? _CAR003_VadeTarihi; 
        private string _CAR003_KarsiEvrakSeriNo; 
        private string _CAR003_Kod1; 
        private string _CAR003_Kod2; 
        private byte? _CAR003_KDVOrani; 
        private byte? _CAR003_KDVDahilHaric; 
        private byte? _CAR003_MuhasebelesmeDurumu; 
        private byte? _CAR003_ParaBirimi; 
        private int? _CAR003_SEQNo; 
        private string _CAR003_GirenKaynak; 
        private int? _CAR003_GirenTarih; 
        private string _CAR003_GirenSaat; 
        private string _CAR003_GirenKodu; 
        private string _CAR003_GirenSurum; 
        private string _CAR003_DegistirenKaynak; 
        private int? _CAR003_DegistirenTarih; 
        private string _CAR003_DegistirenSaat; 
        private string _CAR003_DegistirenKodu; 
        private string _CAR003_DegistirenSurum; 
        private byte? _CAR003_IptalDurumu; 
        private int? _CAR003_AsilEvrakTarihi; 
        private byte? _CAR003_otvdahilharic; 
        private decimal? _CAR003_otvtutari; 
        private string _CAR003_KarsiHesapKodu; 
        private string _CAR003_Kod3; 
        private string _CAR003_Kod4; 
        private string _CAR003_Kod5; 
        private string _CAR003_Kod6; 
        private string _CAR003_Kod7; 
        private string _CAR003_Kod8; 
        private string _CAR003_Kod9; 
        private string _CAR003_Kod10; 
        private decimal? _CAR003_Kod11; 
        private decimal? _CAR003_Kod12; 
        private decimal? _CAR003_Tutar2; 
        private int? _CAR003_Tarih2; 
        private string _CAR003_Aciklama2; 
        private string _CAR003_EvrakSeriNo2; 
        private string _CAR003_DovizCinsi; 
        private decimal? _CAR003_DovizTutari; 
        private decimal? _CAR003_DovizKuru; 
        private string _CAR003_SenetCekBordroNo; 
        private short? _CAR003_SenetCekPozisyonTipi; 
        private short? _CAR003_MuhasebelesmeSekli; 
        private int? _CAR003_MuhasebeFisTarihi; 
        private byte? _CAR003_MuhasebeTipi; 
        private int? _CAR003_MuhasebeFisNumarasi; 
        private string _CAR003_MuhasebeFisKodu; 
        private short? _CAR003_MuhasebeSiraNo; 
        private string _CAR003_MuhasebeHesapNo; 
        private string _CAR003_MuhasebeKarsiHeaspNo; 
        private byte? _CAR003_MuhasebeYevmiyeSekli; 
        private string _CAR003_IskontoTuru; 
        private string _CAR003_EvrakSeriNo3; 
        private string _CAR003_MasrafMerkezi; 
        private int? _CAR003_VadeFarkiTarihi; 
        private decimal? _CAR003_VadeFarkiTutari; 
        private decimal? _CAR003_FaizOrani; 
        private int? _CAR003_Ulke; 
        private string _CAR003_VergiHesapNo; 
        private string _CAR003_Unvani; 
        private short? _CAR003_EvrakSayisi; 
        private short? _CAR003_EvrakTipi; 
        private int? _CAR003_MHSMaddeNo; 
        private string _CAR003_IBAN; 
        private int? _CAR003_KKPOSTableRowID; 
        private short? _CAR003_KKTaksitSayisi; 
        private short? _CAR003_KKTaksitNo; 
        private int? _CAR003_KKKomisyonID; 
        private int? _CAR003_KDVTevkIslemTuru; 
        private string _CAR003_KDVTevkOrani; 
        private string _CAR003_IthalatNo; 
        private byte? _CAR003_IthalatAktarmaFlag; 
        private decimal? _CAR003_KDVTevkIslemBedeli; 
        private string _CAR003_OdemeTipi; 
        private byte? _CAR003_YevmiyeEvrakTip; 
        private string _CAR003_EvrakTipAciklama; 
        private byte? _CAR003_EFaturaTipi; 
        private short? _CAR003_EFaturaDurumu; 
        private string _CAR003_EFaturaOTVListeNo; 
        private int? _CAR003_EFaturaDonemBas; 
        private int? _CAR003_EFaturaDonemBit; 
        private int? _CAR003_EFaturaSure; 
        private byte? _CAR003_EFaturaSureBirimi; 
        private string _CAR003_EFaturaDonemAciklama; 
        private string _CAR003_EFaturaNot; 
        private string _CAR003_EFaturaReferansNo; 
        private string _CAR003_YevEvrakNo; 
        private int? _CAR003_YevEvrakTarihi; 
        private decimal? _CAR003_StopajOrani; 
        private short? _CAR003_OdemeTuru; 
        private string _CAR003_TalimatNo; 
        private string _CAR003_IhracatNo; 
        private int? _CAR003_GrpSiraNo; 
        private byte? _CAR003_EArsivFaturaTipi; 
        private byte? _CAR003_EArsivFaturaTeslimSekli; 
        private short? _CAR003_EArsivFaturaDurumu; 
        private string _CAR003_EArsivAdres; 
        private string _CAR003_EArsivSemt; 
        private string _CAR003_EArsivIL; 
        private string _CAR003_Unvani2; 
        private string _CAR003_YOKCSeriNo; 
        private int? _CAR003_YOKCZRaporuNo; 
        private byte? _CAR003_YOKCBelgeTipi; 
        private byte? _CAR003_YOKCBilgiFisiTipi; 
        private int? _CAR003_YOKCFisNo; 
        private int? _CAR003_YOKCFisTarihi; 
        private string _CAR003_OdemeTurKodu; 
        private int? _CAR003_VergiDairesiKodu; 
        private int? _CAR003_FiiliIhracatTarihi; 
        private string _CAR003_YOKCFisSaat; 
        private byte? _CAR003_YOKCDuzenlemeTip; 
        private string _CAR003_YOKCBankaOnayKod; 
        private string _CAR003_YOKCUniqueID; 
        private int _pk_CAR003_Row_ID;
        #endregion /// Fields
     
       
        /// <summary> INT (4) PrimaryKey IdentityKey * </summary>
        public int CAR003_Row_ID 
        {
            get {   return this._CAR003_Row_ID;   }                          
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR003_HesapKodu 
        {
            get {   return this._CAR003_HesapKodu;   } 
            set 
            {
                this._CAR003_HesapKodu = value;
                OnPropertyChanged("CAR003_HesapKodu"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_Tarih 
        {
            get {   return this._CAR003_Tarih;   } 
            set 
            {
                this._CAR003_Tarih = value;
                OnPropertyChanged("CAR003_Tarih"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_IslemTipi 
        {
            get {   return this._CAR003_IslemTipi;   } 
            set 
            {
                this._CAR003_IslemTipi = value;
                OnPropertyChanged("CAR003_IslemTipi"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR003_EvrakSeriNo 
        {
            get {   return this._CAR003_EvrakSeriNo;   } 
            set 
            {
                this._CAR003_EvrakSeriNo = value;
                OnPropertyChanged("CAR003_EvrakSeriNo"); 
            }                         
        }
       
        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR003_Aciklama 
        {
            get {   return this._CAR003_Aciklama;   } 
            set 
            {
                this._CAR003_Aciklama = value;
                OnPropertyChanged("CAR003_Aciklama"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_BA 
        {
            get {   return this._CAR003_BA;   } 
            set 
            {
                this._CAR003_BA = value;
                OnPropertyChanged("CAR003_BA"); 
            }                         
        }
       
        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR003_Tutar 
        {
            get {   return this._CAR003_Tutar;   } 
            set 
            {
                this._CAR003_Tutar = value;
                OnPropertyChanged("CAR003_Tutar"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_VadeTarihi 
        {
            get {   return this._CAR003_VadeTarihi;   } 
            set 
            {
                this._CAR003_VadeTarihi = value;
                OnPropertyChanged("CAR003_VadeTarihi"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR003_KarsiEvrakSeriNo 
        {
            get {   return this._CAR003_KarsiEvrakSeriNo;   } 
            set 
            {
                this._CAR003_KarsiEvrakSeriNo = value;
                OnPropertyChanged("CAR003_KarsiEvrakSeriNo"); 
            }                         
        }
       
        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_Kod1 
        {
            get {   return this._CAR003_Kod1;   } 
            set 
            {
                this._CAR003_Kod1 = value;
                OnPropertyChanged("CAR003_Kod1"); 
            }                         
        }
       
        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_Kod2 
        {
            get {   return this._CAR003_Kod2;   } 
            set 
            {
                this._CAR003_Kod2 = value;
                OnPropertyChanged("CAR003_Kod2"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_KDVOrani 
        {
            get {   return this._CAR003_KDVOrani;   } 
            set 
            {
                this._CAR003_KDVOrani = value;
                OnPropertyChanged("CAR003_KDVOrani"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_KDVDahilHaric 
        {
            get {   return this._CAR003_KDVDahilHaric;   } 
            set 
            {
                this._CAR003_KDVDahilHaric = value;
                OnPropertyChanged("CAR003_KDVDahilHaric"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_MuhasebelesmeDurumu 
        {
            get {   return this._CAR003_MuhasebelesmeDurumu;   } 
            set 
            {
                this._CAR003_MuhasebelesmeDurumu = value;
                OnPropertyChanged("CAR003_MuhasebelesmeDurumu"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_ParaBirimi 
        {
            get {   return this._CAR003_ParaBirimi;   } 
            set 
            {
                this._CAR003_ParaBirimi = value;
                OnPropertyChanged("CAR003_ParaBirimi"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_SEQNo 
        {
            get {   return this._CAR003_SEQNo;   } 
            set 
            {
                this._CAR003_SEQNo = value;
                OnPropertyChanged("CAR003_SEQNo"); 
            }                         
        }
       
        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR003_GirenKaynak 
        {
            get {   return this._CAR003_GirenKaynak;   } 
            set 
            {
                this._CAR003_GirenKaynak = value;
                OnPropertyChanged("CAR003_GirenKaynak"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_GirenTarih 
        {
            get {   return this._CAR003_GirenTarih;   } 
            set 
            {
                this._CAR003_GirenTarih = value;
                OnPropertyChanged("CAR003_GirenTarih"); 
            }                         
        }
       
        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR003_GirenSaat 
        {
            get {   return this._CAR003_GirenSaat;   } 
            set 
            {
                this._CAR003_GirenSaat = value;
                OnPropertyChanged("CAR003_GirenSaat"); 
            }                         
        }
       
        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_GirenKodu 
        {
            get {   return this._CAR003_GirenKodu;   } 
            set 
            {
                this._CAR003_GirenKodu = value;
                OnPropertyChanged("CAR003_GirenKodu"); 
            }                         
        }
       
        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR003_GirenSurum 
        {
            get {   return this._CAR003_GirenSurum;   } 
            set 
            {
                this._CAR003_GirenSurum = value;
                OnPropertyChanged("CAR003_GirenSurum"); 
            }                         
        }
       
        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR003_DegistirenKaynak 
        {
            get {   return this._CAR003_DegistirenKaynak;   } 
            set 
            {
                this._CAR003_DegistirenKaynak = value;
                OnPropertyChanged("CAR003_DegistirenKaynak"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_DegistirenTarih 
        {
            get {   return this._CAR003_DegistirenTarih;   } 
            set 
            {
                this._CAR003_DegistirenTarih = value;
                OnPropertyChanged("CAR003_DegistirenTarih"); 
            }                         
        }
       
        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR003_DegistirenSaat 
        {
            get {   return this._CAR003_DegistirenSaat;   } 
            set 
            {
                this._CAR003_DegistirenSaat = value;
                OnPropertyChanged("CAR003_DegistirenSaat"); 
            }                         
        }
       
        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_DegistirenKodu 
        {
            get {   return this._CAR003_DegistirenKodu;   } 
            set 
            {
                this._CAR003_DegistirenKodu = value;
                OnPropertyChanged("CAR003_DegistirenKodu"); 
            }                         
        }
       
        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR003_DegistirenSurum 
        {
            get {   return this._CAR003_DegistirenSurum;   } 
            set 
            {
                this._CAR003_DegistirenSurum = value;
                OnPropertyChanged("CAR003_DegistirenSurum"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_IptalDurumu 
        {
            get {   return this._CAR003_IptalDurumu;   } 
            set 
            {
                this._CAR003_IptalDurumu = value;
                OnPropertyChanged("CAR003_IptalDurumu"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_AsilEvrakTarihi 
        {
            get {   return this._CAR003_AsilEvrakTarihi;   } 
            set 
            {
                this._CAR003_AsilEvrakTarihi = value;
                OnPropertyChanged("CAR003_AsilEvrakTarihi"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_otvdahilharic 
        {
            get {   return this._CAR003_otvdahilharic;   } 
            set 
            {
                this._CAR003_otvdahilharic = value;
                OnPropertyChanged("CAR003_otvdahilharic"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR003_otvtutari 
        {
            get {   return this._CAR003_otvtutari;   } 
            set 
            {
                this._CAR003_otvtutari = value;
                OnPropertyChanged("CAR003_otvtutari"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR003_KarsiHesapKodu 
        {
            get {   return this._CAR003_KarsiHesapKodu;   } 
            set 
            {
                this._CAR003_KarsiHesapKodu = value;
                OnPropertyChanged("CAR003_KarsiHesapKodu"); 
            }                         
        }
       
        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_Kod3 
        {
            get {   return this._CAR003_Kod3;   } 
            set 
            {
                this._CAR003_Kod3 = value;
                OnPropertyChanged("CAR003_Kod3"); 
            }                         
        }
       
        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_Kod4 
        {
            get {   return this._CAR003_Kod4;   } 
            set 
            {
                this._CAR003_Kod4 = value;
                OnPropertyChanged("CAR003_Kod4"); 
            }                         
        }
       
        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_Kod5 
        {
            get {   return this._CAR003_Kod5;   } 
            set 
            {
                this._CAR003_Kod5 = value;
                OnPropertyChanged("CAR003_Kod5"); 
            }                         
        }
       
        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_Kod6 
        {
            get {   return this._CAR003_Kod6;   } 
            set 
            {
                this._CAR003_Kod6 = value;
                OnPropertyChanged("CAR003_Kod6"); 
            }                         
        }
       
        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_Kod7 
        {
            get {   return this._CAR003_Kod7;   } 
            set 
            {
                this._CAR003_Kod7 = value;
                OnPropertyChanged("CAR003_Kod7"); 
            }                         
        }
       
        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR003_Kod8 
        {
            get {   return this._CAR003_Kod8;   } 
            set 
            {
                this._CAR003_Kod8 = value;
                OnPropertyChanged("CAR003_Kod8"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR003_Kod9 
        {
            get {   return this._CAR003_Kod9;   } 
            set 
            {
                this._CAR003_Kod9 = value;
                OnPropertyChanged("CAR003_Kod9"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR003_Kod10 
        {
            get {   return this._CAR003_Kod10;   } 
            set 
            {
                this._CAR003_Kod10 = value;
                OnPropertyChanged("CAR003_Kod10"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR003_Kod11 
        {
            get {   return this._CAR003_Kod11;   } 
            set 
            {
                this._CAR003_Kod11 = value;
                OnPropertyChanged("CAR003_Kod11"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR003_Kod12 
        {
            get {   return this._CAR003_Kod12;   } 
            set 
            {
                this._CAR003_Kod12 = value;
                OnPropertyChanged("CAR003_Kod12"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR003_Tutar2 
        {
            get {   return this._CAR003_Tutar2;   } 
            set 
            {
                this._CAR003_Tutar2 = value;
                OnPropertyChanged("CAR003_Tutar2"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_Tarih2 
        {
            get {   return this._CAR003_Tarih2;   } 
            set 
            {
                this._CAR003_Tarih2 = value;
                OnPropertyChanged("CAR003_Tarih2"); 
            }                         
        }
       
        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string CAR003_Aciklama2 
        {
            get {   return this._CAR003_Aciklama2;   } 
            set 
            {
                this._CAR003_Aciklama2 = value;
                OnPropertyChanged("CAR003_Aciklama2"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR003_EvrakSeriNo2 
        {
            get {   return this._CAR003_EvrakSeriNo2;   } 
            set 
            {
                this._CAR003_EvrakSeriNo2 = value;
                OnPropertyChanged("CAR003_EvrakSeriNo2"); 
            }                         
        }
       
        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string CAR003_DovizCinsi 
        {
            get {   return this._CAR003_DovizCinsi;   } 
            set 
            {
                this._CAR003_DovizCinsi = value;
                OnPropertyChanged("CAR003_DovizCinsi"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR003_DovizTutari 
        {
            get {   return this._CAR003_DovizTutari;   } 
            set 
            {
                this._CAR003_DovizTutari = value;
                OnPropertyChanged("CAR003_DovizTutari"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR003_DovizKuru 
        {
            get {   return this._CAR003_DovizKuru;   } 
            set 
            {
                this._CAR003_DovizKuru = value;
                OnPropertyChanged("CAR003_DovizKuru"); 
            }                         
        }
       
        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR003_SenetCekBordroNo 
        {
            get {   return this._CAR003_SenetCekBordroNo;   } 
            set 
            {
                this._CAR003_SenetCekBordroNo = value;
                OnPropertyChanged("CAR003_SenetCekBordroNo"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_SenetCekPozisyonTipi 
        {
            get {   return this._CAR003_SenetCekPozisyonTipi;   } 
            set 
            {
                this._CAR003_SenetCekPozisyonTipi = value;
                OnPropertyChanged("CAR003_SenetCekPozisyonTipi"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_MuhasebelesmeSekli 
        {
            get {   return this._CAR003_MuhasebelesmeSekli;   } 
            set 
            {
                this._CAR003_MuhasebelesmeSekli = value;
                OnPropertyChanged("CAR003_MuhasebelesmeSekli"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_MuhasebeFisTarihi 
        {
            get {   return this._CAR003_MuhasebeFisTarihi;   } 
            set 
            {
                this._CAR003_MuhasebeFisTarihi = value;
                OnPropertyChanged("CAR003_MuhasebeFisTarihi"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_MuhasebeTipi 
        {
            get {   return this._CAR003_MuhasebeTipi;   } 
            set 
            {
                this._CAR003_MuhasebeTipi = value;
                OnPropertyChanged("CAR003_MuhasebeTipi"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_MuhasebeFisNumarasi 
        {
            get {   return this._CAR003_MuhasebeFisNumarasi;   } 
            set 
            {
                this._CAR003_MuhasebeFisNumarasi = value;
                OnPropertyChanged("CAR003_MuhasebeFisNumarasi"); 
            }                         
        }
       
        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string CAR003_MuhasebeFisKodu 
        {
            get {   return this._CAR003_MuhasebeFisKodu;   } 
            set 
            {
                this._CAR003_MuhasebeFisKodu = value;
                OnPropertyChanged("CAR003_MuhasebeFisKodu"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_MuhasebeSiraNo 
        {
            get {   return this._CAR003_MuhasebeSiraNo;   } 
            set 
            {
                this._CAR003_MuhasebeSiraNo = value;
                OnPropertyChanged("CAR003_MuhasebeSiraNo"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR003_MuhasebeHesapNo 
        {
            get {   return this._CAR003_MuhasebeHesapNo;   } 
            set 
            {
                this._CAR003_MuhasebeHesapNo = value;
                OnPropertyChanged("CAR003_MuhasebeHesapNo"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR003_MuhasebeKarsiHeaspNo 
        {
            get {   return this._CAR003_MuhasebeKarsiHeaspNo;   } 
            set 
            {
                this._CAR003_MuhasebeKarsiHeaspNo = value;
                OnPropertyChanged("CAR003_MuhasebeKarsiHeaspNo"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_MuhasebeYevmiyeSekli 
        {
            get {   return this._CAR003_MuhasebeYevmiyeSekli;   } 
            set 
            {
                this._CAR003_MuhasebeYevmiyeSekli = value;
                OnPropertyChanged("CAR003_MuhasebeYevmiyeSekli"); 
            }                         
        }
       
        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string CAR003_IskontoTuru 
        {
            get {   return this._CAR003_IskontoTuru;   } 
            set 
            {
                this._CAR003_IskontoTuru = value;
                OnPropertyChanged("CAR003_IskontoTuru"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR003_EvrakSeriNo3 
        {
            get {   return this._CAR003_EvrakSeriNo3;   } 
            set 
            {
                this._CAR003_EvrakSeriNo3 = value;
                OnPropertyChanged("CAR003_EvrakSeriNo3"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR003_MasrafMerkezi 
        {
            get {   return this._CAR003_MasrafMerkezi;   } 
            set 
            {
                this._CAR003_MasrafMerkezi = value;
                OnPropertyChanged("CAR003_MasrafMerkezi"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_VadeFarkiTarihi 
        {
            get {   return this._CAR003_VadeFarkiTarihi;   } 
            set 
            {
                this._CAR003_VadeFarkiTarihi = value;
                OnPropertyChanged("CAR003_VadeFarkiTarihi"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR003_VadeFarkiTutari 
        {
            get {   return this._CAR003_VadeFarkiTutari;   } 
            set 
            {
                this._CAR003_VadeFarkiTutari = value;
                OnPropertyChanged("CAR003_VadeFarkiTutari"); 
            }                         
        }
       
        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? CAR003_FaizOrani 
        {
            get {   return this._CAR003_FaizOrani;   } 
            set 
            {
                this._CAR003_FaizOrani = value;
                OnPropertyChanged("CAR003_FaizOrani"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_Ulke 
        {
            get {   return this._CAR003_Ulke;   } 
            set 
            {
                this._CAR003_Ulke = value;
                OnPropertyChanged("CAR003_Ulke"); 
            }                         
        }
       
        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string CAR003_VergiHesapNo 
        {
            get {   return this._CAR003_VergiHesapNo;   } 
            set 
            {
                this._CAR003_VergiHesapNo = value;
                OnPropertyChanged("CAR003_VergiHesapNo"); 
            }                         
        }
       
        /// <summary> NVARCHAR (120) Allow Null </summary>
        public string CAR003_Unvani 
        {
            get {   return this._CAR003_Unvani;   } 
            set 
            {
                this._CAR003_Unvani = value;
                OnPropertyChanged("CAR003_Unvani"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_EvrakSayisi 
        {
            get {   return this._CAR003_EvrakSayisi;   } 
            set 
            {
                this._CAR003_EvrakSayisi = value;
                OnPropertyChanged("CAR003_EvrakSayisi"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_EvrakTipi 
        {
            get {   return this._CAR003_EvrakTipi;   } 
            set 
            {
                this._CAR003_EvrakTipi = value;
                OnPropertyChanged("CAR003_EvrakTipi"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_MHSMaddeNo 
        {
            get {   return this._CAR003_MHSMaddeNo;   } 
            set 
            {
                this._CAR003_MHSMaddeNo = value;
                OnPropertyChanged("CAR003_MHSMaddeNo"); 
            }                         
        }
       
        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR003_IBAN 
        {
            get {   return this._CAR003_IBAN;   } 
            set 
            {
                this._CAR003_IBAN = value;
                OnPropertyChanged("CAR003_IBAN"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_KKPOSTableRowID 
        {
            get {   return this._CAR003_KKPOSTableRowID;   } 
            set 
            {
                this._CAR003_KKPOSTableRowID = value;
                OnPropertyChanged("CAR003_KKPOSTableRowID"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_KKTaksitSayisi 
        {
            get {   return this._CAR003_KKTaksitSayisi;   } 
            set 
            {
                this._CAR003_KKTaksitSayisi = value;
                OnPropertyChanged("CAR003_KKTaksitSayisi"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_KKTaksitNo 
        {
            get {   return this._CAR003_KKTaksitNo;   } 
            set 
            {
                this._CAR003_KKTaksitNo = value;
                OnPropertyChanged("CAR003_KKTaksitNo"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_KKKomisyonID 
        {
            get {   return this._CAR003_KKKomisyonID;   } 
            set 
            {
                this._CAR003_KKKomisyonID = value;
                OnPropertyChanged("CAR003_KKKomisyonID"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_KDVTevkIslemTuru 
        {
            get {   return this._CAR003_KDVTevkIslemTuru;   } 
            set 
            {
                this._CAR003_KDVTevkIslemTuru = value;
                OnPropertyChanged("CAR003_KDVTevkIslemTuru"); 
            }                         
        }
       
        /// <summary> NVARCHAR (14) Allow Null </summary>
        public string CAR003_KDVTevkOrani 
        {
            get {   return this._CAR003_KDVTevkOrani;   } 
            set 
            {
                this._CAR003_KDVTevkOrani = value;
                OnPropertyChanged("CAR003_KDVTevkOrani"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR003_IthalatNo 
        {
            get {   return this._CAR003_IthalatNo;   } 
            set 
            {
                this._CAR003_IthalatNo = value;
                OnPropertyChanged("CAR003_IthalatNo"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_IthalatAktarmaFlag 
        {
            get {   return this._CAR003_IthalatAktarmaFlag;   } 
            set 
            {
                this._CAR003_IthalatAktarmaFlag = value;
                OnPropertyChanged("CAR003_IthalatAktarmaFlag"); 
            }                         
        }
       
        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR003_KDVTevkIslemBedeli 
        {
            get {   return this._CAR003_KDVTevkIslemBedeli;   } 
            set 
            {
                this._CAR003_KDVTevkIslemBedeli = value;
                OnPropertyChanged("CAR003_KDVTevkIslemBedeli"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR003_OdemeTipi 
        {
            get {   return this._CAR003_OdemeTipi;   } 
            set 
            {
                this._CAR003_OdemeTipi = value;
                OnPropertyChanged("CAR003_OdemeTipi"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_YevmiyeEvrakTip 
        {
            get {   return this._CAR003_YevmiyeEvrakTip;   } 
            set 
            {
                this._CAR003_YevmiyeEvrakTip = value;
                OnPropertyChanged("CAR003_YevmiyeEvrakTip"); 
            }                         
        }
       
        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR003_EvrakTipAciklama 
        {
            get {   return this._CAR003_EvrakTipAciklama;   } 
            set 
            {
                this._CAR003_EvrakTipAciklama = value;
                OnPropertyChanged("CAR003_EvrakTipAciklama"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_EFaturaTipi 
        {
            get {   return this._CAR003_EFaturaTipi;   } 
            set 
            {
                this._CAR003_EFaturaTipi = value;
                OnPropertyChanged("CAR003_EFaturaTipi"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_EFaturaDurumu 
        {
            get {   return this._CAR003_EFaturaDurumu;   } 
            set 
            {
                this._CAR003_EFaturaDurumu = value;
                OnPropertyChanged("CAR003_EFaturaDurumu"); 
            }                         
        }
       
        /// <summary> NVARCHAR (4) Allow Null </summary>
        public string CAR003_EFaturaOTVListeNo 
        {
            get {   return this._CAR003_EFaturaOTVListeNo;   } 
            set 
            {
                this._CAR003_EFaturaOTVListeNo = value;
                OnPropertyChanged("CAR003_EFaturaOTVListeNo"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_EFaturaDonemBas 
        {
            get {   return this._CAR003_EFaturaDonemBas;   } 
            set 
            {
                this._CAR003_EFaturaDonemBas = value;
                OnPropertyChanged("CAR003_EFaturaDonemBas"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_EFaturaDonemBit 
        {
            get {   return this._CAR003_EFaturaDonemBit;   } 
            set 
            {
                this._CAR003_EFaturaDonemBit = value;
                OnPropertyChanged("CAR003_EFaturaDonemBit"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_EFaturaSure 
        {
            get {   return this._CAR003_EFaturaSure;   } 
            set 
            {
                this._CAR003_EFaturaSure = value;
                OnPropertyChanged("CAR003_EFaturaSure"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_EFaturaSureBirimi 
        {
            get {   return this._CAR003_EFaturaSureBirimi;   } 
            set 
            {
                this._CAR003_EFaturaSureBirimi = value;
                OnPropertyChanged("CAR003_EFaturaSureBirimi"); 
            }                         
        }
       
        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR003_EFaturaDonemAciklama 
        {
            get {   return this._CAR003_EFaturaDonemAciklama;   } 
            set 
            {
                this._CAR003_EFaturaDonemAciklama = value;
                OnPropertyChanged("CAR003_EFaturaDonemAciklama"); 
            }                         
        }
       
        /// <summary> NVARCHAR (512) Allow Null </summary>
        public string CAR003_EFaturaNot 
        {
            get {   return this._CAR003_EFaturaNot;   } 
            set 
            {
                this._CAR003_EFaturaNot = value;
                OnPropertyChanged("CAR003_EFaturaNot"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR003_EFaturaReferansNo 
        {
            get {   return this._CAR003_EFaturaReferansNo;   } 
            set 
            {
                this._CAR003_EFaturaReferansNo = value;
                OnPropertyChanged("CAR003_EFaturaReferansNo"); 
            }                         
        }
       
        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR003_YevEvrakNo 
        {
            get {   return this._CAR003_YevEvrakNo;   } 
            set 
            {
                this._CAR003_YevEvrakNo = value;
                OnPropertyChanged("CAR003_YevEvrakNo"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_YevEvrakTarihi 
        {
            get {   return this._CAR003_YevEvrakTarihi;   } 
            set 
            {
                this._CAR003_YevEvrakTarihi = value;
                OnPropertyChanged("CAR003_YevEvrakTarihi"); 
            }                         
        }
       
        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? CAR003_StopajOrani 
        {
            get {   return this._CAR003_StopajOrani;   } 
            set 
            {
                this._CAR003_StopajOrani = value;
                OnPropertyChanged("CAR003_StopajOrani"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_OdemeTuru 
        {
            get {   return this._CAR003_OdemeTuru;   } 
            set 
            {
                this._CAR003_OdemeTuru = value;
                OnPropertyChanged("CAR003_OdemeTuru"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR003_TalimatNo 
        {
            get {   return this._CAR003_TalimatNo;   } 
            set 
            {
                this._CAR003_TalimatNo = value;
                OnPropertyChanged("CAR003_TalimatNo"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR003_IhracatNo 
        {
            get {   return this._CAR003_IhracatNo;   } 
            set 
            {
                this._CAR003_IhracatNo = value;
                OnPropertyChanged("CAR003_IhracatNo"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_GrpSiraNo 
        {
            get {   return this._CAR003_GrpSiraNo;   } 
            set 
            {
                this._CAR003_GrpSiraNo = value;
                OnPropertyChanged("CAR003_GrpSiraNo"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_EArsivFaturaTipi 
        {
            get {   return this._CAR003_EArsivFaturaTipi;   } 
            set 
            {
                this._CAR003_EArsivFaturaTipi = value;
                OnPropertyChanged("CAR003_EArsivFaturaTipi"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_EArsivFaturaTeslimSekli 
        {
            get {   return this._CAR003_EArsivFaturaTeslimSekli;   } 
            set 
            {
                this._CAR003_EArsivFaturaTeslimSekli = value;
                OnPropertyChanged("CAR003_EArsivFaturaTeslimSekli"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR003_EArsivFaturaDurumu 
        {
            get {   return this._CAR003_EArsivFaturaDurumu;   } 
            set 
            {
                this._CAR003_EArsivFaturaDurumu = value;
                OnPropertyChanged("CAR003_EArsivFaturaDurumu"); 
            }                         
        }
       
        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string CAR003_EArsivAdres 
        {
            get {   return this._CAR003_EArsivAdres;   } 
            set 
            {
                this._CAR003_EArsivAdres = value;
                OnPropertyChanged("CAR003_EArsivAdres"); 
            }                         
        }
       
        /// <summary> NVARCHAR (54) Allow Null </summary>
        public string CAR003_EArsivSemt 
        {
            get {   return this._CAR003_EArsivSemt;   } 
            set 
            {
                this._CAR003_EArsivSemt = value;
                OnPropertyChanged("CAR003_EArsivSemt"); 
            }                         
        }
       
        /// <summary> NVARCHAR (54) Allow Null </summary>
        public string CAR003_EArsivIL 
        {
            get {   return this._CAR003_EArsivIL;   } 
            set 
            {
                this._CAR003_EArsivIL = value;
                OnPropertyChanged("CAR003_EArsivIL"); 
            }                         
        }
       
        /// <summary> NVARCHAR (120) Allow Null </summary>
        public string CAR003_Unvani2 
        {
            get {   return this._CAR003_Unvani2;   } 
            set 
            {
                this._CAR003_Unvani2 = value;
                OnPropertyChanged("CAR003_Unvani2"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR003_YOKCSeriNo 
        {
            get {   return this._CAR003_YOKCSeriNo;   } 
            set 
            {
                this._CAR003_YOKCSeriNo = value;
                OnPropertyChanged("CAR003_YOKCSeriNo"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_YOKCZRaporuNo 
        {
            get {   return this._CAR003_YOKCZRaporuNo;   } 
            set 
            {
                this._CAR003_YOKCZRaporuNo = value;
                OnPropertyChanged("CAR003_YOKCZRaporuNo"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_YOKCBelgeTipi 
        {
            get {   return this._CAR003_YOKCBelgeTipi;   } 
            set 
            {
                this._CAR003_YOKCBelgeTipi = value;
                OnPropertyChanged("CAR003_YOKCBelgeTipi"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_YOKCBilgiFisiTipi 
        {
            get {   return this._CAR003_YOKCBilgiFisiTipi;   } 
            set 
            {
                this._CAR003_YOKCBilgiFisiTipi = value;
                OnPropertyChanged("CAR003_YOKCBilgiFisiTipi"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_YOKCFisNo 
        {
            get {   return this._CAR003_YOKCFisNo;   } 
            set 
            {
                this._CAR003_YOKCFisNo = value;
                OnPropertyChanged("CAR003_YOKCFisNo"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_YOKCFisTarihi 
        {
            get {   return this._CAR003_YOKCFisTarihi;   } 
            set 
            {
                this._CAR003_YOKCFisTarihi = value;
                OnPropertyChanged("CAR003_YOKCFisTarihi"); 
            }                         
        }
       
        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string CAR003_OdemeTurKodu 
        {
            get {   return this._CAR003_OdemeTurKodu;   } 
            set 
            {
                this._CAR003_OdemeTurKodu = value;
                OnPropertyChanged("CAR003_OdemeTurKodu"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_VergiDairesiKodu 
        {
            get {   return this._CAR003_VergiDairesiKodu;   } 
            set 
            {
                this._CAR003_VergiDairesiKodu = value;
                OnPropertyChanged("CAR003_VergiDairesiKodu"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR003_FiiliIhracatTarihi 
        {
            get {   return this._CAR003_FiiliIhracatTarihi;   } 
            set 
            {
                this._CAR003_FiiliIhracatTarihi = value;
                OnPropertyChanged("CAR003_FiiliIhracatTarihi"); 
            }                         
        }
       
        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR003_YOKCFisSaat 
        {
            get {   return this._CAR003_YOKCFisSaat;   } 
            set 
            {
                this._CAR003_YOKCFisSaat = value;
                OnPropertyChanged("CAR003_YOKCFisSaat"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR003_YOKCDuzenlemeTip 
        {
            get {   return this._CAR003_YOKCDuzenlemeTip;   } 
            set 
            {
                this._CAR003_YOKCDuzenlemeTip = value;
                OnPropertyChanged("CAR003_YOKCDuzenlemeTip"); 
            }                         
        }
       
        /// <summary> NVARCHAR (12) Allow Null </summary>
        public string CAR003_YOKCBankaOnayKod 
        {
            get {   return this._CAR003_YOKCBankaOnayKod;   } 
            set 
            {
                this._CAR003_YOKCBankaOnayKod = value;
                OnPropertyChanged("CAR003_YOKCBankaOnayKod"); 
            }                         
        }
       
        /// <summary> NVARCHAR (48) Allow Null </summary>
        public string CAR003_YOKCUniqueID 
        {
            get {   return this._CAR003_YOKCUniqueID;   } 
            set 
            {
                this._CAR003_YOKCUniqueID = value;
                OnPropertyChanged("CAR003_YOKCUniqueID"); 
            }                         
        }

        /// <summary> INT (4) PRIMARY KEY * </summary>
        public int pk_CAR003_Row_ID 
        {
            private get {   return this._pk_CAR003_Row_ID;   } 
            set 
            {
                this._pk_CAR003_Row_ID = value;
                OnPropertyChanged("pk_CAR003_Row_ID"); 
            }                         
        }
        #endregion /// Properties             
        #region Tablo Bilgileri & Metodlar

        public void WhereAdd(CAR003E Property, object Deger, Operand And_Or = Operand.AND)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(CAR003E), Property), Deger, And_Or));
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

        private List<string> WhereList = new List<string>();
        private List<string> SetList = new List<string>(); 
        private string info_FullTableName = "YNS{0}.YNS{0}.CAR003";            
        private string[] info_PrimaryKeys = { "pk_CAR003_Row_ID" };
        private string[] info_IdentityKeys = { "CAR003_Row_ID" };

        private List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public CAR003()
        {
            ChangedProperties = new List<string>();
            this.PropertyChanged += CAR003_PropertyChanged;
        }

        public void AcceptChanges()
        {            
            ChangedProperties.Clear();
        }

        void CAR003_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
