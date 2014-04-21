using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonMarzzan;
public partial class GestionParametros : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinqDataSource1_Inserting(object sender, LinqDataSourceInsertEventArgs e)
    {
        (e.NewObject as Parametro).Contexto = "WEB";
    }
}
