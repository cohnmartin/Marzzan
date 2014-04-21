using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Web.Services;
using System.Web.Script.Services;
using CommonMarzzan;


[ScriptService]
public partial class ucFormaPagoMultiple : BaseUserControl
{
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

    private List<TempFormasPago> ListaPagos
    {
        get
        {

            if (Session["ListaPagos"] == null)
            {
                Session.Add("ListaPagos", new List<TempFormasPago>());
            }

            return (List<TempFormasPago>)Session["ListaPagos"];
        }
        set
        {
            Session["ListaPagos"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ListaPagos = new List<TempFormasPago>();

            this.cboTipoFormadePago.Items.Add(new RadComboBoxItem("Seleccione el tipo de rendicion", "-1"));
            this.cboTipoFormadePago.Items.Add(new RadComboBoxItem("Efectivo", "0"));
            this.cboTipoFormadePago.Items.Add(new RadComboBoxItem("Deposito Bancario", "1"));
            this.cboTipoFormadePago.Items.Add(new RadComboBoxItem("Tranferencia Bancaria", "2"));
            this.cboTipoFormadePago.Items.Add(new RadComboBoxItem("Cheque", "3"));
            this.cboTipoFormadePago.Items.Add(new RadComboBoxItem("Otros", "4"));


            List<Parametro> EntidadesBancarias = (from ent in Contexto.Parametros
                                                  where ent.Tipo == "ENTIDADESBANCARIAS"
                                                  select ent).ToList();

            this.cboBanco.DataTextField = "Valor";
            this.cboBanco.DataValueField = "IdParametro";
            this.cboBanco.DataSource = EntidadesBancarias;
            this.cboBanco.DataBind();


            this.cboBancoT.DataTextField = "Valor";
            this.cboBancoT.DataValueField = "IdParametro";
            this.cboBancoT.DataSource = EntidadesBancarias;
            this.cboBancoT.DataBind();


            List<Parametro> Operadores = (from ent in Contexto.Parametros
                                          where ent.Tipo == "OPERADORES"
                                          select ent).ToList();


            this.cboOperadores.DataTextField = "Valor";
            this.cboOperadores.DataValueField = "IdParametro";
            this.cboOperadores.DataSource = Operadores;
            this.cboOperadores.DataBind();


            List<object> datos = new List<object>();

        }

    }


    public static void CargarPagos(List<DetallePagoRendicion> pagos)
    {
        foreach (DetallePagoRendicion datosPago in pagos)
        {

            TempFormasPago newFP = new TempFormasPago();
            ((List<TempFormasPago>)HttpContext.Current.Session["ListaPagos"]).Add(newFP);


            newFP.IdFormaPago = datosPago.TipoPago;
            newFP.Monto = datosPago.Monto;
            newFP.DescFormaPago = datosPago.objTipoPago.Valor;
            newFP.DescMonto = string.Format("{0:#.00}", datosPago.Monto);
            newFP.IdDetalle = datosPago.IdDetallePagoRendicion;

            switch (datosPago.objTipoPago.Contexto)
            {
                case "0":
                    newFP.DescGeneral = "Pago en Efectivo";
                    break;
                case "1":
                    newFP.DescGeneral = "<b>Fecha Deposito: </b>" + datosPago.D_FechaDeposito.Value.ToShortDateString() + " <b>Nro Transacción: </b>" + datosPago.D_NroTransaccion;

                    newFP.D_NroCtaDestino = datosPago.D_NroCtaDestino;
                    newFP.D_NroTransaccion = datosPago.D_NroTransaccion;
                    newFP.D_FechaDeposito = datosPago.D_FechaDeposito;

                    break;
                case "2":
                    newFP.DescGeneral = "<b>Fecha Transferencia: </b>" + datosPago.T_FechaTranferencia.Value.ToShortDateString() + " <b>Nro Control: </b>" + datosPago.T_NroControl + " <b>Referencia: </b>" + datosPago.T_Referencia;

                    newFP.T_NroCtaDestino = datosPago.T_NroCtaDestino;
                    newFP.T_Referencia = datosPago.T_Referencia;
                    newFP.T_NroControl = datosPago.T_NroControl;
                    newFP.T_FechaTranferencia = datosPago.T_FechaTranferencia;


                    break;
                case "3":
                    newFP.DescGeneral = "<b>Fecha Cobro: </b>" + datosPago.CH_FechaCobro.Value.ToShortDateString() + " <b>Nro Cheque: </b>" + datosPago.CH_NroCheque + " <b>Banco: </b>" + datosPago.objBanco.Valor;

                    newFP.CH_NroCheque = datosPago.CH_NroCheque;
                    newFP.IdBanco = datosPago.CH_Banco;
                    newFP.CH_Titular = datosPago.CH_Titular;
                    newFP.CH_FechaCobro = datosPago.CH_FechaCobro;

                    break;
                case "4":
                    newFP.DescGeneral = "<b>Fecha Pago: </b>" + datosPago.O_FechaPago.Value.ToShortDateString() + " <b>Nro: </b>" + datosPago.O_NroOperacion + " <b>Operador: </b>" + datosPago.objOperador.Valor;

                    newFP.O_NroOperacion = datosPago.O_NroOperacion;
                    newFP.O_FechaPago = datosPago.O_FechaPago;
                    newFP.O_IdOperador = datosPago.O_Operador;
                    break;
                default:
                    break;
            }


        }





    }


    [UserControlScriptMethod]
    [WebMethod(EnableSession = true)]
    public static object RecuperarPagosCargados()
    {
        List<TempFormasPago> datos = ((List<TempFormasPago>)HttpContext.Current.Session["ListaPagos"]).OrderBy(w => w.DescFormaPago).ToList();
        return datos;
    }


    [UserControlScriptMethod]
    [WebMethod(EnableSession = true)]
    public static object AgregarPago(object Valores)
    {
        IDictionary<string, object> datosPago = (IDictionary<string, object>)Valores;
        List<TempFormasPago> DatosIngresados = ((List<TempFormasPago>)HttpContext.Current.Session["ListaPagos"]);
        TempFormasPago newFP = null;
        long idTipoPago = long.Parse(datosPago["IdFormaPago"].ToString());

        /// Verifico si hay pago ingresados, segun la clave de cada pago.
        /// si es asi entonces actualizo el pago con lo valores ingresados.
        if (idTipoPago == 0 && DatosIngresados.Any(w => w.IdFormaPago == 0))
        {
            newFP = DatosIngresados.Where(w => w.IdFormaPago == 0).FirstOrDefault();
        }
        else
        {
            newFP = new TempFormasPago();
            ((List<TempFormasPago>)HttpContext.Current.Session["ListaPagos"]).Add(newFP);
        }


        newFP.IdFormaPago = idTipoPago;
        newFP.Monto = decimal.Parse(datosPago["Monto"].ToString());
        newFP.DescFormaPago = datosPago["DescripcionFormaPago"].ToString();
        newFP.DescMonto = string.Format("{0:#.00}", decimal.Parse(datosPago["Monto"].ToString()));
        newFP.IdDetalle = new Random().Next() * -1;


        switch (datosPago["IdFormaPago"].ToString())
        {
            case "0":
                newFP.DescGeneral = "Pago en Efectivo";
                break;
            case "1":
                newFP.DescGeneral = "<b>Fecha Deposito: </b>" + datosPago["FechaDeposito"].ToString().Trim() + " <b>Nro Transacción: </b>" + datosPago["NroTransaccion"].ToString();

                newFP.D_NroCtaDestino = datosPago["NroCtaDestino"].ToString();
                newFP.D_NroTransaccion = datosPago["NroTransaccion"].ToString();
                newFP.D_FechaDeposito = DateTime.Parse(datosPago["FechaDeposito"].ToString());

                break;
            case "2":
                newFP.DescGeneral = "<b>Fecha Transferencia: </b>" + datosPago["FechaTransferancia"].ToString().Trim() + " <b>Nro Control: </b>" + datosPago["NroControl"].ToString() + " <b>Referencia: </b>" + datosPago["Referencia"].ToString();

                newFP.T_NroCtaDestino = datosPago["NroCtaDestino"].ToString();
                newFP.T_Referencia = datosPago["Referencia"].ToString();
                newFP.T_NroControl = datosPago["NroControl"].ToString();
                newFP.T_FechaTranferencia = DateTime.Parse(datosPago["FechaTransferancia"].ToString());


                break;
            case "3":
                newFP.DescGeneral = "<b>Fecha Cobro: </b>" + datosPago["FechaCobro"].ToString().Trim() + " <b>Nro Cheque: </b>" + datosPago["NroCheque"].ToString() + " <b>Banco: </b>" + datosPago["DescripcionBanco"].ToString();

                newFP.CH_NroCheque = datosPago["NroCheque"].ToString();
                newFP.IdBanco = long.Parse(datosPago["IdBanco"].ToString());
                newFP.CH_Titular = datosPago["Titular"].ToString();
                newFP.CH_FechaCobro = DateTime.Parse(datosPago["FechaCobro"].ToString());

                break;
            case "4":
                newFP.DescGeneral = "<b>Fecha Pago: </b>" + datosPago["FechaPago"].ToString().Trim() + " <b>Nro: </b>" + datosPago["NroOperacion"].ToString() + " <b>Operador: </b>" + datosPago["DescripcionOperador"].ToString();

                newFP.O_NroOperacion = datosPago["NroOperacion"].ToString();
                newFP.O_FechaPago = DateTime.Parse(datosPago["FechaPago"].ToString());
                newFP.O_IdOperador = long.Parse(datosPago["IdOperador"].ToString());
                break;
            default:
                break;
        }






        return ((List<TempFormasPago>)HttpContext.Current.Session["ListaPagos"]).OrderBy(w => w.DescFormaPago);
    }

    [UserControlScriptMethod]
    [WebMethod(EnableSession = true)]
    public static object EliminarPago(object id)
    {
        TempFormasPago objTempEliminar = null;
        if (long.Parse(id.ToString()) > 0)
        {
            Marzzan_InfolegacyDataContext dc = (Marzzan_InfolegacyDataContext)HttpContext.Current.Session["Context"];
            var objEliminar = (from d in dc.DetallePagoRendicions
                               where d.IdDetallePagoRendicion == long.Parse(id.ToString())
                               select d).FirstOrDefault();

            dc.DetallePagoRendicions.DeleteOnSubmit(objEliminar);
            dc.SubmitChanges();

        }

        objTempEliminar = (from d in ((List<TempFormasPago>)HttpContext.Current.Session["ListaPagos"])
                           where d.IdDetalle == long.Parse(id.ToString())
                           select d).FirstOrDefault();

       ((List<TempFormasPago>)HttpContext.Current.Session["ListaPagos"]).Remove(objTempEliminar);

        return RecuperarPagosCargados();
    }
}