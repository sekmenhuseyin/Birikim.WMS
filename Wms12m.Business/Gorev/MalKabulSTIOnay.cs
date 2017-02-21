﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class MalKabulSTIOnay : abstractMalKabul<STII>
    {
        public class dene
        {
            Security.CustomPrincipal Users = HttpContext.Current.User as Security.CustomPrincipal;
            private WMSEntities db = new WMSEntities();
            public string MalKabulOnay(string EvrakNo, string CHK, int IrsaliyeNo, string GorevNo, int SirketKodu)
            {
                abstractMalKabul<STII> STIset = new MalKabulSTIOnay();
                abstractMalKabul<FTDD> FTDset = new MalKabulFTDOnay();
                abstractMalKabul<MFKK> MFKset = new MalKabulMFKOnay();
                abstractMalKabul<STKK> STKset = new MalKabulSTKOnay();
                abstractMalKabul<DSTT> DSTset = new MalKabulDSTOnay();


                var grv = db.GorevListesis.Where(m => (m.GorevNo == GorevNo) && (m.GorevTipiID == 1)).FirstOrDefault();
                if (grv != null)
                {
                    //add new
                    grv.DurumID = 12;
                    db.SaveChanges();
                }

                var irs = db.WMS_IRS.Where(m => m.ID == IrsaliyeNo).FirstOrDefault();
                if (irs != null)
                {
                    //add new
                    irs.Onay = false;
                    db.SaveChanges();
                }
                var sti = db.WMS_STI.Where(m => m.IrsaliyeID == IrsaliyeNo).ToList();
                if (sti != null)
                {
                    using (DinamikModelContext Dinamik = new DinamikModelContext(irs.SirketKod))
                    {
                        for (short i = 0; i < sti.Count; i++)
                        {
                            STI f_sti = new STI();
                            f_sti.DefaultValueSet();
                            f_sti.EvrakNo = sti[i].WMS_IRS.EvrakNo;
                            f_sti.Tarih = Convert.ToInt32(DateTime.Today.ToOADate());
                            f_sti.Chk = CHK;
                            f_sti.KynkEvrakTip = 3;
                            f_sti.SiraNo = i;
                            f_sti.IrsFat = 0;
                            f_sti.IslemTip = 1;
                            f_sti.MalKodu = sti[i].MalKodu;
                            f_sti.Miktar = sti[i].Miktar;
                            f_sti.BirimMiktar = sti[i].Miktar;
                            f_sti.Birim = sti[i].Birim;
                            f_sti.OtvDahilHaric = -1;
                            f_sti.Depo = sti[i].WMS_IRS.TK_DEP.DepoKodu;
                            f_sti.SevkTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                            f_sti.VadeTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                            f_sti.MlyYontem = -1;
                            f_sti.MhsDurum = 1;
                            f_sti.MlyMhs = 1;
                            f_sti.MhsTabloNo = 1;
                            f_sti.EvrakTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                            f_sti.KlmTutarIskNet = -1;
                            f_sti.TeslimChk = CHK;
                            f_sti.Katsayi = 1;
                            f_sti.KaynakIrsTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                            f_sti.ErekIFKEvrakTip = -1;
                            f_sti.ErekIIFKEvrakTip = -1;
                            f_sti.IrsFat2 = -1;
                            f_sti.Kredi_Donem_VadeTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                            f_sti.Duz_Yapilan_Donem = -1;
                            f_sti.Duz_Yontemi = -1;
                            f_sti.Duz_Mhs_Durumu = -1;
                            f_sti.Duz_Mly_Yontemi = -1;
                            f_sti.Duz_Mly_Mhs_Durumu = -1;
                            f_sti.AnaEvrakTip = 3;
                            f_sti.Kaydeden = Users.AppIdentity.User.LogonUserName;
                            f_sti.KayitTarih = Helper.SaatForDB(DateTime.Now);
                            f_sti.KayitSaat = Convert.ToInt32(DateTime.Today.ToOADate());
                            f_sti.Degistiren = Users.AppIdentity.User.LogonUserName;
                            f_sti.DegisTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                            f_sti.DegisSaat = Helper.SaatForDB(DateTime.Now);
                            f_sti.EFatDurum = -1;
                            f_sti.OTVTevkifatVarYok = 1;
                            f_sti.EArsivTeslimSekli = -1;
                            f_sti.EArsivFaturaTipi = -1;
                            f_sti.EArsivFaturaDurum = -1;
                            Dinamik.Context.STIs.Add(f_sti);
                            Dinamik.Context.SaveChanges();
                        }

                    }
                }





                //var ftd = db.WMS_IRS.Where(m => m.ID == IrsaliyeNo).FirstOrDefault();
                //if (ftd != null)
                //{
                //    //add new
                //    ftd.Onay = false;
                //    db.SaveChanges();
                //}
                //var mfk = db.WMS_IRS.Where(m => m.ID == IrsaliyeNo).FirstOrDefault();
                //if (mfk != null)
                //{
                //    //add new
                //    mfk.Onay = false;
                //    db.SaveChanges();
                //}

                return "";
                //return View();
            }
        }
        public override Result Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public override STII Detail(int Id)
        {
            throw new NotImplementedException();
        }

        public override List<STII> GetList()
        {
            throw new NotImplementedException();
        }

        public override Result Operation(STII P)
        {
            throw new NotImplementedException();
        }

        public override List<STII> SubList(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
