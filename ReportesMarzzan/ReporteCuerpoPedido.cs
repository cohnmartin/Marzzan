namespace ReportesMarzzan
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Linq;
    using System.Data.Linq;
    using CommonMarzzan;


    /// <summary>
    /// Summary description for ReporteCuerpoPedido.
    /// </summary>
    public partial class ReporteCuerpoPedido : Telerik.Reporting.Report
    {
        public ReporteCuerpoPedido()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public void InitReport(long Id)
        {
            CabeceraPedido currentNota = null;
            Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

            var notaPedido = (from N in dc.CabeceraPedidos
                              where N.IdCabeceraPedido == Id
                              select N).ToList();

            this.DataSource = notaPedido;
            currentNota = notaPedido.First<CabeceraPedido>();


            if (currentNota.TipoPedido == "RT" || currentNota.TipoPedido == "ND")
            {

                /// si No da error es porque se trata de un remito que proviene 
                /// del regalo de tu líder
                try
                {
                    long ArtCant = (from N in currentNota.DetallePedidos
                                    where N.objProducto.Tipo == 'R'
                                    select N.Cantidad.Value).Sum();

                    this.ReportParameters["TotalProductos"].Value = ArtCant.ToString();
                }
                /// si da error es porque se trata de un remito de la cuenta bolsos
                /// entonces debo contar los productos normales
                catch
                {
                    long ArtCant = (from N in currentNota.DetallePedidos
                                    where  N.objProducto.Tipo == 'A'
                                    select N.Cantidad.Value).Sum();

                    this.ReportParameters["TotalProductos"].Value = ArtCant.ToString();
                }

            }
            else
            {
                long ArtCant = (from N in currentNota.DetallePedidos
                                where N.objProducto.Tipo == 'A'
                                select N.Cantidad.Value).Sum();

                this.ReportParameters["TotalProductos"].Value = ArtCant.ToString();


            }



            try
            {
                DetallePedido DetalleGastoEnvio = (from P in currentNota.DetallePedidos
                                                   where P.objProducto.Tipo == 'G'
                                                   select P).First<DetallePedido>();

                this.ReportParameters["CostoFlete"].Value = DetalleGastoEnvio.objPresentacion.Precio;


                this.ReportParameters["Transportista"].Value = (from C in dc.ConfTransportes
                                                                where C.Provincia == currentNota.objDireccion.Provincia &&
                                                                C.Localidad == currentNota.objDireccion.Localidad &&
                                                                C.FormaDePago == currentNota.objFormaDePago.Descripcion &&
                                                                C.IdProducto == DetalleGastoEnvio.objProducto.IdProducto
                                                                select C).First<ConfTransporte>().Transporte;

            }
            catch
            {
                this.ReportParameters["CostoFlete"].Value = 0;
                this.ReportParameters["Transportista"].Value = "PROXIMO ENVIO";
            }

            ///// Busco los remitos que se vieron afectados en este pedido
            //string remitosAfectados = "";
            //var RA = from r in dc.RemitosAfectados
            //         where r.IdCabecera == Id
            //         select r.DescArticulo;

            //foreach (var item in RA)
            //{
            //    remitosAfectados += item + " <br/> ";
            //}

            //this.ReportParameters["Remitos"].Value = remitosAfectados;


            var detalle = (from P in notaPedido.First<CabeceraPedido>().DetallePedidos
                           where P.objProducto.Tipo == 'P' || P.objProducto.Tipo == 'A' || P.objProducto.Tipo == 'N'
                           || P.objProducto.Tipo == 'R' || P.objProducto.Tipo == 'I' || P.objProducto.Tipo == 'D'
                           orderby P.CodigoCompleto
                           select P).ToList<DetallePedido>();

            this.reporteDetallePedido1.DataSource = detalle;

            if (notaPedido.FirstOrDefault().DetalleImpuestos != null)
            {
                string[] detalleImpuesto = notaPedido.FirstOrDefault().DetalleImpuestos.Split('@');

                if (detalleImpuesto.Count() > 1)
                {
                    lblNeto.Value = detalleImpuesto[0];
                    lblIva.Value = detalleImpuesto[1];
                    lblRG30.Value = detalleImpuesto[2];
                    lblRG212.Value = detalleImpuesto[3];
                    lblTotalDetalle.Value = detalleImpuesto[4];
                }
                else
                {
                    lblNeto.Value = "-";
                    lblIva.Value = "-";
                    lblRG30.Value = "-";
                    lblRG212.Value = "-";
                    lblTotalDetalle.Value = currentNota.MontoTotal.ToString();

                }
            }
            else {
                lblNeto.Value = "-";
                lblIva.Value = "-";
                lblRG30.Value = "-";
                lblRG212.Value = "-";
                lblTotalDetalle.Value = currentNota.MontoTotal.ToString();
            }


        }

    }
}