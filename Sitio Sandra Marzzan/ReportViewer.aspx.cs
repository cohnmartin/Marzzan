using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Reporting;
using ReportesMarzzan;

public partial class ReportViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ReporteCuerpoPedido rep = new ReporteCuerpoPedido();
        rep.InitReport(long.Parse(Request.QueryString["Id"].ToString()));
        this.ReportViewer1.Report = rep;
        (ReportViewer1.FindControl("ReportToolbar").FindControl("ExportGr").Controls[0].Controls[0] as DropDownList).SelectedIndex = 1;
        
    }
}
