namespace ReportesMarzzan
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Data;
    using CommonMarzzan;

    /// <summary>
    /// Summary description for ReporteDetallePedido.
    /// </summary>
    public partial class ReporteDetallePedido : Telerik.Reporting.Report
    {
        public ReporteDetallePedido()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        private void detail_ItemDataBound(object sender, EventArgs e)
        {
            // Get the detail section object from sender
            Telerik.Reporting.Processing.DetailSection section = (Telerik.Reporting.Processing.DetailSection)sender;
            // From the section object get the DataRowView
            DetallePedido dataRowView = (DetallePedido)section.DataObject.RawData;
            Telerik.Reporting.Processing.TextBox procTextbox = (Telerik.Reporting.Processing.TextBox)section.ChildElements.Find("textBox3", true)[0];

            if (dataRowView.objProducto.Tipo == 'P' || dataRowView.objProducto.Tipo == 'R' || dataRowView.objProducto.Tipo == 'G'
                 || dataRowView.objProducto.Tipo == 'D' || dataRowView.objProducto.Tipo == 'I' || dataRowView.objProducto.Tipo == 'N')
            {
                procTextbox.Value = dataRowView.objProducto.Descripcion;
            }
            else
            {
                if (dataRowView.objProducto.DescripcionCompleta.ToLower().Contains("incorporac"))
                {
                    procTextbox.Value = dataRowView.objProducto.Descripcion + " x " + dataRowView.objPresentacion.Descripcion;
                }
                else
                {
                    if (dataRowView.objProducto.objPadre.objPadre.Codigo == "02")
                    {
                        procTextbox.Value = dataRowView.objProducto.objPadre.Descripcion + " " + dataRowView.objProducto.DescripcionCompleta + dataRowView.objPresentacion.Descripcion;
                    }
                    else
                        procTextbox.Value = dataRowView.objProducto.DescripcionCompleta + dataRowView.objPresentacion.Descripcion;

                    //procTextbox.Value = dataRowView.objProducto.objPadre.objPadre.Descripcion + " " + dataRowView.objProducto.objPadre.Descripcion + " " + dataRowView.objProducto.DescripcionCompleta + dataRowView.objPresentacion.Descripcion;
                }
            }

        }
    }
}