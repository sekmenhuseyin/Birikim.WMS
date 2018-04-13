using Birikim.Models;
using Birikim.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class Persons : abstractTables<User>
    {
        /// <summary>
        /// şifre değiştirme işlemleri
        /// </summary>
        public Result ChangePass(User p)
        {
            if (p.Sifre.ToString2() == "")
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
            p.Sifre = CryptographyExtension.Sifrele(p.Sifre);
            try
            {
                var tmp = Detail(p.ID);
                tmp.Sifre = p.Sifre ?? "";
                tmp.Degistiren = vUser.UserName;
                tmp.DegisTarih = DateTime.Today.ToOADateInt();
                tmp.DegisSaat = DateTime.Now.ToOaTime();
                db.SaveChanges();
                LogActions("Business", "Persons", "ChangePass", ComboItems.alDüzenle, p.ID);
                // result
                _Result.Id = p.ID;
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
        /// sil
        /// </summary>
        public override Result Delete(int id)
        {
            _Result = new Result();
            var tbl = db.Users.FirstOrDefault(m => m.ID == id);
            if (tbl == null)
            {
                _Result.Message = "Kayıt Yok";
                return _Result;
            }

            var det = db.UserDetails.FirstOrDefault(m => m.UserID == tbl.ID);
            if (det != null) db.UserDetails.Remove(det);
            var dev = db.UserDevices.Where(m => m.UserID == tbl.ID).ToList();
            db.UserDevices.RemoveRange(dev);
            db.Users.Remove(tbl);
            try
            {
                db.SaveChanges();
                LogActions("Business", "Persons", "Delete", ComboItems.alSil, tbl.ID);
                _Result.Id = id;
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
        /// bir kişinin ayrıntıları
        /// </summary>
        public override User Detail(int id)
        {
            try
            {
                return db.Users.FirstOrDefault(m => m.ID == id);
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
            return db.Users.Where(m => m.ID > 1 & m.Aktif).OrderBy(m => m.AdSoyad).ToList();
        }

        /// <summary>
        /// yetkiye sahip kişiler
        /// </summary>
        public override List<User> GetList(int userId) => db.Users.Where(m => m.ID == userId).ToList();

        public List<User> GetList(string roleName)
        {
            return db.Users.Where(m => m.Sirket == "" && m.RoleName == roleName & m.Aktif).OrderByDescending(m => m.Aktif).ThenBy(m => m.AdSoyad).ToList();
        }

        public List<User> GetList(string[] roleName)
        {
            return db.Users.Where(m => m.Sirket == "" && roleName.Contains(m.RoleName) & m.Aktif).OrderByDescending(m => m.Aktif).ThenBy(m => m.AdSoyad).ToList();
        }

        public List<User> GetListAll()
        {
            return db.Users.Where(m => m.ID > 1).OrderBy(m => m.AdSoyad).ToList();
        }

        public IQueryable<frmChatUser> GetListAll2()
        {
            return db.Users.Where(m => m.ID > 1).Select(m => new frmChatUser { ID = m.ID, Guid = m.Guid.ToString(), AdSoyad = m.AdSoyad, Email = m.Email, Kod = m.Kod, RoleName = m.RoleName, Aktif = m.Aktif }).OrderByDescending(m => m.Aktif).ThenBy(m => m.AdSoyad);
        }

        public List<User> GetListWithoutTerminal()
        {
            return db.Users.Where(m => m.Sirket == "" && m.UserDetail == null && m.ID > 1 & m.Aktif).OrderByDescending(m => m.Aktif).ThenBy(m => m.AdSoyad).ToList();
        }

        /// <summary>
        /// şifre göster
        /// </summary>
        public string GetPass(int id)
        {
            return CryptographyExtension.Cozumle(Detail(id).Sifre);
        }

        /// <summary>
        /// giriş işlemleri
        /// </summary>
        public Result Login(User p, string device)
        {
            _Result = new Result()
            {
                Status = false,
                Message = "Hatalı kombinasyon",
                Id = 0
            };
            try
            {
                p.Kod = p.Kod.Left(5);
                var tbl = db.Users.FirstOrDefault(a => a.Kod.Equals(p.Kod) && a.Sirket == "" && a.Tip == 0 && a.Aktif);
                if (tbl != null)//if user exists
                {
                    var pass = CryptographyExtension.Cozumle(tbl.Sifre);
                    if (p.Sifre == pass)//if password matches
                    {
                        // update db
                        db.LogLogins(p.Kod, device, true, "");
                        db.UpdateUserDevice(tbl.ID, device);
                        // return result
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
        /// ekle, güncelle
        /// </summary>
        public override Result Operation(User tbl)
        {
            _Result = new Result(); var eklemi = false;
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

            var kontrol = db.Users.FirstOrDefault(m => m.Kod == tbl.Kod && m.ID != tbl.ID);
            if (kontrol != null)
            {
                _Result.Id = 0;
                _Result.Message = "Bu isim kullanılıyor";
                _Result.Status = false;
                return _Result;
            }

            if (tbl.Sifre.ToString2() != "") tbl.Sifre = CryptographyExtension.Sifrele(tbl.Sifre);
            tbl.Kod = tbl.Kod.Left(5);
            // set details
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
                LogActions("Business", "Persons", "Operation", eklemi ? ComboItems.alEkle : ComboItems.alDüzenle, tbl.ID, tbl.AdSoyad + ", " + tbl.Email + ", " + tbl.RoleName + ", " + tbl.Kod);
                // result
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
    }
}