using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Data;

namespace KurumsalKaynakPlanlaması12M
{
     [DbTablo("FINSAT6", "FINSAT6", "DEP")]
    public class KKP_DEP : INotifyPropertyChanged
    {

        #region Fields
        private string _Depo;
        private string _DepoAdi;
        private string _Adres1;
        private string _Adres2;
        private string _Adres3;
        private string _Telefon1;
        private string _Telefon2;
        private string _Telefon3;
        private string _Fax;
        private string _BolgeKod;
        private string _GrupKod;
        private string _TipKod;
        private string _Kod1;
        private string _Kod2;
        private string _Kod3;
        private string _Yetkili1;
        private string _Yetkili2;
        private string _Yetkili1EMail;
        private string _Yetkili2EMail;
        private decimal _SonSayimSonuc;
        private short _MerkezDepo;
        private string _GuvenlikKod;
        private string _Kaydeden;
        private int _KayitTarih;
        private int _KayitSaat;
        private short _KayitKaynak;
        private string _KayitSurum;
        private string _Degistiren;
        private int _DegisTarih;
        private int _DegisSaat;
        private short _DegisKaynak;
        private string _DegisSurum;
        private short _CheckSum;
        private string _GirisMhsKod;
        private string _CikisMhsKod;
        private string _TuketimMhsKod;
        private short _DepoTipi;
        private int _Row_ID;

        #endregion Fields



        #region table Properties

        [DbAlan("Depo", SqlDbType.VarChar, 5, false, true, false)]
        public string Depo
        {
            get { return _Depo; }
            set
            {
                if (_Depo != value)
                {
                    OnPropertyChanged("Depo");
                    _Depo = value;
                }
            }
        }

        [DbAlan("DepoAdi", SqlDbType.VarChar, 40)]
        public string DepoAdi
        {
            get { return _DepoAdi; }
            set
            {
                if (_DepoAdi != value)
                {
                    OnPropertyChanged("DepoAdi");
                    _DepoAdi = value;
                }
            }
        }

        [DbAlan("Adres1", SqlDbType.VarChar, 30)]
        public string Adres1
        {
            get { return _Adres1; }
            set
            {
                if (_Adres1 != value)
                {
                    OnPropertyChanged("Adres1");
                    _Adres1 = value;
                }
            }
        }

        [DbAlan("Adres2", SqlDbType.VarChar, 30)]
        public string Adres2
        {
            get { return _Adres2; }
            set
            {
                if (_Adres2 != value)
                {
                    OnPropertyChanged("Adres2");
                    _Adres2 = value;
                }
            }
        }

        [DbAlan("Adres3", SqlDbType.VarChar, 30)]
        public string Adres3
        {
            get { return _Adres3; }
            set
            {
                if (_Adres3 != value)
                {
                    OnPropertyChanged("Adres3");
                    _Adres3 = value;
                }
            }
        }

        [DbAlan("Telefon1", SqlDbType.VarChar, 14)]
        public string Telefon1
        {
            get { return _Telefon1; }
            set
            {
                if (_Telefon1 != value)
                {
                    OnPropertyChanged("Telefon1");
                    _Telefon1 = value;
                }
            }
        }

        [DbAlan("Telefon2", SqlDbType.VarChar, 14)]
        public string Telefon2
        {
            get { return _Telefon2; }
            set
            {
                if (_Telefon2 != value)
                {
                    OnPropertyChanged("Telefon2");
                    _Telefon2 = value;
                }
            }
        }

        [DbAlan("Telefon3", SqlDbType.VarChar, 14)]
        public string Telefon3
        {
            get { return _Telefon3; }
            set
            {
                if (_Telefon3 != value)
                {
                    OnPropertyChanged("Telefon3");
                    _Telefon3 = value;
                }
            }
        }

        [DbAlan("Fax", SqlDbType.VarChar, 14)]
        public string Fax
        {
            get { return _Fax; }
            set
            {
                if (_Fax != value)
                {
                    OnPropertyChanged("Fax");
                    _Fax = value;
                }
            }
        }

        [DbAlan("BolgeKod", SqlDbType.VarChar, 4)]
        public string BolgeKod
        {
            get { return _BolgeKod; }
            set
            {
                if (_BolgeKod != value)
                {
                    OnPropertyChanged("BolgeKod");
                    _BolgeKod = value;
                }
            }
        }

        [DbAlan("GrupKod", SqlDbType.VarChar, 10)]
        public string GrupKod
        {
            get { return _GrupKod; }
            set
            {
                if (_GrupKod != value)
                {
                    OnPropertyChanged("GrupKod");
                    _GrupKod = value;
                }
            }
        }

        [DbAlan("TipKod", SqlDbType.VarChar, 10)]
        public string TipKod
        {
            get { return _TipKod; }
            set
            {
                if (_TipKod != value)
                {
                    OnPropertyChanged("TipKod");
                    _TipKod = value;
                }
            }
        }

