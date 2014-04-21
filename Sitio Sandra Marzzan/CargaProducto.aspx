<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="CargaProducto.aspx.cs" Inherits="CargaProducto" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="ValidacionesCliente.js"></script>
</head>
<script type="text/javascript">
    var codigoEdicion = "<%= CodigoEdicion %>";
    
    function Init() {

        var ctrName = "";
        var codigos = "<%= Codigos %>";
        var precios = "<%= Precios %>";
        var descripciones = "<%= Descripciones %>";
        var cantidad = "<%= Cantidad %>";
        
        
        var id = "<%= Id%>";
        var ProductName = "<%= ProductName %>";

        
        var ArrayCodigos = codigos.split('|');
        var ArrayDescripciones = descripciones.split('|');
        var ArrayPrecios = precios.split('|');


        var i = 0;
        for (i = 0; i < ArrayCodigos.length - 1; i++) {
            eval('document.getElementById(\'' + ctrName + 'lblCol' + i + '\').innerText = ' + 'ArrayDescripciones[i];');
            eval('document.getElementById(\'' + ctrName + 'IdPres' + i + '\').value = ' + 'ArrayCodigos[i];');
            eval('document.getElementById(\'' + ctrName + 'Row' + i + '\').style.display = ' + '\'block\';');
            eval('document.getElementById(\'' + ctrName + 'txtValor' + i + '\').toolTip = ' + '\'Valor Unitario: $' + ArrayPrecios[i] + '\';');

            if (codigoEdicion != "0") {
                var codigoActual = ArrayCodigos[i].replace(/^\s*|\s*$/g, ""); ;

                if (codigoEdicion == codigoActual)
                    eval('document.getElementById(\'' + ctrName + 'txtValor' + i + '\').value = ' + cantidad + '\;');
                else
                    eval('document.getElementById(\'' + ctrName + 'Row' + i + '\').style.display = ' + '\'none\';');
            }
            else
                eval('document.getElementById(\'' + ctrName + 'txtValor' + i + '\').value = \'0\' ;');
            
        }

        //if (lblProducto != null)
        lblProducto.innerText = ProductName;


        HiddenId = document.getElementById(ctrName + "HiddenId");
        HiddenId.value = id;
    }

    function OK_Clicked() {
        var ctrName = "";
        var codigos = "<%= Codigos %>";
        var ArrayCodigos = codigos.split('|');

        var IdPresentacion="";
        var Valor="";
        


        var i = 0;
        for (i = 0; i < ArrayCodigos.length-1; i++) {
            IdPresentacion +=  eval('document.getElementById(\'' + ctrName + 'IdPres' + i + '\').value;') + "|";
            Valor += eval('document.getElementById(\'' + ctrName + 'txtValor' + i + '\').value;') + "|"; 
        }



        HiddenId = document.getElementById("HiddenId");
                
        var oWnd = GetRadWindow();
        var oArg = new Object();

        oArg.IdProducto = HiddenId.value;
        oArg.IdsPresentaciones = IdPresentacion;
        oArg.Valores = Valor;
        oArg.CodigoEdicion = codigoEdicion;
        oWnd.argument = oArg;

        

        
        oWnd.close();
    }

    function GetRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    }

    function clientBeforeShow(sender, eventArgs) {
        sender.set_text(document.getElementById(sender.get_targetControlID()).toolTip);
    }
