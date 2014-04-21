<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="CuentaBolsos.aspx.cs" Inherits="CuentaBolsos" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Gestión Cuenta Bolsos</title>
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

        var comboRegalo = $find("<%= cboIncoporaciones.ClientID%>");
        var comboPago = $find("<%= cboFormaPago.ClientID%>");
        var comboDireccion = $find("<%= cboDirecciones.ClientID%>");
        var tooltip = $find("<%= ToolTipRegalo.ClientID%>");

        if (comboPago.get_selectedIndex() == 0) {
            tooltip.set_text("Debe Seleccionar la forma de pago que va a utilizar para la incorporación antes de poder realizar el pedido.");
            tooltip.set_targetControlID("<%= cboFormaPago.ClientID%>");
            tooltip.show();
            return false;
        }
        else if (comboDireccion.get_selectedIndex() == 0) {
            tooltip.set_text("Debe Seleccionar la dirección de entrega para la incorporación antes de poder realizar el pedido.");
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
    function eventSeleccionarFragancia()
    {

        var codigo = event.srcElement.getAttribute("Codigo");
        var grupo = event.srcElement.getAttribute("Grupo");
        var agrupador = event.srcElement.getAttribute("Agrupador");
        var tooltip = $find("<%= ToolTipSelFragancias.ClientID%>");
        tooltip.hide();
        $find("<%=RadAjaxManager1.ClientID%>").ajaxRequest(codigo + "@" + grupo + "@" + event.srcElement.innerText + "@" + agrupador);

    }

    function EliminarGrupo(grupo) {

        $find("<%=RadAjaxManager1.ClientID%>").ajaxRequest(grupo);
        
    }
    function ShowSeleccionFragancia() {

               
        var codigos = event.srcElement.getAttribute("Codigos").split('|');
        var fragancias = event.srcElement.getAttribute("Fragancias").split('|');
        var grupo = event.srcElement.getAttribute("Grupo");
        var agrupador = event.srcElement.getAttribute("Agrupador");
        
        var resultshere = document.getElementById("tdSeleccionFragancias");
        var tableContainer = document.createElement("table");
        var tableContainerTbody = document.createElement("tbody");


  
        tableContainer.setAttribute("width", "100%");
        tableContainer.setAttribute("height", "100px");
        tableContainer.setAttribute("id", "tblSelFrag");
        tableContainer.bgColor = "transparent";
        
        
        tableContainerTbody.setAttribute("id", "tblSelFrag");
        tableContainerTbody.setAttribute("height", "100px");
        

        for (var j = 0; j < fragancias.length - 1; j++) {

            var trProductos = document.createElement("tr");
            var tdProductos = document.createElement("td");
            tdProductos.setAttribute("align", "center");
            tdProductos.onmouseover = setStyle;
            tdProductos.onmouseout = resetStyle;
            tdProductos.onclick = eventSeleccionarFragancia;
            tdProductos.setAttribute("Codigo", codigos[j]);
            tdProductos.setAttribute("Grupo", grupo);
            tdProductos.setAttribute("Agrupador", agrupador);
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
<body style="background-image: url(Imagenes/repetido.jpg); margin-top:1px; background-repeat:repeat-x;background-color:White;"  >
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <table cellpadding="0" cellspacing="0" border="0"  style="height:100%;width:100%">
            <tr>
                <td  >
                      
                      <div class="Header_panelContainerSimple">
                          <div class="CabeceraInicial">
                            <asp:Button ID="btnVolver" runat="server" Text="" SkinID="btnVolver" OnClick="btnVolver_Click"  Visible="false" />
                          </div>
                      </div>
                      <div class="CabeceraContent" style="text-align:center " >
                          <table cellpadding="0" cellspacing="0" width="80%" border="0" >
                              <tr >
                                  <td align="center" >
                                      <table cellpadding="0" cellspacing="0" style="width: 100%">
                                          <tr>
                                              <td align="center" style="height: 25px; background: url('imagenes/sprite_webBlue.gif') 0  -300px repeat-x">
                                                  <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="14pt" 
                                                    Font-Names="Sans-Serif"
                                                    ForeColor="Gray" Text="Pedidos Cuenta Bolsos" Width="100%"></asp:Label>
                                              </td>
                                          </tr>
                                      </table>
                                  </td>
                              </tr>
                              <tr style="padding-bottom:10px;padding-top:10px;background-color:Transparent;FILTER: progid:DXImageTransform.Microsoft.Gradient(gradientType=0,startColorStr=#7fffd4,endColorStr=#00ff99); ">
                                <td>
                                    <table cellpadding="0" cellspacing="2" class="style6" border="0" >
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
                                                <asp:Label ID="label2" runat="server" SkinID="lblBlue">A Retirar por:</asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblConsultor" runat="server" SkinID="lblBlack" Visible="false"></asp:Label>
                                                <telerik:RadTextBox ID="txtDestinatario" runat="server" Width="310px" style="text-transform:capitalize"></telerik:RadTextBox>
                                                
                                            </td>
                                            
                                            
                                        </tr>
                                        <tr >
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
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:Label ID="label6" runat="server" SkinID="lblBlue">Dirección Entrega:</asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:UpdatePanel ID="upDirecciones" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <telerik:RadComboBox Skin="WebBlue" ID="cboDirecciones" runat="server" Width="310px"
                                                            AutoPostBack="false" HighlightTemplatedItems="true" NoWrap="false"  >
                                                            <HeaderTemplate >
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
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:Label ID="label3" runat="server" SkinID="lblBlue">tipo Incorporacion:</asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:UpdatePanel ID="upRegalos" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <telerik:RadComboBox Skin="WebBlue" ID="cboIncoporaciones" runat="server" Width="310px"
                                                            AutoPostBack="false" AllowCustomText="true" MarkFirstMatch="true">
                                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                                        </telerik:RadComboBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label SkinID="lblBlue" ID="Label9" Text="Cantidad:" runat="server"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Value="1" MaxValue="10"
                                                    MinValue="1" ShowSpinButtons="True" Skin="Vista" Width="70px">
                                                    <NumberFormat DecimalDigits="0" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center" style="padding-top: 5px">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnAgregarDefinicion" runat="server" SkinID="btnAgregar" Text="Agregar"
                                                            OnClick="btnAgregarDefinicion_Click" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="padding-top: 15px">
                                                <table border="0" cellpadding="0" cellspacing="0" runat="server" id="tblSinNivel"
                                                    style="font-size: 11px; font-family: Tahoma; width: 400px;">
                                                    <tr>
                                                        <td colspan="2" style="border-width: medium; border-style: none none solid none;
                                                            border-color: #6C7984;">
                                                            <asp:Label ID="lblRegalo" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt"
                                                                Text="DETALLE DEL PEDIDO"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #ACD1E6; height: 120px">
                                                        <td colspan="2" align="center" style="padding-left: 5px; width: 100%">
                                                            <asp:UpdatePanel ID="upRegalo" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:ListView ID="dlDetalleRegalo" runat="server" GroupItemCount="1" OnItemDataBound="dlDetalleRegalo_ItemDataBound">
                                                                        <LayoutTemplate>
                                                                            <table width="380px">
                                                                                <tr>
                                                                                    <td>
                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="380px">
                                                                                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                           
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <table width="100%">
                                                                                         <%# AddGroupingRow()%>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="font-family: Sans-Serif; font-size: 11px; padding-left: 10px; font-weight: bold;
                                                                                    color: White;" align="left">
                                                                                    <asp:UpdatePanel ID="upDetalle" runat="server" UpdateMode="Conditional">
                                                                                        <ContentTemplate>
                                                                                            <table width="100%">
                                                                                                <tr>
                                                                                                    <td style="width: 90%">
                                                                                                        .
                                                                                                        <asp:Label ID="lblProducto" SkinID="lblWhite" runat="server" Text='<%# Eval("Descripcion")%>'> </asp:Label>
                                                                                                        <asp:Label ID="Label4" SkinID="lblWhite" runat="server" Text='<%# Eval("Presentacion")%>'> </asp:Label>
                                                                                                    </td>
                                                                                                    <td id="tdSeleccionFragancia" runat="server" style="width: 10%">
                                                                                                        <asp:ImageButton ID="btnSeleccionarFragancia" runat="server" ImageUrl="~/Imagenes/Find.gif"
                                                                                                            OnClientClick="ShowSeleccionFragancia(); return false;" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                    <telerik:RadToolTip ID="ToolTipSelFragancias" runat="server" Skin="Office2007" ManualClose="true"
                                                                        RelativeTo="BrowserWindow" Position="Center" Title="Por favor seleccione la fragancia"
                                                                        ShowEvent="FromCode" Sticky="false" Animation="Fade" Width="310px" Height="300px"
                                                                        ContentScrolling="Y">
                                                                        <table width="100%" style="height: 300px; overflow: scroll">
                                                                            <tr>
                                                                                <td id="tdSeleccionFragancias" valign="middle" style="height: 300px; overflow: scroll">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </telerik:RadToolTip>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="cboIncoporaciones" EventName="SelectedIndexChanged" />
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
                                            <td colspan="2" >
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button SkinID="btnBasic" ID="btnRealizarRegalo" Text="Realizar Pedido" runat="server"
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
                                </td>
                              </tr>
                          </table>
                          
                       
                       
                      </div>
                </td>
            </tr>
     </table>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
    </telerik:RadAjaxManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50">
        <ProgressTemplate>
            <div id="DivBloque" class="progressBackgroundFilterBlue" >
            </div>  
            <div style="position: absolute; top: 45%; left: 38%; padding: 5px; width: 24%; z-index: 1001;
                background-color: Transparent; vertical-align: middle; text-align: center;">
                <table border="0" cellpadding="0" cellspacing="0" style="height: 62px;">
                    <tr>
                        <td align="center">
                            <img alt="a" src="Imagenes/waiting.gif"   />
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
   

</body>
</html>
