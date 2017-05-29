using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Reflection;

namespace KurumsalKaynakPlanlaması12M
{
    /*Iskonto ve DovizTutar alanları Private Set olarak ayarlanadı.
     * 
     */

     [DbTablo("FINSAT6", "FINSAT6", "FTD")]
    public class KKP_FTD : INotifyPropertyChanged
    {


        #region Fields
        private short _IslemTip;
        private short _BA;
        private string _EvrakNo;
        private int _Tarih;
        private string _HesapKodu;
        private short _SiraNo;
        private short _SatirTip;
        private string _Aciklama;
        private string _IskontoTur;
        private double _IskontoOran;
        private decimal _Iskonto;
        private short _DvzTL;
        private string _DovizCinsi;
        private decimal _DovizTutar;
        private decimal _DovizKuru;
        private short _Printed;
        private string _FytListeNo;
        private string _TeslimChk;
        private short _NakliYekun;
        private short _DvzKurCins;
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
        private short _KynkEvrakTip;
        private short _AnaEvrakTip;
        private int _FormBaBsTarih;
        private string _IthalatDosyaNo;
        private string _TevkifatOran;
        private short _EFatTip;
        private short _EFatDurum;
        private int _EFatDonemBas;
        private int _EFatDonemBit;
        private short _EFatSure;
        private short _EFatSureDurum;
        private string _EFatDonemAck;
        private string _EFatNot;
        private string _EFatReferansNo;
        private string _GtkListeNo;
        private decimal _YuvFark;
        private decimal _DvzYuvFark;
        private string _KDVMuafAck;
        private string _EfatEtiketGB;
        private string _EfatEtiketPK;
        private string _AliciSiparisNo;
        private int _AliciSiparisTarih;
        private string _IhracatDosyaNo;
        private string _TevkifatOranKod;
        private string _OzelMatKDVKod;
        private short _EArsivTeslimSekli;
        private short _EArsivFaturaTipi;
        private short _EArsivFaturaDurum;
        private int _FaturaSaati;
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

