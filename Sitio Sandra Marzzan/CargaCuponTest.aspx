<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="CargaCuponTest.aspx.cs"
    Inherits="CargaCuponTest" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Carga Cupón Sorteo</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <link href="UploadFiles/CSS/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/Modal-master/jquery.modal.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="Scripts/Modal-master/jquery.modal.js" type="text/javascript"></script>
</head>
<script type="text/javascript">


    var ctrDepartamento;
    var ctrlocalidad;

    function CargarDepartamentos(sender, arg) {

        $('#' + "<%= txtdepartamento.ClientID %>").val("");
        $('#' + "<%= txtdepartamento.ClientID %>").html("");

        jQuery(function () {
            var empresa = arg.get_item().get_text();
            if (ctrDepartamento == null) {
                options = {
                    serviceUrl: 'ASHX/LoadDepartamentos.ashx',
                    width: '384',
                    minChars: 2,
                    showInit: true,
                    showOnFocus: true,
                    params: { Empresa: encodeURIComponent(empresa) },
                    zIndex: 922000000,
                    onSelect: showLocalidades,
                    onBlurFunction: showLocalidades,
                    disabled_NOESTAENLISTA: true
                };

                ctrDepartamento = $('#' + "<%= txtdepartamento.ClientID %>").autocomplete(options);

            }
            else {
                ctrDepartamento.changeParams({ Empresa: encodeURIComponent(empresa) });
            }

            $("#<%= txtdepartamento.ClientID %>").focus();
        });

    }

    function showLocalidades(value, data, obj, aditionalData) {
        if (value == undefined || value == '' || value == 'NO ESTA EN LA LISTA') {

            $("#<%= txtdepartamento.ClientID %>").val("");
            $("#<%= txtdepartamento.ClientID %>").focus();

        }
    }

    function BorrarDatos() {

        $find("<%= txtApellido.ClientID %>").set_value("");
        $find("<%= txtDni.ClientID %>").set_value("");
        $find("<%= txtEmail.ClientID %>").set_value("");
        $find("<%= txtTelefono.ClientID %>").set_value("");
        $find("<%= txtCodigo.ClientID %>").set_value("");
        $find("<%= cboProvincias.ClientID %>").clearSelection()
        $('#' + "<%= txtdepartamento.ClientID %>").val("");
    }

    function GrabarCupon() {

        if (Page_ClientValidate()) {
            ShowWaiting("Enviando Solicitud..");

            var ape = $find("<%= txtApellido.ClientID %>").get_value();
            var dni = $find("<%= txtDni.ClientID %>").get_value();
            var email = $find("<%= txtEmail.ClientID %>").get_value();
            var tel = $find("<%= txtTelefono.ClientID %>").get_value();
            var cupon = $find("<%= txtCodigo.ClientID %>").get_value();
            var prov = $find("<%= cboProvincias.ClientID %>").get_text();
            var loc = $('#' + "<%= txtdepartamento.ClientID %>").val();

            PageMethods.AsignarCupon(ape, dni, email, tel, cupon, prov, loc, function (datos) {
                HideWaiting();

                if (datos["Error"] != undefined) {
                    alert(datos["Error"]);
                }
                else {

                    $('#modalTarjeta').modal({
                        closeFunction: "BorrarDatos",
                        clickClose: false,
                        showClose: true,
                        fadeDuration: 700,
                        fadeDelay: 0.10

                    });
                }

            }, function (err) {
                HideWaiting();
                alert("Ha ocurrido un error con la aplicación por favor tome contacto con los asistentes de Marzzan: " + err._message);
            });
        }
    }

    $(window).ready(function () {
        Page_ClientValidate();
    });
