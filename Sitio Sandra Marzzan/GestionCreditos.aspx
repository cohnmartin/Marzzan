<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="GestionCreditos.aspx.cs"
    Inherits="GestionCreditos" %>

<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Gestión de Creditos</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
     <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 292px;
        }
        .RadWindow.RadWindow_Sunset.rwNormalWindow.rwTransparentWindow
        {
            z-index: 999900000 !important;
        }
    </style>
</head>

<script type="text/javascript">

    function GuardarCambios() {
        if (Page_ClientValidate()) {
            var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
            var Item = $find("<%= RadGrid1.ClientID%>").get_masterTableView().get_selectedItems();
            if (Item.length > 0) {
                ajaxManager.ajaxRequest("Update");
            }
            else {
                ajaxManager.ajaxRequest("Insert");
            }

            $find("<%=ServerControlWindow1.ClientID %>").CloseWindows();
        }
        return false;
    }

    function LimpiarControles() {
        /// Controles Tipo TextBox
        $find("<%= txtMonto.ClientID%>").set_value(0);
        $find("<%= txtTiempoReposicion.ClientID%>").set_value(0);


        /// Controles Tipo Combos
        $find("<%= cboClientes.ClientID%>").clearSelection();
        $find("<%= cboGrupo.ClientID%>").clearSelection();
    }

    function InitInsert() {

        LimpiarControles();

        $find("<%= RadGrid1.ClientID%>").get_masterTableView().clearSelectedItems();
        $find("<%=ServerControlWindow1.ClientID %>").set_CollectionDiv('divPrincipal');
        $find("<%=ServerControlWindow1.ClientID %>").ShowWindows('divPrincipal', "Nueva Configuracion Creditos");

    }
    var Edicion = false;
    var SelCliente = '';
    function EditLegajo() {

        LimpiarControles();
        var grid = $find("<%= RadGrid1.ClientID%>");
        var MasterTable = grid.get_masterTableView();


        var Item = MasterTable.get_selectedItems();
        if (Item.length > 0) {
            var cellCliente = MasterTable.getCellByColumnUniqueName(Item[0], "ClienteColumn");
            var cellMontoAsignado = MasterTable.getCellByColumnUniqueName(Item[0], "MontoAsignadoColumn");
            var cellTiempoReposicion = MasterTable.getCellByColumnUniqueName(Item[0], "TiempoReposicionColumn");
            var cellActivo = MasterTable.getCellByColumnUniqueName(Item[0], "ActivoColumnEdit");
            var cellClasifica = MasterTable.getCellByColumnUniqueName(Item[0], "ClasificaColumn");

            /// Controles Tipo TextBox
            $find("<%= txtMonto.ClientID%>").set_value(cellMontoAsignado.innerText);
            $find("<%= txtTiempoReposicion.ClientID%>").set_value(cellTiempoReposicion.innerText);


            /// Controles Tipo CheckBox
            var habcred = true;
            if (cellActivo.outerText == 'False') { habcred = false };
            if (document.getElementById("<%= chkActivo.ClientID %>") != null) {
                document.getElementById("<%= chkActivo.ClientID %>").checked = habcred;
            }

            if (cellCliente.outerText != "") {

                Edicion = true;
                SelCliente = cellCliente.outerText.trim()
                var itemGrupo = $find("<%= cboGrupo.ClientID%>").findItemByText(cellClasifica.outerText.trim());
                $find("<%= cboGrupo.ClientID%>").set_selectedItem(itemGrupo);
                itemGrupo.select();

            }
            else {
                $find("<%= cboClientes.ClientID%>").clearSelection();
            }

            $find("<%=ServerControlWindow1.ClientID %>").set_CollectionDiv('divPrincipal');
            $find("<%=ServerControlWindow1.ClientID %>").ShowWindows('divPrincipal', "Edición: " + cellCliente.outerText.trim());
        }
        else {
            radalert("Debe seleccionar un legajo para ver poder editar sus datos", 250, 100, "Selección Legajo");
        }
    }

    function LoadClientes(combo, eventarqs) {
        var cboClientes = $find("<%= cboClientes.ClientID%>");

        var item = eventarqs.get_item();
        cboClientes.set_text("Loading...");

        if (item.get_index() > 0) {
            cboClientes.requestItems(item.get_value(), false);
        }
        else {
            cboClientes.set_text(" ");
            cboClientes.clearItems();
        }
    }

    function ItemsLoaded(combo, eventarqs) {
        if (!Edicion) {
            if (combo.get_items().get_count() > 0) {
                combo.set_text(combo.get_items().getItem(0).get_text());
                combo.get_items().getItem(0).highlight();
            }
            combo.showDropDown();
        }
        else {
            var item = combo.findItemByText(SelCliente);
            if (item) {
                item.select();
            }
            Edicion = false;
        }
    }

