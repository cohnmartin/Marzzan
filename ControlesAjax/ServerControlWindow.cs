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
using System.Diagnostics;
using System.Web.Script.Serialization;

[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.ImagenBienvenido_r1_c1.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.ImagenBienvenido_r1_c3.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.ImagenBienvenido_r1_c5.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.ImagenBienvenido_r3_c1.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.ImagenBienvenido_r3_c5.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.ImagenBienvenido_r5_c1.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.ImagenBienvenido_r5_c3.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.ImagenBienvenido_r5_c5.png", "img/png")]


[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.LoginConosud_r1_c1.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.LoginConosud_r1_c3.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.LoginConosud_r1_c5.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.LoginConosud_r3_c1.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.LoginConosud_r3_c5.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.LoginConosud_r5_c1.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.LoginConosud_r5_c3.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.LoginConosud_r5_c5.png", "img/png")]


[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.ServerWindowMarzzan_r1_c1.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.ServerWindowMarzzan_r1_c3.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.ServerWindowMarzzan_r1_c5.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.ServerWindowMarzzan_r3_c1.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.ServerWindowMarzzan_r3_c5.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.ServerWindowMarzzan_r5_c1.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.ServerWindowMarzzan_r5_c3.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.ServerWindowMarzzan_r5_c5.png", "img/png")]

[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.LoginKisoco_r1_c1.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.LoginKisoco_r1_c3.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.LoginKisoco_r1_c5.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.LoginKisoco_r3_c1.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.LoginKisoco_r3_c5.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.LoginKisoco_r5_c1.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.LoginKisoco_r5_c3.png", "img/png")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.LoginKisoco_r5_c5.png", "img/png")]

[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.waiting.gif", "img/gif")]
[assembly: WebResource("ControlsAjaxNotti.ImagenesWindow.Cancelar.gif", "img/gif")]
[assembly: WebResource("ControlsAjaxNotti.ClientControlWindow.js", "application/x-javascript")]
[assembly: WebResource("ControlsAjaxNotti.jquery-1.3.2.min.js", "application/x-javascript")]
[assembly: WebResource("ControlsAjaxNotti.jquery.uploadify.js", "application/x-javascript")]



namespace ControlsAjaxNotti
{
    /// <summary>
    /// Summary description for ServerControlWindow
    /// </summary>

    [ParseChildren(true)]
    public class ServerControlWindow : ScriptControlBase, INamingContainer
    {
        public enum _WindowColor
        {
            Rojo,
            Azul,
            Gray
        }
        public _WindowColor WindowColor
        {
            get;
            set;
        }

        public string ZIndex
        {
            get;
            set;
        }

        private string BackGroundColor
        {
            get
            {
                if (BackColor != null && BackColor.Name != "0")
                {
                    if (BackColor.IsNamedColor)
                        return BackColor.Name;
                    else
                        return "#" + BackColor.Name.Substring(2);
                }
                else
                    return "White";
            }
        }

        private string ForeColorTitle
        {
            get
            {
                if (this.ForeColor != null && this.ForeColor.Name != "0")
                {
                    if (this.ForeColor.IsNamedColor)
                        return this.ForeColor.Name;
                    else
                        return "#" + this.ForeColor.Name.Substring(2);
                }
                else
                    return BackGroundColor;
            }
        }

        private string FontNameTitle
        {
            get
            {
                if (this.Font.Name != "")
                {
                    return this.Font.Name;
                }
                else
                    return "segoe ui";
            }
        }



        public string onClientCanceled
        {
            get
            {
                object value = ViewState["CancelFunction"];

                return (value == null) ? string.Empty : (string)value;
            }
            set
            {
                ViewState["CancelFunction"] = value;
            }
        }

        public ServerControlWindow()
            : base(false, HtmlTextWriterTag.Div)
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected override void OnInit(EventArgs e)
        {
            InitialiseControls();
            base.OnInit(e);


        }

        protected override void OnPreRender(EventArgs e)
        {

            base.OnPreRender(e);
        }


        public override Control FindControl(string name)
        {
            return this.Controls[1].FindControl(name);
        }

        private void InitialiseControls()
        {
            HtmlGenericControl divBlock = new HtmlGenericControl("Div");
            divBlock.ID = "DivCarga";
            divBlock.Style.Add(HtmlTextWriterStyle.Display, "none");
            this.Controls.Add(divBlock);


            HtmlGenericControl divCuerpo = new HtmlGenericControl("Div");
            divCuerpo.ID = "DivCuerpo";
            divCuerpo.Style.Add(HtmlTextWriterStyle.Display, "none");
            divCuerpo.Style.Add(HtmlTextWriterStyle.Position, "absolute");
            divCuerpo.Style.Add(HtmlTextWriterStyle.Top, "30%");
            divCuerpo.Style.Add(HtmlTextWriterStyle.Left, " 25%");

            if (ZIndex == null)
                divCuerpo.Style.Add(HtmlTextWriterStyle.ZIndex, "100199999");
            else
                divCuerpo.Style.Add(HtmlTextWriterStyle.ZIndex, ZIndex);

            divCuerpo.Style.Add(HtmlTextWriterStyle.BackgroundColor, "Transparent");
            divCuerpo.Style.Add(HtmlTextWriterStyle.VerticalAlign, " middle");
            divCuerpo.Style.Add(HtmlTextWriterStyle.TextAlign, " center");
            divCuerpo.Style.Add(HtmlTextWriterStyle.Height, "100%");



            this.Controls.Add(divCuerpo);


            HtmlTable tbl = new HtmlTable();
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell cell = new HtmlTableCell();
            HtmlTableCell cell1 = new HtmlTableCell();
            HtmlTableCell cell2 = new HtmlTableCell();

            tbl.CellPadding = 0;
            tbl.CellSpacing = 0;
            tbl.Attributes.Add("border", "0");
            tbl.Style.Add(HtmlTextWriterStyle.BackgroundColor, "Transparent");

            string ImagenWindowColor = "ServerWindowMarzzan";
            switch (WindowColor)
            {
                case _WindowColor.Rojo:
                    ImagenWindowColor = "LoginConosud";
                    break;
                case _WindowColor.Azul:
                    ImagenWindowColor = "ServerWindowMarzzan";
                    break;
                case _WindowColor.Gray:
                    ImagenWindowColor = "LoginKisoco";
                    break;
                default:
                    break;
            }


            #region Header windows


            cell.Style.Add(HtmlTextWriterStyle.BackgroundImage, Page.ClientScript.GetWebResourceUrl(this.GetType(), "ControlsAjaxNotti.ImagenesWindow." + ImagenWindowColor + "_r1_c1.png"));
            cell.Style.Add(HtmlTextWriterStyle.Width, "27px");
            cell.Style.Add(HtmlTextWriterStyle.Height, "44px");
            cell.Style.Add("background-color", "Transparent");
            cell.Style.Add("background-repeat", " no-repeat");
            cell.Style.Add("background-position", "top");
            cell.InnerHtml = "<div style='width: 100%; background-color: " + BackGroundColor + "; position:relative;left:15px;top:17px;z-index:-1'></div>";


            cell1.Style.Add(HtmlTextWriterStyle.BackgroundImage, Page.ClientScript.GetWebResourceUrl(this.GetType(), "ControlsAjaxNotti.ImagenesWindow." + ImagenWindowColor + "_r1_c3.png"));
            cell1.Style.Add(HtmlTextWriterStyle.Height, "35px");
            cell1.Style.Add("background-color", "Transparent");
            cell1.Style.Add("background-repeat", "repeat-x");
            cell1.Style.Add("background-position", "top");
            cell1.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            cell1.InnerHtml = "<div style='width: 100%;height:15px;background-color: " + BackGroundColor + "; position:relative;top:35px;z-index:-1'></div>";



            Label lblTitulo = new Label();
            lblTitulo.ID = "lblTituloControlWindow";
            lblTitulo.Style.Add(HtmlTextWriterStyle.Color, ForeColorTitle);
            lblTitulo.Style.Add(HtmlTextWriterStyle.FontSize, "16px");
            lblTitulo.Style.Add(HtmlTextWriterStyle.FontFamily, "'" + FontNameTitle + "'");
            lblTitulo.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
            lblTitulo.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            lblTitulo.Style.Add(HtmlTextWriterStyle.Top, "-12px");
            lblTitulo.Style.Add(HtmlTextWriterStyle.Left, "-10px");
            lblTitulo.Style.Add(HtmlTextWriterStyle.Position, "relative");
            lblTitulo.Style.Add("text-transform", "capitalize");
            cell1.ID = "CelllblTitulo";
            cell1.Controls.Add(lblTitulo);




            cell2.Style.Add(HtmlTextWriterStyle.BackgroundImage, Page.ClientScript.GetWebResourceUrl(this.GetType(), "ControlsAjaxNotti.ImagenesWindow." + ImagenWindowColor + "_r1_c5.png"));
            cell2.Style.Add(HtmlTextWriterStyle.Width, "22px");
            cell2.Style.Add(HtmlTextWriterStyle.Height, "35px");
            cell2.Style.Add("background-color", "Transparent");
            cell2.Style.Add("background-repeat", "no-repeat");
            cell2.Style.Add("background-position", "top");
            cell2.Attributes.Add("align", "left");
            cell2.InnerHtml = "<div style='width: 100%; background-color: " + BackGroundColor + "; position:relative;left:-10px;top:25px;z-index:-1'></div>";


            ImageButton imgClose = new ImageButton();
            imgClose.ID = "imgCloseControlWindow";
            imgClose.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "ControlsAjaxNotti.ImagenesWindow.Cancelar.gif");
            imgClose.Style.Add(HtmlTextWriterStyle.Top, "-14px");
            imgClose.Style.Add(HtmlTextWriterStyle.Left, "-5px");
            imgClose.Style.Add(HtmlTextWriterStyle.Position, "relative");
            imgClose.CausesValidation = false;
            imgClose.OnClientClick = "return false;";
            cell2.Controls.Add(imgClose);


            row.Cells.Add(cell);
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            tbl.Rows.Add(row);
            #endregion


            #region Center windows

            row = new HtmlTableRow();
            cell = new HtmlTableCell();
            HtmlTableCell cellContent = new HtmlTableCell();
            cell2 = new HtmlTableCell();

            cell.Style.Add(HtmlTextWriterStyle.BackgroundImage, Page.ClientScript.GetWebResourceUrl(this.GetType(), "ControlsAjaxNotti.ImagenesWindow." + ImagenWindowColor + "_r3_c1.png"));
            cell.Style.Add(HtmlTextWriterStyle.Width, "22px");
            cell.Style.Add("background-color", "Transparent");
            cell.Style.Add("background-repeat", "repeat-y");
            cell.Style.Add("background-position", "left top");

            HtmlGenericControl divLeft = new HtmlGenericControl("Div");
            divLeft.ID = "DivLeft";
            //divLeft.Style.Value = "height:100%;width: 100%;background-color: red; position:relative;left:10px;z-index:-1";
            divLeft.Style.Value = "height:100%;width: 100%;background-color: " + BackGroundColor + "; position:relative;left:10px;z-index:-1";
            cell.Controls.Add(divLeft);



            cellContent.Style.Add("background-color", BackGroundColor);
            cellContent.Attributes.Add("align", "left");
            cellContent.Style.Add(HtmlTextWriterStyle.FontSize, "12px");
            cellContent.Style.Add(HtmlTextWriterStyle.FontFamily, "Tahoma");
            cellContent.Style.Add(HtmlTextWriterStyle.Color, "black");
            cellContent.Style.Add(HtmlTextWriterStyle.MarginTop, "-20px");
            cellContent.Style.Add(HtmlTextWriterStyle.MarginLeft, "-20px");



            cell2.Style.Add(HtmlTextWriterStyle.BackgroundImage, Page.ClientScript.GetWebResourceUrl(this.GetType(), "ControlsAjaxNotti.ImagenesWindow." + ImagenWindowColor + "_r3_c5.png"));
            cell2.Style.Add("background-color", "Transparent");
            cell2.Style.Add("background-repeat", "repeat-y");
            cell2.Style.Add("background-position", "top");

            HtmlGenericControl divRight = new HtmlGenericControl("Div");
            divRight.ID = "DivRight";
            //divRight.Style.Value = "height:100%;width: 100%;background-color: red; position:relative;right:10px;z-index:-1";
            divRight.Style.Value = "height:100%;width: 100%;background-color: " + BackGroundColor + "; position:relative;right:10px;z-index:-1";
            cell2.Controls.Add(divRight);


            row.Cells.Add(cell);
            row.Cells.Add(cellContent);
            row.Cells.Add(cell2);
            tbl.Rows.Add(row);
            #endregion


            #region Footer windows

            row = new HtmlTableRow();
            cell = new HtmlTableCell();
            cell1 = new HtmlTableCell();
            cell2 = new HtmlTableCell();


            cell.Style.Add(HtmlTextWriterStyle.BackgroundImage, Page.ClientScript.GetWebResourceUrl(this.GetType(), "ControlsAjaxNotti.ImagenesWindow." + ImagenWindowColor + "_r5_c1.png"));
            cell.Style.Add(HtmlTextWriterStyle.Height, "37px");
            cell.Style.Add("background-color", "Transparent");
            cell.Style.Add("background-repeat", " no-repeat");
            cell.Style.Add("background-position", "left top");
            cell.InnerHtml = @"<div style='width: 100%; background-color: " + BackGroundColor + "; position:relative;left:10px;top:-15px;z-index:-1'></div>";


            cell1.Style.Add(HtmlTextWriterStyle.BackgroundImage, Page.ClientScript.GetWebResourceUrl(this.GetType(), "ControlsAjaxNotti.ImagenesWindow." + ImagenWindowColor + "_r5_c3.png"));
            cell1.Style.Add("background-color", "Transparent");
            cell1.Style.Add("background-repeat", "repeat-x");
            cell1.Style.Add("background-position", "top");
            cell1.InnerHtml = @"<div style='width: 100%;height:100%;background-color: " + BackGroundColor + "; position:relative;top:-15px;z-index:-1'></div>";


            cell2.Style.Add(HtmlTextWriterStyle.BackgroundImage, Page.ClientScript.GetWebResourceUrl(this.GetType(), "ControlsAjaxNotti.ImagenesWindow." + ImagenWindowColor + "_r5_c5.png"));
            cell2.Style.Add("background-color", "Transparent");
            cell2.Style.Add("background-repeat", "no-repeat");
            cell2.Style.Add("background-position", "top");
            cell2.InnerHtml = @"<div style='width: 100%; background-color: " + BackGroundColor + "; position:relative;top:-15px;right:15px;z-index:-1'></div>";


            row.Cells.Add(cell);
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            tbl.Rows.Add(row);
            #endregion


            divCuerpo.Controls.Add(tbl);


            ContentControls.InstantiateIn(cellContent);

        }


        [PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(TemplateControl)), TemplateInstance(TemplateInstance.Single)]
        public ITemplate ContentControls
        {
            get;
            set;
        }


        protected override IEnumerable<ScriptDescriptor>
                GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor = new ScriptControlDescriptor("ControlsAjaxNotti.ClientControlWindow", this.ClientID);


            descriptor.AddProperty("refWaitingImg", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ControlsAjaxNotti.ImagenesWindow.waiting.gif"));


            if (!string.IsNullOrEmpty(onClientCanceled))
            {
                descriptor.AddEvent("Cancel", onClientCanceled);
            }

            yield return descriptor;
        }


        // Generate the script reference
        protected override IEnumerable<ScriptReference>
                GetScriptReferences()
        {


            List<ScriptReference> js = new List<ScriptReference>();
            js.Add(new ScriptReference("ControlsAjaxNotti.ClientControlWindow.js", this.GetType().Assembly.FullName));
            //js.Add(new ScriptReference("ControlsAjaxNotti.jquery-1.3.2.min.js", this.GetType().Assembly.FullName));
            //js.Add(new ScriptReference("ControlsAjaxNotti.jquery.uploadify.js", this.GetType().Assembly.FullName));

            return js;

        }


    }



}
