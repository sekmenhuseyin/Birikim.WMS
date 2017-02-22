using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Wms12m.Entity;
using Wms12m.Repository;

namespace Wms12m.Business
{
    public class Store : abstractStore<Store01>
    {
        CacheControl<Store01> Cache;
        Result _Result;
        LogCs _Log;
        Logger _Logger;
        List<Store01> PList;
        IUnitOfWork _unitOfWork;
        Security.CustomPrincipal Users = HttpContext.Current.User as Security.CustomPrincipal;
        public override Result Delete(int Id)
        {
            _unitOfWork = new UnitOfWork();
            PList = new List<Store01>();
            Cache = new CacheControl<Store01>();
            _Result = new Result();
            try
            {
                _Result.Id = _unitOfWork.Repository<Store01>().Delete("delete from WMS.TK_DEP where ID=" + Id);
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
                _Log.Method = "Depo Silme Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return _Result;
            }
        }
        public override Store01 Detail(int Id)
        {
            try
            {
                return (GetList().Where(a => a.ID == Id).SingleOrDefault());
            }
            catch (Exception)
            {
                return new Store01();
            }
        }
        public override List<Store01> GetList()
        {
            PList = new List<Store01>();
            try
            {
                return PList = SetList((int)GetListStatus.Close);
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Depo Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return PList;
            }
        }
        private List<Store01> SetList(int Status)
        {
            _unitOfWork = new UnitOfWork();
            PList = new List<Store01>();
            Cache = new CacheControl<Store01>();
            try
            {
                if (Status == (int)GetListStatus.Refresh)
                {
                    PList = ((Task.Run(() => _unitOfWork.Repository<Store01>().All("WMS.TK_DEP")).Result)).ToList();
                    Cache.CacheUpdate(PList, CacheKeys.StoreCacheKeys.DataListOfStore);
                }
                else if (Status == (int)GetListStatus.Close)
                {
                    if (Cache.CacheGet(CacheKeys.StoreCacheKeys.DataListOfStore) == null)
                    {
                        PList = ((Task.Run(() => _unitOfWork.Repository<Store01>().All("WMS.TK_DEP")).Result)).ToList();
                        Cache.CacheUpdate(PList, CacheKeys.StoreCacheKeys.DataListOfStore);
                    }
                    else
                    {
                        PList = Cache.CacheGet(CacheKeys.StoreCacheKeys.DataListOfStore);
                    }
                }
                return PList;
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Depo genel Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return new List<Store01>();
            }
        }
        public override Result Operation(Store01 P)
        {
            _Result = new Result();
            string Query = "";
            bool NameControl = SetList((int)GetListStatus.Close).Where(a => a.DepoAdi == P.DepoAdi).Any();
            bool NameControlId = SetList((int)GetListStatus.Close).Where(a => a.DepoAdi == P.DepoAdi && a.ID != P.ID).Count() > 0 ? true : false;
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
                else if (P.ID == 0)
                {
                    if (NameControl)
                    {
                        _Result.Message = "Bu isimde başka kayıt bulunmakta";
                        _Result.Status = false;
                        return _Result;
                    }
                }
                P.Degistiren = Users.AppIdentity.User.LogonUserName;
                P.DegisTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                if (P.ID > 0)
                {
                    Query = QueryAnalysis<Store01>.Update(P, "WMS.TK_DEP");
                }
                else
                {
                    P.Kaydeden = Users.AppIdentity.User.LogonUserName;
                    P.KayitTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                    Query = QueryAnalysis<Store01>.Insert(P, "WMS.TK_DEP");
                }
                _unitOfWork = new UnitOfWork();
                _Result.Id = _unitOfWork.Repository<Store01>().Add(P, Query);
                _unitOfWork.Commit();
                if (_Result.Id > 0)
                {
                    _Result.Id = SetList((int)GetListStatus.Refresh).OrderByDescending(a => a.ID).Select(a => a.ID).Take(1).SingleOrDefault();
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Status = true;
                }
                else if (_Result.Id==-2)
                {
                    _Result.Message = "Aynı Depo Kodu veya Depo Adına sahip kayıt bulunmaktadır !!!";
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
                _Log.Method = "Depo Kayıt !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return _Result;
            }
        }
        public override List<Store01> SubList(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
