<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaginaParaPruebas.aspx.cs" Inherits="PaginaParaPruebas" %>

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
            height: 13px;
        }
    </style>
    <script type="text/javascript">
        function Limpiar() {
            document.getElementById("lblEstado").innerText = "";
        }
        function Limpiar0() {
            document.getElementById("lblEstado0").innerText = "";
        }

    </script>
</head>

<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <table class="style1">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Mail Destino:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Width="208px">cohn.martin@gmail.com</asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Mail Destino:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox8" runat="server" Width="208px">cohn.martin@gmail.com</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Mail Origen:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" Width="208px">webaltas@sandramarzzan.com.ar</asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Mail Origen:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox9" runat="server" Width="208px">usuariomarzzan@infolegacy.com.ar </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Asunto:</td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" Width="208px">Prueba Envio mail</asp:TextBox>
            </td>
            <td>
                Asunto:</td>
            <td>
                <asp:TextBox ID="TextBox10" runat="server" Width="208px" Text="nada"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Cuerpo:</td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server" Width="208px">nada</asp:TextBox>
            </td>
            <td>
                Cuerpo:</td>
            <td>
                <asp:TextBox ID="TextBox11" runat="server" Width="208px">nada</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                SMTP:</td>
            <td>
                <asp:TextBox ID="TextBox5" runat="server" Width="208px" >mail.sandramarzzan.com.ar</asp:TextBox>
            </td>
            <td>
                SMTP:</td>
            <td>
                <asp:TextBox ID="TextBox12" runat="server" Width="208px">mail.infolegacy.com.ar</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Usuario:</td>
            <td>
                <asp:TextBox ID="TextBox6" runat="server" Width="208px">webaltas@sandramarzzan.com.ar</asp:TextBox>
            </td>
            <td>
                Usuario:</td>
            <td>
                <asp:TextBox ID="TextBox13" runat="server" Width="208px">usuariomarzzan@infolegacy.com.ar </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Clave:</td>
            <td class="style2">
                <asp:TextBox ID="TextBox7" runat="server" Width="208px">123456789</asp:TextBox>
            </td>
            <td class="style2">
                Clave:</td>
            <td class="style2">
                <asp:TextBox ID="TextBox14" runat="server" Width="208px">Marzzan123</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2" colspan="2">
                <asp:CheckBox ID="CheckBox1" runat="server" Text="Conexion Segura" />
            </td>
            <td class="style2" colspan="2">
                <asp:CheckBox ID="CheckBox2" runat="server" Text="Conexion Segura" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnMail" runat="server" onclick="btnMail_Click" 
                    Text="Enviar Mail" Width="154px" OnClientClick="Limpiar();" />
            </td>
            <td align="center" colspan="2">
                <asp:Button ID="btnMail0" runat="server" onclick="btnMail0_Click" 
                    Text="Enviar Mail" Width="154px" OnClientClick="Limpiar0();" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Label ID="Label5" runat="server" Text="Estado:"></asp:Label>
                <asp:Label ID="lblEstado" runat="server" ></asp:Label>
            </td>
            <td align="center" colspan="2">
                <asp:Label ID="Label6" runat="server" Text="Estado:"></asp:Label>
                <asp:Label ID="lblEstado0" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
