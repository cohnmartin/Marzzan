using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CommonMarzzan;

public partial class BusquedaLider : System.Web.UI.Page
{
    private const int ItemsPerRequest = 10;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            lblTitulo.Text += Request.QueryString["Prov"].ToString();

    }
    protected void cboCoord_ItemsRequested(object o, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        var data = from C in dc.Clientes
                   where C.IdCliente == 0
                   select new { C.IdCliente, C.Nombre };

        if (e.Text.Length == 0)
        {
            data = (from C in dc.Clientes
                    from D in C.ColDirecciones
                    where (D.EsPrincipal.Value && D.Provincia.StartsWith(Request.QueryString["Prov"]))
                       orderby C.Nombre
                       select new { C.IdCliente, C.Nombre }).Take(15);
        }
        else
        {
             data = from C in dc.Clientes
                    from D in C.ColDirecciones
                       where C.Nombre.StartsWith(e.Text)
                       && (D.EsPrincipal.Value && D.Provincia.StartsWith(Request.QueryString["Prov"]))
                       orderby C.Nombre
                       select new { C.IdCliente, C.Nombre };
        }

        int itemOffset = e.NumberOfItems;
        int endOffset = Math.Min(itemOffset + ItemsPerRequest, data.Count());
        e.EndOfItems = endOffset == data.Count();

        //for (int i = itemOffset; i < endOffset; i++)
        //{
        //    this.cboCoord.Items.Add(new RadComboBoxItem(data.ToList()[i].Nombre, data.ToList()[i].IdCliente.ToString()));
        //}

        cboCoord.DataValueField = "IdCliente";
        cboCoord.DataTextField = "Nombre";
        cboCoord.DataSource = data;
        cboCoord.DataBind();


        e.Message = GetStatusMessage(endOffset, data.Count());
    }

    private static string GetStatusMessage(int offset, int total)
    {
        if (total <= 0)
            return "No existen coincidencias";

        return String.Format("Registros: <b>1</b>-<b>{0}</b> de <b>{1}</b>", offset, total);
    }

    protected void btnAsignar_Click(object sender, EventArgs e)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
        long id = long.Parse(Request.QueryString["IdUsuario"].ToString());

        UsuariosAltaTemprana usuarioAlta = (from U in dc.UsuariosAltaTempranas
                          where U.IdUsurioAltaTemprana == id
                           select U).First();


        usuarioAlta.LiderDerivado = long.Parse(cboCoord.SelectedValue);
        usuarioAlta.FechaDerivacion = DateTime.Now;
        dc.SubmitChanges();

        ScriptManager.RegisterStartupScript(this, typeof(Page), "volver", "returnToParent(" + cboCoord.SelectedValue + ");", true);
    }
}
