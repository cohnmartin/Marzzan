using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VistaCatalogo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("ImagenesCelular/thumbs"));
            int cantidadImgagenes = dir.GetFiles().Where(w => w.Extension.ToLower() == ".jpg").Count();
            string imagenes = "";

            //for (int j = 1; j < 2; j++)
            //{
                for (int i = 1; i <= cantidadImgagenes; i++)
                {
                    imagenes += string.Format("<li value='{0}'><img src='ImagenesCelular/thumbs/{1}.jpg' width='179' height='126' alt='' /></li>", i, i);
                }
            //}
            thumbs.InnerHtml = imagenes;
        }
    }
}
