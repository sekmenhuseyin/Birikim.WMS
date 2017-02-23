using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wms12m.Presentation.Controllers
{
    public class UploadsController : Controller
    {
        // GET: Uploads
        public JsonResult Irsaliye(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Uploads/"), fileName);
                file.SaveAs(path);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}