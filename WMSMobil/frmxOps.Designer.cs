namespace WMSMobil
{
    partial class frmxOps
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmxOps));
            this.panelUst = new System.Windows.Forms.Panel();
            this.btnUygula = new System.Windows.Forms.Button();
            this.txtRafBarkod = new System.Windows.Forms.TextBox();
            this.txtBarkod = new System.Windows.Forms.TextBox();
            this.btnEkle = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtEvrakno = new System.Windows.Forms.TextBox();
            this.txtHesapKodu = new System.Windows.Forms.TextBox();
            this.txtMakaraBarkod = new System.Windows.Forms.TextBox();
            this.txtUnvan = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelOrta = new System.Windows.Forms.Panel();
            this.lblMakarano = new System.Windows.Forms.Label();
            this.lblIslemMiktar = new System.Windows.Forms.Label();
            this.lblYerlestirmeMiktar = new System.Windows.Forms.Label();
            this.lblOkutulanMiktar = new System.Windows.Forms.Label();
            this.lblBirim = new System.Windows.Forms.Label();
            this.lblMiktar = new System.Windows.Forms.Label();
            this.lblMalzeme = new System.Windows.Forms.Label();
            this.lblMalkodu = new System.Windows.Forms.Label();
            this.panelAlt = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.panelUst.SuspendLayout();
            this.panelOrta.SuspendLayout();
            this.panelAlt.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelUst
            // 
            this.panelUst.BackColor = System.Drawing.Color.Transparent;
            this.panelUst.Controls.Add(this.btnUygula);
            this.panelUst.Controls.Add(this.txtRafBarkod);
            this.panelUst.Controls.Add(this.txtBarkod);
            this.panelUst.Controls.Add(this.btnEkle);
            this.panelUst.Controls.Add(this.label7);
            this.panelUst.Controls.Add(this.label14);
            this.panelUst.Controls.Add(this.label4);
            this.panelUst.Controls.Add(this.panel2);
            this.panelUst.Controls.Add(this.txtEvrakno);
            this.panelUst.Controls.Add(this.txtHesapKodu);
            this.panelUst.Controls.Add(this.txtMakaraBarkod);
            this.panelUst.Controls.Add(this.txtUnvan);
            this.panelUst.Controls.Add(this.label3);
            this.panelUst.Controls.Add(this.label2);
            this.panelUst.Controls.Add(this.label1);
            this.panelUst.Location = new System.Drawing.Point(0, 0);
            this.panelUst.Name = "panelUst";
            this.panelUst.Size = new System.Drawing.Size(240, 111);
            // 
            // btnUygula
            // 
            this.btnUygula.BackColor = System.Drawing.Color.SkyBlue;
            this.btnUygula.ForeColor = System.Drawing.Color.Black;
            this.btnUygula.Location = new System.Drawing.Point(181, 2);
            this.btnUygula.Name = "btnUygula";
            this.btnUygula.Size = new System.Drawing.Size(57, 21);
            this.btnUygula.TabIndex = 9;
            this.btnUygula.Text = "+";
            this.btnUygula.Click += new System.EventHandler(this.btnUygula_Click);
            // 
            // txtRafBarkod
            // 
            this.txtRafBarkod.Location = new System.Drawing.Point(74, 2);
            this.txtRafBarkod.Name = "txtRafBarkod";
            this.txtRafBarkod.Size = new System.Drawing.Size(106, 21);
            this.txtRafBarkod.TabIndex = 0;
            this.txtRafBarkod.GotFocus += new System.EventHandler(this.txt_GotFocus);
            // 
            // txtBarkod
            // 
            this.txtBarkod.Location = new System.Drawing.Point(74, 24);
            this.txtBarkod.Name = "txtBarkod";
            this.txtBarkod.Size = new System.Drawing.Size(106, 21);
            this.txtBarkod.TabIndex = 1;
            this.txtBarkod.TextChanged += new System.EventHandler(this.txtBarkod_TextChanged);
            this.txtBarkod.GotFocus += new System.EventHandler(this.txt_GotFocus);
            // 
            // btnEkle
            // 
            this.btnEkle.BackColor = System.Drawing.Color.SkyBlue;
            this.btnEkle.ForeColor = System.Drawing.Color.Black;
            this.btnEkle.Location = new System.Drawing.Point(181, 24);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(57, 21);
            this.btnEkle.TabIndex = 2;
            this.btnEkle.Text = "Ekle";
            this.btnEkle.Click += new System.EventHandler(this.btnUygula_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.CadetBlue;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.SystemColors.Window;
            this.label7.Location = new System.Drawing.Point(3, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 21);
            this.label7.Text = "Raf Okut";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.CadetBlue;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.SystemColors.Window;
            this.label14.Location = new System.Drawing.Point(3, 46);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 21);
            this.label14.Text = "Makara Okut";
            this.label14.Visible = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.CadetBlue;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.SystemColors.Window;
            this.label4.Location = new System.Drawing.Point(3, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 21);
            this.label4.Text = "Mal Okut";
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 111);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(320, 160);
            // 
            // txtEvrakno
            // 
            this.txtEvrakno.Location = new System.Drawing.Point(181, 90);
            this.txtEvrakno.Name = "txtEvrakno";
            this.txtEvrakno.ReadOnly = true;
            this.txtEvrakno.Size = new System.Drawing.Size(58, 21);
            this.txtEvrakno.TabIndex = 5;
            // 
            // txtHesapKodu
            // 
            this.txtHesapKodu.Location = new System.Drawing.Point(74, 90);
            this.txtHesapKodu.Name = "txtHesapKodu";
            this.txtHesapKodu.ReadOnly = true;
            this.txtHesapKodu.Size = new System.Drawing.Size(54, 21);
            this.txtHesapKodu.TabIndex = 4;
            // 
            // txtMakaraBarkod
            // 
            this.txtMakaraBarkod.Location = new System.Drawing.Point(92, 46);
            this.txtMakaraBarkod.Name = "txtMakaraBarkod";
            this.txtMakaraBarkod.Size = new System.Drawing.Size(147, 21);
            this.txtMakaraBarkod.TabIndex = 3;
            this.txtMakaraBarkod.Visible = false;
            // 
            // txtUnvan
            // 
            this.txtUnvan.Location = new System.Drawing.Point(74, 68);
            this.txtUnvan.Name = "txtUnvan";
            this.txtUnvan.ReadOnly = true;
            this.txtUnvan.Size = new System.Drawing.Size(165, 21);
            this.txtUnvan.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Teal;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(129, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 21);
            this.label3.Text = "EvrakNo";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Teal;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(3, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 21);
            this.label2.Text = "HesapKodu";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Teal;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(3, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 21);
            this.label1.Text = "Unvan";
            // 
            // panelOrta
            // 
            this.panelOrta.AutoScroll = true;
            this.panelOrta.BackColor = System.Drawing.Color.White;
            this.panelOrta.Controls.Add(this.lblMakarano);
            this.panelOrta.Controls.Add(this.lblIslemMiktar);
            this.panelOrta.Controls.Add(this.lblYerlestirmeMiktar);
            this.panelOrta.Controls.Add(this.lblOkutulanMiktar);
            this.panelOrta.Controls.Add(this.lblBirim);
            this.panelOrta.Controls.Add(this.lblMiktar);
            this.panelOrta.Controls.Add(this.lblMalzeme);
            this.panelOrta.Controls.Add(this.lblMalkodu);
            this.panelOrta.Location = new System.Drawing.Point(0, 112);
            this.panelOrta.Name = "panelOrta";
            this.panelOrta.Size = new System.Drawing.Size(240, 148);
            // 
            // lblMakarano
            // 
            this.lblMakarano.BackColor = System.Drawing.Color.CadetBlue;
            this.lblMakarano.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblMakarano.ForeColor = System.Drawing.SystemColors.Window;
            this.lblMakarano.Location = new System.Drawing.Point(145, 0);
            this.lblMakarano.Name = "lblMakarano";
            this.lblMakarano.Size = new System.Drawing.Size(72, 18);
            this.lblMakarano.Text = "Makara No";
            // 
            // lblIslemMiktar
            // 
            this.lblIslemMiktar.BackColor = System.Drawing.Color.CadetBlue;
            this.lblIslemMiktar.ForeColor = System.Drawing.SystemColors.Window;
            this.lblIslemMiktar.Location = new System.Drawing.Point(556, 0);
            this.lblIslemMiktar.Name = "lblIslemMiktar";
            this.lblIslemMiktar.Size = new System.Drawing.Size(70, 18);
            this.lblIslemMiktar.Text = "İşlemMiktar";
            // 
            // lblYerlestirmeMiktar
            // 
            this.lblYerlestirmeMiktar.BackColor = System.Drawing.Color.CadetBlue;
            this.lblYerlestirmeMiktar.ForeColor = System.Drawing.SystemColors.Window;
            this.lblYerlestirmeMiktar.Location = new System.Drawing.Point(450, 0);
            this.lblYerlestirmeMiktar.Name = "lblYerlestirmeMiktar";
            this.lblYerlestirmeMiktar.Size = new System.Drawing.Size(105, 18);
            this.lblYerlestirmeMiktar.Text = "Yerleştirme Miktar";
            // 
            // lblOkutulanMiktar
            // 
            this.lblOkutulanMiktar.BackColor = System.Drawing.Color.CadetBlue;
            this.lblOkutulanMiktar.ForeColor = System.Drawing.SystemColors.Window;
            this.lblOkutulanMiktar.Location = new System.Drawing.Point(357, 0);
            this.lblOkutulanMiktar.Name = "lblOkutulanMiktar";
            this.lblOkutulanMiktar.Size = new System.Drawing.Size(92, 18);
            this.lblOkutulanMiktar.Text = "Okutulan Miktar";
            // 
            // lblBirim
            // 
            this.lblBirim.BackColor = System.Drawing.Color.CadetBlue;
            this.lblBirim.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblBirim.ForeColor = System.Drawing.SystemColors.Window;
            this.lblBirim.Location = new System.Drawing.Point(218, 0);
            this.lblBirim.Name = "lblBirim";
            this.lblBirim.Size = new System.Drawing.Size(72, 18);
            this.lblBirim.Text = "Birim";
            // 
            // lblMiktar
            // 
            this.lblMiktar.BackColor = System.Drawing.Color.CadetBlue;
            this.lblMiktar.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblMiktar.ForeColor = System.Drawing.SystemColors.Window;
            this.lblMiktar.Location = new System.Drawing.Point(291, 0);
            this.lblMiktar.Name = "lblMiktar";
            this.lblMiktar.Size = new System.Drawing.Size(65, 18);
            this.lblMiktar.Text = "Miktar";
            // 
            // lblMalzeme
            // 
            this.lblMalzeme.BackColor = System.Drawing.Color.CadetBlue;
            this.lblMalzeme.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblMalzeme.ForeColor = System.Drawing.SystemColors.Window;
            this.lblMalzeme.Location = new System.Drawing.Point(64, 0);
            this.lblMalzeme.Name = "lblMalzeme";
            this.lblMalzeme.Size = new System.Drawing.Size(80, 18);
            this.lblMalzeme.Text = "Malzeme";
            // 
            // lblMalkodu
            // 
            this.lblMalkodu.BackColor = System.Drawing.Color.CadetBlue;
            this.lblMalkodu.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblMalkodu.ForeColor = System.Drawing.SystemColors.Window;
            this.lblMalkodu.Location = new System.Drawing.Point(3, 0);
            this.lblMalkodu.Name = "lblMalkodu";
            this.lblMalkodu.Size = new System.Drawing.Size(60, 18);
            this.lblMalkodu.Text = "Mal Kodu";
            // 
            // panelAlt
            // 
            this.panelAlt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panelAlt.Controls.Add(this.btnBack);
            this.panelAlt.Controls.Add(this.btnKaydet);
            this.panelAlt.Location = new System.Drawing.Point(0, 261);
            this.panelAlt.Name = "panelAlt";
            this.panelAlt.Size = new System.Drawing.Size(240, 33);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Firebrick;
            this.btnBack.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(3, 5);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(70, 23);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "<<  Geri";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnKaydet
            // 
            this.btnKaydet.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKaydet.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnKaydet.ForeColor = System.Drawing.Color.White;
            this.btnKaydet.Location = new System.Drawing.Point(74, 5);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(165, 23);
            this.btnKaydet.TabIndex = 0;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // frmxOps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.panelAlt);
            this.Controls.Add(this.panelOrta);
            this.Controls.Add(this.panelUst);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmxOps";
            this.Text = "WMS Mobil";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MalzemeIslemleri_Closing);
            this.panelUst.ResumeLayout(false);
            this.panelOrta.ResumeLayout(false);
            this.panelAlt.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelUst;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUnvan;
        private System.Windows.Forms.TextBox txtEvrakno;
        private System.Windows.Forms.TextBox txtHesapKodu;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelOrta;
        private System.Windows.Forms.Label lblMalkodu;
        private System.Windows.Forms.Label lblBirim;
        private System.Windows.Forms.Label lblMiktar;
        private System.Windows.Forms.Label lblMalzeme;
        private System.Windows.Forms.Panel panelAlt;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.TextBox txtBarkod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Label lblOkutulanMiktar;
        private System.Windows.Forms.Label lblYerlestirmeMiktar;
        private System.Windows.Forms.TextBox txtRafBarkod;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label lblIslemMiktar;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnUygula;
        private System.Windows.Forms.Label lblMakarano;
        private System.Windows.Forms.TextBox txtMakaraBarkod;
        private System.Windows.Forms.Label label14;
    }
}