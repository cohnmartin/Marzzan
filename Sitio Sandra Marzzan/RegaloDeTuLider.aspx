<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="RegaloDeTuLider.aspx.cs"
    Inherits="RegaloDeTuLider" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Regalo de tu Líder</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .style6
        {
            width: 40%;
        }
        .MouseOverStyle
        {
            color: #363636;
            border: 1px solid #99defd;
            background: #f6fbfd url(Imagenes/ItemHoveredBg.gif) repeat-x left bottom;
            cursor: hand;
        }
        .MouseOutStyle
        {
            background-color: Transparent;
            color: Black;
            cursor: hand;
        }
    </style>
</head>
<script type="text/javascript">
    function ShowFraganciasFaltantes() {
        var tooltip = $find("toolTipFragancias");
        tooltip.set_text("Hay regalos que no tienen seleccionada la fragancia. Por favor verifique los datos.");
        tooltip.set_targetControlID("<%= dlDetalleRegalo.ClientID%>");
        tooltip.show();
    }
    function DatosCompletos() {

        var comboCliente = $find("<%= cboConsultores.ClientID%>");
        var comboRegalo = $find("<%= cboRegalos.ClientID%>");
        var comboPago = $find("<%= cboFormaPago.ClientID%>");
        var comboDireccion = $find("<%= cboDirecciones.ClientID%>");
        var tooltip = $find("<%= ToolTipRegalo.ClientID%>");

        if (comboCliente.get_value() == 0) {
            tooltip.set_text("Debe Seleccionar el revendedor antes de poder realizar el pedido.");
            tooltip.set_targetControlID("<%= cboConsultores.ClientID%>");
            tooltip.show();
            return false;
        }
        else if (comboRegalo.get_selectedIndex() == 0) {
            tooltip.set_text("Debe Seleccionar el regalo que desea hacer antes de poder realizar el pedido.");
            tooltip.set_targetControlID("<%= cboRegalos.ClientID%>");
            tooltip.show();
            return false;
        }
        //        else if (comboPago.get_selectedIndex() == 0) {
        //            tooltip.set_text("Debe Seleccionar la forma de pago que va a utilizar para el regalo antes de poder realizar el pedido.");
        //            tooltip.set_targetControlID("");
        //            tooltip.show();
        //            return false;
        //        }
        else if (comboDireccion.get_selectedIndex() == 0) {
            tooltip.set_text("Debe Seleccionar la dirección de entrega del regalo que desea hacer antes de poder realizar el pedido.");
            tooltip.set_targetControlID("<%= cboDirecciones.ClientID%>");
            tooltip.show();
            return false;
        }
        else
            return true;

    }
    function setStyle() {
        this.className = 'MouseOverStyle';
    }
    function resetStyle() {
        this.className = 'MouseOutStyle';
    }
    function eventSeleccionarFragancia() {

        var codigo = event.srcElement.getAttribute("Codigo");
        var grupo = event.srcElement.getAttribute("Grupo");
        var tooltip = $find("<%= ToolTipSelFragancias.ClientID%>");
        tooltip.hide();
        $find("<%=RadAjaxManager1.ClientID%>").ajaxRequest(codigo + "@" + grupo + "@" + event.srcElement.innerText);

    }
    function ShowSeleccionFragancia() {
        var codigos = event.srcElement.getAttribute("Codigos").split('|');
        var fragancias = event.srcElement.getAttribute("Fragancias").split('|');
        var grupo = event.srcElement.getAttribute("Grupo");
        var resultshere = document.getElementById("tdSeleccionFragancias");
        var tableContainer = document.createElement("table");
        var tableContainerTbody = document.createElement("tbody");



        tableContainer.setAttribute("width", "100%");
        tableContainer.setAttribute("height", "100%");
        tableContainer.setAttribute("id", "tblSelFrag");
        tableContainerTbody.setAttribute("id", "tblSelFrag");
        tableContainer.bgColor = "transparent";

        for (var j = 0; j < fragancias.length - 1; j++) {

            var trProductos = document.createElement("tr");
            var tdProductos = document.createElement("td");
            tdProductos.setAttribute("align", "left");
            tdProductos.setAttribute("style", "padding-left:10px;");
            tdProductos.onmouseover = setStyle;
            tdProductos.onmouseout = resetStyle;
            tdProductos.onclick = eventSeleccionarFragancia;
            tdProductos.setAttribute("Codigo", codigos[j]);
            tdProductos.setAttribute("Grupo", grupo);
            tdProductos.innerHTML = fragancias[j];
            trProductos.appendChild(tdProductos);
            tableContainerTbody.appendChild(trProductos);
        }


        tableContainer.appendChild(tableContainerTbody);
        resultshere.appendChild(tableContainer);

        var tooltip = $find("<%= ToolTipSelFragancias.ClientID%>");
        tooltip.show();
    }


