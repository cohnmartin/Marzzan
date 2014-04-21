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

public partial class ConsolaMail : BasePage
{

    public class tempConsolaMail
    {
        public string Estado { get; set; }
        public string ToolTipEstado { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public long IdMailCabecera { get; set; }
        public long IdMailDestino { get; set; }
        public string Para { get; set; }
        public string Subject { get; set; }
        public string Cuerpo { get; set; }
        public bool Seleccion { get; set; }
        public string Hora { get; set; }
    }

    private IDictionary<long?, int> MailsUbicacion
    {
        get;
        set;
    }

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

        if (!IsPostBack)
        {
            Session.Timeout = 15;
            Session["Context"] = new Marzzan_InfolegacyDataContext();

            IDictionary<string, object> datos = FiltroMails("Recividos", "", "", "", "", "", 0, gvEmail.PageSize);
            gvEmail.VirtualCount = (int)datos["Cantidad"];
            gvEmail.DataSource = (List<tempConsolaMail>)datos["Mails"];

            /// Verifico que existe la carpeta principal de Recibidos
            var existeCarpetaPrincipal = (from u in Contexto.EstructurasUbicacions
                                          where u.Usuario == long.Parse(HttpContext.Current.Session["IdUsuario"].ToString())
                                          && u.Nombre == "Mensajes Recibidos"
                                          select u).Any();

            if (!existeCarpetaPrincipal)
            {
                EstructurasUbicacion estP = new EstructurasUbicacion();
                estP.Usuario = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
                estP.Nombre = "Mensajes Recibidos";
                Contexto.EstructurasUbicacions.InsertOnSubmit(estP);
                Contexto.SubmitChanges();
            }


            /// Calculo la cantidad de mail nuevos por carpeta
            MailsUbicacion = new Dictionary<long?, int>();
            GetCountMailCarpetas();


            // Cargo el arbol de carpetas para el ordenamieto de los mails y por cada nodo
            // indico la cantidad de nuevos mail que existen.
            List<EstructurasUbicacion> carpetas = (from u in Contexto.EstructurasUbicacions
                                                   where u.Usuario == long.Parse(HttpContext.Current.Session["IdUsuario"].ToString())
                                                   select u).ToList();

            RadTreeCarpetas.DataSource = carpetas;

            RadTreeCarpetas.DataBind();
            RadTreeCarpetas.Nodes.Add(new RadTreeNode("Mensajes Enviados", "Enviados"));
            RadTreeCarpetas.Nodes.Add(new RadTreeNode("Mensajes Eliminados", "Eliminados"));
            RadTreeCarpetas.Nodes[0].Selected = true;

            /// Busco las reglas defindas para el usuario
            gvReglas.DataSource = (IList)GetReglas();


            #region Configuracion de Controles para las reglas

            /// Inicializo el combo con las carpetas para la 
            /// configuracion de las reglas.
            cboCarpetas.DataValueField = "IdEstructuraUbicacion";
            cboCarpetas.DataTextField = "Nombre";
            cboCarpetas.DataSource = carpetas;
            cboCarpetas.DataBind();




            /// Segun el tipo de cliente es con lo que carga el origen posible de la regla
            Cliente cliente = (from C in Contexto.Clientes
                               where C.IdCliente == long.Parse(Session["IdUsuario"].ToString())
                               select C).Single<Cliente>();


            if (cliente.TipoCliente.ToUpper() != TipoClientes.Consultor.ToString().ToUpper()
                && cliente.Clasif1.ToUpper() != "WEB")

            {

                var GruposValidos = Helper.ObtenerGruposSubordinados(Session["GrupoCliente"].ToString());
                GruposValidos.Add("ASISTENTE");

                Session.Add("GruposValidos", GruposValidos);
                Session.Add("DatosGruposAshx", GruposValidos.Select(w => new View_Grupo { Grupo = w }).ToList());

            }
            else if (cliente.Clasif1.ToUpper() == "WEB")
            {

                Session.Add("GruposValidos", null);
                Session.Add("DatosGruposAshx", null);

            }


            #endregion

            #region Configuracion de Controles para la firma
            MailsFirma FirmaActual = Contexto.MailsFirmas.Where(w => w.Usuario == long.Parse(Session["IdUsuario"].ToString())).FirstOrDefault();

            if (FirmaActual != null)
            {
                imgFirma.Src = "ImagenesFirma/" + FirmaActual.NombeArchivo;
                imgFirma.Style.Add("display", "block");
                tdEliminarFirma.Style.Add("display", "block");
                lblFirmaVacia.Style.Add("display", "none");

            }
            #endregion
        }


    }

