//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wms12m.Entity.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Setting
    {
        public int ID { get; set; }
        public string SiteName { get; set; }
        public string LoginLogo { get; set; }
        public string TopLogo { get; set; }
        public bool AllowNewUser { get; set; }
        public bool AllowForgotPass { get; set; }
        public bool KabloSiparisMySql { get; set; }
        public string SmtpEmail { get; set; }
        public string SmtpPass { get; set; }
        public Nullable<int> SmtpPort { get; set; }
        public string SmtpHost { get; set; }
        public bool SmtpSSL { get; set; }
        public string Version { get; set; }
        public bool Aktif { get; set; }
    }
}
