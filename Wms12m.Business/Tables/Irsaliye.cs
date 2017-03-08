using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Business
{
    public class Irsaliye : abstractTables<IR>
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// ekle/güncelle
        /// </summary>
        public override Result Operation(IR tbl)
        {
            _Result = new Result();
            try
            {
                if (tbl.ID == 0)
                {
                    tbl.Kaydeden = Users.AppIdentity.User.LogonUserName;
                    tbl.KayitTarih = DateTime.Today.ToOADateInt();
                    db.IRS.Add(tbl);
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
                        IR tablo = new IR();
                        tablo.SirketKod = tbl.SirketID;
                        tablo.DepoID = tbl.DepoID;
                        tablo.EvrakNo = tbl.EvrakNo;
                        tablo.HesapKodu = tbl.HesapKodu;
                        tablo.Tarih = dateValue.ToOADateInt();
                        tablo.Kaydeden = Users.AppIdentity.User.LogonUserName;
                        tablo.KayitTarih = DateTime.Today.ToOADateInt();
                        db.IRS.Add(tablo);
                        db.SaveChanges();
                        //add görevlist table
                        string gorevno = db.SettingsGorevNo(DateTime.Today.ToOADateInt()).FirstOrDefault();
                        Gorev gorev = new Gorev();
                        gorev.DepoID = tbl.DepoID;
                        gorev.GorevNo = gorevno;
                        gorev.GorevTipiID = ComboItems.MalKabul.ToInt32();
                        gorev.DurumID = ComboItems.Açık.ToInt32();
                        gorev.OlusturanID = Users.AppIdentity.User.Id;
                        gorev.OlusturmaTarihi = DateTime.Today.ToOADateInt();
                        gorev.OlusturmaSaati = DateTime.Now.SaatiAl();
                        gorev.IrsaliyeID = tablo.ID;
                        gorev.Bilgi = "Irs: " + tablo.EvrakNo + ", Tedarikçi: " + tbl.Unvan;
                        db.Gorevs.Add(gorev);
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
        public override IR Detail(int Id)
        {
            try
            {
                return db.IRS.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception)
            {
                return new IR();
            }
        }
        /// <summary>
        /// liste
        /// </summary>
        public override List<IR> GetList()
        {
            return db.IRS.OrderBy(m => m.EvrakNo).ToList();
        }
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<IR> GetList(int ParentId)
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
                IR tbl = db.IRS.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.Gorevs.RemoveRange(db.Gorevs.Where(m => m.IrsaliyeID == tbl.ID));
                    db.IRS_Detay.RemoveRange(db.IRS_Detay.Where(m => m.IrsaliyeID == tbl.ID));
                    db.IRS.Remove(tbl);
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
