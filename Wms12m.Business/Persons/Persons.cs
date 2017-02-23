using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wms12m.Entity;
using Wms12m.Repository;
using Wms12m.Security;

namespace Wms12m.Business
{
    public class Persons : AbstractPersons
    {
        CacheControl<USER01> Cache;
        Result _Result;
        LogCs _Log;
        Logger _Logger;
        USER01 P;
        List<USER01> PList;
        private IUnitOfWork _unitOfWork;
        public override Result Login(USER01 P)
        {
            P.Password = CryptographyExtension.Encrypt(P.Password);
            _Result = new Result();
            try
            {
               
                var tbl = GetList((int)GetListStatus.Close).Where(a=>a.UserName==P.UserName && a.Password==P.Password && a.Aktif==false).SingleOrDefault();
                if(tbl!=null)
                {
                    _Result.Status = true;
                    _Result.Id = tbl.Id;
                    _Result.Message = "İşlem Başarılı";
                    _Result.Data = tbl;
                    SiteSessions.LoggedUserNo = tbl.Id;
                    SiteSessions.LoggedUserName = tbl.UserName;
                    SiteSessions.LoggedRealName = tbl.Name;

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
        public override USER01 Detail(int Id)
        {            
            USER01 _Person = new USER01();
            P = new USER01();
            try
            {               
                _Person = GetList((int)GetListStatus.Close).Where(a=>a.Id==Id).SingleOrDefault();
                return P;
            }
            catch (Exception)
            {
                return new USER01();
            }
        }
        public override List<USER01> PersonList()
        {           
            PList = new List<USER01>();
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
        private List<USER01> GetList(int Status)
        {
            _unitOfWork = new UnitOfWork();
            PList = new List<USER01>();
            Cache = new CacheControl<USER01>();
            try
            {
                if (Status == (int)GetListStatus.Refresh)
                {
                    PList = ((Task.Run(() => _unitOfWork.Repository<USER01>().All("USR01")).Result)).ToList();
                }
                else if (Status == (int)GetListStatus.Close)
                {
                    if (Cache.CacheGet(CacheKeys.PersonsCacheKeys.DataListOfUSER01) ==null)
                    {
                        PList = ((Task.Run(() => _unitOfWork.Repository<USER01>().All("USR01")).Result)).ToList();
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
                return new List<USER01>();
            }
        }
    }
}
