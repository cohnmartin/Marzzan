<%@ Page Language="C#" ValidateRequest="false" Theme="SkinMarzzan" AutoEventWireup="true"
    CodeFile="GestionEscrituraMail.aspx.cs" Inherits="GestionEscrituraMail" %>

<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="AjaxInfo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Gestión de Mensaje</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <link href="UploadFiles/CSS/uploadify.css" rel="stylesheet" type="text/css" />

    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>

    <script src="Scripts/tiny_mce/tiny_mce.js" type="text/javascript"></script>

    <script src="Scripts/jquery.autocomplete.js" type="text/javascript"></script>
    
    <script src="Scripts/jquery.uploadify.js" type="text/javascript"></script>


    <style type="text/css">
        .LibretaContacto
        {
            font: 11px Verdana, Geneva, sans-serif;
            background-color: #F5F5F5;
            border: 3px solid #E5E5E5;
            margin-top: 2px;
            margin-right: 2px;
            padding: 3px;
            float: left;
            width: 665px;
            height: 505px;
            text-align: center;
        }
        .Adjunto
        {
            font: 11px Verdana, Geneva, sans-serif;
            background-color: #F5F5F5;
            border: 3px solid #E5E5E5;
            margin-top: 2px;
            margin-right: 2px;
            padding: 3px;
            float: left;
            text-align: center;
            cursor: pointer;
        }
        a:link
        {
            color: Black;
            text-decoration: none;
        }
        a:visited
        {
            color: Black;
            text-decoration: none;
        }
        a:active
        {
            color: Black;
            text-decoration: none;
        }
    </style>
</head>

<script type="text/javascript">


    var ctrCliente;
    var ctrGrupo;

    jQuery(function() {

        options = {
            serviceUrl: 'ASHX/LoadClientes.ashx',
            width: '384',
            minChars: 3,
            onSelect: LimiarCtrGrupo,
            showInit: false,
            zIndex: 922000000
        };
        ctrCliente = $('#' + "<%= txtCliente.ClientID %>").autocomplete(options);


        options = {
            serviceUrl: 'ASHX/LoadGrupos.ashx',
            width: '384',
            minChars: 3,
            showOnFocus: true,
            showInit: false,
            onSelect: LimiarCtrCliente,
            zIndex: 922000000
        };
        ctrGrupo = $('#' + "<%= txtGrupo.ClientID %>").autocomplete(options);


    });

    function LimiarCtrGrupo() {
        ctrGrupo.Clear();
        AgregarContacto()
    }


    function LimiarCtrCliente() {
        ctrCliente.Clear();
        AgregarContacto()
    }

    function VolverConsola() {
        window.location.href = "ConsolaMail.aspx";
    }

    function EliminarContacto(semder, id) {

        PageMethods.EliminarContacto(id, onSuccess, onFailure);
    }

    function AgregarContacto() {
        var id = "";
        var Tipo = "";
        var Nombre = "";

        if (ctrGrupo.get_SelectedValue() != "") {
            id = ctrGrupo.get_SelectedValue();
            Tipo = "G";
            Nombre = ctrGrupo.get_SelectedText();
        }
        else {
            id = ctrCliente.get_SelectedValue();
            Tipo = "C";
            Nombre = ctrCliente.get_SelectedText();
        }

        PageMethods.AgregarContacto(id, Nombre, Tipo, onSuccess, onFailure);

    }

    function onSuccess(datos) {
        if (datos != null) {
            $find("<%=gvContactosInternos.ClientID %>").set_ClientdataSource(datos);
            ctrGrupo.Clear();
            ctrCliente.Clear();
        }

    }

    function onFailure(error) {
        alert("Error al invocar el web metodo:" + error._message);
    }

    function AbrirArchivo(ruta) {
        window.open(ruta.replace('@', '\\'), '_blank', 'fullscreen=no');
    }


    function NuevoMail() {
        window.location.href = "GestionEscrituraMail.aspx?Accion=Nuevo&id=0";
    }
</script>

