using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EjemploPromos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string imagenes = @"
                    <li value='1'>
                        <img src='ImagenesPromos/thumbs/1.jpg.ashx' width='179' height='100' alt='' /></li>
                    <li value='2'>
                        <img src='ImagenesPromos/thumbs/2.jpg.ashx' width='179' height='100' alt='' /></li>
                    <li value='3'>
                        <img src='ImagenesPromos/thumbs/3.jpg.ashx' width='179' height='100' alt='' /></li>
                    <li value='4'>
                        <img src='ImagenesPromos/thumbs/4.jpg.ashx' width='179' height='100' alt='' /></li>
                   ";

            thumbs.InnerHtml = imagenes;
        }
    }
}
