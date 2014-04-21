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
public partial class ConsultaSaldosVencidos : BasePage
{

    private List<string> GruposValidos
    {

        set
        {
            Session["GruposValidos"] = value;
        }
        get
        {
            return (List<string>)Session["GruposValidos"];
        }

    }

    protected override void PageLoad()
    {

        if (!IsPostBack)
        {
            Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
            txtFechaInicial.SelectedDate = DateTime.Now.AddDays(-7);
            txtFechaFinal.SelectedDate = DateTime.Now;

            GrillaResultados.DataSource = new List<CabeceraPedido>();
            GrillaResultados.DataBind();

            GruposValidos = Helper.ObtenerGruposSubordinados(Session["GrupoCliente"].ToString());
            this.cboGrupoFiltro.DataSource = GruposValidos;
            this.cboGrupoFiltro.DataBind();
            this.cboGrupoFiltro.Items.Insert(0, new RadComboBoxItem("- Todos los Grupos -"));

        }

    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        /// Consulta a BEJERMAN
        Marzzan_BejermanDataContext dc = new Marzzan_BejermanDataContext();
        List<VeoCtaCte> datos = new List<VeoCtaCte>();

        if (txtCliente.Text != "")
        {
            datos = (from c in dc.VeoCtaCtes
                     where c.cve_FContab >= txtFechaInicial.SelectedDate.Value.Date
                     && c.cve_FContab <= txtFechaFinal.SelectedDate.Value.Date
                     && c.cvecli_RazSoc == txtCliente.Text
                     && c.cve_SaldoMonCC > 10
                     select c).OrderBy(w => w.cve_FEmision).ThenBy(w => w.cvecli_RazSoc).ToList();
        }
        else
        {
            if (cboGrupoFiltro.SelectedValue != "")
            {
                datos = (from c in dc.VeoCtaCtes
                         where c.cve_FContab >= txtFechaInicial.SelectedDate.Value.Date
                         && c.cve_FContab <= txtFechaFinal.SelectedDate.Value.Date
                         && c.dc1_Desc == cboGrupoFiltro.Text
                         && c.cve_SaldoMonCC > 10
                         select c).OrderBy(w => w.cve_FEmision).ThenBy(w => w.cvecli_RazSoc).ToList();
            }
            else
            {
                datos = (from c in dc.VeoCtaCtes
                         where c.cve_FContab >= txtFechaInicial.SelectedDate.Value.Date
                         && c.cve_FContab <= txtFechaFinal.SelectedDate.Value.Date
                         && GruposValidos.Contains(c.dc1_Desc)
                         && c.cve_SaldoMonCC > 10
                         select c).OrderBy(w => w.cve_FEmision).ThenBy(w => w.cvecli_RazSoc).ToList();
            }
        
        }



        GrillaResultados.DataSource = datos;
        GrillaResultados.DataBind();
        upResultado.Update();
    }
    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("Inicio.aspx");
    }
    protected void GrillaResultados_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "ExportCtaCte")
        {
            foreach (Telerik.Web.UI.GridColumn column in GrillaResultados.MasterTableView.Columns)
            {
                if (!column.Visible || !column.Display)
                {
                    column.Visible = true;
                    column.Display = true;
                }
            }

            GrillaResultados.ExportSettings.ExportOnlyData = true;
            GrillaResultados.ExportSettings.IgnorePaging = true;
            GrillaResultados.ExportSettings.FileName = "SaldosVencidos" + "_" + string.Format("{0:ddMM}", txtFechaInicial.SelectedDate) + "_" + string.Format("{0:ddMM}", txtFechaFinal.SelectedDate);
            GrillaResultados.MasterTableView.ExportToExcel();

        }
    }



}
