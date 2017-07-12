using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.System.Controllers
{
    public class SettingsController : RootController
    {
        /// <summary>
        /// ayarlar
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.Menü, PermTypes.Reading) == false) return Redirect("/");
            return View("Index", db.Settings.FirstOrDefault());
        }
        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(Setting tbl)
        {
            if (CheckPerm(Perms.Menü, PermTypes.Writing) == false) return Redirect("/");
            if (ModelState.IsValid)
            {
                var set = db.Settings.FirstOrDefault();
                set.SiteName = tbl.SiteName;
                set.LoginLogo = tbl.LoginLogo;
                set.TopLogo = tbl.TopLogo;
                set.AllowNewUser = tbl.AllowNewUser;
                set.AllowForgotPass = tbl.AllowForgotPass;
                set.KabloSiparisMySql = tbl.KabloSiparisMySql;
                set.SmtpEmail = tbl.SmtpEmail;
                set.SmtpPass = tbl.SmtpPass;
                set.SmtpPort = tbl.SmtpPort;
                set.SmtpHost = tbl.SmtpHost;
                set.SmtpSSL = tbl.SmtpSSL;
                set.homeDepo = tbl.homeDepo;
                set.homeUser = tbl.homeUser;
                set.homeTask = tbl.homeTask;
                set.homeTransfer = tbl.homeTransfer;
                set.OnayStok = tbl.OnayStok;
                set.OnayTeminat = tbl.OnayTeminat;
                set.OnaySiparis = tbl.OnaySiparis;
                set.OnaySozlesme = tbl.OnaySozlesme;
                set.OnayRisk = tbl.OnayRisk;
                set.OnayFiyat = tbl.OnayFiyat;
                set.OnayCek = tbl.OnayCek;
                set.OnayTekno = tbl.OnayTekno;
                set.SevkiyatVarmi = tbl.SevkiyatVarmi;
                set.MaliYil = tbl.MaliYil;
                set.MaliYil1 = tbl.MaliYil1;
                set.MaliYil2 = tbl.MaliYil2;
                set.MaliYilDb = tbl.MaliYilDb;
                set.MaliYilDb1 = tbl.MaliYilDb1;
                set.MaliYilDb2 = tbl.MaliYilDb2;
                set.Version = tbl.Version;
                set.Aktif = tbl.Aktif;
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}