</script>
<body style="background-color: #e0b396; text-align: center" class="main">
    <form id="form1" runat="server" defaultbutton="btnOculto">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"
        LoadScriptsBeforeUI="true">
        <Scripts>
            <asp:ScriptReference Path="~/FuncionesComunes.js" />
        </Scripts>
    </asp:ScriptManager>
    <asp:Button ID="btnOculto" Style="display: none" runat="server" OnClientClick="return false;" />
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
            </td>
            <td align="center" style="width: 729px">
                <div style="background-image: url('Imagenes/CuponOk/EncabezadoOKm.png'); background-repeat: no-repeat;
                    background-position: center; height: 206px">
                    &nbsp;
                </div>
            </td>
            <td align="center">
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;
            </td>
            <td align="center" style="background-color: #dec5b1; padding-bottom: 10px; width: 729px">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 236px">
                            <img src="Imagenes/CuponOk/LogoLateralOKm.png" />
                        </td>
                        <td align="left" valign="top">
                            <table width="98%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="color: #965f5a; background-color: #dec5b1; font-size: 12px; font-weight: bold;
                                        font-family: Sans-Serif; padding-bottom: 5px">
                                        Cargá el Código de la promoción que se encuentra en tu cupón, junto con tus datos
                                        personales
                                    </td>
                                </tr>
                            </table>
                            <table width="480px" border="0" cellpadding="0" cellspacing="0" style="border: 3px solid #e0b396;
                                background-color: White">
                                <tr>
                                    <td align="right" style="padding-bottom: 5px; padding-top: 5px">
                                        <asp:Label ID="label2" runat="server" Style="font-family: Sans-Serif; font-size: 13px;
                                            padding-right: 5px">Apellido y Nombre:</asp:Label><br />
                                    </td>
                                    <td style="padding-bottom: 5px; padding-top: 5px">
                                        <telerik:RadTextBox ID="txtApellido" runat="server" InvalidStyleDuration="100" MaxLength="35"
                                            Width="90%">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtApellido"
                                            Text="*" ErrorMessage="Debe Ingresar el Apellido del revendedor"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-bottom: 5px">
                                        <asp:Label ID="label1" runat="server" Style="font-family: Sans-Serif; font-size: 13px;
                                            padding-right: 10px">Dni:</asp:Label><br />
                                    </td>
                                    <td style="padding-bottom: 5px">
                                        <telerik:RadMaskedTextBox ID="txtDni" runat="server" DisplayMask="##.###.###" EmptyMessage="Ingrese su n&uacute;mero de documento "
                                            InvalidStyleDuration="100" Mask="########" Width="90%">
                                        </telerik:RadMaskedTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDni"
                                            Text="*" ErrorMessage="Debe Ingresar el DNI"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-bottom: 5px">
                                        <asp:Label ID="label3" runat="server" Style="font-family: Sans-Serif; font-size: 13px;
                                            padding-right: 10px">Provincia:</asp:Label><br />
                                    </td>
                                    <td style="padding-bottom: 5px">
                                        <asp:LinqDataSource ID="LinqDataSource2" runat="server" ContextTypeName="CommonMarzzan.Marzzan_InfolegacyDataContext"
                                            TableName="Clasificaciones" Where="Tipo == @Tipo" OrderBy="Descripcion">
                                            <WhereParameters>
                                                <asp:Parameter DefaultValue="Provincias" Name="Tipo" Type="String" />
                                            </WhereParameters>
                                        </asp:LinqDataSource>
                                        <telerik:RadComboBox ID="cboProvincias" runat="server" DataSourceID="LinqDataSource2"
                                            MarkFirstMatch="true" AllowCustomText="true" DataTextField="Descripcion" DataValueField="IdClasificacion"
                                            Width="90%" OnClientSelectedIndexChanged="CargarDepartamentos">
                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cboProvincias"
                                            Text="*" ErrorMessage="Debe Ingresar la provincia de residencia"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-bottom: 5px">
                                        <asp:Label ID="label4" runat="server" Style="font-family: Sans-Serif; font-size: 13px;
                                            padding-right: 10px">Localidad:</asp:Label><br />
                                    </td>
                                    <td style="padding-bottom: 5px">
                                        <asp:TextBox ID="txtdepartamento" runat="server" Width="90%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtdepartamento"
                                            Text="*" ErrorMessage="Debe Ingresar el departamento"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-bottom: 5px">
                                        <asp:Label ID="label5" runat="server" Style="font-family: Sans-Serif; font-size: 13px;
                                            padding-right: 10px">Email:</asp:Label><br />
                                    </td>
                                    <td style="padding-bottom: 5px">
                                        <telerik:RadTextBox ID="txtEmail" runat="server" InvalidStyleDuration="100" MaxLength="50"
                                            Width="90%">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator Enabled="true" ID="RequiredFieldValidator7" runat="server"
                                            ControlToValidate="txtEmail" Text="*" ErrorMessage="Debe Ingresar un correo electr&oacute;nico"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="regEmail" runat="server" ControlToValidate="txtEmail"
                                            Display="Dynamic" font-name="Arial" Font-Size="11" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-bottom: 5px">
                                        <asp:Label ID="label6" runat="server" Style="font-family: Sans-Serif; font-size: 13px;
                                            padding-right: 10px">Teléfono/s:</asp:Label><br />
                                    </td>
                                    <td style="padding-bottom: 5px">
                                        <telerik:RadTextBox ID="txtTelefono" runat="server" InvalidStyleDuration="100" MaxLength="50"
                                            Width="90%">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator Enabled="true" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="txtTelefono" Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-bottom: 5px">
                                        <asp:Label ID="label7" runat="server" Style="font-family: Sans-Serif; font-size: 13px;
                                            padding-right: 10px">Código Cupon:</asp:Label><br />
                                    </td>
                                    <td style="padding-bottom: 5px">
                                        <telerik:RadMaskedTextBox ID="txtCodigo" runat="server" DisplayMask="#####LL" InvalidStyleDuration="100"
                                            Mask="#####LL" Width="90%">
                                        </telerik:RadMaskedTextBox>
                                        <asp:RequiredFieldValidator Enabled="true" ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="txtCodigo" Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-bottom: 5px; width: 130px">
                                        &nbsp;
                                    </td>
                                    <td style="padding-bottom: 5px">
                                        <asp:Label ID="label8" runat="server" Style="font-family: Sans-Serif; font-size: 12px;">* Los datos marcados con asterisco son impresindibles para que pueda participal. Por favor, antes de enviar el formulario revisá que dichos datos sean correctos.</asp:Label><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-bottom: 5px; width: 130px">
                                        &nbsp;
                                    </td>
                                    <td align="right" style="padding-bottom: 5px; padding-right: 5px">
                                        <asp:Button ID="btnBorrar" OnClientClick="BorrarDatos();return false;" runat="server"
                                            SkinID="btnBasic_Marron" Text="Borrar" />
                                        <asp:Button ID="btnEnviar" OnClientClick="GrabarCupon();return false;" runat="server"
                                            SkinID="btnBasic_Marron" Text="Enviar" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <span style="font-size: 13px; text-align: left; padding-left: 5px; padding-right: 5px;
                                font-weight: bold">BASES PROMOCIÓN 0 KM 2014</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" style="font-size: 11px; text-align: left; padding-left: 5px;
                            padding-right: 5px; width: 480px">
                            <p>
                                1. La Casa del Aroma S.A. (en adelante "EL ORGANIZADOR") con domicilio en Salta
                                921, Godoy Cruz, Mendoza, organiza la promoción "PROMO 0 KM 2014", a través de la
                                cual el ganador recibirá un Auto 0 KM – Volkswagen Gol Trend 3 puertas.</p>
                            <p style="font-size: 11px; text-align: left; padding-left: 5px; padding-right: 5px">
                                2. La promoción comenzará a desarrollarse el día 01 de Enero de 2014 y finalizará
                                el día 28 de Febrero de 2014 y tendrá vigencia en todo el territorio de la República
                                Argentina. Entre estas fechas se enviará con los pedidos de mercadería,un cupón
                                para participar de la promoción junto con cada uno de los productos indicados en
                                nuestro Catálogo o Revista Enero-Febrero 2014 con el logo de la promoción.</p>
                            <p style="font-size: 11px; text-align: left; padding-left: 5px; padding-right: 5px">
                                3. Podrán participar de la promoción las personas físicas mayores de edad domiciliadas
                                en la República Argentina (con excepción de los empleados de "EL ORGANIZADOR), que:
                                3.1. Carguen los datos solicitados en el formulariode la "PROMO 0 KM2014" en www.sandramarzzan.com.ar,
                                desde el 01 de Enero de 2014 y hasta el 24 de Abril de 2014 inclusive. 3.2. Soliciten
                                sin obligación de compra, la remisión de un cupón referido a la "PROMO 0KM 2014"
                                y luego lo reenvíen junto con un dibujo coloreado del logo de Sandra Marzzan, al
                                domicilio de la calle Salta 921, Godoy Cruz, Mendoza (Código postal: 5501) – Promoción
                                "PROMO 0 KM 2014", indicando su nombre completo, domicilio, tipo y número de documento,
                                teléfono e e-mail.Los cupones con los dibujos deberán ser recibidos en la dirección
                                indicada, desde el 01 de Enero de 2014 y hasta el 24 de Abril de 2014 inclusive.</p>
                            <p style="font-size: 11px; text-align: left; padding-left: 5px; padding-right: 5px">
                                4. El ganador de la "PROMO 0 KM 2014" recibirá como premio un AUTO 0 KM – Marca
                                Volkswagen Gol Trend 3 puertas, sujeto a disponibilidad. El mismo deberá ser retirado
                                en la provincia de Mendoza siendo el ganador responsable de los gastos que impliquen
                                traslados, alojamiento y demás, en caso de ser necesarios. El ganador también será
                                responsable de los gastos por otorgamiento y patentamiento.</p>
                            <p style="font-size: 11px; text-align: left; padding-left: 5px; padding-right: 5px">
                                5. La promoción tendrá un sorteo final cuyo procedimiento se detalla a continuación:
                                el día 25 de Abril de 2014 a las 16.00 hs en el domicilio de la calle Manuel García
                                124, Godoy Cruz, Mendoza, se realizará ante escribano público, un sorteo entre todos
                                los formularios que se hayan cargado en www.sandramarzzan.com.ar,más los cupones
                                con los dibujos, los que previamente serán volcados a la página web, que se hayan
                                recepcionado en el domicilio de la calle Salta 921, Godoy Cruz, Mendoza, solicitando
                                participar del sorteo, del 01 de Enero de 2014 al 28 de Febrero de 2014 inclusive.
                                El sorteo se realizará utilizando la función "ALEATORIO.ENTRE" del programa Excel,
                                para seleccionar aleatoriamente a un potencial ganador y a un suplente, para que
                                en caso de que el potencial ganador no cumpla con los requisitos necesarios para
                                que se le adjudique el premio correspondiente, sea dicho suplente quien se convierta
                                en potencial ganador.</p>
                            <p style="font-size: 11px; text-align: left; padding-left: 5px; padding-right: 5px">
                                6. La asignación del premio estará condicionada a que el formulario seleccionado
                                cuente con todos los datos que "EL ORGANIZADOR" haya determinado como obligatorios
                                en su página web y que éstos sean correctos, como así también que el ganador cuente
                                con el cupón con el que participó de la promoción.El potencial ganador del sorteo
                                será contactado por la empresaen el plazo de 3 días hábiles, y se le informará que
                                debe enviar una foto del cupón de la promoción a info@sandramarzzan.com.ar, dentro
                                de las próximas 96 horas.En caso de tratarse de una persona que envió cupón y dibujo,
                                se chequeará que haya cumplido con las normas establecidas por "EL ORGANIZADOR"
                                en el punto número 3.2.y, en caso de corresponder, será contactado en igual plazo
                                al anteriormente indicado. La asignación del Automóvil O KM también está condicionada
                                a que el potencial ganador conteste correctamente 2 (dos) de las 3(tres) preguntas
                                de Cultura General susceptibles de una única respuesta válida, que oportunamente
                                se le formularán. Para ello, elpotencial ganador del premio será contactado telefónicamente
                                en el mismo momento del sorteo y el escribano público le formulará las citadas preguntas.
                                En caso de responder correctamente y cumplir con los demás requisitos establecidos
                                en este punto, quedará automáticamente convertido en el ganador de la "PROMO 0 KM
                                2014".Por el contrario, el dar respuestas incorrectas a las preguntas de Cultura
                                General y/o no cumplir con el resto de los requisitos, le hará perder automáticamente
                                el carácter de potencial ganador. En este caso se pasará a actuar de la misma manera
                                con el suplente quien, a partir de ese momento se convertirá en el nuevo potencial
                                ganador. En caso de que este último conteste correctamentey cumpla con los demás
                                requisitos establecidos en este punto, quedará automáticamente convertido en el
                                ganador de la "PROMO 0 KM 2014".Por el contrario, el dar respuestas incorrectas
                                a las preguntas de Cultura General y/o no cumplir con el resto de los requisitos,
                                le hará perder automáticamente también a él, el carácter de potencial ganador. En
                                tal caso el premio correspondiente se declarará vacante y sin ganador, quedando
                                en poder de EL ORGANIZADOR, quien podrá disponer libremente del mismo.</p>
                            <p style="font-size: 11px; text-align: left; padding-left: 5px; padding-right: 5px">
                                7. El premio será entregado por EL ORGANIZADOR dentro de los sesenta (60) días hábiles
                                de su asignación. El ganador no podrá exigir el canje del premio por dinero u otros
                                bienes. El ganador del sorteo autorizará a "EL ORGANIZADOR", como condición para
                                la asignación y entrega del premio, a difundir sus datos personales, imágenes y
                                voces con fines publicitarios, en los medios y en la forma que "EL ORGANIZADOR"
                                disponga, sin derecho a compensación alguna hasta un (1) año de finalizada la promoción.</p>
                            <p style="font-size: 11px; text-align: left; padding-left: 5px; padding-right: 5px">
                                8. La participación en esta promoción implica la aceptación de estas Bases, así
                                como de las decisiones que, conforme a derecho, adopte "EL ORGANIZADOR" sobre cualquier
                                cuestión no prevista en las mismas.</p>
                            <p style="font-size: 11px; text-align: left; padding-left: 5px; padding-right: 5px">
                                9. Los participantes eximen expresamente a "EL ORGANIZADOR" de toda responsabilidad
                                ocasionada por cualquier daño o perjuicio sufrido por los participantes, proveniente
                                de caso fortuito o fuerza mayor, hechos de terceros y/o cualquier responsabilidad
                                que no resultare imputable en forma directa.</p>
                            <p style="font-size: 11px; text-align: left; padding-left: 5px; padding-right: 5px">
                                10. Una vez entregado el premio, "EL ORGANIZADOR" queda liberado de cualquier responsabilidad
                                por el mismo.
                            </p>
                            <p style="font-size: 11px; text-align: left; padding-left: 5px; padding-right: 5px">
                                11. Estas bases podrán ser solicitadas al 0-810-122-7662 y serán enviadas por correo
                                o vía mail.</p>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="center">
                &nbsp;
            </td>
        </tr>
    </table>
    <div class="modal" id="modalTarjeta" style="width: 550px; background-color: #e3b498">
        <img src="Imagenes/CuponOk/CuponOK.png" />
    </div>
    </form>
</body>
</html>
