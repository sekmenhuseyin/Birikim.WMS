using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class InvoiceController : RootController
    {
        /// <summary>
        /// anasayfa
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.FaturaOnaylama, PermTypes.Reading) == false) return null;
            return View();
        }

        /// <summary>
        /// lsiteyi getirir
        /// </summary>
        public PartialViewResult List(string Refresh, string ListType)
        {
            if (CheckPerm(Perms.FaturaOnaylama, PermTypes.Reading) == false) return null;
            var FO = new List<FaturaOnay>();
            if (Refresh != "")
            {
                FO = db.Database.SqlQuery<FaturaOnay>(string.Format("[FINSAT6{0}].[wms].[Fatura_Onay] @onayTip='{1}'", vUser.SirketKodu, ListType)).ToList();
            }

            return PartialView("List", FO);
        }

        /// <summary>
        /// detaylar
        /// </summary>
        public ActionResult Detail(string EvrakNo)
        {
            if (CheckPerm(Perms.FaturaOnaylama, PermTypes.Reading) == false) return null;
            var _FDD = new FaturaDetayData()
            {
                GENEL = new List<FaturaDetayGenel>(),
                STI = new List<FaturaDetaySTI>(),
                FTD = new List<FaturaDetayFTD>()
            };
            var FDD = db.MultipleResults(string.Format("[FINSAT6{0}].[wms].[Fatura_OnayDetay] @EvrakNo='{1}'", vUser.SirketKodu, EvrakNo)).With<FaturaDetayGenel>().With<FaturaDetaySTI>().With<FaturaDetayFTD>().Execute();
            foreach (FaturaDetayGenel item in FDD[0])
                _FDD.GENEL.Add(item);

            foreach (FaturaDetaySTI item in FDD[1])
                _FDD.STI.Add(item);

            foreach (FaturaDetayFTD item in FDD[2])
                _FDD.FTD.Add(item);

            ViewBag.EvrakNo = EvrakNo;
            return View(_FDD);
        }

        /// <summary>
        /// onaylama
        /// </summary>
        public string Onay(string EvrakNo, string CHK, string Tarih, short[] ChckSm)
        {
            if (CheckPerm(Perms.FaturaOnaylama, PermTypes.Writing) == false) return "NO";
            var chck = "";
            foreach (var item in ChckSm)
                chck += item + ",";

            chck = chck.Substring(0, chck.Length - 1);
            var FO = db.Database.SqlQuery<int>(string.Format("SELECT Count(*) FROM FINSAT6{0}.FINSAT6{0}.STI (NOLOCK) WHERE EvrakNo='{1}' AND CheckSum IN ({2})", vUser.SirketKodu, EvrakNo, chck)).FirstOrDefault();

            if (FO != ChckSm.Length) return "DEGISIM";
            CHK = db.Database.SqlQuery<string>(string.Format("SELECT Chk FROM [FINSAT6{0}].[FINSAT6{0}].STI WHERE (EvrakNo = '{1}')", vUser.SirketKodu, EvrakNo)).FirstOrDefault();
            try
            {
                var x = db.Database.SqlQuery<int>(string.Format("[FINSAT6{0}].[wms].[Fatura_OnayUpdate] @Tip={1}, @EvrakNo='{2}',@Chk='{3}',@Tarih={4},@RedNedeni='{5}',@Degistiren='{6}',@Degistarih={7}", vUser.SirketKodu, 1, EvrakNo, CHK, Convert.ToInt32(Convert.ToDateTime(Tarih.ToString()).ToOADate()), "", vUser.UserName, Convert.ToInt32(DateTime.Today.ToOADate()))).ToList();
                return "YES";
            }
            catch (Exception)
            {
                return "NO";
            }
        }

        /// <summary>
        /// reddetme
        /// </summary>
        public string Red(string EvrakNo, string CHK, string Tarih, string RedNeden, short[] ChckSm)
        {
            if (CheckPerm(Perms.FaturaOnaylama, PermTypes.Writing) == false) return "NO";
            var chck = "";
            foreach (var item in ChckSm)
                chck += item + ",";

            chck = chck.Substring(0, chck.Length - 1);
            var FO = db.Database.SqlQuery<int>(string.Format("SELECT Count(*) FROM FINSAT6{0}.FINSAT6{0}.STI (NOLOCK) WHERE EvrakNo='{1}' AND CheckSum IN ({2})", vUser.SirketKodu, EvrakNo, chck)).FirstOrDefault();

            if (FO != ChckSm.Length) return "DEGISIM";
            CHK = db.Database.SqlQuery<string>(string.Format("SELECT Chk FROM [FINSAT6{0}].[FINSAT6{0}].STI WHERE (EvrakNo = '{1}')", vUser.SirketKodu, EvrakNo)).FirstOrDefault();
            try
            {
                var x = db.Database.SqlQuery<int>(string.Format("[FINSAT6{0}].[wms].[Fatura_OnayUpdate] @Tip={1}, @EvrakNo='{2}',@Chk='{3}',@Tarih={4},@RedNedeni='{5}',@Degistiren='{6}',@Degistarih={7}", vUser.SirketKodu, 0, EvrakNo, CHK, Convert.ToInt32(Convert.ToDateTime(Tarih.ToString()).ToOADate()), RedNeden, vUser.UserName, Convert.ToInt32(DateTime.Today.ToOADate()))).ToList();
                return "YES";
            }
            catch (Exception)
            {
                return "NO";
            }
        }
    }
}