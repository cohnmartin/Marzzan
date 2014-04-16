namespace ReportesMarzzan
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class ReporteCuerpoPedidoInterno
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.Text8 = new Telerik.Reporting.TextBox();
            this.Field6 = new Telerik.Reporting.TextBox();
            this.Text12 = new Telerik.Reporting.TextBox();
            this.Id1 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.lblTransporte = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.txtCosto = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.lblFlete = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.panel2 = new Telerik.Reporting.Panel();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.nameCaptionTextBox = new Telerik.Reporting.TextBox();
            this.subReport1 = new Telerik.Reporting.SubReport();
            this.reporteDetallePedido1 = new ReportesMarzzan.ReporteDetallePedido();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.subReport3 = new Telerik.Reporting.SubReport();
            this.reporteDetalleRemitos1 = new ReportesMarzzan.ReporteDetalleRemitos();
            this.txtCodigoExterno = new Telerik.Reporting.TextBox();
            this.lblCod = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.subReport2 = new Telerik.Reporting.SubReport();
            this.panel6 = new Telerik.Reporting.Panel();
            this.orderQtyCaptionTextBox = new Telerik.Reporting.TextBox();
            this.unitPriceCaptionTextBox = new Telerik.Reporting.TextBox();
            this.lineTotalCaptionTextBox = new Telerik.Reporting.TextBox();
            this.group1 = new Telerik.Reporting.Group();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.barcodeCodigoExterno = new Telerik.Reporting.Barcode();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            ((System.ComponentModel.ISupportInitialize)(this.reporteDetallePedido1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reporteDetalleRemitos1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageFooter
            // 
            this.pageFooter.Height = new Telerik.Reporting.Drawing.Unit(0.5608249306678772, Telerik.Reporting.Drawing.UnitType.Cm);
            this.pageFooter.Name = "pageFooter";
            this.pageFooter.Style.Visible = false;
            // 
            // reportHeader
            // 
            this.reportHeader.Height = new Telerik.Reporting.Drawing.Unit(0.59999990463256836, Telerik.Reporting.Drawing.UnitType.Cm);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1});
            this.reportHeader.Name = "reportHeader";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.010416666977107525, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.6856865882873535, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.21390247344970703, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Verdana";
            this.textBox1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(12, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Value = "IMPRESIONES PEDIDOS WEB";
            // 
            // Text8
            // 
            this.Text8.CanGrow = false;
            this.Text8.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.7999978065490723, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.0418527126312256, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Text8.Name = "Text8";
            this.Text8.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.6168265342712402, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Text8.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.Text8.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.Text8.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.Text8.Style.Color = System.Drawing.Color.Black;
            this.Text8.Style.Font.Bold = true;
            this.Text8.Style.Font.Italic = false;
            this.Text8.Style.Font.Name = "Verdana";
            this.Text8.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Point);
            this.Text8.Style.Font.Strikeout = false;
            this.Text8.Style.Font.Underline = false;
            this.Text8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.Text8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.Text8.Style.Visible = true;
            this.Text8.Value = "Fecha Impresión:";
            // 
            // Field6
            // 
            this.Field6.CanGrow = false;
            this.Field6.Format = "{0:d}";
            this.Field6.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(8.4170236587524414, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.0418527126312256, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Field6.Name = "Field6";
            this.Field6.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.9829750061035156, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Field6.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.Field6.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.Field6.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.Field6.Style.Color = System.Drawing.Color.Black;
            this.Field6.Style.Font.Bold = false;
            this.Field6.Style.Font.Italic = false;
            this.Field6.Style.Font.Name = "Tahoma";
            this.Field6.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.Field6.Style.Font.Strikeout = false;
            this.Field6.Style.Font.Underline = false;
            this.Field6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.Field6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.Field6.Style.Visible = true;
            this.Field6.Value = "=Now()";
            // 
            // Text12
            // 
            this.Text12.CanGrow = false;
            this.Text12.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(14.703641891479492, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.16760866343975067, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Text12.Name = "Text12";
            this.Text12.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.7948546409606934, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Text12.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.Text12.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.Text12.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.Text12.Style.Color = System.Drawing.Color.Black;
            this.Text12.Style.Font.Bold = true;
            this.Text12.Style.Font.Italic = false;
            this.Text12.Style.Font.Name = "Verdana";
            this.Text12.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Point);
            this.Text12.Style.Font.Strikeout = false;
            this.Text12.Style.Font.Underline = false;
            this.Text12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.Text12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.Text12.Style.Visible = true;
            this.Text12.Value = "=Fields.TipoPedido";
            // 
            // Id1
            // 
            this.Id1.CanGrow = false;
            this.Id1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(14.703641891479492, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.66760843992233276, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Id1.Name = "Id1";
            this.Id1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.7948558330535889, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.2288786172866821, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Id1.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.Id1.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.Id1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.Id1.Style.Color = System.Drawing.Color.OrangeRed;
            this.Id1.Style.Font.Bold = true;
            this.Id1.Style.Font.Italic = false;
            this.Id1.Style.Font.Name = "Verdana";
            this.Id1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(30, Telerik.Reporting.Drawing.UnitType.Point);
            this.Id1.Style.Font.Strikeout = false;
            this.Id1.Style.Font.Underline = false;
            this.Id1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.Id1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.Id1.Style.Visible = true;
            this.Id1.Value = "=Fields.Nro";
            // 
            // detail
            // 
            this.detail.Height = new Telerik.Reporting.Drawing.Unit(8.6999998092651367, Telerik.Reporting.Drawing.UnitType.Cm);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox6,
            this.lblTransporte,
            this.textBox2,
            this.txtCosto,
            this.textBox16,
            this.textBox13,
            this.textBox12,
            this.lblFlete,
            this.textBox18,
            this.textBox17,
            this.textBox8,
            this.textBox9,
            this.textBox29,
            this.textBox30,
            this.panel2,
            this.subReport1,
            this.textBox20,
            this.textBox4,
            this.textBox19,
            this.textBox21,
            this.textBox22,
            this.subReport3});
            this.detail.Name = "detail";
            this.detail.PageBreak = Telerik.Reporting.PageBreak.After;
            this.detail.ItemDataBinding += new System.EventHandler(this.detail_ItemDataBinding);
            this.detail.ItemDataBound += new System.EventHandler(this.detail_ItemDataBound);
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.5604448318481445, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.52873867750167847, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.323124885559082, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox6.Value = "=Fields.objDireccion.Calle";
            // 
            // lblTransporte
            // 
            this.lblTransporte.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.9791667461395264, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.34119352698326111, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblTransporte.Name = "lblTransporte";
            this.lblTransporte.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.0602034330368042, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblTransporte.Style.Font.Bold = true;
            this.lblTransporte.Style.Font.Name = "Verdana";
            this.lblTransporte.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.lblTransporte.Value = "Transporte:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.19963900744915009, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.10991001129150391, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.3464580774307251, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.21390247344970703, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Verdana";
            this.textBox2.Value = "Lugar Entrega:";
            // 
            // txtCosto
            // 
            this.txtCosto.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.0394492149353027, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.085713863372802734, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtCosto.Name = "txtCosto";
            this.txtCosto.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.2237491607666016, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.txtCosto.Style.Font.Name = "Tahoma";
            this.txtCosto.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.txtCosto.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtCosto.Value = "=Fields.CostoFlete";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.0394492149353027, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.34119352698326111, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.3354995250701904, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox16.Style.Font.Name = "Tahoma";
            this.textBox16.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox16.Value = "=Fields.Transportista";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.5604448318481445, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.12032667547464371, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.323124885559082, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox13.Value = "=Fields.objDireccion.Provincia";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.5604448318481445, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.32866001129150391, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.323124885559082, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox12.Value = "=Fields.objDireccion.Localidad";
            // 
            // lblFlete
            // 
            this.lblFlete.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.9791667461395264, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.085713863372802734, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblFlete.Name = "lblFlete";
            this.lblFlete.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.0602034330368042, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblFlete.Style.Font.Bold = true;
            this.lblFlete.Style.Font.Name = "Verdana";
            this.lblFlete.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.lblFlete.Value = "Costo Flete:";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.5604448318481445, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.0799198150634766, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.8895442485809326, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox18.Value = "=Fields.objClienteSolicitante.Clasif1";
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.19685037434101105, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.0695031881332398, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.2000000476837158, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox17.Style.Font.Bold = true;
            this.textBox17.Style.Font.Name = "Verdana";
            this.textBox17.Value = "Grupo:";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.19685037434101105, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.84369945526123047, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.2000000476837158, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox8.Style.Font.Bold = true;
            this.textBox8.Style.Font.Name = "Verdana";
            this.textBox8.Value = "Líder:";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.5604448318481445, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.84369945526123047, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.8895442485809326, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox9.Value = "=Fields.objClienteSolicitante.Nombre";
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(6.0236225128173828, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.9553667306900024, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.1438935995101929, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox29.Style.BorderColor.Default = System.Drawing.Color.Silver;
            this.textBox29.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox29.Style.Font.Name = "Tahoma";
            this.textBox29.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox29.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox29.Value = "=Fields.MontoTotal";
            // 
            // textBox30
            // 
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.2997174263000488, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.9553674459457398, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.91025924682617188, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox30.Style.BorderColor.Default = System.Drawing.Color.Silver;
            this.textBox30.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox30.Style.Font.Name = "Tahoma";
            this.textBox30.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox30.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox30.Value = "=Fields.TotalProductos";
            // 
            // panel2
            // 
            this.panel2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox7,
            this.textBox10,
            this.textBox11,
            this.nameCaptionTextBox});
            this.panel2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.8035413026809692, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.6284735202789307, Telerik.Reporting.Drawing.UnitType.Cm));
            this.panel2.Name = "panel2";
            this.panel2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.4574604034423828, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2904561460018158, Telerik.Reporting.Drawing.UnitType.Inch));
            this.panel2.Style.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel2.Style.Font.Name = "Tahoma";
            this.panel2.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.panel2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.5896623134613037, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.010456191375851631, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.91025900840759277, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox7.StyleName = "Caption";
            this.textBox7.Value = "Cantidad";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.5, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.010456164367496967, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.81356710195541382, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox10.StyleName = "Caption";
            this.textBox10.Value = "V. Unitario";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.3136458396911621, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.010456164367496967, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.1430082321166992, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox11.StyleName = "Caption";
            this.textBox11.Value = "Valor Total";
            // 
            // nameCaptionTextBox
            // 
            this.nameCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.026555541902780533, Telerik.Reporting.Drawing.UnitType.Cm));
            this.nameCaptionTextBox.Name = "nameCaptionTextBox";
            this.nameCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.5895440578460693, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929, Telerik.Reporting.Drawing.UnitType.Inch));
            this.nameCaptionTextBox.Style.Font.Bold = true;
            this.nameCaptionTextBox.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.nameCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.nameCaptionTextBox.StyleName = "Caption";
            this.nameCaptionTextBox.Value = "Nombre Producto";
            // 
            // subReport1
            // 
            this.subReport1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.8035413026809692, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(4.3664321899414062, Telerik.Reporting.Drawing.UnitType.Cm));
            this.subReport1.Name = "subReport1";
            this.subReport1.Parameters.Add(new Telerik.Reporting.Parameter("IdCabeceraPedido", "=Fields.IdCabeceraPedido"));
            this.subReport1.ReportSource = this.reporteDetallePedido1;
            this.subReport1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(16.401948928833008, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000002384185791, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.9791667461395264, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.57741421461105347, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.0602034330368042, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.Font.Name = "Verdana";
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.Value = "FP:";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.0394492149353027, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.58462363481521606, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.4072927236557007, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox4.Style.Font.Name = "Tahoma";
            this.textBox4.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "=Fields.objFormaDePago.Descripcion";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.20242755115032196, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(2.7922029495239258, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.0995734930038452, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox19.Style.Font.Bold = true;
            this.textBox19.Style.Font.Name = "Verdana";
            this.textBox19.Value = "Observación:";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.3020797967910767, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(2.806105375289917, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.8674187660217285, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.51181107759475708, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox21.Value = "=Fields.Retira";
            // 
            // textBox22
            // 
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.19685046374797821, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(2.2123258113861084, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.7716535329818726, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox22.Style.Font.Bold = true;
            this.textBox22.Style.Font.Name = "Verdana";
            this.textBox22.Value = "Remitos Pendientes:";
            // 
            // subReport3
            // 
            this.subReport3.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.8014936447143555, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(6.12750768661499, Telerik.Reporting.Drawing.UnitType.Cm));
            this.subReport3.Name = "subReport3";
            this.subReport3.Parameters.Add(new Telerik.Reporting.Parameter("CodCliente", "=Fields.objCliente.CodigoExterno"));
            this.subReport3.ReportSource = this.reporteDetalleRemitos1;
            this.subReport3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(16.401948928833008, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000002384185791, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // txtCodigoExterno
            // 
            this.txtCodigoExterno.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.3142287731170654, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.35620999336242676, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtCodigoExterno.Name = "txtCodigoExterno";
            this.txtCodigoExterno.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.3550631999969482, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtCodigoExterno.Style.Font.Name = "Tahoma";
            this.txtCodigoExterno.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.txtCodigoExterno.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtCodigoExterno.Value = "=Fields.objCliente.CodigoExterno";
            // 
            // lblCod
            // 
            this.lblCod.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.8897638320922852, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.34234675765037537, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblCod.Name = "lblCod";
            this.lblCod.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.4239473342895508, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.lblCod.Style.Font.Bold = true;
            this.lblCod.Style.Font.Name = "Verdana";
            this.lblCod.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.lblCod.Value = "Código Cliente:";
            // 
            // textBox5
            // 
            this.textBox5.Format = "{0:d}";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(8.4181404113769531, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.30009937286376953, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.3550629615783691, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox5.Style.Font.Name = "Tahoma";
            this.textBox5.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "=Fields.objCliente.Nombre";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.8000001907348633, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.30009937286376953, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.4239472150802612, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Name = "Verdana";
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "Consultor:";
            // 
            // textBox32
            // 
            this.textBox32.Format = "{0:d}";
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.3142287731170654, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.57856708765029907, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.3550629615783691, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox32.Style.Font.Name = "Tahoma";
            this.textBox32.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox32.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox32.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox32.Value = "=Fields.FechaPedido";
            // 
            // subReport2
            // 
            this.subReport2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.2964582443237305, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.9952082633972168, Telerik.Reporting.Drawing.UnitType.Cm));
            this.subReport2.Name = "subReport2";
            this.subReport2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(16.399900436401367, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000061988830566, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // panel6
            // 
            this.panel6.Name = "panel6";
            this.panel6.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.panel6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            // 
            // orderQtyCaptionTextBox
            // 
            this.orderQtyCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.5896623134613037, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.010456191375851631, Telerik.Reporting.Drawing.UnitType.Inch));
            this.orderQtyCaptionTextBox.Name = "orderQtyCaptionTextBox";
            this.orderQtyCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.91025900840759277, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929, Telerik.Reporting.Drawing.UnitType.Inch));
            this.orderQtyCaptionTextBox.Style.Font.Bold = true;
            this.orderQtyCaptionTextBox.StyleName = "Caption";
            this.orderQtyCaptionTextBox.Value = "Cantidad";
            // 
            // unitPriceCaptionTextBox
            // 
            this.unitPriceCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.5, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.010456164367496967, Telerik.Reporting.Drawing.UnitType.Inch));
            this.unitPriceCaptionTextBox.Name = "unitPriceCaptionTextBox";
            this.unitPriceCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.910259485244751, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929, Telerik.Reporting.Drawing.UnitType.Inch));
            this.unitPriceCaptionTextBox.Style.Font.Bold = true;
            this.unitPriceCaptionTextBox.StyleName = "Caption";
            this.unitPriceCaptionTextBox.Value = "V. Unitario";
            // 
            // lineTotalCaptionTextBox
            // 
            this.lineTotalCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.4103379249572754, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.010456164367496967, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lineTotalCaptionTextBox.Name = "lineTotalCaptionTextBox";
            this.lineTotalCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.910259485244751, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lineTotalCaptionTextBox.Style.Font.Bold = true;
            this.lineTotalCaptionTextBox.StyleName = "Caption";
            this.lineTotalCaptionTextBox.Value = "Valor Total";
            // 
            // group1
            // 
            this.group1.GroupFooter = this.groupFooterSection1;
            this.group1.GroupHeader = this.groupHeaderSection1;
            this.group1.Grouping.AddRange(new Telerik.Reporting.Data.Grouping[] {
            new Telerik.Reporting.Data.Grouping("=Fields.IdCabeceraPedido")});
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = new Telerik.Reporting.Drawing.Unit(0.37249016761779785, Telerik.Reporting.Drawing.UnitType.Cm);
            this.groupFooterSection1.Name = "groupFooterSection1";
            this.groupFooterSection1.Style.Visible = false;
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = new Telerik.Reporting.Drawing.Unit(3.1676089763641357, Telerik.Reporting.Drawing.UnitType.Cm);
            this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox32,
            this.Field6,
            this.Text12,
            this.Id1,
            this.txtCodigoExterno,
            this.lblCod,
            this.textBox5,
            this.textBox3,
            this.Text8,
            this.barcodeCodigoExterno,
            this.textBox15,
            this.textBox14,
            this.textBox23});
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            this.groupHeaderSection1.PrintOnEveryPage = true;
            this.groupHeaderSection1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.groupHeaderSection1.Style.Visible = false;
            // 
            // barcodeCodigoExterno
            // 
            this.barcodeCodigoExterno.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.30000004172325134, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.30009937286376953, Telerik.Reporting.Drawing.UnitType.Cm));
            this.barcodeCodigoExterno.Name = "barcodeCodigoExterno";
            this.barcodeCodigoExterno.ShowText = false;
            this.barcodeCodigoExterno.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.3398008346557617, Telerik.Reporting.Drawing.UnitType.Cm));
            this.barcodeCodigoExterno.Stretch = true;
            this.barcodeCodigoExterno.Value = "=Fields.objCliente.CodigoExterno";
            // 
            // textBox15
            // 
            this.textBox15.CanGrow = false;
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.8000001907348633, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.4695603847503662, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.6168265342712402, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox15.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.textBox15.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.textBox15.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox15.Style.Color = System.Drawing.Color.Black;
            this.textBox15.Style.Font.Bold = true;
            this.textBox15.Style.Font.Italic = false;
            this.textBox15.Style.Font.Name = "Verdana";
            this.textBox15.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox15.Style.Font.Strikeout = false;
            this.textBox15.Style.Font.Underline = false;
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.Style.Visible = true;
            this.textBox15.Value = "Fecha Pedido:";
            // 
            // textBox14
            // 
            this.textBox14.CanGrow = false;
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(14.703639984130859, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.0653457641601562, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.7948546409606934, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox14.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.textBox14.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.textBox14.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox14.Style.Color = System.Drawing.Color.Black;
            this.textBox14.Style.Font.Bold = true;
            this.textBox14.Style.Font.Italic = false;
            this.textBox14.Style.Font.Name = "Verdana";
            this.textBox14.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(13, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox14.Style.Font.Strikeout = false;
            this.textBox14.Style.Font.Underline = false;
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox14.Style.Visible = true;
            this.textBox14.Value = "=IIf(Fields.NroImpresion = 0, \'ORIGINAL\', \'IMPRESION NRO \' + Fields.NroImpresion)" +
                "";
            // 
            // textBox23
            // 
            this.textBox23.CanGrow = false;
            this.textBox23.Format = "{0:d}";
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(14.703641891479492, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.5655465126037598, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.7948529720306396, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox23.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.textBox23.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.textBox23.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox23.Style.Color = System.Drawing.Color.Black;
            this.textBox23.Style.Font.Bold = false;
            this.textBox23.Style.Font.Italic = false;
            this.textBox23.Style.Font.Name = "Tahoma";
            this.textBox23.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox23.Style.Font.Strikeout = false;
            this.textBox23.Style.Font.Underline = false;
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox23.Style.Visible = true;
            this.textBox23.Value = "= Parameters.UsuarioImpresion";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = new Telerik.Reporting.Drawing.Unit(0.559902012348175, Telerik.Reporting.Drawing.UnitType.Cm);
            this.reportFooterSection1.Name = "reportFooterSection1";
            this.reportFooterSection1.Style.Visible = false;
            // 
            // ReporteCuerpoPedidoInterno
            // 
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.pageFooter,
            this.reportHeader,
            this.detail,
            this.reportFooterSection1});
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(0.54000002145767212, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(0.54000002145767212, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(0.54000002145767212, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(0.54000002145767212, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.Name = "CostoFlete";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Float;
            reportParameter2.Name = "Transportista";
            reportParameter3.Name = "TotalProductos";
            reportParameter4.Name = "UsuarioImpresion";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule1.Style.Color = System.Drawing.Color.Black;
            styleRule1.Style.Font.Bold = true;
            styleRule1.Style.Font.Italic = false;
            styleRule1.Style.Font.Name = "Tahoma";
            styleRule1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(20, Telerik.Reporting.Drawing.UnitType.Point);
            styleRule1.Style.Font.Strikeout = false;
            styleRule1.Style.Font.Underline = false;
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule2.Style.Color = System.Drawing.Color.Black;
            styleRule2.Style.Font.Name = "Tahoma";
            styleRule2.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Point);
            styleRule2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule3.Style.Font.Name = "Tahoma";
            styleRule3.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Point);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule4.Style.Font.Name = "Tahoma";
            styleRule4.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Point);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4});
            this.Width = new Telerik.Reporting.Drawing.Unit(19.521743774414062, Telerik.Reporting.Drawing.UnitType.Cm);
            ((System.ComponentModel.ISupportInitialize)(this.reporteDetallePedido1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reporteDetalleRemitos1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private PageFooterSection pageFooter;
        private ReportHeaderSection reportHeader;
        private DetailSection detail;
        private Telerik.Reporting.TextBox Text8;
        private Telerik.Reporting.TextBox Id1;
        private Telerik.Reporting.TextBox Text12;
        private Telerik.Reporting.TextBox Field6;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox lblTransporte;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox txtCosto;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox lblFlete;
        private Telerik.Reporting.TextBox txtCodigoExterno;
        private Telerik.Reporting.TextBox lblCod;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.Panel panel2;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox nameCaptionTextBox;
        private SubReport subReport1;
        private SubReport subReport2;
        private Telerik.Reporting.Panel panel6;
        private Telerik.Reporting.TextBox orderQtyCaptionTextBox;
        private Telerik.Reporting.TextBox unitPriceCaptionTextBox;
        private Telerik.Reporting.TextBox lineTotalCaptionTextBox;
        private Group group1;
        private GroupFooterSection groupFooterSection1;
        private GroupHeaderSection groupHeaderSection1;
        private Barcode barcodeCodigoExterno;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox20;
        private ReporteDetallePedido reporteDetallePedido1;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox22;
        private SubReport subReport3;
        private ReporteDetalleRemitos reporteDetalleRemitos1;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox1;
        private ReportFooterSection reportFooterSection1;

    }
}