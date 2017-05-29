using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace TakvimeBagliKisit
{

    /* •————————————————————————————————————————————————————————————————————————————————————————————————•
       |                                                                                             |
       |                                                                                             |
       |     TBK             SDK             SIR         Açıklama                                    |
       |     SirketKodu      SirketKod       Kod         Dönemleri kapsayan Genel Kod                |
       |                     Kod                         Bildiğimiz Şirket Kodu                      |
       |     DönemKodu                                   Bildiğimiz Şirket Kodu                      |
       |     DonemTarihi                                 Şirketin Dönemi-Şirketin Başlangıç TArihi   |
       |     DönemTipi                                   0: FINSAT; 1 MUHASEBE; 2 DEMİRBAŞ; 3 Üretim |
       |     KisıtTipi                                   0: Değişiklik serbest                       |
       |                                                 1: x gün öncesinden bugüne kadar serbest    |
       |                                                 2: İki Tarih aralığı serbest                |
       |                                                 3: Hiç Değişiklik Yok                       |
       |                                                 4: Yeni Kayıt Girişine izin Verme           |
     * •—————————————————————————————————————————————————————————————————————————————————————————• */

    /* •—————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
       | SirKod değişkeni hakkında                                                                                                               |
       | SIR Tablosunda ki Genel Şirket Kodu: SIR.Kod=SDK.SirketKod=TBK.SirketKodu                                                               |
       | Aslında kullanılması gereksiz. DonemKodu yani SirketKodu yeterli                                                                        |
       | Fakat bizim testserverda TBK tablosu karışık olduğundan biz deki denemelerde where koşuluna yazmak gerekti                              |
       | Ayndı DonemKodu(SirketKodu) Farklı SirketKodlarında (SIR.Kod) yer almış. (TBK Tablosunda- SIR tablosunda kod yok) Böyle bir şey olamaz. |
       | Müşteri TBK tablosu düzgün olacağından SirKod kullanılmayabilir.                                                                        |
       •—————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————• */
    public enum MesajTip
    {
        None = 1,
        Mesaj = 2,
        ThrowException = 3
    }

    public class TBKIslem
    {
        private string ConStr { get; set; }
        private string SirketKodu { get; set; }
        private string SirKod { get; set; }
        public TBKIslem(string conStr, string sirketKodu)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(conStr);
            builder.InitialCatalog = "SOLAR6";

            ConStr = builder.ConnectionString;
            SirketKodu = sirketKodu;
        }

        public TBKIslem(string conStr, string sirketKodu, string sirKod)
            : this(conStr, sirketKodu)
        {
            SirKod = sirKod;
        }


        public bool IzinFINSAT(DateTime evrakTarih, bool yeniKayit = false, MesajTip mtip = MesajTip.None)
        {
            return IzinFINSAT(Convert.ToInt32(evrakTarih.Date.ToOADate()), yeniKayit, mtip);
        }

        public bool IzinFINSAT(int evrakTarih, bool yeniKayit = false, MesajTip mtip = MesajTip.None)
        {
            return Izin(evrakTarih, TBKDonemTipi.FINSAT, yeniKayit, mtip);
        }

        public bool IzinMUHASEBE(DateTime evrakTarih, bool yeniKayit = false, MesajTip mtip = MesajTip.None)
        {
            return IzinMUHASEBE(Convert.ToInt32(evrakTarih.Date.ToOADate()), yeniKayit, mtip);
        }

        public bool IzinMUHASEBE(int evrakTarih, bool yeniKayit = false, MesajTip mtip = MesajTip.None)
        {
            return Izin(evrakTarih, TBKDonemTipi.MUHASEBE, yeniKayit, mtip);
        }



        private bool Izin(int evrakTarih, TBKDonemTipi donemTipi, bool yeniKayit = false, MesajTip mtip = MesajTip.None)
        {
            bool sonuc = true;
            using (TBKDataContext ent = new TBKDataContext(ConStr))
            {
                TBK tbk = ent.TBKs.Where(t => t.KullaniciKodu.Contains("ZZZZZ") && t.EvrakTipi == -1 //&& t.KisitTipi == 2
                    && (string.IsNullOrWhiteSpace(SirKod) ? true : t.SirketKodu == SirKod)
                        && t.DonemKodu == SirketKodu && t.DonemTipi == (short)donemTipi).FirstOrDefault();

                if (tbk != null)
                {
                    if (tbk.KisitTipi == (short)TBKKisitTipi.DegisiklikSerbest)
                        sonuc = true;
                    else if (tbk.KisitTipi == (short)TBKKisitTipi.GunOncesindenBuguneKadarSerbest)
                    {
                        int bastarih = Convert.ToInt32(DateTime.Today.AddDays(-tbk.Gun).ToOADate());
                        sonuc = evrakTarih >= bastarih;
                    }
                    else if (tbk.KisitTipi == (short)TBKKisitTipi.TarihlerArasiSerbest)
                        sonuc = (evrakTarih >= tbk.BasTarih && evrakTarih <= tbk.BitTarih);
                    else if (tbk.KisitTipi == (short)TBKKisitTipi.HicDegisiklikYok)
                        sonuc = false;
                    else if (tbk.KisitTipi == (short)TBKKisitTipi.YeniKayitGirisineIzinVerme)
                    {
                        if (yeniKayit)
                            sonuc = false;
                    }
                    else
                        sonuc = false;
                }
            }

            if (sonuc == false)
            {
                if (mtip == MesajTip.Mesaj)
                    MessageBox.Show(HataMesaji((short)donemTipi)
                    , "UYARI"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Warning);
                else if (mtip == MesajTip.ThrowException)
                    throw new TBKException((short)donemTipi);
            }

            return sonuc;
        }

        internal static string DonemTipiAck(short donemTipi)
        {
            if (donemTipi == (short)TBKDonemTipi.FINSAT)
                return "FINSAT";
            else if (donemTipi == (short)TBKDonemTipi.MUHASEBE)
                return "MUHASEBE";
            else if (donemTipi == (short)TBKDonemTipi.DEMIRBAS)
                return "DEMİRBAŞ";
            else if (donemTipi == (short)TBKDonemTipi.URETIM)
                return "ÜRETİM";
            return "";
        }

        internal static string HataMesaji(short donemTipi)
        {
            return string.Format("Takvime Bağlı Kısıtlama kurallarından dolayı işlem devam edemez!!!\n(DB:{0})"
                    , DonemTipiAck((short)donemTipi));
        }

        //public List<TBK> TumTBKListesi()
        //{
        //    List<TBK> list;
        //    using (TBKDataContext ent = new TBKDataContext(ConStr))
        //    {
        //        list = ent.TBKs.Where(t => t.KullaniciKodu.Contains("ZZZZZ") && t.KisitTipi == 2).ToList();
        //    }
        //    return list;
        //}
        
    }



    enum TBKDonemTipi
    {
        FINSAT = 0,
        MUHASEBE = 1,
        DEMIRBAS = 2,
        URETIM = 3
    }

    enum TBKKisitTipi
    {
        DegisiklikSerbest = 0,
        GunOncesindenBuguneKadarSerbest = 1,
        TarihlerArasiSerbest = 2,
        HicDegisiklikYok = 3,
        YeniKayitGirisineIzinVerme = 4
    }

    /// <summary>
    /// public yapılmadı. Public yapmak istersek linq to sql classınnda ( TBK.Dbml) açıp public yapmak gerekir
    /// </summary>
    partial class TBK
    {
        public DateTime DonemTarihi_D
        {
            get { return new DateTime(1900, 1, 1).AddDays(DonemTarihi - 2); }
        }
        
        public DateTime BasTarih_D
        {
            get { return new DateTime(1900, 1, 1).AddDays(BasTarih - 2); }
        }

        public DateTime BitTarih_D
        {
            get { return new DateTime(1900, 1, 1).AddDays(BitTarih - 2); }
        }

        public string DonemTipiAck 
        {
            get 
            {
                return TBKIslem.DonemTipiAck(DonemTipi);
                //if (DonemTipi == (short)TBKDonemTipi.FINSAT)
                //    return "FINSAT";
                //else if (DonemTipi == (short)TBKDonemTipi.MUHASEBE)
                //    return "MUHASEBE";
                //else if (DonemTipi == (short)TBKDonemTipi.DEMIRBAS)
                //    return "DEMİRBAŞ";
                //else if (DonemTipi == (short)TBKDonemTipi.URETIM)
                //    return "ÜRETİM";
                //return "";
            }
        }

        public string KisitTipiAck
        {
            get
            {
                if (KisitTipi == (short)TBKKisitTipi.DegisiklikSerbest)
                    return "Değişiklik Serbest";
                else if (KisitTipi == (short)TBKKisitTipi.GunOncesindenBuguneKadarSerbest)
                    return "x Gün Öncesinden Bugüne Kadar Serbest";
                else if (KisitTipi == (short)TBKKisitTipi.TarihlerArasiSerbest)
                    return "Tarihler Arası Serbest";
                else if (KisitTipi == (short)TBKKisitTipi.HicDegisiklikYok)
                    return "Hiç Değişiklik Yok";
                else if (KisitTipi == (short)TBKKisitTipi.YeniKayitGirisineIzinVerme)
                    return "Yeni Kayıt Girişine İzin Verme";
                return "";
            }
        }

    }
}
