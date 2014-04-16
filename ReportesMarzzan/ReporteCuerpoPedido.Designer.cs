namespace ReportesMarzzan
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class ReporteCuerpoPedido
    {
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.Panel panel4;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox txtCosto;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.Panel panel1;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.Panel panel2;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox lblTransporte;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox lblFlete;
        private SubReport subReport1;
        private PageHeaderSection pageHeader;
        private Telerik.Reporting.Panel panel5;
        private Telerik.Reporting.TextBox lblDireccionEmpresa;
        private Telerik.Reporting.TextBox Field6;
        private Telerik.Reporting.TextBox Text12;
        private Telerik.Reporting.TextBox Id1;
        private Barcode barcode2;
        private Telerik.Reporting.Panel panel3;
        private Telerik.Reporting.TextBox Text8;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.Panel panel6;
        private Telerik.Reporting.TextBox orderQtyCaptionTextBox;
        private Telerik.Reporting.TextBox unitPriceCaptionTextBox;
        private Telerik.Reporting.TextBox lineTotalCaptionTextBox;
        private Telerik.Reporting.TextBox nameCaptionTextBox;

        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteCuerpoPedido));
            Telerik.Reporting.Drawing.FormattingRule formattingRule1 = new Telerik.Reporting.Drawing.FormattingRule();
            Telerik.Reporting.Drawing.FormattingRule formattingRule2 = new Telerik.Reporting.Drawing.FormattingRule();
            Telerik.Reporting.Drawing.FormattingRule formattingRule3 = new Telerik.Reporting.Drawing.FormattingRule();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            this.detail = new Telerik.Reporting.DetailSection();
            this.subReport1 = new Telerik.Reporting.SubReport();
            this.panel6 = new Telerik.Reporting.Panel();
            this.unitPriceCaptionTextBox = new Telerik.Reporting.TextBox();
            this.lineTotalCaptionTextBox = new Telerik.Reporting.TextBox();
            this.nameCaptionTextBox = new Telerik.Reporting.TextBox();
            this.orderQtyCaptionTextBox = new Telerik.Reporting.TextBox();
            this.subReport2 = new Telerik.Reporting.SubReport();
            this.reporteDetallePedido1 = new ReportesMarzzan.ReporteDetallePedido();
            this.lblRetira = new Telerik.Reporting.TextBox();
            this.lblComentario = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.txtRemitos = new Telerik.Reporting.TextBox();
            this.htmlTxtRemitos = new Telerik.Reporting.HtmlTextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.panel11 = new Telerik.Reporting.Panel();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox50 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox51 = new Telerik.Reporting.TextBox();
            this.textBox52 = new Telerik.Reporting.TextBox();
            this.panel4 = new Telerik.Reporting.Panel();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.panel1 = new Telerik.Reporting.Panel();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.panel7 = new Telerik.Reporting.Panel();
            this.textBox34 = new Telerik.Reporting.TextBox();
            this.textBox35 = new Telerik.Reporting.TextBox();
            this.textBox36 = new Telerik.Reporting.TextBox();
            this.textBox37 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.lblNeto = new Telerik.Reporting.TextBox();
            this.lblIva = new Telerik.Reporting.TextBox();
            this.lblRG30 = new Telerik.Reporting.TextBox();
            this.lblRG212 = new Telerik.Reporting.TextBox();
            this.lblTotalDetalle = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.txtCosto = new Telerik.Reporting.TextBox();
            this.panel2 = new Telerik.Reporting.Panel();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.lblTransporte = new Telerik.Reporting.TextBox();
            this.lblFlete = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.lblLeyendaTransporte = new Telerik.Reporting.TextBox();
            this.lblCod = new Telerik.Reporting.TextBox();
            this.txtCodigoExterno = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.panel5 = new Telerik.Reporting.Panel();
            this.lblDireccionEmpresa = new Telerik.Reporting.TextBox();
            this.barcode2 = new Telerik.Reporting.Barcode();
            this.panel3 = new Telerik.Reporting.Panel();
            this.Field6 = new Telerik.Reporting.TextBox();
            this.Text12 = new Telerik.Reporting.TextBox();
            this.Id1 = new Telerik.Reporting.TextBox();
            this.Text8 = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.lblPaginado = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.panel8 = new Telerik.Reporting.Panel();
            this.panel9 = new Telerik.Reporting.Panel();
            this.textBox38 = new Telerik.Reporting.TextBox();
            this.textBox39 = new Telerik.Reporting.TextBox();
            this.textBox40 = new Telerik.Reporting.TextBox();
            this.textBox41 = new Telerik.Reporting.TextBox();
            this.textBox42 = new Telerik.Reporting.TextBox();
            this.textBox43 = new Telerik.Reporting.TextBox();
            this.textBox44 = new Telerik.Reporting.TextBox();
            this.panel10 = new Telerik.Reporting.Panel();
            this.textBox45 = new Telerik.Reporting.TextBox();
            this.textBox46 = new Telerik.Reporting.TextBox();
            this.textBox47 = new Telerik.Reporting.TextBox();
            this.textBox48 = new Telerik.Reporting.TextBox();
            this.textBox49 = new Telerik.Reporting.TextBox();
            this.textBox53 = new Telerik.Reporting.TextBox();
            this.textBox54 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.reporteDetallePedido1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = new Telerik.Reporting.Drawing.Unit(6.9079017639160156D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.subReport1,
            this.panel6,
            this.subReport2,
            this.lblRetira,
            this.lblComentario,
            this.textBox3,
            this.textBox15,
            this.txtRemitos,
            this.htmlTxtRemitos,
            this.textBox19,
            this.panel11,
            this.textBox50,
            this.textBox29,
            this.textBox51,
            this.textBox52,
            this.textBox53,
            this.textBox54});
            this.detail.Name = "detail";
            this.detail.PageBreak = Telerik.Reporting.PageBreak.After;
            // 
            // subReport1
            // 
            this.subReport1.Name = "subReport1";
            // 
            // panel6
            // 
            this.panel6.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.unitPriceCaptionTextBox,
            this.lineTotalCaptionTextBox,
            this.nameCaptionTextBox,
            this.orderQtyCaptionTextBox});
            this.panel6.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.2000000476837158D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.16213399171829224D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.panel6.Name = "panel6";
            this.panel6.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.4574604034423828D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2904561460018158D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.panel6.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.panel6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            // 
            // unitPriceCaptionTextBox
            // 
            this.unitPriceCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.5D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.010456164367496967D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.unitPriceCaptionTextBox.Name = "unitPriceCaptionTextBox";
            this.unitPriceCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.910259485244751D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.unitPriceCaptionTextBox.Style.Font.Bold = true;
            this.unitPriceCaptionTextBox.StyleName = "Caption";
            resources.ApplyResources(this.unitPriceCaptionTextBox, "unitPriceCaptionTextBox");
            // 
            // lineTotalCaptionTextBox
            // 
            this.lineTotalCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.4103379249572754D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.010456164367496967D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lineTotalCaptionTextBox.Name = "lineTotalCaptionTextBox";
            this.lineTotalCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.910259485244751D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lineTotalCaptionTextBox.Style.Font.Bold = true;
            this.lineTotalCaptionTextBox.StyleName = "Caption";
            resources.ApplyResources(this.lineTotalCaptionTextBox, "lineTotalCaptionTextBox");
            // 
            // nameCaptionTextBox
            // 
            this.nameCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.905300498008728D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.02655845507979393D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.nameCaptionTextBox.Name = "nameCaptionTextBox";
            this.nameCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.7498025894165039D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.nameCaptionTextBox.Style.Font.Bold = true;
            this.nameCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.nameCaptionTextBox.StyleName = "Caption";
            resources.ApplyResources(this.nameCaptionTextBox, "nameCaptionTextBox");
            // 
            // orderQtyCaptionTextBox
            // 
            this.orderQtyCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.9498012483818457E-05D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.010456085205078125D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.orderQtyCaptionTextBox.Name = "orderQtyCaptionTextBox";
            this.orderQtyCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.75D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.orderQtyCaptionTextBox.Style.Font.Bold = true;
            this.orderQtyCaptionTextBox.StyleName = "Caption";
            resources.ApplyResources(this.orderQtyCaptionTextBox, "orderQtyCaptionTextBox");
            // 
            // subReport2
            // 
            this.subReport2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.2000000476837158D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.89999997615814209D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.subReport2.Name = "subReport2";
            this.subReport2.Parameters.Add(new Telerik.Reporting.Parameter("IdCabeceraPedido", "=Fields.IdCabeceraPedido"));
            this.subReport2.ReportSource = this.reporteDetallePedido1;
            this.subReport2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(16.399900436401367D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000061988830566D, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // lblRetira
            // 
            this.lblRetira.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.2225593328475952D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.2598425149917603D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblRetira.Name = "lblRetira";
            this.lblRetira.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.974855899810791D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.45853790640830994D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblRetira.Style.Font.Bold = true;
            this.lblRetira.Style.Font.Name = "Arial";
            this.lblRetira.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            resources.ApplyResources(this.lblRetira, "lblRetira");
            // 
            // lblComentario
            // 
            this.lblComentario.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.19108565151691437D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.2598425149917603D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblComentario.Name = "lblComentario";
            this.lblComentario.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.0313948392868042D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.21390247344970703D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblComentario.Style.Font.Bold = true;
            this.lblComentario.Style.Font.Name = "Verdana";
            resources.ApplyResources(this.lblComentario, "lblComentario");
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.14970755577087402D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.7716535329818726D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.4487178325653076D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.21390247344970703D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Name = "Verdana";
            resources.ApplyResources(this.textBox3, "textBox3");
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.5984251499176025D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.7716535329818726D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.1125494241714478D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.21390247344970703D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox15.Style.Font.Name = "Tahoma";
            this.textBox15.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox15, "textBox15");
            // 
            // txtRemitos
            // 
            this.txtRemitos.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.1910855770111084D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.66929119825363159D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtRemitos.Name = "txtRemitos";
            this.txtRemitos.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.0313948392868042D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.21390247344970703D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtRemitos.Style.Font.Bold = true;
            this.txtRemitos.Style.Font.Name = "Verdana";
            this.txtRemitos.Style.Visible = false;
            resources.ApplyResources(this.txtRemitos, "txtRemitos");
            // 
            // htmlTxtRemitos
            // 
            this.htmlTxtRemitos.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.1053004264831543D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.6999995708465576D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.htmlTxtRemitos.Name = "htmlTxtRemitos";
            this.htmlTxtRemitos.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(15.17613410949707D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.3000005483627319D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.htmlTxtRemitos.Style.Visible = false;
            resources.ApplyResources(this.htmlTxtRemitos, "htmlTxtRemitos");
            // 
            // textBox19
            // 
            formattingRule1.Filters.AddRange(new Telerik.Reporting.Data.Filter[] {
            new Telerik.Reporting.Data.Filter("=isNull(Fields.Tarjeta,0)", Telerik.Reporting.Data.FilterOperator.LessOrEqual, "=0")});
            formattingRule1.Style.Visible = false;
            this.textBox19.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule1});
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.14970748126506805D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(2.0472440719604492D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.172080397605896D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.21390247344970703D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox19.Style.Font.Bold = true;
            this.textBox19.Style.Font.Name = "Verdana";
            resources.ApplyResources(this.textBox19, "textBox19");
            // 
            // panel11
            // 
            formattingRule2.Filters.AddRange(new Telerik.Reporting.Data.Filter[] {
            new Telerik.Reporting.Data.Filter("=isNull(Fields.Tarjeta,0)", Telerik.Reporting.Data.FilterOperator.LessOrEqual, "=0")});
            formattingRule2.Style.Visible = false;
            this.panel11.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule2});
            this.panel11.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox20,
            this.textBox25,
            this.textBox21,
            this.textBox22});
            this.panel11.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.14970755577087402D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(2.261225700378418D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.panel11.Name = "panel11";
            this.panel11.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.2413978576660156D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19017474353313446D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.panel11.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.panel11.Style.Font.Name = "Tahoma";
            this.panel11.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.80681848526001D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.3833731412887573D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.17971861362457275D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.Font.Name = "Verdana";
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox20, "textBox20");
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.8341054916381836D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.4072927236557007D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19017501175403595D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox25.Style.Font.Bold = true;
            this.textBox25.Style.Font.Name = "Verdana";
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox25.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox25, "textBox25");
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.3207864761352539D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.164425253868103D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.17971861362457275D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox21.Style.Font.Bold = true;
            this.textBox21.Style.Font.Name = "Verdana";
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox21.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox21, "textBox21");
            // 
            // textBox22
            // 
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.2758820056915283D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.17971861362457275D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox22.Style.Font.Bold = true;
            this.textBox22.Style.Font.Name = "Verdana";
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox22, "textBox22");
            // 
            // textBox50
            // 
            formattingRule3.Filters.AddRange(new Telerik.Reporting.Data.Filter[] {
            new Telerik.Reporting.Data.Filter("=isNull(Fields.Tarjeta,0)", Telerik.Reporting.Data.FilterOperator.LessOrEqual, "=0")});
            formattingRule3.Style.Visible = false;
            this.textBox50.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule3});
            this.textBox50.Format = "{0:d}";
            this.textBox50.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.38025698065757751D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(6.2267570495605469D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox50.Name = "textBox50";
            this.textBox50.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.2758822441101074D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox50.Style.Font.Name = "Tahoma";
            this.textBox50.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox50.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox50, "textBox50");
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.9814376831054688D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(2.4514791965484619D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.4164276123046875D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox29.Style.Font.Name = "Tahoma";
            this.textBox29.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox29.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox29, "textBox29");
            // 
            // textBox51
            // 
            this.textBox51.Format = "{0:d}";
            this.textBox51.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.430957555770874D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(2.4514791965484619D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox51.Name = "textBox51";
            this.textBox51.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.3833731412887573D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox51.Style.Font.Name = "Tahoma";
            this.textBox51.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox51.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox51.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox51, "textBox51");
            // 
            // textBox52
            // 
            this.textBox52.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.8188979625701904D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(2.4514791965484619D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox52.Name = "textBox52";
            this.textBox52.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.1624609231948853D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox52.Style.Font.Name = "Tahoma";
            this.textBox52.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox52.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox52.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox52, "textBox52");
            // 
            // panel4
            // 
            this.panel4.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox30,
            this.textBox32,
            this.panel1,
            this.textBox5,
            this.textBox4,
            this.panel7,
            this.lblNeto,
            this.lblIva,
            this.lblRG30,
            this.lblRG212,
            this.lblTotalDetalle});
            this.panel4.KeepTogether = false;
            this.panel4.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(7.4000000953674316D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.panel4.Name = "panel4";
            this.panel4.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.40246057510376D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.1023620367050171D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.panel4.Style.BorderColor.Default = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(221)))), ((int)(((byte)(252)))));
            this.panel4.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            // 
            // textBox30
            // 
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.1440463066101074D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.39047208428382874D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.1624609231948853D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox30.Style.Font.Name = "Tahoma";
            this.textBox30.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox30.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox30, "textBox30");
            // 
            // textBox32
            // 
            this.textBox32.Format = "{0:d}";
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.760594367980957D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.39047208428382874D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.3833731412887573D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox32.Style.Font.Name = "Tahoma";
            this.textBox32.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox32.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox32.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox32, "textBox32");
            // 
            // panel1
            // 
            this.panel1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox23,
            this.textBox26,
            this.textBox1,
            this.textBox14});
            this.panel1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.47248053550720215D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.081338882446289062D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.panel1.Name = "panel1";
            this.panel1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.2413978576660156D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.29045644402503967D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.panel1.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.panel1.Style.Font.Name = "Tahoma";
            this.panel1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            // 
            // textBox23
            // 
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.80681848526001D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.3833731412887573D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox23.Style.Font.Bold = true;
            this.textBox23.Style.Font.Name = "Verdana";
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox23, "textBox23");
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.3207864761352539D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.164425253868103D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox26.Style.Font.Bold = true;
            this.textBox26.Style.Font.Name = "Verdana";
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox26, "textBox26");
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.0258775781840086D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.2758820056915283D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Verdana";
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox1, "textBox1");
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.8341054916381836D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.4072927236557007D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.29045641422271729D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox14.Style.Font.Bold = true;
            this.textBox14.Style.Font.Name = "Verdana";
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox14, "textBox14");
            // 
            // textBox5
            // 
            this.textBox5.Format = "{0:d}";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.2259781360626221D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.99179911613464355D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.2758822441101074D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox5.Style.Font.Name = "Tahoma";
            this.textBox5.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox5, "textBox5");
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.3065857887268066D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.39047208428382874D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.4072927236557007D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox4.Style.Font.Name = "Tahoma";
            this.textBox4.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox4, "textBox4");
            // 
            // panel7
            // 
            this.panel7.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox34,
            this.textBox35,
            this.textBox36,
            this.textBox37,
            this.textBox33});
            this.panel7.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.15748031437397003D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.66929119825363159D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.panel7.Name = "panel7";
            this.panel7.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.9693751335144043D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.15748055279254913D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.panel7.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.panel7.Style.Font.Name = "Tahoma";
            this.panel7.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            // 
            // textBox34
            // 
            this.textBox34.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(8.3427295684814453D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.164425253868103D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.15748052299022675D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox34.Style.Font.Bold = true;
            this.textBox34.Style.Font.Name = "Verdana";
            this.textBox34.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox34.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox34, "textBox34");
            // 
            // textBox35
            // 
            this.textBox35.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.4494538307189941D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.112510085105896D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.15748052299022675D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox35.Style.Font.Bold = true;
            this.textBox35.Style.Font.Name = "Verdana";
            this.textBox35.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox35.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox35, "textBox35");
            // 
            // textBox36
            // 
            this.textBox36.Name = "textBox36";
            this.textBox36.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.8952873945236206D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.15748052299022675D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox36.Style.Font.Bold = true;
            this.textBox36.Style.Font.Name = "Verdana";
            this.textBox36.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox36.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox36, "textBox36");
            // 
            // textBox37
            // 
            this.textBox37.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.5620822906494141D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.4072927236557007D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.15748052299022675D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox37.Style.Font.Bold = true;
            this.textBox37.Style.Font.Name = "Verdana";
            this.textBox37.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox37.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox37, "textBox37");
            // 
            // textBox33
            // 
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.8292016983032227D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.3833731412887573D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.15748052299022675D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox33.Style.Font.Bold = true;
            this.textBox33.Style.Font.Name = "Verdana";
            this.textBox33.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox33.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox33, "textBox33");
            // 
            // lblNeto
            // 
            this.lblNeto.Format = "{0:d}";
            this.lblNeto.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.1002004146575928D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.lblNeto.Name = "lblNeto";
            this.lblNeto.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.8952873945236206D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblNeto.Style.Font.Name = "Tahoma";
            this.lblNeto.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.lblNeto.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.lblNeto.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.lblNeto, "lblNeto");
            // 
            // lblIva
            // 
            this.lblIva.Format = "{0:d}";
            this.lblIva.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.0587408542633057D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.82685059309005737D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.3833731412887573D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblIva.Style.Font.Name = "Tahoma";
            this.lblIva.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.lblIva.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.lblIva.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.lblIva, "lblIva");
            // 
            // lblRG30
            // 
            this.lblRG30.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.443983793258667D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.82685059309005737D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblRG30.Name = "lblRG30";
            this.lblRG30.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.1624609231948853D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblRG30.Style.Font.Name = "Tahoma";
            this.lblRG30.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.lblRG30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.lblRG30.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.lblRG30, "lblRG30");
            // 
            // lblRG212
            // 
            this.lblRG212.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.6069340705871582D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.82685059309005737D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblRG212.Name = "lblRG212";
            this.lblRG212.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.1125494241714478D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblRG212.Style.Font.Name = "Tahoma";
            this.lblRG212.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.lblRG212.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.lblRG212.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.lblRG212, "lblRG212");
            // 
            // lblTotalDetalle
            // 
            this.lblTotalDetalle.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.7195625305175781D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.82685059309005737D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblTotalDetalle.Name = "lblTotalDetalle";
            this.lblTotalDetalle.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.4072927236557007D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblTotalDetalle.Style.Font.Name = "Tahoma";
            this.lblTotalDetalle.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.lblTotalDetalle.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.lblTotalDetalle.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.lblTotalDetalle, "lblTotalDetalle");
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.8434090614318848D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(2.423781156539917D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.4252153635025024D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            resources.ApplyResources(this.textBox10, "textBox10");
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.4962500333786011D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(2.4237806797027588D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.8895442485809326D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            resources.ApplyResources(this.textBox9, "textBox9");
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.11812499910593033D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(2.4237806797027588D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.2000000476837158D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox8.Style.Font.Bold = true;
            this.textBox8.Style.Font.Name = "Verdana";
            resources.ApplyResources(this.textBox8, "textBox8");
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.4962500333786011D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.9445573091506958D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.323124885559082D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            resources.ApplyResources(this.textBox6, "textBox6");
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.4962500333786011D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.7444783449172974D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.323124885559082D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            resources.ApplyResources(this.textBox12, "textBox12");
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.4962496757507324D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.5383076667785645D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.323124885559082D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            resources.ApplyResources(this.textBox13, "textBox13");
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.4493746757507324D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(2.4237806797027588D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.3939558267593384D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Style.Font.Name = "Verdana";
            resources.ApplyResources(this.textBox11, "textBox11");
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.9331250190734863D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.80003023147583D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.3354995250701904D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            resources.ApplyResources(this.textBox16, "textBox16");
            // 
            // txtCosto
            // 
            this.txtCosto.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.5125002861022949D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.5383073091506958D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtCosto.Name = "txtCosto";
            this.txtCosto.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.88536518812179565D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            resources.ApplyResources(this.txtCosto, "txtCosto");
            // 
            // panel2
            // 
            this.panel2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox7});
            this.panel2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.11033749580383301D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.2238701581954956D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.panel2.Name = "panel2";
            this.panel2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.1582870483398438D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.30045604705810547D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.panel2.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.010416507720947266D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.6986207962036133D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.28999999165534973D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Name = "Verdana";
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox7, "textBox7");
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.11033765226602554D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.5244053602218628D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.3464580774307251D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.21390247344970703D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Verdana";
            resources.ApplyResources(this.textBox2, "textBox2");
            // 
            // lblTransporte
            // 
            this.lblTransporte.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.9183845520019531D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.80003023147583D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblTransporte.Name = "lblTransporte";
            this.lblTransporte.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.971902072429657D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblTransporte.Style.Font.Bold = true;
            this.lblTransporte.Style.Font.Name = "Verdana";
            resources.ApplyResources(this.lblTransporte, "lblTransporte");
            // 
            // lblFlete
            // 
            this.lblFlete.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.9183845520019531D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.5383076667785645D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblFlete.Name = "lblFlete";
            this.lblFlete.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.5927027463912964D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblFlete.Style.Font.Bold = true;
            this.lblFlete.Style.Font.Name = "Verdana";
            resources.ApplyResources(this.lblFlete, "lblFlete");
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.11033757776021957D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(2.69940447807312D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.2000000476837158D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox17.Style.Font.Bold = true;
            this.textBox17.Style.Font.Name = "Verdana";
            resources.ApplyResources(this.textBox17, "textBox17");
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.4962501525878906D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(2.7133071422576904D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.8895442485809326D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            resources.ApplyResources(this.textBox18, "textBox18");
            // 
            // lblLeyendaTransporte
            // 
            this.lblLeyendaTransporte.Culture = new System.Globalization.CultureInfo("es-AR");
            this.lblLeyendaTransporte.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.95145320892334D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(5.1559886932373047D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.lblLeyendaTransporte.Name = "lblLeyendaTransporte";
            this.lblLeyendaTransporte.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(8.5085468292236328D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.87599015235900879D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.lblLeyendaTransporte.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.lblLeyendaTransporte.Style.LineWidth = new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Point);
            this.lblLeyendaTransporte.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            resources.ApplyResources(this.lblLeyendaTransporte, "lblLeyendaTransporte");
            // 
            // lblCod
            // 
            this.lblCod.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.4493746757507324D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(2.69940447807312D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblCod.Name = "lblCod";
            this.lblCod.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.3939558267593384D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblCod.Style.Font.Bold = true;
            this.lblCod.Style.Font.Name = "Verdana";
            resources.ApplyResources(this.lblCod, "lblCod");
            // 
            // txtCodigoExterno
            // 
            this.txtCodigoExterno.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.8434090614318848D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(2.69940447807312D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtCodigoExterno.Name = "txtCodigoExterno";
            this.txtCodigoExterno.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.4252153635025024D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            resources.ApplyResources(this.txtCodigoExterno, "txtCodigoExterno");
            // 
            // pageHeader
            // 
            this.pageHeader.Height = new Telerik.Reporting.Drawing.Unit(10.199999809265137D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.panel5,
            this.textBox13,
            this.textBox9,
            this.textBox8,
            this.textBox6,
            this.textBox12,
            this.textBox10,
            this.textBox11,
            this.textBox16,
            this.txtCosto,
            this.panel2,
            this.textBox2,
            this.lblTransporte,
            this.lblFlete,
            this.textBox17,
            this.textBox18,
            this.lblLeyendaTransporte,
            this.lblCod,
            this.txtCodigoExterno,
            this.panel4});
            this.pageHeader.Name = "pageHeader";
            // 
            // panel5
            // 
            this.panel5.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.lblDireccionEmpresa,
            this.barcode2,
            this.panel3,
            this.pictureBox1});
            this.panel5.Name = "panel5";
            this.panel5.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(18.799900054931641D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.panel5.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.panel5.Style.BorderColor.Default = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(221)))), ((int)(((byte)(252)))));
            this.panel5.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            // 
            // lblDireccionEmpresa
            // 
            this.lblDireccionEmpresa.CanGrow = false;
            this.lblDireccionEmpresa.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.8000001907348633D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.29585421085357666D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.lblDireccionEmpresa.Name = "lblDireccionEmpresa";
            this.lblDireccionEmpresa.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.2712278366088867D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.523809552192688D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.lblDireccionEmpresa.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.lblDireccionEmpresa.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.lblDireccionEmpresa.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.lblDireccionEmpresa.Style.Color = System.Drawing.Color.Black;
            this.lblDireccionEmpresa.Style.Font.Bold = false;
            this.lblDireccionEmpresa.Style.Font.Italic = false;
            this.lblDireccionEmpresa.Style.Font.Name = "Verdana";
            this.lblDireccionEmpresa.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.lblDireccionEmpresa.Style.Font.Strikeout = false;
            this.lblDireccionEmpresa.Style.Font.Underline = false;
            this.lblDireccionEmpresa.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.lblDireccionEmpresa.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.lblDireccionEmpresa.Style.Visible = true;
            resources.ApplyResources(this.lblDireccionEmpresa, "lblDireccionEmpresa");
            // 
            // barcode2
            // 
            this.barcode2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(11.199999809265137D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.30000001192092896D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.barcode2.Name = "barcode2";
            this.barcode2.ShowText = false;
            this.barcode2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.2599997520446777D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.7093372344970703D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.barcode2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.barcode2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            resources.ApplyResources(this.barcode2, "barcode2");
            // 
            // panel3
            // 
            this.panel3.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.Field6,
            this.Text12,
            this.Id1,
            this.Text8});
            this.panel3.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.94488191604614258D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.panel3.Name = "panel3";
            this.panel3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(18.799900054931641D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.panel3.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            // 
            // Field6
            // 
            this.Field6.CanGrow = false;
            this.Field6.Format = "{0:d}";
            this.Field6.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(15.40000057220459D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Field6.Name = "Field6";
            this.Field6.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.059999942779541D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Field6.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.Field6.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.Field6.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.Field6.Style.Color = System.Drawing.Color.OrangeRed;
            this.Field6.Style.Font.Bold = true;
            this.Field6.Style.Font.Italic = false;
            this.Field6.Style.Font.Name = "Verdana";
            this.Field6.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10D, Telerik.Reporting.Drawing.UnitType.Point);
            this.Field6.Style.Font.Strikeout = false;
            this.Field6.Style.Font.Underline = false;
            this.Field6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.Field6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.Field6.Style.Visible = true;
            resources.ApplyResources(this.Field6, "Field6");
            // 
            // Text12
            // 
            this.Text12.CanGrow = false;
            this.Text12.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.1109218597412109D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Text12.Name = "Text12";
            this.Text12.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.6963250637054443D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Text12.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.Text12.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.Text12.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.Text12.Style.Color = System.Drawing.Color.Black;
            this.Text12.Style.Font.Bold = true;
            this.Text12.Style.Font.Italic = false;
            this.Text12.Style.Font.Name = "Verdana";
            this.Text12.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10D, Telerik.Reporting.Drawing.UnitType.Point);
            this.Text12.Style.Font.Strikeout = false;
            this.Text12.Style.Font.Underline = false;
            this.Text12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.Text12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.Text12.Style.Visible = true;
            resources.ApplyResources(this.Text12, "Text12");
            // 
            // Id1
            // 
            this.Id1.CanGrow = false;
            this.Id1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(11.872657775878906D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Id1.Name = "Id1";
            this.Id1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.1237547397613525D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Id1.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.Id1.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.Id1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.Id1.Style.Color = System.Drawing.Color.OrangeRed;
            this.Id1.Style.Font.Bold = true;
            this.Id1.Style.Font.Italic = false;
            this.Id1.Style.Font.Name = "Verdana";
            this.Id1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
            this.Id1.Style.Font.Strikeout = false;
            this.Id1.Style.Font.Underline = false;
            this.Id1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.Id1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.Id1.Style.Visible = true;
            resources.ApplyResources(this.Id1, "Id1");
            // 
            // Text8
            // 
            this.Text8.CanGrow = false;
            this.Text8.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(13.996616363525391D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Text8.Name = "Text8";
            this.Text8.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.3708218336105347D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Text8.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.Text8.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.Text8.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.Text8.Style.Color = System.Drawing.Color.Black;
            this.Text8.Style.Font.Bold = true;
            this.Text8.Style.Font.Italic = false;
            this.Text8.Style.Font.Name = "Verdana";
            this.Text8.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10D, Telerik.Reporting.Drawing.UnitType.Point);
            this.Text8.Style.Font.Strikeout = false;
            this.Text8.Style.Font.Underline = false;
            this.Text8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.Text8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.Text8.Style.Visible = true;
            resources.ApplyResources(this.Text8, "Text8");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.099999971687793732D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.29585421085357666D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.pictureBox1.MimeType = "image/gif";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.6000003814697266D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.004145622253418D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.Stretch;
            this.pictureBox1.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = new Telerik.Reporting.Drawing.Unit(0.50800031423568726D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.lblPaginado});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // lblPaginado
            // 
            this.lblPaginado.Name = "lblPaginado";
            this.lblPaginado.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.4015355110168457D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblPaginado.Style.Font.Name = "Tahoma";
            this.lblPaginado.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.lblPaginado.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.lblPaginado.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.lblPaginado, "lblPaginado");
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.7093749046325684D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.37187385559082031D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.4072927236557007D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox24.Style.Font.Name = "Tahoma";
            this.textBox24.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox24, "textBox24");
            // 
            // textBox27
            // 
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.5967464447021484D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.37187385559082031D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.1125494241714478D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox27.Style.Font.Name = "Tahoma";
            this.textBox27.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox27.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox27, "textBox27");
            // 
            // textBox28
            // 
            this.textBox28.Format = "{0:d}";
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.0472440719604492D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.37187385559082031D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.3833731412887573D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox28.Style.Font.Name = "Tahoma";
            this.textBox28.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox28.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox28.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox28, "textBox28");
            // 
            // textBox31
            // 
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.43420672416687D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.37187385559082031D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.1624609231948853D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox31.Style.Font.Name = "Tahoma";
            this.textBox31.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox31.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox31, "textBox31");
            // 
            // panel8
            // 
            this.panel8.Name = "panel8";
            this.panel8.Style.BorderColor.Default = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(221)))), ((int)(((byte)(252)))));
            this.panel8.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            // 
            // panel9
            // 
            this.panel9.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox38,
            this.textBox39,
            this.textBox40,
            this.textBox41,
            this.textBox42});
            this.panel9.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.14729253947734833D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.081338882446289062D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.panel9.Name = "panel9";
            this.panel9.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.9693751335144043D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.29045644402503967D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.panel9.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.panel9.Style.Font.Name = "Tahoma";
            this.panel9.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            // 
            // textBox38
            // 
            this.textBox38.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.8292016983032227D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.3833731412887573D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox38.Style.Font.Bold = true;
            this.textBox38.Style.Font.Name = "Verdana";
            this.textBox38.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox38.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox38, "textBox38");
            // 
            // textBox39
            // 
            this.textBox39.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(8.3427295684814453D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.164425253868103D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox39.Style.Font.Bold = true;
            this.textBox39.Style.Font.Name = "Verdana";
            this.textBox39.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox39.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox39, "textBox39");
            // 
            // textBox40
            // 
            this.textBox40.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.4494538307189941D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.112510085105896D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.290456086397171D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox40.Style.Font.Bold = true;
            this.textBox40.Style.Font.Name = "Verdana";
            this.textBox40.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox40.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox40, "textBox40");
            // 
            // textBox41
            // 
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.8952873945236206D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox41.Style.Font.Bold = true;
            this.textBox41.Style.Font.Name = "Verdana";
            this.textBox41.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox41.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox41, "textBox41");
            // 
            // textBox42
            // 
            this.textBox42.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.5620822906494141D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox42.Name = "textBox42";
            this.textBox42.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.4072927236557007D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.29045641422271729D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox42.Style.Font.Bold = true;
            this.textBox42.Style.Font.Name = "Verdana";
            this.textBox42.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox42.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox42, "textBox42");
            // 
            // textBox43
            // 
            this.textBox43.Format = "{0:d}";
            this.textBox43.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.39999997615814209D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.94455957412719727D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.8850995302200317D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox43.Style.Font.Name = "Tahoma";
            this.textBox43.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox43.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox43, "textBox43");
            // 
            // textBox44
            // 
            this.textBox44.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.12075392156839371D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.59055107831954956D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox44.Name = "textBox44";
            this.textBox44.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.729640007019043D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox44.Style.Font.Bold = true;
            this.textBox44.Style.Font.Name = "Verdana";
            resources.ApplyResources(this.textBox44, "textBox44");
            // 
            // panel10
            // 
            this.panel10.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox45,
            this.textBox46,
            this.textBox47,
            this.textBox48,
            this.textBox49});
            this.panel10.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.15748031437397003D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.82677143812179565D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.panel10.Name = "panel10";
            this.panel10.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.9693751335144043D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.15748055279254913D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.panel10.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.panel10.Style.Font.Name = "Tahoma";
            this.panel10.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            // 
            // textBox45
            // 
            this.textBox45.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(8.3427295684814453D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox45.Name = "textBox45";
            this.textBox45.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.164425253868103D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.15748052299022675D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox45.Style.Font.Bold = true;
            this.textBox45.Style.Font.Name = "Verdana";
            this.textBox45.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox45.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox45, "textBox45");
            // 
            // textBox46
            // 
            this.textBox46.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.4494538307189941D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox46.Name = "textBox46";
            this.textBox46.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.112510085105896D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.15748052299022675D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox46.Style.Font.Bold = true;
            this.textBox46.Style.Font.Name = "Verdana";
            this.textBox46.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox46.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox46, "textBox46");
            // 
            // textBox47
            // 
            this.textBox47.Name = "textBox47";
            this.textBox47.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.8952873945236206D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.15748052299022675D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox47.Style.Font.Bold = true;
            this.textBox47.Style.Font.Name = "Verdana";
            this.textBox47.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox47.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox47, "textBox47");
            // 
            // textBox48
            // 
            this.textBox48.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.5620822906494141D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox48.Name = "textBox48";
            this.textBox48.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.4072927236557007D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.15748052299022675D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox48.Style.Font.Bold = true;
            this.textBox48.Style.Font.Name = "Verdana";
            this.textBox48.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox48.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox48, "textBox48");
            // 
            // textBox49
            // 
            this.textBox49.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.8292016983032227D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox49.Name = "textBox49";
            this.textBox49.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.3833731412887573D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.15748052299022675D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox49.Style.Font.Bold = true;
            this.textBox49.Style.Font.Name = "Verdana";
            this.textBox49.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox49.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox49, "textBox49");
            // 
            // textBox53
            // 
            this.textBox53.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.7195625305175781D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.7716537714004517D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox53.Name = "textBox53";
            this.textBox53.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.4072927236557007D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.21390247344970703D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox53.Style.Font.Name = "Tahoma";
            this.textBox53.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox53.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox53.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            resources.ApplyResources(this.textBox53, "textBox53");
            // 
            // textBox54
            // 
            this.textBox54.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.8193085193634033D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.7716537714004517D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox54.Name = "textBox54";
            this.textBox54.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.9001750946044922D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.21390247344970703D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox54.Style.Font.Bold = true;
            this.textBox54.Style.Font.Name = "Verdana";
            resources.ApplyResources(this.textBox54, "textBox54");
            // 
            // ReporteCuerpoPedido
            // 
            resources.ApplyResources(this, "$this");
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail,
            this.pageFooterSection1});
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(0.54000002145767212D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(0.54000002145767212D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(0.54000002145767212D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(0.54000002145767212D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.Name = "CostoFlete";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Float;
            reportParameter1.UI.Text = resources.GetString("resource.Text");
            reportParameter2.Name = "TotalProductos";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter2.UI.Text = resources.GetString("resource.Text1");
            reportParameter3.Name = "Transportista";
            reportParameter3.UI.Text = resources.GetString("resource.Text2");
            reportParameter4.Name = "Remitos";
            reportParameter4.UI.Text = resources.GetString("resource.Text3");
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = new Telerik.Reporting.Drawing.Unit(20.395566940307617D, Telerik.Reporting.Drawing.UnitType.Cm);
            ((System.ComponentModel.ISupportInitialize)(this.reporteDetallePedido1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private SubReport subReport2;
        private ReporteDetallePedido reporteDetallePedido1;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox lblLeyendaTransporte;
        private Telerik.Reporting.TextBox lblRetira;
        private Telerik.Reporting.TextBox lblCod;
        private Telerik.Reporting.TextBox txtCodigoExterno;
        private Telerik.Reporting.TextBox lblComentario;
        private PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox lblPaginado;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox txtRemitos;
        private HtmlTextBox htmlTxtRemitos;
        private Telerik.Reporting.Panel panel7;
        private Telerik.Reporting.TextBox textBox34;
        private Telerik.Reporting.TextBox textBox35;
        private Telerik.Reporting.TextBox textBox36;
        private Telerik.Reporting.TextBox textBox37;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox lblNeto;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.Panel panel8;
        private Telerik.Reporting.Panel panel9;
        private Telerik.Reporting.TextBox textBox38;
        private Telerik.Reporting.TextBox textBox39;
        private Telerik.Reporting.TextBox textBox40;
        private Telerik.Reporting.TextBox textBox41;
        private Telerik.Reporting.TextBox textBox42;
        private Telerik.Reporting.TextBox textBox43;
        private Telerik.Reporting.TextBox textBox44;
        private Telerik.Reporting.Panel panel10;
        private Telerik.Reporting.TextBox textBox45;
        private Telerik.Reporting.TextBox textBox46;
        private Telerik.Reporting.TextBox textBox47;
        private Telerik.Reporting.TextBox textBox48;
        private Telerik.Reporting.TextBox textBox49;
        private Telerik.Reporting.TextBox lblIva;
        private Telerik.Reporting.TextBox lblRG30;
        private Telerik.Reporting.TextBox lblRG212;
        private Telerik.Reporting.TextBox lblTotalDetalle;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.Panel panel11;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox50;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox51;
        private Telerik.Reporting.TextBox textBox52;
        private Telerik.Reporting.TextBox textBox53;
        private Telerik.Reporting.TextBox textBox54;

 
    }
}