using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Data;

namespace KurumsalKaynakPlanlaması12M
{
    public class KKPYNS_STK002 : INotifyPropertyChanged
    {

        #region Fields
        private int _STK002_Row_ID;
        private string _STK002_MalKodu;
        private Nullable<int> _STK002_IslemTarihi;
        private Nullable<byte> _STK002_GC;
        private string _STK002_CariHesapKodu;
        private string _STK002_EvrakSeriNo;
        private Nullable<decimal> _STK002_Miktari;
        private Nullable<decimal> _STK002_BirimFiyati;
        private Nullable<decimal> _STK002_Tutari;
        private Nullable<decimal> _STK002_Iskonto;
        private Nullable<decimal> _STK002_KDVTutari;
        private Nullable<short> _STK002_IslemTipi;
        private string _STK002_Kod1;
        private string _STK002_Kod2;
        private string _STK002_IrsaliyeNo;
        private Nullable<decimal> _STK002_TeslimMiktari;
        private Nullable<byte> _STK002_SipDurumu;
        private string _STK002_Bos;
        private Nullable<byte> _STK002_KDVDurumu;
        private Nullable<int> _STK002_TeslimTarihi;
        private Nullable<byte> _STK002_ParaBirimi;
        private Nullable<int> _STK002_SEQNo;
        private string _STK002_GirenKaynak;
        private Nullable<int> _STK002_GirenTarih;
        private string _STK002_GirenSaat;
        private string _STK002_GirenKodu;
        private string _STK002_GirenSurum;
        private string _STK002_DegistirenKaynak;
        private Nullable<int> _STK002_DegistirenTarih;
        private string _STK002_DegistirenSaat;
        private string _STK002_DegistirenKodu;
        private string _STK002_DegistirenSurum;
        private Nullable<byte> _STK002_IptalDurumu;
        private Nullable<int> _STK002_AsilEvrakTarihi;
        private Nullable<decimal> _STK002_Miktar2;
        private Nullable<decimal> _STK002_Tutar2;
        private Nullable<decimal> _STK002_KalemIskontoOrani1;
        private Nullable<decimal> _STK002_KalemIskontoOrani2;
        private Nullable<decimal> _STK002_KalemIskontoOrani3;
        private Nullable<decimal> _STK002_KalemIskontoOrani4;
        private Nullable<decimal> _STK002_KalemIskontoOrani5;
        private Nullable<decimal> _STK002_KalemIskontoTutari1;
        private Nullable<decimal> _STK002_KalemIskontoTutari2;
        private Nullable<decimal> _STK002_KalemIskontoTutari3;
        private Nullable<decimal> _STK002_KalemIskontoTutari4;
        private Nullable<decimal> _STK002_KalemIskontoTutari5;
        private string _STK002_DovizCinsi;
        private Nullable<decimal> _STK002_DovizTutari;
        private Nullable<decimal> _STK002_DovizKuru;
        private string _STK002_Aciklama1;
        private string _STK002_Aciklama2;
        private string _STK002_Depo;
        private string _STK002_Kod3;
        private string _STK002_Kod4;
        private string _STK002_Kod5;
        private string _STK002_Kod6;
        private string _STK002_Kod7;
        private string _STK002_Kod8;
        private string _STK002_Kod9;
        private string _STK002_Kod10;
        private Nullable<decimal> _STK002_Kod11;
        private Nullable<decimal> _STK002_Kod12;
        private string _STK002_Vasita;
        private string _STK002_MalSeriNo;
        private Nullable<int> _STK002_VadeTarihi;
        private Nullable<decimal> _STK002_Masraf;
        private string _STK002_EvrakSeriNo2;
        private Nullable<int> _STK002_Tarih2;
        private Nullable<byte> _STK002_Kod9Flag;
        private Nullable<byte> _STK002_Kod10Flag;
        private Nullable<byte> _STK002_KDVOranFlag;
        private string _STK002_TeslimCHKodu;
        private string _STK002_MasrafMerkezi;
        private Nullable<int> _STK002_SonSevkTarihi;
        private string _STK002_EFaturaOTVListeNo;
        private string _STK002_KDVMuafAciklama;
        private Nullable<decimal> _STK002_DovizBirimFiyati;
        private string _STK002_Not1;
        private string _STK002_Not2;
        private string _STK002_Not3;
        private string _STK002_Not4;
        private string _STK002_Not5;

        #endregion Fields



        #region table Properties

        [DbAlan("STK002_Row_ID", SqlDbType.Int, 4, true, true, false)]
        public int STK002_Row_ID
        {
            get { return _STK002_Row_ID; }
            set
            {
                if (_STK002_Row_ID != value)
                {
                    OnPropertyChanged("STK002_Row_ID");
                    _STK002_Row_ID = value;
                }
            }
        }

