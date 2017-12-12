using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Wms12m.Entity
{
    public class FiyatKoleksiyonSelect
    {
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kod4 { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string TipKod { get; set; }
        /// <summary> VarChar(8) (Not Null) </summary>
        public string FiyatListNum { get; set; }
        /// <summary> Decimal(24,6) (Not Null) </summary>
        public decimal SatisFiyat1 { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string SatisFiyat1Birim { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int SatisFiyat1BirimInt { get; set; }
        /// <summary> Decimal(24,6) (Not Null) </summary>
        public decimal DovizSatisFiyat1 { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string DovizSF1Birim { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int DovizSF1BirimInt { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string DovizCinsi { get; set; }
        /// <summary> Bit (Allow Null) </summary>
        public bool? Onay { get; set; }
        /// <summary> VarChar(19) (Not Null) </summary>
        public string Durum { get; set; }
    }
    public class FiyatGrupSelect
    {
        /// <summary> VarChar(20) (Not Null) </summary>
        public string GrupKod { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kalite { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string En { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Boy { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kalinlik { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Yuzey { get; set; }
        /// <summary> VarChar(8) (Not Null) </summary>
        public string FiyatListNum { get; set; }
        /// <summary> Decimal(24,6) (Not Null) </summary>
        public decimal SatisFiyat1 { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string SatisFiyat1Birim { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int SatisFiyat1BirimInt { get; set; }
        /// <summary> Decimal(24,6) (Not Null) </summary>
        public decimal DovizSatisFiyat1 { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string DovizSF1Birim { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int DovizSF1BirimInt { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string DovizCinsi { get; set; }
        /// <summary> Bit (Allow Null) </summary>
        public bool? Onay { get; set; }
        /// <summary> VarChar(19) (Not Null) </summary>
        public string Durum { get; set; }
    }

    #region Fiyat Class 

    #region FiyatE Enum 
    public enum FiyatE
    {
        ID,
        FiyatListNum,
        MalKodu,
        HesapKodu,
        SatisFiyat1,
        SatisFiyat1Birim,
        SatisFiyat1BirimInt,
        DovizSatisFiyat1,
        DovizSF1Birim,
        DovizSF1BirimInt,
        DovizCinsi,
        ROW_ID,
        Durum,
        Onay,
        Onaylayan,
        SMOnayTarih,
        SPGMYOnay,
        SPGMYOnaylayan,
        SPGMYOnayTarih,
        GMOnay,
        GMOnaylayan,
        GMOnayTarih,
        BasTarih,
        BitTarih

    }
    #endregion /// FiyatE Enum           

    public class Fiyat : INotifyPropertyChanged
    {
        #region Properties
        #region Fields  
        int _ID;
        string _FiyatListNum;
        string _MalKodu;
        string _HesapKodu;
        decimal _SatisFiyat1;
        string _SatisFiyat1Birim;
        int _SatisFiyat1BirimInt;
        decimal _DovizSatisFiyat1;
        string _DovizSF1Birim;
        int _DovizSF1BirimInt;
        string _DovizCinsi;
        int _ROW_ID;
        short _Durum;
        bool _Onay;
        string _Onaylayan;
        DateTime? _SMOnayTarih;
        bool _SPGMYOnay;
        string _SPGMYOnaylayan;
        DateTime? _SPGMYOnayTarih;
        bool _GMOnay;
        string _GMOnaylayan;
        DateTime? _GMOnayTarih;
        int _BasTarih;
        int _BitTarih;
        int _pk_ID;
        #endregion /// Fields

        /// <summary> INT (4) PrimaryKey IdentityKey * </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        /// <summary> VARCHAR (8) * </summary>
        public string FiyatListNum
        {
            get { return _FiyatListNum; }
            set
            {
                _FiyatListNum = value;
                OnPropertyChanged("FiyatListNum");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string MalKodu
        {
            get { return _MalKodu; }
            set
            {
                _MalKodu = value;
                OnPropertyChanged("MalKodu");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string HesapKodu
        {
            get { return _HesapKodu; }
            set
            {
                _HesapKodu = value;
                OnPropertyChanged("HesapKodu");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SatisFiyat1
        {
            get { return _SatisFiyat1; }
            set
            {
                _SatisFiyat1 = value;
                OnPropertyChanged("SatisFiyat1");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string SatisFiyat1Birim
        {
            get { return _SatisFiyat1Birim; }
            set
            {
                _SatisFiyat1Birim = value;
                OnPropertyChanged("SatisFiyat1Birim");
            }
        }

        /// <summary> INT (4) * </summary>
        public int SatisFiyat1BirimInt
        {
            get { return _SatisFiyat1BirimInt; }
            set
            {
                _SatisFiyat1BirimInt = value;
                OnPropertyChanged("SatisFiyat1BirimInt");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal DovizSatisFiyat1
        {
            get { return _DovizSatisFiyat1; }
            set
            {
                _DovizSatisFiyat1 = value;
                OnPropertyChanged("DovizSatisFiyat1");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string DovizSF1Birim
        {
            get { return _DovizSF1Birim; }
            set
            {
                _DovizSF1Birim = value;
                OnPropertyChanged("DovizSF1Birim");
            }
        }

        /// <summary> INT (4) * </summary>
        public int DovizSF1BirimInt
        {
            get { return _DovizSF1BirimInt; }
            set
            {
                _DovizSF1BirimInt = value;
                OnPropertyChanged("DovizSF1BirimInt");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string DovizCinsi
        {
            get { return _DovizCinsi; }
            set
            {
                _DovizCinsi = value;
                OnPropertyChanged("DovizCinsi");
            }
        }

        /// <summary> INT (4) * </summary>
        public int ROW_ID
        {
            get { return _ROW_ID; }
            set
            {
                _ROW_ID = value;
                OnPropertyChanged("ROW_ID");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short Durum
        {
            get { return _Durum; }
            set
            {
                _Durum = value;
                OnPropertyChanged("Durum");
            }
        }

        /// <summary> BIT (1) * </summary>
        public bool Onay
        {
            get { return _Onay; }
            set
            {
                _Onay = value;
                OnPropertyChanged("Onay");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string Onaylayan
        {
            get { return _Onaylayan; }
            set
            {
                _Onaylayan = value;
                OnPropertyChanged("Onaylayan");
            }
        }

        /// <summary> SMALLDATETIME (4) Allow Null </summary>
        public DateTime? SMOnayTarih
        {
            get { return _SMOnayTarih; }
            set
            {
                _SMOnayTarih = value;
                OnPropertyChanged("SMOnayTarih");
            }
        }

        /// <summary> BIT (1) * </summary>
        public bool SPGMYOnay
        {
            get { return _SPGMYOnay; }
            set
            {
                _SPGMYOnay = value;
                OnPropertyChanged("SPGMYOnay");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string SPGMYOnaylayan
        {
            get { return _SPGMYOnaylayan; }
            set
            {
                _SPGMYOnaylayan = value;
                OnPropertyChanged("SPGMYOnaylayan");
            }
        }

        /// <summary> SMALLDATETIME (4) Allow Null </summary>
        public DateTime? SPGMYOnayTarih
        {
            get { return _SPGMYOnayTarih; }
            set
            {
                _SPGMYOnayTarih = value;
                OnPropertyChanged("SPGMYOnayTarih");
            }
        }

        /// <summary> BIT (1) * </summary>
        public bool GMOnay
        {
            get { return _GMOnay; }
            set
            {
                _GMOnay = value;
                OnPropertyChanged("GMOnay");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string GMOnaylayan
        {
            get { return _GMOnaylayan; }
            set
            {
                _GMOnaylayan = value;
                OnPropertyChanged("GMOnaylayan");
            }
        }

        /// <summary> SMALLDATETIME (4) Allow Null </summary>
        public DateTime? GMOnayTarih
        {
            get { return _GMOnayTarih; }
            set
            {
                _GMOnayTarih = value;
                OnPropertyChanged("GMOnayTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int BasTarih
        {
            get { return _BasTarih; }
            set
            {
                _BasTarih = value;
                OnPropertyChanged("BasTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int BitTarih
        {
            get { return _BitTarih; }
            set
            {
                _BitTarih = value;
                OnPropertyChanged("BitTarih");
            }
        }

        /// <summary> INT (4) PRIMARY KEY * </summary>
        public int pk_ID
        {
            private get { return _pk_ID; }
            set
            {
                _pk_ID = value;
                OnPropertyChanged("pk_ID");
            }
        }
        #endregion /// Properties       

        #region Tablo Bilgileri & Metodlar

        List<string> WhereList = new List<string>();
        List<string> SetList = new List<string>();
        string info_FullTableName = "FINSAT6{0}.FINSAT6{0}.Fiyat";
        string[] info_PrimaryKeys = { "pk_ID" };
        string[] info_IdentityKeys = { "ID" };

        List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public Fiyat()
        {
            ChangedProperties = new List<string>();
            PropertyChanged += Fiyat_PropertyChanged;
        }

        public void AcceptChanges() => ChangedProperties.Clear();

        void Fiyat_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
    #endregion /// Fiyat Class
}