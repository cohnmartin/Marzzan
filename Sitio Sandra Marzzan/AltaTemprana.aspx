<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="AltaTemprana.aspx.cs" Inherits="AltaTemprana" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contactos Sandra Marzzan</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .style1
        {
            width: 50%;
            
        }
        .style2
        {
            width: 35%;
            text-align:left;
        }
    </style>
</head>
<body style="background-image: url(Imagenes/repetido.jpg); margin-top:1px; background-repeat:repeat-x;background-color:White;"  >
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
    <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="Header_panelContainerSimple">
                    <div class="CabeceraInicial">
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td align="center">
            <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Tahomas" 
                    Font-Size="16px" ForeColor="#0066CC" Height="39px" 
                    Text="Gracias por tomar contacto con nosotros, cuando termine de llenar el formulario
                        nos comunicaremos a la brevedad con usted." 
                    Width="606px"></asp:Label>
                
            </td>
        </tr>
        <tr>
            <td align="center">
                <div class="PageContent">
                    <table class="style1" border="0">
                        <tr>
                            <td align="center">
                                <asp:UpdatePanel ID="upDatos" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table width="100%" border="0">
                                            <tr>
                                                <td class="style2">
                                                    <asp:Label ID="label1" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                                        font-size: 11px">Nombre Completo:</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadTextBox Width="256px" ID="txtNombre" runat="server" EmptyMessage="Ingrese Apellido y Nombre"
                                                        InvalidStyleDuration="100" MaxLength="250" Skin="WebBlue">
                                                    </telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <asp:Label ID="label2" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                                        font-size: 11px">Fecha Nacimiento</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadDatePicker ID="DpFechaNacimiento" runat="server" Skin="Web20" Width="138px"
                                                        Culture="Spanish (Argentina)" >
                                                        
                                                        <DateInput runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Web20" Skin="WebBlue"
                                                            SelectionOnFocus="SelectAll" ShowButton="True" >
                                                        </DateInput>
                                                        <Calendar runat="server" Skin="WebBlue" >
                                                        </Calendar>
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DpFechaNacimiento"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <asp:Label ID="label9" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                                        font-size: 11px">DNI:</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadMaskedTextBox Width="256px" ID="txtDni" runat="server" EmptyMessage="Ingrese su número de documento "
                                                        InvalidStyleDuration="100" Mask="########" Skin="WebBlue" DisplayMask="##.###.###">
                                                    </telerik:RadMaskedTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDni"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <asp:Label ID="label3" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                                        font-size: 11px">Provincia:</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:LinqDataSource ID="LinqDataSource2" runat="server" ContextTypeName="CommonMarzzan.Marzzan_InfolegacyDataContext"
                                                        TableName="Clasificaciones" Where="Tipo == @Tipo">
                                                        <WhereParameters>
                                                            <asp:Parameter DefaultValue="Provincias" Name="Tipo" Type="String" />
                                                        </WhereParameters>
                                                    </asp:LinqDataSource>
                                                    <telerik:RadComboBox ID="cboProvincias" runat="server" Width="256px" EmptyMessage="Seleccione una Provincia"
                                                        Skin="WebBlue" DataSourceID="LinqDataSource2" DataTextField="Descripcion" DataValueField="IdClasificacion">
                                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cboProvincias"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <asp:Label ID="lblDepartamento" runat="server" Style="width: 100px; color: #0066CC;
                                                        font-family: Sans-Serif; font-size: 11px">Departamento:</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadTextBox Width="256px" ID="txtdepartamento" runat="server" EmptyMessage="Ingrese su departamento de recidencia "
                                                        InvalidStyleDuration="100" MaxLength="100" Skin="WebBlue">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <asp:Label ID="label5" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                                        font-size: 11px">Localidad:</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadTextBox Width="256px" ID="txtlocalidad" runat="server" EmptyMessage="Ingrese su localidad o partido "
                                                        InvalidStyleDuration="100" MaxLength="100" Skin="WebBlue">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <asp:Label ID="label6" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                                        font-size: 11px">Dirección:</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadTextBox Width="256px" ID="txtDireccion" runat="server" EmptyMessage="Ingrese su calle y numero "
                                                        InvalidStyleDuration="100" MaxLength="100" Skin="WebBlue">
                                                    </telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDireccion"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <asp:Label ID="label7" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                                        font-size: 11px">Telefono Fijo</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadMaskedTextBox Width="256px" ID="txtTelFijo" runat="server" EmptyMessage="Ingrese su tel. con el código de área "
                                                        InvalidStyleDuration="100" Mask="(####) - #######" Skin="WebBlue">
                                                    </telerik:RadMaskedTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <asp:Label ID="label8" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                                        font-size: 11px">Teléfono Celular:</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadMaskedTextBox Width="256px" ID="txtTelCel" runat="server" EmptyMessage="Ingrese su tel. con el código de área "
                                                        InvalidStyleDuration="100" Mask="(####) - 15#######" Skin="WebBlue">
                                                    </telerik:RadMaskedTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTelCel"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <asp:Label ID="label4" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                                        font-size: 11px">E-mail:</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadTextBox Width="256px" ID="txtEmail" runat="server" EmptyMessage="Ingrese su correo electrónico"
                                                        InvalidStyleDuration="100" MaxLength="100" Skin="WebBlue">
                                                    </telerik:RadTextBox>
                                                    <asp:RegularExpressionValidator ID="regEmail" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        Display="Dynamic" ForeColor="Red" font-name="Arial" Font-Size="11" runat="server">*</asp:regularexpressionvalidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtEmail"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <asp:Label ID="label10" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                                        font-size: 11px">Como Nos Comocio?</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="CommonMarzzan.Marzzan_InfolegacyDataContext"
                                                        Select="new (IdClasificacion, Descripcion)" TableName="Clasificaciones" Where="Tipo == @Tipo">
                                                        <WhereParameters>
                                                            <asp:Parameter DefaultValue="ComoConocio" Name="Tipo" Type="String" />
                                                        </WhereParameters>
                                                    </asp:LinqDataSource>
                                                    <telerik:RadComboBox ID="cboComo" runat="server" Width="256px" EmptyMessage="Seleccione una forma"
                                                        Skin="WebBlue" DataSourceID="LinqDataSource1" DataTextField="Descripcion" DataValueField="IdClasificacion">
                                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <asp:Label ID="label11" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                                        font-size: 11px">Quien lo Presento?</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadTextBox Width="256px" ID="txtPresento" runat="server" EmptyMessage="Ingrese el nombre de la persona que lo presento "
                                                        InvalidStyleDuration="100" MaxLength="100" Skin="WebBlue">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <asp:Label ID="label12" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                                        font-size: 11px">Comentario:</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadTextBox Width="256px" ID="txtComentario" runat="server" EmptyMessage="Deje su pregunta o comentario "
                                                        InvalidStyleDuration="100" MaxLength="255" Skin="WebBlue" TextMode="MultiLine"
                                                        Rows="6">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                       
                        <tr>
                            <td colspan="2" valign="middle" align="center" style="height: 22px;">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 50%" align="center">
                                            <asp:UpdatePanel ID="upAlta" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnAlta" runat="server" SkinID="btnBasic"
                                                        Text="Solicitar Alta" OnClick="btnAlta_Click" />
                                                    <telerik:RadToolTip ID="tooltipAlta" runat="server" ShowEvent="FromCode" TargetControlID="btnAlta"
                                                        ShowCallout="true" Sticky="true" Animation="Resize" Text="" Skin="WebBlue" Position="TopRight"
                                                        IsClientID="true">
                                                    </telerik:RadToolTip>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50" AssociatedUpdatePanelID="upAlta">
                        <ProgressTemplate>
                            <div id="divBloq1" class="progressBackgroundFilterBlack">
                            </div>
                            <div class="processMessage">
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
                                                Text="Realizando Solicitud....">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
