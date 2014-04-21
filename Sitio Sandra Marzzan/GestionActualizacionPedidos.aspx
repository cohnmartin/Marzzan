<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionActualizacionPedidos.aspx.cs"
    Inherits="GestionActualizacionPedidos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión Actualización Pedidos</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />

    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>

    <script src="Scripts/jquery.tmpl.1.1.1.js" type="text/javascript"></script>

    <style type="text/css">
        .rowAlt
        {
            background-color: #CCF2FF;
        }
        .rowSpl
        {
            background-color: White;
        }
        .ContentFiltro
        {
            font: 13px Verdana, Geneva, sans-serif;
            background-color: #F5F5F5;
            border: 3px solid #DBDBDB;
            margin-top: 0px;
            margin-right: 0px;
            padding: 0px;
            width: 90%;
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
            width: 136px;
            text-align: center;
            cursor: pointer;
            font-weight: bold;
        }
    </style>
</head>

<script type="text/javascript">

    function BuscarPedidos() {

        var fechaInicial = "";
        var fechaFinal = "";

        if ($find("<%= txtFechaInicial.ClientID %>").get_selectedDate() != null)
            fechaInicial = $find("<%= txtFechaInicial.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

        if ($find("<%= txtFechaFinal.ClientID %>").get_selectedDate() != null)
            fechaFinal = $find("<%= txtFechaFinal.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

        if (fechaInicial == "" || fechaFinal == "") {
            alert("Se deben cargar ambas fechas");
        }
        else {
            $("#DivW").show();
            PageMethods.BuscarPedidos(fechaInicial, fechaFinal, onSuccessFiler, onFailure);
        }

    }

    function EjecutarProcesoActualizacion() {

        var fechaInicial = "";
        var fechaFinal = "";

        if ($find("<%= txtFechaInicial.ClientID %>").get_selectedDate() != null)
            fechaInicial = $find("<%= txtFechaInicial.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

        if ($find("<%= txtFechaFinal.ClientID %>").get_selectedDate() != null)
            fechaFinal = $find("<%= txtFechaFinal.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

        if ($('.seleccion:checked').length == 0) {
            alert("Se tiene que seleccionar por lo menos un pedido");
        }
        else if (fechaInicial == "" || fechaFinal == "") {
            alert("Se deben cargar ambas fechas");
        }
        else {

            var ids = new Array();
            $('.seleccion:checked').each(function() {


                ids.push($(this).attr('idCab'));


            });
            $("#DivW").show();
            PageMethods.EjecutarProcesoActualizacion(ids.join(','), fechaInicial, fechaFinal, onSuccessProcess, onFailure);
        }
    }

    function onSuccessProcess(datos) {
        alert("Los Datos se actualizaron correctamente");
        onSuccessFiler(datos);
    }
    function checkAll(obj) {

        if ($(obj).attr('checked') != undefined) {
            $('.seleccion').removeAttr('checked');
            $('.seleccion').attr('checked', 'checked');
        }
        else {
            $('.seleccion').removeAttr('checked');
        }

    }
    function onSuccessFiler(datos) {

        var t = [
        { "header": "<input type='checkbox' id='chkAll' onclick='checkAll(this);' />" },
        { "header": "Nro Pedido" },
        { "header": "Fecha Pedido" },
        { "header": "Revendedor" },
        { "header": "Solicitante" },
        { "header": "Monto Pedido" },
        { "header": "&nbsp;"}];

        var templates = {
            th: '<th>#{header}</th>',
            td: '<tr >' +
                    '<td align="center" style="width:30px" ><input type="checkbox" id="chkSeleccion" class="seleccion" idCab="#{IdCabecera}" /></td>' +
                    '<td align="left" style="width:110px" id="NroPedido">#{NroPedido}</td>' +
                    '<td align="left" style="width:110px" id="Fecha">#{Fecha}</td>' +
                    '<td align="left"  id="Revendedor">#{Revendedor}</td>' +
                    '<td align="left"  id="Solicitante">#{Solicitante}</td>' +
                    '<td align="center" style="width:100px" id="Monto">#{Monto}</td>' +
                    '<td align="center" style="width:100px" id="IdCabecera">#{IdCabecera}</td>' +
                '</tr>'
        };

        var table = '<table width="95%" id="tbl" border="1" cellpadding="0" cellspacing="0" style="font-size:13px; background-color: Transparent;"><thead><tr>';

        /// Genero la fila de encabezado
        $.each(t, function(key, val) {
            table += $.tmpl(templates.th, val);
        });

        table += '</tr></thead><tbody>';

        /// Genero las filas del body
        row = 0;
        for (var i = 0; i < datos.length; i++) {
            table += $.tmpl(templates.td, datos[row]);
            row++;
        }

        table += '</tbody></table>';

        /// Asigno la tabla generada al div para dibujarla
        $("#divtbl")[0].innerHTML = table;
        $("#tbl tbody tr:odd").addClass("rowAlt");
        $("#tbl tbody tr:even").addClass("rowSpl");
        $("#DivW").hide();
    }

    function onFailure(datos) {
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
    <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
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
                                    <asp:Label Font-Names="Bookman Old Style" Font-Size="22pt" ForeColor="#5F5F5F" ID="Label1"
                                        runat="server">Gestión Actualización Pedidos</asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <div class="ContentFiltro">
                                    <table width="100%" border="0" cellspacing="3" cellpadding="0">
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
                                            <td align="right" valign="bottom" style="width: 60px">
                                                <img alt="Buscar Pedidos" src="Imagenes/Search.png" onclick="BuscarPedidos();return false;" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <div id="divtbl">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <input type="submit" onclick="EjecutarProcesoActualizacion();return false;" value="Ejecutar Proceso" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <div style="display: none" id="DivW">
        <div id="DivBloque" class="progressBackgroundFilterBlue">
        </div>
        <div style="position: absolute; top: 45%; left: 38%; padding: 5px; width: 24%; z-index: 1001;
            background-color: Transparent; vertical-align: middle; text-align: center;">
            <table border="0" cellpadding="0" cellspacing="0" style="height: 62px;">
                <tr>
                    <td align="center">
                        <img alt="a" src="Imagenes/waiting.gif" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lbltitulopaciente1" runat="server" Font-Bold="True" Font-Names="Thomas"
                            Font-Size="12px" ForeColor="Black" Height="21px" Style="vertical-align: middle"
                            Text="Buscado Pedidos...">
                        </asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
