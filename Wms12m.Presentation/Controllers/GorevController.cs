using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class GorevController : RootController
    {
        // GET: Gorev
        public ActionResult Index()
        {
            var list = db.GorevListesis.ToList();
            return View("Index", list);
        }
        public ActionResult GorevDetailPartial(int id)
        {
            ViewBag.ID = id;
            ViewBag.GorevTipiID = new SelectList(db.ComboItemNames.Where(m => m.ComboID == 1).ToList(), "ID", "ItemName");
            ViewBag.DurumID = new SelectList(db.ComboItemNames.Where(m => m.ComboID == 2).ToList(), "ID", "ItemName");
            ViewBag.GorevliID = new SelectList(db.USR01.ToList(), "Id", "UserName");
            var list = db.GorevListesis.Where(m => m.ID == id).FirstOrDefault();
            return PartialView("_GorevDetailPartial", list);
        }

        public PartialViewResult Update(frmGorev tbl)
        {
            //check if exists
            var tmp = db.GorevListesis.Where(m => m.ID == tbl.ID).FirstOrDefault();
            if (tmp != null)
            {
                //add new
                tmp.GorevliID = tbl.GorevliID;
                tmp.Aciklama = tbl.Aciklama;
                tmp.Bilgi = tbl.Bilgi;
                tmp.DurumID = tbl.DurumID;
                db.SaveChanges();
            }
            //get list
            ViewBag.GorevID = tbl.ID;
            var list = db.GorevListesis.ToList();
            return PartialView("_GorevGridPartial", list);
        }

        public string MalKabulOnay(string EvrakNo, string CHK, int IrsaliyeNo, string GorevNo, int SirketKodu)
        {
            abstractMalKabul<STII> STIset = new MalKabulSTIOnay();
            abstractMalKabul<FTDD> FTDset = new MalKabulFTDOnay();
            abstractMalKabul<MFKK> MFKset = new MalKabulMFKOnay();
            abstractMalKabul<STKK> STKset = new MalKabulSTKOnay();
            abstractMalKabul<DSTT> DSTset = new MalKabulDSTOnay();



            var grv = db.GorevListesis.Where(m => (m.GorevNo == GorevNo) && (m.GorevTipiID == 1)).FirstOrDefault();
            if (grv != null)
            {
                //add new
                grv.DurumID = 12;
                db.SaveChanges();
            }

            var irs = db.WMS_IRS.Where(m => m.ID == IrsaliyeNo).FirstOrDefault();
            if (irs != null)
            {
                //add new
                irs.Onay = false;
                db.SaveChanges();
            }
            List<STI> f_sti = new List<STI>();
            var sti = db.WMS_STI.Where(m => m.IrsaliyeID == IrsaliyeNo).ToList();
            if (sti != null)
            {
                for (int i = 0; i < sti.Count; i++)
                {
                }


                using (DinamikModelContext Dinamik = new DinamikModelContext(irs.SirketKod))
                {
                    var list = Dinamik.Context.STIs;

                }
                //add new
                db.SaveChanges();
            }

            
            

            
            var ftd = db.WMS_IRS.Where(m => m.ID == IrsaliyeNo).FirstOrDefault();
            if (ftd != null)
            {
                //add new
                ftd.Onay = false;
                db.SaveChanges();
            }
            var mfk = db.WMS_IRS.Where(m => m.ID == IrsaliyeNo).FirstOrDefault();
            if (mfk != null)
            {
                //add new
                mfk.Onay = false;
                db.SaveChanges();
            }

            return "";
            //return View();
        }
    }
}