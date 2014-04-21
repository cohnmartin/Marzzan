<%@ Page Language="C#" EnableEventValidation="false" Theme="SkinMarzzan" AutoEventWireup="true"
    CodeFile="GestionImpresionComprobantes.aspx.cs" Inherits="GestionImpresionComprobantes" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Gestión Impresión de Comprobantes</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="Scripts/jquery.tmpl.1.1.1.js" type="text/javascript"></script>
    <style type="text/css">
        .ContentFiltro
        {
            font: 13px Verdana, Geneva, sans-serif;
            background-color: #F5F5F5;
            border: 3px solid #DBDBDB;
            margin-top: 0px;
            margin-right: 0px;
            padding: 0px;
            width: 70%;
            cursor: pointer;
            font-weight: bold;
            padding: 3px;
        }
        .rowAlt
        {
            background-color: #CCF2FF;
            font-size: 12px;
        }
        .rowSpl
        {
            background-color: White;
            font-size: 12px;
        }
        .alert-JQueryUI
        {
            font-size: 14px;
        }
        .TVista thead
        {
            background-color: White;
        }
        .TVista .Theader
        {
            background-position: 0 -2300px;
            border-top: 0;
            background-image: url('Imagenes/sprite_vista.gif');
            background-repeat: repeat;
            border-style: solid;
            border-width: 1px;
            border-color: #fff #dcf2fc #3c7fb1 #8bbdde;
            color: #333;
            font-family: "segoe ui" ,arial,sans-serif;
            font-size: 12px;
            height: 23px;
            text-align: center;
            padding: 0px 0px 0px 0px;
            line-height: 1em;
        }
        .TVista .tdSimple
        {
            color: #333;
            font: 12px/16px "segoe ui" ,arial,sans-serif;
            border-color: #fff #EFEFEF #fff #EFEFEF;
            border-style: solid;
            border-width: 0 1px 1px;
            padding-left: 2px;
            padding-top: 3px;
            padding-bottom: 3px;
            background-color: White;
            text-align: left;
            vertical-align: middle;
        }
        .TVista .tdSimpleAlt
        {
            color: #333;
            font: 12px/16px "segoe ui" ,arial,sans-serif;
            border-color: #fff #EFEFEF #DBF3FD #EFEFEF;
            border-style: solid;
            border-width: 0 1px 1px;
            padding-left: 2px;
            padding-top: 3px;
            padding-bottom: 3px;
            background-color: #E2F5FE;
            text-align: left;
        }
        .TVista
        {
            border: 1px solid #bbb99d;
        }
    </style>
