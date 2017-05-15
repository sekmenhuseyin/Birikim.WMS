using System;
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
            return View("Login");
        }
        /// <summary>
        /// giriş işlemleri
        /// </summary>
        [HttpPost]
        public ActionResult Login(User P, string RememberMe)
        {
            Persons _Person = new Persons();
            _Result = new Result();
            try
            {             
                if (string.IsNullOrEmpty(P.Kod) || string.IsNullOrEmpty(P.Sifre)){}
                else
                {
                    _Result = _Person.Login(P);
                    if (_Result.Id > 0)
                        Authentication.CreateAuth((User)_Result.Data, RememberMe == "on" ? true : false);
                }                
            }
            catch (Exception ex){
                using (var db = new WMSEntities())
                {
                    db.Logger(P.Kod, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Security/Login");
                }
                return null;
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