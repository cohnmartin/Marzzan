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

public partial class ConsultaSolicitudesAlta : BasePage
{

    private Marzzan_BejermanDataContext ContextoBejerman
    {
        get
        {

            if (Session["ContextoBejerman"] == null)
            {
                Session.Add("ContextoBejerman", new Marzzan_BejermanDataContext());
            }

            return (Marzzan_BejermanDataContext)Session["ContextoBejerman"];
        }

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
        /// 09/01/2013: Para hacer una implementacion rápida se anulo la funcionaidad de cambio de estado
        /// <AjaxInfo:FunctionGral Text="Cambiar Estado" ClickFunction="CambiarEstado" ImgUrl="imagenes/ok.gif" Type="Custom" />

        gvAltas.ExportToExcel += new ControlsAjaxNotti.ClickEventHandler(gvAltas_ExportToExcel);

        if (!IsPostBack)
        {
            Session["GruposHibilitados"] = Helper.ObtenerGruposSubordinados(Session["GrupoCliente"].ToString());
            Session["SolicitudesFiltradas"] = new List<SolicitudesAlta>();
            Session["Context"] = new Marzzan_InfolegacyDataContext();
            Session["ContextoBejerman"] = new Marzzan_BejermanDataContext();

            IDictionary<string, object> datosIniciales = FiltroAltas("", "", "", "", "Todos", 0, 15, false);
            gvAltas.DataSource = (IList)datosIniciales["Altas"];
            gvAltas.VirtualCount = int.Parse(datosIniciales["Cantidad"].ToString());

            cboGrupos.AppendDataBoundItems = true;
            cboGrupos.DataSource = (List<string>)Session["GruposHibilitados"];
            cboGrupos.DataBind();


        }
    }

    void gvAltas_ExportToExcel(object sender)
    {
        var solicitudes = (from s in (Session["SolicitudesFiltradas"] as List<SolicitudesAlta>)
                           select new
                           {
                               s.IdSolicitudAlta,
                               s.Nombre,
                               s.DNI,
                               Provincia = s.Provincia.ToUpper(),
                               Localidad = s.Localidad.ToUpper(),
                               TipoAlta = s.TipoAlta == "Consultor" ? "Revendedor" : s.TipoAlta,
                               s.FechaSolicitud,
                               s.GrupoAlta,
                               Estado = s.EsRepetido.HasValue && s.EsRepetido.Value ? s.MensajeRepetido.Contains("solicitud") ? "Solicitud Repetida" : "Cliente Existente" : "Solicitud Exitosa",
                               Activo = !s.EsRepetido.HasValue ? s.FechaBaja.HasValue ? "NO" : "SI" : "-"
                           }).ToList();


        gvAltas.ExportToExcelFunction("AltasSolicitadas", solicitudes);
    }

    #region Web Metodos para los Despachos

