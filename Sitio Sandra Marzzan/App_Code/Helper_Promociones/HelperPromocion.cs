using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CommonMarzzan;

namespace Helpers
{
    /// <summary>
    /// Summary description for HelperPromocion
    /// </summary>
    public class HelperPromocion
    {
        private List<PedidoTemporal> _currentPedidoTemporal = new List<PedidoTemporal>();
        private int _cantidadPromociones = 0;
        private List<DetalleComponentePromocion> _DetallesComponentes = new List<DetalleComponentePromocion>();
        private List<Producto> _promos = null;

        public HelperPromocion()
        {


        }


        /// <summary>
        /// Establece la cantidad de promociones que se va a poder
        /// generar a partir del pedido actual.
        /// </summary>
        public void CalcularCantidadPromociones()
        {

                if (_DetallesComponentes.Count > 0)
                {

                    _cantidadPromociones = (from D in _DetallesComponentes
                                            select D.CantidadPromoIndividual).Min();

                }
                else
                {
                    _cantidadPromociones = 0;
                }

        }
      

        /// <summary>
        /// Genera una lista de clases para poder realizar los calculos correspondientes
        /// de las promociones
        /// </summary>
        /// <param name="Promo">Promoción con la que se van a realizar los calculos</param>
        private void GenerarComponentesPromocion(Producto Promo)
        {
            _DetallesComponentes = new List<DetalleComponentePromocion>();

            var GrupoComponentesPromo = from G in Promo.ColComposiciones
                                        where G.TipoComposicion == "C"
                                        group G by G.Grupo into c
                                        select new { Grupo = c.Key, componentes = c };

            foreach (var componente in GrupoComponentesPromo)
            { 

                List<long> idsPre = (from Pre in componente.componentes
                                     where Pre.Presentacion.HasValue
                                     select Pre.Presentacion.Value).ToList<long>();

                List<long> idsComp = (from Comp in componente.componentes
                                      select Comp.ComponenteHijo.Value).ToList<long>();

                decimal Cantidad = (from P in _currentPedidoTemporal
                                where idsPre.Contains(P.IdPresentacion)
                                && idsComp.Contains(P.IdProducto)
                                select P.Cantidad).Sum();

                Composicion comp = componente.componentes.First<Composicion>();

                DetalleComponentePromocion det = new DetalleComponentePromocion();
                det.CantidadProducto = int.Parse(Cantidad.ToString());
                det.CantidadPromo = int.Parse(comp.Cantidad);
                det.Componete = comp;
                det.ProductoComponente = comp.objProductoHijo;
                det.PresentacionComponete = comp.objPresentacion;

                
                _DetallesComponentes.Add(det);
            }
         
        }

        private void GenerarComponentesPromocionPosibles(Producto Promo)
        {
            _DetallesComponentes = new List<DetalleComponentePromocion>();

            var GrupoComponentesPromo = from G in Promo.ColComposiciones
                                        where G.TipoComposicion == "C"
                                        group G by G.Grupo into c
                                        select new { Grupo = c.Key, componentes = c };

            foreach (var componente in GrupoComponentesPromo)
            {

                List<long> idsPre = (from Pre in componente.componentes
                                     where Pre.Presentacion.HasValue
                                     select Pre.Presentacion.Value).ToList<long>();

                List<long> idsComp = (from Comp in componente.componentes
                                      select Comp.ComponenteHijo.Value).ToList<long>();

                //decimal Cantidad = (from P in _currentPedidoTemporal
                //                    where idsPre.Contains(P.IdPresentacion)
                //                    && idsComp.Contains(P.IdProducto)
                //                    select P.Cantidad).Sum();



                var pedidosTempPromo = from P in _currentPedidoTemporal
                                       where idsPre.Contains(P.IdPresentacion)
                                       && idsComp.Contains(P.IdProducto)
                                       && P.Cantidad > 0
                                       select P;


                foreach (PedidoTemporal pedTem in pedidosTempPromo)
                {
                    Composicion comp = componente.componentes.First<Composicion>();
                    decimal ProductosPromociones = int.Parse(comp.Cantidad);

                    DetalleComponentePromocion det = new DetalleComponentePromocion();
                    det.CantidadProducto = Convert.ToInt32(ProductosPromociones - pedTem.Cantidad);
                    det.CantidadPromo = int.Parse(comp.Cantidad);
                    det.Componete = comp;
                    det.ProductoComponente = comp.objProductoHijo;
                    det.PresentacionComponete = comp.objPresentacion;

                    pedTem.Cantidad = Convert.ToInt32(pedTem.Cantidad - ProductosPromociones);

                    _DetallesComponentes.Add(det);


                }


                
            }

        }

