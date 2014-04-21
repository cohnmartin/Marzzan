<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucFormaPago.ascx.cs" Inherits="Registraciones_Controles_ucFormaPago" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
                                    <asp:Label ID="Label2" SkinID="lblTecnoBasic" runat="server" Text="Monto:"></asp:Label>
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
                                    <asp:Label ID="Label1" SkinID="lblTecnoBasic" runat="server" Text="Monto:"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtMonto" runat="server" Skin="WebBlue" NumberFormat-DecimalDigits="2"
                                        Width="100px" ClientEvents-OnError="ShowError" MinValue="0" Value="0.0">
                                        <NumberFormat DecimalDigits="2" DecimalSeparator="." GroupSeparator=""></NumberFormat>
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
                                    <asp:Label ID="Label3" SkinID="lblTecnoBasic" runat="server" Text="Monto:"></asp:Label>
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
                                    <asp:Label ID="Label4" SkinID="lblTecnoBasic" runat="server" Text="Nro.:"></asp:Label>
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
                                    <asp:Label ID="Label5" SkinID="lblTecnoBasic" runat="server" Text="Banco:"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox CausesValidation="false" ID="cboBancoT" runat="server" Skin="WebBlue"
                                        Width="219px" MarkFirstMatch="True" ZIndex="900199999">
                                        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label6" SkinID="lblTecnoBasic" runat="server" Text="Fecha Emision:"></asp:Label>
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
                                        NumberFormat-DecimalDigits="2" Width="100px" ClientEvents-OnError="ShowError"
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
                                    <telerik:RadDatePicker ID="txtFechaDeposito" runat="server" ZIndex="900199999">
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
                                    <asp:Label ID="Label8" SkinID="lblTecnoBasic" runat="server" Text="Nro Sobre:"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtNroSobre" runat="server" Skin="WebBlue" Width="150px">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RFV_txtNroSobre" runat="server" ControlToValidate="txtNroSobre"
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
                                        MinValue="0">
                                        <NumberFormat DecimalDigits="2" DecimalSeparator="." GroupSeparator=""></NumberFormat>
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RFV_txtMontoTransferencia" runat="server" ControlToValidate="txtMontoTransferencia"
                                        Enabled="false" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="Label12" SkinID="lblTecnoBasic" runat="server" Text="Fecha:"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFechaTransferencia" runat="server" Width="110px" ZIndex="900199999"
                                        ClientEvents-OnDateSelected="BuscarTransferenciaExistente">
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="RFV_txtFechaTransferencia" runat="server" ControlToValidate="txtFechaTransferencia"
                                        Enabled="false" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 110px">
                                    <asp:Label ID="Label13" SkinID="lblTecnoBasic" runat="server" Text="Nro Cta Destino:"></asp:Label>
                                </td>
                                <td style="width: 140px">
                                    <telerik:RadTextBox ID="txtNroCtaDestinoTransferencia" runat="server" Skin="WebBlue"
                                        Width="120px" ClientEvents-OnValueChanged="BuscarTransferenciaExistente">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RFV_txtNroCtaDestinoTransferencia" runat="server"
                                        ControlToValidate="txtNroCtaDestinoTransferencia" Enabled="false" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="Label15" SkinID="lblTecnoBasic" runat="server" Text="Nro Control:"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtNroControlTransferencia" runat="server" Skin="WebBlue"
                                        Width="130px" ClientEvents-OnValueChanged="BuscarTransferenciaExistente">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RFV_txtNroControlTransferencia" runat="server" ControlToValidate="txtNroControlTransferencia"
                                        Enabled="false" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <asp:Label ID="Label14" SkinID="lblTecnoBasic" runat="server" Text="Concepto:"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="txtConcepto" runat="server" Skin="WebBlue" Width="250px">
                                    </telerik:RadTextBox>
                                </td>
                                <td colspan="2" align="right">
                                    <asp:Label ID="Label16" SkinID="lblTecnoBasic" runat="server" Text="Referencia:"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="txtReferencia" runat="server" Skin="WebBlue" Width="250px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

