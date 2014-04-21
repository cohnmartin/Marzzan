<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="ConsolaMail.aspx.cs"
    Inherits="ConsolaMail" %>

<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="AjaxInfo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Consola de Mensajes</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <link href="Styles.css" type="text/css" rel="stylesheet" />

    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>

    <script src="Scripts/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="Scripts/jquery.uploadify.js" type="text/javascript"></script>

    <link href="UploadFiles/CSS/uploadify.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .FiltroMail
        {
            font: 13px Verdana, Geneva, sans-serif;
            background-color: #F5F5F5;
            border: 3px solid #DBDBDB;
            margin-top: 0px;
            margin-right: 0px;
            padding: 0px;
            width: 90%;
            cursor: pointer;
            font-weight: bold;
            padding: 3px;
        }
        .DivGeneral
        {
            font: 13px Verdana, Geneva, sans-serif;
            background-color: #F5F5F5;
            border: 3px solid #E5E5E5;
            margin-top: 0px;
            margin-right: 0px;
            padding: 0px;
            float: left;
            width: 176px;
            text-align: center;
            cursor: pointer;
            font-weight: bold;
        }
        .LibretaContacto
        {
            font: 13px Verdana, Geneva, sans-serif;
            background-color: #F5F5F5;
            border: 3px solid #E5E5E5;
            margin-top: 0px;
            margin-right: 2px;
            padding: 3px;
            float: left;
            width: 170px;
            height: 20px;
            text-align: center;
            cursor: pointer;
            font-weight: bold;
        }
        .LibretaContactoSelected
        {
            font: 14px Verdana, Geneva, sans-serif;
            background-color: #999999;
            border: 3px solid #E5E5E5;
            margin-top: 0px;
            margin-right: 2px;
            padding: 3px;
            float: left;
            width: 170px;
            height: 20px;
            text-align: center;
            cursor: pointer;
            font-weight: bold;
            color: White;
        }
    </style>
</head>

