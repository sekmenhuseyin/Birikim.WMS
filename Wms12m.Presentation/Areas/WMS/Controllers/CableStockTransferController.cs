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

            ViewBag.DepoID = new SelectList(Store.GetListCable(vUser.DepoId), "ID", "DepoAd");
          
            return View();
        }

        public PartialViewResult GetListOfMySQL(int DepoID)
        {

            var depoAd = Store.Detail(DepoID).DepoAd;
            var data = MySQLDataViewModel
                .GetKabloMySQLViewModelList(this)
                .Where(x => x.DepoID.ToUpper() == depoAd.ToUpper());


            return PartialView(data);
        }

        public JsonResult MysqlKaydet(List<MySQLDataViewModel> data)
        {

            if (data.Count(x=>string.IsNullOrEmpty(x.MakaraNo))!=0)//EKLE
            {
                return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
            }
            string birim = "";
            int DepoID = data[0].DepoID.ToInt32();
            var gorevno = db.SettingsGorevNo(DateTime.Today.ToOADateInt(), DepoID).FirstOrDefault();
            var today = fn.ToOADate();
            var time = fn.ToOATime();
            var cevap = db.InsertIrsaliye(vUser.SirketKodu, DepoID, gorevno, gorevno, today, "Kablo Sayım", false, ComboItems.KabloSayım.ToInt32(), vUser.UserName, today, time, "", "", 0, "", "").FirstOrDefault();
            LogActions("WMS", "Purchase", "New", ComboItems.alEkle, cevap.GorevID.Value, "Kablo Sayım");


            foreach (var item in data)
            {
                string query = string.Format("select birim1 As Birim from FINSAT6{0}.FINSAT6{0}.STK WHERE MalKodu='{1}'", vUser.SirketKodu,item.MalKodu);
                birim = db.Database.SqlQuery<STKBirimMalKod>(query).FirstOrDefault().Birim;

                // yerleştirme kaydı yapılır
                var tmp2 = Yerlestirme.Detail(item.KatID, item.MalKodu, birim);
                if (tmp2 == null)
                {
                    tmp2 = new Yer()
                    {
                        KatID = item.KatID,
                        MalKodu = item.MalKodu,
                        Birim = birim,
                        Miktar = item.Miktar,
                        MakaraNo=item.MakaraNo
                    };
                    var s=Yerlestirme.Insert(tmp2, vUser.Id, "Kablo Sayım", cevap.IrsaliyeID.Value);
                }
                else
                {
                    if (item.Miktar > tmp2.Miktar)//giriş
                    {
                        tmp2.Miktar = item.Miktar;
                        Yerlestirme.Update(tmp2, vUser.Id, "Kablo Sayım", item.Miktar - item.Miktar, false, cevap.IrsaliyeID.Value);
                    }
                    else if (item.Miktar < tmp2.Miktar)//çıkış
                    {
                        tmp2.Miktar = item.Miktar;
                        Yerlestirme.Update(tmp2, vUser.Id, "Kablo Sayım", item.Miktar - item.Miktar, true, cevap.IrsaliyeID.Value);
                    }
                }
            }



            return Json(new Result(true, "Ok"), JsonRequestBehavior.AllowGet);
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
    }
}