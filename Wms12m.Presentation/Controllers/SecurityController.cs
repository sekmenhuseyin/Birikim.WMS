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
        Result _Result;
        public ActionResult Login()
        {
            using (var db = new WMSEntities())
            {
                ViewBag.settings = db.Settings.FirstOrDefault();
            }
            return View("Login");
        }
        /// <summary>
        /// giriş işlemleri
        /// </summary>
        [HttpPost]
        public JsonResult Login(User P, string RememberMe)
        {
            Persons _Person = new Persons();
            _Result = new Result();
            var fn = new Functions();
            using (var db = new WMSEntities())
            {
                try
                {
                    if (string.IsNullOrEmpty(P.Kod) || string.IsNullOrEmpty(P.Sifre)) { }
                    else
                    {
                        _Result = _Person.Login(P);
                        if (_Result.Id > 0)
                        {
                            Authentication.CreateAuth((User)_Result.Data, RememberMe == "1" ? true : false);
                            db.LogLogins(P.Kod, fn.GetIPAddress(), true, "");
                        }
                        else
                            db.LogLogins(P.Kod, fn.GetIPAddress(), false, _Result.Message);
                    }
                }
                catch (Exception ex)
                {
                    string inner = "";
                    if (ex.InnerException != null)
                    {
                        inner = ex.InnerException == null ? "" : ex.InnerException.Message;
                        if (ex.InnerException.InnerException != null) inner += ": " + ex.InnerException.InnerException.Message;
                    }
                    db.Logger(P.Kod, "", fn.GetIPAddress(), ex.Message, inner, "Security/Login");
                    db.LogLogins(P.Kod, fn.GetIPAddress(), false, ex.Message);
                    return null;
                }
            }
            return Json(new { data = (_Result.Status) }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// çıkış işlemleri
        /// </summary>
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Session.Abandon();
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            authCookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Response.Cookies.Add(authCookie);
            HttpCookie sessionCookie = new HttpCookie("ASP.NET_SessionId", "");
            sessionCookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Response.Cookies.Add(sessionCookie);
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