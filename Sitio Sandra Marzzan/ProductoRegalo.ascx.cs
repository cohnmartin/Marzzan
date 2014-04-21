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
using CommonMarzzan;

public partial class ProductoRegalo : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            Session.Remove("IndexRowPromo");
    }
    public void InitControl(long id, int indexPromo, int indexRegalo, long Grupo)
    {
        try
        {
            listProdRegalo.Items.Clear();
            Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();


            var productosRegalo = from P in dc.Productos
                                  from C in P.ColComposiciones
                                  where P.IdProducto == id
                                  && C.TipoComposicion == "O"
                                  && C.Grupo == Grupo
                                  && C.objPresentacion.Activo.Value
                                  orderby C.objProductoHijo.objPadre.Descripcion
                                  select new
                                  {
                                      Descripcion = C.objProductoHijo.objPadre.Descripcion + " " + C.objProductoHijo.Descripcion + " x " + C.objPresentacion.Descripcion,
                                      IdProducto = C.objPresentacion.IdPresentacion
                                  };


            listProdRegalo.DataTextField = "Descripcion";
            listProdRegalo.DataValueField = "IdProducto";
            listProdRegalo.DataSource = productosRegalo;
            listProdRegalo.DataBind();
        }
        catch (Exception err)
        {
            listProdRegalo.Items.Add(err.Message);
            if (err.InnerException != null)
                listProdRegalo.Items.Add(err.InnerException.Message);
        }

        Session.Add("IndexRowPromo", indexPromo);
        Session.Add("IndexRowRegalo", indexRegalo);

    }
    protected void btnSolicitar_Click(object sender, EventArgs e)
    {
        ((IGrilla)(((System.Web.UI.WebControls.WebControl)(sender)).NamingContainer.NamingContainer)).AsociarRegalo(listProdRegalo.SelectedItem.Value, listProdRegalo.SelectedItem.Text, int.Parse(Session["IndexRowPromo"].ToString()), int.Parse(Session["IndexRowRegalo"].ToString()));

    }
}
