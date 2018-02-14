using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class InboxController : RootController
    {
        /// <summary>
        /// gelen kutusu
        /// </summary>
        public ActionResult Index() => View("Index");

        /// <summary>
        /// gelen bildirimler
        /// </summary>
        public ActionResult Notifications()
        {
            var tip = ComboItems.DuyuruMesajı.ToInt32();
            var tbl = db.Messages.Where(m => m.MesajTipi == tip && m.Kime == vUser.UserName).OrderByDescending(m => m.Tarih).ToList();
            foreach (var item in tbl)
                item.Okundu = true;
            db.SaveChanges();
            return View("Notifications", tbl);
        }
    }
}