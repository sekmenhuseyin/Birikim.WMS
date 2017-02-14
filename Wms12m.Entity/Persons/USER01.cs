using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Wms12m.Entity
{
    [Serializable]
    public class USER01
    {
        [Key]
        public int Id { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public DateTime? CreateDate { get; set; }
        [DataMember]
        public DateTime? Updatedate { get; set; }
        [DataMember]
        public bool Locked { get; set; }
        [DataMember]
        public int CompanyId { get; set; }
        [DataMember]
        public int CreateUserId { get; set; }      
    }
}
