<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VistaCatalogo.aspx.cs" Inherits="VistaCatalogo" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <title>Vista Catalogo</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <link href="ImagenesCelular/style.css" rel="stylesheet" type="text/css" />

    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>

</head>

<script type="text/javascript">

    $(document).ready(function() {

        IniciarDetenerPromosActivas();

    })

    function IniciarDetenerPromosActivas() {

        slideShow.init();
        slideShow.lim();
        slideShow.stopnav();
    }

</script>

<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form1" runat="server">
    <div>
        <div id="gallery">
            <div id="imagearea">
                <div id="image">
                </div>
            </div>
            <div id="thumbwrapper">
                <div id="thumbarea">
                    <ul id="thumbs" runat="server">
                    </ul>
                </div>
            </div>
        </div>
        <div class="divnav">
        <a href="javascript:slideShow.mvblock(738,this)" id="previmg" class="imgnav" style="display:none" ></a><a class="imgnav" href="javascript:slideShow.mvblock(-738,this)"
            id="nextimg"></a>
            </div>

        <script type="text/javascript">
            var imgid = 'image';
            var imgdir = 'ImagenesCelular/Full';
            var imgext = '.jpg';
            var thumbid = 'thumbs';
            var auto = true;
            var autodelay = 30000000;
        </script>

        <script src="Scripts/slideCelular.js" type="text/javascript"></script>

    </div>
    </form>
</body>
</html>
