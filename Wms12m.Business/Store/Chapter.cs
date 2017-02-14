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
    public class Chapter : abstractStore<Store04>
    {
        CacheControl<Store04> Cache;
        Result _Result;
        LogCs _Log;
        Logger _Logger;
        List<Store04> PList;
        IUnitOfWork _unitOfWork;
        Wms12m.Security.CustomPrincipal Users = HttpContext.Current.User as Wms12m.Security.CustomPrincipal;
        public override Result Delete(int Id)
        {
            _unitOfWork = new UnitOfWork();
            PList = new List<Store04>();
            Cache = new CacheControl<Store04>();
            _Result = new Result();
            try
            {
                _Result.Id = _unitOfWork.Repository<Store04>().Delete("delete from Store04 where Id=" + Id);
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
                _Log.Method = "Bölüm Silme Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return _Result;
            }
        }
        public override Store04 Detail(int Id)
        {
            try
            {
                return (GetList().Where(a => a.ID == Id).SingleOrDefault());
            }
            catch (Exception)
            {
                return new Store04();
            }
        }
        public override List<Store04> GetList()
        {
            PList = new List<Store04>();
            try
            {
                return PList = SetList((int)GetListStatus.Close);
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Bölüm Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return PList;
            }
        }
        private List<Store04> SetList(int Status)
        {
            _unitOfWork = new UnitOfWork();
            PList = new List<Store04>();
            Cache = new CacheControl<Store04>();
            try
            {
                if (Status == (int)GetListStatus.Refresh)
                {
                    PList = ((Task.Run(() => _unitOfWork.Repository<Store04>().QueryAll("select * from Store04")).Result)).ToList();
                    Cache.CacheUpdate(PList, CacheKeys.StoreCacheKeys.DataListOfChapter);
                }
                else if (Status == (int)GetListStatus.Close)
                {
                    if (Cache.CacheGet(CacheKeys.StoreCacheKeys.DataListOfChapter) == null)
                    {
                        PList = ((Task.Run(() => _unitOfWork.Repository<Store04>().QueryAll("select * from Store04")).Result)).ToList();
                        Cache.CacheUpdate(PList, CacheKeys.StoreCacheKeys.DataListOfChapter);
                    }
                    else
                    {
                        PList = Cache.CacheGet(CacheKeys.StoreCacheKeys.DataListOfChapter);
                    }
                }
                return PList;
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Bölüm genel Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return new List<Store04>();
            }
        }
        public override Result Operation(Store04 P)
        {
            _Result = new Result();
            string Query = "";
            P.Kaydeden = Users.AppIdentity.User.LogonUserName;

            try
            {
                if (P.ID > 0)
                {
                    Query = QueryAnalysis<Store04>.Update(P, "Store04");
                }
                else
                {
                    Query = QueryAnalysis<Store04>.Insert(P, "dbo.Store04");
                }
                _unitOfWork = new UnitOfWork();
                _Result.Id = _unitOfWork.Repository<Store04>().Add(P, Query);
                _unitOfWork.Commit();
                if (_Result.Id > 0)
                {
                    _Result.Id = SetList((int)GetListStatus.Refresh).OrderByDescending(a => a.ID).Select(a => a.ID).Take(1).SingleOrDefault();
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Status = true;
                }
                else if (_Result.Id == -2)
                {
                    _Result.Message = "Bu Raf için aynı isimde Bölüm bulunmaktadır.";
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
                _Log.Method = "Bölüm Kayıt !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return _Result;
            }
        }
        public override List<Store04> SubList(int Id)
        {
            PList = new List<Store04>();
            try
            {
                return PList = SetList((int)GetListStatus.Refresh).Where(a => a.RafID == Id).ToList();
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Bölüm Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return PList;
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