</script>
<body onload="Init()"  style="background-image: url('Imagenes/vacio.jpg'); background-position: top;background-color:white">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
     <telerik:RadToolTipManager ID="RadToolTipManager1" runat="server" Position="TopLeft"  OnClientBeforeShow="clientBeforeShow"
       Animation="Fade" Skin="Web20" Width="140px" ShowEvent="OnMouseOver" AutoCloseDelay="4000" ShowCallout="true" 
        Height="25px" Style="font-size: 18px; text-align: center; font-family: Arial;">
        
        <TargetControls>
            <telerik:ToolTipTargetControl TargetControlID="txtValor0" />
            <telerik:ToolTipTargetControl TargetControlID="txtValor1" />
            <telerik:ToolTipTargetControl TargetControlID="txtValor2" />
            <telerik:ToolTipTargetControl TargetControlID="txtValor3" />
            <telerik:ToolTipTargetControl TargetControlID="txtValor4" />
            <telerik:ToolTipTargetControl TargetControlID="txtValor5" />
            <telerik:ToolTipTargetControl TargetControlID="txtValor6" />
            <telerik:ToolTipTargetControl TargetControlID="txtValor7" />
            <telerik:ToolTipTargetControl TargetControlID="txtValor8" />
            <telerik:ToolTipTargetControl TargetControlID="txtValor9" />
        </TargetControls>
        
    </telerik:RadToolTipManager>
        
    <input id="HiddenId" type="hidden" runat="server" />
    <table runat="server" id="tblEncabezdo" cellpadding="0" border="0" cellspacing="0"
        style="background:trasparent; font-family: Sans-Serif; font-size: 11px; font-weight: bold; text-align: center;color: White;width: 100%; height: 100%">
        <tr style="height: 150px">
            <td colspan="3" valign="bottom" style="height: 150px" >
                <table width="100%" runat="server" id="tblProducto">
                    <tr>
                        <td style="width: 77%;">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblProducto" runat="server" Text="ddd" ></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="Row0" style="display: none; height: 20px">
            <td style="width: 77%; height: 20px">
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblCol0" runat="server" Text="50 ml" ForeColor="Black"></asp:Label>
            </td>
            <td align="center">
                <asp:TextBox ID="txtValor0" runat="server" Height="16px" CssClass="txtFormatSimple"
                    onkeydown="return ValidarNumerico();" onMousemove="CambiarCursor();" OnClick="SubirCantidad();"
                    Width="60px" MaxLength="4"></asp:TextBox>
                <input id="IdPres0" type="hidden" runat="server" />
            </td>
        </tr>
        <tr id="Row1" style="display: none; height: 20px">
            <td style="width: 77%">
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblCol1" runat="server" Text="100 ml" ForeColor="Black"></asp:Label>
            </td>
            <td align="center">
                <asp:TextBox ID="txtValor1" runat="server" Height="16px" CssClass="txtFormatSimple"
                    onkeydown="return ValidarNumerico();" onMousemove="CambiarCursor();" OnClick="SubirCantidad();"
                    Width="60px" MaxLength="4" ToolTip="Valor Unitario:" ></asp:TextBox>
                <input id="IdPres1" type="hidden" runat="server" />
            </td>
        </tr>
        <tr id="Row2" style="display: none; height: 20px">
            <td style="width: 77%">
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblCol2" runat="server" Text="Plum" ForeColor="Black"></asp:Label>
            </td>
            <td align="center">
                <asp:TextBox ID="txtValor2" runat="server" Height="16px" CssClass="txtFormatSimple"
                    onkeydown="return ValidarNumerico();" onMousemove="CambiarCursor();" OnClick="SubirCantidad();"
                    Width="60px" MaxLength="4"></asp:TextBox>
                <input id="IdPres2" type="hidden" runat="server" />
            </td>
        </tr>
        <tr id="Row3" style="display: none; height: 20px">
            <td style="width: 77%">
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblCol3" runat="server" Text="Plum" ForeColor="Black"></asp:Label>
            </td>
            <td align="center">
                <asp:TextBox ID="txtValor3" runat="server" Height="16px" CssClass="txtFormatSimple"
                    onkeydown="return ValidarNumerico();" onMousemove="CambiarCursor();" OnClick="SubirCantidad();"
                    Width="60px" MaxLength="4"  ></asp:TextBox>
                <input id="IdPres3" type="hidden" runat="server" />
            </td>
        </tr>
        <tr id="Row4" style="display: none; height: 20px">
            <td style="width: 77%">
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblCol4" runat="server" Text="Plum" ForeColor="Black"></asp:Label>
            </td>
            <td align="center">
                <asp:TextBox ID="txtValor4" runat="server" Height="16px" CssClass="txtFormatSimple"
                    onkeydown="return ValidarNumerico();" onMousemove="CambiarCursor();" OnClick="SubirCantidad();"
                    Width="60px" MaxLength="4"></asp:TextBox>
                <input id="IdPres4" type="hidden" runat="server" />
            </td>
        </tr>
        <tr id="Row5" style="display: none; height: 20px">
            <td style="width: 77%">
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblCol5" runat="server" Text="Plum" ForeColor="Black"></asp:Label>
            </td>
            <td align="center">
                <asp:TextBox ID="txtValor5" runat="server" Height="16px" CssClass="txtFormatSimple"
                    onkeydown="return ValidarNumerico();" onMousemove="CambiarCursor();" OnClick="SubirCantidad();"
                    Width="60px" MaxLength="4"></asp:TextBox>
                <input id="IdPres5" type="hidden" runat="server" />
            </td>
        </tr>
        <tr id="Row6" style="display: none; height: 20px">
            <td style="width: 77%">
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblCol6" runat="server" Text="Plum" ForeColor="Black"></asp:Label>
            </td>
            <td align="center">
                <asp:TextBox ID="txtValor6" runat="server" Height="16px" CssClass="txtFormatSimple"
                    onkeydown="return ValidarNumerico();" onMousemove="CambiarCursor();" OnClick="SubirCantidad();"
                    Width="60px" MaxLength="4"></asp:TextBox>
                <input id="IdPres6" type="hidden" runat="server" />
            </td>
        </tr>
        <tr id="Row7" style="display: none; height: 20px">
            <td style="width: 77%">
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblCol7" runat="server" Text="Plum" ForeColor="Black"></asp:Label>
            </td>
            <td align="center">
                <asp:TextBox ID="txtValor7" runat="server" Height="16px" CssClass="txtFormatSimple"
                    onkeydown="return ValidarNumerico();" onMousemove="CambiarCursor();" OnClick="SubirCantidad();"
                    Width="60px" MaxLength="4"></asp:TextBox>
                <input id="IdPres7" type="hidden" runat="server" />
            </td>
        </tr>
        <tr id="Row8" style="display: none; height: 20px">
            <td style="width: 77%">
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblCol8" runat="server" Text="Plum" ForeColor="Black"></asp:Label>
            </td>
            <td align="center">
                <asp:TextBox ID="txtValor8" runat="server" Height="16px" CssClass="txtFormatSimple"
                    onkeydown="return ValidarNumerico();" onMousemove="CambiarCursor();" OnClick="SubirCantidad();"
                    Width="60px" MaxLength="4"></asp:TextBox>
                <input id="IdPres8" type="hidden" runat="server" />
            </td>
        </tr>
        <tr id="Row9" style="display: none; height: 20px">
            <td style="width: 77%">
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblCol9" runat="server" Text="Plum" ForeColor="Black"></asp:Label>
            </td>
            <td align="center">
                <asp:TextBox ID="txtValor9" runat="server" Height="16px" CssClass="txtFormatSimple"
                    onkeydown="return ValidarNumerico();" onMousemove="CambiarCursor();" OnClick="SubirCantidad();"
                    Width="60px" MaxLength="4"></asp:TextBox>
                <input id="IdPres9" type="hidden" runat="server" />
            </td>
        </tr>
        <tr style="height: 35px">
            <td style="width: 77%; height: 35px;background-color:Transparent" >
                &nbsp;
            </td>
            <td colspan="2" align="center" style="background-color:Transparent">
                <asp:Button SkinID="btnGray"  ID="btnSolicitar" runat="server"  OnClientClick="OK_Clicked(); return false;" />
            </td>
        </tr>
    </table>
    <div class="processMessageSolicitud" id="DivSolicitud" style="display: none" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="height: 290px; width: 100%">
            <tr>
                <td align="center" style="height: 40px" valign="bottom">
                    <img alt="a" src="Imagenes/waiting.gif" />
                </td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    <asp:Label ID="lbltituloCarga" runat="server" Font-Bold="True" Font-Names="Thomas"
                        Font-Size="12px" ForeColor="white" Height="21px" Style="vertical-align: middle"
                        Text="Enviado Pedido......">
                    </asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
