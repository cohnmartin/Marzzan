<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="GestionPromociones.aspx.cs"
    Inherits="GestionPromociones" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión de Promociones</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<telerik:RadScriptBlock runat="server">

    <script type="text/javascript">
        function ControlarCarga(EsGrabacion) {
            var tooltipo = $find('<%= tooltipCarga.ClientID %>');

            if ($find('<%= cboPromociones.ClientID %>').get_value() == "") {

                tooltipo.set_targetControlID('<%= cboPromociones.ClientID %>');
                tooltipo.set_text("Debe Seleccionar la promoción que desea configurar");
                tooltipo.show();
                return false;

            }
            else if ($find('<%= txtFechaInicial.ClientID %>').get_selectedDate() == null) {

                tooltipo.set_targetControlID('<%= txtFechaInicial.ClientID %>');
                tooltipo.set_text("Debe ingresar la fecha inicial para la vigencia de la promoción");
                tooltipo.show();
                return false;

            }
            else if ($find('<%= txtFechaFinal.ClientID %>').get_selectedDate() == null) {

                tooltipo.set_targetControlID('<%= txtFechaFinal.ClientID %>');
                tooltipo.set_text("Debe ingresar la fecha final para la vigencia de la promoción");
                tooltipo.show();
                return false;

            }
            else if ($find('<%= cboDefiniciones.ClientID %>').get_value() == "" && !EsGrabacion) {

                tooltipo.set_targetControlID('<%= cboDefiniciones.ClientID %>');
                tooltipo.set_text("Debe Seleccionar el tipo de definición para agregar la configuración");
                tooltipo.show();
                return false;

            }
            else if ($find('<%= txtCantidad.ClientID %>').get_value() == "" && !EsGrabacion) {

                tooltipo.set_targetControlID('<%= txtCantidad.ClientID %>');
                tooltipo.set_text("La cantidad del elemento debe ser mayo a cero");
                tooltipo.show();
                return false;

            }
            else if (EsGrabacion) {
                if ($find('<%= grillaComponentes.ClientID %>').get_masterTableView().get_dataItems().length == 0) {
                    tooltipo.set_targetControlID('<%= grillaComponentes.ClientID %>');
                    tooltipo.set_text("Debe Existir al menos una definición para grabar la configuración.");
                    tooltipo.show();
                    return false;
                }
            }



            return true;

        }

    
    </script>

    <script type="text/javascript">
        function LimpiarCombos() {

            var UndNegCombo = $find("cboUndNeg");
            var DefinicionesCombo = $find("cboDefiniciones");
            var LineaCombo = $find("cboLineas");
            var FraganciasCombo = $find("cboFragancias");
            var PresentacionesCombo = $find("cboPresentaciones");

            UndNegCombo.set_text("");
            
            LineaCombo.set_text(" ");
            LineaCombo.clearItems();

            FraganciasCombo.set_text(" ");
            FraganciasCombo.clearItems();

            PresentacionesCombo.set_text(" ");
            PresentacionesCombo.clearItems();

            DefinicionesCombo.set_text(" ");
            DefinicionesCombo.clearItems();
        }

        function LoadLineas(combo, eventarqs) {

            var DefinicionesCombo = $find("cboDefiniciones");
            var LineaCombo = $find("cboLineas");
            var FraganciasCombo = $find("cboFragancias");
            var PresentacionesCombo = $find("cboPresentaciones");
            var item = eventarqs.get_item();

            LineaCombo.set_text("Cargando....");
            FraganciasCombo.clearSelection();
            PresentacionesCombo.clearSelection();
            DefinicionesCombo.clearSelection();


            if (item.get_index() >= 0) {
                LineaCombo.requestItems(item.get_value(), false);
            }
            else {
                LineaCombo.set_text(" ");
                LineaCombo.clearItems();



                FraganciasCombo.set_text(" ");
                FraganciasCombo.clearItems();

                PresentacionesCombo.set_text(" ");
                PresentacionesCombo.clearItems();

                DefinicionesCombo.set_text(" ");
                DefinicionesCombo.clearItems();

            }

        }

        function LoadFragancias(combo, eventarqs) {
            var DefinicionesCombo = $find("cboDefiniciones");
            var FraganciasCombo = $find("cboFragancias");
            var PresentacionesCombo = $find("cboPresentaciones");
            var LineaCombo = $find("cboLineas");
            var UndNegCombo = $find("cboUndNeg");
            var item = eventarqs.get_item();

            if (item.get_index() >= 0) {

                if (item.get_text().indexOf('Promo') < 0 || item.get_text().indexOf('Descue') < 0)
                    FraganciasCombo.set_text("Cargando....");
                else
                    FraganciasCombo.set_text("");

                PresentacionesCombo.clearSelection();
                DefinicionesCombo.clearSelection();
                FraganciasCombo.requestItems(item.get_value() + "|" + UndNegCombo.get_value(), false);
            }
            else {

                FraganciasCombo.set_text(" ");
                FraganciasCombo.clearItems();

                PresentacionesCombo.set_text(" ");
                PresentacionesCombo.clearItems();

                DefinicionesCombo.set_text(" ");
                DefinicionesCombo.clearItems();

            }

        }
        function LoadPresentaciones(combo, eventarqs) {
            var UndNegCombo = $find("cboUndNeg");
            var LineaCombo = $find("cboLineas");
            var DefinicionesCombo = $find("cboDefiniciones");
            var PresentacionesCombo = $find("cboPresentaciones");
            var item = eventarqs.get_item();

            if (item.get_index() >= 0) {
                PresentacionesCombo.set_text("Cargando....");
                DefinicionesCombo.clearSelection();
                if (LineaCombo.get_value() != "0") {
                    PresentacionesCombo.requestItems(item.get_value() + "|" + LineaCombo.get_value() + "|byLinea", false);
                }
                else {
                    PresentacionesCombo.requestItems(item.get_value() + "|" + UndNegCombo.get_value() + "|byUndNeg", false);
                }

            }
            else {

                PresentacionesCombo.set_text(" ");
                PresentacionesCombo.clearItems();

                DefinicionesCombo.set_text(" ");
                DefinicionesCombo.clearItems();

            }


        }

        function LoadDefiniciones(combo, eventarqs) {
            var DefinicionesCombo = $find("cboDefiniciones");
            var item = eventarqs.get_item();

            if (item.get_index() >= 0) {
                DefinicionesCombo.set_text("Cargando....");
                DefinicionesCombo.requestItems(item.get_value(), false);
            }
            else {

                DefinicionesCombo.set_text(" ");
                DefinicionesCombo.clearItems();

            }

        }


        function ItemsLoaded(combo, eventarqs) {
            if (combo.get_items().get_count() > 0) {
                combo.set_text("")
                combo.showDropDown();
            }
            else {
                var DefinicionesCombo = $find("cboDefiniciones");
                DefinicionesCombo.set_text("Cargando...");
                DefinicionesCombo.requestItems("", false);
            }

        }

        function ShowTransportistas() {

            var tooltipo = $find('<%= tooltipCarga.ClientID %>');
            if ($find('<%= cboPromociones.ClientID %>').get_value() == "") {

                tooltipo.set_targetControlID('<%= cboPromociones.ClientID %>');
                tooltipo.set_text("Debe Seleccionar la promoción antes de poder definir los transportistas");
                tooltipo.show();
                return false;

            }
            else if (document.getElementById("IdConfPromoSeleccionada").value == "") {
                tooltipo.set_targetControlID('<%= cboPromociones.ClientID %>');
                tooltipo.set_text("Antes de poder asignar los transportistas deber grabar la configuración actual.");
                tooltipo.show();
                return false;

            }
            else {
                var oWnd = radopen("GestionTransportistasPromos.aspx?IdConfPromocion=" + document.getElementById("IdConfPromoSeleccionada").value, 'RadWindow1');
            }

        }

      
    </script>

