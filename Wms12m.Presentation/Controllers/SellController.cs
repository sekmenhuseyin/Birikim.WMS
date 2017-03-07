using System;
using System.Linq;
using Wms12m.Entity;
using System.Web.Mvc;
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
        /// siparişleri seçince yapılacak işlemler
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step2(frmSiparisOnay tbl)
        {
            using (DinamikModelContext Dinamik = new DinamikModelContext(tbl.SirketID))
            {
                string[] evraks = tbl.checkboxes.Split(',');
                var list = from s in Dinamik.Context.SPIs
                            join s2 in Dinamik.Context.STKs on s.MalKodu equals s2.MalKodu
                            where evraks.Contains(s.EvrakNo) && s.SiparisDurumu == 0 && s.KynkEvrakTip == 62 && s.Depo == tbl.DepoID && (s.BirimMiktar - s.TeslimMiktar - s.KapatilanMiktar) > 0
                            group new { s, s2 } by new { s.MalKodu, s2.MalAdi, s.Birim } into g
                            select new frmSiparisMalzeme { MalKodu = g.Key.MalKodu, MalAdi = g.Key.MalAdi, Miktar = g.Sum(m => m.s.BirimMiktar - m.s.TeslimMiktar - m.s.KapatilanMiktar), Birim = g.Key.Birim };
                //var list2 = from s in db.Yerlestirmes
                //            join s2 in list on s.MalKodu equals s2.MalKodu
                //            where s.TK_KAT.TK_BOL.TK_RAF.TK_KOR.TK_DEP.Depo == tbl.DepoID
                //            select new frmSiparisMalzeme { MalKodu = s2.MalKodu, MalAdi = s2.MalAdi, Miktar = s2.Miktar, Birim = s2.Birim };
                ViewBag.SirketID = tbl.SirketID;
                ViewBag.DepoID = tbl.DepoID;
                ViewBag.EvrakNos = evraks;
                return View("Step2", list.ToList());
            }
        }
        /// <summary>
        /// malzemeleri seçince olanlar
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
                            select new frmSiparisMalzemeDetay { EvrakNo=s.EvrakNo, Tarih=s.Tarih, MalKodu = s.MalKodu, MalAdi = s2.MalAdi, Miktar = (s.BirimMiktar - s.TeslimMiktar - s.KapatilanMiktar), Birim = s.Birim };
                return View("Step3", list.ToList());
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