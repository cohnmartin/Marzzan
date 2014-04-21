<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionBajaClientes.aspx.cs"
    Inherits="GestionBajaClientes" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión Baja Clientes</title>
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
    </style>
</head>

<script type="text/javascript">
    var cantidadBajas;
    function BuscarClientes() {
        var grupo = $find("<%= cboGrupos.ClientID %>").get_value();
        ShowWaiting("Buscando Clientes");
        PageMethods.GetClientesBajas(grupo, callBackFunction, ErroFunction);
    }

    function ControlSeleccion(objCheck) {
        var totalActual = cantidadBajas + $('input[type="checkbox"]:checked').length;
        if (totalActual > 6) {

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
    }

    function callBackFunction(response) {

        datos = response["Clientes"];

        var t = [
        { "header": '&nbsp;' },
        { "header": "Cliente" },
        { "header": "Código" },
        { "header": "Ultima Compra" },
        { "header": "Dias Sin Compra" },
        { "header": "Grupo" },
        { "header": "Saldo"}];

        var templates = {
            th: '<th>#{header}</th>',
            td: '<tr style="cursor:pointer" >' +
                    '<td align="center" style="width:20px" ><input type="checkbox" id="chkSelectRow" onclick="ControlSeleccion(this);"  /></td>' +
                    '<td align="left" style="padding-left:5px">#{Cliente}</td>' +
                    '<td align="left" style="width:70px" id="codigo" >#{Codigo}</td>' +
                    '<td align="left" style="width:110px">#{UltimaCompra}</td>' +
                    '<td align="center" style="width:110px" id="dias">#{DiasSinCompras}</td>' +
                    '<td style="width:170px" id="grupo">#{Grupo}</td>' +
                    '<td style="width:120px" id="saldo">#{Saldo}</td>' +
                '</tr>'
        };

        var table = '<table id="tbl" width="80%" border="1" cellpadding="0" cellspacing="0" style="font-size:13px; background-color: Transparent;"><thead><tr>';

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

        cantidadBajas = parseInt(response["CantidadBajas"]);
        $("#cantidadBajas").text(response["CantidadBajas"]);
        $("#btnGrabar").show();


        HideWaiting();
    }

    function ErroFunction(err) {
        alert(err._message);
    }

    function GrabarBajas() {
        ShowWaiting("Buscando Clientes");
        var codigos = new Array();
        var datosComp = new Array()

        $('input[type="checkbox"]:checked').each(function() {

            //codigos.push($(this).closest("TR").find("td[id=codigo]").text());
            var codigo = $(this).closest("TR").find("td[id=codigo]").text();
            var dias = $(this).closest("TR").find("td[id=dias]").text();
            var grupo = $(this).closest("TR").find("td[id=grupo]").text()
            var saldo = $(this).closest("TR").find("td[id=saldo]").text()
            datosComp.push({ "codigo": codigo, "dias": dias, "grupo": grupo, "saldo": saldo });

        });

        PageMethods.GrabarBajas(datosComp, callBackFunction, ErroFunction);
    }

    $(document).ready(function() {

        cantidadBajas = parseInt("<%=CantidadBajas %>");
        $("#cantidadBajas").text("<%=CantidadBajas %>");

        $("#btnGrabar")
          .button()
          .click(function(event) {
              event.preventDefault();
              GrabarBajas();
          });
    });

    
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
                                        runat="server">Gestión Baja Clientes</asp:Label>
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
                                                <telerik:RadComboBox ID="cboGrupos" runat="server" Skin="Vista" Width="200px">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Value="" Text="Todos los Grupos" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td align="right" valign="bottom">
                                                <img alt="Filtrar Mensajes" src="Imagenes/Search.png" onclick="BuscarClientes();return false;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="font-weight: normal">
                                                Usted posee: <span id="cantidadBajas" style="font-weight: bold; padding-right: 5px">
                                                </span>bajas en el mes actual, recuerde que solo puede dar 6 bajas al mes.
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
                            <td align="center" style="padding-top: 10px">
                                <button id="btnGrabar" style="display: none">
                                    Grabar Bajas</button>
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
