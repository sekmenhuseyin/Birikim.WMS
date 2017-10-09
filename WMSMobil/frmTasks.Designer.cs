namespace WMSMobil
{
    partial class frmTasks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTasks));
            this.panelUst = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDurum = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbGorevli = new System.Windows.Forms.ComboBox();
            this.panelOrta = new System.Windows.Forms.Panel();
            this.lblTarih = new System.Windows.Forms.Label();
            this.lblGorevli = new System.Windows.Forms.Label();
            this.lblBilgiler = new System.Windows.Forms.Label();
            this.lblGorevNo = new System.Windows.Forms.Label();
            this.panelAlt = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnLinkeAktar = new System.Windows.Forms.Button();
            this.btnIslemYap = new System.Windows.Forms.Button();
            this.panelUst.SuspendLayout();
            this.panelOrta.SuspendLayout();
            this.panelAlt.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelUst
            // 
            this.panelUst.BackColor = System.Drawing.Color.White;
            this.panelUst.Controls.Add(this.label2);
            this.panelUst.Controls.Add(this.cmbDurum);
            this.panelUst.Controls.Add(this.label1);
            this.panelUst.Controls.Add(this.cmbGorevli);
            this.panelUst.Location = new System.Drawing.Point(0, 0);
            this.panelUst.Name = "panelUst";
            this.panelUst.Size = new System.Drawing.Size(240, 50);
            this.panelUst.Tag = "2";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.CadetBlue;
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(125, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 18);
            this.label2.Text = "Durum";
            // 
            // cmbDurum
            // 
            this.cmbDurum.Location = new System.Drawing.Point(125, 22);
            this.cmbDurum.Name = "cmbDurum";
            this.cmbDurum.Size = new System.Drawing.Size(112, 22);
            this.cmbDurum.TabIndex = 0;
            this.cmbDurum.SelectedIndexChanged += new System.EventHandler(this.btnListele_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.CadetBlue;
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 18);
            this.label1.Text = "Görevli";
            // 
            // cmbGorevli
            // 
            this.cmbGorevli.Location = new System.Drawing.Point(0, 22);
            this.cmbGorevli.Name = "cmbGorevli";
            this.cmbGorevli.Size = new System.Drawing.Size(122, 22);
            this.cmbGorevli.TabIndex = 1;
            this.cmbGorevli.SelectedIndexChanged += new System.EventHandler(this.btnListele_Click);
            // 
            // panelOrta
            // 
            this.panelOrta.AutoScroll = true;
            this.panelOrta.Controls.Add(this.lblTarih);
            this.panelOrta.Controls.Add(this.lblGorevli);
            this.panelOrta.Controls.Add(this.lblBilgiler);
            this.panelOrta.Controls.Add(this.lblGorevNo);
            this.panelOrta.Location = new System.Drawing.Point(0, 50);
            this.panelOrta.Name = "panelOrta";
            this.panelOrta.Size = new System.Drawing.Size(240, 208);
            this.panelOrta.Tag = "2";
            // 
            // lblTarih
            // 
            this.lblTarih.BackColor = System.Drawing.Color.CadetBlue;
            this.lblTarih.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblTarih.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTarih.Location = new System.Drawing.Point(203, 0);
            this.lblTarih.Name = "lblTarih";
            this.lblTarih.Size = new System.Drawing.Size(70, 18);
            this.lblTarih.Tag = "1";
            this.lblTarih.Text = "Oluşturma";
            // 
            // lblGorevli
            // 
            this.lblGorevli.BackColor = System.Drawing.Color.CadetBlue;
            this.lblGorevli.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblGorevli.ForeColor = System.Drawing.SystemColors.Window;
            this.lblGorevli.Location = new System.Drawing.Point(157, 0);
            this.lblGorevli.Name = "lblGorevli";
            this.lblGorevli.Size = new System.Drawing.Size(45, 18);
            this.lblGorevli.Tag = "1";
            this.lblGorevli.Text = "Görevli";
            // 
            // lblBilgiler
            // 
            this.lblBilgiler.BackColor = System.Drawing.Color.CadetBlue;
            this.lblBilgiler.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblBilgiler.ForeColor = System.Drawing.SystemColors.Window;
            this.lblBilgiler.Location = new System.Drawing.Point(56, 0);
            this.lblBilgiler.Name = "lblBilgiler";
            this.lblBilgiler.Size = new System.Drawing.Size(100, 18);
            this.lblBilgiler.Tag = "1";
            this.lblBilgiler.Text = "Bilgiler";
            // 
            // lblGorevNo
            // 
            this.lblGorevNo.BackColor = System.Drawing.Color.CadetBlue;
            this.lblGorevNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblGorevNo.ForeColor = System.Drawing.SystemColors.Window;
            this.lblGorevNo.Location = new System.Drawing.Point(0, 0);
            this.lblGorevNo.Name = "lblGorevNo";
            this.lblGorevNo.Size = new System.Drawing.Size(55, 18);
            this.lblGorevNo.Tag = "1";
            this.lblGorevNo.Text = "GörevNo";
            // 
            // panelAlt
            // 
            this.panelAlt.Controls.Add(this.btnBack);
            this.panelAlt.Controls.Add(this.btnLinkeAktar);
            this.panelAlt.Controls.Add(this.btnIslemYap);
            this.panelAlt.Location = new System.Drawing.Point(0, 264);
            this.panelAlt.Name = "panelAlt";
            this.panelAlt.Size = new System.Drawing.Size(240, 30);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Firebrick;
            this.btnBack.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(3, 5);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(63, 21);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "<<  Geri";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnLinkeAktar
            // 
            this.btnLinkeAktar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnLinkeAktar.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnLinkeAktar.ForeColor = System.Drawing.Color.White;
            this.btnLinkeAktar.Location = new System.Drawing.Point(142, 4);
            this.btnLinkeAktar.Name = "btnLinkeAktar";
            this.btnLinkeAktar.Size = new System.Drawing.Size(95, 22);
            this.btnLinkeAktar.TabIndex = 0;
            this.btnLinkeAktar.Text = "Linke Aktar";
            this.btnLinkeAktar.Click += new System.EventHandler(this.btnLinkeAktar_Click);
            // 
            // btnIslemYap
            // 
            this.btnIslemYap.BackColor = System.Drawing.Color.SkyBlue;
            this.btnIslemYap.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnIslemYap.ForeColor = System.Drawing.Color.Black;
            this.btnIslemYap.Location = new System.Drawing.Point(70, 5);
            this.btnIslemYap.Name = "btnIslemYap";
            this.btnIslemYap.Size = new System.Drawing.Size(68, 21);
            this.btnIslemYap.TabIndex = 1;
            this.btnIslemYap.Text = "İşlem Yap";
            this.btnIslemYap.Click += new System.EventHandler(this.btnIslemYap_Click);
            // 
            // frmTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.panelAlt);
            this.Controls.Add(this.panelUst);
            this.Controls.Add(this.panelOrta);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmTasks";
            this.Text = "Görevler - WMS Mobil";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmTasks_Closing);
            this.panelUst.ResumeLayout(false);
            this.panelOrta.ResumeLayout(false);
            this.panelAlt.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelUst;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbDurum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbGorevli;
        private System.Windows.Forms.Panel panelOrta;
        private System.Windows.Forms.Label lblTarih;
        private System.Windows.Forms.Label lblBilgiler;
        private System.Windows.Forms.Label lblGorevNo;
        private System.Windows.Forms.Panel panelAlt;
        private System.Windows.Forms.Button btnIslemYap;
        private System.Windows.Forms.Button btnLinkeAktar;
        private System.Windows.Forms.Label lblGorevli;
        private System.Windows.Forms.Button btnBack;

    }
}