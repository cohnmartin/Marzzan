using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PaginaParaPruebas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnMail_Click(object sender, EventArgs e)
    {
        try
        {
            string mailDestino = TextBox1.Text.Trim();
            string MailOrigen = TextBox2.Text.Trim();
            string sSujeto = TextBox3.Text.Trim();
            string body = TextBox4.Text.Trim();
            string SMTPMailOrigen = TextBox5.Text.Trim();
            string UsuarioMailOrigen = TextBox6.Text.Trim();
            string ClaveMailOrigen = TextBox7.Text.Trim();

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add(new System.Net.Mail.MailAddress(mailDestino));
            msg.From = new System.Net.Mail.MailAddress(MailOrigen);
            //msg.CC.Add(new System.Net.Mail.MailAddress(MailOrigen));
            msg.Subject = sSujeto;
            msg.Body = body;
            msg.IsBodyHtml = true;
            System.Net.Mail.SmtpClient clienteSmtp = new System.Net.Mail.SmtpClient(SMTPMailOrigen, 25);
            clienteSmtp.Credentials = new System.Net.NetworkCredential(UsuarioMailOrigen, ClaveMailOrigen);
            clienteSmtp.EnableSsl = CheckBox1.Checked;


            clienteSmtp.Send(msg);
            lblEstado.Text = "Envio OK";
        }
        catch (Exception er)
        {
            lblEstado.Text = er.Message + er.InnerException.Message + er.InnerException.StackTrace;
        }
    }
    protected void btnMail0_Click(object sender, EventArgs e)
    {
        try
        {
            string mailDestino = TextBox8.Text.Trim();
            string MailOrigen = TextBox9.Text.Trim();
            string sSujeto = TextBox10.Text.Trim();
            string body = TextBox11.Text.Trim();
            string SMTPMailOrigen = TextBox12.Text.Trim();
            string UsuarioMailOrigen = TextBox13.Text.Trim();
            string ClaveMailOrigen = TextBox14.Text.Trim();

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add(new System.Net.Mail.MailAddress(mailDestino));
            msg.From = new System.Net.Mail.MailAddress(MailOrigen);
            msg.CC.Add(new System.Net.Mail.MailAddress(MailOrigen));
            msg.Subject = sSujeto;
            msg.Body = body;
            msg.IsBodyHtml = true;
            System.Net.Mail.SmtpClient clienteSmtp = new System.Net.Mail.SmtpClient(SMTPMailOrigen,25);
            clienteSmtp.Credentials = new System.Net.NetworkCredential(UsuarioMailOrigen, ClaveMailOrigen);
            clienteSmtp.EnableSsl = CheckBox2.Checked;
            //clienteSmtp.SendCompleted += new System.Net.Mail.SendCompletedEventHandler(clienteSmtp_SendCompleted);

            clienteSmtp.Send(msg);
            lblEstado0.Text = "Envio OK";
        }
        catch (Exception er)
        {
            lblEstado0.Text = er.Message + er.InnerException.Message + er.InnerException.StackTrace;
        }
    }
}
