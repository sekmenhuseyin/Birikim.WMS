using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
    public class RiskTanimToplu
    {
        public System.Nullable<bool> Onay { get; set; }

        public string HesapKodu { get; set; }

        public string Unvan { get; set; }

        public decimal SahsiCekLimiti { get; set; }

        public decimal MusteriCekLimiti { get; set; }

        public System.Nullable<decimal> YeniSahsiCekLimiti { get; set; }

        public System.Nullable<decimal> YeniMusteriCekLimiti { get; set; }
    }

    #region RiskTanim Class 

    #region RiskTanimE Enum 
    public enum RiskTanimE
    {
        ID,
        HesapKodu,
        Unvan,
        SahsiCekLimiti,
        MusteriCekLimiti,
        SMOnay,
        SMOnaylayan,
        SMOnayTarih,
        SPGMYOnay,
        SPGMYOnaylayan,
        SPGMYOnayTarih,
        MIGMYOnay,
        MIGMYOnaylayan,
        MIGMYOnayTarih,
        GMOnay,
        GMOnaylayan,
        GMOnayTarih,
        OnayTip,
        Durum

    }
    #endregion /// RiskTanimE Enum           

    public class RiskTanim : INotifyPropertyChanged
    {
        #region Properties
        #region Fields  
        private int _ID;
        private string _HesapKodu;
        private string _Unvan;
        private decimal _SahsiCekLimiti;
        private decimal _MusteriCekLimiti;
        private bool? _SMOnay;
        private string _SMOnaylayan;
        private DateTime? _SMOnayTarih;
        private bool? _SPGMYOnay;
        private string _SPGMYOnaylayan;
        private DateTime? _SPGMYOnayTarih;
        private bool? _MIGMYOnay;
        private string _MIGMYOnaylayan;
        private DateTime? _MIGMYOnayTarih;
        private bool? _GMOnay;
        private string _GMOnaylayan;
        private DateTime? _GMOnayTarih;
        private short? _OnayTip;
        private bool? _Durum;
        private int _pk_ID;
        #endregion /// Fields


        /// <summary> INT (4) PrimaryKey IdentityKey * </summary>
        public int ID
        {
            get { return this._ID; }
        }

        /// <summary> VARCHAR (30) * </summary>
        public string HesapKodu
        {
            get { return this._HesapKodu; }
            set
            {
                this._HesapKodu = value;
                OnPropertyChanged("HesapKodu");
            }
        }

        /// <summary> VARCHAR (100) Allow Null </summary>
        public string Unvan
        {
            get { return this._Unvan; }
            set
            {
                this._Unvan = value;
                OnPropertyChanged("Unvan");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SahsiCekLimiti
        {
            get { return this._SahsiCekLimiti; }
            set
            {
                this._SahsiCekLimiti = value;
                OnPropertyChanged("SahsiCekLimiti");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal MusteriCekLimiti
        {
            get { return this._MusteriCekLimiti; }
            set
            {
                this._MusteriCekLimiti = value;
                OnPropertyChanged("MusteriCekLimiti");
            }
        }

        /// <summary> BIT (1) Allow Null </summary>
        public bool? SMOnay
        {
            get { return this._SMOnay; }
            set
            {
                this._SMOnay = value;
                OnPropertyChanged("SMOnay");
            }
        }

        /// <summary> VARCHAR (50) Allow Null </summary>
        public string SMOnaylayan
        {
            get { return this._SMOnaylayan; }
            set
            {
                this._SMOnaylayan = value;
                OnPropertyChanged("SMOnaylayan");
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

        /// <summary> BIT (1) Allow Null </summary>
        public bool? SPGMYOnay
        {
            get { return this._SPGMYOnay; }
            set
            {
                this._SPGMYOnay = value;
                OnPropertyChanged("SPGMYOnay");
            }
        }

        /// <summary> VARCHAR (50) Allow Null </summary>
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

        /// <summary> BIT (1) Allow Null </summary>
        public bool? MIGMYOnay
        {
            get { return this._MIGMYOnay; }
            set
            {
                this._MIGMYOnay = value;
                OnPropertyChanged("MIGMYOnay");
            }
        }

        /// <summary> VARCHAR (50) Allow Null </summary>
        public string MIGMYOnaylayan
        {
            get { return this._MIGMYOnaylayan; }
            set
            {
                this._MIGMYOnaylayan = value;
                OnPropertyChanged("MIGMYOnaylayan");
            }
        }

        /// <summary> SMALLDATETIME (4) Allow Null </summary>
        public DateTime? MIGMYOnayTarih
        {
            get { return this._MIGMYOnayTarih; }
            set
            {
                this._MIGMYOnayTarih = value;
                OnPropertyChanged("MIGMYOnayTarih");
            }
        }

        /// <summary> BIT (1) Allow Null </summary>
        public bool? GMOnay
        {
            get { return this._GMOnay; }
            set
            {
                this._GMOnay = value;
                OnPropertyChanged("GMOnay");
            }
        }

        /// <summary> VARCHAR (50) Allow Null </summary>
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

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? OnayTip
        {
            get { return this._OnayTip; }
            set
            {
                this._OnayTip = value;
                OnPropertyChanged("OnayTip");
            }
        }

        /// <summary> BIT (1) Allow Null </summary>
        public bool? Durum
        {
            get { return this._Durum; }
            set
            {
                this._Durum = value;
                OnPropertyChanged("Durum");
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
        private string info_FullTableName = "FINSAT6{0}.FINSAT6{0}.RiskTanim";
        private string[] info_PrimaryKeys = { "pk_ID" };
        private string[] info_IdentityKeys = { "ID" };

        private List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public RiskTanim()
        {
            ChangedProperties = new List<string>();
            this.PropertyChanged += RiskTanim_PropertyChanged;
        }

        public void AcceptChanges()
        {
            ChangedProperties.Clear();
        }

        void RiskTanim_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
    #endregion /// RiskTanim Class
}
