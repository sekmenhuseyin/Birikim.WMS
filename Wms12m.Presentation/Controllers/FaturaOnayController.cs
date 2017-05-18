using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class FaturaOnayController : RootController
    {
        // GET: FaturaOnay
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult GridPartialRender(string Refresh, string ListType)
        {
            var FO = new List<FaturaOnay>();
            if (Refresh != "")
            {
                FO = db.Database.SqlQuery<FaturaOnay>(string.Format("[FINSAT6{0}].[dbo].[FaturaOnay] @onayTip='{1}'", "17",ListType)).ToList();
               
            }
            return PartialView("_PartialFaturaOnayGrid", FO);
        }

        public ActionResult FaturaDetay(string EvrakNo)
        {
            FaturaDetayData _FDD = new FaturaDetayData();
            _FDD.GENEL = new List<FaturaDetayGenel>();
            _FDD.STI = new List<FaturaDetaySTI>();
            _FDD.FTD = new List<FaturaDetayFTD>();
            var FDD = db.MultipleResults(string.Format("[FINSAT6{0}].[dbo].[FaturaOnayDetay] @EvrakNo='{1}'", 17, EvrakNo)).With<FaturaDetayGenel>().With<FaturaDetaySTI>().With<FaturaDetayFTD>().Execute();
            foreach (FaturaDetayGenel item in FDD[0])
            {
                _FDD.GENEL.Add(item);
            }
            foreach (FaturaDetaySTI item in FDD[1])
            {
                _FDD.STI.Add(item);
            }
            foreach (FaturaDetayFTD item in FDD[2])
            {
                _FDD.FTD.Add(item);
            }
            ViewBag.EvrakNo = EvrakNo;
            return View(_FDD);
        }

        public string FaturaDetayOnayla(string EvrakNo, string CHK, string Tarih, short[] ChckSm)
        {
            string chck = "";
            foreach (var item in ChckSm)
            {
                chck += item + ",";
            }
            chck = chck.Substring(0, chck.Length-1);
            var  FO = db.Database.SqlQuery<int>(string.Format("SELECT Count(*) FROM FINSAT6{0}.FINSAT6{0}.STI (NOLOCK) WHERE EvrakNo='{1}' AND CheckSum IN ({2})", "17", EvrakNo, chck)).FirstOrDefault();
            
            if(FO!=ChckSm.Length)
            {
                return "DEGISIM";
            }
            try
            {
                var x = db.Database.SqlQuery<int>(string.Format("[FINSAT6{0}].[dbo].[FaturaOnayUpdate] @Tip={1}, @EvrakNo='{2}',@Chk='{3}',@Tarih={4},@RedNedeni='{5}',@Degistiren='{6}',@Degistarih={7}", "17", 1,EvrakNo,CHK, Convert.ToInt32(Convert.ToDateTime(Tarih.ToString()).ToOADate()), "",vUser.UserName, Convert.ToInt32(DateTime.Today.ToOADate()))).ToList();
                    return "YES";
            }
            catch (Exception ex)
            {
                return "NO";
            }
        }

        public string FaturaDetayRed(string EvrakNo, string CHK, string Tarih, string RedNeden, short[] ChckSm)
        {
            string chck = "";
            foreach (var item in ChckSm)
            {
                chck += item + ",";
            }
            chck = chck.Substring(0, chck.Length - 1);
            var FO = db.Database.SqlQuery<int>(string.Format("SELECT Count(*) FROM FINSAT6{0}.FINSAT6{0}.STI (NOLOCK) WHERE EvrakNo='{1}' AND CheckSum IN ({2})", "17", EvrakNo, chck)).FirstOrDefault();

            if (FO != ChckSm.Length)
            {
                return "DEGISIM";
            }
            try
            {
                var x = db.Database.SqlQuery<int>(string.Format("[FINSAT6{0}].[dbo].[FaturaOnayUpdate] @Tip={1}, @EvrakNo='{2}',@Chk='{3}',@Tarih={4},@RedNedeni='{5}',@Degistiren='{6}',@Degistarih={7}", "17", 0, EvrakNo, CHK, Convert.ToInt32(Convert.ToDateTime(Tarih.ToString()).ToOADate()), RedNeden, vUser.UserName, Convert.ToInt32(DateTime.Today.ToOADate()))).ToList();
                return "YES";
            }
            catch (Exception ex)
            {
                return "NO";
            }

        }
    }
}