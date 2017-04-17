using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WMSMobil.WMSLocal;


namespace WMSMobil
{
    public partial class frmMain : Form
    {
        MobilServis Servis = new MobilServis();
        /// <summary>
        /// form load
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// her form aktif olduğunda görevleri güncelle
        /// düğmelerin yerlerini değiştir
        /// </summary>
        private void AnaForm_Activated(object sender, EventArgs e)
        {
            lblMalKabul.Text = ""; lblRafKaldirma.Text = ""; lblSiparisToplama.Text = ""; lblKabloSiparisi.Text = ""; lblPaketleme.Text = ""; lblSevkiyat.Text = ""; lblTransferIn.Text = ""; lblTransferOut.Text = "";
            var tbl = Servis.GetGorevOzet(Ayarlar.Kullanici.DepoID).ToList();
            foreach (var item in tbl)
            {
                if (item.ID == 1) { lblMalKabul.Text = "[" + item.Sayi.ToString() + "]"; }
                else if (item.ID == 2) { lblRafKaldirma.Text = "[" + item.Sayi.ToString() + "]"; }
                else if (item.ID == 3) { lblSiparisToplama.Text = "[" + item.Sayi.ToString() + "]"; }
                else if (item.ID == 4) { lblKabloSiparisi.Text = "[" + item.Sayi.ToString() + "]"; }
                else if (item.ID == 6) { lblPaketleme.Text = "[" + item.Sayi.ToString() + "]"; }
                else if (item.ID == 7) { lblSevkiyat.Text = "[" + item.Sayi.ToString() + "]"; }
                else if (item.ID == 19) { lblTransferOut.Text = "[" + item.Sayi.ToString() + "]"; }
                else if (item.ID == 20) { lblTransferIn.Text = "[" + item.Sayi.ToString() + "]"; }
            }
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
        /// dispose and exit
        /// </summary>
        private void AnaForm_Closing(object sender, CancelEventArgs e)
        {
            Servis.Dispose();
            Application.Exit();
        }
    }
}