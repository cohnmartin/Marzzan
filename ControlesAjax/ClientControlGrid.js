/// <reference name="MicrosoftAjax.js"/>

// -------------------------------------- AJAX Required --------------------------------------
Type.registerNamespace("ControlsAjaxNotti");


// -------------------------------------- Constructor --------------------------------------
// Will be called by the MS Ajax Framework to construct the client side reperentation of the control
var $ClientControlGrid = ControlsAjaxNotti.ClientControlGrid = function (element) {


    ControlsAjaxNotti.ClientControlGrid.initializeBase(this, [element]);

    this._element = element; // div control that contains everything


    //Required Event Handlers
    //    this._rowClickedHandler = null;
    //    this._rowClickingHandler = null;
    this._ChangePageIndexHandler = null;


    //Properties
    this._allowGroupRows = false;
    this._tblBase = "";
    this._dataSource = new Array();
    this._columns = new Array();
    this._tblDynamic = null;
    this._functionColumns = new Array();
    this._functionsGral = new Array();

    this._allowMultiSelection = false;
    this._keyName = "";
    this._currentPageIndex = 0;
    this._allowPaging = false;
    this._pageSize = null;
    this._virtualCount = null;
    this._allowRowSelection = false;
    this._callbackID = null;



    this.refDeleteImg = "";
    this.refAddImg = "";
    this.refEditImg = "";
    this.refExcelImg = "";
    this.refImgPageSelected = "";
    this.uniqueID = "";
    this.refWaitingImg = "";
    this.refExclamacionImg = "";
    this.emptyMessage = "";
    this.showDataOnInit = true;
    this.onClientFunctionRowClicked = "";
    this.columnasVisibles = 0;


    //Controls
    this._imgFirst = null;
    this._imgLast = null;
    this._imgNext = null;
    this._imgPrev = null;
    this._toolTip = null;
    this._toolTipCtr = $find(element.id + "_RadToolTipCtr");

    // Variables
    this._CurrentGroupPage = 10;
}



// -------------------------------------- Control Initialisation and Destruction --------------------------------------
$ClientControlGrid.prototype = {


    dispose: function () {

        $clearHandlers(this.get_element()); // Detach all event handlers

        //Add custom dispose actions here
        ControlsAjaxNotti.ClientControlGrid.callBaseMethod(this, 'dispose');
    },


    get_allowGroupRows: function () {
        return this._allowGroupRows;
    },

    set_allowGroupRows: function (value) {
        this._allowGroupRows = value;
    },

    get_tblBase: function () {
        return this._tblBase;
    },

    set_tblBase: function (value) {
        this._tblBase = value;
    },

    get_dataSource: function () {
        return this._dataSource;
    },

    set_dataSource: function (value) {
        this._dataSource = value;
    },

    get_columns: function () {
        return this._columns;
    },

    set_columns: function (value) {
        this._columns = value;
    },

    get_functionColumns: function () {
        return this._functionColumns;
    },

    set_functionColumns: function (value) {
        this._functionColumns = value;
    },

    get_functionsGral: function () {
        return this._functionsGral;
    },

    set_functionsGral: function (value) {
        this._functionsGral = value;
    },

    get_allowMultiSelection: function () {
        return this._allowMultiSelection;
    },

    set_allowMultiSelection: function (value) {
        this._allowMultiSelection = value;
    },

    get_keyName: function () {
        return this._keyName;
    },

    set_keyName: function (value) {
        this._keyName = value;
    },

    set_ClientdataSource: function (value) {

        this.set_dataSource(value);
        if (value != null)
            this._createTable(this._dataSource, this.showDataOnInit);
        else
            this._createTableEmpty();
    },

    get_allowPaging: function () {
        return this._allowPaging;
    },

    set_allowPaging: function (value) {
        this._allowPaging = value;
    },

    get_pageSize: function () {
        return this._pageSize;
    },

    set_pageSize: function (value) {
        this._pageSize = value;
    },

    get_virtualCount: function () {
        return this._virtualCount;

    },

    set_virtualCount: function (value) {
        this._virtualCount = value;
    },

    get_allowRowSelection: function () {
        return this._allowRowSelection;
    },

    set_allowRowSelection: function (value) {
        this._allowRowSelection = value;
    },

    get_currentPageIndex: function () {
        return this._currentPageIndex;
    },

    set_currentPageIndex: function (value) {
        this._currentPageIndex = value;
    },

    get_callbackID: function () {
        return this._callbackID;
    },

    set_callbackID: function (value) {
        this._callbackID = value;
    },

    initialize: function () {
        ControlsAjaxNotti.ClientControlGrid.callBaseMethod(this, 'initialize');

        var target = this.get_element();
        target.innerHTML = this._tblBase;


        if (this._allowGroupRows == null)
            this._allowGroupRows = false;


        this._imgFirst = $("#theFirst")[0];
        this._imgLast = $("#theLast")[0];
        this._imgNext = $("#theNext")[0];
        this._imgPrev = $("#thePrev")[0];
        this._tblDynamic = $("#" + this.uniqueID);

        // Add custom initialization here
        this._addHandlers();


        if (this._dataSource != null && this._dataSource.length > 0) {
            this._createTable(this._dataSource, this.showDataOnInit);
        }
        else {

            if (this.emptyMessage != "") {
                this._createTableEmpty();
            }
        }

    }


}


$ClientControlGrid.prototype._cleanTable = function () {

    //debugger;
    var tbl = this._tblDynamic;
    var IdTable = tbl[0].id;
    var tbody;

    /// Recupero el body de la tabla
    /// para insertar los elementos ahi.
    $('#' + IdTable).each(function () {
        if (!this.tBodies || !this.tBodies.length) {
            return;
        }
        else {
            tbody = this.tBodies[0];
        }
    });

    // Limpio de la grilla el contenido 1 que es el 
    $(tbody).empty();


    var trow = $("<tr>");
    var td = $("<td>")
    .attr("colspan", this.columnasVisibles)
    .attr("align", "left")
    .css("background-color", "white")
    .css("padding-left", "8px")
    .css("font", "12px/25px 'segoe ui' ,arial,sans-serif")
    .appendTo(trow);

    text = $('<span style="padding-left:8px;color:black;position:relative;top:-3px">&nbsp;</span>');
    $(td).append(text);


    trow.appendTo(tbody);
}

