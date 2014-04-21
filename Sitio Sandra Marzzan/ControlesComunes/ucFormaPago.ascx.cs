using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Web.Services;


public partial class Registraciones_Controles_ucFormaPago : BaseUserControl
{
    public Dictionary<string, object> DescripcionDatosAdicionales
    { get; set; }

    public bool? ValidarControl
    {
        get;
        set;
    }

    public List<long> FormaPagoHabilitadas
    {
        get;
        set;
    }

    public string FormaPagoInicial
    {
        get;
        set;
    }

    //public ConfLineasBase _currentConfLinea
    //{
    //    get
    //    {
    //        object value = Session["CurrentConfLinea"];

    //        return (value == null) ? null : (ConfLineasBase)value;
    //    }
    //    set
    //    {
    //        Session["CurrentConfLinea"] = value;
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        Cargarcombos();
    }


    private void Cargarcombos()
    {
        this.cboTipoFormadePago.Items.Add(new RadComboBoxItem("Seleccione el tipo de rendicion", "-1"));
        this.cboTipoFormadePago.Items.Add(new RadComboBoxItem("Efectivo", "0"));
        this.cboTipoFormadePago.Items.Add(new RadComboBoxItem("Deposito Bancario", "1"));
        this.cboTipoFormadePago.Items.Add(new RadComboBoxItem("Tranferencia Bancaria", "2"));
        this.cboTipoFormadePago.Items.Add(new RadComboBoxItem("Cheque", "3"));
        this.cboTipoFormadePago.Items.Add(new RadComboBoxItem("Otros", "4"));

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if ((ValidarControl.HasValue && !ValidarControl.Value) || divCheque.Style[HtmlTextWriterStyle.Display] == "none")
        {
            RequiredFieldValidatorNroCheque.Enabled = false;
        }
    }

    ///// <summary>
    ///// Metodo que se utiliza para verificar si los datos ingresados
    ///// para la forma de pago cheque propios no dan como resultado un
    ///// item existente.
    ///// </summary>
    ///// <returns></returns>
    //public bool ItemValido()
    //{
    //    InfoLinea InfoLin = new InfoLinea();
    //    InfoLin.IdConfLineasBase = _currentConfLinea.IdConfLineaBase;
    //    if (!_currentConfLinea.objMaestroReference.IsLoaded) { _currentConfLinea.objMaestroReference.Load(); }
    //    InfoLin.IdMaestro = Convert.ToInt64(cboTipoFormadePago.SelectedValue);

    //    /// Por lógica de negocio el cheque solo puede existir una sola vez
    //    /// para el par de datos de serialización Nro y Bco.
    //    if (InfoLin.IdMaestro == 10998)
    //    {
    //        ConfDatosAdicionalesSerializacion da1 = NegocioConfDatosAdicionales.GetInstance().GetConfDatosAdicionalesSerializacion(InfoLin.IdMaestro, "Nro Cheque");
    //        ConfDatosAdicionalesSerializacion da2 = NegocioConfDatosAdicionales.GetInstance().GetConfDatosAdicionalesSerializacion(InfoLin.IdMaestro, "Banco");
    //        List<ConfDatosAdicionalesSerializacion> datosSer = new List<ConfDatosAdicionalesSerializacion>();
    //        datosSer.Add(da1);
    //        datosSer.Add(da2);

    //        Dictionary<long, object> valores = new Dictionary<long, object>();
    //        valores.Add(da1.IdConfDatoAdicional, this.txtNroCheque.Value);
    //        valores.Add(da2.IdConfDatoAdicional, this.cboBanco.SelectedValue);

    //        ItemsRegistracion itemCaja = NegocioItemsRegistracion.GetInstance().GetItemxDA(datosSer, valores, _currentConfLinea.objDeposito);

    //        if (itemCaja == null)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    else if (InfoLin.IdMaestro == 10999)
    //    {
    //        ConfDatosAdicionalesSerializacion da1 = NegocioConfDatosAdicionales.GetInstance().GetConfDatosAdicionalesSerializacion(InfoLin.IdMaestro, "Nro Cheque");
    //        ConfDatosAdicionalesSerializacion da2 = NegocioConfDatosAdicionales.GetInstance().GetConfDatosAdicionalesSerializacion(InfoLin.IdMaestro, "Banco");
    //        List<ConfDatosAdicionalesSerializacion> datosSer = new List<ConfDatosAdicionalesSerializacion>();
    //        datosSer.Add(da1);
    //        datosSer.Add(da2);

    //        Dictionary<long, object> valores = new Dictionary<long, object>();
    //        valores.Add(da1.IdConfDatoAdicional, this.txtNroChequeT.Value);
    //        valores.Add(da2.IdConfDatoAdicional, this.cboBancoT.SelectedValue);

    //        ItemsRegistracion itemCaja = NegocioItemsRegistracion.GetInstance().GetItemxDA(datosSer, valores, _currentConfLinea.objDeposito);

    //        if (itemCaja == null)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    else if (InfoLin.IdMaestro == 131406)
    //    {
    //        /// Se anulo el control para que se permitar utilizar el mismo item
    //        /// de transferencia en distintas operaciones.
    //        //ConfDatosAdicionalesSerializacion da1 = NegocioConfDatosAdicionales.GetInstance().GetConfDatosAdicionalesSerializacion(InfoLin.IdMaestro, "Fecha Transferencia");
    //        //ConfDatosAdicionalesSerializacion da2 = NegocioConfDatosAdicionales.GetInstance().GetConfDatosAdicionalesSerializacion(InfoLin.IdMaestro, "Nro Control");
    //        //ConfDatosAdicionalesSerializacion da3 = NegocioConfDatosAdicionales.GetInstance().GetConfDatosAdicionalesSerializacion(InfoLin.IdMaestro, "Nro Cta Destino");


    //        //List<ConfDatosAdicionalesSerializacion> datosSer = new List<ConfDatosAdicionalesSerializacion>();
    //        //datosSer.Add(da1);
    //        //datosSer.Add(da2);
    //        //datosSer.Add(da3);

    //        //Dictionary<long, object> valores = new Dictionary<long, object>();
    //        //valores.Add(da1.IdConfDatoAdicional, this.txtFechaTransferencia.SelectedDate);
    //        //valores.Add(da2.IdConfDatoAdicional, this.txtNroControlTransferencia.Text);
    //        //valores.Add(da3.IdConfDatoAdicional, this.txtNroCtaDestinoTransferencia.Text);


    //        //ItemsRegistracion itemCaja = NegocioItemsRegistracion.GetInstance().GetItemxDA(datosSer, valores, _currentConfLinea.objDeposito);

    //        //if (itemCaja == null)
    //        //{
    //        //    return true;
    //        //}
    //        //else
    //        //{
    //        //    return false;
    //        //}

    //        return true;
    //    }
    //    else if (InfoLin.IdMaestro == 131407)
    //    {
    //        ConfDatosAdicionalesSerializacion da1 = NegocioConfDatosAdicionales.GetInstance().GetConfDatosAdicionalesSerializacion(InfoLin.IdMaestro, "Fecha Deposito");
    //        ConfDatosAdicionalesSerializacion da2 = NegocioConfDatosAdicionales.GetInstance().GetConfDatosAdicionalesSerializacion(InfoLin.IdMaestro, "Nro Cta Destino");
    //        ConfDatosAdicionalesSerializacion da3 = NegocioConfDatosAdicionales.GetInstance().GetConfDatosAdicionalesSerializacion(InfoLin.IdMaestro, "Nro Sobre");



    //        List<ConfDatosAdicionalesSerializacion> datosSer = new List<ConfDatosAdicionalesSerializacion>();
    //        datosSer.Add(da1);
    //        datosSer.Add(da2);
    //        datosSer.Add(da3);


    //        Dictionary<long, object> valores = new Dictionary<long, object>();
    //        valores.Add(da1.IdConfDatoAdicional, this.txtFechaDeposito.SelectedDate);
    //        valores.Add(da2.IdConfDatoAdicional, this.txtNroCtaDestino.Text);
    //        valores.Add(da3.IdConfDatoAdicional, this.txtNroSobre.Text);



    //        ItemsRegistracion itemCaja = NegocioItemsRegistracion.GetInstance().GetItemxDA(datosSer, valores, _currentConfLinea.objDeposito);

    //        if (itemCaja == null)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    return true;

    //}

    //private void Cargarcombos()
    //{

    //    this.cboTipoFormadePago.DataTextField = "Nombre";
    //    this.cboTipoFormadePago.DataValueField = "IdMaestroBase";

    //    if (FormaPagoHabilitadas != null && FormaPagoHabilitadas.Count > 0)
    //    {
    //        List<Fondos> colFondos = NegocioFondos.GetInstance().GetHijosById(_currentConfLinea.objMaestro.IdMaestroBase);
    //        this.cboTipoFormadePago.DataSource = colFondos.Where(w => FormaPagoHabilitadas.Contains(w.IdMaestroBase));
    //    }
    //    else
    //    {
    //        this.cboTipoFormadePago.DataSource = NegocioFondos.GetInstance().GetHijosById(_currentConfLinea.objMaestro.IdMaestroBase)
    //            .Where(w => w.Nombre.Contains("Pesos") || w.Nombre.Contains("Tercero") 
    //                || w.Nombre.Contains("Propios") || w.Nombre.Contains("Transfe") || w.Nombre.Contains("Ajuste"));
    //    }

    //    this.cboTipoFormadePago.DataBind();

    //    if (FormaPagoInicial == null)
    //    {
    //        if (this.cboTipoFormadePago.FindItemByValue(Maestros.PESOS.ToString()) != null)
    //        {
    //            this.cboTipoFormadePago.FindItemByValue(Maestros.PESOS.ToString()).Selected = true;
    //        }
    //        else
    //        {
    //            this.cboTipoFormadePago.Items.First().Selected = true;
    //        }
    //    }
    //    else
    //    {

    //       // divCheque.Style.Add(HtmlTextWriterStyle.Display, "none");

    //        if (this.cboTipoFormadePago.FindItemByValue(FormaPagoInicial) != null)
    //        {
    //            this.cboTipoFormadePago.FindItemByValue(FormaPagoInicial).Selected = true;
    //        }
    //        else if (this.cboTipoFormadePago.Items.Count > 0)
    //        {
    //            this.cboTipoFormadePago.Items.First().Selected = true;
    //        }
    //    }

    //    this.cboBanco.DataTextField = "Nombre";
    //    this.cboBanco.DataValueField = "IdMaestroBase";
    //    this.cboBanco.DataSource = NegocioClasificaciones.GetInstance().GetHijosById(Maestros.ENTIDADES_BANCARIAS);
    //    this.cboBanco.DataBind();


    //    this.cboBancoT.DataTextField = "Nombre";
    //    this.cboBancoT.DataValueField = "IdMaestroBase";
    //    this.cboBancoT.DataSource = NegocioClasificaciones.GetInstance().GetHijosById(Maestros.ENTIDADES_BANCARIAS);
    //    this.cboBancoT.DataBind();

    //}

    //public decimal GetValor()
    //{
    //    if (cboTipoFormadePago.SelectedValue == Maestros.CHEQUES_PROPIOS.ToString())
    //    {
    //        return decimal.Parse(txtMontoCheque.Text.Replace(".", ","));
    //    }
    //    else if (cboTipoFormadePago.SelectedValue == Maestros.PESOS.ToString())
    //    {
    //        return decimal.Parse(txtMonto.Text.Replace(".", ","));
    //    }
    //    else if (cboTipoFormadePago.SelectedValue ==  Maestros.CHEQUES_TERCEROS.ToString())
    //    {
    //        return decimal.Parse(txtMontoChequeT.Text.Replace(".", ","));
    //    }
    //    else if (cboTipoFormadePago.SelectedValue == Maestros.DEPOSITOBANCARIO.ToString())
    //    {
    //        return decimal.Parse(txtMontoDepositoBanco.Text.Replace(".", ","));
    //    }
    //    else if (cboTipoFormadePago.SelectedValue ==  Maestros.TRANSFERENCIABANCARIA.ToString())
    //    {
    //        return decimal.Parse(txtMontoTransferencia.Text.Replace(".", ","));
    //    }
    //    else if (cboTipoFormadePago.SelectedValue ==  Maestros.AJUSTE.ToString())
    //    {
    //        return decimal.Parse(txtMonto.Text.Replace(".", ","));
    //    }

    //    return 0;
    //}

    //public string GetDescripcionFormaPago()
    //{
    //    return cboTipoFormadePago.Text;
    //}

    //public double SetValorLimite
    //{
    //    set
    //    {
    //        double valor = 0;
    //        if (value >= 0)
    //        {
    //            valor = value; 
    //        }

    //        txtMontoCheque.MaxValue = Math.Round(valor, 2);
    //        txtMonto.MaxValue = Math.Round(valor, 2);
    //        txtMontoChequeT.MaxValue = Math.Round(valor, 2);
    //        txtMontoDepositoBanco.MaxValue = Math.Round(valor, 2);
    //        txtMontoTransferencia.MaxValue = Math.Round(valor, 2);


    //        txtMontoCheque.Value = Math.Round(valor, 2);
    //        txtMonto.Value = Math.Round(valor, 2);
    //        txtMontoChequeT.Value = Math.Round(valor, 2);
    //        txtMontoDepositoBanco.Value = Math.Round(valor, 2);
    //        txtMontoTransferencia.Value = Math.Round(valor, 2);

    //        HideValorLimite.Value = Convert.ToString(valor).Replace(",", ".");
    //        upDetalle.Update();
    //    }

    //}

    //public string SetNombreAdjudicacario
    //{
    //    set
    //    {
    //        this.txtAdjudicatario.Text = value;
    //    }
    //}


    //[UserControlScriptMethod]
    //[WebMethod(EnableSession = true)]
    //public static Dictionary<string, object> BusquedaTransferencia(string Fecha, string NroCta, string NroControl, string idMaestro)
    //{
    //    long IdMaestro = long.Parse(idMaestro);
    //    MaestrosBase objDeposito = (HttpContext.Current.Session["CurrentConfLinea"] as ConfLineasBase).objDeposito;
    //    Dictionary<string, object> resultado = new Dictionary<string, object>();

    //    ConfDatosAdicionalesSerializacion da1 = NegocioConfDatosAdicionales.GetInstance().GetConfDatosAdicionalesSerializacion(IdMaestro, "Fecha Transferencia");
    //    ConfDatosAdicionalesSerializacion da2 = NegocioConfDatosAdicionales.GetInstance().GetConfDatosAdicionalesSerializacion(IdMaestro, "Nro Control");
    //    ConfDatosAdicionalesSerializacion da3 = NegocioConfDatosAdicionales.GetInstance().GetConfDatosAdicionalesSerializacion(IdMaestro, "Nro Cta Destino");

    //    List<ConfDatosAdicionalesSerializacion> datosSer = new List<ConfDatosAdicionalesSerializacion>() { da1, da2, da3 };
    //    Dictionary<long, object> valores = new Dictionary<long, object>() { { da1.IdConfDatoAdicional, DateTime.Parse(Fecha) }, { da2.IdConfDatoAdicional, NroControl }, { da3.IdConfDatoAdicional, NroCta} };

    //    ItemsRegistracion itemTrans = NegocioItemsRegistracion.GetInstance().GetItemxDA(datosSer, valores, objDeposito);

    //    if (itemTrans != null)
    //    {
    //        resultado.Add("ItemExistente", true);
    //        resultado.Add("Concepto", itemTrans.colUsosDatosAdicionales.Where(w => w.objConfDatoAdicional.Nombre == "Concepto").First().Valor);
    //        resultado.Add("Referencia", itemTrans.colUsosDatosAdicionales.Where(w => w.objConfDatoAdicional.Nombre == "Referencia").First().Valor);
    //    }


    //    return resultado;
    //}
}