        /// <summary>
        /// Genero un lista compuesta con por clases temporales
        /// de pedido para poder alterar los pedido y calcular las promociones
        /// </summary>
        private void GenerarPedidoTemporal(List<DetallePedido> CurrentPedido)
        {
            _currentPedidoTemporal = new List<PedidoTemporal>();

            foreach (DetallePedido ped in CurrentPedido)
            {
                string a =  ped.Tipo;
                string descCompletoProducto = ped.DescripcionCompleta;
                PedidoTemporal pedtemporal = new PedidoTemporal(ped.Presentacion.Value, ped.Producto.Value, ped.IdPadre, Convert.ToDecimal(ped.Cantidad), descCompletoProducto, ped.Tipo);
                _currentPedidoTemporal.Add(pedtemporal);
            }
        }

        /// <summary>
        /// Actualiza el pedido temporal con las cantiadades
        /// de las promociones que se van calculando.
        /// </summary>
        private void ActualizarPedidoTemporalPromosPosibles(Producto CurrentPromo)
        {
            int hayPromoGeneradas = (from P in _currentPedidoTemporal
                                     where P.IdProducto == CurrentPromo.IdProducto
                                     select P).Count();

            if (hayPromoGeneradas>0)
            {
                var GrupoComponentesPromo = from G in CurrentPromo.ColComposiciones
                                            where G.TipoComposicion == "C"
                                            group G by G.Grupo into c
                                            select new { Grupo = c.Key, componentes = c };

                foreach (var componente in GrupoComponentesPromo)
                {
                    Composicion comp = componente.componentes.First<Composicion>();
                    //decimal ProductosPromociones = int.Parse(comp.Cantidad) * _cantidadPromociones;
                    decimal ProductosPromociones = int.Parse(comp.Cantidad);

                    List<long> idsPre = (from Pre in componente.componentes
                                         select Pre.Presentacion.Value).ToList<long>();

                    List<long> idsComp = (from Comp in componente.componentes
                                          select Comp.ComponenteHijo.Value).ToList<long>();

                    var pedidosTempPromo = from P in _currentPedidoTemporal
                                           where idsPre.Contains(P.IdPresentacion)
                                           && idsComp.Contains(P.IdProducto)
                                           select P;

                    foreach (PedidoTemporal pedTem in pedidosTempPromo)
                    {
                        if (pedTem.Cantidad > 0)
                        {
                            pedTem.Cantidad -= hayPromoGeneradas * ProductosPromociones;
                            ProductosPromociones = 0;
                        }

                        if (ProductosPromociones == 0)
                            break;
                    }
                }
            }

        }

