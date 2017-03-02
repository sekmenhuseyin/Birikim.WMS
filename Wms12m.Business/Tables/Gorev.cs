using System;
using System.Web;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Security;
using Wms12m.Entity.Models;
using System.Collections.Generic;

namespace Wms12m.Business
{
    public class Gorev : abstractTables<GorevListesi>
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// ekle/güncelle
        /// </summary>
        public override Result Operation(GorevListesi tbl)
        {
            throw new NotImplementedException();
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
                string evrakno = db.WMS_IRS.Where(m => m.ID == tbl.IrsaliyeID).Select(m => m.EvrakNo).FirstOrDefault();
                int gorevno = db.GetGorevNo(DateTime.Today.ToOADateInt()).FirstOrDefault().Value;
                GorevListesi gorev = new GorevListesi();
                gorev.DepoID = tbl.DepoID;
                gorev.GorevNo = DateTime.Today.ToString("ddMMyy") + "-" + gorevno;
                gorev.GorevTipiID = ComboNames.MalKabul.ToInt32();
                gorev.DurumID = ComboNames.Açık.ToInt32();
                gorev.OlusturanID = Users.AppIdentity.User.Id;
                gorev.OlusturmaTarihi = DateTime.Today.ToOADate().ToInt32();
                gorev.IrsaliyeID = tbl.IrsaliyeID;
                gorev.Bilgi = "Irs: " + evrakno;
                db.GorevListesis.Add(gorev);
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
            var tmp = db.GorevListesis.Where(m => m.ID == tbl.ID).FirstOrDefault();
            if (tmp != null)
            {
                try
                {
                    tmp.Aciklama = tbl.Aciklama;
                    tmp.Bilgi = tbl.Bilgi;
                    tmp.DurumID = tbl.DurumID;
                    if (tbl.DurumID == ComboNames.Tamamlanan.ToInt32()) tmp.BitisTarihi = DateTime.Today.ToOADateInt();
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
            var tmp = db.GorevListesis.Where(m => m.ID == tbl.ID).FirstOrDefault();
            if (tmp != null)
            {
                try
                {
                    tmp.GorevliID = tbl.GorevliID;
                    tmp.AtayanID = Users.AppIdentity.User.Id;
                    tmp.AtamaTarihi = DateTime.Today.ToOADate().ToInt32();
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
                GorevListesi tbl = db.GorevListesis.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.GorevListesis.Remove(tbl);
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
        public override GorevListesi Detail(int Id)
        {
            try
            {
                return db.GorevListesis.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception)
            {
                return new GorevListesi();
            }
        }
        /// <summary>
        /// liste
        /// </summary>
        public override List<GorevListesi> GetList()
        {
            return db.GorevListesis.OrderBy(m => m.OlusturmaTarihi).ToList();
        }
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<GorevListesi> GetList(int ParentId)
        {
            return GetList();
        }
    }
}
