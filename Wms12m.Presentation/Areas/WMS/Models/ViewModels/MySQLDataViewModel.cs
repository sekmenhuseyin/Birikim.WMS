using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wms12m.Business;
using Wms12m.Entity.Models;
using Wms12m.Entity.Mysql;


namespace Wms12m.Presentation.Areas.WMS.Models.ViewModels
{
    public class MySQLDataViewModel
    {
       
        public int MySQLID { get; set; }
        public int SID { get; set; }
        public string Marka { get; set; }
        public string Cins { get; set; }
        public string Kesit { get; set; }
        public string Makara { get; set; }
        public string MakaraNo { get; set; }
        public string MalKodu { get; set; }
        public string Raf { get; set; }
        public string Bolum { get; set; }
        public string Kat { get; set; }
        public decimal Miktar { get; set; }
        public string DepoID { get; set; }


        /// <summary>
        /// Mysql stok tablosunda bulunan kabloların listesini getirir
        /// </summary>
        /// <returns>Kablo Listesi</returns>
        public static IEnumerable<MySQLDataViewModel> GetKabloMySQLViewModelList(RootController rt)
        {
            List<MySQLDataViewModel> returnList = new List<MySQLDataViewModel>();
            


            using (var db = new KabloEntities())
            {

                //var data = from s in db.stoks
                //           join h in db.harekets on s equals h.id into hs
                //           from h in hs.DefaultIfEmpty()



                  //MakaraNo Olmayanlar ve MakaraNo eşleşip miktarı eşit olmayanları stoklar list'ine atar
                List<string> yer = rt.db.Yers.Select(x => x.MakaraNo).ToList();
                List<kblstok> stoklar = new List<kblstok>();

                
                foreach (var item in db.kblstoks)//mysql stoklar üzerinde dön
                    if (!yer.Contains(item.makarano))//maraka no yoksa listeye ekle
                        stoklar.Add(item);
                    else//MakaraNo eşleşip miktarı eşit olmayanları  
                        foreach (var itemYer in rt.db.Yers.Where(x => x.MakaraNo.Contains(item.makarano)))
                            if (itemYer.Miktar != item.miktar)
                                stoklar.Add(item);



                foreach (var item in stoklar)
                    returnList.Add(
                        new MySQLDataViewModel()
                        {
                            MySQLID = item.id,
                            Marka = item.marka,
                            Cins = item.cins,
                            Kesit = item.kesit,
                            Makara = item.makara,
                            MakaraNo = item.makarano,
                            DepoID = item.depo,
                            Miktar = item.miktar.Value
                        });

            }

            return returnList;
        }


    }
}