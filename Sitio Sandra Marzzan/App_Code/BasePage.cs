using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for BasePage
/// </summary>
public abstract class BasePage : Page
{
    protected  void Page_Load(object sender, EventArgs e)
    {

        /// Estas son las lines que debes descomentar para que no se pueda entrar desde afuera.
        /// es decir le tenes que sacar las 2 "//".
        //if (Request.Url.Host != "10.0.0.24")
        //{
        //    Response.Redirect("~/PaginaMantenimiento.aspx");
        //    return;
        //}

        // Do whatever standard code which occurs on every page
        if (Session["IdUsuario"] == null)
        {
            if (Request.Url.AbsolutePath.IndexOf("Login.aspx") < 0)
                Response.Redirect("~/Login.aspx");
            else
                PageLoad();
        }
        else
            PageLoad();

    }
    
    protected abstract void PageLoad();

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event to initialize the page.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        RegisterPageServiceRequestProxy();
    }

    /// <summary>
    /// Registers the page service request proxy. This is used by any user controls and/or master pages that also want to
    /// encapsulate web methods without having to put all of them in the page.
    /// </summary>
    /// <remarks>AJAX Voodoo!</remarks>
    private void RegisterPageServiceRequestProxy()
    {
        ScriptManager.RegisterClientScriptBlock(
            this,
            GetType(),
            "PageServiceRequestProxy",
            "function InvokeServiceRequest(typeName,methodName,successCallback,failureCallback){if(PageMethods.PageServiceRequest){try{var parms=[];for(var i=4;i<arguments.length;i++){parms.push(arguments[i]);}PageMethods.PageServiceRequest(typeName,methodName,parms,successCallback,failureCallback);}catch(e){alert(e.toString());}}}",
            true);
    }

    /// <summary>
    /// Creates a generic ScriptMethod for use on ANY public static method, whether it is in a UserControl, MasterPage or some other random place.
    /// </summary>
    /// <param name="typeName">Name of the type.</param>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="args">The args.</param>
    /// <returns>The string/value type result OR a JSON serialized instance of the object returned by the target of the invocation.</returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string PageServiceRequest(string typeName, string methodName, object[] args)
    {
        Type ctl = Type.GetType(typeName);
        if (ctl != null)
        {
            object o = ctl.InvokeMember(
                methodName,
                  System.Reflection.BindingFlags.Static
                | System.Reflection.BindingFlags.InvokeMethod
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.IgnoreCase,
                null, null, args ?? new object[] { });
            if (o != null)
            {
                if (o is string || o.GetType().IsValueType)
                    return o.ToString(); // If it is a string or value type, return a string

                // If it is a complex object, return a serialized version of it.
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(o); // allow anonymous types, etc
            }
        }
        return "{}"; // return an empty JSON object
    }
}