$ClientControlGrid.prototype._createTableEmpty = function () {
    //debugger;
    var tbl = this._tblDynamic;
    var IdTable = tbl[0].id;
    var tbody;
    var thead;
    var tfoot;

    /// Recupero el body de la tabla
    /// para insertar los elementos ahi.
    $('#' + IdTable).each(function () {
        if (!this.tBodies || !this.tBodies.length) {
            return;
        }
        else {
            tbody = this.tBodies[0];
        }

        thead = this.tHead;
        tfoot = this.tFoot;
    });

    // Limpio de la grilla el contenido 1 que es el 
    $(tbody).empty();


    var trow = $("<tr>");
    var td = $("<td>");


    td.attr("align", "left")
        .attr("colspan", this.columnasVisibles)
        .css("width", "100%")
        .css("background-color", "white")
        .css("padding-left", "8px")
        .css("font", "12px/25px 'segoe ui' ,arial,sans-serif")
        .appendTo(trow);

    img = $('<img id="imgAdd" src="url(' + this.refExclamacionImg + ')" />');
    text = $('<span style="padding-left:8px;color:black;position:relative;top:-3px">' + this.emptyMessage + "</span>");
    $(td).append(img);
    $(td).append(text);

    trow.appendTo(tbody);

    var columnas = this.get_columns();
    var functionColumnas = this.get_functionColumns();
    var functionGenerals = this.get_functionsGral();
    var TypeFuntionsGralEnum = { "Add": 0, "Delete": 1, "Edit": 2, "Excel": 3, "Custom": 4 }
    var refAddImg = this.refAddImg;
    var callBaxkExcelId = this.get_callbackID();
    var functionColumnAdd = GetFunctionGeneral(TypeFuntionsGralEnum["Add"])[0];
    var functionColumnCustoms = GetFunctionGeneral(TypeFuntionsGralEnum["Custom"]);

    $("#" + IdTable + " .tdFunctionAdd").each(function () {

        /// Limpio el boton antes creado
        $(this).empty();

        /// Div Contenedor
        divContainer = $('<div>');

        if (functionColumnAdd != undefined) {
            // botones para Funcion ADD
            div = $('<div class="tdAdd" style="cursor: pointer;width:190px;display: inline;padding-right:10px" >');
            div.data("keyValue", "");
            div.data("Type", TypeFuntionsGralEnum.Add);
            div.data("Index", 0);
            img = $('<img id="imgAdd" src="url(' + refAddImg + ')" />');
            text = $('<span style="padding-left:2px;color:White;position:relative;top:-3px">' + $(functionColumnAdd).attr("Text") + "</span>");
            $(div).append(img);
            $(div).append(text);

            // Agrego div add al div general
            $(divContainer).append(div);
        }

        for (var i = 0; i < functionColumnCustoms.length; i++) {

            // botones Customs
            divCustom = $('<div class="tdAdd" style="cursor: pointer;width:190px;display: inline;padding-right:10px" >');
            divCustom.data("keyValue", "");
            divCustom.data("Type", TypeFuntionsGralEnum.Custom);
            divCustom.data("Index", i);

            imgCustom = $('<img id="imgCustom" src="' + $(functionColumnCustoms[i]).attr("ImgUrl") + '" />');

            textCustom = $('<span style="padding-left:2px;color:White;position:relative;top:-3px">' + $(functionColumnCustoms[i]).attr("Text") + '</span>');

            $(divCustom).append(imgCustom);
            $(divCustom).append(textCustom);

            // Agrego div excel al div general
            $(divContainer).append(divCustom);


        }

        $(this).append(divContainer);
    });


    /// Busco la imagen Add en el head o foot, y las subscribo el evento click
    $("#" + IdTable + " .tdAdd").click(function () {
        //debugger;
        var functionColumn = GetFunctionGeneral($(this).data("Type"), $(this).data("Index"))[0];
        var functionToCall = eval(functionColumn.ClickFunction);
        if (functionToCall != undefined) {
            if ($(this).data("keyValue") != "") {
                functionToCall($(this)[0], $(this).data("keyValue"));
            }
            else {
                functionToCall($(this)[0], GetKeyValueSelected());
            }

        }
    });


    function GetFunctionGeneral(value, index) {
        var functionColumn = new Array();
        var cont = 0;

        $(functionGenerals).each(function () {
            if ($(this).attr("Type") == value) {

                if (index == undefined) {
                    functionColumn[cont] = this;
                }
                else {
                    if (cont == index) {
                        functionColumn[0] = this;
                    }
                }

                cont++;
            }
        });

        return functionColumn;
    }


    function GetKeyValueSelected() {
        var KeyValueSelected = "";

        $("#" + IdTable + " .trSelected").each(function () {

            var keyValue = $(this).attr('keyValue');
            jQuery.each(datos, function () {
                if ($(this).attr(keyName) == keyValue) {
                    KeyValueSelected = keyValue;
                    return false;
                }
            });

        });

        return KeyValueSelected;

    }
}

