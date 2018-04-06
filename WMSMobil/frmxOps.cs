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
        private Barcode2 Barkod;
        bool glbTip;
        int GorevID, IrsaliyeID, GorevTip, Sayac = 0, makaranoColumnIndex, islemColumnIndex;
        string FocusPanelName = "";
        List<PanelEx> PanelVeriList = new List<PanelEx>();

        /// <summary>
        /// form load
        /// </summary>
        public frmxOps(int grvId, int irsID, bool tip, int gorevtip)
        {
            InitializeComponent();
            Cursor.Current = Cursors.WaitCursor;
            glbTip = tip;
            GorevTip = gorevtip;
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
            RearrangeColumnPlaces();
            try
            {
                //görev bilgilerini getir
                Ayarlar.SeciliGorev = Program.Servis.GetIrsaliye(grvId, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                txtUnvan.Text = Ayarlar.SeciliGorev.Unvan;
                txtHesapKodu.Text = Ayarlar.SeciliGorev.HesapKodu;
                txtEvrakno.Text = Ayarlar.SeciliGorev.EvrakNo;
                txtEvrakno.Tag = Ayarlar.SeciliGorev.ID;
                GorevID = grvId;
                IrsaliyeID = irsID;
                //ürün bilgilerini getir
                Ayarlar.STIKalemler = new List<Tip_STI>(Program.Servis.GetMalzemes(grvId, Ayarlar.Kullanici.ID, tip, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid));
                if (Ayarlar.STIKalemler.Count == 0 && gorevtip != 8)
                {
                    Cursor.Current = Cursors.Default;
                    if (Mesaj.Soru("Bu görevin işleri bitmiş. Tüm listeye bakmak istiyor musunuz?") == DialogResult.Yes)
                    {
                        glbTip = false;
                        Cursor.Current = Cursors.WaitCursor;
                        Ayarlar.STIKalemler = new List<Tip_STI>(Program.Servis.GetMalzemes(grvId, Ayarlar.Kullanici.ID, false, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid));
                    }
                    else
                        this.Close();
                }
            }
            catch (Exception ex)
            {
                Mesaj.Hata(ex);
            }
            //change size
            if (Screen.PrimaryScreen.Bounds.Height == Screen.PrimaryScreen.Bounds.Width)
            {
                int eksik = 320 - Screen.PrimaryScreen.WorkingArea.Height;
                panelOrta.Height -= eksik;
                panelAlt.Top -= eksik;
            }
            Cursor.Current = Cursors.Default;
            //end
            txtRafBarkod.Focus();
            //listele
            if (Ayarlar.MenuTip != MenuType.KontrollüSayım)
                STIGetir();
            else
            {
                islemColumnIndex = 7;
                makaranoColumnIndex = 8;
            }
        }

        /// <summary>
        /// irsaliye detaylarını gösterir
        /// </summary>
        private void STIGetir()
        {
            Cursor.Current = Cursors.WaitCursor;
            ClearPanelList();

            foreach (Tip_STI stiItem in Ayarlar.STIKalemler)
            {
                Sayac++;
                panelOrta.AutoScrollPosition = new Point(0, 0);

                Font font = new Font("Tahoma", 8, FontStyle.Regular);
                PanelEx panelSatir = new PanelEx();
                panelSatir.Name = Sayac.ToString();
                panelSatir.BackColor = Color.White;
                panelSatir.Location = new Point(0, (Sayac * 21).Carpim());

                TextBox tBarkod = new TextBox();
                tBarkod.Visible = false;
                tBarkod.Width = 3;
                tBarkod.Location = new Point(0, 0);
                tBarkod.ReadOnly = true;

                TextBox tMalKodu = new TextBox();
                tMalKodu.Font = font;
                tMalKodu.Width = lblMalkodu.Width;
                tMalKodu.Location = new Point(lblMalkodu.Left, 0);
                tMalKodu.ReadOnly = true;
                tMalKodu.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                TextBox tMalAdi = new TextBox();
                tMalAdi.Font = font;
                tMalAdi.Width = lblMalzeme.Width;
                tMalAdi.Location = new Point(lblMalzeme.Left, 0);
                tMalAdi.ReadOnly = true;
                tMalAdi.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                TextBox tMiktar = new TextBox();
                tMiktar.Font = font;
                tMiktar.Width = lblMiktar.Width;
                tMiktar.Location = new Point(lblMiktar.Left, 0);
                tMiktar.ReadOnly = true;
                tMiktar.TextAlign = HorizontalAlignment.Right;
                tMiktar.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                TextBox tBirim = new TextBox();
                tBirim.Font = font;
                tBirim.Width = lblBirim.Width;
                tBirim.Location = new Point(lblBirim.Left, 0);
                tBirim.ReadOnly = true;
                tBirim.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                TextBox tMakaraNo = new TextBox();
                tMakaraNo.Font = font;
                tMakaraNo.Width = lblMakarano.Width;
                tMakaraNo.Location = new Point(lblMakarano.Left, 0);
                tMakaraNo.ReadOnly = true;
                tMakaraNo.GotFocus += new EventHandler(TextBoxlar_GotFocus);

                TextBox tIslemMiktar = new TextBox();
                tIslemMiktar.Font = font;
                tIslemMiktar.Width = lblIslemMiktar.Width;
                tIslemMiktar.Location = new Point(lblIslemMiktar.Left, 0);
                tIslemMiktar.ReadOnly = false;
                tIslemMiktar.Visible = true;
                tIslemMiktar.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                tIslemMiktar.BackColor = Color.FromArgb(206, 223, 239);
                tIslemMiktar.Text = "0";
                panelSatir.IslemMiktar = 0;

                TextBox tRaf = new TextBox();
                TextBox tYerlestirmeMiktari = new TextBox();
                TextBox tMiktarOkutulan = new TextBox();
                if (Ayarlar.MenuTip == MenuType.MalKabul || Ayarlar.MenuTip == MenuType.Paketle || Ayarlar.MenuTip == MenuType.Sevkiyat || Ayarlar.MenuTip == MenuType.Satıştanİade)
                {
                    tMiktarOkutulan.Font = font;
                    tMiktarOkutulan.Width = lblOkutulanMiktar.Width;
                    tMiktarOkutulan.Location = new Point(lblOkutulanMiktar.Left, 0);
                    tMiktarOkutulan.ReadOnly = true;
                    tMiktarOkutulan.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                    tMiktarOkutulan.BackColor = Color.FromArgb(206, 223, 239);
                    tMiktarOkutulan.Text = stiItem != null ? stiItem.OkutulanMiktar.ToDecimal().ToString("N2") : "0";
                    panelSatir.OkutulanMiktar = stiItem.OkutulanMiktar.ToDecimal();
                }
                else if (Ayarlar.MenuTip == MenuType.KontrollüSayım)
                {
                    tRaf.Font = font;
                    tRaf.Width = lblOkutulanMiktar.Width;
                    tRaf.Location = new Point(lblOkutulanMiktar.Left, 0);
                    tRaf.ReadOnly = true;
                    tRaf.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                    tRaf.BackColor = Color.FromArgb(206, 223, 239);
                    tRaf.Text = stiItem.Raf != null ? stiItem.Raf : "";
                    panelSatir.Raf = stiItem.Raf != null ? stiItem.Raf : "";

                    tMiktarOkutulan.Font = font;
                    tMiktarOkutulan.Width = lblYerlestirmeMiktar.Width;
                    tMiktarOkutulan.Location = new Point(lblYerlestirmeMiktar.Left, 0);
                    tMiktarOkutulan.ReadOnly = true;
                    tMiktarOkutulan.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                    tMiktarOkutulan.BackColor = Color.FromArgb(206, 223, 239);
                    tMiktarOkutulan.Text = stiItem != null ? stiItem.YerlestirmeMiktari.ToDecimal().ToString("N2") : "0";
                    panelSatir.OkutulanMiktar = stiItem.YerlestirmeMiktari.ToDecimal();
                }
                else if (Ayarlar.MenuTip == MenuType.RafaYerlestirme || Ayarlar.MenuTip == MenuType.SiparisToplama || Ayarlar.MenuTip == MenuType.TransferÇıkış || Ayarlar.MenuTip == MenuType.TransferGiriş || Ayarlar.MenuTip == MenuType.Alımdanİade)
                {
                    tRaf.Font = font;
                    tRaf.Width = lblOkutulanMiktar.Width;
                    tRaf.Location = new Point(lblOkutulanMiktar.Left, 0);
                    tRaf.ReadOnly = true;
                    tRaf.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                    tRaf.BackColor = Color.FromArgb(206, 223, 239);
                    tRaf.Text = stiItem.Raf != null ? stiItem.Raf : "";
                    panelSatir.Raf = stiItem.Raf != null ? stiItem.Raf : "";

                    tYerlestirmeMiktari.Font = font;
                    tYerlestirmeMiktari.Width = lblYerlestirmeMiktar.Width;
                    tYerlestirmeMiktari.Location = new Point(lblYerlestirmeMiktar.Left, 0);
                    tYerlestirmeMiktari.ReadOnly = true;
                    tYerlestirmeMiktari.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                    tYerlestirmeMiktari.BackColor = Color.FromArgb(206, 223, 239);
                    tYerlestirmeMiktari.Text = stiItem.YerlestirmeMiktari.ToString("N2");
                    panelSatir.YerlestirmeMiktari = stiItem.YerlestirmeMiktari;
                }
                //renkler
                tMalKodu.BackColor = Color.FromArgb(206, 223, 239);
                tMiktar.BackColor = Color.FromArgb(206, 223, 239);
                tBirim.BackColor = Color.FromArgb(206, 223, 239);
                tMakaraNo.BackColor = Color.FromArgb(206, 223, 239);
                tMalAdi.BackColor = Color.FromArgb(206, 223, 239);
                //yazı ve tag
                tBarkod.Text = stiItem.Barkod;
                tMalKodu.Text = stiItem.MalKodu;
                tBirim.Text = stiItem.Birim;
                tMakaraNo.Text = stiItem.MakaraNo;
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
                panelSatir.MakaraNo = stiItem.MakaraNo;
                //add controls
                panelSatir.Controls.Add(tBarkod);//0
                panelSatir.Controls.Add(tMalKodu);//1
                panelSatir.Controls.Add(tMalAdi);//2
                panelSatir.Controls.Add(tMiktar);//3
                panelSatir.Controls.Add(tBirim);//4
                //raf ve mikar kolnlarını ekle
                if (Ayarlar.MenuTip == MenuType.MalKabul || Ayarlar.MenuTip == MenuType.Paketle || Ayarlar.MenuTip == MenuType.Sevkiyat || Ayarlar.MenuTip == MenuType.Satıştanİade)
                {
                    panelSatir.Size = new Size(525.Carpim(), 21.Carpim());
                    panelSatir.Controls.Add(tMiktarOkutulan);//5
                    islemColumnIndex = 6;
                    makaranoColumnIndex = 7;
                }
                else if (Ayarlar.MenuTip == MenuType.KontrollüSayım)
                {
                    panelSatir.Size = new Size(630.Carpim(), 21.Carpim());
                    panelSatir.Controls.Add(tRaf);//5
                    panelSatir.Controls.Add(tMiktarOkutulan);//6
                    islemColumnIndex = 7;
                    makaranoColumnIndex = 8;
                }
                else if (Ayarlar.MenuTip == MenuType.RafaYerlestirme || Ayarlar.MenuTip == MenuType.SiparisToplama || Ayarlar.MenuTip == MenuType.TransferÇıkış || Ayarlar.MenuTip == MenuType.TransferGiriş || Ayarlar.MenuTip == MenuType.Alımdanİade)
                {
                    panelSatir.Size = new Size(630.Carpim(), 21.Carpim());
                    panelSatir.Controls.Add(tRaf);//5
                    panelSatir.Controls.Add(tYerlestirmeMiktari);//6
                    islemColumnIndex = 7;
                    makaranoColumnIndex = 8;
                }
                panelSatir.Controls.Add(tIslemMiktar);
                panelSatir.Controls.Add(tMakaraNo);
                panelOrta.Controls.Add(panelSatir);
                PanelVeriList.Add(panelSatir);
            }
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// bir tane okur, malın bulunduğu satırda miktarı bir arttırır
        /// </summary>
        private void btnUygula_Click(object sender, EventArgs e)
        {
            int seciliSatırSayisi = 0, seciliSatirID = 0;
            string rowID = ";";
            string mal = txtBarkod.Text;
            string raf = txtRafBarkod.Text.ToUpper();
            string makaraNo = txtMakaraBarkod.Text.ToUpper();
            bool mal_var = false;
            Tip_STI temp_sti = new Tip_STI();
            //barkod kontrol
            var malInfo = Program.Servis.GetMalzemeFromBarcode("", mal, GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
            //genel kontroller
            if (!btnUygulaControl(malInfo)) return;
            //aynı mal veya raftan birden fazla var mı diye kontrol eder
            foreach (var itemPanel in PanelVeriList)
            {
                //tüm satırları normal renge getirir
                SatirRenkDegistir(itemPanel.Controls, Color.FromArgb(206, 223, 239));
                //eğer satırdaki malkodu aynıysa
                if (itemPanel.Controls[0].Text.Contains(";" + mal + ";"))
                {
                    //listede bu ürün var
                    temp_sti.Barkod = itemPanel.Controls[0].Text;
                    temp_sti.MalKodu = itemPanel.Controls[1].Text;
                    temp_sti.ID = itemPanel.Controls[1].Tag.ToInt32();
                    temp_sti.MalAdi = itemPanel.Controls[2].Text;
                    temp_sti.Miktar = itemPanel.Controls[3].Text.ToDecimal();
                    temp_sti.Birim = itemPanel.Controls[4].Text;
                    temp_sti.YerlestirmeMiktari = itemPanel.Controls[6].Text.ToDecimal();
                    temp_sti.Raf = raf;
                    temp_sti.MakaraNo = makaraNo;
                    //eğer makara no girilmişse makara noyu eşleştir
                    if (txtMakaraBarkod.Visible == true)
                    {
                        if (itemPanel.Controls[makaranoColumnIndex].Text.ToUpper() == makaraNo)
                        {
                            mal_var = true;
                            //hem makara hem af açıksa rafa da bak. raf aynı deilse satırı geç
                            if (txtRafBarkod.Visible == true)
                            {
                                if (itemPanel.Controls[5].Text.ToUpper() != raf && itemPanel.Controls[5].Text.ToUpper() != "")
                                {
                                    continue;
                                }
                            }
                            rowID += itemPanel.Controls[1].Tag.ToString() + ";";
                            seciliSatirID = itemPanel.Name.ToInt32();
                            seciliSatırSayisi++;
                        }
                    }
                    //eğer raf girilmişse rafı eşleştir
                    else if (txtRafBarkod.Visible == true)
                    {
                        if (itemPanel.Controls[5].Text.ToUpper() == raf || itemPanel.Controls[5].Text.ToUpper() == "")
                        {
                            mal_var = true;
                            rowID += itemPanel.Controls[1].Tag.ToString() + ";";
                            seciliSatirID = itemPanel.Name.ToInt32();
                            seciliSatırSayisi++;
                        }
                    }
                    //sadece malkodu ise eşleştir
                    else 
                    {
                        mal_var = true;
                        rowID += itemPanel.Controls[1].Tag.ToString() + ";";
                        seciliSatirID = itemPanel.Name.ToInt32();
                        seciliSatırSayisi++;
                    }
                }
            }
            //malkodu, raf ve makara noya göre seçilebilecek satır sayısını bulduk
            //satır bulunamadı ise ya mesaj verecek ya da yeni ekleyecek
            if (seciliSatırSayisi == 0)
            {
                //listede yoksa yeni satır eklenecek
                if (Ayarlar.MenuTip == MenuType.KontrollüSayım)
                {
                    CreateListeSatirSayim(malInfo, raf);
                }
                //yeni satır ekle
                else if (mal_var == true && (Ayarlar.MenuTip == MenuType.RafaYerlestirme || Ayarlar.MenuTip == MenuType.TransferGiriş || Ayarlar.MenuTip == MenuType.SiparisToplama || Ayarlar.MenuTip == MenuType.Alımdanİade))
                {
                    CreateListeSatirRaf(malInfo, temp_sti, (sender == btnUygula) ? temp_sti.Miktar.ToInt32() : 1);
                }
                //mal bulunamadı mesajı verecek
                else
                    Mesaj.Uyari("Göreve ait böyle bir ürün bulunmamaktadır.");
            }
            else
            {
                //seçilebilecek satır birden fazlaysa önce hangisi olduğunu belirle
                if (seciliSatırSayisi > 1)
                {
                    Ayarlar.SeciliSatırID = 0;
                    frmxOpsSelect frm = new frmxOpsSelect(GorevID, malInfo.MalKodu, rowID);
                    var sonuc = frm.ShowDialog();
                    if (Ayarlar.SeciliSatırID == 0)
                    {
                        Mesaj.Uyari("Göreve ait böyle bir ürün bulunmamaktadır.");
                        return;
                    }
                    //seçili satırdaki ID
                    foreach (var itemPanel in PanelVeriList)
                    {
                        if (itemPanel.Controls[1].Tag.ToInt32() == Ayarlar.SeciliSatırID)
                        {
                            seciliSatirID = itemPanel.Name.ToInt32();
                        }
                    }
                }
                seciliSatirID--;
                //sonra seçili satırdaki miktarı arttır
                MiktarArttir(sender == btnUygula && PanelVeriList[seciliSatirID].Miktar != 0, seciliSatirID, islemColumnIndex);
                //sütun güncellemeleri
                if (txtRafBarkod.Visible == true)
                    PanelVeriList[seciliSatirID].Controls[5].Text = raf;
                PanelVeriList[seciliSatirID].Controls[makaranoColumnIndex].Text = makaraNo;
                //seçili satırı turuncu yap
                SatirRenkDegistir(PanelVeriList[seciliSatirID].Controls, Color.DarkOrange);
                // Scrollu sağa kaydırma
                panelOrta.AutoScrollPosition = new Point(400, 0.Carpim());
            }
        }

        /// <summary>
        /// liste satırlarının rengini değiştir
        /// </summary>
        private void SatirRenkDegistir(Control.ControlCollection controls, Color renk)
        {
            foreach (Control item in controls)
                item.BackColor = renk;
        }

        /// <summary>
        /// satırlardaki işlem mikarı sütununun miktarlarını değiştirir
        /// </summary>
        private void MiktarArttir(bool Tip, int seciliSatirID, int islemColumnIndex)
        { 
            //+ tuşu ise miktarı yaz
            if (Tip)
                PanelVeriList[seciliSatirID].Controls[islemColumnIndex].Text = (PanelVeriList[seciliSatirID].Controls[3].Text.ToDecimal() - PanelVeriList[seciliSatirID].Controls[islemColumnIndex - 1].Text.ToDecimal()).ToString();
            //yoksa mikarı bir arttır
            else
                PanelVeriList[seciliSatirID].Controls[islemColumnIndex].Text = (PanelVeriList[seciliSatirID].Controls[islemColumnIndex].Text.ToDecimal() + 1).ToString();
        }
        
        /// <summary>
        /// yeni satır
        /// </summary>
        private void CreateListeSatirSayim(Tip_Malzeme malInfo, string raf)
        {
            Sayac++;
            panelOrta.AutoScrollPosition = new Point(0, 0);

            Font font = new Font("Tahoma", 8, FontStyle.Regular);
            PanelEx panelSatir = new PanelEx();
            panelSatir.Name = Sayac.ToString();
            panelSatir.Size = new Size(627.Carpim(), 20.Carpim());
            panelSatir.Location = new Point(1, (Sayac * 21).Carpim());

            TextBox tBarkod = new TextBox();
            tBarkod.Visible = false;
            tBarkod.Width = 3;
            tBarkod.Location = new Point(0, 0);
            tBarkod.ReadOnly = true;

            TextBox tMalKodu = new TextBox();
            tMalKodu.Font = font;
            tMalKodu.Width = lblMalkodu.Width;
            tMalKodu.Location = new Point(lblMalkodu.Left, 0);
            tMalKodu.ReadOnly = true;
            tMalKodu.GotFocus += new EventHandler(TextBoxlar_GotFocus);

            TextBox tMalAdi = new TextBox();
            tMalAdi.Font = font;
            tMalAdi.Width = lblMalzeme.Width;
            tMalAdi.Location = new Point(lblMalzeme.Left, 0);
            tMalAdi.ReadOnly = true;
            tMalAdi.GotFocus += new EventHandler(TextBoxlar_GotFocus);

            TextBox tMiktar = new TextBox();
            tMiktar.Font = font;
            tMiktar.Width = lblMiktar.Width;
            tMiktar.Location = new Point(lblMiktar.Left, 0);
            tMiktar.TextAlign = HorizontalAlignment.Right;
            tMiktar.ReadOnly = true;
            tMiktar.GotFocus += new EventHandler(TextBoxlar_GotFocus);

            TextBox tBirim = new TextBox();
            tBirim.Font = font;
            tBirim.Width = lblBirim.Width;
            tBirim.Location = new Point(lblBirim.Left, 0);
            tBirim.ReadOnly = true;
            tBirim.GotFocus += new EventHandler(TextBoxlar_GotFocus);

            TextBox tMakaraNo = new TextBox();
            tMakaraNo.Font = font;
            tMakaraNo.Width = lblMakarano.Width;
            tMakaraNo.Location = new Point(lblMakarano.Left, 0);
            tMakaraNo.ReadOnly = false;
            tMakaraNo.GotFocus += new EventHandler(TextBoxlar_GotFocus);

            TextBox tRaf = new TextBox();
            tRaf.Font = font;
            tRaf.Width = lblOkutulanMiktar.Width;
            tRaf.Location = new Point(lblOkutulanMiktar.Left, 0);
            tRaf.ReadOnly = true;
            tRaf.GotFocus += new EventHandler(TextBoxlar_GotFocus);

            TextBox tYerlestirmeMiktari = new TextBox();
            tYerlestirmeMiktari.Font = font;
            tYerlestirmeMiktari.Width = lblYerlestirmeMiktar.Width;
            tYerlestirmeMiktari.Location = new Point(lblYerlestirmeMiktar.Left, 0);
            tYerlestirmeMiktari.ReadOnly = true;
            tYerlestirmeMiktari.GotFocus += new EventHandler(TextBoxlar_GotFocus);

            TextBox tIslemMiktar = new TextBox();
            tIslemMiktar.Font = font;
            tIslemMiktar.Width = lblIslemMiktar.Width;
            tIslemMiktar.Location = new Point(lblIslemMiktar.Left, 0);
            tIslemMiktar.GotFocus += new EventHandler(TextBoxlar_GotFocus);

            tMalKodu.BackColor = Color.DarkOrange;
            tMiktar.BackColor = Color.DarkOrange;
            tBirim.BackColor = Color.DarkOrange;
            tMakaraNo.BackColor = Color.DarkOrange;
            tMalAdi.BackColor = Color.DarkOrange;
            tRaf.BackColor = Color.DarkOrange;
            tIslemMiktar.BackColor = Color.DarkOrange;
            tYerlestirmeMiktari.BackColor = Color.DarkOrange;

            tBarkod.Text = malInfo.Barkod;
            tMalKodu.Text = malInfo.MalKodu;
            tMalAdi.Text = malInfo.MalAdi;
            tBirim.Text = malInfo.Birim;
            tMakaraNo.Text = txtMakaraBarkod.Text;
            tRaf.Text = raf;
            tMiktar.Text = "0";
            tYerlestirmeMiktari.Text = "0";
            tIslemMiktar.Text = "1";
            tMalKodu.Tag = "0";

            panelSatir.MalAdi = tMalAdi.Text;
            panelSatir.MalKodu = tMalKodu.Text;
            panelSatir.Miktar = 0;
            panelSatir.Birim = malInfo.Birim;
            panelSatir.MakaraNo = txtMakaraBarkod.Text;
            panelSatir.YerlestirmeMiktari = 0;
            panelSatir.Raf = raf;

            panelSatir.Controls.Add(tBarkod);
            panelSatir.Controls.Add(tMalKodu);
            panelSatir.Controls.Add(tMalAdi);
            panelSatir.Controls.Add(tMiktar);
            panelSatir.Controls.Add(tBirim);
            panelSatir.Controls.Add(tRaf);
            panelSatir.Controls.Add(tYerlestirmeMiktari);
            panelSatir.Controls.Add(tIslemMiktar);
            panelSatir.Controls.Add(tMakaraNo);
            // yeni eklenen satırı en üste atma
            var tempSayac = PanelVeriList.Count() + 1;
            foreach (PanelEx pnlItem in PanelVeriList)
            {
                pnlItem.Location = new Point(0, (tempSayac * 21).Carpim());
                tempSayac--;
            }

            panelSatir.Location = new Point(0, 21.Carpim());
            panelOrta.Controls.Add(panelSatir);
            PanelVeriList.Add(panelSatir);
            // Scrollu sağa kaydırma
            panelOrta.AutoScrollPosition = new Point(400, 0.Carpim());
        }
        
        /// <summary>
        /// yeni satır
        /// </summary>
        private void CreateListeSatirRaf(Tip_Malzeme malInfo, Tip_STI temp_sti, int Miktar)
        {
            Sayac++;
            panelOrta.AutoScrollPosition = new Point(0, 0);

            Font font = new Font("Tahoma", 8, FontStyle.Regular);
            PanelEx panelSatir = new PanelEx();
            panelSatir.Name = Sayac.ToString();
            panelSatir.Size = new Size(627.Carpim(), 20.Carpim());
            panelSatir.Location = new Point(1, (Sayac * 21).Carpim());

            TextBox tBarkod = new TextBox();
            tBarkod.Visible = false;
            tBarkod.Width = 3;
            tBarkod.Location = new Point(0, 0);
            tBarkod.ReadOnly = true;

            TextBox tMalKodu = new TextBox();
            tMalKodu.Font = font;
            tMalKodu.Width = lblMalkodu.Width;
            tMalKodu.Location = new Point(lblMalkodu.Left, 0);
            tMalKodu.ReadOnly = true;
            tMalKodu.GotFocus += new EventHandler(TextBoxlar_GotFocus);

            TextBox tMalAdi = new TextBox();
            tMalAdi.Font = font;
            tMalAdi.Width = lblMalzeme.Width;
            tMalAdi.Location = new Point(lblMalzeme.Left, 0);
            tMalAdi.ReadOnly = true;
            tMalAdi.GotFocus += new EventHandler(TextBoxlar_GotFocus);

            TextBox tMiktar = new TextBox();
            tMiktar.Font = font;
            tMiktar.Width = lblMiktar.Width;
            tMiktar.Location = new Point(lblMiktar.Left, 0);
            tMiktar.TextAlign = HorizontalAlignment.Right;
            tMiktar.ReadOnly = true;
            tMiktar.GotFocus += new EventHandler(TextBoxlar_GotFocus);

            TextBox tBirim = new TextBox();
            tBirim.Font = font;
            tBirim.Width = lblBirim.Width;
            tBirim.Location = new Point(lblBirim.Left, 0);
            tBirim.ReadOnly = true;
            tBirim.GotFocus += new EventHandler(TextBoxlar_GotFocus);

            TextBox tMakaraNo = new TextBox();
            tMakaraNo.Font = font;
            tMakaraNo.Width = lblMakarano.Width;
            tMakaraNo.Location = new Point(lblMakarano.Left, 0);
            tMakaraNo.ReadOnly = true;
            tMakaraNo.GotFocus += new EventHandler(TextBoxlar_GotFocus);

            TextBox tRaf = new TextBox();
            tRaf.Font = font;
            tRaf.Width = lblOkutulanMiktar.Width;
            tRaf.Location = new Point(lblOkutulanMiktar.Left, 0);
            tRaf.ReadOnly = true;
            tRaf.GotFocus += new EventHandler(TextBoxlar_GotFocus);

            TextBox tYerlestirmeMiktari = new TextBox();
            tYerlestirmeMiktari.Font = font;
            tYerlestirmeMiktari.Width = lblYerlestirmeMiktar.Width;
            tYerlestirmeMiktari.Location = new Point(lblYerlestirmeMiktar.Left, 0);
            tYerlestirmeMiktari.ReadOnly = true;
            tYerlestirmeMiktari.Visible = true;
            tYerlestirmeMiktari.GotFocus += new EventHandler(TextBoxlar_GotFocus);

            TextBox tIslemMiktar = new TextBox();
            tIslemMiktar.Font = font;
            tIslemMiktar.Width = lblIslemMiktar.Width;
            tIslemMiktar.Location = new Point(lblIslemMiktar.Left, 0);
            tIslemMiktar.ReadOnly = false;
            tIslemMiktar.Visible = true;
            tIslemMiktar.GotFocus += new EventHandler(TextBoxlar_GotFocus);

            tMalKodu.BackColor = Color.DarkOrange;
            tMiktar.BackColor = Color.DarkOrange;
            tBirim.BackColor = Color.DarkOrange;
            tMakaraNo.BackColor = Color.DarkOrange;
            tMalAdi.BackColor = Color.DarkOrange;
            tRaf.BackColor = Color.DarkOrange;
            tYerlestirmeMiktari.BackColor = Color.DarkOrange;
            tIslemMiktar.BackColor = Color.DarkOrange;

            tMiktar.Text = temp_sti.Miktar.ToString("N2");
            tBarkod.Text = temp_sti.Barkod;
            tMalKodu.Text = temp_sti.MalKodu;
            tBirim.Text = temp_sti.Birim;
            tMakaraNo.Text = temp_sti.MakaraNo;
            tMalAdi.Text = temp_sti.MalAdi;
            tRaf.Text = temp_sti.Raf;
            tYerlestirmeMiktari.Text = "0";
            tIslemMiktar.Text = Miktar.ToString("N2");

            if (Ayarlar.MenuTip == MenuType.SiparisToplama)
                tMalKodu.Tag = 0;
            else
                tMalKodu.Tag = temp_sti.ID.ToInt32();

            panelSatir.Barkod = temp_sti.Barkod;
            panelSatir.MalAdi = temp_sti.MalAdi;
            panelSatir.MalKodu = temp_sti.MalKodu;
            panelSatir.Miktar = temp_sti.Miktar;
            panelSatir.Birim = temp_sti.Birim;
            panelSatir.MakaraNo = temp_sti.MakaraNo;
            panelSatir.IslemMiktar = temp_sti.YerlestirmeMiktari;
            panelSatir.YerlestirmeMiktari = Miktar;
            panelSatir.Raf = temp_sti.Raf;

            panelSatir.Controls.Add(tBarkod);
            panelSatir.Controls.Add(tMalKodu);
            panelSatir.Controls.Add(tMalAdi);
            panelSatir.Controls.Add(tMiktar);
            panelSatir.Controls.Add(tBirim);
            panelSatir.Controls.Add(tRaf);
            panelSatir.Controls.Add(tYerlestirmeMiktari);
            panelSatir.Controls.Add(tIslemMiktar);
            panelSatir.Controls.Add(tMakaraNo);
            panelOrta.Controls.Add(panelSatir);
            PanelVeriList.Add(panelSatir);
            // Scrollu sağa kaydırma
            panelOrta.AutoScrollPosition = new Point(400, 0.Carpim());
        }
        
        /// <summary>
        /// veritabanına kaydeder
        /// </summary>
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var StiList = new List<frmMalKabul>();
            var YerList = new List<frmYerlesme>();
            var Sonuc = new Result();
            foreach (var itemPanel in PanelVeriList)
            {
                var sti = new frmMalKabul();
                var yer = new frmYerlesme();
                if (Ayarlar.MenuTip == MenuType.MalKabul || Ayarlar.MenuTip == MenuType.Paketle || Ayarlar.MenuTip == MenuType.Sevkiyat || Ayarlar.MenuTip == MenuType.Satıştanİade)
                {
                    sti.OkutulanMiktar = itemPanel.Controls[6].Text.ToDecimal();
                    sti.ID = itemPanel.Controls[1].Tag.ToInt32();
                    StiList.Add(sti);
                }
                else if (Ayarlar.MenuTip == MenuType.KontrollüSayım)
                {
                    //if (itemPanel.Controls[7].Text.ToDecimal() > 0)
                    //{
                    yer.MalKodu = itemPanel.Controls[1].Text;
                    yer.Miktar = itemPanel.Controls[7].Text.ToDecimal();
                    yer.Birim = itemPanel.Controls[4].Text;
                    yer.DepoID = Ayarlar.Kullanici.DepoID;
                    yer.IrsDetayID = itemPanel.Controls[1].Tag.ToInt32();
                    yer.IrsID = txtEvrakno.Tag.ToInt32();
                    yer.RafNo = itemPanel.Controls[5].Text;
                    yer.GorevID = GorevID;
                    yer.MakaraNo = itemPanel.Controls[8].Text;

                    YerList.Add(yer);
                    //}
                }
                else if (Ayarlar.MenuTip == MenuType.RafaYerlestirme || Ayarlar.MenuTip == MenuType.SiparisToplama || Ayarlar.MenuTip == MenuType.TransferÇıkış || Ayarlar.MenuTip == MenuType.TransferGiriş || Ayarlar.MenuTip == MenuType.Alımdanİade)
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
                        // 31-10-17
                        yer.MakaraNo = itemPanel.Controls[8].Text;
                        //
                        yer.OkutulanMiktar = itemPanel.Controls[3].Text.ToDecimal() - itemPanel.Controls[6].Text.ToDecimal();
                        YerList.Add(yer);
                    }
                }
            }
            //servise gönder
            if (Ayarlar.MenuTip == MenuType.MalKabul)
                Sonuc = Program.Servis.Mal_Kabul(StiList.ToArray(), GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
            else if (Ayarlar.MenuTip == MenuType.RafaYerlestirme)
                Sonuc = Program.Servis.Rafa_Kaldir(YerList.ToArray(), Ayarlar.Kullanici.ID, GorevID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
            else if (Ayarlar.MenuTip == MenuType.SiparisToplama || Ayarlar.MenuTip == MenuType.TransferÇıkış)
                Sonuc = Program.Servis.Siparis_Topla(YerList.ToArray(), Ayarlar.Kullanici.ID, GorevID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
            else if (Ayarlar.MenuTip == MenuType.TransferGiriş)
                Sonuc = Program.Servis.Transfer_Giris(YerList.ToArray(), Ayarlar.Kullanici.ID, GorevID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
            else if (Ayarlar.MenuTip == MenuType.Paketle || Ayarlar.MenuTip == MenuType.Sevkiyat)
                Sonuc = Program.Servis.Paketle(StiList.ToArray(), GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
            else if (Ayarlar.MenuTip == MenuType.KontrollüSayım)
                Sonuc = Program.Servis.Kontrollu_Say(YerList.ToArray(), GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
            else if (Ayarlar.MenuTip == MenuType.Alımdanİade)
                Sonuc = Program.Servis.AlimdanIade(YerList.ToArray(), Ayarlar.Kullanici.ID, GorevID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
            else if (Ayarlar.MenuTip == MenuType.Satıştanİade)
                Sonuc = Program.Servis.SatistanIade(StiList.ToArray(), GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
            Cursor.Current = Cursors.Default;
            //sonuç işlemleri
            if (Sonuc.Status == false)
                Mesaj.Uyari(Sonuc.Message);
            else
                Mesaj.Basari("Kayıt tamamlandı");
            //sayfayı yenile
            if (Ayarlar.MenuTip != MenuType.KontrollüSayım)
            {
                Cursor.Current = Cursors.WaitCursor;
                Ayarlar.STIKalemler = new List<Tip_STI>(Program.Servis.GetMalzemes(GorevID, Ayarlar.Kullanici.ID, glbTip, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid));
                Cursor.Current = Cursors.Default;
                if (Ayarlar.STIKalemler.Count == 0) this.Close();
                STIGetir();
            }
            else
            {
                ClearPanelList();
            }

            txtBarkod.Text = "";
            txtRafBarkod.Text = "";
            txtMakaraBarkod.Text = "";
        }

        /// <summary>
        /// uygula butonundaki kontroller buraya alındı
        /// </summary>
        private bool btnUygulaControl(Tip_Malzeme malInfo)
        {
            string mal = txtBarkod.Text;
            string raf = txtRafBarkod.Text.ToUpper();
            string makaraNo = txtMakaraBarkod.Text;
            //malzeme barkodu kontrolü
            if (mal == "")
            {
                Mesaj.Hata(null, "Malzemeyi okutun");
                txtBarkod.Focus();
                return false;
            }
            if (mal.Length > 20)
            {
                mal = mal.Substring(3, 13);
                txtBarkod.Text = mal;
            }
            //raf barkodü kontrol
            if (raf == "" && txtRafBarkod.Visible == true)
            {
                Mesaj.Hata(null, "Rafı okutun");
                txtRafBarkod.Focus();
                return false;
            }
            //raf gerçek mi kontrolü
            if (txtRafBarkod.Visible == true)
            {
                var kontrol = Program.Servis.IfExistsRaf(Ayarlar.Kullanici.DepoID, raf, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                if (kontrol == false)
                {
                    Mesaj.Uyari("Böyle bir raf sistemde kayıtlı değil!");
                    txtRafBarkod.Focus();
                    return false;
                }
            }
            //mal gerçek mi kontrolü
            if (malInfo == null || malInfo.MalKodu == null)
            {
                //birden fazla barkod varsa hata verecek
                if(malInfo.Barkod!=null)
                    Mesaj.Uyari(malInfo.Barkod);
                else
                    Mesaj.Uyari("Sistemde böyle bir barkod bulunamadı");
                txtBarkod.Focus();
                return false;
            }
            //makara no barkodu kontrolü
            if (mal != "" && makaraNo == "" && txtMakaraBarkod.Visible == true && malInfo.Kod1 == "KKABLO")
            {
                Mesaj.Hata(null, "Makara Numarasını okutun");
                txtMakaraBarkod.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// barkod okursa
        /// </summary>
        public delegate void MethodInvoker();
        private void Barkod_OnScan(Symbol.Barcode2.ScanDataCollection scanDataCollection)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    //read data
                    string okunan = scanDataCollection.GetFirst.Text;
                    //makara barkod
                    if (txtMakaraBarkod.Visible == true && txtMakaraBarkod.Focus())
                    {
                        txtMakaraBarkod.Text = okunan;
                    }
                    //raf okutmuşsa
                    else if (txtRafBarkod.Visible == true && (okunan.Length == 9 || okunan == "R-ZR-V"))//okunan.Length == 9 => 01-a01-10
                    {
                        txtRafBarkod.Text = okunan;
                        txtBarkod.Text = "";
                        txtMakaraBarkod.Text = "";
                    }
                    //mal barkodu okutmuşsa
                    else
                    {
                        if (okunan.Length > 20)
                            okunan = okunan.Substring(3, 13);
                        txtBarkod.Text = okunan;
                    }
                    //focus to next textbox or click uygula
                    if (txtRafBarkod.Visible == true && txtRafBarkod.Text == "")
                        txtRafBarkod.Focus();
                    else if (txtBarkod.Text == "")
                        txtBarkod.Focus();                        
                    else if (txtMakaraBarkod.Visible == true && txtMakaraBarkod.Text == "")
                        txtMakaraBarkod.Focus();
                    else
                        btnUygula_Click(Barkod, null);
                });
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// column arrangement on load
        /// </summary>
        private void RearrangeColumnPlaces()
        {
            //places
            if (GorevTip == 1 || GorevTip == 6 || GorevTip == 7 || GorevTip == 73)
            {
                lblIslemMiktar.Left = 450.Carpim();
            }
            else
            {
                lblIslemMiktar.Left = 556.Carpim();
                lblOkutulanMiktar.Left = lblMalkodu.Left;
                lblMalkodu.Left += lblOkutulanMiktar.Width + 1;
                lblMalzeme.Left += lblOkutulanMiktar.Width + 1;
                lblMiktar.Left += lblOkutulanMiktar.Width + 1;
                lblBirim.Left += lblOkutulanMiktar.Width + 1;
                lblMakarano.Left += lblOkutulanMiktar.Width + 1;
            }
            //visibilities
            if (GorevTip == 1)
            {
                this.Text = "Mal Kabulü - WMS Mobil";
                lblOkutulanMiktar.Text = "Okutulan Miktar";
                txtRafBarkod.Visible = false;
                label7.Visible = false;
                lblYerlestirmeMiktar.Visible = false;
            }
            else if (GorevTip == 73)
            {
                this.Text = "Satıştan İade - WMS Mobil";
                lblOkutulanMiktar.Text = "Okutulan Miktar";
                txtRafBarkod.Visible = false;
                label7.Visible = false;
                lblYerlestirmeMiktar.Visible = false;
            }
            else if (GorevTip == 2)
            {
                this.Text = "Rafa Yerleştirme - WMS Mobil";
                lblOkutulanMiktar.Text = "Raf";
            }
            else if (GorevTip == 3)
            {
                this.Text = "Sipariş Toplama - WMS Mobil";
                lblOkutulanMiktar.Text = "Raf";
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                txtUnvan.Visible = false;
                txtHesapKodu.Visible = false;
                txtEvrakno.Visible = false;
                panelOrta.Top -= 44;
                panelOrta.Height += 44;
            }
            else if (GorevTip == 6)
            {
                this.Text = "Paketle - WMS Mobil";
                lblOkutulanMiktar.Text = "Okutulan Miktar";
                txtRafBarkod.Visible = false;
                label7.Visible = false;
                lblYerlestirmeMiktar.Visible = false;
            }
            else if (GorevTip == 7)
            {
                this.Text = "Sevkiyat - WMS Mobil";
                lblOkutulanMiktar.Text = "Okutulan Miktar";
                txtRafBarkod.Visible = false;
                label7.Visible = false;
                lblYerlestirmeMiktar.Visible = false;
            }
            else if (GorevTip == 8)
            {
                this.Text = "Kontrollü Sayım - WMS Mobil";
                lblOkutulanMiktar.Text = "Raf";
                lblYerlestirmeMiktar.Text = "Okutulan Miktar";
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                txtUnvan.Visible = false;
                txtHesapKodu.Visible = false;
                txtEvrakno.Visible = false;
                panelOrta.Top -= 44;
                panelOrta.Height += 44;
            }
            else if (GorevTip == 72)
            {
                this.Text = "Alımdan İade - WMS Mobil";
                lblOkutulanMiktar.Text = "Raf";
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                txtUnvan.Visible = false;
                txtHesapKodu.Visible = false;
                txtEvrakno.Visible = false;
                panelOrta.Top -= 44;
                panelOrta.Height += 44;
            }
            else// if (GorevTip == 19 || GorevTip == 20)
            {
                this.Text = "Transfer - WMS Mobil";
                lblOkutulanMiktar.Text = "Raf";
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                txtUnvan.Visible = false;
                txtHesapKodu.Visible = false;
                txtEvrakno.Visible = false;
                panelOrta.Top -= 44;
                panelOrta.Height += 44;
            }
        }

        /// <summary>
        /// panel listesini temizler
        /// </summary>
        void ClearPanelList()
        {
            foreach (PanelEx rmvItem in PanelVeriList)
            {
                panelOrta.Controls.Remove(rmvItem);
            }
            PanelVeriList.Clear();
            Sayac = 0;
        }

        /// <summary>
        /// barkod yazınca
        /// </summary>
        private void txtBarkod_TextChanged(object sender, EventArgs e)
        {
            txtMakaraBarkod.Visible = false;
            label14.Visible = false;
            var malbilgileri = Program.Servis.GetMalzemeFromBarcode("", txtBarkod.Text, GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
            if (malbilgileri != null)
            {
                if (malbilgileri.Kod1 == "KKABLO")
                {
                    txtMakaraBarkod.Visible = true;
                    label14.Visible = true;
                    txtMakaraBarkod.Focus();
                }
            }
        }

        /// <summary>
        /// listedeki textboxlaralara focusda seçili satırı turuncu yap diğerlerini normal renk
        /// </summary>
        void TextBoxlar_GotFocus(object sender, EventArgs e)
        {
            Control panel = (sender as TextBox).Parent;
            FocusPanelName = panel.Name;
            //hepsini normal yap
            foreach (var itemPanel in PanelVeriList)
                SatirRenkDegistir(itemPanel.Controls, Color.FromArgb(206, 223, 239));
            //seçileni turuncu yap
            foreach (Control itemSecili in panel.Controls)
                itemSecili.BackColor = Color.DarkOrange;
        }

        /// <summary>
        /// txtbarkod txtraf ve txtmakaraya focusta selectall yap
        /// </summary>
        private void txt_GotFocus(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        /// <summary>
        /// geri
        /// </summary>
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// form kapanırken dispose yap
        /// </summary>
        private void MalzemeIslemleri_Closing(object sender, CancelEventArgs e)
        {
            try { Barkod.EnableScanner = false; }
            catch (Exception) { }
            try { Barkod.Dispose(); }
            catch (Exception) { }
        }
    }
}