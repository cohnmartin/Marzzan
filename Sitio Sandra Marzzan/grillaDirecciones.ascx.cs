using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CommonMarzzan;
using System.Collections.Generic;

public partial class grillaDirecciones : System.Web.UI.UserControl
{
    public event RowSelectedHanddler DireccionSeleccionada;
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void InitControl(string CodigoExterno)
    {
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();

        IList direcciones = (from D in dc.Direcciones
                             where D.CodigoExterno == CodigoExterno
                             select D).ToList();


        List<Direccione> DireccionesSinRepetir = new List<Direccione>();
        foreach (Direccione dir in direcciones)
	    {

            Direccione dirExistente = (from D in DireccionesSinRepetir
                                       where D.CodigoExternoDir == dir.CodigoExternoDir
                                       select D).FirstOrDefault();

            if (dirExistente == null)
            {
                DireccionesSinRepetir.Add(dir);
            }
	    }


        grillaDir.DataSource = DireccionesSinRepetir;
        grillaDir.DataBind();
    
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        if (DireccionSeleccionada != null && grillaDir.SelectedItems.Count>0)
            DireccionSeleccionada(long.Parse(grillaDir.Items[grillaDir.SelectedItems[0].DataSetIndex].GetDataKeyValue("IdDireccion").ToString()));
    }
}
