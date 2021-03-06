﻿using System;
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

public partial class NotaDePedidoTest : BasePage
{
    public List<Producto> _productos = null;
    public long _idDireccionGrabada = 0;

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

    private decimal SaldoPagoAnticipado
    {
        get
        {
            if (Session["SaldoAnticipado"] == null)
            {
                Session.Add("SaldoAnticipado", 0);
            }

            return decimal.Parse(Session["SaldoAnticipado"].ToString());
        }
        set
        {

            if (Session["SaldoAnticipado"] == null)
            {
                Session.Add("SaldoAnticipado", value);
            }
            else
                Session["SaldoAnticipado"] = value;




        }


    }

    private long IdDireccionSeleccionada
    {
        get
        {

            if (Session["IdDireccionSeleccionada"] == null)
            {
                Session.Add("IdDireccionSeleccionada", 0);
            }

            return (long)Session["IdDireccionSeleccionada"];
        }

        set
        {
            Session.Add("IdDireccionSeleccionada", value);
        }

    }

    public string TipoCliente
    {
        get
        {
            if (Session["Consultor"] != null)
            {

                return (Session["Consultor"] as Cliente).CodTipoCliente;
            }
            else
            {
                return "";
            }

        }


    }

    public List<Producto> Productos
    {
        get
        {
            if (Session["Productos"] != null)
            {

                return (Session["Productos"] as List<Producto>);
            }
            else
            {
                return new List<Producto>();
            }

        }
        set
        {
            Session["Productos"] = value;
        }

    }