        [DbAlan("STK002_MalKodu", SqlDbType.NVarChar, 60)]
        public string STK002_MalKodu
        {
            get { return _STK002_MalKodu; }
            set
            {
                if (_STK002_MalKodu != value)
                {
                    OnPropertyChanged("STK002_MalKodu");
                    _STK002_MalKodu = value;
                }
            }
        }

        [DbAlan("STK002_IslemTarihi", SqlDbType.Int, 4)]
        public Nullable<int> STK002_IslemTarihi
        {
            get { return _STK002_IslemTarihi; }
            set
            {
                if (_STK002_IslemTarihi != value)
                {
                    OnPropertyChanged("STK002_IslemTarihi");
                    _STK002_IslemTarihi = value;
                }
            }
        }

        [DbAlan("STK002_GC", SqlDbType.TinyInt, 1)]
        public Nullable<byte> STK002_GC
        {
            get { return _STK002_GC; }
            set
            {
                if (_STK002_GC != value)
                {
                    OnPropertyChanged("STK002_GC");
                    _STK002_GC = value;
                }
            }
        }

        [DbAlan("STK002_CariHesapKodu", SqlDbType.NVarChar, 32)]
        public string STK002_CariHesapKodu
        {
            get { return _STK002_CariHesapKodu; }
            set
            {
                if (_STK002_CariHesapKodu != value)
                {
                    OnPropertyChanged("STK002_CariHesapKodu");
                    _STK002_CariHesapKodu = value;
                }
            }
        }

        [DbAlan("STK002_EvrakSeriNo", SqlDbType.NVarChar, 32)]
        public string STK002_EvrakSeriNo
        {
            get { return _STK002_EvrakSeriNo; }
            set
            {
                if (_STK002_EvrakSeriNo != value)
                {
                    OnPropertyChanged("STK002_EvrakSeriNo");
                    _STK002_EvrakSeriNo = value;
                }
            }
        }

        [DbAlan("STK002_Miktari", SqlDbType.Decimal, 9)]
        public Nullable<decimal> STK002_Miktari
        {
            get { return _STK002_Miktari; }
            set
            {
                if (_STK002_Miktari != value)
                {
                    OnPropertyChanged("STK002_Miktari");
                    _STK002_Miktari = value;
                }
            }
        }

        [DbAlan("STK002_BirimFiyati", SqlDbType.Decimal, 13)]
        public Nullable<decimal> STK002_BirimFiyati
        {
            get { return _STK002_BirimFiyati; }
            set
            {
                if (_STK002_BirimFiyati != value)
                {
                    OnPropertyChanged("STK002_BirimFiyati");
                    _STK002_BirimFiyati = value;
                }
            }
        }

        [DbAlan("STK002_Tutari", SqlDbType.Decimal, 9)]
        public Nullable<decimal> STK002_Tutari
        {
            get { return _STK002_Tutari; }
            set
            {
                if (_STK002_Tutari != value)
                {
                    OnPropertyChanged("STK002_Tutari");
                    _STK002_Tutari = value;
                }
            }
        }

        [DbAlan("STK002_Iskonto", SqlDbType.Decimal, 9)]
        public Nullable<decimal> STK002_Iskonto
        {
            get { return _STK002_Iskonto; }
            set
            {
                if (_STK002_Iskonto != value)
                {
                    OnPropertyChanged("STK002_Iskonto");
                    _STK002_Iskonto = value;
                }
            }
        }

        [DbAlan("STK002_KDVTutari", SqlDbType.Decimal, 9)]
        public Nullable<decimal> STK002_KDVTutari
        {
            get { return _STK002_KDVTutari; }
            set
            {
                if (_STK002_KDVTutari != value)
                {
                    OnPropertyChanged("STK002_KDVTutari");
                    _STK002_KDVTutari = value;
                }
            }
        }

        [DbAlan("STK002_IslemTipi", SqlDbType.SmallInt, 2)]
        public Nullable<short> STK002_IslemTipi
        {
            get { return _STK002_IslemTipi; }
            set
            {
                if (_STK002_IslemTipi != value)
                {
                    OnPropertyChanged("STK002_IslemTipi");
                    _STK002_IslemTipi = value;
                }
            }
        }

        [DbAlan("STK002_Kod1", SqlDbType.NVarChar, 40)]
        public string STK002_Kod1
        {
            get { return _STK002_Kod1; }
            set
            {
                if (_STK002_Kod1 != value)
                {
                    OnPropertyChanged("STK002_Kod1");
                    _STK002_Kod1 = value;
                }
            }
        }

