using System;
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
        Terminal Servis = new Terminal();
        Control focusPanel = new Control();
        int Sayac = 0, GorevID = 0, IrsaliyeID = 0;
        decimal carpim = Ayarlar.KatSayi;
        bool aktif;
        /// <summary>
        /// form load
        /// </summary>
        public frmTasks()
        {            
            InitializeComponent();
            Servis.Url = Ayarlar.ServisURL;
            try
            {

                switch (Ayarlar.MenuTip)
                {
                    case MenuType.None:
                        break;
                    case MenuType.MalKabul:
                        this.Text = "Mal Kabul";
                        btnLinkeAktar.Text = "Linke Aktar";
                        break;
                    case MenuType.RafaYerlestirme:
                        this.Text = "Rafa Yerleştirme";
                        btnLinkeAktar.Text="Görevi Sonlandır";
                        break;
                    case MenuType.SiparisToplama:
                        this.Text = "Sipariş Toplama";
                        btnLinkeAktar.Text = "Linke Aktar";
                        break;
                    case MenuType.Paketle:
                        this.Text = "Paketle";
                        btnLinkeAktar.Text = "Görevi Sonlandır";
                        break;
                    case MenuType.Sevkiyat:
                        this.Text = "Sevkiyat";
                        btnLinkeAktar.Text = "Görevi Sonlandır";
                        break;
                    case MenuType.TransferÇıkış:
                        this.Text = "Transfer";
                        btnLinkeAktar.Text = "Linke Aktar";
                        break;
                    case MenuType.TransferGiriş:
                        this.Text = "Transfer";
                        btnLinkeAktar.Text = "Linke Aktar";
                        break;
                    case MenuType.KontrollüSayım:
                        this.Text = "Kontrollü Sayım";
                        btnLinkeAktar.Text = "Görevi Sonlandır";
                        break;
                    default:
                        break;
                }
                aktif = true;
                //görevliler
                Ayarlar.Gorevliler = new List<GetGorevlis_Result>(Servis.GetUsers(Ayarlar.Kullanici.DepoID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid));
                Ayarlar.Gorevliler.Add(new GetGorevlis_Result { ID = 0, Kod = " Tümü" });
                cmbGorevli.ValueMember = "ID";
                cmbGorevli.DisplayMember = "Kod";
                cmbGorevli.DataSource = Ayarlar.Gorevliler.OrderBy(o => o.Kod).ToList();
                //durumlar
                Ayarlar.GorevDurumlari = Servis.GetDurums(Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid).ToList();
                cmbDurum.ValueMember = "ID";
                cmbDurum.DisplayMember = "Name";
                cmbDurum.DataSource = Ayarlar.GorevDurumlari;
                //click listele
                btnListele_Click(null,null);
            }
            catch (Exception ex)
            {
                Mesaj.Hata(ex);
            }
        }
        /// <summary>
        /// listeden bir eleman seçince
        /// </summary>
        void TextBoxlar_GotFocus(object sender, EventArgs e)
        {
            if (aktif == false)
            {
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
        }
        /// <summary>
        /// listele tuluna basınca ve görevli veya durumu değiştirince
        /// </summary>
        private void btnListele_Click(object sender, EventArgs e)
        {
            Ayarlar.Gorevler = new List<Tip_GOREV>(Servis.GetGorevList(cmbGorevli.Text.Replace(" Tümü",""), cmbDurum.SelectedValue.ToInt32(), Ayarlar.MenuTip.ToInt32(), Ayarlar.Kullanici.DepoID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid));
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
                tGorevNo.Width = (55 * carpim).ToInt32();
                tGorevNo.Location = new Point(0, 0);
                tGorevNo.ReadOnly = true;
                tGorevNo.BackColor = Color.FromArgb(206, 223, 239);
                tGorevNo.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                //sütun bilgi
                TextBox tBilgi = new TextBox();
                tBilgi.Font = font;
                tBilgi.Width = (100 * carpim).ToInt32();
                tBilgi.Location = new Point((56 * carpim).ToInt32(), 0);
                tBilgi.ReadOnly = true;
                tBilgi.BackColor = Color.FromArgb(206, 223, 239);
                tBilgi.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                //sütun görevli
                TextBox tGorevli = new TextBox();
                tGorevli.Font = font;
                tGorevli.Width = (45 * carpim).ToInt32();
                tGorevli.Location = new Point((157 * carpim).ToInt32(), 0);
                tGorevli.ReadOnly = true;
                tGorevli.BackColor = Color.FromArgb(206, 223, 239);
                tGorevli.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                //sütun oluşturma tarihi
                TextBox tKayitTarihi = new TextBox();
                tKayitTarihi.Font = font;
                tKayitTarihi.Width = (70 * carpim).ToInt32();
                tKayitTarihi.Location = new Point((203 * carpim).ToInt32(), 0);
                tKayitTarihi.ReadOnly = true;
                tKayitTarihi.BackColor = Color.FromArgb(206, 223, 239);
                tKayitTarihi.GotFocus += new EventHandler(TextBoxlar_GotFocus);
                //bilgileri yerleştir
                tGorevNo.Text = grvItem.GorevNo.ToString();
                tKayitTarihi.Text = grvItem.OlusturmaTarihi.ToString();
                tGorevli.Text = grvItem.Gorevli != null ? grvItem.Gorevli.ToString() : "";
                tBilgi.Text = grvItem.Bilgi;
                //panel ekle
                PanelGrv panelSatir = new PanelGrv();
                panelSatir.Name = Sayac.ToString();
                panelSatir.Size = new Size((273 * carpim).ToInt32(), (20 * carpim).ToInt32());
                panelSatir.Location = new Point(0, (Sayac * 20 * carpim).ToInt32());
                panelSatir.Tag = grvItem.ID + "-" + grvItem.IrsaliyeID;
                panelSatir.Controls.Add(tGorevNo);
                panelSatir.Controls.Add(tBilgi);
                panelSatir.Controls.Add(tGorevli);
                panelSatir.Controls.Add(tKayitTarihi);
                //panel
                panelOrta.Controls.Add(panelSatir);
                PanelVeriList.Add(panelSatir);
            }
            aktif = false;            
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
            frm.ShowDialog();
        }
        /// <summary>
        /// linke aktara basınca
        /// </summary>
        private void btnLinkeAktar_Click(object sender, EventArgs e)
        {
            Result sonuc = new Result();
            try
            {
                if (Ayarlar.MenuTip == MenuType.MalKabul)
                {
                    sonuc = Servis.MalKabul_GorevKontrol(GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                    if (sonuc.Status == false && sonuc.Id == -1)
                        if (Mesaj.Soru("Okunan mal miktarları tutarsız. Yine de devam etmek istiyor musunuz?") == DialogResult.Yes)
                            sonuc.Status = true;
                    if (sonuc.Status == true) sonuc = Servis.MalKabul_GoreviTamamla(GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                }
                else if (Ayarlar.MenuTip == MenuType.RafaYerlestirme)
                    sonuc = Servis.RafaKaldir_GoreviTamamla(GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                else if (Ayarlar.MenuTip == MenuType.SiparisToplama)
                    sonuc = Servis.SiparisTopla_GoreviTamamla(GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                else if (Ayarlar.MenuTip == MenuType.Paketle)
                    sonuc = Servis.Paketle_GoreviTamamla(GorevID, IrsaliyeID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                else if (Ayarlar.MenuTip == MenuType.Sevkiyat)
                    sonuc = Servis.Sevkiyat_GoreviTamamla(GorevID, IrsaliyeID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                else if (Ayarlar.MenuTip == MenuType.TransferÇıkış)
                    sonuc = Servis.TransferCikis_GoreviTamamla(GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                else if (Ayarlar.MenuTip == MenuType.TransferGiriş)
                    sonuc = Servis.TransferGiris_GoreviTamamla(GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
                else if (Ayarlar.MenuTip == MenuType.KontrollüSayım)
                    if (Mesaj.Soru("Bu görevi tamamladınız mı?") == DialogResult.Yes)
                        sonuc = Servis.KontrolluSay_GoreviTamamla(GorevID, Ayarlar.Kullanici.ID, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid);
            }
            catch (Exception ex)
            {
                sonuc.Status = false;
                sonuc.Message = ex.Message;
            }
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
        /// dispose
        /// </summary>
        private void frmTasks_Closing(object sender, CancelEventArgs e)
        {
            Servis.Dispose();
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