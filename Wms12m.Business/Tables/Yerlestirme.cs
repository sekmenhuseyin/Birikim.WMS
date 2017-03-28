using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Business
{
    public class Yerlestirme : abstractTables<Yer>, IDisposable
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// burada yok
        /// </summary>
        public override Result Operation(Yer tbl)
        {
            return new Result();
        }
        /// <summary>
        /// depo silme
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            try
            {
                Yer tbl = db.Yers.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.Yers.Remove(tbl);
                    Yer_Log logs = new Yer_Log();
                    logs.KatID = tbl.KatID;
                    logs.MalKodu = tbl.MalKodu;
                    logs.Birim = tbl.Birim;
                    logs.Miktar = tbl.Miktar;
                    logs.GC = true;
                    logs.Kaydeden = Users.AppIdentity.User.Id;
                    logs.KayitTarihi = DateTime.Today.ToOADateInt();
                    logs.KayitSaati = DateTime.Now.SaatiAl();
                    db.Yer_Log.Add(logs);
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
        /// depo bilgileri
        /// </summary>
        public override Yer Detail(int Id)
        {
            try
            {
                return db.Yers.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception)
            {
                return new Yer();
            }
        }
        /// <summary>
        /// depo listesi
        /// </summary>
        public override List<Yer> GetList()
        {
            return db.Yers.OrderBy(m=>m.MalKodu).ToList();
        }
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<Yer> GetList(int ParentId)
        {
            return db.Yers.Where(m => m.KatID == ParentId).OrderBy(m => m.MalKodu).ToList();
        }
        public List<Yer> GetListFromDepo(int ParentId)
        {
            return db.Yers.Where(m => m.Kat.Bolum.Raf.Koridor.DepoID == ParentId).OrderBy(m => m.MalKodu).ToList();
        }
        public List<Yer> GetListFromKoridor(int ParentId)
        {
            return db.Yers.Where(m => m.Kat.Bolum.Raf.KoridorID == ParentId).OrderBy(m => m.MalKodu).ToList();
        }
        public List<Yer> GetListFromRaf(int ParentId)
        {
            return db.Yers.Where(m => m.Kat.Bolum.RafID == ParentId).OrderBy(m => m.MalKodu).ToList();
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
