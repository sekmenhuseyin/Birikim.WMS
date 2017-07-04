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
        }
        /// <summary>
        /// kaydetme işlemleri
        /// </summary>
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var miktar=txtMiktar.Text.ToDecimal();
            var agirlik=txtAgirlik.Text.ToDecimal();
            if (miktar == 0)
            {
                Mesaj.Uyari ("Lütfen miktarı yazınız");
                return;
            }
            else if (agirlik == 0)
            {
                Mesaj.Uyari("Lütfen ağırlığı yazınız");
                return;
            }
            var pkt = new frmGorevPaket() { SevkiyatNo = txtSevkNo.Text, PaketNo = txtPaketNo.Text, Adet = miktar, Agirlik = agirlik, PaketTipiID = txtTip.SelectedValue.ToInt32() };
            var Sonuc = Servis.UpdatePackageBarcode(pkt, GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
            //sonuç işlemleri
            if (Sonuc.Status == false)
                Mesaj.Uyari(Sonuc.Message);
            else
            {
                Mesaj.Basari("Kayıt tamamlandı");
                this.Close();
            }
        }
    }
}