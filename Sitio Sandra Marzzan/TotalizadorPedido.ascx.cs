using System;
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
using Telerik.Web.UI;
using CommonMarzzan;
public partial class TotalizadorPedido : System.Web.UI.UserControl
{
    int sum = 0;
    decimal sumPrecio = 0;
    public event DeletePedidoHanddler LineaPedidoEliminada;

    public string NameUserControl
    {
        get { return this.ClientID; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void UpdateChange(bool EsResolicitud)
    {
        //string padre = "";
        //foreach (GridDataItem item in grillaPedidos.Items)
        //{
        //   Pedido CurrentePedido = (from P in (Session["detPedido"] as List<Pedido>)
        //                            where P.IdProducto == long.Parse(item.GetDataKeyValue("IdProducto").ToString())
        //                            && P.IdPresentacion == long.Parse(item.GetDataKeyValue("IdPresentacion").ToString())
        //                      select P).Single<Pedido>();

        //   padre = CurrentePedido.IdPadre;
        //   if (EsResolicitud)
        //   {
               
        //       CurrentePedido.ValorTotal = CurrentePedido.Cantidad * CurrentePedido.ValorUnitario;
        //   }
        //   else
        //   {

        //       CurrentePedido.Cantidad = int.Parse((item.FindControl("TextBox1") as TextBox).Text);
        //       CurrentePedido.ValorTotal = CurrentePedido.Cantidad * CurrentePedido.ValorUnitario;
        //   }


        //}

        //if (EsResolicitud)
        //{
        //        sum = 0;
        //        sumPrecio = 0;
        //        List<Pedido> datos = (from P in (Session["detPedido"] as List<Pedido>)
        //                      where P.IdPadre == padre
        //                      select P).ToList<Pedido>();


        //        grillaPedidos.DataSource = datos;
        //        grillaPedidos.DataBind();
        //}

    }
    public bool InitControl(long IdPadre)
    {

        List<DetallePedido> datos = (from P in (Session["detPedido"] as List<DetallePedido>)
                                     select P).ToList<DetallePedido>();

        foreach (DetallePedido item in datos)
        {
            string tipoProducto = item.Tipo;
            string DescProducto = item.DescripcionCompleta;
        }

        if (datos.Count > 0)
        {
            grillaPedidos.DataSource = datos;
            grillaPedidos.DataBind();
  
            return true;
        }
        else
            return false;
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {

        DetallePedido CurrentePedido = (from P in (Session["detPedido"] as List<DetallePedido>)
                                 where P.Producto == long.Parse(((Telerik.Web.UI.GridDataItem)(((System.Web.UI.WebControls.WebControl)sender).NamingContainer)).GetDataKeyValue("Producto").ToString())
                                 && P.Presentacion == long.Parse(((Telerik.Web.UI.GridDataItem)(((System.Web.UI.WebControls.WebControl)sender).NamingContainer)).GetDataKeyValue("Presentacion").ToString())
                                 select P).FirstOrDefault<DetallePedido>();

        string padre = CurrentePedido.IdPadre.ToString();

        (Session["detPedido"] as List<DetallePedido>).Remove(CurrentePedido);

        if (CurrentePedido.IdDetallePedido > 0)
        {
            Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
           
            DetallePedido DetEliminar = (from D in dc.DetallePedidos
                                         where D.IdDetallePedido == CurrentePedido.IdDetallePedido
                                         select D).SingleOrDefault();

            DetEliminar.objCabecera.DetallePedidos.Remove(DetEliminar);
            dc.DetallePedidos.DeleteOnSubmit(DetEliminar);
            dc.SubmitChanges();

        }


        sum = 0;
        sumPrecio = 0;
        List<DetallePedido> datos = (from P in (Session["detPedido"] as List<DetallePedido>)
                                     select P).ToList<DetallePedido>();


        if (datos.Count >0)
            grillaPedidos.DataSource = datos;
        else
            grillaPedidos.DataSource = null;

        grillaPedidos.DataBind();
        
        if (LineaPedidoEliminada != null)
            LineaPedidoEliminada();

        
    }
    public void Clear()
    {
        grillaPedidos.DataSource = null;
        grillaPedidos.DataBind();
    }
    protected void grillaPedidos_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            
            GridDataItem dataItem = (GridDataItem)e.Item;
            string tipoProducto = (dataItem.DataItem as DetallePedido).Tipo;
            string DescProducto = (dataItem.DataItem as DetallePedido).DescripcionCompleta;
            (dataItem.FindControl("btnEditar") as ImageButton).OnClientClick = "EditarPedido('" + (dataItem.DataItem as DetallePedido).Producto.Value.ToString() + "','" + (dataItem.DataItem as DetallePedido).Cantidad.ToString() + "','" + (dataItem.DataItem as DetallePedido).CodigoCompleto + "','" + DescProducto + "'); return false;";
        }
    }
}
