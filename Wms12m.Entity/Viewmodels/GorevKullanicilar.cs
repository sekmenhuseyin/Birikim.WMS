using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
        #region Kullanici Class 

        #region KullaniciE Enum 
        public enum KullaniciE
        {
            ID,
            KulKodu,
            Pass,
            AdSoyad,
            YetkiKod,
            Email,
            Tel,
            Firma,
            Type,
            Kaydeden,
            KayitTarih,
            Degistiren,
            DegisTarih

        }
        #endregion /// KullaniciE Enum           

        public class Kullanici : INotifyPropertyChanged
        {
            #region Properties
            #region Fields  
            private int _ID;
            private string _KulKodu;
            private string _Pass;
            private string _AdSoyad;
            private short _YetkiKod;
            private string _Email;
            private string _Tel;
            private string _Firma;
            private short _Type;
            private string _Kaydeden;
            private DateTime? _KayitTarih;
            private string _Degistiren;
            private DateTime? _DegisTarih;
            private int _pk_ID;
            #endregion /// Fields


            /// <summary> INT (4) PrimaryKey IdentityKey * </summary>
            public int ID
            {
                get { return this._ID; }
            }

            /// <summary> VARCHAR (5) * </summary>
            public string KulKodu
            {
                get { return this._KulKodu; }
                set
                {
                    this._KulKodu = value;
                    OnPropertyChanged("KulKodu");
                }
            }

            /// <summary> VARCHAR (100) * </summary>
            public string Pass
            {
                get { return this._Pass; }
                set
                {
                    this._Pass = value;
                    OnPropertyChanged("Pass");
                }
            }

            /// <summary> VARCHAR (50) * </summary>
            public string AdSoyad
            {
                get { return this._AdSoyad; }
                set
                {
                    this._AdSoyad = value;
                    OnPropertyChanged("AdSoyad");
                }
            }

            /// <summary> SMALLINT (2) * </summary>
            public short YetkiKod
            {
                get { return this._YetkiKod; }
                set
                {
                    this._YetkiKod = value;
                    OnPropertyChanged("YetkiKod");
                }
            }

            /// <summary> VARCHAR (80) Allow Null </summary>
            public string Email
            {
                get { return this._Email; }
                set
                {
                    this._Email = value;
                    OnPropertyChanged("Email");
                }
            }

            /// <summary> VARCHAR (15) Allow Null </summary>
            public string Tel
            {
                get { return this._Tel; }
                set
                {
                    this._Tel = value;
                    OnPropertyChanged("Tel");
                }
            }

            /// <summary> VARCHAR (20) Allow Null </summary>
            public string Firma
            {
                get { return this._Firma; }
                set
                {
                    this._Firma = value;
                    OnPropertyChanged("Firma");
                }
            }

            /// <summary> SMALLINT (2) * </summary>
            public short Type
            {
                get { return this._Type; }
                set
                {
                    this._Type = value;
                    OnPropertyChanged("Type");
                }
            }

            /// <summary> VARCHAR (5) Allow Null </summary>
            public string Kaydeden
            {
                get { return this._Kaydeden; }
                set
                {
                    this._Kaydeden = value;
                    OnPropertyChanged("Kaydeden");
                }
            }

            /// <summary> SMALLDATETIME (4) Allow Null </summary>
            public DateTime? KayitTarih
            {
                get { return this._KayitTarih; }
                set
                {
                    this._KayitTarih = value;
                    OnPropertyChanged("KayitTarih");
                }
            }

            /// <summary> VARCHAR (5) Allow Null </summary>
            public string Degistiren
            {
                get { return this._Degistiren; }
                set
                {
                    this._Degistiren = value;
                    OnPropertyChanged("Degistiren");
                }
            }

            /// <summary> SMALLDATETIME (4) Allow Null </summary>
            public DateTime? DegisTarih
            {
                get { return this._DegisTarih; }
                set
                {
                    this._DegisTarih = value;
                    OnPropertyChanged("DegisTarih");
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
            private string info_FullTableName = "OnikimGorev.ong.Kullanici";
            private string[] info_PrimaryKeys = { "pk_ID" };
            private string[] info_IdentityKeys = { "ID" };

            private List<string> ChangedProperties = new List<string>();
            public event PropertyChangedEventHandler PropertyChanged;

            public Kullanici()
            {
                ChangedProperties = new List<string>();
                this.PropertyChanged += Kullanici_PropertyChanged;
            }

            public void AcceptChanges()
            {
                ChangedProperties.Clear();
            }

            void Kullanici_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
        #endregion /// Kullanici Class
}
