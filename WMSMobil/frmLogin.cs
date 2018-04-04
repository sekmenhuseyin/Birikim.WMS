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
using System.IO;

namespace WMSMobil
{
    public partial class frmLogin : Form
    {
        /// <summary>
        /// load
        /// </summary>
        public frmLogin()
        {
            InitializeComponent();
            /*bizim proje 240*320 olarak planlandı ama bazı terminaller daha yüksek çözünürlüklü onlar için direk Carpim metodu ile düzeltiyoruz.*/
            Ayarlar.KatSayi = (decimal)Screen.PrimaryScreen.Bounds.Height / (decimal)320;
            //burası bilgisayarda çalışırken boyutu değiştirmesin diye
            if (Ayarlar.KatSayi > 2) Ayarlar.KatSayi = 1;
            //version no
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            label4.Text = string.Format("WMS Mobil v{0}.{1}.{2}", version.Major, version.Minor, version.Build);
        }

        /// <summary>
        /// barkod okursa
        /// </summary>
        public delegate void MethodInvoker();
        void Barkod_OnScan(Symbol.Barcode2.ScanDataCollection scanDataCollection)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    if (scanDataCollection.GetFirst.Text != "")
                    {
                        Login login = Program.Servis.LoginKontrol2(scanDataCollection.GetFirst.Text, Ayarlar.AuthCode);
                        if (login.ID != 0)
                        {
                            Ayarlar.Kullanici = login;
                            frmMain anaForm = new frmMain();
                            anaForm.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            Mesaj.Uyari(login.AdSoyad);
                        }
                    }
                });
            }
            catch (Exception)
            {
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
                Cursor.Current = Cursors.WaitCursor;
                Login login = Program.Servis.LoginKontrol(txtKullaniciAdi.Text.Trim().Left(5), txtParola.Text.Trim(), Ayarlar.AuthCode);
                Cursor.Current = Cursors.Default;
                if (login.ID != 0)
                {
                    Ayarlar.Kullanici = login;
                    frmMain anaForm = new frmMain();
                    this.Enabled = true;
                    anaForm.ShowDialog();
                    this.Close();
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
                Cursor.Current = Cursors.Default;
                Mesaj.Uyari("Bağlantı hatası. Lütfen daha sonra tekrar deneyin");
            }
        }

        /// <summary>
        /// connection info
        /// </summary>
        private void btnConnection_Click(object sender, EventArgs e)
        {
            Mesaj.Basari(Ayarlar.ServisURL);
        }

        /// <summary>
        /// entera basarsa
        /// </summary>
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnGiris_Click(sender, null);
            }
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

        /// <summary>
        /// dispose
        /// </summary>
        private void GirisForm_Closing(object sender, CancelEventArgs e)
        {
            Application.Exit();
        }
    }
}