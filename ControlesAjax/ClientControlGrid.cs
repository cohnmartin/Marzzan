using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using AjaxControlToolkit;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.Security.Permissions;
using System.Collections;
using System.Drawing.Design;
using System.ComponentModel.Design;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Data;
//using Telerik.Web.UI;


[assembly: WebResource("ControlsAjaxNotti.WindowStatic.ClientGridStyle.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("ControlsAjaxNotti.WindowStatic.ClientGridStyle_Sunset.css", "text/css", PerformSubstitution = true)]

[assembly: WebResource("ControlsAjaxNotti.ImagenesGrid.sprite_vista.gif", "img/gif")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesGrid.sprite_sunset.gif", "img/gif")]

[assembly: WebResource("ControlsAjaxNotti.ImagenesGrid.Delete.gif", "img/gif")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesGrid.Add.gif", "img/gif")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesGrid.Edit.gif", "img/gif")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesGrid.Excel.gif", "img/gif")]



[assembly: WebResource("ControlsAjaxNotti.ImagenesGrid.Exclamacion.png", "img/png")]

[assembly: WebResource("ControlsAjaxNotti.ImagenesGrid.WaitingGrid.gif", "img/gif")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesGrid.PagingFirst.gif", "img/gif")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesGrid.PagingLast.gif", "img/gif")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesGrid.PagingNext.gif", "img/gif")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesGrid.PagingPrev.gif", "img/gif")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesGrid.btnPageEmpty.jpg", "img/jpg")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesGrid.btnPageEmptyBlue.jpg", "img/jpg")]

