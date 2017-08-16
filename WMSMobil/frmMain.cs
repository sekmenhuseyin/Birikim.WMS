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
    public partial class frmMain : Form
    {
        Terminal Servis = new Terminal();
        bool Baglandi = true;
        /// <summary>
        /// form load
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
            Servis.Url = Ayarlar.ServisURL;
        }
        /// <summary>
        /// her form aktif olduğunda görevleri güncelle
        /// düğmelerin yerlerini değiştir
        /// </summary>
        private void AnaForm_Activated(object sender, EventArgs e)
        {
            if (Baglandi == false)
                return;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                lblMalKabul.Text = ""; lblRafKaldirma.Text = ""; lblSiparisToplama.Text = ""; lblSayim.Text = ""; lblPaketleme.Text = ""; lblTransferIn.Text = ""; lblTransferOut.Text = "";
                var tbl = Servis.GetGorevOzet(Ayarlar.Kullanici.DepoID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid).ToList();
                foreach (var item in tbl)
                {
                    if (item.ID == 1) { lblMalKabul.Text = "[" + item.Sayi.ToString() + "]"; }
                    else if (item.ID == 2) { lblRafKaldirma.Text = "[" + item.Sayi.ToString() + "]"; }
                    else if (item.ID == 3) { lblSiparisToplama.Text = "[" + item.Sayi.ToString() + "]"; }
                    else if (item.ID == 6) { lblPaketleme.Text = "[" + item.Sayi.ToString() + "]"; }
                    else if (item.ID == 8) { lblSayim.Text = "[" + item.Sayi.ToString() + "]"; }
                    else if (item.ID == 19) { lblTransferOut.Text = "[" + item.Sayi.ToString() + "]"; }
                    else if (item.ID == 20) { lblTransferIn.Text = "[" + item.Sayi.ToString() + "]"; }
                }
            }
            catch (Exception)
            {
                this.Enabled = true;
                Baglandi = false;
                Mesaj.Uyari("Bağlantı hatası. Lütfen daha sonra tekrar deneyin");
            }
            Cursor.Current = Cursors.Default;
        }
        /// <summary>
        /// yenile
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Baglandi = true;
            AnaForm_Activated(sender, e);
        }
        /// <summary>
        /// btn click
        /// </summary>
        private void btns_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Ayarlar.MenuTipSet = button.Tag.ToInt32();
            frmTasks frm = new frmTasks();
            frm.ShowDialog();
        }
        /// <summary>
        /// barkod okuma sayfası
        /// </summary>
        private void btnBarcode_Click(object sender, EventArgs e)
        {
            frmxPackage frm = new frmxPackage();
            frm.ShowDialog();
        }
        /// <summary>
        /// kapat
        /// </summary>
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// dispose and exit
        /// </summary>
        private void AnaForm_Closing(object sender, CancelEventArgs e)
        {
            Servis.Dispose();
        }
    }
}