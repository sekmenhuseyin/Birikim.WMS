using System;
using System.Web;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Security;
using Wms12m.Entity.Models;
using System.Collections.Generic;

namespace Wms12m.Business
{
    public class Irsaliye : abstractTables<WMS_IRS>
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// ekle/güncelle
        /// </summary>
        public override Result Operation(WMS_IRS tbl)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// yeni ekle
        /// </summary>
        public Result Insert(frmIrsaliye tbl)
        {
            _Result = new Result();
            DateTime dateValue = DateTime.Now;
            if (DateTime.TryParse(tbl.Tarih, out dateValue) == true)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //add irsaliye table
                        WMS_IRS tablo = new WMS_IRS();
                        tablo.SirketKod = tbl.SirketID;
                        tablo.DepoID = tbl.DepoID;
                        tablo.EvrakNo = tbl.EvrakNo;
                        tablo.HesapKodu = tbl.HesapKodu;
                        tablo.Tarih = Convert.ToInt32(dateValue.ToOADate());
                        tablo.Kaydeden = Users.AppIdentity.User.LogonUserName;
                        tablo.KayitTarih = DateTime.Today.ToOADate().ToInt32();
                        db.WMS_IRS.Add(tablo);
                        db.SaveChanges();
                        //add görevlist table
                        int gorevno = db.GetGorevNo(DateTime.Today.ToOADateInt()).FirstOrDefault().Value;
                        GorevListesi gorev = new GorevListesi();
                        gorev.DepoID = tbl.DepoID;
                        gorev.GorevNo = DateTime.Today.ToString("ddMMyy") + "-" + gorevno;
                        gorev.GorevTipiID = ComboNames.MalKabul.ToInt32();
                        gorev.DurumID = ComboNames.Açık.ToInt32();
                        gorev.OlusturanID = Users.AppIdentity.User.Id;
                        gorev.OlusturmaTarihi = DateTime.Today.ToOADate().ToInt32();
                        gorev.IrsaliyeID = tablo.ID;
                        gorev.Bilgi = "Irs: " + tablo.EvrakNo + ", Tedarikçi: " + tbl.Unvan;
                        db.GorevListesis.Add(gorev);
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                        //result
                        _Result.Message = "İşlem Başarılı !!!";
                        _Result.Status = true;
                        _Result.Id = tablo.ID;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        _Result.Message = ex.Message + ": " + ex.InnerException.Message;
                        _Result.Status = false;
                        _Result.Id = 0;
                    }
                }
            }
            else
            {
                _Result.Message = "İşlem Hatalı !!!";
                _Result.Status = false;
                _Result.Id = 0;
            }
            return _Result;
        }
        /// <summary>
        /// ayrıntılar
        /// </summary>
        public override WMS_IRS Detail(int Id)
        {
            try
            {
                return db.WMS_IRS.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception)
            {
                return new WMS_IRS();
            }
        }
        /// <summary>
        /// liste
        /// </summary>
        public override List<WMS_IRS> GetList()
        {
            return db.WMS_IRS.OrderBy(m => m.EvrakNo).ToList();
        }
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<WMS_IRS> GetList(int ParentId)
        {
            return GetList();
        }
        /// <summary>
        /// silme
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            try
            {
                WMS_IRS tbl = db.WMS_IRS.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.GorevListesis.RemoveRange(db.GorevListesis.Where(m => m.IrsaliyeID == tbl.ID));
                    db.WMS_STI.RemoveRange(db.WMS_STI.Where(m => m.IrsaliyeID == tbl.ID));
                    db.WMS_IRS.Remove(tbl);
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
    }
}
