using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class CalendarController : RootController
    {
        // GET: Calendar
        public ActionResult Index()
        {
            var liste = db.Etkinliks.Where(m => m.Tekrarlayan == false && m.Onay == true).ToList();
            var tekrarlayan = db.Etkinliks.Where(m => m.Tekrarlayan == true).ToList();
            foreach (var item in tekrarlayan)
            {
                var fark = DateTime.Today.Year - item.Tarih.Year + 1;
                for (int i = 0; i <= fark; i++)
                {
                    var item2 = new Etkinlik()
                    {
                        ID = item.ID,
                        Username = item.Username,
                        TatilTipi = item.TatilTipi,
                        Tarih = item.Tarih.AddYears(i),
                        Aciklama = item.Aciklama + " (" + i + ". Tekrar)",
                        Tekrarlayan = item.Tekrarlayan,
                        ComboItem_Name = item.ComboItem_Name
                    };
                    liste.Add(item2);
                }
            }
            return View("Index", liste);
        }
    }
}