        /// <summary>
        /// Actualiza el pedido temporal con las cantiadades
        /// de las promociones que se van calculando.
        /// </summary>
        private List<string> ActualizarPedidoTemporal(Producto CurrentPromo,List<Producto> AllPromos)
        {
            List<string> PoductosUtilizados = new List<string>();

            var GrupoComponentesPromo = from G in CurrentPromo.ColComposiciones
                                        where G.TipoComposicion == "C"
                                        group G by G.Grupo into c
                                        select new { Grupo = c.Key, componentes = c };


            decimal ProductosPromociones = 0;

            ///// Ordeno los productos pedidos segun el peso, el cual esta determinado
            ///// por la cantidad de promociones en la que participa.
            //foreach (PedidoTemporal item in _currentPedidoTemporal.Where(w => w.Cantidad>0).ToList())
            //{
            //    var peso = (from p in AllPromos
            //             where p.ColComposiciones.Any(w => w.TipoComposicion == "C" && w.objPresentacion.IdPresentacion == item.IdPresentacion)
            //             select p).Distinct().Count();

            //    item.Peso = peso;
            //}


            foreach (var componente in GrupoComponentesPromo)
            {
                Composicion comp = componente.componentes.First<Composicion>();
                ProductosPromociones = int.Parse(comp.Cantidad);


                List<long> idsPre = (from Pre in componente.componentes
                                     select Pre.Presentacion.Value).ToList<long>();

                List<long> idsComp = (from Comp in componente.componentes
                                      select Comp.ComponenteHijo.Value).ToList<long>();

                var pedidosTempPromo = from P in _currentPedidoTemporal ///.OrderBy(w => w.Peso)
                                       where idsPre.Contains(P.IdPresentacion)
                                       && idsComp.Contains(P.IdProducto)
                                       select P;

                foreach (PedidoTemporal pedTem in pedidosTempPromo)
                {
                    if (pedTem.Cantidad > 0 && pedTem.Cantidad < ProductosPromociones)
                    {
                        PoductosUtilizados.Add(pedTem.Cantidad.ToString() + "|" + pedTem.DescProducto);
                        ProductosPromociones -= pedTem.Cantidad;
                        pedTem.Cantidad = 0;
                    }
                    else if (pedTem.Cantidad > 0)
                    {
                        PoductosUtilizados.Add(ProductosPromociones.ToString() + "|" + pedTem.DescProducto);
                        pedTem.Cantidad -= ProductosPromociones;
                        ProductosPromociones = 0;
                    }

                    if (ProductosPromociones == 0)
                        break;
                }
            }

            return PoductosUtilizados;
        }


