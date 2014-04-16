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
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.lblPaginado = new Telerik.Reporting.TextBox();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.panel2 = new Telerik.Reporting.Panel();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.nameCaptionTextBox = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.subReport1 = new Telerik.Reporting.SubReport();
            this.reporteDetallePedido1 = new ReportesMarzzan.ReporteDetallePedido();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.subReport3 = new Telerik.Reporting.SubReport();
            this.reporteDetalleRemitos1 = new ReportesMarzzan.ReporteDetalleRemitos();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.lblTransporte = new Telerik.Reporting.TextBox();
            this.txtCosto = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.lblFlete = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.subReport2 = new Telerik.Reporting.SubReport();
            this.panel6 = new Telerik.Reporting.Panel();
            this.orderQtyCaptionTextBox = new Telerik.Reporting.TextBox();
            this.unitPriceCaptionTextBox = new Telerik.Reporting.TextBox();
            this.lineTotalCaptionTextBox = new Telerik.Reporting.TextBox();
            this.group1 = new Telerik.Reporting.Group();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.panel1 = new Telerik.Reporting.Panel();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.Text8 = new Telerik.Reporting.TextBox();
            this.Field6 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.txtCodigoExterno = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.lblCod = new Telerik.Reporting.TextBox();
            this.barcodeCodigoExterno = new Telerik.Reporting.Barcode();
            this.Text12 = new Telerik.Reporting.TextBox();
            this.Id1 = new Telerik.Reporting.TextBox();
            this.panel3 = new Telerik.Reporting.Panel();
            this.panel4 = new Telerik.Reporting.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.reporteDetallePedido1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reporteDetalleRemitos1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = new Telerik.Reporting.Drawing.Unit(0.50009965896606445, Telerik.Reporting.Drawing.UnitType.Cm);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1});
            this.pageHeader.Name = "pageHeader";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = false;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(20.509899139404297, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox1.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.textBox1.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.textBox1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox1.Style.Color = System.Drawing.Color.Black;
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Italic = false;
            this.textBox1.Style.Font.Name = "Verdana";
            this.textBox1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(12, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox1.Style.Font.Strikeout = false;
            this.textBox1.Style.Font.Underline = false;
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox1.Style.Visible = true;
            this.textBox1.Value = "PEDIDOS WEB";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = new Telerik.Reporting.Drawing.Unit(0.535514771938324, Telerik.Reporting.Drawing.UnitType.Cm);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.lblPaginado});
            this.pageFooter.Name = "pageFooter";
            this.pageFooter.Style.Visible = false;
            // 
            // lblPaginado
            // 
            this.lblPaginado.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.7895596027374268, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblPaginado.Name = "lblPaginado";
            this.lblPaginado.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.5328114032745361, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblPaginado.Style.BorderColor.Default = System.Drawing.Color.Silver;
            this.lblPaginado.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.lblPaginado.Style.Font.Name = "Tahoma";
            this.lblPaginado.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.lblPaginado.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.lblPaginado.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.lblPaginado.Value = "= \"Página: \" + PageNumber + \" De \" + PageCount";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = new Telerik.Reporting.Drawing.Unit(0.13229165971279144, Telerik.Reporting.Drawing.UnitType.Cm);
            this.reportHeader.Name = "reportHeader";
            // 
            // detail
            // 
            this.detail.Height = new Telerik.Reporting.Drawing.Unit(4.9999990463256836, Telerik.Reporting.Drawing.UnitType.Cm);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox29,
            this.textBox30,
            this.panel2,
            this.subReport1,
            this.textBox19,
            this.textBox21,
            this.textBox22,
            this.subReport3});
            this.detail.KeepTogether = false;
            this.detail.Name = "detail";
            this.detail.ItemDataBinding += new System.EventHandler(this.detail_ItemDataBinding);
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(6.0236225128173828, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.59055107831954956, Telerik.Reporting.Drawing.UnitType.Inch));
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
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.7100556492805481, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.59055107831954956, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.75, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
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
            this.textBox10,
            this.textBox11,
            this.nameCaptionTextBox,
            this.textBox7});
            this.panel2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.8035413026809692, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.16184088587760925, Telerik.Reporting.Drawing.UnitType.Cm));
            this.panel2.Name = "panel2";
            this.panel2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.4574604034423828, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2904561460018158, Telerik.Reporting.Drawing.UnitType.Inch));
            this.panel2.Style.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel2.Style.Font.Name = "Tahoma";
            this.panel2.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.panel2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.5, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.81356710195541382, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox10.StyleName = "Caption";
            this.textBox10.Value = "V. Unitario";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.3136458396911621, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.1430082321166992, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox11.StyleName = "Caption";
            this.textBox11.Value = "Valor Total";
            // 
            // nameCaptionTextBox
            // 
            this.nameCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.9052002429962158, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm));
            this.nameCaptionTextBox.Name = "nameCaptionTextBox";
            this.nameCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.7435827255249023, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929, Telerik.Reporting.Drawing.UnitType.Inch));
            this.nameCaptionTextBox.Style.Font.Bold = true;
            this.nameCaptionTextBox.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.nameCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.nameCaptionTextBox.StyleName = "Caption";
            this.nameCaptionTextBox.Value = "Nombre Producto";
            // 
            // textBox7
            // 
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.75, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox7.StyleName = "Caption";
            this.textBox7.Value = "Cantidad";
            // 
            // subReport1
            // 
            this.subReport1.KeepTogether = false;
            this.subReport1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.8035413026809692, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.89979970455169678, Telerik.Reporting.Drawing.UnitType.Cm));
            this.subReport1.Name = "subReport1";
            this.subReport1.Parameters.Add(new Telerik.Reporting.Parameter("IdCabeceraPedido", "=Fields.IdCabeceraPedido"));
            this.subReport1.ReportSource = this.reporteDetallePedido1;
            this.subReport1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(16.401948928833008, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000002384185791, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.20242755115032196, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.4273868799209595, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.0995734930038452, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox19.Style.Font.Bold = true;
            this.textBox19.Style.Font.Name = "Verdana";
            this.textBox19.Value = "Observación:";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.3020797967910767, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.4412895441055298, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.8674187660217285, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.51181107759475708, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox21.Value = "=Fields.Retira";
            // 
            // textBox22
            // 
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.19685046374797821, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.84750968217849731, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.7716535329818726, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox22.Style.Font.Bold = true;
            this.textBox22.Style.Font.Name = "Verdana";
            this.textBox22.Value = "Remitos Pendientes:";
            // 
            // subReport3
            // 
            this.subReport3.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.8014936447143555, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.6608750820159912, Telerik.Reporting.Drawing.UnitType.Cm));
            this.subReport3.Name = "subReport3";
            this.subReport3.Parameters.Add(new Telerik.Reporting.Parameter("CodCliente", "=Fields.objCliente.CodigoExterno"));
            this.subReport3.ReportSource = this.reporteDetalleRemitos1;
            this.subReport3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(16.401948928833008, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000002384185791, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.083333410322666168, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.070765972137451172, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.5780839920043945, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox6.Style.Font.Name = "Tahoma";
            this.textBox6.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "=Fields.objDireccion.Calle";
            // 
            // lblTransporte
            // 
            this.lblTransporte.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.11755291372537613, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.29484987258911133, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblTransporte.Name = "lblTransporte";
            this.lblTransporte.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.66658145189285278, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblTransporte.Style.BorderColor.Default = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(192)))));
            this.lblTransporte.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.lblTransporte.Style.BorderWidth.Default = new Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.lblTransporte.Style.Font.Bold = false;
            this.lblTransporte.Style.Font.Name = "Tahoma";
            this.lblTransporte.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.lblTransporte.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.lblTransporte.Value = "Transporte:";
            // 
            // txtCosto
            // 
            this.txtCosto.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.904954731464386, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.039370059967041016, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtCosto.Name = "txtCosto";
            this.txtCosto.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.0775859355926514, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.txtCosto.Style.Font.Bold = true;
            this.txtCosto.Style.Font.Name = "Verdana";
            this.txtCosto.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Point);
            this.txtCosto.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtCosto.Value = "=Fields.CostoFlete";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.904954731464386, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.29484987258911133, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.0775859355926514, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox16.Style.Font.Bold = true;
            this.textBox16.Style.Font.Name = "Verdana";
            this.textBox16.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox16.Value = "=Fields.Transportista";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.083333410322666168, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.569675624370575, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.5780839920043945, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox13.Style.Font.Name = "Tahoma";
            this.textBox13.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "=Fields.objDireccion.Provincia";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.083333410322666168, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.31238254904747009, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.5780839920043945, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox12.Style.Font.Name = "Tahoma";
            this.textBox12.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "=Fields.objDireccion.Localidad";
            // 
            // lblFlete
            // 
            this.lblFlete.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.11755291372537613, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.039370059967041016, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblFlete.Name = "lblFlete";
            this.lblFlete.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.66658145189285278, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblFlete.Style.BorderColor.Default = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(192)))));
            this.lblFlete.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.lblFlete.Style.BorderWidth.Default = new Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.lblFlete.Style.Font.Bold = false;
            this.lblFlete.Style.Font.Name = "Tahoma";
            this.lblFlete.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.lblFlete.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.lblFlete.Value = "Costo Flete:";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.904954731464386, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.0136094093322754, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.0775859355926514, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox18.Style.Font.Bold = true;
            this.textBox18.Style.Font.Name = "Verdana";
            this.textBox18.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.Value = "=Fields.objClienteSolicitante.Clasif1";
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.11755291372537613, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.0136094093322754, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.66658145189285278, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox17.Style.BorderColor.Default = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(192)))));
            this.textBox17.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox17.Style.BorderWidth.Default = new Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox17.Style.Font.Bold = false;
            this.textBox17.Style.Font.Name = "Tahoma";
            this.textBox17.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox17.Value = "Grupo:";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.11755291372537613, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.77738875150680542, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.66658145189285278, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox8.Style.BorderColor.Default = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(192)))));
            this.textBox8.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderWidth.Default = new Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox8.Style.Font.Bold = false;
            this.textBox8.Style.Font.Name = "Tahoma";
            this.textBox8.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "Líder:";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.904954731464386, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.77738875150680542, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.0775859355926514, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.Font.Name = "Verdana";
            this.textBox9.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "=Fields.objCliente.Vendedor";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.11755291372537613, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.5310705304145813, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.66658145189285278, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox20.Style.BorderColor.Default = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(192)))));
            this.textBox20.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox20.Style.BorderWidth.Default = new Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox20.Style.Font.Bold = false;
            this.textBox20.Style.Font.Name = "Tahoma";
            this.textBox20.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.Value = "FP:";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.904954731464386, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.53827971220016479, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.0775859355926514, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Name = "Verdana";
            this.textBox4.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "=Fields.objFormaDePago.Descripcion";
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
            this.group1.GroupKeepTogether = Telerik.Reporting.GroupKeepTogether.All;
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = new Telerik.Reporting.Drawing.Unit(0.13229165971279144, Telerik.Reporting.Drawing.UnitType.Cm);
            this.groupFooterSection1.Name = "groupFooterSection1";
            this.groupFooterSection1.PageBreak = Telerik.Reporting.PageBreak.After;
            this.groupFooterSection1.Style.Visible = true;
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = new Telerik.Reporting.Drawing.Unit(6.6076092720031738, Telerik.Reporting.Drawing.UnitType.Cm);
            this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.panel1,
            this.panel3,
            this.panel4});
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            this.groupHeaderSection1.PrintOnEveryPage = true;
            // 
            // panel1
            // 
            this.panel1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox23,
            this.textBox14,
            this.Text8,
            this.Field6,
            this.textBox15,
            this.txtCodigoExterno,
            this.textBox32,
            this.textBox3,
            this.textBox5,
            this.lblCod,
            this.barcodeCodigoExterno,
            this.Text12,
            this.Id1});
            this.panel1.Name = "panel1";
            this.panel1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(20.509899139404297, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.2273073196411133, Telerik.Reporting.Drawing.UnitType.Cm));
            this.panel1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            // 
            // textBox23
            // 
            this.textBox23.CanGrow = false;
            this.textBox23.Format = "{0:d}";
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(15.899999618530273, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.5676085948944092, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.7948546409606934, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox23.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.textBox23.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.textBox23.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox23.Style.Color = System.Drawing.Color.Black;
            this.textBox23.Style.Font.Bold = false;
            this.textBox23.Style.Font.Italic = false;
            this.textBox23.Style.Font.Name = "Tahoma";
            this.textBox23.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox23.Style.Font.Strikeout = false;
            this.textBox23.Style.Font.Underline = false;
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox23.Style.Visible = true;
            this.textBox23.Value = "= Parameters.UsuarioImpresion";
            // 
            // textBox14
            // 
            this.textBox14.CanGrow = false;
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(15.342082977294922, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.9856250286102295, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.8749856948852539, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox14.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.textBox14.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.textBox14.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox14.Style.Color = System.Drawing.Color.Black;
            this.textBox14.Style.Font.Bold = true;
            this.textBox14.Style.Font.Italic = false;
            this.textBox14.Style.Font.Name = "Verdana";
            this.textBox14.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox14.Style.Font.Strikeout = false;
            this.textBox14.Style.Font.Underline = false;
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox14.Style.Visible = true;
            this.textBox14.Value = "=IIf(Fields.NroImpresion = 0, \"ORIGINAL\", \"IMPRESION NRO \" + Fields.NroImpresion)" +
                "";
            // 
            // Text8
            // 
            this.Text8.CanGrow = false;
            this.Text8.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.0270833969116211, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.0452239513397217, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Text8.Name = "Text8";
            this.Text8.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.4199995994567871, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Text8.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.Text8.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.Text8.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.Text8.Style.Color = System.Drawing.Color.Black;
            this.Text8.Style.Font.Bold = false;
            this.Text8.Style.Font.Italic = false;
            this.Text8.Style.Font.Name = "Tahoma";
            this.Text8.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
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
            this.Field6.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(7.5510797500610352, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.0675084590911865, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Field6.Name = "Field6";
            this.Field6.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.1814374923706055, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Field6.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.Field6.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.Field6.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.Field6.Style.Color = System.Drawing.Color.Black;
            this.Field6.Style.Font.Bold = true;
            this.Field6.Style.Font.Italic = false;
            this.Field6.Style.Font.Name = "Verdana";
            this.Field6.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Point);
            this.Field6.Style.Font.Strikeout = false;
            this.Field6.Style.Font.Underline = false;
            this.Field6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.Field6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.Field6.Style.Visible = true;
            this.Field6.Value = "=Now()";
            // 
            // textBox15
            // 
            this.textBox15.CanGrow = false;
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.0270833969116211, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.463140606880188, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.4199995994567871, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox15.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.textBox15.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.textBox15.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox15.Style.Color = System.Drawing.Color.Black;
            this.textBox15.Style.Font.Bold = false;
            this.textBox15.Style.Font.Italic = false;
            this.textBox15.Style.Font.Name = "Tahoma";
            this.textBox15.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox15.Style.Font.Strikeout = false;
            this.textBox15.Style.Font.Underline = false;
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.Style.Visible = true;
            this.textBox15.Value = "Fecha Pedido:";
            // 
            // txtCodigoExterno
            // 
            this.txtCodigoExterno.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.9728660583496094, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.36606311798095703, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtCodigoExterno.Name = "txtCodigoExterno";
            this.txtCodigoExterno.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.8268985748291016, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtCodigoExterno.Style.Font.Bold = true;
            this.txtCodigoExterno.Style.Font.Name = "Verdana";
            this.txtCodigoExterno.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Point);
            this.txtCodigoExterno.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtCodigoExterno.Value = "=Fields.objCliente.CodigoExterno";
            // 
            // textBox32
            // 
            this.textBox32.Format = "{0:d}";
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.9728660583496094, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.58481305837631226, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.8268983364105225, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox32.Style.Font.Bold = true;
            this.textBox32.Style.Font.Name = "Verdana";
            this.textBox32.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox32.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox32.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox32.Value = "=Fields.FechaPedido";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.0270833969116211, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.29897397756576538, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.95275545120239258, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox3.Style.Font.Bold = false;
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "Consultor:";
            // 
            // textBox5
            // 
            this.textBox5.Format = "{0:d}";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(7.5510797500610352, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.32125863432884216, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.8268983364105225, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Name = "Verdana";
            this.textBox5.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "=Fields.objCliente.Nombre";
            // 
            // lblCod
            // 
            this.lblCod.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.9791666269302368, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.336456298828125, Telerik.Reporting.Drawing.UnitType.Inch));
            this.lblCod.Name = "lblCod";
            this.lblCod.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.9527556300163269, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.lblCod.Style.Font.Bold = false;
            this.lblCod.Style.Font.Name = "Tahoma";
            this.lblCod.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.lblCod.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.lblCod.Value = "Código Cliente:";
            // 
            // barcodeCodigoExterno
            // 
            this.barcodeCodigoExterno.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.21166665852069855, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.29897397756576538, Telerik.Reporting.Drawing.UnitType.Cm));
            this.barcodeCodigoExterno.Name = "barcodeCodigoExterno";
            this.barcodeCodigoExterno.ShowText = false;
            this.barcodeCodigoExterno.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.6999998092651367, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.3398008346557617, Telerik.Reporting.Drawing.UnitType.Cm));
            this.barcodeCodigoExterno.Stretch = true;
            this.barcodeCodigoExterno.Value = "=Fields.objCliente.CodigoExterno";
            // 
            // Text12
            // 
            this.Text12.CanGrow = false;
            this.Text12.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(15.600000381469727, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.10698393732309341, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Text12.Name = "Text12";
            this.Text12.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.3000016212463379, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
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
            this.Id1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(15.100000381469727, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.6096922755241394, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Id1.Name = "Id1";
            this.Id1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.2170705795288086, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.2288786172866821, Telerik.Reporting.Drawing.UnitType.Cm));
            this.Id1.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.Id1.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.Id1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.Id1.Style.Color = System.Drawing.Color.OrangeRed;
            this.Id1.Style.Font.Bold = true;
            this.Id1.Style.Font.Italic = false;
            this.Id1.Style.Font.Name = "Verdana";
            this.Id1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(25, Telerik.Reporting.Drawing.UnitType.Point);
            this.Id1.Style.Font.Strikeout = false;
            this.Id1.Style.Font.Underline = false;
            this.Id1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.Id1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.Id1.Style.Visible = true;
            this.Id1.Value = "=Fields.Nro";
            // 
            // panel3
            // 
            this.panel3.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox6,
            this.textBox13,
            this.textBox12});
            this.panel3.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.2276082038879395, Telerik.Reporting.Drawing.UnitType.Cm));
            this.panel3.Name = "panel3";
            this.panel3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(9.6000003814697266, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.3400003910064697, Telerik.Reporting.Drawing.UnitType.Cm));
            this.panel3.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.panel3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.panel3.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.panel3.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.panel3.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            // 
            // panel4
            // 
            this.panel4.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.lblFlete,
            this.txtCosto,
            this.textBox16,
            this.lblTransporte,
            this.textBox18,
            this.textBox17,
            this.textBox8,
            this.textBox9,
            this.textBox20,
            this.textBox4});
            this.panel4.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.60141658782959, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.2276082038879395, Telerik.Reporting.Drawing.UnitType.Cm));
            this.panel4.Name = "panel4";
            this.panel4.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(10.908482551574707, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.3400003910064697, Telerik.Reporting.Drawing.UnitType.Cm));
            this.panel4.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.panel4.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.panel4.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.panel4.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            // 
            // ReporteCuerpoPedidoInterno
            // 
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.pageHeader,
            this.pageFooter,
            this.reportHeader,
            this.detail});
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(0.54000002145767212, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(0.54000002145767212, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(0.54000002145767212, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(0.54000002145767212, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Legal;
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
            this.Width = new Telerik.Reporting.Drawing.Unit(20.509998321533203, Telerik.Reporting.Drawing.UnitType.Cm);
            ((System.ComponentModel.ISupportInitialize)(this.reporteDetallePedido1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reporteDetalleRemitos1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private PageHeaderSection pageHeader;
        private PageFooterSection pageFooter;
        private ReportHeaderSection reportHeader;
        private DetailSection detail;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox lblTransporte;
        private Telerik.Reporting.TextBox txtCosto;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox lblFlete;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox29;
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
        private Telerik.Reporting.TextBox textBox20;
        private ReporteDetallePedido reporteDetallePedido1;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox22;
        private SubReport subReport3;
        private ReporteDetalleRemitos reporteDetalleRemitos1;
        private Telerik.Reporting.TextBox lblPaginado;
        private Telerik.Reporting.Panel panel1;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox Text8;
        private Telerik.Reporting.TextBox Field6;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox txtCodigoExterno;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox lblCod;
        private Barcode barcodeCodigoExterno;
        private Telerik.Reporting.TextBox Text12;
        private Telerik.Reporting.TextBox Id1;
        private Telerik.Reporting.Panel panel3;
        private Telerik.Reporting.Panel panel4;

    }
}