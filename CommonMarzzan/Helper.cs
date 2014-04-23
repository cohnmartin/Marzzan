using System;
using System.Linq;
using System.Web;
using System.Data;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;
using System.Data.Linq;
using System.IO;
using System.Text;
using CommonMarzzan;

/// <summary>
/// Summary description for Helper
/// </summary>
public class Helper
{
    public static string _descripcion = "";
    public static string _descripcionCompleta = "";
    public Helper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void GestionPedidoConCredito(Cliente cliente, Marzzan_InfolegacyDataContext contexto)
    {
        /// . Calculo el monto de pago anticipado que posee el cliente para cancelar los pedidos
        decimal CreditoDisponible = 0;
        decimal SaldoInformado = cliente.SaldoPagoAnticipado.Value;
        List<decimal?> PagoAnticipadoUtilizado = (from c in contexto.PedidosConCreditos
                                                  where c.Cliente == cliente.IdCliente
                                                  && c.Procesado.Value
                                                  && (c.MontoCredito.Value - c.MontoPagado.Value) >= 0
                                                  select c.MontoPagado).ToList();

        if (SaldoInformado != 0)
        {
            CreditoDisponible = (SaldoInformado - PagoAnticipadoUtilizado.Sum().Value) * -1;
        }

        /// . Si hay credito, recupero los pedido con credito vencidos del cliente
        if (CreditoDisponible > 0)
        {

            List<PedidosConCredito> PedidosVencidos = (from p in contexto.PedidosConCreditos
                                                       where DateTime.Now.Date >= p.FechaVencimiento.Value.Date
                                                       && p.Cliente == cliente.IdCliente
                                                       select p).ToList();



            /// . Si hay pedido vencidos, utilizo el credito disponible para pagar hasta donde alcance dicho pedidos
            if (PedidosVencidos.Count > 0)
            {
                ConfCredito CurrentConfCredito = null;
                decimal CreditoDisponibleTemp = CreditoDisponible;
                foreach (PedidosConCredito pedido in PedidosVencidos)
                {
                    decimal MontoACancelar = pedido.MontoCredito.Value - pedido.MontoPagado.Value;
                    decimal MontoUtilizado = 0;
                    if (CreditoDisponibleTemp >= MontoACancelar)
                    {
                        /// Cancelo el total
                        pedido.MontoPagado += MontoACancelar;
                        CreditoDisponibleTemp -= MontoACancelar;
                        MontoUtilizado = MontoACancelar;
                    }
                    else if (CreditoDisponibleTemp < MontoACancelar)
                    {
                        /// Cancelo por la diferencia
                        pedido.MontoPagado += CreditoDisponibleTemp;
                        MontoUtilizado = CreditoDisponibleTemp;
                        CreditoDisponibleTemp = 0;

                    }

                    /// Si el cliente que estoy evaluando es igual al clientes responsable
                    /// del credito entonces recupero el confCredito del cliente actual, si son
                    /// distintos entoces utilizo el cliente responsable del credito para obtener 
                    /// el confCredito.
                    /// Luego De acuerdo al monto pagado, libero el credito ocupado del cliente 
                    /// (MontoUtilizado en confCreditos)
                    if (cliente.IdCliente == pedido.ClienteRespCredito.Value)
                    {
                        CurrentConfCredito = cliente.ColConfCreditos.First();
                    }
                    else
                    {
                        CurrentConfCredito = pedido.objClienteResponsableCredito.ColConfCreditos.First();
                    }

                    CurrentConfCredito.MontoUtilizado -= MontoUtilizado;
                }

                contexto.SubmitChanges();

            }
        }

    }

    public static string ObtenerDescripcionCompletaProductoEnComun(List<Producto> productos)
    {
        _descripcionCompleta = "";
        return ObtenerDescripcionEnComun(productos);
    }

