﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.WMS.Controllers
{
    public class OrderController : RootController
    {
        public ActionResult Index()
        {
            if (CheckPerm(Perms.MalKabul, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "DepoKodu", "DepoAd");
            return View();
        }

        public JsonResult MusteriSecimi()
        {
            var sql = string.Format(@"SELECT HesapKodu, Unvan1+''+Unvan2 as 'Müşteri Adı',Kod2 ,(DvrB+OdemeB+CiroB+IadeFatB+KDVB+DigerB-DvrA-OdemeA-CiroA-IadeFatA-KDVA-DigerA) as Bakiye
                FROM FINSAT6{0}.FINSAT6{0}.CHK (nolock)
                WHERE GuvenlikKod<>1 and AktifPasif=0 and KartTip in (0,4)  order by Unvan1 asc", vUser.SirketKodu);
            // return
            try
            {
                var list = db.Database.SqlQuery<frmJson>(sql).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "WMS/Order/MusteriSecimi");
                return Json(new List<frmJson>(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetKDVOran(string MalKodu)
        {
            var sql = string.Format("select KDVOran from FINSAT6{0}.FINSAT6{0}.STK where MalKodu = '{1}'", vUser.SirketKodu, MalKodu);
            var KDVOran = db.Database.SqlQuery<Single>(sql).FirstOrDefault();
            return Json(KDVOran, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStock(string MalKodu, string DepoKodu)
        {
            var sql = string.Format(@"SELECT FINSAT6{0}.[wms].[getStockByDepo] ('{1}','{2}')", vUser.SirketKodu, MalKodu, DepoKodu);
            var result = db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}