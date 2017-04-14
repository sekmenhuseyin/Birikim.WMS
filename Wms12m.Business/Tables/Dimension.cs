using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Business
{
    public class Dimension : abstractTables<Olcu>, IDisposable
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// ekle-kaydet
        /// </summary>
        public override Result Operation(Olcu tbl)
        {
            _Result = new Result();
            if (tbl.MalKodu == "" || tbl.Birim == "" || (tbl.Boy == 0 && tbl.En == 0 && tbl.Derinlik == 0 && tbl.Agirlik == 0))
            {
                _Result.Id = 0;
                _Result.Message = "Eksik Bilgi Girdiniz";
                _Result.Status = false;
                return _Result;
            }
            var tmp2 = db.Olcus.Where(m => m.MalKodu == tbl.MalKodu && m.Birim == tbl.Birim && m.ID != tbl.ID).FirstOrDefault();
            if (tmp2 != null)
            {
                _Result.Id = 0;
                _Result.Message = "Bu ölçü daha önce eklendi";
                _Result.Status = false;
                return _Result;
            }
            try
            {
                if (tbl.ID == 0)
                {
                    db.Olcus.Add(tbl);
                }
                else
                {
                    var tmp3 = Detail(tbl.ID);
                    tmp3.MalKodu = tbl.MalKodu;
                    tmp3.Birim = tbl.Birim;
                    tmp3.En = tbl.En;
                    tmp3.Boy = tbl.Boy;
                    tmp3.Derinlik = tbl.Derinlik;
                    tmp3.Agirlik = tbl.Agirlik;
                }
                db.SaveChanges();
                //result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message, ex.InnerException != null ? ex.InnerException.Message : "", "Dimension/Operation");
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
            try
            {
                Olcu tbl = db.Olcus.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.Olcus.Remove(tbl);
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
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message, ex.InnerException != null ? ex.InnerException.Message : "", "Dimension/Delete");
                _Result.Message = ex.Message;
                _Result.Status = false;
            }
            return _Result;
        }
        /// <summary>
        /// ayrıntılar
        /// </summary>
        public override Olcu Detail(int Id)
        {
            try
            {
                return db.Olcus.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message, ex.InnerException != null ? ex.InnerException.Message : "", "Dimension/Detail");
                return new Olcu();
            }
        }
        /// <summary>
        /// tüm liste
        /// </summary>
        public override List<Olcu> GetList()
        {
            return db.Olcus.OrderBy(m => m.MalKodu).ToList();
        }
        public override List<Olcu> GetList(int ParentId)
        {
            return GetList();
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
