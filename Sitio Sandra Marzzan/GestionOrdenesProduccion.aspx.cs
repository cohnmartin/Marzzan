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

public partial class GestionOrdenesProduccion : BasePage
{
    public class tempFormulas
    {
        public long Id { get; set; }
        public string NombreFormula { get; set; }
        public string CodigoFormula { get; set; }
        public int cantidad { get; set; }
    }
    public class tempComponentes
    {
        public string DescripcionComonente { get; set; }
        public string Desposito { get; set; }
        public string Disponible { get; set; }
        public string CodigoComponente { get; set; }
        public string Cantidad { get; set; }
    }

    private List<tempFormulas> ListaFormulas
    {
        get
        {

            if (Session["ListaFormulas"] == null)
            {
                Session.Add("ListaFormulas", new List<tempFormulas>());
            }

            return (List<tempFormulas>)Session["ListaFormulas"];
        }

    }

    private Marzzan_InfolegacyDataContext Contexto
    {
        get
        {

            if (Session["Context"] == null)
            {
                Session.Add("Context", new Marzzan_InfolegacyDataContext());
            }

            return (Marzzan_InfolegacyDataContext)Session["Context"];
        }

    }


    private Marzzan_BejermanDataContext ContextoBejerman
    {
        get
        {

            if (Session["ContextoBejerman"] == null)
            {
                Session.Add("ContextoBejerman", new Marzzan_BejermanDataContext());
            }

            return (Marzzan_BejermanDataContext)Session["ContextoBejerman"];
        }

    }



    protected override void PageLoad()
    {
        if (!IsPostBack)
        {

            Session["ContextoBejerman"] = new Marzzan_BejermanDataContext();
            Session["Context"] = new Marzzan_InfolegacyDataContext();
            Session["ListaFormulas"] = new List<tempFormulas>();
            Session["DatosFormulas"] = (from f in ContextoBejerman.RaulFormulas
                                        select new TempClass.tempBusqueda
                                        {
                                            NombreFormula = f.formula,
                                            CodigoFormula = f.producto.Replace("-", "").Replace(" ", "")

                                        }).Distinct().ToList();

            gvFormulas.DataSource = Session["ListaFormulas"] as List<tempFormulas>;
        }

        gvCalculos.ExportToExcel += new ControlsAjaxNotti.ClickEventHandler(gvCalculos_ExportToExcel);
    }

    void gvCalculos_ExportToExcel(object sender)
    {
        gvCalculos.ExportToExcelFunction("ConsumoOrdenesProduccion", (IList)Session["ConsumoComponentes"]);
    }