$ClientControlGrid.prototype._createTable = function (datos, showData) {

    AlignEnum = { 0: "left", 2: "center", 1: "right" }
    DataTypeEnum = { 0: "String", 1: "DateTime", 2: "Decimal", 3: "Integer", 4: "UrlImage", 5: "Bool" };
    TypeColumnRowEnum = { "Delete": 0, "Edit": 1, "Custom": 2 }
    TypeFuntionsGralEnum = { "Add": 0, "Delete": 1, "Edit": 2, "Excel": 3, "Custom": 4 }

    var tbl = this._tblDynamic;
    var IdTable = tbl[0].id;
    var tbody;

    /// Recupero el body de la tabla
    /// para insertar los elementos ahi.
    $('#' + IdTable).each(function () {
        if (!this.tBodies || !this.tBodies.length) {
            return;
        }
        else {
            tbody = this.tBodies[0];
        }
    });


    // Limpio de la grilla el contenido 1 que es el 
    $(tbody).empty();


    //var datos = this.get_dataSource();
    //var columnas = this.get_columns();
    //debugger;
    var columnas = new Array();
    $(this.get_columns()).filter(function () {
        if ($(this).attr("Display") == true) {
            columnas.push(this);
        }
    }
    );



    var functionColumnas = this.get_functionColumns();
    var functionGenerals = this.get_functionsGral();
    var allowMultiSelection = this.get_allowMultiSelection();
    var keyName = this.get_keyName();
    var NameClassAlt = "Alt";

    //debugger;
    if (showData) {
        for (var r = 1; r <= datos.length; r++) {

            var keyValue = eval('datos[r - 1].' + keyName)
            var trow = $("<tr>");
            trow.attr("keyValue", keyValue);
            trow.addClass("tdUnselected");





            if (r % 2) {
                NameClassAlt = "";
            }
            else {
                NameClassAlt = "Alt";
            }



            // Si existe configuro la columna de Edicion al inicio
            var functionColumnEdit = GetColumnFunction(TypeColumnRowEnum["Edit"]);
            if (functionColumnEdit != undefined) {
                if ($("#" + IdTable + " .tdEdit").get().length > 0) {

                    var clssName = "tdEdit" + NameClassAlt;
                    img = $('<img title="' + $(functionColumnEdit).attr("Text") + '" id="imgEdit" src="url(' + this.refEditImg + ')" />');
                    img.data("keyValue", keyValue);
                    img.data("Type", TypeColumnRowEnum.Edit);
                    $("<td>")
                        .append(img)
                        .addClass(clssName).appendTo(trow);
                }

            }

            //debugger;
            // Si existe configuro la columna Custom al inicio
            var functionColumnCustom = GetColumnFunction(TypeColumnRowEnum["Custom"]);
            if (functionColumnCustom != undefined) {

                if ($("#" + IdTable + " .tdCustom").get().length > 0) {

                    var clssName = "tdEdit" + NameClassAlt;

                    if (keyValue != undefined && parseInt(keyValue) > 0) {
                        img = $('<img title="' + $(functionColumnCustom).attr("Text") + '" id="imgCustom" src="' + $(functionColumnCustom).attr("ImgUrl") + '" />');
                        img.data("keyValue", keyValue);
                        img.data("Type", TypeColumnRowEnum.Custom);
                        $("<td>")
                        .append(img)
                        .addClass(clssName).appendTo(trow);
                    }
                    else {

                        $("<td>")
                        .append("&nbsp;")
                        .addClass(clssName).appendTo(trow);
                    }
                }

            }



            //debugger;



            for (var c = 1; c <= columnas.length; c++) {

                if (columnas[c - 1].Display) {

                    var td = $("<td>");
                    var cellText = eval('datos[r - 1].' + columnas[c - 1].DataFieldName);
                    var toolTipString = columnas[c - 1].DataFieldToolTip != null ? eval('datos[r - 1].' + columnas[c - 1].DataFieldToolTip) : "";
                    var clssName = "tdSimple" + NameClassAlt;
                    var align = AlignEnum[columnas[c - 1].Align];
                    var imgUrl = columnas[c - 1].ImgUrl;
                    var dataType = DataTypeEnum[columnas[c - 1].DataType];

                    /// tipo de dato Imagen, es decir la columna tiene que mostrar una imagen.
                    if (dataType == DataTypeEnum[4]) {

                        img = $('<img style="cursor:pointer;" width="' + columnas[c - 1].Width.Value + '"  Height="' + columnas[c - 1].Height.Value + '" id="img' + columnas[c - 1].DataFieldName + '" src="' + cellText + '" />');
                        img.data("keyValue", keyValue);
                        img.data("onClientClick", columnas[c - 1].onClientClick);
                        td
                        .append(img)
                        .css("text-align", align)
                        .css("vertical-align", "middle")
                        .data("DataFieldName", columnas[c - 1].DataFieldName)
                        .data("AllowClientChange", columnas[c - 1].AllowClientChange)
                        .addClass(clssName).appendTo(trow);
                    }
                    /// Muestra una imagen en lugar del texto, y poner el texto como ayuda.
                    else if (imgUrl != null) {

                        img = $('<img title="' + cellText + '" style="cursor:pointer" id="img' + columnas[c - 1].DataFieldName + '" src="' + imgUrl + '" />');
                        img.data("keyValue", keyValue);
                        img.data("onClientClick", columnas[c - 1].onClientClick);
                        td
                        .append(img)
                        .css("text-align", align)
                        .data("DataFieldName", columnas[c - 1].DataFieldName)
                        .data("AllowClientChange", columnas[c - 1].AllowClientChange)
                        .addClass(clssName).appendTo(trow);

                    }
                    /// Si el dato es de tipo Fecha
                    else if (cellText != null &&
                    (Object.getType(eval('datos[r - 1].' + columnas[c - 1].DataFieldName)).getName() == "Date"
                    || dataType == DataTypeEnum[1])) {

                        td.append(cellText.format("dd/MM/yyyy"))
                        .css("text-align", align)
                        .data("DataFieldName", columnas[c - 1].DataFieldName)
                        .data("AllowClientChange", columnas[c - 1].AllowClientChange)
                        .addClass(clssName)
                        .appendTo(trow);

                    }
                    /// Si el dato es de tipo Decimal
                    else if (cellText != null && dataType == DataTypeEnum[2]) {

                        td.append(parseFloat(cellText).toFixed(2))
                        .css("text-align", align)
                        .data("DataFieldName", columnas[c - 1].DataFieldName)
                        .data("AllowClientChange", columnas[c - 1].AllowClientChange)
                        .addClass(clssName)
                        .appendTo(trow);

                    }
                    /// Si el dato es de tipo Integer
                    else if (cellText != null && dataType == DataTypeEnum[3]) {

                        td.append(parseInt(cellText))
                        .css("text-align", align)
                        .data("DataFieldName", columnas[c - 1].DataFieldName)
                        .data("AllowClientChange", columnas[c - 1].AllowClientChange)
                        .addClass(clssName)
                        .appendTo(trow);

                    }
                    /// Si el dato es de tipo String
                    else if (cellText == null || Object.getType(eval('datos[r - 1].' + columnas[c - 1].DataFieldName)).getName() != "Boolean"
                || dataType == DataTypeEnum[0]) {


                        if (cellText == null || cellText == "")
                            cellText = "&nbsp;";

                        if (columnas[c - 1].Capitalice)
                            cellText = ToCapitalize(cellText);

                        td.append(cellText)
                        .css("text-align", align)
                        .data("DataFieldName", columnas[c - 1].DataFieldName)
                        .data("AllowClientChange", columnas[c - 1].AllowClientChange)
                        .addClass(clssName)
                        .appendTo(trow);
                    }
                    /// Si el dato es de tipo bool
                    else {
                        var isChecked = "";
                        if (cellText == true) {
                            isChecked = "checked='true'";
                        }

                        var isEnabled = "";
                        if (!columnas[c - 1].Enabled) {
                            isEnabled = "disabled = 'disabled'";
                        }


                        var onClientClick = columnas[c - 1].onClientClick;

                        chk = $('<input type="checkbox" ' + isChecked + ' ' + isEnabled + '  />');
                        chk.data("keyValue", keyValue);
                        chk.data("ColumnName", columnas[c - 1].DataFieldName);
                        chk.click(function () {

                            var keyValue = $(this).data("keyValue");
                            var itemData = null;
                            jQuery.each(datos, function () {

                                if ($(this).attr(keyName) == keyValue) {
                                    itemData = this;
                                    return false;
                                }
                            });

                            var valorActual = eval('itemData.' + $(this).data("ColumnName"));
                            eval('itemData.' + $(this).data("ColumnName") + '=' + !valorActual + '');


                            var functionToCall = eval(onClientClick);
                            if (functionToCall != undefined)
                                functionToCall(this, $(this).data("keyValue"), !valorActual);


                            if (!e) var e = window.event;
                            e.cancelBubble = true;
                            if (e.stopPropagation) e.stopPropagation();

                        });


                        td.append(chk)
                        .css("text-align", align)
                        .data("DataFieldName", columnas[c - 1].DataFieldName)
                        .data("AllowClientChange", columnas[c - 1].AllowClientChange)
                        .addClass(clssName)
                        .appendTo(trow);

                    }

                    //Este seria el codigo a utilizar para asociar y mostrar una ayuda
                    //sobre la columna de datos
                    if (toolTipString != "") {
                        //debugger;
                        var toolTip = this._toolTipCtr;
                        var nameColumn = columnas[c - 1].DataFieldName;
                        var stringToolTip = eval('datos[r - 1].' + columnas[c - 1].DataFieldToolTip);
                        $(td[0].childNodes[0]).attr("tooltip", stringToolTip);

                        $(td[0].childNodes[0]).mouseover(function () {
                            toolTip.set_targetControl(this);
                            toolTip.set_text($(this).attr("tooltip"));
                            toolTip.show();
                        });
                    }
                }
            }

            //debugger;
            // Si existe configuro la columna de Eliminacion al final
            var functionColumnDelete = GetColumnFunction(TypeColumnRowEnum["Delete"]);
            if (functionColumnDelete != undefined) {
                if ($("#" + IdTable + " .tdEliminar").get().length > 0) {

                    var clssName = "tdEliminar" + NameClassAlt;
                    img = $('<img title="' + $(functionColumnDelete).attr("Text") + '" id="imgDelete" src="url(' + this.refDeleteImg + ')" />');
                    img.data("keyValue", keyValue);
                    img.data("Type", TypeColumnRowEnum.Delete);
                    $("<td>")
                        .append(img)
                        .addClass(clssName).appendTo(trow);
                }

            }


            trow.appendTo(tbody);
        }

    }

    //debugger;
    var refAddImg = this.refAddImg;
    var refExcelImg = this.refExcelImg;
    var callBaxkExcelId = this.get_callbackID();
    var functionColumnAdd = GetFunctionGeneral(TypeFuntionsGralEnum["Add"])[0];
    var functionColumnExcel = GetFunctionGeneral(TypeFuntionsGralEnum["Excel"])[0];
    var functionColumnCustoms = GetFunctionGeneral(TypeFuntionsGralEnum["Custom"]);

    $("#" + IdTable + " .tdFunctionAdd").each(function () {

        /// Limpio el boton antes creado
        $(this).empty();

        /// Div Contenedor
        divContainer = $('<div>');

        if (functionColumnAdd != undefined) {
            // botones para Funcion ADD
            div = $('<div class="tdAdd" style="cursor: pointer;width:190px;display: inline;padding-right:10px" >');
            div.data("keyValue", "");
            div.data("Type", TypeFuntionsGralEnum.Add);
            div.data("Index", 0);
            img = $('<img id="imgAdd" src="url(' + refAddImg + ')" />');
            text = $('<span style="padding-left:2px;color:White;position:relative;top:-3px">' + $(functionColumnAdd).attr("Text") + "</span>");
            $(div).append(img);
            $(div).append(text);

            // Agrego div add al div general
            $(divContainer).append(div);
        }

        if (functionColumnExcel != undefined) {
            // botones para Funcion Excel
            divExcel = $('<div class="tdExcel" style="cursor: pointer;width:190px;display: inline;padding-right:10px" >');
            divExcel.data("Index", 0);

            imgExcel = $('<img id="imgExcel" src="url(' + refExcelImg + ')" />');
            textExcel = $('<span style="padding-left:2px;color:White;position:relative;top:-3px">Exportar</span>');
            $(divExcel).append(imgExcel);
            $(divExcel).append(textExcel);

            // Agrego div excel al div general
            $(divContainer).append(divExcel);
        }


        for (var i = 0; i < functionColumnCustoms.length; i++) {

            // botones Customs
            divCustom = $('<div class="tdAdd" style="cursor: pointer;width:190px;display: inline;padding-right:10px" >');
            divCustom.data("keyValue", "");
            divCustom.data("Type", TypeFuntionsGralEnum.Custom);
            divCustom.data("Index", i);

            imgCustom = $('<img id="imgCustom" src="' + $(functionColumnCustoms[i]).attr("ImgUrl") + '" />');

            textCustom = $('<span style="padding-left:2px;color:White;position:relative;top:-3px">' + $(functionColumnCustoms[i]).attr("Text") + '</span>');

            $(divCustom).append(imgCustom);
            $(divCustom).append(textCustom);

            // Agrego div excel al div general
            $(divContainer).append(divCustom);


        }

        $(this).append(divContainer);
    });

    //debugger;
    /// Busco las imagenes en el body, imagen por cada row, y las subscribo el evento click
    $("#" + IdTable + " tbody img").click(function () {
        var functionColumn = GetColumnFunction($(this).data("Type"));

        if (functionColumn != undefined) {
            var functionToCall = eval(functionColumn.ClickFunction);
            if (functionToCall != undefined)
                functionToCall($(this)[0], $(this).data("keyValue"));
        }
        else if ($(this).data("onClientClick") != "") {
            var functionToCall = eval($(this).data("onClientClick"));
            if (functionToCall != undefined)
                functionToCall($(this)[0], $(this).data("keyValue"));
        }
    });


    /// Busco la imagen Add en el head o foot, y las subscribo el evento click
    $("#" + IdTable + " .tdAdd").click(function () {
        //debugger;
        var functionColumn = GetFunctionGeneral($(this).data("Type"), $(this).data("Index"))[0];
        var functionToCall = eval(functionColumn.ClickFunction);
        if (functionToCall != undefined) {
            if ($(this).data("keyValue") != "") {
                functionToCall($(this)[0], $(this).data("keyValue"));
            }
            else {
                functionToCall($(this)[0], GetKeyValueSelected());
            }

        }
    });


    /// Busco la imagen Excel en el head o foot, y las subscribo el evento click del servidor
    $("#" + IdTable + " .tdExcel").click(function () {
        $ClientControlGrid.prototype._raiseServerEvent("ExportToExcel", callBaxkExcelId);
    });


    if (this._allowRowSelection) {
        //debugger;
        var onClientFunctionRowClicked = this.onClientFunctionRowClicked;
        $("#" + IdTable + " tbody tr")
            .click(function () {
                if (event.srcElement.nodeName != "DIV" && (event.srcElement.id != "imgCustom" && event.srcElement.id != "imgEdit" && event.srcElement.id != "imgDelete")) {
                    if ($(this).attr('class').indexOf("trSelected") >= 0) {
                        $(this).removeClass("trSelected");
                    }
                    else {

                        if (allowMultiSelection != true) {
                            $("#" + IdTable + "  tr").removeClass("trSelected");
                        }

                        $(this).addClass("trSelected");

                        /// Disparo el evento de la fila seleccionada
                        if (onClientFunctionRowClicked != "") {

                            var functionToCall = eval(onClientFunctionRowClicked);
                            if (functionToCall != undefined)
                                functionToCall(this, $(this).attr('keyValue'));
                        }
                    }
                }
                else if (event.srcElement.nodeName != "DIV" && (event.srcElement.id != "imgCustom" && event.srcElement.id == "imgEdit" || event.srcElement.id == "imgDelete")) {

                    if (allowMultiSelection != true) {
                        $("#" + IdTable + "  tr").removeClass("trSelected");
                    }

                    $(this).addClass("trSelected");

                    /// Disparo el evento de la fila seleccionada
                    if (onClientFunctionRowClicked != "") {

                        var functionToCall = eval(onClientFunctionRowClicked);
                        if (functionToCall != undefined)
                            functionToCall(this, $(this).attr('keyValue'));
                    }
                }
            });
    }


    function GetColumnFunction(value) {
        var functionColumn;

        $(functionColumnas).each(function () {
            if ($(this).attr("Type") == value) {
                functionColumn = this;
            }
        });

        return functionColumn;
    }

    function GetFunctionGeneral(value, index) {
        var functionColumn = new Array();
        var cont = 0;

        $(functionGenerals).each(function () {
            if ($(this).attr("Type") == value) {

                if (index == undefined) {
                    functionColumn[cont] = this;
                }
                else {
                    if (cont == index) {
                        functionColumn[0] = this;
                    }
                }

                cont++;
            }
        });

        return functionColumn;
    }

    function ToCapitalize(value) {
        value = value.toLowerCase();
        return value.replace(/(^|\s)([a-z])/g, function (m, p1, p2) { return p1 + p2.toUpperCase(); });
    }

    function GetKeyValueSelected() {
        var KeyValueSelected = "";

        $("#" + IdTable + " .trSelected").each(function () {

            var keyValue = $(this).attr('keyValue');
            jQuery.each(datos, function () {
                if ($(this).attr(keyName) == keyValue) {
                    KeyValueSelected = keyValue;
                    return false;
                }
            });

        });

        return KeyValueSelected;

    }


    /// Oculto los elementos del Waiting
    var divBlock = "DivBlock" + this._element.id;
    var tblBlock = "tblBlock" + this._element.id;
    $("#" + divBlock).remove();
    $("#" + tblBlock).remove();
}


