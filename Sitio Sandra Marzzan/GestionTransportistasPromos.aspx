<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="GestionTransportistasPromos.aspx.cs" Inherits="GestionTransportistasPromos" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listado de Transportistas Habilitados para la Promoción</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
</head>
<body >
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="FuncionesComunes.js" />
            </Scripts>
        </asp:ScriptManager>
        
        <telerik:RadAjaxManager Mensaje="Agregando Transportista.." ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        </telerik:RadAjaxManager>
    
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Web20" 
         VisibleTitlebar="true" Title="Atención" >
        </telerik:RadWindowManager>
    
    <div style="text-align: center ">
        <asp:UpdatePanel ID="upGrilla" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <telerik:RadGrid ID="gvTranportistas" runat="server" AutoGenerateColumns="false"
                    GridLines="None" Skin="Vista" Height="100%" Width="95%" AllowAutomaticUpdates="True"
                    AllowAutomaticDeletes="True">
                    <MasterTableView ShowHeadersWhenNoRecords="true" ShowHeader="true" EditMode="PopUp"
                        DataKeyNames="IdTransporteHabilitado" NoMasterRecordsText="No hay Transportistas Habilitados"
                        NoDetailRecordsText="No hay Transportistas Habilitados"
                        Width="100%" Height="100%" ShowFooter="true" CommandItemDisplay="Top" TableLayout="Fixed">
                        <CommandItemTemplate>
                            <div style="padding: 5px 5px;">
                                <table width="100%">
                                    <tr>
                                        <td align="right">
                                            <telerik:RadComboBox ID="cboTransporte" runat="server" Skin="Vista" Width="280px"
                                                EmptyMessage="Seleccione un transportista" />
                                        </td>
                                        <td align="left">
                                            <img style="border: 0px; vertical-align: middle; padding-right: 5px; cursor: hand"
                                                alt="" src="Imagenes/AddRecord.gif" onclick="AgregarTransportista();" />Agregar
                                            Transportista
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </CommandItemTemplate>
                        <RowIndicatorColumn Visible="False">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn Visible="False" Resizable="False">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Transporte" HeaderText="Transportista" UniqueName="TransportistaColumn">
                                <ItemStyle Wrap="false" HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="Template1">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="btnEliminar" ImageUrl="~/Imagenes/Delete.gif"
                                        OnClick="btnEliminar_Click" Mensaje="Eliminando Transportista..." />
                                </ItemTemplate>
                                <HeaderStyle Width="30px" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="false" UseStaticHeaders="false" SaveScrollPosition="false" />
                    </ClientSettings>
                </telerik:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
      
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
                                color: Gray; vertical-align: middle" >
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</body>

       <script type="text/javascript">

           function CloseWindows() {

               var oWnd = GetRadWindow();
               var oArg = new Object();

               oWnd.close();

           }

           function GetRadWindow() {
               var oWindow = null;
               if (window.radWindow) oWindow = window.radWindow;
               else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
               return oWindow;
           }

           function AgregarTransportista() {
               $find("<%=RadAjaxManager1.ClientID%>").ajaxRequest("Agregar");
           }
           

           function AlertaTransportista() {
               var oWnd = radalert('Debe Seleccionar un Transportista para agregar.');
           }
    </script>
    
</html>
