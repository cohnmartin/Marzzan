<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="GestionRendicionTransportistas.aspx.cs"
    Inherits="GestionRendicionTransportistas" %>

<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="AjaxInfo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="ControlesComunes/ucFormaPagoMultiple.ascx" TagName="ucFormaPagoMultiple"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestion de Rendición</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <link href="UploadFiles/CSS/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="Scripts/jquery.tmpl.1.1.1.js" type="text/javascript"></script>
    <link href="Scripts/Modal-master/jquery.modal.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Modal-master/jquery.modal.js" type="text/javascript"></script>
    <style type="text/css">
        .footer
        {
            background-color: white;
            color: Black;
            text-align: right;
            padding-right: 10px;
            font-weight: bold;
        }
        .rowAlt
        {
            background-color: #CCF2FF;
            color: Black;
        }
        .rowAlt2
        {
            background-color: #FAEBB8;
            color: Black;
        }
        .rowSpl
        {
            background-color: White;
            color: Black;
        }
        .ContentFiltro
        {
            font: 13px Verdana, Geneva, sans-serif;
            background-color: #F5F5F5;
            border: 3px solid #DBDBDB;
            margin-top: 0px;
            margin-right: 0px;
            padding: 0px;
            width: 70%;
            cursor: pointer;
            font-weight: bold;
            padding: 3px;
        }
        .DivGeneral
        {
            font: 13px Verdana, Geneva, sans-serif;
            background-color: #E0E0E0;
            border: 3px solid #C0C0C0;
            margin-top: 0px;
            margin-right: 0px;
            padding: 0px;
            float: none;
            width: 156px;
            text-align: center;
            cursor: pointer;
            font-weight: bold;
        }
    </style>
