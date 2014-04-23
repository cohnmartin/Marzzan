/// <reference name="MicrosoftAjax.js"/>

// -------------------------------------- AJAX Required --------------------------------------
Type.registerNamespace("ControlsAjaxNotti");


// -------------------------------------- Constructor --------------------------------------
// Will be called by the MS Ajax Framework to construct the client side reperentation of the control
var $ClientServerCampoNumerico = ControlsAjaxNotti.ClientServerCampoNumerico = function (element) {

    ControlsAjaxNotti.ClientServerCampoNumerico.initializeBase(this, [element]);

    this._element = element; // div control that contains everything

    this._txtCampo = $get(element.id + "_txtCampo");

}



// -------------------------------------- Control Initialisation and Destruction --------------------------------------
$ClientServerCampoNumerico.prototype = {
    initialize: function () {
        ControlsAjaxNotti.ClientServerCampoNumerico.callBaseMethod(this, 'initialize');

        // Add custom initialization here
        this._addHandlers();
    },

    dispose: function () {
        //Add custom dispose actions here
        ControlsAjaxNotti.ClientServerCampoNumerico.callBaseMethod(this, 'dispose');
    }
}


$ClientServerCampoNumerico.prototype._addHandlers = function () {
    $find(this._txtCampo.id).add_keyPress(Function.createDelegate(this, this._keyPress));
    $find(this._txtCampo.id).add_blur(Function.createDelegate(this, this._OnBlur));
}

$ClientServerCampoNumerico.prototype._clearHandlers = function () {
    // remove event handlers to avoid memory leaks
    $clearHandlers(this._keyPress);

}


// -------------------------------------- HTML Events --------------------------------------



// -------------------------------------- Client side Properties --------------------------------------




// -------------------------------------- Control methods --------------------------------------

$ClientServerCampoNumerico.prototype._keyPress = function (sender, arg) {
    var c = arg.get_keyCode();
    if (c == 13) {
        arg.set_cancel(true);
        sender.blur();
    }

}

$ClientServerCampoNumerico.prototype._OnBlur = function (sender, arg) {

    var objControl = $find(this._txtCampo.id);
    if (objControl.get_value() != "") {
        // Hay que disparar el evento
        //objControl._onDatosCompletos(sender._element.id);
    }
    else {
        Page_ClientValidate('ServerCampoNumerico');
    }
}


$ClientServerCampoNumerico.prototype._keyPress = function (sender, arg) {
    var c = arg.get_keyCode();
    if (c == 13) {
        arg.set_cancel(true);
        sender.blur();
    }

}




$ClientServerCampoNumerico.registerClass('ControlsAjaxNotti.ClientServerCampoNumerico', Sys.UI.Control);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();