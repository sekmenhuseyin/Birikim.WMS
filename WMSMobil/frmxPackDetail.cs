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
        /// <summary>
        /// form load
        /// </summary>
        public frmxPackDetail(int GorevID)
        {
            InitializeComponent();
            //paket tipi
            Ayarlar.GorevDurumlari = Servis.GetDurums(Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid).ToList();
            txtTip.ValueMember = "ID";
            txtTip.DisplayMember = "Name";
            txtTip.DataSource = Ayarlar.GorevDurumlari;
        }
        /// <summary>
        /// kaydetme işlemleri
        /// </summary>
        private void btnKaydet_Click(object sender, EventArgs e)
        {

        }
    }
}