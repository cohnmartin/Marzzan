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

public partial class ConsultaRendiciones : BasePage
{
    public class tempRendicion
    {
        public string Transporte { get; set; }
        public DateTime FechaRendicion { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public decimal Monto { get; set; }
        public long IdCabeceraRendicion { get; set; }
        public string Estado { get; set; }
        public string TipoRendicion { get; set; }
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

    private int TotalGuias { get; set; }

    protected override void PageLoad()
    {


        if (!IsPostBack)
        {
            Session["Context"] = new Marzzan_InfolegacyDataContext();
            gvRendicion.DataSource = (List<tempRendicion>)FiltroRendicion("", "", "", "Todos", 0, 15)["Rendiciones"];
            string transporteLogin = Session["Transporte"] == null ? "" : Session["Transporte"].ToString().ToUpper();

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

        }

        

        // Solo el transportista puede eliminar una rendición.
        if (Session["Transporte"] == null || Session["Transporte"] == "")
        {
            gvRendicion.FunctionsColumns.RemoveAt(1);
            gvRendicion.FunctionsGral.Clear();
        }
    }

    void gv_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Table t = (Table)(sender as GridView).Controls[0];


            #region Titulo General
            GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);

            TableCell cell = new TableHeaderCell();
            cell.ColumnSpan = 20;
            cell.Font.Size = 30;
            cell.Text = "RENDICION NRO: " + long.Parse(Request.Form["__EVENTARGUMENT"]);
            cell.VerticalAlign = VerticalAlign.Top;
            cell.HorizontalAlign = HorizontalAlign.Left;
            cell.BorderStyle = BorderStyle.None;
            row.Cells.Add(cell);


            cell = new TableHeaderCell();
            cell.ColumnSpan = 14;
            cell.Font.Size = 12;
            cell.HorizontalAlign = HorizontalAlign.Center;
            cell.Text = "Impresión: " + DateTime.Now.ToShortDateString();
            cell.VerticalAlign = VerticalAlign.Top;
            cell.HorizontalAlign = HorizontalAlign.Right;
            cell.BorderStyle = BorderStyle.None;
            row.Cells.Add(cell);

            t.Rows.AddAt(0, row);
            #endregion

            #region Titulo Guias
            row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 20;
            cell.Font.Size = 25;
            cell.Text = "DETALLE GUIAS RENDIDAS";
            cell.VerticalAlign = VerticalAlign.Top;
            cell.HorizontalAlign = HorizontalAlign.Left;
            cell.BorderStyle = BorderStyle.None;
            row.Cells.Add(cell);

            t.Rows.AddAt(1, row);

            #endregion
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            int inicioFila = TotalGuias + 3; // +3 porque hay dos filas de titulos anteriores mas el detalle de las guias
            Table t = (Table)(sender as GridView).Controls[0];

            #region Titulo Pagos
            GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Footer, DataControlRowState.Normal);

            TableCell cell = new TableHeaderCell();
            cell.ColumnSpan = 20;
            cell.Font.Size = 25;
            cell.Text = "DETALLE DE PAGOS";
            cell.VerticalAlign = VerticalAlign.Top;
            cell.HorizontalAlign = HorizontalAlign.Left;
            cell.BorderStyle = BorderStyle.None;
            row.Cells.Add(cell);


            t.Rows.AddAt(inicioFila, row);
            inicioFila++;
            #endregion

            #region Cabecera Pagos
            row = new GridViewRow(0, -1, DataControlRowType.Footer, DataControlRowState.Normal);
            cell = new TableHeaderCell();
            cell.ColumnSpan = 1;
            cell.Font.Size = 11;
            cell.Text = "Tipo Pago";
            cell.VerticalAlign = VerticalAlign.Top;
            cell.HorizontalAlign = HorizontalAlign.Left;
            row.Cells.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 1;
            cell.Font.Size = 11;
            cell.Text = "Monto";
            cell.VerticalAlign = VerticalAlign.Top;
            cell.HorizontalAlign = HorizontalAlign.Left;
            row.Cells.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 10;
            cell.Font.Size = 11;
            cell.Text = "Descripcion";
            cell.VerticalAlign = VerticalAlign.Top;
            cell.HorizontalAlign = HorizontalAlign.Left;
            row.Cells.Add(cell);

            t.Rows.AddAt(inicioFila, row);
            inicioFila++;
            #endregion

            #region Detalle Pagos
            Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
            long nroRendicion = long.Parse(Request.Form["__EVENTARGUMENT"]);
            List<DetallePagoRendicion> pagos = (from p in Contexto.DetallePagoRendicions
                                                where p.CabeceraRendicion == nroRendicion
                                                select p).ToList();