    protected void RadTreeCarpetas_NodeDataBound(object sender, RadTreeNodeEventArgs e)
    {
        e.Node.Expanded = true;
        EstructurasUbicacion row = (EstructurasUbicacion)e.Node.DataItem;
        if (row.Padre != null)
        {

            if (MailsUbicacion.ContainsKey(row.IdEstructuraUbicacion))
            {
                e.Node.Text = "<b>" + e.Node.Text + "</b> &nbsp;<span style='color:blue'>(" + MailsUbicacion[row.IdEstructuraUbicacion].ToString() + ")</span>";
            }
        }
        else
        {
            if (MailsUbicacion.ContainsKey(0))
            {
                e.Node.Text = "<b>" + e.Node.Text + "</b> &nbsp;<span style='color:blue'>(" + MailsUbicacion[0].ToString() + ")</span>";
            }
            e.Node.Value = "Recividos";
        }
    }

    public void GetCountMailCarpetas()
    {
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
        string idUsuaioLogin = HttpContext.Current.Session["IdUsuario"].ToString();

        string cadenaWhere = " m.estado = '" + EstadosMails.SINLEER + "' and m.Usuario=" + idUsuaioLogin + " and m.Estado != '" + EstadosMails.ELIMINADO + "' ";


        var mailsa = Contexto.ExecuteQuery<MailsDestino>(@"select m.* from MailsDestinos as m  " +
                     @" where  " + cadenaWhere).ToList();

        var mailAgrupados = (from m in mailsa
                             group m by m.Ubicacion into g
                             select new
                             {
                                 ubicacion = g.Key,
                                 Cantidad = g.Count()
                             }).ToList();

        foreach (var item in mailAgrupados)
        {
            long key = item.ubicacion == null ? 0 : item.ubicacion.Value;
            MailsUbicacion.Add(key, item.Cantidad);

        }

    }

