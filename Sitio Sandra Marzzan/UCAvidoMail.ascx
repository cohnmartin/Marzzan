<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCAvidoMail.ascx.cs" Inherits="UCAvidoMail" %>
<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="AjaxInfo" %>



<script type="text/javascript">

    function HideMessage() {
        clearInterval(timer);
        $('#about_popup').slideToggle(1500);
    }

    function UCAvisoMail_ShowMessange(CantidadMails) {
        $('#spCantidad').text(CantidadMails);
        $('#about_popup').slideToggle(1500);
        timer = setInterval('HideMessage()', 35000);
        return false;
    }
</script>

<style type="text/css">
    #container
    {
        width: 98%;
        height: 99%;
        margin: 0 auto;
        position: absolute;
        top: 0px;
        left: 0px;
        border: 0px solid black;
        z-index:-100;
    }
    #about_popup
    {
        width: 100%;
        height: 85px;
        padding: 5px;
        position: absolute;
        text-align: right;
        bottom: 0;
        display: none;
        
    }
    a:link
    {
        color: white;
        text-decoration: none;
    }
    a:visited
    {
        color: white;
        text-decoration: none;
    }
    a:active
    {
        color: white;
        text-decoration: none;
    }
</style>
<div id="container">
    <div id="about_popup">
        <table cellpadding="0" border="0" cellspacing="0" 
         style="height:80px;width:320px; background-image:url(Imagenes/FondoMensajeMail.png);background-repeat:no-repeat;">
            <tr>
                <td  style="padding-left:3px; width:230px; color: white; font-family: 'Bookman Old Style'; font-size: small" align="center">
                    <a href="ConsolaMail.aspx" target="_blank"><span>USTED TIENE </span><span id="spCantidad"
                        style="color: white; font-size: 25px; font-weight: bold;"></span><span>&nbsp;NUEVOS
                            CORREOS</span> </a>
                </td>
                <td align="left" style="width: 80px; padding-left: 5px">
                    <img alt="a" src="Imagenes/sobre.png" width="70" height="70" />
                </td>
            </tr>
        </table>
    </div>
</div>
