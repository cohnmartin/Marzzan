<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="ConsultaDeRemitos.aspx.cs" Inherits="ConsultaDeRemitos" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Consultas De Remitos</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .style1
        {
            width: 95%;
        }
        .style2
        {
            width: 101px;
        }
        .style3
        {
            height: 8px;
        }
        .style4
        {
            height: 8px;
        }
        .style5
        {
            height: 8px;
            width: 233px;
        }
    </style>

    <script type="text/javascript">
        function requestStart1(sender, args) {
            if (args.get_eventTarget().indexOf("ExportExcel") > 0) {
                args.set_enableAjax(false);
            }
        }
    </script>

</head>
<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Web20" VisibleTitlebar="true"
        Title="Atención">
    </telerik:RadWindowManager>
    
    <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="Header_panelContainerSimple">
                    <div class="CabeceraInicial">
                    </div>
                </div>
                <div style="font: normal 12px Sans-serif,Arial, Verdana; color: black; margin-top: 5px;
                    margin-left: 10px; vertical-align: text-bottom; text-align: left;">
                    <table width="100%" border="0" cellspacing="0" style="border-style: ridge; border-width: thin">
                        <tr>
                            <td align="right" style="width: 180px;font-weight:bold;">
                                <asp:Label ID="UserNameLabel" SkinID="lblBlue" runat="server">Seleccione el grupo a consultar:</asp:Label>
                            </td>
                            <td align="left" style="width: 250px;">
                                <telerik:RadComboBox ID="cboConsultores" runat="server" Skin="WebBlue" Width="250px"
                                     CloseDropDownOnBlur="true" >
                                    <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                </telerik:RadComboBox>
                            </td>
                            <td align="right" >
                                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" SkinID="btnBasic" 
                                    OnClick="btnConsultar_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="color: #993300; font-family: Sans-Serif; font-size: 11px" colspan="3">
                                <asp:UpdatePanel ID="upResultado" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <telerik:RadGrid ID="GrillaResultados" runat="server" GridLines="None" Skin="Vista"
                                            Width="100%" Height="485px" OnItemCommand="GrillaResultados_ItemCommand">
                                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="NroRemito" ClientDataKeyNames="NroRemito"
                                                ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top" TableLayout="Fixed"
                                                GroupLoadMode="Client" ShowGroupFooter="true" NoMasterRecordsText="Debe seleccionar uno o todos los grupos para ver los remitos pendientes">
                                                <CommandItemTemplate>
                                                    <div style="padding: 5px 5px;">
                                                        <asp:LinkButton CausesValidation="false" Mensaje="Exportando Datos...." ID="ExportExcel"
                                                            runat="server" CommandName="ExportCtaCte">
                                                        <img style="padding-right: 5px;border:0px;vertical-align:middle;" alt="" src="Imagenes/Excel_16x16.gif" />Exportar Datos Excel</asp:LinkButton>&nbsp;&nbsp;
                                                    </div>
                                                </CommandItemTemplate>
                                                <GroupByExpressions>
                                                    <telerik:GridGroupByExpression>
                                                        <SelectFields>
                                                            <telerik:GridGroupByField FieldName="Clasif1" HeaderText="Grupo" />
                                                        </SelectFields>
                                                        <GroupByFields>
                                                            <telerik:GridGroupByField FieldName="Clasif1" />
                                                        </GroupByFields>
                                                    </telerik:GridGroupByExpression>
                                                </GroupByExpressions>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="Codigo" HeaderText="Código Cliente" UniqueName="CodigoColumn">
                                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Nombre" HeaderText="Apellido y Nombre" SortExpression="Nombre"
                                                        UniqueName="NombreColumn">
                                                        <ItemStyle Width="250px" />
                                                        <HeaderStyle Width="250px" HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="NroRemito" HeaderText="Nro Remito" 
                                                        UniqueName="NroRemitoColumn">
                                                        <ItemStyle Width="100px" />
                                                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    
                                                    <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripcion" UniqueName="DescripcionColumn">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    
                                                    <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha"  UniqueName="FechaColumn"
                                                     DataFormatString="{0:dd/MM/yyyy}" >
                                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    
                                                </Columns>
                                                <EditFormSettings>
                                                    <PopUpSettings ScrollBars="None"></PopUpSettings>
                                                </EditFormSettings>
                                            </MasterTableView>
                                            <ClientSettings>
                                                <Selecting AllowRowSelect="false" />
                                                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" ClientEvents-OnRequestStart="requestStart1">
        <ClientEvents OnRequestStart="requestStart1"></ClientEvents>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="GrillaResultados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="GrillaResultados" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50">
        <ProgressTemplate>
            <div id="DivBloque" class="progressBackgroundFilterBlue">
            </div>
            <div style="position: absolute; top: 40%; left: 38%; padding: 5px; width: 24%; z-index: 1001;
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
                                Text="Buscado Datos Consulta...">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</body>
</html>
