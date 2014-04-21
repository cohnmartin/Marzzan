<%@ WebHandler Language="C#" Class="LoadLocalidades" %>

using System;
using System.Web;
using CommonMarzzan;
using System.Data.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Web.SessionState;

public class LoadLocalidades : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string textIngresado = context.Request.QueryString["query"].ToUpper();
        string Departamento = context.Request.QueryString["Departamento"];
        
        List<DivisionesPolitica> datosResult = new List<DivisionesPolitica>();
        List<DivisionesPolitica> datosFiltrados = new List<DivisionesPolitica>();

        try
        {
            datosResult = (List<DivisionesPolitica>)context.Session["DivisionesPoliticas"];
            datosFiltrados = (from m in datosResult
                              where m.Departamento == Departamento
                              && m.Localidad.StartsWith(textIngresado)
                              orderby m.Localidad
                              select m).ToList();
        }
        catch { }

        string cadenaDatos = "";
        string cadenaValues = "";
        foreach (string item in datosFiltrados.Select(w => w.Localidad).Distinct())
        {
            cadenaDatos += "'" + item + "',";
            cadenaValues += "'" + item + "',";
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