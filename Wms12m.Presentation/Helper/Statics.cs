﻿using System.IO;
using System.Web;
using Wms12m.Entity.Models;

namespace Wms12m
{
    public class Statics
    {
        private static string uploadsDirectory = "/Content/Uploads/";
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
        /// <summary>
        /// ImageAddressOrDefault
        /// </summary>
        public static string ImageAddressOrDefault(string filename)
        {
            var imagePath = uploadsDirectory + filename + ".jpg";
            var imageSrc = File.Exists(HttpContext.Current.Server.MapPath(imagePath)) ? filename : "0";
            return imageSrc;
        }
    }
}