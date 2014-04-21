﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NotaPedidoAlternativa.aspx.cs" Inherits="NotaPedidoAlternativa" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="TotalizadorPedido.ascx" TagName="TotalizadorPedido" TagPrefix="uc1" %>
<%@ Register Src="TotalizadorPromos.ascx" TagName="TotalizadorPromos" TagPrefix="uc3" %>
<%@ Register Src="grillaDirecciones.ascx" TagName="grillaDirecciones" TagPrefix="uc4" %>
<%@ Register Src="TotalizadorNivel.ascx" TagName="TotalizadorNivel" TagPrefix="uc5" %>
<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="cc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <title>Pedido Sandra Marzzan</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" src="ValidacionesCliente.js"></script>

    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>

    <link href="MySkin/ToolTip.MySkin.css" rel="stylesheet" type="text/css" />
    <link href="MySkinTab/TabStrip.MySkinTab.css" rel="stylesheet" type="text/css" />
    <link href="MySkinToolTip/ToolTip.MySkinToolTip.css" rel="stylesheet" type="text/css" />
    <link href="ImagenesPromos/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .lblEncabezadoDesc
        {
            text-transform: capitalize;
            font-family: Tahoma;
            font-size: 12px;
        }
        .lblEncabezado
        {
            font-family: Tahoma;
            font-size: 10px;
            font-weight: bold;
        }
        .MouseOverStyle
        {
            color: #363636;
            border: 1px solid #99defd;
            background: #f6fbfd url(Imagenes/ItemHoveredBg.gif.ashx) repeat-x left bottom;
            cursor: hand;
        }
        .MouseOutStyle
        {
            background-color: Transparent;
            color: Black;
            cursor: hand;
        }
        .GrillaProductos
        {
            font-family: "Segoe UI" ,tahoma,verdana,sans-serif;
            padding: 2px;
            font-size: 11px;
        }
        .GrillaProductos td
        {
            border: 1px solid;
            border-color: #fcfcfc #fff #fcfcfc #ededed;
        }
        div.wrapper
        {
        }
        .lblBasic
        {
            font-family: "Segoe UI" ,tahoma,verdana,sans-serif;
            font-size: 11px;
            color: Black;
        }
        .multiPage
        {
            border: 1px solid #94A7B5;
            background-color: #F0F1EB;
            text-align: left;
            height: 450px;
        }
        .UseHand
        {
            cursor: hand;
        }
    </style>
</head>