        [DbAlan("BA", SqlDbType.SmallInt, 2, false, true, false)]
        public short BA
        {
            get { return _BA; }
            set
            {
                if (_BA != value)
                {
                    OnPropertyChanged("BA");
                    _BA = value;
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

        [DbAlan("Tarih", SqlDbType.Int, 4, false, true, false)]
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

        [DbAlan("HesapKodu", SqlDbType.VarChar, 20, false, true, false)]
        public string HesapKodu
        {
            get { return _HesapKodu; }
            set
            {
                if (_HesapKodu != value)
                {
                    OnPropertyChanged("HesapKodu");
                    _HesapKodu = value;
                }
            }
        }

        [DbAlan("SiraNo", SqlDbType.SmallInt, 2, false, true, false)]
        public short SiraNo
        {
            get { return _SiraNo; }
            set
            {
                if (_SiraNo != value)
                {
                    OnPropertyChanged("SiraNo");
                    _SiraNo = value;
                }
            }
        }

        [DbAlan("SatirTip", SqlDbType.SmallInt, 2)]
        public short SatirTip
        {
            get { return _SatirTip; }
            set
            {
                if (_SatirTip != value)
                {
                    OnPropertyChanged("SatirTip");
                    _SatirTip = value;
                }
            }
        }

        [DbAlan("Aciklama", SqlDbType.VarChar, 64)]
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

        [DbAlan("IskontoTur", SqlDbType.VarChar, 2)]
        public string IskontoTur
        {
            get { return _IskontoTur; }
            set
            {
                if (_IskontoTur != value)
                {
                    OnPropertyChanged("IskontoTur");
                    _IskontoTur = value;
                }
            }
        }

        [DbAlan("IskontoOran", SqlDbType.Real, 4)]
        public double IskontoOran
        {
            get { return _IskontoOran; }
            set
            {
                if (_IskontoOran != value)
                {
                    OnPropertyChanged("IskontoOran");
                    _IskontoOran = value;
                }
            }
        }

        [DbAlan("Iskonto", SqlDbType.Decimal, 13)]
        public decimal Iskonto
        {
            get { return _Iskonto; }
            set
            {
                if (_Iskonto != value)
                {
                    OnPropertyChanged("Iskonto");
                    _Iskonto = value;
                }
            }
        }

        [DbAlan("DvzTL", SqlDbType.SmallInt, 2)]
        public short DvzTL
        {
            get { return _DvzTL; }
            set
            {
                if (_DvzTL != value)
                {
                    OnPropertyChanged("DvzTL");
                    _DvzTL = value;
                }
            }
        }

        [DbAlan("DovizCinsi", SqlDbType.VarChar, 3)]
        public string DovizCinsi
        {
            get { return _DovizCinsi; }
            set
            {
                if (_DovizCinsi != value)
                {
                    OnPropertyChanged("DovizCinsi");
                    _DovizCinsi = value;
                }
            }
        }

        [DbAlan("DovizTutar", SqlDbType.Decimal, 13)]
        public decimal DovizTutar
        {
            get { return _DovizTutar; }
            set
            {
                if (_DovizTutar != value)
                {
                    OnPropertyChanged("DovizTutar");
                    _DovizTutar = value;
                }
            }
        }

        [DbAlan("DovizKuru", SqlDbType.Decimal, 13)]
        public decimal DovizKuru
        {
            get { return _DovizKuru; }
            set
            {
                if (_DovizKuru != value)
                {
                    OnPropertyChanged("DovizKuru");
                    _DovizKuru = value;
                }
            }
        }

        [DbAlan("Printed", SqlDbType.SmallInt, 2)]
        public short Printed
        {
            get { return _Printed; }
            set
            {
                if (_Printed != value)
                {
                    OnPropertyChanged("Printed");
                    _Printed = value;
                }
            }
        }

        [DbAlan("FytListeNo", SqlDbType.VarChar, 16)]
        public string FytListeNo
        {
            get { return _FytListeNo; }
            set
            {
                if (_FytListeNo != value)
                {
                    OnPropertyChanged("FytListeNo");
                    _FytListeNo = value;
                }
            }
        }

        [DbAlan("TeslimChk", SqlDbType.VarChar, 20)]
        public string TeslimChk
        {
            get { return _TeslimChk; }
            set
            {
                if (_TeslimChk != value)
                {
                    OnPropertyChanged("TeslimChk");
                    _TeslimChk = value;
                }
            }
        }

        [DbAlan("NakliYekun", SqlDbType.SmallInt, 2)]
        public short NakliYekun
        {
            get { return _NakliYekun; }
            set
            {
                if (_NakliYekun != value)
                {
                    OnPropertyChanged("NakliYekun");
                    _NakliYekun = value;
                }
            }
        }

        [DbAlan("DvzKurCins", SqlDbType.SmallInt, 2)]
        public short DvzKurCins
        {
            get { return _DvzKurCins; }
            set
            {
                if (_DvzKurCins != value)
                {
                    OnPropertyChanged("DvzKurCins");
                    _DvzKurCins = value;
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

        [DbAlan("KynkEvrakTip", SqlDbType.SmallInt, 2)]
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

        [DbAlan("AnaEvrakTip", SqlDbType.SmallInt, 2)]
        public short AnaEvrakTip
        {
            get { return _AnaEvrakTip; }
            set
            {
                if (_AnaEvrakTip != value)
                {
                    OnPropertyChanged("AnaEvrakTip");
                    _AnaEvrakTip = value;
                }
            }
        }

        [DbAlan("FormBaBsTarih", SqlDbType.Int, 4)]
        public int FormBaBsTarih
        {
            get { return _FormBaBsTarih; }
            set
            {
                if (_FormBaBsTarih != value)
                {
                    OnPropertyChanged("FormBaBsTarih");
                    _FormBaBsTarih = value;
                }
            }
        }

        [DbAlan("IthalatDosyaNo", SqlDbType.VarChar, 20)]
        public string IthalatDosyaNo
        {
            get { return _IthalatDosyaNo; }
            set
            {
                if (_IthalatDosyaNo != value)
                {
                    OnPropertyChanged("IthalatDosyaNo");
                    _IthalatDosyaNo = value;
                }
            }
        }

        [DbAlan("TevkifatOran", SqlDbType.VarChar, 7)]
        public string TevkifatOran
        {
            get { return _TevkifatOran; }
            set
            {
                if (_TevkifatOran != value)
                {
                    OnPropertyChanged("TevkifatOran");
                    _TevkifatOran = value;
                }
            }
        }

        [DbAlan("EFatTip", SqlDbType.SmallInt, 2)]
        public short EFatTip
        {
            get { return _EFatTip; }
            set
            {
                if (_EFatTip != value)
                {
                    OnPropertyChanged("EFatTip");
                    _EFatTip = value;
                }
            }
        }

        [DbAlan("EFatDurum", SqlDbType.SmallInt, 2)]
        public short EFatDurum
        {
            get { return _EFatDurum; }
            set
            {
                if (_EFatDurum != value)
                {
                    OnPropertyChanged("EFatDurum");
                    _EFatDurum = value;
                }
            }
        }

        [DbAlan("EFatDonemBas", SqlDbType.Int, 4)]
        public int EFatDonemBas
        {
            get { return _EFatDonemBas; }
            set
            {
                if (_EFatDonemBas != value)
                {
                    OnPropertyChanged("EFatDonemBas");
                    _EFatDonemBas = value;
                }
            }
        }

        [DbAlan("EFatDonemBit", SqlDbType.Int, 4)]
        public int EFatDonemBit
        {
            get { return _EFatDonemBit; }
            set
            {
                if (_EFatDonemBit != value)
                {
                    OnPropertyChanged("EFatDonemBit");
                    _EFatDonemBit = value;
                }
            }
        }

        [DbAlan("EFatSure", SqlDbType.SmallInt, 2)]
        public short EFatSure
        {
            get { return _EFatSure; }
            set
            {
                if (_EFatSure != value)
                {
                    OnPropertyChanged("EFatSure");
                    _EFatSure = value;
                }
            }
        }

        [DbAlan("EFatSureDurum", SqlDbType.SmallInt, 2)]
        public short EFatSureDurum
        {
            get { return _EFatSureDurum; }
            set
            {
                if (_EFatSureDurum != value)
                {
                    OnPropertyChanged("EFatSureDurum");
                    _EFatSureDurum = value;
                }
            }
        }

        [DbAlan("EFatDonemAck", SqlDbType.VarChar, 250)]
        public string EFatDonemAck
        {
            get { return _EFatDonemAck; }
            set
            {
                if (_EFatDonemAck != value)
                {
                    OnPropertyChanged("EFatDonemAck");
                    _EFatDonemAck = value;
                }
            }
        }

        [DbAlan("EFatNot", SqlDbType.VarChar, 256)]
        public string EFatNot
        {
            get { return _EFatNot; }
            set
            {
                if (_EFatNot != value)
                {
                    OnPropertyChanged("EFatNot");
                    _EFatNot = value;
                }
            }
        }

        [DbAlan("EFatReferansNo", SqlDbType.VarChar, 16)]
        public string EFatReferansNo
        {
            get { return _EFatReferansNo; }
            set
            {
                if (_EFatReferansNo != value)
                {
                    OnPropertyChanged("EFatReferansNo");
                    _EFatReferansNo = value;
                }
            }
        }

        [DbAlan("GtkListeNo", SqlDbType.VarChar, 2)]
        public string GtkListeNo
        {
            get { return _GtkListeNo; }
            set
            {
                if (_GtkListeNo != value)
                {
                    OnPropertyChanged("GtkListeNo");
                    _GtkListeNo = value;
                }
            }
        }

        [DbAlan("YuvFark", SqlDbType.Decimal, 13)]
        public decimal YuvFark
        {
            get { return _YuvFark; }
            set
            {
                if (_YuvFark != value)
                {
                    OnPropertyChanged("YuvFark");
                    _YuvFark = value;
                }
            }
        }

        [DbAlan("DvzYuvFark", SqlDbType.Decimal, 13)]
        public decimal DvzYuvFark
        {
            get { return _DvzYuvFark; }
            set
            {
                if (_DvzYuvFark != value)
                {
                    OnPropertyChanged("DvzYuvFark");
                    _DvzYuvFark = value;
                }
            }
        }

        [DbAlan("KDVMuafAck", SqlDbType.VarChar, 20)]
        public string KDVMuafAck
        {
            get { return _KDVMuafAck; }
            set
            {
                if (_KDVMuafAck != value)
                {
                    OnPropertyChanged("KDVMuafAck");
                    _KDVMuafAck = value;
                }
            }
        }

        [DbAlan("EfatEtiketGB", SqlDbType.VarChar, 128)]
        public string EfatEtiketGB
        {
            get { return _EfatEtiketGB; }
            set
            {
                if (_EfatEtiketGB != value)
                {
                    OnPropertyChanged("EfatEtiketGB");
                    _EfatEtiketGB = value;
                }
            }
        }

        [DbAlan("EfatEtiketPK", SqlDbType.VarChar, 128)]
        public string EfatEtiketPK
        {
            get { return _EfatEtiketPK; }
            set
            {
                if (_EfatEtiketPK != value)
                {
                    OnPropertyChanged("EfatEtiketPK");
                    _EfatEtiketPK = value;
                }
            }
        }

        [DbAlan("AliciSiparisNo", SqlDbType.VarChar, 32)]
        public string AliciSiparisNo
        {
            get { return _AliciSiparisNo; }
            set
            {
                if (_AliciSiparisNo != value)
                {
                    OnPropertyChanged("AliciSiparisNo");
                    _AliciSiparisNo = value;
                }
            }
        }

        [DbAlan("AliciSiparisTarih", SqlDbType.Int, 4)]
        public int AliciSiparisTarih
        {
            get { return _AliciSiparisTarih; }
            set
            {
                if (_AliciSiparisTarih != value)
                {
                    OnPropertyChanged("AliciSiparisTarih");
                    _AliciSiparisTarih = value;
                }
            }
        }

        [DbAlan("IhracatDosyaNo", SqlDbType.VarChar, 20)]
        public string IhracatDosyaNo
        {
            get { return _IhracatDosyaNo; }
            set
            {
                if (_IhracatDosyaNo != value)
                {
                    OnPropertyChanged("IhracatDosyaNo");
                    _IhracatDosyaNo = value;
                }
            }
        }

        [DbAlan("TevkifatOranKod", SqlDbType.VarChar, 7)]
        public string TevkifatOranKod
        {
            get { return _TevkifatOranKod; }
            set
            {
                if (_TevkifatOranKod != value)
                {
                    OnPropertyChanged("TevkifatOranKod");
                    _TevkifatOranKod = value;
                }
            }
        }

        [DbAlan("OzelMatKDVKod", SqlDbType.VarChar, 3)]
        public string OzelMatKDVKod
        {
            get { return _OzelMatKDVKod; }
            set
            {
                if (_OzelMatKDVKod != value)
                {
                    OnPropertyChanged("OzelMatKDVKod");
                    _OzelMatKDVKod = value;
                }
            }
        }

        [DbAlan("EArsivTeslimSekli", SqlDbType.SmallInt, 2)]
        public short EArsivTeslimSekli
        {
            get { return _EArsivTeslimSekli; }
            set
            {
                if (_EArsivTeslimSekli != value)
                {
                    OnPropertyChanged("EArsivTeslimSekli");
                    _EArsivTeslimSekli = value;
                }
            }
        }

        [DbAlan("EArsivFaturaTipi", SqlDbType.SmallInt, 2)]
        public short EArsivFaturaTipi
        {
            get { return _EArsivFaturaTipi; }
            set
            {
                if (_EArsivFaturaTipi != value)
                {
                    OnPropertyChanged("EArsivFaturaTipi");
                    _EArsivFaturaTipi = value;
                }
            }
        }

        [DbAlan("EArsivFaturaDurum", SqlDbType.SmallInt, 2)]
        public short EArsivFaturaDurum
        {
            get { return _EArsivFaturaDurum; }
            set
            {
                if (_EArsivFaturaDurum != value)
                {
                    OnPropertyChanged("EArsivFaturaDurum");
                    _EArsivFaturaDurum = value;
                }
            }
        }

        [DbAlan("FaturaSaati", SqlDbType.Int, 4)]
        public int FaturaSaati
        {
            get { return _FaturaSaati; }
            set
            {
                if (_FaturaSaati != value)
                {
                    OnPropertyChanged("FaturaSaati");
                    _FaturaSaati = value;
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



        internal void FTD_PropertyChanged(object sender, PropertyChangedEventArgs e)
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



        public KKP_FTD()
        {
            ///this.PropertyChanged += this.FTD_PropertyChanged;
        }
    }
}
