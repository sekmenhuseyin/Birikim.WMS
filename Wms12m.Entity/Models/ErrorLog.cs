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
    
    public partial class ErrorLog
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public System.DateTime DateTime { get; set; }
        public string Machine { get; set; }
        public string IpAddress { get; set; }
        public string UserName { get; set; }
    }
}