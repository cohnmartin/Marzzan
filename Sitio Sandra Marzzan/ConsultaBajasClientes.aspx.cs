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

public partial class ConsultaBajasClientes : System.Web.UI.Page
{
    private static Marzzan_InfolegacyDataContext Contexto
    {
        get
        {

            if (HttpContext.Current.Session["Context"] == null)
            {
                HttpContext.Current.Session.Add("Context", new Marzzan_InfolegacyDataContext());
            }

            return (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            /// Mes inicial de puesta en marcha las bajas de clientes: 08/2013
            DateTime FechaInicial = DateTime.Parse("31/08/2013");
            DateTime FechaActual = DateTime.Now;
            Session["Context"] = new Marzzan_InfolegacyDataContext();

            List<string> meses = new List<string>();
            do
            {
                meses.Add(HelperWeb.ToCapitalize(string.Format("{0:MMMM-yyyy}", FechaActual)));
                FechaActual = FechaActual.AddMonths(1);

            } while (FechaActual <= FechaInicial);



            cboMeses.AppendDataBoundItems = true;
            cboMeses.DataSource = meses;
            cboMeses.DataBind();


        }

    }

    [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> GetClientesBajas(string mes)
    {

        Dictionary<string, object> datos = new Dictionary<string, object>();

        try
        {
            if (mes != "")
            {
                DateTime mesConsulta = DateTime.Parse("01-" + mes);
                DateTime mesConsultaFinal = mesConsulta.AddMonths(1).AddDays(-1);

                var clientes = (from c in Contexto.HistorialBajas
                                where c.Usuario == long.Parse(HttpContext.Current.Session["IdUsuario"].ToString())
                                && c.FechaBaja >= mesConsulta && c.FechaBaja <= mesConsultaFinal
                                orderby c.FechaBaja
                                select new
                                {
                                    Cliente = c.objRevendedor.Nombre,
                                    Codigo = c.objRevendedor.CodigoExterno,
                                    DiasSinCompras = c.DiasSinPedido,
                                    Grupo = c.Grupo,
                                    FechaBaja = string.Format("{0:dd/MM/yyy}", c.FechaBaja),
                                    MesAño = string.Format("{0:MMMM-yyy}", c.FechaBaja),
                                }).ToList();


                datos.Add("Clientes", clientes.ToList());
            }
            else
            {
                var clientes = (from c in Contexto.HistorialBajas
                                where c.Usuario == long.Parse(HttpContext.Current.Session["IdUsuario"].ToString())
                                orderby c.FechaBaja
                                select new
                                {
                                    Cliente = c.objRevendedor.Nombre,
                                    Codigo = c.objRevendedor.CodigoExterno,
                                    DiasSinCompras = c.DiasSinPedido,
                                    Grupo = c.Grupo,
                                    FechaBaja = string.Format("{0:dd/MM/yyy}", c.FechaBaja),
                                    MesAño = string.Format("{0:MMMM-yyy}", c.FechaBaja),
                                }).ToList();

                datos.Add("Clientes", clientes.ToList());
            }


        }
        catch
        {
            datos.Add("Clientes", new List<object>());
        }

        return datos;

    }

}
