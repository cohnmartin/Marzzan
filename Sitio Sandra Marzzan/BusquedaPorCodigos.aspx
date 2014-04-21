<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BusquedaPorCodigos.aspx.cs"
    Theme="SkinMarzzan" Inherits="BusquedaPorCodigos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />

    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>

</head>

<script type="text/javascript">

    var xmlDoc = null;
    var xmlDocF = null;
    var descProductoSel = "";
    function searchIndex(searchText, tbod) { // search the index (duh!)


        if (!xmlDoc) {
            xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
            xmlDoc.async = "false";
            xmlDoc.load("DetalleProductos.xml");

            xmlDocF = new ActiveXObject("Microsoft.XMLDOM");
            xmlDocF.async = "false";
            xmlDocF.load("ProductosPlanos.xml");

        }

        var allitems = xmlDoc.getElementsByTagName("Unidad");
        var allitemsF = xmlDocF.getElementsByTagName("Fragancia");

        results = new Array;
        resultsName = new Array;
        resultsMedida = new Array;
        resultsCodigos = new Array;
        if (searchText.length < 2) {

            IniciarBusqueda();


        } else {

            var rowsCount = tbod.rows.length - 1;
            for (var i = rowsCount; i >= 0; i--) {
                tbod.deleteRow(i);
            }

            var codigos = "";
            var descripciones = "";
            var nombreCodigo = ""
            var nombreDescripcion = ""
            if (searchText.length <= 2) {
                nombreCodigo = "CodigoL"
                nombreDescripcion = "NombreL"
            }
            else if (searchText.length > 2 && searchText.length < 4) {
                nombreCodigo = "CodigoL"
                nombreDescripcion = "NombreL"
            }
            else {
                nombreCodigo = "CodigoF"
                nombreDescripcion = "NombreF"

            }

            if (searchText.length < 4) {
                for (var i = 0; i < allitems.length; i++) {

                    codigos += allitems[i].selectNodes(nombreCodigo)[0].text + "|";

                    descripciones += allitems[i].selectNodes(nombreDescripcion)[0].text + "|"; ;

                }


                if (codigos != null) {
                    var exp = new RegExp("^" + searchText, "i");
                    var codigosSimple = codigos.split('|');
                    var DescripcionSimple = descripciones.split('|');


                    for (var j = 0; j < codigosSimple.length; j++) {
                        if (codigosSimple[j] != null && codigosSimple[j].match(exp) != null) {

                            var row = tbod.insertRow(0);
                            var cell = row.insertCell(0);
                            cell.align = "left";
                            cell.innerHTML = "<b><span style='color:Blue' >" + codigosSimple[j].substring(0, codigosSimple[j].indexOf(searchText) + searchText.length) + "</span></b>" +
                            "<b><span style='font-weight:bold;font-size: 13px' >" + codigosSimple[j].substring(codigosSimple[j].indexOf(searchText) + searchText.length) + "</span></b>" + " - " + DescripcionSimple[j];
                        }
                    }
                }
            }
            else {
                var presentaciones = "";
                for (var i = 0; i < allitemsF.length; i++) {

                    codigos += allitemsF[i].selectNodes(nombreDescripcion)[0].getAttribute("CodigoBusqueda") + "|";

                    descripciones += allitemsF[i].selectNodes(nombreDescripcion)[0].text + "|"; ;

                    presentaciones += allitemsF[i].selectNodes(nombreDescripcion)[0].getAttribute("Presentaciones") + "|";

                }


                if (codigos != null) {
                    var exp = new RegExp("^" + searchText);
                    var exp1 = new RegExp("@" + searchText);

                    var codigosSimple = codigos.split('|');
                    var DescripcionSimple = descripciones.split('|');
                    var presentacionSimple = presentaciones.split('|');

                    for (var j = 0; j < codigosSimple.length; j++) {
                        if (codigosSimple[j] != null && (codigosSimple[j].match(exp) != null || codigosSimple[j].match(exp1) != null)) {

                            if (codigosSimple[j].indexOf("@") < 0) {
                                var row = tbod.insertRow(0);
                                var cell = row.insertCell(0);
                                cell.align = "left";
                                cell.innerHTML = "<b><span style='color:Blue' >" + codigosSimple[j].substring(0, codigosSimple[j].indexOf(searchText) + searchText.length) + "</span></b>" +
                                "<b><span style='font-weight:bold;font-size: 13px' >" + codigosSimple[j].substring(codigosSimple[j].indexOf(searchText) + searchText.length) + "</span></b>"
                                + " - " + DescripcionSimple[j] + " x " + presentacionSimple[j];
                                
                                cell.setAttribute("NombreProducto", DescripcionSimple[j] + " x " + presentacionSimple[j]);
                                cell.setAttribute("CodigoProducto", codigosSimple[j]);
                            }
                            else {

                                var codigosUnitario = codigosSimple[j].split('@');
                                var DescripcionUnitario = DescripcionSimple[j];
                                var PresentacionUnitario = presentacionSimple[j].split('@');

                                // Si encuentro una coincidencia exacta coloco solo esa y salgo
                                for (var cu = 0; cu < codigosUnitario.length; cu++) {
                                    if (codigosUnitario[cu] == searchText) {
                                        var row = tbod.insertRow(0);
                                        var cell = row.insertCell(0);
                                        cell.align = "left";
                                        cell.innerHTML = "<b><span style='color:Blue' >" + codigosUnitario[cu].substring(0, codigosUnitario[cu].indexOf(searchText) + searchText.length) + "</span></b>" +
                                                     "<b><span style='font-weight:bold;font-size: 13px' >" + codigosUnitario[cu].substring(codigosUnitario[cu].indexOf(searchText) + searchText.length) + "</span></b>"
                                                     + " - " + DescripcionUnitario + " x " + PresentacionUnitario[cu];

                                        cell.setAttribute("NombreProducto", DescripcionUnitario + " x " + PresentacionUnitario[cu]);
                                        cell.setAttribute("CodigoProducto", codigosUnitario[cu]);
                                        return;
                                    }

                                }


                                for (var cu = 0; cu < codigosUnitario.length; cu++) {

                                    var row = tbod.insertRow(0);
                                    var cell = row.insertCell(0);
                                    cell.align = "left";
                                    cell.innerHTML = "<b><span style='color:Blue' >" + codigosUnitario[cu].substring(0, codigosUnitario[cu].indexOf(searchText) + searchText.length) + "</span></b>" +
                                                     "<b><span style='font-weight:bold;font-size: 13px' >" + codigosUnitario[cu].substring(codigosUnitario[cu].indexOf(searchText) + searchText.length) + "</span></b>" + " - " + DescripcionUnitario + " x " + PresentacionUnitario[cu];

                                    cell.setAttribute("NombreProducto", DescripcionUnitario + " x " + PresentacionUnitario[cu]);
                                    cell.setAttribute("CodigoProducto", codigosUnitario[cu]);
                                }


                            }


                        }
                    }
                }

            }

        }


    }


    window.onload = IniciarBusqueda;

    function IniciarBusqueda() {

        if (!xmlDoc) {
            xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
            xmlDoc.async = "false";
            xmlDoc.load("DetalleProductos.xml");

            xmlDocF = new ActiveXObject("Microsoft.XMLDOM");
            xmlDocF.async = "false";
            xmlDocF.load("ProductosPlanos.xml");

        }

        document.getElementById('divInicial').style.display = "none";
        var tbod = document.getElementById('tblResult');
        var allitems = xmlDoc.getElementsByTagName("Unidad");
        results = new Array;
        resultsName = new Array;
        resultsMedida = new Array;
        resultsCodigos = new Array;


        var codigos = "";
        var descripciones = "";
        var nombreCodigo = "Codigo"
        var nombreDescripcion = "Nombre"


        for (var i = 0; i < allitems.length; i++) {

            if (codigos.indexOf(allitems[i].selectNodes(nombreCodigo)[0].text) < 0)
                codigos += allitems[i].selectNodes(nombreCodigo)[0].text + "|";

            if (descripciones.indexOf(allitems[i].selectNodes(nombreDescripcion)[0].text) < 0)
                descripciones += allitems[i].selectNodes(nombreDescripcion)[0].text + "|"; ;

        }

        var rowsCount = tbod.rows.length - 1;
        for (var i = rowsCount; i >= 0; i--) {
            tbod.deleteRow(i);
        }


        if (codigos != null) {

            var codigosSimple = codigos.split('|');
            var DescripcionSimple = descripciones.split('|');

            for (var j = 0; j < codigosSimple.length - 1; j++) {

                var row = tbod.insertRow(j);
                var cell = row.insertCell(0);
                cell.align = "left";
                cell.innerHTML = codigosSimple[j] + " - " + DescripcionSimple[j];

            }
        }




    }

    $(document).ready(function() {
        $("#divBusqueda").keyup(
        function(event) {


            var tbod = document.getElementById('tblResult');

            if (event.keyCode == 13) {
                var rowsCount = tbod.rows.length - 1;
                if (rowsCount == 0) {

                    var descProducto = tbod.rows[0].cells[0].getAttribute("NombreProducto");
                    $find("txtNombre").set_value(descProducto);

                    var codigoProducto = tbod.rows[0].cells[0].getAttribute("CodigoProducto");
                    $find("txtCodigo").set_value(codigoProducto);

                    $find("txtCantidad").focus();
                    $find("ToolTipBusqueda").hide();

                    for (var i = rowsCount; i >= 0; i--) {
                        tbod.deleteRow(i);
                    }
                    document.getElementById('divInicial').style.display = "block";
                    return false;
                }
                else {
                    return false;

                }
            }
            else {

                document.getElementById('divInicial').style.display = "none";
                //searchIndex($find("txtCodigo").get_value() + arg.get_keyCharacter(), tbod);
                searchIndex($find("txtCodigo").get_value(), tbod);
                 
                return true;
            }

        });
    }); 
    
   
       
