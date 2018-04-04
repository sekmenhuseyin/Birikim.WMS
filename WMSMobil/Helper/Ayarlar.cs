using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WMSMobil.TerminalService;

namespace WMSMobil
{
    public enum MenuType
    {
        None = 0, MalKabul = 1, RafaYerlestirme = 2, SiparisToplama = 3, KabloSiparis = 4, Paketle = 6, Sevkiyat = 7, KontrollüSayım = 8, TransferÇıkış = 19, TransferGiriş = 20, Alımdanİade = 72, Satıştanİade = 73
    }

    public class Ayarlar
    {
        public static string AuthCode = "RJ0QdX5V5kWDkjTNR2oWmQdtoxbu2bdbWX4RLI6acw4P/LRTIjKzqaJlx1v76NJw9ngbH1bBDu65EqhU4He0IU2lfI05B8WhXJkMnhlZUNvO5IxKU8fSztlp7uziL6W27/q7v5lV0fXRB0f+RdNzXQIgqMheHhqNbp3z3Vf4VRI=";
        public static decimal KatSayi;

        static string sirketKodu;
        public static string SirketKodu
        {
            get { return Ayarlar.sirketKodu; }
            set { Ayarlar.sirketKodu = value; }
        }

        static string servisURL;
        public static string ServisURL
        {
            get { return Ayarlar.servisURL; }
            set { Ayarlar.servisURL = value; }
        }

        static MenuType menuTip;
        public static MenuType MenuTip
        {
            get { return Ayarlar.menuTip; }
            set { Ayarlar.menuTip = value; }
        }
        public static int MenuTipSet
        {
            set 
            {
                if (value == 1)
                    Ayarlar.menuTip = MenuType.MalKabul;
                else if (value == 2)
                    Ayarlar.menuTip = MenuType.RafaYerlestirme;
                else if (value == 3)
                    Ayarlar.menuTip = MenuType.SiparisToplama;
                else if (value == 4)
                    Ayarlar.menuTip = MenuType.KabloSiparis;
                else if (value == 6)
                    Ayarlar.menuTip = MenuType.Paketle;
                else if (value == 7)
                    Ayarlar.menuTip = MenuType.Sevkiyat;
                else if (value == 8)
                    Ayarlar.menuTip = MenuType.KontrollüSayım;
                else if (value == 19)
                    Ayarlar.menuTip = MenuType.TransferÇıkış;
                else if (value == 72)
                    Ayarlar.menuTip = MenuType.Alımdanİade;
                else if (value == 73)
                    Ayarlar.menuTip = MenuType.Satıştanİade;
                else// if (value == 20)
                    Ayarlar.menuTip = MenuType.TransferGiriş;
            }
        }

        static Login kullanici;
        public static Login Kullanici
        {
            get { return Ayarlar.kullanici; }
            set { Ayarlar.kullanici = value; }
        }

        static int seciliSatırID;
        public static int SeciliSatırID
        {
            get { return Ayarlar.seciliSatırID; }
            set { Ayarlar.seciliSatırID = value; }
        }

        static List<Tip_STI> gSTIKalemler;
        public static List<Tip_STI> STIKalemler
        {
            get { return Ayarlar.gSTIKalemler; }
            set { Ayarlar.gSTIKalemler = value; }
        }

        static List<Tip_GOREV> gGorevler;
        public static List<Tip_GOREV> Gorevler
        {
            get { return Ayarlar.gGorevler; }
            set { Ayarlar.gGorevler = value; }
        }

        static Tip_IRS gSeciliGorev;
        public static Tip_IRS SeciliGorev
        {
            get { return Ayarlar.gSeciliGorev; }
            set { Ayarlar.gSeciliGorev = value; }
        }

        static List<GetGorevlis_Result> gGorevliler;
        public static List<GetGorevlis_Result> Gorevliler
        {
            get { return Ayarlar.gGorevliler; }
            set { Ayarlar.gGorevliler = value; }
        }

        static List<Durum> gGorevDurumlari;
        public static List<Durum> GorevDurumlari
        {
            get { return Ayarlar.gGorevDurumlari; }
            set { Ayarlar.gGorevDurumlari = value; }
        }


    }
}
