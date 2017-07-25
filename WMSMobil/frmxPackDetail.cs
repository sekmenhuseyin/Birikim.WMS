using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WMSMobil.TerminalService;

namespace WMSMobil
{
    public partial class frmxPackDetail : Form
    {
        Terminal Servis = new Terminal();
        int GorevID;
        /// <summary>
        /// form load
        /// </summary>
        public frmxPackDetail(int gorevID)
        {
            InitializeComponent();
            Servis.Url = Ayarlar.ServisURL;
            GorevID = gorevID;
            //paket tipi
            Ayarlar.GorevDurumlari = Servis.GetPaketTip(Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid).ToList();
            txtTip.ValueMember = "ID";
            txtTip.DisplayMember = "Name";
            txtTip.DataSource = Ayarlar.GorevDurumlari;
            //paket ayrıntıları
            var tbl = Servis.GetPackageBarcodeDetails(GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
            txtSevkNo.Text = tbl.SevkiyatNo;
            txtPaketNo.Text = tbl.PaketNo;
            txtMiktar.Text = String.Format("{0:n}", tbl.Adet);
            txtTip.SelectedValue = tbl.PaketTipiID;
            txtAgirlik.Text = String.Format("{0:n}", tbl.Agirlik);
            if (tbl.HepsiVar == false)
                Mesaj.Uyari("Bazı ürünlerin ağırlığı eksik");
        }
        /// <summary>
        /// kaydetme işlemleri
        /// </summary>
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var miktar=txtMiktar.Text.ToDecimal();
            var agirlik = txtAgirlik.Text.ToDecimal();
            var tip = txtTip.SelectedValue.ToInt32();
            if (txtSevkNo.Text == "")
            {
                Mesaj.Uyari("Lütfen sevkiyat noyu yazınız");
                return;
            }
            else if (txtPaketNo.Text == "")
            {
                Mesaj.Uyari("Lütfen paket noyu yazınız");
                return;
            }
            else if (miktar == 0)
            {
                Mesaj.Uyari ("Lütfen miktarı yazınız");
                return;
            }
            else if (tip == 0)
            {
                Mesaj.Uyari("Lütfen paket tipini seçiniz");
                return;
            }
            else if (agirlik == 0)
            {
                Mesaj.Uyari("Lütfen ağırlığı yazınız");
                return;
            }
            var pkt = new frmGorevPaket() { SevkiyatNo = txtSevkNo.Text, PaketNo = txtPaketNo.Text, Adet = miktar, Agirlik = agirlik, PaketTipiID = tip };
            var Sonuc = Servis.UpdatePackageBarcode(pkt, GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
            //sonuç işlemleri
            if (Sonuc.Status == false)
                Mesaj.Uyari(Sonuc.Message);
            else
            {
                this.Close();
            }
        }
        /// <summary>
        /// geri
        /// </summary>
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}