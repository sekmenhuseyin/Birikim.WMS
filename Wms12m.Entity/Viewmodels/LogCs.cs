using System;

namespace Wms12m.Entity
{
    public  class LogCs
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Machine { get; set; }
        public string IpAddress { get; set; }
        public string Description { get; set; }
        public DateTime? LogTime { get; set; }
        public string Method{ get; set; }

    }
}
