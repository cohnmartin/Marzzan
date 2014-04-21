<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="GestionEliminacionPedidos.aspx.cs" Inherits="GestionEliminacionPedidos" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Gestion Eliminación Pedidos</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
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
<script type="text/javascript">
    function ShowComprobante(id) {
       
    }
</script>

<body style="background-image: url(Imagenes/repetido.jpg); margin-top:1px; background-repeat:repeat-x;background-color:White;"  >
    <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="FuncionesComunes.js" />
        </Scripts>
    </asp:ScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Web20" VisibleTitlebar="true" Title="Atención">
    </telerik:RadWindowManager>
    
    <table cellpadding="0" cellspacing="0" border="0"  style="height:100%;width:100%">
            <tr>
                <td  >
                      <div class="Header_panelContainerSimple">
                          <div class="CabeceraInicial">
                          </div>
                      </div>
                      <div class="CabeceraContent">
                       
                          <table width="100%" border="0" cellspacing="0" style="border-style: ridge; border-width: thin">
                          <tr>
                            <td align="center" colspan="3">
                                <table cellpadding="0" cellspacing="5" style="width: 80%">
                                    <tr>
                                        <td align="center" style="height: 25px; background: url('imagenes/sprite_webBlue.gif') 0  -300px repeat-x">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="14pt" Font-Names="Sans-Serif"
                                                ForeColor="Gray" Text="Gestión Eliminación de Pedidos" Width="378px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                              <tr>
                                  <td  style="width:70px;" align="right" >
                                     <asp:Label SkinID="lblBlue" ID="UserNameLabel" runat="server" >Nro Pedido:</asp:Label>
                                  </td>
                                  <td align="left" valign="middle" style="width:300px;" >
                                      <telerik:RadTextBox ID="txtNroPedido" runat="server" Skin="Vista" >
                                      </telerik:RadTextBox>
                                      
                                  </td>
                                  <td>
                                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar" SkinID="btnBasic"
                                          onclick="btnConsultar_Click" Mensaje="Buscando Pedido..." />
                                  </td>
                              </tr>
                              <tr>
                                  <td style="color: #993300; font-family: Sans-Serif; font-size: 11px" colspan="3">
                                      <asp:UpdatePanel ID="upResultado" runat="server" UpdateMode="Conditional">
                                          <ContentTemplate>
                                              <telerik:RadGrid ID="GrillaResultados" runat="server" GridLines="None" Skin="Vista"
                                                  Width="100%" OnItemDataBound="GrillaResultados_ItemDataBound">
                                                  <MasterTableView AutoGenerateColumns="False" DataKeyNames="IdCabeceraPedido" ClientDataKeyNames="IdCabeceraPedido"
                                                      ShowHeadersWhenNoRecords="true" HierarchyLoadMode="Client" TableLayout="Fixed"
                                                      NoMasterRecordsText="No se encontró el pedido solicitado">
                                                      <RowIndicatorColumn Visible="False">
                                                          <HeaderStyle Width="20px"></HeaderStyle>
                                                      </RowIndicatorColumn>
                                                      <ExpandCollapseColumn Visible="False" Resizable="False">
                                                          <HeaderStyle Width="20px"></HeaderStyle>
                                                      </ExpandCollapseColumn>
                                                      <DetailTables>
                                                          <telerik:GridTableView ClientDataKeyNames="IdDetallePedido" DataKeyNames="IdDetallePedido"
                                                              HierarchyLoadMode="Client" Width="100%" TableLayout="Fixed" GridLines="Horizontal"
                                                              AutoGenerateColumns="false">
                                                              <ParentTableRelation>
                                                                  <telerik:GridRelationFields DetailKeyField="CabeceraPedido" MasterKeyField="IdCabeceraPedido" />
                                                              </ParentTableRelation>
                                                              <RowIndicatorColumn Visible="False">
                                                                  <HeaderStyle Width="20px" />
                                                              </RowIndicatorColumn>
                                                              <ExpandCollapseColumn Resizable="False" Visible="False">
                                                                  <HeaderStyle Width="20px" />
                                                              </ExpandCollapseColumn>
                                                              <Columns>
                                                                  <telerik:GridBoundColumn DataField="Cantidad" DataType="System.Int64" HeaderText="Cantidad"
                                                                      ReadOnly="True" SortExpression="Cantidad" UniqueName="Cantidad">
                                                                      <ItemStyle Width="50px" />
                                                                      <HeaderStyle Width="50px" />
                                                                  </telerik:GridBoundColumn>
                                                                  <telerik:GridTemplateColumn HeaderText="Producto" UniqueName="ProductoColumn">
                                                                      <ItemTemplate>
                                                                          <%# GenerarEtiqueta( Eval("objProducto.DescripcionCompleta").ToString(), Eval("objPresentacion.Descripcion").ToString(), Eval("objProducto.Descripcion").ToString())%>
                                                                      </ItemTemplate>
                                                                      <HeaderStyle HorizontalAlign="Center" />
                                                                  </telerik:GridTemplateColumn>
                                                                  <telerik:GridBoundColumn DataField="ValorUnitario" DataType="System.Decimal" HeaderText="Valor Unitario"
                                                                      ReadOnly="True" SortExpression="ValorUnitario" UniqueName="ValorUnitario">
                                                                      <ItemStyle Width="75px" HorizontalAlign="Center" />
                                                                      <HeaderStyle Width="75px" />
                                                                  </telerik:GridBoundColumn>
                                                                  <telerik:GridBoundColumn DataField="ValorTotal" DataType="System.Decimal" HeaderText="Valor Total"
                                                                      ReadOnly="True" SortExpression="ValorTotal" UniqueName="ValorTotal">
                                                                      <ItemStyle Width="65px" HorizontalAlign="Center" />
                                                                      <HeaderStyle Width="65px" />
                                                                  </telerik:GridBoundColumn>
                                                              </Columns>
                                                              <SortExpressions>
                                                              </SortExpressions>
                                                              <EditFormSettings>
                                                                  <PopUpSettings ScrollBars="None" />
                                                              </EditFormSettings>
                                                          </telerik:GridTableView>
                                                      </DetailTables>
                                                      <Columns>
                                                          <telerik:GridBoundColumn DataField="Nro" HeaderText="Nro" SortExpression="Nro" UniqueName="Nro">
                                                              <ItemStyle Width="25px" HorizontalAlign="Center" />
                                                              <HeaderStyle Width="25px" />
                                                          </telerik:GridBoundColumn>
                                                          <telerik:GridTemplateColumn HeaderText="Solicitante a:" UniqueName="SolicitanteColumn">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="Label1434" runat="server" Style="text-transform: capitalize"><%# Eval("objClienteSolicitante.Nombre").ToString().ToLower()%></asp:Label>
                                                              </ItemTemplate>
                                                              <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                                              <ItemStyle Width="110px" />
                                                          </telerik:GridTemplateColumn>
                                                          <telerik:GridTemplateColumn HeaderText="Revendedor" UniqueName="ClienteColumn">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="Label1454" runat="server" Style="text-transform: capitalize"><%# Eval("objCliente.Nombre").ToString().ToLower()%></asp:Label>
                                                              </ItemTemplate>
                                                              <ItemStyle Width="110px" />
                                                              <HeaderStyle Width="110px" HorizontalAlign="Center" />
                                                          </telerik:GridTemplateColumn>
                                                          <telerik:GridBoundColumn DataField="FechaPedido" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy HH:mm}"
                                                              HeaderText="Fecha" SortExpression="FechaPedido" UniqueName="FechaPedido">
                                                              <ItemStyle Width="90px" />
                                                              <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                                          </telerik:GridBoundColumn>
                                                          <telerik:GridBoundColumn DataField="TipoPedido" HeaderText="Tipo" SortExpression="Tipo"
                                                              UniqueName="TipoPedido">
                                                              <ItemStyle Width="25px" HorizontalAlign="Center" />
                                                              <HeaderStyle Width="25px" />
                                                          </telerik:GridBoundColumn>
                                                          <telerik:GridBoundColumn DataField="MontoTotal" DataType="System.Decimal" HeaderText="Total"
                                                              SortExpression="MontoTotal" UniqueName="MontoTotal">
                                                              <ItemStyle Width="65px" HorizontalAlign="Center" />
                                                              <HeaderStyle Width="65px" HorizontalAlign="Center" />
                                                          </telerik:GridBoundColumn>
                                                          <telerik:GridTemplateColumn HeaderText="Forma Pago" UniqueName="FormaPagoColumn">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="Label1fedf" runat="server"><%# Eval("objFormaDePago.Descripcion")%></asp:Label>
                                                              </ItemTemplate>
                                                              <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                              <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                          </telerik:GridTemplateColumn>
                                                          <telerik:GridTemplateColumn HeaderText="Direccion Entrega" UniqueName="DireccionEntregaColumn"
                                                              Display="false">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="Label1fd" runat="server"><%# Eval("objDireccion.DireccionCompleta")%></asp:Label>
                                                              </ItemTemplate>
                                                              <HeaderStyle HorizontalAlign="Left" />
                                                              <ItemStyle Width="260px" />
                                                          </telerik:GridTemplateColumn>
                                                          <telerik:GridTemplateColumn HeaderText="Estado" UniqueName="EstadoColumn">
                                                              <ItemTemplate>
                                                                  <%# GenerarEstado(Eval("EstadoEnvio"))%>
                                                              </ItemTemplate>
                                                              <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                              <ItemStyle HorizontalAlign="Center" />
                                                          </telerik:GridTemplateColumn>
                                                          <telerik:GridTemplateColumn UniqueName="Template1" HeaderText="">
                                                              <ItemTemplate>
                                                                  <asp:ImageButton runat="server" ID="btnEliminar" ImageUrl="~/Imagenes/Delete.gif"
                                                                      OnClick="btnEliminar_Click" Mensaje="Eliminando Pedido..." />
                                                              </ItemTemplate>
                                                              <HeaderStyle HorizontalAlign="Center" Width="18px" />
                                                              <ItemStyle Width="18px" HorizontalAlign="Center" />
                                                          </telerik:GridTemplateColumn>
                                                      </Columns>
                                                      <EditFormSettings>
                                                          <PopUpSettings ScrollBars="None"></PopUpSettings>
                                                      </EditFormSettings>
                                                  </MasterTableView>
                                                  <ClientSettings>
                                                      <Selecting AllowRowSelect="True" />
                                                  </ClientSettings>
                                              </telerik:RadGrid>
                                          </ContentTemplate>
                                          <Triggers>
                                              <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                          </Triggers>
                                      </asp:UpdatePanel>
                                  </td>
                              </tr>
                          </table>
                       
                      </div>
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