<script type="text/javascript">
    var arrayImagenesInsertadas = new Array();

    function ShowTodoGrupo(sender, id, checked) {
        if (checked) {
            $("#<%= lblTodoElGrupo.ClientID %>").css("display", "block");
            $("#<%= gvConsultores.ClientID %>").css("display", "none");
        }
        else {
            $("#<%= lblTodoElGrupo.ClientID %>").css("display", "none");
            $("#<%= gvConsultores.ClientID %>").css("display", "block");
        }

    }

    function RecuperarSeleccion() {


        var listaContactos = $find("<%= gvContactosInternos.ClientID %>").get_ItemsData()
        var resultado = "";

        for (var i = 0; i < listaContactos.length; i++) {
            resultado += listaContactos[i]["Nombre"] + "; ";
        }

        $find("<%= txtPara.ClientID %>").set_value(resultado);
        $find("<%=srvContactosInternos.ClientID %>").CloseWindows();

        return;

        //        var gruposMarcados = $find("<%= gvGrupos.ClientID %>").get_ItemsDataByFilter(true, "Seleccion")
        //        var consultoresMarcados = $find("<%= gvConsultores.ClientID %>").get_ItemsDataByFilter(true, "Seleccion")
        //        var resultado = "";
        //        for (var i = 0; i < gruposMarcados.length; i++) {
        //            resultado += gruposMarcados[i]["Grupo"] + "; ";
        //        }

        //        for (var i = 0; i < consultoresMarcados.length; i++) {

        //            if (resultado.indexOf(consultoresMarcados[i]["Grupo"]) < 0)
        //                resultado += consultoresMarcados[i]["Consultor"] + "; ";

        //        }

        //        $find("<%= txtPara.ClientID %>").set_value(resultado);
        //        $find("<%=srvContactos.ClientID %>").CloseWindows();
    }

    function BuscarConsultores(sender, id, checked) {

        if (!$find("<%= gvGrupos.ClientID %>").get_ItemDataByKey(id)["Seleccion"]) {
            $("#<%= lblTodoElGrupo.ClientID %>").css("display", "none");
            $("#<%= gvConsultores.ClientID %>").css("display", "block");
            $find("<%= gvConsultores.ClientID %>").filterByText(id, "Grupo")

        }
        else {
            $("#<%= lblTodoElGrupo.ClientID %>").css("display", "block");
            $("#<%= gvConsultores.ClientID %>").css("display", "none");
        }
    }

    function OnKeyPress(sender, eventArgs) {
        var c = eventArgs.get_keyCode();
        if (c == 13) {
            $find("<%= gvConsultores.ClientID %>").filterByText($find("<%=txtFiltro.ClientID%>").get_value(), "Consultor")
        }
    }

    var names;
    var namesInsertImage;

    function EnviarMail() {
        debugger;
        var asunto = $find("<%= txtAsunto.ClientID %>").get_value();
        if (asunto == "") {

            alert("Debe ingresar un asunto para poder enviar el mensaje.");
        }
        else {

            // Esto se deshabilito para que sepueda agregar varios destinatarios aun cdo se esta 
            // respondiento un mail.

            //if ("<%=Accion %>" != "Responder") {

            var listaContactos = $find("<%= gvContactosInternos.ClientID %>").get_ItemsData();
            if (listaContactos != null) {
                $get("<%=hdGrupos.ClientID%>").value = "";
                for (var i = 0; i < listaContactos.length; i++) {
                    if (listaContactos[i]["Tipo"] == "G")
                        $get("<%=hdGrupos.ClientID%>").value += listaContactos[i]["Id"] + ";";
                }

                $get("<%=hdClientes.ClientID%>").value = "";
                for (var i = 0; i < listaContactos.length; i++) {
                    if (listaContactos[i]["Tipo"] == "C")
                        $get("<%=hdClientes.ClientID%>").value += listaContactos[i]["Id"] + ";";
                }
            }
            //}


            if ($get("<%=hdClientes.ClientID%>").value != "" || $get("<%=hdGrupos.ClientID%>").value != "") {


                ///Recupero los nombres de los archivos adjuntos
                names = $('#<%=FileUpload2.ClientID%>').getFilesNames().join();

                ///Recupero los nombres de las imagenes insertadas en el cuerpo del mail
                namesInsertImage = arrayImagenesInsertadas.join();

                if (names.length > 0) {
                    // Inicio el upload de los archivos adjuntos y cuando este termina
                    // se ejecuta la funcion FunctionTerminoAdjuntos para ir al servidor
                    // y guardar los datos del mail.
                    $('#<%=FileUpload2.ClientID%>').fileUploadStart();
                }
                else {
                    /// si no hay archivos adjuntos entonces inicio la grabacion desde aca.
                    var content = tinyMCE.get('elm1').getContent();
                    $find("<%=RadAjaxManager1.ClientID%>").ajaxRequest(content + "|" + names + "|" + namesInsertImage);
                }


            }
            else {
                alert('Debe ingresar al menos un destinatario para poder enviar el mensaje.');
            }
        }
    }

    function FunctionTerminoAdjuntos(sender, arg, infoArchivo, DatosArchivo, aa) {


        var content = tinyMCE.get('elm1').getContent();
        $find("<%=RadAjaxManager1.ClientID%>").ajaxRequest(content + "|" + names + "|" + namesInsertImage);

    }

    function ShowContactos() {

        $find("<%=srvContactosInternos.ClientID %>").set_CollectionDiv('divContactosInternos');
        $find("<%=srvContactosInternos.ClientID %>").ShowWindows('divContactosInternos', "Seleccione los contactos");

        return;

        $find("<%=srvContactos.ClientID %>").set_CollectionDiv('divPrincipalContactos');
        $find("<%=srvContactos.ClientID %>").ShowWindows('divPrincipalContactos', "Seleccione los contactos");

    }

    function Responder() {

        window.location.href = "GestionEscrituraMail.aspx?Accion=Responder&id=<%=IdMail %>";


    }

    function isReadOnly(s) {
        var falses = { "false": true, "False": true };
        return !falses[s];
    }

    var readonly = isReadOnly("<%=ReadOnlyEditor %>");

    /// Si se quiere que se controle los cambios en el documento se tiene
    // que agregar esta opcion:  ,autosave
    tinyMCE.init({
        // General options
        mode: "specific_textareas",
        editor_selector: "editorMail",
        theme: "advanced",
        plugins: "autolink,lists,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,spellchecker,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,wordcount,advlist",
        language: 'es',
        readonly: readonly,
        spellchecker_languages: "+Spanish=es",
        spellchecker_rpc_url: "TinyMCE.ashx?module=SpellChecker",


        // Theme options
        theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,fontselect,fontsizeselect,|,bullist,numlist,|,outdent,indent,|,undo,redo,|,insertdate,inserttime,|,forecolor,backcolor,insertImg,spellchecker",
        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "left",
        theme_advanced_statusbar_location: "bottom",
        theme_advanced_resizing: false,

        // Example content CSS (should be your site CSS)
        content_css: "css/content.css",


        formats: {
            alignleft: { selector: 'p,h1,h2,h3,h4,h5,h6,td,th,div,ul,ol,li,table,img', classes: 'left' },
            aligncenter: { selector: 'p,h1,h2,h3,h4,h5,h6,td,th,div,ul,ol,li,table,img', classes: 'center' },
            alignright: { selector: 'p,h1,h2,h3,h4,h5,h6,td,th,div,ul,ol,li,table,img', classes: 'right' },
            alignfull: { selector: 'p,h1,h2,h3,h4,h5,h6,td,th,div,ul,ol,li,table,img', classes: 'full' },
            bold: { inline: 'span', 'classes': 'bold' },
            italic: { inline: 'span', 'classes': 'italic' },
            underline: { inline: 'span', 'classes': 'underline', exact: true },
            strikethrough: { inline: 'del' }
        },

        // Replace values for the template plugin
        template_replace_values: {
            username: "Some User",
            staffid: "991234"
        },

        setup: function(ed) {
            // Add a custom button 
            ed.addButton('insertImg', {
                title: 'Insertar Imagen en el cuerpo del Mensaje',
                image: 'Imagenes/image_add.png',
                onclick: function() {

                    $find("<%=srvInsertImage.ClientID %>").set_CollectionDiv('divPrincipal');
                    $find("<%=srvInsertImage.ClientID %>").ShowWindows('divPrincipal', "Insertar Imagen En Documento");

                }
            });



        }

    });



    $(window).load(
    function() {
        $("#<%=FileUpload1.ClientID %>").fileUpload({
            'uploader': 'UploadFiles/scripts/uploader.swf',
            'cancelImg': 'UploadFiles/images/cancel.png',
            'buttonText': 'Buscar Archivos',
            'script': 'UploadFiles/Upload.ashx',
            'folder': 'TempImgMail',
            'fileDesc': 'Image Files',
            'fileExt': '*.jpg;*.jpeg;*.gif;*.png',
            'multi': false,
            'auto': true,
            'sizeLimit': 1024 * 1024,
            'onComplete': FunctionTermino
        });

    }

);

    $(window).load(

    function() {
        $("#<%=FileUpload2.ClientID %>").fileUpload({
            'uploader': 'UploadFiles/scripts/uploader.swf',
            'cancelImg': 'UploadFiles/images/cancel.png',
            'buttonImg': 'Imagenes/adjuntar.png',
            'wmode': 'transparent',
            'buttonText': 'Adjuntar',
            'script': 'UploadFiles/Upload.ashx',
            'folder': 'TempImgMail',
            'fileDesc': 'Archivos Permitidos (.jpg; .jpeg; .gif; .png; .doc; .docx; .xls; .xlsx; .pdf;)',
            'fileExt': '*.jpg;*.jpeg;*.gif;*.png;*.doc;*.docx;*.xls;*.xlsx;*.pdf;',
            'multi': true,
            'width': '45',
            'auto': false,
            'sizeLimit': 1024 * 1024 * 1.5,
            'onAllComplete': FunctionTerminoAdjuntos
        });
    }

);
    function FunctionTermino(sender, arg, infoArchivo, DatosArchivo, aa) {


        var imgFile = DatosArchivo.split('|')[0].replace("~/", "");
        var ancho = DatosArchivo.split('|')[1];
        var alto = DatosArchivo.split('|')[2];
        arrayImagenesInsertadas.push(imgFile);

        tinyMCE.execCommand('mceInsertContent', false, '<img src="' + imgFile + '" width=' + ancho + ' Height=' + alto + ' />');

        $find("<%=srvInsertImage.ClientID %>").CloseWindows();
    }

    