        [DbAlan("STK002_Kod2", SqlDbType.NVarChar, 40)]
        public string STK002_Kod2
        {
            get { return _STK002_Kod2; }
            set
            {
                if (_STK002_Kod2 != value)
                {
                    OnPropertyChanged("STK002_Kod2");
                    _STK002_Kod2 = value;
                }
            }
        }

        [DbAlan("STK002_IrsaliyeNo", SqlDbType.NVarChar, 32)]
        public string STK002_IrsaliyeNo
        {
            get { return _STK002_IrsaliyeNo; }
            set
            {
                if (_STK002_IrsaliyeNo != value)
                {
                    OnPropertyChanged("STK002_IrsaliyeNo");
                    _STK002_IrsaliyeNo = value;
                }
            }
        }

        [DbAlan("STK002_TeslimMiktari", SqlDbType.Decimal, 9)]
        public Nullable<decimal> STK002_TeslimMiktari
        {
            get { return _STK002_TeslimMiktari; }
            set
            {
                if (_STK002_TeslimMiktari != value)
                {
                    OnPropertyChanged("STK002_TeslimMiktari");
                    _STK002_TeslimMiktari = value;
                }
            }
        }

        [DbAlan("STK002_SipDurumu", SqlDbType.TinyInt, 1)]
        public Nullable<byte> STK002_SipDurumu
        {
            get { return _STK002_SipDurumu; }
            set
            {
                if (_STK002_SipDurumu != value)
                {
                    OnPropertyChanged("STK002_SipDurumu");
                    _STK002_SipDurumu = value;
                }
            }
        }

        [DbAlan("STK002_Bos", SqlDbType.NVarChar, 2)]
        public string STK002_Bos
        {
            get { return _STK002_Bos; }
            set
            {
                if (_STK002_Bos != value)
                {
                    OnPropertyChanged("STK002_Bos");
                    _STK002_Bos = value;
                }
            }
        }

        [DbAlan("STK002_KDVDurumu", SqlDbType.TinyInt, 1)]
        public Nullable<byte> STK002_KDVDurumu
        {
            get { return _STK002_KDVDurumu; }
            set
            {
                if (_STK002_KDVDurumu != value)
                {
                    OnPropertyChanged("STK002_KDVDurumu");
                    _STK002_KDVDurumu = value;
                }
            }
        }

        [DbAlan("STK002_TeslimTarihi", SqlDbType.Int, 4)]
        public Nullable<int> STK002_TeslimTarihi
        {
            get { return _STK002_TeslimTarihi; }
            set
            {
                if (_STK002_TeslimTarihi != value)
                {
                    OnPropertyChanged("STK002_TeslimTarihi");
                    _STK002_TeslimTarihi = value;
                }
            }
        }

        [DbAlan("STK002_ParaBirimi", SqlDbType.TinyInt, 1)]
        public Nullable<byte> STK002_ParaBirimi
        {
            get { return _STK002_ParaBirimi; }
            set
            {
                if (_STK002_ParaBirimi != value)
                {
                    OnPropertyChanged("STK002_ParaBirimi");
                    _STK002_ParaBirimi = value;
                }
            }
        }

        [DbAlan("STK002_SEQNo", SqlDbType.Int, 4)]
        public Nullable<int> STK002_SEQNo
        {
            get { return _STK002_SEQNo; }
            set
            {
                if (_STK002_SEQNo != value)
                {
                    OnPropertyChanged("STK002_SEQNo");
                    _STK002_SEQNo = value;
                }
            }
        }

        [DbAlan("STK002_GirenKaynak", SqlDbType.NVarChar, 10)]
        public string STK002_GirenKaynak
        {
            get { return _STK002_GirenKaynak; }
            set
            {
                if (_STK002_GirenKaynak != value)
                {
                    OnPropertyChanged("STK002_GirenKaynak");
                    _STK002_GirenKaynak = value;
                }
            }
        }

        [DbAlan("STK002_GirenTarih", SqlDbType.Int, 4)]
        public Nullable<int> STK002_GirenTarih
        {
            get { return _STK002_GirenTarih; }
            set
            {
                if (_STK002_GirenTarih != value)
                {
                    OnPropertyChanged("STK002_GirenTarih");
                    _STK002_GirenTarih = value;
                }
            }
        }

        [DbAlan("STK002_GirenSaat", SqlDbType.NVarChar, 16)]
        public string STK002_GirenSaat
        {
            get { return _STK002_GirenSaat; }
            set
            {
                if (_STK002_GirenSaat != value)
                {
                    OnPropertyChanged("STK002_GirenSaat");
                    _STK002_GirenSaat = value;
                }
            }
        }

