using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonMarzzan;

public partial class GestionDerivaciones : System.Web.UI.Page
{
    private const int ItemsPerRequest = 10;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void cboCoord_ItemsRequested(object o, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        string prov = e.Context["Prov"].ToString();// HiddenProvincia.Value;// RadGrid1.Items[RadGrid1.SelectedItems[0].DataSetIndex].GetDataKeyValue("Provincia").ToString();

        var data = from C in dc.Clientes
                   where C.IdCliente == 0
                   select new { C.IdCliente, C.Nombre };

        if (e.Text.Length == 0)
        {
            data = from C in dc.Clientes
                       where C.IdCliente == 0
                       select new { C.IdCliente, C.Nombre };
           
        }
        else
        {
            data = from C in dc.Clientes
                   from D in C.ColDirecciones
                   where C.Nombre.StartsWith(e.Text)
                   && (D.EsPrincipal.Value && D.Provincia.StartsWith(prov))
                   orderby C.Nombre
                   select new { C.IdCliente, C.Nombre };
        }

        int itemOffset = e.NumberOfItems;
        int endOffset = Math.Min(itemOffset + ItemsPerRequest, data.Count());
        e.EndOfItems = endOffset == data.Count();

        cboCoord.DataValueField = "IdCliente";
        cboCoord.DataTextField = "Nombre";
        cboCoord.DataSource = data;
        cboCoord.DataBind();
        upToolTip.Update();

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

        long id = long.Parse(RadGrid1.Items[RadGrid1.SelectedItems[0].DataSetIndex].GetDataKeyValue("IdUsurioAltaTemprana").ToString());



        UsuariosAltaTemprana usuarioAlta = (from U in dc.UsuariosAltaTempranas
                                            where U.IdUsurioAltaTemprana == id
                                            select U).First();


        usuarioAlta.LiderDerivado = long.Parse(cboCoord.SelectedValue);
        usuarioAlta.FechaDerivacion = DateTime.Now;
        dc.SubmitChanges();


        ToolTipOk.Show();
        RadGrid1.Rebind();
        upGrila.Update();
        upToolTip.Update();

    }
    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("Inicio.aspx");
    }
}
