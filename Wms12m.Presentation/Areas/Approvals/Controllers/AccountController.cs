using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class AccountController : RootController
    {
        public ActionResult Index()
        {
            if (CheckPerm(Perms.CariHesapOnaylama, PermTypes.Reading) == false) return Redirect("/");
            var liste = db.Database.SqlQuery<CariHesapOnaySelect>(string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].CHK_TEMP", vUser.SirketKodu)).ToList();


            return View("Index", liste);
        }


        public JsonResult Onay(string Data)
        {
            if (CheckPerm(Perms.CariHesapOnaylama, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = new Result(true);

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            string sql = "";

            foreach (string insertObj in parameters)
            {
                var temp = db.Database.SqlQuery<CariHesapOnaySelect>(string.Format("SELECT HesapKodu,Unvan1,Unvan2,FaturaAdres1,FaturaAdres2,FaturaAdres3,VergiDairesi,HesapNo,OpsiyonGunu,BolgeKod,OzelKod,GrupKod,TipKod,Kod1,Kod2, Kod3,Kod4,Kod5,Kod6,Kod7,Kod8,Kod9,Kod10,Kod11,Kod12,Kod13,Kod14,Kod15,Kod16,Kod17,Kod18,MhsKod,KrediLimiti,EFatKullanici,EFatSenaryo,EFatEtiket,Ad,SoyAd,EFatEtiket FROM FINSAT6{0}.FINSAT6{0}.CHK_TEMP WHERE HesapKodu = '{1}' ", vUser.SirketKodu, insertObj)).FirstOrDefault();

                sql += string.Format("INSERT INTO FINSAT617.FINSAT617.CHK(HesapKodu, Unvan1, Unvan2, FaturaAdres1, FaturaAdres2, FaturaAdres3, VergiDairesi, HesapNo, OpsiyonGunu, BolgeKod, OzelKod, GrupKod, TipKod, Kod1, Kod2, Kod3, Kod4, Kod5, Kod6, Kod7, Kod8, Kod9, Kod10, Kod11, Kod12, Kod13, Kod14, Kod15, Kod16, Kod17, Kod18, MhsKod, KrediLimiti, EFatKullanici, EFatSenaryo, EFatEtiket, Ad, SoyAd)VALUES('"+temp.HesapKodu+"', '"+temp.Unvan1+ "', '" + temp.Unvan2 + "', '" + temp.FaturaAdres1 + "','" + temp.FaturaAdres2 + "', '" + temp.FaturaAdres3 + "', '" + temp.VergiDairesi + "', '" + temp.HesapNo + "', '" + temp.OpsiyonGunu + "',  '" + temp.BolgeKod + "', '" + temp.OzelKod + "', '" + temp.GrupKod + "', '" + temp.TipKod + "', '" + temp.Kod1 + "', '" + temp.Kod2 + "', '" + temp.Kod3 + "', '" + temp.Kod4 + "', '" + temp.Kod5 + "', '" + temp.Kod6 + "', '" + temp.Kod7 + "', '" + temp.Kod8 + "', '" + temp.Kod9 + "', '" + temp.Kod10 + "', '" + temp.Kod11 + "', '" + temp.Kod12 + "', '" + temp.Kod13 + "', '" + temp.Kod14 + "', '" + temp.Kod15 + "', '" + temp.Kod16 + "', '" + temp.Kod17 + "', '" + temp.Kod18 + "', '" + temp.MhsKod + "', '" + temp.KrediLimiti + "', '" + temp.EFatKullanici + "', '" + temp.EFatSenaryo + "', '" + temp.EFatEtiket + "', '" + temp.Ad + "', '" + temp.SoyAd + "')", vUser.SirketKodu, insertObj, vUser.UserName);

            }
            try
            {
                if (sql != "") db.Database.ExecuteSqlCommand(sql);
                return Json(new Result(true, "İşlem Başarılı "), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new Result(false, "Hata Oluştu."), JsonRequestBehavior.AllowGet);
            }

        }
    }
}