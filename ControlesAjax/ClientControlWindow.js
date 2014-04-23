/// <reference name="MicrosoftAjax.js"/>

// -------------------------------------- AJAX Required --------------------------------------
Type.registerNamespace("ControlsAjaxNotti");



// -------------------------------------- Constructor --------------------------------------
// Will be called by the MS Ajax Framework to construct the client side reperentation of the control
var $ClientControlWindow = ControlsAjaxNotti.ClientControlWindow = function (element) {

    ControlsAjaxNotti.ClientControlWindow.initializeBase(this, [element]);

    this._element = element; // div control that contains everything

    this._closeImage = $get(element.id + "_imgCloseControlWindow");
    this._divCuerpo = $get(element.id + "_DivCuerpo");
    this._divCarga = $get(element.id + "_DivCarga");
    this._lblTitulo = $get(element.id + "_lblTituloControlWindow");
    this._divLeft = $get(element.id + "_DivLeft");
    this._divRight = $get(element.id + "_DivRight");


    this.refWaitingImg = "";


    //Required Event Handlers
    this._CancelHandler = null;


}



// -------------------------------------- Control Initialisation and Destruction --------------------------------------
$ClientControlWindow.prototype = {
    initialize: function () {
        ControlsAjaxNotti.ClientControlWindow.callBaseMethod(this, 'initialize');

        // Add custom initialization here
        this._addHandlers();

    },

    dispose: function () {
        //Add custom dispose actions here
        ControlsAjaxNotti.ClientControlWindow.callBaseMethod(this, 'dispose');
    }
}

$ClientControlWindow.prototype._addHandlers = function () {

    $addHandlers(this._closeImage, {
        click: Function.createDelegate(this, this._closeImageClick)
    });


    var target = this.get_element();

    this._CancelHandler = Function.createDelegate(this, this._onCancel)

    //Attach the required event handlers
    $addHandlers(target, { 'Cancel': this._CancelHandler }, this);
}


$ClientControlWindow.prototype.add_Cancel = function (handler) {
    this.get_events().addHandler('Cancel', handler);
}

$ClientControlWindow.prototype.remove_Cancel = function (handler) {
    this.get_events().removeHandler('Cancel', handler);
}


$ClientControlWindow.prototype._clearHandlers = function () {
    // remove event handlers to avoid memory leaks
    $clearHandlers(this._closeItemsImage);
    $clearHandlers(this._CancelHandler);
}


// -------------------------------------- HTML Events --------------------------------------

$ClientControlWindow.prototype._closeImageClick = function(e) {
    // ejecuto el metodo que cierra la ventana.
    this.CloseWindows();

    // ejecuto el evento que indica que la ventana se ha cerrado.
    this._onCancel();


    if ('bubbles' in e) {   // all browsers except IE before version 9
        if (e.bubbles) {
            e.stopPropagation();
        }
    }
    else {  // Internet Explorer before version 9
        // always cancel bubbling
        e.cancelBubble = true;
    }

    //window.event.cancelBubble = true;

    return false;
}

$ClientControlWindow.prototype._onCancel = function (e) {

    var handler = this.get_events().getHandler('Cancel');

    // Check if there is any subscriber of this event
    if (handler != null) {
        handler(this, Sys.EventArgs.Empty);
    }

    return false;
}

// -------------------------------------- Client side Properties --------------------------------------

$ClientControlWindow.prototype.get_CollectionDiv = function () { return this._CollectionDiv; }
$ClientControlWindow.prototype.set_CollectionDiv = function (value) {
    this._CollectionDiv = value;
}

$ClientControlWindow.prototype.get_ControlFocus = function () { return this._ControlFocus; }
$ClientControlWindow.prototype.set_ControlFocus = function (value) {
    this._ControlFocus = value;
}


// -------------------------------------- Control methods --------------------------------------
$ClientControlWindow.prototype.HideWaiting = function () {
    var divCarga = this._divCarga;
    var divCuerpo = this._divCuerpo;
    var divBlock = "DivBlock" + this._element.id;
    var tblBlock = "tblBlock" + this._element.id;


    $("#" + divBlock).remove();
    $("#" + tblBlock).remove();

}