    public static int GetCountMails(string tipoMail, string fechaInicial, string fechaFinal, string origen, string asunto, string estado)
    {
        long idCliente = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
        List<tempConsolaMail> mails = new List<tempConsolaMail>();
        int cantidadTotal = 0;
        string cadenaWhere = "";
        List<object> parametros = new List<object>();
        parametros.Add(idCliente);

        if (tipoMail == "Recividos")
        {
            cadenaWhere = "md.Usuario={0} " + ArmarFilro(fechaInicial, fechaFinal, origen, asunto, estado, ref parametros);


            cantidadTotal = Contexto.ExecuteQuery<MailsCabecera>(@"select m.* from MailsCabecera as m inner join MailsDestinos md " +
                    @" on m.IdMailCabecera = md.Mail " +
                    @" inner join Clientes c on m.Usuario = c.IdCliente  " +
                    @" where md.ubicacion is null and " + cadenaWhere, parametros.ToArray()).Count();

        }
        else if (tipoMail == "Enviados")
        {

            cadenaWhere = "m.Usuario={0} " + ArmarFilro(fechaInicial, fechaFinal, origen, asunto, estado, ref parametros);


            cantidadTotal = Contexto.ExecuteQuery<MailsCabecera>(@"select m.* from MailsCabecera as m inner join MailsDestinos md " +
                    @" on m.IdMailCabecera = md.Mail " +
                    @" inner join Clientes c on md.Usuario = c.IdCliente  " +
                    @" where " + cadenaWhere, parametros.ToArray()).Count();


            //cadenaWhere = "m.Usuario={0} " + ArmarFilro(fechaInicial, fechaFinal, origen, asunto, estado, ref parametros);


            //var mailsa = Contexto.ExecuteQuery<MailsCabecera>(@"select m.* from MailsCabecera as m  " +
            //            @" where " + cadenaWhere, parametros.ToArray()).Count();



        }
        else if (tipoMail == "Eliminados")
        {

            try
            {
                // Mails eliminado que fueron enviados por el usario logeado
                cadenaWhere = "m.Usuario={0} and m.FechaEliminado is not null " + ArmarFilro(fechaInicial, fechaFinal, origen, asunto, estado, ref parametros);


                cantidadTotal = Contexto.ExecuteQuery<MailsCabecera>(@"select m.* from MailsCabecera as m inner join MailsDestinos md " +
                        @" on m.IdMailCabecera = md.Mail " +
                        @" inner join Clientes c on m.Usuario = c.IdCliente  " +
                        @" where " + cadenaWhere, parametros.ToArray()).Count();



                // Mails eliminados que fueron recividos por el usario logeado
                cadenaWhere = "md.Usuario={0} " + ArmarFilro(fechaInicial, fechaFinal, origen, asunto, EstadosMails.ELIMINADO, ref parametros);


                cantidadTotal += Contexto.ExecuteQuery<MailsCabecera>(@"select m.* from MailsCabecera as m inner join MailsDestinos md " +
                        @" on m.IdMailCabecera = md.Mail " +
                        @" inner join Clientes c on md.Usuario = c.IdCliente  " +
                        @" where " + cadenaWhere, parametros.ToArray()).Count();


            }
            catch
            {

            }
        }

        return cantidadTotal;
    }

    private static string ArmarFilro(string fechaInicial, string fechaFinal, string origen, string asunto, string estado, ref List<object> parametros)
    {
        string cadenaWhere = "";
        int i = 1;

        if (fechaInicial != "")
        {
            cadenaWhere += " and m.Fecha >= {" + i + "}";
            parametros.Add(Convert.ToDateTime(fechaInicial));
            i++;
        }

        if (fechaFinal != "")
        {
            cadenaWhere += " and m.Fecha <= {" + i + "}";
            parametros.Add(Convert.ToDateTime(fechaFinal).AddDays(1));
            i++;
        }

        if (estado != "")
        {
            cadenaWhere += " and md.Estado = {" + i + "}";
            parametros.Add(estado);
            i++;
        }

        if (asunto != "")
        {
            cadenaWhere += " and m.Subject like {" + i + "}";
            parametros.Add("%" + asunto + "%");
            i++;
        }

        if (origen != "")
        {
            cadenaWhere += " and c.Nombre like {" + i + "}";
            parametros.Add("%" + origen + "%");
            i++;
        }

        return cadenaWhere;
    }

