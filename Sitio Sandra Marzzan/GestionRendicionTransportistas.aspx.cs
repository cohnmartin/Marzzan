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
using CommonMarzzan;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.Script.Services;
using System.Data.OleDb;

public partial class GestionRendicionTransportistas : BasePage
{
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

    public long IdRendicion
    {
        get
        {
            return long.Parse(Session["IdRendicion"].ToString());
        }
        set
        {
            Session["IdRendicion"] = value;
        }

    }

    public string EstadoRendicion
    {
        get;
        set;

    }

    public decimal? SaldoTransportista
    {
        get;
        set;

    }

    public bool EsTransportista
    {

        get
        {
            if (Session["Transporte"] == null || Session["Transporte"] == "")
                Session["EsTransportista"] = false;
            else
                Session["EsTransportista"] = true;

            return (bool)Session["EsTransportista"];
        }

    }

    public string TipoRendicion
    {

        get
        {
            return HttpContext.Current.Request.QueryString["Tipo"].ToString();
        }

    }

    public bool TieneRolLogistica
    {

        get
        {
            return (bool)Session["TieneRolLogistica"];
        }

    }

    public bool TieneRolCobranza
    {

        get
        {
            return (bool)Session["TieneRolCobranza"];
        }

    }

    protected override void PageLoad()
    {
        if (!IsPostBack)
        {
            IdRendicion = HttpContext.Current.Request.QueryString["IdRendicion"] != null ? long.Parse(HttpContext.Current.Request.QueryString["IdRendicion"].ToString()) : 0;
            Session["Context"] = new Marzzan_InfolegacyDataContext();
            Session["TipoRendicion"] = HttpContext.Current.Request.QueryString["Tipo"] != null ? HttpContext.Current.Request.QueryString["Tipo"].ToString() : "";
            Session["DescTipoRendicion"] = HttpContext.Current.Request.QueryString["DescRendicion"];

            lblTituloRendicion.Text += "Tipo Rendición: " + Session["DescTipoRendicion"].ToString();

            string transporteLogin = Session["Transporte"] == null ? "" : Session["Transporte"].ToString().ToUpper();
            long idUsuarioTransporte = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());


            lblDescripcionMensaje.InnerText = "NO EXISTEN GUIAS DEL TIPO '" + HttpContext.Current.Request.QueryString["DescRendicion"] + "' PARA REALIZAR LA RENDICION";

            cboTransportes.DataTextField = "data";
            cboTransportes.DataValueField = "value";
            cboTransportes.AppendDataBoundItems = true;
            cboTransportes.DataSource = (from t in Contexto.Clientes
                                         where t.Nombre.ToUpper().Contains(transporteLogin) && t.TipoCliente == "TRANSPORTISTA"
                                         orderby t.Nombre
                                         select new
                                         {
                                             data = t.Nombre,
                                             value = t.Nombre
                                         }).ToList();
            cboTransportes.DataBind();


            cboTransporteDevolucion.DataTextField = "data";
            cboTransporteDevolucion.DataValueField = "value";
            cboTransporteDevolucion.AppendDataBoundItems = true;
            cboTransporteDevolucion.DataSource = (from t in Contexto.View_Transportes
                                                  orderby t.Transporte
                                                  select new
                                                  {
                                                      data = t.Transporte,
                                                      value = t.Transporte
                                                  }).ToList();
            cboTransporteDevolucion.DataBind();


            /// Recurpero la rendicion que se esta editando
            CabeceraRendicion cabEdit = (from c in Contexto.CabeceraRendicions
                                         where c.IdCabeceraRendicion == IdRendicion
                                         select c).FirstOrDefault();

            if (cabEdit != null)
            {
                EstadoRendicion = cabEdit.objEstado.Valor;
                if (cabEdit.objTipoRendicion.Contexto == "D")
                {
                    txtFechaEnvioDevolucion.SelectedDate = cabEdit.FechaDevolucion;
                    cboTransporteDevolucion.Text = cabEdit.TransporteDevolucion;
                    txtNroGuiaDevolucion.Text = cabEdit.NroGuiaDevolucion;
                }
            }
            else
            {
                EstadoRendicion = "PRESENTADA";
            }



            /// Busco si existe una definicion de saldos para el transportista
            SaldoTransportista = (from c in Contexto.CabeceraRendicions
                                  where c.UsuarioTransporte == idUsuarioTransporte
                                  && c.IdCabeceraRendicion != IdRendicion
                                  select c).Sum(w => w.Saldo);

            SaldoTransportista = SaldoTransportista == null ? 0 : SaldoTransportista.Value;


            // Determino que tipo de rol tiene el usuario logeado
            var rolesUsuario = (from s in Contexto.SegUsuarioRols
                                where s.Cliente == idUsuarioTransporte
                                select s.SegRol.Descripcion).ToList();

            Session["TieneRolLogistica"] = rolesUsuario.Any(w => w == "LOGISTICA");
            Session["TieneRolCobranza"] = rolesUsuario.Any(w => w == "COBRANZAS");



            if (!EsTransportista)
            {
                tdGrabar.Visible = false;
                if (!TieneRolCobranza && !TieneRolLogistica)
                {
                    tdAprobacion.Visible = false;
                    tdObservarcion.Visible = false;

                    /// Cargo lo valores de observación.
                    txtObsCobranza.Text = cabEdit.ObservacionCobranza;
                    txtObsLogistica.Text = cabEdit.ObservacionLogistica;
                    if (cabEdit.MontoNotaCredito.HasValue)
                        txtMonto.Text = cabEdit.MontoNotaCredito.Value.ToString();
                }
                else
                {


                    switch (TipoRendicion)
                    {
                        ///Estos tipos de rendiciones solo necesita la aprobacion por parte de cobranza.
                        case "V":
                        case "L":
                            if (EstadoRendicion != "APROBADA" && TieneRolCobranza)
                            {
                                tdAprobacion.Visible = true;
                                tdObservarcion.Visible = true;
                            }
                            else
                            {
                                tdAprobacion.Visible = false;
                                tdObservarcion.Visible = false;
                            }
                            /// Oculto los controles de la observación de logística ya que 
                            /// no lleva estos datos para este tipo de rendciones.
                            tdObsLogisticaDatos.Visible = false;
                            tdObsLogisticaDatos1.Visible = false;
                            tdObsLogisticaTitulo.Visible = false;

                            /// Cargo lo valores de observación.
                            txtObsCobranza.Text = cabEdit.ObservacionCobranza;
                            if (cabEdit.MontoNotaCredito.HasValue)
                                txtMonto.Text = cabEdit.MontoNotaCredito.Value.ToString();
                            break;
                        /// Para el restro de las rendiciones: Devoliciones, siniestros y Extravios se necesita la aprobacion
                        /// de ambos: cobranza y logistica
                        default:
                            /// si esta aprobada no se puede hacer mas nada.
                            if (EstadoRendicion == "APROBADA")
                            {
                                tdAprobacion.Visible = false;
                                tdObservarcion.Visible = false;
                            }
                            /// si esta aprobada por logistica y el usuario tiene rol cobranza se puede
                            /// aprobar u orbservar.
                            else if (TieneRolCobranza && EstadoRendicion == "APROBADA L")
                            {
                                tdAprobacion.Visible = true;
                                tdObservarcion.Visible = true;
                            }
                            /// si esta aprobada por cobranza y el usuario tiene rol logistica se puede
                            /// aprobar u orbservar.
                            else if (TieneRolLogistica && EstadoRendicion == "APROBADA C")
                            {
                                tdAprobacion.Visible = true;
                                tdObservarcion.Visible = true;
                            }
                            /// si esta aprobada por logistica y el usuario tiene rol logistica no se 
                            /// puede hacer mas nada
                            else if (TieneRolLogistica && EstadoRendicion == "APROBADA L")
                            {
                                tdAprobacion.Visible = false;
                                tdObservarcion.Visible = false;
                            }
                            /// si esta aprobada por cobranza y el usuario tiene rol cobranza no se 
                            /// puede hacer mas nada
                            else if (TieneRolCobranza && EstadoRendicion == "APROBADA C")
                            {
                                tdAprobacion.Visible = false;
                                tdObservarcion.Visible = false;
                            }
                            /// para el resto de los casos: observada L/C, observada L u observada C se puede
                            /// aprobar u modificar la obaservacion.
                            else
                            {
                                tdAprobacion.Visible = true;
                                tdObservarcion.Visible = true;
                            }

                            /// Cargo lo valores de observación.
                            txtObsCobranza.Text = cabEdit.ObservacionCobranza;
                            txtObsLogistica.Text = cabEdit.ObservacionLogistica;
                            if (cabEdit.MontoNotaCredito.HasValue)
                                txtMonto.Text = cabEdit.MontoNotaCredito.Value.ToString();
                            break;
                    }

                    if (TieneRolLogistica)
                    {
                        txtObsLogistica.Enabled = true;

                        txtMonto.Enabled = false;
                        txtObsCobranza.Enabled = false;
                    }
                    else if (TieneRolCobranza)
                    {
                        txtObsLogistica.Enabled = false;

                        txtMonto.Enabled = true;
                        txtObsCobranza.Enabled = true;
                    }
                }

                if (TipoRendicion == "D")
                {
                    cboTransporteDevolucion.Enabled = false;
                    txtNroGuiaDevolucion.Enabled = false;
                    txtFechaEnvioDevolucion.Enabled = false;
                    lblTituloDevolucion.InnerText = "INFORMACION DE ENVIO INGRESADA";
                }
            }
            else
            {
                tdAprobacion.Visible = false;
                tdObservarcion.Visible = false;

                if (EstadoRendicion == "APROBADA")
                {
                    tdObservaciones.Visible = false;
                   
                }
                else
                {
                    switch (TipoRendicion)
                    {
                        ///Estos tipos de rendiciones solo necesita la aprobacion por parte de cobranza.
                        case "V":
                        case "L":
                            /// Oculto los controles de la observación de logística ya que 
                            /// no lleva estos datos para este tipo de rendciones.
                            tdObsLogisticaDatos.Visible = false;
                            tdObsLogisticaDatos1.Visible = false;
                            tdObsLogisticaTitulo.Visible = false;

                            /// Cargo lo valores de observación.
                            if (cabEdit != null)
                            {
                                txtObsCobranza.Text = cabEdit.ObservacionCobranza;
                                if (cabEdit.MontoNotaCredito.HasValue)
                                    txtMonto.Text = cabEdit.MontoNotaCredito.Value.ToString();
                            }
                            else
                            {
                                tdObservaciones.Visible = false;
                            }
                            break;
                        /// Para el restro de las rendiciones: Devoliciones, siniestros y Extravios se necesita la aprobacion
                        /// de ambos: cobranza y logistica
                        default:
                            if (cabEdit != null)
                            {
                                /// Cargo lo valores de observación.
                                txtObsCobranza.Text = cabEdit.ObservacionCobranza;
                                txtObsLogistica.Text = cabEdit.ObservacionLogistica;
                                if (cabEdit.MontoNotaCredito.HasValue)
                                    txtMonto.Text = cabEdit.MontoNotaCredito.Value.ToString();
                            }
                            else
                            {
                                tdObservaciones.Visible = false;
                            }
                            break;
                    }

                    txtObsCobranza.Enabled = false;
                    txtObsLogistica.Enabled = false;
                    txtMonto.Enabled = false;
                }
            }
        }
    }

    #region Metodos Web

    [WebMethod(EnableSession = true)]
    public static bool GrabarRendicion(IList idsDetalles, string montoGuias, string montoIngresado, string fechaEnvioDev, string nroGuiaDev, string tranportistaDev)
    {

        try
        {
            List<long> ids = new List<long>();
            foreach (var item in idsDetalles)
            {
                ids.Add(long.Parse(item.ToString()));
            }


            //Dictionary<string, object> datos = new Dictionary<string, object>();
            long idUsuarioTransporte = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
            Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
            string idRendicion = HttpContext.Current.Session["IdRendicion"].ToString();
            CabeceraRendicion cabR = null;
            if (idRendicion != "0")
            {
                cabR = (from c in Contexto.CabeceraRendicions
                        where c.IdCabeceraRendicion == long.Parse(idRendicion)
                        select c).FirstOrDefault();


                /// Limpio todas las referencias anteriores para plasmar los cambios.
                foreach (var item in cabR.DetalleGuias.ToList())
                {
                    item.CabeceraRendicion = null;
                }
            }
            else
            {
                cabR = new CabeceraRendicion();
                Contexto.CabeceraRendicions.InsertOnSubmit(cabR);

                cabR.objTipoRendicion = (from p in Contexto.Parametros
                                         where p.Tipo == "TIPORENDICION" && p.Contexto == HttpContext.Current.Session["TipoRendicion"].ToString()
                                         select p).FirstOrDefault();

            }

            cabR.FechaRendicion = DateTime.Now;
            cabR.UsuarioTransporte = idUsuarioTransporte;

            if (HttpContext.Current.Session["TipoRendicion"].ToString() == "D" ||
                HttpContext.Current.Session["TipoRendicion"].ToString() == "L" ||
                HttpContext.Current.Session["TipoRendicion"].ToString() == "S" ||
                HttpContext.Current.Session["TipoRendicion"].ToString() == "E")
            {

                if (HttpContext.Current.Session["TipoRendicion"].ToString() == "D")
                {
                    cabR.FechaDevolucion = DateTime.Parse(fechaEnvioDev);
                    cabR.NroGuiaDevolucion = nroGuiaDev;
                    cabR.TransporteDevolucion = tranportistaDev;
                }

                cabR.Monto = decimal.Parse(montoGuias.Replace(".", ","));
                cabR.MontoPagado = decimal.Parse(montoGuias.Replace(".", ","));
                cabR.Saldo = 0;
            }
            else
            {
                cabR.FechaDevolucion = null;
                cabR.NroGuiaDevolucion = null;
                cabR.TransporteDevolucion = null;
                cabR.Monto = decimal.Parse(montoGuias.Replace(".", ","));
                cabR.MontoPagado = decimal.Parse(montoIngresado.Replace(".", ","));
                cabR.Saldo = cabR.MontoPagado - cabR.Monto;
            }

            cabR.objEstado = (from p in Contexto.Parametros
                              where p.Tipo == "ESTADORENDICION" && p.Valor == "PRESENTADA"
                              select p).FirstOrDefault();



            /// Relaciono las guias que son pagadas con esta rendicion
            List<DetalleGuia> guias = (from g in Contexto.DetalleGuias
                                       where ids.Contains(g.IdDetalleGuia)
                                       select g).ToList();

            foreach (DetalleGuia guia in guias)
            {
                guia.objCabeceraRendicion = cabR;
                guia.objEstadoRendicion = cabR.objEstado;
            }




            // Variable que se llena en el control de foma de pago
            List<TempFormasPago> pagos = (List<TempFormasPago>)ucFormaPagoMultiple.RecuperarPagosCargados();
            foreach (TempFormasPago item in pagos)
            {
                if (item.IdDetalle < 0)
                {

                    DetallePagoRendicion detP = new DetallePagoRendicion();
                    detP.objCabeceraRendicion = cabR;

                    // Datos Comunes
                    detP.Monto = item.Monto;
                    detP.objTipoPago = (from p in Contexto.Parametros where p.Tipo == "TIPOPAGOSRENDICION" && p.Valor == item.DescFormaPago select p).FirstOrDefault();

                    /// Datos Banco
                    detP.CH_Banco = item.IdBanco;
                    detP.CH_NroCheque = item.CH_NroCheque;
                    detP.CH_FechaCobro = item.CH_FechaCobro;
                    detP.CH_Titular = item.CH_Titular;


                    // Datos Transferencia
                    detP.T_FechaTranferencia = item.T_FechaTranferencia;
                    detP.T_NroControl = item.T_NroControl;
                    detP.T_NroCtaDestino = item.T_NroCtaDestino;
                    detP.T_Referencia = item.T_Referencia;

                    // Datos Deposito
                    detP.D_FechaDeposito = item.D_FechaDeposito;
                    detP.D_NroCtaDestino = item.D_NroCtaDestino;
                    detP.D_NroTransaccion = item.D_NroTransaccion;

                    // Otro Medio de Pago
                    detP.O_NroOperacion = item.O_NroOperacion;
                    detP.O_FechaPago = item.O_FechaPago;
                    detP.O_Operador = item.O_IdOperador;

                    Contexto.DetallePagoRendicions.InsertOnSubmit(detP);
                }
            }



            Contexto.SubmitChanges();

        }
        catch
        {
            return false;
        }


        return false;


    }

    [WebMethod(EnableSession = true)]
    public static object FiltroRendicion(string FechaInicial, string FechaFinal, string Tansporte, string EstadoRendicion)
    {

        Dictionary<string, object> datos = new Dictionary<string, object>();
        long idCliente = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];

        try
        {
            string idRendicion = HttpContext.Current.Session["IdRendicion"].ToString();
            string tipoRendicion = HttpContext.Current.Session["TipoRendicion"].ToString();
            string cadenaWhere = "";
            List<object> parametros = new List<object>();

            string condicionRendicion = "";

            condicionRendicion = "cabeceraRendicion = " + idRendicion;

            //if ((bool)HttpContext.Current.Session["EsTransportista"] && EstadoRendicion == "RENDIDA")
            //{
            //    if (idRendicion == "0")
            //        condicionRendicion = "cabeceraRendicion = " + idRendicion;
            //    else
            //    {
            //        /// Si es transportista se va a traer todas las guias segun el estado correspondiente
            //        /// ademas si se esta editando tb se traen las guias en cuestion.
            //        condicionRendicion = "(cabeceraRendicion is null";
            //        condicionRendicion += idRendicion != "0" ? " or cabeceraRendicion = " + idRendicion : "";
            //        condicionRendicion += ")";
            //    }
            //}
            //else
            //{
            //    /// Si no es transportista entonces solo se traen las guias que corresponden 
            //    /// a la edición de la rendición.
            //    condicionRendicion = "cabeceraRendicion = " + idRendicion;
            //}

            if ((bool)HttpContext.Current.Session["EsTransportista"])
                cadenaWhere = condicionRendicion + " or (d.FechaEstadoLogistica is not null and cabecerarendicion is null " + ArmarFilro(tipoRendicion, FechaInicial, FechaFinal, Tansporte, ref parametros) + ")";
            else
                cadenaWhere = condicionRendicion + ArmarFilro("", FechaInicial, FechaFinal, Tansporte, ref parametros);


            var RendicionsTemp = Contexto.ExecuteQuery<DetalleGuia>(@"select distinct d.* from DetalleGuias as d " +
                    @" inner join CabeceraGuias c on c.IdCabeceraGuia  = d.CabeceraGuia" +
                    @" inner join Parametros p on d.EstadoLogistica  = p.Idparametro" +
                    @" where " + cadenaWhere, parametros.ToArray()).OrderByDescending(w => w.FechaEstadoLogistica).ToList();

            var resultado = (from d in RendicionsTemp
                             select new
                             {
                                 Nombre = d.Consultor,
                                 TipoComprobante = d.CCRR,
                                 Monto = string.Format("{0:###,##0.00}", d.TotalPagar),
                                 Lider = d.Lider,
                                 Comprobante = d.Comprobante,
                                 d.IdDetalleGuia,
                                 Fecha = d.FechaEstadoLogistica.Value.ToShortDateString(),
                                 Estado = d.objEstadoLogistica.Valor,
                                 IdCabeceraRendicion = d.CabeceraRendicion == null ? -1 : d.CabeceraRendicion,
                                 MotivoSiniestro = d.MotivoSiniestro,
                                 LiderResp = d.objLiderResponsable != null ? d.objLiderResponsable.Nombre : ""
                             }).Distinct().ToList();


            if (idRendicion != "0")
            {

                List<DetallePagoRendicion> pagos = (from p in Contexto.DetallePagoRendicions
                                                    where p.CabeceraRendicion == long.Parse(idRendicion)
                                                    select p).ToList();

                ucFormaPagoMultiple.CargarPagos(pagos);

            }

            return resultado;
        }
        catch
        {

            return null;
        }


    }

    [WebMethod(EnableSession = true)]
    public static void AprobarRendicion()
    {
        long idRendicion = long.Parse(HttpContext.Current.Session["IdRendicion"].ToString());
        long idCliente = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
        bool TieneRolLogistica = bool.Parse(HttpContext.Current.Session["TieneRolLogistica"].ToString());
        bool TieneRolCobranza = bool.Parse(HttpContext.Current.Session["TieneRolCobranza"].ToString());
        string TipoRendicion = HttpContext.Current.Session["TipoRendicion"].ToString();

        Marzzan_InfolegacyDataContext dc = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];


        CabeceraRendicion cab = (from c in dc.CabeceraRendicions
                                 where c.IdCabeceraRendicion == idRendicion
                                 select c).FirstOrDefault();


        string tipoAprobacion = "";
        switch (TipoRendicion)
        {
            ///Estos tipos de rendiciones solo necesita la aprobacion por parte de cobranza.
            case "V":
            case "L":
                tipoAprobacion = "APROBADA";
                break;
            /// Para el restro de las rendiciones: Devoliciones, siniestros y Extravios se necesita la aprobacion
            /// de ambos: cobranza y logistica
            default:
                if (cab.objEstado.Valor == "OBSERVADA C/L")
                {
                    if (TieneRolLogistica)
                        tipoAprobacion = "APROBADA L / OBSERVADA C";
                    else
                        tipoAprobacion = "APROBADA C / OBSERVADA L";
                }
                else if (cab.objEstado.Valor == "OBSERVADA L")
                {
                    if (TieneRolLogistica)
                        tipoAprobacion = "APROBADA L";
                    else
                        tipoAprobacion = "APROBADA C / OBSERVADA L";
                }
                else if (cab.objEstado.Valor == "OBSERVADA C")
                {
                    if (TieneRolLogistica)
                        tipoAprobacion = "APROBADA L / OBSERVADA C";
                    else
                        tipoAprobacion = "APROBADA C";
                }
                else if (cab.objEstado.Valor == "APROBADA L / OBSERVADA C")
                {
                    /// Como ya esta aprobada por L si ahora la aprueba C entonces 
                    /// queda directamente aprobada, por poseer las dos aprobaciones
                    if (TieneRolCobranza)
                        tipoAprobacion = "APROBADA";
                }
                else if (cab.objEstado.Valor == "APROBADA C / OBSERVADA L")
                {
                    /// Como ya esta aprobada por C si ahora la aprueba L entonces 
                    /// queda directamente aprobada, por poseer las dos aprobaciones
                    if (TieneRolLogistica)
                        tipoAprobacion = "APROBADA";
                }
                else if (cab.objEstado.Valor == "APROBADA C")
                {
                    if (TieneRolLogistica)
                        tipoAprobacion = "APROBADA";
                }
                else if (cab.objEstado.Valor == "APROBADA L")
                {
                    if (TieneRolCobranza)
                        tipoAprobacion = "APROBADA";
                }
                break;
        }



        cab.objEstado = (from p in dc.Parametros
                         where p.Tipo == "ESTADORENDICION" && p.Valor == tipoAprobacion
                         select p).FirstOrDefault();

        cab.FechaAprobacion = DateTime.Now;
        cab.UsuarioAprobador = idCliente;


        /// Actualizo el estado de las guias
        List<DetalleGuia> guias = (from g in dc.DetalleGuias
                                   where g.CabeceraRendicion == idRendicion
                                   select g).ToList();

        foreach (DetalleGuia guia in guias)
        {
            guia.objEstadoRendicion = cab.objEstado;
        }



        dc.SubmitChanges();


    }


    [WebMethod(EnableSession = true)]
    public static void ObservarRendicion(string obsCobranza, string monto, string obsLogistica)
    {
        long idRendicion = long.Parse(HttpContext.Current.Session["IdRendicion"].ToString());
        long idCliente = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
        Marzzan_InfolegacyDataContext dc = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];


        CabeceraRendicion cab = (from c in dc.CabeceraRendicions
                                 where c.IdCabeceraRendicion == idRendicion
                                 select c).FirstOrDefault();


        if (bool.Parse(HttpContext.Current.Session["TieneRolLogistica"].ToString()))
        {
            cab.ObservacionLogistica = obsLogistica;
            if (cab.objEstado.Valor == "OBSERVADA C" || cab.objEstado.Valor == "OBSERVADA C/L")
            {
                cab.objEstado = (from p in dc.Parametros
                                 where p.Tipo == "ESTADORENDICION" && p.Valor == "OBSERVADA C/L"
                                 select p).FirstOrDefault();

                // Si se observa automaticamente se saca la marca de aprobacion.
                cab.FechaAprobacion = null;
                cab.UsuarioAprobador = null;


            }

            else if (cab.objEstado.Valor == "APROBADA C" || cab.objEstado.Valor == "APROBADA C / OBSERVADA L")
            {
                cab.objEstado = (from p in dc.Parametros
                                 where p.Tipo == "ESTADORENDICION" && p.Valor == "APROBADA C / OBSERVADA L"
                                 select p).FirstOrDefault();



            }
            else if (cab.objEstado.Valor == "APROBADA L" || cab.objEstado.Valor == "APROBADA L / OBSERVADA C")
            {
                cab.objEstado = (from p in dc.Parametros
                                 where p.Tipo == "ESTADORENDICION" && p.Valor == "OBSERVADA C/L"
                                 select p).FirstOrDefault();

                // Si se observa automaticamente se saca la marca de aprobacion.
                cab.FechaAprobacion = null;
                cab.UsuarioAprobador = null;


            }
            else
            {
                cab.objEstado = (from p in dc.Parametros
                                 where p.Tipo == "ESTADORENDICION" && p.Valor == "OBSERVADA L"
                                 select p).FirstOrDefault();

                // Si se observa automaticamente se saca la marca de aprobacion.
                cab.FechaAprobacion = null;
                cab.UsuarioAprobador = null;
            }

        }
        else if (bool.Parse(HttpContext.Current.Session["TieneRolCobranza"].ToString()))
        {
            cab.ObservacionCobranza = obsCobranza;
            cab.MontoNotaCredito = decimal.Parse(monto.Replace(".", ","));
            if (cab.objEstado.Valor == "OBSERVADA L" || cab.objEstado.Valor == "OBSERVADA C/L")
            {
                cab.objEstado = (from p in dc.Parametros
                                 where p.Tipo == "ESTADORENDICION" && p.Valor == "OBSERVADA C/L"
                                 select p).FirstOrDefault();

                // Si se observa automaticamente se saca la marca de aprobacion.
                cab.FechaAprobacion = null;
                cab.UsuarioAprobador = null;
            }
            else if (cab.objEstado.Valor == "APROBADA C" || cab.objEstado.Valor == "APROBADA C / OBSERVADA L")
            {
                cab.objEstado = (from p in dc.Parametros
                                 where p.Tipo == "ESTADORENDICION" && p.Valor == "OBSERVADA C/L"
                                 select p).FirstOrDefault();

                // Si se observa automaticamente se saca la marca de aprobacion.
                cab.FechaAprobacion = null;
                cab.UsuarioAprobador = null;

            }
            else if (cab.objEstado.Valor == "APROBADA L" || cab.objEstado.Valor == "APROBADA L / OBSERVADA C")
            {
                cab.objEstado = (from p in dc.Parametros
                                 where p.Tipo == "ESTADORENDICION" && p.Valor == "APROBADA L / OBSERVADA C"
                                 select p).FirstOrDefault();

            }
            else
            {
                cab.objEstado = (from p in dc.Parametros
                                 where p.Tipo == "ESTADORENDICION" && p.Valor == "OBSERVADA C"
                                 select p).FirstOrDefault();

                // Si se observa automaticamente se saca la marca de aprobacion.
                cab.FechaAprobacion = null;
                cab.UsuarioAprobador = null;


            }

        }




        /// Actualizo el estado de las guias
        List<DetalleGuia> guias = (from g in dc.DetalleGuias
                                   where g.CabeceraRendicion == idRendicion
                                   select g).ToList();

        foreach (DetalleGuia guia in guias)
        {
            guia.objEstadoRendicion = cab.objEstado;
        }



        dc.SubmitChanges();


    }



    private static string ArmarFilro(string tipoRendicion, string fechaInicial, string fechaFinal, string transporte, ref List<object> parametros)
    {

        string cadenaWhere = "";
        int i = 0;

        if (tipoRendicion != "")
        {
            switch (tipoRendicion)
            {
                case "V":
                    cadenaWhere += " and p.Valor = '" + EstadoLogisticaGuias.ENTREGADA.ToString() + "'";
                    break;
                case "D":
                    cadenaWhere += " and p.Valor = '" + EstadoLogisticaGuias.DEVUELTA.ToString() + "'";
                    break;
                case "E":
                    cadenaWhere += " and p.Valor = '" + EstadoLogisticaGuias.EXTRAVIADAS.ToString() + "'";
                    break;
                case "L":
                    cadenaWhere += " and p.Valor = '" + EstadoLogisticaGuias.ENTREGALIDER.ToString() + "'";
                    break;
                case "S":
                    cadenaWhere += " and (p.Valor = '" + EstadoLogisticaGuias.SINIESTRADA_PARCIAL.ToString() + "' or  p.Valor = '" + EstadoLogisticaGuias.SINIESTRADA_TOTAL.ToString() + "')";
                    break;
            }
        }

        if (fechaInicial != "")
        {
            cadenaWhere += " and (d.FechaCobranza >= {" + i + "} or d.FechaDevolucion >= {" + i + "})";
            //cadenaWhere += " and c.FechaRendicion  >= {" + i + "}";
            parametros.Add(Convert.ToDateTime(fechaInicial));
            i++;
        }

        if (fechaFinal != "")
        {
            cadenaWhere += " and (d.FechaCobranza <= {" + i + "} or d.FechaDevolucion >= {" + i + "})";
            //cadenaWhere += " and c.FechaRendicion  <= {" + i + "}";
            parametros.Add(Convert.ToDateTime(fechaFinal).AddDays(1));
            i++;
        }


        if (transporte != "")
        {
            if (HttpContext.Current.Session["Transporte"] != null && HttpContext.Current.Session["Transporte"].ToString() != "")
                transporte = transporte == "Todos" ? HttpContext.Current.Session["Transporte"].ToString().ToUpper() : transporte;
            else
                transporte = transporte != "Todos" ? transporte.ToUpper() : "";

            cadenaWhere += " and c.Transporte like {" + i + "}";
            parametros.Add("%" + transporte + "%");
            i++;
        }


        return cadenaWhere;
    }


    #endregion

}
