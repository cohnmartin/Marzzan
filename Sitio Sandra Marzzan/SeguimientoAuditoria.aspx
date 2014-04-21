<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SeguimientoAuditoria.aspx.cs" Inherits="SeguimientoAuditoria" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <title>Seguimiento Sandra Marzzan</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
     
    <style type="text/css">
        .style1
        {
            width: 95%;
        }
        .style2
        {
            width: 382px;
        }
    </style>
<script src="FuncionesComunes.js" type="text/javascript"></script>
<script type="text/javascript">
    function ControlarSeleccion() {

        if ($find("gvUsuarios").get_masterTableView().get_selectedItems().length > 0) {
            return true;
        }
        else {

            $find("tooltipSeleccionElemento").show();
            return false;
        }
    }
    function ControlarR()
    {
        if (ControlarSeleccion()) {

            return blockConfirm('Esta seguro de rechazar al usuario?', event, 330, 100, '', 'Rechazo Usuario');
        }
        else
            return false;
    }
    
    function ControlarA() {
        if (ControlarSeleccion()) {

            return blockConfirm('Esta seguro de aprobar el usuario?', event, 330, 100, '', 'Aprobar Usuario');
        }
        else
            return false;
    }
    
</script>     
</head>



<body style="background-image: url(Imagenes/repetido.jpg); margin-top:1px; background-repeat:repeat-x;background-color:White;"  >
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <telerik:RadWindowManager id="Singleton" runat=server skin="WebBlue"></telerik:RadWindowManager>

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
                
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahomas" 
                    Font-Size="16px" ForeColor="#0066CC" Height="62px" 
                    Text="El listado que se muestra a continuación contiene los futuros revendedores que usted a derivado a un líder o sub líder y sobre los cuales podra ver el estado de la derivación" 
                    Width="606px"></asp:Label>
                
            </td>
        </tr>
        <tr>
            <td align="left" style="padding-left:20px;font-size:11px;font-family:Tahoma">
                
               
                <asp:CheckBox ID="chkPendiente" runat="server" Text="Pendientes" 
                    oncheckedchanged="chkPendiente_CheckedChanged" Checked="true" AutoPostBack="True" />
                <asp:CheckBox ID="chkAprobado" runat="server" Text="Aprobados" 
                    oncheckedchanged="chkPendiente_CheckedChanged" AutoPostBack="True" />
                <asp:CheckBox ID="chkRechazados" runat="server" Text="Rechazados" 
                    oncheckedchanged="chkPendiente_CheckedChanged" AutoPostBack="True" />
                
               
            </td>
        </tr>
        <tr>
            <td align="center" >
                <div class="PageContent">
                          
                    <table class="style1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="upGrila" UpdateMode="Conditional" runat="server" >
                                    <ContentTemplate>
                                        <telerik:RadGrid ID="gvUsuarios" runat="server" GridLines="None" Skin="WebBlue" DataSourceID="LinqDataSource1">
                                            <MasterTableView AutoGenerateColumns="False" 
                                             DataSourceID="LinqDataSource1" DataKeyNames="IdUsurioAltaTemprana"
                                             NoDetailRecordsText="No hay usuarios en los estados seleccionados"
                                             NoMasterRecordsText="No hay usuarios en los estados seleccionados">
                                                <RowIndicatorColumn Visible="False">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn Visible="False" Resizable="False">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="NombreCompleto" HeaderText="Nombre Completo"
                                                        ReadOnly="True" SortExpression="NombreCompleto" UniqueName="NombreCompleto" AllowFiltering="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="FechaAlta" DataType="System.DateTime" HeaderText="Fecha Alta"
                                                        ReadOnly="True" SortExpression="FechaAlta" UniqueName="FechaAlta" DataFormatString="{0:dd/MM/yyyy}">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="objProvincia.Descripcion" HeaderText="Provincia"
                                                        ReadOnly="True" SortExpression="Provincia" UniqueName="Provincia">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="objLiderDerivado.Nombre" HeaderText="Encargado Contacto"
                                                        ReadOnly="True" SortExpression="objLiderDerivado" UniqueName="objLiderDerivado">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="FechaDerivacion" HeaderText="Fecha Derivación"
                                                        ReadOnly="True" SortExpression="FechaDerivacion" UniqueName="FechaDerivacion"
                                                        DataFormatString="{0:dd/MM/yyyy}">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="FechaContacto" HeaderText="Fecha Contacto" ReadOnly="True"
                                                        SortExpression="FechaContacto" UniqueName="FechaContacto" DataFormatString="{0:dd/MM/yyyy}">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ComentarioContacto" HeaderText="Comentario Contacto"
                                                        ReadOnly="True" SortExpression="ComentarioContacto" UniqueName="ComentarioContacto">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="objEstado.Descripcion" HeaderText="Estado Usuario"
                                                        ReadOnly="True" SortExpression="objEstado" UniqueName="objEstado">
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
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="chkAprobado" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="chkPendiente" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="chkRechazados" EventName="CheckedChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td class="style2">
                                            <table width="28%" border="0" cellpadding="0" cellspacing="0" style="border: 1px solid gray">
                                                <tr style="height: 25px; background: url('Imagenes/sprite_webBlue.gif') 0 -300px repeat-x;">
                                                    <td style="width: 20%" align="right" valign="bottom">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="Imagenes/RealizarAlta.gif" />
                                                    </td>
                                                    <td style="vertical-align: middle" align="left">
                                                        <asp:UpdatePanel ID="upRechazar" UpdateMode="Conditional" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnRechazar" runat="server" BackColor="Transparent" Style="cursor: hand;"
                                                                    BorderStyle="None" Height="18px" Font-Bold="true" Font-Names="Verdana" Font-Size="11px"
                                                                    ForeColor="black" Text="Rechazar Usuario" Width="130px" CausesValidation="false"
                                                                    OnClientClick="return ControlarR();" 
                                                                    OnClick = "btnRechazar_Click" />
                               -                             </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table width="28%" border="0" cellpadding="0" cellspacing="0" style="border: 1px solid gray">
                                                <tr style="height: 25px; background: url('Imagenes/sprite_webBlue.gif') 0 -300px repeat-x;">
                                                    <td style="width: 20%" align="right" valign="bottom">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="Imagenes/RealizarAlta.gif" />
                                                    </td>
                                                    <td style="vertical-align: middle" align="left">
                                                        <asp:UpdatePanel ID="upAprobar" UpdateMode="Conditional" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnAprobar" runat="server" BackColor="Transparent" Style="cursor: hand;"
                                                                    BorderStyle="None" Height="18px" Font-Bold="true" Font-Names="Verdana" Font-Size="11px"
                                                                    ForeColor="black" Text="Aprobar Usuario" Width="120px" CausesValidation="false"
                                                                    OnClientClick="return ControlarA();"  
                                                                    onclick="btnAprobar_Click"  />
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
        ContextTypeName="DataClassesDataContext" 
        Select="new (IdUsurioAltaTemprana, NombreCompleto, FechaAlta, FechaContacto, objLiderDerivado, ComentarioContacto, FechaDerivacion, objProvincia, objEstado, Estado)" 
        TableName="UsuariosAltaTempranas" 
        Where="(LiderDerivado != Null) &amp;&amp;
((Estado=31 &amp;&amp; @chkPend ) || (Estado=32 &amp;&amp; @chkRech) || (Estado=33 &amp;&amp; @chkApro))">
        <WhereParameters>
            <asp:ControlParameter ControlID="chkPendiente" Name="chkPend" 
                PropertyName="Checked" />
            <asp:ControlParameter ControlID="chkAprobado" Name="chkApro" 
                PropertyName="Checked" />
            <asp:ControlParameter ControlID="chkRechazados" Name="chkRech" 
                PropertyName="Checked" />
        </WhereParameters>
        
    </asp:LinqDataSource>
    </form>
</body>
</html>
