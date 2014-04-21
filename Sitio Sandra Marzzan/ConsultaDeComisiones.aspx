<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="ConsultaDeComisiones.aspx.cs"
    Inherits="ConsultaDeComisiones" %>

<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="cc6" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Charting" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consultas De Comisiones</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <telerik:RadScriptBlock runat="server">

        <script type="text/javascript">
            function requestStart1(sender, args) {
                if (args.get_eventTarget().indexOf("ExportExcel") > 0) {
                    args.set_enableAjax(false);
                }
            }

            function ShowChart(fecha) {

                $find("<%=RadAjaxManager1.ClientID%>").ajaxRequest("ByFecha|"+fecha);

            }

            function ShowChartByConsultor(CodigoExterno) {
                $find("<%=RadAjaxManager1.ClientID%>").ajaxRequest("ByCodigo|" + CodigoExterno);
            }

            function Show() {

                $find("<%=ServerControlWindow1.ClientID %>").set_CollectionDiv('divPrincipal');
                $find("<%=ServerControlWindow1.ClientID %>").ShowWindows('divPrincipal', "Gráfico del Periodo");
            }
        </script>

    </telerik:RadScriptBlock>
</head>
<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
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
                                <asp:Label ID="Label1" runat="server">Fecha Incial:</asp:Label>
                            </td>
                            <td align="left" style="width: 300px;">
                                <telerik:RadDatePicker ID="txtFechaInicial" runat="server" Skin="Web20" Width="138px"
                                    Culture="Spanish (Argentina)">
                                    <DateInput InvalidStyleDuration="100" LabelCssClass="radLabelCss_Web20" Skin="Web20"
                                        SelectionOnFocus="SelectAll" ShowButton="True">
                                    </DateInput>
                                    <Calendar Skin="Web20">
                                    </Calendar>
                                </telerik:RadDatePicker>
                            </td>
                            <td style="width: 100px; color: #993300; font-family: Sans-Serif; font-size: 11px">
                                <asp:Label ID="Label2" runat="server">Fecha Final:</asp:Label>
                            </td>
                            <td align="left" style="width: 300px;">
                                <telerik:RadDatePicker ID="txtFechaFinal" runat="server" Skin="Web20" Width="138px"
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
                                <asp:Label ID="UserNameLabel" runat="server">Revendedor:</asp:Label>
                            </td>
                            <td align="left" style="width: 300px;" colspan="2">
                                <telerik:RadComboBox ID="cboConsultores" runat="server" Skin="WebBlue" Width="100%"
                                    AllowCustomText="true" CloseDropDownOnBlur="true" MarkFirstMatch="true">
                                    <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                </telerik:RadComboBox>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" SkinID="btnBasic" Width="77px"
                                    OnClick="btnConsultar_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="color: #993300; font-family: Sans-Serif; font-size: 11px;"
                                colspan="4">
                                <asp:UpdatePanel ID="upResultado" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <telerik:RadGrid ID="GrillaResultados" runat="server" GridLines="None" Skin="Vista"
                                            Width="100%" OnItemCommand="GrillaResultados_ItemCommand" Height="480px" OnItemDataBound="GrillaResultados_ItemDataBound">
                                            <MasterTableView AutoGenerateColumns="false" ShowHeadersWhenNoRecords="true" GroupHeaderItemStyle-HorizontalAlign="Left"
                                                CommandItemDisplay="Top" TableLayout="Fixed" GroupLoadMode="Client" ShowGroupFooter="true"
                                                NoMasterRecordsText="Debe seleccionar uno o todos los revendedores para ver las comisiones generadas">
                                                <CommandItemTemplate>
                                                    <div style="padding: 5px 5px;">
                                                        <asp:LinkButton CausesValidation="false" Mensaje="Exportando Datos...." ID="ExportExcel"
                                                            runat="server" CommandName="ExportCtaCte">
                                                        <img style="padding-right: 5px;border:0px;vertical-align:middle;" alt="" src="Imagenes/Excel_16x16.gif" />Exportar Datos Excel</asp:LinkButton>&nbsp;&nbsp;
                                                    </div>
                                                </CommandItemTemplate>
                                                
                                                <Columns>
                                                <telerik:GridBoundColumn DataField="Cod_Cliente" UniqueName="Cod_ClienteColumn" Display="false">
                                                    </telerik:GridBoundColumn>
                                                    
                                                    <telerik:GridTemplateColumn DataField="Lider" HeaderText="Revendedor" ItemStyle-Width="100%" HeaderStyle-Width="150px"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNombre" runat="server" Style="text-transform: capitalize"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    
                                                     <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:MM/yyyy}" ItemStyle-Width="100%" 
                                                        Display="false"
                                                        HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    </telerik:GridBoundColumn>
                                                    
                                                    <telerik:GridBoundColumn DataField="Mes_Ant" HeaderText="Mes Ant" ItemStyle-Width="100%"
                                                        HeaderStyle-Width="52px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Mes_Actual" HeaderText="Mes Act" ItemStyle-Width="100%"
                                                        HeaderStyle-Width="52px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="_Crec" HeaderText="% Crec" ItemStyle-Width="100%"
                                                        HeaderStyle-Width="52px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Comision_Base" HeaderText="Com Base" ItemStyle-Width="100%"
                                                        HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Comision_Sub" HeaderText="Com Sub" ItemStyle-Width="100%"
                                                        HeaderStyle-Width="52px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="_Adic_Gest" HeaderText="% Adic Gest" ItemStyle-Width="100%"
                                                        HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Comi_AdicGest" HeaderText="Com Adic Gest" ItemStyle-Width="100%"
                                                        HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="_Adic_MKT" HeaderText="% Adic MKT" ItemStyle-Width="100%"
                                                        HeaderStyle-Width="65px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Comi_AdicMKT" HeaderText="Com Adic MKT" ItemStyle-Width="100%"
                                                        HeaderStyle-Width="95px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Comision_Adic" HeaderText="Com Adic" ItemStyle-Width="100%"
                                                        HeaderStyle-Width="55px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn Aggregate="Sum" FooterAggregateFormatString="<b>$ {0:0.00}</b>"
                                                        DataField="TOTAL" HeaderText="TOTAL" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
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
    <asp:UpdatePanel ID="upGrafico" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cc6:ServerControlWindow ID="ServerControlWindow1" runat="server" BackColor="WhiteSmoke"
                WindowColor="Azul">
                <ContentControls>
                    <div id="divPrincipal" style="height: 320px">
                        <telerik:RadChart ID="CharComisiones" runat="server" DefaultType="Line" Width="800px"
                            Skin="Vista">
                            <Series>
                                <telerik:ChartSeries Name="Comisiones" Type="Line">
                                    <Appearance>
                                        <FillStyle MainColor="186, 207, 141" SecondColor="146, 176, 83">
                                            <FillSettings GradientMode="Vertical">
                                                <ComplexGradient>
                                                    <telerik:GradientElement Color="213, 247, 255" />
                                                    <telerik:GradientElement Color="193, 239, 252" Position="0.5" />
                                                    <telerik:GradientElement Color="157, 217, 238" Position="1" />
                                                </ComplexGradient>
                                            </FillSettings>
                                        </FillStyle>
                                        <TextAppearance TextProperties-Color="89, 89, 89" Dimensions-Width="250px">
                                        </TextAppearance>
                                        <Border Color="" />
                                    </Appearance>
                                </telerik:ChartSeries>
                            </Series>
                            <PlotArea>
                                <XAxis>
                                    <Appearance Color="134, 134, 134" MajorTick-Color="134, 134, 134">
                                        <MajorGridLines Color="196, 196, 196" Width="0" />
                                        <TextAppearance TextProperties-Color="89, 89, 89">
                                        </TextAppearance>
                                    </Appearance>
                                    <AxisLabel>
                                        <Appearance Dimensions-Paddings="1px, 1px, 10%, 1px">
                                        </Appearance>
                                        <TextBlock>
                                            <Appearance TextProperties-Color="51, 51, 51">
                                            </Appearance>
                                        </TextBlock>
                                    </AxisLabel>
                                </XAxis>
                                <YAxis>
                                    <Appearance Color="134, 134, 134" MajorTick-Color="196, 196, 196" MinorTick-Color="196, 196, 196">
                                        <MajorGridLines Color="196, 196, 196" />
                                        <MinorGridLines Color="196, 196, 196" Width="0" />
                                        <TextAppearance TextProperties-Color="89, 89, 89">
                                        </TextAppearance>
                                    </Appearance>
                                    <AxisLabel>
                                        <TextBlock>
                                            <Appearance TextProperties-Color="220, 158, 119">
                                            </Appearance>
                                        </TextBlock>
                                    </AxisLabel>
                                </YAxis>
                                <Appearance Dimensions-Margins="19%, 90px, 12%, 9%">
                                    <FillStyle MainColor="Transparent" SecondColor="Transparent">
                                    </FillStyle>
                                    <Border Color="WhiteSmoke" />
                                </Appearance>
                            </PlotArea>
                            <Appearance Corners="Round, Round, Round, Round, 7">
                                <FillStyle FillType="ComplexGradient">
                                    <FillSettings>
                                        <ComplexGradient>
                                            <telerik:GradientElement Color="243, 253, 255" />
                                            <telerik:GradientElement Color="White" Position="0.5" />
                                            <telerik:GradientElement Color="243, 253, 255" Position="1" />
                                        </ComplexGradient>
                                    </FillSettings>
                                </FillStyle>
                                <Border Color="212, 221, 222" />
                            </Appearance>
                            <ChartTitle>
                                <Appearance Dimensions-Margins="3%, 1px, 1px, 6%">
                                    <FillStyle MainColor="">
                                    </FillStyle>
                                </Appearance>
                                <TextBlock Text="Progreso Comisiones">
                                    <Appearance TextProperties-Color="86, 88, 89" TextProperties-Font="Verdana, 22px">
                                    </Appearance>
                                </TextBlock>
                            </ChartTitle>
                            <Legend>
                                <Appearance Dimensions-Margins="1px, 1%, 10%, 1px">
                                    <ItemTextAppearance TextProperties-Color="86, 88, 89">
                                    </ItemTextAppearance>
                                    <ItemMarkerAppearance Figure="Rectangle">
                                    </ItemMarkerAppearance>
                                    <FillStyle MainColor="">
                                    </FillStyle>
                                    <Border Color="" />
                                </Appearance>
                                <TextBlock>
                                    <Appearance Position-AlignedPosition="Top" TextProperties-Color="86, 88, 89">
                                    </Appearance>
                                </TextBlock>
                            </Legend>
                        </telerik:RadChart>
                    </div>
                </ContentControls>
            </cc6:ServerControlWindow>
        </ContentTemplate>
    </asp:UpdatePanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
        ClientEvents-OnRequestStart="requestStart1" 
        ClientEvents-OnResponseEnd="Show"
        OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <ClientEvents OnRequestStart="requestStart1"></ClientEvents>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="GrillaResultados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="GrillaResultados" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    </form>
</body>
</html>
