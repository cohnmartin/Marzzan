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
using System.IO;
using CommonMarzzan;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.Script.Services;
using System.IO.Compression;

public partial class ValidarPSP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var nroComercio = Request.Form["NROCOMERCIO"];
        var nroOperacion = Request.Form["NROOPERACION"];

        var medioPago = Request.Form["MEDIODEPAGO"];
        var monto = Request.Form["MONTO"];
        var cuotas = Request.Form["CUOTAS"];
        var url = Request.Form["URLDINAMICA"];

        using (Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext() )
        {
            CabeceraPedido cab = (from c in dc.CabeceraPedidos
                                 where c.IdCabeceraPedido == long.Parse(nroOperacion)
                                 select c).FirstOrDefault();


            cab.TarjetaAprobada = true;
            cab.FechaAprobacion = DateTime.Now;
            cab.CodigoAutorizacion = "09545446";
            cab.EstadoOperacionTarjeta = "Aprobada";
            cab.NombreTitularTarjeta = "Juan Fernandez";
            cab.TipoDocTitularTarjeta = "DNI";
            cab.NroDocTitularTarjeta = "26055345";
            cab.UltimaModificacion = DateTime.Now;
            cab.EsTemporal = false;
            cab.HuboFaltaSaldo = false;

            dc.SubmitChanges();
            
        }

        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Impresion", "ShowComprobante('" + nroOperacion + "');", true);
    }
}
