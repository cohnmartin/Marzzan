<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="ConsultaPedidosInterno.aspx.cs"
    Inherits="ConsultaPedidosInterno" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="AjaxInfo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Consulta Ventas Realizadas</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />

    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>

    <script src="Scripts/jquery.autocomplete.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            width: 95%;
        }
        .style2
        {
            width: 101px;
        }
        .style3
        {
            height: 8px;
        }
        .style4
        {
            height: 8px;
        }
        .style5
        {
            height: 8px;
            width: 233px;
        }
    </style>
</head>
<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Web20" VisibleTitlebar="true"
        Title="Atención">
        <Windows>
            <telerik:RadWindow ID="RadWindow2" runat="server" Behaviors="Close" Width="870" Title="Comprobante Pedido"
                Height="600" Modal="true" Overlay="false" NavigateUrl="ReportViewer.aspx" VisibleTitlebar="true"
                VisibleStatusbar="false" ShowContentDuringLoad="false" Skin="WebBlue">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="Header_panelContainerSimple">
                    <div class="CabeceraInicial">
                        <asp:Button ID="btnVolver" runat="server" Text="" SkinID="btnVolver" OnClick="btnVolver_Click"
                            Visible="false" />
                    </div>
                </div>
                <div class="CabeceraContent">
                    <table width="95%" border="0" cellspacing="0">
                        <tr>
                            <td colspan="4" valign="top" align="center" style="width: 100%;">
                                <div style="position: relative; top: -25px">
                                    <asp:Label Font-Names="Bookman Old Style" Font-Size="22pt" ForeColor="#5F5F5F" ID="Label4"
                                        runat="server">Consulta de Ventas Realizadas</asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">
                                <asp:Label SkinID="lblBlue" ID="Label2" runat="server">Grupo:</asp:Label>
                            </td>
                            <td align="left" style="width: 350px;">
                                <telerik:RadComboBox ID="cboGrupoFiltro" runat="server" Width="410px" EmptyMessage="Todos los Grupos"
                                    Skin="WebBlue" CausesValidation="false">
                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                </telerik:RadComboBox>
                            </td>
                            <td style="width: 100px;">
                                <asp:Label SkinID="lblBlue" ID="UserNameLabel" runat="server">Revendedor:</asp:Label>
                            </td>
                            <td align="left" style="width: 350px;">
                                <asp:TextBox Width="325px" ID="txtCliente" runat="server" EnableViewState="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label SkinID="lblBlue" ID="Label1" runat="server">Transporte:</asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox Width="325px" ID="txtTransporte" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label SkinID="lblBlue" ID="Label3" runat="server">Nro. Pedido:</asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox Width="125px" ID="txtNroPedido" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label SkinID="lblBlue" ID="UserNameLabel0" runat="server">Fecha Incial:</asp:Label>
                            </td>
                            <td align="left">
                                <telerik:RadDatePicker ID="txtFechaInicial" runat="server" Skin="Web20" Culture="Spanish (Argentina)">
                                    <DateInput ID="DateInput1" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Web20"
                                        Skin="Web20" SelectionOnFocus="SelectAll" ShowButton="True">
                                    </DateInput>
                                    <Calendar Skin="Web20">
                                    </Calendar>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <asp:Label SkinID="lblBlue" ID="UserNameLabel1" runat="server">Fecha Final:</asp:Label>
                            </td>
                            <td align="left">
                                <telerik:RadDatePicker ID="txtFechaFinal" runat="server" Skin="Web20" Culture="Spanish (Argentina)">
                                    <DateInput ID="DateInput2" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Web20"
                                        Skin="Web20" SelectionOnFocus="SelectAll" ShowButton="True">
                                    </DateInput>
                                    <Calendar Skin="Web20">
                                    </Calendar>
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="4">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" SkinID="btnBasic" Width="77px"
                                            OnClientClick="ConsultarDatos(); return false;" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="color: #993300; font-family: Sans-Serif; font-size: 11px" colspan="4">
                                <AjaxInfo:ClientControlGrid ID="gridPre" runat="server" AllowMultiSelection="false"
                                    TypeSkin="Vista" PositionAdd="Top" AllowRowSelection="true" Height="100%"
                                    Width="100%" KeyName="ID" AllowPaging="false" PageSize="50" EmptyMessage="Debe consultar para ver los datos">
                                    <FunctionsGral>
                                        <AjaxInfo:FunctionGral Type="Excel" Text="Exportar Datos" />
                                    </FunctionsGral>
                                    <Columns>
                                        <AjaxInfo:Column ExportToExcel="true" HeaderName="Grupo" DataFieldName="Grupo" Align="Derecha" />
                                        <AjaxInfo:Column ExportToExcel="true" HeaderName="Nro Revendedor" DataFieldName="CodCliente" Align="Derecha" />
                                        <AjaxInfo:Column ExportToExcel="true" HeaderName="Razon Social" DataFieldName="RazonSocial" Align="Derecha" />
                                        <AjaxInfo:Column ExportToExcel="true" HeaderName="Transporte" DataFieldName="Transporte" Align="Derecha" />
                                        <AjaxInfo:Column ExportToExcel="true" HeaderName="FEmision" DataFieldName="FEmision" Align="Derecha" />
                                        <AjaxInfo:Column ExportToExcel="true" HeaderName="TipoComp" DataFieldName="TipoComp" Align="Derecha" />
                                        <AjaxInfo:Column ExportToExcel="true" HeaderName="Nro" DataFieldName="Nro" Align="Derecha" />
                                        <AjaxInfo:Column ExportToExcel="true" HeaderName="Importe" DataFieldName="Importe" Width="100px" Align="Centrado"
                                            Totalizar="true" ToFixed="true" />
                                    </Columns>
                                </AjaxInfo:ClientControlGrid>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" ClientEvents-OnRequestStart="requestStart1">
        <ClientEvents OnRequestStart="requestStart1"></ClientEvents>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="GrillaResultados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="GrillaResultados" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
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
                                Text="Buscado Datos Consulta...">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</body>

