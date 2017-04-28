using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Business
{
    public class Task : abstractTables<Gorev>, IDisposable
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// ekle/güncelle
        /// </summary>
        public override Result Operation(Gorev tbl)
        {
            _Result = new Result();
            if (tbl.ID == 0)
            {
                tbl.Olusturan = Users.AppIdentity.User.UserName;
                tbl.OlusturmaTarihi = DateTime.Today.ToOADateInt();
                tbl.OlusturmaSaati = DateTime.Now.ToOaTime();
                tbl.DurumID = ComboItems.Açık.ToInt32();
                db.Gorevs.Add(tbl);
            }
            try
            {
                db.SaveChanges();
                //result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Task/Operation");
                _Result.Id = 0;
                _Result.Message = "İşlem Hatalı: " + ex.Message;
                _Result.Status = false;
            }
            return _Result;
        }
        /// <summary>
        /// yeni ekle
        /// </summary>
        public Result Insert(frmGorev tbl)
        {
            _Result = new Result();
            //add görevlist table
            try
            {
                string evrakno = db.IRS.Where(m => m.ID == tbl.IrsaliyeID).Select(m => m.EvrakNo).FirstOrDefault();
                string gorevno = db.SettingsGorevNo(DateTime.Today.ToOADateInt()).FirstOrDefault();
                Gorev gorev = new Gorev()
                {
                    DepoID = tbl.DepoID,
                    GorevNo = gorevno,
                    GorevTipiID = ComboItems.MalKabul.ToInt32(),
                    DurumID = ComboItems.Açık.ToInt32(),
                    Olusturan = Users.AppIdentity.User.UserName,
                    OlusturmaTarihi = DateTime.Today.ToOADateInt(),
                    OlusturmaSaati = DateTime.Now.ToOaTime(),
                    IrsaliyeID = tbl.IrsaliyeID,
                    Bilgi = "Irs: " + evrakno
                };
                db.Gorevs.Add(gorev);
                db.SaveChanges();
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
                _Result.Id = gorev.ID;
            }
            catch (Exception ex)
            {
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Task/Insert");
                _Result.Message = ex.Message;
                _Result.Status = false;
                _Result.Id = 0;
            }
            return _Result;
        }
        /// <summary>
        /// Güncelle
        /// </summary>
        public Result Update(frmGorev tbl)
        {
            _Result = new Result();
            var tmp = db.Gorevs.Where(m => m.ID == tbl.ID).FirstOrDefault();
            if (tmp != null)
            {
                try
                {
                    tmp.Aciklama = tbl.Aciklama;
                    tmp.Bilgi = tbl.Bilgi;
                    tmp.DurumID = tbl.DurumID;
                    if (tbl.DurumID == ComboItems.Tamamlanan.ToInt32())
                    {
                        tmp.BitisTarihi = DateTime.Today.ToOADateInt();
                        tmp.BitisSaati = DateTime.Now.ToOaTime();
                    }
                    db.SaveChanges();
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Status = true;
                    _Result.Id = tmp.ID;
                }
                catch (Exception ex)
                {
                    db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Task/Update");
                    _Result.Message = ex.Message;
                    _Result.Status = false;
                    _Result.Id = 0;
                }
            }
            return _Result;
        }
        /// <summary>
        /// Güncelle
        /// </summary>
        public Result UpdateGorevli(frmGorevli tbl)
        {
            _Result = new Result();
            var tmp = db.Gorevs.Where(m => m.ID == tbl.ID).FirstOrDefault();
            if (tmp != null)
            {
                try
                {
                    tmp.Gorevli = tbl.Gorevli;
                    tmp.Atayan = Users.AppIdentity.User.UserName;
                    tmp.AtamaTarihi = DateTime.Today.ToOADateInt();
                    db.SaveChanges();
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Status = true;
                    _Result.Id = tmp.ID;
                }
                catch (Exception ex)
                {
                    db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Task/UpdateGorevli");
                    _Result.Message = ex.Message;
                    _Result.Status = false;
                    _Result.Id = 0;
                }
            }
            return _Result;
        }
        /// <summary>
        /// silme işlemi
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            try
            {
                Gorev tbl = db.Gorevs.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.Gorevs.Remove(tbl);
                    db.SaveChanges();
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Id = Id;
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
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Task/Delete");
                _Result.Message = ex.Message;
                _Result.Status = false;
            }
            return _Result;
        }
        public Result DeleteSome()
        {
            _Result = new Result();
            try
            {
                int Id = ComboItems.Başlamamış.ToInt32();
                var tbl = db.Gorevs.Where(m => m.DurumID == Id).ToList();
                if (tbl == null)
                {
                    _Result.Message = "Kayıt Yok";
                    _Result.Status = false;
                }
                foreach (var item in tbl)
                {
                    db.DeleteFromGorev(item.ID);
                }
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Task/DeleteSome");
                _Result.Message = ex.Message;
                _Result.Status = false;
            }
            return _Result;
        }
        /// <summary>
        /// ayrıntılar
        /// </summary>
        public override Gorev Detail(int Id)
        {
            try
            {
                return db.Gorevs.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Task/Detail");
                return new Gorev();
            }
        }
        public frmGorevJson DetailJson(int Id)
        {
            return db.Gorevs
                .Where(m => m.ID == Id)
                .Select(m => new frmGorevJson { Olusturan = m.Olusturan, OlusturmaTarihi = m.OlusturmaTarihi, OlusturmaSaati = m.OlusturmaSaati, Bilgi = m.Bilgi, Aciklama = m.Aciklama, AtamaTarihi = m.AtamaTarihi, BitisTarihi = m.BitisTarihi, BitisSaati = m.BitisSaati, Atayan = m.Atayan, Gorevli = m.Gorevli })
                .FirstOrDefault();
        }
        /// <summary>
        /// liste
        /// </summary>
        public override List<Gorev> GetList()
        {
            return db.Gorevs.OrderBy(m => m.DurumID).ThenByDescending(m => m.ID).ToList();
        }
        public int Count()
        {
            return db.Gorevs.Count();
        }
        /// <summary>
        /// duruma göre olanları getirir
        /// </summary>
        public override List<Gorev> GetList(int ParentId)
        {
            return db.Gorevs.Where(m => m.DurumID == ParentId).OrderByDescending(m => m.ID).ToList();
        }
        public List<Gorev> GetListbyType(int ParentId)
        {
            return db.Gorevs.Where(m => m.GorevTipiID == ParentId).OrderByDescending(m => m.ID).ToList();
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