$ClientControlGrid.prototype._addHandlers = function () {
    var metodoResponsable = this._onChangePageIndex;
    var handdler = this;


    if (this._allowPaging) {
        $addHandlers(this._imgFirst, {
            click: Function.createDelegate(handdler, metodoResponsable)
        });

        $addHandlers(this._imgLast, {
            click: Function.createDelegate(handdler, metodoResponsable)
        });


        $addHandlers(this._imgNext, {
            click: Function.createDelegate(handdler, metodoResponsable)
        });


        $addHandlers(this._imgPrev, {
            click: Function.createDelegate(handdler, metodoResponsable)
        });

        //debugger;
        var IdTable = this._tblDynamic[0].id;
        $("#" + IdTable + " .btnChangePageIndex").each(function () {
            $addHandlers(this, {
                click: Function.createDelegate(handdler, metodoResponsable)
            });

        });
    }

}

$ClientControlGrid.prototype._clearHandlers = function () {

    $clearHandlers(this._imgFirst);
    $clearHandlers(this._imgLast);
    $clearHandlers(this._imgNext);
    $clearHandlers(this._imgPrev);

}

$ClientControlGrid.prototype.add_ChangePageIndex = function (handler) {
    this.get_events().addHandler('ChangePageIndex', handler);
}

$ClientControlGrid.prototype.remove_ChangePageIndex = function (handler) {
    this.get_events().removeHandler('ChangePageIndex', handler);
}




