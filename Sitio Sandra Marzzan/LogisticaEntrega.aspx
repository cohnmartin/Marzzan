<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogisticaEntrega.aspx.cs"
    Inherits="LogisticaEntrega" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Logística de Entrega</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" src="ValidacionesCliente.js"></script>

    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>

    <script src="Scripts/jquery.corners.js" type="text/javascript"></script>

    <style type="text/css">
        .Redondo
        {
            background: url(ImgLogistica/intro2.png) 0 -10px repeat-x;
        }
        .style1
        {
            width: 100%;
        }
        .TituloDatosProv
        {
            font-family: Tahoma;
            font-size: 12px;
            font-weight: bold;
            padding-left: 15px;
        }
        .DescripcionDatosProv
        {
            font-family: Tahoma;
            font-size: 11px;
        }
        .Titulolbl
        {
            font-family: Tahoma;
            font-size: 13px;
            font-weight: bold;
        }
        .Descripcionlbl
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        .TrasporteTD
        {
            padding-left: 15px;
            background-color: #ACD1E6;
        }
        .SucursalTDPlus
        {
            padding-left: 30px;
            cursor: hand;
            background-position: 10px 8px;
            background-image: url('imagenes/SinglePlus.gif');
            background-repeat: no-repeat;
        }
        .SucursalTDMinus
        {
            padding-left: 30px;
            cursor: hand;
            background-position: 10px 8px;
            background-image: url(imagenes/SingleMinus.gif);
            background-repeat: no-repeat;
        }
        .TituloTD
        {
            padding-left: 50px;
            font-family: Tahoma;
            font-size: 13px;
            font-weight: bold;
        }
        .DescripcionTD
        {
            padding-left: 70px;
            font-family: Tahoma;
            font-size: 13px;
        }
        .DatosProvincia
        {
            padding-left: 65px;
            padding-right: 65px;
            padding-top: 8px;
            padding-bottom: 8px;
            background-color: #0099CC;
            color: #000000;
        }
    </style>

