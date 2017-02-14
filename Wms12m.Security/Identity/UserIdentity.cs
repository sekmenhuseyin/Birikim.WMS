using System.Runtime.Serialization;

namespace Wms12m.Security
{
    [DataContract]
    public class UserIdentity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string LogonUserName { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string[] Email { get; set; }

        [DataMember]
        public int Language { get; set; }

        [DataMember]
        public bool Gender { get; set; }


    }

}
