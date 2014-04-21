<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DiseñoCabecera.aspx.cs" Inherits="DiseñoCabecera" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 112px;
        }
        .style3
        {
            width: 137px;
        }
        .style4
        {
            width: 28px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td rowspan="6" class="style2">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Logo-solo.jpg" />
                </td>
                <td align="left" style="color: #336699; font-weight: bold" class="style3">
                    NOTA DE PEDIDO
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left">
                    <span style="font-weight: bold; font-size: 12px">Nro:</span>
                </td>
                <td class="style4">
                    <asp:Label ID="lblNroNotaImp" runat="server" Text=""> </asp:Label>
                </td>
                <td align="left">
                    <span style="font-weight: bold; font-size: 12px">Fecha:</span>
                </td>
                <td>
                    <asp:Label ID="lblFechaImp" runat="server" Text=""> </asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" class="style3">
                    <span style="font-weight: bold; font-size: 12px">Revendedor:</span>
                </td>
                <td align="left">
                    <telerik:RadComboBox Skin="WebBlue" ID="cboConsultores" runat="server" Width="256px"
                        AutoPostBack="True" >
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </telerik:RadComboBox>
                </td>
                <td align="left" colspan="2">
                    <span style="font-weight: bold; font-size: 12px">Total Pedido:</span>
                    <asp:TextBox ID="txtTotalGeneral" runat="server" BackColor="GrayText" ForeColor="White"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" Style="text-align: center;
                        font-weight: bold" Width="80px" ReadOnly="true" Height="15px" Text="0"></asp:TextBox>
                </td>
                <td align="left" colspan="2">
                    <span style="font-weight: bold; font-size: 12px">Monto Total:</span>
                    <asp:TextBox ID="txtMontoGeneral" runat="server" Text="0" BackColor="GrayText" ForeColor="White"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" Style="text-align: center;
                        font-weight: bold" Width="80px" ReadOnly="true" Height="15px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="style3">
                    <span style="font-weight: bold; font-size: 12px">Dirección Entrega:</span></td>
                <td colspan="5">
                    <asp:Label ID="lblDireccionEntrega" runat="server" 
                        Text="Debe seleccionar la dirección de entrega para el pedido"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblIdDireccionEntrega" runat="server" Style="display: none" 
                        Text=""></asp:Label>
                    <asp:Button ID="btnDireccion" runat="server" Height="16px" 
                        OnClientClick="ShowDirecciones();" Text="..." />
                </td>
            </tr>
            <tr>
                <td align="left" class="style3">
                    <span style="font-weight: bold; font-size: 12px">Líder:</span>
                </td>
                <td>
                    <asp:Label ID="lblResponsable" Text="" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="style4">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" class="style3">
                    <span style="font-weight: bold; font-size: 12px">Sub-Revendedor:</span>
                </td>
                <td>
                    <asp:Label ID="lblSubCoor" Text="" runat="server"></asp:Label>
                </td>
                <td align="left" colspan="4">
                    <span style="font-weight: bold; font-size: 12px">Ficha Revendedor:</span><asp:Image
                        ID="Image2" runat="server" ImageUrl="~/Imagenes/FichaTecnica.gif" />
                </td>
            </tr>
            <tr>
                <td align="left" class="style3">
                    <span style="font-weight: bold; font-size: 12px">Transporte:</span>
                </td>
                <td align="left">
                    
                    <asp:Label ID="lblTransporte" Text="" runat="server"></asp:Label>
                    
                </td>
                <td align="left">
                    
                    <span style="font-weight: bold; font-size: 12px">Forma Pago:</span></td>
                <td colspan="3">
                    <telerik:RadComboBox Skin="WebBlue" ID="cboFormaPago" runat="server" Width="230px"
                        AutoPostBack="false">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </telerik:RadComboBox>
                </td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
