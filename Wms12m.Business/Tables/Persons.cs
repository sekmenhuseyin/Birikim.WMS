using System;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Security;
using Wms12m.Repository;
using System.Threading.Tasks;
using System.Collections.Generic;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class Persons : abstractTables<USR01>
    {
        CacheControl<USR01> Cache;
        Result _Result;
        LogCs _Log;
        Logger _Logger;
        USR01 P;
        List<USR01> PList;
        private IUnitOfWork _unitOfWork;
        public Result Login(USR01 P)
        {
            P.Sifre = CryptographyExtension.Sifrele(P.Sifre);
            _Result = new Result();
            try
            {
               
                var tbl = GetList((int)GetListStatus.Close).Where(a=>a.Kod==P.Kod && a.Sifre == P.Sifre && a.Aktif==false).SingleOrDefault();
                if(tbl!=null)
                {
                    _Result.Status = true;
                    _Result.Id = tbl.ID;
                    _Result.Message = "İşlem Başarılı";
                    _Result.Data = tbl;
                    SiteSessions.LoggedUserNo = tbl.ID;
                    SiteSessions.LoggedUserName = tbl.Kod;
                    SiteSessions.LoggedRealName = tbl.AdSoyad;

                }
                else
                {
                    _Result.Status = false;
                    _Result.Message = "İşlem Hata !!!";
                    _Result.Id = 0;
                }
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Login işlemi !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                _Result.Status = false;
                _Result.Message = "İşlem Hata !!!"+ex.Message;
                _Result.Id = 0;
            }
            return _Result;
        }
        public override USR01 Detail(int Id)
        {            
            USR01 _Person = new USR01();
            P = new USR01();
            try
            {               
                _Person = GetList((int)GetListStatus.Close).Where(a=>a.ID==Id).SingleOrDefault();
                return P;
            }
            catch (Exception)
            {
                return new USR01();
            }
        }
        public override List<USR01> GetList()
        {           
            PList = new List<USR01>();
            try
            {
                return PList = GetList((int)GetListStatus.Close);
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Personel Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return PList;
            }
        }
        public override List<USR01> GetList(int ParentId)
        {
            _unitOfWork = new UnitOfWork();
            PList = new List<USR01>();
            Cache = new CacheControl<USR01>();
            try
            {
                if (ParentId == (int)GetListStatus.Refresh)
                {
                    PList = ((Task.Run(() => _unitOfWork.Repository<USR01>().All("USR01")).Result)).ToList();
                }
                else if (ParentId == (int)GetListStatus.Close)
                {
                    if (Cache.CacheGet(CacheKeys.PersonsCacheKeys.DataListOfUSER01) ==null)
                    {
                        PList = ((Task.Run(() => _unitOfWork.Repository<USR01>().All("USR01")).Result)).ToList();
                        Cache.CacheUpdate(PList,CacheKeys.PersonsCacheKeys.DataListOfUSER01);
                    }
                    else
                    {
                        PList = Cache.CacheGet(CacheKeys.PersonsCacheKeys.DataListOfUSER01);
                    }
                }
                return PList;
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Personel genel Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return new List<USR01>();
            }
        }
        public override Result Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public override Result Operation(USR01 tbl)
        {
            throw new NotImplementedException();
        }
    }
}
