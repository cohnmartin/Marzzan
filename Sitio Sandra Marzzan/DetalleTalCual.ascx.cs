using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class DetalleTalCual : System.Web.UI.UserControl
{
    public string ProductID
    {
        get
        {
            if (ViewState["ProductID"] == null)
            {
                return "";
            }
            return (string)ViewState["ProductID"];
        }
        set
        {
            ViewState["ProductID"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //RadNumericTextBoxsdfds1.Value = double.Parse(ViewState["ProductID"].ToString());
    }
    public void iniciar(string valor)
    {
        RadNumericTextBoxsdfds1.Text = valor;
    }
}