    [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> FiltroAltas(string FechaInicial, string FechaFinal, string Cliente, string Dni, string Grupo, int skip, int take, bool esCambioPagina)
    {

        Dictionary<string, object> datos = new Dictionary<string, object>();
        long idCliente = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
        List<SolicitudesAlta> solicitudesAlta = new List<SolicitudesAlta>();
        try
        {
            if (!esCambioPagina)
            {
                string cadenaWhere = "";
                List<object> parametros = new List<object>();

                cadenaWhere = " 1=1 " + ArmarFilro(FechaInicial, FechaFinal, Cliente, Dni, Grupo, ref parametros);


                solicitudesAlta = Contexto.ExecuteQuery<SolicitudesAlta>(@"select s.* from SolicitudesAlta as s  " +
                        @" where " + cadenaWhere, parametros.ToArray()).OrderByDescending(w => w.FechaSolicitud).ToList();

                HttpContext.Current.Session["SolicitudesFiltradas"] = solicitudesAlta;
            }
            else
            {
                solicitudesAlta = (List<SolicitudesAlta>)HttpContext.Current.Session["SolicitudesFiltradas"];
            }
        }
        catch
        {


        }
        var solicitudes = (from s in solicitudesAlta
                           select new
                               {
                                   s.IdSolicitudAlta,
                                   s.Nombre,
                                   s.DNI,
                                   Provincia = s.Provincia.ToUpper(),
                                   Localidad = s.Localidad.ToUpper(),
                                   TipoAlta = s.TipoAlta == "Consultor" ? "Revendedor" : s.TipoAlta,
                                   s.FechaSolicitud,
                                   s.GrupoAlta,
                                   Estado = s.EsRepetido.HasValue && s.EsRepetido.Value ? s.MensajeRepetido.Contains("solicitud") ? "Solicitud Repetida" : "Cliente Existente" : "Solicitud Exitosa",
                                   Activo = !s.EsRepetido.HasValue ? s.FechaBaja.HasValue ? "NO" : "SI" : "-"
                               }).Skip(skip).Take(take);

        //FiltroAltasCount(FechaInicial, FechaFinal, Cliente, Dni, Grupo).ToString()
        datos.Add("Cantidad", solicitudesAlta.Count);
        datos.Add("Altas", solicitudes.ToList());


        return datos;

    }


    [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> BajaCliente(string idSolicitud, int skip, int take)
    {

        Dictionary<string, object> datos = new Dictionary<string, object>();
        long idSolicitudAlta = long.Parse(idSolicitud);
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
        Marzzan_BejermanDataContext ContextoBejerman = (Marzzan_BejermanDataContext)HttpContext.Current.Session["ContextoBejerman"];
        List<SolicitudesAlta> solicitudesAlta = (List<SolicitudesAlta>)HttpContext.Current.Session["SolicitudesFiltradas"];


        /// 1. cambio el tipo de alta en la base web en la tabla de solicitudes
        SolicitudesAlta solCambio = solicitudesAlta.Where(w => w.IdSolicitudAlta == idSolicitudAlta).FirstOrDefault();
        solCambio.FechaBaja = DateTime.Now;

        /// 2. Cambio en la web en la tabla el estado de habilitado para que no lo pueda utilizar.
        Cliente cliWeb = (from c in Contexto.Clientes
                                where c.CodigoExterno == solCambio.CodigoBejerman
                                select c).FirstOrDefault();

        if (cliWeb != null)
            cliWeb.Habilitado = false;


        /// 3. Grabo los datos
        Contexto.SubmitChanges();


        /// 4. Cambio en Bejerman el tipo de solicitud
        Clientes cliBejerman = (from c in ContextoBejerman.Clientes
                                where c.cli_Cod == solCambio.CodigoBejerman
                                select c).FirstOrDefault();

        cliBejerman.cli_Habilitado = false;

        /// 5. Grabo los datos
        ContextoBejerman.SubmitChanges();


        // 6. Reconsulto vuelvo a cargar la grilla para reflejar los cambios.
        var solicitudes = (from s in solicitudesAlta
                           select new
                               {
                                   s.IdSolicitudAlta,
                                   s.Nombre,
                                   s.DNI,
                                   Provincia = s.Provincia.ToUpper(),
                                   Localidad = s.Localidad.ToUpper(),
                                   TipoAlta = s.TipoAlta == "Consultor" ? "Revendedor" : s.TipoAlta,
                                   s.FechaSolicitud,
                                   s.GrupoAlta,
                                   Estado = s.EsRepetido.HasValue && s.EsRepetido.Value ? s.MensajeRepetido.Contains("solicitud") ? "Solicitud Repetida" : "Cliente Existente" : "Solicitud Exitosa",
                                   Activo = !s.EsRepetido.HasValue ? s.FechaBaja.HasValue ? "NO" : "SI" : "-"
                               }).Skip(skip).Take(take);

        datos.Add("Cantidad", solicitudesAlta.Count);
        datos.Add("Altas", solicitudes.ToList());


        return datos;

    }

    [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> CambiarEstado(string idSolicitud, int skip, int take)
    {

        Dictionary<string, object> datos = new Dictionary<string, object>();
        long idCliente = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
        long idSolicitudAlta = long.Parse(idSolicitud);
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
        Marzzan_BejermanDataContext ContextoBejerman = (Marzzan_BejermanDataContext)HttpContext.Current.Session["ContextoBejerman"];



        List<SolicitudesAlta> solicitudesAlta = (List<SolicitudesAlta>)HttpContext.Current.Session["SolicitudesFiltradas"];

        /// cambio el tipo de alta en la base web en la tabla de solicitudes
        SolicitudesAlta solCambio = solicitudesAlta.Where(w => w.IdSolicitudAlta == idSolicitudAlta).FirstOrDefault();
        solCambio.TipoAlta = "Consultor";
        solCambio.FechaCambioEstado = DateTime.Now;
        Contexto.SubmitChanges();


        /// Cambio en Bejerman el tipo de solicitud
        Clientes cliBejerman = (from c in ContextoBejerman.Clientes
                                where c.cli_Cod == solCambio.CodigoBejerman
                                select c).FirstOrDefault();

        cliBejerman.clitic_Cod = ((int)TipoAltaCliente.Revendedor).ToString();
        ContextoBejerman.SubmitChanges();


        // Reconsulto vuelvo a cargar la grilla para reflejar los cambios.
        var solicitudes = (from s in solicitudesAlta
                           select new
                               {
                                   s.IdSolicitudAlta,
                                   s.Nombre,
                                   s.DNI,
                                   Provincia = s.Provincia.ToUpper(),
                                   Localidad = s.Localidad.ToUpper(),
                                   TipoAlta = s.TipoAlta == "Consultor" ? "Revendedor" : s.TipoAlta,
                                   s.FechaSolicitud,
                                   s.GrupoAlta,
                                   Estado = s.EsRepetido.HasValue && s.EsRepetido.Value ? s.MensajeRepetido.Contains("solicitud") ? "Solicitud Repetida" : "Cliente Existente" : "Solicitud Exitosa",
                                   Activo = !s.EsRepetido.HasValue ? s.FechaBaja.HasValue ? "NO" : "SI" : "-"
                               }).Skip(skip).Take(take);

        datos.Add("Cantidad", solicitudesAlta.Count);
        datos.Add("Altas", solicitudes.ToList());


        return datos;

    }



    private static string ArmarFilro(string fechaInicial, string fechaFinal, string cliente, string dni, string grupo, ref List<object> parametros)
    {
        CabeceraGuia a = new CabeceraGuia();
        DetalleGuia b = new DetalleGuia();

        string cadenaWhere = "";
        int i = 0;

        if (fechaInicial != "")
        {
            cadenaWhere += " and s.FechaSolicitud >= {" + i + "}";
            parametros.Add(Convert.ToDateTime(fechaInicial));
            i++;
        }

        if (fechaFinal != "")
        {
            cadenaWhere += " and s.FechaSolicitud  <= {" + i + "}";
            parametros.Add(Convert.ToDateTime(fechaFinal).AddDays(1));
            i++;
        }

        if (cliente != "")
        {
            cadenaWhere += " and s.Nombre like {" + i + "}";
            parametros.Add("%" + cliente + "%");
            i++;
        }


        if (dni != "")
        {
            cadenaWhere += " and s.DNI like {" + i + "}";
            parametros.Add("%" + dni + "%");
            i++;
        }

        if (grupo != "")
        {
            if (grupo == "Todos")
            {
                cadenaWhere += " and s.GrupoAlta in ({" + i + "})";
                parametros.Add(string.Join(",", (HttpContext.Current.Session["GruposHibilitados"] as List<string>).ToArray()));
                i++;
            }
            else
            {
                cadenaWhere += " and s.GrupoAlta = {" + i + "}";
                parametros.Add(grupo);
                i++;
            }

        }
        else
        {
            cadenaWhere += " and s.GrupoAlta in ({" + i + "})";
            parametros.Add(string.Join(",", (HttpContext.Current.Session["GruposHibilitados"] as List<string>).ToArray()));
            i++;
        }


        return cadenaWhere;
    }
    #endregion
}
