using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using CommonMarzzan;

public partial class GestionTransportistasPromos : System.Web.UI.Page
{
    public static Marzzan_InfolegacyDataContext _dc = new Marzzan_InfolegacyDataContext();

    protected override void  OnPreRenderComplete(EventArgs e)
    {
        base.OnPreRenderComplete(e);
        ConfigurarComboTransportistas();

    } 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            CargarGrilla();
           
        }
    }
    public void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

        if (e.Argument != "undefined")
        {
            if (e.Argument == "Agregar")
            {
                RadComboBox cboTransporte = (RadComboBox)gvTranportistas.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("cboTransporte");

                if (cboTransporte.SelectedValue != "")
                {

                    ConfTransPromo conf = new ConfTransPromo();
                    conf.Transporte = cboTransporte.SelectedValue;
                    conf.ConfPromo = long.Parse(Request.QueryString["IdConfPromocion"].ToString());

                    _dc.ConfTransPromos.InsertOnSubmit(conf);
                    _dc.SubmitChanges();

                    CargarGrilla();
                    ConfigurarComboTransportistas();
                    upGrilla.Update();
                }
                else
                    RadAjaxManager1.ResponseScripts.Add("AlertaTransportista()");
            }
        }
    }
    private void CargarGrilla()
    {
        List<ConfTransPromo> ListaConfTrans = (from C in _dc.ConfTransPromos
                                               where C.objConfPromocion.IdConfPromocion == long.Parse(Request.QueryString["IdConfPromocion"].ToString())
                                               select C).ToList<ConfTransPromo>();

        gvTranportistas.DataSource = ListaConfTrans;
        gvTranportistas.DataBind();
    }
    private void ConfigurarComboTransportistas()
    {
        RadComboBox cboTransporte = (RadComboBox)gvTranportistas.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("cboTransporte");


        string[] TransAsignados = (from C in _dc.ConfTransPromos
                                   where C.objConfPromocion.IdConfPromocion == long.Parse(Request.QueryString["IdConfPromocion"].ToString())
                                   select C.Transporte).ToArray<string>();


        cboTransporte.DataTextField = "Transporte";
        cboTransporte.DataValueField = "Transporte";
        cboTransporte.DataSource = (from T in _dc.ConfTransportes
                                    where T.Transporte.Trim() != ""
                                    && !TransAsignados.Contains(T.Transporte)
                                    orderby T.Transporte
                                    select new { IdConfTransportes = T.Transporte, Transporte = T.Transporte }).Distinct().ToList();

        cboTransporte.DataBind();
        cboTransporte.Items.Insert(0, new RadComboBoxItem("Seleccione un Transportista"));
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        long idConf = long.Parse(((sender as ImageButton).NamingContainer as GridDataItem).GetDataKeyValue("IdTransporteHabilitado").ToString());

        ConfTransPromo TransAsignado = (from C in _dc.ConfTransPromos
                                   where C.IdTransporteHabilitado == idConf
                                   select C).Single();

        _dc.ConfTransPromos.DeleteOnSubmit(TransAsignado);
        _dc.SubmitChanges();
        CargarGrilla();
        ConfigurarComboTransportistas();
        upGrilla.Update();

    }
}
