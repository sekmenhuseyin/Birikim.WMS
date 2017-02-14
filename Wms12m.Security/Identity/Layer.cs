using System.Runtime.Serialization;

namespace Wms12m.Security
{
    [DataContract]
    public enum Layer
    {
        [EnumMember]
        Unknown = 0,
        [EnumMember]
        Data = 1,
        [EnumMember]
        Service = 2,
        [EnumMember]
        Application = 4
    }
}
