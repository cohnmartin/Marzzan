var CurrentControl;
var xmlDoc = null;



function loadIndex() { // load indexfile
    var file = "Productos.xml";
//    xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
//    xmlDoc.async = false;
//    xmlDoc.load(file);

    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    
    xmlhttp.open("GET", file, false);
    xmlhttp.send();
    xmlDoc = xmlhttp.responseXML;
}


function showRadWindow() {
    var codigos = event.srcElement.getAttribute("Codigos").toString();
    var descripciones = event.srcElement.getAttribute("Descripciones").toString();
    var precios = event.srcElement.getAttribute("Precios").toString();
    var id = event.srcElement.getAttribute("IdProducto").toString();
    var ProductName = event.srcElement.innerText;

    var oWnd = radopen('CargaProducto.aspx?Codigos=' + codigos + "&descripciones=" + descripciones + "&precios=" + precios + "&id=" + id + "&ProductName=" + ProductName, 'RadWindow1');
}

function LimpiarTarget(sender, eventArgs) {
    sender.set_targetControlID();
}

function searchIndex(searchText) { // search the index (duh!)

    if (searchText.length > 0) {

        if (xmlDoc == null) {
            var file = "Productos.xml";
//            xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
//            xmlDoc.async = false;
//            xmlDoc.load(file);

            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }

            xmlhttp.open("GET", file, false);
            xmlhttp.send();
            xmlDoc = xmlhttp.responseXML;
        }

        // get the search term from a form field with id 'searchme'
        var searchterm = searchText; //document.getElementById("searchme").value;
        var allitems = xmlDoc.getElementsByTagName("item");
        results = new Array;

        var resultshere = document.getElementById("resultshere");
        resultshere.innerText = "";
        var presentaciones = "";

        for (var i = 0; i < allitems.length; i++) {
            // see if the XML entry matches the search term,
            // and (if so) store it in an array
            var idProducto = allitems[i].getAttributeNode("IdProducto");
            var name = allitems[i].getAttributeNode("padre");

            if (name != null && name.value == searchText) {
                /// esta linea es para leer chrom
                //alert(allitems[i].childNodes[0].nodeValue);

                results.push(allitems[i]);
            }
            else if (idProducto != null && idProducto.value == searchText) {
                presentaciones = allitems[i].getAttributeNode("Presentaciones");
            }

        }
        // send the results to another function that displays them to the user
        showResults(results, searchterm, presentaciones);
    }
    else {

        var resultshere = document.getElementById("resultshere");
        resultshere.innerHTML = "";
        document.getElementById("divSolicitar").style.display = "none";

    }

}

function ShowControl() {

    var toolTip = $find("toolTipPrecios");
    var location = Sys.UI.DomElement.getLocation(event.srcElement);
    var top = document.getElementById("txtControlValor");
    top.value = parseInt(event.srcElement.innerHTML);
    top.style.position = 'absolute';
    top.style.top = location.y + 'px';
    top.style.left = location.x + 'px';


    toolTip.set_targetControlID(top.id);

    if (event.srcElement.getAttribute("maxValue") == "1")
        toolTip.set_text("Valor Unitario: $" + event.srcElement.getAttribute("Precio") + "</br><b>Solo uno por pedido</b>");
    else
        toolTip.set_text("Valor Unitario: $" + event.srcElement.getAttribute("Precio"))

    toolTip.show();

    if (event.srcElement != CurrentControl && CurrentControl != null) {
        NormalizarLinea();
    }

    CurrentControl = event.srcElement;
    ResaltarLinea();

}
function NormalizarLinea() {
    if (CurrentControl != null && CurrentControl.parentElement != null) {
        CurrentControl.parentElement.childNodes[0].style.border = "1px solid white";
        CurrentControl.parentElement.childNodes[0].style.fontWeight = "";
        CurrentControl.parentElement.childNodes[0].style.fontSize = "";
    }
}

function ResaltarLinea() {

    event.srcElement.parentElement.childNodes[0].style.border = "1px solid gray";
    event.srcElement.parentElement.childNodes[0].style.fontWeight = "bold";
    event.srcElement.parentElement.childNodes[0].style.fontSize = "12px";
}

function HideControl() {

    var top = document.getElementById("txtControlValor");
    top.value = "0";
    top.style.position = 'absolute';
    top.style.top = '-1000px';
    top.style.left = '-1000px';
    NormalizarLinea();

    var toolTip = $find("toolTipPrecios");
    toolTip.hide();


}

