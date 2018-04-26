using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Wms12m.Entity
{
    public class RiskTanim : INotifyPropertyChanged
    {

        int _ID;
        string _HesapKodu;
        string _Unvan;
        decimal _SahsiCekLimiti;
        decimal _MusteriCekLimiti;
        bool? _SMOnay;
        string _SMOnaylayan;
        DateTime? _SMOnayTarih;
        bool? _SPGMYOnay;
        string _SPGMYOnaylayan;
        DateTime? _SPGMYOnayTarih;
        bool? _MIGMYOnay;
        string _MIGMYOnaylayan;
        DateTime? _MIGMYOnayTarih;
        bool? _GMOnay;
        string _GMOnaylayan;
        DateTime? _GMOnayTarih;
        short? _OnayTip;
        bool? _Durum;
        int _pk_ID;

        /// <summary> INT (4) PrimaryKey IdentityKey * </summary>
        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                // OnPropertyChanged("HesapKodu");
            }

        }

        /// <summary> VARCHAR (30) * </summary>
        public string HesapKodu
        {
            get { return _HesapKodu; }
            set
            {
                _HesapKodu = value;
                OnPropertyChanged("HesapKodu");
            }
        }

        /// <summary> VARCHAR (100) Allow Null </summary>
        public string Unvan
        {
            get { return _Unvan; }
            set
            {
                _Unvan = value;
                OnPropertyChanged("Unvan");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SahsiCekLimiti
        {
            get { return _SahsiCekLimiti; }
            set
            {
                _SahsiCekLimiti = value;
                OnPropertyChanged("SahsiCekLimiti");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal MusteriCekLimiti
        {
            get { return _MusteriCekLimiti; }
            set
            {
                _MusteriCekLimiti = value;
                OnPropertyChanged("MusteriCekLimiti");
            }
        }

        /// <summary> BIT (1) Allow Null </summary>
        public bool? SMOnay
        {
            get { return _SMOnay; }
            set
            {
                _SMOnay = value;
                OnPropertyChanged("SMOnay");
            }
        }

        /// <summary> VARCHAR (50) Allow Null </summary>
        public string SMOnaylayan
        {
            get { return _SMOnaylayan; }
            set
            {
                _SMOnaylayan = value;
                OnPropertyChanged("SMOnaylayan");
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

        /// <summary> BIT (1) Allow Null </summary>
        public bool? SPGMYOnay
        {
            get { return _SPGMYOnay; }
            set
            {
                _SPGMYOnay = value;
                OnPropertyChanged("SPGMYOnay");
            }
        }

        /// <summary> VARCHAR (50) Allow Null </summary>
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

        /// <summary> BIT (1) Allow Null </summary>
        public bool? MIGMYOnay
        {
            get { return _MIGMYOnay; }
            set
            {
                _MIGMYOnay = value;
                OnPropertyChanged("MIGMYOnay");
            }
        }

        /// <summary> VARCHAR (50) Allow Null </summary>
        public string MIGMYOnaylayan
        {
            get { return _MIGMYOnaylayan; }
            set
            {
                _MIGMYOnaylayan = value;
                OnPropertyChanged("MIGMYOnaylayan");
            }
        }

        /// <summary> SMALLDATETIME (4) Allow Null </summary>
        public DateTime? MIGMYOnayTarih
        {
            get { return _MIGMYOnayTarih; }
            set
            {
                _MIGMYOnayTarih = value;
                OnPropertyChanged("MIGMYOnayTarih");
            }
        }

        /// <summary> BIT (1) Allow Null </summary>
        public bool? GMOnay
        {
            get { return _GMOnay; }
            set
            {
                _GMOnay = value;
                OnPropertyChanged("GMOnay");
            }
        }

        /// <summary> VARCHAR (50) Allow Null </summary>
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

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? OnayTip
        {
            get { return _OnayTip; }
            set
            {
                _OnayTip = value;
                OnPropertyChanged("OnayTip");
            }
        }

        /// <summary> BIT (1) Allow Null </summary>
        public bool? Durum
        {
            get { return _Durum; }
            set
            {
                _Durum = value;
                OnPropertyChanged("Durum");
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

        List<string> WhereList = new List<string>();
        List<string> SetList = new List<string>();
        string info_FullTableName = "FINSAT6{0}.FINSAT6{0}.RiskTanim";
        string[] info_PrimaryKeys = { "pk_ID" };
        string[] info_IdentityKeys = { "ID" };

        List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public RiskTanim()
        {
            ChangedProperties = new List<string>();
            PropertyChanged += RiskTanim_PropertyChanged;
        }

        public void AcceptChanges() => ChangedProperties.Clear();

        void RiskTanim_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

    }
}