</script>
<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
        <tr>
            <td align="center">
                <div class="Header_panelContainerSimple">
                    <div class="CabeceraInicial">
                        <asp:Button ID="btnVolver" runat="server" Text="" SkinID="btnVolver" OnClick="btnVolver_Click"
                            Visible="false" />
                    </div>
                </div>
                <div >
                    <table cellpadding="0" cellspacing="2" class="style6" border="0">
                        <tr>
                            <td style="text-align: left; width: 30%">
                                <asp:Label ID="label1" runat="server" SkinID="lblBlue">Líder:</asp:Label>
                            </td>
                            <td style="font-weight: bold; font-size: 13px; text-align: left">
                                <asp:Label ID="lblLider" runat="server" SkinID="lblBlack"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="label2" runat="server" SkinID="lblBlue">Revendedor:</asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:UpdatePanel ID="upConsultores" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <telerik:RadComboBox Skin="WebBlue" ID="cboConsultores" runat="server" Width="310px"
                                            AllowCustomText="true" MarkFirstMatch="true" AutoPostBack="true" OnSelectedIndexChanged="cboConsultores_SelectedIndexChanged">
                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                        </telerik:RadComboBox>
                                        <asp:Label ID="lblConsultor" runat="server" SkinID="lblBlack" Visible="false"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="label6" runat="server" SkinID="lblBlue">Dirección Entrega:</asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:UpdatePanel ID="upDirecciones" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <telerik:RadComboBox Skin="WebBlue" ID="cboDirecciones" runat="server" Width="310px"
                                            AutoPostBack="false" HighlightTemplatedItems="true">
                                            <HeaderTemplate>
                                                <table style="width: 100%; text-align: left">
                                                    <tr>
                                                        <td style="width: 125px;">
                                                            Provincia
                                                        </td>
                                                        <td style="width: 125px;">
                                                            Localidad
                                                        </td>
                                                        <td style="width: 125px;">
                                                            Calle y Nro
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 100%; text-align: left; text-transform: lowercase">
                                                    <tr>
                                                        <td style="width: 125px; text-transform: lowercase">
                                                            <%# DataBinder.Eval(Container.DataItem, "Provincia") %>
                                                        </td>
                                                        <td style="width: 125px; text-transform: lowercase">
                                                            <%# DataBinder.Eval(Container.DataItem, "Localidad") %>
                                                        </td>
                                                        <td style="width: 125px; text-transform: inherit">
                                                            <%# DataBinder.Eval(Container.DataItem, "Calle") %>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                        </telerik:RadComboBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cboConsultores" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="label3" runat="server" SkinID="lblBlue">Regalo:</asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:UpdatePanel ID="upRegalos" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <telerik:RadComboBox Skin="WebBlue" ID="cboRegalos" runat="server" Width="310px"
                                            AutoPostBack="True" AllowCustomText="true" MarkFirstMatch="true" OnSelectedIndexChanged="cboRegalos_SelectedIndexChanged">
                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                        </telerik:RadComboBox>
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50" AssociatedUpdatePanelID="upRegalos">
                                            <ProgressTemplate>
                                                <div style="position: absolute; top: 50%; left: 45%; padding: 5px; width: 100%; z-index: 1001;
                                                    background-color: Transparent; vertical-align: middle; text-align: center;">
                                                    <table border="0" cellpadding="0" cellspacing="0" style="height: 62px;">
                                                        <tr>
                                                            <td align="center">
                                                                <img alt="a" src="Imagenes/waiting.gif" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td style="text-align: left">
                                <asp:Label ID="label5" runat="server" SkinID="lblBlue">Forma de Pago:</asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:UpdatePanel ID="upFormaDePago" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <telerik:RadComboBox Skin="WebBlue" ID="cboFormaPago" runat="server" Width="310px"
                                            AutoPostBack="false">
                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                        </telerik:RadComboBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td style="text-align: left">
                                <asp:Label ID="label8" runat="server" SkinID="lblBlue">Cantidad:</asp:Label>
                            </td>
                            <td style="text-align: left">
                                <telerik:RadNumericTextBox ID="txtCantidad" runat="server" NumberFormat-DecimalDigits="0"
                                    Value="1" MaxValue="10" MinValue="1" ShowSpinButtons="True" Skin="Vista" Width="70px">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center" style="padding-top: 35px">
                                <table border="0" cellpadding="0" cellspacing="0" runat="server" id="tblSinNivel"
                                    style="font-size: 11px; font-family: Tahoma; width: 400px;">
                                    <tr>
                                        <td colspan="2" style="border-width: medium; border-style: none none solid none;
                                            border-color: #6C7984;">
                                            <asp:Label ID="lblRegalo" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt"
                                                Text="DETALLE DEL REGALO"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="background-color: #ACD1E6; height: 120px">
                                        <td colspan="2" align="center" style="padding-left: 5px;">
                                            <asp:UpdatePanel ID="upRegalo" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:ListView ID="dlDetalleRegalo" runat="server" GroupItemCount="1" OnItemDataBound="dlDetalleRegalo_ItemDataBound">
                                                        <LayoutTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                            <asp:PlaceHolder runat="server" ID="groupPlaceHolder"></asp:PlaceHolder>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <GroupTemplate>
                                                            <tr>
                                                                <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                                                            </tr>
                                                        </GroupTemplate>
                                                        <ItemTemplate>
                                                            <td style="font-family: Sans-Serif; font-size: 11px; padding-left: 10px; font-weight: bold;
                                                                color: White;" align="left">
                                                                <asp:UpdatePanel ID="upDetalle" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td>
                                                                                    .
                                                                                    <asp:Label ID="lblProducto" SkinID="lblWhiteLarge" runat="server" Text='<%# Eval("Descripcion")%>'> </asp:Label>
                                                                                    <asp:Label ID="Label7" SkinID="lblWhiteLarge" runat="server" Text='<%# Eval("FraganciaSeleccionada")%>'> </asp:Label>
                                                                                    x
                                                                                    <asp:Label ID="Label4" SkinID="lblWhiteLarge" runat="server" Text='<%# Eval("Presentacion")%>'> </asp:Label>
                                                                                </td>
                                                                                <td id="tdSeleccionFragancia" runat="server">
                                                                                    <asp:ImageButton ID="btnSeleccionarFragancia" runat="server" ImageUrl="~/Imagenes/Find.gif"
                                                                                        OnClientClick="ShowSeleccionFragancia(); return false;" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                    <telerik:RadToolTip ID="ToolTipSelFragancias" runat="server" Skin="Office2007" ManualClose="true"
                                                        Modal="true" RelativeTo="BrowserWindow" Position="Center" Title="Por favor seleccione la fragancia!!"
                                                        ShowEvent="FromCode" Animation="Fade" Width="410px" Height="300px" ContentScrolling="Y">
                                                        <table width="98%" style="height: 300px; overflow: scroll">
                                                            <tr>
                                                                <td id="tdSeleccionFragancias" align="left" valign="middle" style="height: 300px;">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadToolTip>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cboRegalos" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr style="background-color: Transparent; background-image: url('Imagenes/FooterNiveles.gif');
                                        background-repeat: no-repeat">
                                        <td align="left" height="45px" colspan="2" valign="bottom" style="padding-left: 10px;
                                            padding-top: 5px">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <telerik:RadToolTip ID="ToolTipRegalo" runat="server" Skin="Hay" TargetControlID="btnRealizarRegalo"
                                    RelativeTo="Element" Position="MiddleRight" Title="Atención!!" ShowEvent="FromCode"
                                    Sticky="false" Animation="Resize">
                                </telerik:RadToolTip>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Button SkinID="btnBasic" ID="btnRealizarRegalo" Text="Realizar Regalo" runat="server"
                                            OnClientClick="return DatosCompletos();" OnClick="btnRealizarRegalo_Click" />
                                        <telerik:RadToolTip ID="toolTipFragancias" runat="server" Width="240px" Skin="Hay"
                                            ShowCallout="true" ManualClose="true" TargetControlID="btnRealizarRegalo" RelativeTo="BrowserWindow"
                                            Position="Center" Title="Atención!!" ShowEvent="FromCode" Sticky="false" Animation="Fade">
                                        </telerik:RadToolTip>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
    </telerik:RadAjaxManager>
    </form>
</body>
</html>
