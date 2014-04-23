/// <reference name="MicrosoftAjax.js"/>

// -------------------------------------- AJAX Required --------------------------------------
Type.registerNamespace("ControlsAjaxNotti");


// -------------------------------------- Constructor --------------------------------------
// Will be called by the MS Ajax Framework to construct the client side reperentation of the control
var $ClientPanel = ControlsAjaxNotti.ClientServerPanel = function (element) {

    ControlsAjaxNotti.ClientServerPanel.initializeBase(this, [element]);

    this._element = element; // div control that contains everything

}



// -------------------------------------- Control Initialisation and Destruction --------------------------------------
$ClientPanel.prototype = {
    initialize: function () {
        ControlsAjaxNotti.ClientServerPanel.callBaseMethod(this, 'initialize');

        // Add custom initialization here
        //this._addHandlers();

    },

    dispose: function () {
        //Add custom dispose actions here
        ControlsAjaxNotti.ClientServerPanel.callBaseMethod(this, 'dispose');

        //this._clearHandlers();

    },

    _clearElements: function (obj, level) {

        for (var i = 0; i < obj.childNodes.length; i++) {
            var childObj = obj.childNodes[i];
            if (childObj.tagName) {
                this._clearElement(childObj);
            }
            this._clearElements(childObj, level + 1);
        }

    },

    _disableChildElements: function (disabled) {
        var level = 0;
        $("#" + this._element.id + " INPUT").each(function () {

            if (this.id.indexOf("imgCloseControlWindow") < 0 && this.id.indexOf("lblTituloControlWindow") < 0)
                this.disabled = disabled;

        }, null);

        var divBlock = "DivBlock" + this._element.id;
        if (disabled) {
            if ($("#" + divBlock).length > 0)
                $("#" + divBlock).show();
            else
                this._creatediv($("#" + this._element.id));
        }
        else {

            $("#" + divBlock).hide();
        }

        return;

    },


    _clearElement: function (obj) {

        if (obj.className.indexOf("RadComb") >= 0) {
            var objRad = $find(obj.id);
            if (objRad != null) {
                objRad.clearSelection();
                //objRad.clearItems();
            }
        }
        else {

            if (obj.type == 'text' && obj.className.indexOf("rcbInput") < 0) {
                if (obj.defaultValue != "")
                    obj.value = obj.defaultValue;
                else
                    obj.value = "";
            }
            else if (obj.type == 'textarea') {
                obj.value = "";
            }
        }
    },


    _removediv: function () {
        var divBlock = "DivBlock" + this._element.id;
        if ($("#" + divBlock).length > 0)
            $("#" + divBlock).remove();

        return divBlock;
    },

    _creatediv: function (jq_Content) {
        //debugger;
        var divBlock = this._removediv();
        var width = jq_Content.width();
        var height = jq_Content.height();
        var left = jq_Content.position().left;
        var top = jq_Content.position().top;

        if (width > 0) {
            var newdiv = document.createElement('div');
            newdiv.setAttribute('id', divBlock);
            newdiv.innerHTML = "&nbsp;";

            if (width) {
                newdiv.style.width = width;
            }
            if (height) {
                newdiv.style.height = height;
            }

            newdiv.style.position = "absolute";
            newdiv.style.left = left;
            newdiv.style.top = top;

            newdiv.style.textAlign = "center";
            newdiv.style.verticalAlign = "middle";
            newdiv.style.background = "WhiteSmoke";
            newdiv.style.zIndex = 980000000;

            if (typeof (newdiv.style.filter) != "undefined") {
                newdiv.style.filter = "alpha(opacity=0);";
            }
            else if (typeof (newdiv.style.MozOpacity) != "undefined") {
                newdiv.style.MozOpacity = 1 / 2;
            }

            jq_Content.append(newdiv);
        }
    }

}

$ClientPanel.prototype._clearHandlers = function () {

}

$ClientPanel.prototype._addHandlers = function () {

}


// -------------------------------------- HTML Events --------------------------------------


// -------------------------------------- Client side Properties --------------------------------------

// -------------------------------------- Control methods --------------------------------------

$ClientPanel.prototype.DisabledElement = function (disabled) {

    this._disableChildElements(disabled);
    return false;
}

$ClientPanel.prototype.ClearElements = function () {

    this._clearElements(this._element, 0);
    return false;
}


$ClientPanel.registerClass('ControlsAjaxNotti.ClientServerPanel', Sys.UI.Control);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();