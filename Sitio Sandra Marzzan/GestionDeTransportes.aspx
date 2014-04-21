<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="GestionDeTransportes.aspx.cs"
    Inherits="GestionDeTransportes" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión de Transportes</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 292px;
        }
    </style>
</head>

<script type="text/javascript">
    function ItemsLoaded(combo, eventarqs) {
        if (combo.get_items().get_count() > 0) {
            combo.set_text("")
            combo.showDropDown();
            if (combo._uniqueId == "cboLocalidades") {
                $find("<%=RadAjaxManager1.ClientID%>").ajaxRequest(combo._callbacktext);
            }
        }
        else {
            var FomaPagoCombo = $find("cboFormaPago");
            FomaPagoCombo.set_text("");
        }

    }

    function ControlarCarga() {

        var tooltipo = $find('<%= tooltipCarga.ClientID %>');

        if ($find('<%= cboConceptos.ClientID %>').get_value() == "") {

            tooltipo.set_targetControlID('<%= cboConceptos.ClientID %>');
            tooltipo.set_text("Debe Seleccionar el concepto para poder agregar la definición");
            tooltipo.show();
            return false;

        }

        return true;

    }
</script>

<script type="text/javascript">


    function LoadLocalidades(combo, eventarqs) {
        var LocalidadesCombo = $find("cboLocalidades");
        var FomaPagoCombo = $find("cboFormaPago");
        var TransporteCombo = $find("cboTransporte");
        var ConceptosCombo = $find("cboConceptos");
        var item = eventarqs.get_item();

        LocalidadesCombo.set_text("Loading...");
        FomaPagoCombo.clearSelection();
        TransporteCombo.clearSelection();
        ConceptosCombo.clearSelection();


        if (item.get_index() >= 0) {
            LocalidadesCombo.requestItems(item.get_value(), false);
        }
        else {
            LocalidadesCombo.set_text(" ");
            LocalidadesCombo.clearItems();

            FomaPagoCombo.set_text(" ");
            FomaPagoCombo.clearItems();

            TransporteCombo.set_text(" ");
            TransporteCombo.clearItems();

            ConceptosCombo.set_text(" ");
            ConceptosCombo.clearItems();


        }

    }
    function LoadFormaPago(combo, eventarqs) {
        var FomaPagoCombo = $find("cboFormaPago");
        var TransporteCombo = $find("cboTransporte");
        var ConceptosCombo = $find("cboConceptos");
        var item = eventarqs.get_item();

        if (item.get_index() >= 0) {
            FomaPagoCombo.set_text("Loading...");
            TransporteCombo.clearSelection();
            ConceptosCombo.clearSelection();
            FomaPagoCombo.requestItems(item.get_value(), false);
        }
        else {

            FomaPagoCombo.set_text(" ");
            FomaPagoCombo.clearItems();

            TransporteCombo.set_text(" ");
            TransporteCombo.clearItems();

            ConceptosCombo.set_text(" ");
            ConceptosCombo.clearItems();


        }

    }
    function LoadTransporte(combo, eventarqs) {
        var TransporteCombo = $find("cboTransporte");
        var ConceptosCombo = $find("cboConceptos");
        var item = eventarqs.get_item();

        if (item.get_index() >= 0) {
            TransporteCombo.set_text("Loading...");
            ConceptosCombo.clearSelection();
            TransporteCombo.requestItems(item.get_value(), false);
        }
        else {

            TransporteCombo.set_text(" ");
            TransporteCombo.clearItems();

            ConceptosCombo.set_text(" ");
            ConceptosCombo.clearItems();


        }

    }
    function LoadConceptos(combo, eventarqs) {
        var FomaPagoCombo = $find("cboFormaPago");
        var ConceptosCombo = $find("cboConceptos");
        var item = eventarqs.get_item();

        if (item.get_index() >= 0) {
            ConceptosCombo.set_text("Loading...");
            ConceptosCombo.requestItems(item.get_value() + "|" + FomaPagoCombo.get_value(), false);
        }
        else {

            ConceptosCombo.set_text(" ");
            ConceptosCombo.clearItems();

        }


    }

      
      

      

      
</script>