<script type="text/javascript">
    var ctrTransporte;
    var ctrCliente;

    jQuery(function() {

        options = {
            serviceUrl: 'ASHX/LoadTransportes.ashx',
            width: '384',
            minChars: 2,
            zIndex: 922000000
        };
        ctrTransporte = $('#' + "<%= txtTransporte.ClientID %>").autocomplete(options);


        options = {
            serviceUrl: 'ASHX/LoadClientes.ashx',
            width: '384',
            minChars: 3,
            zIndex: 922000000
        };
        ctrCliente = $('#' + "<%= txtCliente.ClientID %>").autocomplete(options);

    });



    function requestStart1(sender, args) {
        if (args.get_eventTarget().indexOf("ExportExcel") > 0) {
            args.set_enableAjax(false);
        }
    }


    function ConsultarDatos() {

        var grupo = $find("<%= cboGrupoFiltro.ClientID %>").get_value();
        var cliente = ctrCliente.currentValue;
        var transporte = ctrTransporte.currentValue;
        var fechaInicial = $find("<%= txtFechaInicial.ClientID %>").get_selectedDate().format("dd/MM/yyyy");
        var fechaFinal = $find("<%= txtFechaFinal.ClientID %>").get_selectedDate().format("dd/MM/yyyy");
        var nroPedido = $get("txtNroPedido").value;


        $find("<%=gridPre.ClientID %>").ShowWaiting("Buscando Datos...");
        PageMethods.GetDatos(grupo, nroPedido, cliente, transporte, fechaInicial, fechaFinal, onSuccess, onFailure);



    }

    function onSuccess(datos) {
        if (datos != null) {
            $find("<%=gridPre.ClientID %>").set_ClientdataSource(datos);
            $('#' + "tblDynamic_gridPre").tableGroup({ groupColumn: 3, groupClass: 'rgGroupItem', useNumChars: 0 });
        }

    }

    function onFailure() {
        alert("ERROR");
    }
</script>

</html>
