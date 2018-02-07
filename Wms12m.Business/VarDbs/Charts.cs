using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m
{
    public class Charts
    {
        /// <summary>
        /// constructor
        /// </summary>
        public Charts(WMSEntities dbContext, string sirketKodu, string veritabani = "FINSAT")
        {
            Veritabani = veritabani;
            Db = dbContext;
            SirketKodu = sirketKodu;
        }

        private WMSEntities Db { get; set; }
        private string SirketKodu { get; set; }
        private string Veritabani { get; set; }

        /// <summary>
        /// Home / Index
        /// </summary>
        public BekleyenOnaylar BekleyenOnaylar(bool dashboardKasa)
        {
            var sonuc = Db.Database.SqlQuery<BekleyenOnaylar>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenOnaylar]", SirketKodu)).FirstOrDefault();
            if (dashboardKasa == true)
            {
                sonuc.MevcudBanka = Db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudBanka]", SirketKodu)).FirstOrDefault();
                sonuc.MevcudCek = Db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudCek]", SirketKodu)).FirstOrDefault();
                sonuc.MevcudKasa = Db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudKasa]", SirketKodu)).FirstOrDefault();
                sonuc.MevcudPOS = Db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudPOS]", SirketKodu)).FirstOrDefault();
            }

            return sonuc;
        }

        /// <summary>
        /// Home / LokasyonSatis
        /// </summary>
        public List<DB_LokasyonBazli_SatisAnalizi> ChartLocation(int tarih)
        {
            return Db.Database.SqlQuery<DB_LokasyonBazli_SatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_LokasyonBazli_SatisAnalizi] @Ay = {1}", SirketKodu, tarih)).ToList();
        }

        /// <summary>
        /// Home / LokasyonSatisKriter
        /// </summary>
        public List<DB_LokasyonBazli_SatisAnalizi_Kriter> ChartLocationKriter(int tarih, string kriter)
        {
            return Db.Database.SqlQuery<DB_LokasyonBazli_SatisAnalizi_Kriter>(string.Format("[FINSAT6{0}].[wms].[DB_LokasyonBazli_SatisAnalizi_Kriter] @Ay = {1}, @Kriter='{2}'", SirketKodu, tarih, kriter)).ToList();
        }

        /// <summary>
        /// Home / AylikSatisAnaliziBar
        /// </summary>
        public List<DB_Aylik_SatisAnalizi> ChartMonthly()
        {
            return Db.Database.SqlQuery<DB_Aylik_SatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi]", SirketKodu)).ToList();
        }

        /// <summary>
        /// Home / AylikSatisAnaliziKodTipDovizBar
        /// </summary>
        public List<DB_Aylik_SatisAnalizi_Tip_Kod_Doviz> ChartMonthlyByKriter(string kod, int islemtip, string doviz)
        {
            return Db.Database.SqlQuery<DB_Aylik_SatisAnalizi_Tip_Kod_Doviz>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi_Tip_Kod_Doviz] @Grup = '{1}', @Kriter = '{2}', @IslemTip = {3}", SirketKodu, kod, doviz, islemtip)).ToList();
        }

        /// <summary>
        /// Home / UrunGrubuSatis
        /// </summary>
        public List<DB_UrunGrubu_SatisAnalizi> ChartUrunGrubu(int tarih)
        {
            return Db.Database.SqlQuery<DB_UrunGrubu_SatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_UrunGrubu_SatisAnalizi] @Ay = {1}", SirketKodu, tarih)).ToList();
        }

        /// <summary>
        /// Home / UrunGrubuSatisKriter
        /// </summary>
        public List<DB_UrunGrubu_SatisAnalizi_Kriter> ChartUrunGrubuKriter(int tarih, string kriter)
        {
            return Db.Database.SqlQuery<DB_UrunGrubu_SatisAnalizi_Kriter>(string.Format("[FINSAT6{0}].[wms].[DB_UrunGrubu_SatisAnalizi_Kriter] @Ay = {1}, @Kriter='{2}'", SirketKodu, tarih, kriter)).ToList();
        }

        /// <summary>
        /// Home / GunlukSatisYearToDay
        /// </summary>
        public List<DB_GunlukSatisAnaliziYearToDay> ChartYear2Day()
        {
            return Db.Database.SqlQuery<DB_GunlukSatisAnaliziYearToDay>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziYearToDay]", SirketKodu)).ToList();
        }

        /// <summary>
        /// Home / AylikSatisCHKAnaliziBar
        /// </summary>
        public List<ChartAylikSatisAnalizi> ChartAylikSatisAnalizi(string chk)
        {
            return Db.Database.SqlQuery<ChartAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi_CHK] @chk='{1}'", SirketKodu, chk)).ToList();
        }

        /// <summary>
        /// Home / GunlukSatisZamanCizelgesi
        /// </summary>
        public List<ChartBaglantiZaman> ChartGunlukZaman()
        {
            return Db.Database.SqlQuery<ChartBaglantiZaman>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisGetir]", SirketKodu)).ToList();
        }

        /// <summary>
        /// Home / BaglantiZamanCizelgesi
        /// </summary>
        public List<ChartBaglantiZaman> ChartBaglantiZaman()
        {
            return Db.Database.SqlQuery<ChartBaglantiZaman>(string.Format("[FINSAT6{0}].[wms].[DB_BaglantiLogGetir]", SirketKodu)).ToList();
        }

        /// <summary>
        /// Home / GunlukSatis
        /// </summary>
        public List<ChartGunlukSatisAnalizi> ChartGunlukSatisAnalizi(int tarih)
        {
            return Db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnalizi] @Tarih = {1}", SirketKodu, tarih)).ToList();
        }

        /// <summary>
        /// Home / GunlukSatisDoubleKriter
        /// </summary>
        public List<ChartGunlukSatisAnalizi> ChartGunlukSatisAnalizi(string kod, int islemtip, int tarih)
        {
            return Db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziDoubleKriter] @Tarih = {1}, @IslemTip = {2}, @Grup = '{3}'", SirketKodu, tarih, islemtip, kod)).ToList();
        }

        /// <summary>
        /// Home / GunlukMDFUretimi
        /// </summary>
        public List<ChartGunlukMDFUretimi> ChartGunlukMDFUretimi(int tarih)
        {
            return Db.Database.SqlQuery<ChartGunlukMDFUretimi>(string.Format("[FINSAT6{0}].[wms].[MDF_UretimRapor_Chart] @BasTarih = {1}, @BitTarih = {2}, @Tip={3}", SirketKodu, tarih, tarih, 1)).ToList();
        }

        /// <summary>
        /// Home / BolgeBazliSatisAnalizi
        /// </summary>
        public List<ChartBolgeBazliSatisAnalizi> ChartBolgeBazliSatisAnalizi(int ay, string kriter)
        {
            return Db.Database.SqlQuery<ChartBolgeBazliSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Bolge_Bazinda_SatisAnalizi] @Ay = {1}, @Kriter = '{2}'", SirketKodu, ay, kriter)).ToList();
        }
    }
}