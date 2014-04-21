using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonMarzzan;
using Telerik.Web.UI;

public partial class GestionMail : System.Web.UI.Page
{
    Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

 
            Parametro ParametroConfMail = (from P in dc.Parametros
                                           where P.Tipo == "ConfMail"
                                           select P).First<Parametro>();

            txtDireccion.Text = ParametroConfMail.ColHijos[0].Valor;
            txtUsuario.Text = ParametroConfMail.ColHijos[1].Valor;
            txtPassword.Text = ParametroConfMail.ColHijos[2].Valor;
            txtSMTP.Text = ParametroConfMail.ColHijos[3].Valor;
            

        }
    }

    protected void cboConsultores_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        RadComboBox combo = (RadComboBox)o;

        if (e.Text != "")
        {
            var clientesConCargo = (from C in dc.Clientes
                                    where (C.CodTipoCliente != TipoClientesConstantes.CONSULTOR
                                    && C.CodTipoCliente != TipoClientesConstantes.INTERNO)
                                    && Convert.ToInt32(C.CodVendedor) > 0
                                    && (C.Nombre.StartsWith(e.Text) && (!C.Nombre.Contains("BOLSOS") && !C.Nombre.Contains("STOCK")))
                                    select new
                                    {
                                        C.Nombre,
                                        C.IdCliente
                                    }).OrderBy(w => w.Nombre);


            foreach (var row in clientesConCargo)
            {
                RadComboBoxItem item = new RadComboBoxItem(row.Nombre, row.IdCliente.ToString());
                combo.Items.Add(item);
            }
        }
        else
        {
            var clientesConCargo = (from C in dc.Clientes
                                    where (C.CodTipoCliente != TipoClientesConstantes.CONSULTOR
                                    && C.CodTipoCliente != TipoClientesConstantes.INTERNO)
                                    && Convert.ToInt32(C.CodVendedor) > 0
                                    && (C.Nombre.StartsWith(e.Text) && (!C.Nombre.Contains("BOLSOS") && !C.Nombre.Contains("STOCK")))
                                    select new
                                    {
                                        C.Nombre,
                                        C.IdCliente
                                    }).OrderBy(w => w.Nombre).Take(20);


            foreach (var row in clientesConCargo)
            {
                RadComboBoxItem item = new RadComboBoxItem(row.Nombre, row.IdCliente.ToString());
                combo.Items.Add(item);
            }
        
        }
    }


    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        try
        {
            Cliente cli = (from C in dc.Clientes
                           where C.IdCliente == Convert.ToInt64(cboConsultores.SelectedValue)
                           select C).First<Cliente>();

            if (cli.ConfMails.Count == 0)
            {
                ConfMail conf = new ConfMail();
                conf.Consultor = Convert.ToInt64(cboConsultores.SelectedValue);
                conf.EmailDestino = txtMailCoordinador.Text;
                dc.ConfMails.InsertOnSubmit(conf);

            }
            else
            {
                cli.ConfMails[0].EmailDestino = txtMailCoordinador.Text;
            }
            

            dc.SubmitChanges();
            txtMailCoordinador.Text="";
            cboConsultores.Text = "";
            cboConsultores.ClearSelection();

        }
        catch
        { }
    }
    protected void cboConsultores_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        try
        {
            var cli = (from C in dc.ConfMails
                           where C.objCoordinador.IdCliente == Convert.ToInt64(cboConsultores.SelectedValue)
                           select C.EmailDestino).FirstOrDefault();

            if (cli!= null)
                txtMailCoordinador.Text = cli;
            else
                txtMailCoordinador.Text = "";
        }
        catch
        {
            txtMailCoordinador.Text = "";
        }

    }
    protected void btnGrabarGeneral_Click(object sender, EventArgs e)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        Parametro ParametroConfMail = (from P in dc.Parametros
                                       where P.Tipo == "ConfMail"
                                       select P).First<Parametro>();

        ParametroConfMail.ColHijos[0].Valor = txtDireccion.Text;
        ParametroConfMail.ColHijos[1].Valor = txtUsuario.Text;
        ParametroConfMail.ColHijos[2].Valor = txtPassword.Text;
        ParametroConfMail.ColHijos[3].Valor = txtSMTP.Text;

        dc.SubmitChanges();
    }
}