        public List<DetallePedido> GenerarPromocionesUnaxPedido(List<DetallePedido> CurrentPedido, List<Producto> Promos, List<Producto> promosExcluyentes)
        {
            _promos = Promos;
            List<DetallePedido> promosGeneradas = new List<DetallePedido>();
            GenerarPedidoTemporal(CurrentPedido);

            foreach (Producto promoExcluyente in promosExcluyentes)
            {
                ActualizarPedidoTemporalPromosPosibles(promoExcluyente);
        		 
            }
            

            foreach (Producto promo in _promos)
            {
                
                GenerarComponentesPromocion(promo);
                CalcularCantidadPromociones();

                if (_cantidadPromociones > 0)
                {
                  

                    for (int i = 0; i < _cantidadPromociones; i++)
                    {
                        List<string> ProductosUtilizados = ActualizarPedidoTemporal(promo, _promos);


                        var composicionRegalo = from R in promo.ColComposiciones
                                                where R.TipoComposicion == "O"
                                                group R by R.Grupo into c
                                                select new { Grupo = c.Key, componentes = c };


                        Presentacion presentacionRegalo = null;
                        List<Producto> productosRegalo = null;
                        DetallePedido detPedido = new DetallePedido();

                        if (composicionRegalo.Count() > 0)
                        {
                            foreach (var componente in composicionRegalo)
                            {
                                presentacionRegalo = composicionRegalo.First().componentes.First<Composicion>().objPresentacion;

                                productosRegalo = (from P in composicionRegalo.First().componentes
                                                   select P.objProductoHijo).ToList<Producto>();


                                DetalleRegalos newRegalo = new DetalleRegalos();

                                if (componente.componentes.Count() == 1)
                                {
                                    newRegalo.DescripcionRegalo = componente.componentes.First().objProductoHijo.Descripcion + " x " + componente.componentes.First().objPresentacion.Descripcion;
                                    newRegalo.IdPresentacionRegaloSeleccionado = componente.componentes.First().objPresentacion.IdPresentacion;
                                    newRegalo.TipoRegalo = "Producto";

                                }
                                else
                                {
                                    List<Producto> productos = (from P in componente.componentes
                                                                select P.objProductoHijo).ToList<Producto>();

                                    newRegalo.DescripcionRegalo = Helper.ObtenerDescripcionCompletaProductoEnComun(productos);
                                    newRegalo.IdPresentacionRegaloSeleccionado = 0;
                                    newRegalo.TipoRegalo = "Producto";

                                }

                                newRegalo.objDetallePedido = detPedido;
                                newRegalo.Grupo = componente.Grupo.Value;
                                detPedido.ColRegalos.Add(newRegalo);

                            }
                        }
                        else
                        {

                            DetalleRegalos newRegalo = new DetalleRegalos();
                            newRegalo.DescripcionRegalo = "Un descuento de $" + promo.Precio.ToString(); 
                            newRegalo.IdPresentacionRegaloSeleccionado = -1;
                            newRegalo.TipoRegalo = "Descuento";
                            newRegalo.objDetallePedido = detPedido;
                            newRegalo.Grupo = 0;
                            detPedido.ColRegalos.Add(newRegalo);

                        }

                        
                        detPedido.Cantidad = 1;
                        detPedido.Producto = promo.IdProducto;
                        detPedido.Presentacion = promo.ColPresentaciones[0].IdPresentacion;
                        detPedido.ProductoDesc = promo.Descripcion;
                        detPedido.PresentacionDesc = promo.ColPresentaciones[0].Descripcion;
                        detPedido.DescripcionCompleta = detPedido.ProductoDesc;
                        detPedido.DescProductosUtilizados = ProductosUtilizados;
                        detPedido.CodigoCompleto = promo.ColPresentaciones[0].Codigo;
                        detPedido.Tipo = "A";
                        detPedido.RegaloDesc = "Un Descuento de $" + promo.Precio.ToString();
                        detPedido.IdRegaloPresentacion = -1;
                        detPedido.IdRegaloSeleccionado = -1;
                        detPedido.IdPadre = promo.objPadre.IdProducto;
                        detPedido.ValorTotal = promo.ColPresentaciones[0].Precio;

                        promosGeneradas.Add(detPedido);
   
                        ///Salgo ya que solo tengo que generara
                        ///una sola promoción por mas que haya para generar
                        ///mas de una.
                        break;
                    }

                }
              
            }

            return promosGeneradas;
        }


