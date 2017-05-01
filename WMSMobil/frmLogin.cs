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
    public partial class frmLogin : Form
    {
        MobilServis Servis = new MobilServis();
        /// <summary>
        /// load
        /// </summary>
        public frmLogin()
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
            this.Enabled = false;
            try
            {
                Login login = Servis.LoginKontrol(txtKullaniciAdi.Text.Trim().Left(5), txtParola.Text.Trim());
                if (login.ID != 0)
                {
                    Ayarlar.Kullanici = login;
                    frmMain anaForm = new frmMain();
                    this.Enabled = true;
                    anaForm.ShowDialog();
                }
                else
                {
                    this.Enabled = true;
                    Mesaj.Uyari(login.AdSoyad);
                }
            }
            catch (Exception)
            {
                this.Enabled = true;
                Mesaj.Uyari("Bağlantı hatası. Lütfen daha sonra tekrar deneyin");
            }
        }
        /// <summary>
        /// dispose
        /// </summary>
        private void GirisForm_Closing(object sender, CancelEventArgs e)
        {
            Servis.Dispose();
            Application.Exit();
        }
        /// <summary>
        /// textbox focusta selectall yap
        /// </summary>
        private void txt_GotFocus(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }
}