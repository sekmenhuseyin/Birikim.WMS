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
            this.btnSayim = new System.Windows.Forms.Button();
            this.btnPaketleme = new System.Windows.Forms.Button();
            this.btnTransferIn = new System.Windows.Forms.Button();
            this.lblRafKaldirma = new System.Windows.Forms.Label();
            this.lblSiparisToplama = new System.Windows.Forms.Label();
            this.lblSayim = new System.Windows.Forms.Label();
            this.lblPaketleme = new System.Windows.Forms.Label();
            this.lblTransferIn = new System.Windows.Forms.Label();
            this.lblTransferOut = new System.Windows.Forms.Label();
            this.btnTransferOut = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnBarcode = new System.Windows.Forms.Button();
            this.btnAlim = new System.Windows.Forms.Button();
            this.btnSatis = new System.Windows.Forms.Button();
            this.lblAlim = new System.Windows.Forms.Label();
            this.lblSatis = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnMalKabul
            // 
            this.btnMalKabul.BackColor = System.Drawing.Color.Teal;
            this.btnMalKabul.ForeColor = System.Drawing.Color.White;
            this.btnMalKabul.Location = new System.Drawing.Point(3, 32);
            this.btnMalKabul.Name = "btnMalKabul";
            this.btnMalKabul.Size = new System.Drawing.Size(115, 50);
            this.btnMalKabul.TabIndex = 1;
            this.btnMalKabul.Tag = "1";
            this.btnMalKabul.Text = "Mal Kabul";
            this.btnMalKabul.Click += new System.EventHandler(this.btns_Click);
            // 
            // btnRafaKaldirma
            // 
            this.btnRafaKaldirma.BackColor = System.Drawing.Color.DarkOrange;
            this.btnRafaKaldirma.ForeColor = System.Drawing.Color.White;
            this.btnRafaKaldirma.Location = new System.Drawing.Point(123, 32);
            this.btnRafaKaldirma.Name = "btnRafaKaldirma";
            this.btnRafaKaldirma.Size = new System.Drawing.Size(115, 50);
            this.btnRafaKaldirma.TabIndex = 2;
            this.btnRafaKaldirma.Tag = "2";
            this.btnRafaKaldirma.Text = "Rafa Kaldırma";
            this.btnRafaKaldirma.Click += new System.EventHandler(this.btns_Click);
            // 
            // lblMalKabul
            // 
            this.lblMalKabul.BackColor = System.Drawing.Color.Teal;
            this.lblMalKabul.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblMalKabul.ForeColor = System.Drawing.Color.White;
            this.lblMalKabul.Location = new System.Drawing.Point(44, 65);
            this.lblMalKabul.Name = "lblMalKabul";
            this.lblMalKabul.Size = new System.Drawing.Size(32, 14);
            this.lblMalKabul.Tag = "";
            this.lblMalKabul.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnSiparisToplama
            // 
            this.btnSiparisToplama.BackColor = System.Drawing.Color.Goldenrod;
            this.btnSiparisToplama.ForeColor = System.Drawing.Color.White;
            this.btnSiparisToplama.Location = new System.Drawing.Point(3, 87);
            this.btnSiparisToplama.Name = "btnSiparisToplama";
            this.btnSiparisToplama.Size = new System.Drawing.Size(115, 50);
            this.btnSiparisToplama.TabIndex = 3;
            this.btnSiparisToplama.Tag = "3";
            this.btnSiparisToplama.Text = "Sipariş Toplama";
            this.btnSiparisToplama.Click += new System.EventHandler(this.btns_Click);
            // 
            // btnSayim
            // 
            this.btnSayim.BackColor = System.Drawing.Color.Green;
            this.btnSayim.ForeColor = System.Drawing.Color.White;
            this.btnSayim.Location = new System.Drawing.Point(4, 195);
            this.btnSayim.Name = "btnSayim";
            this.btnSayim.Size = new System.Drawing.Size(115, 50);
            this.btnSayim.TabIndex = 7;
            this.btnSayim.Tag = "8";
            this.btnSayim.Text = "Kontrollü Sayım";
            this.btnSayim.Click += new System.EventHandler(this.btns_Click);
            // 
            // btnPaketleme
            // 
            this.btnPaketleme.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.btnPaketleme.ForeColor = System.Drawing.Color.White;
            this.btnPaketleme.Location = new System.Drawing.Point(123, 87);
            this.btnPaketleme.Name = "btnPaketleme";
            this.btnPaketleme.Size = new System.Drawing.Size(115, 50);
            this.btnPaketleme.TabIndex = 4;
            this.btnPaketleme.Tag = "6";
            this.btnPaketleme.Text = "Paketleme";
            this.btnPaketleme.Click += new System.EventHandler(this.btns_Click);
            // 
            // btnTransferIn
            // 
            this.btnTransferIn.BackColor = System.Drawing.Color.SteelBlue;
            this.btnTransferIn.ForeColor = System.Drawing.Color.White;
            this.btnTransferIn.Location = new System.Drawing.Point(4, 141);
            this.btnTransferIn.Name = "btnTransferIn";
            this.btnTransferIn.Size = new System.Drawing.Size(115, 50);
            this.btnTransferIn.TabIndex = 5;
            this.btnTransferIn.Tag = "20";
            this.btnTransferIn.Text = "Transfer Giriş";
            this.btnTransferIn.Click += new System.EventHandler(this.btns_Click);
            // 
            // lblRafKaldirma
            // 
            this.lblRafKaldirma.BackColor = System.Drawing.Color.DarkOrange;
            this.lblRafKaldirma.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblRafKaldirma.ForeColor = System.Drawing.Color.White;
            this.lblRafKaldirma.Location = new System.Drawing.Point(164, 65);
            this.lblRafKaldirma.Name = "lblRafKaldirma";
            this.lblRafKaldirma.Size = new System.Drawing.Size(32, 14);
            this.lblRafKaldirma.Tag = "";
            this.lblRafKaldirma.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSiparisToplama
            // 
            this.lblSiparisToplama.BackColor = System.Drawing.Color.Goldenrod;
            this.lblSiparisToplama.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblSiparisToplama.ForeColor = System.Drawing.Color.White;
            this.lblSiparisToplama.Location = new System.Drawing.Point(44, 120);
            this.lblSiparisToplama.Name = "lblSiparisToplama";
            this.lblSiparisToplama.Size = new System.Drawing.Size(32, 14);
            this.lblSiparisToplama.Tag = "";
            this.lblSiparisToplama.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSayim
            // 
            this.lblSayim.BackColor = System.Drawing.Color.Green;
            this.lblSayim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblSayim.ForeColor = System.Drawing.Color.White;
            this.lblSayim.Location = new System.Drawing.Point(44, 227);
            this.lblSayim.Name = "lblSayim";
            this.lblSayim.Size = new System.Drawing.Size(32, 16);
            this.lblSayim.Tag = "";
            this.lblSayim.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblPaketleme
            // 
            this.lblPaketleme.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.lblPaketleme.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblPaketleme.ForeColor = System.Drawing.Color.White;
            this.lblPaketleme.Location = new System.Drawing.Point(164, 120);
            this.lblPaketleme.Name = "lblPaketleme";
            this.lblPaketleme.Size = new System.Drawing.Size(32, 14);
            this.lblPaketleme.Tag = "";
            this.lblPaketleme.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTransferIn
            // 
            this.lblTransferIn.BackColor = System.Drawing.Color.SteelBlue;
            this.lblTransferIn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTransferIn.ForeColor = System.Drawing.Color.White;
            this.lblTransferIn.Location = new System.Drawing.Point(45, 175);
            this.lblTransferIn.Name = "lblTransferIn";
            this.lblTransferIn.Size = new System.Drawing.Size(32, 14);
            this.lblTransferIn.Tag = "";
            this.lblTransferIn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTransferOut
            // 
            this.lblTransferOut.BackColor = System.Drawing.Color.Olive;
            this.lblTransferOut.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTransferOut.ForeColor = System.Drawing.Color.White;
            this.lblTransferOut.Location = new System.Drawing.Point(165, 175);
            this.lblTransferOut.Name = "lblTransferOut";
            this.lblTransferOut.Size = new System.Drawing.Size(32, 14);
            this.lblTransferOut.Tag = "";
            this.lblTransferOut.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnTransferOut
            // 
            this.btnTransferOut.BackColor = System.Drawing.Color.Olive;
            this.btnTransferOut.ForeColor = System.Drawing.Color.White;
            this.btnTransferOut.Location = new System.Drawing.Point(124, 141);
            this.btnTransferOut.Name = "btnTransferOut";
            this.btnTransferOut.Size = new System.Drawing.Size(115, 50);
            this.btnTransferOut.TabIndex = 6;
            this.btnTransferOut.Tag = "19";
            this.btnTransferOut.Text = "Transfer Çıkış";
            this.btnTransferOut.Click += new System.EventHandler(this.btns_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Firebrick;
            this.btnBack.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(167, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(70, 23);
            this.btnBack.TabIndex = 9;
            this.btnBack.Tag = "0";
            this.btnBack.Text = "Kapat";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.SteelBlue;
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(3, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(95, 22);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Tag = "0";
            this.btnRefresh.Text = "Güncelle";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnBarcode
            // 
            this.btnBarcode.BackColor = System.Drawing.Color.DarkViolet;
            this.btnBarcode.ForeColor = System.Drawing.Color.White;
            this.btnBarcode.Location = new System.Drawing.Point(123, 195);
            this.btnBarcode.Name = "btnBarcode";
            this.btnBarcode.Size = new System.Drawing.Size(115, 50);
            this.btnBarcode.TabIndex = 8;
            this.btnBarcode.Tag = "6";
            this.btnBarcode.Text = "Paket Barkod";
            this.btnBarcode.Click += new System.EventHandler(this.btnBarcode_Click);
            // 
            // btnAlim
            // 
            this.btnAlim.BackColor = System.Drawing.Color.DarkCyan;
            this.btnAlim.ForeColor = System.Drawing.Color.White;
            this.btnAlim.Location = new System.Drawing.Point(4, 249);
            this.btnAlim.Name = "btnAlim";
            this.btnAlim.Size = new System.Drawing.Size(115, 50);
            this.btnAlim.TabIndex = 8;
            this.btnAlim.Tag = "6";
            this.btnAlim.Text = "Alımda İade";
            this.btnAlim.Click += new System.EventHandler(this.btnBarcode_Click);
            // 
            // btnSatis
            // 
            this.btnSatis.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnSatis.ForeColor = System.Drawing.Color.White;
            this.btnSatis.Location = new System.Drawing.Point(124, 249);
            this.btnSatis.Name = "btnSatis";
            this.btnSatis.Size = new System.Drawing.Size(115, 50);
            this.btnSatis.TabIndex = 8;
            this.btnSatis.Tag = "6";
            this.btnSatis.Text = "Satıştan İade";
            this.btnSatis.Click += new System.EventHandler(this.btnBarcode_Click);
            // 
            // lblAlim
            // 
            this.lblAlim.BackColor = System.Drawing.Color.DarkCyan;
            this.lblAlim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblAlim.ForeColor = System.Drawing.Color.White;
            this.lblAlim.Location = new System.Drawing.Point(45, 283);
            this.lblAlim.Name = "lblAlim";
            this.lblAlim.Size = new System.Drawing.Size(32, 14);
            this.lblAlim.Tag = "";
            this.lblAlim.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSatis
            // 
            this.lblSatis.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblSatis.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblSatis.ForeColor = System.Drawing.Color.White;
            this.lblSatis.Location = new System.Drawing.Point(164, 283);
            this.lblSatis.Name = "lblSatis";
            this.lblSatis.Size = new System.Drawing.Size(32, 14);
            this.lblSatis.Tag = "";
            this.lblSatis.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(240, 302);
            this.Controls.Add(this.btnBarcode);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblTransferOut);
            this.Controls.Add(this.btnTransferOut);
            this.Controls.Add(this.lblAlim);
            this.Controls.Add(this.lblTransferIn);
            this.Controls.Add(this.lblPaketleme);
            this.Controls.Add(this.lblSatis);
            this.Controls.Add(this.lblSayim);
            this.Controls.Add(this.lblSiparisToplama);
            this.Controls.Add(this.lblRafKaldirma);
            this.Controls.Add(this.lblMalKabul);
            this.Controls.Add(this.btnTransferIn);
            this.Controls.Add(this.btnSayim);
            this.Controls.Add(this.btnPaketleme);
            this.Controls.Add(this.btnSiparisToplama);
            this.Controls.Add(this.btnRafaKaldirma);
            this.Controls.Add(this.btnMalKabul);
            this.Controls.Add(this.btnSatis);
            this.Controls.Add(this.btnAlim);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmMain";
            this.Text = "WMS Mobil";
            this.Activated += new System.EventHandler(this.AnaForm_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.AnaForm_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMalKabul;
        private System.Windows.Forms.Button btnRafaKaldirma;
        private System.Windows.Forms.Label lblMalKabul;
        private System.Windows.Forms.Button btnSiparisToplama;
        private System.Windows.Forms.Button btnSayim;
        private System.Windows.Forms.Button btnPaketleme;
        private System.Windows.Forms.Button btnTransferIn;
        private System.Windows.Forms.Label lblRafKaldirma;
        private System.Windows.Forms.Label lblSiparisToplama;
        private System.Windows.Forms.Label lblSayim;
        private System.Windows.Forms.Label lblPaketleme;
        private System.Windows.Forms.Label lblTransferIn;
        private System.Windows.Forms.Label lblTransferOut;
        private System.Windows.Forms.Button btnTransferOut;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnBarcode;
        private System.Windows.Forms.Button btnAlim;
        private System.Windows.Forms.Button btnSatis;
        private System.Windows.Forms.Label lblAlim;
        private System.Windows.Forms.Label lblSatis;
    }
}

