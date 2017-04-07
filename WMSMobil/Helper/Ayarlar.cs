using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WMSMobil.WMSLocal;

namespace WMSMobil
{

    public enum MenuType
    {
        None = 0, MalKabul = 1, RafaYerlestirme = 2, SiparisToplama = 3, KabloSiparis = 4, Paketle = 6, Sevkiyat = 7, KontrollüSayım = 8
    }

    public class Ayarlar
    {
        static string sirketKodu;
        public static string SirketKodu
        {
            get { return Ayarlar.sirketKodu; }
            set { Ayarlar.sirketKodu = value; }
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
                else
                    Ayarlar.menuTip = MenuType.KontrollüSayım;
            }
        }

        static Login kullanici;
        public static Login Kullanici
        {
            get { return Ayarlar.kullanici; }
            set { Ayarlar.kullanici = value; }
        }

        static int tarih;
        public static int Tarih
        {
            get { return Ayarlar.tarih; }
            set { Ayarlar.tarih = value; }
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

        static List<Kullanicilar> gGorevliler;
        public static List<Kullanicilar> Gorevliler
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
