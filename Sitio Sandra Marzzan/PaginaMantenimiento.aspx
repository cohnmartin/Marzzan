<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaginaMantenimiento.aspx.cs"
    Inherits="PaginaMantenimiento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mantenimiento Sandra Marzzan</title>
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>   
</head>
<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" style="width: 100%" border="0" align="left"
        runat="server" id="tblPrincipal">
        <tr>
            <td align="center" style="width: 100%; height: 20%">
                <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
                    <tr>
                        <td align="right" >
                            <div class="Header_panelContainerSimple">
                                <div class="CabeceraInicial">
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                     <td align="center" >
                        <img src="Imagenes/Trabajando.png" height="350px" width="400px" />
                     </td>
                       
                    </tr>
                    <tr>
                        <td align="center" style="padding-top:30px">
                            &nbsp;
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Bookman Old Style"
                                Font-Size="28pt" ForeColor="Black" Text="Disculpe las molestias, estamos trabajando para mejorar nuestro servicio."></asp:Label>
                            <br />
                            <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Bookman Old Style"
                                Font-Size="18pt" ForeColor="Black" Text="Volvemos en minutos..."></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
