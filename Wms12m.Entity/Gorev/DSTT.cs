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
    [Table("DST")]
    [DataContract(IsReference = true)]
    public class DSTT
    {
        [Key]
        [Column(Order = 1)]
        [MaxLength(30)]
        [Required]
        [Display(Name = "Mal Kodu")]
        public string MalKodu { get; set; }
        [Key]
        [Column(Order = 2)]
        [MaxLength(5)]
        [Required]
        [Display(Name = "Depo")]
        public string Depo { get; set; }
        [Required]
        [Display(Name = "Kritik Stok")]
        public decimal KritikStok { get; set; }
        [Required]
        [Display(Name = "Azami Stok")]
        public decimal AzamiStok { get; set; }
        [Required]
        [Display(Name = "Dvr Miktar")]
        public decimal DvrMiktar { get; set; }
        [Required]
        [Display(Name = "Dvr Tutar")]
        public decimal DvrTutar { get; set; }
        [Required]
        [Display(Name = "Dvr Tarih")]
        public int DvrTarih { get; set; }
        [Required]
        [Display(Name = "Gir Miktar")]
        public decimal GirMiktar { get; set; }
        [Required]
        [Display(Name = "Gir Tutar")]
        public decimal GirTutar { get; set; }
        [Required]
        [Display(Name = "Gir Iskonto")]
        public decimal GirIskonto { get; set; }
        [Required]
        [Display(Name = "Dvz Gir Tutar")]
        public decimal DvzGirTutar { get; set; }
        [Required]
        [Display(Name = "Dvz Gir Iskonto")]
        public decimal DvzGirIskonto { get; set; }
        [Required]
        [Display(Name = "Son Gir Tarih")]
        public int SonGirTarih { get; set; }
        [Required]
        [Display(Name = "Cik Miktar")]
        public decimal CikMiktar { get; set; }
        [Required]
        [Display(Name = "Cik Tutar")]
        public decimal CikTutar { get; set; }
        [Required]
        [Display(Name = "Cik Iskonto")]
        public decimal CikIskonto { get; set; }
        [Required]
        [Display(Name = "Dvz Cik Tutar")]
        public decimal DvzCikTutar { get; set; }
        [Required]
        [Display(Name = "Dvz Cik Iskonto")]
        public decimal DvzCikIskonto { get; set; }
        [Required]
        [Display(Name = "Son Cik Tarih")]
        public int SonCikTarih { get; set; }
        [Required]
        [Display(Name = "Son Sayim Tarih")]
        public int SonSayimTarih { get; set; }
        [Required]
        [Display(Name = "Son Sayim Farki")]
        public decimal SonSayimFarki { get; set; }
        [Required]
        [Display(Name = "Sat Siparis")]
        public decimal SatSiparis { get; set; }
        [Required]
        [Display(Name = "Al Siparis")]
        public decimal AlSiparis { get; set; }
        [Required]
        [Display(Name = "Sat Rezervasyon")]
        public decimal SatRezervasyon { get; set; }
        [Required]
        [Display(Name = "Al Rezervasyon")]
        public decimal AlRezervasyon { get; set; }
        [Required]
        [Display(Name = "Kons Gir Miktar")]
        public decimal KonsGirMiktar { get; set; }
        [Required]
        [Display(Name = "Kons Cik Miktar")]
        public decimal KonsCikMiktar { get; set; }
        [Required]
        [Display(Name = "Son Mly Sekli")]
        public short SonMlySekli { get; set; }
        [Required]
        [Display(Name = "Son Mly Tarih")]
        public int SonMlyTarih { get; set; }
        [Required]
        [Display(Name = "Son Mly Birim Fiyat")]
        public decimal SonMlyBirimFiyat { get; set; }
        [Required]
        [Display(Name = "Miktar2")]
        public decimal Miktar2 { get; set; }
        [Required]
        [Display(Name = "Tutar2")]
        public decimal Tutar2 { get; set; }
        [Required]
        [Display(Name = "Blk Miktar")]
        public decimal BlkMiktar { get; set; }
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
        [Required]
        [Display(Name = "Kod5")]
        public decimal Kod5 { get; set; }
        [Required]
        [Display(Name = "Kod6")]
        public decimal Kod6 { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Kod7")]
        public string Kod7 { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Kod8")]
        public string Kod8 { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Kod9")]
        public string Kod9 { get; set; }
        [Required]
        [Display(Name = "Kod10")]
        public short Kod10 { get; set; }
        [Required]
        [Display(Name = "Kod11")]
        public short Kod11 { get; set; }
        [Required]
        [Display(Name = "Kod12")]
        public decimal Kod12 { get; set; }
        [Required]
        [Display(Name = "Kod13")]
        public decimal Kod13 { get; set; }
        [Required]
        [Display(Name = "Bak Gost Sekli")]
        public short BakGostSekli { get; set; }
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
        [Required]
        [Display(Name = "Tahmini Stok")]
        public decimal TahminiStok { get; set; }
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