</head>
<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: #FFFFFF;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
        <tr style="height: 116px">
            <td>
                <div class="Header_panelContainerSimple">
                    <div class="CabeceraInicial">
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 435px">
                <div id="special" style="cursor: crosshair">
                    <asp:ImageMap ID="ImageMap1" runat="server" ImageUrl="~/ImgLogistica/mapa_argentina.png">
                        <asp:PolygonHotSpot AlternateText="Provincia de Chubut - Click para ver los datos de transporte"
                            Coordinates="107, 366,179, 367,178, 397,155, 414,152, 426,113, 426,119, 405,111, 395,104, 366" />
                        <asp:PolygonHotSpot AlternateText="Provincia de Jujuy - Click para ver los datos de transporte"
                            Coordinates="178, 38,197, 61,187, 70,160, 51" />
                        <asp:PolygonHotSpot AlternateText="Provincia de Salta - Click para ver los datos de transporte"
                            Coordinates="206, 38,225, 43,215, 90,166, 89,167, 71,203, 71" />
                        <asp:PolygonHotSpot AlternateText="Provincia de Formosa - Click para ver los datos de transporte"
                            Coordinates="231, 45,272, 74,297, 92,286, 110,232, 70" />
                        <asp:PolygonHotSpot AlternateText="Provincia de Chaco - Click para ver los datos de transporte"
                            Coordinates="243, 92,242, 128,274, 128,277, 114,233, 74" />
                        <asp:PolygonHotSpot AlternateText="Provincia de Corrientes - Click para ver los datos de transporte"
                            Coordinates="269, 167,285, 166,293, 164,319, 140,318, 136,305, 134,293, 131,285, 128,281, 130,279, 144,275, 150,272, 155,271, 159,270, 164" />
                        <asp:PolygonHotSpot AlternateText="Provincia de Misiones - Click para ver los datos de transporte"
                            Coordinates="348, 104,355, 106,349, 126,330, 134,333, 127,347, 112" />
                        <asp:PolygonHotSpot AlternateText="Provincia de Santiago del Estero - Click para ver los datos de transporte"
                            Coordinates="202, 97,236, 96,234, 133,228, 164,195, 150,191, 128" />
                        <asp:PolygonHotSpot AlternateText="Provincia de Tucumán - Click para ver los datos de transporte"
                            Coordinates="175, 107,179, 104,185, 102,188, 102,189, 109,187, 115,186, 124,181, 126,178, 126,174, 119,173, 116,176, 111,175, 106" />
                        <asp:PolygonHotSpot AlternateText="Provincia de Catamarca - Click para ver los datos de transporte"
                            Coordinates="141, 90,141, 126,165, 137,182, 148,157, 92,140, 89" />
                        <asp:PolygonHotSpot AlternateText="Provincia de Entre Rios - Click para ver los datos de transporte"
                            Coordinates="266, 174,287, 178,276, 228,249, 206" />
                        <asp:PolygonHotSpot AlternateText="Provincia de Santa Fé - Click para ver los datos de transporte"
                            Coordinates="242, 135,272, 135,242, 225,228, 224,240, 146" />
                        <asp:PolygonHotSpot AlternateText="Provincia de San Juan - Click para ver los datos de transporte"
                            Coordinates="119, 162,136, 165,151, 186,149, 197,117, 194" />
                        <asp:PolygonHotSpot AlternateText="Provincia de San Luis - Click para ver los datos de transporte"
                            Coordinates="158, 200,184, 203,184, 259,169, 259,167, 238" />
                        <asp:PolygonHotSpot AlternateText="Provincia de Mendoza - Click para ver los datos de transporte"
                            Coordinates="123, 209,150, 209,162, 259,141, 259,141, 280,116, 266" />
                        <asp:PolygonHotSpot AlternateText="Provincia de La Pampa. - Click para ver los datos de transporte"
                            Coordinates="149, 266,208, 265,207, 308,150, 286" />
                        <asp:PolygonHotSpot AlternateText="Provincia de Bs. As. - Click para ver los datos de transporte"
                            Coordinates="212, 240,239, 238,255, 227,286, 256,282, 299,215, 299" />
                        <asp:PolygonHotSpot AlternateText="Provincia de Neuquén - Click para ver los datos de transporte"
                            Coordinates="112, 275,123, 285,142, 291,140, 311,106, 342" />
                        <asp:PolygonHotSpot AlternateText="Provincia de Rio Negro - Click para ver los datos de transporte"
                            Coordinates="147, 302,205, 319,204, 339,178, 335,178, 357,108, 357,114, 346" />
                        <asp:PolygonHotSpot AlternateText="Provincia de Santa Cruz - Click para ver los datos de transporte"
                            Coordinates="113, 433,152, 435,162, 461,138, 489,137, 519,114, 517,110, 502,101, 485" />
                        <asp:PolygonHotSpot AlternateText="Provincia de Tierra del Fuego - Click para ver los datos de transporte"
                            Coordinates="155, 549,175, 570,153, 568" />
                        <asp:PolygonHotSpot AlternateText="Provincia de Córdoba - Click para ver los datos de transporte"
                            Coordinates="193, 161,228, 178,225, 212,205, 237,190, 235,192, 199,181, 186" />
                        <asp:PolygonHotSpot AlternateText="Provincia de La Rioja - Click para ver los datos de transporte"
                            Coordinates="128, 129,175, 163,174, 189,157, 187,156, 174,126, 141" />
                    </asp:ImageMap>
                </div>
                <div id="divImage" style="position: absolute; left: 0px; top: 0px; display: none;
                    width: 120px; height: 131px; background-image: url('ImgLogistica/Provincias.png');
                    background-position: 0 0px; z-index: 800000">
                    &nbsp;
                </div>
            </td>
            <td style="padding-top: 40px">
                <div id="DivDatos" style="display: none; position: absolute; left: 450px; top: 200px;
                    background-color: #ffffff;" onmouseover="OcultarImagen();">
                    <table id="tblDatos" width="500px" cellpadding="0" cellspacing="0" border="0" style="border: 1px solid #0033CC;">
                        <thead>
                            <tr>
                                <td style="width: 16px; background-color: #0099CC; color: #FFFFFF; padding: 3px;">
                                    &nbsp;
                                </td>
                                <td align="center" style="background-color: #0099CC; color: #FFFFFF; padding: 8px;">
                                    Transportistas de la provincia de
                                    <asp:Label ID="lblProvincia" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="padding: 3px; background-color: #0099CC; color: #FFFFFF;">
                                    <img style="cursor: hand" src="ImgLogistica/Cancelar.gif" id="imgCerrar" onclick="HideImage();return false;" />
                                </td>
                            </tr>
                        </thead>
                        <tbody id="Content">
                        </tbody>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" style="position: absolute; top: 10px; left: 20px;display:none;
        border: 1px solid black; background-color: #FFFFFF; color: #000000; font-size: 8px">
        <tr>
            <td style="width: 750px">
                <h4 id="status2">
                </h4>
            </td>
        </tr>
    </table>
    

    <script type="text/javascript">

        var xml = undefined;
        var points = new Array();
        var polyX = new Array();
        var polyY = new Array();

        $(document).ready(function() {
            //espero que cargue el DOM y aplico el plugin a todos los elemento con clase rounded
            $('.Redondo').corners("5px transparent");
        });

        $.ajax({
            type: 'GET',
            url: 'ImgLogistica/logistica.xml',
            cache: false,
            dataType: ($.browser.msie) ? 'text' : 'xml', // Reconocemos el browser.
            success: CargarDatos
        });

        function OcultarImagen() {

            $('#divImage').fadeOut(100);
            $('#divImage').unbind('mousemove');




        }


        function CargarDatos(data) {

            if (xml == undefined) {
                if (typeof data == 'string') {
                    xml = new ActiveXObject('Microsoft.XMLDOM');
                    xml.async = false;
                    xml.loadXML(data);
                } else {
                    xml = data;
                }
            }


        }

        function GenerarTablaInformacion(Provincia, Id) {

            tbod = document.getElementById('Content');
            var rowsCount = tbod.rows.length - 1;
            for (var i = rowsCount; i >= 0; i--) {
                tbod.deleteRow(i);
            }

            document.getElementById('lblProvincia').innerText = Provincia;

            $(xml).find('Provincia').each(function() {

                if ($(this).attr('Id') == Id) {

                    $(this).find('DatosProvincia').each(function() {
                        var totalReg = $(this).find('Item').length - 1;
                        var i = 0;
                        var strDatos = "";
                        $(this).find('Item').each(function() {
                            var titulo = $(this).find('Titulo').text();
                            var descripcion = $(this).find('Descripcion').text();

                            strDatos += "<span class='TituloDatosProv'>" + titulo + ":</span> <span class='DescripcionDatosProv'>" + descripcion + "</span></br>";

                        });

                        var row = tbod.insertRow(tbod.rows.length);
                        var cell1 = row.insertCell(0);
                        cell1.className = "DatosProvincia";
                        cell1.colSpan = "3";
                        cell1.innerHTML = " <div  class='Redondo' >" + strDatos + "</div>";
                        i++;

                    });


                    $(this).find('Transporte').each(function() {
                        var trans = $(this).attr('Name');
                        var transId = $(this).attr('Id');

                        //---------- Creo las Filas del Transporte --------------------
                        var row = tbod.insertRow(tbod.rows.length);
                        var cell1 = row.insertCell(0);
                        cell1.className = "TrasporteTD";
                        cell1.colSpan = "3";
                        cell1.innerHTML = "<span class='Titulolbl'>Trasporte:</span> <span class='Descripcionlbl'>" + trans + "</span>";

                        $(this).find('Sucursal').each(function() {
                            var suc = $(this).attr('Name');
                            var sucId = $(this).attr('Id');

                            //---------- Creo las Filas de las Sucursales --------------------
                            var row = tbod.insertRow(tbod.rows.length);
                            var cell1 = row.insertCell(0);
                            cell1.className = "SucursalTDPlus";
                            cell1.colSpan = "3";
                            cell1.innerHTML = "<span class='Titulolbl'>Sucursal:</span> <span class='Descripcionlbl'>" + suc + "</span>";

                            $(cell1).click(function(e) {
                                if ($('#' + transId + sucId)[0].style.display == "block") {
                                    $('#' + transId + sucId).slideUp(500);
                                    this.className = "SucursalTDPlus";

                                }
                                else {
                                    $('#' + transId + sucId).slideDown(500);
                                    this.className = "SucursalTDMinus";

                                }
                            });

                            var divContenedor = document.createElement('Div');
                            divContenedor.id = transId + sucId;
                            divContenedor.style.display = "none";


                            var tblDatos = document.createElement('table');
                            var tblContainerTbody = document.createElement("tbody");
                            tblDatos.setAttribute("id", "tblDatos" + suc);
                            tblContainerTbody.setAttribute("id", "tblDatos" + suc);



                            var row = tbod.insertRow(tbod.rows.length);
                            var cell1 = row.insertCell(0);
                            cell1.colSpan = "3";
                            cell1.appendChild(divContenedor);




                            $(this).find('Item').each(function() {
                                var titulo = $(this).find('Titulo').text();
                                var descripcion = $(this).find('Descripcion').text();


                                var row = document.createElement('tr');
                                tblContainerTbody.appendChild(row);
                                var TDtitulo = document.createElement('td');
                                TDtitulo.className = "TituloTD";
                                TDtitulo.colSpan = "3";
                                TDtitulo.appendChild(document.createTextNode(titulo));
                                row.appendChild(TDtitulo);


                                var row = document.createElement('tr');
                                tblContainerTbody.appendChild(row);
                                var TDdescripcion = document.createElement('td');
                                TDdescripcion.className = "DescripcionTD";
                                TDdescripcion.colSpan = "3";
                                TDdescripcion.innerText = descripcion.replace(new RegExp("^[\\n]+", "g"), "").replace(/\s*$/, "");
                                row.appendChild(TDdescripcion);


                            });


                            tblDatos.appendChild(tblContainerTbody);
                            divContenedor.appendChild(tblDatos);

                        });


                    });




                }
            });

            var row = tbod.insertRow(tbod.rows.length);
            var cell1 = row.insertCell(0);
            cell1.className = "TituloTD";
            cell1.colSpan = "3";
            cell1.innerHTML = " <br />";

            $('.Redondo').corners("8px transparent");

        }

        jQuery(document).ready(function() {
            $("#special").click(function(e) {

                var x = event.offsetX + 3;
                var y = event.offsetY + 3;


                $('#status2').html($('#status2').html() + ',' + x + ', ' + y);
            });

        })



        //<![CDATA[
        var IdsProv = new Array("Chubut", "Jujuy", "Salta", "Formosa", "Chaco", "Corrientes", "Misiones", "SantiagoDelEstero", "Tucumán", "Catamarca", "EntreRios", "SantaFé", "SanJuan", "SanLuis", "Mendoza", "LaPampa", "BsAs", "Neuquén", "RíoNegro", "SantaCruz", "TierraDelFuego", "Córdoba", "LaRioja");
        var nombresProv = new Array("Chubut", "Jujuy", "Salta", "Formosa", "Chaco", "Corrientes", "Misiones", "Santiago del Estero", "Tucumán", "Catamarca", "Entre Ríos", "Santa Fé", "San Juan", "San Luis", "Mendoza", "La Pampa", "Bs. As.", "Neuquén", "Río Negro", "Santa Cruz", "Tierra del Fuego", "Córdoba", "La Rioja");
        var countryArray = new Array("0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22");
        var widthArray = new Array("147", "187", "191", "267", "258", "295", "349", "220", "185", "162", "273", "252", "138", "184", "147", "183", "262", "132", "165", "145", "173", "213", "158");
        var heigthArray = new Array("513", "165", "187", "197", "219", "262", "236", "242", "227", "237", "316", "296", "292", "340", "355", "394", "398", "422", "438", "588", "672", "314", "275");
        var coordenadas = "";




        //IMPORTANT!: On the client the ASP.NET framework prepends the actual map element with the prefix "ImageMap"
        //var map = document.getElementsByName("<%= ImageMap1.ClientID %>")[0];
        var areas = document.getElementsByTagName("area");
        for (var i = 0; i < areas.length; i++) {
            var area = areas[i];
            area.setAttribute("pos", countryArray[i]);
            area.setAttribute("X", widthArray[i]);
            area.setAttribute("Y", heigthArray[i]);
            area.setAttribute("Nombre", nombresProv[i]);
            area.setAttribute("Id", IdsProv[i]);


            //Prevent from postbacking the page
            //area.onclick = function(e) { ShowPoint(); return false; }
            area.onclick = function(e) { return false; }
            area.onmouseover = ShowImage;


            area.id = IdsProv[i];

            //remove the alt attribute to prevent the browser's tooltip from showing
            area.removeAttribute("alt");
        }
        //]]>

        function ShowPoint() {

            //coordenadas += ',' + (event.clientX) + ',' + event.clientY;
            //$('#status2').html((event.clientX) + ',' + event.clientY);

        }

        function HideImage() {
            $('#divImage').fadeOut(300);
            $('#DivDatos').fadeOut(300);
        }

        function ShowImage() {


            var width = parseInt(this.getAttribute("X"));
            var heigth = parseInt(this.getAttribute("Y"));
            var Pos = parseInt(this.getAttribute("pos"));
            var Nombre = this.getAttribute("Nombre");
            var Id = this.getAttribute("Id");

            $('#divImage').css('left', (width - 55) + 'px');
            $('#divImage').css('top', (heigth - 60) + 'px');
            $('#divImage').css('background-position', '0 -' + 130 * Pos + 'px');

            $('#divImage').unbind("mousemove");
            $('#divImage').fadeIn(200
            , function() {

                polyX = new Array();
                polyY = new Array();
                var coord = $('#' + Id)[0].coords.split(',');
                for (i = 0; i < coord.length; i++) {
                    points.push({ x: coord[i], y: coord[i + 1] });
                    polyX.push(coord[i]);
                    polyY.push(coord[i + 1]);
                    i++;
                }

                $('#divImage').mousemove(function(e) {
                    CalcularSaleImagen(e);
                });

            });

            //CargarDatos();

            $('#divImage').click(function() {

//                $('#divImage').hide();

//                $('#divImgAncla').css('left', (width - 55) + 'px');
//                $('#divImgAncla').css('top', (heigth - 60) + 'px');
//                $('#divImgAncla').css('background-position', '0 -' + 130 * Pos + 'px');
//                $('#divImgAncla').show();


                GenerarTablaInformacion(Nombre, Id);

                $('#DivDatos').fadeIn(400);

            });

        }

        var stateCount = 0;
        function CalcularSaleImagen(e) {

            if (points.length > 0) {

                var x = e.clientX;
                var y = e.clientY;

                var P1x = x - 15;
                var P1y = y - 120;
                var total = 0;

                for (i = 0; i < polyX.length; i++) {

                    if (i < polyX.length - 1) {
                        total += Angulo(P1x, P1y, polyX[i], polyY[i], polyX[i + 1], polyY[i + 1]);
                    }
                    else {
                        total += Angulo(P1x, P1y, polyX[i], polyY[i], polyX[0], polyY[0]);
                    }
                }

                //$('#status2').html(total + '-' + polyX.length);
                if (Math.round(total, 1) < 345 || Math.round(total, 1) > 380) {
                    $('#divImage').fadeOut(200);
                    $('#divImage').unbind('mousemove');


                }

            }
        }


        function Angulo(P1x, P1y, P2x, P2y, P3x, P3y) {

            var P12 = Math.sqrt(Math.pow(P1x - P2x, 2) + Math.pow(P1y - P2y, 2));
            var P13 = Math.sqrt(Math.pow(P1x - P3x, 2) + Math.pow(P1y - P3y, 2));
            var P23 = Math.sqrt(Math.pow(P2x - P3x, 2) + Math.pow(P2y - P3y, 2));

            //return Math.acos((Math.pow(P23, 2) - Math.pow(P12, 2) - Math.pow(P13, 2)) / (-2 * P12 * P13));
            return Math.acos((Math.pow(P23, 2) - Math.pow(P12, 2) - Math.pow(P13, 2)) / (-2 * P12 * P13)) * 180 / Math.PI;

        }

        function pointInPolygon(polyX, polyY, x, y) {
            var polySides = 4;
            var i, j = polySides - 1
            var oddNodes = false;

            for (i = 0; i < polySides; i++) {
                if (polyY[i] < y && polyY[j] >= y || polyY[j] < y && polyY[i] >= y) {
                    if (x < (polyX[j] - polyX[i]) * (y - polyY[i] / (polyY[j] - polyY[i]) + polyX[i])) {
                        return !oddNodes;
                    }
                }
                j = i;
            }

            return oddNodes;
        }


        function isPointInPoly(poly, pt) {

            for (var c = false, i = -1, l = poly.length, j = l - 1; ++i < l; j = i)
                ((poly[i].y <= pt.y && pt.y < poly[j].y) || (poly[j].y <= pt.y && pt.y < poly[i].y))
            		&& (pt.x < (poly[j].x - poly[i].x) * (pt.y - poly[i].y) / (poly[j].y - poly[i].y) + poly[i].x)
		            && (c = !c);
            return c;
        }
        
    </script>

    </form>
</body>
</html>