        /// <summary>
        /// Generación de los pedido provocados por las promociones.
        /// </summary>
        /// <returns></returns>
        public List<DetallePedido> GenerarPromociones(List<DetallePedido> CurrentPedido, List<Producto> Promos, bool UtilizaRegalos)
        {
            _promos = Promos;
            List<DetallePedido> promosGeneradas = new List<DetallePedido>();
            GenerarPedidoTemporal(CurrentPedido);

            foreach (Producto promo in _promos)
            {
                GenerarComponentesPromocion(promo);
                CalcularCantidadPromociones();

                if (_cantidadPromociones > 0)
                {
                  

                    for (int i = 0; i < _cantidadPromociones; i++)
                    {
                        List<string> ProductosUtilizados = ActualizarPedidoTemporal(promo,_promos);


                        var composicionRegalo = from R in promo.ColComposiciones
                                                where R.TipoComposicion == "O"
                                                group R by R.Grupo into c
                                                select new { Grupo = c.Key, componentes = c };


                        Presentacion presentacionRegalo = null;
                        List<Producto> productosRegalo = null;
                        DetallePedido detPedido = new DetallePedido();

                        if (composicionRegalo.Count() > 0)
                        {
                            foreach (var componente in composicionRegalo)
                            {
                                presentacionRegalo = composicionRegalo.First().componentes.First<Composicion>().objPresentacion;

                                productosRegalo = (from P in composicionRegalo.First().componentes
                                                   select P.objProductoHijo).ToList<Producto>();


                                DetalleRegalos newRegalo = new DetalleRegalos();

                                if (componente.componentes.Count() == 1)
                                {
                                    if (!componente.componentes.First().objProductoHijo.Descripcion.Contains("x Unidad"))
                                        newRegalo.DescripcionRegalo = componente.componentes.First().objProductoHijo.Descripcion + " x " + componente.componentes.First().objPresentacion.Descripcion;
                                    else
                                        newRegalo.DescripcionRegalo = componente.componentes.First().objProductoHijo.Descripcion;

                                    newRegalo.IdPresentacionRegaloSeleccionado = componente.componentes.First().objPresentacion.IdPresentacion;
                                    newRegalo.TipoRegalo = "Producto";

                                }
                                else
                                {
                                    List<Producto> productos = (from P in componente.componentes
                                                                select P.objProductoHijo).ToList<Producto>();

                                    newRegalo.DescripcionRegalo = Helper.ObtenerDescripcionCompletaProductoEnComun(productos);
                                    newRegalo.IdPresentacionRegaloSeleccionado = 0;
                                    newRegalo.TipoRegalo = "Producto";

                                }

                                newRegalo.objDetallePedido = detPedido;
                                newRegalo.Grupo = componente.Grupo.Value;
                                detPedido.ColRegalos.Add(newRegalo);

                            }
                        }
                        else
                        {

                            DetalleRegalos newRegalo = new DetalleRegalos();
                            newRegalo.DescripcionRegalo = "Un descuento de $" + promo.Precio.ToString(); 
                            newRegalo.IdPresentacionRegaloSeleccionado = -1;
                            newRegalo.TipoRegalo = "Descuento";
                            newRegalo.objDetallePedido = detPedido;
                            newRegalo.Grupo = 0;
                            detPedido.ColRegalos.Add(newRegalo);
                            detPedido.ValorTotal = promo.Precio.Value ;
                        }

                        
                        detPedido.Cantidad = 1;
                        detPedido.Producto = promo.IdProducto;
                        detPedido.Presentacion = promo.ColPresentaciones[0].IdPresentacion;
                        detPedido.ProductoDesc = promo.Descripcion;
                        detPedido.PresentacionDesc = promo.ColPresentaciones[0].Descripcion;
                        detPedido.DescripcionCompleta = detPedido.ProductoDesc;
                        detPedido.DescProductosUtilizados = ProductosUtilizados;
                        detPedido.CodigoCompleto = promo.ColPresentaciones[0].Codigo;


                        /// IMPORTANTE: ESTE CAMBIO ES APLICABLE DESPUES
                        /// DE HABLAR CON RAUL
                        //detPedido.ValorTotal = promo.Precio.Value;


                        int HayPromosComoCompomentes = (from C in promo.ColComposiciones
                                                        where C.objProductoHijo.Tipo == 'P'
                                                        select C).Count();

                        if (HayPromosComoCompomentes>0)
                            detPedido.Tipo = "P";
                        else
                            detPedido.Tipo = "A";


                        if (UtilizaRegalos)
                        {
                            detPedido.IdRegaloPresentacion = presentacionRegalo.IdPresentacion;
                           
                            if (productosRegalo.Count == 1)
                            {
                                detPedido.RegaloDesc = productosRegalo[0].DescripcionCompleta + presentacionRegalo.Descripcion;
                                detPedido.IdRegaloSeleccionado = presentacionRegalo.objProducto.IdProducto;
                            }
                            else
                            {
                                detPedido.RegaloDesc = Helper.ObtenerDescripcionCompletaProductoEnComun(productosRegalo) + " x " + presentacionRegalo.Descripcion;
                            }
                        }
                        else
                        {
                            /// Para el caso de promociones que no llevan regalo
                            /// Muestro como descripcion del regalo el descuento que se hace por la 
                            /// promoción y utilizo el valor -1 para indicar esto.
                            detPedido.RegaloDesc = "Un Descuento de $" + promo.Precio.ToString();
                            detPedido.IdRegaloPresentacion = -1;
                            detPedido.IdRegaloSeleccionado = -1;

                        }
                       

                        detPedido.IdPadre = promo.objPadre.IdProducto;

                                            
                        



                        promosGeneradas.Add(detPedido);


                        /// se agrega la promoción como un nuevo pedido
                        /// para poder calcular las promociones combinadas
                        string descCompletoProducto = detPedido.DescripcionCompleta ;
                        PedidoTemporal pedtemporal = new PedidoTemporal(detPedido.Presentacion.Value, detPedido.Producto.Value, 0, Convert.ToDecimal(detPedido.Cantidad), descCompletoProducto, detPedido.Tipo);
                        _currentPedidoTemporal.Add(pedtemporal);

                    }

                }
              
            }

            return promosGeneradas;
        }


