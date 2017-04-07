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
    public partial class GirisForm : Form
    {
        MobilServis Servis = new MobilServis();
        /// <summary>
        /// load
        /// </summary>
        public GirisForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// entera basarsa
        /// </summary>
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13)
            {
                btnGiris_Click(sender, null);
            }
        }
        /// <summary>
        /// login
        /// </summary>
        private void btnGiris_Click(object sender, EventArgs e)
        {
            if (txtKullaniciAdi.Text.Trim() == "" || txtParola.Text.Trim() == "")
            {
                Mesaj.Uyari("Kullanıcı adı veya parola hatalı");
                return;
            }
            Login login = Servis.LoginKontrol(txtKullaniciAdi.Text.Trim().Left(5), txtParola.Text.Trim());
            if (login.IsNotNull())
            {
                Ayarlar.Kullanici = login;
                AnaForm anaForm = new AnaForm();
                anaForm.ShowDialog();
            }
            else
                Mesaj.Uyari("Kullanıcı adı veya parola hatalı");
        }
        /// <summary>
        /// dispose
        /// </summary>
        private void GirisForm_Closing(object sender, CancelEventArgs e)
        {
            Servis.Dispose();
        }
    }
}