    [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> EliminarMails(string tipoMail, string ids, int take)
    {
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
        List<long> idsDelete = ids.Split(',').Select(c => long.Parse(c)).ToList();

        if (tipoMail == "Enviados")
        {
            var mailsDelete = (from m in Contexto.MailsCabeceras
                               where idsDelete.Contains(m.IdMailCabecera)
                               select m).ToList();

            foreach (var item in mailsDelete)
            {
                item.FechaEliminado = DateTime.Now;
            }
        }
        else
        {
            var mailsDelete = (from m in Contexto.MailsDestinos
                               where idsDelete.Contains(m.IdMailDestino)
                               select m).ToList();

            foreach (var item in mailsDelete)
            {
                item.Estado = EstadosMails.ELIMINADO;
                item.FechaCambioEstado = DateTime.Now;
            }

        }

        Contexto.SubmitChanges();

        return null;// FiltroMails(tipoMail, "", "", "", "", "", 0, take);
    }

    [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> FiltroMails(string tipoMail, string fechaInicial, string fechaFinal, string origen, string asunto, string estado, int skip, int take)
    {
        Dictionary<string, object> datos = new Dictionary<string, object>();
        long idCliente = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
        List<tempConsolaMail> mails = new List<tempConsolaMail>();

        string cadenaWhere = "";
        int i = 1;
        List<object> parametros = new List<object>();
        parametros.Add(idCliente);

        if (tipoMail == "Recividos")
        {
            cadenaWhere = "md.Usuario={0} and md.Estado != '" + EstadosMails.ELIMINADO + "' " + ArmarFilro(fechaInicial, fechaFinal, origen, asunto, estado, ref parametros);


            var mailsa = Contexto.ExecuteQuery<MailsCabecera>(@"select m.* from MailsCabecera as m inner join MailsDestinos md " +
                    @" on m.IdMailCabecera = md.Mail " +
                    @" inner join Clientes c on m.Usuario = c.IdCliente  " +
                    @" where md.ubicacion is null and " + cadenaWhere, parametros.ToArray()).OrderByDescending(w => w.Fecha).Skip(skip).Take(take).ToList();

            mails = (from m in mailsa
                     orderby m.Fecha descending
                     select new tempConsolaMail
                     {
                         ToolTipEstado = m.MailsDestinos.FirstOrDefault(w => w.Cliente.IdCliente == idCliente).Estado,
                         IdMailDestino = m.MailsDestinos.FirstOrDefault(w => w.Cliente.IdCliente == idCliente).IdMailDestino,
                         Fecha = m.Fecha.Value,
                         Subject = m.Subject,
                         Cuerpo = m.Cuerpo,
                         Hora = string.Format("{0:HH:mm}", m.Fecha.Value),
                         IdMailCabecera = m.IdMailCabecera,
                         Para = m.Cliente.Nombre,
                         Seleccion = false,
                     }).Distinct().ToList();



        }
        else if (tipoMail == "Enviados")
        {

            cadenaWhere = "m.Usuario={0} and m.FechaEliminado is null  " + ArmarFilro(fechaInicial, fechaFinal, origen, asunto, estado, ref parametros);


            var mailsa = Contexto.ExecuteQuery<MailsCabecera>(@"select m.* from MailsCabecera as m  inner join MailsDestinos md " +
                        @" on m.IdMailCabecera = md.Mail " +
                        @" inner join Clientes c on md.Usuario = c.IdCliente  " +
                        @" where " + cadenaWhere, parametros.ToArray()).OrderByDescending(w => w.Fecha).Skip(skip).Take(take).ToList();


            mails = (from m in mailsa
                     orderby m.Fecha descending
                     select new tempConsolaMail
                     {
                         ToolTipEstado = EstadosMails.ENVIADO,
                         Fecha = m.Fecha.Value,
                         Subject = m.Subject,
                         Cuerpo = m.Cuerpo,
                         Hora = string.Format("{0:HH:mm}", m.Fecha.Value),
                         IdMailCabecera = m.IdMailCabecera,
                         Para = string.Join(";", m.MailsDestinos.Select(w => w.Cliente.Nombre.ToLower()).Take(3).ToArray()),
                         Seleccion = false,
                     }).Distinct().ToList();


        }
        else if (tipoMail == "Eliminados")
        {

            try
            {
                // Mails eliminado que fueron enviados por el usario logeado
                cadenaWhere = "m.Usuario={0} and m.FechaEliminado is not null " + ArmarFilro(fechaInicial, fechaFinal, origen, asunto, estado, ref parametros);


                var mailsa = Contexto.ExecuteQuery<MailsCabecera>(@"select m.* from MailsCabecera as m inner join MailsDestinos md " +
                        @" on m.IdMailCabecera = md.Mail " +
                        @" inner join Clientes c on m.Usuario = c.IdCliente  " +
                        @" where " + cadenaWhere, parametros.ToArray()).OrderByDescending(w => w.Fecha).ToList();


                mails = (from m in mailsa
                         orderby m.Fecha descending
                         select new tempConsolaMail
                         {
                             ToolTipEstado = EstadosMails.ELIMINADOENVIADO,
                             Fecha = m.Fecha.Value,
                             Subject = m.Subject,
                             Cuerpo = m.Cuerpo,
                             Hora = string.Format("{0:HH:mm}", m.Fecha.Value),
                             IdMailCabecera = m.IdMailCabecera,
                             Para = string.Join(";", m.MailsDestinos.Select(w => w.Cliente.Nombre.ToLower()).Take(3).ToArray()),
                             Seleccion = false,
                         }).ToList();


                // Mails eliminados que fueron recividos por el usario logeado
                cadenaWhere = "md.Usuario={0} " + ArmarFilro(fechaInicial, fechaFinal, origen, asunto, EstadosMails.ELIMINADO, ref parametros);


                mailsa = Contexto.ExecuteQuery<MailsCabecera>(@"select m.* from MailsCabecera as m inner join MailsDestinos md " +
                        @" on m.IdMailCabecera = md.Mail " +
                        @" inner join Clientes c on md.Usuario = c.IdCliente  " +
                        @" where " + cadenaWhere, parametros.ToArray()).OrderByDescending(w => w.Fecha).ToList();


                mails.AddRange((from m in mailsa
                                orderby m.Fecha descending
                                select new tempConsolaMail
                                {
                                    ToolTipEstado = EstadosMails.ELIMINADORECIVIDO,
                                    Fecha = m.Fecha.Value,
                                    Subject = m.Subject,
                                    Cuerpo = m.Cuerpo,
                                    Hora = string.Format("{0:HH:mm}", m.Fecha.Value),
                                    IdMailCabecera = m.IdMailCabecera,
                                    Para = m.Cliente.Nombre,
                                    Seleccion = false,
                                }).ToList());


                mails = mails.Skip(skip).Take(take).ToList();
            }
            catch
            {

            }
        }
        else
        {
            string idUbicacion = tipoMail.Split('|')[1];

            cadenaWhere = "md.Usuario={0} and md.ubicacion = " + idUbicacion + " and md.Estado != '" + EstadosMails.ELIMINADO + "' " + ArmarFilro(fechaInicial, fechaFinal, origen, asunto, estado, ref parametros);


            var mailsa = Contexto.ExecuteQuery<MailsCabecera>(@"select m.* from MailsCabecera as m inner join MailsDestinos md " +
                    @" on m.IdMailCabecera = md.Mail " +
                    @" inner join Clientes c on m.Usuario = c.IdCliente  " +
                    @" where " + cadenaWhere, parametros.ToArray()).OrderByDescending(w => w.Fecha).Skip(skip).Take(take).ToList();

            mails = (from m in mailsa
                     orderby m.Fecha descending
                     select new tempConsolaMail
                     {
                         ToolTipEstado = m.MailsDestinos.FirstOrDefault(w => w.Cliente.IdCliente == idCliente).Estado,
                         IdMailDestino = m.MailsDestinos.FirstOrDefault(w => w.Cliente.IdCliente == idCliente).IdMailDestino,
                         Fecha = m.Fecha.Value,
                         Subject = m.Subject,
                         Cuerpo = m.Cuerpo,
                         Hora = string.Format("{0:HH:mm}", m.Fecha.Value),
                         IdMailCabecera = m.IdMailCabecera,
                         Para = m.Cliente.Nombre,
                         Seleccion = false,
                     }).Distinct().ToList();
        }

        foreach (tempConsolaMail item in mails)
        {
            switch (item.ToolTipEstado)
            {
                case EstadosMails.LEIDO:
                    item.Estado = "Imagenes/MailLeido.png";
                    break;
                case EstadosMails.RESPONDIDO:
                    item.Estado = "Imagenes/MailResponder.png";
                    break;
                case EstadosMails.ENVIADO:
                    item.Estado = "Imagenes/EnviarMail.png";
                    break;
                case EstadosMails.ELIMINADOENVIADO:
                    item.Estado = "Imagenes/EnviarMail.png";
                    break;
                case EstadosMails.ELIMINADORECIVIDO:
                    item.Estado = "Imagenes/MailRecividoElimanado.png";
                    break;
                default:
                    item.Estado = "Imagenes/MailSinLeer.png";
                    break;
            }

            if (item.Cuerpo.Length > 25)
                item.Descripcion = "<b>" + item.Subject + "</b> - " + item.Cuerpo.Substring(0, 25).Replace("<p>", " ").Replace("</p>", "").Replace("<img", "").Replace("\r\n", "").Replace("<", " ");
            else
                item.Descripcion = "<b>" + item.Subject + "</b> - " + item.Cuerpo.Replace("<p>", " ").Replace("</p>", "").Replace("<img", "").Replace("\r\n", "").Replace("<", " ");

        }

        datos.Add("Cantidad", GetCountMails(tipoMail, fechaInicial, fechaFinal, origen, asunto, estado));
        datos.Add("Mails", mails);


        return datos;
    }

    [WebMethod(EnableSession = true)]
    public static string GestionCarpetas(string accion, string nombreCarpeta, string IdCarpetaPadre)
    {

        long idCliente = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
        EstructurasUbicacion carpeta = null;
        switch (accion)
        {
            case "Agregar":
                long idPadre = IdCarpetaPadre == "" ? Contexto.EstructurasUbicacions.Where(w => w.Usuario == idCliente && w.Padre == null).FirstOrDefault().IdEstructuraUbicacion : long.Parse(IdCarpetaPadre);

                carpeta = new EstructurasUbicacion();
                carpeta.Nombre = nombreCarpeta;
                carpeta.Usuario = idCliente;
                carpeta.Padre = idPadre;
                Contexto.EstructurasUbicacions.InsertOnSubmit(carpeta);
                Contexto.SubmitChanges();
                return carpeta.IdEstructuraUbicacion.ToString();
            case "Modificar":
                carpeta = Contexto.EstructurasUbicacions.Where(w => w.IdEstructuraUbicacion == long.Parse(IdCarpetaPadre)).FirstOrDefault();
                carpeta.Nombre = nombreCarpeta;
                Contexto.SubmitChanges();
                return carpeta.IdEstructuraUbicacion.ToString();
            case "Eliminar":
                /// Busco la carpeta a eliminar
                carpeta = Contexto.EstructurasUbicacions.Where(w => w.IdEstructuraUbicacion == long.Parse(IdCarpetaPadre)).FirstOrDefault();

                /// Muevo los mail a la carpeta padre de la que esta eliminando
                List<MailsDestino> mailsMover = Contexto.MailsDestinos.Where(w => w.Usuario == idCliente && w.Ubicacion == long.Parse(IdCarpetaPadre)).ToList();
                long? idNuevoPadre = carpeta.objPadre.objPadre != null ? carpeta.Padre : null;
                foreach (var item in mailsMover)
                {
                    item.Ubicacion = idNuevoPadre;
                }


                /// ELimino la carpeta
                Contexto.EstructurasUbicacions.DeleteOnSubmit(carpeta);

                Contexto.SubmitChanges();
                return null;
        }

        return null;

    }

    [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> MoverMails(string idsMails, string IdCarpetaDestino, string IdCarpetaOrigen)
    {

        long idCliente = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];

        List<long> idsMailsMover = new List<long>();
        foreach (var item in idsMails.Split(','))
        {
            if (item != "")
                idsMailsMover.Add(long.Parse(item));
        }


        /// Muevo los mail a la carpeta destino
        long? idUbicacionFinal = null;
        if (IdCarpetaDestino != "Recividos")
            idUbicacionFinal = long.Parse(IdCarpetaDestino);

        List<MailsDestino> mailsMover = Contexto.MailsDestinos.Where(w => w.Usuario == idCliente && idsMailsMover.Contains(w.IdMailDestino)).ToList();
        foreach (var item in mailsMover)
        {
            item.Ubicacion = idUbicacionFinal;

        }


        Contexto.SubmitChanges();
        return null;

    }

    [WebMethod(EnableSession = true)]
    public static object GetReglas()
    {

        long idCliente = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];

        return (from u in Contexto.MailsReglas
                where u.Usuario == long.Parse(HttpContext.Current.Session["IdUsuario"].ToString())
                select new
                {
                    u.IdRegla,
                    u.Nombre,
                    Descripcion = u.Descripcion,
                    Accion = u.Accion1 + (u.Accion2 != null ? ", " + u.Accion2 : "") + (u.Accion3 != null ? ", " + u.Accion3 : "") + (u.Accion4 != null ? ", " + u.Accion4 : ""),
                    Condicion = u.objCondicionUsuario != null ? "Cta: " + u.objCondicionUsuario.Nombre : u.CondicionTexto,
                    Destino = u.objDestinoCarpeta != null ? "Carpeta: " + u.objDestinoCarpeta.Nombre : "Cta: " + u.objDestinoUsuario.Nombre,
                    ImgEjecutarRegla = "Imagenes/ConfigurarMail.png"
                }).ToList();

    }

    [WebMethod(EnableSession = true)]
    public static object AgregarRegla(long origen, long destino)
    {

        long idCliente = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];

        MailsReglas newRegla = new MailsReglas();
        newRegla.Usuario = idCliente;
        newRegla.CondicionUsuario = origen;
        newRegla.DestinoCarpeta = destino;
        newRegla.Nombre = "Segun Origen";
        newRegla.Accion1 = "Mover";
        newRegla.Descripcion = "Ejecuta la Regla: Mover los mensajes a una carpeta especifica según una cta especifica particular";
        Contexto.MailsReglas.InsertOnSubmit(newRegla);
        Contexto.SubmitChanges();

        return GetReglas();

    }


