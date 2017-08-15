using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class TumOrderController : RootController
    {
        // GET: Approvals/IzOrder
        public ActionResult Index(string onayRed)
        {
            if (CheckPerm(Perms.TechnoIKOnaylama, PermTypes.Reading) == false) return Redirect("/");
            if (onayRed == null)
            {
                ViewBag.OnayDurum = "Beklemede";
            }
            else
            {
                ViewBag.OnayDurum = onayRed;
            }
            return View();
        }

        public PartialViewResult TumOrderList(string Tip)
        {
            if (CheckPerm(Perms.TechnoIKOnaylama, PermTypes.Reading) == false) return null;
            ViewBag.Tip = Tip;
            return PartialView("List");
        }

        public string TumOrderListData(string tip)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            List<SipOnay> sipBilgi;
            try
            {
                sipBilgi = db.Database.SqlQuery<SipOnay>(string.Format(@"
select 
convert(bit, 'False') as Secim,
a.Chk as HesapKodu,
max(d.Unvan1 + ' ' + d.Unvan2) as Unvan,
d.TipKod as TipKodu,
d.KrediLimiti as KrediLimiti,
(d.DvrB + d.OdemeB + (d.CiroB + IadeFatB) + d.KDVB + d.DigerB) - (d.DvrA + d.OdemeA + (d.CiroA + d.IadeFatA) + d.KDVA + d.DigerA) as Bakiye,
0.00 as PRTBakiye,
0.00 as Risk,
0.00 as SCek,
0.00 as TCek,
d.Kod2 as Kod2,
0.00 as OrtGun,
0.00 as Kod3OrtGun,
0.00 as Kod3OrtBakiye,
isnull((select Sum(t.Kod14) from FINSAT671.FINSAT671.SPI (nolock)t where t.Chk = a.Chk and t.EvrakNo = a.EvrakNo and t.Tarih = 42961 and t.SiparisDurumu = 0 and t.Kod10 = 'Beklemede'), 0) as SicakSiparis,
isnull((select Sum(t.Kod14) from FINSAT671.FINSAT671.SPI (nolock)t where t.Chk = a.Chk and t.SiparisDurumu = 0 and t.Tarih < 42961 and t.EvrakNo = a.EvrakNo and t.Kod10 = 'Beklemede'), 0) as SogukSiparis ,
isnull((select Sum(t.Kod14) from FINSAT671.FINSAT671.SPI (nolock)t where t.Chk = a.Chk and t.SiparisDurumu = 0 and t.Kod10 <> 'Reddedildi' and t.Tarih < 42961),0) as GunIciSiparis,
case a.Tarih when 42961 then 'SICAK' else 'SOĞUK' end as SiparisTuru,
a.EvrakNo as EvrakNo,
a.Kod10 as OnayDurumu,
Convert(nvarchar(10), cast(a.Tarih as datetime) - 2, 104) as Tarih,
a.Kod3 as Firma,
a.Kod2 as Onaylayan
from FINSAT671.FINSAT671.SPI (nolock)a
inner join FINSAT671.FINSAT671.CHK(nolock) d on d.HesapKodu = a.Chk
group by a.Chk,d.TipKod,d.KrediLimiti,(d.DvrB + d.OdemeB + (d.CiroB + IadeFatB) + d.KDVB + d.DigerB) - (d.DvrA + d.OdemeA + (d.CiroA + d.IadeFatA) + d.KDVA + d.DigerA),d.Kod2,a.Tarih,a.EvrakNo,a.Kod10,a.Kod3,a.Kod2 ")).ToList();
            }
            catch (Exception ex)
            {
                sipBilgi = new List<Entity.SipOnay>();
            }
            return json.Serialize(sipBilgi);
        }
    }
}