        [DbAlan("STK002_GirenKodu", SqlDbType.NVarChar, 20)]
        public string STK002_GirenKodu
        {
            get { return _STK002_GirenKodu; }
            set
            {
                if (_STK002_GirenKodu != value)
                {
                    OnPropertyChanged("STK002_GirenKodu");
                    _STK002_GirenKodu = value;
                }
            }
        }

        [DbAlan("STK002_GirenSurum", SqlDbType.NVarChar, 16)]
        public string STK002_GirenSurum
        {
            get { return _STK002_GirenSurum; }
            set
            {
                if (_STK002_GirenSurum != value)
                {
                    OnPropertyChanged("STK002_GirenSurum");
                    _STK002_GirenSurum = value;
                }
            }
        }

        [DbAlan("STK002_DegistirenKaynak", SqlDbType.NVarChar, 10)]
        public string STK002_DegistirenKaynak
        {
            get { return _STK002_DegistirenKaynak; }
            set
            {
                if (_STK002_DegistirenKaynak != value)
                {
                    OnPropertyChanged("STK002_DegistirenKaynak");
                    _STK002_DegistirenKaynak = value;
                }
            }
        }

        [DbAlan("STK002_DegistirenTarih", SqlDbType.Int, 4)]
        public Nullable<int> STK002_DegistirenTarih
        {
            get { return _STK002_DegistirenTarih; }
            set
            {
                if (_STK002_DegistirenTarih != value)
                {
                    OnPropertyChanged("STK002_DegistirenTarih");
                    _STK002_DegistirenTarih = value;
                }
            }
        }

        [DbAlan("STK002_DegistirenSaat", SqlDbType.NVarChar, 16)]
        public string STK002_DegistirenSaat
        {
            get { return _STK002_DegistirenSaat; }
            set
            {
                if (_STK002_DegistirenSaat != value)
                {
                    OnPropertyChanged("STK002_DegistirenSaat");
                    _STK002_DegistirenSaat = value;
                }
            }
        }

        [DbAlan("STK002_DegistirenKodu", SqlDbType.NVarChar, 20)]
        public string STK002_DegistirenKodu
        {
            get { return _STK002_DegistirenKodu; }
            set
            {
                if (_STK002_DegistirenKodu != value)
                {
                    OnPropertyChanged("STK002_DegistirenKodu");
                    _STK002_DegistirenKodu = value;
                }
            }
        }

        [DbAlan("STK002_DegistirenSurum", SqlDbType.NVarChar, 16)]
        public string STK002_DegistirenSurum
        {
            get { return _STK002_DegistirenSurum; }
            set
            {
                if (_STK002_DegistirenSurum != value)
                {
                    OnPropertyChanged("STK002_DegistirenSurum");
                    _STK002_DegistirenSurum = value;
                }
            }
        }

        [DbAlan("STK002_IptalDurumu", SqlDbType.TinyInt, 1)]
        public Nullable<byte> STK002_IptalDurumu
        {
            get { return _STK002_IptalDurumu; }
            set
            {
                if (_STK002_IptalDurumu != value)
                {
                    OnPropertyChanged("STK002_IptalDurumu");
                    _STK002_IptalDurumu = value;
                }
            }
        }

        [DbAlan("STK002_AsilEvrakTarihi", SqlDbType.Int, 4)]
        public Nullable<int> STK002_AsilEvrakTarihi
        {
            get { return _STK002_AsilEvrakTarihi; }
            set
            {
                if (_STK002_AsilEvrakTarihi != value)
                {
                    OnPropertyChanged("STK002_AsilEvrakTarihi");
                    _STK002_AsilEvrakTarihi = value;
                }
            }
        }

        [DbAlan("STK002_Miktar2", SqlDbType.Decimal, 9)]
        public Nullable<decimal> STK002_Miktar2
        {
            get { return _STK002_Miktar2; }
            set
            {
                if (_STK002_Miktar2 != value)
                {
                    OnPropertyChanged("STK002_Miktar2");
                    _STK002_Miktar2 = value;
                }
            }
        }

        [DbAlan("STK002_Tutar2", SqlDbType.Decimal, 9)]
        public Nullable<decimal> STK002_Tutar2
        {
            get { return _STK002_Tutar2; }
            set
            {
                if (_STK002_Tutar2 != value)
                {
                    OnPropertyChanged("STK002_Tutar2");
                    _STK002_Tutar2 = value;
                }
            }
        }

