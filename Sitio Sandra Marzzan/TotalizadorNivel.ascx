<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TotalizadorNivel.ascx.cs" Inherits="TotalizadorNivel" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<table border="0"  cellpadding="0" cellspacing="0" runat="server" id="tblNivel"
    style="font-size:11px;font-family:Tahoma; height: 70px; width: 400px;" visible="false" >
    <tr >
        <td  style="border-width: medium; border-style: none none solid none; border-color: #9B9C9E;" >
                <asp:Label ID="lblNivel" runat="server" Font-Bold="True" Font-Names="Tahoma" 
                    Font-Size="10pt" Text="NIVEL 1"></asp:Label>
        </td>
        <td align="right"  style="border-width: medium; border-style: none none solid none; border-color: #9B9C9E;" >
                <asp:Label ID="lblMonto" runat="server" Font-Bold="True" Font-Names="Tahoma" 
                    Font-Size="10pt" Text="desde $275 a 500"></asp:Label>
        </td>
    </tr>
    <tr style="background-color: #9CC9E1;">
        <td colspan="2" align="left" style="padding-left:10px">
            <asp:Label ID="Label1" runat="server" Font-Names="Comic Sans MS" 
                Font-Size="11pt" Text="Te Regalamos"></asp:Label>
        </td>
    </tr>
    <tr  style="background-color: #9CC9E1;">
        <td  colspan="2" align="left" style="height:60px" valign="top">
                 <asp:ListView ID="dlDetalleNivel" runat="server" GroupItemCount="1" 
                     onitemdatabound="dlDetalleNivel_ItemDataBound" >
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
                      <td  style="cursor:hand;font-family:Sans-Serif;font-size:11px;padding-left:10px; font-weight:bold; color:White; " 
                      align="left">
                          <asp:UpdatePanel ID="upDetalle" runat="server" UpdateMode="Conditional">
                              <ContentTemplate>
                                  <asp:Label ID="lblProducto" runat="server" Text=''> </asp:Label>
                                  <asp:Label ID="lblFragancia" runat="server" Text='' ForeColor="Black"></asp:Label>
                                  <asp:HiddenField ID="HiddenIdPresentacion" runat="server" />
                                  x
                                  <asp:Label ID="lblPresentacion" runat="server"  Text='<%# Eval("objPresentacion.Descripcion")%>'> </asp:Label>
                                  &nbsp;
                                  <asp:Button ID="btnSeleccion" runat="server" Text="..." Height="15px" Width="20px" />
                                  <telerik:RadToolTip ID="ToolTipRegalo" runat="server" Skin="MySkin" TargetControlID="btnSeleccion"
                                      RelativeTo="Element" Position="BottomCenter" Title="Seleccione la Frangencia"
                                      ShowEvent="OnClick" Sticky="true" Animation="Resize" EnableEmbeddedSkins="false">
                                      <table width="100%" style="background-color: White">
                                          <tr>
                                              <td align="center">
                                                  <asp:ListBox ID="listProdRegalo" runat="server" Height="122px" Width="100%" Style="border: none"
                                                      BackColor="#F4FAFF"></asp:ListBox>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td align="center">
                                                  <asp:Button  SkinID="btnBasic" ID="btnSolicitar" runat="server" Text="Solicitar" OnClick="btnSolicitar_Click" />
                                              </td>
                                          </tr>
                                      </table>
                                  </telerik:RadToolTip>
                              </ContentTemplate>
                          </asp:UpdatePanel>
                          
                      </td>
                   </ItemTemplate>
                </asp:ListView>             
        </td>
    </tr>
    <tr style="background-color: #9CC9E1; background-image: url('Imagenes/FooterNiveles.jpg'); background-repeat: no-repeat">
        <td align="left" height="45" colspan="2" valign="bottom" style="padding-left:5px;padding-top:5px">
            <asp:Label ID="lblMontoGanado" runat="server" Font-Bold="True" Font-Names="Tahoma" 
                Font-Size="8pt" Text="Ganas $26 (precio venta al publico)"></asp:Label>
        </td>
    </tr>
