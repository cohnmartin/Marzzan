<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="ConsultaPedidos.aspx.cs"
    Inherits="ConsultaPedidos" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=3.1.9.807, Culture=neutral, PublicKeyToken=a9d7983dfcc261be"
    Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consultas Sandra Marzzan</title>
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
</head>

<script type="text/javascript">
    function ShowComprobante(id) {
        var oWnd = radopen('ReportViewer.aspx?Id=' + id, 'RadWindow2');
    }

    function requestStart1(sender, args) {
        if (args.get_eventTarget().indexOf("ExportExcel") > 0) {
            args.set_enableAjax(false);
        }
    }   
</script>

<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Web20" VisibleTitlebar="true"
        Title="Atención">
        <Windows>
            <telerik:RadWindow ID="RadWindow2" runat="server" Behaviors="Close" Width="870" Title="Comprobante Pedido"
                Height="600" Modal="true" Overlay="false" NavigateUrl="ReportViewer.aspx" VisibleTitlebar="true"
                VisibleStatusbar="false" ShowContentDuringLoad="false" Skin="WebBlue">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="Header_panelContainerSimple">
                    <div class="CabeceraInicial">
                        <asp:Button ID="btnVolver" runat="server" Text="" SkinID="btnVolver" OnClick="btnVolver_Click"
                            Visible="false" />
                    </div>
                </div>
                <div class="CabeceraContent">
                    <table width="95%" border="0" cellspacing="0" style="border-style: ridge; border-width: thin">
                        <tr>
                            <td style="width: 100px; color: #993300; font-family: Sans-Serif; font-size: 11px">
                                <asp:Label ID="UserNameLabel" runat="server">Revendedor:</asp:Label>
                            </td>
                            <td colspan="2" align="left">
                                <telerik:RadComboBox ID="cboConsultores" runat="server" Skin="WebBlue" Width="290px"
                                    AllowCustomText="true" CloseDropDownOnBlur="true" MarkFirstMatch="true">
                                    <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; color: #993300; font-family: Sans-Serif; font-size: 11px">
                                <asp:Label ID="Label2" runat="server">Tipo Pedido:</asp:Label>
                            </td>
                            <td colspan="2" align="left">
                                <telerik:RadComboBox ID="cboTipoPedido" runat="server" Skin="WebBlue" Width="290px"
                                    CloseDropDownOnBlur="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Todos" Value="0" Selected="true" />
                                        <telerik:RadComboBoxItem Text="Remitos" Value="RT" />
                                        <telerik:RadComboBoxItem Text="Notas Pedido" Value="NP" />
                                        <telerik:RadComboBoxItem Text="Notas Debito" Value="ND" />
                                    </Items>
                                    <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; color: #993300; font-family: Sans-Serif; font-size: 11px">
                                <asp:Label ID="UserNameLabel0" runat="server">Fecha Incial:</asp:Label>
                            </td>
                            <td colspan="2" align="left">
                                <telerik:RadDatePicker ID="txtFechaInicial" runat="server" Skin="Web20" Width="138px"
                                    Culture="Spanish (Argentina)">
                                    <DateInput InvalidStyleDuration="100" LabelCssClass="radLabelCss_Web20" Skin="Web20"
                                        SelectionOnFocus="SelectAll" ShowButton="True">
                                    </DateInput>
                                    <Calendar Skin="Web20">
                                    </Calendar>
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; color: #993300; font-family: Sans-Serif; font-size: 11px">
                                <asp:Label ID="UserNameLabel1" runat="server">Fecha Final:</asp:Label>
                            </td>
                            <td class="style5" align="left">
                                <telerik:RadDatePicker ID="txtFechaFinal" runat="server" Skin="Web20" Width="138px"
                                    Culture="Spanish (Argentina)">
                                    <DateInput InvalidStyleDuration="100" LabelCssClass="radLabelCss_Web20" Skin="Web20"
                                        SelectionOnFocus="SelectAll" ShowButton="True">
                                    </DateInput>
                                    <Calendar Skin="Web20">
                                    </Calendar>
                                </telerik:RadDatePicker>
                            </td>
                            <td align="right">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" SkinID="btnBasic" Width="77px"
                                            OnClick="btnConsultar_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="style3" style="color: #993300; font-family: Sans-Serif; font-size: 11px"
                                colspan="3">
                                <asp:UpdatePanel ID="upResultado" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                <telerik:RadGrid ID="GrillaResultados" runat="server" GridLines="None" Skin="Vista"
                                    Width="100%" OnDetailTableDataBind="GrillaResultados_DetailTableDataBind" OnItemCommand="GrillaResultados_ItemCommand">
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="IdCabeceraPedido" ClientDataKeyNames="IdCabeceraPedido"
                                        ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top" TableLayout="Fixed"
                                        GroupLoadMode="Client" ShowGroupFooter="true" NoMasterRecordsText="No se encontraron pedidos en las fechas seleccionadas"
                                        GroupsDefaultExpanded="true">
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
                                                    <telerik:GridGroupByField FieldName="FechaPedido" HeaderText="Solicitudes de Mes"
                                                        FormatString="{0:M/yyyy}" />
                                                </SelectFields>
                                                <SelectFields>
                                                    <telerik:GridGroupByField FieldName="MontoTotal" HeaderText="Total Solicitado" Aggregate="Sum"
                                                        FormatString="{0:C}" />
                                                </SelectFields>
                                                <GroupByFields>
                                                    <telerik:GridGroupByField FieldName="MesAñoPedido" SortOrder="Ascending" />
                                                </GroupByFields>
                                            </telerik:GridGroupByExpression>
                                        </GroupByExpressions>
                                        <RowIndicatorColumn Visible="False">
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn Visible="False" Resizable="False">
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </ExpandCollapseColumn>
                                        <DetailTables>
                                            <telerik:GridTableView ClientDataKeyNames="IdDetallePedido" DataKeyNames="IdDetallePedido"
                                                HierarchyLoadMode="Client" Width="100%" TableLayout="Fixed" GridLines="Horizontal"
                                                AutoGenerateColumns="false">
                                                <ParentTableRelation>
                                                    <telerik:GridRelationFields DetailKeyField="CabeceraPedido" MasterKeyField="IdCabeceraPedido" />
                                                </ParentTableRelation>
                                                <RowIndicatorColumn Visible="False">
                                                    <HeaderStyle Width="20px" />
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn Resizable="False" Visible="False">
                                                    <HeaderStyle Width="20px" />
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="Cantidad" DataType="System.Int64" HeaderText="Cantidad"
                                                        ReadOnly="True" SortExpression="Cantidad" UniqueName="Cantidad">
                                                        <ItemStyle Width="50px" />
                                                        <HeaderStyle Width="50px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Producto" UniqueName="ProductoColumn">
                                                        <ItemTemplate>
                                                            <%# GenerarEtiqueta( Eval("objProducto.DescripcionCompleta").ToString(), Eval("objPresentacion.Descripcion").ToString(), Eval("objProducto.Descripcion").ToString())%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="ValorUnitario" DataType="System.Decimal" HeaderText="Valor Unitario"
                                                        ReadOnly="True" SortExpression="ValorUnitario" UniqueName="ValorUnitario">
                                                        <ItemStyle Width="75px" HorizontalAlign="Center" />
                                                        <HeaderStyle Width="75px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ValorTotal" DataType="System.Decimal" HeaderText="Valor Total"
                                                        ReadOnly="True" SortExpression="ValorTotal" UniqueName="ValorTotal">
                                                        <ItemStyle Width="65px" HorizontalAlign="Center" />
                                                        <HeaderStyle Width="65px" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <SortExpressions>
                                                </SortExpressions>
                                                <EditFormSettings>
                                                    <PopUpSettings ScrollBars="None" />
                                                </EditFormSettings>
                                            </telerik:GridTableView>
                                        </DetailTables>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Nro" HeaderText="Nro" SortExpression="Nro" UniqueName="Nro">
                                                <ItemStyle Width="45px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="45px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="objClienteSolicitante.Nombre" HeaderText="Solicitante a:"
                                                UniqueName="SolicitanteColumn">
                                                <ItemStyle Width="100%" HorizontalAlign="Left" />
                                                <HeaderStyle Width="110px" HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="objCliente.Nombre" HeaderText="Revendedor" UniqueName="ClienteColumn">
                                                <ItemStyle Width="100%" HorizontalAlign="Left" />
                                                <HeaderStyle Width="110px" HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FechaPedido" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy HH:mm}"
                                                HeaderText="Fecha" SortExpression="FechaPedido" UniqueName="FechaPedido">
                                                <ItemStyle Width="90px" />
                                                <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TipoPedido" HeaderText="Tipo" SortExpression="Tipo"
                                                UniqueName="TipoPedido">
                                                <ItemStyle Width="25px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="25px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MontoTotal" DataType="System.Decimal" HeaderText="Total"
                                                SortExpression="MontoTotal" UniqueName="MontoTotal">
                                                <ItemStyle Width="65px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="65px" HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="objFormaDePago.Descripcion" HeaderText="Forma Pago"
                                                UniqueName="FormaPagoColumn">
                                                <ItemStyle Width="100%" HorizontalAlign="Left" />
                                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn Display="false" DataField="objDireccion.DireccionCompleta"
                                                HeaderText="Direccion Entrega" UniqueName="DireccionEntregaColumn">
                                                <ItemStyle Width="100%" HorizontalAlign="Left" />
                                                <HeaderStyle Width="560px" HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Estado" UniqueName="EstadoColumn">
                                                <ItemTemplate>
                                                    <%# GenerarEstado(Eval("NroImpresion"))%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="" UniqueName="ImprimirColumn">
                                                <ItemTemplate>
                                                    <img alt="imprimir" style="cursor: hand" src="Imagenes/print.gif" onclick="ShowComprobante(<%# Eval("IdCabeceraPedido")%>)" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="18px" />
                                                <ItemStyle Width="18px" HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <PopUpSettings ScrollBars="None"></PopUpSettings>
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </ContentTemplate>
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