        [DbAlan("STK002_KalemIskontoOrani1", SqlDbType.Decimal, 5)]
        public Nullable<decimal> STK002_KalemIskontoOrani1
        {
            get { return _STK002_KalemIskontoOrani1; }
            set
            {
                if (_STK002_KalemIskontoOrani1 != value)
                {
                    OnPropertyChanged("STK002_KalemIskontoOrani1");
                    _STK002_KalemIskontoOrani1 = value;
                }
            }
        }

        [DbAlan("STK002_KalemIskontoOrani2", SqlDbType.Decimal, 5)]
        public Nullable<decimal> STK002_KalemIskontoOrani2
        {
            get { return _STK002_KalemIskontoOrani2; }
            set
            {
                if (_STK002_KalemIskontoOrani2 != value)
                {
                    OnPropertyChanged("STK002_KalemIskontoOrani2");
                    _STK002_KalemIskontoOrani2 = value;
                }
            }
        }

        [DbAlan("STK002_KalemIskontoOrani3", SqlDbType.Decimal, 5)]
        public Nullable<decimal> STK002_KalemIskontoOrani3
        {
            get { return _STK002_KalemIskontoOrani3; }
            set
            {
                if (_STK002_KalemIskontoOrani3 != value)
                {
                    OnPropertyChanged("STK002_KalemIskontoOrani3");
                    _STK002_KalemIskontoOrani3 = value;
                }
            }
        }

        [DbAlan("STK002_KalemIskontoOrani4", SqlDbType.Decimal, 5)]
        public Nullable<decimal> STK002_KalemIskontoOrani4
        {
            get { return _STK002_KalemIskontoOrani4; }
            set
            {
                if (_STK002_KalemIskontoOrani4 != value)
                {
                    OnPropertyChanged("STK002_KalemIskontoOrani4");
                    _STK002_KalemIskontoOrani4 = value;
                }
            }
        }

        [DbAlan("STK002_KalemIskontoOrani5", SqlDbType.Decimal, 5)]
        public Nullable<decimal> STK002_KalemIskontoOrani5
        {
            get { return _STK002_KalemIskontoOrani5; }
            set
            {
                if (_STK002_KalemIskontoOrani5 != value)
                {
                    OnPropertyChanged("STK002_KalemIskontoOrani5");
                    _STK002_KalemIskontoOrani5 = value;
                }
            }
        }

        [DbAlan("STK002_KalemIskontoTutari1", SqlDbType.Decimal, 9)]
        public Nullable<decimal> STK002_KalemIskontoTutari1
        {
            get { return _STK002_KalemIskontoTutari1; }
            set
            {
                if (_STK002_KalemIskontoTutari1 != value)
                {
                    OnPropertyChanged("STK002_KalemIskontoTutari1");
                    _STK002_KalemIskontoTutari1 = value;
                }
            }
        }

        [DbAlan("STK002_KalemIskontoTutari2", SqlDbType.Decimal, 9)]
        public Nullable<decimal> STK002_KalemIskontoTutari2
        {
            get { return _STK002_KalemIskontoTutari2; }
            set
            {
                if (_STK002_KalemIskontoTutari2 != value)
                {
                    OnPropertyChanged("STK002_KalemIskontoTutari2");
                    _STK002_KalemIskontoTutari2 = value;
                }
            }
        }

        [DbAlan("STK002_KalemIskontoTutari3", SqlDbType.Decimal, 9)]
        public Nullable<decimal> STK002_KalemIskontoTutari3
        {
            get { return _STK002_KalemIskontoTutari3; }
            set
            {
                if (_STK002_KalemIskontoTutari3 != value)
                {
                    OnPropertyChanged("STK002_KalemIskontoTutari3");
                    _STK002_KalemIskontoTutari3 = value;
                }
            }
        }

        [DbAlan("STK002_KalemIskontoTutari4", SqlDbType.Decimal, 9)]
        public Nullable<decimal> STK002_KalemIskontoTutari4
        {
            get { return _STK002_KalemIskontoTutari4; }
            set
            {
                if (_STK002_KalemIskontoTutari4 != value)
                {
                    OnPropertyChanged("STK002_KalemIskontoTutari4");
                    _STK002_KalemIskontoTutari4 = value;
                }
            }
        }

        [DbAlan("STK002_KalemIskontoTutari5", SqlDbType.Decimal, 9)]
        public Nullable<decimal> STK002_KalemIskontoTutari5
        {
            get { return _STK002_KalemIskontoTutari5; }
            set
            {
                if (_STK002_KalemIskontoTutari5 != value)
                {
                    OnPropertyChanged("STK002_KalemIskontoTutari5");
                    _STK002_KalemIskontoTutari5 = value;
                }
            }
        }

