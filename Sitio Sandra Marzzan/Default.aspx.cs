using System;
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
using System.Collections;
using CommonMarzzan;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Marzzan_InfolegacyDataContext dbLocal = new Marzzan_InfolegacyDataContext();

        var usos = (from u in dbLocal.View_ProductosPlanos
                    orderby u.CodigoUnidad, u.CodigoLinea, u.CodFragancia
                    select u).ToList();


        XElement DetalleProductos = new XElement("result",
           usos.GroupBy(i => new { i.Unidad, i.CodigoUnidad, i.Linea, i.CodigoLinea })
           .Select(g =>
               new XElement("Unidad",
                            new XElement("Codigo", g.Key.CodigoUnidad),
                            new XElement("Nombre", g.Key.Unidad),
                            new XElement("CodigoL", g.Key.CodigoUnidad + g.Key.CodigoLinea),
                            new XElement("NombreL", g.Key.Linea),
                            new XElement("PresentacionesDisponible",
                                new XAttribute("Descripcion", String.Join("@",
                                    g.Select(l =>
                                        l.Codigo.LastIndexOf('-') >= 0 ?
                                        l.Codigo.Substring(l.Codigo.LastIndexOf('-') + 1).Trim() + '-' + l.Descripcion :
                                        l.IdPresentacion.ToString() + '-' + l.Descripcion).Distinct().ToArray())))
               )
           )
       );


        DetalleProductos.Save(Server.MapPath("") + @"\DetalleProductos.xml");



        int count = 0;
        XElement usosAplanados = new XElement("Fragancias",
           usos.GroupBy(i => new { i.CodigoUnidad, i.CodigoLinea, i.CodFragancia, i.Fragancia } )
           .Select(g =>
               new XElement("Fragancia",
                            new XElement("CodigoF", g.Key.CodFragancia),
                            new XElement("NombreF", g.Key.Fragancia,
                                new XAttribute("CodigoBusqueda", String.Join("@", g.Select(l => g.Key.CodigoUnidad + g.Key.CodigoLinea +
                                    //(l.Codigo.LastIndexOf('-') >= 0 ? l.Codigo.Substring(l.Codigo.LastIndexOf('-') + 1).Trim() : l.IdPresentacion.ToString() )
                                    // l.IdPresentacion.ToString()
                                        string.Format("{0:00}", count++)
                                        ).ToArray())),
                                new XAttribute("Presentaciones", String.Join("@", g.Select(l => l.Descripcion).ToArray())))
               )
           )
       );


        usosAplanados.Save(Server.MapPath("") + @"\ProductosPlanos.xml");


    }
    protected void RadTreeView1_DataBound(object sender, EventArgs e)
    {
        ((RadTreeView)sender).ExpandAllNodes();
    }
    protected void RadTreeView1_HandleDrop(object sender, RadTreeNodeDragDropEventArgs e)
    {
        RadTreeNode sourceNode = e.SourceDragNode;
        RadTreeNode destNode = e.DestDragNode;
        RadTreeViewDropPosition dropPosition = e.DropPosition;

        string result = "";

        if (e.HtmlElementID == DataGrid1.ClientID)
        {
            DataTable dt = (DataTable)Session["DataTable"];
            foreach (RadTreeNode node in e.DraggedNodes)
            {
                string[] values = { sourceNode.Text, sourceNode.Value };
                dt.Rows.Add(values);

                DataGrid1.DataSource = dt;
                DataGrid1.DataBind();
            }
        }
    }

    private void PopulateGrid()
    {
        string[] values = { "One", "Two", "Three" };

        DataTable dt = new DataTable();
        dt.Columns.Add("Text");
        dt.Columns.Add("Value");
        dt.Columns.Add("Category");
        dt.Rows.Add(values);
        dt.Rows.Add(values);
        dt.Rows.Add(values);
        Session["DataTable"] = dt;

        DataGrid1.DataSource = dt;
        DataGrid1.DataBind();
    }
}
