<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="GestionComposicionRemitos.aspx.cs"
    Inherits="GestionComposicionRemitos" %>

<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="AjaxInfo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <title>Gestión Composición Remitos</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <link href="UploadFiles/CSS/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/Jquery-UI/css/start/jquery-ui-1.10.3.custom.min.css" rel="stylesheet"
        type="text/css" />
    <link href="Scripts/Modal-master/jquery.modal.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/Jquery-UI/js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="Scripts/jquery.tmpl.1.1.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery.tablegroup.js" type="text/javascript"></script>
    <script src="Scripts/Modal-master/jquery.modal.js" type="text/javascript"></script>
</head>
<script type="text/javascript">
    var ctrRemito;
    var ctrComponente;

    $(document).ready(function () {
        window.setTimeout(function () {
            PageMethods.FiltroComponentes("", callBackFunction, function () { });

            options = {
                serviceUrl: 'ASHX/LoadProductos.ashx',
                width: '384',
                minChars: 2,
                showInit: false,
                showOnFocus: true,
                params: { Tipo: encodeURIComponent('R') },
                zIndex: 9999999999
            };

            ctrRemito = $('#' + "<%= txtRemito.ClientID %>").autocomplete(options);

            ctrComponente = $('#' + "<%= txtComponente.ClientID %>").autocomplete(options);

        }, 200);



    });

    function FiltrarDespachos() {
        var valorFiltro = $find("<%= cboRemitos.ClientID %>").get_value();
        if (valorFiltro != "") {
            $('#tbl tr[filtro*=' + valorFiltro + ']').siblings().css("display", "none");
            $('#tbl tr[filtro*=' + valorFiltro + ']').css("display", "block");
        }
        else {
            $('#tbl tr').css("display", "block");
        }
    }


    function EliminarComponente(id) {
        ShowWaiting("Eliminando Componente...");

        PageMethods.EliminarComponentes(id, callBackFunction, function () { });

    }

    function callBackFunction(datos) {

        var t = [
        { "header": "Remito" },
        { "header": "Componente" },
        { "header": "Cantidad" },
        { "header": "&nbsp;"}];

        var templates = {
            th: '<th class="Theader">#{header}</th>',
            td: '<tr style="display:inline" filtro="#{CodigoRemito}" >' +
                '<td  align="left" >#{Remito}</td>' +
                '<td align="left" filtro="Comprobante">#{Componente}</td>' +
                '<td align="left" style="width:70px">#{Cantidad}</td>' +
                '<td align="center" ><img alt="Eliminar Componente" src="Imagenes/delete.gif" width="16px" style="cursor:pointer" onclick="EliminarComponente(#{IdComposicion});return false;" /></td>' +
                '</tr>'
        };

        var table = '<table width="100%" id="tbl" cellpadding="0" cellspacing="0" class="TVista" style="font-size:13px; background-color: white;"><thead>' +
        '<tr>';

        /// Genero la fila de encabezado
        $.each(t, function (key, val) {
            table += $.tmpl(templates.th, val);
        });

        table += '</tr></thead><tbody>';

        /// Genero las filas del body
        row = 0;
        for (var i = 0; i < datos["Componentes"].length; i++) {
            var td = $.tmpl(templates.td, datos["Componentes"][row]);

            table += td;
            row++;
        }

        table += '</tbody><tfoot>' +
        '</tfoot></table>';

        /// Asigno la tabla generada al div para dibujarla
        $("#divtbl")[0].innerHTML = table;
        $("#tbl tbody tr:odd").addClass("rowAlt");
        $("#tbl tbody tr:even").addClass("rowSpl");

        $('#' + "tbl").tableGroup({ groupColumn: 1, groupClass: 'rgGroupItem', useNumChars: 0, showInit: false });

        FiltrarDespachos();

        HideWaiting();
    }

    function ShowNuevoComponente() {

        var remitoSelected = $find("<%= cboRemitos.ClientID %>");

        if (remitoSelected.get_value() != "") {
            ctrRemito.set_Value(remitoSelected.get_text(), remitoSelected.get_value());
        }
        else {
            ctrComponente.Clear();
            ctrRemito.Clear();
        }

        $('#divNuevoComponente').modal(
        {
            closeFunction: "LimpiarControles",
            clickClose: false,
            showClose: true,
            zIndex: 999999
        });

    }

    function LimpiarControles() {
        ctrComponente.Clear();
        ctrRemito.Clear();
    }

    function GrabarComponente() {

        var valueRemito = ctrRemito.get_SelectedValue();
        var valueComponente = ctrComponente.get_SelectedValue();
        var cantidad = $("#<%=txtCantidad.ClientID %>").val();

        ShowWaiting("Actualizando Remito...");

        $.modal.close();

        PageMethods.ActualizarRemito(valueRemito, valueComponente, cantidad, callBackFunction, function () { });
    }
