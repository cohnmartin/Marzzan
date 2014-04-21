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


public partial class CargaCuponTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext())
            {
                Session["DivisionesPoliticas"] = (from d in dc.DivisionesPoliticas
                                                  select d).ToList();

                Session["ShowNOESTAENLALISTA"] = false;
            }
            
        }

    }

    [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> AsignarCupon(string ape, string dni,string email, string tel,string cupon, string prov ,string loc)
    {

        Dictionary<string, object> datos = new Dictionary<string, object>();

        using (Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext())
        {
            var cuponGenerado = (from c in dc.CuponesOks
                         where c.Cupon == cupon
                         select c).FirstOrDefault();

            if (cuponGenerado == null)
            {
                datos.Add("Error", "El nro de cupón ingresado no es válido, por favor verifique los datos ingresados.");
            }
            else if (cuponGenerado.Asignado.Value)
            {
                datos.Add("Error", "El nro de cupón ingresado ya esta asignado, por favor verifique los datos ingresados.");
            }
            else
            {
                cuponGenerado.ApellidoNombre = ape.Trim();
                cuponGenerado.DNI = dni.Trim();
                cuponGenerado.Email = email.Trim();
                cuponGenerado.Provincia = prov.Trim();
                cuponGenerado.Localidad = loc.Trim();
                cuponGenerado.Telefonos = tel.Trim();
                cuponGenerado.Asignado = true;

                datos.Add("Ok", "El cupón se asigno correctamente");
            }

            dc.SubmitChanges();

            return datos;
        }
    }

}