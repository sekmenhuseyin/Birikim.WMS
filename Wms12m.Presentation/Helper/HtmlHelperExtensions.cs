using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Wms12m
{
    public static class HtmlHelperExtensions
    {
        private static string defaultImage = "/Uploads/0.jpg";
        private static string uploadsDirectory = "/Uploads/";
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
    }
}