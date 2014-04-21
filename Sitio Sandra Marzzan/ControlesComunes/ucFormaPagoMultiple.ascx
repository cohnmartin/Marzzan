<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucFormaPagoMultiple.ascx.cs"
    Inherits="ucFormaPagoMultiple" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="AjaxInfo" %>
<style type="text/css">
    .style1
    {
        width: 90%;
    }
</style>
<telerik:RadCodeBlock runat="server">
    <script type="text/javascript">
        var montoTotal = 0;
        var isReadOnly = false;

        function ucFormaPagoMultiple_CargarFormasPago() {

            RecuperarPagosCargados(CallBackFunction, ErrorFunction);
        }

        function ucFormaPagoMultiple_ControlarFinalizacion() {
            if (Page_ClientValidate()) {
                var objCargado = ucFormaPagoMultiple_RecuperarValoresCargados();
                if (objCargado != null) {
                    AgregarPago(CallBackFunction, ErrorFunction, objCargado);
                    ucFormaPagoMultiple_HideCarga();
                }
            }

        }
        function ucFormaPagoMultiple_SetReadOnly() {
            isReadOnly = true;
        }

        function ucFormaPagoMultiple_EliminarPago(id) {
            EliminarPago(CallBackFunction, ErrorFunction, id);
        }


        function CallBackFunction(obj) {
            var datos = eval('(' + obj + ')');

            /// Seteo de la visualizacion del control
            var ctrlDelete = '<img onclick="ucFormaPagoMultiple_EliminarPago(#{IdDetalle});return false;" id="imgEliminar" width="15px" src="Imagenes/delete.gif" alt="Eliminar Pago" />';
            if (isReadOnly) {
                ctrlDelete = '&nbsp;'
                $("#trAgregarPago").hide();
            }




            var t = [{ "header": "Tipo" },
            { "header": "Monto" },
            { "header": "Detalle" },
            { "header": "&nbsp;"}];

            var templates = {
                th: '<th class="HeaderVista_Marzzan_sunset">#{header}</th>',
                td: '<tr style="cursor:pointer"  >' +
                '<td id="Tipo" align="left" style="width:160px;padding-left:15px">#{DescFormaPago}</td>' +
                '<td id="Monto" style="width:80px">#{DescMonto}</td>' +
                '<td id="Detalle" align="left" style="padding-left:10px">#{DescGeneral}</td>' +
                '<td align="center" style="width:20px;padding-top:3px;padding-bottom:3px" >' + ctrlDelete + '</td>' +
                '</tr>'
            };

            var table = '<table width="100%" id="tblPagos" border="1" cellpadding="0" cellspacing="0" style="color:White;font-size:13px; background-color: #990033;"><thead><tr>';

            /// Genero la fila de encabezado
            $.each(t, function (key, val) {
                table += $.tmpl(templates.th, val);
            });

            table += '</tr></thead><tbody>';

            /// Genero las filas del body
            row = 0;
            var totalIngresado = 0;
            for (var i = 0; i < datos.length; i++) {
                table += $.tmpl(templates.td, datos[row]);

                totalIngresado += parseFloat(datos[row]["Monto"]);


                row++;
            }

            /// Agrego la fila del total
            table += '<tr><td class="footer" colSpan="4">TOTAL INGRESADO:&nbsp;$&nbsp;<asp:Label ID="lblTotalIngresado" ForeColor="Black" runat="server" Text="' + totalIngresado.toFixed(2) + '"></asp:Label></td></tr>'
            table += '</tbody></table>';

            /// Asigno la tabla generada al div para dibujarla
            $("#divtblPagos")[0].innerHTML = table;
            $("#tblPagos tbody tr:odd").addClass("rowAlt2");
            $("#tblPagos tbody tr:even").addClass("rowSpl");


            // Calculo del Nuevo Saldo
            var saldo = $("#<%=lblSaldo.ClientID %>").text().replace(",", ".");
            var monto = $("#<%=lblTotal.ClientID %>").text().replace(",", ".");
            if (totalIngresado > 0)
                $("#<%=lblNuevoSaldo.ClientID %>").text((totalIngresado - (parseFloat(saldo * -1) + parseFloat(monto))).toFixed(2));
            else
                $("#<%=lblNuevoSaldo.ClientID %>").text("Debe Ingresar pagos...");





        }

        function ErrorFunction() {

        }

        function LimpiarSeleccionTipoPago() {

            var combo = $find("<%= cboTipoFormadePago.ClientID %>");
            combo.trackChanges();
            combo.get_items().getItem(0).select();
            combo.updateClientState();
            combo.commitChanges();
        }

        function IngresoValor(valor) {
            if (valor > 0)
                return true
            else {
                alert("Debe ingresar un valor para el pago que intenta ingresar");
                return false;
            }
        }


        function ucFormaPagoMultiple_RecuperarValoresCargados() {
            var obj = null;
            var ValueSelected = $find("<%= cboTipoFormadePago.ClientID %>").get_selectedItem().get_value();
            var valoresResult = "";
            var Descripciones = "";


            // Efectivo
            if (ValueSelected == '0') {

                if (IngresoValor($find("<%=txtMonto.ClientID %>").get_value())) {
                    obj = new Object();
                    obj.IdFormaPago = ValueSelected;
                    obj.Monto = $find("<%=txtMonto.ClientID %>").get_value();
                    obj.DescripcionFormaPago = $find("<%= cboTipoFormadePago.ClientID %>").get_selectedItem().get_text();

                    /// Limpio los datos ingresados
                    $find("<%=txtMonto.ClientID %>").set_value("0");
                    LimpiarSeleccionTipoPago();
                }

            }
            // Deposito Bancario
            else if (ValueSelected == '1') {
                if (IngresoValor($find("<%=txtMontoDepositoBanco.ClientID %>").get_value())) {
                    obj = new Object();
                    obj.IdFormaPago = ValueSelected;
                    obj.DescripcionFormaPago = $find("<%= cboTipoFormadePago.ClientID %>").get_selectedItem().get_text();
                    obj.Monto = $find("<%=txtMontoDepositoBanco.ClientID %>").get_value();

                    obj.NroCtaDestino = $find("<%=txtNroCtaDestino.ClientID %>").get_value();
                    obj.NroTransaccion = $find("<%=txtNroTransferencia.ClientID %>").get_value();
                    obj.FechaDeposito = $find("<%=txtFechaDeposito.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

                    /// Limpio los datos ingresados
                    $find("<%=txtMontoDepositoBanco.ClientID %>").set_value("0");
                    $find("<%=txtNroCtaDestino.ClientID %>").set_value("");
                    $find("<%=txtNroTransferencia.ClientID %>").set_value("");
                    $find("<%=txtFechaDeposito.ClientID %>").set_selectedDate(null);

                    LimpiarSeleccionTipoPago();
                }

            }
            // Transferencia Bancaria
            else if (ValueSelected == '2') {
                if (IngresoValor($find("<%=txtMontoTransferencia.ClientID %>").get_value())) {
                    obj = new Object();
                    obj.IdFormaPago = ValueSelected;
                    obj.DescripcionFormaPago = $find("<%= cboTipoFormadePago.ClientID %>").get_selectedItem().get_text();
                    obj.Monto = $find("<%=txtMontoTransferencia.ClientID %>").get_value();

                    obj.NroCtaDestino = $find("<%=txtNroCtaDestinoTransferencia.ClientID %>").get_value();
                    obj.Referencia = $find("<%=txtReferencia.ClientID %>").get_value();
                    obj.NroControl = $find("<%=txtNroControlTransferencia.ClientID %>").get_value();
                    obj.FechaTransferancia = $find("<%=txtFechaTransferencia.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

                    /// Limpio los datos ingresados
                    $find("<%=txtMontoTransferencia.ClientID %>").set_value("0");
                    $find("<%=txtNroCtaDestinoTransferencia.ClientID %>").set_value("");
                    $find("<%=txtReferencia.ClientID %>").set_value("");
                    $find("<%=txtNroControlTransferencia.ClientID %>").set_value("");
                    $find("<%=txtFechaTransferencia.ClientID %>").set_selectedDate(null);

                    LimpiarSeleccionTipoPago();
                }

            }
            // Cheque Terceros
            else if (ValueSelected == '3') {
                if (IngresoValor($find("<%=txtMontoChequeT.ClientID %>").get_value())) {
                    obj = new Object();
                    obj.IdFormaPago = ValueSelected;
                    obj.Monto = $find("<%=txtMontoChequeT.ClientID %>").get_value();
                    obj.NroCheque = $find("<%=txtNroChequeT.ClientID %>").get_value();
                    obj.IdBanco = $find("<%=cboBancoT.ClientID %>").get_selectedItem().get_value();
                    obj.Titular = $find("<%=txtTitular.ClientID %>").get_value();
                    obj.FechaCobro = $find("<%=txtFechaEmisionT.ClientID %>").get_selectedDate().format("dd/MM/yyyy");
                    obj.DescripcionBanco = $find("<%=cboBancoT.ClientID %>").get_selectedItem().get_text();
                    obj.DescripcionFormaPago = $find("<%= cboTipoFormadePago.ClientID %>").get_selectedItem().get_text();

                    /// Limpio los datos ingresados
                    $find("<%=txtMontoChequeT.ClientID %>").set_value("0");
                    $find("<%=txtNroChequeT.ClientID %>").set_value("");
                    $find("<%=txtTitular.ClientID %>").set_value("");
                    $find("<%=txtFechaEmisionT.ClientID %>").set_selectedDate(null);
                    LimpiarSeleccionTipoPago();
                }

            }

            // Otros
            else if (ValueSelected == '4') {
                if (IngresoValor($find("<%=txtMontoOtros.ClientID %>").get_value())) {
                    obj = new Object();
                    obj.IdFormaPago = ValueSelected;
                    obj.DescripcionFormaPago = $find("<%= cboTipoFormadePago.ClientID %>").get_selectedItem().get_text();
                    obj.Monto = $find("<%=txtMontoOtros.ClientID %>").get_value();
                    obj.NroOperacion = $find("<%=txtNroOperacionOtros.ClientID %>").get_value();
                    obj.FechaPago = $find("<%=txtFechaOtros.ClientID %>").get_selectedDate().format("dd/MM/yyyy");
                    obj.IdOperador = $find("<%=cboOperadores.ClientID %>").get_selectedItem().get_value();
                    obj.DescripcionOperador = $find("<%=cboOperadores.ClientID %>").get_selectedItem().get_text();

                    /// Limpio los datos ingresados
                    $find("<%=txtMontoOtros.ClientID %>").set_value("0");
                    $find("<%=txtNroOperacionOtros.ClientID %>").set_value(""); ;
                    $find("<%=txtFechaOtros.ClientID %>").set_selectedDate(null);
                    LimpiarSeleccionTipoPago();
                }


            }

            return obj;

        }

        function ucFormaPagoMultiple_HideCarga(Acepto) {


            return false;
        }

        function ucFormaPagoMultiple_ShowCarga() {

            $('#divPrincipalFormaPago').modal({ clickClose: false, showClose: true, zIndex: 99999999 });


            return false;
        }


        function HideCargaEdicion(idDetalle) {


        }

        function ShowEdicion(idDetalle, monto, FormaPago) {



        }

        function Eliminar(idDetalle) {

        }

        function FinAccion(result) {

        }

        function faloEliminacion() {
            alert("Fallo de la eliminar");
        }

        $(this).ready(function () {


        });

        function InitRetardado() {


        }

        function InitGridFP() {

        }

        function InitReadonly() {
            window.setTimeout(function () {
                IniciarSoloLectura(FinAccion, faloEliminacion);
            }, 100);
        }

        function IniciarVisualizacion() {
            SetVisualizacion("");
        }

        function ucFormaPagoMultiple_SetTotales(saldo, monto) {

            $("#<%=lblSaldo.ClientID %>").text(saldo);
            $("#<%=lblTotal.ClientID %>").text(monto);

            $("#<%=lblTotalGeneral.ClientID %>").text((parseFloat(saldo * -1) + parseFloat(monto)).toFixed(2));

            if (ucFormaPagoMultiple_GetTotalIngresado() > 0)
                $("#<%=lblNuevoSaldo.ClientID %>").text(ucFormaPagoMultiple_GetTotalIngresado() - (parseFloat(saldo) + parseFloat(monto)));


        }

        function ucFormaPagoMultiple_GetTotalGuias() {
            return parseFloat($("#<%=lblTotal.ClientID %>").text());
        }

        function ucFormaPagoMultiple_GetTotal() {
            return parseFloat($("#<%=lblTotalGeneral.ClientID %>").text());
        }

        function ucFormaPagoMultiple_GetTotalIngresado() {
            return parseFloat($("#<%=lblTotalIngresado.ClientID %>").text());
        }
        
        

    </script>
    <script language="javascript" type="text/javascript">


        //        function BuscarTransferenciaExistente(sender, args) {
        //            var fecha = ""
        //            if ($find("<%=txtFechaTransferencia.ClientID %>").get_selectedDate() != "")
        //                var fecha = $find("<%=txtFechaTransferencia.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

        //            var nroCta = $find("<%=txtNroCtaDestinoTransferencia.ClientID %>").get_value();
        //            var nroControl = $find("<%=txtNroControlTransferencia.ClientID %>").get_value();
        //            var idMaestro = $find("<%= cboTipoFormadePago.ClientID %>").get_selectedItem().get_value();

        //            if (fecha != "" && nroCta != "" && nroControl != "")
        //                BusquedaTransferencia(FinBusqueda, falloBusqueda, fecha, nroCta, nroControl, idMaestro);
        //        }

        //        function FinBusqueda(result) {
        //            var obj = eval('(' + result + ')');

        //            if (obj["ItemExistente"] != null) {

        //                $find("<%=txtReferencia.ClientID %>").set_value(obj["Referencia"]);
        //            }
        //        }

        //        function falloBusqueda() {

        //        }

        //        function GetValorFormaPago() {
        //            var ValueSelected = $find("<%= cboTipoFormadePago.ClientID %>").get_selectedItem().get_value();
        //            var valoresResult = "";
        //            var Descripciones = "";
        //            debugger;
        //            /// Cheques propios
        //            if (ValueSelected == '10998') {
        //                valoresResult += ValueSelected + "|";
        //                valoresResult += $find("<%=txtMontoCheque.ClientID %>").get_value() + "|";
        //                valoresResult += $find("<%=txtNroCheque.ClientID %>").get_value() + "|";
        //                valoresResult += $find("<%=cboBanco.ClientID %>").get_selectedItem().get_value() + "|";
        //                valoresResult += $find("<%=txtFechaEmision.ClientID %>").get_selectedDate().format("dd/MM/yyyy") + "|";
        //                valoresResult += $find("<%=txtAdjudicatario.ClientID %>").get_value();

        //                Descripciones += $find("<%= cboTipoFormadePago.ClientID %>").get_selectedItem().get_text() + "|";
        //                Descripciones += $find("<%=cboBanco.ClientID %>").get_selectedItem().get_text();

        //            }
        //            // Efectivo
        //            else if (ValueSelected == '10721' || ValueSelected == '132169') {
        //                valoresResult += ValueSelected + "|";
        //                valoresResult += $find("<%=txtMonto.ClientID %>").get_value();

        //                Descripciones += $find("<%= cboTipoFormadePago.ClientID %>").get_selectedItem().get_text() + "|";
        //            }
        //            // Cheque Terceros
        //            else if (ValueSelected == '10999') {
        //                valoresResult += ValueSelected + "|";
        //                valoresResult += $find("<%=txtMontoChequeT.ClientID %>").get_value() + "|";
        //                valoresResult += $find("<%=txtNroChequeT.ClientID %>").get_value() + "|";
        //                valoresResult += $find("<%=cboBancoT.ClientID %>").get_selectedItem().get_value() + "|";
        //                valoresResult += $find("<%=txtFechaEmisionT.ClientID %>").get_selectedDate().format("dd/MM/yyyy") + "|";
        //                valoresResult += $find("<%=txtTitular.ClientID %>").get_value();



        //                Descripciones += $find("<%= cboTipoFormadePago.ClientID %>").get_selectedItem().get_text() + "|";
        //                Descripciones += $find("<%=cboBancoT.ClientID %>").get_selectedItem().get_text();

        //            }
        //            // Deposito Bancario
        //            else if (ValueSelected == '131407') {

        //            }
        //            // Transferencia Bancaria
        //            else if (ValueSelected == '131406') {

        //                valoresResult += ValueSelected + "|";
        //                valoresResult += $find("<%=txtMontoTransferencia.ClientID %>").get_value() + "|";
        //                valoresResult += $find("<%=txtFechaTransferencia.ClientID %>").get_selectedDate().format("dd/MM/yyyy") + "|";
        //                valoresResult += $find("<%=txtNroCtaDestinoTransferencia.ClientID %>").get_value() + "|";
        //                valoresResult += $find("<%=txtNroControlTransferencia.ClientID %>").get_value() + "|";
        //                valoresResult += $find("<%=txtReferencia.ClientID %>").get_value();

        //                Descripciones += $find("<%= cboTipoFormadePago.ClientID %>").get_selectedItem().get_text();
        //            }

        //            var result = new Object();
        //            result.Valores = valoresResult;
        //            result.Descripciones = Descripciones;

        //            return result;

        //        }


        function ValidatorCheque(enabled) {
            var valNroCheque = document.getElementById("<%=RequiredFieldValidatorNroCheque.ClientID %>");
            var valFechaEmision = document.getElementById("<%=RequiredFieldValidatortxtFechaEmision.ClientID %>");
            var valMonto = document.getElementById("<%=RequiredFieldValidatortxtMontoCheque.ClientID %>");
            var valAdj = document.getElementById("<%=RequiredFieldValidatorAdjudicatarioChPropio.ClientID %>");


            ValidatorEnable(valNroCheque, enabled);
            ValidatorEnable(valFechaEmision, enabled);
            ValidatorEnable(valMonto, enabled);
            ValidatorEnable(valAdj, enabled);
        }

        function ValidatorChequeTerceros(enabled) {
            var valNroChuequeT = document.getElementById("<%=RequiredFieldValidatorNroChequeT.ClientID %>");
            var valFechaEmisionT = document.getElementById("<%=RequiredFieldValidatorFechaEmisionT.ClientID %>");
            var valMonto = document.getElementById("<%=RequiredFieldValidatortxtMontoChequeT.ClientID %>");
            var valTitular = document.getElementById("<%=RequiredFieldValidatorTitular.ClientID %>");


            ValidatorEnable(valNroChuequeT, enabled);
            ValidatorEnable(valFechaEmisionT, enabled);
            ValidatorEnable(valMonto, enabled);
            ValidatorEnable(valTitular, enabled);

        }


        function ValidatorEfectivo(enabled) {
            var valEfectivo = document.getElementById("<%=RequiredFieldValidatortxtMonto.ClientID %>");
            ValidatorEnable(valEfectivo, enabled);
        }

        function ValidatorDepositoBancario(enabled) {
            var val1 = document.getElementById("<%=RFV_txtMontoDepositoBanco.ClientID %>");
            var val2 = document.getElementById("<%=RFV_txtFechaDeposito.ClientID %>");
            var val3 = document.getElementById("<%=RFV_txtNroCtaDestino.ClientID %>");
            var val4 = document.getElementById("<%=RFV_txtNroTransferencia.ClientID %>");



            ValidatorEnable(val1, enabled);
            ValidatorEnable(val2, enabled);
            ValidatorEnable(val3, enabled);
            ValidatorEnable(val4, enabled);
        }

        function ValidatorTransferencia(enabled) {
            var val1 = document.getElementById("<%=RFV_txtMontoTransferencia.ClientID %>");
            var val2 = document.getElementById("<%=RFV_txtFechaTransferencia.ClientID %>");
            var val3 = document.getElementById("<%=RFV_txtNroCtaDestinoTransferencia.ClientID %>");
            var val4 = document.getElementById("<%=RFV_txtNroControlTransferencia.ClientID %>");


            ValidatorEnable(val1, enabled);
            ValidatorEnable(val2, enabled);
            ValidatorEnable(val3, enabled);
            ValidatorEnable(val4, enabled);
        }


        function ValidatorOtros(enabled) {
            var val1 = document.getElementById("<%=RFV_txtMontoOtros.ClientID %>");
            var val2 = document.getElementById("<%=RFV_txtFechaOtros.ClientID %>");
            var val3 = document.getElementById("<%=RFV_txtNroOperacionOtros.ClientID %>");


            ValidatorEnable(val1, enabled);
            ValidatorEnable(val2, enabled);
            ValidatorEnable(val3, enabled);

        }


        function SetAdjudicacion(NombreAdj) {

            $find("<%=txtAdjudicatario.ClientID %>").set_value(NombreAdj);

        }

        function SetVisualizacion(ValueSelected) {

            // Efectivo Y Ajuste
            if (ValueSelected == '0') {

                $get("<%=divEfectivo.ClientID %>").style.display = 'block';
                $get("<%=divCheque.ClientID %>").style.display = 'none';
                $get("<%=divChequeTercero.ClientID %>").style.display = 'none';
                $get("<%=divDepositoBanco.ClientID %>").style.display = 'none';
                $get("<%=divTransferencia.ClientID %>").style.display = 'none';
                $get("<%=divOtros.ClientID %>").style.display = 'none';

                ValidatorCheque(false);
                ValidatorEfectivo(true);
                ValidatorChequeTerceros(false);
                ValidatorDepositoBancario(false);
                ValidatorTransferencia(false);
                ValidatorOtros(false);

            }
            // Cheque Terceros
            else if (ValueSelected == '3') {
                $get("<%=divChequeTercero.ClientID %>").style.display = 'block';
                $get("<%=divEfectivo.ClientID %>").style.display = 'none';
                $get("<%=divCheque.ClientID %>").style.display = 'none';
                $get("<%=divDepositoBanco.ClientID %>").style.display = 'none';
                $get("<%=divTransferencia.ClientID %>").style.display = 'none';
                $get("<%=divOtros.ClientID %>").style.display = 'none';

                ValidatorCheque(false);
                ValidatorEfectivo(false);
                ValidatorChequeTerceros(true);
                ValidatorDepositoBancario(false);
                ValidatorTransferencia(false);
                ValidatorOtros(false);
            }
            // Deposito Bancario
            else if (ValueSelected == '1') {
                $get("<%=divChequeTercero.ClientID %>").style.display = 'none';
                $get("<%=divEfectivo.ClientID %>").style.display = 'none';
                $get("<%=divCheque.ClientID %>").style.display = 'none';
                $get("<%=divDepositoBanco.ClientID %>").style.display = 'block';
                $get("<%=divTransferencia.ClientID %>").style.display = 'none';
                $get("<%=divOtros.ClientID %>").style.display = 'none';

                ValidatorCheque(false);
                ValidatorEfectivo(false);
                ValidatorChequeTerceros(false);
                ValidatorDepositoBancario(true);
                ValidatorTransferencia(false);
                ValidatorOtros(false);
            }
            // Transferencia Bancaria
            else if (ValueSelected == '2') {
                $get("<%=divChequeTercero.ClientID %>").style.display = 'none';
                $get("<%=divEfectivo.ClientID %>").style.display = 'none';
                $get("<%=divCheque.ClientID %>").style.display = 'none';
                $get("<%=divDepositoBanco.ClientID %>").style.display = 'none';
                $get("<%=divTransferencia.ClientID %>").style.display = 'block';
                $get("<%=divOtros.ClientID %>").style.display = 'none';

                ValidatorCheque(false);
                ValidatorEfectivo(false);
                ValidatorChequeTerceros(false);
                ValidatorDepositoBancario(false);
                ValidatorTransferencia(true);
                ValidatorOtros(false);
            }
            // Otros
            else if (ValueSelected == '4') {
                $get("<%=divChequeTercero.ClientID %>").style.display = 'none';
                $get("<%=divEfectivo.ClientID %>").style.display = 'none';
                $get("<%=divCheque.ClientID %>").style.display = 'none';
                $get("<%=divDepositoBanco.ClientID %>").style.display = 'none';
                $get("<%=divTransferencia.ClientID %>").style.display = 'none';
                $get("<%=divOtros.ClientID %>").style.display = 'block';

                ValidatorCheque(false);
                ValidatorEfectivo(false);
                ValidatorChequeTerceros(false);
                ValidatorDepositoBancario(false);
                ValidatorTransferencia(false);
                ValidatorOtros(true);
            }
            else if (ValueSelected == "") {

                var combo = $find("<%= cboTipoFormadePago.ClientID %>");
                var itm = combo.findItemByValue("-1");
                itm.select();

            }
            else {
                $get("<%=divCheque.ClientID %>").style.display = 'none';
                $get("<%=divEfectivo.ClientID %>").style.display = 'none';
                $get("<%=divChequeTercero.ClientID %>").style.display = 'none';
                $get("<%=divDepositoBanco.ClientID %>").style.display = 'none';
                $get("<%=divTransferencia.ClientID %>").style.display = 'none';
                $get("<%=divOtros.ClientID %>").style.display = 'none';

                ValidatorCheque(false);
                ValidatorEfectivo(false);
                ValidatorChequeTerceros(false);
                ValidatorDepositoBancario(false);
                ValidatorTransferencia(false);
                ValidatorOtros(false);
            }

        }

        function cboTipoFormadePago_OnClientSelectedIndexChanged(sender, eventArgs) {
            var item = eventArgs.get_item();
            SetVisualizacion(item.get_value());

        }

        //        function CargarValorInicial(ValorLimite) {

        //            if ($find("<%=txtMonto.ClientID %>") != null) {
        //                $find("<%=txtMonto.ClientID %>").set_maxValue(parseFloat(ValorLimite));
        //                $find("<%=txtMontoCheque.ClientID %>").set_maxValue(parseFloat(ValorLimite));
        //                $find("<%=txtMontoChequeT.ClientID %>").set_maxValue(parseFloat(ValorLimite));
        //                $find("<%=txtMontoDepositoBanco.ClientID %>").set_maxValue(parseFloat(ValorLimite));
        //                $find("<%=txtMontoTransferencia.ClientID %>").set_maxValue(parseFloat(ValorLimite));



        //                $find("<%=txtMonto.ClientID %>").set_value(parseFloat(ValorLimite));
        //                $find("<%=txtMontoCheque.ClientID %>").set_value(parseFloat(ValorLimite));
        //                $find("<%=txtMontoChequeT.ClientID %>").set_value(parseFloat(ValorLimite));
        //                $find("<%=txtMontoDepositoBanco.ClientID %>").set_value(parseFloat(ValorLimite));
        //                $find("<%=txtMontoTransferencia.ClientID %>").set_value(parseFloat(ValorLimite));

        //            }

        //        }

        function ShowError() {
            alert("El valor ingresado supera el valor máximo: $ " + $get("<%=HideValorLimite.ClientID %>").value);
        }

        function FormaDePagoHabilitarControlesRequerido() {

            var item = $find("<%= cboTipoFormadePago.ClientID %>").get_selectedItem();
            /// Cheques propios
            if (item.get_value() == '10998') {

                ValidatorCheque(true);
                ValidatorEfectivo(false);
                ValidatorChequeTerceros(false);
                ValidatorDepositoBancario(false);
                ValidatorTransferencia(false);
            }
            // Efectivo
            else if (item.get_value() == '10721' || item.get_value() == '132169') {

                ValidatorCheque(false);
                ValidatorEfectivo(true);
                ValidatorChequeTerceros(false);
                ValidatorDepositoBancario(false);
                ValidatorTransferencia(false);

            }
            // Cheque Terceros
            else if (item.get_value() == '10999') {
                ValidatorCheque(false);
                ValidatorEfectivo(false);
                ValidatorChequeTerceros(true);
                ValidatorDepositoBancario(false);
                ValidatorTransferencia(false);
            }
            // Deposito Banco
            else if (item.get_value() == '131407') {
                ValidatorCheque(false);
                ValidatorEfectivo(false);
                ValidatorChequeTerceros(false);
                ValidatorDepositoBancario(true);
                ValidatorTransferencia(false);
            }
            // Transferencia Bancaria
            else if (item.get_value() == '131406') {
                ValidatorCheque(false);
                ValidatorEfectivo(false);
                ValidatorChequeTerceros(false);
                ValidatorDepositoBancario(false);
                ValidatorTransferencia(true);
            }
            else {
                ValidatorCheque(false);
                ValidatorEfectivo(false);
                ValidatorChequeTerceros(false);
                ValidatorDepositoBancario(false);
                ValidatorTransferencia(false);
            }


            SetVisualizacion(item.get_value());
        }

        function FormaDePagoDesHabilitarControlesRequeridos() {

            ValidatorCheque(false);
            ValidatorEfectivo(false);
            ValidatorChequeTerceros(false);
            ValidatorDepositoBancario(false);
            ValidatorTransferencia(false);

        }
    
    </script>
</telerik:RadCodeBlock>
<div runat="server" id="Handle" style="display: none;">
</div>
<asp:HiddenField ID="HideValorTotal" runat="server" Value="0" />
<table cellpadding="0" cellspacing="0" border="0" width="90%">
    <tr>
        <td colspan="2" align="center">
            <asp:UpdatePanel ID="UpdPnlGrilla" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color: White;">
                        <tr style="padding-top: 5px; font-weight: bold">
                            <td class="HeaderVista_Marzzan">
                                SALDO
                            </td>
                            <td class="HeaderVista_Marzzan">
                                VALOR GUIAS
                            </td>
                            <td class="HeaderVista_Marzzan">
                                TOTAL A PAGAR
                            </td>
                            <td class="HeaderVista_Marzzan">
                                NUEVO SALDO
                            </td>
                        </tr>
                        <tr style="padding-top: 5px; background-color: #F5F5F5; border: solid 1px black">
                            <td class="TdVista_Marzzan">
                                <asp:Label ID="lblSaldo" ForeColor="Black" runat="server" Text="0"></asp:Label>
                            </td>
                            <td class="TdVista_Marzzan">
                                <asp:Label ID="lblTotal" ForeColor="Black" runat="server" Text="0"></asp:Label>
                            </td>
                            <td class="TdVista_Marzzan">
                                <asp:Label ID="lblTotalGeneral" ForeColor="Black" runat="server" Text="0"></asp:Label>
                            </td>
                            <td class="TdVista_Marzzan">
                                <asp:Label ID="lblNuevoSaldo" ForeColor="Black" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trAgregarPago">
                            <td align="left" valign="middle" colspan="4" style="padding-top: 15px;">
                                <button class="btnBasic_Marzzan" onclick="return ucFormaPagoMultiple_ShowCarga();">
                                    <asp:LinkButton ID="btnInsert" runat="server">
                                     <img style="border:0px;vertical-align:middle;" width="18" height="18" alt="" src="Imagenes/AddFormula.png" /><span style="padding:5px">Nuevo Pago</span>
                                    </asp:LinkButton>
                                </button>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center" style="padding-top: 1px; background-color: White">
                                <div id="divtblPagos">
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
<div id="DivCarga" style="display: none;">
</div>
<div class="modal" id="divPrincipalFormaPago" style="height: 115px; width: 980px;">
    <table width="95%" border="0" cellspacing="2" cellpadding="0">
        <tr>
            <td align="left" style="height: 90px" valign="top">
                <asp:UpdatePanel ID="upDetalle" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="HideValorLimite" runat="server" Value="0" />
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <telerik:RadComboBox CausesValidation="false" ID="cboTipoFormadePago" runat="server"
                                        Skin="WebBlue" Width="219px" MarkFirstMatch="True" OnClientSelectedIndexChanged="cboTipoFormadePago_OnClientSelectedIndexChanged"
                                        ZIndex="900199999">
                                        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divCheque" runat="server" style="display: none">
                                        <table style="width: 900px;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label211" SkinID="lblTecnoBasic" runat="server" Text="Monto:"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtMontoCheque" runat="server" Skin="WebBlue" NumberFormat-DecimalDigits="2"
                                                        Width="100px" ClientEvents-OnError="ShowError" MinValue="0" Value="0" MaxValue="0">
                                                        <NumberFormat DecimalDigits="2" DecimalSeparator="." GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtMontoCheque" runat="server"
                                                        ControlToValidate="txtMontoCheque" ErrorMessage="Debe ingresar la cantidad de pesos"
                                                        ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblNroCheque" SkinID="lblTecnoBasic" runat="server" Text="Nro.:"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtNroCheque" runat="server" Skin="WebBlue">
                                                        <NumberFormat DecimalDigits="0" />
                                                    </telerik:RadNumericTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorNroCheque" runat="server" ControlToValidate="txtNroCheque"
                                                        ErrorMessage="Debe ingresar el Nro del Cheque" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblcboBanco" SkinID="lblTecnoBasic" runat="server" Text="Banco:"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox CausesValidation="false" ID="cboBanco" runat="server" Skin="WebBlue"
                                                        Width="219px" MarkFirstMatch="True" ZIndex="900199999">
                                                        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFechaEmision" SkinID="lblTecnoBasic" runat="server" Text="Fecha Emision:"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtFechaEmision" runat="server" ZIndex="900199999">
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtFechaEmision" runat="server"
                                                        ControlToValidate="txtFechaEmision" Enabled="false" ErrorMessage="Debe ingresar la fecha de emisión"
                                                        ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td style="display: none">
                                                    <asp:Label ID="lblNroRecibo" SkinID="lblTecnoBasic" runat="server" Text="Nro. Recibo:"></asp:Label>
                                                </td>
                                                <td style="display: none">
                                                    <telerik:RadNumericTextBox ID="txtNroRecibo" runat="server" Skin="WebBlue">
                                                        <NumberFormat DecimalDigits="0" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label18" SkinID="lblTecnoBasic" runat="server" Text="Adjudicatario:"></asp:Label>
                                                </td>
                                                <td colspan="9">
                                                    <telerik:RadTextBox ID="txtAdjudicatario" runat="server" Width="250px">
                                                    </telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorAdjudicatarioChPropio" runat="server"
                                                        Enabled="false" ControlToValidate="txtAdjudicatario" ErrorMessage="Debe ingresar el Adjudicatario del cheque"
                                                        ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divEfectivo" runat="server" style="display: none">
                                        <table style="width: 250px;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label2" SkinID="lblTecnoBasic" runat="server" Text="Monto:"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtMonto" runat="server" Skin="WebBlue" NumberFormat-DecimalDigits="2"
                                                        Width="100px" ClientEvents-OnError="ShowError" MinValue="0" Value="0.0">
                                                        <NumberFormat DecimalDigits="2" DecimalSeparator="." GroupSeparator=","></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtMonto" runat="server" ControlToValidate="txtMonto"
                                                        ErrorMessage="Debe ingresar la cantidad de pesos" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divChequeTercero" runat="server" style="display: none">
                                        <table style="width: 900px;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label3ss" SkinID="lblTecnoBasic" runat="server" Text="Monto:"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtMontoChequeT" runat="server" Skin="WebBlue" NumberFormat-DecimalDigits="2"
                                                        Width="100px" ClientEvents-OnError="ShowError" MinValue="0" Value="0">
                                                        <NumberFormat DecimalDigits="2" DecimalSeparator="." GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtMontoChequeT" runat="server"
                                                        ControlToValidate="txtMontoChequeT" ErrorMessage="Debe ingresar la cantidad de pesos"
                                                        ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label4d" SkinID="lblTecnoBasic" runat="server" Text="Nro.:"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtNroChequeT" runat="server" Skin="WebBlue">
                                                        <NumberFormat DecimalDigits="0" />
                                                    </telerik:RadNumericTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorNroChequeT" runat="server"
                                                        ControlToValidate="txtNroChequeT" Enabled="false" ErrorMessage="Debe ingresar el Nro del Cheque (T)"
                                                        ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label5dw" SkinID="lblTecnoBasic" runat="server" Text="Banco:"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox CausesValidation="false" ID="cboBancoT" runat="server" Skin="WebBlue"
                                                        Width="219px" MarkFirstMatch="True" ZIndex="900199999">
                                                        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label622" SkinID="lblTecnoBasic" runat="server" Text="Fecha Cobro:"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtFechaEmisionT" runat="server" ZIndex="900199999">
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorFechaEmisionT" runat="server"
                                                        ControlToValidate="txtFechaEmisionT" Enabled="false" ErrorMessage="Debe ingresar la fecha de emisión"
                                                        ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label17" SkinID="lblTecnoBasic" runat="server" Text="Titular:"></asp:Label>
                                                </td>
                                                <td colspan="7">
                                                    <telerik:RadTextBox ID="txtTitular" runat="server" Width="250px">
                                                    </telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTitular" Enabled="false" runat="server"
                                                        ControlToValidate="txtTitular" ErrorMessage="Debe ingresar el titular del cheque"
                                                        ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divDepositoBanco" runat="server" style="display: none">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label7" SkinID="lblTecnoBasic" runat="server" Text="Monto:"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtMontoDepositoBanco" runat="server" Skin="WebBlue"
                                                        NumberFormat-DecimalDigits="2" Width="70px" ClientEvents-OnError="ShowError"
                                                        MinValue="0" Value="0">
                                                        <NumberFormat DecimalDigits="2" DecimalSeparator="." GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                    <asp:RequiredFieldValidator ID="RFV_txtMontoDepositoBanco" runat="server" ControlToValidate="txtMontoDepositoBanco"
                                                        Enabled="false" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label10" SkinID="lblTecnoBasic" runat="server" Text="Fecha Depósito:"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtFechaDeposito" Width="100px" runat="server" ZIndex="900199999">
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RFV_txtFechaDeposito" runat="server" ControlToValidate="txtFechaDeposito"
                                                        Enabled="false" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label11" SkinID="lblTecnoBasic" runat="server" Text="Nro Cta Destino:"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtNroCtaDestino" runat="server" Skin="WebBlue" Width="150px">
                                                    </telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RFV_txtNroCtaDestino" runat="server" ControlToValidate="txtNroCtaDestino"
                                                        Enabled="false" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" SkinID="lblTecnoBasic" runat="server" Text="Nro Transferencia:"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtNroTransferencia" runat="server" Skin="WebBlue" Width="150px">
                                                    </telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RFV_txtNroTransferencia" runat="server" ControlToValidate="txtNroTransferencia"
                                                        Enabled="false" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divTransferencia" runat="server" style="display: none">
                                        <table style="width: 100%;" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label9" SkinID="lblTecnoBasic" runat="server" Text="Monto:"></asp:Label>
                                                </td>
                                                <td style="width: 120px">
                                                    <telerik:RadNumericTextBox ID="txtMontoTransferencia" runat="server" Skin="WebBlue"
                                                        NumberFormat-DecimalDigits="2" Width="100px" ClientEvents-OnError="ShowError"
                                                        MinValue="0" Value="0">
                                                        <NumberFormat DecimalDigits="2" DecimalSeparator="." GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                    <asp:RequiredFieldValidator ID="RFV_txtMontoTransferencia" runat="server" ControlToValidate="txtMontoTransferencia"
                                                        Enabled="false" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label12" SkinID="lblTecnoBasic" runat="server" Text="Fecha:"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtFechaTransferencia" runat="server" Width="110px" ZIndex="900199999">
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RFV_txtFechaTransferencia" runat="server" ControlToValidate="txtFechaTransferencia"
                                                        Enabled="false" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width: 110px">
                                                    <asp:Label ID="Label13" SkinID="lblTecnoBasic" runat="server" Text="Nro Cta Destino:"></asp:Label>
                                                </td>
                                                <td style="width: 140px">
                                                    <telerik:RadTextBox ID="txtNroCtaDestinoTransferencia" runat="server" Skin="WebBlue"
                                                        Width="120px">
                                                    </telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RFV_txtNroCtaDestinoTransferencia" runat="server"
                                                        ControlToValidate="txtNroCtaDestinoTransferencia" Enabled="false" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label15" SkinID="lblTecnoBasic" runat="server" Text="Nro Control:"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtNroControlTransferencia" runat="server" Skin="WebBlue"
                                                        Width="130px">
                                                    </telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RFV_txtNroControlTransferencia" runat="server" ControlToValidate="txtNroControlTransferencia"
                                                        Enabled="false" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="1" align="left">
                                                    <asp:Label ID="Label16" SkinID="lblTecnoBasic" runat="server" Text="Referencia:"></asp:Label>
                                                </td>
                                                <td colspan="7">
                                                    <telerik:RadTextBox ID="txtReferencia" runat="server" Skin="WebBlue" Width="90%">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divOtros" runat="server" style="display: none">
                                        <table style="width: 100%;" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label4" SkinID="lblTecnoBasic" runat="server" Text="Monto:"></asp:Label>
                                                </td>
                                                <td style="width: 120px">
                                                    <telerik:RadNumericTextBox ID="txtMontoOtros" runat="server" Skin="WebBlue" NumberFormat-DecimalDigits="2"
                                                        Width="100px" ClientEvents-OnError="ShowError" MinValue="0" Value="0">
                                                        <NumberFormat DecimalDigits="2" DecimalSeparator="." GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                    <asp:RequiredFieldValidator ID="RFV_txtMontoOtros" runat="server" ControlToValidate="txtMontoOtros"
                                                        Enabled="false" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label5" SkinID="lblTecnoBasic" runat="server" Text="Fecha:"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtFechaOtros" runat="server" Width="110px" ZIndex="900199999">
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RFV_txtFechaOtros" runat="server" ControlToValidate="txtFechaOtros"
                                                        Enabled="false" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width: 50px">
                                                    <asp:Label ID="Label6" SkinID="lblTecnoBasic" runat="server" Text="Nro:"></asp:Label>
                                                </td>
                                                <td style="width: 140px">
                                                    <telerik:RadTextBox ID="txtNroOperacionOtros" runat="server" Skin="WebBlue" Width="120px">
                                                    </telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RFV_txtNroOperacionOtros" runat="server" ControlToValidate="txtNroOperacionOtros"
                                                        Enabled="false" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label14" SkinID="lblTecnoBasic" runat="server" Text="Operador:"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox CausesValidation="false" ID="cboOperadores" runat="server" Skin="WebBlue"
                                                        Width="180px" AllowCustomText="false" ZIndex="900199999">
                                                        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="center">

            <button class="btnBasic_Marzzan" onclick="ucFormaPagoMultiple_ControlarFinalizacion();return false;">
                                    <asp:LinkButton ID="LinkButton1" runat="server">
                                     <img style="border:0px;vertical-align:middle;" width="18" height="18" alt="" src="Imagenes/Ok.gif" /><span style="padding:5px">Agregar</span>
                                    </asp:LinkButton>
                                </button>


            </td>
        </tr>
    </table>
</div>
