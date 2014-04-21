<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="ConsultaEstadoGuias.aspx.cs"
    Inherits="ConsultaEstadoGuias" %>

<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="AjaxInfo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <title>Consulta Estado Guías</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <link href="UploadFiles/CSS/uploadify.css" rel="stylesheet" type="text/css" />

    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>

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
        .Theader
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
        .tdFunction
        {
            background-position: 0 -2100px;
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
            padding: 0px 0px 0px 0px;
            vertical-align: middle;
            background-color: #635c4e;
        }
    </style>
</head>

<script type="text/javascript">
    var IdGuiaSeleccionada = "";

    function GuardarAutorizacion() {
        if (Page_ClientValidate()) {
            var fechaAutorizacion = "";
            var obs = $get("<%= txtObsAuto.ClientID %>").value;

            if ($find("<%= txtFechaAuto.ClientID %>").get_selectedDate() != null)
                fechaAutorizacion = $find("<%= txtFechaAuto.ClientID %>").get_selectedDate().format("dd/MM/yyyy");


            PageMethods.AutorizarGuia(fechaAutorizacion, obs, IdGuiaSeleccionada, callBackFunction, onFailure);
        }
    }

    function ShowAutorizarEntrega() {

        var tds = $(this.event.srcElement.parentElement).find("td");
        var element = this.event.srcElement.parentElement;

        if (tds.length == 0) {
            element = this.event.srcElement.parentElement.parentElement;
        }

        IdGuiaSeleccionada = $(element).find("td[id=IdGuiaSeleccionada]")[0].innerText;

        $find("<%=srvAutorizacion.ClientID %>").set_CollectionDiv('divPrincipalAutorizacion');
        $find("<%=srvAutorizacion.ClientID %>").ShowWindows('divPrincipalAutorizacion', "Autorización Entrega");
        window.event.cancelBubble = true;

        return;
    }

    function ShowCobranza() {
        
        var tds = $(this.event.srcElement.parentElement).find("td");
        var element = this.event.srcElement.parentElement;

        if (tds.length == 0) {
            element = this.event.srcElement.parentElement.parentElement;
            tds = $(element).find("td");
        }


        IdGuiaSeleccionada = $(element).find("td[id=IdGuiaSeleccionada]")[0].innerText;

        $find("<%=srvCobranza.ClientID %>").set_CollectionDiv('divPrincipalCobranza');
        $find("<%=srvCobranza.ClientID %>").ShowWindows('divPrincipalCobranza', tds[1].innerText + " - " + tds[2].innerText);

        var visita1 = $(element).find("td[id=FechaVisita1]")[0].innerText;
        var visita2 = $(element).find("td[id=FechaVisita2]")[0].innerText;
        var visita3 = $(element).find("td[id=FechaVisita3]")[0].innerText;

        var obs1 = $(element).find("td[id=Observacion1]")[0].innerText;
        var obs2 = $(element).find("td[id=Observacion2]")[0].innerText;
        var obs3 = $(element).find("td[id=Observacion3]")[0].innerText;

        var fechaCobro = $(element).find("td[id=FechaCobro]")[0].innerText;
        var Estado = $(element).find("td[id=Estado]")[0].innerText;
        var fechaDevolucion = $(this.event.srcElement.parentElement).find("td[id=FechaDevolucion]")[0].innerText;

        $get("<%=lblEstado.ClientID %>").innerText = Estado;



        if (visita1 != "") {
            dia = visita1.substr(0, 2);
            mes = parseInt(visita1.substr(3, 2)) - 1 + '';
            año = visita1.substr(6);
            Fecha = new Date(año, mes, dia);
            $find("<%= txtVisita1.ClientID %>").set_selectedDate(Fecha);
            $("#<%= txtObservacion1.ClientID %>").val(obs1);

        }
        else {
            $find("<%= txtVisita1.ClientID %>").set_selectedDate(null);
            $("#<%= txtObservacion1.ClientID %>").val("");
        }

        if (visita2 != "") {
            dia = visita2.substr(0, 2);
            mes = parseInt(visita2.substr(3, 2)) - 1 + '';
            año = visita2.substr(6);
            Fecha = new Date(año, mes, dia);
            $find("<%= txtVisita2.ClientID %>").set_selectedDate(Fecha);
            $("#<%= txtObservacion2.ClientID %>").val(obs2);

        }
        else {

            $find("<%= txtVisita2.ClientID %>").set_selectedDate(null);
            $("#<%= txtObservacion2.ClientID %>").val("");

        }

        if (visita3 != "") {
            dia = visita3.substr(0, 2);
            mes = parseInt(visita3.substr(3, 2)) - 1 + '';
            año = visita3.substr(6);
            Fecha = new Date(año, mes, dia);
            $find("<%= txtVisita3.ClientID %>").set_selectedDate(Fecha);
            $("#<%= txtObservacion3.ClientID %>").val(obs3);

        }
        else {

            $find("<%= txtVisita3.ClientID %>").set_selectedDate(null);
            $("#<%= txtObservacion3.ClientID %>").val("");

        }


        if (fechaCobro != "") {
            dia = fechaCobro.substr(0, 2);
            mes = parseInt(fechaCobro.substr(3, 2)) - 1 + '';
            año = fechaCobro.substr(6);
            Fecha = new Date(año, mes, dia);
            $find("<%= txtEntregado.ClientID %>").set_selectedDate(Fecha);
            $find("<%= txtDevolucion.ClientID %>").set_selectedDate(null);
        }
        else if (fechaDevolucion != "") {
            dia = fechaDevolucion.substr(0, 2);
            mes = parseInt(fechaDevolucion.substr(3, 2)) - 1 + '';
            año = fechaDevolucion.substr(6);
            Fecha = new Date(año, mes, dia);

            $find("<%= txtDevolucion.ClientID %>").set_selectedDate(Fecha);
            $find("<%= txtEntregado.ClientID %>").set_selectedDate(null);
        }
        else {
            $find("<%= txtEntregado.ClientID %>").set_selectedDate(null);
            $find("<%= txtDevolucion.ClientID %>").set_selectedDate(null);
        }

        return;

    }


    function callBackFunction(datos) {

        var t = [
        { "header": "&nbsp;" },
        { "header": "Fecha Envio" },
        { "header": "Comprobante" },
        { "header": "Nombre" },
        { "header": "Tipo" },
        { "header": "Direccion" },
        { "header": "Lider" },
        { "header": "Transporte" },
        { "header": "Monto" },
         { "header": "&nbsp;"}];

        var templates = {
            th: '<th class="Theader" align="center">#{header}</th>',
            td: '<tr style="cursor:pointer" onclick="ShowCobranza(this)" >' +
                '<td align="center" style="width:35px" ><img width="18px" src="Imagenes/#{Estado}" alt="#{EstadoTip}" /></td>' +
                '<td align="center" style="width:70px" >#{FechaEnvio}</td>' +
                '<td align="left" style="width:100px" filtro="Comprobante">#{Comprobante}</td>' +
                '<td align="left">#{Nombre}</td>' +
                '<td  align="center" style="width:40px">#{TipoComprobante}</td>' +
                '<td>#{Direccion}</td>' +
                '<td style="width:170px">#{Lider}</td>' +
                '<td style="width:120px">#{Transporte}</td>' +
                '<td align="center" style="width:60px">#{Monto}</td>' +
                '<td style="display:none" id="IdGuiaSeleccionada">#{IdDetalleGuia}</td>' +
                '<td style="display:none" id="FechaVisita1" >#{FechaVisita1}</td>' +
                '<td style="display:none" id="FechaVisita2" >#{FechaVisita2}</td>' +
                '<td style="display:none" id="FechaVisita3" >#{FechaVisita3}</td>' +
                '<td style="display:none" id="Observacion1" >#{Observacion1}</td>' +
                '<td style="display:none" id="Observacion2" >#{Observacion2}</td>' +
                '<td style="display:none" id="Observacion3" >#{Observacion3}</td>' +
                '<td style="display:none" id="FechaCobro" >#{FechaCobranza}</td>' +
                '<td style="display:none" id="FechaDevolucion" >#{FechaDevolucion}</td>' +
                '<td style="display:none" id="Estado" >#{EstadoTip}</td>' +
                '<td align="center" style="width:20px" ><img onclick="ShowAutorizarEntrega(this);return false;" id="imgAutorizar" alt="#{EstadoAutoTip}" width="18px" src="Imagenes/#{EstadoAuto}" alt="Autorizar" /></td>' +
                '</tr>'



        };

        var table = '<table width="100%" id="tbl" border="1" cellpadding="0" cellspacing="0" style="font-size:13px; background-color: Transparent;"><thead><tr>';

        /// Genero la fila de encabezado
        $.each(t, function(key, val) {
            table += $.tmpl(templates.th, val);
        });

        table += '</tr></thead><tbody>';

        /// Genero las filas del body
        row = 0;
        for (var i = 0; i < datos["Guias"].length; i++) {
            table += $.tmpl(templates.td, datos["Guias"][row]);
            row++;
        }

        table += '</tbody></table>';

        /// Asigno la tabla generada al div para dibujarla
        $("#divtbl")[0].innerHTML = table;
        $("#tbl tbody tr:odd").addClass("rowAlt");
        $("#tbl tbody tr:even").addClass("rowSpl");

        $("#tblDetalleCobros").css("display", "block");


        $find("<%=srvCobranza.ClientID %>").CloseWindows();
        $find("<%=srvAutorizacion.ClientID %>").CloseWindows();

        /// Limpio los controles
        $get("<%= txtObsAuto.ClientID %>").value = "";
        $find("<%= txtFechaAuto.ClientID %>").set_selectedDate(null);

        $("#DivWaiting").hide();
    }

    function ErroFunction(datos) {
        alert(datos._message);
    }

    var fileName = "";
    var idDespachoSelected = "";

    function FiltrarDespachos() {
        $("#DivWaiting").show();
        var nroDespacho = $get("<%= txtNroDespacho.ClientID %>").value;
        var nroGuia = $get("<%= txtNroGuia.ClientID %>").value;

        var transporte = $find("<%= cboTransportes.ClientID %>").get_text();
        var estado = $find("<%= cboEstados.ClientID %>").get_value();
        var fechaInicial = "";
        var fechaFinal = "";

        if ($find("<%= txtFechaInicial.ClientID %>").get_selectedDate() != null)
            fechaInicial = $find("<%= txtFechaInicial.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

        if ($find("<%= txtFechaFinal.ClientID %>").get_selectedDate() != null)
            fechaFinal = $find("<%= txtFechaFinal.ClientID %>").get_selectedDate().format("dd/MM/yyyy");



        var take = 1000;
        PageMethods.FiltroDespachos(nroDespacho, fechaInicial, fechaFinal, transporte, estado, nroGuia, 0, take, callBackFunction, onFailure);

        $("#tblDetalleCobros").css("display", "none");
    }

    function onFailure(error) {
        alert(error._message);
    }


    
    
    
</script>

<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form1" runat="server" defaultbutton="imgFiltrar">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        <Scripts>
            <asp:ScriptReference Path="~/FuncionesComunes.js" />
        </Scripts>
    </asp:ScriptManager>
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
                                <div style="position: relative; top: -25px">
                                    <asp:Label Font-Names="Bookman Old Style" Font-Size="22pt" ForeColor="#5F5F5F" ID="Label1"
                                        runat="server">Consulta Estados Guías</asp:Label>
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
                                            <td align="right" valign="bottom" rowspan="4" style="width: 60px">
                                                <asp:ImageButton alt="Filtrar Guías" ID="imgFiltrar" OnClientClick="FiltrarDespachos();return false;"
                                                    runat="server" ImageUrl="~/Imagenes/Search.png" />
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
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label15" runat="server" SkinID="lblBlack">Nº Guía:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox Width="99%" ID="txtNroGuia" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="left" style="padding-left: 5px">
                                                <asp:Label ID="Label16" runat="server" SkinID="lblBlack">Estado:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <telerik:RadComboBox ID="cboEstados" runat="server" Skin="Vista" Width="200px">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Value="0" Text="Todos" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <div id="tblDetalleCobros" style="display: block">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid black">
            <tr>
                <td class="tdFunction">
                    <div>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td valign="baseline" align="right" style="width: 30px; padding-right: 5px; cursor: pointer;">
                                    <asp:ImageButton OnClick="ExportToExcelFunction" ID="ImageButton1" Width="18" Height="18"
                                        runat="server" ImageUrl="~/Imagenes/Excel_24x24.gif" CausesValidation="false" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="Label5" runat="server" SkinID="lblWhite">Exportar Excel</asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <div id="divtbl">
                        <div style="width: 100%; text-align: center">
                            <asp:Label ID="Label14" runat="server" SkinID="lblBlackBold">Por favor ingrese el criterio de busqueda para visualizar las guías</asp:Label>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="tdFunction">
                    <div>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td valign="baseline" align="right" style="width: 30px; padding-right: 5px; cursor: pointer;">
                                    <asp:ImageButton OnClick="ExportToExcelFunction" ID="ImageButton2" Width="18" Height="18"
                                        runat="server" ImageUrl="~/Imagenes/Excel_24x24.gif" CausesValidation="false" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="Label6" runat="server" SkinID="lblWhite">Exportar Excel</asp:Label>
                                </td>
                            </tr>
                        </table>
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
            <div id="divPrincipalCobranza" style="height: 260px; width: 570px;">
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
                            <asp:TextBox Width="99%" ID="txtObservacion1" runat="server" MaxLength="100"></asp:TextBox>
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
                            <asp:TextBox Width="99%" ID="txtObservacion2" MaxLength="100" runat="server"></asp:TextBox>
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
                            <asp:TextBox Width="99%" ID="txtObservacion3" runat="server" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding-bottom: 5px; padding-left: 3px; padding-top: 5px;
                            border-top-style: none; border-left-style: none; border-bottom-style: double;
                            border-bottom-width: thin; border-bottom-color: #006699">
                            <asp:Label ID="Label11" runat="server" SkinID="lblBlackBold">Datos Cobranza</asp:Label>
                        </td>
                    </tr>
                    <tr style="padding-top: 5px;">
                        <td style="width: 60px">
                            <asp:Label ID="Label12" runat="server" SkinID="lblBlack">Entregado:</asp:Label>
                        </td>
                        <td colspan="2">
                            <telerik:RadDatePicker ID="txtEntregado" runat="server" Skin="Vista" Width="118px"
                                Culture="Spanish (Argentina)" ZIndex="990000000">
                                <DateInput ID="DateInput4" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Vista"
                                    Skin="Vista" SelectionOnFocus="SelectAll" ShowButton="True">
                                </DateInput>
                                <Calendar ID="Calendar4" Skin="Vista" runat="server">
                                </Calendar>
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding-bottom: 5px; padding-left: 3px; padding-top: 5px;
                            border-top-style: none; border-left-style: none; border-bottom-style: double;
                            border-bottom-width: thin; border-bottom-color: #006699">
                            <asp:Label ID="Label11ss" runat="server" SkinID="lblBlackBold">Datos Devolución</asp:Label>
                        </td>
                    </tr>
                    <tr style="padding-top: 5px;">
                        <td style="width: 60px">
                            <asp:Label ID="Label12sds" runat="server" SkinID="lblBlack">Devolución:</asp:Label>
                        </td>
                        <td colspan="2">
                            <telerik:RadDatePicker ID="txtDevolucion" runat="server" Skin="Vista" Width="118px"
                                Culture="Spanish (Argentina)" ZIndex="990000000">
                                <DateInput ID="DateInput4" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Vista"
                                    Skin="Vista" SelectionOnFocus="SelectAll" ShowButton="True">
                                </DateInput>
                                <Calendar ID="Calendar4" Skin="Vista" runat="server">
                                </Calendar>
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr style="padding-top: 15px">
                        <td style="width: 60px;">
                            <asp:Label ID="Label13s" runat="server" SkinID="lblBlack">ESTADO:</asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblEstado" runat="server" Style="color: Red; font-weight: bold;">SIN ENTREGAR</asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentControls>
    </AjaxInfo:ServerControlWindow>
    <AjaxInfo:ServerControlWindow ID="srvAutorizacion" runat="server" BackColor="WhiteSmoke"
        WindowColor="Gray" ForeColor="#006699">
        <ContentControls>
            <div id="divPrincipalAutorizacion" style="height: 210px; width: 570px;">
                <table width="95%" border="0" cellspacing="2" cellpadding="0">
                    <tr>
                        <td colspan="2" style="padding-bottom: 5px; padding-left: 3px; border-top-style: none;
                            border-left-style: none; border-bottom-style: double; border-bottom-width: thin;
                            border-bottom-color: #006699">
                            <asp:Label ID="Label17" runat="server" SkinID="lblBlackBold">Información de la  Autorización</asp:Label>
                        </td>
                    </tr>
                    <tr style="padding-top: 5px;">
                        <td style="width: 150px">
                            <asp:Label ID="Label18" runat="server" SkinID="lblBlack">Fecha Autorización:</asp:Label>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtFechaAuto" runat="server" Skin="Vista" Width="110px"
                                Culture="Spanish (Argentina)" ZIndex="990000000">
                                <DateInput ID="DateInput5" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Vista"
                                    Skin="Vista" SelectionOnFocus="SelectAll" ShowButton="True">
                                </DateInput>
                                <Calendar ID="Calendar5" Skin="Vista" runat="server">
                                </Calendar>
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFechaAuto"
                                Text="*" ErrorMessage="Debe Ingresar una observación"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px">
                            <asp:Label ID="Label19" runat="server" SkinID="lblBlack">Observación:</asp:Label>
                        </td>
                        <td align="left" style="width: 450px">
                            <asp:TextBox Width="97%" ID="txtObsAuto" Rows="4" MaxLength="300" TextMode="MultiLine"
                                runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtObsAuto"
                                Text="*" ErrorMessage="Debe Ingresar una observación"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px">
                            <asp:Label ID="Label20" runat="server" SkinID="lblBlack">Usuario Autorizador:</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblUsuarioAutorizacion" runat="server" SkinID="lblBlackBold"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="padding-top: 15px">
                            <asp:Button ID="btnOk" runat="server" Text="Aceptar" SkinID="btnBasic" Width="57px"
                                OnClientClick="GuardarAutorizacion();return false;" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentControls>
    </AjaxInfo:ServerControlWindow>
    <div id="DivWaiting" style="width:100%;height:100%;display:none">
        <div id="DivBloque" class="progressBackgroundFilterBlack">
        </div>
        <div style="position: absolute; top: 45%; left: 42%; padding: 5px; width: 24%; z-index: 1001;
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
                            Text="Buscado Despacho...">
                        </asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
