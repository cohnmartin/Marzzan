using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonMarzzan;

public partial class SeguimientoAuditoria : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnRechazar_Click(object sender, EventArgs e)
    {
        long id = long.Parse(gvUsuarios.Items[gvUsuarios.SelectedItems[0].DataSetIndex].GetDataKeyValue("IdUsurioAltaTemprana").ToString());

        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        UsuariosAltaTemprana usuario = (from U in dc.UsuariosAltaTempranas
                                        where U.IdUsurioAltaTemprana == id
                                        select U).Single<UsuariosAltaTemprana>();

        usuario.Estado = (int)EstadoUsuarios.Rechazado;
        dc.SubmitChanges();

        gvUsuarios.MasterTableView.ClearChildSelectedItems();
        gvUsuarios.Rebind();
        upGrila.Update();

    }
    protected void btnAprobar_Click(object sender, EventArgs e)
    {
        long id = long.Parse(gvUsuarios.Items[gvUsuarios.SelectedItems[0].DataSetIndex].GetDataKeyValue("IdUsurioAltaTemprana").ToString());

        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        UsuariosAltaTemprana usuario = (from U in dc.UsuariosAltaTempranas
                                        where U.IdUsurioAltaTemprana == id
                                        select U).Single<UsuariosAltaTemprana>();

        usuario.Estado = (int)EstadoUsuarios.Aprobado;
        dc.SubmitChanges();

        gvUsuarios.MasterTableView.ClearChildSelectedItems();
        gvUsuarios.Rebind();
        upGrila.Update();
    }
    protected void chkPendiente_CheckedChanged(object sender, EventArgs e)
    {

    }
}
