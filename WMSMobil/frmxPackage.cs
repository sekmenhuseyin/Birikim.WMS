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
                txtBarkod.Text = scanDataCollection.GetFirst.Text;
                btnEkle_Click(Barkod, null);//uygula
            });
        }
        /// <summary>
        /// barkod okut
        /// </summary>
        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                //görev bilgilerini getir
                Ayarlar.SeciliGorev = Servis.GetIrsaliyeFromBarcode(txtBarkod.Text, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                //ürün bilgilerini getir
                Ayarlar.STIKalemler = new List<Tip_STI>(Servis.GetMalzemes(Ayarlar.SeciliGorev.ID, Ayarlar.Kullanici.ID, false, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid));
                //listele
                STIGetir();
            }
            catch (Exception ex)
            {
                Mesaj.Hata(ex);
                return;
            }
        }
        /// <summary>
        /// irsaliye detaylarını gösterir
        /// </summary>
        void STIGetir()
        {
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

                TextBox tMiktar = new TextBox();
                tMiktar.Font = font;
                tMiktar.Width = (65 * carpim).ToInt32();
                tMiktar.Location = new Point((145 * carpim).ToInt32(), 0);
                tMiktar.ReadOnly = true;
                tMiktar.TextAlign = HorizontalAlignment.Right;
                tMiktar.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                TextBox tBirim = new TextBox();
                tBirim.Font = font;
                tBirim.Width = (72 * carpim).ToInt32();
                tBirim.Location = new Point((211 * carpim).ToInt32(), 0);
                tBirim.ReadOnly = true;
                tBirim.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                TextBox tMalAdi = new TextBox();
                tMalAdi.Font = font;
                tMalAdi.Width = (80 * carpim).ToInt32();
                tMalAdi.Location = new Point((64 * carpim).ToInt32(), 0);
                tMalAdi.ReadOnly = true;
                tMalAdi.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                TextBox tRaf = new TextBox();
                TextBox tYerlestirmeMiktari = new TextBox();
                TextBox tMiktarOkutulan = new TextBox();
                TextBox tIslemMiktar = new TextBox();
                if (Ayarlar.MenuTip == MenuType.MalKabul || Ayarlar.MenuTip == MenuType.Paketle || Ayarlar.MenuTip == MenuType.Sevkiyat)
                {
                    tMiktarOkutulan.Font = font;
                    tMiktarOkutulan.Width = (92 * carpim).ToInt32();
                    tMiktarOkutulan.Location = new Point((284 * carpim).ToInt32(), 0);
                    tMiktarOkutulan.ReadOnly = false;
                    tMiktarOkutulan.Name = "txtOkutulanMiktar";
                    tMiktarOkutulan.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                    tMiktarOkutulan.BackColor = Color.FromArgb(206, 223, 239);
                    tMiktarOkutulan.Text = stiItem != null ? stiItem.OkutulanMiktar.ToDecimal().ToString("N2") : "0";
                    panelSatir.OkutulanMiktar = stiItem.OkutulanMiktar.ToDecimal();
                }
                else if (Ayarlar.MenuTip == MenuType.KontrollüSayım)
                {
                    tRaf.Font = font;
                    tRaf.Width = (92 * carpim).ToInt32();
                    tRaf.Location = new Point((284 * carpim).ToInt32(), 0);
                    tRaf.ReadOnly = true;
                    tRaf.Name = "txtRaf";
                    tRaf.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                    tRaf.BackColor = Color.FromArgb(206, 223, 239);
                    tRaf.Text = stiItem.Raf != null ? stiItem.Raf : "";
                    panelSatir.Raf = stiItem.Raf != null ? stiItem.Raf : "";

                    tMiktarOkutulan.Font = font;
                    tMiktarOkutulan.Width = (105 * carpim).ToInt32();
                    tMiktarOkutulan.Location = new Point((377 * carpim).ToInt32(), 0);
                    tMiktarOkutulan.ReadOnly = false;
                    tMiktarOkutulan.Name = "txtOkutulanMiktar";
                    tMiktarOkutulan.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                    tMiktarOkutulan.BackColor = Color.FromArgb(206, 223, 239);
                    tMiktarOkutulan.Text = stiItem != null ? stiItem.YerlestirmeMiktari.ToDecimal().ToString("N2") : "0";
                    panelSatir.OkutulanMiktar = stiItem.YerlestirmeMiktari.ToDecimal();
                }
                else if (Ayarlar.MenuTip == MenuType.RafaYerlestirme || Ayarlar.MenuTip == MenuType.SiparisToplama || Ayarlar.MenuTip == MenuType.TransferÇıkış || Ayarlar.MenuTip == MenuType.TransferGiriş)
                {
                    tRaf.Font = font;
                    tRaf.Width = (92 * carpim).ToInt32();
                    tRaf.Location = new Point((284 * carpim).ToInt32(), 0);
                    tRaf.ReadOnly = true;
                    tRaf.Name = "txtRaf";
                    tRaf.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                    tRaf.BackColor = Color.FromArgb(206, 223, 239);
                    tRaf.Text = stiItem.Raf != null ? stiItem.Raf : "";
                    panelSatir.Raf = stiItem.Raf != null ? stiItem.Raf : "";

                    string yermiktar = stiItem.YerMiktar.ToDecimal().ToString("N2");
                    if (yermiktar == "0,00") yermiktar = stiItem.YerlestirmeMiktari.ToDecimal().ToString("N2");
                    tYerlestirmeMiktari.Font = font;
                    tYerlestirmeMiktari.Width = (105 * carpim).ToInt32();
                    tYerlestirmeMiktari.Location = new Point((377 * carpim).ToInt32(), 0);
                    tYerlestirmeMiktari.ReadOnly = true;
                    tYerlestirmeMiktari.Visible = true;
                    tYerlestirmeMiktari.Name = "txtYerlestirmeMiktari";
                    tYerlestirmeMiktari.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                    tYerlestirmeMiktari.BackColor = Color.FromArgb(206, 223, 239);
                    tYerlestirmeMiktari.Text = yermiktar;
                    panelSatir.YerlestirmeMiktari = stiItem.YerlestirmeMiktari.ToDecimal();

                    tIslemMiktar.Font = font;
                    tIslemMiktar.Width = (70 * carpim).ToInt32();
                    tIslemMiktar.Location = new Point((483 * carpim).ToInt32(), 0);
                    tIslemMiktar.ReadOnly = false;
                    tIslemMiktar.Visible = true;
                    tIslemMiktar.Name = "txtIslemMiktar";
                    tIslemMiktar.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                    tIslemMiktar.BackColor = Color.FromArgb(206, 223, 239);
                    tIslemMiktar.Text = "0";
                    panelSatir.IslemMiktar = 0;
                }
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
                tMalKodu.Tag = stiItem.ID.ToInt32();
                tMiktar.Text = stiItem.Miktar.ToDecimal().ToString("N2");
                tMiktar.Tag = stiItem.Miktar.ToDecimal();
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
                if (Ayarlar.MenuTip == MenuType.MalKabul || Ayarlar.MenuTip == MenuType.Paketle || Ayarlar.MenuTip == MenuType.Sevkiyat)
                {
                    panelSatir.Size = new Size((400 * carpim).ToInt32(), (20 * carpim).ToInt32());
                    panelSatir.Controls.Add(tMiktarOkutulan);
                }
                else if (Ayarlar.MenuTip == MenuType.KontrollüSayım)
                {
                    panelSatir.Size = new Size((500 * carpim).ToInt32(), (20 * carpim).ToInt32());
                    panelSatir.Controls.Add(tRaf);
                    panelSatir.Controls.Add(tMiktarOkutulan);
                }
                else if (Ayarlar.MenuTip == MenuType.RafaYerlestirme || Ayarlar.MenuTip == MenuType.SiparisToplama || Ayarlar.MenuTip == MenuType.TransferÇıkış || Ayarlar.MenuTip == MenuType.TransferGiriş)
                {
                    panelSatir.Size = new Size((550 * carpim).ToInt32(), (20 * carpim).ToInt32());
                    panelSatir.Controls.Add(tRaf);
                    panelSatir.Controls.Add(tYerlestirmeMiktari);
                    panelSatir.Controls.Add(tIslemMiktar);
                }
                panelOrta.Controls.Add(panelSatir);
                PanelVeriList.Add(panelSatir);
            }
        }
        /// <summary>
        /// txt focua
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
    }
}