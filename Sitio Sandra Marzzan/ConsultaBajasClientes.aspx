<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultaBajasClientes.aspx.cs"
    Inherits="ConsultaBajasClientes" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Consulta Bajas Clientes</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <link href="Scripts/Jquery-UI/css/start/jquery-ui-1.10.3.custom.min.css" rel="stylesheet"
        type="text/css" />

    <script src="Scripts/Jquery-UI/js/jquery-1.9.1.js" type="text/javascript"></script>

    <script src="Scripts/Jquery-UI/js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>

    <script src="Scripts/jquery.tmpl.1.1.1.js" type="text/javascript"></script>

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
    </style>
</head>

<script type="text/javascript">
    function BuscarClientes() {
        var mes = $find("<%= cboMeses.ClientID %>").get_value();
        ShowWaiting("Buscando Bajas Clientes...");
        PageMethods.GetClientesBajas(mes, callBackFunction, ErroFunction);
    }

    function ControlSeleccion(objCheck) {

        $("#dialog").dialog({
            dialogClass: "alert-JQueryUI",
            show: {
                effect: "blind",
                duration: 150
            },
            hide: {
                effect: "blind",
                duration: 300
            },
            modal: true,
            width: 400,
            buttons: {
                Aceptar: function() {
                    $(this).dialog("close");
                }
            }
        });

        $(objCheck).removeAttr("checked");

    }

    function callBackFunction(response) {

        datos = response["Clientes"];

        var t = [
        { "header": "Cliente" },
        { "header": "Código" },
        { "header": "Fecha Baja" },
        { "header": "Dias Sin Compra" },
        { "header": "Grupo" },
        { "header": "Mes-Año"}];

        var templates = {
            th: '<th class="Theader">#{header}</th>',
            td: '<tr style="cursor:pointer" >' +
                    '<td align="left" style="padding-left:5px">#{Cliente}</td>' +
                    '<td align="left" style="width:70px" id="codigo" >#{Codigo}</td>' +
                    '<td align="left" style="width:70px" id="FechaBaja" >#{FechaBaja}</td>' +
                    '<td align="center" style="width:110px" id="dias">#{DiasSinCompras}</td>' +
                    '<td style="width:170px" id="grupo">#{Grupo}</td>' +
                    '<td style="width:170px" id="MesAño">#{MesAño}</td>' +
                '</tr>'
        };

        var table = '<table class="TVista" id="tbl" width="80%" cellpadding="0" cellspacing="0" style="font-size:13px;"><thead><tr>';

        /// Genero la fila de encabezado
        $.each(t, function(key, val) {
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
            table += "<td colspan='6' class='tdSimple' align='center'>No existen bajas para el filtro seleccionado</td>";
        }

        table += '</tbody></table>';

        /// Asigno la tabla generada al div para dibujarla
        $("#divtbl")[0].innerHTML = table;
        $("#tbl tbody tr:odd").addClass("tdSimple");
        $("#tbl tbody tr:even").addClass("tdSimpleAlt");

        HideWaiting();
    }

    function ErroFunction(err) {
        alert(err._message);
    }

</script>

<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White; height: 100%;" class="main">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"
        LoadScriptsBeforeUI="true">
        <Scripts>
            <asp:ScriptReference Path="~/FuncionesComunes.js" />
        </Scripts>
    </asp:ScriptManager>
    <div id="dialog" title="Cantidad Superada" style="display: none;">
        No puede seleccionar mas revendedores en este mes.
    </div>
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
                                        runat="server">Consulta Bajas Clientes</asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <div class="ContentFiltro">
                                    <table width="70%" border="0" cellspacing="3" cellpadding="0">
                                        <tr>
                                            <td align="right" style="width: 35%; padding-right: 5px">
                                                <asp:Label ID="Label7" runat="server" SkinID="lblBlack">Grupos:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <telerik:RadComboBox ID="cboMeses" runat="server" Skin="Vista" Width="200px">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Value="" Text="Todos los Meses" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td align="right" valign="bottom">
                                                <img alt="Filtrar Mensajes" src="Imagenes/Search.png" onclick="BuscarClientes();return false;" />
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
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
