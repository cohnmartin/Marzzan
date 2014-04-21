
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Telerik.Web.UI;
using System.IO;
using CommonMarzzan;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.Script.Services;
using System.IO.Compression;

public partial class Version15 : BasePage
{
    public static List<Producto> _productos = null;


    private Marzzan_InfolegacyDataContext Contexto
    {
        get
        {

            if (Session["Context"] == null)
            {
                Session.Add("Context", new Marzzan_InfolegacyDataContext());
            }

            return (Marzzan_InfolegacyDataContext)Session["Context"];
        }

    }


    protected override void PageLoad()
    {
        #region PostBack
        if (!IsPostBack)
        {
            Session.Add("Cliente", null);
            Session.Add("detPedido", new List<DetallePedido>());
            Session.Add("Consultor", null);
            Session.Add("PromosGuardadas", new List<DetallePedido>());
            ViewState.Add("TransportistaSeleccionado", "");
            Session["Context"] = null;
            Cliente cliente = null;


            _productos = (from prod in Contexto.Productos
                          where prod.Tipo == 'A'
                          select prod).ToList<Producto>();


            /// Busco todos las presentaciones que tiene padre
            /// y recupero el id del padre de estos
            List<long> idProductosExistente = (from prod in Contexto.Presentacions
                                               where (prod.objProducto.Tipo == 'A' && prod.Activo == true)
                                               select prod.objProducto.objPadre.IdProducto).Distinct().ToList<long>();

            /// 1.Unidad de negocio - 2.linea - 3.fragancias - presentaciones (Tabla Presentaciones)
            /// Solo cargo en el arbol aquellos lineas donde las fregancias posea
            /// almenos un presentación.
            var productos = from prod in _productos
                            where (prod.Nivel < 2 && prod.ColHijos.Count > 0 && prod.Tipo == 'A')
                            || (prod.Nivel > 1 && idProductosExistente.Contains(prod.IdProducto))
                            select prod;



            RadTreeProductos.DataSource = Helper.LINQToDataTable<Producto>(productos);
            RadTreeProductos.DataBind();


            if (Request.QueryString["IdPedido"] != null)
            {

                CabeceraPedido CurrentCabecera = (from C in Contexto.CabeceraPedidos
                                                  where C.IdCabeceraPedido == long.Parse(Request.QueryString["IdPedido"].ToString())
                                                  select C).SingleOrDefault();

                if (CurrentCabecera.EsTemporal.HasValue && CurrentCabecera.EsTemporal.Value)
                {
                    cliente = CurrentCabecera.objCliente;
                    Session["Cliente"] = CurrentCabecera.objCliente;
                    Session["Consultor"] = CurrentCabecera.objCliente;

                    //Session["detPedido"] = CurrentCabecera.DetallePedidos.ToList<DetallePedido>();

                    Session["detPedido"] = (from d in CurrentCabecera.DetallePedidos
                                            where d.objProducto.Tipo == 'A' && d.objPromocionOrigen == null
                                            select d).ToList();

                    Session["PromosGuardadas"] = (from d in CurrentCabecera.DetallePedidos
                                                  where d.objProducto.Tipo == 'P'
                                                  select d).ToList();

                    CargarFormaDePago(cliente);
                    cboFormaPago.SelectedValue = CurrentCabecera.FormaPago.ToString();

                    cboConsultores.Items.Add(new RadComboBoxItem(cliente.Nombre.Trim(), cliente.IdCliente.ToString()));
                    CargarEncabezado(cliente, CurrentCabecera.IdCabeceraPedido);
                    cboConsultores.Enabled = false;
                    cboConsultores.SelectedIndex = 0;

                    txtObservacion.Text = CurrentCabecera.Retira;
                    ActualizarTotalesGenerales();
                    UCTotalizadorNivel.IniciarControl();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "EdicionInvalida", "AlertaEdicionInvalida();", true);
                    //RadTabStrip2.Visible = false;
                    //RadMultiPrincipal.Visible = false;
                }
            }
            else
            {

                cliente = (from C in Contexto.Clientes
                           where C.IdCliente == long.Parse(Session["IdUsuario"].ToString())
                           select C).Single<Cliente>();


                List<Cliente> consultores = Helper.ObtenerConsultoresSubordinados(cliente);


                if (consultores.Count() > 1)
                {
                    cboConsultores.AppendDataBoundItems = true;
                    cboConsultores.Items.Add(new RadComboBoxItem("Seleccione un Consultor", "0"));
                    cboConsultores.Items.Add(new RadComboBoxItem(cliente.Nombre.Trim(), cliente.IdCliente.ToString()));
                    cboConsultores.DataTextField = "Nombre";
                    cboConsultores.DataValueField = "IdCliente";
                    cboConsultores.DataSource = consultores;
                    cboConsultores.DataBind();
                    CargarFormaDePago(null);
                }
                else
                {
                    Session["Consultor"] = cliente;
                    Session["Cliente"] = cliente;
                    cboConsultores.Items.Add(new RadComboBoxItem(cliente.Nombre.Trim(), cliente.IdCliente.ToString()));
                    CargarFormaDePago(cliente);
                    CargarEncabezado(cliente, 0);
                    cboConsultores.Enabled = false;
                    UCTotalizadorNivel.IniciarControl();
                    cboConsultores.SelectedIndex = 0;


                }


            }







        }

        #endregion

