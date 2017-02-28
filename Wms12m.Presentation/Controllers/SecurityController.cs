using System;
using System.Web;
using Wms12m.Entity;
using System.Web.Mvc;
using Wms12m.Business;
using System.Web.Security;
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
        public ActionResult Login(USR01 P)
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
                        Authentication.CreateAuth((USR01)_Result.Data, false);
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