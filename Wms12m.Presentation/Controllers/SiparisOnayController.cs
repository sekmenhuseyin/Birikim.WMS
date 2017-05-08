using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
namespace Wms12m.Presentation.Controllers
{
    public class SiparisOnayController : RootController
    {
        //public ActionResult Stok()
        //{
        //    var KOD = db.Database.SqlQuery<RaporGetKod>(string.Format("[FINSAT6{0}].[dbo].[DB_GetKod]", "33")).ToList();
        //    return View(KOD);
        //}

        public ActionResult SMOnay()
        {

            return View();
        }
        public PartialViewResult PartialSMOnay()
        {
            List<SMSiparisOnaySelect> KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[SiparisOnayListSM]", "33")).ToList();
            return PartialView("_PartialSMSiparisOnay", KOD);
        }

        public ActionResult SPGMYOnay()
        {

            return View();
        }
        public PartialViewResult PartialSPGMYOnay()
        {
            List<SMSiparisOnaySelect> KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[SiparisOnayListSPGMY]", "33")).ToList();
            return PartialView("_PartialSPGMYSiparisOnay", KOD);
        }

        public ActionResult SiparisGMOnay()
        {

            return View();
        }
        public PartialViewResult PartialGMOnay()
        {
            List<SMSiparisOnaySelect> KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[SiparisOnayListGM]", "33")).ToList();
            return PartialView("_PartialGMSiparisOnay", KOD);
        }

        ////public bool SiparisOnayUpdate(Dictionary<string, int> dicEvrakNo, bool OnayState, int OnaylayanTip, string Kullanici)
        //{
        //    bool Result = true;

        //    try
        //    {
        //        string OnayText = OnayState ? "Onaylandı" : "Reddedildi";

        //        foreach (KeyValuePair<string, int> item in dicEvrakNo)
        //        {
        //            GMSiparisOnaySelect ent = db.Database.SqlQuery<SiparisOnay>(string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[SiparisOnay] WHERE EvrakNo = '{1}'", "17",item.Key)).FirstOrDefault();
        //            //db.SiparisOnays.Where(x => x.EvrakNo == item.Key).FirstOrDefault();

        //            if (OnaylayanTip == 1)
        //            {
        //                ent.SMOnay = OnayState;
        //                ent.SMOnaylayan = Kullanici;
        //                ent.SMOnayTarih = DateTime.Now;

        //                if (!OnayState)
        //                {
        //                    ent.Durum = 3;
        //                    VKContext.SiparisOnayUpdate(item.Key, OnayText);
        //                }
        //                else if (ent.OnayTip == 1)
        //                {
        //                    ent.Durum = 2;
        //                    VKContext.SiparisOnayUpdate(item.Key, OnayText);
        //                }
        //            }
        //            else if (OnaylayanTip == 2)
        //            {
        //                ent.SPGMYOnay = OnayState;
        //                ent.SPGMYOnaylayan = Kullanici;
        //                ent.SPGMYOnayTarih = DateTime.Now;

        //                if (!OnayState)
        //                {
        //                    ent.Durum = 3;
        //                    VKContext.SiparisOnayUpdate(item.Key, OnayText);
        //                }
        //                else if (ent.OnayTip == 2)
        //                {
        //                    ent.Durum = 2;
        //                    VKContext.SiparisOnayUpdate(item.Key, OnayText);
        //                }
        //            }
        //            else if (OnaylayanTip == 3)
        //            {
        //                ent.GMOnay = OnayState;
        //                ent.GMOnaylayan = Kullanici;
        //                ent.GMOnayTarih = DateTime.Now;

        //                if (!OnayState)
        //                {
        //                    ent.Durum = 3;
        //                    VKContext.SiparisOnayUpdate(item.Key, OnayText);
        //                }
        //                else if (ent.OnayTip == 3)
        //                {
        //                    ent.Durum = 2;
        //                    VKContext.SiparisOnayUpdate(item.Key, OnayText);
        //                }
        //            }
        //        }

        //        VKContext.SubmitChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        Result = false;
        //    }

        //    return Result;
        //}
    }
}