        [DbAlan("Kod1", SqlDbType.VarChar, 10)]
        public string Kod1
        {
            get { return _Kod1; }
            set
            {
                if (_Kod1 != value)
                {
                    OnPropertyChanged("Kod1");
                    _Kod1 = value;
                }
            }
        }

        [DbAlan("Kod2", SqlDbType.VarChar, 10)]
        public string Kod2
        {
            get { return _Kod2; }
            set
            {
                if (_Kod2 != value)
                {
                    OnPropertyChanged("Kod2");
                    _Kod2 = value;
                }
            }
        }

        [DbAlan("Kod3", SqlDbType.VarChar, 10)]
        public string Kod3
        {
            get { return _Kod3; }
            set
            {
                if (_Kod3 != value)
                {
                    OnPropertyChanged("Kod3");
                    _Kod3 = value;
                }
            }
        }

        [DbAlan("Yetkili1", SqlDbType.VarChar, 30)]
        public string Yetkili1
        {
            get { return _Yetkili1; }
            set
            {
                if (_Yetkili1 != value)
                {
                    OnPropertyChanged("Yetkili1");
                    _Yetkili1 = value;
                }
            }
        }

        [DbAlan("Yetkili2", SqlDbType.VarChar, 30)]
        public string Yetkili2
        {
            get { return _Yetkili2; }
            set
            {
                if (_Yetkili2 != value)
                {
                    OnPropertyChanged("Yetkili2");
                    _Yetkili2 = value;
                }
            }
        }

        [DbAlan("Yetkili1EMail", SqlDbType.VarChar, 50)]
        public string Yetkili1EMail
        {
            get { return _Yetkili1EMail; }
            set
            {
                if (_Yetkili1EMail != value)
                {
                    OnPropertyChanged("Yetkili1EMail");
                    _Yetkili1EMail = value;
                }
            }
        }

        [DbAlan("Yetkili2EMail", SqlDbType.VarChar, 50)]
        public string Yetkili2EMail
        {
            get { return _Yetkili2EMail; }
            set
            {
                if (_Yetkili2EMail != value)
                {
                    OnPropertyChanged("Yetkili2EMail");
                    _Yetkili2EMail = value;
                }
            }
        }

        [DbAlan("SonSayimSonuc", SqlDbType.Decimal, 13)]
        public decimal SonSayimSonuc
        {
            get { return _SonSayimSonuc; }
            set
            {
                if (_SonSayimSonuc != value)
                {
                    OnPropertyChanged("SonSayimSonuc");
                    _SonSayimSonuc = value;
                }
            }
        }

        [DbAlan("MerkezDepo", SqlDbType.SmallInt, 2)]
        public short MerkezDepo
        {
            get { return _MerkezDepo; }
            set
            {
                if (_MerkezDepo != value)
                {
                    OnPropertyChanged("MerkezDepo");
                    _MerkezDepo = value;
                }
            }
        }

        [DbAlan("GuvenlikKod", SqlDbType.VarChar, 2)]
        public string GuvenlikKod
        {
            get { return _GuvenlikKod; }
            set
            {
                if (_GuvenlikKod != value)
                {
                    OnPropertyChanged("GuvenlikKod");
                    _GuvenlikKod = value;
                }
            }
        }

        [DbAlan("Kaydeden", SqlDbType.VarChar, 5)]
        public string Kaydeden
        {
            get { return _Kaydeden; }
            set
            {
                if (_Kaydeden != value)
                {
                    OnPropertyChanged("Kaydeden");
                    _Kaydeden = value;
                }
            }
        }

        [DbAlan("KayitTarih", SqlDbType.Int, 4)]
        public int KayitTarih
        {
            get { return _KayitTarih; }
            set
            {
                if (_KayitTarih != value)
                {
                    OnPropertyChanged("KayitTarih");
                    _KayitTarih = value;
                }
            }
        }

        [DbAlan("KayitSaat", SqlDbType.Int, 4)]
        public int KayitSaat
        {
            get { return _KayitSaat; }
            set
            {
                if (_KayitSaat != value)
                {
                    OnPropertyChanged("KayitSaat");
                    _KayitSaat = value;
                }
            }
        }

        [DbAlan("KayitKaynak", SqlDbType.SmallInt, 2)]
        public short KayitKaynak
        {
            get { return _KayitKaynak; }
            set
            {
                if (_KayitKaynak != value)
                {
                    OnPropertyChanged("KayitKaynak");
                    _KayitKaynak = value;
                }
            }
        }

        [DbAlan("KayitSurum", SqlDbType.VarChar, 12)]
        public string KayitSurum
        {
            get { return _KayitSurum; }
            set
            {
                if (_KayitSurum != value)
                {
                    OnPropertyChanged("KayitSurum");
                    _KayitSurum = value;
                }
            }
        }

