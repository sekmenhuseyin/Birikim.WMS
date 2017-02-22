using System;
using System.Web;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;
using Wms12m.Configuration;

namespace Wms12m.Business
{
    public class Gorev
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// yeni ekle
        /// </summary>
        public Result Insert(frmGorev tbl)
        {
            UserIdentity User= Users.AppIdentity.User;
            //add görevlist table
            GorevListesi gorev = new GorevListesi();
            gorev.DepoID = tbl.DepoID;
            gorev.GorevNo = DateTime.Today.ToString("ddMMyy") + "-1";
            gorev.GorevTipiID = Enm.ComboNames.MalKabul.ToInt32();
            gorev.DurumID = Enm.ComboNames.Açık.ToInt32();
            gorev.OlusturanID = User.Id;
            gorev.OlusturmaTarihi = Convert.ToInt32(DateTime.Today.ToOADate());
            gorev.IrsaliyeID = tbl.IrsaliyeID;
            gorev.Bilgi = "IrsNo: " + tbl.IrsaliyeID.ToString();
            db.GorevListesis.Add(gorev);
            db.SaveChanges();
            return _Result;
        }
        /// <summary>
        /// Güncelle
        /// </summary>
        public Result Update(frmGorev tbl)
        {
            var tmp = db.GorevListesis.Where(m => m.ID == tbl.ID).FirstOrDefault();
            if (tmp != null)
            {
                tmp.GorevliID = tbl.GorevliID;
                tmp.Aciklama = tbl.Aciklama;
                tmp.Bilgi = tbl.Bilgi;
                tmp.DurumID = tbl.DurumID;
                db.SaveChanges();
            }
            return _Result;
        }
    }
}
