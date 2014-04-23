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
using Telerik.Web.UI;

[assembly: WebResource("ControlsAjaxNotti.ClientServerCampoNumerico.js", "application/x-javascript")]
[assembly: WebResource("ControlsAjaxNotti.jquery-1.3.2.min.js", "application/x-javascript")]


namespace ControlsAjaxNotti
{
    /// <summary>
    /// Summary description for ServerControlWindow
    /// </summary>

    [ParseChildren(true)]
    public class ServerCampoNumerico : ScriptControlBase, INamingContainer
    {
        public ServerCampoNumerico()
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


        private void InitialiseControls()
        {

            RadNumericTextBox txt = new RadNumericTextBox();
            txt.ID = "txtCampo";
            txt.Skin = "WebBlue";
            //txt.Culture = new System.Globalization.CultureInfo("Spanish (Argentina)");
            txt.MinValue = 0;
            txt.Value = 0;
            txt.NumberFormat.DecimalSeparator = ".";
            txt.NumberFormat.GroupSeparator = "";
            txt.Width = Unit.Percentage(90);

            //<telerik:RadNumericTextBox ID="txtCampo" runat="server" Skin="WebBlue" Culture="Spanish (Argentina)"
            //    ClientEvents-OnBlur="VerificarDatosCompletos" ClientEvents-OnKeyPress="ControlarEnter"
            //    MinValue="0" ClientEvents-OnError="ShowErrorCampo">
            //    <ClientEvents OnBlur="VerificarDatosCompletos" OnError="ShowErrorCampo"></ClientEvents>
            //    <NumberFormat DecimalSeparator="." GroupSeparator="" />
            //</telerik:RadNumericTextBox>


            RequiredFieldValidator req = new RequiredFieldValidator();
            req.ID = "reqCampo";
            req.ControlToValidate = "txtCampo";
            req.ErrorMessage = "El campo marcado con * es obligatorio";
            req.ValidationGroup = "ServerCampoNumerico";
            req.Text = "*";


            //<asp:RequiredFieldValidator ID="RequiredFieldValidatorCampo" runat="server" ControlToValidate="txtCampo"
            //    ErrorMessage="El campo marcado con * es obligatorio" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>


            RadToolTip toolTipCampo = new RadToolTip();
            toolTipCampo.Skin = "Hay";
            toolTipCampo.ID = "toolTipoCampo";
            toolTipCampo.Position = ToolTipPosition.TopCenter;
            toolTipCampo.HideEvent = ToolTipHideEvent.ManualClose;
            toolTipCampo.ShowEvent = ToolTipShowEvent.FromCode;
            toolTipCampo.RelativeTo = ToolTipRelativeDisplay.Element;
            toolTipCampo.TargetControlID = "txtCampo";
            toolTipCampo.Animation = ToolTipAnimation.None;


            //<telerik:RadToolTip runat="server" ID="RadToolTip1" Skin="Hay" Position="TopCenter"
            //    ManualClose="false" ShowEvent="FromCode" RelativeTo="Element" TargetControlID="txtCampo"
            //    Animation="None">
            //</telerik:RadToolTip>


            this.Controls.Add(txt);
            this.Controls.Add(req);
            this.Controls.Add(toolTipCampo);


        }


        protected override IEnumerable<ScriptDescriptor>
                GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor = new ScriptControlDescriptor("ControlsAjaxNotti.ClientServerCampoNumerico", this.ClientID);
            yield return descriptor;
        }

        // Generate the script reference
        protected override IEnumerable<ScriptReference>
                GetScriptReferences()
        {
            List<ScriptReference> js = new List<ScriptReference>();
            js.Add(new ScriptReference("ControlsAjaxNotti.ClientServerCampoNumerico.js", this.GetType().Assembly.FullName));
            js.Add(new ScriptReference("ControlsAjaxNotti.jquery-1.3.2.min.js", this.GetType().Assembly.FullName));
            return js;

        }


    }



}

