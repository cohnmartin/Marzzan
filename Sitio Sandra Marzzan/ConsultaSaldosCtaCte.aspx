<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="ConsultaSaldosCtaCte.aspx.cs"
    Inherits="ConsultaSaldosCtaCte" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Consultas Saldos Cta Cte</title>
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
                            <td style="width: 100px; color: #993300; font-family: Sans-Serif; font-size: 11px">
                                <asp:Label ID="UserNameLabel" runat="server">Revendedor:</asp:Label>
                            </td>
                            <td align="left" style="width: 300px;">
                                <telerik:RadComboBox ID="cboConsultores" runat="server" Skin="WebBlue" Width="100%"
                                    AllowCustomText="true" CloseDropDownOnBlur="true" MarkFirstMatch="true">
                                    <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                </telerik:RadComboBox>
                            </td>
                            <td align="left" style="width: 200px; color: #993300; font-family: Sans-Serif; font-size: 11px">
                                <asp:CheckBox Text="Incluir Clientes con Saldo Cero" Checked="false" ID="chkSaldosCero"
                                    runat="server" />
                            </td>
                            <td align="right">
                                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" SkinID="btnBasic" Width="77px"
                                    OnClick="btnConsultar_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="color: #993300; font-family: Sans-Serif; font-size: 11px" colspan="4">
                                <asp:UpdatePanel ID="upResultado" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <telerik:RadGrid ID="GrillaResultados" runat="server" GridLines="None" Skin="Vista"
                                            Width="100%" OnItemCommand="GrillaResultados_ItemCommand">
                                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="IdCliente" ClientDataKeyNames="Nombre"
                                                ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top" TableLayout="Fixed"
                                                GroupLoadMode="Client" ShowGroupFooter="true" NoMasterRecordsText="Debe seleccionar uno o todos los revendedores para ver su estado de cta. cte.">
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
                                                <RowIndicatorColumn Visible="False">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn Visible="False" Resizable="False">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="CodigoExterno" HeaderText="Código" UniqueName="CodigoExternoColumn">
                                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Nombre" HeaderText="Apellido y Nombre" SortExpression="Nombre"
                                                        UniqueName="NombreColumn">
                                                        <ItemStyle Width="250px" />
                                                        <HeaderStyle Width="250px" HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Login" HeaderText="Login" UniqueName="LoginColumn">
                                                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DNI" HeaderText="DNI" SortExpression="DNI" UniqueName="DNIColumn">
                                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Email" HeaderText="Email" SortExpression="Email"
                                                        UniqueName="EmailColumn">
                                                        <ItemStyle Width="180px" />
                                                        <HeaderStyle Width="180px" HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Telefono" HeaderText="Telefono" SortExpression="Telefono"
                                                        UniqueName="TelefonoColumn">
                                                        <ItemStyle Width="180px" />
                                                        <HeaderStyle Width="180px" HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="SaldoCtaCte" HeaderText="Saldo Cta Cte" SortExpression="SaldoCtaCte"
                                                        UniqueName="SaldoCtaCteColumn">
                                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <EditFormSettings>
                                                    <PopUpSettings ScrollBars="None"></PopUpSettings>
                                                </EditFormSettings>
                                            </MasterTableView>
                                            <ClientSettings>
                                                <Selecting AllowRowSelect="True" />
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
