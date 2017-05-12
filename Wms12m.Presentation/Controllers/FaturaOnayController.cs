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

        public string FaturaDetayOnayla(string EvrakNo, string CHK, string Tarih, short[] ChckSm)
        {
            var  FO = db.Database.SqlQuery<int>(string.Format("SELECT Count(*) FROM FINSAT6{0}.FINSAT6{0}.STI (NOLOCK) WHERE EvrakNo='{1}' AND CheckSum={2}", "17", EvrakNo,ChckSm)).FirstOrDefault();
            
            if(FO>0)
            {
                return "DEGISIM";
            }
            try
            {
                var x = db.Database.SqlQuery<FaturaOnay>(string.Format("[FINSAT6{0}].[dbo].[FaturaOnayUpdate] @Tip={1}, @EvrakNo='{2}',@Chk='{3}',@Tarih={4},@RedNedeni='{5}',@Degistiren='{6}',@Degistarih={7}", "17", 1,EvrakNo,CHK, Convert.ToInt32(Convert.ToDateTime(Tarih.ToString()).ToOADate()), "",vUser.UserName, Convert.ToInt32(DateTime.Today.ToOADate()))).ToList();
           
                    return "NO";

            }
            catch (Exception)
            {
                return "NO";
            }

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