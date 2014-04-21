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
using CommonMarzzan;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.Script.Services;
using System.Data.OleDb;

public partial class GestionComposicionRemitos : BasePage
{
    public class tempComponentes
    {
        public long IdComposicion { get; set; }
        public string Remito { get; set; }
        public string CodigoRemito { get; set; }
        public string Componente { get; set; }
        public int Cantidad { get; set; }

    }

    protected override void PageLoad()
    {
        if (!IsPostBack)
        {
            cboRemitos.DataValueField = "CodigoRemito";
            cboRemitos.DataTextField = "Remito";
            cboRemitos.DataSource = (from r in (FiltroComponentes("")["Componentes"] as List<tempComponentes>)
                                     orderby r.Remito
                                     select new
                                     {
                                         r.Remito,
                                         r.CodigoRemito
                                     }).Distinct().ToList();
            cboRemitos.DataBind();
        }
    }


    [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> FiltroComponentes(string codigoComponente)
    {

        Dictionary<string, object> datos = new Dictionary<string, object>();

        using (Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext())
        {
            var componentesRemitos = (from c in dc.ComposicionRemitos
                                      join r in dc.Presentacions on c.CodigoRemito equals r.Codigo
                                      where c.CodigoRemito.Contains(codigoComponente)
                                      select new tempComponentes
                                      {
                                          IdComposicion = c.IdComposicionRemito,
                                          Cantidad = c.Cantidad.Value,
                                          Componente = c.objPresentacion.Codigo + " - " + c.objPresentacion.objProducto.Descripcion + " x " + c.objPresentacion.Descripcion,
                                          Remito = r.Descripcion + "(" + r.Codigo + ")",
                                          CodigoRemito = r.Codigo
                                      }).ToList();

            datos.Add("Componentes", componentesRemitos);
        }

        return datos;

    }


    [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> EliminarComponentes(long idComponente)
    {

        using (Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext())
        {
            var componenteRemito = (from c in dc.ComposicionRemitos
                                      where c.IdComposicionRemito == idComponente
                                      select c).FirstOrDefault();

            dc.ComposicionRemitos.DeleteOnSubmit(componenteRemito);

            dc.SubmitChanges();
        }

        return FiltroComponentes("");

    }

     [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> ActualizarRemito(string codigoRemito, long componente, int cantidad)
    {

        using (Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext())
        {
            ComposicionRemito comp = new ComposicionRemito();
            comp.Cantidad = cantidad;
            comp.PresentacionComponente = componente;

            if (codigoRemito.Length >= 8)
                comp.CodigoRemito = codigoRemito;
            else
            {
                long idPresentacion = long.Parse(codigoRemito);

                comp.CodigoRemito = (from p in dc.Presentacions
                                     where p.IdPresentacion == idPresentacion
                                     select p.Codigo).FirstOrDefault();
            }
                                    
            
           


            dc.ComposicionRemitos.InsertOnSubmit(comp);

            dc.SubmitChanges();
        }

        return FiltroComponentes("");

    }
    
}