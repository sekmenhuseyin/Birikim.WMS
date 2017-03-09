using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class SellController : RootController
    {
        /// <summary>
        /// sipariş planlama sayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return View("Index");
        }
        /// <summary>
        /// siparişleri seçince, siparişlerdeki tüm malzemeler gelecek
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step2(frmSiparisOnay tbl)
        {
            using (DinamikModelContext Dinamik = new DinamikModelContext(tbl.SirketID))
            {
                string[] evraks = tbl.checkboxes.Split(',');
                var list = (from s in Dinamik.Context.SPIs
                            join s2 in Dinamik.Context.STKs on s.MalKodu equals s2.MalKodu
                            where evraks.Contains(s.EvrakNo) && s.SiparisDurumu == 0 && s.KynkEvrakTip == 62 && s.Depo == tbl.DepoID && (s.BirimMiktar - s.TeslimMiktar - s.KapatilanMiktar) > 0
                            select new frmSiparisMalzemeDetay { EvrakNo = s.EvrakNo, Tarih = s.Tarih, MalKodu = s.MalKodu, MalAdi = s2.MalAdi, Miktar = (s.BirimMiktar - s.TeslimMiktar - s.KapatilanMiktar), Birim = s.Birim }).ToList();
                var list2 = db.Yers.ToList();
                var list3 = (from s in list
                             join s2 in list2 on new { s.Birim, s.MalKodu }  equals new { s2.Birim, s2.MalKodu }
                             select new frmSiparisMalzemeDetay { EvrakNo = s.EvrakNo, Tarih = s.Tarih, MalKodu = s.MalKodu, MalAdi = s.MalAdi, Miktar = s.Miktar, Birim = s.Birim, Stok = s2.Miktar }).ToList();
                ViewBag.SirketID = tbl.SirketID;
                ViewBag.DepoID = tbl.DepoID;
                ViewBag.EvrakNos = tbl.checkboxes;
                return View("Step2", list3);
            }
        }
        /// <summary>
        /// seçili malzemeler gruplanmış olarak gelecek
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step3(frmSiparisOnay tbl)
        {
            using (DinamikModelContext Dinamik = new DinamikModelContext(tbl.SirketID))
            {
                string[] mals = tbl.checkboxes.Split(',');
                string[] evraks = tbl.EvrakNos.Split(',');
                var list = from s in Dinamik.Context.SPIs
                            join s2 in Dinamik.Context.STKs on s.MalKodu equals s2.MalKodu
                            where evraks.Contains(s.EvrakNo) && mals.Contains(s.MalKodu) && s.SiparisDurumu == 0 && s.KynkEvrakTip == 62 && s.Depo == tbl.DepoID && (s.BirimMiktar - s.TeslimMiktar - s.KapatilanMiktar) > 0
                            group new { s, s2 } by new { s.MalKodu, s2.MalAdi, s.Birim } into g
                            select new frmSiparisMalzeme { MalKodu = g.Key.MalKodu, MalAdi = g.Key.MalAdi, Miktar = g.Sum(m => m.s.BirimMiktar - m.s.TeslimMiktar - m.s.KapatilanMiktar), Birim = g.Key.Birim };
                ViewBag.SirketID = tbl.SirketID;
                ViewBag.EvrakNos = tbl.EvrakNos;
                ViewBag.DepoID = tbl.DepoID;
                ViewBag.checkboxes = tbl.checkboxes;
                return View("Step3", list.ToList());
            }
        }
        /// <summary>
        /// sipariş onaylandı
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Approve(frmSiparisOnay tbl)
        {
            using (DinamikModelContext Dinamik = new DinamikModelContext(tbl.SirketID))
            {
                string[] mals = tbl.checkboxes.Split(',');
                string[] evraks = tbl.EvrakNos.Split(',');
                string chk = ""; int id = 0; Result _Result;
                var list = (from s in Dinamik.Context.SPIs
                           join s2 in Dinamik.Context.CHKs on s.Chk equals s2.HesapKodu
                           where evraks.Contains(s.EvrakNo) && mals.Contains(s.MalKodu) && s.SiparisDurumu == 0 && s.KynkEvrakTip == 62 && s.Depo == tbl.DepoID && (s.BirimMiktar - s.TeslimMiktar - s.KapatilanMiktar) > 0
                           orderby s.Chk
                           select new { s.EvrakNo, s.Chk, s2.Unvan1, s.MalKodu, Miktar = s.BirimMiktar - s.TeslimMiktar - s.KapatilanMiktar, s.Birim }).ToList();
                foreach (var item in list)
                {
                    //irsaliye tablosu
                    if (chk != item.Chk)
                    {
                        IR irs = new IR();
                        irs.SirketKod = tbl.SirketID;
                        irs.DepoID = db.Depoes.Where(m => m.DepoKodu == tbl.DepoID).Select(m => m.ID).FirstOrDefault();
                        irs.IslemTur = true; //satış irsaliyesi
                        irs.Tarih = DateTime.Today.ToOADateInt();
                        irs.EvrakNo = db.SettingsIrsaliyeNo(DateTime.Today.ToOADateInt()).FirstOrDefault();
                        irs.HesapKodu = item.Chk;
                        chk = item.Chk;
                        var op = new Irsaliye();
                        _Result = op.Operation(irs);
                        id = _Result.Id;
                        //görev tablosu
                        Gorev grv = new Gorev();
                        grv.DepoID = irs.DepoID;
                        grv.GorevNo = db.SettingsGorevNo(DateTime.Today.ToOADateInt()).FirstOrDefault();
                        grv.GorevTipiID = ComboItems.SiparişTopla.ToInt32();
                        grv.Bilgi = "Irs: " + irs.EvrakNo + ", Alıcı: " + item.Unvan1;
                        grv.IrsaliyeID = id;
                        var op3 = new Task();
                        _Result = op3.Operation(grv);
                    }
                    //sti tablosu
                    IRS_Detay sti = new IRS_Detay();
                    sti.IrsaliyeID = id;
                    sti.MalKodu = item.MalKodu;
                    sti.Birim = item.Birim;
                    sti.Miktar = item.Miktar;
                    var op2 = new Stok();
                    _Result = op2.Operation(sti);
                }

                return Redirect("/Gorev");
            }
        }
        /// <summary>
        /// get depo names based on şirket
        /// </summary>
        [HttpPost]
        public JsonResult GetDepo()
        {
            var ID = Url.RequestContext.RouteData.Values["id"];
            if (ID == null || ID.ToString2() == "0") return null;
            //connect
            using (DinamikModelContext Dinamik = new DinamikModelContext(ID.ToString()))
            {
                var list = Dinamik.Context.DEPs.Select(m => new { m.Depo, m.DepoAdi }).ToList();
                return Json(list.Select(x => new { Value = x.Depo, Text = x.DepoAdi, Selected = 0 }), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// depo ve şirket seçince açık siparişler gelecek
        /// </summary>
        [HttpPost]
        public PartialViewResult GetSiparis()
        {
            var ID = Url.RequestContext.RouteData.Values["id"];
            if (ID == null || ID.ToString2().Contains("-") == false) return null;
            string[] tmp = ID.ToString().Split('-');
            string kod = tmp[0]; if (kod == "0") return null;
            string depo = tmp[1]; if (depo == "0") return null;
            using (DinamikModelContext Dinamik = new DinamikModelContext(kod))
            {
                var list = Dinamik.Context.SPIs
                            .Where(m => m.Depo == depo && m.KynkEvrakTip == 62 && m.SiparisDurumu == 0 && (m.BirimMiktar - m.TeslimMiktar - m.KapatilanMiktar) > 0)
                            .GroupBy(m=> new { m.EvrakNo, m.Tarih })
                            .Select(m => new frmSiparisler { EvrakNo = m.Key.EvrakNo, Tarih = m.Key.Tarih })
                            .OrderByDescending(m=>m.Tarih);
                return PartialView("_Siparis", list.ToList());
            }
        }
    }
}