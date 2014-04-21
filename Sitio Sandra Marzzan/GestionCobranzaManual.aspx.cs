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

public partial class GestionCobranzaManual : BasePage
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
        public int CantidadGuias { get; set; }

        public int GuiasSinRendir { get; set; }
        public int GuiasSinAprobar { get; set; }

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
            gvDespachos.DataSource = (List<tempDespachos>)FiltroDespachos("", "", "", "Todos", 0, 15)["Guias"];
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

            var datosVisitas = (from t in Contexto.Parametros
                                where t.Tipo == "ESTADOVISITAS"
                                orderby t.Valor
                                select new
                                {
                                    data = t.Valor,
                                    value = t.IdParametro
                                }).ToList();


            var estadosGuias = (from t in Contexto.Parametros
                                where t.Tipo == "ESTADOLOGISTICACOBRANZA"
                                orderby t.Valor
                                select new
                                {
                                    data = t.Valor,
                                    value = t.IdParametro,
                                    contexto = t.Contexto
                                }).ToList();


            var lideres = (from C in Contexto.Clientes
                                          where C.CodTipoCliente != "1" && C.CodTipoCliente != "12"
                                          && C.Habilitado.Value
                                          orderby C.Nombre
                                          select new
                                          {
                                              C.IdCliente,
                                              C.Nombre
                                          }).ToList();

            cboLideres.DataTextField = "Nombre";
            cboLideres.DataValueField = "IdCliente";
            cboLideres.DataSource =lideres;
            cboLideres.DataBind();

            cboLideresSimple.DataTextField = "Nombre";
            cboLideresSimple.DataValueField = "IdCliente";
            cboLideresSimple.DataSource = lideres;
            cboLideresSimple.DataBind(); 

            cboObservacion1.DataTextField = "data";
            cboObservacion1.DataValueField = "value";
            cboObservacion1.DataSource = datosVisitas;
            cboObservacion1.DataBind();

            cboObservacion2.DataTextField = "data";
            cboObservacion2.DataValueField = "value";
            cboObservacion2.DataSource = datosVisitas;
            cboObservacion2.DataBind();

            cboObservacion3.DataTextField = "data";
            cboObservacion3.DataValueField = "value";
            cboObservacion3.DataSource = datosVisitas;
            cboObservacion3.DataBind();

            cboEstadoGuias.DataTextField = "data";
            cboEstadoGuias.DataValueField = "value";
            cboEstadoGuias.DataSource = estadosGuias.Where(w => w.contexto == "Fisico");
            cboEstadoGuias.DataBind();


            cboEstadosGuiasFiltro.DataTextField = "data";
            cboEstadosGuiasFiltro.DataValueField = "value";
            cboEstadosGuiasFiltro.DataSource = estadosGuias;
            cboEstadosGuiasFiltro.DataBind();

            cboEstadoMasivo.DataTextField = "data";
            cboEstadoMasivo.DataValueField = "value";
            cboEstadoMasivo.DataSource = estadosGuias.Where(w => w.contexto == "Fisico");
            cboEstadoMasivo.DataBind();



        }
    }

    #region Web Metodos para los Despachos

    [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> FiltroDespachos(string NroDespacho, string FechaInicial, string FechaFinal, string Tansporte, int skip, int take)
    {

        Dictionary<string, object> datos = new Dictionary<string, object>();
        long idCliente = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
        List<tempDespachos> despachos = new List<tempDespachos>();
        try
        {
            string cadenaWhere = "";
            List<object> parametros = new List<object>();

            cadenaWhere = " 1=1 " + ArmarFilro(NroDespacho, FechaInicial, FechaFinal, Tansporte, ref parametros);


            var despachosTemp = Contexto.ExecuteQuery<CabeceraGuia>(@"select distinct c.* from CabeceraGuias as c inner join DetalleGuias d " +
                    @" on c.IdCabeceraGuia = d.CabeceraGuia " +
                    @" where " + cadenaWhere, parametros.ToArray()).OrderByDescending(w => w.FechaEnvio).Skip(skip).Take(take).ToList();


            List<long> idsCab = despachosTemp.Select(w => w.IdCabeceraGuia).Distinct().ToList();


            var estadoCobranzaGuias = (from g in Contexto.DetalleGuias
                                       where g.CabeceraRendicion != null
                                       && idsCab.Contains(g.CabeceraGuia.Value)
                                       group g by new { NroDespacho = g.objCabeceraGuia.NroDespacho.Value, Estado = g.objEstadoRendicion.Valor } into t
                                       select new
                                       {
                                           NroDespacho = t.Key.NroDespacho,
                                           EstadoCobranza = t.Key.Estado,
                                           Cantidad = t.Count()
                                       }).ToList();



            despachos = (from d in despachosTemp
                         select new tempDespachos
                         {
                             CantidadGuias = d.colDetalleGuias.Count,
                             Estado = "",
                             FechaEnvio = d.FechaEnvio.Value,
                             FechaGeneracion = d.FechaGeneracion.Value,
                             IdCabeceraGuia = d.IdCabeceraGuia,
                             NroDespacho = d.NroDespacho.Value,
                             Transporte = d.Transporte,
                             Archivo = d.Nombre,
                             GuiasSinRendir = estadoCobranzaGuias.Where(w => w.NroDespacho == d.NroDespacho.Value && (w.EstadoCobranza == "PRESENTADA" || w.EstadoCobranza == "APROBADA")).FirstOrDefault() != null ? d.colDetalleGuias.Count - estadoCobranzaGuias.Where(w => w.NroDespacho == d.NroDespacho.Value && (w.EstadoCobranza == "PRESENTADA" || w.EstadoCobranza == "APROBADA")).Sum(w => w.Cantidad) : d.colDetalleGuias.Count,
                             GuiasSinAprobar = estadoCobranzaGuias.Where(w => w.NroDespacho == d.NroDespacho.Value && w.EstadoCobranza == "APROBADA").FirstOrDefault() != null ? d.colDetalleGuias.Count - estadoCobranzaGuias.Where(w => w.NroDespacho == d.NroDespacho.Value && w.EstadoCobranza == "APROBADA").FirstOrDefault().Cantidad : d.colDetalleGuias.Count,
                         }).Distinct().ToList();

        }
        catch
        {


        }

        datos.Add("Cantidad", despachos.Count());
        datos.Add("Guias", despachos);


        return datos;

    }


    private static string ArmarFilro(string nroDespacho, string fechaInicial, string fechaFinal, string transporte, ref List<object> parametros)
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
    public static object updateGuia(long id, string visita1, string visita2, string visita3, string obs1, string obs2, string obs3, string fechaEstado, long estadoLogistica, long? lider, string motivoSiniestro, string descEstado)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();


        DetalleGuia guia = (from d in dc.DetalleGuias
                            where d.IdDetalleGuia == id
                            select d).FirstOrDefault();

        if (visita1 != "")
            guia.FechaVisita1 = DateTime.Parse(visita1);

        if (visita2 != "")
            guia.FechaVisita2 = DateTime.Parse(visita2);

        if (visita3 != "")
            guia.FechaVisita3 = DateTime.Parse(visita3);


        guia.Observacion1 = obs1;
        guia.Observacion2 = obs2;
        guia.Observacion3 = obs3;

        if (estadoLogistica != -1)
        {
            guia.FechaEstadoLogistica = DateTime.Parse(fechaEstado);
            guia.EstadoLogistica = estadoLogistica;

            if (descEstado.Contains("LIDER"))
            {
                guia.LiderResponsableGuia = lider;
                guia.MotivoSiniestro = "";
            }
            else if (descEstado.Contains("SINIESTR"))
            {
                guia.LiderResponsableGuia = null;
                guia.MotivoSiniestro = motivoSiniestro;
            }
            else
            {
                guia.LiderResponsableGuia = null;
                guia.MotivoSiniestro = "";
            }

        }
        else
        {
            if (visita1 != "" || visita2 != "" || visita3 != "")
            {
                guia.objEstadoLogistica = (from p in dc.Parametros
                                           where p.Tipo == "ESTADOLOGISTICACOBRANZA" && p.Valor == EstadoLogisticaGuias.VISITADO
                                           select p).FirstOrDefault();
            }
        }

        dc.SubmitChanges();
        return GetGuias(guia.CabeceraGuia.Value);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object GetGuias(long id)
    {
        using (Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext())
        {
            var detalleGuias = (from d in dc.DetalleGuias
                               where d.CabeceraGuia == id
                               select new
                               {
                                   Nombre = d.Consultor,
                                   TipoComprobante = d.CCRR,
                                   Direccion = d.Direccion,
                                   Localidad = d.Localidad != null ? d.Localidad.ToUpper() : "&nbsp;",
                                   Monto = string.Format("{0:###,##0.00}", d.TotalPagar),
                                   Lider = d.Lider != "" ? d.Lider : "&nbsp;",
                                   LiderTel = d.TelefonoLider != "" ? d.TelefonoLider : "&nbsp;",
                                   Comprobante = d.Comprobante,
                                   Estado = d.objEstadoLogistica != null ? d.objEstadoLogistica.Valor : "P - PENDIENTE",
                                   EstadoTip = d.objEstadoLogistica != null ? d.objEstadoLogistica.Valor : "P - PENDIENTE",
                                   d.IdDetalleGuia,
                                   d.FechaVisita1,
                                   d.FechaVisita2,
                                   d.FechaVisita3,
                                   d.Observacion1,
                                   d.Observacion2,
                                   d.Observacion3,
                                   d.FechaEstadoLogistica,
                                   d.objEstadoRendicion,
                                   IdRendicion = d.CabeceraRendicion,
                                   LiderResponsable = d.objLiderResponsable,
                                   MotivoSiniestro = d.MotivoSiniestro
                               }).ToList().OrderBy(w => w.Nombre);


            var detalleGuiasFormateadas = (from d in detalleGuias
                           select new
                           {
                               d.Nombre,
                               d.TipoComprobante,
                               d.Direccion,
                               d.Localidad,
                               d.Monto,
                               d.Lider,
                               d.LiderTel,
                               d.Comprobante,
                               EstadoAbreviatura = d.Estado.Split('-')[0].Trim(),
                               Estado= d.Estado,
                               EstadoTip = d.LiderResponsable == null ? d.EstadoTip : d.EstadoTip + " (" + d.LiderResponsable.Nombre +  ")",
                               d.IdDetalleGuia,
                               d.FechaVisita1,
                               d.FechaVisita2,
                               d.FechaVisita3,
                               d.Observacion1,
                               d.Observacion2,
                               d.Observacion3,
                               d.FechaEstadoLogistica,
                               EstadoRendicion = d.objEstadoRendicion != null ? d.objEstadoRendicion.Valor : "",
                               d.IdRendicion,
                               d.MotivoSiniestro,
                               LiderResponsable = d.LiderResponsable == null ? "" :  d.LiderResponsable.Nombre 
                           }).ToList().OrderBy(w => w.Nombre);

            return detalleGuiasFormateadas.ToList();
        }


    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object updateMasivoGuias(List<long> ids, string cobro, long estado, long? lider, string motivo)
    {
        using (Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext())
        {
            List<DetalleGuia> guias = (from d in dc.DetalleGuias
                                       where ids.Contains(d.IdDetalleGuia)
                                       select d).ToList();


            foreach (DetalleGuia guia in guias)
            {
                guia.FechaEstadoLogistica = DateTime.Parse(cobro);
                guia.EstadoLogistica = estado;
                guia.MotivoSiniestro = motivo;
                guia.LiderResponsableGuia = lider;
            }


            dc.SubmitChanges();
            return GetGuias(guias.First().CabeceraGuia.Value);
        }
    }
}
