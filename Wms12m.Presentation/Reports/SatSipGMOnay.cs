using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for SatSipGMOnay
/// </summary>
public class SatSipGMOnay : DevExpress.XtraReports.UI.XtraReport
{
    DevExpress.XtraReports.UI.DetailBand Detail;
    DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    XRLabel xrLabel60;
    XRLabel xrLabel61;
    XRLabel xrLabel62;
    XRLabel xrLabel63;
    XRLabel xrLabel64;
    XRLabel xrLabel65;
    XRLabel xrLabel66;
    XRLabel xrLabel67;
    XRLabel xrLabel68;
    XRLabel xrLabel69;
    XRLabel xrLabel70;
    XRLabel xrLabel71;
    XRLabel xrLabel72;
    XRLabel xrLabel73;
    XRLabel xrLabel74;
    XRLabel xrLabel75;
    XRLabel xrLabel76;
    XRLabel xrLabel77;
    XRCheckBox xrCheckBox1;
    XRLabel xrLabel78;
    XRLabel xrLabel79;
    XRLabel xrLabel80;
    XRLabel xrLabel81;
    XRLabel xrLabel82;
    XRLabel xrLabel83;
    XRLabel xrLabel84;
    XRLabel xrLabel85;
    XRLabel xrLabel86;
    XRLabel xrLabel87;
    XRLabel xrLabel88;
    XRLabel xrLabel89;
    XRLabel xrLabel90;
    XRLabel xrLabel91;
    XRLabel xrLabel92;
    XRLabel xrLabel93;
    XRLabel xrLabel94;
    XRLabel xrLabel95;
    XRLabel xrLabel96;
    XRLabel xrLabel97;
    XRLabel xrLabel98;
    XRLabel xrLabel99;
    XRLabel xrLabel100;
    XRLabel xrLabel101;
    XRLabel xrLabel102;
    XRLabel xrLabel103;
    XRLabel xrLabel104;
    XRLabel xrLabel105;
    XRLabel xrLabel106;
    XRLabel xrLabel107;
    XRLabel xrLabel108;
    XRLabel xrLabel109;
    XRLabel xrLabel110;
    XRLabel xrLabel111;
    XRLabel xrLabel112;
    XRLabel xrLabel113;
    XRLabel xrLabel114;
    XRLabel xrLabel115;
    XRLabel xrLabel116;
    XRLabel xrLabel117;
    DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    ReportHeaderBand reportHeaderBand1;
    XRControlStyle Title;
    XRControlStyle FieldCaption;
    XRControlStyle PageInfo;
    XRControlStyle DataField;
    XRPictureBox xrPictureBox1;
    XRLabel xrLabel118;
    XRLabel xrLabel119;
    XRPictureBox xrPictureBox2;
    XRLabel xrLabel120;
    XRLabel xrLabel121;
    XRLabel xrLabel122;
    XRLabel xrLabel123;
    XRLabel xrLabel124;
    XRLabel xrLabel125;
    XRLabel xrLabel126;
    XRLabel xrLabel127;
    XRLabel xrLabel128;
    XRLabel xrLabel129;
    XRLabel xrLabel130;
    XRLabel xrLabel131;
    XRLabel xrLabel133;
    XRLabel xrLabel134;
    XRLabel xrLabel135;
    XRLabel xrLabel136;
    XRLabel xrLabel137;
    XRPageInfo xrPageInfo3;
    PageHeaderBand PageHeader;
    XRTable xrTable1;
    XRTableRow xrTableRow1;
    XRTableCell xrTableCell3;
    XRTableCell xrTableCell4;
    XRTableCell xrTableCell2;
    XRTableCell xrTableCell5;
    XRTableCell xrTableCell6;
    XRTableCell xrTableCell7;
    XRTableCell xrTableCell8;
    XRTableCell xrTableCell9;
    XRTableCell xrTableCell10;
    XRTableCell xrTableCell11;
    XRTableCell xrTableCell12;
    XRTableCell xrTableCell13;
    XRTableCell xrTableCell1;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    System.ComponentModel.IContainer components;

