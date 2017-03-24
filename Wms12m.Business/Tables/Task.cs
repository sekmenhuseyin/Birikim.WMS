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
            try
            {
                if (tbl.ID == 0)
                {
                    tbl.OlusturanID = Users.AppIdentity.User.Id;
                    tbl.OlusturmaTarihi = DateTime.Today.ToOADateInt();
                    tbl.OlusturmaSaati = DateTime.Now.SaatiAl();
                    tbl.DurumID = ComboItems.Açık.ToInt32();
                    db.Gorevs.Add(tbl);
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
                Gorev gorev = new Gorev();
                gorev.DepoID = tbl.DepoID;
                gorev.GorevNo = gorevno;
                gorev.GorevTipiID = ComboItems.MalKabul.ToInt32();
                gorev.DurumID = ComboItems.Açık.ToInt32();
                gorev.OlusturanID = Users.AppIdentity.User.Id;
                gorev.OlusturmaTarihi = DateTime.Today.ToOADateInt();
                gorev.OlusturmaSaati = DateTime.Now.SaatiAl();
                gorev.IrsaliyeID = tbl.IrsaliyeID;
                gorev.Bilgi = "Irs: " + evrakno;
                db.Gorevs.Add(gorev);
                db.SaveChanges();
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
                _Result.Id = gorev.ID;
            }
            catch (Exception ex)
            {
                _Result.Message = ex.Message + ": " + ex.InnerException.Message;
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
                        tmp.BitisSaati = DateTime.Now.SaatiAl();
                    }
                    db.SaveChanges();
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Status = true;
                    _Result.Id = tmp.ID;
                }
                catch (Exception ex)
                {
                    _Result.Message = ex.Message + ": " + ex.InnerException.Message;
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
                    tmp.GorevliID = tbl.GorevliID;
                    tmp.AtayanID = Users.AppIdentity.User.Id;
                    tmp.AtamaTarihi = DateTime.Today.ToOADateInt();
                    db.SaveChanges();
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Status = true;
                    _Result.Id = tmp.ID;
                }
                catch (Exception ex)
                {
                    _Result.Message = ex.Message + ": " + ex.InnerException.Message;
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
                _Result.Message = ex.Message + ": " + ex.InnerException.Message;
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
            catch (Exception)
            {
                return new Gorev();
            }
        }
        public frmGorevJson DetailJson(int Id)
        {
            return db.Gorevs
                .Where(m => m.ID == Id)
                .Select(m => new frmGorevJson { Olusturan = m.User2.AdSoyad, OlusturmaTarihi = m.OlusturmaTarihi, OlusturmaSaati = m.OlusturmaSaati, Bilgi = m.Bilgi, Aciklama = m.Aciklama, AtamaTarihi = m.AtamaTarihi, BitisTarihi = m.BitisTarihi, BitisSaati = m.BitisSaati, Atayan = m.User.AdSoyad, Gorevli = m.User1.AdSoyad })
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
            return db.Gorevs.Where(m=>m.DurumID==ParentId).OrderBy(m => m.ID).ToList();
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
