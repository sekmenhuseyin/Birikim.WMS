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
    [Table("FTD")]
    [DataContract(IsReference = true)]
    public class FTDD
    {
        [Key]
        [Column(Order = 5)]
        [Required]
        [Display(Name = "Islem Tip")]
        public short IslemTip { get; set; }
        [Key]
        [Column(Order = 6)]
        [Required]
        [Display(Name = "BA")]
        public short BA { get; set; }
        [Key]
        [Column(Order = 1)]
        [MaxLength(16)]
        [Required]
        [Display(Name = "Evrak No")]
        public string EvrakNo { get; set; }
        [Key]
        [Column(Order = 2)]
        [Required]
        [Display(Name = "Tarih")]
        public int Tarih { get; set; }
        [Key]
        [Column(Order = 3)]
        [MaxLength(20)]
        [Required]
        [Display(Name = "Hesap Kodu")]
        public string HesapKodu { get; set; }
        [Key]
        [Column(Order = 4)]
        [Required]
        [Display(Name = "Sira No")]
        public short SiraNo { get; set; }
        [Required]
        [Display(Name = "Satir Tip")]
        public short SatirTip { get; set; }
        [MaxLength(64)]
        [Required]
        [Display(Name = "Aciklama")]
        public string Aciklama { get; set; }
        [MaxLength(2)]
        [Required]
        [Display(Name = "Iskonto Tur")]
        public string IskontoTur { get; set; }
        [Required]
        [Display(Name = "Iskonto Oran")]
        public Single IskontoOran { get; set; }
        [Required]
        [Display(Name = "Iskonto")]
        public decimal Iskonto { get; set; }
        [Required]
        [Display(Name = "Dvz TL")]
        public short DvzTL { get; set; }
        [MaxLength(3)]
        [Required]
        [Display(Name = "Doviz Cinsi")]
        public string DovizCinsi { get; set; }
        [Required]
        [Display(Name = "Doviz Tutar")]
        public decimal DovizTutar { get; set; }
        [Required]
        [Display(Name = "Doviz Kuru")]
        public decimal DovizKuru { get; set; }
        [Required]
        [Display(Name = "Printed")]
        public short Printed { get; set; }
        [MaxLength(16)]
        [Required]
        [Display(Name = "Fyt Liste No")]
        public string FytListeNo { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Teslim Chk")]
        public string TeslimChk { get; set; }
        [Required]
        [Display(Name = "Nakli Yekun")]
        public short NakliYekun { get; set; }
        [Required]
        [Display(Name = "Dvz Kur Cins")]
        public short DvzKurCins { get; set; }
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
        [Display(Name = "Kynk Evrak Tip")]
        public short KynkEvrakTip { get; set; }
        [Required]
        [Display(Name = "Ana Evrak Tip")]
        public short AnaEvrakTip { get; set; }
        [Required]
        [Display(Name = "Form Ba Bs Tarih")]
        public int FormBaBsTarih { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Ithalat Dosya No")]
        public string IthalatDosyaNo { get; set; }
        [MaxLength(7)]
        [Required]
        [Display(Name = "Tevkifat Oran")]
        public string TevkifatOran { get; set; }
        [Required]
        [Display(Name = "E Fat Tip")]
        public short EFatTip { get; set; }
        [Required]
        [Display(Name = "E Fat Durum")]
        public short EFatDurum { get; set; }
        [Required]
        [Display(Name = "E Fat Donem Bas")]
        public int EFatDonemBas { get; set; }
        [Required]
        [Display(Name = "E Fat Donem Bit")]
        public int EFatDonemBit { get; set; }
        [Required]
        [Display(Name = "E Fat Sure")]
        public short EFatSure { get; set; }
        [Required]
        [Display(Name = "E Fat Sure Durum")]
        public short EFatSureDurum { get; set; }
        [MaxLength(250)]
        [Required]
        [Display(Name = "E Fat Donem Ack")]
        public string EFatDonemAck { get; set; }
        [MaxLength(256)]
        [Required]
        [Display(Name = "E Fat Not")]
        public string EFatNot { get; set; }
        [MaxLength(16)]
        [Required]
        [Display(Name = "E Fat Referans No")]
        public string EFatReferansNo { get; set; }
        [MaxLength(2)]
        [Required]
        [Display(Name = "Gtk Liste No")]
        public string GtkListeNo { get; set; }
        [Required]
        [Display(Name = "Yuv Fark")]
        public decimal YuvFark { get; set; }
        [Required]
        [Display(Name = "Dvz Yuv Fark")]
        public decimal DvzYuvFark { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "KDV Muaf Ack")]
        public string KDVMuafAck { get; set; }
        [MaxLength(128)]
        [Required]
        [Display(Name = "Efat Etiket GB")]
        public string EfatEtiketGB { get; set; }
        [MaxLength(128)]
        [Required]
        [Display(Name = "Efat Etiket PK")]
        public string EfatEtiketPK { get; set; }
        [MaxLength(32)]
        [Required]
        [Display(Name = "Alici Siparis No")]
        public string AliciSiparisNo { get; set; }
        [Required]
        [Display(Name = "Alici Siparis Tarih")]
        public int AliciSiparisTarih { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Ihracat Dosya No")]
        public string IhracatDosyaNo { get; set; }
        [MaxLength(7)]
        [Required]
        [Display(Name = "Tevkifat Oran Kod")]
        public string TevkifatOranKod { get; set; }
        [MaxLength(3)]
        [Required]
        [Display(Name = "Ozel Mat KDV Kod")]
        public string OzelMatKDVKod { get; set; }
        [Required]
        [Display(Name = "E Arsiv Teslim Sekli")]
        public short EArsivTeslimSekli { get; set; }
        [Required]
        [Display(Name = "E Arsiv Fatura Tipi")]
        public short EArsivFaturaTipi { get; set; }
        [Required]
        [Display(Name = "E Arsiv Fatura Durum")]
        public short EArsivFaturaDurum { get; set; }
        [Required]
        [Display(Name = "Fatura Saati")]
        public int FaturaSaati { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "ROW ID")]
        public int ROW_ID { get; set; }
        [Timestamp]
        [Required]
        [Display(Name = "timestamp")]
        public byte[] timestamp { get; set; }
    }

    

}