</script>

<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <telerik:RadToolTip runat="server" ID="ToolTipBusqueda" Skin="Office2007" ShowCallout="true"
        ShowEvent="OnFocus" RelativeTo="Element" Position="BottomRight" Width="400" ManualClose="true"
        Title="" Animation="Fade" TargetControlID="txtCodigo">
        <div id="divInicial">
            Ingrese el código del producto a buscar..</div>
        <table width="100%" cellpadding="0" cellspacing="0" id="tblResult">
        </table>
    </telerik:RadToolTip>
    
    <table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 0px; padding-bottom: 20px">
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
        <tr>
            <td align="center">
                <table cellpadding="0" cellspacing="0" width="80%" style="border-top: 1px solid black">
                    <tr style="padding-bottom: 10px; padding-top: 5px">
                        <td style="width: 30%; padding-right: 5px; color: #0066CC; font-family: Sans-Serif;
                            font-size: 13px" align="right">
                            Ingrese Nro Producto:
                        </td>
                        <td>
                            <div id="divBusqueda">
                                <telerik:RadTextBox ID="txtCodigo" runat="server">
                                </telerik:RadTextBox>
                            </div>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtNombre" Width="250px" ReadOnly="true" runat="server">
                            </telerik:RadTextBox>
                        </td>
                        <td style="width: 30%; padding-right: 5px; color: #0066CC; font-family: Sans-Serif;
                            font-size: 13px" class="tdSimple" align="right">
                            Cantidad:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtCantidad" runat="server" SelectionOnFocus="SelectAll">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