[assembly: WebResource("ControlsAjaxNotti.ImagenesGrid.btnPageEmptyBrown.jpg", "img/jpg")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesGrid.btnPageEmptySunset.jpg", "img/jpg")]


[assembly: WebResource("ControlsAjaxNotti.ClientControlGrid.js", "application/x-javascript")]
[assembly: WebResource("ControlsAjaxNotti.jquery-1.3.2.min.js", "application/x-javascript")]
[assembly: WebResource("ControlsAjaxNotti.jquery.tablegroup.js", "application/x-javascript")]
[assembly: WebResource("ControlsAjaxNotti.jquery.autocomplete.js", "application/x-javascript")]




namespace ControlsAjaxNotti
{
    public delegate void ClickEventHandler(object sender);


    /// <summary>
    /// Summary description for ServerControlWindow
    /// </summary>

    [ClientCssResource("ControlsAjaxNotti.WindowStatic.ClientGridStyle.css")]
    [ClientCssResource("ControlsAjaxNotti.WindowStatic.ClientGridStyle_Sunset.css")]
    [ParseChildren(true)]
    public class ClientControlGrid : ScriptControlBase, INamingContainer, IPostBackEventHandler, ICallbackEventHandler
    {
        private Hashtable _defColums = new Hashtable();
        private List<string> _columnsCapitalice = new List<string>();

        protected string _btnSelectedPage;
        public event ClickEventHandler ExportToExcel;
        protected string _ControlID = "";

        public ClientControlGrid()
            : base(false, HtmlTextWriterTag.Div)
        {

        }

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);


        }

        protected override void OnPreRender(EventArgs e)
        {
            _ControlID = "tblDynamic_" + this.ClientID;

            InitialiseControls();

            base.OnPreRender(e);
        }

        private void InitialiseControls()
        {


            switch (TypeSkin)
            {
                case Skin.Vista:
                    this.Attributes["class"] = "TVista";
                    _btnSelectedPage = "btnPageEmptyBlue";
                    break;
                case Skin.Sunset:
                    this.Attributes["class"] = "TSunset";
                    _btnSelectedPage = "btnPageEmptyBrown";
                    break;
                default:
                    this.Attributes["class"] = "TVista";
                    _btnSelectedPage = "btnPageEmptyBlue";
                    break;
            }


            CallbackID = UniqueID; // needed for the callbacks and postbacks

            // Create JavaScript function for ClientCallBack WebForm_DoCallBack
            // Not sure why we need it, but the callback doesn't get registered on the client side properly without it.
            Page.ClientScript.GetCallbackEventReference(this, "", "", "");


            List<FunctionColumnRow> funcionesRows = new List<FunctionColumnRow>();
            foreach (FunctionColumnRow item in _functions)
            {
                funcionesRows.Add(item);
            }


            List<FunctionGral> funcionesGrales = new List<FunctionGral>();
            foreach (FunctionGral item in _functionsGral)
            {
                funcionesGrales.Add(item);
            }


            StringBuilder tblBasica = new StringBuilder();

            tblBasica.AppendLine("<table id='" + _ControlID + "' cellpadding='0' cellspacing='0' width='100%' >");
            tblBasica.AppendLine("<thead>theadAdd");
            tblBasica.AppendLine("<tr>");


            /// Controlo si se ha definido la columna de Edicion
            if (funcionesRows.Any(w => w.Type == FunctionColumnRow.FunctionType.Edit))
            {
                tblBasica.AppendLine("<td class='tdEdit Theader' >&nbsp;</td>");
                ColumnasVisibles++;
            }


            /// Controlo si se ha definido columnas custom
            if (funcionesRows.Any(w => w.Type == FunctionColumnRow.FunctionType.Custom))
            {
                tblBasica.AppendLine("<td class='tdCustom Theader' >&nbsp;</td>");
                ColumnasVisibles++;
            }


            /// Creo todas las columnas definidas
            foreach (Column item in Columns)
            {
                /// Solo creo en la tabla las columnas que se muestran
                if (item.Display == true)
                {
                    bool ColumnaTotalizable = item.Totalizar;
                    string strWidth = item.Width.IsEmpty ? "" : "width:" + item.Width;
                    tblBasica.AppendLine("<td dataFieldName='" + item.DataFieldName + "' style='" + strWidth + "' class='Theader' Totalizar='" + ColumnaTotalizable.ToString() + "' >" + item.HeaderName + "</td>");
                    ColumnasVisibles++;
                }

            }

            /// Controlo si se ha definido la columna de Eliminacion
            if (funcionesRows.Any(w => w.Type == FunctionColumnRow.FunctionType.Delete))
            {
                tblBasica.AppendLine("<td class='tdEliminar Theader' >&nbsp;</td>");
                ColumnasVisibles++;
            }


            tblBasica.AppendLine("</tr>");
            tblBasica.AppendLine("</thead>");
            tblBasica.AppendLine("<tbody></tbody>");



            tblBasica.AppendLine("<tfoot>tfootAdd");

            if (this.AllowPaging)
            {

                tblBasica.AppendLine("<tr class='tdFooter'>");
                tblBasica.AppendLine("<td class='tdGral' colspan='" + ColumnasVisibles.ToString() + "'>");



                long resto = 0;
                long CantidadTotalHojas = Math.DivRem(VirtualCount, PageSize, out resto);
                CantidadTotalHojas += resto > 0 ? 1 : 0;
                string displayTblPaging = CantidadTotalHojas <= 1 ? "display:none" : "display:block";

                tblBasica.AppendLine("<table id='tblPaging' cellpadding='0' cellspacing='3' border='0' style='height: 100%;" + displayTblPaging + "' ><tr>" +
                    "<td style='width:22px;height:22px'><div id='theFirst' class='ImgPagingFirst'>&nbsp;</div></td>" +
                    "<td style='width:22px;height:22px'><div id='thePrev' class='ImgPagingPrev'>&nbsp;</div></td>");


                for (int i = 0; i < 10; i++)
                {
                    if (CantidadTotalHojas > 0 && i < 10)
                    {
                        if (i == 0)
                            tblBasica.AppendLine("<td style='width:22px;height:22px;' class='vacia' ><div id='btnChangePage' class='btnChangePageIndex' style='background-image:url(" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "ControlsAjaxNotti.ImagenesGrid." + _btnSelectedPage + ".jpg") + ")' PageIndex='" + (i).ToString() + "'  >" + (i + 1).ToString() + "</div></td>");
                        else
                        {
                            if (i < CantidadTotalHojas)
                                tblBasica.AppendLine("<td style='width:22px;height:22px;' class='vacia' ><div id='btnChangePage' class='btnChangePageIndex' PageIndex='" + (i).ToString() + "'  >" + (i + 1).ToString() + "</div></td>");
                            else
                                tblBasica.AppendLine("<td style='width:22px;height:22px;display:none'  class='vacia' ><div id='btnChangePage'  class='btnChangePageIndex' PageIndex='" + (i).ToString() + "'  >" + (i + 1).ToString() + "</div></td>");
                        }

                    }

                }

                if (CantidadTotalHojas >= 10)
                {
                    tblBasica.AppendLine("<td style='width:22px;height:22px' class='vacia' ><div id='btnChangePage' class='btnChangePageIndex' PageIndex='...'  >...</div></td>");
                }


                tblBasica.AppendLine("<td style='width:22px;height:22px'><div id='theNext' class='ImgPagingNext'>&nbsp;</div></td>" +
                    "<td style='width:22px;height:22px'><div id='theLast' class='ImgPagingLast'>&nbsp;</div></td>" +
                    "</tr></table>");


                tblBasica.AppendLine("</td></tr>");
            }

            tblBasica.AppendLine("</tfoot>");




            /// Controlo si se han definido funciones generales 
            if (funcionesGrales.Count > 0)
            {
                if (PositionAdd == positionAdd.Top || PositionAdd == positionAdd.Both)
                {
                    tblBasica.Replace("theadAdd", "<tr><td colspan='" + ColumnasVisibles.ToString() + "' class='tdFunctionAdd' ></td></tr>");
                }
                else
                    tblBasica.Replace("theadAdd", "");

                if (PositionAdd == positionAdd.Botton || PositionAdd == positionAdd.Both)
                {
                    tblBasica.Replace("tfootAdd", "<tr><td colspan='" + ColumnasVisibles.ToString() + "' class='tdFunctionAdd' ></td></tr>");
                }
                else
                    tblBasica.Replace("tfootAdd", "");
            }
            else
            {
                tblBasica.Replace("theadAdd", "");
                tblBasica.Replace("tfootAdd", "");
            }




            tblBasica.AppendLine("</table>");
            this.TblBase = tblBasica.ToString();



            /// codigo necesario para agregar un tool tip a la grilla para utilizar
            /// en las columnas de datos.
            HtmlGenericControl divBlock = new HtmlGenericControl("Div");
            divBlock.ID = "DivToolTiop";
            divBlock.Style.Add(HtmlTextWriterStyle.Display, "none");
            this.Controls.Add(divBlock);


            Telerik.Web.UI.RadToolTip tooltip = new Telerik.Web.UI.RadToolTip();
            tooltip.ID = "RadToolTipCtr";
            tooltip.Position = Telerik.Web.UI.ToolTipPosition.MiddleLeft;
            tooltip.RelativeTo = Telerik.Web.UI.ToolTipRelativeDisplay.Element;


            divBlock.Controls.Add(tooltip);

        }

        #region Enumeraciones

        /// <summary>
        /// Server events raised from the client
        /// </summary>
        enum ServerEvent
        {
            ExportToExcel,
            otro
        }

        public enum positionAdd
        {
            Top,
            Botton,
            Both
        }

        public enum Skin
        {
            Vista,
            Sunset
        }

        #endregion

        #region Eventos Servidor

        protected virtual void OnExportToExcel()
        {
            if (ExportToExcel != null)
            {
                ExportToExcel(this);
            }
        }

        public void ExportToExcelFunction(string fileName, IList _dataSourceExcel)
        {
            GridView gv = new GridView();
            gv.AutoGenerateColumns = false;
            gv.RowDataBound += new GridViewRowEventHandler(gv_RowDataBound);
            gv.RowCreated += new GridViewRowEventHandler(gv_RowCreated);

            string htmlTrHead = "";
            if (_dataSourceExcel != null)
            {
                List<Column> columsExport = new List<Column>();
                htmlTrHead += "<tr>";
                int i = 0;
                foreach (Column itemColumn in Columns)
                {

                    if (itemColumn.ExportToExcel)
                    {

                        BoundField boundField = new BoundField();
                        boundField.DataField = itemColumn.DataFieldName;
                        boundField.HeaderText = itemColumn.HeaderName;
                        boundField.NullDisplayText = "";
                        gv.Columns.Add(boundField);

                        _defColums.Add(itemColumn.DataFieldName, i);


                        if (itemColumn.Capitalice)
                            _columnsCapitalice.Add(itemColumn.DataFieldName);


                        i++;
                    }


                }

                if (_dataSourceExcel[0].GetType() == typeof(DataRow))
                    gv.DataSource = ((System.Data.DataRow)(_dataSourceExcel[0])).Table.AsDataView();
                else
                    gv.DataSource = _dataSourceExcel;

                gv.DataBind();


                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                gv.RenderControl(htmlWrite);

                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".xls");
                HttpContext.Current.Response.ContentType = "application/xls";
                HttpContext.Current.Response.Write(stringWrite.ToString());
                HttpContext.Current.Response.End();




            }


        }

        void gv_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
                //Table t = (Table)(sender as GridView).Controls[0];

                //// Adding Cells
                //Image imgLogo = new Image();
                //imgLogo.ImageUrl = @"http://localhost:5097/ConosudVale/images/Encabezdo.png";
                //imgLogo.Width = Unit.Pixel(106);
                //imgLogo.Height = Unit.Pixel(76);



                //TableCell FileDate = new TableHeaderCell();
                //FileDate.ColumnSpan = 1;
                //FileDate.Controls.Add(imgLogo);
                //FileDate.Width = Unit.Pixel(110);
                //FileDate.Height = Unit.Pixel(80);
                //row.Cells.Add(FileDate);


                //TableCell cell = new TableHeaderCell();
                //cell.ColumnSpan = 2;
                //cell.Text = "Reporte Alta y Bajas de Legajos";
                //cell.VerticalAlign = VerticalAlign.Top;
                //cell.HorizontalAlign = HorizontalAlign.Left;
                //row.Cells.Add(cell);


                //t.Rows.AddAt(0, row);
            }
        }



        void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (string ColumnName in _columnsCapitalice)
                {
                    int index = int.Parse(_defColums[ColumnName].ToString());

                    if (e.Row.Cells[index].Text.ToLower() != "&nbsp;")
                        e.Row.Cells[index].Text = ToCapitalize(e.Row.Cells[index].Text.ToLower());
                    else
                        e.Row.Cells[index].Text = "";

                }

                foreach (int index in _defColums.Values)
                {
                    e.Row.Cells[index].Text = ReemplazarCaracteresEspeciales(e.Row.Cells[index].Text);
                }
            }

        }



        #endregion

        #region Definicion Eventos Cliente

        //public string onClientFunctionRowClicked
        //{
        //    get
        //    {
        //        object value = ViewState["onClientFunctionRowClicked"];

        //        return (value == null) ? string.Empty : (string)value;
        //    }
        //    set
        //    {
        //        ViewState["onClientFunctionRowClicked"] = value;
        //    }
        //}

        //public string onClientFunctionRowClicking
        //{
        //    get
        //    {
        //        object value = ViewState["onClientFunctionRowClicking"];

        //        return (value == null) ? string.Empty : (string)value;
        //    }
        //    set
        //    {
        //        ViewState["onClientFunctionRowClicking"] = value;
        //    }
        //}

        public string onClientChangePageIndex
        {
            get
            {
                object value = ViewState["onClientChangePageIndex"];

                return (value == null) ? string.Empty : (string)value;
            }
            set
            {
                ViewState["onClientChangePageIndex"] = value;
            }
        }

        #endregion

        #region Definiciones para la tabla
        private FunctionColumnRowCollection _functions = new FunctionColumnRowCollection();
        private FunctionGralCollection _functionsGral = new FunctionGralCollection();
        private ColumnsCollection _columns = new ColumnsCollection();


        [ClientPropertyName("functionColumns")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public FunctionColumnRowCollection FunctionsColumns
        {
            get { return _functions; }
        }


        [ClientPropertyName("functionsGral")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public FunctionGralCollection FunctionsGral
        {
            get { return _functionsGral; }
        }

        [ClientPropertyName("columns")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public ColumnsCollection Columns
        {
            get { return _columns; }
        }



        #endregion

        #region Propiedades generales de la tabla

        public string OnClientFunctionRowClicked
        { get; set; }

        bool _showDataOnInit = true;
        public bool ShowDataOnInit
        {
            get
            {
                return _showDataOnInit;
            }
            set
            {
                _showDataOnInit = value;
            }
        }

        public string EmptyMessage
        { get; set; }

        protected int ColumnasVisibles
        { get; set; }

        public Skin TypeSkin
        { get; set; }



        public positionAdd PositionAdd
        {
            get;
            set;
        }
        [ClientPropertyName("callbackID")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string CallbackID
        {
            get
            {
                return GetPropertyValue("callbackID", UniqueID);
            }

            set
            {
                SetPropertyValue("callbackID", value);
            }
        }


        [ClientPropertyName("keyName")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string KeyName
        {
            get
            {
                return GetPropertyValue<string>("KeyName", "");
            }
            set
            {
                SetPropertyValue<string>("KeyName", value);
            }
        }

        [ClientPropertyName("allowMultiSelection")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool AllowMultiSelection
        {
            get
            {
                return GetPropertyValue<bool>("AllowMultiSelection", false);
            }
            set
            {
                SetPropertyValue<bool>("AllowMultiSelection", value);
            }
        }

        [ClientPropertyName("allowGroupRows")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool AllowGroupRows
        {
            get
            {
                return GetPropertyValue<bool>("AllowGroupRows", false);
            }
            set
            {
                SetPropertyValue<bool>("AllowGroupRows", value);
            }
        }

        [ClientPropertyName("allowPaging")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool AllowPaging
        {
            get
            {
                return GetPropertyValue<bool>("AllowPaging", false);
            }
            set
            {
                SetPropertyValue<bool>("AllowPaging", value);
            }
        }

        [ClientPropertyName("pageSize")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int PageSize
        {
            get
            {
                return GetPropertyValue<int>("PageSize", 10);
            }
            set
            {
                SetPropertyValue<int>("PageSize", value);
            }
        }

        [ClientPropertyName("virtualCount")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int VirtualCount
        {
            get
            {
                return GetPropertyValue<int>("VirtualCount", 0);
            }
            set
            {
                SetPropertyValue<int>("VirtualCount", value);
            }
        }


        [ClientPropertyName("allowRowSelection")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool AllowRowSelection
        {
            get
            {
                return GetPropertyValue<bool>("AllowRowSelection", false);
            }
            set
            {
                SetPropertyValue<bool>("AllowRowSelection", value);
            }
        }

        private IList _dataSource;
        public IList DataSource
        {
            get
            {
                return _dataSource;
            }

            set
            {
                _dataSource = value;
                if (_dataSource != null)
                {
                    DatosTabla = new object[_dataSource.Count];
                    int RowCount = 0;
                    foreach (var item in _dataSource)
                    {
                        Dictionary<string, object> array = new Dictionary<string, object>();

                        foreach (Column itemColumn in Columns)
                        {
                            object valorColumna;

                            string[] cols = itemColumn.DataFieldName.Split(',');
                            if (cols.Length > 1)
                            {
                                valorColumna = "";
                                foreach (var name in itemColumn.DataFieldName.Split(','))
                                {
                                    object valor = GetValueColum(item, name);
                                    if (valor.ToString() != "")
                                        valorColumna += GetValueColum(item, name) + " - ";
                                }

                                valorColumna = valorColumna.ToString().TrimEnd(' ').TrimEnd('-').TrimEnd(' ');

                            }
                            else
                                valorColumna = GetValueColum(item, itemColumn.DataFieldName);



                            if (itemColumn.Capitalice && valorColumna != null)
                                valorColumna = ToCapitalize(valorColumna.ToString());

                            array.Add(itemColumn.DataFieldName, valorColumna);

                            if (itemColumn.DataFieldNameValueCombo != null && itemColumn.DataFieldNameValueCombo != "")
                            {
                                valorColumna = GetValueColum(item, itemColumn.DataFieldNameValueCombo);
                                array.Add(itemColumn.DataFieldNameValueCombo, valorColumna);
                            }
                        }

                        /// Agrego como parte de la información el valor de la clave
                        if (!array.ContainsKey(KeyName))
                        {
                            if (item.GetType() == typeof(System.Data.DataRow))
                                array.Add(KeyName, (item as System.Data.DataRow)[KeyName]);
                            else
                                array.Add(KeyName, item.GetType().GetProperty(KeyName).GetValue(item, null));
                        }


                        DatosTabla[RowCount] = array;
                        RowCount++;
                    }
                }
            }
        }




        [ClientPropertyName("tblBase")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string TblBase
        {
            get
            {
                return GetPropertyValue<string>("TblBase", "");
            }
            set
            {
                SetPropertyValue<string>("TblBase", value);
            }
        }

        [ClientPropertyName("datosTabla")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        protected object[] DatosTabla
        { get; set; }



        #endregion

        #region Helper methods

        private string ExecuteServerEvent(string eventFromClient)
        {
            // Get the event name
            int separator = eventFromClient.IndexOf(";");
            string serverEvent = eventFromClient.Substring(0, separator);
            ServerEvent e = (ServerEvent)Enum.Parse(typeof(ServerEvent), serverEvent);

            // Get the event args (JSON serialised string)
            string eventArgs = eventFromClient.Substring(separator + 1);

            switch (e)
            {
                case ServerEvent.ExportToExcel:
                    OnExportToExcel();
                    break;
            }
            return "";
        }

        protected string ReemplazarCaracteresEspeciales(string cadena)
        {
            cadena = cadena.Replace("á", "&aacute;");
            cadena = cadena.Replace("é", "&eacute;");
            cadena = cadena.Replace("í", "&iacute;");
            cadena = cadena.Replace("ó", "&oacute;");
            cadena = cadena.Replace("ú", "&uacute;");

            cadena = cadena.Replace("Á", "&Aacute;");
            cadena = cadena.Replace("É", "&Eacute;");
            cadena = cadena.Replace("Í", "&Iacute;");
            cadena = cadena.Replace("Ó", "&Oacute;");
            cadena = cadena.Replace("Ú", "&Uacute;");

            cadena = cadena.Replace("º", "&deg;");
            cadena = cadena.Replace("Ü", "&Ucirc;");
            cadena = cadena.Replace("ü", "&ucirc;");
            cadena = cadena.Replace("“", "&ldquo");
            cadena = cadena.Replace("”", "&rdquo;");



            cadena = cadena.Replace("ñ", "&ntilde;");
            cadena = cadena.Replace("Ñ", "&Ntilde;");

            return cadena;
        }

        /// <summary>
        /// Retrieves the property value from the ViewState
        /// </summary>
        /// <typeparam name="V">Type of value</typeparam>
        /// <param name="propertyName">Property to retrieve</param>
        /// <param name="nullValue">value to be used in case it's not initialised.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        protected V GetPropertyValue<V>(string propertyName, V nullValue)
        {
            if (ViewState[propertyName] == null)
            {
                return nullValue;
            }
            return (V)ViewState[propertyName];
        }

        /// <summary>
        /// Save the property value in the ViewState
        /// </summary>
        /// <typeparam name="V">Property type</typeparam>
        /// <param name="propertyName">Property name</param>
        /// <param name="value">Value</param>
        [DebuggerStepThrough]
        protected void SetPropertyValue<V>(string propertyName, V value)
        {
            ViewState[propertyName] = value;
        }

        public static string ToCapitalize(string inputString)
        {

            System.Globalization.CultureInfo cultureInfo =

            System.Threading.Thread.CurrentThread.CurrentCulture;

            System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;

            return textInfo.ToTitleCase(inputString.ToLower());

        }


        public object GetValueColum(object item, string DataFieldName)
        {
            if (DataFieldName.Contains("."))
            {
                string currentName = DataFieldName.Substring(0, DataFieldName.IndexOf("."));
                object objItem = item.GetType().GetProperty(currentName).GetValue(item, null);
                if (objItem != null)
                    return GetValueColum(objItem, DataFieldName.Substring(DataFieldName.IndexOf(".") + 1));
                else
                    return "";
            }
            else
            {
                if (item.GetType() == typeof(System.Data.DataRow))
                    return (item as System.Data.DataRow)[DataFieldName];
                else
                    return item.GetType().GetProperty(DataFieldName).GetValue(item, null);
            }

        }


        #endregion

        #region IPostBackEventHandler Members

        /// <summary>
        /// Raise postback event in the server.
        /// </summary>
        /// <param name="eventArgument">argument from JScript</param>
        public void RaisePostBackEvent(string eventArgument)
        {
            ExecuteServerEvent(eventArgument);
        }

        #endregion

        #region ICallbackEventHandler Members

        /// <summary>
        /// Return value for the callback call
        /// </summary>
        string callbackResult = "";

        /// <summary>
        /// Returns the value generated by the event (if any).
        /// </summary>
        /// <returns></returns>
        string ICallbackEventHandler.GetCallbackResult()
        {
            return callbackResult;
        }

        /// <summary>
        /// Raises the server event in the server.
        /// </summary>
        /// <param name="eventArgument">argument from JScript</param>
        void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
        {
            callbackResult = ExecuteServerEvent(eventArgument);
        }

        #endregion

        #region ScriptControlBase

        protected override IEnumerable<ScriptDescriptor>
                GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor = new ScriptControlDescriptor("ControlsAjaxNotti.ClientControlGrid", this.ClientID);



            descriptor.AddProperty("allowGroupRows", this.AllowGroupRows);
            descriptor.AddProperty("tblBase", this.TblBase);
            descriptor.AddProperty("dataSource", this.DatosTabla);
            descriptor.AddProperty("columns", this.Columns);
            descriptor.AddProperty("functionColumns", this.FunctionsColumns);
            descriptor.AddProperty("functionsGral", this.FunctionsGral);
            descriptor.AddProperty("allowMultiSelection", this.AllowMultiSelection);
            descriptor.AddProperty("keyName", this.KeyName);
            descriptor.AddProperty("allowPaging", this.AllowPaging);
            descriptor.AddProperty("pageSize", this.PageSize);
            descriptor.AddProperty("callbackID", this.CallbackID);
            descriptor.AddProperty("virtualCount", this.VirtualCount);
            descriptor.AddProperty("allowRowSelection", this.AllowRowSelection);
            descriptor.AddProperty("refDeleteImg", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ControlsAjaxNotti.ImagenesGrid.Delete.gif"));
            descriptor.AddProperty("refAddImg", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ControlsAjaxNotti.ImagenesGrid.Add.gif"));
            descriptor.AddProperty("refEditImg", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ControlsAjaxNotti.ImagenesGrid.Edit.gif"));
            descriptor.AddProperty("refExcelImg", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ControlsAjaxNotti.ImagenesGrid.Excel.gif"));
            descriptor.AddProperty("refImgPageSelected", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ControlsAjaxNotti.ImagenesGrid." + _btnSelectedPage + ".jpg"));
            descriptor.AddProperty("refWaitingImg", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ControlsAjaxNotti.ImagenesGrid.WaitingGrid.gif"));
            descriptor.AddProperty("refExclamacionImg", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ControlsAjaxNotti.ImagenesGrid.Exclamacion.png"));
            descriptor.AddProperty("emptyMessage", EmptyMessage);
            descriptor.AddProperty("showDataOnInit", ShowDataOnInit);
            descriptor.AddProperty("onClientFunctionRowClicked", OnClientFunctionRowClicked);


            descriptor.AddProperty("uniqueID", _ControlID);
            descriptor.AddProperty("columnasVisibles", this.ColumnasVisibles);

            //if (!string.IsNullOrEmpty(onClientFunctionRowClicked))
            //{
            //    descriptor.AddEvent("rowClicked", onClientFunctionRowClicked);
            //}


            //if (!string.IsNullOrEmpty(onClientFunctionRowClicking))
            //{
            //    descriptor.AddEvent("rowClicking", onClientFunctionRowClicking);
            //}

            if (!string.IsNullOrEmpty(onClientChangePageIndex))
            {
                descriptor.AddEvent("ChangePageIndex", onClientChangePageIndex);
            }


            yield return descriptor;
        }

        // Generate the script reference
        protected override IEnumerable<ScriptReference>
                GetScriptReferences()
        {
            List<ScriptReference> js = new List<ScriptReference>();
            js.Add(new ScriptReference("ControlsAjaxNotti.ClientControlGrid.js", this.GetType().Assembly.FullName));
            //js.Add(new ScriptReference("ControlsAjaxNotti.jquery-1.3.2.min.js", this.GetType().Assembly.FullName));
            //js.Add(new ScriptReference("ControlsAjaxNotti.jquery.tablegroup.js", this.GetType().Assembly.FullName));
            //js.Add(new ScriptReference("ControlsAjaxNotti.jquery.autocomplete.js", this.GetType().Assembly.FullName));

            return js;
        }


        #endregion
    }

    public class FunctionColumnRow
    {
        public enum FunctionType
        {
            Delete,
            Edit,
            Custom
        }


        FunctionType _type = FunctionType.Delete;
        string _ClickFunction = "";
        string _text = "";

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public FunctionType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string ClickFunction
        {
            get { return _ClickFunction; }
            set { _ClickFunction = value; }
        }

        public string ImgUrl
        { get; set; }
    }


    public class FunctionGral
    {
        public enum FunctionType
        {
            Add,
            Delete,
            Edit,
            Excel,
            Custom
        }


        FunctionType _type = FunctionType.Add;
        string _ClickFunction = "";
        string _text = "";

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public FunctionType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string ClickFunction
        {
            get { return _ClickFunction; }
            set { _ClickFunction = value; }
        }

        public string ImgUrl
        { get; set; }
    }

    public class Column : Style
    {
        public enum align
        {
            Derecha,
            Izquierda,
            Centrado
        }

        public enum dataType
        {
            String,
            DateTime,
            Decimal,
            Integer,
            UrlImage,
            Bool
        }

        public string DataFieldToolTip
        { get; set; }

        public string HeaderName
        { get; set; }

        public string DataFieldName
        { get; set; }

        public string DataFieldNameValueCombo
        { get; set; }

        private bool _display = true;
        public bool Display
        {
            get
            { return _display; }

            set { _display = value; }
        }

        public align Align
        { get; set; }

        public dataType DataType
        { get; set; }

        public bool ExportToExcel
        { get; set; }

        public bool ToFixed
        { get; set; }

        public bool Totalizar
        { get; set; }

        public bool Capitalice
        { get; set; }

        public string NameControlManger
        { get; set; }

        private bool _enabled = false;
        public bool Enabled
        {
            get
            { return _enabled; }

            set { _enabled = value; }
        }

        public string ImgUrl
        { get; set; }

        /// <summary>
        /// Funcion que se llama cuando la columna es de tipo check, los parametros
        /// son: el objeto check y el id del row al que pertenece el check.
        /// </summary>
        public string onClientClick
        { get; set; }


        private bool _allowClientChange = false;
        public bool AllowClientChange
        {
            get
            { return _allowClientChange; }

            set { _allowClientChange = value; }
        }
    }

    public class FunctionColumnRowCollection : CollectionBase
    {
        public FunctionColumnRowCollection()
        {

        }

        public void Add(FunctionColumnRow param)
        {
            List.Add(param);
        }

        public void Remove(FunctionColumnRow param)
        {
            List.Remove(param);
        }

        public FunctionColumnRow this[int index]
        {
            get { return (FunctionColumnRow)List[index]; }
            set { base.List[index] = value; }
        }

    }

    public class ColumnsCollection : CollectionBase
    {

        public ColumnsCollection()
        {

        }

        public void Add(Column param)
        {
            List.Add(param);
        }

        public void Remove(Column param)
        {
            List.Remove(param);
        }

        public Column this[int index]
        {
            get { return (Column)List[index]; }
            set { base.List[index] = value; }
        }

    }

    public class FunctionGralCollection : CollectionBase
    {
        public FunctionGralCollection()
        {

        }

        public void Add(FunctionGral param)
        {
            List.Add(param);
        }

        public void Remove(FunctionGral param)
        {
            List.Remove(param);
        }

        public FunctionGral this[int index]
        {
            get { return (FunctionGral)List[index]; }
            set { base.List[index] = value; }
        }

    }
}