        [DbAlan("STK002_DovizCinsi", SqlDbType.NVarChar, 6)]
        public string STK002_DovizCinsi
        {
            get { return _STK002_DovizCinsi; }
            set
            {
                if (_STK002_DovizCinsi != value)
                {
                    OnPropertyChanged("STK002_DovizCinsi");
                    _STK002_DovizCinsi = value;
                }
            }
        }

        [DbAlan("STK002_DovizTutari", SqlDbType.Decimal, 9)]
        public Nullable<decimal> STK002_DovizTutari
        {
            get { return _STK002_DovizTutari; }
            set
            {
                if (_STK002_DovizTutari != value)
                {
                    OnPropertyChanged("STK002_DovizTutari");
                    _STK002_DovizTutari = value;
                }
            }
        }

        [DbAlan("STK002_DovizKuru", SqlDbType.Decimal, 9)]
        public Nullable<decimal> STK002_DovizKuru
        {
            get { return _STK002_DovizKuru; }
            set
            {
                if (_STK002_DovizKuru != value)
                {
                    OnPropertyChanged("STK002_DovizKuru");
                    _STK002_DovizKuru = value;
                }
            }
        }

        [DbAlan("STK002_Aciklama1", SqlDbType.NVarChar, 128)]
        public string STK002_Aciklama1
        {
            get { return _STK002_Aciklama1; }
            set
            {
                if (_STK002_Aciklama1 != value)
                {
                    OnPropertyChanged("STK002_Aciklama1");
                    _STK002_Aciklama1 = value;
                }
            }
        }

        [DbAlan("STK002_Aciklama2", SqlDbType.NVarChar, 128)]
        public string STK002_Aciklama2
        {
            get { return _STK002_Aciklama2; }
            set
            {
                if (_STK002_Aciklama2 != value)
                {
                    OnPropertyChanged("STK002_Aciklama2");
                    _STK002_Aciklama2 = value;
                }
            }
        }

        [DbAlan("STK002_Depo", SqlDbType.NVarChar, 6)]
        public string STK002_Depo
        {
            get { return _STK002_Depo; }
            set
            {
                if (_STK002_Depo != value)
                {
                    OnPropertyChanged("STK002_Depo");
                    _STK002_Depo = value;
                }
            }
        }

        [DbAlan("STK002_Kod3", SqlDbType.NVarChar, 40)]
        public string STK002_Kod3
        {
            get { return _STK002_Kod3; }
            set
            {
                if (_STK002_Kod3 != value)
                {
                    OnPropertyChanged("STK002_Kod3");
                    _STK002_Kod3 = value;
                }
            }
        }

        [DbAlan("STK002_Kod4", SqlDbType.NVarChar, 40)]
        public string STK002_Kod4
        {
            get { return _STK002_Kod4; }
            set
            {
                if (_STK002_Kod4 != value)
                {
                    OnPropertyChanged("STK002_Kod4");
                    _STK002_Kod4 = value;
                }
            }
        }

        [DbAlan("STK002_Kod5", SqlDbType.NVarChar, 40)]
        public string STK002_Kod5
        {
            get { return _STK002_Kod5; }
            set
            {
                if (_STK002_Kod5 != value)
                {
                    OnPropertyChanged("STK002_Kod5");
                    _STK002_Kod5 = value;
                }
            }
        }

        [DbAlan("STK002_Kod6", SqlDbType.NVarChar, 40)]
        public string STK002_Kod6
        {
            get { return _STK002_Kod6; }
            set
            {
                if (_STK002_Kod6 != value)
                {
                    OnPropertyChanged("STK002_Kod6");
                    _STK002_Kod6 = value;
                }
            }
        }

        [DbAlan("STK002_Kod7", SqlDbType.NVarChar, 40)]
        public string STK002_Kod7
        {
            get { return _STK002_Kod7; }
            set
            {
                if (_STK002_Kod7 != value)
                {
                    OnPropertyChanged("STK002_Kod7");
                    _STK002_Kod7 = value;
                }
            }
        }

        [DbAlan("STK002_Kod8", SqlDbType.NVarChar, 40)]
        public string STK002_Kod8
        {
            get { return _STK002_Kod8; }
            set
            {
                if (_STK002_Kod8 != value)
                {
                    OnPropertyChanged("STK002_Kod8");
                    _STK002_Kod8 = value;
                }
            }
        }

        [DbAlan("STK002_Kod9", SqlDbType.NVarChar, 40)]
        public string STK002_Kod9
        {
            get { return _STK002_Kod9; }
            set
            {
                if (_STK002_Kod9 != value)
                {
                    OnPropertyChanged("STK002_Kod9");
                    _STK002_Kod9 = value;
                }
            }
        }