</script>
<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;" class="main">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
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
                    <table width="95%" border="0" cellspacing="0">
                        <tr>
                            <td valign="top" align="center" style="width: 100%;">
                                <div style="position: relative; top: -25px;">
                                    <asp:Label Font-Names="Bookman Old Style" Font-Size="22pt" ForeColor="#5F5F5F" ID="Label1"
                                        runat="server">Gestión Composicion Remitos</asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <div class="ContentFiltro">
                                    <table width="100%" border="0" cellspacing="3" cellpadding="0">
                                        <tr>
                                            <td align="right" style="width: 40%">
                                                <asp:Label ID="Label2" runat="server" SkinID="lblBlack">Mostrar Remito:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <telerik:RadComboBox ID="cboRemitos" runat="server" Skin="Vista" Width="420px" OnClientSelectedIndexChanged="FiltrarDespachos"
                                                    MarkFirstMatch="true" AllowCustomText="true" AppendDataBoundItems="true">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Value="" Text="Todos" Selected="true" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td style="padding-top: 10px;">
                                <AjaxInfo:ClientControlGrid ID="gvComponentes" runat="server" AllowMultiSelection="false"
                                    BackColor="White" KeyName="IdComposicion" TypeSkin="Vista" PositionAdd="Top"
                                    AllowRowSelection="false" Height="100%" Width="99%" AllowPaging="false" AllowGroupRows="true"
                                    EmptyMessage="No existen Composiciones">
                                    <FunctionsColumns>
                                        <AjaxInfo:FunctionColumnRow Type="Delete" />
                                    </FunctionsColumns>
                                    <Columns>
                                        <AjaxInfo:Column HeaderName="Remito" DataFieldName="Remito" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Componente" DataFieldName="Componente" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Cantidad" DataFieldName="Cantidad" Align="Centrado" />
                                    </Columns>
                                </AjaxInfo:ClientControlGrid>
                            </td>
                        </tr>
                        <tr id="trAgregarPago">
                            <td align="left" valign="middle" style="padding-top: 15px;">
                                <button class="btnBasic_Marzzan" onclick="ShowNuevoComponente();return false;">
                                    <asp:LinkButton ID="btnInsert" runat="server">
                                     <img style="border:0px;vertical-align:middle;" width="18" height="18" alt="" src="Imagenes/AddFormula.png" /><span style="padding:5px">Componente</span>
                                    </asp:LinkButton>
                                </button>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <div id="divtbl">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <div class="modal" id="divNuevoComponente" style="height: 185px; width: 580px;">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%" id="tblPrincipal"
            runat="server">
            <tr>
                <td align="center" colspan="2">
                    <h3>SELECCIONE EL COMPONENTE DEL REMITO</h3>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" SkinID="lblBlack">Remito:</asp:Label>
                </td>
                <td>
                    <asp:TextBox Width="356px" ID="txtRemito" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" SkinID="lblBlack">Componente:</asp:Label>
                </td>
                <td>
                    <asp:TextBox Width="356px" ID="txtComponente" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" SkinID="lblBlack">Cantidad:</asp:Label>
                </td>
                <td>
                    <asp:TextBox Width="156px" ID="txtCantidad" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" style="padding-top:15px" colspan="2">
                    <button class="btnBasic_Marzzan" onclick="GrabarComponente();return false;">
                        <asp:LinkButton ID="LinkButton1" runat="server">
                                     <img style="border:0px;vertical-align:middle;" width="18" height="18" alt="" src="Imagenes/AddFormula.png" /><span style="padding:5px">Grabar</span>
                        </asp:LinkButton>
                    </button>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
