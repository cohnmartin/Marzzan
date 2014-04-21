using System.Web.Services;
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
using System.Web.SessionState;
using System.Web.Script.Services;
using System.IO.Compression;


public class TempPedido
{

    public long IdDetallePedido { get; set; }
    public string Cantidad { get; set; }
    public string DescripcionCompleta { get; set; }
    public string ValorUnitario { get; set; }
    public string ValorTotal { get; set; }
    public string DescPadre { get; set; }
    public long IdPadre { get; set; }
}

/// <summary>
/// Summary description for WebServiceHelper
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebServiceHelper : System.Web.Services.WebService
{

    public WebServiceHelper()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public object WebServiceHelper_NuevoProducto(string PedidoRealizado)
    {
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)Session["Context"];
        string[] datos = PedidoRealizado.ToString().Split('@');

        string TipoOperacion = "";
        string[] IdsPresentaciones = datos[0].Split('|');
        string[] Valores = datos[1].Split('|');
        string codigoEdicion = "";

        if (datos.Length >= 4)
        {
            codigoEdicion = datos[2].ToString();
            TipoOperacion = datos[3].ToString();
        }
        else
        {
            TipoOperacion = datos[2].ToString();
        }



        for (int i = 0; i < IdsPresentaciones.Length - 1; i++)
        {
            string CodigoPresentacion = IdsPresentaciones[i].ToString();


            if (CodigoPresentacion != "")
            {

                Presentacion presentacion = (from P in Contexto.Presentacions
                                             where P.Codigo == CodigoPresentacion
                                             select P).First<Presentacion>();


                string valor = Valores[i].ToString();

                int YaPidio = (from P in Session["detPedido"] as List<DetallePedido>
                               where P.CodigoCompleto.Trim() == CodigoPresentacion.Trim()
                               select P).Count();


                if (valor != "" && int.Parse(valor) > 0)
                {
                    if (YaPidio == 0)
                    {
                        DetallePedido detPedido = new DetallePedido();
                        detPedido.Cantidad = int.Parse(valor);
                        detPedido.Producto = presentacion.objProducto.IdProducto;
                        detPedido.ProductoDesc = presentacion.objProducto.Descripcion;
                        detPedido.Presentacion = presentacion.IdPresentacion;
                        detPedido.PresentacionDesc = presentacion.Descripcion;
                        detPedido.ValorUnitario = presentacion.Precio.Value;
                        detPedido.ValorTotal = detPedido.ValorUnitario * detPedido.Cantidad;
                        detPedido.CodigoCompleto = presentacion.Codigo;
                        detPedido.Tipo = presentacion.objProducto.Tipo.ToString();

                        if (presentacion.objProducto.objPadre.EsUltimoNivel.Value)
                        {
                            detPedido.DescripcionCompleta = detPedido.ProductoDesc + " x " + detPedido.PresentacionDesc;
                            detPedido.IdPadre = presentacion.objProducto.objPadre.IdProducto;
                            detPedido.DescPadre = Helper.ObtenerDescripcionCompletaProducto(presentacion.objProducto.objPadre);
                        }
                        else
                        {
                            detPedido.DescripcionCompleta = presentacion.objProducto.DescripcionCompleta + detPedido.PresentacionDesc;
                            detPedido.IdPadre = presentacion.objProducto.objPadre.objPadre.IdProducto;
                            detPedido.DescPadre = Helper.ObtenerDescripcionCompletaProducto(presentacion.objProducto.objPadre.objPadre);
                        }

                        detPedido.DescripcionCompleta = detPedido.ProductoDesc + " x " + detPedido.PresentacionDesc;
                        (Session["detPedido"] as List<DetallePedido>).Add(detPedido);
                    }
                    else
                    {
                        DetallePedido pedidoExistente = (from P in Session["detPedido"] as List<DetallePedido>
                                                         where P.CodigoCompleto.Trim() == CodigoPresentacion.Trim()
                                                         select P).First<DetallePedido>();

                        if (TipoOperacion == "N")
                            pedidoExistente.Cantidad += int.Parse(valor);
                        else
                            pedidoExistente.Cantidad = int.Parse(valor);

                        pedidoExistente.ValorTotal = pedidoExistente.Cantidad * pedidoExistente.ValorUnitario;

                    }


                }
                else
                {
                    if (YaPidio > 0 && codigoEdicion == CodigoPresentacion.Trim())
                    {
                        DetallePedido CurrentePedido = (from P in Session["detPedido"] as List<DetallePedido>
                                                        where P.CodigoCompleto.Trim() == CodigoPresentacion.Trim()
                                                        select P).First<DetallePedido>();

                        (Session["detPedido"] as List<DetallePedido>).Remove(CurrentePedido);

                        //TotalizadorPedidos_LineaPedidoEliminada();
                    }
                }
            }
            else
                break;

        }

        //ActualizarTotalesGenerales();

        var Resultado = (from r in (List<DetallePedido>)Session["detPedido"]
                         select new TempPedido
                         {
                             IdDetallePedido = r.IdDetallePedido,
                             Cantidad = r.Cantidad.ToString(),
                             DescripcionCompleta = r.ProductoDesc,
                             ValorUnitario = r.ValorUnitario.ToString(),
                             ValorTotal = r.ValorTotal.ToString(),
                             DescPadre = r.DescPadre,
                             IdPadre = r.IdPadre

                         }).ToList();

        WebServiceHelper_CalcularPromociones("1000");

        return Resultado;

    }

    [WebMethod(EnableSession = true)]
    public string[] WebServiceHelper_ControlCantidadSeleccionada(string productosSeleccionados)
    {
        string[] datos = productosSeleccionados.Split('@');
        string[] IdsPresentaciones = datos[0].Split('|');
        string[] Valores = datos[1].Split('|');

        List<string> detallesYaPedidos = (from P in Session["detPedido"] as List<DetallePedido>
                                 where IdsPresentaciones.Contains(P.CodigoCompleto)
                                 && P.UnoPorPedido
                                 && P.Cantidad > 0
                                 select P.DescripcionCompleta).ToList();

        if (detallesYaPedidos.Count > 0)
            return detallesYaPedidos.ToArray();
        else
            return null;


    }

    [WebMethod(EnableSession = true)]
    public string WebServiceHelper_CalcularPromociones(string TotalPedido)
    {
        if (Session["Cliente"] != null)
        {
            string lblTransporteHidden = "RAPIBOX";

            Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)Session["Context"];
            Cliente CurrentClient = (Cliente)Session["Cliente"];


            Helpers.HelperPromocion helper = new Helpers.HelperPromocion();

            decimal MontoActual = decimal.Parse(TotalPedido.Replace("$", ""));

            long[] idProductosSolicitados = (from P in (Session["detPedido"] as List<DetallePedido>)
                                             select P.Producto.Value).ToArray<long>();


            List<Producto> promosValidas = (from P in Contexto.Productos
                                            where P.objConfPromocion != null
                                            && (DateTime.Now.Date >= P.objConfPromocion.FechaInicio.Date && DateTime.Now.Date <= P.objConfPromocion.FechaFinal.Date)
                                            && (P.objConfPromocion.ColTransportistas.Count == 0 || P.objConfPromocion.ColTransportistas.Where(t => t.Transporte.ToUpper() == lblTransporteHidden.ToUpper()).Count() > 0)
                                            select P).Distinct<Producto>().ToList<Producto>();



            List<Producto> promosConRegalos = (from P in promosValidas
                                               join C in Contexto.Composicions on P.IdProducto equals C.ComponentePricipal
                                               where P.objConfPromocion != null
                                               && (DateTime.Now.Date >= P.objConfPromocion.FechaInicio.Date && DateTime.Now.Date <= P.objConfPromocion.FechaFinal.Date)
                                               && (idProductosSolicitados.Contains(C.objProductoHijo.IdProducto))
                                               && (P.ColComposiciones.Where(p => p.TipoComposicion == "O").Count() > 0)
                                                   /// Para la promociones de tipo INICIL
                                               && (P.objConfPromocion.TipoPromo == "INICIAL")
                                               && (P.objConfPromocion.UnaPorPedido.Value == false)
                                               && (P.objConfPromocion.MontoMinimo.Value == 0 || MontoActual >= P.objConfPromocion.MontoMinimo.Value)
                                               && (P.objConfPromocion.ColTransportistas.Count == 0 || P.objConfPromocion.ColTransportistas.Where(t => t.Transporte.ToUpper() == lblTransporteHidden.ToUpper()).Count() > 0)
                                               select P).Distinct<Producto>().ToList<Producto>();

            promosConRegalos = promosConRegalos.OrderByDescending(c => c.CantidadElementoRequeridos).ToList<Producto>();


            List<Producto> promosVIPConRegalos = (from P in promosValidas
                                                  join C in Contexto.Composicions on P.IdProducto equals C.ComponentePricipal
                                                  where P.objConfPromocion != null
                                                  && (DateTime.Now.Date >= P.objConfPromocion.FechaInicio.Date && DateTime.Now.Date <= P.objConfPromocion.FechaFinal.Date)
                                                      /// Para la promociones de tipo VIP o VIP SENIOR
                                                  && (P.objConfPromocion.TipoPromo == CurrentClient.TipoConsultor && P.objConfPromocion.TipoPromo != "INICIAL")
                                                  && (P.ColComposiciones.Where(p => p.TipoComposicion == "O").Count() > 0)
                                                  && (P.objConfPromocion.UnaPorPedido.Value == false)
                                                  && (P.objConfPromocion.MontoMinimo.Value == 0 || MontoActual >= P.objConfPromocion.MontoMinimo.Value)
                                                  && (P.objConfPromocion.ColTransportistas.Count == 0 || P.objConfPromocion.ColTransportistas.Where(t => t.Transporte.ToUpper() == lblTransporteHidden.ToUpper()).Count() > 0)
                                                  select P).Distinct<Producto>().ToList<Producto>();

            promosVIPConRegalos = promosVIPConRegalos.OrderByDescending(c => c.CantidadElementoRequeridos).ToList<Producto>();


            long[] idPromoConRegalos = (from P in promosConRegalos
                                        select P.IdProducto).ToArray<long>();


            List<Producto> promosSinRegalos = (from P in promosValidas
                                               join C in Contexto.Composicions on P.IdProducto equals C.ComponentePricipal
                                               where P.objConfPromocion != null
                                               && (DateTime.Now.Date >= P.objConfPromocion.FechaInicio.Date && DateTime.Now.Date <= P.objConfPromocion.FechaFinal.Date)
                                               && (idProductosSolicitados.Contains(C.objProductoHijo.IdProducto) || C.objProductoHijo.Tipo == 'P')
                                                   /// Para los tres tipos de promociones
                                               && (P.objConfPromocion.TipoPromo == "INICIAL" || P.objConfPromocion.TipoPromo == CurrentClient.TipoConsultor)
                                               && (P.ColComposiciones.Where(w => w.TipoComposicion == "O").Count() == 0)
                                               && (P.objConfPromocion.UnaPorPedido.Value == false)
                                               && (P.objConfPromocion.MontoMinimo.Value == 0 || MontoActual >= P.objConfPromocion.MontoMinimo.Value)
                                               && (P.objConfPromocion.ColTransportistas.Count == 0 || P.objConfPromocion.ColTransportistas.Where(t => t.Transporte.ToUpper() == lblTransporteHidden.ToUpper()).Count() > 0)
                                               select P).Distinct<Producto>().ToList<Producto>();

            promosSinRegalos = promosSinRegalos.OrderByDescending(c => c.CantidadElementoRequeridos).ToList<Producto>();


            List<Producto> promosUnaPorPedido = (from P in promosValidas
                                                 join C in Contexto.Composicions on P.IdProducto equals C.ComponentePricipal
                                                 where P.objConfPromocion != null
                                                 && (DateTime.Now.Date >= P.objConfPromocion.FechaInicio.Date && DateTime.Now.Date <= P.objConfPromocion.FechaFinal.Date)
                                                 && (idProductosSolicitados.Contains(C.objProductoHijo.IdProducto))
                                                 && (P.objConfPromocion.TipoPromo == "INICIAL" || P.objConfPromocion.TipoPromo == CurrentClient.TipoConsultor)
                                                 && P.objConfPromocion.UnaPorPedido.Value
                                                 && (P.objConfPromocion.MontoMinimo.Value == 0 || MontoActual >= P.objConfPromocion.MontoMinimo.Value)
                                                 && (P.objConfPromocion.ColTransportistas.Count == 0 || P.objConfPromocion.ColTransportistas.Where(t => t.Transporte.ToUpper() == lblTransporteHidden.ToUpper()).Count() > 0)
                                                 select P).Distinct().ToList<Producto>();

            promosUnaPorPedido = promosUnaPorPedido.OrderByDescending(c => c.CantidadElementoRequeridos).ToList<Producto>();


            List<DetallePedido> promosGeneradasConRegalo = new List<DetallePedido>();
            List<DetallePedido> promosGeneradasConRegaloVIP = new List<DetallePedido>();
            List<DetallePedido> promosGeneradasSinRegalos = new List<DetallePedido>();
            List<DetallePedido> promosGeneradasUnaxPedido = new List<DetallePedido>();
            List<DetallePedido> AllPromosGeneradas = new List<DetallePedido>();
            List<DetallePedido> PedidoTemp = new List<DetallePedido>();
            List<DetallePedido> PedidoActual = (List<DetallePedido>)Session["detPedido"];

            if (promosConRegalos.Count > 0 || promosSinRegalos.Count > 0 || promosUnaPorPedido.Count > 0 || promosVIPConRegalos.Count > 0)
            {
                // Generacion de promociones con regalo
                promosGeneradasConRegalo = helper.GenerarPromociones(PedidoActual, promosConRegalos, true).ToList<DetallePedido>();


                // Generacion de promociones sin regalo
                PedidoTemp.AddRange(promosGeneradasConRegalo);
                PedidoTemp.AddRange(PedidoActual);
                promosGeneradasSinRegalos = helper.GenerarPromociones(PedidoTemp, promosSinRegalos, false).ToList<DetallePedido>();


                // Generacion de Promociones VIP
                PedidoTemp.Clear();
                PedidoTemp.AddRange(promosGeneradasConRegalo);
                PedidoTemp.AddRange(PedidoActual);
                PedidoTemp.AddRange(promosGeneradasSinRegalos);
                promosGeneradasConRegaloVIP = helper.GenerarPromociones(PedidoTemp, promosVIPConRegalos, true).ToList<DetallePedido>();


                List<Producto> promosExcluyentes = new List<Producto>();
                promosExcluyentes.AddRange(promosConRegalos);
                promosExcluyentes.AddRange(promosSinRegalos);

                // Generacion de Promociones de tipo una por PEDIDO
                promosGeneradasUnaxPedido = helper.GenerarPromocionesUnaxPedido(PedidoTemp, promosUnaPorPedido, promosExcluyentes).ToList<DetallePedido>();

            }


            #region  Generacion de Promociones Posibles
            PedidoTemp.Clear();
            PedidoTemp.AddRange(promosGeneradasConRegalo);
            PedidoTemp.AddRange(PedidoActual);
            PedidoTemp.AddRange(promosGeneradasSinRegalos);
            Hashtable promosPosiblesConRegalos = helper.GenerarPromocionesPosibles(PedidoTemp, promosConRegalos);
            Hashtable promosPosiblesSinRegalos = helper.GenerarPromocionesPosibles(PedidoTemp, promosSinRegalos);

            Hashtable promosPosibles = new Hashtable();
            foreach (object item in promosPosiblesConRegalos.Keys)
            {
                promosPosibles.Add(item, promosPosiblesConRegalos[item]);
            }

            foreach (object item in promosPosiblesSinRegalos.Keys)
            {
                promosPosibles.Add(item, promosPosiblesSinRegalos[item]);
            }
            #endregion

            #region Generacion Promocion especial y fija de pago facil

            //if (cboFormaPago.Text.Contains("Pago Fácil"))
            //{
            //    string codigoPromoPagoFacil = "1150000021052";
            //    if (decimal.Parse(txtMontoGeneral.Text.Replace("$", "")) > 1500)
            //    {
            //        codigoPromoPagoFacil = "1150000021073";
            //    }

            //    Producto promoPagoFacil = (from P in Contexto.Presentacions
            //                               where P.Codigo.Trim() == codigoPromoPagoFacil
            //                               select P.objProducto).First<Producto>();



            //    if (promoPagoFacil.objConfPromocion != null && promoPagoFacil.objConfPromocion.FechaInicio <= DateTime.Now && promoPagoFacil.objConfPromocion.FechaFinal > DateTime.Now
            //       && decimal.Parse(txtMontoGeneral.Text.Replace("$", "")) >= promoPagoFacil.objConfPromocion.MontoMinimo.Value
            //       && (promoPagoFacil.objConfPromocion.ColTransportistas.Count == 0 || promoPagoFacil.objConfPromocion.ColTransportistas.Any(w => w.Transporte.ToUpper() == lblTransporte.Text.ToUpper())))
            //    {
            //        List<string> descripcionPromo = new List<string>();
            //        descripcionPromo.Add("Pago|Fácil");

            //        DetallePedido pedidoPagoFacil = new DetallePedido();


            //        var composicionRegalo = from R in promoPagoFacil.ColComposiciones
            //                                where R.TipoComposicion == "O"
            //                                group R by R.Grupo into c
            //                                select new { Grupo = c.Key, componentes = c };

            //        if (composicionRegalo.Count() > 0)
            //        {

            //            // ESTE CODIGO ES EL QUE SE DEBE PONER
            //            // PARA SOPORTAR MAS DE UN GRUPO EN LAS PROMOCION DE 
            //            // 'PAGO FACIL', HACER LO MISMO PARA 'PAGO MIS CUENTAS'
            //            foreach (var itemComponente in composicionRegalo)
            //            {
            //                List<Producto> productos = (from P in itemComponente.componentes
            //                                            select P.objProductoHijo).ToList<Producto>();

            //                DetalleRegalos newRegalo = new DetalleRegalos();
            //                newRegalo.DescripcionRegalo = Helper.ObtenerDescripcionCompletaProductoEnComun(productos) + " x " + itemComponente.componentes.First().objPresentacion.Descripcion;
            //                newRegalo.IdPresentacionRegaloSeleccionado = 0;
            //                newRegalo.TipoRegalo = "Producto";
            //                newRegalo.objDetallePedido = pedidoPagoFacil;
            //                newRegalo.Grupo = itemComponente.componentes.First().Grupo.Value;
            //                pedidoPagoFacil.ColRegalos.Add(newRegalo);
            //            }


            //            //// Codigo a Reemplazar por el de arriba
            //            //DetalleRegalos newRegalo = new DetalleRegalos();
            //            //List<Producto> productos = (from P in composicionRegalo.First().componentes
            //            //                            select P.objProductoHijo).ToList<Producto>();
            //            //newRegalo.DescripcionRegalo = Helper.ObtenerDescripcionCompletaProductoEnComun(productos) + " x " + composicionRegalo.First().componentes.First().objPresentacion.Descripcion;
            //            //newRegalo.IdPresentacionRegaloSeleccionado = 0;
            //            //newRegalo.TipoRegalo = "Producto";
            //            //newRegalo.objDetallePedido = pedidoPagoFacil;
            //            //newRegalo.Grupo = composicionRegalo.First().componentes.First().Grupo.Value;
            //            //pedidoPagoFacil.ColRegalos.Add(newRegalo);




            //            pedidoPagoFacil.Cantidad = 1;
            //            pedidoPagoFacil.Producto = promoPagoFacil.IdProducto;
            //            pedidoPagoFacil.Presentacion = promoPagoFacil.ColPresentaciones[0].IdPresentacion;
            //            pedidoPagoFacil.ProductoDesc = promoPagoFacil.Descripcion;
            //            pedidoPagoFacil.PresentacionDesc = promoPagoFacil.ColPresentaciones[0].Descripcion;
            //            pedidoPagoFacil.DescripcionCompleta = pedidoPagoFacil.ProductoDesc;
            //            pedidoPagoFacil.DescProductosUtilizados = descripcionPromo;
            //            pedidoPagoFacil.CodigoCompleto = promoPagoFacil.ColPresentaciones[0].Codigo;
            //            pedidoPagoFacil.Tipo = "E";

            //            AllPromosGeneradas.Add(pedidoPagoFacil);
            //        }
            //    }
            //}
            //else if (cboFormaPago.Text.Contains("Pago Mis Cuentas"))
            //{
            //    string codigoPromoPagoMisCuenas = "1150000021207";
            //    if (decimal.Parse(txtMontoGeneral.Text.Replace("$", "")) > 1500)
            //    {
            //        codigoPromoPagoMisCuenas = "1150000021208";
            //    }

            //    Producto promoPagoMisCuentas = (from P in Contexto.Presentacions
            //                                    where P.Codigo.Trim() == codigoPromoPagoMisCuenas
            //                                    select P.objProducto).First<Producto>();



            //    if (promoPagoMisCuentas.objConfPromocion != null && promoPagoMisCuentas.objConfPromocion.FechaInicio <= DateTime.Now && promoPagoMisCuentas.objConfPromocion.FechaFinal > DateTime.Now
            //       && decimal.Parse(txtMontoGeneral.Text.Replace("$", "")) >= promoPagoMisCuentas.objConfPromocion.MontoMinimo.Value
            //       && (promoPagoMisCuentas.objConfPromocion.ColTransportistas.Count == 0 || promoPagoMisCuentas.objConfPromocion.ColTransportistas.Any(w => w.Transporte.ToUpper() == lblTransporte.Text.ToUpper())))
            //    {
            //        List<string> descripcionPromo = new List<string>();
            //        descripcionPromo.Add("Pago|Mis Cuentas");

            //        DetallePedido pedidoPagoFacil = new DetallePedido();


            //        var composicionRegalo = from R in promoPagoMisCuentas.ColComposiciones
            //                                where R.TipoComposicion == "O"
            //                                group R by R.Grupo into c
            //                                select new { Grupo = c.Key, componentes = c };

            //        if (composicionRegalo.Count() > 0)
            //        {

            //            //List<Producto> productos = (from P in composicionRegalo.First().componentes
            //            //                            select P.objProductoHijo).ToList<Producto>();

            //            //DetalleRegalos newRegalo = new DetalleRegalos();
            //            //newRegalo.DescripcionRegalo = Helper.ObtenerDescripcionCompletaProductoEnComun(productos) + " x " + composicionRegalo.First().componentes.First().objPresentacion.Descripcion;
            //            //newRegalo.IdPresentacionRegaloSeleccionado = 0;
            //            //newRegalo.TipoRegalo = "Producto";
            //            //newRegalo.objDetallePedido = pedidoPagoFacil;
            //            //newRegalo.Grupo = composicionRegalo.First().componentes.First().Grupo.Value;
            //            //pedidoPagoFacil.ColRegalos.Add(newRegalo);

            //            foreach (var itemComponente in composicionRegalo)
            //            {
            //                List<Producto> productos = (from P in itemComponente.componentes
            //                                            select P.objProductoHijo).ToList<Producto>();

            //                DetalleRegalos newRegalo = new DetalleRegalos();
            //                newRegalo.DescripcionRegalo = Helper.ObtenerDescripcionCompletaProductoEnComun(productos) + " x " + itemComponente.componentes.First().objPresentacion.Descripcion;
            //                newRegalo.IdPresentacionRegaloSeleccionado = 0;
            //                newRegalo.TipoRegalo = "Producto";
            //                newRegalo.objDetallePedido = pedidoPagoFacil;
            //                newRegalo.Grupo = itemComponente.componentes.First().Grupo.Value;
            //                pedidoPagoFacil.ColRegalos.Add(newRegalo);
            //            }




            //            pedidoPagoFacil.Cantidad = 1;
            //            pedidoPagoFacil.Producto = promoPagoMisCuentas.IdProducto;
            //            pedidoPagoFacil.Presentacion = promoPagoMisCuentas.ColPresentaciones[0].IdPresentacion;
            //            pedidoPagoFacil.ProductoDesc = promoPagoMisCuentas.Descripcion;
            //            pedidoPagoFacil.PresentacionDesc = promoPagoMisCuentas.ColPresentaciones[0].Descripcion;
            //            pedidoPagoFacil.DescripcionCompleta = pedidoPagoFacil.ProductoDesc;
            //            pedidoPagoFacil.DescProductosUtilizados = descripcionPromo;
            //            pedidoPagoFacil.CodigoCompleto = promoPagoMisCuentas.ColPresentaciones[0].Codigo;
            //            pedidoPagoFacil.Tipo = "E";

            //            AllPromosGeneradas.Add(pedidoPagoFacil);
            //        }
            //    }
            //}
            //else if (cboFormaPago.Text.Contains("Contra Reembolso"))
            //{
            //    string codigoPromoContraReembolso = "1150000021244";

            //    Producto promoContraReembolso = (from P in Contexto.Presentacions
            //                                     where P.Codigo.Trim() == codigoPromoContraReembolso
            //                                     select P.objProducto).FirstOrDefault<Producto>();


            //    if (promoContraReembolso != null)
            //    {
            //        decimal LimiteContrReembolso = decimal.Parse((from P in Session["ParametrosSistema"] as List<Parametro>
            //                                                      where P.IdParametro == (int)TiposDeParametros.LimiteContraReembolso
            //                                                      select P.Valor).Single());


            //        /// Si el pedido es realizado con la forma de pago contra reembolso, 
            //        /// el monto del pedido es mayor o igual al parámetro: limite de compra en contra reembolso y 
            //        /// el cliente de la nota de pedido posee en ese momento un valor en la cuenta corriente a favor (en negativo)
            //        /// y superior al MontoMinimo, entonces se generará para dicho pedido la promoción en cuestión
            //        if (promoContraReembolso != null && promoContraReembolso.objConfPromocion != null && promoContraReembolso.objConfPromocion.FechaInicio <= DateTime.Now && promoContraReembolso.objConfPromocion.FechaFinal > DateTime.Now
            //           && decimal.Parse(txtMontoGeneral.Text.Replace("$", "")) >= LimiteContrReembolso && (CurrentClient.SaldoCtaCte.Value < 0 && Math.Abs(CurrentClient.SaldoCtaCte.Value) >= promoContraReembolso.objConfPromocion.MontoMinimo))
            //        {
            //            List<string> descripcionPromo = new List<string>();
            //            descripcionPromo.Add("Contra|Reembolso y Poseer un Saldo Mayor a $200 en Cta. Cte.");

            //            DetallePedido pedidoPagoFacil = new DetallePedido();


            //            var composicionRegalo = from R in promoContraReembolso.ColComposiciones
            //                                    where R.TipoComposicion == "O"
            //                                    group R by R.Grupo into c
            //                                    select new { Grupo = c.Key, componentes = c };

            //            if (composicionRegalo.Count() > 0)
            //            {

            //                //DetalleRegalos newRegalo = new DetalleRegalos();
            //                //List<Producto> productos = (from P in composicionRegalo.First().componentes
            //                //                            select P.objProductoHijo).ToList<Producto>();


            //                //newRegalo.DescripcionRegalo = Helper.ObtenerDescripcionCompletaProductoEnComun(productos) + " x " + composicionRegalo.First().componentes.First().objPresentacion.Descripcion;
            //                //newRegalo.IdPresentacionRegaloSeleccionado = 0;
            //                //newRegalo.TipoRegalo = "Producto";
            //                //newRegalo.objDetallePedido = pedidoPagoFacil;
            //                //newRegalo.Grupo = composicionRegalo.First().componentes.First().Grupo.Value;
            //                //pedidoPagoFacil.ColRegalos.Add(newRegalo);

            //                foreach (var itemComponente in composicionRegalo)
            //                {
            //                    List<Producto> productos = (from P in itemComponente.componentes
            //                                                select P.objProductoHijo).ToList<Producto>();

            //                    DetalleRegalos newRegalo = new DetalleRegalos();
            //                    newRegalo.DescripcionRegalo = Helper.ObtenerDescripcionCompletaProductoEnComun(productos) + " x " + itemComponente.componentes.First().objPresentacion.Descripcion;
            //                    newRegalo.IdPresentacionRegaloSeleccionado = 0;
            //                    newRegalo.TipoRegalo = "Producto";
            //                    newRegalo.objDetallePedido = pedidoPagoFacil;
            //                    newRegalo.Grupo = itemComponente.componentes.First().Grupo.Value;
            //                    pedidoPagoFacil.ColRegalos.Add(newRegalo);
            //                }




            //                pedidoPagoFacil.Cantidad = 1;
            //                pedidoPagoFacil.Producto = promoContraReembolso.IdProducto;
            //                pedidoPagoFacil.Presentacion = promoContraReembolso.ColPresentaciones[0].IdPresentacion;
            //                pedidoPagoFacil.ProductoDesc = promoContraReembolso.Descripcion;
            //                pedidoPagoFacil.PresentacionDesc = promoContraReembolso.ColPresentaciones[0].Descripcion;
            //                pedidoPagoFacil.DescripcionCompleta = pedidoPagoFacil.ProductoDesc;
            //                pedidoPagoFacil.DescProductosUtilizados = descripcionPromo;
            //                pedidoPagoFacil.CodigoCompleto = promoContraReembolso.ColPresentaciones[0].Codigo;
            //                pedidoPagoFacil.Tipo = "E";

            //                AllPromosGeneradas.Add(pedidoPagoFacil);
            //            }
            //        }
            //    }
            //}

            #endregion

            #region Inicialización del control de visualización de Promociones
            AllPromosGeneradas.AddRange(promosGeneradasUnaxPedido);
            AllPromosGeneradas.AddRange(promosGeneradasConRegalo);
            AllPromosGeneradas.AddRange(promosGeneradasSinRegalos);
            AllPromosGeneradas.AddRange(promosGeneradasConRegaloVIP);


            ///Marco las promociones generada con el id de la promoción
            /// que se guardo anteriormente.
            if (Session["PromosGuardadas"] != null)
            {
                foreach (DetallePedido item in (Session["PromosGuardadas"] as List<DetallePedido>))
                {
                    var promoGenerada = (from p in AllPromosGeneradas
                                         where p.Producto == item.objProducto.IdProducto
                                         && p.IdDetalleLineaGuardado == 0
                                         select p).FirstOrDefault();

                    if (promoGenerada != null)
                    {
                        promoGenerada.IdDetalleLineaGuardado = item.IdDetallePedido;
                        promoGenerada.ValorTotal = Math.Abs(item.ValorTotal.Value);

                        int indexRegalo = 0;
                        foreach (DetallePedido detProdSel in item.ColProductosSeleccionados)
                        {
                            promoGenerada.ColRegalos[indexRegalo].TipoRegalo = "Producto";
                            promoGenerada.ColRegalos[indexRegalo].IdPresentacionPreSeleccionado = detProdSel.objPresentacion.IdPresentacion;
                            promoGenerada.IdRegaloSeleccionado = detProdSel.objPresentacion.IdPresentacion;

                            if (!detProdSel.objProducto.Descripcion.Contains("x Unidad"))
                                promoGenerada.ColRegalos[indexRegalo].DescripcionPreSeleccionado = detProdSel.objProducto.objPadre.Descripcion + " " + detProdSel.objProducto.Descripcion + " x " + detProdSel.objPresentacion.Descripcion;
                            else
                                promoGenerada.ColRegalos[indexRegalo].DescripcionPreSeleccionado = detProdSel.objProducto.Descripcion;

                            indexRegalo++;
                        }

                    }

                }
            }

            //TotalizadorPromos1.InitControl(AllPromosGeneradas, promosPosibles);
            //upPromos.Update();

            #endregion


        }

        return "OK";
    }


    [WebMethod(EnableSession = true)]
    public string WebServiceHelper_ObtenerPromosActivas(string TipoConsultor)
    {
        if (Session["PromosActivas"] == null || TipoConsultor != Session["TipoConsultor"].ToString())
        {
            Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)Session["Context"];
            List<string> tiposPromoHabilitados = new List<string>();

            if (TipoConsultor.ToUpper() == "INICIAL")
            {
                tiposPromoHabilitados.Add("INICIAL");
            }
            else if (TipoConsultor.ToUpper() == "VIP")
            {
                tiposPromoHabilitados.Add("INICIAL");
                tiposPromoHabilitados.Add("VIP");
            }
            else
            {
                tiposPromoHabilitados.Add("INICIAL");
                tiposPromoHabilitados.Add("VIP");
                tiposPromoHabilitados.Add("VIP SENIOR");
            }


            var promocionesActivas = (from p in Contexto.View_PromosActivas
                                      where tiposPromoHabilitados.Contains(p.TipoPromo)
                                      orderby p.Descripcion
                                      group p by p.IdPromocion into g
                                      select new
                                      {
                                          g.First().Codigo,
                                          IdPromocion = g.Key,
                                          g.First().Descripcion,
                                          g.First().UnaPorPedido,
                                          Precio = g.Sum(w => w.Precio)
                                      }).Distinct().ToList().OrderBy(w => w.Descripcion);

            string tbl = "<table width='68%' height='100%' id='tblCargaPedido' class='GrillaProductos'><TBODY id=tblCargaPedido>" +
            "<TR>" +
            "<TD bgColor=white align=middle>Productos</TD>" +
            "<TD bgColor=white align=middle>Unidad</TD></TR>" +
            "<TR>";

            foreach (var item in promocionesActivas)
            {
                string precio = string.Format("{0:00.00}", item.Precio);
                string maxValuePedido = "999";
                if (item.UnaPorPedido.Value)
                    maxValuePedido = "1";

                tbl += "<TR>";
                tbl += "<TD><SPAN id='span1_" + item.Descripcion + "' height='22px' Codigos='" + item.Codigo + "|' Descripciones='Unidad|' Precios='" + precio + "|' IdProducto='" + item.IdPromocion + "'>" + item.Descripcion + "</SPAN></TD>";
                tbl += "<TD width=45 align=middle Id='td_" + item.Codigo + "' Codigo='" + item.Codigo + "' Precio='" + precio + "' Nombre='" + item.Descripcion + "' onmouseover='ShowControl()' Presentacion='Unidad' maxValue='" + maxValuePedido + "'>0</TD></TR>";

            }

            tbl += "</table>";

            Session["PromosActivas"] = tbl;
            Session["TipoConsultor"] = TipoConsultor;

        }

        return Session["PromosActivas"].ToString();
    }

    [WebMethod(EnableSession = true)]
    public string WebServiceHelper_GenerarTablaProductosRequeridos(string idDetallePedidoPromo)
    {
        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)Session["Context"];
        DetallePedido promoActual = (Session["PromosGeneradas"] as List<DetallePedido>).Where(w => w.IdDetallePedido == long.Parse(idDetallePedidoPromo)).FirstOrDefault();

        /// Recupero y genero los datos para los elementos requeridos
        /// de la promoción que se esta solicitando.
        var componentesRequeridos = from p in Contexto.Composicions
                                    where p.ComponentePricipal == promoActual.Producto &&
                                    p.TipoComposicion == "C"
                                    group p by p.Grupo into c
                                    select new { Grupo = c.Key, componentes = c };

        List<DetalleProductosRequeridos> ReqIngresados = promoActual.colProductosRequeridos.Where(w => w.Tipo != "Fijo").ToList();

        Random r = new Random();
        string tblTotal = "";
        string gruposCreados = "";
        foreach (var item in componentesRequeridos)
        {

            if (item.componentes.Count() > 1)
            {

                string tblSimple = "<table ElementosRequeridos='" + item.componentes.First().Cantidad.ToString() + "' id='tbl_Grupo_" + item.Grupo + "' width='95%' border='0' style=''>";
                tblSimple += "    <tr><td colspan='6' style='padding-top:5px;padding-bottom:5px;border-bottom: 1px solid #0066CC'>" +
                             "        <asp:Label ID='label13' runat='server' Style='color: #0066CC; font-family: Tahoma;font-size: 13px;font-weight:bold'>DE ESTE GRUPO DEBE SELECCIONAR LA CANTIDAD TOTAL DE <span style='font-weight: normal;color:black;font-weight:bold'>" + item.componentes.First().Cantidad.ToString() + "</span> PRODUCTOS</asp:Label>" +
                             "    </td></tr>";

                int contColumn = 0;
                foreach (var comp in item.componentes.OrderBy(w => w.objProductoHijo.Descripcion))
                {


                    int randomId = r.Next(0, 999);
                    string nombreProducto = comp.objProductoHijo.Descripcion + " x " + item.componentes.FirstOrDefault().objPresentacion.Descripcion;
                    string idProducto = comp.objProductoHijo.IdProducto.ToString();
                    string idPresentacion = comp.objPresentacion.IdPresentacion.ToString();
                    string valorUnitario = item.componentes.FirstOrDefault().objPresentacion.Precio.Value.ToString().Replace(".", ",");
                    string codigoCompleto = item.componentes.FirstOrDefault().objPresentacion.Codigo;


                    DetalleProductosRequeridos DetalleIngresado = ReqIngresados.Where(w => w.IdPresentacion == long.Parse(idPresentacion) && w.IdProducto == long.Parse(idProducto)).FirstOrDefault();

                    string lblColor = "Black";
                    if (DetalleIngresado != null)
                        lblColor = "Blue";

                    string inputValue = "0";
                    if (DetalleIngresado != null)
                        inputValue = DetalleIngresado.Cantidad.ToString();


                    if (contColumn == 0)
                    {
                        tblSimple += "    <tr>";
                        tblSimple += "    <td><asp:Label id='lbl_" + randomId.ToString() + "' Style='color: " + lblColor + "; font-family: Sans-Serif; font-size: 11px;text-transform:capitalize' ID='lblItemGrupo_" + item.Grupo + "' runat='server' Text=''>" + nombreProducto + "</asp:Label></td>" +
                                     "    <td><input value='" + inputValue + "' id='inp_" + randomId.ToString() + "' class='Grupo_" + item.Grupo + "' type='text' onkeydown='return InputValidateByKeyProducto(this)' onkeyup='InputByKeyProducto(this);' maxValue='" + item.componentes.First().Cantidad.ToString() + "' onMousemove='CambiarCursorProductos(this);' OnClick='ActualizarCantidadProductos(this);' style='width:45px;border: 1px solid #CACACA;background: white url(Imagenes/TextNum.gif) no-repeat right top' value='0'  " +
                                     "  idProducto='" + idProducto +
                                     "' idPresentacion='" + idPresentacion +
                                     "' precio='" + valorUnitario +
                                     "' codigo='" + codigoCompleto +
                                     "' /></td>";

                        gruposCreados += gruposCreados.IndexOf("Grupo_" + item.Grupo) < 0 ? "Grupo_" + item.Grupo + "@" : "";
                    }
                    else
                    {
                        tblSimple += "    <td><asp:Label id='lbl_" + randomId.ToString() + "' Style='color: " + lblColor + "; font-family: Sans-Serif; font-size: 11px;text-transform:capitalize' ID='lblItemGrupo_" + item.Grupo + "' runat='server' Text=''>" + nombreProducto + "</asp:Label></td>" +
                                     "    <td><input value='" + inputValue + "' id='inp_" + randomId.ToString() + "' class='Grupo_" + item.Grupo + "' type='text' onkeydown='return InputValidateByKeyProducto(this)' onkeyup='InputByKeyProducto(this);' maxValue='" + item.componentes.First().Cantidad.ToString() + "' onMousemove='CambiarCursorProductos(this);' OnClick='ActualizarCantidadProductos(this);' style='width:45px;border: 1px solid #CACACA;background: white url(Imagenes/TextNum.gif) no-repeat right top' value='0'  " +
                                     "  idProducto='" + idProducto +
                                     "' idPresentacion='" + idPresentacion +
                                     "' precio='" + valorUnitario +
                                     "' codigo='" + codigoCompleto +
                                     "' /></td>";

                        gruposCreados += gruposCreados.IndexOf("Grupo_" + item.Grupo) < 0 ? "Grupo_" + item.Grupo + "@" : "";
                    }

                    if (contColumn == 2)
                    {
                        tblSimple += "    </tr>";
                        contColumn = 0;
                    }
                    else
                    {
                        contColumn++;
                    }


                }

                tblSimple += "</table>";
                tblTotal += tblSimple;

            }
        }

        return gruposCreados + "|" + tblTotal;
    }


    [WebMethod(EnableSession = true)]
    public string WebServiceHelper_AsociarProductosRequeridos(string idDetallePedidoPromo, string seleccion)
    {

        Marzzan_InfolegacyDataContext Contexto = (Marzzan_InfolegacyDataContext)Session["Context"];
        DetallePedido promoActual = (Session["PromosGeneradas"] as List<DetallePedido>).Where(w => w.IdDetallePedido == long.Parse(idDetallePedidoPromo)).FirstOrDefault();

        List<DetalleProductosRequeridos> requeridosTemp = promoActual.colProductosRequeridos.Where(w => w.Tipo != "Fijo").ToList();
        foreach (var item in requeridosTemp)
        {
            promoActual.colProductosRequeridos.Remove(item);
        }

        var datos = seleccion.Split('|');


        foreach (var item in datos)
        {
            if (item != "")
            {
                long idProducto = long.Parse(item.Split('@')[0]);
                long idPresentacion = long.Parse(item.Split('@')[1]);
                int cantidad = int.Parse(item.Split('@')[2]); ;
                string Descripcion = item.Split('@')[3];
                decimal precio = decimal.Parse(item.Split('@')[4]);
                string codigo = item.Split('@')[5];

                DetalleProductosRequeridos det = new DetalleProductosRequeridos();
                det.Cantidad = cantidad;
                det.IdProducto = idProducto;
                det.IdPresentacion = idPresentacion;
                det.ValorUnitario = precio;
                det.CodigoCompleto = codigo;
                det.objDetallePedido = promoActual;
                det.Tipo = "Dinamico";

                det.DescripcionProducto = "<b><span style='color:Blue' >" + cantidad + "</span></b> " + Descripcion;
                promoActual.colProductosRequeridos.Add(det);

            }
        }

        string resultado = "";
        foreach (var item in promoActual.colProductosRequeridos)
        {
            resultado += item.DescripcionProducto + "|";
        }

        int TotalIngresados = promoActual.colProductosRequeridos.Sum(w => w.Cantidad).Value;
        return resultado + "@" + TotalIngresados;
    }
}

