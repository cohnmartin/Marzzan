<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DetalleProducto.ascx.cs" Inherits="DetalleProducto" %>
<style type="text/css">
    .style1
    {
        width: 508px;
    }
    .style2
    {
        width: 131px;
    }
    .style3
    {
        width: 7px;
    }
</style>
<table class="style1" style="border: 1px solid #0000FF">
    <tr>
        <td align="center" class="style2" rowspan="5">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/aqua.jpg" />
        </td>
        <td class="style3">
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" 
                Font-Size="10pt" Text="Producto:"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Tahoma" 
                Font-Size="10pt" Text="Descripcion:"></asp:Label>
        </td>
        <td rowspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Tahoma" 
                Font-Size="10pt" Text="Precio:"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblPrecio" runat="server" Font-Bold="True" Font-Names="Tahoma" 
                Font-Size="10pt" ForeColor="#3333CC" Text="$ 10.5"></asp:Label>
        </td>
    </tr>
</table>
