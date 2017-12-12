using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for SatSipForm
/// </summary>
public class SatSipForm : DevExpress.XtraReports.UI.XtraReport
{
    public DevExpress.XtraReports.UI.DetailBand Detail;
    public DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    public DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    public DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    public PageHeaderBand PageHeader;
    public ReportHeaderBand ReportHeader;
    public XRLabel xrLabel1;
    public XRLabel xrLabel2;
    public XRLabel xrLabel3;
    public XRLabel xrLabel4;
    public XRLabel xrLabel5;
    public XRLabel xrLabel6;
    public XRLabel xrLabel7;
    public XRLabel lblUnvan;
    public XRLabel lblEmail;
    public XRLabel lblTel;
    public XRLabel lblAdrs;
    public XRLabel lblFax;
    public XRLabel xrLabel13;
    public XRLabel lblOrderNo;
    public XRLabel xrLabel15;
    public XRLabel lblOrderDate;
    public XRLabel xrLabel17;
    public XRLabel xrLabel18;
    public XRLabel xrLabel19;
    public XRPictureBox xrPictureBox3;
    public XRPictureBox xrPictureBox4;
    public XRPageInfo xrPageInfo1;
    public XRTable xrTable1;
    public XRTableRow xrTableRow1;
    public XRTableCell xrTableCell3;
    public XRTableCell xrTableCell4;
    public XRTableCell xrTableCell2;
    public XRTableCell xrTableCell5;
    public XRTableCell xrTableCell6;
    public XRTableCell xrTableCell7;
    public XRTableCell xrTableCell8;
    public XRTableCell xrTableCell9;
    public XRTableCell xrTableCell10;
    public XRTableCell xrTableCell11;
    public XRTableCell xrTableCell12;
    public XRTableCell xrTableCell13;
    public XRTableCell xrTableCell1;
    public XRTable xrTable2;
    public XRTableRow xrTableRow2;
    public XRTableCell xrTableCell17;
    public XRTableCell xrTableCell14;
    public XRTableCell xrTableCell15;
    public XRTableCell xrTableCell18;
    public XRTableCell xrTableCell19;
    public XRTableCell xrTableCell20;
    public XRTableCell xrTableCell21;
    public XRTableCell xrTableCell22;
    public XRTableCell xrTableCell23;
    public XRTableCell xrTableCell24;
    public XRTableCell xrTableCell25;
    public XRTableCell xrTableCell26;
    public XRTableCell xrTableCell16;
    public ReportFooterBand ReportFooter;
    public XRPictureBox PicUcuncuKisiImza;
    public XRPictureBox PicikinciKisiImza;
    public XRLabel lblBirinciKisiUnvn;
    public XRLabel lblikinciKisiUnvn;
    public XRLabel lblUcuncuKisiUnvn;
    public XRLabel lblBirinciKisiAd;
    public XRLabel lblikinciKisiAd;
    public XRLabel lblUcuncuKisiAd;
    public XRLabel xrLabel26;
    public XRLabel xrLabel23;
    public XRLabel lblDovizCins;
    public XRLabel xrLabel24;
    public XRLabel lblDovizCins2;
    public XRLabel xrLabel25;
    public XRLabel lblDovizCins3;
    public XRLabel xrLabel20;
    public XRLabel xrLabel21;
    public XRLabel xrLabel22;
    public XRPictureBox picBirinciKisiImza;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    System.ComponentModel.IContainer components;

