<%@ WebHandler Language="C#" Class="LoadTransportes" %>

using System;
using System.Web;
using CommonMarzzan;
using System.Data.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Web.SessionState;

public class LoadTransportes : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string textIngresado = context.Request.QueryString["query"];

        List<View_Transporte> datosResult = new List<View_Transporte>();
        List<View_Transporte> datosFiltrados = new List<View_Transporte>();


        if (context.Session["DatosTransportesAshx"] == null)
        {
            /// Cargo la variable de session que luego se utilizara 
            /// para la busquede de legajos por documento
            Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
            datosResult = (from t in dc.View_Transportes
                           select t).ToList();
            
            context.Session.Add("DatosTransportesAshx", datosResult);
        }
        else
        {
            datosResult = (List<View_Transporte>)context.Session["DatosTransportesAshx"];
        }


        datosFiltrados = (from m in datosResult
                          where m.Transporte.ToUpper().StartsWith(textIngresado.ToUpper())
                          select m).ToList();


        string cadenaDatos = "";
        string cadenaValues = "";
        foreach (View_Transporte item in datosFiltrados)
        {
            cadenaDatos += "'" + item.Transporte + "',";
            cadenaValues += "'" + item.Transporte + "',";
        }

        if (cadenaDatos.Length > 0)
            cadenaDatos = cadenaDatos.Substring(0, cadenaDatos.Length - 1);

        context.Response.Write("{query:'" + textIngresado + "',  suggestions:[" + cadenaDatos + "],  data:[" + cadenaValues + "]}");
        
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}