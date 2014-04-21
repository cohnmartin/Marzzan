using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonMarzzan;
using Telerik.Web.UI;


public partial class ConsultaDeComisiones : BasePage
{
   
    private Cliente CurrentClient
    {
        get {
            return (Cliente)Session["CurrentClient"];
        }
        set {

            Session["CurrentClient"] = value;
        }
    
    }


    private List<Comisiones> ComisionesConsultadas
    {
        get
        {
            return (List<Comisiones>)Session["ComisionesConsultadas"];
        }
        set
        {

            Session["ComisionesConsultadas"] = value;
        }

    }


    protected override void PageLoad()
    {
        if (!IsPostBack)
        {
            Session.Add("Clientes", null);
            Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

            CurrentClient = (from C in dc.Clientes
                           where C.IdCliente == long.Parse(Session["IdUsuario"].ToString())
                           orderby C.Nombre
                           select C).Single<Cliente>();



            List<Cliente> consultores = Helper.ObtenerConsultoresSubordinados((Cliente)CurrentClient);
            Session["Clientes"] = consultores;

            if (consultores.Count > 0)
            {
                cboConsultores.AppendDataBoundItems = true;
                cboConsultores.Items.Add(new RadComboBoxItem("Todos los Consultores", "-1"));
                cboConsultores.DataTextField = "Nombre";
                cboConsultores.DataValueField = "CodigoExterno";
                cboConsultores.DataSource = consultores;
                cboConsultores.DataBind();
                cboConsultores.Items[0].Selected = true;
            }
            else
            {
                cboConsultores.AppendDataBoundItems = true;
                cboConsultores.DataTextField = "Nombre";
                cboConsultores.DataValueField = "CodigoExterno";
                cboConsultores.Items.Add(new RadComboBoxItem(CurrentClient.Nombre, CurrentClient.Clasif1.ToString()));
                cboConsultores.DataSource = consultores;
                cboConsultores.DataBind();
                cboConsultores.SelectedIndex = 0;
                cboConsultores.Enabled = false;
            }

            GrillaResultados.DataSource = new List<Cliente>();
            GrillaResultados.DataBind();

            txtFechaFinal.SelectedDate = DateTime.Now;
            txtFechaInicial.SelectedDate = DateTime.Now.AddMonths(-2);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {

        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        GridGroupByExpression expression = new GridGroupByExpression();
        GridGroupByField gridGroupByField = new GridGroupByField();
        GrillaResultados.MasterTableView.GroupByExpressions.Clear();

        if (cboConsultores.SelectedValue != "-1")
        {
            double idConsultor = double.Parse(cboConsultores.SelectedValue);
            ComisionesConsultadas = (from C in dc.Comisiones
                          where C.Cod_Cliente == idConsultor
                          && C.Fecha >= txtFechaInicial.SelectedDate.Value && C.Fecha <= txtFechaFinal.SelectedDate.Value
                          select C).ToList<Comisiones>();

            GrillaResultados.Columns.FindByDataField("Lider").Display = false;
            GrillaResultados.Columns.FindByDataField("Fecha").Display = true;


            // SelectFields values (appear in header)
            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "Lider";
            gridGroupByField.FieldAlias = "Consultor";
            gridGroupByField.SortOrder = GridSortOrder.Ascending;
            expression.SelectFields.Add(gridGroupByField);

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "Cod_Cliente";
            expression.SelectFields.Add(gridGroupByField);


            //GroupByFields values (group data)
            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "Lider";
            expression.GroupByFields.Add(gridGroupByField);
            GrillaResultados.MasterTableView.GroupByExpressions.Add(expression);
        }
        else
        {
            DateTime FInicial= txtFechaInicial.SelectedDate.Value;
            DateTime FFinal = txtFechaFinal.SelectedDate.Value;
            List<string> grupos = Helper.ObtenerGruposSubordinados((Cliente)CurrentClient);
            ComisionesConsultadas = (from C in dc.Comisiones
                          where 
                          grupos.Contains(C.Grupo) &&
                          (C.Fecha.Value >= FInicial && C.Fecha.Value <= FFinal )
                        select C).ToList<Comisiones>();

            GrillaResultados.Columns.FindByDataField("Lider").Display = true;
            GrillaResultados.Columns.FindByDataField("Fecha").Display = false;


            // SelectFields values (appear in header)
            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "Fecha";
            gridGroupByField.FieldAlias = "Período";
            gridGroupByField.SortOrder = GridSortOrder.Ascending;
            gridGroupByField.FormatString = "{0:MM/yyyy}";
            expression.SelectFields.Add(gridGroupByField);


            //GroupByFields values (group data)
            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "Fecha";
            expression.GroupByFields.Add(gridGroupByField);
            GrillaResultados.MasterTableView.GroupByExpressions.Add(expression);

        }



        GrillaResultados.DataSource = ComisionesConsultadas;
        GrillaResultados.DataBind();
        upResultado.Update();
    }

    void CharComisiones_ItemDataBound(object sender, Telerik.Charting.ChartItemDataBoundEventArgs e)
    {
        e.ChartSeries.Name = "Comisión";
        e.ChartSeries.Appearance.TextAppearance.Dimensions.Paddings.Left = new Telerik.Charting.Styles.Unit(5);
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

    protected void GrillaResultados_ItemDataBound(object source, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            (e.Item.FindControl("lblNombre") as Label).Text = (e.Item.DataItem as Comisiones).Lider.ToLower();
        }
        CreateHeaderControls(e);
    }
 

    private void CreateHeaderControls(GridItemEventArgs e)
    {

        if (e.Item is GridGroupHeaderItem)
        {
            if (cboConsultores.SelectedValue == "-1")
            {

                GridGroupHeaderItem item = (GridGroupHeaderItem)e.Item;
                System.Data.DataRowView groupDataRow = (System.Data.DataRowView)e.Item.DataItem;
                Label lblTitulo = new Label();
                lblTitulo.ID = "lbltitutlogrupo";


                if (groupDataRow != null)
                {
                    lblTitulo.Text = string.Format("     {0:MM/yyyy}", groupDataRow["Período"]);
                }

                ImageButton img = new ImageButton();
                img.ID = "btnGraficar";
                img.ImageUrl = "~/Imagenes/chart_bar.png";
                img.ToolTip = "Ver Gráfico del Período";
                img.Style.Add(HtmlTextWriterStyle.PaddingTop, "5px");
                img.OnClientClick = "ShowChart('" + groupDataRow["Período"] + "'); return false;";
                item.DataCell.Controls.Add(img);
                item.DataCell.Controls.Add(lblTitulo);
            }
            else
            {

                GridGroupHeaderItem item = (GridGroupHeaderItem)e.Item;
                System.Data.DataRowView groupDataRow = (System.Data.DataRowView)e.Item.DataItem;
                Label lblTitulo = new Label();
                lblTitulo.ID = "lbltitutlogrupo";


                if (groupDataRow != null)
                {
                    lblTitulo.Text = string.Format("    Consultor: {0}", groupDataRow["Consultor"]);
                }

                ImageButton img = new ImageButton();
                img.ID = "btnGraficar";
                img.ImageUrl = "~/Imagenes/chart_bar.png";
                img.ToolTip = "Ver Gráfico del Período";
                img.Style.Add(HtmlTextWriterStyle.PaddingTop, "5px");
                img.OnClientClick = "ShowChartByConsultor('" + groupDataRow["Cod_Cliente"] + "'); return false;";
                item.DataCell.Controls.Add(img);
                item.DataCell.Controls.Add(lblTitulo);
            }
        }
    }


    public void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        if (e.Argument.Contains("ByFecha"))
        {
            DateTime FechaSeleccionada = Convert.ToDateTime(e.Argument.Split('|')[1].Substring(0, 10));
            CharComisiones.ItemDataBound += new EventHandler<Telerik.Charting.ChartItemDataBoundEventArgs>(CharComisiones_ItemDataBound);
            CharComisiones.ChartTitle.TextBlock.Text = string.Format("Comisiones Generales Mes de {0:MMMM}", FechaSeleccionada);
            CharComisiones.DataSource = ComisionesConsultadas.Where(w => w.Fecha.Value.Month == FechaSeleccionada.Month && w.Fecha.Value.Year == FechaSeleccionada.Year).Select(w => w.TOTAL).ToList();
            CharComisiones.DataBind();
            upGrafico.Update();

        }
        else {

            double CodigoExterno = double.Parse(e.Argument.Split('|')[1].ToString());
            CharComisiones.ItemDataBound += new EventHandler<Telerik.Charting.ChartItemDataBoundEventArgs>(CharComisiones_ItemDataBound);
            CharComisiones.ChartTitle.TextBlock.Text = "Comisiones Generales";
            CharComisiones.DataSource = ComisionesConsultadas.Where(w => w.Cod_Cliente == CodigoExterno).Select(w => w.TOTAL).ToList();
            CharComisiones.DataBind();
            upGrafico.Update();        
        
        }
    
    }
}
