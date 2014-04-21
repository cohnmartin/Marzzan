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

public partial class ConsultaEstadoGuias : BasePage
{
    public class tempGuias
    {

        public string Nombre { get; set; }
        public string TipoComprobante { get; set; }
        public string Direccion { get; set; }
        public string Localidad { get; set; }
        public string Monto { get; set; }
        public string Lider { get; set; }
        public string LiderTel { get; set; }
        public string Comprobante { get; set; }
        public string Estado { get; set; }
        public string EstadoTip { get; set; }
        public long IdDetalleGuia { get; set; }
        public DateTime? FechaVisita1 { get; set; }
        public DateTime? FechaVisita2 { get; set; }
        public DateTime? FechaVisita3 { get; set; }
        public string Observacion1 { get; set; }
        public string Observacion2 { get; set; }
        public string Observacion3 { get; set; }
        public DateTime? FechaCobranza { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string Transporte { get; set; }
        public string EstadoAuto { get; set; }
        public string EstadoAutoTip { get; set; }
        public DateTime? FechaAuto { get; set; }
        public string obsAuto { get; set; }
        public string usuarioAuto { get; set; }
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
            Session["DatosFiltrados"] = null;

            cboTransportes.DataTextField = "data";
            cboTransportes.DataValueField = "value";
            cboTransportes.AppendDataBoundItems = true;
            cboTransportes.DataSource = (from t in Contexto.View_Transportes
                                         select new
                                         {
                                             data = t.Transporte,
                                             value = t.Transporte
                                         }).Distinct().OrderBy(w => w.data).ToList();
            cboTransportes.DataBind();


            cboEstados.DataTextField = "data";
            cboEstados.DataValueField = "value";
            cboEstados.AppendDataBoundItems = true;
            cboEstados.DataSource = (from t in Contexto.Parametros
                                     where t.Tipo == "ESTADOGUIA"
                                     select new
                                     {
                                         data = t.Valor,
                                         value = t.IdParametro
                                     }).Distinct().OrderBy(w => w.data).ToList();
            cboEstados.DataBind();

            lblUsuarioAutorizacion.Text = Session["NombreUsuario"].ToString();


        }
    }

