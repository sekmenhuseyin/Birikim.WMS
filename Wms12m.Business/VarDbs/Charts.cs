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
            db = dbContext;
            SirketKodu = sirketKodu;
        }

        private WMSEntities db { get; set; }
        private string SirketKodu { get; set; }
        private string Veritabani { get; set; }

        /// <summary>
        /// Home / Index
        /// </summary>
        public BekleyenOnaylar BekleyenOnaylar(bool dashboardKasa)
        {
            var sonuc = db.Database.SqlQuery<BekleyenOnaylar>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenOnaylar]", SirketKodu)).FirstOrDefault();
            if (dashboardKasa == true)
            {
                sonuc.MevcudBanka = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudBanka]", SirketKodu)).FirstOrDefault();
                sonuc.MevcudCek = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudCek]", SirketKodu)).FirstOrDefault();
                sonuc.MevcudKasa = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudKasa]", SirketKodu)).FirstOrDefault();
                sonuc.MevcudPOS = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudPOS]", SirketKodu)).FirstOrDefault();
            }

            return sonuc;
        }

        /// <summary>
        /// Home / PartialLokasyonSatis
        /// </summary>
        public List<DB_LokasyonBazli_SatisAnalizi> CachedChartLocation(int tarih)
        {
            return db.Database.SqlQuery<DB_LokasyonBazli_SatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_LokasyonBazli_SatisAnalizi] @Ay = {1}", SirketKodu, tarih)).ToList();
        }

        /// <summary>
        /// Home / PartialLokasyonSatisKriter
        /// </summary>
        public List<DB_LokasyonBazli_SatisAnalizi_Kriter> CachedChartLocationKriter(int tarih, string kriter)
        {
            return db.Database.SqlQuery<DB_LokasyonBazli_SatisAnalizi_Kriter>(string.Format("[FINSAT6{0}].[wms].[DB_LokasyonBazli_SatisAnalizi_Kriter] @Ay = {1}, @Kriter='{2}'", SirketKodu, tarih, kriter)).ToList();
        }

        /// <summary>
        /// Home / PartialAylikSatisAnaliziBar
        /// </summary>
        public List<DB_Aylik_SatisAnalizi> CachedChartMonthly()
        {
            return db.Database.SqlQuery<DB_Aylik_SatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi]", SirketKodu)).ToList();
        }

        /// <summary>
        /// Home / PartialAylikSatisAnaliziKodTipDovizBar
        /// </summary>
        public List<DB_Aylik_SatisAnalizi_Tip_Kod_Doviz> CachedChartMonthlyByKriter(string kod, int islemtip, string doviz)
        {
            return db.Database.SqlQuery<DB_Aylik_SatisAnalizi_Tip_Kod_Doviz>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi_Tip_Kod_Doviz] @Grup = '{1}', @Kriter = '{2}', @IslemTip = {3}", SirketKodu, kod, doviz, islemtip)).ToList();
        }

        /// <summary>
        /// Home / PartialUrunGrubuSatis
        /// </summary>
        public List<DB_UrunGrubu_SatisAnalizi> CachedChartUrunGrubu(int tarih)
        {
            return db.Database.SqlQuery<DB_UrunGrubu_SatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_UrunGrubu_SatisAnalizi] @Ay = {1}", SirketKodu, tarih)).ToList();
        }

        /// <summary>
        /// Home / PartialUrunGrubuSatisKriter
        /// </summary>
        public List<DB_UrunGrubu_SatisAnalizi_Kriter> CachedChartUrunGrubuKriter(int tarih, string kriter)
        {
            return db.Database.SqlQuery<DB_UrunGrubu_SatisAnalizi_Kriter>(string.Format("[FINSAT6{0}].[wms].[DB_UrunGrubu_SatisAnalizi_Kriter] @Ay = {1}, @Kriter='{2}'", SirketKodu, tarih, kriter)).ToList();
        }

        /// <summary>
        /// Home / PartialGunlukSatisYearToDay
        /// </summary>
        public List<DB_GunlukSatisAnaliziYearToDay> CachedChartYear2Day()
        {
            return db.Database.SqlQuery<DB_GunlukSatisAnaliziYearToDay>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziYearToDay]", SirketKodu)).ToList();
        }

        /// <summary>
        /// Home / PartialAylikSatisCHKAnaliziBar
        /// </summary>
        public List<ChartAylikSatisAnalizi> ChartAylikSatisAnalizi(string chk)
        {
            return db.Database.SqlQuery<ChartAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi_CHK] @chk='{1}'", SirketKodu, chk)).ToList();
        }

        /// <summary>
        /// Home / PartialGunlukSatisZamanCizelgesi
        /// </summary>
        public List<ChartBaglantiZaman> ChartBaglantiZaman()
        {
            return db.Database.SqlQuery<ChartBaglantiZaman>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisGetir]", SirketKodu)).ToList();
        }

        /// <summary>
        /// Home / PartialGunlukSatis
        /// </summary>
        public List<ChartGunlukSatisAnalizi> ChartGunlukSatisAnalizi(int tarih)
        {
            return db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnalizi] @Tarih = {1}", SirketKodu, tarih)).ToList();
        }

        /// <summary>
        /// Home / PartialGunlukSatisDoubleKriter
        /// </summary>
        public List<ChartGunlukSatisAnalizi> ChartGunlukSatisAnalizi(string kod, int islemtip, int tarih)
        {
            return db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziDoubleKriter] @Tarih = {1}, @IslemTip = {2}, @Grup = '{3}'", SirketKodu, tarih, islemtip, kod)).ToList();
        }
    }
}