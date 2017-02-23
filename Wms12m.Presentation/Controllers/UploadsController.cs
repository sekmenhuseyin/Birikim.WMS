using System;
using System.Collections.Generic;
using System.Data;
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
                var fileName = Path.GetFileName("ImportData.xml");
                var path = Path.Combine(Server.MapPath("~/Content/Uploads/"), fileName);
                file.SaveAs(path);


                DataSet xmlVeri = XmlVerileriGetir(path);
               


            }
            return Json(true, JsonRequestBehavior.AllowGet);
            //return Json(true, JsonRequestBehavior.AllowGet);
        }
        public DataSet XmlVerileriGetir(string xmlDosya)
        {
            DataSet ds = new DataSet();
            // xlm Dosya varmı onun kontrolü.
            if (System.IO.File.Exists(xmlDosya))
            {
                // varsa Dosyayı oku ve dataset ‘ e aktar.
                ds.ReadXml(xmlDosya);
            }
            return ds;
        }
    }
}