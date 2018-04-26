using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Wms12m.Entity
{
    public class Teminat : INotifyPropertyChanged
    {

        int _ID;
        string _HesapKodu;
        string _Unvan;
        string _AltBayi;
        string _Cins;
        decimal _Tutar;
        bool _SureliSuresiz;
        DateTime _Tarih;
        DateTime? _VadeTarih;
        bool _Onay;
        string _Onaylayan;
        DateTime? _OnayTarih;
        int _pk_ID;

        /// <summary> INT (4) PrimaryKey IdentityKey * </summary>
        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                OnPropertyChanged("ID");
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

        /// <summary> VARCHAR (100) * </summary>
        public string Unvan
        {
            get { return _Unvan; }
            set
            {
                _Unvan = value;
                OnPropertyChanged("Unvan");
            }
        }

        /// <summary> VARCHAR (-1) * </summary>
        public string AltBayi
        {
            get { return _AltBayi; }
            set
            {
                _AltBayi = value;
                OnPropertyChanged("AltBayi");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string Cins
        {
            get { return _Cins; }
            set
            {
                _Cins = value;
                OnPropertyChanged("Cins");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Tutar
        {
            get { return _Tutar; }
            set
            {
                _Tutar = value;
                OnPropertyChanged("Tutar");
            }
        }

        /// <summary> BIT (1) * </summary>
        public bool SureliSuresiz
        {
            get { return _SureliSuresiz; }
            set
            {
                _SureliSuresiz = value;
                OnPropertyChanged("SureliSuresiz");
            }
        }

        /// <summary> SMALLDATETIME (4) * </summary>
        public DateTime Tarih
        {
            get { return _Tarih; }
            set
            {
                _Tarih = value;
                OnPropertyChanged("Tarih");
            }
        }

        /// <summary> SMALLDATETIME (4) Allow Null </summary>
        public DateTime? VadeTarih
        {
            get { return _VadeTarih; }
            set
            {
                _VadeTarih = value;
                OnPropertyChanged("VadeTarih");
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

        /// <summary> VARCHAR (5) Allow Null </summary>
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
        public DateTime? OnayTarih
        {
            get { return _OnayTarih; }
            set
            {
                _OnayTarih = value;
                OnPropertyChanged("OnayTarih");
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
        string info_FullTableName = "FINSAT6{0}.FINSAT6{0}.Teminat";
        string[] info_PrimaryKeys = { "pk_ID" };
        string[] info_IdentityKeys = { "ID" };

        List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public Teminat()
        {
            ChangedProperties = new List<string>();
            PropertyChanged += Teminat_PropertyChanged;
        }

        public void AcceptChanges() => ChangedProperties.Clear();

        void Teminat_PropertyChanged(object sender, PropertyChangedEventArgs e)
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