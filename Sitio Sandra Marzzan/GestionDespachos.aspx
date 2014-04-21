<%@ Page Language="C#" ValidateRequest="false" Theme="SkinMarzzan" AutoEventWireup="true"
    CodeFile="GestionDespachos.aspx.cs" Inherits="GestionDespachos" %>

<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="AjaxInfo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestion de Despachos</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <link href="UploadFiles/CSS/uploadify.css" rel="stylesheet" type="text/css" />

    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>

    <script src="Scripts/jquery.autocomplete.js" type="text/javascript"></script>

    <script src="Scripts/jquery.uploadify.js" type="text/javascript"></script>

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

    function FiltrarDespachos() {

        var nroDespacho = $get("<%= txtNroDespacho.ClientID %>").value;
        var cliente = $get("<%= txtCliente.ClientID %>").value;
        var nroGuia = $get("<%= txtNroGuia.ClientID %>").value;
        var transporte = $get("<%= txtTransporte.ClientID %>").value;
        var fechaInicial = "";
        var fechaFinal = "";

        if ($find("<%= txtFechaInicial.ClientID %>").get_selectedDate() != null)
            fechaInicial = $find("<%= txtFechaInicial.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

        if ($find("<%= txtFechaFinal.ClientID %>").get_selectedDate() != null)
            fechaFinal = $find("<%= txtFechaFinal.ClientID %>").get_selectedDate().format("dd/MM/yyyy");



        var take = $find("gvDespachos").get_pageSize();
        $find("<%=gvDespachos.ClientID %>").ShowWaiting("Buscando Despachos...");
        PageMethods.FiltroDespachos(nroDespacho, fechaInicial, fechaFinal, cliente, nroGuia, transporte, 0, take, onSuccessFiler, onFailure);
    }


    function onSuccessFiler(datos) {
        if (datos != null) {
            $find("<%=gvDespachos.ClientID %>").ChangeClientVirtualCount(datos["Cantidad"]);
            $find("<%=gvDespachos.ClientID %>").set_ClientdataSource(datos["Guias"]);
        }
    }


    $(window).load(
            function() {
                $("#<%=FileUpload2.ClientID %>").fileUpload({
                    'uploader': 'UploadFiles/scripts/uploader.swf',
                    'cancelImg': 'UploadFiles/images/cancel.png',
                    'buttonImg': 'Imagenes/adjuntar.png',
                    'wmode': 'transparent',
                    'buttonText': 'Adjuntar',
                    'script': 'UploadFiles/Upload.ashx',
                    'folder': 'ArchivosDespacho',
                    'fileDesc': 'Archivos Excel (.xls;)',
                    'fileExt': '*.xls;*.xlsx',
                    'multi': false,
                    'width': '45',
                    'auto': true,
                    'onComplete': TerminoUpload
                });
            }
        );

    function TerminoUpload(sender, arg, infoArchivo, DatosArchivo, aa) {

        if (DatosArchivo.split('|').length > 1) {
            fileName = infoArchivo.name;
            $("#<%=lblNroDespacho.ClientID %>").text("Nro Despacho: " + DatosArchivo.split('|')[0]);
            $("#<%=lblFechaEnvio.ClientID %>").text("Fecha Envio: " + DatosArchivo.split('|')[1]);
            $("#<%=lblTransporte.ClientID %>").text("Transporte: " + DatosArchivo.split('|')[2]);
            $("#<%=lblTotalGuias.ClientID %>").text("Total de Guías:" + DatosArchivo.split('|')[3]);
        }
        else {
            fileName = "";
            alert(DatosArchivo);
        }
    }

    function AltaArchivoDespacho() {
        if (fileName != '') {

            $find("<%=srvNuevoDespacho.ClientID %>").ShowWaiting(null, "Agregando Archivo...");
            PageMethods.AgregarArchivoDespacho(fileName, onSuccess, onFailure);
        }
        else
            alert("Debe seleccionar un archivo para que se pueda procesar.");

    }

    function onSuccessR(ruta) {
        alert(ruta);
    }

    function EliminarGuia(sender, id) {
        blockConfirmCallBackFn('Esta seguro de eliminar la guis de Despacho?', event, 330, 100, null, 'Eliminación', ConfirEliminacion);

        function ConfirEliminacion(result) {
            if (result)
                PageMethods.EliminarDespacho(id, onSuccess, onFailure);
        }
    }

    function MostrarGuias(sender, id) {
        $get("<%=txtConsultorBusquedaDespacho.ClientID %>").value = "";
        var itemCabecera = $find("<%=gvDespachos.ClientID %>").get_ItemDataByKey(id);
        $("#<%=lblNroDespachoDet .ClientID %>").text(itemCabecera["NroDespacho"]);

        $find("<%=srvDetalleGuias.ClientID %>").set_CollectionDiv('divPrincipalGuias');
        $find("<%=srvDetalleGuias.ClientID %>").ShowWindows('divPrincipalGuias', "Detalle del Despacho");

        idDespachoSelected = id;

        PageMethods.ObtenerGuias(id, onSuccessGuias, onFailure);

    }

    function onSuccess(datos) {
        if (datos != null) {
            $find("<%=gvDespachos.ClientID %>").set_ClientdataSource(datos);
        }

        $find("<%=srvNuevoDespacho.ClientID %>").CloseWindows();
    }

    function onSuccessGuias(datos) {
        if (datos != null) {
            $find("<%=gvGuias.ClientID %>").set_ClientdataSource(datos["Guias"]);
        }

    }

    function onFailure(error) {
        alert("Error al Importar Datos:" + error.get_message());
    }

    function NuevoDespacho() {

        $find("<%=srvNuevoDespacho.ClientID %>").set_CollectionDiv('divPrincipalDespacho');
        $find("<%=srvNuevoDespacho.ClientID %>").ShowWindows('divPrincipalDespacho', "Por favor seleccione el archivo que desea procesar");
    }

    function BuscarGuiaPorConsultor() {
        if (event.keyCode == 13) {
            $find("<%=gvGuias.ClientID %>").ShowWaiting("Buscando Guias...");
            var consultor = $get("<%=txtConsultorBusquedaDespacho.ClientID %>").value;
            PageMethods.BuscarGuias(idDespachoSelected, consultor, onSuccessGuias, onFailure);
            event.preventDefault()
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
                                        runat="server">Gestión de Despachos</asp:Label>
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
                                                <img alt="Filtrar Mensajes" src="Imagenes/Search.png" onclick="FiltrarDespachos();return false;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label7" runat="server" SkinID="lblBlack">Nº Despacho:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox Width="99%" ID="txtNroDespacho" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="left" style="padding-left: 5px">
                                                <asp:Label ID="Label4" runat="server" SkinID="lblBlack">Cliente:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox Width="99%" ID="txtCliente" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label5" runat="server" SkinID="lblBlack">Nº Guia:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox Width="99%" ID="txtNroGuia" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="left" style="padding-left: 5px">
                                                <asp:Label ID="Label6" runat="server" SkinID="lblBlack">Transporte:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox Width="99%" ID="txtTransporte" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 10px;">
                                <AjaxInfo:ClientControlGrid ID="gvDespachos" runat="server" AllowMultiSelection="false"
                                    KeyName="IdCabeceraGuia" TypeSkin="Vista" PositionAdd="Top" AllowRowSelection="true"
                                    Height="100%" Width="99%" AllowPaging="true" PageSize="25" EmptyMessage="No existen despachos">
                                    <FunctionsGral>
                                        <AjaxInfo:FunctionGral Text="Nuevo Despacho" Type="Add" ClickFunction="NuevoDespacho" />
                                    </FunctionsGral>
                                    <FunctionsColumns>
                                        <AjaxInfo:FunctionColumnRow Type="Delete" Text="Eliminar guía completa" ClickFunction="EliminarGuia" />
                                        <AjaxInfo:FunctionColumnRow Type="Custom" ImgUrl="imagenes/Find.gif" Text="Ver guías componentes"
                                            ClickFunction="MostrarGuias" />
                                    </FunctionsColumns>
                                    <Columns>
                                        <AjaxInfo:Column HeaderName="Nro Despacho" DataFieldName="NroDespacho" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Fecha Envio" DataFieldName="FechaEnvio" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Fecha Generacion" DataFieldName="FechaGeneracion" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Transporte" DataFieldName="Transporte" Align="Derecha" />
                                    </Columns>
                                </AjaxInfo:ClientControlGrid>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <AjaxInfo:ServerControlWindow ID="srvDetalleGuias" runat="server" BackColor="WhiteSmoke"
        WindowColor="Gray" ForeColor="GrayText">
        <ContentControls>
            <div id="divPrincipalGuias" style="height: 440px; width: 1000px;">
                <table width="100%" border="0" cellspacing="1">
                    <tr>
                        <td>
                            <asp:Label ID="labelaass" runat="server" Font-Size="18px">GUIAS CONTENIDAS EN DESPACHO NRO:</asp:Label>
                            <asp:Label ID="lblNroDespachoDet" runat="server" Font-Size="18px" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="right" style="padding-right: 10px">
                            <asp:Label ID="Label9" runat="server" Font-Size="12px" Font-Bold="true">Buscar por consultor:</asp:Label>
                            <asp:TextBox Width="250px" ID="txtConsultorBusquedaDespacho" onkeypress="BuscarGuiaPorConsultor();"
                                runat="server" ToolTip="Presione enter una vez ingresado el criterio para buscar"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div style="height: 400px; overflow: auto">
                                <AjaxInfo:ClientControlGrid ID="gvGuias" runat="server" AllowMultiSelection="false"
                                    KeyName="IdDetalleGuia" TypeSkin="Vista" PositionAdd="Top" AllowRowSelection="true"
                                    Height="90%" Width="99%" AllowPaging="true" PageSize="25" EmptyMessage="No existen despachos">
                                    <Columns>
                                        <AjaxInfo:Column HeaderName="Codigo" DataFieldName="Codigo" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Consultor" DataFieldName="Consultor" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Tipo" DataFieldName="CCRR" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Direccion" DataFieldName="Direccion" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Provincia" DataFieldName="Provincia" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Localidad" DataFieldName="Localidad" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Total Pedido" DataType="Decimal" DataFieldName="TotalPedido"
                                            Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Total Pagar" DataType="Decimal" DataFieldName="TotalPagar"
                                            Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Comprobante" DataFieldName="Comprobante" Align="Derecha" />
                                    </Columns>
                                </AjaxInfo:ClientControlGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentControls>
    </AjaxInfo:ServerControlWindow>
    <AjaxInfo:ServerControlWindow ID="srvNuevoDespacho" runat="server" BackColor="WhiteSmoke"
        WindowColor="Gray" ForeColor="#006699">
        <ContentControls>
            <div id="divPrincipalDespacho" style="height: 120px; width: 440px;">
                <table width="95%" border="0" cellspacing="0">
                    <tr>
                        <td style="padding-top: 2px">
                            <table width="95%" border="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNroDespacho" runat="server" SkinID="lblBlack">Nro Despacho: -</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTransporte" runat="server" SkinID="lblBlack">Transporte: -</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFechaEnvio" runat="server" SkinID="lblBlack">Fecha Encvío: -</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTotalGuias" runat="server" SkinID="lblBlack">Total Guías: -</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 210px; padding-top: 10px">
                            <div style="padding: 5px; display: inline;">
                                <asp:FileUpload ID="FileUpload2" runat="server" Style="display: none" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="padding-top: 5px">
                            <div class="DivGeneral" onclick="AltaArchivoDespacho(); return false;">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="right" style="width: 30px; padding-right: 5px">
                                            <asp:ImageButton ID="ImageButton1" Width="24" Height="24" runat="server" ImageUrl="~/Imagenes/EnviarMail.png" />
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label8" runat="server" SkinID="lblBlack">Agregar Archivo</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentControls>
    </AjaxInfo:ServerControlWindow>
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
