<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="ConsultaSolicitudesAlta.aspx.cs"
    Inherits="ConsultaSolicitudesAlta" %>

<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="AjaxInfo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Consulta Solicitudes Alta</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <link href="UploadFiles/CSS/uploadify.css" rel="stylesheet" type="text/css" />
 <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>

    <script src="Scripts/jquery.autocomplete.js" type="text/javascript"></script>

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
    var fileName = "";
    var idDespachoSelected = "";

    function FiltrarAltas(esCambioPagina) {

        var cliente = $get("<%= txtNombre.ClientID %>").value;
        var dni = $get("<%= txtDNI.ClientID %>").value;
        var grupo = $find("<%= cboGrupos.ClientID %>").get_value();
        var fechaInicial = "";
        var fechaFinal = "";

        if ($find("<%= txtFechaInicial.ClientID %>").get_selectedDate() != null)
            fechaInicial = $find("<%= txtFechaInicial.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

        if ($find("<%= txtFechaFinal.ClientID %>").get_selectedDate() != null)
            fechaFinal = $find("<%= txtFechaFinal.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

        if (!esCambioPagina) {
            var skip = 0;
            var take = $find("gvAltas").get_pageSize();
            $find("<%=gvAltas.ClientID %>").ShowWaiting("Buscando Altas...");
            PageMethods.FiltroAltas(fechaInicial, fechaFinal, cliente, dni, grupo, skip, take, esCambioPagina, onSuccessFiler, onFailure);
        }
        else {
            var skip = $find("<%=gvAltas.ClientID %>").get_pageSize() * $find("<%=gvAltas.ClientID %>").get_currentPageIndex();
            var take = $find("gvAltas").get_pageSize();
            $find("<%=gvAltas.ClientID %>").ShowWaiting("Cambiando de Página...");
            PageMethods.FiltroAltas(fechaInicial, fechaFinal, cliente, dni, grupo, skip, take, esCambioPagina, onSuccessChangePage, onFailure);
        }
    }


    function onSuccessFiler(datos) {
        if (datos != null) {
            $find("<%=gvAltas.ClientID %>").ChangeClientVirtualCount(datos["Cantidad"]);
            $find("<%=gvAltas.ClientID %>").set_ClientdataSource(datos["Altas"]);
        }
    }

    function onSuccessChangePage(datos) {
        if (datos != null) {
            $find("gvAltas").set_ClientdataSource(datos["Altas"]);
        }
    }

    function onFailure(err) {
        alert("Error al invocar el web metodo:" + err.get_message());
    }

    function DescargarGuia(sender, id) {
        var item = $find("<%=gvAltas.ClientID %>").get_ItemDataByKey(id);

        window.open("ArchivosDespacho/" + item.Archivo);
    }

    function CambioPagina(sender, page) {
        FiltrarAltas(true);
    }

    function BajaSolicitud() {
        var items = $find("<%=gvAltas.ClientID %>").get_ItemsDataSelected();

        if (items.length == 0) {
            radalert("Debe seleccionar al menos una solicitud para realizar la baja", 300, 100, "Selección");
        }
        else {
            var item = items[0];
            if (item.Activo == "SI") {

                if (item.Estado == "Solicitud Exitosa") {
                    blockConfirmCallBackFn("Esta seguro de dar de baja el cliente de la solicitud?", event, 300, 100, null, "Baja Revendedor", ConfirmacionBaja);
                }
                else {
                    radalert("No se puede dar de baja una solicitud que no es exitosa.", 300, 100, "Baja Revendedor");
                }
            }
            else {
                radalert("No se puede dar de baja un revendedor que no esta activo.", 300, 100, "Baja Revendedor");
            }

        }

        function ConfirmacionBaja(result) {
            if (result) {

                var skip = $find("<%=gvAltas.ClientID %>").get_pageSize() * $find("<%=gvAltas.ClientID %>").get_currentPageIndex();
                var take = $find("gvAltas").get_pageSize();
                $find("<%=gvAltas.ClientID %>").ShowWaiting("Cambiando Estado...");

                PageMethods.BajaCliente(item["IdSolicitudAlta"], skip, take, onSuccessFiler, onFailure);
            }
        }
    }

    function CambiarEstado() {
        var items = $find("<%=gvAltas.ClientID %>").get_ItemsDataSelected();

        if (items.length == 0) {
            radalert("Debe seleccionar al menos una solicitud para realizar el cambio", 300, 100, "Selección");
        }
        else {
            var item = items[0];
            if (item.TipoAlta == "Potencial") {
                if (item.Estado == "Solicitud Exitosa") {
                    blockConfirmCallBackFn("Esta seguro de cambiar el tipo de la solicitud a Revendedor?", event, 300, 100, null, "Cambio Estado", ConfirmacionCambio);
                }
                else {
                    radalert("No se puede cambiar el tipo de alta a Revendedor en una solicitud que no es exitosa.", 300, 100, "Cambio");
                }
            }
            else {
                radalert("No se puede cambiar el tipo de alta Revendedor, solo se puede cambiar de Potencial a Revendedor", 300, 100, "Cambio");
            }
        }

        function ConfirmacionCambio(result) {
            if (result) {

                var skip = $find("<%=gvAltas.ClientID %>").get_pageSize() * $find("<%=gvAltas.ClientID %>").get_currentPageIndex();
                var take = $find("gvAltas").get_pageSize();
                $find("<%=gvAltas.ClientID %>").ShowWaiting("Cambiando Estado...");

                PageMethods.CambiarEstado(item["IdSolicitudAlta"], skip, take, onSuccessFiler, onFailure);
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
                                        runat="server">Consulta Solicitudes Altas</asp:Label>
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
                                            <td align="right" valign="bottom" rowspan="3" style="width: 60px">
                                                <img alt="Filtrar Mensajes" src="Imagenes/Search.png" onclick="FiltrarAltas(false);return false;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label7" runat="server" SkinID="lblBlack">Grupos:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <telerik:RadComboBox ID="cboGrupos" runat="server" Skin="Vista" Width="200px">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Value="" Text="Todos los Grupos" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td align="left" style="padding-left: 5px">
                                                <asp:Label ID="Label4" runat="server" SkinID="lblBlack">DNI:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox Width="60%" ID="txtDNI" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label5" runat="server" SkinID="lblBlack">Apellido y Nombre:</asp:Label>
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox Width="88%" ID="txtNombre" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="padding-top: 10px">
                                <AjaxInfo:ClientControlGrid ID="gvAltas" runat="server" AllowMultiSelection="false"
                                    KeyName="IdSolicitudAlta" TypeSkin="Vista" PositionAdd="Top" AllowRowSelection="true"
                                    Height="100%" Width="99%" AllowPaging="true" PageSize="15" EmptyMessage="No existen solicitudes"
                                    onClientChangePageIndex="CambioPagina">
                                    <FunctionsGral>
                                        <AjaxInfo:FunctionGral Text="Exportar Altar" Type="Excel" />
                                        
                                        <AjaxInfo:FunctionGral Type="Custom" ClickFunction="BajaSolicitud" ImgUrl="imagenes/cancelar.gif"
                                            Text="Dar de Baja" />
                                    </FunctionsGral>
                                    <Columns>
                                        <AjaxInfo:Column ExportToExcel="true" Capitalice="true" HeaderName="Apellido y Nombre"
                                            DataFieldName="Nombre" Align="Derecha" />
                                        <AjaxInfo:Column ExportToExcel="true" HeaderName="DNI" DataFieldName="DNI" Align="Derecha" />
                                        <AjaxInfo:Column ExportToExcel="true" HeaderName="Grupo" DataFieldName="GrupoAlta"
                                            Align="Derecha" />
                                        <AjaxInfo:Column ExportToExcel="true" HeaderName="Provincia" DataFieldName="Provincia"
                                            Align="Derecha" />
                                        <AjaxInfo:Column ExportToExcel="true" HeaderName="Localidad" DataFieldName="Localidad"
                                            Align="Derecha" />
                                        <AjaxInfo:Column ExportToExcel="true" HeaderName="Tipo Alta" DataFieldName="TipoAlta"
                                            Align="Derecha" />
                                        <AjaxInfo:Column ExportToExcel="true" HeaderName="Fecha Solicitud" DataFieldName="FechaSolicitud"
                                            Align="Centrado" />
                                        <AjaxInfo:Column ExportToExcel="true" HeaderName="Estado" DataFieldName="Estado"
                                            Align="Derecha" />
                                        <AjaxInfo:Column ExportToExcel="true" HeaderName="Activo" DataFieldName="Activo"
                                            Align="Centrado" />
                                    </Columns>
                                </AjaxInfo:ClientControlGrid>
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
                                Text="Buscado Despachos...">
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
