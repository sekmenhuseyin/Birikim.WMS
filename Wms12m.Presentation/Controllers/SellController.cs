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
            ViewBag.DepoID = new SelectList(Store.GetList(), "DepoKodu", "DepoAd");
            return View("Index");
        }
        /// <summary>
        /// seçili malzemeler gruplanmış olarak gelecek
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step2(frmSiparisOnay tbl)
        {
            using (DinamikModelContext Dinamik = new DinamikModelContext(tbl.SirketID))
            {
                string[] evraks = tbl.checkboxes.Split('#');
                var list = (from s in Dinamik.Context.SPIs
                            join s2 in Dinamik.Context.STKs on s.MalKodu equals s2.MalKodu
                            where evraks.Contains(s.EvrakNo) && s.SiparisDurumu == 0 && s.KynkEvrakTip == 62 && s.Depo == tbl.DepoID && (s.BirimMiktar - s.TeslimMiktar - s.KapatilanMiktar) > 0
                            group new { s, s2 } by new { s.MalKodu, s2.MalAdi, s.Birim } into g
                            select new { MalKodu = g.Key.MalKodu, MalAdi = g.Key.MalAdi, Miktar = g.Sum(m => m.s.BirimMiktar - m.s.TeslimMiktar - m.s.KapatilanMiktar), Birim = g.Key.Birim }).ToList();
                var list2 = (from s in db.Yers
                            where s.Kat.Bolum.Raf.Koridor.Depo.DepoKodu == tbl.DepoID
                            group s by new { s.Birim, s.MalKodu } into g
                            select new { g.Key.MalKodu, g.Key.Birim, Miktar = g.Sum(m => m.Miktar) }).ToList();
                var list3 = (from s in list
                             join s2 in list2 on new { s.Birim, s.MalKodu } equals new { s2.Birim, s2.MalKodu }
                             orderby s.MalKodu
                             select new frmSiparisMalzeme { MalKodu = s.MalKodu, MalAdi = s.MalAdi, Miktar = s.Miktar > s2.Miktar ? s2.Miktar : s.Miktar, Birim = s.Birim, Stok = s2.Miktar }).ToList();
                ViewBag.SirketID = tbl.SirketID;
                ViewBag.EvrakNos = tbl.checkboxes;
                ViewBag.DepoID = tbl.DepoID;
                return View("Step2", list3);
            }
        }
        /// <summary>
        /// siparişleri seçince, siparişlerdeki tüm malzemeler gelecek
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step3(frmSiparisOnay tbl)
        {
            using (DinamikModelContext Dinamik = new DinamikModelContext(tbl.SirketID))
            {
                string[] mals = tbl.checkboxes.Split('#');
                string[] evraks = tbl.EvrakNos.Split('#');
                var list = (from s in Dinamik.Context.SPIs
                            join s2 in Dinamik.Context.STKs on s.MalKodu equals s2.MalKodu
                            where evraks.Contains(s.EvrakNo) && mals.Contains(s.MalKodu)  && s.SiparisDurumu == 0 && s.KynkEvrakTip == 62 && s.Depo == tbl.DepoID && (s.BirimMiktar - s.TeslimMiktar - s.KapatilanMiktar) > 0
                            select new { ID = s.ROW_ID, EvrakNo = s.EvrakNo, Tarih = s.Tarih, MalKodu = s.MalKodu, MalAdi = s2.MalAdi, Miktar = (s.BirimMiktar - s.TeslimMiktar - s.KapatilanMiktar), Birim = s.Birim }).ToList();
                var list2 = (from s in db.Yers
                            where s.Kat.Bolum.Raf.Koridor.Depo.DepoKodu == tbl.DepoID
                            group s by new { s.Birim, s.MalKodu } into g
                            select new { g.Key.MalKodu, g.Key.Birim, Miktar = g.Sum(m => m.Miktar) }).ToList();
                var list3 = (from s in list
                             join s2 in list2 on new { s.Birim, s.MalKodu }  equals new { s2.Birim, s2.MalKodu }
                             orderby s.MalKodu
                             select new frmSiparisMalzemeDetay { ID = s.ID, EvrakNo = s.EvrakNo, Tarih = s.Tarih, MalKodu = s.MalKodu, MalAdi = s.MalAdi, Miktar = s.Miktar, Birim = s.Birim, Stok = s2.Miktar }).ToList();
                ViewBag.SirketID = tbl.SirketID;
                ViewBag.DepoID = tbl.DepoID;
                ViewBag.EvrakNos = tbl.EvrakNos;
                return View("Step3", list3);
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
                string[] ids = (tbl.checkboxes + "0").Split('#');
                int[] myInts = Array.ConvertAll(ids, int.Parse);
                string[] evraks = tbl.EvrakNos.Split('#');
                string chk = "", evrakNo = ""; Result _Result;
                string evraknolar = "", alıcılar = "";
                int idDepo = db.Depoes.Where(m => m.DepoKodu == tbl.DepoID).Select(m => m.ID).FirstOrDefault();
                string GorevNo = db.SettingsGorevNo(DateTime.Today.ToOADateInt()).FirstOrDefault();
                int today = DateTime.Today.ToOADateInt();
                int time = DateTime.Now.SaatiAl();
                InsertIrsaliye_Result cevap = new InsertIrsaliye_Result();
                //listeler
                var list = (from s in Dinamik.Context.SPIs
                            join s2 in Dinamik.Context.CHKs on s.Chk equals s2.HesapKodu
                            where evraks.Contains(s.EvrakNo) && myInts.Contains(s.ROW_ID) && s.SiparisDurumu == 0 && s.KynkEvrakTip == 62 && s.Depo == tbl.DepoID && (s.BirimMiktar - s.TeslimMiktar - s.KapatilanMiktar) > 0
                            group new { s, s2 } by new { s.EvrakNo, s.Chk, s2.Unvan1, s.MalKodu, s.Birim } into g
                            orderby g.Key.Chk
                            select new { g.Key.EvrakNo, g.Key.Chk, g.Key.Unvan1, g.Key.MalKodu, Miktar = g.Sum(m => m.s.BirimMiktar - m.s.TeslimMiktar - m.s.KapatilanMiktar), g.Key.Birim }).ToList();
                var list2 = (from s in db.Yers
                             where s.Kat.Bolum.Raf.Koridor.Depo.DepoKodu == tbl.DepoID
                             group s by new { s.Birim, s.MalKodu } into g
                             select new { g.Key.MalKodu, g.Key.Birim, Miktar = g.Sum(m => m.Miktar) }).ToList();
                var list3 = (from s in list
                             join s2 in list2 on new { s.Birim, s.MalKodu } equals new { s2.Birim, s2.MalKodu }
                             select new { s.EvrakNo, s.Chk, s.Unvan1, s.MalKodu, Miktar = s.Miktar > s2.Miktar ? s2.Miktar : s.Miktar, s.Birim }).ToList();
                foreach (var item in list3)
                {
                    //irsaliye tablosu
                    if (chk != item.Chk)
                    {
                        evrakNo = db.SettingsIrsaliyeNo(DateTime.Today.ToOADateInt()).FirstOrDefault();
                        cevap = db.InsertIrsaliye(tbl.SirketID, idDepo, GorevNo, evrakNo, "", true, ComboItems.SiparişTopla.ToInt32(), User.Id, User.UserName, today, time, item.Chk).FirstOrDefault();
                        //save sck
                        chk = item.Chk;
                        evraknolar += evrakNo + ",";
                        alıcılar += item.Unvan1 + ",";
                    }
                    //sti tablosu
                    IRS_Detay sti = new IRS_Detay();
                    sti.IrsaliyeID = cevap.IrsaliyeID.Value;
                    sti.MalKodu = item.MalKodu;
                    sti.Birim = item.Birim;
                    sti.Miktar = item.Miktar;
                    var op2 = new Stok();
                    _Result = op2.Operation(sti);
                }
                //görev tablosu
                Gorev grv = db.Gorevs.Where(m => m.ID == cevap.GorevID).FirstOrDefault();
                grv.Bilgi = "Irs: " + evraknolar + " Alıcı: " + alıcılar;
                db.SaveChanges();
                return Redirect("/Gorev");
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