</head>
<script type="text/javascript">

    function AprobarRendicion() {


        blockConfirmCallBackFn('Esta seguro de aprobar la rendición por un total de: ' + ucFormaPagoMultiple_GetTotalIngresado(), event, 330, 100, null, 'Aprobación', ConfirAprobacion);

        function ConfirAprobacion(result) {
            if (result) {
                PageMethods.AprobarRendicion(function () { window.open("ConsultaRendiciones.aspx", "_self"); }, function () { });
            }
        }


    }

    function ObservarRendicion() {
        var obsLog = "";
        var obsCob = "";
        var monto = "";

        if ("<%= TieneRolLogistica%>" == "True") {
            obsLog = $("#<%= txtObsLogistica.ClientID %>").val();
        }
        else {
            obsCob = $("#<%= txtObsCobranza.ClientID %>").val();
            monto = $("#<%= txtMonto.ClientID %>").val();
        }


        if ("<%= TieneRolLogistica%>" == "True" && obsLog == "") {
            alert("Debe ingresar una observación para poder dejar la rendición en estado OBSERVADA L");
        }
        else if ("<%= TieneRolLogistica%>" == "False" && (obsCob == "" || monto == "")) {
            alert("Debe ingresar la información de observación de cobranza para poder dejar la rendición en estado OBSERVADA C");
        }
        else {


            PageMethods.ObservarRendicion(obsCob, monto, obsLog, function () { window.open("ConsultaRendiciones.aspx", "_self"); }, function () { });
        }






    }



    function GrabarRendicion() {

        var diferencia = ucFormaPagoMultiple_GetTotalIngresado() - ucFormaPagoMultiple_GetTotal();

        /// por el momento este control queda des habilitado hasta despues de la vacaciones.
        //if ("<%= TipoRendicion %>" != "V"){}


        diferencia = 0;
        if (diferencia >= 0 || diferencia >= -50) {
            var ids = new Array();
            var chks = $("#tbl").find("input[type='checkbox']:checked");
            chks.each(function () {

                var id = $(this.parentElement.parentElement).find("td[id=IdDetalleGuia]");
                if (id.length > 0)
                    ids.push(id[0].innerText);

            });

            var fechaEnvioDev = "";
            if ("<%= TipoRendicion %>" == "D")
                fechaEnvioDev = $find("<%= txtFechaEnvioDevolucion.ClientID %>").get_selectedDate().format("dd/MM/yyyy");


            var nroGuiaDev = $find("<%= txtNroGuiaDevolucion.ClientID %>").get_value();
            var tranportistaDev = $find("<%= cboTransporteDevolucion.ClientID %>").get_value();

            PageMethods.GrabarRendicion(ids, ucFormaPagoMultiple_GetTotalGuias(), ucFormaPagoMultiple_GetTotalIngresado(), fechaEnvioDev, nroGuiaDev, tranportistaDev, function () { window.open("ConsultaRendiciones.aspx", "_self"); }, function () { });
        }
        else {

            alert("El total de los pagos no es correcto, solo puede dejar pendiente un saldo de $50 o hacer pagos mayores al total, por favor verifique los datos ingresados.");

        }
    }

    function ActualizarMonto(chk) {
        var resultado = 0;
        var valorLinea = parseFloat($(chk.parentElement.parentElement).find("td[id=Monto]")[0].innerText.replace(",", "."));
        //var fechaDevolucion = $(chk.parentElement.parentElement).find("td[id=FechaDevolucion]")[0].innerText;
        var valorTotal = ucFormaPagoMultiple_GetTotal();

        //if (fechaDevolucion.trim() == "") {
        if (chk.checked)
            resultado = valorTotal + valorLinea;
        else
            resultado = valorTotal - valorLinea;

        ucFormaPagoMultiple_SetTotales(parseFloat("<%= SaldoTransportista %>").toFixed(2), resultado.toFixed(2));
        // }

    }

    function CheckAll(chk) {

        var chks = $("#tbl").find("input[type='checkbox']");


        if (chk.checked) {
            chks.each(function () {
                if ($(this)[0].id != "chkAll" && $(this)[0].checked == false) {
                    $(this).attr("checked", "true");
                    ActualizarMonto(this);
                }
            });
        }
        else {
            chks.each(function () {
                if ($(this)[0].id != "chkAll" && $(this)[0].checked == true) {
                    $(this).removeAttr("checked");
                    ActualizarMonto(this);
                }
            });
        }

    }

    function CerrarAbrirDetalleGuias(objImg) {


        $("#tbl").find(".BodyDetalle").fadeToggle(function () {

            if ($(objImg).attr("src").indexOf("SingleMinus") > 0)
                $(objImg).attr("src", "Imagenes/SinglePlus.gif");
            else
                $(objImg).attr("src", "Imagenes/SingleMinus.gif");

        });
    }

    function callBackFunction(datos) {
        if (datos.length > 0) {

            $("#tdPagos").show();

            if ("<%= EsTransportista %>" == "False") {

                $(".ContentFiltro").hide();

            }
            else {
                if ("<%=EstadoRendicion %>" == "PRESENTADA") {
                    $("#tdGrabar").show();
                }
                else
                    $("#tdGrabar").hide();
            }

            /// Segun el Tipo de Rendicion son los controles que se muestrasn.
            switch ("<%= TipoRendicion%>") {
                case "V":
                    $("#tdPagos").show();
                    break;
                case "D":
                    $("#tdPagos").hide();
                    $("#tdDevolucion").show();
                    break;
                case "L":
                case "E":
                case "S":
                    $("#tdPagos").hide();
                    break;
            }


            /// Segun si es tranpostista o no dejo que se modifique la rendicion
            /// como asi tb según el estado de la rendiión.
            var ctrlCheck = '<input onclick="ActualizarMonto(this)" type="checkbox" id="chkSeleccion" checked />';
            if ("<%= EsTransportista %>" == "False" || "<%=EstadoRendicion %>" != "PRESENTADA") { ctrlCheck = "&nbsp;" }

            var ctrlCheckHeader = '<input onclick="CheckAll(this)" type="checkbox" checked id="chkAll" />';
            if ("<%= EsTransportista %>" == "False" || "<%=EstadoRendicion %>" != "PRESENTADA") { ctrlCheckHeader = "&nbsp;" }

            if ("<%= EsTransportista %>" == "False" || "<%=EstadoRendicion %>" != "PRESENTADA") { ucFormaPagoMultiple_SetReadOnly(); }

            "<%= TipoRendicion%>"
            var t = [
            { "header": ctrlCheckHeader },
            { "header": "Comprobante" },
            { "header": "Tipo" },
            { "header": "Nombre" },
            { "header": "Monto" },
            { "header": "Lider" },
            { "header": "Fecha" },
            { "header": "Estado"}];

            var showLiderResp = "display:none";
            var showMotivo = "display:none";
            if ("<%= TipoRendicion%>" == "L") {
                t.push({ "header": "Lider Resp." });
                showLiderResp = "display:inline";
            }

            if ("<%= TipoRendicion%>" == "S") {
                t.push({ "header": "Motivo" });
                showMotivo = "display:inline";
            }

            t.push({ "header": '<img alt="Cerrar/Abrir Detalle" src="Imagenes/SingleMinus.gif" style="cursor:pointer" onclick="CerrarAbrirDetalleGuias(this);return false;" />' });

            var templates = {
                th: '<th class="HeaderVista_Marzzan">#{header}</th>',
                td: '<tr style="cursor:pointer"  >' +
                '<td align="center" style="width:25px" >' + ctrlCheck + '</td>' +
                '<td align="left" style="width:110px" filtro="Comprobante">#{Comprobante}</td>' +
                '<td  align="center" style="width:40px">#{TipoComprobante}</td>' +
                '<td align="left">#{Nombre}</td>' +
                '<td id="Monto" align="center" style="width:60px">#{Monto}</td>' +
                '<td style="width:170px">#{Lider}</td>' +
                '<td id="IdDetalleGuia" style="display:none" >#{IdDetalleGuia}</td>' +
                '<td id="Fecha" >#{Fecha}</td>' +
                '<td id="Estado" style="padding-left:5px" align="left" >#{Estado}</td>' +
                '<td id="LiderResp" style="padding-left:5px;' + showLiderResp + '" align="left" >#{LiderResp}</td>' +
                '<td id="MotivoSiniestro" style="padding-left:5px;' + showMotivo + '" align="left" >#{MotivoSiniestro}</td>' +
                '<td id="imgClose" >&nbsp;</td>' +
                '</tr>'



            };

            var table = '<table width="100%" id="tbl" border="1" cellpadding="0" cellspacing="0" style="font-size:13px; background-color: Transparent;"><thead><tr>';

            /// Genero la fila de encabezado
            $.each(t, function (key, val) {
                table += $.tmpl(templates.th, val);
            });

            table += '</tr></thead><tbody class="BodyDetalle">';

            /// Genero las filas del body
            row = 0;
            var totalRendicion = 0;
            for (var i = 0; i < datos.length; i++) {
                //table += $.tmpl(templates.td, datos[row]);
                var td = $.tmpl(templates.td, datos[row]);


                if (datos[row]["TipoComprobante"] == "CON") {
                    totalRendicion += parseFloat(datos[row]["Monto"].replace(",", "."));
                }


                /// Si estoy en una edicion entonces las guias que no estan grabadas en la rendición
                /// no deben aparecer marcada.
                if ("<%= IdRendicion %>" != "0") {

                    /// Si es una guia que no pertenece a la rendicion que se esta editando
                    /// debo restar el monto del totalizador y desmarcar la guia.
                    if (parseFloat(datos[row]["IdCabeceraRendicion"]) < 0) {
                        // si es menor a cero es porque no pertenece a rendicion 
                        // que se esta editando.
                        td = td.replace("checked", "");

                        //                        //Si tiene devolucion entonces no debo restar el valor, porque tampoco lo sume mas arriba.
                        //                        if (datos[row]["FechaDevolucion"] == "&nbsp;") {
                        //                            totalRendicion -= parseFloat(datos[row]["Monto"].replace(",", "."));
                        //                        }

                    }
                }

                table += td;
                row++;
            }

            table += '</tbody></table>';

            /// Asigno la tabla generada al div para dibujarla
            $("#divtbl")[0].innerHTML = table;
            $("#tbl tbody tr:odd").addClass("rowAlt");
            $("#tbl tbody tr:even").addClass("rowSpl");

            ucFormaPagoMultiple_SetTotales(parseFloat("<%= SaldoTransportista %>").toFixed(2), totalRendicion.toFixed(2));
        }
        else {

            $("#tdPagos").hide();
            $("#tdGrabar").hide();
            $("#tdMensaje").show();

        }

    }

    $(document).ready(function () {

        window.setTimeout(function () {
            PageMethods.FiltroRendicion("", "", "Todos", "<%=EstadoRendicion %>", callBackFunction, ErroFunction);
            ucFormaPagoMultiple_CargarFormasPago();

            if ("<%=IdRendicion %>" != "0") {
                $("#lblTituloRendicion").show();
                $("#lblTituloRendicion").text("Rendición Nro: " + "<%=IdRendicion %>" + " - <%=EstadoRendicion %>");
            }
        });
    }, 300);


    function FiltrarGuias() {

        var transporte = $find("<%= cboTransportes.ClientID %>").get_text();
        var fechaInicial = "";
        var fechaFinal = "";

        if ($find("<%= txtFechaInicial.ClientID %>").get_selectedDate() != null)
            fechaInicial = $find("<%= txtFechaInicial.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

        if ($find("<%= txtFechaFinal.ClientID %>").get_selectedDate() != null)
            fechaFinal = $find("<%= txtFechaFinal.ClientID %>").get_selectedDate().format("dd/MM/yyyy");


        PageMethods.FiltroRendicion(fechaInicial, fechaFinal, transporte, "<%=EstadoRendicion %>", callBackFunction, ErroFunction);
    }

    function ErroFunction(datos) {
        alert(datos._message);
    }

    
</script>
<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        <Scripts>
            <asp:ScriptReference Path="~/FuncionesComunes.js" />
        </Scripts>
    </asp:ScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Vista" VisibleTitlebar="true"
        Title="Atención">
    </telerik:RadWindowManager>
    <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%;">
        <tr>
            <td>
                <div class="Header_panelContainerSimple">
                    <div class="CabeceraInicial">
                    </div>
                </div>
                <div class="CabeceraContent">
                    <table width="95%" border="0" cellspacing="0">
                        <tr>
                            <td valign="top" align="center" style="width: 100%;">
                                <div style="position: relative; top: -25px">
                                    <asp:Label Font-Names="Bookman Old Style" Font-Size="22pt" ForeColor="#5F5F5F" ID="lblTitulo"
                                        runat="server">Gestión de Rendiciones</asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr id="trTitulo">
                            <td valign="top" align="center" style="width: 100%;">
                                <div style="position: relative; top: -15px">
                                    <asp:Label Font-Names="Bookman Old Style" Font-Size="17pt" ForeColor="#5F5F5F" ID="lblTituloRendicion"
                                        runat="server"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <div class="ContentFiltro">
                                    <table width="90%" border="0" cellspacing="3" cellpadding="0">
                                        <tr>
                                            <td align="left" style="width: 110px">
                                                <asp:Label ID="Label2" runat="server" SkinID="lblBlack">Fecha Desde:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <telerik:RadDatePicker ID="txtFechaInicial" runat="server" Skin="Vista" Width="138px"
                                                    Culture="Spanish (Argentina)">
                                                    <DateInput InvalidStyleDuration="100" LabelCssClass="radLabelCss_Vista" Skin="Vista"
                                                        SelectionOnFocus="SelectAll" ShowButton="True">
                                                    </DateInput>
                                                    <Calendar Skin="Vista">
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td align="left" style="padding-left: 5px">
                                                <asp:Label ID="Label3" runat="server" SkinID="lblBlack">Fecha Hasta:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <telerik:RadDatePicker ID="txtFechaFinal" runat="server" Skin="Vista" Width="138px"
                                                    Culture="Spanish (Argentina)">
                                                    <DateInput InvalidStyleDuration="100" LabelCssClass="radLabelCss_Vista" Skin="Vista"
                                                        SelectionOnFocus="SelectAll" ShowButton="True">
                                                    </DateInput>
                                                    <Calendar Skin="Vista">
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td align="right" valign="bottom" rowspan="2" style="width: 60px">
                                                <img alt="Filtrar Mensajes" src="Imagenes/Search.png" onclick="FiltrarGuias();return false;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label4" runat="server" SkinID="lblBlack">Transporte:</asp:Label>
                                            </td>
                                            <td align="left" colspan="3">
                                                <telerik:RadComboBox ID="cboTransportes" runat="server" Skin="Vista" Width="100%">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Value="" Text="Todos" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="padding-top: 10px">
                                <div id="divtbl">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 10px; display: none" align="center" id="tdPagos">
                                <uc1:ucFormaPagoMultiple ID="ucFPMultiple" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 10px; display: none" align="center" id="tdDevolucion" runat="server">
                                <table width="50%" border="0" cellspacing="3" cellpadding="0">
                                    <tr>
                                        <td align="center" colspan="2">
                                            <h3 runat="server" id="lblTituloDevolucion">
                                                INGRESE LA INFORMACIÓN DE ENVÍO</h3>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 100px">
                                            <asp:Label ID="Label5" runat="server" SkinID="lblBlack">Transporte:</asp:Label>
                                        </td>
                                        <td align="left">
                                            <telerik:RadComboBox ID="cboTransporteDevolucion" runat="server" Skin="Vista" Width="320px"
                                                MarkFirstMatch="true" AllowCustomText="true">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 100px">
                                            <asp:Label ID="Label6" runat="server" SkinID="lblBlack">Nro Guía:</asp:Label>
                                        </td>
                                        <td align="left">
                                            <telerik:RadTextBox ID="txtNroGuiaDevolucion" runat="server" EmptyMessage="Ingrese nro guia del transporte"
                                                Skin="Vista" Width="320px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 100px">
                                            <asp:Label ID="Label7" runat="server" SkinID="lblBlack">Fecha Envio:</asp:Label>
                                        </td>
                                        <td align="left">
                                            <telerik:RadDatePicker ID="txtFechaEnvioDevolucion" runat="server" Skin="Vista" Width="138px"
                                                Culture="Spanish (Argentina)">
                                                <DateInput InvalidStyleDuration="100" LabelCssClass="radLabelCss_Vista" Skin="Vista"
                                                    SelectionOnFocus="SelectAll" ShowButton="True">
                                                </DateInput>
                                                <Calendar Skin="Vista">
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 10px" align="center" id="tdObservaciones" runat="server">
                                <table width="50%" border="0" cellspacing="3" cellpadding="0">
                                    <tr>
                                        <td align="center" colspan="2">
                                            <h3 runat="server" id="H1">
                                                INFORMACION DE OBSERVACIONES</h3>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="tdObsCobranzaTitulo">
                                        <td align="left" colspan="2" style="border-bottom: solid 1px black">
                                            <h4 runat="server" id="H2">
                                                COBRANZA</h4>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="tdObsCobranzaDatos1">
                                        <td align="right" style="width: 100px">
                                            <asp:Label ID="Label9" runat="server" SkinID="lblBlack">Observación:</asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox Width="356px" ID="txtObsCobranza" TextMode="MultiLine" Rows="5" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="tdObsLogisticaTitulo">
                                        <td align="left" colspan="2" style="border-bottom: solid 1px black">
                                            <h4 runat="server" id="H3">
                                                LOGISTICA</h4>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="tdObsLogisticaDatos1">
                                        <td align="right" style="width: 180px">
                                            <asp:Label ID="Label8" runat="server" SkinID="lblBlack">Monto Rendir (Nota Credito):</asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox Width="85px" ID="txtMonto" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="tdObsLogisticaDatos">
                                        <td align="right" style="width: 100px">
                                            <asp:Label ID="Label10" runat="server" SkinID="lblBlack">Observación:</asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox Width="356px" ID="txtObsLogistica" TextMode="MultiLine" Rows="5" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td runat="server" style="padding-top: 10px;" align="center" id="tdGrabar">
                                <input class="btnBasic_Marzzan" type="button" value="Grabar" onclick="GrabarRendicion();return false;" />
                            </td>
                        </tr>
                        <tr>
                            <td runat="server" style="padding-top: 10px;" align="center">
                                <table width="100%" border="0" cellspacing="3" cellpadding="0">
                                    <tr>
                                        <td runat="server" style="padding-top: 10px;" align="center" id="tdAprobacion">
                                            <input class="btnBasic_Marzzan" type="button" value="Aprobar" onclick="AprobarRendicion();return false;" />
                                        </td>
                                        <td runat="server" style="padding-top: 10px;" align="center" id="tdObservarcion">
                                            <input class="btnBasic_Marzzan" type="button" value="Observar" onclick="ObservarRendicion();return false;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="display: none; color: Red; background-color: White; border: solid 1px black;
                                font-weight: bold; padding: 10px" align="center" id="tdMensaje">
                                <span runat="server" id="lblDescripcionMensaje"></span>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
