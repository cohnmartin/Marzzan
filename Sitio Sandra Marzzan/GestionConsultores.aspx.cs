using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using CommonMarzzan;
using Telerik.Web.UI;

public partial class GestionConsultores : System.Web.UI.Page
{
    private Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
    private static CommonMarzzan.Parametro confMail = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            var cliente = (from C in dc.Clientes
                           where C.IdCliente == long.Parse(Session["IdUsuario"].ToString())
                           select C).Single<Cliente>();


            List<Cliente> consultores = Helper.ObtenerConsultoresSubordinados((Cliente)cliente);


            if (cliente.TipoCliente == TipoClientes.Consultor.ToString())
            {
                cboConsultores.DataTextField = "Nombre";
                cboConsultores.DataValueField = "CodigoExterno";
                cboConsultores.DataSource = consultores;
                cboConsultores.DataBind();
                cboConsultores.SelectedIndex = 0;
                cboConsultores.Enabled = false;
                cboConsultores_SelectedIndexChanged(this.cboConsultores, null);
                this.txtNombre.Focus();
            }
            else
            {
                cboConsultores.AppendDataBoundItems = true;
                cboConsultores.Items.Add(new RadComboBoxItem("Seleccione un Consultor", "0"));
                cboConsultores.DataTextField = "Nombre";
                cboConsultores.DataValueField = "CodigoExterno";
                cboConsultores.DataSource = consultores;
                cboConsultores.DataBind();
                cboConsultores.SelectedIndex = 0;
            }


