using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CommonMarzzan;

public partial class BusquedaPorCodigos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        Dictionary<long, int> datos = new Dictionary<long, int>();
        datos.Add(153546, 1);
        datos.Add(153994, 1);
        datos.Add(153850, 1);
        datos.Add(153556, 1);
        datos.Add(153564, 1);
        datos.Add(153596, 1);
        datos.Add(153673, 1);
        datos.Add(153725, 1);
        datos.Add(153507, 1);
        datos.Add(153888, 2);
        datos.Add(153995, 1);
        datos.Add(154000, 1);
        datos.Add(153992, 1);
        datos.Add(153632, 1);
        datos.Add(153644, 1);
        datos.Add(153388, 1);
        datos.Add(153772, 1);
        datos.Add(153958, 1);
        datos.Add(153936, 1);
        datos.Add(153914, 1);
        datos.Add(153848, 1);
        datos.Add(153825, 1);
        datos.Add(153499, 1);
        datos.Add(154027, 1);
        datos.Add(153734, 2);
        datos.Add(153757, 1);
        datos.Add(153797, 1);
        datos.Add(153884, 1);
        datos.Add(153822, 2);
        datos.Add(153826, 1);
        datos.Add(153817, 2);
        datos.Add(153890, 1);
        datos.Add(153683, 1);
        datos.Add(153678, 1);
        datos.Add(153885, 2);
        datos.Add(153712, 2);
        datos.Add(153810, 1);
        datos.Add(153776, 1);
        datos.Add(153222, 1);
        datos.Add(153675, 2);
        datos.Add(153769, 1);
        datos.Add(153516, 2);
        datos.Add(153573, 1);
        datos.Add(153132, 1);
        datos.Add(153151, 2);
        datos.Add(153959, 2);
        datos.Add(154025, 1);
        datos.Add(154022, 1);
        datos.Add(153715, 1);
        datos.Add(153944, 1);
        datos.Add(153723, 1);
        datos.Add(153642, 1);
        datos.Add(154005, 1);

        List<string> UnaPromo = (from d in datos
                                 where d.Value == 1
                                 select d.Key.ToString()).ToList<string>();

        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        ///////////// Proceso para UNA promociones /////////////////////////

        /// 1 Promos --> falta dar la promo : registros actualizar 23
        var PedidosSinPromo = (from p in dc.CabeceraPedidos
                               where UnaPromo.Contains(p.Nro)
                               && p.DetallePedidos.Count(w => w.Producto == 5422) == 0
                               && p.DetallePedidos.Count(w => w.Producto == 5403) == 1
                               && p.DetallePedidos.Count(w => w.Producto == 5479) == 1
                               select new
                                {
                                    pedido = p,
                                    Id = p.IdCabeceraPedido,
                                    detalle = p.DetallePedidos
                                }).ToList();

        foreach (var item in PedidosSinPromo)
        {
            DetallePedido detAct = item.detalle.Where(w => w.Producto == 5403).FirstOrDefault();
            detAct.Cantidad = 1;
            detAct.ValorTotal = detAct.Cantidad * detAct.ValorUnitario;


            DetallePedido detAct1 = item.detalle.Where(w => w.Producto == 5479).FirstOrDefault();
            detAct1.Cantidad = 1;
            detAct1.ValorTotal = detAct1.Cantidad * detAct1.ValorUnitario;


            DetallePedido newDet = new DetallePedido();
            newDet.CabeceraPedido = item.Id;
            newDet.Producto = 5422;
            newDet.Presentacion = 5470;
            newDet.CodigoCompleto = "1150000021519   ";
            newDet.Cantidad = 1;
            newDet.ValorUnitario = decimal.Parse("-5,50");
            newDet.ValorTotal = decimal.Parse("-5,50");


            item.pedido.DetallePedidos.Add(newDet);
        }



        /// 1 Promos --> tiene dos productos de mas : registros actualizar 20
        var PedidosProductosDeMas = (from p in dc.CabeceraPedidos
                                     where UnaPromo.Contains(p.Nro)
                                     && p.DetallePedidos.Count(w => w.Producto == 5422) == 1
                                     && p.DetallePedidos.Count(w => w.Producto == 5403) == 2
                                     && p.DetallePedidos.Count(w => w.Producto == 5479) == 2
                                     select new
                                     {
                                         pedido = p,
                                         Id = p.IdCabeceraPedido,
                                         detalle = p.DetallePedidos
                                     }).ToList();

        foreach (var item in PedidosProductosDeMas)
        {

            DetallePedido detEliminar = item.detalle.Where(w => w.Producto == 5403).FirstOrDefault();
            DetallePedido detEliminar1 = item.detalle.Where(w => w.Producto == 5479).FirstOrDefault();
            dc.DetallePedidos.DeleteOnSubmit(detEliminar);
            dc.DetallePedidos.DeleteOnSubmit(detEliminar1);

        }



        ///////////// Proceso para dos promociones /////////////////////////

        List<string> DosPromos = (from d in datos
                                  where d.Value == 2
                                  select d.Key.ToString()).ToList<string>();


        var PedidosDosPromos = (from p in dc.CabeceraPedidos
                                where DosPromos.Contains(p.Nro)
                                && p.MontoTotal == p.DetallePedidos.Sum(w => w.ValorTotal)
                                select new
                                {
                                    pedido = p,
                                    Id = p.IdCabeceraPedido,
                                    detalle = p.DetallePedidos
                                }).ToList();


        foreach (var item in PedidosDosPromos)
        {
            if (item.Id == 155070)
            {

                DetallePedido newDet = new DetallePedido();
                newDet.CabeceraPedido = item.Id;
                newDet.Producto = 5422;
                newDet.Presentacion = 5470;
                newDet.CodigoCompleto = "1150000021519   ";
                newDet.Cantidad = 2;
                newDet.ValorUnitario = decimal.Parse("-5,50");
                newDet.ValorTotal = newDet.ValorUnitario * newDet.Cantidad;

                item.pedido.DetallePedidos.Add(newDet);

            }
            else if (item.Id == 155229)
            {
                DetallePedido detEliminar1 = item.detalle.Where(w => w.Producto == 5422).FirstOrDefault();
                dc.DetallePedidos.DeleteOnSubmit(detEliminar1);


                DetallePedido detAct = item.detalle.Where(w => w.Producto == 5403).FirstOrDefault();
                detAct.Cantidad--;
                detAct.ValorTotal -= detAct.ValorUnitario;

                DetallePedido detAct1 = item.detalle.Where(w => w.Producto == 5479).FirstOrDefault();
                detAct1.Cantidad--;
                detAct1.ValorTotal -= detAct1.ValorUnitario;
            }
            else if (item.Id == 155162)
            {

                DetallePedido detAct = item.detalle.Where(w => w.Producto == 5422).FirstOrDefault();
                detAct.Cantidad++;
                detAct.ValorTotal += detAct.ValorUnitario;

            }
            else if (item.Id == 155157)
            {

                DetallePedido detAct = item.detalle.Where(w => w.Producto == 5422).FirstOrDefault();
                detAct.Cantidad++;
                detAct.ValorTotal += detAct.ValorUnitario;

            }
            else if (item.Id == 155226)
            {

                List<DetallePedido> detEliminar = item.detalle.Where(w => w.Producto == 5403 && w.Cantidad == 1).ToList();
                foreach (var itemD in detEliminar)
                {
                    dc.DetallePedidos.DeleteOnSubmit(itemD);
                }

                List<DetallePedido> detEliminar1 = item.detalle.Where(w => w.Producto == 5479 && w.Cantidad == 1).ToList();
                foreach (var itemD in detEliminar1)
                {
                    dc.DetallePedidos.DeleteOnSubmit(itemD);
                }

            }
            else if (item.Id == 155048)
            {
                DetallePedido newDet = new DetallePedido();
                newDet.CabeceraPedido = item.Id;
                newDet.Producto = 5422;
                newDet.Presentacion = 5470;
                newDet.CodigoCompleto = "1150000021519   ";
                newDet.Cantidad = 2;
                newDet.ValorUnitario = decimal.Parse("-5,50");
                newDet.ValorTotal = newDet.ValorUnitario * newDet.Cantidad;

                item.pedido.DetallePedidos.Add(newDet);
            }
            else if (item.Id == 155011)
            {
                DetallePedido newDet = new DetallePedido();
                newDet.CabeceraPedido = item.Id;
                newDet.Producto = 5422;
                newDet.Presentacion = 5470;
                newDet.CodigoCompleto = "1150000021519   ";
                newDet.Cantidad = 2;
                newDet.ValorUnitario = decimal.Parse("-5,50");
                newDet.ValorTotal = newDet.ValorUnitario * newDet.Cantidad;

                item.pedido.DetallePedidos.Add(newDet);
            }
            else if (item.Id == 154850)
            {
                DetallePedido newDet = new DetallePedido();
                newDet.CabeceraPedido = item.Id;
                newDet.Producto = 5422;
                newDet.Presentacion = 5470;
                newDet.CodigoCompleto = "1150000021519   ";
                newDet.Cantidad = 2;
                newDet.ValorUnitario = decimal.Parse("-5,50");
                newDet.ValorTotal = newDet.ValorUnitario * newDet.Cantidad;

                item.pedido.DetallePedidos.Add(newDet);
            }
            else if (item.Id == 154480)
            {
                DetallePedido newDet = new DetallePedido();
                newDet.CabeceraPedido = item.Id;
                newDet.Producto = 5422;
                newDet.Presentacion = 5470;
                newDet.CodigoCompleto = "1150000021519   ";
                newDet.Cantidad = 2;
                newDet.ValorUnitario = decimal.Parse("-5,50");
                newDet.ValorTotal = newDet.ValorUnitario * newDet.Cantidad;

                item.pedido.DetallePedidos.Add(newDet);
            }
            else if (item.Id == 155301)
            {

                DetallePedido detAct = item.detalle.Where(w => w.Producto == 5422).FirstOrDefault();
                detAct.Cantidad++;
                detAct.ValorTotal += detAct.ValorUnitario;

            }
        }

        dc.SubmitChanges();



        List<string> allPromos = (from d in datos
                                  select d.Key.ToString()).ToList<string>();


        var Pedidos = (from p in dc.CabeceraPedidos
                       where allPromos.Contains(p.Nro)
                       && p.MontoTotal != p.DetallePedidos.Sum(w => w.ValorTotal)
                       select new
                       {
                           pedido = p,
                           Id = p.IdCabeceraPedido,
                           detalle = p.DetallePedidos
                       }).ToList();


        foreach (var item in Pedidos)
        {
            item.pedido.MontoTotal = item.detalle.Sum(w => w.ValorTotal).Value;

        }

        dc.SubmitChanges();
    }
}
