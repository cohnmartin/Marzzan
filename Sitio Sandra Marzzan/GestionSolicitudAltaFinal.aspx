<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="GestionSolicitudAltaFinal.aspx.cs" Inherits="GestionSolicitudAltaFinal" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestion Futuros revendedores de Derivaciones Sandra Marzzan</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <link href="MySkin/ToolTip.MySkin.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 90%;
        }
    </style>
<script type="text/javascript">

    // this method is invoked from the flash animation
    function CloseToolTip(argument, validationGroupName) {

        if (typeof (Page_ClientValidate) == 'function') {
            var validationResult = Page_ClientValidate(validationGroupName);

            if (validationResult == true) {
                var tooltip = $find(argument);
                tooltip.hide();
                return true;
            }
        }

       
    }
    
    function PopupOpening(sender, args) {
        Telerik.Web.UI.Calendar.Popup.zIndex = 100100;
    }

    function ControlarSeleccion() {

        if ($find("gvUsuarios").get_masterTableView().get_selectedItems().length > 0) {
            return true;
        }
        else {

            $find("tooltipSeleccionElemento").show();
            return false;
        }
    }
    function OpenNotificacion() {
        $find("toolTipNotificacion").show();
    }
</script>

</head>
<body style="background-image: url(Imagenes/repetido.jpg); margin-top:1px; background-repeat:repeat-x;background-color:White;"  >
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    

    
     <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="Header_panelContainerSimple">
                    <div class="CabeceraInicial">
                        <asp:Button ID="btnVolver" runat="server" Text="" SkinID="btnVolver"
                         OnClick="btnVolver_Click" Visible="false"  />
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td align="center">
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahomas" 
                    Font-Size="16px" ForeColor="#0066CC" Height="59px" 
                    Text="El listado que se muestra a continuación posee los usuario que se han postulado
                        para ser revendedores, debe seleccionar cada uno de ellos completar el procedimiento para gestionar el
                        alta como usuario definitivos" 
                    Width="606px"></asp:Label>
                    
                
            </td>
        </tr>
        <tr>
            <td align="center">
                <div class="PageContent">
                                
                    <table class="style1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="upGrila" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <telerik:RadGrid ID="gvUsuarios" runat="server" GridLines="None" Skin="WebBlue" DataSourceID="LinqDataSource1">
                                            <MasterTableView AutoGenerateColumns="False" DataSourceID="LinqDataSource1" DataKeyNames="IdUsurioAltaTemprana,Provincia"
                                                ClientDataKeyNames="IdUsurioAltaTemprana,Provincia">
                                                <RowIndicatorColumn Visible="False">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn Visible="False" Resizable="False">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="IdUsurioAltaTemprana" HeaderText="" ReadOnly="True"
                                                        Visible="false" SortExpression="IdUsurioAltaTemprana" UniqueName="IdUsurioAltaTemprana">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="NombreCompleto" HeaderText="Nombre Completo"
                                                        ReadOnly="True" SortExpression="NombreCompleto" UniqueName="NombreCompleto">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="FechaAlta" DataType="System.DateTime" HeaderText="Fecha Alta"
                                                        ReadOnly="True" SortExpression="FechaAlta" UniqueName="FechaAlta" DataFormatString="{0:dd/MM/yyyy}">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="objProvincia.Descripcion" HeaderText="Provincia"
                                                        ReadOnly="True" SortExpression="Provincia" UniqueName="Provincia">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Departamento" HeaderText="Departamento" ReadOnly="True"
                                                        SortExpression="Departamento" UniqueName="Departamento">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Direccion" HeaderText="Direccion" ReadOnly="True"
                                                        SortExpression="Direccion" UniqueName="Direccion">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TelefonoFijo" HeaderText="Teléfono Fijo"
                                                        ReadOnly="True" SortExpression="TelefonoFijo" UniqueName="TelefonoFijo">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TelefonoCelular" HeaderText="Teléfono Celular"
                                                        ReadOnly="True" SortExpression="TelefonoCelular" UniqueName="TelefonoCelular">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Email" HeaderText="Correo Electrónico"
                                                        ReadOnly="True" SortExpression="Email" UniqueName="Email">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="FechaContacto" DataType="System.DateTime" HeaderText="Fecha Contacto"
                                                        ReadOnly="True" SortExpression="FechaContacto" UniqueName="FechaContacto" DataFormatString="{0:dd/MM/yyyy}">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <EditFormSettings>
                                                    <PopUpSettings ScrollBars="None"></PopUpSettings>
                                                </EditFormSettings>
                                            </MasterTableView>
                                            <ClientSettings>
                                                <Selecting AllowRowSelect="True" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    
                                            <telerik:RadToolTip ID="tooltipSeleccionElemento" runat="server" ShowEvent="FromCode"
                                                TargetControlID="gvUsuarios" ShowCallout="false" Sticky="false" Animation="Resize"
                                                Text="Debe Seleccionar un elemento del listado para poder realizar la operacion solicitada."
                                                Skin="Web20" Position="Center" Width="330" ManualClose="true" Title="Falta Seleccion"
                                                IsClientID="true">
                                            </telerik:RadToolTip>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <table width="28%" border="0" cellpadding="0" cellspacing="0">
                                                <tr style="height: 25px;">
                                                    <td style="vertical-align: middle" align="left">
                                                        <asp:UpdatePanel ID="upEditar" UpdateMode="Conditional" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnEditar" runat="server" SkinID="btnBasic"
                                                                    Text="Editar Datos" CausesValidation="false"
                                                                    OnClick="btnEditar_Click" OnClientClick="return ControlarSeleccion();" />
                                                                    
                                                                <asp:UpdateProgress ID="UpdateProgress4" runat="server" DisplayAfter="50" AssociatedUpdatePanelID="upEditar">
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
                                                                                        <asp:Label ID="lbltituloddpaciente1" runat="server" Font-Bold="True" Font-Names="Thomas"
                                                                                            Font-Size="12px" ForeColor="Black" Height="21px" Style="vertical-align: middle"
                                                                                            Text="Cargando Datos...">
                                                                                        </asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </ProgressTemplate>
                                                                </asp:UpdateProgress>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table width="28%" border="0" cellpadding="0" cellspacing="0">
                                                <tr style="height: 25px;">
                                                    <td style="vertical-align: middle" align="left">
                                                        <asp:UpdatePanel ID="upNotificar" UpdateMode="Conditional" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnNotificar" runat="server" SkinID="btnBasic"
                                                                    Text="Notificar Contacto" CausesValidation="false"
                                                                    OnClientClick="if (ControlarSeleccion()){OpenNotificacion();}" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table width="28%" border="0" cellpadding="0" cellspacing="0" >
                                                <tr style="height: 25px;">
                                                    <td style="vertical-align: middle" align="left">
                                                        <asp:UpdatePanel ID="upSolicitar" UpdateMode="Conditional" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnSolicitar" runat="server" SkinID="btnBasic"
                                                                    Text="Solicitar Alta" CausesValidation="false"
                                                                    OnClick="btnSolicitar_Click" OnClientClick="return ControlarSeleccion();" />
                                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="50" AssociatedUpdatePanelID="upSolicitar">
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
                                                                                        <asp:Label ID="lbltitulopacifdente1" runat="server" Font-Bold="True" Font-Names="Thomas"
                                                                                            Font-Size="12px" ForeColor="Black" Height="21px" Style="vertical-align: middle"
                                                                                            Text="Solicitando Alta...">
                                                                                        </asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </ProgressTemplate>
                                                                </asp:UpdateProgress>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            
                               
                            </td>
                        </tr>
                        
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="CommonMarzzan.Marzzan_InfolegacyDataContext" 
        TableName="UsuariosAltaTempranas" Where="LiderDerivado != Null">
        
    </asp:LinqDataSource>
    
    <asp:UpdatePanel ID="upToolTip" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
        
                
            <telerik:RadToolTip runat="server" ID="toolTipEdicin" Skin="Vista" Sticky="true"
                ManualClose="true" Width="400px" Height="280px" Animation="Fade" Position="Center"
                RelativeTo="BrowserWindow" ShowEvent="FromCode" >
              
        
                <table width="100%" border="0" style="height: 100%; background-color: White">
                    <tr>
                        <td class="style2">
                            <asp:Label ID="label2" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                font-size: 11px">Nombre Completo:</asp:Label>
                        </td>
                        <td align="left">
                            <telerik:RadTextBox Width="256px" ID="txtNombre" runat="server" EmptyMessage="Ingrese su nombre completo "
                                InvalidStyleDuration="100" MaxLength="250" Skin="WebBlue">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ValidationGroup="Edicion" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="label3" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                font-size: 11px">Fecha Nacimiento</asp:Label>
                        </td>
                        <td align="left">
                            <telerik:RadDatePicker ID="DpFechaNacimiento" Runat="server" Skin="Web20" ClientEvents-OnPopupOpening="PopupOpening"
                                          Width="138px" Culture="Spanish (Argentina)" MinDate="01/01/1900">
                                        <DateInput InvalidStyleDuration="100" LabelCssClass="radLabelCss_Web20" Skin="Web20" SelectionOnFocus="SelectAll" 
                                              ShowButton="True"></DateInput>
                                          <Calendar Skin="Web20">
                                          </Calendar>
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ValidationGroup="Edicion"  ID="RequiredFieldValidator2" runat="server" ControlToValidate="DpFechaNacimiento"
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
                            <asp:RequiredFieldValidator  ValidationGroup="Edicion" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDni"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="label4" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                font-size: 11px">Provincia:</asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblProvincia" runat="server" Style="width: 100px; color: black;
                                font-family: Sans-Serif; font-size: 12px"></asp:Label>
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
                            <asp:RequiredFieldValidator  ValidationGroup="Edicion" ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDireccion"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="label10" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                font-size: 11px">Email</asp:Label>
                        </td>
                        <td align="left">
                            <telerik:RadTextBox Width="256px" ID="txtEmail" runat="server" EmptyMessage="Ingrese su correo electrónico"
                                InvalidStyleDuration="100" MaxLength="100" Skin="WebBlue">
                            </telerik:RadTextBox>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="style2">
                            <asp:Label ID="label7" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                font-size: 11px">Telefono Fijo</asp:Label>
                        </td>
                        <td align="left">
                            <telerik:RadTextBox Width="256px" ID="txtTelFijo" runat="server" EmptyMessage="Ingrese su tel. con el código de área "
                                InvalidStyleDuration="100" MaxLength="100" Skin="WebBlue">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="label8" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                font-size: 11px">Teléfono Celular:</asp:Label>
                        </td>
                        <td align="left">
                            <telerik:RadMaskedTextBox Width="256px" ID="txtTelCel" runat="server" EmptyMessage="Ingrese su tel. con el código de área "
                                InvalidStyleDuration="100"  ValidationGroup="Edicion" Mask="(####) - 15#######" Skin="WebBlue">
                            </telerik:RadMaskedTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTelCel"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <table width="28%" border="0" cellpadding="0" cellspacing="0" >
                                <tr style="height: 25px;">
                                    <td style="vertical-align: middle" align="left">
                                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnGuardar" runat="server" SkinID="btnBasic"
                                                    Text="Guardar" CausesValidation="true" ValidationGroup="Edicion"
                                                    onclick="btnGuardar_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        
                        </td>
                    </tr>
                </table>
            </telerik:RadToolTip>
          
        </ContentTemplate>
    </asp:UpdatePanel>
    
    
    <asp:UpdatePanel ID="upNotificacion" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
        
                
            <telerik:RadToolTip runat="server" ID="toolTipNotificacion" Skin="MySkin" Sticky="true"
                ManualClose="true" Width="400px" Height="220px"  Animation="Fade" Position="Center"
                RelativeTo="BrowserWindow" ShowEvent="FromCode" EnableEmbeddedSkins="false"
                Title="Notificación de Contacto" >
              
        
                <table width="100%" border="0" style="height: 100%; background-color: White">
                    <tr>
                        <td class="style2">
                            <asp:Label ID="label12" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                font-size: 11px">Fecha Contacto:</asp:Label>
                        </td>
                        <td align="left">
                            <telerik:RadDatePicker ClientEvents-OnPopupOpening="PopupOpening" ID="DpFechaContacto"
                                runat="server" Skin="WebBlue" MinDate="1900-01-01">
                                <DateInput ID="DateInput2" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                </DateInput>
                                <Calendar Skin="WebBlue">
                                </Calendar>
                                <DatePopupButton CssClass="radPopupImage_WebBlue"></DatePopupButton>
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="Notificacion" runat="server" ControlToValidate="DpFechaContacto"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="label11" runat="server" Style="width: 100px; color: #0066CC; font-family: Sans-Serif;
                                font-size: 11px">Comentario:</asp:Label>
                        </td>
                        <td align="left">
                            <telerik:RadTextBox Width="256px" ID="txtComentirioNotificacion" runat="server" EmptyMessage="Ingrese comentario del contacto "
                                InvalidStyleDuration="100" MaxLength="250" Skin="WebBlue" TextMode="MultiLine" Rows="7">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ValidationGroup="Notificacion" ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtComentirioNotificacion"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnGuardarNotificacion" runat="server" SkinID="btnBasic" Text="Notificar"
                                        CausesValidation="true" ValidationGroup="Notificacion" OnClick="btnGuardarNotificacion_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        
                        </td>
                    </tr>
                </table>
            </telerik:RadToolTip>
          
        </ContentTemplate>
    </asp:UpdatePanel>
   </form>
</body>
</html>