            confMail = (from C in dc.Parametros
                        where C.Tipo == "ConfMail"
                        select C).First<Parametro>();
        }

    }
    protected void cboConsultores_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (cboConsultores.SelectedValue != "0")
        {

            var cliente = (from C in dc.Clientes
                           where C.CodigoExterno == cboConsultores.SelectedValue
                           select C).FirstOrDefault<Cliente>();


            var direccionesEntrega = from D in dc.Direcciones
                                     where D.CodigoExterno == cliente.CodigoExterno
                                     select D;

            gvDirEntrega.DataSource = direccionesEntrega;
            gvDirEntrega.DataBind();


            txtDni.Text = cliente.Dni;
            txtNombre.Text = cliente.Nombre;
            txtEmail.Text = cliente.Email;
            txtTelFijo.Text = cliente.Telefono;
            txtTelCel.Text = cliente.TelefonoCel;
            txtFechaNacimiento.SelectedDate = cliente.FechaNacimiento;

            upDirecciones.Update();
            upDatos.Update();


        }

    }
    protected void btnModificacionDatos_Click(object sender, EventArgs e)
    {
        string Subject = "";
        string body = "";

        Cliente ClienteLoging = (from C in dc.Clientes
                                 where C.IdCliente == long.Parse(Session["IdUsuario"].ToString())
                                 select C).FirstOrDefault();

        long IdClienteCombo = (from C in dc.Clientes
                               where C.CodigoExterno == cboConsultores.SelectedValue
                               select C.IdCliente).FirstOrDefault();



        if (ClienteLoging.TipoCliente != TipoClientes.Consultor.ToString())
        {

            body = GetHTMLModificacionClientes(Session["NombreUsuario"].ToString(), cboConsultores.SelectedItem.Text,
                txtNombre.Text, txtDni.Text, txtEmail.Text, txtFechaNacimiento.SelectedDate.Value.ToShortDateString(),
                txtTelFijo.Text, txtTelCel.Text);

            Subject = "Solicitud Modificacion Datos Personales";

            string DireccionDestino = GetDireccionDestino();


            if (DireccionDestino != "")
            {

                Helper.EnvioMailSolicitudAlta(body, DireccionDestino, confMail.ColHijos[0].Valor, Subject, confMail.ColHijos[3].Valor, confMail.ColHijos[1].Valor, confMail.ColHijos[2].Valor);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "CerrarCarga", "ShowFailSend();", true);
                return;
            }
        }
        else
        {

            this.CrearAuditoria(long.Parse(Session["IdUsuario"].ToString()), IdClienteCombo
                      , txtNombre.Text, txtDni.Text, txtEmail.Text, txtFechaNacimiento.SelectedDate.Value, txtTelFijo.Text, txtTelCel.Text);

        }

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CerrssdcarCarga", "confirmarSolicitud();", true);

        txtDni.Text = "";
        txtNombre.Text = "";
        txtEmail.Text = "";
        txtTelFijo.Text = "";
        cboConsultores.ClearSelection();
        upCbo.Update();
        upDatos.Update();
        upDirecciones.Update();

    }

    private void CrearAuditoria(long usuario, long cliente, string nombre, string dni
        , string email, DateTime fechanacimiento, string telefono, string telefonocel)
    {
        try
        {


            AudiSolModCliente audiCli = new AudiSolModCliente();
            audiCli.IdCliente = cliente;
            audiCli.IdUsuario = usuario;
            audiCli.Fecha = DateTime.Now;

            audiCli.Nombre = nombre;
            audiCli.Dni = dni;
            audiCli.FechaNacimiento = fechanacimiento;
            audiCli.Email = email;
            audiCli.Telefono = telefono;
            audiCli.TelefonoCel = telefonocel;

            dc.AudiSolModClientes.InsertOnSubmit(audiCli);
            dc.SubmitChanges();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private string GetHTMLModificacionClientes(string coordinador, string consultor, string nombre,
        string dni, string email, string fechanacimiento, string telefono, string telefonocel)
    {

        Cliente cli = (from C in dc.Clientes
                       where C.CodigoExterno == cboConsultores.SelectedValue
                       select C).First<Cliente>();



        string tbl = "<table>";
        tbl += "<tr>";
        tbl += "    <td>";
        tbl += "Personal de Asistencia:";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td>";
        tbl += "El Líder <span style='font-weight:bold' id='sSolitante' runat='server'>" + coordinador + "</span>";
        tbl += " ha solicitado que se modifiquen los datos personales del revendedor ";
        tbl += "<span style='font-weight:bold' id='Span1' runat='server'>" + consultor + "</span> con la siguiente información:";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "   <td>";
        tbl += "        &nbsp;";
        tbl += "   </td>";
        tbl += "</tr>";

        /// DATOS ORIGINALES
        tbl += "<tr>";
        tbl += "    <td style='padding-left: 45px'>";
        tbl += "        <span style='font-weight:bold' id='Span2' runat='server'>Datos Orignales</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "    <td style='padding-left: 45px'>";
        tbl += "        Apellido y Nombre:<span style='font-weight:bold' id='Span2' runat='server'>" + cli.Nombre + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "DNI:<span style='font-weight:bold' id='Span3' runat='server'>" + cli.Dni + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";

        string fechaN = "";
        if (cli.FechaNacimiento.HasValue)
            fechaN = cli.FechaNacimiento.Value.ToShortDateString();


        tbl += "Fecha Nacimiento:<span style='font-weight:bold' id='Span4' runat='server'>" + fechaN + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "Email:<span style='font-weight:bold' id='Span5' runat='server'>" + cli.Email + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "Teléfono:<span style='font-weight:bold' id='Span5' runat='server'>" + cli.Telefono + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "Teléfono Cel.:<span style='font-weight:bold' id='Span5' runat='server'>" + cli.TelefonoCel + "</span>";
        tbl += "</td>";
        tbl += "</tr>";



        /// DATOS NUEVOS
        tbl += "<tr>";
        tbl += "    <td style='padding-left: 45px'>";
        tbl += "        <span style='font-weight:bold' id='Span2' runat='server'>Nuevos Datos</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "    <td style='padding-left: 45px'>";
        tbl += "        Apellido y Nombre:<span style='font-weight:bold' id='Span2' runat='server'>" + nombre + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "DNI:<span style='font-weight:bold' id='Span3' runat='server'>" + dni + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "Fecha Nacimiento:<span style='font-weight:bold' id='Span4' runat='server'>" + fechanacimiento + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "Email:<span style='font-weight:bold' id='Span5' runat='server'>" + email + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "Teléfono:<span style='font-weight:bold' id='Span5' runat='server'>" + telefono + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "Teléfono Cel.:<span style='font-weight:bold' id='Span5' runat='server'>" + telefonocel + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "</table>";

        return tbl;
    }

    private string GetDireccionDestino()
    {

        ConfMail dir = null;

        try
        {
            Cliente cliente = (from C in dc.Clientes
                               where C.IdCliente == long.Parse(Session["IdUsuario"].ToString())
                               select C).Single<Cliente>();

            if (cliente.TipoCliente == TipoClientes.Consultor.ToString())
            {
                ///si es consultor consulto por esto
                string CodClasif1 = cliente.CodClasif1;
                string CodTipoCliente = ((long)TipoClientes.Consultor).ToString();
                Cliente consultor = (from C in dc.Clientes
                                     where C.CodClasif1 == CodClasif1 && C.CodTipoCliente != CodTipoCliente
                                     select C).FirstOrDefault<Cliente>();

                if (consultor != null && consultor.ConfMails.Count() > 0)
                {
                    dir = consultor.ConfMails.First();
                }
                else
                {
                    return "";
                }
            }
            else if (cliente.ConfMails.Count() > 0)
            {
                dir = cliente.ConfMails.First();
            }
            else
            {
                return "";
            }
        }
        catch
        {
            return "";
        }

        return dir.EmailDestino;


    }


}
