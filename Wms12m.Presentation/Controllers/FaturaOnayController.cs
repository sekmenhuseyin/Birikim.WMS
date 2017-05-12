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
            var FDD = db.MultipleResults(string.Format("[FINSAT6{0}].[dbo].[FaturaOnayDetay] @EvrakNo='{1}'", 17, "VAS000000791")).With<FaturaDetayGenel>().With<FaturaDetaySTI>().With<FaturaDetayFTD>().Execute();
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

        public string FaturaDetayOnayla(string EvrakNo, string CHK, string Tarih)
        {
            //Result _Rslt = new Result();
            //int a;
            //FaturaDetayData FDD = new FaturaDetayData();
            //FaturaDetayData FDD_Eski = new FaturaDetayData();
            //FDD_Eski = setConnection.GetFaturaDetayData();
            //FDD = _FaturaDetayGenel.DetayGenelList(EvrakNo);
            //if (FDD_Eski.STI.Count != FDD.STI.Count)
            //{
            //    //guncelKontrol = true;
            //    return "DEGISIM";
            //}

            //else
            //{
            //    for (int i = 0; i < FDD_Eski.STI.Count; i++)
            //    {
            //        bool compData = FDD_Eski.STI[i].IsDifferent(FDD.STI[i]);

            //        if (compData == true)
            //        {
            //            //guncelKontrol = true;
            //            return "DEGISIM";
            //        }
            //    }

            //    for (int i = 0; i < FDD_Eski.FTD.Count; i++)
            //    {
            //        bool compData = FDD_Eski.FTD[i].IsDifferent(FDD.FTD[i]);

            //        if (compData == true)
            //        {
            //            //guncelKontrol = true;
            //            return "DEGISIM";
            //        }
            //    }

            //    for (int i = 0; i < FDD_Eski.GENEL.Count; i++)
            //    {
            //        bool compData = FDD_Eski.GENEL[i].IsDifferent(FDD.GENEL[i]);

            //        if (compData == true)
            //        {
            //            //guncelKontrol = true;
            //            return "DEGISIM";
            //        }
            //    }
            //}



            ////List<FaturaDetaySTI> aa = FDD_Eski.STI.ToList();
            ////List<FaturaDetaySTI> bb = FDD.STI.ToList();


            //// guncelKontrol = IsDataChanged<FaturaDetaySTI>(aa.Take(1).SingleOrDefault(), bb.Take(1).SingleOrDefault());

            //try
            //{
            //    a = _FaturaDetayGenel.StoredProcInsert(1, EvrakNo, CHK,
            //        Convert.ToInt32(Convert.ToDateTime(Tarih.ToString()).ToOADate()), "", Session["Users"].ToString(), Convert.ToInt32(DateTime.Today.ToOADate()));
            //    if (a != 0)
            //    {
            //        _Rslt.Status = true;

            //        return "OK";
            //    }
            //    else
            //    {
            //        return "NO";
            //    }

            //}
            //catch (Exception)
            //{
            //    return "NO";
            //}

            return "OK";
        }
    }
}