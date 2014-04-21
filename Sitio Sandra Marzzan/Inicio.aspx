<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Inicio.aspx.cs" Inherits="Inicio" %>

<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="cc6" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="UCAvidoMail.ascx" TagName="UCAvidoMail" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inicio Sandra Marzzan</title>
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>    
</head>

<script type="text/javascript">

    function DescmarcarNodo(sender, eventArgs) {
        var node = eventArgs.get_node();
        node.unselect();
        node.unhighlight();
        node.set_expanded(!node.get_expanded());


    }

    function ShowAlertaPotencial() {

        window.setTimeout(function() {
            Show();
        }, 100);

    }

    function Show() {

        $find("<%=ServerControlWindow1.ClientID %>").set_CollectionDiv('divPrincipal');
        $find("<%=ServerControlWindow1.ClientID %>").ShowWindows('divPrincipal', "Pedido En Proceso");
        window.setTimeout(function() {
            $find("<%=ServerControlWindow1.ClientID %>").CloseWindows();

        }, 7000);
    }

    window.setTimeout(function() {
        if (parseInt("<%= CantMailsSinLeer %>") > 0)
            UCAvisoMail_ShowMessange("<%= CantMailsSinLeer %>");
    }, 1000);
    
</script>

<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
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
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Bookman Old Style"
                                Font-Size="28pt" ForeColor="White" Text="Bienvenido"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                                Font-Size="15pt" ForeColor="#155484" Text="A Sandra Marzzan"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblUsuario" runat="server" Font-Bold="false" Style="text-transform: capitalize"
                                Font-Names="Trebuchet MS" Font-Size="12pt" ForeColor="#155484" Text="Martin Cohn"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="Div1" style="position: absolute; top: 42%; left: 35%;" runat="server">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td rowspan="2" style="background-image: url('Imagenes/CornerSuperior2.gif'); background-repeat: no-repeat;
                    background-position: right top" colspan="3">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td class="style6" style="width: 30px; background-image: url('Imagenes/bordeIz.gif');
                    background-repeat: repeat-y; background-position: left bottom">
                </td>
                <td align="left">
                    <telerik:RadTreeView ID="TreeMenu" runat="server"  Skin="Vista"
                        OnClientNodeClicked="DescmarcarNodo"  >
                    </telerik:RadTreeView>
                </td>
                <td style="background-image: url('Imagenes/bordeDe.gif'); background-repeat: repeat-y;
                    background-position: right top">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2" rowspan="2" style="background-image: url('Imagenes/CornerInferior2.gif');
                    background-repeat: no-repeat; background-position: left bottom" colspan="3">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <cc6:ServerControlWindow ID="ServerControlWindow1" runat="server" BackColor="WhiteSmoke"
        WindowColor="Azul">
        <ContentControls>
            <div id="divPrincipal" style="height: 120px; width: 350px">
                <table id="Table1" runat="server" cellpadding="0" border="0" cellspacing="0" style="font-family: Sans-Serif;
                    font-size: 13px; font-weight: bold; text-align: center; color: Black; width: 100%;
                    height: 120px">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblProducto" runat="server" Text="Por el momento no puede realizar otro pedido, su usuario sera habilitado para esta operación por el personal de asistencia a la brevedad."></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentControls>
    </cc6:ServerControlWindow>
    <uc1:UCAvidoMail ID="UCAvidoMail1" runat="server" />
    </form>
</body>
</html>
