using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Wms12m.Entity;
using Wms12m.Repository;
using static Wms12m.Configuration.Enm;

namespace Wms12m.Business
{
    public class Shelf : abstractStore<Store03>
    {
        CacheControl<Store03> Cache;
        Result _Result;
        LogCs _Log;
        Logger _Logger;  
        List<Store03> PList;
        IUnitOfWork _unitOfWork;
        Wms12m.Security.CustomPrincipal Users = HttpContext.Current.User as Wms12m.Security.CustomPrincipal;
        public override Result Delete(int Id)
        {
            _unitOfWork = new UnitOfWork();
            PList = new List<Store03>();
            Cache = new CacheControl<Store03>();
            _Result = new Result();
            try
            {
                _Result.Id = _unitOfWork.Repository<Store03>().Delete("delete from WMS.TK_RAF where ID=" + Id);
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
                _Log.Method = "Raf Silme Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return _Result;
            }
        }
        public override Store03 Detail(int Id)
        {
            try
            {
                return (GetList().Where(a => a.ID == Id).SingleOrDefault());
            }
            catch (Exception)
            {
                return new Store03();
            }
        }
        public override List<Store03> GetList()
        {
            PList = new List<Store03>();
            try
            {
                return PList = SetList((int)GetListStatus.Close);
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Raf Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return PList;
            }
        }
        private List<Store03> SetList(int Status)
        {
            _unitOfWork = new UnitOfWork();
            PList = new List<Store03>();
            Cache = new CacheControl<Store03>();
            try
            {
                if (Status == (int)GetListStatus.Refresh)
                {
                    PList = ((Task.Run(() => _unitOfWork.Repository<Store03>().QueryAll(Ouery())).Result)).ToList();
                    Cache.CacheUpdate(PList, CacheKeys.StoreCacheKeys.DataListOfShelf);
                }
                else if (Status == (int)GetListStatus.Close)
                {
                    if (Cache.CacheGet(CacheKeys.StoreCacheKeys.DataListOfShelf) == null)
                    {
                        PList = ((Task.Run(() => _unitOfWork.Repository<Store03>().QueryAll(Ouery())).Result)).ToList();
                        Cache.CacheUpdate(PList, CacheKeys.StoreCacheKeys.DataListOfShelf);
                    }
                    else
                    {
                        PList = Cache.CacheGet(CacheKeys.StoreCacheKeys.DataListOfShelf);
                    }
                }
                return PList;
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Raf genel Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return new List<Store03>();
            }
        }
        public override Result Operation(Store03 P)
        {
            _Result = new Result();
            string Query = "";
            bool NameControlId = SetList((int)GetListStatus.Close).Where(a => a.Raf == P.Raf && a.ID == P.ID && a.KoridorID == P.KoridorID).Count() > 0 ? true : false;
            try
            {
                P.Degistiren = Users.AppIdentity.User.LogonUserName;
                P.DegisTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                if (P.ID > 0)
                {
                    Query = QueryAnalysis<Store03>.Update(P, "WMS.TK_RAF");
                }
                else
                {
                    P.Kaydeden = Users.AppIdentity.User.LogonUserName;
                    P.KayitTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                    Query = QueryAnalysis<Store03>.Insert(P, "WMS.TK_RAF");
                }
                _unitOfWork = new UnitOfWork();
                _Result.Id = _unitOfWork.Repository<Store03>().Add(P, Query);
                _unitOfWork.Commit();
                if (_Result.Id > 0)
                {
                    _Result.Id = SetList((int)GetListStatus.Refresh).OrderByDescending(a => a.ID).Select(a => a.ID).Take(1).SingleOrDefault();
                    Cache.CacheUpdate(PList, CacheKeys.StoreCacheKeys.DataListOfShelf);
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Status = true;
                }
                else if (_Result.Id == -2)
                {
                    _Result.Message = "Bu Koridorda aynı isimde Raf bulunmaktadır.";
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
                _Log.Method = "Raf Kayıt !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return _Result;
            }
        }
        public override List<Store03> SubList(int Id)
        {
            PList = new List<Store03>();
            try
            {
                return PList = SetList((int)GetListStatus.Close).Where(a => a.KoridorID== Id).ToList();
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Raf Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return PList;
            }
        }
        private string Ouery()
        {
            return @"SELECT        WMS.TK_RAF.ID, WMS.TK_RAF.KoridorID, WMS.TK_RAF.Raf, WMS.TK_RAF.Derinlik, WMS.TK_RAF.SiraNo, WMS.TK_RAF.Aktif, WMS.TK_RAF.Kaydeden, WMS.TK_RAF.KayitTarih, WMS.TK_RAF.Degistiren, 
                         WMS.TK_RAF.DegisTarih, WMS.TK_KOR.Koridor
FROM            WMS.TK_RAF INNER JOIN
                         WMS.TK_KOR ON WMS.TK_RAF.KoridorID = WMS.TK_KOR.ID";
        }
    }
}
