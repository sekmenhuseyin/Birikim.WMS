using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m
{
    public class Charts
    {
        private string Veritabani { get; set; }
        /// <summary>
        /// constructor
        /// </summary>
        public Charts(string veritabani = "FINSAT")
        {
            Veritabani = veritabani;
        }
        /// <summary>
        /// Home / Index
        /// </summary>
        public BekleyenOnaylar BekleyenOnaylar(string SirketKodu, bool DashboardKasa)
        {
            using (var db = new WMSEntities())
            {
                var sonuc = db.Database.SqlQuery<BekleyenOnaylar>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenOnaylar]", SirketKodu)).FirstOrDefault();
                if (DashboardKasa == true)
                {
                    sonuc.MevcudBanka = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudBanka]", SirketKodu)).FirstOrDefault();
                    sonuc.MevcudCek = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudCek]", SirketKodu)).FirstOrDefault();
                    sonuc.MevcudKasa = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudKasa]", SirketKodu)).FirstOrDefault();
                    sonuc.MevcudPOS = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudPOS]", SirketKodu)).FirstOrDefault();
                }
                return sonuc;
            }
        }
        /// <summary>
        /// Home / PartialGunlukSatisZamanCizelgesi
        /// </summary>
        public List<ChartBaglantiZaman> ChartBaglantiZaman(string SirketKodu)
        {
            using (var db = new WMSEntities())
                return db.Database.SqlQuery<ChartBaglantiZaman>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisGetir]", SirketKodu)).ToList();
        }
        /// <summary>
        /// Home / PartialGunlukSatis
        /// </summary>
        public List<ChartGunlukSatisAnalizi> ChartGunlukSatisAnalizi(string SirketKodu, int tarih)
        {
            using (var db = new WMSEntities())
                return db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnalizi] @Tarih = {1}", SirketKodu, tarih)).ToList();
        }
        /// <summary>
        /// Home / PartialGunlukSatisYearToDay
        /// </summary>
        public List<GetCachedChartYear2Day_Result> GetCachedChartYear2Day_Result(string SirketKodu)
        {
            using (var db = new WMSEntities())
                return db.Database.SqlQuery<GetCachedChartYear2Day_Result>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziYearToDay]", SirketKodu)).ToList();
        }
        /// <summary>
        /// Home / PartialGunlukSatisDoubleKriter
        /// </summary>
        public List<ChartGunlukSatisAnalizi> ChartGunlukSatisAnalizi(string SirketKodu, string kod, int islemtip, int tarih)
        {
            using (var db = new WMSEntities())
                return db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziDoubleKriter] @Tarih = {1}, @IslemTip = {2}, @Grup = '{3}'", SirketKodu, tarih, islemtip, kod)).ToList();
        }
        /// <summary>
        /// Home / PartialAylikSatisAnaliziBar
        /// </summary>
        public List<GetCachedChartMonthly_Result> GetCachedChartMonthly_Result(string SirketKodu)
        {
            using (var db = new WMSEntities())
                return db.Database.SqlQuery<GetCachedChartMonthly_Result>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi]", SirketKodu)).ToList();
        }
    }
}
