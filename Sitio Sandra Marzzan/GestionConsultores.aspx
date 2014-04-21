<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="GestionConsultores.aspx.cs" Inherits="GestionConsultores" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
            text-align:right;
            padding-right:5px;
        }
        #tblNuevaDireccino
        {
            width: 530px;
        }
    </style>
  
</head>
  <script type="text/javascript">
      function ShowNuevaDireccion() {
          var consultor = $find("cboConsultores").get_text();
          var idconsultor = $find("cboConsultores").get_value();

          if (idconsultor > 0)
              oWnd = radopen("GestionDireccionesEntrega.aspx?Consultor=" + consultor + "&IdConsultor=" + idconsultor, "RadWindow1");
          else
              radalert("Debe seleccionar un revendedor para realizar la acción solicitada.");

      }
      function ShowEditDireccion() {
          var consultor = $find("cboConsultores").get_text();
          var idconsultor = $find("cboConsultores").get_value();

          if (idconsultor > 0) {

              var grid = $find("<%=gvDirEntrega.ClientID %>");
              var MasterTable = grid.get_masterTableView();

              if (MasterTable.get_selectedItems().length == 1) {
                  var selectedRow = MasterTable.get_selectedItems()[0];
                  oWnd = radopen("GestionDireccionesEntrega.aspx?Consultor=" + consultor + "&IdDireccion=" + selectedRow.getDataKeyValue("IdDireccion") + "&IdConsultor=" + idconsultor, "RadWindow1");
              }
              else {
                  radalert("Debe seleccionar una dirección de entrega para realizar la acción solicitada.");
              }
          }
          else
              radalert("Debe seleccionar un revendedor para realizar la acción solicitada.");
              
      }

      function OnClientClose(oWnd) {


          if (oWnd.argument) {
              if (oWnd.argument.SendMail == true) {
                  confirmarSolicitud();
                  oWnd.BrowserWindow.location.reload();
              }
              else
                  radalert("La solicitud no se ha podido completar, no existe un destinitario para enviar el mail. Por favor informe a asistencia del error.");
          }
      }
      
      function ShowFailSend() {
          radalert("La solicitud no se ha podido completar, no existe un destinitario para enviar el mail. Por favor informe a asistencia del error.");
      }
      
      function confirmarSolicitud()
      {
            radalert("La solicitud de envio de informacion se ha realizado con exito.");
      }
    </script>
