<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="GestionCobranzaManual.aspx.cs"
    Inherits="GestionCobranzaManual" %>

<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="AjaxInfo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <title>Gestión Logística de Cobranza</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <link href="UploadFiles/CSS/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/Jquery-UI/css/start/jquery-ui-1.10.3.custom.min.css" rel="stylesheet"
        type="text/css" />
    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>
    <script src="Scripts/Jquery-UI/js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="Scripts/jquery.tmpl.1.1.1.js" type="text/javascript"></script>
    <style type="text/css">
        .rowAlt
        {
            background-color: #CCF2FF;
        }
        .rowSpl
        {
            background-color: White;
        }
        .ContentFiltro
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
            background-color: #E0E0E0;
            border: 3px solid #C0C0C0;
            margin-top: 0px;
            margin-right: 0px;
            padding: 0px;
            float: none;
            width: 136px;
            text-align: center;
            cursor: pointer;
            font-weight: bold;
        }
    </style>
</head>
<script type="text/javascript">
    var IdGuiaSeleccionada = "";
    $(document).ready(function () {
        $("#dialogcobranza").dialog({
            autoOpen: false
        });
    });

    function ShowCobranza() {
        if (this.event.srcElement.type != "checkbox") {
            var tds = $(this.event.srcElement.parentElement).find("td");
            IdGuiaSeleccionada = $(tds[10]).text();



            var visita1 = $($(this.event.srcElement.parentElement).find("td[id=FechaVisita1]")[0]).text();
            var visita2 = $($(this.event.srcElement.parentElement).find("td[id=FechaVisita2]")[0]).text();
            var visita3 = $($(this.event.srcElement.parentElement).find("td[id=FechaVisita3]")[0]).text();

            var obs1 = $($(this.event.srcElement.parentElement).find("td[id=Observacion1]")[0]).text();
            var obs2 = $($(this.event.srcElement.parentElement).find("td[id=Observacion2]")[0]).text();
            var obs3 = $($(this.event.srcElement.parentElement).find("td[id=Observacion3]")[0]).text();

            var fechaEstadoLogistica = $($(this.event.srcElement.parentElement).find("td[id=FechaEstadoLogistica]")[0]).text();
            var Estado = $($(this.event.srcElement.parentElement).find("td[id=Estado]")[0]).text();
            var EstadoRendicion = $($(this.event.srcElement.parentElement).find("td[id=EstadoRendicion]")[0]).text();
            var NroRendicion = $($(this.event.srcElement.parentElement).find("td[id=IdRendicion]")[0]).text();



            var liderResponsable = $($(this.event.srcElement.parentElement).find("td[id=LiderResponsable]")[0]).text();
            var MotivoSiniestro = $($(this.event.srcElement.parentElement).find("td[id=MotivoSiniestro]")[0]).text();

            var tamañoPantalla = 0;
            if (liderResponsable != "") {
                $find("<%= cboLideresSimple.ClientID %>").set_text(liderResponsable);
                $("#trLideresSimple").show();
                tamañoPantalla += 30;
            }
            else {
                $find("<%= cboLideresSimple.ClientID %>").set_text("");
                $("#trLideresSimple").hide();
                tamañoPantalla += 0;
            }

            if (MotivoSiniestro != "") {
                $("#<%= txtMotivoSiniestroSimple.ClientID %>").text(MotivoSiniestro);
                $("#trMotivoSiniestoSimple").show();
                tamañoPantalla += 80;
            }
            else {
                $("#<%= txtMotivoSiniestroSimple.ClientID %>").text(MotivoSiniestro);
                $("#trMotivoSiniestoSimple").hide();
                tamañoPantalla += 0;
            }


            if (EstadoRendicion != "") {

                $find("<%= txtVisita1.ClientID %>").set_enabled(false);
                $find("<%= cboObservacion1.ClientID %>").disable();

                $find("<%= txtVisita2.ClientID %>").set_enabled(false);
                $find("<%= cboObservacion2.ClientID %>").disable();

                $find("<%= txtVisita3.ClientID %>").set_enabled(false);
                $find("<%= cboObservacion3.ClientID %>").disable();

                $find("<%= txtFechaEstado.ClientID %>").set_enabled(false);
                $find("<%= cboEstadoGuias.ClientID %>").disable();


                $get("<%= lblNroRendicion.ClientID %>").innerText = NroRendicion;
                $get("<%= lblEstadoRendicion.ClientID %>").innerText = EstadoRendicion;

                $("#<%= btnOk.ClientID %>").hide();
                tamañoPantalla += 0;

            }
            else {

                $find("<%= txtVisita1.ClientID %>").set_enabled(true);
                $find("<%= cboObservacion1.ClientID %>").enable();

                $find("<%= txtVisita2.ClientID %>").set_enabled(true);
                $find("<%= cboObservacion2.ClientID %>").enable();

                $find("<%= txtVisita3.ClientID %>").set_enabled(true);
                $find("<%= cboObservacion3.ClientID %>").enable();

                $find("<%= txtFechaEstado.ClientID %>").set_enabled(true);
                $find("<%= cboEstadoGuias.ClientID %>").enable();

                $get("<%= lblNroRendicion.ClientID %>").innerText = "PENDIENTE";
                $get("<%= lblEstadoRendicion.ClientID %>").innerText = "PENDIENTE";

                $("#<%= btnOk.ClientID %>").show();
                tamañoPantalla += 30;

            }
            
            /// Seteo el tamaño de la pantalla segun los controles que se muestran..
            $("#divPrincipalCobranza").css("height", (250 + tamañoPantalla) + "px");


            if (visita1 != "") {
                dia = visita1.substr(0, 2);
                mes = parseInt(visita1.substr(3, 2)) - 1 + '';
                año = visita1.substr(6, 4);
                Fecha = new Date(año, mes, dia);
                $find("<%= txtVisita1.ClientID %>").set_selectedDate(Fecha);
                $find("<%= cboObservacion1.ClientID %>").set_text(obs1);

            }
            else {
                $find("<%= txtVisita1.ClientID %>").set_selectedDate(null);
                $find("<%= cboObservacion1.ClientID %>").set_text(obs1);
            }

            if (visita2 != "") {
                dia = visita2.substr(0, 2);
                mes = parseInt(visita2.substr(3, 2)) - 1 + '';
                año = visita2.substr(6, 4);
                Fecha = new Date(año, mes, dia);
                $find("<%= txtVisita2.ClientID %>").set_selectedDate(Fecha);
                $find("<%= cboObservacion2.ClientID %>").set_text(obs2);

            }
            else {
                $find("<%= txtVisita2.ClientID %>").set_selectedDate(null);
                $find("<%= cboObservacion2.ClientID %>").set_text(obs2);
            }

            if (visita3 != "") {
                dia = visita3.substr(0, 2);
                mes = parseInt(visita3.substr(3, 2)) - 1 + '';
                año = visita3.substr(6, 4);
                Fecha = new Date(año, mes, dia);
                $find("<%= txtVisita3.ClientID %>").set_selectedDate(Fecha);
                $find("<%= cboObservacion3.ClientID %>").set_text(obs3);

            }
            else {

                $find("<%= txtVisita3.ClientID %>").set_selectedDate(null);
                $find("<%= cboObservacion3.ClientID %>").set_text(obs3);
            }


            if (fechaEstadoLogistica != "") {
                dia = fechaEstadoLogistica.substr(0, 2);
                mes = parseInt(fechaEstadoLogistica.substr(3, 2)) - 1 + '';
                año = fechaEstadoLogistica.substr(6, 4);
                Fecha = new Date(año, mes, dia);
                $find("<%= txtFechaEstado.ClientID %>").set_selectedDate(Fecha);
                $find("<%= cboEstadoGuias.ClientID %>").set_text(Estado);
            }
            else {
                $find("<%= txtFechaEstado.ClientID %>").set_selectedDate(null);
                $find("<%= cboEstadoGuias.ClientID %>").set_text("");
            }


            $find("<%=srvCobranza.ClientID %>").set_CollectionDiv('divPrincipalCobranza');
            $find("<%=srvCobranza.ClientID %>").ShowWindows('divPrincipalCobranza', $(tds[1]).text() + " - " + $(tds[2]).text());

        }
        return;

    }

    function FiltrarGuiasPorEstado() {

        var filtroEstodo = $find("<%= cboEstadosGuiasFiltro.ClientID %>").get_text();
        if (filtroEstodo != "Todos") {
            $('#tbl tbody td').parent().css("display", "table-row");
            $('#tbl td[filtro=Estado]:not(:contains(' + filtroEstodo + '))').parent().css("display", "none");
        }
        else {
            $('#tbl tbody td').parent().css("display", "table-row");
        }
    }

    function FiltrarGuias() {
        if (event.keyCode == 13) {

            $('#tbl tbody td').parent().css("display", "table-row");


            if (event.srcElement.id == "txtFiltroGuia") {
                var valorFiltro = $("#txtFiltroGuia").val().toUpperCase();
                if (valorFiltro != "") {
                    //Filtto por el nro de comprobante
                    $('#tbl td[filtro=Comprobante]:not(:contains(' + valorFiltro + '))').parent().css("display", "none");
                    $("#txtFiltroLocalidad").val("");
                }
            }
            else {
                var valorFiltro = $("#txtFiltroLocalidad").val().toUpperCase();
                if (valorFiltro != "") {
                    //Filtto por la localidad
                    $('#tbl td[filtro=Localidad]:not(:contains(' + valorFiltro + '))').parent().css("display", "none");
                    $("#txtFiltroGuia").val("");
                }
            }

            return false;
        }
        else {
            return true;
        }

    }

    function checkAll(obj) {
        if ($(obj).is(":checked")) {
            //$('.seleccion').removeAttr('checked');
            $('.seleccion').attr('checked', true);
        }
        else {
            $('.seleccion').attr('checked', false);
            //$('.seleccion').removeAttr('checked');
        }
    }

    function callBackFunction(datos) {
        if ($("#dialogcobranza"))
            $("#dialogcobranza").dialog("close");

        var t = [
        { "header": "Estado" },
        { "header": "Comprobante" },
        { "header": "Nombre" },
        { "header": "Tipo" },
        { "header": "Direccion" },
        { "header": "Localidad" },
        { "header": "Monto" },
        { "header": "Lider" },
        { "header": "Tel. Lider" },
        { "header": "<input type='checkbox' id='chkAll' onclick='checkAll(this);' />"}];

        var templates = {
            th: '<th>#{header}</th>',
            td: '<tr style="cursor:pointer" style="display:inline" onclick="ShowCobranza(this)" >' +
                '<td align="center" title="#{EstadoTip}" style="font-weight:bold;font-size:11px;color:red" >#{EstadoAbreviatura}</td>' +
                '<td align="left" style="width:110px" filtro="Comprobante">#{Comprobante}</td>' +
                '<td align="left">#{Nombre}</td>' +
                '<td  align="center" style="width:40px">#{TipoComprobante}</td>' +
                '<td>#{Direccion}</td>' +
                '<td align="left" filtro="Localidad" >#{Localidad}</td>' +
                '<td align="center" style="width:60px">#{Monto}</td>' +
                '<td style="width:170px">#{Lider}</td>' +
                '<td style="width:150px">#{LiderTel}</td>' +
                '<td align="center" style="width:30px" ><input type="checkbox" id="chkSeleccion" IdDetalleGuia="#{IdDetalleGuia}" class="seleccion" /></td>' +
                '<td style="display:none" id="IdDetalleGuia">#{IdDetalleGuia}</td>' +
                '<td style="display:none" id="FechaVisita1" >#{FechaVisita1}</td>' +
                '<td style="display:none" id="FechaVisita2" >#{FechaVisita2}</td>' +
                '<td style="display:none" id="FechaVisita3" >#{FechaVisita3}</td>' +
                '<td style="display:none" id="Observacion1" >#{Observacion1}</td>' +
                '<td style="display:none" id="Observacion2" >#{Observacion2}</td>' +
                '<td style="display:none" id="Observacion3" >#{Observacion3}</td>' +
                '<td style="display:none" id="FechaEstadoLogistica" >#{FechaEstadoLogistica}</td>' +
                '<td style="display:none" filtro="Estado" id="Estado" >#{Estado}</td>' +
                '<td style="display:none" filtro="EstadoRendicion" id="EstadoRendicion" >#{EstadoRendicion}</td>' +
                '<td style="display:none" filtro="IdRendicion" id="IdRendicion" >#{IdRendicion}</td>' +
                '<td style="display:none" filtro="LiderResponsable" id="LiderResponsable" >#{LiderResponsable}</td>' +
                '<td style="display:none" filtro="MotivoSiniestro" id="MotivoSiniestro" >#{MotivoSiniestro}</td>' +
                '</tr>'



        };

        var table = '<table width="100%" id="tbl" border="1" cellpadding="0" cellspacing="0" style="font-size:13px; background-color: Transparent;"><thead>' +
        '<tr><th colspan="10" align="right" style="font-size:11px"><button class="bntAplicarEstado" onclick="ShowCobranzaMasiva();return false;">Aplicar Cambios</button></th></tr>' +
        '<tr>';

        /// Genero la fila de encabezado
        $.each(t, function (key, val) {
            table += $.tmpl(templates.th, val);
        });

        table += '</tr></thead><tbody>';

        /// Genero las filas del body
        row = 0;
        for (var i = 0; i < datos.length; i++) {
            var td = $.tmpl(templates.td, datos[row]);

            if (datos[row]["FechaEstadoLogistica"] != null) {
                td = td.replace("class=\"seleccion\" />", " class=\"seleccion\" style=\"display:none\" />&nbsp;");
            }

            table += td;
            row++;
        }

        table += '</tbody><tfoot>' +
        '<tr><th colspan="10" align="right" style="font-size:11px"><button class="bntAplicarEstado" onclick="return false;">Aplicar Cambios</button></th></tr>' +
        '</tfoot></table>';

        /// Asigno la tabla generada al div para dibujarla
        $("#divtbl")[0].innerHTML = table;
        $("#tbl tbody tr:odd").addClass("rowAlt");
        $("#tbl tbody tr:even").addClass("rowSpl");

        $("#tblDetalleCobros").css("display", "block");


        $find("<%=srvCobranza.ClientID %>").CloseWindows();


        $(".bntAplicarEstado").button({
            icons: {
                primary: "ui-icon-arrowrefresh-1-e"
            }
        });

    }

    function ShowCobranzaMasiva() {
        var chks = $("#tbl").find("input[type='checkbox']:checked:visible");
        if (chks.length > 0) {
            $("#dialogcobranza").dialog({
                modal: true,
                autoOpen: true,
                resizable: false,
                width: 350,
                title: "Datos Necesarios",
                buttons: [{ text: "Aceptar", click: function () {


                    var estado = $find("<%= cboEstadoMasivo.ClientID %>").get_value();
                    var descEstado = $find("<%= cboEstadoMasivo.ClientID %>").get_text();
                    var cobro = $find("<%= txtFechaCobroMasivo.ClientID %>").get_selectedDate();
                    var motivo = $("#<%= txtMotivoSiniestro.ClientID %>").text();
                    var lider = $find("<%= cboLideres.ClientID %>").get_value() == "" ? null : $find("<%= cboLideres.ClientID %>").get_value();


                    if (estado == "-1") {
                        alert("Debe seleccionar el estado para las guías.");
                    }
                    else if (cobro == null) {
                        alert("Debe seleccionar la fecha del estado para las guías.");
                    }
                    else if (descEstado.indexOf("LIDER") > 0 && lider == null) {
                        alert("Debe seleccionar el líder al que entrega las guías seleccionadas.");
                    }
                    else if (descEstado.indexOf("SINIESTRA") > 0 && motivo.trim() == "") {
                        alert("Debe ingresar el motivo del siniestro producido sobre las guías.");
                    }
                    else {

                        var ids = new Array();
                        chks.each(function () {
                            if ($(this).attr("IdDetalleGuia") != undefined)
                                ids.push($(this).attr("IdDetalleGuia"));
                        });
                        PageMethods.updateMasivoGuias(ids, cobro.format("dd/MM/yyyy"), estado, lider, motivo, callBackFunction, ErroFunction);

                    }

                }
                }]

            });

            $(".ui-button-text").css({ "font-size": +11 + "px" });
            $(".ui-dialog-title ").css({ "font-size": +13 + "px" });
        }
        else {
            alert("Debe seleccionar al menos una guía.");
        }
    }

    function ErroFunction(datos) {
        alert(datos._message);
    }

    var fileName = "";
    var idDespachoSelected = "";

    function FiltrarDespachos() {

        var nroDespacho = $get("<%= txtNroDespacho.ClientID %>").value;
        var transporte = $find("<%= cboTransportes.ClientID %>").get_text();
        var fechaInicial = "";
        var fechaFinal = "";

        if ($find("<%= txtFechaInicial.ClientID %>").get_selectedDate() != null)
            fechaInicial = $find("<%= txtFechaInicial.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

        if ($find("<%= txtFechaFinal.ClientID %>").get_selectedDate() != null)
            fechaFinal = $find("<%= txtFechaFinal.ClientID %>").get_selectedDate().format("dd/MM/yyyy");



        var take = $find("gvDespachos").get_pageSize();
        $find("<%=gvDespachos.ClientID %>").ShowWaiting("Buscando Despachos...");
        PageMethods.FiltroDespachos(nroDespacho, fechaInicial, fechaFinal, transporte, 0, take, onSuccessFiler, onFailure);

        $("#tblDetalleCobros").css("display", "none");
    }


    function onSuccessFiler(datos) {
        if (datos != null) {
            $find("<%=gvDespachos.ClientID %>").ChangeClientVirtualCount(datos["Cantidad"]);
            $find("<%=gvDespachos.ClientID %>").set_ClientdataSource(datos["Guias"]);
        }
    }

    function onFailure() {
        alert("Error al invocar el web metodo");
    }


    function DescargarGuia(sender, id) {

        PageMethods.GetGuias(id, callBackFunction, ErroFunction);

    }

    function GuardarCobranza() {

        var visita1 = $find("<%= txtVisita1.ClientID %>").get_selectedDate() != null ? $find("<%= txtVisita1.ClientID %>").get_selectedDate().format("dd/MM/yyyy") : "";
        var visita2 = $find("<%= txtVisita2.ClientID %>").get_selectedDate() != null ? $find("<%= txtVisita2.ClientID %>").get_selectedDate().format("dd/MM/yyyy") : "";
        var visita3 = $find("<%= txtVisita3.ClientID %>").get_selectedDate() != null ? $find("<%= txtVisita3.ClientID %>").get_selectedDate().format("dd/MM/yyyy") : "";

        var obs1 = $find("<%= cboObservacion1.ClientID %>").get_text();
        var obs2 = $find("<%= cboObservacion2.ClientID %>").get_text();
        var obs3 = $find("<%= cboObservacion3.ClientID %>").get_text();

        var fechaEstado = $find("<%= txtFechaEstado.ClientID %>").get_selectedDate() != null ? $find("<%= txtFechaEstado.ClientID %>").get_selectedDate().format("dd/MM/yyyy") : "";
        var estado = $find("<%= cboEstadoGuias.ClientID %>").get_value() != "" ? $find("<%= cboEstadoGuias.ClientID %>").get_value() : "-1";
        var descEstado = $find("<%= cboEstadoGuias.ClientID %>").get_text() != "" ? $find("<%= cboEstadoGuias.ClientID %>").get_text() : "";
        var motivo = $("#<%= txtMotivoSiniestroSimple.ClientID %>").text();
        var lider = $find("<%= cboLideresSimple.ClientID %>").get_value() == "" ? null : $find("<%= cboLideresSimple.ClientID %>").get_value();


        if (descEstado.indexOf("LIDER") > 0 && lider == null) {
            alert("Debe seleccionar el líder al que entrega la guía seleccionada.");
        }
        else if (descEstado.indexOf("SINIESTRA") > 0 && motivo.trim() == "") {
            alert("Debe ingresar el motivo del siniestro producido sobre la guía.");
        }
        else {
            PageMethods.updateGuia(parseInt(IdGuiaSeleccionada),
            visita1,
            visita2,
            visita3,
            obs1, obs2, obs3, fechaEstado, estado, lider, motivo, DescEstado, callBackFunction, ErroFunction);
        }

    }

    function ShowCargaAdicional(sender, arg) {
        if (arg.get_item().get_text().indexOf("LIDER") > 0) {
            $("#trLideres").show();
            $("#trSiniestro").hide();
        }
        else if (arg.get_item().get_text().indexOf("SINIESTRA") > 0) {

            $("#trSiniestro").show();
            $("#trLideres").hide();
        }
        else {
            $("#trSiniestro").hide();
            $("#trLideres").hide();
        }

    }

    function ShowCargaAdicionalSimple(sender, arg) {
        var tamañoPantalla = 0;
        if (arg.get_item().get_text().indexOf("LIDER") > 0) {
            $("#trLideresSimple").show();
            $("#trMotivoSiniestoSimple").hide();
            tamañoPantalla += 65;
        }
        else if (arg.get_item().get_text().indexOf("SINIESTRA") > 0) {

            $("#trMotivoSiniestoSimple").show();
            $("#trLideresSimple").hide();
            tamañoPantalla += 115;
        }
        else {
            $("#trMotivoSiniestoSimple").hide();
            $("#trLideresSimple").hide();
        }

        $("#divPrincipalCobranza").css("height", (250 + tamañoPantalla) + "px");
    }
    
    
    
</script>
<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        <Scripts>
            <asp:ScriptReference Path="~/FuncionesComunes.js" />
        </Scripts>
    </asp:ScriptManager>
    <div id="dialogcobranza" title="Basic dialog" style="display: none; font-size: 11px">
        <p>
            Debe cargar la fecha y estado para las guias seleccionadas</p>
        <table width="95%" border="0" cellspacing="2" cellpadding="0">
            <tr style="padding-top: 5px;">
                <td style="width: 120px">
                    <asp:Label ID="Label19" runat="server" SkinID="lblBlack">Estado:</asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox Width="280px" ID="cboEstadoMasivo" runat="server" Skin="Vista"
                        ZIndex="990000000" AppendDataBoundItems="true" OnClientSelectedIndexChanged="ShowCargaAdicional">
                        <Items>
                            <telerik:RadComboBoxItem Value="-1" Text="" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr style="padding-top: 5px;">
                <td style="width: 120px">
                    <asp:Label ID="Label17" runat="server" SkinID="lblBlack">Fecha:</asp:Label>
                </td>
                <td>
                    <telerik:RadDatePicker ID="txtFechaCobroMasivo" runat="server" Skin="Vista" Width="118px"
                        Culture="Spanish (Argentina)" ZIndex="990000000">
                        <DateInput ID="DateInput6" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Vista"
                            Skin="Vista" SelectionOnFocus="SelectAll" ShowButton="True">
                        </DateInput>
                        <Calendar ID="Calendar6" Skin="Vista" runat="server">
                        </Calendar>
                    </telerik:RadDatePicker>
                </td>
            </tr>
            <tr style="padding-top: 5px; display: none" id="trLideres">
                <td style="width: 120px">
                    <asp:Label ID="Label22" runat="server" SkinID="lblBlack">Líder:</asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox Width="280px" ID="cboLideres" runat="server" Skin="Vista" ZIndex="990000000"
                        AllowCustomText="true" MarkFirstMatch="true">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr style="padding-top: 5px; display: none" id="trSiniestro">
                <td style="width: 120px">
                    <asp:Label ID="Label23" runat="server" SkinID="lblBlack">Motivo:</asp:Label>
                </td>
                <td>
                    <asp:TextBox TextMode="MultiLine" Width="280px" Rows="4" MaxLength="250" ID="txtMotivoSiniestro"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Vista" VisibleTitlebar="true"
        Title="Atención">
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
                            <td valign="top" align="center" style="width: 100%;">
                                <div style="position: relative; top: -25px;">
                                    <asp:Label Font-Names="Bookman Old Style" Font-Size="22pt" ForeColor="#5F5F5F" ID="Label1"
                                        runat="server">Gestión Logística de Cobranza</asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <div class="ContentFiltro">
                                    <table width="100%" border="0" cellspacing="3" cellpadding="0">
                                        <tr>
                                            <td align="left" style="width: 110px">
                                                <asp:Label ID="Label2" runat="server" SkinID="lblBlack">Fecha Desde:</asp:Label>
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
                                            <td align="left" style="padding-left: 5px">
                                                <asp:Label ID="Label3" runat="server" SkinID="lblBlack">Fecha Hasta:</asp:Label>
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
                                                <img alt="Filtrar Mensajes" src="Imagenes/Search.png" onclick="FiltrarDespachos();return false;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label7" runat="server" SkinID="lblBlack">Nº Despacho:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox Width="99%" ID="txtNroDespacho" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="left" style="padding-left: 5px">
                                                <asp:Label ID="Label4" runat="server" SkinID="lblBlack">Transporte:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <telerik:RadComboBox ID="cboTransportes" runat="server" Skin="Vista" Width="200px">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Value="" Text="Todos" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 10px">
                                <AjaxInfo:ClientControlGrid ID="gvDespachos" runat="server" AllowMultiSelection="false"
                                    KeyName="IdCabeceraGuia" TypeSkin="Vista" PositionAdd="Top" AllowRowSelection="false"
                                    Height="100%" Width="99%" AllowPaging="true" PageSize="25" EmptyMessage="No existen despachos">
                                    <FunctionsColumns>
                                        <AjaxInfo:FunctionColumnRow Type="Custom" ImgUrl="imagenes/DireccionesEntrega.png"
                                            Text="Ver Detalle Cobranza" ClickFunction="DescargarGuia" />
                                    </FunctionsColumns>
                                    <Columns>
                                        <AjaxInfo:Column HeaderName="Nro Despacho" DataFieldName="NroDespacho" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Fecha Envio" DataFieldName="FechaEnvio" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Fecha Generacion" DataFieldName="FechaGeneracion" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Transporte" DataFieldName="Transporte" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Total Guias" DataFieldName="CantidadGuias" Align="Centrado" />
                                        <AjaxInfo:Column HeaderName="Sin Rendir" DataFieldName="GuiasSinRendir" DataType="Integer"
                                            Align="Centrado" />
                                        <AjaxInfo:Column HeaderName="Sin Aprobar" DataFieldName="GuiasSinAprobar" DataType="Integer"
                                            Align="Centrado" />
                                    </Columns>
                                </AjaxInfo:ClientControlGrid>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <div id="tblDetalleCobros" style="display: none">
        <table width="100%" border="0" cellspacing="3" cellpadding="0">
            <tr>
                <td align="center" valign="middle">
                    <table width="80%" border="0" cellspacing="0" cellpadding="0" style="border: 2px solid #808080;
                        height: 40px; background-color: #C0C0C0; font-weight: bold">
                        <tr>
                            <td align="right" style="width: 70px; padding-right: 5px">
                                <asp:Label ID="Label5" runat="server" SkinID="lblBlackBold">Nro Guia:</asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox Width="200px" ID="txtFiltroGuia" runat="server" onkeydown="return FiltrarGuias();"
                                    ToolTip="Presione enter una vez ingresado el criterio para buscar"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 70px; padding-right: 5px">
                                <asp:Label ID="Label6" runat="server" SkinID="lblBlackBold">Localidad:</asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox Width="200px" ID="txtFiltroLocalidad" runat="server" onkeydown="return FiltrarGuias();"
                                    ToolTip="Presione enter una vez ingresado el criterio para buscar"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 70px; padding-right: 5px">
                                <asp:Label ID="Label16" runat="server" SkinID="lblBlackBold">Estado:</asp:Label>
                            </td>
                            <td align="right" style="width: 250px; padding-right: 5px">
                                <telerik:RadComboBox ID="cboEstadosGuiasFiltro" runat="server" Skin="Vista" Width="250px"
                                    OnClientSelectedIndexChanged="FiltrarGuiasPorEstado" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="Todos" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <div id="divtbl">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <AjaxInfo:ServerControlWindow ID="srvCobranza" runat="server" BackColor="WhiteSmoke"
        WindowColor="Gray" ForeColor="#006699">
        <ContentControls>
            <div id="divPrincipalCobranza" style="height: 300px; width: 570px;">
                <table width="95%" border="0" cellspacing="2" cellpadding="0">
                    <tr>
                        <td colspan="3" style="padding-bottom: 5px; padding-left: 3px; border-top-style: none;
                            border-left-style: none; border-bottom-style: double; border-bottom-width: thin;
                            border-bottom-color: #006699">
                            <asp:Label ID="lblTransporte" runat="server" SkinID="lblBlackBold">Datos Visita (sin entrega)</asp:Label>
                        </td>
                    </tr>
                    <tr style="padding-top: 5px;">
                        <td style="width: 60px">
                            <asp:Label ID="Label8" runat="server" SkinID="lblBlack">Visita 1:</asp:Label>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtVisita1" runat="server" Skin="Vista" Width="118px"
                                Culture="Spanish (Argentina)" ZIndex="990000000">
                                <DateInput ID="DateInput1" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Vista"
                                    Skin="Vista" SelectionOnFocus="SelectAll" ShowButton="True">
                                </DateInput>
                                <Calendar ID="Calendar1" Skin="Vista" runat="server">
                                </Calendar>
                            </telerik:RadDatePicker>
                        </td>
                        <td align="left" style="width: 350px">
                            <telerik:RadComboBox ID="cboObservacion1" runat="server" Skin="Vista" Width="99%"
                                ZIndex="990000000">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="Todos" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px">
                            <asp:Label ID="Label9" runat="server" SkinID="lblBlack">Visita 2:</asp:Label>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtVisita2" runat="server" Skin="Vista" Width="118px"
                                Culture="Spanish (Argentina)" ZIndex="990000000">
                                <DateInput ID="DateInput2" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Vista"
                                    Skin="Vista" SelectionOnFocus="SelectAll" ShowButton="True">
                                </DateInput>
                                <Calendar ID="Calendar2" Skin="Vista" runat="server">
                                </Calendar>
                            </telerik:RadDatePicker>
                        </td>
                        <td align="left" style="width: 350px">
                            <telerik:RadComboBox ID="cboObservacion2" runat="server" Skin="Vista" Width="99%"
                                ZIndex="990000000">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="Todos" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px">
                            <asp:Label ID="Label10" runat="server" SkinID="lblBlack">Visita 3:</asp:Label>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtVisita3" runat="server" Skin="Vista" Width="118px"
                                Culture="Spanish (Argentina)" ZIndex="990000000">
                                <DateInput ID="DateInput3" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Vista"
                                    Skin="Vista" SelectionOnFocus="SelectAll" ShowButton="True">
                                </DateInput>
                                <Calendar ID="Calendar3" Skin="Vista" runat="server">
                                </Calendar>
                            </telerik:RadDatePicker>
                        </td>
                        <td align="left" style="width: 350px">
                            <telerik:RadComboBox ID="cboObservacion3" runat="server" Skin="Vista" Width="99%"
                                ZIndex="990000000">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="Todos" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding-bottom: 5px; padding-left: 3px; padding-top: 5px;
                            border-top-style: none; border-left-style: none; border-bottom-style: double;
                            border-bottom-width: thin; border-bottom-color: #006699">
                            <asp:Label ID="Label11" runat="server" SkinID="lblBlackBold">Datos Logística de Cobranza</asp:Label>
                        </td>
                    </tr>
                    <tr style="padding-top: 5px;">
                        <td style="width: 60px">
                            <asp:Label ID="Label12" runat="server" SkinID="lblBlack">ESTADO:</asp:Label>
                        </td>
                        <td colspan="2">
                            <telerik:RadComboBox Width="280px" ID="cboEstadoGuias" runat="server" Skin="Vista"
                                ZIndex="990000000" AppendDataBoundItems="true" OnClientSelectedIndexChanged="ShowCargaAdicionalSimple">
                                <Items>
                                    <telerik:RadComboBoxItem Value="-1" Text="" />
                                </Items>
                            </telerik:RadComboBox>
                            &nbsp;&nbsp;<asp:Label ID="Label18" runat="server" SkinID="lblBlack">FECHA:</asp:Label>
                            <telerik:RadDatePicker ID="txtFechaEstado" runat="server" Skin="Vista" Width="118px"
                                Culture="Spanish (Argentina)" ZIndex="990000000">
                                <DateInput ID="DateInput4" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Vista"
                                    Skin="Vista" SelectionOnFocus="SelectAll" ShowButton="True">
                                </DateInput>
                                <Calendar ID="Calendar4" Skin="Vista" runat="server">
                                </Calendar>
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr style="padding-top: 5px; display: none" id="trLideresSimple">
                        <td style="width: 60px">
                            <asp:Label ID="Label14" runat="server" SkinID="lblBlack">LÍDER:</asp:Label>
                        </td>
                        <td colspan="2">
                            <telerik:RadComboBox Width="280px" ID="cboLideresSimple" runat="server" Skin="Vista"
                                ZIndex="990000000" AllowCustomText="true" MarkFirstMatch="true">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr style="padding-top: 5px; display: none" id="trMotivoSiniestoSimple">
                        <td style="width: 60px">
                            <asp:Label ID="Label15" runat="server" SkinID="lblBlack">MOTIVO:</asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox TextMode="MultiLine" Width="95%" Rows="4" MaxLength="250" ID="txtMotivoSiniestroSimple"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding-bottom: 5px; padding-left: 3px; padding-top: 5px;
                            border-top-style: none; border-left-style: none; border-bottom-style: double;
                            border-bottom-width: thin; border-bottom-color: #006699">
                            <asp:Label ID="Label13" runat="server" SkinID="lblBlackBold">Datos Estado Rendicion</asp:Label>
                        </td>
                    </tr>
                    <tr style="padding-top: 5px;">
                        <td style="width: 60px">
                            <asp:Label ID="Label20" runat="server" SkinID="lblBlack">Estado:</asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblEstadoRendicion" runat="server" SkinID="lblBlack"></asp:Label>
                        </td>
                    </tr>
                    <tr style="padding-top: 5px;">
                        <td style="width: 60px">
                            <asp:Label ID="Label21" runat="server" SkinID="lblBlack">Rendición:</asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblNroRendicion" runat="server" SkinID="lblBlack"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3" style="padding-top: 15px">
                            <asp:Button ID="btnOk" runat="server" Text="Aceptar" SkinID="btnBasic" Width="57px"
                                OnClientClick="GuardarCobranza();return false;" />
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
                                Text="Buscado Despachos...">
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
