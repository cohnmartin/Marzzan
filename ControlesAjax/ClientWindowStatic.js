/// <reference name="MicrosoftAjax.js"/>

// -------------------------------------- AJAX Required --------------------------------------
Type.registerNamespace("ControlsAjaxNotti");


// -------------------------------------- Constructor --------------------------------------
// Will be called by the MS Ajax Framework to construct the client side reperentation of the control
var $ClientWindowStatic = ControlsAjaxNotti.ClientWindowStatic = function (element) {

    ControlsAjaxNotti.ClientWindowStatic.initializeBase(this, [element]);

    this._element = element; // div control that contains everything


    this._divDialog = $get(element.id + "_divDialog");
    this._divContent = $get(element.id + "_divContent");
    this._divHeader = $get(element.id + "_divHeader");
    this._divBody = $get(element.id + "_divBody");

}



// -------------------------------------- Control Initialisation and Destruction --------------------------------------
$ClientWindowStatic.prototype = {
    initialize: function () {
        ControlsAjaxNotti.ClientWindowStatic.callBaseMethod(this, 'initialize');

        // Add custom initialization here
        //this._addHandlers();
    },

    dispose: function () {
        //Add custom dispose actions here
        ControlsAjaxNotti.ClientWindowStatic.callBaseMethod(this, 'dispose');
    }
}


// -------------------------------------- HTML Events --------------------------------------

// -------------------------------------- Client side Properties --------------------------------------

$ClientWindowStatic.prototype.get_ControlFocus = function () { return this._ControlFocus; }
$ClientWindowStatic.prototype.set_ControlFocus = function (value) {
    this._ControlFocus = value;
}



// -------------------------------------- Control methods --------------------------------------



$ClientWindowStatic.registerClass('ControlsAjaxNotti.ClientWindowStatic', Sys.UI.Control);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();