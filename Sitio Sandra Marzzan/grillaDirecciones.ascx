<%@ Control Language="C#" AutoEventWireup="true" CodeFile="grillaDirecciones.ascx.cs" Inherits="grillaDirecciones" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<script type="text/javascript">
    
    function SeleccionarFila(sender, eventArgs)
    {
        var evt = eventArgs.get_domEvent();   
        var index = eventArgs.get_itemIndexHierarchical();   
        sender.get_masterTableView().selectItem(sender.get_masterTableView().get_dataItems()[index].get_element(), true);   
        
        evt.cancelBubble = true;   
        evt.returnValue = false;   

        if (evt.stopPropagation)   
        {   
           evt.stopPropagation();   
           evt.preventDefault();   
        }   

    }
</script>

<table width="100%" border="0" cellpadding="2" cellspacing="2" >
    <tr>
        <td align="center">
              <asp:UpdatePanel ID="upGrillaDir"  runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
            <telerik:RadGrid ID="grillaDir" runat="server" AllowPaging="False" 
                Width="100%" Font-Size="11px" Font-Names="Tahoma" 
                GridLines="None" Skin="WebBlue"  >
                <MasterTableView ShowFooter="false"
                 AutoGenerateColumns="False" CommandItemDisplay="None" 
                 DataKeyNames="IdDireccion" 
                    NoDetailRecordsText="No hay otras Direcciones" 
                    NoMasterRecordsText="No hay otras Direcciones">
                    <Columns>
                        <telerik:GridBoundColumn CurrentFilterFunction="NoFilter" 
                            DataField="IdDireccion" DataType="System.Int64"
                            ReadOnly="True" UniqueName="IdDireccion" Visible="false">
                        </telerik:GridBoundColumn>
 
                        
                        <telerik:GridBoundColumn CurrentFilterFunction="NoFilter" DataField="Calle"
                            FilterListOptions="VaryByDataType" ForceExtractValue="None"
                            HeaderText="Calle" SortExpression="Calle">
                            <ItemStyle HorizontalAlign="Left" Width="40%" CssClass="UseHand" /> 
                        </telerik:GridBoundColumn>
                        
                       
                        <telerik:GridBoundColumn CurrentFilterFunction="NoFilter" DataField="Provincia"
                            FilterListOptions="VaryByDataType" ForceExtractValue="None"
                            HeaderText="Provincia" SortExpression="Provincia">
                            <ItemStyle HorizontalAlign="Left" Width="15%" CssClass="UseHand" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn CurrentFilterFunction="NoFilter" DataField="Localidad"
                            FilterListOptions="VaryByDataType" ForceExtractValue="None"
                            HeaderText="Localidad" SortExpression="Localidad">
                            <ItemStyle HorizontalAlign="Left" Width="15%" CssClass="UseHand" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView> 
                <ClientSettings>
                    <Selecting AllowRowSelect="True" />
                    <ClientEvents OnRowClick="SeleccionarFila" />
                </ClientSettings>
            </telerik:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>    
        </td>
    </tr>
    <tr>
        <td align="center" style="margin-top:50px;">
        <asp:Button SkinID="btnBasic" ID="btnAceptar" Text="Aceptar" runat="server" 
            ValidationGroup="Login1" Width="101px" onclick="btnAceptar_Click"  />
        </td>
    </tr>
</table>