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
    using System.Collections.Generic;

    /// <summary>
    /// Summary description for Report1.
    /// </summary>
    public partial class ReporteCuerpoPedidoInterno : Telerik.Reporting.Report
    {
        public ReporteCuerpoPedidoInterno()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public void InitReport(List<long> ids, string Usuario)
        {

            Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

            var notaPedido = from N in dc.CabeceraPedidos
                             where ids.Contains(N.IdCabeceraPedido)
                             select N;

            this.DataSource = notaPedido.ToList();


            var detalle = (from P in dc.DetallePedidos
                           where P.objProducto.Tipo == 'P' || P.objProducto.Tipo == 'A'
                           || P.objProducto.Tipo == 'R' || P.objProducto.Tipo == 'I'
                           orderby P.CodigoCompleto
                           select P).ToList<DetallePedido>();

            this.reporteDetallePedido1.DataSource = detalle;


            var remPendientes = from R in dc.RemitosPendientes
                                select R;


            this.reporteDetalleRemitos1.DataSource = remPendientes.ToList<RemitosPendiente>();


            this.ReportParameters["UsuarioImpresion"].Value = Usuario;
            
        }

        private void detail_ItemDataBound(object sender, EventArgs e)
        {
            

        }

        private void detail_ItemDataBinding(object sender, EventArgs e)
        {
            Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

            // Get the detail section object from sender
            Telerik.Reporting.Processing.DetailSection section = (Telerik.Reporting.Processing.DetailSection)sender;
            // From the section object get the DataRowView
            CabeceraPedido currentCabecera = (CabeceraPedido)section.DataObject.RawData;

            if (currentCabecera.TipoPedido == "RT" || currentCabecera.TipoPedido == "ND")
            {

                /// si No da error es porque se trata de un remito que proviene 
                /// del regalo de tu líder
                try
                {


                    long ArtCant = (from N in currentCabecera.DetallePedidos
                                    where N.objProducto.Tipo == 'R'
                                    select N.Cantidad.Value).Sum();

                    currentCabecera.TotalProductos = ArtCant.ToString();
                    //this.ReportParameters["TotalProductos"].Value = ArtCant.ToString();
                }


                /// si da error es porque se trata de un remito de la cuenta bolsos
                /// entonces debo contar los productos normales
                catch
                {
                    long ArtCant = (from N in currentCabecera.DetallePedidos
                                    where N.objProducto.Tipo == 'A'
                                    select N.Cantidad.Value).Sum();

                    currentCabecera.TotalProductos = ArtCant.ToString();
                    //this.ReportParameters["TotalProductos"].Value = ArtCant.ToString();
                }

            }
            else
            {
                long ArtCant = (from N in currentCabecera.DetallePedidos
                                where N.objProducto.Tipo == 'A'
                                select N.Cantidad.Value).Sum();

                currentCabecera.TotalProductos = ArtCant.ToString();
                //this.ReportParameters["TotalProductos"].Value = ArtCant.ToString();


            }



            try
            {
                DetallePedido DetalleGastoEnvio = (from P in currentCabecera.DetallePedidos
                                                   where P.objProducto.Tipo == 'G'
                                                   select P).First<DetallePedido>();

                currentCabecera.CostoFlete = DetalleGastoEnvio.objPresentacion.Precio.Value;
                //this.ReportParameters["CostoFlete"].Value = DetalleGastoEnvio.objPresentacion.Precio;


              

                currentCabecera.Transportista = (from C in dc.ConfTransportes
                                                 where C.Provincia == currentCabecera.objDireccion.Provincia &&
                                                 C.Localidad == currentCabecera.objDireccion.Localidad &&
                                                 C.FormaDePago == currentCabecera.objFormaDePago.Descripcion &&
                                                 C.IdProducto == DetalleGastoEnvio.objProducto.IdProducto
                                                 select C).First<ConfTransporte>().Transporte;

            }
            catch
            {
                currentCabecera.CostoFlete = 0;
                currentCabecera.Transportista = "PROXIMO ENVIO";

                //this.ReportParameters["CostoFlete"].Value = 0;
                //this.ReportParameters["Transportista"].Value = "PROXIMO ENVIO";
            }

           

        }

    }
}