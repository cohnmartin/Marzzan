<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionOrdenesProduccion.aspx.cs"
    Inherits="GestionOrdenesProduccion" %>

<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="AjaxInfo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Gestion de Ordenes Produccion</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />

    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>

    <script src="Scripts/jquery.autocomplete.js" type="text/javascript"></script>
    
    <script src="Scripts/jquery.tablegroup.js" type="text/javascript"></script>

    <style type="text/css">
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
            border: 1px solid #8bbdde;
            margin-top: 0px;
            margin-right: 0px;
            padding: 0px;
            float: none;
            width: 175px;
            text-align: center;
            cursor: pointer;
            font-weight: bold;
        }
    </style>
</head>

<script type="text/javascript">
    var fileName = "";
    var idDespachoSelected = "";
    var ctrNombre;
    var ctrCodigo;
    var ctrSelected;
    jQuery(function() {
        options = {
            serviceUrl: 'ASHX/LoadFormulas.ashx',
            width: '384',
            minChars: 2,
            zIndex: 922000000,
            onSelect: SeleccionarCantidad
        };
        ctrNombre = $('#' + "<%= txtNombre.ClientID %>").autocomplete(options);


        options = {
            serviceUrl: 'ASHX/LoadFormulas.ashx',
            width: '384',
            minChars: 3,
            zIndex: 922000000,
            onSelect: SeleccionarCantidad
        };
        ctrCodigo = $('#' + "<%= txtNumero.ClientID %>").autocomplete(options);

    });

    function SeleccionarCantidad(text, value, sender) {
        if (sender[0].id == "txtNombre")
            ctrSelected = ctrNombre;
        else
            ctrSelected = ctrCodigo;

        $get("<%= txtCantidad.ClientID %>").focus();
        $get("<%= txtCantidad.ClientID %>").select();

    }

    function Agregar() {

        if (event.keyCode == "13") {
            AgregarFormula();
            return false;
        }
        return true;
    }

    function AgregarFormula() {
        var codigoProducto = ctrSelected.SelectedValue;
        var cantidad = $get("<%= txtCantidad.ClientID %>").value;

        PageMethods.AgregarFormula(codigoProducto, cantidad, onSuccess, onFailure);

    }


    function onSuccess(datos) {

        if (datos != null) {

            if (datos["Resultado"] != null) {

                $find("<%=gvFormulas.ClientID %>").set_ClientdataSource(datos["Resultado"]);
                $get("<%= txtCantidad.ClientID %>").value = 1;
                ctrCodigo.Clear();
                ctrNombre.Clear();
                $get("<%= txtNombre.ClientID %>").focus();
            }
            else
                alert(datos["Error"]);

        }
    }

    function onFailure() {
        alert("Error en la llamada al metodo");
    }

    function EliminarFormula(sender, CodigoFormula) {
        PageMethods.EliminarFormula(CodigoFormula, onSuccess, onFailure);
    }

    function CalcularOrder() {

        $find("<%=gvCalculos.ClientID %>").ShowWaiting("Realizando Calculo Consumo...");
        PageMethods.CalcularOrden(onSuccessCalcular, onFailure);


    }

    function onSuccessCalcular(datos) {

        if (datos != null) {

            if (datos["Detalle"] == "") {

                $find("<%=gvCalculos.ClientID %>").set_ClientdataSource(datos["Datos"]);
                $('#' + "tblDynamic_<%=gvCalculos.ClientID %>").tableGroup({ groupColumn: 1, groupClass: 'rgGroupItem', useNumChars: 0 });
            }
            else {
                alert(datos["Detalle"]);
            }
        }

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
                                        runat="server">Gestión Ordenes Producción</asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <div class="ContentFiltro">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="center" style="border-style: solid none solid solid; border-width: 1px;
                                                border-color: #8bbdde; width: 80px; background-color: #E2E2E2;">
                                                <asp:Label ID="Label2" runat="server" SkinID="lblBlack">Nombre:</asp:Label>
                                            </td>
                                            <td align="left" style="background-color: #E2E2E2; border-style: solid none solid none;
                                                border-width: 1px; border-color: #8bbdde; width: 280px">
                                                <asp:TextBox Width="98%" ID="txtNombre" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: #E2E2E2; border-style: solid none solid none;
                                                border-width: 1px; border-color: #8bbdde; width: 80px">
                                                <asp:Label ID="Label3" runat="server" SkinID="lblBlack">Codigo:</asp:Label>
                                            </td>
                                            <td align="left" style="background-color: #E2E2E2; padding-right: 5px; border-style: solid solid solid none;
                                                border-width: 1px; border-color: #8bbdde; width: 280px">
                                                <asp:TextBox Width="98%" ID="txtNumero" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="center" style="padding-left: 5px; width: 80px">
                                                <asp:Label ID="Label4" runat="server" SkinID="lblBlack">Cantidad:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox onkeyDown="return Agregar();" Width="99%" Text="1" ID="txtCantidad"
                                                    runat="server"></asp:TextBox>
                                            </td>
                                            <td align="right" valign="bottom" style="width: 80px">
                                                <img alt="Agregar Formula" src="Imagenes/AddFormula.png" width="32px" height="32px"
                                                    onclick="AgregarFormula();return false;" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 10px">
                                <AjaxInfo:ClientControlGrid ID="gvFormulas" runat="server" AllowMultiSelection="false"
                                    KeyName="CodigoFormula" TypeSkin="Vista" PositionAdd="Top" AllowRowSelection="true"
                                    Height="100%" Width="99%" AllowPaging="true" PageSize="25" EmptyMessage="No existen formulas ingresadas">
                                    <FunctionsColumns>
                                        <AjaxInfo:FunctionColumnRow Type="Delete" Text="Eliminar guía completa" ClickFunction="EliminarFormula" />
                                    </FunctionsColumns>
                                    <Columns>
                                        <AjaxInfo:Column HeaderName="Nombre Formula" DataFieldName="NombreFormula" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Codigo Formula" DataFieldName="CodigoFormula" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Cantidad" DataFieldName="cantidad" Align="Derecha" />
                                    </Columns>
                                </AjaxInfo:ClientControlGrid>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 10px" align="center">
                                <div class="DivGeneral" onclick="CalcularOrder(); return false;">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="right" style="width: 30px; padding-right: 5px">
                                                <asp:ImageButton ID="ImageButton1" Width="24" Height="24" runat="server" ImageUrl="~/Imagenes/calcular.png" />
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="Label5" runat="server" SkinID="lblBlack">Calcular Consumo</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 10px;" align="center" >
                                <div >
                                    <AjaxInfo:ClientControlGrid ID="gvCalculos" runat="server" AllowMultiSelection="false"
                                        KeyName="CodigoComponente" TypeSkin="Vista" PositionAdd="Top" AllowRowSelection="false"
                                        BackColor="White"
                                        Height="100%" Width="70%" AllowPaging="false" PageSize="25" EmptyMessage="SOLO SE MOSTRARAN 100 RESULTADOS, SE EXPORTARA LA TOTALIDAD">
                                        <FunctionsGral>
                                        <AjaxInfo:FunctionGral Type="Excel" Text="Exportar Datos" />
                                    </FunctionsGral>
                                        <Columns>
                                            <AjaxInfo:Column ExportToExcel="true" HeaderName="Comonente" DataFieldName="DescripcionComonente" Align="Derecha" />
                                            <AjaxInfo:Column ExportToExcel="true" HeaderName="Cantidad Utilizar" DataFieldName="Cantidad" Align="Centrado" />
                                            <AjaxInfo:Column ExportToExcel="true" HeaderName="Disponible" DataFieldName="Disponible" Align="Centrado" />
                                            <AjaxInfo:Column ExportToExcel="true" HeaderName="Desposito" DataFieldName="Desposito" Align="Centrado" />
                                        </Columns>
                                    </AjaxInfo:ClientControlGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50">
        <ProgressTemplate>
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
                                Text="Buscado Mensajes...">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</body>
</html>
