using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class Dimension : abstractTables<Olcu>
    {
        /// <summary>
        /// ekle-kaydet
        /// </summary>
        public override Result Operation(Olcu tbl)
        {
            _Result = new Result(false, 0);
            //kontrol
            if (tbl.MalKodu == "" || tbl.Birim == "" || (tbl.Boy == 0 && tbl.En == 0 && tbl.Derinlik == 0 && tbl.Agirlik == 0))
            {
                _Result.Message = "Eksik Bilgi Girdiniz";
                return _Result;
            }
            //var mı
            var tmp2 = db.Olcus.Where(m => m.MalKodu == tbl.MalKodu && m.Birim == tbl.Birim && m.ID != tbl.ID).FirstOrDefault();
            if (tmp2 != null)
            {
                _Result.Message = "Bu ölçü daha önce eklendi";
                return _Result;
            }
            //set details
            tbl.Degistiren = Users.AppIdentity.User.UserName;
            tbl.DegisTarih = DateTime.Today.ToOADateInt();
            if (tbl.ID == 0)
            {
                tbl.Kaydeden = Users.AppIdentity.User.UserName;
                tbl.KayitTarih = DateTime.Today.ToOADateInt();
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
            //kaydet
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
                helper.Logger(Users.AppIdentity.User.UserName, ex, "Business/Dimension/Operation");
                _Result.Message = "İşlem Hatalı: " + ex.Message;
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
                helper.Logger(Users.AppIdentity.User.UserName, ex, "Business/Dimension/Delete");
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
                helper.Logger(Users.AppIdentity.User.UserName, ex, "Business/Dimension/Detail");
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
    }
}
