<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultaDespachos.aspx.cs"
    Theme="SkinMarzzan" Inherits="ConsultaDespachos" %>

<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="AjaxInfo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Consulta de Despachos</title>
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
    function ShowAlert() {
        //debugger;
        var valorFiltro = $("#txtNroDespacho").val().toUpperCase();
        $('#tbl td').parent().css("display", "block");
        if (valorFiltro != "") {
            //Filtto por la localidad
            //            $('#tbl td[filtro=Localidad]:not(:contains(' + valorFiltro + '))').parent().css("display", "none");
            
            //Filtto por el nro de comprobante
            $('#tbl td[filtro=Comprobante]:not(:contains(' + valorFiltro + '))').parent().css("display", "none");
        }

        return;

        var tds = $(this.event.srcElement.parentElement).find("td");
        var idGuia = tds[9].innerText;


        $find("<%=srvCobranza.ClientID %>").set_CollectionDiv('divPrincipalCobranza');
        $find("<%=srvCobranza.ClientID %>").ShowWindows('divPrincipalCobranza', tds[1].innerText + " - " + tds[2].innerText);
        return;


        $(this.event.srcElement.parentElement).find("td").each(function() {
            alert(this.innerText);
        });



    }

    function callBackFunction(datos) {

        var t = [
        { "header": "Estado" },
        { "header": "Comprobante" },
        { "header": "Nombre" },
        { "header": "Tipo" },
        { "header": "Direccion" },
        { "header": "Localidad" },
        { "header": "Monto" },
        { "header": "Lider" },
        { "header": "Tel. Lider"}];

        var templates = {
            th: '<th>#{header}</th>',
            td: '<tr style="cursor:pointer" onclick="ShowAlert(this);"><td align="center" ><img width="18px" src="Imagenes/#{Estado}" alt="#{EstadoTip}" /></td><td align="left" style="width:110px" filtro="Comprobante">#{Comprobante}</td><td align="left">#{Nombre}</td><td  align="center" style="width:40px">#{TipoComprobante}</td><td>#{Direccion}</td><td align="left" filtro="Localidad" >#{Localidad}</td><td align="center" style="width:60px">#{Monto}</td><td style="width:170px">#{Lider}</td><td style="width:150px">#{LiderTel}</td>' +
                '<td style="display:none" >#{IdDetalleGuia}</td></tr>'
        };

        var table = '<table id="tbl" border="1" cellpadding="0" cellspacing="0" style="font-size:13px; background-color: Transparent;"><thead><tr>';

        /// Genero la fila de encabezado
        $.each(t, function(key, val) {
            table += $.tmpl(templates.th, val);
        });

        table += '</tr></thead><tbody>';

        /// Genero las filas del body
        row = 0;
        for (var i = 0; i < datos.length; i++) {
            table += $.tmpl(templates.td, datos[row]);
            row++;
        }

        table += '</tbody></table>';

        /// Asigno la tabla generada al div para dibujarla
        $("#divtbl")[0].innerHTML = table;
        $("#tbl tbody tr:odd").addClass("rowAlt");
        $("#tbl tbody tr:even").addClass("rowSpl");

    }

    function ErroFunction(datos) {
        alert(datos._message);
    }




    var fileName = "";
    var idDespachoSelected = "";

    function FiltrarDespachos() {

        var nroDespacho = $get("<%= txtNroDespacho.ClientID %>").value;
        var cliente = $get("<%= txtCliente.ClientID %>").value;
        var nroGuia = $get("<%= txtNroGuia.ClientID %>").value;
        var transporte = $find("<%= cboTransportes.ClientID %>").get_text();
        var fechaInicial = "";
        var fechaFinal = "";

        if ($find("<%= txtFechaInicial.ClientID %>").get_selectedDate() != null)
            fechaInicial = $find("<%= txtFechaInicial.ClientID %>").get_selectedDate().format("dd/MM/yyyy");

        if ($find("<%= txtFechaFinal.ClientID %>").get_selectedDate() != null)
            fechaFinal = $find("<%= txtFechaFinal.ClientID %>").get_selectedDate().format("dd/MM/yyyy");



        var take = $find("gvDespachos").get_pageSize();
        $find("<%=gvDespachos.ClientID %>").ShowWaiting("Buscando Despachos...");
        PageMethods.FiltroDespachos(nroDespacho, fechaInicial, fechaFinal, cliente, nroGuia, transporte, 0, take, onSuccessFiler, onFailure);
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

//        PageMethods.GetConsultores(id, callBackFunction, ErroFunction);
//        return;

        var item = $find("<%=gvDespachos.ClientID %>").get_ItemDataByKey(id);

        window.open("ArchivosDespacho/" + item.Archivo);

    }

    function GuardarCobranza() {

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
                                        runat="server">Consulta de Despachos</asp:Label>
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
                                                <asp:Label ID="Label4" runat="server" SkinID="lblBlack">Cliente:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox Width="99%" ID="txtCliente" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label5" runat="server" SkinID="lblBlack">Nº Guia:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox Width="99%" ID="txtNroGuia" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="left" style="padding-left: 5px">
                                                <asp:Label ID="Label6" runat="server" SkinID="lblBlack">Transporte:</asp:Label>
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
                                        <AjaxInfo:FunctionColumnRow Type="Custom" ImgUrl="imagenes/download.png" Text="Descargar Guías"
                                            ClickFunction="DescargarGuia" />
                                    </FunctionsColumns>
                                    <Columns>
                                        <AjaxInfo:Column HeaderName="Nro Despacho" DataFieldName="NroDespacho" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Fecha Envio" DataFieldName="FechaEnvio" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Fecha Generacion" DataFieldName="FechaGeneracion" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Transporte" DataFieldName="Transporte" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Archivo" DataFieldName="Archivo" Align="Derecha" />
                                    </Columns>
                                </AjaxInfo:ClientControlGrid>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <div id="divtbl">
    </div>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <AjaxInfo:ServerControlWindow ID="srvCobranza" runat="server" BackColor="WhiteSmoke"
        WindowColor="Gray" ForeColor="#006699">
        <ContentControls>
            <div id="divPrincipalCobranza" style="height: 240px; width: 570px;">
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
                                Culture="Spanish (Argentina)">
                                <DateInput runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Vista"
                                    Skin="Vista" SelectionOnFocus="SelectAll" ShowButton="True">
                                </DateInput>
                                <Calendar Skin="Vista" runat="server">
                                </Calendar>
                            </telerik:RadDatePicker>
                        </td>
                        <td align="left" style="width: 350px">
                            <asp:TextBox Width="99%" ID="txtObservacion1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px">
                            <asp:Label ID="Label9" runat="server" SkinID="lblBlack">Visita 2:</asp:Label>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtVisita2" runat="server" Skin="Vista" Width="118px"
                                Culture="Spanish (Argentina)">
                                <DateInput ID="DateInput1" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Vista"
                                    Skin="Vista" SelectionOnFocus="SelectAll" ShowButton="True">
                                </DateInput>
                                <Calendar ID="Calendar1" Skin="Vista" runat="server">
                                </Calendar>
                            </telerik:RadDatePicker>
                        </td>
                        <td align="left" style="width: 350px">
                            <asp:TextBox Width="99%" ID="txtObservacion2" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px">
                            <asp:Label ID="Label10" runat="server" SkinID="lblBlack">Visita 3:</asp:Label>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtVisita3" runat="server" Skin="Vista" Width="118px"
                                Culture="Spanish (Argentina)">
                                <DateInput ID="DateInput2" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Vista"
                                    Skin="Vista" SelectionOnFocus="SelectAll" ShowButton="True">
                                </DateInput>
                                <Calendar ID="Calendar2" Skin="Vista" runat="server">
                                </Calendar>
                            </telerik:RadDatePicker>
                        </td>
                        <td align="left" style="width: 350px">
                            <asp:TextBox Width="99%" ID="txtObservacion3" runat="server"></asp:TextBox>
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
                                Culture="Spanish (Argentina)">
                                <DateInput ID="DateInput3" runat="server" InvalidStyleDuration="100" LabelCssClass="radLabelCss_Vista"
                                    Skin="Vista" SelectionOnFocus="SelectAll" ShowButton="True">
                                </DateInput>
                                <Calendar ID="Calendar3" Skin="Vista" runat="server">
                                </Calendar>
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px">
                            <asp:Label ID="Label13" runat="server" SkinID="lblBlack">Estado:</asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblEstado" runat="server" SkinID="lblBlackBold">SIN ENTREGAR</asp:Label>
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
