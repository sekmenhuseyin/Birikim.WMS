using Newtonsoft.Json;
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
        public ActionResult Index(string onayRed)
        {
            if (CheckPerm(Perms.TechnoIKOnaylama, PermTypes.Reading) == false) return Redirect("/");
            MyGlobalVariables.Birim = vUser.RoleName;
            if (onayRed == null)
            {
                ViewBag.OnayDurum = "OnayBekleyenler";
            }
            else
            {
                ViewBag.OnayDurum = onayRed;
            }
            ViewBag.Birim = MyGlobalVariables.Birim;
            return View();
        }

        public PartialViewResult Ucret_List(string Tip)
        {
            if (CheckPerm(Perms.TechnoIKOnaylama, PermTypes.Reading) == false) return null;
            ViewBag.Tip = Tip;
            return PartialView();
        }

        public PartialViewResult Prim_List(string Tip)
        {
            if (CheckPerm(Perms.TechnoIKOnaylama, PermTypes.Reading) == false) return null;
            ViewBag.Tip = Tip;
            return PartialView();
        }

        public PartialViewResult EskiUcretData(string PERSONELID)
        {
            if (CheckPerm(Perms.TechnoIKOnaylama, PermTypes.Reading) == false) return null;
            var list = db.Database.SqlQuery<TechnoList>(string.Format("[HR03V01].[wms].[TCH_EskiUcretSelect] {0}", PERSONELID)).ToList();
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
                ucretBilgi = db.Database.SqlQuery<TechnoList>(string.Format("[HR03V01].[wms].[TCH_UcretOnaySelect] @Birim='{1}', @Tip='{0}'", tip, MyGlobalVariables.Birim)).ToList();
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
                primBilgi = db.Database.SqlQuery<TechnoList>(string.Format("[HR03V01].[wms].[TCH_PrimOnaySelect] @Birim='{1}', @Tip='{0}'", tip, MyGlobalVariables.Birim)).ToList();
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
            if (CheckPerm(Perms.TechnoIKOnaylama, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            int[] birimID = { 4263, 2211, 2214, 4063, 4864, 2213, 6163, 6164, 6165, 6166, 6167, 6168, 6169, 6170, 2363, 5364, 5764, 4764, 55555 };
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (JObject insertObj in parameters)
                    {
                        var ID = insertObj["ID"];
                        var OnayDerece = 2;
                        var logDetay = "";
                        var logDetay2 = "";
                        if (MyGlobalVariables.Birim == "GM")
                        {
                            OnayDerece = 3;
                            logDetay = "BUTUCRET_Temp tablosu ID: " + ID + " ,DPERSONELUID: " + insertObj["PERSONELID"].ToString() + " – İlgili satırdaki  " + insertObj["Ad"].ToString() + ' ' + insertObj["Soyad"].ToString() + " isimli kullanıcının ücret bilgisi GM(" + vUser.UserName.ToString() + ") tarafından " + DateTime.Now + " da onaylandı.";
                        }
                        else if (MyGlobalVariables.Birim == "IKGM")
                        {
                            foreach (var item in birimID)
                            {
                                if (insertObj["DBIRIMID"].ToInt32() == item)
                                {
                                    OnayDerece = 1;
                                }
                            }
                            logDetay = "BUTUCRET_Temp tablosu ID: " + ID + " ,DPERSONELUID: " + insertObj["PERSONELID"].ToString() + " – İlgili satırdaki ücret bilgisi " + vUser.UserName.ToString() + " kullanıcısı tarafından GMY onayına düşürülmüştür.";
                        }
                        else
                        {
                            OnayDerece = 2;
                            if (insertObj["IslemTipi"].ToShort() == 0)
                            {
                                logDetay = "BUTUCRET_Temp tablosu ID: " + ID + " ,DPERSONELUID: " + insertObj["PERSONELID"].ToString() + " – İlgili satırdaki ücret bilgisi " + vUser.UserName.ToString() + " kullanıcısı tarafından GM(Murat) onayına düşürülmüştür.";
                            }
                            else
                            {
                                logDetay = "BUTUCRET_Temp tablosu ID: " + ID + " ,DPERSONELUID: " + insertObj["PERSONELID"].ToString() + " – İlgili satırdaki ücret değişimi " + vUser.UserName.ToString() + " kullanıcısı tarafından GM(Murat) onayına düşürülmüştür.";
                            }
                        }
                        string s = string.Format("[HR03V01].[wms].[TCH_UcretOnayUpdate] @OnayDerece={0}, @ID={1},@Birim='{2}'", OnayDerece, ID, MyGlobalVariables.Birim);
                        var x = db.Database.SqlQuery<int>(s).ToList();
                        LogActions("Approvals", "Techno", "Ucret_Onayla", ComboItems.alOnayla, ID.ToInt32(), logDetay);
                        if (MyGlobalVariables.Birim == "GM")
                        {
                            logDetay2 = "BUTUCRET_Temp tablosu ID: " + ID + " ,DPERSONELUID: " + insertObj["PERSONELID"].ToString() + " – İlgili satırdaki  " + insertObj["Ad"].ToString() + ' ' + insertObj["Soyad"].ToString() + " isimli kullanıcının ücret bilgisi onay sürecinden geçmiştir, sisteme delete insert atılmıştır.";
                            db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [HR03V01].[wms].[BUTUCRET] WHERE DBUTUCRETID={0} ", insertObj["DBUTUCRETID"].ToInt32()));
                            string ss = string.Format("[HR03V01].[wms].[TCH_BUTUCRETINSERT] @ID={0}", ID);
                            var xx = db.Database.SqlQuery<int>(ss).ToList();
                            LogActions("Approvals", "Techno", "Ucret_Onayla", ComboItems.alEkle, ID.ToInt32(), logDetay2);
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
            if (CheckPerm(Perms.TechnoIKOnaylama, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = new Result(true);
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            int[] birimID = { 4263, 2211, 2214, 4063, 4864, 2213, 6163, 6164, 6165, 6166, 6167, 6168, 6169, 6170, 2363, 5364, 5764, 4764, 55555 };
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (JObject insertObj in parameters)
                    {
                        var ID = insertObj["ID"];
                        var OnayDerece = 2;
                        var logDetay = "";
                        var logDetay2 = "";
                        if (MyGlobalVariables.Birim == "GM")
                        {
                            OnayDerece = 3;
                            logDetay = "BRDSKALA_Temp tablosu ID: " + ID + " , DSKALAANAID: " + insertObj["DSKALAANAID"].ToInt32() + ", DPERSONELID: " + insertObj["PERSONELID"].ToString() + " – İlgili satırdaki  " + insertObj["Ad"].ToString() + ' ' + insertObj["Soyad"].ToString() + " isimli kullanıcının pozisyon primi ödeneği GM(" + vUser.UserName.ToString() + ") tarafından " + DateTime.Now + " da onaylandı.";
                        }
                        else if (MyGlobalVariables.Birim == "IKGM")
                        {
                            foreach (var item in birimID)
                            {
                                if (insertObj["DBIRIMID"].ToInt32() == item)
                                {
                                    OnayDerece = 1;
                                }
                            }
                            logDetay = "BRDSKALA_Temp tablosu ID: " + ID + " , DSKALAANAID: " + insertObj["DSKALAANAID"].ToInt32() + ", DPERSONELID: " + insertObj["PERSONELID"].ToString() + " – İlgili satırdaki pozisyon primi ödeneği güncellenerek " + vUser.UserName.ToString() + " kullanıcısı tarafından GMY onayına düşürülmüştür.";
                        }
                        else
                        {
                            if (insertObj["IslemTipi"].ToShort() == 0)
                            {
                                logDetay = "BRDSKALA_Temp tablosu ID: " + ID + " , DSKALAANAID: " + insertObj["DSKALAANAID"].ToInt32() + ", DPERSONELID: " + insertObj["PERSONELID"].ToString() + " – İlgili satırdaki pozisyon primi ödeneği bilgisi " + vUser.UserName.ToString() + " kullanıcısı tarafından GM(Murat) onayına düşürülmüştür.";
                            }
                            else
                            {
                                logDetay = "BRDSKALA_Temp tablosu ID: " + ID + " , DSKALAANAID: " + insertObj["DSKALAANAID"].ToInt32() + ", DPERSONELID: " + insertObj["PERSONELID"].ToString() + " – İlgili satırdaki pozisyon primi ödeneği güncellenerek " + vUser.UserName.ToString() + " kullanıcısı tarafından GM(Murat) onayına düşürülmüştür.";
                            }
                        }
                        string s = string.Format("[HR03V01].[wms].[TCH_PrimOnayUpdate] @OnayDerece={0}, @ID={1},@Birim='{2}'", OnayDerece, ID, MyGlobalVariables.Birim);
                        var x = db.Database.SqlQuery<int>(s).ToList();
                        LogActions("Approvals", "Techno", "Prim_Onayla", ComboItems.alEkle, ID.ToInt32(), logDetay);
                        if (MyGlobalVariables.Birim == "GM")
                        {
                            logDetay2 = "BRDSKALA_Temp tablosu ID: " + ID + " , DSKALAANAID: " + insertObj["DSKALAANAID"].ToInt32() + ", DPERSONELID: " + insertObj["PERSONELID"].ToString() + " – İlgili satırdaki  " + insertObj["Ad"].ToString() + ' ' + insertObj["Soyad"].ToString() + " isimli kullanıcının pozisyon primi ödeneği onay sürecinden geçmiştir, sisteme delete insert atılmıştır.";
                            db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [HR03V01].[wms].[BRDSKALA] WHERE DSKALAID={0} ", insertObj["DSKALAID"].ToInt32()));
                            string ss = string.Format("[HR03V01].[wms].[TCH_BRDSKALAINSERT] @ID={0}", ID);
                            var xx = db.Database.SqlQuery<int>(ss).ToList();
                            LogActions("Approvals", "Techno", "Prim_Onayla", ComboItems.alOnayla, ID.ToInt32(), logDetay2);
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
            if (CheckPerm(Perms.TechnoIKOnaylama, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = new Result(true);
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (JObject insertObj in parameters)
                    {
                        var ID = insertObj["ID"];
                        var logDetay = "BUTUCRET_Temp tablosu ID: " + ID + " ,DPERSONELUID: " + insertObj["PERSONELID"].ToString() + " – İlgili satırdaki  " + insertObj["Ad"].ToString() + ' ' + insertObj["Soyad"].ToString() + " isimli kullanıcının ücret bilgisi " + MyGlobalVariables.Birim + "(" + vUser.UserName.ToString() + ") tarafından " + DateTime.Now + " da reddedildi.";
                        string s = string.Format("[HR03V01].[wms].[TCH_UcretRedUpdate] @ID={0},@RedNeden='{1}',@Reddeden='{2}'", ID, RedNeden, vUser.UserName.ToString());
                        var x = db.Database.SqlQuery<int>(s).ToList();
                        LogActions("Approvals", "Techno", "Ucret_Reddet", ComboItems.alRed, ID.ToInt32(), logDetay);
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
            if (CheckPerm(Perms.TechnoIKOnaylama, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (JObject insertObj in parameters)
                    {
                        var ID = insertObj["ID"];
                        var logDetay = "BRDSKALA_Temp tablosu ID: " + ID + " , DSKALAANAID: " + insertObj["DSKALAANAID"].ToInt32() + ", DPERSONELID: " + insertObj["PERSONELID"].ToString() + " – İlgili satırdaki  " + insertObj["Ad"].ToString() + ' ' + insertObj["Soyad"].ToString() + " isimli kullanıcının pozisyon primi ödeneği " + MyGlobalVariables.Birim + "(" + vUser.UserName.ToString() + ") tarafından " + DateTime.Now + " da reddedildi.";
                        string s = string.Format("[HR03V01].[wms].[TCH_PrimRedUpdate] @ID={0},@RedNeden='{1}',@Reddeden='{2}'", ID, RedNeden, vUser.UserName.ToString());
                        var x = db.Database.SqlQuery<int>(s).ToList();
                        LogActions("Approvals", "Techno", "Prim_Reddet", ComboItems.alRed, ID.ToInt32(), logDetay);
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