// -------------------------------------- Server Side Events --------------------------------------

$ClientControlGrid.prototype._raiseServerEvent = function (eventName, controlName, eventArgs) {
    //debugger;
    var eventArgsSerialised = Sys.Serialization.JavaScriptSerializer.serialize(eventArgs);
    var args = String.format("{0};{1}", eventName, eventArgsSerialised);
    var id = controlName;

    /// Llamada para ejecutar el evento
    __doPostBack(id, args);

}






// -------------------------------------- HTML Events --------------------------------------


$ClientControlGrid.prototype._onChangePageIndex = function (e) {
    //debugger;
    var countPageGroup = this._CurrentGroupPage;
    var IdTable = this._tblDynamic[0].id;
    var virtualCountPage = 0;


    /// Ajusto la cantidad de paginas de teniendo en cuesta la division
    var res = this._virtualCount % this._pageSize;
    if (res > 0)
        virtualCountPage = parseInt(this._virtualCount / this._pageSize) + 1;
    else
        virtualCountPage = parseInt(this._virtualCount / this._pageSize);

    var tempCurrentPage = this._currentPageIndex;

    if (virtualCountPage <= 1) {
        $('#tblPaging').css("display", "none");
    }
    else {
        $('#tblPaging').css("display", "block");
        //debugger;
        if (e.target.id == "theFirst") {
            countPageGroup = 0;
            var lastPage = DibujarPageButtons();
            this._CurrentGroupPage = lastPage;
            this._currentPageIndex = 0;

        }
        else if (e.target.id == "theLast") {

            var res = this._virtualCount % this._pageSize;
            if (res > 0)
                this._currentPageIndex = parseInt(this._virtualCount / this._pageSize);
            else
                this._currentPageIndex = parseInt(this._virtualCount / this._pageSize) - 1;


            var ultimogrupo = (parseInt(this._virtualCount / 10) * 10) - 10;
            /// Evaluo si es el primer grupo
            ultimogrupo = ultimogrupo == 0 ? 10 : ultimogrupo;
            if (ultimogrupo != countPageGroup) {
                countPageGroup = (parseInt(this._virtualCount / 10) * 10) - 10;
                var lastPage = DibujarPageButtons();
                this._CurrentGroupPage = lastPage - 1;
                this._currentPageIndex = lastPage - 2; // 10: Cantidad MAXIMA de botones visibles + 1 porque comienza a contar de 0
            }


        }
        else if (e.target.id == "theNext") {
            if (this._currentPageIndex < virtualCountPage - 1) {
                this._currentPageIndex++;

                /// significa que se paso al otro grupo de paginas.
                if (this._currentPageIndex == this._CurrentGroupPage) {
                    var lastPage = DibujarPageButtons();
                    this._CurrentGroupPage = lastPage - 1;
                    this._currentPageIndex = lastPage - 11; // 10: Cantidad MAXIMA de botones visibles + 1 porque comienza a contar de 0

                }
            }
        }
        else if (e.target.id == "thePrev") {
            if (this._currentPageIndex >= 1) {
                this._currentPageIndex--;

                /// significa que se paso al grupo anterior de paginas.
                if (this._currentPageIndex == this._CurrentGroupPage - 11) {
                    countPageGroup = this._CurrentGroupPage - 20;
                    var lastPage = DibujarPageButtons();
                    this._CurrentGroupPage = lastPage - 1;
                    this._currentPageIndex = lastPage - 2; // 10: Cantidad MAXIMA de botones visibles + 1 porque comienza a contar de 0
                }
            }

        }
        else {
            if ($(e.target).attr('PageIndex') != "...") {
                this._currentPageIndex = parseInt($(e.target).attr('PageIndex'));
            }
            else {

                var lastPage = DibujarPageButtons();
                this._CurrentGroupPage = lastPage - 1;
                this._currentPageIndex = lastPage - 11; // 10: Cantidad MAXIMA de botones visibles + 1 porque comienza a contar de 0

            }
        }

        /// Cambio la imagen al boton de la pagina actual
        var refSelected = this.refImgPageSelected;
        var currentPage = this._currentPageIndex + 1;
        $("#" + IdTable + " .btnChangePageIndex").each(function () {

            if ($(this)[0].innerText == currentPage) {
                $(this)[0].style.backgroundImage = "url('" + refSelected + "')";
            }
            else {
                $(this)[0].style.backgroundImage = "";
            }
        });

    }

    if (tempCurrentPage != this._currentPageIndex) {

        var handler = this.get_events().getHandler('ChangePageIndex');

        // Check if there is any subscriber of this event
        if (handler != null) {
            handler(this, this._currentPageIndex, this._pageSize);
        }
    }


    function DibujarPageButtons() {

        $("#" + IdTable + " .btnChangePageIndex").each(function () {
            countPageGroup++;
            if ($(this)[0].innerText != "...") {
                if (countPageGroup <= virtualCountPage) {
                    $(this)[0].innerText = countPageGroup + '';
                    $(this)[0].parentElement.style.display = "block";
                    $(this).attr("PageIndex", countPageGroup - 1);
                }
                else {
                    $(this)[0].parentElement.style.display = "none";
                }
            }
            else {
                if (countPageGroup <= virtualCountPage) {
                    $(this)[0].parentElement.style.display = "block";
                }
                else {
                    $(this)[0].parentElement.style.display = "none";
                }
            }

        });

        return countPageGroup;

    }

}

