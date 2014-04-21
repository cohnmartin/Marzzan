using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CargaProducto : System.Web.UI.Page
{
    public string CodigoEdicion
    {
        get
        {
            if (Request.QueryString["CodigoEdicion"] != null)
                return Request.QueryString["CodigoEdicion"].ToString();
            else
                return "0";
                
        }
    }
    public string Cantidad
    {
        get
        {
            if (Request.QueryString["Cantidad"] != null)
                return Request.QueryString["Cantidad"].ToString();
            else
                return "0";

        }
    }
    public string Codigos
    {
        get { 
            return Request.QueryString["Codigos"].ToString();
        }
    }
    public string Descripciones
    {
        get
        {
            return Request.QueryString["descripciones"].ToString();
        }
    }
    public string Precios
    {
        get
        {
            return Request.QueryString["precios"].ToString();
        }
    }
    public string Id
    {
        get
        {
            return Request.QueryString["id"].ToString();
        }
    }
    public string ProductName
    {
        get
        {
            return Request.QueryString["ProductName"].ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
}
