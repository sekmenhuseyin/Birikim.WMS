namespace WMSMobil
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnMalKabul = new System.Windows.Forms.Button();
            this.btnRafaKaldirma = new System.Windows.Forms.Button();
            this.lblMalKabul = new System.Windows.Forms.Label();
            this.btnSiparisToplama = new System.Windows.Forms.Button();
            this.btnKabloSiparisi = new System.Windows.Forms.Button();
            this.btnPaketleme = new System.Windows.Forms.Button();
            this.btnSevkiyat = new System.Windows.Forms.Button();
            this.btnTransferIn = new System.Windows.Forms.Button();
            this.lblRafKaldirma = new System.Windows.Forms.Label();
            this.lblSiparisToplama = new System.Windows.Forms.Label();
            this.lblKabloSiparisi = new System.Windows.Forms.Label();
            this.lblSevkiyat = new System.Windows.Forms.Label();
            this.lblPaketleme = new System.Windows.Forms.Label();
            this.lblTransferIn = new System.Windows.Forms.Label();
            this.lblTransferOut = new System.Windows.Forms.Label();
            this.btnTransferOut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMalKabul
            // 
            this.btnMalKabul.BackColor = System.Drawing.Color.Teal;
            this.btnMalKabul.ForeColor = System.Drawing.Color.White;
            this.btnMalKabul.Location = new System.Drawing.Point(2, 14);
            this.btnMalKabul.Name = "btnMalKabul";
            this.btnMalKabul.Size = new System.Drawing.Size(115, 56);
            this.btnMalKabul.TabIndex = 0;
            this.btnMalKabul.Tag = "1";
            this.btnMalKabul.Text = "Mal Kabul";
            this.btnMalKabul.Click += new System.EventHandler(this.btns_Click);
            // 
            // btnRafaKaldirma
            // 
            this.btnRafaKaldirma.BackColor = System.Drawing.Color.DarkOrange;
            this.btnRafaKaldirma.ForeColor = System.Drawing.Color.White;
            this.btnRafaKaldirma.Location = new System.Drawing.Point(122, 14);
            this.btnRafaKaldirma.Name = "btnRafaKaldirma";
            this.btnRafaKaldirma.Size = new System.Drawing.Size(115, 56);
            this.btnRafaKaldirma.TabIndex = 0;
            this.btnRafaKaldirma.Tag = "2";
            this.btnRafaKaldirma.Text = "Rafa Kaldırma";
            this.btnRafaKaldirma.Click += new System.EventHandler(this.btns_Click);
            // 
            // lblMalKabul
            // 
            this.lblMalKabul.BackColor = System.Drawing.Color.Teal;
            this.lblMalKabul.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblMalKabul.ForeColor = System.Drawing.Color.White;
            this.lblMalKabul.Location = new System.Drawing.Point(43, 51);
            this.lblMalKabul.Name = "lblMalKabul";
            this.lblMalKabul.Size = new System.Drawing.Size(32, 16);
            this.lblMalKabul.Tag = "";
            this.lblMalKabul.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnSiparisToplama
            // 
            this.btnSiparisToplama.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.btnSiparisToplama.ForeColor = System.Drawing.Color.White;
            this.btnSiparisToplama.Location = new System.Drawing.Point(2, 84);
            this.btnSiparisToplama.Name = "btnSiparisToplama";
            this.btnSiparisToplama.Size = new System.Drawing.Size(115, 56);
            this.btnSiparisToplama.TabIndex = 0;
            this.btnSiparisToplama.Tag = "3";
            this.btnSiparisToplama.Text = "Sipariş Toplama";
            this.btnSiparisToplama.Click += new System.EventHandler(this.btns_Click);
            // 
            // btnKabloSiparisi
            // 
            this.btnKabloSiparisi.BackColor = System.Drawing.Color.Green;
            this.btnKabloSiparisi.ForeColor = System.Drawing.Color.White;
            this.btnKabloSiparisi.Location = new System.Drawing.Point(122, 153);
            this.btnKabloSiparisi.Name = "btnKabloSiparisi";
            this.btnKabloSiparisi.Size = new System.Drawing.Size(115, 56);
            this.btnKabloSiparisi.TabIndex = 0;
            this.btnKabloSiparisi.Tag = "4";
            this.btnKabloSiparisi.Text = "Kablo Siparişi";
            this.btnKabloSiparisi.Click += new System.EventHandler(this.btns_Click);
            // 
            // btnPaketleme
            // 
            this.btnPaketleme.BackColor = System.Drawing.Color.Goldenrod;
            this.btnPaketleme.ForeColor = System.Drawing.Color.White;
            this.btnPaketleme.Location = new System.Drawing.Point(122, 84);
            this.btnPaketleme.Name = "btnPaketleme";
            this.btnPaketleme.Size = new System.Drawing.Size(115, 56);
            this.btnPaketleme.TabIndex = 0;
            this.btnPaketleme.Tag = "6";
            this.btnPaketleme.Text = "Paketleme";
            this.btnPaketleme.Click += new System.EventHandler(this.btns_Click);
            // 
            // btnSevkiyat
            // 
            this.btnSevkiyat.BackColor = System.Drawing.Color.Sienna;
            this.btnSevkiyat.ForeColor = System.Drawing.Color.White;
            this.btnSevkiyat.Location = new System.Drawing.Point(2, 153);
            this.btnSevkiyat.Name = "btnSevkiyat";
            this.btnSevkiyat.Size = new System.Drawing.Size(115, 56);
            this.btnSevkiyat.TabIndex = 0;
            this.btnSevkiyat.Tag = "7";
            this.btnSevkiyat.Text = "Sevkiyat";
            this.btnSevkiyat.Click += new System.EventHandler(this.btns_Click);
            // 
            // btnTransferIn
            // 
            this.btnTransferIn.BackColor = System.Drawing.Color.SteelBlue;
            this.btnTransferIn.ForeColor = System.Drawing.Color.White;
            this.btnTransferIn.Location = new System.Drawing.Point(2, 222);
            this.btnTransferIn.Name = "btnTransferIn";
            this.btnTransferIn.Size = new System.Drawing.Size(115, 56);
            this.btnTransferIn.TabIndex = 0;
            this.btnTransferIn.Tag = "20";
            this.btnTransferIn.Text = "Transfer Giriş";
            this.btnTransferIn.Click += new System.EventHandler(this.btns_Click);
            // 
            // lblRafKaldirma
            // 
            this.lblRafKaldirma.BackColor = System.Drawing.Color.DarkOrange;
            this.lblRafKaldirma.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblRafKaldirma.ForeColor = System.Drawing.Color.White;
            this.lblRafKaldirma.Location = new System.Drawing.Point(163, 51);
            this.lblRafKaldirma.Name = "lblRafKaldirma";
            this.lblRafKaldirma.Size = new System.Drawing.Size(32, 16);
            this.lblRafKaldirma.Tag = "";
            this.lblRafKaldirma.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSiparisToplama
            // 
            this.lblSiparisToplama.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.lblSiparisToplama.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblSiparisToplama.ForeColor = System.Drawing.Color.White;
            this.lblSiparisToplama.Location = new System.Drawing.Point(43, 121);
            this.lblSiparisToplama.Name = "lblSiparisToplama";
            this.lblSiparisToplama.Size = new System.Drawing.Size(32, 16);
            this.lblSiparisToplama.Tag = "";
            this.lblSiparisToplama.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblKabloSiparisi
            // 
            this.lblKabloSiparisi.BackColor = System.Drawing.Color.Green;
            this.lblKabloSiparisi.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblKabloSiparisi.ForeColor = System.Drawing.Color.White;
            this.lblKabloSiparisi.Location = new System.Drawing.Point(163, 190);
            this.lblKabloSiparisi.Name = "lblKabloSiparisi";
            this.lblKabloSiparisi.Size = new System.Drawing.Size(32, 16);
            this.lblKabloSiparisi.Tag = "";
            this.lblKabloSiparisi.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSevkiyat
            // 
            this.lblSevkiyat.BackColor = System.Drawing.Color.Sienna;
            this.lblSevkiyat.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblSevkiyat.ForeColor = System.Drawing.Color.White;
            this.lblSevkiyat.Location = new System.Drawing.Point(43, 190);
            this.lblSevkiyat.Name = "lblSevkiyat";
            this.lblSevkiyat.Size = new System.Drawing.Size(32, 16);
            this.lblSevkiyat.Tag = "";
            this.lblSevkiyat.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblPaketleme
            // 
            this.lblPaketleme.BackColor = System.Drawing.Color.Goldenrod;
            this.lblPaketleme.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblPaketleme.ForeColor = System.Drawing.Color.White;
            this.lblPaketleme.Location = new System.Drawing.Point(163, 121);
            this.lblPaketleme.Name = "lblPaketleme";
            this.lblPaketleme.Size = new System.Drawing.Size(32, 16);
            this.lblPaketleme.Tag = "";
            this.lblPaketleme.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTransferIn
            // 
            this.lblTransferIn.BackColor = System.Drawing.Color.SteelBlue;
            this.lblTransferIn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTransferIn.ForeColor = System.Drawing.Color.White;
            this.lblTransferIn.Location = new System.Drawing.Point(43, 259);
            this.lblTransferIn.Name = "lblTransferIn";
            this.lblTransferIn.Size = new System.Drawing.Size(32, 16);
            this.lblTransferIn.Tag = "";
            this.lblTransferIn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTransferOut
            // 
            this.lblTransferOut.BackColor = System.Drawing.Color.Olive;
            this.lblTransferOut.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTransferOut.ForeColor = System.Drawing.Color.White;
            this.lblTransferOut.Location = new System.Drawing.Point(163, 259);
            this.lblTransferOut.Name = "lblTransferOut";
            this.lblTransferOut.Size = new System.Drawing.Size(32, 16);
            this.lblTransferOut.Tag = "";
            this.lblTransferOut.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnTransferOut
            // 
            this.btnTransferOut.BackColor = System.Drawing.Color.Olive;
            this.btnTransferOut.ForeColor = System.Drawing.Color.White;
            this.btnTransferOut.Location = new System.Drawing.Point(122, 222);
            this.btnTransferOut.Name = "btnTransferOut";
            this.btnTransferOut.Size = new System.Drawing.Size(115, 56);
            this.btnTransferOut.TabIndex = 8;
            this.btnTransferOut.Tag = "19";
            this.btnTransferOut.Text = "Transfer Çıkış";
            this.btnTransferOut.Click += new System.EventHandler(this.btns_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(240, 302);
            this.Controls.Add(this.lblTransferOut);
            this.Controls.Add(this.btnTransferOut);
            this.Controls.Add(this.lblTransferIn);
            this.Controls.Add(this.lblSevkiyat);
            this.Controls.Add(this.lblPaketleme);
            this.Controls.Add(this.lblKabloSiparisi);
            this.Controls.Add(this.lblSiparisToplama);
            this.Controls.Add(this.lblRafKaldirma);
            this.Controls.Add(this.lblMalKabul);
            this.Controls.Add(this.btnSevkiyat);
            this.Controls.Add(this.btnTransferIn);
            this.Controls.Add(this.btnKabloSiparisi);
            this.Controls.Add(this.btnPaketleme);
            this.Controls.Add(this.btnSiparisToplama);
            this.Controls.Add(this.btnRafaKaldirma);
            this.Controls.Add(this.btnMalKabul);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmMain";
            this.Text = "Ana Form";
            this.Activated += new System.EventHandler(this.AnaForm_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.AnaForm_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMalKabul;
        private System.Windows.Forms.Button btnRafaKaldirma;
        private System.Windows.Forms.Label lblMalKabul;
        private System.Windows.Forms.Button btnSiparisToplama;
        private System.Windows.Forms.Button btnKabloSiparisi;
        private System.Windows.Forms.Button btnPaketleme;
        private System.Windows.Forms.Button btnSevkiyat;
        private System.Windows.Forms.Button btnTransferIn;
        private System.Windows.Forms.Label lblRafKaldirma;
        private System.Windows.Forms.Label lblSiparisToplama;
        private System.Windows.Forms.Label lblKabloSiparisi;
        private System.Windows.Forms.Label lblSevkiyat;
        private System.Windows.Forms.Label lblPaketleme;
        private System.Windows.Forms.Label lblTransferIn;
        private System.Windows.Forms.Label lblTransferOut;
        private System.Windows.Forms.Button btnTransferOut;
    }
}

