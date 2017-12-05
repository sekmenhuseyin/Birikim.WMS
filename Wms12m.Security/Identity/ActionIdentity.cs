using System.Runtime.Serialization;

namespace Wms12m.Security
{
    [DataContract]
    public class ActionIdentity
    {
        [DataMember]
        public string TrackingCode { get; set; }

        [DataMember]
        public string Machine { get; set; }

        [DataMember]
        public string IpAddress { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Operation { get; set; }
    }
}
