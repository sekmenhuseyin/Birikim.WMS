using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportSipGM
/// </summary>
public class XtraReportSipGM : DevExpress.XtraReports.UI.XtraReport
{
    DevExpress.XtraReports.UI.DetailBand Detail;
    DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    XRLabel xrLabel1;
    DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
    System.Data.DataSet dataSet1;
    System.Data.DataSet dataSet2;
    ReportHeaderBand ReportHeader;
    XRPictureBox xrPictureBox1;
    PageHeaderBand PageHeader;
    ReportFooterBand ReportFooter;
    XRPictureBox xrPictureBox2;
    XRLabel xrLabel3;
    XRLabel xrLabel2;
    XRLabel xrLabel20;
    XRLabel xrLabel21;
    XRLabel xrLabel18;
    XRLabel xrLabel19;
    XRLabel xrLabel17;
    XRLabel xrLabel16;
    XRLabel xrLabel15;
    XRLabel xrLabel13;
    XRLabel xrLabel14;
    XRLabel xrLabel11;
    XRLabel xrLabel12;
    XRLabel xrLabel10;
    XRLabel xrLabel9;
    XRLabel xrLabel8;
    XRLabel xrLabel7;
    XRLabel xrLabel6;
    XRLabel xrLabel4;
    XRLabel xrLabel5;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    System.ComponentModel.IContainer components;

    public XtraReportSipGM()
    {
        InitializeComponent();
    }

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

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        var resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraReportSipGM));
        Detail = new DevExpress.XtraReports.UI.DetailBand();
        xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(components);
        dataSet1 = new System.Data.DataSet();
        dataSet2 = new System.Data.DataSet();
        ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(dataSet1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(dataSet2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrLabel1});
        Detail.HeightF = 51.04167F;
        Detail.Name = "Detail";
        Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel1
        // 
        xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 10.00001F);
        xrLabel1.Name = "xrLabel1";
        xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel1.SizeF = new System.Drawing.SizeF(100F, 23F);
        xrLabel1.Text = "xrLabel1";
        // 
        // TopMargin
        // 
        TopMargin.HeightF = 49F;
        TopMargin.Name = "TopMargin";
        TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        BottomMargin.HeightF = 56F;
        BottomMargin.Name = "BottomMargin";
        BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // sqlDataSource1
        // 
        sqlDataSource1.Name = "sqlDataSource1";
        // 
        // dataSet1
        // 
        dataSet1.DataSetName = "NewDataSet";
        // 
        // dataSet2
        // 
        dataSet2.DataSetName = "NewDataSet";
        // 
        // ReportHeader
        // 
        ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrLabel20,
            xrLabel21,
            xrLabel18,
            xrLabel19,
            xrLabel17,
            xrLabel16,
            xrLabel15,
            xrLabel13,
            xrLabel14,
            xrLabel11,
            xrLabel12,
            xrLabel10,
            xrLabel9,
            xrLabel8,
            xrLabel7,
            xrLabel6,
            xrLabel4,
            xrLabel3,
            xrLabel2,
            xrPictureBox2,
            xrPictureBox1,
            xrLabel5});
        ReportHeader.HeightF = 303.6668F;
        ReportHeader.Name = "ReportHeader";
        // 
        // xrLabel3
        // 
        xrLabel3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(200F, 31.33334F);
        xrLabel3.Multiline = true;
        xrLabel3.Name = "xrLabel3";
        xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel3.SizeF = new System.Drawing.SizeF(400.0416F, 83.33335F);
        xrLabel3.StylePriority.UseFont = false;
        xrLabel3.StylePriority.UseTextAlignment = false;
        xrLabel3.Text = "ESENTEPE MAHALLESİ ADLİYE SARAYI KARŞISI NO:8\r\nVEZİRKÖPRÜ / SAMSUN\r\nVEZİRKÖPRÜ VE" +
