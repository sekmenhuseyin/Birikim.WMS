using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WMSMobil
{
    public partial class frmxPackage : Form
    {
        private Barcode2 Barkod;
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