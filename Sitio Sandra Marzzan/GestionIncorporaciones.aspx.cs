using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using CommonMarzzan;
using System.Data;
using System.Collections;

public partial class GestionIncorporaciones : System.Web.UI.Page
{
   
    public static List<Composicion> componentes = new List<Composicion>();
    public static Marzzan_InfolegacyDataContext _dc = new Marzzan_InfolegacyDataContext();


    public List<Composicion> Composiciones
    {
        set 
        {
            if (Session["Composiciones"] == null)
            {
                Session.Add("Composiciones", value);
            }
            else
                Session["Composiciones"] = value;
        }
        get {
            return (List<Composicion>)Session["Composiciones"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Add("CantidadesGrupo", new Hashtable());
           

            var Allproductos = from prod in _dc.Productos
                               where prod.Tipo == 'A' && prod.Descripcion.StartsWith("Incorpora") &&
                               prod.Nivel == 3 && ! prod.Descripcion.Contains("Cánepa") 
                               select prod;


            cboPromociones.DataValueField = "IdProducto";
            cboPromociones.DataTextField = "Descripcion";
            cboPromociones.DataSource = Allproductos.ToList<Producto>();
            cboPromociones.DataBind();

           
            componentes = new List<Composicion>();

            InitCombos();
            
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

            Producto linea = (from P in _dc.Productos
                              where P.IdProducto == long.Parse(e.Text)
                              select P).First<Producto>();

            if (linea.Tipo != 'P' && linea.Tipo != 'D')
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
            cboPresentaciones.DataSource = from P in Helper.GetPresentaciones(long.Parse(datos[0].ToString()))
                                           where P.Activo.Value    
                                           select P;
        }
        else
        {
            cboPresentaciones.DataSource = Helper.GetPresentacionesByTipoProducto(long.Parse(datos[1].ToString()));
        }

        cboPresentaciones.DataBind();
    }

    protected void cboPromociones_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (long.Parse(cboPromociones.SelectedValue) > 0)
        {

            var composiciones = from C in _dc.Composicions
                                where C.ComponentePricipal == long.Parse(cboPromociones.SelectedValue)
                                select C;

            Composiciones = composiciones.ToList<Composicion>();
            grillaComponentes.DataSource = Composiciones;
            grillaComponentes.DataBind();
            upGrilla.Update();

            //grillaComponentes.Rebind();
            //upGrilla.Update();
        }

    }

    protected void cboDefiniciones_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        for (int i = 0; i < 5; i++)
        {
            cboDefiniciones.Items.Insert(i, new RadComboBoxItem("Grupo Nº " + Convert.ToString(i + 1)  + " (Todos)" , Convert.ToString(i + 1)));
        }

        for (int i = 5; i < 10; i++)
        {
            cboDefiniciones.Items.Insert(i, new RadComboBoxItem("Grupo Nº " + Convert.ToString(i - 4) + " (Especificos)", Convert.ToString(i + 1)));
        }

        cboDefiniciones.Items.Insert(10, new RadComboBoxItem("Grupo Descuento","99"));

        cboPresentaciones.DataBind();
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

