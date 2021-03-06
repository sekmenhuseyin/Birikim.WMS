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
    public partial class frmxOpsSelect : Form
    {       
        /// <summary>
        /// form load
        /// </summary>
        public frmxOpsSelect(int gorevID, string malKodu, string rowID)
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.BackColor = Color.FromArgb(206, 223, 239);
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
            //malzeme listesini getir
            try
            {
                List<Tip_STI2> liste = new List<Tip_STI2>(Program.Servis.GetMalKoduMalzemes(malKodu, gorevID, Ayarlar.Kullanici.ID, false, Ayarlar.AuthCode, Ayarlar.Kullanici.Guid).ToList());
                foreach (Tip_STI2 item in liste)
                {
                    if (rowID.Contains(";" + item.ID.ToString() + ";"))
                    {
                        string[] l = new string[] { item.ID.ToString(), item.MalKodu, item.MalAdi, item.Miktar.ToString("N2"), item.Birim, item.MakaraNo, item.IrsaliyeNo.ToString(), item.KynkSiparisNo, item.KynkSiparisSiraNo.ToString() };
                        var it = new ListViewItem(l);
                        it.Tag = item.ID.ToInt32();
                        listView1.Items.Add(it);
                    }
                }
            }
            catch (Exception)
            {                
            }
        }

        /// <summary>
        /// listeden br eleman seçince onun değerini deişkene ata
        /// </summary>
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
                return;
            var secili = listView1.SelectedIndices[0];
            Ayarlar.SeciliSatırID = Convert.ToInt32(listView1.Items[secili].Tag);
        }

        /// <summary>
        /// seçe tıklayınc formu kapatır
        /// </summary>
        private void btnSec_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}