<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
    </telerik:RadAjaxManager>
    <telerik:RadToolTip runat="server" ID="tooltipCarga" Skin="WebBlue" Sticky="false"
        ShowCallout="true" AutoCloseDelay="4000" Width="220px" Animation="None" Position="BottomCenter"
        ManualClose="true" RelativeTo="Element" ShowEvent="FromCode" Title="Atención!!">
    </telerik:RadToolTip>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50">
        <ProgressTemplate>
            <div class="progressBackgroundFilterBlue">
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
                                Text="Cargando Configuración Promoción...">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
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
                    <table class="style1">
                        <tr>
                            <td align="center">
                                <table cellpadding="0" cellspacing="5" style="width: 80%">
                                    <tr>
                                        <td align="center" style="height: 25px; background: url('imagenes/sprite_webBlue.gif') 0  -300px repeat-x">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="14pt" Font-Names="Sans-Serif"
                                                ForeColor="Gray" Text="Gestión de Transportes" Width="378px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="upAgregarDefinicion" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="1" cellspacing="2" class="style1">
                                            <tr>
                                                <td>
                                                    <asp:Label SkinID="lblBlue" ID="lblPromociones3" Text="Provincias:" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cboProvincias" runat="server" Skin="Vista" Width="200px"
                                                        OnItemsRequested="cboProvincias_ItemsRequested" OnClientSelectedIndexChanging="LoadLocalidades" />
                                                </td>
                                                <td>
                                                    <asp:Label SkinID="lblBlue" ID="lblPromociones4" Text="Localidades:" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cboLocalidades" runat="server" Skin="Vista" Width="200px"
                                                        OnItemsRequested="cboLocalidades_ItemsRequested" OnClientSelectedIndexChanging="LoadFormaPago"
                                                        OnClientItemsRequested="ItemsLoaded" />
                                                </td>
                                                <td>
                                                    <asp:Label SkinID="lblBlue" ID="lblPromociones5" Text="Forma de Pago:" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cboFormaPago" runat="server" Skin="Vista" Width="200px"
                                                        OnItemsRequested="cboFormaPago_ItemsRequested" OnClientSelectedIndexChanging="LoadTransporte"
                                                        OnClientItemsRequested="ItemsLoaded" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label SkinID="lblBlue" ID="lblPromociones6" Text="Transportista:" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cboTransporte" runat="server" Skin="Vista" Width="200px"
                                                        OnItemsRequested="cboTransporte_ItemsRequested" OnClientSelectedIndexChanging="LoadConceptos"
                                                        OnClientItemsRequested="ItemsLoaded" />
                                                </td>
                                                <td>
                                                    <asp:Label SkinID="lblBlue" ID="lblPromociones7" Text="Concepto:" runat="server"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <telerik:RadComboBox ID="cboConceptos" runat="server" Skin="Vista" Width="350px"
                                                        AllowCustomText="true" MarkFirstMatch="true" OnItemsRequested="cboConceptos_ItemsRequested"
                                                        OnClientItemsRequested="ItemsLoaded" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Button ID="btnAgregarDefinicion" runat="server" SkinID="btnAgregar" Text="Agregar"
                                            OnClientClick="return ControlarCarga();" OnClick="btnAgregarDefinicion_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:UpdatePanel ID="upGrilla" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <telerik:RadGrid ID="grillaTransportes" runat="server" AllowPaging="False" Width="90%"
                                            Font-Size="11px" Font-Names="Segoe UI,tahoma,verdana,sans-serif" GridLines="None"
                                            Skin="Vista" ShowGroupPanel="false">
                                            <MasterTableView GroupLoadMode="Client" AutoGenerateColumns="False" ShowHeadersWhenNoRecords="true"
                                                GroupHeaderItemStyle-HorizontalAlign="Left" GroupsDefaultExpanded="false" ShowGroupFooter="false"
                                                DataKeyNames="IdConfTransportes" NoDetailRecordsText="Selecciones una provincia para ver las configuraciones"
                                                NoMasterRecordsText="Selecciones una provincia para ver las configuraciones">
                                                <GroupByExpressions>
                                                    <telerik:GridGroupByExpression>
                                                        <SelectFields>
                                                            <telerik:GridGroupByField FieldName="Provincia" HeaderText="Provincia" />
                                                        </SelectFields>
                                                        <GroupByFields>
                                                            <telerik:GridGroupByField FieldName="Provincia" SortOrder="Descending" />
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
                                                    <telerik:GridTemplateColumn UniqueName="LocalidadColumn" HeaderText="Localidad">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblLocalidad"><%# Eval("Localidad")%></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn UniqueName="FormaPagoColumn" HeaderText="Forma Pago">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblFormaPago"><%# Eval("FormaDePago")%></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn UniqueName="TransporteColumn" HeaderText="Transporte">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblTransporte"><%# Eval("Transporte")%></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn UniqueName="ProductoColumn" HeaderText="Concepto">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblProducto"><%# Eval("objProducto.Descripcion")%></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn UniqueName="Template1" HeaderText="Eliminar">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="btnEliminar" ImageUrl="~/Imagenes/Delete.gif"
                                                                OnClick="btnEliminar_Click" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <ClientSettings AllowDragToGroup="false">
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
    </form>
</body>
</html>
