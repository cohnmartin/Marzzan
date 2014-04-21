using System;
using System.Collections;
using System.Collections.Generic;
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
using Telerik.Web.UI;
using System.IO;
using CommonMarzzan;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.Script.Services;
using System.IO.Compression;

public partial class LogisticaEntrega : System.Web.UI.Page
{
    private Marzzan_InfolegacyDataContext Contexto
    {
        get
        {

            if (Session["Context"] == null)
            {
                Session.Add("Context", new Marzzan_InfolegacyDataContext());
            }

            return (Marzzan_InfolegacyDataContext)Session["Context"];
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {

        var logistica = (from L in Contexto.LogisticaEntregas
                         group L by L.Provincia into g
                         select new
                         {
                             Key = g.Key,
                             DatosProv = g
                         });

        //var ConsultasLogistica = (from L in Contexto.ConsultasEstadisticas
        //                          orderby L.Orden
        //                          select L);


        Random RandonSuc = new Random();
        Random RandonTrans = new Random();

        XDocument xDoc = new XDocument();
        List<XElement> NodosProv = new List<XElement>();

        foreach (var prov in logistica)
        {

            List<XElement> NodosTrans = new List<XElement>();
            var transportes = (from t in prov.DatosProv
                               select t.Transporte).Distinct();

            foreach (string trans in transportes)
            {
                List<XElement> NodosSucursales = new List<XElement>();
                var Sucursales = (from t in prov.DatosProv
                                  where t.Transporte == trans
                                  select t.Sucursal).Distinct();

                foreach (string suc in Sucursales)
                {
                    List<XElement> NodoFinal = new List<XElement>();
                    var DatosFinales = (from t in prov.DatosProv
                                        where t.Transporte == trans
                                        && t.Sucursal == suc
                                        select new
                                        {
                                            titulo = t.Titulo,
                                            descripcion = t.Descripcion
                                        }).Distinct();

                    foreach (var itemFinal in DatosFinales)
                    {
                        List<XElement> Titulos = new List<XElement>();

                        Titulos.Add(new XElement("Titulo", itemFinal.titulo));
                        Titulos.Add(new XElement("Descripcion", itemFinal.descripcion));
                        NodoFinal.Add(new XElement("Item", Titulos));
                    }


                    NodosSucursales.Add(new XElement("Sucursal", NodoFinal));
                    NodosSucursales[NodosSucursales.Count - 1].Add(new XAttribute("Name", suc));
                    NodosSucursales[NodosSucursales.Count - 1].Add(new XAttribute("Id", "Suc_" + RandonSuc.Next(1, 10000).ToString()));
                }


                NodosTrans.Add(new XElement("Transporte", NodosSucursales));
                NodosTrans[NodosTrans.Count - 1].Add(new XAttribute("Name", trans));
                NodosTrans[NodosTrans.Count - 1].Add(new XAttribute("Id", "Tra_" + RandonTrans.Next(11000, 13000).ToString()));

            }

            NodosProv.Add(new XElement("Provincia", NodosTrans));
            NodosProv[NodosProv.Count - 1].Add(new XAttribute("Name", prov.Key));
            NodosProv[NodosProv.Count - 1].Add(new XAttribute("Id", prov.Key.Replace(" ", "").Replace(".", "")));
            
            /// Estructura del xml
            //<DatosProvincia>
            //  <Item>
            //    <Titulo>Total Femeninos</Titulo>
            //    <Descripcion>6549</Descripcion>
            //  </Item>
            //  <Item>
            //    <Titulo></Titulo>
            //    <Descripcion>6549</Descripcion>
            //  </Item>
            //  <Item>
            //    <Titulo></Titulo>
            //    <Descripcion>6333</Descripcion>
            //  </Item>
            //</DatosProvincia>


            //////////////// Codigo para agragar consultas dinamicas al mapa //////////////////////////////
            /// Codigo Fijo
            //string[] TitulosTemp = new string[3] { "Total Femeninos", "Total Clientes", "Clientes Iniciales" };
            //string[] ValoresTemp = new string[3] { "1520", "1566", "966" };
            //List<XElement> Items1 = new List<XElement>();
            //for (int i = 0; i < TitulosTemp.Count(); i++)
            //{
            //    Items1.Add(new XElement("Item", new XElement[] { new XElement("Titulo", TitulosTemp[i]), new XElement("Descripcion", ValoresTemp[i]) }));
            //}
            //NodosProv[NodosProv.Count - 1].Add(new XElement("DatosProvincia", Items1));

            /// Codigo Dinamico
            //List<XElement> Items1 = new List<XElement>();
            //foreach (ConsultasEstadistica Consul in ConsultasLogistica)
            //{
            //    string valor = GetValorConsulta(prov.Key.ToString(), Consul.Sql, Consul.Contexto);
            //    if (valor != "")
            //        Items1.Add(new XElement("Item", new XElement[] { new XElement("Titulo", Consul.Titulo), new XElement("Descripcion", valor) }));
            //}

            //NodosProv[NodosProv.Count - 1].Add(new XElement("DatosProvincia", Items1));

        }

        xDoc.Add(new XElement("Provincias", NodosProv));
        xDoc.Save(Server.MapPath("ImgLogistica") + @"\logistica.xml");

    }

    //private string GetValorConsulta(string Provincia, string SQL, string Contexto)
    //{

    //    System.Data.SqlClient.SqlConnection cnn = GetConexion(Contexto);
    //    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(SQL + " and provincia = '" + Provincia + "'", cnn);
    //    cmd.CommandType = System.Data.CommandType.Text;
    //    cnn.Open();
    //    System.Data.SqlClient.SqlDataReader typeCodaRbol = cmd.ExecuteReader();
    //    typeCodaRbol.Read();

    //    if (typeCodaRbol.HasRows)
    //        return typeCodaRbol.GetValue(0).ToString();
    //    else
    //        return "";

    //}

    //private System.Data.SqlClient.SqlConnection GetConexion(string Contexto)
    //{

    //    string cadenaConexionSQL = "";
    //    System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/");
    //    System.Configuration.ConnectionStringSettings connString;
    //    if (0 < rootWebConfig.ConnectionStrings.ConnectionStrings.Count)
    //    {
    //        if (Contexto == "Web")
    //            connString = rootWebConfig.ConnectionStrings.ConnectionStrings["MarzzanConnectionString"];
    //        else
    //            connString = rootWebConfig.ConnectionStrings.ConnectionStrings["MarzzanConnectionString"];


    //        if (null != connString)
    //            cadenaConexionSQL = connString.ConnectionString;
    //    }

    //    return new System.Data.SqlClient.SqlConnection(cadenaConexionSQL);

    //}
}
