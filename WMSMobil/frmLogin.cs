using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WMSMobil.TerminalService;
using Symbol.Barcode2.Design;

namespace WMSMobil
{
    public partial class frmLogin : Form
    {
        Terminal Servis = new Terminal();
        private Barcode2 Barkod;
        /// <summary>
        /// load
        /// </summary>
        public frmLogin()
        {
            InitializeComponent();
            Ayarlar.KatSayi = (decimal)Screen.PrimaryScreen.Bounds.Width / (decimal)240;
            if (Ayarlar.KatSayi > 4) Ayarlar.KatSayi = 1;
            //barkod
            //Barkod = new Barcode2();
            //Barkod.DeviceType = Symbol.Barcode2.DEVICETYPES.FIRSTAVAILABLE;
            //Barkod.EnableScanner = true;
            //Barkod.OnScan += new Barcode2.OnScanEventHandler(Barkod_OnScan);
        }
        /// <summary>
        /// barkod okursa
        /// </summary>
        public delegate void MethodInvoker();
        void Barkod_OnScan(Symbol.Barcode2.ScanDataCollection scanDataCollection)
        {
            this.Invoke((MethodInvoker)delegate()
            {
                bool focuslandi = false;
                try
                {
                    foreach (Control cont in panel1.Controls)
                    {
                        if (cont.Focused && (cont.Name == txtKullaniciAdi.Name || cont.Name == txtParola.Name))
                        {
                            focuslandi = true;
                            Login login = Servis.LoginKontrol2(scanDataCollection.GetFirst.Text, Ayarlar.AuthCode);
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
                    }
                }
                catch (Exception ex)
                {
                    Mesaj.Hata(ex);
                }
                //eğer hiç bir yere odaklanmamışsa
                if (!focuslandi)
                {
                    Mesaj.Uyari("Lütfen önce bir yazı kutusuna tıklayın!");
                    return;
                }
            });
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
                Login login = Servis.LoginKontrol(txtKullaniciAdi.Text.Trim().Left(5), txtParola.Text.Trim(), Ayarlar.AuthCode);
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
        /// <summary>
        /// kapat
        /// </summary>
        private void btnBack_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
        }
    }
}