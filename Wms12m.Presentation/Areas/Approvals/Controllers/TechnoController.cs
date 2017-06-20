﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class TechnoController : RootController
    {
        // GET: Approvals/Techno
        public ActionResult Index()
        {
            if (vUser.UserName.ToString() == "murat")
            {
                MyGlobalVariables.Birim = "GM";
            }
            else if (vUser.UserName.ToString() == "orhan")
            {
                MyGlobalVariables.Birim = "SPGMY";
            }
            else if (vUser.UserName.ToString() == "kenan")
            {
                MyGlobalVariables.Birim = "MIGMY";
            }
            else if (vUser.UserName.ToString() == "tunce")
            {
                MyGlobalVariables.Birim = "USGMY";
            }
            else if (vUser.UserName.ToString() == "ariza")
            {
                MyGlobalVariables.Birim = "ARTGMY";
            }
            else if (vUser.UserName.ToString() == "berke")
            {
                MyGlobalVariables.Birim = "IK";
            }
            ViewBag.Birim = MyGlobalVariables.Birim;
            return View();
        }

        public PartialViewResult Ucret_List(string Tip)
        {
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Reading) == false) return null;
            ViewBag.Tip = Tip;
            return PartialView();
        }

        public PartialViewResult Prim_List(string Tip)
        {
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Reading) == false) return null;
            ViewBag.Tip = Tip;
            return PartialView();
        }

        public PartialViewResult EskiUcretData(string PERSONELID)
        {
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Reading) == false) return null;
            var list = db.Database.SqlQuery<TechnoList>(string.Format("[HR0312M].[dbo].[TCH_EskiUcretSelect] {0}", PERSONELID)).ToList();
            return PartialView(list);
        }

        public string UcretListData(string tip)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            List<TechnoList> ucretBilgi;
            try
            {

                ucretBilgi = db.Database.SqlQuery<TechnoList>(string.Format("[HR0312M].[dbo].[TCH_UcretOnaySelect] @Birim='{1}', @Tip='{0}'", tip, MyGlobalVariables.Birim)).ToList();
            }
            catch (Exception ex)
            {
                ucretBilgi = new List<Entity.TechnoList>();
            }
            return json.Serialize(ucretBilgi);
        }
        public string PrimListData(string tip)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            List<TechnoList> primBilgi;
            try
            {
                primBilgi = db.Database.SqlQuery<TechnoList>(string.Format("[HR0312M].[dbo].[TCH_PrimOnaySelect] @Birim='{1}', @Tip='{0}'", tip, MyGlobalVariables.Birim)).ToList();
            }
            catch (Exception ex)
            {
                primBilgi = new List<Entity.TechnoList>();
            }
            return json.Serialize(primBilgi);
        }



        public JsonResult Ucret_Onayla(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (JObject insertObj in parameters)
                    {
                        var ID = insertObj["ID"];
                        var OnayDerece = 1;
                        string s = string.Format("[HR0312M].[dbo].[TCH_UcretOnayUpdate] @OnayDerece={0}, @ID={1},@Birim='{2}'", OnayDerece, ID, MyGlobalVariables.Birim);
                        var x = db.Database.SqlQuery<int>(s).ToList();
                        LogActions("Approvals", "Techno", "Ucret_Onayla", ComboItems.alOnayla, ID.ToInt32(), insertObj["Ad"].ToString() + ' ' + insertObj["Soyad"].ToString() + "'ın ücret değişimi onaylandı.");
                        if (MyGlobalVariables.Birim == "GM")
                        {
                            string ss = string.Format("[HR0312M].[dbo].[TCH_BUTUCRETINSERT] @ID={0}", ID);
                            var xx = db.Database.SqlQuery<int>(ss).ToList();
                            LogActions("Approvals", "Techno", "Ucret_Onayla", ComboItems.alEkle, ID.ToInt32(), "PERSONELID=" + insertObj["PERSONELID"].ToString() + "olan kayıt BUTUCRET tablosuna eklendi");
                        }



                    }
                    _Result.Status = true;
                    _Result.Message = "İşlem Başarılı ";

                }
                catch (Exception ex)
                {
                    _Result.Status = false;
                    _Result.Message = "Hata Oluştu. ";

                }

                try
                {
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    _Result.Status = false;
                    _Result.Message = "Hata Oluştu. ";
                    return Json(_Result, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Prim_Onayla(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (JObject insertObj in parameters)
                    {
                        var ID = insertObj["ID"];
                        var OnayDerece = 1;
                        string s = string.Format("[HR0312M].[dbo].[TCH_PrimOnayUpdate] @OnayDerece={0}, @ID={1},@Birim='{2}'", OnayDerece, ID, MyGlobalVariables.Birim);
                        var x = db.Database.SqlQuery<int>(s).ToList();
                        LogActions("Approvals", "Techno", "Prim_Onayla", ComboItems.alEkle, ID.ToInt32(), insertObj["Ad"].ToString() + ' ' + insertObj["Soyad"].ToString() + "'ın prim değişimi onaylandı.");
                        if (MyGlobalVariables.Birim == "GM")
                        {
                            string ss = string.Format("[HR0312M].[dbo].[TCH_BRDSKALAINSERT] @ID={0}", ID);
                            var xx = db.Database.SqlQuery<int>(ss).ToList();
                            LogActions("Approvals", "Techno", "Prim_Onayla", ComboItems.alOnayla, ID.ToInt32(), "PERSONELID=" + insertObj["PERSONELID"].ToString() + "olan kayıt BRDSKALA tablosuna eklendi");
                        }



                    }
                    _Result.Status = true;
                    _Result.Message = "İşlem Başarılı ";

                }
                catch (Exception ex)
                {
                    _Result.Status = false;
                    _Result.Message = "Hata Oluştu. ";

                }
                try
                {
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    _Result.Status = false;
                    _Result.Message = "Hata Oluştu. ";
                    return Json(_Result, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Ucret_Reddet(string Data, string RedNeden)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (JObject insertObj in parameters)
                    {
                        var ID = insertObj["ID"];

                        string s = string.Format("[HR0312M].[dbo].[TCH_UcretRedUpdate] @ID={0},@RedNeden='{1}'", ID, RedNeden);
                        var x = db.Database.SqlQuery<int>(s).ToList();
                        LogActions("Approvals", "Techno", "Ucret_Reddet", ComboItems.alRed, ID.ToInt32(), insertObj["Ad"].ToString() + ' ' + insertObj["Soyad"].ToString() + "'ın ücret değişimi reddedildi.");
                    }
                    _Result.Status = true;
                    _Result.Message = "İşlem Başarılı ";

                }
                catch (Exception ex)
                {
                    _Result.Status = false;
                    _Result.Message = "Hata Oluştu. ";

                }
                try
                {
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    _Result.Status = false;
                    _Result.Message = "Hata Oluştu. ";
                    return Json(_Result, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Prim_Reddet(string Data, string RedNeden)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (JObject insertObj in parameters)
                    {
                        var ID = insertObj["ID"];
                        string s = string.Format("[HR0312M].[dbo].[TCH_PrimRedUpdate] @ID={0},@RedNeden='{1}'", ID, RedNeden);
                        var x = db.Database.SqlQuery<int>(s).ToList();
                        LogActions("Approvals", "Techno", "Prim_Reddet", ComboItems.alRed, ID.ToInt32(), insertObj["Ad"].ToString() + ' ' + insertObj["Soyad"].ToString() + "'ın prim değişimi reddedildi.");
                    }
                    _Result.Status = true;
                    _Result.Message = "İşlem Başarılı ";

                }
                catch (Exception ex)
                {
                    _Result.Status = false;
                    _Result.Message = "Hata Oluştu. ";

                }
                try
                {
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    _Result.Status = false;
                    _Result.Message = "Hata Oluştu. ";
                    return Json(_Result, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}