    private static string ObtenerDescripcionEnComun(List<CommonMarzzan.Producto> productos)
    {

        List<string> padre = (from P in productos
                              where P.objPadre != null
                              select P.objPadre.Descripcion).Distinct<string>().ToList<string>();

        if (padre.Count > 1)
        {
            ObtenerDescripcionEnComun((from P in productos
                                       select P.objPadre).ToList<CommonMarzzan.Producto>());

        }
        else if (padre.Count == 1)
        {
            _descripcionCompleta = padre[0] + " " + _descripcionCompleta;

            List<Producto> padres = (from P in productos
                                     where P.objPadre.objPadre != null
                                     select P.objPadre).ToList<CommonMarzzan.Producto>();

            if (padres.Count > 0)
                ObtenerDescripcionEnComun(padres);

        }
        else
        {
            _descripcionCompleta = productos[0].Descripcion;
        }

        return _descripcionCompleta;
    }

    public static string ObtenerDescripcionCompletaProducto(CommonMarzzan.Producto producto)
    {

        if (producto.objPadre != null)
        {
            ObtenerDescripcionCompletaProducto(producto.objPadre);
            _descripcion += producto.Descripcion + " ";
        }
        else
        {
            _descripcion = "";
            _descripcion += producto.Descripcion + " ";
        }

        return _descripcion;
    }

    static public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
    {
        DataTable dtReturn = new DataTable();

        // column names 
        PropertyInfo[] oProps = null;

        if (varlist == null) return dtReturn;

        foreach (T rec in varlist)
        {
            // Use reflection to get property names, to create table, Only first time, others 
            //will follow 
            if (oProps == null)
            {
                oProps = ((Type)rec.GetType()).GetProperties();
                foreach (PropertyInfo pi in oProps)
                {
                    Type colType = pi.PropertyType;

                    if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    {
                        colType = colType.GetGenericArguments()[0];
                    }
                    dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                }
            }

            DataRow dr = dtReturn.NewRow();

            foreach (PropertyInfo pi in oProps)
            {
                dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue(rec, null);
            }

            dtReturn.Rows.Add(dr);
        }
        return dtReturn;
    }

    public static List<CommonMarzzan.Producto> GetProductosByNivel(int nivel)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        var productos = from P in dc.Productos
                        where P.Nivel == nivel && (P.Tipo == 'P' || P.Tipo == 'A' || P.Tipo == 'D' || P.Tipo == 'R')
                        select P;

