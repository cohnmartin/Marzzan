<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logistica2.aspx.cs" Inherits="Logistica2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    
    <link href="ImgLogistica/tooltip.css" rel="stylesheet" type="text/css" />
     
    <script type="text/javascript" src="ValidacionesCliente.js"></script>

    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>

    <script src="Scripts/jquery.corners.js" type="text/javascript"></script>

    <script src="ImgLogistica/cvi_map_lib.js" type="text/javascript"></script>

    <script src="ImgLogistica/cvi_tip_lib.js" type="text/javascript"></script>

    <script src="ImgLogistica/mapper.js" type="text/javascript"></script>

    <script src="ImgLogistica/maputil.js" type="text/javascript"></script>

 <script type="text/javascript">	
	<!--
     function showCoords(map, area, x, y, w, h) {
         function parseDMS(v, n) { var d, m, s; d = parseInt(v); m = Math.abs(parseFloat(v - d) * 60); s = Math.abs(parseFloat(parseInt(m) - m) * 60); return Math.abs(d) + "° " + parseInt(m) + "' " + parseInt(s) + "'' " + n; }
         if (map == "map_of_world") {
             var obj, country = "", lon = (x * 360 / w) - 180, lat = 90 - (y * 180 / h);
             lon = parseDMS(lon, lon != 0 ? (lon < 0 ? "W" : "E") : ""); lat = parseDMS(lat, lat != 0 ? (lat < 0 ? "S" : "N") : "");
             if (area != 0) { obj = document.getElementById(area); country = "  (" + (obj.title || obj.alt) + ")"; }
             //document.getElementById("map_of_world_blind").innerHTML = "<p class='coords'>Latitude: " + lat + "  Longitude: " + lon + country + "<\/p>";
             
             $('#status2').html($('#status2').html() + ',' + x + ', ' + y);
         }
     }

     function Anclar(obj) {

       

     }
     
	-->
	
	
	</script>
	    
</head>
<body  style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: #FFFFFF;">
    <form id="form1" runat="server">
    <div>
     <asp:ScriptManager ID="ScriptManager1" runat="server" />
     
     <table width="100%" border="0" style="position: absolute; top: 10px; left: 20px;
        border: 1px solid black; background-color: #FFFFFF; color: #000000; font-size: 8px">
        <tr>
            <td style="width: 750px">
                <h4 id="status2">
                </h4>
            </td>
        </tr>
    </table>
    
    
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
            <td>
                <div id="divImgAncla" style="position: absolute; left: 0px; top: 0px; display: none;
                    width: 120px; height: 131px; background-image: url('ImgLogistica/Provincias_Ancla.png');
                    background-position: 0 0px; z-index: 700000">
                    &nbsp;
                </div>
                
                <div style="cursor: crosshair;">
                    <img id="world" class="mapper iborderE1B500 iopacity100 icolordfda58" src="ImgLogistica/mapa_argentina.png"
                        width="435" border="0" alt="" usemap="#map_of_world" />
                </div>
                <map id="map_of_world" name="map_of_world">
                        
                         <area alt="" shape="poly" tooltip="Provincia de Jujuy" onmouseover="cvi_tip._show(event);" onmouseout="cvi_tip._hide(event);"
                        onmousemove="cvi_tip._move(event);" href="#" coords="178, 38,197, 61,187, 70,160, 51"  />
                        
                         <area alt="" shape="poly" tooltip="Provincia de Salta" onmouseover="cvi_tip._show(event);" onmouseout="cvi_tip._hide(event);"
                        onmousemove="cvi_tip._move(event);" href="#" coords="206, 38,225, 43,215, 90,166, 89,167, 71,203, 71" />
                        
                         <area alt="" shape="poly" tooltip="Provincia de Formosa" onmouseover="cvi_tip._show(event);" onmouseout="cvi_tip._hide(event);"
                        onmousemove="cvi_tip._move(event);" href="#" coords="231, 45,272, 74,297, 92,286, 110,232, 70" />

                   
                         <area alt="" shape="poly" tooltip="Provincia de Chubut" onmouseover="cvi_tip._show(event);" onmouseout="cvi_tip._hide(event);"
                        onmousemove="cvi_tip._move(event);" href="#" coords="108, 368,188, 369,192, 370,192, 373,188, 377,188, 378,189, 381,192, 383,191, 386,188, 390,186, 393,186, 397,187, 400,187, 402,183, 405,182, 409,180, 412,173, 415,167, 418,165, 421,164, 422,162, 425,160, 429,158, 432,160, 436,115, 436,116, 433,116, 430,117, 425,117, 422,118, 419,117, 417,120, 416,120, 413,120, 407,117, 407,114, 407,113, 406,111, 403,113, 398,113, 395,111, 393,111, 389,106, 384,105, 383,104, 378,105, 374,106, 372,109, 369" />
                                     

                        <area alt="" onclick="Anclar(this)" shape="poly" tooltip="Provincia de Tucumán" onmouseover="cvi_tip._show(event);" onmouseout="cvi_tip._hide(event);"
                        onmousemove="cvi_tip._move(event);" href="#" coords="174, 106,179, 106,182, 103,185, 103,187, 104,192, 105,195, 105,198, 105,200, 107,201, 112,198, 116,197, 118,194, 124,194, 129,191, 132,186, 134,183, 136,181, 134,178, 130,175, 127,175, 125,175, 122,176, 118,177, 114,173, 110,174, 106" />
                                     
                        <area id="TucBis" alt="" shape="poly" tooltip="Provincia de Tucumán" onmouseover="cvi_tip._show(event);" onmouseout="cvi_tip._hide(event);" style="display:none"
                        onmousemove="cvi_tip._move(event);" href="#" coords="174, 106,179, 106,182, 103,185, 103,187, 104,192, 105,195, 105,198, 105,200, 107,201, 112,198, 116,197, 118,194, 124,194, 129,191, 132,186, 134,183, 136,181, 134,178, 130,175, 127,175, 125,175, 122,176, 118,177, 114,173, 110,174, 106" />                                     
                        
                        
                             
                        <area alt=""  shape="poly" tooltip="Provincia de Bs. As." onmouseover="cvi_tip._show(event);" onmouseout="cvi_tip._hide(event);"
                        onmousemove="cvi_tip._move(event);"  href="#" coords="217, 240,239, 242,253, 230,255, 230,258, 226,263, 229,271, 233,278, 238,282, 242,284, 245,287, 248,285, 252,291, 253,297, 259,295, 263,294, 267,294, 272,297, 274,300, 278,303, 279,304, 281,301, 286,297, 291,294, 294,290, 297,289, 302,284, 304,270, 309,265, 309,260, 311,233, 311,231, 309,225, 308,226, 311,223, 314,225, 317,225, 320,227, 323,224, 327,223, 328,221, 332,223, 335,225, 341,224, 344,220, 346,216, 345,216, 240" />
                        
                </map>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
