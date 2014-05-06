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
using System.Web.Services;
using System.Web.SessionState;
using System.Web.Script.Services;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        /// Consultas a BEJERMAN
        //Marzzan_BejermanDataContext dc = new Marzzan_BejermanDataContext();
        //DateTime fecha = DateTime.Parse("01/09/2012");
        //var cli = (from c in dc.VeoCtaCtes
        //           where c.cve_FContab >= fecha
        //           select c).ToList();
        //UserName.Text = cli.Count.ToString();



        // Response.Redirect("http://www.pedidossm.com.ar/marzzanweb/login.aspx");
        //Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        //Cliente cli = (from c in dc.Clientes
        //               where c.IdCliente == 13138
        //               select c).First();

        //Helper.GestionPedidoConCredito(cli, dc);

        /// Cambios dia 05/06/2012
        /// Se actualizo el helper del cammon por funcionalidad de creditos
        /// Se actualizo NotaPedido por funcionalidad de creditos
        /// Se actualizo GestionDeCreditos 
    }
    private List<string> ObtenerIds(Producto producto)
    {

        List<string> ids = new List<string>();

        if (producto.ColHijos.Count > 0)
        {
            foreach (Producto prodHijo in producto.ColHijos)
            {
                ObtenerIds(prodHijo);
                ids.Add(producto.IdProducto.ToString());
            }

        }
        else
        {
            ids.Add(producto.IdProducto.ToString());
        }

        return ids;

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
        try
        {

            var cliente = (from C in dc.Clientes
                           where C.Habilitado == true && (C.Login == UserName.Text.Trim() && C.CodTipoCliente != "9" && C.CodTipoCliente != "7") || (C.Login == UserName.Text.Trim() && C.CodTipoCliente == null)
			    
                           select C).FirstOrDefault<Cliente>();


	if (cliente  != null)
	{

            if (Password.Text.Trim() == cliente.Pass.Trim())
            {
                lblError.Visible = false;
                Session.Add("IdUsuario", cliente.IdCliente.ToString());
                Session.Add("NombreUsuario", cliente.Nombre.ToLower().ToString());
                Session.Add("TipoUsuario", cliente.TipoCliente.ToUpper());
                Session.Add("CodigoBejerman", cliente.CodigoExterno.Trim());
                Session.Add("EmailUsuario", cliente.Email);
                Session.Add("GrupoCliente", cliente.Clasif1);
                if (cliente.TipoCliente == "TRANSPORTISTA")
                    Session.Add("Transporte", cliente.Transporte);
                else
                    Session.Add("Transporte", "");

                Session.Add("CodigoVendedor", cliente.CodVendedor);

                Response.Redirect("Inicio.aspx");

            }
            else
            {
                lblError.Visible = true;
            }
	}
else
{
                lblErrorHabilitado.Visible = true;
}


        }
        catch
        {
            lblError.Visible = true;
        }



    }
    protected void btnCambioClave_Click(object sender, EventArgs e)
    {
        txtNuevoLogin.Text = UserName.Text;
        tblCambioClave.Visible = true;
        tblIngreso.Visible = false;
        btnLogin.Visible = false;
        upCambio.Update();
    }

    protected void btnAceptarCambio_Click(object sender, EventArgs e)
    {
        lblError.Visible = false;
        lblErrorCambioClave.Visible = false;
        lblErrorClaveActual.Visible = false;
        lblErrorUsuario.Visible = false;

        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        try
        {
            /// && C.PadreExterno != null : codigo que se saco porque se encontro cliente que no cumplian con esta
            /// condición. 11/08/2010
            var cliente = (from C in dc.Clientes
                           where C.Login == UserName.Text.Trim()
                           select C).First<Cliente>();

            if (txtClaveActual.Text.Trim() == cliente.Pass.Trim())
            {
                if (txtClaveNueva.Text.Trim() == txtClaveNuevaR.Text.Trim())
                {

                    cliente.Login = txtNuevoLogin.Text.Trim();
                    cliente.Pass = txtClaveNuevaR.Text.Trim();
                    dc.SubmitChanges();
                    tblCambioClave.Visible = false;
                    tblIngreso.Visible = true;
                    btnLogin.Visible = true;
                }
                else
                {
                    lblErrorCambioClave.Visible = true;
                }
            }
            else
            {
                lblErrorClaveActual.Visible = true;
            }
        }
        catch
        {
            lblErrorUsuario.Visible = true;
            tblCambioClave.Visible = false;
            tblIngreso.Visible = true;
            btnLogin.Visible = true;

        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        tblCambioClave.Visible = false;
        tblIngreso.Visible = true;
        btnLogin.Visible = true;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object GetConsultores()
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();


        var consultores = (from d in dc.Clientes
                           select new
                           {
                               Nombre = d.Nombre,
                               DNI = d.Dni.Length > 8 ? string.Format("{0:##-########-#}", long.Parse(d.Dni)) : string.Format("{0:##,###,###}", long.Parse(d.Dni)),
                               Codigo = d.CodigoExterno,
                               TipoCliente = d.TipoCliente,
                               TipoConsultor = d.TipoConsultor,
                               Trasnporte = d.Transporte != null ? d.Transporte.ToUpper() : "&nbsp;",
                               Vendedor = d.Vendedor != null ? d.Vendedor.ToUpper() : "&nbsp;",
                           }).Take(120).ToList().OrderBy(w => w.Trasnporte);

        return consultores.ToList();
    }
}
