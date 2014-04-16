namespace ReportesMarzzan
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class ReporteDetallePedido
    {
        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.Panel panel1;
        private Telerik.Reporting.TextBox orderQtyCaptionTextBox;
        private Telerik.Reporting.TextBox unitPriceCaptionTextBox;
        private Telerik.Reporting.TextBox lineTotalCaptionTextBox;
        private Telerik.Reporting.TextBox nameCaptionTextBox;
        private Telerik.Reporting.TextBox productNumberDataTextBox;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.PageFooterSection pageFooter;



        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.panel1 = new Telerik.Reporting.Panel();
            this.orderQtyCaptionTextBox = new Telerik.Reporting.TextBox();
            this.unitPriceCaptionTextBox = new Telerik.Reporting.TextBox();
            this.lineTotalCaptionTextBox = new Telerik.Reporting.TextBox();
            this.nameCaptionTextBox = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.productNumberDataTextBox = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = new Telerik.Reporting.Drawing.Unit(1.0000001192092896, Telerik.Reporting.Drawing.UnitType.Cm);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.panel1});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            // 
            // panel1
            // 
            this.panel1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.unitPriceCaptionTextBox,
            this.lineTotalCaptionTextBox,
            this.nameCaptionTextBox,
            this.orderQtyCaptionTextBox});
            this.panel1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.099999971687793732, Telerik.Reporting.Drawing.UnitType.Cm));
            this.panel1.Name = "panel1";
            this.panel1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.4574604034423828, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2904561460018158, Telerik.Reporting.Drawing.UnitType.Inch));
            this.panel1.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.panel1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            // 
            // orderQtyCaptionTextBox
            // 
            this.orderQtyCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.9418537198798731E-05, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.010456164367496967, Telerik.Reporting.Drawing.UnitType.Inch));
            this.orderQtyCaptionTextBox.Name = "orderQtyCaptionTextBox";
            this.orderQtyCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.74799209833145142, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929, Telerik.Reporting.Drawing.UnitType.Inch));
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
            this.unitPriceCaptionTextBox.Value = "V. U.";
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
            // nameCaptionTextBox
            // 
            this.nameCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.9002001285552979, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.026555541902780533, Telerik.Reporting.Drawing.UnitType.Cm));
            this.nameCaptionTextBox.Name = "nameCaptionTextBox";
            this.nameCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.7518107891082764, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.2800000011920929, Telerik.Reporting.Drawing.UnitType.Inch));
            this.nameCaptionTextBox.Style.Font.Bold = true;
            this.nameCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.nameCaptionTextBox.StyleName = "Caption";
            this.nameCaptionTextBox.Value = "Nombre Producto";
            // 
            // detail
            // 
            this.detail.Height = new Telerik.Reporting.Drawing.Unit(0.59999984502792358, Telerik.Reporting.Drawing.UnitType.Cm);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox2,
            this.textBox3,
            this.productNumberDataTextBox});
            this.detail.KeepTogether = false;
            this.detail.Name = "detail";
            this.detail.Style.BorderColor.Default = System.Drawing.Color.Silver;
            this.detail.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.detail.ItemDataBound += new System.EventHandler(this.detail_ItemDataBound);
            // 
            // productNumberDataTextBox
            // 
            this.productNumberDataTextBox.Name = "productNumberDataTextBox";
            this.productNumberDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.74579447507858276, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000000298023224, Telerik.Reporting.Drawing.UnitType.Inch));
            this.productNumberDataTextBox.Style.Font.Name = "Tahoma";
            this.productNumberDataTextBox.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.productNumberDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.productNumberDataTextBox.StyleName = "Data";
            this.productNumberDataTextBox.Value = "=Fields.Cantidad";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(11.428571701049805, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(9.9908742413390428E-05, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.91025906801223755, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000000298023224, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.StyleName = "Data";
            this.textBox1.Value = "=Fields.ValorUnitario";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(13.740541458129883, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(9.9908742413390428E-05, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.91025906801223755, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000000298023224, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.StyleName = "Data";
            this.textBox2.Value = "=Fields.ValorTotal";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.8945176601409912, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.7534856796264648, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000000298023224, Telerik.Reporting.Drawing.UnitType.Inch));
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox3.StyleName = "Data";
            this.textBox3.Value = "=Fields.objProducto.Descripcion ";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = new Telerik.Reporting.Drawing.Unit(0.79999977350234985, Telerik.Reporting.Drawing.UnitType.Cm);
            this.pageFooter.Name = "pageFooter";
            this.pageFooter.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            // 
            // ReporteDetallePedido
            // 
            this.Filters.AddRange(new Telerik.Reporting.Data.Filter[] {
            new Telerik.Reporting.Data.Filter("= Fields.CabeceraPedido", Telerik.Reporting.Data.FilterOperator.Equal, "=Parameters.IdCabeceraPedido")});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail,
            this.pageFooter});
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(0.54000002145767212, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(0.54000002145767212, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(0.54000002145767212, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(0.54000002145767212, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.Name = "IdCabeceraPedido";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            this.ReportParameters.Add(reportParameter1);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = new Telerik.Reporting.Drawing.Unit(16.401948928833008, Telerik.Reporting.Drawing.UnitType.Cm);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

       
    }
}