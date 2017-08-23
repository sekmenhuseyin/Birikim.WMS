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
    public partial class frmxPackage : Form
    {
        Terminal Servis = new Terminal();
        private Barcode2 Barkod;
        List<PanelEx> PanelVeriList = new List<PanelEx>();
        int Sayac = 0;
        decimal carpim = Ayarlar.KatSayi;
        /// <summary>
        /// form load
        /// </summary>
        public frmxPackage()
        {
            InitializeComponent();
            Servis.Url = Ayarlar.ServisURL;
            //barkod
            Barkod = new Barcode2();
            Barkod.DeviceType = Symbol.Barcode2.DEVICETYPES.FIRSTAVAILABLE;
            try
            {
                Barkod.EnableScanner = true;
            }
            catch (Exception)
            {
            }
            Barkod.OnScan += new Barcode2.OnScanEventHandler(Barkod_OnScan);
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
                    txtBarkod.Text = scanDataCollection.GetFirst.Text;
                    btnEkle_Click(Barkod, null);//uygula
                });
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// barkod okut
        /// </summary>
        private void btnEkle_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                //görev bilgilerini getir
                Ayarlar.SeciliGorev = Servis.GetIrsaliyeFromBarcode(txtBarkod.Text, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                txtUnvan.Text = Ayarlar.SeciliGorev.Unvan;
                txtHesapKodu.Text = Ayarlar.SeciliGorev.HesapKodu;
                txtEvrakno.Text = Ayarlar.SeciliGorev.EvrakNo;
                txtTarih.Text = Ayarlar.SeciliGorev.Tarih;
                txtAgirlik.Text = Ayarlar.SeciliGorev.TeslimCHK;
                //ürün bilgilerini getir
                Ayarlar.STIKalemler = new List<Tip_STI>(Servis.GetMalzemes(Ayarlar.SeciliGorev.ID, Ayarlar.Kullanici.ID, false, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid));
            }
            catch (Exception ex)
            {
                Mesaj.Hata(ex);
            }
            Cursor.Current = Cursors.Default;
            //listele
            STIGetir();
        }
        /// <summary>
        /// irsaliye detaylarını gösterir
        /// </summary>
        void STIGetir()
        {
            Cursor.Current = Cursors.WaitCursor;
            foreach (PanelEx rmvItem in PanelVeriList)
            {
                panelOrta.Controls.Remove(rmvItem);
            }
            PanelVeriList.Clear();
            Sayac = 0;
            foreach (Tip_STI stiItem in Ayarlar.STIKalemler)
            {
                Sayac++;
                panelOrta.AutoScrollPosition = new Point(0, 0);

                Font font = new Font("Tahoma", 8, FontStyle.Regular);
                PanelEx panelSatir = new PanelEx();
                panelSatir.Name = Sayac.ToString();
                panelSatir.Location = new Point(0, (Sayac * 20 * carpim).ToInt32());
                panelSatir.Size = new Size((240 * carpim).ToInt32(), (20 * carpim).ToInt32());

                TextBox tBarkod = new TextBox();
                tBarkod.Visible = false;
                tBarkod.Width = 3;
                tBarkod.Location = new Point(0, 0);
                tBarkod.ReadOnly = true;
                tBarkod.Name = "txtMalKodu";

                TextBox tMalKodu = new TextBox();
                tMalKodu.Font = font;
                tMalKodu.Width = (60 * carpim).ToInt32();
                tMalKodu.Location = new Point((3 * carpim).ToInt32(), 0);
                tMalKodu.ReadOnly = true;
                tMalKodu.Name = "txtMalKodu";
                tMalKodu.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                TextBox tMalAdi = new TextBox();
                tMalAdi.Font = font;
                tMalAdi.Width = (80 * carpim).ToInt32();
                tMalAdi.Location = new Point((64 * carpim).ToInt32(), 0);
                tMalAdi.ReadOnly = true;
                tMalAdi.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                TextBox tMiktar = new TextBox();
                tMiktar.Font = font;
                tMiktar.Width = (53 * carpim).ToInt32();
                tMiktar.Location = new Point((145 * carpim).ToInt32(), 0);
                tMiktar.ReadOnly = true;
                tMiktar.TextAlign = HorizontalAlignment.Right;
                tMiktar.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                TextBox tBirim = new TextBox();
                tBirim.Font = font;
                tBirim.Width = (39 * carpim).ToInt32();
                tBirim.Location = new Point((199 * carpim).ToInt32(), 0);
                tBirim.ReadOnly = true;
                tBirim.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                //renkler
                tMalKodu.BackColor = Color.FromArgb(206, 223, 239);
                tMiktar.BackColor = Color.FromArgb(206, 223, 239);
                tBirim.BackColor = Color.FromArgb(206, 223, 239);
                tMalAdi.BackColor = Color.FromArgb(206, 223, 239);
                //yazı ve tag
                tBarkod.Text = stiItem.Barkod;
                tMalKodu.Text = stiItem.MalKodu;
                tBirim.Text = stiItem.Birim;
                tMalAdi.Text = stiItem.MalAdi;
                tMiktar.Text = stiItem.Miktar.ToDecimal().ToString("N2");
                //panel ekle
                panelSatir.Barkod = stiItem.Barkod;
                panelSatir.MalAdi = stiItem.MalAdi;
                panelSatir.MalKodu = stiItem.MalKodu;
                panelSatir.Miktar = stiItem.Miktar;
                panelSatir.Birim = stiItem.Birim;
                panelSatir.Controls.Add(tBarkod);
                panelSatir.Controls.Add(tMalKodu);
                panelSatir.Controls.Add(tMalAdi);
                panelSatir.Controls.Add(tMiktar);
                panelSatir.Controls.Add(tBirim);

                panelOrta.Controls.Add(panelSatir);
                PanelVeriList.Add(panelSatir);
            }
            Cursor.Current = Cursors.Default;
        }
        /// <summary>
        /// txt focus
        /// </summary>
        void TextBoxlar_GotFocus(object sender, EventArgs e)
        {
            Control panel = (sender as TextBox).Parent;
            foreach (var itemPanel in PanelVeriList)
            {
                foreach (Control item in itemPanel.Controls)
                    item.BackColor = Color.FromArgb(206, 223, 239);
            }
            foreach (Control itemSecili in panel.Controls)
                itemSecili.BackColor = Color.DarkOrange;
        }
        /// <summary>
        /// geri
        /// </summary>
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmxPackage_Closing(object sender, CancelEventArgs e)
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
        }
    }
}