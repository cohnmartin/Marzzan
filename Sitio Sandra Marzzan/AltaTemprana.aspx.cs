using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using CommonMarzzan;

public partial class AltaTemprana : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAlta_Click(object sender, EventArgs e)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        int Existe = (from C in dc.Clientes
                       where txtDni.Text.Trim() == C.Dni.ToString()
                       select C).Count();

        if (Existe > 0)
        {
            tooltipAlta.Text = "Los datos que intenta ingresar ya pertencen a un revendedor de Sandra Marzzan, por favor verifique los dato ingresados.";
            tooltipAlta.Show();
            return;
        }

         Existe = (from C in dc.UsuariosAltaTempranas
                   where txtDni.Text.Trim() == C.DNI.ToString()
                      select C).Count();


         if (Existe > 0)
         {
             tooltipAlta.Text = "Los datos que intenta ingresar ya pertencen a una solicitud pendiente para ser revendedor de Sandra Marzzan, por favor verifique los dato ingresados.";
             tooltipAlta.Show();
             return;
         }


        /// si no existe en ningún de las clases
        /// entonces doy de alta al usaurio.
        /// 

         UsuariosAltaTemprana usu = new UsuariosAltaTemprana();
         usu.Comentario = txtComentario.Text;
         usu.Departamento = txtdepartamento.Text;
         usu.Direccion = txtDireccion.Text;
         usu.DNI = txtDni.Text;
         usu.FechaAlta = DateTime.Now;
         usu.FechaNacimiento = DpFechaNacimiento.SelectedDate.Value; ;
         usu.Localidad = txtlocalidad.Text;
         usu.NombreCompleto = txtNombre.Text;
         usu.Provincia = long.Parse(cboProvincias.SelectedValue);
         usu.QuienPresento = txtPresento.Text;
         usu.TelefonoCelular = txtTelCel.Text;
         usu.Email = txtEmail.Text;
         usu.TelefonoFijo = txtTelFijo.Text;
         usu.ComoNosConocio = long.Parse(cboComo.SelectedValue);
         usu.Estado = (int)EstadoUsuarios.Pendiente;

         dc.UsuariosAltaTempranas.InsertOnSubmit(usu);
         dc.SubmitChanges();

         tooltipAlta.ShowCallout = false;
         tooltipAlta.Sticky = false;
         tooltipAlta.AutoCloseDelay = 6000;
         tooltipAlta.Text = "La solicitud de alta se ha generado correctamente, a la brevedad tomaremos contacto con usted.";
         tooltipAlta.TargetControlID = lblDepartamento.ClientID;
         tooltipAlta.Show();


         txtComentario.Text = "";
         txtdepartamento.Text = "";
         txtDireccion.Text = "";
         txtEmail.Text = "";
         txtDni.Text = "";
         DpFechaNacimiento.Clear();
         txtlocalidad.Text = "";
         txtNombre.Text = "";
         txtPresento.Text = "";
         txtTelCel.Text = "";
         txtTelFijo.Text = "";
         upDatos.Update();

    }
}