<script language="javascript" type="text/javascript">


    function BuscarTransferenciaExistente(sender, args) {
        var fecha = ""
        if ($find("<%=txtFechaTransferencia.ClientID %>").get_selectedDate() != "")
            var fecha = $find("<%=txtFechaTransferencia.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

        var nroCta = $find("<%=txtNroCtaDestinoTransferencia.ClientID %>").get_value();
        var nroControl = $find("<%=txtNroControlTransferencia.ClientID %>").get_value();
        var idMaestro = $find("<%= cboTipoFormadePago.ClientID %>").get_selectedItem().get_value();

        if (fecha != "" && nroCta != "" && nroControl != "")
            BusquedaTransferencia(FinBusqueda, falloBusqueda, fecha, nroCta, nroControl, idMaestro);
    }

    function FinBusqueda(result) {
        var obj = eval('(' + result + ')');

        if (obj["ItemExistente"] != null) {
            $find("<%=txtConcepto.ClientID %>").set_value(obj["Concepto"]);
            $find("<%=txtReferencia.ClientID %>").set_value(obj["Referencia"]);
        }
    }

    function falloBusqueda() {

    }

    function GetValorFormaPago() {
        var ValueSelected = $find("<%= cboTipoFormadePago.ClientID %>").get_selectedItem().get_value();
        var valoresResult = "";
        var Descripciones = "";
        debugger;
        /// Cheques propios
        if (ValueSelected == '10998') {
            valoresResult += ValueSelected + "|";
            valoresResult += $find("<%=txtMontoCheque.ClientID %>").get_value() + "|";
            valoresResult += $find("<%=txtNroCheque.ClientID %>").get_value() + "|";
            valoresResult += $find("<%=cboBanco.ClientID %>").get_selectedItem().get_value() + "|";
            valoresResult += $find("<%=txtFechaEmision.ClientID %>").get_selectedDate().format("dd/MM/yyyy") + "|";
            valoresResult += $find("<%=txtAdjudicatario.ClientID %>").get_value();

            Descripciones += $find("<%= cboTipoFormadePago.ClientID %>").get_selectedItem().get_text() + "|";
            Descripciones += $find("<%=cboBanco.ClientID %>").get_selectedItem().get_text();

        }
        // Efectivo
        else if (ValueSelected == '10721' || ValueSelected == '132169') {
            valoresResult += ValueSelected + "|";
            valoresResult += $find("<%=txtMonto.ClientID %>").get_value();

            Descripciones += $find("<%= cboTipoFormadePago.ClientID %>").get_selectedItem().get_text() + "|";
        }
        // Cheque Terceros
        else if (ValueSelected == '10999') {
            valoresResult += ValueSelected + "|";
            valoresResult += $find("<%=txtMontoChequeT.ClientID %>").get_value() + "|";
            valoresResult += $find("<%=txtNroChequeT.ClientID %>").get_value() + "|";
            valoresResult += $find("<%=cboBancoT.ClientID %>").get_selectedItem().get_value() + "|";
            valoresResult += $find("<%=txtFechaEmisionT.ClientID %>").get_selectedDate().format("dd/MM/yyyy") + "|";
            valoresResult += $find("<%=txtTitular.ClientID %>").get_value();



            Descripciones += $find("<%= cboTipoFormadePago.ClientID %>").get_selectedItem().get_text() + "|";
            Descripciones += $find("<%=cboBancoT.ClientID %>").get_selectedItem().get_text();

        }
        // Deposito Bancario
        else if (ValueSelected == '131407') {

        }
        // Transferencia Bancaria
        else if (ValueSelected == '131406') {

            valoresResult += ValueSelected + "|";
            valoresResult += $find("<%=txtMontoTransferencia.ClientID %>").get_value() + "|";
            valoresResult += $find("<%=txtFechaTransferencia.ClientID %>").get_selectedDate().format("dd/MM/yyyy") + "|";
            valoresResult += $find("<%=txtNroCtaDestinoTransferencia.ClientID %>").get_value() + "|";
            valoresResult += $find("<%=txtNroControlTransferencia.ClientID %>").get_value() + "|";
            valoresResult += $find("<%=txtConcepto.ClientID %>").get_value() + "|";
            valoresResult += $find("<%=txtReferencia.ClientID %>").get_value();

            Descripciones += $find("<%= cboTipoFormadePago.ClientID %>").get_selectedItem().get_text();
        }

        var result = new Object();
        result.Valores = valoresResult;
        result.Descripciones = Descripciones;

        return result;

    }


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
        var val4 = document.getElementById("<%=RFV_txtNroSobre.ClientID %>");



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

    function SetAdjudicacion(NombreAdj) {

        $find("<%=txtAdjudicatario.ClientID %>").set_value(NombreAdj);

    }

    function SetVisualizacion(ValueSelected) {

        /// Cheques propios
        if (ValueSelected == '10998') {

            $get("<%=divCheque.ClientID %>").style.display = 'block';
            $get("<%=divEfectivo.ClientID %>").style.display = 'none';
            $get("<%=divChequeTercero.ClientID %>").style.display = 'none';
            $get("<%=divDepositoBanco.ClientID %>").style.display = 'none';
            $get("<%=divTransferencia.ClientID %>").style.display = 'none';


            ValidatorCheque(true);
            ValidatorEfectivo(false);
            ValidatorChequeTerceros(false);
            ValidatorDepositoBancario(false);
            ValidatorTransferencia(false);
        }
        // Efectivo Y Ajuste
        else if (ValueSelected == '0') {

            $get("<%=divEfectivo.ClientID %>").style.display = 'block';
            $get("<%=divCheque.ClientID %>").style.display = 'none';
            $get("<%=divChequeTercero.ClientID %>").style.display = 'none';
            $get("<%=divDepositoBanco.ClientID %>").style.display = 'none';
            $get("<%=divTransferencia.ClientID %>").style.display = 'none';
            ValidatorCheque(false);
            ValidatorEfectivo(true);
            ValidatorChequeTerceros(false);
            ValidatorDepositoBancario(false);
            ValidatorTransferencia(false);

        }
        // Cheque Terceros
        else if (ValueSelected == '3') {
            $get("<%=divChequeTercero.ClientID %>").style.display = 'block';
            $get("<%=divEfectivo.ClientID %>").style.display = 'none';
            $get("<%=divCheque.ClientID %>").style.display = 'none';
            $get("<%=divDepositoBanco.ClientID %>").style.display = 'none';
            $get("<%=divTransferencia.ClientID %>").style.display = 'none';
            ValidatorCheque(false);
            ValidatorEfectivo(false);
            ValidatorChequeTerceros(true);
            ValidatorDepositoBancario(false);
            ValidatorTransferencia(false);
        }
        // Deposito Bancario
        else if (ValueSelected == '1') {
            $get("<%=divChequeTercero.ClientID %>").style.display = 'none';
            $get("<%=divEfectivo.ClientID %>").style.display = 'none';
            $get("<%=divCheque.ClientID %>").style.display = 'none';
            $get("<%=divDepositoBanco.ClientID %>").style.display = 'block';
            $get("<%=divTransferencia.ClientID %>").style.display = 'none';
            ValidatorCheque(false);
            ValidatorEfectivo(false);
            ValidatorChequeTerceros(false);
            ValidatorDepositoBancario(true);
            ValidatorTransferencia(false);
        }
        // Transferencia Bancaria
        else if (ValueSelected == '2') {
            $get("<%=divChequeTercero.ClientID %>").style.display = 'none';
            $get("<%=divEfectivo.ClientID %>").style.display = 'none';
            $get("<%=divCheque.ClientID %>").style.display = 'none';
            $get("<%=divDepositoBanco.ClientID %>").style.display = 'none';
            $get("<%=divTransferencia.ClientID %>").style.display = 'block';
            ValidatorCheque(false);
            ValidatorEfectivo(false);
            ValidatorChequeTerceros(false);
            ValidatorDepositoBancario(false);
            ValidatorTransferencia(true);
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
            ValidatorCheque(false);
            ValidatorEfectivo(false);
            ValidatorChequeTerceros(false);
            ValidatorDepositoBancario(false);
            ValidatorTransferencia(false);
        }

    }

    function cboTipoFormadePago_OnClientSelectedIndexChanged(sender, eventArgs) {
        var item = eventArgs.get_item();
        SetVisualizacion(item.get_value());

    }
    function CargarValorInicial(ValorLimite) {

        if ($find("<%=txtMonto.ClientID %>") != null) {
            $find("<%=txtMonto.ClientID %>").set_maxValue(parseFloat(ValorLimite));
            $find("<%=txtMontoCheque.ClientID %>").set_maxValue(parseFloat(ValorLimite));
            $find("<%=txtMontoChequeT.ClientID %>").set_maxValue(parseFloat(ValorLimite));
            $find("<%=txtMontoDepositoBanco.ClientID %>").set_maxValue(parseFloat(ValorLimite));
            $find("<%=txtMontoTransferencia.ClientID %>").set_maxValue(parseFloat(ValorLimite));



            $find("<%=txtMonto.ClientID %>").set_value(parseFloat(ValorLimite));
            $find("<%=txtMontoCheque.ClientID %>").set_value(parseFloat(ValorLimite));
            $find("<%=txtMontoChequeT.ClientID %>").set_value(parseFloat(ValorLimite));
            $find("<%=txtMontoDepositoBanco.ClientID %>").set_value(parseFloat(ValorLimite));
            $find("<%=txtMontoTransferencia.ClientID %>").set_value(parseFloat(ValorLimite));

        }

    }

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

