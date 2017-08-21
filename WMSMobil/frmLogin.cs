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
        Terminal Servis = new Terminal();
        private Barcode2 Barkod;
        /// <summary>
        /// load
        /// </summary>
        public frmLogin()
        {
            InitializeComponent();
            Cursor.Current = Cursors.WaitCursor;
            Ayarlar.KatSayi = (decimal)Screen.PrimaryScreen.Bounds.Width / (decimal)240;
            if (Ayarlar.KatSayi > 4) Ayarlar.KatSayi = 1;
            //create if file doesnt exists
            if (!File.Exists(@"\WMSMobil-ip.txt"))
                using (var Dosya = new FileStream(@"\WMSMobil-ip.txt", FileMode.OpenOrCreate, FileAccess.Write))
                    using (var Oku = new StreamWriter(Dosya))
                        Oku.WriteLine(Servis.Url);
            //get servis url
            using (var Dosya = new FileStream(@"\WMSMobil-ip.txt", FileMode.Open))
                using (var Oku = new StreamReader(Dosya))
                    Ayarlar.ServisURL = Oku.ReadLine();
            //set servis
            Servis.Url = Ayarlar.ServisURL;
            //barkod
            //Barkod = new Barcode2();
            //Barkod.DeviceType = Symbol.Barcode2.DEVICETYPES.FIRSTAVAILABLE;
            //try
            //{
            //    Barkod.EnableScanner = true;
            //}
            //catch (Exception)
            //{
            //}
            //Barkod.OnScan += new Barcode2.OnScanEventHandler(Barkod_OnScan);
            Cursor.Current = Cursors.Default;
        }
        /// <summary>
        /// barkod okursa
        /// </summary>
        public delegate void MethodInvoker();
        void Barkod_OnScan(Symbol.Barcode2.ScanDataCollection scanDataCollection)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate()
                {
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
                });
            }
            catch (Exception)
            {
                this.Enabled = true;
                Mesaj.Uyari("Bağlantı hatası. Lütfen daha sonra tekrar deneyin");
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
                Login login = Servis.LoginKontrol(txtKullaniciAdi.Text.Trim().Left(5), txtParola.Text.Trim(), Ayarlar.AuthCode);
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
                Mesaj.Uyari("Bağlantı hatası. Lütfen daha sonra tekrar deneyin");
            }
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
            try
            {
                Barkod.EnableScanner = false;
                Barkod.Dispose();
            }
            catch (Exception)
            {
            }
            Servis.Dispose();
            Application.Exit();
        }
    }
}