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
using System.Data.OleDb;
using System.IO;

public partial class GestionActualizacionPedidos : System.Web.UI.Page
{

    public class TempActualizacion
    {
        public CabeceraPedido cab { get; set; }
        public DetallePedido Detalle { get; set; }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object EjecutarProcesoActualizacion(string cabs, string FI, string FF)
    {
        DateTime FechaInicial = DateTime.Parse(FI);
        DateTime FechaFinal = DateTime.Parse(FF);

        using (Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext())
        {
            List<long> ids = cabs.Split(',').Select(h => long.Parse(h)).ToList();
            int contador = 0;
            List<long> idsParcial = ids.Skip(contador).Take(1).ToList();

            while (idsParcial.Count > 0)
            {

                var pedidos = (from d in dc.DetallePedidos
                               where d.objCabecera.FechaPedido >= FechaInicial && d.objCabecera.FechaPedido <= FechaFinal
                               && idsParcial.Contains(d.objCabecera.IdCabeceraPedido)
                               && d.objCabecera.TipoPedido == "NP"
                               select new TempActualizacion
                               {
                                   cab = d.objCabecera,
                                   Detalle = d
                               }).ToList();


                ActualizarValoresArticulos(dc, pedidos);
                contador += 50;
                idsParcial = ids.Skip(contador).Take(50).ToList();
                //ActualizarPromoOkm(dc, pedidos);
            }

            return BuscarPedidos(FI, FF);
        }

    }

    public static void ActualizarValoresArticulos(Marzzan_InfolegacyDataContext dc, List<TempActualizacion> datos)
    {
        var idspre = datos.Select(w => w.Detalle.Presentacion.Value).ToList();

        var presentacionAct = (from p in dc.Presentacions
                               where idspre.Contains(p.IdPresentacion)
                               select new
                               {
                                   idPre = p.IdPresentacion,
                                   valor = p.Precio
                               }).ToList();


        var datosAgrupados = from d in datos
                             group d by d.cab into g
                             select new
                             {
                                 cab = g.Key,
                                 detalles = g
                             };
        StreamWriter _sw = null;
        _sw = new StreamWriter(HttpContext.Current.Server.MapPath("") + "\\logActualizacionArticulos_" + string.Format("{0:dd_MM_yyyy_hh_mm}", DateTime.Now) + ".txt", false);

        foreach (var pedido in datosAgrupados)
        {

            foreach (var itemP in pedido.detalles)
            {

                var currentPreAct = presentacionAct.Where(w => w.idPre == itemP.Detalle.Presentacion.Value).FirstOrDefault();
                if (currentPreAct != null)
                {
                    _sw.WriteLine("IdCabecera:" + pedido.cab.IdCabeceraPedido.ToString() + " IdDetalle:" + itemP.Detalle.IdDetallePedido.ToString() + " ValorOriginal:" + itemP.Detalle.ValorUnitario.ToString() + " ValorNuevo:" + (itemP.Detalle.ValorUnitario >= 0 ? currentPreAct.valor.ToString() : (-1 * itemP.Detalle.ValorUnitario).ToString()));
                    itemP.Detalle.ValorUnitario = itemP.Detalle.ValorUnitario >= 0 ? currentPreAct.valor : -1 * currentPreAct.valor;
                    itemP.Detalle.ValorTotal = itemP.Detalle.Cantidad * itemP.Detalle.ValorUnitario;
                }
            }

            pedido.cab.MontoTotal = pedido.detalles.Sum(w => w.Detalle.ValorTotal.Value) - pedido.cab.DescuentoProvincia.Value;

        }

        dc.SubmitChanges();

        _sw.Flush();
        _sw.Close();
    }