<body style="background-image: url(Imagenes/repetido.jpg); margin-top:1px; background-repeat:repeat-x;background-color:White;"  >
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="true" VisibleStatusbar="false"
        ReloadOnShow="false" runat="server" Skin="WebBlue">
        <Windows>
            <telerik:RadWindow runat="server" ID="RadWindow1" Behaviors="Close" Width="540" Height="320px" Modal="true"
            OnClientClose="OnClientClose" NavigateUrl="BusquedaLider.aspx" ReloadOnShow="true" >
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>   
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
                            
            </td>
        </tr>
        <tr>
            <td align="left">
                    <table width="100%" border="0" >
                        <tr>
                            <td colspan="2">
                                <table width="100%" border="0" >
                                    <tr>
                                        <td align="right" style="width: 45%">
                                            <asp:Label ID="label10" runat="server" SkinID="lblBlue">Revendedores Existentes:</asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:UpdatePanel ID="upCbo" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:LinqDataSource ID="LinqDataSource3" runat="server" ContextTypeName="CommonMarzzan.Marzzan_InfolegacyDataContext"
                                                        TableName="Clientes" Where="Padre.value == @Usuario " OrderBy="Nombre">
                                                        <WhereParameters>
                                                            <asp:SessionParameter Name="Usuario" SessionField="IdUsuario" Type="Int64" />
                                                        </WhereParameters>
                                                    </asp:LinqDataSource>
                                                    
                                                    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="CommonMarzzan.Marzzan_InfolegacyDataContext"
                                                        TableName="Direcciones" Select="" Where="ObjCliente.CodigoExterno == @Cli" EnableInsert="True"
                                                        EnableUpdate="True">
                                                        <WhereParameters>
                                                            <asp:ControlParameter ControlID="cboConsultores" Type="String" Name="Cli"
                                                                PropertyName="SelectedValue" />
                                                        </WhereParameters>
                                                    </asp:LinqDataSource>
                                                    
                                                    <telerik:RadComboBox Skin="Vista" ID="cboConsultores" runat="server" Width="360px"
                                                        AutoPostBack="True" OnSelectedIndexChanged="cboConsultores_SelectedIndexChanged"
                                                        CausesValidation="false" AllowCustomText="true" MarkFirstMatch="true">
                                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                                    </telerik:RadComboBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%">
                                <asp:UpdatePanel ID="upDatos" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" style="Height:352px;border:1px solid;border-color:#000 #75a4b7 #000;">
                                            <tr>
                                                <td class="style2" style="padding-left:5px;">
                                                    <asp:Label ID="label1" runat="server" SkinID="lblBlue">Nombre Completo:</asp:Label>
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
                                                <td class="style2" style="padding-left:5px;">
                                                    <asp:Label ID="label2" runat="server" SkinID="lblBlue">Fecha Nacimiento:</asp:Label>
                                                </td>
                                                <td align="left" >
                                                    <telerik:RadDatePicker ID="txtFechaNacimiento" runat="server" Skin="Web20" Width="138px"
                                                        Culture="Spanish (Argentina)" MinDate="01/01/1910" >
                                                        
                                                        <DateInput ID="DateInput1" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Web20" Skin="WebBlue"
                                                            SelectionOnFocus="SelectAll" ShowButton="True" >
                                                        </DateInput>
                                                        <Calendar ID="Calendar1" runat="server" Skin="WebBlue" >
                                                        </Calendar>
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFechaNacimiento"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2" style="padding-left:5px;">
                                                    <asp:Label ID="label9" runat="server" SkinID="lblBlue">DNI:</asp:Label>
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
                                                <td class="style2" style="padding-left:5px;">
                                                    <asp:Label ID="label7" runat="server" SkinID="lblBlue">Telefono Fijo:</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadMaskedTextBox Width="256px" ID="txtTelFijo" runat="server" EmptyMessage="Ingrese su tel. con el código de área "
                                                        InvalidStyleDuration="100" Mask="(####) - #######" Skin="WebBlue">
                                                    </telerik:RadMaskedTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2" style="padding-left:5px;">
                                                    <asp:Label ID="label3" runat="server" SkinID="lblBlue">Telefono Cel.:</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadMaskedTextBox Width="256px" ID="txtTelCel" runat="server" EmptyMessage="Ingrese su tel. con el código de área "
                                                        InvalidStyleDuration="100" Mask="(####) - #########" Skin="WebBlue">
                                                    </telerik:RadMaskedTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2" style="padding-left:5px;">
                                                    <asp:Label ID="label4" runat="server" SkinID="lblBlue">E-mail:</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadTextBox Width="256px" ID="txtEmail" runat="server" EmptyMessage="Ingrese su correo electrónico"
                                                        InvalidStyleDuration="100" MaxLength="100" Skin="WebBlue">
                                                    </telerik:RadTextBox>
                                                    <asp:RegularExpressionValidator ID="regEmail" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        Display="Dynamic" ForeColor="Red" font-name="Arial" Font-Size="11" runat="server">*</asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtEmail"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr >
                                                <td colspan="2" align="center" valign="bottom">
                                                <asp:UpdatePanel  ID="upDatosPersonales" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div style="vertical-align: middle; height: 26px; background: #4e96aa 0 -2099px repeat-x url('Imagenes/sprite_vista.gif');
                                                            border: 1px solid; border-color: #000 #75a4b7 #000;">
                                                            <img style="padding-right: 5px; border: 0px; vertical-align: middle;" alt="sss" src="Imagenes/Edit.gif" />
                                                            <asp:LinkButton ID="btnModificacionDatos" runat="server" CausesValidation="true"
                                                                ForeColor="White" Style="text-decoration: none; vertical-align: middle; font: 12px/16px 'segoe ui',arial,sans-serif;"
                                                                OnClick="btnModificacionDatos_Click">
                                                                Enviar Modificación 
                                                            </asp:LinkButton>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                          
                                          
                                          
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td valign="top">
                                <asp:UpdatePanel ID="upDirecciones" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        
                                        
                                        <telerik:RadGrid ID="gvDirEntrega" runat="server" Skin="Vista" AutoGenerateColumns="False"
                                            Height="350px"  AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
                                            GridLines="None" >
                                            <MasterTableView EditMode="PopUp" TableLayout="Fixed" CommandItemDisplay="Bottom" 
                                                DataKeyNames="IdDireccion" ClientDataKeyNames="IdDireccion" NoDetailRecordsText="No posee direcciones de entrega"
                                                NoMasterRecordsText="Debe Seleccionar un revendedor para ver las direcciones" >
                                                <CommandItemTemplate>
                                                    <div style="padding: 5px 5px;">
                                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="ShowNuevaDireccion(); return false;" CommandName="InitInsert" CausesValidation="true">
                                                    <img style="padding-right:5px;border:0px;vertical-align:middle;" alt="sssa" src="Imagenes/AddRecord.gif" />Nueva
                                                        </asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="btnEditSelected" runat="server" CommandName="EditSelected" CausesValidation="false" OnClientClick="ShowEditDireccion(); return false;">
                                                    <img style="padding-right:5px;border:0px;vertical-align:middle;" alt="sss" src="Imagenes/Edit.gif" />Modificar
                                                        </asp:LinkButton>
                                                    </div>
                                                </CommandItemTemplate>
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Direcciones de Entrega">
                                                        <ItemTemplate>
                                                            <table width="100%" border="1" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="style2">
                                                                        <asp:Label ID="label8" runat="server" SkinID="lblBlue">Provincia:</asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblProvincia" runat="server" SkinID="lblBlack"><%# Eval("Provincia")%></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style2">
                                                                        <asp:Label ID="label19" runat="server" SkinID="lblBlue">Departamento:</asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="label20" runat="server" SkinID="lblBlack"><%# Eval("Departamento")%></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style2">
                                                                        <asp:Label ID="label13" runat="server" SkinID="lblBlue">Localidad:</asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="label14" runat="server" SkinID="lblBlack"><%# Eval("Localidad")%></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style2">
                                                                        <asp:Label ID="label15" runat="server" SkinID="lblBlue">Calle:</asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="label16" runat="server" SkinID="lblBlack"><%# Eval("Calle")%></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style2">
                                                                        <asp:Label ID="label17" runat="server" SkinID="lblBlue">Codigo Posta:</asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="label18" runat="server" SkinID="lblBlack"><%# Eval("CodigoPostal")%></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <table width="100%" border="1" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="style2">
                                                                        <asp:Label ID="label8" Width="100%" runat="server" SkinID="lblBlue">Provincia:</asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="ProvinciaTextBox" Width="100%"  runat="server" Text='<%# Bind("Provincia") %>'></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style2">
                                                                        <asp:Label ID="label19"  Width="100%" runat="server" SkinID="lblBlue">Departamento:</asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="TextBox1"  Width="100%" runat="server" Text='<%# Bind("Departamento") %>'></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style2">
                                                                        <asp:Label ID="label13" Width="100%" runat="server" SkinID="lblBlue">Localidad:</asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="TextBox2"  Width="100%" runat="server" Text='<%# Bind("Localidad") %>'></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style2">
                                                                        <asp:Label ID="label15" runat="server" SkinID="lblBlue">Calle:</asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="TextBox3" Width="100%" runat="server" Text='<%# Bind("Calle") %>'></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style2">
                                                                        <asp:Label ID="label17" runat="server" SkinID="lblBlue">Codigo Posta:</asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("CodigoPostal") %>'></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr style="display:none">
                                                                    <td class="style2"  >
                                                                        <asp:Label ID="label21" runat="server" SkinID="lblBlue">Revendedor:</asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtIdCliente" runat="server" Text='<%# Bind("Cliente") %>'></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditItemTemplate>
                                                        
                                                    </telerik:GridTemplateColumn>
                                                    
                                                </Columns>
                                                <EditFormSettings ColumnNumber="1" CaptionFormatString="Edición"  >
                                                    <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                                    <FormMainTableStyle GridLines="Both" CellSpacing="0" CellPadding="3" BackColor="White"
                                                        Width="100%" />
                                                    <FormTableStyle CellSpacing="0" CellPadding="2" Width="100%" Height="110px" BackColor="White" />
                                                    <FormStyle Width="100%" BackColor="#eef2ea"></FormStyle>
                                                    <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                                                    <EditColumn ButtonType="ImageButton" InsertText="Insertar" UpdateText="Actualizar"
                                                        UniqueName="EditCommandColumn1" CancelText="Cancelar" >
                                                    </EditColumn>
                                                    <FormTableButtonRowStyle HorizontalAlign="Right"></FormTableButtonRowStyle>
                                                    <PopUpSettings ScrollBars="Auto" Modal="true" Width="45%" />
                                                </EditFormSettings>
                                                
                                                <RowIndicatorColumn>
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn>
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </ExpandCollapseColumn>
                                            </MasterTableView>
                                           <ValidationSettings CommandsToValidate="PerformInsert,Update" />
                                            <ClientSettings >
                                                <Selecting AllowRowSelect="true" />
                                                <Scrolling AllowScroll="true"  UseStaticHeaders="true" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                       
                    </table>
                
                  
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50" >
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
            </td>
        </tr>
    </table>

    </form>
</body>
</html>
