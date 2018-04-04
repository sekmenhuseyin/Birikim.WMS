using Birikim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Entity.Mysql;

namespace Wms12m.Presentation.Areas.WMS.Controllers
{
    public class CableStockTransferController : RootController
    {
        // GET: WMS/CableStockTransfer
        public ActionResult Index()
        {
            ViewBag.DepoID = new SelectList(Store.GetListCable(vUser.DepoId), "ID", "DepoAd");
            return View();
        }

        public PartialViewResult GetListOfMySQL(int DepoID)
        {
            var depoAd = Store.Detail(DepoID).DepoAd.ToUpper();
            List<MySQLDataViewModel> data = new List<MySQLDataViewModel>();
            using (var dbx = new KabloEntities())
            {
                //MakaraNo Olmayanlar ve MakaraNo eşleşip miktarı eşit olmayanları stoklar list'ine atar
                List<string> listYer = db.Yers.Where(m => m.DepoID == DepoID).Select(x => x.MakaraNo).ToList();
                List<kblstok2> stoklar = new List<kblstok2>();
                foreach (var item in dbx.kblstok2.Where(m => m.depo == depoAd))//mysql stoklar üzerinde dön
                    if (!listYer.Contains(item.makarano))//maraka no yoksa listeye ekle
                        stoklar.Add(item);
                    else//MakaraNo eşleşip miktarı eşit olmayanları
                        foreach (var itemYer in db.Yers.Where(x => x.DepoID == DepoID & x.MakaraNo.Contains(item.makarano)))
                            if (itemYer.Miktar != item.miktar)
                                stoklar.Add(item);
                //add to final list
                foreach (var item in stoklar)
                    data.Add(
                        new MySQLDataViewModel()
                        {
                            MySQLID = item.id,
                            Marka = item.marka,
                            Cins = item.cins,
                            Kesit = item.kesit,
                            Makara = item.makara,
                            MakaraNo = item.makarano,
                            DepoID = item.depo,
                            Miktar = item.miktar.Value
                        });
            }
            return PartialView(data);
        }

        public JsonResult MysqlKaydet(List<MySQLDataViewModel> data)
        {

            if (data.Count(x => string.IsNullOrEmpty(x.MakaraNo)) != 0)//EKLE
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

                string query = string.Format("select birim1 As Birim from FINSAT6{0}.FINSAT6{0}.STK WHERE MalKodu='{1}'", vUser.SirketKodu, item.MalKodu);
                birim = db.Database.SqlQuery<STKBirimMalKod>(query).FirstOrDefault().Birim;

                // yerleştirme kaydı yapılır
                var tmp2 = Yerlestirme.Detail(item.KatID, item.MalKodu, "", item.MakaraNo);
                if (tmp2 == null)
                {
                    var makarakontrol = db.Yers.Where(m => m.DepoID == DepoID && m.MakaraNo == item.MakaraNo).FirstOrDefault();
                    if (makarakontrol == null)
                    {
                        tmp2 = new Yer()
                        {
                            KatID = item.KatID,
                            MalKodu = item.MalKodu,
                            Birim = birim,
                            Miktar = item.Miktar,
                            MakaraNo = item.MakaraNo
                        };
                        Yerlestirme.Insert(tmp2, vUser.Id, "Kablo Sayım", cevap.IrsaliyeID.Value);
                    }
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
    }
}