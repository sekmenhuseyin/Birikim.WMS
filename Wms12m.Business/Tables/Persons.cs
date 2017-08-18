using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Business
{
    public class Persons : abstractTables<User>
    {
        /// <summary>
        /// ekle, güncelle
        /// </summary>
        public override Result Operation(User tbl)
        {
            _Result = new Result(); bool eklemi = false;
            if (tbl.AdSoyad == "" || tbl.Kod == "" || (tbl.ID == 0 && tbl.Sifre.ToString2() == ""))
            {
                _Result.Id = 0;
                _Result.Message = "Eksik Bilgi Girdiniz";
                _Result.Status = false;
                return _Result;
            }
            if (fn.isEmail(tbl.Email) == false)
            {
                _Result.Id = 0;
                _Result.Message = "Geçersiz bir email adresi yazdınız";
                _Result.Status = false;
                return _Result;
            }
            var kontrol = db.Users.Where(m => m.Kod == tbl.Kod && m.ID != tbl.ID).FirstOrDefault();
            if (kontrol != null)
            {
                _Result.Id = 0;
                _Result.Message = "Bu isim kullanılıyor";
                _Result.Status = false;
                return _Result;
            }
            if (tbl.Sifre.ToString2() != "") tbl.Sifre = CryptographyExtension.Sifrele(tbl.Sifre);
            tbl.Kod = tbl.Kod.Left(5);
            //set details
            tbl.Degistiren = vUser.UserName;
            tbl.DegisTarih = DateTime.Today.ToOADateInt();
            tbl.DegisSaat = DateTime.Now.ToOaTime();
            tbl.DegisKaynak = 0;
            tbl.DegisSurum = "1.0.0";
            if (tbl.ID == 0)
            {
                tbl.Guid = Guid.NewGuid();
                tbl.Sirket = "";
                tbl.Email = tbl.Email.ToString2();
                tbl.Tema = tbl.Tema.ToString2();
                tbl.Kaydeden = vUser.UserName;
                tbl.KayitTarih = DateTime.Today.ToOADateInt();
                tbl.KayitSaat = DateTime.Now.ToOaTime();
                tbl.KayitKaynak = 0;
                tbl.KayitSurum = "1.0.0";
                db.Users.Add(tbl);
                eklemi = true;
            }
            else
            {
                var tmp = Detail(tbl.ID);
                tmp.Sirket = "";
                tmp.Tip = 0;
                tmp.Kod = tbl.Kod;
                tmp.AdSoyad = tbl.AdSoyad;
                tmp.Email = tbl.Email.ToString2();
                tmp.Tema = tbl.Tema.ToString2();
                tmp.RoleName = tbl.RoleName;
                tmp.Admin = tbl.Admin;
                tmp.Aktif = tbl.Aktif;
                tmp.Degistiren = tbl.Degistiren;
                tmp.DegisTarih = tbl.DegisTarih;
                tmp.DegisSaat = tbl.DegisSaat;
                tmp.DegisKaynak = tbl.DegisKaynak;
                tmp.DegisSurum = tbl.DegisSurum;
            }
            try
            {
                db.SaveChanges();
                LogActions("Business", "Persons", "Operation", eklemi == true ? ComboItems.alEkle : ComboItems.alDüzenle, tbl.ID, tbl.AdSoyad+", "+tbl.Email + ", " + tbl.RoleName + ", " + tbl.Kod);
                //result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Persons/Operation");
                _Result.Id = 0;
                _Result.Message = "İşlem Hatalı: " + ex.Message;
                _Result.Status = false;
            }
            return _Result;
        }
        /// <summary>
        /// sil
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            User tbl = db.Users.Where(m => m.ID == Id).FirstOrDefault();
            if (tbl != null)
                db.Users.Remove(tbl);
            else
            {
                _Result.Message = "Kayıt Yok";
                return _Result;
            }
            try
            {
                db.SaveChanges();
                LogActions("Business", "Persons", "Delete", ComboItems.alSil, tbl.ID);
                _Result.Id = Id;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Persons/Delete");
                _Result.Message = ex.Message;
            }
            return _Result;
        }
        /// <summary>
        /// giriş işlemleri
        /// </summary>
        public Result Login(User P, string device)
        {
            _Result = new Result()
            {
                Status = false,
                Message = "Hatalı kombinasyon",
                Id = 0
            };
            try
            {
                P.Kod = P.Kod.Left(5).ToLower();
                var tbl = db.Users.Where(a => a.Kod.ToLower() == P.Kod && a.Sirket == "" && a.Tip == 0 && a.Aktif == true).FirstOrDefault();
                if (tbl != null)//if user exists
                {
                    string pass = CryptographyExtension.Cozumle(tbl.Sifre);
                    if (P.Sifre == pass)//if password matches
                    {
                        //update db
                        db.LogLogins(P.Kod, device, true, "");
                        db.UpdateUserDevice(tbl.ID, device);
                        //resturn result
                        _Result.Status = true;
                        _Result.Id = tbl.ID;
                        _Result.Message = "İşlem Başarılı";
                        _Result.Data = tbl;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Persons/Login");
                _Result.Message = "İşlem Hata !!!" + ex.Message;
            }
            return _Result;
        }
        /// <summary>
        /// şifre değiştirme işlemleri
        /// </summary>
        public Result ChangePass(User P)
        {
            if (P.Sifre.ToString2() == "")
            {
                _Result.Id = 0;
                _Result.Message = "Eksik Bilgi Girdiniz";
                _Result.Status = false;
                return _Result;
            }
            _Result = new Result()
            {
                Status = false,
                Message = "İşlem Hata !!!",
                Id = 0
            };
            P.Sifre = CryptographyExtension.Sifrele(P.Sifre);
            try
            {
                var tmp = Detail(P.ID);
                tmp.Sifre = P.Sifre ?? "";
                tmp.Degistiren = vUser.UserName;
                tmp.DegisTarih = DateTime.Today.ToOADateInt();
                tmp.DegisSaat = DateTime.Now.ToOaTime();
                db.SaveChanges();
                LogActions("Business", "Persons", "ChangePass", ComboItems.alDüzenle, P.ID);
                //result
                _Result.Id = P.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Persons/ChangePass");
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
            catch (Exception ex)
            {
                Logger(ex, "Business/Persons/Detail");
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
        public List<User> GetListWithoutTerminal()
        {
            return db.Users.Where(m => m.UserDetail == null).OrderBy(m => m.AdSoyad).ToList();
        }
    }
}
