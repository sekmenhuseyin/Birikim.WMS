using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m
{
    public class Charts
    {
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
    }
}