        public Hashtable GenerarPromocionesPosibles(List<DetallePedido> CurrentPedido, List<Producto> Promos)
        {
            Hashtable PromosPosibles = new Hashtable();
            GenerarPedidoTemporal(CurrentPedido);

            foreach (Producto promo in Promos)
            {
                ActualizarPedidoTemporalPromosPosibles(promo);
            }

            foreach (Producto promo in Promos)
            {
                int productosFaltantes = 0;
                List<DetallePromoPosible> ListaProductosFaltante = new List<DetallePromoPosible>();
                GenerarComponentesPromocion(promo);
                
                foreach (DetalleComponentePromocion det in _DetallesComponentes)
                {
                    /// Esta línea fue comentada ya que como componente 
                    /// de la promoción actual, puede ser un componente de tipo promoción
                    //if (det.ProductoComponente.Tipo != 'P')
                    //{
                        if (det.CantidadProducto < det.CantidadPromo)
                        {
                            productosFaltantes += det.CantidadPromo - det.CantidadProducto;
                            
                            DetallePromoPosible detPromoPosible = new DetallePromoPosible();
                            detPromoPosible.Producto = det.ProductoComponente;
                            detPromoPosible.Presentacion = det.PresentacionComponete;
                            detPromoPosible.Cantidad = productosFaltantes;
                            detPromoPosible.Promo = promo;


                            ListaProductosFaltante.Add(detPromoPosible);
                        }
                    //}
                }

                /// 1: tiene que ser una variable que indique hasta cuantos
                /// productos faltantes son validos para proponer la compra de los mismos
                /// y asi acceder a la promoción.
                if (productosFaltantes == 1 ) ///&& hayProductosPedidos)
                {
                    /// Antes de Incluir la promoción como promo final debo controlar
                    /// que si: ya tengo ganada la esta promo 
                    /// y la cantida de grupos requeridos es uno y la cantidad requerida es 1 producto
                    /// entonces no debo tomar la promo como válida.

                    int HayPromosGanadas = (from P in _currentPedidoTemporal
                                            where P.IdProducto == promo.IdProducto
                                            select P).Count();

                    int GruposReqeridos = (from P in promo.ColComposiciones
                                          where P.TipoComposicion == "C"
                                          select P.Grupo.Value).Distinct().Count();

                    string CantidadReqerida = (from P in promo.ColComposiciones
                                            where  P.TipoComposicion == "C"
                                            select P.Cantidad).Max<string>();


                    if (
                        (HayPromosGanadas == 0 && GruposReqeridos > 1 && int.Parse(CantidadReqerida) >= 1)
                        ||
                        (HayPromosGanadas > 0 && GruposReqeridos > 1 && int.Parse(CantidadReqerida) >= 1)
                        ||
                        (GruposReqeridos == 1 && int.Parse(CantidadReqerida) > 1)
                        )
                        PromosPosibles.Add(promo, ListaProductosFaltante);

                }
            }

            return PromosPosibles;
        }