        [DbAlan("STK002_Kod10", SqlDbType.NVarChar, 40)]
        public string STK002_Kod10
        {
            get { return _STK002_Kod10; }
            set
            {
                if (_STK002_Kod10 != value)
                {
                    OnPropertyChanged("STK002_Kod10");
                    _STK002_Kod10 = value;
                }
            }
        }

        [DbAlan("STK002_Kod11", SqlDbType.Decimal, 9)]
        public Nullable<decimal> STK002_Kod11
        {
            get { return _STK002_Kod11; }
            set
            {
                if (_STK002_Kod11 != value)
                {
                    OnPropertyChanged("STK002_Kod11");
                    _STK002_Kod11 = value;
                }
            }
        }

        [DbAlan("STK002_Kod12", SqlDbType.Decimal, 9)]
        public Nullable<decimal> STK002_Kod12
        {
            get { return _STK002_Kod12; }
            set
            {
                if (_STK002_Kod12 != value)
                {
                    OnPropertyChanged("STK002_Kod12");
                    _STK002_Kod12 = value;
                }
            }
        }

        [DbAlan("STK002_Vasita", SqlDbType.NVarChar, 24)]
        public string STK002_Vasita
        {
            get { return _STK002_Vasita; }
            set
            {
                if (_STK002_Vasita != value)
                {
                    OnPropertyChanged("STK002_Vasita");
                    _STK002_Vasita = value;
                }
            }
        }

        [DbAlan("STK002_MalSeriNo", SqlDbType.NVarChar, 40)]
        public string STK002_MalSeriNo
        {
            get { return _STK002_MalSeriNo; }
            set
            {
                if (_STK002_MalSeriNo != value)
                {
                    OnPropertyChanged("STK002_MalSeriNo");
                    _STK002_MalSeriNo = value;
                }
            }
        }

        [DbAlan("STK002_VadeTarihi", SqlDbType.Int, 4)]
        public Nullable<int> STK002_VadeTarihi
        {
            get { return _STK002_VadeTarihi; }
            set
            {
                if (_STK002_VadeTarihi != value)
                {
                    OnPropertyChanged("STK002_VadeTarihi");
                    _STK002_VadeTarihi = value;
                }
            }
        }

        [DbAlan("STK002_Masraf", SqlDbType.Decimal, 9)]
        public Nullable<decimal> STK002_Masraf
        {
            get { return _STK002_Masraf; }
            set
            {
                if (_STK002_Masraf != value)
                {
                    OnPropertyChanged("STK002_Masraf");
                    _STK002_Masraf = value;
                }
            }
        }

        [DbAlan("STK002_EvrakSeriNo2", SqlDbType.NVarChar, 32)]
        public string STK002_EvrakSeriNo2
        {
            get { return _STK002_EvrakSeriNo2; }
            set
            {
                if (_STK002_EvrakSeriNo2 != value)
                {
                    OnPropertyChanged("STK002_EvrakSeriNo2");
                    _STK002_EvrakSeriNo2 = value;
                }
            }
        }

        [DbAlan("STK002_Tarih2", SqlDbType.Int, 4)]
        public Nullable<int> STK002_Tarih2
        {
            get { return _STK002_Tarih2; }
            set
            {
                if (_STK002_Tarih2 != value)
                {
                    OnPropertyChanged("STK002_Tarih2");
                    _STK002_Tarih2 = value;
                }
            }
        }

        [DbAlan("STK002_Kod9Flag", SqlDbType.TinyInt, 1)]
        public Nullable<byte> STK002_Kod9Flag
        {
            get { return _STK002_Kod9Flag; }
            set
            {
                if (_STK002_Kod9Flag != value)
                {
                    OnPropertyChanged("STK002_Kod9Flag");
                    _STK002_Kod9Flag = value;
                }
            }
        }

        [DbAlan("STK002_Kod10Flag", SqlDbType.TinyInt, 1)]
        public Nullable<byte> STK002_Kod10Flag
        {
            get { return _STK002_Kod10Flag; }
            set
            {
                if (_STK002_Kod10Flag != value)
                {
                    OnPropertyChanged("STK002_Kod10Flag");
                    _STK002_Kod10Flag = value;
                }
            }
        }

        [DbAlan("STK002_KDVOranFlag", SqlDbType.TinyInt, 1)]
        public Nullable<byte> STK002_KDVOranFlag
        {
            get { return _STK002_KDVOranFlag; }
            set
            {
                if (_STK002_KDVOranFlag != value)
                {
                    OnPropertyChanged("STK002_KDVOranFlag");
                    _STK002_KDVOranFlag = value;
                }
            }
        }

