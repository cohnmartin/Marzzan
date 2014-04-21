<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="ConsultaImpresionesAsistencia.aspx.cs"
    Inherits="ConsultaImpresionesAsistencia" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consulta de Impresiones</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="Scripts/jquery.tmpl.1.1.1.js" type="text/javascript"></script>
    <script src="Scripts/Modal-master/jquery.modal.js" type="text/javascript"></script>
    <link href="Scripts/Modal-master/jquery.modal.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
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
        .rowAlt
        {
            background-color: #CCF2FF;
            font-size: 12px;
        }
        .rowSpl
        {
            background-color: White;
            font-size: 12px;
        }
        .alert-JQueryUI
        {
            font-size: 14px;
        }
        .TVista thead
        {
            background-color: White;
        }
        .TVista .Theader
        {
            background-position: 0 -2300px;
            border-top: 0;
            background-image: url('Imagenes/sprite_vista.gif');
            background-repeat: repeat;
            border-style: solid;
            border-width: 1px;
            border-color: #fff #dcf2fc #3c7fb1 #8bbdde;
            color: #333;
            font-family: "segoe ui" ,arial,sans-serif;
            font-size: 12px;
            height: 23px;
            text-align: center;
            padding: 0px 0px 0px 0px;
            line-height: 1em;
        }
        .TVista .tdSimple
        {
            color: #333;
            font: 12px/16px "segoe ui" ,arial,sans-serif;
            border-color: #fff #EFEFEF #fff #EFEFEF;
            border-style: solid;
            border-width: 0 1px 1px;
            padding-left: 2px;
            padding-top: 3px;
            padding-bottom: 3px;
            background-color: White;
            text-align: left;
            vertical-align: middle;
        }
        .TVista .tdSimpleAlt
        {
            color: #333;
            font: 12px/16px "segoe ui" ,arial,sans-serif;
            border-color: #fff #EFEFEF #DBF3FD #EFEFEF;
            border-style: solid;
            border-width: 0 1px 1px;
            padding-left: 2px;
            padding-top: 3px;
            padding-bottom: 3px;
            background-color: #E2F5FE;
            text-align: left;
        }
        .TVista
        {
            border: 1px solid #bbb99d;
        }
        
        .TVista .tdFunctionAdd
        {
            font-weight: bold;
            color: #333;
            font: 12px/16px "segoe ui" ,arial,sans-serif;
            padding-top: 2px;
            padding-bottom: 2px;
            text-align: center;
            background-image: url('Imagenes/sprite_vista.gif');
            background-position: 0 -4900px;
            color: Black;
            text-align: right;
        }
    </style>
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            var ctrGrupo;


            jQuery(function () {

                options = {
                    serviceUrl: 'ASHX/LoadGrupos.ashx',
                    width: '384',
                    minChars: 3,
                    showOnFocus: true,
                    showInit: false,
                    zIndex: 922000000
                };

                ctrGrupo = $('#' + "<%= txtGrupo.ClientID %>").autocomplete(options);


            });
            function GenerarReporte() {

                var FechaInicial = $find("<%=txtFechaInicial.ClientID%>").get_selectedDate().format("dd/MM/yyyy");
                var FechaFinal = $find("<%=txtFechaFinal.ClientID%>").get_selectedDate().format("dd/MM/yyyy");

                $("#<%=hdf_Periodo.ClientID %>").val(FechaInicial + " a " + FechaFinal);
                $("#<%=btnexportar.ClientID %>").click();
            }

            function ControlarDatos() {
                var comboTransporte = $find("<%= cboTransporte.ClientID%>");
                var FechaInicial = $find("<%=txtFechaInicial.ClientID%>");
                var FechaFinal = $find("<%=txtFechaFinal.ClientID%>");


                if (comboTransporte.get_value() == 0) {
                    alert('Debe Seleccionar un transportista para consultar las notas de pedido');
                    return false;
                }

                if (FechaInicial.get_selectedDate() == null) {
                    alert('Debe Seleccionar la fecha incial para consultar las notas de pedido');
                    return false;
                }

                if (FechaFinal.get_selectedDate() == null) {
                    alert('Debe Seleccionar la fecha final para consultar las notas de pedido');
                    return false;
                }

                if (FechaFinal.get_selectedDate() < FechaInicial.get_selectedDate()) {
                    alert('La fecha final no puede ser menor a la fecha inicial.');
                    return false;
                }

                var grupo = ctrGrupo.get_SelectedValue() != undefined && ctrGrupo.get_SelectedValue() != "" ? ctrGrupo.get_SelectedValue() : null;


                ShowWaiting("Buscando Pedidos...");

                PageMethods.ConsultarPedidos(grupo, comboTransporte.get_value(), FechaInicial.get_selectedDate().format("dd/MM/yyyy"), FechaFinal.get_selectedDate().format("dd/MM/yyyy"), onSuccess, function () { });

            }

            function onSuccess(response) {

                datos = response["Datos"];

                var t = [
                { "header": "Nro" },
                { "header": "Solicitante a:" },
                { "header": "Revendedor" },
                { "header": "Fecha" },
                { "header": "Tipo" },
                { "header": "Total" },
                { "header": "Forma Pago" },
                { "header": "Impresión" },
                { "header": "FechaImpresion"}];

                var templates = {
                    th: '<th class="Theader">#{header}</th>',
                    td: '<tr>' +
                    '<td align="center" style="width:50px" style="padding-left:5px">#{Nro}</td>' +
                    '<td align="left" id="Solicitante" >#{Solicitante}</td>' +
                    '<td align="left" id="Cliente" >#{Destinatario}</td>' +
                    '<td align="center" style="width:70px" id="FechaPedido">#{FechaPedido}</td>' +
                    '<td align="center" style="width:70px" id="Tipo">#{TipoPedido}</td>' +
                    '<td align="center" style="width:70px" id="MontoTotal">#{MontoTotal}</td>' +
                    '<td align="center" style="width:120px" id="FormaPago">#{FormaPago}</td>' +
                    '<td align="center" style="width:70px" id="NroImpresion">#{NroImpresion}</td>' +
                    '<td align="center" style="width:100px" id="FechaImpresion">#{FechaImpresion}</td>' +
                    '<td style="width:70px;display:none" id="IdCabeceraPedido" >#{IdCabeceraPedido}</td>' +
                '</tr>'
                };

                var table = '<table class="TVista" id="tbl" width="100%" cellpadding="0" cellspacing="0" style="font-size:13px;"><thead>' +
                '<tr><td colspan="9" class="tdFunctionAdd" ><img src="Imagenes/excel_24x24.gif" height="20px" onclick="GenerarReporte();return false;" alt="Generar Reporte" /></td></tr>' +
                '<tr>';

                /// Genero la fila de encabezado
                $.each(t, function (key, val) {
                    table += $.tmpl(templates.th, val);
                });

                table += '</tr></thead><tbody>';


                if (datos.length > 0) {
                    /// Genero las filas del body
                    row = 0;
                    for (var i = 0; i < datos.length; i++) {
                        table += $.tmpl(templates.td, datos[row]);
                        row++;
                    }
                }
                else {
                    table += "<td colspan='9' class='tdSimple' align='center'>No existen pedidos para los filtros seleccionados</td>";
                }

                table += '</tbody></table>';

                /// Asigno la tabla generada al div para dibujarla
                $("#divtbl")[0].innerHTML = table;
                $("#tbl tbody tr:odd").addClass("tdSimple");
                $("#tbl tbody tr:even").addClass("tdSimpleAlt");

                HideWaiting();
            }
        </script>
    </telerik:RadScriptBlock>
