using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace KurumsalKaynakPlanlaması12M
{

    [DbTablo("FINSAT6", "FINSAT6", "MFK")]
    public class KKP_MFK : INotifyPropertyChanged
    {

        #region Fields
        private short _IslemTip;
        private string _EvrakNo;
        private int _EvrakTarih;
        private string _HesapKod;
        private short _KynkEvrakTip;
        private int _Tarih;
        private decimal _Tutar;
        private string _Aciklama;
        private string _Aciklama2;
        private string _Aciklama3;
        private string _Aciklama4;
        private string _Aciklama5;
        private string _Aciklama6;
        private string _Kod1;
        private string _Kod2;
        private string _Kod3;
        private string _Kod4;
        private string _Kod5;
        private decimal _Kod6;
        private decimal _Kod7;
        private string _Nesne1;
        private string _Nesne2;
        private string _Nesne3;
        private int _OnaySemaKod;
        private short _OnayIslemTip;
        private short _OnayStatus;
        private string _SonOnaylayan;
        private string _Onaylayan1;
        private string _Onaylayan2;
        private string _Onaylayacak;
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
        private string _Depo;
        private string _OnayTakipNo;
        private int _ROW_ID;

        #endregion Fields



        #region table Properties

        [DbAlan("IslemTip", SqlDbType.SmallInt, 2, false, true, false)]
        public short IslemTip
        {
            get { return _IslemTip; }
            set
            {
                if (_IslemTip != value)
                {
                    OnPropertyChanged("IslemTip");
                    _IslemTip = value;
                }
            }
        }

        [DbAlan("EvrakNo", SqlDbType.VarChar, 16, false, true, false)]
        public string EvrakNo
        {
            get { return _EvrakNo; }
            set
            {
                if (_EvrakNo != value)
                {
                    OnPropertyChanged("EvrakNo");
                    _EvrakNo = value;
                }
            }
        }

        [DbAlan("EvrakTarih", SqlDbType.Int, 4, false, true, false)]
        public int EvrakTarih
        {
            get { return _EvrakTarih; }
            set
            {
                if (_EvrakTarih != value)
                {
                    OnPropertyChanged("EvrakTarih");
                    _EvrakTarih = value;
                }
            }
        }

        [DbAlan("HesapKod", SqlDbType.VarChar, 20, false, true, false)]
        public string HesapKod
        {
            get { return _HesapKod; }
            set
            {
                if (_HesapKod != value)
                {
                    OnPropertyChanged("HesapKod");
                    _HesapKod = value;
                }
            }
        }

        [DbAlan("KynkEvrakTip", SqlDbType.SmallInt, 2, false, true, false)]
        public short KynkEvrakTip
        {
            get { return _KynkEvrakTip; }
            set
            {
                if (_KynkEvrakTip != value)
                {
                    OnPropertyChanged("KynkEvrakTip");
                    _KynkEvrakTip = value;
                }
            }
        }

        [DbAlan("Tarih", SqlDbType.Int, 4)]
        public int Tarih
        {
            get { return _Tarih; }
            set
            {
                if (_Tarih != value)
                {
                    OnPropertyChanged("Tarih");
                    _Tarih = value;
                }
            }
        }

        [DbAlan("Tutar", SqlDbType.Decimal, 13)]
        public decimal Tutar
        {
            get { return _Tutar; }
            set
            {
                if (_Tutar != value)
                {
                    OnPropertyChanged("Tutar");
                    _Tutar = value;
                }
            }
        }

        [DbAlan("Aciklama", SqlDbType.VarChar, 120)]
        public string Aciklama
        {
            get { return _Aciklama; }
            set
            {
                if (_Aciklama != value)
                {
                    OnPropertyChanged("Aciklama");
                    _Aciklama = value;
                }
            }
        }

        [DbAlan("Aciklama2", SqlDbType.VarChar, 120)]
        public string Aciklama2
        {
            get { return _Aciklama2; }
            set
            {
                if (_Aciklama2 != value)
                {
                    OnPropertyChanged("Aciklama2");
                    _Aciklama2 = value;
                }
            }
        }

        [DbAlan("Aciklama3", SqlDbType.VarChar, 120)]
        public string Aciklama3
        {
            get { return _Aciklama3; }
            set
            {
                if (_Aciklama3 != value)
                {
                    OnPropertyChanged("Aciklama3");
                    _Aciklama3 = value;
                }
            }
        }

        [DbAlan("Aciklama4", SqlDbType.VarChar, 120)]
        public string Aciklama4
        {
            get { return _Aciklama4; }
            set
            {
                if (_Aciklama4 != value)
                {
                    OnPropertyChanged("Aciklama4");
                    _Aciklama4 = value;
                }
            }
        }

        [DbAlan("Aciklama5", SqlDbType.VarChar, 120)]
        public string Aciklama5
        {
            get { return _Aciklama5; }
            set
            {
                if (_Aciklama5 != value)
                {
                    OnPropertyChanged("Aciklama5");
                    _Aciklama5 = value;
                }
            }
        }

        [DbAlan("Aciklama6", SqlDbType.VarChar, 120)]
        public string Aciklama6
        {
            get { return _Aciklama6; }
            set
            {
                if (_Aciklama6 != value)
                {
                    OnPropertyChanged("Aciklama6");
                    _Aciklama6 = value;
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

        [DbAlan("Kod4", SqlDbType.VarChar, 10)]
        public string Kod4
        {
            get { return _Kod4; }
            set
            {
                if (_Kod4 != value)
                {
                    OnPropertyChanged("Kod4");
                    _Kod4 = value;
                }
            }
        }

        [DbAlan("Kod5", SqlDbType.VarChar, 20)]
        public string Kod5
        {
            get { return _Kod5; }
            set
            {
                if (_Kod5 != value)
                {
                    OnPropertyChanged("Kod5");
                    _Kod5 = value;
                }
            }
        }

        [DbAlan("Kod6", SqlDbType.Decimal, 13)]
        public decimal Kod6
        {
            get { return _Kod6; }
            set
            {
                if (_Kod6 != value)
                {
                    OnPropertyChanged("Kod6");
                    _Kod6 = value;
                }
            }
        }

        [DbAlan("Kod7", SqlDbType.Decimal, 13)]
        public decimal Kod7
        {
            get { return _Kod7; }
            set
            {
                if (_Kod7 != value)
                {
                    OnPropertyChanged("Kod7");
                    _Kod7 = value;
                }
            }
        }

        [DbAlan("Nesne1", SqlDbType.VarChar, 254)]
        public string Nesne1
        {
            get { return _Nesne1; }
            set
            {
                if (_Nesne1 != value)
                {
                    OnPropertyChanged("Nesne1");
                    _Nesne1 = value;
                }
            }
        }

        [DbAlan("Nesne2", SqlDbType.VarChar, 254)]
        public string Nesne2
        {
            get { return _Nesne2; }
            set
            {
                if (_Nesne2 != value)
                {
                    OnPropertyChanged("Nesne2");
                    _Nesne2 = value;
                }
            }
        }

        [DbAlan("Nesne3", SqlDbType.VarChar, 254)]
        public string Nesne3
        {
            get { return _Nesne3; }
            set
            {
                if (_Nesne3 != value)
                {
                    OnPropertyChanged("Nesne3");
                    _Nesne3 = value;
                }
            }
        }

        [DbAlan("OnaySemaKod", SqlDbType.Int, 4)]
        public int OnaySemaKod
        {
            get { return _OnaySemaKod; }
            set
            {
                if (_OnaySemaKod != value)
                {
                    OnPropertyChanged("OnaySemaKod");
                    _OnaySemaKod = value;
                }
            }
        }

        [DbAlan("OnayIslemTip", SqlDbType.SmallInt, 2)]
        public short OnayIslemTip
        {
            get { return _OnayIslemTip; }
            set
            {
                if (_OnayIslemTip != value)
                {
                    OnPropertyChanged("OnayIslemTip");
                    _OnayIslemTip = value;
                }
            }
        }

        [DbAlan("OnayStatus", SqlDbType.SmallInt, 2)]
        public short OnayStatus
        {
            get { return _OnayStatus; }
            set
            {
                if (_OnayStatus != value)
                {
                    OnPropertyChanged("OnayStatus");
                    _OnayStatus = value;
                }
            }
        }

        [DbAlan("SonOnaylayan", SqlDbType.VarChar, 5)]
        public string SonOnaylayan
        {
            get { return _SonOnaylayan; }
            set
            {
                if (_SonOnaylayan != value)
                {
                    OnPropertyChanged("SonOnaylayan");
                    _SonOnaylayan = value;
                }
            }
        }

        [DbAlan("Onaylayan1", SqlDbType.VarChar, 5)]
        public string Onaylayan1
        {
            get { return _Onaylayan1; }
            set
            {
                if (_Onaylayan1 != value)
                {
                    OnPropertyChanged("Onaylayan1");
                    _Onaylayan1 = value;
                }
            }
        }

        [DbAlan("Onaylayan2", SqlDbType.VarChar, 5)]
        public string Onaylayan2
        {
            get { return _Onaylayan2; }
            set
            {
                if (_Onaylayan2 != value)
                {
                    OnPropertyChanged("Onaylayan2");
                    _Onaylayan2 = value;
                }
            }
        }

        [DbAlan("Onaylayacak", SqlDbType.VarChar, 5)]
        public string Onaylayacak
        {
            get { return _Onaylayacak; }
            set
            {
                if (_Onaylayacak != value)
                {
                    OnPropertyChanged("Onaylayacak");
                    _Onaylayacak = value;
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

        [DbAlan("Depo", SqlDbType.VarChar, 5)]
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

        [DbAlan("OnayTakipNo", SqlDbType.VarChar, 16)]
        public string OnayTakipNo
        {
            get { return _OnayTakipNo; }
            set
            {
                if (_OnayTakipNo != value)
                {
                    OnPropertyChanged("OnayTakipNo");
                    _OnayTakipNo = value;
                }
            }
        }

        [DbAlan("ROW_ID", SqlDbType.Int, 4, true, false, false)]
        public int ROW_ID
        {
            get { return _ROW_ID; }
            set
            {
                if (_ROW_ID != value)
                {
                    OnPropertyChanged("ROW_ID");
                    _ROW_ID = value;
                }
            }
        }

        #endregion table Properties



        #region property changes events functions

        public event PropertyChangedEventHandler PropertyChanged;
        private List<PropertyValue> _ChangedList = new List<PropertyValue>();
        public PropertyValue[] ChangedList { get { return _ChangedList.ToArray(); } }



        internal void MFK_PropertyChanged(object sender, PropertyChangedEventArgs e)
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




        public KKP_MFK()
        {
            ///this.PropertyChanged += this.MFK_PropertyChanged;
        }

        public KKP_MFK(KKPKynkEvrakTip kynkEvrkaTip)
        {
            Data.DefaultValueSetMFK(this, kynkEvrkaTip);
            ///this.PropertyChanged += this.MFK_PropertyChanged;
        }

    }
}
