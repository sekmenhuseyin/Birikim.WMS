﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m
{
    /// <summary>
    /// Summary description for Android
    /// </summary>
    [WebService(Namespace = "http://www.12mconsulting.com.tr/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Android : BaseService
    {
        /// <summary>
        /// login işlemleri
        /// </summary>
        [WebMethod]
        public Login LoginKontrol(string userID, string sifre, string AndroidID)
        {
            //new user
            var user = new User() { Kod = userID.Left(5), Sifre = sifre };
            //log in actions
            var person = new Persons();
            var result = person.Login(user);
            //check result
            if (result.Id > 0)
            {
                try
                {
                    db.LogLogins(userID, "Android", true, "");
                    return db.Users.Where(m => m.ID == result.Id).Select(m => new Login { ID = m.ID, Kod = m.Kod, AdSoyad = m.AdSoyad, Guid = m.Guid.ToString() }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Service/Android/Login", userID);
                    db.LogLogins(userID, "Android", false, result.Message);
                }
            }
            else
                db.LogLogins(userID, "Android", false, result.Message);
            return new Login() { ID = 0, AdSoyad = "Hatalı Kullanıcı adı ve şifre" };
        }
        /// <summary>
        /// kullanıcıya ait müşterileri getir
        /// </summary>
        [WebMethod]
        public List<string> GetClients(string user)
        {
            var tbl = dby.CAR002.Where(m => m.CAR002_OzelKodu == user).Select(m => new { m.CAR002_HesapKodu, m.CAR002_Unvan1, CariTipi = m.CAR002_Kod2 }).ToList();
            return new List<string>();
        }
    }
}