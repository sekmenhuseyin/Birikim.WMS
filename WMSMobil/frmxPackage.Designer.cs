namespace WMSMobil
{
    partial class frmxPackage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.panelUst = new System.Windows.Forms.Panel();
            this.txtRafBarkod = new System.Windows.Forms.TextBox();
            this.btnEkle = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtEvrakno = new System.Windows.Forms.TextBox();
            this.txtHesapKodu = new System.Windows.Forms.TextBox();
            this.txtUnvan = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.panelUst.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // panelUst
            // 
            this.panelUst.BackColor = System.Drawing.Color.Transparent;
            this.panelUst.Controls.Add(this.txtRafBarkod);
            this.panelUst.Controls.Add(this.btnEkle);
            this.panelUst.Controls.Add(this.label7);
            this.panelUst.Controls.Add(this.panel2);
            this.panelUst.Controls.Add(this.txtEvrakno);
            this.panelUst.Controls.Add(this.txtHesapKodu);
            this.panelUst.Controls.Add(this.txtUnvan);
            this.panelUst.Controls.Add(this.label3);
            this.panelUst.Controls.Add(this.label2);
            this.panelUst.Controls.Add(this.label1);
            this.panelUst.Location = new System.Drawing.Point(0, 0);
            this.panelUst.Name = "panelUst";
            this.panelUst.Size = new System.Drawing.Size(240, 69);
            // 
            // txtRafBarkod
            // 
            this.txtRafBarkod.Location = new System.Drawing.Point(74, 2);
            this.txtRafBarkod.Name = "txtRafBarkod";
            this.txtRafBarkod.Size = new System.Drawing.Size(106, 21);
            this.txtRafBarkod.TabIndex = 0;
            this.txtRafBarkod.Visible = false;
            // 
            // btnEkle
            // 
            this.btnEkle.BackColor = System.Drawing.Color.SkyBlue;
            this.btnEkle.ForeColor = System.Drawing.Color.Black;
            this.btnEkle.Location = new System.Drawing.Point(181, 2);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(57, 21);
            this.btnEkle.TabIndex = 2;
            this.btnEkle.Text = "Oku";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.CadetBlue;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.SystemColors.Window;
            this.label7.Location = new System.Drawing.Point(3, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 21);
            this.label7.Text = "Barkod";
            this.label7.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 99);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(320, 160);
            // 
            // txtEvrakno
            // 
            this.txtEvrakno.Location = new System.Drawing.Point(181, 46);
            this.txtEvrakno.Name = "txtEvrakno";
            this.txtEvrakno.ReadOnly = true;
            this.txtEvrakno.Size = new System.Drawing.Size(58, 21);
            this.txtEvrakno.TabIndex = 5;
            // 
            // txtHesapKodu
            // 
            this.txtHesapKodu.Location = new System.Drawing.Point(74, 46);
            this.txtHesapKodu.Name = "txtHesapKodu";
            this.txtHesapKodu.ReadOnly = true;
            this.txtHesapKodu.Size = new System.Drawing.Size(54, 21);
            this.txtHesapKodu.TabIndex = 4;
            // 
            // txtUnvan
            // 
            this.txtUnvan.Location = new System.Drawing.Point(74, 24);
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
            this.label3.Location = new System.Drawing.Point(129, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 21);
            this.label3.Text = "EvrakNo";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Teal;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(3, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 21);
            this.label2.Text = "HesapKodu";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Teal;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(3, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 21);
            this.label1.Text = "Unvan";
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "<< Geri";
            // 
            // frmxPackage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.panelUst);
            this.Menu = this.mainMenu1;
            this.Name = "frmxPackage";
            this.Text = "frmxPackage";
            this.panelUst.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelUst;
        private System.Windows.Forms.TextBox txtRafBarkod;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtEvrakno;
        private System.Windows.Forms.TextBox txtHesapKodu;
        private System.Windows.Forms.TextBox txtUnvan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuItem menuItem1;
    }
}