    public SatSipGMOnay()
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
        var resources = new System.ComponentModel.ComponentResourceManager(typeof(SatSipGMOnay));
        Detail = new DevExpress.XtraReports.UI.DetailBand();
        TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(components);
        xrLabel60 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel61 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel62 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel63 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel64 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel65 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel66 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel67 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel68 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel69 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel70 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel71 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel72 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel73 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel74 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel75 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel76 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel77 = new DevExpress.XtraReports.UI.XRLabel();
        xrCheckBox1 = new DevExpress.XtraReports.UI.XRCheckBox();
        xrLabel78 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel79 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel80 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel81 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel82 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel83 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel84 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel85 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel86 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel87 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel88 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel89 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel90 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel91 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel92 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel93 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel94 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel95 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel96 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel97 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel98 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel99 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel100 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel101 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel102 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel103 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel104 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel105 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel106 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel107 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel108 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel109 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel110 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel111 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel112 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel113 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel114 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel115 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel116 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel117 = new DevExpress.XtraReports.UI.XRLabel();
        reportHeaderBand1 = new DevExpress.XtraReports.UI.ReportHeaderBand();
        Title = new DevExpress.XtraReports.UI.XRControlStyle();
        FieldCaption = new DevExpress.XtraReports.UI.XRControlStyle();
        PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
        DataField = new DevExpress.XtraReports.UI.XRControlStyle();
        xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        xrLabel118 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel119 = new DevExpress.XtraReports.UI.XRLabel();
        xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        xrLabel120 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel121 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel122 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel123 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel124 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel125 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel126 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel127 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel128 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel129 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel130 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel131 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel133 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel134 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel135 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel136 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel137 = new DevExpress.XtraReports.UI.XRLabel();
        xrPageInfo3 = new DevExpress.XtraReports.UI.XRPageInfo();
        PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        ((System.ComponentModel.ISupportInitialize)(objectDataSource1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrLabel60,
            xrLabel61,
            xrLabel62,
            xrLabel63,
            xrLabel64,
            xrLabel65,
            xrLabel66,
            xrLabel67,
            xrLabel68,
            xrLabel69,
            xrLabel70,
            xrLabel71,
            xrLabel72,
            xrLabel73,
            xrLabel74,
            xrLabel75,
            xrLabel76,
            xrLabel77,
            xrCheckBox1,
            xrLabel78,
            xrLabel79,
            xrLabel80,
            xrLabel81,
            xrLabel82,
            xrLabel83,
            xrLabel84,
            xrLabel85,
            xrLabel86,
            xrLabel87,
            xrLabel88,
            xrLabel89,
            xrLabel90,
            xrLabel91,
            xrLabel92,
            xrLabel93,
            xrLabel94,
            xrLabel95,
            xrLabel96,
            xrLabel97,
            xrLabel98,
            xrLabel99,
            xrLabel100,
            xrLabel101,
            xrLabel102,
            xrLabel103,
            xrLabel104,
            xrLabel105,
            xrLabel106,
            xrLabel107,
            xrLabel108,
            xrLabel109,
            xrLabel110,
            xrLabel111,
            xrLabel112,
            xrLabel113,
            xrLabel114,
            xrLabel115,
            xrLabel116,
            xrLabel117});
        Detail.HeightF = 1430F;
        Detail.Name = "Detail";
        Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // TopMargin
        // 
        TopMargin.HeightF = 100F;
        TopMargin.Name = "TopMargin";
        TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        BottomMargin.HeightF = 100F;
        BottomMargin.Name = "BottomMargin";
        BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // objectDataSource1
        // 
        objectDataSource1.DataSource = typeof(Wms12m.Entity.SatTalep);
        objectDataSource1.Name = "objectDataSource1";
        // 
        // xrLabel60
        // 
        xrLabel60.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Aciklama")});
        xrLabel60.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrLabel60.Name = "xrLabel60";
        xrLabel60.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel60.SizeF = new System.Drawing.SizeF(20F, 18F);
        xrLabel60.StyleName = "DataField";
        xrLabel60.Text = "xrLabel60";
        // 
        // xrLabel61
        // 
        xrLabel61.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MalKodu")});
        xrLabel61.LocationFloat = new DevExpress.Utils.PointFloat(20F, 0F);
        xrLabel61.Name = "xrLabel61";
        xrLabel61.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel61.SizeF = new System.Drawing.SizeF(79.99998F, 18F);
        xrLabel61.StyleName = "DataField";
        // 
        // xrLabel62
        // 
        xrLabel62.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MalAdi")});
        xrLabel62.LocationFloat = new DevExpress.Utils.PointFloat(99.99998F, 0F);
        xrLabel62.Name = "xrLabel62";
        xrLabel62.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel62.SizeF = new System.Drawing.SizeF(111.46F, 18F);
        xrLabel62.StyleName = "DataField";
        // 
        // xrLabel63
        // 
        xrLabel63.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Aciklama")});
        xrLabel63.LocationFloat = new DevExpress.Utils.PointFloat(211.46F, 0F);
        xrLabel63.Name = "xrLabel63";
        xrLabel63.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel63.SizeF = new System.Drawing.SizeF(105.2101F, 18F);
        xrLabel63.StyleName = "DataField";
        // 
        // xrLabel64
        // 
        xrLabel64.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "IstenenTarih")});
        xrLabel64.LocationFloat = new DevExpress.Utils.PointFloat(316.6701F, 0F);
        xrLabel64.Name = "xrLabel64";
        xrLabel64.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel64.SizeF = new System.Drawing.SizeF(63.53986F, 18F);
        xrLabel64.StyleName = "DataField";
        // 
        // xrLabel65
        // 
        xrLabel65.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BirimMiktar", "{0:C2}")});
        xrLabel65.LocationFloat = new DevExpress.Utils.PointFloat(380.21F, 0F);
        xrLabel65.Name = "xrLabel65";
        xrLabel65.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel65.SizeF = new System.Drawing.SizeF(71.28998F, 18F);
        xrLabel65.StyleName = "DataField";
        xrLabel65.Text = "xrLabel65";
        // 
        // xrLabel66
        // 
        xrLabel66.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DegisTarih")});
        xrLabel66.LocationFloat = new DevExpress.Utils.PointFloat(451.5F, 0F);
        xrLabel66.Name = "xrLabel66";
        xrLabel66.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel66.SizeF = new System.Drawing.SizeF(55.58002F, 18F);
        xrLabel66.StyleName = "DataField";
        xrLabel66.Text = "xrLabel66";
        // 
        // xrLabel67
        // 
        xrLabel67.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Degistiren")});
        xrLabel67.LocationFloat = new DevExpress.Utils.PointFloat(507.08F, 0F);
        xrLabel67.Name = "xrLabel67";
        xrLabel67.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel67.SizeF = new System.Drawing.SizeF(48.66998F, 18F);
        xrLabel67.StyleName = "DataField";
        xrLabel67.Text = "xrLabel67";
        // 
        // xrLabel68
        // 
        xrLabel68.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Durum")});
        xrLabel68.LocationFloat = new DevExpress.Utils.PointFloat(557.3334F, 0F);
        xrLabel68.Name = "xrLabel68";
        xrLabel68.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel68.SizeF = new System.Drawing.SizeF(50.29657F, 18F);
        xrLabel68.StyleName = "DataField";
        xrLabel68.Text = "xrLabel68";
        // 
        // xrLabel69
        // 
        xrLabel69.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DvzCinsi")});
        xrLabel69.LocationFloat = new DevExpress.Utils.PointFloat(607.63F, 0F);
        xrLabel69.Name = "xrLabel69";
        xrLabel69.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel69.SizeF = new System.Drawing.SizeF(58.38F, 18F);
        xrLabel69.StyleName = "DataField";
        xrLabel69.Text = "xrLabel69";
        // 
        // xrLabel70
        // 
        xrLabel70.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DvzKuru")});
        xrLabel70.LocationFloat = new DevExpress.Utils.PointFloat(666.0099F, 0F);
        xrLabel70.Name = "xrLabel70";
        xrLabel70.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel70.SizeF = new System.Drawing.SizeF(42.09009F, 18.00002F);
        xrLabel70.StyleName = "DataField";
        xrLabel70.Text = "xrLabel70";
        // 
        // xrLabel71
        // 
        xrLabel71.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DvzTL")});
        xrLabel71.LocationFloat = new DevExpress.Utils.PointFloat(708.1F, 0F);
        xrLabel71.Name = "xrLabel71";
        xrLabel71.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel71.SizeF = new System.Drawing.SizeF(31.61505F, 18F);
        xrLabel71.StyleName = "DataField";
        xrLabel71.Text = "xrLabel71";
        // 
        // xrLabel72
        // 
        xrLabel72.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DvzTLStr")});
        xrLabel72.LocationFloat = new DevExpress.Utils.PointFloat(739.715F, 0F);
        xrLabel72.Name = "xrLabel72";
        xrLabel72.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel72.SizeF = new System.Drawing.SizeF(61.28491F, 18F);
        xrLabel72.StyleName = "DataField";
        xrLabel72.Text = "xrLabel72";
        // 
        // xrLabel73
        // 
        xrLabel73.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DvzTutar")});
        xrLabel73.LocationFloat = new DevExpress.Utils.PointFloat(174F, 321F);
        xrLabel73.Name = "xrLabel73";
        xrLabel73.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel73.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel73.StyleName = "DataField";
        xrLabel73.Text = "xrLabel73";
        // 
        // xrLabel74
        // 
        xrLabel74.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "EkDosya")});
        xrLabel74.LocationFloat = new DevExpress.Utils.PointFloat(174F, 345F);
        xrLabel74.Name = "xrLabel74";
        xrLabel74.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel74.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel74.StyleName = "DataField";
        xrLabel74.Text = "xrLabel74";
        // 
        // xrLabel75
        // 
        xrLabel75.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FTDDovizCinsi")});
        xrLabel75.LocationFloat = new DevExpress.Utils.PointFloat(174F, 369F);
        xrLabel75.Name = "xrLabel75";
        xrLabel75.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel75.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel75.StyleName = "DataField";
        xrLabel75.Text = "xrLabel75";
        // 
        // xrLabel76
        // 
        xrLabel76.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FTDDovizKuru")});
        xrLabel76.LocationFloat = new DevExpress.Utils.PointFloat(174F, 393F);
        xrLabel76.Name = "xrLabel76";
        xrLabel76.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel76.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel76.StyleName = "DataField";
        xrLabel76.Text = "xrLabel76";
        // 
        // xrLabel77
        // 
        xrLabel77.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FTDDovizTL")});
        xrLabel77.LocationFloat = new DevExpress.Utils.PointFloat(174F, 417F);
        xrLabel77.Name = "xrLabel77";
        xrLabel77.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel77.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel77.StyleName = "DataField";
        xrLabel77.Text = "xrLabel77";
        // 
        // xrCheckBox1
        // 
        xrCheckBox1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("CheckState", null, "Gizle")});
        xrCheckBox1.LocationFloat = new DevExpress.Utils.PointFloat(174F, 441F);
        xrCheckBox1.Name = "xrCheckBox1";
        xrCheckBox1.SizeF = new System.Drawing.SizeF(470F, 23F);
        xrCheckBox1.StyleName = "DataField";
        // 
        // xrLabel78
        // 
        xrLabel78.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GMOnaylayan")});
        xrLabel78.LocationFloat = new DevExpress.Utils.PointFloat(174F, 470F);
        xrLabel78.Name = "xrLabel78";
        xrLabel78.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel78.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel78.StyleName = "DataField";
        xrLabel78.Text = "xrLabel78";
        // 
        // xrLabel79
        // 
        xrLabel79.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GMOnayTarih")});
        xrLabel79.LocationFloat = new DevExpress.Utils.PointFloat(174F, 494F);
        xrLabel79.Name = "xrLabel79";
        xrLabel79.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel79.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel79.StyleName = "DataField";
        xrLabel79.Text = "xrLabel79";
        // 
        // xrLabel80
        // 
        xrLabel80.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GMYMaliOnaylayan")});
        xrLabel80.LocationFloat = new DevExpress.Utils.PointFloat(174F, 518F);
        xrLabel80.Name = "xrLabel80";
        xrLabel80.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel80.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel80.StyleName = "DataField";
        xrLabel80.Text = "xrLabel80";
        // 
        // xrLabel81
        // 
        xrLabel81.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GMYMaliOnayTarih")});
        xrLabel81.LocationFloat = new DevExpress.Utils.PointFloat(174F, 542F);
        xrLabel81.Name = "xrLabel81";
        xrLabel81.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel81.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel81.StyleName = "DataField";
        xrLabel81.Text = "xrLabel81";
        // 
        // xrLabel82
        // 
        xrLabel82.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "HesapKodu")});
        xrLabel82.LocationFloat = new DevExpress.Utils.PointFloat(174F, 566F);
        xrLabel82.Name = "xrLabel82";
        xrLabel82.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel82.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel82.StyleName = "DataField";
        xrLabel82.Text = "xrLabel82";
        // 
        // xrLabel83
        // 
        xrLabel83.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ID")});
        xrLabel83.LocationFloat = new DevExpress.Utils.PointFloat(174F, 590F);
        xrLabel83.Name = "xrLabel83";
        xrLabel83.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel83.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel83.StyleName = "DataField";
        xrLabel83.Text = "xrLabel83";
        // 
        // xrLabel84
        // 
        xrLabel84.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "IstenenTarih")});
        xrLabel84.LocationFloat = new DevExpress.Utils.PointFloat(174F, 614F);
        xrLabel84.Name = "xrLabel84";
        xrLabel84.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel84.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel84.StyleName = "DataField";
        xrLabel84.Text = "xrLabel84";
        // 
        // xrLabel85
        // 
        xrLabel85.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Kademe1Onaylayan")});
        xrLabel85.LocationFloat = new DevExpress.Utils.PointFloat(174F, 638F);
        xrLabel85.Name = "xrLabel85";
        xrLabel85.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel85.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel85.StyleName = "DataField";
        xrLabel85.Text = "xrLabel85";
        // 
        // xrLabel86
        // 
        xrLabel86.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Kademe1OnayTarih")});
        xrLabel86.LocationFloat = new DevExpress.Utils.PointFloat(174F, 662F);
        xrLabel86.Name = "xrLabel86";
        xrLabel86.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel86.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel86.StyleName = "DataField";
        xrLabel86.Text = "xrLabel86";
        // 
        // xrLabel87
        // 
        xrLabel87.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Kademe2Onaylayan")});
        xrLabel87.LocationFloat = new DevExpress.Utils.PointFloat(168F, 686F);
        xrLabel87.Name = "xrLabel87";
        xrLabel87.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel87.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel87.StyleName = "DataField";
        xrLabel87.Text = "xrLabel87";
        // 
        // xrLabel88
        // 
        xrLabel88.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Kademe2OnayTarih")});
        xrLabel88.LocationFloat = new DevExpress.Utils.PointFloat(174F, 710F);
        xrLabel88.Name = "xrLabel88";
        xrLabel88.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel88.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel88.StyleName = "DataField";
        xrLabel88.Text = "xrLabel88";
        // 
        // xrLabel89
        // 
        xrLabel89.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "KapatilanMiktar")});
        xrLabel89.LocationFloat = new DevExpress.Utils.PointFloat(174F, 734F);
        xrLabel89.Name = "xrLabel89";
        xrLabel89.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel89.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel89.StyleName = "DataField";
        xrLabel89.Text = "xrLabel89";
        // 
        // xrLabel90
        // 
        xrLabel90.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "KapatilanMiktar_Onceki")});
        xrLabel90.LocationFloat = new DevExpress.Utils.PointFloat(174F, 758F);
        xrLabel90.Name = "xrLabel90";
        xrLabel90.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel90.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel90.StyleName = "DataField";
        xrLabel90.Text = "xrLabel90";
        // 
        // xrLabel91
        // 
        xrLabel91.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Katsayi")});
        xrLabel91.LocationFloat = new DevExpress.Utils.PointFloat(174F, 782F);
        xrLabel91.Name = "xrLabel91";
        xrLabel91.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel91.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel91.StyleName = "DataField";
        xrLabel91.Text = "xrLabel91";
        // 
        // xrLabel92
        // 
        xrLabel92.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Kaydeden")});
        xrLabel92.LocationFloat = new DevExpress.Utils.PointFloat(174F, 806F);
        xrLabel92.Name = "xrLabel92";
        xrLabel92.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel92.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel92.StyleName = "DataField";
        xrLabel92.Text = "xrLabel92";
        // 
        // xrLabel93
        // 
        xrLabel93.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "KayitTarih")});
        xrLabel93.LocationFloat = new DevExpress.Utils.PointFloat(174F, 830F);
        xrLabel93.Name = "xrLabel93";
        xrLabel93.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel93.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel93.StyleName = "DataField";
        xrLabel93.Text = "xrLabel93";
        // 
        // xrLabel94
        // 
        xrLabel94.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "KDVOran")});
        xrLabel94.LocationFloat = new DevExpress.Utils.PointFloat(174F, 854F);
        xrLabel94.Name = "xrLabel94";
        xrLabel94.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel94.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel94.StyleName = "DataField";
        xrLabel94.Text = "xrLabel94";
        // 
        // xrLabel95
        // 
        xrLabel95.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MalAdi")});
        xrLabel95.LocationFloat = new DevExpress.Utils.PointFloat(174F, 878F);
        xrLabel95.Name = "xrLabel95";
        xrLabel95.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel95.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel95.StyleName = "DataField";
        xrLabel95.Text = "xrLabel95";
        // 
        // xrLabel96
        // 
        xrLabel96.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MalKodu")});
        xrLabel96.LocationFloat = new DevExpress.Utils.PointFloat(174F, 902F);
        xrLabel96.Name = "xrLabel96";
        xrLabel96.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel96.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel96.StyleName = "DataField";
        xrLabel96.Text = "xrLabel96";
        // 
        // xrLabel97
        // 
        xrLabel97.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Miktar")});
        xrLabel97.LocationFloat = new DevExpress.Utils.PointFloat(174F, 926F);
        xrLabel97.Name = "xrLabel97";
        xrLabel97.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel97.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel97.StyleName = "DataField";
        xrLabel97.Text = "xrLabel97";
        // 
        // xrLabel98
        // 
        xrLabel98.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Operator")});
        xrLabel98.LocationFloat = new DevExpress.Utils.PointFloat(174F, 950F);
        xrLabel98.Name = "xrLabel98";
        xrLabel98.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel98.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel98.StyleName = "DataField";
        xrLabel98.Text = "xrLabel98";
        // 
        // xrLabel99
        // 
        xrLabel99.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Satinalmaci")});
        xrLabel99.LocationFloat = new DevExpress.Utils.PointFloat(174F, 974F);
        xrLabel99.Name = "xrLabel99";
        xrLabel99.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel99.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel99.StyleName = "DataField";
        xrLabel99.Text = "xrLabel99";
        // 
        // xrLabel100
        // 
        xrLabel100.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SipEvrakNo")});
        xrLabel100.LocationFloat = new DevExpress.Utils.PointFloat(174F, 998F);
        xrLabel100.Name = "xrLabel100";
        xrLabel100.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel100.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel100.StyleName = "DataField";
        xrLabel100.Text = "xrLabel100";
        // 
        // xrLabel101
        // 
        xrLabel101.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SipIslemTip")});
        xrLabel101.LocationFloat = new DevExpress.Utils.PointFloat(174F, 1022F);
        xrLabel101.Name = "xrLabel101";
        xrLabel101.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel101.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel101.StyleName = "DataField";
        xrLabel101.Text = "xrLabel101";
        // 
        // xrLabel102
        // 
        xrLabel102.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SipIslemTipStr")});
        xrLabel102.LocationFloat = new DevExpress.Utils.PointFloat(174F, 1046F);
        xrLabel102.Name = "xrLabel102";
        xrLabel102.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel102.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel102.StyleName = "DataField";
        xrLabel102.Text = "xrLabel102";
        // 
        // xrLabel103
        // 
        xrLabel103.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SipTalepNo")});
        xrLabel103.LocationFloat = new DevExpress.Utils.PointFloat(174F, 1070F);
        xrLabel103.Name = "xrLabel103";
        xrLabel103.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel103.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel103.StyleName = "DataField";
        xrLabel103.Text = "xrLabel103";
        // 
        // xrLabel104
        // 
        xrLabel104.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SipTerminTarih")});
        xrLabel104.LocationFloat = new DevExpress.Utils.PointFloat(174F, 1094F);
        xrLabel104.Name = "xrLabel104";
        xrLabel104.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel104.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel104.StyleName = "DataField";
        xrLabel104.Text = "xrLabel104";
        // 
        // xrLabel105
        // 
        xrLabel105.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SirketKodu")});
        xrLabel105.LocationFloat = new DevExpress.Utils.PointFloat(174F, 1118F);
        xrLabel105.Name = "xrLabel105";
        xrLabel105.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel105.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel105.StyleName = "DataField";
        xrLabel105.Text = "xrLabel105";
        // 
        // xrLabel106
        // 
        xrLabel106.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TalepNo")});
        xrLabel106.LocationFloat = new DevExpress.Utils.PointFloat(174F, 1142F);
        xrLabel106.Name = "xrLabel106";
        xrLabel106.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel106.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel106.StyleName = "DataField";
        xrLabel106.Text = "xrLabel106";
        // 
        // xrLabel107
        // 
        xrLabel107.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Tarih")});
        xrLabel107.LocationFloat = new DevExpress.Utils.PointFloat(174F, 1166F);
        xrLabel107.Name = "xrLabel107";
        xrLabel107.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel107.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel107.StyleName = "DataField";
        xrLabel107.Text = "xrLabel107";
        // 
        // xrLabel108
        // 
        xrLabel108.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TeklifAciklamasi")});
        xrLabel108.LocationFloat = new DevExpress.Utils.PointFloat(174F, 1190F);
        xrLabel108.Name = "xrLabel108";
        xrLabel108.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel108.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel108.StyleName = "DataField";
        xrLabel108.Text = "xrLabel108";
        // 
        // xrLabel109
        // 
        xrLabel109.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TeklifNo")});
        xrLabel109.LocationFloat = new DevExpress.Utils.PointFloat(174F, 1214F);
        xrLabel109.Name = "xrLabel109";
        xrLabel109.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel109.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel109.StyleName = "DataField";
        xrLabel109.Text = "xrLabel109";
        // 
        // xrLabel110
        // 
        xrLabel110.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TeklifVade")});
        xrLabel110.LocationFloat = new DevExpress.Utils.PointFloat(174F, 1238F);
        xrLabel110.Name = "xrLabel110";
        xrLabel110.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel110.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel110.StyleName = "DataField";
        xrLabel110.Text = "xrLabel110";
        // 
        // xrLabel111
        // 
        xrLabel111.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TesisAdi")});
        xrLabel111.LocationFloat = new DevExpress.Utils.PointFloat(174F, 1262F);
        xrLabel111.Name = "xrLabel111";
        xrLabel111.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel111.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel111.StyleName = "DataField";
        xrLabel111.Text = "xrLabel111";
        // 
        // xrLabel112
        // 
        xrLabel112.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TesisKodu")});
        xrLabel112.LocationFloat = new DevExpress.Utils.PointFloat(174F, 1286F);
        xrLabel112.Name = "xrLabel112";
        xrLabel112.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel112.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel112.StyleName = "DataField";
        xrLabel112.Text = "xrLabel112";
        // 
        // xrLabel113
        // 
        xrLabel113.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TeslimMiktar")});
        xrLabel113.LocationFloat = new DevExpress.Utils.PointFloat(174F, 1310F);
        xrLabel113.Name = "xrLabel113";
        xrLabel113.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel113.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel113.StyleName = "DataField";
        xrLabel113.Text = "xrLabel113";
        // 
        // xrLabel114
        // 
        xrLabel114.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Tip")});
        xrLabel114.LocationFloat = new DevExpress.Utils.PointFloat(174F, 1334F);
        xrLabel114.Name = "xrLabel114";
        xrLabel114.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel114.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel114.StyleName = "DataField";
        xrLabel114.Text = "xrLabel114";
        // 
        // xrLabel115
        // 
        xrLabel115.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TipStr")});
        xrLabel115.LocationFloat = new DevExpress.Utils.PointFloat(174F, 1358F);
        xrLabel115.Name = "xrLabel115";
        xrLabel115.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel115.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel115.StyleName = "DataField";
        xrLabel115.Text = "xrLabel115";
        // 
        // xrLabel116
        // 
        xrLabel116.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Tutar")});
        xrLabel116.LocationFloat = new DevExpress.Utils.PointFloat(174F, 1382F);
        xrLabel116.Name = "xrLabel116";
        xrLabel116.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel116.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel116.StyleName = "DataField";
        xrLabel116.Text = "xrLabel116";
        // 
        // xrLabel117
        // 
        xrLabel117.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Unvan")});
        xrLabel117.LocationFloat = new DevExpress.Utils.PointFloat(174F, 1406F);
        xrLabel117.Name = "xrLabel117";
        xrLabel117.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrLabel117.SizeF = new System.Drawing.SizeF(470F, 18F);
        xrLabel117.StyleName = "DataField";
        xrLabel117.Text = "xrLabel117";
        // 
        // reportHeaderBand1
        // 
        reportHeaderBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrLabel120,
            xrLabel121,
            xrLabel122,
            xrLabel123,
            xrLabel124,
            xrLabel125,
            xrLabel126,
            xrLabel127,
            xrLabel128,
            xrLabel129,
            xrLabel130,
            xrLabel131,
            xrLabel133,
            xrLabel134,
            xrLabel135,
            xrLabel136,
            xrLabel137,
            xrLabel118,
            xrLabel119,
            xrPictureBox2,
            xrPictureBox1,
            xrPageInfo3});
        reportHeaderBand1.HeightF = 308.4167F;
        reportHeaderBand1.Name = "reportHeaderBand1";
        // 
        // Title
        // 
        Title.BackColor = System.Drawing.Color.Transparent;
        Title.BorderColor = System.Drawing.Color.Black;
        Title.Borders = DevExpress.XtraPrinting.BorderSide.None;
        Title.BorderWidth = 1F;
        Title.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold);
        Title.ForeColor = System.Drawing.Color.Maroon;
        Title.Name = "Title";
        // 
        // FieldCaption
        // 
        FieldCaption.BackColor = System.Drawing.Color.Transparent;
        FieldCaption.BorderColor = System.Drawing.Color.Black;
        FieldCaption.Borders = DevExpress.XtraPrinting.BorderSide.None;
        FieldCaption.BorderWidth = 1F;
        FieldCaption.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        FieldCaption.ForeColor = System.Drawing.Color.Maroon;
        FieldCaption.Name = "FieldCaption";
        // 
        // PageInfo
        // 
        PageInfo.BackColor = System.Drawing.Color.Transparent;
        PageInfo.BorderColor = System.Drawing.Color.Black;
        PageInfo.Borders = DevExpress.XtraPrinting.BorderSide.None;
        PageInfo.BorderWidth = 1F;
        PageInfo.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
        PageInfo.ForeColor = System.Drawing.Color.Black;
        PageInfo.Name = "PageInfo";
        // 
        // DataField
        // 
        DataField.BackColor = System.Drawing.Color.Transparent;
        DataField.BorderColor = System.Drawing.Color.Black;
        DataField.Borders = DevExpress.XtraPrinting.BorderSide.None;
        DataField.BorderWidth = 1F;
        DataField.Font = new System.Drawing.Font("Times New Roman", 10F);
        DataField.ForeColor = System.Drawing.Color.Black;
        DataField.Name = "DataField";
        DataField.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        // 
        // xrPictureBox1
        // 
        xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
        xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrPictureBox1.Name = "xrPictureBox1";
        xrPictureBox1.SizeF = new System.Drawing.SizeF(202F, 114.6667F);
        xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
        // 
        // xrLabel118
        // 
        xrLabel118.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel118.LocationFloat = new DevExpress.Utils.PointFloat(202F, 31.33335F);
        xrLabel118.Multiline = true;
        xrLabel118.Name = "xrLabel118";
        xrLabel118.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel118.SizeF = new System.Drawing.SizeF(400.0416F, 83.33335F);
        xrLabel118.StylePriority.UseFont = false;
        xrLabel118.StylePriority.UseTextAlignment = false;
        xrLabel118.Text = "ESENTEPE MAHALLESİ ADLİYE SARAYI KARŞISI NO:8\r\nVEZİRKÖPRÜ / SAMSUN\r\nVEZİRKÖPRÜ VE" +