</script>

<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server">
        <Scripts>
            <asp:ScriptReference Path="FuncionesComunes.js" />
        </Scripts>
    </asp:ScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
    </telerik:RadAjaxManager>
    <div>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Vista" VisibleTitlebar="true"
            Style="z-index: 100000000" Title="Sub Contratistas">
        </telerik:RadWindowManager>
        <div class="Header_panelContainerSimple">
            <div class="CabeceraInicial">
                <asp:Button ID="btnVolver" runat="server" Text="" SkinID="btnVolver" Visible="false" />
            </div>
        </div>
        <br />
        <table cellpadding="0" cellspacing="5" style="width: 100%;padding-top:0px">
            <tr>
                <td align="center" style="padding-bottom:10px;height: 25px; background: url('imagenes/sprite_webBlue.gif') 0  -300px repeat-x">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="14pt" Font-Names="Sans-Serif"
                        ForeColor="Gray" Text="Gestión Crédito de Clientes" Width="378px"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdPnlCabecera" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <table style="border: thin solid #003399; background-color: #B1E0FA; font-family: Sans-Serif;
                    font-size: 11px; width: 100%; vertical-align: middle">
                    <tr>
                        <td valign="middle" align="right" style="width: 35%">
                            <asp:Label ID="lblGrupos" runat="server" SkinID="lblBlack" Text="Filtro por Grupos:"></asp:Label>
                        </td>
                        <td valign="middle" align="left">
                            <telerik:RadComboBox ID="cboGrupoFiltro" runat="server" Width="410px" EmptyMessage="Todos los Grupos"
                                Skin="WebBlue" AllowCustomText="true" MarkFirstMatch="true" DataTextField="Desc"
                                DataValueField="Desc" AutoPostBack="True" OnSelectedIndexChanged="cboGrupoFiltro_SelectedIndexChanged"
                                Mensaje="Cargando Grupos..." CausesValidation="false">
                                <CollapseAnimation Duration="200" Type="OutQuint" />
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdPnlGrilla" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" AllowSorting="True"
                    ShowStatusBar="True" GridLines="None" Skin="Vista" AllowAutomaticDeletes="True"
                    AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AutoGenerateColumns="False"
                    PageSize="10" OnItemCommand="RadGrid1_ItemCommand" OnNeedDataSource="RadGrid1_NeedDataSource">
                    <MasterTableView DataKeyNames="IdConfCreditos" ClientDataKeyNames="IdConfCreditos"
                        TableLayout="Fixed" CommandItemDisplay="Top" NoMasterRecordsText="No existen registros."
                        HorizontalAlign="NotSet">
                        <CommandItemTemplate>
                            <div style="padding: 5px 5px;">
                                <asp:LinkButton Mensaje="Buscar Legajo...." ID="btnEdit" runat="server" Visible='<%# RadGrid1.EditIndexes.Count == 0 %>'
                                    CausesValidation="false" OnClientClick="EditLegajo(); return false;">
                                <img style="padding-right: 5px;border:0px;vertical-align:middle;" alt="" src="Imagenes/Edit.gif" />Editar</asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton Mensaje="Preparando par nuevo legajo...." ID="btnInsert" runat="server"
                                    CausesValidation="false" Visible='<%# !RadGrid1.MasterTableView.IsItemInserted %>'
                                    OnClientClick="InitInsert(); return false;">
                                <img style="padding-right: 5px;border:0px;vertical-align:middle;" alt="" src="Imagenes/AddRecord.gif" />Insertar</asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton Mensaje="Eliminando Legajo...." ID="btnDelete" OnClientClick="return blockConfirm('Esta seguro que desea eliminar el legajo seleccionado?', event, 330, 100,'','Legajos');"
                                    runat="server" OnClick="btnEliminar_Click" CausesValidation="false">
                                <img style="padding-right: 5px;border:0px;vertical-align:middle;" alt="" src="Imagenes/Eliminar.png" />Eliminar</asp:LinkButton>&nbsp;&nbsp;
                            </div>
                        </CommandItemTemplate>
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="objCliente.Nombre" HeaderText="Cliente" SortExpression="Cliente"
                                UniqueName="ClienteColumn">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MontoAsignado" HeaderText="Monto" SortExpression="MontoAsignado"
                                UniqueName="MontoAsignadoColumn" HeaderStyle-Width="70px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TiempoReposicion" HeaderText="Tiempo Reposicion"
                                SortExpression="TiempoReposicion" UniqueName="TiempoReposicionColumn" HeaderStyle-Width="150px"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="Activo" HeaderText="Activo" SortExpression="Activo"
                                UniqueName="ActivoColumn" HeaderStyle-Width="50px">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridBoundColumn DataField="objCliente.Clasif1" HeaderText="Grupo" SortExpression="Clasifica"
                                UniqueName="ClasificaColumn" HeaderStyle-Width="300px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Activo" UniqueName="ActivoColumnEdit" Display="false">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cboGrupoFiltro" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="upEdicion" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <cc1:ServerControlWindow ID="ServerControlWindow1" runat="server" BackColor="WhiteSmoke"
                    WindowColor="Azul">
                    <ContentControls>
                        <div id="divPrincipal" style="height:120px">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" style="text-align: left;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" SkinID="lblBlue" Text="Grupo:"></asp:Label>
                                    </td>
                                    <td style="padding-left: 5px; padding-right: 5px">
                                        <telerik:RadComboBox ID="cboGrupo" runat="server" Skin="Vista" Width="200px" EmptyMessage="Seleccione Grupo"
                                            ZIndex="922000000" MarkFirstMatch="true" AllowCustomText="true" OnClientSelectedIndexChanging="LoadClientes">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" SkinID="lblBlue" Text="Cliente:"></asp:Label>
                                    </td>
                                    <td style="padding-left: 5px; padding-right: 5px">
                                        <asp:UpdatePanel ID="UpdPnlCboCli" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <telerik:RadComboBox ID="cboClientes" runat="server" Skin="Vista" Width="300px" ZIndex="922000000"
                                                    Mensaje="Buscando Clientes..." AllowCustomText="true" MarkFirstMatch="true" OnItemsRequested="cboClientes_ItemsRequested"
                                                    OnClientItemsRequested="ItemsLoaded" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" SkinID="lblBlue" Text="Monto:"></asp:Label>
                                    </td>
                                    <td align="left" style="padding-left: 5px; padding-right: 5px">
                                        <telerik:RadNumericTextBox ID="txtMonto" runat="server" NumberFormat-DecimalDigits="2"
                                            Skin="Vista" Width="200px" Style="text-align: left">
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtMonto"
                                            Display="Dynamic" ErrorMessage="*" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" SkinID="lblBlue" Text="Tiempo Reposicion:"></asp:Label>
                                    </td>
                                    <td style="padding-left: 5px; padding-right: 5px">
                                        <telerik:RadNumericTextBox ID="txtTiempoReposicion" runat="server" ShowSpinButtons="true"
                                            NumberFormat-DecimalDigits="0" Skin="Vista" Width="50px" Style="text-align: center">
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtTiempoReposicion"
                                            Display="Dynamic" ErrorMessage="*" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                     <asp:Label ID="Label5" runat="server" SkinID="lblBlue" Text="Activo:"></asp:Label>
                                    </td>
                                    <td align="left" style="padding-left: 5px; padding-right: 5px">
                                        <asp:CheckBox ID="chkActivo" Text="" runat="server" />
                                    </td>
                                    <td>
                                    </td>
                                    <td style="padding-left: 5px; padding-right: 5px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardando Datos" Mensaje="Buscando Legajos solicitados..."
                                            CausesValidation="true" OnClientClick="return GuardarCambios(); " SkinID="btnBasic" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentControls>
                </cc1:ServerControlWindow>
            </ContentTemplate>
        </asp:UpdatePanel>
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
    </div>
    </form>
</body>
</html>