// -------------------------------------- Client side Properties --------------------------------------



// -------------------------------------- Control methods --------------------------------------


/// Este metodo se utiliza para redibujar los botones del paginado
/// desde el lado del cliente sin tener que ir al servidor.
$ClientControlGrid.prototype.ChangeClientVirtualCount = function (virtualCount) {

    this._virtualCount = virtualCount;
    var IdTable = this._tblDynamic[0].id;
    var virtualCountPage = 0;
    var countPageGroup = 0;


    /// Ajusto la cantidad de paginas de teniendo en cuesta la division
    var res = this._virtualCount % this._pageSize;
    if (res > 0)
        virtualCountPage = parseInt(this._virtualCount / this._pageSize) + 1;
    else
        virtualCountPage = parseInt(this._virtualCount / this._pageSize);

    if (virtualCountPage <= 1) {
        $('#tblPaging').css("display", "none");
    }
    else {
        $('#tblPaging').css("display", "block");

        var lastPage = DibujarPageButtons();
        this._CurrentGroupPage = lastPage - 1;
        this._currentPageIndex = 0;


        /// Cambio la imagen al boton de la pagina actual
        var refSelected = this.refImgPageSelected;
        var currentPage = this._currentPageIndex + 1;
        $("#" + IdTable + " .btnChangePageIndex").each(function () {

            if ($(this)[0].innerText == currentPage) {
                $(this)[0].style.backgroundImage = "url('" + refSelected + "')";
            }
            else {
                $(this)[0].style.backgroundImage = "";
            }
        });



        function DibujarPageButtons() {

            $("#" + IdTable + " .btnChangePageIndex").each(function () {
                countPageGroup++;
                if ($(this)[0].innerText != "...") {
                    if (countPageGroup <= virtualCountPage) {
                        $(this)[0].innerText = countPageGroup + '';
                        $(this)[0].parentElement.style.display = "block";
                        $(this).attr("PageIndex", countPageGroup - 1);
                    }
                    else {
                        $(this)[0].parentElement.style.display = "none";
                    }
                }
                else {
                    if (countPageGroup <= virtualCountPage) {
                        $(this)[0].parentElement.style.display = "block";
                    }
                    else {
                        $(this)[0].parentElement.style.display = "none";
                    }
                }

            });

            return countPageGroup;

        }
    }
}