"RGİ DAİRESİ : 9250056973\r\nMERSİS NO : 0925005697300017\r\nFİRMAMIZ E-FATURA E-ARŞİ" +
"V SİSTEMİNE DAHİLDİR";
        xrLabel118.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrLabel119
        // 
        xrLabel119.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel119.LocationFloat = new DevExpress.Utils.PointFloat(202F, 0F);
        xrLabel119.Name = "xrLabel119";
        xrLabel119.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel119.SizeF = new System.Drawing.SizeF(400.0416F, 31.33334F);
        xrLabel119.StylePriority.UseFont = false;
        xrLabel119.StylePriority.UseTextAlignment = false;
        xrLabel119.Text = "VEZİRKÖPRÜ ORMAN ÜRÜNLERİ VE KAĞIT SANAYİ A.Ş. ";
        xrLabel119.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrPictureBox2
        // 
        xrPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox2.Image")));
        xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(602.0416F, 0F);
        xrPictureBox2.Name = "xrPictureBox2";
        xrPictureBox2.SizeF = new System.Drawing.SizeF(198.9584F, 114.6667F);
        xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
        // 
        // xrLabel120
        // 
        xrLabel120.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel120.LocationFloat = new DevExpress.Utils.PointFloat(0F, 192.4791F);
        xrLabel120.Name = "xrLabel120";
        xrLabel120.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel120.SizeF = new System.Drawing.SizeF(123.9583F, 17.79167F);
        xrLabel120.StylePriority.UseFont = false;
        xrLabel120.StylePriority.UseTextAlignment = false;
        xrLabel120.Text = "Tedarikçi Bilgileri";
        xrLabel120.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel121
        // 
        xrLabel121.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
        xrLabel121.LocationFloat = new DevExpress.Utils.PointFloat(227.0417F, 136.2708F);
        xrLabel121.Name = "xrLabel121";
        xrLabel121.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel121.SizeF = new System.Drawing.SizeF(320.875F, 31.33334F);
        xrLabel121.StylePriority.UseFont = false;
        xrLabel121.StylePriority.UseTextAlignment = false;
        xrLabel121.Text = "SATINALMA SİPARİŞ FORMU";
        xrLabel121.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel122
        // 
        xrLabel122.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel122.LocationFloat = new DevExpress.Utils.PointFloat(0F, 210.2708F);
        xrLabel122.Name = "xrLabel122";
        xrLabel122.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel122.SizeF = new System.Drawing.SizeF(100F, 17.79167F);
        xrLabel122.StylePriority.UseFont = false;
        xrLabel122.StylePriority.UseTextAlignment = false;
        xrLabel122.Text = "Ünvanı";
        xrLabel122.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel123
        // 
        xrLabel123.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel123.LocationFloat = new DevExpress.Utils.PointFloat(0F, 228.0625F);
        xrLabel123.Name = "xrLabel123";
        xrLabel123.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel123.SizeF = new System.Drawing.SizeF(100F, 17.79167F);
        xrLabel123.StylePriority.UseFont = false;
        xrLabel123.StylePriority.UseTextAlignment = false;
        xrLabel123.Text = "Adresi";
        xrLabel123.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel124
        // 
        xrLabel124.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel124.LocationFloat = new DevExpress.Utils.PointFloat(0F, 245.8542F);
        xrLabel124.Name = "xrLabel124";
        xrLabel124.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel124.SizeF = new System.Drawing.SizeF(100F, 17.79167F);
        xrLabel124.StylePriority.UseFont = false;
        xrLabel124.StylePriority.UseTextAlignment = false;
        xrLabel124.Text = "Tel";
        xrLabel124.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel125
        // 
        xrLabel125.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel125.LocationFloat = new DevExpress.Utils.PointFloat(0F, 263.6459F);
        xrLabel125.Name = "xrLabel125";
        xrLabel125.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel125.SizeF = new System.Drawing.SizeF(100F, 17.79167F);
        xrLabel125.StylePriority.UseFont = false;
        xrLabel125.StylePriority.UseTextAlignment = false;
        xrLabel125.Text = "Fax";
        xrLabel125.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel126
        // 
        xrLabel126.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel126.LocationFloat = new DevExpress.Utils.PointFloat(0F, 281.4376F);
        xrLabel126.Name = "xrLabel126";
        xrLabel126.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel126.SizeF = new System.Drawing.SizeF(100F, 17.79167F);
        xrLabel126.StylePriority.UseFont = false;
        xrLabel126.StylePriority.UseTextAlignment = false;
        xrLabel126.Text = "Email";
        xrLabel126.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel127
        // 
        xrLabel127.LocationFloat = new DevExpress.Utils.PointFloat(100F, 210.2708F);
        xrLabel127.Name = "xrLabel127";
        xrLabel127.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel127.SizeF = new System.Drawing.SizeF(284.375F, 17.79167F);
        xrLabel127.StylePriority.UseTextAlignment = false;
        xrLabel127.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel128
        // 
        xrLabel128.LocationFloat = new DevExpress.Utils.PointFloat(100F, 281.4376F);
        xrLabel128.Name = "xrLabel128";
        xrLabel128.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel128.SizeF = new System.Drawing.SizeF(284.375F, 17.79169F);
        xrLabel128.StylePriority.UseTextAlignment = false;
        xrLabel128.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel129
        // 
        xrLabel129.LocationFloat = new DevExpress.Utils.PointFloat(100F, 228.0625F);
        xrLabel129.Name = "xrLabel129";
        xrLabel129.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel129.SizeF = new System.Drawing.SizeF(284.375F, 17.79167F);
        xrLabel129.StylePriority.UseTextAlignment = false;
        xrLabel129.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel130
        // 
        xrLabel130.LocationFloat = new DevExpress.Utils.PointFloat(100F, 245.8542F);
        xrLabel130.Name = "xrLabel130";
        xrLabel130.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel130.SizeF = new System.Drawing.SizeF(284.375F, 17.79169F);
        xrLabel130.StylePriority.UseTextAlignment = false;
        xrLabel130.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel131
        // 
        xrLabel131.LocationFloat = new DevExpress.Utils.PointFloat(100F, 263.6459F);
        xrLabel131.Name = "xrLabel131";
        xrLabel131.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel131.SizeF = new System.Drawing.SizeF(284.375F, 17.79167F);
        xrLabel131.StylePriority.UseTextAlignment = false;
        xrLabel131.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel133
        // 
        xrLabel133.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        xrLabel133.LocationFloat = new DevExpress.Utils.PointFloat(544.7917F, 192.4791F);
        xrLabel133.Name = "xrLabel133";
        xrLabel133.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel133.SizeF = new System.Drawing.SizeF(113.125F, 17.79167F);
        xrLabel133.StylePriority.UseFont = false;
        xrLabel133.StylePriority.UseTextAlignment = false;
        xrLabel133.Text = "Tarih";
        xrLabel133.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel134
        // 
        xrLabel134.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel134.LocationFloat = new DevExpress.Utils.PointFloat(657.9167F, 210.2708F);
        xrLabel134.Name = "xrLabel134";
        xrLabel134.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel134.SizeF = new System.Drawing.SizeF(143.0833F, 17.79167F);
        xrLabel134.StylePriority.UseFont = false;
        xrLabel134.StylePriority.UseTextAlignment = false;
        xrLabel134.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel135
        // 
        xrLabel135.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
        xrLabel135.LocationFloat = new DevExpress.Utils.PointFloat(544.7917F, 210.2708F);
        xrLabel135.Name = "xrLabel135";
        xrLabel135.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel135.SizeF = new System.Drawing.SizeF(113.125F, 17.79167F);
        xrLabel135.StylePriority.UseFont = false;
        xrLabel135.StylePriority.UseTextAlignment = false;
        xrLabel135.Text = "Sipariş No";
        xrLabel135.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel136
        // 
        xrLabel136.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel136.LocationFloat = new DevExpress.Utils.PointFloat(657.9167F, 228.0625F);
        xrLabel136.Name = "xrLabel136";
        xrLabel136.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel136.SizeF = new System.Drawing.SizeF(143.0833F, 17.79167F);
        xrLabel136.StylePriority.UseFont = false;
        xrLabel136.StylePriority.UseTextAlignment = false;
        xrLabel136.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel137
        // 
        xrLabel137.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
        xrLabel137.LocationFloat = new DevExpress.Utils.PointFloat(544.7916F, 228.0625F);
        xrLabel137.Name = "xrLabel137";
        xrLabel137.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel137.SizeF = new System.Drawing.SizeF(113.125F, 17.79167F);
        xrLabel137.StylePriority.UseFont = false;
        xrLabel137.StylePriority.UseTextAlignment = false;
        xrLabel137.Text = "Sipariş Tarihi";
        xrLabel137.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrPageInfo3
        // 
        xrPageInfo3.Format = "{0:d MMMM yyyy HH:mm}";
        xrPageInfo3.LocationFloat = new DevExpress.Utils.PointFloat(657.9167F, 192.4791F);
        xrPageInfo3.Name = "xrPageInfo3";
        xrPageInfo3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        xrPageInfo3.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        xrPageInfo3.SizeF = new System.Drawing.SizeF(143.0833F, 17.79167F);
        xrPageInfo3.StylePriority.UseTextAlignment = false;
        xrPageInfo3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // PageHeader
        // 
        PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable1});
        PageHeader.HeightF = 36F;
        PageHeader.Name = "PageHeader";
        // 
        // xrTable1
        // 
        xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable1.Name = "xrTable1";
        xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow1});
        xrTable1.SizeF = new System.Drawing.SizeF(800.9999F, 36F);
        // 
        // xrTableRow1
        // 
        xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell3,
            xrTableCell4,
            xrTableCell2,
            xrTableCell5,
            xrTableCell6,
            xrTableCell7,
            xrTableCell8,
            xrTableCell9,
            xrTableCell10,
            xrTableCell11,
            xrTableCell12,
            xrTableCell13,
            xrTableCell1});
        xrTableRow1.Name = "xrTableRow1";
        xrTableRow1.Weight = 1.4400000000000002D;
        // 
        // xrTableCell1
        // 
        xrTableCell1.Name = "xrTableCell1";
        xrTableCell1.StylePriority.UseTextAlignment = false;
        xrTableCell1.Text = "Teslim Yeri";
        xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell1.Weight = 0.22953161593831695D;
        // 
        // xrTableCell2
        // 
        xrTableCell2.Name = "xrTableCell2";
        xrTableCell2.StylePriority.UseTextAlignment = false;
        xrTableCell2.Text = "Mal Adı";
        xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell2.Weight = 0.41745321190104867D;
        // 
        // xrTableCell3
        // 
        xrTableCell3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
        xrTableCell3.Name = "xrTableCell3";
        xrTableCell3.StylePriority.UseFont = false;
        xrTableCell3.StylePriority.UseTextAlignment = false;
        xrTableCell3.Text = "No";
        xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell3.Weight = 0.074906372748966582D;
        // 
        // xrTableCell4
        // 
        xrTableCell4.Name = "xrTableCell4";
        xrTableCell4.StylePriority.UseTextAlignment = false;
        xrTableCell4.Text = "Mal Kodu";
        xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell4.Weight = 0.29962549099586611D;
        // 
        // xrTableCell5
        // 
        xrTableCell5.Name = "xrTableCell5";
        xrTableCell5.StylePriority.UseTextAlignment = false;
        xrTableCell5.Text = "Açıklamalar";
        xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell5.Weight = 0.39404497041699643D;
        // 
        // xrTableCell6
        // 
        xrTableCell6.Name = "xrTableCell6";
        xrTableCell6.StylePriority.UseTextAlignment = false;
        xrTableCell6.Text = "İstenen Tarih";
        xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell6.Weight = 0.23797754965240822D;
        // 
        // xrTableCell7
        // 
        xrTableCell7.Name = "xrTableCell7";
        xrTableCell7.StylePriority.UseTextAlignment = false;
        xrTableCell7.Text = "Onaylanan Tarih";
        xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell7.Weight = 0.267003769092633D;
        // 
        // xrTableCell8
        // 
        xrTableCell8.Name = "xrTableCell8";
        xrTableCell8.StylePriority.UseTextAlignment = false;
        xrTableCell8.Text = "Miktar";
        xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell8.Weight = 0.20816481672726128D;
        // 
        // xrTableCell9
        // 
        xrTableCell9.Name = "xrTableCell9";
        xrTableCell9.StylePriority.UseTextAlignment = false;
        xrTableCell9.Text = "Birim";
        xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell9.Weight = 0.182284658370355D;
        // 
        // xrTableCell10
        // 
        xrTableCell10.Name = "xrTableCell10";
        xrTableCell10.StylePriority.UseTextAlignment = false;
        xrTableCell10.Text = "Fiyat";
        xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell10.Weight = 0.19430713133943675D;
        // 
        // xrTableCell11
        // 
        xrTableCell11.Name = "xrTableCell11";
        xrTableCell11.StylePriority.UseTextAlignment = false;
        xrTableCell11.Text = "Tutar";
        xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell11.Weight = 0.218651704268758D;
        // 
        // xrTableCell12
        // 
        xrTableCell12.Name = "xrTableCell12";
        xrTableCell12.StylePriority.UseTextAlignment = false;
        xrTableCell12.Text = "Döviz Cinsi";
        xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell12.Weight = 0.15764046112873667D;
        // 
        // xrTableCell13
        // 
        xrTableCell13.Name = "xrTableCell13";
        xrTableCell13.StylePriority.UseTextAlignment = false;
        xrTableCell13.Text = "Vade";
        xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell13.Weight = 0.11840824741921667D;
        // 
        // SatSipGMOnay
        // 
        Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail,
            TopMargin,
            BottomMargin,
            reportHeaderBand1,
            PageHeader});
        ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            objectDataSource1});
        DataSource = objectDataSource1;
        Margins = new System.Drawing.Printing.Margins(27, 21, 100, 100);
        StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            Title,
            FieldCaption,
            PageInfo,
            DataField});
        Version = "17.1";
        ((System.ComponentModel.ISupportInitialize)(objectDataSource1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
    }

    #endregion
}