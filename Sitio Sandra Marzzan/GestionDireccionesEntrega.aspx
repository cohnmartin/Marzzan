<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="GestionDireccionesEntrega.aspx.cs"
    Inherits="GestionDireccionesEntrega" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gesti&oacute;n de Direcciones Alternativas</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">

    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>

    <script src="Scripts/jquery.autocomplete.js" type="text/javascript"></script>

</head>

<script type="text/javascript">
    function GetRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    }
    function returnToParent() {

        var oWnd = GetRadWindow();
        var oArg = new Object();

        oArg.SendMail = true;
        oWnd.argument = oArg;

        oWnd.close();

    }

    function ShowFailSend() {

        var oWnd = GetRadWindow();
        var oArg = new Object();

        oArg.SendMail = false;
        oWnd.argument = oArg;

        oWnd.close();

    }
    var ctrDepartamento;
    var ctrlocalidad;

    function InitCombosEdicion() {
        
        window.setTimeout(function() {

            var text = $find("<%= cboProvincias.ClientID %>").get_text().toUpperCase()
            IniciarDepartamentos(text, true);
            
            if ($("#<%= txtNuevo_Depart_loc.ClientID %>").val() == "") {
                $("#<%= txtCodigoPostal.ClientID %>").attr("disabled", true);
                $("#<%= txtCodigoPostal.ClientID %>").css("background-color", "#fff");
            }


        }, 200);

    }

    function IniciarDepartamentos(texto, esEdicion) {

        if (!esEdicion) {
            $('#' + "<%= txtDepartamento.ClientID %>").val("");
            $('#' + "<%= txtDepartamento.ClientID %>").html("");
            
            
            $('#' + "<%= txtCalle.ClientID %>").val("");
            $('#' + "<%= txtCodigoPostal.ClientID %>").val("");
            
            
            $('#' + "<%= txtCalle.ClientID %>").html("");
            $('#' + "<%= txtCodigoPostal.ClientID %>").html("");
        }

        $('#' + "<%= txtNuevo_Depart_loc.ClientID %>").val("");
        $('#' + "<%= txtNuevo_Depart_loc.ClientID %>").html("");
        
        
       
        
        

        jQuery(function() {
            var empresa = texto;

            if (ctrDepartamento == null) {
                options = {
                    serviceUrl: 'ASHX/LoadDepartamentos.ashx',
                    width: '284',
                    minChars: 2,
                    showInit: true,
                    IsEdit: esEdicion,
                    showOnFocus: true,
                    params: { Empresa: encodeURIComponent(empresa) },
                    zIndex: 922000000,
                    onSelect: showLocalidades,
                    onBlurFunction: showLocalidades
                };

                ctrDepartamento = $('#' + "<%= txtDepartamento.ClientID %>").autocomplete(options);

            }
            else {
                ctrDepartamento.changeParams({ Empresa: encodeURIComponent(empresa) });
            }

            $("#<%= txtDepartamento.ClientID %>").focus();
        });

    }

    function CargarDepartamentos(sender, arg) {
        IniciarDepartamentos(arg.get_item().get_text(), false);
    }

    function showLocalidades(value, data, obj, aditionalData) {

        if (value == undefined || value == '' || value == 'NO ESTA EN LA LISTA') {

            $("#<%= txtNuevo_Depart_loc.ClientID %>").removeAttr("disabled");
            $("#<%= txtNuevo_Depart_loc.ClientID %>").css("background-color", "white");
            $("#<%= txtNuevo_Depart_loc.ClientID %>").focus();

            $("#<%= txtCodigoPostal.ClientID %>").val("");
            $("#<%= txtCodigoPostal.ClientID %>").attr("disabled", false);
            $("#<%= txtCodigoPostal.ClientID %>").css("background-color", "#fff");

        }
        else {
            $("#<%= txtNuevo_Depart_loc.ClientID %>").val("");
            $("#<%= txtNuevo_Depart_loc.ClientID %>").html("");
            $("#<%= txtNuevo_Depart_loc.ClientID %>").attr("disabled", true);
            $("#<%= txtNuevo_Depart_loc.ClientID %>").css("background-color", "#CCCCCC");
            $get("<%= txtCalle.ClientID %>").focus();

            $("#<%= txtCodigoPostal.ClientID %>").val(aditionalData);
            $("#<%= txtCodigoPostal.ClientID %>").attr("disabled", true);
            $("#<%= txtCodigoPostal.ClientID %>").css("background-color", "#fff");

        }

        return;

        /*if (ctrlocalidad == null) {
        options = {
        serviceUrl: 'ASHX/LoadLocalidades.ashx',
        width: '284',
        minChars: 2,
        showInit: true,
        showOnFocus: true,
        params: { Departamento: data },
        zIndex: 922000000
        };

            ctrlocalidad = $('#' + "<%= txtNuevo_Depart_loc.ClientID %>").autocomplete(options);

        }
        else {
        ctrlocalidad.changeParams({ Departamento: data });
        }

        $("#<%= txtNuevo_Depart_loc.ClientID %>").focus();
        */

    }

</script>

<body>
    <form id="form1" runat="server" defaultbutton="btnOculto">
    <asp:Button ID="btnOculto" Style="display: none" runat="server" OnClientClick="return false;" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div>
        <table width="100%" style="height: 100%" border="0" cellpadding="5" cellspacing="0">
            <tr>
                <td>
                    <asp:Label ID="label8" Width="99%" runat="server" SkinID="lblBlue">Provincia:</asp:Label>
                </td>
                <td align="left">
                    <asp:LinqDataSource ID="LinqDataSource2" runat="server" ContextTypeName="CommonMarzzan.Marzzan_InfolegacyDataContext"
                        TableName="Clasificaciones" Where="Tipo == @Tipo">
                        <WhereParameters>
                            <asp:Parameter DefaultValue="Provincias" Name="Tipo" Type="String" />
                        </WhereParameters>
                    </asp:LinqDataSource>
                    <telerik:RadComboBox ID="cboProvincias" runat="server" DataSourceID="LinqDataSource2"
                        DataTextField="Descripcion" DataValueField="IdClasificacion" EmptyMessage="Seleccione una Provincia"
                        Skin="WebBlue" Width="256px" OnClientSelectedIndexChanged="CargarDepartamentos">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="label19" Width="99%" runat="server" SkinID="lblBlue">Departamento/Localidad:</asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtDepartamento" Width="95%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="label13" Width="99%" runat="server" SkinID="lblBlue">Nuevo Dpto/loc:</asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox Enabled="false" ID="txtNuevo_Depart_loc" Style="background-color: #CCCCCC"
                        runat="server" MaxLength="25"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="label15" runat="server" SkinID="lblBlue">Calle y Nro:</asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtCalle" Width="95%" MaxLength="49" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="label17" runat="server" SkinID="lblBlue">C&oacute;digo Postal:</asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtCodigoPostal" runat="server" MaxLength="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" valign="bottom" style="padding-top: 10px">
                    <asp:UpdatePanel ID="upResultado" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button runat="server" ID="btnEnviarSolicitud" SkinID="btnBasic" Text="Enviar Solicitud"
                                OnClick="btnEnviarSolicitud_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
