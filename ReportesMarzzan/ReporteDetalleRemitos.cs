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
    public partial class ReporteDetalleRemitos : Telerik.Reporting.Report
    {

        public ReporteDetalleRemitos()
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
            

        }
    }
}