$ClientControlGrid.prototype.SelectAll = function (selected) {

    var IdTable = this._tblDynamic[0].id;
    $('#' + IdTable + " input:checkbox").each(function () {
        $(this).attr("checked", selected);
    });


}

$ClientControlGrid.prototype.ShowWaiting = function (mesage) {
    //debugger;

    /// Limpio los datos cargados
    if (this.get_dataSource() != null)
        this._cleanTable();


    var divBlock = "DivBlock" + this._element.id;
    var tblBlock = "tblBlock" + this._element.id;

    var refWaitingImg = this.refWaitingImg;

    if ($("#" + divBlock).length > 0) {
        $("#" + divBlock).show();
        $("#" + tblBlock).show();
    }
    else {
        _creatediv($("#" + this._element.id));
    }


    function _creatediv(jq_Content) {

        var width = jq_Content.width();
        var height = jq_Content.height();
        var left = jq_Content.position().left;
        var top = jq_Content.position().top;

        if (width > 0) {

            var newdiv = document.createElement('div');
            newdiv.setAttribute('id', divBlock);

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
            newdiv.style.background = "black";
            newdiv.style.zIndex = 980000000;


            if (typeof (newdiv.style.filter) != "undefined") {
                newdiv.style.filter = "alpha(opacity=40);";
            }
            else if (typeof (newdiv.style.MozOpacity) != "undefined") {
                newdiv.style.MozOpacity = 1 / 2;
            }


            var tbl = $('<table id="' + tblBlock + '" cellpadding="0" cellspacing="0" style="width: ' + width + 'px;z-index:988000000;height:50px" border="0">')
            tbl.css("position", "absolute");
            tbl.css("left", left);
            tbl.css("top", top + (height / 2) - 20);
            tbl.css("textAlign", "center");
            tbl.css("verticalAlign", "middle");


            var tr = $("<TR>")
            var td = $("<TD>"); //.css("paddingTop", (height / 2));
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

$ClientControlGrid.prototype.changeHeaderText = function (dataFieldName, NewText) {
    var IdTable = this._tblDynamic[0].id;
    $('#' + IdTable + " .Theader").each(function () {
        if ($(this).attr('dataFieldName') == dataFieldName)
            $(this).text(NewText);
    });
}

$ClientControlGrid.prototype.changeDisplayState = function (dataFieldName, state) {
    //debugger;
    var IdTable = this._tblDynamic[0].id;
    $('#' + IdTable + " .Theader").each(function () {
        if ($(this).attr('dataFieldName') == dataFieldName)
            $(this).css("display", state);
    });

    $("#" + IdTable + " td").filter(function () {
        return $(this).data('DataFieldName') == dataFieldName;
    }).each(function () {
        $(this).css("display", state);
    }
      );

}

$ClientControlGrid.prototype.filterAll = function () {
    var IdTable = this._tblDynamic[0].id;
    var tbody;
    /// Recupero el body de la tabla
    /// para insertar los elementos ahi.
    $('#' + IdTable).each(function () {
        if (!this.tBodies || !this.tBodies.length) {
            return;
        }
        else {
            tbody = this.tBodies[0];
        }


    });


    $(tbody + " tr").each(function () {
        $(this).hide();
    });

}

$ClientControlGrid.prototype.get_ItemsDataByFilter = function (text, columnName) {
    //debugger;
    var itemsData = new Array();
    var dataSource = this.get_dataSource();
    $(dataSource).filter(function () {
        if ($(this).attr(columnName) == text) {
            itemsData.push(this);
        }

    });

    return itemsData;
}


$ClientControlGrid.prototype.filterByText = function (text, columnName) {
    //debugger;
    var IdTable = this._tblDynamic[0].id;

    if (this.showDataOnInit) {


        $("#" + IdTable + " td").filter(function () {
            return $(this).data('DataFieldName') == columnName && $(this).text().toUpperCase().indexOf(text.toUpperCase()) < 0;
        }).each(function () {
            $(this).parent().hide();
        }
        );

        $("#" + IdTable + " td").filter(function () {
            return $(this).data('DataFieldName') == columnName && $(this).text().toUpperCase().indexOf(text.toUpperCase()) >= 0;
        }).each(function () {
            $(this).parent().show();
        }
        );
    } else {
        var itemsData = new Array();
        var dataSource = this.get_dataSource();
        //debugger;
        $(dataSource).each(function () {

            //if ($(this).attr(columnName) == text) {
            if ($(this).attr(columnName).toUpperCase().indexOf(text.toUpperCase()) >= 0) {
                itemsData.push(this);
                if (itemsData.length == 20)
                    return false;
            }

        }
        );

        this.ShowWaiting("Cargando Datos...");
        var obj = this;
        var idtimeout = window.setTimeout(function () {
            obj._createTable(itemsData, true);
            window.clearTimeout(idtimeout);
        }
        , 100);

    }
}



$ClientControlGrid.prototype.updateRow = function (ItemData) {
    //debugger;

    var IdTable = this._tblDynamic[0].id;
    var keyName = this.get_keyName();
    var key = eval("ItemData." + keyName);

    var rowHTML = $("#" + IdTable + " tbody tr[keyValue='" + key + "']")[0];

    $(rowHTML).find("TD").each(function () {

        if ($(this).data("AllowClientChange") == true) {
            var cellFieldName = $(this).data("DataFieldName");
            if (cellFieldName != undefined) {
                var valor = eval("ItemData." + cellFieldName);
                if (valor != undefined)
                    this.innerText = valor;
                else {
                    this.innerText = " ";
                }
            }
        }

    }, null);

}


$ClientControlGrid.prototype.get_ItemDataByKey = function (key) {

    var IdTable = this._tblDynamic[0].id;
    var dataSource = this.get_dataSource();
    var keyName = this.get_keyName();
    var itemData = null;


    var rowHTML = $("#" + IdTable + " tbody tr[keyValue='" + key + "']")[0];


    jQuery.each(dataSource, function () {

        if ($(this).attr(keyName) == key) {
            itemData = this;
            return false;
        }
    });

    return itemData;

}

$ClientControlGrid.prototype.get_KeyValueSelected = function () {
    var IdTable = this._tblDynamic[0].id;
    var dataSource = this.get_dataSource();
    var keyName = this.get_keyName();
    var KeyValueSelected;

    $("#" + IdTable + " .trSelected").each(function () {

        var keyValue = $(this).attr('keyValue');
        jQuery.each(dataSource, function () {
            if ($(this).attr(keyName) == keyValue) {
                KeyValueSelected = keyValue;
                return false;
            }
        });

    });

    return KeyValueSelected;
}

$ClientControlGrid.prototype.get_ItemsDataSelected = function () {

    var IdTable = this._tblDynamic[0].id;
    var dataSource = this.get_dataSource();
    var keyName = this.get_keyName();
    var itemsData = new Array();

    $("#" + IdTable + " .trSelected").each(function () {

        var keyValue = $(this).attr('keyValue');
        jQuery.each(dataSource, function () {
            if ($(this).attr(keyName) == keyValue) {
                itemsData.push(this);
                return false;
            }
        });

    });


    return itemsData;

}

$ClientControlGrid.prototype.get_ItemsData = function () {

    return this.get_dataSource();
}



$ClientControlGrid.prototype.initEdit = function (contentControl, key) {
    //debugger;
    var item = this.get_ItemDataByKey(key)
    var columnas = this.get_columns();

    for (var c = 1; c <= columnas.length; c++) {

        if (columnas[c - 1].NameControlManger != null) {

            var valueCombo;
            var value = $(item).attr(columnas[c - 1].DataFieldName);
            if (value == null) { value = "" };

            if (columnas[c - 1].DataFieldNameValueCombo != null) {
                valueCombo = $(item).attr(columnas[c - 1].DataFieldNameValueCombo);
            }

            try {
                SetValueToControl(contentControl, value, columnas[c - 1].NameControlManger, valueCombo);
            }
            catch (Error) {
                alert(Error);
            }
        }
    }


    function SetValueToControl(contentControl, value, controlName, valueCombo) {
        //var crt = $("#" + $(contentControl)[0].get_id() + "_" + controlName);
        var crt = $("[id$='" + controlName + "']");
        if (crt != null) {
            /// Busco para saber si es un Telerik
            var objcrt = $find(crt[0].id);
            if (objcrt != null) {
                if (Telerik.Web.UI.RadComboBox != undefined && objcrt instanceof Telerik.Web.UI.RadComboBox) {

                    if (objcrt.get_items().get_count() > 0) {
                        var item = objcrt.findItemByValue(valueCombo);
                        if (item != null) {
                            item.select();
                        }
                    }
                    else {

                        if (value != "")
                            objcrt.set_text(value);

                        if (valueCombo != null)
                            objcrt.set_value(valueCombo);
                    }
                }
                else if (Telerik.Web.UI.RadDatePicker != undefined && objcrt instanceof Telerik.Web.UI.RadDatePicker) {
                    if (value != "") {
                        //                        value = value.format("dd/MM/yyyy");
                        //                        var dia = value.substr(0, 2);
                        //                        var mes = parseInt(value.substr(3, 2)) - 1 + '';
                        //                        var año = value.substr(6);
                        //                        var Fecha = new Date(año, mes, dia);
                        //                        objcrt.set_selectedDate(Fecha);
                        objcrt.set_selectedDate(value);

                    }
                }
                else {
                    if (value != "")
                        objcrt.set_value(value);
                }

            }
            else {
                objcrt = $get(crt[0].id);
                if (objcrt.type == "text") {
                    if (value != "")
                        objcrt.innerText = value;
                }
                else if (objcrt.type == "checkbox") {
                    objcrt.checked = value;
                }
                else if (objcrt.type == "textarea") {
                    objcrt.innerText = value;
                }
                else if (objcrt.type == undefined && objcrt.tagName == "IMG") {
                    objcrt.src = value;
                }


            }
        }


    }

}


$ClientControlGrid.prototype.getValuesEdit = function (contentControl) {
    //debugger;
    var columnas = this.get_columns();
    var objResult = new Object();

    for (var c = 1; c <= columnas.length; c++) {

        if (columnas[c - 1].NameControlManger != null) {
            GetValueControlToObject(contentControl, columnas[c - 1].NameControlManger, objResult, columnas[c - 1].DataFieldName, columnas[c - 1].DataFieldNameValueCombo);
        }
    }

    return objResult;


    function GetValueControlToObject(contentControl, controlName, objResult, NameProperty, NamePropertyCombo) {
        //var crt = $("#" + $(contentControl)[0].get_id() + "_" + controlName);
        var crt = $("[id$='" + controlName + "']");
        if (crt != null) {
            /// Busco para saber si es un Telerik
            var objcrt = $find(crt[0].id);
            if (objcrt != null) {
                if (Telerik.Web.UI.RadComboBox != undefined && objcrt instanceof Telerik.Web.UI.RadComboBox) {

                    if (objcrt.get_selectedItem() != null) {
                        eval('objResult.' + NameProperty + "='" + objcrt.get_text() + "';");
                        eval('objResult.' + NamePropertyCombo + "='" + objcrt.get_value() + "';");
                    }
                    else if (objcrt.get_value() != "") {
                        eval('objResult.' + NameProperty + "='" + objcrt.get_text() + "';");
                        eval('objResult.' + NamePropertyCombo + "='" + objcrt.get_value() + "';");
                    }
                    else {
                        eval('objResult.' + NameProperty + "= null;");
                        eval('objResult.' + NamePropertyCombo + "= null;");
                    }

                }
                else if (Telerik.Web.UI.RadDatePicker != undefined && objcrt instanceof Telerik.Web.UI.RadDatePicker) {
                    if (objcrt.get_selectedDate() != null) {
                        eval('objResult.' + NameProperty + "='" + objcrt.get_selectedDate().format("dd/MM/yyyy") + "';");
                    }
                    else {
                        eval('objResult.' + NameProperty + "= null;");
                    }
                }
                else {
                    eval('objResult.' + NameProperty + "='" + objcrt.get_value() + "';");
                }

            }
            else {
                objcrt = $get(crt[0].id);
                if (objcrt.type == "text") {
                    eval('objResult.' + NameProperty + "='" + objcrt.value + "';");
                }
                else if (objcrt.type == "checkbox") {
                    eval('objResult.' + NameProperty + "=" + objcrt.checked + ";");
                }
                else if (objcrt.type == "textarea") {
                    eval('objResult.' + NameProperty + "='" + objcrt.value + "';");
                }
            }
        }


    }

}


$ClientControlGrid.registerClass('ControlsAjaxNotti.ClientControlGrid', Sys.UI.Control);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();