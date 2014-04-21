<%@ WebHandler Language="C#" Class="LoadDepartamentos" %>

using System;
using System.Web;
using CommonMarzzan;
using System.Data.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Text.RegularExpressions;


public class LoadDepartamentos : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {

        context.Response.ContentType = "text/plain";
        string textIngresado = context.Request.QueryString["query"];
        string Empresa = HttpUtility.UrlDecode(context.Request.QueryString["Empresa"], System.Text.Encoding.UTF8);


        textIngresado = textIngresado.Length > 15 ? "" : textIngresado;
        Empresa = Empresa == "" ? "Mendoza" : Empresa;

        List<DivisionesPolitica> datosResult = new List<DivisionesPolitica>();
        List<DivisionesPolitica> datosFiltrados = new List<DivisionesPolitica>();




        datosResult = (List<DivisionesPolitica>)context.Session["DivisionesPoliticas"];
        //try
        //{
        datosFiltrados = (from m in datosResult
                          where m.Provincia.ToUpper() == Empresa.ToUpper()
                          && m.Localidad != null && m.Localidad.ToUpper().StartsWith(textIngresado.ToUpper())
                          orderby m.Departamento
                          select m).ToList();

        //}
        //catch
        //{ }

        string cadenaDatos = "";

        if (context.Session["ShowNOESTAENLALISTA"] == null || Convert.ToBoolean(context.Session["ShowNOESTAENLALISTA"].ToString()) == true)
        {
            cadenaDatos = "'NO ESTA EN LA LISTA',";
        }
        else
        {
            cadenaDatos = "";
        }
        
        
        string cadenaValues = "'',";
        string cadenaAdicional = "'',";

        foreach (var item in datosFiltrados.Select(w => new { w.Localidad, w.CodigoPostal }).Distinct())
        {
            cadenaDatos += "'" + item.Localidad + "',";
            cadenaValues += "'" + item.Localidad + "',";
            cadenaAdicional += "'" + item.CodigoPostal + "',";
        }

        if (cadenaDatos.Length > 0)
            cadenaDatos = cadenaDatos.Substring(0, cadenaDatos.Length - 1);

        context.Response.Write("{query:'" + textIngresado + "',  suggestions:[" + cadenaDatos + "],  data:[" + cadenaValues + "],  aditionalData:[" + cadenaAdicional + "]}");


    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}