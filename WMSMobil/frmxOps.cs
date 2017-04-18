using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WMSMobil.WMSLocal;
using Symbol.Barcode2.Design;

namespace WMSMobil
{
    public partial class frmxOps : Form
    {
        MobilServis Servis = new MobilServis();
        private Barcode2 Barkod;
        bool glbTip;
        int GorevID, IrsaliyeID;
        string FocusPanelName = "";
        int Sayac = 0;
        List<PanelEx> PanelVeriList = new List<PanelEx>();
        /// <summary>
        /// form load
        /// </summary>
        public frmxOps(int grvId, int irsID, bool tip, int gorevtip)
        {
            InitializeComponent();
            try
            {
                Ayarlar.STIKalemler = new List<Tip_STI>(Servis.GetMalzemes(grvId, tip));
                Ayarlar.SeciliGorev = Servis.GetIrsaliye(grvId);
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
                this.Close();
            }
            //gizle göster
            glbTip = tip;
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
            else// if (gorevtip == 19)
            {
                this.Text = "Transfer";
                label5.Text = "Raf";
                txtRafBarkod.Visible = true;
                label7.Visible = true;
                label6.Visible = true;
                label12.Visible = true;
                label1.Visible = false;
                txtUnvan.Visible = false;
            }
            //barkod
            //Barkod = new Barcode2();
            //Barkod.DeviceType = Symbol.Barcode2.DEVICETYPES.FIRSTAVAILABLE;
            //Barkod.EnableScanner = true;
            //Barkod.OnScan += new Barcode2.OnScanEventHandler(Barkod_OnScan);
            //end
            txtBarkod.Focus();
        }
        /// <summary>
        /// barkod okursa
        /// </summary>
        public delegate void MethodInvoker();
        void Barkod_OnScan(Symbol.Barcode2.ScanDataCollection scanDataCollection)
        {
            this.Invoke((MethodInvoker)delegate()
            {
                try
                {
                    bool focuslandi = false;
                    foreach (Control cont in panelUst.Controls)
                    {
                        if (cont.Focused)
                        {
                            //eğer odaklanana yer barkod ise barkoda göre malkodunu getir
                            if (cont.Name == txtBarkod.Name)
                            {
                                focuslandi = true;
                                txtBarkod.Text = Servis.GetMalzemeFromBarcode(scanDataCollection.GetFirst.Text);
                                txtRafBarkod.Focus();
                            }
                            //eğer raf ise direk yaz
                            else if (cont.Name == txtRafBarkod.Name)
                            {
                                focuslandi = true;
                                txtRafBarkod.Text = scanDataCollection.GetFirst.Text;
                                //rafı da okutursa yerleştir düğmesine bas
                                btnUygula_Click(Barkod, null);
                                //sonra tekrar malkoduna odaklan
                                txtBarkod.Focus();
                            }
                        }
                    }
                    //eğer hiç bir yere odaklanmamışsa
                    if (!focuslandi)
                    {
                        Mesaj.Uyari("Lütfen önce bir yazı kutusuna tıklayın!");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Mesaj.Hata(ex);
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
                panelSatir.Location = new Point(1, (Sayac * 21));

                TextBox tMalKodu = new TextBox();
                tMalKodu.Font = font;
                tMalKodu.Width = 60;
                tMalKodu.Location = new Point(3, 0);
                tMalKodu.ReadOnly = true;
                tMalKodu.Name = "txtMalKodu";
                tMalKodu.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                TextBox tMiktar = new TextBox();
                tMiktar.Font = font;
                tMiktar.Width = 65;
                tMiktar.Location = new Point(145, 0);
                tMiktar.TextAlign = HorizontalAlignment.Right;
                tMiktar.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                TextBox tBirim = new TextBox();
                tBirim.Font = font;
                tBirim.Width = 72;
                tBirim.Location = new Point(211, 0);
                tBirim.ReadOnly = true;
                tBirim.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                TextBox tMalAdi = new TextBox();
                tMalAdi.Font = font;
                tMalAdi.Width = 80;
                tMalAdi.Location = new Point(64, 0);
                tMalAdi.ReadOnly = true;
                tMalAdi.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                TextBox tRaf = new TextBox();
                TextBox tYerlestirmeMiktari = new TextBox();
                TextBox tMiktarOkutulan = new TextBox();
                TextBox tIslemMiktar = new TextBox();
                if (Ayarlar.MenuTip == MenuType.MalKabul || Ayarlar.MenuTip == MenuType.Paketle || Ayarlar.MenuTip == MenuType.Sevkiyat)
                {

                    tMiktarOkutulan.Font = font;
                    tMiktarOkutulan.Width = 92;
                    tMiktarOkutulan.Location = new Point(284, 0);
                    tMiktarOkutulan.ReadOnly = false;
                    tMiktarOkutulan.Name = "txtOkutulanMiktar";
                    tMiktarOkutulan.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                    tMiktarOkutulan.BackColor = Color.FromArgb(206, 223, 239);
                    tMiktarOkutulan.Text = stiItem != null ? stiItem.OkutulanMiktar.ToDecimal().ToString("N2") : "0";
                    panelSatir.OkutulanMiktar = stiItem.OkutulanMiktar.ToDecimal();
                    tMiktar.Text = stiItem.Miktar.ToDecimal().ToString("N2");
                    tMiktar.Tag = stiItem.Miktar.ToDecimal();
                }
                else if (Ayarlar.MenuTip == MenuType.RafaYerlestirme || Ayarlar.MenuTip == MenuType.SiparisToplama || Ayarlar.MenuTip == MenuType.TransferÇıkış || Ayarlar.MenuTip == MenuType.TransferGiriş)
                {

                    tRaf.Font = font;
                    tRaf.Width = 92;
                    tRaf.Location = new Point(284, 0);
                    tRaf.ReadOnly = true;
                    tRaf.Name = "txtRaf";
                    tRaf.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                    tRaf.BackColor = Color.FromArgb(206, 223, 239);
                    tRaf.Text = stiItem.Raf != null ? stiItem.Raf : "";
                    panelSatir.Raf = stiItem.Raf != null ? stiItem.Raf : "";

                    tYerlestirmeMiktari.Font = font;
                    tYerlestirmeMiktari.Width = 105;
                    tYerlestirmeMiktari.Location = new Point(377, 0);
                    tYerlestirmeMiktari.ReadOnly = true;
                    tYerlestirmeMiktari.Visible = true;
                    tYerlestirmeMiktari.Name = "txtYerlestirmeMiktari";
                    tYerlestirmeMiktari.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                    tYerlestirmeMiktari.BackColor = Color.FromArgb(206, 223, 239);
                    tYerlestirmeMiktari.Text = stiItem.YerlestirmeMiktari.ToDecimal().ToString("N2");
                    panelSatir.YerlestirmeMiktari = stiItem.YerlestirmeMiktari.ToDecimal();

                    tIslemMiktar.Font = font;
                    tIslemMiktar.Width = 70;
                    tIslemMiktar.Location = new Point(483, 0);
                    tIslemMiktar.ReadOnly = false;
                    tIslemMiktar.Visible = true;
                    tIslemMiktar.Name = "txtIslemMiktar";
                    tIslemMiktar.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                    tIslemMiktar.BackColor = Color.FromArgb(206, 223, 239);
                    tIslemMiktar.Text = "0";
                    panelSatir.IslemMiktar = 0;
                    tMiktar.Text = stiItem.Miktar.ToDecimal().ToString("N2");
                    tMiktar.Tag = stiItem.Miktar.ToDecimal();
                }
                tMalKodu.BackColor = Color.FromArgb(206, 223, 239);
                tMiktar.BackColor = Color.FromArgb(206, 223, 239);
                tBirim.BackColor = Color.FromArgb(206, 223, 239);
                tMalAdi.BackColor = Color.FromArgb(206, 223, 239);
                tMalKodu.Text = stiItem.MalKodu;
                tBirim.Text = stiItem.Birim;
                tMalAdi.Text = stiItem.MalAdi;
                tMalKodu.Tag = stiItem.ID.ToInt32();
                panelSatir.MalAdi = stiItem.MalAdi;
                panelSatir.MalKodu = stiItem.MalKodu;
                panelSatir.Miktar = stiItem.Miktar;
                panelSatir.Birim = stiItem.Birim;
                panelSatir.Controls.Add(tMalKodu);
                panelSatir.Controls.Add(tMalAdi);
                panelSatir.Controls.Add(tMiktar);
                panelSatir.Controls.Add(tBirim);
                if (Ayarlar.MenuTip == MenuType.MalKabul || Ayarlar.MenuTip == MenuType.Paketle || Ayarlar.MenuTip == MenuType.Sevkiyat)
                {
                    panelSatir.Size = new Size(400, 22);
                    panelSatir.Controls.Add(tMiktarOkutulan);
                }
                else if (Ayarlar.MenuTip == MenuType.RafaYerlestirme || Ayarlar.MenuTip == MenuType.SiparisToplama || Ayarlar.MenuTip == MenuType.TransferÇıkış || Ayarlar.MenuTip == MenuType.TransferGiriş)
                {
                    panelSatir.Size = new Size(550, 22);
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
        /// bir tane okur, malın bulunduğu satırda omiktarı bir arttırır
        /// </summary>
        private void btnUygula_Click(object sender, EventArgs e)
        {            
            string mal = txtBarkod.Text;
            string raf = txtRafBarkod.Text;
            //if (mal=="")
            //{
            //    Mesaj.Hata("Malkodu okutun");
            //    txtBarkod.Focus();
            //    return;
            //}
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
                if (Ayarlar.MenuTip == MenuType.MalKabul || Ayarlar.MenuTip == MenuType.Paketle || Ayarlar.MenuTip == MenuType.Sevkiyat)
                {
                    if (itemPanel.Controls[0].Text == mal)
                    {
                        itemPanel.Controls[4].Text = (itemPanel.Controls[4].Text.ToDecimal() + 1).ToString();
                        foreach (Control item in itemPanel.Controls)
                            item.BackColor = Color.DarkOrange;
                    }
                }
                else if (Ayarlar.MenuTip == MenuType.RafaYerlestirme || Ayarlar.MenuTip == MenuType.SiparisToplama || Ayarlar.MenuTip == MenuType.TransferÇıkış || Ayarlar.MenuTip == MenuType.TransferGiriş)
                {
                    if (itemPanel.Controls[0].Text == mal)
                    {
                        mal_var = true;
                        temp_sti.YerlestirmeMiktari = itemPanel.Controls[5].Text.ToDecimal();
                        temp_sti.Miktar = itemPanel.Controls[2].Text.ToDecimal();
                        temp_sti.MalKodu = itemPanel.Controls[0].Text;
                        temp_sti.Raf = raf;
                        temp_sti.Birim = itemPanel.Controls[3].Text;
                        temp_sti.MalAdi = itemPanel.Controls[1].Text;
                        temp_sti.ID = itemPanel.Controls[0].Tag.ToInt32();
                        if (((itemPanel.Controls[4].Text == "") || (itemPanel.Controls[4].Text == raf)))
                        {
                            raf_var = true;
                            itemPanel.Controls[4].Text = raf;
                            itemPanel.Controls[6].Text = (itemPanel.Controls[6].Text.ToDecimal() + 1).ToString();
                            foreach (Control item in itemPanel.Controls)
                                item.BackColor = Color.DarkOrange;
                        }
                    }
                }
            }
            if (Ayarlar.MenuTip == MenuType.RafaYerlestirme || Ayarlar.MenuTip == MenuType.SiparisToplama || Ayarlar.MenuTip == MenuType.TransferÇıkış || Ayarlar.MenuTip == MenuType.TransferGiriş)
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
                    panelSatir.Size = new Size(550, 22);
                    panelSatir.Location = new Point(1, (Sayac * 21));

                    TextBox tMalKodu = new TextBox();
                    tMalKodu.Font = font;
                    tMalKodu.Width = 60;
                    tMalKodu.Location = new Point(3, 0);
                    tMalKodu.ReadOnly = true;
                    tMalKodu.Name = "txtMalKodu";
                    tMalKodu.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                    TextBox tMiktar = new TextBox();
                    tMiktar.Font = font;
                    tMiktar.Width = 65;
                    tMiktar.Location = new Point(145, 0);
                    tMiktar.TextAlign = HorizontalAlignment.Right;
                    tMiktar.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                    TextBox tBirim = new TextBox();
                    tBirim.Font = font;
                    tBirim.Width = 72;
                    tBirim.Location = new Point(211, 0);
                    tBirim.ReadOnly = true;
                    tBirim.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                    TextBox tMalAdi = new TextBox();
                    tMalAdi.Font = font;
                    tMalAdi.Width = 80;
                    tMalAdi.Location = new Point(64, 0);
                    tMalAdi.ReadOnly = true;
                    tMalAdi.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                    TextBox tRaf = new TextBox();
                    tRaf.Font = font;
                    tRaf.Width = 92;
                    tRaf.Location = new Point(284, 0);
                    tRaf.ReadOnly = true;
                    tRaf.Name = "txtRaf";
                    tRaf.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                    TextBox tYerlestirmeMiktari = new TextBox();
                    tYerlestirmeMiktari.Font = font;
                    tYerlestirmeMiktari.Width = 105;
                    tYerlestirmeMiktari.Location = new Point(377, 0);
                    tYerlestirmeMiktari.ReadOnly = true;
                    tYerlestirmeMiktari.Visible = true;
                    tYerlestirmeMiktari.Name = "txtYerlestirmeMiktari";
                    tYerlestirmeMiktari.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                    TextBox tIslemMiktar = new TextBox();
                    tIslemMiktar.Font = font;
                    tIslemMiktar.Width = 70;
                    tIslemMiktar.Location = new Point(483, 0);
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
                    tMalKodu.Text = temp_sti.MalKodu;
                    tBirim.Text = temp_sti.Birim;
                    tMalAdi.Text = temp_sti.MalAdi;
                    tRaf.Text = temp_sti.Raf;
                    tYerlestirmeMiktari.Text = temp_sti.YerlestirmeMiktari.ToString("N2");
                    tIslemMiktar.Text = "1";

                    tMalKodu.Tag = temp_sti.ID.ToInt32();

                    panelSatir.MalAdi = temp_sti.MalAdi;
                    panelSatir.MalKodu = temp_sti.MalKodu;
                    panelSatir.Miktar = temp_sti.Miktar;
                    panelSatir.Birim = temp_sti.Birim;
                    panelSatir.IslemMiktar = 1;
                    panelSatir.YerlestirmeMiktari = temp_sti.YerlestirmeMiktari;
                    panelSatir.Raf = temp_sti.Raf;

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
                    sti.OkutulanMiktar = itemPanel.Controls[4].Text.ToDecimal();
                    sti.ID = itemPanel.Controls[0].Tag.ToInt32();
                    StiList.Add(sti);
                }
                else if (Ayarlar.MenuTip == MenuType.RafaYerlestirme || Ayarlar.MenuTip == MenuType.SiparisToplama || Ayarlar.MenuTip == MenuType.TransferÇıkış || Ayarlar.MenuTip == MenuType.TransferGiriş)
                {
                    if (itemPanel.Controls[6].Text.ToDecimal() > 0)
                    {
                        yer.MalKodu = itemPanel.Controls[0].Text;
                        yer.Miktar = itemPanel.Controls[6].Text.ToDecimal();
                        yer.Birim = itemPanel.Controls[3].Text;
                        yer.DepoID = Ayarlar.Kullanici.DepoID;
                        yer.IrsDetayID = itemPanel.Controls[0].Tag.ToInt32();
                        yer.IrsID = txtEvrakno.Tag.ToInt32();
                        yer.RafNo = itemPanel.Controls[4].Text;
                        yer.GorevID = GorevID;
                        YerList.Add(yer);
                    }
                }

            }

            if (Ayarlar.MenuTip == MenuType.MalKabul)
                Sonuc = Servis.Mal_Kabul(StiList.ToArray());
            else if (Ayarlar.MenuTip == MenuType.RafaYerlestirme)
                Sonuc = Servis.Rafa_Kaldir(YerList.ToArray(), Ayarlar.Kullanici.ID);
            else if (Ayarlar.MenuTip == MenuType.SiparisToplama || Ayarlar.MenuTip == MenuType.TransferÇıkış || Ayarlar.MenuTip == MenuType.TransferGiriş)
                Sonuc = Servis.Siparis_Topla(YerList.ToArray(), Ayarlar.Kullanici.ID);
            else if (Ayarlar.MenuTip == MenuType.Paketle)
                Sonuc = Servis.Paketle(StiList.ToArray());
            if (Sonuc.Status)
            {
                Ayarlar.STIKalemler = new List<Tip_STI>(Servis.GetMalzemes(GorevID, glbTip));
                if (Ayarlar.STIKalemler.Count == 0) this.Close();
                STIGetir();
                txtBarkod.Text = "";
                txtRafBarkod.Text = "";
            }
            else
                Mesaj.Uyari(Sonuc.Message);
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
    }
}