using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Wms12m.Business;
using Wms12m.Entity;

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
        public ActionResult Login(USER01 P)
        {
            AbstractPersons _Person = new Persons();
            _Result = new Result();
            try
            {             
                if (string.IsNullOrEmpty(P.UserName) || string.IsNullOrEmpty(P.Password)){}
                else
                {
                    _Result = _Person.Login(P);
                    if (_Result.Id > 0)
                        Authentication.CreateAuth((USER01)_Result.Data, false);
                }                
            }
            catch (Exception){}
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