﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WMSMobil.TerminalService;

namespace WMSMobil
{
    public partial class frmTasks : Form
    {
        List<PanelGrv> PanelVeriList = new List<PanelGrv>();
        Control focusPanel = new Control();
        int Sayac = 0, GorevID = 0, IrsaliyeID = 0;
        bool isReady = false;
        /// <summary>
        /// form load
        /// </summary>
        public frmTasks()
        {            
            InitializeComponent();
            Cursor.Current = Cursors.WaitCursor;
            switch (Ayarlar.MenuTip)
            {
                case MenuType.None:
                    break;
                case MenuType.MalKabul:
                    btnLinkeAktar.Text = "Linke Aktar";
                    break;
                case MenuType.RafaYerlestirme:
                    btnLinkeAktar.Text="Görevi Sonlandır";
                    break;
                case MenuType.SiparisToplama:
                    btnLinkeAktar.Text = "Linke Aktar";
                    break;
                case MenuType.Paketle:
                    btnLinkeAktar.Text = "Görevi Sonlandır";
                    break;
                case MenuType.Sevkiyat:
                    btnLinkeAktar.Text = "Görevi Sonlandır";
                    break;
                case MenuType.TransferÇıkış:
                    btnLinkeAktar.Text = "Linke Aktar";
                    break;
                case MenuType.TransferGiriş:
                    btnLinkeAktar.Text = "Linke Aktar";
                    break;
                case MenuType.KontrollüSayım:
                    btnLinkeAktar.Text = "Görevi Sonlandır";
                    break;
                case MenuType.Alımdanİade:
                    btnLinkeAktar.Text = "Linke Aktar";
                    break;
                case MenuType.Satıştanİade:
                    btnLinkeAktar.Text = "Görevi Sonlandır";
                    break;
                default:
                    break;
            }
            try
            {
                //görevliler
                Ayarlar.Gorevliler = new List<GetGorevlis_Result>(Program.Servis.GetUsers(Ayarlar.Kullanici.DepoID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid));
                Ayarlar.Gorevliler.Add(new GetGorevlis_Result { ID = 0, Kod = " Tümü" });
                cmbGorevli.ValueMember = "ID";
                cmbGorevli.DisplayMember = "Kod";
                cmbGorevli.DataSource = Ayarlar.Gorevliler.OrderBy(o => o.Kod).ToList();
                //durumlar
                Ayarlar.GorevDurumlari = Program.Servis.GetDurums(Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid).ToList();
                cmbDurum.ValueMember = "ID";
                cmbDurum.DisplayMember = "Name";
                cmbDurum.DataSource = Ayarlar.GorevDurumlari;
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
            //click listele
            isReady = true;
            btnListele_Click(null, null);
        }
        /// <summary>
        /// listeden bir eleman seçince
        /// </summary>
        void TextBoxlar_GotFocus(object sender, EventArgs e)
        {
            if (isReady == false)
                return;
            focusPanel = (sender as TextBox).Parent;
            string[] tmp = focusPanel.Tag.ToString().Split('-');
            GorevID = tmp[0].ToInt32();
            IrsaliyeID = tmp[1].ToInt32();
            foreach (var itemPanel in PanelVeriList)
            {
                foreach (Control item in itemPanel.Controls)
                    item.BackColor = Color.FromArgb(206, 223, 239);
            }
            foreach (Control itemSecili in focusPanel.Controls)
                itemSecili.BackColor = Color.DarkOrange;
        }
        /// <summary>
        /// listele tuluna basınca ve görevli veya durumu değiştirince
        /// </summary>
        private void btnListele_Click(object sender, EventArgs e)
        {
            if (isReady == false)
                return;
            isReady = false;
            Cursor.Current = Cursors.WaitCursor;
            Ayarlar.Gorevler = new List<Tip_GOREV>(Program.Servis.GetGorevList(cmbGorevli.Text.Replace(" Tümü", ""), cmbDurum.SelectedValue.ToInt32(), Ayarlar.MenuTip.ToInt32(), Ayarlar.Kullanici.DepoID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid));
            Sayac = 0;
            //listeyi temizle
            foreach (PanelGrv rmvItem in PanelVeriList){panelOrta.Controls.Remove(rmvItem);}
            PanelVeriList.Clear();
            //yenisini ekle
            foreach (Tip_GOREV grvItem in Ayarlar.Gorevler)
            {
                Sayac++;//satır no
                panelOrta.AutoScrollPosition = new Point(0, 0);
                Font font = new Font("Tahoma", 8F, FontStyle.Regular);
                //sütun gorev no
                TextBox tGorevNo = new TextBox();
                tGorevNo.Font = font;
                tGorevNo.Width = lblGorevNo.Width;
                tGorevNo.Location = new Point(0, 0);
                tGorevNo.ReadOnly = true;
                tGorevNo.BackColor = Color.FromArgb(206, 223, 239);
                tGorevNo.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                //sütun bilgi
                TextBox tBilgi = new TextBox();
                tBilgi.Font = font;
                tBilgi.Width = lblBilgiler.Width;
                tBilgi.Location = new Point(lblBilgiler.Left, 0);
                tBilgi.ReadOnly = true;
                tBilgi.BackColor = Color.FromArgb(206, 223, 239);
                tBilgi.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                //sütun görevli
                TextBox tGorevli = new TextBox();
                tGorevli.Font = font;
                tGorevli.Width = lblGorevli.Width;
                tGorevli.Location = new Point(lblGorevli.Left, 0);
                tGorevli.ReadOnly = true;
                tGorevli.BackColor = Color.FromArgb(206, 223, 239);
                tGorevli.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                //sütun oluşturma tarihi
                TextBox tTarih = new TextBox();
                tTarih.Font = font;
                tTarih.Width = lblTarih.Width;
                tTarih.Location = new Point(lblTarih.Left, 0);
                tTarih.ReadOnly = true;
                tTarih.BackColor = Color.FromArgb(206, 223, 239);
                tTarih.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                //bilgileri yerleştir
                tGorevNo.Text = grvItem.GorevNo.ToString();
                tTarih.Text = grvItem.OlusturmaTarihi.ToString();
                tGorevli.Text = grvItem.Gorevli != null ? grvItem.Gorevli.ToString() : "";
                tBilgi.Text = grvItem.Bilgi;
                //panel ekle
                PanelGrv panelSatir = new PanelGrv();
                panelSatir.Name = Sayac.ToString();
                panelSatir.Size = new Size(273.Carpim(), 20.Carpim());
                panelSatir.Location = new Point(0, (Sayac * 20).Carpim());
                panelSatir.Tag = grvItem.ID + "-" + grvItem.IrsaliyeID;
                panelSatir.Controls.Add(tGorevNo);
                panelSatir.Controls.Add(tBilgi);
                panelSatir.Controls.Add(tGorevli);
                panelSatir.Controls.Add(tTarih);
                //panel
                panelOrta.Controls.Add(panelSatir);
                PanelVeriList.Add(panelSatir);
            }
            isReady = true;
            Cursor.Current = Cursors.Default;
        }
        /// <summary>
        /// düzenle tuşuna basınca
        /// </summary>
        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            if (GorevID.ToString2() == "0") return;
            frmxOps frm = new frmxOps(GorevID, IrsaliyeID, false, Ayarlar.MenuTip.ToInt32());
            frm.ShowDialog();        
        }
        /// <summary>
        /// işlem yapa basınca
        /// </summary>
        private void btnIslemYap_Click(object sender, EventArgs e)
        {
            if (GorevID == 0) return;
            frmxOps frm = new frmxOps(GorevID, IrsaliyeID, true, Ayarlar.MenuTip.ToInt32());
            try
            {
                frm.ShowDialog();
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// linke aktara basınca
        /// </summary>
        private void btnLinkeAktar_Click(object sender, EventArgs e)
        {
            Result sonuc = new Result();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (Ayarlar.MenuTip == MenuType.MalKabul)
                {
                    sonuc = Program.Servis.MalKabul_GorevKontrol(GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                    if (sonuc.Status == false && sonuc.Id == -1)
                    {
                        Cursor.Current = Cursors.Default;
                        if (Mesaj.Soru("Okunan mal miktarları tutarsız. Yine de devam etmek istiyor musunuz?") == DialogResult.Yes)
                            sonuc.Status = true;
                    }
                    if (sonuc.Status == true) sonuc = Program.Servis.MalKabul_GoreviTamamla(GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                }
                else if (Ayarlar.MenuTip == MenuType.RafaYerlestirme)
                    sonuc = Program.Servis.RafaKaldir_GoreviTamamla(GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                else if (Ayarlar.MenuTip == MenuType.SiparisToplama)
                    sonuc = Program.Servis.SiparisTopla_GoreviTamamla(GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                else if (Ayarlar.MenuTip == MenuType.Paketle)
                    sonuc = Program.Servis.Paketle_GoreviTamamla(GorevID, IrsaliyeID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                else if (Ayarlar.MenuTip == MenuType.Sevkiyat)
                    sonuc = Program.Servis.Sevkiyat_GoreviTamamla(GorevID, IrsaliyeID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                else if (Ayarlar.MenuTip == MenuType.TransferÇıkış)
                    sonuc = Program.Servis.TransferCikis_GoreviTamamla(GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                else if (Ayarlar.MenuTip == MenuType.TransferGiriş)
                    sonuc = Program.Servis.TransferGiris_GoreviTamamla(GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                else if (Ayarlar.MenuTip == MenuType.KontrollüSayım)
                {
                    Cursor.Current = Cursors.Default;
                    if (Mesaj.Soru("Bu görevi tamamladınız mı?") == DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        sonuc = Program.Servis.KontrolluSay_GoreviTamamla(GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                    }
                }
                else if (Ayarlar.MenuTip == MenuType.Alımdanİade)
                    sonuc = Program.Servis.AlimdanIade_GoreviTamamla(GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                else if (Ayarlar.MenuTip == MenuType.Satıştanİade)
                {
                    sonuc = Program.Servis.SatistanIade_GorevKontrol(GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                    if (sonuc.Status == false && sonuc.Id == -1)
                    {
                        Cursor.Current = Cursors.Default;
                        if (Mesaj.Soru("Okunan mal miktarları tutarsız. Yine de devam etmek istiyor musunuz?") == DialogResult.Yes)
                            sonuc.Status = true;
                    }
                    if (sonuc.Status == true) sonuc = Program.Servis.SatistanIade_GoreviTamamla(GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                }
            }
            catch (Exception ex)
            {
                sonuc.Status = false;
                sonuc.Message = ex.Message;
            }
            Cursor.Current = Cursors.Default;
            //sonuç
            if (sonuc.Status)
            {
                if (Ayarlar.MenuTip == MenuType.Paketle)
                {
                    Mesaj.Basari("İşlem gerçekleşti. Şimdi paket bilgilerini yazmanız gerekiyor.");
                    frmxPackDetail frm = new frmxPackDetail(GorevID);
                    frm.ShowDialog();
                }
                Mesaj.Basari("İşlem başarıyla gerçekleşti.");
                btnListele_Click(sender, e);
                if (Ayarlar.Gorevler.Count == 0) this.Close();
            }
            else
                if (sonuc.Message != "") 
                    Mesaj.Uyari(sonuc.Message);
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