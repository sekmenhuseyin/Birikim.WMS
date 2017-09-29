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
    public partial class frmxOpsSelect : Form
    {
        Terminal Servis = new Terminal();
       
        public frmxOpsSelect(int gorevID, string malKodu)
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.BackColor = Color.FromArgb(206, 223, 239);
            Servis.Url = Ayarlar.ServisURL;
            try
            {
                List<Tip_STI2> liste = new List<Tip_STI2>(Servis.GetMalKoduMalzemes(malKodu, gorevID, Ayarlar.Kullanici.ID, false, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid).ToList());
                foreach (Tip_STI2 item in liste)
                {
                    string[] l = new string[] { item.ID.ToString(), item.MalKodu, item.MalAdi, item.Miktar.ToString("N2"), item.Birim, item.MakaraNo,item.IrsaliyeNo.ToString(), item.KynkSiparisNo, item.KynkSiparisSiraNo.ToString() };
                    var it=new ListViewItem(l);
                    listView1.Items.Add(it);
                }
            }
            catch (Exception)
            {                
            }
            //change size
            if (Screen.PrimaryScreen.Bounds.Height == Screen.PrimaryScreen.Bounds.Width)
            {
                decimal crpm = (decimal)(Screen.PrimaryScreen.WorkingArea.Height - 20) / (decimal)320;
                foreach (Control item in this.Controls)
                {
                    item.Top = (item.Top * crpm).ToInt32();
                    item.Height = (item.Height * crpm).ToInt32();
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var secili = listView1.SelectedIndices[0];
            Ayarlar.Tarih = Convert.ToInt32(listView1.Items[secili].SubItems[0].Text);
        }

        private void btnSec_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmxOpsSelect_Closing(object sender, CancelEventArgs e)
        {
            Servis.Dispose();
        }

    }
}