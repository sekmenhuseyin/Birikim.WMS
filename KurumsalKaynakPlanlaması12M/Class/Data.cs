using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KurumsalKaynakPlanlaması12M
{
    static class Data
    {       
        public static int SaatForDB(DateTime Saatim)
        {
            int saat = 0;
            if (Saatim.Hour >= 1)
            {
                saat += Saatim.Hour * 60 * 60;
            }
            if (Saatim.Minute >= 1)
            {
                saat += Saatim.Minute * 60;
            }
            if (Saatim.Second > 0)
            {
                saat += Saatim.Second;
            }
            return saat;
        }

        public static void DefaultValueSet(object tablo)
        {
            foreach (var pi in tablo.GetType().GetProperties())
            {
                DbAlanAttribute[] attrs = ((DbAlanAttribute[])pi.GetCustomAttributes(typeof(DbAlanAttribute), false));
                if (attrs == null || attrs.Length == 0)
                    continue;

                //if (pi.PropertyType == typeof(string))
                //    pi.SetValue(tablo, "", null);
                //else if (pi.PropertyType == typeof(int) || pi.PropertyType == typeof(decimal) || pi.PropertyType == typeof(float) || pi.PropertyType == typeof(double) || pi.PropertyType == typeof(short))
                //    pi.SetValue(tablo, 0, null);

                if (pi.PropertyType == typeof(string))
                    pi.SetValue(tablo, "", null);
                else if (pi.PropertyType == typeof(int))
                    pi.SetValue(tablo, 0, null);
                else if (pi.PropertyType == typeof(decimal))
                    pi.SetValue(tablo, 0.0M, null);
                else if (pi.PropertyType == typeof(float))
                    pi.SetValue(tablo, 0.0F, null);
                else if (pi.PropertyType == typeof(double))
                    pi.SetValue(tablo, 0.0D, null);
                else if (pi.PropertyType == typeof(short))
                    pi.SetValue(tablo, (short)0, null);
            }
        }

        public static void DefaultValueSetSTK(KKP_STK stk)
        {
            DefaultValueSet(stk);
        }

        public static void DefatulValueSetDST(KKP_DST dst)
        {
            DefaultValueSet(dst);
            dst.SonMlySekli = -1;
            dst.BakGostSekli = -1;

            int tarih = Convert.ToInt32(DateTime.Today.ToOADate());
            int saat = SaatForDB(DateTime.Now);

            dst.KayitTarih = tarih;
            dst.KayitSaat = saat;
            dst.KayitSurum = KKP.LinkSurum;

            dst.DegisTarih = tarih;
            dst.DegisSaat = saat;
            dst.DegisSurum = KKP.LinkSurum;
            

            dst.CheckSum = 12;
        }

        public static void DefaultValueSetSPI(KKP_SPI spi, KKPSiparisTip sipTip)
        {
            DefaultValueSet(spi);

            spi.Tarih = Convert.ToInt32(DateTime.Today.ToOADate());
            spi.IslemTip = (short)KKPIslemTipSPI.İçPiyasa;           
            spi.EvrakTarih = spi.Tarih;
            spi.VadeTarih = spi.Tarih;
            spi.TahTeslimTarih = spi.Tarih;
            spi.SonTeslimTarih = spi.Tarih;
            spi.IskOran1Net = -1;
            spi.IskOran2Net = -1;
            spi.IskOran3Net = -1;
            spi.IskOran4Net = -1;
            spi.IskOran5Net = -1;
            spi.KlmTutarIskNet = -1;
            spi.Katsayi = 1;
            spi.KayitTarih = spi.Tarih;
            spi.KayitSaat = SaatForDB(DateTime.Now);
            spi.KayitSurum = KKP.LinkSurum;
            spi.DegisTarih = spi.Tarih;
            spi.DegisSaat = spi.KayitSaat;
            spi.DegisSurum = KKP.LinkSurum;
            spi.CheckSum = 12;

            

            if (sipTip == KKPSiparisTip.SatisSiparisi)
            {
                spi.IslemTur = 1;
                spi.KynkEvrakTip = 62;
                spi.AnaEvrakTip = 62;
                spi.KayitKaynak = 107;
                spi.DegisKaynak = 107;
            }
            else
            {
                spi.IslemTur = 0;
                spi.KynkEvrakTip = 63;
                spi.AnaEvrakTip = 63;
                spi.KayitKaynak = 108;
                spi.DegisKaynak = 108;
            }
        }

        public static void DefaultValueSetSTI(KKP_STI sti, KKPKynkEvrakTip tip)
        {
            DefaultValueSet(sti);

            sti.KynkEvrakTip = (short)tip;
            sti.AnaEvrakTip = sti.KynkEvrakTip;

            sti.Tarih = Convert.ToInt32(DateTime.Today.ToOADate());
            sti.IrsFat = 0; // 0 İrsaliye; 1 Fatura; 2 Faturalanan İrsaliye; 3 İade Edilen Fatura; 4: İade Edilen İrsaliye
            sti.VadeTarih = sti.Tarih;
            sti.EvrakTarih = sti.Tarih;
            
            sti.Kredi_Donem_VadeTarih = sti.Tarih;

            sti.IslemTip = (short)KKPIslemTipSPI.İçPiyasa;
            sti.OtvDahilHaric = -1;

            sti.MlyYontem = -1;
            sti.MhsDurum = 1;
            sti.MlyMhs = 1;
            sti.MhsTabloNo = 1;


            sti.IskOran1Net = -1;
            sti.IskOran2Net = -1;
            sti.IskOran3Net = -1;
            sti.IskOran4Net = -1;
            sti.IskOran5Net = -1;
            sti.KlmTutarIskNet = -1;
            sti.Katsayi = 1;
            sti.ErekIFKEvrakTip = -1;
            sti.ErekIIFKEvrakTip = -1;
            sti.IrsFat2 = -1;
            //sti.KrediArindirmaSekli = -1;
            //sti.FinansmanGiderTuru = -1;
            sti.Duz_Yapilan_Donem = -1;
            sti.Duz_Yontemi = -1;
            sti.Duz_Mhs_Durumu = -1;
            sti.Duz_Mly_Yontemi = -1;
            sti.Duz_Mly_Mhs_Durumu = -1;

            sti.KayitTarih = sti.Tarih;
            sti.KayitSaat = SaatForDB(DateTime.Now);
            sti.KayitSurum = KKP.LinkSurum;
            sti.DegisTarih = sti.Tarih;
            sti.DegisSaat = sti.KayitSaat;
            sti.DegisSurum = KKP.LinkSurum;
            sti.CheckSum = 12;

            //sti.DvzKurCinsi = -1;
            sti.IthalatMDagDurum = -1;
            sti.EFatTip = -1;  //0 Onaylı Kağıt Fatura; 1 Temel eFatura; 2 Ticari eFatura; 3 e-Arşiv Faturası
            sti.EFatDurum = -1;  //0 Taslak; 1 Gönderildi; 2 Kabul Edildi; 3 Rededildi; 4 İade Edildi; 5 Arsivlendi; 6 GIB Teslim Aldi; 7 GIB Kabul Etmedi; 8 Aliciya Teslim Edildi; 9 Alici Teslim Almadi; 10 Beklemede
            sti.IthalatMDagYontem = -1;
            sti.OTVTevkifatVarYok = -1;

            sti.EArsivTeslimSekli = -1; //0 Kağıt; 1 Elektrik
            sti.EArsivFaturaTipi = -1; //0 Standart; 1 İnternet Üzerinden Satış
            sti.EArsivFaturaDurum = -1; //0 Taslak; 1 Gönderildi; 2 Yazdırıldı;

            
            if (tip == KKPKynkEvrakTip.Satışİrsaliyesi)
            {
                sti.IslemTur = 1;
                sti.SevkTarih = sti.Tarih;
                sti.KaynakIrsTarih = sti.Tarih;
                sti.KayitKaynak = (short)KKPKayitKaynak.SatisIrsaliye;
                sti.DegisKaynak = (short)KKPKayitKaynak.SatisIrsaliye;
            }
            else if (tip == KKPKynkEvrakTip.Alımİrsaliyesi)
            {
                sti.IslemTur = 0;
                sti.SevkTarih = sti.Tarih;
                sti.KaynakIrsTarih = sti.Tarih;
                sti.KayitKaynak = (short)KKPKayitKaynak.AlimIrsaliye;
                sti.DegisKaynak = (short)KKPKayitKaynak.AlimIrsaliye;
            }


        }

        public static void DefaultValueSetFTD(KKP_FTD ftd, KKPKynkEvrakTip kynkEvrakTip, FTDTable.SatirTip satirTip)
        {
            DefaultValueSet(ftd);

            ftd.KynkEvrakTip = (short)kynkEvrakTip;
            ftd.AnaEvrakTip = (short)kynkEvrakTip;

            ftd.SatirTip = (short)satirTip;
            ftd.Aciklama = FTDTable.GetSatirTipName(satirTip);


            ftd.Tarih = Convert.ToInt32(DateTime.Today.ToOADate());
            ftd.KayitTarih = ftd.Tarih;
            ftd.KayitSaat = SaatForDB(DateTime.Now);
            ftd.KayitSurum = KKP.LinkSurum;
            ftd.DegisTarih = ftd.Tarih;
            ftd.DegisSaat = ftd.KayitSaat;
            ftd.DegisSurum = KKP.LinkSurum;
            ftd.CheckSum = 12;
            ftd.FormBaBsTarih = ftd.Tarih;
            ftd.EFatDurum = -1;
            ftd.EFatSureDurum = -1;

            ftd.EArsivTeslimSekli = -1;
            ftd.EArsivFaturaTipi = -1;
            ftd.EArsivFaturaDurum = -1;

            if (kynkEvrakTip == KKPKynkEvrakTip.SatışSiparişi)
            {
                ftd.IslemTip = (short)FTDTable.IslemTip.SatışSiparişi;
                ftd.BA = 0;
                ftd.DvzKurCins = 1;
                ftd.KayitKaynak = (short)KKPKayitKaynak.SatisSiparisi;
                ftd.DegisKaynak = ftd.KayitKaynak;
            }
            else if (kynkEvrakTip == KKPKynkEvrakTip.AlımSiparişi)
            {
                ftd.IslemTip = (short)FTDTable.IslemTip.AlımSiparişi;
                ftd.BA = 1;
                ftd.DvzKurCins = 1;
                ftd.KayitKaynak = (short)KKPKayitKaynak.AlimSiparisi;
                ftd.DegisKaynak = ftd.KayitKaynak;
            }
            else if (kynkEvrakTip == KKPKynkEvrakTip.Alımİrsaliyesi)
            {
                ftd.IslemTip = (short)FTDTable.IslemTip.İrsaliye;
                ftd.BA = 1;
                ftd.DvzKurCins = 1;
                ftd.KayitKaynak = (short)KKPKayitKaynak.AlimIrsaliye;
                ftd.DegisKaynak = (short)KKPKayitKaynak.AlimIrsaliye;
            }
            else if (kynkEvrakTip == KKPKynkEvrakTip.Satışİrsaliyesi)
            {
                ftd.IslemTip = (short)FTDTable.IslemTip.İrsaliye;
                ftd.BA = 0;
                ftd.DvzKurCins = 1;
                ftd.KayitKaynak = (short)KKPKayitKaynak.SatisIrsaliye;
                ftd.DegisKaynak = (short)KKPKayitKaynak.SatisIrsaliye;
            }
        }

        public static void DefaultValueSetMFK(KKP_MFK mfk, KKPKynkEvrakTip kynkEvrakTip)
        {
            DefaultValueSet(mfk);
            mfk.KynkEvrakTip = (short)kynkEvrakTip;

            mfk.EvrakTarih = Convert.ToInt32(DateTime.Today.ToOADate());
            mfk.OnayIslemTip = -1;
            mfk.OnayStatus = -1;
            
            mfk.KayitTarih = mfk.EvrakTarih;
            mfk.KayitSaat = SaatForDB(DateTime.Now);
            mfk.KayitSurum = KKP.LinkSurum;
            
            mfk.DegisTarih = mfk.EvrakTarih;
            mfk.DegisSaat = mfk.KayitSaat;
            mfk.DegisSurum = KKP.LinkSurum;

            mfk.CheckSum = 12;


            // [COMBOITEM_NAME]  ITEMCBOID=72
            // 0 Fatura: 1 İade Faturası; 2 İrsaliye; 3 Alım Siparişi; 4  Satış Siparişi

            if (kynkEvrakTip == KKPKynkEvrakTip.SatışSiparişi)
            {
                mfk.IslemTip = 4;   
                mfk.KayitKaynak = (short)KKPKayitKaynak.SatisSiparisi;
                mfk.DegisKaynak = (short)KKPKayitKaynak.SatisSiparisi;
                
            }
            else if (kynkEvrakTip == KKPKynkEvrakTip.AlımSiparişi)
            {
                mfk.IslemTip = 3;
                mfk.KayitKaynak = (short)KKPKayitKaynak.AlimSiparisi;
                mfk.DegisKaynak = (short)KKPKayitKaynak.AlimSiparisi;
            }
            else if (kynkEvrakTip == KKPKynkEvrakTip.Alımİrsaliyesi)
            {
                mfk.IslemTip = 2;
                mfk.KayitKaynak = (short)KKPKayitKaynak.AlimIrsaliye;
                mfk.DegisKaynak = (short)KKPKayitKaynak.AlimIrsaliye; 
            }
            else if (kynkEvrakTip == KKPKynkEvrakTip.Satışİrsaliyesi)
            {
                mfk.IslemTip = 2;
                mfk.KayitKaynak = (short)KKPKayitKaynak.SatisIrsaliye;
                mfk.DegisKaynak = (short)KKPKayitKaynak.SatisIrsaliye; 
            }
        }

        public static void DefaultValueSetIRS(KKP_IRS irs, KKPKynkEvrakTip kynkEvrakTip)
        {
            DefaultValueSet(irs);

            irs.Tarih = Convert.ToInt32(DateTime.Today.ToOADate());
            irs.KynkEvrakTip = (short)kynkEvrakTip;
            irs.IslemTip = (short)KKPIslemTipSPI.İçPiyasa;


            irs.KayitTarih = irs.Tarih;
            irs.KayitSaat = SaatForDB(DateTime.Now);
            irs.KayitSurum = KKP.LinkSurum;
            irs.DegisTarih = irs.Tarih;
            irs.DegisSaat = irs.KayitSaat;
            irs.DegisSurum = KKP.LinkSurum;
            irs.CheckSum = 12;

            if (kynkEvrakTip == KKPKynkEvrakTip.Alımİrsaliyesi)
            {
                irs.IslemTur = 0;    ///Alım Siparişinden İrsaliye
                irs.IslemTur2 = 0;
                irs.KynkEvrakTip2 = 63;
                irs.IslemTip2 = (short)KKPIslemTipSPI.İçPiyasa;
                irs.KayitKaynak = (short)KKPKayitKaynak.AlimIrsaliye;
                irs.DegisKaynak = (short)KKPKayitKaynak.AlimIrsaliye;
            }
            else if (kynkEvrakTip == KKPKynkEvrakTip.Satışİrsaliyesi)
            {
                irs.IslemTur = 1;   ///Satış Siparişinden İrsaliye
                irs.IslemTur2 = 1;
                irs.KynkEvrakTip2 = 62;
                irs.IslemTip2 = (short)KKPIslemTipSPI.İçPiyasa;
                irs.KayitKaynak = (short)KKPKayitKaynak.SatisIrsaliye;
                irs.DegisKaynak = (short)KKPKayitKaynak.SatisIrsaliye;
            }
        }

        /// <summary>
        /// Insert sorgusu oluşturur
        /// </summary>
        /// <param name="tablo">Oluşturulan Tablo.</param>
        /// <param name="tabloAdi">Update Edilecek Tablo Adı. Örneğin; "STK"</param>
        /// <param name="sirketKodu">FINSAT6 Dan Sonra Gelecek Şirket Kodu. Örneğin; "01"</param>
        /// <returns></returns>
        public static KKPSorguSonuc GetSqlInsert(object tablo, string tabloAdi, string sirketKodu, bool identity_insertON)
        {
            DbTabloAttribute[] dbAttr = (DbTabloAttribute[])tablo.GetType().GetCustomAttributes(typeof(DbTabloAttribute), true);

            if (dbAttr.Length == 0)
            {
                throw new Exception(string.Format("Class DbAttribute Bilgisi içermiyor. ({0})", tablo.ToString()));
            }

            string dbname = string.Format("[{0}{1}]", dbAttr[0].DBAdi, sirketKodu);
            string tabloname = string.Format("[{0}{1}].[{2}]", dbAttr[0].SemaAdi, sirketKodu, dbAttr[0].TabloAdi);
            string fuldbName = string.Format("{0}.{1}", dbname, tabloname);

            StringBuilder sb = new StringBuilder();
            if(identity_insertON)
            {
                ///sb.AppendFormat("SET IDENTITY_INSERT [FINSAT6{0}].[{1}] ON", sirketKodu, tabloAdi);
                sb.AppendFormat("SET IDENTITY_INSERT {0} ON", fuldbName);
                sb.AppendLine();
            }

            //sb.AppendFormat("INSERT INTO [FINSAT6{0}].[{1}]", sirketKodu, tabloAdi);
            sb.AppendFormat("INSERT INTO {0}", fuldbName);
            sb.AppendLine();
            sb.AppendLine("(");

            //StringBuilder sb = new StringBuilder("INSERT INTO [FINSAT6");
            //sb.Append(sirketKodu); 
            //sb.Append("]."); 
            //sb.Append("[" + tabloAdi.ToUpper() + "] (");

            StringBuilder sb2 = new StringBuilder("VALUES (");

            PropertyInfo[] infos = tablo.GetType().GetProperties();
            List<SqlParameter> Params = new List<SqlParameter>();
            for (int i = 0; i < infos.Length; i++)
            {
                PropertyInfo info = infos[i];
                DbAlanAttribute[] attrs = ((DbAlanAttribute[])info.GetCustomAttributes(typeof(DbAlanAttribute), false));
                if (attrs == null || attrs.Length == 0)
                    continue;

                if (attrs[0].AlanAdi == "")
                    continue;

                #region Kontrol

                string Value = Convert.ToString(info.GetValue(tablo, null) ?? DBNull.Value);

                if (attrs[0].Primary == true)
                {
                    if (Value == "")
                    {
                        //throw new Exception(String.Format("{0} Tablosunda, {1} Alanı Boş Geçilemez..", tabloAdi, attrs[0].AlanAdi));
                    }
                }

                if (attrs[0].DbTye.ToString() == "VarChar" || attrs[0].DbTye.ToString() == "NVarChar" || attrs[0].DbTye.ToString() == "Char" || attrs[0].DbTye.ToString() == "NChar"
                    || attrs[0].DbTye.ToString() == "Binary" || attrs[0].DbTye.ToString() == "Time" || attrs[0].DbTye.ToString() == "VarBinary")
                {
                    if (Value.Length > attrs[0].MaxLength)
                    {
                        throw new Exception(String.Format("{0} Tablosunda, {1} Alanı {2} Karakterden Fazla Olmaz..", tabloAdi, attrs[0].AlanAdi, attrs[0].MaxLength));
                    }
                }
                #endregion

                
                if (attrs[0].Identtiy == false || identity_insertON)
                {
                    SqlParameter prm = new SqlParameter(attrs[0].AlanAdi, attrs[0].DbTye);
                    prm.Value = info.GetValue(tablo, null) ?? DBNull.Value;
                    Params.Add(prm);

                    sb.Append(attrs[0].AlanAdi);
                    sb.Append(", ");

                    sb2.Append("@");
                    sb2.Append(attrs[0].AlanAdi);
                    sb2.Append(", ");
                }
            }

            sb.Remove(sb.Length - 2, 2);
            sb2.Remove(sb2.Length - 2, 2);
            sb.Append(" )");
            sb2.Append(" )");
            sb.Append(sb2.ToString());
            sb.AppendLine();
            
            if (identity_insertON)
            {
                //sb.AppendFormat("SET IDENTITY_INSERT [FINSAT6{0}].[{1}] OFF", sirketKodu, tabloAdi);
                sb.AppendFormat("SET IDENTITY_INSERT {0} OFF", fuldbName);
                sb.AppendLine();
            }


            return new KKPSorguSonuc(tabloAdi, sb.ToString(), Params);
        }

        /// <summary>
        /// Update Cümlesi Oluşturulur.
        /// </summary>
        /// <param name="tablo">Oluşturulan Tablo.</param>
        /// <param name="tabloAdi">Update Edilecek Tablo Adı. Örneğin; "STK"</param>
        /// <param name="sirketKodu">FINSAT6 Dan Sonra Gelecek Şirket Kodu. Örneğin; "01"</param>
        /// <returns></returns>
        public static KKPSorguSonuc GetSqlUpdate(object tablo, string tabloAdi, string sirketKodu)
        {
            DbTabloAttribute[] dbAttr = (DbTabloAttribute[])tablo.GetType().GetCustomAttributes(typeof(DbTabloAttribute), true);

            if (dbAttr.Length == 0)
            {
                throw new Exception(string.Format("Class DbAttribute Bilgisi içermiyor. ({0})", tablo.ToString()));
            }

            string dbname = string.Format("[{0}{1}]", dbAttr[0].DBAdi, sirketKodu);
            string tabloname = string.Format("[{0}{1}].[{2}]", dbAttr[0].SemaAdi, sirketKodu, dbAttr[0].TabloAdi);
            string fuldbName = string.Format("{0}.{1}", dbname, tabloname);

            //StringBuilder sb = new StringBuilder("UPDATE [FINSAT6");
            StringBuilder sb = new StringBuilder("UPDATE ");
            sb.Append(fuldbName);
            sb.Append(" SET ");
            //sb.Append(sirketKodu); sb.Append("]."); sb.Append("[" + tabloAdi.ToUpper() + "] SET ");

            StringBuilder sb2 = new StringBuilder();
            sb2.Append(" WHERE ");

            FieldInfo changed = tablo.GetType().GetField("_ChangedList", BindingFlags.NonPublic | BindingFlags.Instance);
            if (changed == null)
                throw new Exception(string.Format("Tabloda _ChangedList field bulunamadı ({0})", tabloAdi));

            List<PropertyValue> changedList = (List<PropertyValue>)changed.GetValue(tablo);
            if (changedList.Count == 0) ///Güncelleme yapılmamış
            {
                return new KKPSorguSonuc(tabloAdi, "-", new List<SqlParameter>());
            }

            
            PropertyInfo[] infos = tablo.GetType().GetProperties();
            List<SqlParameter> Params = new List<SqlParameter>();
            for (int i = 0; i < infos.Length; i++)
            {
                PropertyInfo info = infos[i];
                DbAlanAttribute[] attrs = ((DbAlanAttribute[])info.GetCustomAttributes(typeof(DbAlanAttribute), false));
                if (attrs == null || attrs.Length == 0)
                    continue;

                if (attrs[0].AlanAdi == "")
                    continue;

                if (attrs[0].Primary == false) //PRİMARY KEY LER WHERE de Koşul olarak yazıldığı için kontrolün dışında olacak.
                    if (!changedList.Any(t => t.PropertiName == info.Name))
                        continue;

                #region Kontrol

                string Value = Convert.ToString(info.GetValue(tablo, null) ?? DBNull.Value);

                if (attrs[0].Primary == true)
                {
                    if (Value == "")
                    {
                        throw new Exception(String.Format("{0} Tablosunda, {1} Alanı Boş Geçilemez..", tabloAdi, attrs[0].AlanAdi));
                    }
                }


                if (attrs[0].DbTye.ToString() == "VarChar" || attrs[0].DbTye.ToString() == "NVarChar" || attrs[0].DbTye.ToString() == "Char" || attrs[0].DbTye.ToString() == "NChar"
                    || attrs[0].DbTye.ToString() == "Binary" || attrs[0].DbTye.ToString() == "Time" || attrs[0].DbTye.ToString() == "VarBinary")
                {

                    if (Value.Length > attrs[0].MaxLength)
                    {
                        throw new Exception(String.Format("{0} Tablosunda, {1} Alanı {2} Karakterden Fazla Olmaz..", tabloAdi, attrs[0].AlanAdi, attrs[0].MaxLength));

                    }

                }
                #endregion


                if (attrs[0].Identtiy == false)
                {
                    SqlParameter prm = new SqlParameter(attrs[0].AlanAdi, attrs[0].DbTye);
                    prm.Value = info.GetValue(tablo, null) ?? DBNull.Value;
                    Params.Add(prm);

                                       
                    if (attrs[0].Primary == false)
                    {
                        sb.Append(attrs[0].AlanAdi);
                        sb.Append(" = @");
                        sb.Append(attrs[0].AlanAdi);
                        sb.Append(", ");
                    }
                    else
                    {
                        //if (i != 0)
                        //    sb2.Append(" AND ");

                        sb2.Append(attrs[0].AlanAdi);
                        sb2.Append("=@");
                        sb2.Append(attrs[0].AlanAdi);
                        sb2.Append(" AND ");
                    }                    
                }
            }
            sb.Remove(sb.Length - 2, 2);
            sb2.Remove(sb2.Length - 4, 4);

            sb.Append(sb2.ToString());

            sb.AppendLine();

            return new KKPSorguSonuc(tabloAdi, sb.ToString(), Params, true);

        }


        /// <summary>
        /// Delete Cümlesi Oluşturulur.
        /// </summary>
        /// <param name="tablo">Oluşturulan Tablo.</param>
        /// <param name="tabloAdi">Update Edilecek Tablo Adı. Örneğin; "STK"</param>
        /// <param name="sirketKodu">FINSAT6 Dan Sonra Gelecek Şirket Kodu. Örneğin; "01"</param>        
        /// <returns></returns>
        public static KKPSorguSonuc GetSqlDelete(object tablo, string tabloAdi, string sirketKodu)
        {
            StringBuilder sb = new StringBuilder("DELETE [FINSAT6");
            sb.Append(sirketKodu); sb.Append("]."); sb.Append("[" + tabloAdi.ToUpper() + "] ");

            StringBuilder sb2 = new StringBuilder(" WHERE ");

            PropertyInfo[] infos = tablo.GetType().GetProperties();
            List<SqlParameter> Params = new List<SqlParameter>();
            for (int i = 0; i < infos.Length; i++)
            {
                PropertyInfo info = infos[i];
                DbAlanAttribute[] attrs = ((DbAlanAttribute[])info.GetCustomAttributes(typeof(DbAlanAttribute), false));
                if (attrs == null || attrs.Length == 0)
                    continue;

                if (attrs[0].AlanAdi == "")
                    continue;

                #region Kontrol

                string Value = Convert.ToString(info.GetValue(tablo, null) ?? DBNull.Value);

                if (attrs[0].Primary == true)
                {
                    if (Value == "")
                    {
                        throw new Exception(String.Format("{0} Tablosunda, {1} Alanı Boş Geçilemez..", tabloAdi, attrs[0].AlanAdi));
                    }
                }

                if (attrs[0].DbTye.ToString() == "VarChar" || attrs[0].DbTye.ToString() == "NVarChar" || attrs[0].DbTye.ToString() == "Char" || attrs[0].DbTye.ToString() == "NChar"
                    || attrs[0].DbTye.ToString() == "Binary" || attrs[0].DbTye.ToString() == "Time" || attrs[0].DbTye.ToString() == "VarBinary")
                {

                    if (Value.Length > attrs[0].MaxLength)
                    {
                        throw new Exception(String.Format("{0} Tablosunda, {1} Alanı {2} Karakterden Fazla Olmaz..", tabloAdi, attrs[0].AlanAdi, attrs[0].MaxLength));

                    }

                }
                #endregion

                if (attrs[0].Identtiy == false)
                {
                    SqlParameter prm = new SqlParameter(attrs[0].AlanAdi, attrs[0].DbTye);
                    prm.Value = info.GetValue(tablo, null) ?? DBNull.Value;
                    Params.Add(prm);

                    if (attrs[0].Primary != false)
                    {
                        if (i != 0)
                            sb2.Append(" AND ");

                        sb2.Append(attrs[0].AlanAdi);
                        sb2.Append("=@");
                        sb2.Append(attrs[0].AlanAdi);

                    }
                }
            }
            sb.Append(sb2.ToString());
            sb.AppendLine();

            return new KKPSorguSonuc(tabloAdi, sb.ToString(), Params);
        }



        public static void TabloDoldur(object tablo, SqlDataReader dr)
        {
            List<PropertyInfo> infos = tablo.GetType().GetProperties().ToList();
            
            for (int i = 0; i < dr.FieldCount; i++)
            {
                string fieldName = dr.GetName(i);
                PropertyInfo pi = infos.Find(t => t.Name == fieldName);
                if (pi == null)
                    continue;

                DbAlanAttribute[] attrs = ((DbAlanAttribute[])pi.GetCustomAttributes(typeof(DbAlanAttribute), false));
                if (attrs == null || attrs.Length == 0)
                    continue;

                pi.SetValue(tablo, dr.GetValue(i), null);
                    
            }            
        }
    }
}
