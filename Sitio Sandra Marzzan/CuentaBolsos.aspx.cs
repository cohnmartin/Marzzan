using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CommonMarzzan;

public partial class CuentaBolsos : BasePage
{
    string UltimoAgrupador = "";

    private string ObtenerSeguienteNro(string Tipo)
    {

        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        try
        {
            var ultimoNro = (from D in dc.CabeceraPedidos
                             where D.TipoPedido == Tipo
                             select Convert.ToInt32(D.Nro)).Max<int>();

            return Convert.ToString(long.Parse(ultimoNro.ToString()) + 1);
        }
        catch
        {
            return "1";
        }


    }

    protected override void PageLoad()
    {
        if (!IsPostBack)
        {
            Session.Add("ListaComponente", null);

            Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

            var cliente = (from C in dc.Clientes
                           where C.IdCliente == long.Parse(Session["IdUsuario"].ToString())
                           select C).Single<Cliente>();

            if (cliente.objPadre!= null)
            lblLider.Text = cliente.objPadre.Nombre;
            else
                lblLider.Text = cliente.Nombre;
            txtDestinatario.Text = Session["NombreUsuario"].ToString();
            CargarDirecciones(cliente.Dni);

            
            /// Las incorporaciones Nº 3,4,5 se sacan de la visualización según el
            /// pedido del presupuesto 0023 - Pto 4, para que los consultores no la puedan solicitar.
            /// 03/01/2013: solo se muestran las incorporaciones visibles.
            var AllIncorporaciones = from prod in dc.Productos
                                     where prod.Tipo == 'A' && prod.Descripcion.StartsWith("Incorpora") &&
                                      prod.Nivel == 3 
                                      && prod.ColPresentaciones.Any(w => w.Activo.Value == true)
                                      //&& !prod.Descripcion.Contains("Cánepa") 
                                      //&& !(prod.Descripcion.Contains("Nº 3") || prod.Descripcion.Contains("Nº 4") || prod.Descripcion.Contains("Nº 5"))
                               select prod;

            cboIncoporaciones.AppendDataBoundItems = true;
            cboIncoporaciones.Items.Add(new RadComboBoxItem("Seleccione una Incorporación", "0"));
            cboIncoporaciones.DataTextField = "Descripcion";
            cboIncoporaciones.DataValueField = "IdProducto";
            cboIncoporaciones.DataSource = AllIncorporaciones;
            cboIncoporaciones.DataBind();
            cboIncoporaciones.SelectedIndex = 0;


            cboFormaPago.AppendDataBoundItems = true;
            cboFormaPago.Items.Add(new RadComboBoxItem("Seleccione la Forma de Pago", "0"));
            cboFormaPago.DataTextField = "Descripcion";
            cboFormaPago.DataValueField = "IdFormaPago";

            if (cliente.CodTipoCliente == TipoClientesConstantes.DIRECTOR)

                cboFormaPago.DataSource = from Fp in dc.FormaDePagos
                                          where Fp.Cliente == 2
                                          select Fp;

            else

                cboFormaPago.DataSource = from Fp in dc.FormaDePagos
                                          where Fp.Cliente == 2
                                          && (Fp.IdFormaPago == long.Parse(FormaDePagosConstantes.CONCONRTAREEMBOLSO) || Fp.IdFormaPago == long.Parse(FormaDePagosConstantes.PAGOFACIL))
                                          select Fp;

            cboFormaPago.DataBind();
            cboFormaPago.SelectedIndex = 0;


            cboDirecciones.AppendDataBoundItems = true;
            cboDirecciones.Items.Add(new RadComboBoxItem("Seleccione una Dirección", "0"));
        }
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("Inicio.aspx");
    }

    protected void btnAgregarDefinicion_Click(object sender, EventArgs e)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        var composicionRegalo = from C in dc.Composicions
                                where C.ComponentePricipal == long.Parse(cboIncoporaciones.SelectedValue)
                                group C by C.Grupo into p
                                select new { Grupo = p.Key, componentes = p };

        List<ComponentesRegalo> listaComponente =null;
        if (Session["ListaComponente"] == null)
            listaComponente = new List<ComponentesRegalo>();
        else
            listaComponente = Session["ListaComponente"] as List<ComponentesRegalo>;


        int numeroRandom = new Random().Next();

