/// <reference name="MicrosoftAjax.js"/>
Sys.Application.add_init(AppInit);

function AppInit(sender) {
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(SetTam);
}

function HideWaiting() {
    
    $("#divBlock").remove();
    $("#tblMessange").remove();
    $(".main").css("overflow", "");

}

function ShowWaiting(mesage) {
    var jq_Content = $(".main");
    var width = jq_Content.width();
    var height = $(window).height() > jq_Content.height() ? $(window).height() - 40 : jq_Content.height();
    var divBlock = "divBlock";
    var tblBlock = "tblMessange";

    if (width > 0) {

        // Deshabilito el scroll de la pantalla para que se bloque por completo
        jq_Content.css("overflow", "hidden");

        var newdiv = document.createElement('div');
        newdiv.setAttribute('id', divBlock);

        if (width) {
            newdiv.style.width = width + 15 + "px";
        }
        if (height) {
            newdiv.style.height = height  + "px";
        }

        newdiv.style.height = height+500;
        newdiv.style.position = "absolute";
        newdiv.style.left = jq_Content.position().left + "px";
        newdiv.style.top = jq_Content.position().top + "px";
        newdiv.style.paddingTop = 15 + "px";


        newdiv.style.textAlign = "center";
        newdiv.style.verticalAlign = "middle";
        newdiv.style.backgroundColor = "#000";
        newdiv.style.zIndex = 980000000;
        newdiv.style.border = "1px solid black";

        if (typeof (newdiv.style.filter) != "undefined") {
            if (navigator.appName.indexOf("Microsoft") != -1) {
                newdiv.style.filter = "alpha(opacity=70);";
            }
            else {
                newdiv.style.opacity = .7;
            }
        }
        else if (typeof (newdiv.style.MozOpacity) != "undefined") {
            newdiv.style.MozOpacity = 1 / 2;
        }


        var tbl = $('<table id="' + tblBlock + '" cellpadding="0" cellspacing="0" style="width: ' + (width +40) + 'px;z-index:988000000;height:80px" border="0">')
        tbl.css("position", "absolute");
        tbl.css("left", jq_Content.position().left);
        tbl.css("top", "20%");
        tbl.css("textAlign", "center");
        tbl.css("verticalAlign", "middle");


        var tr = $("<TR>")
        var td = $("<TD>").css("paddingTop", (($(window).height() - 50) / 2) - 120);
        var img = $('<img style="z-index:988000000" id="imgWaiting" src = "imagenes/waiting.gif" />');

        td.append(img).appendTo(tr);
        tr.appendTo(tbl);

        var tr = $("<TR>")
        var td = $("<TD>")
        var msg = $('<span style="font-weight:bold;color:White;z-index:988000000;font-size:12px">' + mesage + '</span>');

        td.append(msg).appendTo(tr);
        tr.appendTo(tbl);


        jq_Content.append(newdiv);
        jq_Content.append(tbl);


    }


}

//the following code use radconfirm to mimic the blocking of the execution thread.
//The approach has the following limitations:
//1. It works inly for elements that have *click* method, e.g. links and buttons
//2. It cannot be used in if(!confirm) checks
window.blockConfirm = function(text, mozEvent, oWidth, oHeight, callerObj, oTitle) {

    var ev = mozEvent ? mozEvent : window.event; //Moz support requires passing the event argument manually 
    //Cancel the event 
    ev.cancelBubble = true;
    ev.returnValue = false;
    if (ev.stopPropagation) ev.stopPropagation();
    if (ev.preventDefault) ev.preventDefault();

    //Determine who is the caller 
    var callerObj = ev.srcElement ? ev.srcElement : ev.target;

    //Call the original radconfirm and pass it all necessary parameters 
    if (callerObj) {
        //Show the confirm, then when it is closing, if returned value was true, automatically call the caller's click method again. 
        var callBackFn = function(arg) {
            if (arg) {
                callerObj["onclick"] = "";
                if (callerObj.click) callerObj.click(); //Works fine every time in IE, but does not work for links in Moz 
                else if (callerObj.tagName == "A") //We assume it is a link button! 
                {
                    try {
                        eval(callerObj.href)
                    }
                    catch (e) { }
                }
            }
        }

        radconfirm(text, callBackFn, oWidth, oHeight, callerObj, oTitle);
    }
    return false;
}