    public static void ActualizarPromoOkm(Marzzan_InfolegacyDataContext dc, List<TempActualizacion> datos)
    {
        var datosAgrupados = from d in datos
                             group d by d.cab into g
                             select new
                             {
                                 cab = g.Key,
                                 detalles = g
                             };


        Presentacion preCuponOK = (from P in dc.Presentacions
                                   where P.Codigo == "2520000111922"
                                   select P).FirstOrDefault();

        #region Carga Productos con Cupones

        /// Regla de Negocio:
        /// 1. Si el producto se encuentra dentro de la lista de productos con regalo, entonces
        /// entrego un cupon OK segun la cantidad establecida.
        Dictionary<string, Int32> ProductosConRegalos = new Dictionary<string, int>();
        ProductosConRegalos.Add("1150000021784", 2);
        ProductosConRegalos.Add("1150000021783", 2);
        ProductosConRegalos.Add("1150000021785", 1);
        ProductosConRegalos.Add("1150000021786", 1);
        ProductosConRegalos.Add("1150000021787", 1);
        ProductosConRegalos.Add("1150000021788", 1);
        ProductosConRegalos.Add("1150000021789", 1);
        ProductosConRegalos.Add("1150000021790", 1);
        ProductosConRegalos.Add("1150000021791", 1);
        ProductosConRegalos.Add("1150000021835", 1);
        ProductosConRegalos.Add("1150000021836", 1);
        ProductosConRegalos.Add("1150000021837", 1);
        ProductosConRegalos.Add("1150000021810", 1);
        ProductosConRegalos.Add("1150000021811", 1);
        ProductosConRegalos.Add("1150000021812", 1);
        ProductosConRegalos.Add("1151600021001", 2);
        ProductosConRegalos.Add("1151600021002", 2);
        ProductosConRegalos.Add("1150000021553", 1);
        ProductosConRegalos.Add("1150000021588", 2);
        ProductosConRegalos.Add("1150000021832", 2);
        ProductosConRegalos.Add("1150000021830", 1);
        ProductosConRegalos.Add("1150000021831", 1);
        ProductosConRegalos.Add("1150000021833", 1);
        ProductosConRegalos.Add("1150000021834", 1);
        ProductosConRegalos.Add("1156500041455", 2);
        ProductosConRegalos.Add("1156500041456", 4);
        ProductosConRegalos.Add("1156500041457", 6);
        ProductosConRegalos.Add("1156500041458", 10);
        ProductosConRegalos.Add("1156500041459", 14);
        ProductosConRegalos.Add("1156500041460", 18);
        ProductosConRegalos.Add("1156500041461", 2);
        ProductosConRegalos.Add("1156500041462", 4);
        ProductosConRegalos.Add("1156500041463", 6);
        ProductosConRegalos.Add("1156500041464", 10);
        ProductosConRegalos.Add("1156500041465", 14);
        ProductosConRegalos.Add("1156500041466", 18);
        ProductosConRegalos.Add("1150000045602", 3);
        ProductosConRegalos.Add("1150000045603", 3);
        ProductosConRegalos.Add("1150000045604", 3);
        ProductosConRegalos.Add("1043100020   -024-00", 3);
        ProductosConRegalos.Add("1043300020   -026-00", 3);
        ProductosConRegalos.Add("1150000021149", 2);
        ProductosConRegalos.Add("1150000021083", 2);
        ProductosConRegalos.Add("1150000021150", 1);
        ProductosConRegalos.Add("1150000021084", 1);
        ProductosConRegalos.Add("1150000021354", 5);
        ProductosConRegalos.Add("2150000021075", 5);
        ProductosConRegalos.Add("1151015021001", 2);
        ProductosConRegalos.Add("1151111021001", 2);
        ProductosConRegalos.Add("1151212021001", 2);
        ProductosConRegalos.Add("1151313021001", 2);
        ProductosConRegalos.Add("1151414021001", 2);
        ProductosConRegalos.Add("1086100002   -129-10", 1);
        ProductosConRegalos.Add("1080000002   -123-15", 1);
        ProductosConRegalos.Add("1080000002   -124-10", 1);
        ProductosConRegalos.Add("1080000002   -125-10", 1);
        ProductosConRegalos.Add("1080000002   -127-15", 1);
        ProductosConRegalos.Add("1080000002   -132-23", 1);
        ProductosConRegalos.Add("1086100002   -204-23", 1);
        ProductosConRegalos.Add("1010300001   -232-50", 2);
        ProductosConRegalos.Add("1138700180007-000-00", 3);
        ProductosConRegalos.Add("1138700180008-000-00", 3);
        ProductosConRegalos.Add("1138700180009-000-00", 3);
        ProductosConRegalos.Add("1138700180100-000-00", 3);
        ProductosConRegalos.Add("1138700180101-000-00", 3);
        ProductosConRegalos.Add("1138700180102-000-00", 3);
        ProductosConRegalos.Add("1138900181001-000-00", 2);
        ProductosConRegalos.Add("1138900181002-000-00", 2);
        ProductosConRegalos.Add("1138900181003-000-00", 2);
        ProductosConRegalos.Add("1138900181004-000-00", 2);
        ProductosConRegalos.Add("1138900181005-000-00", 2);
        ProductosConRegalos.Add("1138900181006-000-00", 2);
        ProductosConRegalos.Add("1138600190007-000-00", 1);
        ProductosConRegalos.Add("1138600190008-000-00", 1);
        ProductosConRegalos.Add("1138600190009-000-00", 1);
        ProductosConRegalos.Add("1138600190010-000-00", 1);
        ProductosConRegalos.Add("1138600190011-000-00", 1);
        ProductosConRegalos.Add("1138600190012-000-00", 1);
        ProductosConRegalos.Add("1018800001   -236-30", 1);
        ProductosConRegalos.Add("1018800001   -237-30", 1);
        ProductosConRegalos.Add("1018800001   -238-30", 1);
        #endregion

        foreach (var pedido in datosAgrupados)
        {

            if (preCuponOK != null && !pedido.detalles.Any(w => w.Detalle.objPresentacion.Codigo == "2520000111922"))
            {

                List<string> codigos = ProductosConRegalos.Keys.ToList();
                var ProductosPara_0KM = (from N in pedido.detalles
                                         where codigos.Contains(N.Detalle.CodigoCompleto.Trim())
                                         select new
                                         {
                                             Cantidad = N.Detalle.Cantidad.Value,
                                             Codigo = N.Detalle.CodigoCompleto.Trim()
                                         }).ToList();


                if (ProductosPara_0KM.Count > 0)
                {

                    long CantidadCupones = 0;
                    foreach (var item in ProductosPara_0KM)
                    {
                        int cantidadParcial = ProductosConRegalos[item.Codigo] != null ? ProductosConRegalos[item.Codigo] : 0;
                        CantidadCupones += cantidadParcial * item.Cantidad;
                    }

                    if (CantidadCupones > 0)
                    {
                        DetallePedido newDetalle = new DetallePedido();
                        newDetalle.Cantidad = CantidadCupones;
                        newDetalle.CodigoCompleto = preCuponOK.Codigo;
                        newDetalle.Presentacion = preCuponOK.IdPresentacion;
                        newDetalle.Producto = preCuponOK.objProducto.IdProducto;
                        newDetalle.ValorUnitario = preCuponOK.Precio;
                        newDetalle.ValorTotal = newDetalle.ValorUnitario * newDetalle.Cantidad;

                        pedido.cab.DetallePedidos.Add(newDetalle);

                    }
                }
            }

        }

        dc.SubmitChanges();
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object BuscarPedidos(string FI, string FF)
    {
        using (Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext())
        {
            DateTime FechaInicial = DateTime.Parse(FI);
            DateTime FechaFinal = DateTime.Parse(FF);

            var consultores = (from d in dc.CabeceraPedidos
                               where d.FechaPedido >= FechaInicial && d.FechaPedido <= FechaFinal
                               && d.MontoTotal > 0
                               select new
                               {
                                   NroPedido = d.Nro,
                                   Fecha = string.Format("{0:dd/MM/yyyy HH:mm}", d.FechaPedido),
                                   Revendedor = d.objCliente.Nombre + " - " + d.objCliente.CodigoExterno,
                                   Solicitante = d.objClienteSolicitante.Nombre + " - " + d.objClienteSolicitante.CodigoExterno,
                                   Monto = string.Format("{0:#0.00}", d.MontoTotal),
                                   IdCabecera = d.IdCabeceraPedido
                               }).ToList().OrderBy(w => w.Fecha);

            return consultores.ToList();
        }

    }
}
