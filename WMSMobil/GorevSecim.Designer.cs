﻿namespace WMSMobil
{
    partial class GorevSecim
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GorevSecim));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnListele = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbDurum = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbGorevli = new System.Windows.Forms.ComboBox();
            this.panelOrta = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLinkeAktar = new System.Windows.Forms.Button();
            this.btnDuzenle = new System.Windows.Forms.Button();
            this.btnIslemYap = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panelOrta.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.btnListele);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.cmbDurum);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cmbGorevli);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 50);
            // 
            // btnListele
            // 
            this.btnListele.BackColor = System.Drawing.Color.SkyBlue;
            this.btnListele.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnListele.ForeColor = System.Drawing.Color.Black;
            this.btnListele.Location = new System.Drawing.Point(186, 3);
            this.btnListele.Name = "btnListele";
            this.btnListele.Size = new System.Drawing.Size(52, 41);
            this.btnListele.TabIndex = 5;
            this.btnListele.Text = "Listele";
            this.btnListele.Click += new System.EventHandler(this.btnListele_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.CadetBlue;
            this.label9.ForeColor = System.Drawing.SystemColors.Window;
            this.label9.Location = new System.Drawing.Point(92, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 18);
            this.label9.Text = "Durum";
            // 
            // cmbDurum
            // 
            this.cmbDurum.Location = new System.Drawing.Point(92, 22);
            this.cmbDurum.Name = "cmbDurum";
            this.cmbDurum.Size = new System.Drawing.Size(90, 22);
            this.cmbDurum.TabIndex = 1;
            this.cmbDurum.SelectedIndexChanged += new System.EventHandler(this.btnListele_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.CadetBlue;
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 18);
            this.label1.Text = "Görevli";
            // 
            // cmbGorevli
            // 
            this.cmbGorevli.Location = new System.Drawing.Point(0, 22);
            this.cmbGorevli.Name = "cmbGorevli";
            this.cmbGorevli.Size = new System.Drawing.Size(90, 22);
            this.cmbGorevli.TabIndex = 1;
            this.cmbGorevli.SelectedIndexChanged += new System.EventHandler(this.btnListele_Click);
            // 
            // panelOrta
            // 
            this.panelOrta.AutoScroll = true;
            this.panelOrta.Controls.Add(this.label8);
            this.panelOrta.Controls.Add(this.label5);
            this.panelOrta.Controls.Add(this.label4);
            this.panelOrta.Controls.Add(this.label3);
            this.panelOrta.Controls.Add(this.label2);
            this.panelOrta.Location = new System.Drawing.Point(0, 50);
            this.panelOrta.Name = "panelOrta";
            this.panelOrta.Size = new System.Drawing.Size(240, 208);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.CadetBlue;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.SystemColors.Window;
            this.label8.Location = new System.Drawing.Point(162, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 18);
            this.label8.Text = "Oluşturma";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.CadetBlue;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.SystemColors.Window;
            this.label5.Location = new System.Drawing.Point(304, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 18);
            this.label5.Text = "Durum";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.CadetBlue;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.SystemColors.Window;
            this.label4.Location = new System.Drawing.Point(243, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 18);
            this.label4.Text = "Görevli";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.CadetBlue;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(61, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 18);
            this.label3.Text = "Bilgiler";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.CadetBlue;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 18);
            this.label2.Text = "GörevNo";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLinkeAktar);
            this.panel1.Controls.Add(this.btnDuzenle);
            this.panel1.Controls.Add(this.btnIslemYap);
            this.panel1.Location = new System.Drawing.Point(0, 264);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 30);
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
            // btnDuzenle
            // 
            this.btnDuzenle.BackColor = System.Drawing.Color.SkyBlue;
            this.btnDuzenle.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnDuzenle.ForeColor = System.Drawing.Color.Black;
            this.btnDuzenle.Location = new System.Drawing.Point(74, 4);
            this.btnDuzenle.Name = "btnDuzenle";
            this.btnDuzenle.Size = new System.Drawing.Size(65, 22);
            this.btnDuzenle.TabIndex = 0;
            this.btnDuzenle.Text = "Tüm Liste";
            this.btnDuzenle.Click += new System.EventHandler(this.btnDuzenle_Click);
            // 
            // btnIslemYap
            // 
            this.btnIslemYap.BackColor = System.Drawing.Color.SkyBlue;
            this.btnIslemYap.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnIslemYap.ForeColor = System.Drawing.Color.Black;
            this.btnIslemYap.Location = new System.Drawing.Point(3, 4);
            this.btnIslemYap.Name = "btnIslemYap";
            this.btnIslemYap.Size = new System.Drawing.Size(68, 22);
            this.btnIslemYap.TabIndex = 0;
            this.btnIslemYap.Text = "Devam Et";
            this.btnIslemYap.Click += new System.EventHandler(this.btnIslemYap_Click);
            // 
            // GorevSecim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelOrta);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GorevSecim";
            this.Text = "MalKabul";
            this.panel2.ResumeLayout(false);
            this.panelOrta.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnListele;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbDurum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbGorevli;
        private System.Windows.Forms.Panel panelOrta;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDuzenle;
        private System.Windows.Forms.Button btnIslemYap;
        private System.Windows.Forms.Button btnLinkeAktar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;

    }
}