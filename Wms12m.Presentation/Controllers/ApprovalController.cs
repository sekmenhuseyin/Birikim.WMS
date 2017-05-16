using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class ApprovalController : RootController
    {
        #region Çek
        public ActionResult Cek_SPGMY()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Cek_SPGMY_List()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[wms].[CekOnaySPGMY]", "33")).ToList();
            return PartialView(KOD);
        }

        public ActionResult Cek_MIGMY()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Cek_MIGMY_List()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[wms].[CekOnayMIGMY]", "33")).ToList();
            return PartialView(KOD);
        }

        public ActionResult Cek_GM()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Cek_GM_List()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[wms].[CekOnayGM]", "33")).ToList();
            return PartialView(KOD);
        }
        #endregion
        #region Fiyat
        public ActionResult Fiyat_GM()
        {
            return View();
        }
        public PartialViewResult Fiyat_GM_List()
        {
            var KOD = db.Database.SqlQuery<FiyatOnayGMSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayListGM]", "33")).ToList();
            return PartialView(KOD);
        }
        public ActionResult Fiyat_SPGMY()
        {
            return View();
        }
        public PartialViewResult Fiyat_SPGMY_List()
        {
            var KOD = db.Database.SqlQuery<FiyatOnayGMSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayListSPGMY]", "33")).ToList();
            return PartialView(KOD);
        }
        public ActionResult Fiyat_SM()
        {
            return View();
        }
        public PartialViewResult Fiyat_SM_List()
        {
            var KOD = db.Database.SqlQuery<FiyatOnayGMSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayList]", "33")).ToList();
            return PartialView(KOD);
        }
        #endregion
        #region Sipariş
        public ActionResult Siparis_SM()
        {
            return View();
        }
        public PartialViewResult Siparis_SM_List()
        {
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListSM]", "33")).ToList();
            return PartialView(KOD);
        }
        public ActionResult Siparis_SPGMY()
        {
            return View();
        }
        public PartialViewResult Siparis_SPGMY_List()
        {
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListSPGMY]", "33")).ToList();
            return PartialView(KOD);
        }
        public ActionResult Siparis_GM()
        {
            return View();
        }
        public PartialViewResult Siparis_GM_List()
        {
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListGM]", "33")).ToList();
            return PartialView(KOD);
        }
        public bool Siparis_Onay(string EvrakNo, string Kaydeden, int OnayTip, bool OnaylandiMi)
        {
            bool Result = true;
            if (OnayTip == 3 && OnaylandiMi == true)//GMOnay
            { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay]", "33", EvrakNo, vUser.UserName, 3, 1)); }
            if (OnayTip == 2 && OnaylandiMi == true)//SPGMYOnay
            { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay]", "33", EvrakNo, vUser.UserName, 2, 1)); }
            if (OnayTip == 1 && OnaylandiMi == true)//SMOnay
            { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay]", "33", EvrakNo, vUser.UserName, 1, 1)); }
            return Result;
        }
        #endregion
        #region Stok
        public ActionResult Stok()
        {
            return View();
        }
        public PartialViewResult StokList(string Durum)
        {
            int param = 1;
            if (Durum == "Tumu") { param = 0; }
            else
            if (Durum == "Onay") { param = 1; }
            else
            if (Durum == "Pasif") { param = 2; }
            else
            if (Durum == "Aktif") { param = 3; }
            else
            if (Durum == "Red") { param = 4; }
            var KOD = db.Database.SqlQuery<StokOnaySelect>(string.Format("[FINSAT6{0}].[wms].[StokOnaySelect] {1}", "33", param)).ToList();
            return PartialView("List", KOD);
        }
        #endregion
        #region Teminat
        public ActionResult Teminat()
        {
            return View();
        }
        public PartialViewResult TeminatList()
        {
            var KOD = db.Database.SqlQuery<TeminatOnaySelect>(string.Format("[FINSAT6{0}].[wms].[TeminatOnayList]", "33")).ToList();
            return PartialView(KOD);
        }
        public ActionResult TeminatTanim()
        {
            return View();
        }
        #endregion
        #region Sözleşme
        public ActionResult Sozlesme_GM()
        {
            return View();
        }
        [HttpPost]
        public PartialViewResult Sozlesme_GM_List()
        {
            List<SozlesmeOnaySelect> list = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnay]", "33")).ToList();
            return PartialView(list);
        }
        [HttpPost]
        public PartialViewResult Sozlesme_GM_Details(string ListeNo)
        {
            var list = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect1] '{1}'", "33", ListeNo)).ToList();
            return PartialView(list);
        }
        public ActionResult Sozlesme_SM()
        {
            return View();
        }
        public PartialViewResult Sozlesme_SM_List()
        {
            List<SozlesmeOnaySelect> list = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnaySM]", "33")).ToList();
            return PartialView(list);
        }
        [HttpPost]
        public PartialViewResult Sozlesme_SM_Details(string ListeNo)
        {
            var list = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect1] '{1}'", "33", ListeNo)).ToList();
            return PartialView(list);
        }
        public ActionResult Sozlesme_SPGMY()
        {
            return View();
        }
        public PartialViewResult Sozlesme_SPGMY_List()
        {
            List<SozlesmeOnaySelect> list = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnaySPGMY]", "33")).ToList();
            return PartialView(list);
        }
        [HttpPost]
        public PartialViewResult Sozlesme_SPMY_Details(string ListeNo)
        {
            var list = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect1] '{1}'", "33", ListeNo)).ToList();
            return PartialView(list);
        }
        #endregion
    }
}