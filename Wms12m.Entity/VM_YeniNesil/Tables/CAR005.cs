using DevHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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
    public class CAR005 : INotifyPropertyChanged
    {
        #region Properties
        #region Fields  
        int cAR005_Row_ID;
        short? cAR005_Secenek;
        int? cAR005_FaturaTarihi;
        string cAR005_FaturaNo;
        string cAR005_BA;
        short? cAR005_CariIslemTipi;
        string cAR005_SatirTipi;
        short? cAR005_SatirNo;
        string cAR005_SatirKodu;
        string cAR005_Filler;
        string cAR005_SatirAciklama;
        string cAR005_CHKodu;
        decimal? cAR005_Tutar;
        decimal? cAR005_Oran;
        int? cAR005_TevkIslemTuru;
        string cAR005_TevkOrani;
        decimal? cAR005_TevkKDVOrani;
        string cAR005_EkBilgi1;
        string cAR005_EFaturaOTVListeNo;
        byte? cAR005_EFaturaTipi;
        short? cAR005_EFaturaDurumu;
        int? cAR005_EFaturaDonemBas;
        int? cAR005_EFaturaDonemBit;
        int? cAR005_EFaturaSure;
        byte? cAR005_EFaturaSureBirimi;
        string cAR005_EFaturaDonemAciklama;
        string cAR005_EFaturaNot;
        string cAR005_EFaturaReferansNo;
        byte? cAR005_ParaBirimi;
        string cAR005_DovizCinsi;
        decimal? cAR005_DovizKuru;
        decimal? cAR005_DovizTutari;
        string cAR005_TeslimCHKodu;
        byte? cAR005_TutarAlanDegeri;
        string cAR005_KDVMuafAciklama;
        string cAR005_EFaturaGonBirimEtiketi;
        string cAR005_EFaturaAliciEtiketi;
        string cAR005_AliciSiparisNo;
        int? cAR005_AliciSiparisTarihi;
        byte? cAR005_IptalDurumu;
        byte? cAR005_EArsivFaturaTipi;
        byte? cAR005_EArsivFaturaTeslimSekli;
        short? cAR005_EArsivFaturaDurumu;
        int _pk_CAR005_Row_ID;
        #endregion /// Fields

        /// <summary> INT (4) PrimaryKey IdentityKey * </summary>
        public int CAR005_Row_ID => cAR005_Row_ID;

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR005_Secenek
        {
            get { return cAR005_Secenek; }
            set
            {
                cAR005_Secenek = value;
                OnPropertyChanged("CAR005_Secenek");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR005_FaturaTarihi
        {
            get { return cAR005_FaturaTarihi; }
            set
            {
                cAR005_FaturaTarihi = value;
                OnPropertyChanged("CAR005_FaturaTarihi");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR005_FaturaNo
        {
            get { return cAR005_FaturaNo; }
            set
            {
                cAR005_FaturaNo = value;
                OnPropertyChanged("CAR005_FaturaNo");
            }
        }

        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string CAR005_BA
        {
            get { return cAR005_BA; }
            set
            {
                cAR005_BA = value;
                OnPropertyChanged("CAR005_BA");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR005_CariIslemTipi
        {
            get { return cAR005_CariIslemTipi; }
            set
            {
                cAR005_CariIslemTipi = value;
                OnPropertyChanged("CAR005_CariIslemTipi");
            }
        }

        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string CAR005_SatirTipi
        {
            get { return cAR005_SatirTipi; }
            set
            {
                cAR005_SatirTipi = value;
                OnPropertyChanged("CAR005_SatirTipi");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR005_SatirNo
        {
            get { return cAR005_SatirNo; }
            set
            {
                cAR005_SatirNo = value;
                OnPropertyChanged("CAR005_SatirNo");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR005_SatirKodu
        {
            get { return cAR005_SatirKodu; }
            set
            {
                cAR005_SatirKodu = value;
                OnPropertyChanged("CAR005_SatirKodu");
            }
        }

        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string CAR005_Filler
        {
            get { return cAR005_Filler; }
            set
            {
                cAR005_Filler = value;
                OnPropertyChanged("CAR005_Filler");
            }
        }

        /// <summary> NVARCHAR (120) Allow Null </summary>
        public string CAR005_SatirAciklama
        {
            get { return cAR005_SatirAciklama; }
            set
            {
                cAR005_SatirAciklama = value;
                OnPropertyChanged("CAR005_SatirAciklama");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR005_CHKodu
        {
            get { return cAR005_CHKodu; }
            set
            {
                cAR005_CHKodu = value;
                OnPropertyChanged("CAR005_CHKodu");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR005_Tutar
        {
            get { return cAR005_Tutar; }
            set
            {
                cAR005_Tutar = value;
                OnPropertyChanged("CAR005_Tutar");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? CAR005_Oran
        {
            get { return cAR005_Oran; }
            set
            {
                cAR005_Oran = value;
                OnPropertyChanged("CAR005_Oran");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR005_TevkIslemTuru
        {
            get { return cAR005_TevkIslemTuru; }
            set
            {
                cAR005_TevkIslemTuru = value;
                OnPropertyChanged("CAR005_TevkIslemTuru");
            }
        }

        /// <summary> NVARCHAR (14) Allow Null </summary>
        public string CAR005_TevkOrani
        {
            get { return cAR005_TevkOrani; }
            set
            {
                cAR005_TevkOrani = value;
                OnPropertyChanged("CAR005_TevkOrani");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? CAR005_TevkKDVOrani
        {
            get { return cAR005_TevkKDVOrani; }
            set
            {
                cAR005_TevkKDVOrani = value;
                OnPropertyChanged("CAR005_TevkKDVOrani");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR005_EkBilgi1
        {
            get { return cAR005_EkBilgi1; }
            set
            {
                cAR005_EkBilgi1 = value;
                OnPropertyChanged("CAR005_EkBilgi1");
            }
        }

        /// <summary> NVARCHAR (4) Allow Null </summary>
        public string CAR005_EFaturaOTVListeNo
        {
            get { return cAR005_EFaturaOTVListeNo; }
            set
            {
                cAR005_EFaturaOTVListeNo = value;
                OnPropertyChanged("CAR005_EFaturaOTVListeNo");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR005_EFaturaTipi
        {
            get { return cAR005_EFaturaTipi; }
            set
            {
                cAR005_EFaturaTipi = value;
                OnPropertyChanged("CAR005_EFaturaTipi");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR005_EFaturaDurumu
        {
            get { return cAR005_EFaturaDurumu; }
            set
            {
                cAR005_EFaturaDurumu = value;
                OnPropertyChanged("CAR005_EFaturaDurumu");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR005_EFaturaDonemBas
        {
            get { return cAR005_EFaturaDonemBas; }
            set
            {
                cAR005_EFaturaDonemBas = value;
                OnPropertyChanged("CAR005_EFaturaDonemBas");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR005_EFaturaDonemBit
        {
            get { return cAR005_EFaturaDonemBit; }
            set
            {
                cAR005_EFaturaDonemBit = value;
                OnPropertyChanged("CAR005_EFaturaDonemBit");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR005_EFaturaSure
        {
            get { return cAR005_EFaturaSure; }
            set
            {
                cAR005_EFaturaSure = value;
                OnPropertyChanged("CAR005_EFaturaSure");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR005_EFaturaSureBirimi
        {
            get { return cAR005_EFaturaSureBirimi; }
            set
            {
                cAR005_EFaturaSureBirimi = value;
                OnPropertyChanged("CAR005_EFaturaSureBirimi");
            }
        }

        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR005_EFaturaDonemAciklama
        {
            get { return cAR005_EFaturaDonemAciklama; }
            set
            {
                cAR005_EFaturaDonemAciklama = value;
                OnPropertyChanged("CAR005_EFaturaDonemAciklama");
            }
        }

        /// <summary> NVARCHAR (512) Allow Null </summary>
        public string CAR005_EFaturaNot
        {
            get { return cAR005_EFaturaNot; }
            set
            {
                cAR005_EFaturaNot = value;
                OnPropertyChanged("CAR005_EFaturaNot");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR005_EFaturaReferansNo
        {
            get { return cAR005_EFaturaReferansNo; }
            set
            {
                cAR005_EFaturaReferansNo = value;
                OnPropertyChanged("CAR005_EFaturaReferansNo");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR005_ParaBirimi
        {
            get { return cAR005_ParaBirimi; }
            set
            {
                cAR005_ParaBirimi = value;
                OnPropertyChanged("CAR005_ParaBirimi");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string CAR005_DovizCinsi
        {
            get { return cAR005_DovizCinsi; }
            set
            {
                cAR005_DovizCinsi = value;
                OnPropertyChanged("CAR005_DovizCinsi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR005_DovizKuru
        {
            get { return cAR005_DovizKuru; }
            set
            {
                cAR005_DovizKuru = value;
                OnPropertyChanged("CAR005_DovizKuru");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR005_DovizTutari
        {
            get { return cAR005_DovizTutari; }
            set
            {
                cAR005_DovizTutari = value;
                OnPropertyChanged("CAR005_DovizTutari");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR005_TeslimCHKodu
        {
            get { return cAR005_TeslimCHKodu; }
            set
            {
                cAR005_TeslimCHKodu = value;
                OnPropertyChanged("CAR005_TeslimCHKodu");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR005_TutarAlanDegeri
        {
            get { return cAR005_TutarAlanDegeri; }
            set
            {
                cAR005_TutarAlanDegeri = value;
                OnPropertyChanged("CAR005_TutarAlanDegeri");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR005_KDVMuafAciklama
        {
            get { return cAR005_KDVMuafAciklama; }
            set
            {
                cAR005_KDVMuafAciklama = value;
                OnPropertyChanged("CAR005_KDVMuafAciklama");
            }
        }

        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string CAR005_EFaturaGonBirimEtiketi
        {
            get { return cAR005_EFaturaGonBirimEtiketi; }
            set
            {
                cAR005_EFaturaGonBirimEtiketi = value;
                OnPropertyChanged("CAR005_EFaturaGonBirimEtiketi");
            }
        }

        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string CAR005_EFaturaAliciEtiketi
        {
            get { return cAR005_EFaturaAliciEtiketi; }
            set
            {
                cAR005_EFaturaAliciEtiketi = value;
                OnPropertyChanged("CAR005_EFaturaAliciEtiketi");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR005_AliciSiparisNo
        {
            get { return cAR005_AliciSiparisNo; }
            set
            {
                cAR005_AliciSiparisNo = value;
                OnPropertyChanged("CAR005_AliciSiparisNo");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR005_AliciSiparisTarihi
        {
            get { return cAR005_AliciSiparisTarihi; }
            set
            {
                cAR005_AliciSiparisTarihi = value;
                OnPropertyChanged("CAR005_AliciSiparisTarihi");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR005_IptalDurumu
        {
            get { return cAR005_IptalDurumu; }
            set
            {
                cAR005_IptalDurumu = value;
                OnPropertyChanged("CAR005_IptalDurumu");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR005_EArsivFaturaTipi
        {
            get { return cAR005_EArsivFaturaTipi; }
            set
            {
                cAR005_EArsivFaturaTipi = value;
                OnPropertyChanged("CAR005_EArsivFaturaTipi");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR005_EArsivFaturaTeslimSekli
        {
            get { return cAR005_EArsivFaturaTeslimSekli; }
            set
            {
                cAR005_EArsivFaturaTeslimSekli = value;
                OnPropertyChanged("CAR005_EArsivFaturaTeslimSekli");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR005_EArsivFaturaDurumu
        {
            get { return cAR005_EArsivFaturaDurumu; }
            set
            {
                cAR005_EArsivFaturaDurumu = value;
                OnPropertyChanged("CAR005_EArsivFaturaDurumu");
            }
        }

        /// <summary> INT (4) PRIMARY KEY * </summary>
        public int pk_CAR005_Row_ID
        {
            private get { return _pk_CAR005_Row_ID; }
            set
            {
                _pk_CAR005_Row_ID = value;
                OnPropertyChanged("pk_CAR005_Row_ID");
            }
        }

        public CAR005()
        {
            ChangedProperties = new List<string>();
            PropertyChanged += CAR005_PropertyChanged;
        }
        #endregion /// Properties             
        #region Tablo Bilgileri & Metodlar

        public void WhereAdd(CAR005E Property, object deger, Operand and_Or = Operand.AND)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(CAR005E), Property), deger, and_Or));
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

        List<string> WhereList = new List<string>();
        List<string> SetList = new List<string>();
        string info_FullTableName = "YNS{0}.YNS{0}.CAR005";
        string[] info_PrimaryKeys = { "pk_CAR005_Row_ID" };
        string[] info_IdentityKeys = { "CAR005_Row_ID" };

        List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public void AcceptChanges() => ChangedProperties.Clear();

        void CAR005_PropertyChanged(object sender, PropertyChangedEventArgs e)
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