"RGİ DAİRESİ : 9250056973\r\nMERSİS NO : 0925005697300017\r\nFİRMAMIZ E-FATURA E-ARŞİ" +
"V SİSTEMİNE DAHİLDİR";
        xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrLabel2
        // 
        xrLabel2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(200F, 0F);
        xrLabel2.Name = "xrLabel2";
        xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel2.SizeF = new System.Drawing.SizeF(400.0416F, 31.33334F);
        xrLabel2.StylePriority.UseFont = false;
        xrLabel2.StylePriority.UseTextAlignment = false;
        xrLabel2.Text = "VEZİRKÖPRÜ ORMAN ÜRÜNLERİ VE KAĞIT SANAYİ A.Ş. ";
        xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrPictureBox2
        // 
        xrPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox2.Image")));
        xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(600.0417F, 0F);
        xrPictureBox2.Name = "xrPictureBox2";
        xrPictureBox2.SizeF = new System.Drawing.SizeF(198.9584F, 114.6667F);
        xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
        // 
        // xrPictureBox1
        // 
        xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
        xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrPictureBox1.Name = "xrPictureBox1";
        xrPictureBox1.SizeF = new System.Drawing.SizeF(200F, 114.6667F);
        xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
        // 
        // PageHeader
        // 
        PageHeader.HeightF = 40.625F;
        PageHeader.Name = "PageHeader";
        // 
        // ReportFooter
        // 
        ReportFooter.HeightF = 100F;
        ReportFooter.Name = "ReportFooter";
        // 
        // xrLabel4
        // 
        xrLabel4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
        xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(231.25F, 139.5833F);
        xrLabel4.Name = "xrLabel4";
        xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel4.SizeF = new System.Drawing.SizeF(320.875F, 31.33334F);
        xrLabel4.StylePriority.UseFont = false;
        xrLabel4.StylePriority.UseTextAlignment = false;
        xrLabel4.Text = "SATINALMA SİPARİŞ FORMU";
        xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel5
        // 
        xrLabel5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 179.125F);
        xrLabel5.Name = "xrLabel5";
        xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel5.SizeF = new System.Drawing.SizeF(123.9583F, 17.79167F);
        xrLabel5.StylePriority.UseFont = false;
        xrLabel5.Text = "Tedarikçi Bilgileri";
        // 
        // xrLabel6
        // 
        xrLabel6.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 196.9167F);
        xrLabel6.Name = "xrLabel6";
        xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel6.SizeF = new System.Drawing.SizeF(100F, 17.79167F);
        xrLabel6.StylePriority.UseFont = false;
        xrLabel6.Text = "Ünvanı";
        // 
        // xrLabel7
        // 
        xrLabel7.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 214.7084F);
        xrLabel7.Name = "xrLabel7";
        xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel7.SizeF = new System.Drawing.SizeF(100F, 17.79167F);
        xrLabel7.StylePriority.UseFont = false;
        xrLabel7.Text = "Adresi";
        // 
        // xrLabel8
        // 
        xrLabel8.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 232.5001F);
        xrLabel8.Name = "xrLabel8";
        xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel8.SizeF = new System.Drawing.SizeF(100F, 17.79167F);
        xrLabel8.StylePriority.UseFont = false;
        xrLabel8.Text = "Tel";
        // 
        // xrLabel9
        // 
        xrLabel9.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 250.2918F);
        xrLabel9.Name = "xrLabel9";
        xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel9.SizeF = new System.Drawing.SizeF(100F, 17.79167F);
        xrLabel9.StylePriority.UseFont = false;
        xrLabel9.Text = "Fax";
        // 
        // xrLabel10
        // 
        xrLabel10.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 268.0834F);
        xrLabel10.Name = "xrLabel10";
        xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel10.SizeF = new System.Drawing.SizeF(100F, 17.79167F);
        xrLabel10.StylePriority.UseFont = false;
        xrLabel10.Text = "Email";
        // 
        // xrLabel11
        // 
        xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(100F, 268.0834F);
        xrLabel11.Name = "xrLabel11";
        xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel11.SizeF = new System.Drawing.SizeF(284.375F, 17.79169F);
        xrLabel11.Text = "xrLabel5";
        // 
        // xrLabel12
        // 
        xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(100F, 196.9167F);
        xrLabel12.Name = "xrLabel12";
        xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel12.SizeF = new System.Drawing.SizeF(284.375F, 17.79167F);
        xrLabel12.Text = "xrLabel5";
        // 
        // xrLabel13
        // 
        xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(100F, 232.5001F);
        xrLabel13.Name = "xrLabel13";
        xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel13.SizeF = new System.Drawing.SizeF(284.375F, 17.79169F);
        xrLabel13.Text = "xrLabel5";
        // 
        // xrLabel14
        // 
        xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(100F, 214.7084F);
        xrLabel14.Name = "xrLabel14";
        xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel14.SizeF = new System.Drawing.SizeF(284.375F, 17.79167F);
        xrLabel14.Text = "xrLabel5";
        // 
        // xrLabel15
        // 
        xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(100F, 250.2918F);
        xrLabel15.Name = "xrLabel15";
        xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel15.SizeF = new System.Drawing.SizeF(284.375F, 17.79167F);
        xrLabel15.Text = "xrLabel5";
        // 
        // xrLabel16
        // 
        xrLabel16.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(683.9583F, 179.125F);
        xrLabel16.Name = "xrLabel16";
        xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel16.SizeF = new System.Drawing.SizeF(115.0416F, 17.79167F);
        xrLabel16.StylePriority.UseFont = false;
        // 
        // xrLabel17
        // 
        xrLabel17.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(573.9583F, 179.125F);
        xrLabel17.Name = "xrLabel17";
        xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel17.SizeF = new System.Drawing.SizeF(110F, 17.79167F);
        xrLabel17.StylePriority.UseFont = false;
        xrLabel17.Text = "Tarih";
        // 
        // xrLabel18
        // 
        xrLabel18.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
        xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(573.9583F, 196.9167F);
        xrLabel18.Name = "xrLabel18";
        xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel18.SizeF = new System.Drawing.SizeF(110F, 17.79167F);
        xrLabel18.StylePriority.UseFont = false;
        xrLabel18.Text = "Sipariş No";
        // 
        // xrLabel19
        // 
        xrLabel19.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(683.9583F, 196.9167F);
        xrLabel19.Name = "xrLabel19";
        xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel19.SizeF = new System.Drawing.SizeF(115.0416F, 17.79167F);
        xrLabel19.StylePriority.UseFont = false;
        // 
        // xrLabel20
        // 
        xrLabel20.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
        xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(573.9583F, 214.7084F);
        xrLabel20.Name = "xrLabel20";
        xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel20.SizeF = new System.Drawing.SizeF(110F, 17.79167F);
        xrLabel20.StylePriority.UseFont = false;
        xrLabel20.Text = "Sipariş Tarihi";
        // 
        // xrLabel21
        // 
        xrLabel21.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(683.9583F, 214.7084F);
        xrLabel21.Name = "xrLabel21";
        xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel21.SizeF = new System.Drawing.SizeF(115.0416F, 17.79167F);
        xrLabel21.StylePriority.UseFont = false;
        // 
        // XtraReportSipGM
        // 
        Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail,
            TopMargin,
            BottomMargin,
            ReportHeader,
            PageHeader,
            ReportFooter});
        ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            sqlDataSource1});
        DataSource = sqlDataSource1;
        Margins = new System.Drawing.Printing.Margins(23, 28, 49, 56);
        Version = "17.1";
        ((System.ComponentModel.ISupportInitialize)(dataSet1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(dataSet2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
    }

    #endregion
}