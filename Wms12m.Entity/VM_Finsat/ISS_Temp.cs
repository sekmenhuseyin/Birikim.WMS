using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Wms12m.Entity
{
    #region ISS_Temp Class 

    #region ISS_TempE Enum 
    public enum ISS_TempE
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
        OnayTip,
        SatisMuduruOnay,
        FinansmanMuduruOnay,
        GenelMudurOnay,
        OnaylayanSatisMuduru,
        OnayTarihSatisMuduru,
        OnaylayanFinansmanMuduru,
        OnayTarihFinansmanMuduru,
        OnaylayanGenelMudur,
        OnayTarihGenelMudur,
        CekTutari,
        CekOrtalamaVadeTarih,
        NakitTutari,
        BaglantiParaCinsi,
        AktifPasif,
        VadeTarihi,
        ValorGun,
        Row_ID,
        timestamp

    }
    #endregion /// ISS_TempE Enum           

    public class ISS_Temp : INotifyPropertyChanged
    {
        #region Properties
        #region Fields  
        string listeNo;
        string listeAdi;
        int basTarih;
        int basSaat;
        int bitTarih;
        int bitSaat;
        short musUygSekli;
        short musKodGrup;
        string musteriKod;
        short malUygSekli;
        short malKodGrup;
        string malKod;
        short siraNo;
        float oran;
        float oran1;
        float oran2;
        float oran3;
        float oran4;
        float oran5;
        decimal mikAralik1;
        float mikYuzde1;
        decimal mikAralik2;
        float mikYuzde2;
        decimal mikAralik3;
        float mikYuzde3;
        decimal mikAralik4;
        float mikYuzde4;
        decimal mikAralik5;
        float mikYuzde5;
        decimal mikAralik6;
        float mikYuzde6;
        decimal mikAralik7;
        float mikYuzde7;
        decimal mikAralik8;
        float mikYuzde8;
        decimal tutarAralik1;
        float tutarYuzde1;
        decimal tutarAralik2;
        float tutarYuzde2;
        decimal tutarAralik3;
        float tutarYuzde3;
        decimal tutarAralik4;
        float tutarYuzde4;
        decimal tutarAralik5;
        float tutarYuzde5;
        decimal tutarAralik6;
        float tutarYuzde6;
        decimal tutarAralik7;
        float tutarYuzde7;
        decimal tutarAralik8;
        float tutarYuzde8;
        decimal odemeAralik1;
        float odemeYuzde1;
        decimal odemeAralik2;
        float odemeYuzde2;
        decimal odemeAralik3;
        float odemeYuzde3;
        decimal odemeAralik4;
        float odemeYuzde4;
        decimal odemeAralik5;
        float odemeYuzde5;
        decimal odemeAralik6;
        float odemeYuzde6;
        decimal odemeAralik7;
        float odemeYuzde7;
        decimal odemeAralik8;
        float odemeYuzde8;
        short kayitTuru;
        string guvenlikKod;
        string kaydeden;
        int kayitTarih;
        int kayitSaat;
        short kayitKaynak;
        string kayitSurum;
        string degistiren;
        int degisTarih;
        int degisSaat;
        short degisKaynak;
        string degisSurum;
        short checkSum;
        string aciklama;
        string kod1;
        string kod2;
        string kod3;
        string kod4;
        string kod5;
        string kod6;
        string kod7;
        string kod8;
        string kod9;
        string kod10;
        decimal kod11;
        decimal kod12;
        int? devirTarih;
        decimal? devirTutar;
        short onayTip;
        bool? satisMuduruOnay;
        bool? finansmanMuduruOnay;
        bool? genelMudurOnay;
        string onaylayanSatisMuduru;
        DateTime? onayTarihSatisMuduru;
        string onaylayanFinansmanMuduru;
        DateTime? onayTarihFinansmanMuduru;
        string onaylayanGenelMudur;
        DateTime? onayTarihGenelMudur;
        decimal? cekTutari;
        DateTime? cekOrtalamaVadeTarih;
        decimal? nakitTutari;
        string baglantiParaCinsi;
        bool aktifPasif;
        int? vadeTarihi;
        int? valorGun;
        int row_ID;
        byte[] _timestamp;
        string _pk_ListeNo;
        int _pk_BasTarih;
        short _pk_MusUygSekli;
        short _pk_SiraNo;
        #endregion /// Fields

        /// <summary> VARCHAR (16) PrimaryKey * </summary>
        public string ListeNo
        {
            get { return listeNo; }
            set
            {
                listeNo = value;
                OnPropertyChanged("ListeNo");
            }
        }

        /// <summary> VARCHAR (40) * </summary>
        public string ListeAdi
        {
            get { return listeAdi; }
            set
            {
                listeAdi = value;
                OnPropertyChanged("ListeAdi");
            }
        }

        /// <summary> INT (4) PrimaryKey * </summary>
        public int BasTarih
        {
            get { return basTarih; }
            set
            {
                basTarih = value;
                OnPropertyChanged("BasTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int BasSaat
        {
            get { return basSaat; }
            set
            {
                basSaat = value;
                OnPropertyChanged("BasSaat");
            }
        }

        /// <summary> INT (4) * </summary>
        public int BitTarih
        {
            get { return bitTarih; }
            set
            {
                bitTarih = value;
                OnPropertyChanged("BitTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int BitSaat
        {
            get { return bitSaat; }
            set
            {
                bitSaat = value;
                OnPropertyChanged("BitSaat");
            }
        }

        /// <summary> SMALLINT (2) PrimaryKey * </summary>
        public short MusUygSekli
        {
            get { return musUygSekli; }
            set
            {
                musUygSekli = value;
                OnPropertyChanged("MusUygSekli");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short MusKodGrup
        {
            get { return musKodGrup; }
            set
            {
                musKodGrup = value;
                OnPropertyChanged("MusKodGrup");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string MusteriKod
        {
            get { return musteriKod; }
            set
            {
                musteriKod = value;
                OnPropertyChanged("MusteriKod");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short MalUygSekli
        {
            get { return malUygSekli; }
            set
            {
                malUygSekli = value;
                OnPropertyChanged("MalUygSekli");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short MalKodGrup
        {
            get { return malKodGrup; }
            set
            {
                malKodGrup = value;
                OnPropertyChanged("MalKodGrup");
            }
        }

        /// <summary> VARCHAR (30) * </summary>
        public string MalKod
        {
            get { return malKod; }
            set
            {
                malKod = value;
                OnPropertyChanged("MalKod");
            }
        }

        /// <summary> SMALLINT (2) PrimaryKey * </summary>
        public short SiraNo
        {
            get { return siraNo; }
            set
            {
                siraNo = value;
                OnPropertyChanged("SiraNo");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float Oran
        {
            get { return oran; }
            set
            {
                oran = value;
                OnPropertyChanged("Oran");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float Oran1
        {
            get { return oran1; }
            set
            {
                oran1 = value;
                OnPropertyChanged("Oran1");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float Oran2
        {
            get { return oran2; }
            set
            {
                oran2 = value;
                OnPropertyChanged("Oran2");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float Oran3
        {
            get { return oran3; }
            set
            {
                oran3 = value;
                OnPropertyChanged("Oran3");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float Oran4
        {
            get { return oran4; }
            set
            {
                oran4 = value;
                OnPropertyChanged("Oran4");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float Oran5
        {
            get { return oran5; }
            set
            {
                oran5 = value;
                OnPropertyChanged("Oran5");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal MikAralik1
        {
            get { return mikAralik1; }
            set
            {
                mikAralik1 = value;
                OnPropertyChanged("MikAralik1");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float MikYuzde1
        {
            get { return mikYuzde1; }
            set
            {
                mikYuzde1 = value;
                OnPropertyChanged("MikYuzde1");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal MikAralik2
        {
            get { return mikAralik2; }
            set
            {
                mikAralik2 = value;
                OnPropertyChanged("MikAralik2");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float MikYuzde2
        {
            get { return mikYuzde2; }
            set
            {
                mikYuzde2 = value;
                OnPropertyChanged("MikYuzde2");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal MikAralik3
        {
            get { return mikAralik3; }
            set
            {
                mikAralik3 = value;
                OnPropertyChanged("MikAralik3");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float MikYuzde3
        {
            get { return mikYuzde3; }
            set
            {
                mikYuzde3 = value;
                OnPropertyChanged("MikYuzde3");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal MikAralik4
        {
            get { return mikAralik4; }
            set
            {
                mikAralik4 = value;
                OnPropertyChanged("MikAralik4");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float MikYuzde4
        {
            get { return mikYuzde4; }
            set
            {
                mikYuzde4 = value;
                OnPropertyChanged("MikYuzde4");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal MikAralik5
        {
            get { return mikAralik5; }
            set
            {
                mikAralik5 = value;
                OnPropertyChanged("MikAralik5");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float MikYuzde5
        {
            get { return mikYuzde5; }
            set
            {
                mikYuzde5 = value;
                OnPropertyChanged("MikYuzde5");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal MikAralik6
        {
            get { return mikAralik6; }
            set
            {
                mikAralik6 = value;
                OnPropertyChanged("MikAralik6");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float MikYuzde6
        {
            get { return mikYuzde6; }
            set
            {
                mikYuzde6 = value;
                OnPropertyChanged("MikYuzde6");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal MikAralik7
        {
            get { return mikAralik7; }
            set
            {
                mikAralik7 = value;
                OnPropertyChanged("MikAralik7");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float MikYuzde7
        {
            get { return mikYuzde7; }
            set
            {
                mikYuzde7 = value;
                OnPropertyChanged("MikYuzde7");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal MikAralik8
        {
            get { return mikAralik8; }
            set
            {
                mikAralik8 = value;
                OnPropertyChanged("MikAralik8");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float MikYuzde8
        {
            get { return mikYuzde8; }
            set
            {
                mikYuzde8 = value;
                OnPropertyChanged("MikYuzde8");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal TutarAralik1
        {
            get { return tutarAralik1; }
            set
            {
                tutarAralik1 = value;
                OnPropertyChanged("TutarAralik1");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float TutarYuzde1
        {
            get { return tutarYuzde1; }
            set
            {
                tutarYuzde1 = value;
                OnPropertyChanged("TutarYuzde1");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal TutarAralik2
        {
            get { return tutarAralik2; }
            set
            {
                tutarAralik2 = value;
                OnPropertyChanged("TutarAralik2");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float TutarYuzde2
        {
            get { return tutarYuzde2; }
            set
            {
                tutarYuzde2 = value;
                OnPropertyChanged("TutarYuzde2");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal TutarAralik3
        {
            get { return tutarAralik3; }
            set
            {
                tutarAralik3 = value;
                OnPropertyChanged("TutarAralik3");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float TutarYuzde3
        {
            get { return tutarYuzde3; }
            set
            {
                tutarYuzde3 = value;
                OnPropertyChanged("TutarYuzde3");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal TutarAralik4
        {
            get { return tutarAralik4; }
            set
            {
                tutarAralik4 = value;
                OnPropertyChanged("TutarAralik4");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float TutarYuzde4
        {
            get { return tutarYuzde4; }
            set
            {
                tutarYuzde4 = value;
                OnPropertyChanged("TutarYuzde4");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal TutarAralik5
        {
            get { return tutarAralik5; }
            set
            {
                tutarAralik5 = value;
                OnPropertyChanged("TutarAralik5");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float TutarYuzde5
        {
            get { return tutarYuzde5; }
            set
            {
                tutarYuzde5 = value;
                OnPropertyChanged("TutarYuzde5");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal TutarAralik6
        {
            get { return tutarAralik6; }
            set
            {
                tutarAralik6 = value;
                OnPropertyChanged("TutarAralik6");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float TutarYuzde6
        {
            get { return tutarYuzde6; }
            set
            {
                tutarYuzde6 = value;
                OnPropertyChanged("TutarYuzde6");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal TutarAralik7
        {
            get { return tutarAralik7; }
            set
            {
                tutarAralik7 = value;
                OnPropertyChanged("TutarAralik7");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float TutarYuzde7
        {
            get { return tutarYuzde7; }
            set
            {
                tutarYuzde7 = value;
                OnPropertyChanged("TutarYuzde7");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal TutarAralik8
        {
            get { return tutarAralik8; }
            set
            {
                tutarAralik8 = value;
                OnPropertyChanged("TutarAralik8");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float TutarYuzde8
        {
            get { return tutarYuzde8; }
            set
            {
                tutarYuzde8 = value;
                OnPropertyChanged("TutarYuzde8");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal OdemeAralik1
        {
            get { return odemeAralik1; }
            set
            {
                odemeAralik1 = value;
                OnPropertyChanged("OdemeAralik1");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float OdemeYuzde1
        {
            get { return odemeYuzde1; }
            set
            {
                odemeYuzde1 = value;
                OnPropertyChanged("OdemeYuzde1");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal OdemeAralik2
        {
            get { return odemeAralik2; }
            set
            {
                odemeAralik2 = value;
                OnPropertyChanged("OdemeAralik2");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float OdemeYuzde2
        {
            get { return odemeYuzde2; }
            set
            {
                odemeYuzde2 = value;
                OnPropertyChanged("OdemeYuzde2");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal OdemeAralik3
        {
            get { return odemeAralik3; }
            set
            {
                odemeAralik3 = value;
                OnPropertyChanged("OdemeAralik3");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float OdemeYuzde3
        {
            get { return odemeYuzde3; }
            set
            {
                odemeYuzde3 = value;
                OnPropertyChanged("OdemeYuzde3");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal OdemeAralik4
        {
            get { return odemeAralik4; }
            set
            {
                odemeAralik4 = value;
                OnPropertyChanged("OdemeAralik4");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float OdemeYuzde4
        {
            get { return odemeYuzde4; }
            set
            {
                odemeYuzde4 = value;
                OnPropertyChanged("OdemeYuzde4");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal OdemeAralik5
        {
            get { return odemeAralik5; }
            set
            {
                odemeAralik5 = value;
                OnPropertyChanged("OdemeAralik5");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float OdemeYuzde5
        {
            get { return odemeYuzde5; }
            set
            {
                odemeYuzde5 = value;
                OnPropertyChanged("OdemeYuzde5");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal OdemeAralik6
        {
            get { return odemeAralik6; }
            set
            {
                odemeAralik6 = value;
                OnPropertyChanged("OdemeAralik6");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float OdemeYuzde6
        {
            get { return odemeYuzde6; }
            set
            {
                odemeYuzde6 = value;
                OnPropertyChanged("OdemeYuzde6");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal OdemeAralik7
        {
            get { return odemeAralik7; }
            set
            {
                odemeAralik7 = value;
                OnPropertyChanged("OdemeAralik7");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float OdemeYuzde7
        {
            get { return odemeYuzde7; }
            set
            {
                odemeYuzde7 = value;
                OnPropertyChanged("OdemeYuzde7");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal OdemeAralik8
        {
            get { return odemeAralik8; }
            set
            {
                odemeAralik8 = value;
                OnPropertyChanged("OdemeAralik8");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float OdemeYuzde8
        {
            get { return odemeYuzde8; }
            set
            {
                odemeYuzde8 = value;
                OnPropertyChanged("OdemeYuzde8");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short KayitTuru
        {
            get { return kayitTuru; }
            set
            {
                kayitTuru = value;
                OnPropertyChanged("KayitTuru");
            }
        }

        /// <summary> VARCHAR (2) * </summary>
        public string GuvenlikKod
        {
            get { return guvenlikKod; }
            set
            {
                guvenlikKod = value;
                OnPropertyChanged("GuvenlikKod");
            }
        }

        /// <summary> VARCHAR (5) * </summary>
        public string Kaydeden
        {
            get { return kaydeden; }
            set
            {
                kaydeden = value;
                OnPropertyChanged("Kaydeden");
            }
        }

        /// <summary> INT (4) * </summary>
        public int KayitTarih
        {
            get { return kayitTarih; }
            set
            {
                kayitTarih = value;
                OnPropertyChanged("KayitTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int KayitSaat
        {
            get { return kayitSaat; }
            set
            {
                kayitSaat = value;
                OnPropertyChanged("KayitSaat");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short KayitKaynak
        {
            get { return kayitKaynak; }
            set
            {
                kayitKaynak = value;
                OnPropertyChanged("KayitKaynak");
            }
        }

        /// <summary> VARCHAR (12) * </summary>
        public string KayitSurum
        {
            get { return kayitSurum; }
            set
            {
                kayitSurum = value;
                OnPropertyChanged("KayitSurum");
            }
        }

        /// <summary> VARCHAR (5) * </summary>
        public string Degistiren
        {
            get { return degistiren; }
            set
            {
                degistiren = value;
                OnPropertyChanged("Degistiren");
            }
        }

        /// <summary> INT (4) * </summary>
        public int DegisTarih
        {
            get { return degisTarih; }
            set
            {
                degisTarih = value;
                OnPropertyChanged("DegisTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int DegisSaat
        {
            get { return degisSaat; }
            set
            {
                degisSaat = value;
                OnPropertyChanged("DegisSaat");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DegisKaynak
        {
            get { return degisKaynak; }
            set
            {
                degisKaynak = value;
                OnPropertyChanged("DegisKaynak");
            }
        }

        /// <summary> VARCHAR (12) * </summary>
        public string DegisSurum
        {
            get { return degisSurum; }
            set
            {
                degisSurum = value;
                OnPropertyChanged("DegisSurum");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short CheckSum
        {
            get { return checkSum; }
            set
            {
                checkSum = value;
                OnPropertyChanged("CheckSum");
            }
        }

        /// <summary> VARCHAR (64) * </summary>
        public string Aciklama
        {
            get { return aciklama; }
            set
            {
                aciklama = value;
                OnPropertyChanged("Aciklama");
            }
        }

        /// <summary> VARCHAR (12) * </summary>
        public string Kod1
        {
            get { return kod1; }
            set
            {
                kod1 = value;
                OnPropertyChanged("Kod1");
            }
        }

        /// <summary> VARCHAR (10) * </summary>
        public string Kod2
        {
            get { return kod2; }
            set
            {
                kod2 = value;
                OnPropertyChanged("Kod2");
            }
        }

        /// <summary> VARCHAR (10) * </summary>
        public string Kod3
        {
            get { return kod3; }
            set
            {
                kod3 = value;
                OnPropertyChanged("Kod3");
            }
        }

        /// <summary> VARCHAR (10) * </summary>
        public string Kod4
        {
            get { return kod4; }
            set
            {
                kod4 = value;
                OnPropertyChanged("Kod4");
            }
        }

        /// <summary> VARCHAR (10) * </summary>
        public string Kod5
        {
            get { return kod5; }
            set
            {
                kod5 = value;
                OnPropertyChanged("Kod5");
            }
        }

        /// <summary> VARCHAR (10) * </summary>
        public string Kod6
        {
            get { return kod6; }
            set
            {
                kod6 = value;
                OnPropertyChanged("Kod6");
            }
        }

        /// <summary> VARCHAR (10) * </summary>
        public string Kod7
        {
            get { return kod7; }
            set
            {
                kod7 = value;
                OnPropertyChanged("Kod7");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod8
        {
            get { return kod8; }
            set
            {
                kod8 = value;
                OnPropertyChanged("Kod8");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod9
        {
            get { return kod9; }
            set
            {
                kod9 = value;
                OnPropertyChanged("Kod9");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod10
        {
            get { return kod10; }
            set
            {
                kod10 = value;
                OnPropertyChanged("Kod10");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Kod11
        {
            get { return kod11; }
            set
            {
                kod11 = value;
                OnPropertyChanged("Kod11");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Kod12
        {
            get { return kod12; }
            set
            {
                kod12 = value;
                OnPropertyChanged("Kod12");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? DevirTarih
        {
            get { return devirTarih; }
            set
            {
                devirTarih = value;
                OnPropertyChanged("DevirTarih");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? DevirTutar
        {
            get { return devirTutar; }
            set
            {
                devirTutar = value;
                OnPropertyChanged("DevirTutar");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short OnayTip
        {
            get { return onayTip; }
            set
            {
                onayTip = value;
                OnPropertyChanged("OnayTip");
            }
        }

        /// <summary> BIT (1) Allow Null </summary>
        public bool? SatisMuduruOnay
        {
            get { return satisMuduruOnay; }
            set
            {
                satisMuduruOnay = value;
                OnPropertyChanged("SatisMuduruOnay");
            }
        }

        /// <summary> BIT (1) Allow Null </summary>
        public bool? FinansmanMuduruOnay
        {
            get { return finansmanMuduruOnay; }
            set
            {
                finansmanMuduruOnay = value;
                OnPropertyChanged("FinansmanMuduruOnay");
            }
        }

        /// <summary> BIT (1) Allow Null </summary>
        public bool? GenelMudurOnay
        {
            get { return genelMudurOnay; }
            set
            {
                genelMudurOnay = value;
                OnPropertyChanged("GenelMudurOnay");
            }
        }

        /// <summary> VARCHAR (15) Allow Null </summary>
        public string OnaylayanSatisMuduru
        {
            get { return onaylayanSatisMuduru; }
            set
            {
                onaylayanSatisMuduru = value;
                OnPropertyChanged("OnaylayanSatisMuduru");
            }
        }

        /// <summary> SMALLDATETIME (4) Allow Null </summary>
        public DateTime? OnayTarihSatisMuduru
        {
            get { return onayTarihSatisMuduru; }
            set
            {
                onayTarihSatisMuduru = value;
                OnPropertyChanged("OnayTarihSatisMuduru");
            }
        }

        /// <summary> VARCHAR (15) Allow Null </summary>
        public string OnaylayanFinansmanMuduru
        {
            get { return onaylayanFinansmanMuduru; }
            set
            {
                onaylayanFinansmanMuduru = value;
                OnPropertyChanged("OnaylayanFinansmanMuduru");
            }
        }

        /// <summary> SMALLDATETIME (4) Allow Null </summary>
        public DateTime? OnayTarihFinansmanMuduru
        {
            get { return onayTarihFinansmanMuduru; }
            set
            {
                onayTarihFinansmanMuduru = value;
                OnPropertyChanged("OnayTarihFinansmanMuduru");
            }
        }

        /// <summary> VARCHAR (15) Allow Null </summary>
        public string OnaylayanGenelMudur
        {
            get { return onaylayanGenelMudur; }
            set
            {
                onaylayanGenelMudur = value;
                OnPropertyChanged("OnaylayanGenelMudur");
            }
        }

        /// <summary> SMALLDATETIME (4) Allow Null </summary>
        public DateTime? OnayTarihGenelMudur
        {
            get { return onayTarihGenelMudur; }
            set
            {
                onayTarihGenelMudur = value;
                OnPropertyChanged("OnayTarihGenelMudur");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CekTutari
        {
            get { return cekTutari; }
            set
            {
                cekTutari = value;
                OnPropertyChanged("CekTutari");
            }
        }

        /// <summary> SMALLDATETIME (4) Allow Null </summary>
        public DateTime? CekOrtalamaVadeTarih
        {
            get { return cekOrtalamaVadeTarih; }
            set
            {
                cekOrtalamaVadeTarih = value;
                OnPropertyChanged("CekOrtalamaVadeTarih");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? NakitTutari
        {
            get { return nakitTutari; }
            set
            {
                nakitTutari = value;
                OnPropertyChanged("NakitTutari");
            }
        }

        /// <summary> VARCHAR (4) Allow Null </summary>
        public string BaglantiParaCinsi
        {
            get { return baglantiParaCinsi; }
            set
            {
                baglantiParaCinsi = value;
                OnPropertyChanged("BaglantiParaCinsi");
            }
        }

        /// <summary> BIT (1) * </summary>
        public bool AktifPasif
        {
            get { return aktifPasif; }
            set
            {
                aktifPasif = value;
                OnPropertyChanged("AktifPasif");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? VadeTarihi
        {
            get { return vadeTarihi; }
            set
            {
                vadeTarihi = value;
                OnPropertyChanged("VadeTarihi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? ValorGun
        {
            get { return valorGun; }
            set
            {
                valorGun = value;
                OnPropertyChanged("ValorGun");
            }
        }

        /// <summary> INT (4) IdentityKey * </summary>
        public int Row_ID => row_ID;

        /// <summary> TIMESTAMP (8) * </summary>
        public byte[] timestamp
        {
            get { return _timestamp; }
            set
            {
                _timestamp = value;
                OnPropertyChanged("timestamp");
            }
        }

        /// <summary> VARCHAR (16) PRIMARY KEY * </summary>
        public string pk_ListeNo
        {
            private get { return _pk_ListeNo; }
            set
            {
                _pk_ListeNo = value;
                OnPropertyChanged("pk_ListeNo");
            }
        }

        /// <summary> INT (4) PRIMARY KEY * </summary>
        public int pk_BasTarih
        {
            private get { return _pk_BasTarih; }
            set
            {
                _pk_BasTarih = value;
                OnPropertyChanged("pk_BasTarih");
            }
        }

        /// <summary> SMALLINT (2) PRIMARY KEY * </summary>
        public short pk_MusUygSekli
        {
            private get { return _pk_MusUygSekli; }
            set
            {
                _pk_MusUygSekli = value;
                OnPropertyChanged("pk_MusUygSekli");
            }
        }

        /// <summary> SMALLINT (2) PRIMARY KEY * </summary>
        public short pk_SiraNo
        {
            private get { return _pk_SiraNo; }
            set
            {
                _pk_SiraNo = value;
                OnPropertyChanged("pk_SiraNo");
            }
        }
        #endregion /// Properties       

        #region Tablo Bilgileri & Metodlar

        List<string> WhereList = new List<string>();
        List<string> SetList = new List<string>();
        string info_FullTableName = "FINSAT6{0}.FINSAT6{0}.ISS_Temp";
        string[] info_PrimaryKeys = { "pk_ListeNo", "pk_BasTarih", "pk_MusUygSekli", "pk_SiraNo" };
        string[] info_IdentityKeys = { "Row_ID" };

        List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public ISS_Temp()
        {
            ChangedProperties = new List<string>();
            PropertyChanged += ISS_Temp_PropertyChanged;
        }

        public void AcceptChanges() => ChangedProperties.Clear();

        void ISS_Temp_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
        #endregion  /// Tablo Bilgileri & Metodlar
    }
    #endregion /// ISS_Temp Class
}