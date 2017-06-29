using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m
{
    /// <summary>
    /// Summary description for Mobile
    /// </summary>
    [WebService(Namespace = "http://www.12mconsulting.com.tr/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Mobile : BaseService
    {
        /// <summary>
        /// login işlemleri
        /// </summary>
        [WebMethod]
        public Login LoginKontrol(string userID, string sifre, string AndroidID, string AuthGiven)
        {
            if (AuthGiven.Cozumle() != AuthPass) return new Login() { ID = 0, AdSoyad = "Yetkisiz giriş!" };
            //new user
            var user = new User() { Kod = userID.Left(5), Sifre = sifre };
            //log in actions
            var person = new Persons();
            var result = person.Login(user, AndroidID);
            //check result
            if (result.Id > 0)
            {
                try
                {
                    return db.Users.Where(m => m.ID == result.Id).Select(m => new Login { ID = m.ID, Kod = m.Kod, AdSoyad = m.AdSoyad, Guid = m.Guid.ToString() }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Service/Mobile/Login", userID);
                    db.LogLogins(userID, "Mobile", false, result.Message);
                }
            }
            else
                db.LogLogins(userID, "Mobile", false, result.Message);
            return new Login() { ID = 0, AdSoyad = "Hatalı Kullanıcı adı ve şifre" };
        }
        /// <summary>
        /// kullanıcıya ait müşterileri getir
        /// </summary>
        [WebMethod]
        public List<string> GetClients(string user, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new List<string>();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new List<string>();
            //return
            var tblx = dby.CAR002.Where(m => m.CAR002_OzelKodu == user).Select(m => new { m.CAR002_HesapKodu, m.CAR002_Unvan1, CariTipi = m.CAR002_Kod2 }).ToList();
            return new List<string>();
        }
        /// <summary>
        /// malları getirir
        /// </summary>
        [WebMethod]
        public List<frmUrunler> GetMals(string search, int KullID, string AuthGiven, string Guid)
        {
            //kontrol
            if (AuthGiven.Cozumle() != AuthPass) return new List<frmUrunler>();
            Guid = Guid.Cozumle();
            var tbl = db.Users.Where(m => m.ID == KullID && m.Guid.ToString() == Guid).FirstOrDefault();
            if (tbl == null) return new List<frmUrunler>();
            //return
            var tmp = dby.STK004.Select(m => new frmUrunler { MalKodu = m.STK004_MalKodu, MalAdi = m.STK004_Aciklama, GrupKodu = m.STK004_GrupKodu, Birim1 = m.STK004_Birim1, Birim2 = m.STK004_Birim2 }).Take(20);
            List<frmUrunler> tblx;
            if (search != "")
                tblx = tmp.Where(m => m.MalKodu.Contains(search)).ToList();
            else
                tblx = tmp.ToList();
            return tblx;
        }
    }
}
