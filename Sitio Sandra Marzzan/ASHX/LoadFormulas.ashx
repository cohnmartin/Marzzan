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
    
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string textIngresado = context.Request.QueryString["query"];

        List<TempClass.tempBusqueda> datosResult = new List<TempClass.tempBusqueda>();
        List<TempClass.tempBusqueda> datosFiltrados = new List<TempClass.tempBusqueda>();


        datosResult = (List<TempClass.tempBusqueda>)context.Session["DatosFormulas"];
        long result;
        if (long.TryParse(textIngresado, out result))
        {
            datosFiltrados = (from m in datosResult
                              where m.CodigoFormula.ToUpper().StartsWith(textIngresado.ToUpper())
                              select m).ToList();
        }
        else
        {
            datosFiltrados = (from m in datosResult
                              where m.NombreFormula.ToUpper().Contains(textIngresado.ToUpper())
                              select m).ToList();
        }


        string cadenaDatos = "";
        string cadenaValues = "";
        foreach (TempClass.tempBusqueda item in datosFiltrados)
        {
            cadenaDatos += "'" + item.CodigoFormula +" - "+ item.NombreFormula + "',";
            cadenaValues += "'" + item.CodigoFormula + "',";
        }

        if (cadenaDatos.Length > 0)
            cadenaDatos = cadenaDatos.Substring(0, cadenaDatos.Length - 1);

        context.Response.Write("{query:'" + textIngresado + "',  suggestions:[" + cadenaDatos + "],  data:[" + cadenaValues + "]}");


    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}