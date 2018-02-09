using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class PriceController : RootController
    {
        #region GM

        public ActionResult GM()
        {
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult GM_List()
        {
            return PartialView();
        }

        public string GMOnayCek()
        {
            var RT = db.Database.SqlQuery<FiyatOnayGMSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayListGM]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public PartialViewResult GM_List_Koleksiyon()
        {
            return PartialView();
        }

        public string GMOnayCekGMKoleksiyon()
        {
            var RT = db.Database.SqlQuery<FiyatKoleksiyonSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayListGM_Koleksiyon]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public PartialViewResult GM_List_Grup()
        {
            return PartialView();
        }

        public string GMOnayCekGMGrup()
        {
            var RT = db.Database.SqlQuery<FiyatGrupSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayListGM_GrupEbatYuzey]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public JsonResult Onay(string Data)//GM
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Writing) == false) return null;

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);

            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            var BasTarih = 0;
            var BitTarih = 0;
            var ListeAdi = "";
            short FytSiraNo = -1;
            try
            {
                foreach (JObject insertObj in parameters)
                {
                    var FLB = db.Database.SqlQuery<short>(string.Format("SELECT ISNULL(MAX(SiraNo),0) FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE FiyatListNum='{1}'", vUser.SirketKodu, insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    if (FLB > 0)
                    {
                        FytSiraNo = FLB;
                        FytSiraNo++;
                    }
                    else
                    {
                        FytSiraNo = 0;
                    }

                    if (insertObj["Durum"].ToString() == "Yeni Kayıt")
                    {
                        var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", vUser.SirketKodu, insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                        var fyt = new FYT();

                        if (fytData.FiyatListName != "")
                        {
                            ListeAdi = fytData.FiyatListName;
                            BasTarih = fytData.BasTarih;
                            BitTarih = fytData.BitTarih;
                        }
                        else
                        {
                            ListeAdi = insertObj["FiyatListNum"].ToString() + " Fiyat Listesi";
                            if (ListeAdi.Length > 30)
                            {
                                ListeAdi = ListeAdi.Substring(0, 30);
                            }

                            BasTarih = insertObj["BasTarih"].ToInt32();
                            BitTarih = insertObj["BitTarih"].ToInt32();
                        }
                        fyt.MalKodu = insertObj["MalKodu"].ToString();
                        fyt.BasTarih = BasTarih;
                        fyt.BasSaat = fn.ToOATime();
                        fyt.BitTarih = BitTarih;
                        fyt.BitSaat = fn.ToOATime();
                        fyt.SiraNo = FytSiraNo;
                        fyt.RenkBedenKod1 = "";
                        fyt.RenkBedenKod2 = "";
                        fyt.RenkBedenKod3 = "";
                        fyt.RenkBedenKod4 = "";
                        fyt.FiyatListNum = insertObj["FiyatListNum"].ToString();
                        fyt.FiyatTuru = 1;
                        fyt.Kod1 = "";
                        fyt.Kod2 = "";
                        fyt.Kod3 = "";
                        fyt.KodTipi = 0;
                        fyt.MusteriKodu = insertObj["HesapKodu"].ToString();
                        fyt.AlisFiyat1 = 0;
                        fyt.AlisFiyat2 = 0;
                        fyt.AlisFiyat3 = 0;
                        fyt.DvzAlisFiyat1 = 0;
                        fyt.DvzAlisFiyat2 = 0;
                        fyt.DvzAlisFiyat3 = 0;
                        fyt.AF1DovizCinsi = "";
                        fyt.AF2DovizCinsi = "";
                        fyt.AF3DovizCinsi = "";
                        fyt.AF1KDV = 0;
                        fyt.AF2KDV = 0;
                        fyt.AF3KDV = 0;
                        fyt.DovizAF1KDV = 0;
                        fyt.DovizAF2KDV = 0;
                        fyt.DovizAF3KDV = 0;
                        fyt.AF1Birim = 0;
                        fyt.AF2Birim = 0;
                        fyt.AF3Birim = 0;
                        fyt.DovizAF1Birim = 0;
                        fyt.DovizAF2Birim = 0;
                        fyt.DovizAF3Birim = 0;
                        fyt.SatisFiyat1 = insertObj["SatisFiyat1"].ToDecimal();
                        fyt.SatisFiyat2 = 0;
                        fyt.SatisFiyat3 = 0;
                        fyt.SatisFiyat4 = 0;
                        fyt.SatisFiyat5 = 0;
                        fyt.SatisFiyat6 = 0;
                        fyt.DvzSatisFiyat1 = insertObj["DovizSatisFiyat1"].ToDecimal();
                        fyt.DvzSatisFiyat2 = 0;
                        fyt.DvzSatisFiyat3 = 0;
                        fyt.SF1DovizCinsi = insertObj["DovizCinsi"].ToString().Trim();
                        fyt.SF2DovizCinsi = "";
                        fyt.SF3DovizCinsi = "";
                        fyt.SF4DovizCinsi = "";
                        fyt.SF5DovizCinsi = "";
                        fyt.SF6DovizCinsi = "";
                        fyt.SF1KDV = 0;
                        fyt.SF2KDV = 0;
                        fyt.SF3KDV = 0;
                        fyt.SF4KDV = 0;
                        fyt.SF5KDV = 0;
                        fyt.SF6KDV = 0;
                        fyt.DovizSF1KDV = 0;
                        fyt.DovizSF2KDV = 0;
                        fyt.DovizSF3KDV = 0;
                        fyt.SF1Birim = insertObj["SatisFiyat1BirimInt"].ToShort();
                        fyt.SF2Birim = -1;
                        fyt.SF3Birim = -1;
                        fyt.SF4Birim = -1;
                        fyt.SF5Birim = -1;
                        fyt.SF6Birim = -1;
                        fyt.DovizSF1Birim = insertObj["DovizSF1BirimInt"].ToShort();
                        fyt.DovizSF2Birim = -1;
                        fyt.DovizSF3Birim = -1;
                        fyt.FiyatListName = ListeAdi;
                        fyt.SatisFiyatAltLimit = 0;
                        fyt.SatisFiyatUstLimit = 0;
                        fyt.SF1ValorGun = 0;
                        fyt.SF2ValorGun = 0;
                        fyt.SF3ValorGun = 0;
                        fyt.SF4ValorGun = 0;
                        fyt.SF5ValorGun = 0;
                        fyt.SF6ValorGun = 0;
                        fyt.DvzSF1ValorGun = 0;
                        fyt.DvzSF2ValorGun = 0;
                        fyt.DvzSF3ValorGun = 0;
                        fyt.KayitTuru = -1;
                        fyt.GuvenlikKod = "";
                        fyt.Kaydeden = vUser.UserName;
                        fyt.KayitTarih = fn.ToOADate();
                        fyt.KayitSaat = fn.ToOATime();
                        fyt.KayitKaynak = 117;
                        fyt.KayitSurum = "8.10.100";
                        fyt.Degistiren = vUser.UserName;
                        fyt.DegisTarih = fn.ToOADate();
                        fyt.DegisSaat = fn.ToOATime();
                        fyt.DegisKaynak = 117;
                        fyt.DegisSurum = "8.10.100";
                        fyt.CheckSum = 12;
                        fyt.Aciklama = "";
                        fyt.Kod4 = "";
                        fyt.Kod5 = "";
                        fyt.Kod6 = "";
                        fyt.Kod7 = "";
                        fyt.Kod8 = "";
                        fyt.Kod9 = "";
                        fyt.Kod10 = "";
                        fyt.Kod11 = 0;
                        fyt.Kod12 = 0;
                        fyt.FiyatListeTipi = -1;
                        sqlexper.Insert(fyt);
                        var sonuc = sqlexper.AcceptChanges();
                        db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", vUser.SirketKodu, insertObj["ID"].ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                    }
                    else if (insertObj["Durum"].ToString() == "Silinecek Kayıt")
                    {
                        var durum2sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE ROW_ID = {1}", vUser.SirketKodu, insertObj["ROW_ID"].ToInt32());
                        var fytSilinecek = db.Database.SqlQuery<FYT>(durum2sql).ToList();
                        if (fytSilinecek.Count > 0)
                        {
                            db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE ROW_ID = {1}", vUser.SirketKodu, insertObj["ROW_ID"].ToInt32()));
                        }
                        db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", vUser.SirketKodu, insertObj["ID"].ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                    }
                    else if (insertObj["Durum"].ToString() == "Güncellenecek Kayıt")
                    {
                        var durum3sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE ROW_ID = {1}", vUser.SirketKodu, insertObj["ROW_ID"].ToInt32());
                        var fytGuncellenecek = db.Database.SqlQuery<FYT>(durum3sql).FirstOrDefault();
                        if (fytGuncellenecek != null)
                        {
                            var ss = Convert.ToDecimal(insertObj["SatisFiyat1"].ToString()).ToString().Replace(",", ".");
                            var sss = string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[FYT] SET SatisFiyat1 = {5}, SF1Birim='" + insertObj["SatisFiyat1BirimInt"].ToShort() + "', DvzSatisFiyat1={1}, SF1DovizCinsi='{2}', DovizSF1Birim={3}  where ROW_ID = '{4}'", vUser.SirketKodu, insertObj["DovizSatisFiyat1"].ToString().Replace(",", "."), insertObj["DovizCinsi"].ToString().Trim(), insertObj["DovizSF1BirimInt"].ToShort(), fytGuncellenecek.ROW_ID, ss);
                            db.Database.ExecuteSqlCommand(sss);
                        }
                        db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", vUser.SirketKodu, insertObj["ID"].ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                    }
                }
                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";
            }
            catch (Exception)
            {
                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Onay_Koleksiyon(string Data)//GM
        {
            var _Result = new Result(true);

            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Writing) == false) return null;

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);

            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            var BasTarih = 0;
            var BitTarih = 0;
            var ListeAdi = "";
            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    if (FiyatMaxSiraNo.ContainsKey(insertObj["FiyatListNum"].ToString()))
                    {
                        continue;
                    }
                    else
                    {
                        var FytSiraNo = -1;
                        var FLB = db.Database.SqlQuery<short>(string.Format("SELECT ISNULL(MAX(SiraNo),0) FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE FiyatListNum='{1}'", vUser.SirketKodu, insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                        if (FLB > 0)
                        {
                            FytSiraNo = FLB;
                            FytSiraNo++;
                        }
                        else
                        {
                            FytSiraNo = 0;
                        }

                        FiyatMaxSiraNo.Add(insertObj["FiyatListNum"].ToString(), FytSiraNo);
                    }

                    var sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod = 'PARKE' AND F.FiyatListNum = '{1}'
                    AND S.Kod4 = '{2}' AND S.TipKod = '{3}' AND F.SatisFiyat1 = {4}
                    AND F.SatisFiyat1Birim = '{5}' AND F.SatisFiyat1BirimInt = {6}
                    AND F.DovizSatisFiyat1 = {7} AND F.DovizSF1Birim = '{8}' AND F.DovizSF1BirimInt = {9}
                    AND F.DovizCinsi = '{10}' AND F.Onay = 1 AND F.SPGMYOnay = 1 AND F.GMOnay = 0 ", vUser.SirketKodu, insertObj["FiyatListNum"].ToString(), insertObj["Kod4"].ToString(),
                    insertObj["TipKod"].ToString(), insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(), insertObj["SatisFiyat1BirimInt"].ToString(),
                    insertObj["DovizSatisFiyat1"].ToString().Replace(",", "."), insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
                    );
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = string.Join(", ", fiyatID.ToArray());
                    sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat] where ID in({1})", vUser.SirketKodu, result);
                    var OnayFiyatList = db.Database.SqlQuery<Fiyat>(sql).ToList();
                    foreach (Fiyat rts in OnayFiyatList)
                    {
                        if (rts.Durum == 1) //YENİ KAYIT
                        {
                            var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", vUser.SirketKodu, rts.FiyatListNum.ToString())).FirstOrDefault();
                            var fyt = new FYT();

                            if (fytData.FiyatListNum != null)
                            {
                                ListeAdi = fytData.FiyatListName;
                                BasTarih = fytData.BasTarih;
                                BitTarih = fytData.BitTarih;
                            }
                            else
                            {
                                ListeAdi = rts.FiyatListNum.ToString() + " Fiyat Listesi";
                                if (ListeAdi.Length > 30)
                                {
                                    ListeAdi = ListeAdi.Substring(0, 30);
                                }

                                BasTarih = rts.BasTarih.ToInt32();
                                BitTarih = rts.BitTarih.ToInt32();
                            }

                            fyt.MalKodu = rts.MalKodu.ToString();
                            fyt.BasTarih = BasTarih;
                            fyt.BasSaat = fn.ToOATime();
                            fyt.BitTarih = BitTarih;
                            fyt.BitSaat = fn.ToOATime();
                            if (FiyatMaxSiraNo.ContainsKey(insertObj["FiyatListNum"].ToString()))
                            {
                                fyt.SiraNo = Convert.ToInt16(FiyatMaxSiraNo[insertObj["FiyatListNum"].ToString()]);
                                FiyatMaxSiraNo[insertObj["FiyatListNum"].ToString()] = FiyatMaxSiraNo[insertObj["FiyatListNum"].ToString()]++;
                            }
                            else
                            {
                                fyt.SiraNo = 0;
                            }

                            fyt.RenkBedenKod1 = "";
                            fyt.RenkBedenKod2 = "";
                            fyt.RenkBedenKod3 = "";
                            fyt.RenkBedenKod4 = "";
                            fyt.FiyatListNum = rts.FiyatListNum.ToString();
                            fyt.FiyatTuru = 1;
                            fyt.Kod1 = "";
                            fyt.Kod2 = "";
                            fyt.Kod3 = "";
                            fyt.KodTipi = 0;
                            fyt.MusteriKodu = rts.HesapKodu.ToString();
                            fyt.AlisFiyat1 = 0;
                            fyt.AlisFiyat2 = 0;
                            fyt.AlisFiyat3 = 0;
                            fyt.DvzAlisFiyat1 = 0;
                            fyt.DvzAlisFiyat2 = 0;
                            fyt.DvzAlisFiyat3 = 0;
                            fyt.AF1DovizCinsi = "";
                            fyt.AF2DovizCinsi = "";
                            fyt.AF3DovizCinsi = "";
                            fyt.AF1KDV = 0;
                            fyt.AF2KDV = 0;
                            fyt.AF3KDV = 0;
                            fyt.DovizAF1KDV = 0;
                            fyt.DovizAF2KDV = 0;
                            fyt.DovizAF3KDV = 0;
                            fyt.AF1Birim = 0;
                            fyt.AF2Birim = 0;
                            fyt.AF3Birim = 0;
                            fyt.DovizAF1Birim = 0;
                            fyt.DovizAF2Birim = 0;
                            fyt.DovizAF3Birim = 0;
                            fyt.SatisFiyat1 = rts.SatisFiyat1.ToDecimal();
                            fyt.SatisFiyat2 = 0;
                            fyt.SatisFiyat3 = 0;
                            fyt.SatisFiyat4 = 0;
                            fyt.SatisFiyat5 = 0;
                            fyt.SatisFiyat6 = 0;
                            fyt.DvzSatisFiyat1 = rts.DovizSatisFiyat1.ToDecimal();
                            fyt.DvzSatisFiyat2 = 0;
                            fyt.DvzSatisFiyat3 = 0;
                            fyt.SF1DovizCinsi = rts.DovizCinsi.ToString().Trim();
                            fyt.SF2DovizCinsi = "";
                            fyt.SF3DovizCinsi = "";
                            fyt.SF4DovizCinsi = "";
                            fyt.SF5DovizCinsi = "";
                            fyt.SF6DovizCinsi = "";
                            fyt.SF1KDV = 0;
                            fyt.SF2KDV = 0;
                            fyt.SF3KDV = 0;
                            fyt.SF4KDV = 0;
                            fyt.SF5KDV = 0;
                            fyt.SF6KDV = 0;
                            fyt.DovizSF1KDV = 0;
                            fyt.DovizSF2KDV = 0;
                            fyt.DovizSF3KDV = 0;
                            fyt.SF1Birim = rts.SatisFiyat1BirimInt.ToShort(); ;
                            fyt.SF2Birim = -1;
                            fyt.SF3Birim = -1;
                            fyt.SF4Birim = -1;
                            fyt.SF5Birim = -1;
                            fyt.SF6Birim = -1;
                            fyt.DovizSF1Birim = rts.DovizSF1BirimInt.ToShort();
                            fyt.DovizSF2Birim = -1;
                            fyt.DovizSF3Birim = -1;
                            fyt.FiyatListName = ListeAdi;
                            fyt.SatisFiyatAltLimit = 0;
                            fyt.SatisFiyatUstLimit = 0;
                            fyt.SF1ValorGun = 0;
                            fyt.SF2ValorGun = 0;
                            fyt.SF3ValorGun = 0;
                            fyt.SF4ValorGun = 0;
                            fyt.SF5ValorGun = 0;
                            fyt.SF6ValorGun = 0;
                            fyt.DvzSF1ValorGun = 0;
                            fyt.DvzSF2ValorGun = 0;
                            fyt.DvzSF3ValorGun = 0;
                            fyt.KayitTuru = -1;
                            fyt.GuvenlikKod = "";
                            fyt.Kaydeden = vUser.UserName.ToString();
                            fyt.KayitTarih = (int)DateTime.Now.ToOADate();
                            fyt.KayitSaat = fn.ToOATime();
                            fyt.KayitKaynak = 117;
                            fyt.KayitSurum = "8.10.100";
                            fyt.Degistiren = vUser.UserName.ToString();
                            fyt.DegisTarih = (int)DateTime.Now.ToOADate();
                            fyt.DegisSaat = fn.ToOATime();
                            fyt.DegisKaynak = 117;
                            fyt.DegisSurum = "8.10.100";
                            fyt.CheckSum = 12;
                            fyt.Aciklama = "";
                            fyt.Kod4 = "";
                            fyt.Kod5 = "";
                            fyt.Kod6 = "";
                            fyt.Kod7 = "";
                            fyt.Kod8 = "";
                            fyt.Kod9 = "";
                            fyt.Kod10 = "";
                            fyt.Kod11 = 0;
                            fyt.Kod12 = 0;

                            var date = DateTime.Now;
                            var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                            sqlexper.Insert(fyt);
                            var sonuc = sqlexper.AcceptChanges();
                            db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", vUser.SirketKodu, rts.ID, shortDate));
                        }
                        else if (rts.Durum == 2)//Silinecek Kayıt
                        {
                            var durum2sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE ROW_ID = {1}", vUser.SirketKodu, rts.ROW_ID);
                            var fytSilinecek = db.Database.SqlQuery<FYT>(durum2sql).ToList();
                            if (fytSilinecek.Count > 0)
                            {
                                db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE ROW_ID = {1}", vUser.SirketKodu, rts.ROW_ID));
                            }

                            var date = DateTime.Now;
                            var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                            db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", vUser.SirketKodu, rts.ID, shortDate));
                        }
                        else if (rts.Durum == 3)//"Güncellenecek Kayıt"
                        {
                            var durum3sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE ROW_ID = {1}", vUser.SirketKodu, rts.ROW_ID);
                            var fytGuncellenecek = db.Database.SqlQuery<FYT>(durum3sql).FirstOrDefault();
                            if (fytGuncellenecek != null)
                            {
                                db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[FYT] SET SatisFiyat1 = " + rts.SatisFiyat1.ToDecimal() + ", SF1Birim='" + rts.SatisFiyat1BirimInt.ToShort() + "', DvzSatisFiyat1={1}, SF1DovizCinsi='{2}', DovizSF1Birim={3}  where ROW_ID = '{4}'", vUser.SirketKodu, rts.DovizSatisFiyat1.ToDecimal(), rts.DovizCinsi.ToString().Trim(), rts.DovizSF1BirimInt.ToShort(), fytGuncellenecek.ROW_ID));
                            }

                            var date = DateTime.Now;
                            var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                            db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", vUser.SirketKodu, rts.ID, shortDate));
                        }
                    }
                }

                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";
            }
            catch (Exception)
            {
                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Onay_Grup(string Data)//GM
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Writing) == false) return null;

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);

            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            var BasTarih = 0;
            var BitTarih = 0;
            var ListeAdi = "";
            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    if (FiyatMaxSiraNo.ContainsKey(insertObj["FiyatListNum"].ToString()))
                    {
                        continue;
                    }
                    else
                    {
                        var FytSiraNo = -1;
                        var FLB = db.Database.SqlQuery<short>(string.Format("SELECT ISNULL(MAX(SiraNo),0) FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE FiyatListNum='{1}'", vUser.SirketKodu, insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                        if (FLB > 0)
                        {
                            FytSiraNo = FLB;
                            FytSiraNo++;
                        }
                        else
                        {
                            FytSiraNo = 0;
                        }

                        FiyatMaxSiraNo.Add(insertObj["FiyatListNum"].ToString(), FytSiraNo);
                    }

                    var sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod <> 'PARKE' AND S.GrupKod = '{1}' AND F.FiyatListNum = '{2}'
                    AND S.TipKod = '{3}' AND S.Kod2 = '{4}' AND S.Kod3 = '{5}' AND S.Kod1 ='{6}'
                    AND S.Kod8 ='{7}'
                    AND F.SatisFiyat1 = {8} AND F.SatisFiyat1Birim = '{9}' AND F.SatisFiyat1BirimInt = {10}
                    AND F.DovizSatisFiyat1 = {11} AND F.DovizSF1Birim = '{12}' AND F.DovizSF1BirimInt = {13}
                    AND F.DovizCinsi = '{14}' AND F.Onay = 1 AND F.SPGMYOnay = 1 AND F.GMOnay = 0 ", vUser.SirketKodu, insertObj["GrupKod"].ToString(),
                    insertObj["FiyatListNum"].ToString(), insertObj["Kalite"].ToString(),
                    insertObj["En"].ToString(), insertObj["Boy"].ToString(), insertObj["Kalinlik"].ToString(), insertObj["Yuzey"].ToString(),
                    insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(),
                    insertObj["SatisFiyat1BirimInt"].ToInt32(), insertObj["DovizSatisFiyat1"].ToString().Replace(",", "."),
                    insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
                    );
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = string.Join(", ", fiyatID.ToArray());
                    sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat] where ID in({1})", vUser.SirketKodu, result);
                    var OnayFiyatList = db.Database.SqlQuery<Fiyat>(sql).ToList();
                    foreach (Fiyat rts in OnayFiyatList)
                    {
                        if (rts.Durum == 1) //YENİ KAYIT
                        {
                            var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", vUser.SirketKodu, rts.FiyatListNum.ToString())).FirstOrDefault();

                            var fyt = new FYT();

                            if (fytData.FiyatListNum != null)
                            {
                                ListeAdi = fytData.FiyatListName;
                                BasTarih = fytData.BasTarih;
                                BitTarih = fytData.BitTarih;
                            }
                            else
                            {
                                ListeAdi = rts.FiyatListNum.ToString() + " Fiyat Listesi";
                                if (ListeAdi.Length > 30)
                                {
                                    ListeAdi = ListeAdi.Substring(0, 30);
                                }

                                BasTarih = rts.BasTarih.ToInt32();
                                BitTarih = rts.BitTarih.ToInt32();
                            }

                            fyt.MalKodu = rts.MalKodu.ToString();
                            fyt.BasTarih = BasTarih;
                            fyt.BasSaat = fn.ToOATime();
                            fyt.BitTarih = BitTarih;
                            fyt.BitSaat = fn.ToOATime();
                            if (FiyatMaxSiraNo.ContainsKey(insertObj["FiyatListNum"].ToString()))
                            {
                                fyt.SiraNo = Convert.ToInt16(FiyatMaxSiraNo[insertObj["FiyatListNum"].ToString()]);
                                FiyatMaxSiraNo[insertObj["FiyatListNum"].ToString()] = FiyatMaxSiraNo[insertObj["FiyatListNum"].ToString()]++;
                            }
                            else
                            {
                                fyt.SiraNo = 0;
                            }

                            fyt.RenkBedenKod1 = "";
                            fyt.RenkBedenKod2 = "";
                            fyt.RenkBedenKod3 = "";
                            fyt.RenkBedenKod4 = "";
                            fyt.FiyatListNum = rts.FiyatListNum.ToString();
                            fyt.FiyatTuru = 1;
                            fyt.Kod1 = "";
                            fyt.Kod2 = "";
                            fyt.Kod3 = "";
                            fyt.KodTipi = 0;
                            fyt.MusteriKodu = rts.HesapKodu.ToString();
                            fyt.AlisFiyat1 = 0;
                            fyt.AlisFiyat2 = 0;
                            fyt.AlisFiyat3 = 0;
                            fyt.DvzAlisFiyat1 = 0;
                            fyt.DvzAlisFiyat2 = 0;
                            fyt.DvzAlisFiyat3 = 0;
                            fyt.AF1DovizCinsi = "";
                            fyt.AF2DovizCinsi = "";
                            fyt.AF3DovizCinsi = "";
                            fyt.AF1KDV = 0;
                            fyt.AF2KDV = 0;
                            fyt.AF3KDV = 0;
                            fyt.DovizAF1KDV = 0;
                            fyt.DovizAF2KDV = 0;
                            fyt.DovizAF3KDV = 0;
                            fyt.AF1Birim = 0;
                            fyt.AF2Birim = 0;
                            fyt.AF3Birim = 0;
                            fyt.DovizAF1Birim = 0;
                            fyt.DovizAF2Birim = 0;
                            fyt.DovizAF3Birim = 0;
                            fyt.SatisFiyat1 = rts.SatisFiyat1.ToDecimal();
                            fyt.SatisFiyat2 = 0;
                            fyt.SatisFiyat3 = 0;
                            fyt.SatisFiyat4 = 0;
                            fyt.SatisFiyat5 = 0;
                            fyt.SatisFiyat6 = 0;
                            fyt.DvzSatisFiyat1 = rts.DovizSatisFiyat1.ToDecimal();
                            fyt.DvzSatisFiyat2 = 0;
                            fyt.DvzSatisFiyat3 = 0;
                            fyt.SF1DovizCinsi = rts.DovizCinsi.ToString().Trim();
                            fyt.SF2DovizCinsi = "";
                            fyt.SF3DovizCinsi = "";
                            fyt.SF4DovizCinsi = "";
                            fyt.SF5DovizCinsi = "";
                            fyt.SF6DovizCinsi = "";
                            fyt.SF1KDV = 0;
                            fyt.SF2KDV = 0;
                            fyt.SF3KDV = 0;
                            fyt.SF4KDV = 0;
                            fyt.SF5KDV = 0;
                            fyt.SF6KDV = 0;
                            fyt.DovizSF1KDV = 0;
                            fyt.DovizSF2KDV = 0;
                            fyt.DovizSF3KDV = 0;
                            fyt.SF1Birim = rts.SatisFiyat1BirimInt.ToShort(); ;
                            fyt.SF2Birim = -1;
                            fyt.SF3Birim = -1;
                            fyt.SF4Birim = -1;
                            fyt.SF5Birim = -1;
                            fyt.SF6Birim = -1;
                            fyt.DovizSF1Birim = rts.DovizSF1BirimInt.ToShort();
                            fyt.DovizSF2Birim = -1;
                            fyt.DovizSF3Birim = -1;
                            fyt.FiyatListName = ListeAdi;
                            fyt.SatisFiyatAltLimit = 0;
                            fyt.SatisFiyatUstLimit = 0;
                            fyt.SF1ValorGun = 0;
                            fyt.SF2ValorGun = 0;
                            fyt.SF3ValorGun = 0;
                            fyt.SF4ValorGun = 0;
                            fyt.SF5ValorGun = 0;
                            fyt.SF6ValorGun = 0;
                            fyt.DvzSF1ValorGun = 0;
                            fyt.DvzSF2ValorGun = 0;
                            fyt.DvzSF3ValorGun = 0;
                            fyt.KayitTuru = -1;
                            fyt.GuvenlikKod = "";
                            fyt.Kaydeden = vUser.UserName.ToString();
                            fyt.KayitTarih = (int)DateTime.Now.ToOADate();
                            fyt.KayitSaat = fn.ToOATime();
                            fyt.KayitKaynak = 117;
                            fyt.KayitSurum = "8.10.100";
                            fyt.Degistiren = vUser.UserName.ToString();
                            fyt.DegisTarih = (int)DateTime.Now.ToOADate();
                            fyt.DegisSaat = fn.ToOATime();
                            fyt.DegisKaynak = 117;
                            fyt.DegisSurum = "8.10.100";
                            fyt.CheckSum = 12;
                            fyt.Aciklama = "";
                            fyt.Kod4 = "";
                            fyt.Kod5 = "";
                            fyt.Kod6 = "";
                            fyt.Kod7 = "";
                            fyt.Kod8 = "";
                            fyt.Kod9 = "";
                            fyt.Kod10 = "";
                            fyt.Kod11 = 0;
                            fyt.Kod12 = 0;

                            var date = DateTime.Now;
                            var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                            sqlexper.Insert(fyt);
                            var sonuc = sqlexper.AcceptChanges();
                            db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", vUser.SirketKodu, rts.ID, shortDate));
                        }
                        else if (rts.Durum == 2)//Silinecek Kayıt
                        {
                            var durum2sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE ROW_ID = {1}", vUser.SirketKodu, rts.ROW_ID);
                            var fytSilinecek = db.Database.SqlQuery<FYT>(durum2sql).ToList();
                            if (fytSilinecek.Count > 0)
                            {
                                db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE ROW_ID = {1}", vUser.SirketKodu, rts.ROW_ID));
                            }

                            var date = DateTime.Now;
                            var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                            db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", vUser.SirketKodu, rts.ID, shortDate));
                        }
                        else if (rts.Durum == 3)//"Güncellenecek Kayıt"
                        {
                            var durum3sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE ROW_ID = {1}", vUser.SirketKodu, rts.ROW_ID);
                            var fytGuncellenecek = db.Database.SqlQuery<FYT>(durum3sql).FirstOrDefault();
                            if (fytGuncellenecek != null)
                            {
                                // fytGuncellenecek.SatisFiyat1 = rts.SatisFiyat1;
                                // fytGuncellenecek.SF1Birim = (short)rts.SatisFiyat1BirimInt;
                                // fytGuncellenecek.DvzSatisFiyat1 = rts.DovizSatisFiyat1;
                                // fytGuncellenecek.SF1DovizCinsi = rts.DovizCinsi.Trim();
                                // fytGuncellenecek.DovizSF1Birim = (short)rts.DovizSF1BirimInt;
                                db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[FYT] SET SatisFiyat1 = " + rts.SatisFiyat1.ToDecimal() + ", SF1Birim='" + rts.SatisFiyat1BirimInt.ToShort() + "', DvzSatisFiyat1={1}, SF1DovizCinsi='{2}', DovizSF1Birim={3}  where ROW_ID = '{4}'", vUser.SirketKodu, rts.DovizSatisFiyat1.ToDecimal(), rts.DovizCinsi.ToString().Trim(), rts.DovizSF1BirimInt.ToShort(), fytGuncellenecek.ROW_ID));
                            }

                            var date = DateTime.Now;
                            var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                            db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", vUser.SirketKodu, rts.ID, shortDate));
                        }
                    }
                }

                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Red(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Writing) == false) return null;

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);

            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            try
            {
                foreach (JObject insertObj in parameters)
                {
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat]  where ID = '{1}'", vUser.SirketKodu, insertObj["ID"].ToString()));
                }

                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception)
            {
                _Result.Message = "Hata oluştu";
                _Result.Status = false;
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Red_Koleksiyon(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Writing) == false) return null;

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);

            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            try
            {
                foreach (JObject insertObj in parameters)
                {
                    var sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod = 'PARKE' AND F.FiyatListNum = '{1}'
                    AND S.Kod4 = '{2}' AND S.TipKod = '{3}' AND F.SatisFiyat1 = {4}
                    AND F.SatisFiyat1Birim = '{5}' AND F.SatisFiyat1BirimInt = {6}
                    AND F.DovizSatisFiyat1 = {7} AND F.DovizSF1Birim = '{8}' AND F.DovizSF1BirimInt = {9}
                    AND F.DovizCinsi = '{10}' AND F.Onay = 1 AND F.SPGMYOnay = 1 AND F.GMOnay = 0 ", vUser.SirketKodu, insertObj["FiyatListNum"].ToString(), insertObj["Kod4"].ToString(),
insertObj["TipKod"].ToString(), insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(), insertObj["SatisFiyat1BirimInt"].ToString(),
insertObj["DovizSatisFiyat1"].ToInt32(), insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
);
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = string.Join(", ", fiyatID.ToArray());

                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat]  where ID in({1})", vUser.SirketKodu, result));
                }

                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Red_Grup(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Writing) == false) return null;

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);

            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            try
            {
                foreach (JObject insertObj in parameters)
                {
                    var sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod <> 'PARKE' AND S.GrupKod = '{1}' AND F.FiyatListNum = '{2}'
                    AND S.TipKod = '{3}' AND S.Kod2 = '{4}' AND S.Kod3 = '{5}' AND S.Kod1 ='{6}'
                    AND S.Kod8 ='{7}'
                    AND F.SatisFiyat1 = {8} AND F.SatisFiyat1Birim = '{9}' AND F.SatisFiyat1BirimInt = {10}
                    AND F.DovizSatisFiyat1 = {11} AND F.DovizSF1Birim = '{12}' AND F.DovizSF1BirimInt = {13}
                    AND F.DovizCinsi = '{14}' AND F.Onay = 1 AND F.SPGMYOnay = 1 AND F.GMOnay = 0 ", vUser.SirketKodu, insertObj["GrupKod"].ToString(),
                    insertObj["FiyatListNum"].ToString(), insertObj["Kalite"].ToString(),
                    insertObj["En"].ToString(), insertObj["Boy"].ToString(), insertObj["Kalinlik"].ToString(), insertObj["Yuzey"].ToString(),
                    insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(),
                    insertObj["SatisFiyat1BirimInt"].ToInt32(), insertObj["DovizSatisFiyat1"].ToString().Replace(",", "."),
                    insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
                    );
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = string.Join(", ", fiyatID.ToArray());

                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat]  where ID in({1})", vUser.SirketKodu, result));
                }

                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        #endregion GM

        #region SPGMY

        public ActionResult SPGMY()
        {
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult SPGMY_List()
        {
            return PartialView();
        }

        public string SPGMYOnayCek()
        {
            var RT = db.Database.SqlQuery<FiyatOnayGMSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayListSPGMY]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public PartialViewResult SPGMY_List_Koleksiyon()
        {
            return PartialView();
        }

        public string SPGMYOnayCekSPGMYKoleksiyon()
        {
            var RT = db.Database.SqlQuery<FiyatKoleksiyonSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayListSPGMY_Koleksiyon]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public PartialViewResult SPGMY_List_Grup()
        {
            return PartialView();
        }

        public string SPGMYOnayCekSPGMYGrup()
        {
            var RT = db.Database.SqlQuery<FiyatGrupSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayListSPGMY_GrupEbatYuzey]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public JsonResult Onay_SPGMY(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Writing) == false) return null;

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);

            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    var date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET SPGMYOnay = 1, SPGMYOnaylayan='" + vUser.UserName + "', SPGMYOnayTarih='{2}'  where ID = '{1}'", vUser.SirketKodu, insertObj["ID"].ToString(), shortDate));
                }
                // return "OK";

                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";
            }
            catch (Exception)
            {
                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Onay_Koleksiyon_SPGMY(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Writing) == false) return null;

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);

            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            try
            {
                foreach (JObject insertObj in parameters)
                {
                    var sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod = 'PARKE' AND F.FiyatListNum = '{1}'
                    AND S.Kod4 = '{2}' AND S.TipKod = '{3}' AND F.SatisFiyat1 = {4}
                    AND F.SatisFiyat1Birim = '{5}' AND F.SatisFiyat1BirimInt = {6}
                    AND F.DovizSatisFiyat1 = {7} AND F.DovizSF1Birim = '{8}' AND F.DovizSF1BirimInt = {9}
                    AND F.DovizCinsi = '{10}' AND F.Onay = 1  AND F.GMOnay = 0 F.SPGMYOnay = 0", vUser.SirketKodu, insertObj["FiyatListNum"].ToString(), insertObj["Kod4"].ToString(),
                    insertObj["TipKod"].ToString(), insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(), insertObj["SatisFiyat1BirimInt"].ToString(),
                    insertObj["DovizSatisFiyat1"].ToString().Replace(",", "."), insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
                    );
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = string.Join(", ", fiyatID.ToArray());
                    sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat] where ID in({1})", vUser.SirketKodu, result);
                    var OnayFiyatList = db.Database.SqlQuery<Fiyat>(sql).ToList();
                    foreach (Fiyat rts in OnayFiyatList)
                    {
                        var date = DateTime.Now;
                        var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                        var sonuc = sqlexper.AcceptChanges();
                        db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET SPGMYOnay = 1, SPGMYOnaylayan='" + vUser.UserName + "', SPGMYOnayTarih='{2}'  where ID = '{1}'", vUser.SirketKodu, rts.ID, shortDate));
                    }
                }

                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Onay_Grup_SPGMY(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Writing) == false) return null;

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);

            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    var sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod <> 'PARKE' AND S.GrupKod = '{1}' AND F.FiyatListNum = '{2}'
                    AND S.TipKod = '{3}' AND S.Kod2 = '{4}' AND S.Kod3 = '{5}' AND S.Kod1 ='{6}'
                    AND S.Kod8 ='{7}'
                    AND F.SatisFiyat1 = {8} AND F.SatisFiyat1Birim = '{9}' AND F.SatisFiyat1BirimInt = {10}
                    AND F.DovizSatisFiyat1 = {11} AND F.DovizSF1Birim = '{12}' AND F.DovizSF1BirimInt = {13}
                    AND F.DovizCinsi = '{14}' AND F.Onay = 1 AND F.SPGMYOnay = 0 AND  F.GMOnay = 0 ", vUser.SirketKodu, insertObj["GrupKod"].ToString(),
                    insertObj["FiyatListNum"].ToString(), insertObj["Kalite"].ToString(),
                    insertObj["En"].ToString(), insertObj["Boy"].ToString(), insertObj["Kalinlik"].ToString(), insertObj["Yuzey"].ToString(),
                    insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(),
                    insertObj["SatisFiyat1BirimInt"].ToInt32(), insertObj["DovizSatisFiyat1"].ToString().Replace(",", "."),
                    insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
                    );
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = string.Join(", ", fiyatID.ToArray());
                    sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat] where ID in({1})", vUser.SirketKodu, result);
                    var OnayFiyatList = db.Database.SqlQuery<Fiyat>(sql).ToList();
                    foreach (Fiyat rts in OnayFiyatList)
                    {
                        var date = DateTime.Now;
                        var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");

                        var sonuc = sqlexper.AcceptChanges();
                        db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET SPGMYOnay = 1, SPGMYOnaylayan='" + vUser.UserName + "', SPGMYOnayTarih='{2}'  where ID = '{1}'", vUser.SirketKodu, rts.ID, shortDate));
                    }
                }

                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Red_SPGMY(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Writing) == false) return null;

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);

            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            try
            {
                foreach (JObject insertObj in parameters)
                {
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat]  where ID = '{1}'", vUser.SirketKodu, insertObj["ID"].ToString()));
                }

                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Red_Koleksiyon_SPGMY(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Writing) == false) return null;

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);

            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            try
            {
                foreach (JObject insertObj in parameters)
                {
                    var sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod = 'PARKE' AND F.FiyatListNum = '{1}'
                    AND S.Kod4 = '{2}' AND S.TipKod = '{3}' AND F.SatisFiyat1 = {4}
                    AND F.SatisFiyat1Birim = '{5}' AND F.SatisFiyat1BirimInt = {6}
                    AND F.DovizSatisFiyat1 = {7} AND F.DovizSF1Birim = '{8}' AND F.DovizSF1BirimInt = {9}
                    AND F.DovizCinsi = '{10}' AND F.Onay = 0 AND F.GMOnay = 0 ", vUser.SirketKodu, insertObj["FiyatListNum"].ToString(), insertObj["Kod4"].ToString(),
insertObj["TipKod"].ToString(), insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(), insertObj["SatisFiyat1BirimInt"].ToString(),
insertObj["DovizSatisFiyat1"].ToInt32(), insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
);
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = string.Join(", ", fiyatID.ToArray());

                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat]  where ID in({1})", vUser.SirketKodu, result));
                }

                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Red_Grup_SPGMY(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Writing) == false) return null;

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            try
            {
                foreach (JObject insertObj in parameters)
                {
                    var sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod <> 'PARKE' AND S.GrupKod = '{1}' AND F.FiyatListNum = '{2}'
                    AND S.TipKod = '{3}' AND S.Kod2 = '{4}' AND S.Kod3 = '{5}' AND S.Kod1 ='{6}'
                    AND S.Kod8 ='{7}'
                    AND F.SatisFiyat1 = {8} AND F.SatisFiyat1Birim = '{9}' AND F.SatisFiyat1BirimInt = {10}
                    AND F.DovizSatisFiyat1 = {11} AND F.DovizSF1Birim = '{12}' AND F.DovizSF1BirimInt = {13}
                    AND F.DovizCinsi = '{14}' AND F.Onay = 1 AND F.GMOnay = 0 AND F.SPGMYOnay = 0 ", vUser.SirketKodu, insertObj["GrupKod"].ToString(),
                    insertObj["FiyatListNum"].ToString(), insertObj["Kalite"].ToString(),
                    insertObj["En"].ToString(), insertObj["Boy"].ToString(), insertObj["Kalinlik"].ToString(), insertObj["Yuzey"].ToString(),
                    insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(),
                    insertObj["SatisFiyat1BirimInt"].ToInt32(), insertObj["DovizSatisFiyat1"].ToString().Replace(",", "."),
                    insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
                    );
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = string.Join(", ", fiyatID.ToArray());

                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat]  where ID in({1})", vUser.SirketKodu, result));
                }

                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        #endregion SPGMY

        #region SM

        public ActionResult SM()
        {
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult SM_List()
        {
            ViewBag.username = vUser.UserName;
            return PartialView();
        }

        public string SMOnayCek()
        {
            var RT = db.Database.SqlQuery<FiyatOnayGMSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayList]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public PartialViewResult SM_List_Koleksiyon()
        {
            return PartialView();
        }

        public string SMOnayCekSMKoleksiyon()
        {
            var RT = db.Database.SqlQuery<FiyatKoleksiyonSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayList_Koleksiyon]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public PartialViewResult SM_List_Grup()
        {
            return PartialView();
        }

        public string SMOnayCekSMGrup()
        {
            var RT = db.Database.SqlQuery<FiyatGrupSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayList_GrupEbatYuzey]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public JsonResult Onay_SM(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Writing) == false) return null;

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);

            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    var date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET Onay = 1, Onaylayan='" + vUser.UserName + "', SMOnayTarih='{2}'  where ID = '{1}'", vUser.SirketKodu, insertObj["ID"].ToString(), shortDate));
                }
                // return "OK";

                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";
            }
            catch (Exception)
            {
                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Onay_Koleksiyon_SM(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Writing) == false) return null;

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);

            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            try
            {
                foreach (JObject insertObj in parameters)
                {
                    var sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod = 'PARKE' AND F.FiyatListNum = '{1}'
                    AND S.Kod4 = '{2}' AND S.TipKod = '{3}' AND F.SatisFiyat1 = {4}
                    AND F.SatisFiyat1Birim = '{5}' AND F.SatisFiyat1BirimInt = {6}
                    AND F.DovizSatisFiyat1 = {7} AND F.DovizSF1Birim = '{8}' AND F.DovizSF1BirimInt = {9}
                    AND F.DovizCinsi = '{10}' AND F.Onay = 0 AND F.GMOnay = 0 ", vUser.SirketKodu, insertObj["FiyatListNum"].ToString(), insertObj["Kod4"].ToString(),
                    insertObj["TipKod"].ToString(), insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(), insertObj["SatisFiyat1BirimInt"].ToString(),
                    insertObj["DovizSatisFiyat1"].ToString().Replace(",", "."), insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
                    );
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = string.Join(", ", fiyatID.ToArray());
                    sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat] where ID in({1})", vUser.SirketKodu, result);
                    var OnayFiyatList = db.Database.SqlQuery<Fiyat>(sql).ToList();
                    foreach (Fiyat rts in OnayFiyatList)
                    {
                        var date = DateTime.Now;
                        var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                        var sonuc = sqlexper.AcceptChanges();
                        db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET Onay = 1, Onaylayan='" + vUser.UserName + "', SMOnayTarih='{2}'  where ID = '{1}'", vUser.SirketKodu, rts.ID, shortDate));
                    }
                }

                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Onay_Grup_SM(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Writing) == false) return null;

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);

            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    var sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod <> 'PARKE' AND S.GrupKod = '{1}' AND F.FiyatListNum = '{2}'
                    AND S.TipKod = '{3}' AND S.Kod2 = '{4}' AND S.Kod3 = '{5}' AND S.Kod1 ='{6}'
                    AND S.Kod8 ='{7}'
                    AND F.SatisFiyat1 = {8} AND F.SatisFiyat1Birim = '{9}' AND F.SatisFiyat1BirimInt = {10}
                    AND F.DovizSatisFiyat1 = {11} AND F.DovizSF1Birim = '{12}' AND F.DovizSF1BirimInt = {13}
                    AND F.DovizCinsi = '{14}' AND F.Onay = 0 AND F.GMOnay = 0 ", vUser.SirketKodu, insertObj["GrupKod"].ToString(),
                    insertObj["FiyatListNum"].ToString(), insertObj["Kalite"].ToString(),
                    insertObj["En"].ToString(), insertObj["Boy"].ToString(), insertObj["Kalinlik"].ToString(), insertObj["Yuzey"].ToString(),
                    insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(),
                    insertObj["SatisFiyat1BirimInt"].ToInt32(), insertObj["DovizSatisFiyat1"].ToString().Replace(",", "."),
                    insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
                    );
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = string.Join(", ", fiyatID.ToArray());
                    sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat] where ID in({1})", vUser.SirketKodu, result);
                    var OnayFiyatList = db.Database.SqlQuery<Fiyat>(sql).ToList();
                    foreach (Fiyat rts in OnayFiyatList)
                    {
                        var date = DateTime.Now;
                        var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");

                        var sonuc = sqlexper.AcceptChanges();
                        db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET Onay = 1, Onaylayan='" + vUser.UserName + "', SMOnayTarih='{2}'  where ID = '{1}'", vUser.SirketKodu, rts.ID, shortDate));
                    }
                }

                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Red_SM(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Writing) == false) return null;

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);

            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            try
            {
                foreach (JObject insertObj in parameters)
                {
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat]  where ID = '{1}'", vUser.SirketKodu, insertObj["ID"].ToString()));
                }

                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Red_Koleksiyon_SM(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Writing) == false) return null;

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);

            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            try
            {
                foreach (JObject insertObj in parameters)
                {
                    var sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod = 'PARKE' AND F.FiyatListNum = '{1}'
                    AND S.Kod4 = '{2}' AND S.TipKod = '{3}' AND F.SatisFiyat1 = {4}
                    AND F.SatisFiyat1Birim = '{5}' AND F.SatisFiyat1BirimInt = {6}
                    AND F.DovizSatisFiyat1 = {7} AND F.DovizSF1Birim = '{8}' AND F.DovizSF1BirimInt = {9}
                    AND F.DovizCinsi = '{10}' AND F.Onay = 0 AND F.GMOnay = 0 ", vUser.SirketKodu, insertObj["FiyatListNum"].ToString(), insertObj["Kod4"].ToString(),
insertObj["TipKod"].ToString(), insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(), insertObj["SatisFiyat1BirimInt"].ToString(),
insertObj["DovizSatisFiyat1"].ToInt32(), insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
);
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = string.Join(", ", fiyatID.ToArray());

                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat]  where ID in({1})", vUser.SirketKodu, result));
                }

                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Red_Grup_SM(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Writing) == false) return null;

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);

            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            try
            {
                foreach (JObject insertObj in parameters)
                {
                    var sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod <> 'PARKE' AND S.GrupKod = '{1}' AND F.FiyatListNum = '{2}'
                    AND S.TipKod = '{3}' AND S.Kod2 = '{4}' AND S.Kod3 = '{5}' AND S.Kod1 ='{6}'
                    AND S.Kod8 ='{7}'
                    AND F.SatisFiyat1 = {8} AND F.SatisFiyat1Birim = '{9}' AND F.SatisFiyat1BirimInt = {10}
                    AND F.DovizSatisFiyat1 = {11} AND F.DovizSF1Birim = '{12}' AND F.DovizSF1BirimInt = {13}
                    AND F.DovizCinsi = '{14}' AND F.Onay = 0 AND F.GMOnay = 0 ", vUser.SirketKodu, insertObj["GrupKod"].ToString(),
                    insertObj["FiyatListNum"].ToString(), insertObj["Kalite"].ToString(),
                    insertObj["En"].ToString(), insertObj["Boy"].ToString(), insertObj["Kalinlik"].ToString(), insertObj["Yuzey"].ToString(),
                    insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(),
                    insertObj["SatisFiyat1BirimInt"].ToInt32(), insertObj["DovizSatisFiyat1"].ToString().Replace(",", "."),
                    insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
                    );
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = string.Join(", ", fiyatID.ToArray());

                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat]  where ID in({1})", vUser.SirketKodu, result));
                }

                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        #endregion SM

        public ActionResult List()
        {
            if (CheckPerm(Perms.FiyatTanim, PermTypes.Reading) == false) return Redirect("/");
            var LNO = db.Database.SqlQuery<ListeNoSelect>(string.Format("[FINSAT6{0}].[wms].[FYTSelect2]", vUser.SirketKodu)).ToList();
            return View(LNO);
        }

        public string UrunGrupSelect()
        {
            var FUGS = db.Database.SqlQuery<FiyatUrunGrupSelect>(string.Format("[FINSAT6{0}].[wms].[STKSelect1]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(FUGS);
            return json;
        }

        public string HesapKoduSelect()
        {
            var FHKS = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[wms].[CHKSelect1]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(FHKS);
            return json;
        }

        public PartialViewResult ListPartial(string listeNo)
        {
            if (CheckPerm(Perms.FiyatTanim, PermTypes.Reading) == false) return null;
            ViewBag.ListeNo = listeNo;
            return PartialView();
        }

        public string ListesiSelect(string listeNo)
        {
            var FYTS = db.Database.SqlQuery<FiyatListSelect>(string.Format("[FINSAT6{0}].[wms].[FYTSelect1] @ListeNo='{1}'", vUser.SirketKodu, listeNo)).ToList();
            var json = new JavaScriptSerializer().Serialize(FYTS);
            return json;
        }

        public string ListesiBekleyen()
        {
            var FLB = db.Database.SqlQuery<BekleyenFiyatListesi>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayListTumBekleyenler]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(FLB);
            return json;
        }

        public string SatirEkle(string Data, string Satirlar)
        {
            if (CheckPerm(Perms.FiyatTanim, PermTypes.Writing) == false) return null;

            var parameters = JsonConvert.DeserializeObject<JObject>(Request["Data"]);
            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var FYTS = db.Database.SqlQuery<FiyatListSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatTanimVeOnay] @ListeNo='{1}'", vUser.SirketKodu, parameters["ListeNo"].ToString())).ToList();

                    if (parameters["Durum"].ToShort() == 1)
                    {
                        foreach (FiyatListSelect item in FYTS)
                        {
                            if (parameters["HesapKodu"] == null)
                            {
                                if (item.MalKodu == parameters["UrunKodu"].ToString() && item.MusteriKodu == "")
                                {
                                    return "MEVCUT";
                                }
                            }
                            else
                            {
                                if (item.MalKodu == parameters["UrunKodu"].ToString() && item.MusteriKodu == parameters["HesapKodu"].ToString())
                                {
                                    return "MEVCUT";
                                }
                            }
                        }

                        var fyt = new Fiyat()
                        {
                            FiyatListNum = parameters["ListeNo"].ToString(),
                            HesapKodu = parameters["HesapKodu"].ToString(),
                            MalKodu = parameters["UrunKodu"].ToString(),
                            Durum = parameters["Durum"].ToShort(),
                            ROW_ID = 0
                        };
                        if (Convert.ToDecimal(parameters["SatisFiyat"].ToString()) > 0)
                        {
                            fyt.SatisFiyat1 = Convert.ToDecimal(parameters["SatisFiyat"].ToString());
                            fyt.SatisFiyat1Birim = parameters["Birim"].ToString();
                            fyt.SatisFiyat1BirimInt = parameters["BirimValue"].ToInt32();
                            fyt.DovizSF1Birim = "";
                            fyt.DovizCinsi = "";
                            fyt.DovizSF1BirimInt = -1;
                        }
                        else
                        {
                            fyt.DovizSatisFiyat1 = Convert.ToDecimal(parameters["DovizSatisFiyat"].ToString());
                            fyt.DovizSF1Birim = parameters["DovizBirim"].ToString();
                            fyt.DovizSF1BirimInt = parameters["DovizBirimValue"].ToInt32();
                            fyt.DovizCinsi = parameters["DovizCinsi"].ToString();
                            fyt.SatisFiyat1Birim = "";
                            fyt.SatisFiyat1BirimInt = -1;
                        }

                        if (fyt.MalKodu == "M60101000080" || fyt.MalKodu == "M60101000081")
                        {
                            fyt.Onay = true;
                            fyt.Onaylayan = "";
                            fyt.SPGMYOnay = true;
                            fyt.SPGMYOnaylayan = "";
                            fyt.GMOnay = false;
                            fyt.GMOnaylayan = "";
                        }
                        /// Fiyatlar İçin KağıtFiltre Kontrolü
                        else if (fyt.MalKodu.StartsWith("2800") || parameters["GrupKod"].ToString() == "FKAĞIT" || fyt.MalKodu == "M001001000017051" || fyt.MalKodu == "M001001000022051") ///2800% ile başlayan ve Navlun ile Sigorta
                        {
                            ///Filtre kağıt önce SM sonra da GMye düşecek.
                            fyt.Onay = false;
                            fyt.Onaylayan = "OZ";  ///SMden Sadece Özgür Beye düşmesi için yapıldı.
                            fyt.SPGMYOnay = true;  ///SPGMY düşmemesi için direk true diyorum
                            fyt.SPGMYOnaylayan = "";
                            fyt.GMOnay = false;
                            fyt.GMOnaylayan = "";
                        }
                        else
                        {
                            fyt.Onay = false;
                            fyt.Onaylayan = "";
                            fyt.SPGMYOnay = false;
                            fyt.SPGMYOnaylayan = "";
                            fyt.GMOnay = false;
                            fyt.GMOnaylayan = "";
                        }

                        sqlexper.Insert(fyt);
                        var sonuc = sqlexper.AcceptChanges();
                        if (sonuc.Status == false) return "NO";
                        else return "OK";
                    }
                    else
                    {
                        var satirlar = JsonConvert.DeserializeObject<JArray>(Request["Satirlar"]);
                        foreach (JObject satir in satirlar)
                        {
                            var fyt = new Fiyat()
                            {
                                FiyatListNum = parameters["ListeNo"].ToString(),
                                HesapKodu = satir["MusteriKodu"].ToString(),
                                MalKodu = satir["MalKodu"].ToString(),
                                SatisFiyat1 = satir["SatisFiyat1"].ToDecimal(),
                                SatisFiyat1Birim = satir["SF1Birim"] == null ? "" : satir["SF1Birim"].ToString()
                            };
                            if (satir["SatisFiyat1"].ToDecimal() > 0)
                            {
                                if (satir["SF1Birim"].ToString().Trim() == satir["Birim1"].ToString().Trim())
                                    fyt.SatisFiyat1BirimInt = 1;
                                else if (satir["SF1Birim"].ToString().Trim() == satir["Birim2"].ToString().Trim())
                                    fyt.SatisFiyat1BirimInt = 2;
                                else if (satir["SF1Birim"].ToString().Trim() == satir["Birim3"].ToString().Trim())
                                    fyt.SatisFiyat1BirimInt = 3;
                                else
                                    fyt.SatisFiyat1BirimInt = -1;
                            }
                            else
                                fyt.SatisFiyat1BirimInt = -1;

                            fyt.DovizSatisFiyat1 = satir["DvzSatisFiyat1"].ToDecimal();
                            fyt.DovizSF1Birim = satir["DovizSF1Birim"] == null ? "" : satir["DovizSF1Birim"].ToString();

                            if (satir["DvzSatisFiyat1"].ToDecimal() > 0)
                            {
                                if (satir["DovizSF1Birim"].ToString().Trim() == satir["Birim1"].ToString().Trim())
                                    fyt.DovizSF1BirimInt = 1;
                                else if (satir["DovizSF1Birim"].ToString().Trim() == satir["Birim2"].ToString().Trim())
                                    fyt.DovizSF1BirimInt = 2;
                                else if (satir["DovizSF1Birim"].ToString().Trim() == satir["Birim3"].ToString().Trim())
                                    fyt.DovizSF1BirimInt = 3;
                                else
                                    fyt.DovizSF1BirimInt = -1;
                            }
                            else
                                fyt.DovizSF1BirimInt = -1;

                            fyt.DovizCinsi = satir["SF1DovizCinsi"] == null ? "" : satir["SF1DovizCinsi"].ToString();
                            fyt.Durum = parameters["Durum"].ToShort();
                            fyt.ROW_ID = satir["ROW_ID"].ToInt32();
                            if (fyt.MalKodu == "M60101000080" || fyt.MalKodu == "M60101000081")
                            {
                                fyt.Onay = true;
                                fyt.Onaylayan = "";
                                fyt.SPGMYOnay = true;
                                fyt.SPGMYOnaylayan = "";
                                fyt.GMOnay = false;
                                fyt.GMOnaylayan = "";
                            }
                            /// Fiyatlar İçin KağıtFiltre Kontrolü
                            else if (fyt.MalKodu.StartsWith("2800") || satir["GrupKod"].ToString() == "FKAĞIT" || fyt.MalKodu == "M001001000017051" || fyt.MalKodu == "M001001000022051") ///2800% ile başlayan ve Navlun ile Sigorta
                            {
                                ///Filtre kağıt önce SM sonra da GMye düşecek.
                                fyt.Onay = false;
                                fyt.Onaylayan = "OZ";
                                fyt.SPGMYOnay = true;  ///SPGMY düşmemesi için direk true diyorum
                                fyt.SPGMYOnaylayan = "";
                                fyt.GMOnay = false;
                                fyt.GMOnaylayan = "";
                            }
                            else
                            {
                                fyt.Onay = false;
                                fyt.Onaylayan = "";
                                fyt.SPGMYOnay = false;
                                fyt.SPGMYOnaylayan = "";
                                fyt.GMOnay = false;
                                fyt.GMOnaylayan = "";
                            }

                            sqlexper.Insert(fyt);
                            var sonuc = sqlexper.AcceptChanges();
                            if (sonuc.Status == false) return "NO";
                        }
                    }

                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    return "OK";
                }
                catch (Exception)
                {
                    sqlexper.RollBack();
                    return "NO";
                }
            }
        }
    }
}