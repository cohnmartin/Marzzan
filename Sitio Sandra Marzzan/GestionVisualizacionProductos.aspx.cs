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
using Telerik.Web.UI;
using System.IO;
using CommonMarzzan;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.Script.Services;

public partial class GestionVisualizacionProductos : BasePage
{
    public class TempPresentaciones
    {
        public long IdPresentacion { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

    }

    public static List<Producto> _productos = null;
    public static List<Presentacion> _presentaciones = null;
    Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

    protected override void PageLoad()
    {
        Session.Timeout = 30;

        if (!IsPostBack)
        {

            var Allproductos = from prod in dc.Productos
                               where (prod.Tipo == 'A' || prod.Tipo == 'P' || prod.Tipo == 'D')
                               select prod;

            _presentaciones = (from P in dc.Presentacions
                               select P).ToList();


            _productos = Allproductos.ToList<Producto>();


             

            var productos = from prod in _productos
                            where prod.ColHijos.Count > 0
                            && (prod.Tipo == 'A' || prod.Tipo == 'P' || prod.Tipo == 'D')
                            select prod;

            RadTreeProductos.DataSource = Helper.LINQToDataTable<Producto>(productos);
            RadTreeProductos.DataBind();

        }
    }

    protected void RadTreeProductos_NodeDataBound(object sender, RadTreeNodeEventArgs e)
    {
        DataRowView row = (DataRowView)e.Node.DataItem;
        e.Node.Attributes.Add("Padre", row["IdProducto"].ToString());

        List<Producto> Hijos = (from P in _productos
                                where P.Padre == long.Parse(row["IdProducto"].ToString())
                                select P).First<Producto>().ColHijos.ToList<Producto>();


        if (Hijos.Count == 0)
        {


            List<long> ids = (from P in dc.Productos
                              where P.Padre == long.Parse(row["IdProducto"].ToString())
                              select P.IdProducto).ToList<long>();



            if (row["Tipo"].ToString() != "P" && row["Tipo"].ToString() != "D")
            {
                var presentaciones = (from P in _presentaciones
                                      where ids.Contains(P.Padre.Value)
                                      select new
                                      {
                                          P.Padre.Value,
                                          P.Descripcion
                                      }).Distinct().ToList();

                foreach (string item in presentaciones.Select(w => w.Descripcion).Distinct().ToList())
                {
                    RadTreeNode nodopre = new RadTreeNode(item, item);
                    nodopre.Attributes.Add("Cargar", "true");
                    nodopre.Attributes.Add("Padre", row["IdProducto"].ToString());
                    e.Node.Nodes.Add(nodopre);

                }
            }
            else
            {
                var presentaciones = (from P in _presentaciones
                                      where ids.Contains(P.Padre.Value) && P.objProducto.objConfPromocion != null
                                      select new
                                     {
                                         P.Padre.Value,
                                         P.Descripcion
                                     }).Distinct().ToList();

                foreach (var item in presentaciones)
                {
                    RadTreeNode nodopre = new RadTreeNode(item.Descripcion, item.Descripcion);
                    nodopre.Attributes.Add("Cargar", "true");
                    nodopre.Attributes.Add("Padre", item.Value.ToString());
                    e.Node.Nodes.Add(nodopre);

                }
            }




        }
        else
        {
            e.Node.Attributes.Add("Cargar", "false");
        }

    }

//    public void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
//    {
//        long id = long.Parse(e.Argument.Split('@')[0]);
//        string desc = e.Argument.Split('@')[1];

//        List<long> ids = (from P in _productos
//                          where P.Padre == id
//                          select P.IdProducto).ToList<long>();


//        if (ids.Count == 0)
//        {
//            var presentaciones = (from P in _presentaciones
//                                  where P.Padre.Value == id
//                                  select new TempPresentaciones
//                                  {
//                                      IdPresentacion = P.IdPresentacion,
//                                      Descripcion = P.objProducto.Descripcion,
//                                      Activo = GetActivo(P.Activo)

//                                  }).Distinct().OrderBy(w => w.Descripcion);

//            gvPresentaciones.DataSource = presentaciones;
//        }
//        else
//        {

//            var presentaciones = (from P in _presentaciones
//                                  where ids.Contains(P.Padre.Value) &&
//                                  P.Descripcion == desc
//                                  select new TempPresentaciones
//                                  {
//                                      IdPresentacion = P.IdPresentacion,
//                                      Descripcion = P.objProducto.Descripcion,
//                                      Activo = GetActivo(P.Activo)

//                                  }).Distinct().OrderBy(w => w.Descripcion);

//            gvPresentaciones.DataSource = presentaciones;
//        }

//        gvPresentaciones.DataBind();
////        upGrilla.Update();

//    }

    private static bool GetActivo(bool? Activo)
    {
        if (Activo.HasValue)
            return Activo.Value;
        else
            return false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ServiceActualizacionClientes act = new ServiceActualizacionClientes();
        act.ReGenerarXML();

        _presentaciones = (from P in dc.Presentacions
                           select P).ToList();

    }
    //protected void btnGrabar_Click(object sender, EventArgs e)
    //{
    //    foreach (GridDataItem item in gvPresentaciones.Items)
    //    {
    //        long id = long.Parse(item.GetDataKeyValue("IdPresentacion").ToString());

    //        Presentacion pre = (from P in dc.Presentacions
    //                            where P.IdPresentacion == id
    //                            select P).FirstOrDefault();

    //        pre.Activo = (item.FindControl("chkVisble") as CheckBox).Checked;

    //    }

    //    dc.SubmitChanges();

    //    _presentaciones = (from P in dc.Presentacions
    //                       select P).ToList();
    //}

    #region WEB Metodos
    [WebMethod(EnableSession = true)]
    public static object GetPresentaciones(string Id, string Descripcion)
    {

        long id = long.Parse(Id);
        string desc = Descripcion;

        List<long> ids = (from P in _productos
                          where P.Padre == id
                          select P.IdProducto).ToList<long>();



        if (ids.Count == 0)
        {
            var presentaciones = (from P in _presentaciones
                                  where P.Padre.Value == id
                                  select new TempPresentaciones
                                  {
                                      IdPresentacion = P.IdPresentacion,
                                      Descripcion = P.objProducto.Descripcion,
                                      Activo = GetActivo(P.Activo)

                                  }).Distinct().OrderBy(w => w.Descripcion);

            return presentaciones.ToList();
        }
        else
        {

            var presentaciones = (from P in _presentaciones
                                  where ids.Contains(P.Padre.Value) &&
                                  P.Descripcion == desc
                                  select new TempPresentaciones
                                  {
                                      IdPresentacion = P.IdPresentacion,
                                      Descripcion = P.objProducto.Descripcion,
                                      Activo = GetActivo(P.Activo)

                                  }).Distinct().OrderBy(w => w.Descripcion);

            return presentaciones.ToList();
        }


        


    }

    [WebMethod(EnableSession = true)]
    public static object UpdateData(List<IDictionary<string, object>> datos)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
        long idPadre = 0;
        foreach (IDictionary<string, object> item in datos)
        {
            long id = long.Parse(item["IdPresentacion"].ToString());

            Presentacion pre = (from P in dc.Presentacions
                                where P.IdPresentacion == id
                                select P).FirstOrDefault();
            
            idPadre = pre.Padre.Value;

            pre.Activo = bool.Parse(item["Activo"].ToString());

        }
        
        dc.SubmitChanges();

        _presentaciones = (from P in dc.Presentacions
                           select P).ToList();

        return GetPresentaciones(idPadre.ToString(), "");

       
    }

    #endregion
}
