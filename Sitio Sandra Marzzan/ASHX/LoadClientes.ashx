<%@ WebHandler Language="C#" Class="LoadClientes" %>

using System;
using System.Web;
using CommonMarzzan;
using System.Data.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Text;
using System.Net;

public class LoadClientes : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string textIngresado = context.Request.QueryString["query"];
        List<string> grupos = (List<string>)context.Session["GruposValidos"];
        List<long> usuarioInvalidos = (List<long>)context.Session["UsuarioInvalidos"];
        List<View_ClienteWeb> datosFiltrados = new List<View_ClienteWeb>();
        usuarioInvalidos = usuarioInvalidos == null ? new List<long>() : usuarioInvalidos;
        
        Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
        
        
        if (grupos != null)
        {
            if (textIngresado.Contains("Ã±"))
                textIngresado = textIngresado.Replace("Ã±", "Ñ").Replace("ã±", "Ñ").ToLower();
            else
                textIngresado = textIngresado.Replace("Ã", "ñ").Replace("ã", "ñ").ToLower();
            
            datosFiltrados = (from c in dc.View_ClienteWebs
                              where grupos.Contains(c.Grupo)
                              && !usuarioInvalidos.Contains(c.Idcliente)
                              && c.Nombre.ToLower().StartsWith(textIngresado.ToLower())
                              select c).Take(30).ToList();
        }
        else
        {
            
            if (textIngresado.Contains("Ã±"))
                textIngresado = textIngresado.Replace("Ã±", "Ñ").Replace("ã±", "Ñ").ToLower();
            else
                textIngresado = textIngresado.Replace("Ã", "ñ").Replace("ã", "ñ").ToLower();

            datosFiltrados = (from c in dc.View_ClienteWebs
                              where c.Nombre.ToLower().StartsWith(textIngresado.ToLower())
                              && !usuarioInvalidos.Contains(c.Idcliente)
                              select c).Take(30).ToList();
        }

        string cadenaDatos = "";
        string cadenaValues = "";
        foreach (View_ClienteWeb item in datosFiltrados)
        {
            cadenaDatos += "'" + item.Nombre + "',";
            cadenaValues += "'" + item.Idcliente + "',";
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