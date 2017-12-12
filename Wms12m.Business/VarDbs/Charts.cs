using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m
{
    public class Charts
    {
        string Veritabani { get; set; }
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
        public BekleyenOnaylar BekleyenOnaylar(string sirketKodu, bool dashboardKasa)
        {
            using (var db = new WMSEntities())
            {
                var sonuc = db.Database.SqlQuery<BekleyenOnaylar>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenOnaylar]", sirketKodu)).FirstOrDefault();
                if (dashboardKasa == true)
                {
                    sonuc.MevcudBanka = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudBanka]", sirketKodu)).FirstOrDefault();
                    sonuc.MevcudCek = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudCek]", sirketKodu)).FirstOrDefault();
                    sonuc.MevcudKasa = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudKasa]", sirketKodu)).FirstOrDefault();
                    sonuc.MevcudPOS = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudPOS]", sirketKodu)).FirstOrDefault();
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
        /// <summary>
        /// Home / PartialAylikSatisCHKAnaliziBar
        /// </summary>
        public List<ChartAylikSatisAnalizi> ChartAylikSatisAnalizi(string SirketKodu, string chk)
        {
            using (var db = new WMSEntities())
                return db.Database.SqlQuery<ChartAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi_CHK] @chk='{1}'", SirketKodu, chk)).ToList();
        }
        /// <summary>
        /// Home / PartialAylikSatisAnaliziKodTipDovizBar
        /// </summary>
        public List<GetCachedChartMonthlyByKriter_Result> GetCachedChartMonthlyByKriter_Result(string SirketKodu, string kod, int islemtip, string doviz)
        {
            using (var db = new WMSEntities())
                return db.Database.SqlQuery<GetCachedChartMonthlyByKriter_Result>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi_Tip_Kod_Doviz] @Grup = '{1}', @Kriter = '{2}', @IslemTip = {3}", SirketKodu, kod, doviz, islemtip)).ToList();
        }
        /// <summary>
        /// Home / PartialUrunGrubuSatis
        /// </summary>
        public List<GetCachedChartUrunGrubu_Result> GetCachedChartUrunGrubu_Result(string SirketKodu, short tarih)
        {
            using (var db = new WMSEntities())
                return db.Database.SqlQuery<GetCachedChartUrunGrubu_Result>(string.Format("[FINSAT6{0}].[wms].[DB_UrunGrubu_SatisAnalizi] @Ay = {1}", SirketKodu, tarih)).ToList();
        }
        /// <summary>
        /// Home / PartialUrunGrubuSatisKriter
        /// </summary>
        public List<GetCachedChartUrunGrubuKriter_Result> GetCachedChartUrunGrubuKriter_Result(string SirketKodu, short tarih, string kriter)
        {
            using (var db = new WMSEntities())
                return db.Database.SqlQuery<GetCachedChartUrunGrubuKriter_Result>(string.Format("[FINSAT6{0}].[wms].[DB_UrunGrubu_SatisAnalizi_Kriter] @Ay = {1}, @Kriter='{2}'", SirketKodu, tarih, kriter)).ToList();
        }
        /// <summary>
        /// Home / PartialLokasyonSatis
        /// </summary>
        public List<GetCachedChartLocation_Result> GetCachedChartLocation_Result(string SirketKodu, short tarih)
        {
            using (var db = new WMSEntities())
                return db.Database.SqlQuery<GetCachedChartLocation_Result>(string.Format("[FINSAT6{0}].[wms].[DB_LokasyonBazli_SatisAnalizi] @Ay = {1}", SirketKodu, tarih)).ToList();
        }
        /// <summary>
        /// Home / PartialLokasyonSatisKriter
        /// </summary>
        public List<GetCachedChartLocationKriter_Result> GetCachedChartLocationKriter_Result(string SirketKodu, int tarih, string kriter)
        {
            using (var db = new WMSEntities())
                return db.Database.SqlQuery<GetCachedChartLocationKriter_Result>(string.Format("[FINSAT6{0}].[wms].[DB_LokasyonBazli_SatisAnalizi_Kriter] @Ay = {1}, @Kriter='{2}'", SirketKodu, tarih, kriter)).ToList();
        }
    }
}