    protected override void PageLoad()
    {
        #region PostBack
        if (!IsPostBack)
        {
            LimpiarPedido();
            Session.Add("MontoDisponibleCredito", null);
            Session.Add("ClienteLogeado", null);
            Session.Add("Cliente", null);
            Session.Add("detPedido", new List<DetallePedido>());
            Session.Add("Consultor", null);
            Session.Add("PromosGuardadas", new List<DetallePedido>());
            Session.Add("PromosGeneradas", null);
            Session.Add("IdDireccionSeleccionada", 0);
            ViewState.Add("TransportistaSeleccionado", "");
            Session["Context"] = null;
            Cliente cliente = null;



            Session["ClienteLogeado"] = (from C in Contexto.Clientes
                                         where C.IdCliente == long.Parse(Session["IdUsuario"].ToString())
                                         select C).Single<Cliente>();


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


                    Session["detPedido"] = (from d in CurrentCabecera.DetallePedidos
                                            where (d.objProducto.Tipo == 'A' && d.objPromocionOrigen == null)
                                            || ((d.objProducto.Tipo == 'P' || d.objProducto.Tipo == 'D') && d.ValorTotal > 0)
                                            select d).ToList();


                    /// Control de cantidad de productos por pedido
                    /// si el producto es de 5 ml, 55 ml o promoción de una sola por pedido
                    /// entonce marco al detalle como UnoPorPedido
                    foreach (var item in Session["detPedido"] as List<DetallePedido>)
                    {

                        if (item.objPresentacion.Descripcion.Contains("5 ml") ||
                        item.objPresentacion.Descripcion.Contains("55 ml") ||
                        ((item.objPresentacion.objProducto.Tipo == 'P' || item.objPresentacion.objProducto.Tipo == 'D') && item.objPresentacion.objProducto.objConfPromocion != null && item.objProducto.objConfPromocion.UnaPorPedido.Value))

                            item.UnoPorPedido = true;
                        else
                            item.UnoPorPedido = false;

                    }


                    Session["PromosGuardadas"] = (from d in CurrentCabecera.DetallePedidos
                                                  where (d.objProducto.Tipo == 'P' || d.objProducto.Tipo == 'D') && d.ValorTotal < 0
                                                  select d).ToList();

                    CargarArbolProductos();
                    CargarFormaDePago(cliente);
                    cboFormaPago.SelectedValue = CurrentCabecera.FormaPago.ToString();

                    cboConsultores.Items.Add(new RadComboBoxItem(cliente.Nombre.Trim(), cliente.IdCliente.ToString()));
                    _idDireccionGrabada = CurrentCabecera.DireccionEntrega;
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
                    RadTabStrip2.Visible = false;
                    RadMultiPrincipal.Visible = false;
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
                    CargarArbolProductos();
                    cboConsultores.AppendDataBoundItems = true;
                    cboConsultores.Items.Add(new RadComboBoxItem("Seleccione un Revendedor", "0"));
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
                    CargarArbolProductos();
                    cboConsultores.Items.Add(new RadComboBoxItem(cliente.Nombre.Trim(), cliente.IdCliente.ToString()));
                    UCTotalizadorNivel.IniciarControl();
                    CargarFormaDePago(cliente);
                    CargarEncabezado(cliente, 0);
                    cboConsultores.Enabled = false;
                    cboConsultores.SelectedIndex = 0;


                }


            }








            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("ImagenesPromos/thumbs"));
            int cantidadImgagenes = dir.GetFiles().Where(w => w.Extension.ToLower() == ".jpg").Count();
            string imagenes = "";

            for (int j = 1; j < 4; j++)
            {
                for (int i = 1; i <= cantidadImgagenes; i++)
                {
                    imagenes += string.Format("<li value='{0}'><img src='ImagenesPromos/thumbs/{1}.jpg' width='179' height='100' alt='' /></li>", i, i);
                }
            }
            thumbs.InnerHtml = imagenes;


        }

        #endregion

        ucGrillaDirecciones.DireccionSeleccionada += new RowSelectedHanddler(ucGrillaDirecciones_DireccionSeleccionada);
        UcTotalizadorPedidos.LineaPedidoEliminada += new DeletePedidoHanddler(TotalizadorPedidos_LineaPedidoEliminada);
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

                UCTotalizadorNivel.IniciarControl();

                CargarFormaDePago(cliente);

                CargarDireccionesEntrega(cliente);

                ActualizarProductosEspeciales(cliente);

                if ((Session["detPedido"] as List<DetallePedido>).Count > 0)
                {
                    CalcularPromociones();
                }


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


                if (
                    (TipoCliente.ToString() == Convert.ToString((int)TipoClientes.PotencialBolso) && IdsPresentaciones.Count() > 2)
                    ||
                    (TipoCliente.ToString() == Convert.ToString((int)TipoClientes.PotencialBolso) && (Session["detPedido"] as List<DetallePedido>).Count > 0))
                {
                    upFichaTecnica.Update();
                    ScriptManager.RegisterStartupScript(upFichaTecnica, typeof(UpdatePanel), "Potencial", "alert('Solo se puede solicitar un tipo de incorporacion')", true);
                    return;
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
                                detPedido.Tipo = presentacion.objProducto.Tipo.ToString();

                                if (presentacion.objProducto.objPadre.EsUltimoNivel.Value)
                                {
                                    detPedido.DescripcionCompleta = detPedido.ProductoDesc + " x " + detPedido.PresentacionDesc;
                                    detPedido.IdPadre = presentacion.objProducto.objPadre.IdProducto;
                                    detPedido.DescPadre = Helper.ObtenerDescripcionCompletaProducto(presentacion.objProducto.objPadre);
                                    detPedido.DescripcionCompleta = detPedido.ProductoDesc + " x " + detPedido.PresentacionDesc;
                                }
                                else if (presentacion.objProducto.EsUltimoNivel.Value && (detPedido.Tipo == "P" || detPedido.Tipo == "D"))
                                {
                                    detPedido.DescripcionCompleta = detPedido.PresentacionDesc;
                                    detPedido.IdPadre = presentacion.objProducto.objPadre.IdProducto;
                                    detPedido.DescPadre = Helper.ObtenerDescripcionCompletaProducto(presentacion.objProducto.objPadre);
                                    detPedido.ValorUnitario = -1 * presentacion.Precio.Value;
                                    detPedido.ValorTotal = detPedido.ValorUnitario * detPedido.Cantidad;
                                }
                                else
                                {
                                    detPedido.DescripcionCompleta = presentacion.objProducto.DescripcionCompleta + detPedido.PresentacionDesc;
                                    detPedido.IdPadre = presentacion.objProducto.objPadre.objPadre.IdProducto;
                                    detPedido.DescPadre = Helper.ObtenerDescripcionCompletaProducto(presentacion.objProducto.objPadre.objPadre);
                                    detPedido.DescripcionCompleta = detPedido.ProductoDesc + " x " + detPedido.PresentacionDesc;
                                }

                                /// Control de cantidad de productos por pedido
                                /// si el producto es de 5 ml, 55 ml o promoción de una sola por pedido
                                /// entonce marco al detalle como UnoPorPedido
                                if (presentacion.Descripcion.Contains("5 ml") ||
                                    presentacion.Descripcion.Contains("55 ml") ||
                                    ((presentacion.objProducto.Tipo == 'P' || presentacion.objProducto.Tipo == 'D') && presentacion.objProducto.objConfPromocion != null && presentacion.objProducto.objConfPromocion.UnaPorPedido.Value))

                                    detPedido.UnoPorPedido = true;
                                else
                                    detPedido.UnoPorPedido = false;


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
        string id = "";
        if (Request.QueryString["IdPedido"] != null)
            id = Request.QueryString["IdPedido"].ToString();

        GuardarPedido(id, false, false);
    }

    protected void btnPedidoTemporal_RealizarPedido(object sender, EventArgs e)
    {
        string id = "";
        if (Request.QueryString["IdPedido"] != null)
            id = Request.QueryString["IdPedido"].ToString();

        GuardarPedido(id, false, true);
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
            if (row["IdProducto"].ToString() == "4251")
            {
                e.Node.Visible = false;
            }

            e.Node.Attributes.Add("Cargar", "false");
            e.Node.Attributes.Add("RutaImagen", row["Image"].ToString());

        }

        if (row["Descripcion"].ToString() == "Incorporaciones" && row["Nivel"].ToString() == "2")
        {
            e.Node.Attributes.Add("NodoIncorporaciones", "true");
        }


    }

    /// <summary>
    ///  Metodo que se dispara cuando se selecciona una nueva dirección de la grilla 
    ///  de direcciones.
    /// </summary>
    /// <param name="Id">Id de la dirección seleccionada</param>
    protected void ucGrillaDirecciones_DireccionSeleccionada(long Id)
    {

        Direccione dirSeleccionada = (from D in Contexto.Direcciones
                                      where D.IdDireccion == Id
                                      select D).First<Direccione>();


        lblCalle.Text = dirSeleccionada.Calle.ToLower();
        lblDireccionEntrega.Text = dirSeleccionada.Provincia.ToLower() + " - " + dirSeleccionada.Localidad.ToLower();
        IdDireccionSeleccionada = dirSeleccionada.IdDireccion;

        var ultimoPedido = (from v in Contexto.View_UltimoPedidoClientes
                            where v.IdCliente == dirSeleccionada.Cliente.Value
                            select v.UltimaFechaPedido).FirstOrDefault();

        lblUltimoPedido.Text = ultimoPedido == null ? "Sin Pedido Previo" : ultimoPedido.Value.ToShortDateString();


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
            lblTransporte.Text = "SIN TRANSPORTE";
            lblTransporteHidden.Value = "";
            lblTransporteValorHidden.Value = "0";
            lblProvinciaPorcentajeDescuentoHidden.Value = "0";
            lblDescuentoProvincia.Text = "$ 0";
        }

        ActualizarTotalesGenerales();
        CargarDatosJerarquia();
        upEncImp.Update();

    }

    protected void cboFormaPago_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (cboFormaPago.SelectedItem.Value != "0" && IdDireccionSeleccionada != 0)
        {

            Direccione dirEntrega = (from D in Contexto.Direcciones
                                     where D.IdDireccion == IdDireccionSeleccionada
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

            if (cboFormaPago.SelectedItem.Text == "Rapi Pago")
                lnkRapiPago.Visible = true;
            else
                lnkRapiPago.Visible = false;

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

    private void EliminarProductosKit(bool EsTemporal, CabeceraPedido cabecera)
    {
        #region Eliminacion de Productos del tipo KIT

        /// Elimino los productos que fueron reemplazados por otros
        /// según la logica de negocio especifica para estos casos.
        if (!EsTemporal)
        {
            List<DetallePedido> DetallesEliminar = new List<DetallePedido>();

            /// Elimino el producto kit Deditos Sticker y Ambientador (5472)
            List<DetallePedido> detallesKitDeditos = (from d in cabecera.DetallePedidos
                                                      where d.Presentacion == 5472
                                                      select d).ToList();

            if (detallesKitDeditos.Count > 0)
            {
                foreach (var item in detallesKitDeditos)
                {
                    if (item.IdDetallePedido != 0)
                    {
                        Contexto.DetallePedidos.DeleteOnSubmit(item);
                    }

                    DetallesEliminar.Add(item);
                }
            }

            /// Elimino el producto kit Inquietos Sticker y Ambientador (5473)
            List<DetallePedido> detallesKitInquietos = (from d in cabecera.DetallePedidos
                                                        where d.Presentacion == 5473
                                                        select d).ToList();

            if (detallesKitInquietos.Count > 0)
            {
                foreach (var item in detallesKitInquietos)
                {
                    if (item.IdDetallePedido != 0)
                    {
                        Contexto.DetallePedidos.DeleteOnSubmit(item);
                    }
                    DetallesEliminar.Add(item);
                }
            }

            foreach (DetallePedido item in DetallesEliminar)
            {
                cabecera.DetallePedidos.Remove(item);
            }

        }

        #endregion
    }

    private void ReemplazarProductos(CabeceraPedido cabecera)
    {
        List<DetallePedido> nuevosDetalles = new List<DetallePedido>();
        foreach (DetallePedido det in cabecera.DetallePedidos)
        {
            if (det.Presentacion == 5472)
            {
                // 5472: idPresentacino kit Deditos Sticker y Ambientador
                /// Si esta este producto y se esta realizando el pedido, no se debe
                /// agregar este producto sino que se tiene que reemplazar por:
                /// 1. Stickers Deditos $10 (2041600039001-000-00 ) - IdPre: 5479
                /// 2. SM Ambientador Automatico Digital x unidad $146 (1041600116001-000-00 ) - IdPre: 5486

                Presentacion StickersDeditos = (from P in Contexto.Presentacions
                                                where P.IdPresentacion == 5532
                                                select P).FirstOrDefault<Presentacion>();

                Presentacion AmbientadorAutomaticoDigital = (from P in Contexto.Presentacions
                                                             where P.IdPresentacion == 5486
                                                             select P).FirstOrDefault<Presentacion>();


                DetallePedido newDetalleST = new DetallePedido();
                newDetalleST.Cantidad = det.Cantidad;
                newDetalleST.CodigoCompleto = StickersDeditos.Codigo;
                newDetalleST.Presentacion = StickersDeditos.IdPresentacion;
                newDetalleST.Producto = StickersDeditos.objProducto.IdProducto;
                newDetalleST.ValorUnitario = StickersDeditos.Precio;
                newDetalleST.ValorTotal = det.Cantidad * StickersDeditos.Precio;
                nuevosDetalles.Add(newDetalleST);


                DetallePedido newDetallePlacerAD = new DetallePedido();
                newDetallePlacerAD.Cantidad = det.Cantidad;
                newDetallePlacerAD.CodigoCompleto = AmbientadorAutomaticoDigital.Codigo;
                newDetallePlacerAD.Presentacion = AmbientadorAutomaticoDigital.IdPresentacion;
                newDetallePlacerAD.Producto = AmbientadorAutomaticoDigital.objProducto.IdProducto;
                newDetallePlacerAD.ValorUnitario = AmbientadorAutomaticoDigital.Precio;
                newDetallePlacerAD.ValorTotal = det.Cantidad * AmbientadorAutomaticoDigital.Precio;
                nuevosDetalles.Add(newDetallePlacerAD);


            }
            else if (det.Presentacion == 5473)
            {
                // 5473: idPresentacino kit Inquietos Sticker y Ambientador
                /// Si esta este producto y se esta realizando el pedido, no se debe
                /// agregar este producto sino que se tiene que reemplazar por:
                /// 1. Stickers Inquietos $10 (2051600039001-000-00 ) - IdPre: 5480
                /// 2. SM Ambientador Automatico Digital x unidad $146 (1041600116001-000-00 ) - IdPre: 5486

                Presentacion StickersInquietos = (from P in Contexto.Presentacions
                                                  where P.IdPresentacion == 5533
                                                  select P).FirstOrDefault<Presentacion>();

                Presentacion AmbientadorAutomaticoDigital = (from P in Contexto.Presentacions
                                                             where P.IdPresentacion == 5486
                                                             select P).FirstOrDefault<Presentacion>();


                DetallePedido newDetalleSI = new DetallePedido();
                newDetalleSI.Cantidad = det.Cantidad;
                newDetalleSI.CodigoCompleto = StickersInquietos.Codigo;
                newDetalleSI.Presentacion = StickersInquietos.IdPresentacion;
                newDetalleSI.Producto = StickersInquietos.objProducto.IdProducto;
                newDetalleSI.ValorUnitario = StickersInquietos.Precio;
                newDetalleSI.ValorTotal = det.Cantidad * StickersInquietos.Precio;
                nuevosDetalles.Add(newDetalleSI);

                DetallePedido newDetallePlacerAD = new DetallePedido();
                newDetallePlacerAD.Cantidad = det.Cantidad;
                newDetallePlacerAD.CodigoCompleto = AmbientadorAutomaticoDigital.Codigo;
                newDetallePlacerAD.Presentacion = AmbientadorAutomaticoDigital.IdPresentacion;
                newDetallePlacerAD.Producto = AmbientadorAutomaticoDigital.objProducto.IdProducto;
                newDetallePlacerAD.ValorUnitario = AmbientadorAutomaticoDigital.Precio;
                newDetallePlacerAD.ValorTotal = det.Cantidad * AmbientadorAutomaticoDigital.Precio;
                nuevosDetalles.Add(newDetallePlacerAD);


            }
        }

        cabecera.DetallePedidos.AddRange(nuevosDetalles);
        EliminarProductosKit(false, cabecera);

    }

    private static void CalcularSaldoPagoAnticipado(Cliente cliente, Marzzan_InfolegacyDataContext contexto)
    {
        try
        {
            decimal SaldoInformado = cliente.SaldoPagoAnticipado.Value;
            List<decimal?> PagoAnticipadoUtilizado = (from c in contexto.PedidosConCreditos
                                                      where c.Cliente == cliente.IdCliente
                                                      && c.Procesado.Value
                                                      && (c.MontoCredito.Value - c.MontoPagado.Value) >= 0
                                                      select c.MontoPagado).ToList();

            if (SaldoInformado != 0)
            {
                HttpContext.Current.Session["SaldoAnticipado"] = (SaldoInformado + PagoAnticipadoUtilizado.Sum());
            }
            else
            {
                HttpContext.Current.Session["SaldoAnticipado"] = cliente.SaldoCtaCte;
            }

        }
        catch (Exception err)
        {

        }

    }

    private void GuardarPedido(string idPedido, bool FaltaSaldo, bool EsTemporal)
    {
        DetallePedido newDetalle = null;
        string mensajeStock = "";

        if (ControlStockValido(EsTemporal, out mensajeStock))
        {
            try
            {
                #region Calculo el Total Comprado
                /// Solo tiene en cuenta los productos comisionables para el calculo, es decir
                /// aquello donde el código comienza con un 1.
                decimal TotalComprado = (from P in (Session["detPedido"] as List<DetallePedido>)
                                         where (P.CodigoCompleto.Substring(0, 1) == "1" && P.Tipo == "A")
                                         || (P.Tipo == "P") || (P.Tipo == "D")
                                         select P.ValorTotal.Value).Sum();

                // Sumo el costo del transporte
                TotalComprado += decimal.Parse(lblCostoFlete.Text.Replace("$", ""));





                #endregion

                #region Calculo el Total Comprado para control PROMOCIONES

                if (!EsTemporal)
                {
                    decimal TotalParaPromociones = 0;

                    /// Calculo de los productos en las promociones Directas
                    List<DetallePedido> promosTemp = (Session["PromosGeneradas"] as List<DetallePedido>).Where(w => w.IdRelacionDetallePromo != 0).ToList();
                    foreach (DetallePedido promo in promosTemp)
                    {
                        TotalParaPromociones += promo.colProductosRequeridos.Where(w => w.CodigoCompleto.Substring(0, 1) != "2").Sum(w => w.ValorUnitario * w.Cantidad).Value;
                    }

                    /// Calculo de los productos solicitados
                    TotalParaPromociones += (from P in (Session["detPedido"] as List<DetallePedido>)
                                             where (P.CodigoCompleto.Substring(0, 1) != "2" && P.Tipo == "A")
                                             select P.ValorTotal.Value).Sum();


                    /// Recupero los montos minimos de las promociones segun su configuracion.
                    List<long> idsPromosTemp = (Session["PromosGeneradas"] as List<DetallePedido>).Select(w => w.Producto.Value).ToList();
                    var PromocionesInvalidas = (from p in Contexto.ConfPromociones
                                                where idsPromosTemp.Contains(p.IdProductoPromo) && p.MontoMinimo > TotalParaPromociones
                                                select new
                                                {
                                                    Descripcion = p.objProductoPromo.Descripcion,
                                                    Monto = p.MontoMinimo
                                                }).ToList();

                    if (PromocionesInvalidas.Count > 0)
                    {
                        string mensajePromos = "Las siguientes promociones no pueden ser solicitadas, ya que las mismas poseen un monto mínimo establecido, el cual es mayor al monto del pedido.</br>Promociones:</br>";
                        foreach (var item in PromocionesInvalidas)
                        {
                            mensajePromos += "&nbsp;&nbsp;&nbsp;" + item.Descripcion + " <b>Monto Mínimo: " + string.Format("${0:###.0#}", item.Monto.Value) + "</b>";
                        }

                        ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "MinimoReqCredito", "AlertaMinimoPromociones('" + mensajePromos + "');", true);
                        return;
                    }
                }
                #endregion

                #region Verifico que la dirección seleccionada pertenezca al cliente seleccionado

                Direccione dirEntrega = (from D in Contexto.Direcciones
                                         where D.IdDireccion == IdDireccionSeleccionada
                                         select D).FirstOrDefault<Direccione>();

                Cliente CliSeleccionado = (from D in Contexto.Clientes
                                           where D.IdCliente == long.Parse(cboConsultores.SelectedValue)
                                           select D).First<Cliente>();



                if (dirEntrega == null || dirEntrega.CodigoExterno != CliSeleccionado.CodigoExterno)
                {
                    ucGrillaDirecciones.InitControl(CliSeleccionado.CodigoExterno);
                    lblDireccionEntrega.Text = "";
                    lblCalle.Text = "";
                    IdDireccionSeleccionada = 0;
                    upEncImp.Update();
                    ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "VerificacionDireccion", "AlertaDireccion();", true);
                    return;

                }

                #endregion

                #region Busco el limite de compra por provincia
                decimal LimitePorProvincia = 0;




                var LimiteCompraProvincia = (from P in Contexto.LimitesDeCompras
                                             where P.Provincia == dirEntrega.Provincia && P.Localidad == dirEntrega.Localidad
                                             select P.Limite).FirstOrDefault();

                /// si es null significa que no hay una definicion especifica
                /// para localidad y provincia entonces busco solo para la provincia.
                if (LimiteCompraProvincia == null)
                {
                    LimiteCompraProvincia = (from P in Contexto.LimitesDeCompras
                                             where P.Provincia == dirEntrega.Provincia
                                             select P.Limite).FirstOrDefault();

                    if (LimiteCompraProvincia != null)
                        LimitePorProvincia = LimiteCompraProvincia.Value;
                }
                else
                {
                    LimitePorProvincia = LimiteCompraProvincia.Value;
                }

                #endregion

                #region Busco el limite de compra general
                string LimiteCompraGeneral = (from P in Session["ParametrosSistema"] as List<Parametro>
                                              where P.IdParametro == (int)TiposDeParametros.LimiteCompra
                                              select P.Valor).Single();

                #endregion

                #region Busco el limite de compra en Contra Reembolso
                string LimiteContraReembolso = (from P in Session["ParametrosSistema"] as List<Parametro>
                                                where P.IdParametro == (int)TiposDeParametros.LimiteContraReembolso
                                                select P.Valor).Single();

                #endregion

                #region Control LIMITE POR PROVINCIA Y LIMITE GENERAL
                if (!EsTemporal)
                {

                    if (TipoCliente.ToString() != Convert.ToString((int)TipoClientes.PotencialBolso))
                    {
                        /// Control LIMITE POR PROVINCIA
                        if (TotalComprado < LimitePorProvincia)
                        {
                            ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "MinimoReq", "AlertaMinimoRequeridoProvincia(" + LimitePorProvincia + ");", true);
                            return;
                        }


                        /// Control LIMITE GENERAL
                        if (!cboConsultores.Text.Contains("bolsos"))
                        {
                            if (TotalComprado < int.Parse(LimiteCompraGeneral))
                            {
                                ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "MinimoReq", "AlertaMinimoRequerido(" + LimiteCompraGeneral + ");", true);
                                return;
                            }
                        }
                    }
                }
                #endregion

                #region Control LIMITE EN CREDITO
                if (!EsTemporal)
                {
                    if (cboFormaPago.SelectedItem.Text == "Crédito")
                    {
                        decimal MontoDisponibleCredito = decimal.Parse(Session["MontoDisponibleCredito"].ToString());
                        if (TotalComprado > MontoDisponibleCredito)
                        {
                            ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "MinimoReqCredito", "AlertaMinimoRequeridoCredito(" + string.Format("{0:0.00}", MontoDisponibleCredito) + ");", true);
                            return;
                        }
                    }
                }
                #endregion

                #region Generacion de la Cabecera del Pedido
                CabeceraPedido cabecera = null;

                if (idPedido != "")
                {

                    cabecera = (from C in Contexto.CabeceraPedidos
                                where C.IdCabeceraPedido == long.Parse(Request.QueryString["IdPedido"].ToString())
                                select C).SingleOrDefault();

                    cabecera.DireccionEntrega = IdDireccionSeleccionada;
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
                    cabecera.DireccionEntrega = IdDireccionSeleccionada;
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
                    List<DetallePedido> DetallesEliminar = new List<DetallePedido>();

                    /// Elimino los producto de regalos guardados ya que se volveran a generar.
                    DetallesEliminar.AddRange(cabecera.DetallePedidos.Where(w => w.objPromocionOrigen != null));

                    /// Elimino la promociones guardadas pero NO las promociones del detalle, es decir
                    /// aquellas que fueron solicitadas en forma directa,
                    DetallesEliminar.AddRange(cabecera.DetallePedidos.Where(w =>
                                            (w.objProducto.Tipo != 'A' &&
                                            w.objProducto.Tipo != 'G' &&
                                            w.objProducto.Tipo != 'R' &&
                                            w.objProducto.Tipo != 'N' &&
                                            w.objProducto.Tipo != 'I') && ((w.objProducto.Tipo == 'P' || w.objProducto.Tipo == 'D') && w.ValorTotal < 0))
                        );



                    Contexto.DetallePedidos.DeleteAllOnSubmit(DetallesEliminar);



                    /// Elimino las bolsas de papel
                    DetallePedido detalleBolsas = (from d in cabecera.DetallePedidos
                                                   where d.CodigoCompleto == "2520000018001"
                                                   select d).FirstOrDefault();
                    if (detalleBolsas != null)
                    {
                        Contexto.DetallePedidos.DeleteOnSubmit(detalleBolsas);
                        DetallesEliminar.Add(detalleBolsas);
                    }

                    /// Elimino los gatillos
                    DetallePedido detalleGatillos = (from d in cabecera.DetallePedidos
                                                     where d.CodigoCompleto == "2520000032073"
                                                     select d).FirstOrDefault();
                    if (detalleGatillos != null)
                    {
                        Contexto.DetallePedidos.DeleteOnSubmit(detalleGatillos);
                        DetallesEliminar.Add(detalleGatillos);
                    }


                    /// Elimino las Varillas
                    DetallePedido detalleVarillas = (from d in cabecera.DetallePedidos
                                                     where d.CodigoCompleto == "2520700110010"
                                                     select d).FirstOrDefault();
                    if (detalleVarillas != null)
                    {
                        Contexto.DetallePedidos.DeleteOnSubmit(detalleVarillas);
                        DetallesEliminar.Add(detalleVarillas);
                    }

                    /// Elimino el gasto de Envio
                    DetallePedido detalleGastoEnvio = (from d in cabecera.DetallePedidos
                                                       where d.objProducto.Tipo == 'G'
                                                       select d).FirstOrDefault();

                    if (detalleGastoEnvio != null)
                    {
                        Contexto.DetallePedidos.DeleteOnSubmit(detalleGastoEnvio);
                        DetallesEliminar.Add(detalleGastoEnvio);
                    }


                    foreach (DetallePedido item in DetallesEliminar)
                    {
                        cabecera.DetallePedidos.Remove(item);
                    }

                    Contexto.SubmitChanges();


                }


                #endregion

                #region Detalles de Pedido
                foreach (DetallePedido det in (Session["detPedido"] as List<DetallePedido>))
                {
                    if (det.IdDetallePedido == 0)
                    {

                        /// Si se trata de un producto de tipo promocion
                        /// entonces no genero el producto y lo reemplazo por los
                        /// elementos requeridos de la promoción.
                        if (!EsTemporal && det.IdRelacionDetallePromo != 0)
                        {

                            List<DetallePedido> promos = (Session["PromosGeneradas"] as List<DetallePedido>).Where(w => w.IdRelacionDetallePromo == det.IdRelacionDetallePromo).ToList();
                            foreach (DetallePedido promo in promos)
                            {
                                foreach (DetalleProductosRequeridos item in promo.colProductosRequeridos)
                                {

                                    newDetalle = cabecera.DetallePedidos.Where(w => w.Producto.Value == item.IdProducto && w.Presentacion.Value == item.IdPresentacion).FirstOrDefault();

                                    if (newDetalle == null)
                                    {
                                        newDetalle = new DetallePedido();
                                        newDetalle.Cantidad = item.Cantidad;
                                        newDetalle.CodigoCompleto = item.CodigoCompleto;
                                        newDetalle.Presentacion = item.IdPresentacion;
                                        newDetalle.Producto = item.IdProducto;
                                        newDetalle.ValorUnitario = item.ValorUnitario;
                                        newDetalle.ValorTotal = newDetalle.ValorUnitario * newDetalle.Cantidad;
                                        cabecera.DetallePedidos.Add(newDetalle);
                                    }
                                    else
                                    {
                                        newDetalle.Cantidad += item.Cantidad;
                                        newDetalle.ValorTotal = newDetalle.ValorUnitario * newDetalle.Cantidad;
                                        cabecera.DetallePedidos.Add(newDetalle);
                                    }


                                }
                            }


                        }
                        //else if (!EsTemporal && (det.Presentacion == 5473 || det.Presentacion == 5472))
                        //{
                        //    ReemplazarProductos(det, cabecera);
                        //}
                        else
                        {
                            newDetalle = new DetallePedido();
                            newDetalle.Cantidad = long.Parse(det.Cantidad.ToString());
                            newDetalle.CodigoCompleto = det.CodigoCompleto;
                            newDetalle.Presentacion = det.Presentacion;
                            newDetalle.Producto = det.Producto.Value;
                            newDetalle.ValorUnitario = det.ValorUnitario;
                            newDetalle.ValorTotal = newDetalle.ValorUnitario * newDetalle.Cantidad;
                            cabecera.DetallePedidos.Add(newDetalle);


                            /// Asocio al detalle de tipo promo los productos requeridos seleccionados
                            /// para luego poder reconstruir cuando se edite
                            if (det.IdRelacionDetallePromo != 0 && det.Tipo == "P" && EsTemporal)
                            {
                                List<DetallePedido> promos = (Session["PromosGeneradas"] as List<DetallePedido>).Where(w => w.IdRelacionDetallePromo == det.IdRelacionDetallePromo).ToList();
                                int grupo = 0;
                                foreach (DetallePedido promo in promos)
                                {
                                    foreach (DetalleProductosRequeridos item in promo.colProductosRequeridos)
                                    {
                                        DetalleProductosRequeridos detReq = new DetalleProductosRequeridos();
                                        detReq.Cantidad = item.Cantidad;
                                        detReq.IdProducto = item.IdProducto;
                                        detReq.IdPresentacion = item.IdPresentacion;
                                        detReq.ValorUnitario = item.ValorUnitario;
                                        detReq.CodigoCompleto = item.CodigoCompleto;
                                        detReq.objDetallePedido = newDetalle;
                                        detReq.Grupo = grupo;
                                        detReq.Tipo = item.Tipo;
                                        detReq.DescripcionProducto = item.DescripcionProducto;
                                    }

                                    grupo++;
                                }
                            }

                        }
                    }
                    /// Si es un pedido guardado y que se esta editando
                    /// se aplica est logica.
                    else
                    {

                        DetallePedido detGuardado = (from D in cabecera.DetallePedidos
                                                     where D.IdDetallePedido == det.IdDetallePedido
                                                     select D).FirstOrDefault<DetallePedido>();
                        if (detGuardado != null)
                        {

                            detGuardado.Cantidad = long.Parse(det.Cantidad.ToString());
                            detGuardado.ValorUnitario = det.ValorUnitario;
                            detGuardado.ValorTotal = detGuardado.ValorUnitario * detGuardado.Cantidad;


                            /// Asocio al detalle de tipo promo los productos requeridos seleccionados
                            /// para luego poder reconstruir cuando se edite
                            if (detGuardado.IdRelacionDetallePromo != 0 && (det.Tipo == "P" || det.Tipo == "D"))
                            {
                                if (EsTemporal)
                                {
                                    /// Elimino el detalle existente y los vuelvo a generar para reflejar los cambios posibles.
                                    Contexto.DetalleProductosRequeridos.DeleteAllOnSubmit(detGuardado.colProductosRequeridos);

                                    /// vuelvo a generar para reflejar los cambios posibles.
                                    List<DetallePedido> promos = (Session["PromosGeneradas"] as List<DetallePedido>).Where(w => w.IdRelacionDetallePromo == det.IdRelacionDetallePromo).ToList();
                                    int grupo = 0;

                                    foreach (DetallePedido promo in promos)
                                    {
                                        foreach (DetalleProductosRequeridos item in promo.colProductosRequeridos)
                                        {
                                            DetalleProductosRequeridos detReq = new DetalleProductosRequeridos();
                                            detReq.Cantidad = item.Cantidad;
                                            detReq.IdProducto = item.IdProducto;
                                            detReq.IdPresentacion = item.IdPresentacion;
                                            detReq.ValorUnitario = item.ValorUnitario;
                                            detReq.CodigoCompleto = item.CodigoCompleto;
                                            detReq.objDetallePedido = detGuardado;
                                            detReq.Grupo = grupo;
                                            detReq.Tipo = item.Tipo;
                                            detReq.DescripcionProducto = item.DescripcionProducto;
                                        }

                                        grupo++;
                                    }
                                }
                                else
                                {

                                    List<DetallePedido> promos = (Session["PromosGeneradas"] as List<DetallePedido>).Where(w => w.IdRelacionDetallePromo == detGuardado.IdRelacionDetallePromo).ToList();
                                    foreach (DetallePedido promo in promos)
                                    {
                                        foreach (DetalleProductosRequeridos item in promo.colProductosRequeridos)
                                        {

                                            newDetalle = cabecera.DetallePedidos.Where(w => w.Producto.Value == item.IdProducto && w.Presentacion.Value == item.IdPresentacion).FirstOrDefault();

                                            if (newDetalle == null)
                                            {
                                                newDetalle = new DetallePedido();
                                                newDetalle.Cantidad = item.Cantidad;
                                                newDetalle.CodigoCompleto = item.CodigoCompleto;
                                                newDetalle.Presentacion = item.IdPresentacion;
                                                newDetalle.Producto = item.IdProducto;
                                                newDetalle.ValorUnitario = item.ValorUnitario;
                                                newDetalle.ValorTotal = newDetalle.ValorUnitario * newDetalle.Cantidad;
                                                cabecera.DetallePedidos.Add(newDetalle);
                                            }
                                            else
                                            {
                                                newDetalle.Cantidad += item.Cantidad;
                                                newDetalle.ValorTotal = newDetalle.ValorUnitario * newDetalle.Cantidad;
                                                cabecera.DetallePedidos.Add(newDetalle);
                                            }


                                        }
                                    }


                                    /// Elimino el detalle pedido de tipo promocion, ya que fue reemplazo por los 
                                    /// productos requeridos que lo componen.
                                    Contexto.DetallePedidos.DeleteOnSubmit(detGuardado);
                                    cabecera.DetallePedidos.Remove(detGuardado);
                                }
                            }
                        }
                    }
                }


                #endregion

                #region Reemplazo de Productos Especificos
                if (!EsTemporal)
                {
                    ReemplazarProductos(cabecera);
                }
                #endregion

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

                #region  Detalle Varillas de Bambú
                // Producto Difusor: 4868
                // Presentaciones con descripcion: Repuesto

                string[] CodigosRepuestosDifusores = new string[] { "1010700110   -194-20 ", "1010700110   -195-20 ", "1010700110   -196-20 ", "1010700110   -197-20 ", "1010700110   -215-20 ", "1010700110   -209-20 " };

                long CantidadRepuestos = Convert.ToInt64(((from N in cabecera.DetallePedidos
                                                           where CodigosRepuestosDifusores.Contains(N.CodigoCompleto)
                                                           select N.Cantidad.Value).Sum() * 1));


                if (CantidadRepuestos > 0)
                {

                    Presentacion preVarilla = (from P in Contexto.Presentacions
                                               where P.Codigo == "2520700110010"
                                               select P).SingleOrDefault();


                    newDetalle = new DetallePedido();
                    newDetalle.Cantidad = CantidadRepuestos;
                    newDetalle.CodigoCompleto = preVarilla.Codigo;
                    newDetalle.Presentacion = preVarilla.IdPresentacion;
                    newDetalle.Producto = preVarilla.objProducto.IdProducto;
                    newDetalle.ValorUnitario = preVarilla.Precio;
                    newDetalle.ValorTotal = newDetalle.ValorUnitario * newDetalle.Cantidad;

                    cabecera.DetallePedidos.Add(newDetalle);
                }

                #endregion

                #endregion

                #region Generacion del Gasto de Envio del Pedido

                decimal valorTansporte = 0;

                try
                {
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

                }
                catch
                {
                    ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "Transporte", "AlertaTransporte();", true);
                    return;
                }


                #endregion


                //decimal Total = Convert.ToDecimal(txtMontoGeneral.Text.Replace("$", ""));
                //decimal Total = cabecera.DetallePedidos.Sum(w => w.ValorTotal).Value - cabecera.DescuentoProvincia.Value;

                decimal Total = 0;
                foreach (DetallePedido item in cabecera.DetallePedidos)
                {
                    if (item.ValorTotal.HasValue)
                        Total += item.ValorTotal.Value;
                }

                /// Descuento del total el descuento por provincia realizado
                if (cabecera.DescuentoProvincia.HasValue)
                    Total -= cabecera.DescuentoProvincia.Value;

                cabecera.MontoTotal = Total;



                if (!FaltaSaldo && !EsTemporal)
                {
                    if (TipoCliente.ToString() != Convert.ToString((int)TipoClientes.PotencialBolso))
                    {
                        decimal SaldoActual = -1 * SaldoPagoAnticipado;
                        switch (cboFormaPago.Text)
                        {
                            case "Pago Fácil":
                                {
                                    if (Total > SaldoActual)
                                    {
                                        decimal TotalSinTransporte = Total - valorTansporte;
                                        ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "SaldoReq", "AlertaSaldoInsuficiente('El monto del pedido (Productos: $" + TotalSinTransporte.ToString() + " + Transporte: $" + valorTansporte.ToString() + ") supera el saldo disponible que posee ($" + SaldoActual.ToString() + "), el mismo no puede ser realizado hasta que tenga saldo suficiente. Si lo desea puede guardar el pedido temporalmente para realizarlo en otro momento.');", true);
                                        return;
                                    }
                                    else
                                        break;


                                }
                            case "Pago Mis Cuentas":
                                {
                                    if (Total > SaldoActual)
                                    {
                                        decimal TotalSinTransporte = Total - valorTansporte;
                                        ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "SaldoReq", "AlertaSaldoInsuficiente('El monto del pedido (Productos: $" + TotalSinTransporte.ToString() + " + Transporte: $" + valorTansporte.ToString() + ") supera el saldo disponible que posee ($" + SaldoActual.ToString() + "), el mismo no puede ser realizado hasta que tenga saldo suficiente. Si lo desea puede guardar el pedido temporalmente para realizarlo en otro momento.');", true);
                                        return;
                                    }
                                    else
                                        break;


                                }
                            case "Contra Reembolso":
                                {
                                    if ((Total - SaldoActual) > decimal.Parse(LimiteContraReembolso))
                                    {
                                        ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "SaldoReq", "AlertaSaldoInsuficiente('El monto del pedido supera el límite en contra reemboldo ($ " + LimiteContraReembolso + "), el mismo no puede ser realizado. Si lo desea puede guardar el pedido temporalmente para realizarlo en otro momento.');", true);
                                        return;
                                    }
                                    else
                                        break;

                                }
                            case "Rapi Pago":
                                {
                                    if (Total > SaldoActual)
                                    {
                                        decimal TotalSinTransporte = Total - valorTansporte;
                                        ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "SaldoReq", "AlertaSaldoInsuficiente('El monto del pedido (Productos: $" + TotalSinTransporte.ToString() + " + Transporte: $" + valorTansporte.ToString() + ") supera el saldo disponible que posee ($" + SaldoActual.ToString() + "), el mismo no puede ser realizado hasta que tenga saldo suficiente. Si lo desea puede guardar el pedido temporalmente para realizarlo en otro momento.');", true);
                                        return;
                                    }
                                    else
                                        break;


                                }
                        }
                    }
                }





                /// Grabacion
                /// significa que estoy en un pedido nuevo.
                if (idPedido == "")
                {
                    Contexto.CabeceraPedidos.InsertOnSubmit(cabecera);

                    if (EsTemporal)
                    {
                        cabecera.UltimaModificacion = DateTime.Now;
                        cabecera.EsTemporal = true;
                        cabecera.HuboFaltaSaldo = FaltaSaldo;
                        Contexto.SubmitChanges();
                        ActualizarTotalPedido(cabecera.IdCabeceraPedido);
                    }
                    else
                    {
                        RelacionarRemitosPendientes(cabecera);
                        GenerarPedidoConCredito(cabecera);
                        Contexto.SubmitChanges();
                        ActualizarTotalPedido(cabecera.IdCabeceraPedido);
                        ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "GrabacionPedido", "AlertaGrabacion('" + cabecera.IdCabeceraPedido.ToString() + "');", true);
                    }

                    LimpiarPedido();
                }
                /// Estoy editando un pedido guardado.
                else
                {
                    if (EsTemporal)
                    {
                        cabecera.UltimaModificacion = DateTime.Now;
                        cabecera.EsTemporal = true;
                        cabecera.HuboFaltaSaldo = FaltaSaldo;
                    }
                    else
                    {
                        RelacionarRemitosPendientes(cabecera);
                        GenerarPedidoConCredito(cabecera);
                        cabecera.UltimaModificacion = null;
                        cabecera.EsTemporal = null;
                        cabecera.HuboFaltaSaldo = null;
                    }

                    Contexto.SubmitChanges();
                    ActualizarTotalPedido(cabecera.IdCabeceraPedido);


                    /// Mando a imprimir, si se realizo la solicitud del pedido
                    if (EsTemporal)
                    {
                        ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "GrabacionPedidoTemp", "javascript:window.close();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "GrabacionPedido", "GrabacionFinalPendiente('" + cabecera.IdCabeceraPedido.ToString() + "');", true);
                    }
                }





            }
            catch (Exception error)
            {
                ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "Error General", "radalert('" + error.Message + "');", true);

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(upSolicitudPedido, typeof(UpdatePanel), "Error Stock", "radalert('" + mensajeStock + "',400,200,'Stock Controlado');", true);
        }
    }

    private void CargarArbolProductos()
    {
        _productos = (from prod in Contexto.Productos
                      where prod.Tipo == 'A'
                      select prod).ToList<Producto>();

        Productos = _productos;

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


        if (TipoCliente.ToString() == Convert.ToString((int)TipoClientes.PotencialBolso))
        {
            productos = from prod in _productos
                        where ((prod.Nivel < 2 && prod.ColHijos.Count > 0 && prod.Tipo == 'A')
                        || (prod.Nivel > 1 && idProductosExistente.Contains(prod.IdProducto)))
                        && prod.Descripcion.Contains("Incorpor")
                        select prod;

            /// Esto es para excluir la incorporacion nro 0
            HiddenIncorporacion0.Value = "2516700019020";
        }
        else
        {
            /// Esto es para incluir todos los productos en la busqueda
            HiddenIncorporacion0.Value = "@";
        }

        RadTreeProductos.DataSource = Helper.LINQToDataTable<Producto>(productos);
        RadTreeProductos.DataBind();

        if (TipoCliente.ToString() != Convert.ToString((int)TipoClientes.PotencialBolso))
        {
            RadTreeNode nodoPromo = new RadTreeNode("Promociones", "Promociones");
            nodoPromo.Attributes.Add("Padre", "");
            nodoPromo.Attributes.Add("Cargar", "true");
            nodoPromo.Attributes.Add("RutaImagen", "");
            RadTreeProductos.Nodes.Add(nodoPromo);
        }

    }

    private bool ControlStockValido(bool EsTemporal, out string mensajeStock)
    {
        mensajeStock = "";
        if (!EsTemporal)
        {
            List<long> productoControl = new List<long>() { 5376, 5377 };

            List<DetallePedido> ProductoStockLimitado = (from P in (Session["detPedido"] as List<DetallePedido>)
                                                         where productoControl.Contains(P.Presentacion.Value)
                                                         select P).ToList();


            //long? CantidadSolicitada = ProductoStockLimitado.Sum(w => w.Cantidad);
            if (ProductoStockLimitado.Any(w => w.Cantidad > 1))
            {
                mensajeStock = "<b>Por el momento solo se permite adquirir un máximo de 1 unidad para los siguientes productos solicitados:</b></br></br>";

                foreach (DetallePedido item in ProductoStockLimitado)
                {
                    mensajeStock += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>" + item.ProductoDesc + " " + item.PresentacionDesc + "</b> Cantidad Solicitada: " + item.Cantidad + "</br>";
                }

                //mensajeStock += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Total Solicitado: " + CantidadSolicitada.ToString() + "</b>";
                return false;
            }
            else
            {
                mensajeStock = "";
                return true;
            }
        }
        else
        {
            mensajeStock = "";
            return true;
        }
    }

    private void RelacionarRemitosPendientes(CabeceraPedido cabecera)
    {

        foreach (RemitosPendiente item in UCTotalizadorNivel.RemitosPendientesNoAfectados)
        {

            RemitosAfectados remAfe = new RemitosAfectados();
            remAfe.Cantidad = item.Cantidad;
            remAfe.Precio = item.Precio;
            remAfe.FechaRemito = item.FechaRemito;
            remAfe.CodArticulo = item.CodArticulo;
            remAfe.CodCliente = item.CodCliente;
            remAfe.DescArticulo = item.DescArticulo;
            remAfe.objCabeceraPedido = cabecera;
            remAfe.NroRemito = item.NroRemito;
            cabecera.colRemitosAfectados.Add(remAfe);
        }


    }

    private void GenerarPedidoConCredito(CabeceraPedido cabecera)
    {
        /// Si la forma de pago es en credito entonces tengo 
        /// que actualizar el valor solicitado en credito para saber si puede
        /// seguir solicitando con esta forma de pago.
        if (cabecera.FormaPago == long.Parse(FormaDePagosConstantes.CREDITO))
        {

            ConfCredito CurrentConf = (from c in Contexto.ConfCreditos
                                       where c.objCliente.IdCliente == cabecera.Cliente
                                       select c).FirstOrDefault();

            if (CurrentConf == null)
            {
                CurrentConf = (from c in Contexto.ConfCreditos
                               where c.objCliente.IdCliente == cabecera.ClienteSolicitante
                               select c).FirstOrDefault();
            }

            CurrentConf.MontoUtilizado += cabecera.MontoTotal;

            /// Luego de la actualización tengo que generar el registro del pedido en credito para
            /// saber cuando se vence.
            PedidosConCredito currentPedidoCredito = new PedidosConCredito();
            currentPedidoCredito.Cliente = cabecera.Cliente;
            currentPedidoCredito.ClienteRespCredito = CurrentConf.objCliente.IdCliente;
            currentPedidoCredito.FechaVencimiento = cabecera.FechaPedido.AddDays(CurrentConf.TiempoReposicion).Date;
            currentPedidoCredito.MontoCredito = cabecera.MontoTotal;
            currentPedidoCredito.MontoPagado = 0;
            currentPedidoCredito.objCabeceraPedido = cabecera;
            currentPedidoCredito.Procesado = false;
            Contexto.PedidosConCreditos.InsertOnSubmit(currentPedidoCredito);
            Contexto.SubmitChanges();
        }

    }


    /// <summary>
    /// Este metodo se utiliza para recalcular el monto total del pedido
    /// despues de grabar, por las dudas de errores al grabar.
    /// </summary>
    /// <param name="IdPedido"></param>
    private void ActualizarTotalPedido(long IdPedido)
    {
        Marzzan_InfolegacyDataContext NewContext = new Marzzan_InfolegacyDataContext();

        CabeceraPedido cab = (from c in NewContext.CabeceraPedidos
                              where c.IdCabeceraPedido == IdPedido
                              select c).First();

        cab.MontoTotal = cab.DetallePedidos.Sum(w => w.ValorTotal.Value);

        if (cab.DescuentoProvincia.HasValue)
            cab.MontoTotal -= cab.DescuentoProvincia.Value;

        NewContext.SubmitChanges();
    }

    private void CargarFormaDePago(Cliente CurrrentCliente)
    {
        List<FormaDePago> FormasDePago = new List<FormaDePago>();
        cboFormaPago.Items.Clear();
        cboFormaPago.DataTextField = "Descripcion";
        cboFormaPago.DataValueField = "IdFormaPago";

        if (CurrrentCliente != null)
        {
            if (UCTotalizadorNivel.PoseeRequerimientoPagoFacil)
            {
                /// Si tiene un requerimiento de pago facil
                /// quiere decir que esta penalizado y solo puede
                /// usar como forma de pago Pago Fácil o Pago Mis Cuentas
                FormasDePago = Contexto.FormaDePagos.Where(w => !w.Descripcion.Contains("Contra") && w.Cliente == 2).ToList();



            }
            else
            {
                if (CurrrentCliente.Cod_CondVta == "SIN")
                {

                    FormasDePago = (from Fp in Contexto.FormaDePagos
                                    where Fp.Cliente == 2
                                    select Fp).ToList();
                }
                else
                {
                    FormasDePago = (from Fp in Contexto.FormaDePagos
                                    where Fp.Cliente == 2
                                    && Fp.Codigo != "SIN"
                                    select Fp).ToList();
                }
            }


            /// Controlo si el cliente puede utilizar la forma de pago en crédito
            /// se controla sobre el cliente que esta logeado, que puede ser o no el destinatario
            /// de la nota de pedido.
            if ((Session["ClienteLogeado"] as Cliente).ColConfCreditos.Count > 0)
            {
                Session["MontoDisponibleCredito"] = (Session["ClienteLogeado"] as Cliente).ColConfCreditos.First().MontoAsignado - (Session["ClienteLogeado"] as Cliente).ColConfCreditos.First().MontoUtilizado;

                if ((Session["ClienteLogeado"] as Cliente).ColConfCreditos.First().Activo &&
                    decimal.Parse(Session["MontoDisponibleCredito"].ToString()) > 0)
                {
                    FormasDePago.AddRange((from Fp in Contexto.FormaDePagos
                                           where Fp.Cliente == 4
                                           select Fp).ToList());


                }
            }


            cboFormaPago.DataSource = FormasDePago;
            cboFormaPago.DataBind();
            cboFormaPago.SelectedValue = "0";
            upFormaDePago.Update();
        }
        else
        {
            cboFormaPago.DataSource = new List<FormaDePago>();
            cboFormaPago.DataBind();
            upFormaDePago.Update();
        }






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

        /// Sumo todos los productos directos
        MontoTotal = (from P in (Session["detPedido"] as List<DetallePedido>)
                      select P.ValorTotal.Value).Sum();


        /// Resto las promociones
        /// IMPORTANTE: ESTE CAMBIO ES APLICABLE DESPUES
        /// DE HABLAR CON RAUL
        //if (Session["PromosGeneradas"] != null && (Session["PromosGeneradas"] as List<DetallePedido>).Count > 0)
        //{
        //    MontoTotal -= (from P in (Session["PromosGeneradas"] as List<DetallePedido>)
        //                   where P.ColRegalos != null && P.ColRegalos.Where(w => w.TipoRegalo == "Producto").Count() == P.ColRegalos.Count 
        //                   select P.ValorTotal.Value).Sum();
        //}


        if (Session["PromosGeneradas"] != null && (Session["PromosGeneradas"] as List<DetallePedido>).Count > 0)
        {
            MontoTotalPromocionesDescuento = (from P in (Session["PromosGeneradas"] as List<DetallePedido>)
                                              where P.ColRegalos != null && P.ColRegalos.Any(w => w.TipoRegalo == "Descuento")
                                              && P.ValorTotal.HasValue
                                              select P.ValorTotal.Value).Sum();
        }

        /// Descuento de las provincias
        /// Articulos - promos + flete = total 
        DescuentoProvincia = Math.Round(((MontoTotal - MontoTotalPromocionesDescuento + ValorFlete) * PorcentajeDescuentoProvincia) / 100, 2);

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
            CalcularPromociones();
            /// vuelvo a actualizar el total del pedido
            /// ya que las promociones afectan el total.
            CalcularTotalPedido();
        }
        else
        {
            txtTotalGeneral.Text = "0";
            txtMontoGeneral.Text = "$ 0";
            lblMontoActual.Text = "$ 0";
        }

        UcTotalizadorPedidos.InitControl(0);
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
                                      && D.EsPrincipal.Value
                                      orderby D.CodigoExternoDir
                                      select D).FirstOrDefault<Direccione>();

        if (direccionIncial == null)
        {

            direccionIncial = (from D in Contexto.Direcciones
                               where D.CodigoExterno == CurrentCliente.CodigoExterno
                               orderby D.CodigoExternoDir
                               select D).FirstOrDefault<Direccione>();
        }



        if (direccionIncial != null)
        {
            IdDireccionSeleccionada = direccionIncial.IdDireccion;

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
                HiddenPoseeCartuchera.Value = "false";
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
            long idCliente = CurrentCliente.IdCliente;

            var PermitirPedido = (from c in Contexto.CabeceraPedidos
                                  where c.Cliente == idCliente
                                  select c).Count();

            if (PermitirPedido > 0)
            {
                NodoInc.ParentNode.Visible = false;
            }
            else
            {

                if (IncorporacionHistorica == null)
                    NodoInc.Visible = true;
                else
                    NodoInc.Visible = false;
            }
        }

        upTreeProductos.Update();

        #endregion

    }

    private void CargarEncabezado(Cliente CurrentCliente, long IdPedido)
    {
        Direccione dirPrincipal = null;
        List<Direccione> direcciones = (from D in Contexto.Direcciones
                                        where D.CodigoExterno == CurrentCliente.CodigoExterno
                                        orderby D.CodigoExternoDir
                                        select D).ToList<Direccione>();



        if (direcciones.Count > 0)
        {
            if (IdPedido == 0)
            {
                dirPrincipal = direcciones.Where(w => w.EsPrincipal.Value).FirstOrDefault();
            }
            else
            {
                var DirGrabada = (from D in Contexto.Direcciones
                                  where D.IdDireccion == _idDireccionGrabada
                                  select D).FirstOrDefault<Direccione>();

                if (DirGrabada != null)
                    dirPrincipal = DirGrabada;
                else
                    dirPrincipal = direcciones.First();
            }

        }

        if (dirPrincipal != null)
        {

            lblDireccionEntrega.Text = dirPrincipal.Provincia.ToLower() + " - " + dirPrincipal.Localidad.ToLower();
            lblCalle.Text = dirPrincipal.Calle.ToLower();
            lblFechaImp.Text = DateTime.Now.ToShortDateString();
            lblNroNotaImp.Text = ObtenerSeguienteNro(IdPedido, Contexto);
            IdDireccionSeleccionada = dirPrincipal.IdDireccion;

            var ultimoPedido = (from v in Contexto.View_UltimoPedidoClientes
                                where v.IdCliente == CurrentCliente.IdCliente
                                select v.UltimaFechaPedido).FirstOrDefault();

            lblUltimoPedido.Text = ultimoPedido == null ? "Sin Pedido Previo" : ultimoPedido.Value.ToShortDateString();


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
                lblTransporte.Text = "SIN TRANSPORTE";
                lblTransporteHidden.Value = "";
                lblTransporteValorHidden.Value = "0";
                lblProvinciaPorcentajeDescuentoHidden.Value = "0";
                lblDescuentoProvincia.Text = "$ 0";

            }

            CargarDatosJerarquia();

            lblToolTipNombre.Text = CurrentCliente.Nombre.ToLower();
            lblToolTipDNI.Text = CurrentCliente.Dni;
            lblToolTipTel.Text = CurrentCliente.Telefono;
            lblToolTipEmail.Text = CurrentCliente.Email;


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

    private void CargarDatosJerarquia()
    {
        Cliente CurrentCliente = (Cliente)Session["Cliente"];
        CalcularSaldoPagoAnticipado(CurrentCliente, Contexto);

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
        lblSaldoCta.Text = SaldoPagoAnticipado.ToString(); //CurrentCliente.SaldoCtaCte.ToString();

        if (CurrentCliente.UltimaActualizacion.HasValue)
            lblUltimaAct.Text = string.Format("{0:d}", CurrentCliente.UltimaActualizacion);
        else
            lblUltimaAct.Text = "";

    }


    private void LimpiarPedido()
    {

        lblResponsable.Text = "";
        lblSubCoor.Text = "";
        lblDireccionEntrega.Text = "";
        lblTransporte.Text = "";
        lblTransporteHidden.Value = "";
        lblTransporteValorHidden.Value = "0";
        lblProvinciaPorcentajeDescuentoHidden.Value = "0";

        txtObservacion.Text = "";
        lblFechaImp.Text = "";
        lblCalle.Text = "";
        lblNroNotaImp.Text = "";
        txtMontoGeneral.Text = "0";
        lblMontoActual.Text = "0";
        txtTotalGeneral.Text = "0";
        lblUltimoPedido.Text = "";

        phTotalizadorPedido.Controls.Clear();
        TotalizadorPromos1.Clear();
        UcTotalizadorPedidos.Clear();

        Session.Add("detPedido", new List<DetallePedido>());
        Session.Add("PromosGeneradas", new List<DetallePedido>());
        Session.Add("IdDireccionSeleccionada", 0);

        cboConsultores.SelectedIndex = 0;
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


        if (Session["Cliente"] != null && TipoCliente.ToString() != Convert.ToString((int)TipoClientes.PotencialBolso))
        {

            Cliente CurrentClient = (Cliente)Session["Cliente"];
            List<DetallePedido> promosGeneradasConRegalo = new List<DetallePedido>();
            List<DetallePedido> promosGeneradasConRegaloVIP = new List<DetallePedido>();
            List<DetallePedido> promosGeneradasSinRegalos = new List<DetallePedido>();
            List<DetallePedido> promosGeneradasUnaxPedido = new List<DetallePedido>();
            List<DetallePedido> AllPromosGeneradas = new List<DetallePedido>();
            List<DetallePedido> PedidoTemp = new List<DetallePedido>();
            List<DetallePedido> PedidoActual = (Session["detPedido"] as List<DetallePedido>).Where(w => w.Tipo != "P").ToList();


            #region Carga de Variables
            Helpers.HelperPromocion helper = new Helpers.HelperPromocion();

            decimal MontoActual = decimal.Parse(lblMontoProductos.Text.Replace("$", ""));

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

            #endregion

            #region Gestor de Promociones : Calculo de Promociones
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

            #endregion

            #region Generación Promociones con Solicitud Directa
            Random randomUsoInterno = new Random();
            List<DetallePedido> promosSolicitadas = (Session["detPedido"] as List<DetallePedido>).Where(w => w.Tipo == "P" || w.Tipo == "D").ToList();

            foreach (DetallePedido promoDelDetalle in promosSolicitadas)
            {
                bool generarPromociones = false;
                List<DetallePedido> promosGeneradaExistente = new List<DetallePedido>();
                if (promoDelDetalle.IdRelacionDetallePromo != 0)
                {

                    promosGeneradaExistente = (Session["PromosGeneradas"] as List<DetallePedido>).Where(w => w.IdRelacionDetallePromo == promoDelDetalle.IdRelacionDetallePromo).ToList();
                    if (promosGeneradaExistente.Count > 0)
                    {
                        if (promosGeneradaExistente.Count <= promoDelDetalle.Cantidad)
                        {
                            generarPromociones = true;
                            AllPromosGeneradas.AddRange(promosGeneradaExistente);
                        }
                        else
                        {
                            int CantidadValida = Convert.ToInt32(promoDelDetalle.Cantidad.Value);
                            AllPromosGeneradas.AddRange(promosGeneradaExistente.Take(CantidadValida).ToList());
                        }
                    }
                    else
                    {
                        generarPromociones = true;
                    }

                }
                else
                {
                    generarPromociones = true;
                }

                if (generarPromociones)
                {
                    /// Le asigno un Id Interno para poder relaciones la promo del detalle 
                    /// con la promo generada.
                    if (promoDelDetalle.IdRelacionDetallePromo == 0)
                    {
                        if (promoDelDetalle.IdDetallePedido == 0)
                            promoDelDetalle.IdRelacionDetallePromo = randomUsoInterno.Next(-1000, -1);
                        else
                            promoDelDetalle.IdRelacionDetallePromo = promoDelDetalle.IdDetallePedido;
                    }


                    List<string> descripcionPromo = new List<string>();
                    descripcionPromo.Add("|");

                    Producto promoSolicitada = (from P in Contexto.Presentacions
                                                where P.Codigo.Trim() == promoDelDetalle.CodigoCompleto
                                                select P.objProducto).First<Producto>();



                    var composicionRegalo = from R in promoSolicitada.ColComposiciones
                                            where R.TipoComposicion == "O"
                                            group R by R.Grupo into c
                                            select new { Grupo = c.Key, componentes = c };

                    for (int i = 0; i < promoDelDetalle.Cantidad - promosGeneradaExistente.Count; i++)
                    {

                        DetallePedido promoGeneradaxSolicitud = new DetallePedido();


                        /// Recupero y genero los datos para los elementos de regalo
                        /// de la promoción que se esta solicitando.
                        if (composicionRegalo.Count() > 0)
                        {

                            foreach (var itemComponente in composicionRegalo)
                            {
                                List<Producto> productos = (from P in itemComponente.componentes
                                                            select P.objProductoHijo).ToList<Producto>();

                                DetalleRegalos newRegalo = new DetalleRegalos();
                                newRegalo.DescripcionRegalo = Helper.ObtenerDescripcionCompletaProductoEnComun(productos) + " x " + itemComponente.componentes.First().objPresentacion.Descripcion;
                                newRegalo.IdPresentacionRegaloSeleccionado = 0;
                                newRegalo.TipoRegalo = "Producto";
                                newRegalo.objDetallePedido = promoGeneradaxSolicitud;
                                newRegalo.Grupo = itemComponente.componentes.First().Grupo.Value;
                                promoGeneradaxSolicitud.ColRegalos.Add(newRegalo);
                            }

                        }
                        else
                        {
                            DetalleRegalos newRegalo = new DetalleRegalos();
                            newRegalo.DescripcionRegalo = "Un descuento de $" + promoSolicitada.Precio.ToString();
                            newRegalo.IdPresentacionRegaloSeleccionado = -1;
                            newRegalo.TipoRegalo = "Descuento";
                            newRegalo.objDetallePedido = promoGeneradaxSolicitud;
                            newRegalo.Grupo = 0;
                            promoGeneradaxSolicitud.ColRegalos.Add(newRegalo);
                        }


                        /// Recupero y genero los datos para los elementos requeridos
                        /// de la promoción que se esta solicitando.
                        var composicionRequeridos = from R in promoSolicitada.ColComposiciones
                                                    where R.TipoComposicion == "C"
                                                    group R by R.Grupo into c
                                                    select new { Grupo = c.Key, componentes = c };



                        decimal valorPromocionSegunComponentes = 0;
                        bool puedeSeleccionarProductosRequeridos = false;
                        foreach (var item in composicionRequeridos)
                        {
                            if (item.componentes.Count() == 1)
                            {
                                /// Verifico si en la coleccion de elementos requeridos guardado esta este definicion
                                DetalleProductosRequeridos detExistente = promoDelDetalle.colProductosRequeridos.Where(w => w.Tipo == "Fijo"
                                    && w.IdProducto == item.componentes.FirstOrDefault().objProductoHijo.IdProducto
                                    && w.IdPresentacion == item.componentes.FirstOrDefault().objPresentacion.IdPresentacion
                                    && w.Grupo == i).FirstOrDefault();

                                if (detExistente == null || promosGeneradaExistente.Count > 0)
                                {
                                    DetalleProductosRequeridos det = new DetalleProductosRequeridos();
                                    det.Cantidad = 1;
                                    det.Tipo = "Fijo";
                                    det.objDetallePedido = promoGeneradaxSolicitud;
                                    det.IdProducto = item.componentes.FirstOrDefault().objProductoHijo.IdProducto;
                                    det.IdPresentacion = item.componentes.FirstOrDefault().objPresentacion.IdPresentacion;
                                    det.ValorUnitario = item.componentes.FirstOrDefault().objPresentacion.Precio.Value;
                                    det.CodigoCompleto = item.componentes.FirstOrDefault().objPresentacion.Codigo;
                                    det.DescripcionProducto = "<b><span style='color:Blue' >1</span></b> " + item.componentes.FirstOrDefault().objProductoHijo.Descripcion + " x " + item.componentes.FirstOrDefault().objPresentacion.Descripcion;
                                    promoGeneradaxSolicitud.colProductosRequeridos.Add(det);
                                    valorPromocionSegunComponentes += item.componentes.FirstOrDefault().objPresentacion.Precio.Value;
                                }
                                else
                                {
                                    DetalleProductosRequeridos det = new DetalleProductosRequeridos();
                                    det.Cantidad = 1;
                                    det.Tipo = "Fijo";
                                    det.objDetallePedido = promoGeneradaxSolicitud;
                                    det.IdProducto = detExistente.IdProducto;
                                    det.IdPresentacion = detExistente.IdPresentacion;
                                    det.ValorUnitario = detExistente.ValorUnitario.Value;
                                    det.CodigoCompleto = detExistente.CodigoCompleto;
                                    det.DescripcionProducto = detExistente.DescripcionProducto;
                                    promoGeneradaxSolicitud.colProductosRequeridos.Add(det);
                                    valorPromocionSegunComponentes += detExistente.ValorUnitario.Value;

                                }
                            }
                            else
                            {
                                puedeSeleccionarProductosRequeridos = true;
                                valorPromocionSegunComponentes += item.componentes.FirstOrDefault().objPresentacion.Precio.Value * int.Parse(item.componentes.FirstOrDefault().Cantidad);
                            }
                        }


                        /// Solo si hay promociones generadas y no se esta creando una nueva
                        /// busco los productos requeridos que estan guardados.
                        if (promoDelDetalle.Cantidad > 0 && promosGeneradaExistente.Count == 0)
                        {
                            /// Genero todos los productos requeridos que fueron guardado
                            List<DetalleProductosRequeridos> detalles = promoDelDetalle.colProductosRequeridos.Where(w => w.Tipo == "Dinamico" && w.Grupo == i).ToList();
                            foreach (DetalleProductosRequeridos detalle in detalles)
                            {
                                DetalleProductosRequeridos det = new DetalleProductosRequeridos();
                                det.Cantidad = detalle.Cantidad;
                                det.Tipo = "Dinamico";
                                det.objDetallePedido = promoGeneradaxSolicitud;
                                det.IdProducto = detalle.IdProducto;
                                det.IdPresentacion = detalle.IdPresentacion;
                                det.ValorUnitario = detalle.ValorUnitario.Value;
                                det.CodigoCompleto = detalle.CodigoCompleto;
                                det.DescripcionProducto = detalle.DescripcionProducto;
                                promoGeneradaxSolicitud.colProductosRequeridos.Add(det);
                            }
                        }



                        if (puedeSeleccionarProductosRequeridos)
                        {
                            promoGeneradaxSolicitud.Tipo = "PS";
                        }
                        else
                            promoGeneradaxSolicitud.Tipo = "PSD";


                        promoGeneradaxSolicitud.IdDetallePedido = randomUsoInterno.Next(-1000, -1);
                        promoGeneradaxSolicitud.IdRelacionDetallePromo = promoDelDetalle.IdRelacionDetallePromo;
                        promoGeneradaxSolicitud.Cantidad = 1;
                        promoGeneradaxSolicitud.Producto = promoSolicitada.IdProducto;
                        promoGeneradaxSolicitud.Presentacion = promoSolicitada.ColPresentaciones[0].IdPresentacion;
                        promoGeneradaxSolicitud.IdRegaloSeleccionado = -1;
                        promoGeneradaxSolicitud.ProductoDesc = "Promociones Solicitadas";
                        promoGeneradaxSolicitud.PresentacionDesc = promoSolicitada.ColPresentaciones[0].Descripcion;

                        promoGeneradaxSolicitud.DescripcionCompleta = promoGeneradaxSolicitud.ProductoDesc;
                        promoGeneradaxSolicitud.DescProductosUtilizados = descripcionPromo;
                        promoGeneradaxSolicitud.CodigoCompleto = promoSolicitada.ColPresentaciones[0].Codigo;
                        promoGeneradaxSolicitud.ValorUnitario = promoSolicitada.Precio;
                        promoGeneradaxSolicitud.ValorTotal = promoSolicitada.Precio;

                        promoDelDetalle.ValorUnitario = valorPromocionSegunComponentes;
                        promoDelDetalle.ValorTotal = promoDelDetalle.ValorUnitario * promoDelDetalle.Cantidad;

                        AllPromosGeneradas.Add(promoGeneradaxSolicitud);
                    }
                }
                //}
            }

            #endregion

            #region  Gestor de Prediccion : Generacion de Promociones Posibles
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

            #region Generacion Promociones especiales y Fijas

            /// Genero la promocion Fija que se entraga por pedido
            if (decimal.Parse(txtMontoGeneral.Text.Replace("$", "")) <= 350)
            {
                // Promo Pedido Hasta $350
                string codigoPedidoMenor = "1150000021427";

                Producto promoPedidoMenor = (from P in Contexto.Presentacions
                                             where P.Codigo.Trim() == codigoPedidoMenor
                                             select P.objProducto).First<Producto>();

                if (promoPedidoMenor.objConfPromocion != null && promoPedidoMenor.objConfPromocion.FechaInicio <= DateTime.Now && promoPedidoMenor.objConfPromocion.FechaFinal > DateTime.Now)
                {

                    List<string> descripcionPromo = new List<string>();
                    descripcionPromo.Add("Hasta|$350");

                    DetallePedido pedidoPedidoMenor = new DetallePedido();


                    var composicionRegalo = from R in promoPedidoMenor.ColComposiciones
                                            where R.TipoComposicion == "O"
                                            group R by R.Grupo into c
                                            select new { Grupo = c.Key, componentes = c };

                    if (composicionRegalo.Count() > 0)
                    {

                        // ESTE CODIGO ES EL QUE SE DEBE PONER
                        // PARA SOPORTAR MAS DE UN GRUPO EN LAS PROMOCION DE 
                        // 'PAGO FACIL', HACER LO MISMO PARA 'PAGO MIS CUENTAS'
                        foreach (var itemComponente in composicionRegalo)
                        {
                            List<Producto> productos = (from P in itemComponente.componentes
                                                        select P.objProductoHijo).ToList<Producto>();

                            DetalleRegalos newRegalo = new DetalleRegalos();
                            newRegalo.DescripcionRegalo = Helper.ObtenerDescripcionCompletaProductoEnComun(productos) + " x " + itemComponente.componentes.First().objPresentacion.Descripcion;
                            newRegalo.IdPresentacionRegaloSeleccionado = 0;
                            newRegalo.TipoRegalo = "Producto";
                            newRegalo.objDetallePedido = pedidoPedidoMenor;
                            newRegalo.Grupo = itemComponente.componentes.First().Grupo.Value;
                            pedidoPedidoMenor.ColRegalos.Add(newRegalo);
                        }


                        pedidoPedidoMenor.Cantidad = 1;
                        pedidoPedidoMenor.Producto = promoPedidoMenor.IdProducto;
                        pedidoPedidoMenor.Presentacion = promoPedidoMenor.ColPresentaciones[0].IdPresentacion;
                        pedidoPedidoMenor.ProductoDesc = promoPedidoMenor.Descripcion;
                        pedidoPedidoMenor.PresentacionDesc = promoPedidoMenor.ColPresentaciones[0].Descripcion;
                        pedidoPedidoMenor.DescripcionCompleta = pedidoPedidoMenor.ProductoDesc;
                        pedidoPedidoMenor.DescProductosUtilizados = descripcionPromo;
                        pedidoPedidoMenor.CodigoCompleto = promoPedidoMenor.ColPresentaciones[0].Codigo;
                        pedidoPedidoMenor.Tipo = "M";

                        AllPromosGeneradas.Add(pedidoPedidoMenor);
                    }

                }
            }
            else if (decimal.Parse(txtMontoGeneral.Text.Replace("$", "")) > 350)
            {
                // Promo Pedido Hasta $350
                string codigoPromoPedidoMayor = "1150000021428";

                Producto promoPedidoMayor = (from P in Contexto.Presentacions
                                             where P.Codigo.Trim() == codigoPromoPedidoMayor
                                             select P.objProducto).First<Producto>();

                if (promoPedidoMayor.objConfPromocion != null && promoPedidoMayor.objConfPromocion.FechaInicio <= DateTime.Now && promoPedidoMayor.objConfPromocion.FechaFinal > DateTime.Now)
                {

                    List<string> descripcionPromo = new List<string>();
                    descripcionPromo.Add("Mas de|$350");

                    DetallePedido pedidoPedidoMayor = new DetallePedido();


                    var composicionRegalo = from R in promoPedidoMayor.ColComposiciones
                                            where R.TipoComposicion == "O"
                                            group R by R.Grupo into c
                                            select new { Grupo = c.Key, componentes = c };

                    if (composicionRegalo.Count() > 0)
                    {

                        // ESTE CODIGO ES EL QUE SE DEBE PONER
                        // PARA SOPORTAR MAS DE UN GRUPO EN LAS PROMOCION DE 
                        // 'PAGO FACIL', HACER LO MISMO PARA 'PAGO MIS CUENTAS'
                        foreach (var itemComponente in composicionRegalo)
                        {
                            List<Producto> productos = (from P in itemComponente.componentes
                                                        select P.objProductoHijo).ToList<Producto>();

                            DetalleRegalos newRegalo = new DetalleRegalos();
                            newRegalo.DescripcionRegalo = Helper.ObtenerDescripcionCompletaProductoEnComun(productos) + " x " + itemComponente.componentes.First().objPresentacion.Descripcion;
                            newRegalo.IdPresentacionRegaloSeleccionado = 0;
                            newRegalo.TipoRegalo = "Producto";
                            newRegalo.objDetallePedido = pedidoPedidoMayor;
                            newRegalo.Grupo = itemComponente.componentes.First().Grupo.Value;
                            pedidoPedidoMayor.ColRegalos.Add(newRegalo);
                        }


                        pedidoPedidoMayor.Cantidad = 1;
                        pedidoPedidoMayor.Producto = promoPedidoMayor.IdProducto;
                        pedidoPedidoMayor.Presentacion = promoPedidoMayor.ColPresentaciones[0].IdPresentacion;
                        pedidoPedidoMayor.ProductoDesc = promoPedidoMayor.Descripcion;
                        pedidoPedidoMayor.PresentacionDesc = promoPedidoMayor.ColPresentaciones[0].Descripcion;
                        pedidoPedidoMayor.DescripcionCompleta = pedidoPedidoMayor.ProductoDesc;
                        pedidoPedidoMayor.DescProductosUtilizados = descripcionPromo;
                        pedidoPedidoMayor.CodigoCompleto = promoPedidoMayor.ColPresentaciones[0].Codigo;
                        pedidoPedidoMayor.Tipo = "M";

                        AllPromosGeneradas.Add(pedidoPedidoMayor);
                    }

                }
            }




            if (cboFormaPago.Text.Contains("Pago Fácil"))
            {
                string codigoPromoPagoFacil = "1150000021052";
                if (decimal.Parse(txtMontoGeneral.Text.Replace("$", "")) > 1650)
                {
                    codigoPromoPagoFacil = "1150000021073";
                }

                Producto promoPagoFacil = (from P in Contexto.Presentacions
                                           where P.Codigo.Trim() == codigoPromoPagoFacil
                                           select P.objProducto).First<Producto>();



                if (promoPagoFacil.objConfPromocion != null && promoPagoFacil.objConfPromocion.FechaInicio <= DateTime.Now && promoPagoFacil.objConfPromocion.FechaFinal > DateTime.Now
                   && decimal.Parse(txtMontoGeneral.Text.Replace("$", "")) >= promoPagoFacil.objConfPromocion.MontoMinimo.Value
                   && (promoPagoFacil.objConfPromocion.ColTransportistas.Count == 0 || promoPagoFacil.objConfPromocion.ColTransportistas.Any(w => w.Transporte.ToUpper() == lblTransporte.Text.ToUpper())))
                {
                    List<string> descripcionPromo = new List<string>();
                    descripcionPromo.Add("Pago|Fácil");

                    DetallePedido pedidoPagoFacil = new DetallePedido();


                    var composicionRegalo = from R in promoPagoFacil.ColComposiciones
                                            where R.TipoComposicion == "O"
                                            group R by R.Grupo into c
                                            select new { Grupo = c.Key, componentes = c };

                    if (composicionRegalo.Count() > 0)
                    {

                        // ESTE CODIGO ES EL QUE SE DEBE PONER
                        // PARA SOPORTAR MAS DE UN GRUPO EN LAS PROMOCION DE 
                        // 'PAGO FACIL', HACER LO MISMO PARA 'PAGO MIS CUENTAS'
                        foreach (var itemComponente in composicionRegalo)
                        {
                            List<Producto> productos = (from P in itemComponente.componentes
                                                        select P.objProductoHijo).ToList<Producto>();

                            DetalleRegalos newRegalo = new DetalleRegalos();
                            newRegalo.DescripcionRegalo = Helper.ObtenerDescripcionCompletaProductoEnComun(productos) + " x " + itemComponente.componentes.First().objPresentacion.Descripcion;
                            newRegalo.IdPresentacionRegaloSeleccionado = 0;
                            newRegalo.TipoRegalo = "Producto";
                            newRegalo.objDetallePedido = pedidoPagoFacil;
                            newRegalo.Grupo = itemComponente.componentes.First().Grupo.Value;
                            pedidoPagoFacil.ColRegalos.Add(newRegalo);
                        }

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
            else if (cboFormaPago.Text.Contains("Pago Mis Cuentas"))
            {
                string codigoPromoPagoMisCuenas = "1150000021207";
                if (decimal.Parse(txtMontoGeneral.Text.Replace("$", "")) > 1500)
                {
                    codigoPromoPagoMisCuenas = "1150000021208";
                }

                Producto promoPagoMisCuentas = (from P in Contexto.Presentacions
                                                where P.Codigo.Trim() == codigoPromoPagoMisCuenas
                                                select P.objProducto).First<Producto>();



                if (promoPagoMisCuentas.objConfPromocion != null && promoPagoMisCuentas.objConfPromocion.FechaInicio <= DateTime.Now && promoPagoMisCuentas.objConfPromocion.FechaFinal > DateTime.Now
                   && decimal.Parse(txtMontoGeneral.Text.Replace("$", "")) >= promoPagoMisCuentas.objConfPromocion.MontoMinimo.Value
                   && (promoPagoMisCuentas.objConfPromocion.ColTransportistas.Count == 0 || promoPagoMisCuentas.objConfPromocion.ColTransportistas.Any(w => w.Transporte.ToUpper() == lblTransporte.Text.ToUpper())))
                {
                    List<string> descripcionPromo = new List<string>();
                    descripcionPromo.Add("Pago|Mis Cuentas");

                    DetallePedido pedidoPagoFacil = new DetallePedido();


                    var composicionRegalo = from R in promoPagoMisCuentas.ColComposiciones
                                            where R.TipoComposicion == "O"
                                            group R by R.Grupo into c
                                            select new { Grupo = c.Key, componentes = c };

                    if (composicionRegalo.Count() > 0)
                    {

                        foreach (var itemComponente in composicionRegalo)
                        {
                            List<Producto> productos = (from P in itemComponente.componentes
                                                        select P.objProductoHijo).ToList<Producto>();

                            DetalleRegalos newRegalo = new DetalleRegalos();
                            newRegalo.DescripcionRegalo = Helper.ObtenerDescripcionCompletaProductoEnComun(productos) + " x " + itemComponente.componentes.First().objPresentacion.Descripcion;
                            newRegalo.IdPresentacionRegaloSeleccionado = 0;
                            newRegalo.TipoRegalo = "Producto";
                            newRegalo.objDetallePedido = pedidoPagoFacil;
                            newRegalo.Grupo = itemComponente.componentes.First().Grupo.Value;
                            pedidoPagoFacil.ColRegalos.Add(newRegalo);
                        }




                        pedidoPagoFacil.Cantidad = 1;
                        pedidoPagoFacil.Producto = promoPagoMisCuentas.IdProducto;
                        pedidoPagoFacil.Presentacion = promoPagoMisCuentas.ColPresentaciones[0].IdPresentacion;
                        pedidoPagoFacil.ProductoDesc = promoPagoMisCuentas.Descripcion;
                        pedidoPagoFacil.PresentacionDesc = promoPagoMisCuentas.ColPresentaciones[0].Descripcion;
                        pedidoPagoFacil.DescripcionCompleta = pedidoPagoFacil.ProductoDesc;
                        pedidoPagoFacil.DescProductosUtilizados = descripcionPromo;
                        pedidoPagoFacil.CodigoCompleto = promoPagoMisCuentas.ColPresentaciones[0].Codigo;
                        pedidoPagoFacil.Tipo = "E";

                        AllPromosGeneradas.Add(pedidoPagoFacil);
                    }
                }
            }
            else if (cboFormaPago.Text.Contains("Rapi Pago"))
            {
                string codigoPromoRapiPago = "1150000021516";

                if (decimal.Parse(txtMontoGeneral.Text.Replace("$", "")) > 1815)
                {
                    codigoPromoRapiPago = "1150000021518";
                }
                else if (decimal.Parse(txtMontoGeneral.Text.Replace("$", "")) > 1000)
                {
                    codigoPromoRapiPago = "1150000021517";
                }

                Producto promoRapiPago = (from P in Contexto.Presentacions
                                          where P.Codigo.Trim() == codigoPromoRapiPago
                                          select P.objProducto).First<Producto>();



                if (promoRapiPago.objConfPromocion != null && promoRapiPago.objConfPromocion.FechaInicio <= DateTime.Now && promoRapiPago.objConfPromocion.FechaFinal > DateTime.Now
                   && decimal.Parse(txtMontoGeneral.Text.Replace("$", "")) >= promoRapiPago.objConfPromocion.MontoMinimo.Value
                   && (promoRapiPago.objConfPromocion.ColTransportistas.Count == 0 || promoRapiPago.objConfPromocion.ColTransportistas.Any(w => w.Transporte.ToUpper() == lblTransporte.Text.ToUpper())))
                {
                    List<string> descripcionPromo = new List<string>();
                    descripcionPromo.Add("Rapi|Pago");

                    DetallePedido pedidoRapiPago = new DetallePedido();


                    var composicionRegalo = from R in promoRapiPago.ColComposiciones
                                            where R.TipoComposicion == "O"
                                            group R by R.Grupo into c
                                            select new { Grupo = c.Key, componentes = c };

                    if (composicionRegalo.Count() > 0)
                    {
                        foreach (var itemComponente in composicionRegalo)
                        {
                            List<Producto> productos = (from P in itemComponente.componentes
                                                        select P.objProductoHijo).ToList<Producto>();

                            DetalleRegalos newRegalo = new DetalleRegalos();
                            newRegalo.DescripcionRegalo = Helper.ObtenerDescripcionCompletaProductoEnComun(productos) + " x " + itemComponente.componentes.First().objPresentacion.Descripcion;
                            newRegalo.IdPresentacionRegaloSeleccionado = 0;
                            newRegalo.TipoRegalo = "Producto";
                            newRegalo.objDetallePedido = pedidoRapiPago;
                            newRegalo.Grupo = itemComponente.componentes.First().Grupo.Value;
                            pedidoRapiPago.ColRegalos.Add(newRegalo);
                        }




                        pedidoRapiPago.Cantidad = 1;
                        pedidoRapiPago.Producto = promoRapiPago.IdProducto;
                        pedidoRapiPago.Presentacion = promoRapiPago.ColPresentaciones[0].IdPresentacion;
                        pedidoRapiPago.ProductoDesc = promoRapiPago.Descripcion;
                        pedidoRapiPago.PresentacionDesc = promoRapiPago.ColPresentaciones[0].Descripcion;
                        pedidoRapiPago.DescripcionCompleta = pedidoRapiPago.ProductoDesc;
                        pedidoRapiPago.DescProductosUtilizados = descripcionPromo;
                        pedidoRapiPago.CodigoCompleto = promoRapiPago.ColPresentaciones[0].Codigo;
                        pedidoRapiPago.Tipo = "E";

                        AllPromosGeneradas.Add(pedidoRapiPago);
                    }
                }
            }
            else if (cboFormaPago.Text.Contains("Contra Reembolso"))
            {
                string codigoPromoContraReembolso = "1150000021244";

                Producto promoContraReembolso = (from P in Contexto.Presentacions
                                                 where P.Codigo.Trim() == codigoPromoContraReembolso
                                                 select P.objProducto).FirstOrDefault<Producto>();


                if (promoContraReembolso != null)
                {
                    decimal LimiteContrReembolso = decimal.Parse((from P in Session["ParametrosSistema"] as List<Parametro>
                                                                  where P.IdParametro == (int)TiposDeParametros.LimiteContraReembolso
                                                                  select P.Valor).Single());


                    /// Si el pedido es realizado con la forma de pago contra reembolso, 
                    /// el monto del pedido es mayor o igual al parámetro: limite de compra en contra reembolso y 
                    /// el cliente de la nota de pedido posee en ese momento un valor en la cuenta corriente a favor (en negativo)
                    /// y superior al MontoMinimo, entonces se generará para dicho pedido la promoción en cuestión
                    if (promoContraReembolso != null && promoContraReembolso.objConfPromocion != null && promoContraReembolso.objConfPromocion.FechaInicio <= DateTime.Now && promoContraReembolso.objConfPromocion.FechaFinal > DateTime.Now
                       && decimal.Parse(txtMontoGeneral.Text.Replace("$", "")) >= LimiteContrReembolso && (SaldoPagoAnticipado < 0 && Math.Abs(SaldoPagoAnticipado) >= promoContraReembolso.objConfPromocion.MontoMinimo))
                    {
                        List<string> descripcionPromo = new List<string>();
                        descripcionPromo.Add("Contra|Reembolso y Poseer un Saldo Mayor a $200 en Cta. Cte.");

                        DetallePedido pedidoPagoFacil = new DetallePedido();


                        var composicionRegalo = from R in promoContraReembolso.ColComposiciones
                                                where R.TipoComposicion == "O"
                                                group R by R.Grupo into c
                                                select new { Grupo = c.Key, componentes = c };

                        if (composicionRegalo.Count() > 0)
                        {
                            foreach (var itemComponente in composicionRegalo)
                            {
                                List<Producto> productos = (from P in itemComponente.componentes
                                                            select P.objProductoHijo).ToList<Producto>();

                                DetalleRegalos newRegalo = new DetalleRegalos();
                                newRegalo.DescripcionRegalo = Helper.ObtenerDescripcionCompletaProductoEnComun(productos) + " x " + itemComponente.componentes.First().objPresentacion.Descripcion;
                                newRegalo.IdPresentacionRegaloSeleccionado = 0;
                                newRegalo.TipoRegalo = "Producto";
                                newRegalo.objDetallePedido = pedidoPagoFacil;
                                newRegalo.Grupo = itemComponente.componentes.First().Grupo.Value;
                                pedidoPagoFacil.ColRegalos.Add(newRegalo);
                            }




                            pedidoPagoFacil.Cantidad = 1;
                            pedidoPagoFacil.Producto = promoContraReembolso.IdProducto;
                            pedidoPagoFacil.Presentacion = promoContraReembolso.ColPresentaciones[0].IdPresentacion;
                            pedidoPagoFacil.ProductoDesc = promoContraReembolso.Descripcion;
                            pedidoPagoFacil.PresentacionDesc = promoContraReembolso.ColPresentaciones[0].Descripcion;
                            pedidoPagoFacil.DescripcionCompleta = pedidoPagoFacil.ProductoDesc;
                            pedidoPagoFacil.DescProductosUtilizados = descripcionPromo;
                            pedidoPagoFacil.CodigoCompleto = promoContraReembolso.ColPresentaciones[0].Codigo;
                            pedidoPagoFacil.Tipo = "E";

                            AllPromosGeneradas.Add(pedidoPagoFacil);
                        }
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

        }

    }

    private void TotalizadorPedidos_LineaPedidoEliminada()
    {
        ActualizarTotalesGenerales();
        CalcularPromociones();
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

        Marzzan_InfolegacyDataContext dc = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];


        Cliente CurrentConsultor = (from D in dc.Clientes
                                    where D.IdCliente == long.Parse(IdConsultor)
                                    select D).First<Cliente>();

        CalcularSaldoPagoAnticipado(CurrentConsultor, dc);

        Direccione direccionIncial = (from D in dc.Direcciones
                                      where D.CodigoExterno == CurrentConsultor.CodigoExterno
                                      && D.EsPrincipal.Value
                                      orderby D.CodigoExternoDir
                                      select D).FirstOrDefault<Direccione>();

        if (direccionIncial == null)
        {

            direccionIncial = (from D in dc.Direcciones
                               where D.CodigoExterno == CurrentConsultor.CodigoExterno
                               orderby D.CodigoExternoDir
                               select D).FirstOrDefault<Direccione>();
        }


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
                Transporte = "SIN TRANSPORTE";



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


            var ultimoPedido = (from v in dc.View_UltimoPedidoClientes
                                where v.IdCliente == CurrentConsultor.IdCliente
                                select v.UltimaFechaPedido).FirstOrDefault();

            SubConsultor = CurrentConsultor.Clasif1;
            retorno = dirEntrega + "|" + Transporte + "|" + Coordinador + "@" + SubConsultor;
            retorno += "|" + DateTime.Now.ToShortDateString() + "|" + ObtenerSeguienteNro(0, dc);
            retorno += "|" + ((decimal)HttpContext.Current.Session["SaldoAnticipado"]).ToString() + "|" + ultimaActualizacion;
            retorno += "|" + (ultimoPedido == null ? "Sin Pedido Previo" : ultimoPedido.Value.ToShortDateString());

            return retorno;
        }
        else
        {
            return "";
        }
    }

    #endregion

}