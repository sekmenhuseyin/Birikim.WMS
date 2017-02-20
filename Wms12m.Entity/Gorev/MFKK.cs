using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
    [Table("MFK")]
    [DataContract(IsReference = true)]
    public class MFKK
    {
        [Key]
        [Column(Order = 4)]
        [Required]
        [Display(Name = "Islem Tip")]
        public short IslemTip { get; set; }
        [Key]
        [Column(Order = 1)]
        [MaxLength(16)]
        [Required]
        [Display(Name = "Evrak No")]
        public string EvrakNo { get; set; }
        [Key]
        [Column(Order = 2)]
        [Required]
        [Display(Name = "Evrak Tarih")]
        public int EvrakTarih { get; set; }
        [Key]
        [Column(Order = 3)]
        [MaxLength(20)]
        [Required]
        [Display(Name = "Hesap Kod")]
        public string HesapKod { get; set; }
        [Key]
        [Column(Order = 5)]
        [Required]
        [Display(Name = "Kynk Evrak Tip")]
        public short KynkEvrakTip { get; set; }
        [Required]
        [Display(Name = "Tarih")]
        public int Tarih { get; set; }
        [Required]
        [Display(Name = "Tutar")]
        public decimal Tutar { get; set; }
        [MaxLength(120)]
        [Required]
        [Display(Name = "Aciklama")]
        public string Aciklama { get; set; }
        [MaxLength(120)]
        [Required]
        [Display(Name = "Aciklama2")]
        public string Aciklama2 { get; set; }
        [MaxLength(120)]
        [Required]
        [Display(Name = "Aciklama3")]
        public string Aciklama3 { get; set; }
        [MaxLength(120)]
        [Required]
        [Display(Name = "Aciklama4")]
        public string Aciklama4 { get; set; }
        [MaxLength(120)]
        [Required]
        [Display(Name = "Aciklama5")]
        public string Aciklama5 { get; set; }
        [MaxLength(120)]
        [Required]
        [Display(Name = "Aciklama6")]
        public string Aciklama6 { get; set; }
        [MaxLength(10)]
        [Required]
        [Display(Name = "Kod1")]
        public string Kod1 { get; set; }
        [MaxLength(10)]
        [Required]
        [Display(Name = "Kod2")]
        public string Kod2 { get; set; }
        [MaxLength(10)]
        [Required]
        [Display(Name = "Kod3")]
        public string Kod3 { get; set; }
        [MaxLength(10)]
        [Required]
        [Display(Name = "Kod4")]
        public string Kod4 { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Kod5")]
        public string Kod5 { get; set; }
        [Required]
        [Display(Name = "Kod6")]
        public decimal Kod6 { get; set; }
        [Required]
        [Display(Name = "Kod7")]
        public decimal Kod7 { get; set; }
        [MaxLength(254)]
        [Required]
        [Display(Name = "Nesne1")]
        public string Nesne1 { get; set; }
        [MaxLength(254)]
        [Required]
        [Display(Name = "Nesne2")]
        public string Nesne2 { get; set; }
        [MaxLength(254)]
        [Required]
        [Display(Name = "Nesne3")]
        public string Nesne3 { get; set; }
        [Required]
        [Display(Name = "Onay Sema Kod")]
        public int OnaySemaKod { get; set; }
        [Required]
        [Display(Name = "Onay Islem Tip")]
        public short OnayIslemTip { get; set; }
        [Required]
        [Display(Name = "Onay Status")]
        public short OnayStatus { get; set; }
        [MaxLength(5)]
        [Required]
        [Display(Name = "Son Onaylayan")]
        public string SonOnaylayan { get; set; }
        [MaxLength(5)]
        [Required]
        [Display(Name = "Onaylayan1")]
        public string Onaylayan1 { get; set; }
        [MaxLength(5)]
        [Required]
        [Display(Name = "Onaylayan2")]
        public string Onaylayan2 { get; set; }
        [MaxLength(5)]
        [Required]
        [Display(Name = "Onaylayacak")]
        public string Onaylayacak { get; set; }
        [MaxLength(2)]
        [Required]
        [Display(Name = "Guvenlik Kod")]
        public string GuvenlikKod { get; set; }
        [MaxLength(5)]
        [Required]
        [Display(Name = "Kaydeden")]
        public string Kaydeden { get; set; }
        [Required]
        [Display(Name = "Kayit Tarih")]
        public int KayitTarih { get; set; }
        [Required]
        [Display(Name = "Kayit Saat")]
        public int KayitSaat { get; set; }
        [Required]
        [Display(Name = "Kayit Kaynak")]
        public short KayitKaynak { get; set; }
        [MaxLength(12)]
        [Required]
        [Display(Name = "Kayit Surum")]
        public string KayitSurum { get; set; }
        [MaxLength(5)]
        [Required]
        [Display(Name = "Degistiren")]
        public string Degistiren { get; set; }
        [Required]
        [Display(Name = "Degis Tarih")]
        public int DegisTarih { get; set; }
        [Required]
        [Display(Name = "Degis Saat")]
        public int DegisSaat { get; set; }
        [Required]
        [Display(Name = "Degis Kaynak")]
        public short DegisKaynak { get; set; }
        [MaxLength(12)]
        [Required]
        [Display(Name = "Degis Surum")]
        public string DegisSurum { get; set; }
        [Required]
        [Display(Name = "Check Sum")]
        public short CheckSum { get; set; }
        [MaxLength(5)]
        [Required]
        [Display(Name = "Depo")]
        public string Depo { get; set; }
        [MaxLength(16)]
        [Required]
        [Display(Name = "Onay Takip No")]
        public string OnayTakipNo { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "Row ID")]
        public int Row_ID { get; set; }
        [Timestamp]
        [Required]
        [Display(Name = "timestamp")]
        public byte[] timestamp { get; set; }
    }

}
