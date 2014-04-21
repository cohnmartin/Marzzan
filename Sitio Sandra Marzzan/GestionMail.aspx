<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="GestionMail.aspx.cs" Inherits="GestionMail" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión de Mails</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
         div.wrapper
        {
        }
        .multiPage
        {
            border: 1px solid #94A7B5;
            background-color: Transparent;
            text-align: center;
            height: 320px;
        }
        </style>
</head>
<body style="background-image: url(Imagenes/repetido.jpg); margin-top:1px; background-repeat:repeat-x;background-color:White;"  >
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50" >
        <ProgressTemplate>
            <div class="progressBackgroundFilterBlue">
            </div>
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
                                Text="Actualizando Configuración ...">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
   
   
    <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="Header_panelContainerSimple">
                    <div class="CabeceraInicial">
                    </div>
                </div>
                <div class="CabeceraContent">
                    <table class="style1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="center">
                                <table cellpadding="0" cellspacing="5" style="width: 80%">
                                    <tr>
                                        <td align="center" style="height: 25px; background: url('imagenes/sprite_webBlue.gif') 0  -300px repeat-x">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="14pt" Font-Names="Sans-Serif"
                                                ForeColor="Gray" Text="Gestión de Mails" Width="378px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="margin-left: 40px">
                            
                                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Web20" 
                                    MultiPageID="RadMultiPage1" SelectedIndex="0" Align="Center">
                                    <Tabs>
                                        <telerik:RadTab runat="server" Text="Líderes" Selected="True" PageViewID="viewCoordinadores">
                                        </telerik:RadTab>
                                        <telerik:RadTab runat="server" Text="General" PageViewID="viewGeneral" Visible="false">
                                        </telerik:RadTab>
                                    </Tabs>
                                </telerik:RadTabStrip>
                            
                            </td>
                        </tr>
                        <tr>
                            <td align="center" >
                            
                                
                                <telerik:RadMultiPage ID="RadMultiPage1" Runat="server" Width="70%" SelectedIndex="0">
                                    <telerik:RadPageView ID="viewCoordinadores" runat="server" CssClass="multiPage" >
                                        
                                        <asp:UpdatePanel ID="upCoord" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table class="style1" border="0">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="Label5" SkinID="lblBlue" runat="server" Text="Líderes"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <telerik:RadComboBox ID="cboConsultores" runat="server" Skin="Vista" Width="370px" EmptyMessage="Escriba el nombre del líder a buscar"
                                                                EnableLoadOnDemand="true" OnItemsRequested="cboConsultores_ItemsRequested" ToolTip="Escriba el nombre del líder a buscar"
                                                                AllowCustomText="true" MarkFirstMatch="true" AutoPostBack="True" OnSelectedIndexChanged="cboConsultores_SelectedIndexChanged">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="Label6" SkinID="lblBlue" runat="server" Text="Dirección Destino"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <telerik:RadTextBox ID="txtMailCoordinador" runat="server" EmptyMessage="Ingrese la dirección de asistencia"
                                                                Skin="Vista" Width="270px">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="btnGrabar" runat="server" OnClick="btnGrabar_Click" Text="Grabar"
                                                                SkinID="btnBasic" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                       
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="viewGeneral" Runat="server" CssClass="multiPage" >
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table class="style1" border="0">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="Label1" SkinID="lblBlue" runat="server" Text="Dirección Origen"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <telerik:RadTextBox ID="txtDireccion" runat="server" EmptyMessage="Ingrese la dirección del correo saliente"
                                                                Skin="Vista" Width="270px">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="Label2" SkinID="lblBlue" runat="server" Text="Usuario"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <telerik:RadTextBox ID="txtUsuario" runat="server" EmptyMessage="Ingrese el usuario del correo saliente"
                                                                Skin="Vista" Width="270px">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="Label3" SkinID="lblBlue" runat="server" Text="Contraseña"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <telerik:RadTextBox ID="txtPassword" runat="server" EmptyMessage="Ingrese la contraseña del usuario"
                                                                Skin="Vista" Width="270px" TextMode="Password">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="Label7" SkinID="lblBlue" runat="server" Text="SMTP"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <telerik:RadTextBox ID="txtSMTP" runat="server" EmptyMessage="Ingrese la dirección del servidor de correo"
                                                                Skin="Vista" Width="270px" >
                                                            </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="btnGrabarGeneral" runat="server" Text="Grabar"
                                                                SkinID="btnBasic" onclick="btnGrabarGeneral_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </telerik:RadPageView>
                                </telerik:RadMultiPage>
                            
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
