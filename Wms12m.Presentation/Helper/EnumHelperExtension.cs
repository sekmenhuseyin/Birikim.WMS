using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace Wms12m
{
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
            var t = typeof(T);
            return !t.IsEnum ? null
                             : new SelectList(BuildSelectListItems(t), "Value", "Text");
        }

        /// <summary>
        /// Build a select list for an enum with a particular value selected
        /// </summary>
        public static SelectList SelectListFor<T>(T selected) where T : struct
        {
            var t = typeof(T);
            return !t.IsEnum ? null
                             : new SelectList(BuildSelectListItems(t), "Text", "Value", selected.ToInt32());
        }

        private static IEnumerable<SelectListItem> BuildSelectListItems(Type t)
        {
            return Enum.GetValues(t)
                       .Cast<Enum>()
                       .Select(e => new SelectListItem { Value = e.ToString(), Text = e.ToInt32().ToString() });
        }
    }
}