// The following is just an example of how you
// could handle the search results
function showResults(results, searchterm, presentaciones) {

    if (results.length > 0 && presentaciones != null) {
        // if there are any results, put them in a list inside the "resultshere" div
        CurrentControl = null;
        var Headers = presentaciones.value.split('|');
        var resultshere = document.getElementById("resultshere");
        var tableContainer = document.createElement("table");
        var tableContainerTbody = document.createElement("tbody");
        tableContainer.setAttribute("width", "68%");
        tableContainer.setAttribute("height", "100%");
        tableContainer.setAttribute("id", "tblCargaPedido");
        tableContainerTbody.setAttribute("id", "tblCargaPedido");
        tableContainer.className = "GrillaProductos";
        tableContainer.bgColor = "transparent";

        /// creo la cabecera de la tabla de productos
        var trProductos = document.createElement("tr");
        var tdProductos = document.createElement("td");
        tdProductos.setAttribute("align", "center");
        tdProductos.setAttribute("bgColor", "White");
        tdProductos.innerHTML = "Productos";
        trProductos.appendChild(tdProductos);

        for (var i = 0; i < Headers.length - 1; i++) {
            tdProductos = document.createElement("td");
            tdProductos.innerHTML = Headers[i];
            tdProductos.setAttribute("align", "center");
            tdProductos.setAttribute("bgColor", "White");
            trProductos.appendChild(tdProductos);
        }
        tableContainerTbody.appendChild(trProductos);

        for (var i = 0; i < results.length; i++) {
            var codigoParaOcultar = document.getElementById("HiddenProductosOcultos").value;
            var CurrentIdProducto = results[i].getAttributeNode("IdProducto").value;
            var CodigoIncorporacion0 = document.getElementById("HiddenIncorporacion0").value;


            if (results[i].getAttributeNode("Codigos").value.indexOf(CodigoIncorporacion0) < 0) {

                if (
                (CurrentIdProducto != "2858" && codigoParaOcultar.indexOf(CurrentIdProducto) < 0)
                ||
                (CurrentIdProducto == "2858" && document.getElementById("HiddenPoseeCartuchera").value == "true")) {
                    var row = "";
                    var tdProductos = document.createElement("td");
                    var trProductos = document.createElement("tr");


                    var ie8 = (navigator.userAgent.match(/msie/i) && navigator.userAgent.match(/8/));
                    var colorFondo = "rgba(255, 255, 255, .8)";
                    if (ie8) {
                        colorFondo = "#F9F9F9";
                    }

                    $(tdProductos).css("background-color", colorFondo);
                    
                    var texto = results[i].text;
                    if (texto == undefined)
                        texto = results[i].textContent;
                    
                    
                    var item = document.createElement("span");
                    item.id = "span1_" + texto;
                    item.innerHTML = texto;

                    item.setAttribute("height", "22px");
                    item.setAttribute("Codigos", results[i].getAttributeNode("Codigos").value);
                    item.setAttribute("Descripciones", results[i].getAttributeNode("Descripciones").value);
                    item.setAttribute("Precios", results[i].getAttributeNode("Precios").value);
                    item.setAttribute("IdProducto", results[i].getAttributeNode("IdProducto").value);

                    var presentaciones = results[i].getAttributeNode("Descripciones").value.split('|');
                    var codigos = results[i].getAttributeNode("Codigos").value.split('|');
                    var precios = results[i].getAttributeNode("Precios").value.split('|');

                    /// Si no posee codigos significa que es un producto que no tiene ninguna presentacion
                    /// y se debe a que fue movida de lugar y el producto no se puede eliminar ya que esta
                    /// utilizado en un pedido viejo. Entonce no lo dibujo.
                    if (!(codigos.length == 1 && codigos[0] == "")) {

                        tdProductos.appendChild(item);
                        trProductos.appendChild(tdProductos);



                        var EncontroPresentacion = false;
                        for (var j = 0; j < Headers.length - 1; j++) {

                            for (var x = 0; x < Headers.length - 1; x++) {

                                if (Headers[j] == presentaciones[x]) {

                                    tdProductos = document.createElement("td");
                                    tdProductos.innerHTML = "0";
                                    tdProductos.setAttribute("Id", "td_" + codigos[x]);
                                    tdProductos.setAttribute("Codigo", codigos[x]);
                                    tdProductos.setAttribute("width", "45px");
                                    tdProductos.setAttribute("Precio", precios[x]);
                                    tdProductos.setAttribute("Nombre", texto);
                                    tdProductos.setAttribute("Presentacion", presentaciones[x]);
                                    $(tdProductos).css("background-color", colorFondo);

                                    //debugger;
                                    tdProductos.setAttribute("align", "center");
                                    if ((presentaciones[x].trim().match("^5 ml$") != null && codigos[x].trim().match("^1128201140") == null)
                                    //                                     ||
                                    //                                     (presentaciones[x].trim().indexOf("5 ml") == 0 && presentaciones[x].indexOf("55 ml") < 0)
                                     ||
                                     (CodigoIncorporacion0 != "@")
                                     
                                    
                                    ) {
                                        tdProductos.setAttribute("maxValue", "1");
                                    }
                                    else
                                        tdProductos.setAttribute("maxValue", "999");

                                    tdProductos.onmouseover = ShowControl;


                                    trProductos.appendChild(tdProductos);
                                    EncontroPresentacion = true;
                                    break;
                                }
                            }

                            if (EncontroPresentacion == false) {

                                tdProductos = document.createElement("td");
                                tdProductos.innerHTML = "x";
                                tdProductos.setAttribute("Id", "td_" + codigos[x]);
                                tdProductos.setAttribute("width", "45px");
                                tdProductos.setAttribute("align", "center");
                                trProductos.appendChild(tdProductos);
                                $(tdProductos).css("background-color", colorFondo);
                            }

                            EncontroPresentacion = false;

                        }

                        tableContainerTbody.appendChild(trProductos);
                    }
                }
            }

        }

        tableContainer.appendChild(tableContainerTbody);
        resultshere.appendChild(tableContainer);
        document.getElementById("divSolicitar").style.display = "block";
    }
    else {
        // else tell the user no matches were found
        var resultshere = document.getElementById("resultshere");
        var para = document.createElement("p");
        var notfound = document.createTextNode("No existen elmentos: " + searchterm);
        resultshere.appendChild(para);
        para.appendChild(notfound);
    }
}