window.blockConfirmCallBackFn = function(text, mozEvent, oWidth, oHeight, callerObj, oTitle, CallBackFn) {

    var ev = mozEvent ? mozEvent : window.event; //Moz support requires passing the event argument manually
    //Cancel th e event 

    ev.cancelBubble = true;
    ev.returnValue = false;
    if (ev.stopPropagation) ev.stopPropagation();
    if (ev.preventDefault) ev.preventDefault();

    //Determine who is the caller 
    var callerObj = ev.srcElement ? ev.srcElement : ev.target;

    //Call the original radconfirm and pass it all necessary parameters 
    if (callerObj) {
        //Show the confirm, then when it is closing, if returned value was true, automatically call the caller's click method again. 
        radconfirm(text, CallBackFn, oWidth, oHeight, callerObj, oTitle);
    }
    return false;
}

function Calcular() {

    var txtlargas = document.forms[0].item('ctl00_ContentPlaceHolder1_txtLargas');
    var txtcortas = document.forms[0].item('ctl00_ContentPlaceHolder1_txtCortas');
    var lbltotal = document.getElementById("ctl00_ContentPlaceHolder1_lblTotalEstacas");

    var largas = txtlargas.value * 3;
    var cortas = txtcortas.value;
    var total = parseInt(largas, 0) + parseInt(cortas, 0);

    lbltotal.innerText = total;
}

function SeleccionarFila(sender, eventArgs) {
    var evt = eventArgs.get_domEvent();
    var index = eventArgs.get_itemIndexHierarchical();
    sender.get_masterTableView().selectItem(sender.get_masterTableView().get_dataItems()[index].get_element(), true);

    var btnEli = document.forms[0].item('ctl00_ContentPlaceHolder1_btnEliminar');
    var btnEdi = document.forms[0].item('ctl00_ContentPlaceHolder1_btnEditar');
    btnEli.disabled = false;
    btnEdi.disabled = false;

    evt.cancelBubble = true;
    evt.returnValue = false;

    if (evt.stopPropagation) {
        evt.stopPropagation();
        evt.preventDefault();
    }
}

function checkControl(obj, items) {

    var lblVPI = document.getElementById("ctl00_ContentPlaceHolder1_lblVariedadPI");
    var oculto = document.getElementById("ctl00_ContentPlaceHolder1_HiddenField1");
    var rbl = document.getElementById(obj);
    var rblChild = null;

    try {
        for (i = 0; i < items; i++) {
            rblChild = document.getElementById(obj + "_" + i.toString());
            if (rblChild.checked) {
                if (rblChild.value == "V") {
                    lblVPI.innerText = 'Variedad';
                    oculto.value = "V";
                }
                else {
                    lblVPI.innerText = 'PortaInjerto';
                    oculto.value = "PI";
                }
            }
        }
    }
    catch (e) { }


}

function SetTam(sender, args) {
    
    var mensaje = args.get_postBackElement().getAttribute("Mensaje");


    var div = document.getElementById('divBloq1');
    if (div != null) {

        if (document.documentElement.clientHeight > document.documentElement.scrollHeight)
            div.style.height = document.documentElement.clientHeight + "px";
        else
            div.style.height = document.documentElement.scrollHeight + "px";


        div.style.width = document.documentElement.scrollWidth + "px";
        div.style.top = div.style.left = 0;
        div.style.position = "absolute";
        div.style.backgroundColor = "#CFF0FE";
        div.style.zIndex = 100000000;

        try { div.style.opacity = "0.5"; }
        catch (e) { }

        if (typeof (div.style.filter) != "undefined") {
            div.style.filter = "alpha(opacity=50);";
        }
        else if (typeof (div.style.MozOpacity) != "undefined") {
            div.style.MozOpacity = 1 / 2;
        }

        if (mensaje != null) {
            var lbl = document.getElementById('divTituloCarga');
            lbl.innerText = mensaje;
        }
        else {
            var lbl = document.getElementById('divTituloCarga');
            //lbl.innerText = "";
        }
    }
}