</head>
<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;" class="main">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"
        LoadScriptsBeforeUI="true">
        <Scripts>
            <asp:ScriptReference Path="~/FuncionesComunes.js" />
        </Scripts>
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="Header_panelContainerSimple">
                    <div class="CabeceraInicial">
                    </div>
                </div>
                <div class="CabeceraContent">
                    <table width="95%" border="0" cellspacing="0" style="border-bottom-style: ridge;
                        border-bottom-width: thin">
                        <tr>
                            <td colspan="2" style="color: #0066CC; font-family: Sans-Serif; font-size: 11px;
                                text-transform: capitalize">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100px; color: #993300; font-family: Sans-Serif; font-size: 11px">
                                            <asp:Label ID="Label1" runat="server" Text="Grupos:" SkinID="lblBlue"></asp:Label>
                                        </td>
                                        <td align="left" colspan="3">
                                            <asp:TextBox Width="300px" ID="txtGrupo" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="color: #0066CC; font-family: Sans-Serif; font-size: 12px;
                                text-transform: capitalize">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100px; color: #993300; font-family: Sans-Serif; font-size: 12px">
                                            <asp:Label SkinID="lblBlue" ID="lblPromociones3" Text="Transporte:" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 290px;">
                                            <telerik:RadComboBox ID="cboTransporte" runat="server" Skin="Vista" Width="100%" />
                                        </td>
                                        <td style="padding-left: 15px; width: 70px; color: #993300; font-family: Sans-Serif;
                                            font-size: 12px" colspan="2">
                                            &nbsp;
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="color: #0066CC; font-family: Sans-Serif; font-size: 11px;
                                text-transform: capitalize">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100px; color: #993300; font-family: Sans-Serif; font-size: 11px">
                                            <asp:Label ID="UserNameLabel0" runat="server" Text="Fecha Incial:" SkinID="lblBlue"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 290px;">
                                            <telerik:RadDatePicker ID="txtFechaInicial" runat="server" Skin="Web20" Width="138px"
                                                Culture="Spanish (Argentina)">
                                                <DateInput ID="DateInput1" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Web20"
                                                    Skin="Web20" SelectionOnFocus="SelectAll" ShowButton="True">
                                                </DateInput>
                                                <Calendar ID="Calendar1" runat="server" Skin="Web20">
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td style="padding-left: 15px; width: 70px; color: #993300; font-family: Sans-Serif;
                                            font-size: 11px">
                                            <asp:Label ID="Label2" runat="server" Text="Fecha Final:" SkinID="lblBlue"></asp:Label>
                                        </td>
                                        <td align="left" colspan="3">
                                            <telerik:RadDatePicker ID="txtFechaFinal" runat="server" Skin="Web20" Width="138px"
                                                Culture="Spanish (Argentina)">
                                                <DateInput ID="DateInput2" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Web20"
                                                    Skin="Web20" SelectionOnFocus="SelectAll" ShowButton="True">
                                                </DateInput>
                                                <Calendar ID="Calendar2" runat="server" Skin="Web20">
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" SkinID="btnBasic" Width="77px"
                                    OnClientClick="ControlarDatos();return false;" />
                            </td>
                        </tr>
                        <tr>
                            <td style="color: #993300; font-family: Sans-Serif; font-size: 11px" colspan="2">
                                <asp:Button ID="btnexportar" runat="server" Text="Exportar" OnClick="btnBuscar_Click"
                                    Style="display: none" />
                                <asp:HiddenField ID="hdf_Periodo" Value="" runat="server" />
                                <div id="divtbl">
                                </div>
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