function ControlarSeleccionProductos(className, ctrlID, ctrlName) {

    var elementosRequeridos = $("#tbl_" + className)[0].getAttribute("ElementosRequeridos");
    var elementosSeleccionados = 0;
    var elementosSeleccionadoSinObj = 0;

    $("#tbl_" + className + " ." + className).each(function() {
        elementosSeleccionados += parseInt($(this)[0].value);
        if ($(this)[0].uniqueID != ctrlID) {
            elementosSeleccionadoSinObj += parseInt($(this)[0].value);
        }
    });

    if (ctrlName != undefined) {
        if ($get(ctrlID).value == 0) {
            $("#" + ctrlName.replace("inp", "lbl")).css("color", "black");
        }
        else {
            $("#" + ctrlName.replace("inp", "lbl")).css("color", "blue");
        }
    }


    if (elementosRequeridos == elementosSeleccionados) {

        $("#tbl_" + className + " ." + className).each(function() {
            if ($(this)[0].uniqueID != ctrlID && $(this)[0].value == 0)
                $(this)[0].disabled = true;
        });

        if (ctrlID != "")
            return $get(ctrlID).value;
    }
    else if (parseInt(elementosSeleccionados) > parseInt(elementosRequeridos)) {

        $("#tbl_" + className + " ." + className).each(function() {
            if ($(this)[0].uniqueID != ctrlID && $(this)[0].value == 0)
                $(this)[0].disabled = true;
        });

        return parseInt(elementosRequeridos) - elementosSeleccionadoSinObj;

    }
    else {
        $("#tbl_" + className + " ." + className).each(function() {
            if ($(this)[0].uniqueID != ctrlID)
                $(this)[0].disabled = false;
        });

        if (ctrlID != "") {
            return $get(ctrlID).value;
        }
    }

}

function ActualizarCantidadProductos(obj) {
    var cant = obj.value;
    //debugger;


    var elementosRequeridos = $("#tbl_" + obj.getAttribute("className"))[0].getAttribute("ElementosRequeridos");
    var elementosSeleccionados = 0;

    $("#tbl_" + obj.getAttribute("className") + " ." + obj.getAttribute("className")).each(function() {
        elementosSeleccionados += parseInt($(this)[0].value);
    });


    if (!e) var e = window.event

    if (elementosRequeridos != elementosSeleccionados) {
        if (e.offsetX >= 35 && e.offsetY <= 10) {
            if (cant == '' || cant == 0) {
                obj.value = 1;
            }
            else {
                if (parseInt(cant, 0) < parseInt(obj.getAttribute("maxValue"), 0)) {
                    obj.value = parseInt(cant, 0) + 1;

                }
            }
        }
        else if (e.offsetX >= 35 && e.offsetY > 10) {
            if (cant == '' || cant == 0) {
                obj.value = 0;

            }
            else {
                obj.value = parseInt(cant, 0) - 1;

            }
        }
    }
    else {
        if (e.offsetX >= 35 && e.offsetY > 10) {
            if (cant == '' || cant == 0) {
                obj.value = 0;

            }
            else {
                obj.value = parseInt(cant, 0) - 1;

            }
        }
    }

    ControlarSeleccionProductos(obj.getAttribute("className"), obj.uniqueID, obj.id);

}

