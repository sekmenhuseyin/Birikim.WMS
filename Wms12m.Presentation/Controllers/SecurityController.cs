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
        // GET: Security
        Result _Result;
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User P)
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
                        Authentication.CreateAuth((User)_Result.Data, false);
                }                
            }
            catch (Exception){
                return null;
            }
            return Json(new { data = (_Result.Status) }, JsonRequestBehavior.AllowGet);
        }
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
    }
}