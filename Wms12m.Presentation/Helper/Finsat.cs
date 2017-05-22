using OnikimCore.GunesCore;
using System.Collections.Generic;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m
{
    public class Finsat
    {
        private string ConStr { get; set; }
        private string SirketKodu { get; set; }
        /// <summary>
        /// yeni finsat
        /// </summary>
        public Finsat(string conStr, string sirketKodu)
        {
            ConStr = conStr;
            SirketKodu = sirketKodu;
        }
        /// <summary>
        /// evrak no oluştur
        /// </summary>
        public string EvrakNo(int EvrakSeriNo)
        {
            DevHelper.Ayarlar.SetConStr(ConStr);
            DevHelper.Ayarlar.SirketKodu = SirketKodu;
            Genel_Islemler GI = new Genel_Islemler(SirketKodu);
            return GI.EvrakNo_Getir(EvrakSeriNo);
        }
        /// <summary>
        /// sayım fişi oluştur
        /// </summary>
        public Result SayımVeFarkFişi(List<STI> stiList, int EvrakSeriNo, bool EvrakNoArttır, string username)
        {
            //evrak no
            string evrakno = EvrakNo(EvrakSeriNo);
            if (evrakno == null || evrakno == "")
                return new Result(false, "Evrak Seri hatası.");
            //definitions
            Functions fn = new Functions();
            int tarih = fn.ToOADate();
            int saat = fn.ToOATime();
            SqlExper sqlexper = new SqlExper(ConStr, SirketKodu);
            //sql exper
            foreach (var item in stiList)
            {
                item.EvrakNo = evrakno;
                item.Kaydeden = username;
                item.KayitTarih = tarih;
                item.KayitSaat = saat;
                item.KayitKaynak = 153;
                item.KayitSurum = "9.01.028";
                item.Degistiren = username;
                item.DegisTarih = tarih;
                item.DegisSaat = saat;
                item.DegisKaynak = 153;
                item.DegisSurum = "9.01.028";
                item.CheckSum = 12;
                sqlexper.Insert(item);
            }
            var sonuc = sqlexper.AcceptChanges();
            if (sonuc.Status == true)
            {
                sonuc.Message = evrakno;
                //evrak no arttır
                if (EvrakNoArttır == true)
                {
                    string seri = string.Empty;
                    string noStr = string.Empty;
                    if (evrakno.Length == 8)
                    {
                        seri = evrakno.Substring(0, 2);
                        noStr = evrakno.Substring(2, 6);
                    }
                    else if (evrakno.Length == 7)
                    {
                        seri = evrakno.Substring(0, 1);
                        noStr = evrakno.Substring(1, 6);
                    }
                    int no = noStr.ToInt32();
                    string evrakNoArti = Helper.EvrakNoOlustur(8, seri, no);
                    sqlexper.Komut("UPDATE FINSAT6{0}.FINSAT6{0}.INI SET MyValue={{0}} WHERE MySection = 1 AND MyEntry = {{1}}", evrakNoArti, EvrakSeriNo);
                    sqlexper.AcceptChanges();
                }
            }
            return sonuc;
        }
    }
}