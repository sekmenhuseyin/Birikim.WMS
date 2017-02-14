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
    public class Size : abstractStore<Store06>
    {
        CacheControl<Store06> Cache;
        Result _Result;
        LogCs _Log;
        Logger _Logger;
        List<Store06> PList;
        IUnitOfWork _unitOfWork;
        Wms12m.Security.CustomPrincipal Users = HttpContext.Current.User as Wms12m.Security.CustomPrincipal;
        public override Result Delete(int Id)
        {
            _unitOfWork = new UnitOfWork();
            PList = new List<Store06>();
            Cache = new CacheControl<Store06>();
            _Result = new Result();
            try
            {
                _Result.Id = _unitOfWork.Repository<Store06>().Delete("delete from Store06 where Id=" + Id);
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
                _Log.Method = "Ölçü Silme Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return _Result;
            }
        }

        public override Store06 Detail(int Id)
        {
            try
            {
                return (GetList().Where(a => a.ID == Id).SingleOrDefault());
            }
            catch (Exception)
            {
                return new Store06();
            }
        }

        public override List<Store06> GetList()
        {
            PList = new List<Store06>();
            try
            {
                return PList = SetList((int)GetListStatus.Close);
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Ölçü Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return PList;
            }
        }

        public override Result Operation(Store06 P)
        {
            _Result = new Result();
            string Query = "";
            P.Kaydeden = Users.AppIdentity.User.LogonUserName;

            try
            {
                if (P.ID > 0)
                {
                    Query = QueryAnalysis<Store06>.Update(P, "Store06");
                }
                else
                {
                    Query = QueryAnalysis<Store06>.Insert(P, "dbo.Store06");
                }
                _unitOfWork = new UnitOfWork();
                _Result.Id = _unitOfWork.Repository<Store06>().Add(P, Query);
                _unitOfWork.Commit();
                if (_Result.Id > 0)
                {
                    _Result.Id = SetList((int)GetListStatus.Refresh).OrderByDescending(a => a.ID).Select(a => a.ID).Take(1).SingleOrDefault();
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Status = true;
                }
                else if (_Result.Id == -2)
                {
                    _Result.Message = "Bu Kat için önceden tanımlanmış Ölçü bilgileri mevcuttur.";
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
                _Log.Method = "Ölçü Kayıt !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return _Result;
            }
        }

        public override List<Store06> SubList(int Id)
        {
            PList = new List<Store06>();
            try
            {
                return PList = SetList((int)GetListStatus.Refresh).Where(a => a.KatID == Id).ToList();
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Ölçü Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return PList;
            }
        }

        private List<Store06> SetList(int Status)
        {
            _unitOfWork = new UnitOfWork();
            PList = new List<Store06>();
            Cache = new CacheControl<Store06>();
            try
            {
                if (Status == (int)GetListStatus.Refresh)
                {
                    PList = ((Task.Run(() => _unitOfWork.Repository<Store06>().QueryAll("select * from Store06")).Result)).ToList();
                    Cache.CacheUpdate(PList, CacheKeys.StoreCacheKeys.DataListOfSize);
                }
                else if (Status == (int)GetListStatus.Close)
                {
                    if (Cache.CacheGet(CacheKeys.StoreCacheKeys.DataListOfSize) == null)
                    {
                        PList = ((Task.Run(() => _unitOfWork.Repository<Store06>().QueryAll("select * from Store06")).Result)).ToList();
                        Cache.CacheUpdate(PList, CacheKeys.StoreCacheKeys.DataListOfSize);
                    }
                    else
                    {
                        PList = Cache.CacheGet(CacheKeys.StoreCacheKeys.DataListOfSize);
                    }
                }
                return PList;
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Ölçü Genel Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return new List<Store06>();
            }
        }

        private string Ouery()
        {
            return @"SELECT dbo.Store03.Id, dbo.Store03.CorridorId, dbo.Store03.ShelfName, dbo.Store03.Locked, dbo.Store03.CreateUserId, dbo.Store03.CreateDate, dbo.Store03.Sort, dbo.Store02.CorridorName
FROM            dbo.Store03 LEFT OUTER JOIN
                         dbo.Store02 ON dbo.Store03.CorridorId = dbo.Store02.Id";
        }
    }
}
