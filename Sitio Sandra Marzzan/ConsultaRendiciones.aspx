<%@ Page Language="C#" Theme="SkinMarzzan" EnableEventValidation="false" AutoEventWireup="true"
    CodeFile="ConsultaRendiciones.aspx.cs" Inherits="ConsultaRendiciones" %>

<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="AjaxInfo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consulta de Rendiciones</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <link href="UploadFiles/CSS/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/Jquery-UI/css/start/jquery-ui-1.10.3.custom.min.css" rel="stylesheet"
        type="text/css" />
    <script src="Scripts/Jquery-UI/js/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/Jquery-UI/js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.autocomplete.js" type="text/javascript"></script>
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
    function FiltrarDespachos() {

        var nroRendicion = $get("<%= txtNroRendicion.ClientID %>").value;
        var transporte = $find("<%= cboTransportes.ClientID %>").get_text();
        var fechaInicial = "";
        var fechaFinal = "";

        if ($find("<%= txtFechaInicial.ClientID %>").get_selectedDate() != null)
            fechaInicial = $find("<%= txtFechaInicial.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

        if ($find("<%= txtFechaFinal.ClientID %>").get_selectedDate() != null)
            fechaFinal = $find("<%= txtFechaFinal.ClientID %>").get_selectedDate().format("dd/MM/yyyy");



        var take = $find("gvRendicion").get_pageSize();
        $find("<%=gvRendicion.ClientID %>").ShowWaiting("Buscando Rendiciones...");
        PageMethods.FiltroRendicion(nroRendicion, fechaInicial, fechaFinal, transporte, 0, take, onSuccessFiler, onFailure);
    }


    function DescargarRendicion(sender, id) {
        __doPostBack('btnExportar', id);
    }


    function EditarRendicion(sender, id) {
        var itemDelete = $find("<%=gvRendicion.ClientID %>").get_ItemDataByKey(id);
        
        var tipoRendicionTemp = itemDelete.TipoRendicion.substr(0, 2);

        if (tipoRendicionTemp != "EN")
            var tipoRendicion = itemDelete.TipoRendicion.substr(0, 1);
        else
            var tipoRendicion = "L";

        window.open("GestionRendicionTransportistas.aspx?IdRendicion=" + id + "&Tipo=" + tipoRendicion + "&DescRendicion=" + itemDelete.TipoRendicion, "_self");

    }

    function EliminarRendicion(sender, id) {

        var itemDelete = $find("<%=gvRendicion.ClientID %>").get_ItemDataByKey(id);

        if (itemDelete.Estado == "PRESENTADA") {
            blockConfirmCallBackFn('Esta seguro de eliminar la rendición nro:' + id + '?', event, 330, 100, null, 'Eliminación', ConfirEliminacion);

            function ConfirEliminacion(result) {
                if (result) {
                    PageMethods.EliminarRendicion(id, onSuccessFiler, onFailure);
                }
            }
        }
        else {
            radalert('La rendición no puede ser eliminada, solo se puede eliminar una rendíción en estado PRESETADA', 330, 100, 'Eliminación');
        }

    }

    function onSuccessFiler(datos) {
        if (datos != null) {
            $find("<%=gvRendicion.ClientID %>").ChangeClientVirtualCount(datos["Cantidad"]);
            $find("<%=gvRendicion.ClientID %>").set_ClientdataSource(datos["Rendiciones"]);
        }
    }

    function onFailure(err) {
        alert(err._message);
    }

    function NuevaRendicion(tipo, descripcion) {
        window.open("GestionRendicionTransportistas.aspx?Tipo=" + tipo + "&DescRendicion=" + descripcion, "_self");
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
                                    <asp:Label Font-Names="Bookman Old Style" Font-Size="22pt" ForeColor="#5F5F5F" ID="lbltituloprinciapal"
                                        runat="server">Consulta de Rendiciones</asp:Label>
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
                                            <td align="right" valign="bottom" rowspan="2" style="width: 60px">
                                                <img alt="Filtrar Mensajes" src="Imagenes/Search.png" onclick="FiltrarDespachos();return false;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label7" runat="server" SkinID="lblBlack">Nº Rendición:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox Width="99%" ID="txtNroRendicion" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="left" style="padding-left: 5px">
                                                <asp:Label ID="Label6" runat="server" SkinID="lblBlack">Transporte:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <telerik:RadComboBox ID="cboTransportes" runat="server" Skin="Vista" Width="200px">
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
                            <td align="left" style="padding-top: 10px">
                                <AjaxInfo:ClientControlGrid ID="gvRendicion" runat="server" AllowMultiSelection="false" ShowDataOnInit="true"
                                    KeyName="IdCabeceraRendicion" TypeSkin="Vista" PositionAdd="Top" AllowRowSelection="false"
                                    Height="100%" Width="99%" AllowPaging="true" PageSize="25" EmptyMessage="No existen rendiciones">
                                    <FunctionsColumns>
                                        <AjaxInfo:FunctionColumnRow Type="Custom" ImgUrl="imagenes/download.png" Text="Descargar Detalle"
                                            ClickFunction="DescargarRendicion" />
                                        <AjaxInfo:FunctionColumnRow Type="Delete" Text="Eliminar Rendicion" ClickFunction="EliminarRendicion" />
                                        <AjaxInfo:FunctionColumnRow Type="Edit" Text="Edicion Rendicion" ClickFunction="EditarRendicion" />
                                    </FunctionsColumns>
                                    <FunctionsGral>
                                        <AjaxInfo:FunctionGral Type="Add" Text="Rendición Tipo:" />
                                        <AjaxInfo:FunctionGral Type="Custom" Text="[Valores]" ClickFunction="NuevaRendicion('V' , 'VALORES')"
                                            ImgUrl="imagenes/spacer.gif" />
                                        <AjaxInfo:FunctionGral Type="Custom" Text="[Devoluciones]" ClickFunction="NuevaRendicion('D','DEVUELTAS')"
                                            ImgUrl="imagenes/spacer.gif" />
                                        <AjaxInfo:FunctionGral Type="Custom" Text="[Extravios]" ClickFunction="NuevaRendicion('E','EXTRAVIADAS')"
                                            ImgUrl="imagenes/spacer.gif" />
                                        <AjaxInfo:FunctionGral Type="Custom" Text="[Entrega Lider]" ClickFunction="NuevaRendicion('L','ENTREGA LIDER')"
                                            ImgUrl="imagenes/spacer.gif" />
                                        <AjaxInfo:FunctionGral Type="Custom" Text="[Siniestros]" ClickFunction="NuevaRendicion('S','SINIESTRADAS')"
                                            ImgUrl="imagenes/spacer.gif" />
                                    </FunctionsGral>
                                    <Columns>
                                        <AjaxInfo:Column HeaderName="Nro Rendición" DataFieldName="IdCabeceraRendicion" Align="Centrado" />
                                        <AjaxInfo:Column HeaderName="Fecha Rendición" DataFieldName="FechaRendicion" Align="Centrado" />
                                        <AjaxInfo:Column HeaderName="Fecha Aprobación" DataFieldName="FechaAprobacion" Align="Centrado" />
                                        <AjaxInfo:Column HeaderName="Estado" DataFieldName="Estado" Align="Centrado" />
                                        <AjaxInfo:Column HeaderName="Monto" DataFieldName="Monto" Align="Centrado" />
                                        <AjaxInfo:Column HeaderName="Tipo Rendición" DataFieldName="TipoRendicion" Align="Centrado" />
                                        <AjaxInfo:Column HeaderName="Transporte" DataFieldName="Transporte" Align="Centrado" />
                                    </Columns>
                                </AjaxInfo:ClientControlGrid>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <%-- boton oculto para poder dispara la exportación a excel--%>
    <asp:Button ID="btnExportar" CausesValidation="false" runat="server" Visible="false"
        OnClick="btnExportar_click" />
    <%--  --------------%>
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