        for (int j = 0; j < txtCantidad.Value; j++)
        {
            numeroRandom = new Random().Next();
            foreach (var item in composicionRegalo)
            {
                ComponentesRegalo compReg = new ComponentesRegalo();

                if (item.componentes.First<Composicion>().TipoComposicion == "T")
                {
                    List<Producto> productos = (from P in item.componentes
                                                select P.objProductoHijo).ToList<Producto>();


                    if (productos.Count > 1)
                    {
                        compReg.Descripcion = Helper.ObtenerDescripcionCompletaProductoEnComun(productos);
                        compReg.FraganciaSeleccionada = " todas las fragancia ";
                    }
                    else
                    {
                        compReg.Descripcion = productos[0].Descripcion;
                        compReg.FraganciaSeleccionada = "";

                    }


                    compReg.Presentacion = item.componentes.First<Composicion>().objPresentacion.Descripcion;
                    compReg.CodigoSeleccionado = item.componentes.First<Composicion>().objPresentacion.Codigo;
                    compReg.Precio = item.componentes.First<Composicion>().objPresentacion.Precio.Value;
                    compReg.IdPresentacion = item.componentes.First<Composicion>().objPresentacion.IdPresentacion;
                    compReg.IdProducto = item.componentes.First<Composicion>().objProductoHijo.IdProducto;
                    compReg.Fragancias = "";
                    compReg.Codigos = "";
                    compReg.Grupo = item.Grupo.ToString();
                    compReg.EsFraganciaSeleccionable = false;
                    compReg.Agrupador = numeroRandom; //item.Grupo.Value + new Random().Next();
                    compReg.DescripcionAgrupador = cboIncoporaciones.SelectedItem.Text;
                    compReg.ComponentePrincipal = long.Parse(cboIncoporaciones.SelectedItem.Value);

                    listaComponente.Add(compReg);
                }
                else if (item.componentes.First<Composicion>().TipoComposicion == "U")
                {

                    for (int i = 0; i < int.Parse(item.componentes.First<Composicion>().Cantidad); i++)
                    {
                        compReg = new ComponentesRegalo();

                        List<Producto> productos = (from P in item.componentes
                                                    select P.objProductoHijo).ToList<Producto>();

                        compReg.Descripcion = Helper.ObtenerDescripcionCompletaProductoEnComun(productos);

                        int CatidadPresentaciones = (from F in item.componentes
                                                     select F.objPresentacion.Descripcion).Distinct().Count();

                        if (CatidadPresentaciones == 1)
                        {
                            compReg.Presentacion = " x " + item.componentes.First<Composicion>().objPresentacion.Descripcion;
                        }
                        else
                        {
                            compReg.Presentacion = "";
                        }
                        
                        
                        compReg.Precio = item.componentes.First<Composicion>().objPresentacion.Precio.Value;
                        compReg.IdProducto = item.componentes.First<Composicion>().objProductoHijo.IdProducto;
                        compReg.IdPresentacion = item.componentes.First<Composicion>().objPresentacion.IdPresentacion;
                        compReg.CodigoSeleccionado = "";
                        compReg.Grupo = item.Grupo.ToString() + i.ToString();
                        compReg.EsFraganciaSeleccionable = true;
                        compReg.Agrupador = numeroRandom; //item.Grupo.Value + numeroRandom;
                        compReg.DescripcionAgrupador = cboIncoporaciones.SelectedItem.Text;
                        compReg.ComponentePrincipal = long.Parse(cboIncoporaciones.SelectedItem.Value);


                        foreach (Composicion comp in item.componentes)
                        {
                            if (comp.objPresentacion.Activo.Value)
                            {
                                compReg.Fragancias += comp.objProductoHijo.objPadre.Descripcion + " " + comp.objProductoHijo.Descripcion + " x " + comp.objPresentacion.Descripcion + "|";
                                compReg.Codigos += comp.objPresentacion.Codigo + "|";
                            }

                        }

                        listaComponente.Add(compReg);
                    }


                }


            }
        }

