using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class AccountController : RootController
    {
        public ActionResult Index()
        {
            if (CheckPerm(Perms.CariHesapOnaylama, PermTypes.Reading) == false) return Redirect("/");
            var liste = db.Database.SqlQuery<CariHesapOnaySelect>(string.Format(@"SELECT (case when EFatKullanici= 0 then 'Hayır' when EFatKullanici= 1 then 'Evet' end ) as EFatKullanici1,
(case when EFatSenaryo = -1 then 'Boş' when EFatSenaryo = 0 then 'Temel EFatura' when EFatSenaryo = 1 then 'Ticari EFatura' when EFatSenaryo = 2 then 'Yolcu Beraber Fatura' 
when EFatSenaryo =3 then 'İhracat' end) as EFatSenaryo1,* FROM [FINSAT6{0}].[FINSAT6{0}].CHK_TEMP", vUser.SirketKodu)).ToList();


            return View("Index", liste);
        }


        public JsonResult Onay(string Data, bool OnaylandiMi)
        {
            if (CheckPerm(Perms.CariHesapOnaylama, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = new Result(true);

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            string sql = "";

            foreach (string insertObj in parameters)
            {
                if (OnaylandiMi)
                {
                    sql += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.CHK SET [CheckSum]=1, AktifPasif=0 WHERE HesapKodu ='{1}'", vUser.SirketKodu, insertObj, vUser.UserName);
                }
                else
                {
                    sql += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.CHK SET [CheckSum]=-1, AktifPasif=0 WHERE HesapKodu ='{1}'", vUser.SirketKodu, insertObj, vUser.UserName);
                }

            }

            if (sql != "")
            {
                db.Database.ExecuteSqlCommand(sql);
                return Json(new Result(true, "İşlem Başarılı "), JsonRequestBehavior.AllowGet);
            }
            else
            { return Json(new Result(false, "Hata Oluştu."), JsonRequestBehavior.AllowGet); }



        }


        public ActionResult MHK()
        {
            if (CheckPerm(Perms.CariHesapOnaylama, PermTypes.Reading) == false) return Redirect("/");
            var liste = db.Database.SqlQuery<MuhasebeOnaySelect>(string.Format("SELECT * FROM [MUHASEBE6{0}].[MUHASEBE6{0}].MHK_Temp", vUser.SirketKodu)).ToList();


            return View("MHK", liste);
        }


        public JsonResult MHKOnay(string Data, bool OnaylandiMi)
        {
            if (CheckPerm(Perms.CariHesapOnaylama, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = new Result(true);

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            string sql = "";

            foreach (string insertObj in parameters)
            {
                var temp = db.Database.SqlQuery<MuhasebeOnaySelect>(string.Format("SELECT YeniHesapKod, YeniHesapAd, EskiHesapKod, EskiHesapAd FROM MUHASEBE6{0}.MUHASEBE6{0}.MHK_Temp WHERE YeniHesapKod = '{1}' ", vUser.SirketKodu, insertObj)).FirstOrDefault();
                if (OnaylandiMi)
                {
                    sql += string.Format(@"UPDATE MUHASEBE617.MUHASEBE617.MHK
                SET HesapKod = '" + temp.YeniHesapKod + "', HesapAd = '" + temp.YeniHesapAd + "', [CheckSum] = 1 WHERE HesapKod = '" + temp.EskiHesapKod + "'; DELETE FROM MUHASEBE6{0}.MUHASEBE6{0}.MHK_Temp WHERE YeniHesapKod = '{1}' ", vUser.SirketKodu, insertObj, vUser.UserName);
                }
                else
                {
                    sql += string.Format(@"DELETE FROM MUHASEBE6{0}.MUHASEBE6{0}.MHK_Temp WHERE YeniHesapKod = '{1}' ", vUser.SirketKodu, insertObj, vUser.UserName);
                }





            }

            if (sql != "")
            {
                db.Database.ExecuteSqlCommand(sql);
                return Json(new Result(true, "İşlem Başarılı "), JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new Result(false, "Hata Oluştu."), JsonRequestBehavior.AllowGet);

        }

    }
}