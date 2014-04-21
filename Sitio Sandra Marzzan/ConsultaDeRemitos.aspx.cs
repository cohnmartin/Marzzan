using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonMarzzan;
using Telerik.Web.UI;


public partial class ConsultaDeRemitos : BasePage
{
    public class TempRemitos
    {
        public DateTime Fecha { get; set; }
        public string NroRemito { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public string Clasif1 { get; set; }

    }
    protected override void PageLoad()
    {
        if (!IsPostBack)
        {
            Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
            string GrupoCliente = Session["GrupoCliente"].ToString();
            List<string> Grupos = Helper.ObtenerGrupos(GrupoCliente);



            if (Grupos.Count > 1)
            {
                cboConsultores.AppendDataBoundItems = true;
                cboConsultores.Items.Add(new RadComboBoxItem("Todos los Grupos", "-1"));
                //cboConsultores.DataTextField = "Nombre";
                //cboConsultores.DataValueField = "IdCliente";
                cboConsultores.DataSource = Grupos;
                cboConsultores.DataBind();
                cboConsultores.SelectedIndex = 0;
            }
            else
            {
                cboConsultores.AppendDataBoundItems = true;
                //cboConsultores.DataTextField = "Nombre";
                //cboConsultores.DataValueField = "IdCliente";
                cboConsultores.Items.Add(new RadComboBoxItem(GrupoCliente, GrupoCliente));
                cboConsultores.DataSource = Grupos;
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
        List<string> gruposSeleccionado = new List<string>();

        if (cboConsultores.SelectedValue != "-1")
        {
            gruposSeleccionado.Add(cboConsultores.SelectedValue);
        }
        else
        {
            gruposSeleccionado.AddRange(cboConsultores.Items.Select(w => w.Value).ToList());
        }

        var clientesGrupo = (from c in dc.Clientes
                             where gruposSeleccionado.Contains(c.Clasif1)
                             select new
                             {
                                 c.Nombre,
                                 c.CodigoExterno,
                                 c.Clasif1
                             }).ToList();

        List<string> CodigosClientes = clientesGrupo.Select(w => w.CodigoExterno).ToList();

        var remitos = (from r in dc.RemitosPendientes
                       where CodigosClientes.Contains(r.CodCliente)
                       select new TempRemitos
                       {
                           Fecha = r.FechaRemito,
                           NroRemito = r.NroRemito,
                           Codigo = r.CodCliente,
                           Descripcion = r.DescArticulo,
                       }).ToList();

        foreach (var item in remitos)
        {
            item.Nombre = clientesGrupo.Where(w => w.CodigoExterno == item.Codigo).FirstOrDefault().Nombre;
            item.Clasif1 = clientesGrupo.Where(w => w.CodigoExterno == item.Codigo).FirstOrDefault().Clasif1;
        }

        GrillaResultados.DataSource = remitos.OrderBy(w=>w.Clasif1).ThenBy(w=>w.Nombre);
        GrillaResultados.DataBind();
        upResultado.Update();

        //List<Cliente> clientes = null;

        //if (cboConsultores.SelectedValue == "")
        //{
        //    return;
        //}
        //else if (cboConsultores.SelectedValue != "-1")
        //{
        //    clientes = (from C in dc.Clientes
        //                where C.IdCliente == long.Parse(cboConsultores.SelectedValue)
        //                orderby C.Nombre
        //                select C).ToList<Cliente>();
        //}
        //else
        //{
        //    if (chkSaldosCero.Checked)
        //        clientes = (List<Cliente>)Session["Clientes"];
        //    else
        //        clientes = (from C in Session["Clientes"] as List<Cliente>
        //                    where C.SaldoCtaCte.Value > 0
        //                    select C).ToList<Cliente>();
        //}

        //GrillaResultados.DataSource = clientes;
        //GrillaResultados.DataBind();
        //upResultado.Update();
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
            GrillaResultados.ExportSettings.FileName = "RemitosPendientes" + "_" + string.Format("{0:ddMMyyyy}", DateTime.Now);
            GrillaResultados.MasterTableView.ExportToExcel();

        }
    }

    
}
