using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonMarzzan;
using Telerik.Web.UI;


public partial class ConsultaSaldosCtaCte : BasePage
{
    protected override void PageLoad()
    {
        if (!IsPostBack)
        {
            Session.Add("Clientes", null);
            Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

            var cliente = (from C in dc.Clientes
                           where C.IdCliente == long.Parse(Session["IdUsuario"].ToString())
                           orderby C.Nombre
                           select C).Single<Cliente>();


            List<Cliente> consultores = Helper.ObtenerConsultoresSubordinados((Cliente)cliente);
            Session["Clientes"] = consultores;

            if (consultores.Count > 0)
            {
                cboConsultores.AppendDataBoundItems = true;
                cboConsultores.Items.Add(new RadComboBoxItem("Todos los Revendedores", "-1"));
                cboConsultores.DataTextField = "Nombre";
                cboConsultores.DataValueField = "IdCliente";
                cboConsultores.DataSource = consultores;
                cboConsultores.DataBind();
            }
            else
            {
                cboConsultores.AppendDataBoundItems = true;
                cboConsultores.DataTextField = "Nombre";
                cboConsultores.DataValueField = "IdCliente";
                cboConsultores.Items.Add(new RadComboBoxItem(cliente.Nombre, cliente.IdCliente.ToString()));
                cboConsultores.DataSource = consultores;
                cboConsultores.DataBind();
                cboConsultores.SelectedIndex = 0;
                cboConsultores.Enabled = false;
            }

            GrillaResultados.DataSource = new List<Cliente>();
            GrillaResultados.DataBind();
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {

        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
        List<Cliente> clientes = null;

        if (cboConsultores.SelectedValue == "")
        {
            return;
        }
        else if (cboConsultores.SelectedValue != "-1")
        {
            clientes = (from C in dc.Clientes
                         where C.IdCliente == long.Parse(cboConsultores.SelectedValue)
                        orderby C.Nombre
                        select C).ToList<Cliente>();
        }
        else
        {
            if (chkSaldosCero.Checked)
                clientes = (List<Cliente>)Session["Clientes"];
            else
                clientes = (from C in Session["Clientes"] as List<Cliente>
                           where C.SaldoCtaCte.Value > 0
                           select C).ToList<Cliente>();
        }

        GrillaResultados.DataSource = clientes;
        GrillaResultados.DataBind();
        upResultado.Update();
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
            GrillaResultados.ExportSettings.FileName = "SaldosCuentaCorriente" + "_" + string.Format("{0:ddMMyyyy}", DateTime.Now);
            GrillaResultados.MasterTableView.ExportToExcel();

        }
    }
    

}