        [DbAlan("STK002_TeslimCHKodu", SqlDbType.NVarChar, 32)]
        public string STK002_TeslimCHKodu
        {
            get { return _STK002_TeslimCHKodu; }
            set
            {
                if (_STK002_TeslimCHKodu != value)
                {
                    OnPropertyChanged("STK002_TeslimCHKodu");
                    _STK002_TeslimCHKodu = value;
                }
            }
        }

        [DbAlan("STK002_MasrafMerkezi", SqlDbType.NVarChar, 40)]
        public string STK002_MasrafMerkezi
        {
            get { return _STK002_MasrafMerkezi; }
            set
            {
                if (_STK002_MasrafMerkezi != value)
                {
                    OnPropertyChanged("STK002_MasrafMerkezi");
                    _STK002_MasrafMerkezi = value;
                }
            }
        }

        [DbAlan("STK002_SonSevkTarihi", SqlDbType.Int, 4)]
        public Nullable<int> STK002_SonSevkTarihi
        {
            get { return _STK002_SonSevkTarihi; }
            set
            {
                if (_STK002_SonSevkTarihi != value)
                {
                    OnPropertyChanged("STK002_SonSevkTarihi");
                    _STK002_SonSevkTarihi = value;
                }
            }
        }

        [DbAlan("STK002_EFaturaOTVListeNo", SqlDbType.NVarChar, 4)]
        public string STK002_EFaturaOTVListeNo
        {
            get { return _STK002_EFaturaOTVListeNo; }
            set
            {
                if (_STK002_EFaturaOTVListeNo != value)
                {
                    OnPropertyChanged("STK002_EFaturaOTVListeNo");
                    _STK002_EFaturaOTVListeNo = value;
                }
            }
        }

        [DbAlan("STK002_KDVMuafAciklama", SqlDbType.NVarChar, 20)]
        public string STK002_KDVMuafAciklama
        {
            get { return _STK002_KDVMuafAciklama; }
            set
            {
                if (_STK002_KDVMuafAciklama != value)
                {
                    OnPropertyChanged("STK002_KDVMuafAciklama");
                    _STK002_KDVMuafAciklama = value;
                }
            }
        }

        [DbAlan("STK002_DovizBirimFiyati", SqlDbType.Decimal, 13)]
        public Nullable<decimal> STK002_DovizBirimFiyati
        {
            get { return _STK002_DovizBirimFiyati; }
            set
            {
                if (_STK002_DovizBirimFiyati != value)
                {
                    OnPropertyChanged("STK002_DovizBirimFiyati");
                    _STK002_DovizBirimFiyati = value;
                }
            }
        }

        [DbAlan("STK002_Not1", SqlDbType.NVarChar, 256)]
        public string STK002_Not1
        {
            get { return _STK002_Not1; }
            set
            {
                if (_STK002_Not1 != value)
                {
                    OnPropertyChanged("STK002_Not1");
                    _STK002_Not1 = value;
                }
            }
        }

        [DbAlan("STK002_Not2", SqlDbType.NVarChar, 256)]
        public string STK002_Not2
        {
            get { return _STK002_Not2; }
            set
            {
                if (_STK002_Not2 != value)
                {
                    OnPropertyChanged("STK002_Not2");
                    _STK002_Not2 = value;
                }
            }
        }

        [DbAlan("STK002_Not3", SqlDbType.NVarChar, 256)]
        public string STK002_Not3
        {
            get { return _STK002_Not3; }
            set
            {
                if (_STK002_Not3 != value)
                {
                    OnPropertyChanged("STK002_Not3");
                    _STK002_Not3 = value;
                }
            }
        }

        [DbAlan("STK002_Not4", SqlDbType.NVarChar, 256)]
        public string STK002_Not4
        {
            get { return _STK002_Not4; }
            set
            {
                if (_STK002_Not4 != value)
                {
                    OnPropertyChanged("STK002_Not4");
                    _STK002_Not4 = value;
                }
            }
        }

        [DbAlan("STK002_Not5", SqlDbType.NVarChar, 256)]
        public string STK002_Not5
        {
            get { return _STK002_Not5; }
            set
            {
                if (_STK002_Not5 != value)
                {
                    OnPropertyChanged("STK002_Not5");
                    _STK002_Not5 = value;
                }
            }
        }

        #endregion table Properties



        #region property changes events functions

        public event PropertyChangedEventHandler PropertyChanged;
        private List<PropertyValue> _ChangedList = new List<PropertyValue>();
        public PropertyValue[] ChangedList { get { return _ChangedList.ToArray(); } }



        internal void STK002_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

            //PropertyValue val = _ChangedList.Find(t => t.PropertiName == propertyName);
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


    }
}