function ActualizarCantidad() {

    var cant = document.getElementById(event.srcElement.id).value;

    if (!e) var e = window.event
    if (e.offsetX >= 35 && e.offsetY <= 10) {
        if (cant == '' || cant == 0) {
            document.getElementById(event.srcElement.id).value = 1;
        }
        else {
            if (parseInt(cant, 0) < 999) {
                document.getElementById(event.srcElement.id).value = parseInt(cant, 0) + 1;
            }
        }

        if (parseInt(CurrentControl.getAttribute("maxValue")) == parseInt("1")) {
            document.getElementById(event.srcElement.id).value = 1;
        }

        CurrentControl.innerHTML = document.getElementById(event.srcElement.id).value;
        CurrentControl.focus();

    }
    else if (e.offsetX >= 35 && e.offsetY > 10) {
        if (cant == '' || cant == 0) {
            document.getElementById(event.srcElement.id).value = 0;
        }
        else {
            document.getElementById(event.srcElement.id).value = parseInt(cant, 0) - 1;
        }

        CurrentControl.innerHTML = document.getElementById(event.srcElement.id).value;
        CurrentControl.focus();
    }

}

function InputValidateByKeyProducto(obj) {
    var cant = obj.value;
    var keyid = (window.Event) ? event.which : event.keyCode;

    if (keyid == 97 && obj.value == "1")
        return false;
    else {
        return (keyid == 46 || keyid == 37 || keyid == 39 || keyid <= 13 || (keyid >= 48 && keyid <= 57) || (keyid >= 96 && keyid <= 105));
    }

}


function InputByKeyProducto(obj) {
    var cant = obj.value;
    var keyid = (window.Event) ? event.which : event.keyCode;
    //debugger;
    if (keyid == 38) {
        if (cant == '') {
            obj.value = 1;
        }
        else {
            if (parseInt(cant, 0) < parseInt(obj.getAttribute("maxValue"), 0)) {
                obj.value = parseInt(cant, 0) + 1;
            }
        }
    }
    else if (keyid == 40) {
        if (cant == '' || cant == 0) {
            obj.value = 0;
        }
        else {
            obj.value = parseInt(cant, 0) - 1;
        }
    }



    if ((keyid == 46 || keyid == 37 || keyid == 39 || keyid <= 13 || (keyid >= 48 && keyid <= 57) || (keyid >= 96 && keyid <= 105))) {

        obj.value = ControlarSeleccionProductos(obj.getAttribute("className"), obj.uniqueID, obj.id);

    }
    else {
        obj.value = ControlarSeleccionProductos(obj.getAttribute("className"), obj.uniqueID, obj.id);

    }


}

function InputByKey() {
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

        if (parseInt(CurrentControl.getAttribute("maxValue")) == parseInt("1")) {
            document.getElementById(event.srcElement.id).value = 1;
        }


        CurrentControl.innerHTML = document.getElementById(event.srcElement.id).value;
    }
    else if (keyid == 40) {
        if (cant == '' || cant == 0) {
            document.getElementById(event.srcElement.id).value = 0;
        }
        else {
            document.getElementById(event.srcElement.id).value = parseInt(cant, 0) - 1;
        }

        CurrentControl.innerHTML = document.getElementById(event.srcElement.id).value;
    }

    if (parseInt(CurrentControl.getAttribute("maxValue")) == parseInt("1") &&
        (keyid != 40 && keyid != 38 && keyid != 39 && keyid != 37 && keyid != 46 && keyid != 8 && keyid != 97)) {
        return false;
    }
    else if (keyid == 97 && document.getElementById(event.srcElement.id).value == "1")
        return false;
    else {
        return (keyid == 46 || keyid == 37 || keyid == 39 || keyid <= 13 || (keyid >= 48 && keyid <= 57) || (keyid >= 96 && keyid <= 105));
    }

}

function ActualizarInput() {
    var cant = document.getElementById(event.srcElement.id).value;
    if (cant != '')
        CurrentControl.innerHTML = document.getElementById(event.srcElement.id).value;
}