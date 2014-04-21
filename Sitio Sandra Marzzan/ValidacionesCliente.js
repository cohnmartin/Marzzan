/// <reference name="MicrosoftAjax.js"/>
function ValidarNumerico() {
    var cant = document.getElementById(event.srcElement.id).value;
    var keyid = (window.Event) ? event.which : event.keyCode;
    //alert(keyid);
    if (keyid == 38) {
        if (cant == '') {
            document.getElementById(event.srcElement.id).value = 1;
        }
        else {
            if (parseInt(cant, 0) < 999) {
                document.getElementById(event.srcElement.id).value = parseInt(cant, 0) + 1;
            }
        }
    }
    else if (keyid == 40) {
        if (cant == '' || cant == 0) {
            document.getElementById(event.srcElement.id).value = 0;
        }
        else {
            document.getElementById(event.srcElement.id).value = parseInt(cant, 0) - 1;
        }
    }

    return (keyid == 46 || keyid == 37 || keyid == 39 || keyid <= 13 || (keyid >= 48 && keyid <= 57) || (keyid >= 96 && keyid <= 105));

}

function OcultarToolTip(strtoolTip)
     {     
         var tooltip = $find(strtoolTip);
         tooltip.hide();
     }

function CambiarCursor() {
    if (!e) var e = window.event

    if (e.offsetX >= 35 && e.offsetY <= 10) {
        document.getElementById(event.srcElement.id).style.cursor = "hand";
    }
    else if (e.offsetX >= 35 && e.offsetY > 10) {
        document.getElementById(event.srcElement.id).style.cursor = "hand";
    }
    else {
        document.getElementById(event.srcElement.id).style.cursor = "default";
    }
}

function CambiarCursorProductos(obj) {
    if (!e) var e = window.event

    if (e.offsetX >= 35 && e.offsetY <= 10) {
        obj.style.cursor = "hand";
    }
    else if (e.offsetX >= 35 && e.offsetY > 10) {
    obj.style.cursor = "hand";
    }
    else {
        obj.style.cursor = "default";
    }
}


function SubirCantidad() {

    var cant = document.getElementById(event.srcElement.id).value;

    if (!e) var e = window.event
    //alert(e.offsetX + '  ' + e.offsetY);
    if (e.offsetX >= 35 && e.offsetY <= 10) {
        if (cant == '' || cant == 0) {
            document.getElementById(event.srcElement.id).value = 1;
        }
        else {
            if (parseInt(cant, 0) < 999) {
                document.getElementById(event.srcElement.id).value = parseInt(cant, 0) + 1;
            }
        }

    }
    else if (e.offsetX >= 35 && e.offsetY > 10) {
        if (cant == '' || cant == 0) {
            document.getElementById(event.srcElement.id).value = 0;
        }
        else {
            document.getElementById(event.srcElement.id).value = parseInt(cant, 0) - 1;
        }
    }
}