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
        public bool homeDepo { get; set; }
        public bool homeUser { get; set; }
        public bool homeTask { get; set; }
        public bool OnayStok { get; set; }
        public bool OnayTeminat { get; set; }
        public bool OnaySiparis { get; set; }
        public bool OnaySozlesme { get; set; }
        public bool OnayRisk { get; set; }
        public bool OnayFiyat { get; set; }
        public bool OnayCek { get; set; }
        public bool OnayTekno { get; set; }
        public bool CrmOzet { get; set; }
        public bool SevkiyatVarmi { get; set; }
        public short MaliYil { get; set; }
        public short MaliYil1 { get; set; }
        public short MaliYil2 { get; set; }
        public string MaliYilDb { get; set; }
        public string MaliYilDb1 { get; set; }
        public string MaliYilDb2 { get; set; }
        public string Version { get; set; }
        public bool Aktif { get; set; }
        public bool SiparisOnayParametre { get; set; }
    }
}
