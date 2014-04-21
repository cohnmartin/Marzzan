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

public partial class DetalleProducto : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void SelectedImage()
    {

        int a = new Random().Next(0,3);
        switch (a.ToString())
        {
            case "0":
                Image1.ImageUrl="~/Imagenes/TalCual01.jpg" ;
                break;
            case "1":
                Image1.ImageUrl = "~/Imagenes/TalCual02.jpg";
                break;
            case "2":
                Image1.ImageUrl = "~/Imagenes/TalCual03.jpg";
                break;
            case "3":
                Image1.ImageUrl = "~/Imagenes/TalCual04.jpg";
                break;
        }
    
    }
}