</table>

<table border="0"  cellpadding="0" cellspacing="0" runat="server" id="tblSinNivel"
    style="font-size:11px;font-family:Tahoma; width: 400px;" visible="false">
    <tr >
       <td colspan="2"  style="border-width: medium; border-style: none none solid none; border-color: #9B9C9E;" >
          <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" Text="NO TE LO PIERDAS!!"></asp:Label>
        </td>
    </tr>
    <tr style="background-color: #9CC9E1;height:120px">
        <td colspan="2" align="center" style="padding-left:10px;">
            <asp:Label ID="lblSinNivel" runat="server" Font-Names="Tahoma" ForeColor="White" Font-Bold="true"
                Font-Size="10pt" Text="Este mes no has llegado al mínimo de ventas ($275) y no has podido acceder a los premios por venta. No te pierdas los importantes premios"></asp:Label>
        </td>
    </tr>
    <tr style="background-color: #9CC9E1; background-image: url('Imagenes/FooterNiveles.jpg'); background-repeat: no-repeat">
        <td align="left" height="45px" colspan="2" valign="bottom" style="padding-left:10px;padding-top:5px">
            &nbsp;
        </td>
    </tr>
</table>


<table width="100%" border="0" style="height:100%" id="tblPromos">
    <tr>
        <td>
            <asp:UpdatePanel ID="upRemitosPendientes" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <telerik:RadGrid ID="grillaRemitosPendientes" runat="server" AllowPaging="False" Width="90%"
                        Font-Size="11px" Font-Names="Segoe UI,tahoma,verdana,sans-serif" GridLines="None"
                        EnableEmbeddedSkins="false" >
                        <MasterTableView ShowFooter="false" GroupLoadMode="Client" ShowGroupFooter="true"
                           AutoGenerateColumns="False" ShowHeader="false" CommandItemDisplay="None" 
                           DataKeyNames="IdRemitoPendiente"
                           NoDetailRecordsText="" NoMasterRecordsText="" >
                            
                            <RowIndicatorColumn CurrentFilterFunction="NoFilter" FilterListOptions="VaryByDataType"
                                Visible="False">
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn CurrentFilterFunction="NoFilter" FilterListOptions="VaryByDataType"
                                Resizable="False" Visible="False">
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="Template1" HeaderText="Producto">
                                    <ItemTemplate>
                                        <table width="400px" border="0" cellpadding="0" cellspacing="0" style="background-color: White;
                                            font-family: Sans-Serif; font-size: 11px">
                                            <tr style="background-color: #9CC9E1;height:30px">
                                                <td align="center" style="font-family: Sans-Serif; font-size: 16px; color: white;font-weight: bold">
                                                    <asp:Label ID="Label6" runat="server"><%# Eval("DescArticulo")%></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="background-color: #9CC9E1;">
                                                <td align="center">
                                                    <asp:Label ID="Label3" runat="server" Text="Fecha:"></asp:Label>&nbsp;&nbsp;
                                                    <asp:Label ID="Label4" runat="server" Text='<%# string.Format("{0:dd/MM/yyyy}", Eval("FechaRemito")) %>' ></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="background-color: #9CC9E1;">
                                                <td align="center">
                                                    <asp:Label ID="lblDescComprado" runat="server" Text="Orden Nro:"></asp:Label>&nbsp;&nbsp;
                                                    <asp:Label ID="lblIdPromo" runat="server" Text='<%# Eval("Nroremito")%>' ></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="height:45px;background-color: #9CC9E1; background-image: url('Imagenes/FooterNiveles.jpg'); background-repeat: no-repeat">
                                                <td align="left" height="45px" colspan="2" valign="bottom" style="padding-left: 10px;
                                                    padding-top: 5px">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100%" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                        </ClientSettings>
                    </telerik:RadGrid>
                </ContentTemplate>
            </asp:UpdatePanel>
             
        </td>
     </tr>
</table>