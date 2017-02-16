using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Wms12m.Entity
{
    public class frmGorev
    {
        [Key]
        public int ID { get; set; }

        [DataMember, DisplayName("Depo"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int DepoID { get; set; }

        [DataMember, DisplayName("Görev No"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string GorevNo { get; set; }

        [DataMember, DisplayName("Görev Tipi"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int GorevTipiID { get; set; }

        [DataMember, DisplayName("Görev Durumu"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int DurumID { get; set; }

        [DataMember, DisplayName("Görevli"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int GorevliID { get; set; }

        [DataMember, DisplayName("Atayan"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int AtayanID { get; set; }

        [DataMember, DisplayName("Oluşturma Tarihi"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int OlusturmaTarihi { get; set; }
        [DataMember, DisplayName("Atama Tarihi"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int AtamaTarihi { get; set; }
        [DataMember, DisplayName("Bitirme Tarihi"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int BitirmeTarihi { get; set; }
        [DataMember, DisplayName("Bilgi"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string Bilgi { get; set; }
        [DataMember, DisplayName("Açıklama"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string Aciklama { get; set; }
    }
}