</script>

<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form2" runat="server" defaultbutton="btnOculto">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <asp:Button ID="btnOculto" Style="display: none" runat="server" OnClientClick="return false;" />
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Web20" VisibleTitlebar="true"
        Title="Atención">
        <windows>
            <telerik:RadWindow ID="RadWindow2" runat="server" Behaviors="Close" Width="870" Title="Comprobante Pedido"
                Height="600" Modal="true" Overlay="false" NavigateUrl="ReportViewer.aspx" VisibleTitlebar="true"
                VisibleStatusbar="false" ShowContentDuringLoad="false" Skin="WebBlue">
            </telerik:RadWindow>
        </windows>
    </telerik:RadWindowManager>
    <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="Header_panelContainerSimple">
                    <div class="CabeceraInicial">
                    </div>
                </div>
                <div class="CabeceraContent">
                    <table width="98%" border="0" cellspacing="0">
                        <tr style="display: none">
                            <td valign="top" align="center" style="width: 100%;">
                                <div style="position: relative; top: -25px">
                                    <asp:Label Font-Names="Bookman Old Style" Font-Size="22pt" ForeColor="#5F5F5F" ID="Label1"
                                        runat="server">Editor de Mensaje</asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="left" style="width: 100%; padding-bottom: 2px; padding-top: 2px">
                                <div id="divLectura" runat="server">
                                    <table width="98%" border="0" cellspacing="0">
                                        <tr>
                                            <td align="left" valign="bottom" style="width: 80%; border-bottom-style: groove;
                                                border-bottom-width: thin; border-bottom-color: #808080; padding-bottom: 10px">
                                                <asp:Label ID="lblAsunto" runat="server" SkinID="lblBlack">ASUENTO: ESTE ES EL MOTIVO POR EL CUAL SE ENVIO EL Mensaje</asp:Label>
                                            </td>
                                            <td align="right" style="border-bottom-style: groove; border-bottom-width: thin;
                                                border-bottom-color: #808080">
                                                <table border="0" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 34px">
                                                            <asp:ImageButton runat="server" ID="ImageButton3" Width="32" Height="32" ImageUrl="~/Imagenes/BackConsola.png"
                                                                OnClientClick="VolverConsola(); return false;" />
                                                        </td>
                                                        <td style="width: 65px" align="left">
                                                            <asp:Label ID="Label4" runat="server" SkinID="lblBlack">Consola Mensaje</asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left" style="padding-left: 10px; border-bottom-style: groove;
                                                border-bottom-width: thin; border-bottom-color: #808080;">
                                                <table width="98%" border="0" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 50px" rowspan="2">
                                                            <img alt="a" src="Imagenes/user.png" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblOrigen" runat="server" SkinID="lblBlack">Persona quien envio el Mensaje</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTelefono" runat="server" SkinID="lblBlack">Tel: 261 4677513</asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top" align="right" style="border-bottom-style: groove; border-bottom-width: thin;
                                                border-bottom-color: #808080; font-weight: bold">
                                                <asp:Label ID="lblFechaHora" runat="server" SkinID="lblBlack">18.09.2012 15:30 hs</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="padding-left: 10px; border-bottom-style: groove; border-bottom-width: thin;
                                                border-bottom-color: #808080;">
                                                <div style="" runat="server" id="divAdjuntos">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="divEnvio" runat="server">
                                    <table width="98%" border="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <table width="98%" border="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" align="left" style="width: 65px; padding-left: 5px;">
                                                            <asp:Label ID="Label8" runat="server" SkinID="lblBlack">Para:</asp:Label>
                                                        </td>
                                                        <td valign="top" align="left" style="width: 90%; float: left; font-weight: bold">
                                                            <div id="div1" style="display: inline; vertical-align: top">
                                                                <telerik:RadTextBox Width="80%" ID="txtPara" runat="server" EmptyMessage="Seleccione los destinatarios del mensaje"
                                                                    Wrap="true" Skin="WebBlue" Rows="3" TextMode="MultiLine" ReadOnly="true">
                                                                </telerik:RadTextBox>
                                                                <asp:HiddenField ID="hdGrupos" runat="server" Value="" />
                                                                <asp:HiddenField ID="hdClientes" runat="server" Value="" />
                                                            </div>
                                                            <div id="div2" style="display: inline">
                                                                <asp:ImageButton ID="btnPara" runat="server" ImageUrl="~/Imagenes/users.png" OnClientClick="ShowContactos(); return false;"
                                                                    Width="42px" Height="42px" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="left" style="padding-left: 5px;">
                                                            <asp:Label ID="Label7" runat="server" SkinID="lblBlack">Asunto:</asp:Label>
                                                        </td>
                                                        <td valign="top" align="left" style="font-weight: bold">
                                                            <telerik:RadTextBox Width="456px" ID="txtAsunto" runat="server" EmptyMessage="Ingrese el asuento del mensaje"
                                                                MaxLength="450" Skin="WebBlue">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left" style="width: 100%; border-bottom-style: groove; border-bottom-width: thin;
                                                border-bottom-color: #808080;">
                                                <table width="98%" border="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <div style="padding: 5px; display: inline">
                                                                <asp:FileUpload ID="FileUpload2" runat="server" Style="display: none" />
                                                            </div>
                                                        </td>
                                                        <td align="right">
                                                            <table border="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="width: 34px">
                                                                        <asp:ImageButton runat="server" ID="ImageButton4" Width="32" Height="32" ImageUrl="~/Imagenes/BackConsola.png"
                                                                            OnClientClick="VolverConsola(); return false;" />
                                                                    </td>
                                                                    <td style="width: 65px" align="left">
                                                                        <asp:Label ID="Label9" runat="server" SkinID="lblBlack">Consola Mensaje</asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr id="trLecturaMail" runat="server">
                            <td valign="top" align="left" style="width: 100%; padding-left: 5px">
                                <table width="98%" border="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 34px">
                                            <asp:ImageButton runat="server" ID="btnResponder" ImageUrl="~/Imagenes/MailResponder.png"
                                                OnClientClick="Responder(); return false;" />
                                        </td>
                                        <td style="width: 87%">
                                            <asp:Label ID="lblResponder" runat="server" SkinID="lblBlack">Responder Mensaje</asp:Label>
                                        </td>
                                        <td style="width: 34px">
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/NuevoMail.png"
                                                OnClientClick="NuevoMail(); return false;" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" SkinID="lblBlack">Nuevo Mensaje</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trNuevoMail" runat="server">
                            <td valign="top" align="left" style="width: 100%; padding-left: 5px">
                                <table width="98%" border="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 34px">
                                            <asp:UpdatePanel runat="server" ID="upEnviar" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/EnviarMail.png"
                                                        OnClientClick="EnviarMail(); return false;" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" SkinID="lblBlack">Enviar Mensaje</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <!-- Gets replaced with TinyMCE, remember HTML in a textarea should be encoded -->
                                <div>
                                    <textarea class="editorMail" id="elm1" name="elm1" rows="22" cols="85" runat="server"
                                        style="width: 95%">
			                        </textarea>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
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
                                Text="Enviando Mensaje...">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <AjaxInfo:ServerControlWindow ID="srvInsertImage" runat="server" BackColor="WhiteSmoke"
        WindowColor="Azul">
        <ContentControls>
            <div id="divPrincipal" style="height: 70px; width: 330px">
                <div style="padding: 10px">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </div>
            </div>
        </ContentControls>
    </AjaxInfo:ServerControlWindow>
    <AjaxInfo:ServerControlWindow ID="srvContactos" runat="server" BackColor="WhiteSmoke"
        WindowColor="Azul">
        <ContentControls>
            <div id="divPrincipalContactos" style="height: 525px; width: 685px">
                <div class="LibretaContacto">
                    <table width="100%" border="0" cellspacing="0">
                        <tr>
                            <td colspan="2">
                                <telerik:RadTextBox Width="99%" ID="txtFiltro" runat="server" EmptyMessage="Ingrese el texto para buscar los consultores"
                                    Skin="WebBlue">
                                    <clientevents onkeypress="OnKeyPress" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="float: left; width: 250px; height: 440px; padding-right: 5px">
                                    <AjaxInfo:ClientControlGrid ID="gvGrupos" runat="server" AllowMultiSelection="false"
                                        ShowDataOnInit="true" KeyName="Grupo" TypeSkin="Vista" PositionAdd="Top" AllowRowSelection="true"
                                        OnClientFunctionRowClicked="BuscarConsultores" Height="99%" Width="250px" AllowPaging="false"
                                        PageSize="50" EmptyMessage="No posee correos electrónicos">
                                        <Columns>
                                            <AjaxInfo:Column HeaderName="&nbsp;" DataFieldName="Seleccion" Align="Centrado" DataType="Bool"
                                                Enabled="true" onClientClick="ShowTodoGrupo" />
                                            <AjaxInfo:Column HeaderName="Grupo" DataFieldName="Grupo" Align="Derecha" />
                                        </Columns>
                                    </AjaxInfo:ClientControlGrid>
                                </div>
                            </td>
                            <td valign="middle" align="center">
                                <div style="float: left; width: 400px; height: 440px; overflow: auto; vertical-align: middle">
                                    <AjaxInfo:ClientControlGrid ID="gvConsultores" runat="server" AllowMultiSelection="false"
                                        ShowDataOnInit="false" KeyName="Id" TypeSkin="Vista" PositionAdd="Top" AllowRowSelection="false"
                                        Height="99%" Width="380px" AllowPaging="false" PageSize="350" EmptyMessage="No posee correos electrónicos">
                                        <Columns>
                                            <AjaxInfo:Column HeaderName="&nbsp;" DataFieldName="Seleccion" Align="Centrado" DataType="Bool"
                                                Enabled="true" />
                                            <AjaxInfo:Column HeaderName="Consultor" DataFieldName="Consultor" Align="Derecha" />
                                            <AjaxInfo:Column HeaderName="Grupo" DataFieldName="Grupo" Align="Derecha" Display="false" />
                                        </Columns>
                                    </AjaxInfo:ClientControlGrid>
                                    <asp:Label ID="lblTodoElGrupo" runat="server" Style="font-weight: bold; color: black;
                                        display: none; padding-top: 100px" Text="Al seleccionar el grupo se enviará el mensaje a todos los integrantes del mismo">
                                    </asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="lbltitulopaciente1" runat="server" Style="font-weight: bold; color: Red"
                                    Text="Solo se mostrarán hasta 20 consultores, utilice el filtro para buscar un consultor en particular">
                                </asp:Label>
                            </td>
                        </tr>
                    </table>
                    <input type="button" value="Finalizar" onclick="RecuperarSeleccion(); return false;" />
                </div>
            </div>
        </ContentControls>
    </AjaxInfo:ServerControlWindow>
    <AjaxInfo:ServerControlWindow ID="srvContactosInternos" runat="server" BackColor="WhiteSmoke"
        WindowColor="Azul">
        <ContentControls>
            <div id="divContactosInternos" style="height: 525px; width: 685px">
                <div class="LibretaContacto">
                    <table width="100%" border="0" cellspacing="0">
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label2" runat="server" SkinID="lblBlack">Buscar Grupo</asp:Label>
                            </td>
                            <td colspan="2" align="center">
                                <asp:Label ID="Label3" runat="server" SkinID="lblBlack">Buscar usuario</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox Width="300px" ID="txtGrupo" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox Width="300px" ID="txtCliente" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnAgregarContacto" runat="server" ImageUrl="~/Imagenes/AddRecord.gif"
                                    OnClientClick="AgregarContacto(); return false;" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <AjaxInfo:ClientControlGrid ID="gvContactosInternos" runat="server" AllowMultiSelection="false"
                                    ShowDataOnInit="true" KeyName="Id" TypeSkin="Vista" PositionAdd="Top" AllowRowSelection="true"
                                    Height="420px" Width="99%" AllowPaging="false" PageSize="50" EmptyMessage="No hay contactos seleccionados">
                                    <FunctionsColumns>
                                        <AjaxInfo:FunctionColumnRow Type="Delete" Text="Sacar contacto de la lista" ClickFunction="EliminarContacto" />
                                    </FunctionsColumns>
                                    <Columns>
                                        <AjaxInfo:Column HeaderName="Contacto" DataFieldName="Nombre" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Tipo" DataFieldName="Tipo" Align="Derecha" />
                                        <AjaxInfo:Column HeaderName="Id" DataFieldName="Id" Display="false" />
                                    </Columns>
                                </AjaxInfo:ClientControlGrid>
                            </td>
                        </tr>
                    </table>
                    <input type="button" value="Finalizar" onclick="RecuperarSeleccion(); return false;" />
                </div>
            </div>
        </ContentControls>
    </AjaxInfo:ServerControlWindow>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest"
        ClientEvents-OnResponseEnd="VolverConsola">
    </telerik:RadAjaxManager>
    </form>
</body>
</html>
