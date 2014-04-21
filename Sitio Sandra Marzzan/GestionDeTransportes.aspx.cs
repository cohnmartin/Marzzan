using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using CommonMarzzan;


public partial class GestionDeTransportes : System.Web.UI.Page
{
    public static List<Producto> _productos = null;
    public static List<Composicion> componentes = new List<Composicion>();
    public static Marzzan_InfolegacyDataContext _dc = new Marzzan_InfolegacyDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitCombos();

            //grillaTransportes.DataSource = from T in _dc.ConfTransportes
            //                               select T;
            //grillaTransportes.DataBind();

            grillaTransportes.DataSource = new List<ConfTransporte>();
            grillaTransportes.DataBind();
            upGrilla.Update();

        }
    }

    private void InitCombos()
    {

        cboProvincias.DataTextField = "Provincia";
        cboProvincias.DataValueField = "Provincia";
        cboProvincias.DataSource = (from P in _dc.Direcciones
                                    where P.Provincia.Trim() != ""
                                    orderby P.Provincia
                                    select new { Provincia = P.Provincia }).Distinct().OrderBy(w=> w.Provincia).ToList();
        cboProvincias.DataBind();
        cboProvincias.Items.Insert(0, new RadComboBoxItem("- Seleccione una Provincia -"));

    }

    protected void cboProvincias_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        InitCombos();
    }

    protected void cboLocalidades_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        if (e.Text != "")
        {
            cboLocalidades.DataTextField = "Localidad";
            cboLocalidades.DataValueField = "Localidad";
            cboLocalidades.DataSource = (from P in _dc.Direcciones
                                         where P.Localidad.Trim() != "" && P.Provincia == e.Text
                                         && P.objCliente.Habilitado.Value
                                         orderby P.Localidad
                                         select new { Localidad = P.Localidad.ToUpper() }).Distinct().OrderBy(w => w.Localidad).ToList();
            cboLocalidades.DataBind();
            cboLocalidades.Items.Insert(0, new RadComboBoxItem("- Seleccione una Localidad -"));

        }
    }

    protected void cboFormaPago_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        if (e.Text != "")
        {

            cboFormaPago.DataTextField = "Descripcion";
            cboFormaPago.DataValueField = "IdFormaPago";
            cboFormaPago.DataSource = (from F in _dc.FormaDePagos
                                       select F).OrderBy(w => w.Descripcion).ToList<FormaDePago>();
            cboFormaPago.DataBind();
            cboFormaPago.Items.Insert(0, new RadComboBoxItem("- Indique Forma de Pago -"));
        }
    }

    protected void cboTransporte_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        if (e.Text != "")
        {

            cboTransporte.DataTextField = "IdConfTransportes";
            cboTransporte.DataValueField = "Transporte";

            var transportes = (from T in _dc.Clientes
                               where T.Transporte.Trim() != ""
                               select new { IdConfTransportes = T.Transporte.ToUpper(), Transporte = T.Transporte.ToUpper() }).Distinct().ToList();

            cboTransporte.DataSource = from T in transportes
                                       orderby T.Transporte
                                       select T;
            cboTransporte.DataBind();
            cboTransporte.Items.Insert(0, new RadComboBoxItem("Seleccione un Trans"));
        }
    }

    protected void cboConceptos_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        string[] datos = e.Text.Split('|');

        cboConceptos.DataTextField = "Descripcion";
        cboConceptos.DataValueField = "IdProducto";

        var conceptos = (from P in _dc.Productos
                         where P.Tipo == 'G' && P.ColPresentaciones.Any(p => (p.Activo.HasValue && p.Activo.Value) || ! p.Activo.HasValue)
                         select P).Distinct().ToList<Producto>();

        cboConceptos.DataSource = from C in conceptos
                                  orderby C.Descripcion
                                  select C;
        cboConceptos.DataBind();
    }

    protected void btnAgregarDefinicion_Click(object sender, EventArgs e)
    {

        int ExisteDefinicion = (from C in _dc.ConfTransportes
                                where C.Provincia == cboProvincias.SelectedItem.Text &&
                                C.Localidad == cboLocalidades.Text &&
                                C.FormaDePago == cboFormaPago.Text &&
                                C.Transporte == cboTransporte.Text &&
                                C.IdProducto == long.Parse(cboConceptos.SelectedValue)
                                select C).Count();

        if (ExisteDefinicion == 0)
        {
            ConfTransporte conf = new ConfTransporte();
            conf.Provincia = cboProvincias.Text;
            conf.Localidad = cboLocalidades.Text;
            conf.FormaDePago = cboFormaPago.Text;
            conf.Transporte = cboTransporte.Text;
            conf.IdProducto = long.Parse(cboConceptos.SelectedValue);
            _dc.ConfTransportes.InsertOnSubmit(conf);
            _dc.SubmitChanges();
        }

        grillaTransportes.DataSource = from T in _dc.ConfTransportes
                                       select T;
        grillaTransportes.DataBind();
        upGrilla.Update();




    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("Inicio.aspx");

    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        long idConf = long.Parse(((sender as ImageButton).NamingContainer as GridDataItem).GetDataKeyValue("IdConfTransportes").ToString());

        if (idConf > 0)
        {
            try
            {
                ConfTransporte conf = (from C in _dc.ConfTransportes
                                       where C.IdConfTransportes == idConf
                                       select C).First<ConfTransporte>();

                _dc.ConfTransportes.DeleteOnSubmit(conf);
                _dc.SubmitChanges();
            }
            catch(Exception err)
            {
                ScriptManager.RegisterStartupScript(upAgregarDefinicion, typeof(UpdatePanel), "error", "alert('" + err.Message + "');",true);
            
            }
        }

        grillaTransportes.DataSource = from T in _dc.ConfTransportes
                                       where T.Provincia.ToUpper() == cboProvincias.Text.ToUpper()
                                       select T;
        grillaTransportes.DataBind();
        upGrilla.Update();


    }

    public void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        grillaTransportes.DataSource = from T in _dc.ConfTransportes
                                       orderby T.Localidad , T.Transporte
                                       where T.Provincia.ToUpper() == cboProvincias.Text.ToUpper()
                                       select T;
        grillaTransportes.DataBind();
        upGrilla.Update();
    }
}
