<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="ConsultaSaldosVencidos.aspx.cs"
    Inherits="ConsultaSaldosVencidos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Consulta de Saldos Vencidos</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />

    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>

    <script src="Scripts/jquery.autocomplete.js" type="text/javascript"></script>

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
    var ctrCliente;
    var ctrGrupo;

    jQuery(function() {

        options = {
            serviceUrl: 'ASHX/LoadClientes.ashx',
            width: '384',
            minChars: 3,
            zIndex: 922000000
        };
        ctrCliente = $('#' + "<%= txtCliente.ClientID %>").autocomplete(options);


    });

    function requestStart1(sender, args) {
        if (args.get_eventTarget().indexOf("ExportExcel") > 0) {
            args.set_enableAjax(false);
        }
    }   
</script>

<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form2" runat="server">
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
                    </div>
                </div>
                <div class="CabeceraContent">
                    <table width="95%" border="0" cellspacing="0">
                        <tr>
                            <td colspan="4" valign="top" align="center" style="width: 100%;">
                                <div style="position:relative;top:-25px">
                                    <asp:Label Font-Names="Bookman Old Style" Font-Size="22pt" ForeColor="#5F5F5F" ID="Label1"
                                        runat="server">Consulta de Saldos Vencidos</asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px;">
                                <asp:Label SkinID="lblBlue" ID="Label2" runat="server">Grupo:</asp:Label>
                            </td>
                            <td align="left" style="width: 350px;">
                                <telerik:RadComboBox ID="cboGrupoFiltro" runat="server" Width="410px" EmptyMessage="Todos los Grupos"
                                    Skin="WebBlue" CausesValidation="false">
                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                </telerik:RadComboBox>
                            </td>
                            <td style="width: 100px;">
                                <asp:Label SkinID="lblBlue" ID="UserNameLabel" runat="server">Revendedor:</asp:Label>
                            </td>
                            <td align="left" style="width: 350px;">
                                <asp:TextBox Width="325px" ID="txtCliente" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label SkinID="lblBlue" ID="UserNameLabel0" runat="server">Fecha Incial:</asp:Label>
                            </td>
                            <td align="left">
                                <telerik:RadDatePicker ID="txtFechaInicial" runat="server" Skin="Web20" Culture="Spanish (Argentina)">
                                    <DateInput ID="DateInput1" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Web20"
                                        Skin="Web20" SelectionOnFocus="SelectAll" ShowButton="True">
                                    </DateInput>
                                    <Calendar Skin="Web20">
                                    </Calendar>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <asp:Label SkinID="lblBlue" ID="UserNameLabel1" runat="server">Fecha Final:</asp:Label>
                            </td>
                            <td align="left">
                                <telerik:RadDatePicker ID="txtFechaFinal" runat="server" Skin="Web20" Culture="Spanish (Argentina)">
                                    <DateInput ID="DateInput2" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Web20"
                                        Skin="Web20" SelectionOnFocus="SelectAll" ShowButton="True">
                                    </DateInput>
                                    <Calendar Skin="Web20">
                                    </Calendar>
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="4">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" SkinID="btnBasic" Width="77px"
                                            OnClick="btnConsultar_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="color: #993300; font-family: Sans-Serif; font-size: 11px" colspan="4">
                                <asp:UpdatePanel ID="upResultado" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <telerik:RadGrid ID="GrillaResultados" runat="server" GridLines="None" Skin="Vista"
                                            Width="100%" OnItemCommand="GrillaResultados_ItemCommand">
                                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="cve_nro" ClientDataKeyNames="cve_nro"
                                                ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top" TableLayout="Fixed"
                                                NoMasterRecordsText="No se encontraron datos para los filtros seleccionados"
                                                GroupLoadMode="Client" ShowGroupFooter="true" ShowFooter="true">
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
                                                            <telerik:GridGroupByField FieldName="cvecli_RazSoc" HeaderText="Revendedor" />
                                                        </SelectFields>
                                                        <GroupByFields>
                                                            <telerik:GridGroupByField FieldName="cvecli_RazSoc" SortOrder="Ascending" />
                                                        </GroupByFields>
                                                    </telerik:GridGroupByExpression>
                                                </GroupByExpressions>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="dc1_Desc" HeaderText="Grupo" SortExpression="Nro"
                                                        UniqueName="dc1_Desc">
                                                        <ItemStyle Width="180px" HorizontalAlign="Center" />
                                                        <HeaderStyle Width="180px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="cve_CodCli" HeaderText="Nro Revendedor" UniqueName="cve_CodCliColumn">
                                                        <ItemStyle Width="100%" HorizontalAlign="Left" />
                                                        <HeaderStyle Width="75px" HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="cvecli_RazSoc" HeaderText="Revendedor" UniqueName="ClienteColumn">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="cve_FEmision" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}"
                                                        HeaderText="Fecha Emisión" UniqueName="FechaPedido">
                                                        <ItemStyle Width="70px" />
                                                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="cvetco_Cod" HeaderText="Tipo" UniqueName="cvetco_CodColumn">
                                                        <ItemStyle Width="45px" HorizontalAlign="Center" />
                                                        <HeaderStyle Width="45px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="cve_Nro" HeaderText="Nro" UniqueName="cve_NroColumn">
                                                        <ItemStyle Width="65px" HorizontalAlign="Center" />
                                                        <HeaderStyle Width="65px" HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="cve_SaldoMonCC" HeaderText="Saldo" 
                                                    FooterAggregateFormatString="Total: {0:00.00}" DataFormatString="{0:###.00}"
                                                        UniqueName="cve_SaldoMonCCColumn" Aggregate="Sum" >
                                                        <ItemStyle Width="100%" HorizontalAlign="Left" />
                                                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
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
