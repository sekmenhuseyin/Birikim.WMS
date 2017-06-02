using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Approval.Controllers
{
    public class InvoiceController : RootController
    {
        /// <summary>
        /// anasayfa
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// lsiteyi getirir
        /// </summary>
        public PartialViewResult List(string Refresh, string ListType)
        {
            var FO = new List<FaturaOnay>();
            if (Refresh != "")
            {
                FO = db.Database.SqlQuery<FaturaOnay>(string.Format("[FINSAT6{0}].[dbo].[FaturaOnay] @onayTip='{1}'", "17", ListType)).ToList();

            }
            return PartialView("List", FO);
        }
        /// <summary>
        /// detaylar
        /// </summary>
        public ActionResult Detail(string EvrakNo)
        {
            FaturaDetayData _FDD = new FaturaDetayData()
            {
                GENEL = new List<FaturaDetayGenel>(),
                STI = new List<FaturaDetaySTI>(),
                FTD = new List<FaturaDetayFTD>()
            };
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
        /// <summary>
        /// onaylama
        /// </summary>
        public string Onay(string EvrakNo, string CHK, string Tarih, short[] ChckSm)
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
                var x = db.Database.SqlQuery<int>(string.Format("[FINSAT6{0}].[dbo].[FaturaOnayUpdate] @Tip={1}, @EvrakNo='{2}',@Chk='{3}',@Tarih={4},@RedNedeni='{5}',@Degistiren='{6}',@Degistarih={7}", "17", 1, EvrakNo, CHK, Convert.ToInt32(Convert.ToDateTime(Tarih.ToString()).ToOADate()), "", vUser.UserName, Convert.ToInt32(DateTime.Today.ToOADate()))).ToList();
                return "YES";
            }
            catch (Exception ex)
            {
                return "NO";
            }
        }
        /// <summary>
        /// reddetme
        /// </summary>
        public string Red(string EvrakNo, string CHK, string Tarih, string RedNeden, short[] ChckSm)
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