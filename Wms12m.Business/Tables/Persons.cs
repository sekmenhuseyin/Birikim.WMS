﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Business
{
    public class Persons : abstractTables<User>, IDisposable
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// ekle, güncelle
        /// </summary>
        public override Result Operation(User tbl)
        {
            _Result = new Result();
            if (tbl.AdSoyad == "" || tbl.Sirket == "" || tbl.Tip == 0 || tbl.Kod == "")
            {
                _Result.Id = 0;
                _Result.Message = "Eksik Bilgi Girdiniz";
                _Result.Status = false;
                return _Result;
            }
            var kontrol = db.Users.Where(m => m.Sirket == tbl.Sirket && m.Tip == tbl.Tip && m.Kod == tbl.Kod && m.ID != tbl.ID).FirstOrDefault();
            if (kontrol != null)
            {
                _Result.Id = 0;
                _Result.Message = "Bu isim kullanılıyor";
                _Result.Status = false;
                return _Result;
            }
            try
            {
                tbl.Degistiren = Users.AppIdentity.User.LogonUserName;
                tbl.DegisTarih = DateTime.Today.ToOADateInt();
                tbl.DegisSaat = DateTime.Now.SaatiAl();
                tbl.DegisKaynak = 0;
                tbl.DegisSurum = "1.0.0";
                if (tbl.ID == 0)
                {
                    tbl.Kaydeden = Users.AppIdentity.User.LogonUserName;
                    tbl.KayitTarih = DateTime.Today.ToOADateInt();
                    tbl.KayitSaat = DateTime.Now.SaatiAl();
                    tbl.KayitKaynak = 0;
                    tbl.KayitSurum = "1.0.0";
                    db.Users.Add(tbl);
                }
                else
                {
                    var tmp = Detail(tbl.ID);
                    tmp.Sirket = tbl.Sirket;
                    tmp.Tip = tbl.Tip;
                    tmp.Kod = tbl.Kod;
                    tmp.AdSoyad = tbl.AdSoyad;
                    tmp.Email = tbl.Email;
                    tmp.RoleName = tbl.RoleName;
                    tmp.Admin = tbl.Admin;
                    tmp.Aktif = tbl.Aktif;
                    tmp.Degistiren = tbl.Degistiren;
                    tmp.DegisTarih = tbl.DegisTarih;
                    tmp.DegisSaat = tbl.DegisSaat;
                    tmp.DegisKaynak = tbl.DegisKaynak;
                    tmp.DegisSurum = tbl.DegisSurum;
                }
                db.SaveChanges();
                //result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                _Result.Id = 0;
                _Result.Message = "İşlem Hatalı: " + ex.Message;
                _Result.Status = false;
            }
            return _Result;
        }
        /// <summary>
        /// giriş işlemleri
        /// </summary>
        public Result Login(User P)
        {
            _Result = new Result();
            _Result.Status = false;
            _Result.Message = "İşlem Hata !!!";
            _Result.Id = 0;
            try
            {

                var tbl = db.Users.Where(a => a.Kod.ToLower() == P.Kod.ToLower() && a.Aktif == true).FirstOrDefault();
                if(tbl!=null)
                {
                    if (P.Sifre == CryptographyExtension.Cozumle(tbl.Sifre))
                    {
                        _Result.Status = true;
                        _Result.Id = tbl.ID;
                        _Result.Message = "İşlem Başarılı";
                        _Result.Data = tbl;
                    }
                }
            }
            catch (Exception ex)
            {
                _Result.Message = "İşlem Hata !!!" + ex.Message;
            }
            return _Result;
        }
        /// <summary>
        /// bir kişinin ayrıntıları
        /// </summary>
        public override User Detail(int Id)
        {
            try
            {
                return db.Users.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception)
            {
                return new User();
            }
        }
        /// <summary>
        /// liste
        /// </summary>
        public override List<User> GetList()
        {
            return db.Users.OrderBy(m => m.AdSoyad).ToList();
        }
        /// <summary>
        /// yetkiye sahip kişiler
        /// </summary>
        public override List<User> GetList(int ParentId)
        {
            return GetList();
        }
        public List<User> GetList(string RoleName)
        {
            return db.Users.Where(m => m.RoleName == RoleName).OrderBy(m => m.AdSoyad).ToList();
        }
        /// <summary>
        /// sil
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            try
            {
                User tbl = db.Users.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.Users.Remove(tbl);
                    db.SaveChanges();
                    _Result.Id = Id;
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Status = true;
                }
                else
                {
                    _Result.Message = "Kayıt Yok";
                    _Result.Status = false;
                }
            }
            catch (Exception ex)
            {
                _Result.Message = ex.Message + ": " + ex.InnerException.Message;
                _Result.Status = false;
            }
            return _Result;
        }
        /// <summary>
        /// dispose
        /// </summary>
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