        private class DetalleComponentePromocion
        {
            /// <summary>
            /// Cantidad de productos validos para un componente de la promocion
            /// </summary>
            private int _CantidadProducto = 0;

            /// <summary>
            /// Cantidad de Productos del componente de la promocion
            /// </summary>
            private int _CantidadPromo = 0;

            /// <summary>
            /// Objeto de la composicion
            /// </summary>
            private Composicion _componente;

            /// <summary>
            /// Producto del componente de la promocion
            /// </summary>
            private Producto _producto;


            /// <summary>
            /// Presentación del componente de la promocion
            /// </summary>
            private Presentacion _presentacion;





            public DetalleComponentePromocion()
            {

            }

            /// <summary>
            ///  Cantidad de Productos disponibles que tengo en la solicitud para uno de los componentes
            ///  de la promoción
            /// </summary>
            public int CantidadProducto
            {
                get { return _CantidadProducto; }
                set { _CantidadProducto = value; }
            }

            /// <summary>
            /// Cantidad de productos requeridos para uno de los componentes
            ///  de la promoción
            /// </summary>
            public int CantidadPromo
            {
                get { return _CantidadPromo; }
                set { _CantidadPromo = value; }
            }
            public Composicion Componete
            {
                get { return _componente; }
                set { _componente = value; }
            }
            public Producto ProductoComponente
            {
                get { return _producto; }
                set { _producto = value; }
            }
            public Presentacion PresentacionComponete
            {
                get { return _presentacion; }
                set { _presentacion = value; }
            }

            /// <summary>
            /// Cantidad de componentes de la promocion que se puede obtener a partir de la cantidad
            /// de productos. 
            /// </summary>
            public int CantidadPromoIndividual
            {
                get
                {
                    int cantidad =   Convert.ToInt32(CantidadProducto / CantidadPromo);

                    return cantidad;
                }

            }

            /// <summary>
            /// Descuenta la cantidad de productos solicitados para no tenerlos en cuenta en los
            /// proximos calculos de promociones.
            /// </summary>
            public void ActualizarCantidadPromoIndividual(int CantidadActualizar)
            {
                _CantidadProducto -= CantidadPromoIndividual * _CantidadPromo;
            }


        }

        private class PedidoTemporal
        {
           


            private long  _IdPresentacion = 0;
            private long _IdProducto = 0;
            private long _IdPadre = 0;
            private decimal _Cantidad = 0;
            private string _descProducto = "";
            private string _tipo = "";



            public PedidoTemporal(long IdPresentacion, long IdProducto, long IdPadre, decimal Cantidad, string DescProducto, string Tipo)
            {
                _IdPresentacion = IdPresentacion;
                _IdProducto = IdProducto;
                _IdPadre = IdPadre;
                _Cantidad = Cantidad;
                _descProducto = DescProducto;
                _tipo = Tipo;

            }

            public long Peso
            { get; set; }

            public string Tipo
            {
                get { return _tipo; }
                set { _tipo = value; }
            }


            public string DescProducto
            {
                get { return _descProducto; }
                set { _descProducto = value; }
            }


            public long IdPresentacion
            {
                get { return _IdPresentacion; }
                set { _IdPresentacion = value; }
            }
            public long IdProducto
            {
                get { return _IdProducto; }
                set { _IdProducto = value; }
            }
            public long IdPadre
            {
                get { return _IdPadre; }
                set { _IdPadre = value; }
            }
            public decimal Cantidad
            {
                get { return _Cantidad; }
                set { _Cantidad = value; }
            }
            
          


        }
    }
}