        ucGrillaDirecciones.DireccionSeleccionada += new RowSelectedHanddler(ucGrillaDirecciones_DireccionSeleccionada);
        //UcTotalizadorPedidos.LineaPedidoEliminada += new DeletePedidoHanddler(TotalizadorPedidos_LineaPedidoEliminada);
    }

    #region Eventos
    public void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

        if (e.Argument != "undefined")
        {
            if (e.Argument == "Limpiar")
            {
                LimpiarPedido();
            }
            else if (e.Argument == "GuardarTemporal")
            {
                string id = "";
                if (Request.QueryString["IdPedido"] != null)
                    id = Request.QueryString["IdPedido"].ToString();

                GuardarPedido(id, true, true);

            }
            else if (e.Argument == "Direcciones")
            {

                Cliente cliente = (from C in Contexto.Clientes
                                   where C.IdCliente == long.Parse(cboConsultores.SelectedValue)
                                   select C).Single<Cliente>();


                Session["Cliente"] = cliente;
                Session["Consultor"] = cliente;

                CargarDireccionesEntrega(cliente);

                ActualizarProductosEspeciales(cliente);

                if ((Session["detPedido"] as List<DetallePedido>).Count > 0)
                {
                   // CalcularPromociones();
                }

                UCTotalizadorNivel.IniciarControl();

                CargarFormaDePago(cliente);
            }
            else
            {

                string[] datos = e.Argument.ToString().Split('@');

                string TipoOperacion = "";
                string[] IdsPresentaciones = datos[0].Split('|');
                string[] Valores = datos[1].Split('|');
                string codigoEdicion = "";

                if (datos.Length >= 4)
                {
                    codigoEdicion = datos[2].ToString();
                    TipoOperacion = datos[3].ToString();
                }
                else
                {
                    TipoOperacion = datos[2].ToString();
                }



                for (int i = 0; i < IdsPresentaciones.Length - 1; i++)
                {
                    string CodigoPresentacion = IdsPresentaciones[i].ToString();


                    if (CodigoPresentacion != "")
                    {

                        Presentacion presentacion = (from P in Contexto.Presentacions
                                                     where P.Codigo == CodigoPresentacion
                                                     select P).First<Presentacion>();


                        string valor = Valores[i].ToString();

                        int YaPidio = (from P in Session["detPedido"] as List<DetallePedido>
                                       where P.CodigoCompleto.Trim() == CodigoPresentacion.Trim()
                                       select P).Count();


                        if (valor != "" && int.Parse(valor) > 0)
                        {
                            if (YaPidio == 0)
                            {
                                DetallePedido detPedido = new DetallePedido();
                                detPedido.Cantidad = int.Parse(valor);
                                detPedido.Producto = presentacion.objProducto.IdProducto;
                                detPedido.ProductoDesc = presentacion.objProducto.Descripcion;
                                detPedido.Presentacion = presentacion.IdPresentacion;
                                detPedido.PresentacionDesc = presentacion.Descripcion;
                                detPedido.ValorUnitario = presentacion.Precio.Value;
                                detPedido.ValorTotal = detPedido.ValorUnitario * detPedido.Cantidad;
                                detPedido.CodigoCompleto = presentacion.Codigo;


                                if (presentacion.objProducto.objPadre.EsUltimoNivel.Value)
                                {
                                    detPedido.DescripcionCompleta = detPedido.ProductoDesc + " x " + detPedido.PresentacionDesc;
                                    detPedido.IdPadre = presentacion.objProducto.objPadre.IdProducto;
                                    detPedido.DescPadre = Helper.ObtenerDescripcionCompletaProducto(presentacion.objProducto.objPadre);
                                }
                                else
                                {
                                    detPedido.DescripcionCompleta = presentacion.objProducto.DescripcionCompleta + detPedido.PresentacionDesc;
                                    detPedido.IdPadre = presentacion.objProducto.objPadre.objPadre.IdProducto;
                                    detPedido.DescPadre = Helper.ObtenerDescripcionCompletaProducto(presentacion.objProducto.objPadre.objPadre);
                                }

                                detPedido.DescripcionCompleta = detPedido.ProductoDesc + " x " + detPedido.PresentacionDesc;
                                (Session["detPedido"] as List<DetallePedido>).Add(detPedido);
                            }
                            else
                            {
                                DetallePedido pedidoExistente = (from P in Session["detPedido"] as List<DetallePedido>
                                                                 where P.CodigoCompleto.Trim() == CodigoPresentacion.Trim()
                                                                 select P).First<DetallePedido>();

                                if (TipoOperacion == "N")
                                    pedidoExistente.Cantidad += int.Parse(valor);
                                else
                                    pedidoExistente.Cantidad = int.Parse(valor);

                                pedidoExistente.ValorTotal = pedidoExistente.Cantidad * pedidoExistente.ValorUnitario;

                            }


                        }
                        else
                        {
                            if (YaPidio > 0 && codigoEdicion == CodigoPresentacion.Trim())
                            {
                                DetallePedido CurrentePedido = (from P in Session["detPedido"] as List<DetallePedido>
                                                                where P.CodigoCompleto.Trim() == CodigoPresentacion.Trim()
                                                                select P).First<DetallePedido>();

                                (Session["detPedido"] as List<DetallePedido>).Remove(CurrentePedido);

                                TotalizadorPedidos_LineaPedidoEliminada();
                            }
                        }
                    }
                    else
                        break;

                }

                ActualizarTotalesGenerales();

            }
        }
    }

    protected void btnPedido_RealizarPedido(object sender, EventArgs e)
    {
        CalcularPromociones();
        ActualizarTotalesGenerales();
        if (!TotalizadorPromos1.HayPromosIncompletas)
        {
            string id = "";
            if (Request.QueryString["IdPedido"] != null)
                id = Request.QueryString["IdPedido"].ToString();

            GuardarPedido(id, false, false);
        }
    }

    protected void btnPedidoTemporal_RealizarPedido(object sender, EventArgs e)
    {
        CalcularPromociones();
        ActualizarTotalesGenerales();

        if (!TotalizadorPromos1.HayPromosIncompletas)
        {
            string id = "";
            if (Request.QueryString["IdPedido"] != null)
                id = Request.QueryString["IdPedido"].ToString();

            GuardarPedido(id, false, true);
        }
    }

    protected void RadTreeProductos_NodeDataBound(object sender, RadTreeNodeEventArgs e)
    {
        DataRowView row = (DataRowView)e.Node.DataItem;
        e.Node.Attributes.Add("Padre", row["IdProducto"].ToString());

        List<Producto> Hijos = (from P in _productos
                                where P.Padre == long.Parse(row["IdProducto"].ToString())
                                select P).First<Producto>().ColHijos.ToList<Producto>();


        if (Hijos.Count == 0)
        {
            e.Node.Attributes.Add("Cargar", "true");
            e.Node.Attributes.Add("RutaImagen", row["Image"].ToString());

            if (e.Node.Text.Trim() == "Ninguna")
                e.Node.Visible = false;
        }
        else
        {
            e.Node.Attributes.Add("Cargar", "false");
            e.Node.Attributes.Add("RutaImagen", row["Image"].ToString());

        }


        if (row["Descripcion"].ToString() == "Incorporaciones" && row["Nivel"].ToString() == "2")
        {
            e.Node.Attributes.Add("NodoIncorporaciones", "true");
        }


    }

    protected void ucGrillaDirecciones_DireccionSeleccionada(long Id)
    {

        Direccione dirSeleccionada = (from D in Contexto.Direcciones
                                      where D.IdDireccion == Id
                                      select D).First<Direccione>();


        lblCalle.Text = dirSeleccionada.Calle.ToLower();
        lblDireccionEntrega.Text = dirSeleccionada.Provincia.ToLower() + " - " + dirSeleccionada.Localidad.ToLower();
        lblIdDireccionEntrega.Text = dirSeleccionada.IdDireccion.ToString();


        ConfTransporte confT = (from C in Contexto.ConfTransportes
                                where C.Provincia.ToLower() == dirSeleccionada.Provincia.ToLower() &&
                           C.Localidad.ToLower() == dirSeleccionada.Localidad.ToLower() &&
                           C.FormaDePago == cboFormaPago.SelectedItem.Text
                                select C).FirstOrDefault<ConfTransporte>();

        if (confT != null)
        {
            lblTransporte.Text = confT.Transporte.ToLower();
            lblTransporteHidden.Value = confT.Transporte.ToLower();
            lblTransporteValorHidden.Value = confT.objProducto.ColPresentaciones[0].Precio.ToString();

            Parametro ParamProvincia = (from D in Contexto.Parametros
                                        where D.Tipo.ToLower() == dirSeleccionada.Provincia.ToLower()
                                        select D).FirstOrDefault();

            if (ParamProvincia != null)
            {
                lblProvinciaPorcentajeDescuentoHidden.Value = ParamProvincia.Valor.Replace(".", ",");
            }
        }
        else
        {
            lblTransporte.Text = "Sin Transporte";
            lblTransporteHidden.Value = "";
            lblTransporteValorHidden.Value = "0";
            lblProvinciaPorcentajeDescuentoHidden.Value = "0";
            lblDescuentoProvincia.Text = "$ 0";
        }


        /// Generación Jerarquia
        Cliente CurrentCliente = (Cliente)Session["Cliente"];
        if (CurrentCliente.CodTipoCliente == TipoClientesConstantes.CONSULTOR)
        {
            Cliente clienteSuperior = (from C in Contexto.Clientes
                                       where C.CodTipoCliente != TipoClientesConstantes.CONSULTOR
                                          && C.CodClasif1 == CurrentCliente.CodClasif1
                                       select C).First();

            if (clienteSuperior.CodTipoCliente == TipoClientesConstantes.SUBCONGESTION ||
                clienteSuperior.CodTipoCliente == TipoClientesConstantes.SUBSINGESTION)
            {
                lblSubCoor.Text = clienteSuperior.Nombre.ToLower();

                clienteSuperior = (from C in Contexto.Clientes
                                   where C.CodTipoCliente != TipoClientesConstantes.CONSULTOR
                                   && C.CodTipoCliente != TipoClientesConstantes.SUBCONGESTION
                                   && C.TipoCliente != TipoClientesConstantes.SUBSINGESTIONDESC
                                   && C.CodVendedor == clienteSuperior.CodVendedor
                                   select C).First();

                if (clienteSuperior.Nombre.ToLower() == lblSubCoor.Text)
                {
                    lblSubCoor.Text = "Sin Asignar";
                }

                lblResponsable.Text = clienteSuperior.Nombre.ToLower();

            }
            else
            {
                lblSubCoor.Text = "Sin Asignar";
                lblResponsable.Text = clienteSuperior.Nombre.ToLower();

            }

        }
        else
        {

            lblSubCoor.Text = "Sin Asignar";
            lblResponsable.Text = CurrentCliente.Nombre.ToLower();

        }

        lblSubCoor.Text = CurrentCliente.Clasif1;
        lblSaldoCta.Text = CurrentCliente.SaldoCtaCte.ToString();

        if (CurrentCliente.UltimaActualizacion.HasValue)
            lblUltimaAct.Text = CurrentCliente.UltimaActualizacion.ToString();
        else
            lblUltimaAct.Text = "";

        upEncImp.Update();

    }

    protected void cboConsultores_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (cboConsultores.SelectedValue != "0")
        {
            var cliente = (from C in Contexto.Clientes
                           where C.IdCliente == long.Parse(cboConsultores.SelectedValue)
                           select C).Single<Cliente>();

            Session["Cliente"] = cliente;

            CargarEncabezado(cliente, 0);

            //CalcularPromociones();

            UCTotalizadorNivel.IniciarControl();


        }
        else
        {

            lblResponsable.Text = "";
            lblDireccionEntrega.Text = "";
            lblTransporte.Text = "";
            lblTransporteHidden.Value = "";
            lblTransporteValorHidden.Value = "0";
            lblProvinciaPorcentajeDescuentoHidden.Value = "0";
            lblFechaImp.Text = "";
            lblNroNotaImp.Text = "";
        }

        ScriptManager.RegisterStartupScript(upEncImp, typeof(UpdatePanel), "ocultar", "OcultarLoading();", true);
        upEncImp.Update();
        upNivel.Update();

    }

    protected void cboFormaPago_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (cboFormaPago.SelectedItem.Value != "0" && lblIdDireccionEntrega.Text != "")
        {

            Direccione dirEntrega = (from D in Contexto.Direcciones
                                     where D.IdDireccion == long.Parse(lblIdDireccionEntrega.Text)
                                     select D).FirstOrDefault<Direccione>();


            if (dirEntrega != null)
            {
                ConfTransporte confT = (from C in Contexto.ConfTransportes
                                        where C.Provincia.ToLower() == dirEntrega.Provincia.ToLower() &&
                                        C.Localidad.ToLower() == dirEntrega.Localidad.ToLower() &&
                                        C.FormaDePago == cboFormaPago.SelectedItem.Text
                                        select C).FirstOrDefault<ConfTransporte>();


                if (confT != null)
                {
                    lblTransporte.Text = confT.Transporte.ToLower();
                    lblTransporteHidden.Value = confT.Transporte.ToLower();
                    lblTransporteValorHidden.Value = confT.objProducto.ColPresentaciones[0].Precio.ToString();

                    Parametro ParamProvincia = (from D in Contexto.Parametros
                                                where D.Tipo.ToLower() == dirEntrega.Provincia.ToLower()
                                                select D).FirstOrDefault();

                    if (ParamProvincia != null)
                    {
                        lblProvinciaPorcentajeDescuentoHidden.Value = ParamProvincia.Valor.Replace(".", ",");

                    }
                }
                else
                {
                    lblTransporte.Text = "SIN TRANSPORTE";
                    lblTransporteHidden.Value = "";
                    lblTransporteValorHidden.Value = "0";
                    lblProvinciaPorcentajeDescuentoHidden.Value = "0";
                    lblDescuentoProvincia.Text = "$ 0";
                }

            }
            else
            {

                lblTransporte.Text = "SIN TRANSPORTE";
                lblTransporteHidden.Value = "";
                lblTransporteValorHidden.Value = "0";
                lblProvinciaPorcentajeDescuentoHidden.Value = "0";
                lblDescuentoProvincia.Text = "$ 0";
            }

            if (cboFormaPago.SelectedItem.Text == "Pago Fácil")
                lnkPagoFacil.Visible = true;
            else
                lnkPagoFacil.Visible = false;

            if (cboFormaPago.SelectedItem.Text == "Pago Mis Cuentas")
                lnkPagoMisCuentas.Visible = true;
            else
                lnkPagoMisCuentas.Visible = false;



            upDirec.Update();
            upTransporte.Update();
        }

        ActualizarTotalesGenerales();
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("Inicio.aspx");
    }

    protected void btnSolicitar_Click(object sender, EventArgs e)
    {
        object aa = resultshere.FindControl("tblCargaPedido");
    }

    #endregion

    #region Metodos

    private void GuardarPedido(string idPedido, bool FaltaSaldo, bool EsTemporal)
    {
        DetallePedido newDetalle = null;

        try
        {
            string LimiteCompra = (from P in Session["ParametrosSistema"] as List<Parametro>
                                   where P.IdParametro == (int)TiposDeParametros.LimiteCompra
                                   select P.Valor).Single();

            string LimiteContrReembolso = (from P in Session["ParametrosSistema"] as List<Parametro>
                                           where P.IdParametro == (int)TiposDeParametros.LimiteContraReembolso
                                           select P.Valor).Single();

            if (!EsTemporal)
            {
                if (!cboConsultores.Text.Contains("bolsos"))
                {
                    decimal TotalComprado = (from P in (Session["detPedido"] as List<DetallePedido>)
                                             where P.CodigoCompleto.Substring(0, 1) == "1"
                                             select P.ValorTotal.Value).Sum();


                    if (TotalComprado < int.Parse(LimiteCompra))
                    {

                        ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "MinimoReq", "AlertaMinimoRequerido(" + LimiteCompra + ");", true);
                        Contexto.Dispose();
                        return;
                    }
                }
            }


            #region Generacion de la Cabecera del Pedido
            CabeceraPedido cabecera = null;

            if (idPedido != "")
            {

                cabecera = (from C in Contexto.CabeceraPedidos
                            where C.IdCabeceraPedido == long.Parse(Request.QueryString["IdPedido"].ToString())
                            select C).SingleOrDefault();

                cabecera.DireccionEntrega = long.Parse(lblIdDireccionEntrega.Text);
                cabecera.FormaPago = long.Parse(cboFormaPago.SelectedValue);
                cabecera.Retira = txtObservacion.Text;
                cabecera.FechaPedido = DateTime.Now;
                cabecera.PorcentajeDescuentoProvincia = Convert.ToDecimal(lblProvinciaPorcentajeDescuentoHidden.Value);
                cabecera.DescuentoProvincia = Convert.ToDecimal(lblDescuentoProvincia.Text.Replace("$", ""));
            }
            else
            {
                cabecera = new CabeceraPedido();
                cabecera.Cliente = long.Parse(cboConsultores.SelectedValue);
                cabecera.ClienteSolicitante = long.Parse(Session["idUsuario"].ToString());
                cabecera.DireccionEntrega = long.Parse(lblIdDireccionEntrega.Text);
                cabecera.FechaPedido = DateTime.Now;
                cabecera.FormaPago = long.Parse(cboFormaPago.SelectedValue);
                cabecera.MontoTotal = 0;
                cabecera.Retira = txtObservacion.Text;
                cabecera.Nro = ObtenerSeguienteNro(0, Contexto);
                cabecera.TipoPedido = "NP";
                cabecera.Impreso = false;
                cabecera.NroImpresion = 0;
                cabecera.PorcentajeDescuentoProvincia = Convert.ToDecimal(lblProvinciaPorcentajeDescuentoHidden.Value);
                cabecera.DescuentoProvincia = Convert.ToDecimal(lblDescuentoProvincia.Text.Replace("$", ""));
            }
            #endregion

            #region Generacion de los Detalle

            #region Eliminacion Detalle Temporales

            if (idPedido != "")
            {
                /// Elimino la promociones guardadas y los producto de regalos generados
                Contexto.DetallePedidos.DeleteAllOnSubmit(cabecera.DetallePedidos.Where(w => w.objPromocionOrigen != null));
                Contexto.DetallePedidos.DeleteAllOnSubmit(cabecera.DetallePedidos.Where(w => w.objProducto.Tipo == 'P'));

                /// Elimino las bolsas de papel
                DetallePedido detalleBolsas = (from d in cabecera.DetallePedidos
                                               where d.CodigoCompleto == "2520000018001"
                                               select d).FirstOrDefault();
                if (detalleBolsas != null)
                    Contexto.DetallePedidos.DeleteOnSubmit(detalleBolsas);

                /// Elimino los gatillos
                DetallePedido detalleGatillos = (from d in cabecera.DetallePedidos
                                                 where d.CodigoCompleto == "2520000032073"
                                                 select d).FirstOrDefault();
                if (detalleGatillos != null)
                    Contexto.DetallePedidos.DeleteOnSubmit(detalleGatillos);

                /// Elimino el gasto de Envio
                DetallePedido detalleGastoEnvio = (from d in cabecera.DetallePedidos
                                                   where d.objProducto.Tipo == 'G'
                                                   select d).FirstOrDefault();

                if (detalleGastoEnvio != null)
                    Contexto.DetallePedidos.DeleteOnSubmit(detalleGastoEnvio);


                Contexto.SubmitChanges();
            }


            #endregion


            #region Detalles de Pedido
            foreach (DetallePedido det in (Session["detPedido"] as List<DetallePedido>))
            {
                if (det.IdDetallePedido == 0)
                {
                    newDetalle = new DetallePedido();
                    newDetalle.Cantidad = long.Parse(det.Cantidad.ToString());
                    newDetalle.CodigoCompleto = det.CodigoCompleto;
                    newDetalle.Presentacion = det.Presentacion;
                    newDetalle.Producto = det.Producto.Value;
                    newDetalle.ValorUnitario = det.ValorUnitario;
                    newDetalle.ValorTotal = newDetalle.ValorUnitario * newDetalle.Cantidad;
                    cabecera.DetallePedidos.Add(newDetalle);
                }
                else
                {

                    DetallePedido detGuardado = (from D in cabecera.DetallePedidos
                                                 where D.IdDetallePedido == det.IdDetallePedido
                                                 select D).Single<DetallePedido>();


                    detGuardado.Cantidad = long.Parse(det.Cantidad.ToString());
                    detGuardado.ValorUnitario = det.ValorUnitario;
                    detGuardado.ValorTotal = detGuardado.ValorUnitario * detGuardado.Cantidad;

                }

            }
            #endregion

            //if (!EsTemporal)
            //{
            #region Generacion de las promociones del Pedido
            if ((Session["PromosGeneradas"] as List<DetallePedido>) != null)
            {
                foreach (DetallePedido det in (Session["PromosGeneradas"] as List<DetallePedido>))
                {
                    Producto ProductoPromo = (from P in Contexto.Productos
                                              where P.IdProducto == det.Producto
                                              select P).First<Producto>();


                    DetallePedido newDetallePromo = new DetallePedido();
                    newDetallePromo.Cantidad = long.Parse(det.Cantidad.ToString());
                    newDetallePromo.CodigoCompleto = det.CodigoCompleto;
                    newDetallePromo.Presentacion = det.Presentacion;
                    newDetallePromo.Producto = det.Producto;
                    newDetallePromo.ValorUnitario = ProductoPromo.Precio * -1;
                    newDetallePromo.ValorTotal = newDetallePromo.ValorUnitario * newDetallePromo.Cantidad;
                    cabecera.DetallePedidos.Add(newDetallePromo);

                    foreach (DetalleRegalos detRegalo in det.ColRegalos)
                    {

                        if (detRegalo.TipoRegalo == "Producto" && detRegalo.IdPresentacionRegaloSeleccionado > 0)
                        {

                            Presentacion PresentacionRegalo = (from P in Contexto.Presentacions
                                                               where P.IdPresentacion == detRegalo.IdPresentacionRegaloSeleccionado
                                                               select P).First<Presentacion>();


                            newDetalle = new DetallePedido();
                            newDetalle.Cantidad = long.Parse(det.Cantidad.ToString());
                            newDetalle.CodigoCompleto = PresentacionRegalo.Codigo;
                            newDetalle.Presentacion = PresentacionRegalo.IdPresentacion;
                            newDetalle.Producto = PresentacionRegalo.objProducto.IdProducto;
                            newDetalle.objPromocionOrigen = newDetallePromo;

                            if (PresentacionRegalo.objProducto.Tipo.ToString() == "A")
                            {
                                newDetalle.ValorUnitario = PresentacionRegalo.Precio;
                            }
                            else
                            {
                                newDetalle.ValorUnitario = PresentacionRegalo.objProducto.Precio;
                            }


                            newDetalle.ValorTotal = newDetalle.ValorUnitario * newDetalle.Cantidad;
                            cabecera.DetallePedidos.Add(newDetalle);

                        }
                    }

                    det.ColRegalos = new System.Data.Linq.EntitySet<DetalleRegalos>();

                }
            }
            #endregion


            #region Generacion detalle Bolsas de Papel de Regalo

            string[] CodigosLlevanBolsas = (from N in Contexto.Presentacions
                                            where N.objProducto.Tipo == 'A'
                                            && !(N.objProducto.objPadre.objPadre.Codigo == "01" && N.Descripcion.Contains("120"))
                                            select N.Codigo).ToArray<string>();


            long CantidadArticulosBolsa = Convert.ToInt64(((from N in cabecera.DetallePedidos
                                                            where CodigosLlevanBolsas.Contains(N.CodigoCompleto)
                                                            && N.CodigoCompleto.Substring(0, 1) == "1"
                                                            select N.Cantidad.Value).Sum() * 0.5));


            if (CantidadArticulosBolsa > 0)
            {

                Presentacion pre = (from P in Contexto.Presentacions
                                    where P.Codigo == "2520000018001"
                                    select P).SingleOrDefault();

                newDetalle = new DetallePedido();
                newDetalle.Cantidad = CantidadArticulosBolsa;
                newDetalle.CodigoCompleto = pre.Codigo;
                newDetalle.Presentacion = pre.IdPresentacion;
                newDetalle.Producto = pre.objProducto.IdProducto;
                newDetalle.ValorUnitario = pre.Precio;
                newDetalle.ValorTotal = newDetalle.ValorUnitario * newDetalle.Cantidad;

                cabecera.DetallePedidos.Add(newDetalle);
            }

            #endregion


            #region Generacion detalle Gatillos Para Embases de 1000 ml

            string[] CodigosLlevanGatillo = (from N in Contexto.Presentacions
                                             where N.objProducto.Tipo == 'A'
                                             && N.Descripcion.Contains("1000 ml")
                                             select N.Codigo).ToArray<string>();


            long CantidadArticulosGatillos = Convert.ToInt64(((from N in cabecera.DetallePedidos
                                                               where CodigosLlevanGatillo.Contains(N.CodigoCompleto)
                                                            && N.CodigoCompleto.Substring(0, 1) == "1"
                                                               select N.Cantidad.Value).Sum() * 1));


            if (CantidadArticulosGatillos > 0)
            {

                Presentacion pre = (from P in Contexto.Presentacions
                                    where P.Codigo == "2520000032073"
                                    select P).SingleOrDefault();

                newDetalle = new DetallePedido();
                newDetalle.Cantidad = CantidadArticulosGatillos;
                newDetalle.CodigoCompleto = pre.Codigo;
                newDetalle.Presentacion = pre.IdPresentacion;
                newDetalle.Producto = pre.objProducto.IdProducto;
                newDetalle.ValorUnitario = pre.Precio;
                newDetalle.ValorTotal = newDetalle.ValorUnitario * newDetalle.Cantidad;

                cabecera.DetallePedidos.Add(newDetalle);
            }

            #endregion
            //}
            #endregion

            decimal valorTansporte = 0;
            //if (!EsTemporal)
            //{
            #region Generacion del Gasto de Envio del Pedido

            Direccione dirEntrega = (from D in Contexto.Direcciones
                                     where D.IdDireccion == long.Parse(lblIdDireccionEntrega.Text)
                                     select D).First<Direccione>();

            ConfTransporte confT = (from C in Contexto.ConfTransportes
                                    where C.Provincia == dirEntrega.Provincia &&
                                    C.Localidad == dirEntrega.Localidad &&
                                    C.FormaDePago == cboFormaPago.SelectedItem.Text
                                    select C).First<ConfTransporte>();


            newDetalle = new DetallePedido();
            newDetalle.Cantidad = 1;
            newDetalle.CodigoCompleto = confT.objProducto.ColPresentaciones[0].Codigo;
            newDetalle.Presentacion = confT.objProducto.ColPresentaciones[0].IdPresentacion;
            newDetalle.Producto = confT.objProducto.IdProducto;
            newDetalle.ValorUnitario = confT.objProducto.ColPresentaciones[0].Precio;
            newDetalle.ValorTotal = newDetalle.ValorUnitario * newDetalle.Cantidad;
            cabecera.DetallePedidos.Add(newDetalle);

            valorTansporte = newDetalle.ValorTotal.Value;


            #endregion
            //}



            ///Calculo y Actualizo el total de la operacion
            decimal Total = (from P in cabecera.DetallePedidos
                             select P.ValorTotal.Value).Sum();

            cabecera.MontoTotal = Total - cabecera.DescuentoProvincia.Value;

            if (!FaltaSaldo && !EsTemporal)
            {
                decimal SaldoActual = -1 * (Session["Consultor"] as Cliente).SaldoCtaCte.Value;
                switch (cboFormaPago.Text)
                {
                    case "Pago Fácil":
                        {
                            if (Total > SaldoActual)
                            {
                                decimal TotalSinTransporte = Total - valorTansporte;
                                ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "SaldoReq", "AlertaSaldoInsuficiente('El monto del pedido (Productos: $" + TotalSinTransporte.ToString() + " + Transporte: $" + valorTansporte.ToString() + ") supera el saldo disponible que posee ($" + SaldoActual.ToString() + "), el mismo no puede ser realizado hasta que tenga saldo suficiente. Desea guardar el pedido temporalmente para realizarlo en otro momento?');", true);
                                Contexto.Dispose();
                                return;
                            }
                            else
                                break;


                        }
                    case "Contra Reembolso":
                        {
                            if ((Total - SaldoActual) > decimal.Parse(LimiteContrReembolso))
                            {
                                ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "SaldoReq", "AlertaSaldoInsuficiente('El monto del pedido supera el límite en contra reemboldo ($ " + LimiteContrReembolso + "), el mismo no puede ser realizado. Desea guardar el pedido temporalmente para realizarlo en otro momento?');", true);
                                Contexto.Dispose();
                                return;
                            }
                            else
                                break;

                        }
                }
            }

            /// Grabo el Pedido realizado
            if (idPedido == "")
            {
                Contexto.CabeceraPedidos.InsertOnSubmit(cabecera);

                if (EsTemporal)
                {
                    cabecera.UltimaModificacion = DateTime.Now;
                    cabecera.EsTemporal = true;
                    cabecera.HuboFaltaSaldo = FaltaSaldo;
                    Contexto.SubmitChanges();
                }
                else
                {
                    Contexto.SubmitChanges();
                    ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "GrabacionPedido", "AlertaGrabacion('" + cabecera.IdCabeceraPedido.ToString() + "');", true);
                }

                LimpiarPedido();
            }
            else
            {
                if (EsTemporal)
                {
                    ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "GrabacionPedidoTemp", "javascript:window.close();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "GrabacionPedido", "GrabacionFinalPendiente('" + cabecera.IdCabeceraPedido.ToString() + "');", true);
                }

                if (EsTemporal)
                {
                    cabecera.UltimaModificacion = DateTime.Now;
                    cabecera.EsTemporal = true;
                    cabecera.HuboFaltaSaldo = FaltaSaldo;
                }
                else
                {
                    cabecera.UltimaModificacion = null;
                    cabecera.EsTemporal = null;
                    cabecera.HuboFaltaSaldo = null;
                }

                Contexto.SubmitChanges();
            }




        }
        catch
        {
            ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "Transporte", "AlertaTransporte();", true);
        }
    }

    private void CargarFormaDePago(Cliente CurrrentCliente)
    {

        cboFormaPago.Items.Clear();
        cboFormaPago.DataTextField = "Descripcion";
        cboFormaPago.DataValueField = "IdFormaPago";

        if (CurrrentCliente != null)
        {
            if (CurrrentCliente.Cod_CondVta == "SIN")
            {

                cboFormaPago.DataSource = from Fp in Contexto.FormaDePagos
                                          where Fp.Cliente == 2
                                          select Fp;
            }
            else
            {
                cboFormaPago.DataSource = from Fp in Contexto.FormaDePagos
                                          where Fp.Cliente == 2
                                          && Fp.Codigo != "SIN"
                                          select Fp;
            }

        }
        else
        {
            cboFormaPago.DataSource = from Fp in Contexto.FormaDePagos
                                      where Fp.Cliente == 2
                                      select Fp;
        }


        cboFormaPago.DataBind();
        cboFormaPago.SelectedValue = "0";
        upFormaDePago.Update();



    }

    private void CalcularTotalPedido()
    {
        decimal Total = 0;
        decimal MontoTotal = 0;
        decimal MontoTotalPromocionesDescuento = 0;
        decimal DescuentoProvincia = 0;
        decimal ValorFlete = Convert.ToDecimal(lblTransporteValorHidden.Value);
        decimal PorcentajeDescuentoProvincia = Convert.ToDecimal(lblProvinciaPorcentajeDescuentoHidden.Value);

        Total = (from P in (Session["detPedido"] as List<DetallePedido>)
                 select P.Cantidad.Value).Sum();

        MontoTotal = (from P in (Session["detPedido"] as List<DetallePedido>)
                      select P.ValorTotal.Value).Sum();

        if (Session["PromosGeneradas"] != null && (Session["PromosGeneradas"] as List<DetallePedido>).Count > 0)
        {
            MontoTotalPromocionesDescuento = (from P in (Session["PromosGeneradas"] as List<DetallePedido>)
                                              where P.ColRegalos != null && P.ColRegalos.Any(w => w.TipoRegalo == "Descuento")
                                              && P.ValorTotal.HasValue
                                              select P.ValorTotal.Value).Sum();
        }

        DescuentoProvincia = Math.Round(((MontoTotal + ValorFlete) * PorcentajeDescuentoProvincia) / 100, 2);

        /// Actualizo tooltip del detalle del monto del pedido.
        lblMontoProductos.Text = "$ " + MontoTotal.ToString();
        lblCostoFlete.Text = "$ " + ValorFlete.ToString();
        lblDescuentos.Text = "$ " + MontoTotalPromocionesDescuento.ToString();
        lblDescuentoProvincia.Text = "$ " + DescuentoProvincia.ToString();

        MontoTotal += ValorFlete - MontoTotalPromocionesDescuento - DescuentoProvincia;
        txtTotalGeneral.Text = Total.ToString();
        txtMontoGeneral.Text = "$ " + MontoTotal.ToString();
        lblMontoActual.Text = "$ " + MontoTotal.ToString();



        UpToolTipMontoPedido.Update();
    }

    private void ActualizarTotalesGenerales()
    {

        if ((Session["detPedido"] as List<DetallePedido>).Count > 0)
        {
            /// El primer llamado es para actualizar el total del 
            /// pedido con la modificación del detalle que se ha realizado
            /// pedido, actualización o eliminación de productos
            CalcularTotalPedido();
            /// Calculo las promociones para actualizar la promociones
            //CalcularPromociones();
            /// vuelvo a actualizar el total del pedido
            /// ya que las promociones afectan el total.
            //CalcularTotalPedido();
        }
        else
        {
            txtTotalGeneral.Text = "0";
            txtMontoGeneral.Text = "$ 0";
            lblMontoActual.Text = "$ 0";
        }

        //UcTotalizadorPedidos.InitControl(0);
        upResumen.Update();
        upTotalizarCabecera.Update();
        upMontoActual.Update();
    }

    private void CargarDireccionesEntrega(Cliente CurrentCliente)
    {
        #region  Actualizo las direcciones de entrega

        lblFechaImp.Text = DateTime.Now.ToShortDateString();
        lblNroNotaImp.Text = ObtenerSeguienteNro(0, Contexto);


        Direccione direccionIncial = (from D in Contexto.Direcciones
                                      where D.CodigoExterno == CurrentCliente.CodigoExterno
                                      select D).FirstOrDefault<Direccione>();


        if (direccionIncial != null)
        {
            lblIdDireccionEntrega.Text = direccionIncial.IdDireccion.ToString();

            ucGrillaDirecciones.InitControl(CurrentCliente.CodigoExterno);
            upDirec.Update();
        }
        else
        {
            ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "SinDir", "AlertaSinDireccion();", true);
            upSolicitudPedido.Update();
            return;
        }
        #endregion

        #region  Busco si hay descuento por provincia
        Parametro ParamProvincia = (from D in Contexto.Parametros
                                    where D.Tipo.ToLower() == direccionIncial.Provincia.ToLower()
                                    select D).FirstOrDefault();

        if (ParamProvincia != null)
        {
            lblProvinciaPorcentajeDescuentoHidden.Value = ParamProvincia.Valor.Replace(".", ",");
        }
        else
        {
            lblProvinciaPorcentajeDescuentoHidden.Value = "0";
        }

        #endregion

        #region  Busco el costo del flete segun los datos del transportistas.
        ConfTransporte confT = (from C in Contexto.ConfTransportes
                                where C.Provincia.ToLower() == direccionIncial.Provincia.ToLower() &&
                                C.Localidad.ToLower() == direccionIncial.Localidad.ToLower() &&
                                C.FormaDePago == cboFormaPago.SelectedItem.Text
                                select C).FirstOrDefault<ConfTransporte>();

        if (confT != null)
        {

            lblTransporte.Text = confT.Transporte.ToLower();
            lblTransporteHidden.Value = confT.Transporte.ToLower();
            lblTransporteValorHidden.Value = confT.objProducto.ColPresentaciones[0].Precio.ToString();
            upTransporte.Update();
        }
        #endregion

        #region Actualización ToolTip

        lblToolTipNombre.Text = CurrentCliente.Nombre.ToLower();
        lblToolTipDNI.Text = CurrentCliente.Dni;
        lblToolTipTel.Text = CurrentCliente.Telefono;
        lblToolTipEmail.Text = CurrentCliente.Email;

        if (CurrentCliente.TipoConsultor != null)
            lblToolTipTipoConsultor.Text = CurrentCliente.TipoConsultor.ToLower();

        lblToolTipSitImpositiva.Text = CurrentCliente.Desc_SitIVA.ToLower();
        upFichaTecnica.Update();
        #endregion

    }

    private void ActualizarProductosEspeciales(Cliente CurrentCliente)
    {

        IncorporacionesHistorica IncorporacionHistorica = (from I in Contexto.IncorporacionesHistoricas
                                                           where I.CodCliente == CurrentCliente.CodigoExterno
                                                           select I).SingleOrDefault();

        #region Producto Particular Cartuchera

        /// Cartuchera Completa con Ficha Técnica : 2858
        if (CurrentCliente.PoseeCartuchera.Value)
            HiddenPoseeCartuchera.Value = "false";
        else
        {

            if (IncorporacionHistorica == null)
                HiddenPoseeCartuchera.Value = "true";
            else if (IncorporacionHistorica.PoseeCartuchera.Value)
                HiddenPoseeCartuchera.Value = "false";
            else
                HiddenPoseeCartuchera.Value = "true";
        }

        #endregion


        #region Nodo Incorporaciones del árbol
        RadTreeNode NodoInc = RadTreeProductos.FindNodeByAttribute("NodoIncorporaciones", "true");

        if (CurrentCliente.PoseeIncorporacion.Value)
            NodoInc.Visible = false;
        else
        {
            if (IncorporacionHistorica == null)
                NodoInc.Visible = true;
            else
                NodoInc.Visible = false;
        }

        upTreeProductos.Update();

        #endregion

    }

    private void CargarEncabezado(Cliente CurrentCliente, long IdPedido)
    {
        Direccione dirPrincipal = null;
        List<Direccione> direcciones = (from D in Contexto.Direcciones
                                        where D.CodigoExterno == CurrentCliente.CodigoExterno
                                        select D).ToList<Direccione>();


        if (direcciones.Count > 0)
        {
            dirPrincipal = direcciones.First();
        }

        if (dirPrincipal != null)
        {

            lblDireccionEntrega.Text = dirPrincipal.Provincia.ToLower() + " - " + dirPrincipal.Localidad.ToLower();
            lblCalle.Text = dirPrincipal.Calle.ToLower();



            try
            {

                ConfTransporte confT = (from C in Contexto.ConfTransportes
                                        where C.Provincia.ToLower() == dirPrincipal.Provincia.ToLower() &&
                                        C.Localidad.ToLower() == dirPrincipal.Localidad.ToLower() &&
                                        C.FormaDePago == cboFormaPago.SelectedItem.Text
                                        select C).First<ConfTransporte>();



                lblTransporte.Text = confT.Transporte.ToLower();
                lblTransporteHidden.Value = confT.Transporte.ToLower();
                lblTransporteValorHidden.Value = confT.objProducto.ColPresentaciones[0].Precio.ToString();


                Parametro ParamProvincia = (from D in Contexto.Parametros
                                            where D.Tipo.ToLower() == dirPrincipal.Provincia.ToLower()
                                            select D).FirstOrDefault();

                if (ParamProvincia != null)
                {
                    lblProvinciaPorcentajeDescuentoHidden.Value = ParamProvincia.Valor.Replace(".", ",");

                }

            }
            catch
            {
                lblTransporte.Text = "Sin Transporte";
                lblTransporteHidden.Value = "";
                lblTransporteValorHidden.Value = "0";
                lblProvinciaPorcentajeDescuentoHidden.Value = "0";
                lblDescuentoProvincia.Text = "$ 0";

            }



            lblFechaImp.Text = DateTime.Now.ToShortDateString();
            lblNroNotaImp.Text = ObtenerSeguienteNro(IdPedido, Contexto);
            lblIdDireccionEntrega.Text = dirPrincipal.IdDireccion.ToString();


            if (CurrentCliente.CodTipoCliente == TipoClientesConstantes.CONSULTOR)
            {


                Cliente clienteSuperior = (from C in Contexto.Clientes
                                           where C.CodTipoCliente != TipoClientesConstantes.CONSULTOR
                                              && C.CodClasif1 == CurrentCliente.CodClasif1
                                           select C).First();

                if ((clienteSuperior.CodTipoCliente == TipoClientesConstantes.SUBCONGESTION || clienteSuperior.CodTipoCliente == TipoClientesConstantes.SUBSINGESTION)
                    && clienteSuperior.TipoCliente.ToUpper().Contains("SUB"))
                {
                    lblResponsable.Text = clienteSuperior.Vendedor.ToLower();

                }
                else
                {
                    lblResponsable.Text = clienteSuperior.Nombre.ToLower();
                }

            }
            else
            {
                lblResponsable.Text = CurrentCliente.Nombre.ToLower();
            }

            /// Muestro siempre el grupo al que pertenece el usuario. clasif1 == Grupo
            lblSubCoor.Text = CurrentCliente.Clasif1;

            lblToolTipNombre.Text = CurrentCliente.Nombre.ToLower();
            lblToolTipDNI.Text = CurrentCliente.Dni;
            lblToolTipTel.Text = CurrentCliente.Telefono;
            lblToolTipEmail.Text = CurrentCliente.Email;
            lblSaldoCta.Text = CurrentCliente.SaldoCtaCte.ToString();

            if (CurrentCliente.UltimaActualizacion.HasValue)
                lblUltimaAct.Text = CurrentCliente.UltimaActualizacion.ToString();
            else
                lblUltimaAct.Text = "";


            if (CurrentCliente.TipoConsultor != null)
                lblToolTipTipoConsultor.Text = CurrentCliente.TipoConsultor.ToLower();

            lblToolTipSitImpositiva.Text = CurrentCliente.Desc_SitIVA.ToLower();
            upFichaTecnica.Update();


            Session["Consultor"] = CurrentCliente;
            ucGrillaDirecciones.InitControl(CurrentCliente.CodigoExterno);
            upDirec.Update();
            upEncImp.Update();

        }
        else
        {
            Session["Consultor"] = CurrentCliente;
            ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "SinDir", "AlertaSinDireccion();", true);
            upSolicitudPedido.Update();
            return;
        }

        ActualizarProductosEspeciales(CurrentCliente);
    }

    private void LimpiarPedido()
    {

        lblResponsable.Text = "";
        lblSubCoor.Text = "";
        lblDireccionEntrega.Text = "";
        lblIdDireccionEntrega.Text = "";
        lblTransporte.Text = "";
        lblTransporteHidden.Value = "";
        lblTransporteValorHidden.Value = "0";
        lblProvinciaPorcentajeDescuentoHidden.Value = "0";

        txtObservacion.Text = "";
        lblFechaImp.Text = "";
        lblNroNotaImp.Text = "";
        txtMontoGeneral.Text = "0";
        lblMontoActual.Text = "0";
        txtTotalGeneral.Text = "0";

        //phTotalizadorPedido.Controls.Clear();
        TotalizadorPromos1.Clear();
        //UcTotalizadorPedidos.Clear();

        Session.Add("detPedido", new List<DetallePedido>());
        Session.Add("PromosGeneradas", new List<DetallePedido>());


        cboConsultores.SelectedIndex = 0;
        cboConsultores_SelectedIndexChanged(null, null);
        cboFormaPago.SelectedIndex = 2;


        upResumen.Update();
        upEncImp.Update();
        upPromos.Update();
        upTotalizarCabecera.Update();
        upMontoActual.Update();
        upCabeceraPagina.Update();
    }

    private static string ObtenerSeguienteNro(long IdPedido, Marzzan_InfolegacyDataContext Contexto)
    {


        try
        {
            if (IdPedido > 0)
            {
                return (from C in Contexto.CabeceraPedidos
                        where C.IdCabeceraPedido == IdPedido
                        select C.Nro.ToString()).SingleOrDefault();
            }
            else
            {
                var ultimoNro = (from D in Contexto.CabeceraPedidos
                                 where D.TipoPedido == "NP"
                                 select Convert.ToInt32(D.Nro)).Max<int>();
                return Convert.ToString(long.Parse(ultimoNro.ToString()) + 1);
            }

        }
        catch
        {
            return "1";
        }


    }

    private void CalcularPromociones()
    {
        if (Session["Cliente"] != null)
        {

            string Tiempo = "";
            //Tiempo += "Inicio Calculo Promos: " + string.Format("{0:mm:ss}", DateTime.Now) + " - ";

            Cliente CurrentClient = (Cliente)Session["Cliente"];


            Helpers.HelperPromocion helper = new Helpers.HelperPromocion();

            decimal MontoActual = decimal.Parse(txtMontoGeneral.Text.Replace("$", ""));

            long[] idProductosSolicitados = (from P in (Session["detPedido"] as List<DetallePedido>)
                                             select P.Producto.Value).ToArray<long>();


            List<Producto> promosValidas = (from P in Contexto.Productos
                                            where P.objConfPromocion != null
                                            && (DateTime.Now.Date >= P.objConfPromocion.FechaInicio.Date && DateTime.Now.Date <= P.objConfPromocion.FechaFinal.Date)
                                            && (P.objConfPromocion.ColTransportistas.Count == 0 || P.objConfPromocion.ColTransportistas.Where(t => t.Transporte.ToUpper() == lblTransporteHidden.Value.ToString().ToUpper()).Count() > 0)
                                            select P).Distinct<Producto>().ToList<Producto>();



            List<Producto> promosConRegalos = (from P in promosValidas
                                               join C in Contexto.Composicions on P.IdProducto equals C.ComponentePricipal
                                               where P.objConfPromocion != null
                                               && (DateTime.Now.Date >= P.objConfPromocion.FechaInicio.Date && DateTime.Now.Date <= P.objConfPromocion.FechaFinal.Date)
                                               && (idProductosSolicitados.Contains(C.objProductoHijo.IdProducto))
                                               && (P.ColComposiciones.Where(p => p.TipoComposicion == "O").Count() > 0)
                                                   /// Para la promociones de tipo INICIL
                                               && (P.objConfPromocion.TipoPromo == "INICIAL")
                                               && (P.objConfPromocion.UnaPorPedido.Value == false)
                                               && (P.objConfPromocion.MontoMinimo.Value == 0 || MontoActual >= P.objConfPromocion.MontoMinimo.Value)
                                               && (P.objConfPromocion.ColTransportistas.Count == 0 || P.objConfPromocion.ColTransportistas.Where(t => t.Transporte.ToUpper() == lblTransporteHidden.Value.ToString().ToUpper()).Count() > 0)
                                               select P).Distinct<Producto>().ToList<Producto>();

            promosConRegalos = promosConRegalos.OrderByDescending(c => c.CantidadElementoRequeridos).ToList<Producto>();


            List<Producto> promosVIPConRegalos = (from P in promosValidas
                                                  join C in Contexto.Composicions on P.IdProducto equals C.ComponentePricipal
                                                  where P.objConfPromocion != null
                                                  && (DateTime.Now.Date >= P.objConfPromocion.FechaInicio.Date && DateTime.Now.Date <= P.objConfPromocion.FechaFinal.Date)
                                                      /// Para la promociones de tipo VIP o VIP SENIOR
                                                  && (P.objConfPromocion.TipoPromo == CurrentClient.TipoConsultor && P.objConfPromocion.TipoPromo != "INICIAL")
                                                  && (P.ColComposiciones.Where(p => p.TipoComposicion == "O").Count() > 0)
                                                  && (P.objConfPromocion.UnaPorPedido.Value == false)
                                                  && (P.objConfPromocion.MontoMinimo.Value == 0 || MontoActual >= P.objConfPromocion.MontoMinimo.Value)
                                                  && (P.objConfPromocion.ColTransportistas.Count == 0 || P.objConfPromocion.ColTransportistas.Where(t => t.Transporte.ToUpper() == lblTransporteHidden.Value.ToString().ToUpper()).Count() > 0)
                                                  select P).Distinct<Producto>().ToList<Producto>();

            promosVIPConRegalos = promosVIPConRegalos.OrderByDescending(c => c.CantidadElementoRequeridos).ToList<Producto>();


            long[] idPromoConRegalos = (from P in promosConRegalos
                                        select P.IdProducto).ToArray<long>();


            List<Producto> promosSinRegalos = (from P in promosValidas
                                               join C in Contexto.Composicions on P.IdProducto equals C.ComponentePricipal
                                               where P.objConfPromocion != null
                                               && (DateTime.Now.Date >= P.objConfPromocion.FechaInicio.Date && DateTime.Now.Date <= P.objConfPromocion.FechaFinal.Date)
                                               && (idProductosSolicitados.Contains(C.objProductoHijo.IdProducto) || C.objProductoHijo.Tipo == 'P')
                                                   /// Para los tres tipos de promociones
                                               && (P.objConfPromocion.TipoPromo == "INICIAL" || P.objConfPromocion.TipoPromo == CurrentClient.TipoConsultor)
                                               && (P.ColComposiciones.Where(w => w.TipoComposicion == "O").Count() == 0)
                                               && (P.objConfPromocion.UnaPorPedido.Value == false)
                                               && (P.objConfPromocion.MontoMinimo.Value == 0 || MontoActual >= P.objConfPromocion.MontoMinimo.Value)
                                               && (P.objConfPromocion.ColTransportistas.Count == 0 || P.objConfPromocion.ColTransportistas.Where(t => t.Transporte.ToUpper() == lblTransporteHidden.Value.ToString().ToUpper()).Count() > 0)
                                               select P).Distinct<Producto>().ToList<Producto>();

            promosSinRegalos = promosSinRegalos.OrderByDescending(c => c.CantidadElementoRequeridos).ToList<Producto>();


            List<Producto> promosUnaPorPedido = (from P in promosValidas
                                                 join C in Contexto.Composicions on P.IdProducto equals C.ComponentePricipal
                                                 where P.objConfPromocion != null
                                                 && (DateTime.Now.Date >= P.objConfPromocion.FechaInicio.Date && DateTime.Now.Date <= P.objConfPromocion.FechaFinal.Date)
                                                 && (idProductosSolicitados.Contains(C.objProductoHijo.IdProducto))
                                                 && (P.objConfPromocion.TipoPromo == "INICIAL" || P.objConfPromocion.TipoPromo == CurrentClient.TipoConsultor)
                                                 && P.objConfPromocion.UnaPorPedido.Value
                                                 && (P.objConfPromocion.MontoMinimo.Value == 0 || MontoActual >= P.objConfPromocion.MontoMinimo.Value)
                                                 && (P.objConfPromocion.ColTransportistas.Count == 0 || P.objConfPromocion.ColTransportistas.Where(t => t.Transporte.ToUpper() == lblTransporteHidden.Value.ToString().ToUpper()).Count() > 0)
                                                 select P).Distinct().ToList<Producto>();

            promosUnaPorPedido = promosUnaPorPedido.OrderByDescending(c => c.CantidadElementoRequeridos).ToList<Producto>();


            List<DetallePedido> promosGeneradasConRegalo = new List<DetallePedido>();
            List<DetallePedido> promosGeneradasConRegaloVIP = new List<DetallePedido>();
            List<DetallePedido> promosGeneradasSinRegalos = new List<DetallePedido>();
            List<DetallePedido> promosGeneradasUnaxPedido = new List<DetallePedido>();
            List<DetallePedido> AllPromosGeneradas = new List<DetallePedido>();
            List<DetallePedido> PedidoTemp = new List<DetallePedido>();
            List<DetallePedido> PedidoActual = (List<DetallePedido>)Session["detPedido"];

            if (promosConRegalos.Count > 0 || promosSinRegalos.Count > 0 || promosUnaPorPedido.Count > 0 || promosVIPConRegalos.Count > 0)
            {
                // Generacion de promociones con regalo
                promosGeneradasConRegalo = helper.GenerarPromociones(PedidoActual, promosConRegalos, true).ToList<DetallePedido>();


                // Generacion de promociones sin regalo
                PedidoTemp.AddRange(promosGeneradasConRegalo);
                PedidoTemp.AddRange(PedidoActual);
                promosGeneradasSinRegalos = helper.GenerarPromociones(PedidoTemp, promosSinRegalos, false).ToList<DetallePedido>();


                // Generacion de Promociones VIP
                PedidoTemp.Clear();
                PedidoTemp.AddRange(promosGeneradasConRegalo);
                PedidoTemp.AddRange(PedidoActual);
                PedidoTemp.AddRange(promosGeneradasSinRegalos);
                promosGeneradasConRegaloVIP = helper.GenerarPromociones(PedidoTemp, promosVIPConRegalos, true).ToList<DetallePedido>();


                List<Producto> promosExcluyentes = new List<Producto>();
                promosExcluyentes.AddRange(promosConRegalos);
                promosExcluyentes.AddRange(promosSinRegalos);

                // Generacion de Promociones de tipo una por PEDIDO
                promosGeneradasUnaxPedido = helper.GenerarPromocionesUnaxPedido(PedidoTemp, promosUnaPorPedido, promosExcluyentes).ToList<DetallePedido>();

            }


            #region  Generacion de Promociones Posibles
            PedidoTemp.Clear();
            PedidoTemp.AddRange(promosGeneradasConRegalo);
            PedidoTemp.AddRange(PedidoActual);
            PedidoTemp.AddRange(promosGeneradasSinRegalos);
            Hashtable promosPosiblesConRegalos = helper.GenerarPromocionesPosibles(PedidoTemp, promosConRegalos);
            Hashtable promosPosiblesSinRegalos = helper.GenerarPromocionesPosibles(PedidoTemp, promosSinRegalos);

            Hashtable promosPosibles = new Hashtable();
            foreach (object item in promosPosiblesConRegalos.Keys)
            {
                promosPosibles.Add(item, promosPosiblesConRegalos[item]);
            }

            foreach (object item in promosPosiblesSinRegalos.Keys)
            {
                promosPosibles.Add(item, promosPosiblesSinRegalos[item]);
            }
            #endregion

            #region Generacion Promocion especial y fija de pago facil

            string LimiteCompra = (from P in Session["ParametrosSistema"] as List<Parametro>
                                   where P.IdParametro == (int)TiposDeParametros.LimiteCompra
                                   select P.Valor).Single();

            if (cboFormaPago.Text.Contains("Pago Fácil") && decimal.Parse(txtMontoGeneral.Text.Replace("$", "")) >= decimal.Parse(LimiteCompra))
            {
                string codigoPromoPagoFacil = "1150000021052";
                if (decimal.Parse(txtMontoGeneral.Text.Replace("$", "")) > 1500)
                {
                    codigoPromoPagoFacil = "1150000021073";
                }

                Producto promoPagoFacil = (from P in Contexto.Presentacions
                                           where P.Codigo.Trim() == codigoPromoPagoFacil
                                           select P.objProducto).First<Producto>();



                if (promoPagoFacil.objConfPromocion != null && promoPagoFacil.objConfPromocion.FechaInicio <= DateTime.Now && promoPagoFacil.objConfPromocion.FechaFinal > DateTime.Now)
                {
                    List<string> descripcionPromo = new List<string>();
                    descripcionPromo.Add("Pago|Fácil");

                    DetallePedido pedidoPagoFacil = new DetallePedido();
                    DetalleRegalos newRegalo = new DetalleRegalos();

                    var composicionRegalo = from R in promoPagoFacil.ColComposiciones
                                            where R.TipoComposicion == "O"
                                            group R by R.Grupo into c
                                            select new { Grupo = c.Key, componentes = c };

                    if (composicionRegalo.Count() > 0)
                    {

                        List<Producto> productos = (from P in composicionRegalo.First().componentes
                                                    select P.objProductoHijo).ToList<Producto>();


                        newRegalo.DescripcionRegalo = Helper.ObtenerDescripcionCompletaProductoEnComun(productos) + " x " + composicionRegalo.First().componentes.First().objPresentacion.Descripcion;
                        newRegalo.IdPresentacionRegaloSeleccionado = 0;
                        newRegalo.TipoRegalo = "Producto";
                        newRegalo.objDetallePedido = pedidoPagoFacil;
                        newRegalo.Grupo = composicionRegalo.First().componentes.First().Grupo.Value;


                        pedidoPagoFacil.ColRegalos.Add(newRegalo);
                        pedidoPagoFacil.Cantidad = 1;
                        pedidoPagoFacil.Producto = promoPagoFacil.IdProducto;
                        pedidoPagoFacil.Presentacion = promoPagoFacil.ColPresentaciones[0].IdPresentacion;
                        pedidoPagoFacil.ProductoDesc = promoPagoFacil.Descripcion;
                        pedidoPagoFacil.PresentacionDesc = promoPagoFacil.ColPresentaciones[0].Descripcion;
                        pedidoPagoFacil.DescripcionCompleta = pedidoPagoFacil.ProductoDesc;
                        pedidoPagoFacil.DescProductosUtilizados = descripcionPromo;
                        pedidoPagoFacil.CodigoCompleto = promoPagoFacil.ColPresentaciones[0].Codigo;
                        pedidoPagoFacil.Tipo = "E";

                        AllPromosGeneradas.Add(pedidoPagoFacil);
                    }
                }
            }

            #endregion

            #region Inicialización del control de visualización de Promociones
            AllPromosGeneradas.AddRange(promosGeneradasUnaxPedido);
            AllPromosGeneradas.AddRange(promosGeneradasConRegalo);
            AllPromosGeneradas.AddRange(promosGeneradasSinRegalos);
            AllPromosGeneradas.AddRange(promosGeneradasConRegaloVIP);


            ///Marco las promociones generada con el id de la promoción
            /// que se guardo anteriormente.
            if (Session["PromosGuardadas"] != null)
            {
                foreach (DetallePedido item in (Session["PromosGuardadas"] as List<DetallePedido>))
                {
                    var promoGenerada = (from p in AllPromosGeneradas
                                         where p.Producto == item.objProducto.IdProducto
                                         && p.IdDetalleLineaGuardado == 0
                                         select p).FirstOrDefault();

                    if (promoGenerada != null)
                    {
                        promoGenerada.IdDetalleLineaGuardado = item.IdDetallePedido;
                        promoGenerada.ValorTotal = Math.Abs(item.ValorTotal.Value);

                        int indexRegalo = 0;
                        foreach (DetallePedido detProdSel in item.ColProductosSeleccionados)
                        {
                            promoGenerada.ColRegalos[indexRegalo].TipoRegalo = "Producto";
                            promoGenerada.ColRegalos[indexRegalo].IdPresentacionPreSeleccionado = detProdSel.objPresentacion.IdPresentacion;
                            promoGenerada.IdRegaloSeleccionado = detProdSel.objPresentacion.IdPresentacion;

                            if (!detProdSel.objProducto.Descripcion.Contains("x Unidad"))
                                promoGenerada.ColRegalos[indexRegalo].DescripcionPreSeleccionado = detProdSel.objProducto.objPadre.Descripcion + " " + detProdSel.objProducto.Descripcion + " x " + detProdSel.objPresentacion.Descripcion;
                            else
                                promoGenerada.ColRegalos[indexRegalo].DescripcionPreSeleccionado = detProdSel.objProducto.Descripcion;

                            indexRegalo++;
                        }

                    }

                }
            }

            TotalizadorPromos1.InitControl(AllPromosGeneradas, promosPosibles);
            upPromos.Update();

            #endregion

            //Tiempo += "Fin Calculo Promos: " + string.Format("{0:mm:ss}", DateTime.Now);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "dd", "alert('" + Tiempo + "');", true);
        }

    }

    private void TotalizadorPedidos_LineaPedidoEliminada()
    {
        ActualizarTotalesGenerales();
    }

    #endregion

    #region WEB Metodos
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static String GetDireccionEntrega(string IdConsultor, string IdFormaDePago)
    {
        string retorno = "false";
        string dirEntrega = "";
        string Transporte = "";
        string Coordinador = "";
        string SubConsultor = "";

        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();


        Cliente CurrentConsultor = (from D in dc.Clientes
                                    where D.IdCliente == long.Parse(IdConsultor)
                                    select D).First<Cliente>();



        Direccione direccionIncial = (from D in dc.Direcciones
                                      where D.CodigoExterno == CurrentConsultor.CodigoExterno
                                      select D).FirstOrDefault<Direccione>();


        if (direccionIncial != null)
        {


            dirEntrega = direccionIncial.Provincia.ToLower().Trim() + " - " + direccionIncial.Localidad.ToLower().Trim();
            dirEntrega += "@" + direccionIncial.Calle.ToLower().Trim();

            ConfTransporte confT = (from C in dc.ConfTransportes
                                    where C.Provincia.ToLower() == direccionIncial.Provincia.ToLower() &&
                                    C.Localidad.ToLower() == direccionIncial.Localidad.ToLower() &&
                                    C.FormaDePago == IdFormaDePago
                                    select C).FirstOrDefault<ConfTransporte>();


            if (confT != null)
            {
                Transporte = confT.Transporte.ToLower();
            }
            else
                Transporte = "Sin Transporte";



            if (CurrentConsultor.CodTipoCliente == TipoClientesConstantes.CONSULTOR)
            {
                Cliente clienteSuperior = (from C in dc.Clientes
                                           where C.CodTipoCliente != TipoClientesConstantes.CONSULTOR
                                              && C.CodClasif1 == CurrentConsultor.CodClasif1
                                           select C).First();

                if ((clienteSuperior.CodTipoCliente == TipoClientesConstantes.SUBCONGESTION ||
                    clienteSuperior.CodTipoCliente == TipoClientesConstantes.SUBSINGESTION) && clienteSuperior.TipoCliente.ToUpper().Contains("SUB"))
                {
                    SubConsultor = clienteSuperior.Nombre.ToLower();

                    // Codigo Anterior
                    //clienteSuperior = (from C in dc.Clientes
                    //                    where C.CodTipoCliente != TipoClientesConstantes.CONSULTOR
                    //                    && C.CodTipoCliente != TipoClientesConstantes.SUBCONGESTION
                    //                    && ! C.TipoCliente.ToUpper().Contains("SUB")
                    //                    && C.CodVendedor == clienteSuperior.CodVendedor
                    //                    && C.Clasif1 != ""
                    //                    select C).First();

                    //if (clienteSuperior.Nombre.ToLower() == SubConsultor)
                    //{
                    //    SubConsultor = "Sin Asignar";
                    //}

                    Coordinador = clienteSuperior.Vendedor.ToLower();

                }
                else
                {
                    SubConsultor = "Sin Asignar";
                    Coordinador = clienteSuperior.Nombre.ToLower();

                }

            }
            else
            {

                SubConsultor = "Sin Asignar";
                Coordinador = CurrentConsultor.Nombre.ToLower();

            }

            string ultimaActualizacion = "";
            if (CurrentConsultor.UltimaActualizacion.HasValue)
                ultimaActualizacion = CurrentConsultor.UltimaActualizacion.Value.ToShortDateString();

            SubConsultor = CurrentConsultor.Clasif1;
            retorno = dirEntrega + "|" + Transporte + "|" + Coordinador + "@" + SubConsultor;
            retorno += "|" + DateTime.Now.ToShortDateString() + "|" + ObtenerSeguienteNro(0, dc);
            retorno += "|" + CurrentConsultor.SaldoCtaCte.ToString() + "|" + ultimaActualizacion;



            return retorno;
        }
        else
        {
            return "";
        }
    }

    #endregion

}