<script type="text/javascript">



    var sumInputCantidadGrupo = null;
    var sumInputCatidadGenenral = null;
    var sumImputValorGeneral = null;
    var sumInputValorLinea = null;
    var sumInputValoTotalGrupo = null;

    var tempCantidadGrupo = 0;
    var tempCantidadGeneral = 0;

    var tempValueLinea = 0;
    var tempValueGrupo = 0;
    var tempValueGeneral = 0;
    var tempValorUnitario = 0;

    var closeWindows = false;


    function IrPaginaPagoFacil() {

        window.open("http://www.e-pagofacil.com", "Pago Facil");

    }
    function updateGrid(result) {

        var tableView = $find("<%= grillaPrueba.ClientID %>").get_masterTableView();
        tableView.set_dataSource(result);
        tableView.dataBind();

        var grid = $find("<%= grillaPrueba.ClientID %>");
        grid.repaint();

        OcultarLoading();

    }
    function ObtenerValoreCargados() {


        //        if (lblTotal != "") {
        //            MostarLoading();
        //            WebServiceHelper.WebServiceHelper_NuevoProducto(presentaciones + "@" + Valores + "@" + "N", updateGrid);
        //        }
        //        else {
        var Valores = "";
        var presentaciones = "";
        var filas = document.getElementById("tblCargaPedido").childNodes[0].childNodes;
        for (var i = 1; i < filas.length; i++) {
            var columnas = filas[i].childNodes;
            for (var j = 1; j < columnas.length; j++) {
                if (parseInt(columnas[j].innerText) > 0) {
                    presentaciones += columnas[j].getAttribute("Codigo").toString() + "|";
                    Valores += columnas[j].innerText + "|";
                    //columnas[j].innerText = "0";
                }
            }

        }

        if (Valores != "") {
            MostarLoading();

            WebServiceHelper.WebServiceHelper_NuevoProducto(presentaciones + "@" + Valores + "@" + "N", updateGrid);
            return false;

            $find("<%=RadAjaxManager1.ClientID%>").ajaxRequest(presentaciones + "@" + Valores + "@" + "N");

        }
        else {
            var toolTip = $find("toolTipPrecios");
            toolTip.set_targetControlID(event.srcElement.id);
            toolTip.set_text("Debe Cargar las cantidades de los productos para poder realizar la solicitud")
            toolTip.show();
        }
        //        }

        return false;
    }

    function MostarLoading() {
        document.getElementById("DivBloque").style.display = "block";
        document.getElementById("DivBarra").style.display = "block";
    }

    function OcultarLoading() {
        document.getElementById("DivBloque").style.display = "none";
        document.getElementById("DivBarra").style.display = "none";
    }


    function GetEncabezado() {

        var idconsultor = $find("<%= cboConsultores.ClientID%>").get_value();
        var formaPago = $find("<%= cboFormaPago.ClientID%>").get_text();

        MostarLoading();

        $.ajax({ type: "POST",
            data: "{ IdConsultor: '" + idconsultor + "', IdFormaDePago:'" + formaPago + "'}",
            url: "NotaDePedido.aspx/GetDireccionEntrega",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            context: document.body,
            success: function(Valores) { CargarEncabezado(Valores) },
            error: function(msg) { alert("Existe un error con la aplicación: " + msg); }
        });

    }

    function CargarEncabezado(Valores) {

        if (Valores.d.length > 0) {
            var datosRecividos = Valores.d.split('|');
            var datosDireccion = datosRecividos[0].split('@');
            var datosJerarquia = datosRecividos[2].split('@');



            document.getElementById("lblDireccionEntrega").innerText = datosDireccion[0];
            document.getElementById("lblCalle").innerText = datosDireccion[1];


            document.getElementById("lblTransporte").innerText = datosRecividos[1];
            document.getElementById("lblTransporteHidden").value = datosRecividos[1];

            document.getElementById("lblResponsable").innerText = datosJerarquia[0];
            document.getElementById("lblSubCoor").innerText = datosJerarquia[1];


            document.getElementById("lblFechaImp").innerText = datosRecividos[3];
            document.getElementById("lblNroNotaImp").innerText = datosRecividos[4];


            document.getElementById("lblSaldoCta").innerText = datosRecividos[5];
            document.getElementById("lblUltimaAct").innerText = datosRecividos[6];


            OcultarLoading();

            $find("<%=RadAjaxManager1.ClientID%>").ajaxRequest("Direcciones");
        }
        else {

            OcultarLoading();
            alert('El revendedor seleccionado no posee direcciones de entrega para utilizar, por favor tome contacto con el personal de aistencia.');

            document.getElementById("lblDireccionEntrega").innerText = "";
            document.getElementById("lblCalle").innerText = "";
            document.getElementById("lblTransporte").innerText = "";
            document.getElementById("lblTransporteHidden").value = "";
            document.getElementById("lblResponsable").innerText = "";
            document.getElementById("lblSubCoor").innerText = "";
            document.getElementById("lblSaldoCta").innerText = "";
            document.getElementById("lblUltimaAct").innerText = "";


        }
    }


    function AlertaSaldoInsuficiente(msn) {
        radalert(msn, 400, 100, 'Saldo Insuficiente');
    }
    function AlertaEdicionInvalida() {

        alert('La operacion que intenta realizar no esta permitida, la solicitud del pedido ya fue enviada.');
        window.close();

    }

    function AlertaDireccion() {
        radalert('Por favor ratifique la dirección de entrega para el pedido.', 300, 100, "Dirección de Entrega");
    }


    function AlertaTransporte() {

        var oWnd = radalert('El pedido no se puede completar ya que no esta definido el transporte, por favor tome contacto con el personal de asistencia.');

    }

    function AlertaMinimoRequerido(montominimo) {

        var oWnd = radalert('El pedido que intenta realizar no alcanza el monto mínimo de $' + montominimo + ', por favor verifique que los productos solicitados sean correctos.');

    }

    function AlertaMinimoRequeridoProvincia(montominimo) {

        var oWnd = radalert('El pedido que intenta realizar no alcanza el monto mínimo de $' + montominimo + ', por favor verifique que los productos solicitados sean correctos.');
    }


    function AlertaSinDireccion() {

        var oWnd = radalert('El revendedor que ha seleccionado no posee ninguna dirección de entrega por lo que no se podrán hacer pedido para el mismo, por favor tome contacto con el personal de asistencia.');

    }




    function AlertaGrabacion(id) {

        //radalert('El pedido se ha realizado con exito.');
        var oWnd = radopen('ReportViewer.aspx?Id=' + id, 'RadWindow2');

    }




    function ControlarDatos(accion) {
        var promosCompletas = $get("TotalizadorPromos1_PromosCompletasHiden").value;
        var combo = $find("<%= cboConsultores.ClientID%>");
        var comboFP = $find("<%= cboFormaPago.ClientID%>");

        var totalProductos = $get("<%= txtTotalGeneral.ClientID%>");

        if (combo.get_value() == 0) {
            radalert('Debe Seleccionar el revendedor antes de poder ' + accion + ' el pedido.', 300, 100, 'Consultor Requerido');
            return false;
        }
        else if (promosCompletas == "false") {
            radalert('Existen promociones que no tienen seleccionado la fragancia del producto que lleva de regalo.', 300, 100, 'Producto de Regalo');
            return false;

        }
        else if (totalProductos.value == "0") {
            radalert('Debe solicitar al menos un producto para poder  ' + accion + ' el pedido.', 300, 100, 'Productos del Pedido');
            return false;
        }

    }



    function MarcarCambios() {

        document.getElementById("hiddenHayCambios").value = "true";
    }


    function IniciarDetenerPromosActivas(sender, args) {
        var TabName = args.get_tab().get_text();
        if (TabName == 'PROMOS ACTIVAS') {
            slideShow.init();
            slideShow.lim();
        }
        else {
            slideShow.stopnav();
        }

    }

    function responseEnd(sender, eventArgs) {
        document.getElementById("DivResumen").style.display = "none";
        document.getElementById("DivPromos").style.display = "none";
        var tab;
        OcultarLoading();


        var tabStrip = $find("<%= RadTabStrip2.ClientID %>");
        var PormosPosibles = document.getElementById("TotalizadorPromos1_rowOportunidades");
        //        if (PormosPosibles != null) {
        //            tab = tabStrip.findTabByText('PROMOS GANADAS');
        //        
        //         }
        //        else {
        //            tab = tabStrip.findTabByText('RESUMEN PEDIDO');

        //        }


        if (PormosPosibles != null) {
            tab = tabStrip.findTabByText('PROMOS GANADAS');
            tab.select();
        }


        if (document.getElementById("tblCargaPedido") != null) {
            var filas = document.getElementById("tblCargaPedido").childNodes[0].childNodes;
            for (var i = 1; i < filas.length; i++) {
                var columnas = filas[i].childNodes;
                for (var j = 1; j < columnas.length; j++) {
                    if (parseInt(columnas[j].innerText) > 0) {
                        columnas[j].innerText = "0";
                    }
                }

            }
        }

    }

    function ShowDirecciones() {
        var tooltip = $find("<%= ToolTipDirecciones.ClientID%>");
        tooltip.show();
    }

    var idTimerImagenes;
    var indexImagen = 1;

    function ShowImagen(Imagenes) {

        var RutasImagenes = Imagenes.split('|');

        var div = document.getElementById("divImagen");
        div.filters[0].Apply();
        div.style.background = "transparent url('" + RutasImagenes[indexImagen] + "') 0 0 no-repeat";
        div.filters[0].Play();


        if (indexImagen == RutasImagenes.length - 1) {
            indexImagen = 0;
        }
        else {
            indexImagen++;
        }

        idTimerImagenes = self.setTimeout("ShowImagen('" + Imagenes + "')", 5000);

    }

    function CargarListaProductos(sender, args) {
        var divImagenPromoInicial = document.getElementById("divImagenPromoInicial");
        divImagenPromoInicial.style.display = "none";

        var node = args.get_node();
        var attributes = node.get_attributes();
        var CargarControl = attributes.getAttribute("Cargar");
        var rutaImagen = attributes.getAttribute("RutaImagen");
        var rutasImagenes = rutaImagen.split('|');

        if (rutasImagenes.length > 1) {

            var div = document.getElementById("divImagen");
            div.filters[0].Apply();
            div.style.background = "transparent url('" + rutasImagenes[0] + ".ashx') 0 0 no-repeat";
            div.filters[0].Play();

            if (idTimerImagenes == null)
                idTimerImagenes = self.setTimeout("ShowImagen('" + rutaImagen.ashx + "')", 5000);

        }
        else {

            clearTimeout(idTimerImagenes);
            idTimerImagenes = null;

            var div = document.getElementById("divImagen");
            div.filters[0].Apply();
            div.style.background = "transparent url('" + rutaImagen + ".ashx') 0 0 no-repeat";
            div.filters[0].Play();
        }

        if (Boolean.parse(CargarControl)) {
            searchIndex(node.get_value());
        }
        else {

            searchIndex("");
            if (node.get_expanded()) {
                node.collapse();
            }
            else {
                node.expand();
            }
        }
    }

    function CargarImagen(sender, args) {

        var divImagenPromoInicial = document.getElementById("divImagenPromoInicial");
        divImagenPromoInicial.style.display = "none";

        var node = args.get_node();

        if (node.get_nodes().get_count() > 0) {
            var attributes = node.get_attributes();
            var rutaImagen = attributes.getAttribute("RutaImagen");
            var div = document.getElementById("divImagen");
            if (div.style.background != "transparent url('" + rutaImagen + ".ashx') no-repeat") {
                div.filters[0].Apply();
                div.style.background = "transparent url('" + rutaImagen + ".ashx') no-repeat";
                div.filters[0].Play();
            }

            searchIndex("");
        }
    }
    function BeginPedidoShow() {
        document.getElementById("DivResumen").style.display = "block";
        document.getElementById("DivSolicitud").style.display = "block";
        MarcarCambios();
    }

    function HideToolTip() {
        var tooltip = $find("RadToolTip3");
        tooltip.hide();
    }


    function OnClientClose(oWnd, args) {

        if (oWnd.argument) {

            document.getElementById("DivResumen").style.display = "block";
            document.getElementById("DivPromos").style.display = "block";
            var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
            ajaxManager.ajaxRequest(oWnd.argument.IdsPresentaciones + "@" + oWnd.argument.Valores + "@" + oWnd.argument.CodigoEdicion + "@" + "E");

        }
    }

    function GrabacionFinalPendiente(id) {
        closeWindows = true;
        var oWnd = radopen('ReportViewer.aspx?Id=' + id, 'RadWindow2');

    }
    function OnClientClosePendiente(oWnd, args) {

        if (closeWindows) {
            window.close();
            closeWindows = false;
        }

    }

    function EditarPedido(id, cantidad, codigoEdicion, productoDesc) {

        if (xmlDoc == null) { loadIndex(); }
        var allitems = xmlDoc.getElementsByTagName("item");
        results = new Array;


        for (var i = 0; i < allitems.length; i++) {
            // see if the XML entry matches the search term,
            // and (if so) store it in an array
            var name = allitems[i].getAttributeNode("IdProducto");
            if (name != null && name.value == id) {
                results.push(allitems[i]);
                break;
            }
        }


        var codigos = results[0].getAttributeNode("Codigos").value;
        var descripciones = results[0].getAttributeNode("Descripciones").value;
        var precios = results[0].getAttributeNode("Precios").value;
        var ProductName = productoDesc;





        $get("<%=lblProducto.ClientID %>").innerText = ProductName;
        $get("<%=lblCodigoEdicion.ClientID %>").innerText = codigoEdicion;
        $get("<%=lblCodigo.ClientID %>").innerText = codigos;
        $get("<%=lblIdProducto.ClientID %>").innerText = id;



        $find("<%=txtCantidadEdicion.ClientID %>").set_value(cantidad);
        $find("<%=ServerControlWindow1.ClientID %>").set_CollectionDiv('divPrincipal');
        $find("<%=ServerControlWindow1.ClientID %>").ShowWindows('divPrincipal', "Edición Cantidad: " + productoDesc);


        //var oWnd = radopen('CargaProducto.aspx?Codigos=' + codigos + "&descripciones=" + descripciones + "&precios=" + precios + "&id=" + id + "&ProductName=" + ProductName + "&Cantidad=" + cantidad + "&CodigoEdicion=" + codigoEdicion, 'RadWindow1');
        return false;


    }

    function TerminarEdicion() {

        var CodigoEdicion = $get("<%=lblCodigoEdicion.ClientID %>").innerText;
        var IdsPresentaciones = $get("<%=lblCodigo.ClientID %>").innerText;
        var Valores = $find("<%=txtCantidadEdicion.ClientID %>").get_value();
        var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
        ajaxManager.ajaxRequest(CodigoEdicion + "|@" + Valores + "@" + CodigoEdicion + "@" + "E");
        $find("<%=ServerControlWindow1.ClientID %>").CloseWindows();
        MostarLoading();
        return false;


    }
   
   
    
