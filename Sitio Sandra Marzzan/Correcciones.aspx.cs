using System;
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
using System.Collections;
using CommonMarzzan;

public partial class Correcciones : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Marzzan_InfolegacyDataContext dbLocal = new Marzzan_InfolegacyDataContext();

        var Pedidos = (from u in dbLocal.CabeceraPedidos
                       where u.IdCabeceraPedido >= 138904 && u.IdCabeceraPedido <= 138998 
                       select u).ToList();


        foreach (CabeceraPedido ped in Pedidos)
        {
            foreach (DetallePedido item in ped.DetallePedidos)
            {
                long idPre = item.objPresentacion.IdPresentacion;
                decimal precioActual = (from u in dbLocal.Presentacions
                                        where u.IdPresentacion == idPre
                                        select u.Precio.Value).FirstOrDefault();

                item.ValorUnitario = item.ValorUnitario < 0 ? -1 * precioActual : precioActual;
                item.ValorTotal = item.Cantidad * (item.ValorUnitario < 0 ? -1 * precioActual : precioActual);
            }

            decimal TotalPedido = ped.DetallePedidos.Sum(w => w.ValorTotal).Value;
            ped.MontoTotal = TotalPedido;
        }

        dbLocal.SubmitChanges();
    }
}