    public void ExportToExcelFunction(object sender, ImageClickEventArgs arg)
    {
        GridView gv = new GridView();
        gv.AutoGenerateColumns = false;
        List<tempGuias> _dataSourceExcel = (List<tempGuias>)HttpContext.Current.Session["DatosFiltrados"];
        List<string> camposExcluir = new List<string>() { "IdDetalleGuia", "Estado", "EstadoAuto" };

        if (_dataSourceExcel != null)
        {
            var someObject = _dataSourceExcel[0];
            foreach (System.Reflection.PropertyInfo item in someObject.GetType().GetProperties())
            {
                if (!camposExcluir.Contains(item.Name))
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = item.Name;
                    boundField.HeaderText = item.Name;
                    boundField.NullDisplayText = "";
                    gv.Columns.Add(boundField);
                }

            }


            //if (_dataSourceExcel[0].GetType() == typeof(DataRow))
            //    gv.DataSource = ((System.Data.DataRow)(_dataSourceExcel[0])).Table.AsDataView();
            //else
            gv.DataSource = _dataSourceExcel;
            gv.DataBind();


            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gv.RenderControl(htmlWrite);

            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=EstadoGuias_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".xls");
            HttpContext.Current.Response.ContentType = "application/xls";
            HttpContext.Current.Response.Write(stringWrite.ToString());
            HttpContext.Current.Response.End();


        }


    }

    #region Web Metodos para los Guias


    [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> AutorizarGuia(string Fecha, string obs, string idGuia)
    {

        Dictionary<string, object> datos = new Dictionary<string, object>();
        long idCliente = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];

        try
        {
            DetalleGuia guia = (from g in Contexto.DetalleGuias
                                where g.IdDetalleGuia == long.Parse(idGuia)
                                select g).FirstOrDefault();


            guia.FechaAutorizacion = DateTime.Parse(Fecha);
            guia.ObservacionAutorizacion = obs;
            guia.objUsuarioAutorizacion = Contexto.Clientes.Where(w => w.IdCliente == idCliente).FirstOrDefault();
            Contexto.SubmitChanges();

            //Actualizo la variable en memoria, para no tener que reconsultar.
            List<tempGuias> tmp = (List<tempGuias>)HttpContext.Current.Session["DatosFiltrados"];
            tmp.Where(w => w.IdDetalleGuia == long.Parse(idGuia)).FirstOrDefault().EstadoAuto = "TildeVerde.png";
            tmp.Where(w => w.IdDetalleGuia == long.Parse(idGuia)).FirstOrDefault().EstadoAutoTip = "Autorizado";
            tmp.Where(w => w.IdDetalleGuia == long.Parse(idGuia)).FirstOrDefault().FechaAuto = DateTime.Parse(Fecha);
            tmp.Where(w => w.IdDetalleGuia == long.Parse(idGuia)).FirstOrDefault().obsAuto = obs;
            tmp.Where(w => w.IdDetalleGuia == long.Parse(idGuia)).FirstOrDefault().usuarioAuto = HttpContext.Current.Session["NombreUsuario"].ToString();

            datos.Add("Cantidad", tmp.Count);
            datos.Add("Guias", tmp);

        }
        catch
        {


        }

        return datos;

    }

    [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> FiltroDespachos(string NroDespacho, string FechaInicial, string FechaFinal, string Tansporte, string Estado, string NroGuia, int skip, int take)
    {

        Dictionary<string, object> datos = new Dictionary<string, object>();
        long idCliente = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];

        try
        {
            string cadenaWhere = "";
            List<object> parametros = new List<object>();

            cadenaWhere = " 1=1 " + ArmarFilro(NroDespacho, FechaInicial, FechaFinal, Tansporte, long.Parse(Estado), NroGuia, ref parametros);


            var guiasTemp = Contexto.ExecuteQuery<DetalleGuia>(@"select distinct d.* from CabeceraGuias as c inner join DetalleGuias d " +
                    @" on c.IdCabeceraGuia = d.CabeceraGuia " +
                    @" where " + cadenaWhere + " order by consultor", parametros.ToArray()).OrderByDescending(w => w.objCabeceraGuia.FechaEnvio).Skip(skip).Take(take).ToList();

            var guias = (from d in guiasTemp
                         select new tempGuias
                         {
                             Nombre = d.Consultor,
                             TipoComprobante = d.CCRR,
                             Direccion = d.Direccion,
                             Localidad = d.Localidad != null ? d.Localidad.ToUpper() : "&nbsp;",
                             Monto = string.Format("{0:###,##0.00}", d.TotalPagar),
                             Lider = d.Lider,
                             LiderTel = d.TelefonoLider,
                             Comprobante = d.Comprobante,
                             Estado = d.FechaCobranza.HasValue ? "GreenBall.png" : d.FechaDevolucion.HasValue ? "BlueBall.png" : !d.FechaVisita1.HasValue && !d.FechaVisita2.HasValue && !d.FechaVisita3.HasValue ? "RedBall.png" : "YellowBall.png",
                             EstadoTip = d.FechaCobranza.HasValue ? "ENTREGADO EXITOSAMENTE" : d.FechaDevolucion.HasValue ? "DEVUELTO AL ORIGEN" : !d.FechaVisita1.HasValue && !d.FechaVisita2.HasValue && !d.FechaVisita3.HasValue ? "SIN INFORMACION DE COBRANZA" : "VISITADO SIN ENTREGA",
                             IdDetalleGuia = d.IdDetalleGuia,
                             FechaVisita1 = d.FechaVisita1,
                             FechaVisita2 = d.FechaVisita2,
                             FechaVisita3 = d.FechaVisita3,
                             Observacion1 = d.Observacion1,
                             Observacion2 = d.Observacion2,
                             Observacion3 = d.Observacion3,
                             FechaCobranza = d.FechaCobranza,
                             FechaEnvio = d.objCabeceraGuia.FechaEnvio.Value,
                             Transporte = d.objCabeceraGuia.Transporte,
                             EstadoAuto = d.FechaAutorizacion.HasValue ? "TildeVerde.png" : "TildeRojo.png",
                             EstadoAutoTip = d.FechaAutorizacion.HasValue ? "Autorizado" : "Sin Autorizar",
                             FechaAuto = d.FechaAutorizacion,
                             obsAuto = d.ObservacionAutorizacion,
                             FechaDevolucion= d.FechaDevolucion,
                             usuarioAuto = d.objUsuarioAutorizacion != null ? d.objUsuarioAutorizacion.Nombre : ""

                         }).Distinct().ToList();

            datos.Add("Cantidad", guias.Count());
            datos.Add("Guias", guias);
            HttpContext.Current.Session.Add("DatosFiltrados", guias);

        }
        catch
        {


        }




        return datos;

    }

    private static string ArmarFilro(string nroDespacho, string fechaInicial, string fechaFinal, string transporte, long estado, string nroGuia, ref List<object> parametros)
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
            cadenaWhere += " and c.FechaEnvio < {" + i + "}";
            parametros.Add(Convert.ToDateTime(fechaFinal).AddDays(1));
            i++;
        }

        if (nroDespacho != "")
        {
            cadenaWhere += " and c.NroDespacho = {" + i + "}";
            parametros.Add(long.Parse(nroDespacho));
            i++;
        }


        if (transporte != "" && transporte != "Todos")
        {
            cadenaWhere += " and c.Transporte like {" + i + "}";
            parametros.Add("%" + transporte + "%");
            i++;
        }

        if (estado > 0)
        {
            cadenaWhere += " and d.Estado = {" + i + "}";
            parametros.Add(estado);
            i++;
        }

        if (nroGuia != "")
        {
            cadenaWhere += " and d.Comprobante like {" + i + "}";
            parametros.Add("%" + nroGuia + "%");
            i++;
        }


        return cadenaWhere;
    }

    #endregion

}