    public SatSipForm()
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
        var resources = new System.ComponentModel.ComponentResourceManager(typeof(SatSipForm));
        var xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        var xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        var xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
        Detail = new DevExpress.XtraReports.UI.DetailBand();
        xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        lblUnvan = new DevExpress.XtraReports.UI.XRLabel();
        lblEmail = new DevExpress.XtraReports.UI.XRLabel();
        lblTel = new DevExpress.XtraReports.UI.XRLabel();
        lblAdrs = new DevExpress.XtraReports.UI.XRLabel();
        lblFax = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        lblOrderNo = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        lblOrderDate = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        xrPictureBox3 = new DevExpress.XtraReports.UI.XRPictureBox();
        xrPictureBox4 = new DevExpress.XtraReports.UI.XRPictureBox();
        xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        PicUcuncuKisiImza = new DevExpress.XtraReports.UI.XRPictureBox();
        PicikinciKisiImza = new DevExpress.XtraReports.UI.XRPictureBox();
        lblBirinciKisiUnvn = new DevExpress.XtraReports.UI.XRLabel();
        lblikinciKisiUnvn = new DevExpress.XtraReports.UI.XRLabel();
        lblUcuncuKisiUnvn = new DevExpress.XtraReports.UI.XRLabel();
        lblBirinciKisiAd = new DevExpress.XtraReports.UI.XRLabel();
        lblikinciKisiAd = new DevExpress.XtraReports.UI.XRLabel();
        lblUcuncuKisiAd = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
        lblDovizCins = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
        lblDovizCins2 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
        lblDovizCins3 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
        picBirinciKisiImza = new DevExpress.XtraReports.UI.XRPictureBox();
        objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(components);
        ((System.ComponentModel.ISupportInitialize)(xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(objectDataSource1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable2});
        Detail.HeightF = 25F;
        Detail.Name = "Detail";
        Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        xrTable2.Font = new System.Drawing.Font("Times New Roman", 8.75F);
        xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable2.Name = "xrTable2";
        xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow2});
        xrTable2.SizeF = new System.Drawing.SizeF(799.4583F, 25F);
        xrTable2.StylePriority.UseBorders = false;
        xrTable2.StylePriority.UseFont = false;
        // 
        // xrTableRow2
        // 
        xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell17,
            xrTableCell14,
            xrTableCell15,
            xrTableCell18,
            xrTableCell19,
            xrTableCell20,
            xrTableCell21,
            xrTableCell22,
            xrTableCell23,
            xrTableCell24,
            xrTableCell25,
            xrTableCell26,
            xrTableCell16});
        xrTableRow2.Name = "xrTableRow2";
        xrTableRow2.Weight = 1D;
        // 
        // xrTableCell17
        // 
        xrTableCell17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Siparis.SatirNo")});
        xrTableCell17.Name = "xrTableCell17";
        xrTableCell17.Weight = 0.19999996185302732D;
        // 
        // xrTableCell14
        // 
        xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Siparis.Malzeme")});
        xrTableCell14.Name = "xrTableCell14";
        xrTableCell14.Weight = 0.80000003814697274D;
        // 
        // xrTableCell15
        // 
        xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Siparis.MalzemeAdi")});
        xrTableCell15.Name = "xrTableCell15";
        xrTableCell15.Weight = 1.1146000671386718D;
        // 
        // xrTableCell18
        // 
        xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Siparis.Aciklama")});
        xrTableCell18.Name = "xrTableCell18";
        xrTableCell18.Weight = 1.0520997619628907D;
        // 
        // xrTableCell19
        // 
        xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Siparis.IstenenTarih")});
        xrTableCell19.Name = "xrTableCell19";
        xrTableCell19.Weight = 0.63540008544921878D;
        // 
        // xrTableCell20
        // 
        xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Siparis.OnaylananTarih")});
        xrTableCell20.Name = "xrTableCell20";
        xrTableCell20.Weight = 0.71289993286132836D;
        // 
        // xrTableCell21
        // 
        xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Siparis.Miktar")});
        xrTableCell21.Name = "xrTableCell21";
        xrTableCell21.Weight = 0.55580055236816417D;
        // 
        // xrTableCell22
        // 
        xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Siparis.Birim")});
        xrTableCell22.Name = "xrTableCell22";
        xrTableCell22.Weight = 0.48669971466064432D;
        // 
        // xrTableCell23
        // 
        xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Siparis.Fiyat")});
        xrTableCell23.Name = "xrTableCell23";
        xrTableCell23.Weight = 0.5187996864318849D;
        // 
        // xrTableCell24
        // 
        xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Siparis.Tutar")});
        xrTableCell24.Name = "xrTableCell24";
        xrTableCell24.Weight = 0.58380047798156742D;
        // 
        // xrTableCell25
        // 
        xrTableCell25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Siparis.DvzCinsi")});
        xrTableCell25.Name = "xrTableCell25";
        xrTableCell25.Weight = 0.42090048313140865D;
        // 
        // xrTableCell26
        // 
        xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Siparis.Vade")});
        xrTableCell26.Name = "xrTableCell26";
        xrTableCell26.Weight = 0.31614970445632928D;
        // 
        // xrTableCell16
        // 
        xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Siparis.TeslimYeri")});
        xrTableCell16.Name = "xrTableCell16";
        xrTableCell16.Weight = 0.59743266344070434D;
        // 
        // TopMargin
        // 
        TopMargin.HeightF = 34F;
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
        // PageHeader
        // 
        PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable1});
        PageHeader.HeightF = 37.77091F;
        PageHeader.Name = "PageHeader";
        // 
        // xrTable1
        // 
        xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        xrTable1.Font = new System.Drawing.Font("Times New Roman", 8.75F);
        xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable1.Name = "xrTable1";
        xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow1});
        xrTable1.SizeF = new System.Drawing.SizeF(800.9999F, 36F);
        xrTable1.StylePriority.UseBorders = false;
        xrTable1.StylePriority.UseFont = false;
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
        // xrTableCell3
        // 
        xrTableCell3.Font = new System.Drawing.Font("Times New Roman", 8.75F, System.Drawing.FontStyle.Bold);
        xrTableCell3.Name = "xrTableCell3";
        xrTableCell3.StylePriority.UseFont = false;
        xrTableCell3.StylePriority.UseTextAlignment = false;
        xrTableCell3.Text = "No";
        xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell3.Weight = 0.074906372748966582D;
        // 
        // xrTableCell4
        // 
        xrTableCell4.Font = new System.Drawing.Font("Times New Roman", 8.75F, System.Drawing.FontStyle.Bold);
        xrTableCell4.Name = "xrTableCell4";
        xrTableCell4.StylePriority.UseFont = false;
        xrTableCell4.StylePriority.UseTextAlignment = false;
        xrTableCell4.Text = "Mal Kodu";
        xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell4.Weight = 0.29962549099586611D;
        // 
        // xrTableCell2
        // 
        xrTableCell2.Font = new System.Drawing.Font("Times New Roman", 8.75F, System.Drawing.FontStyle.Bold);
        xrTableCell2.Name = "xrTableCell2";
        xrTableCell2.StylePriority.UseFont = false;
        xrTableCell2.StylePriority.UseTextAlignment = false;
        xrTableCell2.Text = "Mal Adı";
        xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell2.Weight = 0.41745321190104867D;
        // 
        // xrTableCell5
        // 
        xrTableCell5.Font = new System.Drawing.Font("Times New Roman", 8.75F, System.Drawing.FontStyle.Bold);
        xrTableCell5.Name = "xrTableCell5";
        xrTableCell5.StylePriority.UseFont = false;
        xrTableCell5.StylePriority.UseTextAlignment = false;
        xrTableCell5.Text = "Açıklamalar";
        xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell5.Weight = 0.39404497041699643D;
        // 
        // xrTableCell6
        // 
        xrTableCell6.Font = new System.Drawing.Font("Times New Roman", 8.75F, System.Drawing.FontStyle.Bold);
        xrTableCell6.Name = "xrTableCell6";
        xrTableCell6.StylePriority.UseFont = false;
        xrTableCell6.StylePriority.UseTextAlignment = false;
        xrTableCell6.Text = "İstenen Tarih";
        xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell6.Weight = 0.23797754965240822D;
        // 
        // xrTableCell7
        // 
        xrTableCell7.Font = new System.Drawing.Font("Times New Roman", 8.75F, System.Drawing.FontStyle.Bold);
        xrTableCell7.Name = "xrTableCell7";
        xrTableCell7.StylePriority.UseFont = false;
        xrTableCell7.StylePriority.UseTextAlignment = false;
        xrTableCell7.Text = "Onaylanan Tarih";
        xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell7.Weight = 0.267003769092633D;
        // 
        // xrTableCell8
        // 
        xrTableCell8.Font = new System.Drawing.Font("Times New Roman", 8.75F, System.Drawing.FontStyle.Bold);
        xrTableCell8.Name = "xrTableCell8";
        xrTableCell8.StylePriority.UseFont = false;
        xrTableCell8.StylePriority.UseTextAlignment = false;
        xrTableCell8.Text = "Miktar";
        xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell8.Weight = 0.20816481672726128D;
        // 
        // xrTableCell9
        // 
        xrTableCell9.Font = new System.Drawing.Font("Times New Roman", 8.75F, System.Drawing.FontStyle.Bold);
        xrTableCell9.Name = "xrTableCell9";
        xrTableCell9.StylePriority.UseFont = false;
        xrTableCell9.StylePriority.UseTextAlignment = false;
        xrTableCell9.Text = "Birim";
        xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell9.Weight = 0.182284658370355D;
        // 
        // xrTableCell10
        // 
        xrTableCell10.Font = new System.Drawing.Font("Times New Roman", 8.75F, System.Drawing.FontStyle.Bold);
        xrTableCell10.Name = "xrTableCell10";
        xrTableCell10.StylePriority.UseFont = false;
        xrTableCell10.StylePriority.UseTextAlignment = false;
        xrTableCell10.Text = "Fiyat";
        xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell10.Weight = 0.19430713133943675D;
        // 
        // xrTableCell11
        // 
        xrTableCell11.Font = new System.Drawing.Font("Times New Roman", 8.75F, System.Drawing.FontStyle.Bold);
        xrTableCell11.Name = "xrTableCell11";
        xrTableCell11.StylePriority.UseFont = false;
        xrTableCell11.StylePriority.UseTextAlignment = false;
        xrTableCell11.Text = "Tutar";
        xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell11.Weight = 0.218651704268758D;
        // 
        // xrTableCell12
        // 
        xrTableCell12.Font = new System.Drawing.Font("Times New Roman", 8.75F, System.Drawing.FontStyle.Bold);
        xrTableCell12.Name = "xrTableCell12";
        xrTableCell12.StylePriority.UseFont = false;
        xrTableCell12.StylePriority.UseTextAlignment = false;
        xrTableCell12.Text = "Döviz Cinsi";
        xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell12.Weight = 0.15764046112873667D;
        // 
        // xrTableCell13
        // 
        xrTableCell13.Font = new System.Drawing.Font("Times New Roman", 8.75F, System.Drawing.FontStyle.Bold);
        xrTableCell13.Name = "xrTableCell13";
        xrTableCell13.StylePriority.UseFont = false;
        xrTableCell13.StylePriority.UseTextAlignment = false;
        xrTableCell13.Text = "Vade";
        xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell13.Weight = 0.11840824741921667D;
        // 
        // xrTableCell1
        // 
        xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 8.75F, System.Drawing.FontStyle.Bold);
        xrTableCell1.Name = "xrTableCell1";
        xrTableCell1.StylePriority.UseFont = false;
        xrTableCell1.StylePriority.UseTextAlignment = false;
        xrTableCell1.Text = "Teslim Yeri";
        xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell1.Weight = 0.22953161593831695D;
        // 
        // ReportHeader
        // 
        ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrLabel1,
            xrLabel2,
            xrLabel3,
            xrLabel4,
            xrLabel5,
            xrLabel6,
            xrLabel7,
            lblUnvan,
            lblEmail,
            lblTel,
            lblAdrs,
            lblFax,
            xrLabel13,
            lblOrderNo,
            xrLabel15,
            lblOrderDate,
            xrLabel17,
            xrLabel18,
            xrLabel19,
            xrPictureBox3,
            xrPictureBox4,
            xrPageInfo1});
        ReportHeader.HeightF = 327.0833F;
        ReportHeader.Name = "ReportHeader";
        // 
        // xrLabel1
        // 
        xrLabel1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 192.4791F);
        xrLabel1.Name = "xrLabel1";
        xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel1.SizeF = new System.Drawing.SizeF(123.9583F, 17.79167F);
        xrLabel1.StylePriority.UseFont = false;
        xrLabel1.StylePriority.UseTextAlignment = false;
        xrLabel1.Text = "Tedarikçi Bilgileri";
        xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel2
        // 
        xrLabel2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
        xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(233.8334F, 138.3541F);
        xrLabel2.Name = "xrLabel2";
        xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel2.SizeF = new System.Drawing.SizeF(320.875F, 31.33334F);
        xrLabel2.StylePriority.UseFont = false;
        xrLabel2.StylePriority.UseTextAlignment = false;
        xrLabel2.Text = "SATINALMA SİPARİŞ FORMU";
        xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel3
        // 
        xrLabel3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        xrLabel3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 210.2708F);
        xrLabel3.Name = "xrLabel3";
        xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel3.SizeF = new System.Drawing.SizeF(100F, 17.79167F);
        xrLabel3.StylePriority.UseBorders = false;
        xrLabel3.StylePriority.UseFont = false;
        xrLabel3.StylePriority.UseTextAlignment = false;
        xrLabel3.Text = "Ünvanı";
        xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel4
        // 
        xrLabel4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        xrLabel4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 228.0625F);
        xrLabel4.Name = "xrLabel4";
        xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel4.SizeF = new System.Drawing.SizeF(100F, 38.62503F);
        xrLabel4.StylePriority.UseBorders = false;
        xrLabel4.StylePriority.UseFont = false;
        xrLabel4.StylePriority.UseTextAlignment = false;
        xrLabel4.Text = "Adresi";
        xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel5
        // 
        xrLabel5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        xrLabel5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 266.6875F);
        xrLabel5.Name = "xrLabel5";
        xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel5.SizeF = new System.Drawing.SizeF(100F, 17.79167F);
        xrLabel5.StylePriority.UseBorders = false;
        xrLabel5.StylePriority.UseFont = false;
        xrLabel5.StylePriority.UseTextAlignment = false;
        xrLabel5.Text = "Tel";
        xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel6
        // 
        xrLabel6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        xrLabel6.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 284.4792F);
        xrLabel6.Name = "xrLabel6";
        xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel6.SizeF = new System.Drawing.SizeF(100F, 17.79167F);
        xrLabel6.StylePriority.UseBorders = false;
        xrLabel6.StylePriority.UseFont = false;
        xrLabel6.StylePriority.UseTextAlignment = false;
        xrLabel6.Text = "Fax";
        xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel7
        // 
        xrLabel7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        xrLabel7.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 302.2709F);
        xrLabel7.Name = "xrLabel7";
        xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel7.SizeF = new System.Drawing.SizeF(100F, 17.79167F);
        xrLabel7.StylePriority.UseBorders = false;
        xrLabel7.StylePriority.UseFont = false;
        xrLabel7.StylePriority.UseTextAlignment = false;
        xrLabel7.Text = "Email";
        xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // lblUnvan
        // 
        lblUnvan.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
        lblUnvan.LocationFloat = new DevExpress.Utils.PointFloat(99.99998F, 210.2708F);
        lblUnvan.Name = "lblUnvan";
        lblUnvan.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        lblUnvan.SizeF = new System.Drawing.SizeF(284.375F, 17.79167F);
        lblUnvan.StylePriority.UseBorders = false;
        lblUnvan.StylePriority.UseTextAlignment = false;
        lblUnvan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblEmail
        // 
        lblEmail.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        lblEmail.LocationFloat = new DevExpress.Utils.PointFloat(99.99998F, 302.2709F);
        lblEmail.Name = "lblEmail";
        lblEmail.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        lblEmail.SizeF = new System.Drawing.SizeF(284.375F, 17.79169F);
        lblEmail.StylePriority.UseBorders = false;
        lblEmail.StylePriority.UseTextAlignment = false;
        lblEmail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblTel
        // 
        lblTel.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
        lblTel.LocationFloat = new DevExpress.Utils.PointFloat(99.99998F, 266.6875F);
        lblTel.Name = "lblTel";
        lblTel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        lblTel.SizeF = new System.Drawing.SizeF(284.375F, 17.79167F);
        lblTel.StylePriority.UseBorders = false;
        lblTel.StylePriority.UseTextAlignment = false;
        lblTel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblAdrs
        // 
        lblAdrs.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
        lblAdrs.LocationFloat = new DevExpress.Utils.PointFloat(100F, 228.0625F);
        lblAdrs.Name = "lblAdrs";
        lblAdrs.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        lblAdrs.SizeF = new System.Drawing.SizeF(284.375F, 38.62503F);
        lblAdrs.StylePriority.UseBorders = false;
        lblAdrs.StylePriority.UseTextAlignment = false;
        lblAdrs.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblFax
        // 
        lblFax.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
        lblFax.LocationFloat = new DevExpress.Utils.PointFloat(99.99998F, 284.4792F);
        lblFax.Name = "lblFax";
        lblFax.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        lblFax.SizeF = new System.Drawing.SizeF(284.375F, 17.79167F);
        lblFax.StylePriority.UseBorders = false;
        lblFax.StylePriority.UseTextAlignment = false;
        lblFax.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel13
        // 
        xrLabel13.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(543.2499F, 192.4791F);
        xrLabel13.Name = "xrLabel13";
        xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 4, 0, 0, 100F);
        xrLabel13.SizeF = new System.Drawing.SizeF(113.125F, 17.79167F);
        xrLabel13.StylePriority.UseFont = false;
        xrLabel13.StylePriority.UsePadding = false;
        xrLabel13.StylePriority.UseTextAlignment = false;
        xrLabel13.Text = "Tarih";
        xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblOrderNo
        // 
        lblOrderNo.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        lblOrderNo.LocationFloat = new DevExpress.Utils.PointFloat(656.375F, 210.2708F);
        lblOrderNo.Name = "lblOrderNo";
        lblOrderNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(4, 2, 0, 0, 100F);
        lblOrderNo.SizeF = new System.Drawing.SizeF(143.0833F, 17.79167F);
        lblOrderNo.StylePriority.UseFont = false;
        lblOrderNo.StylePriority.UsePadding = false;
        lblOrderNo.StylePriority.UseTextAlignment = false;
        lblOrderNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel15
        // 
        xrLabel15.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
        xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(543.2499F, 210.2708F);
        xrLabel15.Name = "xrLabel15";
        xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 4, 0, 0, 100F);
        xrLabel15.SizeF = new System.Drawing.SizeF(113.125F, 17.79167F);
        xrLabel15.StylePriority.UseFont = false;
        xrLabel15.StylePriority.UsePadding = false;
        xrLabel15.StylePriority.UseTextAlignment = false;
        xrLabel15.Text = "Sipariş No";
        xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblOrderDate
        // 
        lblOrderDate.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        lblOrderDate.LocationFloat = new DevExpress.Utils.PointFloat(656.375F, 228.0625F);
        lblOrderDate.Name = "lblOrderDate";
        lblOrderDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(4, 2, 0, 0, 100F);
        lblOrderDate.SizeF = new System.Drawing.SizeF(143.0833F, 17.79167F);
        lblOrderDate.StylePriority.UseFont = false;
        lblOrderDate.StylePriority.UsePadding = false;
        lblOrderDate.StylePriority.UseTextAlignment = false;
        lblOrderDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel17
        // 
        xrLabel17.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
        xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(543.2498F, 228.0625F);
        xrLabel17.Name = "xrLabel17";
        xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 4, 0, 0, 100F);
        xrLabel17.SizeF = new System.Drawing.SizeF(113.125F, 17.79167F);
        xrLabel17.StylePriority.UseFont = false;
        xrLabel17.StylePriority.UsePadding = false;
        xrLabel17.StylePriority.UseTextAlignment = false;
        xrLabel17.Text = "Sipariş Tarihi";
        xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel18
        // 
        xrLabel18.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(202F, 23F);
        xrLabel18.Multiline = true;
        xrLabel18.Name = "xrLabel18";
        xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel18.SizeF = new System.Drawing.SizeF(400.04F, 92F);
        xrLabel18.StylePriority.UseFont = false;
        xrLabel18.StylePriority.UseTextAlignment = false;
        xrLabel18.Text = "ESENTEPE MAHALLESİ ADLİYE SARAYI KARŞISI NO:8\r\nVEZİRKÖPRÜ / SAMSUN\r\nVEZİRKÖPRÜ VE" +
