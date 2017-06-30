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
    public partial class frmxOps : Form
    {
        Terminal Servis = new Terminal();
        private Barcode2 Barkod;
        bool glbTip;
        int GorevID, IrsaliyeID, GorevTip;
        string FocusPanelName = "";
        int Sayac = 0;
        decimal carpim = Ayarlar.KatSayi;
        List<PanelEx> PanelVeriList = new List<PanelEx>();
        /// <summary>
        /// form load
        /// </summary>
        public frmxOps(int grvId, int irsID, bool tip, int gorevtip)
        {
            InitializeComponent();
            glbTip = tip;
            GorevTip=gorevtip;
            //barkod
            //Barkod = new Barcode2();
            //Barkod.DeviceType = Symbol.Barcode2.DEVICETYPES.FIRSTAVAILABLE;
            //Barkod.EnableScanner = true;
            //Barkod.OnScan += new Barcode2.OnScanEventHandler(Barkod_OnScan);
            try
            {
                Ayarlar.STIKalemler = new List<Tip_STI>(Servis.GetMalzemes(grvId, Ayarlar.Kullanici.ID, tip, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid));
                if (Ayarlar.STIKalemler.Count == 0 && gorevtip != 8)
                    if (Mesaj.Soru("Bu görevin işleri bitmiş. Tüm listeye bakmak istiyor musunuz?") == DialogResult.Yes)
                    {
                        glbTip = false;
                        Ayarlar.STIKalemler = new List<Tip_STI>(Servis.GetMalzemes(grvId, Ayarlar.Kullanici.ID, false, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid));
                    }
                Ayarlar.SeciliGorev = Servis.GetIrsaliye(grvId, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                txtUnvan.Text = Ayarlar.SeciliGorev.Unvan;
                txtHesapKodu.Text = Ayarlar.SeciliGorev.HesapKodu;
                txtEvrakno.Text = Ayarlar.SeciliGorev.EvrakNo;
                txtEvrakno.Tag = Ayarlar.SeciliGorev.ID;
                GorevID = grvId;
                IrsaliyeID = irsID;
                STIGetir();
            }
            catch (Exception ex)
            {
                Mesaj.Hata(ex);
                return;
            }
            //gizle göster
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            txtUnvan.Visible = true;
            txtHesapKodu.Visible = true;
            txtEvrakno.Visible = true;
            if (gorevtip == 1)
            {
                this.Text = "Mal Kabulü";
                label5.Text = "Okutulan Miktar";
                txtRafBarkod.Visible = false;
                label7.Visible = false;
                label6.Visible = false;
                label12.Visible = false;
            }
            else if (gorevtip == 2)
            {
                this.Text = "Rafa Yerleştirme";
                label5.Text = "Raf";
                txtRafBarkod.Visible = true;
                label7.Visible = true;
                label6.Visible = true;
                label12.Visible = true;
            }
            else if (gorevtip == 3)
            {
                this.Text = "Sipariş Toplama";
                label5.Text = "Raf";
                txtRafBarkod.Visible = true;
                label7.Visible = true;
                label6.Visible = true;
                label12.Visible = true;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                txtUnvan.Visible = false;
                txtHesapKodu.Visible = false;
                txtEvrakno.Visible = false;
            }
            else if (gorevtip == 6)
            {
                this.Text = "Paketle";
                label5.Text = "Okutulan Miktar";
                txtRafBarkod.Visible = false;
                label7.Visible = false;
                label6.Visible = false;
                label12.Visible = false;
            }
            else if (gorevtip == 7)
            {
                this.Text = "Sevkiyat";
                label5.Text = "Okutulan Miktar";
                txtRafBarkod.Visible = false;
                label7.Visible = false;
                label6.Visible = false;
                label12.Visible = false;
            }
            else if (gorevtip == 8)
            {
                this.Text = "Kontrollü Sayım";
                label5.Text = "Raf";
                label6.Text = "Okutulan Miktar";
                txtRafBarkod.Visible = true;
                label7.Visible = true;
                label6.Visible = true;
                label12.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                txtUnvan.Visible = false;
                txtHesapKodu.Visible = false;
                txtEvrakno.Visible = false;
            }
            else// if (gorevtip == 19 || gorevtip == 20)
            {
                this.Text = "Transfer";
                label5.Text = "Raf";
                txtRafBarkod.Visible = true;
                label7.Visible = true;
                label6.Visible = true;
                label12.Visible = true;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                txtUnvan.Visible = false;
                txtHesapKodu.Visible = false;
                txtEvrakno.Visible = false;
            }
            //end
            txtRafBarkod.Focus();
        }
        /// <summary>
        /// barkod okursa
        /// </summary>
        public delegate void MethodInvoker();
        void Barkod_OnScan(Symbol.Barcode2.ScanDataCollection scanDataCollection)
        {
            this.Invoke((MethodInvoker)delegate()
            {
                string okunan = scanDataCollection.GetFirst.Text;
                if (okunan.Length < 13)
                {
                    txtRafBarkod.Text = okunan;
                    txtBarkod.Focus();
                }
                else
                {
                    txtBarkod.Text = okunan;
                    if (txtRafBarkod.Visible == true && txtRafBarkod.Text == "") txtRafBarkod.Focus();
                    else btnUygula_Click(Barkod, null);//uygula
                }
            });
        }
        /// <summary>
        /// irsaliye detaylarını getir
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
            FocusPanelName = panel.Name;
            foreach (var itemPanel in PanelVeriList)
            {
                foreach (Control item in itemPanel.Controls)
                    item.BackColor = Color.FromArgb(206, 223, 239);
            }
            foreach (Control itemSecili in panel.Controls)
                itemSecili.BackColor = Color.DarkOrange;
        }
        /// <summary>
        /// textbox focusta selectall yap
        /// </summary>
        private void txt_GotFocus(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
        /// <summary>
        /// bir tane okur, malın bulunduğu satırda miktarı bir arttırır
        /// </summary>
        private void btnUygula_Click(object sender, EventArgs e)
        {
            string mal = txtBarkod.Text;
            if (mal == "")
            {
                Mesaj.Hata(null, "Malzemeyi okutun");
                txtBarkod.Focus();
                return;
            }
            string raf = txtRafBarkod.Text;
            if (raf == "" && txtRafBarkod.Visible == true)
            {
                Mesaj.Hata(null, "Rafı okutun");
                txtRafBarkod.Focus();
                return;
            }
            bool mal_var = false;
            bool raf_var = false;
            Tip_STI temp_sti = new Tip_STI();
            //tüm sayırları eski rengine döndür
            foreach (var itemPanel in PanelVeriList)
            {
                foreach (Control item in itemPanel.Controls)
                    item.BackColor = Color.FromArgb(206, 223, 239);
            }
            foreach (var itemPanel in PanelVeriList)
            {
                //mal kabul ise malın bulunduğu satırdaki miktarı bir artırıyor, bir de satırı turuncuya boyuyor
                if (Ayarlar.MenuTip == MenuType.MalKabul || Ayarlar.MenuTip == MenuType.Paketle || Ayarlar.MenuTip == MenuType.Sevkiyat || Ayarlar.MenuTip == MenuType.KontrollüSayım)
                {
                    if (itemPanel.Controls[0].Text == mal)
                    {
                        //eğer kontrollü sayım ise rafı da doğru olmalı ki sayıyı arttırsın
                        if (Ayarlar.MenuTip == MenuType.KontrollüSayım)
                        {
                            if (itemPanel.Controls[5].Text == raf)
                            {
                                raf_var = true;
                                itemPanel.Controls[5].Text = raf;
                                itemPanel.Controls[6].Text = (sender == btnUygula) ? itemPanel.Controls[3].Text : (itemPanel.Controls[6].Text.ToDecimal() + 1).ToString();//okutulan miktar farklı sütunda olduğu için burada yazdım kontorllü sayımı
                                foreach (Control item in itemPanel.Controls)
                                    item.BackColor = Color.DarkOrange;
                            }
                        }//diğer görevlerde sadece sayıyı arttır
                        else
                        {
                            itemPanel.Controls[5].Text = (itemPanel.Controls[5].Text.ToDecimal() + 1).ToString();
                            foreach (Control item in itemPanel.Controls)
                                item.BackColor = Color.DarkOrange;
                        }
                    }
                }
                else if (Ayarlar.MenuTip == MenuType.RafaYerlestirme || Ayarlar.MenuTip == MenuType.SiparisToplama || Ayarlar.MenuTip == MenuType.TransferÇıkış || Ayarlar.MenuTip == MenuType.TransferGiriş)
                {
                    if (itemPanel.Controls[0].Text == mal)
                    {
                        mal_var = true;
                        temp_sti.YerlestirmeMiktari = itemPanel.Controls[6].Text.ToDecimal();
                        temp_sti.Miktar = itemPanel.Controls[3].Text.ToDecimal();
                        temp_sti.Barkod = itemPanel.Controls[0].Text;
                        temp_sti.MalKodu = itemPanel.Controls[1].Text;
                        temp_sti.Raf = raf;
                        temp_sti.Birim = itemPanel.Controls[4].Text;
                        temp_sti.MalAdi = itemPanel.Controls[2].Text;
                        temp_sti.ID = itemPanel.Controls[1].Tag.ToInt32();
                        if (itemPanel.Controls[5].Text == "" || itemPanel.Controls[5].Text == raf)
                        {
                            raf_var = true;
                            itemPanel.Controls[5].Text = raf;
                            itemPanel.Controls[7].Text = (sender == btnUygula) ? itemPanel.Controls[3].Text : (itemPanel.Controls[7].Text.ToDecimal() + 1).ToString();
                            foreach (Control item in itemPanel.Controls)
                                item.BackColor = Color.DarkOrange;
                        }
                    }
                }//end of if else
            }//end of foreach
            //kontorllü sayımda sadece satır ekle
            if (Ayarlar.MenuTip == MenuType.KontrollüSayım && raf_var == false)
            {
                var malbilgileri = Servis.GetMalzemeFromBarcode("", mal, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                Sayac++;
                panelOrta.AutoScrollPosition = new Point(0, 0);

                Font font = new Font("Tahoma", 8, FontStyle.Regular);
                PanelEx panelSatir = new PanelEx();
                panelSatir.Name = Sayac.ToString();
                panelSatir.Size = new Size((500 * carpim).ToInt32(), (20 * carpim).ToInt32());
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
                tMiktar.TextAlign = HorizontalAlignment.Right;
                tMiktar.ReadOnly = true;
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
                tRaf.Font = font;
                tRaf.Width = (92 * carpim).ToInt32();
                tRaf.Location = new Point((284 * carpim).ToInt32(), 0);
                tRaf.ReadOnly = true;
                tRaf.Name = "txtRaf";
                tRaf.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                TextBox tYerlestirmeMiktari = new TextBox();
                tYerlestirmeMiktari.Font = font;
                tYerlestirmeMiktari.Width = (105 * carpim).ToInt32();
                tYerlestirmeMiktari.Location = new Point((377 * carpim).ToInt32(), 0);
                tYerlestirmeMiktari.Visible = true;
                tYerlestirmeMiktari.Name = "txtYerlestirmeMiktari";
                tYerlestirmeMiktari.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                tMalKodu.BackColor = Color.DarkOrange;
                tMiktar.BackColor = Color.DarkOrange;
                tBirim.BackColor = Color.DarkOrange;
                tMalAdi.BackColor = Color.DarkOrange;
                tRaf.BackColor = Color.DarkOrange;
                tYerlestirmeMiktari.BackColor = Color.DarkOrange;

                tMiktar.Text = "0";
                tBarkod.Text = mal;
                tMalKodu.Text = malbilgileri.MalKodu;
                tBirim.Text = malbilgileri.Birim;
                tMalAdi.Text = malbilgileri.MalAdi;
                tRaf.Text = raf;
                tYerlestirmeMiktari.Text = "1";

                tMalKodu.Tag = "0";

                panelSatir.Barkod = tBarkod.Text;
                panelSatir.MalAdi = tMalAdi.Text;
                panelSatir.MalKodu = tMalKodu.Text;
                panelSatir.Miktar = 0;
                panelSatir.Birim = malbilgileri.Birim;
                panelSatir.YerlestirmeMiktari = (sender == btnUygula) ? tMiktar.Text.ToDecimal() : temp_sti.Miktar;
                panelSatir.Raf = raf;

                panelSatir.Controls.Add(tBarkod);
                panelSatir.Controls.Add(tMalKodu);
                panelSatir.Controls.Add(tMalAdi);
                panelSatir.Controls.Add(tMiktar);
                panelSatir.Controls.Add(tBirim);
                panelSatir.Controls.Add(tRaf);
                panelSatir.Controls.Add(tYerlestirmeMiktari);

                panelOrta.Controls.Add(panelSatir);
                PanelVeriList.Add(panelSatir);
            }
            //bunlarda da aynı maldan yeni raf ekle
            else if (Ayarlar.MenuTip == MenuType.RafaYerlestirme || Ayarlar.MenuTip == MenuType.TransferGiriş)
            {
                if (raf_var)
                    return;
                else if (mal_var)
                {
                    Sayac++;
                    panelOrta.AutoScrollPosition = new Point(0, 0);

                    Font font = new Font("Tahoma", 8, FontStyle.Regular);
                    PanelEx panelSatir = new PanelEx();
                    panelSatir.Name = Sayac.ToString();
                    panelSatir.Size = new Size((550 * carpim).ToInt32(), (20 * carpim).ToInt32());
                    panelSatir.Location = new Point(1, (Sayac * 21));

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
                    tMiktar.TextAlign = HorizontalAlignment.Right;
                    tMiktar.ReadOnly = true;
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
                    tRaf.Font = font;
                    tRaf.Width = (92 * carpim).ToInt32();
                    tRaf.Location = new Point((284 * carpim).ToInt32(), 0);
                    tRaf.ReadOnly = true;
                    tRaf.Name = "txtRaf";
                    tRaf.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                    TextBox tYerlestirmeMiktari = new TextBox();
                    tYerlestirmeMiktari.Font = font;
                    tYerlestirmeMiktari.Width = (105 * carpim).ToInt32();
                    tYerlestirmeMiktari.Location = new Point((377 * carpim).ToInt32(), 0);
                    tYerlestirmeMiktari.ReadOnly = true;
                    tYerlestirmeMiktari.Visible = true;
                    tYerlestirmeMiktari.Name = "txtYerlestirmeMiktari";
                    tYerlestirmeMiktari.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                    TextBox tIslemMiktar = new TextBox();
                    tIslemMiktar.Font = font;
                    tIslemMiktar.Width = (70 * carpim).ToInt32();
                    tIslemMiktar.Location = new Point((483 * carpim).ToInt32(), 0);
                    tIslemMiktar.ReadOnly = false;
                    tIslemMiktar.Visible = true;
                    tIslemMiktar.Name = "txtIslemMiktar";
                    tIslemMiktar.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                    tMalKodu.BackColor = Color.DarkOrange;
                    tMiktar.BackColor = Color.DarkOrange;
                    tBirim.BackColor = Color.DarkOrange;
                    tMalAdi.BackColor = Color.DarkOrange;
                    tRaf.BackColor = Color.DarkOrange;
                    tYerlestirmeMiktari.BackColor = Color.DarkOrange;
                    tIslemMiktar.BackColor = Color.DarkOrange;

                    tMiktar.Text = temp_sti.Miktar.ToString("N2");
                    tBarkod.Text = temp_sti.Barkod;
                    tMalKodu.Text = temp_sti.MalKodu;
                    tBirim.Text = temp_sti.Birim;
                    tMalAdi.Text = temp_sti.MalAdi;
                    tRaf.Text = temp_sti.Raf;
                    tYerlestirmeMiktari.Text = tMiktar.Text;
                    tIslemMiktar.Text = (sender == btnUygula) ? temp_sti.Miktar.ToString("N2") : "1";

                    tMalKodu.Tag = temp_sti.ID.ToInt32();

                    panelSatir.Barkod = temp_sti.Barkod;
                    panelSatir.MalAdi = temp_sti.MalAdi;
                    panelSatir.MalKodu = temp_sti.MalKodu;
                    panelSatir.Miktar = temp_sti.Miktar;
                    panelSatir.Birim = temp_sti.Birim;
                    panelSatir.IslemMiktar = temp_sti.YerlestirmeMiktari;
                    panelSatir.YerlestirmeMiktari = (sender == btnUygula) ? tMiktar.Text.ToInt32() : 1;
                    panelSatir.Raf = temp_sti.Raf;

                    panelSatir.Controls.Add(tBarkod);
                    panelSatir.Controls.Add(tMalKodu);
                    panelSatir.Controls.Add(tMalAdi);
                    panelSatir.Controls.Add(tMiktar);
                    panelSatir.Controls.Add(tBirim);
                    panelSatir.Controls.Add(tRaf);
                    panelSatir.Controls.Add(tYerlestirmeMiktari);
                    panelSatir.Controls.Add(tIslemMiktar);

                    panelOrta.Controls.Add(panelSatir);
                    PanelVeriList.Add(panelSatir);
                }
                else
                    Mesaj.Uyari("Göreve ait böyle bir MalKodu bulunmamaktadır.");
            }
            else if (!mal_var)
                Mesaj.Uyari("Göreve ait böyle bir MalKodu bulunmamaktadır.");
        }
        /// <summary>
        /// veritabanına kaydeder
        /// </summary>
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var StiList = new List<frmMalKabul>();
            var YerList = new List<frmYerlesme>();
            var Sonuc = new Result();
            foreach (var itemPanel in PanelVeriList)
            {
                var sti = new frmMalKabul();
                var yer = new frmYerlesme();
                if (Ayarlar.MenuTip == MenuType.MalKabul || Ayarlar.MenuTip == MenuType.Paketle || Ayarlar.MenuTip == MenuType.Sevkiyat)
                {
                    sti.OkutulanMiktar = itemPanel.Controls[5].Text.ToDecimal();
                    sti.ID = itemPanel.Controls[1].Tag.ToInt32();
                    StiList.Add(sti);
                }
                else if (Ayarlar.MenuTip == MenuType.KontrollüSayım)
                {
                    if (itemPanel.Controls[6].Text.ToDecimal() > 0)
                    {
                        yer.MalKodu = itemPanel.Controls[1].Text;
                        yer.Miktar = itemPanel.Controls[6].Text.ToDecimal();
                        yer.Birim = itemPanel.Controls[4].Text;
                        yer.DepoID = Ayarlar.Kullanici.DepoID;
                        yer.IrsDetayID = itemPanel.Controls[1].Tag.ToInt32();
                        yer.IrsID = txtEvrakno.Tag.ToInt32();
                        yer.RafNo = itemPanel.Controls[5].Text;
                        yer.GorevID = GorevID;
                        YerList.Add(yer);
                    }
                }
                else if (Ayarlar.MenuTip == MenuType.RafaYerlestirme || Ayarlar.MenuTip == MenuType.SiparisToplama || Ayarlar.MenuTip == MenuType.TransferÇıkış || Ayarlar.MenuTip == MenuType.TransferGiriş)
                {
                    if (itemPanel.Controls[7].Text.ToDecimal() > 0)
                    {
                        yer.MalKodu = itemPanel.Controls[1].Text;
                        yer.Miktar = itemPanel.Controls[7].Text.ToDecimal();
                        yer.Birim = itemPanel.Controls[4].Text;
                        yer.DepoID = Ayarlar.Kullanici.DepoID;
                        yer.IrsDetayID = itemPanel.Controls[1].Tag.ToInt32();
                        yer.IrsID = txtEvrakno.Tag.ToInt32();
                        yer.RafNo = itemPanel.Controls[5].Text;
                        yer.GorevID = GorevID;
                        YerList.Add(yer);
                    }
                }

            }
            //servise gönder
            if (Ayarlar.MenuTip == MenuType.MalKabul)
                Sonuc = Servis.Mal_Kabul(StiList.ToArray(), GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
            else if (Ayarlar.MenuTip == MenuType.RafaYerlestirme || Ayarlar.MenuTip == MenuType.TransferGiriş)
                Sonuc = Servis.Rafa_Kaldir(YerList.ToArray(), Ayarlar.Kullanici.ID, GorevID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
            else if (Ayarlar.MenuTip == MenuType.SiparisToplama || Ayarlar.MenuTip == MenuType.TransferÇıkış)
                Sonuc = Servis.Siparis_Topla(YerList.ToArray(), Ayarlar.Kullanici.ID, GorevID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
            else if (Ayarlar.MenuTip == MenuType.Paketle || Ayarlar.MenuTip == MenuType.Sevkiyat)
                Sonuc = Servis.Paketle(StiList.ToArray(), GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
            else if (Ayarlar.MenuTip == MenuType.KontrollüSayım)
                Sonuc = Servis.Kontrollu_Say(YerList.ToArray(), GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
            //sonuç işlemleri
            if (Sonuc.Status == false)
                Mesaj.Uyari(Sonuc.Message);
            else
                Mesaj.Basari("Kayıt tamamlandı");
            //sayfayı yenile
            Ayarlar.STIKalemler = new List<Tip_STI>(Servis.GetMalzemes(GorevID, Ayarlar.Kullanici.ID, glbTip, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid));
            if (Ayarlar.STIKalemler.Count == 0) this.Close();
            STIGetir();
            txtBarkod.Text = "";
            txtRafBarkod.Text = "";
        }
        /// <summary>
        /// form kapanırken dispose yap
        /// </summary>
        private void MalzemeIslemleri_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                Servis.Dispose();
                Barkod.EnableScanner = false;
                Barkod.Dispose();
            }
            catch { }
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