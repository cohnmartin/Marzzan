using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Services;
using CommonMarzzan;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for ServiceActualizacionClientes
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ServiceActualizacionClientes : System.Web.Services.WebService
{
    private System.Web.UI.Timer TimerPrecesamiento = new System.Web.UI.Timer();

    public ServiceActualizacionClientes()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public void ActualizacionDireccionesClientes(List<Direccione> direcciones)
    {
         Marzzan_InfolegacyDataContext db = new Marzzan_InfolegacyDataContext();

         foreach (CommonMarzzan.Direccione item in direcciones)
         {
             CommonMarzzan.Direccione direccionService = (from C in db.Direcciones
                                                     where C.CodigoExternoDir == item.CodigoExternoDir
                                                     select C).FirstOrDefault<CommonMarzzan.Direccione>();

             if (direccionService != null)
             {
                 direccionService.Provincia = item.Provincia;
                 direccionService.Localidad = item.Localidad;
                 direccionService.Departamento = "";
                 direccionService.CodigoExternoDir = item.CodigoExternoDir;
                 direccionService.CodigoPostal = item.CodigoPostal;
                 direccionService.Calle = item.Calle;
             }
         }

         db.SubmitChanges();
         db.Connection.Close();
    }
    
    [WebMethod]
    public void CreacionDireccionesClientes(List<Direccione> direcciones)
    {
        Marzzan_InfolegacyDataContext db = new Marzzan_InfolegacyDataContext();
        List<Direccione> direccionesExistente = new List<Direccione>();

        var direccionesNueva = from D in direcciones
                               where D != null
                               select D;

        foreach (CommonMarzzan.Direccione item in direccionesNueva)
        {
            if (item != null)
            {

                Cliente cli = (from c in db.Clientes
                               where c.IdCliente == item.Cliente
                               select c).FirstOrDefault();

                if (cli != null)
                {
                    /// Verifico si la direccion que se esta intentando crear
                    /// realmento no existe en la base de datos, si existe 
                    /// la guardo para realizar la actualización y no la creación.
                    CommonMarzzan.Direccione direccionExistente = (from C in db.Direcciones
                                                                 where C.CodigoExternoDir == item.CodigoExternoDir
                                                                 select C).FirstOrDefault<CommonMarzzan.Direccione>();

                    if (direccionExistente == null)
                    {

                        CommonMarzzan.Direccione clienteServive = new CommonMarzzan.Direccione();

                        clienteServive.Provincia = item.Provincia;
                        clienteServive.Localidad = item.Localidad;
                        clienteServive.Departamento = "";
                        clienteServive.CodigoExternoDir = item.CodigoExternoDir;
                        clienteServive.CodigoPostal = item.CodigoPostal;
                        clienteServive.Calle = item.Calle;
                        clienteServive.EsPrincipal = true;
                        clienteServive.Cliente = item.Cliente;
                        clienteServive.CodigoExterno = item.CodigoExterno;

                        db.Direcciones.InsertOnSubmit(clienteServive);
                    }
                    else
                    {
                        direccionesExistente.Add(item);
                    }
                }
            }
        }

        db.SubmitChanges();
        this.ActualizacionDireccionesClientes(direccionesExistente);

        db.Connection.Close();

    }

    [WebMethod]
    public void CreacionClientes(List<Cliente> clientes)
    {
        Marzzan_InfolegacyDataContext db = new Marzzan_InfolegacyDataContext();

        foreach (CommonMarzzan.Cliente item in clientes)
        {
            CommonMarzzan.Cliente clienteServive = new CommonMarzzan.Cliente();

            clienteServive.CodigoExterno = item.CodigoExterno;
            clienteServive.Dni = item.Dni;
            clienteServive.Nombre = item.Nombre;
            clienteServive.Dni = item.Dni;
            clienteServive.CodDNI = item.CodDNI.ToString();
            clienteServive.Telefono = item.Telefono;
            clienteServive.Email = item.Email;
            clienteServive.TipoCliente = item.TipoCliente;
            clienteServive.CodTipoCliente = item.CodTipoCliente.ToString();
            clienteServive.Provincia = item.Provincia;
            clienteServive.CodProvincia = item.CodProvincia;
            clienteServive.Transporte = item.Transporte;
            clienteServive.CodTransporte = item.CodTransporte;
            clienteServive.Vendedor = item.Vendedor;
            clienteServive.CodVendedor = item.CodVendedor;
            clienteServive.Clasif1 = item.Clasif1;
            clienteServive.CodClasif1 = item.CodClasif1;
            clienteServive.CodZona = item.CodZona;
            clienteServive.Cod_SitIVA = item.Cod_SitIVA.ToString();
            clienteServive.Desc_SitIVA = item.Desc_SitIVA;
            clienteServive.Desc_CondVta = item.Desc_CondVta;
            clienteServive.Cod_CondVta = item.Cod_CondVta;
            clienteServive.PadreExterno = item.PadreExterno;
            clienteServive.CantIncSolicitadas = item.CantIncSolicitadas;
            clienteServive.SaldoCtaCte = item.SaldoCtaCte;
            clienteServive.FechaNacimiento = item.FechaNacimiento;
            clienteServive.FechaAlta = item.FechaAlta;
            clienteServive.PoseeIncorporacion = item.PoseeIncorporacion;
            clienteServive.PoseeCartuchera = item.PoseeCartuchera;
            clienteServive.Habilitado = item.Habilitado;
            clienteServive.TipoConsultor = item.TipoConsultor;
            clienteServive.Login = item.Dni;

            clienteServive.Pass = "123";

            db.Clientes.InsertOnSubmit(clienteServive);
        }

        db.SubmitChanges();
        db.Connection.Close();

    }
    
    [WebMethod]
    public void ActualizacionClientes(List<Cliente> clientes)
    {
        Marzzan_InfolegacyDataContext db = new Marzzan_InfolegacyDataContext();

        foreach (CommonMarzzan.Cliente item in clientes)
        {

                CommonMarzzan.Cliente clienteServive = (from C in db.Clientes
                                                        where C.CodigoExterno == item.CodigoExterno
                                                        select C).FirstOrDefault<CommonMarzzan.Cliente>();

                if (clienteServive != null)
                {

                    clienteServive.Dni = item.Dni;
                    clienteServive.Nombre = item.Nombre;
                    clienteServive.Dni = item.Dni;
                    clienteServive.CodDNI = item.CodDNI.ToString();
                    clienteServive.Telefono = item.Telefono;
                    clienteServive.Email = item.Email;
                    clienteServive.TipoCliente = item.TipoCliente;
                    clienteServive.CodTipoCliente = item.CodTipoCliente.ToString();
                    clienteServive.Provincia = item.Provincia;
                    clienteServive.CodProvincia = item.CodProvincia;
                    clienteServive.Transporte = item.Transporte;
                    clienteServive.CodTransporte = item.CodTransporte;
                    clienteServive.Vendedor = item.Vendedor;
                    clienteServive.CodVendedor = item.CodVendedor;
                    clienteServive.Clasif1 = item.Clasif1;
                    clienteServive.CodClasif1 = item.CodClasif1;
                    clienteServive.CodZona = item.CodZona;
                    clienteServive.PadreExterno = item.PadreExterno;
                    clienteServive.CantIncSolicitadas = item.CantIncSolicitadas;
                    clienteServive.SaldoCtaCte = item.SaldoCtaCte;
                    clienteServive.FechaNacimiento = item.FechaNacimiento;
                    clienteServive.FechaAlta = item.FechaAlta;
                    clienteServive.PoseeIncorporacion = item.PoseeIncorporacion;
                    clienteServive.PoseeCartuchera = item.PoseeCartuchera;
                    clienteServive.Habilitado = item.Habilitado;
                    clienteServive.TipoConsultor = item.TipoConsultor;
                    clienteServive.UltimaActualizacion = DateTime.Now;

                }
                else
                {
                    clienteServive = new CommonMarzzan.Cliente();
                    clienteServive.CodigoExterno = item.CodigoExterno;
                    clienteServive.Dni = item.Dni;
                    clienteServive.Nombre = item.Nombre;
                    clienteServive.Dni = item.Dni;
                    clienteServive.CodDNI = item.CodDNI.ToString();
                    clienteServive.Telefono = item.Telefono;
                    clienteServive.Email = item.Email;
                    clienteServive.TipoCliente = item.TipoCliente;
                    clienteServive.CodTipoCliente = item.CodTipoCliente.ToString();
                    clienteServive.Provincia = item.Provincia;
                    clienteServive.CodProvincia = item.CodProvincia;
                    clienteServive.Transporte = item.Transporte;
                    clienteServive.CodTransporte = item.CodTransporte;
                    clienteServive.Vendedor = item.Vendedor;
                    clienteServive.CodVendedor = item.CodVendedor;
                    clienteServive.Clasif1 = item.Clasif1;
                    clienteServive.CodClasif1 = item.CodClasif1;
                    clienteServive.CodZona = item.CodZona;
                    clienteServive.Cod_SitIVA = item.Cod_SitIVA.ToString();
                    clienteServive.Desc_SitIVA = item.Desc_SitIVA;
                    clienteServive.Desc_CondVta = item.Desc_CondVta;
                    clienteServive.Cod_CondVta = item.Cod_CondVta;
                    clienteServive.PadreExterno = item.PadreExterno;
                    clienteServive.CantIncSolicitadas = item.CantIncSolicitadas;
                    clienteServive.SaldoCtaCte = item.SaldoCtaCte;
                    clienteServive.FechaNacimiento = item.FechaNacimiento;
                    clienteServive.PoseeIncorporacion = item.PoseeIncorporacion;
                    clienteServive.Habilitado = item.Habilitado;
                    clienteServive.PoseeCartuchera = item.PoseeCartuchera;
                    clienteServive.TipoConsultor = item.TipoConsultor;
                    clienteServive.Login = item.Dni;
                    clienteServive.Pass = "123";

                    db.Clientes.InsertOnSubmit(clienteServive);
                }

        }

        db.SubmitChanges();
        db.Connection.Close();
    }

    [WebMethod]
    public string ActualizacionJerarquias()
    {

        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
        int countChange = 0;

        List<CommonMarzzan.Cliente> coor = (from C in dc.Clientes
                              where C.PadreExterno == C.CodigoExterno && C.PadreExterno != null
                                            select C).ToList<CommonMarzzan.Cliente>();

        foreach (CommonMarzzan.Cliente item in coor)
        {

            List<CommonMarzzan.Cliente> consultores = (from C in dc.Clientes
                                         where C.PadreExterno == item.CodigoExterno
                                                       select C).ToList<CommonMarzzan.Cliente>();

            foreach (CommonMarzzan.Cliente consul in consultores)
            {
                if (consul.objPadre != item)
                {
                    consul.objPadre = item;
                    countChange++;
                }
            }

            
        }

        dc.SubmitChanges();
        dc.Connection.Close();

        return coor.Count.ToString();
    }

    [WebMethod]
    public void ActualizacionProducto(List<Producto> productos)
    {

        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        foreach (CommonMarzzan.Producto item in productos)
        {
            CommonMarzzan.Producto prod = (from C in dc.Productos
                             where C.IdProducto == item.IdProducto
                                           select C).First<CommonMarzzan.Producto>();

            if (prod != null)
            {
                prod.Descripcion = item.Descripcion;
                prod.Precio = Convert.ToDecimal(item.Precio.ToString()); 
            }
        }

        dc.SubmitChanges();
        dc.Connection.Close();
    }

    [WebMethod]
    public void ActualizacionPresentacionesPromosyOtros(List<Presentacion> presentaciones)
    {

        Marzzan_InfolegacyDataContext dbLocal = new Marzzan_InfolegacyDataContext();

        foreach (Presentacion item in presentaciones)
        {
            Presentacion pre = (from C in dbLocal.Presentacions
                                where C.Codigo.Replace("-", "").Replace(" ", "").Trim() == item.Codigo.Replace("-", "").Replace(" ", "").Trim()
                                select C).FirstOrDefault<Presentacion>();

            if (pre != null)
            {
                pre.Descripcion = item.Descripcion;
                pre.Precio = Convert.ToDecimal(item.Precio.ToString());
                pre.objProducto.Precio = Convert.ToDecimal(item.Precio.ToString());
                pre.CodigoBarras = item.CodigoBarras;
            }
        }

        dbLocal.SubmitChanges();
        dbLocal.Connection.Close();
    }


    [WebMethod]
    public void ActualizacionPPresentaciones(List<Presentacion> presentaciones)
    {

        Marzzan_InfolegacyDataContext dbLocal = new Marzzan_InfolegacyDataContext();

        foreach (Presentacion item in presentaciones)
        {
            try
            {

                Presentacion pre = (from C in dbLocal.Presentacions
                                    where C.Codigo.Replace("-", "").Replace(" ", "").Trim() == item.Codigo.Replace("-", "").Replace(" ", "").Trim()
                                    select C).FirstOrDefault<Presentacion>();


                if (pre != null)
                {
                    /// Tengo que buscar a ver si la fragancia es la misma que la acutal
                    /// si es asi: no sigo controlando, si no: tengo que buscar
                    /// la fragancia nueva donde esta ubicada, teniendo en cuenta la linea y Und Neg
                    string[] DescNuevas = item.Descripcion.Split('|');

                    string CodLinea = DescNuevas[5].Trim();
                    string CodFragancia = DescNuevas[4].Trim();
                    string Fragancia = DescNuevas[3].Trim();
                    string Linea = DescNuevas[2].Trim();
                    string UndNeg = DescNuevas[1].Trim();
                    string Descripcion = DescNuevas[0].Trim();




                    if (pre.objProducto.Descripcion != Fragancia ||
                        pre.objProducto.objPadre.Descripcion != Linea ||
                        pre.objProducto.objPadre.objPadre.Descripcion != UndNeg)
                    {
                        Producto NuevoPadre = (from P in dbLocal.Productos
                                               where P.Descripcion == Fragancia
                                               && P.objPadre.Descripcion == Linea
                                               && P.objPadre.objPadre.Descripcion == UndNeg
                                               select P).SingleOrDefault<Producto>();

                        if (NuevoPadre == null)
                        {
                            Producto pUndNeg = (from P in dbLocal.Productos
                                                where P.Nivel == 1 && P.Descripcion == UndNeg
                                                select P).SingleOrDefault<Producto>();

                            if (pUndNeg != null)
                            {

                                Producto pLiena = (from P in pUndNeg.ColHijos
                                                   where P.Descripcion == Linea
                                                   select P).SingleOrDefault<Producto>();

                                if (pLiena != null)
                                {
                                    Producto pFragancia = (from P in pLiena.ColHijos
                                                           where P.Descripcion == Fragancia
                                                           select P).SingleOrDefault<Producto>();

                                    if (pFragancia == null)
                                    {
                                        Producto newFragancia = new Producto();
                                        newFragancia.Codigo = CodFragancia;
                                        newFragancia.Descripcion = Fragancia;
                                        newFragancia.EsUltimoNivel = true;
                                        newFragancia.Nivel = 3;
                                        newFragancia.objPadre = pLiena;
                                        newFragancia.Precio = Convert.ToDecimal(item.Precio);
                                        newFragancia.Tipo = 'A';
                                        dbLocal.Productos.InsertOnSubmit(newFragancia);


                                        //finalmente la asocio a la presentacion en cuestion
                                        pre.objProducto = newFragancia;
                                    }
                                }
                                else
                                {
                                    Producto newLinea = new Producto();
                                    newLinea.Codigo = CodLinea;
                                    newLinea.Descripcion = Linea;
                                    newLinea.EsUltimoNivel = true;
                                    newLinea.Nivel = 2;
                                    newLinea.objPadre = pUndNeg;
                                    newLinea.Precio = Convert.ToDecimal(item.Precio);
                                    newLinea.Tipo = 'A';
                                    dbLocal.Productos.InsertOnSubmit(newLinea);


                                    Producto newFragancia = new Producto();
                                    newFragancia.Codigo = CodFragancia;
                                    newFragancia.Descripcion = Fragancia;
                                    newFragancia.EsUltimoNivel = true;
                                    newFragancia.Nivel = 3;
                                    newFragancia.objPadre = newLinea;
                                    newFragancia.Precio = Convert.ToDecimal(item.Precio);
                                    newFragancia.Tipo = 'A';

                                    //finalmente la asocio a la presentacion en cuestion
                                    pre.objProducto = newFragancia;

                                }
                            }
                        }
                        else
                        {
                            pre.objProducto = NuevoPadre;
                            NuevoPadre.Codigo = CodFragancia;
                        }

                    }


                    pre.Descripcion = Descripcion;
                    pre.Precio = Convert.ToDecimal(item.Precio.ToString());
                    pre.Codigo = item.Codigo;
                    pre.Activo = item.Activo;
                }
            }
            catch { }


        }

        dbLocal.SubmitChanges();
        dbLocal.Connection.Close();
       
    }
    
    [WebMethod]
    public void CreacionProducto(List<View_Producto> productos)
    {
        Marzzan_InfolegacyDataContext dbLocal = new Marzzan_InfolegacyDataContext();
        List<Producto> productosLocales = dbLocal.Productos.ToList();

        foreach (View_Producto item in productos)
        {
            /// Teniendo cuales son los nuevos productos
            /// comienzo a buscar si la estructura jerarquica 
            /// existe en todo su extención, a medida que lo voy verificado
            /// si: no existe entonces creo el producto de ahi 
            /// hacia abajo incluido la presentación, si toda la estructura jerarquica
            /// existe entonces solo creo la preentación.

            bool creoNivel = false;
            string DescPadre = "";
            for (int nivel = 1; nivel < 4; nivel++)
            {
                string NivelaComparar = ObtenerValorNivel(item, nivel);
                int ProductosExistentes = (from P in productosLocales
                                           where P.Descripcion == NivelaComparar && (P.objPadre == null || (P.objPadre != null && P.objPadre.Descripcion == DescPadre))
                                           select P).Count();

                Producto newprod = null;
                if (ProductosExistentes == 0)
                {
                    /// tengo que crear todos los productos 
                    /// desde el nivel actual hasta el nivel 3

                    for (int NivelaCrear = nivel; NivelaCrear < 4; NivelaCrear++)
                    {

                        newprod = new Producto();
                        newprod.Nivel = NivelaCrear;
                        newprod.Descripcion = ObtenerValorNivel(item, NivelaCrear);
                        newprod.Tipo = 'A';
                        newprod.objPadre = (from P in productosLocales
                                            where P.Nivel == NivelaCrear - 1 && P.Descripcion == ObtenerValorNivel(item, NivelaCrear - 1) && (P.objPadre == null || P.objPadre.Descripcion == ObtenerValorNivel(item, NivelaCrear - 2))
                                            select P).Distinct<Producto>().FirstOrDefault<Producto>();
                        newprod.Image = "";
                        newprod.Precio = decimal.Parse(item.Precio.ToString());
                        newprod.Codigo = ObtenerCodigoNivel(item, NivelaCrear);

                        if (nivel == 3)
                            newprod.EsUltimoNivel = true;
                        else
                            newprod.EsUltimoNivel = false;


                        dbLocal.Productos.InsertOnSubmit(newprod);
                        productosLocales.Add(newprod);
                        creoNivel = true;
                    }

                    //controlado y creado todos lo niveles del producto
                    //creo la presentacion.
                    Presentacion newpres = new Presentacion();
                    newpres.Codigo = item.CodigoCompleto;
                    newpres.Descripcion = item.MEDIDA.Split('-')[1].Trim();
                    newpres.objProducto = newprod;
                    newpres.Precio = decimal.Parse(item.Precio.ToString());
                    newpres.Activo = true;

                    DescPadre = "";
                    break;
                }
                else
                {
                    DescPadre = NivelaComparar;
                }
            }

            if (!creoNivel)
            {

                Presentacion newpres = new Presentacion();
                newpres.Codigo = item.CodigoCompleto;
                newpres.Descripcion = item.MEDIDA.Split('-')[1].Trim();
                newpres.objProducto = (from dl in productosLocales
                                       where
                                       (
                                               dl.Nivel == 3 && dl.Descripcion == item.FRAGANCIA.Split('-')[1].Trim()
                                               && dl.objPadre.Codigo == item.LINEA.Split('-')[0].Trim()
                                               && dl.objPadre.objPadre.Codigo == item.UNIDAD_NEGOCIO.Split('-')[0].Trim()

                                        )
                                       select dl).First<CommonMarzzan.Producto>();

                newpres.Precio = decimal.Parse(item.Precio.ToString());
                newpres.Activo = true;
                dbLocal.Presentacions.InsertOnSubmit(newpres);
                creoNivel = false;

            }


            dbLocal.SubmitChanges();

        }

        dbLocal.Connection.Close();
        
    }

    [WebMethod]
    public void CreacionPromosyOtros(List<View_PromosNivelesRegalo> PromosyOtros)
    {
        Marzzan_InfolegacyDataContext dbLocal = new Marzzan_InfolegacyDataContext();
        foreach (View_PromosNivelesRegalo item in PromosyOtros)
        {

            Producto prodPadre = null;
            Producto newP = null;

            try
            {
                prodPadre = (from P in dbLocal.Productos
                             where P.Padre == null && P.Codigo == item.TIPO_PRODUCTO.Split('-')[0].Trim()
                             select P).First<Producto>();
            }
            catch
            {
                prodPadre = new Producto();
                prodPadre.Codigo = item.TIPO_PRODUCTO.Split('-')[0].Trim();
                prodPadre.Descripcion = item.TIPO_PRODUCTO.Split('-')[1].Trim();
                prodPadre.EsUltimoNivel = false;
                prodPadre.Nivel = 1;
                prodPadre.Precio = decimal.Parse(item.Precio.ToString());
                prodPadre.Tipo = item.Tipo.Value;

                dbLocal.Productos.InsertOnSubmit(prodPadre);
                dbLocal.SubmitChanges();
            }


            newP = new Producto();
            newP.Codigo = item.TIPO_PRODUCTO.Split('-')[0].Trim();
            newP.Descripcion = item.Art_DescCompleta;
            newP.EsUltimoNivel = true;
            newP.Nivel = 2;
            newP.Precio = decimal.Parse(item.Precio.ToString());
            newP.Tipo = prodPadre.Tipo;
            prodPadre.ColHijos.Add(newP);


            Presentacion newPre = new Presentacion();
            newPre.Codigo = item.CodigoCompleto;
            newPre.Descripcion = item.Art_DescCompleta;
            newPre.CodigoBarras = item.CodigoBarra;
            newPre.objProducto = newP;
            

            newPre.Precio = decimal.Parse(item.Precio.ToString());

        }

        dbLocal.SubmitChanges();
    }


    [WebMethod]
    public  List<CommonMarzzan.CabeceraPedido> GetPedidosRealizados()
    {
            /// controlo si hay pedido pendientes de respuesta y cuanto tiempo lleva
            /// esperando la confirmación, si ha paso mas de 5 minutos entonces desmarco
            /// los pedido en proceso para que se vuelvan a inviar.
            /// 
       
            Marzzan_InfolegacyDataContext dbLocal = new Marzzan_InfolegacyDataContext();

            List<CommonMarzzan.CabeceraPedido> cabeceras = (from C in dbLocal.CabeceraPedidos
                                                            where C.EstadoEnvio == "Procesando"
                                                            && (C.EsTemporal == null || C.EsTemporal.Value == false)
                                                            select C).ToList<CommonMarzzan.CabeceraPedido>();

            if (cabeceras.Count > 0)
            {
               TimeSpan TStiempoTranscurrido = new TimeSpan(DateTime.Now.Ticks - cabeceras[cabeceras.Count - 1].FechaEnvio.Value.Ticks);
               if (TStiempoTranscurrido.Minutes > 5)
               {
                   foreach (CommonMarzzan.CabeceraPedido cab in cabeceras)
                   {
                       cab.EstadoEnvio = "";
                       cab.FechaEnvio = null;
                   }

                   dbLocal.SubmitChanges();
               }
            }


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Busco y genero el envio de los pedidos

            cabeceras = (from C in dbLocal.CabeceraPedidos
                         where (C.EstadoEnvio == "" || C.EstadoEnvio == null)
                         && (C.EsTemporal == null || C.EsTemporal.Value == false)
                          select C).ToList<CommonMarzzan.CabeceraPedido>();


            int CantidadPedidos = cabeceras.Count();
            int i = 0;
            List<CommonMarzzan.CabeceraPedido> Resultado = new List<CommonMarzzan.CabeceraPedido>();

            foreach (CommonMarzzan.CabeceraPedido itemC in cabeceras)
            {
                CommonMarzzan.CabeceraPedido ped = new CommonMarzzan.CabeceraPedido();
                ped.Cliente = itemC.objCliente.IdCliente;
                ped.DireccionEntrega = itemC.objDireccion.IdDireccion;
                ped.FechaPedido = itemC.FechaPedido;
                ped.MontoTotal = itemC.MontoTotal;
                ped.FormaPago = itemC.FormaPago;
                ped.Nro = itemC.Nro;
                ped.TipoPedido = itemC.TipoPedido;
                ped.NroImpresion = 0;
                ped.Impreso = false;


                int j = 0;
                CommonMarzzan.DetallePedido[] detalles = new CommonMarzzan.DetallePedido[itemC.DetallePedidos.Count];


                List<CommonMarzzan.DetallePedido> DetallesOrdenados = (from D in itemC.DetallePedidos
                                                        orderby D.CodigoCompleto
                                                         select D).ToList<CommonMarzzan.DetallePedido>();


                foreach (CommonMarzzan.DetallePedido det in DetallesOrdenados)
                {
                    CommonMarzzan.DetallePedido detalle = new CommonMarzzan.DetallePedido();
                    detalle.Cantidad = det.Cantidad;
                    detalle.CodigoCompleto = det.CodigoCompleto;
                    detalle.Presentacion = det.objPresentacion.IdPresentacion;
                    detalle.Producto = det.objProducto.IdProducto;
                    detalle.ValorTotal = det.ValorTotal;
                    detalle.ValorUnitario = det.ValorUnitario;

                    detalles[j] = detalle;
                    j++;

                }

                ped.DetallesInfo = detalles;
                Resultado.Add(ped);
                i++;

                itemC.EstadoEnvio = "Procesando";
                itemC.FechaEnvio = DateTime.Now;

            }

            dbLocal.SubmitChanges();
            return Resultado;

    }

    [WebMethod]
    public void ConfirmGetPedidos(bool ImportCorrect)
    {
        
        Marzzan_InfolegacyDataContext dbLocal = new Marzzan_InfolegacyDataContext();

        List<CommonMarzzan.CabeceraPedido> cabeceras = (from C in dbLocal.CabeceraPedidos
                                                        where C.EstadoEnvio == "Procesando"
                                                        select C).ToList<CommonMarzzan.CabeceraPedido>();
        if (ImportCorrect)
        {
            foreach (CommonMarzzan.CabeceraPedido cab in cabeceras)
            {
                cab.EstadoEnvio = "Enviado";
                cab.FechaEnvio = DateTime.Now;
            }
        }
        else
        {
            foreach (CommonMarzzan.CabeceraPedido cab in cabeceras)
            {
                cab.EstadoEnvio = "";
                cab.FechaEnvio = null;
            }
        }

        dbLocal.SubmitChanges();
    }

    [WebMethod]
    public void ReGenerarXML()
    {
        ReGenerarXMLProductos();
    }

    [WebMethod]
    public void ActualizacionRemitos(List<RemitosPendiente> remitos, bool PrimerPasada)
    {
        Marzzan_InfolegacyDataContext dcWeb = new Marzzan_InfolegacyDataContext();

        if (PrimerPasada)
        {
            var RemitosEliminar = from R in dcWeb.RemitosPendientes
                                  select R;

            dcWeb.RemitosPendientes.DeleteAllOnSubmit(RemitosEliminar);
            dcWeb.SubmitChanges();
        }


        foreach (RemitosPendiente remito in remitos)
        {
            RemitosPendiente remPendiente = new RemitosPendiente();
            remPendiente.Cantidad = decimal.Parse(remito.Cantidad.ToString());
            remPendiente.CodArticulo = remito.CodArticulo;
            remPendiente.CodCliente  = remito.CodCliente;
            remPendiente.DescArticulo = remito.DescArticulo;
            remPendiente.FechaRemito= remito.FechaRemito;
            remPendiente.NroRemito = remito.NroRemito;
            remPendiente.Precio = decimal.Parse(remito.Precio.ToString());

            dcWeb.RemitosPendientes.InsertOnSubmit(remPendiente);
        }

        dcWeb.SubmitChanges();
        dcWeb.Connection.Close();
        
    }

    private SortedList OrdenarPresentaciones(List<string> DescPresentaciones)
    {
        SortedList sort = new SortedList();
        foreach (string item in DescPresentaciones)
        {
            if (item.ToLower() == "unidad")
            {
                sort.Add(decimal.Parse("0"), item);
            }
            else
            {
                sort.Add(decimal.Parse(item.Replace("ml", "")), item);
            }

            //else if (item.Replace("ml", "").Trim() != "2,5")
            //{
            //    sort.Add(decimal.Parse(item.Replace("ml", "")), item);
            //}

        }

        return sort;


    }

    private void ReGenerarXMLProductos()
    {
        Marzzan_InfolegacyDataContext dbLocal = new Marzzan_InfolegacyDataContext();

        XElement xElementPadre = null;
        List<Producto> legajos = (from L in dbLocal.Productos
                                  where L.Tipo == 'A'
                                  && (L.ColPresentaciones.Count == 0 || L.ColPresentaciones.Where<Presentacion>(w => w.Activo.Value).Count() > 0)
                                  orderby L.Nivel ascending, L.Descripcion ascending
                                  select L).ToList<Producto>();

        XDocument xDoc = new XDocument(
                new XElement("searchable_index", from pt in legajos select new XElement("item", pt.IdProducto)));


        IEnumerable<XElement> childList = from el in xDoc.Elements().Elements()
                                          select el;

        foreach (XElement item in childList)
        {
            Producto current = (from P in legajos
                                where P.IdProducto == long.Parse(item.Value)
                                select P).Single<Producto>();

            if (current.Padre.HasValue)
            {
                List<string> ids = ObtenerIds(current);

                List<Presentacion> Presentaciones = (from P in dbLocal.Presentacions
                                                     where ids.Contains(P.Padre.Value.ToString())
                                                     && P.Activo.Value 
                                                     select P).ToList<Presentacion>();

                string codigos = "";
                string descripciones = "";
                string precios = "";

                foreach (Presentacion pre in Presentaciones)
                {
                    codigos += pre.Codigo + "|";
                    descripciones += pre.Descripcion + "|";
                    precios += pre.Precio + "|";
                }


                item.Add(new XAttribute("padre", current.objPadre.IdProducto));
                item.Add(new XAttribute("padreDesc", current.objPadre.Descripcion));
                item.Add(new XAttribute("Codigos", codigos));
                item.Add(new XAttribute("Precios", precios));
                item.Add(new XAttribute("Descripciones", descripciones));
                item.Add(new XAttribute("IdProducto", current.IdProducto));

                if (descripciones != "")
                {
                    int HayElementos= (from X in childList
                                         where X.Attribute("IdProducto") != null && X.Attribute("IdProducto").Value == current.objPadre.IdProducto.ToString()
                                         select X).Count<XElement>();

                    if (HayElementos > 0)
                    {

                        xElementPadre = (from X in childList
                                         where X.Attribute("IdProducto") != null && X.Attribute("IdProducto").Value == current.objPadre.IdProducto.ToString()
                                         select X).First<XElement>();


                        List<string> DescPresentaciones = (from P in dbLocal.Presentacions
                                                           orderby P.Precio ascending
                                                           where P.objProducto.objPadre.IdProducto == long.Parse(xElementPadre.Attribute("IdProducto").Value)
                                                           && P.Activo.Value
                                                           select P.Descripcion).ToList<string>().Distinct<string>().ToList<string>();


                        SortedList DescPresentacionOrdenas = OrdenarPresentaciones(DescPresentaciones);


                        descripciones = "";
                        foreach (string pre in DescPresentacionOrdenas.Values)
                        {
                            descripciones += pre + "|";
                        }

                        XAttribute attributePre = new XAttribute("Presentaciones", descripciones);
                        if (xElementPadre.LastAttribute.Name.ToString() != "Presentaciones")
                            xElementPadre.Add(attributePre);
                    }
                }


                item.SetValue(current.Descripcion);
            }
        }

        #region Codigo Original
        //XElement xElementPadre = null;
        //List<CommonMarzzan.Producto> legajos = (from L in dbLocal.Productos
        //                                        where L.Tipo == 'A'
        //                                        orderby L.Nivel ascending, L.Descripcion ascending
        //                                        select L).ToList<CommonMarzzan.Producto>();

        //XDocument xDoc = new XDocument(
        //        new XElement("searchable_index", from pt in legajos select new XElement("item", pt.IdProducto)));


        //IEnumerable<XElement> childList = from el in xDoc.Elements().Elements()
        //                                  select el;

        //foreach (XElement item in childList)
        //{
        //    CommonMarzzan.Producto current = (from P in legajos
        //                        where P.IdProducto == long.Parse(item.Value)
        //                        select P).Single<CommonMarzzan.Producto>();

        //    if (current.Padre.HasValue)
        //    {
        //        List<string> ids = ObtenerIds(current);

        //        List<Presentacion> Presentaciones = (from P in dbLocal.Presentacions
        //                                              where ids.Contains(P.Padre.Value.ToString())
        //                                                            select P).ToList<Presentacion>();

        //        string codigos = "";
        //        string descripciones = "";
        //        string precios = "";

        //        foreach (Presentacion pre in Presentaciones)
        //        {
        //            codigos += pre.Codigo + "|";
        //            descripciones += pre.Descripcion + "|";
        //            precios += pre.Precio + "|";
        //        }


        //        item.Add(new XAttribute("padre", current.objPadre.IdProducto));
        //        item.Add(new XAttribute("padreDesc", current.objPadre.Descripcion));
        //        item.Add(new XAttribute("Codigos", codigos));
        //        item.Add(new XAttribute("Precios", precios));
        //        item.Add(new XAttribute("Descripciones", descripciones));
        //        item.Add(new XAttribute("IdProducto", current.IdProducto));


        //        if (descripciones != "")
        //        {

        //            xElementPadre = (from X in childList
        //                             where X.Attribute("IdProducto") != null && X.Attribute("IdProducto").Value == current.objPadre.IdProducto.ToString()
        //                             select X).First<XElement>();


        //            List<string> DescPresentaciones = (from P in dbLocal.Presentacions
        //                                               orderby P.Precio ascending
        //                                               where P.objProducto.objPadre.IdProducto == long.Parse(xElementPadre.Attribute("IdProducto").Value)
        //                                               select P.Descripcion).ToList<string>().Distinct<string>().ToList<string>();



        //            descripciones = "";
        //            foreach (string pre in DescPresentaciones)
        //            {
        //                descripciones += pre + "|";
        //            }

        //            XAttribute attributePre = new XAttribute("Presentaciones", descripciones);
        //            if (xElementPadre.LastAttribute.Name.ToString() != "Presentaciones")
        //                xElementPadre.Add(attributePre);
        //        }


        //        item.SetValue(current.Descripcion);
        //    }
        //}

        #endregion


        xDoc.Save(Server.MapPath("") + @"\Productos.xml");
    
    }

    private string ObtenerValorNivel(View_Producto item, int nivel)
    {
        switch (nivel)
        {
            case 1:
                return item.UNIDAD_NEGOCIO.Split('-')[1].Trim();
            case 2:
                return item.LINEA.Split('-')[1].Trim();
            case 3:
                return item.FRAGANCIA.Split('-')[1].Trim();

        }

        return "";
    }

    private string ObtenerCodigoNivel(View_Producto item, int nivel)
    {
        switch (nivel)
        {
            case 1:
                return item.UNIDAD_NEGOCIO.Split('-')[0].Trim();
            case 2:
                return item.LINEA.Split('-')[0].Trim();
            case 3:
                return item.FRAGANCIA.Split('-')[0].Trim();

        }

        return "";
    }

    private List<string> ObtenerIds(Producto producto)
    {

        List<string> ids = new List<string>();

        if (producto.ColHijos.Count > 0)
        {
            foreach (Producto prodHijo in producto.ColHijos)
            {
                ObtenerIds(prodHijo);
                ids.Add(producto.IdProducto.ToString());
            }

        }
        else
        {
            ids.Add(producto.IdProducto.ToString());
        }

        return ids;

    }
}