"RGİ DAİRESİ : 9250056973\r\nMERSİS NO : 0925005697300017\r\nFİRMAMIZ E-FATURA E-ARŞİ" +
"V SİSTEMİNE DAHİLDİR";
        xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel19
        // 
        xrLabel19.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(202F, 1.589457E-05F);
        xrLabel19.Name = "xrLabel19";
        xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel19.SizeF = new System.Drawing.SizeF(400.04F, 23F);
        xrLabel19.StylePriority.UseFont = false;
        xrLabel19.StylePriority.UseTextAlignment = false;
        xrLabel19.Text = "VEZİRKÖPRÜ ORMAN ÜRÜNLERİ VE KAĞIT SANAYİ A.Ş. ";
        xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrPictureBox3
        // 
        xrPictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox3.Image")));
        xrPictureBox3.LocationFloat = new DevExpress.Utils.PointFloat(602.0416F, 0F);
        xrPictureBox3.Name = "xrPictureBox3";
        xrPictureBox3.SizeF = new System.Drawing.SizeF(198.96F, 115F);
        xrPictureBox3.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
        // 
        // xrPictureBox4
        // 
        xrPictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox4.Image")));
        xrPictureBox4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrPictureBox4.Name = "xrPictureBox4";
        xrPictureBox4.SizeF = new System.Drawing.SizeF(202F, 115F);
        xrPictureBox4.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
        // 
        // xrPageInfo1
        // 
        xrPageInfo1.Format = "{0:d MMMM yyyy HH:mm}";
        xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(656.375F, 192.4791F);
        xrPageInfo1.Name = "xrPageInfo1";
        xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(4, 2, 0, 0, 100F);
        xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        xrPageInfo1.SizeF = new System.Drawing.SizeF(143.0833F, 17.79167F);
        xrPageInfo1.StylePriority.UsePadding = false;
        xrPageInfo1.StylePriority.UseTextAlignment = false;
        xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // ReportFooter
        // 
        ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            PicUcuncuKisiImza,
            PicikinciKisiImza,
            lblBirinciKisiUnvn,
            lblikinciKisiUnvn,
            lblUcuncuKisiUnvn,
            lblBirinciKisiAd,
            lblikinciKisiAd,
            lblUcuncuKisiAd,
            xrLabel26,
            xrLabel23,
            lblDovizCins,
            xrLabel24,
            lblDovizCins2,
            xrLabel25,
            lblDovizCins3,
            xrLabel20,
            xrLabel21,
            xrLabel22,
            picBirinciKisiImza});
        ReportFooter.HeightF = 340.625F;
        ReportFooter.Name = "ReportFooter";
        // 
        // PicUcuncuKisiImza
        // 
        PicUcuncuKisiImza.LocationFloat = new DevExpress.Utils.PointFloat(572.9999F, 263.8333F);
        PicUcuncuKisiImza.Name = "PicUcuncuKisiImza";
        PicUcuncuKisiImza.SizeF = new System.Drawing.SizeF(175F, 56.33334F);
        PicUcuncuKisiImza.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // PicikinciKisiImza
        // 
        PicikinciKisiImza.LocationFloat = new DevExpress.Utils.PointFloat(310.5F, 263.8333F);
        PicikinciKisiImza.Name = "PicikinciKisiImza";
        PicikinciKisiImza.SizeF = new System.Drawing.SizeF(175F, 56.33334F);
        PicikinciKisiImza.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // lblBirinciKisiUnvn
        // 
        lblBirinciKisiUnvn.Font = new System.Drawing.Font("Times New Roman", 10.75F, System.Drawing.FontStyle.Bold);
        lblBirinciKisiUnvn.LocationFloat = new DevExpress.Utils.PointFloat(48F, 220.8333F);
        lblBirinciKisiUnvn.Name = "lblBirinciKisiUnvn";
        lblBirinciKisiUnvn.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        lblBirinciKisiUnvn.SizeF = new System.Drawing.SizeF(154F, 23F);
        lblBirinciKisiUnvn.StylePriority.UseFont = false;
        lblBirinciKisiUnvn.StylePriority.UsePadding = false;
        lblBirinciKisiUnvn.StylePriority.UseTextAlignment = false;
        lblBirinciKisiUnvn.Text = "Satınalma Uzmanı";
        lblBirinciKisiUnvn.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // lblikinciKisiUnvn
        // 
        lblikinciKisiUnvn.Font = new System.Drawing.Font("Times New Roman", 10.75F, System.Drawing.FontStyle.Bold);
        lblikinciKisiUnvn.LocationFloat = new DevExpress.Utils.PointFloat(310.5F, 220.8333F);
        lblikinciKisiUnvn.Name = "lblikinciKisiUnvn";
        lblikinciKisiUnvn.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        lblikinciKisiUnvn.SizeF = new System.Drawing.SizeF(175F, 23F);
        lblikinciKisiUnvn.StylePriority.UseFont = false;
        lblikinciKisiUnvn.StylePriority.UsePadding = false;
        lblikinciKisiUnvn.StylePriority.UseTextAlignment = false;
        lblikinciKisiUnvn.Text = "Genel Müdür Yardımcısı";
        lblikinciKisiUnvn.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // lblUcuncuKisiUnvn
        // 
        lblUcuncuKisiUnvn.Font = new System.Drawing.Font("Times New Roman", 10.75F, System.Drawing.FontStyle.Bold);
        lblUcuncuKisiUnvn.LocationFloat = new DevExpress.Utils.PointFloat(573F, 220.8333F);
        lblUcuncuKisiUnvn.Name = "lblUcuncuKisiUnvn";
        lblUcuncuKisiUnvn.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        lblUcuncuKisiUnvn.SizeF = new System.Drawing.SizeF(175F, 23F);
        lblUcuncuKisiUnvn.StylePriority.UseFont = false;
        lblUcuncuKisiUnvn.StylePriority.UsePadding = false;
        lblUcuncuKisiUnvn.StylePriority.UseTextAlignment = false;
        lblUcuncuKisiUnvn.Text = "Genel Müdür";
        lblUcuncuKisiUnvn.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // lblBirinciKisiAd
        // 
        lblBirinciKisiAd.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        lblBirinciKisiAd.LocationFloat = new DevExpress.Utils.PointFloat(48F, 243.8332F);
        lblBirinciKisiAd.Name = "lblBirinciKisiAd";
        lblBirinciKisiAd.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        lblBirinciKisiAd.SizeF = new System.Drawing.SizeF(154F, 20F);
        lblBirinciKisiAd.StylePriority.UseFont = false;
        lblBirinciKisiAd.StylePriority.UseTextAlignment = false;
        lblBirinciKisiAd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // lblikinciKisiAd
        // 
        lblikinciKisiAd.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        lblikinciKisiAd.LocationFloat = new DevExpress.Utils.PointFloat(310.5F, 243.8332F);
        lblikinciKisiAd.Name = "lblikinciKisiAd";
        lblikinciKisiAd.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        lblikinciKisiAd.SizeF = new System.Drawing.SizeF(175F, 20F);
        lblikinciKisiAd.StylePriority.UseFont = false;
        lblikinciKisiAd.StylePriority.UseTextAlignment = false;
        lblikinciKisiAd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // lblUcuncuKisiAd
        // 
        lblUcuncuKisiAd.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        lblUcuncuKisiAd.LocationFloat = new DevExpress.Utils.PointFloat(573F, 243.8332F);
        lblUcuncuKisiAd.Name = "lblUcuncuKisiAd";
        lblUcuncuKisiAd.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        lblUcuncuKisiAd.SizeF = new System.Drawing.SizeF(175F, 20F);
        lblUcuncuKisiAd.StylePriority.UseFont = false;
        lblUcuncuKisiAd.StylePriority.UseTextAlignment = false;
        lblUcuncuKisiAd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel26
        // 
        xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(0F, 93.75F);
        xrLabel26.Multiline = true;
        xrLabel26.Name = "xrLabel26";
        xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel26.SizeF = new System.Drawing.SizeF(798.4265F, 100F);
        xrLabel26.Text = resources.GetString("xrLabel26.Text");
        // 
        // xrLabel23
        // 
        xrLabel23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Siparis.FTDTutar", "{0:n2}")});
        xrLabel23.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(666.0099F, 55.99995F);
        xrLabel23.Name = "xrLabel23";
        xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        xrLabel23.SizeF = new System.Drawing.SizeF(81.99994F, 23F);
        xrLabel23.StylePriority.UseFont = false;
        xrLabel23.StylePriority.UsePadding = false;
        xrLabel23.StylePriority.UseTextAlignment = false;
        xrSummary1.FormatString = "{0:n2}";
        xrLabel23.Summary = xrSummary1;
        xrLabel23.Text = "xrLabel1";
        xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // lblDovizCins
        // 
        lblDovizCins.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Siparis.DovizCinsi")});
        lblDovizCins.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        lblDovizCins.LocationFloat = new DevExpress.Utils.PointFloat(748.0099F, 55.99995F);
        lblDovizCins.Name = "lblDovizCins";
        lblDovizCins.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        lblDovizCins.SizeF = new System.Drawing.SizeF(50.41663F, 23F);
        lblDovizCins.StylePriority.UseFont = false;
        lblDovizCins.StylePriority.UseTextAlignment = false;
        lblDovizCins.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel24
        // 
        xrLabel24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Siparis.FTDKDV", "{0:n2}")});
        xrLabel24.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(666.0099F, 32.99999F);
        xrLabel24.Name = "xrLabel24";
        xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        xrLabel24.SizeF = new System.Drawing.SizeF(81.99994F, 23F);
        xrLabel24.StylePriority.UseFont = false;
        xrLabel24.StylePriority.UsePadding = false;
        xrLabel24.StylePriority.UseTextAlignment = false;
        xrSummary2.FormatString = "{0:n2}";
        xrLabel24.Summary = xrSummary2;
        xrLabel24.Text = "xrLabel9";
        xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // lblDovizCins2
        // 
        lblDovizCins2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Siparis.DovizCinsi")});
        lblDovizCins2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        lblDovizCins2.LocationFloat = new DevExpress.Utils.PointFloat(748.0099F, 32.99999F);
        lblDovizCins2.Name = "lblDovizCins2";
        lblDovizCins2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        lblDovizCins2.SizeF = new System.Drawing.SizeF(50.41663F, 23F);
        lblDovizCins2.StylePriority.UseFont = false;
        lblDovizCins2.StylePriority.UseTextAlignment = false;
        lblDovizCins2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel25
        // 
        xrLabel25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Siparis.FTDMalBedeli", "{0:n2}")});
        xrLabel25.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(666.0099F, 9.999974F);
        xrLabel25.Name = "xrLabel25";
        xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        xrLabel25.SizeF = new System.Drawing.SizeF(81.99994F, 23F);
        xrLabel25.StylePriority.UseFont = false;
        xrLabel25.StylePriority.UsePadding = false;
        xrLabel25.StylePriority.UseTextAlignment = false;
        xrSummary3.FormatString = "{0:n2}";
        xrLabel25.Summary = xrSummary3;
        xrLabel25.Text = "xrLabel10";
        xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // lblDovizCins3
        // 
        lblDovizCins3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Siparis.DovizCinsi")});
        lblDovizCins3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        lblDovizCins3.LocationFloat = new DevExpress.Utils.PointFloat(748.0099F, 9.999974F);
        lblDovizCins3.Name = "lblDovizCins3";
        lblDovizCins3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        lblDovizCins3.SizeF = new System.Drawing.SizeF(50.41663F, 23F);
        lblDovizCins3.StylePriority.UseFont = false;
        lblDovizCins3.StylePriority.UseTextAlignment = false;
        lblDovizCins3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel20
        // 
        xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(566.0099F, 55.99995F);
        xrLabel20.Name = "xrLabel20";
        xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel20.SizeF = new System.Drawing.SizeF(100F, 23F);
        xrLabel20.StylePriority.UseTextAlignment = false;
        xrLabel20.Text = "Toplam Tutar :";
        xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel21
        // 
        xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(566.0099F, 32.99999F);
        xrLabel21.Name = "xrLabel21";
        xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel21.SizeF = new System.Drawing.SizeF(100F, 23F);
        xrLabel21.StylePriority.UseTextAlignment = false;
        xrLabel21.Text = "KDV :";
        xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel22
        // 
        xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(566.0099F, 9.999974F);
        xrLabel22.Name = "xrLabel22";
        xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        xrLabel22.SizeF = new System.Drawing.SizeF(100F, 23F);
        xrLabel22.StylePriority.UseTextAlignment = false;
        xrLabel22.Text = "Ara Toplam :";
        xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // picBirinciKisiImza
        // 
        picBirinciKisiImza.LocationFloat = new DevExpress.Utils.PointFloat(48F, 263.8333F);
        picBirinciKisiImza.Name = "picBirinciKisiImza";
        picBirinciKisiImza.SizeF = new System.Drawing.SizeF(154F, 56.33334F);
        picBirinciKisiImza.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // objectDataSource1
        // 
        objectDataSource1.DataSource = typeof(Wms12m.Presentation.Reports.DsSatinalma);
        objectDataSource1.Name = "objectDataSource1";
        // 
        // SatSipForm
        // 
        Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail,
            TopMargin,
            BottomMargin,
            PageHeader,
            ReportHeader,
            ReportFooter});
        ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            objectDataSource1});
        DataSource = objectDataSource1;
        Margins = new System.Drawing.Printing.Margins(23, 26, 34, 100);
        Version = "17.1";
        ((System.ComponentModel.ISupportInitialize)(xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(objectDataSource1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
    }

    #endregion
}