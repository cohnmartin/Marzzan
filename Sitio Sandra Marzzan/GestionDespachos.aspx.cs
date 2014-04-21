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

public partial class GestionDespachos : BasePage
{

    public class tempDespachos
    {
        public string Estado { get; set; }
        public string Transporte { get; set; }
        public DateTime FechaGeneracion { get; set; }
        public DateTime FechaEnvio { get; set; }
        public long IdCabeceraGuia { get; set; }
        public long NroDespacho { get; set; }
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
            gvDespachos.DataSource = (List<tempDespachos>)FiltroDespachos("", "", "", "", "", "", 0, 15)["Guias"];
        }
    }


    #region Web Metodos para las Guias

    [WebMethod(EnableSession = true)]
    public static object ObtenerGuias(string id)
    {
        Dictionary<string, object> datos = new Dictionary<string, object>();
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
        long idcabecera = long.Parse(id);

        var guias = (from d in Contexto.DetalleGuias
                     where d.CabeceraGuia == idcabecera
                     select new
                     {
                         Codigo = d.Codigo.Value.ToString().PadLeft(6, '0'),
                         d.Consultor,
                         d.CCRR,
                         d.Direccion,
                         d.Localidad,
                         d.Provincia,
                         d.TotalPedido,
                         TotalPagar = d.TotalPagar.HasValue ? d.TotalPagar.Value : 0,
                         d.Comprobante,
                         d.IdDetalleGuia
                     }).Take(15).ToList();


        datos.Add("Guias", guias);
        return datos;
    }

    [WebMethod(EnableSession = true)]
    public static object BuscarGuias(string id, string consultor)
    {
        Dictionary<string, object> datos = new Dictionary<string, object>();
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
        long idcabecera = long.Parse(id);

        var guias = (from d in Contexto.DetalleGuias
                     where d.CabeceraGuia == idcabecera
                     && d.Consultor.StartsWith(consultor)
                     select new
                     {
                         Codigo = d.Codigo.Value.ToString().PadLeft(6, '0'),
                         d.Consultor,
                         d.CCRR,
                         d.Direccion,
                         d.Localidad,
                         d.Provincia,
                         d.TotalPedido,
                         TotalPagar = d.TotalPagar.HasValue ? d.TotalPagar.Value : 0,
                         d.Comprobante,
                         d.IdDetalleGuia
                     }).Take(15).ToList();


        datos.Add("Guias", guias);
        return datos;
    }

    #endregion

    #region Web Metodos para los Despachos



    [WebMethod(EnableSession = true)]
    public static object AgregarArchivoDespacho(string fileName)
    {
        string fullpath = HttpContext.Current.Server.MapPath(@"~/ArchivosDespacho/" + fileName);

        #region Lectura Archivo Excel

        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fullpath + ";Extended Properties=Excel 8.0");
        OleDbCommand command = new OleDbCommand("SELECT * FROM [Hoja1$]", connection);
        OleDbDataReader dr;
        try
        {

            connection.Open();
            dr = command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch
        {

            connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fullpath + ";Extended Properties=Excel 8.0");
            command = new OleDbCommand("SELECT * FROM [Sheet1$]", connection);
            connection.Open();
            dr = command.ExecuteReader(CommandBehavior.CloseConnection);

        }



        DataTable excelData = new DataTable("ExcelData");
        excelData.Load(dr);

        #endregion

        #region Generacion Registros en Base

        
            CabeceraGuia newCabecera = new CabeceraGuia();
            newCabecera.FechaGeneracion = DateTime.Now;
            newCabecera.Usuario = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
            newCabecera.Transporte = excelData.Rows[0]["Transporte"].ToString().Trim().ToUpper();
            newCabecera.FechaEnvio = DateTime.Parse(excelData.Rows[0]["Fecha"].ToString());

            newCabecera.EstadoCabecera = (from p in (HttpContext.Current.Session["Context"] as Marzzan_InfolegacyDataContext).Parametros
                                          where p.Tipo == "ESTADODESPACHO" && p.Valor == "INICIAL"
                                          select p.IdParametro).FirstOrDefault();

            newCabecera.NroDespacho = (from cd in (HttpContext.Current.Session["Context"] as Marzzan_InfolegacyDataContext).CabeceraGuias
                                       select cd.NroDespacho).Max() + 1;

            newCabecera.NroDespacho = newCabecera.NroDespacho == null ? 1 : newCabecera.NroDespacho;

            newCabecera.Nombre = fileName;

            foreach (DataRow item in excelData.Rows)
            {
                DetalleGuia newguia = new DetalleGuia();

                newguia.Codigo = int.Parse(item["CODIGO"].ToString());
                newguia.Consultor = item["CONSULTOR"].ToString();
                newguia.NroDoc = item["NRODOC"].ToString();
                newguia.CCRR = item["CCRR"].ToString();
                newguia.Direccion = item["DIRECCION"].ToString();
                newguia.Localidad = item["LOCALIDAD"].ToString();
                newguia.CodPos = item["CODPOS"].ToString();
                newguia.Provincia = item["PROVINCIA"].ToString();
                newguia.Telefono = item["TELEFONO"].ToString();
                newguia.Bultos = int.Parse(item["BULTOS"].ToString());
                newguia.Tamaño = item["TAMAÑO"].ToString();
                newguia.TotalPedido = item["TOTAL_PEDIDO"].ToString() != "" ? decimal.Parse(item["TOTAL_PEDIDO"].ToString()) : 0;
                newguia.SaldoCtaCte = item["SALDOCTACTE"].ToString() != "" ? decimal.Parse(item["SALDOCTACTE"].ToString()) : 0;
                newguia.Control = item["CONTROL"].ToString() != "" ? decimal.Parse(item["CONTROL"].ToString()) : 0;
                newguia.TotalPagar = item["TOTAL_A_PAGAR"].ToString() != "" ? decimal.Parse(item["TOTAL_A_PAGAR"].ToString()) : 0;
                newguia.ValDeclarado = item["VAL_DECLA"].ToString() != "" ? decimal.Parse(item["VAL_DECLA"].ToString()) : 0;
                newguia.DetPagar = item["DETPAGAR"].ToString();
                newguia.Mensaje = item["MENSAJE"].ToString();
                newguia.Observacion = item["OBSERVACIONES"].ToString();
                newguia.Comprobante = item["COMPROBANTE"].ToString();
                newguia.Lider = item["COORDINADOR"].ToString();
                newguia.TelefonoLider = item["TELEFONOLIDER"].ToString();
                newguia.SCVTRNCOD = int.Parse(item["SCVTRN_COD"].ToString());
                newguia.IdBejerman = long.Parse(item["ID"].ToString());
                newguia.SPVTCOCOD = item["SPVTCO_COD"].ToString();
                newguia.PesoEstimado = item["PESOESTIMADO"].ToString() != "" ? decimal.Parse(item["PESOESTIMADO"].ToString()) : 0;
                newguia.Rotulo = item["ROTULO"].ToString();
                newguia.objCabeceraGuia = newCabecera;
                
                newguia.objEstadoLogistica = (from p in (HttpContext.Current.Session["Context"] as Marzzan_InfolegacyDataContext).Parametros
                                     where p.Tipo == "ESTADOGUIA" && p.Valor == EstadoLogisticaGuias.PENDIENTE
                                  select p).FirstOrDefault();
                
                newCabecera.colDetalleGuias.Add(newguia);
            }

            (HttpContext.Current.Session["Context"] as Marzzan_InfolegacyDataContext).CabeceraGuias.InsertOnSubmit(newCabecera);
            (HttpContext.Current.Session["Context"] as Marzzan_InfolegacyDataContext).SubmitChanges();
        
        #endregion

        return (List<tempDespachos>)FiltroDespachos("", "", "", "", "", "", 0, 15)["Guias"];
    }

    [WebMethod(EnableSession = true)]
    public static object EliminarDespacho(string id)
    {
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
        long idcabecera = long.Parse(id);

        var cabecera = (from d in Contexto.CabeceraGuias
                        where d.IdCabeceraGuia == idcabecera
                        select d).FirstOrDefault();


        Contexto.DetalleGuias.DeleteAllOnSubmit(cabecera.colDetalleGuias);
        Contexto.CabeceraGuias.DeleteOnSubmit(cabecera);
        Contexto.SubmitChanges();

        return (List<tempDespachos>)FiltroDespachos("", "", "", "", "", "", 0, 15)["Guias"];
    }

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
                             Transporte = d.Transporte
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
            cadenaWhere += " and c.Transporte like {" + i + "}";
            parametros.Add("%" + transporte + "%");
            i++;
        }


        return cadenaWhere;
    }
    #endregion
}
