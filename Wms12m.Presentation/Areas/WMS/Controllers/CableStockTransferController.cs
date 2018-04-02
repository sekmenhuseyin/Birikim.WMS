using System.Web.Mvc;
using Wms12m.Presentation.Areas.WMS.Models.ViewModels;
using System.Linq;
using System.Collections.Generic;
using Wms12m.Business;
using Wms12m.Entity.Models;
using System.Net;
using Wms12m.Entity;
using System;
using Birikim.Models;

namespace Wms12m.Presentation.Areas.WMS.Controllers
{
    public class CableStockTransferController : RootController
    {
        // GET: WMS/CableStockTransfer
        public ActionResult Index()
        {
           
            
            ViewBag.baslik = "MySQL Kablo Stok Aktarımı";

            ViewBag.DepoID = new SelectList(Store.GetListCable(vUser.DepoId), "DepoKodu", "DepoAd");
            //ViewBag.BolumID= new SelectList(Section.GetList(), "ID", "BolumAd");
            //ViewBag.RafID = new SelectList(Shelf.GetList(), "ID", "RafAd");
          
            return View();
        }

        public PartialViewResult GetListOfMySQL(string DepoID)
        {
            DepoID = DepoID.Trim();
            var data = MySQLDataViewModel
                .GetKabloMySQLViewModelList(this)
                .Where(x => x.DepoID.ToUpper() == DepoID.ToUpper());


            return PartialView(data);
            //return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MysqlKaydet(List<MySQLDataViewModel> data)
        {

            if (data.Count(x=>string.IsNullOrEmpty(x.MakaraNo))!=0)//EKLE
            {
                return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
            }


            var v = data;
            

            Yer yer=null;
            Yer_Log yerLog=null;
            string hucreAd = "";
            int katId = 0;
            int? depoID = 0;
            string birim = "";
            //foreach (var item in data)
            //{
            //    hucreAd = item.Raf + "-" + item.Bolum + "-" + item.Kat;
            //    yer = db.Yers.SingleOrDefault(x => x.HucreAd == hucreAd);

            //    if (yer == null)
            //    {
            //        katId = db.Kats.First(x => x.KatAd == item.Kat).ID;
            //        depoID = db.Depoes.Single(x => x.DepoAd == item.DepoID).ID;
            //        string query = string.Format("select birim1 As Birim from FINSAT6{0}.FINSAT6{0}.STK", vUser.SirketKodu);
            //        birim = db.Database.SqlQuery<STKBirimMalKod>(query).FirstOrDefault().Birim;


            //        yer = new Yer
            //        {
            //            KatID = katId,
            //            DepoID = depoID,
            //            MalKodu = item.MalKodu,
            //            MakaraNo = "Boş-" + db.SettingsMakaraNo(depoID).FirstOrDefault(),
            //         Miktar = item.Miktar,
            //            MakaraDurum = item.Makara == "AÇIK" ? true : false,
            //            HucreAd = hucreAd,
            //            Birim = birim
            //        };
            //        yerLog = new Yer_Log
            //        {
            //            KatID = katId,
            //            DepoID = depoID,
            //            MalKodu = item.MalKodu,
            //            MakaraNo = item.MakaraNo,
            //            Miktar = item.Miktar,
            //            HucreAd = hucreAd,
            //            KayitTarihi=DateTime.Now.Date.ToOADate().ToInt32(),
            //            KayitSaati=DateTime.Now.ToOaTime(),
            //            IslemTipi="Kablo Stok Ekle MySQL Aktarılan",
            //            Kaydeden=vUser.UserName,
            //            Birim=birim
            //        };

            //        db.Yers.Add(yer);
            //        db.Yer_Log.Add(yerLog);
            //    }
            //    else
            //    {

            //        yer.KatID = katId;
            //        yer.Miktar += item.Miktar;
            //        yer.MakaraDurum = item.Makara == "AÇIK" ? true : false;

            //        yerLog = new Yer_Log
            //        {
            //            KatID = katId,
            //            DepoID = depoID,
            //            MalKodu = item.MalKodu,
            //            MakaraNo = item.MakaraNo,
            //            Miktar = item.Miktar,
            //            HucreAd = hucreAd,
            //            KayitTarihi = DateTime.Now.Date.ToOADate().ToInt32(),
            //            KayitSaati = DateTime.Now.ToOaTime(),
            //            IslemTipi = "Kablo Stok Güncelle MySQL Aktarılan",
            //            Kaydeden = vUser.UserName,
            //            Birim=birim
            //        };

            //        db.Yer_Log.Add(yerLog);

            //    }

            //}
            //db.SaveChanges();

            foreach (var item in data)
            {
                katId = db.Kats.First(x => x.KatAd == item.Kat).ID;
                depoID = db.Depoes.Single(x => x.DepoAd == item.DepoID).ID;
                string query = string.Format("select birim1 As Birim from FINSAT6{0}.FINSAT6{0}.STK WHERE MalKodu={0}", vUser.SirketKodu,item.MalKodu);
                birim = db.Database.SqlQuery<STKBirimMalKod>(query).FirstOrDefault().Birim;

                // yerleştirme kaydı yapılır
                var tmp2 = Yerlestirme.Detail(katId, item.MalKodu, birim);
                if (tmp2 == null)
                {
                    tmp2 = new Yer()
                    {
                        KatID = katId,
                        MalKodu = item.MalKodu,
                        Birim = birim,
                        Miktar = item.Miktar
                    };
                    Yerlestirme.Insert(tmp2, vUser.Id, "Sayım Farkı Fişi", mGorev.IrsaliyeID.Value);
                }
                else
                {
                    if (item.Miktar > item.Stok)//giriş
                    {
                        tmp2.Miktar = item.Miktar;
                        Yerlestirme.Update(tmp2, vUser.Id, "Sayım Farkı Fişi", item.Miktar - item.Stok, false, mGorev.IrsaliyeID.Value);
                    }
                    else if (item.Miktar < item.Stok)//çıkış
                    {
                        tmp2.Miktar = item.Miktar;
                        Yerlestirme.Update(tmp2, vUser.Id, "Sayım Farkı Fişi", item.Stok - item.Miktar, true, mGorev.IrsaliyeID.Value);
                    }
                }
            }



            return Json(new Result(true, "Ok"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRaf()
        {
            
            List<Raf> list = Shelf.GetList(vUser.DepoId.GetValueOrDefault());
  
            var jsonData = from p in list
                           select new { p.ID, p.RafAd };

            return Json(jsonData,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBolum(string id)
        {

            if (vUser.DepoId.IsNull())
                return null;
            int rafid = db.Rafs.First(x => x.RafAd == id).ID;
            List<Bolum> list = Section.GetList(rafid);

            var jsonData = from p in list
                           select new { p.ID, p.BolumAd };
                


            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetKat(string id)
        {

            if (vUser.DepoId.IsNull())
                return null;

            int bolumid = db.Bolums.First(x => x.BolumAd == id).ID;

            List<Kat> list = Floor.GetList(bolumid);

            var jsonData = from p in list
                           select new { p.ID, p.KatAd };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// malzeme autocomplete
        /// </summary>
        public JsonResult GetMalzemebyCode(string term)
        {
            var sql = string.Format("FINSAT6{0}.[wms].[getMalzemeByCode] @MalKodu = N'{1}', @MalAdi = N''", vUser.SirketKodu, term);
            // return
            try
            {
                var list = db.Database.SqlQuery<frmJson>(sql).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "WMS/CableStock/getMalzemebyCode");
                return Json(new List<frmJson>(), JsonRequestBehavior.AllowGet);
            }
        }

        //public JsonResult GetListOfMySQL(string DepoID)
        //{

        //    DepoID = DepoID.Trim();
        //    var data = MySQLDataViewModel
        //        .GetKabloMySQLViewModelList()
        //        .Where(x => x.DepoID.ToUpper() == DepoID.ToUpper());

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
    }
}