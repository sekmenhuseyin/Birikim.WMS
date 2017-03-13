﻿using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using Wms12m.Business;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Presentation.Controllers
{
    public class RootController : Controller
    {
        public WMSEntities db = new WMSEntities();
        public Combo Combo = new Combo();
        public ComboSub ComboSub = new ComboSub();
        public Corridor Corridor = new Corridor();
        public Dimension Dimension = new Dimension();
        public Floor Floor = new Floor();
        public Irsaliye Irsaliye = new Irsaliye();
        public Persons Persons = new Persons();
        public Section Section = new Section();
        public Shelf Shelf = new Shelf();
        public Stok Stok = new Stok();
        public Store Store = new Store();
        public Task Task = new Task();
        /// <summary>
        /// user için kısayol
        /// </summary>
        protected virtual new UserIdentity User
        {
            get
            {
                var u = HttpContext.User as CustomPrincipal;
                if (u != null)
                    return u.AppIdentity.User;
                return null;
            }
        }
        /// <summary>
        /// actiona olmadan hemen önce
        /// </summary>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (User == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Security",
                    action = "Login",
                    status = (int)HttpStatusCode.NotImplemented //Security Attribute koyulmalı!!!
                }));
                return;
            }
            ViewBag.User = User.FirstName + " " + User.LastName;
            base.OnActionExecuting(filterContext);
        }
        /// <summary>
        /// actiondan sonra
        /// </summary>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
        /// <summary>
        /// dispose override
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                Combo.Dispose();
                ComboSub.Dispose();
                Corridor.Dispose();
                Dimension.Dispose();
                Floor.Dispose();
                Irsaliye.Dispose();
                Persons.Dispose();
                Section.Dispose();
                Shelf.Dispose();
                Stok.Dispose();
                Store.Dispose();
                Task.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}