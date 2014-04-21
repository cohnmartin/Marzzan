using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CommonMarzzan;

public partial class GestionCreditos : System.Web.UI.Page
{
    public static Marzzan_InfolegacyDataContext _dc = new Marzzan_InfolegacyDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {
        //RadGrid1.DataBound += new EventHandler(RadGrid1_DataBound);
        if (!Page.IsPostBack)
        {
            LoadGrupos();
        }
        //ConfCredito c;
        //c.TiempoReposicion
        //c.IdConfCreditos
        //c.objCliente.Nombre;
        //c.MontoAsignado;
        //c.Activo;
        //c.Habilitado;
    }

    private void CargarGrilla(string grupo)
    {
        List<ConfCredito> colConfCredito = null;
        if (grupo.Length == 0)
        {
            colConfCredito = (from C in _dc.ConfCreditos select C).ToList<ConfCredito>();
        }
        else
        {
            colConfCredito = (from C in _dc.ConfCreditos
                              where C.objCliente.Clasif1 == grupo
                              select C).ToList<ConfCredito>();
        }
        this.RadGrid1.DataSource = colConfCredito;
        this.UpdPnlGrilla.Update();
    }

    public void ConfigureExportAndExport()
    {
        foreach (Telerik.Web.UI.GridColumn column in RadGrid1.MasterTableView.Columns)
        {
            if (!column.Visible || !column.Display)
            {
                column.Visible = true;
                column.Display = true;
            }
        }

        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.FileName = "Legajos";
        RadGrid1.MasterTableView.ExportToExcel();
    }

    protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "ExportLegajos")
        {
            ConfigureExportAndExport();
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            long id = long.Parse(RadGrid1.SelectedValue.ToString());
            ConfCredito EliConfCredito = (from C in _dc.ConfCreditos where C.IdConfCreditos == id select C).FirstOrDefault<ConfCredito>();

            _dc.ConfCreditos.DeleteOnSubmit(EliConfCredito);
            _dc.SubmitChanges();
            RadGrid1.Rebind();
        }
        catch
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "eliminacino", "alert('La configuracion de credito no puede ser eliminado.');", true);
        }
    }

    public void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        if (e.Argument == "Update")
        {
            long id = long.Parse(RadGrid1.SelectedValue.ToString());

            ConfCredito LegUpdate = (from C in _dc.ConfCreditos
                                          where C.IdConfCreditos == id
                                          select C).FirstOrDefault<ConfCredito>();

            if (LegUpdate != null)
            {
                /// Controles Tipo TextBox
                LegUpdate.MontoAsignado = Convert.ToDecimal(txtMonto.Value);
                LegUpdate.TiempoReposicion = Convert.ToInt32(txtTiempoReposicion.Value);
                LegUpdate.Activo = chkActivo.Checked;

                /// Controles Tipo Combos
                long idCombo = 0;
                if (cboClientes.SelectedValue != string.Empty)
                {
                    idCombo = long.Parse(cboClientes.SelectedValue);
                    Cliente Clie = (from C in _dc.Clientes where C.IdCliente == idCombo select C).FirstOrDefault<Cliente>();
                    LegUpdate.objCliente = Clie;
                }

                _dc.SubmitChanges();
            }
        }
        else
        {
            ConfCredito LegInsert = new ConfCredito();

            /// Controles Tipo TextBox
            LegInsert.MontoAsignado = Convert.ToDecimal(txtMonto.Value);
            LegInsert.TiempoReposicion = Convert.ToInt32(txtTiempoReposicion.Value);
            LegInsert.Activo = chkActivo.Checked;

            /// Controles Tipo Combos
            long idCombo = 0;
            if (cboClientes.SelectedValue != string.Empty)
            {
                idCombo = long.Parse(cboClientes.SelectedValue);
                Cliente Clie = (from C in _dc.Clientes where C.IdCliente == idCombo select C).FirstOrDefault<Cliente>();
                LegInsert.objCliente = Clie;
            }

            _dc.ConfCreditos.InsertOnSubmit(LegInsert);
            _dc.SubmitChanges();
        }

        RadGrid1.Rebind();
        UpdPnlGrilla.Update();
    }

    protected void cboClientes_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        LoadClientes(e.Text);
    }

    private void LoadClientes(string grupo)
    {
        List<Cliente> colConfCredito = (from C in _dc.Clientes where C.Clasif1 == grupo select C).ToList<Cliente>();
        this.cboClientes.DataTextField = "Nombre";
        this.cboClientes.DataValueField = "IdCliente";
        this.cboClientes.DataSource = colConfCredito.OrderBy(w => w.Nombre);
        this.cboClientes.DataBind();
    }

    private void LoadGrupos()
    {
        var colGrupos = (from C in _dc.Clientes
                         orderby C.Clasif1
                                  where C.Clasif1 != null && !C.Clasif1.Contains("Inactivo")
                                  select new { Desc = C.Clasif1 }).Distinct();

        //select distinct(clasif1) from clientes 
        //where clasif1 is not null and clasif1 not like '%Inactivo%'

        ////ABM
        this.cboGrupo.DataTextField = "Desc";
        this.cboGrupo.DataValueField = "Desc";
        this.cboGrupo.DataSource = colGrupos.OrderBy(w => w.Desc);
        this.cboGrupo.DataBind();

        this.cboGrupo.Items.Insert(0, new RadComboBoxItem("- Seleccione una Grupo -"));

        ////Filtro
        this.cboGrupoFiltro.DataTextField = "Desc";
        this.cboGrupoFiltro.DataValueField = "Desc";
        this.cboGrupoFiltro.DataSource = colGrupos.OrderBy(w => w.Desc);
        this.cboGrupoFiltro.DataBind();

        this.cboGrupoFiltro.Items.Insert(0, new RadComboBoxItem("- Todos los Grupos -"));
    }

    protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        CargarGrilla(this.cboGrupoFiltro.SelectedValue);
    }
    
    protected void cboGrupoFiltro_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        this.RadGrid1.Rebind();
    }
}
