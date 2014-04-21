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
using Telerik.Web.UI;
using CommonMarzzan;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.Script.Services;

public partial class ConsultaPedidosInterno : BasePage
{
    private List<string> GruposValidos
    {

        set
        {
            Session["GruposValidos"] = value;
        }
        get
        {
            return (List<string>)Session["GruposValidos"];
        }

    }
    protected override void PageLoad()
    {

        if (!IsPostBack)
        {
            txtFechaInicial.SelectedDate = DateTime.Now.AddDays(-7);
            txtFechaFinal.SelectedDate = DateTime.Now;

            GruposValidos = Helper.ObtenerGruposSubordinados(Session["GrupoCliente"].ToString());
            this.cboGrupoFiltro.DataSource = GruposValidos;
            this.cboGrupoFiltro.DataBind();
            this.cboGrupoFiltro.Items.Insert(0, new RadComboBoxItem("- Todos los Grupos -"));

        }

        gridPre.ExportToExcel += new ControlsAjaxNotti.ClickEventHandler(gridPre_ExportToExcel);
    }

    void gridPre_ExportToExcel(object sender)
    {
        gridPre.ExportToExcelFunction("Ventas", (IList)Session["DatosConsultados"]);
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("Inicio.aspx");
    }

    [WebMethod(EnableSession = true)]
    public static object GetDatos(string grupo, string nroPedido, string cliente, string transporte, string fechaInicial, string fechaFinal)
    {

        /// Consulta a BEJERMAN
        Marzzan_BejermanDataContext dc = new Marzzan_BejermanDataContext();
        dc.CommandTimeout = 60;
        List<VeoComisionablesTotales> datos = new List<VeoComisionablesTotales>();
        DateTime FechaInicial = Convert.ToDateTime(fechaInicial);
        DateTime FechaFinal = Convert.ToDateTime(fechaFinal);
        List<string> gruposValidos = (List<string>)HttpContext.Current.Session["GruposValidos"];

        if (nroPedido != "")
        {
            nroPedido = string.Format("{0:00000000}", long.Parse(nroPedido));
            datos = (from c in dc.VeoComisionablesTotales
                     where c.Nro == nroPedido
                     select c).OrderBy(w => w.RazonSocial).ThenBy(w => w.FEmision).ToList();
        }
        else if (cliente != "")
        {
            if (transporte != "")
            {
                datos = (from c in dc.VeoComisionablesTotales
                         where c.FEmision >= FechaInicial.Date
                         && c.FEmision <= FechaFinal.Date
                         && c.RazonSocial == cliente
                         && c.Transporte == transporte
                         select c).OrderBy(w => w.RazonSocial).ThenBy(w => w.FEmision).ToList();
            }
            else
            {
                datos = (from c in dc.VeoComisionablesTotales
                         where c.FEmision >= FechaInicial.Date
                         && c.FEmision <= FechaFinal.Date
                         && c.RazonSocial == cliente
                         select c).OrderBy(w => w.RazonSocial).ThenBy(w => w.FEmision).ToList();
            }
        }
        else if (grupo != "")
        {
            if (transporte != "")
            {
                datos = (from c in dc.VeoComisionablesTotales
                         where c.FEmision >= FechaInicial.Date
                         && c.FEmision <= FechaFinal.Date
                         && c.Grupo == grupo
                         && c.Transporte == transporte
                         select c).OrderBy(w => w.RazonSocial).ThenBy(w => w.FEmision).ToList();
            }
            else
            {
                datos = (from c in dc.VeoComisionablesTotales
                         where c.FEmision >= FechaInicial.Date
                         && c.FEmision <= FechaFinal.Date
                         && c.Grupo == grupo
                         select c).OrderBy(w => w.RazonSocial).ThenBy(w => w.FEmision).ToList();
            }
        }
        else
        {
            if (transporte != "")
            {
                datos = (from c in dc.VeoComisionablesTotales
                         where c.FEmision >= FechaInicial.Date
                         && c.FEmision <= FechaFinal.Date
                         && gruposValidos.Contains(c.Grupo)
                         && c.Transporte == transporte
                         select c).OrderBy(w => w.RazonSocial).ThenBy(w => w.FEmision).ToList();
            }
            else
            {
                datos = (from c in dc.VeoComisionablesTotales
                         where c.FEmision >= FechaInicial.Date
                         && c.FEmision <= FechaFinal.Date
                         && gruposValidos.Contains(c.Grupo)
                         select c).OrderBy(w => w.RazonSocial).ThenBy(w => w.FEmision).ToList();
            }
        }

        var datosConsultados = (from d in datos
                                select new
                                {
                                    ID = d.ID,
                                    Importe = Math.Round(Convert.ToDecimal(d.Importe), 2),
                                    RazonSocial = d.RazonSocial,
                                    Nro = d.Nro,
                                    CodCliente = d.CodCliente,
                                    Grupo = d.Grupo,
                                    Transporte = d.Transporte,
                                    FEmision = d.FEmision,
                                    TipoComp = d.TipoComp

                                }).ToList();

        HttpContext.Current.Session.Add("DatosConsultados", datosConsultados);


        return datosConsultados;

    }
}
