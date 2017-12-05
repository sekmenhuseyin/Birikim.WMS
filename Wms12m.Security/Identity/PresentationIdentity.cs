using System.Runtime.Serialization;

namespace Wms12m.Security
{
    [DataContract]
    public class PresentationIdentity
    {
        [DataMember]
        public string Application { get; set; }

        [DataMember]
        public string Channel { get; set; }

        [DataMember]
        public Layer Layer { get; set; }
    }
}
