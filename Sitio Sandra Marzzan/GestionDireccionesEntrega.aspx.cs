using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonMarzzan;

public partial class GestionDireccionesEntrega : System.Web.UI.Page
{
    Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
    private static CommonMarzzan.Parametro confMail = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["DivisionesPoliticas"] == null)
            {
                Session["DivisionesPoliticas"] = (from d in dc.DivisionesPoliticas
                                                  select d).ToList();
            }

            if (Request.QueryString["SolicitudAlta"] == null)
            {



                confMail = (from C in dc.Parametros
                            where C.Tipo == "ConfMail"
                            select C).First<Parametro>();

                if (Request.QueryString["IdDireccion"] != null)
                {
                    Direccione dir = (from D in dc.Direcciones
                                      where D.IdDireccion == long.Parse(Request.QueryString["IdDireccion"].ToString())
                                      select D).First<Direccione>();

                    cboProvincias.Text = dir.Provincia.ToUpper();
                    txtDepartamento.Text = dir.Localidad.ToUpper();
                    txtCalle.Text = dir.Calle;
                    txtCodigoPostal.Text = dir.CodigoPostal;
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "CerrarInicial", "InitCombosEdicion();", true);
                    
                }
            }
            else
            {

                cboProvincias.Text = "";
                txtDepartamento.Text = "";
                txtNuevo_Depart_loc.Text = "";
                txtCalle.Text = "";
                txtCodigoPostal.Text = "";
                btnEnviarSolicitud.Text = "Agregar";
            }

        }
    }
    protected void btnEnviarSolicitud_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["SolicitudAlta"] == null)
        {
            string body = "";
            string Subject = "";

            Cliente ClienteLoging = (from C in dc.Clientes
                                     where C.IdCliente == long.Parse(Session["IdUsuario"].ToString())
                                     select C).FirstOrDefault();

            long IdClienteCombo = (from C in dc.Clientes
                                   where C.CodigoExterno == Request.QueryString["IdConsultor"]
                                   select C.IdCliente).FirstOrDefault();


            if (ClienteLoging.TipoCliente != TipoClientes.Consultor.ToString())
            {
                if (Request.QueryString["IdDireccion"] == null)
                {
                    body = GetHTMLNuevoDireccion(Session["NombreUsuario"].ToString(), Request.QueryString["Consultor"].ToString(), cboProvincias.Text, txtDepartamento.Text, txtNuevo_Depart_loc.Text, txtCalle.Text, txtCodigoPostal.Text);
                    Subject = "Solicitud Nueva Dirección Entrega";

                }
                else
                {
                    body = GetHTMLModificacionDireccion(Session["NombreUsuario"].ToString(), Request.QueryString["Consultor"].ToString(), cboProvincias.Text, txtDepartamento.Text, txtNuevo_Depart_loc.Text, txtCalle.Text, txtCodigoPostal.Text);
                    Subject = "Solicitud Modificacion Dirección Entrega";
                }

                string DireccionDestino = GetDireccionDestino();

                if (DireccionDestino != "")
                {

                    Helper.EnvioMailSolicitudAlta(body, DireccionDestino, confMail.ColHijos[0].Valor, Subject, confMail.ColHijos[3].Valor, confMail.ColHijos[1].Valor, confMail.ColHijos[2].Valor);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "CerrarCarga", "ShowFailSend();", true);
                }
            }
            else
            {
                CrearAuditoria(long.Parse(Session["IdUsuario"].ToString()), IdClienteCombo
                                , cboProvincias.Text, txtDepartamento.Text, txtNuevo_Depart_loc.Text, txtCalle.Text, txtCodigoPostal.Text);
            }

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "CerrarCarga", "returnToParent();", true);

            cboProvincias.Text = "";
            txtDepartamento.Text = "";
            txtNuevo_Depart_loc.Text = "";
            txtCalle.Text = "";
            txtCodigoPostal.Text = "";
        }
        else
        {
            Direccione Direccion = new Direccione();
            Direccion.Provincia = cboProvincias.Text;
            Direccion.Departamento = txtDepartamento.Text;
            Direccion.Localidad = txtNuevo_Depart_loc.Text;
            Direccion.Calle = txtCalle.Text;
            Direccion.CodigoPostal = txtCodigoPostal.Text;

            (Session["DireccionesxSolicitud"] as List<Direccione>).Add(Direccion);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "CerrarCarga", "returnToParent();", true);

            //cboProvincias.Text = "";
            //txtDepartamento.Text = "";
            //txtNuevo_Depart_loc.Text = "";
            //txtCalle.Text = "";
            //txtCodigoPostal.Text = "";

        }

    }

    private void CrearAuditoria(long usuario, long cliente, string Provincia, string Departamento
    , string Localidad, string Calle, string CodigoPostal)
    {
        try
        {

            AudiSolModCliente audiCli = new AudiSolModCliente();
            audiCli.IdCliente = cliente;
            audiCli.IdUsuario = usuario;
            audiCli.Fecha = DateTime.Now;

            audiCli.Provincia = Provincia;
            audiCli.Departamento = Departamento;
            audiCli.Localidad = Localidad;
            audiCli.Calle = Calle;
            audiCli.CodigoPostal = CodigoPostal;

            dc.AudiSolModClientes.InsertOnSubmit(audiCli);
            dc.SubmitChanges();

        }
        catch (Exception ex)
        {
            throw ex;
        }
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
    private string GetHTMLNuevoDireccion(string coordinador, string consultor, string provincia, string departamento, string localidad, string direccion, string codigopostal)
    {


        string tbl = "<table>";
        tbl += "<tr>";
        tbl += "    <td>";
        tbl += "Personal de Asistencia:";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td>";
        tbl += "El Líder <span style='font-weight:bold' id='sSolitante' runat='server'>" + coordinador + "</span>";
        tbl += " ha solicitado que se agregue una nueva dirección de entrega al revendedor ";
        tbl += "<span style='font-weight:bold' id='Span1' runat='server'>" + consultor + "</span> con los siguientes datos:";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "   <td>";
        tbl += "        &nbsp;";
        tbl += "   </td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "    <td style='padding-left: 45px'>";
        tbl += "        Provincia:<span style='font-weight:bold' id='Span2' runat='server'>" + provincia + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "Dep/Loc:<span style='font-weight:bold' id='Span3' runat='server'>" + departamento+ "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "Nuevo Dep/Loc:<span style='font-weight:bold' id='Span4' runat='server'>" + localidad + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "Dirección:<span style='font-weight:bold' id='Span5' runat='server'>" + direccion + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "Código Postal:<span style='font-weight:bold' id='Span5' runat='server'>" + codigopostal + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "</table>";

        return tbl;
    }
    private string GetHTMLModificacionDireccion(string coordinador, string consultor, string provincia, string departamento, string localidad, string direccion, string codigopostal)
    {

        Direccione dir = (from D in dc.Direcciones
                          where D.IdDireccion == long.Parse(Request.QueryString["IdDireccion"].ToString())
                          select D).First<Direccione>();



        string tbl = "<table>";
        tbl += "<tr>";
        tbl += "    <td>";
        tbl += "Personal de Asistencia:";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td>";
        tbl += "El Líder <span style='font-weight:bold' id='sSolitante' runat='server'>" + coordinador + "</span>";
        tbl += " ha solicitado que se modifique la dirección de entrega del revendedor ";
        tbl += "<span style='font-weight:bold' id='Span1' runat='server'>" + consultor + "</span> con los siguientes datos:";
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
        tbl += "        Provincia:<span style='font-weight:bold' id='Span2' runat='server'>" + dir.Provincia + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "Departamento:<span style='font-weight:bold' id='Span3' runat='server'>" + dir.Localidad + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "Localidad:<span style='font-weight:bold' id='Span4' runat='server'>" + dir.Departamento + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "Dirección:<span style='font-weight:bold' id='Span5' runat='server'>" + dir.Calle + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "Código Postal:<span style='font-weight:bold' id='Span5' runat='server'>" + dir.CodigoPostal + "</span>";
        tbl += "</td>";
        tbl += "</tr>";





        /// DATOS NUEVOS
        tbl += "<tr>";
        tbl += "    <td style='padding-left: 45px'>";
        tbl += "        <span style='font-weight:bold' id='Span2' runat='server'>Nuevos Datos</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "    <td style='padding-left: 45px'>";
        tbl += "        Provincia:<span style='font-weight:bold' id='Span2' runat='server'>" + provincia + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "Dep/loc:<span style='font-weight:bold' id='Span3' runat='server'>" + departamento + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "Nuevo Dep/Loc:<span style='font-weight:bold' id='Span4' runat='server'>" + localidad + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "Dirección:<span style='font-weight:bold' id='Span5' runat='server'>" + direccion + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "<tr>";
        tbl += "<td style='padding-left: 45px'>";
        tbl += "Código Postal:<span style='font-weight:bold' id='Span5' runat='server'>" + codigopostal + "</span>";
        tbl += "</td>";
        tbl += "</tr>";
        tbl += "</table>";

        return tbl;
    }


}
