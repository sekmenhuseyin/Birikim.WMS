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
        public int CreateDate { get; set; }
        [DataMember]
        public int Updatedate { get; set; }
        [DataMember]
        public bool Aktif { get; set; }
    }
}
