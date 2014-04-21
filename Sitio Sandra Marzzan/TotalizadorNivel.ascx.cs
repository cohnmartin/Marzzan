using System;
using System.Linq;
using System.Data.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CommonMarzzan;

public partial class TotalizadorNivel : System.Web.UI.UserControl
{
    public List<RemitosPendiente> RemitosPendientesNoAfectados
    {

        set
        {
            Session["RemitosPendientesNoAfectados"] = value;
        }

        get
        {
            if (Session["RemitosPendientesNoAfectados"] == null)
                return new List<RemitosPendiente>();
            else
                return (List<RemitosPendiente>)Session["RemitosPendientesNoAfectados"];
        }
    }

    public bool PoseeRequerimientoPagoFacil
    {
        get;
        set;
    }

    protected void Page_Load(object sender, EventArgs e)
    {


    }

    public void IniciarControl()
    {
        Cliente consultor = (Cliente)Session["Consultor"];
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        List<string> remAfectados = (from R in dc.RemitosAfectados
                            where R.CodCliente.Trim() == consultor.CodigoExterno.Trim()
                            && R.CodArticulo != "2550000000018"
                                     select R.NroRemito + R.CodArticulo).ToList();

        var remPendientes = (from R in dc.RemitosPendientes
                             where R.CodCliente.Trim() == consultor.CodigoExterno.Trim()
                             select R).ToList();


        RemitosPendientesNoAfectados = (from R in remPendientes
                                        where !remAfectados.Contains(R.NroRemito + R.CodArticulo)
                                        select R).Distinct().ToList();


        grillaRemitosPendientes.DataSource = RemitosPendientesNoAfectados;
        grillaRemitosPendientes.DataBind();
        upRemitosPendientes.Update();

        if (remPendientes.Any(w => w.CodArticulo.Trim() == "2550000000018" || w.DescArticulo.Contains("Requerimiento Pago Fácil")))
        {
            PoseeRequerimientoPagoFacil = true;
        }
        else
        {
            PoseeRequerimientoPagoFacil = false;
        }

    }
    protected void dlDetalleNivel_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DetalleNivele detNivel = (DetalleNivele)((System.Web.UI.WebControls.ListViewDataItem)(e.Item)).DataItem;
        if (detNivel.MultiplesElementos)
        {
            List<Producto> productos = (from D in detNivel.objNivel.ColDetalleNiveles
                                        where D.Grupo == detNivel.Grupo
                                        select D.objProducto).ToList<Producto>();


            (e.Item.FindControl("lblProducto") as Label).Text = Helper.ObtenerDescripcionCompletaProductoEnComun(productos);
            (e.Item.FindControl("btnSeleccion") as Button).Visible = true;
            (e.Item.FindControl("btnSolicitar") as Button).Attributes.Add("DataItemIndex", ((System.Web.UI.WebControls.ListViewDataItem)(e.Item)).DataItemIndex.ToString());
            (e.Item.FindControl("btnSolicitar") as Button).Attributes.Add("Grupo", detNivel.Grupo.ToString());


            (e.Item.FindControl("listProdRegalo") as ListBox).DataTextField = "Descripcion";
            (e.Item.FindControl("listProdRegalo") as ListBox).DataValueField = "IdProducto";
            (e.Item.FindControl("listProdRegalo") as ListBox).DataSource = productos;
            (e.Item.FindControl("listProdRegalo") as ListBox).DataBind();



        }
        else
        {
            (e.Item.FindControl("lblProducto") as Label).Text = Helper.ObtenerDescripcionCompletaProducto(detNivel.objProducto);
            (e.Item.FindControl("btnSeleccion") as Button).Visible = false;

        }
    }


    protected void btnSolicitar_Click(object sender, EventArgs e)
    {
        int index = int.Parse((sender as Button).Attributes["DataItemIndex"].ToString());
        int grupo = int.Parse((sender as Button).Attributes["Grupo"].ToString());
        long idProducto = long.Parse((dlDetalleNivel.Items[index].FindControl("listProdRegalo") as ListBox).SelectedItem.Value);

        (dlDetalleNivel.Items[index].FindControl("lblFragancia") as Label).Text = " " + (dlDetalleNivel.Items[index].FindControl("listProdRegalo") as ListBox).SelectedItem.Text + " ";

        Nivele CurrentNivel = (Nivele)Session["Nivel"];
        Presentacion presentacion = (from D in CurrentNivel.ColDetalleNiveles
                                     where D.Grupo == grupo
                                     && D.Regalo == idProducto
                                     select D.objPresentacion).Single<Presentacion>();

        (dlDetalleNivel.Items[index].FindControl("HiddenIdPresentacion") as HiddenField).Value = presentacion.IdPresentacion.ToString();

    }
}
