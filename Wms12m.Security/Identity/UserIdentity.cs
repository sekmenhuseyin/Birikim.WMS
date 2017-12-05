using System.Runtime.Serialization;

namespace Wms12m.Security
{
    [DataContract]
    public class UserIdentity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Guid { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public int Language { get; set; }

        [DataMember]
        public int? DepoId { get; set; }

        [DataMember]
        public string SirketKodu { get; set; }

        [DataMember]
        public string SirketAdi { get; set; }
    }
}
