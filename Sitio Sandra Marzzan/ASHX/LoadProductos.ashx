<%@ WebHandler Language="C#" Class="LoadProductos" %>

using System;
using System.Web;
using CommonMarzzan;
using System.Data.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Text;
using System.Net;

public class LoadProductos : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        List<View_ProductosPlano> datosFiltrados = new List<View_ProductosPlano>();
        context.Response.ContentType = "text/plain";
        string textIngresado = context.Request.QueryString["query"];

        using (Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext())
        {
            if (context.Session["ProductosPlanos"] == null)
            {
                context.Session["ProductosPlanos"] = (from p in dc.View_ProductosPlanos
                                                      select p).ToList();
            }


        }


        datosFiltrados = (from p in (context.Session["ProductosPlanos"] as List<View_ProductosPlano>)
                          where p.Descripcion.Trim().ToUpper().Contains(textIngresado.ToUpper()) || p.Fragancia.Trim().ToUpper().Contains(textIngresado.ToUpper())
                          select p).Take(30).ToList();



        string cadenaDatos = "";
        string cadenaValues = "";
        foreach (View_ProductosPlano item in datosFiltrados)
        {
            if (item.Descripcion == item.Fragancia)
                cadenaDatos += "'" + item.Descripcion + "(" + item.Tipo + ")" + "',";
            else
                cadenaDatos += "'" + item.Fragancia + " x " + item.Descripcion + " (" + item.Tipo + ")" + "',";

            cadenaValues += "'" + item.IdPresentacion + "',";
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