</head>
<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;" class="main">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"
        LoadScriptsBeforeUI="true">
        <Scripts>
            <asp:ScriptReference Path="~/FuncionesComunes.js" />
        </Scripts>
    </asp:ScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Web20" VisibleTitlebar="true"
        Title="Atención">
        <Windows>
            <telerik:RadWindow ID="RadWindow2" runat="server" Behaviors="Close" Width="570" Title="Generando Reporte..."
                Height="650" Modal="true" Overlay="false" NavigateUrl="ReporteViewerInterno.aspx"
                VisibleTitlebar="true" VisibleStatusbar="false" ShowContentDuringLoad="false"
                Skin="WebBlue">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadToolTip ID="ToolTipClienteSinConf" Skin="Sunset" Width="550px" runat="server"
        Visible="false" Title="Clientes Sin Asistente Asignado" Sticky="false" ManualClose="true"
        Position="Center" Animation="Fade" VisibleOnPageLoad="true" RelativeTo="BrowserWindow"
        Modal="true">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <telerik:RadGrid ID="gvClientes" runat="server" AllowPaging="true" PageSize="20"
                    Width="100%" Font-Size="11px" Font-Names="Tahoma" GridLines="None" Skin="Sunset"
                    OnPageIndexChanged="gvClientes_PageIndexChanged">
                    <MasterTableView ShowFooter="false" AutoGenerateColumns="False">
                        <Columns>
                            <telerik:GridBoundColumn DataField="Nombre" DataType="System.String" ReadOnly="True"
                                UniqueName="NombreCliente">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TipoCliente" DataType="System.String" ReadOnly="True"
                                UniqueName="TipoCliente">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </telerik:RadToolTip>
    <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="Header_panelContainerSimple">
                    <div class="CabeceraInicial">
                    </div>
                </div>
                <div class="CabeceraContent">
                    <table width="95%" border="0" cellspacing="0" style="border-style: ridge; border-width: thin">
                        <tr>
                            <td colspan="2" style="color: #0066CC; font-family: Sans-Serif; font-size: 12px;
                                text-transform: capitalize">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100px; color: #993300; font-family: Sans-Serif; font-size: 12px">
                                            <asp:Label ID="Label1" runat="server" Text="Grupos:" SkinID="lblBlue"></asp:Label>
                                        </td>
                                        <td align="left" colspan="3">
                                            <asp:TextBox Width="300px" ID="txtGrupo" runat="server"></asp:TextBox>
                                        </td>
                                         <td style="width: 120px; color: #993300; font-family: Sans-Serif; font-size: 12px">
                                            <asp:Label ID="Label3" runat="server" Text="Cliente Especifico:" SkinID="lblBlue"></asp:Label>
                                        </td>
                                        <td align="left" colspan="3">
                                            <asp:TextBox Width="300px" ID="txtClientes" runat="server"></asp:TextBox>
                                        </td>
                                       
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td colspan="2" style="color: #0066CC; font-family: Sans-Serif; font-size: 12px;
                                text-transform: capitalize">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100px; color: #993300; font-family: Sans-Serif; font-size: 12px">
                                            <asp:Label ID="UserNameLabel" runat="server" Text="Revendedor:" SkinID="lblBlue"></asp:Label>
                                        </td>
                                        <td align="left" colspan="3">
                                            <telerik:RadComboBox ID="cboConsultores" runat="server" Width="290px" EmptyMessage="Todos los revendedores"
                                                EnableLoadOnDemand="True" ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                                OnItemsRequested="cboProfesionales_ItemsRequested" Skin="Vista">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="color: #0066CC; font-family: Sans-Serif; font-size: 12px;
                                text-transform: capitalize">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100px; color: #993300; font-family: Sans-Serif; font-size: 12px">
                                            <asp:Label SkinID="lblBlue" ID="lblPromociones3" Text="Transporte:" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 290px;">
                                            <telerik:RadComboBox ID="cboTransporte" runat="server" Skin="Vista" Width="100%" />
                                        </td>
                                        <td style="padding-left: 15px; width: 70px; color: #993300; font-family: Sans-Serif;
                                            font-size: 12px" colspan="2">
                                            &nbsp;
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="color: #0066CC; font-family: Sans-Serif; font-size: 12px;
                                text-transform: capitalize">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100px; color: #993300; font-family: Sans-Serif; font-size: 12px">
                                            <asp:Label ID="UserNameLabel0" runat="server" Text="Fecha Incial:" SkinID="lblBlue"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 290px;">
                                            <telerik:RadDatePicker ID="txtFechaInicial" runat="server" Skin="Web20" Width="138px"
                                                Culture="Spanish (Argentina)">
                                                <DateInput ID="DateInput1" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Web20"
                                                    Skin="Web20" SelectionOnFocus="SelectAll" ShowButton="True">
                                                </DateInput>
                                                <Calendar ID="Calendar1" runat="server" Skin="Web20">
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td style="padding-left: 15px; width: 70px; color: #993300; font-family: Sans-Serif;
                                            font-size: 12px">
                                            <asp:Label ID="Label2" runat="server" Text="Fecha Final:" SkinID="lblBlue"></asp:Label>
                                        </td>
                                        <td align="left" colspan="3">
                                            <telerik:RadDatePicker ID="txtFechaFinal" runat="server" Skin="Web20" Width="138px"
                                                Culture="Spanish (Argentina)">
                                                <DateInput ID="DateInput2" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Web20"
                                                    Skin="Web20" SelectionOnFocus="SelectAll" ShowButton="True">
                                                </DateInput>
                                                <Calendar ID="Calendar2" runat="server" Skin="Web20">
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-left: 98px; color: #0066CC; font-family: Sans-Serif;
                                font-size: 12px; text-transform: capitalize">
                                <asp:CheckBox ID="chkImpresas" runat="server" Text="Incluir Pedido Impresos" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td colspan="2" style="padding-left: 55px; color: #0066CC; font-family: Sans-Serif;
                                font-size: 12px; text-transform: capitalize">
                                <asp:CheckBox ID="chkBolsos" runat="server" Text="Incluir Pedido Cuenta Bolsos" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Button ID="btnGenerar" runat="server" Text="Impresiones" SkinID="btnBasic" Width="77px"
                                            OnClientClick="return CargarIds();" OnClick="btnGenerar_Click" />
                                        <asp:Button ID="btnActDatos" runat="server" Text="Act. Datos" SkinID="btnBasic" OnClick="btnActDatos_Click"
                                            Width="77px" ToolTip="Actualiza solo los datos de Bejerman segun los datos del pedido" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td align="right">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" SkinID="btnBasic" Width="77px"
                                            OnClientClick="return ControlarDatos();return false;" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="color: #993300; font-family: Sans-Serif; font-size: 12px" colspan="2">
                                <asp:HiddenField Value="" ID="hdf_ids" runat="server" />
                                <div id="divtbl">
                                </div>
                                <asp:UpdatePanel ID="upResultado" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <telerik:RadGrid ID="GrillaResultados" runat="server" GridLines="None" Skin="Vista"
                                            Width="100%" AllowMultiRowSelection="true" Visible="false">
                                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="IdCabeceraPedido" ClientDataKeyNames="IdCabeceraPedido"
                                                ShowHeadersWhenNoRecords="true" TableLayout="Fixed" NoMasterRecordsText="No hay resultados...">
                                                <RowIndicatorColumn Visible="False">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn Visible="False" Resizable="False">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridClientSelectColumn UniqueName="OrdenSelectColumn" ItemStyle-Width="20px"
                                                        HeaderStyle-Width="20px">
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                        <ItemStyle Width="20px"></ItemStyle>
                                                    </telerik:GridClientSelectColumn>
                                                    <telerik:GridBoundColumn DataField="Nro" HeaderText="Nro" SortExpression="Nro" UniqueName="Nro">
                                                        <ItemStyle Width="55px" HorizontalAlign="Center" />
                                                        <HeaderStyle Width="55px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Solicitante a:" UniqueName="SolicitanteColumn">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1sd" runat="server" Style="text-transform: capitalize"><%# Eval("Solicitante").ToString().ToLower()%></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                                        <ItemStyle Width="110px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Revendedor" UniqueName="ClienteColumn">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1ff" runat="server" Style="text-transform: capitalize"><%# Eval("Destinatario").ToString().ToLower()%></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="130px" />
                                                        <HeaderStyle Width="130px" HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="FechaPedido" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy HH:mm}"
                                                        HeaderText="Fecha" SortExpression="FechaPedido" UniqueName="FechaPedido">
                                                        <ItemStyle Width="100%" HorizontalAlign="Center" />
                                                        <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TipoPedido" HeaderText="Tipo" SortExpression="Tipo"
                                                        UniqueName="TipoPedido">
                                                        <ItemStyle Width="25px" HorizontalAlign="Center" />
                                                        <HeaderStyle Width="25px" HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="MontoTotal" DataType="System.Decimal" HeaderText="Total"
                                                        SortExpression="MontoTotal" UniqueName="MontoTotal">
                                                        <ItemStyle Width="65px" HorizontalAlign="Center" />
                                                        <HeaderStyle Width="65px" HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Forma Pago" UniqueName="FormaPagoColumn">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1fff" runat="server"><%# Eval("FormaPago")%></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Direccion Entrega" UniqueName="DireccionEntregaColumn"
                                                        Display="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1ffg" runat="server"><%# Eval("DireccionCompleta")%></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="260px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="Estado" HeaderText="Estado" SortExpression="Nro"
                                                        UniqueName="EstadoColumn">
                                                        <HeaderStyle Width="100px" />
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
<script type="text/javascript">
    var ctrGrupo;

    jQuery(function () {

        options = {
            serviceUrl: 'ASHX/LoadGrupos.ashx',
            width: '384',
            minChars: 3,
            showOnFocus: true,
            showInit: false,
            zIndex: 922000000
        };
        ctrGrupo = $('#' + "<%= txtGrupo.ClientID %>").autocomplete(options);


        options = {
            serviceUrl: 'ASHX/LoadClientes.ashx',
            width: '384',
            minChars: 3,
            showOnFocus: false,
            showInit: false,
            zIndex: 922000000
        };
        
        ctrClientes = $('#' + "<%= txtClientes.ClientID %>").autocomplete(options);


    });

    function ShowComprobante() {
        if (ControlarDatos()) {
            var grid = $find("<%= GrillaResultados.ClientID %>");
            if (grid.MasterTableView.get_dataItems().length > 0) {
                var oWnd = radopen('ReporteViewerInterno.aspx', 'RadWindow2');
                //window.open('ReporteViewerInterno.aspx', '', 'titlebar=no,toolbar=0,menubar=0,scrollbars=0,resizable=0,width=1,height=1');
                return false;
            }
            else {

                radalert('Debe haber por lo menos un pedido para generar la impresión.');
                return false;
            }
        }
    }

    function ControlarDatos() {

        var comboTransporte = $find("<%= cboTransporte.ClientID%>");
        var FechaInicial = $find("<%=txtFechaInicial.ClientID%>");
        var FechaFinal = $find("<%=txtFechaFinal.ClientID%>");


        if (comboTransporte.get_value() == 0) {
            radalert('Debe Seleccionar un transportista para consultar las notas de pedido');
            return false;
        }


        if (FechaInicial.get_selectedDate() == null) {
            radalert('Debe Seleccionar la fecha incial para consultar las notas de pedido');
            return false;
        }

        if (FechaFinal.get_selectedDate() == null) {
            radalert('Debe Seleccionar la fecha final para consultar las notas de pedido');
            return false;
        }

        if (FechaFinal.get_selectedDate() < FechaInicial.get_selectedDate()) {
            radalert('La fecha final no puede ser menor a la fecha inicial.');
            return false;
        }


        var cliente = ctrClientes.get_SelectedValue() != undefined && ctrClientes.get_SelectedValue() != "" ? ctrClientes.get_SelectedValue() : null;

        var grupo = ctrGrupo.get_SelectedValue() != undefined && ctrGrupo.get_SelectedValue() != "" ? ctrGrupo.get_SelectedValue() : null;

        var impresas = $("#<%=chkImpresas.ClientID %>").is(":checked");

        ShowWaiting("Buscando Pedidos...");

        PageMethods.ConsultarPedidos(grupo, cliente, comboTransporte.get_value(), FechaInicial.get_selectedDate().format("dd/MM/yyyy"), FechaFinal.get_selectedDate().format("dd/MM/yyyy"), impresas, onSuccess, function () { });

    }

    function onSuccess(response) {

        datos = response["Datos"];

        var t = [
        { "header": "<input type='checkbox' id='chkAll' onclick='checkAll(this);' />" },
        { "header": "Nro" },
        { "header": "Solicitante a:" },
        { "header": "Revendedor" },
        { "header": "Fecha" },
        { "header": "Tipo" },
        { "header": "Total" },
        { "header": "Forma Pago" },
        { "header": "Impresión"}];

        var templates = {
            th: '<th class="Theader">#{header}</th>',
            td: '<tr style="cursor:pointer" >' +
                    '<td align="center" style="width:30px" ><input type="checkbox" id="chkSeleccion" class="seleccion" idCab="#{IdCabeceraPedido}" /></td>' +
                    '<td align="center" style="width:50px" style="padding-left:5px">#{Nro}</td>' +
                    '<td align="left" id="Solicitante" >#{Solicitante}</td>' +
                    '<td align="left" id="Cliente" >#{Destinatario}</td>' +
                    '<td align="center" style="width:110px" id="FechaPedido">#{FechaPedido}</td>' +
                    '<td align="center" style="width:70px" id="Tipo">#{TipoPedido}</td>' +
                    '<td align="center" style="width:70px" id="MontoTotal">#{MontoTotal}</td>' +
                    '<td align="center" style="width:120px" id="FormaPago">#{FormaPago}</td>' +
                    '<td align="center" style="width:70px" id="NroImpresion">#{NroImpresion}</td>' +
                    '<td style="width:70px;display:none" id="IdCabeceraPedido" >#{IdCabeceraPedido}</td>' +
                '</tr>'
        };

        var table = '<table class="TVista" id="tbl" width="100%" cellpadding="0" cellspacing="0" style="font-size:13px;"><thead><tr>';

        /// Genero la fila de encabezado
        $.each(t, function (key, val) {
            table += $.tmpl(templates.th, val);
        });

        table += '</tr></thead><tbody>';


        if (datos.length > 0) {
            /// Genero las filas del body
            row = 0;
            for (var i = 0; i < datos.length; i++) {
                table += $.tmpl(templates.td, datos[row]);
                row++;
            }
        }
        else {
            table += "<td colspan='9' class='tdSimple' align='center'>No existen pedidos para los filtros seleccionados</td>";
        }

        table += '</tbody></table>';

        /// Asigno la tabla generada al div para dibujarla
        $("#divtbl")[0].innerHTML = table;
        $("#tbl tbody tr:odd").addClass("tdSimple");
        $("#tbl tbody tr:even").addClass("tdSimpleAlt");

        HideWaiting();
    }

    function checkAll(obj) {

        if ($(obj).attr('checked') != undefined) {
            $('.seleccion').removeAttr('checked');
            $('.seleccion').attr('checked', 'checked');
        }
        else {
            $('.seleccion').removeAttr('checked');
        }

    }

    function CargarIds() {

        if ($('.seleccion:checked').length == 0) {
            alert("Se tiene que seleccionar por lo menos un pedido");
            return false;
        }
        else {

            ShowWaiting("Generando PDF...");

            var ids = new Array();
            $('.seleccion:checked').each(function () {
                ids.push($(this).attr('idCab'));
            });

            var valores = ids.join(',');
            $("#<%= hdf_ids.ClientID %>").val(valores);
            return true;
        }



    }

</script>
