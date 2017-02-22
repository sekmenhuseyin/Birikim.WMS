﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Wms12m.Entity;
using Wms12m.Repository;

namespace Wms12m.Business
{
    public class Floor : abstractStore<Store05>
    {
        CacheControl<Store05> Cache;
        Result _Result;
        LogCs _Log;
        Logger _Logger;
        List<Store05> PList;
        IUnitOfWork _unitOfWork;
        public override Result Delete(int Id)
        {
            _unitOfWork = new UnitOfWork();
            PList = new List<Store05>();
            Cache = new CacheControl<Store05>();
            _Result = new Result();
            try
            {
                _Result.Id = _unitOfWork.Repository<Store05>().Delete("delete from WMS.TK_KAT where ID=" + Id);
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
                _Log.Method = "Kat Silme Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return _Result;
            }
        }

        public override Store05 Detail(int Id)
        {
            try
            {
                return (GetList().Where(a => a.ID == Id).SingleOrDefault());
            }
            catch (Exception)
            {
                return new Store05();
            }
        }

        public override List<Store05> GetList()
        {
            PList = new List<Store05>();
            try
            {
                return PList = SetList((int)GetListStatus.Close);
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Kat Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return PList;
            }
        }

        public override Result Operation(Store05 P)
        {
            _Result = new Result();
            string Query = "";
            P.Degistiren = SiteSessions.LoggedUserName;
            P.DegisTarih = Convert.ToInt32(DateTime.Today.ToOADate());

            try
            {
                if (P.ID > 0)
                {
                    Query = QueryAnalysis<Store05>.Update(P, "WMS.TK_KAT");
                }
                else
                {
                    P.Kaydeden = SiteSessions.LoggedUserName;
                    P.KayitTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                    Query = QueryAnalysis<Store05>.Insert(P, "WMS.TK_KAT");
                }
                _unitOfWork = new UnitOfWork();
                _Result.Id = _unitOfWork.Repository<Store05>().Add(P, Query);
                _unitOfWork.Commit();
                if (_Result.Id > 0)
                {
                    _Result.Id = SetList((int)GetListStatus.Refresh).OrderByDescending(a => a.ID).Select(a => a.ID).Take(1).SingleOrDefault();
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Status = true;
                }
                else if (_Result.Id == -2)
                {
                    _Result.Message = "Bu Bölüm için aynı isimde Kat bulunmaktadır.";
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
                _Log.Method = "Kat Kayıt !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return _Result;
            }
        }

        public override List<Store05> SubList(int Id)
        {
            PList = new List<Store05>();
            try
            {
                return PList = SetList((int)GetListStatus.Refresh).Where(a => a.RafID == Id).ToList();
            }
            catch (Exception ex)
            {
                _Log = new LogCs();
                _Log.Description = ex.Message;
                _Log.Method = "Kat Listeleme !!!";
                _Logger = new Logger();
                _Logger.Writer(_Log);
                return PList;
            }
        }

        private List<Store05> SetList(int Status)
        {
            _unitOfWork = new UnitOfWork();
            PList = new List<Store05>();
            Cache = new CacheControl<Store05>();
            try
            {
                if (Status == (int)GetListStatus.Refresh)
                {
                    PList = ((Task.Run(() => _unitOfWork.Repository<Store05>().QueryAll("select * from WMS.TK_KAT")).Result)).ToList();
                    Cache.CacheUpdate(PList, CacheKeys.StoreCacheKeys.DataListOfFloor);
                }
                else if (Status == (int)GetListStatus.Close)
                {
                    if (Cache.CacheGet(CacheKeys.StoreCacheKeys.DataListOfFloor) == null)
                    {
                        PList = ((Task.Run(() => _unitOfWork.Repository<Store05>().QueryAll("select * from WMS.TK_KAT")).Result)).ToList();
                        Cache.CacheUpdate(PList, CacheKeys.StoreCacheKeys.DataListOfFloor);
                    }
                    else
                    {
                        PList = Cache.CacheGet(CacheKeys.StoreCacheKeys.DataListOfFloor);
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
                return new List<Store05>();
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
