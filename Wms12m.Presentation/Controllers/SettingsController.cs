﻿using Birikim.Models;
using System;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class SettingsController : RootController
    {
        /// <summary>
        /// ayarlar
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.Menü, PermTypes.Reading) == false) return Redirect("/");
            return View("Index", ViewBag.settings);
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
                var set = ViewBag.settings;
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
                set.GorevProjesi = tbl.GorevProjesi;
                set.OnayStok = tbl.OnayStok;
                set.OnayTeminat = tbl.OnayTeminat;
                set.OnaySiparis = tbl.OnaySiparis;
                set.OnaySozlesme = tbl.OnaySozlesme;
                set.OnayRisk = tbl.OnayRisk;
                set.OnayFiyat = tbl.OnayFiyat;
                set.OnayCek = tbl.OnayCek;
                set.OnayTekno = tbl.OnayTekno;
                set.SevkiyatVarmi = tbl.SevkiyatVarmi;
                set.GitServerAddress = tbl.GitServerAddress;
                set.SiparisOnayParametre = tbl.SiparisOnayParametre;
                set.BolgeKoduParametre = tbl.BolgeKoduParametre;
                set.CrmOzet = tbl.CrmOzet;
                set.Aktif = tbl.Aktif;
                db.SaveChanges();
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        /// <summary>
        /// ayarlar
        /// </summary>
        public ActionResult Sql()
        {
            if (CheckPerm(Perms.Menü, PermTypes.Reading) == false) return Redirect("/");
            return View("Sql");
        }

        /// <summary>
        /// stok malzeme sil
        /// </summary>
        public JsonResult RunSql(string Sql)
        {
            if (CheckPerm(Perms.Menü, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            int r;
            try
            {
                r = db.Database.ExecuteSqlCommand(Sql);
            }
            catch (Exception ex)
            {
                Logger(ex, "System/Settings/RunSql");
                return Json(new Result(false, ex.Message), JsonRequestBehavior.AllowGet);
            }

            return Json(new Result(true, r.ToString()), JsonRequestBehavior.AllowGet);
        }
    }
}