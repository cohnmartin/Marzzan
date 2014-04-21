<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="GestionVisualizacionProductos.aspx.cs"
    Inherits="GestionVisualizacionProductos" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="AjaxInfo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <title>Gestión Visualización de Productos</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>
    <telerik:RadScriptBlock runat="server">

        <script type="text/javascript">

            function CargarListaProductos(sender, args) {
                var node = args.get_node();
                var attributes = node.get_attributes();
                var CargarControl = attributes.getAttribute("Cargar");
                var id = attributes.getAttribute("Padre");
                var DescPre = node.get_text();


                if (CargarControl == "true") {

                    $find("<%=gridPre.ClientID %>").ShowWaiting("Buscando Datos...");
                    PageMethods.GetPresentaciones(id, DescPre, onSuccess, onFailure);
                    return;
                    //$find("<%=RadAjaxManager1.ClientID%>").ajaxRequest(id + "@" + DescPre);
                }


            }

            function onSuccess(datos) {
                if (datos != null)
                    $find("<%=gridPre.ClientID %>").set_ClientdataSource(datos);

            }

            function onFailure() {
                alert("ERROR");
            }

            function AplicarCambios(sender, id) {
                $find("<%=gridPre.ClientID %>").ShowWaiting("Grabando Datos...");
                var items = $find("<%= gridPre.ClientID %>").get_ItemsData();
                PageMethods.UpdateData(items, onSuccess, onFailure);

            }
        </script>

    </telerik:RadScriptBlock>
</head>
<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        <Scripts>
            <asp:ScriptReference Path="FuncionesComunes.js" />
        </Scripts>
    </asp:ScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
        Mensaje="Cargan...">
    </telerik:RadAjaxManager>
    <table cellpadding="0" cellspacing="0" style="width: 100%" border="0" align="left"
        runat="server" id="tblPrincipal">
        <tr>
            <td align="center" style="width: 100%; height: 20%">
                <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
                    <tr>
                        <td align="right">
                            <div class="Header_panelContainerSimple">
                                <div class="CabeceraInicial">
                                </div>
                            </div>
                            <div class="CabeceraContent">
                                <table style="width: 100%">
                                    <tr>
                                        <td valign="top" align="center" style="width: 100%;">
                                            <div style="position: relative; top: -25px">
                                                <asp:Label Font-Names="Bookman Old Style" Font-Size="22pt" ForeColor="#5F5F5F" ID="Label1"
                                                    runat="server">Gestión Visualización de Productos</asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%" cellspacing="0" border="0">
                                                <tr>
                                                    <td align="left" valign="top" style="width: 40%">
                                                        <asp:Panel ScrollBars="Auto" Height="410px" runat="server" ID="pnlTree">
                                                            <asp:UpdatePanel ID="upTreeProductos" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <telerik:RadTreeView ID="RadTreeProductos" runat="server" DataFieldID="IdProducto"
                                                                        DataFieldParentID="Padre" DataTextField="Descripcion" OnClientNodeClicking="CargarListaProductos"
                                                                        OnNodeDataBound="RadTreeProductos_NodeDataBound" DataValueField="IdProducto"
                                                                        Mensaje="Cargando Presentaciones...." Skin="Vista">
                                                                        <CollapseAnimation Type="OutQuint" Duration="100"></CollapseAnimation>
                                                                        <ExpandAnimation Duration="100"></ExpandAnimation>
                                                                    </telerik:RadTreeView>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </asp:Panel>
                                                        <asp:UpdatePanel ID="upActualizarVista" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div style="width: 100%; text-align: center;">
                                                                    <asp:Button ID="Button1" runat="server" Text="Act. Vista Prod." SkinID="btnBasic"
                                                                        Mensaje="Actualizando Vista Productos..." OnClick="Button1_Click" ToolTip="Actualiza la visualización de los productos en la página de pedidos" />
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td align="center" valign="top" style="padding-left: 5px; height: 450px;" width="100%">
                                                    
                                                        <asp:Panel ScrollBars="Auto" Height="400px" runat="server" ID="Panel1">
                                                            <AjaxInfo:ClientControlGrid ID="gridPre" runat="server" AllowMultiSelection="false"
                                                                TypeSkin="Vista" PositionAdd="Botton" AllowRowSelection="true" Height="390px"
                                                                Width="96%" KeyName="IdPresentacion" AllowPaging="false" PageSize="50" EmptyMessage="Debe Seleccionar una rama para ver los datos">
                                                                <Columns>
                                                                    <AjaxInfo:Column HeaderName="Descripcion" DataFieldName="Descripcion" Align="Derecha"
                                                                        Width="85%" />
                                                                    <AjaxInfo:Column DataType="Bool" HeaderName="Visible" DataFieldName="Activo" Align="Centrado" Enabled="true"
                                                                        Display="true" />
                                                                </Columns>
                                                            </AjaxInfo:ClientControlGrid>
                                                        </asp:Panel>
                                                        <div style="width: 550px; text-align: center; padding-top: 5px">
                                                            <asp:Button ID="btnGrabar" runat="server" Text="Grabar" SkinID="btnBasic" Width="77px"
                                                                Mensaje="Guardando Visibilidad..." OnClientClick="AplicarCambios();return false;"
                                                                ToolTip="Guarda los cambios efectuados en la visualización de los productos" />
                                                        </div>
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
            </td>
        </tr>
    </table>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="50">
        <ProgressTemplate>
            <div id="divBloq1">
            </div>
            <div class="processMessageTooltipGral">
                <table border="0" cellpadding="0" cellspacing="0" style="height: 62px;">
                    <tr>
                        <td align="center">
                            <img alt="a" src="Imagenes/waiting.gif" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <div id="divTituloCarga" style="font-weight: bold; font-family: Tahoma; font-size: 12px;
                                color: Gray; vertical-align: middle">
                                Cargando Presentaciones...
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</body>
</html>