            foreach (DetallePagoRendicion pago in pagos)
            {

                row = new GridViewRow(0, -1, DataControlRowType.Footer, DataControlRowState.Normal);
                cell = new TableHeaderCell();
                cell.ColumnSpan = 1;
                cell.Font.Size = 11;
                cell.Text = pago.objTipoPago.Valor;
                cell.VerticalAlign = VerticalAlign.Top;
                cell.HorizontalAlign = HorizontalAlign.Left;
                //cell.BorderStyle = BorderStyle.None;
                row.Cells.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 1;
                cell.Font.Size = 11;
                cell.Text = string.Format("{0:0.00}", pago.Monto).Replace(",", ".");
                cell.VerticalAlign = VerticalAlign.Top;
                cell.HorizontalAlign = HorizontalAlign.Left;
                //cell.BorderStyle = BorderStyle.None;
                row.Cells.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 10;
                cell.Font.Size = 11;
                cell.Text = GenerarDescripcionDetalle(pago);
                cell.VerticalAlign = VerticalAlign.Top;
                cell.HorizontalAlign = HorizontalAlign.Left;
                //cell.BorderStyle = BorderStyle.None;
                row.Cells.Add(cell);

                t.Rows.AddAt(inicioFila, row);
                inicioFila++;
            }
            #endregion

        }
    }

    private string GenerarDescripcionDetalle(DetallePagoRendicion pago)
    {
        switch (pago.objTipoPago.Contexto)
        {
            case "0":
                return "Pago en Efectivo";
            case "1":
                return "<b>Fecha Deposito: </b>" + pago.D_FechaDeposito.Value.ToShortDateString() + " <b>Nro Transacción: </b>" + pago.D_NroTransaccion;
            case "2":
                return "<b>Fecha Transferencia: </b>" + pago.T_FechaTranferencia.Value.ToShortDateString() + " <b>Nro Control: </b>" + pago.T_NroControl + " <b>Referencia: </b>" + pago.T_Referencia;
            case "3":
                return "<b>Fecha Cobro: </b>" + pago.CH_FechaCobro.Value.ToShortDateString() + " <b>Nro Cheque: </b>" + pago.CH_NroCheque + " <b>Banco: </b>" + pago.objBanco.Valor;
            case "4":
                return "<b>Fecha Pago: </b>" + pago.O_FechaPago.Value.ToShortDateString() + " <b>Nro: </b>" + pago.O_NroOperacion + " <b>Operador: </b>" + pago.objOperador.Valor;
            default:
                return "";
        }

    }
    public void btnExportar_click(object sender, EventArgs e)
    {
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
        long nroRendicion = long.Parse(Request.Form["__EVENTARGUMENT"]);
        GridView gv = new GridView();
        gv.RowCreated += new GridViewRowEventHandler(gv_RowCreated);
        gv.AutoGenerateColumns = false;
        List<DetalleGuia> _dataSourceExcel = (from c in Contexto.DetalleGuias
                                              where c.CabeceraRendicion == nroRendicion
                                              select c).ToList();

        TotalGuias = _dataSourceExcel.Count;

        List<string> camposExcluir = new List<string>() { "objCabeceraGuia", "objEstado", "objTipoPago", "objUsuarioAutorizacion", "objCabeceraRendicion", "IdDetalleGuia", "IdDetalleGuia", "FechaVisita1", "FechaVisita2", "FechaVisita3", "Estado", "MontoCobrado", "TipoPago", "DatosPago", "FechaAutorizacion", "UsuarioAutorizacion", "MontoAutorizacion", "CabeceraGuia", "CabeceraRendicion" };


        Dictionary<string, string> alias = new Dictionary<string, string>() { { "CabeceraRendicion", "Nro Rendición" } };

        if (_dataSourceExcel != null)
        {
            var someObject = _dataSourceExcel[0];
            foreach (System.Reflection.PropertyInfo item in someObject.GetType().GetProperties())
            {
                if (!camposExcluir.Contains(item.Name))
                {
                    string alia = alias.ContainsKey(item.Name) ? alias[item.Name] : item.Name;
                    BoundField boundField = new BoundField();
                    boundField.DataField = item.Name;
                    boundField.HeaderText = alia;
                    boundField.NullDisplayText = "";
                    gv.Columns.Add(boundField);
                }

            }

            gv.DataSource = _dataSourceExcel;
            gv.DataBind();


            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gv.RenderControl(htmlWrite);

            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Rendicion_" + nroRendicion + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".xls");
            HttpContext.Current.Response.ContentType = "application/xls";
            HttpContext.Current.Response.Write(stringWrite.ToString());
            HttpContext.Current.Response.End();


        }


    }

    #region Web Metodos para los Rendicions


    [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> EliminarRendicion(long id)
    {
        Dictionary<string, object> datos = new Dictionary<string, object>();
        Marzzan_InfolegacyDataContext dc = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];

        CabeceraRendicion cab = (from c in dc.CabeceraRendicions
                                 where c.IdCabeceraRendicion == id
                                 select c).FirstOrDefault();


        dc.DetallePagoRendicions.DeleteAllOnSubmit(cab.DetallePagoRendicions);
        foreach (var item in cab.DetalleGuias.ToList())
        {
            item.CabeceraRendicion = null;
        }

        dc.CabeceraRendicions.DeleteOnSubmit(cab);

        dc.SubmitChanges();

        return FiltroRendicion("", "", "", "Todos", 0, 15);

    }

    [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> FiltroRendicion(string NroRendicion, string FechaInicial, string FechaFinal, string Tansporte, int skip, int take)
    {

        Dictionary<string, object> datos = new Dictionary<string, object>();
        long idCliente = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
        List<tempRendicion> Rendicions = new List<tempRendicion>();
        string cadenaSql = "";
        string param = "";
        try
        {
            string cadenaWhere = "";
            List<object> parametros = new List<object>();

            cadenaWhere = " 1=1 " + ArmarFilro(NroRendicion, FechaInicial, FechaFinal, Tansporte, ref parametros);

            cadenaSql = @"select distinct c.* from CabeceraRendicion as c " +
                    @" inner join DetalleGuias d on c.IdCabeceraRendicion  = d.CabeceraRendicion " +
                    @" inner join CabeceraGuias cg on cg.IdCabeceraGuia  = d.CabeceraGuia" +
                    @" where " + cadenaWhere ;

            param = Contexto.Connection.ConnectionString;

            var RendicionsTemp = Contexto.ExecuteQuery<CabeceraRendicion>(cadenaSql, parametros.ToArray()).OrderByDescending(w => w.FechaRendicion).Skip(skip).Take(take).ToList();

            Rendicions = (from d in RendicionsTemp
                          select new tempRendicion
                          {
                              FechaRendicion = d.FechaRendicion.Value,
                              FechaAprobacion = d.FechaAprobacion,
                              IdCabeceraRendicion = d.IdCabeceraRendicion,
                              Transporte = d.objTransportista.Nombre,
                              Monto = d.Monto.Value,
                              Estado = d.objEstado.Valor,
                              TipoRendicion = d.objTipoRendicion.Valor
                          }).Distinct().ToList();

        }
        catch(Exception er)
        {
            datos.Add("Cantidad", 0);
            datos.Add("Rendiciones", new List<tempRendicion>());
            datos.Add("cadenaSql", er.Message);
            return datos;
        }

        datos.Add("Cantidad", Rendicions.Count());
        datos.Add("Rendiciones", Rendicions);
        datos.Add("cadenaSql", cadenaSql +param);

        


        return datos;

    }



    private static string ArmarFilro(string nroRendicion, string fechaInicial, string fechaFinal, string transporte, ref List<object> parametros)
    {
        CabeceraGuia a = new CabeceraGuia();
        DetalleGuia b = new DetalleGuia();

        string cadenaWhere = "";
        int i = 0;

        if (fechaInicial != "")
        {
            cadenaWhere += " and c.FechaRendicion  >= {" + i + "}";
            parametros.Add(Convert.ToDateTime(fechaInicial));
            i++;
        }

        if (fechaFinal != "")
        {
            cadenaWhere += " and c.FechaRendicion  <= {" + i + "}";
            parametros.Add(Convert.ToDateTime(fechaFinal).AddDays(1));
            i++;
        }

        if (nroRendicion != "")
        {
            cadenaWhere += " and c.IdCabeceraRendicion  = {" + i + "}";
            parametros.Add(long.Parse(nroRendicion));
            i++;
        }


        if (transporte != "")
        {
            if (HttpContext.Current.Session["Transporte"] != null && HttpContext.Current.Session["Transporte"].ToString() != "")
                transporte = transporte == "Todos" ? HttpContext.Current.Session["Transporte"].ToString().ToUpper() : transporte;
            else
                transporte = transporte != "Todos" ? transporte.ToUpper() : "";

            cadenaWhere += " and cg.Transporte like {" + i + "}";
            parametros.Add("%" + transporte + "%");
            i++;
        }


        return cadenaWhere;
    }
    #endregion
}
