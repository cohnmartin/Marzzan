using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonMarzzan;

public partial class GestionSolicitudAltaFinal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnGuardar.OnClientClick = "CloseToolTip('" + toolTipEdicin.ClientID + "','Edicion');";
        btnGuardarNotificacion.OnClientClick = "CloseToolTip('" + toolTipNotificacion.ClientID + "','Notificacion');";
    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        long id = long.Parse(gvUsuarios.Items[gvUsuarios.SelectedItems[0].DataSetIndex].GetDataKeyValue("IdUsurioAltaTemprana").ToString());

        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        UsuariosAltaTemprana usuario = (from U in dc.UsuariosAltaTempranas
                      where U.IdUsurioAltaTemprana == id
                      select U).Single<UsuariosAltaTemprana>();



        txtdepartamento.Text = usuario.Departamento;
        txtDireccion.Text = usuario.Direccion;
        txtDni.Text = usuario.DNI;
        DpFechaNacimiento.SelectedDate = usuario.FechaNacimiento;
        txtlocalidad.Text = usuario.Localidad;
        txtNombre.Text = usuario.NombreCompleto;
        txtTelCel.Text = usuario.TelefonoCelular;
        txtTelFijo.Text = usuario.TelefonoFijo;
        txtEmail.Text = usuario.Email;
        lblProvincia.Text = usuario.objProvincia.Descripcion;

        toolTipEdicin.Show();
        upToolTip.Update();

        

        
    }
    protected void btnSolicitar_Click(object sender, EventArgs e)
    {

    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        long id = long.Parse(gvUsuarios.Items[gvUsuarios.SelectedItems[0].DataSetIndex].GetDataKeyValue("IdUsurioAltaTemprana").ToString());

        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        UsuariosAltaTemprana usu = (from U in dc.UsuariosAltaTempranas
                                        where U.IdUsurioAltaTemprana == id
                                        select U).Single<UsuariosAltaTemprana>();


        usu.Departamento = txtdepartamento.Text;
        usu.Direccion = txtDireccion.Text;
        usu.DNI = txtDni.Text;
        usu.FechaAlta = DateTime.Now;
        usu.FechaNacimiento = DpFechaNacimiento.SelectedDate.Value; ;
        usu.Localidad = txtlocalidad.Text;
        usu.NombreCompleto = txtNombre.Text;
        usu.TelefonoCelular = txtTelCel.Text;
        usu.TelefonoFijo = txtTelFijo.Text;
        usu.Email = txtEmail.Text;

        dc.SubmitChanges();

        gvUsuarios.MasterTableView.ClearChildSelectedItems();
        gvUsuarios.Rebind();
        upGrila.Update();

    }
    protected void btnGuardarNotificacion_Click(object sender, EventArgs e)
    {
        long id = long.Parse(gvUsuarios.Items[gvUsuarios.SelectedItems[0].DataSetIndex].GetDataKeyValue("IdUsurioAltaTemprana").ToString());

        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        UsuariosAltaTemprana usuario = (from U in dc.UsuariosAltaTempranas
                                        where U.IdUsurioAltaTemprana == id
                                        select U).Single<UsuariosAltaTemprana>();


        usuario.ComentarioContacto = txtComentirioNotificacion.Text;
        usuario.FechaContacto = DpFechaContacto.SelectedDate;
        dc.SubmitChanges();

        txtComentirioNotificacion.Text ="";
        DpFechaContacto.Clear();
        upNotificacion.Update();

        gvUsuarios.MasterTableView.ClearChildSelectedItems();
        gvUsuarios.Rebind();
        upGrila.Update();
    }
    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("Inicio.aspx");
    }
}
