using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using DevHelper;

namespace Wms12m.Entity
{
    #region CAR005E Enum 
    public enum CAR005E 
    {
        CAR005_Row_ID,
        CAR005_Secenek,
        CAR005_FaturaTarihi,
        CAR005_FaturaNo,
        CAR005_BA,
        CAR005_CariIslemTipi,
        CAR005_SatirTipi,
        CAR005_SatirNo,
        CAR005_SatirKodu,
        CAR005_Filler,
        CAR005_SatirAciklama,
        CAR005_CHKodu,
        CAR005_Tutar,
        CAR005_Oran,
        CAR005_TevkIslemTuru,
        CAR005_TevkOrani,
        CAR005_TevkKDVOrani,
        CAR005_EkBilgi1,
        CAR005_EFaturaOTVListeNo,
        CAR005_EFaturaTipi,
        CAR005_EFaturaDurumu,
        CAR005_EFaturaDonemBas,
        CAR005_EFaturaDonemBit,
        CAR005_EFaturaSure,
        CAR005_EFaturaSureBirimi,
        CAR005_EFaturaDonemAciklama,
        CAR005_EFaturaNot,
        CAR005_EFaturaReferansNo,
        CAR005_ParaBirimi,
        CAR005_DovizCinsi,
        CAR005_DovizKuru,
        CAR005_DovizTutari,
        CAR005_TeslimCHKodu,
        CAR005_TutarAlanDegeri,
        CAR005_KDVMuafAciklama,
        CAR005_EFaturaGonBirimEtiketi,
        CAR005_EFaturaAliciEtiketi,
        CAR005_AliciSiparisNo,
        CAR005_AliciSiparisTarihi,
        CAR005_IptalDurumu,
        CAR005_EArsivFaturaTipi,
        CAR005_EArsivFaturaTeslimSekli,
        CAR005_EArsivFaturaDurumu

    } 
    #endregion /// CAR005E Enum           
    public class CAR005 :  INotifyPropertyChanged
    {
        #region Properties
        #region Fields  
        private int _CAR005_Row_ID; 
        private short? _CAR005_Secenek; 
        private int? _CAR005_FaturaTarihi; 
        private string _CAR005_FaturaNo; 
        private string _CAR005_BA; 
        private short? _CAR005_CariIslemTipi; 
        private string _CAR005_SatirTipi; 
        private short? _CAR005_SatirNo; 
        private string _CAR005_SatirKodu; 
        private string _CAR005_Filler; 
        private string _CAR005_SatirAciklama; 
        private string _CAR005_CHKodu; 
        private decimal? _CAR005_Tutar; 
        private decimal? _CAR005_Oran; 
        private int? _CAR005_TevkIslemTuru; 
        private string _CAR005_TevkOrani; 
        private decimal? _CAR005_TevkKDVOrani; 
        private string _CAR005_EkBilgi1; 
        private string _CAR005_EFaturaOTVListeNo; 
        private byte? _CAR005_EFaturaTipi; 
        private short? _CAR005_EFaturaDurumu; 
        private int? _CAR005_EFaturaDonemBas; 
        private int? _CAR005_EFaturaDonemBit; 
        private int? _CAR005_EFaturaSure; 
        private byte? _CAR005_EFaturaSureBirimi; 
        private string _CAR005_EFaturaDonemAciklama; 
        private string _CAR005_EFaturaNot; 
        private string _CAR005_EFaturaReferansNo; 
        private byte? _CAR005_ParaBirimi; 
        private string _CAR005_DovizCinsi; 
        private decimal? _CAR005_DovizKuru; 
        private decimal? _CAR005_DovizTutari; 
        private string _CAR005_TeslimCHKodu; 
        private byte? _CAR005_TutarAlanDegeri; 
        private string _CAR005_KDVMuafAciklama; 
        private string _CAR005_EFaturaGonBirimEtiketi; 
        private string _CAR005_EFaturaAliciEtiketi; 
        private string _CAR005_AliciSiparisNo; 
        private int? _CAR005_AliciSiparisTarihi; 
        private byte? _CAR005_IptalDurumu; 
        private byte? _CAR005_EArsivFaturaTipi; 
        private byte? _CAR005_EArsivFaturaTeslimSekli; 
        private short? _CAR005_EArsivFaturaDurumu; 
        private int _pk_CAR005_Row_ID;
        #endregion /// Fields
     
       
        /// <summary> INT (4) PrimaryKey IdentityKey * </summary>
        public int CAR005_Row_ID 
        {
            get {   return this._CAR005_Row_ID;   }                          
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR005_Secenek 
        {
            get {   return this._CAR005_Secenek;   } 
            set 
            {
                this._CAR005_Secenek = value;
                OnPropertyChanged("CAR005_Secenek"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR005_FaturaTarihi 
        {
            get {   return this._CAR005_FaturaTarihi;   } 
            set 
            {
                this._CAR005_FaturaTarihi = value;
                OnPropertyChanged("CAR005_FaturaTarihi"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR005_FaturaNo 
        {
            get {   return this._CAR005_FaturaNo;   } 
            set 
            {
                this._CAR005_FaturaNo = value;
                OnPropertyChanged("CAR005_FaturaNo"); 
            }                         
        }
       
        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string CAR005_BA 
        {
            get {   return this._CAR005_BA;   } 
            set 
            {
                this._CAR005_BA = value;
                OnPropertyChanged("CAR005_BA"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR005_CariIslemTipi 
        {
            get {   return this._CAR005_CariIslemTipi;   } 
            set 
            {
                this._CAR005_CariIslemTipi = value;
                OnPropertyChanged("CAR005_CariIslemTipi"); 
            }                         
        }
       
        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string CAR005_SatirTipi 
        {
            get {   return this._CAR005_SatirTipi;   } 
            set 
            {
                this._CAR005_SatirTipi = value;
                OnPropertyChanged("CAR005_SatirTipi"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR005_SatirNo 
        {
            get {   return this._CAR005_SatirNo;   } 
            set 
            {
                this._CAR005_SatirNo = value;
                OnPropertyChanged("CAR005_SatirNo"); 
            }                         
        }
       
        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR005_SatirKodu 
        {
            get {   return this._CAR005_SatirKodu;   } 
            set 
            {
                this._CAR005_SatirKodu = value;
                OnPropertyChanged("CAR005_SatirKodu"); 
            }                         
        }
       
        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string CAR005_Filler 
        {
            get {   return this._CAR005_Filler;   } 
            set 
            {
                this._CAR005_Filler = value;
                OnPropertyChanged("CAR005_Filler"); 
            }                         
        }
       
        /// <summary> NVARCHAR (120) Allow Null </summary>
        public string CAR005_SatirAciklama 
        {
            get {   return this._CAR005_SatirAciklama;   } 
            set 
            {
                this._CAR005_SatirAciklama = value;
                OnPropertyChanged("CAR005_SatirAciklama"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR005_CHKodu 
        {
            get {   return this._CAR005_CHKodu;   } 
            set 
            {
                this._CAR005_CHKodu = value;
                OnPropertyChanged("CAR005_CHKodu"); 
            }                         
        }
       
        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR005_Tutar 
        {
            get {   return this._CAR005_Tutar;   } 
            set 
            {
                this._CAR005_Tutar = value;
                OnPropertyChanged("CAR005_Tutar"); 
            }                         
        }
       
        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? CAR005_Oran 
        {
            get {   return this._CAR005_Oran;   } 
            set 
            {
                this._CAR005_Oran = value;
                OnPropertyChanged("CAR005_Oran"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR005_TevkIslemTuru 
        {
            get {   return this._CAR005_TevkIslemTuru;   } 
            set 
            {
                this._CAR005_TevkIslemTuru = value;
                OnPropertyChanged("CAR005_TevkIslemTuru"); 
            }                         
        }
       
        /// <summary> NVARCHAR (14) Allow Null </summary>
        public string CAR005_TevkOrani 
        {
            get {   return this._CAR005_TevkOrani;   } 
            set 
            {
                this._CAR005_TevkOrani = value;
                OnPropertyChanged("CAR005_TevkOrani"); 
            }                         
        }
       
        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? CAR005_TevkKDVOrani 
        {
            get {   return this._CAR005_TevkKDVOrani;   } 
            set 
            {
                this._CAR005_TevkKDVOrani = value;
                OnPropertyChanged("CAR005_TevkKDVOrani"); 
            }                         
        }
       
        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR005_EkBilgi1 
        {
            get {   return this._CAR005_EkBilgi1;   } 
            set 
            {
                this._CAR005_EkBilgi1 = value;
                OnPropertyChanged("CAR005_EkBilgi1"); 
            }                         
        }
       
        /// <summary> NVARCHAR (4) Allow Null </summary>
        public string CAR005_EFaturaOTVListeNo 
        {
            get {   return this._CAR005_EFaturaOTVListeNo;   } 
            set 
            {
                this._CAR005_EFaturaOTVListeNo = value;
                OnPropertyChanged("CAR005_EFaturaOTVListeNo"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR005_EFaturaTipi 
        {
            get {   return this._CAR005_EFaturaTipi;   } 
            set 
            {
                this._CAR005_EFaturaTipi = value;
                OnPropertyChanged("CAR005_EFaturaTipi"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR005_EFaturaDurumu 
        {
            get {   return this._CAR005_EFaturaDurumu;   } 
            set 
            {
                this._CAR005_EFaturaDurumu = value;
                OnPropertyChanged("CAR005_EFaturaDurumu"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR005_EFaturaDonemBas 
        {
            get {   return this._CAR005_EFaturaDonemBas;   } 
            set 
            {
                this._CAR005_EFaturaDonemBas = value;
                OnPropertyChanged("CAR005_EFaturaDonemBas"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR005_EFaturaDonemBit 
        {
            get {   return this._CAR005_EFaturaDonemBit;   } 
            set 
            {
                this._CAR005_EFaturaDonemBit = value;
                OnPropertyChanged("CAR005_EFaturaDonemBit"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR005_EFaturaSure 
        {
            get {   return this._CAR005_EFaturaSure;   } 
            set 
            {
                this._CAR005_EFaturaSure = value;
                OnPropertyChanged("CAR005_EFaturaSure"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR005_EFaturaSureBirimi 
        {
            get {   return this._CAR005_EFaturaSureBirimi;   } 
            set 
            {
                this._CAR005_EFaturaSureBirimi = value;
                OnPropertyChanged("CAR005_EFaturaSureBirimi"); 
            }                         
        }
       
        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR005_EFaturaDonemAciklama 
        {
            get {   return this._CAR005_EFaturaDonemAciklama;   } 
            set 
            {
                this._CAR005_EFaturaDonemAciklama = value;
                OnPropertyChanged("CAR005_EFaturaDonemAciklama"); 
            }                         
        }
       
        /// <summary> NVARCHAR (512) Allow Null </summary>
        public string CAR005_EFaturaNot 
        {
            get {   return this._CAR005_EFaturaNot;   } 
            set 
            {
                this._CAR005_EFaturaNot = value;
                OnPropertyChanged("CAR005_EFaturaNot"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR005_EFaturaReferansNo 
        {
            get {   return this._CAR005_EFaturaReferansNo;   } 
            set 
            {
                this._CAR005_EFaturaReferansNo = value;
                OnPropertyChanged("CAR005_EFaturaReferansNo"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR005_ParaBirimi 
        {
            get {   return this._CAR005_ParaBirimi;   } 
            set 
            {
                this._CAR005_ParaBirimi = value;
                OnPropertyChanged("CAR005_ParaBirimi"); 
            }                         
        }
       
        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string CAR005_DovizCinsi 
        {
            get {   return this._CAR005_DovizCinsi;   } 
            set 
            {
                this._CAR005_DovizCinsi = value;
                OnPropertyChanged("CAR005_DovizCinsi"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR005_DovizKuru 
        {
            get {   return this._CAR005_DovizKuru;   } 
            set 
            {
                this._CAR005_DovizKuru = value;
                OnPropertyChanged("CAR005_DovizKuru"); 
            }                         
        }
       
        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR005_DovizTutari 
        {
            get {   return this._CAR005_DovizTutari;   } 
            set 
            {
                this._CAR005_DovizTutari = value;
                OnPropertyChanged("CAR005_DovizTutari"); 
            }                         
        }
       
        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR005_TeslimCHKodu 
        {
            get {   return this._CAR005_TeslimCHKodu;   } 
            set 
            {
                this._CAR005_TeslimCHKodu = value;
                OnPropertyChanged("CAR005_TeslimCHKodu"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR005_TutarAlanDegeri 
        {
            get {   return this._CAR005_TutarAlanDegeri;   } 
            set 
            {
                this._CAR005_TutarAlanDegeri = value;
                OnPropertyChanged("CAR005_TutarAlanDegeri"); 
            }                         
        }
       
        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR005_KDVMuafAciklama 
        {
            get {   return this._CAR005_KDVMuafAciklama;   } 
            set 
            {
                this._CAR005_KDVMuafAciklama = value;
                OnPropertyChanged("CAR005_KDVMuafAciklama"); 
            }                         
        }
       
        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string CAR005_EFaturaGonBirimEtiketi 
        {
            get {   return this._CAR005_EFaturaGonBirimEtiketi;   } 
            set 
            {
                this._CAR005_EFaturaGonBirimEtiketi = value;
                OnPropertyChanged("CAR005_EFaturaGonBirimEtiketi"); 
            }                         
        }
       
        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string CAR005_EFaturaAliciEtiketi 
        {
            get {   return this._CAR005_EFaturaAliciEtiketi;   } 
            set 
            {
                this._CAR005_EFaturaAliciEtiketi = value;
                OnPropertyChanged("CAR005_EFaturaAliciEtiketi"); 
            }                         
        }
       
        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR005_AliciSiparisNo 
        {
            get {   return this._CAR005_AliciSiparisNo;   } 
            set 
            {
                this._CAR005_AliciSiparisNo = value;
                OnPropertyChanged("CAR005_AliciSiparisNo"); 
            }                         
        }
       
        /// <summary> INT (4) Allow Null </summary>
        public int? CAR005_AliciSiparisTarihi 
        {
            get {   return this._CAR005_AliciSiparisTarihi;   } 
            set 
            {
                this._CAR005_AliciSiparisTarihi = value;
                OnPropertyChanged("CAR005_AliciSiparisTarihi"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR005_IptalDurumu 
        {
            get {   return this._CAR005_IptalDurumu;   } 
            set 
            {
                this._CAR005_IptalDurumu = value;
                OnPropertyChanged("CAR005_IptalDurumu"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR005_EArsivFaturaTipi 
        {
            get {   return this._CAR005_EArsivFaturaTipi;   } 
            set 
            {
                this._CAR005_EArsivFaturaTipi = value;
                OnPropertyChanged("CAR005_EArsivFaturaTipi"); 
            }                         
        }
       
        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR005_EArsivFaturaTeslimSekli 
        {
            get {   return this._CAR005_EArsivFaturaTeslimSekli;   } 
            set 
            {
                this._CAR005_EArsivFaturaTeslimSekli = value;
                OnPropertyChanged("CAR005_EArsivFaturaTeslimSekli"); 
            }                         
        }
       
        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR005_EArsivFaturaDurumu 
        {
            get {   return this._CAR005_EArsivFaturaDurumu;   } 
            set 
            {
                this._CAR005_EArsivFaturaDurumu = value;
                OnPropertyChanged("CAR005_EArsivFaturaDurumu"); 
            }                         
        }

        /// <summary> INT (4) PRIMARY KEY * </summary>
        public int pk_CAR005_Row_ID 
        {
            private get {   return this._pk_CAR005_Row_ID;   } 
            set 
            {
                this._pk_CAR005_Row_ID = value;
                OnPropertyChanged("pk_CAR005_Row_ID"); 
            }                         
        }
        #endregion /// Properties             
        #region Tablo Bilgileri & Metodlar

        public void WhereAdd(CAR005E Property, object Deger, Operand And_Or = Operand.AND)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(CAR005E), Property), Deger, And_Or));
        }

        public void WhereAdd(CAR005E Property, Islem islem, object Deger, Operand And_Or = Operand.AND)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(CAR005E), Property), islem, Deger, And_Or));
        }

        public void WhereAdd(CAR005E Property, Operand In_NotIn, params object[] Degerler_Parantez)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(CAR005E), Property), In_NotIn, Degerler_Parantez));
        }

        public void WhereAdd(params object[] Degerler)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Degerler));
        }

        /// <summary> Set işleminde Property " = " eşit ile otomatik başlar. </summary>
        public void SetAdd(CAR005E Property, params object[] Degerler)
        {
            SetList.Add(SqlExperOperatorIslem.SetAdd(Enum.GetName(typeof(CAR005E), Property), Degerler));
        }

        private List<string> WhereList = new List<string>();
        private List<string> SetList = new List<string>(); 
        private string info_FullTableName = "YNS{0}.YNS{0}.CAR005";            
        private string[] info_PrimaryKeys = { "pk_CAR005_Row_ID" };
        private string[] info_IdentityKeys = { "CAR005_Row_ID" };

        private List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public CAR005()
        {
            ChangedProperties = new List<string>();
            this.PropertyChanged += CAR005_PropertyChanged;
        }

        public void AcceptChanges()
        {            
            ChangedProperties.Clear();
        }

        void CAR005_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