<script type="text/javascript">
    ///Metod que busca el tipo de mail en forma recursiva si es necesario
    /// ya que no se conoce la cantidad de niveles para la rama de Recibidos.
    function getTipoMail(nodo) {

        var tipoMail = "";
        if (nodo == undefined)
            nodo = $find("<%= RadTreeCarpetas.ClientID %>").get_selectedNode();

        if (nodo.get_level() > 0)
            tipoMail = getTipoMail(nodo.get_parent());
        else
            tipoMail = nodo.get_value();

        return tipoMail;
    }

    /// Metodo para obtener el id de la carpeta sobre la que se esta ejecutando la accion
    function getIdCarpeta(nodo) {


        if (nodo.get_level() > 0)
            return nodo.get_value();
        else
            return "";


    }

    function BuscarMailxCarpeta(sender, arg) {

        var tipoMail = "";
        if (arg.get_node().get_level() > 0)
            tipoMail = "Carpeta|" + arg.get_node().get_value();
        else
            tipoMail = arg.get_node().get_value();

        arg.get_node().select();
        var origen = $get("<%= txtOrigen.ClientID %>").value;
        var asunto = $get("<%= txtAsunto.ClientID %>").value;
        var estado = $find("<%= cboEstados.ClientID %>").get_value();
        var fechaInicial = "";
        var fechaFinal = "";

        if ($find("<%= txtFechaInicial.ClientID %>").get_selectedDate() != null)
            fechaInicial = $find("<%= txtFechaInicial.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

        if ($find("<%= txtFechaFinal.ClientID %>").get_selectedDate() != null)
            fechaFinal = $find("<%= txtFechaFinal.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

        var take = $find("gvEmail").get_pageSize();
        $find("<%=gvEmail.ClientID %>").ShowWaiting("Buscando Mensajes...");
        PageMethods.FiltroMails(tipoMail, fechaInicial, fechaFinal, origen, asunto, "", 0, take, onSuccess, onFailure);

        $("#divMails").css("display", "block");
        $("#divReglas").css("display", "none");
        $("#divFirma").css("display", "none");


        if (ctrCliente != undefined)
            ctrCliente.Clear();
    }

    function FiltrarMails() {

        var tipoMail = getTipoMail(); // $(".LibretaContactoSelected").attr("TipoMails");
        var origen = $get("<%= txtOrigen.ClientID %>").value;
        var asunto = $get("<%= txtAsunto.ClientID %>").value;
        var estado = $find("<%= cboEstados.ClientID %>").get_value();
        var fechaInicial = "";
        var fechaFinal = "";

        if ($find("<%= txtFechaInicial.ClientID %>").get_selectedDate() != null)
            fechaInicial = $find("<%= txtFechaInicial.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

        if ($find("<%= txtFechaFinal.ClientID %>").get_selectedDate() != null)
            fechaFinal = $find("<%= txtFechaFinal.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

        var take = $find("gvEmail").get_pageSize();
        $find("<%=gvEmail.ClientID %>").ShowWaiting("Buscando Mensajes...");
        PageMethods.FiltroMails(tipoMail, fechaInicial, fechaFinal, origen, asunto, estado, 0, take, onSuccessFiler, onFailure);
    }

    function EliminarMails() {
        var itemsSelected = $find("<%=gvEmail.ClientID %>").get_ItemsDataByFilter(true, "Seleccion");

        if (itemsSelected.length > 0) {
            blockConfirmCallBackFn('Esta seguro de eliminar los mensajes selecionados?', event, 330, 100, null, 'Eliminación', ConfirEliminacion);

            function ConfirEliminacion(result) {
                if (result) {

                    var tipoMail = getTipoMail(); //$(".LibretaContactoSelected").attr("TipoMails");
                    var take = $find("gvEmail").get_pageSize();
                    var ids = new Array();
                    $(itemsSelected).each(function() {

                        if (tipoMail == "Enviados")
                            ids.push(this["IdMailCabecera"]);
                        else
                            ids.push(this["IdMailDestino"]);
                    })


                    PageMethods.EliminarMails(tipoMail, ids.join(), take, onSuccess, onFailure);
                }
            }
        }
        else {
            radalert('Debe seleccionar los mensajes que desea eliminar.', 300, 100, "Selección Mensajes");
        }
    }



    function ActualizarMails() {
        window.location.reload();
    }


    function AbrirEmail(sender, id) {
        window.location.href = "GestionEscrituraMail.aspx?Accion=Leer&id=" + id;
    }

    function EditarMail(sender, id) {
        window.location.href = "GestionEscrituraMail.aspx?Accion=Leer&id=" + id;
    }

    function NuevoMail() {
        window.location.href = "GestionEscrituraMail.aspx?Accion=Nuevo&id=0";
    }

    function CambioPagina(sender, currentPageIndex) {

        var tipoMail = getTipoMail(); // $(".LibretaContactoSelected").attr("TipoMails");
        var origen = $get("<%= txtOrigen.ClientID %>").value;
        var asunto = $get("<%= txtAsunto.ClientID %>").value;
        var estado = $find("<%= cboEstados.ClientID %>").get_value();
        var fechaInicial = "";
        var fechaFinal = "";

        if ($find("<%= txtFechaInicial.ClientID %>").get_selectedDate() != null)
            fechaInicial = $find("<%= txtFechaInicial.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

        if ($find("<%= txtFechaFinal.ClientID %>").get_selectedDate() != null)
            fechaFinal = $find("<%= txtFechaFinal.ClientID %>").get_selectedDate().format("dd/MM/yyyy");


        var skip = $find("gvEmail").get_pageSize() * currentPageIndex;
        var take = $find("gvEmail").get_pageSize();

        PageMethods.FiltroMails(tipoMail, fechaInicial, fechaFinal, origen, asunto, estado, skip, take, onSuccess, onFailure);

    }


    function onSuccessFiler(datos) {
        if (datos != null) {
            $find("<%=gvEmail.ClientID %>").ChangeClientVirtualCount(datos["Cantidad"]);
            $find("<%=gvEmail.ClientID %>").set_ClientdataSource(datos["Mails"]);
            $find("<%=srvMoverCarpetas.ClientID %>").CloseWindows();
        }
        else {
            window.location.reload();
        }


    }

    function onSuccess(datos) {
        if (datos != null) {
            $find("<%=gvEmail.ClientID %>").set_ClientdataSource(datos["Mails"]);

            var tipo = getTipoMail();

            if (tipo == "Enviados") {
                $find("<%=gvEmail.ClientID %>").changeHeaderText("Para", "Para");
                $find("<%=gvEmail.ClientID %>").changeDisplayState("Seleccion", "block");
                $get("<%=lblFiltroOrigen.ClientID %>").innerText = "Para:";
            }
            else if (tipo == "Recividos") {
                $find("<%=gvEmail.ClientID %>").changeHeaderText("Para", "De");
                $find("<%=gvEmail.ClientID %>").changeDisplayState("Seleccion", "block");
                $get("<%=lblFiltroOrigen.ClientID %>").innerText = "De:";
            }
            else {
                $find("<%=gvEmail.ClientID %>").changeHeaderText("Para", "De/Para");
                $find("<%=gvEmail.ClientID %>").changeDisplayState("Seleccion", "none");
                $get("<%=lblFiltroOrigen.ClientID %>").innerText = "De/Para:";

            }
        }
        else {
            window.location.reload();
        }
    }

    function onFailure(error) {
        alert(error._message);
    }

    function ConfirmarAccion(sender, eventArgs) {
        var item = eventArgs.get_item();

        sender.hide();

        if (item.get_index() == 0) { /// Marcar/Desmarcar
            if (item.get_text().indexOf("Desmarcar") >= 0) {
                $find("<%=gvEmail.ClientID %>").SelectAll(false);
                item.set_text("Marcar Todos");
            }
            else {
                $find("<%=gvEmail.ClientID %>").SelectAll(true);
                item.set_text("Desmarcar Todos");
            }
        }
        else if (item.get_index() == 1) { // Mover Mensajes

            if ($find("<%=gvEmail.ClientID %>").get_ItemsDataByFilter(true, "Seleccion").length > 0)
                GenerarTablaConCarpetas();
            else
                radalert("Debe seleccionar al menos un mensaje para poder moverlo a otra carpeta", 300, 100, "Mover Mensajes");

        }
        else if (item.get_index() == 2) { // Configuracion Reglas
            MostrarReglas();
        }
        else if (item.get_index() == 4) { // Eliminar Mensajes
            EliminarMails();
        }

        eventArgs.set_cancel(true);


    }

    function GenerarTablaConCarpetas() {

        var table = '<table id="tbl" width="250px" border="1" cellpadding="0" cellspacing="0" style="font-size:13px; background-color: Transparent;"><thead><tr><th></th><th>Carpeta</th></tr></thead><tbody>';

        /// Genero las filas del body
        var treeViewSource = $find("<%= RadTreeCarpetas.ClientID %>");
        var nodos = treeViewSource.get_allNodes();

        for (var i = 0; i < nodos.length; i++) {

            if (nodos[i].get_value() == "Recividos" || nodos[i].get_level() > 0)
                table += '<tr><td align="left"><input type="radio" name="group1" value="' + nodos[i].get_value() + '"></td><td style="width:90%" align="left">' + nodos[i].get_text() + '</td></tr>'
        }

        table += '</tbody></table>';

        /// Asigno la tabla generada al div para dibujarla
        $("#divtbl")[0].innerHTML = table;
        $find("<%=srvMoverCarpetas.ClientID %>").set_CollectionDiv('divPrincipal');
        $find("<%=srvMoverCarpetas.ClientID %>").ShowWindows('divPrincipal', "Seleccione la carpeta Destino");

    }


    var accionEnCurso = "";
    var idCarpetaSeleccionada = "";
    var nodoSeleccionado = "";

    function SeleccionarNodo(sender, eventArgs) {

        var nodo = eventArgs.get_node();
        nodo.select();

        if (nodo.get_level() == 0) {
            if (nodo.get_value() == "Recividos")
                eventArgs.get_menu().get_items().toArray()[0].set_enabled(true);
            else
                eventArgs.get_menu().get_items().toArray()[0].set_enabled(false);

            eventArgs.get_menu().get_items().toArray()[1].set_enabled(false);
            eventArgs.get_menu().get_items().toArray()[3].set_enabled(false);
        }
        else {
            eventArgs.get_menu().get_items().toArray()[0].set_enabled(true);
            eventArgs.get_menu().get_items().toArray()[1].set_enabled(true);
            eventArgs.get_menu().get_items().toArray()[3].set_enabled(true);
        }

    }

    function EjecutarAccionMenuArbol(sender, eventArgs) {


        if (eventArgs.get_menuItem().get_index() == 0) {
            accionEnCurso = "Agregar";
        }
        else if (eventArgs.get_menuItem().get_index() == 1) {
            accionEnCurso = "Modificar";
        }
        else if (eventArgs.get_menuItem().get_index() == 3) {
            accionEnCurso = "Eliminar";
        }


        nodoSeleccionado = eventArgs.get_node();
        nodoSeleccionado.select();
        idCarpetaSeleccionada = getIdCarpeta(eventArgs.get_node());
        eventArgs.get_menuItem().get_parent().hide();
        if (accionEnCurso != "Eliminar") {

            $find("<%=srvCarpetas.ClientID %>").set_CollectionDiv('divPrincipalCarpetas');
            $find("<%=srvCarpetas.ClientID %>").ShowWindows('divPrincipalCarpetas', "Por favor ingrese el nombre de la carpeta");
        }
        else {
            blockConfirmCallBackFn('Esta seguro de eliminar la carpeta y mover todos los mensajes a la carpeta principal?', event, 330, 100, null, 'Eliminación Carpeta', GestionarCarpetas);
        }
        eventArgs.set_cancel(true);



    }

    function GestionarCarpetas(result) {

        if (result || result == undefined)
            PageMethods.GestionCarpetas(accionEnCurso, $get("<%= txtNombre.ClientID %>").value, idCarpetaSeleccionada, onSuccessGestionCarpeta, onFailureGestionCarpeta);
    }

    function onSuccessGestionCarpeta(result) {

        if (result != null) {
            var treeViewSource = $find("<%= RadTreeCarpetas.ClientID %>");
            treeViewSource.trackChanges();
            if (accionEnCurso == "Agregar") {

                var node = new Telerik.Web.UI.RadTreeNode();
                node.set_text($get("<%= txtNombre.ClientID %>").value);
                node.set_value(result);

                nodoSeleccionado.get_nodes().add(node);
            }
            else if (accionEnCurso == "Modificar") {
                nodoSeleccionado.set_text($get("<%= txtNombre.ClientID %>").value);
            }

            treeViewSource.commitChanges();

            /// Limpio controles y variables
            accionEnCurso = "";
            idCarpetaSeleccionada = "";
            nodoSeleccionado = "";
            $get("<%= txtNombre.ClientID %>").value = "";
            $find("<%=srvCarpetas.ClientID %>").CloseWindows();
        }
        else {
            window.location.reload();
        }
    }

    function onFailureGestionCarpeta() {

    }

    function MoverMesajes() {

        if ($("#tbl input[type='radio']:checked").length == 0) {
            alert("No hay seleccionados");
        }
        else {

            var idCarpetaDestino = $("#tbl input[type='radio']:checked")[0].value;
            var idCarpetaOrigen = $find("<%= RadTreeCarpetas.ClientID %>").get_selectedNodes()[0].get_value();
            var items = $find("<%=gvEmail.ClientID %>").get_ItemsDataByFilter(true, "Seleccion");
            var ids = "";
            for (var i = 0; i < items.length; i++) {
                ids += items[i].IdMailDestino + ",";
            }

            PageMethods.MoverMails(ids, idCarpetaDestino, idCarpetaOrigen, onSuccessFiler, onFailure);

        }

    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Metodo para las reglas
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////

    function MostrarReglas() {

        $("#divMails").css("display", "none");
        $("#divReglas").css("display", "block");
        $("#divFirma").css("display", "none");

        var nodesSelected = $find("<%= RadTreeCarpetas.ClientID %>").get_selectedNodes();
        for (var i = 0; i < nodesSelected.length; i++) {
            nodesSelected[i].unselect();
        }

    }

    function EjecutarRegla(sender, idRegla) {
        PageMethods.EjecutarRegla(idRegla, onSuccessFiler, onFailure);
    }

    var ctrCliente;
    jQuery(function() {

        options = {
            serviceUrl: 'ASHX/LoadClientes.ashx',
            width: '384',
            minChars: 2,
            zIndex: 922000000
        };
        ctrCliente = $('#' + "<%= txtCliente.ClientID %>").autocomplete(options);

    });


    function AgregarRegla() {

        var origen = ctrCliente.get_SelectedValue();
        var destino = $find("<%= cboCarpetas.ClientID %>").get_value();
        PageMethods.AgregarRegla(origen, destino, onSuccessRegla, onFailure);

    }

    function EliminarRegla(sender, id) {

        blockConfirmCallBackFn('Esta seguro de eliminar la regla?', event, 330, 100, null, 'Eliminación Regla', ConfirEliminacionRegla);

        function ConfirEliminacionRegla(result) {
            if (result) {
                PageMethods.EliminarRegla(id, onSuccessRegla, onFailure);
            }
        }
    }

    function onSuccessRegla(datos) {

        if (datos != null) {
            $find("<%=gvReglas.ClientID %>").set_ClientdataSource(datos);
            ctrCliente.Clear();
        }
        else {
            window.location.reload();
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Metodos para la gestión de Firma
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////

    $(window).load(
            function() {
                $("#<%=FileUpload2.ClientID %>").fileUpload({
                    'uploader': 'UploadFiles/scripts/uploader.swf',
                    'cancelImg': 'UploadFiles/images/cancel.png',
                    'buttonImg': 'Imagenes/adjuntar.png',
                    'wmode': 'transparent',
                    'buttonText': 'Adjuntar',
                    'script': 'UploadFiles/Upload.ashx',
                    'folder': 'ImagenesFirma',
                    'fileDesc': 'Archivos Imagen',
                    'fileExt': '*.jpg;*.png',
                    'multi': false,
                    'width': '45',
                    'auto': true,
                    'onComplete': TerminoUpload
                });
            }
        );

    function MostrarFirma() {
        $("#divMails").css("display", "none");
        $("#divReglas").css("display", "none");
        $("#divFirma").css("display", "block");

        var nodesSelected = $find("<%= RadTreeCarpetas.ClientID %>").get_selectedNodes();
        for (var i = 0; i < nodesSelected.length; i++) {
            nodesSelected[i].unselect();
        }

    }

    function TerminoUpload(sender, arg, infoArchivo, DatosArchivo, aa) {

        if (DatosArchivo.split('|').length > 1) {
            $("#<%=imgFirma.ClientID %>").css("display", "block");
            $("#<%=tdEliminarFirma.ClientID %>").css("display", "block");
            $("#<%=lblFirmaVacia.ClientID %>").css("display", "none");
            $get("imgFirma").src = "ImagenesFirma/" + infoArchivo.name;

            PageMethods.AgregarFirma(infoArchivo.name, onSuccessFirma, onFailure);

        }
    }

    function EliminarFirma() {
        $("#<%=imgFirma.ClientID %>").css("display", "none");
        $("#<%=tdEliminarFirma.ClientID %>").css("display", "none");
        $("#<%=lblFirmaVacia.ClientID %>").css("display", "block");
        $get("imgFirma").src = "#";

        PageMethods.EliminarFirma(onSuccessFirma, onFailure);
    }

    function onSuccessFirma() {
        // No se hace nada
    }
</script>

<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        <Scripts>
            <asp:ScriptReference Path="~/FuncionesComunes.js" />
        </Scripts>
    </asp:ScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Vista" VisibleTitlebar="true"
        Title="Atención">
    </telerik:RadWindowManager>
    <telerik:RadContextMenu ID="RadContextMenu1" runat="server" Skin="Web20" Width="220px"
        OnClientItemClicking="ConfirmarAccion">
        <Targets>
            <telerik:ContextMenuControlTarget ControlID="gvEmail" />
        </Targets>
        <CollapseAnimation Duration="200" Type="OutQuint" />
        <Items>
            <telerik:RadMenuItem NavigateUrl="#" runat="server" Text="Marcar Todos" ImageUrl="~/Imagenes/ok.gif">
            </telerik:RadMenuItem>
            <telerik:RadMenuItem NavigateUrl="#" runat="server" Text="Mover Mensajes Marcados"
                ImageUrl="~/Imagenes/MoverMail.png">
            </telerik:RadMenuItem>
            <telerik:RadMenuItem NavigateUrl="#" runat="server" Text="Gestión de Reglas" ImageUrl="~/Imagenes/ConfigurarMail.png">
            </telerik:RadMenuItem>
            <telerik:RadMenuItem runat="server" IsSeparator="True" Text="Root RadMenuItem4">
            </telerik:RadMenuItem>
            <telerik:RadMenuItem NavigateUrl="#" runat="server" Text="Eliminar Mensaje" ImageUrl="~/Imagenes/Cancelar.gif">
            </telerik:RadMenuItem>
        </Items>
    </telerik:RadContextMenu>
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
                            <td valign="top" align="center" style="width: 100%;">
                                <div style="position: relative; top: -25px">
                                    <asp:Label Font-Names="Bookman Old Style" Font-Size="22pt" ForeColor="#5F5F5F" ID="Label1"
                                        runat="server">Consola de Mensajes</asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <div class="FiltroMail">
                                    <table width="100%" border="0" cellspacing="3" cellpadding="0">
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label2" runat="server" SkinID="lblBlack">Fecha Inicio:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <telerik:RadDatePicker ID="txtFechaInicial" runat="server" Skin="Vista" Width="138px"
                                                    Culture="Spanish (Argentina)">
                                                    <DateInput InvalidStyleDuration="100" LabelCssClass="radLabelCss_Vista" Skin="Vista"
                                                        SelectionOnFocus="SelectAll" ShowButton="True">
                                                    </DateInput>
                                                    <Calendar Skin="Vista">
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="Label3" runat="server" SkinID="lblBlack">Fecha Fin:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <telerik:RadDatePicker ID="txtFechaFinal" runat="server" Skin="Vista" Width="138px"
                                                    Culture="Spanish (Argentina)">
                                                    <DateInput InvalidStyleDuration="100" LabelCssClass="radLabelCss_Vista" Skin="Vista"
                                                        SelectionOnFocus="SelectAll" ShowButton="True">
                                                    </DateInput>
                                                    <Calendar Skin="Vista">
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td align="right" valign="bottom" rowspan="3" style="width: 60px">
                                                <img alt="Filtrar Mensajes" src="Imagenes/Search.png" onclick="FiltrarMails();return false;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label7" runat="server" SkinID="lblBlack">Asunto:</asp:Label>
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox Width="99%" ID="txtAsunto" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblFiltroOrigen" runat="server" SkinID="lblBlack">De:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox Width="350px" ID="txtOrigen" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="Label10" runat="server" SkinID="lblBlack">Estado:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <telerik:RadComboBox ID="cboEstados" runat="server" Width="350px" EmptyMessage="Todos los Estados"
                                                    Skin="Vista" CausesValidation="false">
                                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Todos los estados" Value="" Selected="true" />
                                                        <telerik:RadComboBoxItem Text="Leidos" Value="Leido" />
                                                        <telerik:RadComboBoxItem Text="Sin Leer" Value="Sin Leer" />
                                                        <telerik:RadComboBoxItem Text="Respondidos" Value="Respondido" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="color: #993300; font-family: Sans-Serif; font-size: 11px">
                                <table width="98%" border="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 150px; height: 400px; border-right: solid 2px black; border-right-color: #C0C0C0;
                                            border-right-style: inset;" valign="top">
                                            <div class="DivGeneral" onclick="NuevoMail(); return false;">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="right" style="width: 30px; padding-right: 5px">
                                                            <asp:ImageButton ID="ImageButton1" Width="24" Height="24" runat="server" ImageUrl="~/Imagenes/EnviarMail.png" />
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="Label5" runat="server" SkinID="lblBlack">Nuevo Mensaje</asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="DivGeneral" onclick="ActualizarMails(); return false;">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="right" style="width: 30px; padding-right: 5px">
                                                            <asp:ImageButton ID="ImageButton4" runat="server" Width="24" Height="24" ImageUrl="~/Imagenes/refresh.png" />
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="Label6" runat="server" SkinID="lblBlack">Actualizar Mensajes</asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="DivGeneral" onclick="MostrarReglas(); return false;">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="right" style="width: 30px; padding-right: 5px">
                                                            <asp:ImageButton ID="ImageButton3" runat="server" Width="24" Height="24" ImageUrl="~/Imagenes/ConfigurarMail.png" />
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="Label4" runat="server" SkinID="lblBlack">Gestión de Reglas</asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="DivGeneral" onclick="MostrarFirma(); return false;">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="right" style="width: 30px; padding-right: 5px">
                                                            <asp:ImageButton ID="ImageButton2" runat="server" Width="24" Height="24" ImageUrl="~/Imagenes/edit.gif" />
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="Label8" runat="server" SkinID="lblBlack">Gestión de Firma</asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            &nbsp;
                                            <div runat="server" style="text-align: left">
                                                <telerik:RadTreeView ID="RadTreeCarpetas" runat="server" DataFieldID="IdEstructuraUbicacion"
                                                    DataFieldParentID="Padre" DataTextField="Nombre" DataValueField="IdEstructuraUbicacion"
                                                    OnNodeDataBound="RadTreeCarpetas_NodeDataBound" Skin="Vista" OnClientNodeClicking="BuscarMailxCarpeta"
                                                    OnClientContextMenuShowing="SeleccionarNodo" OnClientContextMenuItemClicking="EjecutarAccionMenuArbol">
                                                    <ContextMenus>
                                                        <telerik:RadTreeViewContextMenu ID="RadTreeViewContextMenu1" runat="server" Width="700px">
                                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                                            <Items>
                                                                <telerik:RadMenuItem NavigateUrl="#" runat="server" Text="Nueva Carpeta" ImageUrl="~/Imagenes/NuevaCarpeta.png">
                                                                </telerik:RadMenuItem>
                                                                <telerik:RadMenuItem NavigateUrl="#" runat="server" Text="Modificar Nombre Carpeta"
                                                                    ImageUrl="~/Imagenes/EditarCarpeta.png">
                                                                </telerik:RadMenuItem>
                                                                <telerik:RadMenuItem runat="server" IsSeparator="True" Text="Root RadMenuItem4">
                                                                </telerik:RadMenuItem>
                                                                <telerik:RadMenuItem NavigateUrl="#" runat="server" Text="Eliminar Carpeta" ImageUrl="~/Imagenes/EliminarCarpeta.png">
                                                                </telerik:RadMenuItem>
                                                            </Items>
                                                            <DefaultGroupSettings OffsetX="100" Width="500px" />
                                                        </telerik:RadTreeViewContextMenu>
                                                    </ContextMenus>
                                                    <CollapseAnimation Type="OutQuint" Duration="100"></CollapseAnimation>
                                                    <ExpandAnimation Duration="100"></ExpandAnimation>
                                                </telerik:RadTreeView>
                                            </div>
                                        </td>
                                        <td valign="top" align="center" style="padding-left: 0px">
                                            <div id="divMails">
                                                <AjaxInfo:ClientControlGrid ID="gvEmail" runat="server" AllowMultiSelection="false"
                                                    KeyName="IdMailCabecera" TypeSkin="Vista" PositionAdd="Top" AllowRowSelection="true"
                                                    Height="100%" Width="99%" AllowPaging="true" PageSize="25" EmptyMessage="No posee mensajes"
                                                    onClientChangePageIndex="CambioPagina" OnClientFunctionRowClicked="AbrirEmail">
                                                    <Columns>
                                                        <AjaxInfo:Column HeaderName="&nbsp;" DataFieldName="Seleccion" Align="Centrado" DataType="Bool"
                                                            Enabled="true" />
                                                        <AjaxInfo:Column HeaderName="&nbsp;" DataFieldName="Estado" Align="Centrado" Width="20"
                                                            Height="20" DataType="UrlImage" DataFieldToolTip="ToolTipEstado" onClientClick="EditarMail" />
                                                        <AjaxInfo:Column HeaderName="De" DataFieldName="Para" Align="Derecha" />
                                                        <AjaxInfo:Column HeaderName="Descripcion" DataFieldName="Descripcion" Align="Derecha" />
                                                        <AjaxInfo:Column HeaderName="Fecha" DataFieldName="Fecha" Align="Centrado" Width="80" />
                                                        <AjaxInfo:Column HeaderName="Hora" DataFieldName="Hora" Align="Centrado" Width="80" />
                                                        <AjaxInfo:Column Display="false" DataFieldName="ToolTipEstado" />
                                                        <AjaxInfo:Column Display="false" DataFieldName="IdMailDestino" />
                                                    </Columns>
                                                </AjaxInfo:ClientControlGrid>
                                            </div>
                                            <div id="divReglas" style="display: none">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr style="padding-top: 5px">
                                                        <td colspan="5" align="center">
                                                            <asp:Label Font-Names="Bookman Old Style" Font-Size="16pt" ForeColor="#5F5F5F" ID="Label12"
                                                                runat="server">Gestión de Regla</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="padding-bottom: 5px; padding-top: 5px">
                                                        <td style="padding-right: 2px">
                                                            <asp:Label ID="Label9" runat="server" SkinID="lblBlackBold">Cta Origen:</asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox Width="300px" ID="txtCliente" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td style="padding-right: 2px">
                                                            <asp:Label ID="Label11" runat="server" SkinID="lblBlackBold">Carpeta Destino:</asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <telerik:RadComboBox ID="cboCarpetas" runat="server" Width="300px" Skin="Vista" CausesValidation="false">
                                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td align="left" style="cursor: pointer">
                                                            <img alt="Agregar Regla" width="25px" height="25px" src="Imagenes/AddFormula.png"
                                                                onclick="AgregarRegla();return false;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="5">
                                                            <AjaxInfo:ClientControlGrid ID="gvReglas" runat="server" AllowMultiSelection="false"
                                                                KeyName="IdRegla" TypeSkin="Vista" PositionAdd="Top" AllowRowSelection="true"
                                                                Height="100%" Width="99%" AllowPaging="true" PageSize="25" EmptyMessage="No posee reglas definidas">
                                                                <FunctionsColumns>
                                                                    <AjaxInfo:FunctionColumnRow Type="Delete" ClickFunction="EliminarRegla" />
                                                                </FunctionsColumns>
                                                                <Columns>
                                                                    <AjaxInfo:Column HeaderName="&nbsp;" DataType="UrlImage" Width="20" Height="20" DataFieldName="ImgEjecutarRegla"
                                                                        DataFieldToolTip="Descripcion" Align="Centrado" onClientClick="EjecutarRegla" />
                                                                    <AjaxInfo:Column HeaderName="Nombre Regla" DataFieldName="Nombre" Align="Centrado" />
                                                                    <AjaxInfo:Column HeaderName="Accion" DataFieldName="Accion" Align="Centrado" />
                                                                    <AjaxInfo:Column HeaderName="Condición" DataFieldName="Condicion" Align="Derecha" />
                                                                    <AjaxInfo:Column HeaderName="Destino" DataFieldName="Destino" Align="Derecha" />
                                                                    <AjaxInfo:Column HeaderName="Descripcion" DataFieldName="Descripcion" Display="false" />
                                                                </Columns>
                                                            </AjaxInfo:ClientControlGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div id="divFirma" style="display: none">
                                                <table width="80%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr style="padding-top: 5px">
                                                        <td colspan="5" align="center">
                                                            <asp:Label Font-Names="Bookman Old Style" Font-Size="16pt" ForeColor="#5F5F5F" ID="Label13"
                                                                runat="server">Gestión de Firma</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="padding-bottom: 5px; padding-top: 5px">
                                                        <td style="padding-right: 2px; width: 60%">
                                                            <asp:Label ID="Label14" runat="server" SkinID="lblBlackBold">Seleccione la imagen para utilizar como firma:</asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:FileUpload ID="FileUpload2" runat="server" Style="display: none" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="padding: 10px; border: 1px solid black;">
                                                            <asp:Label ID="lblFirmaVacia" runat="server" SkinID="lblBlack">No tiene seleccionada ninguna imagen para utilizar como firma</asp:Label>
                                                            <img runat="server" style="display: none; max-width: 600px" alt="Imagen Firma" id="imgFirma"
                                                                src="" onclick="return false;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" runat="server" id="tdEliminarFirma" align="right" style="border: 1px solid black;
                                                            display: none">
                                                            <asp:Label ID="Label15" runat="server" SkinID="lblBlackBold">Eliminar Firma Seleccionada</asp:Label>
                                                            <img runat="server" src="imagenes/Eliminar.png" style="vertical-align: middle; cursor: pointer"
                                                                alt="Elimnar Firma" id="img1" onclick="EliminarFirma();return false;" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <AjaxInfo:ServerControlWindow ID="srvCarpetas" runat="server" BackColor="WhiteSmoke"
        WindowColor="Gray" ForeColor="#006699">
        <ContentControls>
            <div id="divPrincipalCarpetas" style="height: 110px; width: 370px;">
                <table width="95%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="padding-top: 30px">
                            <asp:Label ID="lblTransporte" runat="server" SkinID="lblBlack">Nombre:</asp:Label>
                        </td>
                        <td style="width: 210px; padding-top: 30px">
                            <asp:TextBox Width="97%" ID="txtNombre" runat="server"></asp:TextBox>
                        </td>
                        <td style="padding-top: 30px; padding-left: 5px">
                            <asp:Button ID="btnOk" runat="server" Text="Aceptar" SkinID="btnBasic" Width="57px"
                                OnClientClick="GestionarCarpetas();return false;" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentControls>
    </AjaxInfo:ServerControlWindow>
    <AjaxInfo:ServerControlWindow ID="srvMoverCarpetas" runat="server" BackColor="WhiteSmoke"
        WindowColor="Gray" ForeColor="#006699">
        <ContentControls>
            <div id="divPrincipal" style="height: 190px; width: 300px;">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td valign="top" align="center">
                            <div id="divtbl" style="height: 155px; overflow: auto">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="padding-top: 5px">
                            <asp:Button ID="Button1" runat="server" Text="Aceptar" SkinID="btnBasic" Width="57px"
                                OnClientClick="MoverMesajes();return false;" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentControls>
    </AjaxInfo:ServerControlWindow>
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
                                Text="Buscado Mensajes...">
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
