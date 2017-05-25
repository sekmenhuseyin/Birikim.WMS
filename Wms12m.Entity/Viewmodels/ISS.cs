using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity.Viewmodels
{
    #region ISS Class 

    #region ISSE Enum 
    public enum ISSE
    {
        ListeNo,
        ListeAdi,
        BasTarih,
        BasSaat,
        BitTarih,
        BitSaat,
        MusUygSekli,
        MusKodGrup,
        MusteriKod,
        MalUygSekli,
        MalKodGrup,
        MalKod,
        SiraNo,
        Oran,
        Oran1,
        Oran2,
        Oran3,
        Oran4,
        Oran5,
        MikAralik1,
        MikYuzde1,
        MikAralik2,
        MikYuzde2,
        MikAralik3,
        MikYuzde3,
        MikAralik4,
        MikYuzde4,
        MikAralik5,
        MikYuzde5,
        MikAralik6,
        MikYuzde6,
        MikAralik7,
        MikYuzde7,
        MikAralik8,
        MikYuzde8,
        TutarAralik1,
        TutarYuzde1,
        TutarAralik2,
        TutarYuzde2,
        TutarAralik3,
        TutarYuzde3,
        TutarAralik4,
        TutarYuzde4,
        TutarAralik5,
        TutarYuzde5,
        TutarAralik6,
        TutarYuzde6,
        TutarAralik7,
        TutarYuzde7,
        TutarAralik8,
        TutarYuzde8,
        OdemeAralik1,
        OdemeYuzde1,
        OdemeAralik2,
        OdemeYuzde2,
        OdemeAralik3,
        OdemeYuzde3,
        OdemeAralik4,
        OdemeYuzde4,
        OdemeAralik5,
        OdemeYuzde5,
        OdemeAralik6,
        OdemeYuzde6,
        OdemeAralik7,
        OdemeYuzde7,
        OdemeAralik8,
        OdemeYuzde8,
        KayitTuru,
        GuvenlikKod,
        Kaydeden,
        KayitTarih,
        KayitSaat,
        KayitKaynak,
        KayitSurum,
        Degistiren,
        DegisTarih,
        DegisSaat,
        DegisKaynak,
        DegisSurum,
        CheckSum,
        Aciklama,
        Kod1,
        Kod2,
        Kod3,
        Kod4,
        Kod5,
        Kod6,
        Kod7,
        Kod8,
        Kod9,
        Kod10,
        Kod11,
        Kod12,
        DevirTarih,
        DevirTutar,
        Row_ID,
        timestamp

    }
    #endregion /// ISSE Enum           

    public class ISS : INotifyPropertyChanged
    {
        #region Properties
        #region Fields  
        private string _ListeNo;
        private string _ListeAdi;
        private int _BasTarih;
        private int _BasSaat;
        private int _BitTarih;
        private int _BitSaat;
        private short _MusUygSekli;
        private short _MusKodGrup;
        private string _MusteriKod;
        private short _MalUygSekli;
        private short _MalKodGrup;
        private string _MalKod;
        private short _SiraNo;
        private float _Oran;
        private float _Oran1;
        private float _Oran2;
        private float _Oran3;
        private float _Oran4;
        private float _Oran5;
        private decimal _MikAralik1;
        private float _MikYuzde1;
        private decimal _MikAralik2;
        private float _MikYuzde2;
        private decimal _MikAralik3;
        private float _MikYuzde3;
        private decimal _MikAralik4;
        private float _MikYuzde4;
        private decimal _MikAralik5;
        private float _MikYuzde5;
        private decimal _MikAralik6;
        private float _MikYuzde6;
        private decimal _MikAralik7;
        private float _MikYuzde7;
        private decimal _MikAralik8;
        private float _MikYuzde8;
        private decimal _TutarAralik1;
        private float _TutarYuzde1;
        private decimal _TutarAralik2;
        private float _TutarYuzde2;
        private decimal _TutarAralik3;
        private float _TutarYuzde3;
        private decimal _TutarAralik4;
        private float _TutarYuzde4;
        private decimal _TutarAralik5;
        private float _TutarYuzde5;
        private decimal _TutarAralik6;
        private float _TutarYuzde6;
        private decimal _TutarAralik7;
        private float _TutarYuzde7;
        private decimal _TutarAralik8;
        private float _TutarYuzde8;
        private decimal _OdemeAralik1;
        private float _OdemeYuzde1;
        private decimal _OdemeAralik2;
        private float _OdemeYuzde2;
        private decimal _OdemeAralik3;
        private float _OdemeYuzde3;
        private decimal _OdemeAralik4;
        private float _OdemeYuzde4;
        private decimal _OdemeAralik5;
        private float _OdemeYuzde5;
        private decimal _OdemeAralik6;
        private float _OdemeYuzde6;
        private decimal _OdemeAralik7;
        private float _OdemeYuzde7;
        private decimal _OdemeAralik8;
        private float _OdemeYuzde8;
        private short _KayitTuru;
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
        private string _Aciklama;
        private string _Kod1;
        private string _Kod2;
        private string _Kod3;
        private string _Kod4;
        private string _Kod5;
        private string _Kod6;
        private string _Kod7;
        private string _Kod8;
        private string _Kod9;
        private string _Kod10;
        private decimal _Kod11;
        private decimal _Kod12;
        private int? _DevirTarih;
        private decimal? _DevirTutar;
        private int _Row_ID;
        private byte[] _timestamp;
        private string _pk_ListeNo;
        private int _pk_BasTarih;
        private short _pk_MusUygSekli;
        private short _pk_SiraNo;
        #endregion /// Fields


        /// <summary> VARCHAR (16) PrimaryKey * </summary>
        public string ListeNo
        {
            get { return this._ListeNo; }
            set
            {
                this._ListeNo = value;
                OnPropertyChanged("ListeNo");
            }
        }

        /// <summary> VARCHAR (40) * </summary>
        public string ListeAdi
        {
            get { return this._ListeAdi; }
            set
            {
                this._ListeAdi = value;
                OnPropertyChanged("ListeAdi");
            }
        }

        /// <summary> INT (4) PrimaryKey * </summary>
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
        public int BasSaat
        {
            get { return this._BasSaat; }
            set
            {
                this._BasSaat = value;
                OnPropertyChanged("BasSaat");
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

        /// <summary> INT (4) * </summary>
        public int BitSaat
        {
            get { return this._BitSaat; }
            set
            {
                this._BitSaat = value;
                OnPropertyChanged("BitSaat");
            }
        }

        /// <summary> SMALLINT (2) PrimaryKey * </summary>
        public short MusUygSekli
        {
            get { return this._MusUygSekli; }
            set
            {
                this._MusUygSekli = value;
                OnPropertyChanged("MusUygSekli");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short MusKodGrup
        {
            get { return this._MusKodGrup; }
            set
            {
                this._MusKodGrup = value;
                OnPropertyChanged("MusKodGrup");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string MusteriKod
        {
            get { return this._MusteriKod; }
            set
            {
                this._MusteriKod = value;
                OnPropertyChanged("MusteriKod");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short MalUygSekli
        {
            get { return this._MalUygSekli; }
            set
            {
                this._MalUygSekli = value;
                OnPropertyChanged("MalUygSekli");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short MalKodGrup
        {
            get { return this._MalKodGrup; }
            set
            {
                this._MalKodGrup = value;
                OnPropertyChanged("MalKodGrup");
            }
        }

        /// <summary> VARCHAR (30) * </summary>
        public string MalKod
        {
            get { return this._MalKod; }
            set
            {
                this._MalKod = value;
                OnPropertyChanged("MalKod");
            }
        }

        /// <summary> SMALLINT (2) PrimaryKey * </summary>
        public short SiraNo
        {
            get { return this._SiraNo; }
            set
            {
                this._SiraNo = value;
                OnPropertyChanged("SiraNo");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float Oran
        {
            get { return this._Oran; }
            set
            {
                this._Oran = value;
                OnPropertyChanged("Oran");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float Oran1
        {
            get { return this._Oran1; }
            set
            {
                this._Oran1 = value;
                OnPropertyChanged("Oran1");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float Oran2
        {
            get { return this._Oran2; }
            set
            {
                this._Oran2 = value;
                OnPropertyChanged("Oran2");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float Oran3
        {
            get { return this._Oran3; }
            set
            {
                this._Oran3 = value;
                OnPropertyChanged("Oran3");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float Oran4
        {
            get { return this._Oran4; }
            set
            {
                this._Oran4 = value;
                OnPropertyChanged("Oran4");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float Oran5
        {
            get { return this._Oran5; }
            set
            {
                this._Oran5 = value;
                OnPropertyChanged("Oran5");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal MikAralik1
        {
            get { return this._MikAralik1; }
            set
            {
                this._MikAralik1 = value;
                OnPropertyChanged("MikAralik1");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float MikYuzde1
        {
            get { return this._MikYuzde1; }
            set
            {
                this._MikYuzde1 = value;
                OnPropertyChanged("MikYuzde1");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal MikAralik2
        {
            get { return this._MikAralik2; }
            set
            {
                this._MikAralik2 = value;
                OnPropertyChanged("MikAralik2");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float MikYuzde2
        {
            get { return this._MikYuzde2; }
            set
            {
                this._MikYuzde2 = value;
                OnPropertyChanged("MikYuzde2");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal MikAralik3
        {
            get { return this._MikAralik3; }
            set
            {
                this._MikAralik3 = value;
                OnPropertyChanged("MikAralik3");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float MikYuzde3
        {
            get { return this._MikYuzde3; }
            set
            {
                this._MikYuzde3 = value;
                OnPropertyChanged("MikYuzde3");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal MikAralik4
        {
            get { return this._MikAralik4; }
            set
            {
                this._MikAralik4 = value;
                OnPropertyChanged("MikAralik4");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float MikYuzde4
        {
            get { return this._MikYuzde4; }
            set
            {
                this._MikYuzde4 = value;
                OnPropertyChanged("MikYuzde4");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal MikAralik5
        {
            get { return this._MikAralik5; }
            set
            {
                this._MikAralik5 = value;
                OnPropertyChanged("MikAralik5");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float MikYuzde5
        {
            get { return this._MikYuzde5; }
            set
            {
                this._MikYuzde5 = value;
                OnPropertyChanged("MikYuzde5");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal MikAralik6
        {
            get { return this._MikAralik6; }
            set
            {
                this._MikAralik6 = value;
                OnPropertyChanged("MikAralik6");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float MikYuzde6
        {
            get { return this._MikYuzde6; }
            set
            {
                this._MikYuzde6 = value;
                OnPropertyChanged("MikYuzde6");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal MikAralik7
        {
            get { return this._MikAralik7; }
            set
            {
                this._MikAralik7 = value;
                OnPropertyChanged("MikAralik7");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float MikYuzde7
        {
            get { return this._MikYuzde7; }
            set
            {
                this._MikYuzde7 = value;
                OnPropertyChanged("MikYuzde7");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal MikAralik8
        {
            get { return this._MikAralik8; }
            set
            {
                this._MikAralik8 = value;
                OnPropertyChanged("MikAralik8");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float MikYuzde8
        {
            get { return this._MikYuzde8; }
            set
            {
                this._MikYuzde8 = value;
                OnPropertyChanged("MikYuzde8");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal TutarAralik1
        {
            get { return this._TutarAralik1; }
            set
            {
                this._TutarAralik1 = value;
                OnPropertyChanged("TutarAralik1");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float TutarYuzde1
        {
            get { return this._TutarYuzde1; }
            set
            {
                this._TutarYuzde1 = value;
                OnPropertyChanged("TutarYuzde1");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal TutarAralik2
        {
            get { return this._TutarAralik2; }
            set
            {
                this._TutarAralik2 = value;
                OnPropertyChanged("TutarAralik2");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float TutarYuzde2
        {
            get { return this._TutarYuzde2; }
            set
            {
                this._TutarYuzde2 = value;
                OnPropertyChanged("TutarYuzde2");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal TutarAralik3
        {
            get { return this._TutarAralik3; }
            set
            {
                this._TutarAralik3 = value;
                OnPropertyChanged("TutarAralik3");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float TutarYuzde3
        {
            get { return this._TutarYuzde3; }
            set
            {
                this._TutarYuzde3 = value;
                OnPropertyChanged("TutarYuzde3");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal TutarAralik4
        {
            get { return this._TutarAralik4; }
            set
            {
                this._TutarAralik4 = value;
                OnPropertyChanged("TutarAralik4");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float TutarYuzde4
        {
            get { return this._TutarYuzde4; }
            set
            {
                this._TutarYuzde4 = value;
                OnPropertyChanged("TutarYuzde4");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal TutarAralik5
        {
            get { return this._TutarAralik5; }
            set
            {
                this._TutarAralik5 = value;
                OnPropertyChanged("TutarAralik5");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float TutarYuzde5
        {
            get { return this._TutarYuzde5; }
            set
            {
                this._TutarYuzde5 = value;
                OnPropertyChanged("TutarYuzde5");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal TutarAralik6
        {
            get { return this._TutarAralik6; }
            set
            {
                this._TutarAralik6 = value;
                OnPropertyChanged("TutarAralik6");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float TutarYuzde6
        {
            get { return this._TutarYuzde6; }
            set
            {
                this._TutarYuzde6 = value;
                OnPropertyChanged("TutarYuzde6");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal TutarAralik7
        {
            get { return this._TutarAralik7; }
            set
            {
                this._TutarAralik7 = value;
                OnPropertyChanged("TutarAralik7");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float TutarYuzde7
        {
            get { return this._TutarYuzde7; }
            set
            {
                this._TutarYuzde7 = value;
                OnPropertyChanged("TutarYuzde7");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal TutarAralik8
        {
            get { return this._TutarAralik8; }
            set
            {
                this._TutarAralik8 = value;
                OnPropertyChanged("TutarAralik8");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float TutarYuzde8
        {
            get { return this._TutarYuzde8; }
            set
            {
                this._TutarYuzde8 = value;
                OnPropertyChanged("TutarYuzde8");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal OdemeAralik1
        {
            get { return this._OdemeAralik1; }
            set
            {
                this._OdemeAralik1 = value;
                OnPropertyChanged("OdemeAralik1");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float OdemeYuzde1
        {
            get { return this._OdemeYuzde1; }
            set
            {
                this._OdemeYuzde1 = value;
                OnPropertyChanged("OdemeYuzde1");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal OdemeAralik2
        {
            get { return this._OdemeAralik2; }
            set
            {
                this._OdemeAralik2 = value;
                OnPropertyChanged("OdemeAralik2");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float OdemeYuzde2
        {
            get { return this._OdemeYuzde2; }
            set
            {
                this._OdemeYuzde2 = value;
                OnPropertyChanged("OdemeYuzde2");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal OdemeAralik3
        {
            get { return this._OdemeAralik3; }
            set
            {
                this._OdemeAralik3 = value;
                OnPropertyChanged("OdemeAralik3");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float OdemeYuzde3
        {
            get { return this._OdemeYuzde3; }
            set
            {
                this._OdemeYuzde3 = value;
                OnPropertyChanged("OdemeYuzde3");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal OdemeAralik4
        {
            get { return this._OdemeAralik4; }
            set
            {
                this._OdemeAralik4 = value;
                OnPropertyChanged("OdemeAralik4");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float OdemeYuzde4
        {
            get { return this._OdemeYuzde4; }
            set
            {
                this._OdemeYuzde4 = value;
                OnPropertyChanged("OdemeYuzde4");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal OdemeAralik5
        {
            get { return this._OdemeAralik5; }
            set
            {
                this._OdemeAralik5 = value;
                OnPropertyChanged("OdemeAralik5");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float OdemeYuzde5
        {
            get { return this._OdemeYuzde5; }
            set
            {
                this._OdemeYuzde5 = value;
                OnPropertyChanged("OdemeYuzde5");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal OdemeAralik6
        {
            get { return this._OdemeAralik6; }
            set
            {
                this._OdemeAralik6 = value;
                OnPropertyChanged("OdemeAralik6");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float OdemeYuzde6
        {
            get { return this._OdemeYuzde6; }
            set
            {
                this._OdemeYuzde6 = value;
                OnPropertyChanged("OdemeYuzde6");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal OdemeAralik7
        {
            get { return this._OdemeAralik7; }
            set
            {
                this._OdemeAralik7 = value;
                OnPropertyChanged("OdemeAralik7");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float OdemeYuzde7
        {
            get { return this._OdemeYuzde7; }
            set
            {
                this._OdemeYuzde7 = value;
                OnPropertyChanged("OdemeYuzde7");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal OdemeAralik8
        {
            get { return this._OdemeAralik8; }
            set
            {
                this._OdemeAralik8 = value;
                OnPropertyChanged("OdemeAralik8");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float OdemeYuzde8
        {
            get { return this._OdemeYuzde8; }
            set
            {
                this._OdemeYuzde8 = value;
                OnPropertyChanged("OdemeYuzde8");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short KayitTuru
        {
            get { return this._KayitTuru; }
            set
            {
                this._KayitTuru = value;
                OnPropertyChanged("KayitTuru");
            }
        }

        /// <summary> VARCHAR (2) * </summary>
        public string GuvenlikKod
        {
            get { return this._GuvenlikKod; }
            set
            {
                this._GuvenlikKod = value;
                OnPropertyChanged("GuvenlikKod");
            }
        }

        /// <summary> VARCHAR (5) * </summary>
        public string Kaydeden
        {
            get { return this._Kaydeden; }
            set
            {
                this._Kaydeden = value;
                OnPropertyChanged("Kaydeden");
            }
        }

        /// <summary> INT (4) * </summary>
        public int KayitTarih
        {
            get { return this._KayitTarih; }
            set
            {
                this._KayitTarih = value;
                OnPropertyChanged("KayitTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int KayitSaat
        {
            get { return this._KayitSaat; }
            set
            {
                this._KayitSaat = value;
                OnPropertyChanged("KayitSaat");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short KayitKaynak
        {
            get { return this._KayitKaynak; }
            set
            {
                this._KayitKaynak = value;
                OnPropertyChanged("KayitKaynak");
            }
        }

        /// <summary> VARCHAR (12) * </summary>
        public string KayitSurum
        {
            get { return this._KayitSurum; }
            set
            {
                this._KayitSurum = value;
                OnPropertyChanged("KayitSurum");
            }
        }

        /// <summary> VARCHAR (5) * </summary>
        public string Degistiren
        {
            get { return this._Degistiren; }
            set
            {
                this._Degistiren = value;
                OnPropertyChanged("Degistiren");
            }
        }

        /// <summary> INT (4) * </summary>
        public int DegisTarih
        {
            get { return this._DegisTarih; }
            set
            {
                this._DegisTarih = value;
                OnPropertyChanged("DegisTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int DegisSaat
        {
            get { return this._DegisSaat; }
            set
            {
                this._DegisSaat = value;
                OnPropertyChanged("DegisSaat");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DegisKaynak
        {
            get { return this._DegisKaynak; }
            set
            {
                this._DegisKaynak = value;
                OnPropertyChanged("DegisKaynak");
            }
        }

        /// <summary> VARCHAR (12) * </summary>
        public string DegisSurum
        {
            get { return this._DegisSurum; }
            set
            {
                this._DegisSurum = value;
                OnPropertyChanged("DegisSurum");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short CheckSum
        {
            get { return this._CheckSum; }
            set
            {
                this._CheckSum = value;
                OnPropertyChanged("CheckSum");
            }
        }

        /// <summary> VARCHAR (64) * </summary>
        public string Aciklama
        {
            get { return this._Aciklama; }
            set
            {
                this._Aciklama = value;
                OnPropertyChanged("Aciklama");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod1
        {
            get { return this._Kod1; }
            set
            {
                this._Kod1 = value;
                OnPropertyChanged("Kod1");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod2
        {
            get { return this._Kod2; }
            set
            {
                this._Kod2 = value;
                OnPropertyChanged("Kod2");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod3
        {
            get { return this._Kod3; }
            set
            {
                this._Kod3 = value;
                OnPropertyChanged("Kod3");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod4
        {
            get { return this._Kod4; }
            set
            {
                this._Kod4 = value;
                OnPropertyChanged("Kod4");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod5
        {
            get { return this._Kod5; }
            set
            {
                this._Kod5 = value;
                OnPropertyChanged("Kod5");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod6
        {
            get { return this._Kod6; }
            set
            {
                this._Kod6 = value;
                OnPropertyChanged("Kod6");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod7
        {
            get { return this._Kod7; }
            set
            {
                this._Kod7 = value;
                OnPropertyChanged("Kod7");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod8
        {
            get { return this._Kod8; }
            set
            {
                this._Kod8 = value;
                OnPropertyChanged("Kod8");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod9
        {
            get { return this._Kod9; }
            set
            {
                this._Kod9 = value;
                OnPropertyChanged("Kod9");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod10
        {
            get { return this._Kod10; }
            set
            {
                this._Kod10 = value;
                OnPropertyChanged("Kod10");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Kod11
        {
            get { return this._Kod11; }
            set
            {
                this._Kod11 = value;
                OnPropertyChanged("Kod11");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Kod12
        {
            get { return this._Kod12; }
            set
            {
                this._Kod12 = value;
                OnPropertyChanged("Kod12");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? DevirTarih
        {
            get { return this._DevirTarih; }
            set
            {
                this._DevirTarih = value;
                OnPropertyChanged("DevirTarih");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? DevirTutar
        {
            get { return this._DevirTutar; }
            set
            {
                this._DevirTutar = value;
                OnPropertyChanged("DevirTutar");
            }
        }

        /// <summary> INT (4) IdentityKey * </summary>
        public int Row_ID
        {
            get { return this._Row_ID; }
        }

        /// <summary> TIMESTAMP (8) * </summary>
        public byte[] timestamp
        {
            get { return this._timestamp; }
            set
            {
                this._timestamp = value;
                OnPropertyChanged("timestamp");
            }
        }

        /// <summary> VARCHAR (16) PRIMARY KEY * </summary>
        public string pk_ListeNo
        {
            private get { return this._pk_ListeNo; }
            set
            {
                this._pk_ListeNo = value;
                OnPropertyChanged("pk_ListeNo");
            }
        }

        /// <summary> INT (4) PRIMARY KEY * </summary>
        public int pk_BasTarih
        {
            private get { return this._pk_BasTarih; }
            set
            {
                this._pk_BasTarih = value;
                OnPropertyChanged("pk_BasTarih");
            }
        }

        /// <summary> SMALLINT (2) PRIMARY KEY * </summary>
        public short pk_MusUygSekli
        {
            private get { return this._pk_MusUygSekli; }
            set
            {
                this._pk_MusUygSekli = value;
                OnPropertyChanged("pk_MusUygSekli");
            }
        }

        /// <summary> SMALLINT (2) PRIMARY KEY * </summary>
        public short pk_SiraNo
        {
            private get { return this._pk_SiraNo; }
            set
            {
                this._pk_SiraNo = value;
                OnPropertyChanged("pk_SiraNo");
            }
        }
        #endregion /// Properties       

        #region Tablo Bilgileri & Metodlar

        private List<string> WhereList = new List<string>();
        private List<string> SetList = new List<string>();
        private string info_FullTableName = "FINSAT6{0}.FINSAT6{0}.ISS";
        private string[] info_PrimaryKeys = { "pk_ListeNo", "pk_BasTarih", "pk_MusUygSekli", "pk_SiraNo" };
        private string[] info_IdentityKeys = { "Row_ID" };

        private List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public ISS()
        {
            ChangedProperties = new List<string>();
            this.PropertyChanged += ISS_PropertyChanged;
        }

        public void AcceptChanges()
        {
            ChangedProperties.Clear();
        }

        void ISS_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
    #endregion /// ISS Class
}
