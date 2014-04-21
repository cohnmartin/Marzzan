<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BusquedaLider.aspx.cs" Inherits="BusquedaLider" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 36%;
        }
    </style>
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }
        function returnToParent(value) {
          
            //create the argument that will be returned to the parent page
            var oArg = new Object();

            oArg.selCoor = value;
     
            //get a reference to the RadWindow
            var oWnd = GetRadWindow();

            //set the argument to the RadWindow 
            oWnd.argument = oArg;
            //close the RadWindow
            if (value != "") {
                oWnd.close();
            }
            else {
                alert("Debe seleccionar un líder o sub líder para realizar la asiganción");
            }
        }
       

    </script>
</head>
<body style="background-image: url(Imagenes/repetido.jpg); margin-top:1px; background-repeat:repeat-x;background-color:White;"  >
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div>
    
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td align="center">
                    <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Names="Tahomas" 
                    Font-Size="16px" ForeColor="#0066CC" Height="44px" 
                    Text="Líderes o Sub Líderes de la provincia" 
                    Width="363px"></asp:Label></td>
            </tr>
            <tr>
                <td align="center">
                    <telerik:RadComboBox ID="cboCoord" runat="server" Width="250px" Height="150px"
                        EmptyMessage="Escriba el nombre de un líder" 
                        EnableLoadOnDemand="True" ShowMoreResultsBox="false"
                        EnableVirtualScrolling="false"  Skin="WebBlue"  
                        onitemsrequested="cboCoord_ItemsRequested">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="Label1" runat="server"  Font-Names="Tahomas" 
                    Font-Size="16px" ForeColor="#0066CC" Height="58px" 
                    Text="Recuerde que al momento de realizar la asignación el líder o sub líder
                    será informado vía mail de dicha acción" 
                    Width="312px"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td align="center">
                    <table width="28%" border="0" cellpadding="0" cellspacing="0" style="border:1px solid gray">
                        <tr style="height:25px; background:url('Imagenes/sprite_webBlue.gif') 0 -300px repeat-x;" >
                            <td style="width: 20%" align="right" valign="bottom">
                                <asp:Image ID="Image1" runat="server" ImageUrl="Imagenes/RealizarAlta.gif" />
                            </td>
                            <td style="vertical-align:middle" align="left" >
                                <asp:Button ID="btnAsignar" runat="server" BackColor="Transparent" Style="cursor: hand;"
                                    BorderStyle="None" Height="18px" Font-Bold="true" Font-Names="Verdana" Font-Size="11px"
                                    ForeColor="black" Text="Asignar" Width="60px" 
                                    onclick="btnAsignar_Click"  />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
