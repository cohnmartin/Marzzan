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

public partial class ConsultaDespachos : BasePage
{

    public class tempDespachos
    {
        public string Estado { get; set; }
        public string Transporte { get; set; }
        public DateTime FechaGeneracion { get; set; }
        public DateTime FechaEnvio { get; set; }
        public long IdCabeceraGuia { get; set; }
        public long NroDespacho { get; set; }
        public string Archivo { get; set; }
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
            Session["Context"] = new Marzzan_InfolegacyDataContext();
            gvDespachos.DataSource = (List<tempDespachos>)FiltroDespachos("", "", "", "", "", "Todos", 0, 15)["Guias"];
            string transporteLogin = Session["Transporte"].ToString().ToUpper();

            cboTransportes.DataTextField = "data";
            cboTransportes.DataValueField = "value";
            cboTransportes.AppendDataBoundItems = true;
            cboTransportes.DataSource = (from t in Contexto.View_Transportes
                                         where t.Transporte.ToUpper().Contains(transporteLogin)
                                         select new
                                         {
                                             data = t.Transporte,
                                             value = t.Transporte
                                         }).ToList();
            cboTransportes.DataBind();


        }
    }

    #region Web Metodos para los Despachos

    [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> FiltroDespachos(string NroDespacho, string FechaInicial, string FechaFinal, string Cliente, string NroGuia, string Tansporte, int skip, int take)
    {

        Dictionary<string, object> datos = new Dictionary<string, object>();
        long idCliente = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
        List<tempDespachos> despachos = new List<tempDespachos>();
        try
        {
            string cadenaWhere = "";
            List<object> parametros = new List<object>();

            cadenaWhere = " 1=1 " + ArmarFilro(NroDespacho, FechaInicial, FechaFinal, Cliente, NroGuia, Tansporte, ref parametros);


            var despachosTemp = Contexto.ExecuteQuery<CabeceraGuia>(@"select distinct c.* from CabeceraGuias as c inner join DetalleGuias d " +
                    @" on c.IdCabeceraGuia = d.CabeceraGuia " +
                    @" where " + cadenaWhere, parametros.ToArray()).OrderByDescending(w => w.FechaEnvio).Skip(skip).Take(take).ToList();

            despachos = (from d in despachosTemp
                         select new tempDespachos
                         {
                             Estado = "",
                             FechaEnvio = d.FechaEnvio.Value,
                             FechaGeneracion = d.FechaGeneracion.Value,
                             IdCabeceraGuia = d.IdCabeceraGuia,
                             NroDespacho = d.NroDespacho.Value,
                             Transporte = d.Transporte,
                             Archivo = d.Nombre
                         }).Distinct().ToList();

        }
        catch
        {


        }

        datos.Add("Cantidad", despachos.Count());
        datos.Add("Guias", despachos);


        return datos;

    }

    private static string ArmarFilro(string nroDespacho, string fechaInicial, string fechaFinal, string cliente, string nroGuia, string transporte, ref List<object> parametros)
    {
        CabeceraGuia a = new CabeceraGuia();
        DetalleGuia b = new DetalleGuia();

        string cadenaWhere = "";
        int i = 0;

        if (fechaInicial != "")
        {
            cadenaWhere += " and c.FechaEnvio >= {" + i + "}";
            parametros.Add(Convert.ToDateTime(fechaInicial));
            i++;
        }

        if (fechaFinal != "")
        {
            cadenaWhere += " and c.FechaEnvio <= {" + i + "}";
            parametros.Add(Convert.ToDateTime(fechaFinal).AddDays(1));
            i++;
        }

        if (nroDespacho != "")
        {
            cadenaWhere += " and c.NroDespacho = {" + i + "}";
            parametros.Add(long.Parse(nroDespacho));
            i++;
        }

        if (cliente != "")
        {
            cadenaWhere += " and d.Consultor like {" + i + "}";
            parametros.Add("%" + cliente + "%");
            i++;
        }


        if (nroGuia != "")
        {
            cadenaWhere += " and d.Comprobante like {" + i + "}";
            parametros.Add("%" + nroGuia + "%");
            i++;
        }

        if (transporte != "")
        {
            transporte = transporte == "Todos" ? HttpContext.Current.Session["Transporte"].ToString().ToUpper() : transporte;
            cadenaWhere += " and c.Transporte like {" + i + "}";
            parametros.Add("%" + transporte + "%");
            i++;
        }


        return cadenaWhere;
    }
    #endregion



    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object GetConsultores(long id)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();


        var consultores = (from d in dc.DetalleGuias
                           where d.CabeceraGuia == id
                           select new
                           {
                               Nombre = d.Consultor,
                               TipoComprobante = d.CCRR,
                               Direccion = d.Direccion,
                               Localidad = d.Localidad != null ? d.Localidad.ToUpper() : "&nbsp;",
                               Monto = string.Format("{0:###,##0.00}", d.TotalPagar),
                               Lider = d.Lider,
                               LiderTel = d.TelefonoLider,
                               Comprobante = d.Comprobante,
                               Estado = d.FechaEstadoLogistica.HasValue ? "GreenBall.png" : !d.FechaVisita1.HasValue && !d.FechaVisita2.HasValue && !d.FechaVisita3.HasValue ? "RedBall.png" : "YellowBall.png",
                               EstadoTip = d.FechaEstadoLogistica.HasValue ? "Entregado Exitosamente" : !d.FechaVisita1.HasValue && !d.FechaVisita2.HasValue && !d.FechaVisita3.HasValue ? "Sin Informacion de Cobraza" : "Visitado pero sin entrega",
                               d.IdDetalleGuia
                           }).ToList().OrderBy(w => w.Nombre);

        return consultores.ToList();
    }
}