</telerik:RadScriptBlock>
<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="FuncionesComunes.js" />
        </Scripts>
    </asp:ScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Web20" VisibleTitlebar="true"
        Title="Atención">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close" Width="570" Height="400"
                Modal="true" NavigateUrl="GestionTransportistasPromos.aspx" VisibleTitlebar="true"
                Title="Transportistas Habilitados" VisibleStatusbar="false" ShowContentDuringLoad="false"
                Skin="WebBlue">
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
                    <telerik:RadToolTip runat="server" ID="tooltipCarga" Skin="WebBlue" Sticky="false"
                        ShowCallout="true" AutoCloseDelay="4000" Width="220px" Animation="None" Position="BottomCenter"
                        ManualClose="true" RelativeTo="Element" ShowEvent="FromCode" Title="Atención!!">
                    </telerik:RadToolTip>
                    <table class="style1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="center">
                                <table cellpadding="0" cellspacing="5" style="width: 80%">
                                    <tr>
                                        <td align="center" style="height: 25px; background: url('imagenes/sprite_webBlue.gif') 0  -300px repeat-x">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="14pt" Font-Names="Sans-Serif"
                                                ForeColor="Gray" Text="Gestión de Promociones" Width="378px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="upSelectedPromo" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="0" cellspacing="0" class="style1" style="border: 1px solid #336699;
                                            height: 45px">
                                            <tr style="background-color: #7EA9D3;">
                                                <td style="width: 30%; text-align: right">
                                                    <asp:Label SkinID="lblWhite" ID="lblPromociones" Text="Promociones:" runat="server"></asp:Label>
                                                </td>
                                                <td style="height: 25px">
                                                    <telerik:RadComboBox ID="cboPromociones" runat="server" Width="410px" EmptyMessage="Seleccione una Promoción"
                                                        Skin="WebBlue" AllowCustomText="true" AutoPostBack="True" 
                                                        OnSelectedIndexChanged="cboPromociones_SelectedIndexChanged"
                                                        EnableLoadOnDemand="true" OnItemsRequested="cboPromociones_ItemsRequested"
                                                        OnClientSelectedIndexChanged="LimpiarCombos"
                                                        Mensaje="Cargando Definición Promoción...">
                                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                                        
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <!-- filtro de definición -->
                        <tr>
                            <td style="">
                                <asp:UpdatePanel ID="upConfPromo" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <input type="hidden" runat="server" id="IdConfPromoSeleccionada" value="" />
                                        <table cellpadding="0" cellspacing="0" class="style1" style="border: 1px solid #336699">
                                            <tr style="background-color: #7EA9D3">
                                                <td align="center" style="border-right: 1px solid #336699; border-bottom: 1px solid #336699">
                                                    <asp:Label SkinID="lblWhite" ID="lblPromociones0" Text="Tipo de Promoción" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" style="border-right: 1px solid #336699; border-bottom: 1px solid #336699">
                                                    <asp:Label ID="lblPromociones1" runat="server" SkinID="lblWhite" Text="Fecha Inicio Promoción"></asp:Label>
                                                </td>
                                                <td align="center" style="border-right: 1px solid #336699; border-bottom: 1px solid #336699">
                                                    <asp:Label ID="lblPromociones2" runat="server" SkinID="lblWhite" Text="Fecha Final Promoción"></asp:Label>
                                                </td>
                                                <td align="center" style="border-right: 1px solid #336699; border-bottom: 1px solid #336699">
                                                    <asp:Label ID="Label2ss" runat="server" SkinID="lblWhite" Text="Filtro Transportes"></asp:Label>
                                                </td>
                                                <td align="center" style="border-right: 1px solid #336699; border-bottom: 1px solid #336699">
                                                    <asp:Label ID="Label2ss0" runat="server" SkinID="lblWhite" Text="Única por Pedido"></asp:Label>
                                                </td>
                                                <td align="center" style="border-bottom: 1px solid #336699">
                                                    <asp:Label ID="Label2ss1" runat="server" SkinID="lblWhite" Text="Monto Mínimo Promoción"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <telerik:RadComboBox ID="cboTipoPromo" runat="server" DataSourceID="LinqDataSourceTipoPromo"
                                                        DataTextField="Descripcion" DataValueField="IdClasificacion" EmptyMessage="Seleccione un tipo Promo"
                                                        Skin="WebBlue" Width="196px">
                                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                                    </telerik:RadComboBox>
                                                    <asp:LinqDataSource ID="LinqDataSourceTipoPromo" runat="server" ContextTypeName="CommonMarzzan.Marzzan_InfolegacyDataContext"
                                                        TableName="Clasificaciones" Where="Tipo == @Tipo ">
                                                        <WhereParameters>
                                                            <asp:Parameter DefaultValue="TipoPromo" Name="Tipo" Type="String" />
                                                        </WhereParameters>
                                                    </asp:LinqDataSource>
                                                </td>
                                                <td align="center">
                                                    <telerik:RadDatePicker ID="txtFechaInicial" runat="server" Culture="Spanish (Argentina)"
                                                        Skin="Vista" Width="108px">
                                                        <DateInput InvalidStyleDuration="100" LabelCssClass="radLabelCss_Web20" SelectionOnFocus="SelectAll"
                                                            ShowButton="True" Skin="Vista">
                                                        </DateInput>
                                                        <Calendar Skin="Vista" UseRowHeadersAsSelectors="false">
                                                        </Calendar>
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td align="center">
                                                    <telerik:RadDatePicker ID="txtFechaFinal" runat="server" Culture="Spanish (Argentina)"
                                                        Skin="Vista" Width="108px">
                                                        <DateInput InvalidStyleDuration="100" LabelCssClass="radLabelCss_Web20" SelectionOnFocus="SelectAll"
                                                            ShowButton="True" Skin="Vista">
                                                        </DateInput>
                                                        <Calendar Skin="Vista" UseRowHeadersAsSelectors="false">
                                                        </Calendar>
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td align="center">
                                                    <asp:ImageButton ID="btnTransportistas" runat="server" ImageUrl="~/Imagenes/Transportista.gif"
                                                        OnClientClick="ShowTransportistas(); return false;" />
                                                </td>
                                                <td align="center">
                                                    <asp:CheckBox ID="chkUnicaxPedido" runat="server" Checked="false" />
                                                </td>
                                                <td align="center">
                                                    <telerik:RadNumericTextBox ID="txtMontoMinimo" runat="server" ToolTip="Si el valor es 0 indica que no esta sujeta a un monto mínimo"
                                                        MaxValue="999999" MinValue="0" ShowSpinButtons="True" Skin="Vista" Value="0"
                                                        Width="70px">
                                                        <NumberFormat DecimalDigits="0" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cboPromociones" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="1" cellspacing="2" class="style1" style="border: 1px solid #336699">
                                    <tr>
                                        <td>
                                            <asp:Label SkinID="lblBlue" ID="lblPromociones3" Text="Unidades Negocio:" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cboUndNeg" runat="server" Skin="Vista" Width="200px" AllowCustomText="true"
                                                MarkFirstMatch="true" OnClientSelectedIndexChanging="LoadLineas" OnClientItemsRequested="ItemsLoaded" />
                                        </td>
                                        <td>
                                            <asp:Label SkinID="lblBlue" ID="lblPromociones4" Text="Lineas:" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cboLineas" runat="server" AllowCustomText="true" MarkFirstMatch="true"
                                                Skin="Vista" Width="200px" OnItemsRequested="cboLineas_ItemsRequested" OnClientSelectedIndexChanging="LoadFragancias"
                                                OnClientItemsRequested="ItemsLoaded" />
                                        </td>
                                        <td>
                                            <asp:Label SkinID="lblBlue" ID="lblPromociones6" Text="Fragancias:" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cboFragancias" runat="server" Skin="Vista" Width="200px"
                                                AllowCustomText="true" MarkFirstMatch="true" OnItemsRequested="cboFragancias_ItemsRequested"
                                                OnClientSelectedIndexChanging="LoadPresentaciones" OnClientItemsRequested="ItemsLoaded" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label SkinID="lblBlue" ID="lblPromociones7" Text="Presentaciones:" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cboPresentaciones" runat="server" Skin="Vista" Width="200px"
                                                AllowCustomText="true" MarkFirstMatch="true" OnItemsRequested="cboPresentaciones_ItemsRequested"
                                                OnClientSelectedIndexChanging="LoadDefiniciones" OnClientItemsRequested="ItemsLoaded" />
                                        </td>
                                        <td>
                                            <asp:Label SkinID="lblBlue" ID="lblPromociones8" Text="Definiciones:" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cboDefiniciones" runat="server" Skin="Vista" Width="200px"
                                                AllowCustomText="true" MarkFirstMatch="true" OnItemsRequested="cboDefiniciones_ItemsRequested"
                                                OnClientItemsRequested="ItemsLoaded" />
                                        </td>
                                        <td>
                                            <asp:Label SkinID="lblBlue" ID="lblPromociones9" Text="Cantidad:" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Value="1" MaxValue="10"
                                                MinValue="1" ShowSpinButtons="True" Skin="Vista" Width="70px">
                                                <NumberFormat DecimalDigits="0" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" align="right">
                                            <asp:UpdatePanel ID="upAgregarDefinicion" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnAgregarDefinicion" runat="server" SkinID="btnAgregar" Text="Agregar"
                                                        OnClientClick="return ControlarCarga(false);" OnClick="btnAgregarDefinicion_Click"
                                                        Mensaje="Agregando Definición..." />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <!-- Grilla de Configuración Promociones -->
                        <tr style="padding-top: 3px">
                            <td align="center">
                                <asp:UpdatePanel ID="upGrilla" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <telerik:RadGrid ID="grillaComponentes" runat="server" AllowPaging="False" Width="90%"
                                            Font-Size="11px" Font-Names="Segoe UI,tahoma,verdana,sans-serif" GridLines="None"
                                            Skin="Vista" OnItemDataBound="grillaComponentes_ItemDataBound" OnItemCreated="grillaComponentes_ItemCreated">
                                            <MasterTableView GroupLoadMode="Client" AutoGenerateColumns="False" ShowHeadersWhenNoRecords="true"
                                                GroupHeaderItemStyle-HorizontalAlign="Left" GroupsDefaultExpanded="false" ShowGroupFooter="false"
                                                DataKeyNames="Idcomposicion" NoDetailRecordsText="" NoMasterRecordsText="">
                                                <GroupByExpressions>
                                                    <telerik:GridGroupByExpression>
                                                        <SelectFields>
                                                            <telerik:GridGroupByField FieldName="Grupo" HeaderText="Grupo" />
                                                        </SelectFields>
                                                        <SelectFields>
                                                            <telerik:GridGroupByField FieldName="Cantidad" HeaderText="Cantidad" />
                                                        </SelectFields>
                                                        <SelectFields>
                                                            <telerik:GridGroupByField FieldName="TipoComposicion" HeaderText="TipoComposicion" />
                                                        </SelectFields>
                                                        <GroupByFields>
                                                            <telerik:GridGroupByField FieldName="Grupo" SortOrder="Ascending" />
                                                        </GroupByFields>
                                                    </telerik:GridGroupByExpression>
                                                </GroupByExpressions>
                                                <RowIndicatorColumn CurrentFilterFunction="NoFilter" FilterListOptions="VaryByDataType"
                                                    Visible="False">
                                                    <HeaderStyle Width="20px" />
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn CurrentFilterFunction="NoFilter" FilterListOptions="VaryByDataType"
                                                    Resizable="False" Visible="False">
                                                    <HeaderStyle Width="20px" />
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridTemplateColumn UniqueName="Template1" HeaderText="Producto">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblProd"><%# Eval("objProductoHijo.DescripcionCompleta")%></asp:Label>
                                                            <asp:Label runat="server" ID="Label1"><%# Eval("objPresentacion.Descripcion")%></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            Eliminar Grupo Completo:
                                                            <asp:ImageButton runat="server" ID="btnEliminar" ImageUrl="~/Imagenes/Delete.gif"
                                                                Mensaje="Eliminando Grupo..." />
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="80%" />
                                                        <FooterStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn UniqueName="Template1" HeaderText="Eliminar">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="btnEliminar" ImageUrl="~/Imagenes/Delete.gif"
                                                                OnClick="btnEliminar_Click" Mensaje="Eliminando Linea Definición..." />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <ClientSettings>
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Button ID="btnGrabar" runat="server" SkinID="btnBasic" Text="Grabar" OnClientClick="return ControlarCarga(true);"
                                            OnClick="btnGrabar_Click" Mensaje="Grabando Cambios..." />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="50">
        <ProgressTemplate>
            <div id="divBloq1">
            </div>
            <div class="processMessageTooltipGral">
                <table border="0" cellpadding="0" cellspacing="0" style="height: 62px;">
                    <tr>
                        <td align="center">
                            <img alt="a" src="Imagenes/waiting.gif" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <div id="divTituloCarga" style="font-weight: bold; font-family: Tahoma; font-size: 12px;
                                color: Gray; vertical-align: middle">
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</body>
</html>
