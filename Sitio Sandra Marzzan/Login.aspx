<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="Login.aspx.cs"
    Inherits="Login" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <title>Acceso de Usuario</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />

    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>

    <script src="Scripts/jquery.tmpl.1.1.1.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .rowAlt
        {
            background-color: #CCF2FF;
        }
        .rowSpl
        {
            background-color: White;
        }
    </style>
</head>

<script type="text/javascript">
    function SeleccionarPagina() {

        window.FormTarjeta.action = "Otra.aspx";
        return true;
    }
    function callBackFunction(datos) {

        //debugger;
        var t = [{ "header": "Nombre" },
        { "header": "DNI" },
        { "header": "Codigo" },
        { "header": "Tipo Cliente" },
        { "header": "Tipo Consultor" },
        { "header": "Tranporte" },
        { "header": "Vendedor"}];

        var templates = {
            th: '<th>#{header}</th>',
            td: '<tr><td align="left">#{Nombre}</td><td style="width:100px">#{DNI}</td><td style="width:70px">#{Codigo}</td><td>#{TipoCliente}</td><td>#{TipoConsultor}</td><td align="left">#{Trasnporte}</td><td align="left">#{Vendedor}</td></tr>'
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
    }

    $(document).ready(function() {
       // PageMethods.GetConsultores(callBackFunction, ErroFunction);
    });

    function Hilite(me, focus) {
        me.style.backgroundColor = false != focus ? "#E9F3F3" : "white";
    }

    function ControlarUsuario() {
        if (document.getElementById("UserName").value == "")
            return false;

    }

</script>

<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <telerik:RadToolTipManager runat="server" ID="RadToolTip5" ShowEvent="OnFocus" Skin="Web20"
        ShowDelay="0" AutoCloseDelay="4000" Width="300px" Position="MiddleRight" RelativeTo="Element">
        <TargetControls>
            <telerik:ToolTipTargetControl TargetControlID="UserName" />
            <telerik:ToolTipTargetControl TargetControlID="Password" />
            <telerik:ToolTipTargetControl TargetControlID="txtClaveNuevaR" />
            <telerik:ToolTipTargetControl TargetControlID="txtClaveNueva" />
            <telerik:ToolTipTargetControl TargetControlID="txtClaveActual" />
            <telerik:ToolTipTargetControl TargetControlID="btnCambioClave" />
        </TargetControls>
    </telerik:RadToolTipManager>
    <table cellpadding="0" cellspacing="0" style="width: 100%" border="0" align="left"
        runat="server" id="tblPrincipal">
        <tr>
            <td align="center" style="width: 100%; height: 20%">
                <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
                    <tr>
                        <td align="right">
                            <div class="Header_panelContainerSimple">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 80%">
            <td align="center" style="width: 100%; height: 409px;" valign="middle">
                <table border="0" cellpadding="0" cellspacing="0" style="background-color: Transparent;">
                    <tr>
                        <td style="width: 26px; height: 42px; background-repeat: no-repeat; background-color: Transparent;
                            background-position: top; background-image: url('Imagenes/Login/Login_r1_c1.png')">
                        </td>
                        <td style="width: 350px; height: 42px; background-repeat: repeat-x; background-color: Transparent;
                            background-position: top; background-image: url('Imagenes/Login/Login_r1_c3.png');
                            text-align: left; vertical-align: top">
                            <asp:Label runat="server" ID="lblCabec" Style="color: White; font-size: 14px; font-family: Viner Hand ITC;
                                font-weight: normal; text-align: left; top: 8px; position: relative;" Text="">Acceso Clientes Marzzan</asp:Label>
                        </td>
                        <td style="width: 39px; height: 42px; background-repeat: no-repeat; background-color: Transparent;
                            background-position: top; background-image: url('Imagenes/Login/Login_r1_c5.png')">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 140px; background-repeat: repeat-y; background-color: Transparent;
                            background-position: left top; background-image: url('Imagenes/Login/Login_r3_c1.png')">
                            &nbsp;
                        </td>
                        <td style="width: 350px; height: 140px; background-color: Transparent;">
                            <table border="0" cellpadding="0">
                                <tr>
                                    <td valign="top">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <table width="100%" id="tblIngreso" runat="server">
                                                    <tr>
                                                        <td align="right" style="color: #0066CC; font-family: Sans-Serif; font-size: 11px">
                                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Nombre 
                                                                        de Usuario:</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="UserName" runat="server" Style="border: 1px solid #008ED2;" Width="150px"
                                                                onfocus="Hilite(this)" onblur="Hilite(this, false)" ToolTip="Debe ingresar el nombre de usuario asignado. Si es la primera vez que ingresa la sistema utilice su Nro. Doc. como usuario y la clave 123.">
                                                            </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                                ErrorMessage="Nombre de Usuario es obligatorio" ToolTip="Nombre de Usuario es obligatorio"
                                                                ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="color: #0066CC; font-family: Sans-Serif; font-size: 11px">
                                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Clave:</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Password" runat="server" TextMode="Password" Style="border: 1px solid #008ED2;"
                                                                Width="150px" onfocus="Hilite(this)" onblur="Hilite(this, false)" ToolTip="Debe ingresar su clave personal.">
                                                            </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                                ErrorMessage="Clave es obligatoria" ToolTip="Clave es obligatoria" ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="color: #0066CC; font-family: Sans-Serif; font-size: 11px"
                                                            colspan="2">
                                                            <asp:LinkButton ID="btnCambioClave" runat="server" ForeColor="#0066CC" OnClientClick="return ControlarUsuario();"
                                                                ToolTip="Debe ingresar el nombre de usuario para cambiar la clave" OnClick="btnCambioClave_Click">Cambiar Datos de Ingreso</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2" style="color: Red;">
                                                            <asp:UpdatePanel ID="upLogin" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:Label ID="lblError" runat="server" Visible="false">El usuario o clave no son correctos, por favor intentelo nuevamente.</asp:Label>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="btnLogin" EventName="Click" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="center" colspan="2" style="color: #0066CC;">
                                        <asp:UpdatePanel ID="upCambio" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table class="style1" id="tblCambioClave" runat="server" visible="false">
                                                    <tr>
                                                        <td align="center" colspan="2" style="color: White; font-size: 12px; font-family: Sans-Serif;
                                                            font-weight: bold; background-image: url('Imagenes/Ingreso-Title.gif'); background-repeat: repeat-x;">
                                                            Cambio de Clave
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label1" runat="server" Style="font-size: 12px">Usuario:</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNuevoLogin" runat="server" Style="border: 1px solid #008ED2;"
                                                                Width="150px" onfocus="Hilite(this)" onblur="Hilite(this, false)" ToolTip="Ingrese el nuevo nombre con el que entrara."></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="UserNameLabel2" runat="server" Style="font-size: 12px">Clave Actual:</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtClaveActual" runat="server" Style="border: 1px solid #008ED2;"
                                                                Width="150px" onfocus="Hilite(this)" onblur="Hilite(this, false)" ToolTip="Debe ingresar la clave anterior."
                                                                TextMode="Password"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="UserNameLabel0" runat="server" Style="font-size: 12px">Nueva Clave:</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtClaveNueva" runat="server" Style="border: 1px solid #008ED2;"
                                                                Width="150px" onfocus="Hilite(this)" onblur="Hilite(this, false)" ToolTip="Debe ingresar la nueva clave."
                                                                TextMode="Password"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="UserNameLabel1" runat="server" Style="font-size: 12px">Repetir Clave:</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtClaveNuevaR" runat="server" Style="border: 1px solid #008ED2;"
                                                                Width="150px" onfocus="Hilite(this)" onblur="Hilite(this, false)" ToolTip="Debe repetir la nueva clave."
                                                                TextMode="Password"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            <asp:Button ID="btnCancelar" runat="server" SkinID="btnBasic" CommandName="Login"
                                                                Text="Cancelar" OnClick="btnCancelar_Click" />
                                                            <asp:Button ID="btnAceptarCambio" runat="server" SkinID="btnBasic" Text="Aceptar"
                                                                OnClick="btnAceptarCambio_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="color: red;">
                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:Label ID="lblErrorUsuario" runat="server" Visible="false">El usuario no es correcto, por favor intentelo nuevamente.</asp:Label>
                                                                    <asp:Label ID="lblErrorClaveActual" runat="server" Visible="false">La clave actual ingresada no es correcta, por favor intentelo nuevamente.</asp:Label>
                                                                    <asp:Label ID="lblErrorCambioClave" runat="server" Visible="false">La nueva clave y su reingreso no coinciden, por favor verifique que sean las mismas.</asp:Label>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="btnAceptarCambio" EventName="Click" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnCambioClave" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnLogin" runat="server" SkinID="btnBasic" Text="Ingresar" ValidationGroup="Login1"
                                                    OnClick="btnLogin_Click" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnCambioClave" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="height: 140px; background-repeat: repeat-y; background-color: Transparent;
                            background-position: top; background-image: url('Imagenes/Login/Login_r3_c5.png')">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 33px; background-repeat: no-repeat; background-color: Transparent;
                            background-position: left top; background-image: url('Imagenes/Login/Login_r5_c1.png')">
                            &nbsp;
                        </td>
                        <td style="height: 33px; background-repeat: repeat-x; background-color: Transparent;
                            background-position: top; background-image: url('Imagenes/Login/Login_r5_c3.png')">
                            &nbsp;
                        </td>
                        <td style="height: 33px; background-repeat: no-repeat; background-color: Transparent;
                            background-position: top; background-image: url('Imagenes/Login/Login_r5_c5.png')">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <div id="divtbl">
                </div>
            </td>
        </tr>
    </table>
    </form>
    <form action="ValidarPSP.aspx" method="post" id="FormTarjeta">
        <input type="HIDDEN" name="NROCOMERCIO" value="11111111" size="8" maxlength="8">
        <input type="HIDDEN" name="NROOPERACION" value="3432" size="10" maxlength="10">
        <input type="HIDDEN" name="MEDIODEPAGO" value="1" size="12">
        <input type="HIDDEN" name="MONTO" value="12334" size="12" maxlength="12">
        <input type="HIDDEN" name="CUOTAS" value="01" size="12">
        <input type="HIDDEN" name="URLDINAMICA" value='“http://www.domino.com" size=17'>
        <input type="submit" value="Llamar" onclick="SeleccionarPagina();" style="display:none" />
    </form>
</body>
</html>
