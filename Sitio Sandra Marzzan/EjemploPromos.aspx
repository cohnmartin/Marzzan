<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EjemploPromos.aspx.cs" Inherits="EjemploPromos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ejemplo de Promociones</title>
    <link href="ImagenesPromos/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="gallery">
        <div id="imagearea">
            <div id="image">
                <a href="javascript:slideShow.nav(-1)" class="imgnav " id="previmg"></a><a href="javascript:slideShow.nav(1)"
                    class="imgnav " id="nextimg"></a>
            </div>
        </div>
        <div id="thumbwrapper">
            <div id="thumbarea">
                <ul id="thumbs" runat="server">
                </ul>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var imgid = 'image';
        var imgdir = 'ImagenesPromos/Full';
        var imgext = '.jpg.ashx';
        var thumbid = 'thumbs';
        var auto = true;
        var autodelay = 5;
    </script>

    <script src="Scripts/slide.js" type="text/javascript"></script>

    </form>
</body>
</html>
