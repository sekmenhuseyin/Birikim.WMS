using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class SecurityController : Controller
    {
        /// <summary>
        /// giriş sayfası
        /// </summary>
        private Result _result;

        public ActionResult Login()
        {
            using (var db = new WMSEntities())
            {
                ViewBag.settings = db.Settings.FirstOrDefault();
                var sirkets = db.GetSirkets().ToList();
                ViewBag.SirketKodu = new SelectList(sirkets, "Kod", "Ad");
                ViewBag.sayi = sirkets.Count;
                return View("Login");
            }
        }

        /// <summary>
        /// giriş işlemleri
        /// </summary>
        [HttpPost]
        public JsonResult Login(User p, string RememberMe, string SirketKodu = "")
        {
            _result = new Result();
            var fn = new Functions();
            using (var db = new WMSEntities())
            {
                using (var person = new Persons())
                    try
                    {
                        if (string.IsNullOrEmpty(p.Kod) || string.IsNullOrEmpty(p.Sifre)) { }
                        else
                        {
                            _result = person.Login(p, fn.GetIPAddress());
                            if (_result.Id > 0)
                            {
                                var sirket = SirketKodu == "" ? db.GetSirkets().FirstOrDefault() : db.GetSirkets().FirstOrDefault(m => m.Kod == SirketKodu);
                                Authentication.CreateAuth((User)_result.Data, RememberMe == "1", sirket);
                            }
                            else
                                db.LogLogins(p.Kod, fn.GetIPAddress(), false, _result.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        var inner = "";
                        if (ex.InnerException != null)
                        {
                            inner = ex.InnerException?.Message;
                            if (ex.InnerException.InnerException != null) inner += ": " + ex.InnerException.InnerException.Message;
                        }

                        db.Logger(p.Kod, "", fn.GetIPAddress(), ex.Message, inner, "Security/Login");
                        db.LogLogins(p.Kod, fn.GetIPAddress(), false, ex.Message);
                        return null;
                    }
            }

            return Json(new { data = (_result.Status) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// çıkış işlemleri
        /// </summary>
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Session.Abandon();
            HttpContext.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, "")
            {
                Expires = DateTime.Now.AddYears(-1)
            });
            HttpContext.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", "")
            {
                Expires = DateTime.Now.AddYears(-1)
            });
            return RedirectToAction("Login", "Security");
        }

        /// <summary>
        /// yeni kullanıcı kaydı
        /// </summary>
        public JsonResult New()
        {
            return Json(new Result(false, 0, "Şu anda bu işlemi gerçekleştiremiyoruz.", "Yeni Kullanıcı"), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// şifre hatırlatma
        /// </summary>
        public JsonResult Forgot()
        {
            return Json(new Result(false, 0, "Şu anda bu işlemi gerçekleştiremiyoruz.", "Şifre Hatırlatma"), JsonRequestBehavior.AllowGet);
        }
    }
}