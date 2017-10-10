﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    /// <summary>
    /// enumdan combobox yapmak için lazım
    /// </summary>
    public static class EnumHelperExtension
    {
        // Get the value of the description attribute if the   
        // enum has one, otherwise use the value.  
        public static string GetDescription<TEnum>(this TEnum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            if (fi != null)
            {
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
            }

            return value.ToString();
        }
        /// <summary>
        /// Build a select list for an enum
        /// </summary>
        public static SelectList SelectListFor<T>() where T : struct
        {
            Type t = typeof(T);
            return !t.IsEnum ? null
                             : new SelectList(BuildSelectListItems(t), "Value", "Text");
        }
        /// <summary>
        /// Build a select list for an enum with a particular value selected 
        /// </summary>
        public static SelectList SelectListFor<T>(T selected) where T : struct
        {
            Type t = typeof(T);
            return !t.IsEnum ? null
                             : new SelectList(BuildSelectListItems(t), "Text", "Value", selected.ToString());
        }
        private static IEnumerable<SelectListItem> BuildSelectListItems(Type t)
        {
            return Enum.GetValues(t)
                       .Cast<Enum>()
                       .Select(e => new SelectListItem { Value = e.ToString(), Text = e.GetDescription() });
        }
    }
}