        Session["ListaComponente"] = listaComponente;
        dlDetalleRegalo.DataSource = listaComponente;
        dlDetalleRegalo.DataBind();
        upRegalo.Update();
    }
   
    protected void btnRealizarRegalo_Click(object sender, EventArgs e)
    {
        /// tengo que generar un remito para el consultor, donde el detalle es el producto regalo
        /// seleccionado, y una no de pedido para el consultor seleccionado donde el detalle son los
        /// producto que componen el producto regalo.

        if (Session["ListaComponente"] == null || (Session["ListaComponente"] as List<ComponentesRegalo>).Count == 0)
        {
            toolTipFragancias.Text = "No hay ninguna incorporación agregada, para realizar la solicitud. Por favor agregue una o mas incorporaciones.";
            toolTipFragancias.Show();
            UpdatePanel1.Update();
            return;
        }
        foreach (ComponentesRegalo det in (Session["ListaComponente"] as List<ComponentesRegalo>))
        {
            if (det.FraganciaSeleccionada == null )
            {
                toolTipFragancias.Text = "Hay productos que no tienen seleccionada la fragancia. Por favor verifique los datos.";
                toolTipFragancias.Show();
                UpdatePanel1.Update();
                return;
            }
        }


       


        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        var cliente = (from C in dc.Clientes
                       where C.IdCliente == long.Parse(Session["IdUsuario"].ToString())
                       select C).Single<Cliente>();

        Cliente clienteCtaBolsos = null;


        clienteCtaBolsos = (from C in dc.Clientes
                            where C.Dni == cliente.Dni && C.Nombre.Contains("bolsos")
                            select C).SingleOrDefault<Cliente>();

        if (clienteCtaBolsos != null)
        {

            // Incorporaciones de la cuenta bolsos mas las incorporaciones
            // pedidas por los consultores que tienen a cargo
            int CantidadIncorporacionesActuales = clienteCtaBolsos.CantIncSolicitadas.Value;

            CantidadIncorporacionesActuales += int.Parse((from C in Helper.ObtenerConsultoresSubordinados(cliente)
                                                where C.FechaAlta != null
                                                select C.CantIncSolicitadas).Sum().ToString());

            // cantidad de incorporaciones que se esta pidiendo en el pedido actual
            int CantidadSolicitada = (from C in Session["ListaComponente"] as List<ComponentesRegalo>
                                      select C.Agrupador).Distinct().Count();

            /// Cantidad de consultores a cargo
            int ConsultorACargo = (from C in Helper.ObtenerConsultoresSubordinados(cliente)
                                   where C.FechaAlta != null
                                   select C).Count();

            string LimiteBolsos = (from P in Session["ParametrosSistema"] as List<Parametro>
                                           where P.IdParametro == (int)TiposDeParametros.LimiteCtaBolsos
                                           select P.Valor).Single();

            
            string LimiteBolsosCanepa = (from P in Session["ParametrosSistema"] as List<Parametro>
                                           where P.IdParametro == (int)TiposDeParametros.LimiteCtaBolsoCanepa
                                           select P.Valor).Single();


            // Validación de cantidades a pedir
            int BolsosLimites = int.Parse(LimiteBolsos);
            if (cliente.CodigoExterno == "003796")
                BolsosLimites = int.Parse(LimiteBolsosCanepa);

            if (CantidadIncorporacionesActuales + CantidadSolicitada > ConsultorACargo + BolsosLimites)
            {
                toolTipFragancias.Text = "La cantidad de incorporaciones que esta solicitando supera la disponibiliad que posee, por favor tome contacto con el personal de asistencia";
                toolTipFragancias.Show();
                UpdatePanel1.Update();
                return;
            }
        }
        else
        {
            toolTipFragancias.Text = "Usted no posee una cuenta bolsos creada para realizar la solicitud actual, por favor tome contacto con el personal de asistencia";
            toolTipFragancias.Show();
            UpdatePanel1.Update();
            return;
        }


        /// 1º Genero la Cabecera del Pedido
        decimal TotalPedido = 0;
        CabeceraPedido cabecera = new CabeceraPedido();
        cabecera.Cliente = clienteCtaBolsos.IdCliente;
        cabecera.ClienteSolicitante = clienteCtaBolsos.IdCliente;
        cabecera.DireccionEntrega = long.Parse(cboDirecciones.SelectedValue);
        cabecera.FechaPedido = DateTime.Now;
        cabecera.FormaPago = long.Parse(cboFormaPago.SelectedValue);
        cabecera.MontoTotal = 0;
        cabecera.Retira = "A RETIRAR POR: " + txtDestinatario.Text;
        cabecera.Nro = ObtenerSeguienteNro("NP");
        cabecera.TipoPedido = "NP";
        cabecera.Impreso = false;
        cabecera.NroImpresion = 0;




        ///2º Genero El detalle con la Incoporacion y el descuento de los producto
        /// que lleva la promoción
        var composicionPedido = from C in Session["ListaComponente"] as List<ComponentesRegalo>
                                group C by C.Agrupador into p
                                select new { Grupo = p.Key, componentes = p };

        DetallePedido newDetalle = null;
        
        foreach (var item in composicionPedido)
        {
            long idcompoP = item.componentes.First<ComponentesRegalo>().ComponentePrincipal;
            
            Producto TipoIncorporacion = (from Cp in dc.Productos
                                         where Cp.IdProducto == idcompoP
                                         select Cp).First<Producto>();


            ///2º1º Genero el detalle de la incorporacion
            newDetalle = new DetallePedido();
            newDetalle.Cantidad = 1;
            newDetalle.CodigoCompleto = TipoIncorporacion.ColPresentaciones[0].Codigo;
            newDetalle.Presentacion = TipoIncorporacion.ColPresentaciones[0].IdPresentacion;
            newDetalle.Producto = TipoIncorporacion.IdProducto;
            newDetalle.ValorUnitario = TipoIncorporacion.ColPresentaciones[0].Precio.Value;
            newDetalle.ValorTotal = newDetalle.ValorUnitario * newDetalle.Cantidad;
            TotalPedido += newDetalle.ValorTotal.Value;
            cabecera.DetallePedidos.Add(newDetalle);

           
            ///2º2º Genero el detalle del Descuento de la incorporacion
            var CompDescuentos = from C in dc.Composicions
                                         where C.ComponentePricipal.Value == TipoIncorporacion.IdProducto
                                         && C.TipoComposicion == "D"
                                         select C;


            foreach (Composicion itemDesc in CompDescuentos)
            {
                newDetalle = new DetallePedido();
                newDetalle.Cantidad = 1;
                newDetalle.CodigoCompleto = itemDesc.objProductoHijo.ColPresentaciones[0].Codigo;
                newDetalle.Presentacion = itemDesc.objProductoHijo.ColPresentaciones[0].IdPresentacion;
                newDetalle.Producto = itemDesc.objProductoHijo.IdProducto;
                newDetalle.ValorUnitario = -1 * itemDesc.objProductoHijo.ColPresentaciones[0].Precio.Value;
                newDetalle.ValorTotal = newDetalle.ValorUnitario * newDetalle.Cantidad;
                TotalPedido += newDetalle.ValorTotal.Value;
                cabecera.DetallePedidos.Add(newDetalle);

            }

           


            ///3º Genero cada uno de los detalles que componen la incorporación
            /// segun la configuracion de composiciones.
            foreach (ComponentesRegalo itemComponente in item.componentes)
            {
                if (itemComponente.FraganciaSeleccionada.Contains("todas las") || itemComponente.FraganciaSeleccionada == "")
                {
                    var Comp = from C in dc.Composicions
                                         where C.ComponentePricipal.Value == TipoIncorporacion.IdProducto
                                         && C.TipoComposicion == "T" && C.Grupo == long.Parse(itemComponente.Grupo)
                                         select C;

                    foreach (Composicion itemComp in Comp)
                    {
                        newDetalle = new DetallePedido();
                        newDetalle.Cantidad = 1;
                        newDetalle.CodigoCompleto = itemComp.objProductoHijo.ColPresentaciones[0].Codigo;
                        newDetalle.Presentacion = itemComp.objProductoHijo.ColPresentaciones[0].IdPresentacion;
                        newDetalle.Producto = itemComp.objProductoHijo.IdProducto;
                        newDetalle.ValorUnitario = itemComp.objProductoHijo.ColPresentaciones[0].Precio.Value;
                        newDetalle.ValorTotal = newDetalle.ValorUnitario * newDetalle.Cantidad;
                        TotalPedido += newDetalle.ValorTotal.Value;
                        cabecera.DetallePedidos.Add(newDetalle);
                        
                    }

                }
                else
                {

                    Presentacion PresentacionSeleccionado = (from P in dc.Presentacions
                                                             where P.Codigo == itemComponente.CodigoSeleccionado
                                                             select P).First<Presentacion>();


                    newDetalle = new DetallePedido();
                    newDetalle.Cantidad = 1;
                    newDetalle.CodigoCompleto = itemComponente.CodigoSeleccionado;
                    newDetalle.Presentacion = PresentacionSeleccionado.IdPresentacion;
                    newDetalle.Producto = PresentacionSeleccionado.objProducto.IdProducto;
                    newDetalle.ValorUnitario = PresentacionSeleccionado.Precio;
                    newDetalle.ValorTotal = newDetalle.ValorUnitario * newDetalle.Cantidad;
                    TotalPedido += newDetalle.ValorTotal.Value;
                    cabecera.DetallePedidos.Add(newDetalle);
                }

            }


        }


        ///4º Generacion del Gasto de Envio del Pedido (Flete)
        Direccione dirEntrega = (from D in dc.Direcciones
                                 where D.IdDireccion == long.Parse(cboDirecciones.SelectedValue)
                                 select D).First<Direccione>();

        ConfTransporte confT = (from C in dc.ConfTransportes
                                where C.Provincia == dirEntrega.Provincia &&
                                C.Localidad == dirEntrega.Localidad &&
                                C.FormaDePago == cboFormaPago.SelectedItem.Text
                                select C).SingleOrDefault<ConfTransporte>();

        if (confT != null)
        {

            newDetalle = new DetallePedido();
            newDetalle.Cantidad = 1;
            newDetalle.CodigoCompleto = confT.objProducto.ColPresentaciones[0].Codigo;
            newDetalle.Presentacion = confT.objProducto.ColPresentaciones[0].IdPresentacion;
            newDetalle.Producto = confT.objProducto.IdProducto;
            newDetalle.ValorUnitario = confT.objProducto.ColPresentaciones[0].Precio;
            newDetalle.ValorTotal = newDetalle.ValorUnitario * newDetalle.Cantidad;
            TotalPedido += newDetalle.ValorTotal.Value;
            cabecera.DetallePedidos.Add(newDetalle);

        }
        else
        {
            toolTipFragancias.Text = "Existe un problema con dirección de entraga y el costo del flete, por favor comuniquese con el personal de asistencia.";
            toolTipFragancias.Show();
            UpdatePanel1.Update();
            return;
        
        }

        cabecera.MontoTotal = TotalPedido;
        dc.CabeceraPedidos.InsertOnSubmit(cabecera);
        dc.SubmitChanges();

        cboFormaPago.SelectedIndex = 0;
        cboIncoporaciones.SelectedIndex = 0;
        cboDirecciones.Items.Clear();
        cboDirecciones.AppendDataBoundItems = true;
        cboDirecciones.Items.Add(new RadComboBoxItem("Seleccione una Dirección", "0"));
        dlDetalleRegalo.Items.Clear();
        dlDetalleRegalo.DataSource = null;
        dlDetalleRegalo.DataBind();
        upDirecciones.Update();
        upFormaDePago.Update();
        upRegalo.Update();
        upRegalos.Update();

        toolTipFragancias.Title = "Solicitud";
        toolTipFragancias.Text = "La solicitud para la cuenta bolsos se ha realizado con exito.";
        toolTipFragancias.Show();
        UpdatePanel1.Update();
        dc.Dispose();

    }

    protected void dlDetalleRegalo_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            if (((e.Item as ListViewDataItem).DataItem as ComponentesRegalo).CodigoSeleccionado != "" &&
                ((e.Item as ListViewDataItem).DataItem as ComponentesRegalo).EsFraganciaSeleccionable == false)
                e.Item.FindControl("tdSeleccionFragancia").Visible = false;
            else
            {
                (e.Item.FindControl("btnSeleccionarFragancia") as ImageButton).Attributes.Add("Codigos", ((e.Item as ListViewDataItem).DataItem as ComponentesRegalo).Codigos);
                (e.Item.FindControl("btnSeleccionarFragancia") as ImageButton).Attributes.Add("Fragancias", ((e.Item as ListViewDataItem).DataItem as ComponentesRegalo).Fragancias);
                (e.Item.FindControl("btnSeleccionarFragancia") as ImageButton).Attributes.Add("Grupo", ((e.Item as ListViewDataItem).DataItem as ComponentesRegalo).Grupo);
                (e.Item.FindControl("btnSeleccionarFragancia") as ImageButton).Attributes.Add("Agrupador", ((e.Item as ListViewDataItem).DataItem as ComponentesRegalo).Agrupador.ToString());

            }
        }
    }

    public void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

        if (e.Argument != "undefined")
        {
            string[] datos = e.Argument.ToString().Split('@');

            if (datos.Length > 1)
            {

                string codigo = datos[0];
                string grupo = datos[1];
                string fragancia = datos[2];
                long agrupador = long.Parse(datos[3]);

                ComponentesRegalo comp = (from C in (Session["ListaComponente"] as List<ComponentesRegalo>)
                                          where C.Grupo == grupo && C.Agrupador == agrupador
                                          select C).First<ComponentesRegalo>();

                comp.CodigoSeleccionado = codigo;

                Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
                Presentacion PreSel = (from P in dc.Presentacions
                                       where P.Codigo == codigo
                                       select P).First();

                comp.FraganciaSeleccionada = fragancia;
                comp.Descripcion = Helper.ObtenerDescripcionCompletaProducto(PreSel.objProducto) + " x " + PreSel.Descripcion;
                comp.Presentacion = "";

                dlDetalleRegalo.DataSource = Session["ListaComponente"];
                dlDetalleRegalo.DataBind();
                upRegalo.Update();
            }
            else
            {
                long agrupador = long.Parse(datos[0]);

                List<ComponentesRegalo> comps = (from C in (Session["ListaComponente"] as List<ComponentesRegalo>)
                                          where C.Agrupador == agrupador
                                          select C).ToList<ComponentesRegalo>();

                foreach (ComponentesRegalo comp in comps)
                {
                    (Session["ListaComponente"] as List<ComponentesRegalo>).Remove(comp);

                }
                
                dlDetalleRegalo.DataSource = Session["ListaComponente"];
                dlDetalleRegalo.DataBind();
                upRegalo.Update();
            }

        }
    }

    public string AddGroupingRow()
    {
        string AgrupadorActual = Eval("Agrupador").ToString();
        string descripcion = Eval("DescripcionAgrupador").ToString();
        if (UltimoAgrupador != AgrupadorActual)
        {
            UltimoAgrupador = AgrupadorActual;

            return String.Format("<tr><td style='width: 5%'><img alt='Eliminar' style='cursor:hand' onclick='EliminarGrupo(" + AgrupadorActual.ToString() + ");' src='Imagenes/Eliminar.png' height='16px' /></td><td>{0}</td></tr>", descripcion);
        }
        else
            return "";

    }

    private void CargarDirecciones(string dni)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

     
        Cliente clienteCtaBolsos = (from C in dc.Clientes
                                    where C.Dni == dni && C.Nombre.Contains("bolsos")
                                    select C).SingleOrDefault<Cliente>();


        cboDirecciones.Items.Clear();
        cboDirecciones.AppendDataBoundItems = true;


        if (clienteCtaBolsos != null)
        {
            cboDirecciones.Items.Add(new RadComboBoxItem("Seleccione una Dirección", "0"));
            cboDirecciones.DataTextField = "Calle";
            cboDirecciones.DataValueField = "IdDireccion";
            cboDirecciones.DataSource = clienteCtaBolsos.ColDirecciones;
            cboDirecciones.DataBind();
            cboDirecciones.SelectedIndex = 0;
        }
        else
        {
            cboDirecciones.Items.Add(new RadComboBoxItem("Usted no tiene una cuenta bolsos", "0"));
            cboDirecciones.SelectedIndex = 0;
        }

    }

    private class ComponentesRegalo
    {
        string _descripcion;
        string _presentacion;
        string _codigoSeleccionado;
        string _codigos;
        string _fragancias;
        string _grupo;
        string _fraganciaSeleccionada;
        bool _esFraganciaSeleccionable = false;
        decimal _precio;
        long _idProducto;
        long _idPresentacion;
        long _agrupador;
        string _descripcionAgrupador;
        long _componentePrincipal;


        public long ComponentePrincipal
        {
            get { return _componentePrincipal; }
            set { _componentePrincipal = value; }
        }

        public long Agrupador
        {
            get { return _agrupador; }
            set { _agrupador = value; }
        }

        public string DescripcionAgrupador
        {
            get { return _descripcionAgrupador; }
            set { _descripcionAgrupador = value; }
        }

        public decimal Precio
        {
            get { return _precio; }
            set { _precio = value; }
        }
        public long IdProducto
        {
            get { return _idProducto; }
            set { _idProducto = value; }
        }
        public long IdPresentacion
        {
            get { return _idPresentacion; }
            set { _idPresentacion = value; }
        }

        public bool EsFraganciaSeleccionable
        {
            get { return _esFraganciaSeleccionable; }
            set { _esFraganciaSeleccionable = value; }
        }

        public string FraganciaSeleccionada
        {
            get { return _fraganciaSeleccionada; }
            set { _fraganciaSeleccionada = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public string Presentacion
        {
            get { return _presentacion; }
            set { _presentacion = value; }
        }

        public string CodigoSeleccionado
        {
            get { return _codigoSeleccionado; }
            set { _codigoSeleccionado = value; }
        }

        public string Codigos
        {
            get { return _codigos; }
            set { _codigos = value; }
        }

        public string Fragancias
        {
            get { return _fragancias; }
            set { _fragancias = value; }
        }

        public string Grupo
        {
            get { return _grupo; }
            set { _grupo = value; }
        }
    }
}
