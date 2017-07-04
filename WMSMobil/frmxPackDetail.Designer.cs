namespace WMSMobil
{
    partial class frmxPackDetail
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTip = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.txtSevkNo = new System.Windows.Forms.TextBox();
            this.txtPaketNo = new System.Windows.Forms.TextBox();
            this.txtMiktar = new System.Windows.Forms.TextBox();
            this.txtAgirlik = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.CadetBlue;
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(0, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 18);
            this.label1.Text = "Sevkiyat No";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.CadetBlue;
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(0, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(240, 18);
            this.label2.Text = "Paket No";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.CadetBlue;
            this.label3.ForeColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(0, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(240, 18);
            this.label3.Text = "Paket Miktar";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.CadetBlue;
            this.label4.ForeColor = System.Drawing.SystemColors.Window;
            this.label4.Location = new System.Drawing.Point(0, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(240, 18);
            this.label4.Text = "Paket Tipi";
            // 
            // txtTip
            // 
            this.txtTip.Location = new System.Drawing.Point(0, 165);
            this.txtTip.Name = "txtTip";
            this.txtTip.Size = new System.Drawing.Size(240, 22);
            this.txtTip.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.CadetBlue;
            this.label5.ForeColor = System.Drawing.SystemColors.Window;
            this.label5.Location = new System.Drawing.Point(0, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(240, 18);
            this.label5.Text = "Ağırlık";
            // 
            // btnKaydet
            // 
            this.btnKaydet.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKaydet.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnKaydet.ForeColor = System.Drawing.Color.White;
            this.btnKaydet.Location = new System.Drawing.Point(0, 243);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(240, 37);
            this.btnKaydet.TabIndex = 5;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // txtSevkNo
            // 
            this.txtSevkNo.Location = new System.Drawing.Point(0, 27);
            this.txtSevkNo.Name = "txtSevkNo";
            this.txtSevkNo.Size = new System.Drawing.Size(240, 21);
            this.txtSevkNo.TabIndex = 0;
            // 
            // txtPaketNo
            // 
            this.txtPaketNo.Location = new System.Drawing.Point(0, 73);
            this.txtPaketNo.Name = "txtPaketNo";
            this.txtPaketNo.Size = new System.Drawing.Size(240, 21);
            this.txtPaketNo.TabIndex = 1;
            // 
            // txtMiktar
            // 
            this.txtMiktar.Location = new System.Drawing.Point(0, 119);
            this.txtMiktar.Name = "txtMiktar";
            this.txtMiktar.Size = new System.Drawing.Size(240, 21);
            this.txtMiktar.TabIndex = 2;
            // 
            // txtAgirlik
            // 
            this.txtAgirlik.Location = new System.Drawing.Point(0, 212);
            this.txtAgirlik.Name = "txtAgirlik";
            this.txtAgirlik.Size = new System.Drawing.Size(240, 21);
            this.txtAgirlik.TabIndex = 4;
            // 
            // frmxPackDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.txtAgirlik);
            this.Controls.Add(this.txtMiktar);
            this.Controls.Add(this.txtPaketNo);
            this.Controls.Add(this.txtSevkNo);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTip);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmxPackDetail";
            this.Text = "Paket Bilgileri";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox txtTip;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.TextBox txtSevkNo;
        private System.Windows.Forms.TextBox txtPaketNo;
        private System.Windows.Forms.TextBox txtMiktar;
        private System.Windows.Forms.TextBox txtAgirlik;
    }
}