    [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> AgregarFormula(string codigoFormula, string cantidad)
    {
        List<TempClass.tempBusqueda> datosFormulas = (List<TempClass.tempBusqueda>)HttpContext.Current.Session["DatosFormulas"];
        List<tempFormulas> listaFormulas = (List<tempFormulas>)HttpContext.Current.Session["ListaFormulas"];
        Dictionary<string, object> resultado = new Dictionary<string, object>();

        if (datosFormulas.Where(w => w.CodigoFormula == codigoFormula).FirstOrDefault() != null)
        {

            tempFormulas newtempFormulas = new tempFormulas();
            newtempFormulas.cantidad = int.Parse(cantidad);
            newtempFormulas.NombreFormula = datosFormulas.Where(w => w.CodigoFormula == codigoFormula).FirstOrDefault().NombreFormula;
            newtempFormulas.CodigoFormula = codigoFormula;


            //(HttpContext.Current.Session["ListaFormulas"] as List<tempFormulas>).Add(newtempFormulas);
            listaFormulas.Add(newtempFormulas);
            resultado.Add("Resultado", listaFormulas);
            return resultado;
        }
        else
        {
            resultado.Add("Error", "El producto no existe dentro de la vista: RaulFormulas");
            return resultado;
        }
    }

    [WebMethod(EnableSession = true)]
    public static IDictionary<string, object> EliminarFormula(string codigoFormula)
    {
        Dictionary<string, object> resultado = new Dictionary<string, object>();
        List<tempFormulas> listaFormulas = (List<tempFormulas>)HttpContext.Current.Session["ListaFormulas"];
        listaFormulas.Remove(listaFormulas.Where(w => w.CodigoFormula == codigoFormula).FirstOrDefault());
        HttpContext.Current.Session["ListaFormulas"] = listaFormulas;

        resultado.Add("Resultado", listaFormulas);
        return resultado;
        
        
    }

    [WebMethod(EnableSession = true)]
    public static object CalcularOrden()
    {
        string detalle = "";
        Dictionary<string, object> datos = new Dictionary<string, object>();
        Marzzan_InfolegacyDataContext contexto = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
        Marzzan_BejermanDataContext contextoBejerman = (Marzzan_BejermanDataContext)HttpContext.Current.Session["ContextoBejerman"];


        try
        {

            List<tempFormulas> listaFormulas = (List<tempFormulas>)HttpContext.Current.Session["ListaFormulas"];
            List<tempComponentes> listaDetalleComponentes = new List<tempComponentes>();


            /// de todas la formulas recupero solo los codigos
            List<string> codigos = listaFormulas.Select(w => w.CodigoFormula).ToList();

            /// Con los codigos de las formulas recupero todos los componentes
            List<RaulFormula> datosComponentes = (from d in contextoBejerman.RaulFormulas
                                                  where codigos.Contains(d.producto.Replace("-", "").Replace(" ", ""))
                                                  select d).ToList();

            /// Recupero los codigos de los componentes que voy a utilizar
            /// para el calculo
            List<string> codigosComponentes = (from d in datosComponentes
                                               select d.componente).Distinct().ToList();

            detalle += "Codigo de los Componentes: " + string.Join("-", codigosComponentes.ToArray());

            /// calculo la cantidad necesaria para cada componente segun las cantidades 
            /// de cada una de las formulas.
            var cantidadesNecesarias = (from f in listaFormulas
                                        join c in datosComponentes on f.CodigoFormula equals c.producto.Replace("-", "").Replace(" ", "")
                                        select new
                                        {
                                            CantidadNecesaria = c.Expr1 * f.cantidad,
                                            CodigoComponente = c.componente,
                                            NombreComponente = c.art_DescGen,
                                            Producto = f
                                        }).Distinct().ToList();

            detalle += "Cantidad de Reg CantidadesNecesarias: " + cantidadesNecesarias.Count.ToString();

            foreach (string codigoComponente in codigosComponentes)
            {
                
                RaulFormula componente = datosComponentes.Where(w => w.componente == codigoComponente).FirstOrDefault();
                string nroDepositoPrincipal = componente.art_DescGen.ToLower().Contains("quido aromatizante") ? "31" : "74";
                double CantidadTotalNecesaria = cantidadesNecesarias.Where(w => w.CodigoComponente == codigoComponente).Sum(w => w.CantidadNecesaria).Value;
                double CantidadTotalNecesariaInicial = CantidadTotalNecesaria ; 
                RaulFormula objformula = datosComponentes.Where(w => w.componente == codigoComponente && w.stkdep_Cod == nroDepositoPrincipal).FirstOrDefault();
                double CantidadDisponible = 0;
                bool HayDisponibilidadGeneral = false;
                
                if (objformula!=null)
                    CantidadDisponible = objformula.stk_CantUM1;


                if (CantidadDisponible > 0)
                {
                    tempComponentes comp = new tempComponentes();
                    comp.Desposito = nroDepositoPrincipal.ToString();
                    comp.Disponible = CantidadDisponible.ToString();
                    comp.Cantidad = CantidadTotalNecesaria > CantidadDisponible ? CantidadDisponible.ToString() : CantidadTotalNecesaria.ToString();
                    comp.DescripcionComonente = componente.art_DescGen + " - " + componente.componente + " - Total Necesario: " + CantidadTotalNecesariaInicial.ToString();
                    comp.CodigoComponente = componente.componente;
                    listaDetalleComponentes.Add(comp);
                    CantidadTotalNecesaria -= CantidadDisponible;
                    HayDisponibilidadGeneral = true;
                   
                }

                double CantidadTotalNecesariaSubDepositos = CantidadTotalNecesaria;
                if (CantidadTotalNecesaria > 0)
                {
                    foreach (var itemOtrosDepositos in datosComponentes.Where(w => w.componente == codigoComponente && w.stkdep_Cod != nroDepositoPrincipal).ToList())
                    {
                        
                        if (itemOtrosDepositos.stk_CantUM1 > 0)
                        {
                            //if (!listaDetalleComponentes.Any(w => w.CodigoComponente.Trim() == componente.componente.Trim()))
                            //{
                                tempComponentes comp = new tempComponentes();
                                comp.Desposito = itemOtrosDepositos.stkdep_Cod.ToString();
                                comp.Disponible = itemOtrosDepositos.stk_CantUM1.ToString();

                                /// Si la cantidad necesaria es mayor a la cantidad que tiene el deposito entonces se 
                                /// usa el total del deposito, caso contrario uso la cantidad necesaria.
                                comp.Cantidad = CantidadTotalNecesariaSubDepositos >= itemOtrosDepositos.stk_CantUM1 ? itemOtrosDepositos.stk_CantUM1.ToString() : CantidadTotalNecesariaSubDepositos.ToString();
                                
                                comp.DescripcionComonente = componente.art_DescGen + " - " + componente.componente + " - Total Necesario: " + CantidadTotalNecesariaInicial.ToString();
                                comp.CodigoComponente = componente.componente;
                                listaDetalleComponentes.Add(comp);
                                HayDisponibilidadGeneral = true;
                                CantidadTotalNecesariaSubDepositos -= itemOtrosDepositos.stk_CantUM1;

                            //}
                        }

                    }

                }
                else
                {
                    HayDisponibilidadGeneral = true;
                    CantidadTotalNecesaria = 0;
                }

                /// Aca entra cdo no hay ningun deposito con disponibilidad
                if (!HayDisponibilidadGeneral )
                {
                    tempComponentes comp = new tempComponentes();
                    comp.Desposito = "Sin Stock";
                    comp.Disponible = "Sin Stock";
                    comp.Cantidad =  CantidadTotalNecesaria.ToString();
                    comp.DescripcionComonente = componente.art_DescGen + " - " + componente.componente + " - Total Necesario: " + CantidadTotalNecesariaInicial.ToString();
                    comp.CodigoComponente = componente.componente;
                    listaDetalleComponentes.Add(comp);
                }
                else if (CantidadTotalNecesariaSubDepositos > 0)
                {
                    tempComponentes comp = new tempComponentes();
                    comp.Desposito = "Sin Stock";
                    comp.Disponible = "Sin Stock";
                    comp.Cantidad = CantidadTotalNecesariaSubDepositos.ToString();
                    comp.DescripcionComonente = componente.art_DescGen + " - " + componente.componente + " - Total Necesario: " + CantidadTotalNecesariaInicial.ToString();
                    comp.CodigoComponente = componente.componente;
                    listaDetalleComponentes.Add(comp);
                }


            }

            HttpContext.Current.Session["ConsumoComponentes"] = listaDetalleComponentes.OrderBy(w => w.DescripcionComonente).ToList();

            datos.Add("Detalle", "");
            datos.Add("Datos", (HttpContext.Current.Session["ConsumoComponentes"] as List<tempComponentes>).Take(200).ToList());


            return datos;
        }
        catch (Exception err)
        {
            datos.Add("Detalle", err.Message + err.StackTrace);
            datos.Add("Datos", null);
            
            return datos;
        }

    }




}