            if (((itemPre.Descripcion == cboPresentaciones.Text && cboFragancias.SelectedValue != "") || (cboFragancias.SelectedValue == "")) && (!itemPre.Activo.HasValue || itemPre.Activo.Value))
            {
                newcomp.Cantidad = txtCantidad.Value.ToString();
                newcomp.ComponentePricipal = long.Parse(cboPromociones.SelectedValue);
                newcomp.ComponenteHijo = itemPre.objProducto.IdProducto;
                newcomp.Presentacion = itemPre.IdPresentacion;
                newcomp.objProductoHijo = itemPre.objProducto;
                newcomp.objPresentacion = itemPre;
                newcomp.Grupo = long.Parse(cboDefiniciones.SelectedValue);

                if (long.Parse(cboDefiniciones.SelectedValue) <= 5)
                {
                    /// T: indica que se tiene que generar una línea por cada uno 
                    /// de los productos incluidos en la definición.
                    newcomp.TipoComposicion = "T";
                    if ((Session["CantidadesGrupo"] as Hashtable).ContainsKey(long.Parse(cboDefiniciones.SelectedValue)))
                    {
                        (Session["CantidadesGrupo"] as Hashtable)[long.Parse(cboDefiniciones.SelectedValue)] = txtCantidad.Value;
                    }   
                    else
                    {
                        (Session["CantidadesGrupo"] as Hashtable).Add(long.Parse(cboDefiniciones.SelectedValue),txtCantidad.Value);
                    }
                }
                else if (long.Parse(cboDefiniciones.SelectedValue) == 99)
                {
                    /// D: indica que los conceptos contenidos se tiene que 
                    /// enviar como parte de la facturación
                    newcomp.TipoComposicion = "D";
                }
                else
                {
                    /// U: indica que se le debe dar al usuario la posibilidad
                    /// de seleccionar uno o mas productos incluidos segun la cantidad.
                    newcomp.TipoComposicion = "U";
                    if ((Session["CantidadesGrupo"] as Hashtable).ContainsKey(long.Parse(cboDefiniciones.SelectedValue)))
                    {
                        (Session["CantidadesGrupo"] as Hashtable)[long.Parse(cboDefiniciones.SelectedValue)] = txtCantidad.Value;
                    }
                    else
                    {
                        (Session["CantidadesGrupo"] as Hashtable).Add(long.Parse(cboDefiniciones.SelectedValue), txtCantidad.Value);
                    }
                }

                Composiciones.Add(newcomp);
            }
            
        }

        grillaComponentes.DataSource = Composiciones;
        grillaComponentes.DataBind();
        upGrilla.Update();

        //grillaComponentes.Rebind();
        //upGrilla.Update();

        cboUndNeg.SelectedIndex = 0;
        cboLineas.Text = "";
        cboFragancias.Text = "";
        cboPresentaciones.Text = "";
        cboDefiniciones.Text = "";
        cboPresentaciones.ClearSelection();
        txtCantidad.Value = 1;
        upAgregarDefinicion.Update();

    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {

        long idProdPromo = long.Parse(cboPromociones.SelectedValue);
        foreach (Composicion itemComp in Composiciones)
        {
            if (itemComp.IdComposicion == 0)
            {
                Composicion newcomp = new Composicion();
                
                if ((Session["CantidadesGrupo"] as Hashtable).ContainsKey(itemComp.Grupo))
                    newcomp.Cantidad = (Session["CantidadesGrupo"] as Hashtable)[itemComp.Grupo].ToString();

                newcomp.ComponentePricipal = itemComp.ComponentePricipal;
                newcomp.ComponenteHijo = itemComp.ComponenteHijo;
                newcomp.Presentacion = itemComp.Presentacion;
                newcomp.Grupo = itemComp.Grupo;
                newcomp.TipoComposicion = itemComp.TipoComposicion;
                _dc.Composicions.InsertOnSubmit(newcomp);

            }
            
        }

        foreach (long item in (Session["CantidadesGrupo"] as Hashtable).Keys)
        {

            var a = from C in _dc.Composicions
                     where C.objProducto.IdProducto == idProdPromo
                     && C.Grupo == item
                     select C;

            foreach (Composicion comp in a)
            {
                comp.Cantidad = (Session["CantidadesGrupo"] as Hashtable)[item].ToString();
                
            }
        }

        Producto ProdPromo = (from P in _dc.Productos
                              where P.IdProducto == idProdPromo
                              select P).First<Producto>();

        _dc.SubmitChanges();
        grillaComponentes.DataSource = null;
        grillaComponentes.DataBind();
        upGrilla.Update();

        Composiciones = new List<Composicion>();
        Session["CantidadesGrupo"] = new Hashtable();
        cboPromociones.SelectedIndex = -1;

        cboLineas.Text = "";
        cboFragancias.Text = "";
        cboPresentaciones.Text = "";
        cboDefiniciones.Text = "";
        upAgregarDefinicion.Update();
        upSelectedPromo.Update();

    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("Inicio.aspx");

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

        var composiciones = from C in _dc.Composicions
                            where C.ComponentePricipal == long.Parse(cboPromociones.SelectedValue)
                            select C;

        Composiciones = composiciones.ToList<Composicion>();


        grillaComponentes.DataSource = Composiciones;
        grillaComponentes.DataBind();
        upGrilla.Update();

        //grillaComponentes.Rebind();
        //upGrilla.Update();

    }

    protected void grillaComponentes_ItemDataBound(object sender, GridItemEventArgs e)
    {
        CreateHeaderControls(e);
    }
    protected void grillaComponentes_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        grillaComponentes.DataSource = Composiciones;
        upGrilla.Update();
    }
    private void CreateHeaderControls(GridItemEventArgs e)
    {
        if (e.Item is GridGroupHeaderItem)
        {
            GridGroupHeaderItem item = (GridGroupHeaderItem)e.Item;
            System.Data.DataRowView groupDataRow = (System.Data.DataRowView)e.Item.DataItem;
            Label lblTitulo = new Label();
            lblTitulo.ID = "lbltitutlogrupo";
            string NroGrupo = "";
            
            if (groupDataRow != null)
            {
                if (int.Parse(groupDataRow["Grupo"].ToString()) <= 5)
                {
                    lblTitulo.Text = "          Grupo Nº " + groupDataRow["Grupo"].ToString() + " (Todos)";
                    NroGrupo = groupDataRow["Grupo"].ToString();
                }
                else if (int.Parse(groupDataRow["Grupo"].ToString()) == 99)
                {
                    lblTitulo.Text = "          Grupo Descuentos";
                    NroGrupo = groupDataRow["Grupo"].ToString();
                }
                else
                {
                    lblTitulo.Text = "          Grupo Nº " + Convert.ToString(int.Parse(groupDataRow["Grupo"].ToString()) - 5) + " (Especifico)" + " - Cantidad: " + groupDataRow["Cantidad"].ToString(); ;
                    NroGrupo = Convert.ToString(int.Parse(groupDataRow["Grupo"].ToString()) - 5);
                }
            }

            ImageButton img = new ImageButton();
            img.ID = "DeleteAll" + new Random().Next(1,1000).ToString();
            img.ImageUrl = "~/Imagenes/Delete.gif";
            img.OnClientClick = "DeleteAllGroup(" + groupDataRow["Grupo"].ToString() + ");";
            item.DataCell.Controls.Add(img);
            item.DataCell.Controls.Add(lblTitulo);
        }

    }

    public void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

        //ImageButton img = (ImageButton)sender;
        //GridGroupHeaderItem groupHeader = (GridGroupHeaderItem)img.NamingContainer;
        //long grupo = long.Parse(groupHeader.DataCell.Text.Split(';')[0].Split(':')[1].Trim());

        long grupo = long.Parse(e.Argument);

        var composiciones = (from C in Composiciones
                             where C.Grupo == grupo
                             select C).ToList<Composicion>();


        foreach (Composicion itemComp in composiciones)
        {

            if (itemComp.IdComposicion > 0)
            {
                Composicion compDelete = (from C in _dc.Composicions
                                          where C.IdComposicion == itemComp.IdComposicion
                                          select C).First<Composicion>();

                _dc.Composicions.DeleteOnSubmit(compDelete);
            }
        }

        _dc.SubmitChanges();


        composiciones = (from C in _dc.Composicions
                         where C.ComponentePricipal == long.Parse(cboPromociones.SelectedValue)
                         select C).ToList<Composicion>();

        Composiciones = composiciones;
        grillaComponentes.DataSource = Composiciones;
        grillaComponentes.DataBind();
        upGrilla.Update();

        //grillaComponentes.Rebind();
        //upGrilla.Update();
    }

    private void img_Click(object sender, ImageClickEventArgs e)
    {
       


    }


    

    
    
}
