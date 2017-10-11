using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Wms12m.Entity.Viewmodels
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
        private int _ID;
        private string _FiyatListNum;
        private string _MalKodu;
        private string _HesapKodu;
        private decimal _SatisFiyat1;
        private string _SatisFiyat1Birim;
        private int _SatisFiyat1BirimInt;
        private decimal _DovizSatisFiyat1;
        private string _DovizSF1Birim;
        private int _DovizSF1BirimInt;
        private string _DovizCinsi;
        private int _ROW_ID;
        private short _Durum;
        private bool _Onay;
        private string _Onaylayan;
        private DateTime? _SMOnayTarih;
        private bool _SPGMYOnay;
        private string _SPGMYOnaylayan;
        private DateTime? _SPGMYOnayTarih;
        private bool _GMOnay;
        private string _GMOnaylayan;
        private DateTime? _GMOnayTarih;
        private int _BasTarih;
        private int _BitTarih;
        private int _pk_ID;
        #endregion /// Fields


        /// <summary> INT (4) PrimaryKey IdentityKey * </summary>
        public int ID
        {
            get { return this._ID; }
            set { this._ID = value; }
        }

        /// <summary> VARCHAR (8) * </summary>
        public string FiyatListNum
        {
            get { return this._FiyatListNum; }
            set
            {
                this._FiyatListNum = value;
                OnPropertyChanged("FiyatListNum");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string MalKodu
        {
            get { return this._MalKodu; }
            set
            {
                this._MalKodu = value;
                OnPropertyChanged("MalKodu");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string HesapKodu
        {
            get { return this._HesapKodu; }
            set
            {
                this._HesapKodu = value;
                OnPropertyChanged("HesapKodu");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SatisFiyat1
        {
            get { return this._SatisFiyat1; }
            set
            {
                this._SatisFiyat1 = value;
                OnPropertyChanged("SatisFiyat1");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string SatisFiyat1Birim
        {
            get { return this._SatisFiyat1Birim; }
            set
            {
                this._SatisFiyat1Birim = value;
                OnPropertyChanged("SatisFiyat1Birim");
            }
        }

        /// <summary> INT (4) * </summary>
        public int SatisFiyat1BirimInt
        {
            get { return this._SatisFiyat1BirimInt; }
            set
            {
                this._SatisFiyat1BirimInt = value;
                OnPropertyChanged("SatisFiyat1BirimInt");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal DovizSatisFiyat1
        {
            get { return this._DovizSatisFiyat1; }
            set
            {
                this._DovizSatisFiyat1 = value;
                OnPropertyChanged("DovizSatisFiyat1");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string DovizSF1Birim
        {
            get { return this._DovizSF1Birim; }
            set
            {
                this._DovizSF1Birim = value;
                OnPropertyChanged("DovizSF1Birim");
            }
        }

        /// <summary> INT (4) * </summary>
        public int DovizSF1BirimInt
        {
            get { return this._DovizSF1BirimInt; }
            set
            {
                this._DovizSF1BirimInt = value;
                OnPropertyChanged("DovizSF1BirimInt");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string DovizCinsi
        {
            get { return this._DovizCinsi; }
            set
            {
                this._DovizCinsi = value;
                OnPropertyChanged("DovizCinsi");
            }
        }

        /// <summary> INT (4) * </summary>
        public int ROW_ID
        {
            get { return this._ROW_ID; }
            set
            {
                this._ROW_ID = value;
                OnPropertyChanged("ROW_ID");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short Durum
        {
            get { return this._Durum; }
            set
            {
                this._Durum = value;
                OnPropertyChanged("Durum");
            }
        }

        /// <summary> BIT (1) * </summary>
        public bool Onay
        {
            get { return this._Onay; }
            set
            {
                this._Onay = value;
                OnPropertyChanged("Onay");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string Onaylayan
        {
            get { return this._Onaylayan; }
            set
            {
                this._Onaylayan = value;
                OnPropertyChanged("Onaylayan");
            }
        }

        /// <summary> SMALLDATETIME (4) Allow Null </summary>
        public DateTime? SMOnayTarih
        {
            get { return this._SMOnayTarih; }
            set
            {
                this._SMOnayTarih = value;
                OnPropertyChanged("SMOnayTarih");
            }
        }

        /// <summary> BIT (1) * </summary>
        public bool SPGMYOnay
        {
            get { return this._SPGMYOnay; }
            set
            {
                this._SPGMYOnay = value;
                OnPropertyChanged("SPGMYOnay");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string SPGMYOnaylayan
        {
            get { return this._SPGMYOnaylayan; }
            set
            {
                this._SPGMYOnaylayan = value;
                OnPropertyChanged("SPGMYOnaylayan");
            }
        }

        /// <summary> SMALLDATETIME (4) Allow Null </summary>
        public DateTime? SPGMYOnayTarih
        {
            get { return this._SPGMYOnayTarih; }
            set
            {
                this._SPGMYOnayTarih = value;
                OnPropertyChanged("SPGMYOnayTarih");
            }
        }

        /// <summary> BIT (1) * </summary>
        public bool GMOnay
        {
            get { return this._GMOnay; }
            set
            {
                this._GMOnay = value;
                OnPropertyChanged("GMOnay");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string GMOnaylayan
        {
            get { return this._GMOnaylayan; }
            set
            {
                this._GMOnaylayan = value;
                OnPropertyChanged("GMOnaylayan");
            }
        }

        /// <summary> SMALLDATETIME (4) Allow Null </summary>
        public DateTime? GMOnayTarih
        {
            get { return this._GMOnayTarih; }
            set
            {
                this._GMOnayTarih = value;
                OnPropertyChanged("GMOnayTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int BasTarih
        {
            get { return this._BasTarih; }
            set
            {
                this._BasTarih = value;
                OnPropertyChanged("BasTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int BitTarih
        {
            get { return this._BitTarih; }
            set
            {
                this._BitTarih = value;
                OnPropertyChanged("BitTarih");
            }
        }

        /// <summary> INT (4) PRIMARY KEY * </summary>
        public int pk_ID
        {
            private get { return this._pk_ID; }
            set
            {
                this._pk_ID = value;
                OnPropertyChanged("pk_ID");
            }
        }
        #endregion /// Properties       

        #region Tablo Bilgileri & Metodlar

        private List<string> WhereList = new List<string>();
        private List<string> SetList = new List<string>();
        private string info_FullTableName = "FINSAT6{0}.FINSAT6{0}.Fiyat";
        private string[] info_PrimaryKeys = { "pk_ID" };
        private string[] info_IdentityKeys = { "ID" };

        private List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public Fiyat()
        {
            ChangedProperties = new List<string>();
            this.PropertyChanged += Fiyat_PropertyChanged;
        }

        public void AcceptChanges()
        {
            ChangedProperties.Clear();
        }

        void Fiyat_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
    #endregion /// Fiyat Class

}