        [DbAlan("Degistiren", SqlDbType.VarChar, 5)]
        public string Degistiren
        {
            get { return _Degistiren; }
            set
            {
                if (_Degistiren != value)
                {
                    OnPropertyChanged("Degistiren");
                    _Degistiren = value;
                }
            }
        }

        [DbAlan("DegisTarih", SqlDbType.Int, 4)]
        public int DegisTarih
        {
            get { return _DegisTarih; }
            set
            {
                if (_DegisTarih != value)
                {
                    OnPropertyChanged("DegisTarih");
                    _DegisTarih = value;
                }
            }
        }

        [DbAlan("DegisSaat", SqlDbType.Int, 4)]
        public int DegisSaat
        {
            get { return _DegisSaat; }
            set
            {
                if (_DegisSaat != value)
                {
                    OnPropertyChanged("DegisSaat");
                    _DegisSaat = value;
                }
            }
        }

        [DbAlan("DegisKaynak", SqlDbType.SmallInt, 2)]
        public short DegisKaynak
        {
            get { return _DegisKaynak; }
            set
            {
                if (_DegisKaynak != value)
                {
                    OnPropertyChanged("DegisKaynak");
                    _DegisKaynak = value;
                }
            }
        }

        [DbAlan("DegisSurum", SqlDbType.VarChar, 12)]
        public string DegisSurum
        {
            get { return _DegisSurum; }
            set
            {
                if (_DegisSurum != value)
                {
                    OnPropertyChanged("DegisSurum");
                    _DegisSurum = value;
                }
            }
        }

        [DbAlan("CheckSum", SqlDbType.SmallInt, 2)]
        public short CheckSum
        {
            get { return _CheckSum; }
            set
            {
                if (_CheckSum != value)
                {
                    OnPropertyChanged("CheckSum");
                    _CheckSum = value;
                }
            }
        }

        [DbAlan("GirisMhsKod", SqlDbType.VarChar, 20)]
        public string GirisMhsKod
        {
            get { return _GirisMhsKod; }
            set
            {
                if (_GirisMhsKod != value)
                {
                    OnPropertyChanged("GirisMhsKod");
                    _GirisMhsKod = value;
                }
            }
        }

        [DbAlan("CikisMhsKod", SqlDbType.VarChar, 20)]
        public string CikisMhsKod
        {
            get { return _CikisMhsKod; }
            set
            {
                if (_CikisMhsKod != value)
                {
                    OnPropertyChanged("CikisMhsKod");
                    _CikisMhsKod = value;
                }
            }
        }

        [DbAlan("TuketimMhsKod", SqlDbType.VarChar, 20)]
        public string TuketimMhsKod
        {
            get { return _TuketimMhsKod; }
            set
            {
                if (_TuketimMhsKod != value)
                {
                    OnPropertyChanged("TuketimMhsKod");
                    _TuketimMhsKod = value;
                }
            }
        }

        [DbAlan("DepoTipi", SqlDbType.SmallInt, 2)]
        public short DepoTipi
        {
            get { return _DepoTipi; }
            set
            {
                if (_DepoTipi != value)
                {
                    OnPropertyChanged("DepoTipi");
                    _DepoTipi = value;
                }
            }
        }

        [DbAlan("Row_ID", SqlDbType.Int, 4, true, false, false)]
        public int Row_ID
        {
            get { return _Row_ID; }
            set
            {
                if (_Row_ID != value)
                {
                    OnPropertyChanged("Row_ID");
                    _Row_ID = value;
                }
            }
        }

        #endregion table Properties



        #region property changes events functions

        public event PropertyChangedEventHandler PropertyChanged;
        private List<PropertyValue> _ChangedList = new List<PropertyValue>();
        public PropertyValue[] ChangedList { get { return _ChangedList.ToArray(); } }



        internal void DEP_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyValue propval = _ChangedList.Find(t => t.PropertiName == e.PropertyName);
            if (propval == null)
            {
                propval = new PropertyValue();
                propval.PropertiName = e.PropertyName;
                propval.OldValue = this.GetType().GetProperty(e.PropertyName).GetValue(this, null);
                _ChangedList.Add(propval);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public object GetOldValue(string propertyName)
        {
            PropertyValue val = _ChangedList.Find(t => t.PropertiName == propertyName);
            if (val == null)
                return null;
            return val.OldValue;
            //if (val == null)
            //{
            //    PropertyInfo info = this.GetType().GetProperty(propertyName);
            //    if (info == null)
            //        throw new Exception(string.Format("{0} alan adı bulunamadı!! (GetOldValue())", propertyName));
            //    return info.GetValue(this, null);
            //}
            //else
            //    return val.OldValue;
        }


        public void UpdateChanges()
        {
            _ChangedList.Clear();
        }

        #endregion property changes events functions


        public KKP_DEP()
        {
            ///this.PropertyChanged += this.DEP_PropertyChanged;
        }


    }
}
