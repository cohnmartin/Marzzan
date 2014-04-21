<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TotalizadorPedido.ascx.cs" Inherits="TotalizadorPedido" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script type="text/javascript">

</script>
<table width="100%" border="0" style="font-size:11px;font-family:Tahoma">
    <tr style="display:none">
        <td  >
            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                <tr >
                    <td align="left" style="width:60%;background-image: url('Imagenes/TituloSecundarioResumen.jpg'); background-repeat: repeat-y; color: #FFFFFF; font-weight: bold; font-family: Bookman Old Style; font-size: 12px;" >
                        <asp:Label ID="lblTituloSec" Text="FEMENINO" runat="server" ></asp:Label>   
                    </td>
                    <td align="left" >
                        &nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center">
            <telerik:RadGrid ID="grillaPedidos" runat="server" 
                AllowPaging="False" 
                Width="90%" Font-Size="11px" Font-Names="Segoe UI,tahoma,verdana,sans-serif" 
                GridLines="None" 
                OnItemDataBound="grillaPedidos_ItemDataBound" 
                Skin="Vista"  >
                <MasterTableView ShowFooter="false" GroupLoadMode="Client" ShowGroupFooter="true"
                 AutoGenerateColumns="False" 
                 CommandItemDisplay="None"  
                 GroupHeaderItemStyle-HorizontalAlign="Left"
                 DataKeyNames="Producto,Presentacion,Cantidad" 
                    NoDetailRecordsText="No hay productos solicitado" 
                    NoMasterRecordsText="No hay productos solicitado">
                    <GroupByExpressions>
                    
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="DescPadre" HeaderText="Grupo" />
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="IdPadre" SortOrder="Descending" />
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>

                    <RowIndicatorColumn CurrentFilterFunction="NoFilter" FilterListOptions="VaryByDataType"
                        Visible="False" >
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn CurrentFilterFunction="NoFilter" FilterListOptions="VaryByDataType"
                        Resizable="False" Visible="False" >
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="Template1" HeaderText="" >
                            <ItemTemplate> 
                                <asp:ImageButton runat="server" ID="btnEditar" ImageUrl="~/Imagenes/Edit.gif"  />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        
                        <telerik:GridBoundColumn CurrentFilterFunction="NoFilter" Visible="false"
                            DataField="CodigoCompleto" DataType="System.Int64"
                            FilterListOptions="VaryByDataType" ForceExtractValue="None" HeaderText="Codigo"
                            ReadOnly="True" SortExpression="CodigoCompleto" UniqueName="CodigoCompleto" >
                            <ItemStyle HorizontalAlign="Left" Width="15%" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn CurrentFilterFunction="NoFilter" DataField="DescripcionCompleta"
                            FilterListOptions="VaryByDataType" ForceExtractValue="Always"
                            HeaderText="Descripcion" SortExpression="DescripcionCompleta" 
                            UniqueName="DescripcionCompleta" >
                            <ItemStyle HorizontalAlign="Left" Width="55%" />
                        </telerik:GridBoundColumn>
                    
                        <telerik:GridBoundColumn 
                            Aggregate="Sum" 
                            DataField="Cantidad" 
                            HeaderText="Cantidad" 
                            UniqueName="Cantidad" 
                            FooterText="Total:">
                            <ItemStyle Width="250px" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn  DataField="ValorUnitario" HeaderText="V. U." UniqueName="ValorUnitario" >
                        <ItemStyle Width="280px" />
                        <HeaderStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                       
                        <telerik:GridBoundColumn 
                            Aggregate="Sum" 
                            DataField="ValorTotal" 
                            HeaderText="Valor Total" 
                            UniqueName="ValorTotal" 
                            FooterText="Total:">
                            <ItemStyle Width="250px" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridTemplateColumn UniqueName="Template1" HeaderText="">
                            <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="btnEliminar" ImageUrl="~/Imagenes/Delete.gif" OnClientClick="javascript:if(!confirm('Esta seguro de eliminar la línea de pedido? La misma se eliminará definitivamente del pedido actual.')){return false;}" OnClick="btnEliminar_Click" />
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50" AssociatedUpdatePanelID="upResumen">
                                            <ProgressTemplate>
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
                                                                    Text="Eliminando Pedido...">
                                                                </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                   
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                       
                    </Columns>
                   
                </MasterTableView> 
                <ClientSettings>
                </ClientSettings> 
                
            </telerik:RadGrid>
    
        </td>
    </tr>
    <tr style="display:none">
        <td>
            <table width="100%" border="0">
                <tr>
                    <td align="right" style="font-weight:bold;font-size:11px;width:70%;font-family:Tahoma" >
                        Total Solicitado:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtCantidadTotal" runat="server" 
                        BackColor="GrayText" ForeColor="White"  BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" style="text-align:center;font-weight:bold"
                        Width="80px" ReadOnly="true" Height="15px" ></asp:TextBox>
                        
                        <asp:TextBox ID="txtValorTotal" runat="server" 
                        BackColor="GrayText" ForeColor="White"  BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" style="text-align:center;font-weight:bold"
                        Width="80px" ReadOnly="true" Height="15px" ></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>