using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m
{

    /// <summary>
    /// resim adını veriyorsun, eğer resim yoksa default resmi yüklüyor
    /// </summary>
    public static class HtmlHelperExtension
    {
        private static string defaultImage = "/Content/Uploads/0.jpg";
        private static string uploadsDirectory = "/Content/Uploads/";

        /// <summary>
        /// git serverdan son commit tarihi getirir
        /// </summary>
        public static DateTime GetCommitDate(this string GitGuid, string GitServerAddress, string fullname)
        {
            if (GitServerAddress == "" || GitServerAddress == null || GitGuid == "" || GitGuid == null || fullname == "" || fullname == null) return new DateTime();
            try
            {
                using (WebClient wc = new WebClient())
                {
                    var json = wc.DownloadString(GitServerAddress + "Repository/CommitFrom/" + GitGuid + "?user=" + fullname);
                    var list = JsonConvert.DeserializeObject<ForJson>(json);
                    return list.Date;
                }
            }
            catch (Exception)
            {
                return new DateTime();
            }
        }

        /// <summary>
        /// ImageAddressOrDefault
        /// </summary>
        public static string ImageAddressOrDefault(string filename)
        {
            var imagePath = uploadsDirectory + filename + ".jpg";
            var imageSrc = File.Exists(HttpContext.Current.Server.MapPath(imagePath)) ? filename : "0";
            return imageSrc;
        }

        /// <summary>
        /// ImageAddressOrDefault
        /// </summary>
        public static string ImageAddressOrDefault(this HtmlHelper helper, string filename)
        {
            var imagePath = uploadsDirectory + filename + ".jpg";
            var imageSrc = File.Exists(HttpContext.Current.Server.MapPath(imagePath)) ? filename : "0";
            return imageSrc;
        }

        /// <summary>
        /// stackoverflow.com/questions/19931698/how-to-display-a-default-image-in-case-the-source-does-not-exists
        /// eğer istenen resim yoksa varsayılan resim gösterilecek
        /// </summary>
        public static string ImageOrDefault(this HtmlHelper helper, string filename)
        {
            var imagePath = uploadsDirectory + filename + ".jpg";
            var imageSrc = File.Exists(HttpContext.Current.Server.MapPath(imagePath)) ? imagePath : defaultImage;
            return imageSrc;
        }

        /// <summary>
        /// PutUsersOffline
        /// </summary>
        public static void PutUsersOffline()
        {
            using (var db = new WMSEntities())
            {
                foreach (var connection in db.Connections)
                    connection.IsOnline = false;
                db.SaveChanges();
            }
        }
    }
}