</script>

<body style="background-image: url(Imagenes/repetido.jpg.ashx); margin-top: 1px;
    background-repeat: repeat-x; background-color: White;">
    <form id="form1" runat="server">
    <input id="HiddenPoseeCartuchera" type="hidden" runat="server" value="false" />
    <!-- 
        Productos que se estan Ocultando a mano en el Js de Productos
        Ficha Tal cual : IdProducto 2862 
        Bolsa Regalo: IdProducto 2631
        Diferencia Kit Alta Cosmetica x 5 u: IdProducto 3788 
        Set de cremas p-Viviana Cánepa: 3744	   
        Incorporación Nº1 Cánepa sin Plumines:3784	
    -->
    <input id="HiddenIncorporacion0" type="hidden" runat="server" value="@" />
    <input id="HiddenProductosOcultos" type="hidden" runat="server" value="2862|1633|3744|3784|3788|3789" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="ProductosV1.js" />
        </Scripts>
        <Services>
            <asp:ServiceReference Path="~/WebServiceHelper.asmx" />
        </Services>
    </asp:ScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Web20" VisibleTitlebar="true"
        Title="Atención">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close" Width="570" OnClientClose="OnClientClose"
                Height="300" Modal="true" NavigateUrl="CargaProducto.aspx" VisibleTitlebar="true"
                Title="Ingrese la cantidad a solicitar" VisibleStatusbar="false" ShowContentDuringLoad="false"
                Skin="WebBlue">
            </telerik:RadWindow>
            <telerik:RadWindow ID="RadWindow2" runat="server" Behaviors="Close" Width="870" Title="Comprobante Pedido"
                OnClientClose="OnClientClosePendiente" Height="600" Modal="true" NavigateUrl="ReportViewer.aspx"
                VisibleTitlebar="true" VisibleStatusbar="false" ShowContentDuringLoad="false"
                Skin="WebBlue">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <table cellpadding="0" cellspacing="0" style="width: 100%" border="0" align="center"
        runat="server" id="tblPrincipal">
        <tr>
            <td align="center" style="width: 100%;">
                <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="upCabeceraPagina" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="Header_panelContainerSimple">
                                        <div class="CabeceraContent">
                                            <asp:Button ID="btnVolver" runat="server" Text="" SkinID="btnVolver" OnClick="btnVolver_Click"
                                                Visible="false" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%; height: 80%" align="center" valign="top">
                <div>
                    <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="upTab" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <telerik:RadTabStrip ID="RadTabStrip2" runat="server" Width="100%" MultiPageID="RadMultiPrincipal"
                                            Skin="Web20" Align="Center" OnClientTabSelecting="IniciarDetenerPromosActivas">
                                            <Tabs>
                                                <telerik:RadTab Text="RESUMEN PEDIDO" PageViewID="PageViewResumen" Selected="True">
                                                </telerik:RadTab>
                                                <telerik:RadTab Text="PRODUCTOS" PageViewID="PageViewProductos">
                                                </telerik:RadTab>
                                                <telerik:RadTab Text="PROMOS GANADAS" PageViewID="PageViewPromos">
                                                </telerik:RadTab>
                                                <telerik:RadTab Text="PREMIOS PENDIENTES" PageViewID="PageViewNiveles">
                                                </telerik:RadTab>
                                                <telerik:RadTab Text="PROMOS ACTIVAS" PageViewID="PageViewPromosActivas">
                                                </telerik:RadTab>
                                            </Tabs>
                                        </telerik:RadTabStrip>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <telerik:RadMultiPage runat="server" ID="RadMultiPrincipal" SelectedIndex="0">
                                    <telerik:RadPageView runat="server" ID="PageViewResumen" Height="100%" Width="930px"
                                        BackColor="#93ADD5">
                                        <table width="100%">
                                            <tr>
                                                <td align="center">
                                                    <table width="100%" border="0" style="background-color: White;">
                                                        <tr>
                                                            <td id="tdEncabezado">
                                                                <asp:UpdatePanel ID="upEncImp" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <table cellpadding="0" cellspacing="0" width="100%" border="0" style="padding-top: 3px;
                                                                            padding-left: 3px">
                                                                            <tr>
                                                                                <td align="left" style="color: #336699; font-weight: bold" colspan="3">
                                                                                    NOTA DE PEDIDO
                                                                                </td>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                                                        <tr>
                                                                                            <td align="left" style="width: 55px">
                                                                                                <span class="lblEncabezado" style="font-weight: normal">NRO:</span>
                                                                                            </td>
                                                                                            <td align="left" style="width: 105px">
                                                                                                <asp:Label ID="lblNroNotaImp" runat="server" Text="">&nbsp;</asp:Label>
                                                                                            </td>
                                                                                            <td align="left" style="width: 80px">
                                                                                                <span class="lblEncabezado" style="font-weight: normal">FECHA:</span>
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:Label ID="lblFechaImp" runat="server" Text="">&nbsp;</asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" style="width: 15%">
                                                                                    <span class="lblEncabezado">REVENDEDOR:</span>
                                                                                </td>
                                                                                <td align="left" style="width: 385px">
                                                                                    <telerik:RadComboBox Skin="Vista" ID="cboConsultores" runat="server" Width="380px"
                                                                                        AllowCustomText="true" MarkFirstMatch="true" OnClientSelectedIndexChanged="GetEncabezado">
                                                                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                                    </telerik:RadComboBox>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:UpdatePanel ID="upFichaTecnica" runat="server" UpdateMode="Conditional">
                                                                                        <ContentTemplate>
                                                                                            <asp:Image ID="imgFichaTecnica" runat="server" ImageUrl="~/Imagenes/FichaTecnica.gif.ashx"
                                                                                                Style="cursor: hand" />
                                                                                            <telerik:RadToolTip ID="tooltipFichaTecnica" Skin="Web20" Width="350px" runat="server"
                                                                                                Title="Datos Ficha" ShowCallout="true" Sticky="false" ManualClose="true" Position="MiddleRight"
                                                                                                HideDelay="5000" Animation="Fade" ShowEvent="OnClick" RelativeTo="Element" TargetControlID="imgFichaTecnica">
                                                                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:Label SkinID="lblBlack" ID="Label1" Text="Apellido y Nombre:" runat="server"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label SkinID="lblBlue" ID="lblToolTipNombre" Text="" runat="server"></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:Label SkinID="lblBlack" ID="Label2" Text="DNI:" runat="server"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label SkinID="lblBlue" ID="lblToolTipDNI" Text="" runat="server"></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:Label SkinID="lblBlack" ID="Label3" Text="Teléfono:" runat="server"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label SkinID="lblBlue" ID="lblToolTipTel" Text="" runat="server"></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:Label SkinID="lblBlack" ID="Label4" Text="Correo Electrónico:" runat="server"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label SkinID="lblBlue" ID="lblToolTipEmail" Text="" runat="server"></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:Label SkinID="lblBlack" ID="Label5" Text="Clase Revendedor:" runat="server"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label SkinID="lblBlue" ID="lblToolTipTipoConsultor" Text="" runat="server"></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:Label SkinID="lblBlack" ID="Label6" Text="Sit. Impositiva:" runat="server"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label SkinID="lblBlue" ID="lblToolTipSitImpositiva" Text="" runat="server"></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </telerik:RadToolTip>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                                <td align="left" colspan="2">
                                                                                    <asp:UpdatePanel ID="upTotalizarCabecera" runat="server" UpdateMode="Conditional">
                                                                                        <ContentTemplate>
                                                                                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                                                                <tr>
                                                                                                    <td align="left" style="width: 95px" class="lblEncabezado">
                                                                                                        PRODUCTOS:&nbsp;
                                                                                                    </td>
                                                                                                    <td align="left" style="width: 65px">
                                                                                                        <telerik:RadTextBox ID="txtTotalGeneral" runat="server" Width="60px" Height="15px"
                                                                                                            Text="0" ReadOnly="true" Skin="WebBlue" ReadOnlyStyle-HorizontalAlign="Center"
                                                                                                            Style="font-weight: bold">
                                                                                                        </telerik:RadTextBox>
                                                                                                    </td>
                                                                                                    <td align="left" style="width: 80px" class="lblEncabezado">
                                                                                                        TOTAL:
                                                                                                    </td>
                                                                                                    <td align="left">
                                                                                                        <telerik:RadTextBox ID="txtMontoGeneral" runat="server" Width="60px" Height="15px"
                                                                                                            Text="0" ReadOnly="true" Skin="WebBlue" ReadOnlyStyle-HorizontalAlign="Center"
                                                                                                            Style="font-weight: bold; cursor: hand">
                                                                                                        </telerik:RadTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="5">
                                                                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                                                        <tr>
                                                                                            <td align="left" class="lblEncabezado" style="color: #336699;">
                                                                                                DIRECCION ENTREGA
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:ImageButton ID="btnDireccion" runat="server" ImageUrl="~/Imagenes/DireccionesEntrega.png.ashx"
                                                                                                    Style="cursor: hand" ToolTip="direcciones de entrega alternativas" OnClientClick="ShowDirecciones(); return false;" />
                                                                                            </td>
                                                                                            <td align="left" class="lblEncabezado" style="color: #336699;">
                                                                                                OTROS DATOS
                                                                                            </td>
                                                                                            <td align="left" colspan="3">
                                                                                                <asp:ImageButton ID="btnComentario" runat="server" ImageUrl="~/Imagenes/Comentario.png.ashx"
                                                                                                    Style="cursor: hand" />
                                                                                                <telerik:RadToolTip ID="ToolTipComentario" Skin="Web20" Width="300px" runat="server"
                                                                                                    Title="Ingrese Comentario del Pedido" ShowCallout="true" Sticky="false" ManualClose="true"
                                                                                                    Position="BottomCenter" HideDelay="5000" Animation="Fade" ShowEvent="OnClick"
                                                                                                    RelativeTo="Element" TargetControlID="btnComentario">
                                                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <telerik:RadTextBox ID="txtObservacion" runat="server" MaxLength="250" ReadOnlyStyle-HorizontalAlign="Center"
                                                                                                                    Rows="5" Skin="WebBlue" Text="" TextMode="MultiLine" Width="98%">
                                                                                                                </telerik:RadTextBox>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </telerik:RadToolTip>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" class="lblEncabezado" style="height: 25px; width: 150px; padding-left: 8px;">
                                                                                                PROVINCIA Y LOCALIDAD:
                                                                                            </td>
                                                                                            <td align="left" style="width: 250px">
                                                                                                <asp:Label class="lblEncabezadoDesc" ID="lblDireccionEntrega" runat="server" Text=""></asp:Label>
                                                                                            </td>
                                                                                            <td align="left" class="lblEncabezado" style="padding-left: 8px;">
                                                                                                LIDER:
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:Label class="lblEncabezadoDesc" ID="lblResponsable" Text="" runat="server"></asp:Label>
                                                                                            </td>
                                                                                            <td align="left" class="lblEncabezado" style="padding-left: 8px;">
                                                                                                SALDO CTA:
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:Label class="lblEncabezadoDesc" ID="lblSaldoCta" Text="" runat="server"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" class="lblEncabezado" style="padding-left: 8px;">
                                                                                                CALLE:
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:Label class="lblEncabezadoDesc" ID="lblCalle" runat="server" Text=""></asp:Label>
                                                                                            </td>
                                                                                            <td align="left" class="lblEncabezado" style="padding-left: 8px;">
                                                                                                GRUPO:
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:Label class="lblEncabezadoDesc" ID="lblSubCoor" Text="" runat="server"></asp:Label>
                                                                                            </td>
                                                                                            <td align="left" class="lblEncabezado" style="padding-left: 8px;">
                                                                                                ULTIMA ACT:
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:Label class="lblEncabezadoDesc" ID="lblUltimaAct" Text="" runat="server"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" class="lblEncabezado" style="padding-left: 8px;">
                                                                                                TRANSPORTE:
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:UpdatePanel ID="upTransporte" runat="server" UpdateMode="Conditional">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:Label class="lblEncabezadoDesc" ID="lblTransporte" Text="" runat="server"></asp:Label>
                                                                                                        <input type="hidden" runat="server" id="lblTransporteHidden" value="" />
                                                                                                        <input type="hidden" runat="server" id="lblTransporteValorHidden" value="0" />
                                                                                                        <input type="hidden" runat="server" id="lblProvinciaPorcentajeDescuentoHidden" value="0" />
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </td>
                                                                                            <td align="left" class="lblEncabezado" style="padding-left: 8px;">
                                                                                                FORMA DE PAGO:
                                                                                            </td>
                                                                                            <td align="left" colspan="3">
                                                                                                <asp:UpdatePanel ID="upFormaDePago" runat="server" UpdateMode="Conditional">
                                                                                                    <ContentTemplate>
                                                                                                        <telerik:RadComboBox Skin="Vista" ID="cboFormaPago" runat="server" Width="195px"
                                                                                                            AutoPostBack="true" OnSelectedIndexChanged="cboFormaPago_SelectedIndexChanged">
                                                                                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                                                        </telerik:RadComboBox>
                                                                                                        &nbsp;&nbsp; <a href="http://www.e-pagofacil.com" target="_blank" runat="server"
                                                                                                            id="lnkPagoFacil" visible="false">Donde Pagar..</a> <a href="http://www.pagomiscuentas.com"
                                                                                                                target="_blank" runat="server" id="lnkPagoMisCuentas" visible="false">Donde Pagar..</a>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="50" AssociatedUpdatePanelID="upFormaDePago">
                                                                                                    <ProgressTemplate>
                                                                                                        <div style="position: absolute; top: 45%; left: 38%; padding: 5px; width: 24%; z-index: 1001;
                                                                                                            background-color: Transparent; vertical-align: middle; text-align: center;">
                                                                                                            <table border="0" cellpadding="0" cellspacing="0" style="height: 62px;">
                                                                                                                <tr>
                                                                                                                    <td align="center">
                                                                                                                        <img alt="a" src="Imagenes/waiting.gif.ashx" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="center">
                                                                                                                        <asp:Label ID="lblCalculoPromocion" runat="server" Font-Bold="True" Font-Names="Thomas"
                                                                                                                            Font-Size="12px" ForeColor="Black" Height="21px" Style="vertical-align: middle"
                                                                                                                            Text="Recalculando Promociones...">
                                                                                                                        </asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </div>
                                                                                                    </ProgressTemplate>
                                                                                                </asp:UpdateProgress>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="display: none">
                                                                <asp:UpdatePanel ID="upResumen" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:Panel runat="server" BackColor="White" ScrollBars="Vertical" Height="275px"
                                                                            ID="pnlResumen">
                                                                            <table width="95%" border="0">
                                                                                <tr>
                                                                                    <td>
                                                                                        <uc1:TotalizadorPedido ID="UcTotalizadorPedidos" runat="server" />
                                                                                        <asp:PlaceHolder ID="phTotalizadorPedido" runat="server"></asp:PlaceHolder>
                                                                                        <input id="hiddenHayCambios" type="hidden" runat="server" value="false" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                        <cc6:ServerControlWindow ID="ServerControlWindow1" runat="server" BackColor="WhiteSmoke"
                                                                            WindowColor="Azul">
                                                                            <ContentControls>
                                                                                <div id="divPrincipal">
                                                                                    <table id="Table1" runat="server" cellpadding="0" border="0" cellspacing="0" style="font-family: Sans-Serif;
                                                                                        font-size: 13px; font-weight: bold; text-align: center; color: Black; width: 100%;
                                                                                        height: 100%">
                                                                                        <tr>
                                                                                            <td style="width: 300px">
                                                                                                <asp:Label ID="lblProducto" runat="server" Text=""></asp:Label>
                                                                                                <asp:Label ID="lblCodigo" runat="server" Text="" Style="display: none"></asp:Label>
                                                                                                <asp:Label ID="lblCodigoEdicion" runat="server" Text="" Style="display: none"></asp:Label>
                                                                                                <asp:Label ID="lblPresentacion" runat="server" Text="" Style="display: none"></asp:Label>
                                                                                                <asp:Label ID="lblIdProducto" runat="server" Text="" Style="display: none"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <telerik:RadNumericTextBox ID="txtCantidadEdicion" runat="server" Width="80px" MinValue="0"
                                                                                                    MaxValue="1000" ShowSpinButtons="true">
                                                                                                    <NumberFormat DecimalDigits="0" />
                                                                                                </telerik:RadNumericTextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="2" align="center">
                                                                                                <asp:Button SkinID="btnGray" ID="Button2" runat="server" Text="Aceptar Cambios" OnClientClick="TerminarEdicion();return false" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </ContentControls>
                                                                        </cc6:ServerControlWindow>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <telerik:RadGrid ID="grillaPrueba" runat="server" AllowPaging="False" Width="90%"
                                                                    Font-Size="11px" Font-Names="Segoe UI,tahoma,verdana,sans-serif" GridLines="None"
                                                                    Skin="Vista">
                                                                    <MasterTableView ShowGroupFooter="true" ClientDataKeyNames="IdDetallePedido" GroupLoadMode="Client"
                                                                        AutoGenerateColumns="False" CommandItemDisplay="None" GroupHeaderItemStyle-HorizontalAlign="Left"
                                                                        NoDetailRecordsText="No hay productos solicitado" NoMasterRecordsText="No hay productos solicitado">
                                                                        <GroupByExpressions>
                                                                            <telerik:GridGroupByExpression>
                                                                                <SelectFields>
                                                                                    <telerik:GridGroupByField FieldName="DescPadre" HeaderText="Grupo" />
                                                                                </SelectFields>
                                                                                <GroupByFields>
                                                                                    <telerik:GridGroupByField FieldName="IdPadre" SortOrder="Descending" />
                                                                                </GroupByFields>
                                                                            </telerik:GridGroupByExpression>
                                                                        </GroupByExpressions>
                                                                        <RowIndicatorColumn CurrentFilterFunction="NoFilter" FilterListOptions="VaryByDataType"
                                                                            Visible="False">
                                                                            <HeaderStyle Width="20px" />
                                                                        </RowIndicatorColumn>
                                                                        <ExpandCollapseColumn CurrentFilterFunction="NoFilter" FilterListOptions="VaryByDataType"
                                                                            Resizable="False" >
                                                                            <HeaderStyle Width="20px" />
                                                                        </ExpandCollapseColumn>
                                                                        <Columns>
                                                                            <telerik:GridTemplateColumn UniqueName="Template1" HeaderText="" DataField="IdPadre">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton AlternateText="H" runat="server" ID="btnEditar" ImageUrl="~/Imagenes/Edit.gif" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridBoundColumn CurrentFilterFunction="NoFilter" DataField="DescripcionCompleta"
                                                                                FilterListOptions="VaryByDataType" ForceExtractValue="Always" HeaderText="Descripcion"
                                                                                SortExpression="DescripcionCompleta" UniqueName="DescripcionCompleta">
                                                                                <ItemStyle HorizontalAlign="Left" Width="55%" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn Aggregate="Sum" DataField="Cantidad" HeaderText="Cantidad"
                                                                                UniqueName="Cantidad" FooterText="Total:">
                                                                                <ItemStyle Width="250px" />
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="ValorUnitario" HeaderText="V. U." UniqueName="ValorUnitario">
                                                                                <ItemStyle Width="280px" />
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn Aggregate="Sum" DataField="ValorTotal" HeaderText="Valor Total"
                                                                                UniqueName="ValorTotal" FooterText="Total:">
                                                                                <ItemStyle Width="250px" />
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </telerik:GridBoundColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:UpdatePanel ID="upSolicitudPedido" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <table width="100%">
                                                                <tr>
                                                                    <telerik:RadToolTip ID="RadToolTip1" runat="server" Position="TopLeft" Animation="None"
                                                                        Skin="Web20" Width="140px" RelativeTo="Element" ShowEvent="OnMouseOver" AutoCloseDelay="3000"
                                                                        ShowCallout="true" TargetControlID="btnPedido" Height="25px">
                                                                    </telerik:RadToolTip>
                                                                    <telerik:RadToolTip ID="RadToolTip2" runat="server" Position="TopLeft" Animation="None"
                                                                        Skin="Web20" Width="140px" RelativeTo="Element" ShowEvent="OnMouseOver" AutoCloseDelay="3000"
                                                                        ShowCallout="true" TargetControlID="btnPedidoTemporal" Height="25px">
                                                                    </telerik:RadToolTip>
                                                                    <td align="right" style="width: 50%; padding-right: 15px">
                                                                        <asp:Button SkinID="btnBasic" ID="btnPedido" Text="Realizar Pedido" runat="server"
                                                                            OnClientClick="return ControlarDatos('Solicitar');" OnClick="btnPedido_RealizarPedido"
                                                                            ToolTip="Esta acción enviará la solitud del pedido para que sea preparada y enviada." />
                                                                    </td>
                                                                    <td align="left" style="width: 50%; padding-left: 15px">
                                                                        <asp:Button SkinID="btnBasic" ID="btnPedidoTemporal" Text="Guardar Pedido" runat="server"
                                                                            OnClientClick="return ControlarDatos('Guardar');" OnClick="btnPedidoTemporal_RealizarPedido"
                                                                            ToolTip="Esta acción guardará temporalmente el pedido hasta que usted desee solicitar la preparación." />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50" AssociatedUpdatePanelID="upSolicitudPedido">
                                                                <ProgressTemplate>
                                                                    <div style="position: absolute; top: 45%; left: 38%; padding: 5px; width: 24%; z-index: 1001;
                                                                        background-color: Transparent; vertical-align: middle; text-align: center;">
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="height: 62px;">
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <img alt="a" src="Imagenes/waiting.gif.ashx" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <asp:Label ID="lbltitulopaciente1" runat="server" Font-Bold="True" Font-Names="Thomas"
                                                                                        Font-Size="12px" ForeColor="Black" Height="21px" Style="vertical-align: middle"
                                                                                        Text="Procesando Pedido...">
                                                                                    </asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:UpdatePanel ID="upIndicadorResumen" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="processMessage" id="DivResumen" style="display: none" runat="server">
                                                    <table border="0" cellpadding="0" cellspacing="0" style="height: 62px;">
                                                        <tr>
                                                            <td align="center">
                                                                <img alt="a" src="Imagenes/waiting.gif.ashx" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView runat="server" ID="PageViewProductos" Height="100%" Width="930px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                                        <asp:TextBox ID="txtControlValor" runat="server" Height="15px" CssClass="txtStyleControlValor"
                                            onkeydown="return InputByKey();" onMousemove="CambiarCursor();" OnClick="ActualizarCantidad();"
                                            onkeyUp="ActualizarInput();" Width="45px" MaxLength="4" Text="0"></asp:TextBox>
                                        <table width="100%" cellspacing="0">
                                            <tr>
                                                <td align="left" valign="top" style="border-right: 1px solid black; width: 22%">
                                                    <asp:Panel ScrollBars="Vertical" Height="425px" runat="server" ID="pnlTree">
                                                        <asp:UpdatePanel ID="upTreeProductos" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <telerik:RadTreeView ID="RadTreeProductos" runat="server" DataFieldID="IdProducto"
                                                                    DataFieldParentID="Padre" DataTextField="Descripcion" OnClientNodeClicking="CargarListaProductos"
                                                                    DataValueField="IdProducto" OnNodeDataBound="RadTreeProductos_NodeDataBound"
                                                                    OnClientNodeExpanded="CargarImagen" Skin="Vista">
                                                                    <CollapseAnimation Type="OutQuint" Duration="100"></CollapseAnimation>
                                                                    <ExpandAnimation Duration="100"></ExpandAnimation>
                                                                </telerik:RadTreeView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </asp:Panel>
                                                    <div style="text-align: center; vertical-align: bottom">
                                                        <asp:UpdatePanel ID="upMontoActual" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <span style="font-weight: bold; font-size: 12px">Monto Actual:</span>
                                                                <asp:Label ID="lblMontoActual" runat="server" Text="" Style="font-weight: bold; font-size: 12px"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </td>
                                                <td align="left" valign="top" style="height: 450px;" width="100%">
                                                    <div id="divImagen" style="overflow: auto; filter: revealTrans(transition=12,duration=1);
                                                        width: 100%; height: 100%; background: transparent no-repeat; float: left; background-position: center"
                                                        onmousemove="HideControl();">
                                                        <div id="divImagenPromoInicial" style="text-align: center">
                                                            <asp:Image ID="ImagenPromos" runat="server" ImageUrl="Imagenes/Promos.jpg.ashx" />
                                                        </div>
                                                        <div id="divContente" style="overflow: auto; width: 100%; height: 410px; min-height: 410px;
                                                            padding-bottom: 6px">
                                                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <div runat="server" id="resultshere" style="padding-left: 5px; padding-top: 0px;
                                                                        width: 90%; border: 2px block solid;">
                                                                    </div>
                                                                    <telerik:RadToolTip ID="toolTipPrecios" runat="server" Position="TopLeft" Animation="None"
                                                                        Skin="Web20" Width="140px" RelativeTo="Element" ShowEvent="FromCode" AutoCloseDelay="3000"
                                                                        ShowCallout="true" Height="25px">
                                                                    </telerik:RadToolTip>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div id="divSolicitar" runat="server" style="display: none; text-align: center; width: 400px;
                                                            padding-top: 5px">
                                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:Button SkinID="btnGray" ID="btnSolicitar" Text="Solicitar" runat="server" OnClientClick="return ObtenerValoreCargados();" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView runat="server" ID="PageViewPromos" Height="100%" Width="930px"
                                        BackColor="#93ADD5">
                                        <table width="100%">
                                            <tr>
                                                <td align="center">
                                                    <asp:UpdatePanel runat="server" ID="upPromos" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Panel runat="server" BackColor="White" ID="Panel1" ScrollBars="Vertical" Height="425px">
                                                                <table width="95%" border="0">
                                                                    <tr>
                                                                        <td id="tdEncPromo">
                                                                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                                                <tr>
                                                                                    <td align="left" style="width: 80%; color: White; background-image: url('Imagenes/TituloSecundarioResumen.jpg.ashx');
                                                                                        background-repeat: repeat-y; font-weight: bold; font-family: Bookman Old Style;
                                                                                        font-size: 13px; left: 10px">
                                                                                        PROMOCIONES GANADAS
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <uc3:TotalizadorPromos ID="TotalizadorPromos1" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:UpdatePanel ID="upIndicadorPromos" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="processMessage" id="DivPromos" style="display: none" runat="server">
                                                    <table border="0" cellpadding="0" cellspacing="0" style="height: 62px;">
                                                        <tr>
                                                            <td align="center">
                                                                <img alt="a" src="Imagenes/waiting.gif.ashx" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView runat="server" ID="PageViewNiveles" Height="100%" Width="930px"
                                        BackColor="#93ADD5">
                                        <table width="100%" style="height: 100%" border="1">
                                            <tr>
                                                <td align="center" valign="middle">
                                                    <asp:UpdatePanel ID="upNivel" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Panel runat="server" BackColor="White" ID="pnlNivel" ScrollBars="Vertical" Height="425px">
                                                                <table width="95%" border="0" style="height: 100%">
                                                                    <tr>
                                                                        <td id="tdNivel" valign="top">
                                                                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                                                <tr>
                                                                                    <td align="left" style="width: 80%; color: White; background-image: url('Imagenes/TituloSecundarioResumen.jpg.ashx');
                                                                                        background-repeat: repeat-y; font-weight: bold; font-family: Bookman Old Style;
                                                                                        font-size: 13px; left: 10px">
                                                                                        NIVEL ALCANZADO
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top">
                                                                            <uc5:TotalizadorNivel ID="UCTotalizadorNivel" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView runat="server" ID="PageViewPromosActivas" Height="100%" Width="930px"
                                        BorderColor="#93ADD5" BorderWidth="1px" BorderStyle="Solid" BackColor="#E2E9F3">
                                        <div id="gallery">
                                            <div id="imagearea">
                                                <div id="image">
                                                    <a href="javascript:slideShow.nav(-1)" class="imgnav " id="previmg" style="display: none">
                                                    </a><a href="javascript:slideShow.nav(1)" class="imgnav " id="nextimg" style="display: none">
                                                    </a>
                                                </div>
                                            </div>
                                            <div id="thumbwrapper">
                                                <div id="thumbarea">
                                                    <ul id="thumbs" runat="server">
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>

                                        <script type="text/javascript">
                                            var imgid = 'image';
                                            var imgdir = 'ImagenesPromos/Full';
                                            var imgext = '.jpg';
                                            var thumbid = 'thumbs';
                                            var auto = true;
                                            var autodelay = 30000000;
                                        </script>

                                        <script src="Scripts/slide.js" type="text/javascript"></script>

                                    </telerik:RadPageView>
                                </telerik:RadMultiPage>
                            </td>
                        </tr>
                    </table>
                </div>
                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
                    <ClientEvents OnResponseEnd="responseEnd" />
                </telerik:RadAjaxManager>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="upDirec" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <telerik:RadToolTip ID="ToolTipDirecciones" Skin="Web20" Width="550px" runat="server"
                Title="Por favor seleccione la dirección de entrega para su pedido" Sticky="false"
                ManualClose="true" Position="Center" Animation="None" ShowEvent="OnClick" RelativeTo="BrowserWindow"
                Modal="true">
                <uc4:grillaDirecciones ID="ucGrillaDirecciones" runat="server" />
            </telerik:RadToolTip>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpToolTipMontoPedido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <telerik:RadToolTip ID="ToolTipMontoPedido" Skin="Web20" Width="250px" runat="server"
                Title="Detalle Monto" Sticky="false" ManualClose="false" Position="MiddleLeft"
                HideDelay="200" Animation="None" ShowEvent="OnMouseOver" RelativeTo="Element"
                TargetControlID="txtMontoGeneral">
                <table width="100%" style="height: 100%" border="1">
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Monto en Productos:"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblMontoProductos" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Costo Flete:"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCostoFlete" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label11" runat="server" Text="Descuentos por Promociones:"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDescuentos" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Descuentos por Provincia:"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDescuentoProvincia" runat="server" Text="$ 0"></asp:Label>
                        </td>
                    </tr>
                </table>
            </telerik:RadToolTip>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="DivBloque" style="display: none" class="progressBackgroundFilterBlue">
    </div>
    <div id="DivBarra" style="display: none; position: absolute; top: 45%; left: 40%;
        padding: 5px; width: 24%; z-index: 1001; background-color: Transparent; vertical-align: middle;
        text-align: center;">
        <table cellpadding="0" cellspacing="0" style="width: 100%" border="0" align="center">
            <tr>
                <td>
                    <img alt="a" src="Imagenes/loading.gif.ashx" />
                </td>
            </tr>
            <tr>
                <td style="font-family: tahoma; font-size: 12px; color: Black; font-weight: bold">
                    Calculando Promociones
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>