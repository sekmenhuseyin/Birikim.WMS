using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class Task : abstractTables<Gorev>
    {
        /// <summary>
        /// ekle/güncelle
        /// </summary>
        public override Result Operation(Gorev tbl)
        {
            _Result = new Result(); bool eklemi = false;
            if (tbl.ID == 0)
            {
                tbl.Olusturan = vUser.UserName;
                tbl.OlusturmaTarihi = DateTime.Today.ToOADateInt();
                tbl.OlusturmaSaati = DateTime.Now.ToOaTime();
                tbl.DurumID = ComboItems.Açık.ToInt32();
                db.Gorevs.Add(tbl);
                eklemi = true;
            }
            try
            {
                db.SaveChanges();
                LogActions("Business", "Task", "Insert", eklemi == true ? ComboItems.alEkle : ComboItems.alDüzenle, tbl.ID, "DepoID: " + tbl.DepoID + ", GorevNo: " + tbl.GorevNo + ", GorevTipiID: " + tbl.GorevTipiID + ", Bilgi: " + tbl.Bilgi);
                //result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Task/Operation");
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
                var irsaliye = db.IRS.Where(m => m.ID == tbl.IrsaliyeID).FirstOrDefault();
                string gorevno = db.SettingsGorevNo(DateTime.Today.ToOADateInt(), irsaliye.DepoID).FirstOrDefault();
                Gorev gorev = new Gorev()
                {
                    DepoID = tbl.DepoID,
                    GorevNo = gorevno,
                    GorevTipiID = ComboItems.MalKabul.ToInt32(),
                    DurumID = ComboItems.Açık.ToInt32(),
                    Olusturan = vUser.UserName,
                    OlusturmaTarihi = DateTime.Today.ToOADateInt(),
                    OlusturmaSaati = DateTime.Now.ToOaTime(),
                    IrsaliyeID = tbl.IrsaliyeID,
                    Bilgi = "Irs: " + irsaliye.EvrakNo
                };
                db.Gorevs.Add(gorev);
                db.SaveChanges();
                LogActions("Business", "Task", "Insert", ComboItems.alEkle, tbl.ID, "DepoID: " + tbl.DepoID + ", GorevNo: " + tbl.GorevNo + ", GorevTipiID: " + tbl.GorevTipiID + ", Bilgi: " + tbl.Bilgi);
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
                _Result.Id = gorev.ID;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Task/Insert");
                _Result.Message = ex.Message;
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
                    LogActions("Business", "Task", "Update", ComboItems.alDüzenle, tbl.ID, "DurumID: " + tbl.DurumID + ", Aciklama: " + tbl.Aciklama + ", Bilgi: " + tbl.Bilgi);
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Status = true;
                    _Result.Id = tmp.ID;
                }
                catch (Exception ex)
                {
                    Logger(ex, "Business/Task/Update");
                    _Result.Message = ex.Message;
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
                    tmp.Atayan = vUser.UserName;
                    tmp.AtamaTarihi = DateTime.Today.ToOADateInt();
                    db.SaveChanges();
                    LogActions("Business", "Task", "UpdateGorevli", ComboItems.alDüzenle, tbl.ID, "Görevli: " + tbl.Gorevli);
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Status = true;
                    _Result.Id = tmp.ID;
                }
                catch (Exception ex)
                {
                    Logger(ex, "Business/Task/UpdateGorevli");
                    _Result.Message = ex.Message;
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
                    LogActions("Business", "Task", "Delete", ComboItems.alSil, tbl.ID);
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Id = Id;
                    _Result.Status = true;
                }
                else
                {
                    _Result.Message = "Kayıt Yok";
                    return _Result;
                }
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Task/Delete");
                _Result.Message = ex.Message;
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
                Logger(ex, "Business/Task/Detail");
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
        public override List<Gorev> GetList(int DurumID)
        {
            return db.Gorevs.Where(m => m.DurumID == DurumID).OrderByDescending(m => m.ID).ToList();
        }
        public List<Gorev> GetList(int DurumID, int? DepoID)
        {
            if (DepoID != null)
                return db.Gorevs.Where(m => m.DurumID == DurumID && (m.DepoID == DepoID || m.GorevTipiID == (int)ComboItems.Satıştanİade)).OrderByDescending(m => m.ID).ToList();
            else
                return db.Gorevs.Where(m => m.DurumID == DurumID).OrderByDescending(m => m.ID).ToList();
        }
        /// <summary>
        /// görev tipi ve duruma göre listeler
        /// </summary>
        public List<Gorev> GetList(int TipID, int DurumID)
        {
            return db.Gorevs.Where(m => m.DurumID == DurumID && m.GorevTipiID == TipID).OrderByDescending(m => m.ID).ToList();
        }
    }
}
