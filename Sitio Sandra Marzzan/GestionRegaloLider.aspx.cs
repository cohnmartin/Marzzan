using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using CommonMarzzan;

public partial class GestionRegaloLider : System.Web.UI.Page
{
    public static List<Producto> _productos = null;
    public static List<Composicion> componentes = new List<Composicion>();
    public static Marzzan_InfolegacyDataContext _dc = new Marzzan_InfolegacyDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Add("Componentes", new List<Composicion>());

            Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

            if (_productos == null || _productos.Count == 0)
            {
                var Allproductos = from prod in dc.Productos
                                   where prod.Tipo == 'A' || prod.Tipo == 'P' || prod.Tipo == 'R'
                                   select prod;

                _productos = Allproductos.ToList<Producto>();
            }


            var productos = from prod in _productos
                            where prod.ColHijos.Count > 0
                            && prod.Tipo == 'A' || prod.Tipo == 'P' || prod.Tipo == 'R'
                            select prod;

            componentes = new List<Composicion>();

            InitCombos();
        }
        else
        {

            if (Session["Componentes"] == null)
            { 
                Response.Redirect("~/login.aspx");
            }
        }
    }

    private void InitCombos()
    {

        cboUndNeg.DataTextField = "Descripcion";
        cboUndNeg.DataValueField = "IdProducto";
        cboUndNeg.DataSource = Helper.GetProductosByNivel(1);
        cboUndNeg.DataBind();
        cboUndNeg.Items.Insert(0, new RadComboBoxItem("- Seleccione una Unidad -"));

    }


    protected void cboUndNeg_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        InitCombos();
    }

    protected void cboLineas_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        if (e.Text != "")
        {
            cboLineas.DataTextField = "Descripcion";
            cboLineas.DataValueField = "IdProducto";
            cboLineas.DataSource = Helper.GetProductosByParent(long.Parse(e.Text));
            cboLineas.DataBind();
        }
    }

    protected void cboTipoProductos_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        //if (e.Text != "")
        //{
        //    Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
        //    Producto linea = (from P in dc.Productos
        //                      where P.IdProducto == long.Parse(e.Text)
        //                      select P).First<Producto>();

        //    if (linea.Tipo != 'P')
        //    {

        //        cboTipoProductos.DataTextField = "Descripcion";
        //        cboTipoProductos.DataValueField = "IdProducto";
        //        cboTipoProductos.DataSource = Helper.GetProductosByParent(long.Parse(e.Text));
        //        cboTipoProductos.DataBind();
        //    }
        //    else
        //    {
        //        cboTipoProductos.Enabled = false;
        //        cboFragancias.Enabled = false;
        //        cboPresentaciones.Enabled = false;
        //    }
        //}
    }

    protected void cboFragancias_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        if (e.Text != "")
        {
            Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
            Producto linea = (from P in dc.Productos
                              where P.IdProducto == long.Parse(e.Text)
                              select P).First<Producto>();

            if (linea.Tipo != 'P' && linea.Tipo != 'D' && linea.Tipo != 'R')
            {

                cboFragancias.DataTextField = "Descripcion";
                cboFragancias.DataValueField = "IdProducto";
                cboFragancias.DataSource = Helper.GetProductosByParent(long.Parse(e.Text));
                cboFragancias.DataBind();
                cboFragancias.Items.Insert(0, new RadComboBoxItem("- Todas -", "0"));
            }
            else
            {
                cboFragancias.Text = "";
                cboFragancias.Enabled = false;
                cboPresentaciones.Enabled = false;
                upAgregarDefinicion.Update();
            }
        }
    }

    protected void cboPresentaciones_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        string[] datos = e.Text.Split('|');

        cboPresentaciones.DataTextField = "Descripcion";
        cboPresentaciones.DataValueField = "IdPresentacion";
        if (datos[0].ToString() != "0")
        {
            cboPresentaciones.DataSource = Helper.GetPresentaciones(long.Parse(datos[0].ToString()));
        }
        else
        {
            cboPresentaciones.DataSource = Helper.GetPresentacionesByTipoProducto(long.Parse(datos[1].ToString()));
        }

        cboPresentaciones.DataBind();
    }

    protected void cboDefiniciones_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        for (int i = 0; i < 10; i++)
        {
            cboDefiniciones.Items.Insert(i, new RadComboBoxItem("Productos de Regalo Nº " + Convert.ToString(i + 1), Convert.ToString(i + 1)));
        }


        for (int i = 10; i < 11; i++)
        {
            cboDefiniciones.Items.Insert(i, new RadComboBoxItem("Grupo Conceptos a Facturar ", Convert.ToString(i + 1)));
        }

       // cboPresentaciones.DataBind();
    }

    protected void btnAgregarDefinicion_Click(object sender, EventArgs e)
    {
        List<Presentacion> presentaciones = new List<Presentacion>();
        if (cboFragancias.SelectedValue == "")
        {
            presentaciones = Helper.GetAllPresentacionesByPromo(long.Parse(cboLineas.SelectedValue));
        }
        else if (cboFragancias.SelectedValue != "0")
        {
            presentaciones = Helper.GetPresentaciones(long.Parse(cboFragancias.SelectedValue));
        }
        else
        {
            presentaciones = Helper.GetAllPresentacionesByLnea(long.Parse(cboLineas.SelectedValue));
        }


        foreach (Presentacion itemPre in presentaciones)
        {
             Composicion newcomp = new Composicion();

             if ((itemPre.Descripcion == cboPresentaciones.Text && cboFragancias.SelectedValue != "")
                || cboFragancias.SelectedValue == "")
             {
                 newcomp.Cantidad = "1";
                 newcomp.ComponentePricipal = long.Parse(cboPromociones.SelectedValue);
                 newcomp.ComponenteHijo = itemPre.objProducto.IdProducto;
                 newcomp.Presentacion = itemPre.IdPresentacion;
                 newcomp.objProductoHijo = itemPre.objProducto;
                 newcomp.objPresentacion = itemPre;
                 newcomp.Grupo = long.Parse(cboDefiniciones.SelectedValue);

                 if (long.Parse(cboDefiniciones.SelectedValue) != 11)
                    newcomp.TipoComposicion = "C";
                 else
                    newcomp.TipoComposicion = "D";


                 (Session["Componentes"] as List<Composicion>).Add(newcomp);
             }
        }

        grillaComponentes.DataSource = (Session["Componentes"] as List<Composicion>);
        grillaComponentes.DataBind();
        upGrilla.Update();

        cboUndNeg.SelectedIndex = 0;
        cboLineas.Text = "";
        cboFragancias.Text = "";
        cboPresentaciones.Text = "";
        cboDefiniciones.Text = "";
        upAgregarDefinicion.Update();

    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {

        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
        long idProdPromo = long.Parse(cboPromociones.SelectedValue);
        foreach (Composicion itemComp in (Session["Componentes"] as List<Composicion>))
        {
            if (itemComp.IdComposicion == 0)
            {
                Composicion newcomp = new Composicion();
                newcomp.Cantidad = itemComp.Cantidad;
                newcomp.ComponentePricipal = itemComp.ComponentePricipal;
                newcomp.ComponenteHijo = itemComp.ComponenteHijo;
                newcomp.Presentacion = itemComp.Presentacion;
                newcomp.Grupo = itemComp.Grupo;
                newcomp.TipoComposicion = itemComp.TipoComposicion;
                dc.Composicions.InsertOnSubmit(newcomp);

            }
        }

        Producto ProdPromo = (from P in dc.Productos
                              where P.IdProducto == idProdPromo
                              select P).First<Producto>();

        dc.SubmitChanges();
        grillaComponentes.DataSource = null;
        grillaComponentes.DataBind();
        upGrilla.Update();

        Session["Componentes"] = new List<Composicion>();
        cboPromociones.SelectedIndex = 0;

        cboLineas.Text = "";
        cboFragancias.Text = "";
        cboPresentaciones.Text = "";
        cboDefiniciones.Text = "";
        upAgregarDefinicion.Update();


    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("Inicio.aspx");

    }

    protected void grillaComponentes_ItemCreated(object sender, GridItemEventArgs e)
    {
        CreateHeaderControls(e);
    }
    protected void grillaComponentes_ItemDataBound(object sender, GridItemEventArgs e)
    {
        CreateHeaderControls(e);
    }
    private void CreateHeaderControls(GridItemEventArgs e)
    {
        if (e.Item is GridGroupHeaderItem)
        {
            GridGroupHeaderItem item = (GridGroupHeaderItem)e.Item;
            System.Data.DataRowView groupDataRow = (System.Data.DataRowView)e.Item.DataItem;
            Label lblTitulo = new Label();
            lblTitulo.ID = "lbltitutlogrupo";

            if (groupDataRow != null)
            {
                if (groupDataRow["Grupo"].ToString()!= "11")
                    lblTitulo.Text = "          Productos de Regalo Nº " + groupDataRow["Grupo"].ToString();
                else
                    lblTitulo.Text = "          Grupo Conceptos a Facturar";
                    
            }

            ImageButton img = new ImageButton();
            img.ID = "DeleteAll";
            img.ImageUrl = "~/Imagenes/Delete.gif";
            img.Click += new ImageClickEventHandler(img_Click);
            item.DataCell.Controls.Add(img);
            item.DataCell.Controls.Add(lblTitulo);
        }

    }
    private void img_Click(object sender, ImageClickEventArgs e)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
        ImageButton img = (ImageButton)sender;
        GridGroupHeaderItem groupHeader = (GridGroupHeaderItem)img.NamingContainer;
        long grupo = long.Parse(groupHeader.DataCell.Text.Split(';')[0].Split(':')[1].Trim());

        var composiciones = (from C in (Session["Componentes"] as List<Composicion>)
                             where C.Grupo == grupo
                             select C).ToList<Composicion>();


        foreach (Composicion itemComp in composiciones)
        {

            (Session["Componentes"] as List<Composicion>).Remove(itemComp);

            if (itemComp.IdComposicion > 0)
            {
                Composicion compDelete = (from C in dc.Composicions
                                          where C.IdComposicion == itemComp.IdComposicion
                                          select C).First<Composicion>();

                dc.Composicions.DeleteOnSubmit(compDelete);
            }
        }

        dc.SubmitChanges();
        grillaComponentes.DataSource = (Session["Componentes"] as List<Composicion>).ToList<Composicion>();
        grillaComponentes.DataBind();
        upGrilla.Update();


    }


    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        long idConf = long.Parse(((sender as ImageButton).NamingContainer as GridDataItem).GetDataKeyValue("Idcomposicion").ToString());

        if (idConf > 0)
        {
            Composicion conf = (from C in _dc.Composicions
                                where C.IdComposicion == idConf
                                select C).First<Composicion>();

            _dc.Composicions.DeleteOnSubmit(conf);
            _dc.SubmitChanges();
        }

        (Session["Componentes"] as List<Composicion>).RemoveAt(((sender as ImageButton).NamingContainer as GridDataItem).DataSetIndex);

        grillaComponentes.DataSource = (Session["Componentes"] as List<Composicion>).ToList<Composicion>();
        grillaComponentes.DataBind();
        upGrilla.Update();


    }
    
    protected void cboPromociones_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (long.Parse(cboPromociones.SelectedValue) > 0)
        {
            Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

            var composiciones = from C in dc.Composicions
                                where C.ComponentePricipal == long.Parse(cboPromociones.SelectedValue)
                                select C;

            Session["Componentes"] = composiciones.ToList<Composicion>();
            grillaComponentes.DataSource = composiciones;
            grillaComponentes.DataBind();
            upGrilla.Update();

            
        }

    }
}
