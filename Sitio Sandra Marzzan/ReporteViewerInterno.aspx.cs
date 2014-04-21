using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Reporting;
using ReportesMarzzan;
using System.IO;
using Telerik.Web.UI;
using CommonMarzzan;

public partial class ReporteViewerInterno : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ReporteCuerpoPedidoInterno rep = new ReporteCuerpoPedidoInterno();
            rep.InitReport((List<long>)Session["IdCabecerasConsultados"], Session["NombreUsuario"].ToString());
            this.ReportViewer1.Report = rep;

            Button btnExportCustom = new Button();
            btnExportCustom.Text = "Exportar";
            btnExportCustom.OnClientClick = "ExportarRTF();";
            ReportViewer1.FindControl("ReportToolbar").FindControl("NavGr").Controls.Add(btnExportCustom);
        }
        catch (Exception exc)
        {
            throw exc.InnerException;
        }

    }

    public void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        if (e.Argument == "Imprimir")
        {
            Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
            List<long> ids = (List<long>)Session["IdCabecerasConsultados"];
            List<CabeceraPedido> cabeceras = (from C in dc.CabeceraPedidos
                                              where ids.Contains(C.IdCabeceraPedido)
                                              select C).ToList<CabeceraPedido>();


            foreach (CabeceraPedido item in cabeceras)
            {
                item.NroImpresion++;
            }

            dc.SubmitChanges();
        }
    }
}