$ClientControlWindow.prototype.ShowWaiting = function (CloseOnFinish, mesage) {
    //debugger;
    var divCarga = this._divCarga;
    var divCuerpo = this._divCuerpo;
    var divBlock = "DivBlock" + this._element.id;
    var tblBlock = "tblBlock" + this._element.id;

    var refWaitingImg = this.refWaitingImg;

    if ($("#" + divBlock).length > 0) {
        $("#" + divBlock).show();
    }
    else {
        _creatediv($("#" + this._element.id), $("#" + this._element.id + "_DivCuerpo"));
    }


    /// Me suscribo al evento para detectar cuando termina de actuar
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(_hide);


    function _hide() {

        $("#" + divBlock).remove();
        $("#" + tblBlock).remove();

        if (CloseOnFinish == true) {
            $('#' + divCuerpo.id).fadeOut(300, function () {
                divCarga.style.display = "none";
            });
        }

        Sys.WebForms.PageRequestManager.getInstance().remove_endRequest(_hide);
    }


    function _creatediv(oWin, jq_Content) {
        //debugger;
        var width = jq_Content.width();
        var height = jq_Content.height();
        var left = jq_Content.position().left;
        var top = jq_Content.position().top;



        if (width > 0) {
            //debugger;

            var newdiv = document.createElement('div');
            newdiv.setAttribute('id', divBlock);

            if (width) {
                newdiv.style.width = width - 32;
            }
            if (height) {
                newdiv.style.height = (height + 23) / 2 + 90;
            }

            newdiv.style.position = "absolute";
            newdiv.style.left = 15;
            newdiv.style.top = 33;

            if ((height / 2) > 80) {
                newdiv.style.paddingTop = (height / 2) - 80;
            }
            else {
                newdiv.style.paddingTop = (height / 2) - 50;
            }

            newdiv.style.textAlign = "center";
            newdiv.style.verticalAlign = "middle";
            newdiv.style.background = "black";
            newdiv.style.zIndex = 980000000;


            if (typeof (newdiv.style.filter) != "undefined") {
                newdiv.style.filter = "alpha(opacity=40);";
            }
            else if (typeof (newdiv.style.MozOpacity) != "undefined") {
                newdiv.style.MozOpacity = 1 / 2;
            }


            var tbl = $('<table id="' + tblBlock + '" cellpadding="0" cellspacing="0" style="width: ' + width + 'px;z-index:988000000;height:80px" border="0">')
            tbl.css("position", "absolute");
            tbl.css("left", "0");
            tbl.css("top", "0");
            tbl.css("textAlign", "center");
            tbl.css("verticalAlign", "middle");


            var tr = $("<TR>")
            var td = $("<TD>").css("paddingTop", (height / 2));
            var img = $('<img style="z-index:988000000" id="imgWaiting" src = "url(' + refWaitingImg + ')" />');

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

}
$ClientControlWindow.prototype.CloseWindows = function (Action) {
    //debugger;
    var div = this._divCarga;
    var divBlock = "DivBlock" + this._element.id;
    var tblBlock = "tblBlock" + this._element.id;



    $('#' + this._divCuerpo.id).fadeOut(300, function () {
        div.style.display = "none";
        $("#" + divBlock).remove();
        $("#" + tblBlock).remove();
    });
    return false;
}

$ClientControlWindow.prototype.ShowWindows = function (DivShow, Titulo, CtrFocus) {
    //debugger;
    var div = this._divCarga;
    var divCuerpo = this._divCuerpo;
    var divLeft = this._divLeft;
    var divRight = this._divRight;

    var cotrolFocus = this.get_ControlFocus();

    if (document.documentElement.clientHeight > document.documentElement.scrollHeight)
        div.style.height = document.documentElement.clientHeight + "px";
    else
        div.style.height = document.documentElement.scrollHeight + "px";


    div.style.width = document.documentElement.scrollWidth + "px";
    div.style.top = div.style.left = 0;
    div.style.position = "absolute";
    div.style.backgroundColor = "WhiteSmoke";
    div.style.zIndex = 100000000;


    try { div.style.opacity = "0.7"; }
    catch (e) { }

    if (typeof (div.style.filter) != "undefined") {
        div.style.filter = "alpha(opacity=70);";
    }
    else if (typeof (div.style.MozOpacity) != "undefined") {
        div.style.MozOpacity = 1 / 2;
    }

    //debugger;
    if (Titulo != undefined)
        this._lblTitulo.innerText = Titulo.toLowerCase();

    //debugger;
    var AltoDivShow;
    if (DivShow != undefined) {
        var divs = this.get_CollectionDiv().split('|');
        for (var i = 0; i < divs.length; i++) {
            if (divs[i] == DivShow) {
                var ctrToShow = $('#' + divs[i]);
                ctrToShow.show();
                AltoDivShow = ctrToShow[0].style.height.replace("px", "");
            }
            else {
                var ctrToShow = $('#' + divs[i]);
                ctrToShow.hide();
            }
        }
    }

    //debugger;
    // Establezco el alto de la ventana
    if (AltoDivShow.indexOf("%") < 0 && parseInt(AltoDivShow) > 0)
        $('#' + this._divCuerpo.id).height(parseInt(AltoDivShow));


    $(div).css("display", "inline");
    //div.style.display = "inline-block";
    divCuerpo.style.left = ((document.documentElement.scrollWidth / 2) - ($('#' + this._divCuerpo.id).width() / 2)) - 20 + "px";
    divCuerpo.style.top = (((document.documentElement.clientHeight) / 2) + document.documentElement.scrollTop - ($('#' + this._divCuerpo.id).height() / 2) - 30) + "px";

    if (divCuerpo.style.top.indexOf("-") >= 0) {
        divCuerpo.style.top = "10px";
    }


    divLeft.style.height = $('#' + this._divCuerpo.id).height() + 'px';
    divRight.style.height = $('#' + this._divCuerpo.id).height() + 'px';

    $('#' + this._divCuerpo.id).fadeIn(500, function () {
        if (CtrFocus != null) {
            CtrFocus.focus();
        }
    });


    return false;

}


$ClientControlWindow.registerClass('ControlsAjaxNotti.ClientControlWindow', Sys.UI.Control);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();