    [WebMethod(EnableSession = true)]
    public static object EliminarRegla(long idRegla)
    {

        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
        MailsReglas rDelete = Contexto.MailsReglas.Where(w => w.IdRegla == idRegla).FirstOrDefault();
        Contexto.MailsReglas.DeleteOnSubmit(rDelete);
        Contexto.SubmitChanges();

        return GetReglas();

    }


    [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> EjecutarRegla(long idRegla)
    {

        long idCliente = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];

        MailsReglas regla = (from r in Contexto.MailsReglas
                             where r.IdRegla == idRegla
                             select r).FirstOrDefault();


        switch (regla.Nombre)
        {
            case "Segun Origen":
                /// Busco todos los mails destinos para el usuario logeado y
                /// los agrupo segun el origen, es decir segun quien los envio.
                /// Luego ejecuto las reglas para cada origen.

                var mDestinosAgrupado = (from d in Contexto.MailsDestinos
                                         where d.Usuario == idCliente
                                         group d by d.MailsCabecera.Usuario into g
                                         select new
                                         {
                                             Origen = g.Key,
                                             Mails = g
                                         }).ToList();

                foreach (var item in mDestinosAgrupado)
                {
                    HelperReglasMails.GestionarReglaMoverMailSegunOrigen(item.Origen.Value, item.Mails.ToList(), Contexto);
                }


                break;
            default:
                break;
        }

        return null;

    }

    [WebMethod(EnableSession = true)]
    public static object AgregarFirma(string nombreArchivo)
    {

        long idCliente = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];

        MailsFirma newFirma = Contexto.MailsFirmas.Where(w => w.Usuario == idCliente).FirstOrDefault();

        if (newFirma == null)
        {
            newFirma = new MailsFirma();
            newFirma.Usuario = idCliente;
            newFirma.NombeArchivo = nombreArchivo;
            Contexto.MailsFirmas.InsertOnSubmit(newFirma);
        }
        else
        {
            newFirma.NombeArchivo = nombreArchivo;
        }

        Contexto.SubmitChanges();

        return null;

    }


    
        [WebMethod(EnableSession = true)]
    public static object EliminarFirma()
    {

        long idCliente = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];

        MailsFirma newFirma = Contexto.MailsFirmas.Where(w => w.Usuario == idCliente).FirstOrDefault();

        if (newFirma != null)
        {
            Contexto.MailsFirmas.DeleteOnSubmit(newFirma);
        }

        Contexto.SubmitChanges();

        return null;

    }

}
