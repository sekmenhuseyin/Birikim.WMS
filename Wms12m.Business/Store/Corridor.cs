using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Wms12m.Cache;
using Wms12m.Entity;
using Wms12m.Log;
using Wms12m.Repository;
using static Wms12m.Configuration.Enm;

namespace Wms12m.Business
{
    public class Corridor : abstractStore<Store02>
    {
        CacheControl<Store02> Cache;
        Result _Result;
        LogCs _Log;
        Logger _Logger;
        List<Store02> PList;
        IUnitOfWork _unitOfWork;
        Wms12m.Security.CustomPrincipal Users = HttpContext.Current.User as Wms12m.Security.CustomPrincipal;
        public override Result Delete(int Id)
        {
            _unitOfWork = new UnitOfWork();
            PList = new List<Store02>();
            Cache = new CacheControl<Store02>();
            _Result = new Result();
            try
            {
                _Result.Id = _unitOfWork.Repository<Store02>().Delete("delete from WMS.TK_KOR where ID=" + Id);
                _unitOfWork.Commit();
                if (_Result.Id > 0)
                {
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Status = true;
                    SetList((int)GetListStatus.Refresh);
                }
                else
                {
                    _Result.Message = "İşlem Hatalı !!!";
                    _Result.Status = false;
                }
                return _Result;
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Koridor Silme Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return _Result;
            }
        }
        public override Store02 Detail(int Id)
        {
            try
            {
                return (GetList().Where(a => a.ID == Id).SingleOrDefault());
            }
            catch (Exception)
            {
                return new Store02();
            }
        }
        public override List<Store02> GetList()
        {
            PList = new List<Store02>();
            try
            {
                return PList = SetList((int)GetListStatus.Close);
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Koridor Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return PList;
            }
        }
        private List<Store02> SetList(int Status)
        {
            _unitOfWork = new UnitOfWork();
            PList = new List<Store02>();
            Cache = new CacheControl<Store02>();
            try
            {
                if (Status == (int)GetListStatus.Refresh)
                {
                    PList = ((Task.Run(() => _unitOfWork.Repository<Store02>().All("WMS.TK_KOR")).Result)).ToList();
                    Cache.CacheUpdate(PList, CacheKeys.StoreCacheKeys.DataListOfCorridor);
                }
                else if (Status == (int)GetListStatus.Close)
                {
                    if (Cache.CacheGet(CacheKeys.StoreCacheKeys.DataListOfCorridor) == null)
                    {
                        PList = ((Task.Run(() => _unitOfWork.Repository<Store02>().All("WMS.TK_KOR")).Result)).ToList();
                        Cache.CacheUpdate(PList, CacheKeys.StoreCacheKeys.DataListOfCorridor);
                    }
                    else
                    {
                        PList = Cache.CacheGet(CacheKeys.StoreCacheKeys.DataListOfCorridor);
                    }
                }
                return PList;
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Koridor genel Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return new List<Store02>();
            }
        }

        public override Result Operation(Store02 P)
        {
            _Result = new Result();
            string Query = "";
            P.Kaydeden = Users.AppIdentity.User.LogonUserName;        
            bool NameControlId = SetList((int)GetListStatus.Close).Where(a => a.Koridor == P.Koridor && a.ID != P.ID && a.DepoID == P.DepoID).Count() > 0 ? true : false;
            try
            {
                if (P.ID > 0)
                {
                    if (NameControlId)
                    {
                        _Result.Message = "Bu isimde başka kayıt bulunmakta";
                        _Result.Status = false;
                        return _Result;
                    }
                }
                P.Degistiren = Users.AppIdentity.User.LogonUserName;
                
                if (P.ID > 0)
                {
                    Query = QueryAnalysis<Store02>.Update(P, "WMS.TK_KOR");
                }
                else
                {
                    P.Kaydeden = Users.AppIdentity.User.LogonUserName;
                    Query = QueryAnalysis<Store02>.Insert(P, "WMS.TK_KOR");
                }
                _unitOfWork = new UnitOfWork();
                _Result.Id = _unitOfWork.Repository<Store02>().Add(P, Query);
                _unitOfWork.Commit();
                if (_Result.Id > 0)
                {
                    _Result.Id = SetList((int)GetListStatus.Refresh).OrderByDescending(a => a.ID).Select(a => a.ID).Take(1).SingleOrDefault();
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Status = true;
                }
                else if (_Result.Id == -2)
                {
                    _Result.Message = "Bu Depo için aynı isimde Koridor bulunmaktadır.";
                    _Result.Status = false;
                }
                else
                {
                    _Result.Message = "İşlem Hatalı !!!";
                    _Result.Status = false;
                }
                return _Result;
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Koridor Kayıt !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return _Result;
            }
        }

        public override List<Store02> SubList(int Id)
        {
            PList = new List<Store02>();
            try
            {
                return PList = SetList((int)GetListStatus.Refresh).Where(a => a.DepoID == Id).ToList();
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Koridor Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return PList;
            }
        }
    }
}