        return productos.ToList<CommonMarzzan.Producto>();

    }

    public static List<CommonMarzzan.Producto> GetProductosByParent(long padre)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        var productos = from P in dc.Productos
                        where P.objPadre.IdProducto == padre
                        select P;

        return productos.ToList<CommonMarzzan.Producto>();

    }

    public static List<Presentacion> GetPresentaciones(long padre)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        var presentaciones = from P in dc.Presentacions
                             where P.Padre == padre
                             select P;

        return presentaciones.ToList<Presentacion>();

    }

    public static List<Presentacion> GetPresentacionesByTipoProducto(long idLinea)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        var presentaciones = from P in dc.Presentacions
                             where P.objProducto.objPadre.IdProducto == idLinea
                             select P;

        SortedList preD = new SortedList();
        List<Presentacion> PresentacinoesFinales = new List<Presentacion>();

        foreach (Presentacion pre in presentaciones)
        {
            if (!preD.ContainsKey(pre.Descripcion))
            {
                preD.Add(pre.Descripcion, pre);
                PresentacinoesFinales.Add(pre);
            }
        }

        return PresentacinoesFinales;

    }

    public static List<Presentacion> GetPresentacionesByLinea(long idLinea)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        var presentaciones = from P in dc.Presentacions
                             where P.objProducto.objPadre.IdProducto == idLinea
                             select P;

        SortedList preD = new SortedList();
        List<Presentacion> PresentacinoesFinales = new List<Presentacion>();

        foreach (Presentacion pre in presentaciones)
        {
            if (!preD.ContainsKey(pre.Descripcion))
            {
                preD.Add(pre.Descripcion, pre);
                PresentacinoesFinales.Add(pre);
            }
        }

        return PresentacinoesFinales;

    }

    public static List<Presentacion> GetPresentacionesByUndNeg(long idUndNeg)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();


        var presentaciones = from P in dc.Presentacions
                             where P.objProducto.objPadre.objPadre.IdProducto == idUndNeg
                             select P;


        SortedList preD = new SortedList();
        List<Presentacion> PresentacinoesFinales = new List<Presentacion>();

        foreach (Presentacion pre in presentaciones)
        {
            if (!preD.ContainsKey(pre.Descripcion))
            {
                preD.Add(pre.Descripcion, pre);
                PresentacinoesFinales.Add(pre);
            }
        }

        return PresentacinoesFinales;

    }

    public static List<Producto> GetFraganciasByUndNeg(long IdUndNeg)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        var productosFragancia = from P in dc.Productos
                                 where P.Nivel == 3 && P.objPadre.objPadre.IdProducto == IdUndNeg
                                 select P;

        //SortedList preD = new SortedList();
        //List<Presentacion> PresentacinoesFinales = new List<Presentacion>();

        //foreach (Presentacion pre in presentaciones)
        //{
        //    if (!preD.ContainsKey(pre.Descripcion))
        //    {
        //        preD.Add(pre.Descripcion, pre);
        //        PresentacinoesFinales.Add(pre);
        //    }
        //}

        return productosFragancia.ToList<Producto>();

    }

    public static List<Presentacion> GetAllPresentacionesByUndNeg(long idUndNeg)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        var presentaciones = from P in dc.Presentacions
                             where P.objProducto.objPadre.objPadre.IdProducto == idUndNeg
                             select P;



        return presentaciones.ToList<Presentacion>();

    }

    public static List<Presentacion> GetAllPresentacionesByLnea(long padre)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        var presentaciones = from P in dc.Presentacions
                             where P.objProducto.objPadre.IdProducto == padre
                             select P;


        return presentaciones.ToList<Presentacion>();

    }

    public static List<Presentacion> GetAllPresentacionesByPromo(long padre)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        var presentaciones = from P in dc.Presentacions
                             where P.Padre == padre
                             select P;


        return presentaciones.ToList<Presentacion>();

    }

    public static void EnvioMailSolicitudAlta(string body, string mailDestino, string MailOrigen, string sSujeto, string SMTPMailOrigen, string UsuarioMailOrigen, string ClaveMailOrigen)
    {


        //System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        //msg.To.Add(new System.Net.Mail.MailAddress(mailDestino));
        //msg.From = new System.Net.Mail.MailAddress(MailOrigen);
        //msg.CC.Add(new System.Net.Mail.MailAddress(MailOrigen));
        //msg.Subject = sSujeto;
        //msg.Body = body;
        //msg.IsBodyHtml = true;
        //System.Net.Mail.SmtpClient clienteSmtp = new System.Net.Mail.SmtpClient(SMTPMailOrigen);
        //clienteSmtp.Credentials = new System.Net.NetworkCredential(UsuarioMailOrigen, ClaveMailOrigen);
        ////clienteSmtp.EnableSsl = true;
        ////clienteSmtp.SendCompleted += new System.Net.Mail.SendCompletedEventHandler(clienteSmtp_SendCompleted);

        //clienteSmtp.Send(msg);

        System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage("Sandra<" + MailOrigen + ">", mailDestino);
        m.Subject = sSujeto;
        m.Body = body;
        m.IsBodyHtml = true;
        m.From = new System.Net.Mail.MailAddress(MailOrigen);
        m.CC.Add(new System.Net.Mail.MailAddress(MailOrigen));

        m.To.Add(new System.Net.Mail.MailAddress(mailDestino));
        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
        smtp.Host = SMTPMailOrigen; /// "smtp.gmail.com";

        System.Net.NetworkCredential authinfo = new System.Net.NetworkCredential(UsuarioMailOrigen, ClaveMailOrigen);
        smtp.UseDefaultCredentials = false;
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.Credentials = authinfo;
        smtp.Send(m);
    }

    static void clienteSmtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        throw new NotImplementedException();
    }

    public static List<string> ObtenerGruposSubordinados(string grupoCliente)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
        if (grupoCliente.Split('-')[0].Trim() == grupoCliente.Split('-')[1].Trim())
        {
            return (from C in dc.Clientes
                    where (C.Clasif1 == grupoCliente || C.Clasif1.StartsWith(grupoCliente.Split('-')[0].Trim() + "-"))
                    && C.Habilitado.Value
                    orderby C.Nombre
                    select C.Clasif1).Distinct().ToList<string>();
        }
        else
        {
            return (from C in dc.Clientes
                    where (C.Clasif1 == grupoCliente || C.Clasif1.StartsWith(grupoCliente + "-"))
                    && C.Habilitado.Value
                    orderby C.Nombre
                    select C.Clasif1).Distinct().ToList<string>();
        }
    }
    public static List<string> ObtenerGruposSubordinados(Cliente Cliente)
    {
        return ObtenerGruposSubordinados(Cliente.Clasif1);
    }
    public static List<string> ObtenerGrupos(string grupoCliente)
    {

        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        if (grupoCliente.Split('-')[0].Trim() == grupoCliente.Split('-')[1].Trim())
        {
            return (from C in dc.Clientes
                    where (C.Clasif1 == grupoCliente || C.Clasif1.StartsWith(grupoCliente.Split('-')[0].Trim() + "-"))
                    && C.Habilitado.Value
                    orderby C.Nombre
                    select C.Clasif1).Distinct().ToList<string>();
        }
        else //if (grupoCliente.Split('-')[0].Trim() != grupoCliente.Split('-')[1].Trim())
        {
            return (from C in dc.Clientes
                    where (C.Clasif1 == grupoCliente || C.Clasif1.StartsWith(grupoCliente + "-"))
                    && C.Habilitado.Value
                    orderby C.Nombre
                    select C.Clasif1).Distinct().ToList<string>();
        }

    }

    public static List<Cliente> ObtenerConsultoresSubordinados(Cliente Cliente)
    {

        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        if (Cliente.CodTipoCliente == "1")
        {
            List<Cliente> UnicoCliente = new List<Cliente>();
            UnicoCliente.Add(Cliente);
            return UnicoCliente;
        }
        else if (Cliente.CodTipoCliente == "12")
        {
            return new List<Cliente>();
        }
        else if (Cliente.Clasif1.Split('-').Count() == 1)
        {
            return new List<Cliente>();
        }
        else if (Cliente.Clasif1.Split('-')[0].Trim() == Cliente.Clasif1.Split('-')[1].Trim())
        {

            return (from C in dc.Clientes
                    where (C.Clasif1 == Cliente.Clasif1 || C.Clasif1.StartsWith(Cliente.Clasif1.Split('-')[0].Trim() + "-"))
                    && C.Habilitado.Value
                    orderby C.Nombre
                    select C).ToList<Cliente>();
        }
        else if (Cliente.Clasif1.Split('-')[0].Trim() != Cliente.Clasif1.Split('-')[1].Trim())
        {
            return (from C in dc.Clientes
                    where (C.Clasif1 == Cliente.Clasif1 || C.Clasif1.StartsWith(Cliente.Clasif1 + "-"))
                    && C.Habilitado.Value
                    orderby C.Nombre
                    select C).ToList<Cliente>();
        }
        else
            return new List<Cliente>();


        //if (Cliente.TipoCliente.ToUpper() == "COORDINADOR" || Cliente.TipoCliente.ToUpper() == "DIRECTOR SENIOR" )
        //{
        //    return (from C in dc.Clientes
        //            where C.CodVendedor == Cliente.CodVendedor
        //            && C.Habilitado.Value
        //            orderby C.Nombre
        //            select C).ToList<Cliente>();
        //}
        //else if (Cliente.CodTipoCliente == "1")
        //{
        //    List < Cliente >  UnicoCliente = new List<Cliente>();
        //    UnicoCliente.Add(Cliente);
        //    return UnicoCliente;
        //}
        //else
        //{
        //    return (from C in dc.Clientes
        //            where C.CodClasif1 == Cliente.CodClasif1
        //            && C.Habilitado.Value
        //            orderby C.Nombre
        //            select C).ToList<Cliente>();
        //}

    }
    public static List<Cliente> ObtenerConsultoresSubordinadosDirectos(string grupo)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        return (from C in dc.Clientes
                where (C.Clasif1 == grupo)
                && C.Habilitado.Value
                orderby C.Nombre
                select C).ToList<Cliente>();

    }

    public static long ObtenerLider(long idConsultor, string grupo)
    {

        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        return (from C in dc.Clientes
                where C.Clasif1 == grupo && C.CodTipoCliente != "1" && C.CodTipoCliente != "12"
                && C.Habilitado.Value
                select C.IdCliente).FirstOrDefault();



    }

    public static string Encriptar(string valor)
    {
        string Secret = valor;
        string Key = "infolegacy";
        EncryptionClassLibrary.Encryption.Symmetric.Provider p = EncryptionClassLibrary.Encryption.Symmetric.Provider.TripleDES;

        EncryptionClassLibrary.Encryption.Symmetric sym = new EncryptionClassLibrary.Encryption.Symmetric(p, true);
        sym.Key.Text = Key;


        EncryptionClassLibrary.Encryption.Data encryptedData;
        encryptedData = sym.Encrypt(new EncryptionClassLibrary.Encryption.Data(Secret));
        return encryptedData.Hex;

    }

    public static string DesEncriptar(string valor)
    {

        string Key = "infolegacy";

        EncryptionClassLibrary.Encryption.Symmetric.Provider p = EncryptionClassLibrary.Encryption.Symmetric.Provider.TripleDES;
        EncryptionClassLibrary.Encryption.Symmetric sym = new EncryptionClassLibrary.Encryption.Symmetric(p, true);
        sym.Key.Text = Key;

        EncryptionClassLibrary.Encryption.Data ClaveEncript;
        ClaveEncript = new EncryptionClassLibrary.Encryption.Data();
        ClaveEncript.Hex = valor;

        EncryptionClassLibrary.Encryption.Data decryptedData;
        EncryptionClassLibrary.Encryption.Symmetric sym2 = new EncryptionClassLibrary.Encryption.Symmetric(p, true);
        sym2.Key.Text = sym.Key.Text;
        decryptedData = sym2.Decrypt(ClaveEncript);




        return decryptedData.Text;
    }

    public static Dictionary<string, string> GetCoincidenciasClienteSolicitudesAltas(string Nombre, string Dni, string Email
        , string Telefono, string Celular)
    {
        Dictionary<string, string> datos = new Dictionary<string, string>();
        List<string> codigos = new List<string>();

        Celular = Celular.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
        Telefono = Telefono.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
        Celular = Celular = long.Parse(Celular.Trim()) == 0 ? long.Parse(Telefono.Trim()) != 0 ? Telefono.Trim() : "0000000000" : Celular.Trim();

        using (Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext())
        {



            //List<string> colNombres = Nombre.Split(' ').ToList();
            //string cad1 = string.Empty, cad2 = string.Empty, cad3 = string.Empty;
            //int Cantnombre = 0;
            //if (colNombres.Count() == 1)
            //{
            //    cad1 = colNombres[0].ToLower();

            //    var result = (from P in dc.SolicitudesAltas
            //                  where P.Nombre.ToLower().Contains(cad1)
            //                  select P.IdSolicitudAlta.ToString()).Distinct();

            //    Cantnombre = result.Count();
            //    codigos.AddRange(result);
            //}
            //else if (colNombres.Count() == 2)
            //{
            //    cad1 = colNombres[0].ToLower();
            //    cad2 = colNombres[1].ToLower();
            //    var result = (from P in dc.SolicitudesAltas
            //                  where P.Nombre.ToLower().Contains(cad1) && P.Nombre.ToLower().Contains(cad2)
            //                  select P.IdSolicitudAlta.ToString()).Distinct();

            //    Cantnombre = result.Count();
            //    codigos.AddRange(result);

            //}
            //else if (colNombres.Count() == 3)
            //{
            //    cad1 = colNombres[0].ToLower();
            //    cad2 = colNombres[1].ToLower();
            //    cad3 = colNombres[2].ToLower();
            //    var result = (from P in dc.SolicitudesAltas
            //                  where P.Nombre.ToLower().Contains(cad1) && P.Nombre.ToLower().Contains(cad2) && P.Nombre.ToLower().Contains(cad3)
            //                  select P.IdSolicitudAlta.ToString()).Distinct();

            //    Cantnombre = result.Count();
            //    codigos.AddRange(result);
            //}

            /// Control por el DNI
            var resultDni = (from P in dc.SolicitudesAltas
                             where P.DNI.Contains(Dni)
                             select P.IdSolicitudAlta.ToString()).Distinct();

            int CantDni = resultDni.Count();
            codigos.AddRange(resultDni);



            // Solo se controla si hay un mail para controlar
            int CantEmail = 0;
            //if (Email.Trim() != "")
            //{
            //    //int pos = Email.IndexOf('@');
            //    //Email = Email.Substring(0, pos);

            //    var resultEmail = (from P in dc.SolicitudesAltas
            //                       where P.Email.ToLower() == Email.ToLower()
            //                       select P.IdSolicitudAlta.ToString()).Distinct();

            //    CantEmail = resultEmail.Count();
            //    codigos.AddRange(resultEmail);
            //}


            //int CantTelefono = (from P in dc.SolicitudesAltas
            //                    where P.TelFijo.Contains(Telefono)
            //                    select P).Count();
            Celular = Celular.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            var resultCantCelular = (from P in dc.SolicitudesAltas
                                     where P.TelCelular.Contains(Celular)
                                     select P.IdSolicitudAlta.ToString()).Distinct();

            int CantCelular = resultCantCelular.Count();
            codigos.AddRange(resultCantCelular);


            if (CantDni > 0 || CantEmail > 0 || CantCelular > 0)
            {
                string cad = "Cantidad coincidencias en solicitudes - ";
                cad += " DNI:" + CantDni.ToString();
                cad += " Cel:" + CantCelular.ToString();

                datos.Add("Mensaje", cad);
                datos.Add("Codigos", string.Join(",", codigos.Distinct().ToArray()));
            }


            //if (Cantnombre > 0 || CantDni > 0 ||
            //CantEmail > 0 || CantTelefono > 0 || CantCelular > 0)
            //{
            //    string cad = "Cantidad coincidencias en solicitudes - ";
            //    cad += " Nombre:" + Cantnombre.ToString();
            //    cad += " DNI:" + CantDni.ToString();
            //    cad += " Email:" + CantEmail.ToString();
            //    cad += " Tel:" + CantTelefono.ToString();
            //    cad += " Cel:" + CantCelular.ToString();

            //    datos.Add("Mensaje", cad);
            //    datos.Add("Codigos", string.Join(",", codigos.Distinct().ToArray()));
            //}

            return datos;
        }
    }

    public static Dictionary<string, string> GetCoincidenciasClientesCRM(string Nombre, string Dni, string Email
        , string Telefono, string Celular)
    {
        Dictionary<string, string> datos = new Dictionary<string, string>();
        List<string> codigos = new List<string>();

        Celular = Celular.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
        Telefono = Telefono.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

        Celular = long.Parse(Celular.Trim()) == 0 ? long.Parse(Telefono.Trim()) != 0 ? Telefono.Trim() : "0000000000" : Celular.Trim();


        using (Marzzan_BejermanDataContext dc = new Marzzan_BejermanDataContext())
        {

            #region Control Anulado
            //List<string> colNombres = Nombre.Split(' ').ToList();
            //string cad1 = string.Empty, cad2 = string.Empty, cad3 = string.Empty;

            //int Cantnombre = 0;
            //if (colNombres.Count() == 1)
            //{
            //    cad1 = colNombres[0].ToLower();

            //    var result = (from P in dc.ClientesCRMs
            //                  where P.clr_Habilitado && P.clr_RazSoc.ToLower().Contains(cad1)
            //                  select P.clr_Cod).Distinct();

            //    Cantnombre = result.Count();
            //    codigos.AddRange(result);
            //}
            //else if (colNombres.Count() == 2)
            //{
            //    cad1 = colNombres[0].ToLower();
            //    cad2 = colNombres[1].ToLower();
            //    var result = (from P in dc.ClientesCRMs
            //                  where P.clr_Habilitado && P.clr_RazSoc.ToLower().Contains(cad1) && P.clr_RazSoc.ToLower().Contains(cad2)
            //                  select P.clr_Cod).Distinct();

            //    Cantnombre = result.Count();
            //    codigos.AddRange(result);

            //}
            //else if (colNombres.Count() == 3)
            //{
            //    cad1 = colNombres[0].ToLower();
            //    cad2 = colNombres[1].ToLower();
            //    cad3 = colNombres[2].ToLower();
            //    var result = (from P in dc.ClientesCRMs
            //                  where P.clr_Habilitado && P.clr_RazSoc.ToLower().Contains(cad1) && P.clr_RazSoc.ToLower().Contains(cad2) && P.clr_RazSoc.ToLower().Contains(cad3)
            //                  select P.clr_Cod).Distinct();

            //    Cantnombre = result.Count();
            //    codigos.AddRange(result);
            //}
            #endregion

            #region Control por DNI
            var resultDni = (from P in dc.ClientesCRMs
                             where P.clr_CUIT.Contains(Dni)
                             select P.clr_Cod).Distinct().Take(20);

            int CantDni = resultDni.Count();
            codigos.AddRange(resultDni);
            #endregion

            #region Control por EMAIL - Anulado
            int CantEmail = 0;
            //if (Email.Trim() != "")
            //{
            //    //int pos = Email.IndexOf('@');
            //    //Email = Email.Substring(0, pos);

            //    var resultEmail = (from P in dc.ClientesCRMs
            //                       where P.clr_EMail.ToLower() == Email.ToLower()
            //                       select P.clr_Cod).Distinct();

            //    CantEmail = resultEmail.Count();
            //    codigos.AddRange(resultEmail);
            //}
            #endregion

            #region Control Anulado
            //int CantTelefono = (from P in dc.ClientesCRMs
            //                    where P.clr_Habilitado && P.clr_Tel.Contains(Telefono)
            //                    select P).Count();
            #endregion

            #region Control por TELEFONO CELULAR

            Celular = Celular.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            Celular = Convert.ToInt64(Celular).ToString();

            var resultCantCelular = (from P in dc.ClientesCRMs
                                     where P.clr_Tel.Contains(Celular)
                                     select P.clr_Cod).Distinct().Take(20);

            int CantCelular = resultCantCelular.Count();
            codigos.AddRange(resultCantCelular);
            #endregion

            #region Control Anulado
            //if (Cantnombre > 0 || CantDni > 0 ||
            //CantEmail > 0 || CantTelefono > 0 || CantCelular > 0)
            //{
            //    string cad = "Cantidad coincidencias en Clientes CRM - ";
            //    cad += " Nombre:" + Cantnombre.ToString();
            //    cad += " DNI:" + CantDni.ToString();
            //    cad += " Email:" + CantEmail.ToString();
            //    cad += " Tel:" + CantTelefono.ToString();
            //    cad += " Cel:" + CantCelular.ToString();

            //    datos.Add("Mensaje", cad);
            //    datos.Add("Codigos", string.Join(",", codigos.Distinct().ToArray()));
            //}
            #endregion

            if (CantDni > 0 || CantEmail > 0 || CantCelular > 0)
            {
                string cad = "Cantidad coincidencias en Clientes CRM - ";
                cad += " DNI:" + CantDni.ToString();
                cad += " Cel:" + CantCelular.ToString();

                datos.Add("Mensaje", cad);
                datos.Add("Codigos", string.Join(",", codigos.Distinct().ToArray()));
            }


            return datos;
        }
    }


    public static Dictionary<string, string> GetCoincidenciasCliente(string Nombre, string Dni, string Email
        , string Telefono, string Celular)
    {
        Dictionary<string, string> datos = new Dictionary<string, string>();
        List<string> codigos = new List<string>();

        Celular = Celular.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
        Telefono = Telefono.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
        Celular = Celular = long.Parse(Celular.Trim()) == 0 ? long.Parse(Telefono.Trim()) != 0 ? Telefono.Trim() : "0000000000" : Celular.Trim();

        using (Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext())
        {


            //List<string> colNombres = Nombre.Split(' ').ToList();
            //string cad1 = string.Empty, cad2 = string.Empty, cad3 = string.Empty;
            //int Cantnombre = 0;
            //if (colNombres.Count() == 1)
            //{
            //    cad1 = colNombres[0].ToLower();

            //    var result = (from P in dc.Clientes
            //                  where P.Habilitado.Value && P.Nombre.ToLower().Contains(cad1)
            //                  select P.CodigoExterno).Distinct();

            //    Cantnombre = result.Count();
            //    codigos.AddRange(result);
            //}
            //else if (colNombres.Count() == 2)
            //{
            //    cad1 = colNombres[0].ToLower();
            //    cad2 = colNombres[1].ToLower();
            //    var result = (from P in dc.Clientes
            //                  where P.Habilitado.Value && P.Nombre.ToLower().Contains(cad1) && P.Nombre.ToLower().Contains(cad2)
            //                  select P.CodigoExterno).Distinct();

            //    Cantnombre = result.Count();
            //    codigos.AddRange(result);

            //}
            //else if (colNombres.Count() == 3)
            //{
            //    cad1 = colNombres[0].ToLower();
            //    cad2 = colNombres[1].ToLower();
            //    cad3 = colNombres[2].ToLower();
            //    var result = (from P in dc.Clientes
            //                  where P.Habilitado.Value && P.Nombre.ToLower().Contains(cad1) && P.Nombre.ToLower().Contains(cad2) && P.Nombre.ToLower().Contains(cad3)
            //                  select P.CodigoExterno).Distinct();

            //    Cantnombre = result.Count();
            //    codigos.AddRange(result);
            //}


            var resultDni = (from P in dc.Clientes
                             where P.Dni.Contains(Dni)
                             select P.CodigoExterno).Distinct().Take(20);

            int CantDni = resultDni.Count();
            codigos.AddRange(resultDni);

            // Solo se controla si hay un mail para controlar
            int CantEmail = 0;
            //if (Email.Trim() != "")
            //{
            //    //int pos = Email.IndexOf('@');
            //    //Email = Email.Substring(0, pos);

            //    var resultEmail = (from P in dc.Clientes
            //                       where  P.Email.ToLower() == Email.ToLower()
            //                       select P.CodigoExterno).Distinct();

            //    CantEmail = resultEmail.Count();
            //    codigos.AddRange(resultEmail);
            //}

            //int CantTelefono = (from P in dc.Clientes
            //                    where P.Habilitado.Value && P.Telefono.Contains(Telefono)
            //                    select P).Count();

            Celular = Celular.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            var resultCantCelular = (from P in dc.Clientes
                                     where P.Telefono.Contains(Celular)
                                     select P.CodigoExterno).Distinct().Take(20);

            int CantCelular = resultCantCelular.Count();
            codigos.AddRange(resultCantCelular);

            if (CantDni > 0 || CantEmail > 0 || CantCelular > 0)
            {
                string cad = "Cantidad coincidencias - ";
                cad += " DNI:" + CantDni.ToString();
                cad += " Cel:" + CantCelular.ToString();

                datos.Add("Mensaje", cad);
                datos.Add("Codigos", string.Join(",", codigos.Distinct().ToArray()));
            }

            //if (Cantnombre > 0 || CantDni > 0 ||
            //CantEmail > 0 || CantTelefono > 0 || CantCelular > 0)
            //{
            //    string cad = "Cantidad coincidencias - ";
            //    cad += " Nombre:" + Cantnombre.ToString();
            //    cad += " DNI:" + CantDni.ToString();
            //    cad += " Email:" + CantEmail.ToString();
            //    cad += " Tel:" + CantTelefono.ToString();
            //    cad += " Cel:" + CantCelular.ToString();

            //    datos.Add("Mensaje", cad);
            //    datos.Add("Codigos", string.Join(",", codigos.Distinct().ToArray()));
            //}

            return datos;
        }
    }

    public static string BuscarTransporte(long idDireccion, string formaPago )
    {
        using (Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext())
        {
            ///4º Generacion del Gasto de Envio del Pedido (Flete)
            Direccione dirEntrega = (from D in dc.Direcciones
                                     where D.IdDireccion == idDireccion
                                     select D).First<Direccione>();

            ConfTransporte confT = (from C in dc.ConfTransportes
                                    where C.Provincia == dirEntrega.Provincia &&
                                    C.Localidad == dirEntrega.Localidad &&
                                    C.FormaDePago == formaPago
                                    select C).FirstOrDefault<ConfTransporte>();

            if (confT != null)
            {
                return confT.Transporte.ToUpper().Trim();
            }
            else
            {
                return null;
            }
        }
    }
}



