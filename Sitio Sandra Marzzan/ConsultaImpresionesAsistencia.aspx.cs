using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CommonMarzzan;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Telerik.Reporting;
using Telerik.Reporting.Processing;
using ReportesMarzzan;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.Script.Services;
public partial class ConsultaImpresionesAsistencia : BasePage
{
    protected override void PageLoad()
    {
        if (!IsPostBack)
        {

            using (Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext())
            {
                cboTransporte.DataTextField = "Transporte";
                cboTransporte.DataValueField = "Transporte";
                cboTransporte.DataSource = (from T in dc.View_Transportes
                                            orderby T.Transporte
                                            select new { IdConfTransportes = T.Transporte, Transporte = T.Transporte }).ToList();

                cboTransporte.DataBind();
                cboTransporte.Items.Insert(0, new RadComboBoxItem("Seleccione un Transportista"));

            }
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {


        List<View_PedidosParaImpresion> datosExportar = (List<View_PedidosParaImpresion>)Session["DatosConsultado"];


        List<string> camposExcluir = new List<string>() { "IdCabeceraPedido", "Cliente" };
        Dictionary<string, string> alias = new Dictionary<string, string>();

             

        List<string> DatosReporte = new List<string>();
        DatosReporte.Add("Planilla de Impresiones Asistencia");
        DatosReporte.Add("Fecha y Hora emisi&oacute;n:" + DateTime.Now.ToString());
        DatosReporte.Add("Per&iacute;odo: " + hdf_Periodo.Value);
        DatosReporte.Add("Muestra todas los pedidos impresos en el per&iacute;odo seleccionado");


        GridView gv = HelperWeb.GenerarExportExcel(datosExportar, alias, camposExcluir, DatosReporte);

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        gv.RenderControl(htmlWrite);

        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=ImpresionesAsistencia" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".xls");
        HttpContext.Current.Response.ContentType = "application/xls";
        HttpContext.Current.Response.Write(stringWrite.ToString());
        HttpContext.Current.Response.End();
    }

    [WebMethod(EnableSession = true)]
    public static object ConsultarPedidos(string grupo, string transporte, string FechaInicial, string FechaFinal)
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        DateTime FI = Convert.ToDateTime(FechaInicial);
        DateTime FF = Convert.ToDateTime(FechaFinal);

        using (Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext())
        {

            List<CabeceraPedido> cabeceras = new List<CabeceraPedido>();
            Cliente clienteCtaBolsos = new Cliente();
            List<Cliente> consultores = new List<Cliente>();
            List<long> IdsConsultores = new List<long>();
            List<View_PedidosParaImpresion> Resultado = new List<View_PedidosParaImpresion>();

            if (grupo != null)
            {
                IdsConsultores = (from C in dc.Clientes
                                  where C.Habilitado.Value &&
                                  (C.Clasif1 == grupo)
                                  orderby C.Nombre
                                  select C.IdCliente).ToList<long>();

                Resultado = (from v in dc.View_PedidosParaImpresions
                             where v.FechaImpresion != null && (v.FechaImpresion.Value.Date >= FI && v.FechaImpresion.Value.Date <= FF)
                             && IdsConsultores.Contains(v.Cliente) && v.Transporte == transporte.ToUpper()
                             select v).ToList();


            }
            else
            {

                Resultado = (from v in dc.View_PedidosParaImpresions
                             where v.FechaImpresion != null && (v.FechaImpresion.Value.Date >= FI && v.FechaImpresion.Value.Date <= FF)
                             && v.Transporte == transporte.ToUpper()
                             select v).ToList();
            }





            result.Add("Datos", Resultado);
            HttpContext.Current.Session.Add("DatosConsultado", Resultado.ToList());


        }

        return result;
    }
}