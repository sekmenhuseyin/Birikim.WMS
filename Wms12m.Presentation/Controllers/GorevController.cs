using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            return View("Index", new frmGorev());
        }
        public PartialViewResult GorevGridPartial()
        {
            var list = db.GorevListesis.ToList();
            return PartialView("_GorevGridPartial", list);
        }
        public PartialViewResult GorevDetailPartial(int id)
        {
            ViewBag.ID = id;
            ViewBag.GorevTipiID = new SelectList(db.ComboItemNames.Where(m => m.ComboID == 1).ToList(), "ID", "ItemName");
            ViewBag.DurumID = new SelectList(db.ComboItemNames.Where(m => m.ComboID == 2).ToList(), "ID", "ItemName");
            ViewBag.GorevliID = new SelectList(db.USR01.ToList(), "Id", "UserName");

            var list = db.GorevListesis.Where(m => m.ID == id).FirstOrDefault();
            return PartialView("_GorevDetailPartial", list);
        }

        public PartialViewResult New(frmGorev tbl)
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
            //var list = db.GorevListesis.Where(m => m.ID == tbl.ID).ToList();
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

            Security.CustomPrincipal Users = HttpContext.Current.User as Security.CustomPrincipal;

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
                using (DinamikModelContext Dinamik = new DinamikModelContext(irs.SirketKod))
                {
                    for (short i = 0; i < sti.Count; i++)
                {
                    f_sti[i].DefaultValueSet();
                        f_sti[i].EvrakNo = sti[i].WMS_IRS.EvrakNo;
                        f_sti[i].Tarih = Convert.ToInt32(DateTime.Today.ToOADate());
                        f_sti[i].Chk = CHK;
                        f_sti[i].KynkEvrakTip = 3;
                        f_sti[i].SiraNo = i;
                        f_sti[i].IrsFat = 0;
                        f_sti[i].IslemTip = 1;
                        f_sti[i].MalKodu = sti[i].MalKodu;
                        f_sti[i].Miktar = sti[i].Miktar;
                        f_sti[i].BirimMiktar = sti[i].Miktar;
                        f_sti[i].Birim = sti[i].Birim;
                        f_sti[i].OtvDahilHaric = -1;
                        f_sti[i].Depo = sti[i].WMS_IRS.TK_DEP.DepoKodu;
                        f_sti[i].SevkTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                        f_sti[i].VadeTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                        f_sti[i].MlyYontem = -1;
                        f_sti[i].MhsDurum = 1;
                        f_sti[i].MlyMhs = 1;
                        f_sti[i].MhsTabloNo = 1;
                        f_sti[i].EvrakTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                        f_sti[i].KlmTutarIskNet = -1;
                        f_sti[i].TeslimChk = CHK;
                        f_sti[i].Katsayi = 1;
                        f_sti[i].KaynakIrsTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                        f_sti[i].ErekIFKEvrakTip = -1;
                        f_sti[i].ErekIIFKEvrakTip = -1;
                        f_sti[i].IrsFat2 = -1;
                        f_sti[i].Kredi_Donem_VadeTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                        f_sti[i].Duz_Yapilan_Donem = -1;
                        f_sti[i].Duz_Yontemi = -1;
                        f_sti[i].Duz_Mhs_Durumu = -1;
                        f_sti[i].Duz_Mly_Yontemi = -1;
                        f_sti[i].Duz_Mly_Mhs_Durumu = -1;
                        f_sti[i].AnaEvrakTip = 3;
                        f_sti[i].Kaydeden = Users.AppIdentity.User.LogonUserName;
                        f_sti[i].KayitTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                        f_sti[i].KayitSaat = Convert.ToInt32(DateTime.Today.ToOADate());
                        f_sti[i].Degistiren = Users.AppIdentity.User.LogonUserName;
                        f_sti[i].DegisTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                        f_sti[i].DegisSaat = Convert.ToInt32(DateTime.Today.ToOADate());
                        f_sti[i].EFatDurum = -1;
                        f_sti[i].OTVTevkifatVarYok = 1;
                        f_sti[i].EArsivTeslimSekli = -1;
                        f_sti[i].EArsivFaturaTipi = -1;
                        f_sti[i].EArsivFaturaDurum = -1;
                        db.SaveChanges();
                    }


                
                    var list = Dinamik.Context.STIs;

                }               
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