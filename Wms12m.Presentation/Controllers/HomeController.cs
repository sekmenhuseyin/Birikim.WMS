using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
	public class HomeController : RootController
	{
		/// <summary>
		/// Anasayfa
		/// </summary>
		public ActionResult Index()
		{
			var ozet = db.GetHomeSummary().FirstOrDefault();
			return View("Index", ozet);
		}
		/// <summary>
		/// child view for menu
		/// </summary>
		/// <param name="mYeri">menu yeri no</param>
		/// <param name="mUstNo">üst menu no</param>
		/// <param name="url">current url</param>
		[ChildActionOnly]
		public PartialViewResult Menu(byte mYeri, short mUstNo, string url)
		{
			for (int i = 0; i < 3; i++)
			{
				var ind = url.IndexOf("/");
				url = url.Right(url.Length - ind - 1);
			}
			url = "/" + url;
			ViewBag.ustMenu = mUstNo;
			ViewBag.aktifNo = db.MenuFindAktif(ComboItems.WMS.ToInt32(), mYeri, vUser.RoleName, mUstNo, url).FirstOrDefault();
			var tablo = db.MenuGetirici(ComboItems.WMS.ToInt32(), mYeri, vUser.RoleName, mUstNo).ToList();
			return PartialView("../Shared/_MenuList", tablo);
		}
		public PartialViewResult PartialGunlukSatis(int? tarih)
		{
			var tarih2 = DateTime.Today.Date;
			if (tarih == null)
			{
				tarih = DateTime.Today.ToOADate().ToInt32();
													  
			}
			else
			{
				tarih2 = tarih.Value.FromOaDate().Date;
			}
			var GSA = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[dbo].[DB_GunlukSatisAnalizi] @Tarih = {1}", "33", tarih)).ToList();


			ViewBag.Tarih = tarih2;
			return PartialView("_PartialGunlukSatis", GSA);
		}

		public PartialViewResult PartialGunlukSatisPie(int? tarih)
		{
			var tarih2 = DateTime.Today.Date;
			if (tarih == null)
			{
				tarih = DateTime.Today.ToOADate().ToInt32();
			}
			else
			{
				tarih2 = tarih.Value.FromOaDate().Date;
			}
			var GSA = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[dbo].[DB_GunlukSatisAnalizi] @Tarih = {1}", "33", tarih)).ToList();


			ViewBag.Tarih = tarih2;
			return PartialView("_PartialGunlukSatisPie", GSA);
		}

		public PartialViewResult PartialGunlukSatisYearToDay(int? tarih)
		{
			var GSA = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[dbo].[DB_GunlukSatisAnaliziYearToDay] @Tarih = {1}", "33", tarih)).ToList();
			return PartialView("_PartialGunlukSatısAnaliziYearToDay", GSA);
		}

		public PartialViewResult PartialGunlukSatisYearToDayPie(int? tarih)
		{

			var GSA = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[dbo].[DB_GunlukSatisAnaliziYearToDay] @Tarih = {1}", "33", tarih)).ToList();
			return PartialView("_PartialGunlukSatısAnaliziYearToDayPie", GSA);
		}

		public PartialViewResult PartialAylikSatis()
		{
			var ASA = db.Database.SqlQuery<ChartAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[dbo].[DB_Aylik_SatisAnalizi]", "33")).ToList();
			return PartialView("_PartialAylikSatis", ASA);
		}

		public PartialViewResult PartialAylikSatisAnaliziBar()
		{
			var ASA = db.Database.SqlQuery<ChartAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[dbo].[DB_Aylik_SatisAnalizi]", "33")).ToList();
			return PartialView("_PartialAylikSatisAnaliziBar", ASA);
		}

		public PartialViewResult PartialAylikSatisCHKAnaliziBar(string chk)
		{
			//var ASA = new ChartAylikSatisAnalizi();
			var chk2 = "";
			if (chk == null)
			{
				chk = "";
			}
			else
			{
				chk2 = chk;
			}
			var ASA = db.Database.SqlQuery<ChartAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[dbo].[DB_Aylik_SatisAnalizi_CHK] @chk='{1}'", "33", chk)).ToList();
			ViewBag.CHK = chk2;
			return PartialView("_PartialAylikSatisCHKAnaliziBar", ASA);
		}

		public PartialViewResult PartialUrunGrubuSatis(short? tarih)
		{
			if (tarih == null)
				tarih = (short)DateTime.Today.Month;
			var UGS = db.Database.SqlQuery<ChartUrunGrubuSatisAnalizi>(string.Format("[FINSAT6{0}].[dbo].[DB_UrunGrubu_SatisAnalizi] @Ay = {1}", "33", tarih)).ToList();
			ViewBag.Tarih = tarih;
			return PartialView("_PartialUrunGrubuSatis", UGS);
		}

		public PartialViewResult PartialLokasyonSatis(short? tarih)
		{
			if (tarih == null)
				tarih = (short)DateTime.Today.Month;
			var UGS = db.Database.SqlQuery<ChartUrunGrubuSatisAnalizi>(string.Format("[FINSAT6{0}].[dbo].[DB_LokasyonBazli_SatisAnalizi] @Ay = {1}", "33", tarih)).ToList();
			ViewBag.Tarih = tarih;
			return PartialView("_PartialLokasyonSatis", UGS);
		}

		public PartialViewResult PartialBakiyeRiskAnalizi()
		{
			var BRA = db.Database.SqlQuery<ChartBakiyeRiskAnalizi>(string.Format("[FINSAT6{0}].[dbo].[DB_BakiyeRiskAnalizi]", "33")).ToList();
			return PartialView("_PartialBakiyeRiskAnalizi", BRA);
		}

		public PartialViewResult PartialBekleyenSiparisUrunGrubu(string chk, int? bastarih, int? bittarih)
		{
			var tarih1 = DateTime.Today.Date;
			var tarih2 = DateTime.Today.Date;
			if (bastarih == null)
			{
				bastarih = DateTime.Today.ToOADate().ToInt32();
			}
			else
			{
				tarih1 = bastarih.Value.FromOaDate().Date;
			}

			if (bittarih == null)
			{
				bittarih = DateTime.Today.ToOADate().ToInt32();
			}
			else
			{
				tarih2 = bittarih.Value.FromOaDate().Date;
			}

			ViewBag.BasTarih = tarih1;
			ViewBag.BitTarih = tarih2;

			var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[dbo].[DB_BekleyenSiparis_UrunGrubu] @BasTarih = {1}, @BitTarih = {2}", chk, bastarih, bittarih)).ToList();
			return PartialView("_PartialBekleyenSiparisUrunGrubu", BSUG);
		}


		public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktar(bool miktarTutar)
		{

			if (miktarTutar == true)
			{
				var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[dbo].[DB_BekleyenSiparis_UrunGrubu_Miktar]", "33")).ToList();
				ViewBag.MiktarTutar = "Miktar";
				return PartialView("_PartialBekleyenSiparisUrunGrubuMiktar", BSUG);
			}
			else
			{
				var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[dbo].[DB_BekleyenSiparis_UrunGrubu_Fiyat]", "33")).ToList();
				ViewBag.MiktarTutar = "Tutar";
				return PartialView("_PartialBekleyenSiparisUrunGrubuMiktar", BSUG);
			}
		}

		public string CHKSelect()
		{
			var CHK = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[dbo].[CHKSelectKartTip]", "33")).ToList();
			var json = new JavaScriptSerializer().Serialize(CHK);
			return json;
		}
	}
}